<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrakt
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
        Me.txtTraktUsername = New System.Windows.Forms.TextBox()
        Me.lblTraktUsername = New System.Windows.Forms.Label()
        Me.btGetMoviesTrakt = New System.Windows.Forms.Button()
        Me.chkUseTrakt = New System.Windows.Forms.CheckBox()
        Me.pnlTrakt = New System.Windows.Forms.Panel()
        Me.btGetSeriesTrakt = New System.Windows.Forms.Button()
        Me.btnSaveTraktSettings = New System.Windows.Forms.Button()
        Me.lblTraktPassword = New System.Windows.Forms.Label()
        Me.txtTraktPassword = New System.Windows.Forms.TextBox()
        Me.lblstate = New System.Windows.Forms.Label()
        Me.prgtrakt = New System.Windows.Forms.ProgressBar()
        Me.btSaveMoviesTrakt = New System.Windows.Forms.Button()
        Me.dgvTraktWatched = New System.Windows.Forms.DataGridView()
        Me.col1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.col2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlTrakt.SuspendLayout()
        CType(Me.dgvTraktWatched, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTraktUsername
        '
        Me.txtTraktUsername.Enabled = False
        Me.txtTraktUsername.Location = New System.Drawing.Point(78, 51)
        Me.txtTraktUsername.Name = "txtTraktUsername"
        Me.txtTraktUsername.Size = New System.Drawing.Size(167, 20)
        Me.txtTraktUsername.TabIndex = 0
        '
        'lblTraktUsername
        '
        Me.lblTraktUsername.AutoSize = True
        Me.lblTraktUsername.Location = New System.Drawing.Point(19, 51)
        Me.lblTraktUsername.Name = "lblTraktUsername"
        Me.lblTraktUsername.Size = New System.Drawing.Size(55, 13)
        Me.lblTraktUsername.TabIndex = 1
        Me.lblTraktUsername.Text = "Username"
        '
        'btGetMoviesTrakt
        '
        Me.btGetMoviesTrakt.Enabled = False
        Me.btGetMoviesTrakt.Location = New System.Drawing.Point(22, 208)
        Me.btGetMoviesTrakt.Name = "btGetMoviesTrakt"
        Me.btGetMoviesTrakt.Size = New System.Drawing.Size(105, 66)
        Me.btGetMoviesTrakt.TabIndex = 4
        Me.btGetMoviesTrakt.Text = "Get watched movies"
        Me.btGetMoviesTrakt.UseVisualStyleBackColor = True
        '
        'chkUseTrakt
        '
        Me.chkUseTrakt.AutoSize = True
        Me.chkUseTrakt.Location = New System.Drawing.Point(22, 16)
        Me.chkUseTrakt.Name = "chkUseTrakt"
        Me.chkUseTrakt.Size = New System.Drawing.Size(85, 17)
        Me.chkUseTrakt.TabIndex = 6
        Me.chkUseTrakt.Text = "Use Trakt.tv"
        Me.chkUseTrakt.UseVisualStyleBackColor = True
        '
        'pnlTrakt
        '
        Me.pnlTrakt.Controls.Add(Me.btGetSeriesTrakt)
        Me.pnlTrakt.Controls.Add(Me.btnSaveTraktSettings)
        Me.pnlTrakt.Controls.Add(Me.lblTraktPassword)
        Me.pnlTrakt.Controls.Add(Me.txtTraktPassword)
        Me.pnlTrakt.Controls.Add(Me.lblstate)
        Me.pnlTrakt.Controls.Add(Me.prgtrakt)
        Me.pnlTrakt.Controls.Add(Me.btSaveMoviesTrakt)
        Me.pnlTrakt.Controls.Add(Me.dgvTraktWatched)
        Me.pnlTrakt.Controls.Add(Me.chkUseTrakt)
        Me.pnlTrakt.Controls.Add(Me.btGetMoviesTrakt)
        Me.pnlTrakt.Controls.Add(Me.lblTraktUsername)
        Me.pnlTrakt.Controls.Add(Me.txtTraktUsername)
        Me.pnlTrakt.Location = New System.Drawing.Point(0, 0)
        Me.pnlTrakt.Name = "pnlTrakt"
        Me.pnlTrakt.Size = New System.Drawing.Size(627, 367)
        Me.pnlTrakt.TabIndex = 0
        '
        'btGetSeriesTrakt
        '
        Me.btGetSeriesTrakt.Enabled = False
        Me.btGetSeriesTrakt.Location = New System.Drawing.Point(140, 208)
        Me.btGetSeriesTrakt.Name = "btGetSeriesTrakt"
        Me.btGetSeriesTrakt.Size = New System.Drawing.Size(105, 66)
        Me.btGetSeriesTrakt.TabIndex = 39
        Me.btGetSeriesTrakt.Text = "Get watched episodes"
        Me.btGetSeriesTrakt.UseVisualStyleBackColor = True
        '
        'btnSaveTraktSettings
        '
        Me.btnSaveTraktSettings.Location = New System.Drawing.Point(78, 104)
        Me.btnSaveTraktSettings.Name = "btnSaveTraktSettings"
        Me.btnSaveTraktSettings.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveTraktSettings.TabIndex = 38
        Me.btnSaveTraktSettings.Text = "Save "
        Me.btnSaveTraktSettings.UseVisualStyleBackColor = True
        '
        'lblTraktPassword
        '
        Me.lblTraktPassword.AutoSize = True
        Me.lblTraktPassword.Location = New System.Drawing.Point(19, 77)
        Me.lblTraktPassword.Name = "lblTraktPassword"
        Me.lblTraktPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblTraktPassword.TabIndex = 37
        Me.lblTraktPassword.Text = "Password"
        '
        'txtTraktPassword
        '
        Me.txtTraktPassword.Enabled = False
        Me.txtTraktPassword.Location = New System.Drawing.Point(78, 77)
        Me.txtTraktPassword.Name = "txtTraktPassword"
        Me.txtTraktPassword.Size = New System.Drawing.Size(167, 20)
        Me.txtTraktPassword.TabIndex = 36
        '
        'lblstate
        '
        Me.lblstate.AutoSize = True
        Me.lblstate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstate.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblstate.Location = New System.Drawing.Point(251, 309)
        Me.lblstate.Name = "lblstate"
        Me.lblstate.Size = New System.Drawing.Size(45, 15)
        Me.lblstate.TabIndex = 35
        Me.lblstate.Text = "Done!"
        Me.lblstate.Visible = False
        '
        'prgtrakt
        '
        Me.prgtrakt.Location = New System.Drawing.Point(22, 330)
        Me.prgtrakt.Name = "prgtrakt"
        Me.prgtrakt.Size = New System.Drawing.Size(274, 23)
        Me.prgtrakt.TabIndex = 34
        '
        'btSaveMoviesTrakt
        '
        Me.btSaveMoviesTrakt.Enabled = False
        Me.btSaveMoviesTrakt.Location = New System.Drawing.Point(22, 280)
        Me.btSaveMoviesTrakt.Name = "btSaveMoviesTrakt"
        Me.btSaveMoviesTrakt.Size = New System.Drawing.Size(223, 44)
        Me.btSaveMoviesTrakt.TabIndex = 33
        Me.btSaveMoviesTrakt.Text = "Save playcount to database/Nfo"
        Me.btSaveMoviesTrakt.UseVisualStyleBackColor = True
        '
        'dgvTraktWatched
        '
        Me.dgvTraktWatched.AllowUserToAddRows = False
        Me.dgvTraktWatched.AllowUserToDeleteRows = False
        Me.dgvTraktWatched.AllowUserToResizeColumns = False
        Me.dgvTraktWatched.AllowUserToResizeRows = False
        Me.dgvTraktWatched.BackgroundColor = System.Drawing.Color.White
        Me.dgvTraktWatched.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvTraktWatched.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col1, Me.col2})
        Me.dgvTraktWatched.Location = New System.Drawing.Point(302, 16)
        Me.dgvTraktWatched.MultiSelect = False
        Me.dgvTraktWatched.Name = "dgvTraktWatched"
        Me.dgvTraktWatched.RowHeadersVisible = False
        Me.dgvTraktWatched.RowHeadersWidth = 175
        Me.dgvTraktWatched.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvTraktWatched.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvTraktWatched.ShowCellErrors = False
        Me.dgvTraktWatched.ShowCellToolTips = False
        Me.dgvTraktWatched.ShowRowErrors = False
        Me.dgvTraktWatched.Size = New System.Drawing.Size(314, 338)
        Me.dgvTraktWatched.TabIndex = 32
        '
        'col1
        '
        Me.col1.Frozen = True
        Me.col1.HeaderText = "Title"
        Me.col1.Name = "col1"
        Me.col1.Width = 250
        '
        'col2
        '
        Me.col2.Frozen = True
        Me.col2.HeaderText = "Played"
        Me.col2.Name = "col2"
        Me.col2.Width = 64
        '
        'frmTrakt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 366)
        Me.Controls.Add(Me.pnlTrakt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTrakt"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmTrakt"
        Me.pnlTrakt.ResumeLayout(False)
        Me.pnlTrakt.PerformLayout()
        CType(Me.dgvTraktWatched, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTraktUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblTraktUsername As System.Windows.Forms.Label
    Friend WithEvents btGetMoviesTrakt As System.Windows.Forms.Button
    Friend WithEvents chkUseTrakt As System.Windows.Forms.CheckBox
    Friend WithEvents pnlTrakt As System.Windows.Forms.Panel
    Friend WithEvents dgvTraktWatched As System.Windows.Forms.DataGridView
    Friend WithEvents btSaveMoviesTrakt As System.Windows.Forms.Button
    Friend WithEvents prgtrakt As System.Windows.Forms.ProgressBar
    Friend WithEvents lblstate As System.Windows.Forms.Label
    Friend WithEvents col1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtTraktPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblTraktPassword As System.Windows.Forms.Label
    Friend WithEvents btnSaveTraktSettings As System.Windows.Forms.Button
    Friend WithEvents btGetSeriesTrakt As System.Windows.Forms.Button
End Class
