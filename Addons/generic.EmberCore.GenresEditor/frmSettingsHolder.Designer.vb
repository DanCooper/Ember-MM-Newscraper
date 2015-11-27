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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlGenres = New System.Windows.Forms.Panel()
        Me.btnGenreConfirmAll = New System.Windows.Forms.Button()
        Me.btnGenreConfirm = New System.Windows.Forms.Button()
        Me.btnMappingConfirmAll = New System.Windows.Forms.Button()
        Me.btnMappingConfirm = New System.Windows.Forms.Button()
        Me.btnGenreLoadFromDB = New System.Windows.Forms.Button()
        Me.btnMappingRemove = New System.Windows.Forms.Button()
        Me.btnMappingAdd = New System.Windows.Forms.Button()
        Me.btnGenreRemove = New System.Windows.Forms.Button()
        Me.btnGenreAdd = New System.Windows.Forms.Button()
        Me.cbMappingFilter = New System.Windows.Forms.ComboBox()
        Me.lblMappingFilter = New System.Windows.Forms.Label()
        Me.gbImage = New System.Windows.Forms.GroupBox()
        Me.btnImageChange = New System.Windows.Forms.Button()
        Me.pbImage = New System.Windows.Forms.PictureBox()
        Me.btnImageRemove = New System.Windows.Forms.Button()
        Me.dgvMappings = New System.Windows.Forms.DataGridView()
        Me.MappingSearchString = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvGenres = New System.Windows.Forms.DataGridView()
        Me.GenreEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GenreName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnGenreCleanupDB = New System.Windows.Forms.Button()
        Me.pnlGenres.SuspendLayout()
        Me.gbImage.SuspendLayout()
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvMappings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvGenres, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlGenres
        '
        Me.pnlGenres.Controls.Add(Me.btnGenreCleanupDB)
        Me.pnlGenres.Controls.Add(Me.btnGenreConfirmAll)
        Me.pnlGenres.Controls.Add(Me.btnGenreConfirm)
        Me.pnlGenres.Controls.Add(Me.btnMappingConfirmAll)
        Me.pnlGenres.Controls.Add(Me.btnMappingConfirm)
        Me.pnlGenres.Controls.Add(Me.btnGenreLoadFromDB)
        Me.pnlGenres.Controls.Add(Me.btnMappingRemove)
        Me.pnlGenres.Controls.Add(Me.btnMappingAdd)
        Me.pnlGenres.Controls.Add(Me.btnGenreRemove)
        Me.pnlGenres.Controls.Add(Me.btnGenreAdd)
        Me.pnlGenres.Controls.Add(Me.cbMappingFilter)
        Me.pnlGenres.Controls.Add(Me.lblMappingFilter)
        Me.pnlGenres.Controls.Add(Me.gbImage)
        Me.pnlGenres.Controls.Add(Me.dgvMappings)
        Me.pnlGenres.Controls.Add(Me.dgvGenres)
        Me.pnlGenres.Location = New System.Drawing.Point(0, 0)
        Me.pnlGenres.Name = "pnlGenres"
        Me.pnlGenres.Size = New System.Drawing.Size(583, 422)
        Me.pnlGenres.TabIndex = 0
        '
        'btnGenreConfirmAll
        '
        Me.btnGenreConfirmAll.Location = New System.Drawing.Point(338, 298)
        Me.btnGenreConfirmAll.Name = "btnGenreConfirmAll"
        Me.btnGenreConfirmAll.Size = New System.Drawing.Size(100, 23)
        Me.btnGenreConfirmAll.TabIndex = 12
        Me.btnGenreConfirmAll.Text = "Confirm All"
        Me.btnGenreConfirmAll.UseVisualStyleBackColor = True
        '
        'btnGenreConfirm
        '
        Me.btnGenreConfirm.Enabled = False
        Me.btnGenreConfirm.Location = New System.Drawing.Point(239, 298)
        Me.btnGenreConfirm.Name = "btnGenreConfirm"
        Me.btnGenreConfirm.Size = New System.Drawing.Size(93, 23)
        Me.btnGenreConfirm.TabIndex = 11
        Me.btnGenreConfirm.Text = "Confirm"
        Me.btnGenreConfirm.UseVisualStyleBackColor = True
        '
        'btnMappingConfirmAll
        '
        Me.btnMappingConfirmAll.Location = New System.Drawing.Point(107, 298)
        Me.btnMappingConfirmAll.Name = "btnMappingConfirmAll"
        Me.btnMappingConfirmAll.Size = New System.Drawing.Size(100, 23)
        Me.btnMappingConfirmAll.TabIndex = 10
        Me.btnMappingConfirmAll.Text = "Confirm All"
        Me.btnMappingConfirmAll.UseVisualStyleBackColor = True
        '
        'btnMappingConfirm
        '
        Me.btnMappingConfirm.Enabled = False
        Me.btnMappingConfirm.Location = New System.Drawing.Point(8, 298)
        Me.btnMappingConfirm.Name = "btnMappingConfirm"
        Me.btnMappingConfirm.Size = New System.Drawing.Size(93, 23)
        Me.btnMappingConfirm.TabIndex = 10
        Me.btnMappingConfirm.Text = "Confirm"
        Me.btnMappingConfirm.UseVisualStyleBackColor = True
        '
        'btnGenreLoadFromDB
        '
        Me.btnGenreLoadFromDB.Location = New System.Drawing.Point(239, 23)
        Me.btnGenreLoadFromDB.Name = "btnGenreLoadFromDB"
        Me.btnGenreLoadFromDB.Size = New System.Drawing.Size(200, 23)
        Me.btnGenreLoadFromDB.TabIndex = 9
        Me.btnGenreLoadFromDB.Text = "Load Genres from Database"
        Me.btnGenreLoadFromDB.UseVisualStyleBackColor = True
        '
        'btnMappingRemove
        '
        Me.btnMappingRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMappingRemove.Image = CType(resources.GetObject("btnMappingRemove.Image"), System.Drawing.Image)
        Me.btnMappingRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMappingRemove.Location = New System.Drawing.Point(107, 327)
        Me.btnMappingRemove.Name = "btnMappingRemove"
        Me.btnMappingRemove.Size = New System.Drawing.Size(100, 23)
        Me.btnMappingRemove.TabIndex = 4
        Me.btnMappingRemove.Text = "Remove"
        Me.btnMappingRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMappingRemove.UseVisualStyleBackColor = True
        '
        'btnMappingAdd
        '
        Me.btnMappingAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMappingAdd.Image = CType(resources.GetObject("btnMappingAdd.Image"), System.Drawing.Image)
        Me.btnMappingAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMappingAdd.Location = New System.Drawing.Point(8, 327)
        Me.btnMappingAdd.Name = "btnMappingAdd"
        Me.btnMappingAdd.Size = New System.Drawing.Size(93, 23)
        Me.btnMappingAdd.TabIndex = 3
        Me.btnMappingAdd.Text = "Add"
        Me.btnMappingAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMappingAdd.UseVisualStyleBackColor = True
        '
        'btnGenreRemove
        '
        Me.btnGenreRemove.Enabled = False
        Me.btnGenreRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenreRemove.Image = CType(resources.GetObject("btnGenreRemove.Image"), System.Drawing.Image)
        Me.btnGenreRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenreRemove.Location = New System.Drawing.Point(338, 327)
        Me.btnGenreRemove.Name = "btnGenreRemove"
        Me.btnGenreRemove.Size = New System.Drawing.Size(100, 23)
        Me.btnGenreRemove.TabIndex = 7
        Me.btnGenreRemove.Text = "Remove"
        Me.btnGenreRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenreRemove.UseVisualStyleBackColor = True
        '
        'btnGenreAdd
        '
        Me.btnGenreAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenreAdd.Image = CType(resources.GetObject("btnGenreAdd.Image"), System.Drawing.Image)
        Me.btnGenreAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenreAdd.Location = New System.Drawing.Point(239, 327)
        Me.btnGenreAdd.Name = "btnGenreAdd"
        Me.btnGenreAdd.Size = New System.Drawing.Size(93, 23)
        Me.btnGenreAdd.TabIndex = 6
        Me.btnGenreAdd.Text = "Add"
        Me.btnGenreAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenreAdd.UseVisualStyleBackColor = True
        '
        'cbMappingFilter
        '
        Me.cbMappingFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMappingFilter.FormattingEnabled = True
        Me.cbMappingFilter.Location = New System.Drawing.Point(8, 25)
        Me.cbMappingFilter.Name = "cbMappingFilter"
        Me.cbMappingFilter.Size = New System.Drawing.Size(201, 21)
        Me.cbMappingFilter.TabIndex = 1
        '
        'lblMappingFilter
        '
        Me.lblMappingFilter.AutoSize = True
        Me.lblMappingFilter.Location = New System.Drawing.Point(9, 9)
        Me.lblMappingFilter.Name = "lblMappingFilter"
        Me.lblMappingFilter.Size = New System.Drawing.Size(29, 13)
        Me.lblMappingFilter.TabIndex = 0
        Me.lblMappingFilter.Text = "Filter"
        '
        'gbImage
        '
        Me.gbImage.Controls.Add(Me.btnImageChange)
        Me.gbImage.Controls.Add(Me.pbImage)
        Me.gbImage.Controls.Add(Me.btnImageRemove)
        Me.gbImage.Location = New System.Drawing.Point(463, 78)
        Me.gbImage.Name = "gbImage"
        Me.gbImage.Size = New System.Drawing.Size(97, 191)
        Me.gbImage.TabIndex = 8
        Me.gbImage.TabStop = False
        Me.gbImage.Text = "Image"
        '
        'btnImageChange
        '
        Me.btnImageChange.Enabled = False
        Me.btnImageChange.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImageChange.Image = Global.generic.EmberCore.GenresEditor.My.Resources.Resources.image
        Me.btnImageChange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImageChange.Location = New System.Drawing.Point(6, 125)
        Me.btnImageChange.Name = "btnImageChange"
        Me.btnImageChange.Size = New System.Drawing.Size(81, 23)
        Me.btnImageChange.TabIndex = 0
        Me.btnImageChange.Text = "Change"
        Me.btnImageChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImageChange.UseVisualStyleBackColor = True
        '
        'pbImage
        '
        Me.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbImage.Location = New System.Drawing.Point(13, 19)
        Me.pbImage.Name = "pbImage"
        Me.pbImage.Size = New System.Drawing.Size(68, 100)
        Me.pbImage.TabIndex = 6
        Me.pbImage.TabStop = False
        '
        'btnImageRemove
        '
        Me.btnImageRemove.Enabled = False
        Me.btnImageRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImageRemove.Image = CType(resources.GetObject("btnImageRemove.Image"), System.Drawing.Image)
        Me.btnImageRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImageRemove.Location = New System.Drawing.Point(6, 154)
        Me.btnImageRemove.Name = "btnImageRemove"
        Me.btnImageRemove.Size = New System.Drawing.Size(81, 23)
        Me.btnImageRemove.TabIndex = 7
        Me.btnImageRemove.Text = "Remove"
        Me.btnImageRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImageRemove.UseVisualStyleBackColor = True
        '
        'dgvMappings
        '
        Me.dgvMappings.AllowUserToAddRows = False
        Me.dgvMappings.AllowUserToDeleteRows = False
        Me.dgvMappings.AllowUserToResizeColumns = False
        Me.dgvMappings.AllowUserToResizeRows = False
        Me.dgvMappings.BackgroundColor = System.Drawing.Color.White
        Me.dgvMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMappings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MappingSearchString})
        Me.dgvMappings.Location = New System.Drawing.Point(7, 52)
        Me.dgvMappings.MultiSelect = False
        Me.dgvMappings.Name = "dgvMappings"
        Me.dgvMappings.RowHeadersVisible = False
        Me.dgvMappings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvMappings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMappings.ShowCellErrors = False
        Me.dgvMappings.ShowCellToolTips = False
        Me.dgvMappings.ShowEditingIcon = False
        Me.dgvMappings.ShowRowErrors = False
        Me.dgvMappings.Size = New System.Drawing.Size(200, 240)
        Me.dgvMappings.TabIndex = 2
        '
        'MappingSearchString
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.MappingSearchString.DefaultCellStyle = DataGridViewCellStyle4
        Me.MappingSearchString.FillWeight = 180.0!
        Me.MappingSearchString.HeaderText = "Mapping"
        Me.MappingSearchString.Name = "MappingSearchString"
        Me.MappingSearchString.ReadOnly = True
        Me.MappingSearchString.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MappingSearchString.Width = 180
        '
        'dgvGenres
        '
        Me.dgvGenres.AllowUserToAddRows = False
        Me.dgvGenres.AllowUserToDeleteRows = False
        Me.dgvGenres.AllowUserToResizeColumns = False
        Me.dgvGenres.AllowUserToResizeRows = False
        Me.dgvGenres.BackgroundColor = System.Drawing.Color.White
        Me.dgvGenres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGenres.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GenreEnabled, Me.GenreName})
        Me.dgvGenres.Location = New System.Drawing.Point(239, 52)
        Me.dgvGenres.MultiSelect = False
        Me.dgvGenres.Name = "dgvGenres"
        Me.dgvGenres.RowHeadersVisible = False
        Me.dgvGenres.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvGenres.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGenres.ShowCellErrors = False
        Me.dgvGenres.ShowCellToolTips = False
        Me.dgvGenres.ShowRowErrors = False
        Me.dgvGenres.Size = New System.Drawing.Size(200, 240)
        Me.dgvGenres.TabIndex = 5
        '
        'GenreEnabled
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.NullValue = False
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.GenreEnabled.DefaultCellStyle = DataGridViewCellStyle5
        Me.GenreEnabled.FillWeight = 22.0!
        Me.GenreEnabled.HeaderText = ""
        Me.GenreEnabled.Name = "GenreEnabled"
        Me.GenreEnabled.Width = 22
        '
        'GenreName
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.GenreName.DefaultCellStyle = DataGridViewCellStyle6
        Me.GenreName.FillWeight = 158.0!
        Me.GenreName.HeaderText = "Genres"
        Me.GenreName.Name = "GenreName"
        Me.GenreName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GenreName.Width = 158
        '
        'btnGenreCleanupDB
        '
        Me.btnGenreCleanupDB.Location = New System.Drawing.Point(239, 374)
        Me.btnGenreCleanupDB.Name = "btnGenreCleanupDB"
        Me.btnGenreCleanupDB.Size = New System.Drawing.Size(199, 23)
        Me.btnGenreCleanupDB.TabIndex = 13
        Me.btnGenreCleanupDB.Text = "Cleanup Database"
        Me.btnGenreCleanupDB.UseVisualStyleBackColor = True
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 427)
        Me.Controls.Add(Me.pnlGenres)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmGenresEditor"
        Me.pnlGenres.ResumeLayout(False)
        Me.pnlGenres.PerformLayout()
        Me.gbImage.ResumeLayout(False)
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvMappings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvGenres, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlGenres As System.Windows.Forms.Panel
    Friend WithEvents gbImage As System.Windows.Forms.GroupBox
    Friend WithEvents btnImageChange As System.Windows.Forms.Button
    Friend WithEvents pbImage As System.Windows.Forms.PictureBox
    Friend WithEvents dgvMappings As System.Windows.Forms.DataGridView
    Friend WithEvents dgvGenres As System.Windows.Forms.DataGridView
    Friend WithEvents lblMappingFilter As System.Windows.Forms.Label
    Friend WithEvents cbMappingFilter As System.Windows.Forms.ComboBox
    Friend WithEvents btnMappingRemove As System.Windows.Forms.Button
    Friend WithEvents btnMappingAdd As System.Windows.Forms.Button
    Friend WithEvents btnGenreRemove As System.Windows.Forms.Button
    Friend WithEvents btnGenreAdd As System.Windows.Forms.Button
    Friend WithEvents btnGenreLoadFromDB As Windows.Forms.Button
    Friend WithEvents btnImageRemove As Windows.Forms.Button
    Friend WithEvents MappingSearchString As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GenreEnabled As Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents GenreName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnGenreConfirmAll As Windows.Forms.Button
    Friend WithEvents btnGenreConfirm As Windows.Forms.Button
    Friend WithEvents btnMappingConfirmAll As Windows.Forms.Button
    Friend WithEvents btnMappingConfirm As Windows.Forms.Button
    Friend WithEvents btnGenreCleanupDB As Windows.Forms.Button
End Class
