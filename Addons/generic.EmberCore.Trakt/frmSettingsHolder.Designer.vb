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
        Me.gbSettingsLastPlayed = New System.Windows.Forms.GroupBox()
        Me.tblSettingsLastPlayed = New System.Windows.Forms.TableLayoutPanel()
        Me.chkSyncLastPlayedSingleMovies = New System.Windows.Forms.CheckBox()
        Me.chkSyncLastPlayedMultiMovies = New System.Windows.Forms.CheckBox()
        Me.chkSyncLastPlayedEditMovies = New System.Windows.Forms.CheckBox()
        Me.chkSyncLastPlayedMultiEpisodes = New System.Windows.Forms.CheckBox()
        Me.chkSyncLastPlayedSingleEpisodes = New System.Windows.Forms.CheckBox()
        Me.chkSyncLastPlayedEditEpisodes = New System.Windows.Forms.CheckBox()
        Me.gbSettingsPlaycount = New System.Windows.Forms.GroupBox()
        Me.tblSettingsPlaycount = New System.Windows.Forms.TableLayoutPanel()
        Me.chkSyncPlaycountSingleMovies = New System.Windows.Forms.CheckBox()
        Me.chkSyncPlaycountMultiMovies = New System.Windows.Forms.CheckBox()
        Me.chkSyncPlaycountEditMovies = New System.Windows.Forms.CheckBox()
        Me.chkSyncPlaycountMultiEpisodes = New System.Windows.Forms.CheckBox()
        Me.chkSyncPlaycountSingleEpisodes = New System.Windows.Forms.CheckBox()
        Me.chkSyncPlaycountEditEpisodes = New System.Windows.Forms.CheckBox()
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
        Me.gbSettingsLastPlayed.SuspendLayout()
        Me.tblSettingsLastPlayed.SuspendLayout()
        Me.gbSettingsPlaycount.SuspendLayout()
        Me.tblSettingsPlaycount.SuspendLayout()
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
        Me.tblSettingsMain.Controls.Add(Me.gbSettingsLastPlayed, 0, 3)
        Me.tblSettingsMain.Controls.Add(Me.gbSettingsPlaycount, 0, 3)
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
        Me.tblSettingsMain.Size = New System.Drawing.Size(778, 557)
        Me.tblSettingsMain.TabIndex = 21
        '
        'gbSettingsLastPlayed
        '
        Me.gbSettingsLastPlayed.AutoSize = True
        Me.gbSettingsLastPlayed.Controls.Add(Me.tblSettingsLastPlayed)
        Me.gbSettingsLastPlayed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSettingsLastPlayed.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSettingsLastPlayed.Location = New System.Drawing.Point(3, 109)
        Me.gbSettingsLastPlayed.Name = "gbSettingsLastPlayed"
        Me.gbSettingsLastPlayed.Size = New System.Drawing.Size(383, 159)
        Me.gbSettingsLastPlayed.TabIndex = 56
        Me.gbSettingsLastPlayed.TabStop = False
        Me.gbSettingsLastPlayed.Text = "Last Played"
        '
        'tblSettingsLastPlayed
        '
        Me.tblSettingsLastPlayed.AutoSize = True
        Me.tblSettingsLastPlayed.ColumnCount = 1
        Me.tblSettingsLastPlayed.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsLastPlayed.Controls.Add(Me.chkSyncLastPlayedSingleMovies, 0, 0)
        Me.tblSettingsLastPlayed.Controls.Add(Me.chkSyncLastPlayedMultiMovies, 0, 1)
        Me.tblSettingsLastPlayed.Controls.Add(Me.chkSyncLastPlayedEditMovies, 0, 2)
        Me.tblSettingsLastPlayed.Controls.Add(Me.chkSyncLastPlayedMultiEpisodes, 0, 4)
        Me.tblSettingsLastPlayed.Controls.Add(Me.chkSyncLastPlayedSingleEpisodes, 0, 3)
        Me.tblSettingsLastPlayed.Controls.Add(Me.chkSyncLastPlayedEditEpisodes, 0, 5)
        Me.tblSettingsLastPlayed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsLastPlayed.Location = New System.Drawing.Point(3, 18)
        Me.tblSettingsLastPlayed.Name = "tblSettingsLastPlayed"
        Me.tblSettingsLastPlayed.RowCount = 7
        Me.tblSettingsLastPlayed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsLastPlayed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsLastPlayed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsLastPlayed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsLastPlayed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsLastPlayed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsLastPlayed.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsLastPlayed.Size = New System.Drawing.Size(377, 138)
        Me.tblSettingsLastPlayed.TabIndex = 7
        '
        'chkSyncLastPlayedSingleMovies
        '
        Me.chkSyncLastPlayedSingleMovies.AutoSize = True
        Me.chkSyncLastPlayedSingleMovies.Location = New System.Drawing.Point(3, 3)
        Me.chkSyncLastPlayedSingleMovies.Name = "chkSyncLastPlayedSingleMovies"
        Me.chkSyncLastPlayedSingleMovies.Size = New System.Drawing.Size(276, 17)
        Me.chkSyncLastPlayedSingleMovies.TabIndex = 45
        Me.chkSyncLastPlayedSingleMovies.Text = "Automatically Sync Movies During Single-Scraper"
        Me.chkSyncLastPlayedSingleMovies.UseVisualStyleBackColor = True
        '
        'chkSyncLastPlayedMultiMovies
        '
        Me.chkSyncLastPlayedMultiMovies.AutoSize = True
        Me.chkSyncLastPlayedMultiMovies.Location = New System.Drawing.Point(3, 26)
        Me.chkSyncLastPlayedMultiMovies.Name = "chkSyncLastPlayedMultiMovies"
        Me.chkSyncLastPlayedMultiMovies.Size = New System.Drawing.Size(271, 17)
        Me.chkSyncLastPlayedMultiMovies.TabIndex = 46
        Me.chkSyncLastPlayedMultiMovies.Text = "Automatically Sync Movies During Multi-Scraper"
        Me.chkSyncLastPlayedMultiMovies.UseVisualStyleBackColor = True
        '
        'chkSyncLastPlayedEditMovies
        '
        Me.chkSyncLastPlayedEditMovies.AutoSize = True
        Me.chkSyncLastPlayedEditMovies.Location = New System.Drawing.Point(3, 49)
        Me.chkSyncLastPlayedEditMovies.Name = "chkSyncLastPlayedEditMovies"
        Me.chkSyncLastPlayedEditMovies.Size = New System.Drawing.Size(220, 17)
        Me.chkSyncLastPlayedEditMovies.TabIndex = 48
        Me.chkSyncLastPlayedEditMovies.Text = "Automatically Sync Movies Before Edit"
        Me.chkSyncLastPlayedEditMovies.UseVisualStyleBackColor = True
        '
        'chkSyncLastPlayedMultiEpisodes
        '
        Me.chkSyncLastPlayedMultiEpisodes.AutoSize = True
        Me.chkSyncLastPlayedMultiEpisodes.Location = New System.Drawing.Point(3, 95)
        Me.chkSyncLastPlayedMultiEpisodes.Name = "chkSyncLastPlayedMultiEpisodes"
        Me.chkSyncLastPlayedMultiEpisodes.Size = New System.Drawing.Size(281, 17)
        Me.chkSyncLastPlayedMultiEpisodes.TabIndex = 49
        Me.chkSyncLastPlayedMultiEpisodes.Text = "Automatically Sync Episodes During Multi-Scraper"
        Me.chkSyncLastPlayedMultiEpisodes.UseVisualStyleBackColor = True
        '
        'chkSyncLastPlayedSingleEpisodes
        '
        Me.chkSyncLastPlayedSingleEpisodes.AutoSize = True
        Me.chkSyncLastPlayedSingleEpisodes.Location = New System.Drawing.Point(3, 72)
        Me.chkSyncLastPlayedSingleEpisodes.Name = "chkSyncLastPlayedSingleEpisodes"
        Me.chkSyncLastPlayedSingleEpisodes.Size = New System.Drawing.Size(286, 17)
        Me.chkSyncLastPlayedSingleEpisodes.TabIndex = 51
        Me.chkSyncLastPlayedSingleEpisodes.Text = "Automatically Sync Episodes During Single-Scraper"
        Me.chkSyncLastPlayedSingleEpisodes.UseVisualStyleBackColor = True
        '
        'chkSyncLastPlayedEditEpisodes
        '
        Me.chkSyncLastPlayedEditEpisodes.AutoSize = True
        Me.chkSyncLastPlayedEditEpisodes.Location = New System.Drawing.Point(3, 118)
        Me.chkSyncLastPlayedEditEpisodes.Name = "chkSyncLastPlayedEditEpisodes"
        Me.chkSyncLastPlayedEditEpisodes.Size = New System.Drawing.Size(230, 17)
        Me.chkSyncLastPlayedEditEpisodes.TabIndex = 52
        Me.chkSyncLastPlayedEditEpisodes.Text = "Automatically Sync Episodes Before Edit"
        Me.chkSyncLastPlayedEditEpisodes.UseVisualStyleBackColor = True
        '
        'gbSettingsPlaycount
        '
        Me.gbSettingsPlaycount.AutoSize = True
        Me.gbSettingsPlaycount.Controls.Add(Me.tblSettingsPlaycount)
        Me.gbSettingsPlaycount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSettingsPlaycount.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSettingsPlaycount.Location = New System.Drawing.Point(392, 109)
        Me.gbSettingsPlaycount.Name = "gbSettingsPlaycount"
        Me.gbSettingsPlaycount.Size = New System.Drawing.Size(383, 159)
        Me.gbSettingsPlaycount.TabIndex = 55
        Me.gbSettingsPlaycount.TabStop = False
        Me.gbSettingsPlaycount.Text = "Playcount"
        '
        'tblSettingsPlaycount
        '
        Me.tblSettingsPlaycount.AutoSize = True
        Me.tblSettingsPlaycount.ColumnCount = 1
        Me.tblSettingsPlaycount.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsPlaycount.Controls.Add(Me.chkSyncPlaycountSingleMovies, 0, 0)
        Me.tblSettingsPlaycount.Controls.Add(Me.chkSyncPlaycountMultiMovies, 0, 2)
        Me.tblSettingsPlaycount.Controls.Add(Me.chkSyncPlaycountEditMovies, 0, 3)
        Me.tblSettingsPlaycount.Controls.Add(Me.chkSyncPlaycountMultiEpisodes, 0, 5)
        Me.tblSettingsPlaycount.Controls.Add(Me.chkSyncPlaycountSingleEpisodes, 0, 4)
        Me.tblSettingsPlaycount.Controls.Add(Me.chkSyncPlaycountEditEpisodes, 0, 6)
        Me.tblSettingsPlaycount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsPlaycount.Location = New System.Drawing.Point(3, 18)
        Me.tblSettingsPlaycount.Name = "tblSettingsPlaycount"
        Me.tblSettingsPlaycount.RowCount = 7
        Me.tblSettingsPlaycount.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsPlaycount.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsPlaycount.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsPlaycount.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsPlaycount.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsPlaycount.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsPlaycount.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsPlaycount.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsPlaycount.Size = New System.Drawing.Size(377, 138)
        Me.tblSettingsPlaycount.TabIndex = 7
        '
        'chkSyncPlaycountSingleMovies
        '
        Me.chkSyncPlaycountSingleMovies.AutoSize = True
        Me.chkSyncPlaycountSingleMovies.Location = New System.Drawing.Point(3, 3)
        Me.chkSyncPlaycountSingleMovies.Name = "chkSyncPlaycountSingleMovies"
        Me.chkSyncPlaycountSingleMovies.Size = New System.Drawing.Size(276, 17)
        Me.chkSyncPlaycountSingleMovies.TabIndex = 45
        Me.chkSyncPlaycountSingleMovies.Text = "Automatically Sync Movies During Single-Scraper"
        Me.chkSyncPlaycountSingleMovies.UseVisualStyleBackColor = True
        '
        'chkSyncPlaycountMultiMovies
        '
        Me.chkSyncPlaycountMultiMovies.AutoSize = True
        Me.chkSyncPlaycountMultiMovies.Location = New System.Drawing.Point(3, 26)
        Me.chkSyncPlaycountMultiMovies.Name = "chkSyncPlaycountMultiMovies"
        Me.chkSyncPlaycountMultiMovies.Size = New System.Drawing.Size(271, 17)
        Me.chkSyncPlaycountMultiMovies.TabIndex = 46
        Me.chkSyncPlaycountMultiMovies.Text = "Automatically Sync Movies During Multi-Scraper"
        Me.chkSyncPlaycountMultiMovies.UseVisualStyleBackColor = True
        '
        'chkSyncPlaycountEditMovies
        '
        Me.chkSyncPlaycountEditMovies.AutoSize = True
        Me.chkSyncPlaycountEditMovies.Location = New System.Drawing.Point(3, 49)
        Me.chkSyncPlaycountEditMovies.Name = "chkSyncPlaycountEditMovies"
        Me.chkSyncPlaycountEditMovies.Size = New System.Drawing.Size(220, 17)
        Me.chkSyncPlaycountEditMovies.TabIndex = 48
        Me.chkSyncPlaycountEditMovies.Text = "Automatically Sync Movies Before Edit"
        Me.chkSyncPlaycountEditMovies.UseVisualStyleBackColor = True
        '
        'chkSyncPlaycountMultiEpisodes
        '
        Me.chkSyncPlaycountMultiEpisodes.AutoSize = True
        Me.chkSyncPlaycountMultiEpisodes.Location = New System.Drawing.Point(3, 95)
        Me.chkSyncPlaycountMultiEpisodes.Name = "chkSyncPlaycountMultiEpisodes"
        Me.chkSyncPlaycountMultiEpisodes.Size = New System.Drawing.Size(281, 17)
        Me.chkSyncPlaycountMultiEpisodes.TabIndex = 49
        Me.chkSyncPlaycountMultiEpisodes.Text = "Automatically Sync Episodes During Multi-Scraper"
        Me.chkSyncPlaycountMultiEpisodes.UseVisualStyleBackColor = True
        '
        'chkSyncPlaycountSingleEpisodes
        '
        Me.chkSyncPlaycountSingleEpisodes.AutoSize = True
        Me.chkSyncPlaycountSingleEpisodes.Location = New System.Drawing.Point(3, 72)
        Me.chkSyncPlaycountSingleEpisodes.Name = "chkSyncPlaycountSingleEpisodes"
        Me.chkSyncPlaycountSingleEpisodes.Size = New System.Drawing.Size(286, 17)
        Me.chkSyncPlaycountSingleEpisodes.TabIndex = 51
        Me.chkSyncPlaycountSingleEpisodes.Text = "Automatically Sync Episodes During Single-Scraper"
        Me.chkSyncPlaycountSingleEpisodes.UseVisualStyleBackColor = True
        '
        'chkSyncPlaycountEditEpisodes
        '
        Me.chkSyncPlaycountEditEpisodes.AutoSize = True
        Me.chkSyncPlaycountEditEpisodes.Location = New System.Drawing.Point(3, 118)
        Me.chkSyncPlaycountEditEpisodes.Name = "chkSyncPlaycountEditEpisodes"
        Me.chkSyncPlaycountEditEpisodes.Size = New System.Drawing.Size(230, 17)
        Me.chkSyncPlaycountEditEpisodes.TabIndex = 52
        Me.chkSyncPlaycountEditEpisodes.Text = "Automatically Sync Episodes Before Edit"
        Me.chkSyncPlaycountEditEpisodes.UseVisualStyleBackColor = True
        '
        'gbSettingsGeneral
        '
        Me.gbSettingsGeneral.AutoSize = True
        Me.gbSettingsGeneral.Controls.Add(Me.tblSettingsGeneral)
        Me.gbSettingsGeneral.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.txtPassword.Location = New System.Drawing.Point(67, 31)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(200, 22)
        Me.txtPassword.TabIndex = 41
        '
        'lblPassword
        '
        Me.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(3, 35)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 42
        Me.lblPassword.Text = "Password"
        '
        'txtUsername
        '
        Me.txtUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtUsername.Location = New System.Drawing.Point(67, 3)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(200, 22)
        Me.txtUsername.TabIndex = 39
        '
        'lblUsername
        '
        Me.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(3, 7)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 40
        Me.lblUsername.Text = "Username"
        '
        'chkGetShowProgress
        '
        Me.chkGetShowProgress.AutoSize = True
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
        Me.pnlSettingsMain.Size = New System.Drawing.Size(778, 557)
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
        Me.pnlSettings.Size = New System.Drawing.Size(778, 580)
        Me.pnlSettings.TabIndex = 1
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(778, 580)
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
        Me.gbSettingsLastPlayed.ResumeLayout(False)
        Me.gbSettingsLastPlayed.PerformLayout()
        Me.tblSettingsLastPlayed.ResumeLayout(False)
        Me.tblSettingsLastPlayed.PerformLayout()
        Me.gbSettingsPlaycount.ResumeLayout(False)
        Me.gbSettingsPlaycount.PerformLayout()
        Me.tblSettingsPlaycount.ResumeLayout(False)
        Me.tblSettingsPlaycount.PerformLayout()
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
    Friend WithEvents gbSettingsPlaycount As System.Windows.Forms.GroupBox
    Friend WithEvents tblSettingsPlaycount As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkSyncPlaycountSingleMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncPlaycountMultiMovies As System.Windows.Forms.CheckBox
    Friend WithEvents gbSettingsLastPlayed As System.Windows.Forms.GroupBox
    Friend WithEvents tblSettingsLastPlayed As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkSyncLastPlayedSingleMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncLastPlayedMultiMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncLastPlayedEditMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncLastPlayedMultiEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncLastPlayedSingleEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncLastPlayedEditEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncPlaycountEditMovies As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncPlaycountMultiEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncPlaycountSingleEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkSyncPlaycountEditEpisodes As System.Windows.Forms.CheckBox

End Class
