<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgClearOrReplace
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgClearOrReplace))
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.chkActors = New System.Windows.Forms.CheckBox()
        Me.chkCertifications = New System.Windows.Forms.CheckBox()
        Me.chkCountries = New System.Windows.Forms.CheckBox()
        Me.chkDirectors = New System.Windows.Forms.CheckBox()
        Me.chkGenres = New System.Windows.Forms.CheckBox()
        Me.chkMPAA = New System.Windows.Forms.CheckBox()
        Me.chkOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.chkOutline = New System.Windows.Forms.CheckBox()
        Me.chkRating = New System.Windows.Forms.CheckBox()
        Me.chkRuntime = New System.Windows.Forms.CheckBox()
        Me.chkStudios = New System.Windows.Forms.CheckBox()
        Me.chkTagline = New System.Windows.Forms.CheckBox()
        Me.chkTags = New System.Windows.Forms.CheckBox()
        Me.chkTop250 = New System.Windows.Forms.CheckBox()
        Me.chkTrailer = New System.Windows.Forms.CheckBox()
        Me.chkWriters = New System.Windows.Forms.CheckBox()
        Me.chkVideoSource = New System.Windows.Forms.CheckBox()
        Me.chkUserRating = New System.Windows.Forms.CheckBox()
        Me.txtVideoSource = New System.Windows.Forms.TextBox()
        Me.txtTagline = New System.Windows.Forms.TextBox()
        Me.txtMPAA = New System.Windows.Forms.TextBox()
        Me.chkAired = New System.Windows.Forms.CheckBox()
        Me.chkGuestStars = New System.Windows.Forms.CheckBox()
        Me.chkCreators = New System.Windows.Forms.CheckBox()
        Me.chkPremiered = New System.Windows.Forms.CheckBox()
        Me.txtPremiered = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtUserRating = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtAired = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.chkTitle = New System.Windows.Forms.CheckBox()
        Me.txtCertifications = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtCountries = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtCreators = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtDirectors = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtGenres = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtStudios = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtTags = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.txtWriters = New EmberAPI.AdvancedControls.TextBox_with_Watermark()
        Me.chkPlot = New System.Windows.Forms.CheckBox()
        Me.lblActors = New System.Windows.Forms.Label()
        Me.lblGuestStars = New System.Windows.Forms.Label()
        Me.lblOriginalTitle = New System.Windows.Forms.Label()
        Me.lblPlot = New System.Windows.Forms.Label()
        Me.lblOutline = New System.Windows.Forms.Label()
        Me.lblRating = New System.Windows.Forms.Label()
        Me.lblRuntime = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblTop250 = New System.Windows.Forms.Label()
        Me.lblTrailer = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkEdition = New System.Windows.Forms.CheckBox()
        Me.txtEdition = New System.Windows.Forms.TextBox()
        Me.tblMain.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblMain
        '
        Me.tblMain.AutoSize = True
        Me.tblMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMain.ColumnCount = 2
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.chkActors, 0, 0)
        Me.tblMain.Controls.Add(Me.chkCertifications, 0, 2)
        Me.tblMain.Controls.Add(Me.chkCountries, 0, 3)
        Me.tblMain.Controls.Add(Me.chkDirectors, 0, 5)
        Me.tblMain.Controls.Add(Me.chkGenres, 0, 7)
        Me.tblMain.Controls.Add(Me.chkMPAA, 0, 9)
        Me.tblMain.Controls.Add(Me.chkOriginalTitle, 0, 10)
        Me.tblMain.Controls.Add(Me.chkOutline, 0, 12)
        Me.tblMain.Controls.Add(Me.chkRating, 0, 14)
        Me.tblMain.Controls.Add(Me.chkRuntime, 0, 15)
        Me.tblMain.Controls.Add(Me.chkStudios, 0, 17)
        Me.tblMain.Controls.Add(Me.chkTagline, 0, 18)
        Me.tblMain.Controls.Add(Me.chkTags, 0, 19)
        Me.tblMain.Controls.Add(Me.chkTop250, 0, 21)
        Me.tblMain.Controls.Add(Me.chkTrailer, 0, 22)
        Me.tblMain.Controls.Add(Me.chkWriters, 0, 25)
        Me.tblMain.Controls.Add(Me.chkVideoSource, 0, 24)
        Me.tblMain.Controls.Add(Me.chkUserRating, 0, 23)
        Me.tblMain.Controls.Add(Me.txtVideoSource, 1, 24)
        Me.tblMain.Controls.Add(Me.txtTagline, 1, 18)
        Me.tblMain.Controls.Add(Me.txtMPAA, 1, 9)
        Me.tblMain.Controls.Add(Me.chkAired, 0, 1)
        Me.tblMain.Controls.Add(Me.chkGuestStars, 0, 8)
        Me.tblMain.Controls.Add(Me.chkCreators, 0, 4)
        Me.tblMain.Controls.Add(Me.chkPremiered, 0, 13)
        Me.tblMain.Controls.Add(Me.txtPremiered, 1, 13)
        Me.tblMain.Controls.Add(Me.txtUserRating, 1, 23)
        Me.tblMain.Controls.Add(Me.txtAired, 1, 1)
        Me.tblMain.Controls.Add(Me.chkStatus, 0, 16)
        Me.tblMain.Controls.Add(Me.txtStatus, 1, 16)
        Me.tblMain.Controls.Add(Me.chkTitle, 0, 20)
        Me.tblMain.Controls.Add(Me.txtCertifications, 1, 2)
        Me.tblMain.Controls.Add(Me.txtCountries, 1, 3)
        Me.tblMain.Controls.Add(Me.txtCreators, 1, 4)
        Me.tblMain.Controls.Add(Me.txtDirectors, 1, 5)
        Me.tblMain.Controls.Add(Me.txtGenres, 1, 7)
        Me.tblMain.Controls.Add(Me.txtStudios, 1, 17)
        Me.tblMain.Controls.Add(Me.txtTags, 1, 19)
        Me.tblMain.Controls.Add(Me.txtWriters, 1, 25)
        Me.tblMain.Controls.Add(Me.chkPlot, 0, 11)
        Me.tblMain.Controls.Add(Me.lblActors, 1, 0)
        Me.tblMain.Controls.Add(Me.lblGuestStars, 1, 8)
        Me.tblMain.Controls.Add(Me.lblOriginalTitle, 1, 10)
        Me.tblMain.Controls.Add(Me.lblPlot, 1, 11)
        Me.tblMain.Controls.Add(Me.lblOutline, 1, 12)
        Me.tblMain.Controls.Add(Me.lblRating, 1, 14)
        Me.tblMain.Controls.Add(Me.lblRuntime, 1, 15)
        Me.tblMain.Controls.Add(Me.lblTitle, 1, 20)
        Me.tblMain.Controls.Add(Me.lblTop250, 1, 21)
        Me.tblMain.Controls.Add(Me.lblTrailer, 1, 22)
        Me.tblMain.Controls.Add(Me.chkEdition, 0, 6)
        Me.tblMain.Controls.Add(Me.txtEdition, 1, 6)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 27
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.Size = New System.Drawing.Size(334, 723)
        Me.tblMain.TabIndex = 2
        '
        'chkActors
        '
        Me.chkActors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkActors.AutoSize = True
        Me.chkActors.Location = New System.Drawing.Point(3, 3)
        Me.chkActors.Name = "chkActors"
        Me.chkActors.Size = New System.Drawing.Size(56, 17)
        Me.chkActors.TabIndex = 0
        Me.chkActors.Text = "Actors"
        Me.chkActors.UseVisualStyleBackColor = True
        '
        'chkCertifications
        '
        Me.chkCertifications.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertifications.AutoSize = True
        Me.chkCertifications.Location = New System.Drawing.Point(3, 53)
        Me.chkCertifications.Name = "chkCertifications"
        Me.chkCertifications.Size = New System.Drawing.Size(86, 17)
        Me.chkCertifications.TabIndex = 3
        Me.chkCertifications.Text = "Certifications"
        Me.chkCertifications.UseVisualStyleBackColor = True
        '
        'chkCountries
        '
        Me.chkCountries.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCountries.AutoSize = True
        Me.chkCountries.Location = New System.Drawing.Point(3, 79)
        Me.chkCountries.Name = "chkCountries"
        Me.chkCountries.Size = New System.Drawing.Size(70, 17)
        Me.chkCountries.TabIndex = 5
        Me.chkCountries.Text = "Countries"
        Me.chkCountries.UseVisualStyleBackColor = True
        '
        'chkDirectors
        '
        Me.chkDirectors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDirectors.AutoSize = True
        Me.chkDirectors.Location = New System.Drawing.Point(3, 131)
        Me.chkDirectors.Name = "chkDirectors"
        Me.chkDirectors.Size = New System.Drawing.Size(68, 17)
        Me.chkDirectors.TabIndex = 9
        Me.chkDirectors.Text = "Directors"
        Me.chkDirectors.UseVisualStyleBackColor = True
        '
        'chkGenres
        '
        Me.chkGenres.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGenres.AutoSize = True
        Me.chkGenres.Location = New System.Drawing.Point(3, 183)
        Me.chkGenres.Name = "chkGenres"
        Me.chkGenres.Size = New System.Drawing.Size(60, 17)
        Me.chkGenres.TabIndex = 11
        Me.chkGenres.Text = "Genres"
        Me.chkGenres.UseVisualStyleBackColor = True
        '
        'chkMPAA
        '
        Me.chkMPAA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMPAA.AutoSize = True
        Me.chkMPAA.Location = New System.Drawing.Point(3, 232)
        Me.chkMPAA.Name = "chkMPAA"
        Me.chkMPAA.Size = New System.Drawing.Size(56, 17)
        Me.chkMPAA.TabIndex = 14
        Me.chkMPAA.Text = "MPAA"
        Me.chkMPAA.UseVisualStyleBackColor = True
        '
        'chkOriginalTitle
        '
        Me.chkOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOriginalTitle.AutoSize = True
        Me.chkOriginalTitle.Location = New System.Drawing.Point(3, 257)
        Me.chkOriginalTitle.Name = "chkOriginalTitle"
        Me.chkOriginalTitle.Size = New System.Drawing.Size(84, 17)
        Me.chkOriginalTitle.TabIndex = 16
        Me.chkOriginalTitle.Text = "Original Title"
        Me.chkOriginalTitle.UseVisualStyleBackColor = True
        '
        'chkOutline
        '
        Me.chkOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOutline.AutoSize = True
        Me.chkOutline.Location = New System.Drawing.Point(3, 303)
        Me.chkOutline.Name = "chkOutline"
        Me.chkOutline.Size = New System.Drawing.Size(80, 17)
        Me.chkOutline.TabIndex = 18
        Me.chkOutline.Text = "Plot Outline"
        Me.chkOutline.UseVisualStyleBackColor = True
        '
        'chkRating
        '
        Me.chkRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRating.AutoSize = True
        Me.chkRating.Location = New System.Drawing.Point(3, 352)
        Me.chkRating.Name = "chkRating"
        Me.chkRating.Size = New System.Drawing.Size(95, 17)
        Me.chkRating.TabIndex = 21
        Me.chkRating.Text = "Rating / Votes"
        Me.chkRating.UseVisualStyleBackColor = True
        '
        'chkRuntime
        '
        Me.chkRuntime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRuntime.AutoSize = True
        Me.chkRuntime.Location = New System.Drawing.Point(3, 375)
        Me.chkRuntime.Name = "chkRuntime"
        Me.chkRuntime.Size = New System.Drawing.Size(65, 17)
        Me.chkRuntime.TabIndex = 24
        Me.chkRuntime.Text = "Runtime"
        Me.chkRuntime.UseVisualStyleBackColor = True
        '
        'chkStudios
        '
        Me.chkStudios.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkStudios.AutoSize = True
        Me.chkStudios.Location = New System.Drawing.Point(3, 425)
        Me.chkStudios.Name = "chkStudios"
        Me.chkStudios.Size = New System.Drawing.Size(61, 17)
        Me.chkStudios.TabIndex = 27
        Me.chkStudios.Text = "Studios"
        Me.chkStudios.UseVisualStyleBackColor = True
        '
        'chkTagline
        '
        Me.chkTagline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTagline.AutoSize = True
        Me.chkTagline.Location = New System.Drawing.Point(3, 451)
        Me.chkTagline.Name = "chkTagline"
        Me.chkTagline.Size = New System.Drawing.Size(61, 17)
        Me.chkTagline.TabIndex = 29
        Me.chkTagline.Text = "Tagline"
        Me.chkTagline.UseVisualStyleBackColor = True
        '
        'chkTags
        '
        Me.chkTags.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTags.AutoSize = True
        Me.chkTags.Location = New System.Drawing.Point(3, 477)
        Me.chkTags.Name = "chkTags"
        Me.chkTags.Size = New System.Drawing.Size(50, 17)
        Me.chkTags.TabIndex = 31
        Me.chkTags.Text = "Tags"
        Me.chkTags.UseVisualStyleBackColor = True
        '
        'chkTop250
        '
        Me.chkTop250.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTop250.AutoSize = True
        Me.chkTop250.Location = New System.Drawing.Point(3, 525)
        Me.chkTop250.Name = "chkTop250"
        Me.chkTop250.Size = New System.Drawing.Size(66, 17)
        Me.chkTop250.TabIndex = 34
        Me.chkTop250.Text = "Top 250"
        Me.chkTop250.UseVisualStyleBackColor = True
        '
        'chkTrailer
        '
        Me.chkTrailer.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTrailer.AutoSize = True
        Me.chkTrailer.Location = New System.Drawing.Point(3, 548)
        Me.chkTrailer.Name = "chkTrailer"
        Me.chkTrailer.Size = New System.Drawing.Size(55, 17)
        Me.chkTrailer.TabIndex = 35
        Me.chkTrailer.Text = "Trailer"
        Me.chkTrailer.UseVisualStyleBackColor = True
        '
        'chkWriters
        '
        Me.chkWriters.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkWriters.AutoSize = True
        Me.chkWriters.Location = New System.Drawing.Point(3, 624)
        Me.chkWriters.Name = "chkWriters"
        Me.chkWriters.Size = New System.Drawing.Size(100, 17)
        Me.chkWriters.TabIndex = 40
        Me.chkWriters.Text = "Credits (Writers)"
        Me.chkWriters.UseVisualStyleBackColor = True
        '
        'chkVideoSource
        '
        Me.chkVideoSource.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkVideoSource.AutoSize = True
        Me.chkVideoSource.Location = New System.Drawing.Point(3, 598)
        Me.chkVideoSource.Name = "chkVideoSource"
        Me.chkVideoSource.Size = New System.Drawing.Size(87, 17)
        Me.chkVideoSource.TabIndex = 38
        Me.chkVideoSource.Text = "VideoSource"
        Me.chkVideoSource.UseVisualStyleBackColor = True
        '
        'chkUserRating
        '
        Me.chkUserRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkUserRating.AutoSize = True
        Me.chkUserRating.Location = New System.Drawing.Point(3, 572)
        Me.chkUserRating.Name = "chkUserRating"
        Me.chkUserRating.Size = New System.Drawing.Size(82, 17)
        Me.chkUserRating.TabIndex = 36
        Me.chkUserRating.Text = "User Rating"
        Me.chkUserRating.UseVisualStyleBackColor = True
        '
        'txtVideoSource
        '
        Me.txtVideoSource.Enabled = False
        Me.txtVideoSource.Location = New System.Drawing.Point(109, 597)
        Me.txtVideoSource.Name = "txtVideoSource"
        Me.txtVideoSource.Size = New System.Drawing.Size(200, 20)
        Me.txtVideoSource.TabIndex = 39
        '
        'txtTagline
        '
        Me.txtTagline.Enabled = False
        Me.txtTagline.Location = New System.Drawing.Point(109, 450)
        Me.txtTagline.Name = "txtTagline"
        Me.txtTagline.Size = New System.Drawing.Size(200, 20)
        Me.txtTagline.TabIndex = 30
        '
        'txtMPAA
        '
        Me.txtMPAA.Enabled = False
        Me.txtMPAA.Location = New System.Drawing.Point(109, 231)
        Me.txtMPAA.Name = "txtMPAA"
        Me.txtMPAA.Size = New System.Drawing.Size(200, 20)
        Me.txtMPAA.TabIndex = 15
        '
        'chkAired
        '
        Me.chkAired.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkAired.AutoSize = True
        Me.chkAired.Location = New System.Drawing.Point(3, 27)
        Me.chkAired.Name = "chkAired"
        Me.chkAired.Size = New System.Drawing.Size(50, 17)
        Me.chkAired.TabIndex = 1
        Me.chkAired.Text = "Aired"
        Me.chkAired.UseVisualStyleBackColor = True
        '
        'chkGuestStars
        '
        Me.chkGuestStars.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGuestStars.AutoSize = True
        Me.chkGuestStars.Location = New System.Drawing.Point(3, 208)
        Me.chkGuestStars.Name = "chkGuestStars"
        Me.chkGuestStars.Size = New System.Drawing.Size(81, 17)
        Me.chkGuestStars.TabIndex = 13
        Me.chkGuestStars.Text = "Guest Stars"
        Me.chkGuestStars.UseVisualStyleBackColor = True
        '
        'chkCreators
        '
        Me.chkCreators.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCreators.AutoSize = True
        Me.chkCreators.Location = New System.Drawing.Point(3, 105)
        Me.chkCreators.Name = "chkCreators"
        Me.chkCreators.Size = New System.Drawing.Size(65, 17)
        Me.chkCreators.TabIndex = 7
        Me.chkCreators.Text = "Creators"
        Me.chkCreators.UseVisualStyleBackColor = True
        '
        'chkPremiered
        '
        Me.chkPremiered.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPremiered.AutoSize = True
        Me.chkPremiered.Location = New System.Drawing.Point(3, 327)
        Me.chkPremiered.Name = "chkPremiered"
        Me.chkPremiered.Size = New System.Drawing.Size(73, 17)
        Me.chkPremiered.TabIndex = 19
        Me.chkPremiered.Text = "Premiered"
        Me.chkPremiered.UseVisualStyleBackColor = True
        '
        'txtPremiered
        '
        Me.txtPremiered.Enabled = False
        Me.txtPremiered.Location = New System.Drawing.Point(109, 326)
        Me.txtPremiered.Name = "txtPremiered"
        Me.txtPremiered.Size = New System.Drawing.Size(90, 20)
        Me.txtPremiered.TabIndex = 20
        Me.txtPremiered.WatermarkColor = System.Drawing.Color.Gray
        Me.txtPremiered.WatermarkText = "yyyy-MM-dd"
        '
        'txtUserRating
        '
        Me.txtUserRating.Enabled = False
        Me.txtUserRating.Location = New System.Drawing.Point(109, 571)
        Me.txtUserRating.Name = "txtUserRating"
        Me.txtUserRating.Size = New System.Drawing.Size(40, 20)
        Me.txtUserRating.TabIndex = 37
        Me.txtUserRating.WatermarkColor = System.Drawing.Color.Gray
        Me.txtUserRating.WatermarkText = "0-9"
        '
        'txtAired
        '
        Me.txtAired.Enabled = False
        Me.txtAired.Location = New System.Drawing.Point(109, 26)
        Me.txtAired.Name = "txtAired"
        Me.txtAired.Size = New System.Drawing.Size(90, 20)
        Me.txtAired.TabIndex = 2
        Me.txtAired.WatermarkColor = System.Drawing.Color.Gray
        Me.txtAired.WatermarkText = "yyyy-MM-dd"
        '
        'chkStatus
        '
        Me.chkStatus.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkStatus.AutoSize = True
        Me.chkStatus.Location = New System.Drawing.Point(3, 399)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Size = New System.Drawing.Size(56, 17)
        Me.chkStatus.TabIndex = 25
        Me.chkStatus.Text = "Status"
        Me.chkStatus.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.Enabled = False
        Me.txtStatus.Location = New System.Drawing.Point(109, 398)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(200, 20)
        Me.txtStatus.TabIndex = 26
        '
        'chkTitle
        '
        Me.chkTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitle.AutoSize = True
        Me.chkTitle.Location = New System.Drawing.Point(3, 502)
        Me.chkTitle.Name = "chkTitle"
        Me.chkTitle.Size = New System.Drawing.Size(46, 17)
        Me.chkTitle.TabIndex = 33
        Me.chkTitle.Text = "Title"
        Me.chkTitle.UseVisualStyleBackColor = True
        '
        'txtCertifications
        '
        Me.txtCertifications.Enabled = False
        Me.txtCertifications.Location = New System.Drawing.Point(109, 52)
        Me.txtCertifications.Name = "txtCertifications"
        Me.txtCertifications.Size = New System.Drawing.Size(200, 20)
        Me.txtCertifications.TabIndex = 4
        Me.txtCertifications.WatermarkColor = System.Drawing.Color.Gray
        Me.txtCertifications.WatermarkText = "comma separated"
        '
        'txtCountries
        '
        Me.txtCountries.Enabled = False
        Me.txtCountries.Location = New System.Drawing.Point(109, 78)
        Me.txtCountries.Name = "txtCountries"
        Me.txtCountries.Size = New System.Drawing.Size(200, 20)
        Me.txtCountries.TabIndex = 6
        Me.txtCountries.WatermarkColor = System.Drawing.Color.Gray
        Me.txtCountries.WatermarkText = "comma separated"
        '
        'txtCreators
        '
        Me.txtCreators.Enabled = False
        Me.txtCreators.Location = New System.Drawing.Point(109, 104)
        Me.txtCreators.Name = "txtCreators"
        Me.txtCreators.Size = New System.Drawing.Size(200, 20)
        Me.txtCreators.TabIndex = 8
        Me.txtCreators.WatermarkColor = System.Drawing.Color.Gray
        Me.txtCreators.WatermarkText = "comma separated"
        '
        'txtDirectors
        '
        Me.txtDirectors.Enabled = False
        Me.txtDirectors.Location = New System.Drawing.Point(109, 130)
        Me.txtDirectors.Name = "txtDirectors"
        Me.txtDirectors.Size = New System.Drawing.Size(200, 20)
        Me.txtDirectors.TabIndex = 10
        Me.txtDirectors.WatermarkColor = System.Drawing.Color.Gray
        Me.txtDirectors.WatermarkText = "comma separated"
        '
        'txtGenres
        '
        Me.txtGenres.Enabled = False
        Me.txtGenres.Location = New System.Drawing.Point(109, 182)
        Me.txtGenres.Name = "txtGenres"
        Me.txtGenres.Size = New System.Drawing.Size(200, 20)
        Me.txtGenres.TabIndex = 12
        Me.txtGenres.WatermarkColor = System.Drawing.Color.Gray
        Me.txtGenres.WatermarkText = "comma separated"
        '
        'txtStudios
        '
        Me.txtStudios.Enabled = False
        Me.txtStudios.Location = New System.Drawing.Point(109, 424)
        Me.txtStudios.Name = "txtStudios"
        Me.txtStudios.Size = New System.Drawing.Size(200, 20)
        Me.txtStudios.TabIndex = 28
        Me.txtStudios.WatermarkColor = System.Drawing.Color.Gray
        Me.txtStudios.WatermarkText = "comma separated"
        '
        'txtTags
        '
        Me.txtTags.Enabled = False
        Me.txtTags.Location = New System.Drawing.Point(109, 476)
        Me.txtTags.Name = "txtTags"
        Me.txtTags.Size = New System.Drawing.Size(200, 20)
        Me.txtTags.TabIndex = 32
        Me.txtTags.WatermarkColor = System.Drawing.Color.Gray
        Me.txtTags.WatermarkText = "comma separated"
        '
        'txtWriters
        '
        Me.txtWriters.Enabled = False
        Me.txtWriters.Location = New System.Drawing.Point(109, 623)
        Me.txtWriters.Name = "txtWriters"
        Me.txtWriters.Size = New System.Drawing.Size(200, 20)
        Me.txtWriters.TabIndex = 41
        Me.txtWriters.WatermarkColor = System.Drawing.Color.Gray
        Me.txtWriters.WatermarkText = "comma separated"
        '
        'chkPlot
        '
        Me.chkPlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPlot.AutoSize = True
        Me.chkPlot.Location = New System.Drawing.Point(3, 280)
        Me.chkPlot.Name = "chkPlot"
        Me.chkPlot.Size = New System.Drawing.Size(44, 17)
        Me.chkPlot.TabIndex = 17
        Me.chkPlot.Text = "Plot"
        Me.chkPlot.UseVisualStyleBackColor = True
        '
        'lblActors
        '
        Me.lblActors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblActors.AutoSize = True
        Me.lblActors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActors.ForeColor = System.Drawing.Color.Red
        Me.lblActors.Location = New System.Drawing.Point(109, 5)
        Me.lblActors.Name = "lblActors"
        Me.lblActors.Size = New System.Drawing.Size(74, 13)
        Me.lblActors.TabIndex = 13
        Me.lblActors.Text = "will be cleared"
        Me.lblActors.Visible = False
        '
        'lblGuestStars
        '
        Me.lblGuestStars.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGuestStars.AutoSize = True
        Me.lblGuestStars.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGuestStars.ForeColor = System.Drawing.Color.Red
        Me.lblGuestStars.Location = New System.Drawing.Point(109, 210)
        Me.lblGuestStars.Name = "lblGuestStars"
        Me.lblGuestStars.Size = New System.Drawing.Size(74, 13)
        Me.lblGuestStars.TabIndex = 13
        Me.lblGuestStars.Text = "will be cleared"
        Me.lblGuestStars.Visible = False
        '
        'lblOriginalTitle
        '
        Me.lblOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOriginalTitle.AutoSize = True
        Me.lblOriginalTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOriginalTitle.ForeColor = System.Drawing.Color.Red
        Me.lblOriginalTitle.Location = New System.Drawing.Point(109, 259)
        Me.lblOriginalTitle.Name = "lblOriginalTitle"
        Me.lblOriginalTitle.Size = New System.Drawing.Size(74, 13)
        Me.lblOriginalTitle.TabIndex = 13
        Me.lblOriginalTitle.Text = "will be cleared"
        Me.lblOriginalTitle.Visible = False
        '
        'lblPlot
        '
        Me.lblPlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlot.ForeColor = System.Drawing.Color.Red
        Me.lblPlot.Location = New System.Drawing.Point(109, 282)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(74, 13)
        Me.lblPlot.TabIndex = 13
        Me.lblPlot.Text = "will be cleared"
        Me.lblPlot.Visible = False
        '
        'lblOutline
        '
        Me.lblOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOutline.AutoSize = True
        Me.lblOutline.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutline.ForeColor = System.Drawing.Color.Red
        Me.lblOutline.Location = New System.Drawing.Point(109, 305)
        Me.lblOutline.Name = "lblOutline"
        Me.lblOutline.Size = New System.Drawing.Size(74, 13)
        Me.lblOutline.TabIndex = 13
        Me.lblOutline.Text = "will be cleared"
        Me.lblOutline.Visible = False
        '
        'lblRating
        '
        Me.lblRating.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRating.AutoSize = True
        Me.lblRating.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRating.ForeColor = System.Drawing.Color.Red
        Me.lblRating.Location = New System.Drawing.Point(109, 354)
        Me.lblRating.Name = "lblRating"
        Me.lblRating.Size = New System.Drawing.Size(74, 13)
        Me.lblRating.TabIndex = 13
        Me.lblRating.Text = "will be cleared"
        Me.lblRating.Visible = False
        '
        'lblRuntime
        '
        Me.lblRuntime.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRuntime.AutoSize = True
        Me.lblRuntime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuntime.ForeColor = System.Drawing.Color.Red
        Me.lblRuntime.Location = New System.Drawing.Point(109, 377)
        Me.lblRuntime.Name = "lblRuntime"
        Me.lblRuntime.Size = New System.Drawing.Size(74, 13)
        Me.lblRuntime.TabIndex = 13
        Me.lblRuntime.Text = "will be cleared"
        Me.lblRuntime.Visible = False
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Red
        Me.lblTitle.Location = New System.Drawing.Point(109, 504)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(74, 13)
        Me.lblTitle.TabIndex = 13
        Me.lblTitle.Text = "will be cleared"
        Me.lblTitle.Visible = False
        '
        'lblTop250
        '
        Me.lblTop250.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTop250.AutoSize = True
        Me.lblTop250.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTop250.ForeColor = System.Drawing.Color.Red
        Me.lblTop250.Location = New System.Drawing.Point(109, 527)
        Me.lblTop250.Name = "lblTop250"
        Me.lblTop250.Size = New System.Drawing.Size(74, 13)
        Me.lblTop250.TabIndex = 13
        Me.lblTop250.Text = "will be cleared"
        Me.lblTop250.Visible = False
        '
        'lblTrailer
        '
        Me.lblTrailer.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTrailer.AutoSize = True
        Me.lblTrailer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrailer.ForeColor = System.Drawing.Color.Red
        Me.lblTrailer.Location = New System.Drawing.Point(109, 550)
        Me.lblTrailer.Name = "lblTrailer"
        Me.lblTrailer.Size = New System.Drawing.Size(74, 13)
        Me.lblTrailer.TabIndex = 13
        Me.lblTrailer.Text = "will be cleared"
        Me.lblTrailer.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(334, 723)
        Me.pnlMain.TabIndex = 3
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 723)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(334, 29)
        Me.pnlBottom.TabIndex = 4
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 3
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnOK, 1, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 2, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(334, 29)
        Me.tblBottom.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(175, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(256, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkEdition
        '
        Me.chkEdition.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkEdition.AutoSize = True
        Me.chkEdition.Location = New System.Drawing.Point(3, 157)
        Me.chkEdition.Name = "chkEdition"
        Me.chkEdition.Size = New System.Drawing.Size(58, 17)
        Me.chkEdition.TabIndex = 42
        Me.chkEdition.Text = "Edition"
        Me.chkEdition.UseVisualStyleBackColor = True
        '
        'txtEdition
        '
        Me.txtEdition.Enabled = False
        Me.txtEdition.Location = New System.Drawing.Point(109, 156)
        Me.txtEdition.Name = "txtEdition"
        Me.txtEdition.Size = New System.Drawing.Size(200, 20)
        Me.txtEdition.TabIndex = 26
        '
        'dlgClearOrReplace
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(334, 752)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(350, 39)
        Me.Name = "dlgClearOrReplace"
        Me.Text = "Clear or Replace Data Fields"
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tblMain As TableLayoutPanel
    Friend WithEvents pnlMain As Panel
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents chkActors As CheckBox
    Friend WithEvents chkCertifications As CheckBox
    Friend WithEvents chkCountries As CheckBox
    Friend WithEvents chkDirectors As CheckBox
    Friend WithEvents chkGenres As CheckBox
    Friend WithEvents chkMPAA As CheckBox
    Friend WithEvents chkOriginalTitle As CheckBox
    Friend WithEvents chkOutline As CheckBox
    Friend WithEvents chkPlot As CheckBox
    Friend WithEvents chkRating As CheckBox
    Friend WithEvents chkRuntime As CheckBox
    Friend WithEvents chkStudios As CheckBox
    Friend WithEvents chkTagline As CheckBox
    Friend WithEvents chkTags As CheckBox
    Friend WithEvents chkTop250 As CheckBox
    Friend WithEvents chkTrailer As CheckBox
    Friend WithEvents chkUserRating As CheckBox
    Friend WithEvents chkWriters As CheckBox
    Friend WithEvents chkVideoSource As CheckBox
    Friend WithEvents txtVideoSource As TextBox
    Friend WithEvents txtTagline As TextBox
    Friend WithEvents txtMPAA As TextBox
    Friend WithEvents chkAired As CheckBox
    Friend WithEvents chkGuestStars As CheckBox
    Friend WithEvents chkCreators As CheckBox
    Friend WithEvents chkPremiered As CheckBox
    Friend WithEvents txtPremiered As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtUserRating As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtAired As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents chkStatus As CheckBox
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents chkTitle As CheckBox
    Friend WithEvents txtCertifications As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtCountries As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtCreators As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtDirectors As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtGenres As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtStudios As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtTags As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents txtWriters As EmberAPI.AdvancedControls.TextBox_with_Watermark
    Friend WithEvents lblActors As Label
    Friend WithEvents lblGuestStars As Label
    Friend WithEvents lblOriginalTitle As Label
    Friend WithEvents lblPlot As Label
    Friend WithEvents lblOutline As Label
    Friend WithEvents lblRating As Label
    Friend WithEvents lblRuntime As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblTop250 As Label
    Friend WithEvents lblTrailer As Label
    Friend WithEvents chkEdition As CheckBox
    Friend WithEvents txtEdition As TextBox
End Class
