<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgDVDProfilerSelect
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
        Me.ofdCollectionXML = New System.Windows.Forms.OpenFileDialog()
        Me.btnLoadCollection = New System.Windows.Forms.Button()
        Me.lvCollection = New System.Windows.Forms.ListView()
        Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.CANCEL_Button = New System.Windows.Forms.Button()
        Me.tlpButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLoadCollection
        '
        Me.btnLoadCollection.Location = New System.Drawing.Point(195, 12)
        Me.btnLoadCollection.Name = "btnLoadCollection"
        Me.btnLoadCollection.Size = New System.Drawing.Size(116, 23)
        Me.btnLoadCollection.TabIndex = 0
        Me.btnLoadCollection.Text = "Load Collection XML"
        Me.btnLoadCollection.UseVisualStyleBackColor = True
        '
        'lvCollection
        '
        Me.lvCollection.Location = New System.Drawing.Point(12, 42)
        Me.lvCollection.Name = "lvCollection"
        Me.lvCollection.Size = New System.Drawing.Size(494, 335)
        Me.lvCollection.TabIndex = 1
        Me.lvCollection.UseCompatibleStateImageBehavior = False
        Me.lvCollection.View = System.Windows.Forms.View.Details
        '
        'tlpButtons
        '
        Me.tlpButtons.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpButtons.ColumnCount = 2
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Controls.Add(Me.OK_Button, 0, 0)
        Me.tlpButtons.Controls.Add(Me.CANCEL_Button, 1, 0)
        Me.tlpButtons.Location = New System.Drawing.Point(360, 383)
        Me.tlpButtons.Name = "tlpButtons"
        Me.tlpButtons.RowCount = 1
        Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Size = New System.Drawing.Size(146, 29)
        Me.tlpButtons.TabIndex = 2
        '
        'OK_Button
        '
        Me.OK_Button.Enabled = False
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        Me.OK_Button.UseVisualStyleBackColor = True
        '
        'CANCEL_Button
        '
        Me.CANCEL_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CANCEL_Button.Location = New System.Drawing.Point(76, 3)
        Me.CANCEL_Button.Name = "CANCEL_Button"
        Me.CANCEL_Button.Size = New System.Drawing.Size(67, 23)
        Me.CANCEL_Button.TabIndex = 1
        Me.CANCEL_Button.Text = "Cancel"
        Me.CANCEL_Button.UseVisualStyleBackColor = True
        '
        'dlgDVDProfilerSelect
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CANCEL_Button
        Me.ClientSize = New System.Drawing.Size(518, 424)
        Me.Controls.Add(Me.tlpButtons)
        Me.Controls.Add(Me.lvCollection)
        Me.Controls.Add(Me.btnLoadCollection)
        Me.Name = "dlgDVDProfilerSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Load DVD Profiler Collection"
        Me.tlpButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ofdCollectionXML As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnLoadCollection As System.Windows.Forms.Button
    Friend WithEvents lvCollection As System.Windows.Forms.ListView
    Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents CANCEL_Button As System.Windows.Forms.Button
End Class
