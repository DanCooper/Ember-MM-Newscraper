<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMovieset_GUI
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovieset_GUI))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieSetGeneralCustomScrapeButton = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetGeneralCustomScrapeButton = New System.Windows.Forms.TableLayoutPanel()
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType = New System.Windows.Forms.ComboBox()
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType = New System.Windows.Forms.ComboBox()
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType = New System.Windows.Forms.Label()
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType = New System.Windows.Forms.Label()
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled = New System.Windows.Forms.RadioButton()
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled = New System.Windows.Forms.RadioButton()
        Me.gbMovieSetGeneralMediaListOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetGeneralMediaListOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieSetGeneralMediaListSorting = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetGeneralMediaListSorting = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieSetGeneralMediaListSortingDown = New System.Windows.Forms.Button()
        Me.btnMovieSetGeneralMediaListSortingUp = New System.Windows.Forms.Button()
        Me.lvMovieSetGeneralMediaListSorting = New System.Windows.Forms.ListView()
        Me.colMovieSetGeneralMediaListSortingDisplayIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSetGeneralMediaListSortingColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSetGeneralMediaListSortingLabel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieSetGeneralMediaListSortingHide = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnMovieSetGeneralMediaListSortingReset = New System.Windows.Forms.Button()
        Me.gbMovieSetGeneralMediaListSortTokensOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetGeneralSortTokensOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieSetSortTokenReset = New System.Windows.Forms.Button()
        Me.btnMovieSetSortTokenRemove = New System.Windows.Forms.Button()
        Me.lstMovieSetSortTokens = New System.Windows.Forms.ListBox()
        Me.btnMovieSetSortTokenAdd = New System.Windows.Forms.Button()
        Me.txtMovieSetSortToken = New System.Windows.Forms.TextBox()
        Me.gbMovieSetGeneralMiscOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetGeneralMiscOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieSetClickScrapeAsk = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetClickScrape = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetGeneralMarkNew = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMovieSetGeneralCustomScrapeButton.SuspendLayout()
        Me.tblMovieSetGeneralCustomScrapeButton.SuspendLayout()
        Me.gbMovieSetGeneralMediaListOpts.SuspendLayout()
        Me.tblMovieSetGeneralMediaListOpts.SuspendLayout()
        Me.gbMovieSetGeneralMediaListSorting.SuspendLayout()
        Me.tblMovieSetGeneralMediaListSorting.SuspendLayout()
        Me.gbMovieSetGeneralMediaListSortTokensOpts.SuspendLayout()
        Me.tblMovieSetGeneralSortTokensOpts.SuspendLayout()
        Me.gbMovieSetGeneralMiscOpts.SuspendLayout()
        Me.tblMovieSetGeneralMiscOpts.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(800, 450)
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
        Me.tblSettings.Controls.Add(Me.gbMovieSetGeneralCustomScrapeButton, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbMovieSetGeneralMediaListOpts, 0, 1)
        Me.tblSettings.Controls.Add(Me.gbMovieSetGeneralMiscOpts, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 3
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 450)
        Me.tblSettings.TabIndex = 6
        '
        'gbMovieSetGeneralCustomScrapeButton
        '
        Me.gbMovieSetGeneralCustomScrapeButton.AutoSize = True
        Me.gbMovieSetGeneralCustomScrapeButton.Controls.Add(Me.tblMovieSetGeneralCustomScrapeButton)
        Me.gbMovieSetGeneralCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetGeneralCustomScrapeButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieSetGeneralCustomScrapeButton.Location = New System.Drawing.Point(233, 3)
        Me.gbMovieSetGeneralCustomScrapeButton.Name = "gbMovieSetGeneralCustomScrapeButton"
        Me.gbMovieSetGeneralCustomScrapeButton.Size = New System.Drawing.Size(497, 98)
        Me.gbMovieSetGeneralCustomScrapeButton.TabIndex = 12
        Me.gbMovieSetGeneralCustomScrapeButton.TabStop = False
        Me.gbMovieSetGeneralCustomScrapeButton.Text = "Scrape Button"
        '
        'tblMovieSetGeneralCustomScrapeButton
        '
        Me.tblMovieSetGeneralCustomScrapeButton.AutoSize = True
        Me.tblMovieSetGeneralCustomScrapeButton.ColumnCount = 2
        Me.tblMovieSetGeneralCustomScrapeButton.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralCustomScrapeButton.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblMovieSetGeneralCustomScrapeButton.Controls.Add(Me.cbMovieSetGeneralCustomScrapeButtonScrapeType, 1, 1)
        Me.tblMovieSetGeneralCustomScrapeButton.Controls.Add(Me.cbMovieSetGeneralCustomScrapeButtonModifierType, 1, 2)
        Me.tblMovieSetGeneralCustomScrapeButton.Controls.Add(Me.txtMovieSetGeneralCustomScrapeButtonScrapeType, 0, 1)
        Me.tblMovieSetGeneralCustomScrapeButton.Controls.Add(Me.txtMovieSetGeneralCustomScrapeButtonModifierType, 0, 2)
        Me.tblMovieSetGeneralCustomScrapeButton.Controls.Add(Me.rbMovieSetGeneralCustomScrapeButtonEnabled, 1, 0)
        Me.tblMovieSetGeneralCustomScrapeButton.Controls.Add(Me.rbMovieSetGeneralCustomScrapeButtonDisabled, 0, 0)
        Me.tblMovieSetGeneralCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetGeneralCustomScrapeButton.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetGeneralCustomScrapeButton.Name = "tblMovieSetGeneralCustomScrapeButton"
        Me.tblMovieSetGeneralCustomScrapeButton.RowCount = 4
        Me.tblMovieSetGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralCustomScrapeButton.Size = New System.Drawing.Size(491, 77)
        Me.tblMovieSetGeneralCustomScrapeButton.TabIndex = 0
        '
        'cbMovieSetGeneralCustomScrapeButtonScrapeType
        '
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = False
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.FormattingEnabled = True
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.Location = New System.Drawing.Point(159, 26)
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.Name = "cbMovieSetGeneralCustomScrapeButtonScrapeType"
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(329, 21)
        Me.cbMovieSetGeneralCustomScrapeButtonScrapeType.TabIndex = 1
        '
        'cbMovieSetGeneralCustomScrapeButtonModifierType
        '
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.Enabled = False
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.FormattingEnabled = True
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.Location = New System.Drawing.Point(159, 53)
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.Name = "cbMovieSetGeneralCustomScrapeButtonModifierType"
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.Size = New System.Drawing.Size(329, 21)
        Me.cbMovieSetGeneralCustomScrapeButtonModifierType.TabIndex = 2
        '
        'txtMovieSetGeneralCustomScrapeButtonScrapeType
        '
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.AutoSize = True
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.Enabled = False
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.Location = New System.Drawing.Point(3, 30)
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.Name = "txtMovieSetGeneralCustomScrapeButtonScrapeType"
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(66, 13)
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.TabIndex = 3
        Me.txtMovieSetGeneralCustomScrapeButtonScrapeType.Text = "Scrape Type"
        '
        'txtMovieSetGeneralCustomScrapeButtonModifierType
        '
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.AutoSize = True
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.Enabled = False
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.Location = New System.Drawing.Point(3, 57)
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.Name = "txtMovieSetGeneralCustomScrapeButtonModifierType"
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.Size = New System.Drawing.Size(76, 13)
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.TabIndex = 4
        Me.txtMovieSetGeneralCustomScrapeButtonModifierType.Text = "Modifier Type"
        '
        'rbMovieSetGeneralCustomScrapeButtonEnabled
        '
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.AutoSize = True
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.Location = New System.Drawing.Point(159, 3)
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.Name = "rbMovieSetGeneralCustomScrapeButtonEnabled"
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.Size = New System.Drawing.Size(150, 17)
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.TabIndex = 5
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.TabStop = True
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.Text = "Custom Scrape Function"
        Me.rbMovieSetGeneralCustomScrapeButtonEnabled.UseVisualStyleBackColor = True
        '
        'rbMovieSetGeneralCustomScrapeButtonDisabled
        '
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.AutoSize = True
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.Location = New System.Drawing.Point(3, 3)
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.Name = "rbMovieSetGeneralCustomScrapeButtonDisabled"
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.Size = New System.Drawing.Size(150, 17)
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.TabIndex = 6
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.TabStop = True
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.Text = "Open Drop Down Menu"
        Me.rbMovieSetGeneralCustomScrapeButtonDisabled.UseVisualStyleBackColor = True
        '
        'gbMovieSetGeneralMediaListOpts
        '
        Me.gbMovieSetGeneralMediaListOpts.AutoSize = True
        Me.gbMovieSetGeneralMediaListOpts.Controls.Add(Me.tblMovieSetGeneralMediaListOpts)
        Me.gbMovieSetGeneralMediaListOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetGeneralMediaListOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieSetGeneralMediaListOpts.Location = New System.Drawing.Point(3, 107)
        Me.gbMovieSetGeneralMediaListOpts.Name = "gbMovieSetGeneralMediaListOpts"
        Me.gbMovieSetGeneralMediaListOpts.Size = New System.Drawing.Size(224, 438)
        Me.gbMovieSetGeneralMediaListOpts.TabIndex = 4
        Me.gbMovieSetGeneralMediaListOpts.TabStop = False
        Me.gbMovieSetGeneralMediaListOpts.Text = "Media List Options"
        '
        'tblMovieSetGeneralMediaListOpts
        '
        Me.tblMovieSetGeneralMediaListOpts.AutoSize = True
        Me.tblMovieSetGeneralMediaListOpts.ColumnCount = 2
        Me.tblMovieSetGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.Controls.Add(Me.gbMovieSetGeneralMediaListSorting, 0, 1)
        Me.tblMovieSetGeneralMediaListOpts.Controls.Add(Me.gbMovieSetGeneralMediaListSortTokensOpts, 0, 0)
        Me.tblMovieSetGeneralMediaListOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetGeneralMediaListOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetGeneralMediaListOpts.Name = "tblMovieSetGeneralMediaListOpts"
        Me.tblMovieSetGeneralMediaListOpts.RowCount = 3
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetGeneralMediaListOpts.Size = New System.Drawing.Size(218, 417)
        Me.tblMovieSetGeneralMediaListOpts.TabIndex = 7
        '
        'gbMovieSetGeneralMediaListSorting
        '
        Me.gbMovieSetGeneralMediaListSorting.AutoSize = True
        Me.tblMovieSetGeneralMediaListOpts.SetColumnSpan(Me.gbMovieSetGeneralMediaListSorting, 2)
        Me.gbMovieSetGeneralMediaListSorting.Controls.Add(Me.tblMovieSetGeneralMediaListSorting)
        Me.gbMovieSetGeneralMediaListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetGeneralMediaListSorting.Location = New System.Drawing.Point(3, 108)
        Me.gbMovieSetGeneralMediaListSorting.Name = "gbMovieSetGeneralMediaListSorting"
        Me.gbMovieSetGeneralMediaListSorting.Size = New System.Drawing.Size(212, 306)
        Me.gbMovieSetGeneralMediaListSorting.TabIndex = 84
        Me.gbMovieSetGeneralMediaListSorting.TabStop = False
        Me.gbMovieSetGeneralMediaListSorting.Text = "Media List Sorting"
        '
        'tblMovieSetGeneralMediaListSorting
        '
        Me.tblMovieSetGeneralMediaListSorting.AutoSize = True
        Me.tblMovieSetGeneralMediaListSorting.ColumnCount = 6
        Me.tblMovieSetGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMediaListSorting.Controls.Add(Me.btnMovieSetGeneralMediaListSortingDown, 2, 1)
        Me.tblMovieSetGeneralMediaListSorting.Controls.Add(Me.btnMovieSetGeneralMediaListSortingUp, 1, 1)
        Me.tblMovieSetGeneralMediaListSorting.Controls.Add(Me.lvMovieSetGeneralMediaListSorting, 0, 0)
        Me.tblMovieSetGeneralMediaListSorting.Controls.Add(Me.btnMovieSetGeneralMediaListSortingReset, 4, 1)
        Me.tblMovieSetGeneralMediaListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetGeneralMediaListSorting.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetGeneralMediaListSorting.Name = "tblMovieSetGeneralMediaListSorting"
        Me.tblMovieSetGeneralMediaListSorting.RowCount = 3
        Me.tblMovieSetGeneralMediaListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMediaListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMediaListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMediaListSorting.Size = New System.Drawing.Size(206, 285)
        Me.tblMovieSetGeneralMediaListSorting.TabIndex = 0
        '
        'btnMovieSetGeneralMediaListSortingDown
        '
        Me.btnMovieSetGeneralMediaListSortingDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMovieSetGeneralMediaListSortingDown.Image = CType(resources.GetObject("btnMovieSetGeneralMediaListSortingDown.Image"), System.Drawing.Image)
        Me.btnMovieSetGeneralMediaListSortingDown.Location = New System.Drawing.Point(91, 259)
        Me.btnMovieSetGeneralMediaListSortingDown.Name = "btnMovieSetGeneralMediaListSortingDown"
        Me.btnMovieSetGeneralMediaListSortingDown.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSetGeneralMediaListSortingDown.TabIndex = 12
        Me.btnMovieSetGeneralMediaListSortingDown.UseVisualStyleBackColor = True
        '
        'btnMovieSetGeneralMediaListSortingUp
        '
        Me.btnMovieSetGeneralMediaListSortingUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMovieSetGeneralMediaListSortingUp.Image = CType(resources.GetObject("btnMovieSetGeneralMediaListSortingUp.Image"), System.Drawing.Image)
        Me.btnMovieSetGeneralMediaListSortingUp.Location = New System.Drawing.Point(62, 259)
        Me.btnMovieSetGeneralMediaListSortingUp.Name = "btnMovieSetGeneralMediaListSortingUp"
        Me.btnMovieSetGeneralMediaListSortingUp.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSetGeneralMediaListSortingUp.TabIndex = 13
        Me.btnMovieSetGeneralMediaListSortingUp.UseVisualStyleBackColor = True
        '
        'lvMovieSetGeneralMediaListSorting
        '
        Me.lvMovieSetGeneralMediaListSorting.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colMovieSetGeneralMediaListSortingDisplayIndex, Me.colMovieSetGeneralMediaListSortingColumn, Me.colMovieSetGeneralMediaListSortingLabel, Me.colMovieSetGeneralMediaListSortingHide})
        Me.tblMovieSetGeneralMediaListSorting.SetColumnSpan(Me.lvMovieSetGeneralMediaListSorting, 5)
        Me.lvMovieSetGeneralMediaListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMovieSetGeneralMediaListSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvMovieSetGeneralMediaListSorting.FullRowSelect = True
        Me.lvMovieSetGeneralMediaListSorting.HideSelection = False
        Me.lvMovieSetGeneralMediaListSorting.Location = New System.Drawing.Point(3, 3)
        Me.lvMovieSetGeneralMediaListSorting.Name = "lvMovieSetGeneralMediaListSorting"
        Me.lvMovieSetGeneralMediaListSorting.Size = New System.Drawing.Size(200, 250)
        Me.lvMovieSetGeneralMediaListSorting.TabIndex = 10
        Me.lvMovieSetGeneralMediaListSorting.UseCompatibleStateImageBehavior = False
        Me.lvMovieSetGeneralMediaListSorting.View = System.Windows.Forms.View.Details
        '
        'colMovieSetGeneralMediaListSortingDisplayIndex
        '
        Me.colMovieSetGeneralMediaListSortingDisplayIndex.Text = "DisplayIndex"
        Me.colMovieSetGeneralMediaListSortingDisplayIndex.Width = 0
        '
        'colMovieSetGeneralMediaListSortingColumn
        '
        Me.colMovieSetGeneralMediaListSortingColumn.Text = "DBName"
        Me.colMovieSetGeneralMediaListSortingColumn.Width = 0
        '
        'colMovieSetGeneralMediaListSortingLabel
        '
        Me.colMovieSetGeneralMediaListSortingLabel.Text = "Column"
        Me.colMovieSetGeneralMediaListSortingLabel.Width = 110
        '
        'colMovieSetGeneralMediaListSortingHide
        '
        Me.colMovieSetGeneralMediaListSortingHide.Text = "Hide"
        '
        'btnMovieSetGeneralMediaListSortingReset
        '
        Me.btnMovieSetGeneralMediaListSortingReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieSetGeneralMediaListSortingReset.Image = CType(resources.GetObject("btnMovieSetGeneralMediaListSortingReset.Image"), System.Drawing.Image)
        Me.btnMovieSetGeneralMediaListSortingReset.Location = New System.Drawing.Point(180, 259)
        Me.btnMovieSetGeneralMediaListSortingReset.Name = "btnMovieSetGeneralMediaListSortingReset"
        Me.btnMovieSetGeneralMediaListSortingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSetGeneralMediaListSortingReset.TabIndex = 11
        Me.btnMovieSetGeneralMediaListSortingReset.UseVisualStyleBackColor = True
        '
        'gbMovieSetGeneralMediaListSortTokensOpts
        '
        Me.gbMovieSetGeneralMediaListSortTokensOpts.AutoSize = True
        Me.tblMovieSetGeneralMediaListOpts.SetColumnSpan(Me.gbMovieSetGeneralMediaListSortTokensOpts, 2)
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Controls.Add(Me.tblMovieSetGeneralSortTokensOpts)
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Name = "gbMovieSetGeneralMediaListSortTokensOpts"
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Size = New System.Drawing.Size(212, 99)
        Me.gbMovieSetGeneralMediaListSortTokensOpts.TabIndex = 83
        Me.gbMovieSetGeneralMediaListSortTokensOpts.TabStop = False
        Me.gbMovieSetGeneralMediaListSortTokensOpts.Text = "Sort Tokens to Ignore"
        '
        'tblMovieSetGeneralSortTokensOpts
        '
        Me.tblMovieSetGeneralSortTokensOpts.AutoSize = True
        Me.tblMovieSetGeneralSortTokensOpts.ColumnCount = 5
        Me.tblMovieSetGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralSortTokensOpts.Controls.Add(Me.btnMovieSetSortTokenReset, 3, 1)
        Me.tblMovieSetGeneralSortTokensOpts.Controls.Add(Me.btnMovieSetSortTokenRemove, 2, 1)
        Me.tblMovieSetGeneralSortTokensOpts.Controls.Add(Me.lstMovieSetSortTokens, 0, 0)
        Me.tblMovieSetGeneralSortTokensOpts.Controls.Add(Me.btnMovieSetSortTokenAdd, 1, 1)
        Me.tblMovieSetGeneralSortTokensOpts.Controls.Add(Me.txtMovieSetSortToken, 0, 1)
        Me.tblMovieSetGeneralSortTokensOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetGeneralSortTokensOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetGeneralSortTokensOpts.Name = "tblMovieSetGeneralSortTokensOpts"
        Me.tblMovieSetGeneralSortTokensOpts.RowCount = 3
        Me.tblMovieSetGeneralSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralSortTokensOpts.Size = New System.Drawing.Size(206, 78)
        Me.tblMovieSetGeneralSortTokensOpts.TabIndex = 8
        '
        'btnMovieSetSortTokenReset
        '
        Me.btnMovieSetSortTokenReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieSetSortTokenReset.Image = CType(resources.GetObject("btnMovieSetSortTokenReset.Image"), System.Drawing.Image)
        Me.btnMovieSetSortTokenReset.Location = New System.Drawing.Point(180, 52)
        Me.btnMovieSetSortTokenReset.Name = "btnMovieSetSortTokenReset"
        Me.btnMovieSetSortTokenReset.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSetSortTokenReset.TabIndex = 10
        Me.btnMovieSetSortTokenReset.UseVisualStyleBackColor = True
        '
        'btnMovieSetSortTokenRemove
        '
        Me.btnMovieSetSortTokenRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieSetSortTokenRemove.Image = CType(resources.GetObject("btnMovieSetSortTokenRemove.Image"), System.Drawing.Image)
        Me.btnMovieSetSortTokenRemove.Location = New System.Drawing.Point(99, 52)
        Me.btnMovieSetSortTokenRemove.Name = "btnMovieSetSortTokenRemove"
        Me.btnMovieSetSortTokenRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSetSortTokenRemove.TabIndex = 3
        Me.btnMovieSetSortTokenRemove.UseVisualStyleBackColor = True
        '
        'lstMovieSetSortTokens
        '
        Me.tblMovieSetGeneralSortTokensOpts.SetColumnSpan(Me.lstMovieSetSortTokens, 4)
        Me.lstMovieSetSortTokens.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstMovieSetSortTokens.FormattingEnabled = True
        Me.lstMovieSetSortTokens.Location = New System.Drawing.Point(3, 3)
        Me.lstMovieSetSortTokens.Name = "lstMovieSetSortTokens"
        Me.lstMovieSetSortTokens.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstMovieSetSortTokens.Size = New System.Drawing.Size(200, 43)
        Me.lstMovieSetSortTokens.Sorted = True
        Me.lstMovieSetSortTokens.TabIndex = 0
        '
        'btnMovieSetSortTokenAdd
        '
        Me.btnMovieSetSortTokenAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieSetSortTokenAdd.Image = CType(resources.GetObject("btnMovieSetSortTokenAdd.Image"), System.Drawing.Image)
        Me.btnMovieSetSortTokenAdd.Location = New System.Drawing.Point(70, 52)
        Me.btnMovieSetSortTokenAdd.Name = "btnMovieSetSortTokenAdd"
        Me.btnMovieSetSortTokenAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSetSortTokenAdd.TabIndex = 2
        Me.btnMovieSetSortTokenAdd.UseVisualStyleBackColor = True
        '
        'txtMovieSetSortToken
        '
        Me.txtMovieSetSortToken.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSetSortToken.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieSetSortToken.Location = New System.Drawing.Point(3, 52)
        Me.txtMovieSetSortToken.Name = "txtMovieSetSortToken"
        Me.txtMovieSetSortToken.Size = New System.Drawing.Size(61, 22)
        Me.txtMovieSetSortToken.TabIndex = 1
        '
        'gbMovieSetGeneralMiscOpts
        '
        Me.gbMovieSetGeneralMiscOpts.AutoSize = True
        Me.gbMovieSetGeneralMiscOpts.Controls.Add(Me.tblMovieSetGeneralMiscOpts)
        Me.gbMovieSetGeneralMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetGeneralMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieSetGeneralMiscOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieSetGeneralMiscOpts.Name = "gbMovieSetGeneralMiscOpts"
        Me.gbMovieSetGeneralMiscOpts.Size = New System.Drawing.Size(224, 98)
        Me.gbMovieSetGeneralMiscOpts.TabIndex = 1
        Me.gbMovieSetGeneralMiscOpts.TabStop = False
        Me.gbMovieSetGeneralMiscOpts.Text = "Miscellaneous"
        '
        'tblMovieSetGeneralMiscOpts
        '
        Me.tblMovieSetGeneralMiscOpts.AutoSize = True
        Me.tblMovieSetGeneralMiscOpts.ColumnCount = 2
        Me.tblMovieSetGeneralMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetGeneralMiscOpts.Controls.Add(Me.chkMovieSetClickScrapeAsk, 0, 2)
        Me.tblMovieSetGeneralMiscOpts.Controls.Add(Me.chkMovieSetClickScrape, 0, 1)
        Me.tblMovieSetGeneralMiscOpts.Controls.Add(Me.chkMovieSetGeneralMarkNew, 0, 0)
        Me.tblMovieSetGeneralMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetGeneralMiscOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetGeneralMiscOpts.Name = "tblMovieSetGeneralMiscOpts"
        Me.tblMovieSetGeneralMiscOpts.RowCount = 4
        Me.tblMovieSetGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetGeneralMiscOpts.Size = New System.Drawing.Size(218, 77)
        Me.tblMovieSetGeneralMiscOpts.TabIndex = 7
        '
        'chkMovieSetClickScrapeAsk
        '
        Me.chkMovieSetClickScrapeAsk.AutoSize = True
        Me.chkMovieSetClickScrapeAsk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMovieSetClickScrapeAsk.Location = New System.Drawing.Point(3, 49)
        Me.chkMovieSetClickScrapeAsk.Name = "chkMovieSetClickScrapeAsk"
        Me.chkMovieSetClickScrapeAsk.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMovieSetClickScrapeAsk.Size = New System.Drawing.Size(152, 17)
        Me.chkMovieSetClickScrapeAsk.TabIndex = 64
        Me.chkMovieSetClickScrapeAsk.Text = "Show Results Dialog"
        Me.chkMovieSetClickScrapeAsk.UseVisualStyleBackColor = True
        '
        'chkMovieSetClickScrape
        '
        Me.chkMovieSetClickScrape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetClickScrape.AutoSize = True
        Me.chkMovieSetClickScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMovieSetClickScrape.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieSetClickScrape.Name = "chkMovieSetClickScrape"
        Me.chkMovieSetClickScrape.Size = New System.Drawing.Size(126, 17)
        Me.chkMovieSetClickScrape.TabIndex = 65
        Me.chkMovieSetClickScrape.Text = "Enable Click-Scrape"
        Me.chkMovieSetClickScrape.UseVisualStyleBackColor = True
        '
        'chkMovieSetGeneralMarkNew
        '
        Me.chkMovieSetGeneralMarkNew.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieSetGeneralMarkNew.AutoSize = True
        Me.chkMovieSetGeneralMarkNew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieSetGeneralMarkNew.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieSetGeneralMarkNew.Name = "chkMovieSetGeneralMarkNew"
        Me.chkMovieSetGeneralMarkNew.Size = New System.Drawing.Size(133, 17)
        Me.chkMovieSetGeneralMarkNew.TabIndex = 66
        Me.chkMovieSetGeneralMarkNew.Text = "Mark New MovieSets"
        Me.chkMovieSetGeneralMarkNew.UseVisualStyleBackColor = True
        '
        'frmMovieset_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmMovieset_GUI"
        Me.Text = "frmMovieSet_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbMovieSetGeneralCustomScrapeButton.ResumeLayout(False)
        Me.gbMovieSetGeneralCustomScrapeButton.PerformLayout()
        Me.tblMovieSetGeneralCustomScrapeButton.ResumeLayout(False)
        Me.tblMovieSetGeneralCustomScrapeButton.PerformLayout()
        Me.gbMovieSetGeneralMediaListOpts.ResumeLayout(False)
        Me.gbMovieSetGeneralMediaListOpts.PerformLayout()
        Me.tblMovieSetGeneralMediaListOpts.ResumeLayout(False)
        Me.tblMovieSetGeneralMediaListOpts.PerformLayout()
        Me.gbMovieSetGeneralMediaListSorting.ResumeLayout(False)
        Me.gbMovieSetGeneralMediaListSorting.PerformLayout()
        Me.tblMovieSetGeneralMediaListSorting.ResumeLayout(False)
        Me.gbMovieSetGeneralMediaListSortTokensOpts.ResumeLayout(False)
        Me.gbMovieSetGeneralMediaListSortTokensOpts.PerformLayout()
        Me.tblMovieSetGeneralSortTokensOpts.ResumeLayout(False)
        Me.tblMovieSetGeneralSortTokensOpts.PerformLayout()
        Me.gbMovieSetGeneralMiscOpts.ResumeLayout(False)
        Me.gbMovieSetGeneralMiscOpts.PerformLayout()
        Me.tblMovieSetGeneralMiscOpts.ResumeLayout(False)
        Me.tblMovieSetGeneralMiscOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbMovieSetGeneralCustomScrapeButton As GroupBox
    Friend WithEvents tblMovieSetGeneralCustomScrapeButton As TableLayoutPanel
    Friend WithEvents cbMovieSetGeneralCustomScrapeButtonScrapeType As ComboBox
    Friend WithEvents cbMovieSetGeneralCustomScrapeButtonModifierType As ComboBox
    Friend WithEvents txtMovieSetGeneralCustomScrapeButtonScrapeType As Label
    Friend WithEvents txtMovieSetGeneralCustomScrapeButtonModifierType As Label
    Friend WithEvents rbMovieSetGeneralCustomScrapeButtonEnabled As RadioButton
    Friend WithEvents rbMovieSetGeneralCustomScrapeButtonDisabled As RadioButton
    Friend WithEvents gbMovieSetGeneralMediaListOpts As GroupBox
    Friend WithEvents tblMovieSetGeneralMediaListOpts As TableLayoutPanel
    Friend WithEvents gbMovieSetGeneralMediaListSorting As GroupBox
    Friend WithEvents tblMovieSetGeneralMediaListSorting As TableLayoutPanel
    Friend WithEvents btnMovieSetGeneralMediaListSortingDown As Button
    Friend WithEvents btnMovieSetGeneralMediaListSortingUp As Button
    Friend WithEvents lvMovieSetGeneralMediaListSorting As ListView
    Friend WithEvents colMovieSetGeneralMediaListSortingDisplayIndex As ColumnHeader
    Friend WithEvents colMovieSetGeneralMediaListSortingColumn As ColumnHeader
    Friend WithEvents colMovieSetGeneralMediaListSortingLabel As ColumnHeader
    Friend WithEvents colMovieSetGeneralMediaListSortingHide As ColumnHeader
    Friend WithEvents btnMovieSetGeneralMediaListSortingReset As Button
    Friend WithEvents gbMovieSetGeneralMediaListSortTokensOpts As GroupBox
    Friend WithEvents tblMovieSetGeneralSortTokensOpts As TableLayoutPanel
    Friend WithEvents btnMovieSetSortTokenReset As Button
    Friend WithEvents btnMovieSetSortTokenRemove As Button
    Friend WithEvents lstMovieSetSortTokens As ListBox
    Friend WithEvents btnMovieSetSortTokenAdd As Button
    Friend WithEvents txtMovieSetSortToken As TextBox
    Friend WithEvents gbMovieSetGeneralMiscOpts As GroupBox
    Friend WithEvents tblMovieSetGeneralMiscOpts As TableLayoutPanel
    Friend WithEvents chkMovieSetClickScrapeAsk As CheckBox
    Friend WithEvents chkMovieSetClickScrape As CheckBox
    Friend WithEvents chkMovieSetGeneralMarkNew As CheckBox
End Class
