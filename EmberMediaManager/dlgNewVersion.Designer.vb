<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgNewVersion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgNewVersion))
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.txtChangelog = New System.Windows.Forms.TextBox()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.lblNew = New System.Windows.Forms.Label()
        Me.llblClick = New System.Windows.Forms.LinkLabel()
        Me.lblVisit = New System.Windows.Forms.Label()
        Me.btnUpgrade = New System.Windows.Forms.Button()
        Me.pnlUpgrade = New System.Windows.Forms.Panel()
        Me.btnNo = New System.Windows.Forms.Button()
        Me.btnYes = New System.Windows.Forms.Button()
        Me.prbUpgrade = New System.Windows.Forms.ProgressBar()
        Me.lblStart = New System.Windows.Forms.Label()
        Me.lblUpgrade = New System.Windows.Forms.Label()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUpgrade.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(457, 390)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'txtChangelog
        '
        Me.txtChangelog.AcceptsReturn = True
        Me.txtChangelog.AcceptsTab = True
        Me.txtChangelog.BackColor = System.Drawing.Color.White
        Me.txtChangelog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChangelog.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtChangelog.Location = New System.Drawing.Point(9, 96)
        Me.txtChangelog.Multiline = True
        Me.txtChangelog.Name = "txtChangelog"
        Me.txtChangelog.ReadOnly = True
        Me.txtChangelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtChangelog.Size = New System.Drawing.Size(515, 282)
        Me.txtChangelog.TabIndex = 2
        '
        'pbLogo
        '
        Me.pbLogo.Image = CType(resources.GetObject("pbLogo.Image"), System.Drawing.Image)
        Me.pbLogo.Location = New System.Drawing.Point(9, 9)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(75, 78)
        Me.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbLogo.TabIndex = 10
        Me.pbLogo.TabStop = False
        '
        'lblNew
        '
        Me.lblNew.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblNew.Location = New System.Drawing.Point(247, 27)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(277, 43)
        Me.lblNew.TabIndex = 3
        Me.lblNew.Text = "Version r{0} is now available."
        Me.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'llblClick
        '
        Me.llblClick.AutoSize = True
        Me.llblClick.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.llblClick.Location = New System.Drawing.Point(5, 391)
        Me.llblClick.Name = "llblClick"
        Me.llblClick.Size = New System.Drawing.Size(87, 21)
        Me.llblClick.TabIndex = 5
        Me.llblClick.TabStop = True
        Me.llblClick.Text = "Click Here"
        '
        'lblVisit
        '
        Me.lblVisit.AutoSize = True
        Me.lblVisit.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVisit.Location = New System.Drawing.Point(93, 391)
        Me.lblVisit.Name = "lblVisit"
        Me.lblVisit.Size = New System.Drawing.Size(169, 21)
        Me.lblVisit.TabIndex = 6
        Me.lblVisit.Text = "to visit embermm.com."
        '
        'btnUpgrade
        '
        Me.btnUpgrade.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnUpgrade.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnUpgrade.Location = New System.Drawing.Point(386, 390)
        Me.btnUpgrade.Name = "btnUpgrade"
        Me.btnUpgrade.Size = New System.Drawing.Size(67, 23)
        Me.btnUpgrade.TabIndex = 0
        Me.btnUpgrade.Text = "Upgrade"
        '
        'pnlUpgrade
        '
        Me.pnlUpgrade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlUpgrade.Controls.Add(Me.btnNo)
        Me.pnlUpgrade.Controls.Add(Me.btnYes)
        Me.pnlUpgrade.Controls.Add(Me.prbUpgrade)
        Me.pnlUpgrade.Controls.Add(Me.lblStart)
        Me.pnlUpgrade.Controls.Add(Me.lblUpgrade)
        Me.pnlUpgrade.Location = New System.Drawing.Point(60, 122)
        Me.pnlUpgrade.Name = "pnlUpgrade"
        Me.pnlUpgrade.Size = New System.Drawing.Size(400, 152)
        Me.pnlUpgrade.TabIndex = 4
        Me.pnlUpgrade.Visible = False
        '
        'btnNo
        '
        Me.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnNo.Location = New System.Drawing.Point(201, 123)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(58, 22)
        Me.btnNo.TabIndex = 4
        Me.btnNo.Text = "NO"
        Me.btnNo.UseVisualStyleBackColor = True
        Me.btnNo.Visible = False
        '
        'btnYes
        '
        Me.btnYes.Location = New System.Drawing.Point(137, 123)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(58, 22)
        Me.btnYes.TabIndex = 3
        Me.btnYes.Text = "YES"
        Me.btnYes.UseVisualStyleBackColor = True
        Me.btnYes.Visible = False
        '
        'prbUpgrade
        '
        Me.prbUpgrade.Location = New System.Drawing.Point(3, 37)
        Me.prbUpgrade.Name = "prbUpgrade"
        Me.prbUpgrade.Size = New System.Drawing.Size(394, 13)
        Me.prbUpgrade.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbUpgrade.TabIndex = 2
        '
        'lblStart
        '
        Me.lblStart.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStart.Location = New System.Drawing.Point(12, 11)
        Me.lblStart.Name = "lblStart"
        Me.lblStart.Size = New System.Drawing.Size(369, 23)
        Me.lblStart.TabIndex = 0
        Me.lblStart.Text = "Preparing for upgrade ..."
        Me.lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUpgrade
        '
        Me.lblUpgrade.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUpgrade.Location = New System.Drawing.Point(3, 11)
        Me.lblUpgrade.Name = "lblUpgrade"
        Me.lblUpgrade.Size = New System.Drawing.Size(394, 109)
        Me.lblUpgrade.TabIndex = 1
        Me.lblUpgrade.Text = "TEXT"
        Me.lblUpgrade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUpgrade.Visible = False
        '
        'dlgNewVersion
        '
        Me.AcceptButton = Me.btnUpgrade
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(536, 425)
        Me.Controls.Add(Me.pnlUpgrade)
        Me.Controls.Add(Me.btnUpgrade)
        Me.Controls.Add(Me.lblVisit)
        Me.Controls.Add(Me.llblClick)
        Me.Controls.Add(Me.lblNew)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.pbLogo)
        Me.Controls.Add(Me.txtChangelog)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgNewVersion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "A New Version Is Available"
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUpgrade.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents txtChangelog As System.Windows.Forms.TextBox
    Friend WithEvents pbLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblNew As System.Windows.Forms.Label
    Friend WithEvents llblClick As System.Windows.Forms.LinkLabel
    Friend WithEvents lblVisit As System.Windows.Forms.Label
    Friend WithEvents btnUpgrade As System.Windows.Forms.Button
    Friend WithEvents pnlUpgrade As System.Windows.Forms.Panel
    Friend WithEvents prbUpgrade As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStart As System.Windows.Forms.Label
    Friend WithEvents lblUpgrade As System.Windows.Forms.Label
    Friend WithEvents btnNo As System.Windows.Forms.Button
    Friend WithEvents btnYes As System.Windows.Forms.Button

End Class
