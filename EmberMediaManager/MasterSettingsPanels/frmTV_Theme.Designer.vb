<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTV_Theme
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
        Me.gbTVThemeOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVThemeOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTVShowThemeKeepExisting = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbTVThemeOpts.SuspendLayout()
        Me.tblTVThemeOpts.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(800, 450)
        Me.pnlSettings.TabIndex = 24
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbTVThemeOpts, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 450)
        Me.tblSettings.TabIndex = 4
        '
        'gbTVThemeOpts
        '
        Me.gbTVThemeOpts.AutoSize = True
        Me.gbTVThemeOpts.Controls.Add(Me.tblTVThemeOpts)
        Me.gbTVThemeOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVThemeOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbTVThemeOpts.Name = "gbTVThemeOpts"
        Me.gbTVThemeOpts.Size = New System.Drawing.Size(106, 44)
        Me.gbTVThemeOpts.TabIndex = 2
        Me.gbTVThemeOpts.TabStop = False
        Me.gbTVThemeOpts.Text = "Themes"
        '
        'tblTVThemeOpts
        '
        Me.tblTVThemeOpts.AutoSize = True
        Me.tblTVThemeOpts.ColumnCount = 2
        Me.tblTVThemeOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVThemeOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVThemeOpts.Controls.Add(Me.chkTVShowThemeKeepExisting, 0, 0)
        Me.tblTVThemeOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVThemeOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVThemeOpts.Name = "tblTVThemeOpts"
        Me.tblTVThemeOpts.RowCount = 2
        Me.tblTVThemeOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVThemeOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVThemeOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVThemeOpts.Size = New System.Drawing.Size(100, 23)
        Me.tblTVThemeOpts.TabIndex = 3
        '
        'chkTVShowThemeKeepExisting
        '
        Me.chkTVShowThemeKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVShowThemeKeepExisting.AutoSize = True
        Me.chkTVShowThemeKeepExisting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVShowThemeKeepExisting.Location = New System.Drawing.Point(3, 3)
        Me.chkTVShowThemeKeepExisting.Name = "chkTVShowThemeKeepExisting"
        Me.chkTVShowThemeKeepExisting.Size = New System.Drawing.Size(94, 17)
        Me.chkTVShowThemeKeepExisting.TabIndex = 4
        Me.chkTVShowThemeKeepExisting.Text = "Keep existing"
        Me.chkTVShowThemeKeepExisting.UseVisualStyleBackColor = True
        '
        'frmTV_Theme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmTV_Theme"
        Me.Text = "frmTV_Theme"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbTVThemeOpts.ResumeLayout(False)
        Me.gbTVThemeOpts.PerformLayout()
        Me.tblTVThemeOpts.ResumeLayout(False)
        Me.tblTVThemeOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbTVThemeOpts As GroupBox
    Friend WithEvents tblTVThemeOpts As TableLayoutPanel
    Friend WithEvents chkTVShowThemeKeepExisting As CheckBox
End Class
