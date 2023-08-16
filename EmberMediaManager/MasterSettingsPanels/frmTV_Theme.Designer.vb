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
        Me.gbThemes = New System.Windows.Forms.GroupBox()
        Me.tblThemes = New System.Windows.Forms.TableLayoutPanel()
        Me.txtDefaultSearchParameter = New System.Windows.Forms.TextBox()
        Me.chkKeepExisting = New System.Windows.Forms.CheckBox()
        Me.lblDefaultSearchParameter = New System.Windows.Forms.Label()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbThemes.SuspendLayout()
        Me.tblThemes.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(223, 121)
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
        Me.tblSettings.Controls.Add(Me.gbThemes, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(223, 121)
        Me.tblSettings.TabIndex = 4
        '
        'gbThemes
        '
        Me.gbThemes.AutoSize = True
        Me.gbThemes.Controls.Add(Me.tblThemes)
        Me.gbThemes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbThemes.Location = New System.Drawing.Point(3, 3)
        Me.gbThemes.Name = "gbThemes"
        Me.gbThemes.Size = New System.Drawing.Size(194, 92)
        Me.gbThemes.TabIndex = 2
        Me.gbThemes.TabStop = False
        Me.gbThemes.Text = "Themes"
        '
        'tblThemes
        '
        Me.tblThemes.AutoSize = True
        Me.tblThemes.ColumnCount = 1
        Me.tblThemes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblThemes.Controls.Add(Me.txtDefaultSearchParameter, 0, 2)
        Me.tblThemes.Controls.Add(Me.chkKeepExisting, 0, 0)
        Me.tblThemes.Controls.Add(Me.lblDefaultSearchParameter, 0, 1)
        Me.tblThemes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblThemes.Location = New System.Drawing.Point(3, 18)
        Me.tblThemes.Name = "tblThemes"
        Me.tblThemes.RowCount = 3
        Me.tblThemes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblThemes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblThemes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblThemes.Size = New System.Drawing.Size(188, 71)
        Me.tblThemes.TabIndex = 3
        '
        'txtDefaultSearchParameter
        '
        Me.txtDefaultSearchParameter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtDefaultSearchParameter.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDefaultSearchParameter.Location = New System.Drawing.Point(3, 46)
        Me.txtDefaultSearchParameter.Name = "txtDefaultSearchParameter"
        Me.txtDefaultSearchParameter.Size = New System.Drawing.Size(182, 22)
        Me.txtDefaultSearchParameter.TabIndex = 13
        '
        'chkKeepExisting
        '
        Me.chkKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkKeepExisting.AutoSize = True
        Me.chkKeepExisting.Location = New System.Drawing.Point(3, 3)
        Me.chkKeepExisting.Name = "chkKeepExisting"
        Me.chkKeepExisting.Size = New System.Drawing.Size(94, 17)
        Me.chkKeepExisting.TabIndex = 4
        Me.chkKeepExisting.Text = "Keep existing"
        Me.chkKeepExisting.UseVisualStyleBackColor = True
        '
        'lblDefaultSearchParameter
        '
        Me.lblDefaultSearchParameter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDefaultSearchParameter.AutoSize = True
        Me.lblDefaultSearchParameter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDefaultSearchParameter.Location = New System.Drawing.Point(3, 26)
        Me.lblDefaultSearchParameter.Name = "lblDefaultSearchParameter"
        Me.lblDefaultSearchParameter.Size = New System.Drawing.Size(139, 13)
        Me.lblDefaultSearchParameter.TabIndex = 12
        Me.lblDefaultSearchParameter.Text = "Default Search Parameter:"
        '
        'frmTV_Theme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(223, 121)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmTV_Theme"
        Me.Text = "frmTV_Theme"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbThemes.ResumeLayout(False)
        Me.gbThemes.PerformLayout()
        Me.tblThemes.ResumeLayout(False)
        Me.tblThemes.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbThemes As GroupBox
    Friend WithEvents tblThemes As TableLayoutPanel
    Friend WithEvents chkKeepExisting As CheckBox
    Friend WithEvents lblDefaultSearchParameter As Label
    Friend WithEvents txtDefaultSearchParameter As TextBox
End Class
