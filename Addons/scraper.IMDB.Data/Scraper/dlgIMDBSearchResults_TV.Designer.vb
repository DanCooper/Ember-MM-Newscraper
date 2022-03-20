<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgIMDBSearchResults_TV
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgIMDBSearchResults_TV))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.tvResults = New System.Windows.Forms.TreeView()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblTagline = New System.Windows.Forms.Label()
        Me.txtPlot = New System.Windows.Forms.TextBox()
        Me.lblCreators = New System.Windows.Forms.Label()
        Me.lblGenre = New System.Windows.Forms.Label()
        Me.txtIMDBID = New System.Windows.Forms.TextBox()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.chkManual = New System.Windows.Forms.CheckBox()
        Me.btnVerify = New System.Windows.Forms.Button()
        Me.lblIMDBID = New System.Windows.Forms.Label()
        Me.lblCreatorsHeader = New System.Windows.Forms.Label()
        Me.lblGenreHeader = New System.Windows.Forms.Label()
        Me.lblIMDBHeader = New System.Windows.Forms.Label()
        Me.lblPlotHeader = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.pnlLoading = New System.Windows.Forms.Panel()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.pbLoading = New System.Windows.Forms.ProgressBar()
        Me.pnlPicStatus = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.btnOpenFolder = New System.Windows.Forms.Button()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLoading.SuspendLayout()
        Me.pnlPicStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Enabled = False
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(501, 431)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 22)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(574, 431)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 22)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'tvResults
        '
        Me.tvResults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvResults.HideSelection = False
        Me.tvResults.Location = New System.Drawing.Point(8, 126)
        Me.tvResults.Name = "tvResults"
        Me.tvResults.Size = New System.Drawing.Size(281, 299)
        Me.tvResults.TabIndex = 5
        '
        'pbPoster
        '
        Me.pbPoster.Location = New System.Drawing.Point(298, 160)
        Me.pbPoster.Name = "pbPoster"
        Me.pbPoster.Size = New System.Drawing.Size(110, 130)
        Me.pbPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbPoster.TabIndex = 2
        Me.pbPoster.TabStop = False
        Me.pbPoster.Visible = False
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(295, 100)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(346, 19)
        Me.lblTitle.TabIndex = 9
        Me.lblTitle.Text = "Title"
        Me.lblTitle.Visible = False
        '
        'lblTagline
        '
        Me.lblTagline.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTagline.Location = New System.Drawing.Point(295, 119)
        Me.lblTagline.Name = "lblTagline"
        Me.lblTagline.Size = New System.Drawing.Size(346, 16)
        Me.lblTagline.TabIndex = 10
        Me.lblTagline.Text = "Tagline"
        Me.lblTagline.Visible = False
        '
        'txtPlot
        '
        Me.txtPlot.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlot.Location = New System.Drawing.Point(298, 325)
        Me.txtPlot.Multiline = True
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlot.Size = New System.Drawing.Size(343, 100)
        Me.txtPlot.TabIndex = 22
        Me.txtPlot.TabStop = False
        Me.txtPlot.Visible = False
        '
        'lblCreators
        '
        Me.lblCreators.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreators.Location = New System.Drawing.Point(476, 186)
        Me.lblCreators.Name = "lblCreators"
        Me.lblCreators.Size = New System.Drawing.Size(165, 16)
        Me.lblCreators.TabIndex = 15
        Me.lblCreators.Text = "Creators"
        Me.lblCreators.Visible = False
        '
        'lblGenre
        '
        Me.lblGenre.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGenre.Location = New System.Drawing.Point(476, 213)
        Me.lblGenre.Name = "lblGenre"
        Me.lblGenre.Size = New System.Drawing.Size(165, 52)
        Me.lblGenre.TabIndex = 17
        Me.lblGenre.Text = "Genre"
        Me.lblGenre.Visible = False
        '
        'txtIMDBID
        '
        Me.txtIMDBID.Enabled = False
        Me.txtIMDBID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIMDBID.Location = New System.Drawing.Point(143, 429)
        Me.txtIMDBID.Name = "txtIMDBID"
        Me.txtIMDBID.Size = New System.Drawing.Size(100, 22)
        Me.txtIMDBID.TabIndex = 7
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.PictureBox1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(664, 64)
        Me.pnlTop.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(61, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(288, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "View details of each result to find the proper TV show."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(58, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(216, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "TV Search Results"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(7, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'chkManual
        '
        Me.chkManual.AutoSize = True
        Me.chkManual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkManual.Location = New System.Drawing.Point(8, 433)
        Me.chkManual.Name = "chkManual"
        Me.chkManual.Size = New System.Drawing.Size(127, 17)
        Me.chkManual.TabIndex = 6
        Me.chkManual.Text = "Manual IMDB Entry:"
        Me.chkManual.UseVisualStyleBackColor = True
        '
        'btnVerify
        '
        Me.btnVerify.Enabled = False
        Me.btnVerify.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerify.Location = New System.Drawing.Point(249, 429)
        Me.btnVerify.Name = "btnVerify"
        Me.btnVerify.Size = New System.Drawing.Size(75, 22)
        Me.btnVerify.TabIndex = 8
        Me.btnVerify.Text = "Verify"
        Me.btnVerify.UseVisualStyleBackColor = True
        '
        'lblIMDBID
        '
        Me.lblIMDBID.AutoSize = True
        Me.lblIMDBID.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIMDBID.Location = New System.Drawing.Point(476, 277)
        Me.lblIMDBID.Name = "lblIMDBID"
        Me.lblIMDBID.Size = New System.Drawing.Size(34, 13)
        Me.lblIMDBID.TabIndex = 19
        Me.lblIMDBID.Text = "IMDB"
        Me.lblIMDBID.Visible = False
        '
        'lblCreatorsHeader
        '
        Me.lblCreatorsHeader.AutoSize = True
        Me.lblCreatorsHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCreatorsHeader.Location = New System.Drawing.Point(414, 186)
        Me.lblCreatorsHeader.Name = "lblCreatorsHeader"
        Me.lblCreatorsHeader.Size = New System.Drawing.Size(53, 13)
        Me.lblCreatorsHeader.TabIndex = 14
        Me.lblCreatorsHeader.Text = "Creators:"
        Me.lblCreatorsHeader.Visible = False
        '
        'lblGenreHeader
        '
        Me.lblGenreHeader.AutoSize = True
        Me.lblGenreHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblGenreHeader.Location = New System.Drawing.Point(414, 213)
        Me.lblGenreHeader.Name = "lblGenreHeader"
        Me.lblGenreHeader.Size = New System.Drawing.Size(54, 13)
        Me.lblGenreHeader.TabIndex = 16
        Me.lblGenreHeader.Text = "Genre(s):"
        Me.lblGenreHeader.Visible = False
        '
        'lblIMDBHeader
        '
        Me.lblIMDBHeader.AutoSize = True
        Me.lblIMDBHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblIMDBHeader.Location = New System.Drawing.Point(414, 277)
        Me.lblIMDBHeader.Name = "lblIMDBHeader"
        Me.lblIMDBHeader.Size = New System.Drawing.Size(53, 13)
        Me.lblIMDBHeader.TabIndex = 18
        Me.lblIMDBHeader.Text = "IMDB ID:"
        Me.lblIMDBHeader.Visible = False
        '
        'lblPlotHeader
        '
        Me.lblPlotHeader.AutoSize = True
        Me.lblPlotHeader.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblPlotHeader.Location = New System.Drawing.Point(295, 309)
        Me.lblPlotHeader.Name = "lblPlotHeader"
        Me.lblPlotHeader.Size = New System.Drawing.Size(31, 13)
        Me.lblPlotHeader.TabIndex = 21
        Me.lblPlotHeader.Text = "Plot:"
        Me.lblPlotHeader.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(266, 100)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(23, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(8, 101)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(252, 22)
        Me.txtSearch.TabIndex = 3
        '
        'pnlLoading
        '
        Me.pnlLoading.BackColor = System.Drawing.Color.White
        Me.pnlLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLoading.Controls.Add(Me.lblLoading)
        Me.pnlLoading.Controls.Add(Me.pbLoading)
        Me.pnlLoading.Location = New System.Drawing.Point(368, 213)
        Me.pnlLoading.Name = "pnlLoading"
        Me.pnlLoading.Size = New System.Drawing.Size(200, 54)
        Me.pnlLoading.TabIndex = 20
        '
        'lblLoading
        '
        Me.lblLoading.AutoSize = True
        Me.lblLoading.Location = New System.Drawing.Point(3, 10)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(73, 13)
        Me.lblLoading.TabIndex = 0
        Me.lblLoading.Text = "Please wait..."
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbLoading
        '
        Me.pbLoading.Location = New System.Drawing.Point(3, 32)
        Me.pbLoading.MarqueeAnimationSpeed = 25
        Me.pbLoading.Name = "pbLoading"
        Me.pbLoading.Size = New System.Drawing.Size(192, 17)
        Me.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbLoading.TabIndex = 1
        '
        'pnlPicStatus
        '
        Me.pnlPicStatus.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlPicStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPicStatus.Controls.Add(Me.Label4)
        Me.pnlPicStatus.Location = New System.Drawing.Point(312, 185)
        Me.pnlPicStatus.Name = "pnlPicStatus"
        Me.pnlPicStatus.Size = New System.Drawing.Size(81, 45)
        Me.pnlPicStatus.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(5, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 33)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Fetching Poster..."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtFileName
        '
        Me.txtFileName.Enabled = False
        Me.txtFileName.Location = New System.Drawing.Point(8, 73)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(598, 22)
        Me.txtFileName.TabIndex = 3
        '
        'btnOpenFolder
        '
        Me.btnOpenFolder.Location = New System.Drawing.Point(612, 73)
        Me.btnOpenFolder.Name = "btnOpenFolder"
        Me.btnOpenFolder.Size = New System.Drawing.Size(29, 23)
        Me.btnOpenFolder.TabIndex = 23
        Me.btnOpenFolder.Text = "..."
        Me.btnOpenFolder.UseVisualStyleBackColor = True
        '
        'dlgIMDBSearchResults_TV
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(664, 494)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOpenFolder)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.pnlLoading)
        Me.Controls.Add(Me.pnlPicStatus)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblPlotHeader)
        Me.Controls.Add(Me.lblIMDBHeader)
        Me.Controls.Add(Me.lblGenreHeader)
        Me.Controls.Add(Me.lblCreatorsHeader)
        Me.Controls.Add(Me.lblIMDBID)
        Me.Controls.Add(Me.btnVerify)
        Me.Controls.Add(Me.chkManual)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.txtIMDBID)
        Me.Controls.Add(Me.lblGenre)
        Me.Controls.Add(Me.lblCreators)
        Me.Controls.Add(Me.txtPlot)
        Me.Controls.Add(Me.lblTagline)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.pbPoster)
        Me.Controls.Add(Me.tvResults)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(670, 500)
        Me.Name = "dlgIMDBSearchResults_TV"
        Me.Text = "Search Results"
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLoading.ResumeLayout(False)
        Me.pnlLoading.PerformLayout()
        Me.pnlPicStatus.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents tvResults As System.Windows.Forms.TreeView
    Friend WithEvents pbPoster As System.Windows.Forms.PictureBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblTagline As System.Windows.Forms.Label
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Friend WithEvents lblCreators As System.Windows.Forms.Label
    Friend WithEvents lblGenre As System.Windows.Forms.Label
    Friend WithEvents txtIMDBID As System.Windows.Forms.TextBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkManual As System.Windows.Forms.CheckBox
    Friend WithEvents btnVerify As System.Windows.Forms.Button
    Friend WithEvents lblIMDBID As System.Windows.Forms.Label
    Friend WithEvents lblCreatorsHeader As System.Windows.Forms.Label
    Friend WithEvents lblGenreHeader As System.Windows.Forms.Label
    Friend WithEvents lblIMDBHeader As System.Windows.Forms.Label
    Friend WithEvents lblPlotHeader As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents pnlLoading As System.Windows.Forms.Panel
    Friend WithEvents lblLoading As System.Windows.Forms.Label
    Friend WithEvents pbLoading As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlPicStatus As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents btnOpenFolder As System.Windows.Forms.Button

End Class
