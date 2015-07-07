<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsHolder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsHolder))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.gbSettingsGeneral = New System.Windows.Forms.GroupBox()
        Me.chkNotification = New System.Windows.Forms.CheckBox()
        Me.btnEditHost = New System.Windows.Forms.Button()
        Me.btnRemoveHost = New System.Windows.Forms.Button()
        Me.lbHosts = New System.Windows.Forms.ListBox()
        Me.btnAddHost = New System.Windows.Forms.Button()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.gbSettingsGeneral.SuspendLayout()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(713, 491)
        Me.pnlSettings.TabIndex = 84
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.gbSettingsGeneral)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(713, 468)
        Me.pnlSettingsMain.TabIndex = 5
        '
        'gbSettingsGeneral
        '
        Me.gbSettingsGeneral.AutoSize = True
        Me.gbSettingsGeneral.Controls.Add(Me.chkNotification)
        Me.gbSettingsGeneral.Controls.Add(Me.btnEditHost)
        Me.gbSettingsGeneral.Controls.Add(Me.btnRemoveHost)
        Me.gbSettingsGeneral.Controls.Add(Me.lbHosts)
        Me.gbSettingsGeneral.Controls.Add(Me.btnAddHost)
        Me.gbSettingsGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSettingsGeneral.Location = New System.Drawing.Point(0, 0)
        Me.gbSettingsGeneral.Name = "gbSettingsGeneral"
        Me.gbSettingsGeneral.Size = New System.Drawing.Size(713, 468)
        Me.gbSettingsGeneral.TabIndex = 0
        Me.gbSettingsGeneral.TabStop = False
        Me.gbSettingsGeneral.Text = "General Settings"
        '
        'chkNotification
        '
        Me.chkNotification.AutoSize = True
        Me.chkNotification.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNotification.Location = New System.Drawing.Point(318, 21)
        Me.chkNotification.Name = "chkNotification"
        Me.chkNotification.Size = New System.Drawing.Size(121, 17)
        Me.chkNotification.TabIndex = 15
        Me.chkNotification.Text = "Send Notifications"
        Me.chkNotification.UseVisualStyleBackColor = True
        '
        'btnEditHost
        '
        Me.btnEditHost.Enabled = False
        Me.btnEditHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditHost.Image = CType(resources.GetObject("btnEditHost.Image"), System.Drawing.Image)
        Me.btnEditHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditHost.Location = New System.Drawing.Point(202, 188)
        Me.btnEditHost.Name = "btnEditHost"
        Me.btnEditHost.Size = New System.Drawing.Size(91, 23)
        Me.btnEditHost.TabIndex = 11
        Me.btnEditHost.Text = "Edit"
        Me.btnEditHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditHost.UseVisualStyleBackColor = True
        '
        'btnRemoveHost
        '
        Me.btnRemoveHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveHost.Image = CType(resources.GetObject("btnRemoveHost.Image"), System.Drawing.Image)
        Me.btnRemoveHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveHost.Location = New System.Drawing.Point(109, 188)
        Me.btnRemoveHost.Name = "btnRemoveHost"
        Me.btnRemoveHost.Size = New System.Drawing.Size(87, 23)
        Me.btnRemoveHost.TabIndex = 10
        Me.btnRemoveHost.Text = "Remove"
        Me.btnRemoveHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemoveHost.UseVisualStyleBackColor = True
        '
        'lbHosts
        '
        Me.lbHosts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbHosts.FormattingEnabled = True
        Me.lbHosts.Location = New System.Drawing.Point(16, 21)
        Me.lbHosts.Name = "lbHosts"
        Me.lbHosts.Size = New System.Drawing.Size(283, 160)
        Me.lbHosts.Sorted = True
        Me.lbHosts.TabIndex = 8
        '
        'btnAddHost
        '
        Me.btnAddHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddHost.Image = CType(resources.GetObject("btnAddHost.Image"), System.Drawing.Image)
        Me.btnAddHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddHost.Location = New System.Drawing.Point(16, 188)
        Me.btnAddHost.Name = "btnAddHost"
        Me.btnAddHost.Size = New System.Drawing.Size(87, 23)
        Me.btnAddHost.TabIndex = 9
        Me.btnAddHost.Text = "Add"
        Me.btnAddHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddHost.UseVisualStyleBackColor = True
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(713, 23)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 2
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.chkEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(713, 23)
        Me.tblSettingsTop.TabIndex = 5
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 3)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(713, 491)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings for Kodi Interface"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.gbSettingsGeneral.ResumeLayout(False)
        Me.gbSettingsGeneral.PerformLayout()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbSettingsGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents chkNotification As System.Windows.Forms.CheckBox
    Friend WithEvents btnEditHost As System.Windows.Forms.Button
    Friend WithEvents btnRemoveHost As System.Windows.Forms.Button
    Friend WithEvents lbHosts As System.Windows.Forms.ListBox
    Friend WithEvents btnAddHost As System.Windows.Forms.Button
End Class
