<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMovieset_Data
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovieset_Data))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieSetScraperTitleRenamerOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetScraperTitleRenamerOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieSetScraperTitleRenamerAdd = New System.Windows.Forms.Button()
        Me.btnMovieSetScraperTitleRenamerRemove = New System.Windows.Forms.Button()
        Me.dgvMovieSetScraperTitleRenamer = New System.Windows.Forms.DataGridView()
        Me.tbcMovieSetScrapedTitleRenamerFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tbcMovieSetScrapedTitleRenamerTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbMovieSetScraperGlobalOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieSetScraperGlobalOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieSetLockPlot = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetScraperPlot = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetLockTitle = New System.Windows.Forms.CheckBox()
        Me.chkMovieSetScraperTitle = New System.Windows.Forms.CheckBox()
        Me.lblMovieSetScraperGlobalHeaderLock = New System.Windows.Forms.Label()
        Me.lblMovieSetScraperGlobalTitle = New System.Windows.Forms.Label()
        Me.lblMovieSetScraperGlobalPlot = New System.Windows.Forms.Label()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMovieSetScraperTitleRenamerOpts.SuspendLayout()
        Me.tblMovieSetScraperTitleRenamerOpts.SuspendLayout()
        CType(Me.dgvMovieSetScraperTitleRenamer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMovieSetScraperGlobalOpts.SuspendLayout()
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
        Me.tblSettings.Controls.Add(Me.gbMovieSetScraperTitleRenamerOpts, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbMovieSetScraperGlobalOpts, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 450)
        Me.tblSettings.TabIndex = 70
        '
        'gbMovieSetScraperTitleRenamerOpts
        '
        Me.gbMovieSetScraperTitleRenamerOpts.AutoSize = True
        Me.gbMovieSetScraperTitleRenamerOpts.Controls.Add(Me.tblMovieSetScraperTitleRenamerOpts)
        Me.gbMovieSetScraperTitleRenamerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetScraperTitleRenamerOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbMovieSetScraperTitleRenamerOpts.Location = New System.Drawing.Point(169, 3)
        Me.gbMovieSetScraperTitleRenamerOpts.Name = "gbMovieSetScraperTitleRenamerOpts"
        Me.gbMovieSetScraperTitleRenamerOpts.Size = New System.Drawing.Size(314, 212)
        Me.gbMovieSetScraperTitleRenamerOpts.TabIndex = 69
        Me.gbMovieSetScraperTitleRenamerOpts.TabStop = False
        Me.gbMovieSetScraperTitleRenamerOpts.Text = "Title Renamer"
        '
        'tblMovieSetScraperTitleRenamerOpts
        '
        Me.tblMovieSetScraperTitleRenamerOpts.AutoSize = True
        Me.tblMovieSetScraperTitleRenamerOpts.ColumnCount = 3
        Me.tblMovieSetScraperTitleRenamerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.Controls.Add(Me.btnMovieSetScraperTitleRenamerAdd, 0, 1)
        Me.tblMovieSetScraperTitleRenamerOpts.Controls.Add(Me.btnMovieSetScraperTitleRenamerRemove, 1, 1)
        Me.tblMovieSetScraperTitleRenamerOpts.Controls.Add(Me.dgvMovieSetScraperTitleRenamer, 0, 0)
        Me.tblMovieSetScraperTitleRenamerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetScraperTitleRenamerOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetScraperTitleRenamerOpts.Name = "tblMovieSetScraperTitleRenamerOpts"
        Me.tblMovieSetScraperTitleRenamerOpts.RowCount = 3
        Me.tblMovieSetScraperTitleRenamerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperTitleRenamerOpts.Size = New System.Drawing.Size(308, 191)
        Me.tblMovieSetScraperTitleRenamerOpts.TabIndex = 70
        '
        'btnMovieSetScraperTitleRenamerAdd
        '
        Me.btnMovieSetScraperTitleRenamerAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMovieSetScraperTitleRenamerAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMovieSetScraperTitleRenamerAdd.Image = CType(resources.GetObject("btnMovieSetScraperTitleRenamerAdd.Image"), System.Drawing.Image)
        Me.btnMovieSetScraperTitleRenamerAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSetScraperTitleRenamerAdd.Location = New System.Drawing.Point(3, 165)
        Me.btnMovieSetScraperTitleRenamerAdd.Name = "btnMovieSetScraperTitleRenamerAdd"
        Me.btnMovieSetScraperTitleRenamerAdd.Size = New System.Drawing.Size(87, 23)
        Me.btnMovieSetScraperTitleRenamerAdd.TabIndex = 69
        Me.btnMovieSetScraperTitleRenamerAdd.Text = "Add"
        Me.btnMovieSetScraperTitleRenamerAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSetScraperTitleRenamerAdd.UseVisualStyleBackColor = True
        '
        'btnMovieSetScraperTitleRenamerRemove
        '
        Me.btnMovieSetScraperTitleRenamerRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMovieSetScraperTitleRenamerRemove.Enabled = False
        Me.btnMovieSetScraperTitleRenamerRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMovieSetScraperTitleRenamerRemove.Image = CType(resources.GetObject("btnMovieSetScraperTitleRenamerRemove.Image"), System.Drawing.Image)
        Me.btnMovieSetScraperTitleRenamerRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieSetScraperTitleRenamerRemove.Location = New System.Drawing.Point(218, 165)
        Me.btnMovieSetScraperTitleRenamerRemove.Name = "btnMovieSetScraperTitleRenamerRemove"
        Me.btnMovieSetScraperTitleRenamerRemove.Size = New System.Drawing.Size(87, 23)
        Me.btnMovieSetScraperTitleRenamerRemove.TabIndex = 70
        Me.btnMovieSetScraperTitleRenamerRemove.Text = "Remove"
        Me.btnMovieSetScraperTitleRenamerRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieSetScraperTitleRenamerRemove.UseVisualStyleBackColor = True
        '
        'dgvMovieSetScraperTitleRenamer
        '
        Me.dgvMovieSetScraperTitleRenamer.AllowUserToAddRows = False
        Me.dgvMovieSetScraperTitleRenamer.AllowUserToDeleteRows = False
        Me.dgvMovieSetScraperTitleRenamer.AllowUserToResizeColumns = False
        Me.dgvMovieSetScraperTitleRenamer.AllowUserToResizeRows = False
        Me.dgvMovieSetScraperTitleRenamer.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovieSetScraperTitleRenamer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovieSetScraperTitleRenamer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.tbcMovieSetScrapedTitleRenamerFrom, Me.tbcMovieSetScrapedTitleRenamerTo})
        Me.tblMovieSetScraperTitleRenamerOpts.SetColumnSpan(Me.dgvMovieSetScraperTitleRenamer, 2)
        Me.dgvMovieSetScraperTitleRenamer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovieSetScraperTitleRenamer.Location = New System.Drawing.Point(3, 3)
        Me.dgvMovieSetScraperTitleRenamer.MultiSelect = False
        Me.dgvMovieSetScraperTitleRenamer.Name = "dgvMovieSetScraperTitleRenamer"
        Me.dgvMovieSetScraperTitleRenamer.RowHeadersVisible = False
        Me.dgvMovieSetScraperTitleRenamer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvMovieSetScraperTitleRenamer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvMovieSetScraperTitleRenamer.ShowCellErrors = False
        Me.dgvMovieSetScraperTitleRenamer.ShowCellToolTips = False
        Me.dgvMovieSetScraperTitleRenamer.ShowRowErrors = False
        Me.dgvMovieSetScraperTitleRenamer.Size = New System.Drawing.Size(302, 156)
        Me.dgvMovieSetScraperTitleRenamer.TabIndex = 68
        '
        'tbcMovieSetScrapedTitleRenamerFrom
        '
        Me.tbcMovieSetScrapedTitleRenamerFrom.FillWeight = 130.0!
        Me.tbcMovieSetScrapedTitleRenamerFrom.HeaderText = "From"
        Me.tbcMovieSetScrapedTitleRenamerFrom.Name = "tbcMovieSetScrapedTitleRenamerFrom"
        Me.tbcMovieSetScrapedTitleRenamerFrom.Width = 130
        '
        'tbcMovieSetScrapedTitleRenamerTo
        '
        Me.tbcMovieSetScrapedTitleRenamerTo.FillWeight = 150.0!
        Me.tbcMovieSetScrapedTitleRenamerTo.HeaderText = "To"
        Me.tbcMovieSetScrapedTitleRenamerTo.Name = "tbcMovieSetScrapedTitleRenamerTo"
        Me.tbcMovieSetScrapedTitleRenamerTo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.tbcMovieSetScrapedTitleRenamerTo.Width = 150
        '
        'gbMovieSetScraperGlobalOpts
        '
        Me.gbMovieSetScraperGlobalOpts.AutoSize = True
        Me.gbMovieSetScraperGlobalOpts.Controls.Add(Me.tblMovieSetScraperGlobalOpts)
        Me.gbMovieSetScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieSetScraperGlobalOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieSetScraperGlobalOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieSetScraperGlobalOpts.MinimumSize = New System.Drawing.Size(160, 0)
        Me.gbMovieSetScraperGlobalOpts.Name = "gbMovieSetScraperGlobalOpts"
        Me.gbMovieSetScraperGlobalOpts.Size = New System.Drawing.Size(160, 212)
        Me.gbMovieSetScraperGlobalOpts.TabIndex = 67
        Me.gbMovieSetScraperGlobalOpts.TabStop = False
        Me.gbMovieSetScraperGlobalOpts.Text = "Scraper Fields - Global"
        '
        'tblMovieSetScraperGlobalOpts
        '
        Me.tblMovieSetScraperGlobalOpts.AutoScroll = True
        Me.tblMovieSetScraperGlobalOpts.AutoSize = True
        Me.tblMovieSetScraperGlobalOpts.ColumnCount = 4
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkMovieSetLockPlot, 2, 2)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkMovieSetScraperPlot, 1, 2)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkMovieSetLockTitle, 2, 1)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.chkMovieSetScraperTitle, 1, 1)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.lblMovieSetScraperGlobalHeaderLock, 2, 0)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.lblMovieSetScraperGlobalTitle, 0, 1)
        Me.tblMovieSetScraperGlobalOpts.Controls.Add(Me.lblMovieSetScraperGlobalPlot, 0, 2)
        Me.tblMovieSetScraperGlobalOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieSetScraperGlobalOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieSetScraperGlobalOpts.Name = "tblMovieSetScraperGlobalOpts"
        Me.tblMovieSetScraperGlobalOpts.RowCount = 4
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieSetScraperGlobalOpts.Size = New System.Drawing.Size(154, 191)
        Me.tblMovieSetScraperGlobalOpts.TabIndex = 1
        '
        'chkMovieSetLockPlot
        '
        Me.chkMovieSetLockPlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieSetLockPlot.AutoSize = True
        Me.chkMovieSetLockPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieSetLockPlot.Location = New System.Drawing.Point(66, 43)
        Me.chkMovieSetLockPlot.Name = "chkMovieSetLockPlot"
        Me.chkMovieSetLockPlot.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieSetLockPlot.TabIndex = 0
        Me.chkMovieSetLockPlot.UseVisualStyleBackColor = True
        '
        'chkMovieSetScraperPlot
        '
        Me.chkMovieSetScraperPlot.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieSetScraperPlot.AutoSize = True
        Me.chkMovieSetScraperPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieSetScraperPlot.Location = New System.Drawing.Point(37, 43)
        Me.chkMovieSetScraperPlot.Name = "chkMovieSetScraperPlot"
        Me.chkMovieSetScraperPlot.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieSetScraperPlot.TabIndex = 12
        Me.chkMovieSetScraperPlot.UseVisualStyleBackColor = True
        '
        'chkMovieSetLockTitle
        '
        Me.chkMovieSetLockTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieSetLockTitle.AutoSize = True
        Me.chkMovieSetLockTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieSetLockTitle.Location = New System.Drawing.Point(66, 23)
        Me.chkMovieSetLockTitle.Name = "chkMovieSetLockTitle"
        Me.chkMovieSetLockTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieSetLockTitle.TabIndex = 2
        Me.chkMovieSetLockTitle.UseVisualStyleBackColor = True
        '
        'chkMovieSetScraperTitle
        '
        Me.chkMovieSetScraperTitle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMovieSetScraperTitle.AutoSize = True
        Me.chkMovieSetScraperTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieSetScraperTitle.Location = New System.Drawing.Point(37, 23)
        Me.chkMovieSetScraperTitle.Name = "chkMovieSetScraperTitle"
        Me.chkMovieSetScraperTitle.Size = New System.Drawing.Size(15, 14)
        Me.chkMovieSetScraperTitle.TabIndex = 0
        Me.chkMovieSetScraperTitle.UseVisualStyleBackColor = True
        '
        'lblMovieSetScraperGlobalHeaderLock
        '
        Me.lblMovieSetScraperGlobalHeaderLock.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblMovieSetScraperGlobalHeaderLock.AutoSize = True
        Me.lblMovieSetScraperGlobalHeaderLock.Location = New System.Drawing.Point(58, 3)
        Me.lblMovieSetScraperGlobalHeaderLock.Name = "lblMovieSetScraperGlobalHeaderLock"
        Me.lblMovieSetScraperGlobalHeaderLock.Size = New System.Drawing.Size(31, 13)
        Me.lblMovieSetScraperGlobalHeaderLock.TabIndex = 12
        Me.lblMovieSetScraperGlobalHeaderLock.Text = "Lock"
        '
        'lblMovieSetScraperGlobalTitle
        '
        Me.lblMovieSetScraperGlobalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetScraperGlobalTitle.AutoSize = True
        Me.lblMovieSetScraperGlobalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieSetScraperGlobalTitle.Location = New System.Drawing.Point(3, 23)
        Me.lblMovieSetScraperGlobalTitle.Name = "lblMovieSetScraperGlobalTitle"
        Me.lblMovieSetScraperGlobalTitle.Size = New System.Drawing.Size(28, 13)
        Me.lblMovieSetScraperGlobalTitle.TabIndex = 67
        Me.lblMovieSetScraperGlobalTitle.Text = "Title"
        '
        'lblMovieSetScraperGlobalPlot
        '
        Me.lblMovieSetScraperGlobalPlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieSetScraperGlobalPlot.AutoSize = True
        Me.lblMovieSetScraperGlobalPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovieSetScraperGlobalPlot.Location = New System.Drawing.Point(3, 43)
        Me.lblMovieSetScraperGlobalPlot.Name = "lblMovieSetScraperGlobalPlot"
        Me.lblMovieSetScraperGlobalPlot.Size = New System.Drawing.Size(27, 13)
        Me.lblMovieSetScraperGlobalPlot.TabIndex = 68
        Me.lblMovieSetScraperGlobalPlot.Text = "Plot"
        '
        'frmMovieSet_Data
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmMovieSet_Data"
        Me.Text = "frmMovieSet_Data"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbMovieSetScraperTitleRenamerOpts.ResumeLayout(False)
        Me.gbMovieSetScraperTitleRenamerOpts.PerformLayout()
        Me.tblMovieSetScraperTitleRenamerOpts.ResumeLayout(False)
        CType(Me.dgvMovieSetScraperTitleRenamer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMovieSetScraperGlobalOpts.ResumeLayout(False)
        Me.gbMovieSetScraperGlobalOpts.PerformLayout()
        Me.tblMovieSetScraperGlobalOpts.ResumeLayout(False)
        Me.tblMovieSetScraperGlobalOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbMovieSetScraperTitleRenamerOpts As GroupBox
    Friend WithEvents tblMovieSetScraperTitleRenamerOpts As TableLayoutPanel
    Friend WithEvents btnMovieSetScraperTitleRenamerAdd As Button
    Friend WithEvents btnMovieSetScraperTitleRenamerRemove As Button
    Friend WithEvents dgvMovieSetScraperTitleRenamer As DataGridView
    Friend WithEvents tbcMovieSetScrapedTitleRenamerFrom As DataGridViewTextBoxColumn
    Friend WithEvents tbcMovieSetScrapedTitleRenamerTo As DataGridViewTextBoxColumn
    Friend WithEvents gbMovieSetScraperGlobalOpts As GroupBox
    Friend WithEvents tblMovieSetScraperGlobalOpts As TableLayoutPanel
    Friend WithEvents chkMovieSetLockPlot As CheckBox
    Friend WithEvents chkMovieSetScraperPlot As CheckBox
    Friend WithEvents chkMovieSetLockTitle As CheckBox
    Friend WithEvents chkMovieSetScraperTitle As CheckBox
    Friend WithEvents lblMovieSetScraperGlobalHeaderLock As Label
    Friend WithEvents lblMovieSetScraperGlobalTitle As Label
    Friend WithEvents lblMovieSetScraperGlobalPlot As Label
End Class
