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
        Me.lbl_FilterHelp = New System.Windows.Forms.Label()
        Me.gpMediaList = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtViewName = New System.Windows.Forms.TextBox()
        Me.lbl_FilterType = New System.Windows.Forms.Label()
        Me.cbMediaListType = New System.Windows.Forms.ComboBox()
        Me.btnRemoveView = New System.Windows.Forms.Button()
        Me.btnAddView = New System.Windows.Forms.Button()
        Me.txtViewQuery = New System.Windows.Forms.TextBox()
        Me.cbMediaList = New System.Windows.Forms.ComboBox()
        Me.lbl_MediaLists = New System.Windows.Forms.Label()
        Me.pnlFilter.SuspendLayout()
        Me.gpMediaList.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlFilter
        '
        Me.pnlFilter.Controls.Add(Me.linklbl_FilterURL)
        Me.pnlFilter.Controls.Add(Me.lbl_FilterURL)
        Me.pnlFilter.Controls.Add(Me.lbl_FilterHelp)
        Me.pnlFilter.Controls.Add(Me.gpMediaList)
        Me.pnlFilter.Controls.Add(Me.cbMediaList)
        Me.pnlFilter.Controls.Add(Me.lbl_MediaLists)
        Me.pnlFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(627, 367)
        Me.pnlFilter.TabIndex = 0
        '
        'linklbl_FilterURL
        '
        Me.linklbl_FilterURL.AutoSize = True
        Me.linklbl_FilterURL.Location = New System.Drawing.Point(18, 354)
        Me.linklbl_FilterURL.Name = "linklbl_FilterURL"
        Me.linklbl_FilterURL.Size = New System.Drawing.Size(86, 13)
        Me.linklbl_FilterURL.TabIndex = 14
        Me.linklbl_FilterURL.TabStop = True
        Me.linklbl_FilterURL.Text = "Ember Database"
        '
        'lbl_FilterURL
        '
        Me.lbl_FilterURL.Location = New System.Drawing.Point(18, 336)
        Me.lbl_FilterURL.Name = "lbl_FilterURL"
        Me.lbl_FilterURL.Size = New System.Drawing.Size(593, 18)
        Me.lbl_FilterURL.TabIndex = 13
        Me.lbl_FilterURL.Text = "Complete overview of Ember datatables:"
        '
        'lbl_FilterHelp
        '
        Me.lbl_FilterHelp.Location = New System.Drawing.Point(18, 283)
        Me.lbl_FilterHelp.Name = "lbl_FilterHelp"
        Me.lbl_FilterHelp.Size = New System.Drawing.Size(590, 53)
        Me.lbl_FilterHelp.TabIndex = 12
        Me.lbl_FilterHelp.Text = "Result of query must contain either field idMovie (Movie-Filter), idSet(Set-Filte" & _
    "r) or idShow(Show-Filter) or/and idMedia!"
        '
        'gpMediaList
        '
        Me.gpMediaList.Controls.Add(Me.Label2)
        Me.gpMediaList.Controls.Add(Me.Label1)
        Me.gpMediaList.Controls.Add(Me.txtViewName)
        Me.gpMediaList.Controls.Add(Me.lbl_FilterType)
        Me.gpMediaList.Controls.Add(Me.cbMediaListType)
        Me.gpMediaList.Controls.Add(Me.btnRemoveView)
        Me.gpMediaList.Controls.Add(Me.btnAddView)
        Me.gpMediaList.Controls.Add(Me.txtViewQuery)
        Me.gpMediaList.Location = New System.Drawing.Point(12, 63)
        Me.gpMediaList.Name = "gpMediaList"
        Me.gpMediaList.Size = New System.Drawing.Size(599, 217)
        Me.gpMediaList.TabIndex = 11
        Me.gpMediaList.TabStop = False
        Me.gpMediaList.Text = "Current Media List"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(202, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "AS"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "CREATE VIEW"
        '
        'txtViewName
        '
        Me.txtViewName.Location = New System.Drawing.Point(96, 65)
        Me.txtViewName.Name = "txtViewName"
        Me.txtViewName.Size = New System.Drawing.Size(100, 20)
        Me.txtViewName.TabIndex = 14
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
        Me.cbMediaListType.Location = New System.Drawing.Point(66, 19)
        Me.cbMediaListType.Name = "cbMediaListType"
        Me.cbMediaListType.Size = New System.Drawing.Size(121, 21)
        Me.cbMediaListType.TabIndex = 12
        '
        'btnRemoveView
        '
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
        'txtViewQuery
        '
        Me.txtViewQuery.Location = New System.Drawing.Point(9, 90)
        Me.txtViewQuery.Multiline = True
        Me.txtViewQuery.Name = "txtViewQuery"
        Me.txtViewQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtViewQuery.Size = New System.Drawing.Size(584, 91)
        Me.txtViewQuery.TabIndex = 8
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
        'frmMediaListEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 366)
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
    Friend WithEvents txtViewQuery As System.Windows.Forms.TextBox
    Friend WithEvents gpMediaList As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_FilterType As System.Windows.Forms.Label
    Friend WithEvents cbMediaListType As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_FilterHelp As System.Windows.Forms.Label
    Friend WithEvents lbl_FilterURL As System.Windows.Forms.Label
    Friend WithEvents linklbl_FilterURL As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtViewName As System.Windows.Forms.TextBox

End Class
