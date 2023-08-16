<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovieset_Information
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTitleMapping = New System.Windows.Forms.GroupBox()
        Me.tblTitleMapping = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvTitleMapping = New System.Windows.Forms.DataGridView()
        Me.gbScraperFields = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetScraperGlobalOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkPlotLocked = New System.Windows.Forms.CheckBox()
        Me.chkPlotEnabled = New System.Windows.Forms.CheckBox()
        Me.chkTitleLocked = New System.Windows.Forms.CheckBox()
        Me.chkTitleEnabled = New System.Windows.Forms.CheckBox()
        Me.lblScraperFieldsHeaderLocked = New System.Windows.Forms.Label()
        Me.colInput = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMapping = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbTitleMapping.SuspendLayout()
        Me.tblTitleMapping.SuspendLayout()
        CType(Me.dgvTitleMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbScraperFields.SuspendLayout()
        Me.tblMovieSetScraperGlobalOpts.SuspendLayout()
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
        Me.pnlSettings.TabIndex = 27
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
        Me.tblSettings.Controls.Add(Me.gbTitleMapping, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbScraperFields, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 450)
        Me.tblSettings.TabIndex = 70
        '
        'gbTitleMapping
        '
        Me.gbTitleMapping.AutoSize = True
        Me.gbTitleMapping.Controls.Add(Me.tblTitleMapping)
        Me.gbTitleMapping.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTitleMapping.Location = New System.Drawing.Point(169, 3)
        Me.gbTitleMapping.Name = "gbTitleMapping"
        Me.gbTitleMapping.Size = New System.Drawing.Size(412, 183)
        Me.gbTitleMapping.TabIndex = 69
        Me.gbTitleMapping.TabStop = False
        Me.gbTitleMapping.Text = "Title Mapping"
        '
        'tblTitleMapping
        '
        Me.tblTitleMapping.AutoSize = True
        Me.tblTitleMapping.ColumnCount = 1
        Me.tblTitleMapping.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTitleMapping.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTitleMapping.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTitleMapping.Controls.Add(Me.dgvTitleMapping, 0, 0)
        Me.tblTitleMapping.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTitleMapping.Location = New System.Drawing.Point(3, 18)
        Me.tblTitleMapping.Name = "tblTitleMapping"
        Me.tblTitleMapping.RowCount = 1
        Me.tblTitleMapping.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTitleMapping.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTitleMapping.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTitleMapping.Size = New System.Drawing.Size(406, 162)
        Me.tblTitleMapping.TabIndex = 70
        '
        'dgvTitleMapping
        '
        Me.dgvTitleMapping.AllowUserToResizeRows = False
        Me.dgvTitleMapping.BackgroundColor = System.Drawing.Color.White
        Me.dgvTitleMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTitleMapping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colInput, Me.colMapping})
        Me.dgvTitleMapping.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTitleMapping.Location = New System.Drawing.Point(3, 3)
        Me.dgvTitleMapping.MultiSelect = False
        Me.dgvTitleMapping.Name = "dgvTitleMapping"
        Me.dgvTitleMapping.RowHeadersVisible = False
        Me.dgvTitleMapping.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvTitleMapping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvTitleMapping.ShowCellErrors = False
        Me.dgvTitleMapping.ShowCellToolTips = False
        Me.dgvTitleMapping.ShowRowErrors = False
        Me.dgvTitleMapping.Size = New System.Drawing.Size(400, 156)
        Me.dgvTitleMapping.TabIndex = 68
        '
        'gbScraperFields
        '
        Me.gbScraperFields.AutoSize = True
        Me.gbScraperFields.Controls.Add(Me.tblMovieSetScraperGlobalOpts)
        Me.gbScraperFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFields.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperFields.MinimumSize = New System.Drawing.Size(160, 0)
        Me.gbScraperFields.Name = "gbScraperFields"
        Me.gbScraperFields.Size = New System.Drawing.Size(160, 183)
        Me.gbScraperFields.TabIndex = 67
        Me.gbScraperFields.TabStop = False
        Me.gbScraperFields.Text = "Scraper Fields - Global"
        '
        'tblMovieSetScraperGlobalOpts
        '
        Me.tblMovieSetScraperGlobalOpts.AutoScroll = True
        Me.tblMovieSetScraperGlobalOpts.AutoSize = True
        Me.tblMovieSetScraperGlobalOpts.ColumnCount = 3
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkPlotLocked, 1, 2)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkPlotEnabled, 0, 2)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkTitleLocked, 1, 1)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkTitleEnabled, 0, 1)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.lblScraperFieldsHeaderLocked, 1, 0)
        Me.tblMovieSetScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetScraperGlobalOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetScraperGlobalOpts.Name = "tblMovieSetScraperGlobalOpts"
        Me.tblMovieSetScraperGlobalOpts.RowCount = 4
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.Size = New System.Drawing.Size(154, 162)
        Me.tblMovieSetScraperGlobalOpts.TabIndex = 1
        '
        'chkPlotLocked
        '
        Me.chkPlotLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPlotLocked.AutoSize = True
        Me.chkPlotLocked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlotLocked.Location = New System.Drawing.Point(71, 47)
        Me.chkPlotLocked.Name = "chkPlotLocked"
        Me.chkPlotLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkPlotLocked.TabIndex = 0
        Me.chkPlotLocked.UseVisualStyleBackColor = True
        '
        'chkPlotEnabled
        '
        Me.chkPlotEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPlotEnabled.AutoSize = True
        Me.chkPlotEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlotEnabled.Location = New System.Drawing.Point(3, 46)
        Me.chkPlotEnabled.Name = "chkPlotEnabled"
        Me.chkPlotEnabled.Size = New System.Drawing.Size(46, 17)
        Me.chkPlotEnabled.TabIndex = 12
        Me.chkPlotEnabled.Text = "Plot"
        Me.chkPlotEnabled.UseVisualStyleBackColor = True
        '
        'chkTitleLocked
        '
        Me.chkTitleLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTitleLocked.AutoSize = True
        Me.chkTitleLocked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitleLocked.Location = New System.Drawing.Point(71, 24)
        Me.chkTitleLocked.Name = "chkTitleLocked"
        Me.chkTitleLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkTitleLocked.TabIndex = 2
        Me.chkTitleLocked.UseVisualStyleBackColor = True
        '
        'chkTitleEnabled
        '
        Me.chkTitleEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleEnabled.AutoSize = True
        Me.chkTitleEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTitleEnabled.Location = New System.Drawing.Point(3, 23)
        Me.chkTitleEnabled.Name = "chkTitleEnabled"
        Me.chkTitleEnabled.Size = New System.Drawing.Size(48, 17)
        Me.chkTitleEnabled.TabIndex = 0
        Me.chkTitleEnabled.Text = "Title"
        Me.chkTitleEnabled.UseVisualStyleBackColor = True
        '
        'lblScraperFieldsHeaderLocked
        '
        Me.lblScraperFieldsHeaderLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblScraperFieldsHeaderLocked.AutoSize = True
        Me.lblScraperFieldsHeaderLocked.Location = New System.Drawing.Point(57, 3)
        Me.lblScraperFieldsHeaderLocked.Name = "lblScraperFieldsHeaderLocked"
        Me.lblScraperFieldsHeaderLocked.Size = New System.Drawing.Size(43, 13)
        Me.lblScraperFieldsHeaderLocked.TabIndex = 12
        Me.lblScraperFieldsHeaderLocked.Text = "Locked"
        '
        'colInput
        '
        Me.colInput.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colInput.HeaderText = "Input"
        Me.colInput.Name = "colInput"
        Me.colInput.Width = 60
        '
        'colMapping
        '
        Me.colMapping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMapping.HeaderText = "Mapped To"
        Me.colMapping.Name = "colMapping"
        '
        'frmMovieset_Information
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmMovieset_Information"
        Me.Text = "frmMovieSet_Data"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbTitleMapping.ResumeLayout(False)
        Me.gbTitleMapping.PerformLayout()
        Me.tblTitleMapping.ResumeLayout(False)
        CType(Me.dgvTitleMapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbScraperFields.ResumeLayout(False)
        Me.gbScraperFields.PerformLayout()
        Me.tblMovieSetScraperGlobalOpts.ResumeLayout(False)
        Me.tblMovieSetScraperGlobalOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbTitleMapping As GroupBox
    Friend WithEvents tblTitleMapping As TableLayoutPanel
    Friend WithEvents dgvTitleMapping As DataGridView
    Friend WithEvents gbScraperFields As GroupBox
    Friend WithEvents tblMovieSetScraperGlobalOpts As TableLayoutPanel
    Friend WithEvents chkPlotLocked As CheckBox
    Friend WithEvents chkPlotEnabled As CheckBox
    Friend WithEvents chkTitleLocked As CheckBox
    Friend WithEvents chkTitleEnabled As CheckBox
    Friend WithEvents lblScraperFieldsHeaderLocked As Label
    Friend WithEvents colInput As DataGridViewTextBoxColumn
    Friend WithEvents colMapping As DataGridViewTextBoxColumn
End Class
