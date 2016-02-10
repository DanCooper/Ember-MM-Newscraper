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
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGeneralOpts = New System.Windows.Forms.GroupBox()
        Me.tblGeneralOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.lblVLCPath = New System.Windows.Forms.Label()
        Me.btnVLCPath = New System.Windows.Forms.Button()
        Me.txtVLCPath = New System.Windows.Forms.TextBox()
        Me.chkUseAsVideoPlayer = New System.Windows.Forms.CheckBox()
        Me.chkUseAsAudioPlayer = New System.Windows.Forms.CheckBox()
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
        Me.pnlSettingsTop.Size = New System.Drawing.Size(348, 23)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoScroll = True
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(348, 23)
        Me.tblSettingsTop.TabIndex = 1
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
        Me.pnlSettings.Size = New System.Drawing.Size(348, 215)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(348, 192)
        Me.pnlSettingsMain.TabIndex = 2
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 1
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbGeneralOpts, 0, 4)
        Me.tblSettingsMain.Controls.Add(Me.chkUseAsVideoPlayer, 0, 1)
        Me.tblSettingsMain.Controls.Add(Me.chkUseAsAudioPlayer, 0, 0)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 5
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsMain.Size = New System.Drawing.Size(348, 192)
        Me.tblSettingsMain.TabIndex = 0
        '
        'gbGeneralOpts
        '
        Me.gbGeneralOpts.AutoSize = True
        Me.gbGeneralOpts.Controls.Add(Me.tblGeneralOpts)
        Me.gbGeneralOpts.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbGeneralOpts.Location = New System.Drawing.Point(3, 49)
        Me.gbGeneralOpts.Name = "gbGeneralOpts"
        Me.gbGeneralOpts.Size = New System.Drawing.Size(342, 49)
        Me.gbGeneralOpts.TabIndex = 21
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
        Me.tblGeneralOpts.Controls.Add(Me.lblVLCPath, 0, 0)
        Me.tblGeneralOpts.Controls.Add(Me.btnVLCPath, 2, 0)
        Me.tblGeneralOpts.Controls.Add(Me.txtVLCPath, 1, 0)
        Me.tblGeneralOpts.Dock = System.Windows.Forms.DockStyle.Top
        Me.tblGeneralOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralOpts.Name = "tblGeneralOpts"
        Me.tblGeneralOpts.RowCount = 1
        Me.tblGeneralOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralOpts.Size = New System.Drawing.Size(336, 28)
        Me.tblGeneralOpts.TabIndex = 23
        '
        'lblVLCPath
        '
        Me.lblVLCPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVLCPath.AutoSize = True
        Me.lblVLCPath.Location = New System.Drawing.Point(3, 7)
        Me.lblVLCPath.Name = "lblVLCPath"
        Me.lblVLCPath.Size = New System.Drawing.Size(72, 13)
        Me.lblVLCPath.TabIndex = 12
        Me.lblVLCPath.Text = "VLC x86 Path"
        Me.lblVLCPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnVLCPath
        '
        Me.btnVLCPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnVLCPath.Location = New System.Drawing.Point(314, 3)
        Me.btnVLCPath.Margin = New System.Windows.Forms.Padding(0)
        Me.btnVLCPath.Name = "btnVLCPath"
        Me.btnVLCPath.Size = New System.Drawing.Size(24, 22)
        Me.btnVLCPath.TabIndex = 14
        Me.btnVLCPath.Text = "..."
        Me.btnVLCPath.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnVLCPath.UseVisualStyleBackColor = True
        '
        'txtVLCPath
        '
        Me.txtVLCPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtVLCPath.Location = New System.Drawing.Point(81, 3)
        Me.txtVLCPath.Name = "txtVLCPath"
        Me.txtVLCPath.Size = New System.Drawing.Size(230, 22)
        Me.txtVLCPath.TabIndex = 13
        '
        'chkUseAsVideoPlayer
        '
        Me.chkUseAsVideoPlayer.AutoSize = True
        Me.chkUseAsVideoPlayer.Location = New System.Drawing.Point(3, 26)
        Me.chkUseAsVideoPlayer.Name = "chkUseAsVideoPlayer"
        Me.chkUseAsVideoPlayer.Size = New System.Drawing.Size(125, 17)
        Me.chkUseAsVideoPlayer.TabIndex = 2
        Me.chkUseAsVideoPlayer.Text = "Use as Video Player"
        Me.chkUseAsVideoPlayer.UseVisualStyleBackColor = True
        '
        'chkUseAsAudioPlayer
        '
        Me.chkUseAsAudioPlayer.AutoSize = True
        Me.chkUseAsAudioPlayer.Location = New System.Drawing.Point(3, 3)
        Me.chkUseAsAudioPlayer.Name = "chkUseAsAudioPlayer"
        Me.chkUseAsAudioPlayer.Size = New System.Drawing.Size(126, 17)
        Me.chkUseAsAudioPlayer.TabIndex = 3
        Me.chkUseAsAudioPlayer.Text = "Use as Audio Player"
        Me.chkUseAsAudioPlayer.UseVisualStyleBackColor = True
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(348, 215)
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
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkUseAsVideoPlayer As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseAsAudioPlayer As System.Windows.Forms.CheckBox
    Friend WithEvents gbGeneralOpts As GroupBox
    Friend WithEvents tblGeneralOpts As TableLayoutPanel
    Friend WithEvents lblVLCPath As Label
    Friend WithEvents btnVLCPath As Button
    Friend WithEvents txtVLCPath As TextBox
End Class
