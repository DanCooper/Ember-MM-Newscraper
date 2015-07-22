<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEditTVSeason
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditTVSeason))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.tcEditSeason = New System.Windows.Forms.TabControl()
        Me.tpSeasonPoster = New System.Windows.Forms.TabPage()
        Me.btnSetSeasonPosterDL = New System.Windows.Forms.Button()
        Me.btnRemoveSeasonPoster = New System.Windows.Forms.Button()
        Me.lblSeasonPosterSize = New System.Windows.Forms.Label()
        Me.btnSetSeasonPosterScrape = New System.Windows.Forms.Button()
        Me.btnSetSeasonPosterLocal = New System.Windows.Forms.Button()
        Me.pbSeasonPoster = New System.Windows.Forms.PictureBox()
        Me.tpSeasonBanner = New System.Windows.Forms.TabPage()
        Me.btnSetSeasonBannerDL = New System.Windows.Forms.Button()
        Me.btnRemoveSeasonBanner = New System.Windows.Forms.Button()
        Me.lblSeasonBannerSize = New System.Windows.Forms.Label()
        Me.btnSetSeasonBannerScrape = New System.Windows.Forms.Button()
        Me.btnSetSeasonBannerLocal = New System.Windows.Forms.Button()
        Me.pbSeasonBanner = New System.Windows.Forms.PictureBox()
        Me.tpSeasonLandscape = New System.Windows.Forms.TabPage()
        Me.btnSetSeasonLandscapeDL = New System.Windows.Forms.Button()
        Me.btnRemoveSeasonLandscape = New System.Windows.Forms.Button()
        Me.lblSeasonLandscapeSize = New System.Windows.Forms.Label()
        Me.btnSetSeasonLandscapeScrape = New System.Windows.Forms.Button()
        Me.btnSetSeasonLandscapeLocal = New System.Windows.Forms.Button()
        Me.pbSeasonLandscape = New System.Windows.Forms.PictureBox()
        Me.tpSeasonFanart = New System.Windows.Forms.TabPage()
        Me.btnSetSeasonFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveSeasonFanart = New System.Windows.Forms.Button()
        Me.lblSeasonFanartSize = New System.Windows.Forms.Label()
        Me.btnSetSeasonFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetSeasonFanartLocal = New System.Windows.Forms.Button()
        Me.pbSeasonFanart = New System.Windows.Forms.PictureBox()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcEditSeason.SuspendLayout()
        Me.tpSeasonPoster.SuspendLayout()
        CType(Me.pbSeasonPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSeasonBanner.SuspendLayout()
        CType(Me.pbSeasonBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSeasonLandscape.SuspendLayout()
        CType(Me.pbSeasonLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpSeasonFanart.SuspendLayout()
        CType(Me.pbSeasonFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.tblTopDetails)
        Me.pnlTop.Controls.Add(Me.lblTopTitle)
        Me.pnlTop.Controls.Add(Me.pbTopLogo)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(854, 64)
        Me.pnlTop.TabIndex = 2
        '
        'tblTopDetails
        '
        Me.tblTopDetails.AutoSize = True
        Me.tblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.tblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.tblTopDetails.ForeColor = System.Drawing.Color.White
        Me.tblTopDetails.Location = New System.Drawing.Point(61, 38)
        Me.tblTopDetails.Name = "tblTopDetails"
        Me.tblTopDetails.Size = New System.Drawing.Size(209, 13)
        Me.tblTopDetails.TabIndex = 1
        Me.tblTopDetails.Text = "Edit the details for the selected season."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(146, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Edit Season"
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
        'tcEditSeason
        '
        Me.tcEditSeason.Controls.Add(Me.tpSeasonPoster)
        Me.tcEditSeason.Controls.Add(Me.tpSeasonBanner)
        Me.tcEditSeason.Controls.Add(Me.tpSeasonLandscape)
        Me.tcEditSeason.Controls.Add(Me.tpSeasonFanart)
        Me.tcEditSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcEditSeason.Location = New System.Drawing.Point(4, 70)
        Me.tcEditSeason.Name = "tcEditSeason"
        Me.tcEditSeason.SelectedIndex = 0
        Me.tcEditSeason.Size = New System.Drawing.Size(844, 478)
        Me.tcEditSeason.TabIndex = 3
        '
        'tpSeasonPoster
        '
        Me.tpSeasonPoster.Controls.Add(Me.btnSetSeasonPosterDL)
        Me.tpSeasonPoster.Controls.Add(Me.btnRemoveSeasonPoster)
        Me.tpSeasonPoster.Controls.Add(Me.lblSeasonPosterSize)
        Me.tpSeasonPoster.Controls.Add(Me.btnSetSeasonPosterScrape)
        Me.tpSeasonPoster.Controls.Add(Me.btnSetSeasonPosterLocal)
        Me.tpSeasonPoster.Controls.Add(Me.pbSeasonPoster)
        Me.tpSeasonPoster.Location = New System.Drawing.Point(4, 22)
        Me.tpSeasonPoster.Name = "tpSeasonPoster"
        Me.tpSeasonPoster.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSeasonPoster.Size = New System.Drawing.Size(836, 452)
        Me.tpSeasonPoster.TabIndex = 1
        Me.tpSeasonPoster.Text = "Poster"
        Me.tpSeasonPoster.UseVisualStyleBackColor = True
        '
        'btnSetSeasonPosterDL
        '
        Me.btnSetSeasonPosterDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonPosterDL.Image = CType(resources.GetObject("btnSetSeasonPosterDL.Image"), System.Drawing.Image)
        Me.btnSetSeasonPosterDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonPosterDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetSeasonPosterDL.Name = "btnSetSeasonPosterDL"
        Me.btnSetSeasonPosterDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonPosterDL.TabIndex = 3
        Me.btnSetSeasonPosterDL.Text = "Change Poster (Download)"
        Me.btnSetSeasonPosterDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonPosterDL.UseVisualStyleBackColor = True
        '
        'btnRemoveSeasonPoster
        '
        Me.btnRemoveSeasonPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveSeasonPoster.Image = CType(resources.GetObject("btnRemoveSeasonPoster.Image"), System.Drawing.Image)
        Me.btnRemoveSeasonPoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveSeasonPoster.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveSeasonPoster.Name = "btnRemoveSeasonPoster"
        Me.btnRemoveSeasonPoster.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveSeasonPoster.TabIndex = 4
        Me.btnRemoveSeasonPoster.Text = "Remove Poster"
        Me.btnRemoveSeasonPoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveSeasonPoster.UseVisualStyleBackColor = True
        '
        'lblSeasonPosterSize
        '
        Me.lblSeasonPosterSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeasonPosterSize.Location = New System.Drawing.Point(8, 8)
        Me.lblSeasonPosterSize.Name = "lblSeasonPosterSize"
        Me.lblSeasonPosterSize.Size = New System.Drawing.Size(105, 23)
        Me.lblSeasonPosterSize.TabIndex = 0
        Me.lblSeasonPosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblSeasonPosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSeasonPosterSize.Visible = False
        '
        'btnSetSeasonPosterScrape
        '
        Me.btnSetSeasonPosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonPosterScrape.Image = CType(resources.GetObject("btnSetSeasonPosterScrape.Image"), System.Drawing.Image)
        Me.btnSetSeasonPosterScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonPosterScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetSeasonPosterScrape.Name = "btnSetSeasonPosterScrape"
        Me.btnSetSeasonPosterScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonPosterScrape.TabIndex = 2
        Me.btnSetSeasonPosterScrape.Text = "Change Poster (Scrape)"
        Me.btnSetSeasonPosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonPosterScrape.UseVisualStyleBackColor = True
        '
        'btnSetSeasonPosterLocal
        '
        Me.btnSetSeasonPosterLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonPosterLocal.Image = CType(resources.GetObject("btnSetSeasonPosterLocal.Image"), System.Drawing.Image)
        Me.btnSetSeasonPosterLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonPosterLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetSeasonPosterLocal.Name = "btnSetSeasonPosterLocal"
        Me.btnSetSeasonPosterLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonPosterLocal.TabIndex = 1
        Me.btnSetSeasonPosterLocal.Text = "Change Poster (Local)"
        Me.btnSetSeasonPosterLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonPosterLocal.UseVisualStyleBackColor = True
        '
        'pbSeasonPoster
        '
        Me.pbSeasonPoster.BackColor = System.Drawing.Color.DimGray
        Me.pbSeasonPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbSeasonPoster.Location = New System.Drawing.Point(6, 6)
        Me.pbSeasonPoster.Name = "pbSeasonPoster"
        Me.pbSeasonPoster.Size = New System.Drawing.Size(724, 440)
        Me.pbSeasonPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSeasonPoster.TabIndex = 0
        Me.pbSeasonPoster.TabStop = False
        '
        'tpSeasonBanner
        '
        Me.tpSeasonBanner.Controls.Add(Me.btnSetSeasonBannerDL)
        Me.tpSeasonBanner.Controls.Add(Me.btnRemoveSeasonBanner)
        Me.tpSeasonBanner.Controls.Add(Me.lblSeasonBannerSize)
        Me.tpSeasonBanner.Controls.Add(Me.btnSetSeasonBannerScrape)
        Me.tpSeasonBanner.Controls.Add(Me.btnSetSeasonBannerLocal)
        Me.tpSeasonBanner.Controls.Add(Me.pbSeasonBanner)
        Me.tpSeasonBanner.Location = New System.Drawing.Point(4, 22)
        Me.tpSeasonBanner.Name = "tpSeasonBanner"
        Me.tpSeasonBanner.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSeasonBanner.Size = New System.Drawing.Size(836, 452)
        Me.tpSeasonBanner.TabIndex = 3
        Me.tpSeasonBanner.Text = "Banner"
        Me.tpSeasonBanner.UseVisualStyleBackColor = True
        '
        'btnSetSeasonBannerDL
        '
        Me.btnSetSeasonBannerDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonBannerDL.Image = CType(resources.GetObject("btnSetSeasonBannerDL.Image"), System.Drawing.Image)
        Me.btnSetSeasonBannerDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonBannerDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetSeasonBannerDL.Name = "btnSetSeasonBannerDL"
        Me.btnSetSeasonBannerDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonBannerDL.TabIndex = 9
        Me.btnSetSeasonBannerDL.Text = "Change Banner (Download)"
        Me.btnSetSeasonBannerDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonBannerDL.UseVisualStyleBackColor = True
        '
        'btnRemoveSeasonBanner
        '
        Me.btnRemoveSeasonBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveSeasonBanner.Image = CType(resources.GetObject("btnRemoveSeasonBanner.Image"), System.Drawing.Image)
        Me.btnRemoveSeasonBanner.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveSeasonBanner.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveSeasonBanner.Name = "btnRemoveSeasonBanner"
        Me.btnRemoveSeasonBanner.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveSeasonBanner.TabIndex = 10
        Me.btnRemoveSeasonBanner.Text = "Remove Banner"
        Me.btnRemoveSeasonBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveSeasonBanner.UseVisualStyleBackColor = True
        '
        'lblSeasonBannerSize
        '
        Me.lblSeasonBannerSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeasonBannerSize.Location = New System.Drawing.Point(8, 8)
        Me.lblSeasonBannerSize.Name = "lblSeasonBannerSize"
        Me.lblSeasonBannerSize.Size = New System.Drawing.Size(105, 23)
        Me.lblSeasonBannerSize.TabIndex = 5
        Me.lblSeasonBannerSize.Text = "Size: (XXXXxXXXX)"
        Me.lblSeasonBannerSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSeasonBannerSize.Visible = False
        '
        'btnSetSeasonBannerScrape
        '
        Me.btnSetSeasonBannerScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonBannerScrape.Image = CType(resources.GetObject("btnSetSeasonBannerScrape.Image"), System.Drawing.Image)
        Me.btnSetSeasonBannerScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonBannerScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetSeasonBannerScrape.Name = "btnSetSeasonBannerScrape"
        Me.btnSetSeasonBannerScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonBannerScrape.TabIndex = 8
        Me.btnSetSeasonBannerScrape.Text = "Change Banner (Scrape)"
        Me.btnSetSeasonBannerScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonBannerScrape.UseVisualStyleBackColor = True
        '
        'btnSetSeasonBannerLocal
        '
        Me.btnSetSeasonBannerLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonBannerLocal.Image = CType(resources.GetObject("btnSetSeasonBannerLocal.Image"), System.Drawing.Image)
        Me.btnSetSeasonBannerLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonBannerLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetSeasonBannerLocal.Name = "btnSetSeasonBannerLocal"
        Me.btnSetSeasonBannerLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonBannerLocal.TabIndex = 7
        Me.btnSetSeasonBannerLocal.Text = "Change Banner (Local)"
        Me.btnSetSeasonBannerLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonBannerLocal.UseVisualStyleBackColor = True
        '
        'pbSeasonBanner
        '
        Me.pbSeasonBanner.BackColor = System.Drawing.Color.DimGray
        Me.pbSeasonBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbSeasonBanner.Location = New System.Drawing.Point(6, 6)
        Me.pbSeasonBanner.Name = "pbSeasonBanner"
        Me.pbSeasonBanner.Size = New System.Drawing.Size(724, 440)
        Me.pbSeasonBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSeasonBanner.TabIndex = 6
        Me.pbSeasonBanner.TabStop = False
        '
        'tpSeasonLandscape
        '
        Me.tpSeasonLandscape.Controls.Add(Me.btnSetSeasonLandscapeDL)
        Me.tpSeasonLandscape.Controls.Add(Me.btnRemoveSeasonLandscape)
        Me.tpSeasonLandscape.Controls.Add(Me.lblSeasonLandscapeSize)
        Me.tpSeasonLandscape.Controls.Add(Me.btnSetSeasonLandscapeScrape)
        Me.tpSeasonLandscape.Controls.Add(Me.btnSetSeasonLandscapeLocal)
        Me.tpSeasonLandscape.Controls.Add(Me.pbSeasonLandscape)
        Me.tpSeasonLandscape.Location = New System.Drawing.Point(4, 22)
        Me.tpSeasonLandscape.Name = "tpSeasonLandscape"
        Me.tpSeasonLandscape.Size = New System.Drawing.Size(836, 452)
        Me.tpSeasonLandscape.TabIndex = 4
        Me.tpSeasonLandscape.Text = "Landscape"
        Me.tpSeasonLandscape.UseVisualStyleBackColor = True
        '
        'btnSetSeasonLandscapeDL
        '
        Me.btnSetSeasonLandscapeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonLandscapeDL.Image = CType(resources.GetObject("btnSetSeasonLandscapeDL.Image"), System.Drawing.Image)
        Me.btnSetSeasonLandscapeDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonLandscapeDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetSeasonLandscapeDL.Name = "btnSetSeasonLandscapeDL"
        Me.btnSetSeasonLandscapeDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonLandscapeDL.TabIndex = 15
        Me.btnSetSeasonLandscapeDL.Text = "Change Landscape (Download)"
        Me.btnSetSeasonLandscapeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonLandscapeDL.UseVisualStyleBackColor = True
        '
        'btnRemoveSeasonLandscape
        '
        Me.btnRemoveSeasonLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveSeasonLandscape.Image = CType(resources.GetObject("btnRemoveSeasonLandscape.Image"), System.Drawing.Image)
        Me.btnRemoveSeasonLandscape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveSeasonLandscape.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveSeasonLandscape.Name = "btnRemoveSeasonLandscape"
        Me.btnRemoveSeasonLandscape.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveSeasonLandscape.TabIndex = 16
        Me.btnRemoveSeasonLandscape.Text = "Remove Landscape"
        Me.btnRemoveSeasonLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveSeasonLandscape.UseVisualStyleBackColor = True
        '
        'lblSeasonLandscapeSize
        '
        Me.lblSeasonLandscapeSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeasonLandscapeSize.Location = New System.Drawing.Point(8, 8)
        Me.lblSeasonLandscapeSize.Name = "lblSeasonLandscapeSize"
        Me.lblSeasonLandscapeSize.Size = New System.Drawing.Size(105, 23)
        Me.lblSeasonLandscapeSize.TabIndex = 11
        Me.lblSeasonLandscapeSize.Text = "Size: (XXXXxXXXX)"
        Me.lblSeasonLandscapeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSeasonLandscapeSize.Visible = False
        '
        'btnSetSeasonLandscapeScrape
        '
        Me.btnSetSeasonLandscapeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonLandscapeScrape.Image = CType(resources.GetObject("btnSetSeasonLandscapeScrape.Image"), System.Drawing.Image)
        Me.btnSetSeasonLandscapeScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonLandscapeScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetSeasonLandscapeScrape.Name = "btnSetSeasonLandscapeScrape"
        Me.btnSetSeasonLandscapeScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonLandscapeScrape.TabIndex = 14
        Me.btnSetSeasonLandscapeScrape.Text = "Change Landscape (Scrape)"
        Me.btnSetSeasonLandscapeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonLandscapeScrape.UseVisualStyleBackColor = True
        '
        'btnSetSeasonLandscapeLocal
        '
        Me.btnSetSeasonLandscapeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonLandscapeLocal.Image = CType(resources.GetObject("btnSetSeasonLandscapeLocal.Image"), System.Drawing.Image)
        Me.btnSetSeasonLandscapeLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonLandscapeLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetSeasonLandscapeLocal.Name = "btnSetSeasonLandscapeLocal"
        Me.btnSetSeasonLandscapeLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonLandscapeLocal.TabIndex = 13
        Me.btnSetSeasonLandscapeLocal.Text = "Change Landscape (Local)"
        Me.btnSetSeasonLandscapeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonLandscapeLocal.UseVisualStyleBackColor = True
        '
        'pbSeasonLandscape
        '
        Me.pbSeasonLandscape.BackColor = System.Drawing.Color.DimGray
        Me.pbSeasonLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbSeasonLandscape.Location = New System.Drawing.Point(6, 6)
        Me.pbSeasonLandscape.Name = "pbSeasonLandscape"
        Me.pbSeasonLandscape.Size = New System.Drawing.Size(724, 440)
        Me.pbSeasonLandscape.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSeasonLandscape.TabIndex = 12
        Me.pbSeasonLandscape.TabStop = False
        '
        'tpSeasonFanart
        '
        Me.tpSeasonFanart.Controls.Add(Me.btnSetSeasonFanartDL)
        Me.tpSeasonFanart.Controls.Add(Me.btnRemoveSeasonFanart)
        Me.tpSeasonFanart.Controls.Add(Me.lblSeasonFanartSize)
        Me.tpSeasonFanart.Controls.Add(Me.btnSetSeasonFanartScrape)
        Me.tpSeasonFanart.Controls.Add(Me.btnSetSeasonFanartLocal)
        Me.tpSeasonFanart.Controls.Add(Me.pbSeasonFanart)
        Me.tpSeasonFanart.Location = New System.Drawing.Point(4, 22)
        Me.tpSeasonFanart.Name = "tpSeasonFanart"
        Me.tpSeasonFanart.Size = New System.Drawing.Size(836, 452)
        Me.tpSeasonFanart.TabIndex = 2
        Me.tpSeasonFanart.Text = "Fanart"
        Me.tpSeasonFanart.UseVisualStyleBackColor = True
        '
        'btnSetSeasonFanartDL
        '
        Me.btnSetSeasonFanartDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonFanartDL.Image = CType(resources.GetObject("btnSetSeasonFanartDL.Image"), System.Drawing.Image)
        Me.btnSetSeasonFanartDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonFanartDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetSeasonFanartDL.Name = "btnSetSeasonFanartDL"
        Me.btnSetSeasonFanartDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonFanartDL.TabIndex = 3
        Me.btnSetSeasonFanartDL.Text = "Change Fanart (Download)"
        Me.btnSetSeasonFanartDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonFanartDL.UseVisualStyleBackColor = True
        '
        'btnRemoveSeasonFanart
        '
        Me.btnRemoveSeasonFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveSeasonFanart.Image = CType(resources.GetObject("btnRemoveSeasonFanart.Image"), System.Drawing.Image)
        Me.btnRemoveSeasonFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveSeasonFanart.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveSeasonFanart.Name = "btnRemoveSeasonFanart"
        Me.btnRemoveSeasonFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveSeasonFanart.TabIndex = 4
        Me.btnRemoveSeasonFanart.Text = "Remove Fanart"
        Me.btnRemoveSeasonFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveSeasonFanart.UseVisualStyleBackColor = True
        '
        'lblSeasonFanartSize
        '
        Me.lblSeasonFanartSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeasonFanartSize.Location = New System.Drawing.Point(8, 8)
        Me.lblSeasonFanartSize.Name = "lblSeasonFanartSize"
        Me.lblSeasonFanartSize.Size = New System.Drawing.Size(105, 23)
        Me.lblSeasonFanartSize.TabIndex = 0
        Me.lblSeasonFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblSeasonFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSeasonFanartSize.Visible = False
        '
        'btnSetSeasonFanartScrape
        '
        Me.btnSetSeasonFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonFanartScrape.Image = CType(resources.GetObject("btnSetSeasonFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetSeasonFanartScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonFanartScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetSeasonFanartScrape.Name = "btnSetSeasonFanartScrape"
        Me.btnSetSeasonFanartScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonFanartScrape.TabIndex = 2
        Me.btnSetSeasonFanartScrape.Text = "Change Fanart (Scrape)"
        Me.btnSetSeasonFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonFanartScrape.UseVisualStyleBackColor = True
        '
        'btnSetSeasonFanartLocal
        '
        Me.btnSetSeasonFanartLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSeasonFanartLocal.Image = CType(resources.GetObject("btnSetSeasonFanartLocal.Image"), System.Drawing.Image)
        Me.btnSetSeasonFanartLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetSeasonFanartLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetSeasonFanartLocal.Name = "btnSetSeasonFanartLocal"
        Me.btnSetSeasonFanartLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetSeasonFanartLocal.TabIndex = 1
        Me.btnSetSeasonFanartLocal.Text = "Change Fanart (Local)"
        Me.btnSetSeasonFanartLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetSeasonFanartLocal.UseVisualStyleBackColor = True
        '
        'pbSeasonFanart
        '
        Me.pbSeasonFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbSeasonFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbSeasonFanart.Location = New System.Drawing.Point(6, 6)
        Me.pbSeasonFanart.Name = "pbSeasonFanart"
        Me.pbSeasonFanart.Size = New System.Drawing.Size(724, 440)
        Me.pbSeasonFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbSeasonFanart.TabIndex = 1
        Me.pbSeasonFanart.TabStop = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(781, 553)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'OK_Button
        '
        Me.OK_Button.Location = New System.Drawing.Point(708, 553)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'dlgEditSeason
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(854, 582)
        Me.Controls.Add(Me.tcEditSeason)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditSeason"
        Me.Text = "Edit Season"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcEditSeason.ResumeLayout(False)
        Me.tpSeasonPoster.ResumeLayout(False)
        CType(Me.pbSeasonPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpSeasonBanner.ResumeLayout(False)
        CType(Me.pbSeasonBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpSeasonLandscape.ResumeLayout(False)
        CType(Me.pbSeasonLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpSeasonFanart.ResumeLayout(False)
        CType(Me.pbSeasonFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents tcEditSeason As System.Windows.Forms.TabControl
    Friend WithEvents tpSeasonPoster As System.Windows.Forms.TabPage
    Friend WithEvents btnSetSeasonPosterDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSeasonPoster As System.Windows.Forms.Button
    Friend WithEvents lblSeasonPosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetSeasonPosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetSeasonPosterLocal As System.Windows.Forms.Button
    Friend WithEvents pbSeasonPoster As System.Windows.Forms.PictureBox
    Friend WithEvents tpSeasonFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetSeasonFanartDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSeasonFanart As System.Windows.Forms.Button
    Friend WithEvents lblSeasonFanartSize As System.Windows.Forms.Label
    Friend WithEvents btnSetSeasonFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetSeasonFanartLocal As System.Windows.Forms.Button
    Friend WithEvents pbSeasonFanart As System.Windows.Forms.PictureBox
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tpSeasonBanner As System.Windows.Forms.TabPage
    Friend WithEvents btnSetSeasonBannerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSeasonBanner As System.Windows.Forms.Button
    Friend WithEvents lblSeasonBannerSize As System.Windows.Forms.Label
    Friend WithEvents btnSetSeasonBannerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetSeasonBannerLocal As System.Windows.Forms.Button
    Friend WithEvents pbSeasonBanner As System.Windows.Forms.PictureBox
    Friend WithEvents tpSeasonLandscape As System.Windows.Forms.TabPage
    Friend WithEvents btnSetSeasonLandscapeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSeasonLandscape As System.Windows.Forms.Button
    Friend WithEvents lblSeasonLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents btnSetSeasonLandscapeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetSeasonLandscapeLocal As System.Windows.Forms.Button
    Friend WithEvents pbSeasonLandscape As System.Windows.Forms.PictureBox

End Class
