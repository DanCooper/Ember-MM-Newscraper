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
        Me.cmnuSources = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuSourcesAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesMarkToRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesReject = New System.Windows.Forms.ToolStripMenuItem()
        Me.gbImportOptions = New System.Windows.Forms.GroupBox()
        Me.tblImportOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSkipLessThan = New System.Windows.Forms.Label()
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
        Me.colTitleFiltersIndex_TVShow = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTitleFiltersRegex_TVShow = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkTitleProperCase_TVShow = New System.Windows.Forms.CheckBox()
        Me.btnTitleFilterDefaults_TVShow = New System.Windows.Forms.Button()
        Me.chkTitleFiltersEnabled_TVShow = New System.Windows.Forms.CheckBox()
        Me.lblTitleFilters_TVShow = New System.Windows.Forms.Label()
        Me.gbTitleCleanup_TVEpisode = New System.Windows.Forms.GroupBox()
        Me.tblTitleCleanup_TVEpisode = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvTitleFilters_TVEpisode = New System.Windows.Forms.DataGridView()
        Me.colTitleFiltersIndex_TVEpisode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTitleFiltersRegex_TVEpisode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkTitleProperCase_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.btnTitleFilterDefaults_TVEpisode = New System.Windows.Forms.Button()
        Me.chkTitleFiltersEnabled_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.lblTitleFilters_TVEpisode = New System.Windows.Forms.Label()
        Me.gbMarkNew = New System.Windows.Forms.GroupBox()
        Me.tblMarkNew = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMarkNew_TVShow = New System.Windows.Forms.GroupBox()
        Me.tblMarkNew_TVShow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMarkAsNew_TVShow = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsMarked_TVShow = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsNewWithoutNFO_TVShow = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsMarkedWithoutNFO_TVShow = New System.Windows.Forms.CheckBox()
        Me.gbMarkNew_TVEpisode = New System.Windows.Forms.GroupBox()
        Me.tblMarkNew_TVEpisode = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMarkAsNew_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsMarked_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsNewWithoutNFO_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.gbResetNew = New System.Windows.Forms.GroupBox()
        Me.tblResetNew = New System.Windows.Forms.TableLayoutPanel()
        Me.gbResetNew_TVShow = New System.Windows.Forms.GroupBox()
        Me.tblResetNew_TVShow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkResetNewBeforeDBUpdate_TVShow = New System.Windows.Forms.CheckBox()
        Me.chkResetNewOnExit_TVShow = New System.Windows.Forms.CheckBox()
        Me.gbResetNew_TVEpisode = New System.Windows.Forms.GroupBox()
        Me.tblResetNew_TVEpisode = New System.Windows.Forms.TableLayoutPanel()
        Me.chkResetNewBeforeDBUpdate_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkResetNewOnExit_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbSources.SuspendLayout()
        Me.tblSources.SuspendLayout()
        Me.gbSourcesDefaults.SuspendLayout()
        Me.tblSourcesDefaults.SuspendLayout()
        CType(Me.dgvSources, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnuSources.SuspendLayout()
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
        Me.gbMarkNew.SuspendLayout()
        Me.tblMarkNew.SuspendLayout()
        Me.gbMarkNew_TVShow.SuspendLayout()
        Me.tblMarkNew_TVShow.SuspendLayout()
        Me.gbMarkNew_TVEpisode.SuspendLayout()
        Me.tblMarkNew_TVEpisode.SuspendLayout()
        Me.gbResetNew.SuspendLayout()
        Me.tblResetNew.SuspendLayout()
        Me.gbResetNew_TVShow.SuspendLayout()
        Me.tblResetNew_TVShow.SuspendLayout()
        Me.gbResetNew_TVEpisode.SuspendLayout()
        Me.tblResetNew_TVEpisode.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(1138, 627)
        Me.pnlSettings.TabIndex = 14
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSettings.ColumnCount = 1
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettings.Controls.Add(Me.gbSources, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbImportOptions, 0, 1)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 3
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(1138, 627)
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
        Me.gbSources.Size = New System.Drawing.Size(1132, 169)
        Me.gbSources.TabIndex = 19
        Me.gbSources.TabStop = False
        Me.gbSources.Text = "Sources"
        '
        'tblSources
        '
        Me.tblSources.AutoSize = True
        Me.tblSources.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSources.ColumnCount = 1
        Me.tblSources.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSources.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSources.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSources.Controls.Add(Me.gbSourcesDefaults, 0, 0)
        Me.tblSources.Controls.Add(Me.dgvSources, 0, 1)
        Me.tblSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSources.Location = New System.Drawing.Point(3, 18)
        Me.tblSources.Name = "tblSources"
        Me.tblSources.RowCount = 2
        Me.tblSources.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSources.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSources.Size = New System.Drawing.Size(1126, 148)
        Me.tblSources.TabIndex = 0
        '
        'gbSourcesDefaults
        '
        Me.gbSourcesDefaults.AutoSize = True
        Me.gbSourcesDefaults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbSourcesDefaults.Controls.Add(Me.tblSourcesDefaults)
        Me.gbSourcesDefaults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSourcesDefaults.Location = New System.Drawing.Point(3, 3)
        Me.gbSourcesDefaults.Name = "gbSourcesDefaults"
        Me.gbSourcesDefaults.Size = New System.Drawing.Size(1120, 48)
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
        Me.tblSourcesDefaults.Size = New System.Drawing.Size(1114, 27)
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
        Me.dgvSources.Size = New System.Drawing.Size(1120, 88)
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
        'gbImportOptions
        '
        Me.gbImportOptions.AutoSize = True
        Me.gbImportOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbImportOptions.Controls.Add(Me.tblImportOptions)
        Me.gbImportOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbImportOptions.Location = New System.Drawing.Point(3, 178)
        Me.gbImportOptions.Name = "gbImportOptions"
        Me.gbImportOptions.Size = New System.Drawing.Size(1132, 446)
        Me.gbImportOptions.TabIndex = 20
        Me.gbImportOptions.TabStop = False
        Me.gbImportOptions.Text = "Import Options"
        '
        'tblImportOptions
        '
        Me.tblImportOptions.AutoSize = True
        Me.tblImportOptions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblImportOptions.ColumnCount = 5
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblImportOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.Controls.Add(Me.lblSkipLessThan, 0, 0)
        Me.tblImportOptions.Controls.Add(Me.txtSkipLessThan, 1, 0)
        Me.tblImportOptions.Controls.Add(Me.chkCleanLibraryAfterUpdate, 0, 6)
        Me.tblImportOptions.Controls.Add(Me.lblSkipLessThanMB, 2, 0)
        Me.tblImportOptions.Controls.Add(Me.chkOverwriteNfo, 0, 1)
        Me.tblImportOptions.Controls.Add(Me.lblOverwriteNfo, 0, 2)
        Me.tblImportOptions.Controls.Add(Me.chkVideoSourceFromFolder, 0, 3)
        Me.tblImportOptions.Controls.Add(Me.chkDateAddedIgnoreNFO, 0, 4)
        Me.tblImportOptions.Controls.Add(Me.lblDateAdded, 0, 5)
        Me.tblImportOptions.Controls.Add(Me.cbDateAddedDateTime, 1, 5)
        Me.tblImportOptions.Controls.Add(Me.gbTitleCleanup, 4, 0)
        Me.tblImportOptions.Controls.Add(Me.gbMarkNew, 0, 7)
        Me.tblImportOptions.Controls.Add(Me.gbResetNew, 3, 7)
        Me.tblImportOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImportOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblImportOptions.Name = "tblImportOptions"
        Me.tblImportOptions.RowCount = 8
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.Size = New System.Drawing.Size(1126, 425)
        Me.tblImportOptions.TabIndex = 0
        '
        'lblSkipLessThan
        '
        Me.lblSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSkipLessThan.AutoSize = True
        Me.lblSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSkipLessThan.Location = New System.Drawing.Point(3, 7)
        Me.lblSkipLessThan.Name = "lblSkipLessThan"
        Me.lblSkipLessThan.Size = New System.Drawing.Size(122, 13)
        Me.lblSkipLessThan.TabIndex = 1
        Me.lblSkipLessThan.Text = "Skip files smaller than:"
        '
        'txtSkipLessThan
        '
        Me.txtSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSkipLessThan.Location = New System.Drawing.Point(177, 3)
        Me.txtSkipLessThan.Name = "txtSkipLessThan"
        Me.txtSkipLessThan.Size = New System.Drawing.Size(51, 22)
        Me.txtSkipLessThan.TabIndex = 0
        '
        'chkCleanLibraryAfterUpdate
        '
        Me.chkCleanLibraryAfterUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCleanLibraryAfterUpdate.AutoSize = True
        Me.chkCleanLibraryAfterUpdate.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblImportOptions.SetColumnSpan(Me.chkCleanLibraryAfterUpdate, 4)
        Me.chkCleanLibraryAfterUpdate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCleanLibraryAfterUpdate.Location = New System.Drawing.Point(3, 140)
        Me.chkCleanLibraryAfterUpdate.Name = "chkCleanLibraryAfterUpdate"
        Me.chkCleanLibraryAfterUpdate.Size = New System.Drawing.Size(218, 17)
        Me.chkCleanLibraryAfterUpdate.TabIndex = 5
        Me.chkCleanLibraryAfterUpdate.Text = "Clean database after updating library"
        Me.chkCleanLibraryAfterUpdate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkCleanLibraryAfterUpdate.UseVisualStyleBackColor = True
        '
        'lblSkipLessThanMB
        '
        Me.lblSkipLessThanMB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSkipLessThanMB.AutoSize = True
        Me.lblSkipLessThanMB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSkipLessThanMB.Location = New System.Drawing.Point(234, 7)
        Me.lblSkipLessThanMB.Name = "lblSkipLessThanMB"
        Me.lblSkipLessThanMB.Size = New System.Drawing.Size(24, 13)
        Me.lblSkipLessThanMB.TabIndex = 2
        Me.lblSkipLessThanMB.Text = "MB"
        '
        'chkOverwriteNfo
        '
        Me.chkOverwriteNfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOverwriteNfo.AutoSize = True
        Me.tblImportOptions.SetColumnSpan(Me.chkOverwriteNfo, 4)
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
        Me.tblImportOptions.SetColumnSpan(Me.lblOverwriteNfo, 4)
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
        Me.tblImportOptions.SetColumnSpan(Me.chkVideoSourceFromFolder, 4)
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
        Me.tblImportOptions.SetColumnSpan(Me.chkDateAddedIgnoreNFO, 4)
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
        Me.tblImportOptions.SetColumnSpan(Me.cbDateAddedDateTime, 3)
        Me.cbDateAddedDateTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbDateAddedDateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDateAddedDateTime.FormattingEnabled = True
        Me.cbDateAddedDateTime.Location = New System.Drawing.Point(177, 113)
        Me.cbDateAddedDateTime.Name = "cbDateAddedDateTime"
        Me.cbDateAddedDateTime.Size = New System.Drawing.Size(270, 21)
        Me.cbDateAddedDateTime.TabIndex = 11
        '
        'gbTitleCleanup
        '
        Me.gbTitleCleanup.AutoSize = True
        Me.gbTitleCleanup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbTitleCleanup.Controls.Add(Me.tblTitleCleanup)
        Me.gbTitleCleanup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTitleCleanup.Location = New System.Drawing.Point(453, 3)
        Me.gbTitleCleanup.Name = "gbTitleCleanup"
        Me.tblImportOptions.SetRowSpan(Me.gbTitleCleanup, 8)
        Me.gbTitleCleanup.Size = New System.Drawing.Size(670, 419)
        Me.gbTitleCleanup.TabIndex = 15
        Me.gbTitleCleanup.TabStop = False
        Me.gbTitleCleanup.Text = "Title Cleanup"
        '
        'tblTitleCleanup
        '
        Me.tblTitleCleanup.AutoSize = True
        Me.tblTitleCleanup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblTitleCleanup.ColumnCount = 2
        Me.tblTitleCleanup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblTitleCleanup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblTitleCleanup.Controls.Add(Me.gbTitleCleanup_TVShow, 0, 0)
        Me.tblTitleCleanup.Controls.Add(Me.gbTitleCleanup_TVEpisode, 1, 0)
        Me.tblTitleCleanup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTitleCleanup.Location = New System.Drawing.Point(3, 18)
        Me.tblTitleCleanup.Name = "tblTitleCleanup"
        Me.tblTitleCleanup.RowCount = 1
        Me.tblTitleCleanup.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup.Size = New System.Drawing.Size(664, 398)
        Me.tblTitleCleanup.TabIndex = 0
        '
        'gbTitleCleanup_TVShow
        '
        Me.gbTitleCleanup_TVShow.AutoSize = True
        Me.gbTitleCleanup_TVShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbTitleCleanup_TVShow.Controls.Add(Me.tblTitleCleanup_TVShow)
        Me.gbTitleCleanup_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTitleCleanup_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.gbTitleCleanup_TVShow.Name = "gbTitleCleanup_TVShow"
        Me.gbTitleCleanup_TVShow.Size = New System.Drawing.Size(326, 392)
        Me.gbTitleCleanup_TVShow.TabIndex = 0
        Me.gbTitleCleanup_TVShow.TabStop = False
        Me.gbTitleCleanup_TVShow.Text = "TV Shows"
        '
        'tblTitleCleanup_TVShow
        '
        Me.tblTitleCleanup_TVShow.AutoSize = True
        Me.tblTitleCleanup_TVShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
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
        Me.tblTitleCleanup_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblTitleCleanup_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVShow.Size = New System.Drawing.Size(320, 371)
        Me.tblTitleCleanup_TVShow.TabIndex = 0
        '
        'dgvTitleFilters_TVShow
        '
        Me.dgvTitleFilters_TVShow.AllowUserToResizeRows = False
        Me.dgvTitleFilters_TVShow.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvTitleFilters_TVShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTitleFilters_TVShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTitleFilters_TVShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTitleFiltersIndex_TVShow, Me.colTitleFiltersRegex_TVShow})
        Me.tblTitleCleanup_TVShow.SetColumnSpan(Me.dgvTitleFilters_TVShow, 2)
        Me.dgvTitleFilters_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTitleFilters_TVShow.Location = New System.Drawing.Point(3, 49)
        Me.dgvTitleFilters_TVShow.Name = "dgvTitleFilters_TVShow"
        Me.dgvTitleFilters_TVShow.RowHeadersWidth = 25
        Me.dgvTitleFilters_TVShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTitleFilters_TVShow.ShowCellErrors = False
        Me.dgvTitleFilters_TVShow.ShowCellToolTips = False
        Me.dgvTitleFilters_TVShow.ShowRowErrors = False
        Me.dgvTitleFilters_TVShow.Size = New System.Drawing.Size(314, 290)
        Me.dgvTitleFilters_TVShow.TabIndex = 8
        '
        'colTitleFiltersIndex_TVShow
        '
        Me.colTitleFiltersIndex_TVShow.HeaderText = "Index"
        Me.colTitleFiltersIndex_TVShow.Name = "colTitleFiltersIndex_TVShow"
        Me.colTitleFiltersIndex_TVShow.Visible = False
        '
        'colTitleFiltersRegex_TVShow
        '
        Me.colTitleFiltersRegex_TVShow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTitleFiltersRegex_TVShow.HeaderText = "Regex"
        Me.colTitleFiltersRegex_TVShow.Name = "colTitleFiltersRegex_TVShow"
        '
        'chkTitleProperCase_TVShow
        '
        Me.chkTitleProperCase_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleProperCase_TVShow.AutoSize = True
        Me.tblTitleCleanup_TVShow.SetColumnSpan(Me.chkTitleProperCase_TVShow, 2)
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
        Me.btnTitleFilterDefaults_TVShow.Location = New System.Drawing.Point(212, 345)
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
        Me.tblTitleCleanup_TVShow.SetColumnSpan(Me.chkTitleFiltersEnabled_TVShow, 2)
        Me.chkTitleFiltersEnabled_TVShow.Location = New System.Drawing.Point(3, 26)
        Me.chkTitleFiltersEnabled_TVShow.Name = "chkTitleFiltersEnabled_TVShow"
        Me.chkTitleFiltersEnabled_TVShow.Size = New System.Drawing.Size(119, 17)
        Me.chkTitleFiltersEnabled_TVShow.TabIndex = 0
        Me.chkTitleFiltersEnabled_TVShow.Text = "Enable Title Filters"
        Me.chkTitleFiltersEnabled_TVShow.UseVisualStyleBackColor = True
        '
        'lblTitleFilters_TVShow
        '
        Me.lblTitleFilters_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitleFilters_TVShow.AutoSize = True
        Me.lblTitleFilters_TVShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleFilters_TVShow.Location = New System.Drawing.Point(3, 350)
        Me.lblTitleFilters_TVShow.Name = "lblTitleFilters_TVShow"
        Me.lblTitleFilters_TVShow.Size = New System.Drawing.Size(201, 13)
        Me.lblTitleFilters_TVShow.TabIndex = 6
        Me.lblTitleFilters_TVShow.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'gbTitleCleanup_TVEpisode
        '
        Me.gbTitleCleanup_TVEpisode.AutoSize = True
        Me.gbTitleCleanup_TVEpisode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbTitleCleanup_TVEpisode.Controls.Add(Me.tblTitleCleanup_TVEpisode)
        Me.gbTitleCleanup_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTitleCleanup_TVEpisode.Location = New System.Drawing.Point(335, 3)
        Me.gbTitleCleanup_TVEpisode.Name = "gbTitleCleanup_TVEpisode"
        Me.gbTitleCleanup_TVEpisode.Size = New System.Drawing.Size(326, 392)
        Me.gbTitleCleanup_TVEpisode.TabIndex = 0
        Me.gbTitleCleanup_TVEpisode.TabStop = False
        Me.gbTitleCleanup_TVEpisode.Text = "Episodes"
        '
        'tblTitleCleanup_TVEpisode
        '
        Me.tblTitleCleanup_TVEpisode.AutoSize = True
        Me.tblTitleCleanup_TVEpisode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblTitleCleanup_TVEpisode.ColumnCount = 2
        Me.tblTitleCleanup_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.dgvTitleFilters_TVEpisode, 0, 2)
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.chkTitleProperCase_TVEpisode, 0, 0)
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.btnTitleFilterDefaults_TVEpisode, 1, 3)
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.chkTitleFiltersEnabled_TVEpisode, 0, 1)
        Me.tblTitleCleanup_TVEpisode.Controls.Add(Me.lblTitleFilters_TVEpisode, 0, 3)
        Me.tblTitleCleanup_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTitleCleanup_TVEpisode.Location = New System.Drawing.Point(3, 18)
        Me.tblTitleCleanup_TVEpisode.Name = "tblTitleCleanup_TVEpisode"
        Me.tblTitleCleanup_TVEpisode.RowCount = 4
        Me.tblTitleCleanup_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblTitleCleanup_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup_TVEpisode.Size = New System.Drawing.Size(320, 371)
        Me.tblTitleCleanup_TVEpisode.TabIndex = 0
        '
        'dgvTitleFilters_TVEpisode
        '
        Me.dgvTitleFilters_TVEpisode.AllowUserToResizeRows = False
        Me.dgvTitleFilters_TVEpisode.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvTitleFilters_TVEpisode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTitleFilters_TVEpisode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTitleFilters_TVEpisode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTitleFiltersIndex_TVEpisode, Me.colTitleFiltersRegex_TVEpisode})
        Me.tblTitleCleanup_TVEpisode.SetColumnSpan(Me.dgvTitleFilters_TVEpisode, 2)
        Me.dgvTitleFilters_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTitleFilters_TVEpisode.Location = New System.Drawing.Point(3, 49)
        Me.dgvTitleFilters_TVEpisode.Name = "dgvTitleFilters_TVEpisode"
        Me.dgvTitleFilters_TVEpisode.RowHeadersWidth = 25
        Me.dgvTitleFilters_TVEpisode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTitleFilters_TVEpisode.ShowCellErrors = False
        Me.dgvTitleFilters_TVEpisode.ShowCellToolTips = False
        Me.dgvTitleFilters_TVEpisode.ShowRowErrors = False
        Me.dgvTitleFilters_TVEpisode.Size = New System.Drawing.Size(314, 290)
        Me.dgvTitleFilters_TVEpisode.TabIndex = 8
        '
        'colTitleFiltersIndex_TVEpisode
        '
        Me.colTitleFiltersIndex_TVEpisode.HeaderText = "Index"
        Me.colTitleFiltersIndex_TVEpisode.Name = "colTitleFiltersIndex_TVEpisode"
        Me.colTitleFiltersIndex_TVEpisode.Visible = False
        '
        'colTitleFiltersRegex_TVEpisode
        '
        Me.colTitleFiltersRegex_TVEpisode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTitleFiltersRegex_TVEpisode.HeaderText = "Regex"
        Me.colTitleFiltersRegex_TVEpisode.Name = "colTitleFiltersRegex_TVEpisode"
        '
        'chkTitleProperCase_TVEpisode
        '
        Me.chkTitleProperCase_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleProperCase_TVEpisode.AutoSize = True
        Me.tblTitleCleanup_TVEpisode.SetColumnSpan(Me.chkTitleProperCase_TVEpisode, 2)
        Me.chkTitleProperCase_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitleProperCase_TVEpisode.Location = New System.Drawing.Point(3, 3)
        Me.chkTitleProperCase_TVEpisode.Name = "chkTitleProperCase_TVEpisode"
        Me.chkTitleProperCase_TVEpisode.Size = New System.Drawing.Size(181, 17)
        Me.chkTitleProperCase_TVEpisode.TabIndex = 1
        Me.chkTitleProperCase_TVEpisode.Text = "Convert Names to Proper Case"
        Me.chkTitleProperCase_TVEpisode.UseVisualStyleBackColor = True
        '
        'btnTitleFilterDefaults_TVEpisode
        '
        Me.btnTitleFilterDefaults_TVEpisode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTitleFilterDefaults_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTitleFilterDefaults_TVEpisode.Image = CType(resources.GetObject("btnTitleFilterDefaults_TVEpisode.Image"), System.Drawing.Image)
        Me.btnTitleFilterDefaults_TVEpisode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTitleFilterDefaults_TVEpisode.Location = New System.Drawing.Point(212, 345)
        Me.btnTitleFilterDefaults_TVEpisode.Name = "btnTitleFilterDefaults_TVEpisode"
        Me.btnTitleFilterDefaults_TVEpisode.Size = New System.Drawing.Size(105, 23)
        Me.btnTitleFilterDefaults_TVEpisode.TabIndex = 5
        Me.btnTitleFilterDefaults_TVEpisode.Text = "Defaults"
        Me.btnTitleFilterDefaults_TVEpisode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTitleFilterDefaults_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkTitleFiltersEnabled_TVEpisode
        '
        Me.chkTitleFiltersEnabled_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleFiltersEnabled_TVEpisode.AutoSize = True
        Me.tblTitleCleanup_TVEpisode.SetColumnSpan(Me.chkTitleFiltersEnabled_TVEpisode, 2)
        Me.chkTitleFiltersEnabled_TVEpisode.Location = New System.Drawing.Point(3, 26)
        Me.chkTitleFiltersEnabled_TVEpisode.Name = "chkTitleFiltersEnabled_TVEpisode"
        Me.chkTitleFiltersEnabled_TVEpisode.Size = New System.Drawing.Size(119, 17)
        Me.chkTitleFiltersEnabled_TVEpisode.TabIndex = 0
        Me.chkTitleFiltersEnabled_TVEpisode.Text = "Enable Title Filters"
        Me.chkTitleFiltersEnabled_TVEpisode.UseVisualStyleBackColor = True
        '
        'lblTitleFilters_TVEpisode
        '
        Me.lblTitleFilters_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitleFilters_TVEpisode.AutoSize = True
        Me.lblTitleFilters_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleFilters_TVEpisode.Location = New System.Drawing.Point(3, 350)
        Me.lblTitleFilters_TVEpisode.Name = "lblTitleFilters_TVEpisode"
        Me.lblTitleFilters_TVEpisode.Size = New System.Drawing.Size(201, 13)
        Me.lblTitleFilters_TVEpisode.TabIndex = 6
        Me.lblTitleFilters_TVEpisode.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'gbMarkNew
        '
        Me.gbMarkNew.AutoSize = True
        Me.gbMarkNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblImportOptions.SetColumnSpan(Me.gbMarkNew, 3)
        Me.gbMarkNew.Controls.Add(Me.tblMarkNew)
        Me.gbMarkNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMarkNew.Location = New System.Drawing.Point(3, 163)
        Me.gbMarkNew.Name = "gbMarkNew"
        Me.gbMarkNew.Size = New System.Drawing.Size(255, 259)
        Me.gbMarkNew.TabIndex = 16
        Me.gbMarkNew.TabStop = False
        Me.gbMarkNew.Text = "Mark newly added"
        '
        'tblMarkNew
        '
        Me.tblMarkNew.AutoSize = True
        Me.tblMarkNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMarkNew.ColumnCount = 1
        Me.tblMarkNew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMarkNew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMarkNew.Controls.Add(Me.gbMarkNew_TVShow, 0, 0)
        Me.tblMarkNew.Controls.Add(Me.gbMarkNew_TVEpisode, 0, 1)
        Me.tblMarkNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMarkNew.Location = New System.Drawing.Point(3, 18)
        Me.tblMarkNew.Name = "tblMarkNew"
        Me.tblMarkNew.RowCount = 1
        Me.tblMarkNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew.Size = New System.Drawing.Size(249, 238)
        Me.tblMarkNew.TabIndex = 0
        '
        'gbMarkNew_TVShow
        '
        Me.gbMarkNew_TVShow.AutoSize = True
        Me.gbMarkNew_TVShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbMarkNew_TVShow.Controls.Add(Me.tblMarkNew_TVShow)
        Me.gbMarkNew_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMarkNew_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.gbMarkNew_TVShow.Name = "gbMarkNew_TVShow"
        Me.gbMarkNew_TVShow.Size = New System.Drawing.Size(243, 113)
        Me.gbMarkNew_TVShow.TabIndex = 8
        Me.gbMarkNew_TVShow.TabStop = False
        Me.gbMarkNew_TVShow.Text = "TV Shows"
        '
        'tblMarkNew_TVShow
        '
        Me.tblMarkNew_TVShow.AutoSize = True
        Me.tblMarkNew_TVShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMarkNew_TVShow.ColumnCount = 1
        Me.tblMarkNew_TVShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMarkNew_TVShow.Controls.Add(Me.chkMarkAsNew_TVShow, 0, 0)
        Me.tblMarkNew_TVShow.Controls.Add(Me.chkMarkAsMarked_TVShow, 0, 2)
        Me.tblMarkNew_TVShow.Controls.Add(Me.chkMarkAsNewWithoutNFO_TVShow, 0, 1)
        Me.tblMarkNew_TVShow.Controls.Add(Me.chkMarkAsMarkedWithoutNFO_TVShow, 0, 3)
        Me.tblMarkNew_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMarkNew_TVShow.Location = New System.Drawing.Point(3, 18)
        Me.tblMarkNew_TVShow.Name = "tblMarkNew_TVShow"
        Me.tblMarkNew_TVShow.RowCount = 4
        Me.tblMarkNew_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew_TVShow.Size = New System.Drawing.Size(237, 92)
        Me.tblMarkNew_TVShow.TabIndex = 0
        '
        'chkMarkAsNew_TVShow
        '
        Me.chkMarkAsNew_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMarkAsNew_TVShow.AutoSize = True
        Me.chkMarkAsNew_TVShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkAsNew_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.chkMarkAsNew_TVShow.Name = "chkMarkAsNew_TVShow"
        Me.chkMarkAsNew_TVShow.Size = New System.Drawing.Size(100, 17)
        Me.chkMarkAsNew_TVShow.TabIndex = 6
        Me.chkMarkAsNew_TVShow.Text = "Mark as ""New"""
        Me.chkMarkAsNew_TVShow.UseVisualStyleBackColor = True
        '
        'chkMarkAsMarked_TVShow
        '
        Me.chkMarkAsMarked_TVShow.AutoSize = True
        Me.chkMarkAsMarked_TVShow.Location = New System.Drawing.Point(3, 49)
        Me.chkMarkAsMarked_TVShow.Name = "chkMarkAsMarked_TVShow"
        Me.chkMarkAsMarked_TVShow.Size = New System.Drawing.Size(116, 17)
        Me.chkMarkAsMarked_TVShow.TabIndex = 0
        Me.chkMarkAsMarked_TVShow.Text = "Mark as ""Marked"""
        Me.chkMarkAsMarked_TVShow.UseVisualStyleBackColor = True
        '
        'chkMarkAsNewWithoutNFO_TVShow
        '
        Me.chkMarkAsNewWithoutNFO_TVShow.AutoSize = True
        Me.chkMarkAsNewWithoutNFO_TVShow.Location = New System.Drawing.Point(3, 26)
        Me.chkMarkAsNewWithoutNFO_TVShow.Name = "chkMarkAsNewWithoutNFO_TVShow"
        Me.chkMarkAsNewWithoutNFO_TVShow.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMarkAsNewWithoutNFO_TVShow.Size = New System.Drawing.Size(181, 17)
        Me.chkMarkAsNewWithoutNFO_TVShow.TabIndex = 0
        Me.chkMarkAsNewWithoutNFO_TVShow.Text = "Only if no valid NFO exists"
        Me.chkMarkAsNewWithoutNFO_TVShow.UseVisualStyleBackColor = True
        '
        'chkMarkAsMarkedWithoutNFO_TVShow
        '
        Me.chkMarkAsMarkedWithoutNFO_TVShow.AutoSize = True
        Me.chkMarkAsMarkedWithoutNFO_TVShow.Location = New System.Drawing.Point(3, 72)
        Me.chkMarkAsMarkedWithoutNFO_TVShow.Name = "chkMarkAsMarkedWithoutNFO_TVShow"
        Me.chkMarkAsMarkedWithoutNFO_TVShow.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMarkAsMarkedWithoutNFO_TVShow.Size = New System.Drawing.Size(181, 17)
        Me.chkMarkAsMarkedWithoutNFO_TVShow.TabIndex = 0
        Me.chkMarkAsMarkedWithoutNFO_TVShow.Text = "Only if no valid NFO exists"
        Me.chkMarkAsMarkedWithoutNFO_TVShow.UseVisualStyleBackColor = True
        '
        'gbMarkNew_TVEpisode
        '
        Me.gbMarkNew_TVEpisode.AutoSize = True
        Me.gbMarkNew_TVEpisode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbMarkNew_TVEpisode.Controls.Add(Me.tblMarkNew_TVEpisode)
        Me.gbMarkNew_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMarkNew_TVEpisode.Location = New System.Drawing.Point(3, 122)
        Me.gbMarkNew_TVEpisode.Name = "gbMarkNew_TVEpisode"
        Me.gbMarkNew_TVEpisode.Size = New System.Drawing.Size(243, 113)
        Me.gbMarkNew_TVEpisode.TabIndex = 9
        Me.gbMarkNew_TVEpisode.TabStop = False
        Me.gbMarkNew_TVEpisode.Text = "Episodes"
        '
        'tblMarkNew_TVEpisode
        '
        Me.tblMarkNew_TVEpisode.AutoSize = True
        Me.tblMarkNew_TVEpisode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMarkNew_TVEpisode.ColumnCount = 1
        Me.tblMarkNew_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMarkNew_TVEpisode.Controls.Add(Me.chkMarkAsNew_TVEpisode, 0, 0)
        Me.tblMarkNew_TVEpisode.Controls.Add(Me.chkMarkAsMarked_TVEpisode, 0, 2)
        Me.tblMarkNew_TVEpisode.Controls.Add(Me.chkMarkAsNewWithoutNFO_TVEpisode, 0, 1)
        Me.tblMarkNew_TVEpisode.Controls.Add(Me.chkMarkAsMarkedWithoutNFO_TVEpisode, 0, 3)
        Me.tblMarkNew_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMarkNew_TVEpisode.Location = New System.Drawing.Point(3, 18)
        Me.tblMarkNew_TVEpisode.Name = "tblMarkNew_TVEpisode"
        Me.tblMarkNew_TVEpisode.RowCount = 4
        Me.tblMarkNew_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew_TVEpisode.Size = New System.Drawing.Size(237, 92)
        Me.tblMarkNew_TVEpisode.TabIndex = 0
        '
        'chkMarkAsNew_TVEpisode
        '
        Me.chkMarkAsNew_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMarkAsNew_TVEpisode.AutoSize = True
        Me.chkMarkAsNew_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMarkAsNew_TVEpisode.Location = New System.Drawing.Point(3, 3)
        Me.chkMarkAsNew_TVEpisode.Name = "chkMarkAsNew_TVEpisode"
        Me.chkMarkAsNew_TVEpisode.Size = New System.Drawing.Size(100, 17)
        Me.chkMarkAsNew_TVEpisode.TabIndex = 7
        Me.chkMarkAsNew_TVEpisode.Text = "Mark as ""New"""
        Me.chkMarkAsNew_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkMarkAsMarked_TVEpisode
        '
        Me.chkMarkAsMarked_TVEpisode.AutoSize = True
        Me.chkMarkAsMarked_TVEpisode.Location = New System.Drawing.Point(3, 49)
        Me.chkMarkAsMarked_TVEpisode.Name = "chkMarkAsMarked_TVEpisode"
        Me.chkMarkAsMarked_TVEpisode.Size = New System.Drawing.Size(116, 17)
        Me.chkMarkAsMarked_TVEpisode.TabIndex = 0
        Me.chkMarkAsMarked_TVEpisode.Text = "Mark as ""Marked"""
        Me.chkMarkAsMarked_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkMarkAsNewWithoutNFO_TVEpisode
        '
        Me.chkMarkAsNewWithoutNFO_TVEpisode.AutoSize = True
        Me.chkMarkAsNewWithoutNFO_TVEpisode.Location = New System.Drawing.Point(3, 26)
        Me.chkMarkAsNewWithoutNFO_TVEpisode.Name = "chkMarkAsNewWithoutNFO_TVEpisode"
        Me.chkMarkAsNewWithoutNFO_TVEpisode.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMarkAsNewWithoutNFO_TVEpisode.Size = New System.Drawing.Size(181, 17)
        Me.chkMarkAsNewWithoutNFO_TVEpisode.TabIndex = 0
        Me.chkMarkAsNewWithoutNFO_TVEpisode.Text = "Only if no valid NFO exists"
        Me.chkMarkAsNewWithoutNFO_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkMarkAsMarkedWithoutNFO_TVEpisode
        '
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode.AutoSize = True
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode.Location = New System.Drawing.Point(3, 72)
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode.Name = "chkMarkAsMarkedWithoutNFO_TVEpisode"
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode.Size = New System.Drawing.Size(181, 17)
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode.TabIndex = 0
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode.Text = "Only if no valid NFO exists"
        Me.chkMarkAsMarkedWithoutNFO_TVEpisode.UseVisualStyleBackColor = True
        '
        'gbResetNew
        '
        Me.gbResetNew.AutoSize = True
        Me.gbResetNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbResetNew.Controls.Add(Me.tblResetNew)
        Me.gbResetNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbResetNew.Location = New System.Drawing.Point(264, 163)
        Me.gbResetNew.Name = "gbResetNew"
        Me.gbResetNew.Size = New System.Drawing.Size(183, 259)
        Me.gbResetNew.TabIndex = 17
        Me.gbResetNew.TabStop = False
        Me.gbResetNew.Text = "Reset marker ""New"""
        '
        'tblResetNew
        '
        Me.tblResetNew.AutoSize = True
        Me.tblResetNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblResetNew.ColumnCount = 1
        Me.tblResetNew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblResetNew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblResetNew.Controls.Add(Me.gbResetNew_TVShow, 0, 0)
        Me.tblResetNew.Controls.Add(Me.gbResetNew_TVEpisode, 0, 1)
        Me.tblResetNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblResetNew.Location = New System.Drawing.Point(3, 18)
        Me.tblResetNew.Name = "tblResetNew"
        Me.tblResetNew.RowCount = 1
        Me.tblResetNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblResetNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblResetNew.Size = New System.Drawing.Size(177, 238)
        Me.tblResetNew.TabIndex = 0
        '
        'gbResetNew_TVShow
        '
        Me.gbResetNew_TVShow.AutoSize = True
        Me.gbResetNew_TVShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbResetNew_TVShow.Controls.Add(Me.tblResetNew_TVShow)
        Me.gbResetNew_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbResetNew_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.gbResetNew_TVShow.Name = "gbResetNew_TVShow"
        Me.gbResetNew_TVShow.Size = New System.Drawing.Size(171, 67)
        Me.gbResetNew_TVShow.TabIndex = 0
        Me.gbResetNew_TVShow.TabStop = False
        Me.gbResetNew_TVShow.Text = "TV Shows"
        '
        'tblResetNew_TVShow
        '
        Me.tblResetNew_TVShow.AutoSize = True
        Me.tblResetNew_TVShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblResetNew_TVShow.ColumnCount = 1
        Me.tblResetNew_TVShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblResetNew_TVShow.Controls.Add(Me.chkResetNewBeforeDBUpdate_TVShow, 0, 0)
        Me.tblResetNew_TVShow.Controls.Add(Me.chkResetNewOnExit_TVShow, 0, 1)
        Me.tblResetNew_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblResetNew_TVShow.Location = New System.Drawing.Point(3, 18)
        Me.tblResetNew_TVShow.Name = "tblResetNew_TVShow"
        Me.tblResetNew_TVShow.RowCount = 2
        Me.tblResetNew_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblResetNew_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblResetNew_TVShow.Size = New System.Drawing.Size(165, 46)
        Me.tblResetNew_TVShow.TabIndex = 0
        '
        'chkResetNewBeforeDBUpdate_TVShow
        '
        Me.chkResetNewBeforeDBUpdate_TVShow.AutoSize = True
        Me.chkResetNewBeforeDBUpdate_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.chkResetNewBeforeDBUpdate_TVShow.Name = "chkResetNewBeforeDBUpdate_TVShow"
        Me.chkResetNewBeforeDBUpdate_TVShow.Size = New System.Drawing.Size(159, 17)
        Me.chkResetNewBeforeDBUpdate_TVShow.TabIndex = 0
        Me.chkResetNewBeforeDBUpdate_TVShow.Text = "Before any Library Update"
        Me.chkResetNewBeforeDBUpdate_TVShow.UseVisualStyleBackColor = True
        '
        'chkResetNewOnExit_TVShow
        '
        Me.chkResetNewOnExit_TVShow.AutoSize = True
        Me.chkResetNewOnExit_TVShow.Location = New System.Drawing.Point(3, 26)
        Me.chkResetNewOnExit_TVShow.Name = "chkResetNewOnExit_TVShow"
        Me.chkResetNewOnExit_TVShow.Size = New System.Drawing.Size(63, 17)
        Me.chkResetNewOnExit_TVShow.TabIndex = 0
        Me.chkResetNewOnExit_TVShow.Text = "On Exit"
        Me.chkResetNewOnExit_TVShow.UseVisualStyleBackColor = True
        '
        'gbResetNew_TVEpisode
        '
        Me.gbResetNew_TVEpisode.AutoSize = True
        Me.gbResetNew_TVEpisode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbResetNew_TVEpisode.Controls.Add(Me.tblResetNew_TVEpisode)
        Me.gbResetNew_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbResetNew_TVEpisode.Location = New System.Drawing.Point(3, 76)
        Me.gbResetNew_TVEpisode.Name = "gbResetNew_TVEpisode"
        Me.gbResetNew_TVEpisode.Size = New System.Drawing.Size(171, 159)
        Me.gbResetNew_TVEpisode.TabIndex = 0
        Me.gbResetNew_TVEpisode.TabStop = False
        Me.gbResetNew_TVEpisode.Text = "Episodes"
        '
        'tblResetNew_TVEpisode
        '
        Me.tblResetNew_TVEpisode.AutoSize = True
        Me.tblResetNew_TVEpisode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblResetNew_TVEpisode.ColumnCount = 1
        Me.tblResetNew_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblResetNew_TVEpisode.Controls.Add(Me.chkResetNewBeforeDBUpdate_TVEpisode, 0, 0)
        Me.tblResetNew_TVEpisode.Controls.Add(Me.chkResetNewOnExit_TVEpisode, 0, 1)
        Me.tblResetNew_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblResetNew_TVEpisode.Location = New System.Drawing.Point(3, 18)
        Me.tblResetNew_TVEpisode.Name = "tblResetNew_TVEpisode"
        Me.tblResetNew_TVEpisode.RowCount = 2
        Me.tblResetNew_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblResetNew_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblResetNew_TVEpisode.Size = New System.Drawing.Size(165, 138)
        Me.tblResetNew_TVEpisode.TabIndex = 0
        '
        'chkResetNewBeforeDBUpdate_TVEpisode
        '
        Me.chkResetNewBeforeDBUpdate_TVEpisode.AutoSize = True
        Me.chkResetNewBeforeDBUpdate_TVEpisode.Location = New System.Drawing.Point(3, 3)
        Me.chkResetNewBeforeDBUpdate_TVEpisode.Name = "chkResetNewBeforeDBUpdate_TVEpisode"
        Me.chkResetNewBeforeDBUpdate_TVEpisode.Size = New System.Drawing.Size(159, 17)
        Me.chkResetNewBeforeDBUpdate_TVEpisode.TabIndex = 0
        Me.chkResetNewBeforeDBUpdate_TVEpisode.Text = "Before any Library Update"
        Me.chkResetNewBeforeDBUpdate_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkResetNewOnExit_TVEpisode
        '
        Me.chkResetNewOnExit_TVEpisode.AutoSize = True
        Me.chkResetNewOnExit_TVEpisode.Location = New System.Drawing.Point(3, 26)
        Me.chkResetNewOnExit_TVEpisode.Name = "chkResetNewOnExit_TVEpisode"
        Me.chkResetNewOnExit_TVEpisode.Size = New System.Drawing.Size(63, 17)
        Me.chkResetNewOnExit_TVEpisode.TabIndex = 0
        Me.chkResetNewOnExit_TVEpisode.Text = "On Exit"
        Me.chkResetNewOnExit_TVEpisode.UseVisualStyleBackColor = True
        '
        'frmTV_Source
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1138, 627)
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
        Me.cmnuSources.ResumeLayout(False)
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
        Me.gbMarkNew.ResumeLayout(False)
        Me.gbMarkNew.PerformLayout()
        Me.tblMarkNew.ResumeLayout(False)
        Me.tblMarkNew.PerformLayout()
        Me.gbMarkNew_TVShow.ResumeLayout(False)
        Me.gbMarkNew_TVShow.PerformLayout()
        Me.tblMarkNew_TVShow.ResumeLayout(False)
        Me.tblMarkNew_TVShow.PerformLayout()
        Me.gbMarkNew_TVEpisode.ResumeLayout(False)
        Me.gbMarkNew_TVEpisode.PerformLayout()
        Me.tblMarkNew_TVEpisode.ResumeLayout(False)
        Me.tblMarkNew_TVEpisode.PerformLayout()
        Me.gbResetNew.ResumeLayout(False)
        Me.gbResetNew.PerformLayout()
        Me.tblResetNew.ResumeLayout(False)
        Me.tblResetNew.PerformLayout()
        Me.gbResetNew_TVShow.ResumeLayout(False)
        Me.gbResetNew_TVShow.PerformLayout()
        Me.tblResetNew_TVShow.ResumeLayout(False)
        Me.tblResetNew_TVShow.PerformLayout()
        Me.gbResetNew_TVEpisode.ResumeLayout(False)
        Me.gbResetNew_TVEpisode.PerformLayout()
        Me.tblResetNew_TVEpisode.ResumeLayout(False)
        Me.tblResetNew_TVEpisode.PerformLayout()
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
    Friend WithEvents chkMarkAsNew_TVEpisode As CheckBox
    Friend WithEvents lblSkipLessThan As Label
    Friend WithEvents chkMarkAsNew_TVShow As CheckBox
    Friend WithEvents txtSkipLessThan As TextBox
    Friend WithEvents chkCleanLibraryAfterUpdate As CheckBox
    Friend WithEvents lblSkipLessThanMB As Label
    Friend WithEvents chkOverwriteNfo As CheckBox
    Friend WithEvents lblOverwriteNfo As Label
    Friend WithEvents chkVideoSourceFromFolder As CheckBox
    Friend WithEvents chkDateAddedIgnoreNFO As CheckBox
    Friend WithEvents lblDateAdded As Label
    Friend WithEvents cbDateAddedDateTime As ComboBox
    Friend WithEvents cmnuSources As ContextMenuStrip
    Friend WithEvents cmnuSourcesAdd As ToolStripMenuItem
    Friend WithEvents cmnuSourcesEdit As ToolStripMenuItem
    Friend WithEvents cmnuSourcesMarkToRemove As ToolStripMenuItem
    Friend WithEvents cmnuSourcesReject As ToolStripMenuItem
    Friend WithEvents gbMarkNew As GroupBox
    Friend WithEvents tblMarkNew As TableLayoutPanel
    Friend WithEvents gbMarkNew_TVShow As GroupBox
    Friend WithEvents tblMarkNew_TVShow As TableLayoutPanel
    Friend WithEvents gbMarkNew_TVEpisode As GroupBox
    Friend WithEvents tblMarkNew_TVEpisode As TableLayoutPanel
    Friend WithEvents gbResetNew As GroupBox
    Friend WithEvents tblResetNew As TableLayoutPanel
    Friend WithEvents gbResetNew_TVShow As GroupBox
    Friend WithEvents tblResetNew_TVShow As TableLayoutPanel
    Friend WithEvents gbResetNew_TVEpisode As GroupBox
    Friend WithEvents tblResetNew_TVEpisode As TableLayoutPanel
    Friend WithEvents chkMarkAsNewWithoutNFO_TVShow As CheckBox
    Friend WithEvents chkMarkAsNewWithoutNFO_TVEpisode As CheckBox
    Friend WithEvents chkMarkAsMarked_TVShow As CheckBox
    Friend WithEvents chkMarkAsMarkedWithoutNFO_TVShow As CheckBox
    Friend WithEvents chkMarkAsMarked_TVEpisode As CheckBox
    Friend WithEvents chkMarkAsMarkedWithoutNFO_TVEpisode As CheckBox
    Friend WithEvents chkResetNewBeforeDBUpdate_TVShow As CheckBox
    Friend WithEvents chkResetNewOnExit_TVShow As CheckBox
    Friend WithEvents chkResetNewBeforeDBUpdate_TVEpisode As CheckBox
    Friend WithEvents chkResetNewOnExit_TVEpisode As CheckBox
    Friend WithEvents gbTitleCleanup As GroupBox
    Friend WithEvents tblTitleCleanup As TableLayoutPanel
    Friend WithEvents gbTitleCleanup_TVShow As GroupBox
    Friend WithEvents tblTitleCleanup_TVShow As TableLayoutPanel
    Friend WithEvents dgvTitleFilters_TVShow As DataGridView
    Friend WithEvents colTitleFiltersIndex_TVShow As DataGridViewTextBoxColumn
    Friend WithEvents colTitleFiltersRegex_TVShow As DataGridViewTextBoxColumn
    Friend WithEvents chkTitleProperCase_TVShow As CheckBox
    Friend WithEvents btnTitleFilterDefaults_TVShow As Button
    Friend WithEvents chkTitleFiltersEnabled_TVShow As CheckBox
    Friend WithEvents lblTitleFilters_TVShow As Label
    Friend WithEvents gbTitleCleanup_TVEpisode As GroupBox
    Friend WithEvents tblTitleCleanup_TVEpisode As TableLayoutPanel
    Friend WithEvents dgvTitleFilters_TVEpisode As DataGridView
    Friend WithEvents colTitleFiltersIndex_TVEpisode As DataGridViewTextBoxColumn
    Friend WithEvents colTitleFiltersRegex_TVEpisode As DataGridViewTextBoxColumn
    Friend WithEvents chkTitleProperCase_TVEpisode As CheckBox
    Friend WithEvents btnTitleFilterDefaults_TVEpisode As Button
    Friend WithEvents chkTitleFiltersEnabled_TVEpisode As CheckBox
    Friend WithEvents lblTitleFilters_TVEpisode As Label
End Class
