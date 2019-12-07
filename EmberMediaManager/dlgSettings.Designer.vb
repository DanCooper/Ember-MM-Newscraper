<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Catch ex As Exception
        End Try
        Try
            'Finally
            MyBase.Dispose(disposing)
        Catch ex As Exception
        End Try
    End Sub

    Delegate Sub DelegateSub(ByVal b As Boolean)

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgSettings))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.lblSettingsTopDetails = New System.Windows.Forms.Label()
        Me.lblSettingsTopTitle = New System.Windows.Forms.Label()
        Me.pbSettingsTopLogo = New System.Windows.Forms.PictureBox()
        Me.ilSettings = New System.Windows.Forms.ImageList(Me.components)
        Me.tvSettingsList = New System.Windows.Forms.TreeView()
        Me.lblSettingsCurrent = New System.Windows.Forms.Label()
        Me.pnlSettingsCurrent = New System.Windows.Forms.Panel()
        Me.pbSettingsCurrent = New System.Windows.Forms.PictureBox()
        Me.ToolTips = New System.Windows.Forms.ToolTip(Me.components)
        Me.tsSettingsTopMenu = New System.Windows.Forms.ToolStrip()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.scSettings = New System.Windows.Forms.SplitContainer()
        Me.tblSettingsCurrent = New System.Windows.Forms.TableLayoutPanel()
        Me.scSettingsBody = New System.Windows.Forms.SplitContainer()
        Me.scSettingsMain = New System.Windows.Forms.SplitContainer()
        Me.tblSettingsFooter = New System.Windows.Forms.TableLayoutPanel()
        Me.ssBottom = New System.Windows.Forms.StatusStrip()
        Me.pnlSettingsTop.SuspendLayout()
        CType(Me.pbSettingsTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSettingsCurrent.SuspendLayout()
        CType(Me.pbSettingsCurrent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scSettings.Panel1.SuspendLayout()
        Me.scSettings.Panel2.SuspendLayout()
        Me.scSettings.SuspendLayout()
        Me.tblSettingsCurrent.SuspendLayout()
        CType(Me.scSettingsBody, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scSettingsBody.Panel1.SuspendLayout()
        Me.scSettingsBody.Panel2.SuspendLayout()
        Me.scSettingsBody.SuspendLayout()
        CType(Me.scSettingsMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scSettingsMain.Panel1.SuspendLayout()
        Me.scSettingsMain.Panel2.SuspendLayout()
        Me.scSettingsMain.SuspendLayout()
        Me.tblSettingsFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.AutoSize = True
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOK.Location = New System.Drawing.Point(1299, 3)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnOK.MinimumSize = New System.Drawing.Size(75, 23)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.AutoSize = True
        Me.btnApply.Enabled = False
        Me.btnApply.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnApply.Location = New System.Drawing.Point(1137, 3)
        Me.btnApply.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnApply.MinimumSize = New System.Drawing.Size(75, 23)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 2
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(1218, 3)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.btnCancel.MinimumSize = New System.Drawing.Size(75, 23)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlSettingsTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSettingsTop.Controls.Add(Me.lblSettingsTopDetails)
        Me.pnlSettingsTop.Controls.Add(Me.lblSettingsTopTitle)
        Me.pnlSettingsTop.Controls.Add(Me.pbSettingsTopLogo)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(1384, 64)
        Me.pnlSettingsTop.TabIndex = 3
        '
        'lblSettingsTopDetails
        '
        Me.lblSettingsTopDetails.AutoSize = True
        Me.lblSettingsTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblSettingsTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSettingsTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblSettingsTopDetails.Location = New System.Drawing.Point(61, 38)
        Me.lblSettingsTopDetails.Name = "lblSettingsTopDetails"
        Me.lblSettingsTopDetails.Size = New System.Drawing.Size(245, 13)
        Me.lblSettingsTopDetails.TabIndex = 1
        Me.lblSettingsTopDetails.Text = "Configure Ember's appearance and operation."
        '
        'lblSettingsTopTitle
        '
        Me.lblSettingsTopTitle.AutoSize = True
        Me.lblSettingsTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblSettingsTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSettingsTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblSettingsTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblSettingsTopTitle.Name = "lblSettingsTopTitle"
        Me.lblSettingsTopTitle.Size = New System.Drawing.Size(107, 32)
        Me.lblSettingsTopTitle.TabIndex = 0
        Me.lblSettingsTopTitle.Text = "Settings"
        '
        'pbSettingsTopLogo
        '
        Me.pbSettingsTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbSettingsTopLogo.Image = CType(resources.GetObject("pbSettingsTopLogo.Image"), System.Drawing.Image)
        Me.pbSettingsTopLogo.Location = New System.Drawing.Point(7, 8)
        Me.pbSettingsTopLogo.Name = "pbSettingsTopLogo"
        Me.pbSettingsTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbSettingsTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbSettingsTopLogo.TabIndex = 0
        Me.pbSettingsTopLogo.TabStop = False
        '
        'ilSettings
        '
        Me.ilSettings.ImageStream = CType(resources.GetObject("ilSettings.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilSettings.TransparentColor = System.Drawing.Color.Transparent
        Me.ilSettings.Images.SetKeyName(0, "process.png")
        Me.ilSettings.Images.SetKeyName(1, "comments.png")
        Me.ilSettings.Images.SetKeyName(2, "film.png")
        Me.ilSettings.Images.SetKeyName(3, "copy_paste.png")
        Me.ilSettings.Images.SetKeyName(4, "attachment.png")
        Me.ilSettings.Images.SetKeyName(5, "folder_full.png")
        Me.ilSettings.Images.SetKeyName(6, "image.png")
        Me.ilSettings.Images.SetKeyName(7, "television.ico")
        Me.ilSettings.Images.SetKeyName(8, "favorite_film.png")
        Me.ilSettings.Images.SetKeyName(9, "settingscheck.png")
        Me.ilSettings.Images.SetKeyName(10, "settingsx.png")
        Me.ilSettings.Images.SetKeyName(11, "note.png")
        Me.ilSettings.Images.SetKeyName(12, "dvd.png")
        Me.ilSettings.Images.SetKeyName(13, "equalizer.png")
        '
        'tvSettingsList
        '
        Me.tvSettingsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tvSettingsList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvSettingsList.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvSettingsList.FullRowSelect = True
        Me.tvSettingsList.HideSelection = False
        Me.tvSettingsList.ImageIndex = 0
        Me.tvSettingsList.ImageList = Me.ilSettings
        Me.tvSettingsList.Location = New System.Drawing.Point(5, 0)
        Me.tvSettingsList.Name = "tvSettingsList"
        Me.tvSettingsList.SelectedImageIndex = 0
        Me.tvSettingsList.ShowLines = False
        Me.tvSettingsList.ShowPlusMinus = False
        Me.tvSettingsList.Size = New System.Drawing.Size(242, 855)
        Me.tvSettingsList.TabIndex = 7
        '
        'lblSettingsCurrent
        '
        Me.lblSettingsCurrent.BackColor = System.Drawing.Color.Transparent
        Me.lblSettingsCurrent.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSettingsCurrent.ForeColor = System.Drawing.Color.White
        Me.lblSettingsCurrent.Location = New System.Drawing.Point(26, -1)
        Me.lblSettingsCurrent.Name = "lblSettingsCurrent"
        Me.lblSettingsCurrent.Size = New System.Drawing.Size(969, 25)
        Me.lblSettingsCurrent.TabIndex = 0
        Me.lblSettingsCurrent.Text = "General"
        '
        'pnlSettingsCurrent
        '
        Me.pnlSettingsCurrent.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlSettingsCurrent.Controls.Add(Me.pbSettingsCurrent)
        Me.pnlSettingsCurrent.Controls.Add(Me.lblSettingsCurrent)
        Me.pnlSettingsCurrent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsCurrent.Location = New System.Drawing.Point(5, 3)
        Me.pnlSettingsCurrent.Name = "pnlSettingsCurrent"
        Me.pnlSettingsCurrent.Size = New System.Drawing.Size(1374, 27)
        Me.pnlSettingsCurrent.TabIndex = 5
        '
        'pbSettingsCurrent
        '
        Me.pbSettingsCurrent.Location = New System.Drawing.Point(2, 0)
        Me.pbSettingsCurrent.Name = "pbSettingsCurrent"
        Me.pbSettingsCurrent.Size = New System.Drawing.Size(24, 24)
        Me.pbSettingsCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSettingsCurrent.TabIndex = 2
        Me.pbSettingsCurrent.TabStop = False
        '
        'tsSettingsTopMenu
        '
        Me.tsSettingsTopMenu.AllowMerge = False
        Me.tsSettingsTopMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsSettingsTopMenu.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsSettingsTopMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tsSettingsTopMenu.Location = New System.Drawing.Point(0, 64)
        Me.tsSettingsTopMenu.Name = "tsSettingsTopMenu"
        Me.tsSettingsTopMenu.Size = New System.Drawing.Size(1384, 25)
        Me.tsSettingsTopMenu.Stretch = True
        Me.tsSettingsTopMenu.TabIndex = 4
        Me.tsSettingsTopMenu.Text = "ToolStrip1"
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.BackColor = System.Drawing.Color.White
        Me.pnlSettingsMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(1128, 855)
        Me.pnlSettingsMain.TabIndex = 9
        '
        'scSettings
        '
        Me.scSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scSettings.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scSettings.IsSplitterFixed = True
        Me.scSettings.Location = New System.Drawing.Point(0, 0)
        Me.scSettings.Name = "scSettings"
        Me.scSettings.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scSettings.Panel1
        '
        Me.scSettings.Panel1.Controls.Add(Me.tblSettingsCurrent)
        Me.scSettings.Panel1.Controls.Add(Me.tsSettingsTopMenu)
        Me.scSettings.Panel1.Controls.Add(Me.pnlSettingsTop)
        '
        'scSettings.Panel2
        '
        Me.scSettings.Panel2.Controls.Add(Me.scSettingsBody)
        Me.scSettings.Size = New System.Drawing.Size(1384, 1039)
        Me.scSettings.SplitterDistance = 145
        Me.scSettings.TabIndex = 28
        '
        'tblSettingsCurrent
        '
        Me.tblSettingsCurrent.ColumnCount = 1
        Me.tblSettingsCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSettingsCurrent.Controls.Add(Me.pnlSettingsCurrent, 0, 0)
        Me.tblSettingsCurrent.Dock = System.Windows.Forms.DockStyle.Top
        Me.tblSettingsCurrent.Location = New System.Drawing.Point(0, 89)
        Me.tblSettingsCurrent.Name = "tblSettingsCurrent"
        Me.tblSettingsCurrent.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.tblSettingsCurrent.RowCount = 1
        Me.tblSettingsCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSettingsCurrent.Size = New System.Drawing.Size(1384, 33)
        Me.tblSettingsCurrent.TabIndex = 6
        '
        'scSettingsBody
        '
        Me.scSettingsBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scSettingsBody.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.scSettingsBody.IsSplitterFixed = True
        Me.scSettingsBody.Location = New System.Drawing.Point(0, 0)
        Me.scSettingsBody.Name = "scSettingsBody"
        Me.scSettingsBody.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scSettingsBody.Panel1
        '
        Me.scSettingsBody.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.scSettingsBody.Panel1.Controls.Add(Me.scSettingsMain)
        '
        'scSettingsBody.Panel2
        '
        Me.scSettingsBody.Panel2.Controls.Add(Me.tblSettingsFooter)
        Me.scSettingsBody.Panel2.Padding = New System.Windows.Forms.Padding(2, 0, 5, 0)
        Me.scSettingsBody.Size = New System.Drawing.Size(1384, 890)
        Me.scSettingsBody.SplitterDistance = 855
        Me.scSettingsBody.TabIndex = 99
        '
        'scSettingsMain
        '
        Me.scSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scSettingsMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.scSettingsMain.Name = "scSettingsMain"
        '
        'scSettingsMain.Panel1
        '
        Me.scSettingsMain.Panel1.Controls.Add(Me.tvSettingsList)
        Me.scSettingsMain.Panel1.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        '
        'scSettingsMain.Panel2
        '
        Me.scSettingsMain.Panel2.Controls.Add(Me.pnlSettingsMain)
        Me.scSettingsMain.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.scSettingsMain.Size = New System.Drawing.Size(1384, 855)
        Me.scSettingsMain.SplitterDistance = 247
        Me.scSettingsMain.TabIndex = 0
        '
        'tblSettingsFooter
        '
        Me.tblSettingsFooter.AutoSize = True
        Me.tblSettingsFooter.ColumnCount = 4
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsFooter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsFooter.Controls.Add(Me.btnApply, 1, 0)
        Me.tblSettingsFooter.Controls.Add(Me.btnCancel, 2, 0)
        Me.tblSettingsFooter.Controls.Add(Me.btnOK, 3, 0)
        Me.tblSettingsFooter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsFooter.Location = New System.Drawing.Point(2, 0)
        Me.tblSettingsFooter.Name = "tblSettingsFooter"
        Me.tblSettingsFooter.RowCount = 1
        Me.tblSettingsFooter.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsFooter.Size = New System.Drawing.Size(1377, 31)
        Me.tblSettingsFooter.TabIndex = 0
        '
        'ssBottom
        '
        Me.ssBottom.Location = New System.Drawing.Point(0, 1039)
        Me.ssBottom.Name = "ssBottom"
        Me.ssBottom.Size = New System.Drawing.Size(1384, 22)
        Me.ssBottom.TabIndex = 3
        Me.ssBottom.Text = "StatusStrip1"
        '
        'dlgSettings
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1384, 1061)
        Me.Controls.Add(Me.scSettings)
        Me.Controls.Add(Me.ssBottom)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "dlgSettings"
        Me.Text = "Settings"
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        CType(Me.pbSettingsTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSettingsCurrent.ResumeLayout(False)
        CType(Me.pbSettingsCurrent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scSettings.Panel1.ResumeLayout(False)
        Me.scSettings.Panel1.PerformLayout()
        Me.scSettings.Panel2.ResumeLayout(False)
        CType(Me.scSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scSettings.ResumeLayout(False)
        Me.tblSettingsCurrent.ResumeLayout(False)
        Me.scSettingsBody.Panel1.ResumeLayout(False)
        Me.scSettingsBody.Panel2.ResumeLayout(False)
        Me.scSettingsBody.Panel2.PerformLayout()
        CType(Me.scSettingsBody, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scSettingsBody.ResumeLayout(False)
        Me.scSettingsMain.Panel1.ResumeLayout(False)
        Me.scSettingsMain.Panel2.ResumeLayout(False)
        CType(Me.scSettingsMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scSettingsMain.ResumeLayout(False)
        Me.tblSettingsFooter.ResumeLayout(False)
        Me.tblSettingsFooter.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents lblSettingsTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblSettingsTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbSettingsTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents ilSettings As System.Windows.Forms.ImageList
    Friend WithEvents tvSettingsList As System.Windows.Forms.TreeView
    Friend WithEvents lblSettingsCurrent As System.Windows.Forms.Label
    Friend WithEvents pnlSettingsCurrent As System.Windows.Forms.Panel
    Friend WithEvents ToolTips As System.Windows.Forms.ToolTip
    Friend WithEvents tsSettingsTopMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents pbSettingsCurrent As System.Windows.Forms.PictureBox
    Friend WithEvents scSettings As System.Windows.Forms.SplitContainer
    Friend WithEvents scSettingsBody As System.Windows.Forms.SplitContainer
    Friend WithEvents scSettingsMain As System.Windows.Forms.SplitContainer
    Friend WithEvents tblSettingsFooter As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsCurrent As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ssBottom As StatusStrip
End Class