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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnable = New System.Windows.Forms.CheckBox()
        Me.chkCascade = New System.Windows.Forms.CheckBox()
        Me.gbItems = New System.Windows.Forms.GroupBox()
        Me.tblItems = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScanFolder = New System.Windows.Forms.CheckBox()
        Me.chkAddMovieSource = New System.Windows.Forms.CheckBox()
        Me.chkAddTVShowSource = New System.Windows.Forms.CheckBox()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        Me.gbItems.SuspendLayout()
        Me.tblItems.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(297, 153)
        Me.pnlMain.TabIndex = 0
        '
        'tblMain
        '
        Me.tblMain.AutoScroll = True
        Me.tblMain.AutoSize = True
        Me.tblMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMain.ColumnCount = 2
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.chkEnable, 0, 0)
        Me.tblMain.Controls.Add(Me.chkCascade, 0, 1)
        Me.tblMain.Controls.Add(Me.gbItems, 0, 2)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 4
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(297, 153)
        Me.tblMain.TabIndex = 0
        '
        'chkEnable
        '
        Me.chkEnable.AutoSize = True
        Me.chkEnable.Location = New System.Drawing.Point(3, 3)
        Me.chkEnable.Name = "chkEnable"
        Me.chkEnable.Size = New System.Drawing.Size(281, 17)
        Me.chkEnable.TabIndex = 0
        Me.chkEnable.Text = "Integrate Ember Media Manager to shell context menu"
        Me.chkEnable.UseVisualStyleBackColor = True
        '
        'chkCascade
        '
        Me.chkCascade.AutoSize = True
        Me.chkCascade.Enabled = False
        Me.chkCascade.Location = New System.Drawing.Point(3, 26)
        Me.chkCascade.Name = "chkCascade"
        Me.chkCascade.Size = New System.Drawing.Size(135, 17)
        Me.chkCascade.TabIndex = 1
        Me.chkCascade.Text = "Cascade context menu"
        Me.chkCascade.UseVisualStyleBackColor = True
        '
        'gbItems
        '
        Me.gbItems.AutoSize = True
        Me.gbItems.Controls.Add(Me.tblItems)
        Me.gbItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbItems.Enabled = False
        Me.gbItems.Location = New System.Drawing.Point(3, 49)
        Me.gbItems.Name = "gbItems"
        Me.gbItems.Size = New System.Drawing.Size(281, 88)
        Me.gbItems.TabIndex = 4
        Me.gbItems.TabStop = False
        Me.gbItems.Text = "Context menu items:"
        '
        'tblItems
        '
        Me.tblItems.AutoSize = True
        Me.tblItems.ColumnCount = 2
        Me.tblItems.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblItems.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblItems.Controls.Add(Me.chkScanFolder, 0, 2)
        Me.tblItems.Controls.Add(Me.chkAddMovieSource, 0, 0)
        Me.tblItems.Controls.Add(Me.chkAddTVShowSource, 0, 1)
        Me.tblItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblItems.Location = New System.Drawing.Point(3, 16)
        Me.tblItems.Name = "tblItems"
        Me.tblItems.RowCount = 4
        Me.tblItems.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblItems.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblItems.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblItems.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblItems.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblItems.Size = New System.Drawing.Size(275, 69)
        Me.tblItems.TabIndex = 0
        '
        'chkScanFolder
        '
        Me.chkScanFolder.AutoSize = True
        Me.chkScanFolder.Location = New System.Drawing.Point(3, 49)
        Me.chkScanFolder.Name = "chkScanFolder"
        Me.chkScanFolder.Size = New System.Drawing.Size(157, 17)
        Me.chkScanFolder.TabIndex = 0
        Me.chkScanFolder.Text = "Scan folder for new content"
        Me.chkScanFolder.UseVisualStyleBackColor = True
        '
        'chkAddMovieSource
        '
        Me.chkAddMovieSource.AutoSize = True
        Me.chkAddMovieSource.Location = New System.Drawing.Point(3, 3)
        Me.chkAddMovieSource.Name = "chkAddMovieSource"
        Me.chkAddMovieSource.Size = New System.Drawing.Size(172, 17)
        Me.chkAddMovieSource.TabIndex = 1
        Me.chkAddMovieSource.Text = "Add folder as a movie source..."
        Me.chkAddMovieSource.UseVisualStyleBackColor = True
        '
        'chkAddTVShowSource
        '
        Me.chkAddTVShowSource.AutoSize = True
        Me.chkAddTVShowSource.Location = New System.Drawing.Point(3, 26)
        Me.chkAddTVShowSource.Name = "chkAddTVShowSource"
        Me.chkAddTVShowSource.Size = New System.Drawing.Size(181, 17)
        Me.chkAddTVShowSource.TabIndex = 2
        Me.chkAddTVShowSource.Text = "Add folder as a tv show source..."
        Me.chkAddTVShowSource.UseVisualStyleBackColor = True
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(297, 153)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmContextMenu"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        Me.gbItems.ResumeLayout(False)
        Me.gbItems.PerformLayout()
        Me.tblItems.ResumeLayout(False)
        Me.tblItems.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents tblMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkEnable As System.Windows.Forms.CheckBox
    Friend WithEvents chkCascade As System.Windows.Forms.CheckBox
    Friend WithEvents gbItems As System.Windows.Forms.GroupBox
    Friend WithEvents tblItems As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkScanFolder As System.Windows.Forms.CheckBox
    Friend WithEvents chkAddMovieSource As System.Windows.Forms.CheckBox
    Friend WithEvents chkAddTVShowSource As System.Windows.Forms.CheckBox

End Class
