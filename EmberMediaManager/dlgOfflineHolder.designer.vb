<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgOfflineHolder
    Inherits System.Windows.Forms.Form

#Region "Fields"

    Friend WithEvents btnBackgroundColor As System.Windows.Forms.Button
    Friend WithEvents btnFont As System.Windows.Forms.Button
    Friend WithEvents btnTextColor As System.Windows.Forms.Button
    Friend WithEvents cbFormat As System.Windows.Forms.ComboBox
    Friend WithEvents cbSources As System.Windows.Forms.ComboBox
    Friend WithEvents cdColor As System.Windows.Forms.ColorDialog
    Friend WithEvents fdFont As System.Windows.Forms.FontDialog
    Friend WithEvents chkBackground As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverlay As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseFanart As System.Windows.Forms.CheckBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents colCondition As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents gbPreview As System.Windows.Forms.GroupBox
    Friend WithEvents gbInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblTextColor As System.Windows.Forms.Label
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTaglineTop As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents lblTaglineBGColor As System.Windows.Forms.Label
    Friend WithEvents lblVideoFormat As System.Windows.Forms.Label
    Friend WithEvents lblMovie As System.Windows.Forms.Label
    Friend WithEvents lblTagline As System.Windows.Forms.Label
    Friend WithEvents lvStatus As System.Windows.Forms.ListView
    Friend WithEvents pbPreview As System.Windows.Forms.PictureBox
    Friend WithEvents pbProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tbTagLine As System.Windows.Forms.TrackBar
    Friend WithEvents tmrName As System.Windows.Forms.Timer
    Friend WithEvents tmrNameWait As System.Windows.Forms.Timer
    Friend WithEvents txtMovieName As System.Windows.Forms.TextBox
    Friend WithEvents txtTagline As System.Windows.Forms.TextBox
    Friend WithEvents txtTop As System.Windows.Forms.TextBox

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgOfflineHolder))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.cbSources = New System.Windows.Forms.ComboBox()
        Me.txtMovieName = New System.Windows.Forms.TextBox()
        Me.lblMovie = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.pbProgress = New System.Windows.Forms.ProgressBar()
        Me.lvStatus = New System.Windows.Forms.ListView()
        Me.colCondition = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.chkUseFanart = New System.Windows.Forms.CheckBox()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.txtTagline = New System.Windows.Forms.TextBox()
        Me.btnTextColor = New System.Windows.Forms.Button()
        Me.lblTextColor = New System.Windows.Forms.Label()
        Me.cdColor = New System.Windows.Forms.ColorDialog()
        Me.pbPreview = New System.Windows.Forms.PictureBox()
        Me.gbPreview = New System.Windows.Forms.GroupBox()
        Me.lblVideoFormat = New System.Windows.Forms.Label()
        Me.cbFormat = New System.Windows.Forms.ComboBox()
        Me.chkBackground = New System.Windows.Forms.CheckBox()
        Me.btnBackgroundColor = New System.Windows.Forms.Button()
        Me.lblTaglineBGColor = New System.Windows.Forms.Label()
        Me.chkOverlay = New System.Windows.Forms.CheckBox()
        Me.btnFont = New System.Windows.Forms.Button()
        Me.txtTop = New System.Windows.Forms.TextBox()
        Me.lblTaglineTop = New System.Windows.Forms.Label()
        Me.tmrName = New System.Windows.Forms.Timer(Me.components)
        Me.fdFont = New System.Windows.Forms.FontDialog()
        Me.gbInfo = New System.Windows.Forms.GroupBox()
        Me.tbTagLine = New System.Windows.Forms.TrackBar()
        Me.tmrNameWait = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gbHolderType = New System.Windows.Forms.GroupBox()
        Me.rbMediaStub = New System.Windows.Forms.RadioButton()
        Me.rbDummyMovie = New System.Windows.Forms.RadioButton()
        Me.gbSettings = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMovieTitle = New System.Windows.Forms.TextBox()
        Me.gbDefaults = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnDefaultLoad = New System.Windows.Forms.Button()
        Me.btnDefaultSave = New System.Windows.Forms.Button()
        Me.gbSearch = New System.Windows.Forms.GroupBox()
        Me.gbMovieTitle = New System.Windows.Forms.GroupBox()
        Me.gbType = New System.Windows.Forms.GroupBox()
        Me.rbTypeDVDProfiler = New System.Windows.Forms.RadioButton()
        Me.rbTypeTitle = New System.Windows.Forms.RadioButton()
        Me.gbMode = New System.Windows.Forms.GroupBox()
        Me.rbModeBatch = New System.Windows.Forms.RadioButton()
        Me.rbModeSingle = New System.Windows.Forms.RadioButton()
        Me.gbSource = New System.Windows.Forms.GroupBox()
        Me.gbDVDProfiler = New System.Windows.Forms.GroupBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtDVDTitle = New System.Windows.Forms.TextBox()
        Me.lblSlot = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.lblCaseType = New System.Windows.Forms.Label()
        Me.lblMediaType = New System.Windows.Forms.Label()
        Me.btnLoadSingle = New System.Windows.Forms.Button()
        Me.txtMediaType = New System.Windows.Forms.TextBox()
        Me.txtCaseType = New System.Windows.Forms.TextBox()
        Me.txtSlot = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.btnLoadCollection = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ofdLoadXML = New System.Windows.Forms.OpenFileDialog()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPreview.SuspendLayout()
        Me.gbInfo.SuspendLayout()
        CType(Me.tbTagLine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.gbHolderType.SuspendLayout()
        Me.gbSettings.SuspendLayout()
        Me.gbDefaults.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.gbSearch.SuspendLayout()
        Me.gbMovieTitle.SuspendLayout()
        Me.gbType.SuspendLayout()
        Me.gbMode.SuspendLayout()
        Me.gbSource.SuspendLayout()
        Me.gbDVDProfiler.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClose.Location = New System.Drawing.Point(96, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 22)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblTopDetails)
        Me.pnlTop.Controls.Add(Me.lblTopTitle)
        Me.pnlTop.Controls.Add(Me.pbTopLogo)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(801, 64)
        Me.pnlTop.TabIndex = 2
        '
        'lblTopDetails
        '
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(64, 38)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(102, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Add Offline movie"
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(61, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(280, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Offline Media Manager"
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
        'cbSources
        '
        Me.cbSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSources.FormattingEnabled = True
        Me.cbSources.Location = New System.Drawing.Point(6, 27)
        Me.cbSources.Name = "cbSources"
        Me.cbSources.Size = New System.Drawing.Size(318, 21)
        Me.cbSources.TabIndex = 1
        '
        'txtMovieName
        '
        Me.txtMovieName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtMovieName.Location = New System.Drawing.Point(6, 34)
        Me.txtMovieName.Name = "txtMovieName"
        Me.txtMovieName.Size = New System.Drawing.Size(318, 22)
        Me.txtMovieName.TabIndex = 3
        '
        'lblMovie
        '
        Me.lblMovie.AutoSize = True
        Me.lblMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMovie.Location = New System.Drawing.Point(6, 18)
        Me.lblMovie.Name = "lblMovie"
        Me.lblMovie.Size = New System.Drawing.Size(177, 13)
        Me.lblMovie.TabIndex = 2
        Me.lblMovie.Text = "Place Holder Folder/Movie Name:"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.SystemColors.Control
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(47, 27)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(102, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "Search Movie"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'pbProgress
        '
        Me.pbProgress.Location = New System.Drawing.Point(6, 19)
        Me.pbProgress.MarqueeAnimationSpeed = 25
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(318, 20)
        Me.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbProgress.TabIndex = 0
        Me.pbProgress.Visible = False
        '
        'lvStatus
        '
        Me.lvStatus.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colCondition, Me.colStatus})
        Me.lvStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvStatus.FullRowSelect = True
        Me.lvStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvStatus.Location = New System.Drawing.Point(6, 44)
        Me.lvStatus.MultiSelect = False
        Me.lvStatus.Name = "lvStatus"
        Me.lvStatus.Size = New System.Drawing.Size(318, 141)
        Me.lvStatus.TabIndex = 1
        Me.lvStatus.UseCompatibleStateImageBehavior = False
        Me.lvStatus.View = System.Windows.Forms.View.Details
        '
        'colCondition
        '
        Me.colCondition.Text = "Condition"
        Me.colCondition.Width = 236
        '
        'colStatus
        '
        Me.colStatus.Text = "Status"
        Me.colStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCreate
        '
        Me.btnCreate.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCreate.Enabled = False
        Me.btnCreate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCreate.Location = New System.Drawing.Point(5, 3)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(80, 22)
        Me.btnCreate.TabIndex = 0
        Me.btnCreate.Text = "Create"
        '
        'chkUseFanart
        '
        Me.chkUseFanart.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkUseFanart.Enabled = False
        Me.chkUseFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkUseFanart.Location = New System.Drawing.Point(6, 158)
        Me.chkUseFanart.Name = "chkUseFanart"
        Me.chkUseFanart.Size = New System.Drawing.Size(182, 22)
        Me.chkUseFanart.TabIndex = 9
        Me.chkUseFanart.Text = "Use Fanart for Place Holder Video"
        Me.chkUseFanart.UseVisualStyleBackColor = True
        '
        'lblTagline
        '
        Me.lblTagline.AutoSize = True
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTagline.Location = New System.Drawing.Point(6, 67)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(147, 13)
        Me.lblTagline.TabIndex = 0
        Me.lblTagline.Text = "Place Holder Video Tagline:"
        '
        'txtTagline
        '
        Me.txtTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTagline.Location = New System.Drawing.Point(6, 83)
        Me.txtTagline.Name = "txtTagline"
        Me.txtTagline.Size = New System.Drawing.Size(242, 22)
        Me.txtTagline.TabIndex = 1
        Me.txtTagline.Text = "Insert DVD"
        '
        'btnTextColor
        '
        Me.btnTextColor.BackColor = System.Drawing.Color.White
        Me.btnTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnTextColor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTextColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnTextColor.Location = New System.Drawing.Point(366, 42)
        Me.btnTextColor.Name = "btnTextColor"
        Me.btnTextColor.Size = New System.Drawing.Size(24, 22)
        Me.btnTextColor.TabIndex = 5
        Me.btnTextColor.UseVisualStyleBackColor = False
        '
        'lblTextColor
        '
        Me.lblTextColor.AutoSize = True
        Me.lblTextColor.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTextColor.Location = New System.Drawing.Point(287, 47)
        Me.lblTextColor.Name = "lblTextColor"
        Me.lblTextColor.Size = New System.Drawing.Size(61, 13)
        Me.lblTextColor.TabIndex = 4
        Me.lblTextColor.Text = "Text Color:"
        Me.lblTextColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pbPreview
        '
        Me.pbPreview.Location = New System.Drawing.Point(6, 17)
        Me.pbPreview.Name = "pbPreview"
        Me.pbPreview.Size = New System.Drawing.Size(384, 308)
        Me.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbPreview.TabIndex = 73
        Me.pbPreview.TabStop = False
        '
        'gbPreview
        '
        Me.gbPreview.Controls.Add(Me.pbPreview)
        Me.gbPreview.Enabled = False
        Me.gbPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbPreview.Location = New System.Drawing.Point(366, 320)
        Me.gbPreview.Name = "gbPreview"
        Me.gbPreview.Size = New System.Drawing.Size(396, 333)
        Me.gbPreview.TabIndex = 7
        Me.gbPreview.TabStop = False
        Me.gbPreview.Text = "Preview"
        Me.gbPreview.Visible = False
        '
        'lblVideoFormat
        '
        Me.lblVideoFormat.AutoSize = True
        Me.lblVideoFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblVideoFormat.Location = New System.Drawing.Point(6, 131)
        Me.lblVideoFormat.Name = "lblVideoFormat"
        Me.lblVideoFormat.Size = New System.Drawing.Size(146, 13)
        Me.lblVideoFormat.TabIndex = 7
        Me.lblVideoFormat.Text = "Place Holder Video Format:"
        '
        'cbFormat
        '
        Me.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbFormat.FormattingEnabled = True
        Me.cbFormat.Location = New System.Drawing.Point(160, 128)
        Me.cbFormat.Name = "cbFormat"
        Me.cbFormat.Size = New System.Drawing.Size(95, 21)
        Me.cbFormat.TabIndex = 8
        '
        'chkBackground
        '
        Me.chkBackground.Checked = True
        Me.chkBackground.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkBackground.Enabled = False
        Me.chkBackground.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkBackground.Location = New System.Drawing.Point(6, 202)
        Me.chkBackground.Name = "chkBackground"
        Me.chkBackground.Size = New System.Drawing.Size(182, 22)
        Me.chkBackground.TabIndex = 11
        Me.chkBackground.Text = "Use Tagline background"
        Me.chkBackground.UseVisualStyleBackColor = True
        '
        'btnBackgroundColor
        '
        Me.btnBackgroundColor.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnBackgroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBackgroundColor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBackgroundColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBackgroundColor.Location = New System.Drawing.Point(366, 98)
        Me.btnBackgroundColor.Name = "btnBackgroundColor"
        Me.btnBackgroundColor.Size = New System.Drawing.Size(24, 22)
        Me.btnBackgroundColor.TabIndex = 13
        Me.btnBackgroundColor.UseVisualStyleBackColor = False
        '
        'lblTaglineBGColor
        '
        Me.lblTaglineBGColor.AutoSize = True
        Me.lblTaglineBGColor.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTaglineBGColor.Location = New System.Drawing.Point(204, 103)
        Me.lblTaglineBGColor.Name = "lblTaglineBGColor"
        Me.lblTaglineBGColor.Size = New System.Drawing.Size(144, 13)
        Me.lblTaglineBGColor.TabIndex = 12
        Me.lblTaglineBGColor.Text = "Tagline background Color:"
        '
        'chkOverlay
        '
        Me.chkOverlay.Checked = True
        Me.chkOverlay.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkOverlay.Enabled = False
        Me.chkOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkOverlay.Location = New System.Drawing.Point(6, 180)
        Me.chkOverlay.Name = "chkOverlay"
        Me.chkOverlay.Size = New System.Drawing.Size(182, 22)
        Me.chkOverlay.TabIndex = 10
        Me.chkOverlay.Text = "Use Ember Overlay"
        Me.chkOverlay.UseVisualStyleBackColor = True
        '
        'btnFont
        '
        Me.btnFont.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFont.Location = New System.Drawing.Point(286, 13)
        Me.btnFont.Name = "btnFont"
        Me.btnFont.Size = New System.Drawing.Size(104, 23)
        Me.btnFont.TabIndex = 6
        Me.btnFont.Text = "Select Font..."
        Me.btnFont.UseVisualStyleBackColor = True
        '
        'txtTop
        '
        Me.txtTop.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTop.Location = New System.Drawing.Point(354, 70)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(36, 22)
        Me.txtTop.TabIndex = 3
        Me.txtTop.Text = "470"
        Me.txtTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTaglineTop
        '
        Me.lblTaglineTop.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTaglineTop.Location = New System.Drawing.Point(260, 73)
        Me.lblTaglineTop.Name = "lblTaglineTop"
        Me.lblTaglineTop.Size = New System.Drawing.Size(88, 13)
        Me.lblTaglineTop.TabIndex = 2
        Me.lblTaglineTop.Text = "Tagline Top:"
        Me.lblTaglineTop.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tmrName
        '
        Me.tmrName.Interval = 250
        '
        'fdFont
        '
        Me.fdFont.Font = New System.Drawing.Font("Arial", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '
        'gbInfo
        '
        Me.gbInfo.Controls.Add(Me.pbProgress)
        Me.gbInfo.Controls.Add(Me.lvStatus)
        Me.gbInfo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbInfo.Location = New System.Drawing.Point(10, 460)
        Me.gbInfo.Name = "gbInfo"
        Me.gbInfo.Size = New System.Drawing.Size(330, 193)
        Me.gbInfo.TabIndex = 6
        Me.gbInfo.TabStop = False
        Me.gbInfo.Text = "Information"
        '
        'tbTagLine
        '
        Me.tbTagLine.Location = New System.Drawing.Point(741, 312)
        Me.tbTagLine.Maximum = 576
        Me.tbTagLine.Name = "tbTagLine"
        Me.tbTagLine.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbTagLine.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tbTagLine.RightToLeftLayout = True
        Me.tbTagLine.Size = New System.Drawing.Size(45, 338)
        Me.tbTagLine.TabIndex = 8
        Me.tbTagLine.TickStyle = System.Windows.Forms.TickStyle.None
        Me.tbTagLine.Visible = False
        '
        'tmrNameWait
        '
        Me.tmrNameWait.Interval = 250
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.gbHolderType)
        Me.Panel1.Controls.Add(Me.gbSettings)
        Me.Panel1.Controls.Add(Me.gbSearch)
        Me.Panel1.Controls.Add(Me.gbMovieTitle)
        Me.Panel1.Controls.Add(Me.gbType)
        Me.Panel1.Controls.Add(Me.gbMode)
        Me.Panel1.Controls.Add(Me.gbSource)
        Me.Panel1.Controls.Add(Me.gbDVDProfiler)
        Me.Panel1.Controls.Add(Me.gbInfo)
        Me.Panel1.Controls.Add(Me.gbPreview)
        Me.Panel1.Controls.Add(Me.tbTagLine)
        Me.Panel1.Location = New System.Drawing.Point(4, 68)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(791, 657)
        Me.Panel1.TabIndex = 3
        '
        'gbHolderType
        '
        Me.gbHolderType.Controls.Add(Me.rbMediaStub)
        Me.gbHolderType.Controls.Add(Me.rbDummyMovie)
        Me.gbHolderType.Enabled = False
        Me.gbHolderType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbHolderType.Location = New System.Drawing.Point(567, 10)
        Me.gbHolderType.Name = "gbHolderType"
        Me.gbHolderType.Size = New System.Drawing.Size(195, 68)
        Me.gbHolderType.TabIndex = 17
        Me.gbHolderType.TabStop = False
        Me.gbHolderType.Text = "6. Holder Type"
        '
        'rbMediaStub
        '
        Me.rbMediaStub.AutoSize = True
        Me.rbMediaStub.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rbMediaStub.Location = New System.Drawing.Point(6, 43)
        Me.rbMediaStub.Name = "rbMediaStub"
        Me.rbMediaStub.Size = New System.Drawing.Size(81, 17)
        Me.rbMediaStub.TabIndex = 1
        Me.rbMediaStub.TabStop = True
        Me.rbMediaStub.Text = "MediaStub"
        Me.rbMediaStub.UseVisualStyleBackColor = True
        '
        'rbDummyMovie
        '
        Me.rbDummyMovie.AutoSize = True
        Me.rbDummyMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rbDummyMovie.Location = New System.Drawing.Point(6, 21)
        Me.rbDummyMovie.Name = "rbDummyMovie"
        Me.rbDummyMovie.Size = New System.Drawing.Size(97, 17)
        Me.rbDummyMovie.TabIndex = 0
        Me.rbDummyMovie.TabStop = True
        Me.rbDummyMovie.Text = "Dummy Movie"
        Me.rbDummyMovie.UseVisualStyleBackColor = True
        '
        'gbSettings
        '
        Me.gbSettings.Controls.Add(Me.Label1)
        Me.gbSettings.Controls.Add(Me.txtMovieTitle)
        Me.gbSettings.Controls.Add(Me.gbDefaults)
        Me.gbSettings.Controls.Add(Me.lblVideoFormat)
        Me.gbSettings.Controls.Add(Me.lblTagline)
        Me.gbSettings.Controls.Add(Me.cbFormat)
        Me.gbSettings.Controls.Add(Me.lblTaglineTop)
        Me.gbSettings.Controls.Add(Me.chkBackground)
        Me.gbSettings.Controls.Add(Me.txtTop)
        Me.gbSettings.Controls.Add(Me.btnBackgroundColor)
        Me.gbSettings.Controls.Add(Me.lblTextColor)
        Me.gbSettings.Controls.Add(Me.lblTaglineBGColor)
        Me.gbSettings.Controls.Add(Me.txtTagline)
        Me.gbSettings.Controls.Add(Me.chkOverlay)
        Me.gbSettings.Controls.Add(Me.btnTextColor)
        Me.gbSettings.Controls.Add(Me.btnFont)
        Me.gbSettings.Controls.Add(Me.chkUseFanart)
        Me.gbSettings.Enabled = False
        Me.gbSettings.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSettings.Location = New System.Drawing.Point(366, 84)
        Me.gbSettings.Name = "gbSettings"
        Me.gbSettings.Size = New System.Drawing.Size(396, 230)
        Me.gbSettings.TabIndex = 16
        Me.gbSettings.TabStop = False
        Me.gbSettings.Text = "7. Settings"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(7, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Movie Title"
        '
        'txtMovieTitle
        '
        Me.txtMovieTitle.Enabled = False
        Me.txtMovieTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieTitle.Location = New System.Drawing.Point(6, 38)
        Me.txtMovieTitle.Name = "txtMovieTitle"
        Me.txtMovieTitle.Size = New System.Drawing.Size(241, 22)
        Me.txtMovieTitle.TabIndex = 17
        '
        'gbDefaults
        '
        Me.gbDefaults.Controls.Add(Me.TableLayoutPanel2)
        Me.gbDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gbDefaults.Location = New System.Drawing.Point(194, 171)
        Me.gbDefaults.Name = "gbDefaults"
        Me.gbDefaults.Size = New System.Drawing.Size(196, 53)
        Me.gbDefaults.TabIndex = 16
        Me.gbDefaults.TabStop = False
        Me.gbDefaults.Text = "Defaults"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnDefaultLoad, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnDefaultSave, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(6, 17)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(182, 28)
        Me.TableLayoutPanel2.TabIndex = 15
        '
        'btnDefaultLoad
        '
        Me.btnDefaultLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnDefaultLoad.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDefaultLoad.Location = New System.Drawing.Point(96, 3)
        Me.btnDefaultLoad.Name = "btnDefaultLoad"
        Me.btnDefaultLoad.Size = New System.Drawing.Size(80, 22)
        Me.btnDefaultLoad.TabIndex = 1
        Me.btnDefaultLoad.Text = "Load"
        '
        'btnDefaultSave
        '
        Me.btnDefaultSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnDefaultSave.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnDefaultSave.Location = New System.Drawing.Point(5, 3)
        Me.btnDefaultSave.Name = "btnDefaultSave"
        Me.btnDefaultSave.Size = New System.Drawing.Size(80, 22)
        Me.btnDefaultSave.TabIndex = 0
        Me.btnDefaultSave.Text = "Save"
        '
        'gbSearch
        '
        Me.gbSearch.Controls.Add(Me.btnSearch)
        Me.gbSearch.Enabled = False
        Me.gbSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSearch.Location = New System.Drawing.Point(366, 10)
        Me.gbSearch.Name = "gbSearch"
        Me.gbSearch.Size = New System.Drawing.Size(195, 68)
        Me.gbSearch.TabIndex = 15
        Me.gbSearch.TabStop = False
        Me.gbSearch.Text = "5. Get Movie Details"
        '
        'gbMovieTitle
        '
        Me.gbMovieTitle.Controls.Add(Me.lblMovie)
        Me.gbMovieTitle.Controls.Add(Me.txtMovieName)
        Me.gbMovieTitle.Enabled = False
        Me.gbMovieTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieTitle.Location = New System.Drawing.Point(10, 158)
        Me.gbMovieTitle.Name = "gbMovieTitle"
        Me.gbMovieTitle.Size = New System.Drawing.Size(330, 70)
        Me.gbMovieTitle.TabIndex = 14
        Me.gbMovieTitle.TabStop = False
        Me.gbMovieTitle.Text = "4. Movie Title"
        '
        'gbType
        '
        Me.gbType.Controls.Add(Me.rbTypeDVDProfiler)
        Me.gbType.Controls.Add(Me.rbTypeTitle)
        Me.gbType.Enabled = False
        Me.gbType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbType.Location = New System.Drawing.Point(178, 84)
        Me.gbType.Name = "gbType"
        Me.gbType.Size = New System.Drawing.Size(162, 68)
        Me.gbType.TabIndex = 13
        Me.gbType.TabStop = False
        Me.gbType.Text = "3. Type"
        '
        'rbTypeDVDProfiler
        '
        Me.rbTypeDVDProfiler.AutoSize = True
        Me.rbTypeDVDProfiler.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rbTypeDVDProfiler.Location = New System.Drawing.Point(6, 45)
        Me.rbTypeDVDProfiler.Name = "rbTypeDVDProfiler"
        Me.rbTypeDVDProfiler.Size = New System.Drawing.Size(88, 17)
        Me.rbTypeDVDProfiler.TabIndex = 1
        Me.rbTypeDVDProfiler.TabStop = True
        Me.rbTypeDVDProfiler.Text = "DVD Profiler"
        Me.rbTypeDVDProfiler.UseVisualStyleBackColor = True
        '
        'rbTypeTitle
        '
        Me.rbTypeTitle.AutoSize = True
        Me.rbTypeTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rbTypeTitle.Location = New System.Drawing.Point(7, 22)
        Me.rbTypeTitle.Name = "rbTypeTitle"
        Me.rbTypeTitle.Size = New System.Drawing.Size(80, 17)
        Me.rbTypeTitle.TabIndex = 0
        Me.rbTypeTitle.TabStop = True
        Me.rbTypeTitle.Text = "Movie Title"
        Me.rbTypeTitle.UseVisualStyleBackColor = True
        '
        'gbMode
        '
        Me.gbMode.Controls.Add(Me.rbModeBatch)
        Me.gbMode.Controls.Add(Me.rbModeSingle)
        Me.gbMode.Enabled = False
        Me.gbMode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMode.Location = New System.Drawing.Point(10, 84)
        Me.gbMode.Name = "gbMode"
        Me.gbMode.Size = New System.Drawing.Size(162, 68)
        Me.gbMode.TabIndex = 12
        Me.gbMode.TabStop = False
        Me.gbMode.Text = "2. Single or Batch Mode"
        '
        'rbModeBatch
        '
        Me.rbModeBatch.AutoSize = True
        Me.rbModeBatch.Enabled = False
        Me.rbModeBatch.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rbModeBatch.Location = New System.Drawing.Point(6, 45)
        Me.rbModeBatch.Name = "rbModeBatch"
        Me.rbModeBatch.Size = New System.Drawing.Size(54, 17)
        Me.rbModeBatch.TabIndex = 1
        Me.rbModeBatch.TabStop = True
        Me.rbModeBatch.Text = "Batch"
        Me.rbModeBatch.UseVisualStyleBackColor = True
        '
        'rbModeSingle
        '
        Me.rbModeSingle.AutoSize = True
        Me.rbModeSingle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.rbModeSingle.Location = New System.Drawing.Point(6, 22)
        Me.rbModeSingle.Name = "rbModeSingle"
        Me.rbModeSingle.Size = New System.Drawing.Size(57, 17)
        Me.rbModeSingle.TabIndex = 0
        Me.rbModeSingle.TabStop = True
        Me.rbModeSingle.Text = "Single"
        Me.rbModeSingle.UseVisualStyleBackColor = True
        '
        'gbSource
        '
        Me.gbSource.Controls.Add(Me.cbSources)
        Me.gbSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSource.Location = New System.Drawing.Point(10, 10)
        Me.gbSource.Name = "gbSource"
        Me.gbSource.Size = New System.Drawing.Size(330, 68)
        Me.gbSource.TabIndex = 11
        Me.gbSource.TabStop = False
        Me.gbSource.Text = "1. Add to Source:"
        '
        'gbDVDProfiler
        '
        Me.gbDVDProfiler.Controls.Add(Me.lblTitle)
        Me.gbDVDProfiler.Controls.Add(Me.txtDVDTitle)
        Me.gbDVDProfiler.Controls.Add(Me.lblSlot)
        Me.gbDVDProfiler.Controls.Add(Me.lblLocation)
        Me.gbDVDProfiler.Controls.Add(Me.lblCaseType)
        Me.gbDVDProfiler.Controls.Add(Me.lblMediaType)
        Me.gbDVDProfiler.Controls.Add(Me.btnLoadSingle)
        Me.gbDVDProfiler.Controls.Add(Me.txtMediaType)
        Me.gbDVDProfiler.Controls.Add(Me.txtCaseType)
        Me.gbDVDProfiler.Controls.Add(Me.txtSlot)
        Me.gbDVDProfiler.Controls.Add(Me.txtLocation)
        Me.gbDVDProfiler.Controls.Add(Me.btnLoadCollection)
        Me.gbDVDProfiler.Enabled = False
        Me.gbDVDProfiler.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbDVDProfiler.Location = New System.Drawing.Point(10, 234)
        Me.gbDVDProfiler.Name = "gbDVDProfiler"
        Me.gbDVDProfiler.Size = New System.Drawing.Size(330, 220)
        Me.gbDVDProfiler.TabIndex = 10
        Me.gbDVDProfiler.TabStop = False
        Me.gbDVDProfiler.Text = "4. DVD Profiler"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblTitle.Location = New System.Drawing.Point(6, 61)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(53, 13)
        Me.lblTitle.TabIndex = 20
        Me.lblTitle.Text = "Title = $T"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDVDTitle
        '
        Me.txtDVDTitle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtDVDTitle.Location = New System.Drawing.Point(6, 78)
        Me.txtDVDTitle.Name = "txtDVDTitle"
        Me.txtDVDTitle.Size = New System.Drawing.Size(318, 22)
        Me.txtDVDTitle.TabIndex = 19
        '
        'lblSlot
        '
        Me.lblSlot.AutoSize = True
        Me.lblSlot.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblSlot.Location = New System.Drawing.Point(100, 193)
        Me.lblSlot.Name = "lblSlot"
        Me.lblSlot.Size = New System.Drawing.Size(53, 13)
        Me.lblSlot.TabIndex = 18
        Me.lblSlot.Text = "Slot = $S"
        Me.lblSlot.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblLocation.Location = New System.Drawing.Point(77, 165)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(76, 13)
        Me.lblLocation.TabIndex = 17
        Me.lblLocation.Text = "Location = $L"
        Me.lblLocation.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCaseType
        '
        Me.lblCaseType.AutoSize = True
        Me.lblCaseType.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblCaseType.Location = New System.Drawing.Point(72, 137)
        Me.lblCaseType.Name = "lblCaseType"
        Me.lblCaseType.Size = New System.Drawing.Size(81, 13)
        Me.lblCaseType.TabIndex = 16
        Me.lblCaseType.Text = "CaseType = $C"
        Me.lblCaseType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMediaType
        '
        Me.lblMediaType.AutoSize = True
        Me.lblMediaType.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMediaType.Location = New System.Drawing.Point(66, 109)
        Me.lblMediaType.Name = "lblMediaType"
        Me.lblMediaType.Size = New System.Drawing.Size(87, 13)
        Me.lblMediaType.TabIndex = 15
        Me.lblMediaType.Text = "MediaType = $T"
        Me.lblMediaType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnLoadSingle
        '
        Me.btnLoadSingle.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnLoadSingle.Location = New System.Drawing.Point(159, 21)
        Me.btnLoadSingle.Name = "btnLoadSingle"
        Me.btnLoadSingle.Size = New System.Drawing.Size(165, 23)
        Me.btnLoadSingle.TabIndex = 14
        Me.btnLoadSingle.Text = "Load Single .xml"
        Me.btnLoadSingle.UseVisualStyleBackColor = True
        '
        'txtMediaType
        '
        Me.txtMediaType.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMediaType.Location = New System.Drawing.Point(159, 106)
        Me.txtMediaType.Name = "txtMediaType"
        Me.txtMediaType.ReadOnly = True
        Me.txtMediaType.Size = New System.Drawing.Size(165, 22)
        Me.txtMediaType.TabIndex = 13
        '
        'txtCaseType
        '
        Me.txtCaseType.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCaseType.Location = New System.Drawing.Point(159, 134)
        Me.txtCaseType.Name = "txtCaseType"
        Me.txtCaseType.ReadOnly = True
        Me.txtCaseType.Size = New System.Drawing.Size(165, 22)
        Me.txtCaseType.TabIndex = 12
        '
        'txtSlot
        '
        Me.txtSlot.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtSlot.Location = New System.Drawing.Point(159, 190)
        Me.txtSlot.Name = "txtSlot"
        Me.txtSlot.ReadOnly = True
        Me.txtSlot.Size = New System.Drawing.Size(165, 22)
        Me.txtSlot.TabIndex = 11
        '
        'txtLocation
        '
        Me.txtLocation.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtLocation.Location = New System.Drawing.Point(159, 162)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReadOnly = True
        Me.txtLocation.Size = New System.Drawing.Size(165, 22)
        Me.txtLocation.TabIndex = 10
        '
        'btnLoadCollection
        '
        Me.btnLoadCollection.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadCollection.Location = New System.Drawing.Point(6, 21)
        Me.btnLoadCollection.Name = "btnLoadCollection"
        Me.btnLoadCollection.Size = New System.Drawing.Size(147, 23)
        Me.btnLoadCollection.TabIndex = 9
        Me.btnLoadCollection.Text = "Load Collection.xml"
        Me.btnLoadCollection.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnClose, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnCreate, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(619, 729)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(182, 28)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'dlgOfflineHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(801, 757)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgOfflineHolder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Offline Media Manager"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbPreview.ResumeLayout(False)
        Me.gbPreview.PerformLayout()
        Me.gbInfo.ResumeLayout(False)
        CType(Me.tbTagLine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gbHolderType.ResumeLayout(False)
        Me.gbHolderType.PerformLayout()
        Me.gbSettings.ResumeLayout(False)
        Me.gbSettings.PerformLayout()
        Me.gbDefaults.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.gbSearch.ResumeLayout(False)
        Me.gbMovieTitle.ResumeLayout(False)
        Me.gbMovieTitle.PerformLayout()
        Me.gbType.ResumeLayout(False)
        Me.gbType.PerformLayout()
        Me.gbMode.ResumeLayout(False)
        Me.gbMode.PerformLayout()
        Me.gbSource.ResumeLayout(False)
        Me.gbDVDProfiler.ResumeLayout(False)
        Me.gbDVDProfiler.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnLoadCollection As System.Windows.Forms.Button
    Friend WithEvents gbDVDProfiler As System.Windows.Forms.GroupBox
    Friend WithEvents txtMediaType As System.Windows.Forms.TextBox
    Friend WithEvents txtCaseType As System.Windows.Forms.TextBox
    Friend WithEvents txtSlot As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbSource As System.Windows.Forms.GroupBox
    Friend WithEvents gbSearch As System.Windows.Forms.GroupBox
    Friend WithEvents gbMovieTitle As System.Windows.Forms.GroupBox
    Friend WithEvents gbType As System.Windows.Forms.GroupBox
    Friend WithEvents rbTypeDVDProfiler As System.Windows.Forms.RadioButton
    Friend WithEvents rbTypeTitle As System.Windows.Forms.RadioButton
    Friend WithEvents gbMode As System.Windows.Forms.GroupBox
    Friend WithEvents rbModeBatch As System.Windows.Forms.RadioButton
    Friend WithEvents rbModeSingle As System.Windows.Forms.RadioButton
    Friend WithEvents lblSlot As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents lblCaseType As System.Windows.Forms.Label
    Friend WithEvents lblMediaType As System.Windows.Forms.Label
    Friend WithEvents btnLoadSingle As System.Windows.Forms.Button
    Friend WithEvents ofdLoadXML As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtDVDTitle As System.Windows.Forms.TextBox
    Friend WithEvents gbSettings As System.Windows.Forms.GroupBox
    Friend WithEvents gbDefaults As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnDefaultLoad As System.Windows.Forms.Button
    Friend WithEvents btnDefaultSave As System.Windows.Forms.Button
    Friend WithEvents gbHolderType As System.Windows.Forms.GroupBox
    Friend WithEvents rbMediaStub As System.Windows.Forms.RadioButton
    Friend WithEvents rbDummyMovie As System.Windows.Forms.RadioButton
    Friend WithEvents txtMovieTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

#End Region 'Methods

End Class