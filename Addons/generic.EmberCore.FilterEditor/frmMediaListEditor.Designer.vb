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
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.linklbl_FilterURL = New System.Windows.Forms.LinkLabel()
        Me.lbl_FilterURL = New System.Windows.Forms.Label()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.gpMediaList = New System.Windows.Forms.GroupBox()
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
        Me.txtView_Prefix = New System.Windows.Forms.TextBox()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.pnlFilter.SuspendLayout()
        Me.gpMediaList.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlFilter
        '
        Me.pnlFilter.Controls.Add(Me.linklbl_FilterURL)
        Me.pnlFilter.Controls.Add(Me.lbl_FilterURL)
        Me.pnlFilter.Controls.Add(Me.lblHelp)
        Me.pnlFilter.Controls.Add(Me.gpMediaList)
        Me.pnlFilter.Controls.Add(Me.cbMediaList)
        Me.pnlFilter.Controls.Add(Me.lbl_MediaLists)
        Me.pnlFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(627, 474)
        Me.pnlFilter.TabIndex = 0
        '
        'linklbl_FilterURL
        '
        Me.linklbl_FilterURL.AutoSize = True
        Me.linklbl_FilterURL.Location = New System.Drawing.Point(21, 423)
        Me.linklbl_FilterURL.Name = "linklbl_FilterURL"
        Me.linklbl_FilterURL.Size = New System.Drawing.Size(86, 13)
        Me.linklbl_FilterURL.TabIndex = 14
        Me.linklbl_FilterURL.TabStop = True
        Me.linklbl_FilterURL.Text = "Ember Database"
        '
        'lbl_FilterURL
        '
        Me.lbl_FilterURL.Location = New System.Drawing.Point(15, 380)
        Me.lbl_FilterURL.Name = "lbl_FilterURL"
        Me.lbl_FilterURL.Size = New System.Drawing.Size(593, 18)
        Me.lbl_FilterURL.TabIndex = 13
        Me.lbl_FilterURL.Text = "Complete overview of Ember datatables:"
        '
        'lblHelp
        '
        Me.lblHelp.Location = New System.Drawing.Point(18, 314)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(590, 53)
        Me.lblHelp.TabIndex = 12
        Me.lblHelp.Text = "Use CTRL + Return for new lines."
        '
        'gpMediaList
        '
        Me.gpMediaList.Controls.Add(Me.lblPrefix)
        Me.gpMediaList.Controls.Add(Me.txtView_Prefix)
        Me.gpMediaList.Controls.Add(Me.lblView_AS)
        Me.gpMediaList.Controls.Add(Me.lblViewCreate)
        Me.gpMediaList.Controls.Add(Me.txtView_Name)
        Me.gpMediaList.Controls.Add(Me.lbl_FilterType)
        Me.gpMediaList.Controls.Add(Me.cbMediaListType)
        Me.gpMediaList.Controls.Add(Me.btnRemoveView)
        Me.gpMediaList.Controls.Add(Me.btnAddView)
        Me.gpMediaList.Controls.Add(Me.txtView_Query)
        Me.gpMediaList.Location = New System.Drawing.Point(12, 63)
        Me.gpMediaList.Name = "gpMediaList"
        Me.gpMediaList.Size = New System.Drawing.Size(599, 217)
        Me.gpMediaList.TabIndex = 11
        Me.gpMediaList.TabStop = False
        Me.gpMediaList.Text = "Current Media List"
        '
        'lblView_AS
        '
        Me.lblView_AS.AutoSize = True
        Me.lblView_AS.Location = New System.Drawing.Point(308, 67)
        Me.lblView_AS.Name = "lblView_AS"
        Me.lblView_AS.Size = New System.Drawing.Size(26, 13)
        Me.lblView_AS.TabIndex = 16
        Me.lblView_AS.Text = "' AS"
        '
        'lblViewCreate
        '
        Me.lblViewCreate.AutoSize = True
        Me.lblViewCreate.Location = New System.Drawing.Point(9, 68)
        Me.lblViewCreate.Name = "lblViewCreate"
        Me.lblViewCreate.Size = New System.Drawing.Size(86, 13)
        Me.lblViewCreate.TabIndex = 15
        Me.lblViewCreate.Text = "CREATE VIEW '"
        '
        'txtView_Name
        '
        Me.txtView_Name.Location = New System.Drawing.Point(202, 64)
        Me.txtView_Name.Name = "txtView_Name"
        Me.txtView_Name.Size = New System.Drawing.Size(100, 20)
        Me.txtView_Name.TabIndex = 14
        '
        'lbl_FilterType
        '
        Me.lbl_FilterType.AutoSize = True
        Me.lbl_FilterType.Location = New System.Drawing.Point(9, 22)
        Me.lbl_FilterType.Name = "lbl_FilterType"
        Me.lbl_FilterType.Size = New System.Drawing.Size(31, 13)
        Me.lbl_FilterType.TabIndex = 13
        Me.lbl_FilterType.Text = "Type"
        '
        'cbMediaListType
        '
        Me.cbMediaListType.FormattingEnabled = True
        Me.cbMediaListType.Items.AddRange(New Object() {"movie", "sets", "tvshow", "seasons", "episode"})
        Me.cbMediaListType.Location = New System.Drawing.Point(96, 19)
        Me.cbMediaListType.Name = "cbMediaListType"
        Me.cbMediaListType.Size = New System.Drawing.Size(100, 21)
        Me.cbMediaListType.TabIndex = 12
        '
        'btnRemoveView
        '
        Me.btnRemoveView.Enabled = False
        Me.btnRemoveView.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveView.Image = CType(resources.GetObject("btnRemoveView.Image"), System.Drawing.Image)
        Me.btnRemoveView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveView.Location = New System.Drawing.Point(90, 188)
        Me.btnRemoveView.Name = "btnRemoveView"
        Me.btnRemoveView.Size = New System.Drawing.Size(72, 23)
        Me.btnRemoveView.TabIndex = 4
        Me.btnRemoveView.Text = "Remove"
        Me.btnRemoveView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemoveView.UseVisualStyleBackColor = True
        '
        'btnAddView
        '
        Me.btnAddView.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddView.Image = CType(resources.GetObject("btnAddView.Image"), System.Drawing.Image)
        Me.btnAddView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddView.Location = New System.Drawing.Point(12, 188)
        Me.btnAddView.Name = "btnAddView"
        Me.btnAddView.Size = New System.Drawing.Size(72, 23)
        Me.btnAddView.TabIndex = 3
        Me.btnAddView.Text = "Add"
        Me.btnAddView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddView.UseVisualStyleBackColor = True
        '
        'txtView_Query
        '
        Me.txtView_Query.Location = New System.Drawing.Point(9, 90)
        Me.txtView_Query.Multiline = True
        Me.txtView_Query.Name = "txtView_Query"
        Me.txtView_Query.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtView_Query.Size = New System.Drawing.Size(584, 91)
        Me.txtView_Query.TabIndex = 8
        '
        'cbMediaList
        '
        Me.cbMediaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMediaList.FormattingEnabled = True
        Me.cbMediaList.Location = New System.Drawing.Point(12, 36)
        Me.cbMediaList.Name = "cbMediaList"
        Me.cbMediaList.Size = New System.Drawing.Size(330, 21)
        Me.cbMediaList.TabIndex = 1
        '
        'lbl_MediaLists
        '
        Me.lbl_MediaLists.AutoSize = True
        Me.lbl_MediaLists.Location = New System.Drawing.Point(12, 20)
        Me.lbl_MediaLists.Name = "lbl_MediaLists"
        Me.lbl_MediaLists.Size = New System.Drawing.Size(60, 13)
        Me.lbl_MediaLists.TabIndex = 0
        Me.lbl_MediaLists.Text = "Media Lists"
        '
        'txtView_Prefix
        '
        Me.txtView_Prefix.Enabled = False
        Me.txtView_Prefix.Location = New System.Drawing.Point(96, 64)
        Me.txtView_Prefix.Name = "txtView_Prefix"
        Me.txtView_Prefix.Size = New System.Drawing.Size(100, 20)
        Me.txtView_Prefix.TabIndex = 17
        '
        'lblPrefix
        '
        Me.lblPrefix.AutoSize = True
        Me.lblPrefix.Location = New System.Drawing.Point(129, 48)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(33, 13)
        Me.lblPrefix.TabIndex = 18
        Me.lblPrefix.Text = "Prefix"
        '
        'frmMediaListEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(737, 544)
        Me.Controls.Add(Me.pnlFilter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMediaListEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFilterEditor"
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        Me.gpMediaList.ResumeLayout(False)
        Me.gpMediaList.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents lbl_MediaLists As System.Windows.Forms.Label
    Friend WithEvents cbMediaList As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemoveView As System.Windows.Forms.Button
    Friend WithEvents btnAddView As System.Windows.Forms.Button
    Friend WithEvents txtView_Query As System.Windows.Forms.TextBox
    Friend WithEvents gpMediaList As System.Windows.Forms.GroupBox
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

End Class
