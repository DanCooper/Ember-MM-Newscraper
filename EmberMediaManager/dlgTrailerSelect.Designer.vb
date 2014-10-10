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
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.gbSelectTrailer = New System.Windows.Forms.GroupBox()
        Me.btnTrailerScrape = New System.Windows.Forms.Button()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbStatus = New System.Windows.Forms.ProgressBar()
        Me.lvTrailers = New System.Windows.Forms.ListView()
        Me.colNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colURL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colWebURL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDuration = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colQuality = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSource = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colExtension = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gbYouTubeSearch = New System.Windows.Forms.GroupBox()
        Me.btnYouTubeSearch = New System.Windows.Forms.Button()
        Me.txtYouTubeSearch = New System.Windows.Forms.TextBox()
        Me.gbManualTrailer = New System.Windows.Forms.GroupBox()
        Me.btnClearManualTrailerLink = New System.Windows.Forms.Button()
        Me.btnBrowseLocalTrailer = New System.Windows.Forms.Button()
        Me.txtLocalTrailer = New System.Windows.Forms.TextBox()
        Me.lblLocalTrailer = New System.Windows.Forms.Label()
        Me.btnPlayLocalTrailer = New System.Windows.Forms.Button()
        Me.txtManualTrailerLink = New System.Windows.Forms.TextBox()
        Me.lblManualTrailerLink = New System.Windows.Forms.Label()
        Me.ofdTrailer = New System.Windows.Forms.OpenFileDialog()
        Me.pnlTrailerSelect = New System.Windows.Forms.Panel()
        Me.gbPreview = New System.Windows.Forms.GroupBox()
        Me.btnTrailerMute = New System.Windows.Forms.Button()
        Me.btnTrailerStop = New System.Windows.Forms.Button()
        Me.btnTrailerPlay = New System.Windows.Forms.Button()
        Me.axVLCTrailer = New AxAXVLC.AxVLCPlugin2()
        Me.btnPlayInBrowser = New System.Windows.Forms.Button()
        Me.gbSelectTrailer.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.gbYouTubeSearch.SuspendLayout()
        Me.gbManualTrailer.SuspendLayout()
        Me.pnlTrailerSelect.SuspendLayout()
        Me.gbPreview.SuspendLayout()
        CType(Me.axVLCTrailer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Enabled = False
        Me.OK_Button.Location = New System.Drawing.Point(758, 500)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(120, 23)
        Me.OK_Button.TabIndex = 6
        Me.OK_Button.Text = "Download"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(884, 500)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(120, 23)
        Me.Cancel_Button.TabIndex = 7
        Me.Cancel_Button.Text = "Cancel"
        '
        'gbSelectTrailer
        '
        Me.gbSelectTrailer.Controls.Add(Me.btnTrailerScrape)
        Me.gbSelectTrailer.Controls.Add(Me.pnlStatus)
        Me.gbSelectTrailer.Controls.Add(Me.lvTrailers)
        Me.gbSelectTrailer.Controls.Add(Me.gbYouTubeSearch)
        Me.gbSelectTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSelectTrailer.Location = New System.Drawing.Point(9, 9)
        Me.gbSelectTrailer.Name = "gbSelectTrailer"
        Me.gbSelectTrailer.Size = New System.Drawing.Size(582, 440)
        Me.gbSelectTrailer.TabIndex = 0
        Me.gbSelectTrailer.TabStop = False
        Me.gbSelectTrailer.Text = "Select Trailer to Scrape"
        '
        'btnTrailerScrape
        '
        Me.btnTrailerScrape.Location = New System.Drawing.Point(6, 319)
        Me.btnTrailerScrape.Name = "btnTrailerScrape"
        Me.btnTrailerScrape.Size = New System.Drawing.Size(152, 23)
        Me.btnTrailerScrape.TabIndex = 5
        Me.btnTrailerScrape.Text = "Scrape Trailers"
        Me.btnTrailerScrape.UseVisualStyleBackColor = True
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.White
        Me.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatus.Controls.Add(Me.lblStatus)
        Me.pnlStatus.Controls.Add(Me.pbStatus)
        Me.pnlStatus.Location = New System.Drawing.Point(122, 60)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(200, 54)
        Me.pnlStatus.TabIndex = 1
        Me.pnlStatus.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(3, 10)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(121, 13)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Compiling trailer list..."
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbStatus
        '
        Me.pbStatus.Location = New System.Drawing.Point(3, 32)
        Me.pbStatus.MarqueeAnimationSpeed = 25
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(192, 17)
        Me.pbStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbStatus.TabIndex = 1
        '
        'lvTrailers
        '
        Me.lvTrailers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNumber, Me.colURL, Me.colWebURL, Me.colDescription, Me.colDuration, Me.colQuality, Me.colSource, Me.colExtension})
        Me.lvTrailers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvTrailers.Location = New System.Drawing.Point(6, 19)
        Me.lvTrailers.Name = "lvTrailers"
        Me.lvTrailers.Size = New System.Drawing.Size(570, 298)
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
        Me.colDescription.Width = 270
        '
        'colDuration
        '
        Me.colDuration.Text = "Duration"
        '
        'colQuality
        '
        Me.colQuality.Text = "Quality"
        '
        'colSource
        '
        Me.colSource.Text = "Source"
        Me.colSource.Width = 80
        '
        'colExtension
        '
        Me.colExtension.Text = "Typ"
        Me.colExtension.Width = 50
        '
        'gbYouTubeSearch
        '
        Me.gbYouTubeSearch.Controls.Add(Me.btnYouTubeSearch)
        Me.gbYouTubeSearch.Controls.Add(Me.txtYouTubeSearch)
        Me.gbYouTubeSearch.Location = New System.Drawing.Point(6, 364)
        Me.gbYouTubeSearch.Name = "gbYouTubeSearch"
        Me.gbYouTubeSearch.Size = New System.Drawing.Size(445, 70)
        Me.gbYouTubeSearch.TabIndex = 9
        Me.gbYouTubeSearch.TabStop = False
        Me.gbYouTubeSearch.Text = "Search On YouTube"
        '
        'btnYouTubeSearch
        '
        Me.btnYouTubeSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnYouTubeSearch.Location = New System.Drawing.Point(364, 27)
        Me.btnYouTubeSearch.Name = "btnYouTubeSearch"
        Me.btnYouTubeSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnYouTubeSearch.TabIndex = 1
        Me.btnYouTubeSearch.Text = "Search"
        Me.btnYouTubeSearch.UseVisualStyleBackColor = True
        '
        'txtYouTubeSearch
        '
        Me.txtYouTubeSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtYouTubeSearch.Location = New System.Drawing.Point(6, 27)
        Me.txtYouTubeSearch.Name = "txtYouTubeSearch"
        Me.txtYouTubeSearch.Size = New System.Drawing.Size(352, 22)
        Me.txtYouTubeSearch.TabIndex = 0
        '
        'gbManualTrailer
        '
        Me.gbManualTrailer.Controls.Add(Me.btnClearManualTrailerLink)
        Me.gbManualTrailer.Controls.Add(Me.btnBrowseLocalTrailer)
        Me.gbManualTrailer.Controls.Add(Me.txtLocalTrailer)
        Me.gbManualTrailer.Controls.Add(Me.lblLocalTrailer)
        Me.gbManualTrailer.Controls.Add(Me.btnPlayLocalTrailer)
        Me.gbManualTrailer.Controls.Add(Me.txtManualTrailerLink)
        Me.gbManualTrailer.Controls.Add(Me.lblManualTrailerLink)
        Me.gbManualTrailer.Location = New System.Drawing.Point(6, 294)
        Me.gbManualTrailer.Name = "gbManualTrailer"
        Me.gbManualTrailer.Size = New System.Drawing.Size(384, 140)
        Me.gbManualTrailer.TabIndex = 3
        Me.gbManualTrailer.TabStop = False
        Me.gbManualTrailer.Text = "Manual Trailer Entry"
        '
        'btnClearManualTrailerLink
        '
        Me.btnClearManualTrailerLink.Location = New System.Drawing.Point(352, 28)
        Me.btnClearManualTrailerLink.Name = "btnClearManualTrailerLink"
        Me.btnClearManualTrailerLink.Size = New System.Drawing.Size(25, 23)
        Me.btnClearManualTrailerLink.TabIndex = 5
        Me.btnClearManualTrailerLink.Text = "X"
        Me.btnClearManualTrailerLink.UseVisualStyleBackColor = True
        '
        'btnBrowseLocalTrailer
        '
        Me.btnBrowseLocalTrailer.Location = New System.Drawing.Point(352, 81)
        Me.btnBrowseLocalTrailer.Name = "btnBrowseLocalTrailer"
        Me.btnBrowseLocalTrailer.Size = New System.Drawing.Size(25, 23)
        Me.btnBrowseLocalTrailer.TabIndex = 4
        Me.btnBrowseLocalTrailer.Text = "..."
        Me.btnBrowseLocalTrailer.UseVisualStyleBackColor = True
        '
        'txtLocalTrailer
        '
        Me.txtLocalTrailer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocalTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocalTrailer.Location = New System.Drawing.Point(9, 82)
        Me.txtLocalTrailer.Name = "txtLocalTrailer"
        Me.txtLocalTrailer.Size = New System.Drawing.Size(337, 22)
        Me.txtLocalTrailer.TabIndex = 3
        '
        'lblLocalTrailer
        '
        Me.lblLocalTrailer.AutoSize = True
        Me.lblLocalTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocalTrailer.Location = New System.Drawing.Point(6, 68)
        Me.lblLocalTrailer.Name = "lblLocalTrailer"
        Me.lblLocalTrailer.Size = New System.Drawing.Size(71, 13)
        Me.lblLocalTrailer.TabIndex = 2
        Me.lblLocalTrailer.Text = "Local Trailer:"
        '
        'btnPlayLocalTrailer
        '
        Me.btnPlayLocalTrailer.Enabled = False
        Me.btnPlayLocalTrailer.Image = CType(resources.GetObject("btnPlayLocalTrailer.Image"), System.Drawing.Image)
        Me.btnPlayLocalTrailer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPlayLocalTrailer.Location = New System.Drawing.Point(121, 110)
        Me.btnPlayLocalTrailer.Name = "btnPlayLocalTrailer"
        Me.btnPlayLocalTrailer.Size = New System.Drawing.Size(120, 23)
        Me.btnPlayLocalTrailer.TabIndex = 3
        Me.btnPlayLocalTrailer.Text = "Preview Trailer"
        Me.btnPlayLocalTrailer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPlayLocalTrailer.UseVisualStyleBackColor = True
        '
        'txtManualTrailerLink
        '
        Me.txtManualTrailerLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManualTrailerLink.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualTrailerLink.Location = New System.Drawing.Point(9, 28)
        Me.txtManualTrailerLink.Name = "txtManualTrailerLink"
        Me.txtManualTrailerLink.Size = New System.Drawing.Size(337, 22)
        Me.txtManualTrailerLink.TabIndex = 1
        '
        'lblManualTrailerLink
        '
        Me.lblManualTrailerLink.AutoSize = True
        Me.lblManualTrailerLink.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManualTrailerLink.Location = New System.Drawing.Point(6, 14)
        Me.lblManualTrailerLink.Name = "lblManualTrailerLink"
        Me.lblManualTrailerLink.Size = New System.Drawing.Size(257, 13)
        Me.lblManualTrailerLink.TabIndex = 0
        Me.lblManualTrailerLink.Text = "Direct Link, YouTube, IMDB or Apple Trailer URL:"
        '
        'pnlTrailerSelect
        '
        Me.pnlTrailerSelect.BackColor = System.Drawing.Color.White
        Me.pnlTrailerSelect.Controls.Add(Me.gbPreview)
        Me.pnlTrailerSelect.Controls.Add(Me.gbSelectTrailer)
        Me.pnlTrailerSelect.Location = New System.Drawing.Point(3, 3)
        Me.pnlTrailerSelect.Name = "pnlTrailerSelect"
        Me.pnlTrailerSelect.Size = New System.Drawing.Size(1001, 462)
        Me.pnlTrailerSelect.TabIndex = 2
        '
        'gbPreview
        '
        Me.gbPreview.Controls.Add(Me.btnTrailerMute)
        Me.gbPreview.Controls.Add(Me.btnTrailerStop)
        Me.gbPreview.Controls.Add(Me.gbManualTrailer)
        Me.gbPreview.Controls.Add(Me.btnTrailerPlay)
        Me.gbPreview.Controls.Add(Me.axVLCTrailer)
        Me.gbPreview.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbPreview.Location = New System.Drawing.Point(597, 9)
        Me.gbPreview.Name = "gbPreview"
        Me.gbPreview.Size = New System.Drawing.Size(396, 440)
        Me.gbPreview.TabIndex = 9
        Me.gbPreview.TabStop = False
        Me.gbPreview.Text = "Preview"
        '
        'btnTrailerMute
        '
        Me.btnTrailerMute.Location = New System.Drawing.Point(309, 244)
        Me.btnTrailerMute.Name = "btnTrailerMute"
        Me.btnTrailerMute.Size = New System.Drawing.Size(75, 23)
        Me.btnTrailerMute.TabIndex = 15
        Me.btnTrailerMute.Text = "Mute"
        Me.btnTrailerMute.UseVisualStyleBackColor = True
        '
        'btnTrailerStop
        '
        Me.btnTrailerStop.Enabled = False
        Me.btnTrailerStop.Location = New System.Drawing.Point(94, 244)
        Me.btnTrailerStop.Name = "btnTrailerStop"
        Me.btnTrailerStop.Size = New System.Drawing.Size(75, 23)
        Me.btnTrailerStop.TabIndex = 14
        Me.btnTrailerStop.Text = "Stop"
        Me.btnTrailerStop.UseVisualStyleBackColor = True
        '
        'btnTrailerPlay
        '
        Me.btnTrailerPlay.Enabled = False
        Me.btnTrailerPlay.Location = New System.Drawing.Point(13, 244)
        Me.btnTrailerPlay.Name = "btnTrailerPlay"
        Me.btnTrailerPlay.Size = New System.Drawing.Size(75, 23)
        Me.btnTrailerPlay.TabIndex = 13
        Me.btnTrailerPlay.Text = "Play"
        Me.btnTrailerPlay.UseVisualStyleBackColor = True
        '
        'axVLCTrailer
        '
        Me.axVLCTrailer.Enabled = True
        Me.axVLCTrailer.Location = New System.Drawing.Point(7, 22)
        Me.axVLCTrailer.Name = "axVLCTrailer"
        Me.axVLCTrailer.OcxState = CType(resources.GetObject("axVLCTrailer.OcxState"), System.Windows.Forms.AxHost.State)
        Me.axVLCTrailer.Size = New System.Drawing.Size(384, 216)
        Me.axVLCTrailer.TabIndex = 10
        '
        'btnPlayInBrowser
        '
        Me.btnPlayInBrowser.Enabled = False
        Me.btnPlayInBrowser.Location = New System.Drawing.Point(12, 500)
        Me.btnPlayInBrowser.Name = "btnPlayInBrowser"
        Me.btnPlayInBrowser.Size = New System.Drawing.Size(120, 23)
        Me.btnPlayInBrowser.TabIndex = 4
        Me.btnPlayInBrowser.Text = "Open In Browser"
        Me.btnPlayInBrowser.UseVisualStyleBackColor = True
        '
        'dlgTrailerSelect
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(1008, 535)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlTrailerSelect)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.btnPlayInBrowser)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgTrailerSelect"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Trailer"
        Me.gbSelectTrailer.ResumeLayout(False)
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.gbYouTubeSearch.ResumeLayout(False)
        Me.gbYouTubeSearch.PerformLayout()
        Me.gbManualTrailer.ResumeLayout(False)
        Me.gbManualTrailer.PerformLayout()
        Me.pnlTrailerSelect.ResumeLayout(False)
        Me.gbPreview.ResumeLayout(False)
        CType(Me.axVLCTrailer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents gbSelectTrailer As System.Windows.Forms.GroupBox
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pbStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents btnPlayLocalTrailer As System.Windows.Forms.Button
    Friend WithEvents gbManualTrailer As System.Windows.Forms.GroupBox
    Friend WithEvents lblManualTrailerLink As System.Windows.Forms.Label
    Friend WithEvents txtManualTrailerLink As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseLocalTrailer As System.Windows.Forms.Button
    Friend WithEvents txtLocalTrailer As System.Windows.Forms.TextBox
    Friend WithEvents lblLocalTrailer As System.Windows.Forms.Label
    Friend WithEvents ofdTrailer As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlTrailerSelect As System.Windows.Forms.Panel
    Friend WithEvents btnPlayInBrowser As System.Windows.Forms.Button
    Friend WithEvents lvTrailers As System.Windows.Forms.ListView
    Friend WithEvents gbPreview As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearManualTrailerLink As System.Windows.Forms.Button
    Friend WithEvents gbYouTubeSearch As System.Windows.Forms.GroupBox
    Friend WithEvents btnYouTubeSearch As System.Windows.Forms.Button
    Friend WithEvents txtYouTubeSearch As System.Windows.Forms.TextBox
    Friend WithEvents axVLCTrailer As AxAXVLC.AxVLCPlugin2
    Friend WithEvents btnTrailerScrape As System.Windows.Forms.Button
    Friend WithEvents btnTrailerMute As System.Windows.Forms.Button
    Friend WithEvents btnTrailerStop As System.Windows.Forms.Button
    Friend WithEvents btnTrailerPlay As System.Windows.Forms.Button
    Friend WithEvents colURL As System.Windows.Forms.ColumnHeader
    Friend WithEvents colWebURL As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDuration As System.Windows.Forms.ColumnHeader
    Friend WithEvents colQuality As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSource As System.Windows.Forms.ColumnHeader
    Friend WithEvents colNumber As System.Windows.Forms.ColumnHeader
    Friend WithEvents colExtension As System.Windows.Forms.ColumnHeader

End Class
