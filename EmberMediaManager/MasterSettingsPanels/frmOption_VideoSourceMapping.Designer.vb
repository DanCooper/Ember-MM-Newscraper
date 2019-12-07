<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOption_VideoSourceMapping
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOption_VideoSourceMapping))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbByExtension = New System.Windows.Forms.GroupBox()
        Me.tblByExtension = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvByExtension = New System.Windows.Forms.DataGridView()
        Me.colByExtensionFileExtension = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colByExtensionVideoSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkByExtensionEnabled = New System.Windows.Forms.CheckBox()
        Me.gbByRegex = New System.Windows.Forms.GroupBox()
        Me.tblByRegex = New System.Windows.Forms.TableLayoutPanel()
        Me.chkRegexEnabled = New System.Windows.Forms.CheckBox()
        Me.dgvByRegex = New System.Windows.Forms.DataGridView()
        Me.colByRegexRegex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colByRegexVideoSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnByRegexDefaults = New System.Windows.Forms.Button()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbByExtension.SuspendLayout()
        Me.tblByExtension.SuspendLayout()
        CType(Me.dgvByExtension, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbByRegex.SuspendLayout()
        Me.tblByRegex.SuspendLayout()
        CType(Me.dgvByRegex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(606, 262)
        Me.pnlSettings.TabIndex = 20
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSettings.Controls.Add(Me.gbByExtension, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbByRegex, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 1
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(606, 262)
        Me.tblSettings.TabIndex = 1
        '
        'gbByExtension
        '
        Me.gbByExtension.AutoSize = True
        Me.gbByExtension.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbByExtension.Controls.Add(Me.tblByExtension)
        Me.gbByExtension.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbByExtension.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbByExtension.Location = New System.Drawing.Point(306, 3)
        Me.gbByExtension.Name = "gbByExtension"
        Me.gbByExtension.Size = New System.Drawing.Size(297, 256)
        Me.gbByExtension.TabIndex = 7
        Me.gbByExtension.TabStop = False
        Me.gbByExtension.Text = "Mapping by File Extension"
        '
        'tblByExtension
        '
        Me.tblByExtension.AutoSize = True
        Me.tblByExtension.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblByExtension.ColumnCount = 1
        Me.tblByExtension.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblByExtension.Controls.Add(Me.dgvByExtension, 0, 1)
        Me.tblByExtension.Controls.Add(Me.chkByExtensionEnabled, 0, 0)
        Me.tblByExtension.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblByExtension.Location = New System.Drawing.Point(3, 18)
        Me.tblByExtension.Name = "tblByExtension"
        Me.tblByExtension.RowCount = 2
        Me.tblByExtension.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblByExtension.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblByExtension.Size = New System.Drawing.Size(291, 235)
        Me.tblByExtension.TabIndex = 0
        '
        'dgvByExtension
        '
        Me.dgvByExtension.AllowUserToResizeRows = False
        Me.dgvByExtension.BackgroundColor = System.Drawing.Color.White
        Me.dgvByExtension.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvByExtension.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvByExtension.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colByExtensionFileExtension, Me.colByExtensionVideoSource})
        Me.dgvByExtension.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvByExtension.Location = New System.Drawing.Point(3, 26)
        Me.dgvByExtension.Name = "dgvByExtension"
        Me.dgvByExtension.RowHeadersWidth = 25
        Me.dgvByExtension.ShowCellErrors = False
        Me.dgvByExtension.ShowCellToolTips = False
        Me.dgvByExtension.ShowRowErrors = False
        Me.dgvByExtension.Size = New System.Drawing.Size(285, 206)
        Me.dgvByExtension.TabIndex = 6
        '
        'colByExtensionFileExtension
        '
        Me.colByExtensionFileExtension.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colByExtensionFileExtension.HeaderText = "File Extension"
        Me.colByExtensionFileExtension.Name = "colByExtensionFileExtension"
        '
        'colByExtensionVideoSource
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colByExtensionVideoSource.DefaultCellStyle = DataGridViewCellStyle1
        Me.colByExtensionVideoSource.HeaderText = "VideoSource"
        Me.colByExtensionVideoSource.Name = "colByExtensionVideoSource"
        Me.colByExtensionVideoSource.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colByExtensionVideoSource.Width = 150
        '
        'chkByExtensionEnabled
        '
        Me.chkByExtensionEnabled.AutoSize = True
        Me.chkByExtensionEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkByExtensionEnabled.Location = New System.Drawing.Point(3, 3)
        Me.chkByExtensionEnabled.Name = "chkByExtensionEnabled"
        Me.chkByExtensionEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkByExtensionEnabled.TabIndex = 5
        Me.chkByExtensionEnabled.Text = "Enabled"
        Me.chkByExtensionEnabled.UseVisualStyleBackColor = True
        '
        'gbByRegex
        '
        Me.gbByRegex.AutoSize = True
        Me.gbByRegex.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbByRegex.Controls.Add(Me.tblByRegex)
        Me.gbByRegex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbByRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbByRegex.Location = New System.Drawing.Point(3, 3)
        Me.gbByRegex.Name = "gbByRegex"
        Me.gbByRegex.Size = New System.Drawing.Size(297, 256)
        Me.gbByRegex.TabIndex = 7
        Me.gbByRegex.TabStop = False
        Me.gbByRegex.Text = "Mapping by Regex"
        '
        'tblByRegex
        '
        Me.tblByRegex.AutoSize = True
        Me.tblByRegex.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblByRegex.ColumnCount = 1
        Me.tblByRegex.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblByRegex.Controls.Add(Me.chkRegexEnabled, 0, 0)
        Me.tblByRegex.Controls.Add(Me.dgvByRegex, 0, 1)
        Me.tblByRegex.Controls.Add(Me.btnByRegexDefaults, 0, 2)
        Me.tblByRegex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblByRegex.Location = New System.Drawing.Point(3, 18)
        Me.tblByRegex.Name = "tblByRegex"
        Me.tblByRegex.RowCount = 3
        Me.tblByRegex.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblByRegex.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblByRegex.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblByRegex.Size = New System.Drawing.Size(291, 235)
        Me.tblByRegex.TabIndex = 0
        '
        'chkRegexEnabled
        '
        Me.chkRegexEnabled.AutoSize = True
        Me.chkRegexEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRegexEnabled.Location = New System.Drawing.Point(3, 3)
        Me.chkRegexEnabled.Name = "chkRegexEnabled"
        Me.chkRegexEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkRegexEnabled.TabIndex = 5
        Me.chkRegexEnabled.Text = "Enabled"
        Me.chkRegexEnabled.UseVisualStyleBackColor = True
        '
        'dgvByRegex
        '
        Me.dgvByRegex.AllowUserToResizeRows = False
        Me.dgvByRegex.BackgroundColor = System.Drawing.Color.White
        Me.dgvByRegex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvByRegex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvByRegex.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colByRegexRegex, Me.colByRegexVideoSource})
        Me.dgvByRegex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvByRegex.Location = New System.Drawing.Point(3, 26)
        Me.dgvByRegex.Name = "dgvByRegex"
        Me.dgvByRegex.RowHeadersWidth = 25
        Me.dgvByRegex.ShowCellErrors = False
        Me.dgvByRegex.ShowCellToolTips = False
        Me.dgvByRegex.ShowRowErrors = False
        Me.dgvByRegex.Size = New System.Drawing.Size(285, 177)
        Me.dgvByRegex.TabIndex = 1
        '
        'colByRegexRegex
        '
        Me.colByRegexRegex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colByRegexRegex.HeaderText = "Regex"
        Me.colByRegexRegex.Name = "colByRegexRegex"
        '
        'colByRegexVideoSource
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colByRegexVideoSource.DefaultCellStyle = DataGridViewCellStyle2
        Me.colByRegexVideoSource.HeaderText = "VideoSource"
        Me.colByRegexVideoSource.Name = "colByRegexVideoSource"
        Me.colByRegexVideoSource.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colByRegexVideoSource.Width = 150
        '
        'btnByRegexDefaults
        '
        Me.btnByRegexDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnByRegexDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnByRegexDefaults.Image = CType(resources.GetObject("btnByRegexDefaults.Image"), System.Drawing.Image)
        Me.btnByRegexDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnByRegexDefaults.Location = New System.Drawing.Point(183, 209)
        Me.btnByRegexDefaults.Name = "btnByRegexDefaults"
        Me.btnByRegexDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnByRegexDefaults.TabIndex = 2
        Me.btnByRegexDefaults.Text = "Defaults"
        Me.btnByRegexDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnByRegexDefaults.UseVisualStyleBackColor = True
        '
        'frmOption_VideoSourceMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 262)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmOption_VideoSourceMapping"
        Me.Text = "frmOption_VideoSourceMapping"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbByExtension.ResumeLayout(False)
        Me.gbByExtension.PerformLayout()
        Me.tblByExtension.ResumeLayout(False)
        Me.tblByExtension.PerformLayout()
        CType(Me.dgvByExtension, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbByRegex.ResumeLayout(False)
        Me.gbByRegex.PerformLayout()
        Me.tblByRegex.ResumeLayout(False)
        Me.tblByRegex.PerformLayout()
        CType(Me.dgvByRegex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbByExtension As GroupBox
    Friend WithEvents tblByExtension As TableLayoutPanel
    Friend WithEvents dgvByExtension As DataGridView
    Friend WithEvents colByExtensionFileExtension As DataGridViewTextBoxColumn
    Friend WithEvents colByExtensionVideoSource As DataGridViewTextBoxColumn
    Friend WithEvents chkByExtensionEnabled As CheckBox
    Friend WithEvents gbByRegex As GroupBox
    Friend WithEvents tblByRegex As TableLayoutPanel
    Friend WithEvents btnByRegexDefaults As Button
    Friend WithEvents chkRegexEnabled As CheckBox
    Friend WithEvents dgvByRegex As DataGridView
    Friend WithEvents colByRegexRegex As DataGridViewTextBoxColumn
    Friend WithEvents colByRegexVideoSource As DataGridViewTextBoxColumn
End Class
