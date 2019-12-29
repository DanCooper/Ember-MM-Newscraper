<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTV_Source_Regex
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTV_Source_Regex))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbEpisodeMultipartMatching = New System.Windows.Forms.GroupBox()
        Me.tblEpisodeMultipartMatching = New System.Windows.Forms.TableLayoutPanel()
        Me.txtEpisodeMultipartMatching = New System.Windows.Forms.TextBox()
        Me.btnEpisodeMultipartMatchingDefaults = New System.Windows.Forms.Button()
        Me.gbEpisodeMatching = New System.Windows.Forms.GroupBox()
        Me.tblEpisodeMatching = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvEpisodeMatching = New System.Windows.Forms.DataGridView()
        Me.colEpisodeMatchingIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEpisodeMatchingRegex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEpisodeMatchingDefaultSeason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEpisodeMatchingByDate = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnEpisodeMatchingDefaults = New System.Windows.Forms.Button()
        Me.lblEpisodeMatching = New System.Windows.Forms.Label()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbEpisodeMultipartMatching.SuspendLayout()
        Me.tblEpisodeMultipartMatching.SuspendLayout()
        Me.gbEpisodeMatching.SuspendLayout()
        Me.tblEpisodeMatching.SuspendLayout()
        CType(Me.dgvEpisodeMatching, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(652, 441)
        Me.pnlSettings.TabIndex = 0
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSettings.ColumnCount = 1
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbEpisodeMultipartMatching, 0, 1)
        Me.tblSettings.Controls.Add(Me.gbEpisodeMatching, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 3
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(652, 441)
        Me.tblSettings.TabIndex = 0
        '
        'gbEpisodeMultipartMatching
        '
        Me.gbEpisodeMultipartMatching.AutoSize = True
        Me.gbEpisodeMultipartMatching.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbEpisodeMultipartMatching.Controls.Add(Me.tblEpisodeMultipartMatching)
        Me.gbEpisodeMultipartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbEpisodeMultipartMatching.Location = New System.Drawing.Point(3, 365)
        Me.gbEpisodeMultipartMatching.Name = "gbEpisodeMultipartMatching"
        Me.gbEpisodeMultipartMatching.Size = New System.Drawing.Size(646, 50)
        Me.gbEpisodeMultipartMatching.TabIndex = 9
        Me.gbEpisodeMultipartMatching.TabStop = False
        Me.gbEpisodeMultipartMatching.Text = "Episode Multipart Matching"
        '
        'tblEpisodeMultipartMatching
        '
        Me.tblEpisodeMultipartMatching.AutoSize = True
        Me.tblEpisodeMultipartMatching.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblEpisodeMultipartMatching.ColumnCount = 2
        Me.tblEpisodeMultipartMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblEpisodeMultipartMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeMultipartMatching.Controls.Add(Me.txtEpisodeMultipartMatching, 0, 0)
        Me.tblEpisodeMultipartMatching.Controls.Add(Me.btnEpisodeMultipartMatchingDefaults, 1, 0)
        Me.tblEpisodeMultipartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblEpisodeMultipartMatching.Location = New System.Drawing.Point(3, 18)
        Me.tblEpisodeMultipartMatching.Name = "tblEpisodeMultipartMatching"
        Me.tblEpisodeMultipartMatching.RowCount = 1
        Me.tblEpisodeMultipartMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeMultipartMatching.Size = New System.Drawing.Size(640, 29)
        Me.tblEpisodeMultipartMatching.TabIndex = 0
        '
        'txtEpisodeMultipartMatching
        '
        Me.txtEpisodeMultipartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtEpisodeMultipartMatching.Location = New System.Drawing.Point(3, 3)
        Me.txtEpisodeMultipartMatching.Name = "txtEpisodeMultipartMatching"
        Me.txtEpisodeMultipartMatching.Size = New System.Drawing.Size(523, 22)
        Me.txtEpisodeMultipartMatching.TabIndex = 0
        '
        'btnEpisodeMultipartMatchingDefaults
        '
        Me.btnEpisodeMultipartMatchingDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEpisodeMultipartMatchingDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEpisodeMultipartMatchingDefaults.Image = CType(resources.GetObject("btnEpisodeMultipartMatchingDefaults.Image"), System.Drawing.Image)
        Me.btnEpisodeMultipartMatchingDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEpisodeMultipartMatchingDefaults.Location = New System.Drawing.Point(532, 3)
        Me.btnEpisodeMultipartMatchingDefaults.Name = "btnEpisodeMultipartMatchingDefaults"
        Me.btnEpisodeMultipartMatchingDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnEpisodeMultipartMatchingDefaults.TabIndex = 5
        Me.btnEpisodeMultipartMatchingDefaults.Text = "Defaults"
        Me.btnEpisodeMultipartMatchingDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEpisodeMultipartMatchingDefaults.UseVisualStyleBackColor = True
        '
        'gbEpisodeMatching
        '
        Me.gbEpisodeMatching.AutoSize = True
        Me.gbEpisodeMatching.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbEpisodeMatching.Controls.Add(Me.tblEpisodeMatching)
        Me.gbEpisodeMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbEpisodeMatching.Location = New System.Drawing.Point(3, 3)
        Me.gbEpisodeMatching.Name = "gbEpisodeMatching"
        Me.gbEpisodeMatching.Size = New System.Drawing.Size(646, 356)
        Me.gbEpisodeMatching.TabIndex = 8
        Me.gbEpisodeMatching.TabStop = False
        Me.gbEpisodeMatching.Text = "Episode Matching"
        '
        'tblEpisodeMatching
        '
        Me.tblEpisodeMatching.AutoSize = True
        Me.tblEpisodeMatching.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblEpisodeMatching.ColumnCount = 2
        Me.tblEpisodeMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeMatching.Controls.Add(Me.dgvEpisodeMatching, 0, 0)
        Me.tblEpisodeMatching.Controls.Add(Me.btnEpisodeMatchingDefaults, 1, 1)
        Me.tblEpisodeMatching.Controls.Add(Me.lblEpisodeMatching, 0, 1)
        Me.tblEpisodeMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblEpisodeMatching.Location = New System.Drawing.Point(3, 18)
        Me.tblEpisodeMatching.Name = "tblEpisodeMatching"
        Me.tblEpisodeMatching.RowCount = 2
        Me.tblEpisodeMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeMatching.Size = New System.Drawing.Size(640, 335)
        Me.tblEpisodeMatching.TabIndex = 8
        '
        'dgvEpisodeMatching
        '
        Me.dgvEpisodeMatching.AllowUserToResizeColumns = False
        Me.dgvEpisodeMatching.AllowUserToResizeRows = False
        Me.dgvEpisodeMatching.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvEpisodeMatching.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvEpisodeMatching.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvEpisodeMatching.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colEpisodeMatchingIndex, Me.colEpisodeMatchingRegex, Me.colEpisodeMatchingDefaultSeason, Me.colEpisodeMatchingByDate})
        Me.tblEpisodeMatching.SetColumnSpan(Me.dgvEpisodeMatching, 2)
        Me.dgvEpisodeMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEpisodeMatching.Location = New System.Drawing.Point(3, 3)
        Me.dgvEpisodeMatching.Name = "dgvEpisodeMatching"
        Me.dgvEpisodeMatching.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEpisodeMatching.ShowCellErrors = False
        Me.dgvEpisodeMatching.ShowCellToolTips = False
        Me.dgvEpisodeMatching.ShowRowErrors = False
        Me.dgvEpisodeMatching.Size = New System.Drawing.Size(634, 300)
        Me.dgvEpisodeMatching.TabIndex = 12
        '
        'colEpisodeMatchingIndex
        '
        Me.colEpisodeMatchingIndex.HeaderText = "Index"
        Me.colEpisodeMatchingIndex.Name = "colEpisodeMatchingIndex"
        Me.colEpisodeMatchingIndex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colEpisodeMatchingIndex.Visible = False
        '
        'colEpisodeMatchingRegex
        '
        Me.colEpisodeMatchingRegex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEpisodeMatchingRegex.HeaderText = "Regex"
        Me.colEpisodeMatchingRegex.MinimumWidth = 50
        Me.colEpisodeMatchingRegex.Name = "colEpisodeMatchingRegex"
        Me.colEpisodeMatchingRegex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colEpisodeMatchingDefaultSeason
        '
        Me.colEpisodeMatchingDefaultSeason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colEpisodeMatchingDefaultSeason.HeaderText = "Default Season"
        Me.colEpisodeMatchingDefaultSeason.Name = "colEpisodeMatchingDefaultSeason"
        Me.colEpisodeMatchingDefaultSeason.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colEpisodeMatchingDefaultSeason.Width = 91
        '
        'colEpisodeMatchingByDate
        '
        Me.colEpisodeMatchingByDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colEpisodeMatchingByDate.HeaderText = "by Date"
        Me.colEpisodeMatchingByDate.Name = "colEpisodeMatchingByDate"
        Me.colEpisodeMatchingByDate.Width = 52
        '
        'btnEpisodeMatchingDefaults
        '
        Me.btnEpisodeMatchingDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEpisodeMatchingDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEpisodeMatchingDefaults.Image = CType(resources.GetObject("btnEpisodeMatchingDefaults.Image"), System.Drawing.Image)
        Me.btnEpisodeMatchingDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEpisodeMatchingDefaults.Location = New System.Drawing.Point(532, 309)
        Me.btnEpisodeMatchingDefaults.Name = "btnEpisodeMatchingDefaults"
        Me.btnEpisodeMatchingDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnEpisodeMatchingDefaults.TabIndex = 5
        Me.btnEpisodeMatchingDefaults.Text = "Defaults"
        Me.btnEpisodeMatchingDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEpisodeMatchingDefaults.UseVisualStyleBackColor = True
        '
        'lblEpisodeMatching
        '
        Me.lblEpisodeMatching.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblEpisodeMatching.AutoSize = True
        Me.lblEpisodeMatching.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEpisodeMatching.Location = New System.Drawing.Point(3, 314)
        Me.lblEpisodeMatching.Name = "lblEpisodeMatching"
        Me.lblEpisodeMatching.Size = New System.Drawing.Size(201, 13)
        Me.lblEpisodeMatching.TabIndex = 6
        Me.lblEpisodeMatching.Text = "Use ALT + UP / DOWN to move the rows"
        '
        'frmTV_Source_Regex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 441)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmTV_Source_Regex"
        Me.Text = "frmTV_Source_Regex"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbEpisodeMultipartMatching.ResumeLayout(False)
        Me.gbEpisodeMultipartMatching.PerformLayout()
        Me.tblEpisodeMultipartMatching.ResumeLayout(False)
        Me.tblEpisodeMultipartMatching.PerformLayout()
        Me.gbEpisodeMatching.ResumeLayout(False)
        Me.gbEpisodeMatching.PerformLayout()
        Me.tblEpisodeMatching.ResumeLayout(False)
        Me.tblEpisodeMatching.PerformLayout()
        CType(Me.dgvEpisodeMatching, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbEpisodeMatching As GroupBox
    Friend WithEvents tblEpisodeMatching As TableLayoutPanel
    Friend WithEvents gbEpisodeMultipartMatching As GroupBox
    Friend WithEvents tblEpisodeMultipartMatching As TableLayoutPanel
    Friend WithEvents txtEpisodeMultipartMatching As TextBox
    Friend WithEvents btnEpisodeMultipartMatchingDefaults As Button
    Friend WithEvents btnEpisodeMatchingDefaults As Button
    Friend WithEvents lblEpisodeMatching As Label
    Friend WithEvents dgvEpisodeMatching As DataGridView
    Friend WithEvents colEpisodeMatchingIndex As DataGridViewTextBoxColumn
    Friend WithEvents colEpisodeMatchingRegex As DataGridViewTextBoxColumn
    Friend WithEvents colEpisodeMatchingDefaultSeason As DataGridViewTextBoxColumn
    Friend WithEvents colEpisodeMatchingByDate As DataGridViewCheckBoxColumn
End Class
