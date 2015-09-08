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
        Me.tbllSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSettingsGeneral = New System.Windows.Forms.GroupBox()
        Me.tblSettingsGeneral = New System.Windows.Forms.TableLayoutPanel()
        Me.lbHosts = New System.Windows.Forms.ListBox()
        Me.btnEditHost = New System.Windows.Forms.Button()
        Me.cbPlayCountHost = New System.Windows.Forms.ComboBox()
        Me.btnRemoveHost = New System.Windows.Forms.Button()
        Me.chkNotification = New System.Windows.Forms.CheckBox()
        Me.btnAddHost = New System.Windows.Forms.Button()
        Me.chkPlayCount = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tbllSettingsMain.SuspendLayout()
        Me.gbSettingsGeneral.SuspendLayout()
        Me.tblSettingsGeneral.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(508, 280)
        Me.pnlSettings.TabIndex = 84
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tbllSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(508, 257)
        Me.pnlSettingsMain.TabIndex = 5
        '
        'tbllSettingsMain
        '
        Me.tbllSettingsMain.AutoScroll = True
        Me.tbllSettingsMain.AutoSize = True
        Me.tbllSettingsMain.ColumnCount = 2
        Me.tbllSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tbllSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tbllSettingsMain.Controls.Add(Me.gbSettingsGeneral, 0, 0)
        Me.tbllSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbllSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tbllSettingsMain.Name = "tbllSettingsMain"
        Me.tbllSettingsMain.RowCount = 2
        Me.tbllSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tbllSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tbllSettingsMain.Size = New System.Drawing.Size(508, 257)
        Me.tbllSettingsMain.TabIndex = 1
        '
        'gbSettingsGeneral
        '
        Me.gbSettingsGeneral.AutoSize = True
        Me.gbSettingsGeneral.Controls.Add(Me.tblSettingsGeneral)
        Me.gbSettingsGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSettingsGeneral.Location = New System.Drawing.Point(3, 3)
        Me.gbSettingsGeneral.Name = "gbSettingsGeneral"
        Me.gbSettingsGeneral.Size = New System.Drawing.Size(470, 216)
        Me.gbSettingsGeneral.TabIndex = 0
        Me.gbSettingsGeneral.TabStop = False
        Me.gbSettingsGeneral.Text = "General Settings"
        '
        'tblSettingsGeneral
        '
        Me.tblSettingsGeneral.AutoSize = True
        Me.tblSettingsGeneral.ColumnCount = 5
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.Controls.Add(Me.lbHosts, 0, 0)
        Me.tblSettingsGeneral.Controls.Add(Me.btnEditHost, 2, 4)
        Me.tblSettingsGeneral.Controls.Add(Me.cbPlayCountHost, 3, 2)
        Me.tblSettingsGeneral.Controls.Add(Me.btnRemoveHost, 1, 4)
        Me.tblSettingsGeneral.Controls.Add(Me.chkNotification, 3, 0)
        Me.tblSettingsGeneral.Controls.Add(Me.btnAddHost, 0, 4)
        Me.tblSettingsGeneral.Controls.Add(Me.chkPlayCount, 3, 1)
        Me.tblSettingsGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsGeneral.Location = New System.Drawing.Point(3, 18)
        Me.tblSettingsGeneral.Name = "tblSettingsGeneral"
        Me.tblSettingsGeneral.RowCount = 6
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.Size = New System.Drawing.Size(464, 195)
        Me.tblSettingsGeneral.TabIndex = 87
        '
        'lbHosts
        '
        Me.tblSettingsGeneral.SetColumnSpan(Me.lbHosts, 3)
        Me.lbHosts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbHosts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbHosts.FormattingEnabled = True
        Me.lbHosts.Location = New System.Drawing.Point(3, 3)
        Me.lbHosts.Name = "lbHosts"
        Me.tblSettingsGeneral.SetRowSpan(Me.lbHosts, 4)
        Me.lbHosts.Size = New System.Drawing.Size(300, 160)
        Me.lbHosts.Sorted = True
        Me.lbHosts.TabIndex = 8
        '
        'btnEditHost
        '
        Me.btnEditHost.Enabled = False
        Me.btnEditHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditHost.Image = CType(resources.GetObject("btnEditHost.Image"), System.Drawing.Image)
        Me.btnEditHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditHost.Location = New System.Drawing.Point(189, 169)
        Me.btnEditHost.Name = "btnEditHost"
        Me.btnEditHost.Size = New System.Drawing.Size(91, 23)
        Me.btnEditHost.TabIndex = 11
        Me.btnEditHost.Text = "Edit"
        Me.btnEditHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditHost.UseVisualStyleBackColor = True
        '
        'cbPlayCountHost
        '
        Me.cbPlayCountHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbPlayCountHost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPlayCountHost.FormattingEnabled = True
        Me.cbPlayCountHost.Location = New System.Drawing.Point(309, 49)
        Me.cbPlayCountHost.Name = "cbPlayCountHost"
        Me.cbPlayCountHost.Size = New System.Drawing.Size(152, 21)
        Me.cbPlayCountHost.TabIndex = 86
        '
        'btnRemoveHost
        '
        Me.btnRemoveHost.Enabled = False
        Me.btnRemoveHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveHost.Image = CType(resources.GetObject("btnRemoveHost.Image"), System.Drawing.Image)
        Me.btnRemoveHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveHost.Location = New System.Drawing.Point(96, 169)
        Me.btnRemoveHost.Name = "btnRemoveHost"
        Me.btnRemoveHost.Size = New System.Drawing.Size(87, 23)
        Me.btnRemoveHost.TabIndex = 10
        Me.btnRemoveHost.Text = "Remove"
        Me.btnRemoveHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemoveHost.UseVisualStyleBackColor = True
        '
        'chkNotification
        '
        Me.chkNotification.AutoSize = True
        Me.chkNotification.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNotification.Location = New System.Drawing.Point(309, 3)
        Me.chkNotification.Name = "chkNotification"
        Me.chkNotification.Size = New System.Drawing.Size(121, 17)
        Me.chkNotification.TabIndex = 15
        Me.chkNotification.Text = "Send Notifications"
        Me.chkNotification.UseVisualStyleBackColor = True
        '
        'btnAddHost
        '
        Me.btnAddHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddHost.Image = CType(resources.GetObject("btnAddHost.Image"), System.Drawing.Image)
        Me.btnAddHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddHost.Location = New System.Drawing.Point(3, 169)
        Me.btnAddHost.Name = "btnAddHost"
        Me.btnAddHost.Size = New System.Drawing.Size(87, 23)
        Me.btnAddHost.TabIndex = 9
        Me.btnAddHost.Text = "Add"
        Me.btnAddHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddHost.UseVisualStyleBackColor = True
        '
        'chkPlayCount
        '
        Me.chkPlayCount.AutoSize = True
        Me.chkPlayCount.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPlayCount.Location = New System.Drawing.Point(309, 26)
        Me.chkPlayCount.Name = "chkPlayCount"
        Me.chkPlayCount.Size = New System.Drawing.Size(152, 17)
        Me.chkPlayCount.TabIndex = 85
        Me.chkPlayCount.Text = "Retrieve PlayCount from:"
        Me.chkPlayCount.UseVisualStyleBackColor = True
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(508, 23)
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(508, 23)
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
        Me.ClientSize = New System.Drawing.Size(508, 280)
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
        Me.tbllSettingsMain.ResumeLayout(False)
        Me.tbllSettingsMain.PerformLayout()
        Me.gbSettingsGeneral.ResumeLayout(False)
        Me.gbSettingsGeneral.PerformLayout()
        Me.tblSettingsGeneral.ResumeLayout(False)
        Me.tblSettingsGeneral.PerformLayout()
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
    Friend WithEvents cbPlayCountHost As System.Windows.Forms.ComboBox
    Friend WithEvents chkPlayCount As System.Windows.Forms.CheckBox
    Friend WithEvents tbllSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsGeneral As System.Windows.Forms.TableLayoutPanel
End Class
