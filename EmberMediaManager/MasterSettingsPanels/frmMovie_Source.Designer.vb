<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovie_Source
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovie_Source))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSources = New System.Windows.Forms.GroupBox()
        Me.tblSources = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSourcesDefaults = New System.Windows.Forms.GroupBox()
        Me.tblSourcesDefaults = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSourcesDefaultsLanguage = New System.Windows.Forms.Label()
        Me.cbSourcesDefaultsLanguage = New System.Windows.Forms.ComboBox()
        Me.dgvSources = New System.Windows.Forms.DataGridView()
        Me.colSourcesStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesLanguage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSourcesRecursive = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colSourcesUseFolderName = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colSourcesIsSingle = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colSourcesExclude = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colSourcesGetYear = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmnuSources = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuSourcesAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesMarkToRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSourcesReject = New System.Windows.Forms.ToolStripMenuItem()
        Me.gbImportOptions = New System.Windows.Forms.GroupBox()
        Me.tblImportOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.gbResetNew = New System.Windows.Forms.GroupBox()
        Me.tblResetNew = New System.Windows.Forms.TableLayoutPanel()
        Me.chkResetNewBeforeDBUpdate = New System.Windows.Forms.CheckBox()
        Me.chkResetNewOnExit = New System.Windows.Forms.CheckBox()
        Me.lblSkipLessThan = New System.Windows.Forms.Label()
        Me.gbMarkNew = New System.Windows.Forms.GroupBox()
        Me.tblMarkNew = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMarkAsNew = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsMarked = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsNewWithoutNFO = New System.Windows.Forms.CheckBox()
        Me.chkMarkAsMarkedWithoutNFO = New System.Windows.Forms.CheckBox()
        Me.gbTitleCleanup = New System.Windows.Forms.GroupBox()
        Me.tblTitleCleanup = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTitleProperCase = New System.Windows.Forms.CheckBox()
        Me.dgvTitleFilters = New System.Windows.Forms.DataGridView()
        Me.colTitleFiltersIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTitleFiltersRegex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnTitleFilterDefaults = New System.Windows.Forms.Button()
        Me.lblTitleFilters = New System.Windows.Forms.Label()
        Me.chkTitleFiltersEnabled = New System.Windows.Forms.CheckBox()
        Me.txtSkipLessThan = New System.Windows.Forms.TextBox()
        Me.lblSkipLessThanMB = New System.Windows.Forms.Label()
        Me.chkSortBeforeScan = New System.Windows.Forms.CheckBox()
        Me.chkOverwriteNfo = New System.Windows.Forms.CheckBox()
        Me.lblOverwriteNfo = New System.Windows.Forms.Label()
        Me.chkVideoSourceFromFolder = New System.Windows.Forms.CheckBox()
        Me.chkCleanLibraryAfterUpdate = New System.Windows.Forms.CheckBox()
        Me.chkDateAddedIgnoreNFO = New System.Windows.Forms.CheckBox()
        Me.lblDateAdded = New System.Windows.Forms.Label()
        Me.cbDateAddedDateTime = New System.Windows.Forms.ComboBox()
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
        Me.gbResetNew.SuspendLayout()
        Me.tblResetNew.SuspendLayout()
        Me.gbMarkNew.SuspendLayout()
        Me.tblMarkNew.SuspendLayout()
        Me.gbTitleCleanup.SuspendLayout()
        Me.tblTitleCleanup.SuspendLayout()
        CType(Me.dgvTitleFilters, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlSettings.Size = New System.Drawing.Size(910, 561)
        Me.pnlSettings.TabIndex = 0
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSettings.BackColor = System.Drawing.Color.White
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbSources, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbImportOptions, 0, 1)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(910, 561)
        Me.tblSettings.TabIndex = 0
        '
        'gbSources
        '
        Me.gbSources.AutoSize = True
        Me.gbSources.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbSources.Controls.Add(Me.tblSources)
        Me.gbSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSources.Location = New System.Drawing.Point(3, 3)
        Me.gbSources.Name = "gbSources"
        Me.gbSources.Size = New System.Drawing.Size(904, 226)
        Me.gbSources.TabIndex = 18
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
        Me.tblSources.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSources.Controls.Add(Me.gbSourcesDefaults, 0, 0)
        Me.tblSources.Controls.Add(Me.dgvSources, 0, 1)
        Me.tblSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSources.Location = New System.Drawing.Point(3, 18)
        Me.tblSources.Name = "tblSources"
        Me.tblSources.RowCount = 2
        Me.tblSources.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSources.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSources.Size = New System.Drawing.Size(898, 205)
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
        Me.gbSourcesDefaults.Size = New System.Drawing.Size(892, 48)
        Me.gbSourcesDefaults.TabIndex = 10
        Me.gbSourcesDefaults.TabStop = False
        Me.gbSourcesDefaults.Text = "Defaults for new Sources"
        '
        'tblSourcesDefaults
        '
        Me.tblSourcesDefaults.AutoSize = True
        Me.tblSourcesDefaults.ColumnCount = 2
        Me.tblSourcesDefaults.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourcesDefaults.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSourcesDefaults.Controls.Add(Me.lblSourcesDefaultsLanguage, 0, 0)
        Me.tblSourcesDefaults.Controls.Add(Me.cbSourcesDefaultsLanguage, 1, 0)
        Me.tblSourcesDefaults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSourcesDefaults.Location = New System.Drawing.Point(3, 18)
        Me.tblSourcesDefaults.Name = "tblSourcesDefaults"
        Me.tblSourcesDefaults.RowCount = 2
        Me.tblSourcesDefaults.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourcesDefaults.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSourcesDefaults.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSourcesDefaults.Size = New System.Drawing.Size(886, 27)
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
        Me.dgvSources.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSourcesStatus, Me.colSourcesID, Me.colSourcesName, Me.colSourcesPath, Me.colSourcesLanguage, Me.colSourcesRecursive, Me.colSourcesUseFolderName, Me.colSourcesIsSingle, Me.colSourcesExclude, Me.colSourcesGetYear})
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
        Me.dgvSources.Size = New System.Drawing.Size(892, 145)
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
        'colSourcesRecursive
        '
        Me.colSourcesRecursive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colSourcesRecursive.HeaderText = "Recursive"
        Me.colSourcesRecursive.Name = "colSourcesRecursive"
        Me.colSourcesRecursive.ReadOnly = True
        Me.colSourcesRecursive.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colSourcesRecursive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colSourcesRecursive.Width = 80
        '
        'colSourcesUseFolderName
        '
        Me.colSourcesUseFolderName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colSourcesUseFolderName.HeaderText = "Use Folder Name"
        Me.colSourcesUseFolderName.Name = "colSourcesUseFolderName"
        Me.colSourcesUseFolderName.ReadOnly = True
        Me.colSourcesUseFolderName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colSourcesUseFolderName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colSourcesUseFolderName.Width = 119
        '
        'colSourcesIsSingle
        '
        Me.colSourcesIsSingle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colSourcesIsSingle.HeaderText = "Single Video"
        Me.colSourcesIsSingle.Name = "colSourcesIsSingle"
        Me.colSourcesIsSingle.ReadOnly = True
        Me.colSourcesIsSingle.Width = 78
        '
        'colSourcesExclude
        '
        Me.colSourcesExclude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colSourcesExclude.HeaderText = "Exclude"
        Me.colSourcesExclude.Name = "colSourcesExclude"
        Me.colSourcesExclude.ReadOnly = True
        Me.colSourcesExclude.Width = 52
        '
        'colSourcesGetYear
        '
        Me.colSourcesGetYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colSourcesGetYear.HeaderText = "Get Year"
        Me.colSourcesGetYear.Name = "colSourcesGetYear"
        Me.colSourcesGetYear.ReadOnly = True
        Me.colSourcesGetYear.Width = 54
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
        Me.gbImportOptions.Controls.Add(Me.tblImportOptions)
        Me.gbImportOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbImportOptions.Location = New System.Drawing.Point(3, 235)
        Me.gbImportOptions.Name = "gbImportOptions"
        Me.gbImportOptions.Size = New System.Drawing.Size(904, 323)
        Me.gbImportOptions.TabIndex = 11
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
        Me.tblImportOptions.Controls.Add(Me.gbResetNew, 2, 8)
        Me.tblImportOptions.Controls.Add(Me.lblSkipLessThan, 0, 1)
        Me.tblImportOptions.Controls.Add(Me.gbMarkNew, 0, 8)
        Me.tblImportOptions.Controls.Add(Me.gbTitleCleanup, 3, 0)
        Me.tblImportOptions.Controls.Add(Me.txtSkipLessThan, 1, 1)
        Me.tblImportOptions.Controls.Add(Me.lblSkipLessThanMB, 2, 1)
        Me.tblImportOptions.Controls.Add(Me.chkSortBeforeScan, 0, 0)
        Me.tblImportOptions.Controls.Add(Me.chkOverwriteNfo, 0, 2)
        Me.tblImportOptions.Controls.Add(Me.lblOverwriteNfo, 0, 3)
        Me.tblImportOptions.Controls.Add(Me.chkVideoSourceFromFolder, 0, 4)
        Me.tblImportOptions.Controls.Add(Me.chkCleanLibraryAfterUpdate, 0, 7)
        Me.tblImportOptions.Controls.Add(Me.chkDateAddedIgnoreNFO, 0, 5)
        Me.tblImportOptions.Controls.Add(Me.lblDateAdded, 0, 6)
        Me.tblImportOptions.Controls.Add(Me.cbDateAddedDateTime, 1, 6)
        Me.tblImportOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImportOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblImportOptions.Name = "tblImportOptions"
        Me.tblImportOptions.RowCount = 9
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImportOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImportOptions.Size = New System.Drawing.Size(898, 302)
        Me.tblImportOptions.TabIndex = 9
        '
        'gbResetNew
        '
        Me.gbResetNew.AutoSize = True
        Me.gbResetNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbResetNew.Controls.Add(Me.tblResetNew)
        Me.gbResetNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbResetNew.Location = New System.Drawing.Point(234, 186)
        Me.gbResetNew.Name = "gbResetNew"
        Me.gbResetNew.Size = New System.Drawing.Size(171, 113)
        Me.gbResetNew.TabIndex = 9
        Me.gbResetNew.TabStop = False
        Me.gbResetNew.Text = "Reset marker ""New"""
        '
        'tblResetNew
        '
        Me.tblResetNew.AutoSize = True
        Me.tblResetNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblResetNew.ColumnCount = 1
        Me.tblResetNew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblResetNew.Controls.Add(Me.chkResetNewBeforeDBUpdate, 0, 0)
        Me.tblResetNew.Controls.Add(Me.chkResetNewOnExit, 0, 1)
        Me.tblResetNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblResetNew.Location = New System.Drawing.Point(3, 18)
        Me.tblResetNew.Name = "tblResetNew"
        Me.tblResetNew.RowCount = 2
        Me.tblResetNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblResetNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblResetNew.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblResetNew.Size = New System.Drawing.Size(165, 92)
        Me.tblResetNew.TabIndex = 0
        '
        'chkResetNewBeforeDBUpdate
        '
        Me.chkResetNewBeforeDBUpdate.AutoSize = True
        Me.chkResetNewBeforeDBUpdate.Location = New System.Drawing.Point(3, 3)
        Me.chkResetNewBeforeDBUpdate.Name = "chkResetNewBeforeDBUpdate"
        Me.chkResetNewBeforeDBUpdate.Size = New System.Drawing.Size(159, 17)
        Me.chkResetNewBeforeDBUpdate.TabIndex = 0
        Me.chkResetNewBeforeDBUpdate.Text = "Before any Library Update"
        Me.chkResetNewBeforeDBUpdate.UseVisualStyleBackColor = True
        '
        'chkResetNewOnExit
        '
        Me.chkResetNewOnExit.AutoSize = True
        Me.chkResetNewOnExit.Location = New System.Drawing.Point(3, 26)
        Me.chkResetNewOnExit.Name = "chkResetNewOnExit"
        Me.chkResetNewOnExit.Size = New System.Drawing.Size(63, 17)
        Me.chkResetNewOnExit.TabIndex = 0
        Me.chkResetNewOnExit.Text = "On Exit"
        Me.chkResetNewOnExit.UseVisualStyleBackColor = True
        '
        'lblSkipLessThan
        '
        Me.lblSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSkipLessThan.AutoSize = True
        Me.lblSkipLessThan.Location = New System.Drawing.Point(3, 30)
        Me.lblSkipLessThan.Name = "lblSkipLessThan"
        Me.lblSkipLessThan.Size = New System.Drawing.Size(122, 13)
        Me.lblSkipLessThan.TabIndex = 0
        Me.lblSkipLessThan.Text = "Skip files smaller than:"
        '
        'gbMarkNew
        '
        Me.gbMarkNew.AutoSize = True
        Me.gbMarkNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblImportOptions.SetColumnSpan(Me.gbMarkNew, 2)
        Me.gbMarkNew.Controls.Add(Me.tblMarkNew)
        Me.gbMarkNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMarkNew.Location = New System.Drawing.Point(3, 186)
        Me.gbMarkNew.Name = "gbMarkNew"
        Me.gbMarkNew.Size = New System.Drawing.Size(225, 113)
        Me.gbMarkNew.TabIndex = 8
        Me.gbMarkNew.TabStop = False
        Me.gbMarkNew.Text = "Mark newly added"
        '
        'tblMarkNew
        '
        Me.tblMarkNew.AutoSize = True
        Me.tblMarkNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMarkNew.ColumnCount = 1
        Me.tblMarkNew.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMarkNew.Controls.Add(Me.chkMarkAsNew, 0, 0)
        Me.tblMarkNew.Controls.Add(Me.chkMarkAsMarked, 0, 2)
        Me.tblMarkNew.Controls.Add(Me.chkMarkAsNewWithoutNFO, 0, 1)
        Me.tblMarkNew.Controls.Add(Me.chkMarkAsMarkedWithoutNFO, 0, 4)
        Me.tblMarkNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMarkNew.Location = New System.Drawing.Point(3, 18)
        Me.tblMarkNew.Name = "tblMarkNew"
        Me.tblMarkNew.RowCount = 5
        Me.tblMarkNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMarkNew.Size = New System.Drawing.Size(219, 92)
        Me.tblMarkNew.TabIndex = 0
        '
        'chkMarkAsNew
        '
        Me.chkMarkAsNew.AutoSize = True
        Me.chkMarkAsNew.Location = New System.Drawing.Point(3, 3)
        Me.chkMarkAsNew.Name = "chkMarkAsNew"
        Me.chkMarkAsNew.Size = New System.Drawing.Size(100, 17)
        Me.chkMarkAsNew.TabIndex = 0
        Me.chkMarkAsNew.Text = "Mark as ""New"""
        Me.chkMarkAsNew.UseVisualStyleBackColor = True
        '
        'chkMarkAsMarked
        '
        Me.chkMarkAsMarked.AutoSize = True
        Me.chkMarkAsMarked.Location = New System.Drawing.Point(3, 49)
        Me.chkMarkAsMarked.Name = "chkMarkAsMarked"
        Me.chkMarkAsMarked.Size = New System.Drawing.Size(116, 17)
        Me.chkMarkAsMarked.TabIndex = 0
        Me.chkMarkAsMarked.Text = "Mark as ""Marked"""
        Me.chkMarkAsMarked.UseVisualStyleBackColor = True
        '
        'chkMarkAsNewWithoutNFO
        '
        Me.chkMarkAsNewWithoutNFO.AutoSize = True
        Me.chkMarkAsNewWithoutNFO.Location = New System.Drawing.Point(3, 26)
        Me.chkMarkAsNewWithoutNFO.Name = "chkMarkAsNewWithoutNFO"
        Me.chkMarkAsNewWithoutNFO.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMarkAsNewWithoutNFO.Size = New System.Drawing.Size(181, 17)
        Me.chkMarkAsNewWithoutNFO.TabIndex = 0
        Me.chkMarkAsNewWithoutNFO.Text = "Only if no valid NFO exists"
        Me.chkMarkAsNewWithoutNFO.UseVisualStyleBackColor = True
        '
        'chkMarkAsMarkedWithoutNFO
        '
        Me.chkMarkAsMarkedWithoutNFO.AutoSize = True
        Me.chkMarkAsMarkedWithoutNFO.Location = New System.Drawing.Point(3, 72)
        Me.chkMarkAsMarkedWithoutNFO.Name = "chkMarkAsMarkedWithoutNFO"
        Me.chkMarkAsMarkedWithoutNFO.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMarkAsMarkedWithoutNFO.Size = New System.Drawing.Size(181, 17)
        Me.chkMarkAsMarkedWithoutNFO.TabIndex = 0
        Me.chkMarkAsMarkedWithoutNFO.Text = "Only if no valid NFO exists"
        Me.chkMarkAsMarkedWithoutNFO.UseVisualStyleBackColor = True
        '
        'gbTitleCleanup
        '
        Me.gbTitleCleanup.AutoSize = True
        Me.gbTitleCleanup.Controls.Add(Me.tblTitleCleanup)
        Me.gbTitleCleanup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTitleCleanup.Location = New System.Drawing.Point(411, 3)
        Me.gbTitleCleanup.Name = "gbTitleCleanup"
        Me.tblImportOptions.SetRowSpan(Me.gbTitleCleanup, 9)
        Me.gbTitleCleanup.Size = New System.Drawing.Size(484, 296)
        Me.gbTitleCleanup.TabIndex = 12
        Me.gbTitleCleanup.TabStop = False
        Me.gbTitleCleanup.Text = "Title Cleanup"
        '
        'tblTitleCleanup
        '
        Me.tblTitleCleanup.AutoSize = True
        Me.tblTitleCleanup.ColumnCount = 2
        Me.tblTitleCleanup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleCleanup.Controls.Add(Me.chkTitleProperCase, 0, 0)
        Me.tblTitleCleanup.Controls.Add(Me.dgvTitleFilters, 0, 2)
        Me.tblTitleCleanup.Controls.Add(Me.btnTitleFilterDefaults, 1, 3)
        Me.tblTitleCleanup.Controls.Add(Me.lblTitleFilters, 0, 3)
        Me.tblTitleCleanup.Controls.Add(Me.chkTitleFiltersEnabled, 0, 1)
        Me.tblTitleCleanup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTitleCleanup.Location = New System.Drawing.Point(3, 18)
        Me.tblTitleCleanup.Name = "tblTitleCleanup"
        Me.tblTitleCleanup.RowCount = 4
        Me.tblTitleCleanup.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblTitleCleanup.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleCleanup.Size = New System.Drawing.Size(478, 275)
        Me.tblTitleCleanup.TabIndex = 10
        '
        'chkTitleProperCase
        '
        Me.chkTitleProperCase.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleProperCase.AutoSize = True
        Me.tblTitleCleanup.SetColumnSpan(Me.chkTitleProperCase, 2)
        Me.chkTitleProperCase.Location = New System.Drawing.Point(3, 3)
        Me.chkTitleProperCase.Name = "chkTitleProperCase"
        Me.chkTitleProperCase.Size = New System.Drawing.Size(168, 17)
        Me.chkTitleProperCase.TabIndex = 0
        Me.chkTitleProperCase.Text = "Convert Title to Proper Case"
        Me.chkTitleProperCase.UseVisualStyleBackColor = True
        '
        'dgvTitleFilters
        '
        Me.dgvTitleFilters.AllowUserToResizeRows = False
        Me.dgvTitleFilters.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvTitleFilters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTitleFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTitleFilters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colTitleFiltersIndex, Me.colTitleFiltersRegex})
        Me.tblTitleCleanup.SetColumnSpan(Me.dgvTitleFilters, 2)
        Me.dgvTitleFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTitleFilters.Location = New System.Drawing.Point(3, 49)
        Me.dgvTitleFilters.Name = "dgvTitleFilters"
        Me.dgvTitleFilters.RowHeadersWidth = 25
        Me.dgvTitleFilters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTitleFilters.ShowCellErrors = False
        Me.dgvTitleFilters.ShowCellToolTips = False
        Me.dgvTitleFilters.ShowRowErrors = False
        Me.dgvTitleFilters.Size = New System.Drawing.Size(472, 194)
        Me.dgvTitleFilters.TabIndex = 7
        '
        'colTitleFiltersIndex
        '
        Me.colTitleFiltersIndex.HeaderText = "Index"
        Me.colTitleFiltersIndex.Name = "colTitleFiltersIndex"
        Me.colTitleFiltersIndex.Visible = False
        '
        'colTitleFiltersRegex
        '
        Me.colTitleFiltersRegex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTitleFiltersRegex.HeaderText = "Regex"
        Me.colTitleFiltersRegex.Name = "colTitleFiltersRegex"
        '
        'btnTitleFilterDefaults
        '
        Me.btnTitleFilterDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTitleFilterDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTitleFilterDefaults.Image = CType(resources.GetObject("btnTitleFilterDefaults.Image"), System.Drawing.Image)
        Me.btnTitleFilterDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTitleFilterDefaults.Location = New System.Drawing.Point(370, 249)
        Me.btnTitleFilterDefaults.Name = "btnTitleFilterDefaults"
        Me.btnTitleFilterDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnTitleFilterDefaults.TabIndex = 5
        Me.btnTitleFilterDefaults.Text = "Defaults"
        Me.btnTitleFilterDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTitleFilterDefaults.UseVisualStyleBackColor = True
        '
        'lblTitleFilters
        '
        Me.lblTitleFilters.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitleFilters.AutoSize = True
        Me.lblTitleFilters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleFilters.Location = New System.Drawing.Point(3, 254)
        Me.lblTitleFilters.Name = "lblTitleFilters"
        Me.lblTitleFilters.Size = New System.Drawing.Size(201, 13)
        Me.lblTitleFilters.TabIndex = 6
        Me.lblTitleFilters.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'chkTitleFiltersEnabled
        '
        Me.chkTitleFiltersEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleFiltersEnabled.AutoSize = True
        Me.tblTitleCleanup.SetColumnSpan(Me.chkTitleFiltersEnabled, 2)
        Me.chkTitleFiltersEnabled.Location = New System.Drawing.Point(3, 26)
        Me.chkTitleFiltersEnabled.Name = "chkTitleFiltersEnabled"
        Me.chkTitleFiltersEnabled.Size = New System.Drawing.Size(119, 17)
        Me.chkTitleFiltersEnabled.TabIndex = 0
        Me.chkTitleFiltersEnabled.Text = "Enable Title Filters"
        Me.chkTitleFiltersEnabled.UseVisualStyleBackColor = True
        '
        'txtSkipLessThan
        '
        Me.txtSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSkipLessThan.Location = New System.Drawing.Point(177, 26)
        Me.txtSkipLessThan.Name = "txtSkipLessThan"
        Me.txtSkipLessThan.Size = New System.Drawing.Size(51, 22)
        Me.txtSkipLessThan.TabIndex = 1
        '
        'lblSkipLessThanMB
        '
        Me.lblSkipLessThanMB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSkipLessThanMB.AutoSize = True
        Me.lblSkipLessThanMB.Location = New System.Drawing.Point(234, 30)
        Me.lblSkipLessThanMB.Name = "lblSkipLessThanMB"
        Me.lblSkipLessThanMB.Size = New System.Drawing.Size(24, 13)
        Me.lblSkipLessThanMB.TabIndex = 2
        Me.lblSkipLessThanMB.Text = "MB"
        '
        'chkSortBeforeScan
        '
        Me.chkSortBeforeScan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkSortBeforeScan.AutoSize = True
        Me.tblImportOptions.SetColumnSpan(Me.chkSortBeforeScan, 3)
        Me.chkSortBeforeScan.Location = New System.Drawing.Point(3, 3)
        Me.chkSortBeforeScan.Name = "chkSortBeforeScan"
        Me.chkSortBeforeScan.Size = New System.Drawing.Size(276, 17)
        Me.chkSortBeforeScan.TabIndex = 6
        Me.chkSortBeforeScan.Text = "Sort files into folders before each Library Update"
        Me.chkSortBeforeScan.UseVisualStyleBackColor = True
        '
        'chkOverwriteNfo
        '
        Me.chkOverwriteNfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOverwriteNfo.AutoSize = True
        Me.tblImportOptions.SetColumnSpan(Me.chkOverwriteNfo, 3)
        Me.chkOverwriteNfo.Location = New System.Drawing.Point(3, 54)
        Me.chkOverwriteNfo.Name = "chkOverwriteNfo"
        Me.chkOverwriteNfo.Size = New System.Drawing.Size(144, 17)
        Me.chkOverwriteNfo.TabIndex = 12
        Me.chkOverwriteNfo.Text = "Overwrite invalid NFOs"
        Me.chkOverwriteNfo.UseVisualStyleBackColor = True
        '
        'lblOverwriteNfo
        '
        Me.lblOverwriteNfo.AutoSize = True
        Me.tblImportOptions.SetColumnSpan(Me.lblOverwriteNfo, 3)
        Me.lblOverwriteNfo.Location = New System.Drawing.Point(3, 74)
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
        Me.tblImportOptions.SetColumnSpan(Me.chkVideoSourceFromFolder, 3)
        Me.chkVideoSourceFromFolder.Location = New System.Drawing.Point(3, 90)
        Me.chkVideoSourceFromFolder.Name = "chkVideoSourceFromFolder"
        Me.chkVideoSourceFromFolder.Size = New System.Drawing.Size(290, 17)
        Me.chkVideoSourceFromFolder.TabIndex = 11
        Me.chkVideoSourceFromFolder.Text = "Search in the full path for VideoSource information"
        Me.chkVideoSourceFromFolder.UseVisualStyleBackColor = True
        '
        'chkCleanLibraryAfterUpdate
        '
        Me.chkCleanLibraryAfterUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCleanLibraryAfterUpdate.AutoSize = True
        Me.tblImportOptions.SetColumnSpan(Me.chkCleanLibraryAfterUpdate, 3)
        Me.chkCleanLibraryAfterUpdate.Location = New System.Drawing.Point(3, 163)
        Me.chkCleanLibraryAfterUpdate.Name = "chkCleanLibraryAfterUpdate"
        Me.chkCleanLibraryAfterUpdate.Size = New System.Drawing.Size(210, 17)
        Me.chkCleanLibraryAfterUpdate.TabIndex = 9
        Me.chkCleanLibraryAfterUpdate.Text = "Clean database after Library Update"
        Me.chkCleanLibraryAfterUpdate.UseVisualStyleBackColor = True
        '
        'chkDateAddedIgnoreNFO
        '
        Me.chkDateAddedIgnoreNFO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDateAddedIgnoreNFO.AutoSize = True
        Me.tblImportOptions.SetColumnSpan(Me.chkDateAddedIgnoreNFO, 3)
        Me.chkDateAddedIgnoreNFO.Location = New System.Drawing.Point(3, 113)
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
        Me.lblDateAdded.Location = New System.Drawing.Point(3, 140)
        Me.lblDateAdded.Name = "lblDateAdded"
        Me.lblDateAdded.Size = New System.Drawing.Size(168, 13)
        Me.lblDateAdded.TabIndex = 14
        Me.lblDateAdded.Text = "Default value for <dateadded>"
        '
        'cbDateAddedDateTime
        '
        Me.tblImportOptions.SetColumnSpan(Me.cbDateAddedDateTime, 2)
        Me.cbDateAddedDateTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbDateAddedDateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDateAddedDateTime.FormattingEnabled = True
        Me.cbDateAddedDateTime.Location = New System.Drawing.Point(177, 136)
        Me.cbDateAddedDateTime.Name = "cbDateAddedDateTime"
        Me.cbDateAddedDateTime.Size = New System.Drawing.Size(228, 21)
        Me.cbDateAddedDateTime.TabIndex = 11
        '
        'frmMovie_Source
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(910, 561)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmMovie_Source"
        Me.Text = "frmMovie_Source"
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
        Me.gbResetNew.ResumeLayout(False)
        Me.gbResetNew.PerformLayout()
        Me.tblResetNew.ResumeLayout(False)
        Me.tblResetNew.PerformLayout()
        Me.gbMarkNew.ResumeLayout(False)
        Me.gbMarkNew.PerformLayout()
        Me.tblMarkNew.ResumeLayout(False)
        Me.tblMarkNew.PerformLayout()
        Me.gbTitleCleanup.ResumeLayout(False)
        Me.gbTitleCleanup.PerformLayout()
        Me.tblTitleCleanup.ResumeLayout(False)
        Me.tblTitleCleanup.PerformLayout()
        CType(Me.dgvTitleFilters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbSourcesDefaults As GroupBox
    Friend WithEvents tblSourcesDefaults As TableLayoutPanel
    Friend WithEvents cbSourcesDefaultsLanguage As ComboBox
    Friend WithEvents lblSourcesDefaultsLanguage As Label
    Friend WithEvents gbImportOptions As GroupBox
    Friend WithEvents tblImportOptions As TableLayoutPanel
    Friend WithEvents chkCleanLibraryAfterUpdate As CheckBox
    Friend WithEvents lblSkipLessThan As Label
    Friend WithEvents chkSortBeforeScan As CheckBox
    Friend WithEvents txtSkipLessThan As TextBox
    Friend WithEvents lblSkipLessThanMB As Label
    Friend WithEvents gbTitleCleanup As GroupBox
    Friend WithEvents tblTitleCleanup As TableLayoutPanel
    Friend WithEvents chkTitleProperCase As CheckBox
    Friend WithEvents chkDateAddedIgnoreNFO As CheckBox
    Friend WithEvents chkVideoSourceFromFolder As CheckBox
    Friend WithEvents chkOverwriteNfo As CheckBox
    Friend WithEvents lblOverwriteNfo As Label
    Friend WithEvents btnTitleFilterDefaults As Button
    Friend WithEvents lblTitleFilters As Label
    Friend WithEvents dgvTitleFilters As DataGridView
    Friend WithEvents gbResetNew As GroupBox
    Friend WithEvents tblResetNew As TableLayoutPanel
    Friend WithEvents chkResetNewBeforeDBUpdate As CheckBox
    Friend WithEvents chkResetNewOnExit As CheckBox
    Friend WithEvents chkMarkAsNewWithoutNFO As CheckBox
    Friend WithEvents gbMarkNew As GroupBox
    Friend WithEvents tblMarkNew As TableLayoutPanel
    Friend WithEvents chkMarkAsNew As CheckBox
    Friend WithEvents chkMarkAsMarked As CheckBox
    Friend WithEvents gbSources As GroupBox
    Friend WithEvents tblSources As TableLayoutPanel
    Friend WithEvents dgvSources As DataGridView
    Friend WithEvents colTitleFiltersIndex As DataGridViewTextBoxColumn
    Friend WithEvents colTitleFiltersRegex As DataGridViewTextBoxColumn
    Friend WithEvents cmnuSources As ContextMenuStrip
    Friend WithEvents cmnuSourcesAdd As ToolStripMenuItem
    Friend WithEvents cmnuSourcesEdit As ToolStripMenuItem
    Friend WithEvents cmnuSourcesMarkToRemove As ToolStripMenuItem
    Friend WithEvents cmnuSourcesReject As ToolStripMenuItem
    Friend WithEvents colSourcesState As DataGridViewTextBoxColumn
    Friend WithEvents lblDateAdded As Label
    Friend WithEvents colSourcesStatus As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesID As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesName As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesPath As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesLanguage As DataGridViewTextBoxColumn
    Friend WithEvents colSourcesRecursive As DataGridViewCheckBoxColumn
    Friend WithEvents colSourcesUseFolderName As DataGridViewCheckBoxColumn
    Friend WithEvents colSourcesIsSingle As DataGridViewCheckBoxColumn
    Friend WithEvents colSourcesExclude As DataGridViewCheckBoxColumn
    Friend WithEvents colSourcesGetYear As DataGridViewCheckBoxColumn
    Friend WithEvents chkTitleFiltersEnabled As CheckBox
    Friend WithEvents cbDateAddedDateTime As ComboBox
    Friend WithEvents chkMarkAsMarkedWithoutNFO As CheckBox
End Class
