<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgBulkRenamer_Movie
    Inherits System.Windows.Forms.Form

#Region "Fields"

    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkRenamedOnly As System.Windows.Forms.CheckBox
    Friend WithEvents Close_Button As System.Windows.Forms.Button
    Friend WithEvents cmsMovieList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents dgvMoviesList As System.Windows.Forms.DataGridView
    Friend WithEvents lblFolderPatternNotSingle As System.Windows.Forms.Label
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents lblFilePattern As System.Windows.Forms.Label
    Friend WithEvents lblFolderPattern As System.Windows.Forms.Label
    Friend WithEvents pbCompile As System.Windows.Forms.ProgressBar
    Friend WithEvents pbTopLogo As System.Windows.Forms.PictureBox
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Rename_Button As System.Windows.Forms.Button
    Friend WithEvents tmrSimul As System.Windows.Forms.Timer
    Friend WithEvents tsmSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmLockAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmLockMovie As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmUnlockAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmUnlockMovie As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtFilePattern As System.Windows.Forms.TextBox
    Friend WithEvents txtFolderPattern As System.Windows.Forms.TextBox
    Friend WithEvents txtFolderPatternNotSingle As System.Windows.Forms.TextBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

#End Region 'Fields

#Region "Methods"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgBulkRenamer_Movie))
        Me.Close_Button = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pbTopLogo = New System.Windows.Forms.PictureBox()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.pbCompile = New System.Windows.Forms.ProgressBar()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Rename_Button = New System.Windows.Forms.Button()
        Me.tmrSimul = New System.Windows.Forms.Timer(Me.components)
        Me.dgvMoviesList = New System.Windows.Forms.DataGridView()
        Me.cmsMovieList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmLockMovie = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmUnlockMovie = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmLockAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmUnlockAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblFolderPattern = New System.Windows.Forms.Label()
        Me.lblFilePattern = New System.Windows.Forms.Label()
        Me.txtFilePattern = New System.Windows.Forms.TextBox()
        Me.txtFolderPattern = New System.Windows.Forms.TextBox()
        Me.txtFolderPatternNotSingle = New System.Windows.Forms.TextBox()
        Me.lblFolderPatternNotSingle = New System.Windows.Forms.Label()
        Me.chkRenamedOnly = New System.Windows.Forms.CheckBox()
        Me.btnFolderPatternHelp = New System.Windows.Forms.Button()
        Me.btnFilePatternHelp = New System.Windows.Forms.Button()
        Me.btnFolderPatternNotSingleHelp = New System.Windows.Forms.Button()
        Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlTop.SuspendLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCancel.SuspendLayout()
        CType(Me.dgvMoviesList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsMovieList.SuspendLayout()
        Me.tlpButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'Close_Button
        '
        Me.Close_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Close_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Close_Button.Location = New System.Drawing.Point(76, 3)
        Me.Close_Button.Name = "Close_Button"
        Me.Close_Button.Size = New System.Drawing.Size(67, 23)
        Me.Close_Button.TabIndex = 0
        Me.Close_Button.Text = "Close"
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
        Me.pnlTop.Size = New System.Drawing.Size(992, 64)
        Me.pnlTop.TabIndex = 2
        '
        'lblTopDetails
        '
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(64, 38)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(136, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Rename movies and files"
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(61, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(173, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Bulk Renamer"
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
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.LightGray
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.pbCompile)
        Me.pnlCancel.Controls.Add(Me.lblCompiling)
        Me.pnlCancel.Controls.Add(Me.lblFile)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Location = New System.Drawing.Point(295, 196)
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
        Me.lblCompiling.Text = "Compiling Movie List..."
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
        'Rename_Button
        '
        Me.Rename_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Rename_Button.Enabled = False
        Me.Rename_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Rename_Button.Location = New System.Drawing.Point(3, 3)
        Me.Rename_Button.Name = "Rename_Button"
        Me.Rename_Button.Size = New System.Drawing.Size(67, 23)
        Me.Rename_Button.TabIndex = 1
        Me.Rename_Button.Text = "Rename"
        '
        'tmrSimul
        '
        Me.tmrSimul.Interval = 250
        '
        'dgvMoviesList
        '
        Me.dgvMoviesList.AllowUserToAddRows = False
        Me.dgvMoviesList.AllowUserToDeleteRows = False
        Me.dgvMoviesList.AllowUserToResizeRows = False
        Me.dgvMoviesList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMoviesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMoviesList.ContextMenuStrip = Me.cmsMovieList
        Me.dgvMoviesList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvMoviesList.Location = New System.Drawing.Point(12, 76)
        Me.dgvMoviesList.Name = "dgvMoviesList"
        Me.dgvMoviesList.RowHeadersVisible = False
        Me.dgvMoviesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMoviesList.ShowEditingIcon = False
        Me.dgvMoviesList.Size = New System.Drawing.Size(966, 316)
        Me.dgvMoviesList.TabIndex = 3
        '
        'cmsMovieList
        '
        Me.cmsMovieList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmLockMovie, Me.tsmUnlockMovie, Me.tsmSeparator, Me.tsmLockAll, Me.tsmUnlockAll})
        Me.cmsMovieList.Name = "cmsMovieList"
        Me.cmsMovieList.Size = New System.Drawing.Size(161, 98)
        '
        'tsmLockMovie
        '
        Me.tsmLockMovie.Name = "tsmLockMovie"
        Me.tsmLockMovie.Size = New System.Drawing.Size(160, 22)
        Me.tsmLockMovie.Text = "Lock Movie(s)"
        '
        'tsmUnlockMovie
        '
        Me.tsmUnlockMovie.Name = "tsmUnlockMovie"
        Me.tsmUnlockMovie.Size = New System.Drawing.Size(160, 22)
        Me.tsmUnlockMovie.Text = "Unlock Movie(s)"
        '
        'tsmSeparator
        '
        Me.tsmSeparator.Name = "tsmSeparator"
        Me.tsmSeparator.Size = New System.Drawing.Size(157, 6)
        '
        'tsmLockAll
        '
        Me.tsmLockAll.Name = "tsmLockAll"
        Me.tsmLockAll.Size = New System.Drawing.Size(160, 22)
        Me.tsmLockAll.Text = "Lock All"
        '
        'tsmUnlockAll
        '
        Me.tsmUnlockAll.Name = "tsmUnlockAll"
        Me.tsmUnlockAll.Size = New System.Drawing.Size(160, 22)
        Me.tsmUnlockAll.Text = "Unlock All"
        '
        'lblFolderPattern
        '
        Me.lblFolderPattern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFolderPattern.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFolderPattern.Location = New System.Drawing.Point(12, 401)
        Me.lblFolderPattern.Name = "lblFolderPattern"
        Me.lblFolderPattern.Size = New System.Drawing.Size(298, 13)
        Me.lblFolderPattern.TabIndex = 5
        Me.lblFolderPattern.Text = "Folder Pattern (for Single movie in Folder)"
        Me.lblFolderPattern.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFilePattern
        '
        Me.lblFilePattern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFilePattern.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilePattern.Location = New System.Drawing.Point(522, 401)
        Me.lblFilePattern.Name = "lblFilePattern"
        Me.lblFilePattern.Size = New System.Drawing.Size(140, 13)
        Me.lblFilePattern.TabIndex = 8
        Me.lblFilePattern.Text = "File Pattern"
        Me.lblFilePattern.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFilePattern
        '
        Me.txtFilePattern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFilePattern.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtFilePattern.Location = New System.Drawing.Point(668, 398)
        Me.txtFilePattern.Name = "txtFilePattern"
        Me.txtFilePattern.Size = New System.Drawing.Size(224, 22)
        Me.txtFilePattern.TabIndex = 9
        Me.txtFilePattern.Text = "$T"
        '
        'txtFolderPattern
        '
        Me.txtFolderPattern.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFolderPattern.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtFolderPattern.Location = New System.Drawing.Point(316, 398)
        Me.txtFolderPattern.Name = "txtFolderPattern"
        Me.txtFolderPattern.Size = New System.Drawing.Size(200, 22)
        Me.txtFolderPattern.TabIndex = 6
        Me.txtFolderPattern.Text = "$T ($Y)"
        '
        'txtFolderPatternNotSingle
        '
        Me.txtFolderPatternNotSingle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFolderPatternNotSingle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtFolderPatternNotSingle.Location = New System.Drawing.Point(316, 424)
        Me.txtFolderPatternNotSingle.Name = "txtFolderPatternNotSingle"
        Me.txtFolderPatternNotSingle.Size = New System.Drawing.Size(200, 22)
        Me.txtFolderPatternNotSingle.TabIndex = 12
        Me.txtFolderPatternNotSingle.Text = "$D"
        '
        'lblFolderPatternNotSingle
        '
        Me.lblFolderPatternNotSingle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFolderPatternNotSingle.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFolderPatternNotSingle.Location = New System.Drawing.Point(12, 427)
        Me.lblFolderPatternNotSingle.Name = "lblFolderPatternNotSingle"
        Me.lblFolderPatternNotSingle.Size = New System.Drawing.Size(296, 13)
        Me.lblFolderPatternNotSingle.TabIndex = 11
        Me.lblFolderPatternNotSingle.Text = "Folder Pattern (for Multiple movies in Folder)"
        Me.lblFolderPatternNotSingle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkRenamedOnly
        '
        Me.chkRenamedOnly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkRenamedOnly.AutoSize = True
        Me.chkRenamedOnly.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkRenamedOnly.Location = New System.Drawing.Point(668, 424)
        Me.chkRenamedOnly.Name = "chkRenamedOnly"
        Me.chkRenamedOnly.Size = New System.Drawing.Size(244, 17)
        Me.chkRenamedOnly.TabIndex = 14
        Me.chkRenamedOnly.Text = "Display Only Movies That Will Be Renamed"
        Me.chkRenamedOnly.UseVisualStyleBackColor = True
        '
        'btnFolderPatternHelp
        '
        Me.btnFolderPatternHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFolderPatternHelp.Image = CType(resources.GetObject("btnFolderPatternHelp.Image"), System.Drawing.Image)
        Me.btnFolderPatternHelp.Location = New System.Drawing.Point(517, 400)
        Me.btnFolderPatternHelp.Name = "btnFolderPatternHelp"
        Me.btnFolderPatternHelp.Size = New System.Drawing.Size(17, 19)
        Me.btnFolderPatternHelp.TabIndex = 7
        Me.btnFolderPatternHelp.UseVisualStyleBackColor = True
        '
        'btnFilePatternHelp
        '
        Me.btnFilePatternHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFilePatternHelp.Image = CType(resources.GetObject("btnFilePatternHelp.Image"), System.Drawing.Image)
        Me.btnFilePatternHelp.Location = New System.Drawing.Point(895, 398)
        Me.btnFilePatternHelp.Name = "btnFilePatternHelp"
        Me.btnFilePatternHelp.Size = New System.Drawing.Size(17, 19)
        Me.btnFilePatternHelp.TabIndex = 10
        Me.btnFilePatternHelp.UseVisualStyleBackColor = True
        '
        'btnFolderPatternNotSingleHelp
        '
        Me.btnFolderPatternNotSingleHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFolderPatternNotSingleHelp.Image = CType(resources.GetObject("btnFolderPatternNotSingleHelp.Image"), System.Drawing.Image)
        Me.btnFolderPatternNotSingleHelp.Location = New System.Drawing.Point(517, 427)
        Me.btnFolderPatternNotSingleHelp.Name = "btnFolderPatternNotSingleHelp"
        Me.btnFolderPatternNotSingleHelp.Size = New System.Drawing.Size(17, 19)
        Me.btnFolderPatternNotSingleHelp.TabIndex = 13
        Me.btnFolderPatternNotSingleHelp.UseVisualStyleBackColor = True
        '
        'tlpButtons
        '
        Me.tlpButtons.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpButtons.ColumnCount = 2
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Controls.Add(Me.Close_Button, 1, 0)
        Me.tlpButtons.Controls.Add(Me.Rename_Button, 0, 0)
        Me.tlpButtons.Location = New System.Drawing.Point(845, 442)
        Me.tlpButtons.Name = "tlpButtons"
        Me.tlpButtons.RowCount = 1
        Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpButtons.Size = New System.Drawing.Size(146, 29)
        Me.tlpButtons.TabIndex = 15
        '
        'dlgBulkRenamer_Movie
        '
        Me.AcceptButton = Me.Rename_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Close_Button
        Me.ClientSize = New System.Drawing.Size(992, 472)
        Me.Controls.Add(Me.tlpButtons)
        Me.Controls.Add(Me.btnFolderPatternNotSingleHelp)
        Me.Controls.Add(Me.btnFilePatternHelp)
        Me.Controls.Add(Me.btnFolderPatternHelp)
        Me.Controls.Add(Me.txtFolderPatternNotSingle)
        Me.Controls.Add(Me.chkRenamedOnly)
        Me.Controls.Add(Me.lblFolderPatternNotSingle)
        Me.Controls.Add(Me.txtFolderPattern)
        Me.Controls.Add(Me.pnlCancel)
        Me.Controls.Add(Me.lblFolderPattern)
        Me.Controls.Add(Me.lblFilePattern)
        Me.Controls.Add(Me.txtFilePattern)
        Me.Controls.Add(Me.dgvMoviesList)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1008, 511)
        Me.Name = "dlgBulkRenamer_Movie"
        Me.ShowInTaskbar = False
        Me.Text = "Bulk Renamer"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.pbTopLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCancel.ResumeLayout(False)
        CType(Me.dgvMoviesList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsMovieList.ResumeLayout(False)
        Me.tlpButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnFolderPatternHelp As System.Windows.Forms.Button
    Friend WithEvents btnFilePatternHelp As System.Windows.Forms.Button
    Friend WithEvents btnFolderPatternNotSingleHelp As System.Windows.Forms.Button
    Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel

#End Region 'Methods

End Class