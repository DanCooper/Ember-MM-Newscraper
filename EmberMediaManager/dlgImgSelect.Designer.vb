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
        Me.pnlImgSelect = New System.Windows.Forms.Panel()
        Me.pnlImgSelectMain = New System.Windows.Forms.Panel()
        Me.pnlTopImages = New System.Windows.Forms.Panel()
        Me.pnlImgSelectBottom = New System.Windows.Forms.Panel()
        Me.tblImgSelectBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
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
        Me.cmnuSubImageRestoreOriginal = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSubImageRestorePreferred = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSubImageRemove = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuSubImageRemoveAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuListImage = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmnuListImageSelect = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuListImageSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmnuListImagePreview = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlImgSelect.SuspendLayout()
        Me.pnlImgSelectBottom.SuspendLayout()
        Me.tblImgSelectBottom.SuspendLayout()
        Me.pnlImgSelectLeft.SuspendLayout()
        Me.tblImgSelectLeft.SuspendLayout()
        Me.ssImgSelect.SuspendLayout()
        Me.pnlLoading.SuspendLayout()
        Me.cmnuTopImage.SuspendLayout()
        Me.cmnuSubImage.SuspendLayout()
        Me.cmnuListImage.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlImgSelect
        '
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectMain)
        Me.pnlImgSelect.Controls.Add(Me.pnlTopImages)
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectBottom)
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectLeft)
        Me.pnlImgSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImgSelect.Location = New System.Drawing.Point(0, 0)
        Me.pnlImgSelect.Name = "pnlImgSelect"
        Me.pnlImgSelect.Size = New System.Drawing.Size(1704, 774)
        Me.pnlImgSelect.TabIndex = 3
        '
        'pnlImgSelectMain
        '
        Me.pnlImgSelectMain.AutoScroll = True
        Me.pnlImgSelectMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImgSelectMain.Location = New System.Drawing.Point(211, 200)
        Me.pnlImgSelectMain.Name = "pnlImgSelectMain"
        Me.pnlImgSelectMain.Size = New System.Drawing.Size(1493, 545)
        Me.pnlImgSelectMain.TabIndex = 3
        '
        'pnlTopImages
        '
        Me.pnlTopImages.AutoScroll = True
        Me.pnlTopImages.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopImages.Location = New System.Drawing.Point(211, 0)
        Me.pnlTopImages.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlTopImages.Name = "pnlTopImages"
        Me.pnlTopImages.Size = New System.Drawing.Size(1493, 200)
        Me.pnlTopImages.TabIndex = 3
        '
        'pnlImgSelectBottom
        '
        Me.pnlImgSelectBottom.AutoSize = True
        Me.pnlImgSelectBottom.Controls.Add(Me.tblImgSelectBottom)
        Me.pnlImgSelectBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlImgSelectBottom.Location = New System.Drawing.Point(211, 745)
        Me.pnlImgSelectBottom.Name = "pnlImgSelectBottom"
        Me.pnlImgSelectBottom.Size = New System.Drawing.Size(1493, 29)
        Me.pnlImgSelectBottom.TabIndex = 1
        '
        'tblImgSelectBottom
        '
        Me.tblImgSelectBottom.AutoSize = True
        Me.tblImgSelectBottom.ColumnCount = 3
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImgSelectBottom.Controls.Add(Me.btnOK, 1, 0)
        Me.tblImgSelectBottom.Controls.Add(Me.btnCancel, 2, 0)
        Me.tblImgSelectBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImgSelectBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblImgSelectBottom.Name = "tblImgSelectBottom"
        Me.tblImgSelectBottom.RowCount = 1
        Me.tblImgSelectBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectBottom.Size = New System.Drawing.Size(1493, 29)
        Me.tblImgSelectBottom.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.Location = New System.Drawing.Point(1334, 3)
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
        Me.btnCancel.Location = New System.Drawing.Point(1415, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
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
        Me.btnSubImageUp.Location = New System.Drawing.Point(79, 748)
        Me.btnSubImageUp.Name = "btnSubImageUp"
        Me.btnSubImageUp.Size = New System.Drawing.Size(23, 23)
        Me.btnSubImageUp.TabIndex = 5
        Me.btnSubImageUp.UseVisualStyleBackColor = True
        '
        'btnSubImageDown
        '
        Me.btnSubImageDown.Enabled = False
        Me.btnSubImageDown.Image = CType(resources.GetObject("btnSubImageDown.Image"), System.Drawing.Image)
        Me.btnSubImageDown.Location = New System.Drawing.Point(108, 748)
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
        Me.ssImgSelect.Size = New System.Drawing.Size(1704, 22)
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
        Me.pnlLoading.Location = New System.Drawing.Point(700, 370)
        Me.pnlLoading.Name = "pnlLoading"
        Me.pnlLoading.Size = New System.Drawing.Size(280, 83)
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
        Me.cmnuSubImage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuSubImageRestoreOriginal, Me.cmnuSubImageRestorePreferred, Me.cmnuSubImageRemove, Me.cmnuSubImageRemoveAll})
        Me.cmnuSubImage.Name = "cmnuSubImage"
        Me.cmnuSubImage.Size = New System.Drawing.Size(165, 92)
        '
        'cmnuSubImageRestoreOriginal
        '
        Me.cmnuSubImageRestoreOriginal.Image = Global.Ember_Media_Manager.My.Resources.Resources.undo
        Me.cmnuSubImageRestoreOriginal.Name = "cmnuSubImageRestoreOriginal"
        Me.cmnuSubImageRestoreOriginal.Size = New System.Drawing.Size(164, 22)
        Me.cmnuSubImageRestoreOriginal.Text = "Restore Original"
        '
        'cmnuSubImageRestorePreferred
        '
        Me.cmnuSubImageRestorePreferred.Image = Global.Ember_Media_Manager.My.Resources.Resources.undo
        Me.cmnuSubImageRestorePreferred.Name = "cmnuSubImageRestorePreferred"
        Me.cmnuSubImageRestorePreferred.Size = New System.Drawing.Size(164, 22)
        Me.cmnuSubImageRestorePreferred.Text = "Restore Preferred"
        '
        'cmnuSubImageRemove
        '
        Me.cmnuSubImageRemove.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.cmnuSubImageRemove.Name = "cmnuSubImageRemove"
        Me.cmnuSubImageRemove.Size = New System.Drawing.Size(164, 22)
        Me.cmnuSubImageRemove.Text = "Remove"
        '
        'cmnuSubImageRemoveAll
        '
        Me.cmnuSubImageRemoveAll.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.cmnuSubImageRemoveAll.Name = "cmnuSubImageRemoveAll"
        Me.cmnuSubImageRemoveAll.Size = New System.Drawing.Size(164, 22)
        Me.cmnuSubImageRemoveAll.Text = "Remove All"
        '
        'cmnuListImage
        '
        Me.cmnuListImage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmnuListImageSelect, Me.cmnuListImageSelectAll, Me.cmnuListImagePreview})
        Me.cmnuListImage.Name = "cmnuListImage"
        Me.cmnuListImage.Size = New System.Drawing.Size(164, 70)
        '
        'cmnuListImageSelect
        '
        Me.cmnuListImageSelect.Image = Global.Ember_Media_Manager.My.Resources.Resources.menuAdd
        Me.cmnuListImageSelect.Name = "cmnuListImageSelect"
        Me.cmnuListImageSelect.Size = New System.Drawing.Size(163, 22)
        Me.cmnuListImageSelect.Text = "Select Image"
        '
        'cmnuListImageSelectAll
        '
        Me.cmnuListImageSelectAll.Image = Global.Ember_Media_Manager.My.Resources.Resources.menuAdd
        Me.cmnuListImageSelectAll.Name = "cmnuListImageSelectAll"
        Me.cmnuListImageSelectAll.Size = New System.Drawing.Size(163, 22)
        Me.cmnuListImageSelectAll.Text = "Select All Images"
        '
        'cmnuListImagePreview
        '
        Me.cmnuListImagePreview.Image = Global.Ember_Media_Manager.My.Resources.Resources.preview
        Me.cmnuListImagePreview.Name = "cmnuListImagePreview"
        Me.cmnuListImagePreview.Size = New System.Drawing.Size(163, 22)
        Me.cmnuListImagePreview.Text = "Preview"
        '
        'dlgImgSelect
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1704, 796)
        Me.Controls.Add(Me.pnlLoading)
        Me.Controls.Add(Me.pnlImgSelect)
        Me.Controls.Add(Me.ssImgSelect)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "dlgImgSelect"
        Me.Text = "Image Select"
        Me.pnlImgSelect.ResumeLayout(False)
        Me.pnlImgSelect.PerformLayout()
        Me.pnlImgSelectBottom.ResumeLayout(False)
        Me.pnlImgSelectBottom.PerformLayout()
        Me.tblImgSelectBottom.ResumeLayout(False)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlImgSelect As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectMain As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectLeft As System.Windows.Forms.Panel
    Friend WithEvents tblImgSelectLeft As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSubImages As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlTopImages As System.Windows.Forms.Panel
    Friend WithEvents tblImgSelectBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
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
    Friend WithEvents cmnuSubImageRemove As ToolStripMenuItem
    Friend WithEvents cmnuListImage As ContextMenuStrip
    Friend WithEvents cmnuListImageSelect As ToolStripMenuItem
    Friend WithEvents cmnuSubImageRemoveAll As ToolStripMenuItem
    Friend WithEvents cmnuSubImageRestoreOriginal As ToolStripMenuItem
    Friend WithEvents cmnuSubImageRestorePreferred As ToolStripMenuItem
    Friend WithEvents cmnuListImageSelectAll As ToolStripMenuItem
    Friend WithEvents cmnuListImagePreview As ToolStripMenuItem
End Class
