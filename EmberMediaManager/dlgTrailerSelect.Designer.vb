<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTrailerSelect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTrailerSelect))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnTrailerScrape = New System.Windows.Forms.Button()
        Me.lvTrailers = New System.Windows.Forms.ListView()
        Me.colNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colURL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colWebURL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDuration = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colVideoQuality = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colVideoType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSource = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colScraper = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gbYouTubeSearch = New System.Windows.Forms.GroupBox()
        Me.tblYouTubeSearch = New System.Windows.Forms.TableLayoutPanel()
        Me.txtYouTubeSearch = New System.Windows.Forms.TextBox()
        Me.btnYouTubeSearch = New System.Windows.Forms.Button()
        Me.gbManualTrailer = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnBrowseLocalTrailer = New System.Windows.Forms.Button()
        Me.btnClearManualTrailerLink = New System.Windows.Forms.Button()
        Me.txtLocalTrailer = New System.Windows.Forms.TextBox()
        Me.lblManualTrailerLink = New System.Windows.Forms.Label()
        Me.lblLocalTrailer = New System.Windows.Forms.Label()
        Me.txtManualTrailerLink = New System.Windows.Forms.TextBox()
        Me.ofdTrailer = New System.Windows.Forms.OpenFileDialog()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.btnPlayInBrowser = New System.Windows.Forms.Button()
        Me.pblBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.pbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.gbYouTubeSearch.SuspendLayout()
        Me.tblYouTubeSearch.SuspendLayout()
        Me.gbManualTrailer.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        Me.pblBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Enabled = False
        Me.btnOK.Location = New System.Drawing.Point(635, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(120, 23)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "Download"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(761, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        '
        'btnTrailerScrape
        '
        Me.btnTrailerScrape.AutoSize = True
        Me.btnTrailerScrape.Location = New System.Drawing.Point(3, 363)
        Me.btnTrailerScrape.Name = "btnTrailerScrape"
        Me.btnTrailerScrape.Size = New System.Drawing.Size(152, 23)
        Me.btnTrailerScrape.TabIndex = 5
        Me.btnTrailerScrape.Text = "Scrape Trailers"
        Me.btnTrailerScrape.UseVisualStyleBackColor = True
        '
        'lvTrailers
        '
        Me.lvTrailers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvTrailers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNumber, Me.colURL, Me.colWebURL, Me.colDescription, Me.colDuration, Me.colVideoQuality, Me.colVideoType, Me.colSource, Me.colScraper})
        Me.tblMain.SetColumnSpan(Me.lvTrailers, 2)
        Me.lvTrailers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvTrailers.HideSelection = False
        Me.lvTrailers.Location = New System.Drawing.Point(3, 3)
        Me.lvTrailers.Name = "lvTrailers"
        Me.lvTrailers.Size = New System.Drawing.Size(878, 354)
        Me.lvTrailers.TabIndex = 4
        Me.lvTrailers.UseCompatibleStateImageBehavior = False
        Me.lvTrailers.View = System.Windows.Forms.View.Details
        '
        'colNumber
        '
        Me.colNumber.Text = "#"
        Me.colNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colNumber.Width = 20
        '
        'colURL
        '
        Me.colURL.Text = "URL"
        Me.colURL.Width = 0
        '
        'colWebURL
        '
        Me.colWebURL.Text = "WebURL"
        Me.colWebURL.Width = 0
        '
        'colDescription
        '
        Me.colDescription.Text = "Description"
        Me.colDescription.Width = 400
        '
        'colDuration
        '
        Me.colDuration.Text = "Duration"
        '
        'colVideoQuality
        '
        Me.colVideoQuality.Text = "Quality"
        '
        'colVideoType
        '
        Me.colVideoType.Text = "Type"
        '
        'colSource
        '
        Me.colSource.Text = "Source"
        Me.colSource.Width = 120
        '
        'colScraper
        '
        Me.colScraper.Text = "Scraper"
        Me.colScraper.Width = 120
        '
        'gbYouTubeSearch
        '
        Me.gbYouTubeSearch.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.gbYouTubeSearch, 2)
        Me.gbYouTubeSearch.Controls.Add(Me.tblYouTubeSearch)
        Me.gbYouTubeSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbYouTubeSearch.Location = New System.Drawing.Point(3, 412)
        Me.gbYouTubeSearch.Name = "gbYouTubeSearch"
        Me.gbYouTubeSearch.Size = New System.Drawing.Size(878, 50)
        Me.gbYouTubeSearch.TabIndex = 9
        Me.gbYouTubeSearch.TabStop = False
        Me.gbYouTubeSearch.Text = "Search On YouTube"
        '
        'tblYouTubeSearch
        '
        Me.tblYouTubeSearch.AutoSize = True
        Me.tblYouTubeSearch.ColumnCount = 2
        Me.tblYouTubeSearch.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblYouTubeSearch.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblYouTubeSearch.Controls.Add(Me.txtYouTubeSearch, 0, 0)
        Me.tblYouTubeSearch.Controls.Add(Me.btnYouTubeSearch, 1, 0)
        Me.tblYouTubeSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblYouTubeSearch.Location = New System.Drawing.Point(3, 18)
        Me.tblYouTubeSearch.Name = "tblYouTubeSearch"
        Me.tblYouTubeSearch.RowCount = 1
        Me.tblYouTubeSearch.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblYouTubeSearch.Size = New System.Drawing.Size(872, 29)
        Me.tblYouTubeSearch.TabIndex = 2
        '
        'txtYouTubeSearch
        '
        Me.txtYouTubeSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtYouTubeSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYouTubeSearch.Location = New System.Drawing.Point(3, 3)
        Me.txtYouTubeSearch.Name = "txtYouTubeSearch"
        Me.txtYouTubeSearch.Size = New System.Drawing.Size(785, 22)
        Me.txtYouTubeSearch.TabIndex = 0
        '
        'btnYouTubeSearch
        '
        Me.btnYouTubeSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnYouTubeSearch.Location = New System.Drawing.Point(794, 3)
        Me.btnYouTubeSearch.Name = "btnYouTubeSearch"
        Me.btnYouTubeSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnYouTubeSearch.TabIndex = 1
        Me.btnYouTubeSearch.Text = "Search"
        Me.btnYouTubeSearch.UseVisualStyleBackColor = True
        '
        'gbManualTrailer
        '
        Me.gbManualTrailer.AutoSize = True
        Me.tblMain.SetColumnSpan(Me.gbManualTrailer, 2)
        Me.gbManualTrailer.Controls.Add(Me.TableLayoutPanel1)
        Me.gbManualTrailer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbManualTrailer.Location = New System.Drawing.Point(3, 488)
        Me.gbManualTrailer.Name = "gbManualTrailer"
        Me.gbManualTrailer.Size = New System.Drawing.Size(878, 119)
        Me.gbManualTrailer.TabIndex = 3
        Me.gbManualTrailer.TabStop = False
        Me.gbManualTrailer.Text = "Manual Trailer Entry"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.btnBrowseLocalTrailer, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.btnClearManualTrailerLink, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtLocalTrailer, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.lblManualTrailerLink, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblLocalTrailer, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtManualTrailerLink, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(872, 98)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'btnBrowseLocalTrailer
        '
        Me.btnBrowseLocalTrailer.Location = New System.Drawing.Point(844, 72)
        Me.btnBrowseLocalTrailer.Name = "btnBrowseLocalTrailer"
        Me.btnBrowseLocalTrailer.Size = New System.Drawing.Size(25, 23)
        Me.btnBrowseLocalTrailer.TabIndex = 4
        Me.btnBrowseLocalTrailer.Text = "..."
        Me.btnBrowseLocalTrailer.UseVisualStyleBackColor = True
        '
        'btnClearManualTrailerLink
        '
        Me.btnClearManualTrailerLink.Image = Global.Ember_Media_Manager.My.Resources.Resources.invalid
        Me.btnClearManualTrailerLink.Location = New System.Drawing.Point(844, 23)
        Me.btnClearManualTrailerLink.Name = "btnClearManualTrailerLink"
        Me.btnClearManualTrailerLink.Size = New System.Drawing.Size(25, 23)
        Me.btnClearManualTrailerLink.TabIndex = 5
        Me.btnClearManualTrailerLink.UseVisualStyleBackColor = True
        '
        'txtLocalTrailer
        '
        Me.txtLocalTrailer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocalTrailer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLocalTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalTrailer.Location = New System.Drawing.Point(3, 72)
        Me.txtLocalTrailer.Name = "txtLocalTrailer"
        Me.txtLocalTrailer.Size = New System.Drawing.Size(835, 22)
        Me.txtLocalTrailer.TabIndex = 3
        '
        'lblManualTrailerLink
        '
        Me.lblManualTrailerLink.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblManualTrailerLink.AutoSize = True
        Me.lblManualTrailerLink.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManualTrailerLink.Location = New System.Drawing.Point(3, 3)
        Me.lblManualTrailerLink.Name = "lblManualTrailerLink"
        Me.lblManualTrailerLink.Size = New System.Drawing.Size(147, 13)
        Me.lblManualTrailerLink.TabIndex = 0
        Me.lblManualTrailerLink.Text = "Direct Link or YouTube URL:"
        '
        'lblLocalTrailer
        '
        Me.lblLocalTrailer.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLocalTrailer.AutoSize = True
        Me.lblLocalTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocalTrailer.Location = New System.Drawing.Point(3, 52)
        Me.lblLocalTrailer.Name = "lblLocalTrailer"
        Me.lblLocalTrailer.Size = New System.Drawing.Size(69, 13)
        Me.lblLocalTrailer.TabIndex = 2
        Me.lblLocalTrailer.Text = "Local Trailer:"
        '
        'txtManualTrailerLink
        '
        Me.txtManualTrailerLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManualTrailerLink.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtManualTrailerLink.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualTrailerLink.Location = New System.Drawing.Point(3, 23)
        Me.txtManualTrailerLink.Name = "txtManualTrailerLink"
        Me.txtManualTrailerLink.Size = New System.Drawing.Size(835, 22)
        Me.txtManualTrailerLink.TabIndex = 1
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.AutoSize = True
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(884, 610)
        Me.pnlMain.TabIndex = 2
        '
        'tblMain
        '
        Me.tblMain.AutoScroll = True
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 2
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.gbYouTubeSearch, 0, 3)
        Me.tblMain.Controls.Add(Me.gbManualTrailer, 0, 6)
        Me.tblMain.Controls.Add(Me.lvTrailers, 0, 0)
        Me.tblMain.Controls.Add(Me.btnTrailerScrape, 0, 1)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 7
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(884, 610)
        Me.tblMain.TabIndex = 4
        '
        'btnPlayInBrowser
        '
        Me.btnPlayInBrowser.Enabled = False
        Me.btnPlayInBrowser.Location = New System.Drawing.Point(3, 3)
        Me.btnPlayInBrowser.Name = "btnPlayInBrowser"
        Me.btnPlayInBrowser.Size = New System.Drawing.Size(120, 23)
        Me.btnPlayInBrowser.TabIndex = 4
        Me.btnPlayInBrowser.Text = "Open In Browser"
        Me.btnPlayInBrowser.UseVisualStyleBackColor = True
        '
        'pblBottom
        '
        Me.pblBottom.AutoSize = True
        Me.pblBottom.Controls.Add(Me.tblBottom)
        Me.pblBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pblBottom.Location = New System.Drawing.Point(0, 610)
        Me.pblBottom.Name = "pblBottom"
        Me.pblBottom.Size = New System.Drawing.Size(884, 29)
        Me.pblBottom.TabIndex = 8
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 4
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnPlayInBrowser, 0, 0)
        Me.tblBottom.Controls.Add(Me.btnOK, 2, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 3, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.tblBottom.Size = New System.Drawing.Size(884, 29)
        Me.tblBottom.TabIndex = 0
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pbStatus, Me.lblStatus})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 639)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(884, 22)
        Me.StatusStrip.TabIndex = 9
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'pbStatus
        '
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(100, 16)
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(119, 17)
        Me.lblStatus.Text = "Compiling Trailer List"
        '
        'dlgTrailerSelect
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(884, 661)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pblBottom)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "dlgTrailerSelect"
        Me.Text = "Select Trailer"
        Me.gbYouTubeSearch.ResumeLayout(False)
        Me.gbYouTubeSearch.PerformLayout()
        Me.tblYouTubeSearch.ResumeLayout(False)
        Me.tblYouTubeSearch.PerformLayout()
        Me.gbManualTrailer.ResumeLayout(False)
        Me.gbManualTrailer.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        Me.pblBottom.ResumeLayout(False)
        Me.pblBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gbManualTrailer As System.Windows.Forms.GroupBox
    Friend WithEvents lblManualTrailerLink As System.Windows.Forms.Label
    Friend WithEvents txtManualTrailerLink As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseLocalTrailer As System.Windows.Forms.Button
    Friend WithEvents txtLocalTrailer As System.Windows.Forms.TextBox
    Friend WithEvents lblLocalTrailer As System.Windows.Forms.Label
    Friend WithEvents ofdTrailer As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnPlayInBrowser As System.Windows.Forms.Button
    Friend WithEvents lvTrailers As System.Windows.Forms.ListView
    Friend WithEvents btnClearManualTrailerLink As System.Windows.Forms.Button
    Friend WithEvents gbYouTubeSearch As System.Windows.Forms.GroupBox
    Friend WithEvents btnYouTubeSearch As System.Windows.Forms.Button
    Friend WithEvents txtYouTubeSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnTrailerScrape As System.Windows.Forms.Button
    Friend WithEvents colURL As System.Windows.Forms.ColumnHeader
    Friend WithEvents colWebURL As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDuration As System.Windows.Forms.ColumnHeader
    Friend WithEvents colVideoQuality As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSource As System.Windows.Forms.ColumnHeader
    Friend WithEvents colNumber As System.Windows.Forms.ColumnHeader
    Friend WithEvents pblBottom As System.Windows.Forms.Panel
    Friend WithEvents tblBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents colScraper As System.Windows.Forms.ColumnHeader
    Friend WithEvents colVideoType As System.Windows.Forms.ColumnHeader
    Friend WithEvents tblMain As TableLayoutPanel
    Friend WithEvents tblYouTubeSearch As TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents pbStatus As ToolStripProgressBar
    Friend WithEvents lblStatus As ToolStripStatusLabel
End Class