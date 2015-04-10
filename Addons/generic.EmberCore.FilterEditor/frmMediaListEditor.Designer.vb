<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMediaListEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMediaListEditor))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlMediaListEditor = New System.Windows.Forms.Panel()
        Me.tblMediaListEditor = New System.Windows.Forms.TableLayoutPanel()
        Me.gpMediaListCurrent = New System.Windows.Forms.GroupBox()
        Me.tblMediaListCurrent = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_FilterType = New System.Windows.Forms.Label()
        Me.cbMediaList = New System.Windows.Forms.ComboBox()
        Me.linklbl_FilterURL = New System.Windows.Forms.LinkLabel()
        Me.btnViewRemove = New System.Windows.Forms.Button()
        Me.lblView_AS = New System.Windows.Forms.Label()
        Me.lbl_FilterURL = New System.Windows.Forms.Label()
        Me.btnViewAdd = New System.Windows.Forms.Button()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.txtView_Prefix = New System.Windows.Forms.TextBox()
        Me.txtView_Query = New System.Windows.Forms.TextBox()
        Me.txtView_Name = New System.Windows.Forms.TextBox()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.cbMediaListType = New System.Windows.Forms.ComboBox()
        Me.lblViewCreate = New System.Windows.Forms.Label()
        Me.gbCustomTabs = New System.Windows.Forms.GroupBox()
        Me.tblCustomTabs = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCustomTabsAdd = New System.Windows.Forms.Button()
        Me.btnCustomTabsRemove = New System.Windows.Forms.Button()
        Me.dgvCustomTabs = New System.Windows.Forms.DataGridView()
        Me.colCustomTabsName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCustomTabsList = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.pnlMediaListEditor.SuspendLayout()
        Me.tblMediaListEditor.SuspendLayout()
        Me.gpMediaListCurrent.SuspendLayout()
        Me.tblMediaListCurrent.SuspendLayout()
        Me.gbCustomTabs.SuspendLayout()
        Me.tblCustomTabs.SuspendLayout()
        CType(Me.dgvCustomTabs, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tblMediaListEditor.Controls.Add(Me.gpMediaListCurrent, 0, 0)
        Me.tblMediaListEditor.Controls.Add(Me.gbCustomTabs, 0, 2)
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
        'gpMediaListCurrent
        '
        Me.gpMediaListCurrent.AutoSize = True
        Me.gpMediaListCurrent.Controls.Add(Me.tblMediaListCurrent)
        Me.gpMediaListCurrent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpMediaListCurrent.Location = New System.Drawing.Point(3, 3)
        Me.gpMediaListCurrent.Name = "gpMediaListCurrent"
        Me.gpMediaListCurrent.Size = New System.Drawing.Size(599, 382)
        Me.gpMediaListCurrent.TabIndex = 11
        Me.gpMediaListCurrent.TabStop = False
        Me.gpMediaListCurrent.Text = "Custom Media Lists"
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
        Me.tblMediaListCurrent.Controls.Add(Me.lbl_FilterType, 0, 1)
        Me.tblMediaListCurrent.Controls.Add(Me.cbMediaList, 0, 0)
        Me.tblMediaListCurrent.Controls.Add(Me.linklbl_FilterURL, 0, 10)
        Me.tblMediaListCurrent.Controls.Add(Me.btnViewRemove, 5, 6)
        Me.tblMediaListCurrent.Controls.Add(Me.lblView_AS, 6, 4)
        Me.tblMediaListCurrent.Controls.Add(Me.lbl_FilterURL, 0, 9)
        Me.tblMediaListCurrent.Controls.Add(Me.btnViewAdd, 4, 6)
        Me.tblMediaListCurrent.Controls.Add(Me.lblHelp, 0, 7)
        Me.tblMediaListCurrent.Controls.Add(Me.txtView_Prefix, 1, 4)
        Me.tblMediaListCurrent.Controls.Add(Me.txtView_Query, 0, 5)
        Me.tblMediaListCurrent.Controls.Add(Me.txtView_Name, 3, 4)
        Me.tblMediaListCurrent.Controls.Add(Me.lblPrefix, 1, 3)
        Me.tblMediaListCurrent.Controls.Add(Me.cbMediaListType, 1, 1)
        Me.tblMediaListCurrent.Controls.Add(Me.lblViewCreate, 0, 4)
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
        'lbl_FilterType
        '
        Me.lbl_FilterType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lbl_FilterType.AutoSize = True
        Me.lbl_FilterType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_FilterType.Location = New System.Drawing.Point(3, 34)
        Me.lbl_FilterType.Name = "lbl_FilterType"
        Me.lbl_FilterType.Size = New System.Drawing.Size(31, 13)
        Me.lbl_FilterType.TabIndex = 13
        Me.lbl_FilterType.Text = "Type"
        '
        'cbMediaList
        '
        Me.tblMediaListCurrent.SetColumnSpan(Me.cbMediaList, 7)
        Me.cbMediaList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbMediaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMediaList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMediaList.FormattingEnabled = True
        Me.cbMediaList.Location = New System.Drawing.Point(3, 3)
        Me.cbMediaList.Name = "cbMediaList"
        Me.cbMediaList.Size = New System.Drawing.Size(587, 21)
        Me.cbMediaList.TabIndex = 1
        '
        'linklbl_FilterURL
        '
        Me.linklbl_FilterURL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.linklbl_FilterURL.AutoSize = True
        Me.tblMediaListCurrent.SetColumnSpan(Me.linklbl_FilterURL, 7)
        Me.linklbl_FilterURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linklbl_FilterURL.Location = New System.Drawing.Point(3, 346)
        Me.linklbl_FilterURL.Name = "linklbl_FilterURL"
        Me.linklbl_FilterURL.Size = New System.Drawing.Size(86, 13)
        Me.linklbl_FilterURL.TabIndex = 14
        Me.linklbl_FilterURL.TabStop = True
        Me.linklbl_FilterURL.Text = "Ember Database"
        '
        'btnViewRemove
        '
        Me.btnViewRemove.AutoSize = True
        Me.tblMediaListCurrent.SetColumnSpan(Me.btnViewRemove, 2)
        Me.btnViewRemove.Enabled = False
        Me.btnViewRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewRemove.Image = CType(resources.GetObject("btnViewRemove.Image"), System.Drawing.Image)
        Me.btnViewRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewRemove.Location = New System.Drawing.Point(500, 257)
        Me.btnViewRemove.Name = "btnViewRemove"
        Me.btnViewRemove.Size = New System.Drawing.Size(90, 23)
        Me.btnViewRemove.TabIndex = 4
        Me.btnViewRemove.Text = "Remove"
        Me.btnViewRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnViewRemove.UseVisualStyleBackColor = True
        '
        'lblView_AS
        '
        Me.lblView_AS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblView_AS.AutoSize = True
        Me.lblView_AS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblView_AS.Location = New System.Drawing.Point(564, 100)
        Me.lblView_AS.Name = "lblView_AS"
        Me.lblView_AS.Size = New System.Drawing.Size(26, 13)
        Me.lblView_AS.TabIndex = 16
        Me.lblView_AS.Text = "' AS"
        '
        'lbl_FilterURL
        '
        Me.lbl_FilterURL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lbl_FilterURL.AutoSize = True
        Me.tblMediaListCurrent.SetColumnSpan(Me.lbl_FilterURL, 7)
        Me.lbl_FilterURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_FilterURL.Location = New System.Drawing.Point(3, 326)
        Me.lbl_FilterURL.Name = "lbl_FilterURL"
        Me.lbl_FilterURL.Size = New System.Drawing.Size(197, 13)
        Me.lbl_FilterURL.TabIndex = 13
        Me.lbl_FilterURL.Text = "Complete overview of Ember datatables:"
        '
        'btnViewAdd
        '
        Me.btnViewAdd.AutoSize = True
        Me.btnViewAdd.Enabled = False
        Me.btnViewAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewAdd.Image = CType(resources.GetObject("btnViewAdd.Image"), System.Drawing.Image)
        Me.btnViewAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnViewAdd.Location = New System.Drawing.Point(404, 257)
        Me.btnViewAdd.Name = "btnViewAdd"
        Me.btnViewAdd.Size = New System.Drawing.Size(90, 23)
        Me.btnViewAdd.TabIndex = 3
        Me.btnViewAdd.Text = "Add"
        Me.btnViewAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnViewAdd.UseVisualStyleBackColor = True
        '
        'lblHelp
        '
        Me.lblHelp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHelp.AutoSize = True
        Me.tblMediaListCurrent.SetColumnSpan(Me.lblHelp, 7)
        Me.lblHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp.Location = New System.Drawing.Point(3, 286)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(166, 13)
        Me.lblHelp.TabIndex = 12
        Me.lblHelp.Text = "Use CTRL + Return for new lines."
        '
        'txtView_Prefix
        '
        Me.txtView_Prefix.Enabled = False
        Me.txtView_Prefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtView_Prefix.Location = New System.Drawing.Point(95, 97)
        Me.txtView_Prefix.Name = "txtView_Prefix"
        Me.txtView_Prefix.Size = New System.Drawing.Size(98, 20)
        Me.txtView_Prefix.TabIndex = 17
        '
        'txtView_Query
        '
        Me.tblMediaListCurrent.SetColumnSpan(Me.txtView_Query, 7)
        Me.txtView_Query.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtView_Query.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtView_Query.Location = New System.Drawing.Point(3, 123)
        Me.txtView_Query.Multiline = True
        Me.txtView_Query.Name = "txtView_Query"
        Me.txtView_Query.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtView_Query.Size = New System.Drawing.Size(587, 128)
        Me.txtView_Query.TabIndex = 8
        '
        'txtView_Name
        '
        Me.tblMediaListCurrent.SetColumnSpan(Me.txtView_Name, 3)
        Me.txtView_Name.Enabled = False
        Me.txtView_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtView_Name.Location = New System.Drawing.Point(199, 97)
        Me.txtView_Name.Name = "txtView_Name"
        Me.txtView_Name.Size = New System.Drawing.Size(359, 20)
        Me.txtView_Name.TabIndex = 14
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
        'cbMediaListType
        '
        Me.cbMediaListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMediaListType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMediaListType.FormattingEnabled = True
        Me.cbMediaListType.Items.AddRange(New Object() {"movie", "sets", "tvshow", "seasons", "episode"})
        Me.cbMediaListType.Location = New System.Drawing.Point(95, 30)
        Me.cbMediaListType.Name = "cbMediaListType"
        Me.cbMediaListType.Size = New System.Drawing.Size(98, 21)
        Me.cbMediaListType.TabIndex = 12
        '
        'lblViewCreate
        '
        Me.lblViewCreate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblViewCreate.AutoSize = True
        Me.lblViewCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblViewCreate.Location = New System.Drawing.Point(3, 100)
        Me.lblViewCreate.Name = "lblViewCreate"
        Me.lblViewCreate.Size = New System.Drawing.Size(86, 13)
        Me.lblViewCreate.TabIndex = 15
        Me.lblViewCreate.Text = "CREATE VIEW '"
        '
        'gbCustomTabs
        '
        Me.gbCustomTabs.AutoSize = True
        Me.gbCustomTabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbCustomTabs.Controls.Add(Me.tblCustomTabs)
        Me.gbCustomTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCustomTabs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbCustomTabs.Location = New System.Drawing.Point(3, 411)
        Me.gbCustomTabs.Name = "gbCustomTabs"
        Me.gbCustomTabs.Size = New System.Drawing.Size(599, 204)
        Me.gbCustomTabs.TabIndex = 15
        Me.gbCustomTabs.TabStop = False
        Me.gbCustomTabs.Text = "Custom Tabs"
        '
        'tblCustomTabs
        '
        Me.tblCustomTabs.AutoSize = True
        Me.tblCustomTabs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblCustomTabs.ColumnCount = 3
        Me.tblCustomTabs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomTabs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomTabs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCustomTabs.Controls.Add(Me.btnCustomTabsAdd, 1, 1)
        Me.tblCustomTabs.Controls.Add(Me.btnCustomTabsRemove, 2, 1)
        Me.tblCustomTabs.Controls.Add(Me.dgvCustomTabs, 0, 0)
        Me.tblCustomTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCustomTabs.Location = New System.Drawing.Point(3, 16)
        Me.tblCustomTabs.Name = "tblCustomTabs"
        Me.tblCustomTabs.RowCount = 3
        Me.tblCustomTabs.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomTabs.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomTabs.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCustomTabs.Size = New System.Drawing.Size(593, 185)
        Me.tblCustomTabs.TabIndex = 0
        '
        'btnCustomTabsAdd
        '
        Me.btnCustomTabsAdd.AutoSize = True
        Me.btnCustomTabsAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomTabsAdd.Image = CType(resources.GetObject("btnCustomTabsAdd.Image"), System.Drawing.Image)
        Me.btnCustomTabsAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCustomTabsAdd.Location = New System.Drawing.Point(404, 159)
        Me.btnCustomTabsAdd.Name = "btnCustomTabsAdd"
        Me.btnCustomTabsAdd.Size = New System.Drawing.Size(90, 23)
        Me.btnCustomTabsAdd.TabIndex = 13
        Me.btnCustomTabsAdd.Text = "Add"
        Me.btnCustomTabsAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCustomTabsAdd.UseVisualStyleBackColor = True
        '
        'btnCustomTabsRemove
        '
        Me.btnCustomTabsRemove.AutoSize = True
        Me.btnCustomTabsRemove.Enabled = False
        Me.btnCustomTabsRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomTabsRemove.Image = CType(resources.GetObject("btnCustomTabsRemove.Image"), System.Drawing.Image)
        Me.btnCustomTabsRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCustomTabsRemove.Location = New System.Drawing.Point(500, 159)
        Me.btnCustomTabsRemove.Name = "btnCustomTabsRemove"
        Me.btnCustomTabsRemove.Size = New System.Drawing.Size(90, 23)
        Me.btnCustomTabsRemove.TabIndex = 12
        Me.btnCustomTabsRemove.Text = "Remove"
        Me.btnCustomTabsRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCustomTabsRemove.UseVisualStyleBackColor = True
        '
        'dgvCustomTabs
        '
        Me.dgvCustomTabs.AllowUserToAddRows = False
        Me.dgvCustomTabs.AllowUserToDeleteRows = False
        Me.dgvCustomTabs.AllowUserToResizeColumns = False
        Me.dgvCustomTabs.AllowUserToResizeRows = False
        Me.dgvCustomTabs.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCustomTabs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCustomTabs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCustomTabs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCustomTabsName, Me.colCustomTabsList})
        Me.tblCustomTabs.SetColumnSpan(Me.dgvCustomTabs, 4)
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCustomTabs.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvCustomTabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCustomTabs.Location = New System.Drawing.Point(3, 3)
        Me.dgvCustomTabs.MultiSelect = False
        Me.dgvCustomTabs.Name = "dgvCustomTabs"
        Me.dgvCustomTabs.RowHeadersVisible = False
        Me.dgvCustomTabs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvCustomTabs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvCustomTabs.ShowCellErrors = False
        Me.dgvCustomTabs.ShowCellToolTips = False
        Me.dgvCustomTabs.ShowRowErrors = False
        Me.dgvCustomTabs.Size = New System.Drawing.Size(587, 150)
        Me.dgvCustomTabs.TabIndex = 11
        '
        'colCustomTabsName
        '
        Me.colCustomTabsName.FillWeight = 190.0!
        Me.colCustomTabsName.HeaderText = "Name"
        Me.colCustomTabsName.Name = "colCustomTabsName"
        Me.colCustomTabsName.Width = 190
        '
        'colCustomTabsList
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.colCustomTabsList.DefaultCellStyle = DataGridViewCellStyle2
        Me.colCustomTabsList.HeaderText = "List"
        Me.colCustomTabsList.Name = "colCustomTabsList"
        Me.colCustomTabsList.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colCustomTabsList.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colCustomTabsList.Width = 360
        '
        'frmMediaListEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 679)
        Me.Controls.Add(Me.pnlMediaListEditor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMediaListEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFilterEditor"
        Me.pnlMediaListEditor.ResumeLayout(False)
        Me.pnlMediaListEditor.PerformLayout()
        Me.tblMediaListEditor.ResumeLayout(False)
        Me.tblMediaListEditor.PerformLayout()
        Me.gpMediaListCurrent.ResumeLayout(False)
        Me.gpMediaListCurrent.PerformLayout()
        Me.tblMediaListCurrent.ResumeLayout(False)
        Me.tblMediaListCurrent.PerformLayout()
        Me.gbCustomTabs.ResumeLayout(False)
        Me.gbCustomTabs.PerformLayout()
        Me.tblCustomTabs.ResumeLayout(False)
        Me.tblCustomTabs.PerformLayout()
        CType(Me.dgvCustomTabs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMediaListEditor As System.Windows.Forms.Panel
    Friend WithEvents cbMediaList As System.Windows.Forms.ComboBox
    Friend WithEvents btnViewRemove As System.Windows.Forms.Button
    Friend WithEvents btnViewAdd As System.Windows.Forms.Button
    Friend WithEvents txtView_Query As System.Windows.Forms.TextBox
    Friend WithEvents gpMediaListCurrent As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_FilterType As System.Windows.Forms.Label
    Friend WithEvents cbMediaListType As System.Windows.Forms.ComboBox
    Friend WithEvents lblHelp As System.Windows.Forms.Label
    Friend WithEvents lbl_FilterURL As System.Windows.Forms.Label
    Friend WithEvents linklbl_FilterURL As System.Windows.Forms.LinkLabel
    Friend WithEvents lblView_AS As System.Windows.Forms.Label
    Friend WithEvents lblViewCreate As System.Windows.Forms.Label
    Friend WithEvents txtView_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtView_Prefix As System.Windows.Forms.TextBox
    Friend WithEvents lblPrefix As System.Windows.Forms.Label
    Friend WithEvents tblMediaListEditor As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMediaListCurrent As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbCustomTabs As System.Windows.Forms.GroupBox
    Friend WithEvents tblCustomTabs As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents dgvCustomTabs As System.Windows.Forms.DataGridView
    Friend WithEvents btnCustomTabsRemove As System.Windows.Forms.Button
    Friend WithEvents btnCustomTabsAdd As System.Windows.Forms.Button
    Friend WithEvents colCustomTabsName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCustomTabsList As System.Windows.Forms.DataGridViewComboBoxColumn

End Class
