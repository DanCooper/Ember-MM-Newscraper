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
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGeneralOpts = New System.Windows.Forms.GroupBox()
        Me.tblGeneralOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkExportMissingEpisodes = New System.Windows.Forms.CheckBox()
        Me.lblGeneralPath = New System.Windows.Forms.Label()
        Me.btnExportPath = New System.Windows.Forms.Button()
        Me.txtExportPath = New System.Windows.Forms.TextBox()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbGeneralOpts.SuspendLayout()
        Me.tblGeneralOpts.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(388, 23)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 2
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.cbEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(388, 23)
        Me.tblSettingsTop.TabIndex = 15
        '
        'cbEnabled
        '
        Me.cbEnabled.AutoSize = True
        Me.cbEnabled.Location = New System.Drawing.Point(8, 3)
        Me.cbEnabled.Name = "cbEnabled"
        Me.cbEnabled.Size = New System.Drawing.Size(68, 17)
        Me.cbEnabled.TabIndex = 0
        Me.cbEnabled.Text = "Enabled"
        Me.cbEnabled.UseVisualStyleBackColor = True
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(388, 157)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(388, 134)
        Me.pnlSettingsMain.TabIndex = 23
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 2
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsMain.Controls.Add(Me.gbGeneralOpts, 0, 0)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 2
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsMain.Size = New System.Drawing.Size(388, 134)
        Me.tblSettingsMain.TabIndex = 0
        '
        'gbGeneralOpts
        '
        Me.gbGeneralOpts.AutoSize = True
        Me.gbGeneralOpts.Controls.Add(Me.tblGeneralOpts)
        Me.gbGeneralOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbGeneralOpts.Name = "gbGeneralOpts"
        Me.gbGeneralOpts.Size = New System.Drawing.Size(338, 72)
        Me.gbGeneralOpts.TabIndex = 20
        Me.gbGeneralOpts.TabStop = False
        Me.gbGeneralOpts.Text = "General Settings"
        '
        'tblGeneralOpts
        '
        Me.tblGeneralOpts.AutoSize = True
        Me.tblGeneralOpts.ColumnCount = 4
        Me.tblGeneralOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralOpts.Controls.Add(Me.chkExportMissingEpisodes, 0, 1)
        Me.tblGeneralOpts.Controls.Add(Me.lblGeneralPath, 0, 0)
        Me.tblGeneralOpts.Controls.Add(Me.btnExportPath, 2, 0)
        Me.tblGeneralOpts.Controls.Add(Me.txtExportPath, 1, 0)
        Me.tblGeneralOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralOpts.Name = "tblGeneralOpts"
        Me.tblGeneralOpts.RowCount = 3
        Me.tblGeneralOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralOpts.Size = New System.Drawing.Size(332, 51)
        Me.tblGeneralOpts.TabIndex = 23
        '
        'chkExportMissingEpisodes
        '
        Me.chkExportMissingEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkExportMissingEpisodes.AutoSize = True
        Me.tblGeneralOpts.SetColumnSpan(Me.chkExportMissingEpisodes, 3)
        Me.chkExportMissingEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkExportMissingEpisodes.Location = New System.Drawing.Point(3, 31)
        Me.chkExportMissingEpisodes.Name = "chkExportMissingEpisodes"
        Me.chkExportMissingEpisodes.Size = New System.Drawing.Size(155, 17)
        Me.chkExportMissingEpisodes.TabIndex = 15
        Me.chkExportMissingEpisodes.Text = "Display Missing Episodes"
        Me.chkExportMissingEpisodes.UseVisualStyleBackColor = True
        '
        'lblGeneralPath
        '
        Me.lblGeneralPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralPath.AutoSize = True
        Me.lblGeneralPath.Location = New System.Drawing.Point(3, 7)
        Me.lblGeneralPath.Name = "lblGeneralPath"
        Me.lblGeneralPath.Size = New System.Drawing.Size(66, 13)
        Me.lblGeneralPath.TabIndex = 12
        Me.lblGeneralPath.Text = "Export Path"
        Me.lblGeneralPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnExportPath
        '
        Me.btnExportPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnExportPath.Location = New System.Drawing.Point(308, 3)
        Me.btnExportPath.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExportPath.Name = "btnExportPath"
        Me.btnExportPath.Size = New System.Drawing.Size(24, 22)
        Me.btnExportPath.TabIndex = 14
        Me.btnExportPath.Text = "..."
        Me.btnExportPath.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExportPath.UseVisualStyleBackColor = True
        '
        'txt_exportmoviepath
        '
        Me.txtExportPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtExportPath.Location = New System.Drawing.Point(75, 3)
        Me.txtExportPath.Name = "txt_exportmoviepath"
        Me.txtExportPath.Size = New System.Drawing.Size(230, 22)
        Me.txtExportPath.TabIndex = 13
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(388, 157)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmSettingsHolder"
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
        Me.gbGeneralOpts.ResumeLayout(False)
        Me.gbGeneralOpts.PerformLayout()
        Me.tblGeneralOpts.ResumeLayout(False)
        Me.tblGeneralOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents cbEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents lblGeneralPath As System.Windows.Forms.Label
    Friend WithEvents btnExportPath As System.Windows.Forms.Button
    Friend WithEvents txtExportPath As System.Windows.Forms.TextBox
    Friend WithEvents gbGeneralOpts As System.Windows.Forms.GroupBox
    Friend WithEvents chkExportMissingEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneralOpts As System.Windows.Forms.TableLayoutPanel

End Class
