<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgRegexMapping
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgRegexMapping))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsslSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvMappings = New System.Windows.Forms.DataGridView()
        Me.colRegex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMapping = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.StatusStrip1.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        CType(Me.dgvMappings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslSpring, Me.tspbStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 739)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(584, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsslSpring
        '
        Me.tsslSpring.Name = "tsslSpring"
        Me.tsslSpring.Size = New System.Drawing.Size(569, 17)
        Me.tsslSpring.Spring = True
        '
        'tspbStatus
        '
        Me.tspbStatus.Name = "tspbStatus"
        Me.tspbStatus.Size = New System.Drawing.Size(100, 16)
        Me.tspbStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.tspbStatus.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(584, 710)
        Me.pnlMain.TabIndex = 1
        '
        'tblMain
        '
        Me.tblMain.AutoScroll = True
        Me.tblMain.AutoSize = True
        Me.tblMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMain.ColumnCount = 1
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.Controls.Add(Me.dgvMappings, 0, 0)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 1
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 710.0!))
        Me.tblMain.Size = New System.Drawing.Size(584, 710)
        Me.tblMain.TabIndex = 0
        '
        'dgvMappings
        '
        Me.dgvMappings.BackgroundColor = System.Drawing.Color.White
        Me.dgvMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMappings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRegex, Me.colMapping})
        Me.dgvMappings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMappings.Location = New System.Drawing.Point(3, 3)
        Me.dgvMappings.Name = "dgvMappings"
        Me.dgvMappings.Size = New System.Drawing.Size(578, 704)
        Me.dgvMappings.TabIndex = 0
        '
        'colRegex
        '
        Me.colRegex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colRegex.HeaderText = "Regex"
        Me.colRegex.Name = "colRegex"
        Me.colRegex.Width = 63
        '
        'colMapping
        '
        Me.colMapping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colMapping.HeaderText = "Mapped to"
        Me.colMapping.Name = "colMapping"
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 710)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(584, 29)
        Me.pnlBottom.TabIndex = 2
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 3
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnCancel, 1, 0)
        Me.tblBottom.Controls.Add(Me.btnOK, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(584, 29)
        Me.tblBottom.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCancel.Location = New System.Drawing.Point(480, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(101, 23)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Location = New System.Drawing.Point(381, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(93, 23)
        Me.btnOK.TabIndex = 14
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'dlgRegexMapping
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(584, 761)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgRegexMapping"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edition Mapping"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        CType(Me.dgvMappings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As Windows.Forms.StatusStrip
    Friend WithEvents pnlMain As Windows.Forms.Panel
    Friend WithEvents tblMain As Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlBottom As Windows.Forms.Panel
    Friend WithEvents tblBottom As Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOK As Windows.Forms.Button
    Friend WithEvents btnCancel As Windows.Forms.Button
    Friend WithEvents tsslSpring As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tspbStatus As Windows.Forms.ToolStripProgressBar
    Friend WithEvents dgvMappings As Windows.Forms.DataGridView
    Friend WithEvents colRegex As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colMapping As Windows.Forms.DataGridViewTextBoxColumn
End Class
