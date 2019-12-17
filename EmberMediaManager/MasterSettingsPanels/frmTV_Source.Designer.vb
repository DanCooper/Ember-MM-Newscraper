<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTV_Source
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTV_Source))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSources = New System.Windows.Forms.GroupBox()
        Me.tblSources = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSourcesDefaults = New System.Windows.Forms.GroupBox()
        Me.tblSourcesDefaults = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSourcesDefaultsLanguage = New System.Windows.Forms.Label()
        Me.cbSourcesDefaultsEpisodeOrdering = New System.Windows.Forms.ComboBox()
        Me.lblSourcesDefaultsEpisodeOrdering = New System.Windows.Forms.Label()
        Me.cbSourcesDefaultsLanguage = New System.Windows.Forms.ComboBox()
        Me.dgvSources = New System.Windows.Forms.DataGridView()
        Me.colSourcesStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesLanguage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesEpisodeOrdering = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesSorting = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesIsSingle = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colSourcesExclude = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.gbImportOptions = New System.Windows.Forms.GroupBox()
        Me.tblImportOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMarkAsMarked_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.lblSkipLessThan = New System.Windows.Forms.Label()
        Me.chkMarkAsMarked_TVShow = New System.Windows.Forms.CheckBox()
        Me.txtSkipLessThan = New System.Windows.Forms.TextBox()
        Me.chkCleanLibraryAfterUpdate = New System.Windows.Forms.CheckBox()
        Me.lblSkipLessThanMB = New System.Windows.Forms.Label()
        Me.chkOverwriteNfo = New System.Windows.Forms.CheckBox()
        Me.lblOverwriteNfo = New System.Windows.Forms.Label()
        Me.chkVideoSourceFromFolder = New System.Windows.Forms.CheckBox()
        Me.chkDateAddedIgnoreNFO = New System.Windows.Forms.CheckBox()
        Me.lblDateAdded = New System.Windows.Forms.Label()
        Me.cbDateAddedDateTime = New System.Windows.Forms.ComboBox()
        Me.gbTitleCleanup = New System.Windows.Forms.GroupBox()
        Me.tblTitleCleanup = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTitleCleanup_TVShow = New System.Windows.Forms.GroupBox()
        Me.tblTitleCleanup_TVShow = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvTitleFilters_TVShow = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkTitleProperCase_TVShow = New System.Windows.Forms.CheckBox()
        Me.btnTitleFilterDefaults_TVShow = New System.Windows.Forms.Button()
        Me.chkTitleFiltersEnabled_TVShow = New System.Windows.Forms.CheckBox()
        Me.lblTitleFilters_TVShow = New System.Windows.Forms.Label()
        Me.gbTitleCleanup_TVEpisode = New System.Windows.Forms.GroupBox()
        Me.tblTitleCleanup_TVEpisode = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvTitleFilters_TVEpisode = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkTitleProperCase_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.btnTitleFilterDefaults_TVepisode = New System.Windows.Forms.Button()
        Me.chkTitleFiltersEnabled_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.lblTitleFilters_TVEpisode = New System.Windows.Forms.Label()
        Me.cmnuSources = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuSourcesAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesMarkToRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesReject = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbSources.SuspendLayout()
        Me.tblSources.SuspendLayout()
        Me.gbSourcesDefaults.SuspendLayout()
        Me.tblSourcesDefaults.SuspendLayout()
        CType(Me.dgvSources, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbImportOptions.SuspendLayout()
        Me.tblImportOptions.SuspendLayout()
        Me.gbTitleCleanup.SuspendLayout()
        Me.tblTitleCleanup.SuspendLayout()
        Me.gbTitleCleanup_TVShow.SuspendLayout()
        Me.tblTitleCleanup_TVShow.SuspendLayout()
        CType(Me.dgvTitleFilters_TVShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTitleCleanup_TVEpisode.SuspendLayout()
        Me.tblTitleCleanup_TVEpisode.SuspendLayout()
        CType(Me.dgvTitleFilters_TVEpisode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnuSources.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(1690, 1111)
        Me.pnlSettings.TabIndex = 14
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbSources, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbImportOptions, 0, 1)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 3
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(1690, 1111)
        Me.tblSettings.TabIndex = 1
        '
        'gbSources
        '
        Me.gbSources.AutoSize = True
        Me.gbSources.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbSources.Controls.Add(Me.tblSources)
        Me.gbSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSources.Location = New System.Drawing.Point(3, 3)
        Me.gbSources.Name = "gbSources"
        Me.gbSources.Size = New System.Drawing.Size(1278, 256)
        Me.gbSources.TabIndex = 19
        Me.gbSources.TabStop = False
        Me.gbSources.Text = "Sources"
        '
        'tblSources
        '
        Me.tblSources.AutoSize = True
        Me.tblSources.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSources.ColumnCount = 4
        Me.tblSources.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSources.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSources.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSources.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSources.Controls.Add(Me.gbSourcesDefaults, 0, 0)
        Me.tblSources.Controls.Add(Me.dgvSources, 0, 1)
        Me.tblSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSources.Location = New System.Drawing.Point(3, 18)
        Me.tblSources.Name = "tblSources"
        Me.tblSources.RowCount = 2
        Me.tblSources.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSources.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSources.Size = New System.Drawing.Size(1272, 235)
        Me.tblSources.TabIndex = 0
        '
        'gbSourcesDefaults
        '
        Me.gbSourcesDefaults.AutoSize = True
        Me.gbSourcesDefaults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSources.SetColumnSpan(Me.gbSourcesDefaults, 4)
        Me.gbSourcesDefaults.Controls.Add(Me.tblSourcesDefaults)
        Me.gbSourcesDefaults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSourcesDefaults.Location = New System.Drawing.Point(3, 3)
        Me.gbSourcesDefaults.Name = "gbSourcesDefaults"
        Me.gbSourcesDefaults.Size = New System.Drawing.Size(1266, 48)
        Me.gbSourcesDefaults.TabIndex = 10
        Me.gbSourcesDefaults.TabStop = False
        Me.gbSourcesDefaults.Text = "Defaults for new Sources"
        '
        'tblSourcesDefaults
        '
        Me.tblSourcesDefaults.AutoSize = True
        Me.tblSourcesDefaults.ColumnCount = 4
        Me.tblSourcesDefaults.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourcesDefaults.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourcesDefaults.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourcesDefaults.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourcesDefaults.Controls.Add(Me.lblSourcesDefaultsLanguage, 0, 0)
        Me.tblSourcesDefaults.Controls.Add(Me.cbSourcesDefaultsEpisodeOrdering, 3, 0)
        Me.tblSourcesDefaults.Controls.Add(Me.lblSourcesDefaultsEpisodeOrdering, 2, 0)
        Me.tblSourcesDefaults.Controls.Add(Me.cbSourcesDefaultsLanguage, 1, 0)
        Me.tblSourcesDefaults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSourcesDefaults.Location = New System.Drawing.Point(3, 18)
        Me.tblSourcesDefaults.Name = "tblSourcesDefaults"
        Me.tblSourcesDefaults.RowCount = 2
        Me.tblSourcesDefaults.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourcesDefaults.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourcesDefaults.Size = New System.Drawing.Size(1260, 27)
        Me.tblSourcesDefaults.TabIndex = 0
        '
        'lblSourcesDefaultsLanguage
        '
        Me.lblSourcesDefaultsLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourcesDefaultsLanguage.AutoSize = True
        Me.lblSourcesDefaultsLanguage.Location = New System.Drawing.Point(3, 7)
        Me.lblSourcesDefaultsLanguage.Name = "lblSourcesDefaultsLanguage"
        Me.lblSourcesDefaultsLanguage.Size = New System.Drawing.Size(102, 13)
        Me.lblSourcesDefaultsLanguage.TabIndex = 8
        Me.lblSourcesDefaultsLanguage.Text = "Default Language:"
        Me.lblSourcesDefaultsLanguage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbSourcesDefaultsEpisodeOrdering
        '
        Me.cbSourcesDefaultsEpisodeOrdering.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbSourcesDefaultsEpisodeOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourcesDefaultsEpisodeOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourcesDefaultsEpisodeOrdering.FormattingEnabled = True
        Me.cbSourcesDefaultsEpisodeOrdering.Location = New System.Drawing.Point(425, 3)
        Me.cbSourcesDefaultsEpisodeOrdering.Name = "cbSourcesDefaultsEpisodeOrdering"
        Me.cbSourcesDefaultsEpisodeOrdering.Size = New System.Drawing.Size(160, 21)
        Me.cbSourcesDefaultsEpisodeOrdering.TabIndex = 8
        '
        'lblSourcesDefaultsEpisodeOrdering
        '
        Me.lblSourcesDefaultsEpisodeOrdering.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSourcesDefaultsEpisodeOrdering.AutoSize = True
        Me.lblSourcesDefaultsEpisodeOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSourcesDefaultsEpisodeOrdering.Location = New System.Drawing.Point(277, 7)
        Me.lblSourcesDefaultsEpisodeOrdering.Name = "lblSourcesDefaultsEpisodeOrdering"
        Me.lblSourcesDefaultsEpisodeOrdering.Size = New System.Drawing.Size(142, 13)
        Me.lblSourcesDefaultsEpisodeOrdering.TabIndex = 7
        Me.lblSourcesDefaultsEpisodeOrdering.Text = "Default Episode Ordering:"
        Me.lblSourcesDefaultsEpisodeOrdering.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbSourcesDefaultsLanguage
        '
        Me.cbSourcesDefaultsLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbSourcesDefaultsLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourcesDefaultsLanguage.Location = New System.Drawing.Point(111, 3)
        Me.cbSourcesDefaultsLanguage.Name = "cbSourcesDefaultsLanguage"
        Me.cbSourcesDefaultsLanguage.Size = New System.Drawing.Size(160, 21)
        Me.cbSourcesDefaultsLanguage.TabIndex = 12
        '
        'dgvSources
        '
        Me.dgvSources.AllowUserToAddRows = False
        Me.dgvSources.AllowUserToDeleteRows = False
        Me.dgvSources.AllowUserToResizeColumns = False
        Me.dgvSources.AllowUserToResizeRows = False
        Me.dgvSources.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvSources.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvSources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvSources.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSourcesStatus, Me.colSourcesID, Me.colSourcesName, Me.colSourcesPath, Me.colSourcesLanguage, Me.colSourcesEpisodeOrdering, Me.colSourcesSorting, Me.colSourcesIsSingle, Me.colSourcesExclude})
        Me.tblSources.SetColumnSpan(Me.dgvSources, 4)
        Me.dgvSources.ContextMenuStrip = Me.cmnuSources
        Me.dgvSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSources.Location = New System.Drawing.Point(3, 57)
        Me.dgvSources.Name = "dgvSources"
        Me.dgvSources.ReadOnly = True
        Me.dgvSources.RowHeadersVisible = False
        Me.dgvSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSources.ShowCellErrors = False
        Me.dgvSources.ShowCellToolTips = False
        Me.dgvSources.ShowRowErrors = False
        Me.dgvSources.Size = New System.Drawing.Size(1266, 175)
        Me.dgvSources.TabIndex = 11
        '
        'colSourcesStatus
        '
        Me.colSourcesStatus.HeaderText = "State"
        Me.colSourcesStatus.Name = "colSourcesStatus"
        Me.colSourcesStatus.ReadOnly = True
        Me.colSourcesStatus.Visible = False
        '
        'colSourcesID
        '
        Me.colSourcesID.HeaderText = "ID"
        Me.colSourcesID.Name = "colSourcesID"
        Me.colSourcesID.ReadOnly = True
        Me.colSourcesID.Visible = False
        '
        'colSourcesName
        '
        Me.colSourcesName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colSourcesName.HeaderText = "Name"
        Me.colSourcesName.MinimumWidth = 50
        Me.colSourcesName.Name = "colSourcesName"
        Me.colSourcesName.ReadOnly = True
        '
        'colSourcesPath
        '
        Me.colSourcesPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colSourcesPath.HeaderText = "Path"
        Me.colSourcesPath.Name = "colSourcesPath"
        Me.colSourcesPath.ReadOnly = True
        Me.colSourcesPath.Width = 55
        '
        'colSourcesLanguage
        '
        Me.colSourcesLanguage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colSourcesLanguage.HeaderText = "Language"
        Me.colSourcesLanguage.Name = "colSourcesLanguage"
        Me.colSourcesLanguage.ReadOnly = True
        Me.colSourcesLanguage.Width = 83
        '
        'colSourcesEpisodeOrdering
        '
        Me.colSourcesEpisodeOrdering.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colSourcesEpisodeOrdering.HeaderText = "Ordering"
        Me.colSourcesEpisodeOrdering.Name = "colSourcesEpisodeOrdering"
        Me.colSourcesEpisodeOrdering.ReadOnly = True
        Me.colSourcesEpisodeOrdering.Width = 79
        '
        'colSourcesSorting
        '
        Me.colSourcesSorting.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colSourcesSorting.HeaderText = "Sorting"
        Me.colSourcesSorting.Name = "colSourcesSorting"
        Me.colSourcesSorting.ReadOnly = True
        Me.colSourcesSorting.Width = 70
        '
        'colSourcesIsSingle
        '
        Me.colSourcesIsSingle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colSourcesIsSingle.HeaderText = "Single TV Show"
        Me.colSourcesIsSingle.Name = "colSourcesIsSingle"
        Me.colSourcesIsSingle.ReadOnly = True
        Me.colSourcesIsSingle.Width = 92
        '
        'colSourcesExclude
        '
        Me.colSourcesExclude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colSourcesExclude.HeaderText = "Exclude"
        Me.colSourcesExclude.Name = "colSourcesExclude"
        Me.colSourcesExclude.ReadOnly = True
        Me.colSourcesExclude.Width = 52
        '
        'gbImportOptions
        '
        Me.gbImportOptions.AutoSize = True
        Me.gbImportOptions.Controls.Add(Me.tblImportOptions)
        Me.gbImportOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbImportOptions.Location = New System.Drawing.Point(3, 265)
        Me.gbImportOptions.Name = "gbImportOptions"
        Me.gbImportOptions.Size = New System.Drawing.Size(1278, 456)
        Me.gbImportOptions.TabIndex = 20
        Me.gbImportOptions.TabStop = False
        Me.gbImportOptions.Text = "Import Options"
        '
        'tblImportOptions
        '
        Me.tblImportOptions.AutoSize = True
        Me.tblImportOptions.ColumnCount = 4
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImportOptions.Controls.Add(Me.chkMarkAsMarked_TVEpisode, 0, 9)
        Me.tblImportOptions.Controls.Add(Me.lblSkipLessThan, 0, 0)
        Me.tblImportOptions.Controls.Add(Me.chkMarkAsMarked_TVShow, 0, 8)
        Me.tblImportOptions.Controls.Add(Me.txtSkipLessThan, 1, 0)
        Me.tblImportOptions.Controls.Add(Me.chkCleanLibraryAfterUpdate, 0, 6)
        Me.tblImportOptions.Controls.Add(Me.lblSkipLessThanMB, 2, 0)
        Me.tblImportOptions.Controls.Add(Me.chkOverwriteNfo, 0, 1)
        Me.tblImportOptions.Controls.Add(Me.lblOverwriteNfo, 0, 2)
        Me.tblImportOptions.Controls.Add(Me.chkVideoSourceFromFolder, 0, 3)
        Me.tblImportOptions.Controls.Add(Me.chkDateAddedIgnoreNFO, 0, 4)
        Me.tblImportOptions.Controls.Add(Me.lblDateAdded, 0, 5)
        Me.tblImportOptions.Controls.Add(Me.cbDateAddedDateTime, 1, 5)
        Me.tblImportOptions.Controls.Add(Me.gbTitleCleanup, 3, 0)
        Me.tblImportOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImportOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblImportOptions.Name = "tblImportOptions"
        Me.tblImportOptions.RowCount = 12
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.Size = New System.Drawing.Size(1272, 435)
        Me.tblImportOptions.TabIndex = 0
        '
        'chkTVGeneralMarkNewEpisodes
        '
        Me.chkMarkAsMarked_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMarkAsMarked_TVEpisode.AutoSize = True
        Me.chkMarkAsMarked_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkAsMarked_TVEpisode.Location = New System.Drawing.Point(3, 378)
        Me.chkMarkAsMarked_TVEpisode.Name = "chkTVGeneralMarkNewEpisodes"
        Me.chkMarkAsMarked_TVEpisode.Size = New System.Drawing.Size(127, 14)
        Me.chkMarkAsMarked_TVEpisode.TabIndex = 7
        Me.chkMarkAsMarked_TVEpisode.Text = "Mark New Episodes"
        Me.chkMarkAsMarked_TVEpisode.UseVisualStyleBackColor = True
        '
        'lblTVSkipLessThan
        '
        Me.lblSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSkipLessThan.AutoSize = True
        Me.lblSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSkipLessThan.Location = New System.Drawing.Point(3, 7)
        Me.lblSkipLessThan.Name = "lblTVSkipLessThan"
        Me.lblSkipLessThan.Size = New System.Drawing.Size(122, 13)
        Me.lblSkipLessThan.TabIndex = 1
        Me.lblSkipLessThan.Text = "Skip files smaller than:"
        '
        'chkTVGeneralMarkNewShows
        '
        Me.chkMarkAsMarked_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMarkAsMarked_TVShow.AutoSize = True
        Me.chkMarkAsMarked_TVShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkAsMarked_TVShow.Location = New System.Drawing.Point(3, 358)
        Me.chkMarkAsMarked_TVShow.Name = "chkTVGeneralMarkNewShows"
        Me.chkMarkAsMarked_TVShow.Size = New System.Drawing.Size(115, 14)
        Me.chkMarkAsMarked_TVShow.TabIndex = 6
        Me.chkMarkAsMarked_TVShow.Text = "Mark New Shows"
        Me.chkMarkAsMarked_TVShow.UseVisualStyleBackColor = True
        '
        'txtSkipLessThan
        '
        Me.txtSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSkipLessThan.Location = New System.Drawing.Point(365, 3)
        Me.txtSkipLessThan.Name = "txtSkipLessThan"
        Me.txtSkipLessThan.Size = New System.Drawing.Size(51, 22)
        Me.txtSkipLessThan.TabIndex = 0
        '
        'chkCleanLibraryAfterUpdate
        '
        Me.chkCleanLibraryAfterUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCleanLibraryAfterUpdate.AutoSize = True
        Me.chkCleanLibraryAfterUpdate.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkCleanLibraryAfterUpdate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCleanLibraryAfterUpdate.Location = New System.Drawing.Point(3, 227)
        Me.chkCleanLibraryAfterUpdate.Name = "chkCleanLibraryAfterUpdate"
        Me.chkCleanLibraryAfterUpdate.Size = New System.Drawing.Size(218, 17)
        Me.chkCleanLibraryAfterUpdate.TabIndex = 5
        Me.chkCleanLibraryAfterUpdate.Text = "Clean database after updating library"
        Me.chkCleanLibraryAfterUpdate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkCleanLibraryAfterUpdate.UseVisualStyleBackColor = True
        '
        'lblTVSkipLessThanMB
        '
        Me.lblSkipLessThanMB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSkipLessThanMB.AutoSize = True
        Me.lblSkipLessThanMB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSkipLessThanMB.Location = New System.Drawing.Point(422, 7)
        Me.lblSkipLessThanMB.Name = "lblTVSkipLessThanMB"
        Me.lblSkipLessThanMB.Size = New System.Drawing.Size(24, 13)
        Me.lblSkipLessThanMB.TabIndex = 2
        Me.lblSkipLessThanMB.Text = "MB"
        '
        'chkOverwriteNfo
        '
        Me.chkOverwriteNfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOverwriteNfo.AutoSize = True
        Me.chkOverwriteNfo.Location = New System.Drawing.Point(3, 31)
        Me.chkOverwriteNfo.Name = "chkOverwriteNfo"
        Me.chkOverwriteNfo.Size = New System.Drawing.Size(144, 17)
        Me.chkOverwriteNfo.TabIndex = 12
        Me.chkOverwriteNfo.Text = "Overwrite invalid NFOs"
        Me.chkOverwriteNfo.UseVisualStyleBackColor = True
        '
        'lblOverwriteNfo
        '
        Me.lblOverwriteNfo.AutoSize = True
        Me.lblOverwriteNfo.Location = New System.Drawing.Point(3, 51)
        Me.lblOverwriteNfo.Name = "lblOverwriteNfo"
        Me.lblOverwriteNfo.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblOverwriteNfo.Size = New System.Drawing.Size(356, 13)
        Me.lblOverwriteNfo.TabIndex = 13
        Me.lblOverwriteNfo.Text = "(If unchecked, invalid NFOs will be renamed to <filename>.info)"
        Me.lblOverwriteNfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkVideoSourceFromFolder
        '
        Me.chkVideoSourceFromFolder.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkVideoSourceFromFolder.AutoSize = True
        Me.chkVideoSourceFromFolder.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkVideoSourceFromFolder.Location = New System.Drawing.Point(3, 67)
        Me.chkVideoSourceFromFolder.Name = "chkVideoSourceFromFolder"
        Me.chkVideoSourceFromFolder.Size = New System.Drawing.Size(290, 17)
        Me.chkVideoSourceFromFolder.TabIndex = 11
        Me.chkVideoSourceFromFolder.Text = "Search in the full path for VideoSource information"
        Me.chkVideoSourceFromFolder.UseVisualStyleBackColor = True
        '
        'chkDateAddedIgnoreNFO
        '
        Me.chkDateAddedIgnoreNFO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDateAddedIgnoreNFO.AutoSize = True
        Me.chkDateAddedIgnoreNFO.Location = New System.Drawing.Point(3, 90)
        Me.chkDateAddedIgnoreNFO.Name = "chkDateAddedIgnoreNFO"
        Me.chkDateAddedIgnoreNFO.Size = New System.Drawing.Size(188, 17)
        Me.chkDateAddedIgnoreNFO.TabIndex = 10
        Me.chkDateAddedIgnoreNFO.Text = "Ignore <dateadded> from NFO"
        Me.chkDateAddedIgnoreNFO.UseVisualStyleBackColor = True
        '
        'lblDateAdded
        '
        Me.lblDateAdded.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDateAdded.AutoSize = True
        Me.lblDateAdded.Location = New System.Drawing.Point(3, 117)
        Me.lblDateAdded.Name = "lblDateAdded"
        Me.lblDateAdded.Size = New System.Drawing.Size(168, 13)
        Me.lblDateAdded.TabIndex = 14
        Me.lblDateAdded.Text = "Default value for <dateadded>"
        '
        'cbDateAddedDateTime
        '
        Me.cbDateAddedDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblImportOptions.SetColumnSpan(Me.cbDateAddedDateTime, 2)
        Me.cbDateAddedDateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDateAddedDateTime.FormattingEnabled = True
        Me.cbDateAddedDateTime.Location = New System.Drawing.Point(365, 113)
        Me.cbDateAddedDateTime.Name = "cbDateAddedDateTime"
        Me.cbDateAddedDateTime.Size = New System.Drawing.Size(200, 21)
        Me.cbDateAddedDateTime.TabIndex = 11
        '
        'gbTitleCleanup
        '
        Me.gbTitleCleanup.AutoSize = True
        Me.gbTitleCleanup.Controls.Add(Me.tblTitleCleanup)
        Me.gbTitleCleanup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTitleCleanup.Location = New System.Drawing.Point(571, 3)
        Me.gbTitleCleanup.Name = "gbTitleCleanup"
        Me.tblImportOptions.SetRowSpan(Me.gbTitleCleanup, 11)
        Me.gbTitleCleanup.Size = New System.Drawing.Size(698, 409)
        Me.gbTitleCleanup.TabIndex = 15
        Me.gbTitleCleanup.TabStop = False
        Me.gbTitleCleanup.Text = "Title Cleanup"
        '
        'tblTitleCleanup
        '
        Me.tblTitleCleanup.AutoSize = True
        Me.tblTitleCleanup.ColumnCount = 2
        Me.tblTitleCleanup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup.Controls.Add(Me.gbTitleCleanup_TVShow, 0, 0)
        Me.tblTitleCleanup.Controls.Add(Me.gbTitleCleanup_TVEpisode, 1, 0)
        Me.tblTitleCleanup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTitleCleanup.Location = New System.Drawing.Point(3, 18)
        Me.tblTitleCleanup.Name = "tblTitleCleanup"
        Me.tblTitleCleanup.RowCount = 1
        Me.tblTitleCleanup.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 386.0!))
        Me.tblTitleCleanup.Size = New System.Drawing.Size(692, 388)
        Me.tblTitleCleanup.TabIndex = 0
        '
        'gbTitleCleanup_TVShow
        '
        Me.gbTitleCleanup_TVShow.AutoSize = True
        Me.gbTitleCleanup_TVShow.Controls.Add(Me.tblTitleCleanup_TVShow)
        Me.gbTitleCleanup_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTitleCleanup_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.gbTitleCleanup_TVShow.Name = "gbTitleCleanup_TVShow"
        Me.gbTitleCleanup_TVShow.Size = New System.Drawing.Size(324, 382)
        Me.gbTitleCleanup_TVShow.TabIndex = 0
        Me.gbTitleCleanup_TVShow.TabStop = False
        Me.gbTitleCleanup_TVShow.Text = "TV Shows"
        '
        'tblTitleCleanup_TVShow
        '
        Me.tblTitleCleanup_TVShow.AutoSize = True
        Me.tblTitleCleanup_TVShow.ColumnCount = 2
        Me.tblTitleCleanup_TVShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup_TVShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup_TVShow.Controls.Add(Me.dgvTitleFilters_TVShow, 0, 2)
        Me.tblTitleCleanup_TVShow.Controls.Add(Me.chkTitleProperCase_TVShow, 0, 0)
        Me.tblTitleCleanup_TVShow.Controls.Add(Me.btnTitleFilterDefaults_TVShow, 1, 3)
        Me.tblTitleCleanup_TVShow.Controls.Add(Me.chkTitleFiltersEnabled_TVShow, 0, 1)
        Me.tblTitleCleanup_TVShow.Controls.Add(Me.lblTitleFilters_TVShow, 0, 3)
        Me.tblTitleCleanup_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTitleCleanup_TVShow.Location = New System.Drawing.Point(3, 18)
        Me.tblTitleCleanup_TVShow.Name = "tblTitleCleanup_TVShow"
        Me.tblTitleCleanup_TVShow.RowCount = 4
        Me.tblTitleCleanup_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVShow.Size = New System.Drawing.Size(318, 361)
        Me.tblTitleCleanup_TVShow.TabIndex = 0
        '
        'dgvTitleFilters_TVShow
        '
        Me.dgvTitleFilters_TVShow.AllowUserToResizeRows = False
        Me.dgvTitleFilters_TVShow.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvTitleFilters_TVShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTitleFilters_TVShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTitleFilters_TVShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.tblTitleCleanup_TVShow.SetColumnSpan(Me.dgvTitleFilters_TVShow, 2)
        Me.dgvTitleFilters_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTitleFilters_TVShow.Location = New System.Drawing.Point(3, 49)
        Me.dgvTitleFilters_TVShow.Name = "dgvTitleFilters_TVShow"
        Me.dgvTitleFilters_TVShow.RowHeadersWidth = 25
        Me.dgvTitleFilters_TVShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTitleFilters_TVShow.ShowCellErrors = False
        Me.dgvTitleFilters_TVShow.ShowCellToolTips = False
        Me.dgvTitleFilters_TVShow.ShowRowErrors = False
        Me.dgvTitleFilters_TVShow.Size = New System.Drawing.Size(312, 280)
        Me.dgvTitleFilters_TVShow.TabIndex = 8
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Visible = False
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.HeaderText = "Regex"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'chkTitleProperCase_TVShow
        '
        Me.chkTitleProperCase_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleProperCase_TVShow.AutoSize = True
        Me.chkTitleProperCase_TVShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitleProperCase_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.chkTitleProperCase_TVShow.Name = "chkTitleProperCase_TVShow"
        Me.chkTitleProperCase_TVShow.Size = New System.Drawing.Size(181, 17)
        Me.chkTitleProperCase_TVShow.TabIndex = 0
        Me.chkTitleProperCase_TVShow.Text = "Convert Names to Proper Case"
        Me.chkTitleProperCase_TVShow.UseVisualStyleBackColor = True
        '
        'btnTitleFilterDefaults_TVShow
        '
        Me.btnTitleFilterDefaults_TVShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTitleFilterDefaults_TVShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTitleFilterDefaults_TVShow.Image = CType(resources.GetObject("btnTitleFilterDefaults_TVShow.Image"), System.Drawing.Image)
        Me.btnTitleFilterDefaults_TVShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTitleFilterDefaults_TVShow.Location = New System.Drawing.Point(210, 335)
        Me.btnTitleFilterDefaults_TVShow.Name = "btnTitleFilterDefaults_TVShow"
        Me.btnTitleFilterDefaults_TVShow.Size = New System.Drawing.Size(105, 23)
        Me.btnTitleFilterDefaults_TVShow.TabIndex = 5
        Me.btnTitleFilterDefaults_TVShow.Text = "Defaults"
        Me.btnTitleFilterDefaults_TVShow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTitleFilterDefaults_TVShow.UseVisualStyleBackColor = True
        '
        'chkTitleFiltersEnabled_TVShow
        '
        Me.chkTitleFiltersEnabled_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleFiltersEnabled_TVShow.AutoSize = True
        Me.chkTitleFiltersEnabled_TVShow.Location = New System.Drawing.Point(3, 26)
        Me.chkTitleFiltersEnabled_TVShow.Name = "chkTitleFiltersEnabled_TVShow"
        Me.chkTitleFiltersEnabled_TVShow.Size = New System.Drawing.Size(119, 17)
        Me.chkTitleFiltersEnabled_TVShow.TabIndex = 0
        Me.chkTitleFiltersEnabled_TVShow.Text = "Enable Title Filters"
        Me.chkTitleFiltersEnabled_TVShow.UseVisualStyleBackColor = True
        '
        'lblTitleFilter_TVShows
        '
        Me.lblTitleFilters_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitleFilters_TVShow.AutoSize = True
        Me.lblTitleFilters_TVShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleFilters_TVShow.Location = New System.Drawing.Point(3, 340)
        Me.lblTitleFilters_TVShow.Name = "lblTitleFilter_TVShows"
        Me.lblTitleFilters_TVShow.Size = New System.Drawing.Size(201, 13)
        Me.lblTitleFilters_TVShow.TabIndex = 6
        Me.lblTitleFilters_TVShow.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'gbTitleCleanup_TVEpisode
        '
        Me.gbTitleCleanup_TVEpisode.AutoSize = True
        Me.gbTitleCleanup_TVEpisode.Controls.Add(Me.tblTitleCleanup_TVEpisode)
        Me.gbTitleCleanup_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTitleCleanup_TVEpisode.Location = New System.Drawing.Point(333, 3)
        Me.gbTitleCleanup_TVEpisode.Name = "gbTitleCleanup_TVEpisode"
        Me.gbTitleCleanup_TVEpisode.Size = New System.Drawing.Size(356, 382)
        Me.gbTitleCleanup_TVEpisode.TabIndex = 0
        Me.gbTitleCleanup_TVEpisode.TabStop = False
        Me.gbTitleCleanup_TVEpisode.Text = "Episodes"
        '
        'tblTitleCleanup_TVEpisode
        '
        Me.tblTitleCleanup_TVEpisode.AutoSize = True
        Me.tblTitleCleanup_TVEpisode.ColumnCount = 2
        Me.tblTitleCleanup_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.dgvTitleFilters_TVEpisode, 0, 2)
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.chkTitleProperCase_TVEpisode, 0, 0)
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.btnTitleFilterDefaults_TVepisode, 1, 3)
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.chkTitleFiltersEnabled_TVEpisode, 0, 1)
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.lblTitleFilters_TVEpisode, 0, 3)
        Me.tblTitleCleanup_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTitleCleanup_TVEpisode.Location = New System.Drawing.Point(3, 18)
        Me.tblTitleCleanup_TVEpisode.Name = "tblTitleCleanup_TVEpisode"
        Me.tblTitleCleanup_TVEpisode.RowCount = 4
        Me.tblTitleCleanup_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVEpisode.Size = New System.Drawing.Size(350, 361)
        Me.tblTitleCleanup_TVEpisode.TabIndex = 0
        '
        'dgvTitleFilters_TVEpisode
        '
        Me.dgvTitleFilters_TVEpisode.AllowUserToResizeRows = False
        Me.dgvTitleFilters_TVEpisode.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvTitleFilters_TVEpisode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTitleFilters_TVEpisode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTitleFilters_TVEpisode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6})
        Me.tblTitleCleanup_TVEpisode.SetColumnSpan(Me.dgvTitleFilters_TVEpisode, 2)
        Me.dgvTitleFilters_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTitleFilters_TVEpisode.Location = New System.Drawing.Point(3, 49)
        Me.dgvTitleFilters_TVEpisode.Name = "dgvTitleFilters_TVEpisode"
        Me.dgvTitleFilters_TVEpisode.RowHeadersWidth = 25
        Me.dgvTitleFilters_TVEpisode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTitleFilters_TVEpisode.ShowCellErrors = False
        Me.dgvTitleFilters_TVEpisode.ShowCellToolTips = False
        Me.dgvTitleFilters_TVEpisode.ShowRowErrors = False
        Me.dgvTitleFilters_TVEpisode.Size = New System.Drawing.Size(344, 280)
        Me.dgvTitleFilters_TVEpisode.TabIndex = 8
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.HeaderText = "Regex"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'chkTitleProperCase_TVEpisode
        '
        Me.chkTitleProperCase_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleProperCase_TVEpisode.AutoSize = True
        Me.chkTitleProperCase_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitleProperCase_TVEpisode.Location = New System.Drawing.Point(3, 3)
        Me.chkTitleProperCase_TVEpisode.Name = "chkTitleProperCase_TVEpisode"
        Me.chkTitleProperCase_TVEpisode.Size = New System.Drawing.Size(181, 17)
        Me.chkTitleProperCase_TVEpisode.TabIndex = 1
        Me.chkTitleProperCase_TVEpisode.Text = "Convert Names to Proper Case"
        Me.chkTitleProperCase_TVEpisode.UseVisualStyleBackColor = True
        '
        'btnTitleFilterDefaults_TVepisode
        '
        Me.btnTitleFilterDefaults_TVepisode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTitleFilterDefaults_TVepisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTitleFilterDefaults_TVepisode.Image = CType(resources.GetObject("btnTitleFilterDefaults_TVepisode.Image"), System.Drawing.Image)
        Me.btnTitleFilterDefaults_TVepisode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTitleFilterDefaults_TVepisode.Location = New System.Drawing.Point(242, 335)
        Me.btnTitleFilterDefaults_TVepisode.Name = "btnTitleFilterDefaults_TVepisode"
        Me.btnTitleFilterDefaults_TVepisode.Size = New System.Drawing.Size(105, 23)
        Me.btnTitleFilterDefaults_TVepisode.TabIndex = 5
        Me.btnTitleFilterDefaults_TVepisode.Text = "Defaults"
        Me.btnTitleFilterDefaults_TVepisode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTitleFilterDefaults_TVepisode.UseVisualStyleBackColor = True
        '
        'chkTitleFiltersEnabled_TVEpisode
        '
        Me.chkTitleFiltersEnabled_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleFiltersEnabled_TVEpisode.AutoSize = True
        Me.chkTitleFiltersEnabled_TVEpisode.Location = New System.Drawing.Point(3, 26)
        Me.chkTitleFiltersEnabled_TVEpisode.Name = "chkTitleFiltersEnabled_TVEpisode"
        Me.chkTitleFiltersEnabled_TVEpisode.Size = New System.Drawing.Size(119, 17)
        Me.chkTitleFiltersEnabled_TVEpisode.TabIndex = 0
        Me.chkTitleFiltersEnabled_TVEpisode.Text = "Enable Title Filters"
        Me.chkTitleFiltersEnabled_TVEpisode.UseVisualStyleBackColor = True
        '
        'lblTitleFilter_TVEpisodes
        '
        Me.lblTitleFilters_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitleFilters_TVEpisode.AutoSize = True
        Me.lblTitleFilters_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleFilters_TVEpisode.Location = New System.Drawing.Point(3, 340)
        Me.lblTitleFilters_TVEpisode.Name = "lblTitleFilter_TVEpisodes"
        Me.lblTitleFilters_TVEpisode.Size = New System.Drawing.Size(201, 13)
        Me.lblTitleFilters_TVEpisode.TabIndex = 6
        Me.lblTitleFilters_TVEpisode.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'cmnuSources
        '
        Me.cmnuSources.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuSourcesAdd, Me.cmnuSourcesEdit, Me.cmnuSourcesMarkToRemove, Me.cmnuSourcesReject})
        Me.cmnuSources.Name = "cmnuSources"
        Me.cmnuSources.Size = New System.Drawing.Size(193, 92)
        '
        'cmnuSourcesAdd
        '
        Me.cmnuSourcesAdd.Image = Global.Ember_Media_Manager.My.Resources.Resources.menuAdd
        Me.cmnuSourcesAdd.Name = "cmnuSourcesAdd"
        Me.cmnuSourcesAdd.Size = New System.Drawing.Size(192, 22)
        Me.cmnuSourcesAdd.Text = "Add New Source"
        '
        'cmnuSourcesEdit
        '
        Me.cmnuSourcesEdit.Image = Global.Ember_Media_Manager.My.Resources.Resources.edit
        Me.cmnuSourcesEdit.Name = "cmnuSourcesEdit"
        Me.cmnuSourcesEdit.Size = New System.Drawing.Size(192, 22)
        Me.cmnuSourcesEdit.Text = "Edit Source"
        '
        'cmnuSourcesMarkToRemove
        '
        Me.cmnuSourcesMarkToRemove.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.cmnuSourcesMarkToRemove.Name = "cmnuSourcesMarkToRemove"
        Me.cmnuSourcesMarkToRemove.Size = New System.Drawing.Size(192, 22)
        Me.cmnuSourcesMarkToRemove.Text = "Mark to Remove"
        '
        'cmnuSourcesReject
        '
        Me.cmnuSourcesReject.Image = Global.Ember_Media_Manager.My.Resources.Resources.undo
        Me.cmnuSourcesReject.Name = "cmnuSourcesReject"
        Me.cmnuSourcesReject.Size = New System.Drawing.Size(192, 22)
        Me.cmnuSourcesReject.Text = "Reject Remove Marker"
        '
        'frmTV_Source
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1690, 1111)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmTV_Source"
        Me.Text = "frmTV_Source"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbSources.ResumeLayout(False)
        Me.gbSources.PerformLayout()
        Me.tblSources.ResumeLayout(False)
        Me.tblSources.PerformLayout()
        Me.gbSourcesDefaults.ResumeLayout(False)
        Me.gbSourcesDefaults.PerformLayout()
        Me.tblSourcesDefaults.ResumeLayout(False)
        Me.tblSourcesDefaults.PerformLayout()
        CType(Me.dgvSources, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbImportOptions.ResumeLayout(False)
        Me.gbImportOptions.PerformLayout()
        Me.tblImportOptions.ResumeLayout(False)
        Me.tblImportOptions.PerformLayout()
        Me.gbTitleCleanup.ResumeLayout(False)
        Me.gbTitleCleanup.PerformLayout()
        Me.tblTitleCleanup.ResumeLayout(False)
        Me.tblTitleCleanup.PerformLayout()
        Me.gbTitleCleanup_TVShow.ResumeLayout(False)
        Me.gbTitleCleanup_TVShow.PerformLayout()
        Me.tblTitleCleanup_TVShow.ResumeLayout(False)
        Me.tblTitleCleanup_TVShow.PerformLayout()
        CType(Me.dgvTitleFilters_TVShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTitleCleanup_TVEpisode.ResumeLayout(False)
        Me.gbTitleCleanup_TVEpisode.PerformLayout()
        Me.tblTitleCleanup_TVEpisode.ResumeLayout(False)
        Me.tblTitleCleanup_TVEpisode.PerformLayout()
        CType(Me.dgvTitleFilters_TVEpisode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmnuSources.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbSources As GroupBox
    Friend WithEvents tblSources As TableLayoutPanel
    Friend WithEvents gbSourcesDefaults As GroupBox
    Friend WithEvents tblSourcesDefaults As TableLayoutPanel
    Friend WithEvents lblSourcesDefaultsLanguage As Label
    Friend WithEvents cbSourcesDefaultsEpisodeOrdering As ComboBox
    Friend WithEvents lblSourcesDefaultsEpisodeOrdering As Label
    Friend WithEvents cbSourcesDefaultsLanguage As ComboBox
    Friend WithEvents dgvSources As DataGridView
    Friend WithEvents colSourcesStatus As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesID As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesName As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesPath As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesLanguage As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesEpisodeOrdering As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesSorting As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesIsSingle As DataGridViewCheckBoxColumn
    Friend WithEvents colSourcesExclude As DataGridViewCheckBoxColumn
    Friend WithEvents gbImportOptions As GroupBox
    Friend WithEvents tblImportOptions As TableLayoutPanel
    Friend WithEvents chkMarkAsMarked_TVEpisode As CheckBox
    Friend WithEvents lblSkipLessThan As Label
    Friend WithEvents chkMarkAsMarked_TVShow As CheckBox
    Friend WithEvents txtSkipLessThan As TextBox
    Friend WithEvents chkCleanLibraryAfterUpdate As CheckBox
    Friend WithEvents lblSkipLessThanMB As Label
    Friend WithEvents chkOverwriteNfo As CheckBox
    Friend WithEvents lblOverwriteNfo As Label
    Friend WithEvents chkVideoSourceFromFolder As CheckBox
    Friend WithEvents chkDateAddedIgnoreNFO As CheckBox
    Friend WithEvents lblDateAdded As Label
    Friend WithEvents cbDateAddedDateTime As ComboBox
    Friend WithEvents gbTitleCleanup As GroupBox
    Friend WithEvents tblTitleCleanup As TableLayoutPanel
    Friend WithEvents gbTitleCleanup_TVShow As GroupBox
    Friend WithEvents tblTitleCleanup_TVShow As TableLayoutPanel
    Friend WithEvents dgvTitleFilters_TVShow As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents chkTitleProperCase_TVShow As CheckBox
    Friend WithEvents btnTitleFilterDefaults_TVShow As Button
    Friend WithEvents chkTitleFiltersEnabled_TVShow As CheckBox
    Friend WithEvents lblTitleFilters_TVShow As Label
    Friend WithEvents gbTitleCleanup_TVEpisode As GroupBox
    Friend WithEvents tblTitleCleanup_TVEpisode As TableLayoutPanel
    Friend WithEvents dgvTitleFilters_TVEpisode As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents chkTitleProperCase_TVEpisode As CheckBox
    Friend WithEvents btnTitleFilterDefaults_TVepisode As Button
    Friend WithEvents chkTitleFiltersEnabled_TVEpisode As CheckBox
    Friend WithEvents lblTitleFilters_TVEpisode As Label
    Friend WithEvents cmnuSources As ContextMenuStrip
    Friend WithEvents cmnuSourcesAdd As ToolStripMenuItem
    Friend WithEvents cmnuSourcesEdit As ToolStripMenuItem
    Friend WithEvents cmnuSourcesMarkToRemove As ToolStripMenuItem
    Friend WithEvents cmnuSourcesReject As ToolStripMenuItem
End Class
