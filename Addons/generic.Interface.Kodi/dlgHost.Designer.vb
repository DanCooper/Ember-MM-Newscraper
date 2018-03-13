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
        Me.btnHostConnectionCheck = New System.Windows.Forms.Button()
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
        Me.colHostRemoteSource = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colHostContentType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.prgLoading = New System.Windows.Forms.ProgressBar()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.gbHostMoviesetPath = New System.Windows.Forms.GroupBox()
        Me.tblHostMoviesetPath = New System.Windows.Forms.TableLayoutPanel()
        Me.txtHostMoviesetPath = New EmberAPI.FormUtils.TextBox_with_Watermark()
        Me.tblHost = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCustomRemotePath = New System.Windows.Forms.Label()
        Me.txtCustomRemotePath = New System.Windows.Forms.TextBox()
        Me.btnCustomRemotePath = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.gbHostDetails.SuspendLayout()
        Me.tblHostDetails.SuspendLayout()
        CType(Me.dgvHostSources, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbHostMoviesetPath.SuspendLayout()
        Me.tblHostMoviesetPath.SuspendLayout()
        Me.tblHost.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnHostPopulateSources
        '
        Me.btnHostPopulateSources.Location = New System.Drawing.Point(619, 164)
        Me.btnHostPopulateSources.Name = "btnHostPopulateSources"
        Me.btnHostPopulateSources.Size = New System.Drawing.Size(87, 45)
        Me.btnHostPopulateSources.TabIndex = 8
        Me.btnHostPopulateSources.Text = "Read Sources from Kodi"
        Me.btnHostPopulateSources.UseVisualStyleBackColor = True
        '
        'gbHostDetails
        '
        Me.gbHostDetails.AutoSize = True
        Me.tblHost.SetColumnSpan(Me.gbHostDetails, 3)
        Me.gbHostDetails.Controls.Add(Me.tblHostDetails)
        Me.gbHostDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbHostDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbHostDetails.Location = New System.Drawing.Point(3, 3)
        Me.gbHostDetails.Name = "gbHostDetails"
        Me.gbHostDetails.Size = New System.Drawing.Size(610, 100)
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
        Me.tblHostDetails.Controls.Add(Me.btnHostConnectionCheck, 4, 0)
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
        Me.tblHostDetails.Size = New System.Drawing.Size(604, 81)
        Me.tblHostDetails.TabIndex = 0
        '
        'btnHostConnectionCheck
        '
        Me.btnHostConnectionCheck.AutoSize = True
        Me.btnHostConnectionCheck.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnHostConnectionCheck.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHostConnectionCheck.Location = New System.Drawing.Point(363, 3)
        Me.btnHostConnectionCheck.Name = "btnHostConnectionCheck"
        Me.btnHostConnectionCheck.Size = New System.Drawing.Size(189, 23)
        Me.btnHostConnectionCheck.TabIndex = 19
        Me.btnHostConnectionCheck.Text = "Check Connection"
        Me.btnHostConnectionCheck.UseVisualStyleBackColor = True
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
        Me.dgvHostSources.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colHostEmberSource, Me.colHostRemoteSource, Me.colHostContentType})
        Me.tblHost.SetColumnSpan(Me.dgvHostSources, 3)
        Me.dgvHostSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvHostSources.Enabled = False
        Me.dgvHostSources.Location = New System.Drawing.Point(3, 164)
        Me.dgvHostSources.MultiSelect = False
        Me.dgvHostSources.Name = "dgvHostSources"
        Me.dgvHostSources.RowHeadersVisible = False
        Me.tblHost.SetRowSpan(Me.dgvHostSources, 2)
        Me.dgvHostSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvHostSources.ShowCellErrors = False
        Me.dgvHostSources.ShowCellToolTips = False
        Me.dgvHostSources.ShowRowErrors = False
        Me.dgvHostSources.Size = New System.Drawing.Size(610, 145)
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
        'colHostRemoteSource
        '
        Me.colHostRemoteSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colHostRemoteSource.FillWeight = 270.0!
        Me.colHostRemoteSource.HeaderText = "Kodi Source"
        Me.colHostRemoteSource.Name = "colHostRemoteSource"
        Me.colHostRemoteSource.Width = 270
        '
        'colHostContentType
        '
        Me.colHostContentType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colHostContentType.HeaderText = "Type"
        Me.colHostContentType.Name = "colHostContentType"
        '
        'prgLoading
        '
        Me.prgLoading.Location = New System.Drawing.Point(619, 215)
        Me.prgLoading.MarqueeAnimationSpeed = 25
        Me.prgLoading.Name = "prgLoading"
        Me.prgLoading.Size = New System.Drawing.Size(87, 14)
        Me.prgLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prgLoading.TabIndex = 1
        Me.prgLoading.Visible = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(538, 364)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(619, 364)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'gbHostMoviesetPath
        '
        Me.gbHostMoviesetPath.AutoSize = True
        Me.tblHost.SetColumnSpan(Me.gbHostMoviesetPath, 3)
        Me.gbHostMoviesetPath.Controls.Add(Me.tblHostMoviesetPath)
        Me.gbHostMoviesetPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbHostMoviesetPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbHostMoviesetPath.Location = New System.Drawing.Point(3, 109)
        Me.gbHostMoviesetPath.Name = "gbHostMoviesetPath"
        Me.gbHostMoviesetPath.Size = New System.Drawing.Size(610, 49)
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
        Me.tblHostMoviesetPath.Size = New System.Drawing.Size(604, 28)
        Me.tblHostMoviesetPath.TabIndex = 2
        '
        'txtHostMoviesetPath
        '
        Me.txtHostMoviesetPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtHostMoviesetPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHostMoviesetPath.Location = New System.Drawing.Point(3, 3)
        Me.txtHostMoviesetPath.Name = "txtHostMoviesetPath"
        Me.txtHostMoviesetPath.Size = New System.Drawing.Size(598, 22)
        Me.txtHostMoviesetPath.TabIndex = 2
        Me.txtHostMoviesetPath.WatermarkColor = System.Drawing.Color.Gray
        Me.txtHostMoviesetPath.WatermarkText = "smb://SERVER/SHARE"
        '
        'tblHost
        '
        Me.tblHost.AutoSize = True
        Me.tblHost.ColumnCount = 4
        Me.tblHost.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHost.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblHost.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHost.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblHost.Controls.Add(Me.gbHostDetails, 0, 0)
        Me.tblHost.Controls.Add(Me.prgLoading, 3, 3)
        Me.tblHost.Controls.Add(Me.gbHostMoviesetPath, 0, 1)
        Me.tblHost.Controls.Add(Me.dgvHostSources, 0, 2)
        Me.tblHost.Controls.Add(Me.btnOK, 2, 6)
        Me.tblHost.Controls.Add(Me.btnCancel, 3, 6)
        Me.tblHost.Controls.Add(Me.btnHostPopulateSources, 3, 2)
        Me.tblHost.Controls.Add(Me.lblCustomRemotePath, 0, 4)
        Me.tblHost.Controls.Add(Me.txtCustomRemotePath, 1, 4)
        Me.tblHost.Controls.Add(Me.btnCustomRemotePath, 2, 4)
        Me.tblHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblHost.Location = New System.Drawing.Point(0, 0)
        Me.tblHost.Name = "tblHost"
        Me.tblHost.RowCount = 7
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblHost.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblHost.Size = New System.Drawing.Size(709, 390)
        Me.tblHost.TabIndex = 91
        '
        'lblCustomRemotePath
        '
        Me.lblCustomRemotePath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomRemotePath.AutoSize = True
        Me.lblCustomRemotePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomRemotePath.Location = New System.Drawing.Point(3, 320)
        Me.lblCustomRemotePath.Name = "lblCustomRemotePath"
        Me.lblCustomRemotePath.Size = New System.Drawing.Size(140, 13)
        Me.lblCustomRemotePath.TabIndex = 91
        Me.lblCustomRemotePath.Text = "Add Custom Kodi Source:"
        '
        'txtCustomRemotePath
        '
        Me.txtCustomRemotePath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomRemotePath.Location = New System.Drawing.Point(149, 315)
        Me.txtCustomRemotePath.Name = "txtCustomRemotePath"
        Me.txtCustomRemotePath.Size = New System.Drawing.Size(383, 22)
        Me.txtCustomRemotePath.TabIndex = 92
        '
        'btnCustomRemotePath
        '
        Me.btnCustomRemotePath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCustomRemotePath.AutoSize = True
        Me.btnCustomRemotePath.Enabled = False
        Me.btnCustomRemotePath.Location = New System.Drawing.Point(538, 315)
        Me.btnCustomRemotePath.Name = "btnCustomRemotePath"
        Me.btnCustomRemotePath.Size = New System.Drawing.Size(75, 23)
        Me.btnCustomRemotePath.TabIndex = 93
        Me.btnCustomRemotePath.Text = "Add"
        Me.btnCustomRemotePath.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 390)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(709, 22)
        Me.StatusStrip1.TabIndex = 92
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'dlgHost
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(709, 412)
        Me.Controls.Add(Me.tblHost)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MinimizeBox = False
        Me.Name = "dlgHost"
        Me.ShowInTaskbar = False
        Me.Text = "Kodi Interface"
        Me.gbHostDetails.ResumeLayout(False)
        Me.gbHostDetails.PerformLayout()
        Me.tblHostDetails.ResumeLayout(False)
        Me.tblHostDetails.PerformLayout()
        CType(Me.dgvHostSources, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents prgLoading As System.Windows.Forms.ProgressBar
    Friend WithEvents lblHostLabel As System.Windows.Forms.Label
    Friend WithEvents txtLabel As System.Windows.Forms.TextBox
    Friend WithEvents chkHostRealTimeSync As System.Windows.Forms.CheckBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnHostConnectionCheck As System.Windows.Forms.Button
    Friend WithEvents gbHostMoviesetPath As System.Windows.Forms.GroupBox
    Friend WithEvents tblHost As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblHostMoviesetPath As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtHostMoviesetPath As EmberAPI.FormUtils.TextBox_with_Watermark
    Friend WithEvents colHostEmberSource As DataGridViewTextBoxColumn
    Friend WithEvents colHostRemoteSource As DataGridViewComboBoxColumn
    Friend WithEvents colHostContentType As DataGridViewComboBoxColumn
    Friend WithEvents lblCustomRemotePath As Label
    Friend WithEvents txtCustomRemotePath As TextBox
    Friend WithEvents btnCustomRemotePath As Button
    Friend WithEvents StatusStrip1 As StatusStrip
End Class
