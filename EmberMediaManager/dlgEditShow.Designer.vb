<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEditShow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditShow))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.cbOrdering = New System.Windows.Forms.ComboBox()
        Me.lblOrdering = New System.Windows.Forms.Label()
        Me.cbEpisodeSorting = New System.Windows.Forms.ComboBox()
        Me.lblEpisodeSorting = New System.Windows.Forms.Label()
        Me.tpShowEFanarts = New System.Windows.Forms.TabPage()
        Me.pbShowEFanarts = New System.Windows.Forms.PictureBox()
        Me.btnShowEFanartsRemove = New System.Windows.Forms.Button()
        Me.btnShowEFanartsRefresh = New System.Windows.Forms.Button()
        Me.pnlShowEFanartsBG = New System.Windows.Forms.Panel()
        Me.pnlShowEFanartsSetAsFanart = New System.Windows.Forms.Panel()
        Me.btnShowEFanartsSetAsFanart = New System.Windows.Forms.Button()
        Me.lblShowEFanartsSize = New System.Windows.Forms.Label()
        Me.tpShowFanart = New System.Windows.Forms.TabPage()
        Me.pbShowFanart = New System.Windows.Forms.PictureBox()
        Me.btnSetShowFanartLocal = New System.Windows.Forms.Button()
        Me.btnSetShowFanartScrape = New System.Windows.Forms.Button()
        Me.lblShowFanartSize = New System.Windows.Forms.Label()
        Me.btnRemoveShowFanart = New System.Windows.Forms.Button()
        Me.btnSetShowFanartDL = New System.Windows.Forms.Button()
        Me.tpShowClearLogo = New System.Windows.Forms.TabPage()
        Me.pbShowClearLogo = New System.Windows.Forms.PictureBox()
        Me.btnSetShowClearLogoLocal = New System.Windows.Forms.Button()
        Me.btnSetShowClearLogoScrape = New System.Windows.Forms.Button()
        Me.btnRemoveShowClearLogo = New System.Windows.Forms.Button()
        Me.btnSetShowClearLogoDL = New System.Windows.Forms.Button()
        Me.lblShowClearLogoSize = New System.Windows.Forms.Label()
        Me.tpShowClearArt = New System.Windows.Forms.TabPage()
        Me.pbShowClearArt = New System.Windows.Forms.PictureBox()
        Me.btnSetShowClearArtLocal = New System.Windows.Forms.Button()
        Me.btnSetShowClearArtScrape = New System.Windows.Forms.Button()
        Me.btnRemoveShowClearArt = New System.Windows.Forms.Button()
        Me.btnSetShowClearArtDL = New System.Windows.Forms.Button()
        Me.lblShowClearArtSize = New System.Windows.Forms.Label()
        Me.tpShowCharacterArt = New System.Windows.Forms.TabPage()
        Me.pbShowCharacterArt = New System.Windows.Forms.PictureBox()
        Me.btnSetShowCharacterArtLocal = New System.Windows.Forms.Button()
        Me.btnSetShowCharacterArtScrape = New System.Windows.Forms.Button()
        Me.btnRemoveShowCharacterArt = New System.Windows.Forms.Button()
        Me.btnSetShowCharacterArtDL = New System.Windows.Forms.Button()
        Me.lblShowCharacterArtSize = New System.Windows.Forms.Label()
        Me.tpShowLandscape = New System.Windows.Forms.TabPage()
        Me.pbShowLandscape = New System.Windows.Forms.PictureBox()
        Me.btnSetShowLandscapeLocal = New System.Windows.Forms.Button()
        Me.btnSetShowLandscapeScrape = New System.Windows.Forms.Button()
        Me.lblShowLandscapeSize = New System.Windows.Forms.Label()
        Me.btnRemoveShowLandscape = New System.Windows.Forms.Button()
        Me.btnSetShowLandscapeDL = New System.Windows.Forms.Button()
        Me.tpShowBanner = New System.Windows.Forms.TabPage()
        Me.pbShowBanner = New System.Windows.Forms.PictureBox()
        Me.btnSetShowBannerLocal = New System.Windows.Forms.Button()
        Me.btnSetShowBannerScrape = New System.Windows.Forms.Button()
        Me.lblShowBannerSize = New System.Windows.Forms.Label()
        Me.btnRemoveShowBanner = New System.Windows.Forms.Button()
        Me.btnSetShowBannerDL = New System.Windows.Forms.Button()
        Me.tpShowPoster = New System.Windows.Forms.TabPage()
        Me.pbShowPoster = New System.Windows.Forms.PictureBox()
        Me.btnSetShowPosterLocal = New System.Windows.Forms.Button()
        Me.btnSetShowPosterScrape = New System.Windows.Forms.Button()
        Me.lblShowPosterSize = New System.Windows.Forms.Label()
        Me.btnRemoveShowPoster = New System.Windows.Forms.Button()
        Me.btnSetShowPosterDL = New System.Windows.Forms.Button()
        Me.tpShowDetails = New System.Windows.Forms.TabPage()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.txtSortTitle = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSortTitle = New System.Windows.Forms.Label()
        Me.lblPremiered = New System.Windows.Forms.Label()
        Me.lblRating = New System.Windows.Forms.Label()
        Me.pbStar1 = New System.Windows.Forms.PictureBox()
        Me.pbStar2 = New System.Windows.Forms.PictureBox()
        Me.pbStar3 = New System.Windows.Forms.PictureBox()
        Me.pbStar4 = New System.Windows.Forms.PictureBox()
        Me.pbStar5 = New System.Windows.Forms.PictureBox()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.lblMPAA = New System.Windows.Forms.Label()
        Me.lblGenre = New System.Windows.Forms.Label()
        Me.lbMPAA = New System.Windows.Forms.ListBox()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblActors = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnAddActor = New System.Windows.Forms.Button()
        Me.btnEditActor = New System.Windows.Forms.Button()
        Me.txtStudio = New System.Windows.Forms.TextBox()
        Me.lblStudio = New System.Windows.Forms.Label()
        Me.clbGenre = New System.Windows.Forms.CheckedListBox()
        Me.txtPremiered = New System.Windows.Forms.TextBox()
        Me.btnActorUp = New System.Windows.Forms.Button()
        Me.btnActorDown = New System.Windows.Forms.Button()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbStar6 = New System.Windows.Forms.PictureBox()
        Me.pbStar7 = New System.Windows.Forms.PictureBox()
        Me.pbStar8 = New System.Windows.Forms.PictureBox()
        Me.pbStar9 = New System.Windows.Forms.PictureBox()
        Me.pbStar10 = New System.Windows.Forms.PictureBox()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.lblVotes = New System.Windows.Forms.Label()
        Me.txtVotes = New System.Windows.Forms.TextBox()
        Me.txtMPAA = New System.Windows.Forms.TextBox()
        Me.tcEditShow = New System.Windows.Forms.TabControl()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowEFanarts.SuspendLayout()
        CType(Me.pbShowEFanarts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlShowEFanartsSetAsFanart.SuspendLayout()
        Me.tpShowFanart.SuspendLayout()
        CType(Me.pbShowFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowClearLogo.SuspendLayout()
        CType(Me.pbShowClearLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowClearArt.SuspendLayout()
        CType(Me.pbShowClearArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowCharacterArt.SuspendLayout()
        CType(Me.pbShowCharacterArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowLandscape.SuspendLayout()
        CType(Me.pbShowLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowBanner.SuspendLayout()
        CType(Me.pbShowBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowPoster.SuspendLayout()
        CType(Me.pbShowPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowDetails.SuspendLayout()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcEditShow.SuspendLayout()
        Me.SuspendLayout()
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
        Me.pnlTop.Size = New System.Drawing.Size(853, 64)
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
        Me.lblTopDetails.Size = New System.Drawing.Size(201, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Edit the details for the selected show."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(127, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Edit Show"
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
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(781, 565)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'OK_Button
        '
        Me.OK_Button.Location = New System.Drawing.Point(708, 565)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'cbOrdering
        '
        Me.cbOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbOrdering.FormattingEnabled = True
        Me.cbOrdering.Location = New System.Drawing.Point(111, 565)
        Me.cbOrdering.Name = "cbOrdering"
        Me.cbOrdering.Size = New System.Drawing.Size(166, 21)
        Me.cbOrdering.TabIndex = 5
        '
        'lblOrdering
        '
        Me.lblOrdering.AutoSize = True
        Me.lblOrdering.Location = New System.Drawing.Point(5, 570)
        Me.lblOrdering.Name = "lblOrdering"
        Me.lblOrdering.Size = New System.Drawing.Size(101, 13)
        Me.lblOrdering.TabIndex = 4
        Me.lblOrdering.Text = "Episode Ordering:"
        '
        'cbEpisodeSorting
        '
        Me.cbEpisodeSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEpisodeSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbEpisodeSorting.FormattingEnabled = True
        Me.cbEpisodeSorting.Location = New System.Drawing.Point(440, 565)
        Me.cbEpisodeSorting.Name = "cbEpisodeSorting"
        Me.cbEpisodeSorting.Size = New System.Drawing.Size(166, 21)
        Me.cbEpisodeSorting.TabIndex = 5
        '
        'lblEpisodeSorting
        '
        Me.lblEpisodeSorting.AutoSize = True
        Me.lblEpisodeSorting.Location = New System.Drawing.Point(330, 570)
        Me.lblEpisodeSorting.Name = "lblEpisodeSorting"
        Me.lblEpisodeSorting.Size = New System.Drawing.Size(103, 13)
        Me.lblEpisodeSorting.TabIndex = 4
        Me.lblEpisodeSorting.Text = "Episode Sorted by:"
        Me.lblEpisodeSorting.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tpShowEFanarts
        '
        Me.tpShowEFanarts.Controls.Add(Me.lblShowEFanartsSize)
        Me.tpShowEFanarts.Controls.Add(Me.pnlShowEFanartsSetAsFanart)
        Me.tpShowEFanarts.Controls.Add(Me.pnlShowEFanartsBG)
        Me.tpShowEFanarts.Controls.Add(Me.btnShowEFanartsRefresh)
        Me.tpShowEFanarts.Controls.Add(Me.btnShowEFanartsRemove)
        Me.tpShowEFanarts.Controls.Add(Me.pbShowEFanarts)
        Me.tpShowEFanarts.Location = New System.Drawing.Point(4, 22)
        Me.tpShowEFanarts.Name = "tpShowEFanarts"
        Me.tpShowEFanarts.Size = New System.Drawing.Size(836, 463)
        Me.tpShowEFanarts.TabIndex = 12
        Me.tpShowEFanarts.Text = "Extrafanarts"
        Me.tpShowEFanarts.UseVisualStyleBackColor = True
        '
        'pbShowEFanarts
        '
        Me.pbShowEFanarts.BackColor = System.Drawing.Color.DimGray
        Me.pbShowEFanarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbShowEFanarts.Location = New System.Drawing.Point(177, 8)
        Me.pbShowEFanarts.Name = "pbShowEFanarts"
        Me.pbShowEFanarts.Size = New System.Drawing.Size(653, 437)
        Me.pbShowEFanarts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbShowEFanarts.TabIndex = 18
        Me.pbShowEFanarts.TabStop = False
        '
        'btnShowEFanartsRemove
        '
        Me.btnShowEFanartsRemove.Image = CType(resources.GetObject("btnShowEFanartsRemove.Image"), System.Drawing.Image)
        Me.btnShowEFanartsRemove.Location = New System.Drawing.Point(148, 422)
        Me.btnShowEFanartsRemove.Name = "btnShowEFanartsRemove"
        Me.btnShowEFanartsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnShowEFanartsRemove.TabIndex = 20
        Me.btnShowEFanartsRemove.UseVisualStyleBackColor = True
        '
        'btnShowEFanartsRefresh
        '
        Me.btnShowEFanartsRefresh.Image = CType(resources.GetObject("btnShowEFanartsRefresh.Image"), System.Drawing.Image)
        Me.btnShowEFanartsRefresh.Location = New System.Drawing.Point(88, 422)
        Me.btnShowEFanartsRefresh.Name = "btnShowEFanartsRefresh"
        Me.btnShowEFanartsRefresh.Size = New System.Drawing.Size(23, 23)
        Me.btnShowEFanartsRefresh.TabIndex = 19
        Me.btnShowEFanartsRefresh.UseVisualStyleBackColor = True
        '
        'pnlShowEFanartsBG
        '
        Me.pnlShowEFanartsBG.AutoScroll = True
        Me.pnlShowEFanartsBG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlShowEFanartsBG.Location = New System.Drawing.Point(6, 8)
        Me.pnlShowEFanartsBG.Name = "pnlShowEFanartsBG"
        Me.pnlShowEFanartsBG.Size = New System.Drawing.Size(165, 408)
        Me.pnlShowEFanartsBG.TabIndex = 22
        '
        'pnlShowEFanartsSetAsFanart
        '
        Me.pnlShowEFanartsSetAsFanart.BackColor = System.Drawing.Color.LightGray
        Me.pnlShowEFanartsSetAsFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlShowEFanartsSetAsFanart.Controls.Add(Me.btnShowEFanartsSetAsFanart)
        Me.pnlShowEFanartsSetAsFanart.Location = New System.Drawing.Point(719, 403)
        Me.pnlShowEFanartsSetAsFanart.Name = "pnlShowEFanartsSetAsFanart"
        Me.pnlShowEFanartsSetAsFanart.Size = New System.Drawing.Size(109, 39)
        Me.pnlShowEFanartsSetAsFanart.TabIndex = 21
        '
        'btnShowEFanartsSetAsFanart
        '
        Me.btnShowEFanartsSetAsFanart.Enabled = False
        Me.btnShowEFanartsSetAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnShowEFanartsSetAsFanart.Image = CType(resources.GetObject("btnShowEFanartsSetAsFanart.Image"), System.Drawing.Image)
        Me.btnShowEFanartsSetAsFanart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnShowEFanartsSetAsFanart.Location = New System.Drawing.Point(2, 3)
        Me.btnShowEFanartsSetAsFanart.Name = "btnShowEFanartsSetAsFanart"
        Me.btnShowEFanartsSetAsFanart.Size = New System.Drawing.Size(103, 32)
        Me.btnShowEFanartsSetAsFanart.TabIndex = 0
        Me.btnShowEFanartsSetAsFanart.Text = "Set As Fanart"
        Me.btnShowEFanartsSetAsFanart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnShowEFanartsSetAsFanart.UseVisualStyleBackColor = True
        '
        'lblShowEFanartsSize
        '
        Me.lblShowEFanartsSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShowEFanartsSize.Location = New System.Drawing.Point(178, 10)
        Me.lblShowEFanartsSize.Name = "lblShowEFanartsSize"
        Me.lblShowEFanartsSize.Size = New System.Drawing.Size(104, 23)
        Me.lblShowEFanartsSize.TabIndex = 23
        Me.lblShowEFanartsSize.Text = "Size: (XXXXxXXXX)"
        Me.lblShowEFanartsSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblShowEFanartsSize.Visible = False
        '
        'tpShowFanart
        '
        Me.tpShowFanart.Controls.Add(Me.btnSetShowFanartDL)
        Me.tpShowFanart.Controls.Add(Me.btnRemoveShowFanart)
        Me.tpShowFanart.Controls.Add(Me.lblShowFanartSize)
        Me.tpShowFanart.Controls.Add(Me.btnSetShowFanartScrape)
        Me.tpShowFanart.Controls.Add(Me.btnSetShowFanartLocal)
        Me.tpShowFanart.Controls.Add(Me.pbShowFanart)
        Me.tpShowFanart.Location = New System.Drawing.Point(4, 22)
        Me.tpShowFanart.Name = "tpShowFanart"
        Me.tpShowFanart.Size = New System.Drawing.Size(836, 463)
        Me.tpShowFanart.TabIndex = 2
        Me.tpShowFanart.Text = "Fanart"
        Me.tpShowFanart.UseVisualStyleBackColor = True
        '
        'pbShowFanart
        '
        Me.pbShowFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbShowFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbShowFanart.Location = New System.Drawing.Point(6, 6)
        Me.pbShowFanart.Name = "pbShowFanart"
        Me.pbShowFanart.Size = New System.Drawing.Size(724, 440)
        Me.pbShowFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbShowFanart.TabIndex = 1
        Me.pbShowFanart.TabStop = False
        '
        'btnSetShowFanartLocal
        '
        Me.btnSetShowFanartLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowFanartLocal.Image = CType(resources.GetObject("btnSetShowFanartLocal.Image"), System.Drawing.Image)
        Me.btnSetShowFanartLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowFanartLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetShowFanartLocal.Name = "btnSetShowFanartLocal"
        Me.btnSetShowFanartLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowFanartLocal.TabIndex = 1
        Me.btnSetShowFanartLocal.Text = "Change Fanart (Local)"
        Me.btnSetShowFanartLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowFanartLocal.UseVisualStyleBackColor = True
        '
        'btnSetShowFanartScrape
        '
        Me.btnSetShowFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowFanartScrape.Image = CType(resources.GetObject("btnSetShowFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetShowFanartScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowFanartScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetShowFanartScrape.Name = "btnSetShowFanartScrape"
        Me.btnSetShowFanartScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowFanartScrape.TabIndex = 2
        Me.btnSetShowFanartScrape.Text = "Change Fanart (Scrape)"
        Me.btnSetShowFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowFanartScrape.UseVisualStyleBackColor = True
        '
        'lblShowFanartSize
        '
        Me.lblShowFanartSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShowFanartSize.Location = New System.Drawing.Point(8, 8)
        Me.lblShowFanartSize.Name = "lblShowFanartSize"
        Me.lblShowFanartSize.Size = New System.Drawing.Size(104, 23)
        Me.lblShowFanartSize.TabIndex = 0
        Me.lblShowFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblShowFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblShowFanartSize.Visible = False
        '
        'btnRemoveShowFanart
        '
        Me.btnRemoveShowFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveShowFanart.Image = CType(resources.GetObject("btnRemoveShowFanart.Image"), System.Drawing.Image)
        Me.btnRemoveShowFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveShowFanart.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveShowFanart.Name = "btnRemoveShowFanart"
        Me.btnRemoveShowFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveShowFanart.TabIndex = 4
        Me.btnRemoveShowFanart.Text = "Remove Fanart"
        Me.btnRemoveShowFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveShowFanart.UseVisualStyleBackColor = True
        '
        'btnSetShowFanartDL
        '
        Me.btnSetShowFanartDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowFanartDL.Image = CType(resources.GetObject("btnSetShowFanartDL.Image"), System.Drawing.Image)
        Me.btnSetShowFanartDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowFanartDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetShowFanartDL.Name = "btnSetShowFanartDL"
        Me.btnSetShowFanartDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowFanartDL.TabIndex = 3
        Me.btnSetShowFanartDL.Text = "Change Fanart (Download)"
        Me.btnSetShowFanartDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowFanartDL.UseVisualStyleBackColor = True
        '
        'tpShowClearLogo
        '
        Me.tpShowClearLogo.Controls.Add(Me.lblShowClearLogoSize)
        Me.tpShowClearLogo.Controls.Add(Me.btnSetShowClearLogoDL)
        Me.tpShowClearLogo.Controls.Add(Me.btnRemoveShowClearLogo)
        Me.tpShowClearLogo.Controls.Add(Me.btnSetShowClearLogoScrape)
        Me.tpShowClearLogo.Controls.Add(Me.btnSetShowClearLogoLocal)
        Me.tpShowClearLogo.Controls.Add(Me.pbShowClearLogo)
        Me.tpShowClearLogo.Location = New System.Drawing.Point(4, 22)
        Me.tpShowClearLogo.Name = "tpShowClearLogo"
        Me.tpShowClearLogo.Size = New System.Drawing.Size(836, 463)
        Me.tpShowClearLogo.TabIndex = 11
        Me.tpShowClearLogo.Text = "ClearLogo"
        Me.tpShowClearLogo.UseVisualStyleBackColor = True
        '
        'pbShowClearLogo
        '
        Me.pbShowClearLogo.BackColor = System.Drawing.Color.DimGray
        Me.pbShowClearLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbShowClearLogo.Location = New System.Drawing.Point(6, 6)
        Me.pbShowClearLogo.Name = "pbShowClearLogo"
        Me.pbShowClearLogo.Size = New System.Drawing.Size(724, 440)
        Me.pbShowClearLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbShowClearLogo.TabIndex = 12
        Me.pbShowClearLogo.TabStop = False
        '
        'btnSetShowClearLogoLocal
        '
        Me.btnSetShowClearLogoLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowClearLogoLocal.Image = CType(resources.GetObject("btnSetShowClearLogoLocal.Image"), System.Drawing.Image)
        Me.btnSetShowClearLogoLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowClearLogoLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetShowClearLogoLocal.Name = "btnSetShowClearLogoLocal"
        Me.btnSetShowClearLogoLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowClearLogoLocal.TabIndex = 13
        Me.btnSetShowClearLogoLocal.Text = "Change ClearLogo (Local Browse)"
        Me.btnSetShowClearLogoLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowClearLogoLocal.UseVisualStyleBackColor = True
        '
        'btnSetShowClearLogoScrape
        '
        Me.btnSetShowClearLogoScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowClearLogoScrape.Image = CType(resources.GetObject("btnSetShowClearLogoScrape.Image"), System.Drawing.Image)
        Me.btnSetShowClearLogoScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowClearLogoScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetShowClearLogoScrape.Name = "btnSetShowClearLogoScrape"
        Me.btnSetShowClearLogoScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowClearLogoScrape.TabIndex = 14
        Me.btnSetShowClearLogoScrape.Text = "Change ClearLogo (Scrape)"
        Me.btnSetShowClearLogoScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowClearLogoScrape.UseVisualStyleBackColor = True
        '
        'btnRemoveShowClearLogo
        '
        Me.btnRemoveShowClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveShowClearLogo.Image = CType(resources.GetObject("btnRemoveShowClearLogo.Image"), System.Drawing.Image)
        Me.btnRemoveShowClearLogo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveShowClearLogo.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveShowClearLogo.Name = "btnRemoveShowClearLogo"
        Me.btnRemoveShowClearLogo.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveShowClearLogo.TabIndex = 16
        Me.btnRemoveShowClearLogo.Text = "Remove ClearLogo"
        Me.btnRemoveShowClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveShowClearLogo.UseVisualStyleBackColor = True
        '
        'btnSetShowClearLogoDL
        '
        Me.btnSetShowClearLogoDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowClearLogoDL.Image = CType(resources.GetObject("btnSetShowClearLogoDL.Image"), System.Drawing.Image)
        Me.btnSetShowClearLogoDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowClearLogoDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetShowClearLogoDL.Name = "btnSetShowClearLogoDL"
        Me.btnSetShowClearLogoDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowClearLogoDL.TabIndex = 15
        Me.btnSetShowClearLogoDL.Text = "Change ClearLogo (Download)"
        Me.btnSetShowClearLogoDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowClearLogoDL.UseVisualStyleBackColor = True
        '
        'lblShowClearLogoSize
        '
        Me.lblShowClearLogoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShowClearLogoSize.Location = New System.Drawing.Point(8, 8)
        Me.lblShowClearLogoSize.Name = "lblShowClearLogoSize"
        Me.lblShowClearLogoSize.Size = New System.Drawing.Size(104, 23)
        Me.lblShowClearLogoSize.TabIndex = 17
        Me.lblShowClearLogoSize.Text = "Size: (XXXXxXXXX)"
        Me.lblShowClearLogoSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblShowClearLogoSize.Visible = False
        '
        'tpShowClearArt
        '
        Me.tpShowClearArt.Controls.Add(Me.lblShowClearArtSize)
        Me.tpShowClearArt.Controls.Add(Me.btnSetShowClearArtDL)
        Me.tpShowClearArt.Controls.Add(Me.btnRemoveShowClearArt)
        Me.tpShowClearArt.Controls.Add(Me.btnSetShowClearArtScrape)
        Me.tpShowClearArt.Controls.Add(Me.btnSetShowClearArtLocal)
        Me.tpShowClearArt.Controls.Add(Me.pbShowClearArt)
        Me.tpShowClearArt.Location = New System.Drawing.Point(4, 22)
        Me.tpShowClearArt.Name = "tpShowClearArt"
        Me.tpShowClearArt.Size = New System.Drawing.Size(836, 463)
        Me.tpShowClearArt.TabIndex = 10
        Me.tpShowClearArt.Text = "ClearArt"
        Me.tpShowClearArt.UseVisualStyleBackColor = True
        '
        'pbShowClearArt
        '
        Me.pbShowClearArt.BackColor = System.Drawing.Color.DimGray
        Me.pbShowClearArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbShowClearArt.Location = New System.Drawing.Point(6, 6)
        Me.pbShowClearArt.Name = "pbShowClearArt"
        Me.pbShowClearArt.Size = New System.Drawing.Size(724, 440)
        Me.pbShowClearArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbShowClearArt.TabIndex = 12
        Me.pbShowClearArt.TabStop = False
        '
        'btnSetShowClearArtLocal
        '
        Me.btnSetShowClearArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowClearArtLocal.Image = CType(resources.GetObject("btnSetShowClearArtLocal.Image"), System.Drawing.Image)
        Me.btnSetShowClearArtLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowClearArtLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetShowClearArtLocal.Name = "btnSetShowClearArtLocal"
        Me.btnSetShowClearArtLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowClearArtLocal.TabIndex = 13
        Me.btnSetShowClearArtLocal.Text = "Change ClearArt (Local Browse)"
        Me.btnSetShowClearArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowClearArtLocal.UseVisualStyleBackColor = True
        '
        'btnSetShowClearArtScrape
        '
        Me.btnSetShowClearArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowClearArtScrape.Image = CType(resources.GetObject("btnSetShowClearArtScrape.Image"), System.Drawing.Image)
        Me.btnSetShowClearArtScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowClearArtScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetShowClearArtScrape.Name = "btnSetShowClearArtScrape"
        Me.btnSetShowClearArtScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowClearArtScrape.TabIndex = 14
        Me.btnSetShowClearArtScrape.Text = "Change ClearArt (Scrape)"
        Me.btnSetShowClearArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowClearArtScrape.UseVisualStyleBackColor = True
        '
        'btnRemoveShowClearArt
        '
        Me.btnRemoveShowClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveShowClearArt.Image = CType(resources.GetObject("btnRemoveShowClearArt.Image"), System.Drawing.Image)
        Me.btnRemoveShowClearArt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveShowClearArt.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveShowClearArt.Name = "btnRemoveShowClearArt"
        Me.btnRemoveShowClearArt.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveShowClearArt.TabIndex = 16
        Me.btnRemoveShowClearArt.Text = "Remove ClearArt"
        Me.btnRemoveShowClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveShowClearArt.UseVisualStyleBackColor = True
        '
        'btnSetShowClearArtDL
        '
        Me.btnSetShowClearArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowClearArtDL.Image = CType(resources.GetObject("btnSetShowClearArtDL.Image"), System.Drawing.Image)
        Me.btnSetShowClearArtDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowClearArtDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetShowClearArtDL.Name = "btnSetShowClearArtDL"
        Me.btnSetShowClearArtDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowClearArtDL.TabIndex = 15
        Me.btnSetShowClearArtDL.Text = "Change ClearArt (Download)"
        Me.btnSetShowClearArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowClearArtDL.UseVisualStyleBackColor = True
        '
        'lblShowClearArtSize
        '
        Me.lblShowClearArtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShowClearArtSize.Location = New System.Drawing.Point(8, 8)
        Me.lblShowClearArtSize.Name = "lblShowClearArtSize"
        Me.lblShowClearArtSize.Size = New System.Drawing.Size(104, 23)
        Me.lblShowClearArtSize.TabIndex = 17
        Me.lblShowClearArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblShowClearArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblShowClearArtSize.Visible = False
        '
        'tpShowCharacterArt
        '
        Me.tpShowCharacterArt.Controls.Add(Me.lblShowCharacterArtSize)
        Me.tpShowCharacterArt.Controls.Add(Me.btnSetShowCharacterArtDL)
        Me.tpShowCharacterArt.Controls.Add(Me.btnRemoveShowCharacterArt)
        Me.tpShowCharacterArt.Controls.Add(Me.btnSetShowCharacterArtScrape)
        Me.tpShowCharacterArt.Controls.Add(Me.btnSetShowCharacterArtLocal)
        Me.tpShowCharacterArt.Controls.Add(Me.pbShowCharacterArt)
        Me.tpShowCharacterArt.Location = New System.Drawing.Point(4, 22)
        Me.tpShowCharacterArt.Name = "tpShowCharacterArt"
        Me.tpShowCharacterArt.Size = New System.Drawing.Size(836, 463)
        Me.tpShowCharacterArt.TabIndex = 9
        Me.tpShowCharacterArt.Text = "CharacterArt"
        Me.tpShowCharacterArt.UseVisualStyleBackColor = True
        '
        'pbShowCharacterArt
        '
        Me.pbShowCharacterArt.BackColor = System.Drawing.Color.DimGray
        Me.pbShowCharacterArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbShowCharacterArt.Location = New System.Drawing.Point(6, 6)
        Me.pbShowCharacterArt.Name = "pbShowCharacterArt"
        Me.pbShowCharacterArt.Size = New System.Drawing.Size(724, 440)
        Me.pbShowCharacterArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbShowCharacterArt.TabIndex = 18
        Me.pbShowCharacterArt.TabStop = False
        '
        'btnSetShowCharacterArtLocal
        '
        Me.btnSetShowCharacterArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowCharacterArtLocal.Image = CType(resources.GetObject("btnSetShowCharacterArtLocal.Image"), System.Drawing.Image)
        Me.btnSetShowCharacterArtLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowCharacterArtLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetShowCharacterArtLocal.Name = "btnSetShowCharacterArtLocal"
        Me.btnSetShowCharacterArtLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowCharacterArtLocal.TabIndex = 19
        Me.btnSetShowCharacterArtLocal.Text = "Change CharacterArt (Local Browse)"
        Me.btnSetShowCharacterArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowCharacterArtLocal.UseVisualStyleBackColor = True
        '
        'btnSetShowCharacterArtScrape
        '
        Me.btnSetShowCharacterArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowCharacterArtScrape.Image = CType(resources.GetObject("btnSetShowCharacterArtScrape.Image"), System.Drawing.Image)
        Me.btnSetShowCharacterArtScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowCharacterArtScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetShowCharacterArtScrape.Name = "btnSetShowCharacterArtScrape"
        Me.btnSetShowCharacterArtScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowCharacterArtScrape.TabIndex = 20
        Me.btnSetShowCharacterArtScrape.Text = "Change CharacterArt (Scrape)"
        Me.btnSetShowCharacterArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowCharacterArtScrape.UseVisualStyleBackColor = True
        '
        'btnRemoveShowCharacterArt
        '
        Me.btnRemoveShowCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveShowCharacterArt.Image = CType(resources.GetObject("btnRemoveShowCharacterArt.Image"), System.Drawing.Image)
        Me.btnRemoveShowCharacterArt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveShowCharacterArt.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveShowCharacterArt.Name = "btnRemoveShowCharacterArt"
        Me.btnRemoveShowCharacterArt.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveShowCharacterArt.TabIndex = 22
        Me.btnRemoveShowCharacterArt.Text = "Remove CharacterArt"
        Me.btnRemoveShowCharacterArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveShowCharacterArt.UseVisualStyleBackColor = True
        '
        'btnSetShowCharacterArtDL
        '
        Me.btnSetShowCharacterArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetShowCharacterArtDL.Image = CType(resources.GetObject("btnSetShowCharacterArtDL.Image"), System.Drawing.Image)
        Me.btnSetShowCharacterArtDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowCharacterArtDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetShowCharacterArtDL.Name = "btnSetShowCharacterArtDL"
        Me.btnSetShowCharacterArtDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowCharacterArtDL.TabIndex = 21
        Me.btnSetShowCharacterArtDL.Text = "Change CharacterArt (Download)"
        Me.btnSetShowCharacterArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowCharacterArtDL.UseVisualStyleBackColor = True
        '
        'lblShowCharacterArtSize
        '
        Me.lblShowCharacterArtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShowCharacterArtSize.Location = New System.Drawing.Point(8, 8)
        Me.lblShowCharacterArtSize.Name = "lblShowCharacterArtSize"
        Me.lblShowCharacterArtSize.Size = New System.Drawing.Size(104, 23)
        Me.lblShowCharacterArtSize.TabIndex = 23
        Me.lblShowCharacterArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblShowCharacterArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblShowCharacterArtSize.Visible = False
        '
        'tpShowLandscape
        '
        Me.tpShowLandscape.Controls.Add(Me.btnSetShowLandscapeDL)
        Me.tpShowLandscape.Controls.Add(Me.btnRemoveShowLandscape)
        Me.tpShowLandscape.Controls.Add(Me.lblShowLandscapeSize)
        Me.tpShowLandscape.Controls.Add(Me.btnSetShowLandscapeScrape)
        Me.tpShowLandscape.Controls.Add(Me.btnSetShowLandscapeLocal)
        Me.tpShowLandscape.Controls.Add(Me.pbShowLandscape)
        Me.tpShowLandscape.Location = New System.Drawing.Point(4, 22)
        Me.tpShowLandscape.Name = "tpShowLandscape"
        Me.tpShowLandscape.Size = New System.Drawing.Size(836, 463)
        Me.tpShowLandscape.TabIndex = 7
        Me.tpShowLandscape.Text = "Landscape"
        Me.tpShowLandscape.UseVisualStyleBackColor = True
        '
        'pbShowLandscape
        '
        Me.pbShowLandscape.BackColor = System.Drawing.Color.DimGray
        Me.pbShowLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbShowLandscape.Location = New System.Drawing.Point(6, 6)
        Me.pbShowLandscape.Name = "pbShowLandscape"
        Me.pbShowLandscape.Size = New System.Drawing.Size(724, 440)
        Me.pbShowLandscape.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbShowLandscape.TabIndex = 6
        Me.pbShowLandscape.TabStop = False
        '
        'btnSetShowLandscapeLocal
        '
        Me.btnSetShowLandscapeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowLandscapeLocal.Image = CType(resources.GetObject("btnSetShowLandscapeLocal.Image"), System.Drawing.Image)
        Me.btnSetShowLandscapeLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowLandscapeLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetShowLandscapeLocal.Name = "btnSetShowLandscapeLocal"
        Me.btnSetShowLandscapeLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowLandscapeLocal.TabIndex = 7
        Me.btnSetShowLandscapeLocal.Text = "Change Landscape (Local)"
        Me.btnSetShowLandscapeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowLandscapeLocal.UseVisualStyleBackColor = True
        '
        'btnSetShowLandscapeScrape
        '
        Me.btnSetShowLandscapeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowLandscapeScrape.Image = CType(resources.GetObject("btnSetShowLandscapeScrape.Image"), System.Drawing.Image)
        Me.btnSetShowLandscapeScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowLandscapeScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetShowLandscapeScrape.Name = "btnSetShowLandscapeScrape"
        Me.btnSetShowLandscapeScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowLandscapeScrape.TabIndex = 8
        Me.btnSetShowLandscapeScrape.Text = "Change Landscape (Scrape)"
        Me.btnSetShowLandscapeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowLandscapeScrape.UseVisualStyleBackColor = True
        '
        'lblShowLandscapeSize
        '
        Me.lblShowLandscapeSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShowLandscapeSize.Location = New System.Drawing.Point(8, 8)
        Me.lblShowLandscapeSize.Name = "lblShowLandscapeSize"
        Me.lblShowLandscapeSize.Size = New System.Drawing.Size(104, 23)
        Me.lblShowLandscapeSize.TabIndex = 5
        Me.lblShowLandscapeSize.Text = "Size: (XXXXxXXXX)"
        Me.lblShowLandscapeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblShowLandscapeSize.Visible = False
        '
        'btnRemoveShowLandscape
        '
        Me.btnRemoveShowLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveShowLandscape.Image = CType(resources.GetObject("btnRemoveShowLandscape.Image"), System.Drawing.Image)
        Me.btnRemoveShowLandscape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveShowLandscape.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveShowLandscape.Name = "btnRemoveShowLandscape"
        Me.btnRemoveShowLandscape.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveShowLandscape.TabIndex = 10
        Me.btnRemoveShowLandscape.Text = "Remove Landscape"
        Me.btnRemoveShowLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveShowLandscape.UseVisualStyleBackColor = True
        '
        'btnSetShowLandscapeDL
        '
        Me.btnSetShowLandscapeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowLandscapeDL.Image = CType(resources.GetObject("btnSetShowLandscapeDL.Image"), System.Drawing.Image)
        Me.btnSetShowLandscapeDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowLandscapeDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetShowLandscapeDL.Name = "btnSetShowLandscapeDL"
        Me.btnSetShowLandscapeDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowLandscapeDL.TabIndex = 9
        Me.btnSetShowLandscapeDL.Text = "Change Landscape (Download)"
        Me.btnSetShowLandscapeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowLandscapeDL.UseVisualStyleBackColor = True
        '
        'tpShowBanner
        '
        Me.tpShowBanner.Controls.Add(Me.btnSetShowBannerDL)
        Me.tpShowBanner.Controls.Add(Me.btnRemoveShowBanner)
        Me.tpShowBanner.Controls.Add(Me.lblShowBannerSize)
        Me.tpShowBanner.Controls.Add(Me.btnSetShowBannerScrape)
        Me.tpShowBanner.Controls.Add(Me.btnSetShowBannerLocal)
        Me.tpShowBanner.Controls.Add(Me.pbShowBanner)
        Me.tpShowBanner.Location = New System.Drawing.Point(4, 22)
        Me.tpShowBanner.Name = "tpShowBanner"
        Me.tpShowBanner.Size = New System.Drawing.Size(836, 463)
        Me.tpShowBanner.TabIndex = 4
        Me.tpShowBanner.Text = "Banner"
        Me.tpShowBanner.UseVisualStyleBackColor = True
        '
        'pbShowBanner
        '
        Me.pbShowBanner.BackColor = System.Drawing.Color.DimGray
        Me.pbShowBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbShowBanner.Location = New System.Drawing.Point(6, 6)
        Me.pbShowBanner.Name = "pbShowBanner"
        Me.pbShowBanner.Size = New System.Drawing.Size(724, 440)
        Me.pbShowBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbShowBanner.TabIndex = 6
        Me.pbShowBanner.TabStop = False
        '
        'btnSetShowBannerLocal
        '
        Me.btnSetShowBannerLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowBannerLocal.Image = CType(resources.GetObject("btnSetShowBannerLocal.Image"), System.Drawing.Image)
        Me.btnSetShowBannerLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowBannerLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetShowBannerLocal.Name = "btnSetShowBannerLocal"
        Me.btnSetShowBannerLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowBannerLocal.TabIndex = 7
        Me.btnSetShowBannerLocal.Text = "Change Banner (Local)"
        Me.btnSetShowBannerLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowBannerLocal.UseVisualStyleBackColor = True
        '
        'btnSetShowBannerScrape
        '
        Me.btnSetShowBannerScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowBannerScrape.Image = CType(resources.GetObject("btnSetShowBannerScrape.Image"), System.Drawing.Image)
        Me.btnSetShowBannerScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowBannerScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetShowBannerScrape.Name = "btnSetShowBannerScrape"
        Me.btnSetShowBannerScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowBannerScrape.TabIndex = 8
        Me.btnSetShowBannerScrape.Text = "Change Banner (Scrape)"
        Me.btnSetShowBannerScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowBannerScrape.UseVisualStyleBackColor = True
        '
        'lblShowBannerSize
        '
        Me.lblShowBannerSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShowBannerSize.Location = New System.Drawing.Point(8, 8)
        Me.lblShowBannerSize.Name = "lblShowBannerSize"
        Me.lblShowBannerSize.Size = New System.Drawing.Size(104, 23)
        Me.lblShowBannerSize.TabIndex = 5
        Me.lblShowBannerSize.Text = "Size: (XXXXxXXXX)"
        Me.lblShowBannerSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblShowBannerSize.Visible = False
        '
        'btnRemoveShowBanner
        '
        Me.btnRemoveShowBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveShowBanner.Image = CType(resources.GetObject("btnRemoveShowBanner.Image"), System.Drawing.Image)
        Me.btnRemoveShowBanner.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveShowBanner.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveShowBanner.Name = "btnRemoveShowBanner"
        Me.btnRemoveShowBanner.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveShowBanner.TabIndex = 10
        Me.btnRemoveShowBanner.Text = "Remove Banner"
        Me.btnRemoveShowBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveShowBanner.UseVisualStyleBackColor = True
        '
        'btnSetShowBannerDL
        '
        Me.btnSetShowBannerDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowBannerDL.Image = CType(resources.GetObject("btnSetShowBannerDL.Image"), System.Drawing.Image)
        Me.btnSetShowBannerDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowBannerDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetShowBannerDL.Name = "btnSetShowBannerDL"
        Me.btnSetShowBannerDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowBannerDL.TabIndex = 9
        Me.btnSetShowBannerDL.Text = "Change Banner (Download)"
        Me.btnSetShowBannerDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowBannerDL.UseVisualStyleBackColor = True
        '
        'tpShowPoster
        '
        Me.tpShowPoster.Controls.Add(Me.btnSetShowPosterDL)
        Me.tpShowPoster.Controls.Add(Me.btnRemoveShowPoster)
        Me.tpShowPoster.Controls.Add(Me.lblShowPosterSize)
        Me.tpShowPoster.Controls.Add(Me.btnSetShowPosterScrape)
        Me.tpShowPoster.Controls.Add(Me.btnSetShowPosterLocal)
        Me.tpShowPoster.Controls.Add(Me.pbShowPoster)
        Me.tpShowPoster.Location = New System.Drawing.Point(4, 22)
        Me.tpShowPoster.Name = "tpShowPoster"
        Me.tpShowPoster.Padding = New System.Windows.Forms.Padding(3)
        Me.tpShowPoster.Size = New System.Drawing.Size(836, 463)
        Me.tpShowPoster.TabIndex = 1
        Me.tpShowPoster.Text = "Poster"
        Me.tpShowPoster.UseVisualStyleBackColor = True
        '
        'pbShowPoster
        '
        Me.pbShowPoster.BackColor = System.Drawing.Color.DimGray
        Me.pbShowPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbShowPoster.Location = New System.Drawing.Point(6, 6)
        Me.pbShowPoster.Name = "pbShowPoster"
        Me.pbShowPoster.Size = New System.Drawing.Size(724, 440)
        Me.pbShowPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbShowPoster.TabIndex = 0
        Me.pbShowPoster.TabStop = False
        '
        'btnSetShowPosterLocal
        '
        Me.btnSetShowPosterLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowPosterLocal.Image = CType(resources.GetObject("btnSetShowPosterLocal.Image"), System.Drawing.Image)
        Me.btnSetShowPosterLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowPosterLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetShowPosterLocal.Name = "btnSetShowPosterLocal"
        Me.btnSetShowPosterLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowPosterLocal.TabIndex = 1
        Me.btnSetShowPosterLocal.Text = "Change Poster (Local)"
        Me.btnSetShowPosterLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowPosterLocal.UseVisualStyleBackColor = True
        '
        'btnSetShowPosterScrape
        '
        Me.btnSetShowPosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowPosterScrape.Image = CType(resources.GetObject("btnSetShowPosterScrape.Image"), System.Drawing.Image)
        Me.btnSetShowPosterScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowPosterScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetShowPosterScrape.Name = "btnSetShowPosterScrape"
        Me.btnSetShowPosterScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowPosterScrape.TabIndex = 2
        Me.btnSetShowPosterScrape.Text = "Change Poster (Scrape)"
        Me.btnSetShowPosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowPosterScrape.UseVisualStyleBackColor = True
        '
        'lblShowPosterSize
        '
        Me.lblShowPosterSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblShowPosterSize.Location = New System.Drawing.Point(8, 8)
        Me.lblShowPosterSize.Name = "lblShowPosterSize"
        Me.lblShowPosterSize.Size = New System.Drawing.Size(104, 23)
        Me.lblShowPosterSize.TabIndex = 0
        Me.lblShowPosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblShowPosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblShowPosterSize.Visible = False
        '
        'btnRemoveShowPoster
        '
        Me.btnRemoveShowPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveShowPoster.Image = CType(resources.GetObject("btnRemoveShowPoster.Image"), System.Drawing.Image)
        Me.btnRemoveShowPoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveShowPoster.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveShowPoster.Name = "btnRemoveShowPoster"
        Me.btnRemoveShowPoster.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveShowPoster.TabIndex = 4
        Me.btnRemoveShowPoster.Text = "Remove Poster"
        Me.btnRemoveShowPoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveShowPoster.UseVisualStyleBackColor = True
        '
        'btnSetShowPosterDL
        '
        Me.btnSetShowPosterDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetShowPosterDL.Image = CType(resources.GetObject("btnSetShowPosterDL.Image"), System.Drawing.Image)
        Me.btnSetShowPosterDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetShowPosterDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetShowPosterDL.Name = "btnSetShowPosterDL"
        Me.btnSetShowPosterDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetShowPosterDL.TabIndex = 3
        Me.btnSetShowPosterDL.Text = "Change Poster (Download)"
        Me.btnSetShowPosterDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetShowPosterDL.UseVisualStyleBackColor = True
        '
        'tpShowDetails
        '
        Me.tpShowDetails.Controls.Add(Me.txtMPAA)
        Me.tpShowDetails.Controls.Add(Me.txtVotes)
        Me.tpShowDetails.Controls.Add(Me.txtRuntime)
        Me.tpShowDetails.Controls.Add(Me.txtStatus)
        Me.tpShowDetails.Controls.Add(Me.txtPremiered)
        Me.tpShowDetails.Controls.Add(Me.txtStudio)
        Me.tpShowDetails.Controls.Add(Me.lblVotes)
        Me.tpShowDetails.Controls.Add(Me.lblRuntime)
        Me.tpShowDetails.Controls.Add(Me.pbStar10)
        Me.tpShowDetails.Controls.Add(Me.pbStar9)
        Me.tpShowDetails.Controls.Add(Me.pbStar8)
        Me.tpShowDetails.Controls.Add(Me.pbStar7)
        Me.tpShowDetails.Controls.Add(Me.pbStar6)
        Me.tpShowDetails.Controls.Add(Me.lblStatus)
        Me.tpShowDetails.Controls.Add(Me.btnActorDown)
        Me.tpShowDetails.Controls.Add(Me.btnActorUp)
        Me.tpShowDetails.Controls.Add(Me.clbGenre)
        Me.tpShowDetails.Controls.Add(Me.lblStudio)
        Me.tpShowDetails.Controls.Add(Me.btnEditActor)
        Me.tpShowDetails.Controls.Add(Me.btnAddActor)
        Me.tpShowDetails.Controls.Add(Me.btnManual)
        Me.tpShowDetails.Controls.Add(Me.btnRemove)
        Me.tpShowDetails.Controls.Add(Me.lblActors)
        Me.tpShowDetails.Controls.Add(Me.lvActors)
        Me.tpShowDetails.Controls.Add(Me.txtPlot)
        Me.tpShowDetails.Controls.Add(Me.txtSortTitle)
        Me.tpShowDetails.Controls.Add(Me.txtTitle)
        Me.tpShowDetails.Controls.Add(Me.lbMPAA)
        Me.tpShowDetails.Controls.Add(Me.lblGenre)
        Me.tpShowDetails.Controls.Add(Me.lblMPAA)
        Me.tpShowDetails.Controls.Add(Me.lblPlot)
        Me.tpShowDetails.Controls.Add(Me.pbStar5)
        Me.tpShowDetails.Controls.Add(Me.pbStar4)
        Me.tpShowDetails.Controls.Add(Me.pbStar3)
        Me.tpShowDetails.Controls.Add(Me.pbStar2)
        Me.tpShowDetails.Controls.Add(Me.pbStar1)
        Me.tpShowDetails.Controls.Add(Me.lblRating)
        Me.tpShowDetails.Controls.Add(Me.lblPremiered)
        Me.tpShowDetails.Controls.Add(Me.lblSortTitle)
        Me.tpShowDetails.Controls.Add(Me.lblTitle)
        Me.tpShowDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpShowDetails.Name = "tpShowDetails"
        Me.tpShowDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpShowDetails.Size = New System.Drawing.Size(836, 463)
        Me.tpShowDetails.TabIndex = 0
        Me.tpShowDetails.Text = "Details"
        Me.tpShowDetails.UseVisualStyleBackColor = True
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(6, 23)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtTitle.TabIndex = 1
        '
        'txtSortTitle
        '
        Me.txtSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSortTitle.Location = New System.Drawing.Point(6, 64)
        Me.txtSortTitle.Name = "txtSortTitle"
        Me.txtSortTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtSortTitle.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(6, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(32, 13)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Title:"
        '
        'lblSortTitle
        '
        Me.lblSortTitle.AutoSize = True
        Me.lblSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSortTitle.Location = New System.Drawing.Point(6, 48)
        Me.lblSortTitle.Name = "lblSortTitle"
        Me.lblSortTitle.Size = New System.Drawing.Size(56, 13)
        Me.lblSortTitle.TabIndex = 0
        Me.lblSortTitle.Text = "Sort Title:"
        '
        'lblPremiered
        '
        Me.lblPremiered.AutoSize = True
        Me.lblPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPremiered.Location = New System.Drawing.Point(6, 89)
        Me.lblPremiered.Name = "lblPremiered"
        Me.lblPremiered.Size = New System.Drawing.Size(63, 13)
        Me.lblPremiered.TabIndex = 2
        Me.lblPremiered.Text = "Premiered:"
        '
        'lblRating
        '
        Me.lblRating.AutoSize = True
        Me.lblRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRating.Location = New System.Drawing.Point(6, 130)
        Me.lblRating.Name = "lblRating"
        Me.lblRating.Size = New System.Drawing.Size(44, 13)
        Me.lblRating.TabIndex = 4
        Me.lblRating.Text = "Rating:"
        '
        'pbStar1
        '
        Me.pbStar1.Location = New System.Drawing.Point(6, 146)
        Me.pbStar1.Name = "pbStar1"
        Me.pbStar1.Size = New System.Drawing.Size(24, 24)
        Me.pbStar1.TabIndex = 63
        Me.pbStar1.TabStop = False
        '
        'pbStar2
        '
        Me.pbStar2.Location = New System.Drawing.Point(30, 146)
        Me.pbStar2.Name = "pbStar2"
        Me.pbStar2.Size = New System.Drawing.Size(24, 24)
        Me.pbStar2.TabIndex = 64
        Me.pbStar2.TabStop = False
        '
        'pbStar3
        '
        Me.pbStar3.Location = New System.Drawing.Point(54, 146)
        Me.pbStar3.Name = "pbStar3"
        Me.pbStar3.Size = New System.Drawing.Size(24, 24)
        Me.pbStar3.TabIndex = 65
        Me.pbStar3.TabStop = False
        '
        'pbStar4
        '
        Me.pbStar4.Location = New System.Drawing.Point(78, 146)
        Me.pbStar4.Name = "pbStar4"
        Me.pbStar4.Size = New System.Drawing.Size(24, 24)
        Me.pbStar4.TabIndex = 66
        Me.pbStar4.TabStop = False
        '
        'pbStar5
        '
        Me.pbStar5.Location = New System.Drawing.Point(102, 146)
        Me.pbStar5.Name = "pbStar5"
        Me.pbStar5.Size = New System.Drawing.Size(24, 24)
        Me.pbStar5.TabIndex = 67
        Me.pbStar5.TabStop = False
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(268, 26)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.Size = New System.Drawing.Size(560, 108)
        Me.txtPlot.TabIndex = 8
        '
        'lblPlot
        '
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(265, 7)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(31, 13)
        Me.lblPlot.TabIndex = 7
        Me.lblPlot.Text = "Plot:"
        '
        'lblMPAA
        '
        Me.lblMPAA.AutoSize = True
        Me.lblMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMPAA.Location = New System.Drawing.Point(632, 139)
        Me.lblMPAA.Name = "lblMPAA"
        Me.lblMPAA.Size = New System.Drawing.Size(80, 13)
        Me.lblMPAA.TabIndex = 16
        Me.lblMPAA.Text = "MPAA Rating:"
        '
        'lblGenre
        '
        Me.lblGenre.AutoSize = True
        Me.lblGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGenre.Location = New System.Drawing.Point(4, 173)
        Me.lblGenre.Name = "lblGenre"
        Me.lblGenre.Size = New System.Drawing.Size(41, 13)
        Me.lblGenre.TabIndex = 5
        Me.lblGenre.Text = "Genre:"
        '
        'lbMPAA
        '
        Me.lbMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMPAA.FormattingEnabled = True
        Me.lbMPAA.Location = New System.Drawing.Point(635, 155)
        Me.lbMPAA.Name = "lbMPAA"
        Me.lbMPAA.Size = New System.Drawing.Size(193, 108)
        Me.lbMPAA.TabIndex = 17
        '
        'lvActors
        '
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colName, Me.colRole, Me.colThumb})
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.Location = New System.Drawing.Point(217, 189)
        Me.lvActors.Name = "lvActors"
        Me.lvActors.Size = New System.Drawing.Size(408, 236)
        Me.lvActors.TabIndex = 10
        Me.lvActors.UseCompatibleStateImageBehavior = False
        Me.lvActors.View = System.Windows.Forms.View.Details
        '
        'colID
        '
        Me.colID.Text = "ID"
        Me.colID.Width = 0
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
        Me.lblActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblActors.Location = New System.Drawing.Point(214, 173)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(43, 13)
        Me.lblActors.TabIndex = 9
        Me.lblActors.Text = "Actors:"
        '
        'btnRemove
        '
        Me.btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), System.Drawing.Image)
        Me.btnRemove.Location = New System.Drawing.Point(602, 431)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnRemove.TabIndex = 15
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnManual
        '
        Me.btnManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManual.Location = New System.Drawing.Point(738, 434)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(92, 23)
        Me.btnManual.TabIndex = 20
        Me.btnManual.Text = "Manual Edit"
        Me.btnManual.UseVisualStyleBackColor = True
        '
        'btnAddActor
        '
        Me.btnAddActor.Image = CType(resources.GetObject("btnAddActor.Image"), System.Drawing.Image)
        Me.btnAddActor.Location = New System.Drawing.Point(217, 431)
        Me.btnAddActor.Name = "btnAddActor"
        Me.btnAddActor.Size = New System.Drawing.Size(23, 23)
        Me.btnAddActor.TabIndex = 11
        Me.btnAddActor.UseVisualStyleBackColor = True
        '
        'btnEditActor
        '
        Me.btnEditActor.Image = CType(resources.GetObject("btnEditActor.Image"), System.Drawing.Image)
        Me.btnEditActor.Location = New System.Drawing.Point(246, 431)
        Me.btnEditActor.Name = "btnEditActor"
        Me.btnEditActor.Size = New System.Drawing.Size(23, 23)
        Me.btnEditActor.TabIndex = 12
        Me.btnEditActor.UseVisualStyleBackColor = True
        '
        'txtStudio
        '
        Me.txtStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStudio.Location = New System.Drawing.Point(635, 321)
        Me.txtStudio.Name = "txtStudio"
        Me.txtStudio.Size = New System.Drawing.Size(193, 22)
        Me.txtStudio.TabIndex = 19
        '
        'lblStudio
        '
        Me.lblStudio.AutoSize = True
        Me.lblStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStudio.Location = New System.Drawing.Point(635, 305)
        Me.lblStudio.Name = "lblStudio"
        Me.lblStudio.Size = New System.Drawing.Size(44, 13)
        Me.lblStudio.TabIndex = 18
        Me.lblStudio.Text = "Studio:"
        '
        'clbGenre
        '
        Me.clbGenre.CheckOnClick = True
        Me.clbGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clbGenre.FormattingEnabled = True
        Me.clbGenre.IntegralHeight = False
        Me.clbGenre.Location = New System.Drawing.Point(7, 189)
        Me.clbGenre.Name = "clbGenre"
        Me.clbGenre.Size = New System.Drawing.Size(192, 268)
        Me.clbGenre.Sorted = True
        Me.clbGenre.TabIndex = 6
        '
        'txtPremiered
        '
        Me.txtPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPremiered.Location = New System.Drawing.Point(6, 105)
        Me.txtPremiered.Name = "txtPremiered"
        Me.txtPremiered.Size = New System.Drawing.Size(192, 22)
        Me.txtPremiered.TabIndex = 3
        '
        'btnActorUp
        '
        Me.btnActorUp.Image = CType(resources.GetObject("btnActorUp.Image"), System.Drawing.Image)
        Me.btnActorUp.Location = New System.Drawing.Point(410, 431)
        Me.btnActorUp.Name = "btnActorUp"
        Me.btnActorUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorUp.TabIndex = 13
        Me.btnActorUp.UseVisualStyleBackColor = True
        '
        'btnActorDown
        '
        Me.btnActorDown.Image = CType(resources.GetObject("btnActorDown.Image"), System.Drawing.Image)
        Me.btnActorDown.Location = New System.Drawing.Point(434, 431)
        Me.btnActorDown.Name = "btnActorDown"
        Me.btnActorDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorDown.TabIndex = 14
        Me.btnActorDown.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(635, 362)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(193, 22)
        Me.txtStatus.TabIndex = 69
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(635, 346)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(42, 13)
        Me.lblStatus.TabIndex = 68
        Me.lblStatus.Text = "Status:"
        '
        'pbStar6
        '
        Me.pbStar6.Location = New System.Drawing.Point(126, 146)
        Me.pbStar6.Name = "pbStar6"
        Me.pbStar6.Size = New System.Drawing.Size(24, 24)
        Me.pbStar6.TabIndex = 73
        Me.pbStar6.TabStop = False
        '
        'pbStar7
        '
        Me.pbStar7.Location = New System.Drawing.Point(150, 146)
        Me.pbStar7.Name = "pbStar7"
        Me.pbStar7.Size = New System.Drawing.Size(24, 24)
        Me.pbStar7.TabIndex = 74
        Me.pbStar7.TabStop = False
        '
        'pbStar8
        '
        Me.pbStar8.Location = New System.Drawing.Point(174, 146)
        Me.pbStar8.Name = "pbStar8"
        Me.pbStar8.Size = New System.Drawing.Size(24, 24)
        Me.pbStar8.TabIndex = 75
        Me.pbStar8.TabStop = False
        '
        'pbStar9
        '
        Me.pbStar9.Location = New System.Drawing.Point(198, 146)
        Me.pbStar9.Name = "pbStar9"
        Me.pbStar9.Size = New System.Drawing.Size(24, 24)
        Me.pbStar9.TabIndex = 76
        Me.pbStar9.TabStop = False
        '
        'pbStar10
        '
        Me.pbStar10.Location = New System.Drawing.Point(222, 146)
        Me.pbStar10.Name = "pbStar10"
        Me.pbStar10.Size = New System.Drawing.Size(24, 24)
        Me.pbStar10.TabIndex = 77
        Me.pbStar10.TabStop = False
        '
        'txtRuntime
        '
        Me.txtRuntime.BackColor = System.Drawing.SystemColors.Window
        Me.txtRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtRuntime.Location = New System.Drawing.Point(635, 403)
        Me.txtRuntime.Name = "txtRuntime"
        Me.txtRuntime.Size = New System.Drawing.Size(66, 22)
        Me.txtRuntime.TabIndex = 79
        '
        'lblRuntime
        '
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRuntime.Location = New System.Drawing.Point(635, 387)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(54, 13)
        Me.lblRuntime.TabIndex = 78
        Me.lblRuntime.Text = "Runtime:"
        '
        'lblVotes
        '
        Me.lblVotes.AutoSize = True
        Me.lblVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVotes.Location = New System.Drawing.Point(735, 388)
        Me.lblVotes.Name = "lblVotes"
        Me.lblVotes.Size = New System.Drawing.Size(38, 13)
        Me.lblVotes.TabIndex = 80
        Me.lblVotes.Text = "Votes:"
        '
        'txtVotes
        '
        Me.txtVotes.BackColor = System.Drawing.SystemColors.Window
        Me.txtVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVotes.Location = New System.Drawing.Point(738, 403)
        Me.txtVotes.Name = "txtVotes"
        Me.txtVotes.Size = New System.Drawing.Size(66, 22)
        Me.txtVotes.TabIndex = 81
        '
        'txtMPAA
        '
        Me.txtMPAA.BackColor = System.Drawing.SystemColors.Window
        Me.txtMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtMPAA.Location = New System.Drawing.Point(635, 269)
        Me.txtMPAA.Name = "txtMPAA"
        Me.txtMPAA.Size = New System.Drawing.Size(193, 22)
        Me.txtMPAA.TabIndex = 82
        '
        'tcEditShow
        '
        Me.tcEditShow.Controls.Add(Me.tpShowDetails)
        Me.tcEditShow.Controls.Add(Me.tpShowPoster)
        Me.tcEditShow.Controls.Add(Me.tpShowBanner)
        Me.tcEditShow.Controls.Add(Me.tpShowLandscape)
        Me.tcEditShow.Controls.Add(Me.tpShowCharacterArt)
        Me.tcEditShow.Controls.Add(Me.tpShowClearArt)
        Me.tcEditShow.Controls.Add(Me.tpShowClearLogo)
        Me.tcEditShow.Controls.Add(Me.tpShowFanart)
        Me.tcEditShow.Controls.Add(Me.tpShowEFanarts)
        Me.tcEditShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcEditShow.Location = New System.Drawing.Point(4, 70)
        Me.tcEditShow.Name = "tcEditShow"
        Me.tcEditShow.SelectedIndex = 0
        Me.tcEditShow.Size = New System.Drawing.Size(844, 489)
        Me.tcEditShow.TabIndex = 3
        '
        'dlgEditShow
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(853, 600)
        Me.Controls.Add(Me.lblEpisodeSorting)
        Me.Controls.Add(Me.cbEpisodeSorting)
        Me.Controls.Add(Me.lblOrdering)
        Me.Controls.Add(Me.cbOrdering)
        Me.Controls.Add(Me.tcEditShow)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditShow"
        Me.Text = "Edit Show"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowEFanarts.ResumeLayout(False)
        CType(Me.pbShowEFanarts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlShowEFanartsSetAsFanart.ResumeLayout(False)
        Me.tpShowFanart.ResumeLayout(False)
        CType(Me.pbShowFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowClearLogo.ResumeLayout(False)
        CType(Me.pbShowClearLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowClearArt.ResumeLayout(False)
        CType(Me.pbShowClearArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowCharacterArt.ResumeLayout(False)
        CType(Me.pbShowCharacterArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowLandscape.ResumeLayout(False)
        CType(Me.pbShowLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowBanner.ResumeLayout(False)
        CType(Me.pbShowBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowPoster.ResumeLayout(False)
        CType(Me.pbShowPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowDetails.ResumeLayout(False)
        Me.tpShowDetails.PerformLayout()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcEditShow.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cbOrdering As System.Windows.Forms.ComboBox
    Friend WithEvents lblOrdering As System.Windows.Forms.Label
    Friend WithEvents cbEpisodeSorting As System.Windows.Forms.ComboBox
    Friend WithEvents lblEpisodeSorting As System.Windows.Forms.Label
    Friend WithEvents tpShowEFanarts As System.Windows.Forms.TabPage
    Friend WithEvents lblShowEFanartsSize As System.Windows.Forms.Label
    Friend WithEvents pnlShowEFanartsSetAsFanart As System.Windows.Forms.Panel
    Friend WithEvents btnShowEFanartsSetAsFanart As System.Windows.Forms.Button
    Friend WithEvents pnlShowEFanartsBG As System.Windows.Forms.Panel
    Friend WithEvents btnShowEFanartsRefresh As System.Windows.Forms.Button
    Friend WithEvents btnShowEFanartsRemove As System.Windows.Forms.Button
    Friend WithEvents pbShowEFanarts As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetShowFanartDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowFanart As System.Windows.Forms.Button
    Friend WithEvents lblShowFanartSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowFanartLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowFanart As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowClearLogo As System.Windows.Forms.TabPage
    Friend WithEvents lblShowClearLogoSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowClearLogoDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowClearLogo As System.Windows.Forms.Button
    Friend WithEvents btnSetShowClearLogoScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowClearLogoLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowClearLogo As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowClearArt As System.Windows.Forms.TabPage
    Friend WithEvents lblShowClearArtSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowClearArtDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowClearArt As System.Windows.Forms.Button
    Friend WithEvents btnSetShowClearArtScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowClearArtLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowClearArt As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowCharacterArt As System.Windows.Forms.TabPage
    Friend WithEvents lblShowCharacterArtSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowCharacterArtDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowCharacterArt As System.Windows.Forms.Button
    Friend WithEvents btnSetShowCharacterArtScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowCharacterArtLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowCharacterArt As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowLandscape As System.Windows.Forms.TabPage
    Friend WithEvents btnSetShowLandscapeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowLandscape As System.Windows.Forms.Button
    Friend WithEvents lblShowLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowLandscapeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowLandscapeLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowBanner As System.Windows.Forms.TabPage
    Friend WithEvents btnSetShowBannerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowBanner As System.Windows.Forms.Button
    Friend WithEvents lblShowBannerSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowBannerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowBannerLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowBanner As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowPoster As System.Windows.Forms.TabPage
    Friend WithEvents btnSetShowPosterDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowPoster As System.Windows.Forms.Button
    Friend WithEvents lblShowPosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowPosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowPosterLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowPoster As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowDetails As System.Windows.Forms.TabPage
    Friend WithEvents txtMPAA As System.Windows.Forms.TextBox
    Friend WithEvents txtVotes As System.Windows.Forms.TextBox
    Friend WithEvents txtRuntime As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPremiered As System.Windows.Forms.TextBox
    Friend WithEvents txtStudio As System.Windows.Forms.TextBox
    Friend WithEvents lblVotes As System.Windows.Forms.Label
    Friend WithEvents lblRuntime As System.Windows.Forms.Label
    Friend WithEvents pbStar10 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar9 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar8 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar7 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar6 As System.Windows.Forms.PictureBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnActorDown As System.Windows.Forms.Button
    Friend WithEvents btnActorUp As System.Windows.Forms.Button
    Friend WithEvents clbGenre As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblStudio As System.Windows.Forms.Label
    Friend WithEvents btnEditActor As System.Windows.Forms.Button
    Friend WithEvents btnAddActor As System.Windows.Forms.Button
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents lblActors As System.Windows.Forms.Label
    Friend WithEvents lvActors As System.Windows.Forms.ListView
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRole As System.Windows.Forms.ColumnHeader
    Friend WithEvents colThumb As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents txtSortTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents lbMPAA As System.Windows.Forms.ListBox
    Friend WithEvents lblGenre As System.Windows.Forms.Label
    Friend WithEvents lblMPAA As System.Windows.Forms.Label
    Friend WithEvents lblPlot As System.Windows.Forms.Label
    Friend WithEvents pbStar5 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar4 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar3 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar2 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblRating As System.Windows.Forms.Label
    Friend WithEvents lblPremiered As System.Windows.Forms.Label
    Friend WithEvents lblSortTitle As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents tcEditShow As System.Windows.Forms.TabControl

End Class
