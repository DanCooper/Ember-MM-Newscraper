<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgHost
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
        Me.btnHostPopulateSources = New System.Windows.Forms.Button()
        Me.gbHostDetails = New System.Windows.Forms.GroupBox()
        Me.tblHostDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.btnHostCheck = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblHostPassword = New System.Windows.Forms.Label()
        Me.chkHostRealTimeSync = New System.Windows.Forms.CheckBox()
        Me.lblHostUsername = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblHostWebserverPort = New System.Windows.Forms.Label()
        Me.lblHostIP = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtHostIP = New System.Windows.Forms.TextBox()
        Me.lblHostLabel = New System.Windows.Forms.Label()
        Me.txtLabel = New System.Windows.Forms.TextBox()
        Me.dgvHostSources = New System.Windows.Forms.DataGridView()
        Me.colHostEmberSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colHostSource = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colHostType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.pnlLoading = New System.Windows.Forms.Panel()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.gbHostMoviesetPath = New System.Windows.Forms.GroupBox()
        Me.tblHostMoviesetPath = New System.Windows.Forms.TableLayoutPanel()
        Me.txtHostMoviesetPath = New EmberAPI.FormUtils.TextBox_with_Watermark()
        Me.tblHost = New System.Windows.Forms.TableLayoutPanel()
        Me.gbHostDetails.SuspendLayout()
        Me.tblHostDetails.SuspendLayout()
        CType(Me.dgvHostSources, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLoading.SuspendLayout()
        Me.gbHostMoviesetPath.SuspendLayout()
        Me.tblHostMoviesetPath.SuspendLayout()
        Me.tblHost.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnHostPopulateSources
        '
        Me.btnHostPopulateSources.Location = New System.Drawing.Point(614, 164)
        Me.btnHostPopulateSources.Name = "btnHostPopulateSources"
        Me.btnHostPopulateSources.Size = New System.Drawing.Size(87, 45)
        Me.btnHostPopulateSources.TabIndex = 8
        Me.btnHostPopulateSources.Text = "Populate Sources"
        Me.btnHostPopulateSources.UseVisualStyleBackColor = True
        '
        'gbHostDetails
        '
        Me.gbHostDetails.AutoSize = True
        Me.tblHost.SetColumnSpan(Me.gbHostDetails, 2)
        Me.gbHostDetails.Controls.Add(Me.tblHostDetails)
        Me.gbHostDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbHostDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbHostDetails.Location = New System.Drawing.Point(3, 3)
        Me.gbHostDetails.Name = "gbHostDetails"
        Me.gbHostDetails.Size = New System.Drawing.Size(605, 100)
        Me.gbHostDetails.TabIndex = 5
        Me.gbHostDetails.TabStop = False
        Me.gbHostDetails.Text = "Kodi Host"
        '
        'tblHostDetails
        '
        Me.tblHostDetails.AutoSize = True
        Me.tblHostDetails.ColumnCount = 6
        Me.tblHostDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHostDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHostDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHostDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHostDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHostDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHostDetails.Controls.Add(Me.btnHostCheck, 4, 0)
        Me.tblHostDetails.Controls.Add(Me.txtPassword, 3, 2)
        Me.tblHostDetails.Controls.Add(Me.lblHostPassword, 2, 2)
        Me.tblHostDetails.Controls.Add(Me.chkHostRealTimeSync, 4, 2)
        Me.tblHostDetails.Controls.Add(Me.lblHostUsername, 0, 2)
        Me.tblHostDetails.Controls.Add(Me.txtUsername, 1, 2)
        Me.tblHostDetails.Controls.Add(Me.lblHostWebserverPort, 2, 1)
        Me.tblHostDetails.Controls.Add(Me.lblHostIP, 0, 1)
        Me.tblHostDetails.Controls.Add(Me.txtPort, 3, 1)
        Me.tblHostDetails.Controls.Add(Me.txtHostIP, 1, 1)
        Me.tblHostDetails.Controls.Add(Me.lblHostLabel, 0, 0)
        Me.tblHostDetails.Controls.Add(Me.txtLabel, 1, 0)
        Me.tblHostDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblHostDetails.Location = New System.Drawing.Point(3, 16)
        Me.tblHostDetails.Name = "tblHostDetails"
        Me.tblHostDetails.RowCount = 4
        Me.tblHostDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHostDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHostDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHostDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHostDetails.Size = New System.Drawing.Size(599, 81)
        Me.tblHostDetails.TabIndex = 0
        '
        'btnHostCheck
        '
        Me.btnHostCheck.AutoSize = True
        Me.btnHostCheck.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnHostCheck.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHostCheck.Location = New System.Drawing.Point(363, 3)
        Me.btnHostCheck.Name = "btnHostCheck"
        Me.btnHostCheck.Size = New System.Drawing.Size(189, 23)
        Me.btnHostCheck.TabIndex = 19
        Me.btnHostCheck.Text = "Check Connection"
        Me.btnHostCheck.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(257, 58)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(100, 20)
        Me.txtPassword.TabIndex = 4
        '
        'lblHostPassword
        '
        Me.lblHostPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHostPassword.AutoSize = True
        Me.lblHostPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHostPassword.Location = New System.Drawing.Point(170, 61)
        Me.lblHostPassword.Name = "lblHostPassword"
        Me.lblHostPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblHostPassword.TabIndex = 2
        Me.lblHostPassword.Text = "Password"
        '
        'chkHostRealTimeSync
        '
        Me.chkHostRealTimeSync.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkHostRealTimeSync.AutoSize = True
        Me.chkHostRealTimeSync.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHostRealTimeSync.Location = New System.Drawing.Point(363, 61)
        Me.chkHostRealTimeSync.Name = "chkHostRealTimeSync"
        Me.chkHostRealTimeSync.Size = New System.Drawing.Size(189, 17)
        Me.chkHostRealTimeSync.TabIndex = 5
        Me.chkHostRealTimeSync.Text = "Enable Real Time synchronization "
        Me.chkHostRealTimeSync.UseVisualStyleBackColor = True
        '
        'lblHostUsername
        '
        Me.lblHostUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHostUsername.AutoSize = True
        Me.lblHostUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHostUsername.Location = New System.Drawing.Point(3, 61)
        Me.lblHostUsername.Name = "lblHostUsername"
        Me.lblHostUsername.Size = New System.Drawing.Size(55, 13)
        Me.lblHostUsername.TabIndex = 1
        Me.lblHostUsername.Text = "Username"
        '
        'txtUsername
        '
        Me.txtUsername.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(64, 58)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(100, 20)
        Me.txtUsername.TabIndex = 3
        Me.txtUsername.Text = "kodi"
        '
        'lblHostWebserverPort
        '
        Me.lblHostWebserverPort.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHostWebserverPort.AutoSize = True
        Me.lblHostWebserverPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHostWebserverPort.Location = New System.Drawing.Point(170, 35)
        Me.lblHostWebserverPort.Name = "lblHostWebserverPort"
        Me.lblHostWebserverPort.Size = New System.Drawing.Size(81, 13)
        Me.lblHostWebserverPort.TabIndex = 0
        Me.lblHostWebserverPort.Text = "Webserver Port"
        '
        'lblHostIP
        '
        Me.lblHostIP.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHostIP.AutoSize = True
        Me.lblHostIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHostIP.Location = New System.Drawing.Point(3, 35)
        Me.lblHostIP.Name = "lblHostIP"
        Me.lblHostIP.Size = New System.Drawing.Size(42, 13)
        Me.lblHostIP.TabIndex = 0
        Me.lblHostIP.Text = "Host IP"
        '
        'txtPort
        '
        Me.txtPort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPort.Location = New System.Drawing.Point(257, 32)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(100, 20)
        Me.txtPort.TabIndex = 2
        Me.txtPort.Text = "80"
        '
        'txtHostIP
        '
        Me.txtHostIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtHostIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHostIP.Location = New System.Drawing.Point(64, 32)
        Me.txtHostIP.Name = "txtHostIP"
        Me.txtHostIP.Size = New System.Drawing.Size(100, 20)
        Me.txtHostIP.TabIndex = 1
        Me.txtHostIP.Text = "localhost"
        '
        'lblHostLabel
        '
        Me.lblHostLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHostLabel.AutoSize = True
        Me.lblHostLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHostLabel.Location = New System.Drawing.Point(3, 8)
        Me.lblHostLabel.Name = "lblHostLabel"
        Me.lblHostLabel.Size = New System.Drawing.Size(35, 13)
        Me.lblHostLabel.TabIndex = 0
        Me.lblHostLabel.Text = "Name"
        '
        'txtLabel
        '
        Me.tblHostDetails.SetColumnSpan(Me.txtLabel, 3)
        Me.txtLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLabel.Location = New System.Drawing.Point(64, 3)
        Me.txtLabel.Name = "txtLabel"
        Me.txtLabel.Size = New System.Drawing.Size(293, 20)
        Me.txtLabel.TabIndex = 0
        Me.txtLabel.Text = "My Kodi"
        '
        'dgvHostSources
        '
        Me.dgvHostSources.AllowUserToAddRows = False
        Me.dgvHostSources.AllowUserToDeleteRows = False
        Me.dgvHostSources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHostSources.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colHostEmberSource, Me.colHostSource, Me.colHostType})
        Me.tblHost.SetColumnSpan(Me.dgvHostSources, 2)
        Me.dgvHostSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvHostSources.Enabled = False
        Me.dgvHostSources.Location = New System.Drawing.Point(3, 164)
        Me.dgvHostSources.MultiSelect = False
        Me.dgvHostSources.Name = "dgvHostSources"
        Me.dgvHostSources.RowHeadersVisible = False
        Me.dgvHostSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvHostSources.ShowCellErrors = False
        Me.dgvHostSources.ShowCellToolTips = False
        Me.dgvHostSources.ShowRowErrors = False
        Me.dgvHostSources.Size = New System.Drawing.Size(605, 185)
        Me.dgvHostSources.TabIndex = 14
        '
        'colHostEmberSource
        '
        Me.colHostEmberSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colHostEmberSource.FillWeight = 208.0!
        Me.colHostEmberSource.HeaderText = "Ember Source"
        Me.colHostEmberSource.Name = "colHostEmberSource"
        Me.colHostEmberSource.ReadOnly = True
        Me.colHostEmberSource.Width = 208
        '
        'colHostSource
        '
        Me.colHostSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colHostSource.FillWeight = 270.0!
        Me.colHostSource.HeaderText = "Kodi Source"
        Me.colHostSource.Name = "colHostSource"
        Me.colHostSource.Width = 270
        '
        'colHostType
        '
        Me.colHostType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colHostType.HeaderText = "Type"
        Me.colHostType.Name = "colHostType"
        '
        'pnlLoading
        '
        Me.pnlLoading.BackColor = System.Drawing.Color.White
        Me.pnlLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLoading.Controls.Add(Me.lblCompiling)
        Me.pnlLoading.Controls.Add(Me.ProgressBar1)
        Me.pnlLoading.Location = New System.Drawing.Point(178, 212)
        Me.pnlLoading.Name = "pnlLoading"
        Me.pnlLoading.Size = New System.Drawing.Size(200, 54)
        Me.pnlLoading.TabIndex = 15
        Me.pnlLoading.Visible = False
        '
        'lblCompiling
        '
        Me.lblCompiling.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompiling.Location = New System.Drawing.Point(3, 5)
        Me.lblCompiling.Name = "lblCompiling"
        Me.lblCompiling.Size = New System.Drawing.Size(192, 15)
        Me.lblCompiling.TabIndex = 0
        Me.lblCompiling.Text = "Searching ..."
        Me.lblCompiling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(3, 32)
        Me.ProgressBar1.MarqueeAnimationSpeed = 25
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(192, 17)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(533, 355)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(614, 355)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'gbHostMoviesetPath
        '
        Me.gbHostMoviesetPath.AutoSize = True
        Me.tblHost.SetColumnSpan(Me.gbHostMoviesetPath, 2)
        Me.gbHostMoviesetPath.Controls.Add(Me.tblHostMoviesetPath)
        Me.gbHostMoviesetPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbHostMoviesetPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbHostMoviesetPath.Location = New System.Drawing.Point(3, 109)
        Me.gbHostMoviesetPath.Name = "gbHostMoviesetPath"
        Me.gbHostMoviesetPath.Size = New System.Drawing.Size(605, 49)
        Me.gbHostMoviesetPath.TabIndex = 90
        Me.gbHostMoviesetPath.TabStop = False
        Me.gbHostMoviesetPath.Text = "Kodi MovieSet Artwork Folder"
        '
        'tblHostMoviesetPath
        '
        Me.tblHostMoviesetPath.AutoSize = True
        Me.tblHostMoviesetPath.ColumnCount = 1
        Me.tblHostMoviesetPath.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHostMoviesetPath.Controls.Add(Me.txtHostMoviesetPath, 0, 0)
        Me.tblHostMoviesetPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblHostMoviesetPath.Location = New System.Drawing.Point(3, 18)
        Me.tblHostMoviesetPath.Name = "tblHostMoviesetPath"
        Me.tblHostMoviesetPath.RowCount = 1
        Me.tblHostMoviesetPath.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHostMoviesetPath.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.tblHostMoviesetPath.Size = New System.Drawing.Size(599, 28)
        Me.tblHostMoviesetPath.TabIndex = 2
        '
        'txtHostMoviesetPath
        '
        Me.txtHostMoviesetPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtHostMoviesetPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHostMoviesetPath.Location = New System.Drawing.Point(3, 3)
        Me.txtHostMoviesetPath.Name = "txtHostMoviesetPath"
        Me.txtHostMoviesetPath.Size = New System.Drawing.Size(593, 22)
        Me.txtHostMoviesetPath.TabIndex = 2
        Me.txtHostMoviesetPath.WatermarkColor = System.Drawing.Color.Gray
        Me.txtHostMoviesetPath.WatermarkText = "smb://SERVER/SHARE"
        '
        'tblHost
        '
        Me.tblHost.AutoSize = True
        Me.tblHost.ColumnCount = 3
        Me.tblHost.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblHost.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHost.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHost.Controls.Add(Me.gbHostDetails, 0, 0)
        Me.tblHost.Controls.Add(Me.gbHostMoviesetPath, 0, 1)
        Me.tblHost.Controls.Add(Me.dgvHostSources, 0, 2)
        Me.tblHost.Controls.Add(Me.btnOK, 1, 3)
        Me.tblHost.Controls.Add(Me.btnCancel, 2, 3)
        Me.tblHost.Controls.Add(Me.btnHostPopulateSources, 2, 2)
        Me.tblHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblHost.Location = New System.Drawing.Point(0, 0)
        Me.tblHost.Name = "tblHost"
        Me.tblHost.RowCount = 4
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHost.Size = New System.Drawing.Size(704, 381)
        Me.tblHost.TabIndex = 91
        '
        'dlgHost
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(704, 381)
        Me.Controls.Add(Me.pnlLoading)
        Me.Controls.Add(Me.tblHost)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgHost"
        Me.ShowInTaskbar = False
        Me.Text = "Kodi Interface"
        Me.gbHostDetails.ResumeLayout(False)
        Me.gbHostDetails.PerformLayout()
        Me.tblHostDetails.ResumeLayout(False)
        Me.tblHostDetails.PerformLayout()
        CType(Me.dgvHostSources, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLoading.ResumeLayout(False)
        Me.gbHostMoviesetPath.ResumeLayout(False)
        Me.gbHostMoviesetPath.PerformLayout()
        Me.tblHostMoviesetPath.ResumeLayout(False)
        Me.tblHostMoviesetPath.PerformLayout()
        Me.tblHost.ResumeLayout(False)
        Me.tblHost.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnHostPopulateSources As System.Windows.Forms.Button
    Friend WithEvents gbHostDetails As System.Windows.Forms.GroupBox
    Friend WithEvents tblHostDetails As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblHostPassword As System.Windows.Forms.Label
    Friend WithEvents lblHostUsername As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblHostWebserverPort As System.Windows.Forms.Label
    Friend WithEvents lblHostIP As System.Windows.Forms.Label
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents txtHostIP As System.Windows.Forms.TextBox
    Friend WithEvents dgvHostSources As System.Windows.Forms.DataGridView
    Friend WithEvents pnlLoading As System.Windows.Forms.Panel
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblHostLabel As System.Windows.Forms.Label
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents chkHostRealTimeSync As System.Windows.Forms.CheckBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnHostCheck As System.Windows.Forms.Button
    Friend WithEvents gbHostMoviesetPath As System.Windows.Forms.GroupBox
    Friend WithEvents tblHost As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblHostMoviesetPath As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents colHostEmberSource As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colHostSource As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents colHostType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents txtHostMoviesetPath As EmberAPI.FormUtils.TextBox_with_Watermark
End Class
