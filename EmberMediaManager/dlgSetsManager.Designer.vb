<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgSetsManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgSetsManager))
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.gbMovies = New System.Windows.Forms.GroupBox()
        Me.lbMovies = New System.Windows.Forms.ListBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.gbSets = New System.Windows.Forms.GroupBox()
        Me.btnRemoveSet = New System.Windows.Forms.Button()
        Me.btnEditSet = New System.Windows.Forms.Button()
        Me.btnNewSet = New System.Windows.Forms.Button()
        Me.lbSets = New System.Windows.Forms.ListBox()
        Me.gbMoviesInSet = New System.Windows.Forms.GroupBox()
        Me.lblCurrentSet = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lbMoviesInSet = New System.Windows.Forms.ListBox()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.pnlSaving = New System.Windows.Forms.Panel()
        Me.lblSaving = New System.Windows.Forms.Label()
        Me.prbSaving = New System.Windows.Forms.ProgressBar()
        Me.prbCompile = New System.Windows.Forms.ProgressBar()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.btnRemovePoster = New System.Windows.Forms.Button()
        Me.lblPosterSize = New System.Windows.Forms.Label()
        Me.btnSetPosterScrape = New System.Windows.Forms.Button()
        Me.btnSetPoster = New System.Windows.Forms.Button()
        Me.pbPoster = New System.Windows.Forms.PictureBox()
        Me.ofdImage = New System.Windows.Forms.OpenFileDialog()
        Me.btnRemoveFanart = New System.Windows.Forms.Button()
        Me.lblFanartSize = New System.Windows.Forms.Label()
        Me.btnSetFanartScrape = New System.Windows.Forms.Button()
        Me.btnSetFanart = New System.Windows.Forms.Button()
        Me.pbFanart = New System.Windows.Forms.PictureBox()
        Me.btSave = New System.Windows.Forms.Button()
        Me.lblSourcePath = New System.Windows.Forms.Label()
        Me.txtSourcePath = New System.Windows.Forms.TextBox()
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.gbMovies.SuspendLayout()
        Me.gbSets.SuspendLayout()
        Me.gbMoviesInSet.SuspendLayout()
        Me.pnlCancel.SuspendLayout()
        Me.pnlSaving.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(5, 542)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(84, 32)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Close"
        '
        'gbMovies
        '
        Me.gbMovies.Controls.Add(Me.lbMovies)
        Me.gbMovies.Controls.Add(Me.btnAdd)
        Me.gbMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovies.Location = New System.Drawing.Point(481, 69)
        Me.gbMovies.Name = "gbMovies"
        Me.gbMovies.Size = New System.Drawing.Size(239, 467)
        Me.gbMovies.TabIndex = 4
        Me.gbMovies.TabStop = False
        Me.gbMovies.Text = "Movies"
        '
        'lbMovies
        '
        Me.lbMovies.Enabled = False
        Me.lbMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbMovies.FormattingEnabled = True
        Me.lbMovies.HorizontalScrollbar = True
        Me.lbMovies.Location = New System.Drawing.Point(8, 19)
        Me.lbMovies.Name = "lbMovies"
        Me.lbMovies.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbMovies.Size = New System.Drawing.Size(225, 407)
        Me.lbMovies.TabIndex = 0
        '
        'btnAdd
        '
        Me.btnAdd.Enabled = False
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAdd.Location = New System.Drawing.Point(6, 438)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'gbSets
        '
        Me.gbSets.Controls.Add(Me.btnRemoveSet)
        Me.gbSets.Controls.Add(Me.btnEditSet)
        Me.gbSets.Controls.Add(Me.btnNewSet)
        Me.gbSets.Controls.Add(Me.lbSets)
        Me.gbSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbSets.Location = New System.Drawing.Point(5, 69)
        Me.gbSets.Name = "gbSets"
        Me.gbSets.Size = New System.Drawing.Size(211, 467)
        Me.gbSets.TabIndex = 2
        Me.gbSets.TabStop = False
        Me.gbSets.Text = "Sets"
        '
        'btnRemoveSet
        '
        Me.btnRemoveSet.Enabled = False
        Me.btnRemoveSet.Image = CType(resources.GetObject("btnRemoveSet.Image"), System.Drawing.Image)
        Me.btnRemoveSet.Location = New System.Drawing.Point(184, 438)
        Me.btnRemoveSet.Name = "btnRemoveSet"
        Me.btnRemoveSet.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveSet.TabIndex = 3
        Me.btnRemoveSet.UseVisualStyleBackColor = True
        '
        'btnEditSet
        '
        Me.btnEditSet.Enabled = False
        Me.btnEditSet.Image = CType(resources.GetObject("btnEditSet.Image"), System.Drawing.Image)
        Me.btnEditSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditSet.Location = New System.Drawing.Point(37, 438)
        Me.btnEditSet.Name = "btnEditSet"
        Me.btnEditSet.Size = New System.Drawing.Size(23, 23)
        Me.btnEditSet.TabIndex = 2
        Me.btnEditSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditSet.UseVisualStyleBackColor = True
        '
        'btnNewSet
        '
        Me.btnNewSet.Enabled = False
        Me.btnNewSet.Image = CType(resources.GetObject("btnNewSet.Image"), System.Drawing.Image)
        Me.btnNewSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewSet.Location = New System.Drawing.Point(8, 438)
        Me.btnNewSet.Name = "btnNewSet"
        Me.btnNewSet.Size = New System.Drawing.Size(23, 23)
        Me.btnNewSet.TabIndex = 1
        Me.btnNewSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNewSet.UseVisualStyleBackColor = True
        '
        'lbSets
        '
        Me.lbSets.Enabled = False
        Me.lbSets.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbSets.FormattingEnabled = True
        Me.lbSets.Location = New System.Drawing.Point(6, 20)
        Me.lbSets.Name = "lbSets"
        Me.lbSets.Size = New System.Drawing.Size(201, 407)
        Me.lbSets.Sorted = True
        Me.lbSets.TabIndex = 0
        '
        'gbMoviesInSet
        '
        Me.gbMoviesInSet.Controls.Add(Me.lblCurrentSet)
        Me.gbMoviesInSet.Controls.Add(Me.btnDown)
        Me.gbMoviesInSet.Controls.Add(Me.btnUp)
        Me.gbMoviesInSet.Controls.Add(Me.btnRemove)
        Me.gbMoviesInSet.Controls.Add(Me.lbMoviesInSet)
        Me.gbMoviesInSet.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMoviesInSet.Location = New System.Drawing.Point(218, 69)
        Me.gbMoviesInSet.Name = "gbMoviesInSet"
        Me.gbMoviesInSet.Size = New System.Drawing.Size(262, 467)
        Me.gbMoviesInSet.TabIndex = 3
        Me.gbMoviesInSet.TabStop = False
        Me.gbMoviesInSet.Text = "Movies In Set"
        '
        'lblCurrentSet
        '
        Me.lblCurrentSet.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCurrentSet.Location = New System.Drawing.Point(6, 20)
        Me.lblCurrentSet.Name = "lblCurrentSet"
        Me.lblCurrentSet.Size = New System.Drawing.Size(214, 23)
        Me.lblCurrentSet.TabIndex = 0
        Me.lblCurrentSet.Text = "None Selected"
        Me.lblCurrentSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDown
        '
        Me.btnDown.Enabled = False
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.Location = New System.Drawing.Point(38, 438)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(23, 23)
        Me.btnDown.TabIndex = 3
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Enabled = False
        Me.btnUp.Image = CType(resources.GetObject("btnUp.Image"), System.Drawing.Image)
        Me.btnUp.Location = New System.Drawing.Point(9, 438)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(23, 23)
        Me.btnUp.TabIndex = 2
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Enabled = False
        Me.btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), System.Drawing.Image)
        Me.btnRemove.Location = New System.Drawing.Point(231, 438)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnRemove.TabIndex = 4
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'lbMoviesInSet
        '
        Me.lbMoviesInSet.Enabled = False
        Me.lbMoviesInSet.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbMoviesInSet.FormattingEnabled = True
        Me.lbMoviesInSet.HorizontalScrollbar = True
        Me.lbMoviesInSet.Location = New System.Drawing.Point(4, 46)
        Me.lbMoviesInSet.Name = "lbMoviesInSet"
        Me.lbMoviesInSet.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbMoviesInSet.Size = New System.Drawing.Size(252, 381)
        Me.lbMoviesInSet.TabIndex = 1
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.White
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.pnlSaving)
        Me.pnlCancel.Controls.Add(Me.prbCompile)
        Me.pnlCancel.Controls.Add(Me.lblCompiling)
        Me.pnlCancel.Controls.Add(Me.lblFile)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Location = New System.Drawing.Point(150, 209)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(403, 76)
        Me.pnlCancel.TabIndex = 4
        Me.pnlCancel.Visible = False
        '
        'pnlSaving
        '
        Me.pnlSaving.BackColor = System.Drawing.Color.White
        Me.pnlSaving.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSaving.Controls.Add(Me.lblSaving)
        Me.pnlSaving.Controls.Add(Me.prbSaving)
        Me.pnlSaving.Location = New System.Drawing.Point(77, 12)
        Me.pnlSaving.Name = "pnlSaving"
        Me.pnlSaving.Size = New System.Drawing.Size(252, 51)
        Me.pnlSaving.TabIndex = 5
        Me.pnlSaving.Visible = False
        '
        'lblSaving
        '
        Me.lblSaving.AutoSize = True
        Me.lblSaving.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSaving.Location = New System.Drawing.Point(2, 7)
        Me.lblSaving.Name = "lblSaving"
        Me.lblSaving.Size = New System.Drawing.Size(51, 13)
        Me.lblSaving.TabIndex = 0
        Me.lblSaving.Text = "Saving..."
        '
        'prbSaving
        '
        Me.prbSaving.Location = New System.Drawing.Point(4, 26)
        Me.prbSaving.MarqueeAnimationSpeed = 25
        Me.prbSaving.Name = "prbSaving"
        Me.prbSaving.Size = New System.Drawing.Size(242, 16)
        Me.prbSaving.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.prbSaving.TabIndex = 1
        '
        'prbCompile
        '
        Me.prbCompile.Location = New System.Drawing.Point(8, 36)
        Me.prbCompile.Name = "prbCompile"
        Me.prbCompile.Size = New System.Drawing.Size(388, 18)
        Me.prbCompile.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prbCompile.TabIndex = 3
        '
        'lblCompiling
        '
        Me.lblCompiling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCompiling.Location = New System.Drawing.Point(3, 11)
        Me.lblCompiling.Name = "lblCompiling"
        Me.lblCompiling.Size = New System.Drawing.Size(203, 20)
        Me.lblCompiling.TabIndex = 0
        Me.lblCompiling.Text = "Loading Movies and Sets..."
        Me.lblCompiling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCompiling.Visible = False
        '
        'lblFile
        '
        Me.lblFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFile.Location = New System.Drawing.Point(3, 57)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(395, 13)
        Me.lblFile.TabIndex = 4
        Me.lblFile.Text = "File ..."
        '
        'lblCanceling
        '
        Me.lblCanceling.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanceling.Location = New System.Drawing.Point(110, 12)
        Me.lblCanceling.Name = "lblCanceling"
        Me.lblCanceling.Size = New System.Drawing.Size(186, 20)
        Me.lblCanceling.TabIndex = 1
        Me.lblCanceling.Text = "Canceling Load..."
        Me.lblCanceling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCanceling.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(298, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = True
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
        Me.pnlTop.Size = New System.Drawing.Size(1104, 64)
        Me.pnlTop.TabIndex = 1
        '
        'lblTopDetails
        '
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(61, 38)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(202, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Add and configure movie boxed sets."
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(170, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Sets Manager"
        '
        'pbTopLogo
        '
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.Location = New System.Drawing.Point(12, 7)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.pbTopLogo.Size = New System.Drawing.Size(36, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'btnRemovePoster
        '
        Me.btnRemovePoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemovePoster.Image = CType(resources.GetObject("btnRemovePoster.Image"), System.Drawing.Image)
        Me.btnRemovePoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemovePoster.Location = New System.Drawing.Point(1004, 248)
        Me.btnRemovePoster.Name = "btnRemovePoster"
        Me.btnRemovePoster.Size = New System.Drawing.Size(96, 79)
        Me.btnRemovePoster.TabIndex = 10
        Me.btnRemovePoster.Text = "Remove Poster"
        Me.btnRemovePoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemovePoster.UseVisualStyleBackColor = True
        '
        'lblPosterSize
        '
        Me.lblPosterSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPosterSize.Location = New System.Drawing.Point(726, 70)
        Me.lblPosterSize.Name = "lblPosterSize"
        Me.lblPosterSize.Size = New System.Drawing.Size(105, 23)
        Me.lblPosterSize.TabIndex = 5
        Me.lblPosterSize.Text = "Size: (XXXXxXXXX)"
        Me.lblPosterSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPosterSize.Visible = False
        '
        'btnSetPosterScrape
        '
        Me.btnSetPosterScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetPosterScrape.Image = CType(resources.GetObject("btnSetPosterScrape.Image"), System.Drawing.Image)
        Me.btnSetPosterScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetPosterScrape.Location = New System.Drawing.Point(1004, 155)
        Me.btnSetPosterScrape.Name = "btnSetPosterScrape"
        Me.btnSetPosterScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetPosterScrape.TabIndex = 8
        Me.btnSetPosterScrape.Text = "Change Poster (Scrape)"
        Me.btnSetPosterScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPosterScrape.UseVisualStyleBackColor = True
        '
        'btnSetPoster
        '
        Me.btnSetPoster.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetPoster.Image = CType(resources.GetObject("btnSetPoster.Image"), System.Drawing.Image)
        Me.btnSetPoster.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetPoster.Location = New System.Drawing.Point(1004, 68)
        Me.btnSetPoster.Name = "btnSetPoster"
        Me.btnSetPoster.Size = New System.Drawing.Size(96, 83)
        Me.btnSetPoster.TabIndex = 7
        Me.btnSetPoster.Text = "Change Poster (Local Browse)"
        Me.btnSetPoster.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetPoster.UseVisualStyleBackColor = True
        '
        'pbPoster
        '
        Me.pbPoster.BackColor = System.Drawing.Color.DimGray
        Me.pbPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbPoster.Location = New System.Drawing.Point(726, 70)
        Me.pbPoster.Name = "pbPoster"
        Me.pbPoster.Size = New System.Drawing.Size(272, 257)
        Me.pbPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbPoster.TabIndex = 6
        Me.pbPoster.TabStop = False
        '
        'btnRemoveFanart
        '
        Me.btnRemoveFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnRemoveFanart.Image = CType(resources.GetObject("btnRemoveFanart.Image"), System.Drawing.Image)
        Me.btnRemoveFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRemoveFanart.Location = New System.Drawing.Point(1003, 509)
        Me.btnRemoveFanart.Name = "btnRemoveFanart"
        Me.btnRemoveFanart.Size = New System.Drawing.Size(97, 64)
        Me.btnRemoveFanart.TabIndex = 15
        Me.btnRemoveFanart.Text = "Remove Fanart"
        Me.btnRemoveFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRemoveFanart.UseVisualStyleBackColor = True
        '
        'lblFanartSize
        '
        Me.lblFanartSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFanartSize.Location = New System.Drawing.Point(725, 333)
        Me.lblFanartSize.Name = "lblFanartSize"
        Me.lblFanartSize.Size = New System.Drawing.Size(105, 23)
        Me.lblFanartSize.TabIndex = 11
        Me.lblFanartSize.Text = "Size: (XXXXxXXXX)"
        Me.lblFanartSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblFanartSize.Visible = False
        '
        'btnSetFanartScrape
        '
        Me.btnSetFanartScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetFanartScrape.Image = CType(resources.GetObject("btnSetFanartScrape.Image"), System.Drawing.Image)
        Me.btnSetFanartScrape.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetFanartScrape.Location = New System.Drawing.Point(1003, 420)
        Me.btnSetFanartScrape.Name = "btnSetFanartScrape"
        Me.btnSetFanartScrape.Size = New System.Drawing.Size(96, 83)
        Me.btnSetFanartScrape.TabIndex = 14
        Me.btnSetFanartScrape.Text = "Change Fanart (Scrape)"
        Me.btnSetFanartScrape.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanartScrape.UseVisualStyleBackColor = True
        '
        'btnSetFanart
        '
        Me.btnSetFanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSetFanart.Image = CType(resources.GetObject("btnSetFanart.Image"), System.Drawing.Image)
        Me.btnSetFanart.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSetFanart.Location = New System.Drawing.Point(1003, 333)
        Me.btnSetFanart.Name = "btnSetFanart"
        Me.btnSetFanart.Size = New System.Drawing.Size(96, 83)
        Me.btnSetFanart.TabIndex = 12
        Me.btnSetFanart.Text = "Change Fanart (Local Browse)"
        Me.btnSetFanart.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSetFanart.UseVisualStyleBackColor = True
        '
        'pbFanart
        '
        Me.pbFanart.BackColor = System.Drawing.Color.DimGray
        Me.pbFanart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbFanart.Location = New System.Drawing.Point(725, 333)
        Me.pbFanart.Name = "pbFanart"
        Me.pbFanart.Size = New System.Drawing.Size(272, 202)
        Me.pbFanart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbFanart.TabIndex = 13
        Me.pbFanart.TabStop = False
        '
        'btSave
        '
        Me.btSave.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btSave.Location = New System.Drawing.Point(726, 541)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(272, 32)
        Me.btSave.TabIndex = 16
        Me.btSave.Text = "Save Images"
        '
        'lblSourcePath
        '
        Me.lblSourcePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSourcePath.Location = New System.Drawing.Point(126, 548)
        Me.lblSourcePath.Name = "lblSourcePath"
        Me.lblSourcePath.Size = New System.Drawing.Size(143, 22)
        Me.lblSourcePath.TabIndex = 18
        Me.lblSourcePath.Text = "Source Path:"
        Me.lblSourcePath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSourcePath
        '
        Me.txtSourcePath.Enabled = False
        Me.txtSourcePath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSourcePath.Location = New System.Drawing.Point(274, 548)
        Me.txtSourcePath.Name = "txtSourcePath"
        Me.txtSourcePath.Size = New System.Drawing.Size(421, 22)
        Me.txtSourcePath.TabIndex = 19
        '
        'fbdBrowse
        '
        Me.fbdBrowse.Description = "Select the folder for your movieset artwork"
        '
        'dlgSetsManager
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1104, 579)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblSourcePath)
        Me.Controls.Add(Me.txtSourcePath)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.btnRemoveFanart)
        Me.Controls.Add(Me.lblFanartSize)
        Me.Controls.Add(Me.pbFanart)
        Me.Controls.Add(Me.btnSetFanartScrape)
        Me.Controls.Add(Me.btnSetFanart)
        Me.Controls.Add(Me.btnRemovePoster)
        Me.Controls.Add(Me.lblPosterSize)
        Me.Controls.Add(Me.btnSetPosterScrape)
        Me.Controls.Add(Me.btnSetPoster)
        Me.Controls.Add(Me.pbPoster)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.pnlCancel)
        Me.Controls.Add(Me.gbMoviesInSet)
        Me.Controls.Add(Me.gbSets)
        Me.Controls.Add(Me.gbMovies)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgSetsManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sets Manager"
        Me.gbMovies.ResumeLayout(False)
        Me.gbSets.ResumeLayout(False)
        Me.gbMoviesInSet.ResumeLayout(False)
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlSaving.ResumeLayout(False)
        Me.pnlSaving.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbPoster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFanart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents gbMovies As System.Windows.Forms.GroupBox
    Friend WithEvents gbSets As System.Windows.Forms.GroupBox
    Friend WithEvents gbMoviesInSet As System.Windows.Forms.GroupBox
    Friend WithEvents lbMovies As System.Windows.Forms.ListBox
    Friend WithEvents btnNewSet As System.Windows.Forms.Button
    Friend WithEvents lbSets As System.Windows.Forms.ListBox
    Friend WithEvents lbMoviesInSet As System.Windows.Forms.ListBox
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents prbCompile As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnEditSet As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSet As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents lblCurrentSet As System.Windows.Forms.Label
    Friend WithEvents pnlSaving As System.Windows.Forms.Panel
    Friend WithEvents lblSaving As System.Windows.Forms.Label
    Friend WithEvents prbSaving As System.Windows.Forms.ProgressBar
    Friend WithEvents btnRemovePoster As System.Windows.Forms.Button
    Friend WithEvents lblPosterSize As System.Windows.Forms.Label
    Friend WithEvents btnSetPosterScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetPoster As System.Windows.Forms.Button
    Friend WithEvents pbPoster As System.Windows.Forms.PictureBox
    Friend WithEvents ofdImage As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnRemoveFanart As System.Windows.Forms.Button
    Friend WithEvents lblFanartSize As System.Windows.Forms.Label
    Friend WithEvents btnSetFanartScrape As System.Windows.Forms.Button
    Friend WithEvents btnSetFanart As System.Windows.Forms.Button
    Friend WithEvents pbFanart As System.Windows.Forms.PictureBox
    Friend WithEvents btSave As System.Windows.Forms.Button
    Friend WithEvents lblSourcePath As System.Windows.Forms.Label
    Friend WithEvents txtSourcePath As System.Windows.Forms.TextBox
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog

End Class
