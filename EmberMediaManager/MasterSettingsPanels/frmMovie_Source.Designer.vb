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
        Me.btnMovieFilterRemove = New System.Windows.Forms.Button()
        Me.btnMovieFilterDown = New System.Windows.Forms.Button()
        Me.btnMovieFilterUp = New System.Windows.Forms.Button()
        Me.chkMovieProperCase = New System.Windows.Forms.CheckBox()
        Me.lstMovieFilters = New System.Windows.Forms.ListBox()
        Me.btnMovieFilterAdd = New System.Windows.Forms.Button()
        Me.txtMovieFilter = New System.Windows.Forms.TextBox()
        Me.btnMovieFilterReset = New System.Windows.Forms.Button()
        Me.gbImportOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSourcesMiscOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieGeneralMarkNew = New System.Windows.Forms.CheckBox()
        Me.chkMovieCleanDB = New System.Windows.Forms.CheckBox()
        Me.lblMovieSkipLessThan = New System.Windows.Forms.Label()
        Me.chkMovieSortBeforeScan = New System.Windows.Forms.CheckBox()
        Me.txtMovieSkipLessThan = New System.Windows.Forms.TextBox()
        Me.lblMovieSkipLessThanMB = New System.Windows.Forms.Label()
        Me.chkMovieSkipStackedSizeCheck = New System.Windows.Forms.CheckBox()
        Me.gbMovieSourcesDefaultsOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSourcesDefaultsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMovieSourcesDefaultsLanguage = New System.Windows.Forms.Label()
        Me.cbMovieGeneralLang = New System.Windows.Forms.ComboBox()
        Me.btnMovieSourceRemove = New System.Windows.Forms.Button()
        Me.btnMovieSourceEdit = New System.Windows.Forms.Button()
        Me.btnMovieSourceAdd = New System.Windows.Forms.Button()
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
        Me.gbGeneralDateAdded = New System.Windows.Forms.GroupBox()
        Me.tblGeneralDateAdded = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGeneralDateAddedIgnoreNFO = New System.Windows.Forms.CheckBox()
        Me.cbGeneralDateTime = New System.Windows.Forms.ComboBox()
        Me.chkGeneralSourceFromFolder = New System.Windows.Forms.CheckBox()
        Me.chkGeneralOverwriteNfo = New System.Windows.Forms.CheckBox()
        Me.lblGeneralOverwriteNfo = New System.Windows.Forms.Label()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMovieGeneralFiltersOpts.SuspendLayout()
        Me.tblMovieGeneralFiltersOpts.SuspendLayout()
        Me.gbImportOpts.SuspendLayout()
        Me.tblMovieSourcesMiscOpts.SuspendLayout()
        Me.gbMovieSourcesDefaultsOpts.SuspendLayout()
        Me.tblMovieSourcesDefaultsOpts.SuspendLayout()
        Me.gbGeneralDateAdded.SuspendLayout()
        Me.tblGeneralDateAdded.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(833, 544)
        Me.pnlSettings.TabIndex = 0
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.BackColor = System.Drawing.Color.White
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbMovieGeneralFiltersOpts, 0, 5)
        Me.tblSettings.Controls.Add(Me.gbImportOpts, 0, 4)
        Me.tblSettings.Controls.Add(Me.gbMovieSourcesDefaultsOpts, 0, 0)
        Me.tblSettings.Controls.Add(Me.btnMovieSourceRemove, 1, 3)
        Me.tblSettings.Controls.Add(Me.btnMovieSourceEdit, 1, 2)
        Me.tblSettings.Controls.Add(Me.btnMovieSourceAdd, 1, 1)
        Me.tblSettings.Controls.Add(Me.lvMovieSources, 0, 1)
        Me.tblSettings.Controls.Add(Me.gbGeneralDateAdded, 1, 4)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 6
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettings.Size = New System.Drawing.Size(833, 544)
        Me.tblSettings.TabIndex = 0
        '
        'gbMovieGeneralFiltersOpts
        '
        Me.gbMovieGeneralFiltersOpts.AutoSize = True
        Me.gbMovieGeneralFiltersOpts.Controls.Add(Me.tblMovieGeneralFiltersOpts)
        Me.gbMovieGeneralFiltersOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieGeneralFiltersOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieGeneralFiltersOpts.Location = New System.Drawing.Point(3, 336)
        Me.gbMovieGeneralFiltersOpts.Name = "gbMovieGeneralFiltersOpts"
        Me.gbMovieGeneralFiltersOpts.Size = New System.Drawing.Size(583, 205)
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
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterRemove, 4, 2)
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterDown, 3, 2)
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterUp, 2, 2)
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.chkMovieProperCase, 0, 0)
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.lstMovieFilters, 0, 1)
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterAdd, 1, 2)
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.txtMovieFilter, 0, 2)
        Me.tblMovieGeneralFiltersOpts.Controls.Add(Me.btnMovieFilterReset, 4, 0)
        Me.tblMovieGeneralFiltersOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralFiltersOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralFiltersOpts.Name = "tblMovieGeneralFiltersOpts"
        Me.tblMovieGeneralFiltersOpts.RowCount = 4
        Me.tblMovieGeneralFiltersOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralFiltersOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralFiltersOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralFiltersOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralFiltersOpts.Size = New System.Drawing.Size(577, 184)
        Me.tblMovieGeneralFiltersOpts.TabIndex = 10
        '
        'btnMovieFilterRemove
        '
        Me.btnMovieFilterRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieFilterRemove.Image = CType(resources.GetObject("btnMovieFilterRemove.Image"), System.Drawing.Image)
        Me.btnMovieFilterRemove.Location = New System.Drawing.Point(200, 158)
        Me.btnMovieFilterRemove.Name = "btnMovieFilterRemove"
        Me.btnMovieFilterRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterRemove.TabIndex = 6
        Me.btnMovieFilterRemove.UseVisualStyleBackColor = True
        '
        'btnMovieFilterDown
        '
        Me.btnMovieFilterDown.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieFilterDown.Image = CType(resources.GetObject("btnMovieFilterDown.Image"), System.Drawing.Image)
        Me.btnMovieFilterDown.Location = New System.Drawing.Point(171, 158)
        Me.btnMovieFilterDown.Name = "btnMovieFilterDown"
        Me.btnMovieFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterDown.TabIndex = 5
        Me.btnMovieFilterDown.UseVisualStyleBackColor = True
        '
        'btnMovieFilterUp
        '
        Me.btnMovieFilterUp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieFilterUp.Image = CType(resources.GetObject("btnMovieFilterUp.Image"), System.Drawing.Image)
        Me.btnMovieFilterUp.Location = New System.Drawing.Point(142, 158)
        Me.btnMovieFilterUp.Name = "btnMovieFilterUp"
        Me.btnMovieFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterUp.TabIndex = 4
        Me.btnMovieFilterUp.UseVisualStyleBackColor = True
        '
        'chkMovieProperCase
        '
        Me.chkMovieProperCase.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieProperCase.AutoSize = True
        Me.tblMovieGeneralFiltersOpts.SetColumnSpan(Me.chkMovieProperCase, 4)
        Me.chkMovieProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieProperCase.Location = New System.Drawing.Point(3, 6)
        Me.chkMovieProperCase.Name = "chkMovieProperCase"
        Me.chkMovieProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkMovieProperCase.TabIndex = 0
        Me.chkMovieProperCase.Text = "Convert Names to Proper Case"
        Me.chkMovieProperCase.UseVisualStyleBackColor = True
        '
        'lstMovieFilters
        '
        Me.tblMovieGeneralFiltersOpts.SetColumnSpan(Me.lstMovieFilters, 5)
        Me.lstMovieFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMovieFilters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstMovieFilters.FormattingEnabled = True
        Me.lstMovieFilters.Location = New System.Drawing.Point(3, 32)
        Me.lstMovieFilters.Name = "lstMovieFilters"
        Me.lstMovieFilters.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstMovieFilters.Size = New System.Drawing.Size(220, 120)
        Me.lstMovieFilters.TabIndex = 1
        '
        'btnMovieFilterAdd
        '
        Me.btnMovieFilterAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieFilterAdd.Image = CType(resources.GetObject("btnMovieFilterAdd.Image"), System.Drawing.Image)
        Me.btnMovieFilterAdd.Location = New System.Drawing.Point(113, 158)
        Me.btnMovieFilterAdd.Name = "btnMovieFilterAdd"
        Me.btnMovieFilterAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterAdd.TabIndex = 3
        Me.btnMovieFilterAdd.UseVisualStyleBackColor = True
        '
        'txtMovieFilter
        '
        Me.txtMovieFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieFilter.Location = New System.Drawing.Point(3, 158)
        Me.txtMovieFilter.Name = "txtMovieFilter"
        Me.txtMovieFilter.Size = New System.Drawing.Size(104, 22)
        Me.txtMovieFilter.TabIndex = 2
        '
        'btnMovieFilterReset
        '
        Me.btnMovieFilterReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieFilterReset.Image = CType(resources.GetObject("btnMovieFilterReset.Image"), System.Drawing.Image)
        Me.btnMovieFilterReset.Location = New System.Drawing.Point(200, 3)
        Me.btnMovieFilterReset.Name = "btnMovieFilterReset"
        Me.btnMovieFilterReset.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieFilterReset.TabIndex = 8
        Me.btnMovieFilterReset.UseVisualStyleBackColor = True
        '
        'gbImportOpts
        '
        Me.gbImportOpts.AutoSize = True
        Me.gbImportOpts.Controls.Add(Me.tblMovieSourcesMiscOpts)
        Me.gbImportOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbImportOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbImportOpts.Location = New System.Drawing.Point(3, 189)
        Me.gbImportOpts.Name = "gbImportOpts"
        Me.gbImportOpts.Size = New System.Drawing.Size(583, 141)
        Me.gbImportOpts.TabIndex = 11
        Me.gbImportOpts.TabStop = False
        Me.gbImportOpts.Text = "Import Options"
        '
        'tblMovieSourcesMiscOpts
        '
        Me.tblMovieSourcesMiscOpts.AutoSize = True
        Me.tblMovieSourcesMiscOpts.ColumnCount = 4
        Me.tblMovieSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkMovieGeneralMarkNew, 0, 4)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkMovieCleanDB, 0, 3)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.lblMovieSkipLessThan, 0, 0)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkMovieSortBeforeScan, 0, 2)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.txtMovieSkipLessThan, 1, 0)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.lblMovieSkipLessThanMB, 2, 0)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkMovieSkipStackedSizeCheck, 0, 1)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkGeneralSourceFromFolder, 3, 4)
        Me.tblMovieSourcesMiscOpts.Controls.Add(Me.chkGeneralOverwriteNfo, 3, 2)
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
        Me.tblMovieSourcesMiscOpts.Size = New System.Drawing.Size(577, 120)
        Me.tblMovieSourcesMiscOpts.TabIndex = 9
        '
        'chkMovieGeneralMarkNew
        '
        Me.chkMovieGeneralMarkNew.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieGeneralMarkNew.AutoSize = True
        Me.chkMovieGeneralMarkNew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieGeneralMarkNew.Location = New System.Drawing.Point(3, 100)
        Me.chkMovieGeneralMarkNew.Name = "chkMovieGeneralMarkNew"
        Me.chkMovieGeneralMarkNew.Size = New System.Drawing.Size(117, 17)
        Me.chkMovieGeneralMarkNew.TabIndex = 10
        Me.chkMovieGeneralMarkNew.Text = "Mark New Movies"
        Me.chkMovieGeneralMarkNew.UseVisualStyleBackColor = True
        '
        'chkMovieCleanDB
        '
        Me.chkMovieCleanDB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieCleanDB.AutoSize = True
        Me.tblMovieSourcesMiscOpts.SetColumnSpan(Me.chkMovieCleanDB, 3)
        Me.chkMovieCleanDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieCleanDB.Location = New System.Drawing.Point(3, 77)
        Me.chkMovieCleanDB.Name = "chkMovieCleanDB"
        Me.chkMovieCleanDB.Size = New System.Drawing.Size(218, 17)
        Me.chkMovieCleanDB.TabIndex = 9
        Me.chkMovieCleanDB.Text = "Clean database after updating library"
        Me.chkMovieCleanDB.UseVisualStyleBackColor = True
        '
        'lblMovieSkipLessThan
        '
        Me.lblMovieSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSkipLessThan.AutoSize = True
        Me.lblMovieSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieSkipLessThan.Location = New System.Drawing.Point(3, 7)
        Me.lblMovieSkipLessThan.Name = "lblMovieSkipLessThan"
        Me.lblMovieSkipLessThan.Size = New System.Drawing.Size(122, 13)
        Me.lblMovieSkipLessThan.TabIndex = 0
        Me.lblMovieSkipLessThan.Text = "Skip files smaller than:"
        '
        'chkMovieSortBeforeScan
        '
        Me.chkMovieSortBeforeScan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSortBeforeScan.AutoSize = True
        Me.tblMovieSourcesMiscOpts.SetColumnSpan(Me.chkMovieSortBeforeScan, 3)
        Me.chkMovieSortBeforeScan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieSortBeforeScan.Location = New System.Drawing.Point(3, 54)
        Me.chkMovieSortBeforeScan.Name = "chkMovieSortBeforeScan"
        Me.chkMovieSortBeforeScan.Size = New System.Drawing.Size(273, 17)
        Me.chkMovieSortBeforeScan.TabIndex = 6
        Me.chkMovieSortBeforeScan.Text = "Sort files into folders before each library update"
        Me.chkMovieSortBeforeScan.UseVisualStyleBackColor = True
        '
        'txtMovieSkipLessThan
        '
        Me.txtMovieSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieSkipLessThan.Location = New System.Drawing.Point(131, 3)
        Me.txtMovieSkipLessThan.Name = "txtMovieSkipLessThan"
        Me.txtMovieSkipLessThan.Size = New System.Drawing.Size(51, 22)
        Me.txtMovieSkipLessThan.TabIndex = 1
        '
        'lblMovieSkipLessThanMB
        '
        Me.lblMovieSkipLessThanMB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSkipLessThanMB.AutoSize = True
        Me.lblMovieSkipLessThanMB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.chkMovieSkipStackedSizeCheck.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieSkipStackedSizeCheck.Location = New System.Drawing.Point(3, 31)
        Me.chkMovieSkipStackedSizeCheck.Name = "chkMovieSkipStackedSizeCheck"
        Me.chkMovieSkipStackedSizeCheck.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMovieSkipStackedSizeCheck.Size = New System.Drawing.Size(208, 17)
        Me.chkMovieSkipStackedSizeCheck.TabIndex = 3
        Me.chkMovieSkipStackedSizeCheck.Text = "Skip Size Check of Stacked Files"
        Me.chkMovieSkipStackedSizeCheck.UseVisualStyleBackColor = True
        '
        'gbMovieSourcesDefaultsOpts
        '
        Me.gbMovieSourcesDefaultsOpts.AutoSize = True
        Me.tblSettings.SetColumnSpan(Me.gbMovieSourcesDefaultsOpts, 2)
        Me.gbMovieSourcesDefaultsOpts.Controls.Add(Me.tblMovieSourcesDefaultsOpts)
        Me.gbMovieSourcesDefaultsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSourcesDefaultsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieSourcesDefaultsOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieSourcesDefaultsOpts.Name = "gbMovieSourcesDefaultsOpts"
        Me.gbMovieSourcesDefaultsOpts.Size = New System.Drawing.Size(827, 48)
        Me.gbMovieSourcesDefaultsOpts.TabIndex = 10
        Me.gbMovieSourcesDefaultsOpts.TabStop = False
        Me.gbMovieSourcesDefaultsOpts.Text = "Defaults for new Sources"
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
        Me.tblMovieSourcesDefaultsOpts.Size = New System.Drawing.Size(821, 27)
        Me.tblMovieSourcesDefaultsOpts.TabIndex = 0
        '
        'lblMovieSourcesDefaultsLanguage
        '
        Me.lblMovieSourcesDefaultsLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSourcesDefaultsLanguage.AutoSize = True
        Me.lblMovieSourcesDefaultsLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
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
        Me.cbMovieGeneralLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbMovieGeneralLang.Location = New System.Drawing.Point(111, 3)
        Me.cbMovieGeneralLang.Name = "cbMovieGeneralLang"
        Me.cbMovieGeneralLang.Size = New System.Drawing.Size(160, 21)
        Me.cbMovieGeneralLang.TabIndex = 12
        '
        'btnMovieSourceRemove
        '
        Me.btnMovieSourceRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMovieSourceRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMovieSourceRemove.Image = CType(resources.GetObject("btnMovieSourceRemove.Image"), System.Drawing.Image)
        Me.btnMovieSourceRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceRemove.Location = New System.Drawing.Point(592, 160)
        Me.btnMovieSourceRemove.Name = "btnMovieSourceRemove"
        Me.btnMovieSourceRemove.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceRemove.TabIndex = 4
        Me.btnMovieSourceRemove.Text = "Remove"
        Me.btnMovieSourceRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceRemove.UseVisualStyleBackColor = True
        '
        'btnMovieSourceEdit
        '
        Me.btnMovieSourceEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMovieSourceEdit.Image = CType(resources.GetObject("btnMovieSourceEdit.Image"), System.Drawing.Image)
        Me.btnMovieSourceEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceEdit.Location = New System.Drawing.Point(592, 86)
        Me.btnMovieSourceEdit.Name = "btnMovieSourceEdit"
        Me.btnMovieSourceEdit.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceEdit.TabIndex = 3
        Me.btnMovieSourceEdit.Text = "Edit Source"
        Me.btnMovieSourceEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceEdit.UseVisualStyleBackColor = True
        '
        'btnMovieSourceAdd
        '
        Me.btnMovieSourceAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMovieSourceAdd.Image = CType(resources.GetObject("btnMovieSourceAdd.Image"), System.Drawing.Image)
        Me.btnMovieSourceAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSourceAdd.Location = New System.Drawing.Point(592, 57)
        Me.btnMovieSourceAdd.Name = "btnMovieSourceAdd"
        Me.btnMovieSourceAdd.Size = New System.Drawing.Size(104, 23)
        Me.btnMovieSourceAdd.TabIndex = 2
        Me.btnMovieSourceAdd.Text = "Add Source"
        Me.btnMovieSourceAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSourceAdd.UseVisualStyleBackColor = True
        '
        'lvMovieSources
        '
        Me.lvMovieSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colMovieSourcesID, Me.colMovieSourcesName, Me.colMovieSourcesPath, Me.colMovieSourcesLanguage, Me.colMovieSourcesRecur, Me.colMovieSourcesFolder, Me.colMovieSourcesSingle, Me.colMovieSourcesExclude, Me.colMovieSourcesGetYear})
        Me.lvMovieSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMovieSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvMovieSources.FullRowSelect = True
        Me.lvMovieSources.HideSelection = False
        Me.lvMovieSources.Location = New System.Drawing.Point(3, 57)
        Me.lvMovieSources.Name = "lvMovieSources"
        Me.tblSettings.SetRowSpan(Me.lvMovieSources, 3)
        Me.lvMovieSources.Size = New System.Drawing.Size(583, 126)
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
        'gbGeneralDateAdded
        '
        Me.gbGeneralDateAdded.AutoSize = True
        Me.gbGeneralDateAdded.Controls.Add(Me.tblGeneralDateAdded)
        Me.gbGeneralDateAdded.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGeneralDateAdded.Location = New System.Drawing.Point(592, 189)
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
        Me.tblGeneralDateAdded.Controls.Add(Me.chkGeneralDateAddedIgnoreNFO, 0, 1)
        Me.tblGeneralDateAdded.Controls.Add(Me.cbGeneralDateTime, 0, 0)
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
        Me.chkGeneralDateAddedIgnoreNFO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralDateAddedIgnoreNFO.AutoSize = True
        Me.chkGeneralDateAddedIgnoreNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralDateAddedIgnoreNFO.Location = New System.Drawing.Point(3, 30)
        Me.chkGeneralDateAddedIgnoreNFO.Name = "chkGeneralDateAddedIgnoreNFO"
        Me.chkGeneralDateAddedIgnoreNFO.Size = New System.Drawing.Size(188, 17)
        Me.chkGeneralDateAddedIgnoreNFO.TabIndex = 10
        Me.chkGeneralDateAddedIgnoreNFO.Text = "Ignore <dateadded> from NFO"
        Me.chkGeneralDateAddedIgnoreNFO.UseVisualStyleBackColor = True
        '
        'cbGeneralDateTime
        '
        Me.cbGeneralDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralDateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralDateTime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbGeneralDateTime.FormattingEnabled = True
        Me.cbGeneralDateTime.Location = New System.Drawing.Point(3, 3)
        Me.cbGeneralDateTime.Name = "cbGeneralDateTime"
        Me.cbGeneralDateTime.Size = New System.Drawing.Size(226, 21)
        Me.cbGeneralDateTime.TabIndex = 11
        '
        'chkGeneralSourceFromFolder
        '
        Me.chkGeneralSourceFromFolder.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralSourceFromFolder.AutoSize = True
        Me.chkGeneralSourceFromFolder.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkGeneralSourceFromFolder.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralSourceFromFolder.Location = New System.Drawing.Point(282, 100)
        Me.chkGeneralSourceFromFolder.Name = "chkGeneralSourceFromFolder"
        Me.chkGeneralSourceFromFolder.Size = New System.Drawing.Size(242, 17)
        Me.chkGeneralSourceFromFolder.TabIndex = 11
        Me.chkGeneralSourceFromFolder.Text = "Include Folder Name in Source Type Check"
        Me.chkGeneralSourceFromFolder.UseVisualStyleBackColor = True
        '
        'chkGeneralOverwriteNfo
        '
        Me.chkGeneralOverwriteNfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGeneralOverwriteNfo.AutoSize = True
        Me.chkGeneralOverwriteNfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGeneralOverwriteNfo.Location = New System.Drawing.Point(282, 54)
        Me.chkGeneralOverwriteNfo.Name = "chkGeneralOverwriteNfo"
        Me.chkGeneralOverwriteNfo.Size = New System.Drawing.Size(191, 17)
        Me.chkGeneralOverwriteNfo.TabIndex = 12
        Me.chkGeneralOverwriteNfo.Text = "Overwrite Non-conforming nfos"
        Me.chkGeneralOverwriteNfo.UseVisualStyleBackColor = True
        '
        'lblGeneralOverwriteNfo
        '
        Me.lblGeneralOverwriteNfo.AutoSize = True
        Me.lblGeneralOverwriteNfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralOverwriteNfo.Location = New System.Drawing.Point(282, 74)
        Me.lblGeneralOverwriteNfo.Name = "lblGeneralOverwriteNfo"
        Me.lblGeneralOverwriteNfo.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblGeneralOverwriteNfo.Size = New System.Drawing.Size(314, 12)
        Me.lblGeneralOverwriteNfo.TabIndex = 13
        Me.lblGeneralOverwriteNfo.Text = "(If unchecked, non-conforming nfos will be renamed to <filename>.info)"
        Me.lblGeneralOverwriteNfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMovie_Source
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 544)
        Me.Controls.Add(Me.pnlSettings)
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
        Me.gbImportOpts.ResumeLayout(False)
        Me.gbImportOpts.PerformLayout()
        Me.tblMovieSourcesMiscOpts.ResumeLayout(False)
        Me.tblMovieSourcesMiscOpts.PerformLayout()
        Me.gbMovieSourcesDefaultsOpts.ResumeLayout(False)
        Me.gbMovieSourcesDefaultsOpts.PerformLayout()
        Me.tblMovieSourcesDefaultsOpts.ResumeLayout(False)
        Me.tblMovieSourcesDefaultsOpts.PerformLayout()
        Me.gbGeneralDateAdded.ResumeLayout(False)
        Me.gbGeneralDateAdded.PerformLayout()
        Me.tblGeneralDateAdded.ResumeLayout(False)
        Me.tblGeneralDateAdded.PerformLayout()
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
    Friend WithEvents gbMovieSourcesDefaultsOpts As GroupBox
    Friend WithEvents tblMovieSourcesDefaultsOpts As TableLayoutPanel
    Friend WithEvents cbMovieGeneralLang As ComboBox
    Friend WithEvents lblMovieSourcesDefaultsLanguage As Label
    Friend WithEvents gbImportOpts As GroupBox
    Friend WithEvents tblMovieSourcesMiscOpts As TableLayoutPanel
    Friend WithEvents chkMovieCleanDB As CheckBox
    Friend WithEvents lblMovieSkipLessThan As Label
    Friend WithEvents chkMovieSortBeforeScan As CheckBox
    Friend WithEvents txtMovieSkipLessThan As TextBox
    Friend WithEvents lblMovieSkipLessThanMB As Label
    Friend WithEvents chkMovieSkipStackedSizeCheck As CheckBox
    Friend WithEvents gbMovieGeneralFiltersOpts As GroupBox
    Friend WithEvents tblMovieGeneralFiltersOpts As TableLayoutPanel
    Friend WithEvents btnMovieFilterRemove As Button
    Friend WithEvents btnMovieFilterDown As Button
    Friend WithEvents btnMovieFilterUp As Button
    Friend WithEvents chkMovieProperCase As CheckBox
    Friend WithEvents lstMovieFilters As ListBox
    Friend WithEvents btnMovieFilterAdd As Button
    Friend WithEvents txtMovieFilter As TextBox
    Friend WithEvents btnMovieFilterReset As Button
    Friend WithEvents chkMovieGeneralMarkNew As CheckBox
    Friend WithEvents gbGeneralDateAdded As GroupBox
    Friend WithEvents tblGeneralDateAdded As TableLayoutPanel
    Friend WithEvents chkGeneralDateAddedIgnoreNFO As CheckBox
    Friend WithEvents cbGeneralDateTime As ComboBox
    Friend WithEvents chkGeneralSourceFromFolder As CheckBox
    Friend WithEvents chkGeneralOverwriteNfo As CheckBox
    Friend WithEvents lblGeneralOverwriteNfo As Label
End Class
