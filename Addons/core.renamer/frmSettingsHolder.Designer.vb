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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsHolder))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbRenamerPatternsTV = New System.Windows.Forms.GroupBox()
        Me.tblRenamerPatternsTV = New System.Windows.Forms.TableLayoutPanel()
        Me.btnFolderPatternSeasonsReset = New System.Windows.Forms.Button()
        Me.btnFolderPatternShowsReset = New System.Windows.Forms.Button()
        Me.chkRenameSingleShows = New System.Windows.Forms.CheckBox()
        Me.lblFolderPatternShows = New System.Windows.Forms.Label()
        Me.chkRenameMultiShows = New System.Windows.Forms.CheckBox()
        Me.txtFolderPatternShows = New System.Windows.Forms.TextBox()
        Me.txtFilePatternEpisodes = New System.Windows.Forms.TextBox()
        Me.lblFilePatternEpisodes = New System.Windows.Forms.Label()
        Me.lblFolderPatternSeasons = New System.Windows.Forms.Label()
        Me.txtFolderPatternSeasons = New System.Windows.Forms.TextBox()
        Me.chkRenameEditEpisodes = New System.Windows.Forms.CheckBox()
        Me.chkRenameUpdateEpisodes = New System.Windows.Forms.CheckBox()
        Me.btnFilePatternEpisodesReset = New System.Windows.Forms.Button()
        Me.gbRenamerPatternsMovie = New System.Windows.Forms.GroupBox()
        Me.tblRenamerPatternsMovie = New System.Windows.Forms.TableLayoutPanel()
        Me.chkRenameSingleMovies = New System.Windows.Forms.CheckBox()
        Me.btnFolderPatternMoviesReset = New System.Windows.Forms.Button()
        Me.lblFolderPatternMovies = New System.Windows.Forms.Label()
        Me.chkRenameMultiMovies = New System.Windows.Forms.CheckBox()
        Me.txtFolderPatternMovies = New System.Windows.Forms.TextBox()
        Me.txtFilePatternMovies = New System.Windows.Forms.TextBox()
        Me.lblFilePatternMovies = New System.Windows.Forms.Label()
        Me.chkRenameEditMovies = New System.Windows.Forms.CheckBox()
        Me.btnFilePatternMoviesReset = New System.Windows.Forms.Button()
        Me.gbPreview = New System.Windows.Forms.GroupBox()
        Me.tblPreview = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSingleEpisodeFile = New System.Windows.Forms.Label()
        Me.txtSingleEpisodeFile = New System.Windows.Forms.TextBox()
        Me.lblMultiEpisodeFile = New System.Windows.Forms.Label()
        Me.txtMultiEpisodeFile = New System.Windows.Forms.TextBox()
        Me.lblSingleMovieFile = New System.Windows.Forms.Label()
        Me.txtSingleMovieFile = New System.Windows.Forms.TextBox()
        Me.lblMultiSeasonFile = New System.Windows.Forms.Label()
        Me.txtMultiSeasonFile = New System.Windows.Forms.TextBox()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.tblTips = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTips = New System.Windows.Forms.Label()
        Me.pnlTips = New System.Windows.Forms.Panel()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbRenamerPatternsTV.SuspendLayout()
        Me.tblRenamerPatternsTV.SuspendLayout()
        Me.gbRenamerPatternsMovie.SuspendLayout()
        Me.tblRenamerPatternsMovie.SuspendLayout()
        Me.gbPreview.SuspendLayout()
        Me.tblPreview.SuspendLayout()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.tblTips.SuspendLayout()
        Me.pnlTips.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(778, 580)
        Me.pnlSettings.TabIndex = 84
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(778, 557)
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
        Me.tblSettingsMain.Controls.Add(Me.gbRenamerPatternsTV, 0, 1)
        Me.tblSettingsMain.Controls.Add(Me.gbRenamerPatternsMovie, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.gbPreview, 1, 2)
        Me.tblSettingsMain.Controls.Add(Me.pnlTips, 1, 0)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 4
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(778, 557)
        Me.tblSettingsMain.TabIndex = 6
        '
        'gbRenamerPatternsTV
        '
        Me.gbRenamerPatternsTV.AutoSize = True
        Me.gbRenamerPatternsTV.Controls.Add(Me.tblRenamerPatternsTV)
        Me.gbRenamerPatternsTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbRenamerPatternsTV.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbRenamerPatternsTV.Location = New System.Drawing.Point(3, 197)
        Me.gbRenamerPatternsTV.Name = "gbRenamerPatternsTV"
        Me.tblSettingsMain.SetRowSpan(Me.gbRenamerPatternsTV, 2)
        Me.gbRenamerPatternsTV.Size = New System.Drawing.Size(322, 260)
        Me.gbRenamerPatternsTV.TabIndex = 5
        Me.gbRenamerPatternsTV.TabStop = False
        Me.gbRenamerPatternsTV.Text = "Default TV Renaming Patterns"
        '
        'tblRenamerPatternsTV
        '
        Me.tblRenamerPatternsTV.AutoSize = True
        Me.tblRenamerPatternsTV.ColumnCount = 3
        Me.tblRenamerPatternsTV.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatternsTV.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatternsTV.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatternsTV.Controls.Add(Me.btnFolderPatternSeasonsReset, 1, 3)
        Me.tblRenamerPatternsTV.Controls.Add(Me.btnFolderPatternShowsReset, 1, 1)
        Me.tblRenamerPatternsTV.Controls.Add(Me.chkRenameSingleShows, 0, 7)
        Me.tblRenamerPatternsTV.Controls.Add(Me.lblFolderPatternShows, 0, 0)
        Me.tblRenamerPatternsTV.Controls.Add(Me.chkRenameMultiShows, 0, 6)
        Me.tblRenamerPatternsTV.Controls.Add(Me.txtFolderPatternShows, 0, 1)
        Me.tblRenamerPatternsTV.Controls.Add(Me.txtFilePatternEpisodes, 0, 5)
        Me.tblRenamerPatternsTV.Controls.Add(Me.lblFilePatternEpisodes, 0, 4)
        Me.tblRenamerPatternsTV.Controls.Add(Me.lblFolderPatternSeasons, 0, 2)
        Me.tblRenamerPatternsTV.Controls.Add(Me.txtFolderPatternSeasons, 0, 3)
        Me.tblRenamerPatternsTV.Controls.Add(Me.chkRenameEditEpisodes, 0, 9)
        Me.tblRenamerPatternsTV.Controls.Add(Me.chkRenameUpdateEpisodes, 0, 8)
        Me.tblRenamerPatternsTV.Controls.Add(Me.btnFilePatternEpisodesReset, 1, 5)
        Me.tblRenamerPatternsTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRenamerPatternsTV.Location = New System.Drawing.Point(3, 18)
        Me.tblRenamerPatternsTV.Name = "tblRenamerPatternsTV"
        Me.tblRenamerPatternsTV.RowCount = 11
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsTV.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsTV.Size = New System.Drawing.Size(316, 239)
        Me.tblRenamerPatternsTV.TabIndex = 7
        '
        'btnFolderPatternSeasonsReset
        '
        Me.btnFolderPatternSeasonsReset.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFolderPatternSeasonsReset.Image = CType(resources.GetObject("btnFolderPatternSeasonsReset.Image"), System.Drawing.Image)
        Me.btnFolderPatternSeasonsReset.Location = New System.Drawing.Point(289, 72)
        Me.btnFolderPatternSeasonsReset.Name = "btnFolderPatternSeasonsReset"
        Me.btnFolderPatternSeasonsReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFolderPatternSeasonsReset.TabIndex = 7
        Me.btnFolderPatternSeasonsReset.UseVisualStyleBackColor = True
        '
        'btnFolderPatternShowsReset
        '
        Me.btnFolderPatternShowsReset.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFolderPatternShowsReset.Image = CType(resources.GetObject("btnFolderPatternShowsReset.Image"), System.Drawing.Image)
        Me.btnFolderPatternShowsReset.Location = New System.Drawing.Point(289, 23)
        Me.btnFolderPatternShowsReset.Name = "btnFolderPatternShowsReset"
        Me.btnFolderPatternShowsReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFolderPatternShowsReset.TabIndex = 6
        Me.btnFolderPatternShowsReset.UseVisualStyleBackColor = True
        '
        'chkRenameSingleShows
        '
        Me.chkRenameSingleShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameSingleShows.AutoSize = True
        Me.chkRenameSingleShows.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblRenamerPatternsTV.SetColumnSpan(Me.chkRenameSingleShows, 2)
        Me.chkRenameSingleShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameSingleShows.Location = New System.Drawing.Point(3, 173)
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
        Me.tblRenamerPatternsTV.SetColumnSpan(Me.chkRenameMultiShows, 2)
        Me.chkRenameMultiShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameMultiShows.Location = New System.Drawing.Point(3, 150)
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
        Me.txtFilePatternEpisodes.Location = New System.Drawing.Point(3, 121)
        Me.txtFilePatternEpisodes.Name = "txtFilePatternEpisodes"
        Me.txtFilePatternEpisodes.Size = New System.Drawing.Size(280, 22)
        Me.txtFilePatternEpisodes.TabIndex = 3
        '
        'lblFilePatternEpisodes
        '
        Me.lblFilePatternEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilePatternEpisodes.AutoSize = True
        Me.lblFilePatternEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePatternEpisodes.Location = New System.Drawing.Point(3, 101)
        Me.lblFilePatternEpisodes.Name = "lblFilePatternEpisodes"
        Me.lblFilePatternEpisodes.Size = New System.Drawing.Size(114, 13)
        Me.lblFilePatternEpisodes.TabIndex = 2
        Me.lblFilePatternEpisodes.Text = "Episode Files Pattern"
        '
        'lblFolderPatternSeasons
        '
        Me.lblFolderPatternSeasons.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFolderPatternSeasons.AutoSize = True
        Me.lblFolderPatternSeasons.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolderPatternSeasons.Location = New System.Drawing.Point(3, 52)
        Me.lblFolderPatternSeasons.Name = "lblFolderPatternSeasons"
        Me.lblFolderPatternSeasons.Size = New System.Drawing.Size(125, 13)
        Me.lblFolderPatternSeasons.TabIndex = 0
        Me.lblFolderPatternSeasons.Text = "Season Folders Pattern"
        '
        'txtFolderPatternSeasons
        '
        Me.txtFolderPatternSeasons.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFolderPatternSeasons.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolderPatternSeasons.Location = New System.Drawing.Point(3, 72)
        Me.txtFolderPatternSeasons.Name = "txtFolderPatternSeasons"
        Me.txtFolderPatternSeasons.Size = New System.Drawing.Size(280, 22)
        Me.txtFolderPatternSeasons.TabIndex = 1
        '
        'chkRenameEditEpisodes
        '
        Me.chkRenameEditEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameEditEpisodes.AutoSize = True
        Me.chkRenameEditEpisodes.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblRenamerPatternsTV.SetColumnSpan(Me.chkRenameEditEpisodes, 2)
        Me.chkRenameEditEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameEditEpisodes.Location = New System.Drawing.Point(3, 219)
        Me.chkRenameEditEpisodes.Name = "chkRenameEditEpisodes"
        Me.chkRenameEditEpisodes.Size = New System.Drawing.Size(260, 17)
        Me.chkRenameEditEpisodes.TabIndex = 5
        Me.chkRenameEditEpisodes.Text = "Automatically Rename Files After Edit Episode"
        Me.chkRenameEditEpisodes.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameEditEpisodes.UseVisualStyleBackColor = True
        '
        'chkRenameUpdateEpisodes
        '
        Me.chkRenameUpdateEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameUpdateEpisodes.AutoSize = True
        Me.chkRenameUpdateEpisodes.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblRenamerPatternsTV.SetColumnSpan(Me.chkRenameUpdateEpisodes, 2)
        Me.chkRenameUpdateEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameUpdateEpisodes.Location = New System.Drawing.Point(3, 196)
        Me.chkRenameUpdateEpisodes.Name = "chkRenameUpdateEpisodes"
        Me.chkRenameUpdateEpisodes.Size = New System.Drawing.Size(252, 17)
        Me.chkRenameUpdateEpisodes.TabIndex = 5
        Me.chkRenameUpdateEpisodes.Text = "Automatically Rename Files During DB Update"
        Me.chkRenameUpdateEpisodes.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameUpdateEpisodes.UseVisualStyleBackColor = True
        '
        'btnFilePatternEpisodesReset
        '
        Me.btnFilePatternEpisodesReset.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFilePatternEpisodesReset.Image = CType(resources.GetObject("btnFilePatternEpisodesReset.Image"), System.Drawing.Image)
        Me.btnFilePatternEpisodesReset.Location = New System.Drawing.Point(289, 121)
        Me.btnFilePatternEpisodesReset.Name = "btnFilePatternEpisodesReset"
        Me.btnFilePatternEpisodesReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFilePatternEpisodesReset.TabIndex = 7
        Me.btnFilePatternEpisodesReset.UseVisualStyleBackColor = True
        '
        'gbRenamerPatternsMovie
        '
        Me.gbRenamerPatternsMovie.AutoSize = True
        Me.gbRenamerPatternsMovie.Controls.Add(Me.tblRenamerPatternsMovie)
        Me.gbRenamerPatternsMovie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbRenamerPatternsMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbRenamerPatternsMovie.Location = New System.Drawing.Point(3, 3)
        Me.gbRenamerPatternsMovie.Name = "gbRenamerPatternsMovie"
        Me.gbRenamerPatternsMovie.Size = New System.Drawing.Size(322, 188)
        Me.gbRenamerPatternsMovie.TabIndex = 3
        Me.gbRenamerPatternsMovie.TabStop = False
        Me.gbRenamerPatternsMovie.Text = "Default Movie Renaming Patterns"
        '
        'tblRenamerPatternsMovie
        '
        Me.tblRenamerPatternsMovie.AutoSize = True
        Me.tblRenamerPatternsMovie.ColumnCount = 3
        Me.tblRenamerPatternsMovie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatternsMovie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatternsMovie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRenamerPatternsMovie.Controls.Add(Me.chkRenameSingleMovies, 0, 5)
        Me.tblRenamerPatternsMovie.Controls.Add(Me.btnFolderPatternMoviesReset, 1, 1)
        Me.tblRenamerPatternsMovie.Controls.Add(Me.lblFolderPatternMovies, 0, 0)
        Me.tblRenamerPatternsMovie.Controls.Add(Me.chkRenameMultiMovies, 0, 4)
        Me.tblRenamerPatternsMovie.Controls.Add(Me.txtFolderPatternMovies, 0, 1)
        Me.tblRenamerPatternsMovie.Controls.Add(Me.txtFilePatternMovies, 0, 3)
        Me.tblRenamerPatternsMovie.Controls.Add(Me.lblFilePatternMovies, 0, 2)
        Me.tblRenamerPatternsMovie.Controls.Add(Me.chkRenameEditMovies, 0, 6)
        Me.tblRenamerPatternsMovie.Controls.Add(Me.btnFilePatternMoviesReset, 1, 3)
        Me.tblRenamerPatternsMovie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRenamerPatternsMovie.Location = New System.Drawing.Point(3, 18)
        Me.tblRenamerPatternsMovie.Name = "tblRenamerPatternsMovie"
        Me.tblRenamerPatternsMovie.RowCount = 8
        Me.tblRenamerPatternsMovie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatternsMovie.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsMovie.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblRenamerPatternsMovie.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsMovie.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsMovie.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsMovie.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsMovie.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRenamerPatternsMovie.Size = New System.Drawing.Size(316, 167)
        Me.tblRenamerPatternsMovie.TabIndex = 7
        '
        'chkRenameSingleMovies
        '
        Me.chkRenameSingleMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameSingleMovies.AutoSize = True
        Me.chkRenameSingleMovies.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameSingleMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameSingleMovies.Location = New System.Drawing.Point(3, 124)
        Me.chkRenameSingleMovies.Name = "chkRenameSingleMovies"
        Me.chkRenameSingleMovies.Size = New System.Drawing.Size(281, 17)
        Me.chkRenameSingleMovies.TabIndex = 5
        Me.chkRenameSingleMovies.Text = "Automatically Rename Files During Single-Scraper"
        Me.chkRenameSingleMovies.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameSingleMovies.UseVisualStyleBackColor = True
        '
        'btnFolderPatternMoviesReset
        '
        Me.btnFolderPatternMoviesReset.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFolderPatternMoviesReset.Image = CType(resources.GetObject("btnFolderPatternMoviesReset.Image"), System.Drawing.Image)
        Me.btnFolderPatternMoviesReset.Location = New System.Drawing.Point(290, 23)
        Me.btnFolderPatternMoviesReset.Name = "btnFolderPatternMoviesReset"
        Me.btnFolderPatternMoviesReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFolderPatternMoviesReset.TabIndex = 6
        Me.btnFolderPatternMoviesReset.UseVisualStyleBackColor = True
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
        Me.chkRenameMultiMovies.Location = New System.Drawing.Point(3, 101)
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
        Me.txtFilePatternMovies.Location = New System.Drawing.Point(3, 72)
        Me.txtFilePatternMovies.Name = "txtFilePatternMovies"
        Me.txtFilePatternMovies.Size = New System.Drawing.Size(280, 22)
        Me.txtFilePatternMovies.TabIndex = 3
        '
        'lblFilePatternMovies
        '
        Me.lblFilePatternMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilePatternMovies.AutoSize = True
        Me.lblFilePatternMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePatternMovies.Location = New System.Drawing.Point(3, 52)
        Me.lblFilePatternMovies.Name = "lblFilePatternMovies"
        Me.lblFilePatternMovies.Size = New System.Drawing.Size(70, 13)
        Me.lblFilePatternMovies.TabIndex = 2
        Me.lblFilePatternMovies.Text = "Files Pattern"
        '
        'chkRenameEditMovies
        '
        Me.chkRenameEditMovies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRenameEditMovies.AutoSize = True
        Me.chkRenameEditMovies.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameEditMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRenameEditMovies.Location = New System.Drawing.Point(3, 147)
        Me.chkRenameEditMovies.Name = "chkRenameEditMovies"
        Me.chkRenameEditMovies.Size = New System.Drawing.Size(216, 17)
        Me.chkRenameEditMovies.TabIndex = 5
        Me.chkRenameEditMovies.Text = "Automatically Rename Files After Edit"
        Me.chkRenameEditMovies.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkRenameEditMovies.UseVisualStyleBackColor = True
        '
        'btnFilePatternMoviesReset
        '
        Me.btnFilePatternMoviesReset.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFilePatternMoviesReset.Image = CType(resources.GetObject("btnFilePatternMoviesReset.Image"), System.Drawing.Image)
        Me.btnFilePatternMoviesReset.Location = New System.Drawing.Point(290, 72)
        Me.btnFilePatternMoviesReset.Name = "btnFilePatternMoviesReset"
        Me.btnFilePatternMoviesReset.Size = New System.Drawing.Size(23, 23)
        Me.btnFilePatternMoviesReset.TabIndex = 6
        Me.btnFilePatternMoviesReset.UseVisualStyleBackColor = True
        '
        'gbPreview
        '
        Me.gbPreview.AutoSize = True
        Me.gbPreview.Controls.Add(Me.tblPreview)
        Me.gbPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbPreview.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbPreview.Location = New System.Drawing.Point(331, 244)
        Me.gbPreview.Name = "gbPreview"
        Me.gbPreview.Size = New System.Drawing.Size(412, 213)
        Me.gbPreview.TabIndex = 6
        Me.gbPreview.TabStop = False
        Me.gbPreview.Text = "Preview"
        '
        'tblPreview
        '
        Me.tblPreview.AutoSize = True
        Me.tblPreview.ColumnCount = 1
        Me.tblPreview.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblPreview.Controls.Add(Me.lblSingleEpisodeFile, 0, 2)
        Me.tblPreview.Controls.Add(Me.txtSingleEpisodeFile, 0, 3)
        Me.tblPreview.Controls.Add(Me.lblMultiEpisodeFile, 0, 4)
        Me.tblPreview.Controls.Add(Me.txtMultiEpisodeFile, 0, 5)
        Me.tblPreview.Controls.Add(Me.lblSingleMovieFile, 0, 0)
        Me.tblPreview.Controls.Add(Me.txtSingleMovieFile, 0, 1)
        Me.tblPreview.Controls.Add(Me.lblMultiSeasonFile, 0, 6)
        Me.tblPreview.Controls.Add(Me.txtMultiSeasonFile, 0, 7)
        Me.tblPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblPreview.Location = New System.Drawing.Point(3, 18)
        Me.tblPreview.Name = "tblPreview"
        Me.tblPreview.RowCount = 9
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPreview.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblPreview.Size = New System.Drawing.Size(406, 192)
        Me.tblPreview.TabIndex = 0
        '
        'lblSingleEpisodeFile
        '
        Me.lblSingleEpisodeFile.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSingleEpisodeFile.AutoSize = True
        Me.lblSingleEpisodeFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSingleEpisodeFile.Location = New System.Drawing.Point(3, 51)
        Me.lblSingleEpisodeFile.Name = "lblSingleEpisodeFile"
        Me.lblSingleEpisodeFile.Size = New System.Drawing.Size(107, 13)
        Me.lblSingleEpisodeFile.TabIndex = 0
        Me.lblSingleEpisodeFile.Text = "Single Episode File:"
        '
        'txtSingleEpisodeFile
        '
        Me.txtSingleEpisodeFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSingleEpisodeFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSingleEpisodeFile.Location = New System.Drawing.Point(3, 71)
        Me.txtSingleEpisodeFile.Name = "txtSingleEpisodeFile"
        Me.txtSingleEpisodeFile.Size = New System.Drawing.Size(400, 22)
        Me.txtSingleEpisodeFile.TabIndex = 1
        '
        'lblMultiEpisodeFile
        '
        Me.lblMultiEpisodeFile.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMultiEpisodeFile.AutoSize = True
        Me.lblMultiEpisodeFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMultiEpisodeFile.Location = New System.Drawing.Point(3, 99)
        Me.lblMultiEpisodeFile.Name = "lblMultiEpisodeFile"
        Me.lblMultiEpisodeFile.Size = New System.Drawing.Size(102, 13)
        Me.lblMultiEpisodeFile.TabIndex = 2
        Me.lblMultiEpisodeFile.Text = "Multi Episode File:"
        '
        'txtMultiEpisodeFile
        '
        Me.txtMultiEpisodeFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMultiEpisodeFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiEpisodeFile.Location = New System.Drawing.Point(3, 119)
        Me.txtMultiEpisodeFile.Name = "txtMultiEpisodeFile"
        Me.txtMultiEpisodeFile.Size = New System.Drawing.Size(400, 22)
        Me.txtMultiEpisodeFile.TabIndex = 3
        '
        'lblSingleMovieFile
        '
        Me.lblSingleMovieFile.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSingleMovieFile.AutoSize = True
        Me.lblSingleMovieFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSingleMovieFile.Location = New System.Drawing.Point(3, 3)
        Me.lblSingleMovieFile.Name = "lblSingleMovieFile"
        Me.lblSingleMovieFile.Size = New System.Drawing.Size(97, 13)
        Me.lblSingleMovieFile.TabIndex = 0
        Me.lblSingleMovieFile.Text = "Single Movie File:"
        '
        'txtSingleMovieFile
        '
        Me.txtSingleMovieFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSingleMovieFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSingleMovieFile.Location = New System.Drawing.Point(3, 23)
        Me.txtSingleMovieFile.Name = "txtSingleMovieFile"
        Me.txtSingleMovieFile.Size = New System.Drawing.Size(400, 22)
        Me.txtSingleMovieFile.TabIndex = 4
        '
        'lblMultiSeasonFile
        '
        Me.lblMultiSeasonFile.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMultiSeasonFile.AutoSize = True
        Me.lblMultiSeasonFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMultiSeasonFile.Location = New System.Drawing.Point(3, 147)
        Me.lblMultiSeasonFile.Name = "lblMultiSeasonFile"
        Me.lblMultiSeasonFile.Size = New System.Drawing.Size(98, 13)
        Me.lblMultiSeasonFile.TabIndex = 2
        Me.lblMultiSeasonFile.Text = "Multi Season File:"
        '
        'txtMultiSeasonFile
        '
        Me.txtMultiSeasonFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMultiSeasonFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiSeasonFile.Location = New System.Drawing.Point(3, 167)
        Me.txtMultiSeasonFile.Name = "txtMultiSeasonFile"
        Me.txtMultiSeasonFile.Size = New System.Drawing.Size(400, 22)
        Me.txtMultiSeasonFile.TabIndex = 3
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(778, 23)
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(778, 23)
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
        'tblTips
        '
        Me.tblTips.AutoScroll = True
        Me.tblTips.AutoSize = True
        Me.tblTips.ColumnCount = 1
        Me.tblTips.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTips.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTips.Controls.Add(Me.lblTips, 0, 0)
        Me.tblTips.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTips.Location = New System.Drawing.Point(0, 0)
        Me.tblTips.Name = "tblTips"
        Me.tblTips.RowCount = 1
        Me.tblTips.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTips.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 279.0!))
        Me.tblTips.Size = New System.Drawing.Size(410, 233)
        Me.tblTips.TabIndex = 5
        '
        'lblTips
        '
        Me.lblTips.AutoSize = True
        Me.lblTips.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTips.Location = New System.Drawing.Point(3, 0)
        Me.lblTips.Name = "lblTips"
        Me.lblTips.Size = New System.Drawing.Size(404, 233)
        Me.lblTips.TabIndex = 4
        Me.lblTips.Text = "Tips List"
        '
        'pnlTips
        '
        Me.pnlTips.AutoSize = True
        Me.pnlTips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTips.Controls.Add(Me.tblTips)
        Me.pnlTips.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTips.Location = New System.Drawing.Point(331, 3)
        Me.pnlTips.MaximumSize = New System.Drawing.Size(475, 360)
        Me.pnlTips.Name = "pnlTips"
        Me.tblSettingsMain.SetRowSpan(Me.pnlTips, 2)
        Me.pnlTips.Size = New System.Drawing.Size(412, 235)
        Me.pnlTips.TabIndex = 7
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(778, 580)
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
        Me.gbRenamerPatternsTV.ResumeLayout(False)
        Me.gbRenamerPatternsTV.PerformLayout()
        Me.tblRenamerPatternsTV.ResumeLayout(False)
        Me.tblRenamerPatternsTV.PerformLayout()
        Me.gbRenamerPatternsMovie.ResumeLayout(False)
        Me.gbRenamerPatternsMovie.PerformLayout()
        Me.tblRenamerPatternsMovie.ResumeLayout(False)
        Me.tblRenamerPatternsMovie.PerformLayout()
        Me.gbPreview.ResumeLayout(False)
        Me.gbPreview.PerformLayout()
        Me.tblPreview.ResumeLayout(False)
        Me.tblPreview.PerformLayout()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.tblTips.ResumeLayout(False)
        Me.tblTips.PerformLayout()
        Me.pnlTips.ResumeLayout(False)
        Me.pnlTips.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents gbRenamerPatternsMovie As System.Windows.Forms.GroupBox
    Friend WithEvents chkRenameSingleMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkRenameMultiMovies As System.Windows.Forms.CheckBox
    Friend WithEvents lblFilePatternMovies As System.Windows.Forms.Label
    Friend WithEvents lblFolderPatternMovies As System.Windows.Forms.Label
    Friend WithEvents txtFilePatternMovies As System.Windows.Forms.TextBox
    Friend WithEvents txtFolderPatternMovies As System.Windows.Forms.TextBox
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblRenamerPatternsMovie As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbRenamerPatternsTV As System.Windows.Forms.GroupBox
    Friend WithEvents tblRenamerPatternsTV As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkRenameSingleShows As System.Windows.Forms.CheckBox
    Friend WithEvents lblFolderPatternShows As System.Windows.Forms.Label
    Friend WithEvents chkRenameMultiShows As System.Windows.Forms.CheckBox
    Friend WithEvents txtFolderPatternShows As System.Windows.Forms.TextBox
    Friend WithEvents txtFilePatternEpisodes As System.Windows.Forms.TextBox
    Friend WithEvents lblFilePatternEpisodes As System.Windows.Forms.Label
    Friend WithEvents lblFolderPatternSeasons As System.Windows.Forms.Label
    Friend WithEvents txtFolderPatternSeasons As System.Windows.Forms.TextBox
    Friend WithEvents chkRenameEditMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkRenameEditEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkRenameUpdateEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents gbPreview As System.Windows.Forms.GroupBox
    Friend WithEvents tblPreview As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblSingleEpisodeFile As System.Windows.Forms.Label
    Friend WithEvents txtSingleEpisodeFile As System.Windows.Forms.TextBox
    Friend WithEvents lblMultiEpisodeFile As System.Windows.Forms.Label
    Friend WithEvents txtMultiEpisodeFile As System.Windows.Forms.TextBox
    Friend WithEvents lblSingleMovieFile As System.Windows.Forms.Label
    Friend WithEvents txtSingleMovieFile As System.Windows.Forms.TextBox
    Friend WithEvents lblMultiSeasonFile As System.Windows.Forms.Label
    Friend WithEvents txtMultiSeasonFile As System.Windows.Forms.TextBox
    Friend WithEvents btnFolderPatternShowsReset As System.Windows.Forms.Button
    Friend WithEvents btnFolderPatternSeasonsReset As System.Windows.Forms.Button
    Friend WithEvents btnFilePatternEpisodesReset As System.Windows.Forms.Button
    Friend WithEvents btnFolderPatternMoviesReset As System.Windows.Forms.Button
    Friend WithEvents btnFilePatternMoviesReset As System.Windows.Forms.Button
    Friend WithEvents pnlTips As System.Windows.Forms.Panel
    Friend WithEvents tblTips As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblTips As System.Windows.Forms.Label
End Class
