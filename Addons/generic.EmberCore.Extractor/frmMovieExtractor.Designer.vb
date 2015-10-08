<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMovieExtractor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovieExtractor))
        Me.pnlExtrator = New System.Windows.Forms.Panel()
        Me.btnFrameSaveAsExtrafanart = New System.Windows.Forms.Button()
        Me.btnFrameSaveAsFanart = New System.Windows.Forms.Button()
        Me.gbAutoGenerate = New System.Windows.Forms.GroupBox()
        Me.txtThumbCount = New System.Windows.Forms.TextBox()
        Me.lblToCreate = New System.Windows.Forms.Label()
        Me.btnAutoGen = New System.Windows.Forms.Button()
        Me.btnFrameSaveAsExtrathumb = New System.Windows.Forms.Button()
        Me.pnlFrameProgress = New System.Windows.Forms.Panel()
        Me.lblExtractingFrame = New System.Windows.Forms.Label()
        Me.prbExtractingFrame = New System.Windows.Forms.ProgressBar()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.tbFrame = New System.Windows.Forms.TrackBar()
        Me.btnFrameLoad = New System.Windows.Forms.Button()
        Me.pbFrame = New System.Windows.Forms.PictureBox()
        Me.tmrDelay = New System.Windows.Forms.Timer(Me.components)
        Me.pnlExtrator.SuspendLayout()
        Me.gbAutoGenerate.SuspendLayout()
        Me.pnlFrameProgress.SuspendLayout()
        CType(Me.tbFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFrame, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlExtrator
        '
        Me.pnlExtrator.BackColor = System.Drawing.Color.White
        Me.pnlExtrator.Controls.Add(Me.btnFrameSaveAsExtrafanart)
        Me.pnlExtrator.Controls.Add(Me.btnFrameSaveAsFanart)
        Me.pnlExtrator.Controls.Add(Me.gbAutoGenerate)
        Me.pnlExtrator.Controls.Add(Me.btnFrameSaveAsExtrathumb)
        Me.pnlExtrator.Controls.Add(Me.pnlFrameProgress)
        Me.pnlExtrator.Controls.Add(Me.lblTime)
        Me.pnlExtrator.Controls.Add(Me.tbFrame)
        Me.pnlExtrator.Controls.Add(Me.btnFrameLoad)
        Me.pnlExtrator.Controls.Add(Me.pbFrame)
        Me.pnlExtrator.Location = New System.Drawing.Point(2, 2)
        Me.pnlExtrator.Name = "pnlExtrator"
        Me.pnlExtrator.Size = New System.Drawing.Size(834, 468)
        Me.pnlExtrator.TabIndex = 0
        '
        'btnFrameSaveAsEFanart
        '
        Me.btnFrameSaveAsExtrafanart.Enabled = False
        Me.btnFrameSaveAsExtrafanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFrameSaveAsExtrafanart.Image = CType(resources.GetObject("btnFrameSaveAsEFanart.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsExtrafanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsExtrafanart.Location = New System.Drawing.Point(735, 377)
        Me.btnFrameSaveAsExtrafanart.Name = "btnFrameSaveAsEFanart"
        Me.btnFrameSaveAsExtrafanart.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameSaveAsExtrafanart.TabIndex = 18
        Me.btnFrameSaveAsExtrafanart.Text = "Save as Extrafanart"
        Me.btnFrameSaveAsExtrafanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameSaveAsExtrafanart.UseVisualStyleBackColor = True
        '
        'btnFrameSaveAsFanart
        '
        Me.btnFrameSaveAsFanart.Enabled = False
        Me.btnFrameSaveAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFrameSaveAsFanart.Image = CType(resources.GetObject("btnFrameSaveAsFanart.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsFanart.Location = New System.Drawing.Point(735, 199)
        Me.btnFrameSaveAsFanart.Name = "btnFrameSaveAsFanart"
        Me.btnFrameSaveAsFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameSaveAsFanart.TabIndex = 17
        Me.btnFrameSaveAsFanart.Text = "Save as Fanart"
        Me.btnFrameSaveAsFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameSaveAsFanart.UseVisualStyleBackColor = True
        '
        'gbAutoGenerate
        '
        Me.gbAutoGenerate.Controls.Add(Me.txtThumbCount)
        Me.gbAutoGenerate.Controls.Add(Me.lblToCreate)
        Me.gbAutoGenerate.Controls.Add(Me.btnAutoGen)
        Me.gbAutoGenerate.Location = New System.Drawing.Point(734, 93)
        Me.gbAutoGenerate.Name = "gbAutoGenerate"
        Me.gbAutoGenerate.Size = New System.Drawing.Size(99, 100)
        Me.gbAutoGenerate.TabIndex = 1
        Me.gbAutoGenerate.TabStop = False
        Me.gbAutoGenerate.Text = "Auto-Generate"
        '
        'txtThumbCount
        '
        Me.txtThumbCount.Location = New System.Drawing.Point(68, 18)
        Me.txtThumbCount.Name = "txtThumbCount"
        Me.txtThumbCount.Size = New System.Drawing.Size(25, 22)
        Me.txtThumbCount.TabIndex = 1
        '
        'lblToCreate
        '
        Me.lblToCreate.AutoSize = True
        Me.lblToCreate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblToCreate.Location = New System.Drawing.Point(2, 23)
        Me.lblToCreate.Name = "lblToCreate"
        Me.lblToCreate.Size = New System.Drawing.Size(67, 13)
        Me.lblToCreate.TabIndex = 0
        Me.lblToCreate.Text = "# to Create:"
        '
        'btnAutoGen
        '
        Me.btnAutoGen.Enabled = False
        Me.btnAutoGen.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnAutoGen.Image = CType(resources.GetObject("btnAutoGen.Image"), System.Drawing.Image)
        Me.btnAutoGen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAutoGen.Location = New System.Drawing.Point(5, 49)
        Me.btnAutoGen.Name = "btnAutoGen"
        Me.btnAutoGen.Size = New System.Drawing.Size(89, 45)
        Me.btnAutoGen.TabIndex = 2
        Me.btnAutoGen.Text = "Auto-Gen"
        Me.btnAutoGen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAutoGen.UseVisualStyleBackColor = True
        '
        'btnFrameSaveAsEThumb
        '
        Me.btnFrameSaveAsExtrathumb.Enabled = False
        Me.btnFrameSaveAsExtrathumb.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFrameSaveAsExtrathumb.Image = CType(resources.GetObject("btnFrameSaveAsEThumb.Image"), System.Drawing.Image)
        Me.btnFrameSaveAsExtrathumb.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameSaveAsExtrathumb.Location = New System.Drawing.Point(735, 288)
        Me.btnFrameSaveAsExtrathumb.Name = "btnFrameSaveAsEThumb"
        Me.btnFrameSaveAsExtrathumb.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameSaveAsExtrathumb.TabIndex = 2
        Me.btnFrameSaveAsExtrathumb.Text = "Save as Extrathumb"
        Me.btnFrameSaveAsExtrathumb.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameSaveAsExtrathumb.UseVisualStyleBackColor = True
        '
        'pnlFrameProgress
        '
        Me.pnlFrameProgress.BackColor = System.Drawing.Color.White
        Me.pnlFrameProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFrameProgress.Controls.Add(Me.lblExtractingFrame)
        Me.pnlFrameProgress.Controls.Add(Me.prbExtractingFrame)
        Me.pnlFrameProgress.Location = New System.Drawing.Point(241, 173)
        Me.pnlFrameProgress.Name = "pnlFrameProgress"
        Me.pnlFrameProgress.Size = New System.Drawing.Size(252, 51)
        Me.pnlFrameProgress.TabIndex = 0
        Me.pnlFrameProgress.Visible = False
        '
        'lblExtractingFrame
        '
        Me.lblExtractingFrame.AutoSize = True
        Me.lblExtractingFrame.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblExtractingFrame.Location = New System.Drawing.Point(2, 7)
        Me.lblExtractingFrame.Name = "lblExtractingFrame"
        Me.lblExtractingFrame.Size = New System.Drawing.Size(103, 13)
        Me.lblExtractingFrame.TabIndex = 0
        Me.lblExtractingFrame.Text = "Extracting Frame..."
        '
        'prbExtractingFrame
        '
        Me.prbExtractingFrame.Location = New System.Drawing.Point(4, 26)
        Me.prbExtractingFrame.MarqueeAnimationSpeed = 25
        Me.prbExtractingFrame.Name = "prbExtractingFrame"
        Me.prbExtractingFrame.Size = New System.Drawing.Size(242, 16)
        Me.prbExtractingFrame.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbExtractingFrame.TabIndex = 1
        '
        'lblTime
        '
        Me.lblTime.Location = New System.Drawing.Point(671, 420)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(59, 23)
        Me.lblTime.TabIndex = 4
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbFrame
        '
        Me.tbFrame.BackColor = System.Drawing.Color.White
        Me.tbFrame.Enabled = False
        Me.tbFrame.Location = New System.Drawing.Point(6, 420)
        Me.tbFrame.Name = "tbFrame"
        Me.tbFrame.Size = New System.Drawing.Size(659, 45)
        Me.tbFrame.TabIndex = 3
        Me.tbFrame.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'btnFrameLoad
        '
        Me.btnFrameLoad.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFrameLoad.Image = CType(resources.GetObject("btnFrameLoad.Image"), System.Drawing.Image)
        Me.btnFrameLoad.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnFrameLoad.Location = New System.Drawing.Point(735, 4)
        Me.btnFrameLoad.Name = "btnFrameLoad"
        Me.btnFrameLoad.Size = New System.Drawing.Size(96, 83)
        Me.btnFrameLoad.TabIndex = 0
        Me.btnFrameLoad.Text = "Load Movie"
        Me.btnFrameLoad.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFrameLoad.UseVisualStyleBackColor = True
        '
        'pbFrame
        '
        Me.pbFrame.BackColor = System.Drawing.Color.DimGray
        Me.pbFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbFrame.Location = New System.Drawing.Point(6, 4)
        Me.pbFrame.Name = "pbFrame"
        Me.pbFrame.Size = New System.Drawing.Size(724, 414)
        Me.pbFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbFrame.TabIndex = 16
        Me.pbFrame.TabStop = False
        '
        'tmrDelay
        '
        Me.tmrDelay.Interval = 250
        '
        'frmMovieExtractor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(837, 471)
        Me.Controls.Add(Me.pnlExtrator)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMovieExtractor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmExtrator"
        Me.pnlExtrator.ResumeLayout(False)
        Me.pnlExtrator.PerformLayout()
        Me.gbAutoGenerate.ResumeLayout(False)
        Me.gbAutoGenerate.PerformLayout()
        Me.pnlFrameProgress.ResumeLayout(False)
        Me.pnlFrameProgress.PerformLayout()
        CType(Me.tbFrame, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFrame, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlExtrator As System.Windows.Forms.Panel
    Friend WithEvents gbAutoGenerate As System.Windows.Forms.GroupBox
    Friend WithEvents txtThumbCount As System.Windows.Forms.TextBox
    Friend WithEvents lblToCreate As System.Windows.Forms.Label
    Friend WithEvents btnAutoGen As System.Windows.Forms.Button
    Friend WithEvents btnFrameSaveAsExtrathumb As System.Windows.Forms.Button
    Friend WithEvents pnlFrameProgress As System.Windows.Forms.Panel
    Friend WithEvents lblExtractingFrame As System.Windows.Forms.Label
    Friend WithEvents prbExtractingFrame As System.Windows.Forms.ProgressBar
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents tbFrame As System.Windows.Forms.TrackBar
    Friend WithEvents btnFrameLoad As System.Windows.Forms.Button
    Friend WithEvents pbFrame As System.Windows.Forms.PictureBox
    Friend WithEvents tmrDelay As System.Windows.Forms.Timer
    Friend WithEvents btnFrameSaveAsFanart As System.Windows.Forms.Button
    Friend WithEvents btnFrameSaveAsExtrafanart As System.Windows.Forms.Button

End Class
