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
        Me.btnTestInstallation = New System.Windows.Forms.Button()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.chkUseAsVideoPlayer = New System.Windows.Forms.CheckBox()
        Me.chkUseAsAudioPlayer = New System.Windows.Forms.CheckBox()
        Me.lblMediaPlayerInfo = New System.Windows.Forms.Label()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
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
        Me.pnlSettingsTop.Size = New System.Drawing.Size(390, 23)
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(390, 23)
        Me.tblSettingsTop.TabIndex = 1
        '
        'cbEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 3)
        Me.chkEnabled.Name = "cbEnabled"
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
        Me.pnlSettings.Size = New System.Drawing.Size(390, 257)
        Me.pnlSettings.TabIndex = 0
        '
        'btnTestInstallation
        '
        Me.btnTestInstallation.AutoSize = True
        Me.btnTestInstallation.Location = New System.Drawing.Point(3, 98)
        Me.btnTestInstallation.Name = "btnTestInstallation"
        Me.btnTestInstallation.Size = New System.Drawing.Size(131, 23)
        Me.btnTestInstallation.TabIndex = 1
        Me.btnTestInstallation.Text = "Check VLC Installation"
        Me.btnTestInstallation.UseVisualStyleBackColor = True
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(390, 234)
        Me.pnlSettingsMain.TabIndex = 2
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 2
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.btnTestInstallation, 0, 3)
        Me.tblSettingsMain.Controls.Add(Me.chkUseAsVideoPlayer, 0, 1)
        Me.tblSettingsMain.Controls.Add(Me.chkUseAsAudioPlayer, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.lblMediaPlayerInfo, 0, 2)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 5
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(390, 234)
        Me.tblSettingsMain.TabIndex = 0
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
        'lblMediaPlayerInfo
        '
        Me.lblMediaPlayerInfo.AutoSize = True
        Me.lblMediaPlayerInfo.Location = New System.Drawing.Point(3, 66)
        Me.lblMediaPlayerInfo.Margin = New System.Windows.Forms.Padding(3, 20, 3, 3)
        Me.lblMediaPlayerInfo.Name = "lblMediaPlayerInfo"
        Me.lblMediaPlayerInfo.Size = New System.Drawing.Size(313, 26)
        Me.lblMediaPlayerInfo.TabIndex = 4
        Me.lblMediaPlayerInfo.Text = "Needs VLC x86 with installed ActiveX plugin." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Works only with official release (n" & _
    "o Nightly Builds support)."
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(390, 257)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnTestInstallation As System.Windows.Forms.Button
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkUseAsVideoPlayer As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseAsAudioPlayer As System.Windows.Forms.CheckBox
    Friend WithEvents lblMediaPlayerInfo As System.Windows.Forms.Label

End Class
