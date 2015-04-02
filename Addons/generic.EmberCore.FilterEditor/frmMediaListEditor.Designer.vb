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
        Me.pnlMediaListEditor = New System.Windows.Forms.Panel()
        Me.linklbl_FilterURL = New System.Windows.Forms.LinkLabel()
        Me.lbl_FilterURL = New System.Windows.Forms.Label()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.gpMediaListCurrent = New System.Windows.Forms.GroupBox()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.txtView_Prefix = New System.Windows.Forms.TextBox()
        Me.lblView_AS = New System.Windows.Forms.Label()
        Me.lblViewCreate = New System.Windows.Forms.Label()
        Me.txtView_Name = New System.Windows.Forms.TextBox()
        Me.lbl_FilterType = New System.Windows.Forms.Label()
        Me.cbMediaListType = New System.Windows.Forms.ComboBox()
        Me.btnRemoveView = New System.Windows.Forms.Button()
        Me.btnAddView = New System.Windows.Forms.Button()
        Me.txtView_Query = New System.Windows.Forms.TextBox()
        Me.cbMediaList = New System.Windows.Forms.ComboBox()
        Me.lbl_MediaLists = New System.Windows.Forms.Label()
        Me.tblMediaListEditor = New System.Windows.Forms.TableLayoutPanel()
        Me.tblMediaListCurrent = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlMediaListEditor.SuspendLayout()
        Me.gpMediaListCurrent.SuspendLayout()
        Me.tblMediaListEditor.SuspendLayout()
        Me.tblMediaListCurrent.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMediaListEditor
        '
        Me.pnlMediaListEditor.AutoSize = True
        Me.pnlMediaListEditor.Controls.Add(Me.tblMediaListEditor)
        Me.pnlMediaListEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMediaListEditor.Location = New System.Drawing.Point(0, 0)
        Me.pnlMediaListEditor.Name = "pnlMediaListEditor"
        Me.pnlMediaListEditor.Size = New System.Drawing.Size(631, 432)
        Me.pnlMediaListEditor.TabIndex = 0
        '
        'linklbl_FilterURL
        '
        Me.linklbl_FilterURL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.linklbl_FilterURL.AutoSize = True
        Me.linklbl_FilterURL.Location = New System.Drawing.Point(3, 391)
        Me.linklbl_FilterURL.Name = "linklbl_FilterURL"
        Me.linklbl_FilterURL.Size = New System.Drawing.Size(86, 13)
        Me.linklbl_FilterURL.TabIndex = 14
        Me.linklbl_FilterURL.TabStop = True
        Me.linklbl_FilterURL.Text = "Ember Database"
        '
        'lbl_FilterURL
        '
        Me.lbl_FilterURL.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lbl_FilterURL.AutoSize = True
        Me.lbl_FilterURL.Location = New System.Drawing.Point(3, 371)
        Me.lbl_FilterURL.Name = "lbl_FilterURL"
        Me.lbl_FilterURL.Size = New System.Drawing.Size(197, 13)
        Me.lbl_FilterURL.TabIndex = 13
        Me.lbl_FilterURL.Text = "Complete overview of Ember datatables:"
        '
        'lblHelp
        '
        Me.lblHelp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHelp.AutoSize = True
        Me.lblHelp.Location = New System.Drawing.Point(3, 331)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(166, 13)
        Me.lblHelp.TabIndex = 12
        Me.lblHelp.Text = "Use CTRL + Return for new lines."
        '
        'gpMediaListCurrent
        '
        Me.gpMediaListCurrent.AutoSize = True
        Me.gpMediaListCurrent.Controls.Add(Me.tblMediaListCurrent)
        Me.gpMediaListCurrent.Location = New System.Drawing.Point(3, 50)
        Me.gpMediaListCurrent.Name = "gpMediaListCurrent"
        Me.gpMediaListCurrent.Size = New System.Drawing.Size(599, 275)
        Me.gpMediaListCurrent.TabIndex = 11
        Me.gpMediaListCurrent.TabStop = False
        Me.gpMediaListCurrent.Text = "Current Media List"
        '
        'lblPrefix
        '
        Me.lblPrefix.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPrefix.AutoSize = True
        Me.lblPrefix.Location = New System.Drawing.Point(127, 50)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(33, 13)
        Me.lblPrefix.TabIndex = 18
        Me.lblPrefix.Text = "Prefix"
        '
        'txtView_Prefix
        '
        Me.txtView_Prefix.Enabled = False
        Me.txtView_Prefix.Location = New System.Drawing.Point(95, 70)
        Me.txtView_Prefix.Name = "txtView_Prefix"
        Me.txtView_Prefix.Size = New System.Drawing.Size(98, 20)
        Me.txtView_Prefix.TabIndex = 17
        '
        'lblView_AS
        '
        Me.lblView_AS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblView_AS.AutoSize = True
        Me.lblView_AS.Location = New System.Drawing.Point(564, 73)
        Me.lblView_AS.Name = "lblView_AS"
        Me.lblView_AS.Size = New System.Drawing.Size(26, 13)
        Me.lblView_AS.TabIndex = 16
        Me.lblView_AS.Text = "' AS"
        '
        'lblViewCreate
        '
        Me.lblViewCreate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblViewCreate.AutoSize = True
        Me.lblViewCreate.Location = New System.Drawing.Point(3, 73)
        Me.lblViewCreate.Name = "lblViewCreate"
        Me.lblViewCreate.Size = New System.Drawing.Size(86, 13)
        Me.lblViewCreate.TabIndex = 15
        Me.lblViewCreate.Text = "CREATE VIEW '"
        '
        'txtView_Name
        '
        Me.txtView_Name.Enabled = False
        Me.txtView_Name.Location = New System.Drawing.Point(199, 70)
        Me.txtView_Name.Name = "txtView_Name"
        Me.txtView_Name.Size = New System.Drawing.Size(359, 20)
        Me.txtView_Name.TabIndex = 14
        '
        'lbl_FilterType
        '
        Me.lbl_FilterType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lbl_FilterType.AutoSize = True
        Me.lbl_FilterType.Location = New System.Drawing.Point(3, 7)
        Me.lbl_FilterType.Name = "lbl_FilterType"
        Me.lbl_FilterType.Size = New System.Drawing.Size(31, 13)
        Me.lbl_FilterType.TabIndex = 13
        Me.lbl_FilterType.Text = "Type"
        '
        'cbMediaListType
        '
        Me.cbMediaListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMediaListType.FormattingEnabled = True
        Me.cbMediaListType.Items.AddRange(New Object() {"movie", "sets", "tvshow", "seasons", "episode"})
        Me.cbMediaListType.Location = New System.Drawing.Point(95, 3)
        Me.cbMediaListType.Name = "cbMediaListType"
        Me.cbMediaListType.Size = New System.Drawing.Size(98, 21)
        Me.cbMediaListType.TabIndex = 12
        '
        'btnRemoveView
        '
        Me.btnRemoveView.AutoSize = True
        Me.btnRemoveView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRemoveView.Enabled = False
        Me.btnRemoveView.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveView.Image = CType(resources.GetObject("btnRemoveView.Image"), System.Drawing.Image)
        Me.btnRemoveView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveView.Location = New System.Drawing.Point(95, 230)
        Me.btnRemoveView.Name = "btnRemoveView"
        Me.btnRemoveView.Size = New System.Drawing.Size(98, 23)
        Me.btnRemoveView.TabIndex = 4
        Me.btnRemoveView.Text = "Remove"
        Me.btnRemoveView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemoveView.UseVisualStyleBackColor = True
        '
        'btnAddView
        '
        Me.btnAddView.AutoSize = True
        Me.btnAddView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAddView.Enabled = False
        Me.btnAddView.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddView.Image = CType(resources.GetObject("btnAddView.Image"), System.Drawing.Image)
        Me.btnAddView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddView.Location = New System.Drawing.Point(3, 230)
        Me.btnAddView.Name = "btnAddView"
        Me.btnAddView.Size = New System.Drawing.Size(86, 23)
        Me.btnAddView.TabIndex = 3
        Me.btnAddView.Text = "Add"
        Me.btnAddView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddView.UseVisualStyleBackColor = True
        '
        'txtView_Query
        '
        Me.tblMediaListCurrent.SetColumnSpan(Me.txtView_Query, 4)
        Me.txtView_Query.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtView_Query.Location = New System.Drawing.Point(3, 96)
        Me.txtView_Query.Multiline = True
        Me.txtView_Query.Name = "txtView_Query"
        Me.txtView_Query.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtView_Query.Size = New System.Drawing.Size(587, 128)
        Me.txtView_Query.TabIndex = 8
        '
        'cbMediaList
        '
        Me.cbMediaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMediaList.FormattingEnabled = True
        Me.cbMediaList.Location = New System.Drawing.Point(3, 23)
        Me.cbMediaList.Name = "cbMediaList"
        Me.cbMediaList.Size = New System.Drawing.Size(330, 21)
        Me.cbMediaList.TabIndex = 1
        '
        'lbl_MediaLists
        '
        Me.lbl_MediaLists.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lbl_MediaLists.AutoSize = True
        Me.lbl_MediaLists.Location = New System.Drawing.Point(3, 3)
        Me.lbl_MediaLists.Name = "lbl_MediaLists"
        Me.lbl_MediaLists.Size = New System.Drawing.Size(60, 13)
        Me.lbl_MediaLists.TabIndex = 0
        Me.lbl_MediaLists.Text = "Media Lists"
        '
        'tblMediaListEditor
        '
        Me.tblMediaListEditor.AutoScroll = True
        Me.tblMediaListEditor.AutoSize = True
        Me.tblMediaListEditor.ColumnCount = 2
        Me.tblMediaListEditor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListEditor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListEditor.Controls.Add(Me.lbl_MediaLists, 0, 0)
        Me.tblMediaListEditor.Controls.Add(Me.linklbl_FilterURL, 0, 6)
        Me.tblMediaListEditor.Controls.Add(Me.cbMediaList, 0, 1)
        Me.tblMediaListEditor.Controls.Add(Me.lbl_FilterURL, 0, 5)
        Me.tblMediaListEditor.Controls.Add(Me.lblHelp, 0, 3)
        Me.tblMediaListEditor.Controls.Add(Me.gpMediaListCurrent, 0, 2)
        Me.tblMediaListEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaListEditor.Location = New System.Drawing.Point(0, 0)
        Me.tblMediaListEditor.Name = "tblMediaListEditor"
        Me.tblMediaListEditor.RowCount = 8
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListEditor.Size = New System.Drawing.Size(631, 432)
        Me.tblMediaListEditor.TabIndex = 15
        '
        'tblMediaListCurrent
        '
        Me.tblMediaListCurrent.AutoSize = True
        Me.tblMediaListCurrent.ColumnCount = 5
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMediaListCurrent.Controls.Add(Me.lbl_FilterType, 0, 0)
        Me.tblMediaListCurrent.Controls.Add(Me.btnRemoveView, 1, 5)
        Me.tblMediaListCurrent.Controls.Add(Me.lblView_AS, 3, 3)
        Me.tblMediaListCurrent.Controls.Add(Me.btnAddView, 0, 5)
        Me.tblMediaListCurrent.Controls.Add(Me.txtView_Prefix, 1, 3)
        Me.tblMediaListCurrent.Controls.Add(Me.txtView_Query, 0, 4)
        Me.tblMediaListCurrent.Controls.Add(Me.txtView_Name, 2, 3)
        Me.tblMediaListCurrent.Controls.Add(Me.lblPrefix, 1, 2)
        Me.tblMediaListCurrent.Controls.Add(Me.cbMediaListType, 1, 0)
        Me.tblMediaListCurrent.Controls.Add(Me.lblViewCreate, 0, 3)
        Me.tblMediaListCurrent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMediaListCurrent.Location = New System.Drawing.Point(3, 16)
        Me.tblMediaListCurrent.Name = "tblMediaListCurrent"
        Me.tblMediaListCurrent.RowCount = 7
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMediaListCurrent.Size = New System.Drawing.Size(593, 256)
        Me.tblMediaListCurrent.TabIndex = 19
        '
        'frmMediaListEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 432)
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
        Me.gpMediaListCurrent.ResumeLayout(False)
        Me.gpMediaListCurrent.PerformLayout()
        Me.tblMediaListEditor.ResumeLayout(False)
        Me.tblMediaListEditor.PerformLayout()
        Me.tblMediaListCurrent.ResumeLayout(False)
        Me.tblMediaListCurrent.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMediaListEditor As System.Windows.Forms.Panel
    Friend WithEvents lbl_MediaLists As System.Windows.Forms.Label
    Friend WithEvents cbMediaList As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemoveView As System.Windows.Forms.Button
    Friend WithEvents btnAddView As System.Windows.Forms.Button
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

End Class
