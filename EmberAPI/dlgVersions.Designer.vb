<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgVersions
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
        Me.btnOk = New System.Windows.Forms.Button()
        Me.lstVersions = New System.Windows.Forms.ListView()
        Me.colAddon = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnOk.Location = New System.Drawing.Point(350, 289)
        Me.btnOk.Name = "OK_Button"
        Me.btnOk.Size = New System.Drawing.Size(67, 23)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "OK"
        '
        'lstVersions
        '
        Me.lstVersions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colAddon, Me.colVersion})
        Me.lstVersions.FullRowSelect = True
        Me.lstVersions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstVersions.HideSelection = False
        Me.lstVersions.Location = New System.Drawing.Point(0, 0)
        Me.lstVersions.Name = "lstVersions"
        Me.lstVersions.Size = New System.Drawing.Size(417, 286)
        Me.lstVersions.TabIndex = 2
        Me.lstVersions.UseCompatibleStateImageBehavior = False
        Me.lstVersions.View = System.Windows.Forms.View.Details
        '
        'colAddon
        '
        Me.colAddon.Text = "Addon"
        Me.colAddon.Width = 290
        '
        'colVersion
        '
        Me.colVersion.Text = "Version"
        Me.colVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colVersion.Width = 99
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(0, 288)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(133, 23)
        Me.btnCopy.TabIndex = 1
        Me.btnCopy.Text = "Copy to Clipboard"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'dlgVersions
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnOk
        Me.ClientSize = New System.Drawing.Size(418, 314)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.lstVersions)
        Me.Controls.Add(Me.btnOk)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgVersions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Versions"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents colAddon As System.Windows.Forms.ColumnHeader
    Friend WithEvents colVersion As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstVersions As System.Windows.Forms.ListView
    Friend WithEvents btnCopy As System.Windows.Forms.Button

End Class
