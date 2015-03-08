<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFilterEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFilterEditor))
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.linklbl_FilterURL = New System.Windows.Forms.LinkLabel()
        Me.lbl_FilterURL = New System.Windows.Forms.Label()
        Me.lbl_FilterHelp = New System.Windows.Forms.Label()
        Me.gpb_Filter = New System.Windows.Forms.GroupBox()
        Me.lbl_FilterType = New System.Windows.Forms.Label()
        Me.cbo_FilterType = New System.Windows.Forms.ComboBox()
        Me.lbl_FilterQuery = New System.Windows.Forms.Label()
        Me.btnRemoveFilter = New System.Windows.Forms.Button()
        Me.lbl_FilterName = New System.Windows.Forms.Label()
        Me.btnAddFilter = New System.Windows.Forms.Button()
        Me.txt_FilterQuery = New System.Windows.Forms.TextBox()
        Me.txt_FilterName = New System.Windows.Forms.TextBox()
        Me.cb_Filter = New System.Windows.Forms.ComboBox()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.pnlFilter.SuspendLayout()
        Me.gpb_Filter.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlFilter
        '
        Me.pnlFilter.Controls.Add(Me.linklbl_FilterURL)
        Me.pnlFilter.Controls.Add(Me.lbl_FilterURL)
        Me.pnlFilter.Controls.Add(Me.lbl_FilterHelp)
        Me.pnlFilter.Controls.Add(Me.gpb_Filter)
        Me.pnlFilter.Controls.Add(Me.cb_Filter)
        Me.pnlFilter.Controls.Add(Me.lbl_Filter)
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
        'gpb_Filter
        '
        Me.gpb_Filter.Controls.Add(Me.lbl_FilterType)
        Me.gpb_Filter.Controls.Add(Me.cbo_FilterType)
        Me.gpb_Filter.Controls.Add(Me.lbl_FilterQuery)
        Me.gpb_Filter.Controls.Add(Me.btnRemoveFilter)
        Me.gpb_Filter.Controls.Add(Me.lbl_FilterName)
        Me.gpb_Filter.Controls.Add(Me.btnAddFilter)
        Me.gpb_Filter.Controls.Add(Me.txt_FilterQuery)
        Me.gpb_Filter.Controls.Add(Me.txt_FilterName)
        Me.gpb_Filter.Location = New System.Drawing.Point(12, 63)
        Me.gpb_Filter.Name = "gpb_Filter"
        Me.gpb_Filter.Size = New System.Drawing.Size(599, 217)
        Me.gpb_Filter.TabIndex = 11
        Me.gpb_Filter.TabStop = False
        Me.gpb_Filter.Text = "Current Filter"
        '
        'lbl_FilterType
        '
        Me.lbl_FilterType.Location = New System.Drawing.Point(359, 25)
        Me.lbl_FilterType.Name = "lbl_FilterType"
        Me.lbl_FilterType.Size = New System.Drawing.Size(54, 13)
        Me.lbl_FilterType.TabIndex = 13
        Me.lbl_FilterType.Text = "Type"
        '
        'cbo_FilterType
        '
        Me.cbo_FilterType.FormattingEnabled = True
        Me.cbo_FilterType.Location = New System.Drawing.Point(362, 41)
        Me.cbo_FilterType.Name = "cbo_FilterType"
        Me.cbo_FilterType.Size = New System.Drawing.Size(121, 21)
        Me.cbo_FilterType.TabIndex = 12
        '
        'lbl_FilterQuery
        '
        Me.lbl_FilterQuery.Location = New System.Drawing.Point(6, 74)
        Me.lbl_FilterQuery.Name = "lbl_FilterQuery"
        Me.lbl_FilterQuery.Size = New System.Drawing.Size(61, 13)
        Me.lbl_FilterQuery.TabIndex = 11
        Me.lbl_FilterQuery.Text = "Query"
        '
        'btnRemoveFilter
        '
        Me.btnRemoveFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveFilter.Image = CType(resources.GetObject("btnRemoveFilter.Image"), System.Drawing.Image)
        Me.btnRemoveFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveFilter.Location = New System.Drawing.Point(90, 188)
        Me.btnRemoveFilter.Name = "btnRemoveFilter"
        Me.btnRemoveFilter.Size = New System.Drawing.Size(72, 23)
        Me.btnRemoveFilter.TabIndex = 4
        Me.btnRemoveFilter.Text = "Remove"
        Me.btnRemoveFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemoveFilter.UseVisualStyleBackColor = True
        '
        'lbl_FilterName
        '
        Me.lbl_FilterName.Location = New System.Drawing.Point(6, 27)
        Me.lbl_FilterName.Name = "lbl_FilterName"
        Me.lbl_FilterName.Size = New System.Drawing.Size(54, 13)
        Me.lbl_FilterName.TabIndex = 10
        Me.lbl_FilterName.Text = "Name"
        '
        'btnAddFilter
        '
        Me.btnAddFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddFilter.Image = CType(resources.GetObject("btnAddFilter.Image"), System.Drawing.Image)
        Me.btnAddFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddFilter.Location = New System.Drawing.Point(12, 188)
        Me.btnAddFilter.Name = "btnAddFilter"
        Me.btnAddFilter.Size = New System.Drawing.Size(72, 23)
        Me.btnAddFilter.TabIndex = 3
        Me.btnAddFilter.Text = "Add"
        Me.btnAddFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddFilter.UseVisualStyleBackColor = True
        '
        'txt_FilterQuery
        '
        Me.txt_FilterQuery.Location = New System.Drawing.Point(9, 90)
        Me.txt_FilterQuery.Multiline = True
        Me.txt_FilterQuery.Name = "txt_FilterQuery"
        Me.txt_FilterQuery.Size = New System.Drawing.Size(584, 91)
        Me.txt_FilterQuery.TabIndex = 8
        '
        'txt_FilterName
        '
        Me.txt_FilterName.Location = New System.Drawing.Point(9, 43)
        Me.txt_FilterName.Name = "txt_FilterName"
        Me.txt_FilterName.Size = New System.Drawing.Size(318, 20)
        Me.txt_FilterName.TabIndex = 9
        '
        'cb_Filter
        '
        Me.cb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_Filter.FormattingEnabled = True
        Me.cb_Filter.Location = New System.Drawing.Point(12, 36)
        Me.cb_Filter.Name = "cb_Filter"
        Me.cb_Filter.Size = New System.Drawing.Size(330, 21)
        Me.cb_Filter.TabIndex = 1
        '
        'lbl_Filter
        '
        Me.lbl_Filter.Location = New System.Drawing.Point(12, 20)
        Me.lbl_Filter.Name = "lbl_Filter"
        Me.lbl_Filter.Size = New System.Drawing.Size(87, 13)
        Me.lbl_Filter.TabIndex = 0
        Me.lbl_Filter.Text = "Filter"
        '
        'frmFilterEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 366)
        Me.Controls.Add(Me.pnlFilter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFilterEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmFilterEditor"
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        Me.gpb_Filter.ResumeLayout(False)
        Me.gpb_Filter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents cb_Filter As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemoveFilter As System.Windows.Forms.Button
    Friend WithEvents btnAddFilter As System.Windows.Forms.Button
    Friend WithEvents txt_FilterQuery As System.Windows.Forms.TextBox
    Friend WithEvents lbl_FilterName As System.Windows.Forms.Label
    Friend WithEvents txt_FilterName As System.Windows.Forms.TextBox
    Friend WithEvents gpb_Filter As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_FilterQuery As System.Windows.Forms.Label
    Friend WithEvents lbl_FilterType As System.Windows.Forms.Label
    Friend WithEvents cbo_FilterType As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_FilterHelp As System.Windows.Forms.Label
    Friend WithEvents lbl_FilterURL As System.Windows.Forms.Label
    Friend WithEvents linklbl_FilterURL As System.Windows.Forms.LinkLabel

End Class
