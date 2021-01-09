<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgMediaFileSelect
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgMediaFileSelect))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnScrape = New System.Windows.Forms.Button()
        Me.gbYouTubeSearch = New System.Windows.Forms.GroupBox()
        Me.tblYouTubeSearch = New System.Windows.Forms.TableLayoutPanel()
        Me.txtYouTubeSearch = New System.Windows.Forms.TextBox()
        Me.btnYouTubeSearch = New System.Windows.Forms.Button()
        Me.gbCustom = New System.Windows.Forms.GroupBox()
        Me.tblCustom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCustomLocalFile_Browse = New System.Windows.Forms.Button()
        Me.btnCustomURL_Remove = New System.Windows.Forms.Button()
        Me.txtCustomLocalFile = New System.Windows.Forms.TextBox()
        Me.lblCustomURL = New System.Windows.Forms.Label()
        Me.lblCustomLocalFile = New System.Windows.Forms.Label()
        Me.txtCustomURL = New System.Windows.Forms.TextBox()
        Me.ofdFile = New System.Windows.Forms.OpenFileDialog()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvMediaFiles = New System.Windows.Forms.DataGridView()
        Me.btnOpenInBrowser = New System.Windows.Forms.Button()
        Me.pblBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.pbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.colMediaFileTitel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaFileUrlWebsite = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaFileLanguage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaFileDuration = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaFileVariant = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.colMediaFileVideoType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaFileSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaFileAddon = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbYouTubeSearch.SuspendLayout()
        Me.tblYouTubeSearch.SuspendLayout()
        Me.gbCustom.SuspendLayout()
        Me.tblCustom.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        CType(Me.dgvMediaFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pblBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Enabled = False
        Me.btnOK.Location = New System.Drawing.Point(735, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(120, 23)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "Download"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(861, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        '
        'btnScrape
        '
        Me.btnScrape.AutoSize = True
        Me.btnScrape.Location = New System.Drawing.Point(3, 363)
        Me.btnScrape.Name = "btnScrape"
        Me.btnScrape.Size = New System.Drawing.Size(152, 23)
        Me.btnScrape.TabIndex = 5
        Me.btnScrape.Text = "Scrape"
        Me.btnScrape.UseVisualStyleBackColor = True
        '
        'gbYouTubeSearch
        '
        Me.gbYouTubeSearch.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.gbYouTubeSearch, 2)
        Me.gbYouTubeSearch.Controls.Add(Me.tblYouTubeSearch)
        Me.gbYouTubeSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbYouTubeSearch.Location = New System.Drawing.Point(3, 412)
        Me.gbYouTubeSearch.Name = "gbYouTubeSearch"
        Me.gbYouTubeSearch.Size = New System.Drawing.Size(978, 50)
        Me.gbYouTubeSearch.TabIndex = 9
        Me.gbYouTubeSearch.TabStop = False
        Me.gbYouTubeSearch.Text = "Search On YouTube"
        '
        'tblYouTubeSearch
        '
        Me.tblYouTubeSearch.AutoSize = True
        Me.tblYouTubeSearch.ColumnCount = 2
        Me.tblYouTubeSearch.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblYouTubeSearch.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblYouTubeSearch.Controls.Add(Me.txtYouTubeSearch, 0, 0)
        Me.tblYouTubeSearch.Controls.Add(Me.btnYouTubeSearch, 1, 0)
        Me.tblYouTubeSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblYouTubeSearch.Location = New System.Drawing.Point(3, 18)
        Me.tblYouTubeSearch.Name = "tblYouTubeSearch"
        Me.tblYouTubeSearch.RowCount = 1
        Me.tblYouTubeSearch.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblYouTubeSearch.Size = New System.Drawing.Size(972, 29)
        Me.tblYouTubeSearch.TabIndex = 2
        '
        'txtYouTubeSearch
        '
        Me.txtYouTubeSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtYouTubeSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYouTubeSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtYouTubeSearch.Name = "txtYouTubeSearch"
        Me.txtYouTubeSearch.Size = New System.Drawing.Size(885, 22)
        Me.txtYouTubeSearch.TabIndex = 0
        '
        'btnYouTubeSearch
        '
        Me.btnYouTubeSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnYouTubeSearch.Location = New System.Drawing.Point(894, 3)
        Me.btnYouTubeSearch.Name = "btnYouTubeSearch"
        Me.btnYouTubeSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnYouTubeSearch.TabIndex = 1
        Me.btnYouTubeSearch.Text = "Search"
        Me.btnYouTubeSearch.UseVisualStyleBackColor = True
        '
        'gbCustom
        '
        Me.gbCustom.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.gbCustom, 2)
        Me.gbCustom.Controls.Add(Me.tblCustom)
        Me.gbCustom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCustom.Location = New System.Drawing.Point(3, 488)
        Me.gbCustom.Name = "gbCustom"
        Me.gbCustom.Size = New System.Drawing.Size(978, 119)
        Me.gbCustom.TabIndex = 3
        Me.gbCustom.TabStop = False
        Me.gbCustom.Text = "Custom"
        '
        'tblCustom
        '
        Me.tblCustom.AutoSize = True
        Me.tblCustom.ColumnCount = 2
        Me.tblCustom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblCustom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustom.Controls.Add(Me.btnCustomLocalFile_Browse, 1, 3)
        Me.tblCustom.Controls.Add(Me.btnCustomURL_Remove, 1, 1)
        Me.tblCustom.Controls.Add(Me.txtCustomLocalFile, 0, 3)
        Me.tblCustom.Controls.Add(Me.lblCustomURL, 0, 0)
        Me.tblCustom.Controls.Add(Me.lblCustomLocalFile, 0, 2)
        Me.tblCustom.Controls.Add(Me.txtCustomURL, 0, 1)
        Me.tblCustom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCustom.Location = New System.Drawing.Point(3, 18)
        Me.tblCustom.Name = "tblCustom"
        Me.tblCustom.RowCount = 4
        Me.tblCustom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCustom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblCustom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustom.Size = New System.Drawing.Size(972, 98)
        Me.tblCustom.TabIndex = 5
        '
        'btnCustomLocalFile_Browse
        '
        Me.btnCustomLocalFile_Browse.Location = New System.Drawing.Point(944, 72)
        Me.btnCustomLocalFile_Browse.Name = "btnCustomLocalFile_Browse"
        Me.btnCustomLocalFile_Browse.Size = New System.Drawing.Size(25, 23)
        Me.btnCustomLocalFile_Browse.TabIndex = 4
        Me.btnCustomLocalFile_Browse.Text = "..."
        Me.btnCustomLocalFile_Browse.UseVisualStyleBackColor = True
        '
        'btnCustomURL_Remove
        '
        Me.btnCustomURL_Remove.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.btnCustomURL_Remove.Location = New System.Drawing.Point(944, 23)
        Me.btnCustomURL_Remove.Name = "btnCustomURL_Remove"
        Me.btnCustomURL_Remove.Size = New System.Drawing.Size(25, 23)
        Me.btnCustomURL_Remove.TabIndex = 5
        Me.btnCustomURL_Remove.UseVisualStyleBackColor = True
        '
        'txtCustomLocalFile
        '
        Me.txtCustomLocalFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomLocalFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomLocalFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomLocalFile.Location = New System.Drawing.Point(3, 72)
        Me.txtCustomLocalFile.Name = "txtCustomLocalFile"
        Me.txtCustomLocalFile.Size = New System.Drawing.Size(935, 22)
        Me.txtCustomLocalFile.TabIndex = 3
        '
        'lblCustomURL
        '
        Me.lblCustomURL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomURL.AutoSize = True
        Me.lblCustomURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomURL.Location = New System.Drawing.Point(3, 3)
        Me.lblCustomURL.Name = "lblCustomURL"
        Me.lblCustomURL.Size = New System.Drawing.Size(148, 13)
        Me.lblCustomURL.TabIndex = 0
        Me.lblCustomURL.Text = "Direct Link or YouTube URL:"
        '
        'lblCustomLocalFile
        '
        Me.lblCustomLocalFile.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomLocalFile.AutoSize = True
        Me.lblCustomLocalFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomLocalFile.Location = New System.Drawing.Point(3, 52)
        Me.lblCustomLocalFile.Name = "lblCustomLocalFile"
        Me.lblCustomLocalFile.Size = New System.Drawing.Size(57, 13)
        Me.lblCustomLocalFile.TabIndex = 2
        Me.lblCustomLocalFile.Text = "Local File:"
        '
        'txtCustomURL
        '
        Me.txtCustomURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomURL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomURL.Location = New System.Drawing.Point(3, 23)
        Me.txtCustomURL.Name = "txtCustomURL"
        Me.txtCustomURL.Size = New System.Drawing.Size(935, 22)
        Me.txtCustomURL.TabIndex = 1
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(984, 610)
        Me.pnlMain.TabIndex = 2
        '
        'tblMain
        '
        Me.tblMain.AutoScroll = True
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 2
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.gbYouTubeSearch, 0, 3)
        Me.tblMain.Controls.Add(Me.gbCustom, 0, 6)
        Me.tblMain.Controls.Add(Me.btnScrape, 0, 1)
        Me.tblMain.Controls.Add(Me.dgvMediaFiles, 0, 0)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 7
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(984, 610)
        Me.tblMain.TabIndex = 4
        '
        'dgvMediaFiles
        '
        Me.dgvMediaFiles.AllowUserToAddRows = False
        Me.dgvMediaFiles.AllowUserToDeleteRows = False
        Me.dgvMediaFiles.AllowUserToResizeRows = False
        Me.dgvMediaFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMediaFiles.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMediaFileTitel, Me.colMediaFileUrlWebsite, Me.colMediaFileLanguage, Me.colMediaFileDuration, Me.colMediaFileVariant, Me.colMediaFileVideoType, Me.colMediaFileSource, Me.colMediaFileAddon})
        Me.tblMain.SetColumnSpan(Me.dgvMediaFiles, 2)
        Me.dgvMediaFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMediaFiles.Location = New System.Drawing.Point(3, 3)
        Me.dgvMediaFiles.MultiSelect = False
        Me.dgvMediaFiles.Name = "dgvMediaFiles"
        Me.dgvMediaFiles.RowHeadersVisible = False
        Me.dgvMediaFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMediaFiles.Size = New System.Drawing.Size(978, 354)
        Me.dgvMediaFiles.TabIndex = 10
        '
        'btnOpenInBrowser
        '
        Me.btnOpenInBrowser.Enabled = False
        Me.btnOpenInBrowser.Location = New System.Drawing.Point(3, 3)
        Me.btnOpenInBrowser.Name = "btnOpenInBrowser"
        Me.btnOpenInBrowser.Size = New System.Drawing.Size(120, 23)
        Me.btnOpenInBrowser.TabIndex = 4
        Me.btnOpenInBrowser.Text = "Open In Browser"
        Me.btnOpenInBrowser.UseVisualStyleBackColor = True
        '
        'pblBottom
        '
        Me.pblBottom.AutoSize = True
        Me.pblBottom.Controls.Add(Me.tblBottom)
        Me.pblBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pblBottom.Location = New System.Drawing.Point(0, 610)
        Me.pblBottom.Name = "pblBottom"
        Me.pblBottom.Size = New System.Drawing.Size(984, 29)
        Me.pblBottom.TabIndex = 8
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 4
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnOpenInBrowser, 0, 0)
        Me.tblBottom.Controls.Add(Me.btnOK, 2, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 3, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.tblBottom.Size = New System.Drawing.Size(984, 29)
        Me.tblBottom.TabIndex = 0
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pbStatus, Me.lblStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 639)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(984, 22)
        Me.StatusStrip.TabIndex = 9
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'pbStatus
        '
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(100, 16)
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(90, 17)
        Me.lblStatus.Text = "Compiling list..."
        '
        'colMediaFileTitel
        '
        Me.colMediaFileTitel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMediaFileTitel.HeaderText = "Title"
        Me.colMediaFileTitel.Name = "colMediaFileTitel"
        Me.colMediaFileTitel.ReadOnly = True
        '
        'colMediaFileUrlWebsite
        '
        Me.colMediaFileUrlWebsite.HeaderText = "Website"
        Me.colMediaFileUrlWebsite.Name = "colMediaFileUrlWebsite"
        Me.colMediaFileUrlWebsite.ReadOnly = True
        Me.colMediaFileUrlWebsite.Visible = False
        Me.colMediaFileUrlWebsite.Width = 5
        '
        'colMediaFileLanguage
        '
        Me.colMediaFileLanguage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colMediaFileLanguage.HeaderText = "Language"
        Me.colMediaFileLanguage.Name = "colMediaFileLanguage"
        Me.colMediaFileLanguage.ReadOnly = True
        Me.colMediaFileLanguage.Width = 83
        '
        'colMediaFileDuration
        '
        Me.colMediaFileDuration.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colMediaFileDuration.DefaultCellStyle = DataGridViewCellStyle1
        Me.colMediaFileDuration.HeaderText = "Duration"
        Me.colMediaFileDuration.Name = "colMediaFileDuration"
        Me.colMediaFileDuration.ReadOnly = True
        Me.colMediaFileDuration.Width = 78
        '
        'colMediaFileVariant
        '
        Me.colMediaFileVariant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colMediaFileVariant.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.colMediaFileVariant.HeaderText = "Variant"
        Me.colMediaFileVariant.Name = "colMediaFileVariant"
        Me.colMediaFileVariant.Width = 49
        '
        'colMediaFileVideoType
        '
        Me.colMediaFileVideoType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colMediaFileVideoType.HeaderText = "Type"
        Me.colMediaFileVideoType.Name = "colMediaFileVideoType"
        Me.colMediaFileVideoType.ReadOnly = True
        Me.colMediaFileVideoType.Width = 55
        '
        'colMediaFileSource
        '
        Me.colMediaFileSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colMediaFileSource.HeaderText = "Source"
        Me.colMediaFileSource.Name = "colMediaFileSource"
        Me.colMediaFileSource.ReadOnly = True
        Me.colMediaFileSource.Width = 67
        '
        'colMediaFileAddon
        '
        Me.colMediaFileAddon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colMediaFileAddon.HeaderText = "Addon"
        Me.colMediaFileAddon.Name = "colMediaFileAddon"
        Me.colMediaFileAddon.ReadOnly = True
        Me.colMediaFileAddon.Width = 67
        '
        'dlgMediaFileSelect
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(984, 661)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pblBottom)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "dlgMediaFileSelect"
        Me.Text = "Select Media File"
        Me.gbYouTubeSearch.ResumeLayout(False)
        Me.gbYouTubeSearch.PerformLayout()
        Me.tblYouTubeSearch.ResumeLayout(False)
        Me.tblYouTubeSearch.PerformLayout()
        Me.gbCustom.ResumeLayout(False)
        Me.gbCustom.PerformLayout()
        Me.tblCustom.ResumeLayout(False)
        Me.tblCustom.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        CType(Me.dgvMediaFiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pblBottom.ResumeLayout(False)
        Me.pblBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gbCustom As System.Windows.Forms.GroupBox
    Friend WithEvents lblCustomURL As System.Windows.Forms.Label
    Friend WithEvents txtCustomURL As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomLocalFile_Browse As System.Windows.Forms.Button
    Friend WithEvents txtCustomLocalFile As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomLocalFile As System.Windows.Forms.Label
    Friend WithEvents ofdFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnOpenInBrowser As System.Windows.Forms.Button
    Friend WithEvents btnCustomURL_Remove As System.Windows.Forms.Button
    Friend WithEvents gbYouTubeSearch As System.Windows.Forms.GroupBox
    Friend WithEvents btnYouTubeSearch As System.Windows.Forms.Button
    Friend WithEvents txtYouTubeSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnScrape As System.Windows.Forms.Button
    Friend WithEvents pblBottom As System.Windows.Forms.Panel
    Friend WithEvents tblBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMain As TableLayoutPanel
    Friend WithEvents tblYouTubeSearch As TableLayoutPanel
    Friend WithEvents tblCustom As TableLayoutPanel
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents pbStatus As ToolStripProgressBar
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents dgvMediaFiles As DataGridView
    Friend WithEvents colMediaFileTitel As DataGridViewTextBoxColumn
    Friend WithEvents colMediaFileUrlWebsite As DataGridViewTextBoxColumn
    Friend WithEvents colMediaFileLanguage As DataGridViewTextBoxColumn
    Friend WithEvents colMediaFileDuration As DataGridViewTextBoxColumn
    Friend WithEvents colMediaFileVariant As DataGridViewComboBoxColumn
    Friend WithEvents colMediaFileVideoType As DataGridViewTextBoxColumn
    Friend WithEvents colMediaFileSource As DataGridViewTextBoxColumn
    Friend WithEvents colMediaFileAddon As DataGridViewTextBoxColumn
End Class