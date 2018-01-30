<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInfoPanel
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlInfoPanel = New System.Windows.Forms.Panel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.lvFlags = New System.Windows.Forms.ListView()
        Me.colFlag = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colInfo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colContentType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.pnlInfoPanel.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlInfoPanel
        '
        Me.pnlInfoPanel.Controls.Add(Me.pnlMain)
        Me.pnlInfoPanel.Controls.Add(Me.pnlBottom)
        Me.pnlInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlInfoPanel.Location = New System.Drawing.Point(0, 0)
        Me.pnlInfoPanel.Name = "pnlInfoPanel"
        Me.pnlInfoPanel.Size = New System.Drawing.Size(611, 396)
        Me.pnlInfoPanel.TabIndex = 0
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.lvFlags)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(611, 353)
        Me.pnlMain.TabIndex = 1
        '
        'lvFlags
        '
        Me.lvFlags.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colFlag, Me.colInfo, Me.colContentType})
        Me.lvFlags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFlags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvFlags.FullRowSelect = True
        Me.lvFlags.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lvFlags.Location = New System.Drawing.Point(0, 0)
        Me.lvFlags.MultiSelect = False
        Me.lvFlags.Name = "lvFlags"
        Me.lvFlags.Size = New System.Drawing.Size(611, 353)
        Me.lvFlags.TabIndex = 2
        Me.lvFlags.UseCompatibleStateImageBehavior = False
        Me.lvFlags.View = System.Windows.Forms.View.Details
        '
        'colFlag
        '
        Me.colFlag.Width = 200
        '
        'colInfo
        '
        Me.colInfo.Width = 200
        '
        'colContentType
        '
        Me.colContentType.Width = 200
        '
        'pnlBottom
        '
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 353)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(611, 43)
        Me.pnlBottom.TabIndex = 0
        '
        'frmInfoPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 396)
        Me.Controls.Add(Me.pnlInfoPanel)
        Me.Name = "frmInfoPanel"
        Me.Text = "frmInfoPanel"
        Me.pnlInfoPanel.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlInfoPanel As Panel
    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents lvFlags As ListView
    Friend WithEvents colFlag As ColumnHeader
    Friend WithEvents colInfo As ColumnHeader
    Friend WithEvents colContentType As ColumnHeader
End Class
