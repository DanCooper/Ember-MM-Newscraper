<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOption_AVCodecMapping
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOption_AVCodecMapping))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbAudio = New System.Windows.Forms.GroupBox()
        Me.tblAudio = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvAudio = New System.Windows.Forms.DataGridView()
        Me.colAudioDetected = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAudioMappedCodec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAudioAdditionalFeatures = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnLoadDefaultsAudio = New System.Windows.Forms.Button()
        Me.gbVideo = New System.Windows.Forms.GroupBox()
        Me.tblVideo = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvVideo = New System.Windows.Forms.DataGridView()
        Me.colVideoDetected = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colVideoMappedCodec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnLoadDefaultsVideo = New System.Windows.Forms.Button()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbAudio.SuspendLayout()
        Me.tblAudio.SuspendLayout()
        CType(Me.dgvAudio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVideo.SuspendLayout()
        Me.tblVideo.SuspendLayout()
        CType(Me.dgvVideo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(555, 235)
        Me.pnlSettings.TabIndex = 1
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSettings.Controls.Add(Me.gbAudio, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbVideo, 1, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 1
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(555, 235)
        Me.tblSettings.TabIndex = 10
        '
        'gbAudio
        '
        Me.gbAudio.AutoSize = True
        Me.gbAudio.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbAudio.Controls.Add(Me.tblAudio)
        Me.gbAudio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbAudio.Location = New System.Drawing.Point(3, 3)
        Me.gbAudio.Name = "gbAudio"
        Me.gbAudio.Size = New System.Drawing.Size(271, 229)
        Me.gbAudio.TabIndex = 0
        Me.gbAudio.TabStop = False
        Me.gbAudio.Text = "Audio Codec Mapping"
        '
        'tblAudio
        '
        Me.tblAudio.AutoSize = True
        Me.tblAudio.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblAudio.BackColor = System.Drawing.Color.White
        Me.tblAudio.ColumnCount = 1
        Me.tblAudio.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblAudio.Controls.Add(Me.dgvAudio, 0, 0)
        Me.tblAudio.Controls.Add(Me.btnLoadDefaultsAudio, 0, 1)
        Me.tblAudio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblAudio.Location = New System.Drawing.Point(3, 18)
        Me.tblAudio.Name = "tblAudio"
        Me.tblAudio.RowCount = 2
        Me.tblAudio.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblAudio.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblAudio.Size = New System.Drawing.Size(265, 208)
        Me.tblAudio.TabIndex = 0
        '
        'dgvAudio
        '
        Me.dgvAudio.AllowUserToResizeColumns = False
        Me.dgvAudio.AllowUserToResizeRows = False
        Me.dgvAudio.BackgroundColor = System.Drawing.Color.White
        Me.dgvAudio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvAudio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAudio.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colAudioDetected, Me.colAudioMappedCodec, Me.colAudioAdditionalFeatures})
        Me.dgvAudio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAudio.Location = New System.Drawing.Point(3, 3)
        Me.dgvAudio.Name = "dgvAudio"
        Me.dgvAudio.RowHeadersWidth = 25
        Me.dgvAudio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAudio.ShowCellErrors = False
        Me.dgvAudio.ShowCellToolTips = False
        Me.dgvAudio.ShowRowErrors = False
        Me.dgvAudio.Size = New System.Drawing.Size(259, 173)
        Me.dgvAudio.TabIndex = 1
        '
        'colAudioDetected
        '
        Me.colAudioDetected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colAudioDetected.HeaderText = "Detected"
        Me.colAudioDetected.Name = "colAudioDetected"
        '
        'colAudioMappedCodec
        '
        Me.colAudioMappedCodec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colAudioMappedCodec.DefaultCellStyle = DataGridViewCellStyle1
        Me.colAudioMappedCodec.HeaderText = "Mapped Codec"
        Me.colAudioMappedCodec.Name = "colAudioMappedCodec"
        Me.colAudioMappedCodec.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colAudioAdditionalFeatures
        '
        Me.colAudioAdditionalFeatures.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colAudioAdditionalFeatures.HeaderText = "Additional Features"
        Me.colAudioAdditionalFeatures.Name = "colAudioAdditionalFeatures"
        '
        'btnAudioDefaults
        '
        Me.btnLoadDefaultsAudio.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadDefaultsAudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadDefaultsAudio.Image = CType(resources.GetObject("btnAudioDefaults.Image"), System.Drawing.Image)
        Me.btnLoadDefaultsAudio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadDefaultsAudio.Location = New System.Drawing.Point(157, 182)
        Me.btnLoadDefaultsAudio.Name = "btnAudioDefaults"
        Me.btnLoadDefaultsAudio.Size = New System.Drawing.Size(105, 23)
        Me.btnLoadDefaultsAudio.TabIndex = 8
        Me.btnLoadDefaultsAudio.Text = "Defaults"
        Me.btnLoadDefaultsAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLoadDefaultsAudio.UseVisualStyleBackColor = True
        '
        'gbVideo
        '
        Me.gbVideo.AutoSize = True
        Me.gbVideo.Controls.Add(Me.tblVideo)
        Me.gbVideo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbVideo.Location = New System.Drawing.Point(280, 3)
        Me.gbVideo.Name = "gbVideo"
        Me.gbVideo.Size = New System.Drawing.Size(272, 229)
        Me.gbVideo.TabIndex = 1
        Me.gbVideo.TabStop = False
        Me.gbVideo.Text = "Video Codec Mapping"
        '
        'tblVideo
        '
        Me.tblVideo.AutoSize = True
        Me.tblVideo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblVideo.BackColor = System.Drawing.Color.White
        Me.tblVideo.ColumnCount = 1
        Me.tblVideo.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblVideo.Controls.Add(Me.dgvVideo, 0, 0)
        Me.tblVideo.Controls.Add(Me.btnLoadDefaultsVideo, 0, 1)
        Me.tblVideo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblVideo.Location = New System.Drawing.Point(3, 18)
        Me.tblVideo.Name = "tblVideo"
        Me.tblVideo.RowCount = 2
        Me.tblVideo.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblVideo.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblVideo.Size = New System.Drawing.Size(266, 208)
        Me.tblVideo.TabIndex = 0
        '
        'dgvVideo
        '
        Me.dgvVideo.AllowUserToResizeColumns = False
        Me.dgvVideo.AllowUserToResizeRows = False
        Me.dgvVideo.BackgroundColor = System.Drawing.Color.White
        Me.dgvVideo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvVideo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVideo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colVideoDetected, Me.colVideoMappedCodec})
        Me.dgvVideo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvVideo.Location = New System.Drawing.Point(3, 3)
        Me.dgvVideo.Name = "dgvVideo"
        Me.dgvVideo.RowHeadersWidth = 25
        Me.dgvVideo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvVideo.ShowCellErrors = False
        Me.dgvVideo.ShowCellToolTips = False
        Me.dgvVideo.ShowRowErrors = False
        Me.dgvVideo.Size = New System.Drawing.Size(260, 173)
        Me.dgvVideo.TabIndex = 5
        '
        'colVideoDetected
        '
        Me.colVideoDetected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colVideoDetected.HeaderText = "Detected"
        Me.colVideoDetected.Name = "colVideoDetected"
        '
        'colVideoMappedCodec
        '
        Me.colVideoMappedCodec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colVideoMappedCodec.DefaultCellStyle = DataGridViewCellStyle2
        Me.colVideoMappedCodec.HeaderText = "Mapped Codec"
        Me.colVideoMappedCodec.Name = "colVideoMappedCodec"
        Me.colVideoMappedCodec.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'btnVideoDefaults
        '
        Me.btnLoadDefaultsVideo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadDefaultsVideo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadDefaultsVideo.Image = CType(resources.GetObject("btnVideoDefaults.Image"), System.Drawing.Image)
        Me.btnLoadDefaultsVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadDefaultsVideo.Location = New System.Drawing.Point(158, 182)
        Me.btnLoadDefaultsVideo.Name = "btnVideoDefaults"
        Me.btnLoadDefaultsVideo.Size = New System.Drawing.Size(105, 23)
        Me.btnLoadDefaultsVideo.TabIndex = 9
        Me.btnLoadDefaultsVideo.Text = "Defaults"
        Me.btnLoadDefaultsVideo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLoadDefaultsVideo.UseVisualStyleBackColor = True
        '
        'frmOption_AVCodecMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 235)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmOption_AVCodecMapping"
        Me.Text = "frmOption_AVCodecMapping"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbAudio.ResumeLayout(False)
        Me.gbAudio.PerformLayout()
        Me.tblAudio.ResumeLayout(False)
        CType(Me.dgvAudio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVideo.ResumeLayout(False)
        Me.gbVideo.PerformLayout()
        Me.tblVideo.ResumeLayout(False)
        CType(Me.dgvVideo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbAudio As GroupBox
    Friend WithEvents tblAudio As TableLayoutPanel
    Friend WithEvents dgvAudio As DataGridView
    Friend WithEvents btnLoadDefaultsAudio As Button
    Friend WithEvents gbVideo As GroupBox
    Friend WithEvents tblVideo As TableLayoutPanel
    Friend WithEvents dgvVideo As DataGridView
    Friend WithEvents btnLoadDefaultsVideo As Button
    Friend WithEvents colAudioDetected As DataGridViewTextBoxColumn
    Friend WithEvents colAudioMappedCodec As DataGridViewTextBoxColumn
    Friend WithEvents colAudioAdditionalFeatures As DataGridViewTextBoxColumn
    Friend WithEvents colVideoDetected As DataGridViewTextBoxColumn
    Friend WithEvents colVideoMappedCodec As DataGridViewTextBoxColumn
End Class
