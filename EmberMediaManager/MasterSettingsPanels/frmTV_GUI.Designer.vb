<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTV_GUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTV_GUI))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblTVGeneral = New System.Windows.Forms.TableLayoutPanel()
        Me.gbCustomScrapeButton = New System.Windows.Forms.GroupBox()
        Me.tblCustomScrapeButton = New System.Windows.Forms.TableLayoutPanel()
        Me.cbCustomScrapeButtonSelectionType = New System.Windows.Forms.ComboBox()
        Me.cbCustomScrapeButtonScrapeType = New System.Windows.Forms.ComboBox()
        Me.cbCustomScrapeButtonModifierType = New System.Windows.Forms.ComboBox()
        Me.lblCustomScrapeButtonScrapeType = New System.Windows.Forms.Label()
        Me.lblCustomScrapeButtonModifierType = New System.Windows.Forms.Label()
        Me.rbCustomScrapeButtonEnabled = New System.Windows.Forms.RadioButton()
        Me.rbCustomScrapeButtonDisabled = New System.Windows.Forms.RadioButton()
        Me.lblCustomScrapeButtonSelectionType = New System.Windows.Forms.Label()
        Me.gbMediaList = New System.Windows.Forms.GroupBox()
        Me.tblMediaList = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMediaListSorting_TVEpisode = New System.Windows.Forms.GroupBox()
        Me.tblMediaListSorting_TVEpisode = New System.Windows.Forms.TableLayoutPanel()
        Me.chkClickScrapeShowResults_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.dgvMediaListSorting_TVEpisode = New System.Windows.Forms.DataGridView()
        Me.colMediaListSorting_DisplayIndex_TVEpisode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_Show_TVEpisode = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colMediaListSorting_Column_TVEpisode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_AutoSizeMode_TVEpisode = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.btnMediaListSortingDefaults_TVEpisode = New System.Windows.Forms.Button()
        Me.chkClickScrapeEnabled_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkDisplayMissingEpisodes = New System.Windows.Forms.CheckBox()
        Me.lblMediaListSorting_TVEpisode = New System.Windows.Forms.Label()
        Me.gbMediaListSorting_TVSeason = New System.Windows.Forms.GroupBox()
        Me.tblMediaListSorting_TVSeason = New System.Windows.Forms.TableLayoutPanel()
        Me.chkClickScrapeShowResults_TVSeason = New System.Windows.Forms.CheckBox()
        Me.dgvMediaListSorting_TVSeason = New System.Windows.Forms.DataGridView()
        Me.colMediaListSorting_DisplayIndex_TVSeason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_Show_TVSeason = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colMediaListSorting_Column_TVSeason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_AutoSizeMode_TVSeason = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chkClickScrapeEnabled_TVSeason = New System.Windows.Forms.CheckBox()
        Me.btnMediaListSortingDefaults_TVSeason = New System.Windows.Forms.Button()
        Me.lblMediaListSorting_TVSeason = New System.Windows.Forms.Label()
        Me.gbMediaListSorting_TVShow = New System.Windows.Forms.GroupBox()
        Me.tblMediaListSorting_TVShow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkClickScrapeShowResults_TVShow = New System.Windows.Forms.CheckBox()
        Me.chkClickScrapeEnabled_TVShow = New System.Windows.Forms.CheckBox()
        Me.dgvMediaListSorting_TVShow = New System.Windows.Forms.DataGridView()
        Me.colMediaListSorting_DisplayIndex_TVShow = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_Show_TVShow = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colMediaListSorting_Column_TVShow = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_AutoSizeMode_TVShow = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.btnMediaListSortingDefaults_TVShow = New System.Windows.Forms.Button()
        Me.lblMediaListSorting_TVShow = New System.Windows.Forms.Label()
        Me.gbMainWindow = New System.Windows.Forms.GroupBox()
        Me.tblMainWindow = New System.Windows.Forms.TableLayoutPanel()
        Me.lblLanguageOverlay = New System.Windows.Forms.Label()
        Me.cbLanguageOverlay = New System.Windows.Forms.ComboBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblTVGeneral.SuspendLayout()
        Me.gbCustomScrapeButton.SuspendLayout()
        Me.tblCustomScrapeButton.SuspendLayout()
        Me.gbMediaList.SuspendLayout()
        Me.tblMediaList.SuspendLayout()
        Me.gbMediaListSorting_TVEpisode.SuspendLayout()
        Me.tblMediaListSorting_TVEpisode.SuspendLayout()
        CType(Me.dgvMediaListSorting_TVEpisode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMediaListSorting_TVSeason.SuspendLayout()
        Me.tblMediaListSorting_TVSeason.SuspendLayout()
        CType(Me.dgvMediaListSorting_TVSeason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMediaListSorting_TVShow.SuspendLayout()
        Me.tblMediaListSorting_TVShow.SuspendLayout()
        CType(Me.dgvMediaListSorting_TVShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMainWindow.SuspendLayout()
        Me.tblMainWindow.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblTVGeneral)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(1094, 623)
        Me.pnlSettings.TabIndex = 21
        Me.pnlSettings.Visible = False
        '
        'tblTVGeneral
        '
        Me.tblTVGeneral.AutoScroll = True
        Me.tblTVGeneral.AutoSize = True
        Me.tblTVGeneral.ColumnCount = 3
        Me.tblTVGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneral.Controls.Add(Me.gbCustomScrapeButton, 0, 1)
        Me.tblTVGeneral.Controls.Add(Me.gbMediaList, 0, 0)
        Me.tblTVGeneral.Controls.Add(Me.gbMainWindow, 1, 1)
        Me.tblTVGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneral.Location = New System.Drawing.Point(0, 0)
        Me.tblTVGeneral.Name = "tblTVGeneral"
        Me.tblTVGeneral.RowCount = 3
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVGeneral.Size = New System.Drawing.Size(1094, 623)
        Me.tblTVGeneral.TabIndex = 5
        '
        'gbCustomScrapeButton
        '
        Me.gbCustomScrapeButton.AutoSize = True
        Me.gbCustomScrapeButton.Controls.Add(Me.tblCustomScrapeButton)
        Me.gbCustomScrapeButton.Location = New System.Drawing.Point(3, 438)
        Me.gbCustomScrapeButton.Name = "gbCustomScrapeButton"
        Me.gbCustomScrapeButton.Size = New System.Drawing.Size(468, 125)
        Me.gbCustomScrapeButton.TabIndex = 13
        Me.gbCustomScrapeButton.TabStop = False
        Me.gbCustomScrapeButton.Text = "Scrape Button"
        '
        'tblCustomScrapeButton
        '
        Me.tblCustomScrapeButton.AutoSize = True
        Me.tblCustomScrapeButton.ColumnCount = 2
        Me.tblCustomScrapeButton.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomScrapeButton.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblCustomScrapeButton.Controls.Add(Me.cbCustomScrapeButtonSelectionType, 1, 1)
        Me.tblCustomScrapeButton.Controls.Add(Me.cbCustomScrapeButtonScrapeType, 1, 2)
        Me.tblCustomScrapeButton.Controls.Add(Me.cbCustomScrapeButtonModifierType, 1, 3)
        Me.tblCustomScrapeButton.Controls.Add(Me.lblCustomScrapeButtonScrapeType, 0, 2)
        Me.tblCustomScrapeButton.Controls.Add(Me.lblCustomScrapeButtonModifierType, 0, 3)
        Me.tblCustomScrapeButton.Controls.Add(Me.rbCustomScrapeButtonEnabled, 1, 0)
        Me.tblCustomScrapeButton.Controls.Add(Me.rbCustomScrapeButtonDisabled, 0, 0)
        Me.tblCustomScrapeButton.Controls.Add(Me.lblCustomScrapeButtonSelectionType, 0, 1)
        Me.tblCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCustomScrapeButton.Location = New System.Drawing.Point(3, 18)
        Me.tblCustomScrapeButton.Name = "tblCustomScrapeButton"
        Me.tblCustomScrapeButton.RowCount = 5
        Me.tblCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomScrapeButton.Size = New System.Drawing.Size(462, 104)
        Me.tblCustomScrapeButton.TabIndex = 0
        '
        'cbCustomScrapeButtonSelectionType
        '
        Me.cbCustomScrapeButtonSelectionType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbCustomScrapeButtonSelectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCustomScrapeButtonSelectionType.Enabled = False
        Me.cbCustomScrapeButtonSelectionType.FormattingEnabled = True
        Me.cbCustomScrapeButtonSelectionType.Location = New System.Drawing.Point(159, 26)
        Me.cbCustomScrapeButtonSelectionType.Name = "cbCustomScrapeButtonSelectionType"
        Me.cbCustomScrapeButtonSelectionType.Size = New System.Drawing.Size(300, 21)
        Me.cbCustomScrapeButtonSelectionType.TabIndex = 7
        '
        'cbCustomScrapeButtonScrapeType
        '
        Me.cbCustomScrapeButtonScrapeType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbCustomScrapeButtonScrapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCustomScrapeButtonScrapeType.Enabled = False
        Me.cbCustomScrapeButtonScrapeType.FormattingEnabled = True
        Me.cbCustomScrapeButtonScrapeType.Location = New System.Drawing.Point(159, 53)
        Me.cbCustomScrapeButtonScrapeType.Name = "cbCustomScrapeButtonScrapeType"
        Me.cbCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(300, 21)
        Me.cbCustomScrapeButtonScrapeType.TabIndex = 1
        '
        'cbCustomScrapeButtonModifierType
        '
        Me.cbCustomScrapeButtonModifierType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbCustomScrapeButtonModifierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCustomScrapeButtonModifierType.Enabled = False
        Me.cbCustomScrapeButtonModifierType.FormattingEnabled = True
        Me.cbCustomScrapeButtonModifierType.Location = New System.Drawing.Point(159, 80)
        Me.cbCustomScrapeButtonModifierType.Name = "cbCustomScrapeButtonModifierType"
        Me.cbCustomScrapeButtonModifierType.Size = New System.Drawing.Size(300, 21)
        Me.cbCustomScrapeButtonModifierType.TabIndex = 2
        '
        'lblCustomScrapeButtonScrapeType
        '
        Me.lblCustomScrapeButtonScrapeType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomScrapeButtonScrapeType.AutoSize = True
        Me.lblCustomScrapeButtonScrapeType.Enabled = False
        Me.lblCustomScrapeButtonScrapeType.Location = New System.Drawing.Point(3, 57)
        Me.lblCustomScrapeButtonScrapeType.Name = "lblCustomScrapeButtonScrapeType"
        Me.lblCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(67, 13)
        Me.lblCustomScrapeButtonScrapeType.TabIndex = 3
        Me.lblCustomScrapeButtonScrapeType.Text = "Scrape Type"
        '
        'lblCustomScrapeButtonModifierType
        '
        Me.lblCustomScrapeButtonModifierType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomScrapeButtonModifierType.AutoSize = True
        Me.lblCustomScrapeButtonModifierType.Enabled = False
        Me.lblCustomScrapeButtonModifierType.Location = New System.Drawing.Point(3, 84)
        Me.lblCustomScrapeButtonModifierType.Name = "lblCustomScrapeButtonModifierType"
        Me.lblCustomScrapeButtonModifierType.Size = New System.Drawing.Size(77, 13)
        Me.lblCustomScrapeButtonModifierType.TabIndex = 4
        Me.lblCustomScrapeButtonModifierType.Text = "Modifier Type"
        '
        'rbCustomScrapeButtonEnabled
        '
        Me.rbCustomScrapeButtonEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbCustomScrapeButtonEnabled.AutoSize = True
        Me.rbCustomScrapeButtonEnabled.Location = New System.Drawing.Point(159, 3)
        Me.rbCustomScrapeButtonEnabled.Name = "rbCustomScrapeButtonEnabled"
        Me.rbCustomScrapeButtonEnabled.Size = New System.Drawing.Size(150, 17)
        Me.rbCustomScrapeButtonEnabled.TabIndex = 5
        Me.rbCustomScrapeButtonEnabled.TabStop = True
        Me.rbCustomScrapeButtonEnabled.Text = "Custom Scrape Function"
        Me.rbCustomScrapeButtonEnabled.UseVisualStyleBackColor = True
        '
        'rbCustomScrapeButtonDisabled
        '
        Me.rbCustomScrapeButtonDisabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbCustomScrapeButtonDisabled.AutoSize = True
        Me.rbCustomScrapeButtonDisabled.Location = New System.Drawing.Point(3, 3)
        Me.rbCustomScrapeButtonDisabled.Name = "rbCustomScrapeButtonDisabled"
        Me.rbCustomScrapeButtonDisabled.Size = New System.Drawing.Size(150, 17)
        Me.rbCustomScrapeButtonDisabled.TabIndex = 6
        Me.rbCustomScrapeButtonDisabled.TabStop = True
        Me.rbCustomScrapeButtonDisabled.Text = "Open Drop Down Menu"
        Me.rbCustomScrapeButtonDisabled.UseVisualStyleBackColor = True
        '
        'lblCustomScrapeButtonSelectionType
        '
        Me.lblCustomScrapeButtonSelectionType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomScrapeButtonSelectionType.AutoSize = True
        Me.lblCustomScrapeButtonSelectionType.Enabled = False
        Me.lblCustomScrapeButtonSelectionType.Location = New System.Drawing.Point(3, 30)
        Me.lblCustomScrapeButtonSelectionType.Name = "lblCustomScrapeButtonSelectionType"
        Me.lblCustomScrapeButtonSelectionType.Size = New System.Drawing.Size(80, 13)
        Me.lblCustomScrapeButtonSelectionType.TabIndex = 3
        Me.lblCustomScrapeButtonSelectionType.Text = "Selection Type"
        '
        'gbMediaList
        '
        Me.gbMediaList.AutoSize = True
        Me.tblTVGeneral.SetColumnSpan(Me.gbMediaList, 2)
        Me.gbMediaList.Controls.Add(Me.tblMediaList)
        Me.gbMediaList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMediaList.Location = New System.Drawing.Point(3, 3)
        Me.gbMediaList.Name = "gbMediaList"
        Me.gbMediaList.Size = New System.Drawing.Size(1047, 429)
        Me.gbMediaList.TabIndex = 1
        Me.gbMediaList.TabStop = False
        Me.gbMediaList.Text = "Media List"
        '
        'tblMediaList
        '
        Me.tblMediaList.AutoSize = True
        Me.tblMediaList.ColumnCount = 3
        Me.tblMediaList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaList.Controls.Add(Me.gbMediaListSorting_TVEpisode, 2, 0)
        Me.tblMediaList.Controls.Add(Me.gbMediaListSorting_TVSeason, 1, 0)
        Me.tblMediaList.Controls.Add(Me.gbMediaListSorting_TVShow, 0, 0)
        Me.tblMediaList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaList.Location = New System.Drawing.Point(3, 18)
        Me.tblMediaList.Name = "tblMediaList"
        Me.tblMediaList.RowCount = 1
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 406.0!))
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 406.0!))
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 406.0!))
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 406.0!))
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 406.0!))
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 406.0!))
        Me.tblMediaList.Size = New System.Drawing.Size(1041, 408)
        Me.tblMediaList.TabIndex = 74
        '
        'gbMediaListSorting_TVEpisode
        '
        Me.gbMediaListSorting_TVEpisode.AutoSize = True
        Me.gbMediaListSorting_TVEpisode.Controls.Add(Me.tblMediaListSorting_TVEpisode)
        Me.gbMediaListSorting_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMediaListSorting_TVEpisode.Location = New System.Drawing.Point(697, 3)
        Me.gbMediaListSorting_TVEpisode.Name = "gbMediaListSorting_TVEpisode"
        Me.gbMediaListSorting_TVEpisode.Size = New System.Drawing.Size(341, 402)
        Me.gbMediaListSorting_TVEpisode.TabIndex = 76
        Me.gbMediaListSorting_TVEpisode.TabStop = False
        Me.gbMediaListSorting_TVEpisode.Text = "Episodes"
        '
        'tblMediaListSorting_TVEpisode
        '
        Me.tblMediaListSorting_TVEpisode.AutoSize = True
        Me.tblMediaListSorting_TVEpisode.ColumnCount = 3
        Me.tblMediaListSorting_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVEpisode.Controls.Add(Me.chkClickScrapeShowResults_TVEpisode, 1, 0)
        Me.tblMediaListSorting_TVEpisode.Controls.Add(Me.dgvMediaListSorting_TVEpisode, 0, 2)
        Me.tblMediaListSorting_TVEpisode.Controls.Add(Me.btnMediaListSortingDefaults_TVEpisode, 2, 3)
        Me.tblMediaListSorting_TVEpisode.Controls.Add(Me.chkClickScrapeEnabled_TVEpisode, 0, 0)
        Me.tblMediaListSorting_TVEpisode.Controls.Add(Me.chkDisplayMissingEpisodes, 0, 1)
        Me.tblMediaListSorting_TVEpisode.Controls.Add(Me.lblMediaListSorting_TVEpisode, 0, 3)
        Me.tblMediaListSorting_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaListSorting_TVEpisode.Location = New System.Drawing.Point(3, 18)
        Me.tblMediaListSorting_TVEpisode.Name = "tblMediaListSorting_TVEpisode"
        Me.tblMediaListSorting_TVEpisode.RowCount = 4
        Me.tblMediaListSorting_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListSorting_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListSorting_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMediaListSorting_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListSorting_TVEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListSorting_TVEpisode.Size = New System.Drawing.Size(335, 381)
        Me.tblMediaListSorting_TVEpisode.TabIndex = 0
        '
        'chkClickScrapeShowResults_TVEpisode
        '
        Me.chkClickScrapeShowResults_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeShowResults_TVEpisode.AutoSize = True
        Me.tblMediaListSorting_TVEpisode.SetColumnSpan(Me.chkClickScrapeShowResults_TVEpisode, 2)
        Me.chkClickScrapeShowResults_TVEpisode.Location = New System.Drawing.Point(135, 3)
        Me.chkClickScrapeShowResults_TVEpisode.Name = "chkClickScrapeShowResults_TVEpisode"
        Me.chkClickScrapeShowResults_TVEpisode.Size = New System.Drawing.Size(132, 17)
        Me.chkClickScrapeShowResults_TVEpisode.TabIndex = 67
        Me.chkClickScrapeShowResults_TVEpisode.Text = "Show Results Dialog"
        Me.chkClickScrapeShowResults_TVEpisode.UseVisualStyleBackColor = True
        '
        'dgvMediaListSorting_TVEpisode
        '
        Me.dgvMediaListSorting_TVEpisode.AllowUserToAddRows = False
        Me.dgvMediaListSorting_TVEpisode.AllowUserToDeleteRows = False
        Me.dgvMediaListSorting_TVEpisode.AllowUserToResizeRows = False
        Me.dgvMediaListSorting_TVEpisode.BackgroundColor = System.Drawing.Color.White
        Me.dgvMediaListSorting_TVEpisode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMediaListSorting_TVEpisode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMediaListSorting_TVEpisode.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMediaListSorting_DisplayIndex_TVEpisode, Me.colMediaListSorting_Show_TVEpisode, Me.colMediaListSorting_Column_TVEpisode, Me.colMediaListSorting_AutoSizeMode_TVEpisode})
        Me.tblMediaListSorting_TVEpisode.SetColumnSpan(Me.dgvMediaListSorting_TVEpisode, 3)
        Me.dgvMediaListSorting_TVEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMediaListSorting_TVEpisode.Location = New System.Drawing.Point(3, 49)
        Me.dgvMediaListSorting_TVEpisode.MultiSelect = False
        Me.dgvMediaListSorting_TVEpisode.Name = "dgvMediaListSorting_TVEpisode"
        Me.dgvMediaListSorting_TVEpisode.RowHeadersVisible = False
        Me.dgvMediaListSorting_TVEpisode.Size = New System.Drawing.Size(329, 300)
        Me.dgvMediaListSorting_TVEpisode.TabIndex = 1
        '
        'colMediaListSorting_DisplayIndex_TVEpisode
        '
        Me.colMediaListSorting_DisplayIndex_TVEpisode.HeaderText = "DisplayIndex"
        Me.colMediaListSorting_DisplayIndex_TVEpisode.Name = "colMediaListSorting_DisplayIndex_TVEpisode"
        Me.colMediaListSorting_DisplayIndex_TVEpisode.ReadOnly = True
        Me.colMediaListSorting_DisplayIndex_TVEpisode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMediaListSorting_DisplayIndex_TVEpisode.Visible = False
        Me.colMediaListSorting_DisplayIndex_TVEpisode.Width = 30
        '
        'colMediaListSorting_Show_TVEpisode
        '
        Me.colMediaListSorting_Show_TVEpisode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colMediaListSorting_Show_TVEpisode.HeaderText = "Show"
        Me.colMediaListSorting_Show_TVEpisode.Name = "colMediaListSorting_Show_TVEpisode"
        Me.colMediaListSorting_Show_TVEpisode.Width = 42
        '
        'colMediaListSorting_Column_TVEpisode
        '
        Me.colMediaListSorting_Column_TVEpisode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMediaListSorting_Column_TVEpisode.HeaderText = "Column"
        Me.colMediaListSorting_Column_TVEpisode.Name = "colMediaListSorting_Column_TVEpisode"
        Me.colMediaListSorting_Column_TVEpisode.ReadOnly = True
        Me.colMediaListSorting_Column_TVEpisode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colMediaListSorting_AutoSizeMode_TVEpisode
        '
        Me.colMediaListSorting_AutoSizeMode_TVEpisode.HeaderText = "AutoSizeMode"
        Me.colMediaListSorting_AutoSizeMode_TVEpisode.Name = "colMediaListSorting_AutoSizeMode_TVEpisode"
        '
        'btnMediaListSortingDefaults_TVEpisode
        '
        Me.btnMediaListSortingDefaults_TVEpisode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMediaListSortingDefaults_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMediaListSortingDefaults_TVEpisode.Image = CType(resources.GetObject("btnMediaListSortingDefaults_TVEpisode.Image"), System.Drawing.Image)
        Me.btnMediaListSortingDefaults_TVEpisode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMediaListSortingDefaults_TVEpisode.Location = New System.Drawing.Point(227, 355)
        Me.btnMediaListSortingDefaults_TVEpisode.Name = "btnMediaListSortingDefaults_TVEpisode"
        Me.btnMediaListSortingDefaults_TVEpisode.Size = New System.Drawing.Size(105, 23)
        Me.btnMediaListSortingDefaults_TVEpisode.TabIndex = 2
        Me.btnMediaListSortingDefaults_TVEpisode.Text = "Defaults"
        Me.btnMediaListSortingDefaults_TVEpisode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMediaListSortingDefaults_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkClickScrapeEnabled_TVEpisode
        '
        Me.chkClickScrapeEnabled_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeEnabled_TVEpisode.AutoSize = True
        Me.chkClickScrapeEnabled_TVEpisode.Location = New System.Drawing.Point(3, 3)
        Me.chkClickScrapeEnabled_TVEpisode.Name = "chkClickScrapeEnabled_TVEpisode"
        Me.chkClickScrapeEnabled_TVEpisode.Size = New System.Drawing.Size(126, 17)
        Me.chkClickScrapeEnabled_TVEpisode.TabIndex = 66
        Me.chkClickScrapeEnabled_TVEpisode.Text = "Enable Click-Scrape"
        Me.chkClickScrapeEnabled_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkDisplayMissingEpisodes
        '
        Me.chkDisplayMissingEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDisplayMissingEpisodes.AutoSize = True
        Me.tblMediaListSorting_TVEpisode.SetColumnSpan(Me.chkDisplayMissingEpisodes, 3)
        Me.chkDisplayMissingEpisodes.Location = New System.Drawing.Point(3, 26)
        Me.chkDisplayMissingEpisodes.Name = "chkDisplayMissingEpisodes"
        Me.chkDisplayMissingEpisodes.Size = New System.Drawing.Size(155, 17)
        Me.chkDisplayMissingEpisodes.TabIndex = 3
        Me.chkDisplayMissingEpisodes.Text = "Display Missing Episodes"
        Me.chkDisplayMissingEpisodes.UseVisualStyleBackColor = True
        '
        'lblMediaListSorting_TVEpisode
        '
        Me.lblMediaListSorting_TVEpisode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMediaListSorting_TVEpisode.AutoSize = True
        Me.tblMediaListSorting_TVEpisode.SetColumnSpan(Me.lblMediaListSorting_TVEpisode, 2)
        Me.lblMediaListSorting_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMediaListSorting_TVEpisode.Location = New System.Drawing.Point(3, 360)
        Me.lblMediaListSorting_TVEpisode.Name = "lblMediaListSorting_TVEpisode"
        Me.lblMediaListSorting_TVEpisode.Size = New System.Drawing.Size(201, 13)
        Me.lblMediaListSorting_TVEpisode.TabIndex = 3
        Me.lblMediaListSorting_TVEpisode.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'gbMediaListSorting_TVSeason
        '
        Me.gbMediaListSorting_TVSeason.AutoSize = True
        Me.gbMediaListSorting_TVSeason.Controls.Add(Me.tblMediaListSorting_TVSeason)
        Me.gbMediaListSorting_TVSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMediaListSorting_TVSeason.Location = New System.Drawing.Point(350, 3)
        Me.gbMediaListSorting_TVSeason.Name = "gbMediaListSorting_TVSeason"
        Me.gbMediaListSorting_TVSeason.Size = New System.Drawing.Size(341, 402)
        Me.gbMediaListSorting_TVSeason.TabIndex = 75
        Me.gbMediaListSorting_TVSeason.TabStop = False
        Me.gbMediaListSorting_TVSeason.Text = "Seasons"
        '
        'tblMediaListSorting_TVSeason
        '
        Me.tblMediaListSorting_TVSeason.AutoSize = True
        Me.tblMediaListSorting_TVSeason.ColumnCount = 3
        Me.tblMediaListSorting_TVSeason.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVSeason.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVSeason.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVSeason.Controls.Add(Me.chkClickScrapeShowResults_TVSeason, 1, 0)
        Me.tblMediaListSorting_TVSeason.Controls.Add(Me.dgvMediaListSorting_TVSeason, 0, 1)
        Me.tblMediaListSorting_TVSeason.Controls.Add(Me.chkClickScrapeEnabled_TVSeason, 0, 0)
        Me.tblMediaListSorting_TVSeason.Controls.Add(Me.btnMediaListSortingDefaults_TVSeason, 2, 2)
        Me.tblMediaListSorting_TVSeason.Controls.Add(Me.lblMediaListSorting_TVSeason, 0, 2)
        Me.tblMediaListSorting_TVSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaListSorting_TVSeason.Location = New System.Drawing.Point(3, 18)
        Me.tblMediaListSorting_TVSeason.Name = "tblMediaListSorting_TVSeason"
        Me.tblMediaListSorting_TVSeason.RowCount = 3
        Me.tblMediaListSorting_TVSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListSorting_TVSeason.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMediaListSorting_TVSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListSorting_TVSeason.Size = New System.Drawing.Size(335, 381)
        Me.tblMediaListSorting_TVSeason.TabIndex = 0
        '
        'chkClickScrapeShowResults_TVSeason
        '
        Me.chkClickScrapeShowResults_TVSeason.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeShowResults_TVSeason.AutoSize = True
        Me.tblMediaListSorting_TVSeason.SetColumnSpan(Me.chkClickScrapeShowResults_TVSeason, 2)
        Me.chkClickScrapeShowResults_TVSeason.Location = New System.Drawing.Point(135, 3)
        Me.chkClickScrapeShowResults_TVSeason.Name = "chkClickScrapeShowResults_TVSeason"
        Me.chkClickScrapeShowResults_TVSeason.Size = New System.Drawing.Size(132, 17)
        Me.chkClickScrapeShowResults_TVSeason.TabIndex = 67
        Me.chkClickScrapeShowResults_TVSeason.Text = "Show Results Dialog"
        Me.chkClickScrapeShowResults_TVSeason.UseVisualStyleBackColor = True
        '
        'dgvMediaListSorting_TVSeason
        '
        Me.dgvMediaListSorting_TVSeason.AllowUserToAddRows = False
        Me.dgvMediaListSorting_TVSeason.AllowUserToDeleteRows = False
        Me.dgvMediaListSorting_TVSeason.AllowUserToResizeRows = False
        Me.dgvMediaListSorting_TVSeason.BackgroundColor = System.Drawing.Color.White
        Me.dgvMediaListSorting_TVSeason.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMediaListSorting_TVSeason.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMediaListSorting_TVSeason.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMediaListSorting_DisplayIndex_TVSeason, Me.colMediaListSorting_Show_TVSeason, Me.colMediaListSorting_Column_TVSeason, Me.colMediaListSorting_AutoSizeMode_TVSeason})
        Me.tblMediaListSorting_TVSeason.SetColumnSpan(Me.dgvMediaListSorting_TVSeason, 3)
        Me.dgvMediaListSorting_TVSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMediaListSorting_TVSeason.Location = New System.Drawing.Point(3, 26)
        Me.dgvMediaListSorting_TVSeason.MultiSelect = False
        Me.dgvMediaListSorting_TVSeason.Name = "dgvMediaListSorting_TVSeason"
        Me.dgvMediaListSorting_TVSeason.RowHeadersVisible = False
        Me.dgvMediaListSorting_TVSeason.Size = New System.Drawing.Size(329, 323)
        Me.dgvMediaListSorting_TVSeason.TabIndex = 1
        '
        'colMediaListSorting_DisplayIndex_TVSeason
        '
        Me.colMediaListSorting_DisplayIndex_TVSeason.HeaderText = "DisplayIndex"
        Me.colMediaListSorting_DisplayIndex_TVSeason.Name = "colMediaListSorting_DisplayIndex_TVSeason"
        Me.colMediaListSorting_DisplayIndex_TVSeason.ReadOnly = True
        Me.colMediaListSorting_DisplayIndex_TVSeason.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMediaListSorting_DisplayIndex_TVSeason.Visible = False
        Me.colMediaListSorting_DisplayIndex_TVSeason.Width = 30
        '
        'colMediaListSorting_Show_TVSeason
        '
        Me.colMediaListSorting_Show_TVSeason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colMediaListSorting_Show_TVSeason.HeaderText = "Show"
        Me.colMediaListSorting_Show_TVSeason.Name = "colMediaListSorting_Show_TVSeason"
        Me.colMediaListSorting_Show_TVSeason.Width = 42
        '
        'colMediaListSorting_Column_TVSeason
        '
        Me.colMediaListSorting_Column_TVSeason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMediaListSorting_Column_TVSeason.HeaderText = "Column"
        Me.colMediaListSorting_Column_TVSeason.Name = "colMediaListSorting_Column_TVSeason"
        Me.colMediaListSorting_Column_TVSeason.ReadOnly = True
        Me.colMediaListSorting_Column_TVSeason.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colMediaListSorting_AutoSizeMode_TVSeason
        '
        Me.colMediaListSorting_AutoSizeMode_TVSeason.HeaderText = "AutoSizeMode"
        Me.colMediaListSorting_AutoSizeMode_TVSeason.Name = "colMediaListSorting_AutoSizeMode_TVSeason"
        '
        'chkClickScrapeEnabled_TVSeason
        '
        Me.chkClickScrapeEnabled_TVSeason.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeEnabled_TVSeason.AutoSize = True
        Me.chkClickScrapeEnabled_TVSeason.Location = New System.Drawing.Point(3, 3)
        Me.chkClickScrapeEnabled_TVSeason.Name = "chkClickScrapeEnabled_TVSeason"
        Me.chkClickScrapeEnabled_TVSeason.Size = New System.Drawing.Size(126, 17)
        Me.chkClickScrapeEnabled_TVSeason.TabIndex = 66
        Me.chkClickScrapeEnabled_TVSeason.Text = "Enable Click-Scrape"
        Me.chkClickScrapeEnabled_TVSeason.UseVisualStyleBackColor = True
        '
        'btnMediaListSortingDefaults_TVSeason
        '
        Me.btnMediaListSortingDefaults_TVSeason.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMediaListSortingDefaults_TVSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMediaListSortingDefaults_TVSeason.Image = CType(resources.GetObject("btnMediaListSortingDefaults_TVSeason.Image"), System.Drawing.Image)
        Me.btnMediaListSortingDefaults_TVSeason.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMediaListSortingDefaults_TVSeason.Location = New System.Drawing.Point(227, 355)
        Me.btnMediaListSortingDefaults_TVSeason.Name = "btnMediaListSortingDefaults_TVSeason"
        Me.btnMediaListSortingDefaults_TVSeason.Size = New System.Drawing.Size(105, 23)
        Me.btnMediaListSortingDefaults_TVSeason.TabIndex = 2
        Me.btnMediaListSortingDefaults_TVSeason.Text = "Defaults"
        Me.btnMediaListSortingDefaults_TVSeason.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMediaListSortingDefaults_TVSeason.UseVisualStyleBackColor = True
        '
        'lblMediaListSorting_TVSeason
        '
        Me.lblMediaListSorting_TVSeason.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMediaListSorting_TVSeason.AutoSize = True
        Me.tblMediaListSorting_TVSeason.SetColumnSpan(Me.lblMediaListSorting_TVSeason, 2)
        Me.lblMediaListSorting_TVSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMediaListSorting_TVSeason.Location = New System.Drawing.Point(3, 360)
        Me.lblMediaListSorting_TVSeason.Name = "lblMediaListSorting_TVSeason"
        Me.lblMediaListSorting_TVSeason.Size = New System.Drawing.Size(201, 13)
        Me.lblMediaListSorting_TVSeason.TabIndex = 3
        Me.lblMediaListSorting_TVSeason.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'gbMediaListSorting_TVShow
        '
        Me.gbMediaListSorting_TVShow.AutoSize = True
        Me.gbMediaListSorting_TVShow.Controls.Add(Me.tblMediaListSorting_TVShow)
        Me.gbMediaListSorting_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMediaListSorting_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.gbMediaListSorting_TVShow.Name = "gbMediaListSorting_TVShow"
        Me.gbMediaListSorting_TVShow.Size = New System.Drawing.Size(341, 402)
        Me.gbMediaListSorting_TVShow.TabIndex = 74
        Me.gbMediaListSorting_TVShow.TabStop = False
        Me.gbMediaListSorting_TVShow.Text = "TV Shows"
        '
        'tblMediaListSorting_TVShow
        '
        Me.tblMediaListSorting_TVShow.AutoSize = True
        Me.tblMediaListSorting_TVShow.ColumnCount = 3
        Me.tblMediaListSorting_TVShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListSorting_TVShow.Controls.Add(Me.chkClickScrapeShowResults_TVShow, 1, 0)
        Me.tblMediaListSorting_TVShow.Controls.Add(Me.chkClickScrapeEnabled_TVShow, 0, 0)
        Me.tblMediaListSorting_TVShow.Controls.Add(Me.dgvMediaListSorting_TVShow, 0, 1)
        Me.tblMediaListSorting_TVShow.Controls.Add(Me.btnMediaListSortingDefaults_TVShow, 2, 2)
        Me.tblMediaListSorting_TVShow.Controls.Add(Me.lblMediaListSorting_TVShow, 0, 2)
        Me.tblMediaListSorting_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaListSorting_TVShow.Location = New System.Drawing.Point(3, 18)
        Me.tblMediaListSorting_TVShow.Name = "tblMediaListSorting_TVShow"
        Me.tblMediaListSorting_TVShow.RowCount = 3
        Me.tblMediaListSorting_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListSorting_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMediaListSorting_TVShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListSorting_TVShow.Size = New System.Drawing.Size(335, 381)
        Me.tblMediaListSorting_TVShow.TabIndex = 0
        '
        'chkClickScrapeShowResults_TVShow
        '
        Me.chkClickScrapeShowResults_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeShowResults_TVShow.AutoSize = True
        Me.tblMediaListSorting_TVShow.SetColumnSpan(Me.chkClickScrapeShowResults_TVShow, 2)
        Me.chkClickScrapeShowResults_TVShow.Location = New System.Drawing.Point(135, 3)
        Me.chkClickScrapeShowResults_TVShow.Name = "chkClickScrapeShowResults_TVShow"
        Me.chkClickScrapeShowResults_TVShow.Size = New System.Drawing.Size(132, 17)
        Me.chkClickScrapeShowResults_TVShow.TabIndex = 67
        Me.chkClickScrapeShowResults_TVShow.Text = "Show Results Dialog"
        Me.chkClickScrapeShowResults_TVShow.UseVisualStyleBackColor = True
        '
        'chkClickScrapeEnabled_TVShow
        '
        Me.chkClickScrapeEnabled_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeEnabled_TVShow.AutoSize = True
        Me.chkClickScrapeEnabled_TVShow.Location = New System.Drawing.Point(3, 3)
        Me.chkClickScrapeEnabled_TVShow.Name = "chkClickScrapeEnabled_TVShow"
        Me.chkClickScrapeEnabled_TVShow.Size = New System.Drawing.Size(126, 17)
        Me.chkClickScrapeEnabled_TVShow.TabIndex = 66
        Me.chkClickScrapeEnabled_TVShow.Text = "Enable Click-Scrape"
        Me.chkClickScrapeEnabled_TVShow.UseVisualStyleBackColor = True
        '
        'dgvMediaListSorting_TVShow
        '
        Me.dgvMediaListSorting_TVShow.AllowUserToAddRows = False
        Me.dgvMediaListSorting_TVShow.AllowUserToDeleteRows = False
        Me.dgvMediaListSorting_TVShow.AllowUserToResizeRows = False
        Me.dgvMediaListSorting_TVShow.BackgroundColor = System.Drawing.Color.White
        Me.dgvMediaListSorting_TVShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMediaListSorting_TVShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMediaListSorting_TVShow.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMediaListSorting_DisplayIndex_TVShow, Me.colMediaListSorting_Show_TVShow, Me.colMediaListSorting_Column_TVShow, Me.colMediaListSorting_AutoSizeMode_TVShow})
        Me.tblMediaListSorting_TVShow.SetColumnSpan(Me.dgvMediaListSorting_TVShow, 3)
        Me.dgvMediaListSorting_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMediaListSorting_TVShow.Location = New System.Drawing.Point(3, 26)
        Me.dgvMediaListSorting_TVShow.MultiSelect = False
        Me.dgvMediaListSorting_TVShow.Name = "dgvMediaListSorting_TVShow"
        Me.dgvMediaListSorting_TVShow.RowHeadersVisible = False
        Me.dgvMediaListSorting_TVShow.Size = New System.Drawing.Size(329, 323)
        Me.dgvMediaListSorting_TVShow.TabIndex = 1
        '
        'colMediaListSorting_DisplayIndex_TVShow
        '
        Me.colMediaListSorting_DisplayIndex_TVShow.HeaderText = "DisplayIndex"
        Me.colMediaListSorting_DisplayIndex_TVShow.Name = "colMediaListSorting_DisplayIndex_TVShow"
        Me.colMediaListSorting_DisplayIndex_TVShow.ReadOnly = True
        Me.colMediaListSorting_DisplayIndex_TVShow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMediaListSorting_DisplayIndex_TVShow.Visible = False
        Me.colMediaListSorting_DisplayIndex_TVShow.Width = 30
        '
        'colMediaListSorting_Show_TVShow
        '
        Me.colMediaListSorting_Show_TVShow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colMediaListSorting_Show_TVShow.HeaderText = "Show"
        Me.colMediaListSorting_Show_TVShow.Name = "colMediaListSorting_Show_TVShow"
        Me.colMediaListSorting_Show_TVShow.Width = 42
        '
        'colMediaListSorting_Column_TVShow
        '
        Me.colMediaListSorting_Column_TVShow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMediaListSorting_Column_TVShow.HeaderText = "Column"
        Me.colMediaListSorting_Column_TVShow.Name = "colMediaListSorting_Column_TVShow"
        Me.colMediaListSorting_Column_TVShow.ReadOnly = True
        Me.colMediaListSorting_Column_TVShow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colMediaListSorting_AutoSizeMode_TVShow
        '
        Me.colMediaListSorting_AutoSizeMode_TVShow.HeaderText = "AutoSizeMode"
        Me.colMediaListSorting_AutoSizeMode_TVShow.Name = "colMediaListSorting_AutoSizeMode_TVShow"
        '
        'btnMediaListSortingDefaults_TVShow
        '
        Me.btnMediaListSortingDefaults_TVShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMediaListSortingDefaults_TVShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMediaListSortingDefaults_TVShow.Image = CType(resources.GetObject("btnMediaListSortingDefaults_TVShow.Image"), System.Drawing.Image)
        Me.btnMediaListSortingDefaults_TVShow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMediaListSortingDefaults_TVShow.Location = New System.Drawing.Point(227, 355)
        Me.btnMediaListSortingDefaults_TVShow.Name = "btnMediaListSortingDefaults_TVShow"
        Me.btnMediaListSortingDefaults_TVShow.Size = New System.Drawing.Size(105, 23)
        Me.btnMediaListSortingDefaults_TVShow.TabIndex = 2
        Me.btnMediaListSortingDefaults_TVShow.Text = "Defaults"
        Me.btnMediaListSortingDefaults_TVShow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMediaListSortingDefaults_TVShow.UseVisualStyleBackColor = True
        '
        'lblMediaListSorting_TVShow
        '
        Me.lblMediaListSorting_TVShow.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMediaListSorting_TVShow.AutoSize = True
        Me.tblMediaListSorting_TVShow.SetColumnSpan(Me.lblMediaListSorting_TVShow, 2)
        Me.lblMediaListSorting_TVShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMediaListSorting_TVShow.Location = New System.Drawing.Point(3, 360)
        Me.lblMediaListSorting_TVShow.Name = "lblMediaListSorting_TVShow"
        Me.lblMediaListSorting_TVShow.Size = New System.Drawing.Size(201, 13)
        Me.lblMediaListSorting_TVShow.TabIndex = 3
        Me.lblMediaListSorting_TVShow.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'gbMainWindow
        '
        Me.gbMainWindow.AutoSize = True
        Me.gbMainWindow.Controls.Add(Me.tblMainWindow)
        Me.gbMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMainWindow.Location = New System.Drawing.Point(477, 438)
        Me.gbMainWindow.Name = "gbMainWindow"
        Me.gbMainWindow.Size = New System.Drawing.Size(573, 125)
        Me.gbMainWindow.TabIndex = 4
        Me.gbMainWindow.TabStop = False
        Me.gbMainWindow.Text = "Main Window"
        '
        'tblMainWindow
        '
        Me.tblMainWindow.AutoSize = True
        Me.tblMainWindow.ColumnCount = 2
        Me.tblMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainWindow.Controls.Add(Me.lblLanguageOverlay, 0, 0)
        Me.tblMainWindow.Controls.Add(Me.cbLanguageOverlay, 1, 0)
        Me.tblMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMainWindow.Location = New System.Drawing.Point(3, 18)
        Me.tblMainWindow.Name = "tblMainWindow"
        Me.tblMainWindow.RowCount = 3
        Me.tblMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainWindow.Size = New System.Drawing.Size(567, 104)
        Me.tblMainWindow.TabIndex = 0
        '
        'lblLanguageOverlay
        '
        Me.lblLanguageOverlay.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLanguageOverlay.AutoSize = True
        Me.lblLanguageOverlay.Location = New System.Drawing.Point(3, 0)
        Me.lblLanguageOverlay.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblLanguageOverlay.Name = "lblLanguageOverlay"
        Me.lblLanguageOverlay.Size = New System.Drawing.Size(243, 26)
        Me.lblLanguageOverlay.TabIndex = 1
        Me.lblLanguageOverlay.Text = "Display best Audio Stream with the following Language:"
        '
        'cbLanguageOverlay
        '
        Me.cbLanguageOverlay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbLanguageOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLanguageOverlay.FormattingEnabled = True
        Me.cbLanguageOverlay.Location = New System.Drawing.Point(252, 3)
        Me.cbLanguageOverlay.Name = "cbLanguageOverlay"
        Me.cbLanguageOverlay.Size = New System.Drawing.Size(312, 21)
        Me.cbLanguageOverlay.Sorted = True
        Me.cbLanguageOverlay.TabIndex = 2
        '
        'frmTV_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1094, 623)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmTV_GUI"
        Me.Text = "frmTV_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblTVGeneral.ResumeLayout(False)
        Me.tblTVGeneral.PerformLayout()
        Me.gbCustomScrapeButton.ResumeLayout(False)
        Me.gbCustomScrapeButton.PerformLayout()
        Me.tblCustomScrapeButton.ResumeLayout(False)
        Me.tblCustomScrapeButton.PerformLayout()
        Me.gbMediaList.ResumeLayout(False)
        Me.gbMediaList.PerformLayout()
        Me.tblMediaList.ResumeLayout(False)
        Me.tblMediaList.PerformLayout()
        Me.gbMediaListSorting_TVEpisode.ResumeLayout(False)
        Me.gbMediaListSorting_TVEpisode.PerformLayout()
        Me.tblMediaListSorting_TVEpisode.ResumeLayout(False)
        Me.tblMediaListSorting_TVEpisode.PerformLayout()
        CType(Me.dgvMediaListSorting_TVEpisode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMediaListSorting_TVSeason.ResumeLayout(False)
        Me.gbMediaListSorting_TVSeason.PerformLayout()
        Me.tblMediaListSorting_TVSeason.ResumeLayout(False)
        Me.tblMediaListSorting_TVSeason.PerformLayout()
        CType(Me.dgvMediaListSorting_TVSeason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMediaListSorting_TVShow.ResumeLayout(False)
        Me.gbMediaListSorting_TVShow.PerformLayout()
        Me.tblMediaListSorting_TVShow.ResumeLayout(False)
        Me.tblMediaListSorting_TVShow.PerformLayout()
        CType(Me.dgvMediaListSorting_TVShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMainWindow.ResumeLayout(False)
        Me.gbMainWindow.PerformLayout()
        Me.tblMainWindow.ResumeLayout(False)
        Me.tblMainWindow.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblTVGeneral As TableLayoutPanel
    Friend WithEvents gbMediaList As GroupBox
    Friend WithEvents tblMediaList As TableLayoutPanel
    Friend WithEvents gbMediaListSorting_TVEpisode As GroupBox
    Friend WithEvents tblMediaListSorting_TVEpisode As TableLayoutPanel
    Friend WithEvents gbMediaListSorting_TVSeason As GroupBox
    Friend WithEvents tblMediaListSorting_TVSeason As TableLayoutPanel
    Friend WithEvents gbMediaListSorting_TVShow As GroupBox
    Friend WithEvents tblMediaListSorting_TVShow As TableLayoutPanel
    Friend WithEvents dgvMediaListSorting_TVEpisode As DataGridView
    Friend WithEvents dgvMediaListSorting_TVSeason As DataGridView
    Friend WithEvents dgvMediaListSorting_TVShow As DataGridView
    Friend WithEvents lblMediaListSorting_TVShow As Label
    Friend WithEvents btnMediaListSortingDefaults_TVShow As Button
    Friend WithEvents btnMediaListSortingDefaults_TVEpisode As Button
    Friend WithEvents lblMediaListSorting_TVEpisode As Label
    Friend WithEvents btnMediaListSortingDefaults_TVSeason As Button
    Friend WithEvents lblMediaListSorting_TVSeason As Label
    Friend WithEvents gbCustomScrapeButton As GroupBox
    Friend WithEvents tblCustomScrapeButton As TableLayoutPanel
    Friend WithEvents cbCustomScrapeButtonScrapeType As ComboBox
    Friend WithEvents cbCustomScrapeButtonModifierType As ComboBox
    Friend WithEvents lblCustomScrapeButtonScrapeType As Label
    Friend WithEvents lblCustomScrapeButtonModifierType As Label
    Friend WithEvents rbCustomScrapeButtonEnabled As RadioButton
    Friend WithEvents rbCustomScrapeButtonDisabled As RadioButton
    Friend WithEvents chkClickScrapeEnabled_TVShow As CheckBox
    Friend WithEvents chkClickScrapeShowResults_TVShow As CheckBox
    Friend WithEvents chkDisplayMissingEpisodes As CheckBox
    Friend WithEvents gbMainWindow As GroupBox
    Friend WithEvents tblMainWindow As TableLayoutPanel
    Friend WithEvents lblLanguageOverlay As Label
    Friend WithEvents cbLanguageOverlay As ComboBox
    Friend WithEvents colMediaListSorting_DisplayIndex_TVEpisode As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_Show_TVEpisode As DataGridViewCheckBoxColumn
    Friend WithEvents colMediaListSorting_Column_TVEpisode As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_AutoSizeMode_TVEpisode As DataGridViewComboBoxColumn
    Friend WithEvents colMediaListSorting_DisplayIndex_TVSeason As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_Show_TVSeason As DataGridViewCheckBoxColumn
    Friend WithEvents colMediaListSorting_Column_TVSeason As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_AutoSizeMode_TVSeason As DataGridViewComboBoxColumn
    Friend WithEvents colMediaListSorting_DisplayIndex_TVShow As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_Show_TVShow As DataGridViewCheckBoxColumn
    Friend WithEvents colMediaListSorting_Column_TVShow As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_AutoSizeMode_TVShow As DataGridViewComboBoxColumn
    Friend WithEvents chkClickScrapeShowResults_TVSeason As CheckBox
    Friend WithEvents chkClickScrapeEnabled_TVSeason As CheckBox
    Friend WithEvents chkClickScrapeShowResults_TVEpisode As CheckBox
    Friend WithEvents chkClickScrapeEnabled_TVEpisode As CheckBox
    Friend WithEvents cbCustomScrapeButtonSelectionType As ComboBox
    Friend WithEvents lblCustomScrapeButtonSelectionType As Label
End Class
