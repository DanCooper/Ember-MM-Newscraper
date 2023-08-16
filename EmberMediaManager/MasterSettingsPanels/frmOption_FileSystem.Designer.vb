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
        Me.gbValidVideoExtensions = New System.Windows.Forms.GroupBox()
        Me.tblValidVideoExtensions = New System.Windows.Forms.TableLayoutPanel()
        Me.btnValidVideoExtensionsDefaults = New System.Windows.Forms.Button()
        Me.dgvValidVideoExtensions = New System.Windows.Forms.DataGridView()
        Me.colVideoExtensions = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gbExcludedPaths = New System.Windows.Forms.GroupBox()
        Me.tblExcludedPaths = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvExcludedPaths = New System.Windows.Forms.DataGridView()
        Me.colExcludedPathsPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExcludedPathsBrowse = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.gbValidSubtitlesExtensions = New System.Windows.Forms.GroupBox()
        Me.tblValidSubtitlesExtensions = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvValidSubtitleExtensions = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnValidSubtitleExtensionsDefaults = New System.Windows.Forms.Button()
        Me.gbValidThemeExtensions = New System.Windows.Forms.GroupBox()
        Me.tblValidThemeExts = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvValidThemeExtensions = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnValidThemeExtensionsDefaults = New System.Windows.Forms.Button()
        Me.gbVirtualDrive = New System.Windows.Forms.GroupBox()
        Me.tblVirtualDrive = New System.Windows.Forms.TableLayoutPanel()
        Me.btnVirtualDrivePathBrowse = New System.Windows.Forms.Button()
        Me.lblVirtualDriveDriveLetter = New System.Windows.Forms.Label()
        Me.txtVirtualDrivePath = New System.Windows.Forms.TextBox()
        Me.cbVirtualDriveDriveLetter = New System.Windows.Forms.ComboBox()
        Me.lblVirtualDriveDrivePath = New System.Windows.Forms.Label()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.fileBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbValidVideoExtensions.SuspendLayout()
        Me.tblValidVideoExtensions.SuspendLayout()
        CType(Me.dgvValidVideoExtensions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbExcludedPaths.SuspendLayout()
        Me.tblExcludedPaths.SuspendLayout()
        CType(Me.dgvExcludedPaths, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbValidSubtitlesExtensions.SuspendLayout()
        Me.tblValidSubtitlesExtensions.SuspendLayout()
        CType(Me.dgvValidSubtitleExtensions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbValidThemeExtensions.SuspendLayout()
        Me.tblValidThemeExts.SuspendLayout()
        CType(Me.dgvValidThemeExtensions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbVirtualDrive.SuspendLayout()
        Me.tblVirtualDrive.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(632, 738)
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
        Me.tblSettings.Controls.Add(Me.gbValidVideoExtensions, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbExcludedPaths, 0, 1)
        Me.tblSettings.Controls.Add(Me.gbValidSubtitlesExtensions, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbValidThemeExtensions, 2, 0)
        Me.tblSettings.Controls.Add(Me.gbVirtualDrive, 0, 2)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 4
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(632, 738)
        Me.tblSettings.TabIndex = 6
        '
        'gbValidVideoExtensions
        '
        Me.gbValidVideoExtensions.AutoSize = True
        Me.gbValidVideoExtensions.Controls.Add(Me.tblValidVideoExtensions)
        Me.gbValidVideoExtensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbValidVideoExtensions.Location = New System.Drawing.Point(3, 3)
        Me.gbValidVideoExtensions.Name = "gbValidVideoExtensions"
        Me.gbValidVideoExtensions.Size = New System.Drawing.Size(172, 473)
        Me.gbValidVideoExtensions.TabIndex = 0
        Me.gbValidVideoExtensions.TabStop = False
        Me.gbValidVideoExtensions.Text = "Valid Video Extensions"
        '
        'tblValidVideoExtensions
        '
        Me.tblValidVideoExtensions.AutoSize = True
        Me.tblValidVideoExtensions.ColumnCount = 1
        Me.tblValidVideoExtensions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblValidVideoExtensions.Controls.Add(Me.btnValidVideoExtensionsDefaults, 0, 1)
        Me.tblValidVideoExtensions.Controls.Add(Me.dgvValidVideoExtensions, 0, 0)
        Me.tblValidVideoExtensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblValidVideoExtensions.Location = New System.Drawing.Point(3, 18)
        Me.tblValidVideoExtensions.Name = "tblValidVideoExtensions"
        Me.tblValidVideoExtensions.RowCount = 2
        Me.tblValidVideoExtensions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblValidVideoExtensions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblValidVideoExtensions.Size = New System.Drawing.Size(166, 452)
        Me.tblValidVideoExtensions.TabIndex = 7
        '
        'btnValidVideoExtensionsDefaults
        '
        Me.btnValidVideoExtensionsDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValidVideoExtensionsDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidVideoExtensionsDefaults.Image = CType(resources.GetObject("btnValidVideoExtensionsDefaults.Image"), System.Drawing.Image)
        Me.btnValidVideoExtensionsDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnValidVideoExtensionsDefaults.Location = New System.Drawing.Point(58, 426)
        Me.btnValidVideoExtensionsDefaults.Name = "btnValidVideoExtensionsDefaults"
        Me.btnValidVideoExtensionsDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnValidVideoExtensionsDefaults.TabIndex = 6
        Me.btnValidVideoExtensionsDefaults.Text = "Defaults"
        Me.btnValidVideoExtensionsDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnValidVideoExtensionsDefaults.UseVisualStyleBackColor = True
        '
        'dgvValidVideoExtensions
        '
        Me.dgvValidVideoExtensions.AllowUserToResizeRows = False
        Me.dgvValidVideoExtensions.BackgroundColor = System.Drawing.Color.White
        Me.dgvValidVideoExtensions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvValidVideoExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvValidVideoExtensions.ColumnHeadersVisible = False
        Me.dgvValidVideoExtensions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colVideoExtensions})
        Me.dgvValidVideoExtensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvValidVideoExtensions.Location = New System.Drawing.Point(3, 3)
        Me.dgvValidVideoExtensions.Name = "dgvValidVideoExtensions"
        Me.dgvValidVideoExtensions.RowHeadersWidth = 25
        Me.dgvValidVideoExtensions.ShowCellErrors = False
        Me.dgvValidVideoExtensions.ShowCellToolTips = False
        Me.dgvValidVideoExtensions.ShowRowErrors = False
        Me.dgvValidVideoExtensions.Size = New System.Drawing.Size(160, 417)
        Me.dgvValidVideoExtensions.TabIndex = 5
        '
        'colVideoExtensions
        '
        Me.colVideoExtensions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colVideoExtensions.HeaderText = "Extension"
        Me.colVideoExtensions.Name = "colVideoExtensions"
        '
        'gbExcludedPaths
        '
        Me.gbExcludedPaths.AutoSize = True
        Me.tblSettings.SetColumnSpan(Me.gbExcludedPaths, 3)
        Me.gbExcludedPaths.Controls.Add(Me.tblExcludedPaths)
        Me.gbExcludedPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbExcludedPaths.Location = New System.Drawing.Point(3, 482)
        Me.gbExcludedPaths.Name = "gbExcludedPaths"
        Me.gbExcludedPaths.Size = New System.Drawing.Size(528, 177)
        Me.gbExcludedPaths.TabIndex = 4
        Me.gbExcludedPaths.TabStop = False
        Me.gbExcludedPaths.Text = "Excluded Paths"
        '
        'tblExcludedPaths
        '
        Me.tblExcludedPaths.AutoSize = True
        Me.tblExcludedPaths.ColumnCount = 1
        Me.tblExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblExcludedPaths.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblExcludedPaths.Controls.Add(Me.dgvExcludedPaths, 0, 0)
        Me.tblExcludedPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblExcludedPaths.Location = New System.Drawing.Point(3, 18)
        Me.tblExcludedPaths.Name = "tblExcludedPaths"
        Me.tblExcludedPaths.RowCount = 1
        Me.tblExcludedPaths.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblExcludedPaths.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156.0!))
        Me.tblExcludedPaths.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156.0!))
        Me.tblExcludedPaths.Size = New System.Drawing.Size(522, 156)
        Me.tblExcludedPaths.TabIndex = 7
        '
        'dgvExcludedPaths
        '
        Me.dgvExcludedPaths.AllowUserToResizeRows = False
        Me.dgvExcludedPaths.BackgroundColor = System.Drawing.Color.White
        Me.dgvExcludedPaths.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvExcludedPaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvExcludedPaths.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colExcludedPathsPath, Me.colExcludedPathsBrowse})
        Me.dgvExcludedPaths.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvExcludedPaths.Location = New System.Drawing.Point(3, 3)
        Me.dgvExcludedPaths.Name = "dgvExcludedPaths"
        Me.dgvExcludedPaths.RowHeadersWidth = 25
        Me.dgvExcludedPaths.ShowCellErrors = False
        Me.dgvExcludedPaths.ShowCellToolTips = False
        Me.dgvExcludedPaths.ShowRowErrors = False
        Me.dgvExcludedPaths.Size = New System.Drawing.Size(516, 150)
        Me.dgvExcludedPaths.TabIndex = 8
        '
        'colExcludedPathsPath
        '
        Me.colExcludedPathsPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colExcludedPathsPath.HeaderText = "Full Path"
        Me.colExcludedPathsPath.Name = "colExcludedPathsPath"
        '
        'colExcludedPathsBrowse
        '
        Me.colExcludedPathsBrowse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.colExcludedPathsBrowse.HeaderText = "Browse"
        Me.colExcludedPathsBrowse.Name = "colExcludedPathsBrowse"
        Me.colExcludedPathsBrowse.Text = ""
        Me.colExcludedPathsBrowse.Width = 51
        '
        'gbValidSubtitlesExtensions
        '
        Me.gbValidSubtitlesExtensions.AutoSize = True
        Me.gbValidSubtitlesExtensions.Controls.Add(Me.tblValidSubtitlesExtensions)
        Me.gbValidSubtitlesExtensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbValidSubtitlesExtensions.Location = New System.Drawing.Point(181, 3)
        Me.gbValidSubtitlesExtensions.Name = "gbValidSubtitlesExtensions"
        Me.gbValidSubtitlesExtensions.Size = New System.Drawing.Size(172, 473)
        Me.gbValidSubtitlesExtensions.TabIndex = 5
        Me.gbValidSubtitlesExtensions.TabStop = False
        Me.gbValidSubtitlesExtensions.Text = "Valid Subtitles Extensions"
        '
        'tblValidSubtitlesExtensions
        '
        Me.tblValidSubtitlesExtensions.AutoSize = True
        Me.tblValidSubtitlesExtensions.ColumnCount = 1
        Me.tblValidSubtitlesExtensions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblValidSubtitlesExtensions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblValidSubtitlesExtensions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblValidSubtitlesExtensions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblValidSubtitlesExtensions.Controls.Add(Me.dgvValidSubtitleExtensions, 0, 0)
        Me.tblValidSubtitlesExtensions.Controls.Add(Me.btnValidSubtitleExtensionsDefaults, 0, 1)
        Me.tblValidSubtitlesExtensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblValidSubtitlesExtensions.Location = New System.Drawing.Point(3, 18)
        Me.tblValidSubtitlesExtensions.Name = "tblValidSubtitlesExtensions"
        Me.tblValidSubtitlesExtensions.RowCount = 2
        Me.tblValidSubtitlesExtensions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblValidSubtitlesExtensions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblValidSubtitlesExtensions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblValidSubtitlesExtensions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblValidSubtitlesExtensions.Size = New System.Drawing.Size(166, 452)
        Me.tblValidSubtitlesExtensions.TabIndex = 8
        '
        'dgvValidSubtitleExtensions
        '
        Me.dgvValidSubtitleExtensions.AllowUserToResizeRows = False
        Me.dgvValidSubtitleExtensions.BackgroundColor = System.Drawing.Color.White
        Me.dgvValidSubtitleExtensions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvValidSubtitleExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvValidSubtitleExtensions.ColumnHeadersVisible = False
        Me.dgvValidSubtitleExtensions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1})
        Me.dgvValidSubtitleExtensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvValidSubtitleExtensions.Location = New System.Drawing.Point(3, 3)
        Me.dgvValidSubtitleExtensions.Name = "dgvValidSubtitleExtensions"
        Me.dgvValidSubtitleExtensions.RowHeadersWidth = 25
        Me.dgvValidSubtitleExtensions.ShowCellErrors = False
        Me.dgvValidSubtitleExtensions.ShowCellToolTips = False
        Me.dgvValidSubtitleExtensions.ShowRowErrors = False
        Me.dgvValidSubtitleExtensions.Size = New System.Drawing.Size(160, 417)
        Me.dgvValidSubtitleExtensions.TabIndex = 7
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn1.HeaderText = "Extension"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'btnValidSubtitleExtensionsDefaults
        '
        Me.btnValidSubtitleExtensionsDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValidSubtitleExtensionsDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidSubtitleExtensionsDefaults.Image = CType(resources.GetObject("btnValidSubtitleExtensionsDefaults.Image"), System.Drawing.Image)
        Me.btnValidSubtitleExtensionsDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnValidSubtitleExtensionsDefaults.Location = New System.Drawing.Point(58, 426)
        Me.btnValidSubtitleExtensionsDefaults.Name = "btnValidSubtitleExtensionsDefaults"
        Me.btnValidSubtitleExtensionsDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnValidSubtitleExtensionsDefaults.TabIndex = 6
        Me.btnValidSubtitleExtensionsDefaults.Text = "Defaults"
        Me.btnValidSubtitleExtensionsDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnValidSubtitleExtensionsDefaults.UseVisualStyleBackColor = True
        '
        'gbValidThemeExtensions
        '
        Me.gbValidThemeExtensions.AutoSize = True
        Me.gbValidThemeExtensions.Controls.Add(Me.tblValidThemeExts)
        Me.gbValidThemeExtensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbValidThemeExtensions.Location = New System.Drawing.Point(359, 3)
        Me.gbValidThemeExtensions.Name = "gbValidThemeExtensions"
        Me.gbValidThemeExtensions.Size = New System.Drawing.Size(172, 473)
        Me.gbValidThemeExtensions.TabIndex = 3
        Me.gbValidThemeExtensions.TabStop = False
        Me.gbValidThemeExtensions.Text = "Valid Theme Extensions"
        '
        'tblValidThemeExts
        '
        Me.tblValidThemeExts.AutoSize = True
        Me.tblValidThemeExts.ColumnCount = 1
        Me.tblValidThemeExts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblValidThemeExts.Controls.Add(Me.dgvValidThemeExtensions, 0, 0)
        Me.tblValidThemeExts.Controls.Add(Me.btnValidThemeExtensionsDefaults, 0, 1)
        Me.tblValidThemeExts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblValidThemeExts.Location = New System.Drawing.Point(3, 18)
        Me.tblValidThemeExts.Name = "tblValidThemeExts"
        Me.tblValidThemeExts.RowCount = 2
        Me.tblValidThemeExts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblValidThemeExts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblValidThemeExts.Size = New System.Drawing.Size(166, 452)
        Me.tblValidThemeExts.TabIndex = 9
        '
        'dgvValidThemeExtensions
        '
        Me.dgvValidThemeExtensions.AllowUserToResizeRows = False
        Me.dgvValidThemeExtensions.BackgroundColor = System.Drawing.Color.White
        Me.dgvValidThemeExtensions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvValidThemeExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvValidThemeExtensions.ColumnHeadersVisible = False
        Me.dgvValidThemeExtensions.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2})
        Me.dgvValidThemeExtensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvValidThemeExtensions.Location = New System.Drawing.Point(3, 3)
        Me.dgvValidThemeExtensions.Name = "dgvValidThemeExtensions"
        Me.dgvValidThemeExtensions.RowHeadersWidth = 25
        Me.dgvValidThemeExtensions.ShowCellErrors = False
        Me.dgvValidThemeExtensions.ShowCellToolTips = False
        Me.dgvValidThemeExtensions.ShowRowErrors = False
        Me.dgvValidThemeExtensions.Size = New System.Drawing.Size(160, 417)
        Me.dgvValidThemeExtensions.TabIndex = 8
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn2.HeaderText = "Extension"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'btnValidThemeExtensionsDefaults
        '
        Me.btnValidThemeExtensionsDefaults.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValidThemeExtensionsDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidThemeExtensionsDefaults.Image = CType(resources.GetObject("btnValidThemeExtensionsDefaults.Image"), System.Drawing.Image)
        Me.btnValidThemeExtensionsDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnValidThemeExtensionsDefaults.Location = New System.Drawing.Point(58, 426)
        Me.btnValidThemeExtensionsDefaults.Name = "btnValidThemeExtensionsDefaults"
        Me.btnValidThemeExtensionsDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnValidThemeExtensionsDefaults.TabIndex = 6
        Me.btnValidThemeExtensionsDefaults.Text = "Defaults"
        Me.btnValidThemeExtensionsDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnValidThemeExtensionsDefaults.UseVisualStyleBackColor = True
        '
        'gbVirtualDrive
        '
        Me.gbVirtualDrive.AutoSize = True
        Me.tblSettings.SetColumnSpan(Me.gbVirtualDrive, 3)
        Me.gbVirtualDrive.Controls.Add(Me.tblVirtualDrive)
        Me.gbVirtualDrive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbVirtualDrive.Location = New System.Drawing.Point(3, 665)
        Me.gbVirtualDrive.Name = "gbVirtualDrive"
        Me.gbVirtualDrive.Size = New System.Drawing.Size(528, 70)
        Me.gbVirtualDrive.TabIndex = 18
        Me.gbVirtualDrive.TabStop = False
        Me.gbVirtualDrive.Text = "Virtual Drive"
        '
        'tblVirtualDrive
        '
        Me.tblVirtualDrive.AutoSize = True
        Me.tblVirtualDrive.ColumnCount = 3
        Me.tblVirtualDrive.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblVirtualDrive.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblVirtualDrive.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblVirtualDrive.Controls.Add(Me.btnVirtualDrivePathBrowse, 2, 1)
        Me.tblVirtualDrive.Controls.Add(Me.lblVirtualDriveDriveLetter, 0, 0)
        Me.tblVirtualDrive.Controls.Add(Me.txtVirtualDrivePath, 1, 1)
        Me.tblVirtualDrive.Controls.Add(Me.cbVirtualDriveDriveLetter, 0, 1)
        Me.tblVirtualDrive.Controls.Add(Me.lblVirtualDriveDrivePath, 1, 0)
        Me.tblVirtualDrive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblVirtualDrive.Location = New System.Drawing.Point(3, 18)
        Me.tblVirtualDrive.Name = "tblVirtualDrive"
        Me.tblVirtualDrive.RowCount = 3
        Me.tblVirtualDrive.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblVirtualDrive.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblVirtualDrive.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblVirtualDrive.Size = New System.Drawing.Size(522, 49)
        Me.tblVirtualDrive.TabIndex = 17
        '
        'btnVirtualDrivePathBrowse
        '
        Me.btnVirtualDrivePathBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnVirtualDrivePathBrowse.Location = New System.Drawing.Point(494, 23)
        Me.btnVirtualDrivePathBrowse.Name = "btnVirtualDrivePathBrowse"
        Me.btnVirtualDrivePathBrowse.Size = New System.Drawing.Size(25, 23)
        Me.btnVirtualDrivePathBrowse.TabIndex = 4
        Me.btnVirtualDrivePathBrowse.Text = "..."
        Me.btnVirtualDrivePathBrowse.UseVisualStyleBackColor = True
        '
        'lblVirtualDriveDriveLetter
        '
        Me.lblVirtualDriveDriveLetter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVirtualDriveDriveLetter.AutoSize = True
        Me.lblVirtualDriveDriveLetter.Location = New System.Drawing.Point(3, 3)
        Me.lblVirtualDriveDriveLetter.Name = "lblVirtualDriveDriveLetter"
        Me.lblVirtualDriveDriveLetter.Size = New System.Drawing.Size(60, 13)
        Me.lblVirtualDriveDriveLetter.TabIndex = 6
        Me.lblVirtualDriveDriveLetter.Text = "Driveletter"
        '
        'txtVirtualDrivePath
        '
        Me.txtVirtualDrivePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVirtualDrivePath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtVirtualDrivePath.Location = New System.Drawing.Point(82, 23)
        Me.txtVirtualDrivePath.Name = "txtVirtualDrivePath"
        Me.txtVirtualDrivePath.Size = New System.Drawing.Size(406, 22)
        Me.txtVirtualDrivePath.TabIndex = 3
        '
        'cbVirtualDriveDriveLetter
        '
        Me.cbVirtualDriveDriveLetter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbVirtualDriveDriveLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVirtualDriveDriveLetter.FormattingEnabled = True
        Me.cbVirtualDriveDriveLetter.Items.AddRange(New Object() {"", "A", "B", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbVirtualDriveDriveLetter.Location = New System.Drawing.Point(3, 24)
        Me.cbVirtualDriveDriveLetter.Name = "cbVirtualDriveDriveLetter"
        Me.cbVirtualDriveDriveLetter.Size = New System.Drawing.Size(73, 21)
        Me.cbVirtualDriveDriveLetter.TabIndex = 7
        '
        'lblVirtualDriveDrivePath
        '
        Me.lblVirtualDriveDrivePath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVirtualDriveDrivePath.AutoSize = True
        Me.tblVirtualDrive.SetColumnSpan(Me.lblVirtualDriveDrivePath, 2)
        Me.lblVirtualDriveDrivePath.Location = New System.Drawing.Point(82, 3)
        Me.lblVirtualDriveDrivePath.Name = "lblVirtualDriveDrivePath"
        Me.lblVirtualDriveDrivePath.Size = New System.Drawing.Size(226, 13)
        Me.lblVirtualDriveDrivePath.TabIndex = 2
        Me.lblVirtualDriveDrivePath.Text = "Path to VCDMount.exe (Virtual CloneDrive)"
        '
        'frmOption_FileSystem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 738)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmOption_FileSystem"
        Me.Text = "frmOption_FileSystem"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbValidVideoExtensions.ResumeLayout(False)
        Me.gbValidVideoExtensions.PerformLayout()
        Me.tblValidVideoExtensions.ResumeLayout(False)
        CType(Me.dgvValidVideoExtensions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbExcludedPaths.ResumeLayout(False)
        Me.gbExcludedPaths.PerformLayout()
        Me.tblExcludedPaths.ResumeLayout(False)
        CType(Me.dgvExcludedPaths, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbValidSubtitlesExtensions.ResumeLayout(False)
        Me.gbValidSubtitlesExtensions.PerformLayout()
        Me.tblValidSubtitlesExtensions.ResumeLayout(False)
        CType(Me.dgvValidSubtitleExtensions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbValidThemeExtensions.ResumeLayout(False)
        Me.gbValidThemeExtensions.PerformLayout()
        Me.tblValidThemeExts.ResumeLayout(False)
        CType(Me.dgvValidThemeExtensions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbVirtualDrive.ResumeLayout(False)
        Me.gbVirtualDrive.PerformLayout()
        Me.tblVirtualDrive.ResumeLayout(False)
        Me.tblVirtualDrive.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbValidVideoExtensions As GroupBox
    Friend WithEvents tblValidVideoExtensions As TableLayoutPanel
    Friend WithEvents gbExcludedPaths As GroupBox
    Friend WithEvents tblExcludedPaths As TableLayoutPanel
    Friend WithEvents gbValidSubtitlesExtensions As GroupBox
    Friend WithEvents tblValidSubtitlesExtensions As TableLayoutPanel
    Friend WithEvents gbValidThemeExtensions As GroupBox
    Friend WithEvents tblValidThemeExts As TableLayoutPanel
    Friend WithEvents fbdBrowse As FolderBrowserDialog
    Friend WithEvents gbVirtualDrive As GroupBox
    Friend WithEvents tblVirtualDrive As TableLayoutPanel
    Friend WithEvents btnVirtualDrivePathBrowse As Button
    Friend WithEvents lblVirtualDriveDriveLetter As Label
    Friend WithEvents txtVirtualDrivePath As TextBox
    Friend WithEvents cbVirtualDriveDriveLetter As ComboBox
    Friend WithEvents lblVirtualDriveDrivePath As Label
    Friend WithEvents fileBrowse As OpenFileDialog
    Friend WithEvents dgvValidVideoExtensions As DataGridView
    Friend WithEvents btnValidVideoExtensionsDefaults As Button
    Friend WithEvents colVideoExtensions As DataGridViewTextBoxColumn
    Friend WithEvents dgvValidSubtitleExtensions As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents btnValidSubtitleExtensionsDefaults As Button
    Friend WithEvents dgvValidThemeExtensions As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents btnValidThemeExtensionsDefaults As Button
    Friend WithEvents dgvExcludedPaths As DataGridView
    Friend WithEvents colExcludedPathsPath As DataGridViewTextBoxColumn
    Friend WithEvents colExcludedPathsBrowse As DataGridViewButtonColumn
End Class
