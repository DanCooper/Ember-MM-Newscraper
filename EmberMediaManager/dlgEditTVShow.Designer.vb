<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEditTVShow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditTVShow))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.ofdLocalFiles = New System.Windows.Forms.OpenFileDialog()
        Me.cbOrdering = New System.Windows.Forms.ComboBox()
        Me.lblOrdering = New System.Windows.Forms.Label()
        Me.cbEpisodeSorting = New System.Windows.Forms.ComboBox()
        Me.lblEpisodeSorting = New System.Windows.Forms.Label()
        Me.tpExtrafanarts = New System.Windows.Forms.TabPage()
        Me.lblExtrafanartsSize = New System.Windows.Forms.Label()
        Me.pnlShowExtrafanartsSetAsFanart = New System.Windows.Forms.Panel()
        Me.btnExtrafanartsSetAsFanart = New System.Windows.Forms.Button()
        Me.pnlExtrafanarts = New System.Windows.Forms.Panel()
        Me.btnExtrafanartsRefresh = New System.Windows.Forms.Button()
        Me.btnExtrafanartsRemove = New System.Windows.Forms.Button()
        Me.pbExtrafanarts = New System.Windows.Forms.PictureBox()
        Me.tpFanart = New System.Windows.Forms.TabPage()
        Me.btnSetFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveFanart = New System.Windows.Forms.Button()
        Me.lblFanartSize = New System.Windows.Forms.Label()
        Me.btnSetFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetFanartLocal = New System.Windows.Forms.Button()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.tpClearLogo = New System.Windows.Forms.TabPage()
        Me.lblClearLogoSize = New System.Windows.Forms.Label()
        Me.btnSetClearLogoDL = New System.Windows.Forms.Button()
        Me.btnRemoveClearLogo = New System.Windows.Forms.Button()
        Me.btnSetClearLogoScrape = New System.Windows.Forms.Button()
        Me.btnSetClearLogoLocal = New System.Windows.Forms.Button()
        Me.pbClearLogo = New System.Windows.Forms.PictureBox()
        Me.tpClearArt = New System.Windows.Forms.TabPage()
        Me.lblClearArtSize = New System.Windows.Forms.Label()
        Me.btnSetClearArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveClearArt = New System.Windows.Forms.Button()
        Me.btnSetClearArtScrape = New System.Windows.Forms.Button()
        Me.btnSetClearArtLocal = New System.Windows.Forms.Button()
        Me.pbClearArt = New System.Windows.Forms.PictureBox()
        Me.tpCharacterArt = New System.Windows.Forms.TabPage()
        Me.lblCharacterArtSize = New System.Windows.Forms.Label()
        Me.btnSetCharacterArtDL = New System.Windows.Forms.Button()
        Me.btnRemoveCharacterArt = New System.Windows.Forms.Button()
        Me.btnSetCharacterArtScrape = New System.Windows.Forms.Button()
        Me.btnSetCharacterArtLocal = New System.Windows.Forms.Button()
        Me.pbCharacterArt = New System.Windows.Forms.PictureBox()
        Me.tpLandscape = New System.Windows.Forms.TabPage()
        Me.btnSetLandscapeDL = New System.Windows.Forms.Button()
        Me.btnRemoveLandscape = New System.Windows.Forms.Button()
        Me.lblLandscapeSize = New System.Windows.Forms.Label()
        Me.btnSetLandscapeScrape = New System.Windows.Forms.Button()
        Me.btnSetLandscapeLocal = New System.Windows.Forms.Button()
        Me.pbLandscape = New System.Windows.Forms.PictureBox()
        Me.tpBanner = New System.Windows.Forms.TabPage()
        Me.btnSetBannerDL = New System.Windows.Forms.Button()
        Me.btnRemoveBanner = New System.Windows.Forms.Button()
        Me.lblBannerSize = New System.Windows.Forms.Label()
        Me.btnSetBannerScrape = New System.Windows.Forms.Button()
        Me.btnSetBannerLocal = New System.Windows.Forms.Button()
        Me.pbBanner = New System.Windows.Forms.PictureBox()
        Me.tpPoster = New System.Windows.Forms.TabPage()
        Me.btnSetPosterDL = New System.Windows.Forms.Button()
        Me.btnRemovePoster = New System.Windows.Forms.Button()
        Me.lblPosterSize = New System.Windows.Forms.Label()
        Me.btnSetPosterScrape = New System.Windows.Forms.Button()
        Me.btnSetPosterLocal = New System.Windows.Forms.Button()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.tpDetails = New System.Windows.Forms.TabPage()
        Me.txtMPAA = New System.Windows.Forms.TextBox()
        Me.txtVotes = New System.Windows.Forms.TextBox()
        Me.txtRuntime = New System.Windows.Forms.TextBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtPremiered = New System.Windows.Forms.TextBox()
        Me.txtStudio = New System.Windows.Forms.TextBox()
        Me.lblVotes = New System.Windows.Forms.Label()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.pbStar10 = New System.Windows.Forms.PictureBox()
        Me.pbStar9 = New System.Windows.Forms.PictureBox()
        Me.pbStar8 = New System.Windows.Forms.PictureBox()
        Me.pbStar7 = New System.Windows.Forms.PictureBox()
        Me.pbStar6 = New System.Windows.Forms.PictureBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnActorDown = New System.Windows.Forms.Button()
        Me.btnActorUp = New System.Windows.Forms.Button()
        Me.clbGenre = New System.Windows.Forms.CheckedListBox()
        Me.lblStudio = New System.Windows.Forms.Label()
        Me.btnActorEdit = New System.Windows.Forms.Button()
        Me.btnActorAdd = New System.Windows.Forms.Button()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnActorRemove = New System.Windows.Forms.Button()
        Me.lblActors = New System.Windows.Forms.Label()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.txtSortTitle = New System.Windows.Forms.TextBox()
        Me.txtOriginalTitle = New System.Windows.Forms.TextBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lbMPAA = New System.Windows.Forms.ListBox()
        Me.lblGenre = New System.Windows.Forms.Label()
        Me.lblMPAA = New System.Windows.Forms.Label()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.pbStar5 = New System.Windows.Forms.PictureBox()
        Me.pbStar4 = New System.Windows.Forms.PictureBox()
        Me.pbStar3 = New System.Windows.Forms.PictureBox()
        Me.pbStar2 = New System.Windows.Forms.PictureBox()
        Me.pbStar1 = New System.Windows.Forms.PictureBox()
        Me.lblRating = New System.Windows.Forms.Label()
        Me.lblPremiered = New System.Windows.Forms.Label()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.lblSortTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.tcEdit = New System.Windows.Forms.TabControl()
        Me.tpTheme = New System.Windows.Forms.TabPage()
        Me.pnlThemePreview = New System.Windows.Forms.Panel()
        Me.pnlThemePreviewNoPlayer = New System.Windows.Forms.Panel()
        Me.tblThemePreviewNoPlayer = New System.Windows.Forms.TableLayoutPanel()
        Me.lblThemePreviewNoPlayer = New System.Windows.Forms.Label()
        Me.btnSetThemeDL = New System.Windows.Forms.Button()
        Me.btnRemoveTheme = New System.Windows.Forms.Button()
        Me.btnSetThemeScrape = New System.Windows.Forms.Button()
        Me.btnSetThemeLocal = New System.Windows.Forms.Button()
        Me.btnLocalThemePlay = New System.Windows.Forms.Button()
        Me.txtLocalTheme = New System.Windows.Forms.TextBox()
        Me.lblLanguage = New System.Windows.Forms.Label()
        Me.cbSourceLanguage = New System.Windows.Forms.ComboBox()
        Me.lblUserRating = New System.Windows.Forms.Label()
        Me.txtUserRating = New System.Windows.Forms.TextBox()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpExtrafanarts.SuspendLayout()
        Me.pnlShowExtrafanartsSetAsFanart.SuspendLayout()
        CType(Me.pbExtrafanarts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFanart.SuspendLayout()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpClearLogo.SuspendLayout()
        CType(Me.pbClearLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpClearArt.SuspendLayout()
        CType(Me.pbClearArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCharacterArt.SuspendLayout()
        CType(Me.pbCharacterArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpLandscape.SuspendLayout()
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBanner.SuspendLayout()
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPoster.SuspendLayout()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDetails.SuspendLayout()
        CType(Me.pbStar10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcEdit.SuspendLayout()
        Me.tpTheme.SuspendLayout()
        Me.pnlThemePreview.SuspendLayout()
        Me.pnlThemePreviewNoPlayer.SuspendLayout()
        Me.tblThemePreviewNoPlayer.SuspendLayout()
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
        Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(781, 592)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.OK_Button.Location = New System.Drawing.Point(708, 592)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'cbOrdering
        '
        Me.cbOrdering.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbOrdering.FormattingEnabled = True
        Me.cbOrdering.Location = New System.Drawing.Point(142, 592)
        Me.cbOrdering.Name = "cbOrdering"
        Me.cbOrdering.Size = New System.Drawing.Size(166, 21)
        Me.cbOrdering.TabIndex = 5
        '
        'lblOrdering
        '
        Me.lblOrdering.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOrdering.AutoSize = True
        Me.lblOrdering.Location = New System.Drawing.Point(5, 597)
        Me.lblOrdering.Name = "lblOrdering"
        Me.lblOrdering.Size = New System.Drawing.Size(101, 13)
        Me.lblOrdering.TabIndex = 4
        Me.lblOrdering.Text = "Episode Ordering:"
        '
        'cbEpisodeSorting
        '
        Me.cbEpisodeSorting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbEpisodeSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEpisodeSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbEpisodeSorting.FormattingEnabled = True
        Me.cbEpisodeSorting.Location = New System.Drawing.Point(142, 619)
        Me.cbEpisodeSorting.Name = "cbEpisodeSorting"
        Me.cbEpisodeSorting.Size = New System.Drawing.Size(166, 21)
        Me.cbEpisodeSorting.TabIndex = 5
        '
        'lblEpisodeSorting
        '
        Me.lblEpisodeSorting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblEpisodeSorting.AutoSize = True
        Me.lblEpisodeSorting.Location = New System.Drawing.Point(5, 622)
        Me.lblEpisodeSorting.Name = "lblEpisodeSorting"
        Me.lblEpisodeSorting.Size = New System.Drawing.Size(103, 13)
        Me.lblEpisodeSorting.TabIndex = 4
        Me.lblEpisodeSorting.Text = "Episode Sorted by:"
        Me.lblEpisodeSorting.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'tpExtrafanarts
        '
        Me.tpExtrafanarts.Controls.Add(Me.lblExtrafanartsSize)
        Me.tpExtrafanarts.Controls.Add(Me.pnlShowExtrafanartsSetAsFanart)
        Me.tpExtrafanarts.Controls.Add(Me.pnlExtrafanarts)
        Me.tpExtrafanarts.Controls.Add(Me.btnExtrafanartsRefresh)
        Me.tpExtrafanarts.Controls.Add(Me.btnExtrafanartsRemove)
        Me.tpExtrafanarts.Controls.Add(Me.pbExtrafanarts)
        Me.tpExtrafanarts.Location = New System.Drawing.Point(4, 22)
        Me.tpExtrafanarts.Name = "tpExtrafanarts"
        Me.tpExtrafanarts.Size = New System.Drawing.Size(836, 490)
        Me.tpExtrafanarts.TabIndex = 12
        Me.tpExtrafanarts.Text = "Extrafanarts"
        Me.tpExtrafanarts.UseVisualStyleBackColor = True
        '
        'lblExtrafanartsSize
        '
        Me.lblExtrafanartsSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblExtrafanartsSize.Location = New System.Drawing.Point(178, 10)
        Me.lblExtrafanartsSize.Name = "lblExtrafanartsSize"
        Me.lblExtrafanartsSize.Size = New System.Drawing.Size(104, 23)
        Me.lblExtrafanartsSize.TabIndex = 23
        Me.lblExtrafanartsSize.Text = "Size: (XXXXxXXXX)"
        Me.lblExtrafanartsSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblExtrafanartsSize.Visible = False
        '
        'pnlShowExtrafanartsSetAsFanart
        '
        Me.pnlShowExtrafanartsSetAsFanart.BackColor = System.Drawing.Color.LightGray
        Me.pnlShowExtrafanartsSetAsFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlShowExtrafanartsSetAsFanart.Controls.Add(Me.btnExtrafanartsSetAsFanart)
        Me.pnlShowExtrafanartsSetAsFanart.Location = New System.Drawing.Point(719, 403)
        Me.pnlShowExtrafanartsSetAsFanart.Name = "pnlShowExtrafanartsSetAsFanart"
        Me.pnlShowExtrafanartsSetAsFanart.Size = New System.Drawing.Size(109, 39)
        Me.pnlShowExtrafanartsSetAsFanart.TabIndex = 21
        '
        'btnExtrafanartsSetAsFanart
        '
        Me.btnExtrafanartsSetAsFanart.Enabled = False
        Me.btnExtrafanartsSetAsFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnExtrafanartsSetAsFanart.Image = CType(resources.GetObject("btnExtrafanartsSetAsFanart.Image"), System.Drawing.Image)
        Me.btnExtrafanartsSetAsFanart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExtrafanartsSetAsFanart.Location = New System.Drawing.Point(2, 3)
        Me.btnExtrafanartsSetAsFanart.Name = "btnExtrafanartsSetAsFanart"
        Me.btnExtrafanartsSetAsFanart.Size = New System.Drawing.Size(103, 32)
        Me.btnExtrafanartsSetAsFanart.TabIndex = 0
        Me.btnExtrafanartsSetAsFanart.Text = "Set As Fanart"
        Me.btnExtrafanartsSetAsFanart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExtrafanartsSetAsFanart.UseVisualStyleBackColor = True
        '
        'pnlExtrafanarts
        '
        Me.pnlExtrafanarts.AutoScroll = True
        Me.pnlExtrafanarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlExtrafanarts.Location = New System.Drawing.Point(6, 8)
        Me.pnlExtrafanarts.Name = "pnlExtrafanarts"
        Me.pnlExtrafanarts.Size = New System.Drawing.Size(165, 408)
        Me.pnlExtrafanarts.TabIndex = 22
        '
        'btnExtrafanartsRefresh
        '
        Me.btnExtrafanartsRefresh.Image = CType(resources.GetObject("btnExtrafanartsRefresh.Image"), System.Drawing.Image)
        Me.btnExtrafanartsRefresh.Location = New System.Drawing.Point(88, 422)
        Me.btnExtrafanartsRefresh.Name = "btnExtrafanartsRefresh"
        Me.btnExtrafanartsRefresh.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrafanartsRefresh.TabIndex = 19
        Me.btnExtrafanartsRefresh.UseVisualStyleBackColor = True
        '
        'btnExtrafanartsRemove
        '
        Me.btnExtrafanartsRemove.Image = CType(resources.GetObject("btnExtrafanartsRemove.Image"), System.Drawing.Image)
        Me.btnExtrafanartsRemove.Location = New System.Drawing.Point(148, 422)
        Me.btnExtrafanartsRemove.Name = "btnExtrafanartsRemove"
        Me.btnExtrafanartsRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnExtrafanartsRemove.TabIndex = 20
        Me.btnExtrafanartsRemove.UseVisualStyleBackColor = True
        '
        'pbExtrafanarts
        '
        Me.pbExtrafanarts.BackColor = System.Drawing.Color.DimGray
        Me.pbExtrafanarts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbExtrafanarts.Location = New System.Drawing.Point(177, 8)
        Me.pbExtrafanarts.Name = "pbExtrafanarts"
        Me.pbExtrafanarts.Size = New System.Drawing.Size(653, 437)
        Me.pbExtrafanarts.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbExtrafanarts.TabIndex = 18
        Me.pbExtrafanarts.TabStop = False
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
        Me.tpFanart.Size = New System.Drawing.Size(836, 490)
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
        Me.lblFanartSize.Size = New System.Drawing.Size(104, 23)
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
        Me.btnSetFanartLocal.Text = "Local Browse"
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
        'tpClearLogo
        '
        Me.tpClearLogo.Controls.Add(Me.lblClearLogoSize)
        Me.tpClearLogo.Controls.Add(Me.btnSetClearLogoDL)
        Me.tpClearLogo.Controls.Add(Me.btnRemoveClearLogo)
        Me.tpClearLogo.Controls.Add(Me.btnSetClearLogoScrape)
        Me.tpClearLogo.Controls.Add(Me.btnSetClearLogoLocal)
        Me.tpClearLogo.Controls.Add(Me.pbClearLogo)
        Me.tpClearLogo.Location = New System.Drawing.Point(4, 22)
        Me.tpClearLogo.Name = "tpClearLogo"
        Me.tpClearLogo.Size = New System.Drawing.Size(836, 490)
        Me.tpClearLogo.TabIndex = 11
        Me.tpClearLogo.Text = "ClearLogo"
        Me.tpClearLogo.UseVisualStyleBackColor = True
        '
        'lblClearLogoSize
        '
        Me.lblClearLogoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClearLogoSize.Location = New System.Drawing.Point(8, 8)
        Me.lblClearLogoSize.Name = "lblClearLogoSize"
        Me.lblClearLogoSize.Size = New System.Drawing.Size(104, 23)
        Me.lblClearLogoSize.TabIndex = 17
        Me.lblClearLogoSize.Text = "Size: (XXXXxXXXX)"
        Me.lblClearLogoSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblClearLogoSize.Visible = False
        '
        'btnSetClearLogoDL
        '
        Me.btnSetClearLogoDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoDL.Image = CType(resources.GetObject("btnSetClearLogoDL.Image"), System.Drawing.Image)
        Me.btnSetClearLogoDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearLogoDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetClearLogoDL.Name = "btnSetClearLogoDL"
        Me.btnSetClearLogoDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearLogoDL.TabIndex = 15
        Me.btnSetClearLogoDL.Text = "Download"
        Me.btnSetClearLogoDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoDL.UseVisualStyleBackColor = True
        '
        'btnRemoveClearLogo
        '
        Me.btnRemoveClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveClearLogo.Image = CType(resources.GetObject("btnRemoveClearLogo.Image"), System.Drawing.Image)
        Me.btnRemoveClearLogo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveClearLogo.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveClearLogo.Name = "btnRemoveClearLogo"
        Me.btnRemoveClearLogo.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveClearLogo.TabIndex = 16
        Me.btnRemoveClearLogo.Text = "Remove"
        Me.btnRemoveClearLogo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveClearLogo.UseVisualStyleBackColor = True
        '
        'btnSetClearLogoScrape
        '
        Me.btnSetClearLogoScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoScrape.Image = CType(resources.GetObject("btnSetClearLogoScrape.Image"), System.Drawing.Image)
        Me.btnSetClearLogoScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearLogoScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetClearLogoScrape.Name = "btnSetClearLogoScrape"
        Me.btnSetClearLogoScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearLogoScrape.TabIndex = 14
        Me.btnSetClearLogoScrape.Text = "Scrape"
        Me.btnSetClearLogoScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoScrape.UseVisualStyleBackColor = True
        '
        'btnSetClearLogoLocal
        '
        Me.btnSetClearLogoLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearLogoLocal.Image = CType(resources.GetObject("btnSetClearLogoLocal.Image"), System.Drawing.Image)
        Me.btnSetClearLogoLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearLogoLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetClearLogoLocal.Name = "btnSetClearLogoLocal"
        Me.btnSetClearLogoLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearLogoLocal.TabIndex = 13
        Me.btnSetClearLogoLocal.Text = "Local Browse"
        Me.btnSetClearLogoLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearLogoLocal.UseVisualStyleBackColor = True
        '
        'pbClearLogo
        '
        Me.pbClearLogo.BackColor = System.Drawing.Color.DimGray
        Me.pbClearLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbClearLogo.Location = New System.Drawing.Point(6, 6)
        Me.pbClearLogo.Name = "pbClearLogo"
        Me.pbClearLogo.Size = New System.Drawing.Size(724, 440)
        Me.pbClearLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbClearLogo.TabIndex = 12
        Me.pbClearLogo.TabStop = False
        '
        'tpClearArt
        '
        Me.tpClearArt.Controls.Add(Me.lblClearArtSize)
        Me.tpClearArt.Controls.Add(Me.btnSetClearArtDL)
        Me.tpClearArt.Controls.Add(Me.btnRemoveClearArt)
        Me.tpClearArt.Controls.Add(Me.btnSetClearArtScrape)
        Me.tpClearArt.Controls.Add(Me.btnSetClearArtLocal)
        Me.tpClearArt.Controls.Add(Me.pbClearArt)
        Me.tpClearArt.Location = New System.Drawing.Point(4, 22)
        Me.tpClearArt.Name = "tpClearArt"
        Me.tpClearArt.Size = New System.Drawing.Size(836, 490)
        Me.tpClearArt.TabIndex = 10
        Me.tpClearArt.Text = "ClearArt"
        Me.tpClearArt.UseVisualStyleBackColor = True
        '
        'lblClearArtSize
        '
        Me.lblClearArtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClearArtSize.Location = New System.Drawing.Point(8, 8)
        Me.lblClearArtSize.Name = "lblClearArtSize"
        Me.lblClearArtSize.Size = New System.Drawing.Size(104, 23)
        Me.lblClearArtSize.TabIndex = 17
        Me.lblClearArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblClearArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblClearArtSize.Visible = False
        '
        'btnSetClearArtDL
        '
        Me.btnSetClearArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtDL.Image = CType(resources.GetObject("btnSetClearArtDL.Image"), System.Drawing.Image)
        Me.btnSetClearArtDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearArtDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetClearArtDL.Name = "btnSetClearArtDL"
        Me.btnSetClearArtDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearArtDL.TabIndex = 15
        Me.btnSetClearArtDL.Text = "Download"
        Me.btnSetClearArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtDL.UseVisualStyleBackColor = True
        '
        'btnRemoveClearArt
        '
        Me.btnRemoveClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveClearArt.Image = CType(resources.GetObject("btnRemoveClearArt.Image"), System.Drawing.Image)
        Me.btnRemoveClearArt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveClearArt.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveClearArt.Name = "btnRemoveClearArt"
        Me.btnRemoveClearArt.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveClearArt.TabIndex = 16
        Me.btnRemoveClearArt.Text = "Remove"
        Me.btnRemoveClearArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveClearArt.UseVisualStyleBackColor = True
        '
        'btnSetClearArtScrape
        '
        Me.btnSetClearArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtScrape.Image = CType(resources.GetObject("btnSetClearArtScrape.Image"), System.Drawing.Image)
        Me.btnSetClearArtScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearArtScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetClearArtScrape.Name = "btnSetClearArtScrape"
        Me.btnSetClearArtScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearArtScrape.TabIndex = 14
        Me.btnSetClearArtScrape.Text = "Scrape"
        Me.btnSetClearArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtScrape.UseVisualStyleBackColor = True
        '
        'btnSetClearArtLocal
        '
        Me.btnSetClearArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetClearArtLocal.Image = CType(resources.GetObject("btnSetClearArtLocal.Image"), System.Drawing.Image)
        Me.btnSetClearArtLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetClearArtLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetClearArtLocal.Name = "btnSetClearArtLocal"
        Me.btnSetClearArtLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetClearArtLocal.TabIndex = 13
        Me.btnSetClearArtLocal.Text = "Local Browse"
        Me.btnSetClearArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetClearArtLocal.UseVisualStyleBackColor = True
        '
        'pbClearArt
        '
        Me.pbClearArt.BackColor = System.Drawing.Color.DimGray
        Me.pbClearArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbClearArt.Location = New System.Drawing.Point(6, 6)
        Me.pbClearArt.Name = "pbClearArt"
        Me.pbClearArt.Size = New System.Drawing.Size(724, 440)
        Me.pbClearArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbClearArt.TabIndex = 12
        Me.pbClearArt.TabStop = False
        '
        'tpCharacterArt
        '
        Me.tpCharacterArt.Controls.Add(Me.lblCharacterArtSize)
        Me.tpCharacterArt.Controls.Add(Me.btnSetCharacterArtDL)
        Me.tpCharacterArt.Controls.Add(Me.btnRemoveCharacterArt)
        Me.tpCharacterArt.Controls.Add(Me.btnSetCharacterArtScrape)
        Me.tpCharacterArt.Controls.Add(Me.btnSetCharacterArtLocal)
        Me.tpCharacterArt.Controls.Add(Me.pbCharacterArt)
        Me.tpCharacterArt.Location = New System.Drawing.Point(4, 22)
        Me.tpCharacterArt.Name = "tpCharacterArt"
        Me.tpCharacterArt.Size = New System.Drawing.Size(836, 490)
        Me.tpCharacterArt.TabIndex = 9
        Me.tpCharacterArt.Text = "CharacterArt"
        Me.tpCharacterArt.UseVisualStyleBackColor = True
        '
        'lblCharacterArtSize
        '
        Me.lblCharacterArtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCharacterArtSize.Location = New System.Drawing.Point(8, 8)
        Me.lblCharacterArtSize.Name = "lblCharacterArtSize"
        Me.lblCharacterArtSize.Size = New System.Drawing.Size(104, 23)
        Me.lblCharacterArtSize.TabIndex = 23
        Me.lblCharacterArtSize.Text = "Size: (XXXXxXXXX)"
        Me.lblCharacterArtSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCharacterArtSize.Visible = False
        '
        'btnSetCharacterArtDL
        '
        Me.btnSetCharacterArtDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetCharacterArtDL.Image = CType(resources.GetObject("btnSetCharacterArtDL.Image"), System.Drawing.Image)
        Me.btnSetCharacterArtDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetCharacterArtDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetCharacterArtDL.Name = "btnSetCharacterArtDL"
        Me.btnSetCharacterArtDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetCharacterArtDL.TabIndex = 21
        Me.btnSetCharacterArtDL.Text = "Download"
        Me.btnSetCharacterArtDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetCharacterArtDL.UseVisualStyleBackColor = True
        '
        'btnRemoveCharacterArt
        '
        Me.btnRemoveCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveCharacterArt.Image = CType(resources.GetObject("btnRemoveCharacterArt.Image"), System.Drawing.Image)
        Me.btnRemoveCharacterArt.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveCharacterArt.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveCharacterArt.Name = "btnRemoveCharacterArt"
        Me.btnRemoveCharacterArt.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveCharacterArt.TabIndex = 22
        Me.btnRemoveCharacterArt.Text = "Remove"
        Me.btnRemoveCharacterArt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveCharacterArt.UseVisualStyleBackColor = True
        '
        'btnSetCharacterArtScrape
        '
        Me.btnSetCharacterArtScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetCharacterArtScrape.Image = CType(resources.GetObject("btnSetCharacterArtScrape.Image"), System.Drawing.Image)
        Me.btnSetCharacterArtScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetCharacterArtScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetCharacterArtScrape.Name = "btnSetCharacterArtScrape"
        Me.btnSetCharacterArtScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetCharacterArtScrape.TabIndex = 20
        Me.btnSetCharacterArtScrape.Text = "Scrape"
        Me.btnSetCharacterArtScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetCharacterArtScrape.UseVisualStyleBackColor = True
        '
        'btnSetCharacterArtLocal
        '
        Me.btnSetCharacterArtLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetCharacterArtLocal.Image = CType(resources.GetObject("btnSetCharacterArtLocal.Image"), System.Drawing.Image)
        Me.btnSetCharacterArtLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetCharacterArtLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetCharacterArtLocal.Name = "btnSetCharacterArtLocal"
        Me.btnSetCharacterArtLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetCharacterArtLocal.TabIndex = 19
        Me.btnSetCharacterArtLocal.Text = "Local Browse"
        Me.btnSetCharacterArtLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetCharacterArtLocal.UseVisualStyleBackColor = True
        '
        'pbCharacterArt
        '
        Me.pbCharacterArt.BackColor = System.Drawing.Color.DimGray
        Me.pbCharacterArt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbCharacterArt.Location = New System.Drawing.Point(6, 6)
        Me.pbCharacterArt.Name = "pbCharacterArt"
        Me.pbCharacterArt.Size = New System.Drawing.Size(724, 440)
        Me.pbCharacterArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbCharacterArt.TabIndex = 18
        Me.pbCharacterArt.TabStop = False
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
        Me.tpLandscape.Size = New System.Drawing.Size(836, 490)
        Me.tpLandscape.TabIndex = 7
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
        Me.btnSetLandscapeDL.TabIndex = 9
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
        Me.btnRemoveLandscape.TabIndex = 10
        Me.btnRemoveLandscape.Text = "Remove"
        Me.btnRemoveLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveLandscape.UseVisualStyleBackColor = True
        '
        'lblLandscapeSize
        '
        Me.lblLandscapeSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLandscapeSize.Location = New System.Drawing.Point(8, 8)
        Me.lblLandscapeSize.Name = "lblLandscapeSize"
        Me.lblLandscapeSize.Size = New System.Drawing.Size(104, 23)
        Me.lblLandscapeSize.TabIndex = 5
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
        Me.btnSetLandscapeScrape.TabIndex = 8
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
        Me.btnSetLandscapeLocal.TabIndex = 7
        Me.btnSetLandscapeLocal.Text = "Local Browse"
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
        Me.pbLandscape.TabIndex = 6
        Me.pbLandscape.TabStop = False
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
        Me.tpBanner.Size = New System.Drawing.Size(836, 490)
        Me.tpBanner.TabIndex = 4
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
        Me.lblBannerSize.Size = New System.Drawing.Size(104, 23)
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
        Me.btnSetBannerLocal.Text = "Local Browse"
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
        Me.tpPoster.Size = New System.Drawing.Size(836, 490)
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
        Me.lblPosterSize.Size = New System.Drawing.Size(104, 23)
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
        Me.btnSetPosterLocal.Text = "Local Browse"
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
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.txtMPAA)
        Me.tpDetails.Controls.Add(Me.txtUserRating)
        Me.tpDetails.Controls.Add(Me.txtVotes)
        Me.tpDetails.Controls.Add(Me.txtRuntime)
        Me.tpDetails.Controls.Add(Me.txtStatus)
        Me.tpDetails.Controls.Add(Me.txtPremiered)
        Me.tpDetails.Controls.Add(Me.txtStudio)
        Me.tpDetails.Controls.Add(Me.lblUserRating)
        Me.tpDetails.Controls.Add(Me.lblVotes)
        Me.tpDetails.Controls.Add(Me.lblRuntime)
        Me.tpDetails.Controls.Add(Me.pbStar10)
        Me.tpDetails.Controls.Add(Me.pbStar9)
        Me.tpDetails.Controls.Add(Me.pbStar8)
        Me.tpDetails.Controls.Add(Me.pbStar7)
        Me.tpDetails.Controls.Add(Me.pbStar6)
        Me.tpDetails.Controls.Add(Me.lblStatus)
        Me.tpDetails.Controls.Add(Me.btnActorDown)
        Me.tpDetails.Controls.Add(Me.btnActorUp)
        Me.tpDetails.Controls.Add(Me.clbGenre)
        Me.tpDetails.Controls.Add(Me.lblStudio)
        Me.tpDetails.Controls.Add(Me.btnActorEdit)
        Me.tpDetails.Controls.Add(Me.btnActorAdd)
        Me.tpDetails.Controls.Add(Me.btnManual)
        Me.tpDetails.Controls.Add(Me.btnActorRemove)
        Me.tpDetails.Controls.Add(Me.lblActors)
        Me.tpDetails.Controls.Add(Me.lvActors)
        Me.tpDetails.Controls.Add(Me.txtPlot)
        Me.tpDetails.Controls.Add(Me.txtSortTitle)
        Me.tpDetails.Controls.Add(Me.txtOriginalTitle)
        Me.tpDetails.Controls.Add(Me.txtTitle)
        Me.tpDetails.Controls.Add(Me.lbMPAA)
        Me.tpDetails.Controls.Add(Me.lblGenre)
        Me.tpDetails.Controls.Add(Me.lblMPAA)
        Me.tpDetails.Controls.Add(Me.lblPlot)
        Me.tpDetails.Controls.Add(Me.pbStar5)
        Me.tpDetails.Controls.Add(Me.pbStar4)
        Me.tpDetails.Controls.Add(Me.pbStar3)
        Me.tpDetails.Controls.Add(Me.pbStar2)
        Me.tpDetails.Controls.Add(Me.pbStar1)
        Me.tpDetails.Controls.Add(Me.lblRating)
        Me.tpDetails.Controls.Add(Me.lblPremiered)
        Me.tpDetails.Controls.Add(Me.lblOriginalTitle)
        Me.tpDetails.Controls.Add(Me.lblSortTitle)
        Me.tpDetails.Controls.Add(Me.lblTitle)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(836, 490)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
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
        'txtVotes
        '
        Me.txtVotes.BackColor = System.Drawing.SystemColors.Window
        Me.txtVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVotes.Location = New System.Drawing.Point(268, 188)
        Me.txtVotes.Name = "txtVotes"
        Me.txtVotes.Size = New System.Drawing.Size(66, 22)
        Me.txtVotes.TabIndex = 81
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
        'txtStatus
        '
        Me.txtStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(635, 362)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(193, 22)
        Me.txtStatus.TabIndex = 69
        '
        'txtPremiered
        '
        Me.txtPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPremiered.Location = New System.Drawing.Point(6, 148)
        Me.txtPremiered.Name = "txtPremiered"
        Me.txtPremiered.Size = New System.Drawing.Size(192, 22)
        Me.txtPremiered.TabIndex = 3
        '
        'txtStudio
        '
        Me.txtStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStudio.Location = New System.Drawing.Point(635, 321)
        Me.txtStudio.Name = "txtStudio"
        Me.txtStudio.Size = New System.Drawing.Size(193, 22)
        Me.txtStudio.TabIndex = 19
        '
        'lblVotes
        '
        Me.lblVotes.AutoSize = True
        Me.lblVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVotes.Location = New System.Drawing.Point(265, 173)
        Me.lblVotes.Name = "lblVotes"
        Me.lblVotes.Size = New System.Drawing.Size(38, 13)
        Me.lblVotes.TabIndex = 80
        Me.lblVotes.Text = "Votes:"
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
        'pbStar10
        '
        Me.pbStar10.Location = New System.Drawing.Point(222, 189)
        Me.pbStar10.Name = "pbStar10"
        Me.pbStar10.Size = New System.Drawing.Size(24, 24)
        Me.pbStar10.TabIndex = 77
        Me.pbStar10.TabStop = False
        '
        'pbStar9
        '
        Me.pbStar9.Location = New System.Drawing.Point(198, 189)
        Me.pbStar9.Name = "pbStar9"
        Me.pbStar9.Size = New System.Drawing.Size(24, 24)
        Me.pbStar9.TabIndex = 76
        Me.pbStar9.TabStop = False
        '
        'pbStar8
        '
        Me.pbStar8.Location = New System.Drawing.Point(174, 189)
        Me.pbStar8.Name = "pbStar8"
        Me.pbStar8.Size = New System.Drawing.Size(24, 24)
        Me.pbStar8.TabIndex = 75
        Me.pbStar8.TabStop = False
        '
        'pbStar7
        '
        Me.pbStar7.Location = New System.Drawing.Point(150, 189)
        Me.pbStar7.Name = "pbStar7"
        Me.pbStar7.Size = New System.Drawing.Size(24, 24)
        Me.pbStar7.TabIndex = 74
        Me.pbStar7.TabStop = False
        '
        'pbStar6
        '
        Me.pbStar6.Location = New System.Drawing.Point(126, 189)
        Me.pbStar6.Name = "pbStar6"
        Me.pbStar6.Size = New System.Drawing.Size(24, 24)
        Me.pbStar6.TabIndex = 73
        Me.pbStar6.TabStop = False
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
        'btnActorDown
        '
        Me.btnActorDown.Image = CType(resources.GetObject("btnActorDown.Image"), System.Drawing.Image)
        Me.btnActorDown.Location = New System.Drawing.Point(434, 431)
        Me.btnActorDown.Name = "btnActorDown"
        Me.btnActorDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorDown.TabIndex = 14
        Me.btnActorDown.UseVisualStyleBackColor = True
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
        'clbGenre
        '
        Me.clbGenre.CheckOnClick = True
        Me.clbGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clbGenre.FormattingEnabled = True
        Me.clbGenre.IntegralHeight = False
        Me.clbGenre.Location = New System.Drawing.Point(7, 245)
        Me.clbGenre.Name = "clbGenre"
        Me.clbGenre.Size = New System.Drawing.Size(192, 212)
        Me.clbGenre.TabIndex = 6
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
        'btnActorEdit
        '
        Me.btnActorEdit.Image = CType(resources.GetObject("btnActorEdit.Image"), System.Drawing.Image)
        Me.btnActorEdit.Location = New System.Drawing.Point(246, 431)
        Me.btnActorEdit.Name = "btnActorEdit"
        Me.btnActorEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnActorEdit.TabIndex = 12
        Me.btnActorEdit.UseVisualStyleBackColor = True
        '
        'btnActorAdd
        '
        Me.btnActorAdd.Image = CType(resources.GetObject("btnActorAdd.Image"), System.Drawing.Image)
        Me.btnActorAdd.Location = New System.Drawing.Point(217, 431)
        Me.btnActorAdd.Name = "btnActorAdd"
        Me.btnActorAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnActorAdd.TabIndex = 11
        Me.btnActorAdd.UseVisualStyleBackColor = True
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
        'btnActorRemove
        '
        Me.btnActorRemove.Image = CType(resources.GetObject("btnActorRemove.Image"), System.Drawing.Image)
        Me.btnActorRemove.Location = New System.Drawing.Point(602, 431)
        Me.btnActorRemove.Name = "btnActorRemove"
        Me.btnActorRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnActorRemove.TabIndex = 15
        Me.btnActorRemove.UseVisualStyleBackColor = True
        '
        'lblActors
        '
        Me.lblActors.AutoSize = True
        Me.lblActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblActors.Location = New System.Drawing.Point(214, 229)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(43, 13)
        Me.lblActors.TabIndex = 9
        Me.lblActors.Text = "Actors:"
        '
        'lvActors
        '
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colID, Me.colName, Me.colRole, Me.colThumb})
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.Location = New System.Drawing.Point(217, 245)
        Me.lvActors.Name = "lvActors"
        Me.lvActors.Size = New System.Drawing.Size(408, 180)
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
        'txtSortTitle
        '
        Me.txtSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSortTitle.Location = New System.Drawing.Point(6, 107)
        Me.txtSortTitle.Name = "txtSortTitle"
        Me.txtSortTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtSortTitle.TabIndex = 1
        '
        'txtOriginalTitle
        '
        Me.txtOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOriginalTitle.Location = New System.Drawing.Point(6, 66)
        Me.txtOriginalTitle.Name = "txtOriginalTitle"
        Me.txtOriginalTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtOriginalTitle.TabIndex = 1
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(6, 23)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtTitle.TabIndex = 1
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
        'lblGenre
        '
        Me.lblGenre.AutoSize = True
        Me.lblGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGenre.Location = New System.Drawing.Point(6, 229)
        Me.lblGenre.Name = "lblGenre"
        Me.lblGenre.Size = New System.Drawing.Size(41, 13)
        Me.lblGenre.TabIndex = 5
        Me.lblGenre.Text = "Genre:"
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
        'pbStar5
        '
        Me.pbStar5.Location = New System.Drawing.Point(102, 189)
        Me.pbStar5.Name = "pbStar5"
        Me.pbStar5.Size = New System.Drawing.Size(24, 24)
        Me.pbStar5.TabIndex = 67
        Me.pbStar5.TabStop = False
        '
        'pbStar4
        '
        Me.pbStar4.Location = New System.Drawing.Point(78, 189)
        Me.pbStar4.Name = "pbStar4"
        Me.pbStar4.Size = New System.Drawing.Size(24, 24)
        Me.pbStar4.TabIndex = 66
        Me.pbStar4.TabStop = False
        '
        'pbStar3
        '
        Me.pbStar3.Location = New System.Drawing.Point(54, 189)
        Me.pbStar3.Name = "pbStar3"
        Me.pbStar3.Size = New System.Drawing.Size(24, 24)
        Me.pbStar3.TabIndex = 65
        Me.pbStar3.TabStop = False
        '
        'pbStar2
        '
        Me.pbStar2.Location = New System.Drawing.Point(30, 189)
        Me.pbStar2.Name = "pbStar2"
        Me.pbStar2.Size = New System.Drawing.Size(24, 24)
        Me.pbStar2.TabIndex = 64
        Me.pbStar2.TabStop = False
        '
        'pbStar1
        '
        Me.pbStar1.Location = New System.Drawing.Point(6, 189)
        Me.pbStar1.Name = "pbStar1"
        Me.pbStar1.Size = New System.Drawing.Size(24, 24)
        Me.pbStar1.TabIndex = 63
        Me.pbStar1.TabStop = False
        '
        'lblRating
        '
        Me.lblRating.AutoSize = True
        Me.lblRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRating.Location = New System.Drawing.Point(6, 173)
        Me.lblRating.Name = "lblRating"
        Me.lblRating.Size = New System.Drawing.Size(44, 13)
        Me.lblRating.TabIndex = 4
        Me.lblRating.Text = "Rating:"
        '
        'lblPremiered
        '
        Me.lblPremiered.AutoSize = True
        Me.lblPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPremiered.Location = New System.Drawing.Point(6, 132)
        Me.lblPremiered.Name = "lblPremiered"
        Me.lblPremiered.Size = New System.Drawing.Size(63, 13)
        Me.lblPremiered.TabIndex = 2
        Me.lblPremiered.Text = "Premiered:"
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.AutoSize = True
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblOriginalTitle.Location = New System.Drawing.Point(6, 50)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(76, 13)
        Me.lblOriginalTitle.TabIndex = 0
        Me.lblOriginalTitle.Text = "Original Title:"
        '
        'lblSortTitle
        '
        Me.lblSortTitle.AutoSize = True
        Me.lblSortTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSortTitle.Location = New System.Drawing.Point(6, 91)
        Me.lblSortTitle.Name = "lblSortTitle"
        Me.lblSortTitle.Size = New System.Drawing.Size(56, 13)
        Me.lblSortTitle.TabIndex = 0
        Me.lblSortTitle.Text = "Sort Title:"
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
        'tcEdit
        '
        Me.tcEdit.Controls.Add(Me.tpDetails)
        Me.tcEdit.Controls.Add(Me.tpPoster)
        Me.tcEdit.Controls.Add(Me.tpBanner)
        Me.tcEdit.Controls.Add(Me.tpLandscape)
        Me.tcEdit.Controls.Add(Me.tpCharacterArt)
        Me.tcEdit.Controls.Add(Me.tpClearArt)
        Me.tcEdit.Controls.Add(Me.tpClearLogo)
        Me.tcEdit.Controls.Add(Me.tpFanart)
        Me.tcEdit.Controls.Add(Me.tpExtrafanarts)
        Me.tcEdit.Controls.Add(Me.tpTheme)
        Me.tcEdit.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcEdit.Location = New System.Drawing.Point(4, 70)
        Me.tcEdit.Name = "tcEdit"
        Me.tcEdit.SelectedIndex = 0
        Me.tcEdit.Size = New System.Drawing.Size(844, 516)
        Me.tcEdit.TabIndex = 3
        '
        'tpTheme
        '
        Me.tpTheme.Controls.Add(Me.pnlThemePreview)
        Me.tpTheme.Controls.Add(Me.btnSetThemeDL)
        Me.tpTheme.Controls.Add(Me.btnRemoveTheme)
        Me.tpTheme.Controls.Add(Me.btnSetThemeScrape)
        Me.tpTheme.Controls.Add(Me.btnSetThemeLocal)
        Me.tpTheme.Controls.Add(Me.btnLocalThemePlay)
        Me.tpTheme.Controls.Add(Me.txtLocalTheme)
        Me.tpTheme.Location = New System.Drawing.Point(4, 22)
        Me.tpTheme.Name = "tpTheme"
        Me.tpTheme.Padding = New System.Windows.Forms.Padding(3)
        Me.tpTheme.Size = New System.Drawing.Size(836, 490)
        Me.tpTheme.TabIndex = 13
        Me.tpTheme.Text = "Theme"
        Me.tpTheme.UseVisualStyleBackColor = True
        '
        'pnlThemePreview
        '
        Me.pnlThemePreview.BackColor = System.Drawing.Color.DimGray
        Me.pnlThemePreview.Controls.Add(Me.pnlThemePreviewNoPlayer)
        Me.pnlThemePreview.Location = New System.Drawing.Point(6, 6)
        Me.pnlThemePreview.Name = "pnlThemePreview"
        Me.pnlThemePreview.Size = New System.Drawing.Size(724, 440)
        Me.pnlThemePreview.TabIndex = 61
        '
        'pnlThemePreviewNoPlayer
        '
        Me.pnlThemePreviewNoPlayer.BackColor = System.Drawing.Color.White
        Me.pnlThemePreviewNoPlayer.Controls.Add(Me.tblThemePreviewNoPlayer)
        Me.pnlThemePreviewNoPlayer.Location = New System.Drawing.Point(285, 203)
        Me.pnlThemePreviewNoPlayer.Name = "pnlThemePreviewNoPlayer"
        Me.pnlThemePreviewNoPlayer.Size = New System.Drawing.Size(242, 56)
        Me.pnlThemePreviewNoPlayer.TabIndex = 0
        '
        'tblThemePreviewNoPlayer
        '
        Me.tblThemePreviewNoPlayer.AutoSize = True
        Me.tblThemePreviewNoPlayer.ColumnCount = 1
        Me.tblThemePreviewNoPlayer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblThemePreviewNoPlayer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblThemePreviewNoPlayer.Controls.Add(Me.lblThemePreviewNoPlayer, 0, 0)
        Me.tblThemePreviewNoPlayer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblThemePreviewNoPlayer.Location = New System.Drawing.Point(0, 0)
        Me.tblThemePreviewNoPlayer.Name = "tblThemePreviewNoPlayer"
        Me.tblThemePreviewNoPlayer.RowCount = 1
        Me.tblThemePreviewNoPlayer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblThemePreviewNoPlayer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.tblThemePreviewNoPlayer.Size = New System.Drawing.Size(242, 56)
        Me.tblThemePreviewNoPlayer.TabIndex = 0
        '
        'lblThemePreviewNoPlayer
        '
        Me.lblThemePreviewNoPlayer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblThemePreviewNoPlayer.AutoSize = True
        Me.lblThemePreviewNoPlayer.Location = New System.Drawing.Point(52, 21)
        Me.lblThemePreviewNoPlayer.Name = "lblThemePreviewNoPlayer"
        Me.lblThemePreviewNoPlayer.Size = New System.Drawing.Size(137, 13)
        Me.lblThemePreviewNoPlayer.TabIndex = 0
        Me.lblThemePreviewNoPlayer.Text = "no Media Player enabled"
        '
        'btnSetThemeDL
        '
        Me.btnSetThemeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeDL.Image = CType(resources.GetObject("btnSetThemeDL.Image"), System.Drawing.Image)
        Me.btnSetThemeDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetThemeDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetThemeDL.Name = "btnSetThemeDL"
        Me.btnSetThemeDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetThemeDL.TabIndex = 59
        Me.btnSetThemeDL.Text = "Download"
        Me.btnSetThemeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeDL.UseVisualStyleBackColor = True
        '
        'btnRemoveTheme
        '
        Me.btnRemoveTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveTheme.Image = CType(resources.GetObject("btnRemoveTheme.Image"), System.Drawing.Image)
        Me.btnRemoveTheme.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveTheme.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveTheme.Name = "btnRemoveTheme"
        Me.btnRemoveTheme.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveTheme.TabIndex = 60
        Me.btnRemoveTheme.Text = "Remove"
        Me.btnRemoveTheme.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveTheme.UseVisualStyleBackColor = True
        '
        'btnSetThemeScrape
        '
        Me.btnSetThemeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeScrape.Image = CType(resources.GetObject("btnSetThemeScrape.Image"), System.Drawing.Image)
        Me.btnSetThemeScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetThemeScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetThemeScrape.Name = "btnSetThemeScrape"
        Me.btnSetThemeScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetThemeScrape.TabIndex = 58
        Me.btnSetThemeScrape.Text = "Scrape"
        Me.btnSetThemeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeScrape.UseVisualStyleBackColor = True
        '
        'btnSetThemeLocal
        '
        Me.btnSetThemeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetThemeLocal.Image = CType(resources.GetObject("btnSetThemeLocal.Image"), System.Drawing.Image)
        Me.btnSetThemeLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetThemeLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetThemeLocal.Name = "btnSetThemeLocal"
        Me.btnSetThemeLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetThemeLocal.TabIndex = 57
        Me.btnSetThemeLocal.Text = "Local Browse"
        Me.btnSetThemeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetThemeLocal.UseVisualStyleBackColor = True
        '
        'btnLocalThemePlay
        '
        Me.btnLocalThemePlay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLocalThemePlay.Enabled = False
        Me.btnLocalThemePlay.Image = Global.Ember_Media_Manager.My.Resources.Resources.Play_Icon
        Me.btnLocalThemePlay.Location = New System.Drawing.Point(707, 452)
        Me.btnLocalThemePlay.Name = "btnLocalThemePlay"
        Me.btnLocalThemePlay.Size = New System.Drawing.Size(23, 22)
        Me.btnLocalThemePlay.TabIndex = 63
        Me.btnLocalThemePlay.UseVisualStyleBackColor = True
        '
        'txtLocalTheme
        '
        Me.txtLocalTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtLocalTheme.Location = New System.Drawing.Point(6, 452)
        Me.txtLocalTheme.Name = "txtLocalTheme"
        Me.txtLocalTheme.ReadOnly = True
        Me.txtLocalTheme.Size = New System.Drawing.Size(695, 22)
        Me.txtLocalTheme.TabIndex = 62
        '
        'lblLanguage
        '
        Me.lblLanguage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLanguage.AutoSize = True
        Me.lblLanguage.Location = New System.Drawing.Point(339, 597)
        Me.lblLanguage.Name = "lblLanguage"
        Me.lblLanguage.Size = New System.Drawing.Size(61, 13)
        Me.lblLanguage.TabIndex = 4
        Me.lblLanguage.Text = "Language:"
        '
        'cbSourceLanguage
        '
        Me.cbSourceLanguage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSourceLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSourceLanguage.Location = New System.Drawing.Point(418, 592)
        Me.cbSourceLanguage.Name = "cbSourceLanguage"
        Me.cbSourceLanguage.Size = New System.Drawing.Size(172, 21)
        Me.cbSourceLanguage.TabIndex = 9
        '
        'lblUserRating
        '
        Me.lblUserRating.AutoSize = True
        Me.lblUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblUserRating.Location = New System.Drawing.Point(354, 173)
        Me.lblUserRating.Name = "lblUserRating"
        Me.lblUserRating.Size = New System.Drawing.Size(70, 13)
        Me.lblUserRating.TabIndex = 80
        Me.lblUserRating.Text = "User Rating:"
        '
        'txtUserRating
        '
        Me.txtUserRating.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtUserRating.Location = New System.Drawing.Point(357, 188)
        Me.txtUserRating.Name = "txtUserRating"
        Me.txtUserRating.Size = New System.Drawing.Size(66, 22)
        Me.txtUserRating.TabIndex = 81
        '
        'dlgEditTVShow
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(853, 648)
        Me.Controls.Add(Me.cbSourceLanguage)
        Me.Controls.Add(Me.lblEpisodeSorting)
        Me.Controls.Add(Me.cbEpisodeSorting)
        Me.Controls.Add(Me.lblLanguage)
        Me.Controls.Add(Me.lblOrdering)
        Me.Controls.Add(Me.cbOrdering)
        Me.Controls.Add(Me.tcEdit)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditTVShow"
        Me.Text = "Edit Show"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpExtrafanarts.ResumeLayout(False)
        Me.pnlShowExtrafanartsSetAsFanart.ResumeLayout(False)
        CType(Me.pbExtrafanarts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFanart.ResumeLayout(False)
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpClearLogo.ResumeLayout(False)
        CType(Me.pbClearLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpClearArt.ResumeLayout(False)
        CType(Me.pbClearArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCharacterArt.ResumeLayout(False)
        CType(Me.pbCharacterArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpLandscape.ResumeLayout(False)
        CType(Me.pbLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBanner.ResumeLayout(False)
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPoster.ResumeLayout(False)
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDetails.ResumeLayout(False)
        Me.tpDetails.PerformLayout()
        CType(Me.pbStar10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcEdit.ResumeLayout(False)
        Me.tpTheme.ResumeLayout(False)
        Me.tpTheme.PerformLayout()
        Me.pnlThemePreview.ResumeLayout(False)
        Me.pnlThemePreviewNoPlayer.ResumeLayout(False)
        Me.pnlThemePreviewNoPlayer.PerformLayout()
        Me.tblThemePreviewNoPlayer.ResumeLayout(False)
        Me.tblThemePreviewNoPlayer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents ofdLocalFiles As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cbOrdering As System.Windows.Forms.ComboBox
    Friend WithEvents lblOrdering As System.Windows.Forms.Label
    Friend WithEvents cbEpisodeSorting As System.Windows.Forms.ComboBox
    Friend WithEvents lblEpisodeSorting As System.Windows.Forms.Label
    Friend WithEvents tpExtrafanarts As System.Windows.Forms.TabPage
    Friend WithEvents lblExtrafanartsSize As System.Windows.Forms.Label
    Friend WithEvents pnlShowExtrafanartsSetAsFanart As System.Windows.Forms.Panel
    Friend WithEvents btnExtrafanartsSetAsFanart As System.Windows.Forms.Button
    Friend WithEvents pnlExtrafanarts As System.Windows.Forms.Panel
    Friend WithEvents btnExtrafanartsRefresh As System.Windows.Forms.Button
    Friend WithEvents btnExtrafanartsRemove As System.Windows.Forms.Button
    Friend WithEvents pbExtrafanarts As System.Windows.Forms.PictureBox
    Friend WithEvents tpFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetFanartDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveFanart As System.Windows.Forms.Button
    Friend WithEvents lblFanartSize As System.Windows.Forms.Label
    Friend WithEvents btnSetFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetFanartLocal As System.Windows.Forms.Button
    Friend WithEvents pbFanart As System.Windows.Forms.PictureBox
    Friend WithEvents tpClearLogo As System.Windows.Forms.TabPage
    Friend WithEvents lblClearLogoSize As System.Windows.Forms.Label
    Friend WithEvents btnSetClearLogoDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveClearLogo As System.Windows.Forms.Button
    Friend WithEvents btnSetClearLogoScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetClearLogoLocal As System.Windows.Forms.Button
    Friend WithEvents pbClearLogo As System.Windows.Forms.PictureBox
    Friend WithEvents tpClearArt As System.Windows.Forms.TabPage
    Friend WithEvents lblClearArtSize As System.Windows.Forms.Label
    Friend WithEvents btnSetClearArtDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveClearArt As System.Windows.Forms.Button
    Friend WithEvents btnSetClearArtScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetClearArtLocal As System.Windows.Forms.Button
    Friend WithEvents pbClearArt As System.Windows.Forms.PictureBox
    Friend WithEvents tpCharacterArt As System.Windows.Forms.TabPage
    Friend WithEvents lblCharacterArtSize As System.Windows.Forms.Label
    Friend WithEvents btnSetCharacterArtDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveCharacterArt As System.Windows.Forms.Button
    Friend WithEvents btnSetCharacterArtScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetCharacterArtLocal As System.Windows.Forms.Button
    Friend WithEvents pbCharacterArt As System.Windows.Forms.PictureBox
    Friend WithEvents tpLandscape As System.Windows.Forms.TabPage
    Friend WithEvents btnSetLandscapeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveLandscape As System.Windows.Forms.Button
    Friend WithEvents lblLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents btnSetLandscapeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetLandscapeLocal As System.Windows.Forms.Button
    Friend WithEvents pbLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents tpBanner As System.Windows.Forms.TabPage
    Friend WithEvents btnSetBannerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveBanner As System.Windows.Forms.Button
    Friend WithEvents lblBannerSize As System.Windows.Forms.Label
    Friend WithEvents btnSetBannerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetBannerLocal As System.Windows.Forms.Button
    Friend WithEvents pbBanner As System.Windows.Forms.PictureBox
    Friend WithEvents tpPoster As System.Windows.Forms.TabPage
    Friend WithEvents btnSetPosterDL As System.Windows.Forms.Button
    Friend WithEvents btnRemovePoster As System.Windows.Forms.Button
    Friend WithEvents lblPosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetPosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetPosterLocal As System.Windows.Forms.Button
    Friend WithEvents pbPoster As System.Windows.Forms.PictureBox
    Friend WithEvents tpDetails As System.Windows.Forms.TabPage
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
    Friend WithEvents btnActorEdit As System.Windows.Forms.Button
    Friend WithEvents btnActorAdd As System.Windows.Forms.Button
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnActorRemove As System.Windows.Forms.Button
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
    Friend WithEvents tcEdit As System.Windows.Forms.TabControl
    Friend WithEvents txtOriginalTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblOriginalTitle As System.Windows.Forms.Label
    Friend WithEvents lblLanguage As System.Windows.Forms.Label
    Friend WithEvents cbSourceLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents tpTheme As TabPage
    Friend WithEvents pnlThemePreview As Panel
    Friend WithEvents pnlThemePreviewNoPlayer As Panel
    Friend WithEvents tblThemePreviewNoPlayer As TableLayoutPanel
    Friend WithEvents lblThemePreviewNoPlayer As Label
    Friend WithEvents btnSetThemeDL As Button
    Friend WithEvents btnRemoveTheme As Button
    Friend WithEvents btnSetThemeScrape As Button
    Friend WithEvents btnSetThemeLocal As Button
    Friend WithEvents btnLocalThemePlay As Button
    Friend WithEvents txtLocalTheme As TextBox
    Friend WithEvents txtUserRating As TextBox
    Friend WithEvents lblUserRating As Label
End Class
