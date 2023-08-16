<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsHolder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsHolder))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlMediaListEditor = New System.Windows.Forms.Panel()
        Me.tblMediaListEditor = New System.Windows.Forms.TableLayoutPanel()
        Me.gbCustomMediaList = New System.Windows.Forms.GroupBox()
        Me.tblMediaListCurrent = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCustomMediaListType = New System.Windows.Forms.Label()
        Me.cbCustomMediaList = New System.Windows.Forms.ComboBox()
        Me.lnkCustomMediaListURL = New System.Windows.Forms.LinkLabel()
        Me.btnCustomMediaListRemove = New System.Windows.Forms.Button()
        Me.lblCustomMediaListAS = New System.Windows.Forms.Label()
        Me.lblCustomMediaListURL = New System.Windows.Forms.Label()
        Me.btnCustomMediaListAdd = New System.Windows.Forms.Button()
        Me.lblCustomMediaListHelp = New System.Windows.Forms.Label()
        Me.txtCustomMediaListPrefix = New System.Windows.Forms.TextBox()
        Me.txtCustomMediaListQuery = New System.Windows.Forms.TextBox()
        Me.txtCustomMediaListName = New System.Windows.Forms.TextBox()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.cbCustomMediaListType = New System.Windows.Forms.ComboBox()
        Me.lblCustomMediaListCREATE = New System.Windows.Forms.Label()
        Me.gbCustomTab = New System.Windows.Forms.GroupBox()
        Me.tblCustomTabs = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCustomTabAdd = New System.Windows.Forms.Button()
        Me.btnCustomTabRemove = New System.Windows.Forms.Button()
        Me.dgvCustomTab = New System.Windows.Forms.DataGridView()
        Me.colCustomTabName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCustomTabList = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.pnlMediaListEditor.SuspendLayout()
        Me.tblMediaListEditor.SuspendLayout()
        Me.gbCustomMediaList.SuspendLayout()
        Me.tblMediaListCurrent.SuspendLayout()
        Me.gbCustomTab.SuspendLayout()
        Me.tblCustomTabs.SuspendLayout()
        CType(Me.dgvCustomTab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMediaListEditor
        '
        Me.pnlMediaListEditor.AutoSize = True
        Me.pnlMediaListEditor.Controls.Add(Me.tblMediaListEditor)
        Me.pnlMediaListEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMediaListEditor.Location = New System.Drawing.Point(0, 0)
        Me.pnlMediaListEditor.Name = "pnlMediaListEditor"
        Me.pnlMediaListEditor.Size = New System.Drawing.Size(701, 679)
        Me.pnlMediaListEditor.TabIndex = 0
        '
        'tblMediaListEditor
        '
        Me.tblMediaListEditor.AutoScroll = True
        Me.tblMediaListEditor.AutoSize = True
        Me.tblMediaListEditor.ColumnCount = 2
        Me.tblMediaListEditor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListEditor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListEditor.Controls.Add(Me.gbCustomMediaList, 0, 0)
        Me.tblMediaListEditor.Controls.Add(Me.gbCustomTab, 0, 2)
        Me.tblMediaListEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaListEditor.Location = New System.Drawing.Point(0, 0)
        Me.tblMediaListEditor.Name = "tblMediaListEditor"
        Me.tblMediaListEditor.RowCount = 4
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.Size = New System.Drawing.Size(701, 679)
        Me.tblMediaListEditor.TabIndex = 15
        '
        'gbCustomMediaList
        '
        Me.gbCustomMediaList.AutoSize = True
        Me.gbCustomMediaList.Controls.Add(Me.tblMediaListCurrent)
        Me.gbCustomMediaList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbCustomMediaList.Location = New System.Drawing.Point(3, 3)
        Me.gbCustomMediaList.Name = "gbCustomMediaList"
        Me.gbCustomMediaList.Size = New System.Drawing.Size(599, 382)
        Me.gbCustomMediaList.TabIndex = 11
        Me.gbCustomMediaList.TabStop = False
        Me.gbCustomMediaList.Text = "Custom Media Lists"
        '
        'tblMediaListCurrent
        '
        Me.tblMediaListCurrent.AutoSize = True
        Me.tblMediaListCurrent.ColumnCount = 7
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.Controls.Add(Me.lblCustomMediaListType, 0, 1)
        Me.tblMediaListCurrent.Controls.Add(Me.cbCustomMediaList, 0, 0)
        Me.tblMediaListCurrent.Controls.Add(Me.lnkCustomMediaListURL, 0, 10)
        Me.tblMediaListCurrent.Controls.Add(Me.btnCustomMediaListRemove, 5, 6)
        Me.tblMediaListCurrent.Controls.Add(Me.lblCustomMediaListAS, 6, 4)
        Me.tblMediaListCurrent.Controls.Add(Me.lblCustomMediaListURL, 0, 9)
        Me.tblMediaListCurrent.Controls.Add(Me.btnCustomMediaListAdd, 4, 6)
        Me.tblMediaListCurrent.Controls.Add(Me.lblCustomMediaListHelp, 0, 7)
        Me.tblMediaListCurrent.Controls.Add(Me.txtCustomMediaListPrefix, 1, 4)
        Me.tblMediaListCurrent.Controls.Add(Me.txtCustomMediaListQuery, 0, 5)
        Me.tblMediaListCurrent.Controls.Add(Me.txtCustomMediaListName, 3, 4)
        Me.tblMediaListCurrent.Controls.Add(Me.lblPrefix, 1, 3)
        Me.tblMediaListCurrent.Controls.Add(Me.cbCustomMediaListType, 1, 1)
        Me.tblMediaListCurrent.Controls.Add(Me.lblCustomMediaListCREATE, 0, 4)
        Me.tblMediaListCurrent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaListCurrent.Location = New System.Drawing.Point(3, 16)
        Me.tblMediaListCurrent.Name = "tblMediaListCurrent"
        Me.tblMediaListCurrent.RowCount = 12
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.Size = New System.Drawing.Size(593, 363)
        Me.tblMediaListCurrent.TabIndex = 19
        '
        'lblCustomMediaListType
        '
        Me.lblCustomMediaListType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMediaListType.AutoSize = True
        Me.lblCustomMediaListType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomMediaListType.Location = New System.Drawing.Point(3, 34)
        Me.lblCustomMediaListType.Name = "lblCustomMediaListType"
        Me.lblCustomMediaListType.Size = New System.Drawing.Size(31, 13)
        Me.lblCustomMediaListType.TabIndex = 13
        Me.lblCustomMediaListType.Text = "Type"
        '
        'cbCustomMediaList
        '
        Me.tblMediaListCurrent.SetColumnSpan(Me.cbCustomMediaList, 7)
        Me.cbCustomMediaList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbCustomMediaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCustomMediaList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCustomMediaList.FormattingEnabled = True
        Me.cbCustomMediaList.Location = New System.Drawing.Point(3, 3)
        Me.cbCustomMediaList.Name = "cbCustomMediaList"
        Me.cbCustomMediaList.Size = New System.Drawing.Size(587, 21)
        Me.cbCustomMediaList.TabIndex = 1
        '
        'lnkCustomMediaListURL
        '
        Me.lnkCustomMediaListURL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lnkCustomMediaListURL.AutoSize = True
        Me.tblMediaListCurrent.SetColumnSpan(Me.lnkCustomMediaListURL, 7)
        Me.lnkCustomMediaListURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkCustomMediaListURL.Location = New System.Drawing.Point(3, 346)
        Me.lnkCustomMediaListURL.Name = "lnkCustomMediaListURL"
        Me.lnkCustomMediaListURL.Size = New System.Drawing.Size(86, 13)
        Me.lnkCustomMediaListURL.TabIndex = 14
        Me.lnkCustomMediaListURL.TabStop = True
        Me.lnkCustomMediaListURL.Text = "Ember Database"
        '
        'btnCustomMediaListRemove
        '
        Me.btnCustomMediaListRemove.AutoSize = True
        Me.tblMediaListCurrent.SetColumnSpan(Me.btnCustomMediaListRemove, 2)
        Me.btnCustomMediaListRemove.Enabled = False
        Me.btnCustomMediaListRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomMediaListRemove.Image = CType(resources.GetObject("btnCustomMediaListRemove.Image"), System.Drawing.Image)
        Me.btnCustomMediaListRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCustomMediaListRemove.Location = New System.Drawing.Point(500, 257)
        Me.btnCustomMediaListRemove.Name = "btnCustomMediaListRemove"
        Me.btnCustomMediaListRemove.Size = New System.Drawing.Size(90, 23)
        Me.btnCustomMediaListRemove.TabIndex = 4
        Me.btnCustomMediaListRemove.Text = "Remove"
        Me.btnCustomMediaListRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCustomMediaListRemove.UseVisualStyleBackColor = True
        '
        'lblCustomMediaListAS
        '
        Me.lblCustomMediaListAS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMediaListAS.AutoSize = True
        Me.lblCustomMediaListAS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomMediaListAS.Location = New System.Drawing.Point(564, 100)
        Me.lblCustomMediaListAS.Name = "lblCustomMediaListAS"
        Me.lblCustomMediaListAS.Size = New System.Drawing.Size(26, 13)
        Me.lblCustomMediaListAS.TabIndex = 16
        Me.lblCustomMediaListAS.Text = "' AS"
        '
        'lblCustomMediaListURL
        '
        Me.lblCustomMediaListURL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMediaListURL.AutoSize = True
        Me.tblMediaListCurrent.SetColumnSpan(Me.lblCustomMediaListURL, 7)
        Me.lblCustomMediaListURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomMediaListURL.Location = New System.Drawing.Point(3, 326)
        Me.lblCustomMediaListURL.Name = "lblCustomMediaListURL"
        Me.lblCustomMediaListURL.Size = New System.Drawing.Size(197, 13)
        Me.lblCustomMediaListURL.TabIndex = 13
        Me.lblCustomMediaListURL.Text = "Complete overview of Ember datatables:"
        '
        'btnCustomMediaListAdd
        '
        Me.btnCustomMediaListAdd.AutoSize = True
        Me.btnCustomMediaListAdd.Enabled = False
        Me.btnCustomMediaListAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomMediaListAdd.Image = CType(resources.GetObject("btnCustomMediaListAdd.Image"), System.Drawing.Image)
        Me.btnCustomMediaListAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCustomMediaListAdd.Location = New System.Drawing.Point(404, 257)
        Me.btnCustomMediaListAdd.Name = "btnCustomMediaListAdd"
        Me.btnCustomMediaListAdd.Size = New System.Drawing.Size(90, 23)
        Me.btnCustomMediaListAdd.TabIndex = 3
        Me.btnCustomMediaListAdd.Text = "Add"
        Me.btnCustomMediaListAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCustomMediaListAdd.UseVisualStyleBackColor = True
        '
        'lblCustomMediaListHelp
        '
        Me.lblCustomMediaListHelp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMediaListHelp.AutoSize = True
        Me.tblMediaListCurrent.SetColumnSpan(Me.lblCustomMediaListHelp, 7)
        Me.lblCustomMediaListHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomMediaListHelp.Location = New System.Drawing.Point(3, 286)
        Me.lblCustomMediaListHelp.Name = "lblCustomMediaListHelp"
        Me.lblCustomMediaListHelp.Size = New System.Drawing.Size(166, 13)
        Me.lblCustomMediaListHelp.TabIndex = 12
        Me.lblCustomMediaListHelp.Text = "Use CTRL + Return for new lines."
        '
        'txtCustomMediaListPrefix
        '
        Me.txtCustomMediaListPrefix.Enabled = False
        Me.txtCustomMediaListPrefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomMediaListPrefix.Location = New System.Drawing.Point(95, 97)
        Me.txtCustomMediaListPrefix.Name = "txtCustomMediaListPrefix"
        Me.txtCustomMediaListPrefix.Size = New System.Drawing.Size(98, 20)
        Me.txtCustomMediaListPrefix.TabIndex = 17
        '
        'txtCustomMediaListQuery
        '
        Me.tblMediaListCurrent.SetColumnSpan(Me.txtCustomMediaListQuery, 7)
        Me.txtCustomMediaListQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomMediaListQuery.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomMediaListQuery.Location = New System.Drawing.Point(3, 123)
        Me.txtCustomMediaListQuery.Multiline = True
        Me.txtCustomMediaListQuery.Name = "txtCustomMediaListQuery"
        Me.txtCustomMediaListQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCustomMediaListQuery.Size = New System.Drawing.Size(587, 128)
        Me.txtCustomMediaListQuery.TabIndex = 8
        '
        'txtCustomMediaListName
        '
        Me.tblMediaListCurrent.SetColumnSpan(Me.txtCustomMediaListName, 3)
        Me.txtCustomMediaListName.Enabled = False
        Me.txtCustomMediaListName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomMediaListName.Location = New System.Drawing.Point(199, 97)
        Me.txtCustomMediaListName.Name = "txtCustomMediaListName"
        Me.txtCustomMediaListName.Size = New System.Drawing.Size(359, 20)
        Me.txtCustomMediaListName.TabIndex = 14
        '
        'lblPrefix
        '
        Me.lblPrefix.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPrefix.AutoSize = True
        Me.lblPrefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrefix.Location = New System.Drawing.Point(127, 77)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(33, 13)
        Me.lblPrefix.TabIndex = 18
        Me.lblPrefix.Text = "Prefix"
        '
        'cbCustomMediaListType
        '
        Me.cbCustomMediaListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCustomMediaListType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCustomMediaListType.FormattingEnabled = True
        Me.cbCustomMediaListType.Items.AddRange(New Object() {"movie", "sets", "tvshow", "seasons", "episode"})
        Me.cbCustomMediaListType.Location = New System.Drawing.Point(95, 30)
        Me.cbCustomMediaListType.Name = "cbCustomMediaListType"
        Me.cbCustomMediaListType.Size = New System.Drawing.Size(98, 21)
        Me.cbCustomMediaListType.TabIndex = 12
        '
        'lblCustomMediaListCREATE
        '
        Me.lblCustomMediaListCREATE.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCustomMediaListCREATE.AutoSize = True
        Me.lblCustomMediaListCREATE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomMediaListCREATE.Location = New System.Drawing.Point(3, 100)
        Me.lblCustomMediaListCREATE.Name = "lblCustomMediaListCREATE"
        Me.lblCustomMediaListCREATE.Size = New System.Drawing.Size(86, 13)
        Me.lblCustomMediaListCREATE.TabIndex = 15
        Me.lblCustomMediaListCREATE.Text = "CREATE VIEW '"
        '
        'gbCustomTab
        '
        Me.gbCustomTab.AutoSize = True
        Me.gbCustomTab.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbCustomTab.Controls.Add(Me.tblCustomTabs)
        Me.gbCustomTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCustomTab.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbCustomTab.Location = New System.Drawing.Point(3, 411)
        Me.gbCustomTab.Name = "gbCustomTab"
        Me.gbCustomTab.Size = New System.Drawing.Size(599, 204)
        Me.gbCustomTab.TabIndex = 15
        Me.gbCustomTab.TabStop = False
        Me.gbCustomTab.Text = "Custom Tabs"
        '
        'tblCustomTabs
        '
        Me.tblCustomTabs.AutoSize = True
        Me.tblCustomTabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblCustomTabs.ColumnCount = 3
        Me.tblCustomTabs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomTabs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomTabs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomTabs.Controls.Add(Me.btnCustomTabAdd, 1, 1)
        Me.tblCustomTabs.Controls.Add(Me.btnCustomTabRemove, 2, 1)
        Me.tblCustomTabs.Controls.Add(Me.dgvCustomTab, 0, 0)
        Me.tblCustomTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCustomTabs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tblCustomTabs.Location = New System.Drawing.Point(3, 16)
        Me.tblCustomTabs.Name = "tblCustomTabs"
        Me.tblCustomTabs.RowCount = 3
        Me.tblCustomTabs.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomTabs.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomTabs.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomTabs.Size = New System.Drawing.Size(593, 185)
        Me.tblCustomTabs.TabIndex = 0
        '
        'btnCustomTabAdd
        '
        Me.btnCustomTabAdd.AutoSize = True
        Me.btnCustomTabAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomTabAdd.Image = CType(resources.GetObject("btnCustomTabAdd.Image"), System.Drawing.Image)
        Me.btnCustomTabAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCustomTabAdd.Location = New System.Drawing.Point(404, 159)
        Me.btnCustomTabAdd.Name = "btnCustomTabAdd"
        Me.btnCustomTabAdd.Size = New System.Drawing.Size(90, 23)
        Me.btnCustomTabAdd.TabIndex = 13
        Me.btnCustomTabAdd.Text = "Add"
        Me.btnCustomTabAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCustomTabAdd.UseVisualStyleBackColor = True
        '
        'btnCustomTabRemove
        '
        Me.btnCustomTabRemove.AutoSize = True
        Me.btnCustomTabRemove.Enabled = False
        Me.btnCustomTabRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomTabRemove.Image = CType(resources.GetObject("btnCustomTabRemove.Image"), System.Drawing.Image)
        Me.btnCustomTabRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCustomTabRemove.Location = New System.Drawing.Point(500, 159)
        Me.btnCustomTabRemove.Name = "btnCustomTabRemove"
        Me.btnCustomTabRemove.Size = New System.Drawing.Size(90, 23)
        Me.btnCustomTabRemove.TabIndex = 12
        Me.btnCustomTabRemove.Text = "Remove"
        Me.btnCustomTabRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCustomTabRemove.UseVisualStyleBackColor = True
        '
        'dgvCustomTab
        '
        Me.dgvCustomTab.AllowUserToAddRows = False
        Me.dgvCustomTab.AllowUserToDeleteRows = False
        Me.dgvCustomTab.AllowUserToResizeColumns = False
        Me.dgvCustomTab.AllowUserToResizeRows = False
        Me.dgvCustomTab.BackgroundColor = System.Drawing.Color.White
        Me.dgvCustomTab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCustomTab.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCustomTabName, Me.colCustomTabList})
        Me.tblCustomTabs.SetColumnSpan(Me.dgvCustomTab, 4)
        Me.dgvCustomTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCustomTab.Location = New System.Drawing.Point(3, 3)
        Me.dgvCustomTab.MultiSelect = False
        Me.dgvCustomTab.Name = "dgvCustomTab"
        Me.dgvCustomTab.RowHeadersVisible = False
        Me.dgvCustomTab.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvCustomTab.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvCustomTab.ShowCellErrors = False
        Me.dgvCustomTab.ShowCellToolTips = False
        Me.dgvCustomTab.ShowRowErrors = False
        Me.dgvCustomTab.Size = New System.Drawing.Size(587, 150)
        Me.dgvCustomTab.TabIndex = 11
        '
        'colCustomTabName
        '
        Me.colCustomTabName.FillWeight = 190.0!
        Me.colCustomTabName.HeaderText = "Name"
        Me.colCustomTabName.Name = "colCustomTabName"
        Me.colCustomTabName.Width = 190
        '
        'colCustomTabList
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colCustomTabList.DefaultCellStyle = DataGridViewCellStyle1
        Me.colCustomTabList.HeaderText = "List"
        Me.colCustomTabList.Name = "colCustomTabList"
        Me.colCustomTabList.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colCustomTabList.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colCustomTabList.Width = 360
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 679)
        Me.Controls.Add(Me.pnlMediaListEditor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFilterEditor"
        Me.pnlMediaListEditor.ResumeLayout(False)
        Me.pnlMediaListEditor.PerformLayout()
        Me.tblMediaListEditor.ResumeLayout(False)
        Me.tblMediaListEditor.PerformLayout()
        Me.gbCustomMediaList.ResumeLayout(False)
        Me.gbCustomMediaList.PerformLayout()
        Me.tblMediaListCurrent.ResumeLayout(False)
        Me.tblMediaListCurrent.PerformLayout()
        Me.gbCustomTab.ResumeLayout(False)
        Me.gbCustomTab.PerformLayout()
        Me.tblCustomTabs.ResumeLayout(False)
        Me.tblCustomTabs.PerformLayout()
        CType(Me.dgvCustomTab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMediaListEditor As System.Windows.Forms.Panel
    Friend WithEvents cbCustomMediaList As System.Windows.Forms.ComboBox
    Friend WithEvents btnCustomMediaListRemove As System.Windows.Forms.Button
    Friend WithEvents btnCustomMediaListAdd As System.Windows.Forms.Button
    Friend WithEvents txtCustomMediaListQuery As System.Windows.Forms.TextBox
    Friend WithEvents gbCustomMediaList As System.Windows.Forms.GroupBox
    Friend WithEvents lblCustomMediaListType As System.Windows.Forms.Label
    Friend WithEvents cbCustomMediaListType As System.Windows.Forms.ComboBox
    Friend WithEvents lblCustomMediaListHelp As System.Windows.Forms.Label
    Friend WithEvents lblCustomMediaListURL As System.Windows.Forms.Label
    Friend WithEvents lnkCustomMediaListURL As System.Windows.Forms.LinkLabel
    Friend WithEvents lblCustomMediaListAS As System.Windows.Forms.Label
    Friend WithEvents lblCustomMediaListCREATE As System.Windows.Forms.Label
    Friend WithEvents txtCustomMediaListName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomMediaListPrefix As System.Windows.Forms.TextBox
    Friend WithEvents lblPrefix As System.Windows.Forms.Label
    Friend WithEvents tblMediaListEditor As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMediaListCurrent As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbCustomTab As System.Windows.Forms.GroupBox
    Friend WithEvents tblCustomTabs As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgvCustomTab As System.Windows.Forms.DataGridView
    Friend WithEvents btnCustomTabRemove As System.Windows.Forms.Button
    Friend WithEvents btnCustomTabAdd As System.Windows.Forms.Button
    Friend WithEvents colCustomTabName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCustomTabList As System.Windows.Forms.DataGridViewComboBoxColumn

End Class
