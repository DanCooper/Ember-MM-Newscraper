<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsHolder_TV
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettingsHolder_TV))
        Me.chkScraperShowCertifications = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowCountries = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowCreators = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowGenres = New System.Windows.Forms.CheckBox()
        Me.pbIconBottom = New System.Windows.Forms.PictureBox()
        Me.gbScraperFieldsEpisode = New System.Windows.Forms.GroupBox()
        Me.tblScraperFieldsEpisode = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScraperEpisodeAired = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeActors = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeRating = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeCredits = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodePlot = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeTitle = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeGuestStars = New System.Windows.Forms.CheckBox()
        Me.chkScraperEpisodeDirectors = New System.Windows.Forms.CheckBox()
        Me.gbScraperFieldsSeason = New System.Windows.Forms.GroupBox()
        Me.tblScraperFieldsSeason = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScraperSeasonAired = New System.Windows.Forms.CheckBox()
        Me.chkScraperSeasonPlot = New System.Windows.Forms.CheckBox()
        Me.chkScraperSeasonTitle = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsBottom = New System.Windows.Forms.Panel()
        Me.tblSettingsBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblInfoBottom = New System.Windows.Forms.Label()
        Me.chkScraperShowActors = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowPlot = New System.Windows.Forms.CheckBox()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.lblScraperOrder = New System.Windows.Forms.Label()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.gbScraperOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.pbApiPinInfo = New System.Windows.Forms.PictureBox()
        Me.txtApiPin = New System.Windows.Forms.TextBox()
        Me.chkFallBackEng = New System.Windows.Forms.CheckBox()
        Me.lblApiPin = New System.Windows.Forms.Label()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblScraperFieldsShow = New System.Windows.Forms.TableLayoutPanel()
        Me.chkScraperShowOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowRuntime = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowRating = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowPremiered = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowStatus = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowStudios = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowTitle = New System.Windows.Forms.CheckBox()
        Me.chkScraperShowTagline = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperFieldsOpts = New System.Windows.Forms.GroupBox()
        Me.tblScraperFieldsOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraperFieldsShow = New System.Windows.Forms.GroupBox()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbScraperFieldsEpisode.SuspendLayout()
        Me.tblScraperFieldsEpisode.SuspendLayout()
        Me.gbScraperFieldsSeason.SuspendLayout()
        Me.tblScraperFieldsSeason.SuspendLayout()
        Me.pnlSettingsBottom.SuspendLayout()
        Me.tblSettingsBottom.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.gbScraperOpts.SuspendLayout()
        Me.tblScraperOpts.SuspendLayout()
        CType(Me.pbApiPinInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblScraperFieldsShow.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbScraperFieldsOpts.SuspendLayout()
        Me.tblScraperFieldsOpts.SuspendLayout()
        Me.gbScraperFieldsShow.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkScraperShowCertifications
        '
        Me.chkScraperShowCertifications.AutoSize = True
        Me.chkScraperShowCertifications.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowCertifications.Location = New System.Drawing.Point(3, 26)
        Me.chkScraperShowCertifications.Name = "chkScraperShowCertifications"
        Me.chkScraperShowCertifications.Size = New System.Drawing.Size(94, 17)
        Me.chkScraperShowCertifications.TabIndex = 3
        Me.chkScraperShowCertifications.Text = "Certifications"
        Me.chkScraperShowCertifications.UseVisualStyleBackColor = True
        '
        'chkScraperShowCountries
        '
        Me.chkScraperShowCountries.AutoSize = True
        Me.chkScraperShowCountries.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowCountries.Location = New System.Drawing.Point(3, 49)
        Me.chkScraperShowCountries.Name = "chkScraperShowCountries"
        Me.chkScraperShowCountries.Size = New System.Drawing.Size(76, 17)
        Me.chkScraperShowCountries.TabIndex = 10
        Me.chkScraperShowCountries.Text = "Countries"
        Me.chkScraperShowCountries.UseVisualStyleBackColor = True
        '
        'chkScraperShowCreators
        '
        Me.chkScraperShowCreators.AutoSize = True
        Me.chkScraperShowCreators.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowCreators.Location = New System.Drawing.Point(3, 72)
        Me.chkScraperShowCreators.Name = "chkScraperShowCreators"
        Me.chkScraperShowCreators.Size = New System.Drawing.Size(69, 17)
        Me.chkScraperShowCreators.TabIndex = 10
        Me.chkScraperShowCreators.Text = "Creators"
        Me.chkScraperShowCreators.UseVisualStyleBackColor = True
        '
        'chkScraperShowGenres
        '
        Me.chkScraperShowGenres.AutoSize = True
        Me.chkScraperShowGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowGenres.Location = New System.Drawing.Point(3, 95)
        Me.chkScraperShowGenres.Name = "chkScraperShowGenres"
        Me.chkScraperShowGenres.Size = New System.Drawing.Size(62, 17)
        Me.chkScraperShowGenres.TabIndex = 2
        Me.chkScraperShowGenres.Text = "Genres"
        Me.chkScraperShowGenres.UseVisualStyleBackColor = True
        '
        'pbIconBottom
        '
        Me.pbIconBottom.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbIconBottom.Image = CType(resources.GetObject("pbIconBottom.Image"), System.Drawing.Image)
        Me.pbIconBottom.Location = New System.Drawing.Point(3, 3)
        Me.pbIconBottom.Name = "pbIconBottom"
        Me.pbIconBottom.Size = New System.Drawing.Size(30, 31)
        Me.pbIconBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbIconBottom.TabIndex = 96
        Me.pbIconBottom.TabStop = False
        '
        'gbScraperFieldsEpisode
        '
        Me.gbScraperFieldsEpisode.AutoSize = True
        Me.gbScraperFieldsEpisode.Controls.Add(Me.tblScraperFieldsEpisode)
        Me.gbScraperFieldsEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFieldsEpisode.Location = New System.Drawing.Point(269, 3)
        Me.gbScraperFieldsEpisode.Name = "gbScraperFieldsEpisode"
        Me.gbScraperFieldsEpisode.Size = New System.Drawing.Size(210, 182)
        Me.gbScraperFieldsEpisode.TabIndex = 1
        Me.gbScraperFieldsEpisode.TabStop = False
        Me.gbScraperFieldsEpisode.Text = "Episode"
        '
        'tblScraperFieldsEpisode
        '
        Me.tblScraperFieldsEpisode.AutoSize = True
        Me.tblScraperFieldsEpisode.ColumnCount = 3
        Me.tblScraperFieldsEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsEpisode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeAired, 0, 1)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeActors, 0, 0)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeRating, 1, 2)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeCredits, 0, 2)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodePlot, 1, 1)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeTitle, 1, 3)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeGuestStars, 1, 0)
        Me.tblScraperFieldsEpisode.Controls.Add(Me.chkScraperEpisodeDirectors, 0, 3)
        Me.tblScraperFieldsEpisode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsEpisode.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsEpisode.Name = "tblScraperFieldsEpisode"
        Me.tblScraperFieldsEpisode.RowCount = 5
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsEpisode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperFieldsEpisode.Size = New System.Drawing.Size(204, 161)
        Me.tblScraperFieldsEpisode.TabIndex = 0
        '
        'chkScraperEpisodeAired
        '
        Me.chkScraperEpisodeAired.AutoSize = True
        Me.chkScraperEpisodeAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeAired.Location = New System.Drawing.Point(3, 26)
        Me.chkScraperEpisodeAired.Name = "chkScraperEpisodeAired"
        Me.chkScraperEpisodeAired.Size = New System.Drawing.Size(53, 17)
        Me.chkScraperEpisodeAired.TabIndex = 4
        Me.chkScraperEpisodeAired.Text = "Aired"
        Me.chkScraperEpisodeAired.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeActors
        '
        Me.chkScraperEpisodeActors.AutoSize = True
        Me.chkScraperEpisodeActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeActors.Location = New System.Drawing.Point(3, 3)
        Me.chkScraperEpisodeActors.Name = "chkScraperEpisodeActors"
        Me.chkScraperEpisodeActors.Size = New System.Drawing.Size(58, 17)
        Me.chkScraperEpisodeActors.TabIndex = 0
        Me.chkScraperEpisodeActors.Text = "Actors"
        Me.chkScraperEpisodeActors.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeRating
        '
        Me.chkScraperEpisodeRating.AutoSize = True
        Me.chkScraperEpisodeRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeRating.Location = New System.Drawing.Point(117, 49)
        Me.chkScraperEpisodeRating.Name = "chkScraperEpisodeRating"
        Me.chkScraperEpisodeRating.Size = New System.Drawing.Size(60, 17)
        Me.chkScraperEpisodeRating.TabIndex = 5
        Me.chkScraperEpisodeRating.Text = "Rating"
        Me.chkScraperEpisodeRating.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeCredits
        '
        Me.chkScraperEpisodeCredits.AutoSize = True
        Me.chkScraperEpisodeCredits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeCredits.Location = New System.Drawing.Point(3, 49)
        Me.chkScraperEpisodeCredits.Name = "chkScraperEpisodeCredits"
        Me.chkScraperEpisodeCredits.Size = New System.Drawing.Size(108, 17)
        Me.chkScraperEpisodeCredits.TabIndex = 8
        Me.chkScraperEpisodeCredits.Text = "Credits (Writers)"
        Me.chkScraperEpisodeCredits.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodePlot
        '
        Me.chkScraperEpisodePlot.AutoSize = True
        Me.chkScraperEpisodePlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodePlot.Location = New System.Drawing.Point(117, 26)
        Me.chkScraperEpisodePlot.Name = "chkScraperEpisodePlot"
        Me.chkScraperEpisodePlot.Size = New System.Drawing.Size(46, 17)
        Me.chkScraperEpisodePlot.TabIndex = 6
        Me.chkScraperEpisodePlot.Text = "Plot"
        Me.chkScraperEpisodePlot.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeTitle
        '
        Me.chkScraperEpisodeTitle.AutoSize = True
        Me.chkScraperEpisodeTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeTitle.Location = New System.Drawing.Point(117, 72)
        Me.chkScraperEpisodeTitle.Name = "chkScraperEpisodeTitle"
        Me.chkScraperEpisodeTitle.Size = New System.Drawing.Size(48, 17)
        Me.chkScraperEpisodeTitle.TabIndex = 0
        Me.chkScraperEpisodeTitle.Text = "Title"
        Me.chkScraperEpisodeTitle.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeGuestStars
        '
        Me.chkScraperEpisodeGuestStars.AutoSize = True
        Me.chkScraperEpisodeGuestStars.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkScraperEpisodeGuestStars.Location = New System.Drawing.Point(117, 3)
        Me.chkScraperEpisodeGuestStars.Name = "chkScraperEpisodeGuestStars"
        Me.chkScraperEpisodeGuestStars.Size = New System.Drawing.Size(84, 17)
        Me.chkScraperEpisodeGuestStars.TabIndex = 10
        Me.chkScraperEpisodeGuestStars.Text = "Guest Stars"
        Me.chkScraperEpisodeGuestStars.UseVisualStyleBackColor = True
        '
        'chkScraperEpisodeDirectors
        '
        Me.chkScraperEpisodeDirectors.AutoSize = True
        Me.chkScraperEpisodeDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperEpisodeDirectors.Location = New System.Drawing.Point(3, 72)
        Me.chkScraperEpisodeDirectors.Name = "chkScraperEpisodeDirectors"
        Me.chkScraperEpisodeDirectors.Size = New System.Drawing.Size(72, 17)
        Me.chkScraperEpisodeDirectors.TabIndex = 7
        Me.chkScraperEpisodeDirectors.Text = "Directors"
        Me.chkScraperEpisodeDirectors.UseVisualStyleBackColor = True
        '
        'gbScraperFieldsSeason
        '
        Me.gbScraperFieldsSeason.AutoSize = True
        Me.gbScraperFieldsSeason.Controls.Add(Me.tblScraperFieldsSeason)
        Me.gbScraperFieldsSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFieldsSeason.Location = New System.Drawing.Point(198, 3)
        Me.gbScraperFieldsSeason.Name = "gbScraperFieldsSeason"
        Me.gbScraperFieldsSeason.Size = New System.Drawing.Size(65, 182)
        Me.gbScraperFieldsSeason.TabIndex = 2
        Me.gbScraperFieldsSeason.TabStop = False
        Me.gbScraperFieldsSeason.Text = "Season"
        '
        'tblScraperFieldsSeason
        '
        Me.tblScraperFieldsSeason.AutoSize = True
        Me.tblScraperFieldsSeason.ColumnCount = 2
        Me.tblScraperFieldsSeason.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsSeason.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsSeason.Controls.Add(Me.chkScraperSeasonAired, 0, 0)
        Me.tblScraperFieldsSeason.Controls.Add(Me.chkScraperSeasonPlot, 0, 1)
        Me.tblScraperFieldsSeason.Controls.Add(Me.chkScraperSeasonTitle, 0, 2)
        Me.tblScraperFieldsSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsSeason.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsSeason.Name = "tblScraperFieldsSeason"
        Me.tblScraperFieldsSeason.RowCount = 4
        Me.tblScraperFieldsSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsSeason.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsSeason.Size = New System.Drawing.Size(59, 161)
        Me.tblScraperFieldsSeason.TabIndex = 0
        '
        'chkScraperSeasonAired
        '
        Me.chkScraperSeasonAired.AutoSize = True
        Me.chkScraperSeasonAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperSeasonAired.Location = New System.Drawing.Point(3, 3)
        Me.chkScraperSeasonAired.Name = "chkScraperSeasonAired"
        Me.chkScraperSeasonAired.Size = New System.Drawing.Size(53, 17)
        Me.chkScraperSeasonAired.TabIndex = 4
        Me.chkScraperSeasonAired.Text = "Aired"
        Me.chkScraperSeasonAired.UseVisualStyleBackColor = True
        '
        'chkScraperSeasonPlot
        '
        Me.chkScraperSeasonPlot.AutoSize = True
        Me.chkScraperSeasonPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperSeasonPlot.Location = New System.Drawing.Point(3, 26)
        Me.chkScraperSeasonPlot.Name = "chkScraperSeasonPlot"
        Me.chkScraperSeasonPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkScraperSeasonPlot.TabIndex = 4
        Me.chkScraperSeasonPlot.Text = "Plot"
        Me.chkScraperSeasonPlot.UseVisualStyleBackColor = True
        '
        'chkScraperSeasonTitle
        '
        Me.chkScraperSeasonTitle.AutoSize = True
        Me.chkScraperSeasonTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperSeasonTitle.Location = New System.Drawing.Point(3, 49)
        Me.chkScraperSeasonTitle.Name = "chkScraperSeasonTitle"
        Me.chkScraperSeasonTitle.Size = New System.Drawing.Size(48, 17)
        Me.chkScraperSeasonTitle.TabIndex = 4
        Me.chkScraperSeasonTitle.Text = "Title"
        Me.chkScraperSeasonTitle.UseVisualStyleBackColor = True
        '
        'pnlSettingsBottom
        '
        Me.pnlSettingsBottom.AutoSize = True
        Me.pnlSettingsBottom.Controls.Add(Me.tblSettingsBottom)
        Me.pnlSettingsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSettingsBottom.Location = New System.Drawing.Point(0, 378)
        Me.pnlSettingsBottom.Name = "pnlSettingsBottom"
        Me.pnlSettingsBottom.Size = New System.Drawing.Size(525, 37)
        Me.pnlSettingsBottom.TabIndex = 97
        '
        'tblSettingsBottom
        '
        Me.tblSettingsBottom.AutoSize = True
        Me.tblSettingsBottom.ColumnCount = 3
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsBottom.Controls.Add(Me.pbIconBottom, 0, 0)
        Me.tblSettingsBottom.Controls.Add(Me.lblInfoBottom, 1, 0)
        Me.tblSettingsBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsBottom.Name = "tblSettingsBottom"
        Me.tblSettingsBottom.RowCount = 2
        Me.tblSettingsBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsBottom.Size = New System.Drawing.Size(525, 37)
        Me.tblSettingsBottom.TabIndex = 0
        '
        'lblInfoBottom
        '
        Me.lblInfoBottom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInfoBottom.AutoSize = True
        Me.lblInfoBottom.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblInfoBottom.ForeColor = System.Drawing.Color.Blue
        Me.lblInfoBottom.Location = New System.Drawing.Point(39, 6)
        Me.lblInfoBottom.Name = "lblInfoBottom"
        Me.lblInfoBottom.Size = New System.Drawing.Size(205, 24)
        Me.lblInfoBottom.TabIndex = 4
        Me.lblInfoBottom.Text = "These settings are specific to this module." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please refer to the global settings " &
    "for more options."
        Me.lblInfoBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkScraperShowActors
        '
        Me.chkScraperShowActors.AutoSize = True
        Me.chkScraperShowActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowActors.Location = New System.Drawing.Point(3, 3)
        Me.chkScraperShowActors.Name = "chkScraperShowActors"
        Me.chkScraperShowActors.Size = New System.Drawing.Size(58, 17)
        Me.chkScraperShowActors.TabIndex = 8
        Me.chkScraperShowActors.Text = "Actors"
        Me.chkScraperShowActors.UseVisualStyleBackColor = True
        '
        'chkScraperShowPlot
        '
        Me.chkScraperShowPlot.AutoSize = True
        Me.chkScraperShowPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowPlot.Location = New System.Drawing.Point(3, 141)
        Me.chkScraperShowPlot.Name = "chkScraperShowPlot"
        Me.chkScraperShowPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkScraperShowPlot.TabIndex = 4
        Me.chkScraperShowPlot.Text = "Plot"
        Me.chkScraperShowPlot.UseVisualStyleBackColor = True
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 5
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.btnDown, 4, 0)
        Me.tblSettingsTop.Controls.Add(Me.lblScraperOrder, 2, 0)
        Me.tblSettingsTop.Controls.Add(Me.btnUp, 3, 0)
        Me.tblSettingsTop.Controls.Add(Me.chkEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(525, 29)
        Me.tblSettingsTop.TabIndex = 98
        '
        'btnDown
        '
        Me.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(499, 3)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(23, 23)
        Me.btnDown.TabIndex = 3
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'lblScraperOrder
        '
        Me.lblScraperOrder.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblScraperOrder.AutoSize = True
        Me.lblScraperOrder.Font = New System.Drawing.Font("Segoe UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScraperOrder.Location = New System.Drawing.Point(406, 8)
        Me.lblScraperOrder.Name = "lblScraperOrder"
        Me.lblScraperOrder.Size = New System.Drawing.Size(58, 12)
        Me.lblScraperOrder.TabIndex = 1
        Me.lblScraperOrder.Text = "Scraper order"
        '
        'btnUp
        '
        Me.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(470, 3)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.TabIndex = 2
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'chkEnabled
        '
        Me.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnabled.Location = New System.Drawing.Point(8, 6)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'gbScraperOpts
        '
        Me.gbScraperOpts.AutoSize = True
        Me.gbScraperOpts.Controls.Add(Me.tblScraperOpts)
        Me.gbScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScraperOpts.Location = New System.Drawing.Point(3, 218)
        Me.gbScraperOpts.Name = "gbScraperOpts"
        Me.gbScraperOpts.Size = New System.Drawing.Size(488, 72)
        Me.gbScraperOpts.TabIndex = 1
        Me.gbScraperOpts.TabStop = False
        Me.gbScraperOpts.Text = "Scraper Options"
        '
        'tblScraperOpts
        '
        Me.tblScraperOpts.AutoSize = True
        Me.tblScraperOpts.ColumnCount = 4
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperOpts.Controls.Add(Me.pbApiPinInfo, 2, 0)
        Me.tblScraperOpts.Controls.Add(Me.txtApiPin, 1, 0)
        Me.tblScraperOpts.Controls.Add(Me.chkFallBackEng, 0, 1)
        Me.tblScraperOpts.Controls.Add(Me.lblApiPin, 0, 0)
        Me.tblScraperOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperOpts.Name = "tblScraperOpts"
        Me.tblScraperOpts.RowCount = 3
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperOpts.Size = New System.Drawing.Size(482, 51)
        Me.tblScraperOpts.TabIndex = 98
        '
        'pbApiPinInfo
        '
        Me.pbApiPinInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pbApiPinInfo.Image = CType(resources.GetObject("pbApiPinInfo.Image"), System.Drawing.Image)
        Me.pbApiPinInfo.Location = New System.Drawing.Point(321, 6)
        Me.pbApiPinInfo.Name = "pbApiPinInfo"
        Me.pbApiPinInfo.Size = New System.Drawing.Size(16, 16)
        Me.pbApiPinInfo.TabIndex = 5
        Me.pbApiPinInfo.TabStop = False
        '
        'txtApiPin
        '
        Me.txtApiPin.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtApiPin.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApiPin.Location = New System.Drawing.Point(85, 3)
        Me.txtApiPin.Name = "txtApiPin"
        Me.txtApiPin.Size = New System.Drawing.Size(230, 22)
        Me.txtApiPin.TabIndex = 1
        '
        'chkFallBackEng
        '
        Me.chkFallBackEng.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFallBackEng.AutoSize = True
        Me.tblScraperOpts.SetColumnSpan(Me.chkFallBackEng, 2)
        Me.chkFallBackEng.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFallBackEng.Location = New System.Drawing.Point(3, 31)
        Me.chkFallBackEng.Name = "chkFallBackEng"
        Me.chkFallBackEng.Size = New System.Drawing.Size(123, 17)
        Me.chkFallBackEng.TabIndex = 4
        Me.chkFallBackEng.Text = "Fallback to english"
        Me.chkFallBackEng.UseVisualStyleBackColor = True
        '
        'lblApiPin
        '
        Me.lblApiPin.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblApiPin.AutoSize = True
        Me.lblApiPin.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApiPin.Location = New System.Drawing.Point(3, 7)
        Me.lblApiPin.Name = "lblApiPin"
        Me.lblApiPin.Size = New System.Drawing.Size(76, 13)
        Me.lblApiPin.TabIndex = 0
        Me.lblApiPin.Text = "TVDb API Pin:"
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(525, 29)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'tblScraperFieldsShow
        '
        Me.tblScraperFieldsShow.AutoSize = True
        Me.tblScraperFieldsShow.ColumnCount = 3
        Me.tblScraperFieldsShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsShow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowActors, 0, 0)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowCertifications, 0, 1)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowCountries, 0, 2)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowCreators, 0, 3)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowGenres, 0, 4)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowPlot, 0, 6)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowOriginalTitle, 0, 5)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowRuntime, 1, 2)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowRating, 1, 1)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowPremiered, 1, 0)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowStatus, 1, 3)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowStudios, 1, 4)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowTitle, 1, 6)
        Me.tblScraperFieldsShow.Controls.Add(Me.chkScraperShowTagline, 1, 5)
        Me.tblScraperFieldsShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsShow.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsShow.Name = "tblScraperFieldsShow"
        Me.tblScraperFieldsShow.RowCount = 8
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsShow.Size = New System.Drawing.Size(183, 161)
        Me.tblScraperFieldsShow.TabIndex = 0
        '
        'chkScraperShowOriginalTitle
        '
        Me.chkScraperShowOriginalTitle.AutoSize = True
        Me.chkScraperShowOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowOriginalTitle.Location = New System.Drawing.Point(3, 118)
        Me.chkScraperShowOriginalTitle.Name = "chkScraperShowOriginalTitle"
        Me.chkScraperShowOriginalTitle.Size = New System.Drawing.Size(93, 17)
        Me.chkScraperShowOriginalTitle.TabIndex = 0
        Me.chkScraperShowOriginalTitle.Text = "Original Title"
        Me.chkScraperShowOriginalTitle.UseVisualStyleBackColor = True
        '
        'chkScraperShowRuntime
        '
        Me.chkScraperShowRuntime.AutoSize = True
        Me.chkScraperShowRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowRuntime.Location = New System.Drawing.Point(103, 49)
        Me.chkScraperShowRuntime.Name = "chkScraperShowRuntime"
        Me.chkScraperShowRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkScraperShowRuntime.TabIndex = 10
        Me.chkScraperShowRuntime.Text = "Runtime"
        Me.chkScraperShowRuntime.UseVisualStyleBackColor = True
        '
        'chkScraperShowRating
        '
        Me.chkScraperShowRating.AutoSize = True
        Me.chkScraperShowRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowRating.Location = New System.Drawing.Point(103, 26)
        Me.chkScraperShowRating.Name = "chkScraperShowRating"
        Me.chkScraperShowRating.Size = New System.Drawing.Size(60, 17)
        Me.chkScraperShowRating.TabIndex = 6
        Me.chkScraperShowRating.Text = "Rating"
        Me.chkScraperShowRating.UseVisualStyleBackColor = True
        '
        'chkScraperShowPremiered
        '
        Me.chkScraperShowPremiered.AutoSize = True
        Me.chkScraperShowPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowPremiered.Location = New System.Drawing.Point(103, 3)
        Me.chkScraperShowPremiered.Name = "chkScraperShowPremiered"
        Me.chkScraperShowPremiered.Size = New System.Drawing.Size(77, 17)
        Me.chkScraperShowPremiered.TabIndex = 5
        Me.chkScraperShowPremiered.Text = "Premiered"
        Me.chkScraperShowPremiered.UseVisualStyleBackColor = True
        '
        'chkScraperShowStatus
        '
        Me.chkScraperShowStatus.AutoSize = True
        Me.chkScraperShowStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowStatus.Location = New System.Drawing.Point(103, 72)
        Me.chkScraperShowStatus.Name = "chkScraperShowStatus"
        Me.chkScraperShowStatus.Size = New System.Drawing.Size(58, 17)
        Me.chkScraperShowStatus.TabIndex = 9
        Me.chkScraperShowStatus.Text = "Status"
        Me.chkScraperShowStatus.UseVisualStyleBackColor = True
        '
        'chkScraperShowStudios
        '
        Me.chkScraperShowStudios.AutoSize = True
        Me.chkScraperShowStudios.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowStudios.Location = New System.Drawing.Point(103, 95)
        Me.chkScraperShowStudios.Name = "chkScraperShowStudios"
        Me.chkScraperShowStudios.Size = New System.Drawing.Size(65, 17)
        Me.chkScraperShowStudios.TabIndex = 7
        Me.chkScraperShowStudios.Text = "Studios"
        Me.chkScraperShowStudios.UseVisualStyleBackColor = True
        '
        'chkScraperShowTitle
        '
        Me.chkScraperShowTitle.AutoSize = True
        Me.chkScraperShowTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowTitle.Location = New System.Drawing.Point(103, 141)
        Me.chkScraperShowTitle.Name = "chkScraperShowTitle"
        Me.chkScraperShowTitle.Size = New System.Drawing.Size(48, 17)
        Me.chkScraperShowTitle.TabIndex = 0
        Me.chkScraperShowTitle.Text = "Title"
        Me.chkScraperShowTitle.UseVisualStyleBackColor = True
        '
        'chkScraperShowTagline
        '
        Me.chkScraperShowTagline.AutoSize = True
        Me.chkScraperShowTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScraperShowTagline.Location = New System.Drawing.Point(103, 118)
        Me.chkScraperShowTagline.Name = "chkScraperShowTagline"
        Me.chkScraperShowTagline.Size = New System.Drawing.Size(63, 17)
        Me.chkScraperShowTagline.TabIndex = 0
        Me.chkScraperShowTagline.Text = "Tagline"
        Me.chkScraperShowTagline.UseVisualStyleBackColor = True
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 29)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(525, 349)
        Me.pnlSettingsMain.TabIndex = 98
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 2
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbScraperFieldsOpts, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.gbScraperOpts, 0, 1)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 3
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(525, 349)
        Me.tblSettingsMain.TabIndex = 0
        '
        'gbScraperFieldsOpts
        '
        Me.gbScraperFieldsOpts.AutoSize = True
        Me.gbScraperFieldsOpts.Controls.Add(Me.tblScraperFieldsOpts)
        Me.gbScraperFieldsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFieldsOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbScraperFieldsOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperFieldsOpts.Name = "gbScraperFieldsOpts"
        Me.gbScraperFieldsOpts.Size = New System.Drawing.Size(488, 209)
        Me.gbScraperFieldsOpts.TabIndex = 3
        Me.gbScraperFieldsOpts.TabStop = False
        Me.gbScraperFieldsOpts.Text = "Scraper Fields - Scraper specific"
        '
        'tblScraperFieldsOpts
        '
        Me.tblScraperFieldsOpts.AutoSize = True
        Me.tblScraperFieldsOpts.ColumnCount = 4
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFieldsOpts.Controls.Add(Me.gbScraperFieldsShow, 0, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.gbScraperFieldsEpisode, 2, 0)
        Me.tblScraperFieldsOpts.Controls.Add(Me.gbScraperFieldsSeason, 1, 0)
        Me.tblScraperFieldsOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFieldsOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFieldsOpts.Name = "tblScraperFieldsOpts"
        Me.tblScraperFieldsOpts.RowCount = 2
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFieldsOpts.Size = New System.Drawing.Size(482, 188)
        Me.tblScraperFieldsOpts.TabIndex = 99
        '
        'gbScraperFieldsShow
        '
        Me.gbScraperFieldsShow.AutoSize = True
        Me.gbScraperFieldsShow.Controls.Add(Me.tblScraperFieldsShow)
        Me.gbScraperFieldsShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFieldsShow.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperFieldsShow.Name = "gbScraperFieldsShow"
        Me.gbScraperFieldsShow.Size = New System.Drawing.Size(189, 182)
        Me.gbScraperFieldsShow.TabIndex = 0
        Me.gbScraperFieldsShow.TabStop = False
        Me.gbScraperFieldsShow.Text = "Show"
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsBottom)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(525, 415)
        Me.pnlSettings.TabIndex = 1
        '
        'frmSettingsHolder_TV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(525, 415)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder_TV"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        CType(Me.pbIconBottom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbScraperFieldsEpisode.ResumeLayout(False)
        Me.gbScraperFieldsEpisode.PerformLayout()
        Me.tblScraperFieldsEpisode.ResumeLayout(False)
        Me.tblScraperFieldsEpisode.PerformLayout()
        Me.gbScraperFieldsSeason.ResumeLayout(False)
        Me.gbScraperFieldsSeason.PerformLayout()
        Me.tblScraperFieldsSeason.ResumeLayout(False)
        Me.tblScraperFieldsSeason.PerformLayout()
        Me.pnlSettingsBottom.ResumeLayout(False)
        Me.pnlSettingsBottom.PerformLayout()
        Me.tblSettingsBottom.ResumeLayout(False)
        Me.tblSettingsBottom.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.gbScraperOpts.ResumeLayout(False)
        Me.gbScraperOpts.PerformLayout()
        Me.tblScraperOpts.ResumeLayout(False)
        Me.tblScraperOpts.PerformLayout()
        CType(Me.pbApiPinInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblScraperFieldsShow.ResumeLayout(False)
        Me.tblScraperFieldsShow.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.gbScraperFieldsOpts.ResumeLayout(False)
        Me.gbScraperFieldsOpts.PerformLayout()
        Me.tblScraperFieldsOpts.ResumeLayout(False)
        Me.tblScraperFieldsOpts.PerformLayout()
        Me.gbScraperFieldsShow.ResumeLayout(False)
        Me.gbScraperFieldsShow.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkScraperShowCertifications As CheckBox
    Friend WithEvents chkScraperShowCountries As CheckBox
    Friend WithEvents chkScraperShowCreators As CheckBox
    Friend WithEvents chkScraperShowGenres As CheckBox
    Friend WithEvents pbIconBottom As PictureBox
    Friend WithEvents gbScraperFieldsEpisode As GroupBox
    Friend WithEvents tblScraperFieldsEpisode As TableLayoutPanel
    Friend WithEvents chkScraperEpisodeAired As CheckBox
    Friend WithEvents chkScraperEpisodeActors As CheckBox
    Friend WithEvents chkScraperEpisodeRating As CheckBox
    Friend WithEvents chkScraperEpisodeCredits As CheckBox
    Friend WithEvents chkScraperEpisodePlot As CheckBox
    Friend WithEvents chkScraperEpisodeTitle As CheckBox
    Friend WithEvents chkScraperEpisodeGuestStars As CheckBox
    Friend WithEvents chkScraperEpisodeDirectors As CheckBox
    Friend WithEvents gbScraperFieldsSeason As GroupBox
    Friend WithEvents tblScraperFieldsSeason As TableLayoutPanel
    Friend WithEvents chkScraperSeasonAired As CheckBox
    Friend WithEvents chkScraperSeasonPlot As CheckBox
    Friend WithEvents chkScraperSeasonTitle As CheckBox
    Friend WithEvents pnlSettingsBottom As Panel
    Friend WithEvents tblSettingsBottom As TableLayoutPanel
    Friend WithEvents lblInfoBottom As Label
    Friend WithEvents chkScraperShowActors As CheckBox
    Friend WithEvents chkScraperShowPlot As CheckBox
    Friend WithEvents tblSettingsTop As TableLayoutPanel
    Friend WithEvents btnDown As Button
    Friend WithEvents lblScraperOrder As Label
    Friend WithEvents btnUp As Button
    Friend WithEvents chkEnabled As CheckBox
    Friend WithEvents gbScraperOpts As GroupBox
    Friend WithEvents tblScraperOpts As TableLayoutPanel
    Friend WithEvents pbApiPinInfo As PictureBox
    Friend WithEvents txtApiPin As TextBox
    Friend WithEvents chkFallBackEng As CheckBox
    Friend WithEvents pnlSettingsTop As Panel
    Friend WithEvents tblScraperFieldsShow As TableLayoutPanel
    Friend WithEvents chkScraperShowOriginalTitle As CheckBox
    Friend WithEvents chkScraperShowRuntime As CheckBox
    Friend WithEvents chkScraperShowRating As CheckBox
    Friend WithEvents chkScraperShowPremiered As CheckBox
    Friend WithEvents chkScraperShowStatus As CheckBox
    Friend WithEvents chkScraperShowStudios As CheckBox
    Friend WithEvents chkScraperShowTitle As CheckBox
    Friend WithEvents chkScraperShowTagline As CheckBox
    Friend WithEvents pnlSettingsMain As Panel
    Friend WithEvents tblSettingsMain As TableLayoutPanel
    Friend WithEvents gbScraperFieldsOpts As GroupBox
    Friend WithEvents tblScraperFieldsOpts As TableLayoutPanel
    Friend WithEvents gbScraperFieldsShow As GroupBox
    Friend WithEvents pnlSettings As Panel
    Friend WithEvents lblApiPin As Label
End Class
