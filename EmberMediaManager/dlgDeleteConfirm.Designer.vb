<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgDeleteConfirm
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgDeleteConfirm))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.tvFiles = New System.Windows.Forms.TreeView()
        Me.ilFiles = New System.Windows.Forms.ImageList(Me.components)
        Me.btnToggleAllFiles = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.ssStatus = New System.Windows.Forms.StatusStrip()
        Me.tsslSelectedNode = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.ssStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOK.Location = New System.Drawing.Point(341, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(67, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(414, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(67, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'tvFiles
        '
        Me.tvFiles.CheckBoxes = True
        Me.tvFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvFiles.ImageIndex = 0
        Me.tvFiles.ImageList = Me.ilFiles
        Me.tvFiles.Location = New System.Drawing.Point(0, 0)
        Me.tvFiles.Name = "tvFiles"
        Me.tvFiles.SelectedImageIndex = 0
        Me.tvFiles.Size = New System.Drawing.Size(484, 310)
        Me.tvFiles.TabIndex = 2
        '
        'ilFiles
        '
        Me.ilFiles.ImageStream = CType(resources.GetObject("ilFiles.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilFiles.TransparentColor = System.Drawing.Color.Transparent
        Me.ilFiles.Images.SetKeyName(0, "DBE")
        Me.ilFiles.Images.SetKeyName(1, "FILE")
        Me.ilFiles.Images.SetKeyName(2, "FOLDER")
        Me.ilFiles.Images.SetKeyName(3, "VIDEO")
        '
        'btnToggleAllFiles
        '
        Me.btnToggleAllFiles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnToggleAllFiles.Location = New System.Drawing.Point(3, 3)
        Me.btnToggleAllFiles.Name = "btnToggleAllFiles"
        Me.btnToggleAllFiles.Size = New System.Drawing.Size(115, 23)
        Me.btnToggleAllFiles.TabIndex = 4
        Me.btnToggleAllFiles.Text = "Toggle All Files"
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 310)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(484, 29)
        Me.pnlBottom.TabIndex = 5
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 4
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnToggleAllFiles, 0, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 3, 0)
        Me.tblBottom.Controls.Add(Me.btnOK, 2, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(484, 29)
        Me.tblBottom.TabIndex = 0
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.Controls.Add(Me.tvFiles)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(484, 310)
        Me.pnlMain.TabIndex = 6
        '
        'ssStatus
        '
        Me.ssStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslSelectedNode})
        Me.ssStatus.Location = New System.Drawing.Point(0, 339)
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(484, 22)
        Me.ssStatus.TabIndex = 3
        Me.ssStatus.Text = "StatusStrip1"
        '
        'tsslSelectedNode
        '
        Me.tsslSelectedNode.Name = "tsslSelectedNode"
        Me.tsslSelectedNode.Size = New System.Drawing.Size(95, 17)
        Me.tsslSelectedNode.Text = "SelctedNodeInfo"
        '
        'dlgDeleteConfirm
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(484, 361)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.ssStatus)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgDeleteConfirm"
        Me.ShowInTaskbar = False
        Me.Text = "Confirm Items To Be Deleted"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.ssStatus.ResumeLayout(False)
        Me.ssStatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents tvFiles As System.Windows.Forms.TreeView
    Friend WithEvents ilFiles As System.Windows.Forms.ImageList
    Friend WithEvents btnToggleAllFiles As System.Windows.Forms.Button
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents pnlMain As Panel
    Friend WithEvents ssStatus As StatusStrip
    Friend WithEvents tsslSelectedNode As ToolStripStatusLabel
End Class
