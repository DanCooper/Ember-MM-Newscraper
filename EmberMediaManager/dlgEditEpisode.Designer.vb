﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgEditEpisode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgEditEpisode))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.tcEditEpisode = New System.Windows.Forms.TabControl()
        Me.tpEpsiodeDetails = New System.Windows.Forms.TabPage()
        Me.btnActorDown = New System.Windows.Forms.Button()
        Me.btnActorUp = New System.Windows.Forms.Button()
        Me.txtAired = New System.Windows.Forms.TextBox()
        Me.txtEpisode = New System.Windows.Forms.TextBox()
        Me.lblEpisode = New System.Windows.Forms.Label()
        Me.txtSeason = New System.Windows.Forms.TextBox()
        Me.lblSeason = New System.Windows.Forms.Label()
        Me.lblCredits = New System.Windows.Forms.Label()
        Me.txtCredits = New System.Windows.Forms.TextBox()
        Me.btnEditActor = New System.Windows.Forms.Button()
        Me.btnAddActor = New System.Windows.Forms.Button()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lblActors = New System.Windows.Forms.Label()
        Me.lvActors = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRole = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colThumb = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblDirector = New System.Windows.Forms.Label()
        Me.txtDirector = New System.Windows.Forms.TextBox()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.pbStar5 = New System.Windows.Forms.PictureBox()
        Me.pbStar4 = New System.Windows.Forms.PictureBox()
        Me.pbStar3 = New System.Windows.Forms.PictureBox()
        Me.pbStar2 = New System.Windows.Forms.PictureBox()
        Me.pbStar1 = New System.Windows.Forms.PictureBox()
        Me.lblRating = New System.Windows.Forms.Label()
        Me.lblAired = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.tpEpisodePoster = New System.Windows.Forms.TabPage()
        Me.btnSetEpisodePosterDL = New System.Windows.Forms.Button()
        Me.btnRemoveEpisodePoster = New System.Windows.Forms.Button()
        Me.lblEpisodePosterSize = New System.Windows.Forms.Label()
        Me.btnSetEpisodePosterScrape = New System.Windows.Forms.Button()
        Me.btnSetEpisodePoster = New System.Windows.Forms.Button()
        Me.pbEpisodePoster = New System.Windows.Forms.PictureBox()
        Me.tpEpisodeFanart = New System.Windows.Forms.TabPage()
        Me.lblEpisodeFanartSize = New System.Windows.Forms.Label()
        Me.btnSetEpisodeFanartDL = New System.Windows.Forms.Button()
        Me.btnRemoveEpisodeFanart = New System.Windows.Forms.Button()
        Me.btnSetEpisodeFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetEpisodeFanart = New System.Windows.Forms.Button()
        Me.pbEpisodeFanart = New System.Windows.Forms.PictureBox()
        Me.tpFrameExtraction = New System.Windows.Forms.TabPage()
        Me.pnlFrameExtrator = New System.Windows.Forms.Panel()
        Me.tpEpisodeMetaData = New System.Windows.Forms.TabPage()
        Me.pnlFileInfo = New System.Windows.Forms.Panel()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.chkWatched = New System.Windows.Forms.CheckBox()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcEditEpisode.SuspendLayout()
        Me.tpEpsiodeDetails.SuspendLayout()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpEpisodePoster.SuspendLayout()
        CType(Me.pbEpisodePoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpEpisodeFanart.SuspendLayout()
        CType(Me.pbEpisodeFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpFrameExtraction.SuspendLayout()
        Me.tpEpisodeMetaData.SuspendLayout()
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
        Me.lblTopDetails.Size = New System.Drawing.Size(214, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Edit the details for the selected episode."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(155, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Edit Episode"
        '
        'pbTopLogo
        '
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.ErrorImage = Nothing
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.InitialImage = Nothing
        Me.pbTopLogo.Location = New System.Drawing.Point(7, 8)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.pbTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'tcEditEpisode
        '
        Me.tcEditEpisode.Controls.Add(Me.tpEpsiodeDetails)
        Me.tcEditEpisode.Controls.Add(Me.tpEpisodePoster)
        Me.tcEditEpisode.Controls.Add(Me.tpEpisodeFanart)
        Me.tcEditEpisode.Controls.Add(Me.tpFrameExtraction)
        Me.tcEditEpisode.Controls.Add(Me.tpEpisodeMetaData)
        Me.tcEditEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tcEditEpisode.Location = New System.Drawing.Point(4, 70)
        Me.tcEditEpisode.Name = "tcEditEpisode"
        Me.tcEditEpisode.SelectedIndex = 0
        Me.tcEditEpisode.Size = New System.Drawing.Size(844, 478)
        Me.tcEditEpisode.TabIndex = 3
        '
        'tpEpsiodeDetails
        '
        Me.tpEpsiodeDetails.Controls.Add(Me.btnActorDown)
        Me.tpEpsiodeDetails.Controls.Add(Me.btnActorUp)
        Me.tpEpsiodeDetails.Controls.Add(Me.txtAired)
        Me.tpEpsiodeDetails.Controls.Add(Me.txtEpisode)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblEpisode)
        Me.tpEpsiodeDetails.Controls.Add(Me.txtSeason)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblSeason)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblCredits)
        Me.tpEpsiodeDetails.Controls.Add(Me.txtCredits)
        Me.tpEpsiodeDetails.Controls.Add(Me.btnEditActor)
        Me.tpEpsiodeDetails.Controls.Add(Me.btnAddActor)
        Me.tpEpsiodeDetails.Controls.Add(Me.btnManual)
        Me.tpEpsiodeDetails.Controls.Add(Me.btnRemove)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblActors)
        Me.tpEpsiodeDetails.Controls.Add(Me.lvActors)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblDirector)
        Me.tpEpsiodeDetails.Controls.Add(Me.txtDirector)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblPlot)
        Me.tpEpsiodeDetails.Controls.Add(Me.txtPlot)
        Me.tpEpsiodeDetails.Controls.Add(Me.pbStar5)
        Me.tpEpsiodeDetails.Controls.Add(Me.pbStar4)
        Me.tpEpsiodeDetails.Controls.Add(Me.pbStar3)
        Me.tpEpsiodeDetails.Controls.Add(Me.pbStar2)
        Me.tpEpsiodeDetails.Controls.Add(Me.pbStar1)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblRating)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblAired)
        Me.tpEpsiodeDetails.Controls.Add(Me.lblTitle)
        Me.tpEpsiodeDetails.Controls.Add(Me.txtTitle)
        Me.tpEpsiodeDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpEpsiodeDetails.Name = "tpEpsiodeDetails"
        Me.tpEpsiodeDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEpsiodeDetails.Size = New System.Drawing.Size(836, 452)
        Me.tpEpsiodeDetails.TabIndex = 0
        Me.tpEpsiodeDetails.Text = "Details"
        Me.tpEpsiodeDetails.UseVisualStyleBackColor = True
        '
        'btnActorDown
        '
        Me.btnActorDown.Image = CType(resources.GetObject("btnActorDown.Image"), System.Drawing.Image)
        Me.btnActorDown.Location = New System.Drawing.Point(328, 423)
        Me.btnActorDown.Name = "btnActorDown"
        Me.btnActorDown.Size = New System.Drawing.Size(23, 23)
        Me.btnActorDown.TabIndex = 20
        Me.btnActorDown.UseVisualStyleBackColor = True
        '
        'btnActorUp
        '
        Me.btnActorUp.Image = CType(resources.GetObject("btnActorUp.Image"), System.Drawing.Image)
        Me.btnActorUp.Location = New System.Drawing.Point(304, 423)
        Me.btnActorUp.Name = "btnActorUp"
        Me.btnActorUp.Size = New System.Drawing.Size(23, 23)
        Me.btnActorUp.TabIndex = 19
        Me.btnActorUp.UseVisualStyleBackColor = True
        '
        'txtAired
        '
        Me.txtAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAired.Location = New System.Drawing.Point(111, 67)
        Me.txtAired.Name = "txtAired"
        Me.txtAired.Size = New System.Drawing.Size(88, 22)
        Me.txtAired.TabIndex = 9
        '
        'txtEpisode
        '
        Me.txtEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpisode.Location = New System.Drawing.Point(59, 67)
        Me.txtEpisode.Name = "txtEpisode"
        Me.txtEpisode.Size = New System.Drawing.Size(46, 22)
        Me.txtEpisode.TabIndex = 7
        '
        'lblEpisode
        '
        Me.lblEpisode.AutoSize = True
        Me.lblEpisode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblEpisode.Location = New System.Drawing.Point(59, 51)
        Me.lblEpisode.Name = "lblEpisode"
        Me.lblEpisode.Size = New System.Drawing.Size(51, 13)
        Me.lblEpisode.TabIndex = 6
        Me.lblEpisode.Text = "Episode:"
        '
        'txtSeason
        '
        Me.txtSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeason.Location = New System.Drawing.Point(7, 67)
        Me.txtSeason.Name = "txtSeason"
        Me.txtSeason.Size = New System.Drawing.Size(46, 22)
        Me.txtSeason.TabIndex = 5
        '
        'lblSeason
        '
        Me.lblSeason.AutoSize = True
        Me.lblSeason.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSeason.Location = New System.Drawing.Point(7, 51)
        Me.lblSeason.Name = "lblSeason"
        Me.lblSeason.Size = New System.Drawing.Size(47, 13)
        Me.lblSeason.TabIndex = 4
        Me.lblSeason.Text = "Season:"
        '
        'lblCredits
        '
        Me.lblCredits.AutoSize = True
        Me.lblCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCredits.Location = New System.Drawing.Point(217, 139)
        Me.lblCredits.Name = "lblCredits"
        Me.lblCredits.Size = New System.Drawing.Size(46, 13)
        Me.lblCredits.TabIndex = 13
        Me.lblCredits.Text = "Credits:"
        '
        'txtCredits
        '
        Me.txtCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCredits.Location = New System.Drawing.Point(217, 155)
        Me.txtCredits.Name = "txtCredits"
        Me.txtCredits.Size = New System.Drawing.Size(408, 22)
        Me.txtCredits.TabIndex = 14
        '
        'btnEditActor
        '
        Me.btnEditActor.Image = CType(resources.GetObject("btnEditActor.Image"), System.Drawing.Image)
        Me.btnEditActor.Location = New System.Drawing.Point(35, 423)
        Me.btnEditActor.Name = "btnEditActor"
        Me.btnEditActor.Size = New System.Drawing.Size(23, 23)
        Me.btnEditActor.TabIndex = 18
        Me.btnEditActor.UseVisualStyleBackColor = True
        '
        'btnAddActor
        '
        Me.btnAddActor.Image = CType(resources.GetObject("btnAddActor.Image"), System.Drawing.Image)
        Me.btnAddActor.Location = New System.Drawing.Point(6, 423)
        Me.btnAddActor.Name = "btnAddActor"
        Me.btnAddActor.Size = New System.Drawing.Size(23, 23)
        Me.btnAddActor.TabIndex = 17
        Me.btnAddActor.UseVisualStyleBackColor = True
        '
        'btnManual
        '
        Me.btnManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManual.Location = New System.Drawing.Point(738, 423)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(92, 23)
        Me.btnManual.TabIndex = 22
        Me.btnManual.Text = "Manual Edit"
        Me.btnManual.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), System.Drawing.Image)
        Me.btnRemove.Location = New System.Drawing.Point(602, 423)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnRemove.TabIndex = 21
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'lblActors
        '
        Me.lblActors.AutoSize = True
        Me.lblActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblActors.Location = New System.Drawing.Point(7, 188)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(43, 13)
        Me.lblActors.TabIndex = 15
        Me.lblActors.Text = "Actors:"
        '
        'lvActors
        '
        Me.lvActors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colRole, Me.colThumb})
        Me.lvActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvActors.FullRowSelect = True
        Me.lvActors.Location = New System.Drawing.Point(7, 204)
        Me.lvActors.Name = "lvActors"
        Me.lvActors.Size = New System.Drawing.Size(618, 213)
        Me.lvActors.TabIndex = 16
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
        Me.colThumb.Width = 387
        '
        'lblDirector
        '
        Me.lblDirector.AutoSize = True
        Me.lblDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblDirector.Location = New System.Drawing.Point(7, 139)
        Me.lblDirector.Name = "lblDirector"
        Me.lblDirector.Size = New System.Drawing.Size(51, 13)
        Me.lblDirector.TabIndex = 11
        Me.lblDirector.Text = "Director:"
        '
        'txtDirector
        '
        Me.txtDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDirector.Location = New System.Drawing.Point(7, 155)
        Me.txtDirector.Name = "txtDirector"
        Me.txtDirector.Size = New System.Drawing.Size(192, 22)
        Me.txtDirector.TabIndex = 12
        '
        'lblPlot
        '
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlot.Location = New System.Drawing.Point(217, 7)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(31, 13)
        Me.lblPlot.TabIndex = 2
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
        Me.txtPlot.TabIndex = 3
        '
        'pbStar5
        '
        Me.pbStar5.Location = New System.Drawing.Point(104, 110)
        Me.pbStar5.Name = "pbStar5"
        Me.pbStar5.Size = New System.Drawing.Size(24, 24)
        Me.pbStar5.TabIndex = 67
        Me.pbStar5.TabStop = False
        '
        'pbStar4
        '
        Me.pbStar4.Location = New System.Drawing.Point(80, 110)
        Me.pbStar4.Name = "pbStar4"
        Me.pbStar4.Size = New System.Drawing.Size(24, 24)
        Me.pbStar4.TabIndex = 66
        Me.pbStar4.TabStop = False
        '
        'pbStar3
        '
        Me.pbStar3.Location = New System.Drawing.Point(56, 110)
        Me.pbStar3.Name = "pbStar3"
        Me.pbStar3.Size = New System.Drawing.Size(24, 24)
        Me.pbStar3.TabIndex = 65
        Me.pbStar3.TabStop = False
        '
        'pbStar2
        '
        Me.pbStar2.Location = New System.Drawing.Point(32, 110)
        Me.pbStar2.Name = "pbStar2"
        Me.pbStar2.Size = New System.Drawing.Size(24, 24)
        Me.pbStar2.TabIndex = 64
        Me.pbStar2.TabStop = False
        '
        'pbStar1
        '
        Me.pbStar1.Location = New System.Drawing.Point(8, 110)
        Me.pbStar1.Name = "pbStar1"
        Me.pbStar1.Size = New System.Drawing.Size(24, 24)
        Me.pbStar1.TabIndex = 63
        Me.pbStar1.TabStop = False
        '
        'lblRating
        '
        Me.lblRating.AutoSize = True
        Me.lblRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblRating.Location = New System.Drawing.Point(7, 94)
        Me.lblRating.Name = "lblRating"
        Me.lblRating.Size = New System.Drawing.Size(44, 13)
        Me.lblRating.TabIndex = 10
        Me.lblRating.Text = "Rating:"
        '
        'lblAired
        '
        Me.lblAired.AutoSize = True
        Me.lblAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAired.Location = New System.Drawing.Point(111, 51)
        Me.lblAired.Name = "lblAired"
        Me.lblAired.Size = New System.Drawing.Size(38, 13)
        Me.lblAired.TabIndex = 8
        Me.lblAired.Text = "Aired:"
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
        'tpEpisodePoster
        '
        Me.tpEpisodePoster.Controls.Add(Me.btnSetEpisodePosterDL)
        Me.tpEpisodePoster.Controls.Add(Me.btnRemoveEpisodePoster)
        Me.tpEpisodePoster.Controls.Add(Me.lblEpisodePosterSize)
        Me.tpEpisodePoster.Controls.Add(Me.btnSetEpisodePosterScrape)
        Me.tpEpisodePoster.Controls.Add(Me.btnSetEpisodePoster)
        Me.tpEpisodePoster.Controls.Add(Me.pbEpisodePoster)
        Me.tpEpisodePoster.Location = New System.Drawing.Point(4, 22)
        Me.tpEpisodePoster.Name = "tpEpisodePoster"
        Me.tpEpisodePoster.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEpisodePoster.Size = New System.Drawing.Size(836, 452)
        Me.tpEpisodePoster.TabIndex = 1
        Me.tpEpisodePoster.Text = "Poster"
        Me.tpEpisodePoster.UseVisualStyleBackColor = True
        '
        'btnSetEpisodePosterDL
        '
        Me.btnSetEpisodePosterDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetEpisodePosterDL.Image = CType(resources.GetObject("btnSetEpisodePosterDL.Image"), System.Drawing.Image)
        Me.btnSetEpisodePosterDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetEpisodePosterDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetEpisodePosterDL.Name = "btnSetEpisodePosterDL"
        Me.btnSetEpisodePosterDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetEpisodePosterDL.TabIndex = 3
        Me.btnSetEpisodePosterDL.Text = "Change Poster (Download)"
        Me.btnSetEpisodePosterDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetEpisodePosterDL.UseVisualStyleBackColor = True
        '
        'btnRemoveEpisodePoster
        '
        Me.btnRemoveEpisodePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveEpisodePoster.Image = CType(resources.GetObject("btnRemoveEpisodePoster.Image"), System.Drawing.Image)
        Me.btnRemoveEpisodePoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveEpisodePoster.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveEpisodePoster.Name = "btnRemoveEpisodePoster"
        Me.btnRemoveEpisodePoster.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveEpisodePoster.TabIndex = 4
        Me.btnRemoveEpisodePoster.Text = "Remove Poster"
        Me.btnRemoveEpisodePoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveEpisodePoster.UseVisualStyleBackColor = True
        '
        'lblEpisodePosterSize
        '
        Me.lblEpisodePosterSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEpisodePosterSize.Location = New System.Drawing.Point(8, 8)
        Me.lblEpisodePosterSize.Name = "lblEpisodePosterSize"
        Me.lblEpisodePosterSize.Size = New System.Drawing.Size(104, 23)
        Me.lblEpisodePosterSize.TabIndex = 0
        Me.lblEpisodePosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblEpisodePosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblEpisodePosterSize.Visible = False
        '
        'btnSetEpisodePosterScrape
        '
        Me.btnSetEpisodePosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetEpisodePosterScrape.Image = CType(resources.GetObject("btnSetEpisodePosterScrape.Image"), System.Drawing.Image)
        Me.btnSetEpisodePosterScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetEpisodePosterScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetEpisodePosterScrape.Name = "btnSetEpisodePosterScrape"
        Me.btnSetEpisodePosterScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetEpisodePosterScrape.TabIndex = 2
        Me.btnSetEpisodePosterScrape.Text = "Change Poster (Scrape)"
        Me.btnSetEpisodePosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetEpisodePosterScrape.UseVisualStyleBackColor = True
        '
        'btnSetEpisodePoster
        '
        Me.btnSetEpisodePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetEpisodePoster.Image = CType(resources.GetObject("btnSetEpisodePoster.Image"), System.Drawing.Image)
        Me.btnSetEpisodePoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetEpisodePoster.Location = New System.Drawing.Point(735, 6)
        Me.btnSetEpisodePoster.Name = "btnSetEpisodePoster"
        Me.btnSetEpisodePoster.Size = New System.Drawing.Size(96, 83)
        Me.btnSetEpisodePoster.TabIndex = 1
        Me.btnSetEpisodePoster.Text = "Change Poster (Local)"
        Me.btnSetEpisodePoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetEpisodePoster.UseVisualStyleBackColor = True
        '
        'pbEpisodePoster
        '
        Me.pbEpisodePoster.BackColor = System.Drawing.Color.DimGray
        Me.pbEpisodePoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbEpisodePoster.Location = New System.Drawing.Point(6, 6)
        Me.pbEpisodePoster.Name = "pbEpisodePoster"
        Me.pbEpisodePoster.Size = New System.Drawing.Size(724, 440)
        Me.pbEpisodePoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbEpisodePoster.TabIndex = 0
        Me.pbEpisodePoster.TabStop = False
        '
        'tpEpisodeFanart
        '
        Me.tpEpisodeFanart.Controls.Add(Me.lblEpisodeFanartSize)
        Me.tpEpisodeFanart.Controls.Add(Me.btnSetEpisodeFanartDL)
        Me.tpEpisodeFanart.Controls.Add(Me.btnRemoveEpisodeFanart)
        Me.tpEpisodeFanart.Controls.Add(Me.btnSetEpisodeFanartScrape)
        Me.tpEpisodeFanart.Controls.Add(Me.btnSetEpisodeFanart)
        Me.tpEpisodeFanart.Controls.Add(Me.pbEpisodeFanart)
        Me.tpEpisodeFanart.Location = New System.Drawing.Point(4, 22)
        Me.tpEpisodeFanart.Name = "tpEpisodeFanart"
        Me.tpEpisodeFanart.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEpisodeFanart.Size = New System.Drawing.Size(836, 452)
        Me.tpEpisodeFanart.TabIndex = 6
        Me.tpEpisodeFanart.Text = "Fanart"
        Me.tpEpisodeFanart.UseVisualStyleBackColor = True
        '
        'lblEpisodeFanartSize
        '
        Me.lblEpisodeFanartSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEpisodeFanartSize.Location = New System.Drawing.Point(8, 8)
        Me.lblEpisodeFanartSize.Name = "lblEpisodeFanartSize"
        Me.lblEpisodeFanartSize.Size = New System.Drawing.Size(104, 23)
        Me.lblEpisodeFanartSize.TabIndex = 0
        Me.lblEpisodeFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblEpisodeFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblEpisodeFanartSize.Visible = False
        '
        'btnSetEpisodeFanartDL
        '
        Me.btnSetEpisodeFanartDL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetEpisodeFanartDL.Image = CType(resources.GetObject("btnSetEpisodeFanartDL.Image"), System.Drawing.Image)
        Me.btnSetEpisodeFanartDL.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetEpisodeFanartDL.Location = New System.Drawing.Point(735, 180)
        Me.btnSetEpisodeFanartDL.Name = "btnSetEpisodeFanartDL"
        Me.btnSetEpisodeFanartDL.Size = New System.Drawing.Size(96, 83)
        Me.btnSetEpisodeFanartDL.TabIndex = 3
        Me.btnSetEpisodeFanartDL.Text = "Change Fanart (Download)"
        Me.btnSetEpisodeFanartDL.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetEpisodeFanartDL.UseVisualStyleBackColor = True
        '
        'btnRemoveEpisodeFanart
        '
        Me.btnRemoveEpisodeFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemoveEpisodeFanart.Image = CType(resources.GetObject("btnRemoveEpisodeFanart.Image"), System.Drawing.Image)
        Me.btnRemoveEpisodeFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveEpisodeFanart.Location = New System.Drawing.Point(735, 363)
        Me.btnRemoveEpisodeFanart.Name = "btnRemoveEpisodeFanart"
        Me.btnRemoveEpisodeFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnRemoveEpisodeFanart.TabIndex = 4
        Me.btnRemoveEpisodeFanart.Text = "Remove Fanart"
        Me.btnRemoveEpisodeFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveEpisodeFanart.UseVisualStyleBackColor = True
        '
        'btnSetEpisodeFanartScrape
        '
        Me.btnSetEpisodeFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetEpisodeFanartScrape.Image = CType(resources.GetObject("btnSetEpisodeFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetEpisodeFanartScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetEpisodeFanartScrape.Location = New System.Drawing.Point(735, 93)
        Me.btnSetEpisodeFanartScrape.Name = "btnSetEpisodeFanartScrape"
        Me.btnSetEpisodeFanartScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetEpisodeFanartScrape.TabIndex = 2
        Me.btnSetEpisodeFanartScrape.Text = "Change Fanart (Scrape)"
        Me.btnSetEpisodeFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetEpisodeFanartScrape.UseVisualStyleBackColor = True
        '
        'btnSetEpisodeFanart
        '
        Me.btnSetEpisodeFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetEpisodeFanart.Image = CType(resources.GetObject("btnSetEpisodeFanart.Image"), System.Drawing.Image)
        Me.btnSetEpisodeFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetEpisodeFanart.Location = New System.Drawing.Point(735, 6)
        Me.btnSetEpisodeFanart.Name = "btnSetEpisodeFanart"
        Me.btnSetEpisodeFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnSetEpisodeFanart.TabIndex = 1
        Me.btnSetEpisodeFanart.Text = "Change Fanart (Local)"
        Me.btnSetEpisodeFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetEpisodeFanart.UseVisualStyleBackColor = True
        '
        'pbEpisodeFanart
        '
        Me.pbEpisodeFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbEpisodeFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbEpisodeFanart.Location = New System.Drawing.Point(6, 6)
        Me.pbEpisodeFanart.Name = "pbEpisodeFanart"
        Me.pbEpisodeFanart.Size = New System.Drawing.Size(724, 440)
        Me.pbEpisodeFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbEpisodeFanart.TabIndex = 30
        Me.pbEpisodeFanart.TabStop = False
        '
        'tpFrameExtraction
        '
        Me.tpFrameExtraction.Controls.Add(Me.pnlFrameExtrator)
        Me.tpFrameExtraction.Location = New System.Drawing.Point(4, 22)
        Me.tpFrameExtraction.Name = "tpFrameExtraction"
        Me.tpFrameExtraction.Padding = New System.Windows.Forms.Padding(3)
        Me.tpFrameExtraction.Size = New System.Drawing.Size(836, 452)
        Me.tpFrameExtraction.TabIndex = 3
        Me.tpFrameExtraction.Text = "Frame Extraction"
        Me.tpFrameExtraction.UseVisualStyleBackColor = True
        '
        'pnlFrameExtrator
        '
        Me.pnlFrameExtrator.Location = New System.Drawing.Point(1, 0)
        Me.pnlFrameExtrator.Name = "pnlFrameExtrator"
        Me.pnlFrameExtrator.Size = New System.Drawing.Size(834, 452)
        Me.pnlFrameExtrator.TabIndex = 0
        '
        'tpEpisodeMetaData
        '
        Me.tpEpisodeMetaData.Controls.Add(Me.pnlFileInfo)
        Me.tpEpisodeMetaData.Location = New System.Drawing.Point(4, 22)
        Me.tpEpisodeMetaData.Name = "tpEpisodeMetaData"
        Me.tpEpisodeMetaData.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEpisodeMetaData.Size = New System.Drawing.Size(836, 452)
        Me.tpEpisodeMetaData.TabIndex = 5
        Me.tpEpisodeMetaData.Text = "Meta Data"
        Me.tpEpisodeMetaData.UseVisualStyleBackColor = True
        '
        'pnlFileInfo
        '
        Me.pnlFileInfo.Location = New System.Drawing.Point(-4, 0)
        Me.pnlFileInfo.Name = "pnlFileInfo"
        Me.pnlFileInfo.Size = New System.Drawing.Size(844, 452)
        Me.pnlFileInfo.TabIndex = 0
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
        'chkWatched
        '
        Me.chkWatched.AutoSize = True
        Me.chkWatched.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWatched.Location = New System.Drawing.Point(12, 557)
        Me.chkWatched.Name = "chkWatched"
        Me.chkWatched.Size = New System.Drawing.Size(72, 17)
        Me.chkWatched.TabIndex = 7
        Me.chkWatched.Text = "Watched"
        Me.chkWatched.UseVisualStyleBackColor = True
        '
        'dlgEditEpisode
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(854, 582)
        Me.Controls.Add(Me.chkWatched)
        Me.Controls.Add(Me.tcEditEpisode)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditEpisode"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Episode"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcEditEpisode.ResumeLayout(False)
        Me.tpEpsiodeDetails.ResumeLayout(False)
        Me.tpEpsiodeDetails.PerformLayout()
        CType(Me.pbStar5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbStar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpEpisodePoster.ResumeLayout(False)
        CType(Me.pbEpisodePoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpEpisodeFanart.ResumeLayout(False)
        CType(Me.pbEpisodeFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpFrameExtraction.ResumeLayout(False)
        Me.tpEpisodeMetaData.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents tcEditEpisode As System.Windows.Forms.TabControl
    Friend WithEvents tpEpsiodeDetails As System.Windows.Forms.TabPage
    Friend WithEvents lblCredits As System.Windows.Forms.Label
    Friend WithEvents txtCredits As System.Windows.Forms.TextBox
    Friend WithEvents btnEditActor As System.Windows.Forms.Button
    Friend WithEvents btnAddActor As System.Windows.Forms.Button
    Friend WithEvents btnManual As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents lblActors As System.Windows.Forms.Label
    Friend WithEvents lvActors As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRole As System.Windows.Forms.ColumnHeader
    Friend WithEvents colThumb As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblDirector As System.Windows.Forms.Label
    Friend WithEvents txtDirector As System.Windows.Forms.TextBox
    Friend WithEvents lblPlot As System.Windows.Forms.Label
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents pbStar5 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar4 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar3 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar2 As System.Windows.Forms.PictureBox
    Friend WithEvents pbStar1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblRating As System.Windows.Forms.Label
    Friend WithEvents lblAired As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents tpEpisodePoster As System.Windows.Forms.TabPage
    Friend WithEvents btnSetEpisodePosterDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveEpisodePoster As System.Windows.Forms.Button
    Friend WithEvents lblEpisodePosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetEpisodePosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetEpisodePoster As System.Windows.Forms.Button
    Friend WithEvents pbEpisodePoster As System.Windows.Forms.PictureBox
    Friend WithEvents tpFrameExtraction As System.Windows.Forms.TabPage
    Friend WithEvents tpEpisodeMetaData As System.Windows.Forms.TabPage
    Friend WithEvents pnlFileInfo As System.Windows.Forms.Panel
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents txtEpisode As System.Windows.Forms.TextBox
    Friend WithEvents lblEpisode As System.Windows.Forms.Label
    Friend WithEvents txtSeason As System.Windows.Forms.TextBox
    Friend WithEvents lblSeason As System.Windows.Forms.Label
    Friend WithEvents tpEpisodeFanart As System.Windows.Forms.TabPage
    Friend WithEvents btnSetEpisodeFanartDL As System.Windows.Forms.Button
    Friend WithEvents btnRemoveEpisodeFanart As System.Windows.Forms.Button
    Friend WithEvents btnSetEpisodeFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetEpisodeFanart As System.Windows.Forms.Button
    Friend WithEvents pbEpisodeFanart As System.Windows.Forms.PictureBox
    Friend WithEvents lblEpisodeFanartSize As System.Windows.Forms.Label
    Friend WithEvents txtAired As System.Windows.Forms.TextBox
    Friend WithEvents btnActorDown As System.Windows.Forms.Button
    Friend WithEvents btnActorUp As System.Windows.Forms.Button
    Friend WithEvents pnlFrameExtrator As System.Windows.Forms.Panel
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkWatched As System.Windows.Forms.CheckBox

End Class
