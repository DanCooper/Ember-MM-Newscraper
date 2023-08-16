<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsPanel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsPanel))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGlobal = New System.Windows.Forms.GroupBox()
        Me.tblGlobal = New System.Windows.Forms.TableLayoutPanel()
        Me.pbApiKey = New System.Windows.Forms.PictureBox()
        Me.lblApiKey = New System.Windows.Forms.Label()
        Me.txtApiKey = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbGlobal.SuspendLayout()
        Me.tblGlobal.SuspendLayout()
        CType(Me.pbApiKey, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(487, 450)
        Me.pnlSettings.TabIndex = 0
        '
        'tblSettings
        '
        Me.tblSettings.AutoSize = True
        Me.tblSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbGlobal, 0, 0)
        Me.tblSettings.Controls.Add(Me.pbLogo, 0, 1)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 3
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(487, 450)
        Me.tblSettings.TabIndex = 0
        '
        'gbGlobal
        '
        Me.gbGlobal.AutoSize = True
        Me.gbGlobal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbGlobal.Controls.Add(Me.tblGlobal)
        Me.gbGlobal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGlobal.Location = New System.Drawing.Point(3, 3)
        Me.gbGlobal.Name = "gbGlobal"
        Me.gbGlobal.Size = New System.Drawing.Size(351, 49)
        Me.gbGlobal.TabIndex = 0
        Me.gbGlobal.TabStop = False
        Me.gbGlobal.Text = "Global"
        '
        'tblGlobal
        '
        Me.tblGlobal.AutoSize = True
        Me.tblGlobal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblGlobal.ColumnCount = 3
        Me.tblGlobal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGlobal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGlobal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGlobal.Controls.Add(Me.pbApiKey, 2, 0)
        Me.tblGlobal.Controls.Add(Me.lblApiKey, 0, 0)
        Me.tblGlobal.Controls.Add(Me.txtApiKey, 1, 0)
        Me.tblGlobal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGlobal.Location = New System.Drawing.Point(3, 18)
        Me.tblGlobal.Name = "tblGlobal"
        Me.tblGlobal.RowCount = 1
        Me.tblGlobal.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGlobal.Size = New System.Drawing.Size(345, 28)
        Me.tblGlobal.TabIndex = 0
        '
        'pbApiKey
        '
        Me.pbApiKey.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbApiKey.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbApiKey.Image = CType(resources.GetObject("pbApiKey.Image"), System.Drawing.Image)
        Me.pbApiKey.Location = New System.Drawing.Point(326, 6)
        Me.pbApiKey.Name = "pbApiKey"
        Me.pbApiKey.Size = New System.Drawing.Size(16, 16)
        Me.pbApiKey.TabIndex = 6
        Me.pbApiKey.TabStop = False
        '
        'lblApiKey
        '
        Me.lblApiKey.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblApiKey.AutoSize = True
        Me.lblApiKey.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApiKey.Location = New System.Drawing.Point(3, 7)
        Me.lblApiKey.Name = "lblApiKey"
        Me.lblApiKey.Size = New System.Drawing.Size(81, 13)
        Me.lblApiKey.TabIndex = 1
        Me.lblApiKey.Text = "TMDb Api Key:"
        '
        'txtApiKey
        '
        Me.txtApiKey.Location = New System.Drawing.Point(90, 3)
        Me.txtApiKey.Name = "txtApiKey"
        Me.txtApiKey.Size = New System.Drawing.Size(230, 22)
        Me.txtApiKey.TabIndex = 2
        Me.txtApiKey.WatermarkColor = System.Drawing.Color.Gray
        Me.txtApiKey.WatermarkText = "Ember Media Manager Embedded API Key"
        '
        'pbLogo
        '
        Me.pbLogo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbLogo.Image = Global.addon.themoviedb.org.My.Resources.Resources._408x161_powered_by_rectangle_blue
        Me.pbLogo.Location = New System.Drawing.Point(3, 58)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(351, 50)
        Me.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbLogo.TabIndex = 1
        Me.pbLogo.TabStop = False
        '
        'frmSettingsPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(487, 450)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmSettingsPanel"
        Me.Text = "frmSettingsPanel"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbGlobal.ResumeLayout(False)
        Me.gbGlobal.PerformLayout()
        Me.tblGlobal.ResumeLayout(False)
        Me.tblGlobal.PerformLayout()
        CType(Me.pbApiKey, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbGlobal As GroupBox
    Friend WithEvents tblGlobal As TableLayoutPanel
    Friend WithEvents lblApiKey As Label
    Friend WithEvents pbApiKey As PictureBox
    Friend WithEvents txtApiKey As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents pbLogo As PictureBox
End Class
