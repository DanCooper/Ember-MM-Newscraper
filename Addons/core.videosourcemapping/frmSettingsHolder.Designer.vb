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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblByName = New System.Windows.Forms.Label()
        Me.btnLoadDefaultsByName = New System.Windows.Forms.Button()
        Me.dgvByExtension = New System.Windows.Forms.DataGridView()
        Me.dgvByName = New System.Windows.Forms.DataGridView()
        Me.lblByExtension = New System.Windows.Forms.Label()
        Me.colByExtensionExtension = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colByExtensionMapping = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colByFilenameRegex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colByFilenameMapping = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        CType(Me.dgvByExtension, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvByName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(684, 461)
        Me.pnlMain.TabIndex = 0
        '
        'tblMain
        '
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 3
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.Controls.Add(Me.lblByName, 0, 0)
        Me.tblMain.Controls.Add(Me.btnLoadDefaultsByName, 0, 2)
        Me.tblMain.Controls.Add(Me.dgvByExtension, 2, 1)
        Me.tblMain.Controls.Add(Me.dgvByName, 0, 1)
        Me.tblMain.Controls.Add(Me.lblByExtension, 2, 0)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 3
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(684, 461)
        Me.tblMain.TabIndex = 9
        '
        'lblByName
        '
        Me.lblByName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblByName.AutoSize = True
        Me.lblByName.Location = New System.Drawing.Point(3, 6)
        Me.lblByName.Name = "lblByName"
        Me.lblByName.Size = New System.Drawing.Size(154, 13)
        Me.lblByName.TabIndex = 0
        Me.lblByName.Text = "Map Video Source by Filename"
        '
        'btnLoadDefaultsByName
        '
        Me.btnLoadDefaultsByName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadDefaultsByName.Image = CType(resources.GetObject("btnLoadDefaultsByName.Image"), System.Drawing.Image)
        Me.btnLoadDefaultsByName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadDefaultsByName.Location = New System.Drawing.Point(3, 435)
        Me.btnLoadDefaultsByName.Name = "btnLoadDefaultsByName"
        Me.btnLoadDefaultsByName.Size = New System.Drawing.Size(105, 23)
        Me.btnLoadDefaultsByName.TabIndex = 2
        Me.btnLoadDefaultsByName.Text = "Defaults"
        Me.btnLoadDefaultsByName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLoadDefaultsByName.UseVisualStyleBackColor = True
        '
        'dgvByExtension
        '
        Me.dgvByExtension.AllowUserToResizeColumns = False
        Me.dgvByExtension.AllowUserToResizeRows = False
        Me.dgvByExtension.BackgroundColor = System.Drawing.Color.White
        Me.dgvByExtension.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvByExtension.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colByExtensionExtension, Me.colByExtensionMapping})
        Me.dgvByExtension.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvByExtension.Location = New System.Drawing.Point(355, 28)
        Me.dgvByExtension.Name = "dgvByExtension"
        Me.dgvByExtension.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvByExtension.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvByExtension.ShowCellErrors = False
        Me.dgvByExtension.ShowCellToolTips = False
        Me.dgvByExtension.ShowRowErrors = False
        Me.dgvByExtension.Size = New System.Drawing.Size(326, 401)
        Me.dgvByExtension.TabIndex = 6
        '
        'dgvByName
        '
        Me.dgvByName.AllowUserToResizeColumns = False
        Me.dgvByName.AllowUserToResizeRows = False
        Me.dgvByName.BackgroundColor = System.Drawing.Color.White
        Me.dgvByName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvByName.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colByFilenameRegex, Me.colByFilenameMapping})
        Me.dgvByName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvByName.Location = New System.Drawing.Point(3, 28)
        Me.dgvByName.Name = "dgvByName"
        Me.dgvByName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvByName.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvByName.ShowCellErrors = False
        Me.dgvByName.ShowCellToolTips = False
        Me.dgvByName.ShowRowErrors = False
        Me.dgvByName.Size = New System.Drawing.Size(326, 401)
        Me.dgvByName.TabIndex = 1
        '
        'lblByExtension
        '
        Me.lblByExtension.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblByExtension.AutoSize = True
        Me.lblByExtension.Location = New System.Drawing.Point(355, 6)
        Me.lblByExtension.Name = "lblByExtension"
        Me.lblByExtension.Size = New System.Drawing.Size(177, 13)
        Me.lblByExtension.TabIndex = 9
        Me.lblByExtension.Text = "Map Video Source by File Extension"
        '
        'colByExtensionExtension
        '
        Me.colByExtensionExtension.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colByExtensionExtension.HeaderText = "File Extension"
        Me.colByExtensionExtension.Name = "colByExtensionExtension"
        '
        'colByExtensionMapping
        '
        Me.colByExtensionMapping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colByExtensionMapping.HeaderText = "Mapped to"
        Me.colByExtensionMapping.MinimumWidth = 120
        Me.colByExtensionMapping.Name = "colByExtensionMapping"
        Me.colByExtensionMapping.Width = 120
        '
        'colByFilenameRegex
        '
        Me.colByFilenameRegex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colByFilenameRegex.HeaderText = "Regex"
        Me.colByFilenameRegex.Name = "colByFilenameRegex"
        '
        'colByFilenameMapping
        '
        Me.colByFilenameMapping.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colByFilenameMapping.HeaderText = "Mapped to"
        Me.colByFilenameMapping.MinimumWidth = 120
        Me.colByFilenameMapping.Name = "colByFilenameMapping"
        Me.colByFilenameMapping.Width = 120
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 461)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmVideoSourceMapping"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        CType(Me.dgvByExtension, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvByName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents dgvByName As System.Windows.Forms.DataGridView
    Friend WithEvents lblByName As System.Windows.Forms.Label
    Friend WithEvents btnLoadDefaultsByName As System.Windows.Forms.Button
    Friend WithEvents dgvByExtension As System.Windows.Forms.DataGridView
    Friend WithEvents tblMain As Windows.Forms.TableLayoutPanel
    Friend WithEvents lblByExtension As Windows.Forms.Label
    Friend WithEvents colByExtensionExtension As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colByExtensionMapping As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colByFilenameRegex As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colByFilenameMapping As Windows.Forms.DataGridViewTextBoxColumn
End Class
