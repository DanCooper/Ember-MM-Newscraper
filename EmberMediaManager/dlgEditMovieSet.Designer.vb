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
        Me.btnRescrape = New System.Windows.Forms.Button()
        Me.tmrDelay = New System.Windows.Forms.Timer(Me.components)
        Me.tpFanart = New System.Windows.Forms.TabPage()
        Me.btnSetMovieFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveMovieFanart = New System.Windows.Forms.Button()
        Me.lblMovieFanartSize = New System.Windows.Forms.Label()
        Me.btnSetMovieFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetMovieFanartLocal = New System.Windows.Forms.Button()
        Me.pbMovieFanart = New System.Windows.Forms.PictureBox()
        Me.tpDiscArt = New System.Windows.Forms.TabPage()
        Me.btnSetMovieDiscArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveMovieDiscArt = New System.Windows.Forms.Button()
        Me.lblMovieDiscArtSize = New System.Windows.Forms.Label()
        Me.btnSetMovieDiscArtScrape = New System.Windows.Forms.Button()
        Me.btnSetMovieDiscArtLocal = New System.Windows.Forms.Button()
        Me.pbMovieDiscArt = New System.Windows.Forms.PictureBox()
        Me.tpClearLogo = New System.Windows.Forms.TabPage()
        Me.btnSetMovieClearLogoDL = New System.Windows.Forms.Button()
        Me.btnRemoveMovieClearLogo = New System.Windows.Forms.Button()
        Me.lblMovieClearLogoSize = New System.Windows.Forms.Label()
        Me.btnSetMovieClearLogoScrape = New System.Windows.Forms.Button()
        Me.btnSetMovieClearLogoLocal = New System.Windows.Forms.Button()
        Me.pbMovieClearLogo = New System.Windows.Forms.PictureBox()
        Me.tpClearArt = New System.Windows.Forms.TabPage()
        Me.btnSetMovieClearArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveMovieClearArt = New System.Windows.Forms.Button()
        Me.lblMovieClearArtSize = New System.Windows.Forms.Label()
        Me.btnSetMovieClearArtScrape = New System.Windows.Forms.Button()
        Me.btnSetMovieClearArtLocal = New System.Windows.Forms.Button()
        Me.pbMovieClearArt = New System.Windows.Forms.PictureBox()
        Me.tpLandscape = New System.Windows.Forms.TabPage()
        Me.btnSetMovieLandscapeDL = New System.Windows.Forms.Button()
        Me.btnRemoveMovieLandscape = New System.Windows.Forms.Button()
        Me.lblMovieLandscapeSize = New System.Windows.Forms.Label()
        Me.btnSetMovieLandscapeScrape = New System.Windows.Forms.Button()
        Me.btnSetMovieLandscapeLocal = New System.Windows.Forms.Button()
        Me.pbMovieLandscape = New System.Windows.Forms.PictureBox()
        Me.tpBanner = New System.Windows.Forms.TabPage()
        Me.btnSetMovieBannerDL = New System.Windows.Forms.Button()
        Me.btnRemoveMovieBanner = New System.Windows.Forms.Button()
        Me.lblMovieBannerSize = New System.Windows.Forms.Label()
        Me.btnSetMovieBannerScrape = New System.Windows.Forms.Button()
        Me.btnSetMovieBannerLocal = New System.Windows.Forms.Button()
        Me.pbMovieBanner = New System.Windows.Forms.PictureBox()
        Me.tpPoster = New System.Windows.Forms.TabPage()
        Me.btnSetMoviePosterDL = New System.Windows.Forms.Button()
        Me.btnRemoveMoviePoster = New System.Windows.Forms.Button()
        Me.lblMoviePosterSize = New System.Windows.Forms.Label()
        Me.btnSetMoviePosterScrape = New System.Windows.Forms.Button()
        Me.btnSetMoviePosterLocal = New System.Windows.Forms.Button()
        Me.pbMoviePoster = New System.Windows.Forms.PictureBox()
        Me.tpMovies = New System.Windows.Forms.TabPage()
        Me.lvMoviesToRemove = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnMovieReAdd = New System.Windows.Forms.Button()
        Me.lblMoviesToRemove = New System.Windows.Forms.Label()
        Me.btnLoadMoviesFromDB = New System.Windows.Forms.Button()
        Me.btnMovieAdd = New System.Windows.Forms.Button()
        Me.lblMoviesInDB = New System.Windows.Forms.Label()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.prbCompile = New System.Windows.Forms.ProgressBar()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnMovieDown = New System.Windows.Forms.Button()
        Me.btnMovieUp = New System.Windows.Forms.Button()
        Me.btnMovieRemove = New System.Windows.Forms.Button()
        Me.lblMoviesInMovieset = New System.Windows.Forms.Label()
        Me.lvMoviesInSet = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colOrdering = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvMoviesInDB = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tcEditMovie = New System.Windows.Forms.TabControl()
        Me.tpDetails = New System.Windows.Forms.TabPage()
        Me.btnGetTMDBColID = New System.Windows.Forms.Button()
        Me.txtCollectionID = New System.Windows.Forms.TextBox()
        Me.lblCollectionID = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.pnlSaving = New System.Windows.Forms.Panel()
        Me.lblSaving = New System.Windows.Forms.Label()
        Me.prbSaving = New System.Windows.Forms.ProgressBar()
        Me.chkMark = New System.Windows.Forms.CheckBox()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tpMovies.SuspendLayout()
        Me.pnlCancel.SuspendLayout()
        Me.tcEditMovie.SuspendLayout()
        Me.tpDetails.SuspendLayout()
        Me.pnlSaving.SuspendLayout()
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
        'tmrDelay
        '
        Me.tmrDelay.Interval = 250
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
        'tpMovies
        '
        Me.tpMovies.Controls.Add(Me.lvMoviesToRemove)
        Me.tpMovies.Controls.Add(Me.btnMovieReAdd)
        Me.tpMovies.Controls.Add(Me.lblMoviesToRemove)
        Me.tpMovies.Controls.Add(Me.btnLoadMoviesFromDB)
        Me.tpMovies.Controls.Add(Me.btnMovieAdd)
        Me.tpMovies.Controls.Add(Me.lblMoviesInDB)
        Me.tpMovies.Controls.Add(Me.pnlCancel)
        Me.tpMovies.Controls.Add(Me.btnMovieDown)
        Me.tpMovies.Controls.Add(Me.btnMovieUp)
        Me.tpMovies.Controls.Add(Me.btnMovieRemove)
        Me.tpMovies.Controls.Add(Me.lblMoviesInMovieset)
        Me.tpMovies.Controls.Add(Me.lvMoviesInSet)
        Me.tpMovies.Controls.Add(Me.lvMoviesInDB)
        Me.tpMovies.Location = New System.Drawing.Point(4, 22)
        Me.tpMovies.Name = "tpMovies"
        Me.tpMovies.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMovies.Size = New System.Drawing.Size(836, 491)
        Me.tpMovies.TabIndex = 0
        Me.tpMovies.Text = "Movies"
        Me.tpMovies.UseVisualStyleBackColor = True
        '
        'lvMoviesToRemove
        '
        Me.lvMoviesToRemove.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvMoviesToRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvMoviesToRemove.FullRowSelect = True
        Me.lvMoviesToRemove.Location = New System.Drawing.Point(4, 331)
        Me.lvMoviesToRemove.Name = "lvMoviesToRemove"
        Me.lvMoviesToRemove.Size = New System.Drawing.Size(252, 114)
        Me.lvMoviesToRemove.TabIndex = 49
        Me.lvMoviesToRemove.UseCompatibleStateImageBehavior = False
        Me.lvMoviesToRemove.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        Me.ColumnHeader1.Width = 25
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Ordering"
        Me.ColumnHeader2.Width = 25
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Movie"
        Me.ColumnHeader3.Width = 198
        '
        'btnMovieReAdd
        '
        Me.btnMovieReAdd.Enabled = False
        Me.btnMovieReAdd.Image = CType(resources.GetObject("btnMovieReAdd.Image"), System.Drawing.Image)
        Me.btnMovieReAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieReAdd.Location = New System.Drawing.Point(226, 450)
        Me.btnMovieReAdd.Name = "btnMovieReAdd"
        Me.btnMovieReAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieReAdd.TabIndex = 47
        Me.btnMovieReAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieReAdd.UseVisualStyleBackColor = True
        '
        'lblMoviesToRemove
        '
        Me.lblMoviesToRemove.AutoSize = True
        Me.lblMoviesToRemove.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMoviesToRemove.Location = New System.Drawing.Point(4, 315)
        Me.lblMoviesToRemove.Name = "lblMoviesToRemove"
        Me.lblMoviesToRemove.Size = New System.Drawing.Size(183, 13)
        Me.lblMoviesToRemove.TabIndex = 46
        Me.lblMoviesToRemove.Text = "Movies to remove from Movieset:"
        '
        'btnLoadMoviesFromDB
        '
        Me.btnLoadMoviesFromDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnLoadMoviesFromDB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadMoviesFromDB.Location = New System.Drawing.Point(522, 450)
        Me.btnLoadMoviesFromDB.Name = "btnLoadMoviesFromDB"
        Me.btnLoadMoviesFromDB.Size = New System.Drawing.Size(98, 23)
        Me.btnLoadMoviesFromDB.TabIndex = 41
        Me.btnLoadMoviesFromDB.Text = "Load Movies"
        Me.btnLoadMoviesFromDB.UseVisualStyleBackColor = True
        '
        'btnMovieAdd
        '
        Me.btnMovieAdd.Enabled = False
        Me.btnMovieAdd.Image = CType(resources.GetObject("btnMovieAdd.Image"), System.Drawing.Image)
        Me.btnMovieAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMovieAdd.Location = New System.Drawing.Point(292, 135)
        Me.btnMovieAdd.Name = "btnMovieAdd"
        Me.btnMovieAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieAdd.TabIndex = 40
        Me.btnMovieAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMovieAdd.UseVisualStyleBackColor = True
        '
        'lblMoviesInDB
        '
        Me.lblMoviesInDB.AutoSize = True
        Me.lblMoviesInDB.Enabled = False
        Me.lblMoviesInDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMoviesInDB.Location = New System.Drawing.Point(343, 47)
        Me.lblMoviesInDB.Name = "lblMoviesInDB"
        Me.lblMoviesInDB.Size = New System.Drawing.Size(112, 13)
        Me.lblMoviesInDB.TabIndex = 39
        Me.lblMoviesInDB.Text = "Movies in Database:"
        Me.lblMoviesInDB.Visible = False
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.White
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.prbCompile)
        Me.pnlCancel.Controls.Add(Me.lblCompiling)
        Me.pnlCancel.Controls.Add(Me.lblFile)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Location = New System.Drawing.Point(217, 207)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(403, 76)
        Me.pnlCancel.TabIndex = 37
        Me.pnlCancel.Visible = False
        '
        'prbCompile
        '
        Me.prbCompile.Location = New System.Drawing.Point(8, 36)
        Me.prbCompile.Name = "prbCompile"
        Me.prbCompile.Size = New System.Drawing.Size(388, 18)
        Me.prbCompile.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prbCompile.TabIndex = 3
        '
        'lblCompiling
        '
        Me.lblCompiling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCompiling.Location = New System.Drawing.Point(3, 11)
        Me.lblCompiling.Name = "lblCompiling"
        Me.lblCompiling.Size = New System.Drawing.Size(203, 20)
        Me.lblCompiling.TabIndex = 0
        Me.lblCompiling.Text = "Loading Movies and Sets..."
        Me.lblCompiling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCompiling.Visible = False
        '
        'lblFile
        '
        Me.lblFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFile.Location = New System.Drawing.Point(3, 57)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(395, 13)
        Me.lblFile.TabIndex = 4
        Me.lblFile.Text = "File ..."
        '
        'lblCanceling
        '
        Me.lblCanceling.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanceling.Location = New System.Drawing.Point(110, 12)
        Me.lblCanceling.Name = "lblCanceling"
        Me.lblCanceling.Size = New System.Drawing.Size(186, 20)
        Me.lblCanceling.TabIndex = 1
        Me.lblCanceling.Text = "Canceling Load..."
        Me.lblCanceling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCanceling.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(298, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnMovieDown
        '
        Me.btnMovieDown.Enabled = False
        Me.btnMovieDown.Image = CType(resources.GetObject("btnMovieDown.Image"), System.Drawing.Image)
        Me.btnMovieDown.Location = New System.Drawing.Point(148, 268)
        Me.btnMovieDown.Name = "btnMovieDown"
        Me.btnMovieDown.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieDown.TabIndex = 34
        Me.btnMovieDown.UseVisualStyleBackColor = True
        '
        'btnMovieUp
        '
        Me.btnMovieUp.Image = CType(resources.GetObject("btnMovieUp.Image"), System.Drawing.Image)
        Me.btnMovieUp.Location = New System.Drawing.Point(115, 268)
        Me.btnMovieUp.Name = "btnMovieUp"
        Me.btnMovieUp.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieUp.TabIndex = 33
        Me.btnMovieUp.UseVisualStyleBackColor = True
        '
        'btnMovieRemove
        '
        Me.btnMovieRemove.Enabled = False
        Me.btnMovieRemove.Image = CType(resources.GetObject("btnMovieRemove.Image"), System.Drawing.Image)
        Me.btnMovieRemove.Location = New System.Drawing.Point(226, 268)
        Me.btnMovieRemove.Name = "btnMovieRemove"
        Me.btnMovieRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieRemove.TabIndex = 35
        Me.btnMovieRemove.UseVisualStyleBackColor = True
        '
        'lblMoviesInMovieset
        '
        Me.lblMoviesInMovieset.AutoSize = True
        Me.lblMoviesInMovieset.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMoviesInMovieset.Location = New System.Drawing.Point(6, 47)
        Me.lblMoviesInMovieset.Name = "lblMoviesInMovieset"
        Me.lblMoviesInMovieset.Size = New System.Drawing.Size(112, 13)
        Me.lblMoviesInMovieset.TabIndex = 29
        Me.lblMoviesInMovieset.Text = "Movies in Movieset:"
        '
        'lvMoviesInSet
        '
        Me.lvMoviesInSet.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colOrdering, Me.colMovie})
        Me.lvMoviesInSet.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvMoviesInSet.FullRowSelect = True
        Me.lvMoviesInSet.Location = New System.Drawing.Point(6, 63)
        Me.lvMoviesInSet.Name = "lvMoviesInSet"
        Me.lvMoviesInSet.Size = New System.Drawing.Size(252, 199)
        Me.lvMoviesInSet.TabIndex = 48
        Me.lvMoviesInSet.UseCompatibleStateImageBehavior = False
        Me.lvMoviesInSet.View = System.Windows.Forms.View.Details
        '
        'colID
        '
        Me.colID.Text = "ID"
        Me.colID.Width = 25
        '
        'colOrdering
        '
        Me.colOrdering.Text = "Ordering"
        Me.colOrdering.Width = 25
        '
        'colMovie
        '
        Me.colMovie.Text = "Movie"
        Me.colMovie.Width = 198
        '
        'lvMoviesInDB
        '
        Me.lvMoviesInDB.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lvMoviesInDB.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvMoviesInDB.FullRowSelect = True
        Me.lvMoviesInDB.Location = New System.Drawing.Point(346, 63)
        Me.lvMoviesInDB.Name = "lvMoviesInDB"
        Me.lvMoviesInDB.Size = New System.Drawing.Size(484, 382)
        Me.lvMoviesInDB.TabIndex = 49
        Me.lvMoviesInDB.UseCompatibleStateImageBehavior = False
        Me.lvMoviesInDB.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "ID"
        Me.ColumnHeader4.Width = 25
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Ordering"
        Me.ColumnHeader5.Width = 25
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Movie"
        Me.ColumnHeader6.Width = 430
        '
        'tcEditMovie
        '
        Me.tcEditMovie.Controls.Add(Me.tpDetails)
        Me.tcEditMovie.Controls.Add(Me.tpMovies)
        Me.tcEditMovie.Controls.Add(Me.tpPoster)
        Me.tcEditMovie.Controls.Add(Me.tpBanner)
        Me.tcEditMovie.Controls.Add(Me.tpLandscape)
        Me.tcEditMovie.Controls.Add(Me.tpClearArt)
        Me.tcEditMovie.Controls.Add(Me.tpClearLogo)
        Me.tcEditMovie.Controls.Add(Me.tpDiscArt)
        Me.tcEditMovie.Controls.Add(Me.tpFanart)
        Me.tcEditMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tcEditMovie.Location = New System.Drawing.Point(4, 70)
        Me.tcEditMovie.Name = "tcEditMovie"
        Me.tcEditMovie.SelectedIndex = 0
        Me.tcEditMovie.Size = New System.Drawing.Size(844, 517)
        Me.tcEditMovie.TabIndex = 3
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.btnGetTMDBColID)
        Me.tpDetails.Controls.Add(Me.txtCollectionID)
        Me.tpDetails.Controls.Add(Me.lblCollectionID)
        Me.tpDetails.Controls.Add(Me.txtTitle)
        Me.tpDetails.Controls.Add(Me.lblTitle)
        Me.tpDetails.Controls.Add(Me.lblPlot)
        Me.tpDetails.Controls.Add(Me.txtPlot)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Size = New System.Drawing.Size(836, 491)
        Me.tpDetails.TabIndex = 13
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'btnGetTMDBColID
        '
        Me.btnGetTMDBColID.Location = New System.Drawing.Point(92, 61)
        Me.btnGetTMDBColID.Name = "btnGetTMDBColID"
        Me.btnGetTMDBColID.Size = New System.Drawing.Size(149, 24)
        Me.btnGetTMDBColID.TabIndex = 49
        Me.btnGetTMDBColID.Text = "Get TMDB Collection ID"
        Me.btnGetTMDBColID.UseVisualStyleBackColor = True
        '
        'txtCollectionID
        '
        Me.txtCollectionID.BackColor = System.Drawing.SystemColors.Window
        Me.txtCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtCollectionID.Location = New System.Drawing.Point(7, 63)
        Me.txtCollectionID.Name = "txtCollectionID"
        Me.txtCollectionID.Size = New System.Drawing.Size(79, 22)
        Me.txtCollectionID.TabIndex = 48
        '
        'lblCollectionID
        '
        Me.lblCollectionID.AutoSize = True
        Me.lblCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCollectionID.Location = New System.Drawing.Point(5, 47)
        Me.lblCollectionID.Name = "lblCollectionID"
        Me.lblCollectionID.Size = New System.Drawing.Size(76, 13)
        Me.lblCollectionID.TabIndex = 47
        Me.lblCollectionID.Text = "Collection ID:"
        '
        'txtTitle
        '
        Me.txtTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(7, 22)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtTitle.TabIndex = 46
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(5, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(32, 13)
        Me.lblTitle.TabIndex = 45
        Me.lblTitle.Text = "Title:"
        '
        'lblPlot
        '
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(5, 87)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(31, 13)
        Me.lblPlot.TabIndex = 29
        Me.lblPlot.Text = "Plot:"
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(7, 102)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(461, 112)
        Me.txtPlot.TabIndex = 30
        '
        'pnlSaving
        '
        Me.pnlSaving.BackColor = System.Drawing.Color.White
        Me.pnlSaving.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSaving.Controls.Add(Me.lblSaving)
        Me.pnlSaving.Controls.Add(Me.prbSaving)
        Me.pnlSaving.Location = New System.Drawing.Point(300, 314)
        Me.pnlSaving.Name = "pnlSaving"
        Me.pnlSaving.Size = New System.Drawing.Size(252, 51)
        Me.pnlSaving.TabIndex = 5
        Me.pnlSaving.Visible = False
        '
        'lblSaving
        '
        Me.lblSaving.AutoSize = True
        Me.lblSaving.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSaving.Location = New System.Drawing.Point(2, 7)
        Me.lblSaving.Name = "lblSaving"
        Me.lblSaving.Size = New System.Drawing.Size(51, 13)
        Me.lblSaving.TabIndex = 0
        Me.lblSaving.Text = "Saving..."
        '
        'prbSaving
        '
        Me.prbSaving.Location = New System.Drawing.Point(4, 26)
        Me.prbSaving.MarqueeAnimationSpeed = 25
        Me.prbSaving.Name = "prbSaving"
        Me.prbSaving.Size = New System.Drawing.Size(242, 16)
        Me.prbSaving.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbSaving.TabIndex = 1
        '
        'chkMark
        '
        Me.chkMark.AutoSize = True
        Me.chkMark.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMark.Location = New System.Drawing.Point(8, 598)
        Me.chkMark.Name = "chkMark"
        Me.chkMark.Size = New System.Drawing.Size(86, 17)
        Me.chkMark.TabIndex = 8
        Me.chkMark.Text = "Mark Movie"
        Me.chkMark.UseVisualStyleBackColor = True
        '
        'dlgEditMovieSet
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(854, 621)
        Me.Controls.Add(Me.chkMark)
        Me.Controls.Add(Me.pnlSaving)
        Me.Controls.Add(Me.btnRescrape)
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
        Me.tpMovies.ResumeLayout(False)
        Me.tpMovies.PerformLayout()
        Me.pnlCancel.ResumeLayout(False)
        Me.tcEditMovie.ResumeLayout(False)
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        Me.pnlSaving.ResumeLayout(False)
        Me.pnlSaving.PerformLayout()
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
    Friend WithEvents btnRescrape As System.Windows.Forms.Button
    Friend WithEvents tmrDelay As System.Windows.Forms.Timer
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
    Friend WithEvents tpMovies As System.Windows.Forms.TabPage
    Friend WithEvents btnMovieDown As System.Windows.Forms.Button
    Friend WithEvents btnMovieUp As System.Windows.Forms.Button
    Friend WithEvents btnMovieRemove As System.Windows.Forms.Button
    Friend WithEvents lblMoviesInMovieset As System.Windows.Forms.Label
    Friend WithEvents tcEditMovie As System.Windows.Forms.TabControl
    Friend WithEvents lblMoviesInDB As System.Windows.Forms.Label
    Friend WithEvents btnMovieAdd As System.Windows.Forms.Button
    Friend WithEvents btnLoadMoviesFromDB As System.Windows.Forms.Button
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents prbCompile As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlSaving As System.Windows.Forms.Panel
    Friend WithEvents lblSaving As System.Windows.Forms.Label
    Friend WithEvents prbSaving As System.Windows.Forms.ProgressBar
    Friend WithEvents lblMoviesToRemove As System.Windows.Forms.Label
    Friend WithEvents btnMovieReAdd As System.Windows.Forms.Button
    Friend WithEvents lvMoviesInSet As System.Windows.Forms.ListView
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colMovie As System.Windows.Forms.ColumnHeader
    Friend WithEvents colOrdering As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvMoviesToRemove As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvMoviesInDB As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tpDetails As System.Windows.Forms.TabPage
    Friend WithEvents lblPlot As System.Windows.Forms.Label
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents btnGetTMDBColID As System.Windows.Forms.Button
    Friend WithEvents txtCollectionID As System.Windows.Forms.TextBox
    Friend WithEvents lblCollectionID As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents chkMark As System.Windows.Forms.CheckBox

End Class
