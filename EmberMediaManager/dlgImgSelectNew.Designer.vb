<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgImgSelectNew
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
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.btnRemoveMain = New System.Windows.Forms.Button()
        Me.btnRestoreMain = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLeftBottom = New System.Windows.Forms.Panel()
        Me.btnRemoveSub = New System.Windows.Forms.Button()
        Me.btnRestoreSub = New System.Windows.Forms.Button()
        Me.pnlLeftTop = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.pnlImages = New System.Windows.Forms.Panel()
        Me.pnlTop.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftBottom.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.pnlTopRight)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1008, 119)
        Me.pnlTop.TabIndex = 0
        '
        'pnlTopRight
        '
        Me.pnlTopRight.Controls.Add(Me.btnRemoveMain)
        Me.pnlTopRight.Controls.Add(Me.btnRestoreMain)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlTopRight.Location = New System.Drawing.Point(978, 0)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(30, 119)
        Me.pnlTopRight.TabIndex = 0
        '
        'btnRemoveMain
        '
        Me.btnRemoveMain.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.btnRemoveMain.Location = New System.Drawing.Point(3, 87)
        Me.btnRemoveMain.Name = "btnRemoveMain"
        Me.btnRemoveMain.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveMain.TabIndex = 1
        Me.btnRemoveMain.UseVisualStyleBackColor = True
        '
        'btnRestoreMain
        '
        Me.btnRestoreMain.Image = Global.Ember_Media_Manager.My.Resources.Resources.undo
        Me.btnRestoreMain.Location = New System.Drawing.Point(3, 6)
        Me.btnRestoreMain.Name = "btnRestoreMain"
        Me.btnRestoreMain.Size = New System.Drawing.Size(23, 23)
        Me.btnRestoreMain.TabIndex = 0
        Me.btnRestoreMain.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnCancel)
        Me.pnlBottom.Controls.Add(Me.btnOK)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 557)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1008, 54)
        Me.pnlBottom.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(921, 19)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(840, 19)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'pnlLeft
        '
        Me.pnlLeft.AutoScroll = True
        Me.pnlLeft.Controls.Add(Me.pnlLeftBottom)
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 119)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(154, 438)
        Me.pnlLeft.TabIndex = 3
        '
        'pnlLeftBottom
        '
        Me.pnlLeftBottom.Controls.Add(Me.btnRemoveSub)
        Me.pnlLeftBottom.Controls.Add(Me.btnRestoreSub)
        Me.pnlLeftBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLeftBottom.Location = New System.Drawing.Point(0, 408)
        Me.pnlLeftBottom.Name = "pnlLeftBottom"
        Me.pnlLeftBottom.Size = New System.Drawing.Size(154, 30)
        Me.pnlLeftBottom.TabIndex = 1
        '
        'btnRemoveSub
        '
        Me.btnRemoveSub.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.btnRemoveSub.Location = New System.Drawing.Point(124, 3)
        Me.btnRemoveSub.Name = "btnRemoveSub"
        Me.btnRemoveSub.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveSub.TabIndex = 2
        Me.btnRemoveSub.UseVisualStyleBackColor = True
        '
        'btnRestoreSub
        '
        Me.btnRestoreSub.Image = Global.Ember_Media_Manager.My.Resources.Resources.undo
        Me.btnRestoreSub.Location = New System.Drawing.Point(6, 3)
        Me.btnRestoreSub.Name = "btnRestoreSub"
        Me.btnRestoreSub.Size = New System.Drawing.Size(23, 23)
        Me.btnRestoreSub.TabIndex = 1
        Me.btnRestoreSub.UseVisualStyleBackColor = True
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.Controls.Add(Me.ComboBox1)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Size = New System.Drawing.Size(154, 32)
        Me.pnlLeftTop.TabIndex = 0
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(6, 6)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(142, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'pnlImages
        '
        Me.pnlImages.AutoScroll = True
        Me.pnlImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImages.Location = New System.Drawing.Point(154, 119)
        Me.pnlImages.Name = "pnlImages"
        Me.pnlImages.Size = New System.Drawing.Size(854, 438)
        Me.pnlImages.TabIndex = 4
        '
        'dlgImgSelectNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 611)
        Me.Controls.Add(Me.pnlImages)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "dlgImgSelectNew"
        Me.Text = "Form1"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftBottom.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlImages As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents pnlLeftBottom As System.Windows.Forms.Panel
    Friend WithEvents btnRemoveMain As System.Windows.Forms.Button
    Friend WithEvents btnRestoreMain As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSub As System.Windows.Forms.Button
    Friend WithEvents btnRestoreSub As System.Windows.Forms.Button
End Class
