<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgUpdateMedia
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend WithEvents chkModAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkCast As System.Windows.Forms.CheckBox
    Friend WithEvents chkCert As System.Windows.Forms.CheckBox
    Friend WithEvents chkCrew As System.Windows.Forms.CheckBox
    Friend WithEvents chkDirector As System.Windows.Forms.CheckBox
    Friend WithEvents chkModEThumbs As System.Windows.Forms.CheckBox
    Friend WithEvents chkModFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkGenre As System.Windows.Forms.CheckBox
    Friend WithEvents chkModMeta As System.Windows.Forms.CheckBox
    Friend WithEvents chkMPAA As System.Windows.Forms.CheckBox
    Friend WithEvents chkMusicBy As System.Windows.Forms.CheckBox
    Friend WithEvents chkModNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkOutline As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkModPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkProducers As System.Windows.Forms.CheckBox
    Friend WithEvents chkRating As System.Windows.Forms.CheckBox
    Friend WithEvents chkRelease As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents chkStudio As System.Windows.Forms.CheckBox
    Friend WithEvents chkTagline As System.Windows.Forms.CheckBox
    Friend WithEvents chkTitle As System.Windows.Forms.CheckBox
    Friend WithEvents chkTop250 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCountry As System.Windows.Forms.CheckBox
    Friend WithEvents chkTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkModTrailer As System.Windows.Forms.CheckBox
    Friend WithEvents chkVotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkWriters As System.Windows.Forms.CheckBox
    Friend WithEvents chkYear As System.Windows.Forms.CheckBox
    Friend WithEvents gbOptions As System.Windows.Forms.GroupBox
    Friend WithEvents gbUpdateItems As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgUpdateMedia))
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
        Me.rbUpdate_Ask = New System.Windows.Forms.RadioButton()
        Me.rbUpdate_Auto = New System.Windows.Forms.RadioButton()
        Me.gbUpdateItems = New System.Windows.Forms.GroupBox()
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
        Me.chkCert = New System.Windows.Forms.CheckBox()
        Me.chkCountry = New System.Windows.Forms.CheckBox()
        Me.chkTop250 = New System.Windows.Forms.CheckBox()
        Me.chkCrew = New System.Windows.Forms.CheckBox()
        Me.chkMusicBy = New System.Windows.Forms.CheckBox()
        Me.chkProducers = New System.Windows.Forms.CheckBox()
        Me.chkWriters = New System.Windows.Forms.CheckBox()
        Me.chkStudio = New System.Windows.Forms.CheckBox()
        Me.chkRuntime = New System.Windows.Forms.CheckBox()
        Me.chkPlot = New System.Windows.Forms.CheckBox()
        Me.chkOutline = New System.Windows.Forms.CheckBox()
        Me.chkGenre = New System.Windows.Forms.CheckBox()
        Me.chkDirector = New System.Windows.Forms.CheckBox()
        Me.chkTagline = New System.Windows.Forms.CheckBox()
        Me.chkCast = New System.Windows.Forms.CheckBox()
        Me.chkVotes = New System.Windows.Forms.CheckBox()
        Me.chkTrailer = New System.Windows.Forms.CheckBox()
        Me.chkRating = New System.Windows.Forms.CheckBox()
        Me.chkRelease = New System.Windows.Forms.CheckBox()
        Me.chkMPAA = New System.Windows.Forms.CheckBox()
        Me.chkYear = New System.Windows.Forms.CheckBox()
        Me.chkTitle = New System.Windows.Forms.CheckBox()
        Me.pnlUpdateMedia = New System.Windows.Forms.Panel()
        Me.chkModTheme = New System.Windows.Forms.CheckBox()
        Me.chkModDiscArt = New System.Windows.Forms.CheckBox()
        Me.chkModClearLogo = New System.Windows.Forms.CheckBox()
        Me.chkModClearArt = New System.Windows.Forms.CheckBox()
        Me.chkModBanner = New System.Windows.Forms.CheckBox()
        Me.chkModLandscape = New System.Windows.Forms.CheckBox()
        Me.rbUpdate_Skip = New System.Windows.Forms.RadioButton()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbUpdateModifier.SuspendLayout()
        Me.gbUpdateType.SuspendLayout()
        Me.gbUpdateItems.SuspendLayout()
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
        Me.rbUpdateModifier_All.Size = New System.Drawing.Size(77, 17)
        Me.rbUpdateModifier_All.TabIndex = 0
        Me.rbUpdateModifier_All.TabStop = True
        Me.rbUpdateModifier_All.Text = "All Movies"
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
        Me.rbUpdateModifier_Marked.Size = New System.Drawing.Size(103, 17)
        Me.rbUpdateModifier_Marked.TabIndex = 3
        Me.rbUpdateModifier_Marked.Text = "Marked Movies"
        Me.rbUpdateModifier_Marked.UseVisualStyleBackColor = True
        '
        'rbUpdateModifier_New
        '
        Me.rbUpdateModifier_New.AutoSize = True
        Me.rbUpdateModifier_New.Enabled = False
        Me.rbUpdateModifier_New.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdateModifier_New.Location = New System.Drawing.Point(6, 42)
        Me.rbUpdateModifier_New.Name = "rbUpdateModifier_New"
        Me.rbUpdateModifier_New.Size = New System.Drawing.Size(87, 17)
        Me.rbUpdateModifier_New.TabIndex = 1
        Me.rbUpdateModifier_New.Text = "New Movies"
        Me.rbUpdateModifier_New.UseVisualStyleBackColor = True
        '
        'rbUpdateModifier_Missing
        '
        Me.rbUpdateModifier_Missing.AutoSize = True
        Me.rbUpdateModifier_Missing.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbUpdateModifier_Missing.Location = New System.Drawing.Point(126, 20)
        Me.rbUpdateModifier_Missing.Name = "rbUpdateModifier_Missing"
        Me.rbUpdateModifier_Missing.Size = New System.Drawing.Size(134, 17)
        Me.rbUpdateModifier_Missing.TabIndex = 2
        Me.rbUpdateModifier_Missing.Text = "Movies Missing Items"
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
        'gbUpdateItems
        '
        Me.gbUpdateItems.Controls.Add(Me.chkModLandscape)
        Me.gbUpdateItems.Controls.Add(Me.chkModBanner)
        Me.gbUpdateItems.Controls.Add(Me.chkModClearArt)
        Me.gbUpdateItems.Controls.Add(Me.chkModClearLogo)
        Me.gbUpdateItems.Controls.Add(Me.chkModDiscArt)
        Me.gbUpdateItems.Controls.Add(Me.chkModTheme)
        Me.gbUpdateItems.Controls.Add(Me.chkModActorThumbs)
        Me.gbUpdateItems.Controls.Add(Me.chkModEFanarts)
        Me.gbUpdateItems.Controls.Add(Me.chkModTrailer)
        Me.gbUpdateItems.Controls.Add(Me.chkModEThumbs)
        Me.gbUpdateItems.Controls.Add(Me.chkModMeta)
        Me.gbUpdateItems.Controls.Add(Me.chkModFanart)
        Me.gbUpdateItems.Controls.Add(Me.chkModPoster)
        Me.gbUpdateItems.Controls.Add(Me.chkModNFO)
        Me.gbUpdateItems.Controls.Add(Me.chkModAll)
        Me.gbUpdateItems.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbUpdateItems.Location = New System.Drawing.Point(6, 175)
        Me.gbUpdateItems.Name = "gbUpdateItems"
        Me.gbUpdateItems.Size = New System.Drawing.Size(275, 207)
        Me.gbUpdateItems.TabIndex = 2
        Me.gbUpdateItems.TabStop = False
        Me.gbUpdateItems.Text = "Modifiers"
        '
        'chkModActorThumbs
        '
        Me.chkModActorThumbs.AutoSize = True
        Me.chkModActorThumbs.Checked = True
        Me.chkModActorThumbs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModActorThumbs.Enabled = False
        Me.chkModActorThumbs.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModActorThumbs.Location = New System.Drawing.Point(14, 41)
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
        Me.chkModEFanarts.Location = New System.Drawing.Point(14, 156)
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
        Me.chkModTrailer.Location = New System.Drawing.Point(149, 156)
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
        Me.chkModEThumbs.Location = New System.Drawing.Point(14, 179)
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
        Me.chkModMeta.Location = New System.Drawing.Point(149, 64)
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
        Me.chkModFanart.Location = New System.Drawing.Point(149, 18)
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
        Me.chkModPoster.Location = New System.Drawing.Point(149, 110)
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
        Me.chkModNFO.Location = New System.Drawing.Point(149, 87)
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
        Me.chkModAll.Location = New System.Drawing.Point(14, 18)
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
        Me.gbOptions.Controls.Add(Me.chkCert)
        Me.gbOptions.Controls.Add(Me.chkCountry)
        Me.gbOptions.Controls.Add(Me.chkTop250)
        Me.gbOptions.Controls.Add(Me.chkCrew)
        Me.gbOptions.Controls.Add(Me.chkMusicBy)
        Me.gbOptions.Controls.Add(Me.chkProducers)
        Me.gbOptions.Controls.Add(Me.chkWriters)
        Me.gbOptions.Controls.Add(Me.chkStudio)
        Me.gbOptions.Controls.Add(Me.chkRuntime)
        Me.gbOptions.Controls.Add(Me.chkPlot)
        Me.gbOptions.Controls.Add(Me.chkOutline)
        Me.gbOptions.Controls.Add(Me.chkGenre)
        Me.gbOptions.Controls.Add(Me.chkDirector)
        Me.gbOptions.Controls.Add(Me.chkTagline)
        Me.gbOptions.Controls.Add(Me.chkCast)
        Me.gbOptions.Controls.Add(Me.chkVotes)
        Me.gbOptions.Controls.Add(Me.chkTrailer)
        Me.gbOptions.Controls.Add(Me.chkRating)
        Me.gbOptions.Controls.Add(Me.chkRelease)
        Me.gbOptions.Controls.Add(Me.chkMPAA)
        Me.gbOptions.Controls.Add(Me.chkYear)
        Me.gbOptions.Controls.Add(Me.chkTitle)
        Me.gbOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbOptions.Location = New System.Drawing.Point(287, 2)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(274, 254)
        Me.gbOptions.TabIndex = 3
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Options"
        '
        'chkCert
        '
        Me.chkCert.AutoSize = True
        Me.chkCert.Checked = True
        Me.chkCert.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCert.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkCert.Location = New System.Drawing.Point(6, 80)
        Me.chkCert.Name = "chkCert"
        Me.chkCert.Size = New System.Drawing.Size(89, 17)
        Me.chkCert.TabIndex = 3
        Me.chkCert.Text = "Certification"
        Me.chkCert.UseVisualStyleBackColor = True
        '
        'chkCountry
        '
        Me.chkCountry.AutoSize = True
        Me.chkCountry.Checked = True
        Me.chkCountry.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCountry.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkCountry.Location = New System.Drawing.Point(147, 220)
        Me.chkCountry.Name = "chkCountry"
        Me.chkCountry.Size = New System.Drawing.Size(67, 17)
        Me.chkCountry.TabIndex = 21
        Me.chkCountry.Text = "Country"
        Me.chkCountry.UseVisualStyleBackColor = True
        '
        'chkTop250
        '
        Me.chkTop250.AutoSize = True
        Me.chkTop250.Checked = True
        Me.chkTop250.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTop250.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkTop250.Location = New System.Drawing.Point(147, 200)
        Me.chkTop250.Name = "chkTop250"
        Me.chkTop250.Size = New System.Drawing.Size(66, 17)
        Me.chkTop250.TabIndex = 20
        Me.chkTop250.Text = "Top 250"
        Me.chkTop250.UseVisualStyleBackColor = True
        '
        'chkCrew
        '
        Me.chkCrew.AutoSize = True
        Me.chkCrew.Checked = True
        Me.chkCrew.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCrew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkCrew.Location = New System.Drawing.Point(147, 180)
        Me.chkCrew.Name = "chkCrew"
        Me.chkCrew.Size = New System.Drawing.Size(85, 17)
        Me.chkCrew.TabIndex = 19
        Me.chkCrew.Text = "Other Crew"
        Me.chkCrew.UseVisualStyleBackColor = True
        '
        'chkMusicBy
        '
        Me.chkMusicBy.AutoSize = True
        Me.chkMusicBy.Checked = True
        Me.chkMusicBy.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMusicBy.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMusicBy.Location = New System.Drawing.Point(147, 160)
        Me.chkMusicBy.Name = "chkMusicBy"
        Me.chkMusicBy.Size = New System.Drawing.Size(71, 17)
        Me.chkMusicBy.TabIndex = 18
        Me.chkMusicBy.Text = "Music By"
        Me.chkMusicBy.UseVisualStyleBackColor = True
        '
        'chkProducers
        '
        Me.chkProducers.AutoSize = True
        Me.chkProducers.Checked = True
        Me.chkProducers.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkProducers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkProducers.Location = New System.Drawing.Point(147, 140)
        Me.chkProducers.Name = "chkProducers"
        Me.chkProducers.Size = New System.Drawing.Size(77, 17)
        Me.chkProducers.TabIndex = 17
        Me.chkProducers.Text = "Producers"
        Me.chkProducers.UseVisualStyleBackColor = True
        '
        'chkWriters
        '
        Me.chkWriters.AutoSize = True
        Me.chkWriters.Checked = True
        Me.chkWriters.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWriters.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkWriters.Location = New System.Drawing.Point(147, 120)
        Me.chkWriters.Name = "chkWriters"
        Me.chkWriters.Size = New System.Drawing.Size(63, 17)
        Me.chkWriters.TabIndex = 16
        Me.chkWriters.Text = "Writers"
        Me.chkWriters.UseVisualStyleBackColor = True
        '
        'chkStudio
        '
        Me.chkStudio.AutoSize = True
        Me.chkStudio.Checked = True
        Me.chkStudio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStudio.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkStudio.Location = New System.Drawing.Point(6, 180)
        Me.chkStudio.Name = "chkStudio"
        Me.chkStudio.Size = New System.Drawing.Size(60, 17)
        Me.chkStudio.TabIndex = 8
        Me.chkStudio.Text = "Studio"
        Me.chkStudio.UseVisualStyleBackColor = True
        '
        'chkRuntime
        '
        Me.chkRuntime.AutoSize = True
        Me.chkRuntime.Checked = True
        Me.chkRuntime.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRuntime.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkRuntime.Location = New System.Drawing.Point(6, 120)
        Me.chkRuntime.Name = "chkRuntime"
        Me.chkRuntime.Size = New System.Drawing.Size(69, 17)
        Me.chkRuntime.TabIndex = 5
        Me.chkRuntime.Text = "Runtime"
        Me.chkRuntime.UseVisualStyleBackColor = True
        '
        'chkPlot
        '
        Me.chkPlot.AutoSize = True
        Me.chkPlot.Checked = True
        Me.chkPlot.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkPlot.Location = New System.Drawing.Point(147, 60)
        Me.chkPlot.Name = "chkPlot"
        Me.chkPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkPlot.TabIndex = 13
        Me.chkPlot.Text = "Plot"
        Me.chkPlot.UseVisualStyleBackColor = True
        '
        'chkOutline
        '
        Me.chkOutline.AutoSize = True
        Me.chkOutline.Checked = True
        Me.chkOutline.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOutline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOutline.Location = New System.Drawing.Point(147, 40)
        Me.chkOutline.Name = "chkOutline"
        Me.chkOutline.Size = New System.Drawing.Size(65, 17)
        Me.chkOutline.TabIndex = 12
        Me.chkOutline.Text = "Outline"
        Me.chkOutline.UseVisualStyleBackColor = True
        '
        'chkGenre
        '
        Me.chkGenre.AutoSize = True
        Me.chkGenre.Checked = True
        Me.chkGenre.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkGenre.Location = New System.Drawing.Point(6, 200)
        Me.chkGenre.Name = "chkGenre"
        Me.chkGenre.Size = New System.Drawing.Size(57, 17)
        Me.chkGenre.TabIndex = 9
        Me.chkGenre.Text = "Genre"
        Me.chkGenre.UseVisualStyleBackColor = True
        '
        'chkDirector
        '
        Me.chkDirector.AutoSize = True
        Me.chkDirector.Checked = True
        Me.chkDirector.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDirector.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkDirector.Location = New System.Drawing.Point(147, 100)
        Me.chkDirector.Name = "chkDirector"
        Me.chkDirector.Size = New System.Drawing.Size(67, 17)
        Me.chkDirector.TabIndex = 15
        Me.chkDirector.Text = "Director"
        Me.chkDirector.UseVisualStyleBackColor = True
        '
        'chkTagline
        '
        Me.chkTagline.AutoSize = True
        Me.chkTagline.Checked = True
        Me.chkTagline.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkTagline.Location = New System.Drawing.Point(147, 19)
        Me.chkTagline.Name = "chkTagline"
        Me.chkTagline.Size = New System.Drawing.Size(63, 17)
        Me.chkTagline.TabIndex = 11
        Me.chkTagline.Text = "Tagline"
        Me.chkTagline.UseVisualStyleBackColor = True
        '
        'chkCast
        '
        Me.chkCast.AutoSize = True
        Me.chkCast.Checked = True
        Me.chkCast.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCast.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkCast.Location = New System.Drawing.Point(147, 80)
        Me.chkCast.Name = "chkCast"
        Me.chkCast.Size = New System.Drawing.Size(48, 17)
        Me.chkCast.TabIndex = 14
        Me.chkCast.Text = "Cast"
        Me.chkCast.UseVisualStyleBackColor = True
        '
        'chkVotes
        '
        Me.chkVotes.AutoSize = True
        Me.chkVotes.Checked = True
        Me.chkVotes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVotes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkVotes.Location = New System.Drawing.Point(6, 160)
        Me.chkVotes.Name = "chkVotes"
        Me.chkVotes.Size = New System.Drawing.Size(55, 17)
        Me.chkVotes.TabIndex = 7
        Me.chkVotes.Text = "Votes"
        Me.chkVotes.UseVisualStyleBackColor = True
        '
        'chkTrailer
        '
        Me.chkTrailer.AutoSize = True
        Me.chkTrailer.Checked = True
        Me.chkTrailer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkTrailer.Location = New System.Drawing.Point(6, 220)
        Me.chkTrailer.Name = "chkTrailer"
        Me.chkTrailer.Size = New System.Drawing.Size(57, 17)
        Me.chkTrailer.TabIndex = 10
        Me.chkTrailer.Text = "Trailer"
        Me.chkTrailer.UseVisualStyleBackColor = True
        '
        'chkRating
        '
        Me.chkRating.AutoSize = True
        Me.chkRating.Checked = True
        Me.chkRating.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRating.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkRating.Location = New System.Drawing.Point(6, 140)
        Me.chkRating.Name = "chkRating"
        Me.chkRating.Size = New System.Drawing.Size(60, 17)
        Me.chkRating.TabIndex = 6
        Me.chkRating.Text = "Rating"
        Me.chkRating.UseVisualStyleBackColor = True
        '
        'chkRelease
        '
        Me.chkRelease.AutoSize = True
        Me.chkRelease.Checked = True
        Me.chkRelease.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRelease.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkRelease.Location = New System.Drawing.Point(6, 100)
        Me.chkRelease.Name = "chkRelease"
        Me.chkRelease.Size = New System.Drawing.Size(92, 17)
        Me.chkRelease.TabIndex = 4
        Me.chkRelease.Text = "Release Date"
        Me.chkRelease.UseVisualStyleBackColor = True
        '
        'chkMPAA
        '
        Me.chkMPAA.AutoSize = True
        Me.chkMPAA.Checked = True
        Me.chkMPAA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMPAA.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMPAA.Location = New System.Drawing.Point(6, 60)
        Me.chkMPAA.Name = "chkMPAA"
        Me.chkMPAA.Size = New System.Drawing.Size(56, 17)
        Me.chkMPAA.TabIndex = 2
        Me.chkMPAA.Text = "MPAA"
        Me.chkMPAA.UseVisualStyleBackColor = True
        '
        'chkYear
        '
        Me.chkYear.AutoSize = True
        Me.chkYear.Checked = True
        Me.chkYear.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkYear.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkYear.Location = New System.Drawing.Point(6, 40)
        Me.chkYear.Name = "chkYear"
        Me.chkYear.Size = New System.Drawing.Size(47, 17)
        Me.chkYear.TabIndex = 1
        Me.chkYear.Text = "Year"
        Me.chkYear.UseVisualStyleBackColor = True
        '
        'chkTitle
        '
        Me.chkTitle.AutoSize = True
        Me.chkTitle.Checked = True
        Me.chkTitle.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkTitle.Location = New System.Drawing.Point(6, 19)
        Me.chkTitle.Name = "chkTitle"
        Me.chkTitle.Size = New System.Drawing.Size(47, 17)
        Me.chkTitle.TabIndex = 0
        Me.chkTitle.Text = "Title"
        Me.chkTitle.UseVisualStyleBackColor = True
        '
        'pnlUpdateMedia
        '
        Me.pnlUpdateMedia.BackColor = System.Drawing.Color.White
        Me.pnlUpdateMedia.Controls.Add(Me.gbOptions)
        Me.pnlUpdateMedia.Controls.Add(Me.gbUpdateItems)
        Me.pnlUpdateMedia.Controls.Add(Me.gbUpdateType)
        Me.pnlUpdateMedia.Controls.Add(Me.gbUpdateModifier)
        Me.pnlUpdateMedia.Location = New System.Drawing.Point(4, 68)
        Me.pnlUpdateMedia.Name = "pnlUpdateMedia"
        Me.pnlUpdateMedia.Size = New System.Drawing.Size(568, 385)
        Me.pnlUpdateMedia.TabIndex = 3
        '
        'chkModTheme
        '
        Me.chkModTheme.AutoSize = True
        Me.chkModTheme.Checked = True
        Me.chkModTheme.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModTheme.Enabled = False
        Me.chkModTheme.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModTheme.Location = New System.Drawing.Point(149, 133)
        Me.chkModTheme.Name = "chkModTheme"
        Me.chkModTheme.Size = New System.Drawing.Size(59, 17)
        Me.chkModTheme.TabIndex = 8
        Me.chkModTheme.Text = "Theme"
        Me.chkModTheme.UseVisualStyleBackColor = True
        '
        'chkModDiscArt
        '
        Me.chkModDiscArt.AutoSize = True
        Me.chkModDiscArt.Checked = True
        Me.chkModDiscArt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModDiscArt.Enabled = False
        Me.chkModDiscArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModDiscArt.Location = New System.Drawing.Point(14, 133)
        Me.chkModDiscArt.Name = "chkModDiscArt"
        Me.chkModDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkModDiscArt.TabIndex = 9
        Me.chkModDiscArt.Text = "DiscArt"
        Me.chkModDiscArt.UseVisualStyleBackColor = True
        '
        'chkModClearLogo
        '
        Me.chkModClearLogo.AutoSize = True
        Me.chkModClearLogo.Checked = True
        Me.chkModClearLogo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModClearLogo.Enabled = False
        Me.chkModClearLogo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModClearLogo.Location = New System.Drawing.Point(14, 110)
        Me.chkModClearLogo.Name = "chkModClearLogo"
        Me.chkModClearLogo.Size = New System.Drawing.Size(78, 17)
        Me.chkModClearLogo.TabIndex = 10
        Me.chkModClearLogo.Text = "ClearLogo"
        Me.chkModClearLogo.UseVisualStyleBackColor = True
        '
        'chkModClearArt
        '
        Me.chkModClearArt.AutoSize = True
        Me.chkModClearArt.Checked = True
        Me.chkModClearArt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModClearArt.Enabled = False
        Me.chkModClearArt.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModClearArt.Location = New System.Drawing.Point(14, 87)
        Me.chkModClearArt.Name = "chkModClearArt"
        Me.chkModClearArt.Size = New System.Drawing.Size(67, 17)
        Me.chkModClearArt.TabIndex = 11
        Me.chkModClearArt.Text = "ClearArt"
        Me.chkModClearArt.UseVisualStyleBackColor = True
        '
        'chkModBanner
        '
        Me.chkModBanner.AutoSize = True
        Me.chkModBanner.Checked = True
        Me.chkModBanner.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModBanner.Enabled = False
        Me.chkModBanner.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModBanner.Location = New System.Drawing.Point(14, 64)
        Me.chkModBanner.Name = "chkModBanner"
        Me.chkModBanner.Size = New System.Drawing.Size(63, 17)
        Me.chkModBanner.TabIndex = 12
        Me.chkModBanner.Text = "Banner"
        Me.chkModBanner.UseVisualStyleBackColor = True
        '
        'chkModLandscape
        '
        Me.chkModLandscape.AutoSize = True
        Me.chkModLandscape.Checked = True
        Me.chkModLandscape.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModLandscape.Enabled = False
        Me.chkModLandscape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkModLandscape.Location = New System.Drawing.Point(149, 41)
        Me.chkModLandscape.Name = "chkModLandscape"
        Me.chkModLandscape.Size = New System.Drawing.Size(80, 17)
        Me.chkModLandscape.TabIndex = 13
        Me.chkModLandscape.Text = "Landscape"
        Me.chkModLandscape.UseVisualStyleBackColor = True
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
        'dlgUpdateMedia
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
        Me.Name = "dlgUpdateMedia"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Custom Scraper"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbUpdateModifier.ResumeLayout(False)
        Me.gbUpdateModifier.PerformLayout()
        Me.gbUpdateType.ResumeLayout(False)
        Me.gbUpdateType.PerformLayout()
        Me.gbUpdateItems.ResumeLayout(False)
        Me.gbUpdateItems.PerformLayout()
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

#End Region 'Methods

End Class