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
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTips = New System.Windows.Forms.Label()
        Me.chkBulkRenamer = New System.Windows.Forms.CheckBox()
        Me.chkGenericModule = New System.Windows.Forms.CheckBox()
        Me.gbRenamerPatternsMovies = New System.Windows.Forms.GroupBox()
        Me.tblRenamerPatterns = New System.Windows.Forms.TableLayoutPanel()
        Me.chkRenameSingleMovies = New System.Windows.Forms.CheckBox()
        Me.lblFolderPatternMovies = New System.Windows.Forms.Label()
        Me.chkRenameMultiMovies = New System.Windows.Forms.CheckBox()
        Me.txtFolderPatternMovies = New System.Windows.Forms.TextBox()
        Me.txtFilePatternMovies = New System.Windows.Forms.TextBox()
        Me.lblFilePatternMovies = New System.Windows.Forms.Label()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.gbRenamerPatternsShows = New System.Windows.Forms.GroupBox()
        Me.tblRenamerPatternsShows = New System.Windows.Forms.TableLayoutPanel()
        Me.chkRenameSingleShows = New System.Windows.Forms.CheckBox()
        Me.lblFolderPatternShows = New System.Windows.Forms.Label()
        Me.chkRenameMultiShows = New System.Windows.Forms.CheckBox()
        Me.txtFolderPatternShows = New System.Windows.Forms.TextBox()
        Me.txtFilePatternEpisodes = New System.Windows.Forms.TextBox()
        Me.lblFilePatternEpisodes = New System.Windows.Forms.Label()
        Me.lblFolderPatternSeasons = New System.Windows.Forms.Label()
        Me.txtFolderPatternSeasons = New System.Windows.Forms.TextBox()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbRenamerPatternsMovies.SuspendLayout()
        Me.tblRenamerPatterns.SuspendLayout()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.gbRenamerPatternsShows.SuspendLayout()
        Me.tblRenamerPatternsShows.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(364, 500)
        Me.pnlSettings.TabIndex = 84
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(364, 477)
        Me.pnlSettingsMain.TabIndex = 5
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 3
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbRenamerPatternsShows, 0, 3)
        Me.tblSettingsMain.Controls.Add(Me.chkBulkRenamer, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.chkGenericModule, 0, 1)
        Me.tblSettingsMain.Controls.Add(Me.gbRenamerPatternsMovies, 0, 2)
        Me.tblSettingsMain.Controls.Add(Me.lblTips, 1, 0)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 5
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsMain.Size = New System.Drawing.Size(364, 477)
        Me.tblSettingsMain.TabIndex = 6
        '
        'lblTips
        '
        Me.lblTips.AutoSize = True
        Me.lblTips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTips.Location = New System.Drawing.Point(302, 0)
        Me.lblTips.MaximumSize = New System.Drawing.Size(475, 360)
        Me.lblTips.Name = "lblTips"
        Me.tblSettingsMain.SetRowSpan(Me.lblTips, 4)
        Me.lblTips.Size = New System.Drawing.Size(42, 15)
        Me.lblTips.TabIndex = 4
        Me.lblTips.Text = "Label1"
        '
        'chkBulkRenamer
        '
        Me.chkBulkRenamer.AutoSize = True
        Me.chkBulkRenamer.Location = New System.Drawing.Point(3, 3)
        Me.chkBulkRenamer.Name = "chkBulkRenamer"
        Me.chkBulkRenamer.Size = New System.Drawing.Size(160, 17)
        Me.chkBulkRenamer.TabIndex = 2
        Me.chkBulkRenamer.Text = "Enable Bulk Renamer Tool"
        Me.chkBulkRenamer.UseVisualStyleBackColor = True
        '
        'chkGenericModule
        '
        Me.chkGenericModule.AutoSize = True
        Me.chkGenericModule.Location = New System.Drawing.Point(3, 26)
        Me.chkGenericModule.Name = "chkGenericModule"
        Me.chkGenericModule.Size = New System.Drawing.Size(190, 17)
        Me.chkGenericModule.TabIndex = 1
        Me.chkGenericModule.Text = "Enable Generic Rename Module"
        Me.chkGenericModule.UseVisualStyleBackColor = True
        '
        'gbRenamerPatternsMovies
        '
        Me.gbRenamerPatternsMovies.AutoSize = True
        Me.gbRenamerPatternsMovies.Controls.Add(Me.tblRenamerPatterns)
        Me.gbRenamerPatternsMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbRenamerPatternsMovies.Location = New System.Drawing.Point(3, 49)
        Me.gbRenamerPatternsMovies.Name = "gbRenamerPatternsMovies"
        Me.gbRenamerPatternsMovies.Size = New System.Drawing.Size(293, 163)
        Me.gbRenamerPatternsMovies.TabIndex = 3
        Me.gbRenamerPatternsMovies.TabStop = False
        Me.gbRenamerPatternsMovies.Text = "Default Renaming Patterns"
        '
        'tblRenamerPatterns
        '
        Me.tblRenamerPatterns.AutoSize = True
        Me.tblRenamerPatterns.ColumnCount = 2
        Me.tblRenamerPatterns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatterns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatterns.Controls.Add(Me.chkRenameSingleMovies, 0, 5)
        Me.tblRenamerPatterns.Controls.Add(Me.lblFolderPatternMovies, 0, 0)
        Me.tblRenamerPatterns.Controls.Add(Me.chkRenameMultiMovies, 0, 4)
        Me.tblRenamerPatterns.Controls.Add(Me.txtFolderPatternMovies, 0, 1)
        Me.tblRenamerPatterns.Controls.Add(Me.txtFilePatternMovies, 0, 3)
        Me.tblRenamerPatterns.Controls.Add(Me.lblFilePatternMovies, 0, 2)
        Me.tblRenamerPatterns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRenamerPatterns.Location = New System.Drawing.Point(3, 18)
        Me.tblRenamerPatterns.Name = "tblRenamerPatterns"
        Me.tblRenamerPatterns.RowCount = 7
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatterns.Size = New System.Drawing.Size(287, 142)
        Me.tblRenamerPatterns.TabIndex = 7
        '
        'chkRenameSingleMovies
        '
        Me.chkRenameSingleMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameSingleMovies.AutoSize = True
        Me.chkRenameSingleMovies.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameSingleMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameSingleMovies.Location = New System.Drawing.Point(3, 122)
        Me.chkRenameSingleMovies.Name = "chkRenameSingleMovies"
        Me.chkRenameSingleMovies.Size = New System.Drawing.Size(281, 17)
        Me.chkRenameSingleMovies.TabIndex = 5
        Me.chkRenameSingleMovies.Text = "Automatically Rename Files During Single-Scraper"
        Me.chkRenameSingleMovies.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameSingleMovies.UseVisualStyleBackColor = True
        '
        'lblFolderPatternMovies
        '
        Me.lblFolderPatternMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFolderPatternMovies.AutoSize = True
        Me.lblFolderPatternMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolderPatternMovies.Location = New System.Drawing.Point(3, 3)
        Me.lblFolderPatternMovies.Name = "lblFolderPatternMovies"
        Me.lblFolderPatternMovies.Size = New System.Drawing.Size(85, 13)
        Me.lblFolderPatternMovies.TabIndex = 0
        Me.lblFolderPatternMovies.Text = "Folders Pattern"
        '
        'chkRenameMultiMovies
        '
        Me.chkRenameMultiMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameMultiMovies.AutoSize = True
        Me.chkRenameMultiMovies.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameMultiMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameMultiMovies.Location = New System.Drawing.Point(3, 99)
        Me.chkRenameMultiMovies.Name = "chkRenameMultiMovies"
        Me.chkRenameMultiMovies.Size = New System.Drawing.Size(276, 17)
        Me.chkRenameMultiMovies.TabIndex = 4
        Me.chkRenameMultiMovies.Text = "Automatically Rename Files During Multi-Scraper"
        Me.chkRenameMultiMovies.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameMultiMovies.UseVisualStyleBackColor = True
        '
        'txtFolderPatternMovies
        '
        Me.txtFolderPatternMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFolderPatternMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolderPatternMovies.Location = New System.Drawing.Point(3, 23)
        Me.txtFolderPatternMovies.Name = "txtFolderPatternMovies"
        Me.txtFolderPatternMovies.Size = New System.Drawing.Size(280, 22)
        Me.txtFolderPatternMovies.TabIndex = 1
        '
        'txtFilePatternMovies
        '
        Me.txtFilePatternMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFilePatternMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePatternMovies.Location = New System.Drawing.Point(3, 71)
        Me.txtFilePatternMovies.Name = "txtFilePatternMovies"
        Me.txtFilePatternMovies.Size = New System.Drawing.Size(280, 22)
        Me.txtFilePatternMovies.TabIndex = 3
        '
        'lblFilePatternMovies
        '
        Me.lblFilePatternMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilePatternMovies.AutoSize = True
        Me.lblFilePatternMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePatternMovies.Location = New System.Drawing.Point(3, 51)
        Me.lblFilePatternMovies.Name = "lblFilePatternMovies"
        Me.lblFilePatternMovies.Size = New System.Drawing.Size(70, 13)
        Me.lblFilePatternMovies.TabIndex = 2
        Me.lblFilePatternMovies.Text = "Files Pattern"
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(364, 23)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 2
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.chkEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(364, 23)
        Me.tblSettingsTop.TabIndex = 5
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 3)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'gbRenamerPatternsShows
        '
        Me.gbRenamerPatternsShows.AutoSize = True
        Me.gbRenamerPatternsShows.Controls.Add(Me.tblRenamerPatternsShows)
        Me.gbRenamerPatternsShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbRenamerPatternsShows.Location = New System.Drawing.Point(3, 218)
        Me.gbRenamerPatternsShows.Name = "gbRenamerPatternsShows"
        Me.gbRenamerPatternsShows.Size = New System.Drawing.Size(293, 211)
        Me.gbRenamerPatternsShows.TabIndex = 5
        Me.gbRenamerPatternsShows.TabStop = False
        Me.gbRenamerPatternsShows.Text = "Default Renaming Patterns"
        '
        'tblRenamerPatternsShows
        '
        Me.tblRenamerPatternsShows.AutoSize = True
        Me.tblRenamerPatternsShows.ColumnCount = 2
        Me.tblRenamerPatternsShows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatternsShows.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatternsShows.Controls.Add(Me.chkRenameSingleShows, 0, 7)
        Me.tblRenamerPatternsShows.Controls.Add(Me.lblFolderPatternShows, 0, 0)
        Me.tblRenamerPatternsShows.Controls.Add(Me.chkRenameMultiShows, 0, 6)
        Me.tblRenamerPatternsShows.Controls.Add(Me.txtFolderPatternShows, 0, 1)
        Me.tblRenamerPatternsShows.Controls.Add(Me.txtFilePatternEpisodes, 0, 5)
        Me.tblRenamerPatternsShows.Controls.Add(Me.lblFilePatternEpisodes, 0, 4)
        Me.tblRenamerPatternsShows.Controls.Add(Me.lblFolderPatternSeasons, 0, 2)
        Me.tblRenamerPatternsShows.Controls.Add(Me.txtFolderPatternSeasons, 0, 3)
        Me.tblRenamerPatternsShows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRenamerPatternsShows.Location = New System.Drawing.Point(3, 18)
        Me.tblRenamerPatternsShows.Name = "tblRenamerPatternsShows"
        Me.tblRenamerPatternsShows.RowCount = 9
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsShows.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsShows.Size = New System.Drawing.Size(287, 190)
        Me.tblRenamerPatternsShows.TabIndex = 7
        '
        'chkRenameSingleShows
        '
        Me.chkRenameSingleShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameSingleShows.AutoSize = True
        Me.chkRenameSingleShows.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameSingleShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameSingleShows.Location = New System.Drawing.Point(3, 170)
        Me.chkRenameSingleShows.Name = "chkRenameSingleShows"
        Me.chkRenameSingleShows.Size = New System.Drawing.Size(281, 17)
        Me.chkRenameSingleShows.TabIndex = 5
        Me.chkRenameSingleShows.Text = "Automatically Rename Files During Single-Scraper"
        Me.chkRenameSingleShows.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameSingleShows.UseVisualStyleBackColor = True
        '
        'lblFolderPatternShows
        '
        Me.lblFolderPatternShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFolderPatternShows.AutoSize = True
        Me.lblFolderPatternShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolderPatternShows.Location = New System.Drawing.Point(3, 3)
        Me.lblFolderPatternShows.Name = "lblFolderPatternShows"
        Me.lblFolderPatternShows.Size = New System.Drawing.Size(117, 13)
        Me.lblFolderPatternShows.TabIndex = 0
        Me.lblFolderPatternShows.Text = "Show Folders Pattern"
        '
        'chkRenameMultiShows
        '
        Me.chkRenameMultiShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameMultiShows.AutoSize = True
        Me.chkRenameMultiShows.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameMultiShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameMultiShows.Location = New System.Drawing.Point(3, 147)
        Me.chkRenameMultiShows.Name = "chkRenameMultiShows"
        Me.chkRenameMultiShows.Size = New System.Drawing.Size(276, 17)
        Me.chkRenameMultiShows.TabIndex = 4
        Me.chkRenameMultiShows.Text = "Automatically Rename Files During Multi-Scraper"
        Me.chkRenameMultiShows.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameMultiShows.UseVisualStyleBackColor = True
        '
        'txtFolderPatternShows
        '
        Me.txtFolderPatternShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFolderPatternShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolderPatternShows.Location = New System.Drawing.Point(3, 23)
        Me.txtFolderPatternShows.Name = "txtFolderPatternShows"
        Me.txtFolderPatternShows.Size = New System.Drawing.Size(280, 22)
        Me.txtFolderPatternShows.TabIndex = 1
        '
        'txtFilePatternEpisodes
        '
        Me.txtFilePatternEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFilePatternEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePatternEpisodes.Location = New System.Drawing.Point(3, 119)
        Me.txtFilePatternEpisodes.Name = "txtFilePatternEpisodes"
        Me.txtFilePatternEpisodes.Size = New System.Drawing.Size(280, 22)
        Me.txtFilePatternEpisodes.TabIndex = 3
        '
        'lblFilePatternEpisodes
        '
        Me.lblFilePatternEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilePatternEpisodes.AutoSize = True
        Me.lblFilePatternEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePatternEpisodes.Location = New System.Drawing.Point(3, 99)
        Me.lblFilePatternEpisodes.Name = "lblFilePatternEpisodes"
        Me.lblFilePatternEpisodes.Size = New System.Drawing.Size(114, 13)
        Me.lblFilePatternEpisodes.TabIndex = 2
        Me.lblFilePatternEpisodes.Text = "Epsiode Files Pattern"
        '
        'lblFolderPatternSeasons
        '
        Me.lblFolderPatternSeasons.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFolderPatternSeasons.AutoSize = True
        Me.lblFolderPatternSeasons.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolderPatternSeasons.Location = New System.Drawing.Point(3, 51)
        Me.lblFolderPatternSeasons.Name = "lblFolderPatternSeasons"
        Me.lblFolderPatternSeasons.Size = New System.Drawing.Size(125, 13)
        Me.lblFolderPatternSeasons.TabIndex = 0
        Me.lblFolderPatternSeasons.Text = "Season Folders Pattern"
        '
        'txtFolderPatternSeasons
        '
        Me.txtFolderPatternSeasons.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFolderPatternSeasons.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolderPatternSeasons.Location = New System.Drawing.Point(3, 71)
        Me.txtFolderPatternSeasons.Name = "txtFolderPatternSeasons"
        Me.txtFolderPatternSeasons.Size = New System.Drawing.Size(280, 22)
        Me.txtFolderPatternSeasons.TabIndex = 1
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(364, 500)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmSettingsHolder"
        Me.Text = "frmSettingsHolder"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.gbRenamerPatternsMovies.ResumeLayout(False)
        Me.gbRenamerPatternsMovies.PerformLayout()
        Me.tblRenamerPatterns.ResumeLayout(False)
        Me.tblRenamerPatterns.PerformLayout()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.gbRenamerPatternsShows.ResumeLayout(False)
        Me.gbRenamerPatternsShows.PerformLayout()
        Me.tblRenamerPatternsShows.ResumeLayout(False)
        Me.tblRenamerPatternsShows.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkBulkRenamer As System.Windows.Forms.CheckBox
    Friend WithEvents chkGenericModule As System.Windows.Forms.CheckBox
    Friend WithEvents gbRenamerPatternsMovies As System.Windows.Forms.GroupBox
    Friend WithEvents chkRenameSingleMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkRenameMultiMovies As System.Windows.Forms.CheckBox
    Friend WithEvents lblFilePatternMovies As System.Windows.Forms.Label
    Friend WithEvents lblFolderPatternMovies As System.Windows.Forms.Label
    Friend WithEvents txtFilePatternMovies As System.Windows.Forms.TextBox
    Friend WithEvents txtFolderPatternMovies As System.Windows.Forms.TextBox
    Friend WithEvents lblTips As System.Windows.Forms.Label
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblRenamerPatterns As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbRenamerPatternsShows As System.Windows.Forms.GroupBox
    Friend WithEvents tblRenamerPatternsShows As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkRenameSingleShows As System.Windows.Forms.CheckBox
    Friend WithEvents lblFolderPatternShows As System.Windows.Forms.Label
    Friend WithEvents chkRenameMultiShows As System.Windows.Forms.CheckBox
    Friend WithEvents txtFolderPatternShows As System.Windows.Forms.TextBox
    Friend WithEvents txtFilePatternEpisodes As System.Windows.Forms.TextBox
    Friend WithEvents lblFilePatternEpisodes As System.Windows.Forms.Label
    Friend WithEvents lblFolderPatternSeasons As System.Windows.Forms.Label
    Friend WithEvents txtFolderPatternSeasons As System.Windows.Forms.TextBox
End Class
