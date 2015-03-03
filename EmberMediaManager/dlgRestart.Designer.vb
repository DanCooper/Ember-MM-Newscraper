<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgRestart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgRestart))
        Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblBody = New System.Windows.Forms.Label()
        Me.pnlRestart = New System.Windows.Forms.Panel()
        Me.tlpButtons.SuspendLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRestart.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpButtons
        '
        Me.tlpButtons.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpButtons.ColumnCount = 2
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Controls.Add(Me.OK_Button, 0, 0)
        Me.tlpButtons.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.tlpButtons.Location = New System.Drawing.Point(179, 105)
        Me.tlpButtons.Name = "tlpButtons"
        Me.tlpButtons.RowCount = 1
        Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Size = New System.Drawing.Size(206, 29)
        Me.tlpButtons.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(18, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Yes"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(121, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'pbLogo
        '
        Me.pbLogo.Image = CType(resources.GetObject("pbLogo.Image"), System.Drawing.Image)
        Me.pbLogo.Location = New System.Drawing.Point(10, 12)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(64, 64)
        Me.pbLogo.TabIndex = 1
        Me.pbLogo.TabStop = False
        '
        'lblHeader
        '
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.Location = New System.Drawing.Point(81, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(398, 29)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Restart Ember Media Manager?"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblBody
        '
        Me.lblBody.Location = New System.Drawing.Point(83, 38)
        Me.lblBody.Name = "lblBody"
        Me.lblBody.Size = New System.Drawing.Size(395, 52)
        Me.lblBody.TabIndex = 0
        Me.lblBody.Text = "Recent changes require a restart of Ember Media Manager to complete." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Would you" & _
    " like to restart Ember Media Manager now?"
        Me.lblBody.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlRestart
        '
        Me.pnlRestart.BackColor = System.Drawing.Color.White
        Me.pnlRestart.Controls.Add(Me.lblBody)
        Me.pnlRestart.Controls.Add(Me.lblHeader)
        Me.pnlRestart.Controls.Add(Me.pbLogo)
        Me.pnlRestart.Location = New System.Drawing.Point(1, 2)
        Me.pnlRestart.Name = "pnlRestart"
        Me.pnlRestart.Size = New System.Drawing.Size(489, 97)
        Me.pnlRestart.TabIndex = 1
        '
        'dlgRestart
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(492, 140)
        Me.Controls.Add(Me.pnlRestart)
        Me.Controls.Add(Me.tlpButtons)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgRestart"
        Me.ShowInTaskbar = False
        Me.Text = "Restart Ember Media Manager?"
        Me.tlpButtons.ResumeLayout(False)
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRestart.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents pbLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblBody As System.Windows.Forms.Label
    Friend WithEvents pnlRestart As System.Windows.Forms.Panel

End Class
