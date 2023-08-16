<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovie_GUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovie_GUI))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMediaList = New System.Windows.Forms.GroupBox()
        Me.tblMediaList = New System.Windows.Forms.TableLayoutPanel()
        Me.txtLevTolerance = New System.Windows.Forms.TextBox()
        Me.dgvMediaListSorting = New System.Windows.Forms.DataGridView()
        Me.colMediaListSorting_DisplayIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_Show = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colMediaListSorting_Column = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_AutoSizeMode = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.lblLevTolerance = New System.Windows.Forms.Label()
        Me.btnMediaListSortingDefaults = New System.Windows.Forms.Button()
        Me.chkLevTolerance = New System.Windows.Forms.CheckBox()
        Me.lblMediaListSorting = New System.Windows.Forms.Label()
        Me.chkClickScrapeShowResults = New System.Windows.Forms.CheckBox()
        Me.chkClickScrapeEnabled = New System.Windows.Forms.CheckBox()
        Me.gbCustomMarker = New System.Windows.Forms.GroupBox()
        Me.tblCustomMarker = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCustomMarker4 = New System.Windows.Forms.Button()
        Me.lblCustomMarker1 = New System.Windows.Forms.Label()
        Me.btnCustomMarker3 = New System.Windows.Forms.Button()
        Me.txtCustomMarker4 = New System.Windows.Forms.TextBox()
        Me.btnCustomMarker2 = New System.Windows.Forms.Button()
        Me.lblCustomMarker2 = New System.Windows.Forms.Label()
        Me.btnCustomMarker1 = New System.Windows.Forms.Button()
        Me.lblCustomMarker4 = New System.Windows.Forms.Label()
        Me.txtCustomMarker3 = New System.Windows.Forms.TextBox()
        Me.lblCustomMarker3 = New System.Windows.Forms.Label()
        Me.txtCustomMarker1 = New System.Windows.Forms.TextBox()
        Me.txtCustomMarker2 = New System.Windows.Forms.TextBox()
        Me.gbMainWindow = New System.Windows.Forms.GroupBox()
        Me.tblMainWindow = New System.Windows.Forms.TableLayoutPanel()
        Me.lblLanguageOverlay = New System.Windows.Forms.Label()
        Me.cbLanguageOverlay = New System.Windows.Forms.ComboBox()
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
        Me.cdColor = New System.Windows.Forms.ColorDialog()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMediaList.SuspendLayout()
        Me.tblMediaList.SuspendLayout()
        CType(Me.dgvMediaListSorting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbCustomMarker.SuspendLayout()
        Me.tblCustomMarker.SuspendLayout()
        Me.gbMainWindow.SuspendLayout()
        Me.tblMainWindow.SuspendLayout()
        Me.gbCustomScrapeButton.SuspendLayout()
        Me.tblCustomScrapeButton.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(894, 441)
        Me.pnlSettings.TabIndex = 16
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 3
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbMediaList, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbCustomMarker, 1, 2)
        Me.tblSettings.Controls.Add(Me.gbMainWindow, 1, 1)
        Me.tblSettings.Controls.Add(Me.gbCustomScrapeButton, 1, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 5
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettings.Size = New System.Drawing.Size(894, 441)
        Me.tblSettings.TabIndex = 10
        '
        'gbMediaList
        '
        Me.gbMediaList.AutoSize = True
        Me.gbMediaList.Controls.Add(Me.tblMediaList)
        Me.gbMediaList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMediaList.Location = New System.Drawing.Point(3, 3)
        Me.gbMediaList.Name = "gbMediaList"
        Me.tblSettings.SetRowSpan(Me.gbMediaList, 4)
        Me.gbMediaList.Size = New System.Drawing.Size(354, 407)
        Me.gbMediaList.TabIndex = 14
        Me.gbMediaList.TabStop = False
        Me.gbMediaList.Text = "Media List"
        '
        'tblMediaList
        '
        Me.tblMediaList.AutoSize = True
        Me.tblMediaList.ColumnCount = 4
        Me.tblMediaList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaList.Controls.Add(Me.txtLevTolerance, 3, 1)
        Me.tblMediaList.Controls.Add(Me.dgvMediaListSorting, 0, 2)
        Me.tblMediaList.Controls.Add(Me.lblLevTolerance, 1, 1)
        Me.tblMediaList.Controls.Add(Me.btnMediaListSortingDefaults, 2, 3)
        Me.tblMediaList.Controls.Add(Me.chkLevTolerance, 0, 1)
        Me.tblMediaList.Controls.Add(Me.lblMediaListSorting, 0, 3)
        Me.tblMediaList.Controls.Add(Me.chkClickScrapeShowResults, 1, 0)
        Me.tblMediaList.Controls.Add(Me.chkClickScrapeEnabled, 0, 0)
        Me.tblMediaList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaList.Location = New System.Drawing.Point(3, 18)
        Me.tblMediaList.Name = "tblMediaList"
        Me.tblMediaList.RowCount = 4
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaList.Size = New System.Drawing.Size(348, 386)
        Me.tblMediaList.TabIndex = 0
        '
        'txtLevTolerance
        '
        Me.txtLevTolerance.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtLevTolerance.Enabled = False
        Me.txtLevTolerance.Location = New System.Drawing.Point(305, 26)
        Me.txtLevTolerance.Name = "txtLevTolerance"
        Me.txtLevTolerance.Size = New System.Drawing.Size(40, 22)
        Me.txtLevTolerance.TabIndex = 74
        '
        'dgvMediaListSorting
        '
        Me.dgvMediaListSorting.AllowUserToAddRows = False
        Me.dgvMediaListSorting.AllowUserToDeleteRows = False
        Me.dgvMediaListSorting.AllowUserToResizeRows = False
        Me.dgvMediaListSorting.BackgroundColor = System.Drawing.Color.White
        Me.dgvMediaListSorting.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMediaListSorting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMediaListSorting.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colMediaListSorting_DisplayIndex, Me.colMediaListSorting_Show, Me.colMediaListSorting_Column, Me.colMediaListSorting_AutoSizeMode})
        Me.tblMediaList.SetColumnSpan(Me.dgvMediaListSorting, 4)
        Me.dgvMediaListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMediaListSorting.Location = New System.Drawing.Point(3, 54)
        Me.dgvMediaListSorting.MultiSelect = False
        Me.dgvMediaListSorting.Name = "dgvMediaListSorting"
        Me.dgvMediaListSorting.RowHeadersVisible = False
        Me.dgvMediaListSorting.Size = New System.Drawing.Size(342, 300)
        Me.dgvMediaListSorting.TabIndex = 0
        '
        'colMediaListSorting_DisplayIndex
        '
        Me.colMediaListSorting_DisplayIndex.HeaderText = "DisplayIndex"
        Me.colMediaListSorting_DisplayIndex.Name = "colMediaListSorting_DisplayIndex"
        Me.colMediaListSorting_DisplayIndex.ReadOnly = True
        Me.colMediaListSorting_DisplayIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colMediaListSorting_DisplayIndex.Visible = False
        Me.colMediaListSorting_DisplayIndex.Width = 30
        '
        'colMediaListSorting_Show
        '
        Me.colMediaListSorting_Show.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colMediaListSorting_Show.HeaderText = "Show"
        Me.colMediaListSorting_Show.Name = "colMediaListSorting_Show"
        Me.colMediaListSorting_Show.Width = 42
        '
        'colMediaListSorting_Column
        '
        Me.colMediaListSorting_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMediaListSorting_Column.HeaderText = "Column"
        Me.colMediaListSorting_Column.Name = "colMediaListSorting_Column"
        Me.colMediaListSorting_Column.ReadOnly = True
        Me.colMediaListSorting_Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colMediaListSorting_AutoSizeMode
        '
        Me.colMediaListSorting_AutoSizeMode.HeaderText = "AutoSizeMode"
        Me.colMediaListSorting_AutoSizeMode.Name = "colMediaListSorting_AutoSizeMode"
        '
        'lblLevTolerance
        '
        Me.lblLevTolerance.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblLevTolerance.AutoSize = True
        Me.tblMediaList.SetColumnSpan(Me.lblLevTolerance, 2)
        Me.lblLevTolerance.Location = New System.Drawing.Point(188, 30)
        Me.lblLevTolerance.Name = "lblLevTolerance"
        Me.lblLevTolerance.Size = New System.Drawing.Size(111, 13)
        Me.lblLevTolerance.TabIndex = 73
        Me.lblLevTolerance.Text = "Mismatch Tolerance:"
        Me.lblLevTolerance.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnMediaListSortingDefaults
        '
        Me.btnMediaListSortingDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tblMediaList.SetColumnSpan(Me.btnMediaListSortingDefaults, 2)
        Me.btnMediaListSortingDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMediaListSortingDefaults.Image = CType(resources.GetObject("btnMediaListSortingDefaults.Image"), System.Drawing.Image)
        Me.btnMediaListSortingDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMediaListSortingDefaults.Location = New System.Drawing.Point(240, 360)
        Me.btnMediaListSortingDefaults.Name = "btnMediaListSortingDefaults"
        Me.btnMediaListSortingDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnMediaListSortingDefaults.TabIndex = 2
        Me.btnMediaListSortingDefaults.Text = "Defaults"
        Me.btnMediaListSortingDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMediaListSortingDefaults.UseVisualStyleBackColor = True
        '
        'chkLevTolerance
        '
        Me.chkLevTolerance.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkLevTolerance.AutoSize = True
        Me.chkLevTolerance.Location = New System.Drawing.Point(3, 28)
        Me.chkLevTolerance.Name = "chkLevTolerance"
        Me.chkLevTolerance.Size = New System.Drawing.Size(179, 17)
        Me.chkLevTolerance.TabIndex = 72
        Me.chkLevTolerance.Text = "Check Title Match Confidence"
        Me.chkLevTolerance.UseVisualStyleBackColor = True
        '
        'lblMediaListSorting
        '
        Me.lblMediaListSorting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMediaListSorting.AutoSize = True
        Me.tblMediaList.SetColumnSpan(Me.lblMediaListSorting, 2)
        Me.lblMediaListSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMediaListSorting.Location = New System.Drawing.Point(3, 365)
        Me.lblMediaListSorting.Name = "lblMediaListSorting"
        Me.lblMediaListSorting.Size = New System.Drawing.Size(218, 13)
        Me.lblMediaListSorting.TabIndex = 3
        Me.lblMediaListSorting.Text = "Use ALT + UP / DOWN to move the columns"
        '
        'chkClickScrapeShowResults
        '
        Me.chkClickScrapeShowResults.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeShowResults.AutoSize = True
        Me.tblMediaList.SetColumnSpan(Me.chkClickScrapeShowResults, 3)
        Me.chkClickScrapeShowResults.Location = New System.Drawing.Point(188, 3)
        Me.chkClickScrapeShowResults.Name = "chkClickScrapeShowResults"
        Me.chkClickScrapeShowResults.Size = New System.Drawing.Size(132, 17)
        Me.chkClickScrapeShowResults.TabIndex = 64
        Me.chkClickScrapeShowResults.Text = "Show Results Dialog"
        Me.chkClickScrapeShowResults.UseVisualStyleBackColor = True
        '
        'chkClickScrapeEnabled
        '
        Me.chkClickScrapeEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeEnabled.AutoSize = True
        Me.chkClickScrapeEnabled.Location = New System.Drawing.Point(3, 3)
        Me.chkClickScrapeEnabled.Name = "chkClickScrapeEnabled"
        Me.chkClickScrapeEnabled.Size = New System.Drawing.Size(126, 17)
        Me.chkClickScrapeEnabled.TabIndex = 65
        Me.chkClickScrapeEnabled.Text = "Enable Click-Scrape"
        Me.chkClickScrapeEnabled.UseVisualStyleBackColor = True
        '
        'gbCustomMarker
        '
        Me.gbCustomMarker.AutoSize = True
        Me.gbCustomMarker.Controls.Add(Me.tblCustomMarker)
        Me.gbCustomMarker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCustomMarker.Location = New System.Drawing.Point(363, 188)
        Me.gbCustomMarker.Name = "gbCustomMarker"
        Me.gbCustomMarker.Size = New System.Drawing.Size(497, 133)
        Me.gbCustomMarker.TabIndex = 9
        Me.gbCustomMarker.TabStop = False
        Me.gbCustomMarker.Text = "Custom Marker"
        '
        'tblCustomMarker
        '
        Me.tblCustomMarker.AutoSize = True
        Me.tblCustomMarker.ColumnCount = 4
        Me.tblCustomMarker.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomMarker.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblCustomMarker.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomMarker.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomMarker.Controls.Add(Me.btnCustomMarker4, 2, 3)
        Me.tblCustomMarker.Controls.Add(Me.lblCustomMarker1, 0, 0)
        Me.tblCustomMarker.Controls.Add(Me.btnCustomMarker3, 2, 2)
        Me.tblCustomMarker.Controls.Add(Me.txtCustomMarker4, 1, 3)
        Me.tblCustomMarker.Controls.Add(Me.btnCustomMarker2, 2, 1)
        Me.tblCustomMarker.Controls.Add(Me.lblCustomMarker2, 0, 1)
        Me.tblCustomMarker.Controls.Add(Me.btnCustomMarker1, 2, 0)
        Me.tblCustomMarker.Controls.Add(Me.lblCustomMarker4, 0, 3)
        Me.tblCustomMarker.Controls.Add(Me.txtCustomMarker3, 1, 2)
        Me.tblCustomMarker.Controls.Add(Me.lblCustomMarker3, 0, 2)
        Me.tblCustomMarker.Controls.Add(Me.txtCustomMarker1, 1, 0)
        Me.tblCustomMarker.Controls.Add(Me.txtCustomMarker2, 1, 1)
        Me.tblCustomMarker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCustomMarker.Location = New System.Drawing.Point(3, 18)
        Me.tblCustomMarker.Name = "tblCustomMarker"
        Me.tblCustomMarker.RowCount = 5
        Me.tblCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomMarker.Size = New System.Drawing.Size(491, 112)
        Me.tblCustomMarker.TabIndex = 10
        '
        'btnCustomMarker4
        '
        Me.btnCustomMarker4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnCustomMarker4.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnCustomMarker4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCustomMarker4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCustomMarker4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCustomMarker4.Location = New System.Drawing.Point(464, 87)
        Me.btnCustomMarker4.Name = "btnCustomMarker4"
        Me.btnCustomMarker4.Size = New System.Drawing.Size(24, 22)
        Me.btnCustomMarker4.TabIndex = 24
        Me.btnCustomMarker4.UseVisualStyleBackColor = False
        '
        'lblCustomMarker1
        '
        Me.lblCustomMarker1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMarker1.AutoSize = True
        Me.lblCustomMarker1.Location = New System.Drawing.Point(3, 7)
        Me.lblCustomMarker1.Name = "lblCustomMarker1"
        Me.lblCustomMarker1.Size = New System.Drawing.Size(55, 13)
        Me.lblCustomMarker1.TabIndex = 0
        Me.lblCustomMarker1.Text = "Custom 1"
        '
        'btnCustomMarker3
        '
        Me.btnCustomMarker3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnCustomMarker3.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnCustomMarker3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCustomMarker3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCustomMarker3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCustomMarker3.Location = New System.Drawing.Point(464, 59)
        Me.btnCustomMarker3.Name = "btnCustomMarker3"
        Me.btnCustomMarker3.Size = New System.Drawing.Size(24, 22)
        Me.btnCustomMarker3.TabIndex = 21
        Me.btnCustomMarker3.UseVisualStyleBackColor = False
        '
        'txtCustomMarker4
        '
        Me.txtCustomMarker4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomMarker4.Location = New System.Drawing.Point(64, 87)
        Me.txtCustomMarker4.Name = "txtCustomMarker4"
        Me.txtCustomMarker4.Size = New System.Drawing.Size(394, 22)
        Me.txtCustomMarker4.TabIndex = 23
        '
        'btnCustomMarker2
        '
        Me.btnCustomMarker2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnCustomMarker2.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnCustomMarker2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCustomMarker2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCustomMarker2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCustomMarker2.Location = New System.Drawing.Point(464, 31)
        Me.btnCustomMarker2.Name = "btnCustomMarker2"
        Me.btnCustomMarker2.Size = New System.Drawing.Size(24, 22)
        Me.btnCustomMarker2.TabIndex = 18
        Me.btnCustomMarker2.UseVisualStyleBackColor = False
        '
        'lblCustomMarker2
        '
        Me.lblCustomMarker2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMarker2.AutoSize = True
        Me.lblCustomMarker2.Location = New System.Drawing.Point(3, 35)
        Me.lblCustomMarker2.Name = "lblCustomMarker2"
        Me.lblCustomMarker2.Size = New System.Drawing.Size(55, 13)
        Me.lblCustomMarker2.TabIndex = 16
        Me.lblCustomMarker2.Text = "Custom 2"
        '
        'btnCustomMarker1
        '
        Me.btnCustomMarker1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnCustomMarker1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnCustomMarker1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCustomMarker1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCustomMarker1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCustomMarker1.Location = New System.Drawing.Point(464, 3)
        Me.btnCustomMarker1.Name = "btnCustomMarker1"
        Me.btnCustomMarker1.Size = New System.Drawing.Size(24, 22)
        Me.btnCustomMarker1.TabIndex = 15
        Me.btnCustomMarker1.UseVisualStyleBackColor = False
        '
        'lblCustomMarker4
        '
        Me.lblCustomMarker4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMarker4.AutoSize = True
        Me.lblCustomMarker4.Location = New System.Drawing.Point(3, 91)
        Me.lblCustomMarker4.Name = "lblCustomMarker4"
        Me.lblCustomMarker4.Size = New System.Drawing.Size(55, 13)
        Me.lblCustomMarker4.TabIndex = 22
        Me.lblCustomMarker4.Text = "Custom 4"
        '
        'txtCustomMarker3
        '
        Me.txtCustomMarker3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomMarker3.Location = New System.Drawing.Point(64, 59)
        Me.txtCustomMarker3.Name = "txtCustomMarker3"
        Me.txtCustomMarker3.Size = New System.Drawing.Size(394, 22)
        Me.txtCustomMarker3.TabIndex = 20
        '
        'lblCustomMarker3
        '
        Me.lblCustomMarker3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMarker3.AutoSize = True
        Me.lblCustomMarker3.Location = New System.Drawing.Point(3, 63)
        Me.lblCustomMarker3.Name = "lblCustomMarker3"
        Me.lblCustomMarker3.Size = New System.Drawing.Size(55, 13)
        Me.lblCustomMarker3.TabIndex = 19
        Me.lblCustomMarker3.Text = "Custom 3"
        '
        'txtCustomMarker1
        '
        Me.txtCustomMarker1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomMarker1.Location = New System.Drawing.Point(64, 3)
        Me.txtCustomMarker1.Name = "txtCustomMarker1"
        Me.txtCustomMarker1.Size = New System.Drawing.Size(394, 22)
        Me.txtCustomMarker1.TabIndex = 1
        '
        'txtCustomMarker2
        '
        Me.txtCustomMarker2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomMarker2.Location = New System.Drawing.Point(64, 31)
        Me.txtCustomMarker2.Name = "txtCustomMarker2"
        Me.txtCustomMarker2.Size = New System.Drawing.Size(394, 22)
        Me.txtCustomMarker2.TabIndex = 17
        '
        'gbMainWindow
        '
        Me.gbMainWindow.AutoSize = True
        Me.gbMainWindow.Controls.Add(Me.tblMainWindow)
        Me.gbMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMainWindow.Location = New System.Drawing.Point(363, 134)
        Me.gbMainWindow.Name = "gbMainWindow"
        Me.gbMainWindow.Size = New System.Drawing.Size(497, 48)
        Me.gbMainWindow.TabIndex = 10
        Me.gbMainWindow.TabStop = False
        Me.gbMainWindow.Text = "Main Window"
        '
        'tblMainWindow
        '
        Me.tblMainWindow.AutoSize = True
        Me.tblMainWindow.ColumnCount = 2
        Me.tblMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainWindow.Controls.Add(Me.lblLanguageOverlay, 0, 0)
        Me.tblMainWindow.Controls.Add(Me.cbLanguageOverlay, 1, 0)
        Me.tblMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMainWindow.Location = New System.Drawing.Point(3, 18)
        Me.tblMainWindow.Name = "tblMainWindow"
        Me.tblMainWindow.RowCount = 1
        Me.tblMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainWindow.Size = New System.Drawing.Size(491, 27)
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
        Me.lblLanguageOverlay.TabIndex = 16
        Me.lblLanguageOverlay.Text = "Display best Audio Stream with the following Language:"
        '
        'cbLanguageOverlay
        '
        Me.cbLanguageOverlay.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbLanguageOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLanguageOverlay.FormattingEnabled = True
        Me.cbLanguageOverlay.Location = New System.Drawing.Point(314, 3)
        Me.cbLanguageOverlay.Name = "cbLanguageOverlay"
        Me.cbLanguageOverlay.Size = New System.Drawing.Size(174, 21)
        Me.cbLanguageOverlay.Sorted = True
        Me.cbLanguageOverlay.TabIndex = 17
        '
        'gbCustomScrapeButton
        '
        Me.gbCustomScrapeButton.AutoSize = True
        Me.gbCustomScrapeButton.Controls.Add(Me.tblCustomScrapeButton)
        Me.gbCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCustomScrapeButton.Location = New System.Drawing.Point(363, 3)
        Me.gbCustomScrapeButton.Name = "gbCustomScrapeButton"
        Me.gbCustomScrapeButton.Size = New System.Drawing.Size(497, 125)
        Me.gbCustomScrapeButton.TabIndex = 11
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
        Me.tblCustomScrapeButton.Size = New System.Drawing.Size(491, 104)
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
        Me.cbCustomScrapeButtonSelectionType.Size = New System.Drawing.Size(329, 21)
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
        Me.cbCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(329, 21)
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
        Me.cbCustomScrapeButtonModifierType.Size = New System.Drawing.Size(329, 21)
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
        'frmMovie_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 441)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmMovie_GUI"
        Me.Text = "frmMovie_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbMediaList.ResumeLayout(False)
        Me.gbMediaList.PerformLayout()
        Me.tblMediaList.ResumeLayout(False)
        Me.tblMediaList.PerformLayout()
        CType(Me.dgvMediaListSorting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbCustomMarker.ResumeLayout(False)
        Me.gbCustomMarker.PerformLayout()
        Me.tblCustomMarker.ResumeLayout(False)
        Me.tblCustomMarker.PerformLayout()
        Me.gbMainWindow.ResumeLayout(False)
        Me.gbMainWindow.PerformLayout()
        Me.tblMainWindow.ResumeLayout(False)
        Me.tblMainWindow.PerformLayout()
        Me.gbCustomScrapeButton.ResumeLayout(False)
        Me.gbCustomScrapeButton.PerformLayout()
        Me.tblCustomScrapeButton.ResumeLayout(False)
        Me.tblCustomScrapeButton.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents chkClickScrapeShowResults As CheckBox
    Friend WithEvents chkClickScrapeEnabled As CheckBox
    Friend WithEvents chkLevTolerance As CheckBox
    Friend WithEvents lblLevTolerance As Label
    Friend WithEvents txtLevTolerance As TextBox
    Friend WithEvents gbMediaList As GroupBox
    Friend WithEvents tblMediaList As TableLayoutPanel
    Friend WithEvents gbCustomMarker As GroupBox
    Friend WithEvents tblCustomMarker As TableLayoutPanel
    Friend WithEvents btnCustomMarker4 As Button
    Friend WithEvents lblCustomMarker1 As Label
    Friend WithEvents btnCustomMarker3 As Button
    Friend WithEvents txtCustomMarker4 As TextBox
    Friend WithEvents btnCustomMarker2 As Button
    Friend WithEvents lblCustomMarker2 As Label
    Friend WithEvents btnCustomMarker1 As Button
    Friend WithEvents lblCustomMarker4 As Label
    Friend WithEvents txtCustomMarker3 As TextBox
    Friend WithEvents lblCustomMarker3 As Label
    Friend WithEvents txtCustomMarker1 As TextBox
    Friend WithEvents txtCustomMarker2 As TextBox
    Friend WithEvents gbMainWindow As GroupBox
    Friend WithEvents tblMainWindow As TableLayoutPanel
    Friend WithEvents lblLanguageOverlay As Label
    Friend WithEvents cbLanguageOverlay As ComboBox
    Friend WithEvents gbCustomScrapeButton As GroupBox
    Friend WithEvents tblCustomScrapeButton As TableLayoutPanel
    Friend WithEvents cbCustomScrapeButtonScrapeType As ComboBox
    Friend WithEvents cbCustomScrapeButtonModifierType As ComboBox
    Friend WithEvents lblCustomScrapeButtonScrapeType As Label
    Friend WithEvents lblCustomScrapeButtonModifierType As Label
    Friend WithEvents rbCustomScrapeButtonEnabled As RadioButton
    Friend WithEvents rbCustomScrapeButtonDisabled As RadioButton
    Friend WithEvents cdColor As ColorDialog
    Friend WithEvents dgvMediaListSorting As DataGridView
    Friend WithEvents btnMediaListSortingDefaults As Button
    Friend WithEvents colMediaListSorting_DisplayIndex As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_Show As DataGridViewCheckBoxColumn
    Friend WithEvents colMediaListSorting_Column As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_AutoSizeMode As DataGridViewComboBoxColumn
    Friend WithEvents lblMediaListSorting As Label
    Friend WithEvents cbCustomScrapeButtonSelectionType As ComboBox
    Friend WithEvents lblCustomScrapeButtonSelectionType As Label
End Class
