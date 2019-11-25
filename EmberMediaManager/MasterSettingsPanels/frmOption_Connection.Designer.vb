<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOption_Connection
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbProxyOpts = New System.Windows.Forms.GroupBox()
        Me.tblProxyOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.gbProxyCredsOpts = New System.Windows.Forms.GroupBox()
        Me.tblProxyCredsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.txtProxyDomain = New System.Windows.Forms.TextBox()
        Me.chkProxyCredsEnable = New System.Windows.Forms.CheckBox()
        Me.lblProxyDomain = New System.Windows.Forms.Label()
        Me.lblProxyUsername = New System.Windows.Forms.Label()
        Me.txtProxyPassword = New System.Windows.Forms.TextBox()
        Me.txtProxyUsername = New System.Windows.Forms.TextBox()
        Me.lblProxyPassword = New System.Windows.Forms.Label()
        Me.chkProxyEnable = New System.Windows.Forms.CheckBox()
        Me.txtProxyPort = New System.Windows.Forms.TextBox()
        Me.lblProxyPort = New System.Windows.Forms.Label()
        Me.lblProxyURI = New System.Windows.Forms.Label()
        Me.txtProxyURI = New System.Windows.Forms.TextBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbProxyOpts.SuspendLayout()
        Me.tblProxyOpts.SuspendLayout()
        Me.gbProxyCredsOpts.SuspendLayout()
        Me.tblProxyCredsOpts.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(800, 450)
        Me.pnlSettings.TabIndex = 19
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbProxyOpts, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 450)
        Me.tblSettings.TabIndex = 1
        '
        'gbProxyOpts
        '
        Me.gbProxyOpts.AutoSize = True
        Me.gbProxyOpts.Controls.Add(Me.tblProxyOpts)
        Me.gbProxyOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbProxyOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbProxyOpts.Name = "gbProxyOpts"
        Me.gbProxyOpts.Size = New System.Drawing.Size(362, 226)
        Me.gbProxyOpts.TabIndex = 0
        Me.gbProxyOpts.TabStop = False
        Me.gbProxyOpts.Text = "Proxy"
        '
        'tblProxyOpts
        '
        Me.tblProxyOpts.AutoSize = True
        Me.tblProxyOpts.ColumnCount = 4
        Me.tblProxyOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyOpts.Controls.Add(Me.gbProxyCredsOpts, 0, 4)
        Me.tblProxyOpts.Controls.Add(Me.chkProxyEnable, 0, 0)
        Me.tblProxyOpts.Controls.Add(Me.txtProxyPort, 1, 3)
        Me.tblProxyOpts.Controls.Add(Me.lblProxyPort, 0, 3)
        Me.tblProxyOpts.Controls.Add(Me.lblProxyURI, 0, 1)
        Me.tblProxyOpts.Controls.Add(Me.txtProxyURI, 0, 2)
        Me.tblProxyOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblProxyOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblProxyOpts.Name = "tblProxyOpts"
        Me.tblProxyOpts.RowCount = 6
        Me.tblProxyOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblProxyOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyOpts.Size = New System.Drawing.Size(356, 205)
        Me.tblProxyOpts.TabIndex = 1
        '
        'gbProxyCredsOpts
        '
        Me.gbProxyCredsOpts.AutoSize = True
        Me.tblProxyOpts.SetColumnSpan(Me.gbProxyCredsOpts, 3)
        Me.gbProxyCredsOpts.Controls.Add(Me.tblProxyCredsOpts)
        Me.gbProxyCredsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbProxyCredsOpts.Enabled = False
        Me.gbProxyCredsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbProxyCredsOpts.Location = New System.Drawing.Point(3, 102)
        Me.gbProxyCredsOpts.Name = "gbProxyCredsOpts"
        Me.gbProxyCredsOpts.Size = New System.Drawing.Size(350, 100)
        Me.gbProxyCredsOpts.TabIndex = 5
        Me.gbProxyCredsOpts.TabStop = False
        Me.gbProxyCredsOpts.Text = "Credentials"
        '
        'tblProxyCredsOpts
        '
        Me.tblProxyCredsOpts.AutoSize = True
        Me.tblProxyCredsOpts.ColumnCount = 5
        Me.tblProxyCredsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyCredsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyCredsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyCredsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyCredsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxyCredsOpts.Controls.Add(Me.txtProxyDomain, 1, 2)
        Me.tblProxyCredsOpts.Controls.Add(Me.chkProxyCredsEnable, 0, 0)
        Me.tblProxyCredsOpts.Controls.Add(Me.lblProxyDomain, 0, 2)
        Me.tblProxyCredsOpts.Controls.Add(Me.lblProxyUsername, 0, 1)
        Me.tblProxyCredsOpts.Controls.Add(Me.txtProxyPassword, 3, 1)
        Me.tblProxyCredsOpts.Controls.Add(Me.txtProxyUsername, 1, 1)
        Me.tblProxyCredsOpts.Controls.Add(Me.lblProxyPassword, 2, 1)
        Me.tblProxyCredsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblProxyCredsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblProxyCredsOpts.Name = "tblProxyCredsOpts"
        Me.tblProxyCredsOpts.RowCount = 4
        Me.tblProxyCredsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyCredsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyCredsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyCredsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxyCredsOpts.Size = New System.Drawing.Size(344, 79)
        Me.tblProxyCredsOpts.TabIndex = 1
        '
        'txtProxyDomain
        '
        Me.txtProxyDomain.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblProxyCredsOpts.SetColumnSpan(Me.txtProxyDomain, 3)
        Me.txtProxyDomain.Enabled = False
        Me.txtProxyDomain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyDomain.Location = New System.Drawing.Point(70, 54)
        Me.txtProxyDomain.Name = "txtProxyDomain"
        Me.txtProxyDomain.Size = New System.Drawing.Size(271, 22)
        Me.txtProxyDomain.TabIndex = 6
        '
        'chkProxyCredsEnable
        '
        Me.chkProxyCredsEnable.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkProxyCredsEnable.AutoSize = True
        Me.tblProxyCredsOpts.SetColumnSpan(Me.chkProxyCredsEnable, 4)
        Me.chkProxyCredsEnable.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProxyCredsEnable.Location = New System.Drawing.Point(3, 3)
        Me.chkProxyCredsEnable.Name = "chkProxyCredsEnable"
        Me.chkProxyCredsEnable.Size = New System.Drawing.Size(122, 17)
        Me.chkProxyCredsEnable.TabIndex = 0
        Me.chkProxyCredsEnable.Text = "Enable Credentials"
        Me.chkProxyCredsEnable.UseVisualStyleBackColor = True
        '
        'lblProxyDomain
        '
        Me.lblProxyDomain.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblProxyDomain.AutoSize = True
        Me.lblProxyDomain.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyDomain.Location = New System.Drawing.Point(3, 58)
        Me.lblProxyDomain.Name = "lblProxyDomain"
        Me.lblProxyDomain.Size = New System.Drawing.Size(50, 13)
        Me.lblProxyDomain.TabIndex = 5
        Me.lblProxyDomain.Text = "Domain:"
        '
        'lblProxyUsername
        '
        Me.lblProxyUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblProxyUsername.AutoSize = True
        Me.lblProxyUsername.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyUsername.Location = New System.Drawing.Point(3, 30)
        Me.lblProxyUsername.Name = "lblProxyUsername"
        Me.lblProxyUsername.Size = New System.Drawing.Size(61, 13)
        Me.lblProxyUsername.TabIndex = 1
        Me.lblProxyUsername.Text = "Username:"
        '
        'txtProxyPassword
        '
        Me.txtProxyPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtProxyPassword.Enabled = False
        Me.txtProxyPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyPassword.Location = New System.Drawing.Point(241, 26)
        Me.txtProxyPassword.Name = "txtProxyPassword"
        Me.txtProxyPassword.Size = New System.Drawing.Size(100, 22)
        Me.txtProxyPassword.TabIndex = 4
        Me.txtProxyPassword.UseSystemPasswordChar = True
        '
        'txtProxyUsername
        '
        Me.txtProxyUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtProxyUsername.Enabled = False
        Me.txtProxyUsername.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyUsername.Location = New System.Drawing.Point(70, 26)
        Me.txtProxyUsername.Name = "txtProxyUsername"
        Me.txtProxyUsername.Size = New System.Drawing.Size(100, 22)
        Me.txtProxyUsername.TabIndex = 2
        '
        'lblProxyPassword
        '
        Me.lblProxyPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblProxyPassword.AutoSize = True
        Me.lblProxyPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyPassword.Location = New System.Drawing.Point(176, 30)
        Me.lblProxyPassword.Name = "lblProxyPassword"
        Me.lblProxyPassword.Size = New System.Drawing.Size(59, 13)
        Me.lblProxyPassword.TabIndex = 3
        Me.lblProxyPassword.Text = "Password:"
        '
        'chkProxyEnable
        '
        Me.chkProxyEnable.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkProxyEnable.AutoSize = True
        Me.chkProxyEnable.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProxyEnable.Location = New System.Drawing.Point(3, 3)
        Me.chkProxyEnable.Name = "chkProxyEnable"
        Me.chkProxyEnable.Size = New System.Drawing.Size(91, 17)
        Me.chkProxyEnable.TabIndex = 0
        Me.chkProxyEnable.Text = "Enable Proxy"
        Me.chkProxyEnable.UseVisualStyleBackColor = True
        '
        'txtProxyPort
        '
        Me.txtProxyPort.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtProxyPort.Enabled = False
        Me.txtProxyPort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyPort.Location = New System.Drawing.Point(100, 74)
        Me.txtProxyPort.Name = "txtProxyPort"
        Me.txtProxyPort.Size = New System.Drawing.Size(51, 22)
        Me.txtProxyPort.TabIndex = 4
        '
        'lblProxyPort
        '
        Me.lblProxyPort.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblProxyPort.AutoSize = True
        Me.lblProxyPort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyPort.Location = New System.Drawing.Point(3, 78)
        Me.lblProxyPort.Name = "lblProxyPort"
        Me.lblProxyPort.Size = New System.Drawing.Size(61, 13)
        Me.lblProxyPort.TabIndex = 3
        Me.lblProxyPort.Text = "Proxy Port:"
        '
        'lblProxyURI
        '
        Me.lblProxyURI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblProxyURI.AutoSize = True
        Me.lblProxyURI.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProxyURI.Location = New System.Drawing.Point(3, 26)
        Me.lblProxyURI.Name = "lblProxyURI"
        Me.lblProxyURI.Size = New System.Drawing.Size(58, 13)
        Me.lblProxyURI.TabIndex = 1
        Me.lblProxyURI.Text = "Proxy URI:"
        '
        'txtProxyURI
        '
        Me.txtProxyURI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblProxyOpts.SetColumnSpan(Me.txtProxyURI, 3)
        Me.txtProxyURI.Enabled = False
        Me.txtProxyURI.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProxyURI.Location = New System.Drawing.Point(3, 46)
        Me.txtProxyURI.Name = "txtProxyURI"
        Me.txtProxyURI.Size = New System.Drawing.Size(267, 22)
        Me.txtProxyURI.TabIndex = 2
        '
        'frmOption_Connection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmOption_Connection"
        Me.Text = "frmOption_Proxy"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbProxyOpts.ResumeLayout(False)
        Me.gbProxyOpts.PerformLayout()
        Me.tblProxyOpts.ResumeLayout(False)
        Me.tblProxyOpts.PerformLayout()
        Me.gbProxyCredsOpts.ResumeLayout(False)
        Me.gbProxyCredsOpts.PerformLayout()
        Me.tblProxyCredsOpts.ResumeLayout(False)
        Me.tblProxyCredsOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbProxyOpts As GroupBox
    Friend WithEvents tblProxyOpts As TableLayoutPanel
    Friend WithEvents gbProxyCredsOpts As GroupBox
    Friend WithEvents tblProxyCredsOpts As TableLayoutPanel
    Friend WithEvents txtProxyDomain As TextBox
    Friend WithEvents chkProxyCredsEnable As CheckBox
    Friend WithEvents lblProxyDomain As Label
    Friend WithEvents lblProxyUsername As Label
    Friend WithEvents txtProxyPassword As TextBox
    Friend WithEvents txtProxyUsername As TextBox
    Friend WithEvents lblProxyPassword As Label
    Friend WithEvents chkProxyEnable As CheckBox
    Friend WithEvents txtProxyPort As TextBox
    Friend WithEvents lblProxyPort As Label
    Friend WithEvents lblProxyURI As Label
    Friend WithEvents txtProxyURI As TextBox
End Class
