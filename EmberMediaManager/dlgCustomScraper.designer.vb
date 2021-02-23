<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgCustomScraper
    Inherits System.Windows.Forms.Form

#Region "Fields"

    Friend WithEvents chkMainModifierAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsCertifications As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsDirectors As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierExtrathumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsGenres As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierMetaData As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsStudios As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsTop250 As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsCountries As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsWriters As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsYear As System.Windows.Forms.CheckBox
    Friend WithEvents gbMainScrapeOptions As System.Windows.Forms.GroupBox
    Friend WithEvents gbMainScrapeModifiers As System.Windows.Forms.GroupBox
    Friend WithEvents gbScrapeType_Filter As System.Windows.Forms.GroupBox
    Friend WithEvents gbScrapeType_Mode As System.Windows.Forms.GroupBox
    Friend WithEvents lblTopDescription As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents rbScrapeType_All As System.Windows.Forms.RadioButton
    Friend WithEvents rbScrapeType_Marked As System.Windows.Forms.RadioButton
    Friend WithEvents rbScrapeType_Missing As System.Windows.Forms.RadioButton
    Friend WithEvents rbScrapeType_New As System.Windows.Forms.RadioButton
    Friend WithEvents rbScrapeType_Ask As System.Windows.Forms.RadioButton
    Friend WithEvents rbScrapeType_Auto As System.Windows.Forms.RadioButton
    Friend WithEvents btnOK As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

#End Region 'Fields

