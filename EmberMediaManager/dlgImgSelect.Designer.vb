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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.pnlBottomMain = New System.Windows.Forms.Panel()
        Me.cbFilterSize = New System.Windows.Forms.ComboBox()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.pnlSize = New System.Windows.Forms.Panel()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.rbSmall = New System.Windows.Forms.RadioButton()
        Me.rbMedium = New System.Windows.Forms.RadioButton()
        Me.rbLarge = New System.Windows.Forms.RadioButton()
        Me.rbXLarge = New System.Windows.Forms.RadioButton()
        Me.lblDL2 = New System.Windows.Forms.Label()
        Me.pnlDLStatus = New System.Windows.Forms.Panel()
        Me.pnlDwld = New System.Windows.Forms.Panel()
        Me.lblDL1Status = New System.Windows.Forms.Label()
        Me.lblDL1 = New System.Windows.Forms.Label()
        Me.pbDL1 = New System.Windows.Forms.ProgressBar()
        Me.pnlBG = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlBottomMain.SuspendLayout()
        Me.pnlSize.SuspendLayout()
        Me.pnlDLStatus.SuspendLayout()
        Me.pnlDwld.SuspendLayout()
        Me.pnlBG.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(690, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 64)
        Me.TableLayoutPanel1.TabIndex = 0
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
        Me.pnlBottomMain.Controls.Add(Me.cbFilterSize)
        Me.pnlBottomMain.Controls.Add(Me.lblSize)
        Me.pnlBottomMain.Controls.Add(Me.pnlSize)
        Me.pnlBottomMain.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlBottomMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottomMain.Location = New System.Drawing.Point(0, 606)
        Me.pnlBottomMain.Name = "pnlBottomMain"
        Me.pnlBottomMain.Size = New System.Drawing.Size(836, 64)
        Me.pnlBottomMain.TabIndex = 0
        '
        'cbFilterSize
        '
        Me.cbFilterSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFilterSize.Enabled = False
        Me.cbFilterSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbFilterSize.FormattingEnabled = True
        Me.cbFilterSize.Location = New System.Drawing.Point(147, 6)
        Me.cbFilterSize.Name = "cbFilterSize"
        Me.cbFilterSize.Size = New System.Drawing.Size(148, 21)
        Me.cbFilterSize.TabIndex = 5
        Me.cbFilterSize.Visible = False
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSize.Location = New System.Drawing.Point(6, 9)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(135, 13)
        Me.lblSize.TabIndex = 6
        Me.lblSize.Text = "Show the ones with size:"
        Me.lblSize.Visible = False
        '
        'pnlSize
        '
        Me.pnlSize.BackColor = System.Drawing.Color.White
        Me.pnlSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSize.Controls.Add(Me.btnPreview)
        Me.pnlSize.Controls.Add(Me.rbSmall)
        Me.pnlSize.Controls.Add(Me.rbMedium)
        Me.pnlSize.Controls.Add(Me.rbLarge)
        Me.pnlSize.Controls.Add(Me.rbXLarge)
        Me.pnlSize.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSize.Location = New System.Drawing.Point(0, 30)
        Me.pnlSize.Name = "pnlSize"
        Me.pnlSize.Size = New System.Drawing.Size(690, 34)
        Me.pnlSize.TabIndex = 4
        Me.pnlSize.Visible = False
        '
        'btnPreview
        '
        Me.btnPreview.Enabled = False
        Me.btnPreview.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnPreview.Image = CType(resources.GetObject("btnPreview.Image"), System.Drawing.Image)
        Me.btnPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPreview.Location = New System.Drawing.Point(593, 5)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 6
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'rbSmall
        '
        Me.rbSmall.AutoSize = True
        Me.rbSmall.Enabled = False
        Me.rbSmall.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbSmall.Location = New System.Drawing.Point(463, 8)
        Me.rbSmall.Name = "rbSmall"
        Me.rbSmall.Size = New System.Drawing.Size(124, 17)
        Me.rbSmall.TabIndex = 5
        Me.rbSmall.TabStop = True
        Me.rbSmall.Text = "Small (1920 x 1600)"
        Me.rbSmall.UseVisualStyleBackColor = True
        '
        'rbMedium
        '
        Me.rbMedium.AutoSize = True
        Me.rbMedium.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbMedium.Location = New System.Drawing.Point(161, 8)
        Me.rbMedium.Name = "rbMedium"
        Me.rbMedium.Size = New System.Drawing.Size(140, 17)
        Me.rbMedium.TabIndex = 4
        Me.rbMedium.TabStop = True
        Me.rbMedium.Text = "Medium (1920 x 1600)"
        Me.rbMedium.UseVisualStyleBackColor = True
        '
        'rbLarge
        '
        Me.rbLarge.AutoSize = True
        Me.rbLarge.Enabled = False
        Me.rbLarge.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbLarge.Location = New System.Drawing.Point(319, 8)
        Me.rbLarge.Name = "rbLarge"
        Me.rbLarge.Size = New System.Drawing.Size(126, 17)
        Me.rbLarge.TabIndex = 3
        Me.rbLarge.TabStop = True
        Me.rbLarge.Text = "Cover (1920 x 1600)"
        Me.rbLarge.UseVisualStyleBackColor = True
        '
        'rbXLarge
        '
        Me.rbXLarge.AutoSize = True
        Me.rbXLarge.Enabled = False
        Me.rbXLarge.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbXLarge.Location = New System.Drawing.Point(6, 8)
        Me.rbXLarge.Name = "rbXLarge"
        Me.rbXLarge.Size = New System.Drawing.Size(137, 17)
        Me.rbXLarge.TabIndex = 2
        Me.rbXLarge.TabStop = True
        Me.rbXLarge.Text = "Original (1920 x 1600)"
        Me.rbXLarge.UseVisualStyleBackColor = True
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
        Me.pnlBG.Controls.Add(Me.pnlDLStatus)
        Me.pnlBG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBG.Location = New System.Drawing.Point(0, 0)
        Me.pnlBG.Name = "pnlBG"
        Me.pnlBG.Size = New System.Drawing.Size(836, 606)
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
        Me.ClientSize = New System.Drawing.Size(836, 670)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlBG)
        Me.Controls.Add(Me.pnlBottomMain)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgImgSelect"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Poster"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pnlBottomMain.ResumeLayout(False)
        Me.pnlBottomMain.PerformLayout()
        Me.pnlSize.ResumeLayout(False)
        Me.pnlSize.PerformLayout()
        Me.pnlDLStatus.ResumeLayout(False)
        Me.pnlDwld.ResumeLayout(False)
        Me.pnlBG.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents pnlBottomMain As System.Windows.Forms.Panel
    Friend WithEvents pnlSize As System.Windows.Forms.Panel
    Friend WithEvents rbSmall As System.Windows.Forms.RadioButton
    Friend WithEvents rbMedium As System.Windows.Forms.RadioButton
    Friend WithEvents rbLarge As System.Windows.Forms.RadioButton
    Friend WithEvents rbXLarge As System.Windows.Forms.RadioButton
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents lblDL2 As System.Windows.Forms.Label
    Friend WithEvents pnlDLStatus As System.Windows.Forms.Panel
    Friend WithEvents pnlDwld As System.Windows.Forms.Panel
    Friend WithEvents lblDL1Status As System.Windows.Forms.Label
    Friend WithEvents lblDL1 As System.Windows.Forms.Label
    Friend WithEvents pbDL1 As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlBG As System.Windows.Forms.Panel
    Friend WithEvents cbFilterSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblSize As System.Windows.Forms.Label

End Class
