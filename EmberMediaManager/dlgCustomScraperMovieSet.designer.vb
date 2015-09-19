<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgCustomScraperMovieSet
    Inherits System.Windows.Forms.Form

#Region "Fields"

    Friend WithEvents chkModAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkModFanart As System.Windows.Forms.CheckBox
    Friend WithEvents chkModNFO As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsPlot As System.Windows.Forms.CheckBox
    Friend WithEvents chkModPoster As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsTitle As System.Windows.Forms.CheckBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgCustomScraperMovieSet))
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
        Me.chkModFanart = New System.Windows.Forms.CheckBox()
        Me.chkModPoster = New System.Windows.Forms.CheckBox()
        Me.chkModNFO = New System.Windows.Forms.CheckBox()
        Me.chkModAll = New System.Windows.Forms.CheckBox()
        Me.Update_Button = New System.Windows.Forms.Button()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.btnOptsNone = New System.Windows.Forms.Button()
        Me.chkOptsAll = New System.Windows.Forms.CheckBox()
        Me.chkOptsPlot = New System.Windows.Forms.CheckBox()
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
        Me.OK_Button.Location = New System.Drawing.Point(489, 406)
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
        Me.gbModifiers.Controls.Add(Me.chkModFanart)
        Me.gbModifiers.Controls.Add(Me.chkModPoster)
        Me.gbModifiers.Controls.Add(Me.chkModNFO)
        Me.gbModifiers.Controls.Add(Me.chkModAll)
        Me.gbModifiers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbModifiers.Location = New System.Drawing.Point(6, 175)
        Me.gbModifiers.Name = "gbModifiers"
        Me.gbModifiers.Size = New System.Drawing.Size(275, 141)
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
        Me.chkModBanner.Location = New System.Drawing.Point(6, 44)
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
        Me.chkModClearArt.Location = New System.Drawing.Point(6, 67)
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
        Me.chkModClearLogo.Location = New System.Drawing.Point(6, 90)
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
        Me.chkModDiscArt.Location = New System.Drawing.Point(7, 113)
        Me.chkModDiscArt.Name = "chkModDiscArt"
        Me.chkModDiscArt.Size = New System.Drawing.Size(62, 17)
        Me.chkModDiscArt.TabIndex = 9
        Me.chkModDiscArt.Text = "DiscArt"
        Me.chkModDiscArt.UseVisualStyleBackColor = True
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
        Me.chkModPoster.Location = New System.Drawing.Point(153, 113)
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
        Me.chkModNFO.Location = New System.Drawing.Point(153, 90)
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
        Me.Update_Button.Location = New System.Drawing.Point(401, 406)
        Me.Update_Button.Name = "Update_Button"
        Me.Update_Button.Size = New System.Drawing.Size(80, 23)
        Me.Update_Button.TabIndex = 0
        Me.Update_Button.Text = "Begin"
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.btnOptsNone)
        Me.gbOptions.Controls.Add(Me.chkOptsAll)
        Me.gbOptions.Controls.Add(Me.chkOptsPlot)
        Me.gbOptions.Controls.Add(Me.chkOptsTitle)
        Me.gbOptions.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbOptions.Location = New System.Drawing.Point(287, 2)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(274, 314)
        Me.gbOptions.TabIndex = 3
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Options"
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
        'chkOptsPlot
        '
        Me.chkOptsPlot.AutoSize = True
        Me.chkOptsPlot.Checked = True
        Me.chkOptsPlot.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOptsPlot.Enabled = False
        Me.chkOptsPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOptsPlot.Location = New System.Drawing.Point(6, 67)
        Me.chkOptsPlot.Name = "chkOptsPlot"
        Me.chkOptsPlot.Size = New System.Drawing.Size(46, 17)
        Me.chkOptsPlot.TabIndex = 13
        Me.chkOptsPlot.Text = "Plot"
        Me.chkOptsPlot.UseVisualStyleBackColor = True
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
        Me.pnlUpdateMedia.Size = New System.Drawing.Size(568, 328)
        Me.pnlUpdateMedia.TabIndex = 3
        '
        'dlgCustomScraperMovieSet
        '
        Me.AcceptButton = Me.Update_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.OK_Button
        Me.ClientSize = New System.Drawing.Size(576, 431)
        Me.Controls.Add(Me.pnlUpdateMedia)
        Me.Controls.Add(Me.Update_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.OK_Button)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgCustomScraperMovieSet"
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
    Friend WithEvents chkModBanner As System.Windows.Forms.CheckBox
    Friend WithEvents chkModClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents chkModClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents chkModDiscArt As System.Windows.Forms.CheckBox
    Friend WithEvents rbUpdate_Skip As System.Windows.Forms.RadioButton
    Friend WithEvents chkModLandscape As System.Windows.Forms.CheckBox
    Friend WithEvents chkOptsAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnModNone As System.Windows.Forms.Button
    Friend WithEvents btnOptsNone As System.Windows.Forms.Button

#End Region 'Methods

End Class