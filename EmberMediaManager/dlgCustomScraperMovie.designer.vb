<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgCustomScraperMovie
    Inherits System.Windows.Forms.Form

#Region "Fields"

    Friend WithEvents chkModAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsCast As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsCert As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsCrew As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkModEThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkModFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkModMeta As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMusicBy As System.Windows.Forms.CheckBox
    Friend WithEvents chkModNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkModPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsProducers As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsRelease As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsTop250 As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsCountry As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkModTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsWriters As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsYear As System.Windows.Forms.CheckBox
    Friend WithEvents gbOptions As System.Windows.Forms.GroupBox
    Friend WithEvents gbModifiers As System.Windows.Forms.GroupBox
    Friend WithEvents gbUpdateModifier As System.Windows.Forms.GroupBox
    Friend WithEvents gbUpdateType As System.Windows.Forms.GroupBox
    Friend WithEvents lblTopDescription As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents rbUpdateModifier_All As System.Windows.Forms.RadioButton
    Friend WithEvents rbUpdateModifier_Marked As System.Windows.Forms.RadioButton
    Friend WithEvents rbUpdateModifier_Missing As System.Windows.Forms.RadioButton
    Friend WithEvents rbUpdateModifier_New As System.Windows.Forms.RadioButton
    Friend WithEvents rbUpdate_Ask As System.Windows.Forms.RadioButton
    Friend WithEvents rbUpdate_Auto As System.Windows.Forms.RadioButton
    Friend WithEvents Update_Button As System.Windows.Forms.Button

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCustomScraperMovie))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDescription = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.rbUpdateModifier_All = New System.Windows.Forms.RadioButton()
        Me.gbUpdateModifier = New System.Windows.Forms.GroupBox()
        Me.rbUpdateModifier_Marked = New System.Windows.Forms.RadioButton()
        Me.rbUpdateModifier_New = New System.Windows.Forms.RadioButton()
        Me.rbUpdateModifier_Missing = New System.Windows.Forms.RadioButton()
        Me.gbUpdateType = New System.Windows.Forms.GroupBox()
        Me.rbUpdate_Skip = New System.Windows.Forms.RadioButton()
        Me.rbUpdate_Ask = New System.Windows.Forms.RadioButton()
        Me.rbUpdate_Auto = New System.Windows.Forms.RadioButton()
        Me.gbModifiers = New System.Windows.Forms.GroupBox()
        Me.btnModNone = New System.Windows.Forms.Button()
        Me.chkModLandscape = New System.Windows.Forms.CheckBox()
        Me.chkModBanner = New System.Windows.Forms.CheckBox()
        Me.chkModClearArt = New System.Windows.Forms.CheckBox()
        Me.chkModClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkModDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkModTheme = New System.Windows.Forms.CheckBox()
        Me.chkModActorThumbs = New System.Windows.Forms.CheckBox()
        Me.chkModEFanarts = New System.Windows.Forms.CheckBox()
        Me.chkModTrailer = New System.Windows.Forms.CheckBox()
        Me.chkModEThumbs = New System.Windows.Forms.CheckBox()
        Me.chkModMeta = New System.Windows.Forms.CheckBox()
        Me.chkModFanart = New System.Windows.Forms.CheckBox()
        Me.chkModPoster = New System.Windows.Forms.CheckBox()
        Me.chkModNFO = New System.Windows.Forms.CheckBox()
        Me.chkModAll = New System.Windows.Forms.CheckBox()
        Me.Update_Button = New System.Windows.Forms.Button()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.chkOptsOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.chkOptsCollectionID = New System.Windows.Forms.CheckBox()
        Me.btnOptsNone = New System.Windows.Forms.Button()
        Me.chkOptsAll = New System.Windows.Forms.CheckBox()
        Me.chkOptsCert = New System.Windows.Forms.CheckBox()
        Me.chkOptsCountry = New System.Windows.Forms.CheckBox()
        Me.chkOptsTop250 = New System.Windows.Forms.CheckBox()
        Me.chkOptsCrew = New System.Windows.Forms.CheckBox()
        Me.chkOptsMusicBy = New System.Windows.Forms.CheckBox()
        Me.chkOptsProducers = New System.Windows.Forms.CheckBox()
        Me.chkOptsWriters = New System.Windows.Forms.CheckBox()
        Me.chkOptsStudio = New System.Windows.Forms.CheckBox()
        Me.chkOptsRuntime = New System.Windows.Forms.CheckBox()
        Me.chkOptsPlot = New System.Windows.Forms.CheckBox()
        Me.chkOptsOutline = New System.Windows.Forms.CheckBox()
        Me.chkOptsGenre = New System.Windows.Forms.CheckBox()
        Me.chkOptsDirector = New System.Windows.Forms.CheckBox()
        Me.chkOptsTagline = New System.Windows.Forms.CheckBox()
        Me.chkOptsCast = New System.Windows.Forms.CheckBox()
        Me.chkOptsTrailer = New System.Windows.Forms.CheckBox()
        Me.chkOptsRating = New System.Windows.Forms.CheckBox()
        Me.chkOptsRelease = New System.Windows.Forms.CheckBox()
        Me.chkOptsMPAA = New System.Windows.Forms.CheckBox()
        Me.chkOptsYear = New System.Windows.Forms.CheckBox()
        Me.chkOptsTitle = New System.Windows.Forms.CheckBox()
        Me.pnlUpdateMedia = New System.Windows.Forms.Panel()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbUpdateModifier.SuspendLayout()
        Me.gbUpdateType.SuspendLayout()
        Me.gbModifiers.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        Me.pnlUpdateMedia.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.OK_Button.Location = New System.Drawing.Point(489, 459)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(80, 23)
        Me.OK_Button.TabIndex = 1
        Me.OK_Button.Text = "Cancel"
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblTopDescription)
        Me.pnlTop.Controls.Add(Me.lblTopTitle)
        Me.pnlTop.Controls.Add(Me.pbTopLogo)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(576, 64)
        Me.pnlTop.TabIndex = 2
        '
        'lblTopDescription
        '
        Me.lblTopDescription.AutoSize = True
        Me.lblTopDescription.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDescription.ForeColor = System.Drawing.Color.White
        Me.lblTopDescription.Location = New System.Drawing.Point(64, 38)
        Me.lblTopDescription.Name = "lblTopDescription"
        Me.lblTopDescription.Size = New System.Drawing.Size(130, 13)
        Me.lblTopDescription.TabIndex = 1
        Me.lblTopDescription.Text = "Create a custom scraper"
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(61, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(195, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Custom Scraper"
        '
        'pbTopLogo
        '
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.Location = New System.Drawing.Point(12, 7)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.pbTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'rbUpdateModifier_All
        '
        Me.rbUpdateModifier_All.AutoSize = True
        Me.rbUpdateModifier_All.Checked = True
        Me.rbUpdateModifier_All.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdateModifier_All.Location = New System.Drawing.Point(6, 19)
        Me.rbUpdateModifier_All.Name = "rbUpdateModifier_All"
        Me.rbUpdateModifier_All.Size = New System.Drawing.Size(38, 17)
        Me.rbUpdateModifier_All.TabIndex = 0
        Me.rbUpdateModifier_All.TabStop = True
        Me.rbUpdateModifier_All.Text = "All"
        Me.rbUpdateModifier_All.UseVisualStyleBackColor = True
        '
        'gbUpdateModifier
        '
        Me.gbUpdateModifier.Controls.Add(Me.rbUpdateModifier_Marked)
        Me.gbUpdateModifier.Controls.Add(Me.rbUpdateModifier_New)
        Me.gbUpdateModifier.Controls.Add(Me.rbUpdateModifier_Missing)
        Me.gbUpdateModifier.Controls.Add(Me.rbUpdateModifier_All)
        Me.gbUpdateModifier.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbUpdateModifier.Location = New System.Drawing.Point(6, 2)
        Me.gbUpdateModifier.Name = "gbUpdateModifier"
        Me.gbUpdateModifier.Size = New System.Drawing.Size(275, 68)
        Me.gbUpdateModifier.TabIndex = 0
        Me.gbUpdateModifier.TabStop = False
        Me.gbUpdateModifier.Text = "Selection Filter"
        '
        'rbUpdateModifier_Marked
        '
        Me.rbUpdateModifier_Marked.AutoSize = True
        Me.rbUpdateModifier_Marked.Enabled = False
        Me.rbUpdateModifier_Marked.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdateModifier_Marked.Location = New System.Drawing.Point(126, 42)
        Me.rbUpdateModifier_Marked.Name = "rbUpdateModifier_Marked"
        Me.rbUpdateModifier_Marked.Size = New System.Drawing.Size(64, 17)
        Me.rbUpdateModifier_Marked.TabIndex = 3
        Me.rbUpdateModifier_Marked.Text = "Marked"
        Me.rbUpdateModifier_Marked.UseVisualStyleBackColor = True
        '
        'rbUpdateModifier_New
        '
        Me.rbUpdateModifier_New.AutoSize = True
        Me.rbUpdateModifier_New.Enabled = False
        Me.rbUpdateModifier_New.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdateModifier_New.Location = New System.Drawing.Point(6, 42)
        Me.rbUpdateModifier_New.Name = "rbUpdateModifier_New"
        Me.rbUpdateModifier_New.Size = New System.Drawing.Size(48, 17)
        Me.rbUpdateModifier_New.TabIndex = 1
        Me.rbUpdateModifier_New.Text = "New"
        Me.rbUpdateModifier_New.UseVisualStyleBackColor = True
        '
        'rbUpdateModifier_Missing
        '
        Me.rbUpdateModifier_Missing.AutoSize = True
        Me.rbUpdateModifier_Missing.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdateModifier_Missing.Location = New System.Drawing.Point(126, 20)
        Me.rbUpdateModifier_Missing.Name = "rbUpdateModifier_Missing"
        Me.rbUpdateModifier_Missing.Size = New System.Drawing.Size(95, 17)
        Me.rbUpdateModifier_Missing.TabIndex = 2
        Me.rbUpdateModifier_Missing.Text = "Missing Items"
        Me.rbUpdateModifier_Missing.UseVisualStyleBackColor = True
        '
        'gbUpdateType
        '
        Me.gbUpdateType.Controls.Add(Me.rbUpdate_Skip)
        Me.gbUpdateType.Controls.Add(Me.rbUpdate_Ask)
        Me.gbUpdateType.Controls.Add(Me.rbUpdate_Auto)
        Me.gbUpdateType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbUpdateType.Location = New System.Drawing.Point(6, 79)
        Me.gbUpdateType.Name = "gbUpdateType"
        Me.gbUpdateType.Size = New System.Drawing.Size(275, 90)
        Me.gbUpdateType.TabIndex = 1
        Me.gbUpdateType.TabStop = False
        Me.gbUpdateType.Text = "Update Mode"
        '
        'rbUpdate_Skip
        '
        Me.rbUpdate_Skip.AutoSize = True
        Me.rbUpdate_Skip.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdate_Skip.Location = New System.Drawing.Point(6, 64)
        Me.rbUpdate_Skip.Name = "rbUpdate_Skip"
        Me.rbUpdate_Skip.Size = New System.Drawing.Size(206, 17)
        Me.rbUpdate_Skip.TabIndex = 2
        Me.rbUpdate_Skip.Text = "Skip (Skip If More Than One Match)"
        Me.rbUpdate_Skip.UseVisualStyleBackColor = True
        '
        'rbUpdate_Ask
        '
        Me.rbUpdate_Ask.AutoSize = True
        Me.rbUpdate_Ask.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdate_Ask.Location = New System.Drawing.Point(6, 41)
        Me.rbUpdate_Ask.Name = "rbUpdate_Ask"
        Me.rbUpdate_Ask.Size = New System.Drawing.Size(215, 17)
        Me.rbUpdate_Ask.TabIndex = 1
        Me.rbUpdate_Ask.Text = "Ask (Require Input If No Exact Match)"
        Me.rbUpdate_Ask.UseVisualStyleBackColor = True
        '
        'rbUpdate_Auto
        '
        Me.rbUpdate_Auto.AutoSize = True
        Me.rbUpdate_Auto.Checked = True
        Me.rbUpdate_Auto.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdate_Auto.Location = New System.Drawing.Point(6, 18)
        Me.rbUpdate_Auto.Name = "rbUpdate_Auto"
        Me.rbUpdate_Auto.Size = New System.Drawing.Size(174, 17)
        Me.rbUpdate_Auto.TabIndex = 0
        Me.rbUpdate_Auto.TabStop = True
        Me.rbUpdate_Auto.Text = "Automatic (Force Best Match)"
        Me.rbUpdate_Auto.UseVisualStyleBackColor = True
        '
        'gbModifiers
        '
        Me.gbModifiers.Controls.Add(Me.btnModNone)
        Me.gbModifiers.Controls.Add(Me.chkModLandscape)
        Me.gbModifiers.Controls.Add(Me.chkModBanner)
        Me.gbModifiers.Controls.Add(Me.chkModClearArt)
        Me.gbModifiers.Controls.Add(Me.chkModClearLogo)
        Me.gbModifiers.Controls.Add(Me.chkModDiscArt)
        Me.gbModifiers.Controls.Add(Me.chkModTheme)
        Me.gbModifiers.Controls.Add(Me.chkModActorThumbs)
        Me.gbModifiers.Controls.Add(Me.chkModEFanarts)
        Me.gbModifiers.Controls.Add(Me.chkModTrailer)
        Me.gbModifiers.Controls.Add(Me.chkModEThumbs)
        Me.gbModifiers.Controls.Add(Me.chkModMeta)
        Me.gbModifiers.Controls.Add(Me.chkModFanart)
        Me.gbModifiers.Controls.Add(Me.chkModPoster)
        Me.gbModifiers.Controls.Add(Me.chkModNFO)
        Me.gbModifiers.Controls.Add(Me.chkModAll)
        Me.gbModifiers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbModifiers.Location = New System.Drawing.Point(6, 175)
        Me.gbModifiers.Name = "gbModifiers"
        Me.gbModifiers.Size = New System.Drawing.Size(275, 207)
        Me.gbModifiers.TabIndex = 2
        Me.gbModifiers.TabStop = False
        Me.gbModifiers.Text = "Modifiers"
        '
        'btnModNone
        '
        Me.btnModNone.Location = New System.Drawing.Point(153, 17)
        Me.btnModNone.Name = "btnModNone"
        Me.btnModNone.Size = New System.Drawing.Size(116, 23)
        Me.btnModNone.TabIndex = 14
        Me.btnModNone.Text = "Select None"
        Me.btnModNone.UseVisualStyleBackColor = True
        '
        'chkModLandscape
        '
        Me.chkModLandscape.AutoSize = True
        Me.chkModLandscape.Checked = True
        Me.chkModLandscape.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModLandscape.Enabled = False
        Me.chkModLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModLandscape.Location = New System.Drawing.Point(153, 67)
        Me.chkModLandscape.Name = "chkModLandscape"
        Me.chkModLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkModLandscape.TabIndex = 13
        Me.chkModLandscape.Text = "Landscape"
        Me.chkModLandscape.UseVisualStyleBackColor = True
        '
        'chkModBanner
        '
        Me.chkModBanner.AutoSize = True
        Me.chkModBanner.Checked = True
        Me.chkModBanner.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModBanner.Enabled = False
        Me.chkModBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModBanner.Location = New System.Drawing.Point(6, 67)
        Me.chkModBanner.Name = "chkModBanner"
        Me.chkModBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkModBanner.TabIndex = 12
        Me.chkModBanner.Text = "Banner"
        Me.chkModBanner.UseVisualStyleBackColor = True
        '
        'chkModClearArt
        '
        Me.chkModClearArt.AutoSize = True
        Me.chkModClearArt.Checked = True
        Me.chkModClearArt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModClearArt.Enabled = False
        Me.chkModClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModClearArt.Location = New System.Drawing.Point(6, 90)
        Me.chkModClearArt.Name = "chkModClearArt"
        Me.chkModClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkModClearArt.TabIndex = 11
        Me.chkModClearArt.Text = "ClearArt"
        Me.chkModClearArt.UseVisualStyleBackColor = True
        '
        'chkModClearLogo
        '
        Me.chkModClearLogo.AutoSize = True
        Me.chkModClearLogo.Checked = True
        Me.chkModClearLogo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModClearLogo.Enabled = False
        Me.chkModClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModClearLogo.Location = New System.Drawing.Point(6, 113)
        Me.chkModClearLogo.Name = "chkModClearLogo"
        Me.chkModClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkModClearLogo.TabIndex = 10
        Me.chkModClearLogo.Text = "ClearLogo"
        Me.chkModClearLogo.UseVisualStyleBackColor = True
        '
        'chkModDiscArt
        '
        Me.chkModDiscArt.AutoSize = True
        Me.chkModDiscArt.Checked = True
        Me.chkModDiscArt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModDiscArt.Enabled = False
        Me.chkModDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModDiscArt.Location = New System.Drawing.Point(6, 136)
        Me.chkModDiscArt.Name = "chkModDiscArt"
        Me.chkModDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkModDiscArt.TabIndex = 9
        Me.chkModDiscArt.Text = "DiscArt"
        Me.chkModDiscArt.UseVisualStyleBackColor = True
        '
        'chkModTheme
        '
        Me.chkModTheme.AutoSize = True
        Me.chkModTheme.Checked = True
        Me.chkModTheme.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModTheme.Enabled = False
        Me.chkModTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModTheme.Location = New System.Drawing.Point(153, 159)
        Me.chkModTheme.Name = "chkModTheme"
        Me.chkModTheme.Size = New System.Drawing.Size(59, 17)
        Me.chkModTheme.TabIndex = 8
        Me.chkModTheme.Text = "Theme"
        Me.chkModTheme.UseVisualStyleBackColor = True
        '
        'chkModActorThumbs
        '
        Me.chkModActorThumbs.AutoSize = True
        Me.chkModActorThumbs.Checked = True
        Me.chkModActorThumbs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModActorThumbs.Enabled = False
        Me.chkModActorThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModActorThumbs.Location = New System.Drawing.Point(6, 44)
        Me.chkModActorThumbs.Name = "chkModActorThumbs"
        Me.chkModActorThumbs.Size = New System.Drawing.Size(96, 17)
        Me.chkModActorThumbs.TabIndex = 7
        Me.chkModActorThumbs.Text = "Actor Thumbs"
        Me.chkModActorThumbs.UseVisualStyleBackColor = True
        '
        'chkModEFanarts
        '
        Me.chkModEFanarts.AutoSize = True
        Me.chkModEFanarts.Checked = True
        Me.chkModEFanarts.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModEFanarts.Enabled = False
        Me.chkModEFanarts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModEFanarts.Location = New System.Drawing.Point(6, 159)
        Me.chkModEFanarts.Name = "chkModEFanarts"
        Me.chkModEFanarts.Size = New System.Drawing.Size(87, 17)
        Me.chkModEFanarts.TabIndex = 6
        Me.chkModEFanarts.Text = "Extrafanarts"
        Me.chkModEFanarts.UseVisualStyleBackColor = True
        '
        'chkModTrailer
        '
        Me.chkModTrailer.AutoSize = True
        Me.chkModTrailer.Checked = True
        Me.chkModTrailer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModTrailer.Enabled = False
        Me.chkModTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModTrailer.Location = New System.Drawing.Point(153, 182)
        Me.chkModTrailer.Name = "chkModTrailer"
        Me.chkModTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkModTrailer.TabIndex = 5
        Me.chkModTrailer.Text = "Trailer"
        Me.chkModTrailer.UseVisualStyleBackColor = True
        '
        'chkModEThumbs
        '
        Me.chkModEThumbs.AutoSize = True
        Me.chkModEThumbs.Checked = True
        Me.chkModEThumbs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModEThumbs.Enabled = False
        Me.chkModEThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModEThumbs.Location = New System.Drawing.Point(6, 182)
        Me.chkModEThumbs.Name = "chkModEThumbs"
        Me.chkModEThumbs.Size = New System.Drawing.Size(90, 17)
        Me.chkModEThumbs.TabIndex = 4
        Me.chkModEThumbs.Text = "Extrathumbs"
        Me.chkModEThumbs.UseVisualStyleBackColor = True
        '
        'chkModMeta
        '
        Me.chkModMeta.AutoSize = True
        Me.chkModMeta.Checked = True
        Me.chkModMeta.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModMeta.Enabled = False
        Me.chkModMeta.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModMeta.Location = New System.Drawing.Point(153, 90)
        Me.chkModMeta.Name = "chkModMeta"
        Me.chkModMeta.Size = New System.Drawing.Size(79, 17)
        Me.chkModMeta.TabIndex = 3
        Me.chkModMeta.Text = "Meta Data"
        Me.chkModMeta.UseVisualStyleBackColor = True
        '
        'chkModFanart
        '
        Me.chkModFanart.AutoSize = True
        Me.chkModFanart.Checked = True
        Me.chkModFanart.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModFanart.Enabled = False
        Me.chkModFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModFanart.Location = New System.Drawing.Point(153, 44)
        Me.chkModFanart.Name = "chkModFanart"
        Me.chkModFanart.Size = New System.Drawing.Size(59, 17)
        Me.chkModFanart.TabIndex = 2
        Me.chkModFanart.Text = "Fanart"
        Me.chkModFanart.UseVisualStyleBackColor = True
        '
        'chkModPoster
        '
        Me.chkModPoster.AutoSize = True
        Me.chkModPoster.Checked = True
        Me.chkModPoster.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModPoster.Enabled = False
        Me.chkModPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModPoster.Location = New System.Drawing.Point(153, 136)
        Me.chkModPoster.Name = "chkModPoster"
        Me.chkModPoster.Size = New System.Drawing.Size(58, 17)
        Me.chkModPoster.TabIndex = 2
        Me.chkModPoster.Text = "Poster"
        Me.chkModPoster.UseVisualStyleBackColor = True
        '
        'chkModNFO
        '
        Me.chkModNFO.AutoSize = True
        Me.chkModNFO.Checked = True
        Me.chkModNFO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModNFO.Enabled = False
        Me.chkModNFO.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModNFO.Location = New System.Drawing.Point(153, 113)
        Me.chkModNFO.Name = "chkModNFO"
        Me.chkModNFO.Size = New System.Drawing.Size(49, 17)
        Me.chkModNFO.TabIndex = 1
        Me.chkModNFO.Text = "NFO"
        Me.chkModNFO.UseVisualStyleBackColor = True
        '
        'chkModAll
        '
        Me.chkModAll.AutoSize = True
        Me.chkModAll.Checked = True
        Me.chkModAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModAll.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModAll.Location = New System.Drawing.Point(6, 21)
        Me.chkModAll.Name = "chkModAll"
        Me.chkModAll.Size = New System.Drawing.Size(69, 17)
        Me.chkModAll.TabIndex = 0
        Me.chkModAll.Text = "All Items"
        Me.chkModAll.UseVisualStyleBackColor = True
        '
        'Update_Button
        '
        Me.Update_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Update_Button.Enabled = False
        Me.Update_Button.Location = New System.Drawing.Point(401, 459)
        Me.Update_Button.Name = "Update_Button"
        Me.Update_Button.Size = New System.Drawing.Size(80, 23)
        Me.Update_Button.TabIndex = 0
        Me.Update_Button.Text = "Begin"
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.chkOptsOriginalTitle)
        Me.gbOptions.Controls.Add(Me.chkOptsCollectionID)
        Me.gbOptions.Controls.Add(Me.btnOptsNone)
        Me.gbOptions.Controls.Add(Me.chkOptsAll)
        Me.gbOptions.Controls.Add(Me.chkOptsCert)
        Me.gbOptions.Controls.Add(Me.chkOptsCountry)
        Me.gbOptions.Controls.Add(Me.chkOptsTop250)
        Me.gbOptions.Controls.Add(Me.chkOptsCrew)
        Me.gbOptions.Controls.Add(Me.chkOptsMusicBy)
        Me.gbOptions.Controls.Add(Me.chkOptsProducers)
        Me.gbOptions.Controls.Add(Me.chkOptsWriters)
        Me.gbOptions.Controls.Add(Me.chkOptsStudio)
        Me.gbOptions.Controls.Add(Me.chkOptsRuntime)
        Me.gbOptions.Controls.Add(Me.chkOptsPlot)
        Me.gbOptions.Controls.Add(Me.chkOptsOutline)
        Me.gbOptions.Controls.Add(Me.chkOptsGenre)
        Me.gbOptions.Controls.Add(Me.chkOptsDirector)
        Me.gbOptions.Controls.Add(Me.chkOptsTagline)
        Me.gbOptions.Controls.Add(Me.chkOptsCast)
        Me.gbOptions.Controls.Add(Me.chkOptsTrailer)
        Me.gbOptions.Controls.Add(Me.chkOptsRating)
        Me.gbOptions.Controls.Add(Me.chkOptsRelease)
        Me.gbOptions.Controls.Add(Me.chkOptsMPAA)
        Me.gbOptions.Controls.Add(Me.chkOptsYear)
        Me.gbOptions.Controls.Add(Me.chkOptsTitle)
        Me.gbOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbOptions.Location = New System.Drawing.Point(287, 2)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(274, 303)
        Me.gbOptions.TabIndex = 3
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Options"
        '
        'chkOptsOriginalTitle
        '
        Me.chkOptsOriginalTitle.AutoSize = True
        Me.chkOptsOriginalTitle.Checked = True
        Me.chkOptsOriginalTitle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsOriginalTitle.Enabled = False
        Me.chkOptsOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsOriginalTitle.Location = New System.Drawing.Point(147, 267)
        Me.chkOptsOriginalTitle.Name = "chkOptsOriginalTitle"
        Me.chkOptsOriginalTitle.Size = New System.Drawing.Size(92, 17)
        Me.chkOptsOriginalTitle.TabIndex = 24
        Me.chkOptsOriginalTitle.Text = "Original Title"
        Me.chkOptsOriginalTitle.UseVisualStyleBackColor = True
        '
        'chkOptsCollectionID
        '
        Me.chkOptsCollectionID.AutoSize = True
        Me.chkOptsCollectionID.Checked = True
        Me.chkOptsCollectionID.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsCollectionID.Enabled = False
        Me.chkOptsCollectionID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsCollectionID.Location = New System.Drawing.Point(6, 267)
        Me.chkOptsCollectionID.Name = "chkOptsCollectionID"
        Me.chkOptsCollectionID.Size = New System.Drawing.Size(92, 17)
        Me.chkOptsCollectionID.TabIndex = 23
        Me.chkOptsCollectionID.Text = "Collection ID"
        Me.chkOptsCollectionID.UseVisualStyleBackColor = True
        '
        'btnOptsNone
        '
        Me.btnOptsNone.Location = New System.Drawing.Point(147, 17)
        Me.btnOptsNone.Name = "btnOptsNone"
        Me.btnOptsNone.Size = New System.Drawing.Size(121, 23)
        Me.btnOptsNone.TabIndex = 15
        Me.btnOptsNone.Text = "Select None"
        Me.btnOptsNone.UseVisualStyleBackColor = True
        '
        'chkOptsAll
        '
        Me.chkOptsAll.AutoSize = True
        Me.chkOptsAll.Checked = True
        Me.chkOptsAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsAll.Enabled = False
        Me.chkOptsAll.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.chkOptsAll.Location = New System.Drawing.Point(6, 21)
        Me.chkOptsAll.Name = "chkOptsAll"
        Me.chkOptsAll.Size = New System.Drawing.Size(69, 17)
        Me.chkOptsAll.TabIndex = 22
        Me.chkOptsAll.Text = "All Items"
        Me.chkOptsAll.UseVisualStyleBackColor = True
        '
        'chkOptsCert
        '
        Me.chkOptsCert.AutoSize = True
        Me.chkOptsCert.Checked = True
        Me.chkOptsCert.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsCert.Enabled = False
        Me.chkOptsCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsCert.Location = New System.Drawing.Point(6, 104)
        Me.chkOptsCert.Name = "chkOptsCert"
        Me.chkOptsCert.Size = New System.Drawing.Size(89, 17)
        Me.chkOptsCert.TabIndex = 3
        Me.chkOptsCert.Text = "Certification"
        Me.chkOptsCert.UseVisualStyleBackColor = True
        '
        'chkOptsCountry
        '
        Me.chkOptsCountry.AutoSize = True
        Me.chkOptsCountry.Checked = True
        Me.chkOptsCountry.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsCountry.Enabled = False
        Me.chkOptsCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsCountry.Location = New System.Drawing.Point(147, 245)
        Me.chkOptsCountry.Name = "chkOptsCountry"
        Me.chkOptsCountry.Size = New System.Drawing.Size(67, 17)
        Me.chkOptsCountry.TabIndex = 21
        Me.chkOptsCountry.Text = "Country"
        Me.chkOptsCountry.UseVisualStyleBackColor = True
        '
        'chkOptsTop250
        '
        Me.chkOptsTop250.AutoSize = True
        Me.chkOptsTop250.Checked = True
        Me.chkOptsTop250.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsTop250.Enabled = False
        Me.chkOptsTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsTop250.Location = New System.Drawing.Point(147, 225)
        Me.chkOptsTop250.Name = "chkOptsTop250"
        Me.chkOptsTop250.Size = New System.Drawing.Size(66, 17)
        Me.chkOptsTop250.TabIndex = 20
        Me.chkOptsTop250.Text = "Top 250"
        Me.chkOptsTop250.UseVisualStyleBackColor = True
        '
        'chkOptsCrew
        '
        Me.chkOptsCrew.AutoSize = True
        Me.chkOptsCrew.Checked = True
        Me.chkOptsCrew.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsCrew.Enabled = False
        Me.chkOptsCrew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsCrew.Location = New System.Drawing.Point(147, 205)
        Me.chkOptsCrew.Name = "chkOptsCrew"
        Me.chkOptsCrew.Size = New System.Drawing.Size(85, 17)
        Me.chkOptsCrew.TabIndex = 19
        Me.chkOptsCrew.Text = "Other Crew"
        Me.chkOptsCrew.UseVisualStyleBackColor = True
        '
        'chkOptsMusicBy
        '
        Me.chkOptsMusicBy.AutoSize = True
        Me.chkOptsMusicBy.Checked = True
        Me.chkOptsMusicBy.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMusicBy.Enabled = False
        Me.chkOptsMusicBy.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMusicBy.Location = New System.Drawing.Point(147, 185)
        Me.chkOptsMusicBy.Name = "chkOptsMusicBy"
        Me.chkOptsMusicBy.Size = New System.Drawing.Size(71, 17)
        Me.chkOptsMusicBy.TabIndex = 18
        Me.chkOptsMusicBy.Text = "Music By"
        Me.chkOptsMusicBy.UseVisualStyleBackColor = True
        '
        'chkOptsProducers
        '
        Me.chkOptsProducers.AutoSize = True
        Me.chkOptsProducers.Checked = True
        Me.chkOptsProducers.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsProducers.Enabled = False
        Me.chkOptsProducers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsProducers.Location = New System.Drawing.Point(147, 165)
        Me.chkOptsProducers.Name = "chkOptsProducers"
        Me.chkOptsProducers.Size = New System.Drawing.Size(77, 17)
        Me.chkOptsProducers.TabIndex = 17
        Me.chkOptsProducers.Text = "Producers"
        Me.chkOptsProducers.UseVisualStyleBackColor = True
        '
        'chkOptsWriters
        '
        Me.chkOptsWriters.AutoSize = True
        Me.chkOptsWriters.Checked = True
        Me.chkOptsWriters.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsWriters.Enabled = False
        Me.chkOptsWriters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsWriters.Location = New System.Drawing.Point(147, 145)
        Me.chkOptsWriters.Name = "chkOptsWriters"
        Me.chkOptsWriters.Size = New System.Drawing.Size(63, 17)
        Me.chkOptsWriters.TabIndex = 16
        Me.chkOptsWriters.Text = "Writers"
        Me.chkOptsWriters.UseVisualStyleBackColor = True
        '
        'chkOptsStudio
        '
        Me.chkOptsStudio.AutoSize = True
        Me.chkOptsStudio.Checked = True
        Me.chkOptsStudio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsStudio.Enabled = False
        Me.chkOptsStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsStudio.Location = New System.Drawing.Point(6, 204)
        Me.chkOptsStudio.Name = "chkOptsStudio"
        Me.chkOptsStudio.Size = New System.Drawing.Size(60, 17)
        Me.chkOptsStudio.TabIndex = 8
        Me.chkOptsStudio.Text = "Studio"
        Me.chkOptsStudio.UseVisualStyleBackColor = True
        '
        'chkOptsRuntime
        '
        Me.chkOptsRuntime.AutoSize = True
        Me.chkOptsRuntime.Checked = True
        Me.chkOptsRuntime.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsRuntime.Enabled = False
        Me.chkOptsRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsRuntime.Location = New System.Drawing.Point(6, 144)
        Me.chkOptsRuntime.Name = "chkOptsRuntime"
        Me.chkOptsRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkOptsRuntime.TabIndex = 5
        Me.chkOptsRuntime.Text = "Runtime"
        Me.chkOptsRuntime.UseVisualStyleBackColor = True
        '
        'chkOptsPlot
        '
        Me.chkOptsPlot.AutoSize = True
        Me.chkOptsPlot.Checked = True
        Me.chkOptsPlot.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsPlot.Enabled = False
        Me.chkOptsPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsPlot.Location = New System.Drawing.Point(147, 85)
        Me.chkOptsPlot.Name = "chkOptsPlot"
        Me.chkOptsPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkOptsPlot.TabIndex = 13
        Me.chkOptsPlot.Text = "Plot"
        Me.chkOptsPlot.UseVisualStyleBackColor = True
        '
        'chkOptsOutline
        '
        Me.chkOptsOutline.AutoSize = True
        Me.chkOptsOutline.Checked = True
        Me.chkOptsOutline.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsOutline.Enabled = False
        Me.chkOptsOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsOutline.Location = New System.Drawing.Point(147, 65)
        Me.chkOptsOutline.Name = "chkOptsOutline"
        Me.chkOptsOutline.Size = New System.Drawing.Size(65, 17)
        Me.chkOptsOutline.TabIndex = 12
        Me.chkOptsOutline.Text = "Outline"
        Me.chkOptsOutline.UseVisualStyleBackColor = True
        '
        'chkOptsGenre
        '
        Me.chkOptsGenre.AutoSize = True
        Me.chkOptsGenre.Checked = True
        Me.chkOptsGenre.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsGenre.Enabled = False
        Me.chkOptsGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsGenre.Location = New System.Drawing.Point(6, 224)
        Me.chkOptsGenre.Name = "chkOptsGenre"
        Me.chkOptsGenre.Size = New System.Drawing.Size(57, 17)
        Me.chkOptsGenre.TabIndex = 9
        Me.chkOptsGenre.Text = "Genre"
        Me.chkOptsGenre.UseVisualStyleBackColor = True
        '
        'chkOptsDirector
        '
        Me.chkOptsDirector.AutoSize = True
        Me.chkOptsDirector.Checked = True
        Me.chkOptsDirector.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsDirector.Enabled = False
        Me.chkOptsDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsDirector.Location = New System.Drawing.Point(147, 125)
        Me.chkOptsDirector.Name = "chkOptsDirector"
        Me.chkOptsDirector.Size = New System.Drawing.Size(67, 17)
        Me.chkOptsDirector.TabIndex = 15
        Me.chkOptsDirector.Text = "Director"
        Me.chkOptsDirector.UseVisualStyleBackColor = True
        '
        'chkOptsTagline
        '
        Me.chkOptsTagline.AutoSize = True
        Me.chkOptsTagline.Checked = True
        Me.chkOptsTagline.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsTagline.Enabled = False
        Me.chkOptsTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsTagline.Location = New System.Drawing.Point(147, 44)
        Me.chkOptsTagline.Name = "chkOptsTagline"
        Me.chkOptsTagline.Size = New System.Drawing.Size(63, 17)
        Me.chkOptsTagline.TabIndex = 11
        Me.chkOptsTagline.Text = "Tagline"
        Me.chkOptsTagline.UseVisualStyleBackColor = True
        '
        'chkOptsCast
        '
        Me.chkOptsCast.AutoSize = True
        Me.chkOptsCast.Checked = True
        Me.chkOptsCast.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsCast.Enabled = False
        Me.chkOptsCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsCast.Location = New System.Drawing.Point(147, 105)
        Me.chkOptsCast.Name = "chkOptsCast"
        Me.chkOptsCast.Size = New System.Drawing.Size(48, 17)
        Me.chkOptsCast.TabIndex = 14
        Me.chkOptsCast.Text = "Cast"
        Me.chkOptsCast.UseVisualStyleBackColor = True
        '
        'chkOptsTrailer
        '
        Me.chkOptsTrailer.AutoSize = True
        Me.chkOptsTrailer.Checked = True
        Me.chkOptsTrailer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsTrailer.Enabled = False
        Me.chkOptsTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsTrailer.Location = New System.Drawing.Point(6, 244)
        Me.chkOptsTrailer.Name = "chkOptsTrailer"
        Me.chkOptsTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkOptsTrailer.TabIndex = 10
        Me.chkOptsTrailer.Text = "Trailer"
        Me.chkOptsTrailer.UseVisualStyleBackColor = True
        '
        'chkOptsRating
        '
        Me.chkOptsRating.AutoSize = True
        Me.chkOptsRating.Checked = True
        Me.chkOptsRating.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsRating.Enabled = False
        Me.chkOptsRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsRating.Location = New System.Drawing.Point(6, 164)
        Me.chkOptsRating.Name = "chkOptsRating"
        Me.chkOptsRating.Size = New System.Drawing.Size(60, 17)
        Me.chkOptsRating.TabIndex = 6
        Me.chkOptsRating.Text = "Rating"
        Me.chkOptsRating.UseVisualStyleBackColor = True
        '
        'chkOptsRelease
        '
        Me.chkOptsRelease.AutoSize = True
        Me.chkOptsRelease.Checked = True
        Me.chkOptsRelease.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsRelease.Enabled = False
        Me.chkOptsRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsRelease.Location = New System.Drawing.Point(6, 124)
        Me.chkOptsRelease.Name = "chkOptsRelease"
        Me.chkOptsRelease.Size = New System.Drawing.Size(92, 17)
        Me.chkOptsRelease.TabIndex = 4
        Me.chkOptsRelease.Text = "Release Date"
        Me.chkOptsRelease.UseVisualStyleBackColor = True
        '
        'chkOptsMPAA
        '
        Me.chkOptsMPAA.AutoSize = True
        Me.chkOptsMPAA.Checked = True
        Me.chkOptsMPAA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMPAA.Enabled = False
        Me.chkOptsMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMPAA.Location = New System.Drawing.Point(6, 84)
        Me.chkOptsMPAA.Name = "chkOptsMPAA"
        Me.chkOptsMPAA.Size = New System.Drawing.Size(56, 17)
        Me.chkOptsMPAA.TabIndex = 2
        Me.chkOptsMPAA.Text = "MPAA"
        Me.chkOptsMPAA.UseVisualStyleBackColor = True
        '
        'chkOptsYear
        '
        Me.chkOptsYear.AutoSize = True
        Me.chkOptsYear.Checked = True
        Me.chkOptsYear.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsYear.Enabled = False
        Me.chkOptsYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsYear.Location = New System.Drawing.Point(6, 64)
        Me.chkOptsYear.Name = "chkOptsYear"
        Me.chkOptsYear.Size = New System.Drawing.Size(47, 17)
        Me.chkOptsYear.TabIndex = 1
        Me.chkOptsYear.Text = "Year"
        Me.chkOptsYear.UseVisualStyleBackColor = True
        '
        'chkOptsTitle
        '
        Me.chkOptsTitle.AutoSize = True
        Me.chkOptsTitle.Checked = True
        Me.chkOptsTitle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsTitle.Enabled = False
        Me.chkOptsTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsTitle.Location = New System.Drawing.Point(6, 44)
        Me.chkOptsTitle.Name = "chkOptsTitle"
        Me.chkOptsTitle.Size = New System.Drawing.Size(47, 17)
        Me.chkOptsTitle.TabIndex = 0
        Me.chkOptsTitle.Text = "Title"
        Me.chkOptsTitle.UseVisualStyleBackColor = True
        '
        'pnlUpdateMedia
        '
        Me.pnlUpdateMedia.BackColor = System.Drawing.Color.White
        Me.pnlUpdateMedia.Controls.Add(Me.gbOptions)
        Me.pnlUpdateMedia.Controls.Add(Me.gbModifiers)
        Me.pnlUpdateMedia.Controls.Add(Me.gbUpdateType)
        Me.pnlUpdateMedia.Controls.Add(Me.gbUpdateModifier)
        Me.pnlUpdateMedia.Location = New System.Drawing.Point(4, 68)
        Me.pnlUpdateMedia.Name = "pnlUpdateMedia"
        Me.pnlUpdateMedia.Size = New System.Drawing.Size(568, 385)
        Me.pnlUpdateMedia.TabIndex = 3
        '
        'dlgCustomScraper
        '
        Me.AcceptButton = Me.Update_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.OK_Button
        Me.ClientSize = New System.Drawing.Size(576, 484)
        Me.Controls.Add(Me.pnlUpdateMedia)
        Me.Controls.Add(Me.Update_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.OK_Button)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgCustomScraper"
        Me.Text = "Custom Scraper"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbUpdateModifier.ResumeLayout(False)
        Me.gbUpdateModifier.PerformLayout()
        Me.gbUpdateType.ResumeLayout(False)
        Me.gbUpdateType.PerformLayout()
        Me.gbModifiers.ResumeLayout(False)
        Me.gbModifiers.PerformLayout()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.pnlUpdateMedia.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlUpdateMedia As System.Windows.Forms.Panel
    Friend WithEvents chkModEFanarts As System.Windows.Forms.CheckBox
    Friend WithEvents chkModActorThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkModBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkModClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkModClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkModDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkModTheme As System.Windows.Forms.CheckBox
    Friend WithEvents rbUpdate_Skip As System.Windows.Forms.RadioButton
    Friend WithEvents chkModLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnModNone As System.Windows.Forms.Button
    Friend WithEvents btnOptsNone As System.Windows.Forms.Button
    Friend WithEvents chkOptsCollectionID As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsOriginalTitle As System.Windows.Forms.CheckBox

#End Region 'Methods

End Class