#Region "Methods"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCustomScraper))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.lblTopDescription = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.rbScrapeType_All = New System.Windows.Forms.RadioButton()
        Me.gbScrapeType_Filter = New System.Windows.Forms.GroupBox()
        Me.tblScrapeType_Filter = New System.Windows.Forms.TableLayoutPanel()
        Me.rbScrapeType_Marked = New System.Windows.Forms.RadioButton()
        Me.rbScrapeType_New = New System.Windows.Forms.RadioButton()
        Me.rbScrapeType_Missing = New System.Windows.Forms.RadioButton()
        Me.rbScrapeType_Selected = New System.Windows.Forms.RadioButton()
        Me.rbScrapeType_Filter = New System.Windows.Forms.RadioButton()
        Me.gbScrapeType_Mode = New System.Windows.Forms.GroupBox()
        Me.tblScrapeType_Mode = New System.Windows.Forms.TableLayoutPanel()
        Me.rbScrapeType_Auto = New System.Windows.Forms.RadioButton()
        Me.rbScrapeType_Skip = New System.Windows.Forms.RadioButton()
        Me.rbScrapeType_Ask = New System.Windows.Forms.RadioButton()
        Me.gbMainScrapeModifiers = New System.Windows.Forms.GroupBox()
        Me.tblMainScrapeModifiers = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMainModifierTrailer = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierTheme = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierLandscape = New System.Windows.Forms.CheckBox()
        Me.btnMainScrapeModifierNone = New System.Windows.Forms.Button()
        Me.chkMainModifierPoster = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierMetaData = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierNFO = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierExtrafanarts = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierExtrathumbs = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierFanart = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierClearArt = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierBanner = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierActorThumbs = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierAll = New System.Windows.Forms.CheckBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.gbMainScrapeOptions = New System.Windows.Forms.GroupBox()
        Me.tblMainScrapeOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMainOptionsAll = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.btnMainScrapeOptionsNone = New System.Windows.Forms.Button()
        Me.chkMainOptionsCountries = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsCollectionID = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsActors = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsCertifications = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsCreators = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsPlot = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsDirectors = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsOutline = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsEpisodeGuideURL = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsGenres = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsMPAA = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsRating = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsPremiered = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsRuntime = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsStatus = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsStudios = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsTagline = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsTitle = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsTop250 = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsTrailer = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsWriters = New System.Windows.Forms.CheckBox()
        Me.chkMainOptionsYear = New System.Windows.Forms.CheckBox()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbSeasonScrapeModifiers = New System.Windows.Forms.GroupBox()
        Me.tblSeasonScrapeModifiers = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSeasonScrapeModifierNone = New System.Windows.Forms.Button()
        Me.chkSeasonModifierAll = New System.Windows.Forms.CheckBox()
        Me.chkSeasonModifierBanner = New System.Windows.Forms.CheckBox()
        Me.chkSeasonModifierFanart = New System.Windows.Forms.CheckBox()
        Me.chkSeasonModifierLandscape = New System.Windows.Forms.CheckBox()
        Me.chkSeasonModifierPoster = New System.Windows.Forms.CheckBox()
        Me.gbSpecialScrapeModifiers = New System.Windows.Forms.GroupBox()
        Me.tblSpecialScrapeModifiers = New System.Windows.Forms.TableLayoutPanel()
        Me.chkSpecialModifierAll = New System.Windows.Forms.CheckBox()
        Me.btnSpecialScrapeModifierNone = New System.Windows.Forms.Button()
        Me.chkSpecialModifierWithSeasons = New System.Windows.Forms.CheckBox()
        Me.chkSpecialModifierWithEpisodes = New System.Windows.Forms.CheckBox()
        Me.gbEpisodeScrapeModifiers = New System.Windows.Forms.GroupBox()
        Me.tblEpisodeScrapeModifiers = New System.Windows.Forms.TableLayoutPanel()
        Me.btnEpisodeScrapeModifierNone = New System.Windows.Forms.Button()
        Me.chkEpisodeModifierAll = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeModifierPoster = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeModifierNFO = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeModifierFanart = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeModifierActorThumbs = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeModifierMetaData = New System.Windows.Forms.CheckBox()
        Me.gbEpisodeScrapeOptions = New System.Windows.Forms.GroupBox()
        Me.tblEpisodeScrapeOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEpisodeOptionsAll = New System.Windows.Forms.CheckBox()
        Me.btnEpisodeScrapeOptionsNone = New System.Windows.Forms.Button()
        Me.chkEpisodeOptionsActors = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeOptionsAired = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeOptionsDirectors = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeOptionsGuestStars = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeOptionsPlot = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeOptionsRating = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeOptionsRuntime = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeOptionsTitle = New System.Windows.Forms.CheckBox()
        Me.chkEpisodeOptionsWriters = New System.Windows.Forms.CheckBox()
        Me.gbSeasonScrapeOptions = New System.Windows.Forms.GroupBox()
        Me.tblSeasonScrapeOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.chkSeasonOptionsAll = New System.Windows.Forms.CheckBox()
        Me.btnSeasonScrapeOptionsNone = New System.Windows.Forms.Button()
        Me.chkSeasonOptionsAired = New System.Windows.Forms.CheckBox()
        Me.chkSeasonOptionsPlot = New System.Windows.Forms.CheckBox()
        Me.chkSeasonOptionsTitle = New System.Windows.Forms.CheckBox()
        Me.chkMainModifierKeyart = New System.Windows.Forms.CheckBox()
        Me.pnlTop.SuspendLayout()
        Me.tblTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbScrapeType_Filter.SuspendLayout()
        Me.tblScrapeType_Filter.SuspendLayout()
        Me.gbScrapeType_Mode.SuspendLayout()
        Me.tblScrapeType_Mode.SuspendLayout()
        Me.gbMainScrapeModifiers.SuspendLayout()
        Me.tblMainScrapeModifiers.SuspendLayout()
        Me.gbMainScrapeOptions.SuspendLayout()
        Me.tblMainScrapeOptions.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        Me.gbSeasonScrapeModifiers.SuspendLayout()
        Me.tblSeasonScrapeModifiers.SuspendLayout()
        Me.gbSpecialScrapeModifiers.SuspendLayout()
        Me.tblSpecialScrapeModifiers.SuspendLayout()
        Me.gbEpisodeScrapeModifiers.SuspendLayout()
        Me.tblEpisodeScrapeModifiers.SuspendLayout()
        Me.gbEpisodeScrapeOptions.SuspendLayout()
        Me.tblEpisodeScrapeOptions.SuspendLayout()
        Me.gbSeasonScrapeOptions.SuspendLayout()
        Me.tblSeasonScrapeOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(893, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'pnlTop
        '
        Me.pnlTop.AutoSize = True
        Me.pnlTop.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.tblTop)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(976, 66)
        Me.pnlTop.TabIndex = 2
        '
        'tblTop
        '
        Me.tblTop.AutoSize = True
        Me.tblTop.ColumnCount = 3
        Me.tblTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblTop.Controls.Add(Me.pbTopLogo, 0, 0)
        Me.tblTop.Controls.Add(Me.lblTopDescription, 1, 1)
        Me.tblTop.Controls.Add(Me.lblTopTitle, 1, 0)
        Me.tblTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTop.Location = New System.Drawing.Point(0, 0)
        Me.tblTop.Name = "tblTop"
        Me.tblTop.RowCount = 2
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTop.Size = New System.Drawing.Size(974, 64)
        Me.tblTop.TabIndex = 2
        '
        'pbTopLogo
        '
        Me.pbTopLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.Location = New System.Drawing.Point(3, 3)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.pbTopLogo.Padding = New System.Windows.Forms.Padding(5)
        Me.tblTop.SetRowSpan(Me.pbTopLogo, 2)
        Me.pbTopLogo.Size = New System.Drawing.Size(58, 58)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'lblTopDescription
        '
        Me.lblTopDescription.AutoSize = True
        Me.lblTopDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDescription.ForeColor = System.Drawing.Color.White
        Me.lblTopDescription.Location = New System.Drawing.Point(67, 32)
        Me.lblTopDescription.Name = "lblTopDescription"
        Me.lblTopDescription.Padding = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.lblTopDescription.Size = New System.Drawing.Size(136, 13)
        Me.lblTopDescription.TabIndex = 1
        Me.lblTopDescription.Text = "Create a custom scraper"
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(67, 0)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(195, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Custom Scraper"
        '
        'rbScrapeType_All
        '
        Me.rbScrapeType_All.AutoSize = True
        Me.rbScrapeType_All.Checked = True
        Me.rbScrapeType_All.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_All.Location = New System.Drawing.Point(3, 3)
        Me.rbScrapeType_All.Name = "rbScrapeType_All"
        Me.rbScrapeType_All.Size = New System.Drawing.Size(38, 17)
        Me.rbScrapeType_All.TabIndex = 0
        Me.rbScrapeType_All.TabStop = True
        Me.rbScrapeType_All.Text = "All"
        Me.rbScrapeType_All.UseVisualStyleBackColor = True
        '
        'gbScrapeType_Filter
        '
        Me.gbScrapeType_Filter.AutoSize = True
        Me.gbScrapeType_Filter.Controls.Add(Me.tblScrapeType_Filter)
        Me.gbScrapeType_Filter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScrapeType_Filter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScrapeType_Filter.Location = New System.Drawing.Point(3, 3)
        Me.gbScrapeType_Filter.Name = "gbScrapeType_Filter"
        Me.gbScrapeType_Filter.Size = New System.Drawing.Size(231, 90)
        Me.gbScrapeType_Filter.TabIndex = 0
        Me.gbScrapeType_Filter.TabStop = False
        Me.gbScrapeType_Filter.Text = "Selection Filter"
        '
        'tblScrapeType_Filter
        '
        Me.tblScrapeType_Filter.AutoSize = True
        Me.tblScrapeType_Filter.ColumnCount = 3
        Me.tblScrapeType_Filter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScrapeType_Filter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScrapeType_Filter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScrapeType_Filter.Controls.Add(Me.rbScrapeType_All, 0, 0)
        Me.tblScrapeType_Filter.Controls.Add(Me.rbScrapeType_Marked, 1, 1)
        Me.tblScrapeType_Filter.Controls.Add(Me.rbScrapeType_New, 0, 1)
        Me.tblScrapeType_Filter.Controls.Add(Me.rbScrapeType_Missing, 1, 0)
        Me.tblScrapeType_Filter.Controls.Add(Me.rbScrapeType_Selected, 1, 2)
        Me.tblScrapeType_Filter.Controls.Add(Me.rbScrapeType_Filter, 0, 2)
        Me.tblScrapeType_Filter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScrapeType_Filter.Location = New System.Drawing.Point(3, 18)
        Me.tblScrapeType_Filter.Name = "tblScrapeType_Filter"
        Me.tblScrapeType_Filter.RowCount = 3
        Me.tblScrapeType_Filter.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScrapeType_Filter.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScrapeType_Filter.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScrapeType_Filter.Size = New System.Drawing.Size(225, 69)
        Me.tblScrapeType_Filter.TabIndex = 4
        '
        'rbScrapeType_Marked
        '
        Me.rbScrapeType_Marked.AutoSize = True
        Me.rbScrapeType_Marked.Enabled = False
        Me.rbScrapeType_Marked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_Marked.Location = New System.Drawing.Point(102, 26)
        Me.rbScrapeType_Marked.Name = "rbScrapeType_Marked"
        Me.rbScrapeType_Marked.Size = New System.Drawing.Size(64, 17)
        Me.rbScrapeType_Marked.TabIndex = 3
        Me.rbScrapeType_Marked.Text = "Marked"
        Me.rbScrapeType_Marked.UseVisualStyleBackColor = True
        '
        'rbScrapeType_New
        '
        Me.rbScrapeType_New.AutoSize = True
        Me.rbScrapeType_New.Enabled = False
        Me.rbScrapeType_New.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_New.Location = New System.Drawing.Point(3, 26)
        Me.rbScrapeType_New.Name = "rbScrapeType_New"
        Me.rbScrapeType_New.Size = New System.Drawing.Size(48, 17)
        Me.rbScrapeType_New.TabIndex = 2
        Me.rbScrapeType_New.Text = "New"
        Me.rbScrapeType_New.UseVisualStyleBackColor = True
        '
        'rbScrapeType_Missing
        '
        Me.rbScrapeType_Missing.AutoSize = True
        Me.rbScrapeType_Missing.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_Missing.Location = New System.Drawing.Point(102, 3)
        Me.rbScrapeType_Missing.Name = "rbScrapeType_Missing"
        Me.rbScrapeType_Missing.Size = New System.Drawing.Size(95, 17)
        Me.rbScrapeType_Missing.TabIndex = 1
        Me.rbScrapeType_Missing.Text = "Missing Items"
        Me.rbScrapeType_Missing.UseVisualStyleBackColor = True
        '
        'rbScrapeType_Selected
        '
        Me.rbScrapeType_Selected.AutoSize = True
        Me.tblScrapeType_Filter.SetColumnSpan(Me.rbScrapeType_Selected, 2)
        Me.rbScrapeType_Selected.Enabled = False
        Me.rbScrapeType_Selected.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_Selected.Location = New System.Drawing.Point(102, 49)
        Me.rbScrapeType_Selected.Name = "rbScrapeType_Selected"
        Me.rbScrapeType_Selected.Size = New System.Drawing.Size(68, 17)
        Me.rbScrapeType_Selected.TabIndex = 5
        Me.rbScrapeType_Selected.Text = "Selected"
        Me.rbScrapeType_Selected.UseVisualStyleBackColor = True
        '
        'rbScrapeType_Filter
        '
        Me.rbScrapeType_Filter.AutoSize = True
        Me.rbScrapeType_Filter.Enabled = False
        Me.rbScrapeType_Filter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_Filter.Location = New System.Drawing.Point(3, 49)
        Me.rbScrapeType_Filter.Name = "rbScrapeType_Filter"
        Me.rbScrapeType_Filter.Size = New System.Drawing.Size(93, 17)
        Me.rbScrapeType_Filter.TabIndex = 4
        Me.rbScrapeType_Filter.Text = "Current Filter"
        Me.rbScrapeType_Filter.UseVisualStyleBackColor = True
        '
        'gbScrapeType_Mode
        '
        Me.gbScrapeType_Mode.AutoSize = True
        Me.gbScrapeType_Mode.Controls.Add(Me.tblScrapeType_Mode)
        Me.gbScrapeType_Mode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScrapeType_Mode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbScrapeType_Mode.Location = New System.Drawing.Point(240, 3)
        Me.gbScrapeType_Mode.Name = "gbScrapeType_Mode"
        Me.gbScrapeType_Mode.Size = New System.Drawing.Size(263, 90)
        Me.gbScrapeType_Mode.TabIndex = 1
        Me.gbScrapeType_Mode.TabStop = False
        Me.gbScrapeType_Mode.Text = "Update Mode"
        '
        'tblScrapeType_Mode
        '
        Me.tblScrapeType_Mode.AutoSize = True
        Me.tblScrapeType_Mode.ColumnCount = 2
        Me.tblScrapeType_Mode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScrapeType_Mode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScrapeType_Mode.Controls.Add(Me.rbScrapeType_Auto, 0, 0)
        Me.tblScrapeType_Mode.Controls.Add(Me.rbScrapeType_Skip, 0, 2)
        Me.tblScrapeType_Mode.Controls.Add(Me.rbScrapeType_Ask, 0, 1)
        Me.tblScrapeType_Mode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScrapeType_Mode.Location = New System.Drawing.Point(3, 18)
        Me.tblScrapeType_Mode.Name = "tblScrapeType_Mode"
        Me.tblScrapeType_Mode.RowCount = 4
        Me.tblScrapeType_Mode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScrapeType_Mode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScrapeType_Mode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScrapeType_Mode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScrapeType_Mode.Size = New System.Drawing.Size(257, 69)
        Me.tblScrapeType_Mode.TabIndex = 3
        '
        'rbScrapeType_Auto
        '
        Me.rbScrapeType_Auto.AutoSize = True
        Me.rbScrapeType_Auto.Checked = True
        Me.rbScrapeType_Auto.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_Auto.Location = New System.Drawing.Point(3, 3)
        Me.rbScrapeType_Auto.Name = "rbScrapeType_Auto"
        Me.rbScrapeType_Auto.Size = New System.Drawing.Size(173, 17)
        Me.rbScrapeType_Auto.TabIndex = 0
        Me.rbScrapeType_Auto.TabStop = True
        Me.rbScrapeType_Auto.Text = "Automatic (Force Best Match)"
        Me.rbScrapeType_Auto.UseVisualStyleBackColor = True
        '
        'rbScrapeType_Skip
        '
        Me.rbScrapeType_Skip.AutoSize = True
        Me.rbScrapeType_Skip.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_Skip.Location = New System.Drawing.Point(3, 49)
        Me.rbScrapeType_Skip.Name = "rbScrapeType_Skip"
        Me.rbScrapeType_Skip.Size = New System.Drawing.Size(207, 17)
        Me.rbScrapeType_Skip.TabIndex = 2
        Me.rbScrapeType_Skip.Text = "Skip (Skip If More Than One Match)"
        Me.rbScrapeType_Skip.UseVisualStyleBackColor = True
        '
        'rbScrapeType_Ask
        '
        Me.rbScrapeType_Ask.AutoSize = True
        Me.rbScrapeType_Ask.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbScrapeType_Ask.Location = New System.Drawing.Point(3, 26)
        Me.rbScrapeType_Ask.Name = "rbScrapeType_Ask"
        Me.rbScrapeType_Ask.Size = New System.Drawing.Size(215, 17)
        Me.rbScrapeType_Ask.TabIndex = 1
        Me.rbScrapeType_Ask.Text = "Ask (Require Input If No Exact Match)"
        Me.rbScrapeType_Ask.UseVisualStyleBackColor = True
        '
        'gbMainScrapeModifiers
        '
        Me.gbMainScrapeModifiers.AutoSize = True
        Me.gbMainScrapeModifiers.Controls.Add(Me.tblMainScrapeModifiers)
        Me.gbMainScrapeModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMainScrapeModifiers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMainScrapeModifiers.Location = New System.Drawing.Point(3, 99)
        Me.gbMainScrapeModifiers.Name = "gbMainScrapeModifiers"
        Me.tblMain.SetRowSpan(Me.gbMainScrapeModifiers, 3)
        Me.gbMainScrapeModifiers.Size = New System.Drawing.Size(231, 267)
        Me.gbMainScrapeModifiers.TabIndex = 2
        Me.gbMainScrapeModifiers.TabStop = False
        Me.gbMainScrapeModifiers.Text = "Main Modifiers"
        '
        'tblMainScrapeModifiers
        '
        Me.tblMainScrapeModifiers.AutoSize = True
        Me.tblMainScrapeModifiers.ColumnCount = 3
        Me.tblMainScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainScrapeModifiers.Controls.Add(Me.btnMainScrapeModifierNone, 1, 0)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierDiscArt, 0, 6)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierExtrafanarts, 0, 7)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierExtrathumbs, 0, 8)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierFanart, 1, 1)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierClearLogo, 0, 5)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierClearArt, 0, 4)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierCharacterArt, 0, 3)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierBanner, 0, 2)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierActorThumbs, 0, 1)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierAll, 0, 0)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierTrailer, 1, 8)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierTheme, 1, 7)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierPoster, 1, 6)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierNFO, 1, 5)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierMetaData, 1, 4)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierLandscape, 1, 3)
        Me.tblMainScrapeModifiers.Controls.Add(Me.chkMainModifierKeyart, 1, 2)
        Me.tblMainScrapeModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMainScrapeModifiers.Location = New System.Drawing.Point(3, 18)
        Me.tblMainScrapeModifiers.Name = "tblMainScrapeModifiers"
        Me.tblMainScrapeModifiers.RowCount = 10
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeModifiers.Size = New System.Drawing.Size(225, 246)
        Me.tblMainScrapeModifiers.TabIndex = 15
        '
        'chkMainModifierTrailer
        '
        Me.chkMainModifierTrailer.AutoSize = True
        Me.chkMainModifierTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierTrailer.Location = New System.Drawing.Point(106, 193)
        Me.chkMainModifierTrailer.Name = "chkMainModifierTrailer"
        Me.chkMainModifierTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkMainModifierTrailer.TabIndex = 5
        Me.chkMainModifierTrailer.Text = "Trailer"
        Me.chkMainModifierTrailer.UseVisualStyleBackColor = True
        '
        'chkMainModifierTheme
        '
        Me.chkMainModifierTheme.AutoSize = True
        Me.chkMainModifierTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierTheme.Location = New System.Drawing.Point(106, 170)
        Me.chkMainModifierTheme.Name = "chkMainModifierTheme"
        Me.chkMainModifierTheme.Size = New System.Drawing.Size(60, 17)
        Me.chkMainModifierTheme.TabIndex = 8
        Me.chkMainModifierTheme.Text = "Theme"
        Me.chkMainModifierTheme.UseVisualStyleBackColor = True
        '
        'chkMainModifierLandscape
        '
        Me.chkMainModifierLandscape.AutoSize = True
        Me.chkMainModifierLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierLandscape.Location = New System.Drawing.Point(106, 78)
        Me.chkMainModifierLandscape.Name = "chkMainModifierLandscape"
        Me.chkMainModifierLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkMainModifierLandscape.TabIndex = 13
        Me.chkMainModifierLandscape.Text = "Landscape"
        Me.chkMainModifierLandscape.UseVisualStyleBackColor = True
        '
        'btnMainScrapeModifierNone
        '
        Me.btnMainScrapeModifierNone.Location = New System.Drawing.Point(106, 3)
        Me.btnMainScrapeModifierNone.Name = "btnMainScrapeModifierNone"
        Me.btnMainScrapeModifierNone.Size = New System.Drawing.Size(116, 23)
        Me.btnMainScrapeModifierNone.TabIndex = 14
        Me.btnMainScrapeModifierNone.Text = "Select None"
        Me.btnMainScrapeModifierNone.UseVisualStyleBackColor = True
        '
        'chkMainModifierPoster
        '
        Me.chkMainModifierPoster.AutoSize = True
        Me.chkMainModifierPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierPoster.Location = New System.Drawing.Point(106, 147)
        Me.chkMainModifierPoster.Name = "chkMainModifierPoster"
        Me.chkMainModifierPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkMainModifierPoster.TabIndex = 2
        Me.chkMainModifierPoster.Text = "Poster"
        Me.chkMainModifierPoster.UseVisualStyleBackColor = True
        '
        'chkMainModifierMetaData
        '
        Me.chkMainModifierMetaData.AutoSize = True
        Me.chkMainModifierMetaData.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierMetaData.Location = New System.Drawing.Point(106, 101)
        Me.chkMainModifierMetaData.Name = "chkMainModifierMetaData"
        Me.chkMainModifierMetaData.Size = New System.Drawing.Size(79, 17)
        Me.chkMainModifierMetaData.TabIndex = 3
        Me.chkMainModifierMetaData.Text = "Meta Data"
        Me.chkMainModifierMetaData.UseVisualStyleBackColor = True
        '
        'chkMainModifierNFO
        '
        Me.chkMainModifierNFO.AutoSize = True
        Me.chkMainModifierNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierNFO.Location = New System.Drawing.Point(106, 124)
        Me.chkMainModifierNFO.Name = "chkMainModifierNFO"
        Me.chkMainModifierNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkMainModifierNFO.TabIndex = 1
        Me.chkMainModifierNFO.Text = "NFO"
        Me.chkMainModifierNFO.UseVisualStyleBackColor = True
        '
        'chkMainModifierDiscArt
        '
        Me.chkMainModifierDiscArt.AutoSize = True
        Me.chkMainModifierDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierDiscArt.Location = New System.Drawing.Point(3, 147)
        Me.chkMainModifierDiscArt.Name = "chkMainModifierDiscArt"
        Me.chkMainModifierDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkMainModifierDiscArt.TabIndex = 9
        Me.chkMainModifierDiscArt.Text = "DiscArt"
        Me.chkMainModifierDiscArt.UseVisualStyleBackColor = True
        '
        'chkMainModifierExtrafanarts
        '
        Me.chkMainModifierExtrafanarts.AutoSize = True
        Me.chkMainModifierExtrafanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierExtrafanarts.Location = New System.Drawing.Point(3, 170)
        Me.chkMainModifierExtrafanarts.Name = "chkMainModifierExtrafanarts"
        Me.chkMainModifierExtrafanarts.Size = New System.Drawing.Size(87, 17)
        Me.chkMainModifierExtrafanarts.TabIndex = 6
        Me.chkMainModifierExtrafanarts.Text = "Extrafanarts"
        Me.chkMainModifierExtrafanarts.UseVisualStyleBackColor = True
        '
        'chkMainModifierExtrathumbs
        '
        Me.chkMainModifierExtrathumbs.AutoSize = True
        Me.chkMainModifierExtrathumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierExtrathumbs.Location = New System.Drawing.Point(3, 193)
        Me.chkMainModifierExtrathumbs.Name = "chkMainModifierExtrathumbs"
        Me.chkMainModifierExtrathumbs.Size = New System.Drawing.Size(90, 17)
        Me.chkMainModifierExtrathumbs.TabIndex = 4
        Me.chkMainModifierExtrathumbs.Text = "Extrathumbs"
        Me.chkMainModifierExtrathumbs.UseVisualStyleBackColor = True
        '
        'chkMainModifierFanart
        '
        Me.chkMainModifierFanart.AutoSize = True
        Me.chkMainModifierFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierFanart.Location = New System.Drawing.Point(106, 32)
        Me.chkMainModifierFanart.Name = "chkMainModifierFanart"
        Me.chkMainModifierFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkMainModifierFanart.TabIndex = 2
        Me.chkMainModifierFanart.Text = "Fanart"
        Me.chkMainModifierFanart.UseVisualStyleBackColor = True
        '
        'chkMainModifierClearLogo
        '
        Me.chkMainModifierClearLogo.AutoSize = True
        Me.chkMainModifierClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierClearLogo.Location = New System.Drawing.Point(3, 124)
        Me.chkMainModifierClearLogo.Name = "chkMainModifierClearLogo"
        Me.chkMainModifierClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkMainModifierClearLogo.TabIndex = 10
        Me.chkMainModifierClearLogo.Text = "ClearLogo"
        Me.chkMainModifierClearLogo.UseVisualStyleBackColor = True
        '
        'chkMainModifierClearArt
        '
        Me.chkMainModifierClearArt.AutoSize = True
        Me.chkMainModifierClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierClearArt.Location = New System.Drawing.Point(3, 101)
        Me.chkMainModifierClearArt.Name = "chkMainModifierClearArt"
        Me.chkMainModifierClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkMainModifierClearArt.TabIndex = 11
        Me.chkMainModifierClearArt.Text = "ClearArt"
        Me.chkMainModifierClearArt.UseVisualStyleBackColor = True
        '
        'chkMainModifierCharacterArt
        '
        Me.chkMainModifierCharacterArt.AutoSize = True
        Me.chkMainModifierCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierCharacterArt.Location = New System.Drawing.Point(3, 78)
        Me.chkMainModifierCharacterArt.Name = "chkMainModifierCharacterArt"
        Me.chkMainModifierCharacterArt.Size = New System.Drawing.Size(90, 17)
        Me.chkMainModifierCharacterArt.TabIndex = 11
        Me.chkMainModifierCharacterArt.Text = "CharacterArt"
        Me.chkMainModifierCharacterArt.UseVisualStyleBackColor = True
        '
        'chkMainModifierBanner
        '
        Me.chkMainModifierBanner.AutoSize = True
        Me.chkMainModifierBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierBanner.Location = New System.Drawing.Point(3, 55)
        Me.chkMainModifierBanner.Name = "chkMainModifierBanner"
        Me.chkMainModifierBanner.Size = New System.Drawing.Size(62, 17)
        Me.chkMainModifierBanner.TabIndex = 12
        Me.chkMainModifierBanner.Text = "Banner"
        Me.chkMainModifierBanner.UseVisualStyleBackColor = True
        '
        'chkMainModifierActorThumbs
        '
        Me.chkMainModifierActorThumbs.AutoSize = True
        Me.chkMainModifierActorThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierActorThumbs.Location = New System.Drawing.Point(3, 32)
        Me.chkMainModifierActorThumbs.Name = "chkMainModifierActorThumbs"
        Me.chkMainModifierActorThumbs.Size = New System.Drawing.Size(97, 17)
        Me.chkMainModifierActorThumbs.TabIndex = 7
        Me.chkMainModifierActorThumbs.Text = "Actor Thumbs"
        Me.chkMainModifierActorThumbs.UseVisualStyleBackColor = True
        '
        'chkMainModifierAll
        '
        Me.chkMainModifierAll.AutoSize = True
        Me.chkMainModifierAll.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierAll.Location = New System.Drawing.Point(3, 3)
        Me.chkMainModifierAll.Name = "chkMainModifierAll"
        Me.chkMainModifierAll.Size = New System.Drawing.Size(69, 17)
        Me.chkMainModifierAll.TabIndex = 0
        Me.chkMainModifierAll.Text = "All Items"
        Me.chkMainModifierAll.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOK.Enabled = False
        Me.btnOK.Location = New System.Drawing.Point(807, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(80, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "Begin"
        '
        'gbMainScrapeOptions
        '
        Me.gbMainScrapeOptions.AutoSize = True
        Me.gbMainScrapeOptions.Controls.Add(Me.tblMainScrapeOptions)
        Me.gbMainScrapeOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMainScrapeOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMainScrapeOptions.Location = New System.Drawing.Point(240, 99)
        Me.gbMainScrapeOptions.Name = "gbMainScrapeOptions"
        Me.tblMain.SetRowSpan(Me.gbMainScrapeOptions, 4)
        Me.gbMainScrapeOptions.Size = New System.Drawing.Size(263, 326)
        Me.gbMainScrapeOptions.TabIndex = 3
        Me.gbMainScrapeOptions.TabStop = False
        Me.gbMainScrapeOptions.Text = "Main Options"
        '
        'tblMainScrapeOptions
        '
        Me.tblMainScrapeOptions.AutoSize = True
        Me.tblMainScrapeOptions.ColumnCount = 3
        Me.tblMainScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsAll, 0, 0)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsOriginalTitle, 0, 10)
        Me.tblMainScrapeOptions.Controls.Add(Me.btnMainScrapeOptionsNone, 1, 0)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsCountries, 0, 4)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsCollectionID, 0, 3)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsActors, 0, 1)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsCertifications, 0, 2)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsCreators, 0, 5)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsPlot, 0, 12)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsDirectors, 0, 6)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsOutline, 0, 11)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsEpisodeGuideURL, 0, 7)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsGenres, 0, 8)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsMPAA, 0, 9)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsRating, 1, 2)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsPremiered, 1, 1)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsRuntime, 1, 3)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsStatus, 1, 4)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsStudios, 1, 5)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsTagline, 1, 6)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsTitle, 1, 7)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsTop250, 1, 8)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsTrailer, 1, 9)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsWriters, 1, 10)
        Me.tblMainScrapeOptions.Controls.Add(Me.chkMainOptionsYear, 1, 11)
        Me.tblMainScrapeOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMainScrapeOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblMainScrapeOptions.Name = "tblMainScrapeOptions"
        Me.tblMainScrapeOptions.RowCount = 14
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMainScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMainScrapeOptions.Size = New System.Drawing.Size(257, 305)
        Me.tblMainScrapeOptions.TabIndex = 25
        '
        'chkMainOptionsAll
        '
        Me.chkMainOptionsAll.AutoSize = True
        Me.chkMainOptionsAll.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkMainOptionsAll.Location = New System.Drawing.Point(3, 3)
        Me.chkMainOptionsAll.Name = "chkMainOptionsAll"
        Me.chkMainOptionsAll.Size = New System.Drawing.Size(69, 17)
        Me.chkMainOptionsAll.TabIndex = 22
        Me.chkMainOptionsAll.Text = "All Items"
        Me.chkMainOptionsAll.UseVisualStyleBackColor = True
        '
        'chkMainOptionsOriginalTitle
        '
        Me.chkMainOptionsOriginalTitle.AutoSize = True
        Me.chkMainOptionsOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsOriginalTitle.Location = New System.Drawing.Point(3, 239)
        Me.chkMainOptionsOriginalTitle.Name = "chkMainOptionsOriginalTitle"
        Me.chkMainOptionsOriginalTitle.Size = New System.Drawing.Size(93, 17)
        Me.chkMainOptionsOriginalTitle.TabIndex = 24
        Me.chkMainOptionsOriginalTitle.Text = "Original Title"
        Me.chkMainOptionsOriginalTitle.UseVisualStyleBackColor = True
        '
        'btnMainScrapeOptionsNone
        '
        Me.btnMainScrapeOptionsNone.Location = New System.Drawing.Point(133, 3)
        Me.btnMainScrapeOptionsNone.Name = "btnMainScrapeOptionsNone"
        Me.btnMainScrapeOptionsNone.Size = New System.Drawing.Size(121, 23)
        Me.btnMainScrapeOptionsNone.TabIndex = 15
        Me.btnMainScrapeOptionsNone.Text = "Select None"
        Me.btnMainScrapeOptionsNone.UseVisualStyleBackColor = True
        '
        'chkMainOptionsCountries
        '
        Me.chkMainOptionsCountries.AutoSize = True
        Me.chkMainOptionsCountries.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsCountries.Location = New System.Drawing.Point(3, 101)
        Me.chkMainOptionsCountries.Name = "chkMainOptionsCountries"
        Me.chkMainOptionsCountries.Size = New System.Drawing.Size(76, 17)
        Me.chkMainOptionsCountries.TabIndex = 21
        Me.chkMainOptionsCountries.Text = "Countries"
        Me.chkMainOptionsCountries.UseVisualStyleBackColor = True
        '
        'chkMainOptionsCollectionID
        '
        Me.chkMainOptionsCollectionID.AutoSize = True
        Me.chkMainOptionsCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsCollectionID.Location = New System.Drawing.Point(3, 78)
        Me.chkMainOptionsCollectionID.Name = "chkMainOptionsCollectionID"
        Me.chkMainOptionsCollectionID.Size = New System.Drawing.Size(92, 17)
        Me.chkMainOptionsCollectionID.TabIndex = 23
        Me.chkMainOptionsCollectionID.Text = "Collection ID"
        Me.chkMainOptionsCollectionID.UseVisualStyleBackColor = True
        '
        'chkMainOptionsActors
        '
        Me.chkMainOptionsActors.AutoSize = True
        Me.chkMainOptionsActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsActors.Location = New System.Drawing.Point(3, 32)
        Me.chkMainOptionsActors.Name = "chkMainOptionsActors"
        Me.chkMainOptionsActors.Size = New System.Drawing.Size(58, 17)
        Me.chkMainOptionsActors.TabIndex = 14
        Me.chkMainOptionsActors.Text = "Actors"
        Me.chkMainOptionsActors.UseVisualStyleBackColor = True
        '
        'chkMainOptionsCertifications
        '
        Me.chkMainOptionsCertifications.AutoSize = True
        Me.chkMainOptionsCertifications.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsCertifications.Location = New System.Drawing.Point(3, 55)
        Me.chkMainOptionsCertifications.Name = "chkMainOptionsCertifications"
        Me.chkMainOptionsCertifications.Size = New System.Drawing.Size(94, 17)
        Me.chkMainOptionsCertifications.TabIndex = 3
        Me.chkMainOptionsCertifications.Text = "Certifications"
        Me.chkMainOptionsCertifications.UseVisualStyleBackColor = True
        '
        'chkMainOptionsCreators
        '
        Me.chkMainOptionsCreators.AutoSize = True
        Me.chkMainOptionsCreators.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsCreators.Location = New System.Drawing.Point(3, 124)
        Me.chkMainOptionsCreators.Name = "chkMainOptionsCreators"
        Me.chkMainOptionsCreators.Size = New System.Drawing.Size(69, 17)
        Me.chkMainOptionsCreators.TabIndex = 21
        Me.chkMainOptionsCreators.Text = "Creators"
        Me.chkMainOptionsCreators.UseVisualStyleBackColor = True
        '
        'chkMainOptionsPlot
        '
        Me.chkMainOptionsPlot.AutoSize = True
        Me.chkMainOptionsPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsPlot.Location = New System.Drawing.Point(3, 285)
        Me.chkMainOptionsPlot.Name = "chkMainOptionsPlot"
        Me.chkMainOptionsPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkMainOptionsPlot.TabIndex = 13
        Me.chkMainOptionsPlot.Text = "Plot"
        Me.chkMainOptionsPlot.UseVisualStyleBackColor = True
        '
        'chkMainOptionsDirectors
        '
        Me.chkMainOptionsDirectors.AutoSize = True
        Me.chkMainOptionsDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsDirectors.Location = New System.Drawing.Point(3, 147)
        Me.chkMainOptionsDirectors.Name = "chkMainOptionsDirectors"
        Me.chkMainOptionsDirectors.Size = New System.Drawing.Size(72, 17)
        Me.chkMainOptionsDirectors.TabIndex = 15
        Me.chkMainOptionsDirectors.Text = "Directors"
        Me.chkMainOptionsDirectors.UseVisualStyleBackColor = True
        '
        'chkMainOptionsOutline
        '
        Me.chkMainOptionsOutline.AutoSize = True
        Me.chkMainOptionsOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsOutline.Location = New System.Drawing.Point(3, 262)
        Me.chkMainOptionsOutline.Name = "chkMainOptionsOutline"
        Me.chkMainOptionsOutline.Size = New System.Drawing.Size(65, 17)
        Me.chkMainOptionsOutline.TabIndex = 12
        Me.chkMainOptionsOutline.Text = "Outline"
        Me.chkMainOptionsOutline.UseVisualStyleBackColor = True
        '
        'chkMainOptionsEpisodeGuideURL
        '
        Me.chkMainOptionsEpisodeGuideURL.AutoSize = True
        Me.chkMainOptionsEpisodeGuideURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsEpisodeGuideURL.Location = New System.Drawing.Point(3, 170)
        Me.chkMainOptionsEpisodeGuideURL.Name = "chkMainOptionsEpisodeGuideURL"
        Me.chkMainOptionsEpisodeGuideURL.Size = New System.Drawing.Size(124, 17)
        Me.chkMainOptionsEpisodeGuideURL.TabIndex = 15
        Me.chkMainOptionsEpisodeGuideURL.Text = "Episode Guide URL"
        Me.chkMainOptionsEpisodeGuideURL.UseVisualStyleBackColor = True
        '
        'chkMainOptionsGenres
        '
        Me.chkMainOptionsGenres.AutoSize = True
        Me.chkMainOptionsGenres.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsGenres.Location = New System.Drawing.Point(3, 193)
        Me.chkMainOptionsGenres.Name = "chkMainOptionsGenres"
        Me.chkMainOptionsGenres.Size = New System.Drawing.Size(62, 17)
        Me.chkMainOptionsGenres.TabIndex = 9
        Me.chkMainOptionsGenres.Text = "Genres"
        Me.chkMainOptionsGenres.UseVisualStyleBackColor = True
        '
        'chkMainOptionsMPAA
        '
        Me.chkMainOptionsMPAA.AutoSize = True
        Me.chkMainOptionsMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsMPAA.Location = New System.Drawing.Point(3, 216)
        Me.chkMainOptionsMPAA.Name = "chkMainOptionsMPAA"
        Me.chkMainOptionsMPAA.Size = New System.Drawing.Size(55, 17)
        Me.chkMainOptionsMPAA.TabIndex = 2
        Me.chkMainOptionsMPAA.Text = "MPAA"
        Me.chkMainOptionsMPAA.UseVisualStyleBackColor = True
        '
        'chkMainOptionsRating
        '
        Me.chkMainOptionsRating.AutoSize = True
        Me.chkMainOptionsRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsRating.Location = New System.Drawing.Point(133, 55)
        Me.chkMainOptionsRating.Name = "chkMainOptionsRating"
        Me.chkMainOptionsRating.Size = New System.Drawing.Size(60, 17)
        Me.chkMainOptionsRating.TabIndex = 6
        Me.chkMainOptionsRating.Text = "Rating"
        Me.chkMainOptionsRating.UseVisualStyleBackColor = True
        '
        'chkMainOptionsPremiered
        '
        Me.chkMainOptionsPremiered.AutoSize = True
        Me.chkMainOptionsPremiered.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsPremiered.Location = New System.Drawing.Point(133, 32)
        Me.chkMainOptionsPremiered.Name = "chkMainOptionsPremiered"
        Me.chkMainOptionsPremiered.Size = New System.Drawing.Size(77, 17)
        Me.chkMainOptionsPremiered.TabIndex = 13
        Me.chkMainOptionsPremiered.Text = "Premiered"
        Me.chkMainOptionsPremiered.UseVisualStyleBackColor = True
        '
        'chkMainOptionsRuntime
        '
        Me.chkMainOptionsRuntime.AutoSize = True
        Me.chkMainOptionsRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsRuntime.Location = New System.Drawing.Point(133, 78)
        Me.chkMainOptionsRuntime.Name = "chkMainOptionsRuntime"
        Me.chkMainOptionsRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkMainOptionsRuntime.TabIndex = 5
        Me.chkMainOptionsRuntime.Text = "Runtime"
        Me.chkMainOptionsRuntime.UseVisualStyleBackColor = True
        '
        'chkMainOptionsStatus
        '
        Me.chkMainOptionsStatus.AutoSize = True
        Me.chkMainOptionsStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsStatus.Location = New System.Drawing.Point(133, 101)
        Me.chkMainOptionsStatus.Name = "chkMainOptionsStatus"
        Me.chkMainOptionsStatus.Size = New System.Drawing.Size(58, 17)
        Me.chkMainOptionsStatus.TabIndex = 8
        Me.chkMainOptionsStatus.Text = "Status"
        Me.chkMainOptionsStatus.UseVisualStyleBackColor = True
        '
        'chkMainOptionsStudios
        '
        Me.chkMainOptionsStudios.AutoSize = True
        Me.chkMainOptionsStudios.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsStudios.Location = New System.Drawing.Point(133, 124)
        Me.chkMainOptionsStudios.Name = "chkMainOptionsStudios"
        Me.chkMainOptionsStudios.Size = New System.Drawing.Size(65, 17)
        Me.chkMainOptionsStudios.TabIndex = 8
        Me.chkMainOptionsStudios.Text = "Studios"
        Me.chkMainOptionsStudios.UseVisualStyleBackColor = True
        '
        'chkMainOptionsTagline
        '
        Me.chkMainOptionsTagline.AutoSize = True
        Me.chkMainOptionsTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsTagline.Location = New System.Drawing.Point(133, 147)
        Me.chkMainOptionsTagline.Name = "chkMainOptionsTagline"
        Me.chkMainOptionsTagline.Size = New System.Drawing.Size(63, 17)
        Me.chkMainOptionsTagline.TabIndex = 11
        Me.chkMainOptionsTagline.Text = "Tagline"
        Me.chkMainOptionsTagline.UseVisualStyleBackColor = True
        '
        'chkMainOptionsTitle
        '
        Me.chkMainOptionsTitle.AutoSize = True
        Me.chkMainOptionsTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsTitle.Location = New System.Drawing.Point(133, 170)
        Me.chkMainOptionsTitle.Name = "chkMainOptionsTitle"
        Me.chkMainOptionsTitle.Size = New System.Drawing.Size(48, 17)
        Me.chkMainOptionsTitle.TabIndex = 0
        Me.chkMainOptionsTitle.Text = "Title"
        Me.chkMainOptionsTitle.UseVisualStyleBackColor = True
        '
        'chkMainOptionsTop250
        '
        Me.chkMainOptionsTop250.AutoSize = True
        Me.chkMainOptionsTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsTop250.Location = New System.Drawing.Point(133, 193)
        Me.chkMainOptionsTop250.Name = "chkMainOptionsTop250"
        Me.chkMainOptionsTop250.Size = New System.Drawing.Size(66, 17)
        Me.chkMainOptionsTop250.TabIndex = 20
        Me.chkMainOptionsTop250.Text = "Top 250"
        Me.chkMainOptionsTop250.UseVisualStyleBackColor = True
        '
        'chkMainOptionsTrailer
        '
        Me.chkMainOptionsTrailer.AutoSize = True
        Me.chkMainOptionsTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsTrailer.Location = New System.Drawing.Point(133, 216)
        Me.chkMainOptionsTrailer.Name = "chkMainOptionsTrailer"
        Me.chkMainOptionsTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkMainOptionsTrailer.TabIndex = 10
        Me.chkMainOptionsTrailer.Text = "Trailer"
        Me.chkMainOptionsTrailer.UseVisualStyleBackColor = True
        '
        'chkMainOptionsWriters
        '
        Me.chkMainOptionsWriters.AutoSize = True
        Me.chkMainOptionsWriters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsWriters.Location = New System.Drawing.Point(133, 239)
        Me.chkMainOptionsWriters.Name = "chkMainOptionsWriters"
        Me.chkMainOptionsWriters.Size = New System.Drawing.Size(108, 17)
        Me.chkMainOptionsWriters.TabIndex = 16
        Me.chkMainOptionsWriters.Text = "Credits (Writers)"
        Me.chkMainOptionsWriters.UseVisualStyleBackColor = True
        '
        'chkMainOptionsYear
        '
        Me.chkMainOptionsYear.AutoSize = True
        Me.chkMainOptionsYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainOptionsYear.Location = New System.Drawing.Point(133, 262)
        Me.chkMainOptionsYear.Name = "chkMainOptionsYear"
        Me.chkMainOptionsYear.Size = New System.Drawing.Size(46, 17)
        Me.chkMainOptionsYear.TabIndex = 1
        Me.chkMainOptionsYear.Text = "Year"
        Me.chkMainOptionsYear.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 548)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(976, 29)
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
        Me.tblBottom.Size = New System.Drawing.Size(976, 29)
        Me.tblBottom.TabIndex = 0
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 66)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(976, 482)
        Me.pnlMain.TabIndex = 5
        '
        'tblMain
        '
        Me.tblMain.AutoScroll = True
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 5
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.gbMainScrapeOptions, 1, 1)
        Me.tblMain.Controls.Add(Me.gbScrapeType_Filter, 0, 0)
        Me.tblMain.Controls.Add(Me.gbScrapeType_Mode, 1, 0)
        Me.tblMain.Controls.Add(Me.gbMainScrapeModifiers, 0, 1)
        Me.tblMain.Controls.Add(Me.gbSeasonScrapeModifiers, 2, 1)
        Me.tblMain.Controls.Add(Me.gbSpecialScrapeModifiers, 0, 4)
        Me.tblMain.Controls.Add(Me.gbEpisodeScrapeModifiers, 2, 2)
        Me.tblMain.Controls.Add(Me.gbEpisodeScrapeOptions, 3, 2)
        Me.tblMain.Controls.Add(Me.gbSeasonScrapeOptions, 3, 1)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 7
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(976, 482)
        Me.tblMain.TabIndex = 0
        '
        'gbSeasonScrapeModifiers
        '
        Me.gbSeasonScrapeModifiers.AutoSize = True
        Me.gbSeasonScrapeModifiers.Controls.Add(Me.tblSeasonScrapeModifiers)
        Me.gbSeasonScrapeModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSeasonScrapeModifiers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSeasonScrapeModifiers.Location = New System.Drawing.Point(509, 99)
        Me.gbSeasonScrapeModifiers.Name = "gbSeasonScrapeModifiers"
        Me.gbSeasonScrapeModifiers.Size = New System.Drawing.Size(231, 96)
        Me.gbSeasonScrapeModifiers.TabIndex = 2
        Me.gbSeasonScrapeModifiers.TabStop = False
        Me.gbSeasonScrapeModifiers.Text = "Seasons Modifiers"
        '
        'tblSeasonScrapeModifiers
        '
        Me.tblSeasonScrapeModifiers.AutoSize = True
        Me.tblSeasonScrapeModifiers.ColumnCount = 3
        Me.tblSeasonScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSeasonScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSeasonScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSeasonScrapeModifiers.Controls.Add(Me.btnSeasonScrapeModifierNone, 1, 0)
        Me.tblSeasonScrapeModifiers.Controls.Add(Me.chkSeasonModifierAll, 0, 0)
        Me.tblSeasonScrapeModifiers.Controls.Add(Me.chkSeasonModifierBanner, 0, 1)
        Me.tblSeasonScrapeModifiers.Controls.Add(Me.chkSeasonModifierFanart, 0, 2)
        Me.tblSeasonScrapeModifiers.Controls.Add(Me.chkSeasonModifierLandscape, 1, 1)
        Me.tblSeasonScrapeModifiers.Controls.Add(Me.chkSeasonModifierPoster, 1, 2)
        Me.tblSeasonScrapeModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSeasonScrapeModifiers.Location = New System.Drawing.Point(3, 18)
        Me.tblSeasonScrapeModifiers.Name = "tblSeasonScrapeModifiers"
        Me.tblSeasonScrapeModifiers.RowCount = 4
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeModifiers.Size = New System.Drawing.Size(225, 75)
        Me.tblSeasonScrapeModifiers.TabIndex = 15
        '
        'btnSeasonScrapeModifierNone
        '
        Me.btnSeasonScrapeModifierNone.Location = New System.Drawing.Point(78, 3)
        Me.btnSeasonScrapeModifierNone.Name = "btnSeasonScrapeModifierNone"
        Me.btnSeasonScrapeModifierNone.Size = New System.Drawing.Size(116, 23)
        Me.btnSeasonScrapeModifierNone.TabIndex = 14
        Me.btnSeasonScrapeModifierNone.Text = "Select None"
        Me.btnSeasonScrapeModifierNone.UseVisualStyleBackColor = True
        '
        'chkSeasonModifierAll
        '
        Me.chkSeasonModifierAll.AutoSize = True
        Me.chkSeasonModifierAll.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSeasonModifierAll.Location = New System.Drawing.Point(3, 3)
        Me.chkSeasonModifierAll.Name = "chkSeasonModifierAll"
        Me.chkSeasonModifierAll.Size = New System.Drawing.Size(69, 17)
        Me.chkSeasonModifierAll.TabIndex = 0
        Me.chkSeasonModifierAll.Text = "All Items"
        Me.chkSeasonModifierAll.UseVisualStyleBackColor = True
        '
        'chkSeasonModifierBanner
        '
        Me.chkSeasonModifierBanner.AutoSize = True
        Me.chkSeasonModifierBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSeasonModifierBanner.Location = New System.Drawing.Point(3, 32)
        Me.chkSeasonModifierBanner.Name = "chkSeasonModifierBanner"
        Me.chkSeasonModifierBanner.Size = New System.Drawing.Size(62, 17)
        Me.chkSeasonModifierBanner.TabIndex = 12
        Me.chkSeasonModifierBanner.Text = "Banner"
        Me.chkSeasonModifierBanner.UseVisualStyleBackColor = True
        '
        'chkSeasonModifierFanart
        '
        Me.chkSeasonModifierFanart.AutoSize = True
        Me.chkSeasonModifierFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSeasonModifierFanart.Location = New System.Drawing.Point(3, 55)
        Me.chkSeasonModifierFanart.Name = "chkSeasonModifierFanart"
        Me.chkSeasonModifierFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkSeasonModifierFanart.TabIndex = 2
        Me.chkSeasonModifierFanart.Text = "Fanart"
        Me.chkSeasonModifierFanart.UseVisualStyleBackColor = True
        '
        'chkSeasonModifierLandscape
        '
        Me.chkSeasonModifierLandscape.AutoSize = True
        Me.chkSeasonModifierLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSeasonModifierLandscape.Location = New System.Drawing.Point(78, 32)
        Me.chkSeasonModifierLandscape.Name = "chkSeasonModifierLandscape"
        Me.chkSeasonModifierLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkSeasonModifierLandscape.TabIndex = 13
        Me.chkSeasonModifierLandscape.Text = "Landscape"
        Me.chkSeasonModifierLandscape.UseVisualStyleBackColor = True
        '
        'chkSeasonModifierPoster
        '
        Me.chkSeasonModifierPoster.AutoSize = True
        Me.chkSeasonModifierPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSeasonModifierPoster.Location = New System.Drawing.Point(78, 55)
        Me.chkSeasonModifierPoster.Name = "chkSeasonModifierPoster"
        Me.chkSeasonModifierPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkSeasonModifierPoster.TabIndex = 2
        Me.chkSeasonModifierPoster.Text = "Poster"
        Me.chkSeasonModifierPoster.UseVisualStyleBackColor = True
        '
        'gbSpecialScrapeModifiers
        '
        Me.gbSpecialScrapeModifiers.AutoSize = True
        Me.gbSpecialScrapeModifiers.Controls.Add(Me.tblSpecialScrapeModifiers)
        Me.gbSpecialScrapeModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSpecialScrapeModifiers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSpecialScrapeModifiers.Location = New System.Drawing.Point(3, 372)
        Me.gbSpecialScrapeModifiers.Name = "gbSpecialScrapeModifiers"
        Me.tblMain.SetRowSpan(Me.gbSpecialScrapeModifiers, 2)
        Me.gbSpecialScrapeModifiers.Size = New System.Drawing.Size(231, 73)
        Me.gbSpecialScrapeModifiers.TabIndex = 4
        Me.gbSpecialScrapeModifiers.TabStop = False
        Me.gbSpecialScrapeModifiers.Text = "Special Modifiers"
        '
        'tblSpecialScrapeModifiers
        '
        Me.tblSpecialScrapeModifiers.AutoSize = True
        Me.tblSpecialScrapeModifiers.ColumnCount = 3
        Me.tblSpecialScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSpecialScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSpecialScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSpecialScrapeModifiers.Controls.Add(Me.chkSpecialModifierAll, 0, 0)
        Me.tblSpecialScrapeModifiers.Controls.Add(Me.btnSpecialScrapeModifierNone, 1, 0)
        Me.tblSpecialScrapeModifiers.Controls.Add(Me.chkSpecialModifierWithSeasons, 0, 1)
        Me.tblSpecialScrapeModifiers.Controls.Add(Me.chkSpecialModifierWithEpisodes, 1, 1)
        Me.tblSpecialScrapeModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSpecialScrapeModifiers.Location = New System.Drawing.Point(3, 18)
        Me.tblSpecialScrapeModifiers.Name = "tblSpecialScrapeModifiers"
        Me.tblSpecialScrapeModifiers.RowCount = 3
        Me.tblSpecialScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSpecialScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSpecialScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSpecialScrapeModifiers.Size = New System.Drawing.Size(225, 52)
        Me.tblSpecialScrapeModifiers.TabIndex = 0
        '
        'chkSpecialModifierAll
        '
        Me.chkSpecialModifierAll.AutoSize = True
        Me.chkSpecialModifierAll.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSpecialModifierAll.Location = New System.Drawing.Point(3, 3)
        Me.chkSpecialModifierAll.Name = "chkSpecialModifierAll"
        Me.chkSpecialModifierAll.Size = New System.Drawing.Size(69, 17)
        Me.chkSpecialModifierAll.TabIndex = 0
        Me.chkSpecialModifierAll.Text = "All Items"
        Me.chkSpecialModifierAll.UseVisualStyleBackColor = True
        '
        'btnSpecialScrapeModifierNone
        '
        Me.btnSpecialScrapeModifierNone.Location = New System.Drawing.Point(103, 3)
        Me.btnSpecialScrapeModifierNone.Name = "btnSpecialScrapeModifierNone"
        Me.btnSpecialScrapeModifierNone.Size = New System.Drawing.Size(116, 23)
        Me.btnSpecialScrapeModifierNone.TabIndex = 14
        Me.btnSpecialScrapeModifierNone.Text = "Select None"
        Me.btnSpecialScrapeModifierNone.UseVisualStyleBackColor = True
        '
        'chkSpecialModifierWithSeasons
        '
        Me.chkSpecialModifierWithSeasons.AutoSize = True
        Me.chkSpecialModifierWithSeasons.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSpecialModifierWithSeasons.Location = New System.Drawing.Point(3, 32)
        Me.chkSpecialModifierWithSeasons.Name = "chkSpecialModifierWithSeasons"
        Me.chkSpecialModifierWithSeasons.Size = New System.Drawing.Size(94, 17)
        Me.chkSpecialModifierWithSeasons.TabIndex = 7
        Me.chkSpecialModifierWithSeasons.Text = "with Seasons"
        Me.chkSpecialModifierWithSeasons.UseVisualStyleBackColor = True
        '
        'chkSpecialModifierWithEpisodes
        '
        Me.chkSpecialModifierWithEpisodes.AutoSize = True
        Me.chkSpecialModifierWithEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSpecialModifierWithEpisodes.Location = New System.Drawing.Point(103, 32)
        Me.chkSpecialModifierWithEpisodes.Name = "chkSpecialModifierWithEpisodes"
        Me.chkSpecialModifierWithEpisodes.Size = New System.Drawing.Size(98, 17)
        Me.chkSpecialModifierWithEpisodes.TabIndex = 2
        Me.chkSpecialModifierWithEpisodes.Text = "with Episodes"
        Me.chkSpecialModifierWithEpisodes.UseVisualStyleBackColor = True
        '
        'gbEpisodeScrapeModifiers
        '
        Me.gbEpisodeScrapeModifiers.AutoSize = True
        Me.gbEpisodeScrapeModifiers.Controls.Add(Me.tblEpisodeScrapeModifiers)
        Me.gbEpisodeScrapeModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbEpisodeScrapeModifiers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbEpisodeScrapeModifiers.Location = New System.Drawing.Point(509, 201)
        Me.gbEpisodeScrapeModifiers.Name = "gbEpisodeScrapeModifiers"
        Me.gbEpisodeScrapeModifiers.Size = New System.Drawing.Size(231, 119)
        Me.gbEpisodeScrapeModifiers.TabIndex = 2
        Me.gbEpisodeScrapeModifiers.TabStop = False
        Me.gbEpisodeScrapeModifiers.Text = "Episodes Modifiers"
        '
        'tblEpisodeScrapeModifiers
        '
        Me.tblEpisodeScrapeModifiers.AutoSize = True
        Me.tblEpisodeScrapeModifiers.ColumnCount = 3
        Me.tblEpisodeScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeScrapeModifiers.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeScrapeModifiers.Controls.Add(Me.btnEpisodeScrapeModifierNone, 1, 0)
        Me.tblEpisodeScrapeModifiers.Controls.Add(Me.chkEpisodeModifierAll, 0, 0)
        Me.tblEpisodeScrapeModifiers.Controls.Add(Me.chkEpisodeModifierPoster, 1, 2)
        Me.tblEpisodeScrapeModifiers.Controls.Add(Me.chkEpisodeModifierNFO, 1, 1)
        Me.tblEpisodeScrapeModifiers.Controls.Add(Me.chkEpisodeModifierFanart, 0, 2)
        Me.tblEpisodeScrapeModifiers.Controls.Add(Me.chkEpisodeModifierActorThumbs, 0, 1)
        Me.tblEpisodeScrapeModifiers.Controls.Add(Me.chkEpisodeModifierMetaData, 0, 3)
        Me.tblEpisodeScrapeModifiers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblEpisodeScrapeModifiers.Location = New System.Drawing.Point(3, 18)
        Me.tblEpisodeScrapeModifiers.Name = "tblEpisodeScrapeModifiers"
        Me.tblEpisodeScrapeModifiers.RowCount = 5
        Me.tblEpisodeScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeModifiers.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeModifiers.Size = New System.Drawing.Size(225, 98)
        Me.tblEpisodeScrapeModifiers.TabIndex = 15
        '
        'btnEpisodeScrapeModifierNone
        '
        Me.btnEpisodeScrapeModifierNone.Location = New System.Drawing.Point(106, 3)
        Me.btnEpisodeScrapeModifierNone.Name = "btnEpisodeScrapeModifierNone"
        Me.btnEpisodeScrapeModifierNone.Size = New System.Drawing.Size(116, 23)
        Me.btnEpisodeScrapeModifierNone.TabIndex = 14
        Me.btnEpisodeScrapeModifierNone.Text = "Select None"
        Me.btnEpisodeScrapeModifierNone.UseVisualStyleBackColor = True
        '
        'chkEpisodeModifierAll
        '
        Me.chkEpisodeModifierAll.AutoSize = True
        Me.chkEpisodeModifierAll.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeModifierAll.Location = New System.Drawing.Point(3, 3)
        Me.chkEpisodeModifierAll.Name = "chkEpisodeModifierAll"
        Me.chkEpisodeModifierAll.Size = New System.Drawing.Size(69, 17)
        Me.chkEpisodeModifierAll.TabIndex = 0
        Me.chkEpisodeModifierAll.Text = "All Items"
        Me.chkEpisodeModifierAll.UseVisualStyleBackColor = True
        '
        'chkEpisodeModifierPoster
        '
        Me.chkEpisodeModifierPoster.AutoSize = True
        Me.chkEpisodeModifierPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeModifierPoster.Location = New System.Drawing.Point(106, 55)
        Me.chkEpisodeModifierPoster.Name = "chkEpisodeModifierPoster"
        Me.chkEpisodeModifierPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkEpisodeModifierPoster.TabIndex = 2
        Me.chkEpisodeModifierPoster.Text = "Poster"
        Me.chkEpisodeModifierPoster.UseVisualStyleBackColor = True
        '
        'chkEpisodeModifierNFO
        '
        Me.chkEpisodeModifierNFO.AutoSize = True
        Me.chkEpisodeModifierNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeModifierNFO.Location = New System.Drawing.Point(106, 32)
        Me.chkEpisodeModifierNFO.Name = "chkEpisodeModifierNFO"
        Me.chkEpisodeModifierNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkEpisodeModifierNFO.TabIndex = 2
        Me.chkEpisodeModifierNFO.Text = "NFO"
        Me.chkEpisodeModifierNFO.UseVisualStyleBackColor = True
        '
        'chkEpisodeModifierFanart
        '
        Me.chkEpisodeModifierFanart.AutoSize = True
        Me.chkEpisodeModifierFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeModifierFanart.Location = New System.Drawing.Point(3, 55)
        Me.chkEpisodeModifierFanart.Name = "chkEpisodeModifierFanart"
        Me.chkEpisodeModifierFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkEpisodeModifierFanart.TabIndex = 2
        Me.chkEpisodeModifierFanart.Text = "Fanart"
        Me.chkEpisodeModifierFanart.UseVisualStyleBackColor = True
        '
        'chkEpisodeModifierActorThumbs
        '
        Me.chkEpisodeModifierActorThumbs.AutoSize = True
        Me.chkEpisodeModifierActorThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeModifierActorThumbs.Location = New System.Drawing.Point(3, 32)
        Me.chkEpisodeModifierActorThumbs.Name = "chkEpisodeModifierActorThumbs"
        Me.chkEpisodeModifierActorThumbs.Size = New System.Drawing.Size(97, 17)
        Me.chkEpisodeModifierActorThumbs.TabIndex = 7
        Me.chkEpisodeModifierActorThumbs.Text = "Actor Thumbs"
        Me.chkEpisodeModifierActorThumbs.UseVisualStyleBackColor = True
        '
        'chkEpisodeModifierMetaData
        '
        Me.chkEpisodeModifierMetaData.AutoSize = True
        Me.chkEpisodeModifierMetaData.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeModifierMetaData.Location = New System.Drawing.Point(3, 78)
        Me.chkEpisodeModifierMetaData.Name = "chkEpisodeModifierMetaData"
        Me.chkEpisodeModifierMetaData.Size = New System.Drawing.Size(79, 17)
        Me.chkEpisodeModifierMetaData.TabIndex = 2
        Me.chkEpisodeModifierMetaData.Text = "Meta Data"
        Me.chkEpisodeModifierMetaData.UseVisualStyleBackColor = True
        '
        'gbEpisodeScrapeOptions
        '
        Me.gbEpisodeScrapeOptions.AutoSize = True
        Me.gbEpisodeScrapeOptions.Controls.Add(Me.tblEpisodeScrapeOptions)
        Me.gbEpisodeScrapeOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbEpisodeScrapeOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbEpisodeScrapeOptions.Location = New System.Drawing.Point(746, 201)
        Me.gbEpisodeScrapeOptions.Name = "gbEpisodeScrapeOptions"
        Me.tblMain.SetRowSpan(Me.gbEpisodeScrapeOptions, 2)
        Me.gbEpisodeScrapeOptions.Size = New System.Drawing.Size(223, 165)
        Me.gbEpisodeScrapeOptions.TabIndex = 3
        Me.gbEpisodeScrapeOptions.TabStop = False
        Me.gbEpisodeScrapeOptions.Text = "Episodes Options"
        '
        'tblEpisodeScrapeOptions
        '
        Me.tblEpisodeScrapeOptions.AutoSize = True
        Me.tblEpisodeScrapeOptions.ColumnCount = 3
        Me.tblEpisodeScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsAll, 0, 0)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.btnEpisodeScrapeOptionsNone, 1, 0)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsActors, 0, 1)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsAired, 0, 2)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsDirectors, 0, 3)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsGuestStars, 0, 4)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsPlot, 0, 5)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsRating, 1, 1)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsRuntime, 1, 2)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsTitle, 1, 3)
        Me.tblEpisodeScrapeOptions.Controls.Add(Me.chkEpisodeOptionsWriters, 1, 4)
        Me.tblEpisodeScrapeOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblEpisodeScrapeOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblEpisodeScrapeOptions.Name = "tblEpisodeScrapeOptions"
        Me.tblEpisodeScrapeOptions.RowCount = 7
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblEpisodeScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblEpisodeScrapeOptions.Size = New System.Drawing.Size(217, 144)
        Me.tblEpisodeScrapeOptions.TabIndex = 25
        '
        'chkEpisodeOptionsAll
        '
        Me.chkEpisodeOptionsAll.AutoSize = True
        Me.chkEpisodeOptionsAll.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkEpisodeOptionsAll.Location = New System.Drawing.Point(3, 3)
        Me.chkEpisodeOptionsAll.Name = "chkEpisodeOptionsAll"
        Me.chkEpisodeOptionsAll.Size = New System.Drawing.Size(69, 17)
        Me.chkEpisodeOptionsAll.TabIndex = 22
        Me.chkEpisodeOptionsAll.Text = "All Items"
        Me.chkEpisodeOptionsAll.UseVisualStyleBackColor = True
        '
        'btnEpisodeScrapeOptionsNone
        '
        Me.btnEpisodeScrapeOptionsNone.Location = New System.Drawing.Point(93, 3)
        Me.btnEpisodeScrapeOptionsNone.Name = "btnEpisodeScrapeOptionsNone"
        Me.btnEpisodeScrapeOptionsNone.Size = New System.Drawing.Size(121, 23)
        Me.btnEpisodeScrapeOptionsNone.TabIndex = 15
        Me.btnEpisodeScrapeOptionsNone.Text = "Select None"
        Me.btnEpisodeScrapeOptionsNone.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsActors
        '
        Me.chkEpisodeOptionsActors.AutoSize = True
        Me.chkEpisodeOptionsActors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsActors.Location = New System.Drawing.Point(3, 32)
        Me.chkEpisodeOptionsActors.Name = "chkEpisodeOptionsActors"
        Me.chkEpisodeOptionsActors.Size = New System.Drawing.Size(58, 17)
        Me.chkEpisodeOptionsActors.TabIndex = 14
        Me.chkEpisodeOptionsActors.Text = "Actors"
        Me.chkEpisodeOptionsActors.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsAired
        '
        Me.chkEpisodeOptionsAired.AutoSize = True
        Me.chkEpisodeOptionsAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsAired.Location = New System.Drawing.Point(3, 55)
        Me.chkEpisodeOptionsAired.Name = "chkEpisodeOptionsAired"
        Me.chkEpisodeOptionsAired.Size = New System.Drawing.Size(53, 17)
        Me.chkEpisodeOptionsAired.TabIndex = 3
        Me.chkEpisodeOptionsAired.Text = "Aired"
        Me.chkEpisodeOptionsAired.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsDirectors
        '
        Me.chkEpisodeOptionsDirectors.AutoSize = True
        Me.chkEpisodeOptionsDirectors.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsDirectors.Location = New System.Drawing.Point(3, 78)
        Me.chkEpisodeOptionsDirectors.Name = "chkEpisodeOptionsDirectors"
        Me.chkEpisodeOptionsDirectors.Size = New System.Drawing.Size(72, 17)
        Me.chkEpisodeOptionsDirectors.TabIndex = 15
        Me.chkEpisodeOptionsDirectors.Text = "Directors"
        Me.chkEpisodeOptionsDirectors.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsGuestStars
        '
        Me.chkEpisodeOptionsGuestStars.AutoSize = True
        Me.chkEpisodeOptionsGuestStars.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsGuestStars.Location = New System.Drawing.Point(3, 101)
        Me.chkEpisodeOptionsGuestStars.Name = "chkEpisodeOptionsGuestStars"
        Me.chkEpisodeOptionsGuestStars.Size = New System.Drawing.Size(84, 17)
        Me.chkEpisodeOptionsGuestStars.TabIndex = 21
        Me.chkEpisodeOptionsGuestStars.Text = "Guest Stars"
        Me.chkEpisodeOptionsGuestStars.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsPlot
        '
        Me.chkEpisodeOptionsPlot.AutoSize = True
        Me.chkEpisodeOptionsPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsPlot.Location = New System.Drawing.Point(3, 124)
        Me.chkEpisodeOptionsPlot.Name = "chkEpisodeOptionsPlot"
        Me.chkEpisodeOptionsPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkEpisodeOptionsPlot.TabIndex = 13
        Me.chkEpisodeOptionsPlot.Text = "Plot"
        Me.chkEpisodeOptionsPlot.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsRating
        '
        Me.chkEpisodeOptionsRating.AutoSize = True
        Me.chkEpisodeOptionsRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsRating.Location = New System.Drawing.Point(93, 32)
        Me.chkEpisodeOptionsRating.Name = "chkEpisodeOptionsRating"
        Me.chkEpisodeOptionsRating.Size = New System.Drawing.Size(60, 17)
        Me.chkEpisodeOptionsRating.TabIndex = 6
        Me.chkEpisodeOptionsRating.Text = "Rating"
        Me.chkEpisodeOptionsRating.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsRuntime
        '
        Me.chkEpisodeOptionsRuntime.AutoSize = True
        Me.chkEpisodeOptionsRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsRuntime.Location = New System.Drawing.Point(93, 55)
        Me.chkEpisodeOptionsRuntime.Name = "chkEpisodeOptionsRuntime"
        Me.chkEpisodeOptionsRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkEpisodeOptionsRuntime.TabIndex = 5
        Me.chkEpisodeOptionsRuntime.Text = "Runtime"
        Me.chkEpisodeOptionsRuntime.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsTitle
        '
        Me.chkEpisodeOptionsTitle.AutoSize = True
        Me.chkEpisodeOptionsTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsTitle.Location = New System.Drawing.Point(93, 78)
        Me.chkEpisodeOptionsTitle.Name = "chkEpisodeOptionsTitle"
        Me.chkEpisodeOptionsTitle.Size = New System.Drawing.Size(48, 17)
        Me.chkEpisodeOptionsTitle.TabIndex = 0
        Me.chkEpisodeOptionsTitle.Text = "Title"
        Me.chkEpisodeOptionsTitle.UseVisualStyleBackColor = True
        '
        'chkEpisodeOptionsWriters
        '
        Me.chkEpisodeOptionsWriters.AutoSize = True
        Me.chkEpisodeOptionsWriters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkEpisodeOptionsWriters.Location = New System.Drawing.Point(93, 101)
        Me.chkEpisodeOptionsWriters.Name = "chkEpisodeOptionsWriters"
        Me.chkEpisodeOptionsWriters.Size = New System.Drawing.Size(108, 17)
        Me.chkEpisodeOptionsWriters.TabIndex = 16
        Me.chkEpisodeOptionsWriters.Text = "Credits (Writers)"
        Me.chkEpisodeOptionsWriters.UseVisualStyleBackColor = True
        '
        'gbSeasonScrapeOptions
        '
        Me.gbSeasonScrapeOptions.AutoSize = True
        Me.gbSeasonScrapeOptions.Controls.Add(Me.tblSeasonScrapeOptions)
        Me.gbSeasonScrapeOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSeasonScrapeOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSeasonScrapeOptions.Location = New System.Drawing.Point(746, 99)
        Me.gbSeasonScrapeOptions.Name = "gbSeasonScrapeOptions"
        Me.gbSeasonScrapeOptions.Size = New System.Drawing.Size(223, 96)
        Me.gbSeasonScrapeOptions.TabIndex = 3
        Me.gbSeasonScrapeOptions.TabStop = False
        Me.gbSeasonScrapeOptions.Text = "Seasons Options"
        '
        'tblSeasonScrapeOptions
        '
        Me.tblSeasonScrapeOptions.AutoSize = True
        Me.tblSeasonScrapeOptions.ColumnCount = 3
        Me.tblSeasonScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSeasonScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSeasonScrapeOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSeasonScrapeOptions.Controls.Add(Me.chkSeasonOptionsAll, 0, 0)
        Me.tblSeasonScrapeOptions.Controls.Add(Me.btnSeasonScrapeOptionsNone, 1, 0)
        Me.tblSeasonScrapeOptions.Controls.Add(Me.chkSeasonOptionsAired, 0, 1)
        Me.tblSeasonScrapeOptions.Controls.Add(Me.chkSeasonOptionsPlot, 0, 2)
        Me.tblSeasonScrapeOptions.Controls.Add(Me.chkSeasonOptionsTitle, 1, 1)
        Me.tblSeasonScrapeOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSeasonScrapeOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblSeasonScrapeOptions.Name = "tblSeasonScrapeOptions"
        Me.tblSeasonScrapeOptions.RowCount = 3
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSeasonScrapeOptions.Size = New System.Drawing.Size(217, 75)
        Me.tblSeasonScrapeOptions.TabIndex = 25
        '
        'chkSeasonOptionsAll
        '
        Me.chkSeasonOptionsAll.AutoSize = True
        Me.chkSeasonOptionsAll.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkSeasonOptionsAll.Location = New System.Drawing.Point(3, 3)
        Me.chkSeasonOptionsAll.Name = "chkSeasonOptionsAll"
        Me.chkSeasonOptionsAll.Size = New System.Drawing.Size(69, 17)
        Me.chkSeasonOptionsAll.TabIndex = 22
        Me.chkSeasonOptionsAll.Text = "All Items"
        Me.chkSeasonOptionsAll.UseVisualStyleBackColor = True
        '
        'btnSeasonScrapeOptionsNone
        '
        Me.btnSeasonScrapeOptionsNone.Location = New System.Drawing.Point(78, 3)
        Me.btnSeasonScrapeOptionsNone.Name = "btnSeasonScrapeOptionsNone"
        Me.btnSeasonScrapeOptionsNone.Size = New System.Drawing.Size(121, 23)
        Me.btnSeasonScrapeOptionsNone.TabIndex = 15
        Me.btnSeasonScrapeOptionsNone.Text = "Select None"
        Me.btnSeasonScrapeOptionsNone.UseVisualStyleBackColor = True
        '
        'chkSeasonOptionsAired
        '
        Me.chkSeasonOptionsAired.AutoSize = True
        Me.chkSeasonOptionsAired.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSeasonOptionsAired.Location = New System.Drawing.Point(3, 32)
        Me.chkSeasonOptionsAired.Name = "chkSeasonOptionsAired"
        Me.chkSeasonOptionsAired.Size = New System.Drawing.Size(53, 17)
        Me.chkSeasonOptionsAired.TabIndex = 3
        Me.chkSeasonOptionsAired.Text = "Aired"
        Me.chkSeasonOptionsAired.UseVisualStyleBackColor = True
        '
        'chkSeasonOptionsPlot
        '
        Me.chkSeasonOptionsPlot.AutoSize = True
        Me.chkSeasonOptionsPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSeasonOptionsPlot.Location = New System.Drawing.Point(3, 55)
        Me.chkSeasonOptionsPlot.Name = "chkSeasonOptionsPlot"
        Me.chkSeasonOptionsPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkSeasonOptionsPlot.TabIndex = 13
        Me.chkSeasonOptionsPlot.Text = "Plot"
        Me.chkSeasonOptionsPlot.UseVisualStyleBackColor = True
        '
        'chkSeasonOptionsTitle
        '
        Me.chkSeasonOptionsTitle.AutoSize = True
        Me.chkSeasonOptionsTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkSeasonOptionsTitle.Location = New System.Drawing.Point(78, 32)
        Me.chkSeasonOptionsTitle.Name = "chkSeasonOptionsTitle"
        Me.chkSeasonOptionsTitle.Size = New System.Drawing.Size(48, 17)
        Me.chkSeasonOptionsTitle.TabIndex = 13
        Me.chkSeasonOptionsTitle.Text = "Title"
        Me.chkSeasonOptionsTitle.UseVisualStyleBackColor = True
        '
        'chkMainModifierKeyart
        '
        Me.chkMainModifierKeyart.AutoSize = True
        Me.chkMainModifierKeyart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMainModifierKeyart.Location = New System.Drawing.Point(106, 55)
        Me.chkMainModifierKeyart.Name = "chkMainModifierKeyart"
        Me.chkMainModifierKeyart.Size = New System.Drawing.Size(57, 17)
        Me.chkMainModifierKeyart.TabIndex = 13
        Me.chkMainModifierKeyart.Text = "Keyart"
        Me.chkMainModifierKeyart.UseVisualStyleBackColor = True
        '
        'dlgCustomScraper
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(976, 577)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgCustomScraper"
        Me.Text = "Custom Scraper"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblTop.ResumeLayout(False)
        Me.tblTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbScrapeType_Filter.ResumeLayout(False)
        Me.gbScrapeType_Filter.PerformLayout()
        Me.tblScrapeType_Filter.ResumeLayout(False)
        Me.tblScrapeType_Filter.PerformLayout()
        Me.gbScrapeType_Mode.ResumeLayout(False)
        Me.gbScrapeType_Mode.PerformLayout()
        Me.tblScrapeType_Mode.ResumeLayout(False)
        Me.tblScrapeType_Mode.PerformLayout()
        Me.gbMainScrapeModifiers.ResumeLayout(False)
        Me.gbMainScrapeModifiers.PerformLayout()
        Me.tblMainScrapeModifiers.ResumeLayout(False)
        Me.tblMainScrapeModifiers.PerformLayout()
        Me.gbMainScrapeOptions.ResumeLayout(False)
        Me.gbMainScrapeOptions.PerformLayout()
        Me.tblMainScrapeOptions.ResumeLayout(False)
        Me.tblMainScrapeOptions.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        Me.gbSeasonScrapeModifiers.ResumeLayout(False)
        Me.gbSeasonScrapeModifiers.PerformLayout()
        Me.tblSeasonScrapeModifiers.ResumeLayout(False)
        Me.tblSeasonScrapeModifiers.PerformLayout()
        Me.gbSpecialScrapeModifiers.ResumeLayout(False)
        Me.gbSpecialScrapeModifiers.PerformLayout()
        Me.tblSpecialScrapeModifiers.ResumeLayout(False)
        Me.tblSpecialScrapeModifiers.PerformLayout()
        Me.gbEpisodeScrapeModifiers.ResumeLayout(False)
        Me.gbEpisodeScrapeModifiers.PerformLayout()
        Me.tblEpisodeScrapeModifiers.ResumeLayout(False)
        Me.tblEpisodeScrapeModifiers.PerformLayout()
        Me.gbEpisodeScrapeOptions.ResumeLayout(False)
        Me.gbEpisodeScrapeOptions.PerformLayout()
        Me.tblEpisodeScrapeOptions.ResumeLayout(False)
        Me.tblEpisodeScrapeOptions.PerformLayout()
        Me.gbSeasonScrapeOptions.ResumeLayout(False)
        Me.gbSeasonScrapeOptions.PerformLayout()
        Me.tblSeasonScrapeOptions.ResumeLayout(False)
        Me.tblSeasonScrapeOptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkMainModifierExtrafanarts As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierActorThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainModifierTheme As System.Windows.Forms.CheckBox
    Friend WithEvents rbScrapeType_Skip As System.Windows.Forms.RadioButton
    Friend WithEvents chkMainModifierLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnMainScrapeModifierNone As System.Windows.Forms.Button
    Friend WithEvents btnMainScrapeOptionsNone As System.Windows.Forms.Button
    Friend WithEvents chkMainOptionsCollectionID As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsOriginalTitle As System.Windows.Forms.CheckBox
    Friend WithEvents tblScrapeType_Filter As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents tblBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents tblMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblScrapeType_Mode As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMainScrapeModifiers As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkMainModifierCharacterArt As System.Windows.Forms.CheckBox
    Friend WithEvents tblMainScrapeOptions As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkMainOptionsStatus As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsCreators As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsEpisodeGuideURL As System.Windows.Forms.CheckBox
    Friend WithEvents chkMainOptionsPremiered As System.Windows.Forms.CheckBox
    Friend WithEvents tblTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbSeasonScrapeModifiers As System.Windows.Forms.GroupBox
    Friend WithEvents tblSeasonScrapeModifiers As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSeasonScrapeModifierNone As System.Windows.Forms.Button
    Friend WithEvents chkSeasonModifierAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeasonModifierBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeasonModifierFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeasonModifierLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeasonModifierPoster As System.Windows.Forms.CheckBox
    Friend WithEvents gbSpecialScrapeModifiers As System.Windows.Forms.GroupBox
    Friend WithEvents tblSpecialScrapeModifiers As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkSpecialModifierAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnSpecialScrapeModifierNone As System.Windows.Forms.Button
    Friend WithEvents chkSpecialModifierWithSeasons As System.Windows.Forms.CheckBox
    Friend WithEvents chkSpecialModifierWithEpisodes As System.Windows.Forms.CheckBox
    Friend WithEvents gbEpisodeScrapeModifiers As System.Windows.Forms.GroupBox
    Friend WithEvents tblEpisodeScrapeModifiers As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnEpisodeScrapeModifierNone As System.Windows.Forms.Button
    Friend WithEvents chkEpisodeModifierAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeModifierFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeModifierPoster As System.Windows.Forms.CheckBox
    Friend WithEvents gbEpisodeScrapeOptions As System.Windows.Forms.GroupBox
    Friend WithEvents tblEpisodeScrapeOptions As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkEpisodeOptionsAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeOptionsWriters As System.Windows.Forms.CheckBox
    Friend WithEvents btnEpisodeScrapeOptionsNone As System.Windows.Forms.Button
    Friend WithEvents chkEpisodeOptionsTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeOptionsRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeOptionsActors As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeOptionsAired As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeOptionsGuestStars As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeOptionsRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeOptionsPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeOptionsDirectors As System.Windows.Forms.CheckBox
    Friend WithEvents gbSeasonScrapeOptions As System.Windows.Forms.GroupBox
    Friend WithEvents tblSeasonScrapeOptions As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents chkSeasonOptionsAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnSeasonScrapeOptionsNone As System.Windows.Forms.Button
    Friend WithEvents chkSeasonOptionsAired As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeasonOptionsPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeModifierNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeModifierActorThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkEpisodeModifierMetaData As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeasonOptionsTitle As System.Windows.Forms.CheckBox
    Friend WithEvents rbScrapeType_Selected As RadioButton
    Friend WithEvents rbScrapeType_Filter As RadioButton
    Friend WithEvents chkMainModifierKeyart As CheckBox

#End Region 'Methods

End Class