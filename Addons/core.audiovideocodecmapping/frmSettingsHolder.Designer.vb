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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblAudio = New System.Windows.Forms.Label()
        Me.dgvVideo = New System.Windows.Forms.DataGridView()
        Me.lblVideo = New System.Windows.Forms.Label()
        Me.dgvAudio = New System.Windows.Forms.DataGridView()
        Me.btnLoadDefaultsVideo = New System.Windows.Forms.Button()
        Me.btnLoadDefaultsAudio = New System.Windows.Forms.Button()
        Me.colAudioCodec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAudioMapping = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVideoCodec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVideoMapping = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        CType(Me.dgvVideo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAudio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(684, 461)
        Me.pnlMain.TabIndex = 0
        '
        'tblMain
        '
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 3
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblMain.Controls.Add(Me.lblAudio, 0, 0)
        Me.tblMain.Controls.Add(Me.dgvVideo, 2, 1)
        Me.tblMain.Controls.Add(Me.lblVideo, 2, 0)
        Me.tblMain.Controls.Add(Me.dgvAudio, 0, 1)
        Me.tblMain.Controls.Add(Me.btnLoadDefaultsVideo, 2, 2)
        Me.tblMain.Controls.Add(Me.btnLoadDefaultsAudio, 0, 2)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 3
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(684, 461)
        Me.tblMain.TabIndex = 10
        '
        'lblAudio
        '
        Me.lblAudio.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAudio.AutoSize = True
        Me.lblAudio.Location = New System.Drawing.Point(3, 6)
        Me.lblAudio.Name = "lblAudio"
        Me.lblAudio.Size = New System.Drawing.Size(34, 13)
        Me.lblAudio.TabIndex = 0
        Me.lblAudio.Text = "Audio"
        '
        'dgvVideo
        '
        Me.dgvVideo.AllowUserToResizeColumns = False
        Me.dgvVideo.AllowUserToResizeRows = False
        Me.dgvVideo.BackgroundColor = System.Drawing.Color.White
        Me.dgvVideo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVideo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colVideoCodec, Me.colVideoMapping})
        Me.dgvVideo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvVideo.Location = New System.Drawing.Point(355, 28)
        Me.dgvVideo.Name = "dgvVideo"
        Me.dgvVideo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvVideo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvVideo.ShowCellErrors = False
        Me.dgvVideo.ShowCellToolTips = False
        Me.dgvVideo.ShowRowErrors = False
        Me.dgvVideo.Size = New System.Drawing.Size(326, 401)
        Me.dgvVideo.TabIndex = 5
        '
        'lblVideo
        '
        Me.lblVideo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVideo.AutoSize = True
        Me.lblVideo.Location = New System.Drawing.Point(355, 6)
        Me.lblVideo.Name = "lblVideo"
        Me.lblVideo.Size = New System.Drawing.Size(34, 13)
        Me.lblVideo.TabIndex = 4
        Me.lblVideo.Text = "Video"
        '
        'dgvAudio
        '
        Me.dgvAudio.AllowUserToResizeColumns = False
        Me.dgvAudio.AllowUserToResizeRows = False
        Me.dgvAudio.BackgroundColor = System.Drawing.Color.White
        Me.dgvAudio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAudio.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colAudioCodec, Me.colAudioMapping})
        Me.dgvAudio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAudio.Location = New System.Drawing.Point(3, 28)
        Me.dgvAudio.Name = "dgvAudio"
        Me.dgvAudio.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvAudio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAudio.ShowCellErrors = False
        Me.dgvAudio.ShowCellToolTips = False
        Me.dgvAudio.ShowRowErrors = False
        Me.dgvAudio.Size = New System.Drawing.Size(326, 401)
        Me.dgvAudio.TabIndex = 1
        '
        'btnLoadDefaultsVideo
        '
        Me.btnLoadDefaultsVideo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadDefaultsVideo.Image = CType(resources.GetObject("btnLoadDefaultsVideo.Image"), System.Drawing.Image)
        Me.btnLoadDefaultsVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadDefaultsVideo.Location = New System.Drawing.Point(355, 435)
        Me.btnLoadDefaultsVideo.Name = "btnLoadDefaultsVideo"
        Me.btnLoadDefaultsVideo.Size = New System.Drawing.Size(105, 23)
        Me.btnLoadDefaultsVideo.TabIndex = 9
        Me.btnLoadDefaultsVideo.Text = "Defaults"
        Me.btnLoadDefaultsVideo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLoadDefaultsVideo.UseVisualStyleBackColor = True
        '
        'btnLoadDefaultsAudio
        '
        Me.btnLoadDefaultsAudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadDefaultsAudio.Image = CType(resources.GetObject("btnLoadDefaultsAudio.Image"), System.Drawing.Image)
        Me.btnLoadDefaultsAudio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadDefaultsAudio.Location = New System.Drawing.Point(3, 435)
        Me.btnLoadDefaultsAudio.Name = "btnLoadDefaultsAudio"
        Me.btnLoadDefaultsAudio.Size = New System.Drawing.Size(105, 23)
        Me.btnLoadDefaultsAudio.TabIndex = 8
        Me.btnLoadDefaultsAudio.Text = "Defaults"
        Me.btnLoadDefaultsAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLoadDefaultsAudio.UseVisualStyleBackColor = True
        '
        'colAudioCodec
        '
        Me.colAudioCodec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colAudioCodec.HeaderText = "Mediainfo Codec"
        Me.colAudioCodec.Name = "colAudioCodec"
        '
        'colAudioMapping
        '
        Me.colAudioMapping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colAudioMapping.DefaultCellStyle = DataGridViewCellStyle2
        Me.colAudioMapping.HeaderText = "Mapped Codec"
        Me.colAudioMapping.MinimumWidth = 120
        Me.colAudioMapping.Name = "colAudioMapping"
        Me.colAudioMapping.Width = 120
        '
        'colVideoCodec
        '
        Me.colVideoCodec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colVideoCodec.HeaderText = "Mediainfo Codec"
        Me.colVideoCodec.Name = "colVideoCodec"
        '
        'colVideoMapping
        '
        Me.colVideoMapping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colVideoMapping.DefaultCellStyle = DataGridViewCellStyle1
        Me.colVideoMapping.HeaderText = "Mapped Codec"
        Me.colVideoMapping.MinimumWidth = 120
        Me.colVideoMapping.Name = "colVideoMapping"
        Me.colVideoMapping.Width = 120
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 461)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmAVCodecEditor"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        CType(Me.dgvVideo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAudio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents dgvAudio As System.Windows.Forms.DataGridView
    Friend WithEvents lblVideo As System.Windows.Forms.Label
    Friend WithEvents lblAudio As System.Windows.Forms.Label
    Friend WithEvents dgvVideo As System.Windows.Forms.DataGridView
    Friend WithEvents btnLoadDefaultsVideo As System.Windows.Forms.Button
    Friend WithEvents btnLoadDefaultsAudio As System.Windows.Forms.Button
    Friend WithEvents tblMain As Windows.Forms.TableLayoutPanel
    Friend WithEvents colVideoCodec As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colVideoMapping As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAudioCodec As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAudioMapping As Windows.Forms.DataGridViewTextBoxColumn
End Class
