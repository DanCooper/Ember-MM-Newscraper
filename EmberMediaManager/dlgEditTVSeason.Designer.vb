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
        Me.tcEdit = New System.Windows.Forms.TabControl()
        Me.tpDetails = New System.Windows.Forms.TabPage()
        Me.txtAired = New System.Windows.Forms.TextBox()
        Me.lblAired = New System.Windows.Forms.Label()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.tpPoster = New System.Windows.Forms.TabPage()
        Me.btnSetPosterDL = New System.Windows.Forms.Button()
        Me.btnRemovePoster = New System.Windows.Forms.Button()
        Me.lblPosterSize = New System.Windows.Forms.Label()
        Me.btnSetPosterScrape = New System.Windows.Forms.Button()
        Me.btnSetPosterLocal = New System.Windows.Forms.Button()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.tpBanner = New System.Windows.Forms.TabPage()
        Me.btnSetBannerDL = New System.Windows.Forms.Button()
        Me.btnRemoveBanner = New System.Windows.Forms.Button()
        Me.lblBannerSize = New System.Windows.Forms.Label()
        Me.btnSetBannerScrape = New System.Windows.Forms.Button()
        Me.btnSetBannerLocal = New System.Windows.Forms.Button()
        Me.pbBanner = New System.Windows.Forms.PictureBox()
        Me.tpLandscape = New System.Windows.Forms.TabPage()
        Me.btnSetLandscapeDL = New System.Windows.Forms.Button()
        Me.btnRemoveLandscape = New System.Windows.Forms.Button()
        Me.lblLandscapeSize = New System.Windows.Forms.Label()
        Me.btnSetLandscapeScrape = New System.Windows.Forms.Button()
        Me.btnSetLandscapeLocal = New System.Windows.Forms.Button()
        Me.pbLandscape = New System.Windows.Forms.PictureBox()
        Me.tpFanart = New System.Windows.Forms.TabPage()
        Me.btnSetFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveFanart = New System.Windows.Forms.Button()
        Me.lblFanartSize = New System.Windows.Forms.Label()
        Me.btnSetFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetFanartLocal = New System.Windows.Forms.Button()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcEdit.SuspendLayout()
        Me.tpDetails.SuspendLayout()
        Me.tpPoster.SuspendLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBanner.SuspendLayout()
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpLandscape.SuspendLayout()
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFanart.SuspendLayout()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'tcEdit
        '
        Me.tcEdit.Controls.Add(Me.tpDetails)
        Me.tcEdit.Controls.Add(Me.tpPoster)
        Me.tcEdit.Controls.Add(Me.tpBanner)
        Me.tcEdit.Controls.Add(Me.tpLandscape)
        Me.tcEdit.Controls.Add(Me.tpFanart)
        Me.tcEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcEdit.Location = New System.Drawing.Point(4, 70)
        Me.tcEdit.Name = "tcEdit"
        Me.tcEdit.SelectedIndex = 0
        Me.tcEdit.Size = New System.Drawing.Size(844, 478)
        Me.tcEdit.TabIndex = 3
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.txtAired)
        Me.tpDetails.Controls.Add(Me.lblAired)
        Me.tpDetails.Controls.Add(Me.lblPlot)
        Me.tpDetails.Controls.Add(Me.txtPlot)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Size = New System.Drawing.Size(836, 452)
        Me.tpDetails.TabIndex = 5
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'txtAired
        '
        Me.txtAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAired.Location = New System.Drawing.Point(7, 22)
        Me.txtAired.Name = "txtAired"
        Me.txtAired.Size = New System.Drawing.Size(137, 22)
        Me.txtAired.TabIndex = 32
        '
        'lblAired
        '
        Me.lblAired.AutoSize = True
        Me.lblAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAired.Location = New System.Drawing.Point(5, 7)
        Me.lblAired.Name = "lblAired"
        Me.lblAired.Size = New System.Drawing.Size(38, 13)
        Me.lblAired.TabIndex = 31
        Me.lblAired.Text = "Aired:"
        '
        'lblPlot
        '
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(5, 47)
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
        Me.txtPlot.Location = New System.Drawing.Point(7, 63)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(811, 229)
        Me.txtPlot.TabIndex = 30
        '
        'tpPoster
        '
        Me.tpPoster.Controls.Add(Me.btnSetPosterDL)
        Me.tpPoster.Controls.Add(Me.btnRemovePoster)
        Me.tpPoster.Controls.Add(Me.lblPosterSize)
        Me.tpPoster.Controls.Add(Me.btnSetPosterScrape)
        Me.tpPoster.Controls.Add(Me.btnSetPosterLocal)
        Me.tpPoster.Controls.Add(Me.pbPoster)
        Me.tpPoster.Location = New System.Drawing.Point(4, 22)
        Me.tpPoster.Name = "tpPoster"
        Me.tpPoster.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPoster.Size = New System.Drawing.Size(836, 452)
        Me.tpPoster.TabIndex = 1
        Me.tpPoster.Text = "Poster"
        Me.tpPoster.UseVisualStyleBackColor = True
        '
        'btnSetPosterDL
        '
        Me.btnSetPosterDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetPosterDL.Image = CType(resources.GetObject("btnSetPosterDL.Image"), System.Drawing.Image)
        Me.btnSetPosterDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetPosterDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetPosterDL.Name = "btnSetPosterDL"
        Me.btnSetPosterDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetPosterDL.TabIndex = 3
        Me.btnSetPosterDL.Text = "Download"
        Me.btnSetPosterDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterDL.UseVisualStyleBackColor = True
        '
        'btnRemovePoster
        '
        Me.btnRemovePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemovePoster.Image = CType(resources.GetObject("btnRemovePoster.Image"), System.Drawing.Image)
        Me.btnRemovePoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemovePoster.Location = New System.Drawing.Point(735, 363)
        Me.btnRemovePoster.Name = "btnRemovePoster"
        Me.btnRemovePoster.Size = New System.Drawing.Size(96, 83)
        Me.btnRemovePoster.TabIndex = 4
        Me.btnRemovePoster.Text = "Remove"
        Me.btnRemovePoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemovePoster.UseVisualStyleBackColor = True
        '
        'lblPosterSize
        '
        Me.lblPosterSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPosterSize.Location = New System.Drawing.Point(8, 8)
        Me.lblPosterSize.Name = "lblPosterSize"
        Me.lblPosterSize.Size = New System.Drawing.Size(105, 23)
        Me.lblPosterSize.TabIndex = 0
        Me.lblPosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblPosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPosterSize.Visible = False
        '
        'btnSetPosterScrape
        '
        Me.btnSetPosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetPosterScrape.Image = CType(resources.GetObject("btnSetPosterScrape.Image"), System.Drawing.Image)
        Me.btnSetPosterScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetPosterScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetPosterScrape.Name = "btnSetPosterScrape"
        Me.btnSetPosterScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetPosterScrape.TabIndex = 2
        Me.btnSetPosterScrape.Text = "Scrape"
        Me.btnSetPosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterScrape.UseVisualStyleBackColor = True
        '
        'btnSetPosterLocal
        '
        Me.btnSetPosterLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetPosterLocal.Image = CType(resources.GetObject("btnSetPosterLocal.Image"), System.Drawing.Image)
        Me.btnSetPosterLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetPosterLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetPosterLocal.Name = "btnSetPosterLocal"
        Me.btnSetPosterLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetPosterLocal.TabIndex = 1
        Me.btnSetPosterLocal.Text = "Local"
        Me.btnSetPosterLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterLocal.UseVisualStyleBackColor = True
        '
        'pbPoster
        '
        Me.pbPoster.BackColor = System.Drawing.Color.DimGray
        Me.pbPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbPoster.Location = New System.Drawing.Point(6, 6)
        Me.pbPoster.Name = "pbPoster"
        Me.pbPoster.Size = New System.Drawing.Size(724, 440)
        Me.pbPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbPoster.TabIndex = 0
        Me.pbPoster.TabStop = False
        '
        'tpBanner
        '
        Me.tpBanner.Controls.Add(Me.btnSetBannerDL)
        Me.tpBanner.Controls.Add(Me.btnRemoveBanner)
        Me.tpBanner.Controls.Add(Me.lblBannerSize)
        Me.tpBanner.Controls.Add(Me.btnSetBannerScrape)
        Me.tpBanner.Controls.Add(Me.btnSetBannerLocal)
        Me.tpBanner.Controls.Add(Me.pbBanner)
        Me.tpBanner.Location = New System.Drawing.Point(4, 22)
        Me.tpBanner.Name = "tpBanner"
        Me.tpBanner.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBanner.Size = New System.Drawing.Size(836, 452)
        Me.tpBanner.TabIndex = 3
        Me.tpBanner.Text = "Banner"
        Me.tpBanner.UseVisualStyleBackColor = True
        '
        'btnSetBannerDL
        '
        Me.btnSetBannerDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetBannerDL.Image = CType(resources.GetObject("btnSetBannerDL.Image"), System.Drawing.Image)
        Me.btnSetBannerDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetBannerDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetBannerDL.Name = "btnSetBannerDL"
        Me.btnSetBannerDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetBannerDL.TabIndex = 9
        Me.btnSetBannerDL.Text = "Download"
        Me.btnSetBannerDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerDL.UseVisualStyleBackColor = True
        '
        'btnRemoveBanner
        '
        Me.btnRemoveBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveBanner.Image = CType(resources.GetObject("btnRemoveBanner.Image"), System.Drawing.Image)
        Me.btnRemoveBanner.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveBanner.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveBanner.Name = "btnRemoveBanner"
        Me.btnRemoveBanner.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveBanner.TabIndex = 10
        Me.btnRemoveBanner.Text = "Remove"
        Me.btnRemoveBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveBanner.UseVisualStyleBackColor = True
        '
        'lblBannerSize
        '
        Me.lblBannerSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBannerSize.Location = New System.Drawing.Point(8, 8)
        Me.lblBannerSize.Name = "lblBannerSize"
        Me.lblBannerSize.Size = New System.Drawing.Size(105, 23)
        Me.lblBannerSize.TabIndex = 5
        Me.lblBannerSize.Text = "Size: (XXXXxXXXX)"
        Me.lblBannerSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBannerSize.Visible = False
        '
        'btnSetBannerScrape
        '
        Me.btnSetBannerScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetBannerScrape.Image = CType(resources.GetObject("btnSetBannerScrape.Image"), System.Drawing.Image)
        Me.btnSetBannerScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetBannerScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetBannerScrape.Name = "btnSetBannerScrape"
        Me.btnSetBannerScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetBannerScrape.TabIndex = 8
        Me.btnSetBannerScrape.Text = "Scrape"
        Me.btnSetBannerScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerScrape.UseVisualStyleBackColor = True
        '
        'btnSetBannerLocal
        '
        Me.btnSetBannerLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetBannerLocal.Image = CType(resources.GetObject("btnSetBannerLocal.Image"), System.Drawing.Image)
        Me.btnSetBannerLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetBannerLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetBannerLocal.Name = "btnSetBannerLocal"
        Me.btnSetBannerLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetBannerLocal.TabIndex = 7
        Me.btnSetBannerLocal.Text = "Local"
        Me.btnSetBannerLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetBannerLocal.UseVisualStyleBackColor = True
        '
        'pbBanner
        '
        Me.pbBanner.BackColor = System.Drawing.Color.DimGray
        Me.pbBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbBanner.Location = New System.Drawing.Point(6, 6)
        Me.pbBanner.Name = "pbBanner"
        Me.pbBanner.Size = New System.Drawing.Size(724, 440)
        Me.pbBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbBanner.TabIndex = 6
        Me.pbBanner.TabStop = False
        '
        'tpLandscape
        '
        Me.tpLandscape.Controls.Add(Me.btnSetLandscapeDL)
        Me.tpLandscape.Controls.Add(Me.btnRemoveLandscape)
        Me.tpLandscape.Controls.Add(Me.lblLandscapeSize)
        Me.tpLandscape.Controls.Add(Me.btnSetLandscapeScrape)
        Me.tpLandscape.Controls.Add(Me.btnSetLandscapeLocal)
        Me.tpLandscape.Controls.Add(Me.pbLandscape)
        Me.tpLandscape.Location = New System.Drawing.Point(4, 22)
        Me.tpLandscape.Name = "tpLandscape"
        Me.tpLandscape.Size = New System.Drawing.Size(836, 452)
        Me.tpLandscape.TabIndex = 4
        Me.tpLandscape.Text = "Landscape"
        Me.tpLandscape.UseVisualStyleBackColor = True
        '
        'btnSetLandscapeDL
        '
        Me.btnSetLandscapeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetLandscapeDL.Image = CType(resources.GetObject("btnSetLandscapeDL.Image"), System.Drawing.Image)
        Me.btnSetLandscapeDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetLandscapeDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetLandscapeDL.Name = "btnSetLandscapeDL"
        Me.btnSetLandscapeDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetLandscapeDL.TabIndex = 15
        Me.btnSetLandscapeDL.Text = "Download"
        Me.btnSetLandscapeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeDL.UseVisualStyleBackColor = True
        '
        'btnRemoveLandscape
        '
        Me.btnRemoveLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveLandscape.Image = CType(resources.GetObject("btnRemoveLandscape.Image"), System.Drawing.Image)
        Me.btnRemoveLandscape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveLandscape.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveLandscape.Name = "btnRemoveLandscape"
        Me.btnRemoveLandscape.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveLandscape.TabIndex = 16
        Me.btnRemoveLandscape.Text = "Remove"
        Me.btnRemoveLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveLandscape.UseVisualStyleBackColor = True
        '
        'lblLandscapeSize
        '
        Me.lblLandscapeSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLandscapeSize.Location = New System.Drawing.Point(8, 8)
        Me.lblLandscapeSize.Name = "lblLandscapeSize"
        Me.lblLandscapeSize.Size = New System.Drawing.Size(105, 23)
        Me.lblLandscapeSize.TabIndex = 11
        Me.lblLandscapeSize.Text = "Size: (XXXXxXXXX)"
        Me.lblLandscapeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLandscapeSize.Visible = False
        '
        'btnSetLandscapeScrape
        '
        Me.btnSetLandscapeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetLandscapeScrape.Image = CType(resources.GetObject("btnSetLandscapeScrape.Image"), System.Drawing.Image)
        Me.btnSetLandscapeScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetLandscapeScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetLandscapeScrape.Name = "btnSetLandscapeScrape"
        Me.btnSetLandscapeScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetLandscapeScrape.TabIndex = 14
        Me.btnSetLandscapeScrape.Text = "Scrape"
        Me.btnSetLandscapeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeScrape.UseVisualStyleBackColor = True
        '
        'btnSetLandscapeLocal
        '
        Me.btnSetLandscapeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetLandscapeLocal.Image = CType(resources.GetObject("btnSetLandscapeLocal.Image"), System.Drawing.Image)
        Me.btnSetLandscapeLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetLandscapeLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetLandscapeLocal.Name = "btnSetLandscapeLocal"
        Me.btnSetLandscapeLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetLandscapeLocal.TabIndex = 13
        Me.btnSetLandscapeLocal.Text = "Local"
        Me.btnSetLandscapeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetLandscapeLocal.UseVisualStyleBackColor = True
        '
        'pbLandscape
        '
        Me.pbLandscape.BackColor = System.Drawing.Color.DimGray
        Me.pbLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbLandscape.Location = New System.Drawing.Point(6, 6)
        Me.pbLandscape.Name = "pbLandscape"
        Me.pbLandscape.Size = New System.Drawing.Size(724, 440)
        Me.pbLandscape.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbLandscape.TabIndex = 12
        Me.pbLandscape.TabStop = False
        '
        'tpFanart
        '
        Me.tpFanart.Controls.Add(Me.btnSetFanartDL)
        Me.tpFanart.Controls.Add(Me.btnRemoveFanart)
        Me.tpFanart.Controls.Add(Me.lblFanartSize)
        Me.tpFanart.Controls.Add(Me.btnSetFanartScrape)
        Me.tpFanart.Controls.Add(Me.btnSetFanartLocal)
        Me.tpFanart.Controls.Add(Me.pbFanart)
        Me.tpFanart.Location = New System.Drawing.Point(4, 22)
        Me.tpFanart.Name = "tpFanart"
        Me.tpFanart.Size = New System.Drawing.Size(836, 452)
        Me.tpFanart.TabIndex = 2
        Me.tpFanart.Text = "Fanart"
        Me.tpFanart.UseVisualStyleBackColor = True
        '
        'btnSetFanartDL
        '
        Me.btnSetFanartDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetFanartDL.Image = CType(resources.GetObject("btnSetFanartDL.Image"), System.Drawing.Image)
        Me.btnSetFanartDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetFanartDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetFanartDL.Name = "btnSetFanartDL"
        Me.btnSetFanartDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetFanartDL.TabIndex = 3
        Me.btnSetFanartDL.Text = "Download"
        Me.btnSetFanartDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartDL.UseVisualStyleBackColor = True
        '
        'btnRemoveFanart
        '
        Me.btnRemoveFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveFanart.Image = CType(resources.GetObject("btnRemoveFanart.Image"), System.Drawing.Image)
        Me.btnRemoveFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveFanart.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveFanart.Name = "btnRemoveFanart"
        Me.btnRemoveFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveFanart.TabIndex = 4
        Me.btnRemoveFanart.Text = "Remove"
        Me.btnRemoveFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveFanart.UseVisualStyleBackColor = True
        '
        'lblFanartSize
        '
        Me.lblFanartSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFanartSize.Location = New System.Drawing.Point(8, 8)
        Me.lblFanartSize.Name = "lblFanartSize"
        Me.lblFanartSize.Size = New System.Drawing.Size(105, 23)
        Me.lblFanartSize.TabIndex = 0
        Me.lblFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFanartSize.Visible = False
        '
        'btnSetFanartScrape
        '
        Me.btnSetFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetFanartScrape.Image = CType(resources.GetObject("btnSetFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetFanartScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetFanartScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetFanartScrape.Name = "btnSetFanartScrape"
        Me.btnSetFanartScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetFanartScrape.TabIndex = 2
        Me.btnSetFanartScrape.Text = "Scrape"
        Me.btnSetFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartScrape.UseVisualStyleBackColor = True
        '
        'btnSetFanartLocal
        '
        Me.btnSetFanartLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetFanartLocal.Image = CType(resources.GetObject("btnSetFanartLocal.Image"), System.Drawing.Image)
        Me.btnSetFanartLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetFanartLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetFanartLocal.Name = "btnSetFanartLocal"
        Me.btnSetFanartLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetFanartLocal.TabIndex = 1
        Me.btnSetFanartLocal.Text = "Local"
        Me.btnSetFanartLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartLocal.UseVisualStyleBackColor = True
        '
        'pbFanart
        '
        Me.pbFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbFanart.Location = New System.Drawing.Point(6, 6)
        Me.pbFanart.Name = "pbFanart"
        Me.pbFanart.Size = New System.Drawing.Size(724, 440)
        Me.pbFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbFanart.TabIndex = 1
        Me.pbFanart.TabStop = False
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
        'dlgEditTVSeason
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(854, 582)
        Me.Controls.Add(Me.tcEdit)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditTVSeason"
        Me.Text = "Edit Season"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcEdit.ResumeLayout(False)
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        Me.tpPoster.ResumeLayout(False)
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBanner.ResumeLayout(False)
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpLandscape.ResumeLayout(False)
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFanart.ResumeLayout(False)
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents tcEdit As System.Windows.Forms.TabControl
    Friend WithEvents tpPoster As System.Windows.Forms.TabPage
    Friend WithEvents btnSetPosterDL As System.Windows.Forms.Button
    Friend WithEvents btnRemovePoster As System.Windows.Forms.Button
    Friend WithEvents lblPosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetPosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetPosterLocal As System.Windows.Forms.Button
    Friend WithEvents pbPoster As System.Windows.Forms.PictureBox
    Friend WithEvents tpFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetFanartDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveFanart As System.Windows.Forms.Button
    Friend WithEvents lblFanartSize As System.Windows.Forms.Label
    Friend WithEvents btnSetFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetFanartLocal As System.Windows.Forms.Button
    Friend WithEvents pbFanart As System.Windows.Forms.PictureBox
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tpBanner As System.Windows.Forms.TabPage
    Friend WithEvents btnSetBannerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveBanner As System.Windows.Forms.Button
    Friend WithEvents lblBannerSize As System.Windows.Forms.Label
    Friend WithEvents btnSetBannerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetBannerLocal As System.Windows.Forms.Button
    Friend WithEvents pbBanner As System.Windows.Forms.PictureBox
    Friend WithEvents tpLandscape As System.Windows.Forms.TabPage
    Friend WithEvents btnSetLandscapeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveLandscape As System.Windows.Forms.Button
    Friend WithEvents lblLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents btnSetLandscapeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetLandscapeLocal As System.Windows.Forms.Button
    Friend WithEvents pbLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents tpDetails As System.Windows.Forms.TabPage
    Friend WithEvents lblPlot As System.Windows.Forms.Label
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents txtAired As System.Windows.Forms.TextBox
    Friend WithEvents lblAired As System.Windows.Forms.Label

End Class
