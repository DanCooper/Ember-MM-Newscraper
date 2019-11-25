<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOption_FileSystem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOption_FileSystem))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbFileSystemValidVideoExts = New System.Windows.Forms.GroupBox()
        Me.tblFileSystemValidVideoExts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFileSystemValidVideoExtsRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemValidVideoExtsReset = New System.Windows.Forms.Button()
        Me.btnFileSystemValidVideoExtsAdd = New System.Windows.Forms.Button()
        Me.lstFileSystemValidVideoExts = New System.Windows.Forms.ListBox()
        Me.txtFileSystemValidVideoExts = New System.Windows.Forms.TextBox()
        Me.gbFileSystemNoStackExts = New System.Windows.Forms.GroupBox()
        Me.tblFileSystemNoStackExts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFileSystemNoStackExtsRemove = New System.Windows.Forms.Button()
        Me.lstFileSystemNoStackExts = New System.Windows.Forms.ListBox()
        Me.btnFileSystemNoStackExtsAdd = New System.Windows.Forms.Button()
        Me.txtFileSystemNoStackExts = New System.Windows.Forms.TextBox()
        Me.gbFileSystemExcludedPaths = New System.Windows.Forms.GroupBox()
        Me.tblFileSystemExcludedPaths = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFileSystemExcludedPathsRemove = New System.Windows.Forms.Button()
        Me.lstFileSystemExcludedPaths = New System.Windows.Forms.ListBox()
        Me.txtFileSystemExcludedPaths = New System.Windows.Forms.TextBox()
        Me.btnFileSystemExcludedPathsAdd = New System.Windows.Forms.Button()
        Me.btnFileSystemExcludedPathsBrowse = New System.Windows.Forms.Button()
        Me.gbFileSystemValidSubtitlesExts = New System.Windows.Forms.GroupBox()
        Me.tblFileSystemValidSubtitlesExts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFileSystemValidSubtitlesExtsRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemValidSubtitlesExtsReset = New System.Windows.Forms.Button()
        Me.btnFileSystemValidSubtitlesExtsAdd = New System.Windows.Forms.Button()
        Me.lstFileSystemValidSubtitlesExts = New System.Windows.Forms.ListBox()
        Me.txtFileSystemValidSubtitlesExts = New System.Windows.Forms.TextBox()
        Me.gbFileSystemValidThemeExts = New System.Windows.Forms.GroupBox()
        Me.tblFileSystemValidThemeExts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFileSystemValidThemeExtsRemove = New System.Windows.Forms.Button()
        Me.btnFileSystemValidThemeExtsReset = New System.Windows.Forms.Button()
        Me.btnFileSystemValidThemeExtsAdd = New System.Windows.Forms.Button()
        Me.lstFileSystemValidThemeExts = New System.Windows.Forms.ListBox()
        Me.txtFileSystemValidThemeExts = New System.Windows.Forms.TextBox()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.gbGeneralVirtualDrive = New System.Windows.Forms.GroupBox()
        Me.tblGeneralVirtualDrive = New System.Windows.Forms.TableLayoutPanel()
        Me.btnGeneralVirtualDriveBinPathBrowse = New System.Windows.Forms.Button()
        Me.lblGeneralVirtualDriveLetter = New System.Windows.Forms.Label()
        Me.txtGeneralVirtualDriveBinPath = New System.Windows.Forms.TextBox()
        Me.cbGeneralVirtualDriveLetter = New System.Windows.Forms.ComboBox()
        Me.lblGeneralVirtualDrivePath = New System.Windows.Forms.Label()
        Me.fileBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbFileSystemValidVideoExts.SuspendLayout()
        Me.tblFileSystemValidVideoExts.SuspendLayout()
        Me.gbFileSystemNoStackExts.SuspendLayout()
        Me.tblFileSystemNoStackExts.SuspendLayout()
        Me.gbFileSystemExcludedPaths.SuspendLayout()
        Me.tblFileSystemExcludedPaths.SuspendLayout()
        Me.gbFileSystemValidSubtitlesExts.SuspendLayout()
        Me.tblFileSystemValidSubtitlesExts.SuspendLayout()
        Me.gbFileSystemValidThemeExts.SuspendLayout()
        Me.tblFileSystemValidThemeExts.SuspendLayout()
        Me.gbGeneralVirtualDrive.SuspendLayout()
        Me.tblGeneralVirtualDrive.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(849, 655)
        Me.pnlSettings.TabIndex = 18
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 4
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbFileSystemValidVideoExts, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbFileSystemNoStackExts, 2, 0)
        Me.tblSettings.Controls.Add(Me.gbFileSystemExcludedPaths, 0, 2)
        Me.tblSettings.Controls.Add(Me.gbFileSystemValidSubtitlesExts, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbFileSystemValidThemeExts, 1, 1)
        Me.tblSettings.Controls.Add(Me.gbGeneralVirtualDrive, 0, 3)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 5
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(849, 655)
        Me.tblSettings.TabIndex = 6
        '
        'gbFileSystemValidVideoExts
        '
        Me.gbFileSystemValidVideoExts.AutoSize = True
        Me.gbFileSystemValidVideoExts.Controls.Add(Me.tblFileSystemValidVideoExts)
        Me.gbFileSystemValidVideoExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFileSystemValidVideoExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFileSystemValidVideoExts.Location = New System.Drawing.Point(3, 3)
        Me.gbFileSystemValidVideoExts.Name = "gbFileSystemValidVideoExts"
        Me.tblSettings.SetRowSpan(Me.gbFileSystemValidVideoExts, 2)
        Me.gbFileSystemValidVideoExts.Size = New System.Drawing.Size(162, 346)
        Me.gbFileSystemValidVideoExts.TabIndex = 0
        Me.gbFileSystemValidVideoExts.TabStop = False
        Me.gbFileSystemValidVideoExts.Text = "Valid Video Extensions"
        '
        'tblFileSystemValidVideoExts
        '
        Me.tblFileSystemValidVideoExts.AutoSize = True
        Me.tblFileSystemValidVideoExts.ColumnCount = 4
        Me.tblFileSystemValidVideoExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidVideoExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidVideoExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidVideoExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidVideoExts.Controls.Add(Me.btnFileSystemValidVideoExtsRemove, 2, 2)
        Me.tblFileSystemValidVideoExts.Controls.Add(Me.btnFileSystemValidVideoExtsReset, 2, 0)
        Me.tblFileSystemValidVideoExts.Controls.Add(Me.btnFileSystemValidVideoExtsAdd, 1, 2)
        Me.tblFileSystemValidVideoExts.Controls.Add(Me.lstFileSystemValidVideoExts, 0, 1)
        Me.tblFileSystemValidVideoExts.Controls.Add(Me.txtFileSystemValidVideoExts, 0, 2)
        Me.tblFileSystemValidVideoExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFileSystemValidVideoExts.Location = New System.Drawing.Point(3, 18)
        Me.tblFileSystemValidVideoExts.Name = "tblFileSystemValidVideoExts"
        Me.tblFileSystemValidVideoExts.RowCount = 4
        Me.tblFileSystemValidVideoExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidVideoExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidVideoExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidVideoExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidVideoExts.Size = New System.Drawing.Size(156, 325)
        Me.tblFileSystemValidVideoExts.TabIndex = 7
        '
        'btnFileSystemValidVideoExtsRemove
        '
        Me.btnFileSystemValidVideoExtsRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFileSystemValidVideoExtsRemove.Image = CType(resources.GetObject("btnFileSystemValidVideoExtsRemove.Image"), System.Drawing.Image)
        Me.btnFileSystemValidVideoExtsRemove.Location = New System.Drawing.Point(130, 298)
        Me.btnFileSystemValidVideoExtsRemove.Name = "btnFileSystemValidVideoExtsRemove"
        Me.btnFileSystemValidVideoExtsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidVideoExtsRemove.TabIndex = 3
        Me.btnFileSystemValidVideoExtsRemove.UseVisualStyleBackColor = True
        '
        'btnFileSystemValidVideoExtsReset
        '
        Me.btnFileSystemValidVideoExtsReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFileSystemValidVideoExtsReset.Image = CType(resources.GetObject("btnFileSystemValidVideoExtsReset.Image"), System.Drawing.Image)
        Me.btnFileSystemValidVideoExtsReset.Location = New System.Drawing.Point(130, 3)
        Me.btnFileSystemValidVideoExtsReset.Name = "btnFileSystemValidVideoExtsReset"
        Me.btnFileSystemValidVideoExtsReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidVideoExtsReset.TabIndex = 4
        Me.btnFileSystemValidVideoExtsReset.UseVisualStyleBackColor = True
        '
        'btnFileSystemValidVideoExtsAdd
        '
        Me.btnFileSystemValidVideoExtsAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFileSystemValidVideoExtsAdd.Image = CType(resources.GetObject("btnFileSystemValidVideoExtsAdd.Image"), System.Drawing.Image)
        Me.btnFileSystemValidVideoExtsAdd.Location = New System.Drawing.Point(59, 298)
        Me.btnFileSystemValidVideoExtsAdd.Name = "btnFileSystemValidVideoExtsAdd"
        Me.btnFileSystemValidVideoExtsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidVideoExtsAdd.TabIndex = 2
        Me.btnFileSystemValidVideoExtsAdd.UseVisualStyleBackColor = True
        '
        'lstFileSystemValidVideoExts
        '
        Me.tblFileSystemValidVideoExts.SetColumnSpan(Me.lstFileSystemValidVideoExts, 3)
        Me.lstFileSystemValidVideoExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstFileSystemValidVideoExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstFileSystemValidVideoExts.FormattingEnabled = True
        Me.lstFileSystemValidVideoExts.Location = New System.Drawing.Point(3, 32)
        Me.lstFileSystemValidVideoExts.Name = "lstFileSystemValidVideoExts"
        Me.lstFileSystemValidVideoExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemValidVideoExts.Size = New System.Drawing.Size(150, 260)
        Me.lstFileSystemValidVideoExts.Sorted = True
        Me.lstFileSystemValidVideoExts.TabIndex = 0
        '
        'txtFileSystemValidVideoExts
        '
        Me.txtFileSystemValidVideoExts.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFileSystemValidVideoExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileSystemValidVideoExts.Location = New System.Drawing.Point(3, 298)
        Me.txtFileSystemValidVideoExts.Name = "txtFileSystemValidVideoExts"
        Me.txtFileSystemValidVideoExts.Size = New System.Drawing.Size(50, 22)
        Me.txtFileSystemValidVideoExts.TabIndex = 1
        '
        'gbFileSystemNoStackExts
        '
        Me.gbFileSystemNoStackExts.AutoSize = True
        Me.gbFileSystemNoStackExts.Controls.Add(Me.tblFileSystemNoStackExts)
        Me.gbFileSystemNoStackExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFileSystemNoStackExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFileSystemNoStackExts.Location = New System.Drawing.Point(339, 3)
        Me.gbFileSystemNoStackExts.Name = "gbFileSystemNoStackExts"
        Me.gbFileSystemNoStackExts.Size = New System.Drawing.Size(275, 170)
        Me.gbFileSystemNoStackExts.TabIndex = 1
        Me.gbFileSystemNoStackExts.TabStop = False
        Me.gbFileSystemNoStackExts.Text = "No Stack Extensions"
        '
        'tblFileSystemNoStackExts
        '
        Me.tblFileSystemNoStackExts.AutoSize = True
        Me.tblFileSystemNoStackExts.ColumnCount = 4
        Me.tblFileSystemNoStackExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemNoStackExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemNoStackExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemNoStackExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemNoStackExts.Controls.Add(Me.btnFileSystemNoStackExtsRemove, 2, 1)
        Me.tblFileSystemNoStackExts.Controls.Add(Me.lstFileSystemNoStackExts, 0, 0)
        Me.tblFileSystemNoStackExts.Controls.Add(Me.btnFileSystemNoStackExtsAdd, 1, 1)
        Me.tblFileSystemNoStackExts.Controls.Add(Me.txtFileSystemNoStackExts, 0, 1)
        Me.tblFileSystemNoStackExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFileSystemNoStackExts.Location = New System.Drawing.Point(3, 18)
        Me.tblFileSystemNoStackExts.Name = "tblFileSystemNoStackExts"
        Me.tblFileSystemNoStackExts.RowCount = 3
        Me.tblFileSystemNoStackExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemNoStackExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemNoStackExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemNoStackExts.Size = New System.Drawing.Size(269, 149)
        Me.tblFileSystemNoStackExts.TabIndex = 9
        '
        'btnFileSystemNoStackExtsRemove
        '
        Me.btnFileSystemNoStackExtsRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFileSystemNoStackExtsRemove.Image = CType(resources.GetObject("btnFileSystemNoStackExtsRemove.Image"), System.Drawing.Image)
        Me.btnFileSystemNoStackExtsRemove.Location = New System.Drawing.Point(160, 122)
        Me.btnFileSystemNoStackExtsRemove.Name = "btnFileSystemNoStackExtsRemove"
        Me.btnFileSystemNoStackExtsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemNoStackExtsRemove.TabIndex = 3
        Me.btnFileSystemNoStackExtsRemove.UseVisualStyleBackColor = True
        '
        'lstFileSystemNoStackExts
        '
        Me.tblFileSystemNoStackExts.SetColumnSpan(Me.lstFileSystemNoStackExts, 3)
        Me.lstFileSystemNoStackExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstFileSystemNoStackExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstFileSystemNoStackExts.FormattingEnabled = True
        Me.lstFileSystemNoStackExts.Location = New System.Drawing.Point(3, 3)
        Me.lstFileSystemNoStackExts.Name = "lstFileSystemNoStackExts"
        Me.lstFileSystemNoStackExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemNoStackExts.Size = New System.Drawing.Size(180, 113)
        Me.lstFileSystemNoStackExts.Sorted = True
        Me.lstFileSystemNoStackExts.TabIndex = 0
        '
        'btnFileSystemNoStackExtsAdd
        '
        Me.btnFileSystemNoStackExtsAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFileSystemNoStackExtsAdd.Image = CType(resources.GetObject("btnFileSystemNoStackExtsAdd.Image"), System.Drawing.Image)
        Me.btnFileSystemNoStackExtsAdd.Location = New System.Drawing.Point(59, 122)
        Me.btnFileSystemNoStackExtsAdd.Name = "btnFileSystemNoStackExtsAdd"
        Me.btnFileSystemNoStackExtsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemNoStackExtsAdd.TabIndex = 2
        Me.btnFileSystemNoStackExtsAdd.UseVisualStyleBackColor = True
        '
        'txtFileSystemNoStackExts
        '
        Me.txtFileSystemNoStackExts.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFileSystemNoStackExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileSystemNoStackExts.Location = New System.Drawing.Point(3, 122)
        Me.txtFileSystemNoStackExts.Name = "txtFileSystemNoStackExts"
        Me.txtFileSystemNoStackExts.Size = New System.Drawing.Size(50, 22)
        Me.txtFileSystemNoStackExts.TabIndex = 1
        '
        'gbFileSystemExcludedPaths
        '
        Me.gbFileSystemExcludedPaths.AutoSize = True
        Me.tblSettings.SetColumnSpan(Me.gbFileSystemExcludedPaths, 3)
        Me.gbFileSystemExcludedPaths.Controls.Add(Me.tblFileSystemExcludedPaths)
        Me.gbFileSystemExcludedPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFileSystemExcludedPaths.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gbFileSystemExcludedPaths.Location = New System.Drawing.Point(3, 355)
        Me.gbFileSystemExcludedPaths.Name = "gbFileSystemExcludedPaths"
        Me.gbFileSystemExcludedPaths.Size = New System.Drawing.Size(611, 138)
        Me.gbFileSystemExcludedPaths.TabIndex = 4
        Me.gbFileSystemExcludedPaths.TabStop = False
        Me.gbFileSystemExcludedPaths.Text = "Excluded Paths"
        '
        'tblFileSystemExcludedPaths
        '
        Me.tblFileSystemExcludedPaths.AutoSize = True
        Me.tblFileSystemExcludedPaths.ColumnCount = 4
        Me.tblFileSystemExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFileSystemExcludedPaths.Controls.Add(Me.btnFileSystemExcludedPathsRemove, 3, 1)
        Me.tblFileSystemExcludedPaths.Controls.Add(Me.lstFileSystemExcludedPaths, 0, 0)
        Me.tblFileSystemExcludedPaths.Controls.Add(Me.txtFileSystemExcludedPaths, 0, 1)
        Me.tblFileSystemExcludedPaths.Controls.Add(Me.btnFileSystemExcludedPathsAdd, 2, 1)
        Me.tblFileSystemExcludedPaths.Controls.Add(Me.btnFileSystemExcludedPathsBrowse, 1, 1)
        Me.tblFileSystemExcludedPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFileSystemExcludedPaths.Location = New System.Drawing.Point(3, 18)
        Me.tblFileSystemExcludedPaths.Name = "tblFileSystemExcludedPaths"
        Me.tblFileSystemExcludedPaths.RowCount = 3
        Me.tblFileSystemExcludedPaths.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemExcludedPaths.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemExcludedPaths.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemExcludedPaths.Size = New System.Drawing.Size(605, 117)
        Me.tblFileSystemExcludedPaths.TabIndex = 7
        '
        'btnFileSystemExcludedPathsRemove
        '
        Me.btnFileSystemExcludedPathsRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFileSystemExcludedPathsRemove.Image = CType(resources.GetObject("btnFileSystemExcludedPathsRemove.Image"), System.Drawing.Image)
        Me.btnFileSystemExcludedPathsRemove.Location = New System.Drawing.Point(579, 91)
        Me.btnFileSystemExcludedPathsRemove.Name = "btnFileSystemExcludedPathsRemove"
        Me.btnFileSystemExcludedPathsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemExcludedPathsRemove.TabIndex = 6
        Me.btnFileSystemExcludedPathsRemove.UseVisualStyleBackColor = True
        '
        'lstFileSystemExcludedPaths
        '
        Me.tblFileSystemExcludedPaths.SetColumnSpan(Me.lstFileSystemExcludedPaths, 4)
        Me.lstFileSystemExcludedPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstFileSystemExcludedPaths.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstFileSystemExcludedPaths.FormattingEnabled = True
        Me.lstFileSystemExcludedPaths.Location = New System.Drawing.Point(3, 3)
        Me.lstFileSystemExcludedPaths.Name = "lstFileSystemExcludedPaths"
        Me.lstFileSystemExcludedPaths.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemExcludedPaths.Size = New System.Drawing.Size(599, 82)
        Me.lstFileSystemExcludedPaths.Sorted = True
        Me.lstFileSystemExcludedPaths.TabIndex = 1
        '
        'txtFileSystemExcludedPaths
        '
        Me.txtFileSystemExcludedPaths.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFileSystemExcludedPaths.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileSystemExcludedPaths.Location = New System.Drawing.Point(3, 91)
        Me.txtFileSystemExcludedPaths.Name = "txtFileSystemExcludedPaths"
        Me.txtFileSystemExcludedPaths.Size = New System.Drawing.Size(410, 22)
        Me.txtFileSystemExcludedPaths.TabIndex = 4
        '
        'btnFileSystemExcludedPathsAdd
        '
        Me.btnFileSystemExcludedPathsAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFileSystemExcludedPathsAdd.Image = CType(resources.GetObject("btnFileSystemExcludedPathsAdd.Image"), System.Drawing.Image)
        Me.btnFileSystemExcludedPathsAdd.Location = New System.Drawing.Point(450, 91)
        Me.btnFileSystemExcludedPathsAdd.Name = "btnFileSystemExcludedPathsAdd"
        Me.btnFileSystemExcludedPathsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemExcludedPathsAdd.TabIndex = 5
        Me.btnFileSystemExcludedPathsAdd.UseVisualStyleBackColor = True
        '
        'btnFileSystemExcludedPathsBrowse
        '
        Me.btnFileSystemExcludedPathsBrowse.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFileSystemExcludedPathsBrowse.Location = New System.Drawing.Point(419, 91)
        Me.btnFileSystemExcludedPathsBrowse.Name = "btnFileSystemExcludedPathsBrowse"
        Me.btnFileSystemExcludedPathsBrowse.Size = New System.Drawing.Size(25, 23)
        Me.btnFileSystemExcludedPathsBrowse.TabIndex = 7
        Me.btnFileSystemExcludedPathsBrowse.Text = "..."
        Me.btnFileSystemExcludedPathsBrowse.UseVisualStyleBackColor = True
        '
        'gbFileSystemValidSubtitlesExts
        '
        Me.gbFileSystemValidSubtitlesExts.AutoSize = True
        Me.gbFileSystemValidSubtitlesExts.Controls.Add(Me.tblFileSystemValidSubtitlesExts)
        Me.gbFileSystemValidSubtitlesExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFileSystemValidSubtitlesExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFileSystemValidSubtitlesExts.Location = New System.Drawing.Point(171, 3)
        Me.gbFileSystemValidSubtitlesExts.Name = "gbFileSystemValidSubtitlesExts"
        Me.gbFileSystemValidSubtitlesExts.Size = New System.Drawing.Size(162, 170)
        Me.gbFileSystemValidSubtitlesExts.TabIndex = 5
        Me.gbFileSystemValidSubtitlesExts.TabStop = False
        Me.gbFileSystemValidSubtitlesExts.Text = "Valid Subtitles Extensions"
        '
        'tblFileSystemValidSubtitlesExts
        '
        Me.tblFileSystemValidSubtitlesExts.AutoSize = True
        Me.tblFileSystemValidSubtitlesExts.ColumnCount = 4
        Me.tblFileSystemValidSubtitlesExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidSubtitlesExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidSubtitlesExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidSubtitlesExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidSubtitlesExts.Controls.Add(Me.btnFileSystemValidSubtitlesExtsRemove, 2, 2)
        Me.tblFileSystemValidSubtitlesExts.Controls.Add(Me.btnFileSystemValidSubtitlesExtsReset, 2, 0)
        Me.tblFileSystemValidSubtitlesExts.Controls.Add(Me.btnFileSystemValidSubtitlesExtsAdd, 1, 2)
        Me.tblFileSystemValidSubtitlesExts.Controls.Add(Me.lstFileSystemValidSubtitlesExts, 0, 1)
        Me.tblFileSystemValidSubtitlesExts.Controls.Add(Me.txtFileSystemValidSubtitlesExts, 0, 2)
        Me.tblFileSystemValidSubtitlesExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFileSystemValidSubtitlesExts.Location = New System.Drawing.Point(3, 18)
        Me.tblFileSystemValidSubtitlesExts.Name = "tblFileSystemValidSubtitlesExts"
        Me.tblFileSystemValidSubtitlesExts.RowCount = 4
        Me.tblFileSystemValidSubtitlesExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidSubtitlesExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidSubtitlesExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidSubtitlesExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidSubtitlesExts.Size = New System.Drawing.Size(156, 149)
        Me.tblFileSystemValidSubtitlesExts.TabIndex = 8
        '
        'btnFileSystemValidSubtitlesExtsRemove
        '
        Me.btnFileSystemValidSubtitlesExtsRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFileSystemValidSubtitlesExtsRemove.Image = CType(resources.GetObject("btnFileSystemValidSubtitlesExtsRemove.Image"), System.Drawing.Image)
        Me.btnFileSystemValidSubtitlesExtsRemove.Location = New System.Drawing.Point(130, 123)
        Me.btnFileSystemValidSubtitlesExtsRemove.Name = "btnFileSystemValidSubtitlesExtsRemove"
        Me.btnFileSystemValidSubtitlesExtsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidSubtitlesExtsRemove.TabIndex = 3
        Me.btnFileSystemValidSubtitlesExtsRemove.UseVisualStyleBackColor = True
        '
        'btnFileSystemValidSubtitlesExtsReset
        '
        Me.btnFileSystemValidSubtitlesExtsReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFileSystemValidSubtitlesExtsReset.Image = CType(resources.GetObject("btnFileSystemValidSubtitlesExtsReset.Image"), System.Drawing.Image)
        Me.btnFileSystemValidSubtitlesExtsReset.Location = New System.Drawing.Point(130, 3)
        Me.btnFileSystemValidSubtitlesExtsReset.Name = "btnFileSystemValidSubtitlesExtsReset"
        Me.btnFileSystemValidSubtitlesExtsReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidSubtitlesExtsReset.TabIndex = 5
        Me.btnFileSystemValidSubtitlesExtsReset.UseVisualStyleBackColor = True
        '
        'btnFileSystemValidSubtitlesExtsAdd
        '
        Me.btnFileSystemValidSubtitlesExtsAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFileSystemValidSubtitlesExtsAdd.Image = CType(resources.GetObject("btnFileSystemValidSubtitlesExtsAdd.Image"), System.Drawing.Image)
        Me.btnFileSystemValidSubtitlesExtsAdd.Location = New System.Drawing.Point(59, 123)
        Me.btnFileSystemValidSubtitlesExtsAdd.Name = "btnFileSystemValidSubtitlesExtsAdd"
        Me.btnFileSystemValidSubtitlesExtsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidSubtitlesExtsAdd.TabIndex = 2
        Me.btnFileSystemValidSubtitlesExtsAdd.UseVisualStyleBackColor = True
        '
        'lstFileSystemValidSubtitlesExts
        '
        Me.tblFileSystemValidSubtitlesExts.SetColumnSpan(Me.lstFileSystemValidSubtitlesExts, 3)
        Me.lstFileSystemValidSubtitlesExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstFileSystemValidSubtitlesExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstFileSystemValidSubtitlesExts.FormattingEnabled = True
        Me.lstFileSystemValidSubtitlesExts.Location = New System.Drawing.Point(3, 32)
        Me.lstFileSystemValidSubtitlesExts.Name = "lstFileSystemValidSubtitlesExts"
        Me.lstFileSystemValidSubtitlesExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemValidSubtitlesExts.Size = New System.Drawing.Size(150, 85)
        Me.lstFileSystemValidSubtitlesExts.Sorted = True
        Me.lstFileSystemValidSubtitlesExts.TabIndex = 0
        '
        'txtFileSystemValidSubtitlesExts
        '
        Me.txtFileSystemValidSubtitlesExts.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFileSystemValidSubtitlesExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileSystemValidSubtitlesExts.Location = New System.Drawing.Point(3, 123)
        Me.txtFileSystemValidSubtitlesExts.Name = "txtFileSystemValidSubtitlesExts"
        Me.txtFileSystemValidSubtitlesExts.Size = New System.Drawing.Size(50, 22)
        Me.txtFileSystemValidSubtitlesExts.TabIndex = 1
        '
        'gbFileSystemValidThemeExts
        '
        Me.gbFileSystemValidThemeExts.AutoSize = True
        Me.gbFileSystemValidThemeExts.Controls.Add(Me.tblFileSystemValidThemeExts)
        Me.gbFileSystemValidThemeExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFileSystemValidThemeExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbFileSystemValidThemeExts.Location = New System.Drawing.Point(171, 179)
        Me.gbFileSystemValidThemeExts.Name = "gbFileSystemValidThemeExts"
        Me.gbFileSystemValidThemeExts.Size = New System.Drawing.Size(162, 170)
        Me.gbFileSystemValidThemeExts.TabIndex = 3
        Me.gbFileSystemValidThemeExts.TabStop = False
        Me.gbFileSystemValidThemeExts.Text = "Valid Theme Extensions"
        '
        'tblFileSystemValidThemeExts
        '
        Me.tblFileSystemValidThemeExts.AutoSize = True
        Me.tblFileSystemValidThemeExts.ColumnCount = 4
        Me.tblFileSystemValidThemeExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidThemeExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidThemeExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidThemeExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFileSystemValidThemeExts.Controls.Add(Me.btnFileSystemValidThemeExtsRemove, 2, 2)
        Me.tblFileSystemValidThemeExts.Controls.Add(Me.btnFileSystemValidThemeExtsReset, 2, 0)
        Me.tblFileSystemValidThemeExts.Controls.Add(Me.btnFileSystemValidThemeExtsAdd, 1, 2)
        Me.tblFileSystemValidThemeExts.Controls.Add(Me.lstFileSystemValidThemeExts, 0, 1)
        Me.tblFileSystemValidThemeExts.Controls.Add(Me.txtFileSystemValidThemeExts, 0, 2)
        Me.tblFileSystemValidThemeExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFileSystemValidThemeExts.Location = New System.Drawing.Point(3, 18)
        Me.tblFileSystemValidThemeExts.Name = "tblFileSystemValidThemeExts"
        Me.tblFileSystemValidThemeExts.RowCount = 4
        Me.tblFileSystemValidThemeExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidThemeExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidThemeExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidThemeExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFileSystemValidThemeExts.Size = New System.Drawing.Size(156, 149)
        Me.tblFileSystemValidThemeExts.TabIndex = 9
        '
        'btnFileSystemValidThemeExtsRemove
        '
        Me.btnFileSystemValidThemeExtsRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFileSystemValidThemeExtsRemove.Image = CType(resources.GetObject("btnFileSystemValidThemeExtsRemove.Image"), System.Drawing.Image)
        Me.btnFileSystemValidThemeExtsRemove.Location = New System.Drawing.Point(130, 123)
        Me.btnFileSystemValidThemeExtsRemove.Name = "btnFileSystemValidThemeExtsRemove"
        Me.btnFileSystemValidThemeExtsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidThemeExtsRemove.TabIndex = 3
        Me.btnFileSystemValidThemeExtsRemove.UseVisualStyleBackColor = True
        '
        'btnFileSystemValidThemeExtsReset
        '
        Me.btnFileSystemValidThemeExtsReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnFileSystemValidThemeExtsReset.Image = CType(resources.GetObject("btnFileSystemValidThemeExtsReset.Image"), System.Drawing.Image)
        Me.btnFileSystemValidThemeExtsReset.Location = New System.Drawing.Point(130, 3)
        Me.btnFileSystemValidThemeExtsReset.Name = "btnFileSystemValidThemeExtsReset"
        Me.btnFileSystemValidThemeExtsReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidThemeExtsReset.TabIndex = 4
        Me.btnFileSystemValidThemeExtsReset.UseVisualStyleBackColor = True
        '
        'btnFileSystemValidThemeExtsAdd
        '
        Me.btnFileSystemValidThemeExtsAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFileSystemValidThemeExtsAdd.Image = CType(resources.GetObject("btnFileSystemValidThemeExtsAdd.Image"), System.Drawing.Image)
        Me.btnFileSystemValidThemeExtsAdd.Location = New System.Drawing.Point(59, 123)
        Me.btnFileSystemValidThemeExtsAdd.Name = "btnFileSystemValidThemeExtsAdd"
        Me.btnFileSystemValidThemeExtsAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnFileSystemValidThemeExtsAdd.TabIndex = 2
        Me.btnFileSystemValidThemeExtsAdd.UseVisualStyleBackColor = True
        '
        'lstFileSystemValidThemeExts
        '
        Me.tblFileSystemValidThemeExts.SetColumnSpan(Me.lstFileSystemValidThemeExts, 3)
        Me.lstFileSystemValidThemeExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstFileSystemValidThemeExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstFileSystemValidThemeExts.FormattingEnabled = True
        Me.lstFileSystemValidThemeExts.Location = New System.Drawing.Point(3, 32)
        Me.lstFileSystemValidThemeExts.Name = "lstFileSystemValidThemeExts"
        Me.lstFileSystemValidThemeExts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileSystemValidThemeExts.Size = New System.Drawing.Size(150, 85)
        Me.lstFileSystemValidThemeExts.Sorted = True
        Me.lstFileSystemValidThemeExts.TabIndex = 0
        '
        'txtFileSystemValidThemeExts
        '
        Me.txtFileSystemValidThemeExts.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFileSystemValidThemeExts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileSystemValidThemeExts.Location = New System.Drawing.Point(3, 123)
        Me.txtFileSystemValidThemeExts.Name = "txtFileSystemValidThemeExts"
        Me.txtFileSystemValidThemeExts.Size = New System.Drawing.Size(50, 22)
        Me.txtFileSystemValidThemeExts.TabIndex = 1
        '
        'gbGeneralVirtualDrive
        '
        Me.gbGeneralVirtualDrive.AutoSize = True
        Me.tblSettings.SetColumnSpan(Me.gbGeneralVirtualDrive, 3)
        Me.gbGeneralVirtualDrive.Controls.Add(Me.tblGeneralVirtualDrive)
        Me.gbGeneralVirtualDrive.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGeneralVirtualDrive.Location = New System.Drawing.Point(3, 499)
        Me.gbGeneralVirtualDrive.Name = "gbGeneralVirtualDrive"
        Me.gbGeneralVirtualDrive.Size = New System.Drawing.Size(611, 70)
        Me.gbGeneralVirtualDrive.TabIndex = 18
        Me.gbGeneralVirtualDrive.TabStop = False
        Me.gbGeneralVirtualDrive.Text = "Configuration ISO Filescanning"
        '
        'tblGeneralVirtualDrive
        '
        Me.tblGeneralVirtualDrive.AutoSize = True
        Me.tblGeneralVirtualDrive.ColumnCount = 3
        Me.tblGeneralVirtualDrive.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralVirtualDrive.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblGeneralVirtualDrive.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralVirtualDrive.Controls.Add(Me.btnGeneralVirtualDriveBinPathBrowse, 2, 1)
        Me.tblGeneralVirtualDrive.Controls.Add(Me.lblGeneralVirtualDriveLetter, 0, 0)
        Me.tblGeneralVirtualDrive.Controls.Add(Me.txtGeneralVirtualDriveBinPath, 1, 1)
        Me.tblGeneralVirtualDrive.Controls.Add(Me.cbGeneralVirtualDriveLetter, 0, 1)
        Me.tblGeneralVirtualDrive.Controls.Add(Me.lblGeneralVirtualDrivePath, 1, 0)
        Me.tblGeneralVirtualDrive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralVirtualDrive.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralVirtualDrive.Name = "tblGeneralVirtualDrive"
        Me.tblGeneralVirtualDrive.RowCount = 3
        Me.tblGeneralVirtualDrive.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblGeneralVirtualDrive.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralVirtualDrive.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralVirtualDrive.Size = New System.Drawing.Size(605, 49)
        Me.tblGeneralVirtualDrive.TabIndex = 17
        '
        'btnGeneralVirtualDriveBinPathBrowse
        '
        Me.btnGeneralVirtualDriveBinPathBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnGeneralVirtualDriveBinPathBrowse.Location = New System.Drawing.Point(577, 23)
        Me.btnGeneralVirtualDriveBinPathBrowse.Name = "btnGeneralVirtualDriveBinPathBrowse"
        Me.btnGeneralVirtualDriveBinPathBrowse.Size = New System.Drawing.Size(25, 23)
        Me.btnGeneralVirtualDriveBinPathBrowse.TabIndex = 4
        Me.btnGeneralVirtualDriveBinPathBrowse.Text = "..."
        Me.btnGeneralVirtualDriveBinPathBrowse.UseVisualStyleBackColor = True
        '
        'lblGeneralVirtualDriveLetter
        '
        Me.lblGeneralVirtualDriveLetter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralVirtualDriveLetter.AutoSize = True
        Me.lblGeneralVirtualDriveLetter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGeneralVirtualDriveLetter.Location = New System.Drawing.Point(3, 3)
        Me.lblGeneralVirtualDriveLetter.Name = "lblGeneralVirtualDriveLetter"
        Me.lblGeneralVirtualDriveLetter.Size = New System.Drawing.Size(60, 13)
        Me.lblGeneralVirtualDriveLetter.TabIndex = 6
        Me.lblGeneralVirtualDriveLetter.Text = "Driveletter"
        '
        'txtGeneralVirtualDriveBinPath
        '
        Me.txtGeneralVirtualDriveBinPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGeneralVirtualDriveBinPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGeneralVirtualDriveBinPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGeneralVirtualDriveBinPath.Location = New System.Drawing.Point(82, 23)
        Me.txtGeneralVirtualDriveBinPath.Name = "txtGeneralVirtualDriveBinPath"
        Me.txtGeneralVirtualDriveBinPath.Size = New System.Drawing.Size(489, 22)
        Me.txtGeneralVirtualDriveBinPath.TabIndex = 3
        '
        'cbGeneralVirtualDriveLetter
        '
        Me.cbGeneralVirtualDriveLetter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbGeneralVirtualDriveLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGeneralVirtualDriveLetter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbGeneralVirtualDriveLetter.FormattingEnabled = True
        Me.cbGeneralVirtualDriveLetter.Items.AddRange(New Object() {"", "A", "B", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbGeneralVirtualDriveLetter.Location = New System.Drawing.Point(3, 24)
        Me.cbGeneralVirtualDriveLetter.Name = "cbGeneralVirtualDriveLetter"
        Me.cbGeneralVirtualDriveLetter.Size = New System.Drawing.Size(73, 21)
        Me.cbGeneralVirtualDriveLetter.TabIndex = 7
        '
        'lblGeneralVirtualDrivePath
        '
        Me.lblGeneralVirtualDrivePath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralVirtualDrivePath.AutoSize = True
        Me.tblGeneralVirtualDrive.SetColumnSpan(Me.lblGeneralVirtualDrivePath, 2)
        Me.lblGeneralVirtualDrivePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGeneralVirtualDrivePath.Location = New System.Drawing.Point(82, 3)
        Me.lblGeneralVirtualDrivePath.Name = "lblGeneralVirtualDrivePath"
        Me.lblGeneralVirtualDrivePath.Size = New System.Drawing.Size(226, 13)
        Me.lblGeneralVirtualDrivePath.TabIndex = 2
        Me.lblGeneralVirtualDrivePath.Text = "Path to VCDMount.exe (Virtual CloneDrive)"
        '
        'frmOption_FileSystem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(849, 655)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmOption_FileSystem"
        Me.Text = "frmOption_FileSystem"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbFileSystemValidVideoExts.ResumeLayout(False)
        Me.gbFileSystemValidVideoExts.PerformLayout()
        Me.tblFileSystemValidVideoExts.ResumeLayout(False)
        Me.tblFileSystemValidVideoExts.PerformLayout()
        Me.gbFileSystemNoStackExts.ResumeLayout(False)
        Me.gbFileSystemNoStackExts.PerformLayout()
        Me.tblFileSystemNoStackExts.ResumeLayout(False)
        Me.tblFileSystemNoStackExts.PerformLayout()
        Me.gbFileSystemExcludedPaths.ResumeLayout(False)
        Me.gbFileSystemExcludedPaths.PerformLayout()
        Me.tblFileSystemExcludedPaths.ResumeLayout(False)
        Me.tblFileSystemExcludedPaths.PerformLayout()
        Me.gbFileSystemValidSubtitlesExts.ResumeLayout(False)
        Me.gbFileSystemValidSubtitlesExts.PerformLayout()
        Me.tblFileSystemValidSubtitlesExts.ResumeLayout(False)
        Me.tblFileSystemValidSubtitlesExts.PerformLayout()
        Me.gbFileSystemValidThemeExts.ResumeLayout(False)
        Me.gbFileSystemValidThemeExts.PerformLayout()
        Me.tblFileSystemValidThemeExts.ResumeLayout(False)
        Me.tblFileSystemValidThemeExts.PerformLayout()
        Me.gbGeneralVirtualDrive.ResumeLayout(False)
        Me.gbGeneralVirtualDrive.PerformLayout()
        Me.tblGeneralVirtualDrive.ResumeLayout(False)
        Me.tblGeneralVirtualDrive.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbFileSystemValidVideoExts As GroupBox
    Friend WithEvents tblFileSystemValidVideoExts As TableLayoutPanel
    Friend WithEvents btnFileSystemValidVideoExtsRemove As Button
    Friend WithEvents btnFileSystemValidVideoExtsReset As Button
    Friend WithEvents btnFileSystemValidVideoExtsAdd As Button
    Friend WithEvents lstFileSystemValidVideoExts As ListBox
    Friend WithEvents txtFileSystemValidVideoExts As TextBox
    Friend WithEvents gbFileSystemNoStackExts As GroupBox
    Friend WithEvents tblFileSystemNoStackExts As TableLayoutPanel
    Friend WithEvents btnFileSystemNoStackExtsRemove As Button
    Friend WithEvents lstFileSystemNoStackExts As ListBox
    Friend WithEvents btnFileSystemNoStackExtsAdd As Button
    Friend WithEvents txtFileSystemNoStackExts As TextBox
    Friend WithEvents gbFileSystemExcludedPaths As GroupBox
    Friend WithEvents tblFileSystemExcludedPaths As TableLayoutPanel
    Friend WithEvents btnFileSystemExcludedPathsRemove As Button
    Friend WithEvents lstFileSystemExcludedPaths As ListBox
    Friend WithEvents txtFileSystemExcludedPaths As TextBox
    Friend WithEvents btnFileSystemExcludedPathsAdd As Button
    Friend WithEvents btnFileSystemExcludedPathsBrowse As Button
    Friend WithEvents gbFileSystemValidSubtitlesExts As GroupBox
    Friend WithEvents tblFileSystemValidSubtitlesExts As TableLayoutPanel
    Friend WithEvents btnFileSystemValidSubtitlesExtsRemove As Button
    Friend WithEvents btnFileSystemValidSubtitlesExtsReset As Button
    Friend WithEvents btnFileSystemValidSubtitlesExtsAdd As Button
    Friend WithEvents lstFileSystemValidSubtitlesExts As ListBox
    Friend WithEvents txtFileSystemValidSubtitlesExts As TextBox
    Friend WithEvents gbFileSystemValidThemeExts As GroupBox
    Friend WithEvents tblFileSystemValidThemeExts As TableLayoutPanel
    Friend WithEvents btnFileSystemValidThemeExtsRemove As Button
    Friend WithEvents btnFileSystemValidThemeExtsReset As Button
    Friend WithEvents btnFileSystemValidThemeExtsAdd As Button
    Friend WithEvents lstFileSystemValidThemeExts As ListBox
    Friend WithEvents txtFileSystemValidThemeExts As TextBox
    Friend WithEvents fbdBrowse As FolderBrowserDialog
    Friend WithEvents gbGeneralVirtualDrive As GroupBox
    Friend WithEvents tblGeneralVirtualDrive As TableLayoutPanel
    Friend WithEvents btnGeneralVirtualDriveBinPathBrowse As Button
    Friend WithEvents lblGeneralVirtualDriveLetter As Label
    Friend WithEvents txtGeneralVirtualDriveBinPath As TextBox
    Friend WithEvents cbGeneralVirtualDriveLetter As ComboBox
    Friend WithEvents lblGeneralVirtualDrivePath As Label
    Friend WithEvents fileBrowse As OpenFileDialog
End Class
