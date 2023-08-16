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
        Me.gbProxy = New System.Windows.Forms.GroupBox()
        Me.tblProxy = New System.Windows.Forms.TableLayoutPanel()
        Me.gbCredentials = New System.Windows.Forms.GroupBox()
        Me.tblCredentials = New System.Windows.Forms.TableLayoutPanel()
        Me.txtCredentialsDomain = New System.Windows.Forms.TextBox()
        Me.chkCredentialsEnabled = New System.Windows.Forms.CheckBox()
        Me.lblCredentialsDomain = New System.Windows.Forms.Label()
        Me.lblCredentialsUsername = New System.Windows.Forms.Label()
        Me.txtCredentialsPassword = New System.Windows.Forms.TextBox()
        Me.txtCredentialsUsername = New System.Windows.Forms.TextBox()
        Me.lblCredentialsPassword = New System.Windows.Forms.Label()
        Me.chkProxyEnabled = New System.Windows.Forms.CheckBox()
        Me.txtProxyPort = New System.Windows.Forms.TextBox()
        Me.lblProxyPort = New System.Windows.Forms.Label()
        Me.lblProxyURI = New System.Windows.Forms.Label()
        Me.txtProxyURI = New System.Windows.Forms.TextBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbProxy.SuspendLayout()
        Me.tblProxy.SuspendLayout()
        Me.gbCredentials.SuspendLayout()
        Me.tblCredentials.SuspendLayout()
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
        Me.tblSettings.Controls.Add(Me.gbProxy, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 450)
        Me.tblSettings.TabIndex = 1
        '
        'gbProxy
        '
        Me.gbProxy.AutoSize = True
        Me.gbProxy.Controls.Add(Me.tblProxy)
        Me.gbProxy.Location = New System.Drawing.Point(3, 3)
        Me.gbProxy.Name = "gbProxy"
        Me.gbProxy.Size = New System.Drawing.Size(362, 226)
        Me.gbProxy.TabIndex = 0
        Me.gbProxy.TabStop = False
        Me.gbProxy.Text = "Proxy"
        '
        'tblProxy
        '
        Me.tblProxy.AutoSize = True
        Me.tblProxy.ColumnCount = 4
        Me.tblProxy.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxy.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxy.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxy.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblProxy.Controls.Add(Me.gbCredentials, 0, 4)
        Me.tblProxy.Controls.Add(Me.chkProxyEnabled, 0, 0)
        Me.tblProxy.Controls.Add(Me.txtProxyPort, 1, 3)
        Me.tblProxy.Controls.Add(Me.lblProxyPort, 0, 3)
        Me.tblProxy.Controls.Add(Me.lblProxyURI, 0, 1)
        Me.tblProxy.Controls.Add(Me.txtProxyURI, 0, 2)
        Me.tblProxy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblProxy.Location = New System.Drawing.Point(3, 18)
        Me.tblProxy.Name = "tblProxy"
        Me.tblProxy.RowCount = 6
        Me.tblProxy.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxy.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblProxy.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxy.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxy.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxy.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblProxy.Size = New System.Drawing.Size(356, 205)
        Me.tblProxy.TabIndex = 1
        '
        'gbCredentials
        '
        Me.gbCredentials.AutoSize = True
        Me.tblProxy.SetColumnSpan(Me.gbCredentials, 3)
        Me.gbCredentials.Controls.Add(Me.tblCredentials)
        Me.gbCredentials.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbCredentials.Enabled = False
        Me.gbCredentials.Location = New System.Drawing.Point(3, 102)
        Me.gbCredentials.Name = "gbCredentials"
        Me.gbCredentials.Size = New System.Drawing.Size(350, 100)
        Me.gbCredentials.TabIndex = 5
        Me.gbCredentials.TabStop = False
        Me.gbCredentials.Text = "Credentials"
        '
        'tblCredentials
        '
        Me.tblCredentials.AutoSize = True
        Me.tblCredentials.ColumnCount = 5
        Me.tblCredentials.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCredentials.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCredentials.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCredentials.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCredentials.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblCredentials.Controls.Add(Me.txtCredentialsDomain, 1, 2)
        Me.tblCredentials.Controls.Add(Me.chkCredentialsEnabled, 0, 0)
        Me.tblCredentials.Controls.Add(Me.lblCredentialsDomain, 0, 2)
        Me.tblCredentials.Controls.Add(Me.lblCredentialsUsername, 0, 1)
        Me.tblCredentials.Controls.Add(Me.txtCredentialsPassword, 3, 1)
        Me.tblCredentials.Controls.Add(Me.txtCredentialsUsername, 1, 1)
        Me.tblCredentials.Controls.Add(Me.lblCredentialsPassword, 2, 1)
        Me.tblCredentials.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCredentials.Location = New System.Drawing.Point(3, 18)
        Me.tblCredentials.Name = "tblCredentials"
        Me.tblCredentials.RowCount = 4
        Me.tblCredentials.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCredentials.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCredentials.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCredentials.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblCredentials.Size = New System.Drawing.Size(344, 79)
        Me.tblCredentials.TabIndex = 1
        '
        'txtCredentialsDomain
        '
        Me.txtCredentialsDomain.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblCredentials.SetColumnSpan(Me.txtCredentialsDomain, 3)
        Me.txtCredentialsDomain.Enabled = False
        Me.txtCredentialsDomain.Location = New System.Drawing.Point(70, 54)
        Me.txtCredentialsDomain.Name = "txtCredentialsDomain"
        Me.txtCredentialsDomain.Size = New System.Drawing.Size(271, 22)
        Me.txtCredentialsDomain.TabIndex = 6
        '
        'chkCredentialsEnabled
        '
        Me.chkCredentialsEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCredentialsEnabled.AutoSize = True
        Me.tblCredentials.SetColumnSpan(Me.chkCredentialsEnabled, 4)
        Me.chkCredentialsEnabled.Location = New System.Drawing.Point(3, 3)
        Me.chkCredentialsEnabled.Name = "chkCredentialsEnabled"
        Me.chkCredentialsEnabled.Size = New System.Drawing.Size(122, 17)
        Me.chkCredentialsEnabled.TabIndex = 0
        Me.chkCredentialsEnabled.Text = "Enable Credentials"
        Me.chkCredentialsEnabled.UseVisualStyleBackColor = True
        '
        'lblCredentialsDomain
        '
        Me.lblCredentialsDomain.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCredentialsDomain.AutoSize = True
        Me.lblCredentialsDomain.Location = New System.Drawing.Point(3, 58)
        Me.lblCredentialsDomain.Name = "lblCredentialsDomain"
        Me.lblCredentialsDomain.Size = New System.Drawing.Size(50, 13)
        Me.lblCredentialsDomain.TabIndex = 5
        Me.lblCredentialsDomain.Text = "Domain:"
        '
        'lblCredentialsUsername
        '
        Me.lblCredentialsUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCredentialsUsername.AutoSize = True
        Me.lblCredentialsUsername.Location = New System.Drawing.Point(3, 30)
        Me.lblCredentialsUsername.Name = "lblCredentialsUsername"
        Me.lblCredentialsUsername.Size = New System.Drawing.Size(61, 13)
        Me.lblCredentialsUsername.TabIndex = 1
        Me.lblCredentialsUsername.Text = "Username:"
        '
        'txtCredentialsPassword
        '
        Me.txtCredentialsPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtCredentialsPassword.Enabled = False
        Me.txtCredentialsPassword.Location = New System.Drawing.Point(241, 26)
        Me.txtCredentialsPassword.Name = "txtCredentialsPassword"
        Me.txtCredentialsPassword.Size = New System.Drawing.Size(100, 22)
        Me.txtCredentialsPassword.TabIndex = 4
        Me.txtCredentialsPassword.UseSystemPasswordChar = True
        '
        'txtCredentialsUsername
        '
        Me.txtCredentialsUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtCredentialsUsername.Enabled = False
        Me.txtCredentialsUsername.Location = New System.Drawing.Point(70, 26)
        Me.txtCredentialsUsername.Name = "txtCredentialsUsername"
        Me.txtCredentialsUsername.Size = New System.Drawing.Size(100, 22)
        Me.txtCredentialsUsername.TabIndex = 2
        '
        'lblCredentialsPassword
        '
        Me.lblCredentialsPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCredentialsPassword.AutoSize = True
        Me.lblCredentialsPassword.Location = New System.Drawing.Point(176, 30)
        Me.lblCredentialsPassword.Name = "lblCredentialsPassword"
        Me.lblCredentialsPassword.Size = New System.Drawing.Size(59, 13)
        Me.lblCredentialsPassword.TabIndex = 3
        Me.lblCredentialsPassword.Text = "Password:"
        '
        'chkProxyEnabled
        '
        Me.chkProxyEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkProxyEnabled.AutoSize = True
        Me.chkProxyEnabled.Location = New System.Drawing.Point(3, 3)
        Me.chkProxyEnabled.Name = "chkProxyEnabled"
        Me.chkProxyEnabled.Size = New System.Drawing.Size(91, 17)
        Me.chkProxyEnabled.TabIndex = 0
        Me.chkProxyEnabled.Text = "Enable Proxy"
        Me.chkProxyEnabled.UseVisualStyleBackColor = True
        '
        'txtProxyPort
        '
        Me.txtProxyPort.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtProxyPort.Enabled = False
        Me.txtProxyPort.Location = New System.Drawing.Point(100, 74)
        Me.txtProxyPort.Name = "txtProxyPort"
        Me.txtProxyPort.Size = New System.Drawing.Size(51, 22)
        Me.txtProxyPort.TabIndex = 4
        '
        'lblProxyPort
        '
        Me.lblProxyPort.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblProxyPort.AutoSize = True
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
        Me.lblProxyURI.Location = New System.Drawing.Point(3, 26)
        Me.lblProxyURI.Name = "lblProxyURI"
        Me.lblProxyURI.Size = New System.Drawing.Size(58, 13)
        Me.lblProxyURI.TabIndex = 1
        Me.lblProxyURI.Text = "Proxy URI:"
        '
        'txtProxyURI
        '
        Me.txtProxyURI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblProxy.SetColumnSpan(Me.txtProxyURI, 3)
        Me.txtProxyURI.Enabled = False
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
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmOption_Connection"
        Me.Text = "frmOption_Proxy"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbProxy.ResumeLayout(False)
        Me.gbProxy.PerformLayout()
        Me.tblProxy.ResumeLayout(False)
        Me.tblProxy.PerformLayout()
        Me.gbCredentials.ResumeLayout(False)
        Me.gbCredentials.PerformLayout()
        Me.tblCredentials.ResumeLayout(False)
        Me.tblCredentials.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbProxy As GroupBox
    Friend WithEvents tblProxy As TableLayoutPanel
    Friend WithEvents gbCredentials As GroupBox
    Friend WithEvents tblCredentials As TableLayoutPanel
    Friend WithEvents txtCredentialsDomain As TextBox
    Friend WithEvents chkCredentialsEnabled As CheckBox
    Friend WithEvents lblCredentialsDomain As Label
    Friend WithEvents lblCredentialsUsername As Label
    Friend WithEvents txtCredentialsPassword As TextBox
    Friend WithEvents txtCredentialsUsername As TextBox
    Friend WithEvents lblCredentialsPassword As Label
    Friend WithEvents chkProxyEnabled As CheckBox
    Friend WithEvents txtProxyPort As TextBox
    Friend WithEvents lblProxyPort As Label
    Friend WithEvents lblProxyURI As Label
    Friend WithEvents txtProxyURI As TextBox
End Class
