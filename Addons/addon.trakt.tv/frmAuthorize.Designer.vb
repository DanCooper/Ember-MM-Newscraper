<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAuthorize
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAuthorize))
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtAutorizeURL = New System.Windows.Forms.TextBox()
        Me.txtPIN = New System.Windows.Forms.TextBox()
        Me.lblPIN = New System.Windows.Forms.Label()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.tblAuthorize = New System.Windows.Forms.TableLayoutPanel()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlAuthorize = New System.Windows.Forms.Panel()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblAuthorize.SuspendLayout()
        Me.pnlAuthorize.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOpen
        '
        Me.btnOpen.AutoSize = True
        Me.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOpen.Location = New System.Drawing.Point(397, 57)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(96, 23)
        Me.btnOpen.TabIndex = 0
        Me.btnOpen.Text = "Open In Browser"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.AutoSize = True
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Enabled = False
        Me.btnOK.Location = New System.Drawing.Point(397, 86)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(96, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtAutorizeURL
        '
        Me.tblAuthorize.SetColumnSpan(Me.txtAutorizeURL, 3)
        Me.txtAutorizeURL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAutorizeURL.Location = New System.Drawing.Point(3, 57)
        Me.txtAutorizeURL.Name = "txtAutorizeURL"
        Me.txtAutorizeURL.ReadOnly = True
        Me.txtAutorizeURL.Size = New System.Drawing.Size(388, 20)
        Me.txtAutorizeURL.TabIndex = 1
        '
        'txtPIN
        '
        Me.txtPIN.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.txtPIN.Location = New System.Drawing.Point(291, 87)
        Me.txtPIN.Name = "txtPIN"
        Me.txtPIN.Size = New System.Drawing.Size(100, 20)
        Me.txtPIN.TabIndex = 2
        '
        'lblPIN
        '
        Me.lblPIN.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblPIN.AutoSize = True
        Me.lblPIN.Location = New System.Drawing.Point(232, 91)
        Me.lblPIN.Name = "lblPIN"
        Me.lblPIN.Size = New System.Drawing.Size(53, 13)
        Me.lblPIN.TabIndex = 3
        Me.lblPIN.Text = "PIN Code"
        '
        'pbLogo
        '
        Me.pbLogo.Image = Global.addon.trakt.tv.My.Resources.Resources.logo
        Me.pbLogo.Location = New System.Drawing.Point(3, 3)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbLogo.TabIndex = 4
        Me.pbLogo.TabStop = False
        '
        'tblAuthorize
        '
        Me.tblAuthorize.AutoSize = True
        Me.tblAuthorize.BackColor = System.Drawing.Color.White
        Me.tblAuthorize.ColumnCount = 5
        Me.tblAuthorize.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblAuthorize.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblAuthorize.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblAuthorize.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblAuthorize.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblAuthorize.Controls.Add(Me.btnOK, 3, 2)
        Me.tblAuthorize.Controls.Add(Me.txtPIN, 2, 2)
        Me.tblAuthorize.Controls.Add(Me.lblPIN, 1, 2)
        Me.tblAuthorize.Controls.Add(Me.btnOpen, 3, 1)
        Me.tblAuthorize.Controls.Add(Me.pbLogo, 0, 0)
        Me.tblAuthorize.Controls.Add(Me.lblInfo, 1, 0)
        Me.tblAuthorize.Controls.Add(Me.txtAutorizeURL, 0, 1)
        Me.tblAuthorize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblAuthorize.Location = New System.Drawing.Point(0, 0)
        Me.tblAuthorize.Name = "tblAuthorize"
        Me.tblAuthorize.RowCount = 4
        Me.tblAuthorize.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblAuthorize.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblAuthorize.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblAuthorize.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblAuthorize.Size = New System.Drawing.Size(496, 116)
        Me.tblAuthorize.TabIndex = 5
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInfo.AutoSize = True
        Me.tblAuthorize.SetColumnSpan(Me.lblInfo, 3)
        Me.lblInfo.Location = New System.Drawing.Point(57, 20)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(431, 13)
        Me.lblInfo.TabIndex = 5
        Me.lblInfo.Text = "The Trakt addon CAN NOT be used without authorizing it to access your trakt.tv ac" &
    "count."
        '
        'pnlAuthorize
        '
        Me.pnlAuthorize.AutoSize = True
        Me.pnlAuthorize.Controls.Add(Me.tblAuthorize)
        Me.pnlAuthorize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAuthorize.Location = New System.Drawing.Point(0, 0)
        Me.pnlAuthorize.Name = "pnlAuthorize"
        Me.pnlAuthorize.Size = New System.Drawing.Size(496, 116)
        Me.pnlAuthorize.TabIndex = 6
        '
        'frmAuthorize
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(496, 116)
        Me.Controls.Add(Me.pnlAuthorize)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAuthorize"
        Me.Text = "Trakt Account Authorization"
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblAuthorize.ResumeLayout(False)
        Me.tblAuthorize.PerformLayout()
        Me.pnlAuthorize.ResumeLayout(False)
        Me.pnlAuthorize.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnOpen As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents txtAutorizeURL As TextBox
    Friend WithEvents txtPIN As TextBox
    Friend WithEvents lblPIN As Label
    Friend WithEvents pbLogo As PictureBox
    Friend WithEvents tblAuthorize As TableLayoutPanel
    Friend WithEvents lblInfo As Label
    Friend WithEvents pnlAuthorize As Panel
End Class
