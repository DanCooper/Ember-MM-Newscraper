<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgBulkRenamer_TV
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgBulkRenamer_TV))
        Me.tsmUnlockEpisode = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.chkRenamedOnly = New System.Windows.Forms.CheckBox()
        Me.txtFolderPatternShows = New System.Windows.Forms.TextBox()
        Me.tsmLockAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblFolderPatternShows = New System.Windows.Forms.Label()
        Me.lblFilePatternEpisodes = New System.Windows.Forms.Label()
        Me.tsmUnlockAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtFilePatternEpisodes = New System.Windows.Forms.TextBox()
        Me.tsmLockEpisode = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvEpisodesList = New System.Windows.Forms.DataGridView()
        Me.cmsEpisodeList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.Close_Button = New System.Windows.Forms.Button()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.pbCompile = New System.Windows.Forms.ProgressBar()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.tmrSimul = New System.Windows.Forms.Timer(Me.components)
        Me.Rename_Button = New System.Windows.Forms.Button()
        Me.btnFilePatternHelp = New System.Windows.Forms.Button()
        Me.btnFolderPatternHelp = New System.Windows.Forms.Button()
        Me.lblFolderPatternSeasons = New System.Windows.Forms.Label()
        Me.txtFolderPatternSeasons = New System.Windows.Forms.TextBox()
        CType(Me.dgvEpisodesList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsEpisodeList.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCancel.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsmUnlockEpisode
        '
        Me.tsmUnlockEpisode.Name = "tsmUnlockEpisode"
        Me.tsmUnlockEpisode.Size = New System.Drawing.Size(173, 22)
        Me.tsmUnlockEpisode.Text = "Unlock TV Show(s)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(170, 6)
        '
        'chkRenamedOnly
        '
        Me.chkRenamedOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkRenamedOnly.AutoSize = True
        Me.chkRenamedOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkRenamedOnly.Location = New System.Drawing.Point(668, 442)
        Me.chkRenamedOnly.Name = "chkRenamedOnly"
        Me.chkRenamedOnly.Size = New System.Drawing.Size(254, 17)
        Me.chkRenamedOnly.TabIndex = 11
        Me.chkRenamedOnly.Text = "Display Only Episodes That Will Be Renamed"
        Me.chkRenamedOnly.UseVisualStyleBackColor = True
        '
        'txtFolderPatternShows
        '
        Me.txtFolderPatternShows.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFolderPatternShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtFolderPatternShows.Location = New System.Drawing.Point(316, 416)
        Me.txtFolderPatternShows.Name = "txtFolderPatternShows"
        Me.txtFolderPatternShows.Size = New System.Drawing.Size(200, 22)
        Me.txtFolderPatternShows.TabIndex = 6
        Me.txtFolderPatternShows.Text = "$T ($Y)"
        Me.txtFolderPatternShows.Visible = False
        '
        'tsmLockAll
        '
        Me.tsmLockAll.Name = "tsmLockAll"
        Me.tsmLockAll.Size = New System.Drawing.Size(173, 22)
        Me.tsmLockAll.Text = "Lock All"
        '
        'lblFolderPatternShows
        '
        Me.lblFolderPatternShows.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFolderPatternShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFolderPatternShows.Location = New System.Drawing.Point(12, 419)
        Me.lblFolderPatternShows.Name = "lblFolderPatternShows"
        Me.lblFolderPatternShows.Size = New System.Drawing.Size(298, 13)
        Me.lblFolderPatternShows.TabIndex = 5
        Me.lblFolderPatternShows.Text = "Show Folders Pattern"
        Me.lblFolderPatternShows.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblFolderPatternShows.Visible = False
        '
        'lblFilePatternEpisodes
        '
        Me.lblFilePatternEpisodes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFilePatternEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilePatternEpisodes.Location = New System.Drawing.Point(540, 419)
        Me.lblFilePatternEpisodes.Name = "lblFilePatternEpisodes"
        Me.lblFilePatternEpisodes.Size = New System.Drawing.Size(122, 13)
        Me.lblFilePatternEpisodes.TabIndex = 8
        Me.lblFilePatternEpisodes.Text = "Episode Files Pattern"
        Me.lblFilePatternEpisodes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tsmUnlockAll
        '
        Me.tsmUnlockAll.Name = "tsmUnlockAll"
        Me.tsmUnlockAll.Size = New System.Drawing.Size(173, 22)
        Me.tsmUnlockAll.Text = "Unlock All"
        '
        'txtFilePatternEpisodes
        '
        Me.txtFilePatternEpisodes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFilePatternEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtFilePatternEpisodes.Location = New System.Drawing.Point(668, 416)
        Me.txtFilePatternEpisodes.Name = "txtFilePatternEpisodes"
        Me.txtFilePatternEpisodes.Size = New System.Drawing.Size(224, 22)
        Me.txtFilePatternEpisodes.TabIndex = 9
        Me.txtFilePatternEpisodes.Text = "$T"
        '
        'tsmLockEpisode
        '
        Me.tsmLockEpisode.Name = "tsmLockEpisode"
        Me.tsmLockEpisode.Size = New System.Drawing.Size(173, 22)
        Me.tsmLockEpisode.Text = "Lock TV Show(s)"
        '
        'dgvEpisodesList
        '
        Me.dgvEpisodesList.AllowUserToAddRows = False
        Me.dgvEpisodesList.AllowUserToDeleteRows = False
        Me.dgvEpisodesList.AllowUserToResizeRows = False
        Me.dgvEpisodesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvEpisodesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEpisodesList.ContextMenuStrip = Me.cmsEpisodeList
        Me.dgvEpisodesList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvEpisodesList.Location = New System.Drawing.Point(12, 94)
        Me.dgvEpisodesList.Name = "dgvEpisodesList"
        Me.dgvEpisodesList.RowHeadersVisible = False
        Me.dgvEpisodesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEpisodesList.ShowEditingIcon = False
        Me.dgvEpisodesList.Size = New System.Drawing.Size(966, 316)
        Me.dgvEpisodesList.TabIndex = 3
        '
        'cmsEpisodeList
        '
        Me.cmsEpisodeList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmLockEpisode, Me.tsmUnlockEpisode, Me.ToolStripSeparator1, Me.tsmLockAll, Me.tsmUnlockAll})
        Me.cmsEpisodeList.Name = "cmsMovieList"
        Me.cmsEpisodeList.Size = New System.Drawing.Size(174, 98)
        '
        'lblTopDetails
        '
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(64, 38)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(149, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Rename TV Shows and files"
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
        Me.pnlTop.Size = New System.Drawing.Size(1000, 64)
        Me.pnlTop.TabIndex = 2
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(61, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(211, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "TV Bulk Renamer"
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
        'Close_Button
        '
        Me.Close_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Close_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Close_Button.Location = New System.Drawing.Point(898, 463)
        Me.Close_Button.Name = "Close_Button"
        Me.Close_Button.Size = New System.Drawing.Size(80, 23)
        Me.Close_Button.TabIndex = 0
        Me.Close_Button.Text = "Close"
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.LightGray
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.pbCompile)
        Me.pnlCancel.Controls.Add(Me.lblCompiling)
        Me.pnlCancel.Controls.Add(Me.lblFile)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Location = New System.Drawing.Point(295, 214)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(403, 76)
        Me.pnlCancel.TabIndex = 4
        Me.pnlCancel.Visible = False
        '
        'pbCompile
        '
        Me.pbCompile.Location = New System.Drawing.Point(8, 36)
        Me.pbCompile.MarqueeAnimationSpeed = 25
        Me.pbCompile.Name = "pbCompile"
        Me.pbCompile.Size = New System.Drawing.Size(388, 18)
        Me.pbCompile.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbCompile.TabIndex = 3
        '
        'lblCompiling
        '
        Me.lblCompiling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCompiling.Location = New System.Drawing.Point(3, 12)
        Me.lblCompiling.Name = "lblCompiling"
        Me.lblCompiling.Size = New System.Drawing.Size(186, 20)
        Me.lblCompiling.TabIndex = 0
        Me.lblCompiling.Text = "Compiling Tv Show List..."
        Me.lblCompiling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCompiling.Visible = False
        '
        'lblFile
        '
        Me.lblFile.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFile.Location = New System.Drawing.Point(6, 57)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(390, 13)
        Me.lblFile.TabIndex = 4
        Me.lblFile.Text = "File ..."
        '
        'lblCanceling
        '
        Me.lblCanceling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCanceling.Location = New System.Drawing.Point(110, 12)
        Me.lblCanceling.Name = "lblCanceling"
        Me.lblCanceling.Size = New System.Drawing.Size(186, 20)
        Me.lblCanceling.TabIndex = 1
        Me.lblCanceling.Text = "Canceling Compilation..."
        Me.lblCanceling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCanceling.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'tmrSimul
        '
        Me.tmrSimul.Interval = 250
        '
        'Rename_Button
        '
        Me.Rename_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Rename_Button.Enabled = False
        Me.Rename_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Rename_Button.Location = New System.Drawing.Point(812, 463)
        Me.Rename_Button.Name = "Rename_Button"
        Me.Rename_Button.Size = New System.Drawing.Size(80, 23)
        Me.Rename_Button.TabIndex = 1
        Me.Rename_Button.Text = "Rename"
        '
        'btnFilePatternHelp
        '
        Me.btnFilePatternHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFilePatternHelp.Image = CType(resources.GetObject("btnFilePatternHelp.Image"), System.Drawing.Image)
        Me.btnFilePatternHelp.Location = New System.Drawing.Point(895, 416)
        Me.btnFilePatternHelp.Name = "btnFilePatternHelp"
        Me.btnFilePatternHelp.Size = New System.Drawing.Size(17, 19)
        Me.btnFilePatternHelp.TabIndex = 10
        Me.btnFilePatternHelp.UseVisualStyleBackColor = True
        '
        'btnFolderPatternHelp
        '
        Me.btnFolderPatternHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFolderPatternHelp.Image = CType(resources.GetObject("btnFolderPatternHelp.Image"), System.Drawing.Image)
        Me.btnFolderPatternHelp.Location = New System.Drawing.Point(522, 444)
        Me.btnFolderPatternHelp.Name = "btnFolderPatternHelp"
        Me.btnFolderPatternHelp.Size = New System.Drawing.Size(17, 19)
        Me.btnFolderPatternHelp.TabIndex = 7
        Me.btnFolderPatternHelp.UseVisualStyleBackColor = True
        '
        'lblFolderPatternSeasons
        '
        Me.lblFolderPatternSeasons.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFolderPatternSeasons.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFolderPatternSeasons.Location = New System.Drawing.Point(12, 447)
        Me.lblFolderPatternSeasons.Name = "lblFolderPatternSeasons"
        Me.lblFolderPatternSeasons.Size = New System.Drawing.Size(298, 13)
        Me.lblFolderPatternSeasons.TabIndex = 5
        Me.lblFolderPatternSeasons.Text = "Season Folders Pattern"
        Me.lblFolderPatternSeasons.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFolderPatternSeasons
        '
        Me.txtFolderPatternSeasons.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFolderPatternSeasons.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtFolderPatternSeasons.Location = New System.Drawing.Point(316, 444)
        Me.txtFolderPatternSeasons.Name = "txtFolderPatternSeasons"
        Me.txtFolderPatternSeasons.Size = New System.Drawing.Size(200, 22)
        Me.txtFolderPatternSeasons.TabIndex = 6
        Me.txtFolderPatternSeasons.Text = "$T ($Y)"
        '
        'dlgBulkRenamer_TV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1000, 505)
        Me.Controls.Add(Me.btnFilePatternHelp)
        Me.Controls.Add(Me.btnFolderPatternHelp)
        Me.Controls.Add(Me.chkRenamedOnly)
        Me.Controls.Add(Me.txtFolderPatternSeasons)
        Me.Controls.Add(Me.lblFolderPatternSeasons)
        Me.Controls.Add(Me.txtFolderPatternShows)
        Me.Controls.Add(Me.lblFolderPatternShows)
        Me.Controls.Add(Me.lblFilePatternEpisodes)
        Me.Controls.Add(Me.txtFilePatternEpisodes)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.Close_Button)
        Me.Controls.Add(Me.pnlCancel)
        Me.Controls.Add(Me.Rename_Button)
        Me.Controls.Add(Me.dgvEpisodesList)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "dlgBulkRenamer_TV"
        Me.ShowInTaskbar = False
        Me.Text = "TV Bulk Renamer"
        CType(Me.dgvEpisodesList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsEpisodeList.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCancel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnFilePatternHelp As System.Windows.Forms.Button
    Friend WithEvents btnFolderPatternHelp As System.Windows.Forms.Button
    Friend WithEvents tsmUnlockEpisode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkRenamedOnly As System.Windows.Forms.CheckBox
    Friend WithEvents txtFolderPatternShows As System.Windows.Forms.TextBox
    Friend WithEvents tsmLockAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblFolderPatternShows As System.Windows.Forms.Label
    Friend WithEvents lblFilePatternEpisodes As System.Windows.Forms.Label
    Friend WithEvents tsmUnlockAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtFilePatternEpisodes As System.Windows.Forms.TextBox
    Friend WithEvents tsmLockEpisode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvEpisodesList As System.Windows.Forms.DataGridView
    Friend WithEvents cmsEpisodeList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents Close_Button As System.Windows.Forms.Button
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents pbCompile As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents tmrSimul As System.Windows.Forms.Timer
    Friend WithEvents Rename_Button As System.Windows.Forms.Button
    Friend WithEvents lblFolderPatternSeasons As System.Windows.Forms.Label
    Friend WithEvents txtFolderPatternSeasons As System.Windows.Forms.TextBox

End Class
