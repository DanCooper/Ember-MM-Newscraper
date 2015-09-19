<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgCustomScraperTV
    Inherits System.Windows.Forms.Form

#Region "Fields"

    Friend WithEvents chkModAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainCast As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainCert As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainCreator As System.Windows.Forms.CheckBox
    Friend WithEvents chkModFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkModNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkModPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainRelease As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainCountry As System.Windows.Forms.CheckBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCustomScraperTV))
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
        Me.chkModCharacterArt = New System.Windows.Forms.CheckBox()
        Me.chkModTheme = New System.Windows.Forms.CheckBox()
        Me.chkModActorThumbs = New System.Windows.Forms.CheckBox()
        Me.chkModFanart = New System.Windows.Forms.CheckBox()
        Me.chkModPoster = New System.Windows.Forms.CheckBox()
        Me.chkModNFO = New System.Windows.Forms.CheckBox()
        Me.chkModAll = New System.Windows.Forms.CheckBox()
        Me.Update_Button = New System.Windows.Forms.Button()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.chkOptsMainOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.btnOptsNone = New System.Windows.Forms.Button()
        Me.chkOptsAll = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainCert = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainCountry = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainCreator = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainStudio = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainRuntime = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainPlot = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainEpisodeGuideURL = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainGenre = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainStatus = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainCast = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainRating = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainRelease = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainMPAA = New System.Windows.Forms.CheckBox()
        Me.chkOptsMainTitle = New System.Windows.Forms.CheckBox()
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
        Me.gbModifiers.Controls.Add(Me.chkModCharacterArt)
        Me.gbModifiers.Controls.Add(Me.chkModTheme)
        Me.gbModifiers.Controls.Add(Me.chkModActorThumbs)
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
        Me.chkModClearArt.Location = New System.Drawing.Point(6, 113)
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
        Me.chkModClearLogo.Location = New System.Drawing.Point(6, 136)
        Me.chkModClearLogo.Name = "chkModClearLogo"
        Me.chkModClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkModClearLogo.TabIndex = 10
        Me.chkModClearLogo.Text = "ClearLogo"
        Me.chkModClearLogo.UseVisualStyleBackColor = True
        '
        'chkModCharacterArt
        '
        Me.chkModCharacterArt.AutoSize = True
        Me.chkModCharacterArt.Checked = True
        Me.chkModCharacterArt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModCharacterArt.Enabled = False
        Me.chkModCharacterArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModCharacterArt.Location = New System.Drawing.Point(6, 90)
        Me.chkModCharacterArt.Name = "chkModCharacterArt"
        Me.chkModCharacterArt.Size = New System.Drawing.Size(90, 17)
        Me.chkModCharacterArt.TabIndex = 9
        Me.chkModCharacterArt.Text = "CharacterArt"
        Me.chkModCharacterArt.UseVisualStyleBackColor = True
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
        Me.gbOptions.Controls.Add(Me.chkOptsMainOriginalTitle)
        Me.gbOptions.Controls.Add(Me.btnOptsNone)
        Me.gbOptions.Controls.Add(Me.chkOptsAll)
        Me.gbOptions.Controls.Add(Me.chkOptsMainCert)
        Me.gbOptions.Controls.Add(Me.chkOptsMainCountry)
        Me.gbOptions.Controls.Add(Me.chkOptsMainCreator)
        Me.gbOptions.Controls.Add(Me.chkOptsMainStudio)
        Me.gbOptions.Controls.Add(Me.chkOptsMainRuntime)
        Me.gbOptions.Controls.Add(Me.chkOptsMainPlot)
        Me.gbOptions.Controls.Add(Me.chkOptsMainEpisodeGuideURL)
        Me.gbOptions.Controls.Add(Me.chkOptsMainGenre)
        Me.gbOptions.Controls.Add(Me.chkOptsMainStatus)
        Me.gbOptions.Controls.Add(Me.chkOptsMainCast)
        Me.gbOptions.Controls.Add(Me.chkOptsMainRating)
        Me.gbOptions.Controls.Add(Me.chkOptsMainRelease)
        Me.gbOptions.Controls.Add(Me.chkOptsMainMPAA)
        Me.gbOptions.Controls.Add(Me.chkOptsMainTitle)
        Me.gbOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbOptions.Location = New System.Drawing.Point(287, 2)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(274, 303)
        Me.gbOptions.TabIndex = 3
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Options"
        '
        'chkOptsMainOriginalTitle
        '
        Me.chkOptsMainOriginalTitle.AutoSize = True
        Me.chkOptsMainOriginalTitle.Checked = True
        Me.chkOptsMainOriginalTitle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainOriginalTitle.Enabled = False
        Me.chkOptsMainOriginalTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainOriginalTitle.Location = New System.Drawing.Point(147, 267)
        Me.chkOptsMainOriginalTitle.Name = "chkOptsMainOriginalTitle"
        Me.chkOptsMainOriginalTitle.Size = New System.Drawing.Size(92, 17)
        Me.chkOptsMainOriginalTitle.TabIndex = 24
        Me.chkOptsMainOriginalTitle.Text = "Original Title"
        Me.chkOptsMainOriginalTitle.UseVisualStyleBackColor = True
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
        'chkOptsMainCert
        '
        Me.chkOptsMainCert.AutoSize = True
        Me.chkOptsMainCert.Checked = True
        Me.chkOptsMainCert.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainCert.Enabled = False
        Me.chkOptsMainCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainCert.Location = New System.Drawing.Point(6, 104)
        Me.chkOptsMainCert.Name = "chkOptsMainCert"
        Me.chkOptsMainCert.Size = New System.Drawing.Size(89, 17)
        Me.chkOptsMainCert.TabIndex = 3
        Me.chkOptsMainCert.Text = "Certification"
        Me.chkOptsMainCert.UseVisualStyleBackColor = True
        '
        'chkOptsMainCountry
        '
        Me.chkOptsMainCountry.AutoSize = True
        Me.chkOptsMainCountry.Checked = True
        Me.chkOptsMainCountry.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainCountry.Enabled = False
        Me.chkOptsMainCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainCountry.Location = New System.Drawing.Point(147, 245)
        Me.chkOptsMainCountry.Name = "chkOptsMainCountry"
        Me.chkOptsMainCountry.Size = New System.Drawing.Size(67, 17)
        Me.chkOptsMainCountry.TabIndex = 21
        Me.chkOptsMainCountry.Text = "Country"
        Me.chkOptsMainCountry.UseVisualStyleBackColor = True
        '
        'chkOptsMainCreator
        '
        Me.chkOptsMainCreator.AutoSize = True
        Me.chkOptsMainCreator.Checked = True
        Me.chkOptsMainCreator.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainCreator.Enabled = False
        Me.chkOptsMainCreator.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainCreator.Location = New System.Drawing.Point(147, 205)
        Me.chkOptsMainCreator.Name = "chkOptsMainCreator"
        Me.chkOptsMainCreator.Size = New System.Drawing.Size(75, 17)
        Me.chkOptsMainCreator.TabIndex = 19
        Me.chkOptsMainCreator.Text = "Creator(s)"
        Me.chkOptsMainCreator.UseVisualStyleBackColor = True
        '
        'chkOptsMainStudio
        '
        Me.chkOptsMainStudio.AutoSize = True
        Me.chkOptsMainStudio.Checked = True
        Me.chkOptsMainStudio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainStudio.Enabled = False
        Me.chkOptsMainStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainStudio.Location = New System.Drawing.Point(6, 204)
        Me.chkOptsMainStudio.Name = "chkOptsMainStudio"
        Me.chkOptsMainStudio.Size = New System.Drawing.Size(60, 17)
        Me.chkOptsMainStudio.TabIndex = 8
        Me.chkOptsMainStudio.Text = "Studio"
        Me.chkOptsMainStudio.UseVisualStyleBackColor = True
        '
        'chkOptsMainRuntime
        '
        Me.chkOptsMainRuntime.AutoSize = True
        Me.chkOptsMainRuntime.Checked = True
        Me.chkOptsMainRuntime.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainRuntime.Enabled = False
        Me.chkOptsMainRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainRuntime.Location = New System.Drawing.Point(6, 144)
        Me.chkOptsMainRuntime.Name = "chkOptsMainRuntime"
        Me.chkOptsMainRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkOptsMainRuntime.TabIndex = 5
        Me.chkOptsMainRuntime.Text = "Runtime"
        Me.chkOptsMainRuntime.UseVisualStyleBackColor = True
        '
        'chkOptsMainPlot
        '
        Me.chkOptsMainPlot.AutoSize = True
        Me.chkOptsMainPlot.Checked = True
        Me.chkOptsMainPlot.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainPlot.Enabled = False
        Me.chkOptsMainPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainPlot.Location = New System.Drawing.Point(147, 85)
        Me.chkOptsMainPlot.Name = "chkOptsMainPlot"
        Me.chkOptsMainPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkOptsMainPlot.TabIndex = 13
        Me.chkOptsMainPlot.Text = "Plot"
        Me.chkOptsMainPlot.UseVisualStyleBackColor = True
        '
        'chkOptsMainEpisodeGuideURL
        '
        Me.chkOptsMainEpisodeGuideURL.AutoSize = True
        Me.chkOptsMainEpisodeGuideURL.Checked = True
        Me.chkOptsMainEpisodeGuideURL.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainEpisodeGuideURL.Enabled = False
        Me.chkOptsMainEpisodeGuideURL.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainEpisodeGuideURL.Location = New System.Drawing.Point(147, 65)
        Me.chkOptsMainEpisodeGuideURL.Name = "chkOptsMainEpisodeGuideURL"
        Me.chkOptsMainEpisodeGuideURL.Size = New System.Drawing.Size(124, 17)
        Me.chkOptsMainEpisodeGuideURL.TabIndex = 12
        Me.chkOptsMainEpisodeGuideURL.Text = "Episode Guide URL"
        Me.chkOptsMainEpisodeGuideURL.UseVisualStyleBackColor = True
        '
        'chkOptsMainGenre
        '
        Me.chkOptsMainGenre.AutoSize = True
        Me.chkOptsMainGenre.Checked = True
        Me.chkOptsMainGenre.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainGenre.Enabled = False
        Me.chkOptsMainGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainGenre.Location = New System.Drawing.Point(6, 224)
        Me.chkOptsMainGenre.Name = "chkOptsMainGenre"
        Me.chkOptsMainGenre.Size = New System.Drawing.Size(57, 17)
        Me.chkOptsMainGenre.TabIndex = 9
        Me.chkOptsMainGenre.Text = "Genre"
        Me.chkOptsMainGenre.UseVisualStyleBackColor = True
        '
        'chkOptsMainStatus
        '
        Me.chkOptsMainStatus.AutoSize = True
        Me.chkOptsMainStatus.Checked = True
        Me.chkOptsMainStatus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainStatus.Enabled = False
        Me.chkOptsMainStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainStatus.Location = New System.Drawing.Point(147, 44)
        Me.chkOptsMainStatus.Name = "chkOptsMainStatus"
        Me.chkOptsMainStatus.Size = New System.Drawing.Size(58, 17)
        Me.chkOptsMainStatus.TabIndex = 11
        Me.chkOptsMainStatus.Text = "Status"
        Me.chkOptsMainStatus.UseVisualStyleBackColor = True
        '
        'chkOptsMainCast
        '
        Me.chkOptsMainCast.AutoSize = True
        Me.chkOptsMainCast.Checked = True
        Me.chkOptsMainCast.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainCast.Enabled = False
        Me.chkOptsMainCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainCast.Location = New System.Drawing.Point(147, 105)
        Me.chkOptsMainCast.Name = "chkOptsMainCast"
        Me.chkOptsMainCast.Size = New System.Drawing.Size(48, 17)
        Me.chkOptsMainCast.TabIndex = 14
        Me.chkOptsMainCast.Text = "Cast"
        Me.chkOptsMainCast.UseVisualStyleBackColor = True
        '
        'chkOptsMainRating
        '
        Me.chkOptsMainRating.AutoSize = True
        Me.chkOptsMainRating.Checked = True
        Me.chkOptsMainRating.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainRating.Enabled = False
        Me.chkOptsMainRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainRating.Location = New System.Drawing.Point(6, 164)
        Me.chkOptsMainRating.Name = "chkOptsMainRating"
        Me.chkOptsMainRating.Size = New System.Drawing.Size(60, 17)
        Me.chkOptsMainRating.TabIndex = 6
        Me.chkOptsMainRating.Text = "Rating"
        Me.chkOptsMainRating.UseVisualStyleBackColor = True
        '
        'chkOptsMainRelease
        '
        Me.chkOptsMainRelease.AutoSize = True
        Me.chkOptsMainRelease.Checked = True
        Me.chkOptsMainRelease.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainRelease.Enabled = False
        Me.chkOptsMainRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainRelease.Location = New System.Drawing.Point(6, 124)
        Me.chkOptsMainRelease.Name = "chkOptsMainRelease"
        Me.chkOptsMainRelease.Size = New System.Drawing.Size(77, 17)
        Me.chkOptsMainRelease.TabIndex = 4
        Me.chkOptsMainRelease.Text = "Premiered"
        Me.chkOptsMainRelease.UseVisualStyleBackColor = True
        '
        'chkOptsMainMPAA
        '
        Me.chkOptsMainMPAA.AutoSize = True
        Me.chkOptsMainMPAA.Checked = True
        Me.chkOptsMainMPAA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainMPAA.Enabled = False
        Me.chkOptsMainMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainMPAA.Location = New System.Drawing.Point(6, 84)
        Me.chkOptsMainMPAA.Name = "chkOptsMainMPAA"
        Me.chkOptsMainMPAA.Size = New System.Drawing.Size(56, 17)
        Me.chkOptsMainMPAA.TabIndex = 2
        Me.chkOptsMainMPAA.Text = "MPAA"
        Me.chkOptsMainMPAA.UseVisualStyleBackColor = True
        '
        'chkOptsMainTitle
        '
        Me.chkOptsMainTitle.AutoSize = True
        Me.chkOptsMainTitle.Checked = True
        Me.chkOptsMainTitle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsMainTitle.Enabled = False
        Me.chkOptsMainTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsMainTitle.Location = New System.Drawing.Point(6, 44)
        Me.chkOptsMainTitle.Name = "chkOptsMainTitle"
        Me.chkOptsMainTitle.Size = New System.Drawing.Size(47, 17)
        Me.chkOptsMainTitle.TabIndex = 0
        Me.chkOptsMainTitle.Text = "Title"
        Me.chkOptsMainTitle.UseVisualStyleBackColor = True
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
        'dlgCustomScraperTV
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
        Me.Name = "dlgCustomScraperTV"
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
    Friend WithEvents chkModActorThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkModBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkModClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkModClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkModCharacterArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkModTheme As System.Windows.Forms.CheckBox
    Friend WithEvents rbUpdate_Skip As System.Windows.Forms.RadioButton
    Friend WithEvents chkModLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnModNone As System.Windows.Forms.Button
    Friend WithEvents btnOptsNone As System.Windows.Forms.Button
    Friend WithEvents chkOptsMainOriginalTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainEpisodeGuideURL As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsMainStatus As System.Windows.Forms.CheckBox

#End Region 'Methods

End Class