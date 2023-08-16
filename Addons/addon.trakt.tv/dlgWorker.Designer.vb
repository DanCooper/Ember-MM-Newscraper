<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgWorker
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgWorker))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pbLogo = New System.Windows.Forms.PictureBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblOverallStateInfo = New System.Windows.Forms.Label()
        Me.prgbOverallState = New System.Windows.Forms.ProgressBar()
        Me.lblCurrentItemInfo = New System.Windows.Forms.Label()
        Me.prgbCurrentItem = New System.Windows.Forms.ProgressBar()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.lblOverallState = New System.Windows.Forms.Label()
        Me.lblCurrentItem = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(438, 196)
        Me.pnlMain.TabIndex = 0
        '
        'tblMain
        '
        Me.tblMain.ColumnCount = 4
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.Controls.Add(Me.pbLogo, 0, 0)
        Me.tblMain.Controls.Add(Me.lblTitle, 1, 0)
        Me.tblMain.Controls.Add(Me.prgbOverallState, 2, 2)
        Me.tblMain.Controls.Add(Me.prgbCurrentItem, 2, 4)
        Me.tblMain.Controls.Add(Me.lblOverallStateInfo, 2, 1)
        Me.tblMain.Controls.Add(Me.lblOverallState, 1, 2)
        Me.tblMain.Controls.Add(Me.lblCurrentItemInfo, 2, 3)
        Me.tblMain.Controls.Add(Me.lblCurrentItem, 1, 4)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 6
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(438, 196)
        Me.tblMain.TabIndex = 0
        '
        'pbLogo
        '
        Me.pbLogo.Image = Global.addon.trakt.tv.My.Resources.Resources.logo
        Me.pbLogo.Location = New System.Drawing.Point(3, 3)
        Me.pbLogo.Name = "pbLogo"
        Me.pbLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbLogo.TabIndex = 0
        Me.pbLogo.TabStop = False
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitle.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.lblTitle, 2)
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(57, 17)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(284, 20)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Trakt movie watched state syncing"
        '
        'lblOverallStateInfo
        '
        Me.lblOverallStateInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOverallStateInfo.AutoSize = True
        Me.lblOverallStateInfo.Location = New System.Drawing.Point(129, 57)
        Me.lblOverallStateInfo.Name = "lblOverallStateInfo"
        Me.lblOverallStateInfo.Size = New System.Drawing.Size(83, 13)
        Me.lblOverallStateInfo.TabIndex = 2
        Me.lblOverallStateInfo.Text = "OverallStateInfo"
        '
        'prgbOverallState
        '
        Me.prgbOverallState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prgbOverallState.Location = New System.Drawing.Point(129, 77)
        Me.prgbOverallState.Name = "prgbOverallState"
        Me.prgbOverallState.Size = New System.Drawing.Size(284, 23)
        Me.prgbOverallState.TabIndex = 3
        '
        'lblCurrentItemInfo
        '
        Me.lblCurrentItemInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCurrentItemInfo.AutoSize = True
        Me.lblCurrentItemInfo.Location = New System.Drawing.Point(129, 106)
        Me.lblCurrentItemInfo.Name = "lblCurrentItemInfo"
        Me.lblCurrentItemInfo.Size = New System.Drawing.Size(79, 13)
        Me.lblCurrentItemInfo.TabIndex = 2
        Me.lblCurrentItemInfo.Text = "CurrentItemInfo"
        '
        'prgbCurrentItem
        '
        Me.prgbCurrentItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prgbCurrentItem.Location = New System.Drawing.Point(129, 126)
        Me.prgbCurrentItem.Name = "prgbCurrentItem"
        Me.prgbCurrentItem.Size = New System.Drawing.Size(284, 23)
        Me.prgbCurrentItem.TabIndex = 3
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 196)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(438, 29)
        Me.pnlBottom.TabIndex = 1
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 3
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnCancel, 2, 0)
        Me.tblBottom.Controls.Add(Me.btnStart, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(438, 29)
        Me.tblBottom.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = True
        Me.btnCancel.Location = New System.Drawing.Point(360, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.AutoSize = True
        Me.btnStart.Location = New System.Drawing.Point(274, 3)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(80, 23)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start Syncing"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'lblOverallState
        '
        Me.lblOverallState.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOverallState.AutoSize = True
        Me.lblOverallState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverallState.Location = New System.Drawing.Point(57, 82)
        Me.lblOverallState.Name = "lblOverallState"
        Me.lblOverallState.Size = New System.Drawing.Size(66, 13)
        Me.lblOverallState.TabIndex = 4
        Me.lblOverallState.Text = "Overall state"
        '
        'lblCurrentItem
        '
        Me.lblCurrentItem.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCurrentItem.AutoSize = True
        Me.lblCurrentItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentItem.Location = New System.Drawing.Point(57, 131)
        Me.lblCurrentItem.Name = "lblCurrentItem"
        Me.lblCurrentItem.Size = New System.Drawing.Size(63, 13)
        Me.lblCurrentItem.TabIndex = 4
        Me.lblCurrentItem.Text = "Current item"
        '
        'dlgWorker
        '
        Me.AcceptButton = Me.btnStart
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 225)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "dlgWorker"
        Me.Text = "Trakt Addon Worker"
        Me.pnlMain.ResumeLayout(False)
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        CType(Me.pbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents btnCancel As Button
    Friend WithEvents tblMain As TableLayoutPanel
    Friend WithEvents pbLogo As PictureBox
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblOverallStateInfo As Label
    Friend WithEvents prgbOverallState As ProgressBar
    Friend WithEvents lblCurrentItemInfo As Label
    Friend WithEvents prgbCurrentItem As ProgressBar
    Friend WithEvents btnStart As Button
    Friend WithEvents lblOverallState As Label
    Friend WithEvents lblCurrentItem As Label
End Class
