<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMediaBrowser
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
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.chkVideoTSParent = New System.Windows.Forms.CheckBox()
        Me.chkBackdrop = New System.Windows.Forms.CheckBox()
        Me.chkMyMovies = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.lblInfo)
        Me.pnlSettings.Controls.Add(Me.chkVideoTSParent)
        Me.pnlSettings.Controls.Add(Me.chkBackdrop)
        Me.pnlSettings.Controls.Add(Me.chkMyMovies)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(643, 356)
        Me.pnlSettings.TabIndex = 0
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.Red
        Me.lblInfo.Location = New System.Drawing.Point(100, 171)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(443, 51)
        Me.lblInfo.TabIndex = 4
        Me.lblInfo.Text = "The development of this module has been stopped." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please make a request if you wa" & _
    "nt that this module will be developed." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "We need up to date information about the" & _
    " MediaBrowser."
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'chkVideoTSParent
        '
        Me.chkVideoTSParent.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkVideoTSParent.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVideoTSParent.Location = New System.Drawing.Point(13, 32)
        Me.chkVideoTSParent.Name = "chkVideoTSParent"
        Me.chkVideoTSParent.Size = New System.Drawing.Size(584, 17)
        Me.chkVideoTSParent.TabIndex = 1
        Me.chkVideoTSParent.Text = "Compatible VIDEO_TS File Placement/Naming"
        Me.chkVideoTSParent.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkVideoTSParent.UseVisualStyleBackColor = True
        '
        'chkBackdrop
        '
        Me.chkBackdrop.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBackdrop.Location = New System.Drawing.Point(13, 55)
        Me.chkBackdrop.Name = "chkBackdrop"
        Me.chkBackdrop.Size = New System.Drawing.Size(584, 18)
        Me.chkBackdrop.TabIndex = 2
        Me.chkBackdrop.Text = "Movie Fanart as backdrop.jpg"
        Me.chkBackdrop.UseVisualStyleBackColor = True
        '
        'chkMyMovies
        '
        Me.chkMyMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMyMovies.Location = New System.Drawing.Point(13, 79)
        Me.chkMyMovies.Name = "chkMyMovies"
        Me.chkMyMovies.Size = New System.Drawing.Size(584, 18)
        Me.chkMyMovies.TabIndex = 3
        Me.chkMyMovies.Text = "Media Browser mymovies.xml"
        Me.chkMyMovies.UseVisualStyleBackColor = True
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(643, 23)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'chkEnabled
        '
        Me.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 3)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(643, 23)
        Me.tblSettingsTop.TabIndex = 5
        '
        'frmMediaBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(643, 356)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmMediaBrowser"
        Me.Text = "frmSettingsHolder"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
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
    Friend WithEvents chkBackdrop As System.Windows.Forms.CheckBox
    Friend WithEvents chkMyMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkVideoTSParent As System.Windows.Forms.CheckBox
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
End Class
