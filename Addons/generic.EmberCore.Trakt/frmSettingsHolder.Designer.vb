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
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGetWatchedState = New System.Windows.Forms.GroupBox()
        Me.tblGetWatchedState = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGetWatchedState = New System.Windows.Forms.CheckBox()
        Me.gbGetWatchedStateTVEpisodes = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGetWatchedStateBeforeEdit_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperMulti_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperSingle_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.gbGetWatchedStateMovies = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGetWatchedStateBeforeEdit_Movie = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperMulti_Movie = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperSingle_Movie = New System.Windows.Forms.CheckBox()
        Me.gbSettingsGeneral = New System.Windows.Forms.GroupBox()
        Me.tblSettingsGeneral = New System.Windows.Forms.TableLayoutPanel()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.chkGetShowProgress = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbGetWatchedState.SuspendLayout()
        Me.tblGetWatchedState.SuspendLayout()
        Me.gbGetWatchedStateTVEpisodes.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.gbGetWatchedStateMovies.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.gbSettingsGeneral.SuspendLayout()
        Me.tblSettingsGeneral.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(423, 23)
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(423, 23)
        Me.tblSettingsTop.TabIndex = 21
        '
        'chkEnabled
        '
        Me.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 3)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 2
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbGetWatchedState, 0, 3)
        Me.tblSettingsMain.Controls.Add(Me.gbSettingsGeneral, 0, 0)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 5
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsMain.Size = New System.Drawing.Size(423, 285)
        Me.tblSettingsMain.TabIndex = 21
        '
        'gbGetWatchedState
        '
        Me.gbGetWatchedState.AutoSize = True
        Me.gbGetWatchedState.Controls.Add(Me.tblGetWatchedState)
        Me.gbGetWatchedState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedState.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbGetWatchedState.Location = New System.Drawing.Point(3, 109)
        Me.gbGetWatchedState.Name = "gbGetWatchedState"
        Me.gbGetWatchedState.Size = New System.Drawing.Size(383, 140)
        Me.gbGetWatchedState.TabIndex = 55
        Me.gbGetWatchedState.TabStop = False
        Me.gbGetWatchedState.Text = "Watched State"
        '
        'tblGetWatchedState
        '
        Me.tblGetWatchedState.AutoSize = True
        Me.tblGetWatchedState.ColumnCount = 3
        Me.tblGetWatchedState.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGetWatchedState.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGetWatchedState.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGetWatchedState.Controls.Add(Me.chkGetWatchedState, 0, 0)
        Me.tblGetWatchedState.Controls.Add(Me.gbGetWatchedStateTVEpisodes, 1, 1)
        Me.tblGetWatchedState.Controls.Add(Me.gbGetWatchedStateMovies, 0, 1)
        Me.tblGetWatchedState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGetWatchedState.Location = New System.Drawing.Point(3, 18)
        Me.tblGetWatchedState.Name = "tblGetWatchedState"
        Me.tblGetWatchedState.RowCount = 3
        Me.tblGetWatchedState.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGetWatchedState.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGetWatchedState.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGetWatchedState.Size = New System.Drawing.Size(377, 119)
        Me.tblGetWatchedState.TabIndex = 7
        '
        'chkGetWatchedState
        '
        Me.chkGetWatchedState.AutoSize = True
        Me.tblGetWatchedState.SetColumnSpan(Me.chkGetWatchedState, 2)
        Me.chkGetWatchedState.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetWatchedState.Location = New System.Drawing.Point(3, 3)
        Me.chkGetWatchedState.Name = "chkGetWatchedState"
        Me.chkGetWatchedState.Size = New System.Drawing.Size(122, 17)
        Me.chkGetWatchedState.TabIndex = 87
        Me.chkGetWatchedState.Text = "Get Watched State"
        Me.chkGetWatchedState.UseVisualStyleBackColor = True
        '
        'gbGetWatchedStateTVEpisodes
        '
        Me.gbGetWatchedStateTVEpisodes.AutoSize = True
        Me.gbGetWatchedStateTVEpisodes.Controls.Add(Me.TableLayoutPanel3)
        Me.gbGetWatchedStateTVEpisodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedStateTVEpisodes.Enabled = False
        Me.gbGetWatchedStateTVEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGetWatchedStateTVEpisodes.Location = New System.Drawing.Point(167, 26)
        Me.gbGetWatchedStateTVEpisodes.Name = "gbGetWatchedStateTVEpisodes"
        Me.gbGetWatchedStateTVEpisodes.Size = New System.Drawing.Size(158, 90)
        Me.gbGetWatchedStateTVEpisodes.TabIndex = 90
        Me.gbGetWatchedStateTVEpisodes.TabStop = False
        Me.gbGetWatchedStateTVEpisodes.Text = "Episodes"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.AutoSize = True
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.Controls.Add(Me.chkGetWatchedStateBeforeEdit_TVEpisode, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.chkGetWatchedStateScraperMulti_TVEpisode, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.chkGetWatchedStateScraperSingle_TVEpisode, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(152, 69)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'chkGetWatchedStateBeforeEdit_TVEpisode
        '
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.AutoSize = True
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Location = New System.Drawing.Point(3, 3)
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Name = "chkGetWatchedStateBeforeEdit_TVEpisode"
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Size = New System.Drawing.Size(83, 17)
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.TabIndex = 87
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Text = "Before Edit"
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkGetWatchedStateScraperMulti_TVEpisode
        '
        Me.chkGetWatchedStateScraperMulti_TVEpisode.AutoSize = True
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Location = New System.Drawing.Point(3, 49)
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Name = "chkGetWatchedStateScraperMulti_TVEpisode"
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Size = New System.Drawing.Size(141, 17)
        Me.chkGetWatchedStateScraperMulti_TVEpisode.TabIndex = 87
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Text = "During Multi-Scraping"
        Me.chkGetWatchedStateScraperMulti_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkGetWatchedStateScraperSingle_TVEpisode
        '
        Me.chkGetWatchedStateScraperSingle_TVEpisode.AutoSize = True
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Location = New System.Drawing.Point(3, 26)
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Name = "chkGetWatchedStateScraperSingle_TVEpisode"
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Size = New System.Drawing.Size(146, 17)
        Me.chkGetWatchedStateScraperSingle_TVEpisode.TabIndex = 87
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Text = "During Single-Scraping"
        Me.chkGetWatchedStateScraperSingle_TVEpisode.UseVisualStyleBackColor = True
        '
        'gbGetWatchedStateMovies
        '
        Me.gbGetWatchedStateMovies.AutoSize = True
        Me.gbGetWatchedStateMovies.Controls.Add(Me.TableLayoutPanel2)
        Me.gbGetWatchedStateMovies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedStateMovies.Enabled = False
        Me.gbGetWatchedStateMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGetWatchedStateMovies.Location = New System.Drawing.Point(3, 26)
        Me.gbGetWatchedStateMovies.Name = "gbGetWatchedStateMovies"
        Me.gbGetWatchedStateMovies.Size = New System.Drawing.Size(158, 90)
        Me.gbGetWatchedStateMovies.TabIndex = 89
        Me.gbGetWatchedStateMovies.TabStop = False
        Me.gbGetWatchedStateMovies.Text = "Movies"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.chkGetWatchedStateBeforeEdit_Movie, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.chkGetWatchedStateScraperMulti_Movie, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.chkGetWatchedStateScraperSingle_Movie, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(152, 69)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'chkGetWatchedStateBeforeEdit_Movie
        '
        Me.chkGetWatchedStateBeforeEdit_Movie.AutoSize = True
        Me.chkGetWatchedStateBeforeEdit_Movie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetWatchedStateBeforeEdit_Movie.Location = New System.Drawing.Point(3, 3)
        Me.chkGetWatchedStateBeforeEdit_Movie.Name = "chkGetWatchedStateBeforeEdit_Movie"
        Me.chkGetWatchedStateBeforeEdit_Movie.Size = New System.Drawing.Size(83, 17)
        Me.chkGetWatchedStateBeforeEdit_Movie.TabIndex = 87
        Me.chkGetWatchedStateBeforeEdit_Movie.Text = "Before Edit"
        Me.chkGetWatchedStateBeforeEdit_Movie.UseVisualStyleBackColor = True
        '
        'chkGetWatchedStateScraperMulti_Movie
        '
        Me.chkGetWatchedStateScraperMulti_Movie.AutoSize = True
        Me.chkGetWatchedStateScraperMulti_Movie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetWatchedStateScraperMulti_Movie.Location = New System.Drawing.Point(3, 49)
        Me.chkGetWatchedStateScraperMulti_Movie.Name = "chkGetWatchedStateScraperMulti_Movie"
        Me.chkGetWatchedStateScraperMulti_Movie.Size = New System.Drawing.Size(141, 17)
        Me.chkGetWatchedStateScraperMulti_Movie.TabIndex = 87
        Me.chkGetWatchedStateScraperMulti_Movie.Text = "During Multi-Scraping"
        Me.chkGetWatchedStateScraperMulti_Movie.UseVisualStyleBackColor = True
        '
        'chkGetWatchedStateScraperSingle_Movie
        '
        Me.chkGetWatchedStateScraperSingle_Movie.AutoSize = True
        Me.chkGetWatchedStateScraperSingle_Movie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetWatchedStateScraperSingle_Movie.Location = New System.Drawing.Point(3, 26)
        Me.chkGetWatchedStateScraperSingle_Movie.Name = "chkGetWatchedStateScraperSingle_Movie"
        Me.chkGetWatchedStateScraperSingle_Movie.Size = New System.Drawing.Size(146, 17)
        Me.chkGetWatchedStateScraperSingle_Movie.TabIndex = 87
        Me.chkGetWatchedStateScraperSingle_Movie.Text = "During Single-Scraping"
        Me.chkGetWatchedStateScraperSingle_Movie.UseVisualStyleBackColor = True
        '
        'gbSettingsGeneral
        '
        Me.gbSettingsGeneral.AutoSize = True
        Me.gbSettingsGeneral.Controls.Add(Me.tblSettingsGeneral)
        Me.gbSettingsGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSettingsGeneral.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSettingsGeneral.Location = New System.Drawing.Point(3, 3)
        Me.gbSettingsGeneral.Name = "gbSettingsGeneral"
        Me.gbSettingsGeneral.Size = New System.Drawing.Size(383, 100)
        Me.gbSettingsGeneral.TabIndex = 20
        Me.gbSettingsGeneral.TabStop = False
        Me.gbSettingsGeneral.Text = "General Settings"
        '
        'tblSettingsGeneral
        '
        Me.tblSettingsGeneral.AutoSize = True
        Me.tblSettingsGeneral.ColumnCount = 3
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.Controls.Add(Me.txtPassword, 1, 1)
        Me.tblSettingsGeneral.Controls.Add(Me.lblPassword, 0, 1)
        Me.tblSettingsGeneral.Controls.Add(Me.txtUsername, 1, 0)
        Me.tblSettingsGeneral.Controls.Add(Me.lblUsername, 0, 0)
        Me.tblSettingsGeneral.Controls.Add(Me.chkGetShowProgress, 1, 2)
        Me.tblSettingsGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsGeneral.Location = New System.Drawing.Point(3, 18)
        Me.tblSettingsGeneral.Name = "tblSettingsGeneral"
        Me.tblSettingsGeneral.RowCount = 3
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsGeneral.Size = New System.Drawing.Size(377, 79)
        Me.tblSettingsGeneral.TabIndex = 21
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(67, 31)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(200, 22)
        Me.txtPassword.TabIndex = 41
        '
        'lblPassword
        '
        Me.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(3, 35)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 42
        Me.lblPassword.Text = "Password"
        '
        'txtUsername
        '
        Me.txtUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.Location = New System.Drawing.Point(67, 3)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(200, 22)
        Me.txtUsername.TabIndex = 39
        '
        'lblUsername
        '
        Me.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsername.Location = New System.Drawing.Point(3, 7)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 40
        Me.lblUsername.Text = "Username"
        '
        'chkGetShowProgress
        '
        Me.chkGetShowProgress.AutoSize = True
        Me.chkGetShowProgress.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetShowProgress.Location = New System.Drawing.Point(67, 59)
        Me.chkGetShowProgress.Name = "chkGetShowProgress"
        Me.chkGetShowProgress.Size = New System.Drawing.Size(307, 17)
        Me.chkGetShowProgress.TabIndex = 44
        Me.chkGetShowProgress.Text = "Display watched progress for shows (Time consuming!)"
        Me.chkGetShowProgress.UseVisualStyleBackColor = True
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(423, 285)
        Me.pnlSettingsMain.TabIndex = 1
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(423, 308)
        Me.pnlSettings.TabIndex = 1
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(423, 308)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmSettingsHolder"
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.gbGetWatchedState.ResumeLayout(False)
        Me.gbGetWatchedState.PerformLayout()
        Me.tblGetWatchedState.ResumeLayout(False)
        Me.tblGetWatchedState.PerformLayout()
        Me.gbGetWatchedStateTVEpisodes.ResumeLayout(False)
        Me.gbGetWatchedStateTVEpisodes.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.gbGetWatchedStateMovies.ResumeLayout(False)
        Me.gbGetWatchedStateMovies.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.gbSettingsGeneral.ResumeLayout(False)
        Me.gbSettingsGeneral.PerformLayout()
        Me.tblSettingsGeneral.ResumeLayout(False)
        Me.tblSettingsGeneral.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents gbSettingsGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents tblSettingsGeneral As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents chkGetShowProgress As System.Windows.Forms.CheckBox
    Friend WithEvents gbGetWatchedState As System.Windows.Forms.GroupBox
    Friend WithEvents tblGetWatchedState As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbGetWatchedStateMovies As Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As Windows.Forms.TableLayoutPanel
    Friend WithEvents chkGetWatchedStateBeforeEdit_Movie As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedStateScraperMulti_Movie As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedStateScraperSingle_Movie As Windows.Forms.CheckBox
    Friend WithEvents gbGetWatchedStateTVEpisodes As Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel3 As Windows.Forms.TableLayoutPanel
    Friend WithEvents chkGetWatchedStateBeforeEdit_TVEpisode As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedStateScraperMulti_TVEpisode As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedStateScraperSingle_TVEpisode As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedState As Windows.Forms.CheckBox
End Class
