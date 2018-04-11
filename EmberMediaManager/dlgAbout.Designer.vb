<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgAbout
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgAbout))
        Me.OKButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pbYouTube = New System.Windows.Forms.PictureBox()
        Me.pbXBMC = New System.Windows.Forms.PictureBox()
        Me.pbFFMPEG = New System.Windows.Forms.PictureBox()
        Me.pbMI = New System.Windows.Forms.PictureBox()
        Me.picDisplay = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.pbYouTube, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbXBMC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFFMPEG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OKButton
        '
        Me.OKButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.OKButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OKButton.Location = New System.Drawing.Point(342, 343)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(87, 23)
        Me.OKButton.TabIndex = 0
        Me.OKButton.Text = "&OK"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.pbYouTube)
        Me.Panel1.Controls.Add(Me.pbXBMC)
        Me.Panel1.Controls.Add(Me.pbFFMPEG)
        Me.Panel1.Controls.Add(Me.pbMI)
        Me.Panel1.Location = New System.Drawing.Point(7, 226)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(422, 114)
        Me.Panel1.TabIndex = 1
        '
        'pbYouTube
        '
        Me.pbYouTube.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbYouTube.Image = CType(resources.GetObject("pbYouTube.Image"), System.Drawing.Image)
        Me.pbYouTube.Location = New System.Drawing.Point(-2, 34)
        Me.pbYouTube.Name = "pbYouTube"
        Me.pbYouTube.Size = New System.Drawing.Size(91, 38)
        Me.pbYouTube.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbYouTube.TabIndex = 17
        Me.pbYouTube.TabStop = False
        '
        'pbXBMC
        '
        Me.pbXBMC.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbXBMC.Image = CType(resources.GetObject("pbXBMC.Image"), System.Drawing.Image)
        Me.pbXBMC.Location = New System.Drawing.Point(3, 3)
        Me.pbXBMC.Name = "pbXBMC"
        Me.pbXBMC.Size = New System.Drawing.Size(133, 25)
        Me.pbXBMC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbXBMC.TabIndex = 16
        Me.pbXBMC.TabStop = False
        '
        'pbFFMPEG
        '
        Me.pbFFMPEG.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbFFMPEG.Image = CType(resources.GetObject("pbFFMPEG.Image"), System.Drawing.Image)
        Me.pbFFMPEG.Location = New System.Drawing.Point(293, 2)
        Me.pbFFMPEG.Name = "pbFFMPEG"
        Me.pbFFMPEG.Size = New System.Drawing.Size(124, 26)
        Me.pbFFMPEG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbFFMPEG.TabIndex = 14
        Me.pbFFMPEG.TabStop = False
        '
        'pbMI
        '
        Me.pbMI.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbMI.Image = CType(resources.GetObject("pbMI.Image"), System.Drawing.Image)
        Me.pbMI.Location = New System.Drawing.Point(326, 33)
        Me.pbMI.Name = "pbMI"
        Me.pbMI.Size = New System.Drawing.Size(91, 38)
        Me.pbMI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMI.TabIndex = 13
        Me.pbMI.TabStop = False
        '
        'picDisplay
        '
        Me.picDisplay.BackColor = System.Drawing.Color.Transparent
        Me.picDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picDisplay.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picDisplay.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.picDisplay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picDisplay.Location = New System.Drawing.Point(6, 5)
        Me.picDisplay.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.picDisplay.Name = "picDisplay"
        Me.picDisplay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picDisplay.Size = New System.Drawing.Size(423, 213)
        Me.picDisplay.TabIndex = 10
        Me.picDisplay.TabStop = False
        '
        'dlgAbout
        '
        Me.AcceptButton = Me.OKButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.OKButton
        Me.ClientSize = New System.Drawing.Size(435, 370)
        Me.Controls.Add(Me.picDisplay)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.OKButton)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgAbout"
        Me.Padding = New System.Windows.Forms.Padding(11, 9, 11, 9)
        Me.Text = "About Ember Media Manager"
        Me.Panel1.ResumeLayout(False)
        CType(Me.pbYouTube, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbXBMC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFFMPEG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pbFFMPEG As System.Windows.Forms.PictureBox
    Friend WithEvents pbMI As System.Windows.Forms.PictureBox
    Friend WithEvents pbXBMC As System.Windows.Forms.PictureBox
    Public WithEvents picDisplay As System.Windows.Forms.PictureBox
    Friend WithEvents pbYouTube As System.Windows.Forms.PictureBox

End Class
