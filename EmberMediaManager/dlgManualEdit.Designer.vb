<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgManualEdit
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
        Me.components = New System.ComponentModel.Container()
        Dim XmlViewerSettings2 As Ember_Media_Manager.XMLViewerSettings = New Ember_Media_Manager.XMLViewerSettings()
        Me.mnuFormat = New System.Windows.Forms.MenuItem()
        Me.pnlManualEdit = New System.Windows.Forms.Panel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.xmlvNFO = New Ember_Media_Manager.XMLViewer()
        Me.splMain = New System.Windows.Forms.Splitter()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.lbErrorLog = New System.Windows.Forms.ListBox()
        Me.mnuParse = New System.Windows.Forms.MenuItem()
        Me.mnuTools = New System.Windows.Forms.MenuItem()
        Me.mmManualEdit = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuFile = New System.Windows.Forms.MenuItem()
        Me.mnuSave = New System.Windows.Forms.MenuItem()
        Me.mnuSplitter = New System.Windows.Forms.MenuItem()
        Me.mnuExit = New System.Windows.Forms.MenuItem()
        Me.pnlManualEdit.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuFormat
        '
        Me.mnuFormat.Index = 1
        Me.mnuFormat.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF
        Me.mnuFormat.Text = "&Format / Indent"
        '
        'pnlManualEdit
        '
        Me.pnlManualEdit.Controls.Add(Me.pnlMain)
        Me.pnlManualEdit.Controls.Add(Me.splMain)
        Me.pnlManualEdit.Controls.Add(Me.pnlBottom)
        Me.pnlManualEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlManualEdit.Location = New System.Drawing.Point(0, 0)
        Me.pnlManualEdit.Name = "pnlManualEdit"
        Me.pnlManualEdit.Size = New System.Drawing.Size(853, 426)
        Me.pnlManualEdit.TabIndex = 1
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.xmlvNFO)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(853, 322)
        Me.pnlMain.TabIndex = 2
        '
        'xmlvNFO
        '
        Me.xmlvNFO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xmlvNFO.Location = New System.Drawing.Point(0, 0)
        Me.xmlvNFO.Name = "xmlvNFO"
        XmlViewerSettings2.AttributeKey = System.Drawing.Color.Red
        XmlViewerSettings2.AttributeValue = System.Drawing.Color.Blue
        XmlViewerSettings2.Element = System.Drawing.Color.DarkRed
        XmlViewerSettings2.Tag = System.Drawing.Color.Blue
        XmlViewerSettings2.Value = System.Drawing.Color.Black
        Me.xmlvNFO.Settings = XmlViewerSettings2
        Me.xmlvNFO.Size = New System.Drawing.Size(853, 322)
        Me.xmlvNFO.TabIndex = 0
        Me.xmlvNFO.Text = ""
        '
        'splMain
        '
        Me.splMain.BackColor = System.Drawing.Color.DimGray
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.splMain.Location = New System.Drawing.Point(0, 322)
        Me.splMain.Name = "splMain"
        Me.splMain.Size = New System.Drawing.Size(853, 4)
        Me.splMain.TabIndex = 0
        Me.splMain.TabStop = False
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.lbErrorLog)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 326)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(853, 100)
        Me.pnlBottom.TabIndex = 0
        '
        'lbErrorLog
        '
        Me.lbErrorLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbErrorLog.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbErrorLog.HorizontalScrollbar = True
        Me.lbErrorLog.ItemHeight = 21
        Me.lbErrorLog.Location = New System.Drawing.Point(0, 0)
        Me.lbErrorLog.Name = "lbErrorLog"
        Me.lbErrorLog.Size = New System.Drawing.Size(853, 100)
        Me.lbErrorLog.TabIndex = 0
        '
        'mnuParse
        '
        Me.mnuParse.Index = 0
        Me.mnuParse.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP
        Me.mnuParse.Text = "&Parse"
        '
        'mnuTools
        '
        Me.mnuTools.Index = 1
        Me.mnuTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuParse, Me.mnuFormat})
        Me.mnuTools.Text = "&Tools"
        '
        'mmManualEdit
        '
        Me.mmManualEdit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuTools})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSave, Me.mnuSplitter, Me.mnuExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuSave
        '
        Me.mnuSave.Index = 0
        Me.mnuSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mnuSave.Text = "&Save"
        '
        'mnuSplitter
        '
        Me.mnuSplitter.Index = 1
        Me.mnuSplitter.Text = "-"
        '
        'mnuExit
        '
        Me.mnuExit.Index = 2
        Me.mnuExit.Shortcut = System.Windows.Forms.Shortcut.AltF4
        Me.mnuExit.Text = "E&xit"
        '
        'dlgManualEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(853, 426)
        Me.Controls.Add(Me.pnlManualEdit)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Menu = Me.mmManualEdit
        Me.MinimizeBox = False
        Me.Name = "dlgManualEdit"
        Me.Text = "Manual NFO Editor"
        Me.pnlManualEdit.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents mnuFormat As System.Windows.Forms.MenuItem
    Friend WithEvents pnlManualEdit As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents splMain As System.Windows.Forms.Splitter
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents lbErrorLog As System.Windows.Forms.ListBox
    Friend WithEvents mnuParse As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTools As System.Windows.Forms.MenuItem
    Friend WithEvents mmManualEdit As System.Windows.Forms.MainMenu
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSave As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSplitter As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    Friend WithEvents xmlvNFO As Ember_Media_Manager.XMLViewer

End Class
