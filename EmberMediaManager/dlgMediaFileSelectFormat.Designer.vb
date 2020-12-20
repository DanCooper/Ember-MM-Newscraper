<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgMediaFileSelectFormat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgMediaFileSelectFormat))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.lbVideoFormats = New System.Windows.Forms.ListBox()
        Me.gbVideoFormats = New System.Windows.Forms.GroupBox()
        Me.pnlTrailerFormat = New System.Windows.Forms.Panel()
        Me.gbAudioFormats = New System.Windows.Forms.GroupBox()
        Me.lbAudioFormats = New System.Windows.Forms.ListBox()
        Me.gbVideoFormats.SuspendLayout()
        Me.pnlTrailerFormat.SuspendLayout()
        Me.gbAudioFormats.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Enabled = False
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(268, 187)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(341, 187)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'lbVideoFormats
        '
        Me.lbVideoFormats.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbVideoFormats.FormattingEnabled = True
        Me.lbVideoFormats.Location = New System.Drawing.Point(4, 19)
        Me.lbVideoFormats.Name = "lbVideoFormats"
        Me.lbVideoFormats.Size = New System.Drawing.Size(170, 121)
        Me.lbVideoFormats.TabIndex = 0
        '
        'gbVideoFormats
        '
        Me.gbVideoFormats.Controls.Add(Me.lbVideoFormats)
        Me.gbVideoFormats.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbVideoFormats.Location = New System.Drawing.Point(10, 8)
        Me.gbVideoFormats.Name = "gbVideoFormats"
        Me.gbVideoFormats.Size = New System.Drawing.Size(180, 151)
        Me.gbVideoFormats.TabIndex = 0
        Me.gbVideoFormats.TabStop = False
        Me.gbVideoFormats.Text = "Available Video Formats"
        '
        'pnlTrailerFormat
        '
        Me.pnlTrailerFormat.BackColor = System.Drawing.Color.White
        Me.pnlTrailerFormat.Controls.Add(Me.gbAudioFormats)
        Me.pnlTrailerFormat.Controls.Add(Me.gbVideoFormats)
        Me.pnlTrailerFormat.Location = New System.Drawing.Point(12, 12)
        Me.pnlTrailerFormat.Name = "pnlTrailerFormat"
        Me.pnlTrailerFormat.Size = New System.Drawing.Size(396, 169)
        Me.pnlTrailerFormat.TabIndex = 2
        '
        'gbAudioFormats
        '
        Me.gbAudioFormats.Controls.Add(Me.lbAudioFormats)
        Me.gbAudioFormats.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbAudioFormats.Location = New System.Drawing.Point(206, 8)
        Me.gbAudioFormats.Name = "gbAudioFormats"
        Me.gbAudioFormats.Size = New System.Drawing.Size(180, 151)
        Me.gbAudioFormats.TabIndex = 1
        Me.gbAudioFormats.TabStop = False
        Me.gbAudioFormats.Text = "Available Audio Formats"
        '
        'lbAudioFormats
        '
        Me.lbAudioFormats.Enabled = False
        Me.lbAudioFormats.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbAudioFormats.FormattingEnabled = True
        Me.lbAudioFormats.Location = New System.Drawing.Point(4, 19)
        Me.lbAudioFormats.Name = "lbAudioFormats"
        Me.lbAudioFormats.Size = New System.Drawing.Size(170, 121)
        Me.lbAudioFormats.TabIndex = 0
        '
        'dlgTrailerFormat
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(418, 217)
        Me.Controls.Add(Me.pnlTrailerFormat)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgTrailerFormat"
        Me.Text = "Select Format"
        Me.gbVideoFormats.ResumeLayout(False)
        Me.pnlTrailerFormat.ResumeLayout(False)
        Me.gbAudioFormats.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents lbVideoFormats As System.Windows.Forms.ListBox
    Friend WithEvents gbVideoFormats As System.Windows.Forms.GroupBox
    Friend WithEvents pnlTrailerFormat As System.Windows.Forms.Panel
    Friend WithEvents gbAudioFormats As System.Windows.Forms.GroupBox
    Friend WithEvents lbAudioFormats As System.Windows.Forms.ListBox

End Class
