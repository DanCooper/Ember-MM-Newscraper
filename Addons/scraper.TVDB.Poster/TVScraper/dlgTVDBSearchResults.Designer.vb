<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
        Partial Class dlgTVDBSearchResults
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTVDBSearchResults))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.pbBanner = New System.Windows.Forms.PictureBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblAiredHeader = New System.Windows.Forms.Label()
        Me.lblAired = New System.Windows.Forms.Label()
        Me.lblPlotHeader = New System.Windows.Forms.Label()
        Me.txtOutline = New System.Windows.Forms.TextBox()
        Me.lvSearchResults = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colLang = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colLev = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSLang = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopInfo = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnVerify = New System.Windows.Forms.Button()
        Me.chkManual = New System.Windows.Forms.CheckBox()
        Me.txtTVDBID = New System.Windows.Forms.TextBox()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.lblSearching = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tblMain.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.OK_Button.Enabled = False
        Me.OK_Button.Location = New System.Drawing.Point(512, 274)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(585, 274)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'pbBanner
        '
        Me.tblMain.SetColumnSpan(Me.pbBanner, 4)
        Me.pbBanner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pbBanner.Location = New System.Drawing.Point(352, 3)
        Me.pbBanner.MinimumSize = New System.Drawing.Size(300, 55)
        Me.pbBanner.Name = "pbBanner"
        Me.tblMain.SetRowSpan(Me.pbBanner, 2)
        Me.pbBanner.Size = New System.Drawing.Size(300, 55)
        Me.pbBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbBanner.TabIndex = 3
        Me.pbBanner.TabStop = False
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTitle.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.lblTitle, 4)
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(352, 68)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(39, 16)
        Me.lblTitle.TabIndex = 9
        Me.lblTitle.Text = "Title"
        '
        'lblAiredHeader
        '
        Me.lblAiredHeader.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAiredHeader.AutoSize = True
        Me.lblAiredHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAiredHeader.Location = New System.Drawing.Point(352, 94)
        Me.lblAiredHeader.Name = "lblAiredHeader"
        Me.lblAiredHeader.Size = New System.Drawing.Size(38, 13)
        Me.lblAiredHeader.TabIndex = 10
        Me.lblAiredHeader.Text = "Aired:"
        '
        'lblAired
        '
        Me.lblAired.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAired.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.lblAired, 2)
        Me.lblAired.Location = New System.Drawing.Point(441, 94)
        Me.lblAired.Name = "lblAired"
        Me.lblAired.Size = New System.Drawing.Size(63, 13)
        Me.lblAired.TabIndex = 12
        Me.lblAired.Text = "00/00/0000"
        '
        'lblPlotHeader
        '
        Me.lblPlotHeader.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPlotHeader.AutoSize = True
        Me.lblPlotHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlotHeader.Location = New System.Drawing.Point(352, 114)
        Me.lblPlotHeader.Name = "lblPlotHeader"
        Me.lblPlotHeader.Size = New System.Drawing.Size(83, 13)
        Me.lblPlotHeader.TabIndex = 13
        Me.lblPlotHeader.Text = "Plot Summary:"
        '
        'txtOutline
        '
        Me.txtOutline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tblMain.SetColumnSpan(Me.txtOutline, 4)
        Me.txtOutline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOutline.Location = New System.Drawing.Point(352, 134)
        Me.txtOutline.Multiline = True
        Me.txtOutline.Name = "txtOutline"
        Me.txtOutline.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutline.Size = New System.Drawing.Size(300, 134)
        Me.txtOutline.TabIndex = 14
        Me.txtOutline.TabStop = False
        '
        'lvSearchResults
        '
        Me.lvSearchResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colLang, Me.colLev, Me.colID, Me.colSLang})
        Me.tblMain.SetColumnSpan(Me.lvSearchResults, 5)
        Me.lvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSearchResults.FullRowSelect = True
        Me.lvSearchResults.HideSelection = False
        Me.lvSearchResults.Location = New System.Drawing.Point(3, 32)
        Me.lvSearchResults.MultiSelect = False
        Me.lvSearchResults.Name = "lvSearchResults"
        Me.tblMain.SetRowSpan(Me.lvSearchResults, 5)
        Me.lvSearchResults.Size = New System.Drawing.Size(343, 236)
        Me.lvSearchResults.TabIndex = 5
        Me.lvSearchResults.UseCompatibleStateImageBehavior = False
        Me.lvSearchResults.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Title"
        Me.colName.Width = 223
        '
        'colLang
        '
        Me.colLang.Text = "Language"
        Me.colLang.Width = 89
        '
        'colLev
        '
        Me.colLev.Width = 0
        '
        'colID
        '
        Me.colID.Width = 0
        '
        'colSLang
        '
        Me.colSLang.Width = 0
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblTopInfo)
        Me.pnlTop.Controls.Add(Me.lblTopTitle)
        Me.pnlTop.Controls.Add(Me.pbTopLogo)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(667, 64)
        Me.pnlTop.TabIndex = 2
        '
        'lblTopInfo
        '
        Me.lblTopInfo.AutoSize = True
        Me.lblTopInfo.BackColor = System.Drawing.Color.Transparent
        Me.lblTopInfo.ForeColor = System.Drawing.Color.White
        Me.lblTopInfo.Location = New System.Drawing.Point(61, 38)
        Me.lblTopInfo.Name = "lblTopInfo"
        Me.lblTopInfo.Size = New System.Drawing.Size(287, 13)
        Me.lblTopInfo.TabIndex = 1
        Me.lblTopInfo.Text = "View details of each result to find the proper TV show."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(216, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "TV Search Results"
        '
        'pbTopLogo
        '
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.Location = New System.Drawing.Point(7, 8)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.pbTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(323, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(23, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.tblMain.SetColumnSpan(Me.txtSearch, 4)
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(314, 22)
        Me.txtSearch.TabIndex = 3
        '
        'btnVerify
        '
        Me.tblMain.SetColumnSpan(Me.btnVerify, 2)
        Me.btnVerify.Enabled = False
        Me.btnVerify.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerify.Location = New System.Drawing.Point(271, 274)
        Me.btnVerify.Name = "btnVerify"
        Me.btnVerify.Size = New System.Drawing.Size(75, 22)
        Me.btnVerify.TabIndex = 8
        Me.btnVerify.Text = "Verify"
        Me.btnVerify.UseVisualStyleBackColor = True
        '
        'chkManual
        '
        Me.chkManual.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkManual.AutoSize = True
        Me.chkManual.Enabled = False
        Me.chkManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkManual.Location = New System.Drawing.Point(3, 277)
        Me.chkManual.Name = "chkManual"
        Me.chkManual.Size = New System.Drawing.Size(127, 17)
        Me.chkManual.TabIndex = 6
        Me.chkManual.Text = "Manual TVDB Entry:"
        Me.chkManual.UseVisualStyleBackColor = True
        '
        'txtTVDBID
        '
        Me.txtTVDBID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTVDBID.Enabled = False
        Me.txtTVDBID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVDBID.Location = New System.Drawing.Point(136, 274)
        Me.txtTVDBID.Name = "txtTVDBID"
        Me.txtTVDBID.Size = New System.Drawing.Size(100, 22)
        Me.txtTVDBID.TabIndex = 7
        '
        'tblMain
        '
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 10
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.txtSearch, 0, 0)
        Me.tblMain.Controls.Add(Me.lvSearchResults, 0, 1)
        Me.tblMain.Controls.Add(Me.txtTVDBID, 1, 6)
        Me.tblMain.Controls.Add(Me.lblTitle, 5, 2)
        Me.tblMain.Controls.Add(Me.chkManual, 0, 6)
        Me.tblMain.Controls.Add(Me.lblAiredHeader, 5, 3)
        Me.tblMain.Controls.Add(Me.pbBanner, 5, 0)
        Me.tblMain.Controls.Add(Me.txtOutline, 5, 5)
        Me.tblMain.Controls.Add(Me.lblPlotHeader, 5, 4)
        Me.tblMain.Controls.Add(Me.btnSearch, 4, 0)
        Me.tblMain.Controls.Add(Me.lblAired, 6, 3)
        Me.tblMain.Controls.Add(Me.Cancel_Button, 8, 6)
        Me.tblMain.Controls.Add(Me.OK_Button, 7, 6)
        Me.tblMain.Controls.Add(Me.btnVerify, 3, 6)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 8
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(667, 314)
        Me.tblMain.TabIndex = 15
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 64)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(667, 314)
        Me.pnlMain.TabIndex = 16
        '
        'StatusStrip
        '
        Me.StatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblSearching, Me.ProgressBar})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 378)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(667, 22)
        Me.StatusStrip.TabIndex = 17
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'lblSearching
        '
        Me.lblSearching.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearching.Name = "lblSearching"
        Me.lblSearching.Size = New System.Drawing.Size(146, 17)
        Me.lblSearching.Text = "Downloading show info..."
        Me.lblSearching.Visible = False
        '
        'ProgressBar
        '
        Me.ProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(100, 16)
        Me.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar.Visible = False
        '
        'dlgTVDBSearchResults
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(667, 400)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(649, 406)
        Me.Name = "dlgTVDBSearchResults"
        Me.Text = "TV Search Results"
        CType(Me.pbBanner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents pbBanner As System.Windows.Forms.PictureBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblAiredHeader As System.Windows.Forms.Label
    Friend WithEvents lblAired As System.Windows.Forms.Label
    Friend WithEvents lblPlotHeader As System.Windows.Forms.Label
    Friend WithEvents txtOutline As System.Windows.Forms.TextBox
    Friend WithEvents lvSearchResults As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colLang As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopInfo As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents colLev As System.Windows.Forms.ColumnHeader
    Friend WithEvents colID As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSLang As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnVerify As System.Windows.Forms.Button
    Friend WithEvents chkManual As System.Windows.Forms.CheckBox
    Friend WithEvents txtTVDBID As System.Windows.Forms.TextBox
    Friend WithEvents tblMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents lblSearching As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar

End Class