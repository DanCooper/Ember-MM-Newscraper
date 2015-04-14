<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgCopyFiles
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
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.lblFilename = New System.Windows.Forms.Label()
        Me.lblUnknown = New System.Windows.Forms.Label()
        Me.prbStatus = New System.Windows.Forms.ProgressBar()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.pnlStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(130, 73)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 0
        Me.Cancel_Button.Text = "Cancel"
        '
        'lblFilename
        '
        Me.lblFilename.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilename.Location = New System.Drawing.Point(28, 4)
        Me.lblFilename.Name = "lblFilename"
        Me.lblFilename.Size = New System.Drawing.Size(261, 18)
        Me.lblFilename.TabIndex = 0
        Me.lblFilename.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblUnknown
        '
        Me.lblUnknown.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUnknown.Location = New System.Drawing.Point(28, 25)
        Me.lblUnknown.Name = "lblUnknown"
        Me.lblUnknown.Size = New System.Drawing.Size(261, 18)
        Me.lblUnknown.TabIndex = 1
        Me.lblUnknown.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'prbStatus
        '
        Me.prbStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.prbStatus.Location = New System.Drawing.Point(9, 46)
        Me.prbStatus.Name = "prbStatus"
        Me.prbStatus.Size = New System.Drawing.Size(297, 13)
        Me.prbStatus.TabIndex = 2
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.White
        Me.pnlStatus.Controls.Add(Me.prbStatus)
        Me.pnlStatus.Controls.Add(Me.lblUnknown)
        Me.pnlStatus.Controls.Add(Me.lblFilename)
        Me.pnlStatus.Location = New System.Drawing.Point(3, 3)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(316, 69)
        Me.pnlStatus.TabIndex = 1
        '
        'dlgCopyFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(321, 98)
        Me.Controls.Add(Me.pnlStatus)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgCopyFiles"
        Me.ShowInTaskbar = False
        Me.Text = "dlgCopyFiles"
        Me.pnlStatus.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents lblFilename As System.Windows.Forms.Label
    Friend WithEvents lblUnknown As System.Windows.Forms.Label
    Friend WithEvents prbStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel

End Class
