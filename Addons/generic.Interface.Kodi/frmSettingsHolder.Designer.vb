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
        Me.tbllSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbHosts = New System.Windows.Forms.GroupBox()
        Me.tblSettingsGeneral = New System.Windows.Forms.TableLayoutPanel()
        Me.lbHosts = New System.Windows.Forms.ListBox()
        Me.btnEditHost = New System.Windows.Forms.Button()
        Me.btnRemoveHost = New System.Windows.Forms.Button()
        Me.chkNotification = New System.Windows.Forms.CheckBox()
        Me.btnAddHost = New System.Windows.Forms.Button()
        Me.gbGetWatchedState = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGetWatchedStateTVEpisodes = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGetWatchedStateBeforeEdit_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperMulti_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperSingle_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedState = New System.Windows.Forms.CheckBox()
        Me.cbGetWatchedStateHost = New System.Windows.Forms.ComboBox()
        Me.gbGetWatchedStateMovies = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGetWatchedStateBeforeEdit_Movie = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperMulti_Movie = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperSingle_Movie = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tbllSettingsMain.SuspendLayout()
        Me.gbHosts.SuspendLayout()
        Me.tblSettingsGeneral.SuspendLayout()
        Me.gbGetWatchedState.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbGetWatchedStateTVEpisodes.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.gbGetWatchedStateMovies.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(695, 516)
        Me.pnlSettings.TabIndex = 84
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tbllSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(695, 493)
        Me.pnlSettingsMain.TabIndex = 5
        '
        'tbllSettingsMain
        '
        Me.tbllSettingsMain.AutoScroll = True
        Me.tbllSettingsMain.AutoSize = True
        Me.tbllSettingsMain.ColumnCount = 2
        Me.tbllSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tbllSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tbllSettingsMain.Controls.Add(Me.gbHosts, 0, 0)
        Me.tbllSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbllSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tbllSettingsMain.Name = "tbllSettingsMain"
        Me.tbllSettingsMain.RowCount = 2
        Me.tbllSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tbllSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tbllSettingsMain.Size = New System.Drawing.Size(695, 493)
        Me.tbllSettingsMain.TabIndex = 1
        '
        'gbHosts
        '
        Me.gbHosts.AutoSize = True
        Me.gbHosts.Controls.Add(Me.tblSettingsGeneral)
        Me.gbHosts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbHosts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbHosts.Location = New System.Drawing.Point(3, 3)
        Me.gbHosts.Name = "gbHosts"
        Me.gbHosts.Size = New System.Drawing.Size(488, 342)
        Me.gbHosts.TabIndex = 0
        Me.gbHosts.TabStop = False
        Me.gbHosts.Text = "General Settings"
        '
        'tblSettingsGeneral
        '
        Me.tblSettingsGeneral.AutoSize = True
        Me.tblSettingsGeneral.ColumnCount = 5
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsGeneral.Controls.Add(Me.lbHosts, 0, 0)
        Me.tblSettingsGeneral.Controls.Add(Me.btnEditHost, 2, 2)
        Me.tblSettingsGeneral.Controls.Add(Me.btnRemoveHost, 1, 2)
        Me.tblSettingsGeneral.Controls.Add(Me.chkNotification, 3, 0)
        Me.tblSettingsGeneral.Controls.Add(Me.btnAddHost, 0, 2)
        Me.tblSettingsGeneral.Controls.Add(Me.gbGetWatchedState, 3, 1)
        Me.tblSettingsGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsGeneral.Location = New System.Drawing.Point(3, 18)
        Me.tblSettingsGeneral.Name = "tblSettingsGeneral"
        Me.tblSettingsGeneral.RowCount = 4
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsGeneral.Size = New System.Drawing.Size(482, 321)
        Me.tblSettingsGeneral.TabIndex = 87
        '
        'lbHosts
        '
        Me.tblSettingsGeneral.SetColumnSpan(Me.lbHosts, 3)
        Me.lbHosts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbHosts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbHosts.FormattingEnabled = True
        Me.lbHosts.Location = New System.Drawing.Point(3, 3)
        Me.lbHosts.Name = "lbHosts"
        Me.tblSettingsGeneral.SetRowSpan(Me.lbHosts, 2)
        Me.lbHosts.Size = New System.Drawing.Size(300, 286)
        Me.lbHosts.Sorted = True
        Me.lbHosts.TabIndex = 8
        '
        'btnEditHost
        '
        Me.btnEditHost.Enabled = False
        Me.btnEditHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditHost.Image = CType(resources.GetObject("btnEditHost.Image"), System.Drawing.Image)
        Me.btnEditHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditHost.Location = New System.Drawing.Point(189, 295)
        Me.btnEditHost.Name = "btnEditHost"
        Me.btnEditHost.Size = New System.Drawing.Size(91, 23)
        Me.btnEditHost.TabIndex = 11
        Me.btnEditHost.Text = "Edit"
        Me.btnEditHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditHost.UseVisualStyleBackColor = True
        '
        'btnRemoveHost
        '
        Me.btnRemoveHost.Enabled = False
        Me.btnRemoveHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveHost.Image = CType(resources.GetObject("btnRemoveHost.Image"), System.Drawing.Image)
        Me.btnRemoveHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemoveHost.Location = New System.Drawing.Point(96, 295)
        Me.btnRemoveHost.Name = "btnRemoveHost"
        Me.btnRemoveHost.Size = New System.Drawing.Size(87, 23)
        Me.btnRemoveHost.TabIndex = 10
        Me.btnRemoveHost.Text = "Remove"
        Me.btnRemoveHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemoveHost.UseVisualStyleBackColor = True
        '
        'chkNotification
        '
        Me.chkNotification.AutoSize = True
        Me.chkNotification.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNotification.Location = New System.Drawing.Point(309, 3)
        Me.chkNotification.Name = "chkNotification"
        Me.chkNotification.Size = New System.Drawing.Size(121, 17)
        Me.chkNotification.TabIndex = 15
        Me.chkNotification.Text = "Send Notifications"
        Me.chkNotification.UseVisualStyleBackColor = True
        '
        'btnAddHost
        '
        Me.btnAddHost.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddHost.Image = CType(resources.GetObject("btnAddHost.Image"), System.Drawing.Image)
        Me.btnAddHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddHost.Location = New System.Drawing.Point(3, 295)
        Me.btnAddHost.Name = "btnAddHost"
        Me.btnAddHost.Size = New System.Drawing.Size(87, 23)
        Me.btnAddHost.TabIndex = 9
        Me.btnAddHost.Text = "Add"
        Me.btnAddHost.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddHost.UseVisualStyleBackColor = True
        '
        'gbGetWatchedState
        '
        Me.gbGetWatchedState.AutoSize = True
        Me.gbGetWatchedState.Controls.Add(Me.TableLayoutPanel1)
        Me.gbGetWatchedState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedState.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGetWatchedState.Location = New System.Drawing.Point(309, 26)
        Me.gbGetWatchedState.Name = "gbGetWatchedState"
        Me.gbGetWatchedState.Size = New System.Drawing.Size(170, 263)
        Me.gbGetWatchedState.TabIndex = 87
        Me.gbGetWatchedState.TabStop = False
        Me.gbGetWatchedState.Text = "Watched State"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.gbGetWatchedStateTVEpisodes, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.chkGetWatchedState, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cbGetWatchedStateHost, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.gbGetWatchedStateMovies, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(164, 242)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'gbGetWatchedStateTVEpisodes
        '
        Me.gbGetWatchedStateTVEpisodes.AutoSize = True
        Me.gbGetWatchedStateTVEpisodes.Controls.Add(Me.TableLayoutPanel3)
        Me.gbGetWatchedStateTVEpisodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedStateTVEpisodes.Enabled = False
        Me.gbGetWatchedStateTVEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGetWatchedStateTVEpisodes.Location = New System.Drawing.Point(3, 149)
        Me.gbGetWatchedStateTVEpisodes.Name = "gbGetWatchedStateTVEpisodes"
        Me.gbGetWatchedStateTVEpisodes.Size = New System.Drawing.Size(158, 90)
        Me.gbGetWatchedStateTVEpisodes.TabIndex = 89
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
        'chkGetWatchedState
        '
        Me.chkGetWatchedState.AutoSize = True
        Me.chkGetWatchedState.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGetWatchedState.Location = New System.Drawing.Point(3, 3)
        Me.chkGetWatchedState.Name = "chkGetWatchedState"
        Me.chkGetWatchedState.Size = New System.Drawing.Size(152, 17)
        Me.chkGetWatchedState.TabIndex = 85
        Me.chkGetWatchedState.Text = "Get Watched State from:"
        Me.chkGetWatchedState.UseVisualStyleBackColor = True
        '
        'cbGetWatchedStateHost
        '
        Me.cbGetWatchedStateHost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGetWatchedStateHost.Enabled = False
        Me.cbGetWatchedStateHost.FormattingEnabled = True
        Me.cbGetWatchedStateHost.Location = New System.Drawing.Point(3, 26)
        Me.cbGetWatchedStateHost.Name = "cbGetWatchedStateHost"
        Me.cbGetWatchedStateHost.Size = New System.Drawing.Size(152, 21)
        Me.cbGetWatchedStateHost.TabIndex = 86
        '
        'gbGetWatchedStateMovies
        '
        Me.gbGetWatchedStateMovies.AutoSize = True
        Me.gbGetWatchedStateMovies.Controls.Add(Me.TableLayoutPanel2)
        Me.gbGetWatchedStateMovies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedStateMovies.Enabled = False
        Me.gbGetWatchedStateMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbGetWatchedStateMovies.Location = New System.Drawing.Point(3, 53)
        Me.gbGetWatchedStateMovies.Name = "gbGetWatchedStateMovies"
        Me.gbGetWatchedStateMovies.Size = New System.Drawing.Size(158, 90)
        Me.gbGetWatchedStateMovies.TabIndex = 88
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
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(695, 23)
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
        Me.tblSettingsTop.Size = New System.Drawing.Size(695, 23)
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
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(695, 516)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings for Kodi Interface"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tbllSettingsMain.ResumeLayout(False)
        Me.tbllSettingsMain.PerformLayout()
        Me.gbHosts.ResumeLayout(False)
        Me.gbHosts.PerformLayout()
        Me.tblSettingsGeneral.ResumeLayout(False)
        Me.tblSettingsGeneral.PerformLayout()
        Me.gbGetWatchedState.ResumeLayout(False)
        Me.gbGetWatchedState.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.gbGetWatchedStateTVEpisodes.ResumeLayout(False)
        Me.gbGetWatchedStateTVEpisodes.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.gbGetWatchedStateMovies.ResumeLayout(False)
        Me.gbGetWatchedStateMovies.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbHosts As System.Windows.Forms.GroupBox
    Friend WithEvents chkNotification As System.Windows.Forms.CheckBox
    Friend WithEvents btnEditHost As System.Windows.Forms.Button
    Friend WithEvents btnRemoveHost As System.Windows.Forms.Button
    Friend WithEvents lbHosts As System.Windows.Forms.ListBox
    Friend WithEvents btnAddHost As System.Windows.Forms.Button
    Friend WithEvents cbGetWatchedStateHost As System.Windows.Forms.ComboBox
    Friend WithEvents chkGetWatchedState As System.Windows.Forms.CheckBox
    Friend WithEvents tbllSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblSettingsGeneral As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbGetWatchedState As GroupBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents chkGetWatchedStateBeforeEdit_Movie As CheckBox
    Friend WithEvents chkGetWatchedStateScraperMulti_Movie As CheckBox
    Friend WithEvents chkGetWatchedStateScraperSingle_Movie As CheckBox
    Friend WithEvents gbGetWatchedStateTVEpisodes As GroupBox
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents chkGetWatchedStateBeforeEdit_TVEpisode As CheckBox
    Friend WithEvents chkGetWatchedStateScraperMulti_TVEpisode As CheckBox
    Friend WithEvents chkGetWatchedStateScraperSingle_TVEpisode As CheckBox
    Friend WithEvents gbGetWatchedStateMovies As GroupBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
End Class
