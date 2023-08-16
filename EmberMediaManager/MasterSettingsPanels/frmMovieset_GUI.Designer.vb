<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovieset_GUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovieset_GUI))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMediaList = New System.Windows.Forms.GroupBox()
        Me.tblMediaList = New System.Windows.Forms.TableLayoutPanel()
        Me.chkClickScrapeShowResults = New System.Windows.Forms.CheckBox()
        Me.dgvMediaListSorting = New System.Windows.Forms.DataGridView()
        Me.colMediaListSorting_DisplayIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_Show = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colMediaListSorting_Column = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMediaListSorting_AutoSizeMode = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.chkClickScrapeEnabled = New System.Windows.Forms.CheckBox()
        Me.btnMediaListSortingDefaults = New System.Windows.Forms.Button()
        Me.lblMediaList = New System.Windows.Forms.Label()
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
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMediaList.SuspendLayout()
        Me.tblMediaList.SuspendLayout()
        CType(Me.dgvMediaListSorting, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlSettings.Size = New System.Drawing.Size(872, 386)
        Me.pnlSettings.TabIndex = 25
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
        Me.tblSettings.Controls.Add(Me.gbCustomScrapeButton, 1, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 3
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettings.Size = New System.Drawing.Size(872, 386)
        Me.tblSettings.TabIndex = 11
        '
        'gbMediaList
        '
        Me.gbMediaList.AutoSize = True
        Me.gbMediaList.Controls.Add(Me.tblMediaList)
        Me.gbMediaList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMediaList.Location = New System.Drawing.Point(3, 3)
        Me.gbMediaList.Name = "gbMediaList"
        Me.tblSettings.SetRowSpan(Me.gbMediaList, 2)
        Me.gbMediaList.Size = New System.Drawing.Size(341, 360)
        Me.gbMediaList.TabIndex = 14
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
        Me.tblMediaList.Controls.Add(Me.chkClickScrapeShowResults, 1, 0)
        Me.tblMediaList.Controls.Add(Me.dgvMediaListSorting, 0, 1)
        Me.tblMediaList.Controls.Add(Me.chkClickScrapeEnabled, 0, 0)
        Me.tblMediaList.Controls.Add(Me.btnMediaListSortingDefaults, 2, 2)
        Me.tblMediaList.Controls.Add(Me.lblMediaList, 0, 2)
        Me.tblMediaList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaList.Location = New System.Drawing.Point(3, 18)
        Me.tblMediaList.Name = "tblMediaList"
        Me.tblMediaList.RowCount = 3
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMediaList.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaList.Size = New System.Drawing.Size(335, 339)
        Me.tblMediaList.TabIndex = 0
        '
        'chkClickScrapeShowResults
        '
        Me.chkClickScrapeShowResults.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClickScrapeShowResults.AutoSize = True
        Me.tblMediaList.SetColumnSpan(Me.chkClickScrapeShowResults, 2)
        Me.chkClickScrapeShowResults.Location = New System.Drawing.Point(135, 3)
        Me.chkClickScrapeShowResults.Name = "chkClickScrapeShowResults"
        Me.chkClickScrapeShowResults.Size = New System.Drawing.Size(132, 17)
        Me.chkClickScrapeShowResults.TabIndex = 64
        Me.chkClickScrapeShowResults.Text = "Show Results Dialog"
        Me.chkClickScrapeShowResults.UseVisualStyleBackColor = True
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
        Me.tblMediaList.SetColumnSpan(Me.dgvMediaListSorting, 3)
        Me.dgvMediaListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMediaListSorting.Location = New System.Drawing.Point(3, 26)
        Me.dgvMediaListSorting.MultiSelect = False
        Me.dgvMediaListSorting.Name = "dgvMediaListSorting"
        Me.dgvMediaListSorting.RowHeadersVisible = False
        Me.dgvMediaListSorting.Size = New System.Drawing.Size(329, 281)
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
        'btnMediaListSortingDefaults
        '
        Me.btnMediaListSortingDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMediaListSortingDefaults.Image = CType(resources.GetObject("btnMediaListSortingDefaults.Image"), System.Drawing.Image)
        Me.btnMediaListSortingDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMediaListSortingDefaults.Location = New System.Drawing.Point(227, 313)
        Me.btnMediaListSortingDefaults.Name = "btnMediaListSortingDefaults"
        Me.btnMediaListSortingDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnMediaListSortingDefaults.TabIndex = 2
        Me.btnMediaListSortingDefaults.Text = "Defaults"
        Me.btnMediaListSortingDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMediaListSortingDefaults.UseVisualStyleBackColor = True
        '
        'lblMediaList
        '
        Me.lblMediaList.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMediaList.AutoSize = True
        Me.tblMediaList.SetColumnSpan(Me.lblMediaList, 2)
        Me.lblMediaList.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMediaList.Location = New System.Drawing.Point(3, 318)
        Me.lblMediaList.Name = "lblMediaList"
        Me.lblMediaList.Size = New System.Drawing.Size(201, 13)
        Me.lblMediaList.TabIndex = 3
        Me.lblMediaList.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'gbCustomScrapeButton
        '
        Me.gbCustomScrapeButton.AutoSize = True
        Me.gbCustomScrapeButton.Controls.Add(Me.tblCustomScrapeButton)
        Me.gbCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCustomScrapeButton.Location = New System.Drawing.Point(350, 3)
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
        'frmMovieset_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 386)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmMovieset_GUI"
        Me.Text = "frmMovieSet_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbMediaList.ResumeLayout(False)
        Me.gbMediaList.PerformLayout()
        Me.tblMediaList.ResumeLayout(False)
        Me.tblMediaList.PerformLayout()
        CType(Me.dgvMediaListSorting, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents gbCustomScrapeButton As GroupBox
    Friend WithEvents tblCustomScrapeButton As TableLayoutPanel
    Friend WithEvents cbCustomScrapeButtonScrapeType As ComboBox
    Friend WithEvents cbCustomScrapeButtonModifierType As ComboBox
    Friend WithEvents lblCustomScrapeButtonScrapeType As Label
    Friend WithEvents lblCustomScrapeButtonModifierType As Label
    Friend WithEvents rbCustomScrapeButtonEnabled As RadioButton
    Friend WithEvents rbCustomScrapeButtonDisabled As RadioButton
    Friend WithEvents gbMediaList As GroupBox
    Friend WithEvents tblMediaList As TableLayoutPanel
    Friend WithEvents dgvMediaListSorting As DataGridView
    Friend WithEvents colMediaListSorting_DisplayIndex As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_Show As DataGridViewCheckBoxColumn
    Friend WithEvents colMediaListSorting_Column As DataGridViewTextBoxColumn
    Friend WithEvents colMediaListSorting_AutoSizeMode As DataGridViewComboBoxColumn
    Friend WithEvents btnMediaListSortingDefaults As Button
    Friend WithEvents lblMediaList As Label
    Friend WithEvents cbCustomScrapeButtonSelectionType As ComboBox
    Friend WithEvents lblCustomScrapeButtonSelectionType As Label
End Class
