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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlGenres = New System.Windows.Forms.Panel()
        Me.btnMappingRemove = New System.Windows.Forms.Button()
        Me.btnMappingAdd = New System.Windows.Forms.Button()
        Me.btnGenreRemove = New System.Windows.Forms.Button()
        Me.btnGenreAdd = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnImageChange = New System.Windows.Forms.Button()
        Me.pbIcon = New System.Windows.Forms.PictureBox()
        Me.dgvMapping = New System.Windows.Forms.DataGridView()
        Me.dgvGenres = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.searchstring = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnGenresLoadFromDB = New System.Windows.Forms.Button()
        Me.pnlGenres.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pbIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvGenres, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlGenres
        '
        Me.pnlGenres.Controls.Add(Me.btnGenresLoadFromDB)
        Me.pnlGenres.Controls.Add(Me.btnMappingRemove)
        Me.pnlGenres.Controls.Add(Me.btnMappingAdd)
        Me.pnlGenres.Controls.Add(Me.btnGenreRemove)
        Me.pnlGenres.Controls.Add(Me.btnGenreAdd)
        Me.pnlGenres.Controls.Add(Me.GroupBox1)
        Me.pnlGenres.Controls.Add(Me.dgvMapping)
        Me.pnlGenres.Controls.Add(Me.dgvGenres)
        Me.pnlGenres.Location = New System.Drawing.Point(0, 0)
        Me.pnlGenres.Name = "pnlGenres"
        Me.pnlGenres.Size = New System.Drawing.Size(764, 439)
        Me.pnlGenres.TabIndex = 0
        '
        'btnRemoveGenre
        '
        Me.btnMappingRemove.Enabled = False
        Me.btnMappingRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMappingRemove.Image = CType(resources.GetObject("btnRemoveGenre.Image"), System.Drawing.Image)
        Me.btnMappingRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMappingRemove.Location = New System.Drawing.Point(114, 248)
        Me.btnMappingRemove.Name = "btnRemoveGenre"
        Me.btnMappingRemove.Size = New System.Drawing.Size(72, 23)
        Me.btnMappingRemove.TabIndex = 4
        Me.btnMappingRemove.Text = "Remove"
        Me.btnMappingRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMappingRemove.UseVisualStyleBackColor = True
        '
        'btnAddGenre
        '
        Me.btnMappingAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMappingAdd.Image = CType(resources.GetObject("btnAddGenre.Image"), System.Drawing.Image)
        Me.btnMappingAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMappingAdd.Location = New System.Drawing.Point(22, 248)
        Me.btnMappingAdd.Name = "btnAddGenre"
        Me.btnMappingAdd.Size = New System.Drawing.Size(72, 23)
        Me.btnMappingAdd.TabIndex = 3
        Me.btnMappingAdd.Text = "Add"
        Me.btnMappingAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMappingAdd.UseVisualStyleBackColor = True
        '
        'btnRemoveLang
        '
        Me.btnGenreRemove.Enabled = False
        Me.btnGenreRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenreRemove.Image = CType(resources.GetObject("btnRemoveLang.Image"), System.Drawing.Image)
        Me.btnGenreRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenreRemove.Location = New System.Drawing.Point(331, 248)
        Me.btnGenreRemove.Name = "btnRemoveLang"
        Me.btnGenreRemove.Size = New System.Drawing.Size(72, 23)
        Me.btnGenreRemove.TabIndex = 7
        Me.btnGenreRemove.Text = "Remove"
        Me.btnGenreRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenreRemove.UseVisualStyleBackColor = True
        '
        'btnAddLang
        '
        Me.btnGenreAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenreAdd.Image = CType(resources.GetObject("btnAddLang.Image"), System.Drawing.Image)
        Me.btnGenreAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenreAdd.Location = New System.Drawing.Point(239, 248)
        Me.btnGenreAdd.Name = "btnAddLang"
        Me.btnGenreAdd.Size = New System.Drawing.Size(72, 23)
        Me.btnGenreAdd.TabIndex = 6
        Me.btnGenreAdd.Text = "Add"
        Me.btnGenreAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenreAdd.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnImageChange)
        Me.GroupBox1.Controls.Add(Me.pbIcon)
        Me.GroupBox1.Location = New System.Drawing.Point(557, 52)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(180, 195)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Image"
        '
        'btnChangeImg
        '
        Me.btnImageChange.Enabled = False
        Me.btnImageChange.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImageChange.Image = Global.generic.EmberCore.GenresEditor.My.Resources.Resources.image
        Me.btnImageChange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImageChange.Location = New System.Drawing.Point(87, 19)
        Me.btnImageChange.Name = "btnChangeImg"
        Me.btnImageChange.Size = New System.Drawing.Size(81, 23)
        Me.btnImageChange.TabIndex = 0
        Me.btnImageChange.Text = "Change"
        Me.btnImageChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImageChange.UseVisualStyleBackColor = True
        '
        'pbIcon
        '
        Me.pbIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbIcon.Location = New System.Drawing.Point(8, 19)
        Me.pbIcon.Name = "pbIcon"
        Me.pbIcon.Size = New System.Drawing.Size(68, 102)
        Me.pbIcon.TabIndex = 6
        Me.pbIcon.TabStop = False
        '
        'dgvMapping
        '
        Me.dgvMapping.AllowUserToAddRows = False
        Me.dgvMapping.AllowUserToDeleteRows = False
        Me.dgvMapping.AllowUserToResizeColumns = False
        Me.dgvMapping.AllowUserToResizeRows = False
        Me.dgvMapping.BackgroundColor = System.Drawing.Color.White
        Me.dgvMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMapping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.searchstring})
        Me.dgvMapping.Location = New System.Drawing.Point(7, 52)
        Me.dgvMapping.MultiSelect = False
        Me.dgvMapping.Name = "dgvMapping"
        Me.dgvMapping.RowHeadersVisible = False
        Me.dgvMapping.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvMapping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvMapping.ShowCellErrors = False
        Me.dgvMapping.ShowCellToolTips = False
        Me.dgvMapping.ShowRowErrors = False
        Me.dgvMapping.Size = New System.Drawing.Size(202, 192)
        Me.dgvMapping.TabIndex = 2
        '
        'dgvGenres
        '
        Me.dgvGenres.AllowUserToAddRows = False
        Me.dgvGenres.AllowUserToDeleteRows = False
        Me.dgvGenres.AllowUserToResizeColumns = False
        Me.dgvGenres.AllowUserToResizeRows = False
        Me.dgvGenres.BackgroundColor = System.Drawing.Color.White
        Me.dgvGenres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGenres.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.DataGridViewTextBoxColumn1})
        Me.dgvGenres.Location = New System.Drawing.Point(239, 52)
        Me.dgvGenres.MultiSelect = False
        Me.dgvGenres.Name = "dgvGenres"
        Me.dgvGenres.RowHeadersVisible = False
        Me.dgvGenres.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvGenres.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGenres.ShowCellErrors = False
        Me.dgvGenres.ShowCellToolTips = False
        Me.dgvGenres.ShowEditingIcon = False
        Me.dgvGenres.ShowRowErrors = False
        Me.dgvGenres.Size = New System.Drawing.Size(164, 192)
        Me.dgvGenres.TabIndex = 5
        '
        'Column1
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.NullValue = False
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column1.FillWeight = 22.0!
        Me.Column1.HeaderText = ""
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 22
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn1.FillWeight = 120.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "Genres"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'searchstring
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.searchstring.DefaultCellStyle = DataGridViewCellStyle1
        Me.searchstring.FillWeight = 180.0!
        Me.searchstring.HeaderText = "Mapping"
        Me.searchstring.Name = "searchstring"
        Me.searchstring.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.searchstring.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.searchstring.Width = 180
        '
        'btnGenresLoadFromDB
        '
        Me.btnGenresLoadFromDB.Location = New System.Drawing.Point(239, 23)
        Me.btnGenresLoadFromDB.Name = "btnGenresLoadFromDB"
        Me.btnGenresLoadFromDB.Size = New System.Drawing.Size(164, 23)
        Me.btnGenresLoadFromDB.TabIndex = 9
        Me.btnGenresLoadFromDB.Text = "Load Genres from DB"
        Me.btnGenresLoadFromDB.UseVisualStyleBackColor = True
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 496)
        Me.Controls.Add(Me.pnlGenres)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmGenresEditor"
        Me.pnlGenres.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.pbIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvMapping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvGenres, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlGenres As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnImageChange As System.Windows.Forms.Button
    Friend WithEvents pbIcon As System.Windows.Forms.PictureBox
    Friend WithEvents dgvMapping As System.Windows.Forms.DataGridView
    Friend WithEvents dgvGenres As System.Windows.Forms.DataGridView
    Friend WithEvents btnMappingRemove As System.Windows.Forms.Button
    Friend WithEvents btnMappingAdd As System.Windows.Forms.Button
    Friend WithEvents btnGenreRemove As System.Windows.Forms.Button
    Friend WithEvents btnGenreAdd As System.Windows.Forms.Button
    Friend WithEvents btnGenresLoadFromDB As Windows.Forms.Button
    Friend WithEvents searchstring As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
End Class
