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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovie_Source))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieGeneralFiltersOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieGeneralFiltersOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTitleProperCase = New System.Windows.Forms.CheckBox()
        Me.gbImportOptions = New System.Windows.Forms.GroupBox()
        Me.tblMovieSourcesMiscOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMarkNew = New System.Windows.Forms.CheckBox()
        Me.chkCleanDB = New System.Windows.Forms.CheckBox()
        Me.lblMovieSkipLessThan = New System.Windows.Forms.Label()
        Me.chkSortBeforeScan = New System.Windows.Forms.CheckBox()
        Me.txtSkipLessThan = New System.Windows.Forms.TextBox()
        Me.lblMovieSkipLessThanMB = New System.Windows.Forms.Label()
        Me.chkMovieSkipStackedSizeCheck = New System.Windows.Forms.CheckBox()
        Me.chkVideoSourceFromFolder = New System.Windows.Forms.CheckBox()
        Me.chkOverwriteNfo = New System.Windows.Forms.CheckBox()
        Me.lblGeneralOverwriteNfo = New System.Windows.Forms.Label()
        Me.gbSourcesDefaults = New System.Windows.Forms.GroupBox()
        Me.tblMovieSourcesDefaultsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMovieSourcesDefaultsLanguage = New System.Windows.Forms.Label()
        Me.cbMovieGeneralLang = New System.Windows.Forms.ComboBox()
        Me.btnMovieSourceRemove = New System.Windows.Forms.Button()
        Me.btnMovieSourceEdit = New System.Windows.Forms.Button()
        Me.btnMovieSourceAdd = New System.Windows.Forms.Button()
        Me.gbGeneralDateAdded = New System.Windows.Forms.GroupBox()
        Me.tblGeneralDateAdded = New System.Windows.Forms.TableLayoutPanel()
        Me.chkDateAddedIgnoreNFO = New System.Windows.Forms.CheckBox()
        Me.cbDateAddedDateTime = New System.Windows.Forms.ComboBox()
        Me.lvMovieSources = New System.Windows.Forms.ListView()
        Me.colMovieSourcesID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSourcesName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSourcesPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSourcesLanguage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSourcesRecur = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSourcesFolder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSourcesSingle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSourcesExclude = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSourcesGetYear = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnTitleFilterDefaults = New System.Windows.Forms.Button()
        Me.lblTitleFilter = New System.Windows.Forms.Label()
        Me.dgvTitleFilter = New System.Windows.Forms.DataGridView()
        Me.colVideoExtensions = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMovieGeneralFiltersOpts.SuspendLayout()
        Me.tblMovieGeneralFiltersOpts.SuspendLayout()
        Me.gbImportOptions.SuspendLayout()
        Me.tblMovieSourcesMiscOpts.SuspendLayout()
        Me.gbSourcesDefaults.SuspendLayout()
        Me.tblMovieSourcesDefaultsOpts.SuspendLayout()
        Me.gbGeneralDateAdded.SuspendLayout()
        Me.tblGeneralDateAdded.SuspendLayout()
        CType(Me.dgvTitleFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.dgvTitleFilter)
        Me.pnlSettings.Controls.Add(Me.btnTitleFilterDefaults)
        Me.pnlSettings.Controls.Add(Me.lblTitleFilter)
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(1185, 858)
        Me.pnlSettings.TabIndex = 0
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.BackColor = System.Drawing.Color.White
        Me.tblSettings.ColumnCount = 3
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbMovieGeneralFiltersOpts, 0, 5)
        Me.tblSettings.Controls.Add(Me.gbImportOptions, 0, 4)
        Me.tblSettings.Controls.Add(Me.gbSourcesDefaults, 0, 0)
        Me.tblSettings.Controls.Add(Me.btnMovieSourceRemove, 1, 3)
        Me.tblSettings.Controls.Add(Me.btnMovieSourceEdit, 1, 2)
        Me.tblSettings.Controls.Add(Me.btnMovieSourceAdd, 1, 1)
        Me.tblSettings.Controls.Add(Me.gbGeneralDateAdded, 1, 4)
        Me.tblSettings.Controls.Add(Me.lvMovieSources, 0, 1)
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 7
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(970, 445)
        Me.tblSettings.TabIndex = 0
        '
        'gbMovieGeneralFiltersOpts
        '
        Me.gbMovieGeneralFiltersOpts.AutoSize = True
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.tblMovieGeneralFiltersOpts)
        Me.gbMovieGeneralFiltersOpts.Location = New System.Drawing.Point(3, 398)
        Me.gbMovieGeneralFiltersOpts.Name = "gbMovieGeneralFiltersOpts"
        Me.gbMovieGeneralFiltersOpts.Size = New System.Drawing.Size(180, 44)
        Me.gbMovieGeneralFiltersOpts.TabIndex = 12
        Me.gbMovieGeneralFiltersOpts.TabStop = False
        Me.gbMovieGeneralFiltersOpts.Text = "Folder/File Name Filters"
        '
        'tblMovieGeneralFiltersOpts
        '
        Me.tblMovieGeneralFiltersOpts.AutoSize = True
        Me.tblMovieGeneralFiltersOpts.ColumnCount = 6
        Me.tblMovieGeneralFiltersOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralFiltersOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralFiltersOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralFiltersOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralFiltersOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralFiltersOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.chkTitleProperCase, 0, 0)
        Me.tblMovieGeneralFiltersOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralFiltersOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralFiltersOpts.Name = "tblMovieGeneralFiltersOpts"
        Me.tblMovieGeneralFiltersOpts.RowCount = 4
        Me.tblMovieGeneralFiltersOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralFiltersOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralFiltersOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralFiltersOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralFiltersOpts.Size = New System.Drawing.Size(174, 23)
        Me.tblMovieGeneralFiltersOpts.TabIndex = 10
        '
        'chkProperCase
        '
        Me.chkTitleProperCase.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleProperCase.AutoSize = True
        Me.tblMovieGeneralFiltersOpts.SetColumnSpan(Me.chkTitleProperCase, 4)
        Me.chkTitleProperCase.Location = New System.Drawing.Point(3, 3)
        Me.chkTitleProperCase.Name = "chkProperCase"
        Me.chkTitleProperCase.Size = New System.Drawing.Size(168, 17)
        Me.chkTitleProperCase.TabIndex = 0
        Me.chkTitleProperCase.Text = "Convert Title to Proper Case"
        Me.chkTitleProperCase.UseVisualStyleBackColor = True
        '
        'gbImportOptions
        '
        Me.gbImportOptions.AutoSize = True
        Me.gbImportOptions.Controls.Add(Me.tblMovieSourcesMiscOpts)
        Me.gbImportOptions.Location = New System.Drawing.Point(3, 251)
        Me.gbImportOptions.Name = "gbImportOptions"
        Me.gbImportOptions.Size = New System.Drawing.Size(693, 141)
        Me.gbImportOptions.TabIndex = 11
        Me.gbImportOptions.TabStop = False
        Me.gbImportOptions.Text = "Import Options"
        '
        'tblMovieSourcesMiscOpts
        '
        Me.tblMovieSourcesMiscOpts.AutoSize = True
        Me.tblMovieSourcesMiscOpts.ColumnCount = 4
        Me.tblMovieSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkMarkNew, 0, 4)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkCleanDB, 0, 3)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.lblMovieSkipLessThan, 0, 0)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkSortBeforeScan, 0, 2)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.txtSkipLessThan, 1, 0)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.lblMovieSkipLessThanMB, 2, 0)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkMovieSkipStackedSizeCheck, 0, 1)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkVideoSourceFromFolder, 3, 4)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkOverwriteNfo, 3, 2)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.lblGeneralOverwriteNfo, 3, 3)
        Me.tblMovieSourcesMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSourcesMiscOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSourcesMiscOpts.Name = "tblMovieSourcesMiscOpts"
        Me.tblMovieSourcesMiscOpts.RowCount = 5
        Me.tblMovieSourcesMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSourcesMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSourcesMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSourcesMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSourcesMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSourcesMiscOpts.Size = New System.Drawing.Size(687, 120)
        Me.tblMovieSourcesMiscOpts.TabIndex = 9
        '
        'chkMarkNew
        '
        Me.chkMarkNew.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMarkNew.AutoSize = True
        Me.chkMarkNew.Location = New System.Drawing.Point(3, 100)
        Me.chkMarkNew.Name = "chkMarkNew"
        Me.chkMarkNew.Size = New System.Drawing.Size(117, 17)
        Me.chkMarkNew.TabIndex = 10
        Me.chkMarkNew.Text = "Mark New Movies"
        Me.chkMarkNew.UseVisualStyleBackColor = True
        '
        'chkCleanDB
        '
        Me.chkCleanDB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCleanDB.AutoSize = True
        Me.tblMovieSourcesMiscOpts.SetColumnSpan(Me.chkCleanDB, 3)
        Me.chkCleanDB.Location = New System.Drawing.Point(3, 77)
        Me.chkCleanDB.Name = "chkCleanDB"
        Me.chkCleanDB.Size = New System.Drawing.Size(218, 17)
        Me.chkCleanDB.TabIndex = 9
        Me.chkCleanDB.Text = "Clean database after updating library"
        Me.chkCleanDB.UseVisualStyleBackColor = True
        '
        'lblMovieSkipLessThan
        '
        Me.lblMovieSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSkipLessThan.AutoSize = True
        Me.lblMovieSkipLessThan.Location = New System.Drawing.Point(3, 7)
        Me.lblMovieSkipLessThan.Name = "lblMovieSkipLessThan"
        Me.lblMovieSkipLessThan.Size = New System.Drawing.Size(122, 13)
        Me.lblMovieSkipLessThan.TabIndex = 0
        Me.lblMovieSkipLessThan.Text = "Skip files smaller than:"
        '
        'chkSortBeforeScan
        '
        Me.chkSortBeforeScan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkSortBeforeScan.AutoSize = True
        Me.tblMovieSourcesMiscOpts.SetColumnSpan(Me.chkSortBeforeScan, 3)
        Me.chkSortBeforeScan.Location = New System.Drawing.Point(3, 54)
        Me.chkSortBeforeScan.Name = "chkSortBeforeScan"
        Me.chkSortBeforeScan.Size = New System.Drawing.Size(273, 17)
        Me.chkSortBeforeScan.TabIndex = 6
        Me.chkSortBeforeScan.Text = "Sort files into folders before each library update"
        Me.chkSortBeforeScan.UseVisualStyleBackColor = True
        '
        'txtSkipLessThan
        '
        Me.txtSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSkipLessThan.Location = New System.Drawing.Point(131, 3)
        Me.txtSkipLessThan.Name = "txtSkipLessThan"
        Me.txtSkipLessThan.Size = New System.Drawing.Size(51, 22)
        Me.txtSkipLessThan.TabIndex = 1
        '
        'lblMovieSkipLessThanMB
        '
        Me.lblMovieSkipLessThanMB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSkipLessThanMB.AutoSize = True
        Me.lblMovieSkipLessThanMB.Location = New System.Drawing.Point(188, 7)
        Me.lblMovieSkipLessThanMB.Name = "lblMovieSkipLessThanMB"
        Me.lblMovieSkipLessThanMB.Size = New System.Drawing.Size(24, 13)
        Me.lblMovieSkipLessThanMB.TabIndex = 2
        Me.lblMovieSkipLessThanMB.Text = "MB"
        '
        'chkMovieSkipStackedSizeCheck
        '
        Me.chkMovieSkipStackedSizeCheck.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSkipStackedSizeCheck.AutoSize = True
        Me.tblMovieSourcesMiscOpts.SetColumnSpan(Me.chkMovieSkipStackedSizeCheck, 3)
        Me.chkMovieSkipStackedSizeCheck.Location = New System.Drawing.Point(3, 31)
        Me.chkMovieSkipStackedSizeCheck.Name = "chkMovieSkipStackedSizeCheck"
        Me.chkMovieSkipStackedSizeCheck.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMovieSkipStackedSizeCheck.Size = New System.Drawing.Size(208, 17)
        Me.chkMovieSkipStackedSizeCheck.TabIndex = 3
        Me.chkMovieSkipStackedSizeCheck.Text = "Skip Size Check of Stacked Files"
        Me.chkMovieSkipStackedSizeCheck.UseVisualStyleBackColor = True
        '
        'chkSourceFromFolder
        '
        Me.chkVideoSourceFromFolder.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkVideoSourceFromFolder.AutoSize = True
        Me.chkVideoSourceFromFolder.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkVideoSourceFromFolder.Location = New System.Drawing.Point(282, 100)
        Me.chkVideoSourceFromFolder.Name = "chkSourceFromFolder"
        Me.chkVideoSourceFromFolder.Size = New System.Drawing.Size(290, 17)
        Me.chkVideoSourceFromFolder.TabIndex = 11
        Me.chkVideoSourceFromFolder.Text = "Search in the full path for VideoSource information"
        Me.chkVideoSourceFromFolder.UseVisualStyleBackColor = True
        '
        'chkOverwriteNfo
        '
        Me.chkOverwriteNfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOverwriteNfo.AutoSize = True
        Me.chkOverwriteNfo.Location = New System.Drawing.Point(282, 54)
        Me.chkOverwriteNfo.Name = "chkOverwriteNfo"
        Me.chkOverwriteNfo.Size = New System.Drawing.Size(191, 17)
        Me.chkOverwriteNfo.TabIndex = 12
        Me.chkOverwriteNfo.Text = "Overwrite Non-conforming nfos"
        Me.chkOverwriteNfo.UseVisualStyleBackColor = True
        '
        'lblGeneralOverwriteNfo
        '
        Me.lblGeneralOverwriteNfo.AutoSize = True
        Me.lblGeneralOverwriteNfo.Location = New System.Drawing.Point(282, 74)
        Me.lblGeneralOverwriteNfo.Name = "lblGeneralOverwriteNfo"
        Me.lblGeneralOverwriteNfo.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblGeneralOverwriteNfo.Size = New System.Drawing.Size(402, 13)
        Me.lblGeneralOverwriteNfo.TabIndex = 13
        Me.lblGeneralOverwriteNfo.Text = "(If unchecked, non-conforming nfos will be renamed to <filename>.info)"
        Me.lblGeneralOverwriteNfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbSourcesDefaults
        '
        Me.gbSourcesDefaults.AutoSize = True
        Me.tblSettings.SetColumnSpan(Me.gbSourcesDefaults, 2)
        Me.gbSourcesDefaults.Controls.Add(Me.tblMovieSourcesDefaultsOpts)
        Me.gbSourcesDefaults.Location = New System.Drawing.Point(3, 3)
        Me.gbSourcesDefaults.Name = "gbSourcesDefaults"
        Me.gbSourcesDefaults.Size = New System.Drawing.Size(280, 48)
        Me.gbSourcesDefaults.TabIndex = 10
        Me.gbSourcesDefaults.TabStop = False
        Me.gbSourcesDefaults.Text = "Defaults for new Sources"
        '
        'tblMovieSourcesDefaultsOpts
        '
        Me.tblMovieSourcesDefaultsOpts.AutoSize = True
        Me.tblMovieSourcesDefaultsOpts.ColumnCount = 2
        Me.tblMovieSourcesDefaultsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesDefaultsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesDefaultsOpts.Controls.Add(Me.lblMovieSourcesDefaultsLanguage, 0, 0)
        Me.tblMovieSourcesDefaultsOpts.Controls.Add(Me.cbMovieGeneralLang, 1, 0)
        Me.tblMovieSourcesDefaultsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSourcesDefaultsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSourcesDefaultsOpts.Name = "tblMovieSourcesDefaultsOpts"
        Me.tblMovieSourcesDefaultsOpts.RowCount = 2
        Me.tblMovieSourcesDefaultsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSourcesDefaultsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSourcesDefaultsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSourcesDefaultsOpts.Size = New System.Drawing.Size(274, 27)
        Me.tblMovieSourcesDefaultsOpts.TabIndex = 0
        '
        'lblMovieSourcesDefaultsLanguage
        '
        Me.lblMovieSourcesDefaultsLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSourcesDefaultsLanguage.AutoSize = True
        Me.lblMovieSourcesDefaultsLanguage.Location = New System.Drawing.Point(3, 7)
        Me.lblMovieSourcesDefaultsLanguage.Name = "lblMovieSourcesDefaultsLanguage"
        Me.lblMovieSourcesDefaultsLanguage.Size = New System.Drawing.Size(102, 13)
        Me.lblMovieSourcesDefaultsLanguage.TabIndex = 8
        Me.lblMovieSourcesDefaultsLanguage.Text = "Default Language:"
        Me.lblMovieSourcesDefaultsLanguage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbMovieGeneralLang
        '
        Me.cbMovieGeneralLang.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbMovieGeneralLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieGeneralLang.Location = New System.Drawing.Point(111, 3)
        Me.cbMovieGeneralLang.Name = "cbMovieGeneralLang"
        Me.cbMovieGeneralLang.Size = New System.Drawing.Size(160, 21)
        Me.cbMovieGeneralLang.TabIndex = 12
        '
        'btnMovieSourceRemove
        '
        Me.btnMovieSourceRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMovieSourceRemove.Image = CType(resources.GetObject("btnMovieSourceRemove.Image"), System.Drawing.Image)
        Me.btnMovieSourceRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceRemove.Location = New System.Drawing.Point(729, 222)
        Me.btnMovieSourceRemove.Name = "btnMovieSourceRemove"
        Me.btnMovieSourceRemove.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceRemove.TabIndex = 4
        Me.btnMovieSourceRemove.Text = "Remove"
        Me.btnMovieSourceRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceRemove.UseVisualStyleBackColor = True
        '
        'btnMovieSourceEdit
        '
        Me.btnMovieSourceEdit.Image = CType(resources.GetObject("btnMovieSourceEdit.Image"), System.Drawing.Image)
        Me.btnMovieSourceEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceEdit.Location = New System.Drawing.Point(729, 86)
        Me.btnMovieSourceEdit.Name = "btnMovieSourceEdit"
        Me.btnMovieSourceEdit.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceEdit.TabIndex = 3
        Me.btnMovieSourceEdit.Text = "Edit Source"
        Me.btnMovieSourceEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceEdit.UseVisualStyleBackColor = True
        '
        'btnMovieSourceAdd
        '
        Me.btnMovieSourceAdd.Image = CType(resources.GetObject("btnMovieSourceAdd.Image"), System.Drawing.Image)
        Me.btnMovieSourceAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceAdd.Location = New System.Drawing.Point(729, 57)
        Me.btnMovieSourceAdd.Name = "btnMovieSourceAdd"
        Me.btnMovieSourceAdd.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceAdd.TabIndex = 2
        Me.btnMovieSourceAdd.Text = "Add Source"
        Me.btnMovieSourceAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceAdd.UseVisualStyleBackColor = True
        '
        'gbGeneralDateAdded
        '
        Me.gbGeneralDateAdded.AutoSize = True
        Me.gbGeneralDateAdded.Controls.Add(Me.tblGeneralDateAdded)
        Me.gbGeneralDateAdded.Location = New System.Drawing.Point(729, 251)
        Me.gbGeneralDateAdded.Name = "gbGeneralDateAdded"
        Me.gbGeneralDateAdded.Size = New System.Drawing.Size(238, 71)
        Me.gbGeneralDateAdded.TabIndex = 17
        Me.gbGeneralDateAdded.TabStop = False
        Me.gbGeneralDateAdded.Text = "Adding Date"
        '
        'tblGeneralDateAdded
        '
        Me.tblGeneralDateAdded.AutoSize = True
        Me.tblGeneralDateAdded.ColumnCount = 2
        Me.tblGeneralDateAdded.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDateAdded.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralDateAdded.Controls.Add(Me.chkDateAddedIgnoreNFO, 0, 1)
        Me.tblGeneralDateAdded.Controls.Add(Me.cbDateAddedDateTime, 0, 0)
        Me.tblGeneralDateAdded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralDateAdded.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralDateAdded.Name = "tblGeneralDateAdded"
        Me.tblGeneralDateAdded.RowCount = 3
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralDateAdded.Size = New System.Drawing.Size(232, 50)
        Me.tblGeneralDateAdded.TabIndex = 17
        '
        'chkGeneralDateAddedIgnoreNFO
        '
        Me.chkDateAddedIgnoreNFO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDateAddedIgnoreNFO.AutoSize = True
        Me.chkDateAddedIgnoreNFO.Location = New System.Drawing.Point(3, 30)
        Me.chkDateAddedIgnoreNFO.Name = "chkGeneralDateAddedIgnoreNFO"
        Me.chkDateAddedIgnoreNFO.Size = New System.Drawing.Size(188, 17)
        Me.chkDateAddedIgnoreNFO.TabIndex = 10
        Me.chkDateAddedIgnoreNFO.Text = "Ignore <dateadded> from NFO"
        Me.chkDateAddedIgnoreNFO.UseVisualStyleBackColor = True
        '
        'cbGeneralDateTime
        '
        Me.cbDateAddedDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbDateAddedDateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDateAddedDateTime.FormattingEnabled = True
        Me.cbDateAddedDateTime.Location = New System.Drawing.Point(3, 3)
        Me.cbDateAddedDateTime.Name = "cbGeneralDateTime"
        Me.cbDateAddedDateTime.Size = New System.Drawing.Size(226, 21)
        Me.cbDateAddedDateTime.TabIndex = 11
        '
        'lvMovieSources
        '
        Me.lvMovieSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colMovieSourcesID, Me.colMovieSourcesName, Me.colMovieSourcesPath, Me.colMovieSourcesLanguage, Me.colMovieSourcesRecur, Me.colMovieSourcesFolder, Me.colMovieSourcesSingle, Me.colMovieSourcesExclude, Me.colMovieSourcesGetYear})
        Me.lvMovieSources.FullRowSelect = True
        Me.lvMovieSources.HideSelection = False
        Me.lvMovieSources.Location = New System.Drawing.Point(3, 57)
        Me.lvMovieSources.Name = "lvMovieSources"
        Me.tblSettings.SetRowSpan(Me.lvMovieSources, 3)
        Me.lvMovieSources.Size = New System.Drawing.Size(720, 108)
        Me.lvMovieSources.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvMovieSources.TabIndex = 1
        Me.lvMovieSources.UseCompatibleStateImageBehavior = False
        Me.lvMovieSources.View = System.Windows.Forms.View.Details
        '
        'colMovieSourcesID
        '
        Me.colMovieSourcesID.Text = "ID"
        Me.colMovieSourcesID.Width = 0
        '
        'colMovieSourcesName
        '
        Me.colMovieSourcesName.Text = "Name"
        Me.colMovieSourcesName.Width = 100
        '
        'colMovieSourcesPath
        '
        Me.colMovieSourcesPath.Text = "Path"
        Me.colMovieSourcesPath.Width = 130
        '
        'colMovieSourcesLanguage
        '
        Me.colMovieSourcesLanguage.Text = "Language"
        Me.colMovieSourcesLanguage.Width = 80
        '
        'colMovieSourcesRecur
        '
        Me.colMovieSourcesRecur.Text = "Recursive"
        '
        'colMovieSourcesFolder
        '
        Me.colMovieSourcesFolder.Text = "Use Folder Name"
        Me.colMovieSourcesFolder.Width = 110
        '
        'colMovieSourcesSingle
        '
        Me.colMovieSourcesSingle.Text = "Single Video"
        Me.colMovieSourcesSingle.Width = 90
        '
        'colMovieSourcesExclude
        '
        Me.colMovieSourcesExclude.Text = "Exclude"
        '
        'colMovieSourcesGetYear
        '
        Me.colMovieSourcesGetYear.Text = "Get Year"
        '
        'btnTitleFilterDefaults
        '
        Me.btnTitleFilterDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTitleFilterDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTitleFilterDefaults.Image = CType(resources.GetObject("btnTitleFilterDefaults.Image"), System.Drawing.Image)
        Me.btnTitleFilterDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTitleFilterDefaults.Location = New System.Drawing.Point(432, 778)
        Me.btnTitleFilterDefaults.Name = "btnTitleFilterDefaults"
        Me.btnTitleFilterDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnTitleFilterDefaults.TabIndex = 5
        Me.btnTitleFilterDefaults.Text = "Defaults"
        Me.btnTitleFilterDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTitleFilterDefaults.UseVisualStyleBackColor = True
        '
        'lblTitleFilter
        '
        Me.lblTitleFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitleFilter.AutoSize = True
        Me.lblTitleFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitleFilter.Location = New System.Drawing.Point(197, 783)
        Me.lblTitleFilter.Name = "lblTitleFilter"
        Me.lblTitleFilter.Size = New System.Drawing.Size(218, 13)
        Me.lblTitleFilter.TabIndex = 6
        Me.lblTitleFilter.Text = "Use ALT + UP / DOWN to move the columns"
        '
        'dgvTitleFilter
        '
        Me.dgvTitleFilter.AllowUserToResizeRows = False
        Me.dgvTitleFilter.BackgroundColor = System.Drawing.Color.White
        Me.dgvTitleFilter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTitleFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTitleFilter.ColumnHeadersVisible = False
        Me.dgvTitleFilter.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colVideoExtensions})
        Me.dgvTitleFilter.Location = New System.Drawing.Point(200, 557)
        Me.dgvTitleFilter.Name = "dgvTitleFilter"
        Me.dgvTitleFilter.RowHeadersWidth = 25
        Me.dgvTitleFilter.ShowCellErrors = False
        Me.dgvTitleFilter.ShowCellToolTips = False
        Me.dgvTitleFilter.ShowRowErrors = False
        Me.dgvTitleFilter.Size = New System.Drawing.Size(160, 194)
        Me.dgvTitleFilter.TabIndex = 7
        '
        'colVideoExtensions
        '
        Me.colVideoExtensions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colVideoExtensions.HeaderText = "Extension"
        Me.colVideoExtensions.Name = "colVideoExtensions"
        '
        'frmMovie_Source
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1185, 858)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmMovie_Source"
        Me.Text = "frmMovie_Source"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbMovieGeneralFiltersOpts.ResumeLayout(False)
        Me.gbMovieGeneralFiltersOpts.PerformLayout()
        Me.tblMovieGeneralFiltersOpts.ResumeLayout(False)
        Me.tblMovieGeneralFiltersOpts.PerformLayout()
        Me.gbImportOptions.ResumeLayout(False)
        Me.gbImportOptions.PerformLayout()
        Me.tblMovieSourcesMiscOpts.ResumeLayout(False)
        Me.tblMovieSourcesMiscOpts.PerformLayout()
        Me.gbSourcesDefaults.ResumeLayout(False)
        Me.gbSourcesDefaults.PerformLayout()
        Me.tblMovieSourcesDefaultsOpts.ResumeLayout(False)
        Me.tblMovieSourcesDefaultsOpts.PerformLayout()
        Me.gbGeneralDateAdded.ResumeLayout(False)
        Me.gbGeneralDateAdded.PerformLayout()
        Me.tblGeneralDateAdded.ResumeLayout(False)
        Me.tblGeneralDateAdded.PerformLayout()
        CType(Me.dgvTitleFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents lvMovieSources As ListView
    Friend WithEvents colMovieSourcesID As ColumnHeader
    Friend WithEvents colMovieSourcesName As ColumnHeader
    Friend WithEvents colMovieSourcesPath As ColumnHeader
    Friend WithEvents colMovieSourcesLanguage As ColumnHeader
    Friend WithEvents colMovieSourcesRecur As ColumnHeader
    Friend WithEvents colMovieSourcesFolder As ColumnHeader
    Friend WithEvents colMovieSourcesSingle As ColumnHeader
    Friend WithEvents colMovieSourcesExclude As ColumnHeader
    Friend WithEvents colMovieSourcesGetYear As ColumnHeader
    Friend WithEvents btnMovieSourceAdd As Button
    Friend WithEvents btnMovieSourceEdit As Button
    Friend WithEvents btnMovieSourceRemove As Button
    Friend WithEvents gbSourcesDefaults As GroupBox
    Friend WithEvents tblMovieSourcesDefaultsOpts As TableLayoutPanel
    Friend WithEvents cbMovieGeneralLang As ComboBox
    Friend WithEvents lblMovieSourcesDefaultsLanguage As Label
    Friend WithEvents gbImportOptions As GroupBox
    Friend WithEvents tblMovieSourcesMiscOpts As TableLayoutPanel
    Friend WithEvents chkCleanDB As CheckBox
    Friend WithEvents lblMovieSkipLessThan As Label
    Friend WithEvents chkSortBeforeScan As CheckBox
    Friend WithEvents txtSkipLessThan As TextBox
    Friend WithEvents lblMovieSkipLessThanMB As Label
    Friend WithEvents chkMovieSkipStackedSizeCheck As CheckBox
    Friend WithEvents gbMovieGeneralFiltersOpts As GroupBox
    Friend WithEvents tblMovieGeneralFiltersOpts As TableLayoutPanel
    Friend WithEvents chkTitleProperCase As CheckBox
    Friend WithEvents chkMarkNew As CheckBox
    Friend WithEvents gbGeneralDateAdded As GroupBox
    Friend WithEvents tblGeneralDateAdded As TableLayoutPanel
    Friend WithEvents chkDateAddedIgnoreNFO As CheckBox
    Friend WithEvents cbDateAddedDateTime As ComboBox
    Friend WithEvents chkVideoSourceFromFolder As CheckBox
    Friend WithEvents chkOverwriteNfo As CheckBox
    Friend WithEvents lblGeneralOverwriteNfo As Label
    Friend WithEvents btnTitleFilterDefaults As Button
    Friend WithEvents lblTitleFilter As Label
    Friend WithEvents dgvTitleFilter As DataGridView
    Friend WithEvents colVideoExtensions As DataGridViewTextBoxColumn
End Class
