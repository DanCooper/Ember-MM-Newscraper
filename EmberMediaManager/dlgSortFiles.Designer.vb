<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSortFiles
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.gbStatus = New System.Windows.Forms.GroupBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbStatus = New System.Windows.Forms.ProgressBar()
        Me.lblPathToSort = New System.Windows.Forms.Label()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlSortFiles = New System.Windows.Forms.Panel()
        Me.gbStatus.SuspendLayout()
        Me.pnlSortFiles.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cancel_Button
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClose.Location = New System.Drawing.Point(351, 143)
        Me.btnClose.Name = "Cancel_Button"
        Me.btnClose.Size = New System.Drawing.Size(72, 23)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(383, 25)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(37, 23)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPath.Location = New System.Drawing.Point(9, 26)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(365, 22)
        Me.txtPath.TabIndex = 1
        '
        'btnGo
        '
        Me.btnGo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnGo.Location = New System.Drawing.Point(12, 143)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(72, 24)
        Me.btnGo.TabIndex = 0
        Me.btnGo.Text = "Go"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'gbStatus
        '
        Me.gbStatus.Controls.Add(Me.lblStatus)
        Me.gbStatus.Controls.Add(Me.pbStatus)
        Me.gbStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbStatus.Location = New System.Drawing.Point(9, 52)
        Me.gbStatus.Name = "gbStatus"
        Me.gbStatus.Size = New System.Drawing.Size(411, 79)
        Me.gbStatus.TabIndex = 3
        Me.gbStatus.TabStop = False
        Me.gbStatus.Text = "Status"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(6, 25)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(191, 13)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Enter Path and Press ""Go"" to Begin."
        '
        'pbStatus
        '
        Me.pbStatus.Location = New System.Drawing.Point(6, 45)
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(399, 23)
        Me.pbStatus.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbStatus.TabIndex = 1
        '
        'lblPathToSort
        '
        Me.lblPathToSort.AutoSize = True
        Me.lblPathToSort.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPathToSort.Location = New System.Drawing.Point(6, 6)
        Me.lblPathToSort.Name = "lblPathToSort"
        Me.lblPathToSort.Size = New System.Drawing.Size(72, 13)
        Me.lblPathToSort.TabIndex = 0
        Me.lblPathToSort.Text = "Path to Sort:"
        '
        'fbdBrowse
        '
        Me.fbdBrowse.Description = "Select the folder which contains the files you wish to sort."
        '
        'pnlSortFiles
        '
        Me.pnlSortFiles.BackColor = System.Drawing.Color.White
        Me.pnlSortFiles.Controls.Add(Me.lblPathToSort)
        Me.pnlSortFiles.Controls.Add(Me.gbStatus)
        Me.pnlSortFiles.Controls.Add(Me.txtPath)
        Me.pnlSortFiles.Controls.Add(Me.btnBrowse)
        Me.pnlSortFiles.Location = New System.Drawing.Point(3, 3)
        Me.pnlSortFiles.Name = "pnlSortFiles"
        Me.pnlSortFiles.Size = New System.Drawing.Size(429, 137)
        Me.pnlSortFiles.TabIndex = 2
        '
        'dlgSortFiles
        '
        Me.AcceptButton = Me.btnGo
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(435, 169)
        Me.Controls.Add(Me.pnlSortFiles)
        Me.Controls.Add(Me.btnGo)
        Me.Controls.Add(Me.btnClose)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgSortFiles"
        Me.ShowInTaskbar = False
        Me.Text = "Sort Files Into Folders"
        Me.gbStatus.ResumeLayout(False)
        Me.gbStatus.PerformLayout()
        Me.pnlSortFiles.ResumeLayout(False)
        Me.pnlSortFiles.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnGo As System.Windows.Forms.Button
    Friend WithEvents gbStatus As System.Windows.Forms.GroupBox
    Friend WithEvents pbStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblPathToSort As System.Windows.Forms.Label
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents pnlSortFiles As System.Windows.Forms.Panel

End Class
