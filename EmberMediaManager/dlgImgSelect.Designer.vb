<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgImgSelect
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgImgSelect))
        Me.btnRemoveSubImage = New System.Windows.Forms.Button()
        Me.btnRestoreSubImage = New System.Windows.Forms.Button()
        Me.pnlImgSelect = New System.Windows.Forms.Panel()
        Me.pnlImgSelectMain = New System.Windows.Forms.Panel()
        Me.pnlImgSelectTop = New System.Windows.Forms.Panel()
        Me.pnlTopImages = New System.Windows.Forms.Panel()
        Me.pnlImgSelectBottom = New System.Windows.Forms.Panel()
        Me.tblImgSelectBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.tblFilter = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSelectButtons = New System.Windows.Forms.Panel()
        Me.tblSelectButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnSelectNone = New System.Windows.Forms.Button()
        Me.pnlImgSelectLeft = New System.Windows.Forms.Panel()
        Me.tblImgSelectLeft = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSubImages = New System.Windows.Forms.Panel()
        Me.btnSubImageUp = New System.Windows.Forms.Button()
        Me.btnSubImageDown = New System.Windows.Forms.Button()
        Me.btnExtrafanarts = New System.Windows.Forms.Button()
        Me.btnExtrathumbs = New System.Windows.Forms.Button()
        Me.btnSeasonBanner = New System.Windows.Forms.Button()
        Me.btnSeasonFanart = New System.Windows.Forms.Button()
        Me.btnSeasonLandscape = New System.Windows.Forms.Button()
        Me.btnSeasonPoster = New System.Windows.Forms.Button()
        Me.ssImgSelect = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.tmrReorderMainList = New System.Windows.Forms.Timer(Me.components)
        Me.pnlLoading = New System.Windows.Forms.Panel()
        Me.pbLoading = New System.Windows.Forms.ProgressBar()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.cmnuTopImage = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuTopImageRestoreOriginal = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTopImageRestorePreferred = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuTopImageRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSubImage = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SubToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuListImage = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tblImgSelectTop = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlImgSelect.SuspendLayout()
        Me.pnlImgSelectTop.SuspendLayout()
        Me.pnlImgSelectBottom.SuspendLayout()
        Me.tblImgSelectBottom.SuspendLayout()
        Me.pnlFilter.SuspendLayout()
        Me.pnlSelectButtons.SuspendLayout()
        Me.tblSelectButtons.SuspendLayout()
        Me.pnlImgSelectLeft.SuspendLayout()
        Me.tblImgSelectLeft.SuspendLayout()
        Me.ssImgSelect.SuspendLayout()
        Me.pnlLoading.SuspendLayout()
        Me.cmnuTopImage.SuspendLayout()
        Me.cmnuSubImage.SuspendLayout()
        Me.cmnuListImage.SuspendLayout()
        Me.tblImgSelectTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRemoveSubImage
        '
        Me.btnRemoveSubImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveSubImage.Enabled = False
        Me.btnRemoveSubImage.Image = CType(resources.GetObject("btnRemoveSubImage.Image"), System.Drawing.Image)
        Me.btnRemoveSubImage.Location = New System.Drawing.Point(165, 748)
        Me.btnRemoveSubImage.Margin = New System.Windows.Forms.Padding(3, 3, 23, 3)
        Me.btnRemoveSubImage.Name = "btnRemoveSubImage"
        Me.btnRemoveSubImage.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveSubImage.TabIndex = 2
        Me.btnRemoveSubImage.UseVisualStyleBackColor = True
        '
        'btnRestoreSubImage
        '
        Me.btnRestoreSubImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRestoreSubImage.Enabled = False
        Me.btnRestoreSubImage.Image = CType(resources.GetObject("btnRestoreSubImage.Image"), System.Drawing.Image)
        Me.btnRestoreSubImage.Location = New System.Drawing.Point(6, 748)
        Me.btnRestoreSubImage.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.btnRestoreSubImage.Name = "btnRestoreSubImage"
        Me.btnRestoreSubImage.Size = New System.Drawing.Size(23, 23)
        Me.btnRestoreSubImage.TabIndex = 1
        Me.btnRestoreSubImage.UseVisualStyleBackColor = True
        '
        'pnlImgSelect
        '
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectMain)
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectTop)
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectBottom)
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectLeft)
        Me.pnlImgSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImgSelect.Location = New System.Drawing.Point(0, 0)
        Me.pnlImgSelect.Name = "pnlImgSelect"
        Me.pnlImgSelect.Size = New System.Drawing.Size(1534, 774)
        Me.pnlImgSelect.TabIndex = 3
        '
        'pnlImgSelectMain
        '
        Me.pnlImgSelectMain.AutoScroll = True
        Me.pnlImgSelectMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImgSelectMain.Location = New System.Drawing.Point(211, 203)
        Me.pnlImgSelectMain.Name = "pnlImgSelectMain"
        Me.pnlImgSelectMain.Size = New System.Drawing.Size(1323, 471)
        Me.pnlImgSelectMain.TabIndex = 3
        '
        'pnlImgSelectTop
        '
        Me.pnlImgSelectTop.AutoSize = True
        Me.pnlImgSelectTop.Controls.Add(Me.tblImgSelectTop)
        Me.pnlImgSelectTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlImgSelectTop.Location = New System.Drawing.Point(211, 0)
        Me.pnlImgSelectTop.Name = "pnlImgSelectTop"
        Me.pnlImgSelectTop.Size = New System.Drawing.Size(1323, 203)
        Me.pnlImgSelectTop.TabIndex = 0
        '
        'pnlTopImages
        '
        Me.pnlTopImages.AutoScroll = True
        Me.pnlTopImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopImages.Location = New System.Drawing.Point(0, 3)
        Me.pnlTopImages.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlTopImages.Name = "pnlTopImages"
        Me.pnlTopImages.Size = New System.Drawing.Size(1320, 197)
        Me.pnlTopImages.TabIndex = 3
        '
        'pnlImgSelectBottom
        '
        Me.pnlImgSelectBottom.Controls.Add(Me.tblImgSelectBottom)
        Me.pnlImgSelectBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlImgSelectBottom.Location = New System.Drawing.Point(211, 674)
        Me.pnlImgSelectBottom.Name = "pnlImgSelectBottom"
        Me.pnlImgSelectBottom.Size = New System.Drawing.Size(1323, 100)
        Me.pnlImgSelectBottom.TabIndex = 1
        '
        'tblImgSelectBottom
        '
        Me.tblImgSelectBottom.ColumnCount = 5
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.Controls.Add(Me.btnOK, 3, 0)
        Me.tblImgSelectBottom.Controls.Add(Me.btnCancel, 4, 0)
        Me.tblImgSelectBottom.Controls.Add(Me.pnlFilter, 0, 0)
        Me.tblImgSelectBottom.Controls.Add(Me.pnlSelectButtons, 1, 0)
        Me.tblImgSelectBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImgSelectBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblImgSelectBottom.Name = "tblImgSelectBottom"
        Me.tblImgSelectBottom.RowCount = 1
        Me.tblImgSelectBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectBottom.Size = New System.Drawing.Size(1323, 100)
        Me.tblImgSelectBottom.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.Location = New System.Drawing.Point(1164, 41)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(1245, 41)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pnlFilter
        '
        Me.pnlFilter.Controls.Add(Me.tblFilter)
        Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilter.Location = New System.Drawing.Point(3, 3)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(200, 100)
        Me.pnlFilter.TabIndex = 2
        '
        'tblFilter
        '
        Me.tblFilter.ColumnCount = 2
        Me.tblFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilter.Location = New System.Drawing.Point(0, 0)
        Me.tblFilter.Name = "tblFilter"
        Me.tblFilter.RowCount = 2
        Me.tblFilter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFilter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFilter.Size = New System.Drawing.Size(200, 100)
        Me.tblFilter.TabIndex = 0
        '
        'pnlSelectButtons
        '
        Me.pnlSelectButtons.AutoSize = True
        Me.pnlSelectButtons.Controls.Add(Me.tblSelectButtons)
        Me.pnlSelectButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelectButtons.Location = New System.Drawing.Point(209, 3)
        Me.pnlSelectButtons.Name = "pnlSelectButtons"
        Me.pnlSelectButtons.Size = New System.Drawing.Size(82, 100)
        Me.pnlSelectButtons.TabIndex = 3
        '
        'tblSelectButtons
        '
        Me.tblSelectButtons.AutoSize = True
        Me.tblSelectButtons.ColumnCount = 1
        Me.tblSelectButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSelectButtons.Controls.Add(Me.btnSelectAll, 0, 1)
        Me.tblSelectButtons.Controls.Add(Me.btnSelectNone, 0, 2)
        Me.tblSelectButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSelectButtons.Location = New System.Drawing.Point(0, 0)
        Me.tblSelectButtons.Name = "tblSelectButtons"
        Me.tblSelectButtons.RowCount = 4
        Me.tblSelectButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSelectButtons.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSelectButtons.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSelectButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSelectButtons.Size = New System.Drawing.Size(82, 100)
        Me.tblSelectButtons.TabIndex = 0
        '
        'btnSelectAll
        '
        Me.btnSelectAll.AutoSize = True
        Me.btnSelectAll.Enabled = False
        Me.btnSelectAll.Location = New System.Drawing.Point(3, 24)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectAll.TabIndex = 0
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnSelectNone
        '
        Me.btnSelectNone.AutoSize = True
        Me.btnSelectNone.Enabled = False
        Me.btnSelectNone.Location = New System.Drawing.Point(3, 53)
        Me.btnSelectNone.Name = "btnSelectNone"
        Me.btnSelectNone.Size = New System.Drawing.Size(76, 23)
        Me.btnSelectNone.TabIndex = 1
        Me.btnSelectNone.Text = "Select None"
        Me.btnSelectNone.UseVisualStyleBackColor = True
        '
        'pnlImgSelectLeft
        '
        Me.pnlImgSelectLeft.AutoSize = True
        Me.pnlImgSelectLeft.Controls.Add(Me.tblImgSelectLeft)
        Me.pnlImgSelectLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlImgSelectLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlImgSelectLeft.Name = "pnlImgSelectLeft"
        Me.pnlImgSelectLeft.Size = New System.Drawing.Size(211, 774)
        Me.pnlImgSelectLeft.TabIndex = 2
        '
        'tblImgSelectLeft
        '
        Me.tblImgSelectLeft.AutoSize = True
        Me.tblImgSelectLeft.ColumnCount = 6
        Me.tblImgSelectLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblImgSelectLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblImgSelectLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectLeft.Controls.Add(Me.btnRestoreSubImage, 0, 7)
        Me.tblImgSelectLeft.Controls.Add(Me.btnRemoveSubImage, 5, 7)
        Me.tblImgSelectLeft.Controls.Add(Me.pnlSubImages, 0, 6)
        Me.tblImgSelectLeft.Controls.Add(Me.btnSubImageUp, 2, 7)
        Me.tblImgSelectLeft.Controls.Add(Me.btnSubImageDown, 3, 7)
        Me.tblImgSelectLeft.Controls.Add(Me.btnExtrafanarts, 0, 0)
        Me.tblImgSelectLeft.Controls.Add(Me.btnExtrathumbs, 0, 1)
        Me.tblImgSelectLeft.Controls.Add(Me.btnSeasonBanner, 0, 2)
        Me.tblImgSelectLeft.Controls.Add(Me.btnSeasonFanart, 0, 3)
        Me.tblImgSelectLeft.Controls.Add(Me.btnSeasonLandscape, 0, 4)
        Me.tblImgSelectLeft.Controls.Add(Me.btnSeasonPoster, 0, 5)
        Me.tblImgSelectLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImgSelectLeft.Location = New System.Drawing.Point(0, 0)
        Me.tblImgSelectLeft.Name = "tblImgSelectLeft"
        Me.tblImgSelectLeft.RowCount = 8
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImgSelectLeft.Size = New System.Drawing.Size(211, 774)
        Me.tblImgSelectLeft.TabIndex = 0
        '
        'pnlSubImages
        '
        Me.pnlSubImages.AutoScroll = True
        Me.pnlSubImages.AutoSize = True
        Me.tblImgSelectLeft.SetColumnSpan(Me.pnlSubImages, 6)
        Me.pnlSubImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSubImages.Location = New System.Drawing.Point(3, 177)
        Me.pnlSubImages.Name = "pnlSubImages"
        Me.pnlSubImages.Size = New System.Drawing.Size(205, 565)
        Me.pnlSubImages.TabIndex = 4
        '
        'btnSubImageUp
        '
        Me.btnSubImageUp.Enabled = False
        Me.btnSubImageUp.Image = CType(resources.GetObject("btnSubImageUp.Image"), System.Drawing.Image)
        Me.btnSubImageUp.Location = New System.Drawing.Point(71, 748)
        Me.btnSubImageUp.Name = "btnSubImageUp"
        Me.btnSubImageUp.Size = New System.Drawing.Size(23, 23)
        Me.btnSubImageUp.TabIndex = 5
        Me.btnSubImageUp.UseVisualStyleBackColor = True
        '
        'btnSubImageDown
        '
        Me.btnSubImageDown.Enabled = False
        Me.btnSubImageDown.Image = CType(resources.GetObject("btnSubImageDown.Image"), System.Drawing.Image)
        Me.btnSubImageDown.Location = New System.Drawing.Point(100, 748)
        Me.btnSubImageDown.Name = "btnSubImageDown"
        Me.btnSubImageDown.Size = New System.Drawing.Size(23, 23)
        Me.btnSubImageDown.TabIndex = 6
        Me.btnSubImageDown.UseVisualStyleBackColor = True
        '
        'btnExtrafanarts
        '
        Me.tblImgSelectLeft.SetColumnSpan(Me.btnExtrafanarts, 6)
        Me.btnExtrafanarts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExtrafanarts.Enabled = False
        Me.btnExtrafanarts.Location = New System.Drawing.Point(3, 3)
        Me.btnExtrafanarts.Name = "btnExtrafanarts"
        Me.btnExtrafanarts.Size = New System.Drawing.Size(205, 23)
        Me.btnExtrafanarts.TabIndex = 7
        Me.btnExtrafanarts.Text = "Extrafanarts"
        Me.btnExtrafanarts.UseVisualStyleBackColor = True
        '
        'btnExtrathumbs
        '
        Me.tblImgSelectLeft.SetColumnSpan(Me.btnExtrathumbs, 6)
        Me.btnExtrathumbs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExtrathumbs.Enabled = False
        Me.btnExtrathumbs.Location = New System.Drawing.Point(3, 32)
        Me.btnExtrathumbs.Name = "btnExtrathumbs"
        Me.btnExtrathumbs.Size = New System.Drawing.Size(205, 23)
        Me.btnExtrathumbs.TabIndex = 8
        Me.btnExtrathumbs.Text = "Extrathumbs"
        Me.btnExtrathumbs.UseVisualStyleBackColor = True
        '
        'btnSeasonBanner
        '
        Me.tblImgSelectLeft.SetColumnSpan(Me.btnSeasonBanner, 6)
        Me.btnSeasonBanner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSeasonBanner.Enabled = False
        Me.btnSeasonBanner.Location = New System.Drawing.Point(3, 61)
        Me.btnSeasonBanner.Name = "btnSeasonBanner"
        Me.btnSeasonBanner.Size = New System.Drawing.Size(205, 23)
        Me.btnSeasonBanner.TabIndex = 9
        Me.btnSeasonBanner.Text = "Season Banner"
        Me.btnSeasonBanner.UseVisualStyleBackColor = True
        '
        'btnSeasonFanart
        '
        Me.tblImgSelectLeft.SetColumnSpan(Me.btnSeasonFanart, 6)
        Me.btnSeasonFanart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSeasonFanart.Enabled = False
        Me.btnSeasonFanart.Location = New System.Drawing.Point(3, 90)
        Me.btnSeasonFanart.Name = "btnSeasonFanart"
        Me.btnSeasonFanart.Size = New System.Drawing.Size(205, 23)
        Me.btnSeasonFanart.TabIndex = 10
        Me.btnSeasonFanart.Text = "Season Fanrt"
        Me.btnSeasonFanart.UseVisualStyleBackColor = True
        '
        'btnSeasonLandscape
        '
        Me.tblImgSelectLeft.SetColumnSpan(Me.btnSeasonLandscape, 6)
        Me.btnSeasonLandscape.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSeasonLandscape.Enabled = False
        Me.btnSeasonLandscape.Location = New System.Drawing.Point(3, 119)
        Me.btnSeasonLandscape.Name = "btnSeasonLandscape"
        Me.btnSeasonLandscape.Size = New System.Drawing.Size(205, 23)
        Me.btnSeasonLandscape.TabIndex = 11
        Me.btnSeasonLandscape.Text = "Season Landscape"
        Me.btnSeasonLandscape.UseVisualStyleBackColor = True
        '
        'btnSeasonPoster
        '
        Me.tblImgSelectLeft.SetColumnSpan(Me.btnSeasonPoster, 6)
        Me.btnSeasonPoster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSeasonPoster.Enabled = False
        Me.btnSeasonPoster.Location = New System.Drawing.Point(3, 148)
        Me.btnSeasonPoster.Name = "btnSeasonPoster"
        Me.btnSeasonPoster.Size = New System.Drawing.Size(205, 23)
        Me.btnSeasonPoster.TabIndex = 12
        Me.btnSeasonPoster.Text = "Season Poster"
        Me.btnSeasonPoster.UseVisualStyleBackColor = True
        '
        'ssImgSelect
        '
        Me.ssImgSelect.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.pbStatus})
        Me.ssImgSelect.Location = New System.Drawing.Point(0, 774)
        Me.ssImgSelect.Name = "ssImgSelect"
        Me.ssImgSelect.Size = New System.Drawing.Size(1534, 22)
        Me.ssImgSelect.TabIndex = 4
        Me.ssImgSelect.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(1417, 17)
        Me.lblStatus.Spring = True
        Me.lblStatus.Text = "Downloading"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStatus.Visible = False
        '
        'pbStatus
        '
        Me.pbStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(100, 16)
        Me.pbStatus.Visible = False
        '
        'tmrReorderMainList
        '
        '
        'pnlLoading
        '
        Me.pnlLoading.BackColor = System.Drawing.Color.White
        Me.pnlLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLoading.Controls.Add(Me.pbLoading)
        Me.pnlLoading.Controls.Add(Me.lblLoading)
        Me.pnlLoading.Location = New System.Drawing.Point(595, 368)
        Me.pnlLoading.Name = "pnlLoading"
        Me.pnlLoading.Size = New System.Drawing.Size(281, 83)
        Me.pnlLoading.TabIndex = 0
        Me.pnlLoading.Visible = False
        '
        'pbLoading
        '
        Me.pbLoading.Location = New System.Drawing.Point(3, 51)
        Me.pbLoading.Name = "pbLoading"
        Me.pbLoading.Size = New System.Drawing.Size(273, 23)
        Me.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbLoading.TabIndex = 1
        '
        'lblLoading
        '
        Me.lblLoading.AutoSize = True
        Me.lblLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoading.Location = New System.Drawing.Point(3, 17)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(136, 13)
        Me.lblLoading.TabIndex = 0
        Me.lblLoading.Text = "Downloading Images..."
        '
        'cmnuTopImage
        '
        Me.cmnuTopImage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuTopImageRestoreOriginal, Me.cmnuTopImageRestorePreferred, Me.cmnuTopImageRemove})
        Me.cmnuTopImage.Name = "cmnuImage"
        Me.cmnuTopImage.Size = New System.Drawing.Size(165, 70)
        '
        'cmnuTopImageRestoreOriginal
        '
        Me.cmnuTopImageRestoreOriginal.Image = Global.Ember_Media_Manager.My.Resources.Resources.undo
        Me.cmnuTopImageRestoreOriginal.Name = "cmnuTopImageRestoreOriginal"
        Me.cmnuTopImageRestoreOriginal.Size = New System.Drawing.Size(164, 22)
        Me.cmnuTopImageRestoreOriginal.Text = "Restore Original"
        '
        'cmnuTopImageRestorePreferred
        '
        Me.cmnuTopImageRestorePreferred.Image = Global.Ember_Media_Manager.My.Resources.Resources.undo
        Me.cmnuTopImageRestorePreferred.Name = "cmnuTopImageRestorePreferred"
        Me.cmnuTopImageRestorePreferred.Size = New System.Drawing.Size(164, 22)
        Me.cmnuTopImageRestorePreferred.Text = "Restore Preferred"
        '
        'cmnuTopImageRemove
        '
        Me.cmnuTopImageRemove.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.cmnuTopImageRemove.Name = "cmnuTopImageRemove"
        Me.cmnuTopImageRemove.Size = New System.Drawing.Size(164, 22)
        Me.cmnuTopImageRemove.Text = "Remove"
        '
        'cmnuSubImage
        '
        Me.cmnuSubImage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SubToolStripMenuItem})
        Me.cmnuSubImage.Name = "cmnuSubImage"
        Me.cmnuSubImage.Size = New System.Drawing.Size(95, 26)
        '
        'SubToolStripMenuItem
        '
        Me.SubToolStripMenuItem.Name = "SubToolStripMenuItem"
        Me.SubToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.SubToolStripMenuItem.Text = "Sub"
        '
        'cmnuListImage
        '
        Me.cmnuListImage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ListToolStripMenuItem})
        Me.cmnuListImage.Name = "cmnuListImage"
        Me.cmnuListImage.Size = New System.Drawing.Size(93, 26)
        '
        'ListToolStripMenuItem
        '
        Me.ListToolStripMenuItem.Name = "ListToolStripMenuItem"
        Me.ListToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.ListToolStripMenuItem.Text = "List"
        '
        'tblImgSelectTop
        '
        Me.tblImgSelectTop.AutoSize = True
        Me.tblImgSelectTop.ColumnCount = 1
        Me.tblImgSelectTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImgSelectTop.Controls.Add(Me.pnlTopImages, 0, 0)
        Me.tblImgSelectTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImgSelectTop.Location = New System.Drawing.Point(0, 0)
        Me.tblImgSelectTop.Name = "tblImgSelectTop"
        Me.tblImgSelectTop.RowCount = 1
        Me.tblImgSelectTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImgSelectTop.Size = New System.Drawing.Size(1323, 203)
        Me.tblImgSelectTop.TabIndex = 0
        '
        'dlgImgSelect
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1534, 796)
        Me.Controls.Add(Me.pnlLoading)
        Me.Controls.Add(Me.pnlImgSelect)
        Me.Controls.Add(Me.ssImgSelect)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "dlgImgSelect"
        Me.Text = "Image Select"
        Me.pnlImgSelect.ResumeLayout(False)
        Me.pnlImgSelect.PerformLayout()
        Me.pnlImgSelectTop.ResumeLayout(False)
        Me.pnlImgSelectTop.PerformLayout()
        Me.pnlImgSelectBottom.ResumeLayout(False)
        Me.tblImgSelectBottom.ResumeLayout(False)
        Me.tblImgSelectBottom.PerformLayout()
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlSelectButtons.ResumeLayout(False)
        Me.pnlSelectButtons.PerformLayout()
        Me.tblSelectButtons.ResumeLayout(False)
        Me.tblSelectButtons.PerformLayout()
        Me.pnlImgSelectLeft.ResumeLayout(False)
        Me.pnlImgSelectLeft.PerformLayout()
        Me.tblImgSelectLeft.ResumeLayout(False)
        Me.tblImgSelectLeft.PerformLayout()
        Me.ssImgSelect.ResumeLayout(False)
        Me.ssImgSelect.PerformLayout()
        Me.pnlLoading.ResumeLayout(False)
        Me.pnlLoading.PerformLayout()
        Me.cmnuTopImage.ResumeLayout(False)
        Me.cmnuSubImage.ResumeLayout(False)
        Me.cmnuListImage.ResumeLayout(False)
        Me.tblImgSelectTop.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRemoveSubImage As System.Windows.Forms.Button
    Friend WithEvents btnRestoreSubImage As System.Windows.Forms.Button
    Friend WithEvents pnlImgSelect As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectMain As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectLeft As System.Windows.Forms.Panel
    Friend WithEvents tblImgSelectLeft As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSubImages As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectTop As System.Windows.Forms.Panel
    Friend WithEvents pnlTopImages As System.Windows.Forms.Panel
    Friend WithEvents tblImgSelectBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents tblFilter As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSelectButtons As System.Windows.Forms.Panel
    Friend WithEvents tblSelectButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectNone As System.Windows.Forms.Button
    Friend WithEvents ssImgSelect As System.Windows.Forms.StatusStrip
    Friend WithEvents tmrReorderMainList As System.Windows.Forms.Timer
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbStatus As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents btnSubImageUp As System.Windows.Forms.Button
    Friend WithEvents btnSubImageDown As System.Windows.Forms.Button
    Friend WithEvents pnlLoading As System.Windows.Forms.Panel
    Friend WithEvents pbLoading As System.Windows.Forms.ProgressBar
    Friend WithEvents lblLoading As System.Windows.Forms.Label
    Friend WithEvents btnExtrafanarts As Button
    Friend WithEvents btnExtrathumbs As Button
    Friend WithEvents btnSeasonBanner As Button
    Friend WithEvents btnSeasonFanart As Button
    Friend WithEvents btnSeasonLandscape As Button
    Friend WithEvents btnSeasonPoster As Button
    Friend WithEvents cmnuTopImage As ContextMenuStrip
    Friend WithEvents cmnuTopImageRestoreOriginal As ToolStripMenuItem
    Friend WithEvents cmnuTopImageRestorePreferred As ToolStripMenuItem
    Friend WithEvents cmnuTopImageRemove As ToolStripMenuItem
    Friend WithEvents cmnuSubImage As ContextMenuStrip
    Friend WithEvents SubToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cmnuListImage As ContextMenuStrip
    Friend WithEvents ListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tblImgSelectTop As TableLayoutPanel
End Class
