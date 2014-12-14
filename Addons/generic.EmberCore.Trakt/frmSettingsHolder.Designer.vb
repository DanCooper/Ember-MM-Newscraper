<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraktSettingsHolder
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
        Me.pnlTraktSettingsTop = New System.Windows.Forms.Panel()
        Me.chkTraktEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlTraktSettings = New System.Windows.Forms.Panel()
        Me.gpb_TraktGeneralSettings = New System.Windows.Forms.GroupBox()
        Me.lblTraktPassword = New System.Windows.Forms.Label()
        Me.txtTraktPassword = New System.Windows.Forms.TextBox()
        Me.lblTraktUsername = New System.Windows.Forms.Label()
        Me.txtTraktUsername = New System.Windows.Forms.TextBox()
        Me.btnTraktSaveSettings = New System.Windows.Forms.Button()
        Me.pnlTraktSettingsTop.SuspendLayout()
        Me.pnlTraktSettings.SuspendLayout()
        Me.gpb_TraktGeneralSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTraktSettingsTop
        '
        Me.pnlTraktSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlTraktSettingsTop.Controls.Add(Me.chkTraktEnabled)
        Me.pnlTraktSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTraktSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTraktSettingsTop.Name = "pnlTraktSettingsTop"
        Me.pnlTraktSettingsTop.Size = New System.Drawing.Size(610, 25)
        Me.pnlTraktSettingsTop.TabIndex = 0
        '
        'chkTraktEnabled
        '
        Me.chkTraktEnabled.AutoSize = True
        Me.chkTraktEnabled.Location = New System.Drawing.Point(10, 5)
        Me.chkTraktEnabled.Name = "chkTraktEnabled"
        Me.chkTraktEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkTraktEnabled.TabIndex = 0
        Me.chkTraktEnabled.Text = "Enabled"
        Me.chkTraktEnabled.UseVisualStyleBackColor = True
        '
        'pnlTraktSettings
        '
        Me.pnlTraktSettings.Controls.Add(Me.gpb_TraktGeneralSettings)
        Me.pnlTraktSettings.Controls.Add(Me.pnlTraktSettingsTop)
        Me.pnlTraktSettings.Location = New System.Drawing.Point(3, 12)
        Me.pnlTraktSettings.Name = "pnlTraktSettings"
        Me.pnlTraktSettings.Size = New System.Drawing.Size(610, 387)
        Me.pnlTraktSettings.TabIndex = 0
        '
        'gpb_TraktGeneralSettings
        '
        Me.gpb_TraktGeneralSettings.Controls.Add(Me.lblTraktPassword)
        Me.gpb_TraktGeneralSettings.Controls.Add(Me.txtTraktPassword)
        Me.gpb_TraktGeneralSettings.Controls.Add(Me.lblTraktUsername)
        Me.gpb_TraktGeneralSettings.Controls.Add(Me.txtTraktUsername)
        Me.gpb_TraktGeneralSettings.Controls.Add(Me.btnTraktSaveSettings)
        Me.gpb_TraktGeneralSettings.Location = New System.Drawing.Point(9, 42)
        Me.gpb_TraktGeneralSettings.Name = "gpb_TraktGeneralSettings"
        Me.gpb_TraktGeneralSettings.Size = New System.Drawing.Size(552, 81)
        Me.gpb_TraktGeneralSettings.TabIndex = 20
        Me.gpb_TraktGeneralSettings.TabStop = False
        Me.gpb_TraktGeneralSettings.Text = "General Settings"
        '
        'lblTraktPassword
        '
        Me.lblTraktPassword.AutoSize = True
        Me.lblTraktPassword.Location = New System.Drawing.Point(9, 50)
        Me.lblTraktPassword.Name = "lblTraktPassword"
        Me.lblTraktPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblTraktPassword.TabIndex = 42
        Me.lblTraktPassword.Text = "Password"
        '
        'txtTraktPassword
        '
        Me.txtTraktPassword.Location = New System.Drawing.Point(93, 47)
        Me.txtTraktPassword.Name = "txtTraktPassword"
        Me.txtTraktPassword.Size = New System.Drawing.Size(167, 22)
        Me.txtTraktPassword.TabIndex = 41
        '
        'lblTraktUsername
        '
        Me.lblTraktUsername.AutoSize = True
        Me.lblTraktUsername.Location = New System.Drawing.Point(9, 24)
        Me.lblTraktUsername.Name = "lblTraktUsername"
        Me.lblTraktUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblTraktUsername.TabIndex = 40
        Me.lblTraktUsername.Text = "Username"
        '
        'txtTraktUsername
        '
        Me.txtTraktUsername.Location = New System.Drawing.Point(93, 21)
        Me.txtTraktUsername.Name = "txtTraktUsername"
        Me.txtTraktUsername.Size = New System.Drawing.Size(167, 22)
        Me.txtTraktUsername.TabIndex = 39
        '
        'btnTraktSaveSettings
        '
        Me.btnTraktSaveSettings.Location = New System.Drawing.Point(278, 21)
        Me.btnTraktSaveSettings.Name = "btnTraktSaveSettings"
        Me.btnTraktSaveSettings.Size = New System.Drawing.Size(75, 47)
        Me.btnTraktSaveSettings.TabIndex = 43
        Me.btnTraktSaveSettings.Text = "Save "
        Me.btnTraktSaveSettings.UseVisualStyleBackColor = True
        '
        'frmTraktSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(625, 411)
        Me.Controls.Add(Me.pnlTraktSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTraktSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmSettingsHolder"
        Me.pnlTraktSettingsTop.ResumeLayout(False)
        Me.pnlTraktSettingsTop.PerformLayout()
        Me.pnlTraktSettings.ResumeLayout(False)
        Me.gpb_TraktGeneralSettings.ResumeLayout(False)
        Me.gpb_TraktGeneralSettings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTraktSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkTraktEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlTraktSettings As System.Windows.Forms.Panel
    Friend WithEvents gpb_TraktGeneralSettings As System.Windows.Forms.GroupBox
    Friend WithEvents lblTraktPassword As System.Windows.Forms.Label
    Friend WithEvents txtTraktPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblTraktUsername As System.Windows.Forms.Label
    Friend WithEvents txtTraktUsername As System.Windows.Forms.TextBox
    Friend WithEvents btnTraktSaveSettings As System.Windows.Forms.Button

End Class
