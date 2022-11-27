<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTagManager
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTagManager))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblTop = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTVShows = New System.Windows.Forms.GroupBox()
        Me.dgvTVShow = New System.Windows.Forms.DataGridView()
        Me.gbTagged = New System.Windows.Forms.GroupBox()
        Me.scTagged = New System.Windows.Forms.SplitContainer()
        Me.dgvTagged_Movie = New System.Windows.Forms.DataGridView()
        Me.dgvTagged_TVShow = New System.Windows.Forms.DataGridView()
        Me.gbMovies = New System.Windows.Forms.GroupBox()
        Me.dgvMovie = New System.Windows.Forms.DataGridView()
        Me.gbTags = New System.Windows.Forms.GroupBox()
        Me.dgvTags = New System.Windows.Forms.DataGridView()
        Me.ssStatus = New System.Windows.Forms.StatusStrip()
        Me.tsslSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.tblTop.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        Me.gbTVShows.SuspendLayout()
        CType(Me.dgvTVShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTagged.SuspendLayout()
        CType(Me.scTagged, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scTagged.Panel1.SuspendLayout()
        Me.scTagged.Panel2.SuspendLayout()
        Me.scTagged.SuspendLayout()
        CType(Me.dgvTagged_Movie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTagged_TVShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMovies.SuspendLayout()
        CType(Me.dgvMovie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTags.SuspendLayout()
        CType(Me.dgvTags, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ssStatus.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.AutoSize = True
        Me.pnlTop.Controls.Add(Me.tblTop)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1184, 54)
        Me.pnlTop.TabIndex = 0
        '
        'tblTop
        '
        Me.tblTop.AutoSize = True
        Me.tblTop.BackColor = System.Drawing.Color.LightSteelBlue
        Me.tblTop.ColumnCount = 2
        Me.tblTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTop.Controls.Add(Me.PictureBox1, 0, 0)
        Me.tblTop.Controls.Add(Me.lblTopTitle, 1, 0)
        Me.tblTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTop.Location = New System.Drawing.Point(0, 0)
        Me.tblTop.Name = "tblTop"
        Me.tblTop.RowCount = 2
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTop.Size = New System.Drawing.Size(1184, 54)
        Me.tblTop.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.Ember_Media_Manager.My.Resources.Resources.MovieSet
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.tblTop.SetRowSpan(Me.PictureBox1, 2)
        Me.PictureBox1.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(57, 0)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(164, 32)
        Me.lblTopTitle.TabIndex = 1
        Me.lblTopTitle.Text = "Tag Manager"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1184, 656)
        Me.pnlMain.TabIndex = 1
        '
        'tblMain
        '
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 4
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblMain.Controls.Add(Me.gbTVShows, 3, 0)
        Me.tblMain.Controls.Add(Me.gbTagged, 1, 0)
        Me.tblMain.Controls.Add(Me.gbMovies, 2, 0)
        Me.tblMain.Controls.Add(Me.gbTags, 0, 0)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 2
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(1184, 656)
        Me.tblMain.TabIndex = 0
        '
        'gbTVShows
        '
        Me.gbTVShows.AutoSize = True
        Me.gbTVShows.Controls.Add(Me.dgvTVShow)
        Me.gbTVShows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVShows.Location = New System.Drawing.Point(891, 3)
        Me.gbTVShows.Name = "gbTVShows"
        Me.gbTVShows.Size = New System.Drawing.Size(290, 650)
        Me.gbTVShows.TabIndex = 4
        Me.gbTVShows.TabStop = False
        Me.gbTVShows.Text = "TV Shows"
        '
        'dgvTVShow
        '
        Me.dgvTVShow.AllowUserToAddRows = False
        Me.dgvTVShow.AllowUserToDeleteRows = False
        Me.dgvTVShow.AllowUserToResizeColumns = False
        Me.dgvTVShow.AllowUserToResizeRows = False
        Me.dgvTVShow.BackgroundColor = System.Drawing.Color.White
        Me.dgvTVShow.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTVShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTVShow.GridColor = System.Drawing.Color.White
        Me.dgvTVShow.Location = New System.Drawing.Point(3, 18)
        Me.dgvTVShow.Name = "dgvTVShow"
        Me.dgvTVShow.ReadOnly = True
        Me.dgvTVShow.RowHeadersVisible = False
        Me.dgvTVShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTVShow.Size = New System.Drawing.Size(284, 629)
        Me.dgvTVShow.TabIndex = 0
        '
        'gbTagged
        '
        Me.gbTagged.AutoSize = True
        Me.gbTagged.Controls.Add(Me.scTagged)
        Me.gbTagged.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTagged.Location = New System.Drawing.Point(299, 3)
        Me.gbTagged.Name = "gbTagged"
        Me.gbTagged.Size = New System.Drawing.Size(290, 650)
        Me.gbTagged.TabIndex = 2
        Me.gbTagged.TabStop = False
        Me.gbTagged.Text = "Tagged"
        '
        'scTagged
        '
        Me.scTagged.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scTagged.Location = New System.Drawing.Point(3, 18)
        Me.scTagged.Name = "scTagged"
        Me.scTagged.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'scTagged.Panel1
        '
        Me.scTagged.Panel1.Controls.Add(Me.dgvTagged_Movie)
        '
        'scTagged.Panel2
        '
        Me.scTagged.Panel2.Controls.Add(Me.dgvTagged_TVShow)
        Me.scTagged.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.scTagged.Size = New System.Drawing.Size(284, 629)
        Me.scTagged.SplitterDistance = 314
        Me.scTagged.TabIndex = 5
        '
        'dgvTagged_Movie
        '
        Me.dgvTagged_Movie.AllowUserToAddRows = False
        Me.dgvTagged_Movie.AllowUserToDeleteRows = False
        Me.dgvTagged_Movie.AllowUserToResizeColumns = False
        Me.dgvTagged_Movie.AllowUserToResizeRows = False
        Me.dgvTagged_Movie.BackgroundColor = System.Drawing.Color.White
        Me.dgvTagged_Movie.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTagged_Movie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTagged_Movie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTagged_Movie.GridColor = System.Drawing.Color.White
        Me.dgvTagged_Movie.Location = New System.Drawing.Point(0, 0)
        Me.dgvTagged_Movie.Name = "dgvTagged_Movie"
        Me.dgvTagged_Movie.ReadOnly = True
        Me.dgvTagged_Movie.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTagged_Movie.Size = New System.Drawing.Size(284, 314)
        Me.dgvTagged_Movie.TabIndex = 0
        '
        'dgvTagged_TVShow
        '
        Me.dgvTagged_TVShow.AllowUserToAddRows = False
        Me.dgvTagged_TVShow.AllowUserToDeleteRows = False
        Me.dgvTagged_TVShow.AllowUserToResizeColumns = False
        Me.dgvTagged_TVShow.AllowUserToResizeRows = False
        Me.dgvTagged_TVShow.BackgroundColor = System.Drawing.Color.White
        Me.dgvTagged_TVShow.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTagged_TVShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTagged_TVShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTagged_TVShow.GridColor = System.Drawing.Color.White
        Me.dgvTagged_TVShow.Location = New System.Drawing.Point(0, 0)
        Me.dgvTagged_TVShow.Name = "dgvTagged_TVShow"
        Me.dgvTagged_TVShow.ReadOnly = True
        Me.dgvTagged_TVShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTagged_TVShow.Size = New System.Drawing.Size(284, 311)
        Me.dgvTagged_TVShow.TabIndex = 0
        '
        'gbMovies
        '
        Me.gbMovies.AutoSize = True
        Me.gbMovies.Controls.Add(Me.dgvMovie)
        Me.gbMovies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovies.Location = New System.Drawing.Point(595, 3)
        Me.gbMovies.Name = "gbMovies"
        Me.gbMovies.Size = New System.Drawing.Size(290, 650)
        Me.gbMovies.TabIndex = 3
        Me.gbMovies.TabStop = False
        Me.gbMovies.Text = "Movies"
        '
        'dgvMovie
        '
        Me.dgvMovie.AllowUserToAddRows = False
        Me.dgvMovie.AllowUserToDeleteRows = False
        Me.dgvMovie.AllowUserToResizeColumns = False
        Me.dgvMovie.AllowUserToResizeRows = False
        Me.dgvMovie.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovie.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvMovie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMovie.GridColor = System.Drawing.Color.White
        Me.dgvMovie.Location = New System.Drawing.Point(3, 18)
        Me.dgvMovie.Name = "dgvMovie"
        Me.dgvMovie.ReadOnly = True
        Me.dgvMovie.RowHeadersVisible = False
        Me.dgvMovie.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovie.Size = New System.Drawing.Size(284, 629)
        Me.dgvMovie.TabIndex = 0
        '
        'gbTags
        '
        Me.gbTags.Controls.Add(Me.dgvTags)
        Me.gbTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTags.Location = New System.Drawing.Point(3, 3)
        Me.gbTags.Name = "gbTags"
        Me.gbTags.Size = New System.Drawing.Size(290, 650)
        Me.gbTags.TabIndex = 1
        Me.gbTags.TabStop = False
        Me.gbTags.Text = "Tags"
        '
        'dgvTags
        '
        Me.dgvTags.AllowUserToDeleteRows = False
        Me.dgvTags.AllowUserToResizeColumns = False
        Me.dgvTags.AllowUserToResizeRows = False
        Me.dgvTags.BackgroundColor = System.Drawing.Color.White
        Me.dgvTags.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTags.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTags.GridColor = System.Drawing.Color.White
        Me.dgvTags.Location = New System.Drawing.Point(3, 18)
        Me.dgvTags.Name = "dgvTags"
        Me.dgvTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTags.Size = New System.Drawing.Size(284, 629)
        Me.dgvTags.TabIndex = 0
        '
        'ssStatus
        '
        Me.ssStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslSpring, Me.tsslStatus, Me.tspbStatus})
        Me.ssStatus.Location = New System.Drawing.Point(0, 739)
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(1184, 22)
        Me.ssStatus.TabIndex = 2
        Me.ssStatus.Text = "StatusStrip1"
        '
        'tsslSpring
        '
        Me.tsslSpring.Name = "tsslSpring"
        Me.tsslSpring.Size = New System.Drawing.Size(1169, 17)
        Me.tsslSpring.Spring = True
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(39, 17)
        Me.tsslStatus.Text = "Status"
        Me.tsslStatus.Visible = False
        '
        'tspbStatus
        '
        Me.tspbStatus.Name = "tspbStatus"
        Me.tspbStatus.Size = New System.Drawing.Size(100, 16)
        Me.tspbStatus.Visible = False
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 710)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1184, 29)
        Me.pnlBottom.TabIndex = 3
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 3
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnOk, 1, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 2, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(1184, 29)
        Me.tblBottom.TabIndex = 0
        '
        'btnOk
        '
        Me.btnOk.AutoSize = True
        Me.btnOk.Location = New System.Drawing.Point(1025, 3)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = True
        Me.btnCancel.Location = New System.Drawing.Point(1106, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pbTopLogo
        '
        Me.pbTopLogo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbTopLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbTopLogo.Image = CType(resources.GetObject("pbTopLogo.Image"), System.Drawing.Image)
        Me.pbTopLogo.Location = New System.Drawing.Point(3, 3)
        Me.pbTopLogo.Name = "pbTopLogo"
        Me.TableLayoutPanel1.SetRowSpan(Me.pbTopLogo, 2)
        Me.pbTopLogo.Size = New System.Drawing.Size(48, 48)
        Me.pbTopLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbTopLogo.TabIndex = 0
        Me.pbTopLogo.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.pbTopLogo, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTopDetails, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1282, 54)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'lblTopDetails
        '
        Me.lblTopDetails.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(57, 36)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(205, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Edit the details for the selected movie."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(57, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Edit Movie"
        '
        'dlgTagManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 761)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.ssStatus)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "dlgTagManager"
        Me.Text = "Tag Manager"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblTop.ResumeLayout(False)
        Me.tblTop.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.tblMain.PerformLayout()
        Me.gbTVShows.ResumeLayout(False)
        CType(Me.dgvTVShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTagged.ResumeLayout(False)
        Me.scTagged.Panel1.ResumeLayout(False)
        Me.scTagged.Panel2.ResumeLayout(False)
        CType(Me.scTagged, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scTagged.ResumeLayout(False)
        CType(Me.dgvTagged_Movie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTagged_TVShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMovies.ResumeLayout(False)
        CType(Me.dgvMovie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTags.ResumeLayout(False)
        CType(Me.dgvTags, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ssStatus.ResumeLayout(False)
        Me.ssStatus.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlTop As Windows.Forms.Panel
    Friend WithEvents pnlMain As Windows.Forms.Panel
    Friend WithEvents ssStatus As Windows.Forms.StatusStrip
    Friend WithEvents tblTop As Windows.Forms.TableLayoutPanel
    Friend WithEvents tblMain As Windows.Forms.TableLayoutPanel
    Friend WithEvents tsslSpring As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsslStatus As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tspbStatus As Windows.Forms.ToolStripProgressBar
    Friend WithEvents pnlBottom As Windows.Forms.Panel
    Friend WithEvents tblBottom As Windows.Forms.TableLayoutPanel
    Friend WithEvents dgvTags As Windows.Forms.DataGridView
    Friend WithEvents btnOk As Windows.Forms.Button
    Friend WithEvents btnCancel As Windows.Forms.Button
    Friend WithEvents gbTags As Windows.Forms.GroupBox
    Friend WithEvents gbTVShows As Windows.Forms.GroupBox
    Friend WithEvents dgvTVShow As Windows.Forms.DataGridView
    Friend WithEvents gbMovies As Windows.Forms.GroupBox
    Friend WithEvents dgvMovie As Windows.Forms.DataGridView
    Friend WithEvents gbTagged As Windows.Forms.GroupBox
    Friend WithEvents dgvTagged_Movie As Windows.Forms.DataGridView
    Friend WithEvents scTagged As Windows.Forms.SplitContainer
    Friend WithEvents dgvTagged_TVShow As Windows.Forms.DataGridView
    Friend WithEvents lblTopTitle As Windows.Forms.Label
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents pbTopLogo As Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents lblTopDetails As Windows.Forms.Label
    Friend WithEvents Label1 As Windows.Forms.Label
End Class
