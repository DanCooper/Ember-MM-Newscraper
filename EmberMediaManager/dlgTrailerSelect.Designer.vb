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
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbStatus = New System.Windows.Forms.ProgressBar()
        Me.gbManualTrailer = New System.Windows.Forms.GroupBox()
        Me.btnClearLink = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtManual = New System.Windows.Forms.TextBox()
        Me.lblManual = New System.Windows.Forms.Label()
        Me.txtManualTrailerLink = New System.Windows.Forms.TextBox()
        Me.lblYouTube = New System.Windows.Forms.Label()
        Me.lvTrailers = New System.Windows.Forms.ListView()
        Me.btnPlayTrailer = New System.Windows.Forms.Button()
        Me.btnSetNfo = New System.Windows.Forms.Button()
        Me.ofdTrailer = New System.Windows.Forms.OpenFileDialog()
        Me.pnlTrailerSelect = New System.Windows.Forms.Panel()
        Me.gbYouTube = New System.Windows.Forms.GroupBox()
        Me.gbYouTubeSearch = New System.Windows.Forms.GroupBox()
        Me.btnYouTubeSearch = New System.Windows.Forms.Button()
        Me.txtYouTubeSearch = New System.Windows.Forms.TextBox()
        Me.btnPlayBrowser = New System.Windows.Forms.Button()
        Me.AxVLCPlayer = New AxAXVLC.AxVLCPlugin2()
        Me.gbSelectTrailer.SuspendLayout
        Me.pnlStatus.SuspendLayout
        Me.gbManualTrailer.SuspendLayout
        Me.pnlTrailerSelect.SuspendLayout
        Me.gbYouTube.SuspendLayout
        Me.gbYouTubeSearch.SuspendLayout
        CType(Me.AxVLCPlayer,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'OK_Button
        '
        Me.OK_Button.Enabled = false
        Me.OK_Button.Location = New System.Drawing.Point(752, 340)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(120, 23)
        Me.OK_Button.TabIndex = 6
        Me.OK_Button.Text = "Download"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(752, 369)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(120, 23)
        Me.Cancel_Button.TabIndex = 7
        Me.Cancel_Button.Text = "Cancel"
        '
        'gbSelectTrailer
        '
        Me.gbSelectTrailer.Controls.Add(Me.pnlStatus)
        Me.gbSelectTrailer.Controls.Add(Me.gbManualTrailer)
        Me.gbSelectTrailer.Controls.Add(Me.lvTrailers)
        Me.gbSelectTrailer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbSelectTrailer.Location = New System.Drawing.Point(9, 9)
        Me.gbSelectTrailer.Name = "gbSelectTrailer"
        Me.gbSelectTrailer.Size = New System.Drawing.Size(458, 319)
        Me.gbSelectTrailer.TabIndex = 0
        Me.gbSelectTrailer.TabStop = false
        Me.gbSelectTrailer.Text = "Select Trailer to Scrape"
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
        Me.pnlStatus.Visible = false
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = true
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
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
        'gbManualTrailer
        '
        Me.gbManualTrailer.Controls.Add(Me.btnClearLink)
        Me.gbManualTrailer.Controls.Add(Me.btnBrowse)
        Me.gbManualTrailer.Controls.Add(Me.txtManual)
        Me.gbManualTrailer.Controls.Add(Me.lblManual)
        Me.gbManualTrailer.Controls.Add(Me.txtManualTrailerLink)
        Me.gbManualTrailer.Controls.Add(Me.lblYouTube)
        Me.gbManualTrailer.Location = New System.Drawing.Point(6, 201)
        Me.gbManualTrailer.Name = "gbManualTrailer"
        Me.gbManualTrailer.Size = New System.Drawing.Size(445, 111)
        Me.gbManualTrailer.TabIndex = 3
        Me.gbManualTrailer.TabStop = false
        Me.gbManualTrailer.Text = "Manual Trailer Entry"
        '
        'btnClearLink
        '
        Me.btnClearLink.Location = New System.Drawing.Point(410, 28)
        Me.btnClearLink.Name = "btnClearLink"
        Me.btnClearLink.Size = New System.Drawing.Size(25, 23)
        Me.btnClearLink.TabIndex = 5
        Me.btnClearLink.Text = "X"
        Me.btnClearLink.UseVisualStyleBackColor = true
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(410, 82)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(25, 23)
        Me.btnBrowse.TabIndex = 4
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = true
        '
        'txtManual
        '
        Me.txtManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtManual.Location = New System.Drawing.Point(9, 82)
        Me.txtManual.Name = "txtManual"
        Me.txtManual.Size = New System.Drawing.Size(395, 22)
        Me.txtManual.TabIndex = 3
        '
        'lblManual
        '
        Me.lblManual.AutoSize = true
        Me.lblManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblManual.Location = New System.Drawing.Point(6, 68)
        Me.lblManual.Name = "lblManual"
        Me.lblManual.Size = New System.Drawing.Size(71, 13)
        Me.lblManual.TabIndex = 2
        Me.lblManual.Text = "Local Trailer:"
        '
        'txtManualTrailerLink
        '
        Me.txtManualTrailerLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManualTrailerLink.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtManualTrailerLink.Location = New System.Drawing.Point(9, 28)
        Me.txtManualTrailerLink.Name = "txtManualTrailerLink"
        Me.txtManualTrailerLink.Size = New System.Drawing.Size(395, 22)
        Me.txtManualTrailerLink.TabIndex = 1
        '
        'lblYouTube
        '
        Me.lblYouTube.AutoSize = true
        Me.lblYouTube.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblYouTube.Location = New System.Drawing.Point(6, 14)
        Me.lblYouTube.Name = "lblYouTube"
        Me.lblYouTube.Size = New System.Drawing.Size(257, 13)
        Me.lblYouTube.TabIndex = 0
        Me.lblYouTube.Text = "Direct Link, YouTube, IMDB or Apple Trailer URL:"
        '
        'lvTrailers
        '
        Me.lvTrailers.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lvTrailers.Location = New System.Drawing.Point(6, 19)
        Me.lvTrailers.Name = "lvTrailers"
        Me.lvTrailers.Size = New System.Drawing.Size(445, 173)
        Me.lvTrailers.TabIndex = 4
        Me.lvTrailers.UseCompatibleStateImageBehavior = false
        Me.lvTrailers.View = System.Windows.Forms.View.Details
        '
        'btnPlayTrailer
        '
        Me.btnPlayTrailer.Enabled = false
        Me.btnPlayTrailer.Image = CType(resources.GetObject("btnPlayTrailer.Image"),System.Drawing.Image)
        Me.btnPlayTrailer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPlayTrailer.Location = New System.Drawing.Point(12, 340)
        Me.btnPlayTrailer.Name = "btnPlayTrailer"
        Me.btnPlayTrailer.Size = New System.Drawing.Size(120, 23)
        Me.btnPlayTrailer.TabIndex = 3
        Me.btnPlayTrailer.Text = "Preview Trailer"
        Me.btnPlayTrailer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPlayTrailer.UseVisualStyleBackColor = true
        '
        'btnSetNfo
        '
        Me.btnSetNfo.Enabled = false
        Me.btnSetNfo.Location = New System.Drawing.Point(626, 340)
        Me.btnSetNfo.Name = "btnSetNfo"
        Me.btnSetNfo.Size = New System.Drawing.Size(120, 23)
        Me.btnSetNfo.TabIndex = 5
        Me.btnSetNfo.Text = "Set To Nfo"
        '
        'pnlTrailerSelect
        '
        Me.pnlTrailerSelect.BackColor = System.Drawing.Color.White
        Me.pnlTrailerSelect.Controls.Add(Me.gbYouTube)
        Me.pnlTrailerSelect.Controls.Add(Me.gbSelectTrailer)
        Me.pnlTrailerSelect.Location = New System.Drawing.Point(3, 3)
        Me.pnlTrailerSelect.Name = "pnlTrailerSelect"
        Me.pnlTrailerSelect.Size = New System.Drawing.Size(879, 331)
        Me.pnlTrailerSelect.TabIndex = 2
        '
        'gbYouTube
        '
        Me.gbYouTube.Controls.Add(Me.AxVLCPlayer)
        Me.gbYouTube.Controls.Add(Me.gbYouTubeSearch)
        Me.gbYouTube.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.gbYouTube.Location = New System.Drawing.Point(473, 9)
        Me.gbYouTube.Name = "gbYouTube"
        Me.gbYouTube.Size = New System.Drawing.Size(396, 319)
        Me.gbYouTube.TabIndex = 9
        Me.gbYouTube.TabStop = false
        Me.gbYouTube.Text = "YouTube Player"
        '
        'gbYouTubeSearch
        '
        Me.gbYouTubeSearch.Controls.Add(Me.btnYouTubeSearch)
        Me.gbYouTubeSearch.Controls.Add(Me.txtYouTubeSearch)
        Me.gbYouTubeSearch.Location = New System.Drawing.Point(7, 242)
        Me.gbYouTubeSearch.Name = "gbYouTubeSearch"
        Me.gbYouTubeSearch.Size = New System.Drawing.Size(383, 70)
        Me.gbYouTubeSearch.TabIndex = 9
        Me.gbYouTubeSearch.TabStop = false
        Me.gbYouTubeSearch.Text = "Search On YouTube"
        '
        'btnYouTubeSearch
        '
        Me.btnYouTubeSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnYouTubeSearch.Location = New System.Drawing.Point(302, 27)
        Me.btnYouTubeSearch.Name = "btnYouTubeSearch"
        Me.btnYouTubeSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnYouTubeSearch.TabIndex = 1
        Me.btnYouTubeSearch.Text = "Search"
        Me.btnYouTubeSearch.UseVisualStyleBackColor = true
        '
        'txtYouTubeSearch
        '
        Me.txtYouTubeSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtYouTubeSearch.Location = New System.Drawing.Point(6, 27)
        Me.txtYouTubeSearch.Name = "txtYouTubeSearch"
        Me.txtYouTubeSearch.Size = New System.Drawing.Size(290, 22)
        Me.txtYouTubeSearch.TabIndex = 0
        '
        'btnPlayBrowser
        '
        Me.btnPlayBrowser.Enabled = false
        Me.btnPlayBrowser.Location = New System.Drawing.Point(12, 369)
        Me.btnPlayBrowser.Name = "btnPlayBrowser"
        Me.btnPlayBrowser.Size = New System.Drawing.Size(120, 23)
        Me.btnPlayBrowser.TabIndex = 4
        Me.btnPlayBrowser.Text = "Open In Browser"
        Me.btnPlayBrowser.UseVisualStyleBackColor = true
        '
        'AxVLCPlayer
        '
        Me.AxVLCPlayer.Enabled = true
        Me.AxVLCPlayer.Location = New System.Drawing.Point(7, 22)
        Me.AxVLCPlayer.Name = "AxVLCPlayer"
        Me.AxVLCPlayer.OcxState = CType(resources.GetObject("AxVLCPlayer.OcxState"),System.Windows.Forms.AxHost.State)
        Me.AxVLCPlayer.Size = New System.Drawing.Size(384, 216)
        Me.AxVLCPlayer.TabIndex = 10
        '
        'dlgTrailerSelect
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96!, 96!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(884, 398)
        Me.ControlBox = false
        Me.Controls.Add(Me.pnlTrailerSelect)
        Me.Controls.Add(Me.btnSetNfo)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.btnPlayTrailer)
        Me.Controls.Add(Me.btnPlayBrowser)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "dlgTrailerSelect"
        Me.ShowIcon = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Trailer"
        Me.gbSelectTrailer.ResumeLayout(false)
        Me.pnlStatus.ResumeLayout(false)
        Me.pnlStatus.PerformLayout
        Me.gbManualTrailer.ResumeLayout(false)
        Me.gbManualTrailer.PerformLayout
        Me.pnlTrailerSelect.ResumeLayout(false)
        Me.gbYouTube.ResumeLayout(false)
        Me.gbYouTubeSearch.ResumeLayout(false)
        Me.gbYouTubeSearch.PerformLayout
        CType(Me.AxVLCPlayer,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents gbSelectTrailer As System.Windows.Forms.GroupBox
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pbStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents btnPlayTrailer As System.Windows.Forms.Button
    Friend WithEvents btnSetNfo As System.Windows.Forms.Button
    Friend WithEvents gbManualTrailer As System.Windows.Forms.GroupBox
    Friend WithEvents lblYouTube As System.Windows.Forms.Label
    Friend WithEvents txtManualTrailerLink As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtManual As System.Windows.Forms.TextBox
    Friend WithEvents lblManual As System.Windows.Forms.Label
    Friend WithEvents ofdTrailer As System.Windows.Forms.OpenFileDialog
    Friend WithEvents pnlTrailerSelect As System.Windows.Forms.Panel
    Friend WithEvents btnPlayBrowser As System.Windows.Forms.Button
    Friend WithEvents lvTrailers As System.Windows.Forms.ListView
    Friend WithEvents gbYouTube As System.Windows.Forms.GroupBox
    Friend WithEvents btnClearLink As System.Windows.Forms.Button
    Friend WithEvents gbYouTubeSearch As System.Windows.Forms.GroupBox
    Friend WithEvents btnYouTubeSearch As System.Windows.Forms.Button
    Friend WithEvents txtYouTubeSearch As System.Windows.Forms.TextBox
    Friend WithEvents AxVLCPlayer As AxAXVLC.AxVLCPlugin2

End Class
