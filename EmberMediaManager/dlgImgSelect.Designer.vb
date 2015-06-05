<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgImgSelect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgImgSelect))
        Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.pnlBottomMain = New System.Windows.Forms.Panel()
        Me.lblDL2 = New System.Windows.Forms.Label()
        Me.pnlDLStatus = New System.Windows.Forms.Panel()
        Me.pnlDwld = New System.Windows.Forms.Panel()
        Me.lblDL1Status = New System.Windows.Forms.Label()
        Me.lblDL1 = New System.Windows.Forms.Label()
        Me.pbDL1 = New System.Windows.Forms.ProgressBar()
        Me.pnlBG = New System.Windows.Forms.Panel()
        Me.tlpButtons.SuspendLayout()
        Me.pnlBottomMain.SuspendLayout()
        Me.pnlDLStatus.SuspendLayout()
        Me.pnlDwld.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpButtons
        '
        Me.tlpButtons.ColumnCount = 2
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Controls.Add(Me.OK_Button, 0, 0)
        Me.tlpButtons.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.tlpButtons.Dock = System.Windows.Forms.DockStyle.Right
        Me.tlpButtons.Location = New System.Drawing.Point(690, 0)
        Me.tlpButtons.Name = "tlpButtons"
        Me.tlpButtons.RowCount = 1
        Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Size = New System.Drawing.Size(146, 64)
        Me.tlpButtons.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Enabled = False
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(3, 13)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 37)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 13)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 37)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'pnlBottomMain
        '
        Me.pnlBottomMain.Controls.Add(Me.tlpButtons)
        Me.pnlBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottomMain.Location = New System.Drawing.Point(0, 627)
        Me.pnlBottomMain.Name = "pnlBottomMain"
        Me.pnlBottomMain.Size = New System.Drawing.Size(836, 64)
        Me.pnlBottomMain.TabIndex = 0
        '
        'lblDL2
        '
        Me.lblDL2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDL2.Location = New System.Drawing.Point(5, 10)
        Me.lblDL2.Name = "lblDL2"
        Me.lblDL2.Size = New System.Drawing.Size(310, 13)
        Me.lblDL2.TabIndex = 7
        Me.lblDL2.Text = "Performing Preliminary Tasks..."
        '
        'pnlDLStatus
        '
        Me.pnlDLStatus.BackColor = System.Drawing.Color.White
        Me.pnlDLStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDLStatus.Controls.Add(Me.pnlDwld)
        Me.pnlDLStatus.Location = New System.Drawing.Point(265, 177)
        Me.pnlDLStatus.Name = "pnlDLStatus"
        Me.pnlDLStatus.Size = New System.Drawing.Size(331, 85)
        Me.pnlDLStatus.TabIndex = 0
        Me.pnlDLStatus.Visible = False
        '
        'pnlDwld
        '
        Me.pnlDwld.Controls.Add(Me.lblDL1Status)
        Me.pnlDwld.Controls.Add(Me.lblDL1)
        Me.pnlDwld.Controls.Add(Me.pbDL1)
        Me.pnlDwld.Location = New System.Drawing.Point(3, 3)
        Me.pnlDwld.Name = "pnlDwld"
        Me.pnlDwld.Size = New System.Drawing.Size(321, 75)
        Me.pnlDwld.TabIndex = 10
        '
        'lblDL1Status
        '
        Me.lblDL1Status.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDL1Status.Location = New System.Drawing.Point(5, 34)
        Me.lblDL1Status.Name = "lblDL1Status"
        Me.lblDL1Status.Size = New System.Drawing.Size(310, 13)
        Me.lblDL1Status.TabIndex = 8
        '
        'lblDL1
        '
        Me.lblDL1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDL1.Location = New System.Drawing.Point(5, 10)
        Me.lblDL1.Name = "lblDL1"
        Me.lblDL1.Size = New System.Drawing.Size(310, 13)
        Me.lblDL1.TabIndex = 7
        Me.lblDL1.Text = "Performing Preliminary Tasks..."
        '
        'pbDL1
        '
        Me.pbDL1.Location = New System.Drawing.Point(6, 52)
        Me.pbDL1.Name = "pbDL1"
        Me.pbDL1.Size = New System.Drawing.Size(309, 19)
        Me.pbDL1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbDL1.TabIndex = 6
        '
        'pnlBG
        '
        Me.pnlBG.AutoScroll = True
        Me.pnlBG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBG.Location = New System.Drawing.Point(0, 0)
        Me.pnlBG.Name = "pnlBG"
        Me.pnlBG.Size = New System.Drawing.Size(836, 627)
        Me.pnlBG.TabIndex = 2
        Me.pnlBG.Visible = False
        '
        'dlgImgSelect
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(836, 691)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlDLStatus)
        Me.Controls.Add(Me.pnlBG)
        Me.Controls.Add(Me.pnlBottomMain)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgImgSelect"
        Me.Text = "Select Poster"
        Me.tlpButtons.ResumeLayout(False)
        Me.pnlBottomMain.ResumeLayout(False)
        Me.pnlDLStatus.ResumeLayout(False)
        Me.pnlDwld.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents pnlBottomMain As System.Windows.Forms.Panel
    Friend WithEvents lblDL2 As System.Windows.Forms.Label
    Friend WithEvents pnlDLStatus As System.Windows.Forms.Panel
    Friend WithEvents pnlDwld As System.Windows.Forms.Panel
    Friend WithEvents lblDL1Status As System.Windows.Forms.Label
    Friend WithEvents lblDL1 As System.Windows.Forms.Label
    Friend WithEvents pbDL1 As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlBG As System.Windows.Forms.Panel

End Class
