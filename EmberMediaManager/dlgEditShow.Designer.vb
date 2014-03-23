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
        Me.tcEditShow = New System.Windows.Forms.TabControl()
        Me.tpShowDetails = New System.Windows.Forms.TabPage()
        Me.btnActorDown = New System.Windows.Forms.Button()
        Me.btnActorUp = New System.Windows.Forms.Button()
        Me.txtPremiered = New System.Windows.Forms.TextBox()
        Me.clbGenre = New System.Windows.Forms.CheckedListBox()
        Me.lblStudio = New System.Windows.Forms.Label()
        Me.txtStudio = New System.Windows.Forms.TextBox()
        Me.btnEditActor = New System.Windows.Forms.Button()
        Me.btnAddActor = New System.Windows.Forms.Button()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lblActors = New System.Windows.Forms.Label()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lbMPAA = New System.Windows.Forms.ListBox()
        Me.lblGenre = New System.Windows.Forms.Label()
        Me.lblMPAA = New System.Windows.Forms.Label()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.pbStar5 = New System.Windows.Forms.PictureBox()
        Me.pbStar4 = New System.Windows.Forms.PictureBox()
        Me.pbStar3 = New System.Windows.Forms.PictureBox()
        Me.pbStar2 = New System.Windows.Forms.PictureBox()
        Me.pbStar1 = New System.Windows.Forms.PictureBox()
        Me.lblRating = New System.Windows.Forms.Label()
        Me.lblPremiered = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.tpShowPoster = New System.Windows.Forms.TabPage()
        Me.btnSetShowPosterDL = New System.Windows.Forms.Button()
        Me.btnRemoveShowPoster = New System.Windows.Forms.Button()
        Me.lblShowPosterSize = New System.Windows.Forms.Label()
        Me.btnSetShowPosterScrape = New System.Windows.Forms.Button()
        Me.btnSetShowPosterLocal = New System.Windows.Forms.Button()
        Me.pbShowPoster = New System.Windows.Forms.PictureBox()
        Me.tpShowBanner = New System.Windows.Forms.TabPage()
        Me.btnSetShowBannerDL = New System.Windows.Forms.Button()
        Me.btnRemoveShowBanner = New System.Windows.Forms.Button()
        Me.lblShowBannerSize = New System.Windows.Forms.Label()
        Me.btnSetShowBannerScrape = New System.Windows.Forms.Button()
        Me.btnSetShowBannerLocal = New System.Windows.Forms.Button()
        Me.pbShowBanner = New System.Windows.Forms.PictureBox()
        Me.tpShowLandscape = New System.Windows.Forms.TabPage()
        Me.btnSetShowLandscapeDL = New System.Windows.Forms.Button()
        Me.btnRemoveShowLandscape = New System.Windows.Forms.Button()
        Me.lblShowLandscapeSize = New System.Windows.Forms.Label()
        Me.btnSetShowLandscapeScrape = New System.Windows.Forms.Button()
        Me.btnSetShowLandscapeLocal = New System.Windows.Forms.Button()
        Me.pbShowLandscape = New System.Windows.Forms.PictureBox()
        Me.tpShowFanart = New System.Windows.Forms.TabPage()
        Me.btnSetShowFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveShowFanart = New System.Windows.Forms.Button()
        Me.lblShowFanartSize = New System.Windows.Forms.Label()
        Me.btnSetShowFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetShowFanartLocal = New System.Windows.Forms.Button()
        Me.pbShowFanart = New System.Windows.Forms.PictureBox()
        Me.tpASPoster = New System.Windows.Forms.TabPage()
        Me.lblASPosterSize = New System.Windows.Forms.Label()
        Me.btnSetASPosterDL = New System.Windows.Forms.Button()
        Me.btnRemoveASPoster = New System.Windows.Forms.Button()
        Me.btnSetASPosterScrape = New System.Windows.Forms.Button()
        Me.btnSetASPosterLocal = New System.Windows.Forms.Button()
        Me.pbASPoster = New System.Windows.Forms.PictureBox()
        Me.tpASBanner = New System.Windows.Forms.TabPage()
        Me.btnSetASBannerDL = New System.Windows.Forms.Button()
        Me.btnRemoveASBanner = New System.Windows.Forms.Button()
        Me.lblASBannerSize = New System.Windows.Forms.Label()
        Me.btnSetASBannerScrape = New System.Windows.Forms.Button()
        Me.btnSetASBannerLocal = New System.Windows.Forms.Button()
        Me.pbASBanner = New System.Windows.Forms.PictureBox()
        Me.tpASLandscape = New System.Windows.Forms.TabPage()
        Me.btnSetASLandscapeDL = New System.Windows.Forms.Button()
        Me.btnRemoveASLandscape = New System.Windows.Forms.Button()
        Me.lblASLandscapeSize = New System.Windows.Forms.Label()
        Me.btnSetASLandscapeScrape = New System.Windows.Forms.Button()
        Me.btnSetASLandscapeLocal = New System.Windows.Forms.Button()
        Me.pbASLandscape = New System.Windows.Forms.PictureBox()
        Me.tpASFanart = New System.Windows.Forms.TabPage()
        Me.btnSetASFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveASFanart = New System.Windows.Forms.Button()
        Me.lblASFanartSize = New System.Windows.Forms.Label()
        Me.btnSetASFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetASFanartLocal = New System.Windows.Forms.Button()
        Me.pbASFanart = New System.Windows.Forms.PictureBox()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.cbOrdering = New System.Windows.Forms.ComboBox()
        Me.lblOrdering = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcEditShow.SuspendLayout()
        Me.tpShowDetails.SuspendLayout()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowPoster.SuspendLayout()
        CType(Me.pbShowPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowBanner.SuspendLayout()
        CType(Me.pbShowBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowLandscape.SuspendLayout()
        CType(Me.pbShowLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpShowFanart.SuspendLayout()
        CType(Me.pbShowFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpASPoster.SuspendLayout()
        CType(Me.pbASPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpASBanner.SuspendLayout()
        CType(Me.pbASBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpASLandscape.SuspendLayout()
        CType(Me.pbASLandscape, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpASFanart.SuspendLayout()
        CType(Me.pbASFanart, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'tcEditShow
        '
        Me.tcEditShow.Controls.Add(Me.tpShowDetails)
        Me.tcEditShow.Controls.Add(Me.tpShowPoster)
        Me.tcEditShow.Controls.Add(Me.tpShowBanner)
        Me.tcEditShow.Controls.Add(Me.tpShowLandscape)
        Me.tcEditShow.Controls.Add(Me.tpShowFanart)
        Me.tcEditShow.Controls.Add(Me.tpASPoster)
        Me.tcEditShow.Controls.Add(Me.tpASBanner)
        Me.tcEditShow.Controls.Add(Me.tpASLandscape)
        Me.tcEditShow.Controls.Add(Me.tpASFanart)
        Me.tcEditShow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcEditShow.Location = New System.Drawing.Point(4, 70)
        Me.tcEditShow.Name = "tcEditShow"
        Me.tcEditShow.SelectedIndex = 0
        Me.tcEditShow.Size = New System.Drawing.Size(844, 478)
        Me.tcEditShow.TabIndex = 3
        '
        'tpShowDetails
        '
        Me.tpShowDetails.Controls.Add(Me.lblStatus)
        Me.tpShowDetails.Controls.Add(Me.txtStatus)
        Me.tpShowDetails.Controls.Add(Me.btnActorDown)
        Me.tpShowDetails.Controls.Add(Me.btnActorUp)
        Me.tpShowDetails.Controls.Add(Me.txtPremiered)
        Me.tpShowDetails.Controls.Add(Me.clbGenre)
        Me.tpShowDetails.Controls.Add(Me.lblStudio)
        Me.tpShowDetails.Controls.Add(Me.txtStudio)
        Me.tpShowDetails.Controls.Add(Me.btnEditActor)
        Me.tpShowDetails.Controls.Add(Me.btnAddActor)
        Me.tpShowDetails.Controls.Add(Me.btnManual)
        Me.tpShowDetails.Controls.Add(Me.btnRemove)
        Me.tpShowDetails.Controls.Add(Me.lblActors)
        Me.tpShowDetails.Controls.Add(Me.lvActors)
        Me.tpShowDetails.Controls.Add(Me.lbMPAA)
        Me.tpShowDetails.Controls.Add(Me.lblGenre)
        Me.tpShowDetails.Controls.Add(Me.lblMPAA)
        Me.tpShowDetails.Controls.Add(Me.lblPlot)
        Me.tpShowDetails.Controls.Add(Me.txtPlot)
        Me.tpShowDetails.Controls.Add(Me.pbStar5)
        Me.tpShowDetails.Controls.Add(Me.pbStar4)
        Me.tpShowDetails.Controls.Add(Me.pbStar3)
        Me.tpShowDetails.Controls.Add(Me.pbStar2)
        Me.tpShowDetails.Controls.Add(Me.pbStar1)
        Me.tpShowDetails.Controls.Add(Me.lblRating)
        Me.tpShowDetails.Controls.Add(Me.lblPremiered)
        Me.tpShowDetails.Controls.Add(Me.lblTitle)
        Me.tpShowDetails.Controls.Add(Me.txtTitle)
        Me.tpShowDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpShowDetails.Name = "tpShowDetails"
        Me.tpShowDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpShowDetails.Size = New System.Drawing.Size(836, 452)
        Me.tpShowDetails.TabIndex = 0
        Me.tpShowDetails.Text = "Details"
        Me.tpShowDetails.UseVisualStyleBackColor = True
        '
        'btnActorDown
        '
        Me.btnActorDown.Image = CType(resources.GetObject("btnActorDown.Image"), System.Drawing.Image)
        Me.btnActorDown.Location = New System.Drawing.Point(434, 420)
        Me.btnActorDown.Name = "btnActorDown"
        Me.btnActorDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorDown.TabIndex = 14
        Me.btnActorDown.UseVisualStyleBackColor = True
        '
        'btnActorUp
        '
        Me.btnActorUp.Image = CType(resources.GetObject("btnActorUp.Image"), System.Drawing.Image)
        Me.btnActorUp.Location = New System.Drawing.Point(410, 420)
        Me.btnActorUp.Name = "btnActorUp"
        Me.btnActorUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorUp.TabIndex = 13
        Me.btnActorUp.UseVisualStyleBackColor = True
        '
        'txtPremiered
        '
        Me.txtPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPremiered.Location = New System.Drawing.Point(7, 69)
        Me.txtPremiered.Name = "txtPremiered"
        Me.txtPremiered.Size = New System.Drawing.Size(192, 22)
        Me.txtPremiered.TabIndex = 3
        '
        'clbGenre
        '
        Me.clbGenre.CheckOnClick = True
        Me.clbGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clbGenre.FormattingEnabled = True
        Me.clbGenre.IntegralHeight = False
        Me.clbGenre.Location = New System.Drawing.Point(7, 160)
        Me.clbGenre.Name = "clbGenre"
        Me.clbGenre.Size = New System.Drawing.Size(192, 283)
        Me.clbGenre.Sorted = True
        Me.clbGenre.TabIndex = 6
        '
        'lblStudio
        '
        Me.lblStudio.AutoSize = True
        Me.lblStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStudio.Location = New System.Drawing.Point(635, 273)
        Me.lblStudio.Name = "lblStudio"
        Me.lblStudio.Size = New System.Drawing.Size(44, 13)
        Me.lblStudio.TabIndex = 18
        Me.lblStudio.Text = "Studio:"
        '
        'txtStudio
        '
        Me.txtStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStudio.Location = New System.Drawing.Point(635, 289)
        Me.txtStudio.Name = "txtStudio"
        Me.txtStudio.Size = New System.Drawing.Size(193, 22)
        Me.txtStudio.TabIndex = 19
        '
        'btnEditActor
        '
        Me.btnEditActor.Image = CType(resources.GetObject("btnEditActor.Image"), System.Drawing.Image)
        Me.btnEditActor.Location = New System.Drawing.Point(246, 420)
        Me.btnEditActor.Name = "btnEditActor"
        Me.btnEditActor.Size = New System.Drawing.Size(23, 23)
        Me.btnEditActor.TabIndex = 12
        Me.btnEditActor.UseVisualStyleBackColor = True
        '
        'btnAddActor
        '
        Me.btnAddActor.Image = CType(resources.GetObject("btnAddActor.Image"), System.Drawing.Image)
        Me.btnAddActor.Location = New System.Drawing.Point(217, 420)
        Me.btnAddActor.Name = "btnAddActor"
        Me.btnAddActor.Size = New System.Drawing.Size(23, 23)
        Me.btnAddActor.TabIndex = 11
        Me.btnAddActor.UseVisualStyleBackColor = True
        '
        'btnManual
        '
        Me.btnManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManual.Location = New System.Drawing.Point(738, 423)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(92, 23)
        Me.btnManual.TabIndex = 20
        Me.btnManual.Text = "Manual Edit"
        Me.btnManual.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), System.Drawing.Image)
        Me.btnRemove.Location = New System.Drawing.Point(602, 420)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnRemove.TabIndex = 15
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'lblActors
        '
        Me.lblActors.AutoSize = True
        Me.lblActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblActors.Location = New System.Drawing.Point(218, 139)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(43, 13)
        Me.lblActors.TabIndex = 9
        Me.lblActors.Text = "Actors:"
        '
        'lvActors
        '
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colRole, Me.colThumb})
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.Location = New System.Drawing.Point(217, 155)
        Me.lvActors.Name = "lvActors"
        Me.lvActors.Size = New System.Drawing.Size(408, 259)
        Me.lvActors.TabIndex = 10
        Me.lvActors.UseCompatibleStateImageBehavior = False
        Me.lvActors.View = System.Windows.Forms.View.Details
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
        Me.lblGenre.Location = New System.Drawing.Point(7, 144)
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
        Me.lblPlot.Location = New System.Drawing.Point(218, 7)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(31, 13)
        Me.lblPlot.TabIndex = 7
        Me.lblPlot.Text = "Plot:"
        '
        'txtPlot
        '
        Me.txtPlot.AcceptsReturn = True
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(217, 26)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.Size = New System.Drawing.Size(611, 108)
        Me.txtPlot.TabIndex = 8
        '
        'pbStar5
        '
        Me.pbStar5.Location = New System.Drawing.Point(103, 112)
        Me.pbStar5.Name = "pbStar5"
        Me.pbStar5.Size = New System.Drawing.Size(24, 24)
        Me.pbStar5.TabIndex = 67
        Me.pbStar5.TabStop = False
        '
        'pbStar4
        '
        Me.pbStar4.Location = New System.Drawing.Point(79, 112)
        Me.pbStar4.Name = "pbStar4"
        Me.pbStar4.Size = New System.Drawing.Size(24, 24)
        Me.pbStar4.TabIndex = 66
        Me.pbStar4.TabStop = False
        '
        'pbStar3
        '
        Me.pbStar3.Location = New System.Drawing.Point(55, 112)
        Me.pbStar3.Name = "pbStar3"
        Me.pbStar3.Size = New System.Drawing.Size(24, 24)
        Me.pbStar3.TabIndex = 65
        Me.pbStar3.TabStop = False
        '
        'pbStar2
        '
        Me.pbStar2.Location = New System.Drawing.Point(31, 112)
        Me.pbStar2.Name = "pbStar2"
        Me.pbStar2.Size = New System.Drawing.Size(24, 24)
        Me.pbStar2.TabIndex = 64
        Me.pbStar2.TabStop = False
        '
        'pbStar1
        '
        Me.pbStar1.Location = New System.Drawing.Point(7, 112)
        Me.pbStar1.Name = "pbStar1"
        Me.pbStar1.Size = New System.Drawing.Size(24, 24)
        Me.pbStar1.TabIndex = 63
        Me.pbStar1.TabStop = False
        '
        'lblRating
        '
        Me.lblRating.AutoSize = True
        Me.lblRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRating.Location = New System.Drawing.Point(7, 96)
        Me.lblRating.Name = "lblRating"
        Me.lblRating.Size = New System.Drawing.Size(44, 13)
        Me.lblRating.TabIndex = 4
        Me.lblRating.Text = "Rating:"
        '
        'lblPremiered
        '
        Me.lblPremiered.AutoSize = True
        Me.lblPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPremiered.Location = New System.Drawing.Point(7, 53)
        Me.lblPremiered.Name = "lblPremiered"
        Me.lblPremiered.Size = New System.Drawing.Size(63, 13)
        Me.lblPremiered.TabIndex = 2
        Me.lblPremiered.Text = "Premiered:"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(7, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(32, 13)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Title:"
        '
        'txtTitle
        '
        Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTitle.Location = New System.Drawing.Point(7, 26)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(192, 22)
        Me.txtTitle.TabIndex = 1
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
        Me.tpShowPoster.Size = New System.Drawing.Size(836, 452)
        Me.tpShowPoster.TabIndex = 1
        Me.tpShowPoster.Text = "Poster"
        Me.tpShowPoster.UseVisualStyleBackColor = True
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
        Me.tpShowBanner.Size = New System.Drawing.Size(836, 452)
        Me.tpShowBanner.TabIndex = 4
        Me.tpShowBanner.Text = "Banner"
        Me.tpShowBanner.UseVisualStyleBackColor = True
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
        Me.tpShowLandscape.Size = New System.Drawing.Size(836, 452)
        Me.tpShowLandscape.TabIndex = 7
        Me.tpShowLandscape.Text = "Landscape"
        Me.tpShowLandscape.UseVisualStyleBackColor = True
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
        Me.tpShowFanart.Size = New System.Drawing.Size(836, 452)
        Me.tpShowFanart.TabIndex = 2
        Me.tpShowFanart.Text = "Fanart"
        Me.tpShowFanart.UseVisualStyleBackColor = True
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
        'tpASPoster
        '
        Me.tpASPoster.Controls.Add(Me.lblASPosterSize)
        Me.tpASPoster.Controls.Add(Me.btnSetASPosterDL)
        Me.tpASPoster.Controls.Add(Me.btnRemoveASPoster)
        Me.tpASPoster.Controls.Add(Me.btnSetASPosterScrape)
        Me.tpASPoster.Controls.Add(Me.btnSetASPosterLocal)
        Me.tpASPoster.Controls.Add(Me.pbASPoster)
        Me.tpASPoster.Location = New System.Drawing.Point(4, 22)
        Me.tpASPoster.Name = "tpASPoster"
        Me.tpASPoster.Size = New System.Drawing.Size(836, 452)
        Me.tpASPoster.TabIndex = 3
        Me.tpASPoster.Text = "All Seasons Poster"
        Me.tpASPoster.UseVisualStyleBackColor = True
        '
        'lblASPosterSize
        '
        Me.lblASPosterSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblASPosterSize.Location = New System.Drawing.Point(8, 8)
        Me.lblASPosterSize.Name = "lblASPosterSize"
        Me.lblASPosterSize.Size = New System.Drawing.Size(104, 23)
        Me.lblASPosterSize.TabIndex = 0
        Me.lblASPosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblASPosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblASPosterSize.Visible = False
        '
        'btnSetASPosterDL
        '
        Me.btnSetASPosterDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASPosterDL.Image = CType(resources.GetObject("btnSetASPosterDL.Image"), System.Drawing.Image)
        Me.btnSetASPosterDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASPosterDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetASPosterDL.Name = "btnSetASPosterDL"
        Me.btnSetASPosterDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASPosterDL.TabIndex = 3
        Me.btnSetASPosterDL.Text = "Change Poster (Download)"
        Me.btnSetASPosterDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASPosterDL.UseVisualStyleBackColor = True
        '
        'btnRemoveASPoster
        '
        Me.btnRemoveASPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveASPoster.Image = CType(resources.GetObject("btnRemoveASPoster.Image"), System.Drawing.Image)
        Me.btnRemoveASPoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveASPoster.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveASPoster.Name = "btnRemoveASPoster"
        Me.btnRemoveASPoster.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveASPoster.TabIndex = 4
        Me.btnRemoveASPoster.Text = "Remove Poster"
        Me.btnRemoveASPoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveASPoster.UseVisualStyleBackColor = True
        '
        'btnSetASPosterScrape
        '
        Me.btnSetASPosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASPosterScrape.Image = CType(resources.GetObject("btnSetASPosterScrape.Image"), System.Drawing.Image)
        Me.btnSetASPosterScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASPosterScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetASPosterScrape.Name = "btnSetASPosterScrape"
        Me.btnSetASPosterScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASPosterScrape.TabIndex = 2
        Me.btnSetASPosterScrape.Text = "Change Poster (Scrape)"
        Me.btnSetASPosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASPosterScrape.UseVisualStyleBackColor = True
        '
        'btnSetASPosterLocal
        '
        Me.btnSetASPosterLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASPosterLocal.Image = CType(resources.GetObject("btnSetASPosterLocal.Image"), System.Drawing.Image)
        Me.btnSetASPosterLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASPosterLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetASPosterLocal.Name = "btnSetASPosterLocal"
        Me.btnSetASPosterLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASPosterLocal.TabIndex = 1
        Me.btnSetASPosterLocal.Text = "Change Poster (Local)"
        Me.btnSetASPosterLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASPosterLocal.UseVisualStyleBackColor = True
        '
        'pbASPoster
        '
        Me.pbASPoster.BackColor = System.Drawing.Color.DimGray
        Me.pbASPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbASPoster.Location = New System.Drawing.Point(6, 6)
        Me.pbASPoster.Name = "pbASPoster"
        Me.pbASPoster.Size = New System.Drawing.Size(724, 440)
        Me.pbASPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbASPoster.TabIndex = 28
        Me.pbASPoster.TabStop = False
        '
        'tpASBanner
        '
        Me.tpASBanner.Controls.Add(Me.btnSetASBannerDL)
        Me.tpASBanner.Controls.Add(Me.btnRemoveASBanner)
        Me.tpASBanner.Controls.Add(Me.lblASBannerSize)
        Me.tpASBanner.Controls.Add(Me.btnSetASBannerScrape)
        Me.tpASBanner.Controls.Add(Me.btnSetASBannerLocal)
        Me.tpASBanner.Controls.Add(Me.pbASBanner)
        Me.tpASBanner.Location = New System.Drawing.Point(4, 22)
        Me.tpASBanner.Name = "tpASBanner"
        Me.tpASBanner.Padding = New System.Windows.Forms.Padding(3)
        Me.tpASBanner.Size = New System.Drawing.Size(836, 452)
        Me.tpASBanner.TabIndex = 5
        Me.tpASBanner.Text = "All Seasons Banner"
        Me.tpASBanner.UseVisualStyleBackColor = True
        '
        'btnSetASBannerDL
        '
        Me.btnSetASBannerDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASBannerDL.Image = CType(resources.GetObject("btnSetASBannerDL.Image"), System.Drawing.Image)
        Me.btnSetASBannerDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASBannerDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetASBannerDL.Name = "btnSetASBannerDL"
        Me.btnSetASBannerDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASBannerDL.TabIndex = 15
        Me.btnSetASBannerDL.Text = "Change Banner (Download)"
        Me.btnSetASBannerDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASBannerDL.UseVisualStyleBackColor = True
        '
        'btnRemoveASBanner
        '
        Me.btnRemoveASBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveASBanner.Image = CType(resources.GetObject("btnRemoveASBanner.Image"), System.Drawing.Image)
        Me.btnRemoveASBanner.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveASBanner.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveASBanner.Name = "btnRemoveASBanner"
        Me.btnRemoveASBanner.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveASBanner.TabIndex = 16
        Me.btnRemoveASBanner.Text = "Remove Banner"
        Me.btnRemoveASBanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveASBanner.UseVisualStyleBackColor = True
        '
        'lblASBannerSize
        '
        Me.lblASBannerSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblASBannerSize.Location = New System.Drawing.Point(8, 8)
        Me.lblASBannerSize.Name = "lblASBannerSize"
        Me.lblASBannerSize.Size = New System.Drawing.Size(104, 23)
        Me.lblASBannerSize.TabIndex = 11
        Me.lblASBannerSize.Text = "Size: (XXXXxXXXX)"
        Me.lblASBannerSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblASBannerSize.Visible = False
        '
        'btnSetASBannerScrape
        '
        Me.btnSetASBannerScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASBannerScrape.Image = CType(resources.GetObject("btnSetASBannerScrape.Image"), System.Drawing.Image)
        Me.btnSetASBannerScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASBannerScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetASBannerScrape.Name = "btnSetASBannerScrape"
        Me.btnSetASBannerScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASBannerScrape.TabIndex = 14
        Me.btnSetASBannerScrape.Text = "Change Banner (Scrape)"
        Me.btnSetASBannerScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASBannerScrape.UseVisualStyleBackColor = True
        '
        'btnSetASBannerLocal
        '
        Me.btnSetASBannerLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASBannerLocal.Image = CType(resources.GetObject("btnSetASBannerLocal.Image"), System.Drawing.Image)
        Me.btnSetASBannerLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASBannerLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetASBannerLocal.Name = "btnSetASBannerLocal"
        Me.btnSetASBannerLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASBannerLocal.TabIndex = 13
        Me.btnSetASBannerLocal.Text = "Change Banner (Local)"
        Me.btnSetASBannerLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASBannerLocal.UseVisualStyleBackColor = True
        '
        'pbASBanner
        '
        Me.pbASBanner.BackColor = System.Drawing.Color.DimGray
        Me.pbASBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbASBanner.Location = New System.Drawing.Point(6, 6)
        Me.pbASBanner.Name = "pbASBanner"
        Me.pbASBanner.Size = New System.Drawing.Size(724, 440)
        Me.pbASBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbASBanner.TabIndex = 12
        Me.pbASBanner.TabStop = False
        '
        'tpASLandscape
        '
        Me.tpASLandscape.Controls.Add(Me.btnSetASLandscapeDL)
        Me.tpASLandscape.Controls.Add(Me.btnRemoveASLandscape)
        Me.tpASLandscape.Controls.Add(Me.lblASLandscapeSize)
        Me.tpASLandscape.Controls.Add(Me.btnSetASLandscapeScrape)
        Me.tpASLandscape.Controls.Add(Me.btnSetASLandscapeLocal)
        Me.tpASLandscape.Controls.Add(Me.pbASLandscape)
        Me.tpASLandscape.Location = New System.Drawing.Point(4, 22)
        Me.tpASLandscape.Name = "tpASLandscape"
        Me.tpASLandscape.Size = New System.Drawing.Size(836, 452)
        Me.tpASLandscape.TabIndex = 8
        Me.tpASLandscape.Text = "All Seasons Landscape"
        Me.tpASLandscape.UseVisualStyleBackColor = True
        '
        'btnSetASLandscapeDL
        '
        Me.btnSetASLandscapeDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASLandscapeDL.Image = CType(resources.GetObject("btnSetASLandscapeDL.Image"), System.Drawing.Image)
        Me.btnSetASLandscapeDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASLandscapeDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetASLandscapeDL.Name = "btnSetASLandscapeDL"
        Me.btnSetASLandscapeDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASLandscapeDL.TabIndex = 15
        Me.btnSetASLandscapeDL.Text = "Change Landscape (Download)"
        Me.btnSetASLandscapeDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASLandscapeDL.UseVisualStyleBackColor = True
        '
        'btnRemoveASLandscape
        '
        Me.btnRemoveASLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveASLandscape.Image = CType(resources.GetObject("btnRemoveASLandscape.Image"), System.Drawing.Image)
        Me.btnRemoveASLandscape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveASLandscape.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveASLandscape.Name = "btnRemoveASLandscape"
        Me.btnRemoveASLandscape.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveASLandscape.TabIndex = 16
        Me.btnRemoveASLandscape.Text = "Remove Landscape"
        Me.btnRemoveASLandscape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveASLandscape.UseVisualStyleBackColor = True
        '
        'lblASLandscapeSize
        '
        Me.lblASLandscapeSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblASLandscapeSize.Location = New System.Drawing.Point(8, 8)
        Me.lblASLandscapeSize.Name = "lblASLandscapeSize"
        Me.lblASLandscapeSize.Size = New System.Drawing.Size(104, 23)
        Me.lblASLandscapeSize.TabIndex = 11
        Me.lblASLandscapeSize.Text = "Size: (XXXXxXXXX)"
        Me.lblASLandscapeSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblASLandscapeSize.Visible = False
        '
        'btnSetASLandscapeScrape
        '
        Me.btnSetASLandscapeScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASLandscapeScrape.Image = CType(resources.GetObject("btnSetASLandscapeScrape.Image"), System.Drawing.Image)
        Me.btnSetASLandscapeScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASLandscapeScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetASLandscapeScrape.Name = "btnSetASLandscapeScrape"
        Me.btnSetASLandscapeScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASLandscapeScrape.TabIndex = 14
        Me.btnSetASLandscapeScrape.Text = "Change Landscape (Scrape)"
        Me.btnSetASLandscapeScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASLandscapeScrape.UseVisualStyleBackColor = True
        '
        'btnSetASLandscapeLocal
        '
        Me.btnSetASLandscapeLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASLandscapeLocal.Image = CType(resources.GetObject("btnSetASLandscapeLocal.Image"), System.Drawing.Image)
        Me.btnSetASLandscapeLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASLandscapeLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetASLandscapeLocal.Name = "btnSetASLandscapeLocal"
        Me.btnSetASLandscapeLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASLandscapeLocal.TabIndex = 13
        Me.btnSetASLandscapeLocal.Text = "Change Landscape (Local)"
        Me.btnSetASLandscapeLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASLandscapeLocal.UseVisualStyleBackColor = True
        '
        'pbASLandscape
        '
        Me.pbASLandscape.BackColor = System.Drawing.Color.DimGray
        Me.pbASLandscape.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbASLandscape.Location = New System.Drawing.Point(6, 6)
        Me.pbASLandscape.Name = "pbASLandscape"
        Me.pbASLandscape.Size = New System.Drawing.Size(724, 440)
        Me.pbASLandscape.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbASLandscape.TabIndex = 12
        Me.pbASLandscape.TabStop = False
        '
        'tpASFanart
        '
        Me.tpASFanart.Controls.Add(Me.btnSetASFanartDL)
        Me.tpASFanart.Controls.Add(Me.btnRemoveASFanart)
        Me.tpASFanart.Controls.Add(Me.lblASFanartSize)
        Me.tpASFanart.Controls.Add(Me.btnSetASFanartScrape)
        Me.tpASFanart.Controls.Add(Me.btnSetASFanartLocal)
        Me.tpASFanart.Controls.Add(Me.pbASFanart)
        Me.tpASFanart.Location = New System.Drawing.Point(4, 22)
        Me.tpASFanart.Name = "tpASFanart"
        Me.tpASFanart.Size = New System.Drawing.Size(836, 452)
        Me.tpASFanart.TabIndex = 6
        Me.tpASFanart.Text = "All Seasons Fanart"
        Me.tpASFanart.UseVisualStyleBackColor = True
        '
        'btnSetASFanartDL
        '
        Me.btnSetASFanartDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASFanartDL.Image = CType(resources.GetObject("btnSetASFanartDL.Image"), System.Drawing.Image)
        Me.btnSetASFanartDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASFanartDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetASFanartDL.Name = "btnSetASFanartDL"
        Me.btnSetASFanartDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASFanartDL.TabIndex = 9
        Me.btnSetASFanartDL.Text = "Change Fanart (Download)"
        Me.btnSetASFanartDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASFanartDL.UseVisualStyleBackColor = True
        '
        'btnRemoveASFanart
        '
        Me.btnRemoveASFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveASFanart.Image = CType(resources.GetObject("btnRemoveASFanart.Image"), System.Drawing.Image)
        Me.btnRemoveASFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveASFanart.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveASFanart.Name = "btnRemoveASFanart"
        Me.btnRemoveASFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveASFanart.TabIndex = 10
        Me.btnRemoveASFanart.Text = "Remove Fanart"
        Me.btnRemoveASFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveASFanart.UseVisualStyleBackColor = True
        '
        'lblASFanartSize
        '
        Me.lblASFanartSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblASFanartSize.Location = New System.Drawing.Point(8, 8)
        Me.lblASFanartSize.Name = "lblASFanartSize"
        Me.lblASFanartSize.Size = New System.Drawing.Size(104, 23)
        Me.lblASFanartSize.TabIndex = 5
        Me.lblASFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblASFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblASFanartSize.Visible = False
        '
        'btnSetASFanartScrape
        '
        Me.btnSetASFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASFanartScrape.Image = CType(resources.GetObject("btnSetASFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetASFanartScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASFanartScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetASFanartScrape.Name = "btnSetASFanartScrape"
        Me.btnSetASFanartScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASFanartScrape.TabIndex = 8
        Me.btnSetASFanartScrape.Text = "Change Fanart (Scrape)"
        Me.btnSetASFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASFanartScrape.UseVisualStyleBackColor = True
        '
        'btnSetASFanartLocal
        '
        Me.btnSetASFanartLocal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetASFanartLocal.Image = CType(resources.GetObject("btnSetASFanartLocal.Image"), System.Drawing.Image)
        Me.btnSetASFanartLocal.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetASFanartLocal.Location = New System.Drawing.Point(735, 6)
        Me.btnSetASFanartLocal.Name = "btnSetASFanartLocal"
        Me.btnSetASFanartLocal.Size = New System.Drawing.Size(96, 83)
        Me.btnSetASFanartLocal.TabIndex = 6
        Me.btnSetASFanartLocal.Text = "Change Fanart (Local)"
        Me.btnSetASFanartLocal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetASFanartLocal.UseVisualStyleBackColor = True
        '
        'pbASFanart
        '
        Me.pbASFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbASFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbASFanart.Location = New System.Drawing.Point(6, 6)
        Me.pbASFanart.Name = "pbASFanart"
        Me.pbASFanart.Size = New System.Drawing.Size(724, 440)
        Me.pbASFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbASFanart.TabIndex = 7
        Me.pbASFanart.TabStop = False
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
        'cbOrdering
        '
        Me.cbOrdering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOrdering.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbOrdering.FormattingEnabled = True
        Me.cbOrdering.Location = New System.Drawing.Point(111, 553)
        Me.cbOrdering.Name = "cbOrdering"
        Me.cbOrdering.Size = New System.Drawing.Size(166, 21)
        Me.cbOrdering.TabIndex = 5
        '
        'lblOrdering
        '
        Me.lblOrdering.AutoSize = True
        Me.lblOrdering.Location = New System.Drawing.Point(5, 558)
        Me.lblOrdering.Name = "lblOrdering"
        Me.lblOrdering.Size = New System.Drawing.Size(101, 13)
        Me.lblOrdering.TabIndex = 4
        Me.lblOrdering.Text = "Episode Ordering:"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(635, 330)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(42, 13)
        Me.lblStatus.TabIndex = 68
        Me.lblStatus.Text = "Status:"
        '
        'txtStatus
        '
        Me.txtStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(635, 346)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(193, 22)
        Me.txtStatus.TabIndex = 69
        '
        'dlgEditShow
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(854, 582)
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
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Show"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcEditShow.ResumeLayout(False)
        Me.tpShowDetails.ResumeLayout(False)
        Me.tpShowDetails.PerformLayout()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowPoster.ResumeLayout(False)
        CType(Me.pbShowPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowBanner.ResumeLayout(False)
        CType(Me.pbShowBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowLandscape.ResumeLayout(False)
        CType(Me.pbShowLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpShowFanart.ResumeLayout(False)
        CType(Me.pbShowFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpASPoster.ResumeLayout(False)
        CType(Me.pbASPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpASBanner.ResumeLayout(False)
        CType(Me.pbASBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpASLandscape.ResumeLayout(False)
        CType(Me.pbASLandscape, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpASFanart.ResumeLayout(False)
        CType(Me.pbASFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents tcEditShow As System.Windows.Forms.TabControl
    Friend WithEvents tpShowDetails As System.Windows.Forms.TabPage
    Friend WithEvents clbGenre As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblStudio As System.Windows.Forms.Label
    Friend WithEvents txtStudio As System.Windows.Forms.TextBox
    Friend WithEvents btnEditActor As System.Windows.Forms.Button
    Friend WithEvents btnAddActor As System.Windows.Forms.Button
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents lblActors As System.Windows.Forms.Label
    Friend WithEvents lvActors As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRole As System.Windows.Forms.ColumnHeader
    Friend WithEvents colThumb As System.Windows.Forms.ColumnHeader
    Friend WithEvents lbMPAA As System.Windows.Forms.ListBox
    Friend WithEvents lblGenre As System.Windows.Forms.Label
    Friend WithEvents lblMPAA As System.Windows.Forms.Label
    Friend WithEvents lblPlot As System.Windows.Forms.Label
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents pbStar5 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar4 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar3 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar2 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblRating As System.Windows.Forms.Label
    Friend WithEvents lblPremiered As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents tpShowPoster As System.Windows.Forms.TabPage
    Friend WithEvents btnSetShowPosterDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowPoster As System.Windows.Forms.Button
    Friend WithEvents lblShowPosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowPosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowPosterLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowPoster As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetShowFanartDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowFanart As System.Windows.Forms.Button
    Friend WithEvents lblShowFanartSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowFanartLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowFanart As System.Windows.Forms.PictureBox
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents txtPremiered As System.Windows.Forms.TextBox
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnActorDown As System.Windows.Forms.Button
    Friend WithEvents btnActorUp As System.Windows.Forms.Button
    Friend WithEvents tpASPoster As System.Windows.Forms.TabPage
    Friend WithEvents lblASPosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetASPosterDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveASPoster As System.Windows.Forms.Button
    Friend WithEvents btnSetASPosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetASPosterLocal As System.Windows.Forms.Button
    Friend WithEvents pbASPoster As System.Windows.Forms.PictureBox
    Friend WithEvents cbOrdering As System.Windows.Forms.ComboBox
    Friend WithEvents lblOrdering As System.Windows.Forms.Label
    Friend WithEvents tpShowBanner As System.Windows.Forms.TabPage
    Friend WithEvents btnSetShowBannerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowBanner As System.Windows.Forms.Button
    Friend WithEvents lblShowBannerSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowBannerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowBannerLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowBanner As System.Windows.Forms.PictureBox
    Friend WithEvents tpASBanner As System.Windows.Forms.TabPage
    Friend WithEvents btnSetASBannerDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveASBanner As System.Windows.Forms.Button
    Friend WithEvents lblASBannerSize As System.Windows.Forms.Label
    Friend WithEvents btnSetASBannerScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetASBannerLocal As System.Windows.Forms.Button
    Friend WithEvents pbASBanner As System.Windows.Forms.PictureBox
    Friend WithEvents tpASFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetASFanartDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveASFanart As System.Windows.Forms.Button
    Friend WithEvents lblASFanartSize As System.Windows.Forms.Label
    Friend WithEvents btnSetASFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetASFanartLocal As System.Windows.Forms.Button
    Friend WithEvents pbASFanart As System.Windows.Forms.PictureBox
    Friend WithEvents tpShowLandscape As System.Windows.Forms.TabPage
    Friend WithEvents btnSetShowLandscapeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveShowLandscape As System.Windows.Forms.Button
    Friend WithEvents lblShowLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents btnSetShowLandscapeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetShowLandscapeLocal As System.Windows.Forms.Button
    Friend WithEvents pbShowLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents tpASLandscape As System.Windows.Forms.TabPage
    Friend WithEvents btnSetASLandscapeDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveASLandscape As System.Windows.Forms.Button
    Friend WithEvents lblASLandscapeSize As System.Windows.Forms.Label
    Friend WithEvents btnSetASLandscapeScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetASLandscapeLocal As System.Windows.Forms.Button
    Friend WithEvents pbASLandscape As System.Windows.Forms.PictureBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox

End Class
