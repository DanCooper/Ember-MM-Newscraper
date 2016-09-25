<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSplash
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.LoadingBar = New System.Windows.Forms.ProgressBar()
        Me.VersionNumber = New System.Windows.Forms.Label()
        Me.LoadingMesg = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LoadingBar
        '
        Me.LoadingBar.Location = New System.Drawing.Point(1, 357)
        Me.LoadingBar.Maximum = 9
        Me.LoadingBar.Name = "LoadingBar"
        Me.LoadingBar.Size = New System.Drawing.Size(371, 15)
        Me.LoadingBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.LoadingBar.TabIndex = 2
        '
        'VersionNumber
        '
        Me.VersionNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionNumber.BackColor = System.Drawing.Color.Transparent
        Me.VersionNumber.Location = New System.Drawing.Point(229, 341)
        Me.VersionNumber.Name = "VersionNumber"
        Me.VersionNumber.Size = New System.Drawing.Size(143, 13)
        Me.VersionNumber.TabIndex = 1
        Me.VersionNumber.Text = "Version {0}.{1}.{2}.{3}"
        Me.VersionNumber.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LoadingMesg
        '
        Me.LoadingMesg.BackColor = System.Drawing.Color.Transparent
        Me.LoadingMesg.Location = New System.Drawing.Point(4, 341)
        Me.LoadingMesg.Name = "LoadingMesg"
        Me.LoadingMesg.Size = New System.Drawing.Size(219, 13)
        Me.LoadingMesg.TabIndex = 0
        Me.LoadingMesg.Text = "Loading..."
        '
        'frmSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(373, 373)
        Me.ControlBox = False
        Me.Controls.Add(Me.LoadingMesg)
        Me.Controls.Add(Me.VersionNumber)
        Me.Controls.Add(Me.LoadingBar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LoadingBar As System.Windows.Forms.ProgressBar
    Friend WithEvents VersionNumber As System.Windows.Forms.Label
    Friend WithEvents LoadingMesg As System.Windows.Forms.Label
End Class
