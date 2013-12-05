<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgOfflineHolder
    Inherits System.Windows.Forms.Form

#Region "Fields"

    Friend WithEvents btnBackgroundColor As System.Windows.Forms.Button
    Friend WithEvents btnFont As System.Windows.Forms.Button
    Friend WithEvents btnTextColor As System.Windows.Forms.Button
    Friend WithEvents Bulk_Button As System.Windows.Forms.Button
    Friend WithEvents cbFormat As System.Windows.Forms.ComboBox
    Friend WithEvents cbSources As System.Windows.Forms.ComboBox
    Friend WithEvents cdColor As System.Windows.Forms.ColorDialog
    Friend WithEvents fdFont As System.Windows.Forms.FontDialog
    Friend WithEvents chkBackground As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverlay As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseFanart As System.Windows.Forms.CheckBox
    Friend WithEvents CLOSE_Button As System.Windows.Forms.Button
    Friend WithEvents colCondition As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents Create_Button As System.Windows.Forms.Button
    Friend WithEvents btnSearchMovie As System.Windows.Forms.Button
    Friend WithEvents gbPreview As System.Windows.Forms.GroupBox
    Friend WithEvents gbInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblTextColor As System.Windows.Forms.Label
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTaglineTop As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents lblTaglineBGColor As System.Windows.Forms.Label
    Friend WithEvents lblVideoFormat As System.Windows.Forms.Label
    Friend WithEvents lblMovie As System.Windows.Forms.Label
    Friend WithEvents lblSources As System.Windows.Forms.Label
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
        Me.CLOSE_Button = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.cbSources = New System.Windows.Forms.ComboBox()
        Me.lblSources = New System.Windows.Forms.Label()
        Me.txtMovieName = New System.Windows.Forms.TextBox()
        Me.lblMovie = New System.Windows.Forms.Label()
        Me.btnSearchMovie = New System.Windows.Forms.Button()
        Me.Bulk_Button = New System.Windows.Forms.Button()
        Me.pbProgress = New System.Windows.Forms.ProgressBar()
        Me.lvStatus = New System.Windows.Forms.ListView()
        Me.colCondition = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Create_Button = New System.Windows.Forms.Button()
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
        Me.gbDVDProfiler = New System.Windows.Forms.GroupBox()
        Me.btnDVDProfiler = New System.Windows.Forms.Button()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtSlot = New System.Windows.Forms.TextBox()
        Me.txtCaseType = New System.Windows.Forms.TextBox()
        Me.txtMediaType = New System.Windows.Forms.TextBox()
        Me.btnSearchPMovie = New System.Windows.Forms.Button()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbPreview.SuspendLayout()
        Me.gbInfo.SuspendLayout()
        CType(Me.tbTagLine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.gbDVDProfiler.SuspendLayout()
        Me.SuspendLayout()
        '
        'CLOSE_Button
        '
        Me.CLOSE_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.CLOSE_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CLOSE_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.CLOSE_Button.Location = New System.Drawing.Point(668, 567)
        Me.CLOSE_Button.Name = "CLOSE_Button"
        Me.CLOSE_Button.Size = New System.Drawing.Size(80, 23)
        Me.CLOSE_Button.TabIndex = 1
        Me.CLOSE_Button.Text = "Close"
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
        Me.pnlTop.Size = New System.Drawing.Size(773, 64)
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
        Me.cbSources.Location = New System.Drawing.Point(9, 19)
        Me.cbSources.Name = "cbSources"
        Me.cbSources.Size = New System.Drawing.Size(313, 21)
        Me.cbSources.TabIndex = 1
        '
        'lblSources
        '
        Me.lblSources.AutoSize = True
        Me.lblSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSources.Location = New System.Drawing.Point(7, 4)
        Me.lblSources.Name = "lblSources"
        Me.lblSources.Size = New System.Drawing.Size(84, 13)
        Me.lblSources.TabIndex = 0
        Me.lblSources.Text = "Add to Source:"
        '
        'txtMovieName
        '
        Me.txtMovieName.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtMovieName.Location = New System.Drawing.Point(9, 65)
        Me.txtMovieName.Name = "txtMovieName"
        Me.txtMovieName.Size = New System.Drawing.Size(313, 22)
        Me.txtMovieName.TabIndex = 3
        '
        'lblMovie
        '
        Me.lblMovie.AutoSize = True
        Me.lblMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblMovie.Location = New System.Drawing.Point(7, 50)
        Me.lblMovie.Name = "lblMovie"
        Me.lblMovie.Size = New System.Drawing.Size(183, 13)
        Me.lblMovie.TabIndex = 2
        Me.lblMovie.Text = "Place Holder Folder/Movie Name:"
        '
        'btnSearchMovie
        '
        Me.btnSearchMovie.BackColor = System.Drawing.SystemColors.Control
        Me.btnSearchMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSearchMovie.Location = New System.Drawing.Point(9, 90)
        Me.btnSearchMovie.Name = "btnSearchMovie"
        Me.btnSearchMovie.Size = New System.Drawing.Size(102, 23)
        Me.btnSearchMovie.TabIndex = 4
        Me.btnSearchMovie.Text = "Search Movie"
        Me.btnSearchMovie.UseVisualStyleBackColor = True
        '
        'Bulk_Button
        '
        Me.Bulk_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Bulk_Button.Enabled = False
        Me.Bulk_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Bulk_Button.Location = New System.Drawing.Point(117, 90)
        Me.Bulk_Button.Name = "Bulk_Button"
        Me.Bulk_Button.Size = New System.Drawing.Size(80, 23)
        Me.Bulk_Button.TabIndex = 5
        Me.Bulk_Button.Text = "Bulk Creator"
        Me.Bulk_Button.UseVisualStyleBackColor = True
        Me.Bulk_Button.Visible = False
        '
        'pbProgress
        '
        Me.pbProgress.Location = New System.Drawing.Point(6, 19)
        Me.pbProgress.MarqueeAnimationSpeed = 25
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(301, 20)
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
        Me.lvStatus.Location = New System.Drawing.Point(5, 44)
        Me.lvStatus.MultiSelect = False
        Me.lvStatus.Name = "lvStatus"
        Me.lvStatus.Size = New System.Drawing.Size(303, 229)
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
        'Create_Button
        '
        Me.Create_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Create_Button.Enabled = False
        Me.Create_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Create_Button.Location = New System.Drawing.Point(582, 567)
        Me.Create_Button.Name = "Create_Button"
        Me.Create_Button.Size = New System.Drawing.Size(80, 23)
        Me.Create_Button.TabIndex = 0
        Me.Create_Button.Text = "Create"
        '
        'chkUseFanart
        '
        Me.chkUseFanart.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkUseFanart.Enabled = False
        Me.chkUseFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkUseFanart.Location = New System.Drawing.Point(9, 409)
        Me.chkUseFanart.Name = "chkUseFanart"
        Me.chkUseFanart.Size = New System.Drawing.Size(192, 22)
        Me.chkUseFanart.TabIndex = 9
        Me.chkUseFanart.Text = "Use Fanart for Place Holder Video"
        Me.chkUseFanart.UseVisualStyleBackColor = True
        '
        'lblTagline
        '
        Me.lblTagline.AutoSize = True
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTagline.Location = New System.Drawing.Point(7, 342)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(148, 13)
        Me.lblTagline.TabIndex = 0
        Me.lblTagline.Text = "Place Holder Video Tagline:"
        '
        'txtTagline
        '
        Me.txtTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTagline.Location = New System.Drawing.Point(9, 357)
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
        Me.btnTextColor.Location = New System.Drawing.Point(359, 365)
        Me.btnTextColor.Name = "btnTextColor"
        Me.btnTextColor.Size = New System.Drawing.Size(24, 22)
        Me.btnTextColor.TabIndex = 5
        Me.btnTextColor.UseVisualStyleBackColor = False
        '
        'lblTextColor
        '
        Me.lblTextColor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTextColor.Location = New System.Drawing.Point(257, 370)
        Me.lblTextColor.Name = "lblTextColor"
        Me.lblTextColor.Size = New System.Drawing.Size(100, 13)
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
        Me.gbPreview.Controls.Add(Me.lblVideoFormat)
        Me.gbPreview.Controls.Add(Me.cbFormat)
        Me.gbPreview.Controls.Add(Me.chkBackground)
        Me.gbPreview.Controls.Add(Me.btnBackgroundColor)
        Me.gbPreview.Controls.Add(Me.lblTaglineBGColor)
        Me.gbPreview.Controls.Add(Me.chkOverlay)
        Me.gbPreview.Controls.Add(Me.btnFont)
        Me.gbPreview.Controls.Add(Me.chkUseFanart)
        Me.gbPreview.Controls.Add(Me.lblTagline)
        Me.gbPreview.Controls.Add(Me.btnTextColor)
        Me.gbPreview.Controls.Add(Me.txtTagline)
        Me.gbPreview.Controls.Add(Me.lblTextColor)
        Me.gbPreview.Controls.Add(Me.txtTop)
        Me.gbPreview.Controls.Add(Me.lblTaglineTop)
        Me.gbPreview.Controls.Add(Me.pbPreview)
        Me.gbPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbPreview.Location = New System.Drawing.Point(332, 3)
        Me.gbPreview.Name = "gbPreview"
        Me.gbPreview.Size = New System.Drawing.Size(396, 482)
        Me.gbPreview.TabIndex = 7
        Me.gbPreview.TabStop = False
        Me.gbPreview.Text = "Preview"
        '
        'lblVideoFormat
        '
        Me.lblVideoFormat.AutoSize = True
        Me.lblVideoFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoFormat.Location = New System.Drawing.Point(6, 388)
        Me.lblVideoFormat.Name = "lblVideoFormat"
        Me.lblVideoFormat.Size = New System.Drawing.Size(148, 13)
        Me.lblVideoFormat.TabIndex = 7
        Me.lblVideoFormat.Text = "Place Holder Video Format:"
        '
        'cbFormat
        '
        Me.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFormat.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbFormat.FormattingEnabled = True
        Me.cbFormat.Location = New System.Drawing.Point(156, 383)
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
        Me.chkBackground.Location = New System.Drawing.Point(9, 453)
        Me.chkBackground.Name = "chkBackground"
        Me.chkBackground.Size = New System.Drawing.Size(176, 22)
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
        Me.btnBackgroundColor.Location = New System.Drawing.Point(359, 450)
        Me.btnBackgroundColor.Name = "btnBackgroundColor"
        Me.btnBackgroundColor.Size = New System.Drawing.Size(24, 22)
        Me.btnBackgroundColor.TabIndex = 13
        Me.btnBackgroundColor.UseVisualStyleBackColor = False
        '
        'lblTaglineBGColor
        '
        Me.lblTaglineBGColor.AutoSize = True
        Me.lblTaglineBGColor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTaglineBGColor.Location = New System.Drawing.Point(213, 455)
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
        Me.chkOverlay.Location = New System.Drawing.Point(9, 431)
        Me.chkOverlay.Name = "chkOverlay"
        Me.chkOverlay.Size = New System.Drawing.Size(192, 22)
        Me.chkOverlay.TabIndex = 10
        Me.chkOverlay.Text = "Use Ember Overlay"
        Me.chkOverlay.UseVisualStyleBackColor = True
        '
        'btnFont
        '
        Me.btnFont.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnFont.Location = New System.Drawing.Point(280, 390)
        Me.btnFont.Name = "btnFont"
        Me.btnFont.Size = New System.Drawing.Size(104, 23)
        Me.btnFont.TabIndex = 6
        Me.btnFont.Text = "Select Font..."
        Me.btnFont.UseVisualStyleBackColor = True
        '
        'txtTop
        '
        Me.txtTop.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTop.Location = New System.Drawing.Point(347, 340)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(36, 22)
        Me.txtTop.TabIndex = 3
        Me.txtTop.Text = "470"
        Me.txtTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTaglineTop
        '
        Me.lblTaglineTop.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTaglineTop.Location = New System.Drawing.Point(260, 342)
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
        Me.gbInfo.Location = New System.Drawing.Point(9, 206)
        Me.gbInfo.Name = "gbInfo"
        Me.gbInfo.Size = New System.Drawing.Size(313, 279)
        Me.gbInfo.TabIndex = 6
        Me.gbInfo.TabStop = False
        Me.gbInfo.Text = "Information"
        '
        'tbTagLine
        '
        Me.tbTagLine.Location = New System.Drawing.Point(706, 7)
        Me.tbTagLine.Maximum = 576
        Me.tbTagLine.Name = "tbTagLine"
        Me.tbTagLine.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tbTagLine.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tbTagLine.RightToLeftLayout = True
        Me.tbTagLine.Size = New System.Drawing.Size(45, 334)
        Me.tbTagLine.TabIndex = 8
        Me.tbTagLine.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'tmrNameWait
        '
        Me.tmrNameWait.Interval = 250
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.gbDVDProfiler)
        Me.Panel1.Controls.Add(Me.gbInfo)
        Me.Panel1.Controls.Add(Me.gbPreview)
        Me.Panel1.Controls.Add(Me.Bulk_Button)
        Me.Panel1.Controls.Add(Me.btnSearchMovie)
        Me.Panel1.Controls.Add(Me.lblMovie)
        Me.Panel1.Controls.Add(Me.txtMovieName)
        Me.Panel1.Controls.Add(Me.lblSources)
        Me.Panel1.Controls.Add(Me.cbSources)
        Me.Panel1.Controls.Add(Me.tbTagLine)
        Me.Panel1.Location = New System.Drawing.Point(4, 68)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(765, 494)
        Me.Panel1.TabIndex = 3
        '
        'gbDVDProfiler
        '
        Me.gbDVDProfiler.Controls.Add(Me.btnSearchPMovie)
        Me.gbDVDProfiler.Controls.Add(Me.txtMediaType)
        Me.gbDVDProfiler.Controls.Add(Me.txtCaseType)
        Me.gbDVDProfiler.Controls.Add(Me.txtSlot)
        Me.gbDVDProfiler.Controls.Add(Me.txtLocation)
        Me.gbDVDProfiler.Controls.Add(Me.btnDVDProfiler)
        Me.gbDVDProfiler.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbDVDProfiler.Location = New System.Drawing.Point(9, 120)
        Me.gbDVDProfiler.Name = "gbDVDProfiler"
        Me.gbDVDProfiler.Size = New System.Drawing.Size(313, 80)
        Me.gbDVDProfiler.TabIndex = 10
        Me.gbDVDProfiler.TabStop = False
        Me.gbDVDProfiler.Text = "DVDProfiler"
        '
        'btnDVDProfiler
        '
        Me.btnDVDProfiler.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDVDProfiler.Location = New System.Drawing.Point(1, 21)
        Me.btnDVDProfiler.Name = "btnDVDProfiler"
        Me.btnDVDProfiler.Size = New System.Drawing.Size(96, 23)
        Me.btnDVDProfiler.TabIndex = 9
        Me.btnDVDProfiler.Text = "Select Movie"
        Me.btnDVDProfiler.UseVisualStyleBackColor = True
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(207, 21)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(100, 22)
        Me.txtLocation.TabIndex = 10
        '
        'txtSlot
        '
        Me.txtSlot.Location = New System.Drawing.Point(207, 52)
        Me.txtSlot.Name = "txtSlot"
        Me.txtSlot.Size = New System.Drawing.Size(100, 22)
        Me.txtSlot.TabIndex = 11
        '
        'txtCaseType
        '
        Me.txtCaseType.Location = New System.Drawing.Point(101, 21)
        Me.txtCaseType.Name = "txtCaseType"
        Me.txtCaseType.Size = New System.Drawing.Size(100, 22)
        Me.txtCaseType.TabIndex = 12
        '
        'txtMediaType
        '
        Me.txtMediaType.Location = New System.Drawing.Point(101, 52)
        Me.txtMediaType.Name = "txtMediaType"
        Me.txtMediaType.Size = New System.Drawing.Size(100, 22)
        Me.txtMediaType.TabIndex = 13
        '
        'btnSearchPMovie
        '
        Me.btnSearchPMovie.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchPMovie.Location = New System.Drawing.Point(1, 50)
        Me.btnSearchPMovie.Name = "btnSearchPMovie"
        Me.btnSearchPMovie.Size = New System.Drawing.Size(96, 23)
        Me.btnSearchPMovie.TabIndex = 14
        Me.btnSearchPMovie.Text = "Search"
        Me.btnSearchPMovie.UseVisualStyleBackColor = True
        '
        'dlgOfflineHolder
        '
        Me.AcceptButton = Me.Create_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.CLOSE_Button
        Me.ClientSize = New System.Drawing.Size(773, 594)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Create_Button)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.CLOSE_Button)
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
        Me.gbDVDProfiler.ResumeLayout(False)
        Me.gbDVDProfiler.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDVDProfiler As System.Windows.Forms.Button
    Friend WithEvents gbDVDProfiler As System.Windows.Forms.GroupBox
    Friend WithEvents txtMediaType As System.Windows.Forms.TextBox
    Friend WithEvents txtCaseType As System.Windows.Forms.TextBox
    Friend WithEvents txtSlot As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchPMovie As System.Windows.Forms.Button

#End Region 'Methods

End Class