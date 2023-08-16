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
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperTrailerOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperTrailerOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkFallBackEng = New System.Windows.Forms.CheckBox()
        Me.pbApiKeyInfo = New System.Windows.Forms.PictureBox()
        Me.lblEMMAPI = New System.Windows.Forms.Label()
        Me.lblApiKey = New System.Windows.Forms.Label()
        Me.btnUnlockAPI = New System.Windows.Forms.Button()
        Me.txtApiKey = New System.Windows.Forms.TextBox()
        Me.pnlSettingsBottom = New System.Windows.Forms.Panel()
        Me.tblSettingsBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pbIconBottom = New System.Windows.Forms.PictureBox()
        Me.lblInfoBottom = New System.Windows.Forms.Label()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.lblScraperOrder = New System.Windows.Forms.Label()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbScraperTrailerOpts.SuspendLayout()
        Me.tblScraperTrailerOpts.SuspendLayout()
        CType(Me.pbApiKeyInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSettingsBottom.SuspendLayout()
        Me.tblSettingsBottom.SuspendLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsBottom)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(473, 192)
        Me.pnlSettings.TabIndex = 0
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 29)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(473, 126)
        Me.pnlSettingsMain.TabIndex = 98
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 2
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbScraperTrailerOpts, 0, 0)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 2
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(473, 126)
        Me.tblSettingsMain.TabIndex = 99
        '
        'gbScraperTrailerOpts
        '
        Me.gbScraperTrailerOpts.AutoSize = True
        Me.gbScraperTrailerOpts.Controls.Add(Me.tblScraperTrailerOpts)
        Me.gbScraperTrailerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperTrailerOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperTrailerOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperTrailerOpts.Name = "gbScraperTrailerOpts"
        Me.gbScraperTrailerOpts.Size = New System.Drawing.Size(432, 93)
        Me.gbScraperTrailerOpts.TabIndex = 96
        Me.gbScraperTrailerOpts.TabStop = False
        Me.gbScraperTrailerOpts.Text = "Trailers - Scraper specific"
        '
        'tblScraperTrailerOpts
        '
        Me.tblScraperTrailerOpts.AutoSize = True
        Me.tblScraperTrailerOpts.ColumnCount = 4
        Me.tblScraperTrailerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperTrailerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperTrailerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperTrailerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperTrailerOpts.Controls.Add(Me.pbApiKeyInfo, 2, 1)
        Me.tblScraperTrailerOpts.Controls.Add(Me.lblEMMAPI, 1, 0)
        Me.tblScraperTrailerOpts.Controls.Add(Me.lblApiKey, 0, 0)
        Me.tblScraperTrailerOpts.Controls.Add(Me.btnUnlockAPI, 0, 1)
        Me.tblScraperTrailerOpts.Controls.Add(Me.txtApiKey, 1, 1)
        Me.tblScraperTrailerOpts.Controls.Add(Me.chkFallBackEng, 0, 2)
        Me.tblScraperTrailerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperTrailerOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperTrailerOpts.Name = "tblScraperTrailerOpts"
        Me.tblScraperTrailerOpts.RowCount = 4
        Me.tblScraperTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperTrailerOpts.Size = New System.Drawing.Size(426, 72)
        Me.tblScraperTrailerOpts.TabIndex = 99
        '
        'chkFallBackEng
        '
        Me.chkFallBackEng.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFallBackEng.AutoSize = True
        Me.chkFallBackEng.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFallBackEng.Location = New System.Drawing.Point(3, 52)
        Me.chkFallBackEng.Name = "chkFallBackEng"
        Me.chkFallBackEng.Size = New System.Drawing.Size(129, 17)
        Me.chkFallBackEng.TabIndex = 4
        Me.chkFallBackEng.Text = "Fallback to english"
        Me.chkFallBackEng.UseVisualStyleBackColor = True
        '
        'pbApiKeyInfo
        '
        Me.pbApiKeyInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbApiKeyInfo.Image = CType(resources.GetObject("pbApiKeyInfo.Image"), System.Drawing.Image)
        Me.pbApiKeyInfo.Location = New System.Drawing.Point(407, 26)
        Me.pbApiKeyInfo.Name = "pbApiKeyInfo"
        Me.pbApiKeyInfo.Size = New System.Drawing.Size(16, 16)
        Me.pbApiKeyInfo.TabIndex = 5
        Me.pbApiKeyInfo.TabStop = False
        '
        'lblEMMAPI
        '
        Me.lblEMMAPI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblEMMAPI.AutoSize = True
        Me.tblScraperTrailerOpts.SetColumnSpan(Me.lblEMMAPI, 2)
        Me.lblEMMAPI.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblEMMAPI.Location = New System.Drawing.Point(171, 3)
        Me.lblEMMAPI.Name = "lblEMMAPI"
        Me.lblEMMAPI.Size = New System.Drawing.Size(220, 13)
        Me.lblEMMAPI.TabIndex = 16
        Me.lblEMMAPI.Text = "Ember Media Manager Embedded API Key"
        '
        'lblApiKey
        '
        Me.lblApiKey.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblApiKey.AutoSize = True
        Me.lblApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApiKey.Location = New System.Drawing.Point(3, 3)
        Me.lblApiKey.Name = "lblApiKey"
        Me.lblApiKey.Size = New System.Drawing.Size(79, 13)
        Me.lblApiKey.TabIndex = 0
        Me.lblApiKey.Text = "TMDB API Key:"
        '
        'btnUnlockAPI
        '
        Me.btnUnlockAPI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnUnlockAPI.Location = New System.Drawing.Point(3, 23)
        Me.btnUnlockAPI.Name = "btnUnlockAPI"
        Me.btnUnlockAPI.Size = New System.Drawing.Size(162, 23)
        Me.btnUnlockAPI.TabIndex = 15
        Me.btnUnlockAPI.Text = "Use my own API key"
        Me.btnUnlockAPI.UseVisualStyleBackColor = True
        '
        'txtApiKey
        '
        Me.txtApiKey.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtApiKey.Enabled = False
        Me.txtApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApiKey.Location = New System.Drawing.Point(171, 23)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.Size = New System.Drawing.Size(230, 22)
        Me.txtApiKey.TabIndex = 1
        '
        'pnlSettingsBottom
        '
        Me.pnlSettingsBottom.AutoSize = True
        Me.pnlSettingsBottom.Controls.Add(Me.tblSettingsBottom)
        Me.pnlSettingsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSettingsBottom.Location = New System.Drawing.Point(0, 155)
        Me.pnlSettingsBottom.Name = "pnlSettingsBottom"
        Me.pnlSettingsBottom.Size = New System.Drawing.Size(473, 37)
        Me.pnlSettingsBottom.TabIndex = 97
        '
        'tblSettingsBottom
        '
        Me.tblSettingsBottom.AutoSize = True
        Me.tblSettingsBottom.ColumnCount = 3
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.Controls.Add(Me.pbIconBottom, 0, 0)
        Me.tblSettingsBottom.Controls.Add(Me.lblInfoBottom, 1, 0)
        Me.tblSettingsBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsBottom.Name = "tblSettingsBottom"
        Me.tblSettingsBottom.RowCount = 2
        Me.tblSettingsBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsBottom.Size = New System.Drawing.Size(473, 37)
        Me.tblSettingsBottom.TabIndex = 98
        '
        'pbIconBottom
        '
        Me.pbIconBottom.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbIconBottom.Image = CType(resources.GetObject("pbIconBottom.Image"), System.Drawing.Image)
        Me.pbIconBottom.Location = New System.Drawing.Point(3, 3)
        Me.pbIconBottom.Name = "pbIconBottom"
        Me.pbIconBottom.Size = New System.Drawing.Size(30, 31)
        Me.pbIconBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconBottom.TabIndex = 94
        Me.pbIconBottom.TabStop = False
        '
        'lblInfoBottom
        '
        Me.lblInfoBottom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInfoBottom.AutoSize = True
        Me.lblInfoBottom.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblInfoBottom.ForeColor = System.Drawing.Color.Blue
        Me.lblInfoBottom.Location = New System.Drawing.Point(39, 6)
        Me.lblInfoBottom.Name = "lblInfoBottom"
        Me.lblInfoBottom.Size = New System.Drawing.Size(205, 24)
        Me.lblInfoBottom.TabIndex = 3
        Me.lblInfoBottom.Text = "These settings are specific to this module." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to the global settings " & _
    "for more options."
        Me.lblInfoBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(473, 29)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 5
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.btnDown, 4, 0)
        Me.tblSettingsTop.Controls.Add(Me.lblScraperOrder, 2, 0)
        Me.tblSettingsTop.Controls.Add(Me.btnUp, 3, 0)
        Me.tblSettingsTop.Controls.Add(Me.chkEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(473, 29)
        Me.tblSettingsTop.TabIndex = 98
        '
        'btnDown
        '
        Me.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(447, 3)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(23, 23)
        Me.btnDown.TabIndex = 3
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'lblScraperOrder
        '
        Me.lblScraperOrder.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblScraperOrder.AutoSize = True
        Me.lblScraperOrder.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScraperOrder.Location = New System.Drawing.Point(354, 8)
        Me.lblScraperOrder.Name = "lblScraperOrder"
        Me.lblScraperOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScraperOrder.TabIndex = 1
        Me.lblScraperOrder.Text = "Scraper order"
        '
        'btnUp
        '
        Me.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(418, 3)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.TabIndex = 2
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'chkEnabled
        '
        Me.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 6)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(473, 192)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.gbScraperTrailerOpts.ResumeLayout(False)
        Me.gbScraperTrailerOpts.PerformLayout()
        Me.tblScraperTrailerOpts.ResumeLayout(False)
        Me.tblScraperTrailerOpts.PerformLayout()
        CType(Me.pbApiKeyInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSettingsBottom.ResumeLayout(False)
        Me.pnlSettingsBottom.PerformLayout()
        Me.tblSettingsBottom.ResumeLayout(False)
        Me.tblSettingsBottom.PerformLayout()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents lblScraperOrder As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents lblInfoBottom As System.Windows.Forms.Label
    Friend WithEvents pbIconBottom As System.Windows.Forms.PictureBox
    Friend WithEvents gbScraperTrailerOpts As System.Windows.Forms.GroupBox
    Friend WithEvents pbApiKeyInfo As System.Windows.Forms.PictureBox
    Friend WithEvents chkFallBackEng As System.Windows.Forms.CheckBox
    Friend WithEvents lblApiKey As System.Windows.Forms.Label
    Friend WithEvents txtApiKey As System.Windows.Forms.TextBox
    Friend WithEvents lblEMMAPI As System.Windows.Forms.Label
    Friend WithEvents btnUnlockAPI As System.Windows.Forms.Button
    Friend WithEvents pnlSettingsBottom As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScraperTrailerOpts As System.Windows.Forms.TableLayoutPanel

End Class