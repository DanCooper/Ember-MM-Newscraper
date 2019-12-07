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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTV_Source))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.tcTVSources = New System.Windows.Forms.TabControl()
        Me.tpTVSourcesGeneral = New System.Windows.Forms.TabPage()
        Me.tblTVSourcesGeneral = New System.Windows.Forms.TableLayoutPanel()
        Me.lvTVSources = New System.Windows.Forms.ListView()
        Me.colTVSourcesID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesLanguage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesOrdering = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesExclude = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesSorting = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesSingle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gbTVSourcesMiscOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVSourcesMiscOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTVCleanDB = New System.Windows.Forms.CheckBox()
        Me.lblTVSkipLessThanMB = New System.Windows.Forms.Label()
        Me.lblTVSkipLessThan = New System.Windows.Forms.Label()
        Me.txtTVSkipLessThan = New System.Windows.Forms.TextBox()
        Me.btnRemTVSource = New System.Windows.Forms.Button()
        Me.btnTVSourceEdit = New System.Windows.Forms.Button()
        Me.btnTVSourceAdd = New System.Windows.Forms.Button()
        Me.gbTVSourcesDefaultsOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVSourcesDefaultsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.cbTVGeneralLang = New System.Windows.Forms.ComboBox()
        Me.lblTVSourcesDefaultsOrdering = New System.Windows.Forms.Label()
        Me.cbTVScraperOptionsOrdering = New System.Windows.Forms.ComboBox()
        Me.lblTVSourcesDefaultsLanguage = New System.Windows.Forms.Label()
        Me.tpTVSourcesRegex = New System.Windows.Forms.TabPage()
        Me.tblTVSourcesRegex = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTVSourcesRegexMultiPartMatching = New System.Windows.Forms.GroupBox()
        Me.tblTVSourcesRegexMultiPartMatching = New System.Windows.Forms.TableLayoutPanel()
        Me.txtTVSourcesRegexMultiPartMatching = New System.Windows.Forms.TextBox()
        Me.btnTVSourcesRegexMultiPartMatchingReset = New System.Windows.Forms.Button()
        Me.gbTVSourcesRegexTVShowMatching = New System.Windows.Forms.GroupBox()
        Me.tblTVSourcesRegexTVShowMatching = New System.Windows.Forms.TableLayoutPanel()
        Me.lvTVSourcesRegexTVShowMatching = New System.Windows.Forms.ListView()
        Me.colTVSourcesRegexTVShowMatchingID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesRegexTVShowMatchingRegex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.coTVSourcesRegexTVShowMatchingDefaultSeason = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesRegexTVShowMatchingByDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnTVSourcesRegexTVShowMatchingClear = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingDown = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingUp = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingEdit = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingReset = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingRemove = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingAdd = New System.Windows.Forms.Button()
        Me.tblTVSourcesRegexTVShowMatchingEdit = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTVSourcesRegexTVShowMatchingRegex = New System.Windows.Forms.Label()
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason = New System.Windows.Forms.Label()
        Me.txtTVSourcesRegexTVShowMatchingRegex = New System.Windows.Forms.TextBox()
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason = New System.Windows.Forms.TextBox()
        Me.lblTVSourcesRegexTVShowMatchingByDate = New System.Windows.Forms.Label()
        Me.chkTVSourcesRegexTVShowMatchingByDate = New System.Windows.Forms.CheckBox()
        Me.chkTVGeneralMarkNewShows = New System.Windows.Forms.CheckBox()
        Me.chkTVGeneralMarkNewEpisodes = New System.Windows.Forms.CheckBox()
        Me.gbTVShowFilterOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVShowFilterOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTVShowFilterRemove = New System.Windows.Forms.Button()
        Me.btnTVShowFilterDown = New System.Windows.Forms.Button()
        Me.btnTVShowFilterReset = New System.Windows.Forms.Button()
        Me.btnTVShowFilterUp = New System.Windows.Forms.Button()
        Me.chkTVShowProperCase = New System.Windows.Forms.CheckBox()
        Me.lstTVShowFilter = New System.Windows.Forms.ListBox()
        Me.btnTVShowFilterAdd = New System.Windows.Forms.Button()
        Me.txtTVShowFilter = New System.Windows.Forms.TextBox()
        Me.gbTVEpisodeFilterOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVEpisodeFilterOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTVEpisodeFilterRemove = New System.Windows.Forms.Button()
        Me.btnTVEpisodeFilterDown = New System.Windows.Forms.Button()
        Me.btnTVEpisodeFilterReset = New System.Windows.Forms.Button()
        Me.btnTVEpisodeFilterUp = New System.Windows.Forms.Button()
        Me.chkTVEpisodeNoFilter = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodeProperCase = New System.Windows.Forms.CheckBox()
        Me.btnTVEpisodeFilterAdd = New System.Windows.Forms.Button()
        Me.lstTVEpisodeFilter = New System.Windows.Forms.ListBox()
        Me.txtTVEpisodeFilter = New System.Windows.Forms.TextBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.tcTVSources.SuspendLayout()
        Me.tpTVSourcesGeneral.SuspendLayout()
        Me.tblTVSourcesGeneral.SuspendLayout()
        Me.gbTVSourcesMiscOpts.SuspendLayout()
        Me.tblTVSourcesMiscOpts.SuspendLayout()
        Me.gbTVSourcesDefaultsOpts.SuspendLayout()
        Me.tblTVSourcesDefaultsOpts.SuspendLayout()
        Me.tpTVSourcesRegex.SuspendLayout()
        Me.tblTVSourcesRegex.SuspendLayout()
        Me.gbTVSourcesRegexMultiPartMatching.SuspendLayout()
        Me.tblTVSourcesRegexMultiPartMatching.SuspendLayout()
        Me.gbTVSourcesRegexTVShowMatching.SuspendLayout()
        Me.tblTVSourcesRegexTVShowMatching.SuspendLayout()
        Me.tblTVSourcesRegexTVShowMatchingEdit.SuspendLayout()
        Me.gbTVShowFilterOpts.SuspendLayout()
        Me.tblTVShowFilterOpts.SuspendLayout()
        Me.gbTVEpisodeFilterOpts.SuspendLayout()
        Me.tblTVEpisodeFilterOpts.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(898, 658)
        Me.pnlSettings.TabIndex = 12
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.tcTVSources, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(898, 658)
        Me.tblSettings.TabIndex = 1
        '
        'tcTVSources
        '
        Me.tcTVSources.Controls.Add(Me.tpTVSourcesGeneral)
        Me.tcTVSources.Controls.Add(Me.tpTVSourcesRegex)
        Me.tcTVSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcTVSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcTVSources.Location = New System.Drawing.Point(3, 3)
        Me.tcTVSources.Name = "tcTVSources"
        Me.tcTVSources.SelectedIndex = 0
        Me.tcTVSources.Size = New System.Drawing.Size(830, 500)
        Me.tcTVSources.TabIndex = 0
        '
        'tpTVSourcesGeneral
        '
        Me.tpTVSourcesGeneral.Controls.Add(Me.tblTVSourcesGeneral)
        Me.tpTVSourcesGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tpTVSourcesGeneral.Name = "tpTVSourcesGeneral"
        Me.tpTVSourcesGeneral.Size = New System.Drawing.Size(822, 474)
        Me.tpTVSourcesGeneral.TabIndex = 0
        Me.tpTVSourcesGeneral.Text = "General"
        Me.tpTVSourcesGeneral.UseVisualStyleBackColor = True
        '
        'tblTVSourcesGeneral
        '
        Me.tblTVSourcesGeneral.AutoSize = True
        Me.tblTVSourcesGeneral.ColumnCount = 4
        Me.tblTVSourcesGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesGeneral.Controls.Add(Me.gbTVEpisodeFilterOpts, 0, 6)
        Me.tblTVSourcesGeneral.Controls.Add(Me.gbTVShowFilterOpts, 0, 6)
        Me.tblTVSourcesGeneral.Controls.Add(Me.lvTVSources, 0, 0)
        Me.tblTVSourcesGeneral.Controls.Add(Me.gbTVSourcesMiscOpts, 1, 4)
        Me.tblTVSourcesGeneral.Controls.Add(Me.btnRemTVSource, 2, 2)
        Me.tblTVSourcesGeneral.Controls.Add(Me.btnTVSourceEdit, 2, 1)
        Me.tblTVSourcesGeneral.Controls.Add(Me.btnTVSourceAdd, 2, 0)
        Me.tblTVSourcesGeneral.Controls.Add(Me.gbTVSourcesDefaultsOpts, 1, 3)
        Me.tblTVSourcesGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesGeneral.Location = New System.Drawing.Point(0, 0)
        Me.tblTVSourcesGeneral.Name = "tblTVSourcesGeneral"
        Me.tblTVSourcesGeneral.RowCount = 7
        Me.tblTVSourcesGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesGeneral.Size = New System.Drawing.Size(822, 474)
        Me.tblTVSourcesGeneral.TabIndex = 7
        '
        'lvTVSources
        '
        Me.lvTVSources.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTVSourcesID, Me.colTVSourcesName, Me.colTVSourcesPath, Me.colTVSourcesLanguage, Me.colTVSourcesOrdering, Me.colTVSourcesExclude, Me.colTVSourcesSorting, Me.colTVSourcesSingle})
        Me.tblTVSourcesGeneral.SetColumnSpan(Me.lvTVSources, 2)
        Me.lvTVSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTVSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvTVSources.FullRowSelect = True
        Me.lvTVSources.HideSelection = False
        Me.lvTVSources.Location = New System.Drawing.Point(3, 3)
        Me.lvTVSources.Name = "lvTVSources"
        Me.tblTVSourcesGeneral.SetRowSpan(Me.lvTVSources, 3)
        Me.lvTVSources.Size = New System.Drawing.Size(690, 105)
        Me.lvTVSources.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTVSources.TabIndex = 0
        Me.lvTVSources.UseCompatibleStateImageBehavior = False
        Me.lvTVSources.View = System.Windows.Forms.View.Details
        '
        'colTVSourcesID
        '
        Me.colTVSourcesID.Text = "ID"
        Me.colTVSourcesID.Width = 0
        '
        'colTVSourcesName
        '
        Me.colTVSourcesName.Text = "Name"
        Me.colTVSourcesName.Width = 94
        '
        'colTVSourcesPath
        '
        Me.colTVSourcesPath.Text = "Path"
        Me.colTVSourcesPath.Width = 250
        '
        'colTVSourcesLanguage
        '
        Me.colTVSourcesLanguage.Text = "Language"
        Me.colTVSourcesLanguage.Width = 80
        '
        'colTVSourcesOrdering
        '
        Me.colTVSourcesOrdering.Text = "Ordering"
        '
        'colTVSourcesExclude
        '
        Me.colTVSourcesExclude.Text = "Exclude"
        '
        'colTVSourcesSorting
        '
        Me.colTVSourcesSorting.Text = "Sorting"
        '
        'colTVSourcesSingle
        '
        Me.colTVSourcesSingle.Text = "Single TV Show"
        '
        'gbTVSourcesMiscOpts
        '
        Me.gbTVSourcesMiscOpts.AutoSize = True
        Me.tblTVSourcesGeneral.SetColumnSpan(Me.gbTVSourcesMiscOpts, 3)
        Me.gbTVSourcesMiscOpts.Controls.Add(Me.tblTVSourcesMiscOpts)
        Me.gbTVSourcesMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVSourcesMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVSourcesMiscOpts.Location = New System.Drawing.Point(335, 235)
        Me.gbTVSourcesMiscOpts.Name = "gbTVSourcesMiscOpts"
        Me.gbTVSourcesMiscOpts.Size = New System.Drawing.Size(484, 95)
        Me.gbTVSourcesMiscOpts.TabIndex = 4
        Me.gbTVSourcesMiscOpts.TabStop = False
        Me.gbTVSourcesMiscOpts.Text = "Miscellaneous Options"
        '
        'tblTVSourcesMiscOpts
        '
        Me.tblTVSourcesMiscOpts.AutoSize = True
        Me.tblTVSourcesMiscOpts.ColumnCount = 4
        Me.tblTVSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesMiscOpts.Controls.Add(Me.chkTVGeneralMarkNewEpisodes, 0, 2)
        Me.tblTVSourcesMiscOpts.Controls.Add(Me.chkTVGeneralMarkNewShows, 0, 2)
        Me.tblTVSourcesMiscOpts.Controls.Add(Me.chkTVCleanDB, 0, 1)
        Me.tblTVSourcesMiscOpts.Controls.Add(Me.lblTVSkipLessThanMB, 2, 0)
        Me.tblTVSourcesMiscOpts.Controls.Add(Me.lblTVSkipLessThan, 0, 0)
        Me.tblTVSourcesMiscOpts.Controls.Add(Me.txtTVSkipLessThan, 1, 0)
        Me.tblTVSourcesMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesMiscOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVSourcesMiscOpts.Name = "tblTVSourcesMiscOpts"
        Me.tblTVSourcesMiscOpts.RowCount = 3
        Me.tblTVSourcesMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesMiscOpts.Size = New System.Drawing.Size(478, 74)
        Me.tblTVSourcesMiscOpts.TabIndex = 7
        '
        'chkTVCleanDB
        '
        Me.chkTVCleanDB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVCleanDB.AutoSize = True
        Me.chkTVCleanDB.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblTVSourcesMiscOpts.SetColumnSpan(Me.chkTVCleanDB, 3)
        Me.chkTVCleanDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVCleanDB.Location = New System.Drawing.Point(3, 31)
        Me.chkTVCleanDB.Name = "chkTVCleanDB"
        Me.chkTVCleanDB.Size = New System.Drawing.Size(218, 17)
        Me.chkTVCleanDB.TabIndex = 5
        Me.chkTVCleanDB.Text = "Clean database after updating library"
        Me.chkTVCleanDB.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVCleanDB.UseVisualStyleBackColor = True
        '
        'lblTVSkipLessThanMB
        '
        Me.lblTVSkipLessThanMB.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTVSkipLessThanMB.AutoSize = True
        Me.lblTVSkipLessThanMB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVSkipLessThanMB.Location = New System.Drawing.Point(257, 7)
        Me.lblTVSkipLessThanMB.Name = "lblTVSkipLessThanMB"
        Me.lblTVSkipLessThanMB.Size = New System.Drawing.Size(24, 13)
        Me.lblTVSkipLessThanMB.TabIndex = 2
        Me.lblTVSkipLessThanMB.Text = "MB"
        '
        'lblTVSkipLessThan
        '
        Me.lblTVSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTVSkipLessThan.AutoSize = True
        Me.lblTVSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVSkipLessThan.Location = New System.Drawing.Point(3, 7)
        Me.lblTVSkipLessThan.Name = "lblTVSkipLessThan"
        Me.lblTVSkipLessThan.Size = New System.Drawing.Size(122, 13)
        Me.lblTVSkipLessThan.TabIndex = 1
        Me.lblTVSkipLessThan.Text = "Skip files smaller than:"
        '
        'txtTVSkipLessThan
        '
        Me.txtTVSkipLessThan.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTVSkipLessThan.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVSkipLessThan.Location = New System.Drawing.Point(136, 3)
        Me.txtTVSkipLessThan.Name = "txtTVSkipLessThan"
        Me.txtTVSkipLessThan.Size = New System.Drawing.Size(51, 22)
        Me.txtTVSkipLessThan.TabIndex = 0
        '
        'btnRemTVSource
        '
        Me.btnRemTVSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemTVSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemTVSource.Image = CType(resources.GetObject("btnRemTVSource.Image"), System.Drawing.Image)
        Me.btnRemTVSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemTVSource.Location = New System.Drawing.Point(699, 85)
        Me.btnRemTVSource.Name = "btnRemTVSource"
        Me.btnRemTVSource.Size = New System.Drawing.Size(104, 23)
        Me.btnRemTVSource.TabIndex = 3
        Me.btnRemTVSource.Text = "Remove"
        Me.btnRemTVSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemTVSource.UseVisualStyleBackColor = True
        '
        'btnTVSourceEdit
        '
        Me.btnTVSourceEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVSourceEdit.Image = CType(resources.GetObject("btnTVSourceEdit.Image"), System.Drawing.Image)
        Me.btnTVSourceEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourceEdit.Location = New System.Drawing.Point(699, 32)
        Me.btnTVSourceEdit.Name = "btnTVSourceEdit"
        Me.btnTVSourceEdit.Size = New System.Drawing.Size(104, 23)
        Me.btnTVSourceEdit.TabIndex = 2
        Me.btnTVSourceEdit.Text = "Edit Source"
        Me.btnTVSourceEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourceEdit.UseVisualStyleBackColor = True
        '
        'btnTVSourceAdd
        '
        Me.btnTVSourceAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVSourceAdd.Image = CType(resources.GetObject("btnTVSourceAdd.Image"), System.Drawing.Image)
        Me.btnTVSourceAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourceAdd.Location = New System.Drawing.Point(699, 3)
        Me.btnTVSourceAdd.Name = "btnTVSourceAdd"
        Me.btnTVSourceAdd.Size = New System.Drawing.Size(104, 23)
        Me.btnTVSourceAdd.TabIndex = 1
        Me.btnTVSourceAdd.Text = "Add Source"
        Me.btnTVSourceAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourceAdd.UseVisualStyleBackColor = True
        '
        'gbTVSourcesDefaultsOpts
        '
        Me.gbTVSourcesDefaultsOpts.AutoSize = True
        Me.tblTVSourcesGeneral.SetColumnSpan(Me.gbTVSourcesDefaultsOpts, 3)
        Me.gbTVSourcesDefaultsOpts.Controls.Add(Me.tblTVSourcesDefaultsOpts)
        Me.gbTVSourcesDefaultsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVSourcesDefaultsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTVSourcesDefaultsOpts.Location = New System.Drawing.Point(335, 114)
        Me.gbTVSourcesDefaultsOpts.Name = "gbTVSourcesDefaultsOpts"
        Me.gbTVSourcesDefaultsOpts.Size = New System.Drawing.Size(484, 115)
        Me.gbTVSourcesDefaultsOpts.TabIndex = 4
        Me.gbTVSourcesDefaultsOpts.TabStop = False
        Me.gbTVSourcesDefaultsOpts.Text = "Defaults for new Sources"
        '
        'tblTVSourcesDefaultsOpts
        '
        Me.tblTVSourcesDefaultsOpts.AutoSize = True
        Me.tblTVSourcesDefaultsOpts.ColumnCount = 2
        Me.tblTVSourcesDefaultsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesDefaultsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesDefaultsOpts.Controls.Add(Me.cbTVGeneralLang, 0, 1)
        Me.tblTVSourcesDefaultsOpts.Controls.Add(Me.lblTVSourcesDefaultsOrdering, 0, 2)
        Me.tblTVSourcesDefaultsOpts.Controls.Add(Me.cbTVScraperOptionsOrdering, 0, 3)
        Me.tblTVSourcesDefaultsOpts.Controls.Add(Me.lblTVSourcesDefaultsLanguage, 0, 0)
        Me.tblTVSourcesDefaultsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesDefaultsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVSourcesDefaultsOpts.Name = "tblTVSourcesDefaultsOpts"
        Me.tblTVSourcesDefaultsOpts.RowCount = 5
        Me.tblTVSourcesDefaultsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesDefaultsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesDefaultsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesDefaultsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesDefaultsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesDefaultsOpts.Size = New System.Drawing.Size(478, 94)
        Me.tblTVSourcesDefaultsOpts.TabIndex = 5
        '
        'cbTVGeneralLang
        '
        Me.cbTVGeneralLang.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbTVGeneralLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVGeneralLang.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTVGeneralLang.Location = New System.Drawing.Point(3, 23)
        Me.cbTVGeneralLang.Name = "cbTVGeneralLang"
        Me.cbTVGeneralLang.Size = New System.Drawing.Size(160, 21)
        Me.cbTVGeneralLang.TabIndex = 11
        '
        'lblTVSourcesDefaultsOrdering
        '
        Me.lblTVSourcesDefaultsOrdering.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTVSourcesDefaultsOrdering.AutoSize = True
        Me.lblTVSourcesDefaultsOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTVSourcesDefaultsOrdering.Location = New System.Drawing.Point(3, 50)
        Me.lblTVSourcesDefaultsOrdering.Name = "lblTVSourcesDefaultsOrdering"
        Me.lblTVSourcesDefaultsOrdering.Size = New System.Drawing.Size(142, 13)
        Me.lblTVSourcesDefaultsOrdering.TabIndex = 7
        Me.lblTVSourcesDefaultsOrdering.Text = "Default Episode Ordering:"
        Me.lblTVSourcesDefaultsOrdering.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbTVScraperOptionsOrdering
        '
        Me.cbTVScraperOptionsOrdering.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbTVScraperOptionsOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVScraperOptionsOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTVScraperOptionsOrdering.FormattingEnabled = True
        Me.cbTVScraperOptionsOrdering.Location = New System.Drawing.Point(3, 70)
        Me.cbTVScraperOptionsOrdering.Name = "cbTVScraperOptionsOrdering"
        Me.cbTVScraperOptionsOrdering.Size = New System.Drawing.Size(160, 21)
        Me.cbTVScraperOptionsOrdering.TabIndex = 8
        '
        'lblTVSourcesDefaultsLanguage
        '
        Me.lblTVSourcesDefaultsLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTVSourcesDefaultsLanguage.AutoSize = True
        Me.lblTVSourcesDefaultsLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTVSourcesDefaultsLanguage.Location = New System.Drawing.Point(3, 3)
        Me.lblTVSourcesDefaultsLanguage.Name = "lblTVSourcesDefaultsLanguage"
        Me.lblTVSourcesDefaultsLanguage.Size = New System.Drawing.Size(102, 13)
        Me.lblTVSourcesDefaultsLanguage.TabIndex = 7
        Me.lblTVSourcesDefaultsLanguage.Text = "Default Language:"
        Me.lblTVSourcesDefaultsLanguage.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tpTVSourcesRegex
        '
        Me.tpTVSourcesRegex.Controls.Add(Me.tblTVSourcesRegex)
        Me.tpTVSourcesRegex.Location = New System.Drawing.Point(4, 22)
        Me.tpTVSourcesRegex.Name = "tpTVSourcesRegex"
        Me.tpTVSourcesRegex.Size = New System.Drawing.Size(822, 474)
        Me.tpTVSourcesRegex.TabIndex = 1
        Me.tpTVSourcesRegex.Text = "Regex"
        Me.tpTVSourcesRegex.UseVisualStyleBackColor = True
        '
        'tblTVSourcesRegex
        '
        Me.tblTVSourcesRegex.AutoSize = True
        Me.tblTVSourcesRegex.ColumnCount = 2
        Me.tblTVSourcesRegex.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegex.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegex.Controls.Add(Me.gbTVSourcesRegexMultiPartMatching, 0, 1)
        Me.tblTVSourcesRegex.Controls.Add(Me.gbTVSourcesRegexTVShowMatching, 0, 0)
        Me.tblTVSourcesRegex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesRegex.Location = New System.Drawing.Point(0, 0)
        Me.tblTVSourcesRegex.Name = "tblTVSourcesRegex"
        Me.tblTVSourcesRegex.RowCount = 3
        Me.tblTVSourcesRegex.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegex.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegex.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegex.Size = New System.Drawing.Size(822, 474)
        Me.tblTVSourcesRegex.TabIndex = 9
        '
        'gbTVSourcesRegexMultiPartMatching
        '
        Me.gbTVSourcesRegexMultiPartMatching.AutoSize = True
        Me.gbTVSourcesRegexMultiPartMatching.Controls.Add(Me.tblTVSourcesRegexMultiPartMatching)
        Me.gbTVSourcesRegexMultiPartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVSourcesRegexMultiPartMatching.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTVSourcesRegexMultiPartMatching.Location = New System.Drawing.Point(3, 377)
        Me.gbTVSourcesRegexMultiPartMatching.Name = "gbTVSourcesRegexMultiPartMatching"
        Me.gbTVSourcesRegexMultiPartMatching.Size = New System.Drawing.Size(812, 50)
        Me.gbTVSourcesRegexMultiPartMatching.TabIndex = 8
        Me.gbTVSourcesRegexMultiPartMatching.TabStop = False
        Me.gbTVSourcesRegexMultiPartMatching.Text = "TV Show Multi Part Matching"
        '
        'tblTVSourcesRegexMultiPartMatching
        '
        Me.tblTVSourcesRegexMultiPartMatching.AutoSize = True
        Me.tblTVSourcesRegexMultiPartMatching.ColumnCount = 2
        Me.tblTVSourcesRegexMultiPartMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexMultiPartMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexMultiPartMatching.Controls.Add(Me.txtTVSourcesRegexMultiPartMatching, 0, 0)
        Me.tblTVSourcesRegexMultiPartMatching.Controls.Add(Me.btnTVSourcesRegexMultiPartMatchingReset, 1, 0)
        Me.tblTVSourcesRegexMultiPartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesRegexMultiPartMatching.Location = New System.Drawing.Point(3, 18)
        Me.tblTVSourcesRegexMultiPartMatching.Name = "tblTVSourcesRegexMultiPartMatching"
        Me.tblTVSourcesRegexMultiPartMatching.RowCount = 2
        Me.tblTVSourcesRegexMultiPartMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexMultiPartMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexMultiPartMatching.Size = New System.Drawing.Size(806, 29)
        Me.tblTVSourcesRegexMultiPartMatching.TabIndex = 0
        '
        'txtTVSourcesRegexMultiPartMatching
        '
        Me.txtTVSourcesRegexMultiPartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTVSourcesRegexMultiPartMatching.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtTVSourcesRegexMultiPartMatching.Location = New System.Drawing.Point(3, 3)
        Me.txtTVSourcesRegexMultiPartMatching.Name = "txtTVSourcesRegexMultiPartMatching"
        Me.txtTVSourcesRegexMultiPartMatching.Size = New System.Drawing.Size(759, 22)
        Me.txtTVSourcesRegexMultiPartMatching.TabIndex = 0
        '
        'btnTVSourcesRegexMultiPartMatchingReset
        '
        Me.btnTVSourcesRegexMultiPartMatchingReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTVSourcesRegexMultiPartMatchingReset.Image = CType(resources.GetObject("btnTVSourcesRegexMultiPartMatchingReset.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexMultiPartMatchingReset.Location = New System.Drawing.Point(780, 3)
        Me.btnTVSourcesRegexMultiPartMatchingReset.Name = "btnTVSourcesRegexMultiPartMatchingReset"
        Me.btnTVSourcesRegexMultiPartMatchingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSourcesRegexMultiPartMatchingReset.TabIndex = 3
        Me.btnTVSourcesRegexMultiPartMatchingReset.UseVisualStyleBackColor = True
        '
        'gbTVSourcesRegexTVShowMatching
        '
        Me.gbTVSourcesRegexTVShowMatching.AutoSize = True
        Me.gbTVSourcesRegexTVShowMatching.Controls.Add(Me.tblTVSourcesRegexTVShowMatching)
        Me.gbTVSourcesRegexTVShowMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVSourcesRegexTVShowMatching.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTVSourcesRegexTVShowMatching.Location = New System.Drawing.Point(3, 3)
        Me.gbTVSourcesRegexTVShowMatching.Name = "gbTVSourcesRegexTVShowMatching"
        Me.gbTVSourcesRegexTVShowMatching.Size = New System.Drawing.Size(812, 368)
        Me.gbTVSourcesRegexTVShowMatching.TabIndex = 7
        Me.gbTVSourcesRegexTVShowMatching.TabStop = False
        Me.gbTVSourcesRegexTVShowMatching.Text = "TV Show Matching"
        '
        'tblTVSourcesRegexTVShowMatching
        '
        Me.tblTVSourcesRegexTVShowMatching.AutoSize = True
        Me.tblTVSourcesRegexTVShowMatching.ColumnCount = 8
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.lvTVSourcesRegexTVShowMatching, 0, 1)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingClear, 0, 4)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingDown, 3, 2)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingUp, 2, 2)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingEdit, 0, 2)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingReset, 6, 0)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingRemove, 5, 2)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingAdd, 5, 4)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.tblTVSourcesRegexTVShowMatchingEdit, 0, 3)
        Me.tblTVSourcesRegexTVShowMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesRegexTVShowMatching.Location = New System.Drawing.Point(3, 18)
        Me.tblTVSourcesRegexTVShowMatching.Name = "tblTVSourcesRegexTVShowMatching"
        Me.tblTVSourcesRegexTVShowMatching.RowCount = 6
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.Size = New System.Drawing.Size(806, 347)
        Me.tblTVSourcesRegexTVShowMatching.TabIndex = 8
        '
        'lvTVSourcesRegexTVShowMatching
        '
        Me.lvTVSourcesRegexTVShowMatching.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTVSourcesRegexTVShowMatchingID, Me.colTVSourcesRegexTVShowMatchingRegex, Me.coTVSourcesRegexTVShowMatchingDefaultSeason, Me.colTVSourcesRegexTVShowMatchingByDate})
        Me.tblTVSourcesRegexTVShowMatching.SetColumnSpan(Me.lvTVSourcesRegexTVShowMatching, 7)
        Me.lvTVSourcesRegexTVShowMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTVSourcesRegexTVShowMatching.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvTVSourcesRegexTVShowMatching.FullRowSelect = True
        Me.lvTVSourcesRegexTVShowMatching.HideSelection = False
        Me.lvTVSourcesRegexTVShowMatching.Location = New System.Drawing.Point(3, 32)
        Me.lvTVSourcesRegexTVShowMatching.Name = "lvTVSourcesRegexTVShowMatching"
        Me.lvTVSourcesRegexTVShowMatching.Size = New System.Drawing.Size(800, 200)
        Me.lvTVSourcesRegexTVShowMatching.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTVSourcesRegexTVShowMatching.TabIndex = 0
        Me.lvTVSourcesRegexTVShowMatching.UseCompatibleStateImageBehavior = False
        Me.lvTVSourcesRegexTVShowMatching.View = System.Windows.Forms.View.Details
        '
        'colTVSourcesRegexTVShowMatchingID
        '
        Me.colTVSourcesRegexTVShowMatchingID.DisplayIndex = 2
        Me.colTVSourcesRegexTVShowMatchingID.Width = 0
        '
        'colTVSourcesRegexTVShowMatchingRegex
        '
        Me.colTVSourcesRegexTVShowMatchingRegex.DisplayIndex = 0
        Me.colTVSourcesRegexTVShowMatchingRegex.Text = "Regex"
        Me.colTVSourcesRegexTVShowMatchingRegex.Width = 600
        '
        'coTVSourcesRegexTVShowMatchingDefaultSeason
        '
        Me.coTVSourcesRegexTVShowMatchingDefaultSeason.DisplayIndex = 1
        Me.coTVSourcesRegexTVShowMatchingDefaultSeason.Text = "Default Season"
        Me.coTVSourcesRegexTVShowMatchingDefaultSeason.Width = 90
        '
        'colTVSourcesRegexTVShowMatchingByDate
        '
        Me.colTVSourcesRegexTVShowMatchingByDate.Text = "by Date"
        '
        'btnTVSourcesRegexTVShowMatchingClear
        '
        Me.btnTVSourcesRegexTVShowMatchingClear.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVSourcesRegexTVShowMatchingClear.AutoSize = True
        Me.btnTVSourcesRegexTVShowMatchingClear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVSourcesRegexTVShowMatchingClear.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingClear.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourcesRegexTVShowMatchingClear.Location = New System.Drawing.Point(3, 321)
        Me.btnTVSourcesRegexTVShowMatchingClear.Name = "btnTVSourcesRegexTVShowMatchingClear"
        Me.btnTVSourcesRegexTVShowMatchingClear.Size = New System.Drawing.Size(100, 23)
        Me.btnTVSourcesRegexTVShowMatchingClear.TabIndex = 8
        Me.btnTVSourcesRegexTVShowMatchingClear.Text = "Clear"
        Me.btnTVSourcesRegexTVShowMatchingClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourcesRegexTVShowMatchingClear.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingDown
        '
        Me.btnTVSourcesRegexTVShowMatchingDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVSourcesRegexTVShowMatchingDown.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingDown.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingDown.Location = New System.Drawing.Point(406, 238)
        Me.btnTVSourcesRegexTVShowMatchingDown.Name = "btnTVSourcesRegexTVShowMatchingDown"
        Me.btnTVSourcesRegexTVShowMatchingDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSourcesRegexTVShowMatchingDown.TabIndex = 5
        Me.btnTVSourcesRegexTVShowMatchingDown.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingUp
        '
        Me.btnTVSourcesRegexTVShowMatchingUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVSourcesRegexTVShowMatchingUp.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingUp.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingUp.Location = New System.Drawing.Point(377, 238)
        Me.btnTVSourcesRegexTVShowMatchingUp.Name = "btnTVSourcesRegexTVShowMatchingUp"
        Me.btnTVSourcesRegexTVShowMatchingUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSourcesRegexTVShowMatchingUp.TabIndex = 4
        Me.btnTVSourcesRegexTVShowMatchingUp.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingEdit
        '
        Me.btnTVSourcesRegexTVShowMatchingEdit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVSourcesRegexTVShowMatchingEdit.AutoSize = True
        Me.btnTVSourcesRegexTVShowMatchingEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVSourcesRegexTVShowMatchingEdit.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingEdit.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourcesRegexTVShowMatchingEdit.Location = New System.Drawing.Point(3, 238)
        Me.btnTVSourcesRegexTVShowMatchingEdit.Name = "btnTVSourcesRegexTVShowMatchingEdit"
        Me.btnTVSourcesRegexTVShowMatchingEdit.Size = New System.Drawing.Size(100, 23)
        Me.btnTVSourcesRegexTVShowMatchingEdit.TabIndex = 3
        Me.btnTVSourcesRegexTVShowMatchingEdit.Text = "Edit Regex"
        Me.btnTVSourcesRegexTVShowMatchingEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourcesRegexTVShowMatchingEdit.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingReset
        '
        Me.btnTVSourcesRegexTVShowMatchingReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVSourcesRegexTVShowMatchingReset.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingReset.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingReset.Location = New System.Drawing.Point(780, 3)
        Me.btnTVSourcesRegexTVShowMatchingReset.Name = "btnTVSourcesRegexTVShowMatchingReset"
        Me.btnTVSourcesRegexTVShowMatchingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSourcesRegexTVShowMatchingReset.TabIndex = 2
        Me.btnTVSourcesRegexTVShowMatchingReset.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingRemove
        '
        Me.btnTVSourcesRegexTVShowMatchingRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVSourcesRegexTVShowMatchingRemove.AutoSize = True
        Me.tblTVSourcesRegexTVShowMatching.SetColumnSpan(Me.btnTVSourcesRegexTVShowMatchingRemove, 2)
        Me.btnTVSourcesRegexTVShowMatchingRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVSourcesRegexTVShowMatchingRemove.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingRemove.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourcesRegexTVShowMatchingRemove.Location = New System.Drawing.Point(703, 238)
        Me.btnTVSourcesRegexTVShowMatchingRemove.Name = "btnTVSourcesRegexTVShowMatchingRemove"
        Me.btnTVSourcesRegexTVShowMatchingRemove.Size = New System.Drawing.Size(100, 23)
        Me.btnTVSourcesRegexTVShowMatchingRemove.TabIndex = 6
        Me.btnTVSourcesRegexTVShowMatchingRemove.Text = "Remove"
        Me.btnTVSourcesRegexTVShowMatchingRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourcesRegexTVShowMatchingRemove.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingAdd
        '
        Me.btnTVSourcesRegexTVShowMatchingAdd.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVSourcesRegexTVShowMatchingAdd.AutoSize = True
        Me.tblTVSourcesRegexTVShowMatching.SetColumnSpan(Me.btnTVSourcesRegexTVShowMatchingAdd, 2)
        Me.btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        Me.btnTVSourcesRegexTVShowMatchingAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTVSourcesRegexTVShowMatchingAdd.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingAdd.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourcesRegexTVShowMatchingAdd.Location = New System.Drawing.Point(703, 321)
        Me.btnTVSourcesRegexTVShowMatchingAdd.Name = "btnTVSourcesRegexTVShowMatchingAdd"
        Me.btnTVSourcesRegexTVShowMatchingAdd.Size = New System.Drawing.Size(100, 23)
        Me.btnTVSourcesRegexTVShowMatchingAdd.TabIndex = 9
        Me.btnTVSourcesRegexTVShowMatchingAdd.Text = "Add Regex"
        Me.btnTVSourcesRegexTVShowMatchingAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourcesRegexTVShowMatchingAdd.UseVisualStyleBackColor = True
        '
        'tblTVSourcesRegexTVShowMatchingEdit
        '
        Me.tblTVSourcesRegexTVShowMatchingEdit.AutoSize = True
        Me.tblTVSourcesRegexTVShowMatchingEdit.ColumnCount = 3
        Me.tblTVSourcesRegexTVShowMatching.SetColumnSpan(Me.tblTVSourcesRegexTVShowMatchingEdit, 7)
        Me.tblTVSourcesRegexTVShowMatchingEdit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatchingEdit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatchingEdit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.lblTVSourcesRegexTVShowMatchingRegex, 0, 0)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.lblTVSourcesRegexTVShowMatchingDefaultSeason, 1, 0)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.txtTVSourcesRegexTVShowMatchingRegex, 0, 1)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.txtTVSourcesRegexTVShowMatchingDefaultSeason, 1, 1)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.lblTVSourcesRegexTVShowMatchingByDate, 2, 0)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.chkTVSourcesRegexTVShowMatchingByDate, 2, 1)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesRegexTVShowMatchingEdit.Location = New System.Drawing.Point(3, 267)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Name = "tblTVSourcesRegexTVShowMatchingEdit"
        Me.tblTVSourcesRegexTVShowMatchingEdit.RowCount = 2
        Me.tblTVSourcesRegexTVShowMatchingEdit.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesRegexTVShowMatchingEdit.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatchingEdit.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesRegexTVShowMatchingEdit.Size = New System.Drawing.Size(800, 48)
        Me.tblTVSourcesRegexTVShowMatchingEdit.TabIndex = 12
        '
        'lblTVSourcesRegexTVShowMatchingRegex
        '
        Me.lblTVSourcesRegexTVShowMatchingRegex.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTVSourcesRegexTVShowMatchingRegex.AutoSize = True
        Me.lblTVSourcesRegexTVShowMatchingRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVSourcesRegexTVShowMatchingRegex.Location = New System.Drawing.Point(3, 3)
        Me.lblTVSourcesRegexTVShowMatchingRegex.Name = "lblTVSourcesRegexTVShowMatchingRegex"
        Me.lblTVSourcesRegexTVShowMatchingRegex.Size = New System.Drawing.Size(38, 13)
        Me.lblTVSourcesRegexTVShowMatchingRegex.TabIndex = 0
        Me.lblTVSourcesRegexTVShowMatchingRegex.Text = "Regex"
        '
        'lblTVSourcesRegexTVShowMatchingDefaultSeason
        '
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.AutoSize = True
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Location = New System.Drawing.Point(575, 3)
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Name = "lblTVSourcesRegexTVShowMatchingDefaultSeason"
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Size = New System.Drawing.Size(85, 13)
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.TabIndex = 2
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Text = "Default Season"
        '
        'txtTVSourcesRegexTVShowMatchingRegex
        '
        Me.txtTVSourcesRegexTVShowMatchingRegex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTVSourcesRegexTVShowMatchingRegex.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVSourcesRegexTVShowMatchingRegex.Location = New System.Drawing.Point(3, 23)
        Me.txtTVSourcesRegexTVShowMatchingRegex.Name = "txtTVSourcesRegexTVShowMatchingRegex"
        Me.txtTVSourcesRegexTVShowMatchingRegex.Size = New System.Drawing.Size(566, 22)
        Me.txtTVSourcesRegexTVShowMatchingRegex.TabIndex = 1
        '
        'txtTVSourcesRegexTVShowMatchingDefaultSeason
        '
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Location = New System.Drawing.Point(575, 23)
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Name = "txtTVSourcesRegexTVShowMatchingDefaultSeason"
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Size = New System.Drawing.Size(90, 22)
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.TabIndex = 11
        '
        'lblTVSourcesRegexTVShowMatchingByDate
        '
        Me.lblTVSourcesRegexTVShowMatchingByDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTVSourcesRegexTVShowMatchingByDate.AutoSize = True
        Me.lblTVSourcesRegexTVShowMatchingByDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVSourcesRegexTVShowMatchingByDate.Location = New System.Drawing.Point(711, 3)
        Me.lblTVSourcesRegexTVShowMatchingByDate.Name = "lblTVSourcesRegexTVShowMatchingByDate"
        Me.lblTVSourcesRegexTVShowMatchingByDate.Size = New System.Drawing.Size(46, 13)
        Me.lblTVSourcesRegexTVShowMatchingByDate.TabIndex = 6
        Me.lblTVSourcesRegexTVShowMatchingByDate.Text = "by Date"
        '
        'chkTVSourcesRegexTVShowMatchingByDate
        '
        Me.chkTVSourcesRegexTVShowMatchingByDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTVSourcesRegexTVShowMatchingByDate.AutoSize = True
        Me.chkTVSourcesRegexTVShowMatchingByDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVSourcesRegexTVShowMatchingByDate.Location = New System.Drawing.Point(726, 27)
        Me.chkTVSourcesRegexTVShowMatchingByDate.Name = "chkTVSourcesRegexTVShowMatchingByDate"
        Me.chkTVSourcesRegexTVShowMatchingByDate.Size = New System.Drawing.Size(15, 14)
        Me.chkTVSourcesRegexTVShowMatchingByDate.TabIndex = 10
        Me.chkTVSourcesRegexTVShowMatchingByDate.UseVisualStyleBackColor = True
        '
        'chkTVGeneralMarkNewShows
        '
        Me.chkTVGeneralMarkNewShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVGeneralMarkNewShows.AutoSize = True
        Me.chkTVGeneralMarkNewShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVGeneralMarkNewShows.Location = New System.Drawing.Point(136, 54)
        Me.chkTVGeneralMarkNewShows.Name = "chkTVGeneralMarkNewShows"
        Me.chkTVGeneralMarkNewShows.Size = New System.Drawing.Size(115, 17)
        Me.chkTVGeneralMarkNewShows.TabIndex = 6
        Me.chkTVGeneralMarkNewShows.Text = "Mark New Shows"
        Me.chkTVGeneralMarkNewShows.UseVisualStyleBackColor = True
        '
        'chkTVGeneralMarkNewEpisodes
        '
        Me.chkTVGeneralMarkNewEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVGeneralMarkNewEpisodes.AutoSize = True
        Me.chkTVGeneralMarkNewEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVGeneralMarkNewEpisodes.Location = New System.Drawing.Point(3, 54)
        Me.chkTVGeneralMarkNewEpisodes.Name = "chkTVGeneralMarkNewEpisodes"
        Me.chkTVGeneralMarkNewEpisodes.Size = New System.Drawing.Size(127, 17)
        Me.chkTVGeneralMarkNewEpisodes.TabIndex = 7
        Me.chkTVGeneralMarkNewEpisodes.Text = "Mark New Episodes"
        Me.chkTVGeneralMarkNewEpisodes.UseVisualStyleBackColor = True
        '
        'gbTVShowFilterOpts
        '
        Me.gbTVShowFilterOpts.AutoSize = True
        Me.gbTVShowFilterOpts.Controls.Add(Me.tblTVShowFilterOpts)
        Me.gbTVShowFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVShowFilterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVShowFilterOpts.Location = New System.Drawing.Point(335, 336)
        Me.gbTVShowFilterOpts.Name = "gbTVShowFilterOpts"
        Me.gbTVShowFilterOpts.Size = New System.Drawing.Size(358, 203)
        Me.gbTVShowFilterOpts.TabIndex = 5
        Me.gbTVShowFilterOpts.TabStop = False
        Me.gbTVShowFilterOpts.Text = "Show Folder/File Name Filters"
        '
        'tblTVShowFilterOpts
        '
        Me.tblTVShowFilterOpts.AutoSize = True
        Me.tblTVShowFilterOpts.ColumnCount = 6
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterRemove, 4, 2)
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterDown, 3, 2)
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterReset, 4, 0)
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterUp, 2, 2)
        Me.tblTVShowFilterOpts.Controls.Add(Me.chkTVShowProperCase, 0, 0)
        Me.tblTVShowFilterOpts.Controls.Add(Me.lstTVShowFilter, 0, 1)
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterAdd, 1, 2)
        Me.tblTVShowFilterOpts.Controls.Add(Me.txtTVShowFilter, 0, 2)
        Me.tblTVShowFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVShowFilterOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVShowFilterOpts.Name = "tblTVShowFilterOpts"
        Me.tblTVShowFilterOpts.RowCount = 4
        Me.tblTVShowFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVShowFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVShowFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVShowFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVShowFilterOpts.Size = New System.Drawing.Size(352, 182)
        Me.tblTVShowFilterOpts.TabIndex = 5
        '
        'btnTVShowFilterRemove
        '
        Me.btnTVShowFilterRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVShowFilterRemove.Image = CType(resources.GetObject("btnTVShowFilterRemove.Image"), System.Drawing.Image)
        Me.btnTVShowFilterRemove.Location = New System.Drawing.Point(294, 133)
        Me.btnTVShowFilterRemove.Name = "btnTVShowFilterRemove"
        Me.btnTVShowFilterRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterRemove.TabIndex = 7
        Me.btnTVShowFilterRemove.UseVisualStyleBackColor = True
        '
        'btnTVShowFilterDown
        '
        Me.btnTVShowFilterDown.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVShowFilterDown.Image = CType(resources.GetObject("btnTVShowFilterDown.Image"), System.Drawing.Image)
        Me.btnTVShowFilterDown.Location = New System.Drawing.Point(262, 133)
        Me.btnTVShowFilterDown.Name = "btnTVShowFilterDown"
        Me.btnTVShowFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterDown.TabIndex = 6
        Me.btnTVShowFilterDown.UseVisualStyleBackColor = True
        '
        'btnTVShowFilterReset
        '
        Me.btnTVShowFilterReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVShowFilterReset.Image = CType(resources.GetObject("btnTVShowFilterReset.Image"), System.Drawing.Image)
        Me.btnTVShowFilterReset.Location = New System.Drawing.Point(294, 3)
        Me.btnTVShowFilterReset.Name = "btnTVShowFilterReset"
        Me.btnTVShowFilterReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterReset.TabIndex = 2
        Me.btnTVShowFilterReset.UseVisualStyleBackColor = True
        '
        'btnTVShowFilterUp
        '
        Me.btnTVShowFilterUp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVShowFilterUp.Image = CType(resources.GetObject("btnTVShowFilterUp.Image"), System.Drawing.Image)
        Me.btnTVShowFilterUp.Location = New System.Drawing.Point(233, 133)
        Me.btnTVShowFilterUp.Name = "btnTVShowFilterUp"
        Me.btnTVShowFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterUp.TabIndex = 5
        Me.btnTVShowFilterUp.UseVisualStyleBackColor = True
        '
        'chkTVShowProperCase
        '
        Me.chkTVShowProperCase.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVShowProperCase.AutoSize = True
        Me.tblTVShowFilterOpts.SetColumnSpan(Me.chkTVShowProperCase, 4)
        Me.chkTVShowProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVShowProperCase.Location = New System.Drawing.Point(3, 6)
        Me.chkTVShowProperCase.Name = "chkTVShowProperCase"
        Me.chkTVShowProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkTVShowProperCase.TabIndex = 0
        Me.chkTVShowProperCase.Text = "Convert Names to Proper Case"
        Me.chkTVShowProperCase.UseVisualStyleBackColor = True
        '
        'lstTVShowFilter
        '
        Me.tblTVShowFilterOpts.SetColumnSpan(Me.lstTVShowFilter, 5)
        Me.lstTVShowFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTVShowFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstTVShowFilter.FormattingEnabled = True
        Me.lstTVShowFilter.Location = New System.Drawing.Point(3, 32)
        Me.lstTVShowFilter.Name = "lstTVShowFilter"
        Me.lstTVShowFilter.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTVShowFilter.Size = New System.Drawing.Size(314, 95)
        Me.lstTVShowFilter.TabIndex = 1
        '
        'btnTVShowFilterAdd
        '
        Me.btnTVShowFilterAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVShowFilterAdd.Image = CType(resources.GetObject("btnTVShowFilterAdd.Image"), System.Drawing.Image)
        Me.btnTVShowFilterAdd.Location = New System.Drawing.Point(204, 133)
        Me.btnTVShowFilterAdd.Name = "btnTVShowFilterAdd"
        Me.btnTVShowFilterAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterAdd.TabIndex = 4
        Me.btnTVShowFilterAdd.UseVisualStyleBackColor = True
        '
        'txtTVShowFilter
        '
        Me.txtTVShowFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTVShowFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVShowFilter.Location = New System.Drawing.Point(3, 133)
        Me.txtTVShowFilter.Name = "txtTVShowFilter"
        Me.txtTVShowFilter.Size = New System.Drawing.Size(195, 22)
        Me.txtTVShowFilter.TabIndex = 3
        '
        'gbTVEpisodeFilterOpts
        '
        Me.gbTVEpisodeFilterOpts.AutoSize = True
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.tblTVEpisodeFilterOpts)
        Me.gbTVEpisodeFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVEpisodeFilterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVEpisodeFilterOpts.Location = New System.Drawing.Point(3, 336)
        Me.gbTVEpisodeFilterOpts.Name = "gbTVEpisodeFilterOpts"
        Me.gbTVEpisodeFilterOpts.Size = New System.Drawing.Size(326, 203)
        Me.gbTVEpisodeFilterOpts.TabIndex = 6
        Me.gbTVEpisodeFilterOpts.TabStop = False
        Me.gbTVEpisodeFilterOpts.Text = "Episode Folder/File Name Filters"
        '
        'tblTVEpisodeFilterOpts
        '
        Me.tblTVEpisodeFilterOpts.AutoSize = True
        Me.tblTVEpisodeFilterOpts.ColumnCount = 6
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterRemove, 4, 3)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterDown, 3, 3)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterReset, 4, 1)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterUp, 2, 3)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.chkTVEpisodeNoFilter, 0, 0)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.chkTVEpisodeProperCase, 0, 1)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterAdd, 1, 3)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.lstTVEpisodeFilter, 0, 2)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.txtTVEpisodeFilter, 0, 3)
        Me.tblTVEpisodeFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVEpisodeFilterOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVEpisodeFilterOpts.Name = "tblTVEpisodeFilterOpts"
        Me.tblTVEpisodeFilterOpts.RowCount = 5
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.Size = New System.Drawing.Size(320, 182)
        Me.tblTVEpisodeFilterOpts.TabIndex = 5
        '
        'btnTVEpisodeFilterRemove
        '
        Me.btnTVEpisodeFilterRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVEpisodeFilterRemove.Image = CType(resources.GetObject("btnTVEpisodeFilterRemove.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterRemove.Location = New System.Drawing.Point(294, 156)
        Me.btnTVEpisodeFilterRemove.Name = "btnTVEpisodeFilterRemove"
        Me.btnTVEpisodeFilterRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterRemove.TabIndex = 8
        Me.btnTVEpisodeFilterRemove.UseVisualStyleBackColor = True
        '
        'btnTVEpisodeFilterDown
        '
        Me.btnTVEpisodeFilterDown.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVEpisodeFilterDown.Image = CType(resources.GetObject("btnTVEpisodeFilterDown.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterDown.Location = New System.Drawing.Point(262, 156)
        Me.btnTVEpisodeFilterDown.Name = "btnTVEpisodeFilterDown"
        Me.btnTVEpisodeFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterDown.TabIndex = 7
        Me.btnTVEpisodeFilterDown.UseVisualStyleBackColor = True
        '
        'btnTVEpisodeFilterReset
        '
        Me.btnTVEpisodeFilterReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVEpisodeFilterReset.Image = CType(resources.GetObject("btnTVEpisodeFilterReset.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterReset.Location = New System.Drawing.Point(294, 26)
        Me.btnTVEpisodeFilterReset.Name = "btnTVEpisodeFilterReset"
        Me.btnTVEpisodeFilterReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterReset.TabIndex = 3
        Me.btnTVEpisodeFilterReset.UseVisualStyleBackColor = True
        '
        'btnTVEpisodeFilterUp
        '
        Me.btnTVEpisodeFilterUp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVEpisodeFilterUp.Image = CType(resources.GetObject("btnTVEpisodeFilterUp.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterUp.Location = New System.Drawing.Point(233, 156)
        Me.btnTVEpisodeFilterUp.Name = "btnTVEpisodeFilterUp"
        Me.btnTVEpisodeFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterUp.TabIndex = 6
        Me.btnTVEpisodeFilterUp.UseVisualStyleBackColor = True
        '
        'chkTVEpisodeNoFilter
        '
        Me.chkTVEpisodeNoFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVEpisodeNoFilter.AutoSize = True
        Me.chkTVEpisodeNoFilter.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblTVEpisodeFilterOpts.SetColumnSpan(Me.chkTVEpisodeNoFilter, 5)
        Me.chkTVEpisodeNoFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkTVEpisodeNoFilter.Location = New System.Drawing.Point(3, 3)
        Me.chkTVEpisodeNoFilter.Name = "chkTVEpisodeNoFilter"
        Me.chkTVEpisodeNoFilter.Size = New System.Drawing.Size(222, 17)
        Me.chkTVEpisodeNoFilter.TabIndex = 0
        Me.chkTVEpisodeNoFilter.Text = "Build Episode Title Instead of Filtering"
        Me.chkTVEpisodeNoFilter.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVEpisodeNoFilter.UseVisualStyleBackColor = True
        '
        'chkTVEpisodeProperCase
        '
        Me.chkTVEpisodeProperCase.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVEpisodeProperCase.AutoSize = True
        Me.tblTVEpisodeFilterOpts.SetColumnSpan(Me.chkTVEpisodeProperCase, 4)
        Me.chkTVEpisodeProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVEpisodeProperCase.Location = New System.Drawing.Point(3, 29)
        Me.chkTVEpisodeProperCase.Name = "chkTVEpisodeProperCase"
        Me.chkTVEpisodeProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkTVEpisodeProperCase.TabIndex = 1
        Me.chkTVEpisodeProperCase.Text = "Convert Names to Proper Case"
        Me.chkTVEpisodeProperCase.UseVisualStyleBackColor = True
        '
        'btnTVEpisodeFilterAdd
        '
        Me.btnTVEpisodeFilterAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVEpisodeFilterAdd.Image = CType(resources.GetObject("btnTVEpisodeFilterAdd.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterAdd.Location = New System.Drawing.Point(204, 156)
        Me.btnTVEpisodeFilterAdd.Name = "btnTVEpisodeFilterAdd"
        Me.btnTVEpisodeFilterAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterAdd.TabIndex = 5
        Me.btnTVEpisodeFilterAdd.UseVisualStyleBackColor = True
        '
        'lstTVEpisodeFilter
        '
        Me.tblTVEpisodeFilterOpts.SetColumnSpan(Me.lstTVEpisodeFilter, 5)
        Me.lstTVEpisodeFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTVEpisodeFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstTVEpisodeFilter.FormattingEnabled = True
        Me.lstTVEpisodeFilter.Location = New System.Drawing.Point(3, 55)
        Me.lstTVEpisodeFilter.Name = "lstTVEpisodeFilter"
        Me.lstTVEpisodeFilter.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTVEpisodeFilter.Size = New System.Drawing.Size(314, 95)
        Me.lstTVEpisodeFilter.TabIndex = 2
        '
        'txtTVEpisodeFilter
        '
        Me.txtTVEpisodeFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTVEpisodeFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVEpisodeFilter.Location = New System.Drawing.Point(3, 156)
        Me.txtTVEpisodeFilter.Name = "txtTVEpisodeFilter"
        Me.txtTVEpisodeFilter.Size = New System.Drawing.Size(195, 22)
        Me.txtTVEpisodeFilter.TabIndex = 4
        '
        'frmTV_Source
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(898, 658)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmTV_Source"
        Me.Text = "frmTV_Source"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tcTVSources.ResumeLayout(False)
        Me.tpTVSourcesGeneral.ResumeLayout(False)
        Me.tpTVSourcesGeneral.PerformLayout()
        Me.tblTVSourcesGeneral.ResumeLayout(False)
        Me.tblTVSourcesGeneral.PerformLayout()
        Me.gbTVSourcesMiscOpts.ResumeLayout(False)
        Me.gbTVSourcesMiscOpts.PerformLayout()
        Me.tblTVSourcesMiscOpts.ResumeLayout(False)
        Me.tblTVSourcesMiscOpts.PerformLayout()
        Me.gbTVSourcesDefaultsOpts.ResumeLayout(False)
        Me.gbTVSourcesDefaultsOpts.PerformLayout()
        Me.tblTVSourcesDefaultsOpts.ResumeLayout(False)
        Me.tblTVSourcesDefaultsOpts.PerformLayout()
        Me.tpTVSourcesRegex.ResumeLayout(False)
        Me.tpTVSourcesRegex.PerformLayout()
        Me.tblTVSourcesRegex.ResumeLayout(False)
        Me.tblTVSourcesRegex.PerformLayout()
        Me.gbTVSourcesRegexMultiPartMatching.ResumeLayout(False)
        Me.gbTVSourcesRegexMultiPartMatching.PerformLayout()
        Me.tblTVSourcesRegexMultiPartMatching.ResumeLayout(False)
        Me.tblTVSourcesRegexMultiPartMatching.PerformLayout()
        Me.gbTVSourcesRegexTVShowMatching.ResumeLayout(False)
        Me.gbTVSourcesRegexTVShowMatching.PerformLayout()
        Me.tblTVSourcesRegexTVShowMatching.ResumeLayout(False)
        Me.tblTVSourcesRegexTVShowMatching.PerformLayout()
        Me.tblTVSourcesRegexTVShowMatchingEdit.ResumeLayout(False)
        Me.tblTVSourcesRegexTVShowMatchingEdit.PerformLayout()
        Me.gbTVShowFilterOpts.ResumeLayout(False)
        Me.gbTVShowFilterOpts.PerformLayout()
        Me.tblTVShowFilterOpts.ResumeLayout(False)
        Me.tblTVShowFilterOpts.PerformLayout()
        Me.gbTVEpisodeFilterOpts.ResumeLayout(False)
        Me.gbTVEpisodeFilterOpts.PerformLayout()
        Me.tblTVEpisodeFilterOpts.ResumeLayout(False)
        Me.tblTVEpisodeFilterOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents tcTVSources As TabControl
    Friend WithEvents tpTVSourcesGeneral As TabPage
    Friend WithEvents tblTVSourcesGeneral As TableLayoutPanel
    Friend WithEvents lvTVSources As ListView
    Friend WithEvents colTVSourcesID As ColumnHeader
    Friend WithEvents colTVSourcesName As ColumnHeader
    Friend WithEvents colTVSourcesPath As ColumnHeader
    Friend WithEvents colTVSourcesLanguage As ColumnHeader
    Friend WithEvents colTVSourcesOrdering As ColumnHeader
    Friend WithEvents colTVSourcesExclude As ColumnHeader
    Friend WithEvents colTVSourcesSorting As ColumnHeader
    Friend WithEvents colTVSourcesSingle As ColumnHeader
    Friend WithEvents gbTVSourcesMiscOpts As GroupBox
    Friend WithEvents tblTVSourcesMiscOpts As TableLayoutPanel
    Friend WithEvents chkTVCleanDB As CheckBox
    Friend WithEvents lblTVSkipLessThanMB As Label
    Friend WithEvents lblTVSkipLessThan As Label
    Friend WithEvents txtTVSkipLessThan As TextBox
    Friend WithEvents btnRemTVSource As Button
    Friend WithEvents btnTVSourceEdit As Button
    Friend WithEvents btnTVSourceAdd As Button
    Friend WithEvents gbTVSourcesDefaultsOpts As GroupBox
    Friend WithEvents tblTVSourcesDefaultsOpts As TableLayoutPanel
    Friend WithEvents cbTVGeneralLang As ComboBox
    Friend WithEvents lblTVSourcesDefaultsOrdering As Label
    Friend WithEvents cbTVScraperOptionsOrdering As ComboBox
    Friend WithEvents lblTVSourcesDefaultsLanguage As Label
    Friend WithEvents tpTVSourcesRegex As TabPage
    Friend WithEvents tblTVSourcesRegex As TableLayoutPanel
    Friend WithEvents gbTVSourcesRegexMultiPartMatching As GroupBox
    Friend WithEvents tblTVSourcesRegexMultiPartMatching As TableLayoutPanel
    Friend WithEvents txtTVSourcesRegexMultiPartMatching As TextBox
    Friend WithEvents btnTVSourcesRegexMultiPartMatchingReset As Button
    Friend WithEvents gbTVSourcesRegexTVShowMatching As GroupBox
    Friend WithEvents tblTVSourcesRegexTVShowMatching As TableLayoutPanel
    Friend WithEvents lvTVSourcesRegexTVShowMatching As ListView
    Friend WithEvents colTVSourcesRegexTVShowMatchingID As ColumnHeader
    Friend WithEvents colTVSourcesRegexTVShowMatchingRegex As ColumnHeader
    Friend WithEvents coTVSourcesRegexTVShowMatchingDefaultSeason As ColumnHeader
    Friend WithEvents colTVSourcesRegexTVShowMatchingByDate As ColumnHeader
    Friend WithEvents btnTVSourcesRegexTVShowMatchingClear As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingDown As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingUp As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingEdit As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingReset As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingRemove As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingAdd As Button
    Friend WithEvents tblTVSourcesRegexTVShowMatchingEdit As TableLayoutPanel
    Friend WithEvents lblTVSourcesRegexTVShowMatchingRegex As Label
    Friend WithEvents lblTVSourcesRegexTVShowMatchingDefaultSeason As Label
    Friend WithEvents txtTVSourcesRegexTVShowMatchingRegex As TextBox
    Friend WithEvents txtTVSourcesRegexTVShowMatchingDefaultSeason As TextBox
    Friend WithEvents lblTVSourcesRegexTVShowMatchingByDate As Label
    Friend WithEvents chkTVSourcesRegexTVShowMatchingByDate As CheckBox
    Friend WithEvents chkTVGeneralMarkNewShows As CheckBox
    Friend WithEvents chkTVGeneralMarkNewEpisodes As CheckBox
    Friend WithEvents gbTVShowFilterOpts As GroupBox
    Friend WithEvents tblTVShowFilterOpts As TableLayoutPanel
    Friend WithEvents btnTVShowFilterRemove As Button
    Friend WithEvents btnTVShowFilterDown As Button
    Friend WithEvents btnTVShowFilterReset As Button
    Friend WithEvents btnTVShowFilterUp As Button
    Friend WithEvents chkTVShowProperCase As CheckBox
    Friend WithEvents lstTVShowFilter As ListBox
    Friend WithEvents btnTVShowFilterAdd As Button
    Friend WithEvents txtTVShowFilter As TextBox
    Friend WithEvents gbTVEpisodeFilterOpts As GroupBox
    Friend WithEvents tblTVEpisodeFilterOpts As TableLayoutPanel
    Friend WithEvents btnTVEpisodeFilterRemove As Button
    Friend WithEvents btnTVEpisodeFilterDown As Button
    Friend WithEvents btnTVEpisodeFilterReset As Button
    Friend WithEvents btnTVEpisodeFilterUp As Button
    Friend WithEvents chkTVEpisodeNoFilter As CheckBox
    Friend WithEvents chkTVEpisodeProperCase As CheckBox
    Friend WithEvents btnTVEpisodeFilterAdd As Button
    Friend WithEvents lstTVEpisodeFilter As ListBox
    Friend WithEvents txtTVEpisodeFilter As TextBox
End Class
