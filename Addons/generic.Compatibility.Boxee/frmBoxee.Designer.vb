<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBoxee
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
        Me.pnlBoxee = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkBoxeeId = New System.Windows.Forms.CheckBox()
        Me.pnlEnabled = New System.Windows.Forms.Panel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlBoxee.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlEnabled.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBoxee
        '
        Me.pnlBoxee.BackColor = System.Drawing.Color.White
        Me.pnlBoxee.Controls.Add(Me.GroupBox1)
        Me.pnlBoxee.Controls.Add(Me.pnlEnabled)
        Me.pnlBoxee.Location = New System.Drawing.Point(12, 12)
        Me.pnlBoxee.Name = "pnlBoxee"
        Me.pnlBoxee.Size = New System.Drawing.Size(617, 327)
        Me.pnlBoxee.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkBoxeeId)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(10, 31)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(604, 49)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "TV Show Options"
        '
        'chkBoxeeId
        '
        Me.chkBoxeeId.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkBoxeeId.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxeeId.Location = New System.Drawing.Point(6, 21)
        Me.chkBoxeeId.Name = "chkBoxeeId"
        Me.chkBoxeeId.Size = New System.Drawing.Size(584, 17)
        Me.chkBoxeeId.TabIndex = 0
        Me.chkBoxeeId.Text = "Replace ID field with Boxee Id Field"
        Me.chkBoxeeId.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkBoxeeId.UseVisualStyleBackColor = True
        '
        'pnlEnabled
        '
        Me.pnlEnabled.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlEnabled.Controls.Add(Me.chkEnabled)
        Me.pnlEnabled.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEnabled.Location = New System.Drawing.Point(0, 0)
        Me.pnlEnabled.Name = "pnlEnabled"
        Me.pnlEnabled.Size = New System.Drawing.Size(617, 25)
        Me.pnlEnabled.TabIndex = 0
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(10, 5)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(65, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'frmBoxee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 356)
        Me.Controls.Add(Me.pnlBoxee)
        Me.Name = "frmBoxee"
        Me.Text = "frmBoxee"
        Me.pnlBoxee.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.pnlEnabled.ResumeLayout(False)
        Me.pnlEnabled.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlBoxee As System.Windows.Forms.Panel
    Friend WithEvents chkBoxeeId As System.Windows.Forms.CheckBox
    Friend WithEvents pnlEnabled As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
