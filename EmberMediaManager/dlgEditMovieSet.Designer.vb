<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEditMovieSet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditMovieSet))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.chkMark = New System.Windows.Forms.CheckBox()
        Me.btnRescrape = New System.Windows.Forms.Button()
        Me.btnChangeMovie = New System.Windows.Forms.Button()
        Me.tmrDelay = New System.Windows.Forms.Timer(Me.components)
        Me.chkWatched = New System.Windows.Forms.CheckBox()
        Me.tpEThumbs = New System.Windows.Forms.TabPage()
        Me.pbMovieEThumbs = New System.Windows.Forms.PictureBox()
        Me.btnMovieEThumbsUp = New System.Windows.Forms.Button()
        Me.btnMovieEThumbsDown = New System.Windows.Forms.Button()
        Me.btnMovieEThumbsRemove = New System.Windows.Forms.Button()
        Me.btnMovieEThumbsRefresh = New System.Windows.Forms.Button()
        Me.pnlEThumbsSetAsFanart = New System.Windows.Forms.Panel()
        Me.btnMovieEThumbsSetAsFanart = New System.Windows.Forms.Button()
        Me.pnlMovieETQueue = New System.Windows.Forms.Panel()
        Me.btnMovieEThumbsTransfer = New System.Windows.Forms.Button()
        Me.lbMovieEThumbsQueue = New System.Windows.Forms.Label()
        Me.pnlMovieEThumbsBG = New System.Windows.Forms.Panel()
        Me.lblMovieEThumbsSize = New System.Windows.Forms.Label()
        Me.tpEFanarts = New System.Windows.Forms.TabPage()
        Me.pbMovieEFanarts = New System.Windows.Forms.PictureBox()
        Me.pnlMovieEFanartsQueue = New System.Windows.Forms.Panel()
        Me.btnMovieEFanartsTransfer = New System.Windows.Forms.Button()
        Me.lbMovieEFanartsQueue = New System.Windows.Forms.Label()
        Me.btnMovieEFanartsRemove = New System.Windows.Forms.Button()
        Me.btnMovieEFanartsRefresh = New System.Windows.Forms.Button()
        Me.pnlEFanartsSetAsFanart = New System.Windows.Forms.Panel()
        Me.btnMovieEFanartsSetAsFanart = New System.Windows.Forms.Button()
        Me.pnlMovieEFanartsBG = New System.Windows.Forms.Panel()
        Me.lblMovieEFanartsSize = New System.Windows.Forms.Label()
        Me.tpFanart = New System.Windows.Forms.TabPage()
        Me.pbMovieFanart = New System.Windows.Forms.PictureBox()
        Me.btnSetMovieFanartLocal = New System.Windows.Forms.Button()
        Me.btnSetMovieFanartScrape = New System.Windows.Forms.Button()
        Me.lblMovieFanartSize = New System.Windows.Forms.Label()
        Me.btnRemoveMovieFanart = New System.Windows.Forms.Button()
        Me.btnSetMovieFanartDL = New System.Windows.Forms.Button()
        Me.tpDiscArt = New System.Windows.Forms.TabPage()
        Me.pbMovieDiscArt = New System.Windows.Forms.PictureBox()
        Me.btnSetMovieDiscArtLocal = New System.Windows.Forms.Button()
        Me.btnSetMovieDiscArtScrape = New System.Windows.Forms.Button()
        Me.lblMovieDiscArtSize = New System.Windows.Forms.Label()
        Me.btnRemoveMovieDiscArt = New System.Windows.Forms.Button()
        Me.btnSetMovieDiscArtDL = New System.Windows.Forms.Button()
        Me.tpClearLogo = New System.Windows.Forms.TabPage()
        Me.pbMovieClearLogo = New System.Windows.Forms.PictureBox()
        Me.btnSetMovieClearLogoLocal = New System.Windows.Forms.Button()
        Me.btnSetMovieClearLogoScrape = New System.Windows.Forms.Button()
        Me.lblMovieClearLogoSize = New System.Windows.Forms.Label()
        Me.btnRemoveMovieClearLogo = New System.Windows.Forms.Button()
        Me.btnSetMovieClearLogoDL = New System.Windows.Forms.Button()
        Me.tpClearArt = New System.Windows.Forms.TabPage()
        Me.pbMovieClearArt = New System.Windows.Forms.PictureBox()
        Me.btnSetMovieClearArtLocal = New System.Windows.Forms.Button()
        Me.btnSetMovieClearArtScrape = New System.Windows.Forms.Button()
        Me.lblMovieClearArtSize = New System.Windows.Forms.Label()
        Me.btnRemoveMovieClearArt = New System.Windows.Forms.Button()
        Me.btnSetMovieClearArtDL = New System.Windows.Forms.Button()
        Me.tpLandscape = New System.Windows.Forms.TabPage()
        Me.pbMovieLandscape = New System.Windows.Forms.PictureBox()
        Me.btnSetMovieLandscapeLocal = New System.Windows.Forms.Button()
        Me.btnSetMovieLandscapeScrape = New System.Windows.Forms.Button()
        Me.lblMovieLandscapeSize = New System.Windows.Forms.Label()
        Me.btnRemoveMovieLandscape = New System.Windows.Forms.Button()
        Me.btnSetMovieLandscapeDL = New System.Windows.Forms.Button()
        Me.tpBanner = New System.Windows.Forms.TabPage()
        Me.pbMovieBanner = New System.Windows.Forms.PictureBox()
        Me.btnSetMovieBannerLocal = New System.Windows.Forms.Button()
        Me.btnSetMovieBannerScrape = New System.Windows.Forms.Button()
        Me.lblMovieBannerSize = New System.Windows.Forms.Label()
        Me.btnRemoveMovieBanner = New System.Windows.Forms.Button()
        Me.btnSetMovieBannerDL = New System.Windows.Forms.Button()
        Me.tpPoster = New System.Windows.Forms.TabPage()
        Me.pbMoviePoster = New System.Windows.Forms.PictureBox()
        Me.btnSetMoviePosterLocal = New System.Windows.Forms.Button()
        Me.btnSetMoviePosterScrape = New System.Windows.Forms.Button()
        Me.lblMoviePosterSize = New System.Windows.Forms.Label()
        Me.btnRemoveMoviePoster = New System.Windows.Forms.Button()
        Me.btnSetMoviePosterDL = New System.Windows.Forms.Button()
        Me.tpDetails = New System.Windows.Forms.TabPage()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.mtxtYear = New System.Windows.Forms.MaskedTextBox()
        Me.lblRating = New System.Windows.Forms.Label()
        Me.lblVotes = New System.Windows.Forms.Label()
        Me.txtVotes = New System.Windows.Forms.TextBox()
        Me.pbStar1 = New System.Windows.Forms.PictureBox()
        Me.pbStar2 = New System.Windows.Forms.PictureBox()
        Me.pbStar3 = New System.Windows.Forms.PictureBox()
        Me.pbStar4 = New System.Windows.Forms.PictureBox()
        Me.pbStar5 = New System.Windows.Forms.PictureBox()
        Me.txtTagline = New System.Windows.Forms.TextBox()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.txtOutline = New System.Windows.Forms.TextBox()
        Me.lblOutline = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.lblTop250 = New System.Windows.Forms.Label()
        Me.txtTop250 = New System.Windows.Forms.TextBox()
        Me.txtDirector = New System.Windows.Forms.TextBox()
        Me.lblDirector = New System.Windows.Forms.Label()
        Me.lblMPAA = New System.Windows.Forms.Label()
        Me.lblGenre = New System.Windows.Forms.Label()
        Me.lbMPAA = New System.Windows.Forms.ListBox()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblActors = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnAddActor = New System.Windows.Forms.Button()
        Me.btnEditActor = New System.Windows.Forms.Button()
        Me.txtMPAADesc = New System.Windows.Forms.TextBox()
        Me.lblMPAADesc = New System.Windows.Forms.Label()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.txtCerts = New System.Windows.Forms.TextBox()
        Me.lblCerts = New System.Windows.Forms.Label()
        Me.txtCredits = New System.Windows.Forms.TextBox()
        Me.lblCredits = New System.Windows.Forms.Label()
        Me.lblReleaseDate = New System.Windows.Forms.Label()
        Me.txtReleaseDate = New System.Windows.Forms.TextBox()
        Me.txtTrailer = New System.Windows.Forms.TextBox()
        Me.lblTrailerURL = New System.Windows.Forms.Label()
        Me.txtStudio = New System.Windows.Forms.TextBox()
        Me.lblStudio = New System.Windows.Forms.Label()
        Me.btnStudio = New System.Windows.Forms.Button()
        Me.clbGenre = New System.Windows.Forms.CheckedListBox()
        Me.btnDLTrailer = New System.Windows.Forms.Button()
        Me.btnPlayTrailer = New System.Windows.Forms.Button()
        Me.lblLocalTrailer = New System.Windows.Forms.Label()
        Me.txtSortTitle = New System.Windows.Forms.TextBox()
        Me.lblSortTilte = New System.Windows.Forms.Label()
        Me.btnActorUp = New System.Windows.Forms.Button()
        Me.btnActorDown = New System.Windows.Forms.Button()
        Me.lblFileSource = New System.Windows.Forms.Label()
        Me.txtFileSource = New System.Windows.Forms.TextBox()
        Me.lblCountry = New System.Windows.Forms.Label()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.txtOriginalTitle = New System.Windows.Forms.TextBox()
        Me.btnDLTheme = New System.Windows.Forms.Button()
        Me.btnPlayTheme = New System.Windows.Forms.Button()
        Me.lblTheme = New System.Windows.Forms.Label()
        Me.tcEditMovie = New System.Windows.Forms.TabControl()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpEThumbs.SuspendLayout()
        CType(Me.pbMovieEThumbs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEThumbsSetAsFanart.SuspendLayout()
        Me.pnlMovieETQueue.SuspendLayout()
        Me.tpEFanarts.SuspendLayout()
        CType(Me.pbMovieEFanarts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMovieEFanartsQueue.SuspendLayout()
        Me.pnlEFanartsSetAsFanart.SuspendLayout()
        Me.tpFanart.SuspendLayout()
        CType(Me.pbMovieFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDiscArt.SuspendLayout()
        CType(Me.pbMovieDiscArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpClearLogo.SuspendLayout()
        CType(Me.pbMovieClearLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpClearArt.SuspendLayout()
        CType(Me.pbMovieClearArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpLandscape.SuspendLayout()
        CType(Me.pbMovieLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBanner.SuspendLayout()
        CType(Me.pbMovieBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPoster.SuspendLayout()
        CType(Me.pbMoviePoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDetails.SuspendLayout()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcEditMovie.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(708, 592)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(781, 592)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblTopDetails)
        Me.pnlTop.Controls.Add(Me.lblTopTitle)
        Me.pnlTop.Controls.Add(Me.pbTopLogo)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(854, 64)
        Me.pnlTop.TabIndex = 2
        '
        'lblTopDetails
        '
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(61, 38)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(220, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Edit the details for the selected movieset."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(170, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Edit Movieset"
        '
        'pbTopLogo
        '
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.Location = New System.Drawing.Point(7, 8)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.pbTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'chkMark
        '
        Me.chkMark.AutoSize = True
        Me.chkMark.Enabled = False
        Me.chkMark.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMark.Location = New System.Drawing.Point(4, 598)
        Me.chkMark.Name = "chkMark"
        Me.chkMark.Size = New System.Drawing.Size(86, 17)
        Me.chkMark.TabIndex = 5
        Me.chkMark.Text = "Mark Movie"
        Me.chkMark.UseVisualStyleBackColor = True
        Me.chkMark.Visible = False
        '
        'btnRescrape
        '
        Me.btnRescrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRescrape.Image = CType(resources.GetObject("btnRescrape.Image"), System.Drawing.Image)
        Me.btnRescrape.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRescrape.Location = New System.Drawing.Point(310, 592)
        Me.btnRescrape.Name = "btnRescrape"
        Me.btnRescrape.Size = New System.Drawing.Size(98, 23)
        Me.btnRescrape.TabIndex = 7
        Me.btnRescrape.Text = "Re-scrape"
        Me.btnRescrape.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRescrape.UseVisualStyleBackColor = True
        '
        'btnChangeMovie
        '
        Me.btnChangeMovie.Enabled = False
        Me.btnChangeMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnChangeMovie.Image = CType(resources.GetObject("btnChangeMovie.Image"), System.Drawing.Image)
        Me.btnChangeMovie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnChangeMovie.Location = New System.Drawing.Point(429, 592)
        Me.btnChangeMovie.Name = "btnChangeMovie"
        Me.btnChangeMovie.Size = New System.Drawing.Size(107, 23)
        Me.btnChangeMovie.TabIndex = 8
        Me.btnChangeMovie.Text = "Change Movie"
        Me.btnChangeMovie.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnChangeMovie.UseVisualStyleBackColor = True
        Me.btnChangeMovie.Visible = False
        '
        'tmrDelay
        '
        Me.tmrDelay.Interval = 250
        '
        'chkWatched
        '
        Me.chkWatched.AutoSize = True
        Me.chkWatched.Enabled = False
        Me.chkWatched.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWatched.Location = New System.Drawing.Point(96, 598)
        Me.chkWatched.Name = "chkWatched"
        Me.chkWatched.Size = New System.Drawing.Size(72, 17)
        Me.chkWatched.TabIndex = 6
        Me.chkWatched.Text = "Watched"
        Me.chkWatched.UseVisualStyleBackColor = True
        Me.chkWatched.Visible = False
        '
        'tpEThumbs
        '
        Me.tpEThumbs.Controls.Add(Me.lblMovieEThumbsSize)
        Me.tpEThumbs.Controls.Add(Me.pnlMovieEThumbsBG)
        Me.tpEThumbs.Controls.Add(Me.pnlMovieETQueue)
        Me.tpEThumbs.Controls.Add(Me.pnlEThumbsSetAsFanart)
        Me.tpEThumbs.Controls.Add(Me.btnMovieEThumbsRefresh)
        Me.tpEThumbs.Controls.Add(Me.btnMovieEThumbsRemove)
        Me.tpEThumbs.Controls.Add(Me.btnMovieEThumbsDown)
        Me.tpEThumbs.Controls.Add(Me.btnMovieEThumbsUp)
        Me.tpEThumbs.Controls.Add(Me.pbMovieEThumbs)
        Me.tpEThumbs.Location = New System.Drawing.Point(4, 22)
        Me.tpEThumbs.Name = "tpEThumbs"
        Me.tpEThumbs.Size = New System.Drawing.Size(836, 491)
        Me.tpEThumbs.TabIndex = 4
        Me.tpEThumbs.Text = "Extrathumbs"
        Me.tpEThumbs.UseVisualStyleBackColor = True
        '
        'pbMovieEThumbs
        '
        Me.pbMovieEThumbs.BackColor = System.Drawing.Color.DimGray
        Me.pbMovieEThumbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMovieEThumbs.Location = New System.Drawing.Point(176, 8)
        Me.pbMovieEThumbs.Name = "pbMovieEThumbs"
        Me.pbMovieEThumbs.Size = New System.Drawing.Size(653, 437)
        Me.pbMovieEThumbs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMovieEThumbs.TabIndex = 2
        Me.pbMovieEThumbs.TabStop = False
        '
        'btnMovieEThumbsUp
        '
        Me.btnMovieEThumbsUp.Enabled = False
        Me.btnMovieEThumbsUp.Image = CType(resources.GetObject("btnMovieEThumbsUp.Image"), System.Drawing.Image)
        Me.btnMovieEThumbsUp.Location = New System.Drawing.Point(4, 422)
        Me.btnMovieEThumbsUp.Name = "btnMovieEThumbsUp"
        Me.btnMovieEThumbsUp.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieEThumbsUp.TabIndex = 2
        Me.btnMovieEThumbsUp.UseVisualStyleBackColor = True
        Me.btnMovieEThumbsUp.Visible = False
        '
        'btnMovieEThumbsDown
        '
        Me.btnMovieEThumbsDown.Enabled = False
        Me.btnMovieEThumbsDown.Image = CType(resources.GetObject("btnMovieEThumbsDown.Image"), System.Drawing.Image)
        Me.btnMovieEThumbsDown.Location = New System.Drawing.Point(28, 422)
        Me.btnMovieEThumbsDown.Name = "btnMovieEThumbsDown"
        Me.btnMovieEThumbsDown.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieEThumbsDown.TabIndex = 3
        Me.btnMovieEThumbsDown.UseVisualStyleBackColor = True
        Me.btnMovieEThumbsDown.Visible = False
        '
        'btnMovieEThumbsRemove
        '
        Me.btnMovieEThumbsRemove.Image = CType(resources.GetObject("btnMovieEThumbsRemove.Image"), System.Drawing.Image)
        Me.btnMovieEThumbsRemove.Location = New System.Drawing.Point(147, 422)
        Me.btnMovieEThumbsRemove.Name = "btnMovieEThumbsRemove"
        Me.btnMovieEThumbsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieEThumbsRemove.TabIndex = 5
        Me.btnMovieEThumbsRemove.UseVisualStyleBackColor = True
        '
        'btnMovieEThumbsRefresh
        '
        Me.btnMovieEThumbsRefresh.Image = CType(resources.GetObject("btnMovieEThumbsRefresh.Image"), System.Drawing.Image)
        Me.btnMovieEThumbsRefresh.Location = New System.Drawing.Point(87, 422)
        Me.btnMovieEThumbsRefresh.Name = "btnMovieEThumbsRefresh"
        Me.btnMovieEThumbsRefresh.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieEThumbsRefresh.TabIndex = 4
        Me.btnMovieEThumbsRefresh.UseVisualStyleBackColor = True
        '
        'pnlEThumbsSetAsFanart
        '
        Me.pnlEThumbsSetAsFanart.BackColor = System.Drawing.Color.LightGray
        Me.pnlEThumbsSetAsFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlEThumbsSetAsFanart.Controls.Add(Me.btnMovieEThumbsSetAsFanart)
        Me.pnlEThumbsSetAsFanart.Location = New System.Drawing.Point(718, 403)
        Me.pnlEThumbsSetAsFanart.Name = "pnlEThumbsSetAsFanart"
        Me.pnlEThumbsSetAsFanart.Size = New System.Drawing.Size(109, 39)
        Me.pnlEThumbsSetAsFanart.TabIndex = 6
        '
        'btnMovieEThumbsSetAsFanart
        '
        Me.btnMovieEThumbsSetAsFanart.Enabled = False
        Me.btnMovieEThumbsSetAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieEThumbsSetAsFanart.Image = CType(resources.GetObject("btnMovieEThumbsSetAsFanart.Image"), System.Drawing.Image)
        Me.btnMovieEThumbsSetAsFanart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieEThumbsSetAsFanart.Location = New System.Drawing.Point(2, 3)
        Me.btnMovieEThumbsSetAsFanart.Name = "btnMovieEThumbsSetAsFanart"
        Me.btnMovieEThumbsSetAsFanart.Size = New System.Drawing.Size(103, 32)
        Me.btnMovieEThumbsSetAsFanart.TabIndex = 0
        Me.btnMovieEThumbsSetAsFanart.Text = "Set As Fanart"
        Me.btnMovieEThumbsSetAsFanart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieEThumbsSetAsFanart.UseVisualStyleBackColor = True
        '
        'pnlMovieETQueue
        '
        Me.pnlMovieETQueue.BackColor = System.Drawing.Color.LightGray
        Me.pnlMovieETQueue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMovieETQueue.Controls.Add(Me.lbMovieEThumbsQueue)
        Me.pnlMovieETQueue.Controls.Add(Me.btnMovieEThumbsTransfer)
        Me.pnlMovieETQueue.Location = New System.Drawing.Point(626, 11)
        Me.pnlMovieETQueue.Name = "pnlMovieETQueue"
        Me.pnlMovieETQueue.Size = New System.Drawing.Size(201, 69)
        Me.pnlMovieETQueue.TabIndex = 1
        Me.pnlMovieETQueue.Visible = False
        '
        'btnMovieEThumbsTransfer
        '
        Me.btnMovieEThumbsTransfer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieEThumbsTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieEThumbsTransfer.Location = New System.Drawing.Point(53, 32)
        Me.btnMovieEThumbsTransfer.Name = "btnMovieEThumbsTransfer"
        Me.btnMovieEThumbsTransfer.Size = New System.Drawing.Size(103, 32)
        Me.btnMovieEThumbsTransfer.TabIndex = 1
        Me.btnMovieEThumbsTransfer.Text = "Transfer Now"
        Me.btnMovieEThumbsTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieEThumbsTransfer.UseVisualStyleBackColor = True
        '
        'lbMovieEThumbsQueue
        '
        Me.lbMovieEThumbsQueue.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbMovieEThumbsQueue.Location = New System.Drawing.Point(3, 3)
        Me.lbMovieEThumbsQueue.Name = "lbMovieEThumbsQueue"
        Me.lbMovieEThumbsQueue.Size = New System.Drawing.Size(193, 26)
        Me.lbMovieEThumbsQueue.TabIndex = 0
        Me.lbMovieEThumbsQueue.Text = "You have extrathumbs queued to be transferred to the movie directory."
        Me.lbMovieEThumbsQueue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMovieEThumbsBG
        '
        Me.pnlMovieEThumbsBG.AutoScroll = True
        Me.pnlMovieEThumbsBG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMovieEThumbsBG.Location = New System.Drawing.Point(5, 8)
        Me.pnlMovieEThumbsBG.Name = "pnlMovieEThumbsBG"
        Me.pnlMovieEThumbsBG.Size = New System.Drawing.Size(165, 408)
        Me.pnlMovieEThumbsBG.TabIndex = 7
        '
        'lblMovieEThumbsSize
        '
        Me.lblMovieEThumbsSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovieEThumbsSize.Location = New System.Drawing.Point(178, 10)
        Me.lblMovieEThumbsSize.Name = "lblMovieEThumbsSize"
        Me.lblMovieEThumbsSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMovieEThumbsSize.TabIndex = 17
        Me.lblMovieEThumbsSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMovieEThumbsSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMovieEThumbsSize.Visible = False
        '
        'tpEFanarts
        '
        Me.tpEFanarts.Controls.Add(Me.lblMovieEFanartsSize)
        Me.tpEFanarts.Controls.Add(Me.pnlMovieEFanartsBG)
        Me.tpEFanarts.Controls.Add(Me.pnlEFanartsSetAsFanart)
        Me.tpEFanarts.Controls.Add(Me.btnMovieEFanartsRefresh)
        Me.tpEFanarts.Controls.Add(Me.btnMovieEFanartsRemove)
        Me.tpEFanarts.Controls.Add(Me.pnlMovieEFanartsQueue)
        Me.tpEFanarts.Controls.Add(Me.pbMovieEFanarts)
        Me.tpEFanarts.Location = New System.Drawing.Point(4, 22)
        Me.tpEFanarts.Name = "tpEFanarts"
        Me.tpEFanarts.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEFanarts.Size = New System.Drawing.Size(836, 491)
        Me.tpEFanarts.TabIndex = 6
        Me.tpEFanarts.Text = "Extrafanarts"
        Me.tpEFanarts.UseVisualStyleBackColor = True
        '
        'pbMovieEFanarts
        '
        Me.pbMovieEFanarts.BackColor = System.Drawing.Color.DimGray
        Me.pbMovieEFanarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMovieEFanarts.Location = New System.Drawing.Point(176, 8)
        Me.pbMovieEFanarts.Name = "pbMovieEFanarts"
        Me.pbMovieEFanarts.Size = New System.Drawing.Size(653, 437)
        Me.pbMovieEFanarts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMovieEFanarts.TabIndex = 10
        Me.pbMovieEFanarts.TabStop = False
        '
        'pnlMovieEFanartsQueue
        '
        Me.pnlMovieEFanartsQueue.BackColor = System.Drawing.Color.LightGray
        Me.pnlMovieEFanartsQueue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMovieEFanartsQueue.Controls.Add(Me.lbMovieEFanartsQueue)
        Me.pnlMovieEFanartsQueue.Controls.Add(Me.btnMovieEFanartsTransfer)
        Me.pnlMovieEFanartsQueue.Location = New System.Drawing.Point(626, 11)
        Me.pnlMovieEFanartsQueue.Name = "pnlMovieEFanartsQueue"
        Me.pnlMovieEFanartsQueue.Size = New System.Drawing.Size(201, 69)
        Me.pnlMovieEFanartsQueue.TabIndex = 8
        Me.pnlMovieEFanartsQueue.Visible = False
        '
        'btnMovieEFanartsTransfer
        '
        Me.btnMovieEFanartsTransfer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieEFanartsTransfer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieEFanartsTransfer.Location = New System.Drawing.Point(53, 32)
        Me.btnMovieEFanartsTransfer.Name = "btnMovieEFanartsTransfer"
        Me.btnMovieEFanartsTransfer.Size = New System.Drawing.Size(103, 32)
        Me.btnMovieEFanartsTransfer.TabIndex = 1
        Me.btnMovieEFanartsTransfer.Text = "Transfer Now"
        Me.btnMovieEFanartsTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieEFanartsTransfer.UseVisualStyleBackColor = True
        '
        'lbMovieEFanartsQueue
        '
        Me.lbMovieEFanartsQueue.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbMovieEFanartsQueue.Location = New System.Drawing.Point(3, 3)
        Me.lbMovieEFanartsQueue.Name = "lbMovieEFanartsQueue"
        Me.lbMovieEFanartsQueue.Size = New System.Drawing.Size(193, 26)
        Me.lbMovieEFanartsQueue.TabIndex = 0
        Me.lbMovieEFanartsQueue.Text = "You have extrafanarts queued to be transferred to the movie directory."
        Me.lbMovieEFanartsQueue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMovieEFanartsRemove
        '
        Me.btnMovieEFanartsRemove.Image = CType(resources.GetObject("btnMovieEFanartsRemove.Image"), System.Drawing.Image)
        Me.btnMovieEFanartsRemove.Location = New System.Drawing.Point(147, 422)
        Me.btnMovieEFanartsRemove.Name = "btnMovieEFanartsRemove"
        Me.btnMovieEFanartsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieEFanartsRemove.TabIndex = 13
        Me.btnMovieEFanartsRemove.UseVisualStyleBackColor = True
        '
        'btnMovieEFanartsRefresh
        '
        Me.btnMovieEFanartsRefresh.Image = CType(resources.GetObject("btnMovieEFanartsRefresh.Image"), System.Drawing.Image)
        Me.btnMovieEFanartsRefresh.Location = New System.Drawing.Point(87, 422)
        Me.btnMovieEFanartsRefresh.Name = "btnMovieEFanartsRefresh"
        Me.btnMovieEFanartsRefresh.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieEFanartsRefresh.TabIndex = 12
        Me.btnMovieEFanartsRefresh.UseVisualStyleBackColor = True
        '
        'pnlEFanartsSetAsFanart
        '
        Me.pnlEFanartsSetAsFanart.BackColor = System.Drawing.Color.LightGray
        Me.pnlEFanartsSetAsFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlEFanartsSetAsFanart.Controls.Add(Me.btnMovieEFanartsSetAsFanart)
        Me.pnlEFanartsSetAsFanart.Location = New System.Drawing.Point(718, 403)
        Me.pnlEFanartsSetAsFanart.Name = "pnlEFanartsSetAsFanart"
        Me.pnlEFanartsSetAsFanart.Size = New System.Drawing.Size(109, 39)
        Me.pnlEFanartsSetAsFanart.TabIndex = 14
        '
        'btnMovieEFanartsSetAsFanart
        '
        Me.btnMovieEFanartsSetAsFanart.Enabled = False
        Me.btnMovieEFanartsSetAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieEFanartsSetAsFanart.Image = CType(resources.GetObject("btnMovieEFanartsSetAsFanart.Image"), System.Drawing.Image)
        Me.btnMovieEFanartsSetAsFanart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieEFanartsSetAsFanart.Location = New System.Drawing.Point(2, 3)
        Me.btnMovieEFanartsSetAsFanart.Name = "btnMovieEFanartsSetAsFanart"
        Me.btnMovieEFanartsSetAsFanart.Size = New System.Drawing.Size(103, 32)
        Me.btnMovieEFanartsSetAsFanart.TabIndex = 0
        Me.btnMovieEFanartsSetAsFanart.Text = "Set As Fanart"
        Me.btnMovieEFanartsSetAsFanart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieEFanartsSetAsFanart.UseVisualStyleBackColor = True
        '
        'pnlMovieEFanartsBG
        '
        Me.pnlMovieEFanartsBG.AutoScroll = True
        Me.pnlMovieEFanartsBG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMovieEFanartsBG.Location = New System.Drawing.Point(5, 8)
        Me.pnlMovieEFanartsBG.Name = "pnlMovieEFanartsBG"
        Me.pnlMovieEFanartsBG.Size = New System.Drawing.Size(165, 408)
        Me.pnlMovieEFanartsBG.TabIndex = 15
        '
        'lblMovieEFanartsSize
        '
        Me.lblMovieEFanartsSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovieEFanartsSize.Location = New System.Drawing.Point(178, 10)
        Me.lblMovieEFanartsSize.Name = "lblMovieEFanartsSize"
        Me.lblMovieEFanartsSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMovieEFanartsSize.TabIndex = 16
        Me.lblMovieEFanartsSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMovieEFanartsSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMovieEFanartsSize.Visible = False
        '
        'tpFanart
        '
        Me.tpFanart.Controls.Add(Me.btnSetMovieFanartDL)
        Me.tpFanart.Controls.Add(Me.btnRemoveMovieFanart)
        Me.tpFanart.Controls.Add(Me.lblMovieFanartSize)
        Me.tpFanart.Controls.Add(Me.btnSetMovieFanartScrape)
        Me.tpFanart.Controls.Add(Me.btnSetMovieFanartLocal)
        Me.tpFanart.Controls.Add(Me.pbMovieFanart)
        Me.tpFanart.Location = New System.Drawing.Point(4, 22)
        Me.tpFanart.Name = "tpFanart"
        Me.tpFanart.Size = New System.Drawing.Size(836, 491)
        Me.tpFanart.TabIndex = 2
        Me.tpFanart.Text = "Fanart"
        Me.tpFanart.UseVisualStyleBackColor = True
        '
        'pbMovieFanart
        '
        Me.pbMovieFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbMovieFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMovieFanart.Location = New System.Drawing.Point(6, 6)
        Me.pbMovieFanart.Name = "pbMovieFanart"
        Me.pbMovieFanart.Size = New System.Drawing.Size(724, 440)
        Me.pbMovieFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMovieFanart.TabIndex = 1
        Me.pbMovieFanart.TabStop = False
        '
        'btnSetMovieFanartLocal
        '
        Me.btnSetMovieFanartLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieFanartLocal.Image = CType(resources.GetObject("btnSetMovieFanartLocal.Image"), System.Drawing.Image)
        Me.btnSetMovieFanartLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieFanartLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetMovieFanartLocal.Name = "btnSetMovieFanartLocal"
        Me.btnSetMovieFanartLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieFanartLocal.TabIndex = 1
        Me.btnSetMovieFanartLocal.Text = "Change Fanart (Local Browse)"
        Me.btnSetMovieFanartLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieFanartLocal.UseVisualStyleBackColor = True
        '
        'btnSetMovieFanartScrape
        '
        Me.btnSetMovieFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieFanartScrape.Image = CType(resources.GetObject("btnSetMovieFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetMovieFanartScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieFanartScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetMovieFanartScrape.Name = "btnSetMovieFanartScrape"
        Me.btnSetMovieFanartScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieFanartScrape.TabIndex = 2
        Me.btnSetMovieFanartScrape.Text = "Change Fanart (Scrape)"
        Me.btnSetMovieFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieFanartScrape.UseVisualStyleBackColor = True
        '
        'lblMovieFanartSize
        '
        Me.lblMovieFanartSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovieFanartSize.Location = New System.Drawing.Point(8, 8)
        Me.lblMovieFanartSize.Name = "lblMovieFanartSize"
        Me.lblMovieFanartSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMovieFanartSize.TabIndex = 0
        Me.lblMovieFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMovieFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMovieFanartSize.Visible = False
        '
        'btnRemoveMovieFanart
        '
        Me.btnRemoveMovieFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveMovieFanart.Image = CType(resources.GetObject("btnRemoveMovieFanart.Image"), System.Drawing.Image)
        Me.btnRemoveMovieFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveMovieFanart.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveMovieFanart.Name = "btnRemoveMovieFanart"
        Me.btnRemoveMovieFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveMovieFanart.TabIndex = 4
        Me.btnRemoveMovieFanart.Text = "Remove Fanart"
        Me.btnRemoveMovieFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveMovieFanart.UseVisualStyleBackColor = True
        '
        'btnSetMovieFanartDL
        '
        Me.btnSetMovieFanartDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieFanartDL.Image = CType(resources.GetObject("btnSetMovieFanartDL.Image"), System.Drawing.Image)
        Me.btnSetMovieFanartDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieFanartDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetMovieFanartDL.Name = "btnSetMovieFanartDL"
        Me.btnSetMovieFanartDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieFanartDL.TabIndex = 3
        Me.btnSetMovieFanartDL.Text = "Change Fanart (Download)"
        Me.btnSetMovieFanartDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieFanartDL.UseVisualStyleBackColor = True
        '
        'tpDiscArt
        '
        Me.tpDiscArt.Controls.Add(Me.btnSetMovieDiscArtDL)
        Me.tpDiscArt.Controls.Add(Me.btnRemoveMovieDiscArt)
        Me.tpDiscArt.Controls.Add(Me.lblMovieDiscArtSize)
        Me.tpDiscArt.Controls.Add(Me.btnSetMovieDiscArtScrape)
        Me.tpDiscArt.Controls.Add(Me.btnSetMovieDiscArtLocal)
        Me.tpDiscArt.Controls.Add(Me.pbMovieDiscArt)
        Me.tpDiscArt.Location = New System.Drawing.Point(4, 22)
        Me.tpDiscArt.Name = "tpDiscArt"
        Me.tpDiscArt.Size = New System.Drawing.Size(836, 491)
        Me.tpDiscArt.TabIndex = 10
        Me.tpDiscArt.Text = "DiscArt"
        Me.tpDiscArt.UseVisualStyleBackColor = True
        '
        'pbMovieDiscArt
        '
        Me.pbMovieDiscArt.BackColor = System.Drawing.Color.DimGray
        Me.pbMovieDiscArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMovieDiscArt.Location = New System.Drawing.Point(6, 6)
        Me.pbMovieDiscArt.Name = "pbMovieDiscArt"
        Me.pbMovieDiscArt.Size = New System.Drawing.Size(724, 440)
        Me.pbMovieDiscArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMovieDiscArt.TabIndex = 6
        Me.pbMovieDiscArt.TabStop = False
        '
        'btnSetMovieDiscArtLocal
        '
        Me.btnSetMovieDiscArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieDiscArtLocal.Image = CType(resources.GetObject("btnSetMovieDiscArtLocal.Image"), System.Drawing.Image)
        Me.btnSetMovieDiscArtLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieDiscArtLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetMovieDiscArtLocal.Name = "btnSetMovieDiscArtLocal"
        Me.btnSetMovieDiscArtLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieDiscArtLocal.TabIndex = 7
        Me.btnSetMovieDiscArtLocal.Text = "Change DiscArt (Local Browse)"
        Me.btnSetMovieDiscArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieDiscArtLocal.UseVisualStyleBackColor = True
        '
        'btnSetMovieDiscArtScrape
        '
        Me.btnSetMovieDiscArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieDiscArtScrape.Image = CType(resources.GetObject("btnSetMovieDiscArtScrape.Image"), System.Drawing.Image)
        Me.btnSetMovieDiscArtScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieDiscArtScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetMovieDiscArtScrape.Name = "btnSetMovieDiscArtScrape"
        Me.btnSetMovieDiscArtScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieDiscArtScrape.TabIndex = 8
        Me.btnSetMovieDiscArtScrape.Text = "Change DiscArt (Scrape)"
        Me.btnSetMovieDiscArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieDiscArtScrape.UseVisualStyleBackColor = True
        '
        'lblMovieDiscArtSize
        '
        Me.lblMovieDiscArtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovieDiscArtSize.Location = New System.Drawing.Point(8, 8)
        Me.lblMovieDiscArtSize.Name = "lblMovieDiscArtSize"
        Me.lblMovieDiscArtSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMovieDiscArtSize.TabIndex = 5
        Me.lblMovieDiscArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMovieDiscArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMovieDiscArtSize.Visible = False
        '
        'btnRemoveMovieDiscArt
        '
        Me.btnRemoveMovieDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveMovieDiscArt.Image = CType(resources.GetObject("btnRemoveMovieDiscArt.Image"), System.Drawing.Image)
        Me.btnRemoveMovieDiscArt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveMovieDiscArt.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveMovieDiscArt.Name = "btnRemoveMovieDiscArt"
        Me.btnRemoveMovieDiscArt.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveMovieDiscArt.TabIndex = 10
        Me.btnRemoveMovieDiscArt.Text = "Remove DiscArt"
        Me.btnRemoveMovieDiscArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveMovieDiscArt.UseVisualStyleBackColor = True
        '
        'btnSetMovieDiscArtDL
        '
        Me.btnSetMovieDiscArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieDiscArtDL.Image = CType(resources.GetObject("btnSetMovieDiscArtDL.Image"), System.Drawing.Image)
        Me.btnSetMovieDiscArtDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieDiscArtDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetMovieDiscArtDL.Name = "btnSetMovieDiscArtDL"
        Me.btnSetMovieDiscArtDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieDiscArtDL.TabIndex = 9
        Me.btnSetMovieDiscArtDL.Text = "Change DiscArt (Download)"
        Me.btnSetMovieDiscArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieDiscArtDL.UseVisualStyleBackColor = True
        '
        'tpClearLogo
        '
        Me.tpClearLogo.Controls.Add(Me.btnSetMovieClearLogoDL)
        Me.tpClearLogo.Controls.Add(Me.btnRemoveMovieClearLogo)
        Me.tpClearLogo.Controls.Add(Me.lblMovieClearLogoSize)
        Me.tpClearLogo.Controls.Add(Me.btnSetMovieClearLogoScrape)
        Me.tpClearLogo.Controls.Add(Me.btnSetMovieClearLogoLocal)
        Me.tpClearLogo.Controls.Add(Me.pbMovieClearLogo)
        Me.tpClearLogo.Location = New System.Drawing.Point(4, 22)
        Me.tpClearLogo.Name = "tpClearLogo"
        Me.tpClearLogo.Size = New System.Drawing.Size(836, 491)
        Me.tpClearLogo.TabIndex = 12
        Me.tpClearLogo.Text = "ClearLogo"
        Me.tpClearLogo.UseVisualStyleBackColor = True
        '
        'pbMovieClearLogo
        '
        Me.pbMovieClearLogo.BackColor = System.Drawing.Color.DimGray
        Me.pbMovieClearLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMovieClearLogo.Location = New System.Drawing.Point(6, 6)
        Me.pbMovieClearLogo.Name = "pbMovieClearLogo"
        Me.pbMovieClearLogo.Size = New System.Drawing.Size(724, 440)
        Me.pbMovieClearLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMovieClearLogo.TabIndex = 6
        Me.pbMovieClearLogo.TabStop = False
        '
        'btnSetMovieClearLogoLocal
        '
        Me.btnSetMovieClearLogoLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieClearLogoLocal.Image = CType(resources.GetObject("btnSetMovieClearLogoLocal.Image"), System.Drawing.Image)
        Me.btnSetMovieClearLogoLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieClearLogoLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetMovieClearLogoLocal.Name = "btnSetMovieClearLogoLocal"
        Me.btnSetMovieClearLogoLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieClearLogoLocal.TabIndex = 7
        Me.btnSetMovieClearLogoLocal.Text = "Change ClearLogo (Local Browse)"
        Me.btnSetMovieClearLogoLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieClearLogoLocal.UseVisualStyleBackColor = True
        '
        'btnSetMovieClearLogoScrape
        '
        Me.btnSetMovieClearLogoScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieClearLogoScrape.Image = CType(resources.GetObject("btnSetMovieClearLogoScrape.Image"), System.Drawing.Image)
        Me.btnSetMovieClearLogoScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieClearLogoScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetMovieClearLogoScrape.Name = "btnSetMovieClearLogoScrape"
        Me.btnSetMovieClearLogoScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieClearLogoScrape.TabIndex = 8
        Me.btnSetMovieClearLogoScrape.Text = "Change ClearLogo (Scrape)"
        Me.btnSetMovieClearLogoScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieClearLogoScrape.UseVisualStyleBackColor = True
        '
        'lblMovieClearLogoSize
        '
        Me.lblMovieClearLogoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovieClearLogoSize.Location = New System.Drawing.Point(8, 8)
        Me.lblMovieClearLogoSize.Name = "lblMovieClearLogoSize"
        Me.lblMovieClearLogoSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMovieClearLogoSize.TabIndex = 5
        Me.lblMovieClearLogoSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMovieClearLogoSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMovieClearLogoSize.Visible = False
        '
        'btnRemoveMovieClearLogo
        '
        Me.btnRemoveMovieClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveMovieClearLogo.Image = CType(resources.GetObject("btnRemoveMovieClearLogo.Image"), System.Drawing.Image)
        Me.btnRemoveMovieClearLogo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveMovieClearLogo.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveMovieClearLogo.Name = "btnRemoveMovieClearLogo"
        Me.btnRemoveMovieClearLogo.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveMovieClearLogo.TabIndex = 10
        Me.btnRemoveMovieClearLogo.Text = "Remove ClearLogo"
        Me.btnRemoveMovieClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveMovieClearLogo.UseVisualStyleBackColor = True
        '
        'btnSetMovieClearLogoDL
        '
        Me.btnSetMovieClearLogoDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieClearLogoDL.Image = CType(resources.GetObject("btnSetMovieClearLogoDL.Image"), System.Drawing.Image)
        Me.btnSetMovieClearLogoDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieClearLogoDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetMovieClearLogoDL.Name = "btnSetMovieClearLogoDL"
        Me.btnSetMovieClearLogoDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieClearLogoDL.TabIndex = 9
        Me.btnSetMovieClearLogoDL.Text = "Change ClearLogo (Download)"
        Me.btnSetMovieClearLogoDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieClearLogoDL.UseVisualStyleBackColor = True
        '
        'tpClearArt
        '
        Me.tpClearArt.Controls.Add(Me.btnSetMovieClearArtDL)
        Me.tpClearArt.Controls.Add(Me.btnRemoveMovieClearArt)
        Me.tpClearArt.Controls.Add(Me.lblMovieClearArtSize)
        Me.tpClearArt.Controls.Add(Me.btnSetMovieClearArtScrape)
        Me.tpClearArt.Controls.Add(Me.btnSetMovieClearArtLocal)
        Me.tpClearArt.Controls.Add(Me.pbMovieClearArt)
        Me.tpClearArt.Location = New System.Drawing.Point(4, 22)
        Me.tpClearArt.Name = "tpClearArt"
        Me.tpClearArt.Size = New System.Drawing.Size(836, 491)
        Me.tpClearArt.TabIndex = 11
        Me.tpClearArt.Text = "ClearArt"
        Me.tpClearArt.UseVisualStyleBackColor = True
        '
        'pbMovieClearArt
        '
        Me.pbMovieClearArt.BackColor = System.Drawing.Color.DimGray
        Me.pbMovieClearArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMovieClearArt.Location = New System.Drawing.Point(6, 6)
        Me.pbMovieClearArt.Name = "pbMovieClearArt"
        Me.pbMovieClearArt.Size = New System.Drawing.Size(724, 440)
        Me.pbMovieClearArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMovieClearArt.TabIndex = 6
        Me.pbMovieClearArt.TabStop = False
        '
        'btnSetMovieClearArtLocal
        '
        Me.btnSetMovieClearArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieClearArtLocal.Image = CType(resources.GetObject("btnSetMovieClearArtLocal.Image"), System.Drawing.Image)
        Me.btnSetMovieClearArtLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieClearArtLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetMovieClearArtLocal.Name = "btnSetMovieClearArtLocal"
        Me.btnSetMovieClearArtLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieClearArtLocal.TabIndex = 7
        Me.btnSetMovieClearArtLocal.Text = "Change ClearArt (Local Browse)"
        Me.btnSetMovieClearArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieClearArtLocal.UseVisualStyleBackColor = True
        '
        'btnSetMovieClearArtScrape
        '
        Me.btnSetMovieClearArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieClearArtScrape.Image = CType(resources.GetObject("btnSetMovieClearArtScrape.Image"), System.Drawing.Image)
        Me.btnSetMovieClearArtScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieClearArtScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetMovieClearArtScrape.Name = "btnSetMovieClearArtScrape"
        Me.btnSetMovieClearArtScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieClearArtScrape.TabIndex = 8
        Me.btnSetMovieClearArtScrape.Text = "Change ClearArt (Scrape)"
        Me.btnSetMovieClearArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieClearArtScrape.UseVisualStyleBackColor = True
        '
        'lblMovieClearArtSize
        '
        Me.lblMovieClearArtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovieClearArtSize.Location = New System.Drawing.Point(8, 8)
        Me.lblMovieClearArtSize.Name = "lblMovieClearArtSize"
        Me.lblMovieClearArtSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMovieClearArtSize.TabIndex = 5
        Me.lblMovieClearArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMovieClearArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMovieClearArtSize.Visible = False
        '
        'btnRemoveMovieClearArt
        '
        Me.btnRemoveMovieClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveMovieClearArt.Image = CType(resources.GetObject("btnRemoveMovieClearArt.Image"), System.Drawing.Image)
        Me.btnRemoveMovieClearArt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveMovieClearArt.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveMovieClearArt.Name = "btnRemoveMovieClearArt"
        Me.btnRemoveMovieClearArt.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveMovieClearArt.TabIndex = 10
        Me.btnRemoveMovieClearArt.Text = "Remove ClearArt"
        Me.btnRemoveMovieClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveMovieClearArt.UseVisualStyleBackColor = True
        '
        'btnSetMovieClearArtDL
        '
        Me.btnSetMovieClearArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieClearArtDL.Image = CType(resources.GetObject("btnSetMovieClearArtDL.Image"), System.Drawing.Image)
        Me.btnSetMovieClearArtDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieClearArtDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetMovieClearArtDL.Name = "btnSetMovieClearArtDL"
        Me.btnSetMovieClearArtDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieClearArtDL.TabIndex = 9
        Me.btnSetMovieClearArtDL.Text = "Change ClearArt (Download)"
        Me.btnSetMovieClearArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieClearArtDL.UseVisualStyleBackColor = True
        '
        'tpLandscape
        '
        Me.tpLandscape.Controls.Add(Me.btnSetMovieLandscapeDL)
        Me.tpLandscape.Controls.Add(Me.btnRemoveMovieLandscape)
        Me.tpLandscape.Controls.Add(Me.lblMovieLandscapeSize)
        Me.tpLandscape.Controls.Add(Me.btnSetMovieLandscapeScrape)
        Me.tpLandscape.Controls.Add(Me.btnSetMovieLandscapeLocal)
        Me.tpLandscape.Controls.Add(Me.pbMovieLandscape)
        Me.tpLandscape.Location = New System.Drawing.Point(4, 22)
        Me.tpLandscape.Name = "tpLandscape"
        Me.tpLandscape.Size = New System.Drawing.Size(836, 491)
        Me.tpLandscape.TabIndex = 9
        Me.tpLandscape.Text = "Landscape"
        Me.tpLandscape.UseVisualStyleBackColor = True
        '
        'pbMovieLandscape
        '
        Me.pbMovieLandscape.BackColor = System.Drawing.Color.DimGray
        Me.pbMovieLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMovieLandscape.Location = New System.Drawing.Point(6, 6)
        Me.pbMovieLandscape.Name = "pbMovieLandscape"
        Me.pbMovieLandscape.Size = New System.Drawing.Size(724, 440)
        Me.pbMovieLandscape.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMovieLandscape.TabIndex = 6
        Me.pbMovieLandscape.TabStop = False
        '
        'btnSetMovieLandscapeLocal
        '
        Me.btnSetMovieLandscapeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieLandscapeLocal.Image = CType(resources.GetObject("btnSetMovieLandscapeLocal.Image"), System.Drawing.Image)
        Me.btnSetMovieLandscapeLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieLandscapeLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetMovieLandscapeLocal.Name = "btnSetMovieLandscapeLocal"
        Me.btnSetMovieLandscapeLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieLandscapeLocal.TabIndex = 7
        Me.btnSetMovieLandscapeLocal.Text = "Change Landscape (Local Browse)"
        Me.btnSetMovieLandscapeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieLandscapeLocal.UseVisualStyleBackColor = True
        '
        'btnSetMovieLandscapeScrape
        '
        Me.btnSetMovieLandscapeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieLandscapeScrape.Image = CType(resources.GetObject("btnSetMovieLandscapeScrape.Image"), System.Drawing.Image)
        Me.btnSetMovieLandscapeScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieLandscapeScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetMovieLandscapeScrape.Name = "btnSetMovieLandscapeScrape"
        Me.btnSetMovieLandscapeScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieLandscapeScrape.TabIndex = 8
        Me.btnSetMovieLandscapeScrape.Text = "Change Landscape (Scrape)"
        Me.btnSetMovieLandscapeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieLandscapeScrape.UseVisualStyleBackColor = True
        '
        'lblMovieLandscapeSize
        '
        Me.lblMovieLandscapeSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovieLandscapeSize.Location = New System.Drawing.Point(8, 8)
        Me.lblMovieLandscapeSize.Name = "lblMovieLandscapeSize"
        Me.lblMovieLandscapeSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMovieLandscapeSize.TabIndex = 5
        Me.lblMovieLandscapeSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMovieLandscapeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMovieLandscapeSize.Visible = False
        '
        'btnRemoveMovieLandscape
        '
        Me.btnRemoveMovieLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveMovieLandscape.Image = CType(resources.GetObject("btnRemoveMovieLandscape.Image"), System.Drawing.Image)
        Me.btnRemoveMovieLandscape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveMovieLandscape.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveMovieLandscape.Name = "btnRemoveMovieLandscape"
        Me.btnRemoveMovieLandscape.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveMovieLandscape.TabIndex = 10
        Me.btnRemoveMovieLandscape.Text = "Remove Landscape"
        Me.btnRemoveMovieLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveMovieLandscape.UseVisualStyleBackColor = True
        '
        'btnSetMovieLandscapeDL
        '
        Me.btnSetMovieLandscapeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieLandscapeDL.Image = CType(resources.GetObject("btnSetMovieLandscapeDL.Image"), System.Drawing.Image)
        Me.btnSetMovieLandscapeDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieLandscapeDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetMovieLandscapeDL.Name = "btnSetMovieLandscapeDL"
        Me.btnSetMovieLandscapeDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieLandscapeDL.TabIndex = 9
        Me.btnSetMovieLandscapeDL.Text = "Change Landscape (Download)"
        Me.btnSetMovieLandscapeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieLandscapeDL.UseVisualStyleBackColor = True
        '
        'tpBanner
        '
        Me.tpBanner.Controls.Add(Me.btnSetMovieBannerDL)
        Me.tpBanner.Controls.Add(Me.btnRemoveMovieBanner)
        Me.tpBanner.Controls.Add(Me.lblMovieBannerSize)
        Me.tpBanner.Controls.Add(Me.btnSetMovieBannerScrape)
        Me.tpBanner.Controls.Add(Me.btnSetMovieBannerLocal)
        Me.tpBanner.Controls.Add(Me.pbMovieBanner)
        Me.tpBanner.Location = New System.Drawing.Point(4, 22)
        Me.tpBanner.Name = "tpBanner"
        Me.tpBanner.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBanner.Size = New System.Drawing.Size(836, 491)
        Me.tpBanner.TabIndex = 8
        Me.tpBanner.Text = "Banner"
        Me.tpBanner.UseVisualStyleBackColor = True
        '
        'pbMovieBanner
        '
        Me.pbMovieBanner.BackColor = System.Drawing.Color.DimGray
        Me.pbMovieBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMovieBanner.Location = New System.Drawing.Point(6, 6)
        Me.pbMovieBanner.Name = "pbMovieBanner"
        Me.pbMovieBanner.Size = New System.Drawing.Size(724, 440)
        Me.pbMovieBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMovieBanner.TabIndex = 6
        Me.pbMovieBanner.TabStop = False
        '
        'btnSetMovieBannerLocal
        '
        Me.btnSetMovieBannerLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieBannerLocal.Image = CType(resources.GetObject("btnSetMovieBannerLocal.Image"), System.Drawing.Image)
        Me.btnSetMovieBannerLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieBannerLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetMovieBannerLocal.Name = "btnSetMovieBannerLocal"
        Me.btnSetMovieBannerLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieBannerLocal.TabIndex = 7
        Me.btnSetMovieBannerLocal.Text = "Change Banner (Local Browse)"
        Me.btnSetMovieBannerLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieBannerLocal.UseVisualStyleBackColor = True
        '
        'btnSetMovieBannerScrape
        '
        Me.btnSetMovieBannerScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieBannerScrape.Image = CType(resources.GetObject("btnSetMovieBannerScrape.Image"), System.Drawing.Image)
        Me.btnSetMovieBannerScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieBannerScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetMovieBannerScrape.Name = "btnSetMovieBannerScrape"
        Me.btnSetMovieBannerScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieBannerScrape.TabIndex = 8
        Me.btnSetMovieBannerScrape.Text = "Change Banner (Scrape)"
        Me.btnSetMovieBannerScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieBannerScrape.UseVisualStyleBackColor = True
        '
        'lblMovieBannerSize
        '
        Me.lblMovieBannerSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMovieBannerSize.Location = New System.Drawing.Point(8, 8)
        Me.lblMovieBannerSize.Name = "lblMovieBannerSize"
        Me.lblMovieBannerSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMovieBannerSize.TabIndex = 5
        Me.lblMovieBannerSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMovieBannerSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMovieBannerSize.Visible = False
        '
        'btnRemoveMovieBanner
        '
        Me.btnRemoveMovieBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveMovieBanner.Image = CType(resources.GetObject("btnRemoveMovieBanner.Image"), System.Drawing.Image)
        Me.btnRemoveMovieBanner.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveMovieBanner.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveMovieBanner.Name = "btnRemoveMovieBanner"
        Me.btnRemoveMovieBanner.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveMovieBanner.TabIndex = 10
        Me.btnRemoveMovieBanner.Text = "Remove Banner"
        Me.btnRemoveMovieBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveMovieBanner.UseVisualStyleBackColor = True
        '
        'btnSetMovieBannerDL
        '
        Me.btnSetMovieBannerDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMovieBannerDL.Image = CType(resources.GetObject("btnSetMovieBannerDL.Image"), System.Drawing.Image)
        Me.btnSetMovieBannerDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMovieBannerDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetMovieBannerDL.Name = "btnSetMovieBannerDL"
        Me.btnSetMovieBannerDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMovieBannerDL.TabIndex = 9
        Me.btnSetMovieBannerDL.Text = "Change Banner (Download)"
        Me.btnSetMovieBannerDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMovieBannerDL.UseVisualStyleBackColor = True
        '
        'tpPoster
        '
        Me.tpPoster.Controls.Add(Me.btnSetMoviePosterDL)
        Me.tpPoster.Controls.Add(Me.btnRemoveMoviePoster)
        Me.tpPoster.Controls.Add(Me.lblMoviePosterSize)
        Me.tpPoster.Controls.Add(Me.btnSetMoviePosterScrape)
        Me.tpPoster.Controls.Add(Me.btnSetMoviePosterLocal)
        Me.tpPoster.Controls.Add(Me.pbMoviePoster)
        Me.tpPoster.Location = New System.Drawing.Point(4, 22)
        Me.tpPoster.Name = "tpPoster"
        Me.tpPoster.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPoster.Size = New System.Drawing.Size(836, 491)
        Me.tpPoster.TabIndex = 1
        Me.tpPoster.Text = "Poster"
        Me.tpPoster.UseVisualStyleBackColor = True
        '
        'pbMoviePoster
        '
        Me.pbMoviePoster.BackColor = System.Drawing.Color.DimGray
        Me.pbMoviePoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMoviePoster.Location = New System.Drawing.Point(6, 6)
        Me.pbMoviePoster.Name = "pbMoviePoster"
        Me.pbMoviePoster.Size = New System.Drawing.Size(724, 440)
        Me.pbMoviePoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbMoviePoster.TabIndex = 0
        Me.pbMoviePoster.TabStop = False
        '
        'btnSetMoviePosterLocal
        '
        Me.btnSetMoviePosterLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMoviePosterLocal.Image = CType(resources.GetObject("btnSetMoviePosterLocal.Image"), System.Drawing.Image)
        Me.btnSetMoviePosterLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMoviePosterLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetMoviePosterLocal.Name = "btnSetMoviePosterLocal"
        Me.btnSetMoviePosterLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMoviePosterLocal.TabIndex = 1
        Me.btnSetMoviePosterLocal.Text = "Change Poster (Local Browse)"
        Me.btnSetMoviePosterLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMoviePosterLocal.UseVisualStyleBackColor = True
        '
        'btnSetMoviePosterScrape
        '
        Me.btnSetMoviePosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMoviePosterScrape.Image = CType(resources.GetObject("btnSetMoviePosterScrape.Image"), System.Drawing.Image)
        Me.btnSetMoviePosterScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMoviePosterScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetMoviePosterScrape.Name = "btnSetMoviePosterScrape"
        Me.btnSetMoviePosterScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMoviePosterScrape.TabIndex = 2
        Me.btnSetMoviePosterScrape.Text = "Change Poster (Scrape)"
        Me.btnSetMoviePosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMoviePosterScrape.UseVisualStyleBackColor = True
        '
        'lblMoviePosterSize
        '
        Me.lblMoviePosterSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMoviePosterSize.Location = New System.Drawing.Point(8, 8)
        Me.lblMoviePosterSize.Name = "lblMoviePosterSize"
        Me.lblMoviePosterSize.Size = New System.Drawing.Size(105, 23)
        Me.lblMoviePosterSize.TabIndex = 0
        Me.lblMoviePosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblMoviePosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMoviePosterSize.Visible = False
        '
        'btnRemoveMoviePoster
        '
        Me.btnRemoveMoviePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveMoviePoster.Image = CType(resources.GetObject("btnRemoveMoviePoster.Image"), System.Drawing.Image)
        Me.btnRemoveMoviePoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveMoviePoster.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveMoviePoster.Name = "btnRemoveMoviePoster"
        Me.btnRemoveMoviePoster.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveMoviePoster.TabIndex = 4
        Me.btnRemoveMoviePoster.Text = "Remove Poster"
        Me.btnRemoveMoviePoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveMoviePoster.UseVisualStyleBackColor = True
        '
        'btnSetMoviePosterDL
        '
        Me.btnSetMoviePosterDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetMoviePosterDL.Image = CType(resources.GetObject("btnSetMoviePosterDL.Image"), System.Drawing.Image)
        Me.btnSetMoviePosterDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetMoviePosterDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetMoviePosterDL.Name = "btnSetMoviePosterDL"
        Me.btnSetMoviePosterDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetMoviePosterDL.TabIndex = 3
        Me.btnSetMoviePosterDL.Text = "Change Poster (Download)"
        Me.btnSetMoviePosterDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetMoviePosterDL.UseVisualStyleBackColor = True
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.lblTheme)
        Me.tpDetails.Controls.Add(Me.btnPlayTheme)
        Me.tpDetails.Controls.Add(Me.btnDLTheme)
        Me.tpDetails.Controls.Add(Me.txtOriginalTitle)
        Me.tpDetails.Controls.Add(Me.txtCountry)
        Me.tpDetails.Controls.Add(Me.txtFileSource)
        Me.tpDetails.Controls.Add(Me.txtSortTitle)
        Me.tpDetails.Controls.Add(Me.txtStudio)
        Me.tpDetails.Controls.Add(Me.txtTrailer)
        Me.tpDetails.Controls.Add(Me.txtReleaseDate)
        Me.tpDetails.Controls.Add(Me.txtCredits)
        Me.tpDetails.Controls.Add(Me.txtCerts)
        Me.tpDetails.Controls.Add(Me.txtRuntime)
        Me.tpDetails.Controls.Add(Me.txtMPAADesc)
        Me.tpDetails.Controls.Add(Me.lblOriginalTitle)
        Me.tpDetails.Controls.Add(Me.lblCountry)
        Me.tpDetails.Controls.Add(Me.lblFileSource)
        Me.tpDetails.Controls.Add(Me.btnActorDown)
        Me.tpDetails.Controls.Add(Me.btnActorUp)
        Me.tpDetails.Controls.Add(Me.lblSortTilte)
        Me.tpDetails.Controls.Add(Me.lblLocalTrailer)
        Me.tpDetails.Controls.Add(Me.btnPlayTrailer)
        Me.tpDetails.Controls.Add(Me.btnDLTrailer)
        Me.tpDetails.Controls.Add(Me.clbGenre)
        Me.tpDetails.Controls.Add(Me.btnStudio)
        Me.tpDetails.Controls.Add(Me.lblStudio)
        Me.tpDetails.Controls.Add(Me.lblTrailerURL)
        Me.tpDetails.Controls.Add(Me.lblReleaseDate)
        Me.tpDetails.Controls.Add(Me.lblCredits)
        Me.tpDetails.Controls.Add(Me.lblCerts)
        Me.tpDetails.Controls.Add(Me.lblRuntime)
        Me.tpDetails.Controls.Add(Me.lblMPAADesc)
        Me.tpDetails.Controls.Add(Me.btnEditActor)
        Me.tpDetails.Controls.Add(Me.btnAddActor)
        Me.tpDetails.Controls.Add(Me.btnManual)
        Me.tpDetails.Controls.Add(Me.btnRemove)
        Me.tpDetails.Controls.Add(Me.lblActors)
        Me.tpDetails.Controls.Add(Me.lvActors)
        Me.tpDetails.Controls.Add(Me.txtDirector)
        Me.tpDetails.Controls.Add(Me.txtTop250)
        Me.tpDetails.Controls.Add(Me.txtPlot)
        Me.tpDetails.Controls.Add(Me.txtOutline)
        Me.tpDetails.Controls.Add(Me.txtTagline)
        Me.tpDetails.Controls.Add(Me.txtVotes)
        Me.tpDetails.Controls.Add(Me.txtTitle)
        Me.tpDetails.Controls.Add(Me.lbMPAA)
        Me.tpDetails.Controls.Add(Me.lblGenre)
        Me.tpDetails.Controls.Add(Me.lblMPAA)
        Me.tpDetails.Controls.Add(Me.lblDirector)
        Me.tpDetails.Controls.Add(Me.lblTop250)
        Me.tpDetails.Controls.Add(Me.lblPlot)
        Me.tpDetails.Controls.Add(Me.lblOutline)
        Me.tpDetails.Controls.Add(Me.lblTagline)
        Me.tpDetails.Controls.Add(Me.pbStar5)
        Me.tpDetails.Controls.Add(Me.pbStar4)
        Me.tpDetails.Controls.Add(Me.pbStar3)
        Me.tpDetails.Controls.Add(Me.pbStar2)
        Me.tpDetails.Controls.Add(Me.pbStar1)
        Me.tpDetails.Controls.Add(Me.lblVotes)
        Me.tpDetails.Controls.Add(Me.lblRating)
        Me.tpDetails.Controls.Add(Me.mtxtYear)
        Me.tpDetails.Controls.Add(Me.lblYear)
        Me.tpDetails.Controls.Add(Me.lblTitle)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(836, 491)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(7, 22)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtTitle.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(5, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(32, 13)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Title:"
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Enabled = False
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblYear.Location = New System.Drawing.Point(5, 167)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(32, 13)
        Me.lblYear.TabIndex = 8
        Me.lblYear.Text = "Year:"
        Me.lblYear.Visible = False
        '
        'mtxtYear
        '
        Me.mtxtYear.BackColor = System.Drawing.SystemColors.Window
        Me.mtxtYear.Enabled = False
        Me.mtxtYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.mtxtYear.Location = New System.Drawing.Point(7, 182)
        Me.mtxtYear.Mask = "####"
        Me.mtxtYear.Name = "mtxtYear"
        Me.mtxtYear.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mtxtYear.Size = New System.Drawing.Size(50, 22)
        Me.mtxtYear.TabIndex = 9
        Me.mtxtYear.Visible = False
        '
        'lblRating
        '
        Me.lblRating.AutoSize = True
        Me.lblRating.Enabled = False
        Me.lblRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRating.Location = New System.Drawing.Point(77, 167)
        Me.lblRating.Name = "lblRating"
        Me.lblRating.Size = New System.Drawing.Size(44, 13)
        Me.lblRating.TabIndex = 10
        Me.lblRating.Text = "Rating:"
        Me.lblRating.Visible = False
        '
        'lblVotes
        '
        Me.lblVotes.AutoSize = True
        Me.lblVotes.Enabled = False
        Me.lblVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVotes.Location = New System.Drawing.Point(78, 249)
        Me.lblVotes.Name = "lblVotes"
        Me.lblVotes.Size = New System.Drawing.Size(38, 13)
        Me.lblVotes.TabIndex = 17
        Me.lblVotes.Text = "Votes:"
        Me.lblVotes.Visible = False
        '
        'txtVotes
        '
        Me.txtVotes.BackColor = System.Drawing.SystemColors.Window
        Me.txtVotes.Enabled = False
        Me.txtVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVotes.Location = New System.Drawing.Point(81, 264)
        Me.txtVotes.Name = "txtVotes"
        Me.txtVotes.Size = New System.Drawing.Size(66, 22)
        Me.txtVotes.TabIndex = 18
        Me.txtVotes.Visible = False
        '
        'pbStar1
        '
        Me.pbStar1.Enabled = False
        Me.pbStar1.Location = New System.Drawing.Point(79, 180)
        Me.pbStar1.Name = "pbStar1"
        Me.pbStar1.Size = New System.Drawing.Size(24, 24)
        Me.pbStar1.TabIndex = 63
        Me.pbStar1.TabStop = False
        Me.pbStar1.Visible = False
        '
        'pbStar2
        '
        Me.pbStar2.Enabled = False
        Me.pbStar2.Location = New System.Drawing.Point(103, 180)
        Me.pbStar2.Name = "pbStar2"
        Me.pbStar2.Size = New System.Drawing.Size(24, 24)
        Me.pbStar2.TabIndex = 64
        Me.pbStar2.TabStop = False
        Me.pbStar2.Visible = False
        '
        'pbStar3
        '
        Me.pbStar3.Enabled = False
        Me.pbStar3.Location = New System.Drawing.Point(127, 180)
        Me.pbStar3.Name = "pbStar3"
        Me.pbStar3.Size = New System.Drawing.Size(24, 24)
        Me.pbStar3.TabIndex = 65
        Me.pbStar3.TabStop = False
        Me.pbStar3.Visible = False
        '
        'pbStar4
        '
        Me.pbStar4.Enabled = False
        Me.pbStar4.Location = New System.Drawing.Point(151, 180)
        Me.pbStar4.Name = "pbStar4"
        Me.pbStar4.Size = New System.Drawing.Size(24, 24)
        Me.pbStar4.TabIndex = 66
        Me.pbStar4.TabStop = False
        Me.pbStar4.Visible = False
        '
        'pbStar5
        '
        Me.pbStar5.Enabled = False
        Me.pbStar5.Location = New System.Drawing.Point(175, 180)
        Me.pbStar5.Name = "pbStar5"
        Me.pbStar5.Size = New System.Drawing.Size(24, 24)
        Me.pbStar5.TabIndex = 67
        Me.pbStar5.TabStop = False
        Me.pbStar5.Visible = False
        '
        'txtTagline
        '
        Me.txtTagline.BackColor = System.Drawing.SystemColors.Window
        Me.txtTagline.Enabled = False
        Me.txtTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTagline.Location = New System.Drawing.Point(7, 142)
        Me.txtTagline.Name = "txtTagline"
        Me.txtTagline.Size = New System.Drawing.Size(192, 22)
        Me.txtTagline.TabIndex = 7
        Me.txtTagline.Visible = False
        '
        'lblTagline
        '
        Me.lblTagline.AutoSize = True
        Me.lblTagline.Enabled = False
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTagline.Location = New System.Drawing.Point(5, 127)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(47, 13)
        Me.lblTagline.TabIndex = 6
        Me.lblTagline.Text = "Tagline:"
        Me.lblTagline.Visible = False
        '
        'txtOutline
        '
        Me.txtOutline.AcceptsReturn = True
        Me.txtOutline.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutline.Enabled = False
        Me.txtOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtOutline.Location = New System.Drawing.Point(217, 22)
        Me.txtOutline.Multiline = True
        Me.txtOutline.Name = "txtOutline"
        Me.txtOutline.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutline.Size = New System.Drawing.Size(220, 112)
        Me.txtOutline.TabIndex = 26
        Me.txtOutline.Visible = False
        '
        'lblOutline
        '
        Me.lblOutline.AutoSize = True
        Me.lblOutline.Enabled = False
        Me.lblOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOutline.Location = New System.Drawing.Point(215, 7)
        Me.lblOutline.Name = "lblOutline"
        Me.lblOutline.Size = New System.Drawing.Size(48, 13)
        Me.lblOutline.TabIndex = 25
        Me.lblOutline.Text = "Outline:"
        Me.lblOutline.Visible = False
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlot.Enabled = False
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(443, 22)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(385, 112)
        Me.txtPlot.TabIndex = 28
        Me.txtPlot.Visible = False
        '
        'lblPlot
        '
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Enabled = False
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(441, 7)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(31, 13)
        Me.lblPlot.TabIndex = 27
        Me.lblPlot.Text = "Plot:"
        Me.lblPlot.Visible = False
        '
        'lblTop250
        '
        Me.lblTop250.AutoSize = True
        Me.lblTop250.Enabled = False
        Me.lblTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTop250.Location = New System.Drawing.Point(153, 249)
        Me.lblTop250.Name = "lblTop250"
        Me.lblTop250.Size = New System.Drawing.Size(50, 13)
        Me.lblTop250.TabIndex = 19
        Me.lblTop250.Text = "Top 250:"
        Me.lblTop250.Visible = False
        '
        'txtTop250
        '
        Me.txtTop250.BackColor = System.Drawing.SystemColors.Window
        Me.txtTop250.Enabled = False
        Me.txtTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTop250.Location = New System.Drawing.Point(156, 264)
        Me.txtTop250.Name = "txtTop250"
        Me.txtTop250.Size = New System.Drawing.Size(43, 22)
        Me.txtTop250.TabIndex = 20
        Me.txtTop250.Visible = False
        '
        'txtDirector
        '
        Me.txtDirector.BackColor = System.Drawing.SystemColors.Window
        Me.txtDirector.Enabled = False
        Me.txtDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtDirector.Location = New System.Drawing.Point(7, 304)
        Me.txtDirector.Name = "txtDirector"
        Me.txtDirector.Size = New System.Drawing.Size(192, 22)
        Me.txtDirector.TabIndex = 22
        Me.txtDirector.Visible = False
        '
        'lblDirector
        '
        Me.lblDirector.AutoSize = True
        Me.lblDirector.Enabled = False
        Me.lblDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDirector.Location = New System.Drawing.Point(5, 289)
        Me.lblDirector.Name = "lblDirector"
        Me.lblDirector.Size = New System.Drawing.Size(51, 13)
        Me.lblDirector.TabIndex = 21
        Me.lblDirector.Text = "Director:"
        Me.lblDirector.Visible = False
        '
        'lblMPAA
        '
        Me.lblMPAA.AutoSize = True
        Me.lblMPAA.Enabled = False
        Me.lblMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMPAA.Location = New System.Drawing.Point(633, 141)
        Me.lblMPAA.Name = "lblMPAA"
        Me.lblMPAA.Size = New System.Drawing.Size(80, 13)
        Me.lblMPAA.TabIndex = 36
        Me.lblMPAA.Text = "MPAA Rating:"
        Me.lblMPAA.Visible = False
        '
        'lblGenre
        '
        Me.lblGenre.AutoSize = True
        Me.lblGenre.Enabled = False
        Me.lblGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGenre.Location = New System.Drawing.Point(5, 330)
        Me.lblGenre.Name = "lblGenre"
        Me.lblGenre.Size = New System.Drawing.Size(41, 13)
        Me.lblGenre.TabIndex = 23
        Me.lblGenre.Text = "Genre:"
        Me.lblGenre.Visible = False
        '
        'lbMPAA
        '
        Me.lbMPAA.BackColor = System.Drawing.SystemColors.Window
        Me.lbMPAA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbMPAA.Enabled = False
        Me.lbMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbMPAA.FormattingEnabled = True
        Me.lbMPAA.Location = New System.Drawing.Point(635, 155)
        Me.lbMPAA.Name = "lbMPAA"
        Me.lbMPAA.Size = New System.Drawing.Size(193, 80)
        Me.lbMPAA.TabIndex = 37
        Me.lbMPAA.Visible = False
        '
        'lvActors
        '
        Me.lvActors.BackColor = System.Drawing.SystemColors.Window
        Me.lvActors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colRole, Me.colThumb})
        Me.lvActors.Enabled = False
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.Location = New System.Drawing.Point(217, 155)
        Me.lvActors.Name = "lvActors"
        Me.lvActors.Size = New System.Drawing.Size(408, 147)
        Me.lvActors.TabIndex = 30
        Me.lvActors.UseCompatibleStateImageBehavior = False
        Me.lvActors.View = System.Windows.Forms.View.Details
        Me.lvActors.Visible = False
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 110
        '
        'colRole
        '
        Me.colRole.Text = "Role"
        Me.colRole.Width = 100
        '
        'colThumb
        '
        Me.colThumb.Text = "Thumb"
        Me.colThumb.Width = 174
        '
        'lblActors
        '
        Me.lblActors.AutoSize = True
        Me.lblActors.Enabled = False
        Me.lblActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblActors.Location = New System.Drawing.Point(215, 141)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(43, 13)
        Me.lblActors.TabIndex = 29
        Me.lblActors.Text = "Actors:"
        Me.lblActors.Visible = False
        '
        'btnRemove
        '
        Me.btnRemove.Enabled = False
        Me.btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), System.Drawing.Image)
        Me.btnRemove.Location = New System.Drawing.Point(602, 303)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnRemove.TabIndex = 35
        Me.btnRemove.UseVisualStyleBackColor = True
        Me.btnRemove.Visible = False
        '
        'btnManual
        '
        Me.btnManual.Enabled = False
        Me.btnManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnManual.Location = New System.Drawing.Point(738, 446)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(92, 23)
        Me.btnManual.TabIndex = 54
        Me.btnManual.Text = "Manual Edit"
        Me.btnManual.UseVisualStyleBackColor = True
        Me.btnManual.Visible = False
        '
        'btnAddActor
        '
        Me.btnAddActor.Enabled = False
        Me.btnAddActor.Image = CType(resources.GetObject("btnAddActor.Image"), System.Drawing.Image)
        Me.btnAddActor.Location = New System.Drawing.Point(217, 303)
        Me.btnAddActor.Name = "btnAddActor"
        Me.btnAddActor.Size = New System.Drawing.Size(23, 23)
        Me.btnAddActor.TabIndex = 31
        Me.btnAddActor.UseVisualStyleBackColor = True
        Me.btnAddActor.Visible = False
        '
        'btnEditActor
        '
        Me.btnEditActor.Enabled = False
        Me.btnEditActor.Image = CType(resources.GetObject("btnEditActor.Image"), System.Drawing.Image)
        Me.btnEditActor.Location = New System.Drawing.Point(246, 303)
        Me.btnEditActor.Name = "btnEditActor"
        Me.btnEditActor.Size = New System.Drawing.Size(23, 23)
        Me.btnEditActor.TabIndex = 32
        Me.btnEditActor.UseVisualStyleBackColor = True
        Me.btnEditActor.Visible = False
        '
        'txtMPAADesc
        '
        Me.txtMPAADesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtMPAADesc.Enabled = False
        Me.txtMPAADesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtMPAADesc.Location = New System.Drawing.Point(635, 260)
        Me.txtMPAADesc.Multiline = True
        Me.txtMPAADesc.Name = "txtMPAADesc"
        Me.txtMPAADesc.Size = New System.Drawing.Size(193, 64)
        Me.txtMPAADesc.TabIndex = 39
        Me.txtMPAADesc.Visible = False
        '
        'lblMPAADesc
        '
        Me.lblMPAADesc.AutoSize = True
        Me.lblMPAADesc.Enabled = False
        Me.lblMPAADesc.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMPAADesc.Location = New System.Drawing.Point(633, 245)
        Me.lblMPAADesc.Name = "lblMPAADesc"
        Me.lblMPAADesc.Size = New System.Drawing.Size(142, 13)
        Me.lblMPAADesc.TabIndex = 38
        Me.lblMPAADesc.Text = "MPAA Rating Description:"
        Me.lblMPAADesc.Visible = False
        '
        'txtRuntime
        '
        Me.txtRuntime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRuntime.Enabled = False
        Me.txtRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtRuntime.Location = New System.Drawing.Point(7, 264)
        Me.txtRuntime.Name = "txtRuntime"
        Me.txtRuntime.Size = New System.Drawing.Size(66, 22)
        Me.txtRuntime.TabIndex = 16
        Me.txtRuntime.Visible = False
        '
        'lblRuntime
        '
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Enabled = False
        Me.lblRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRuntime.Location = New System.Drawing.Point(5, 249)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(54, 13)
        Me.lblRuntime.TabIndex = 15
        Me.lblRuntime.Text = "Runtime:"
        Me.lblRuntime.Visible = False
        '
        'txtCerts
        '
        Me.txtCerts.BackColor = System.Drawing.SystemColors.Window
        Me.txtCerts.Enabled = False
        Me.txtCerts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtCerts.Location = New System.Drawing.Point(217, 385)
        Me.txtCerts.Name = "txtCerts"
        Me.txtCerts.Size = New System.Drawing.Size(408, 22)
        Me.txtCerts.TabIndex = 46
        Me.txtCerts.Visible = False
        '
        'lblCerts
        '
        Me.lblCerts.AutoSize = True
        Me.lblCerts.Enabled = False
        Me.lblCerts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCerts.Location = New System.Drawing.Point(215, 370)
        Me.lblCerts.Name = "lblCerts"
        Me.lblCerts.Size = New System.Drawing.Size(86, 13)
        Me.lblCerts.TabIndex = 45
        Me.lblCerts.Text = "Certification(s):"
        Me.lblCerts.Visible = False
        '
        'txtCredits
        '
        Me.txtCredits.BackColor = System.Drawing.SystemColors.Window
        Me.txtCredits.Enabled = False
        Me.txtCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtCredits.Location = New System.Drawing.Point(217, 345)
        Me.txtCredits.Name = "txtCredits"
        Me.txtCredits.Size = New System.Drawing.Size(408, 22)
        Me.txtCredits.TabIndex = 41
        Me.txtCredits.Visible = False
        '
        'lblCredits
        '
        Me.lblCredits.AutoSize = True
        Me.lblCredits.Enabled = False
        Me.lblCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCredits.Location = New System.Drawing.Point(215, 330)
        Me.lblCredits.Name = "lblCredits"
        Me.lblCredits.Size = New System.Drawing.Size(46, 13)
        Me.lblCredits.TabIndex = 40
        Me.lblCredits.Text = "Credits:"
        Me.lblCredits.Visible = False
        '
        'lblReleaseDate
        '
        Me.lblReleaseDate.AutoSize = True
        Me.lblReleaseDate.Enabled = False
        Me.lblReleaseDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblReleaseDate.Location = New System.Drawing.Point(100, 209)
        Me.lblReleaseDate.Name = "lblReleaseDate"
        Me.lblReleaseDate.Size = New System.Drawing.Size(76, 13)
        Me.lblReleaseDate.TabIndex = 13
        Me.lblReleaseDate.Text = "Release Date:"
        Me.lblReleaseDate.Visible = False
        '
        'txtReleaseDate
        '
        Me.txtReleaseDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReleaseDate.Enabled = False
        Me.txtReleaseDate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtReleaseDate.Location = New System.Drawing.Point(103, 224)
        Me.txtReleaseDate.Name = "txtReleaseDate"
        Me.txtReleaseDate.Size = New System.Drawing.Size(96, 22)
        Me.txtReleaseDate.TabIndex = 14
        Me.txtReleaseDate.Visible = False
        '
        'txtTrailer
        '
        Me.txtTrailer.BackColor = System.Drawing.SystemColors.Window
        Me.txtTrailer.Enabled = False
        Me.txtTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTrailer.Location = New System.Drawing.Point(217, 425)
        Me.txtTrailer.Name = "txtTrailer"
        Me.txtTrailer.Size = New System.Drawing.Size(360, 22)
        Me.txtTrailer.TabIndex = 50
        Me.txtTrailer.Visible = False
        '
        'lblTrailerURL
        '
        Me.lblTrailerURL.AutoSize = True
        Me.lblTrailerURL.Enabled = False
        Me.lblTrailerURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTrailerURL.Location = New System.Drawing.Point(215, 410)
        Me.lblTrailerURL.Name = "lblTrailerURL"
        Me.lblTrailerURL.Size = New System.Drawing.Size(65, 13)
        Me.lblTrailerURL.TabIndex = 49
        Me.lblTrailerURL.Text = "Trailer URL:"
        Me.lblTrailerURL.Visible = False
        '
        'txtStudio
        '
        Me.txtStudio.BackColor = System.Drawing.SystemColors.Window
        Me.txtStudio.Enabled = False
        Me.txtStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtStudio.Location = New System.Drawing.Point(635, 345)
        Me.txtStudio.Name = "txtStudio"
        Me.txtStudio.Size = New System.Drawing.Size(167, 22)
        Me.txtStudio.TabIndex = 43
        Me.txtStudio.Visible = False
        '
        'lblStudio
        '
        Me.lblStudio.AutoSize = True
        Me.lblStudio.Enabled = False
        Me.lblStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStudio.Location = New System.Drawing.Point(633, 330)
        Me.lblStudio.Name = "lblStudio"
        Me.lblStudio.Size = New System.Drawing.Size(44, 13)
        Me.lblStudio.TabIndex = 42
        Me.lblStudio.Text = "Studio:"
        Me.lblStudio.Visible = False
        '
        'btnStudio
        '
        Me.btnStudio.Enabled = False
        Me.btnStudio.Image = CType(resources.GetObject("btnStudio.Image"), System.Drawing.Image)
        Me.btnStudio.Location = New System.Drawing.Point(805, 343)
        Me.btnStudio.Name = "btnStudio"
        Me.btnStudio.Size = New System.Drawing.Size(23, 23)
        Me.btnStudio.TabIndex = 44
        Me.btnStudio.UseVisualStyleBackColor = True
        Me.btnStudio.Visible = False
        '
        'clbGenre
        '
        Me.clbGenre.BackColor = System.Drawing.SystemColors.Window
        Me.clbGenre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbGenre.CheckOnClick = True
        Me.clbGenre.Enabled = False
        Me.clbGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.clbGenre.FormattingEnabled = True
        Me.clbGenre.IntegralHeight = False
        Me.clbGenre.Location = New System.Drawing.Point(8, 345)
        Me.clbGenre.Name = "clbGenre"
        Me.clbGenre.Size = New System.Drawing.Size(192, 100)
        Me.clbGenre.Sorted = True
        Me.clbGenre.TabIndex = 24
        Me.clbGenre.Visible = False
        '
        'btnDLTrailer
        '
        Me.btnDLTrailer.Enabled = False
        Me.btnDLTrailer.Image = CType(resources.GetObject("btnDLTrailer.Image"), System.Drawing.Image)
        Me.btnDLTrailer.Location = New System.Drawing.Point(602, 423)
        Me.btnDLTrailer.Name = "btnDLTrailer"
        Me.btnDLTrailer.Size = New System.Drawing.Size(23, 23)
        Me.btnDLTrailer.TabIndex = 53
        Me.btnDLTrailer.UseVisualStyleBackColor = True
        Me.btnDLTrailer.Visible = False
        '
        'btnPlayTrailer
        '
        Me.btnPlayTrailer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPlayTrailer.Enabled = False
        Me.btnPlayTrailer.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnPlayTrailer.Location = New System.Drawing.Point(579, 423)
        Me.btnPlayTrailer.Name = "btnPlayTrailer"
        Me.btnPlayTrailer.Size = New System.Drawing.Size(23, 23)
        Me.btnPlayTrailer.TabIndex = 52
        Me.btnPlayTrailer.UseVisualStyleBackColor = True
        Me.btnPlayTrailer.Visible = False
        '
        'lblLocalTrailer
        '
        Me.lblLocalTrailer.AutoSize = True
        Me.lblLocalTrailer.Enabled = False
        Me.lblLocalTrailer.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocalTrailer.ForeColor = System.Drawing.Color.Green
        Me.lblLocalTrailer.Location = New System.Drawing.Point(487, 414)
        Me.lblLocalTrailer.Name = "lblLocalTrailer"
        Me.lblLocalTrailer.Size = New System.Drawing.Size(90, 9)
        Me.lblLocalTrailer.TabIndex = 51
        Me.lblLocalTrailer.Text = "Local Trailer Found"
        Me.lblLocalTrailer.Visible = False
        '
        'txtSortTitle
        '
        Me.txtSortTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtSortTitle.Enabled = False
        Me.txtSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSortTitle.Location = New System.Drawing.Point(7, 102)
        Me.txtSortTitle.Name = "txtSortTitle"
        Me.txtSortTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtSortTitle.TabIndex = 5
        Me.txtSortTitle.Visible = False
        '
        'lblSortTilte
        '
        Me.lblSortTilte.AutoSize = True
        Me.lblSortTilte.Enabled = False
        Me.lblSortTilte.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSortTilte.Location = New System.Drawing.Point(5, 87)
        Me.lblSortTilte.Name = "lblSortTilte"
        Me.lblSortTilte.Size = New System.Drawing.Size(56, 13)
        Me.lblSortTilte.TabIndex = 4
        Me.lblSortTilte.Text = "Sort Title:"
        Me.lblSortTilte.Visible = False
        '
        'btnActorUp
        '
        Me.btnActorUp.Enabled = False
        Me.btnActorUp.Image = CType(resources.GetObject("btnActorUp.Image"), System.Drawing.Image)
        Me.btnActorUp.Location = New System.Drawing.Point(406, 304)
        Me.btnActorUp.Name = "btnActorUp"
        Me.btnActorUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorUp.TabIndex = 33
        Me.btnActorUp.UseVisualStyleBackColor = True
        Me.btnActorUp.Visible = False
        '
        'btnActorDown
        '
        Me.btnActorDown.Enabled = False
        Me.btnActorDown.Image = CType(resources.GetObject("btnActorDown.Image"), System.Drawing.Image)
        Me.btnActorDown.Location = New System.Drawing.Point(430, 304)
        Me.btnActorDown.Name = "btnActorDown"
        Me.btnActorDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorDown.TabIndex = 34
        Me.btnActorDown.UseVisualStyleBackColor = True
        Me.btnActorDown.Visible = False
        '
        'lblFileSource
        '
        Me.lblFileSource.AutoSize = True
        Me.lblFileSource.Enabled = False
        Me.lblFileSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFileSource.Location = New System.Drawing.Point(633, 370)
        Me.lblFileSource.Name = "lblFileSource"
        Me.lblFileSource.Size = New System.Drawing.Size(78, 13)
        Me.lblFileSource.TabIndex = 47
        Me.lblFileSource.Text = "Video Source:"
        Me.lblFileSource.Visible = False
        '
        'txtFileSource
        '
        Me.txtFileSource.BackColor = System.Drawing.SystemColors.Window
        Me.txtFileSource.Enabled = False
        Me.txtFileSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtFileSource.Location = New System.Drawing.Point(636, 385)
        Me.txtFileSource.Name = "txtFileSource"
        Me.txtFileSource.Size = New System.Drawing.Size(167, 22)
        Me.txtFileSource.TabIndex = 48
        Me.txtFileSource.Visible = False
        '
        'lblCountry
        '
        Me.lblCountry.AutoSize = True
        Me.lblCountry.Enabled = False
        Me.lblCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCountry.Location = New System.Drawing.Point(5, 209)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(52, 13)
        Me.lblCountry.TabIndex = 11
        Me.lblCountry.Text = "Country:"
        Me.lblCountry.Visible = False
        '
        'txtCountry
        '
        Me.txtCountry.BackColor = System.Drawing.SystemColors.Window
        Me.txtCountry.Enabled = False
        Me.txtCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtCountry.Location = New System.Drawing.Point(8, 224)
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(89, 22)
        Me.txtCountry.TabIndex = 12
        Me.txtCountry.Visible = False
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.AutoSize = True
        Me.lblOriginalTitle.Enabled = False
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOriginalTitle.Location = New System.Drawing.Point(5, 47)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(76, 13)
        Me.lblOriginalTitle.TabIndex = 2
        Me.lblOriginalTitle.Text = "Original Title:"
        Me.lblOriginalTitle.Visible = False
        '
        'txtOriginalTitle
        '
        Me.txtOriginalTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtOriginalTitle.Enabled = False
        Me.txtOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtOriginalTitle.Location = New System.Drawing.Point(7, 63)
        Me.txtOriginalTitle.Name = "txtOriginalTitle"
        Me.txtOriginalTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtOriginalTitle.TabIndex = 3
        Me.txtOriginalTitle.Visible = False
        '
        'btnDLTheme
        '
        Me.btnDLTheme.Enabled = False
        Me.btnDLTheme.Image = CType(resources.GetObject("btnDLTheme.Image"), System.Drawing.Image)
        Me.btnDLTheme.Location = New System.Drawing.Point(602, 462)
        Me.btnDLTheme.Name = "btnDLTheme"
        Me.btnDLTheme.Size = New System.Drawing.Size(23, 23)
        Me.btnDLTheme.TabIndex = 68
        Me.btnDLTheme.UseVisualStyleBackColor = True
        Me.btnDLTheme.Visible = False
        '
        'btnPlayTheme
        '
        Me.btnPlayTheme.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPlayTheme.Enabled = False
        Me.btnPlayTheme.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnPlayTheme.Location = New System.Drawing.Point(579, 462)
        Me.btnPlayTheme.Name = "btnPlayTheme"
        Me.btnPlayTheme.Size = New System.Drawing.Size(23, 23)
        Me.btnPlayTheme.TabIndex = 69
        Me.btnPlayTheme.UseVisualStyleBackColor = True
        Me.btnPlayTheme.Visible = False
        '
        'lblTheme
        '
        Me.lblTheme.AutoSize = True
        Me.lblTheme.Enabled = False
        Me.lblTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTheme.Location = New System.Drawing.Point(528, 467)
        Me.lblTheme.Name = "lblTheme"
        Me.lblTheme.Size = New System.Drawing.Size(45, 13)
        Me.lblTheme.TabIndex = 70
        Me.lblTheme.Text = "Theme:"
        Me.lblTheme.Visible = False
        '
        'tcEditMovie
        '
        Me.tcEditMovie.Controls.Add(Me.tpDetails)
        Me.tcEditMovie.Controls.Add(Me.tpPoster)
        Me.tcEditMovie.Controls.Add(Me.tpBanner)
        Me.tcEditMovie.Controls.Add(Me.tpLandscape)
        Me.tcEditMovie.Controls.Add(Me.tpClearArt)
        Me.tcEditMovie.Controls.Add(Me.tpClearLogo)
        Me.tcEditMovie.Controls.Add(Me.tpDiscArt)
        Me.tcEditMovie.Controls.Add(Me.tpFanart)
        Me.tcEditMovie.Controls.Add(Me.tpEFanarts)
        Me.tcEditMovie.Controls.Add(Me.tpEThumbs)
        Me.tcEditMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcEditMovie.Location = New System.Drawing.Point(4, 70)
        Me.tcEditMovie.Name = "tcEditMovie"
        Me.tcEditMovie.SelectedIndex = 0
        Me.tcEditMovie.Size = New System.Drawing.Size(844, 517)
        Me.tcEditMovie.TabIndex = 3
        '
        'dlgEditMovieSet
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(854, 621)
        Me.Controls.Add(Me.chkWatched)
        Me.Controls.Add(Me.btnChangeMovie)
        Me.Controls.Add(Me.btnRescrape)
        Me.Controls.Add(Me.chkMark)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.tcEditMovie)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditMovieSet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Movie"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpEThumbs.ResumeLayout(False)
        CType(Me.pbMovieEThumbs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEThumbsSetAsFanart.ResumeLayout(False)
        Me.pnlMovieETQueue.ResumeLayout(False)
        Me.tpEFanarts.ResumeLayout(False)
        CType(Me.pbMovieEFanarts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMovieEFanartsQueue.ResumeLayout(False)
        Me.pnlEFanartsSetAsFanart.ResumeLayout(False)
        Me.tpFanart.ResumeLayout(False)
        CType(Me.pbMovieFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDiscArt.ResumeLayout(False)
        CType(Me.pbMovieDiscArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpClearLogo.ResumeLayout(False)
        CType(Me.pbMovieClearLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpClearArt.ResumeLayout(False)
        CType(Me.pbMovieClearArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpLandscape.ResumeLayout(False)
        CType(Me.pbMovieLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBanner.ResumeLayout(False)
        CType(Me.pbMovieBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPoster.ResumeLayout(False)
        CType(Me.pbMoviePoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcEditMovie.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkMark As System.Windows.Forms.CheckBox
    Friend WithEvents btnRescrape As System.Windows.Forms.Button
    Friend WithEvents btnChangeMovie As System.Windows.Forms.Button
    Friend WithEvents tmrDelay As System.Windows.Forms.Timer
    Friend WithEvents chkWatched As System.Windows.Forms.CheckBox
    Friend WithEvents tpEThumbs As System.Windows.Forms.TabPage
    Friend WithEvents lblMovieEThumbsSize As System.Windows.Forms.Label
    Friend WithEvents pnlMovieEThumbsBG As System.Windows.Forms.Panel
    Friend WithEvents pnlMovieETQueue As System.Windows.Forms.Panel
    Friend WithEvents lbMovieEThumbsQueue As System.Windows.Forms.Label
    Friend WithEvents btnMovieEThumbsTransfer As System.Windows.Forms.Button
    Friend WithEvents pnlEThumbsSetAsFanart As System.Windows.Forms.Panel
    Friend WithEvents btnMovieEThumbsSetAsFanart As System.Windows.Forms.Button
    Friend WithEvents btnMovieEThumbsRefresh As System.Windows.Forms.Button
    Friend WithEvents btnMovieEThumbsRemove As System.Windows.Forms.Button
    Friend WithEvents btnMovieEThumbsDown As System.Windows.Forms.Button
    Friend WithEvents btnMovieEThumbsUp As System.Windows.Forms.Button
    Friend WithEvents pbMovieEThumbs As System.Windows.Forms.PictureBox
    Friend WithEvents tpEFanarts As System.Windows.Forms.TabPage
    Friend WithEvents lblMovieEFanartsSize As System.Windows.Forms.Label
    Friend WithEvents pnlMovieEFanartsBG As System.Windows.Forms.Panel
    Friend WithEvents pnlEFanartsSetAsFanart As System.Windows.Forms.Panel
    Friend WithEvents btnMovieEFanartsSetAsFanart As System.Windows.Forms.Button
    Friend WithEvents btnMovieEFanartsRefresh As System.Windows.Forms.Button
    Friend WithEvents btnMovieEFanartsRemove As System.Windows.Forms.Button
    Friend WithEvents pnlMovieEFanartsQueue As System.Windows.Forms.Panel
    Friend WithEvents lbMovieEFanartsQueue As System.Windows.Forms.Label
    Friend WithEvents btnMovieEFanartsTransfer As System.Windows.Forms.Button
    Friend WithEvents pbMovieEFanarts As System.Windows.Forms.PictureBox
    Friend WithEvents tpFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetMovieFanartDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMovieFanart As System.Windows.Forms.Button
    Friend WithEvents lblMovieFanartSize As System.Windows.Forms.Label
    Friend WithEvents btnSetMovieFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetMovieFanartLocal As System.Windows.Forms.Button
    Friend WithEvents pbMovieFanart As System.Windows.Forms.PictureBox
    Friend WithEvents tpDiscArt As System.Windows.Forms.TabPage
    Friend WithEvents btnSetMovieDiscArtDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMovieDiscArt As System.Windows.Forms.Button
    Friend WithEvents lblMovieDiscArtSize As System.Windows.Forms.Label
    Friend WithEvents btnSetMovieDiscArtScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetMovieDiscArtLocal As System.Windows.Forms.Button
    Friend WithEvents pbMovieDiscArt As System.Windows.Forms.PictureBox
    Friend WithEvents tpClearLogo As System.Windows.Forms.TabPage
    Friend WithEvents btnSetMovieClearLogoDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMovieClearLogo As System.Windows.Forms.Button
    Friend WithEvents lblMovieClearLogoSize As System.Windows.Forms.Label
    Friend WithEvents btnSetMovieClearLogoScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetMovieClearLogoLocal As System.Windows.Forms.Button
    Friend WithEvents pbMovieClearLogo As System.Windows.Forms.PictureBox
    Friend WithEvents tpClearArt As System.Windows.Forms.TabPage
    Friend WithEvents btnSetMovieClearArtDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMovieClearArt As System.Windows.Forms.Button
    Friend WithEvents lblMovieClearArtSize As System.Windows.Forms.Label
    Friend WithEvents btnSetMovieClearArtScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetMovieClearArtLocal As System.Windows.Forms.Button
    Friend WithEvents pbMovieClearArt As System.Windows.Forms.PictureBox
    Friend WithEvents tpLandscape As System.Windows.Forms.TabPage
    Friend WithEvents btnSetMovieLandscapeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMovieLandscape As System.Windows.Forms.Button
    Friend WithEvents lblMovieLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents btnSetMovieLandscapeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetMovieLandscapeLocal As System.Windows.Forms.Button
    Friend WithEvents pbMovieLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents tpBanner As System.Windows.Forms.TabPage
    Friend WithEvents btnSetMovieBannerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMovieBanner As System.Windows.Forms.Button
    Friend WithEvents lblMovieBannerSize As System.Windows.Forms.Label
    Friend WithEvents btnSetMovieBannerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetMovieBannerLocal As System.Windows.Forms.Button
    Friend WithEvents pbMovieBanner As System.Windows.Forms.PictureBox
    Friend WithEvents tpPoster As System.Windows.Forms.TabPage
    Friend WithEvents btnSetMoviePosterDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveMoviePoster As System.Windows.Forms.Button
    Friend WithEvents lblMoviePosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetMoviePosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetMoviePosterLocal As System.Windows.Forms.Button
    Friend WithEvents pbMoviePoster As System.Windows.Forms.PictureBox
    Friend WithEvents tpDetails As System.Windows.Forms.TabPage
    Friend WithEvents lblTheme As System.Windows.Forms.Label
    Friend WithEvents btnPlayTheme As System.Windows.Forms.Button
    Friend WithEvents btnDLTheme As System.Windows.Forms.Button
    Friend WithEvents txtOriginalTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents txtFileSource As System.Windows.Forms.TextBox
    Friend WithEvents txtSortTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtStudio As System.Windows.Forms.TextBox
    Friend WithEvents txtTrailer As System.Windows.Forms.TextBox
    Friend WithEvents txtReleaseDate As System.Windows.Forms.TextBox
    Friend WithEvents txtCredits As System.Windows.Forms.TextBox
    Friend WithEvents txtCerts As System.Windows.Forms.TextBox
    Friend WithEvents txtRuntime As System.Windows.Forms.TextBox
    Friend WithEvents txtMPAADesc As System.Windows.Forms.TextBox
    Friend WithEvents lblOriginalTitle As System.Windows.Forms.Label
    Friend WithEvents lblCountry As System.Windows.Forms.Label
    Friend WithEvents lblFileSource As System.Windows.Forms.Label
    Friend WithEvents btnActorDown As System.Windows.Forms.Button
    Friend WithEvents btnActorUp As System.Windows.Forms.Button
    Friend WithEvents lblSortTilte As System.Windows.Forms.Label
    Friend WithEvents lblLocalTrailer As System.Windows.Forms.Label
    Friend WithEvents btnPlayTrailer As System.Windows.Forms.Button
    Friend WithEvents btnDLTrailer As System.Windows.Forms.Button
    Friend WithEvents clbGenre As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnStudio As System.Windows.Forms.Button
    Friend WithEvents lblStudio As System.Windows.Forms.Label
    Friend WithEvents lblTrailerURL As System.Windows.Forms.Label
    Friend WithEvents lblReleaseDate As System.Windows.Forms.Label
    Friend WithEvents lblCredits As System.Windows.Forms.Label
    Friend WithEvents lblCerts As System.Windows.Forms.Label
    Friend WithEvents lblRuntime As System.Windows.Forms.Label
    Friend WithEvents lblMPAADesc As System.Windows.Forms.Label
    Friend WithEvents btnEditActor As System.Windows.Forms.Button
    Friend WithEvents btnAddActor As System.Windows.Forms.Button
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents lblActors As System.Windows.Forms.Label
    Friend WithEvents lvActors As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRole As System.Windows.Forms.ColumnHeader
    Friend WithEvents colThumb As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtDirector As System.Windows.Forms.TextBox
    Friend WithEvents txtTop250 As System.Windows.Forms.TextBox
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents txtOutline As System.Windows.Forms.TextBox
    Friend WithEvents txtTagline As System.Windows.Forms.TextBox
    Friend WithEvents txtVotes As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents lbMPAA As System.Windows.Forms.ListBox
    Friend WithEvents lblGenre As System.Windows.Forms.Label
    Friend WithEvents lblMPAA As System.Windows.Forms.Label
    Friend WithEvents lblDirector As System.Windows.Forms.Label
    Friend WithEvents lblTop250 As System.Windows.Forms.Label
    Friend WithEvents lblPlot As System.Windows.Forms.Label
    Friend WithEvents lblOutline As System.Windows.Forms.Label
    Friend WithEvents lblTagline As System.Windows.Forms.Label
    Friend WithEvents pbStar5 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar4 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar3 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar2 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblVotes As System.Windows.Forms.Label
    Friend WithEvents lblRating As System.Windows.Forms.Label
    Friend WithEvents mtxtYear As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents tcEditMovie As System.Windows.Forms.TabControl

End Class
