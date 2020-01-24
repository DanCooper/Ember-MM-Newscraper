﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgTagManager
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgTagManager))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.picTopTitle = New System.Windows.Forms.PictureBox()
        Me.lblTopDetails = New System.Windows.Forms.Label()
        Me.lblTopTitle = New System.Windows.Forms.Label()
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.pnlSaving = New System.Windows.Forms.Panel()
        Me.lblSaving = New System.Windows.Forms.Label()
        Me.prbSaving = New System.Windows.Forms.ProgressBar()
        Me.prbCompile = New System.Windows.Forms.ProgressBar()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovies = New System.Windows.Forms.GroupBox()
        Me.gbMoviesFilter = New System.Windows.Forms.GroupBox()
        Me.rdMoviesFiltered = New System.Windows.Forms.RadioButton()
        Me.rdMoviesAll = New System.Windows.Forms.RadioButton()
        Me.rdMoviesSelected = New System.Windows.Forms.RadioButton()
        Me.dgvMovies = New System.Windows.Forms.DataGridView()
        Me.btnAddMovie = New System.Windows.Forms.Button()
        Me.gbTags = New System.Windows.Forms.GroupBox()
        Me.txtEditTag = New System.Windows.Forms.TextBox()
        Me.btnRemoveTag = New System.Windows.Forms.Button()
        Me.btnEditTag = New System.Windows.Forms.Button()
        Me.btnNewTag = New System.Windows.Forms.Button()
        Me.lbTags = New System.Windows.Forms.ListBox()
        Me.gbMoviesInTag = New System.Windows.Forms.GroupBox()
        Me.lblCurrentTag = New System.Windows.Forms.Label()
        Me.btnRemoveMovie = New System.Windows.Forms.Button()
        Me.lbMoviesInTag = New System.Windows.Forms.ListBox()
        Me.btnglobalTagsSync = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlTop.SuspendLayout()
        CType(Me.picTopTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCancel.SuspendLayout()
        Me.pnlSaving.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        Me.gbMovies.SuspendLayout()
        Me.gbMoviesFilter.SuspendLayout()
        CType(Me.dgvMovies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTags.SuspendLayout()
        Me.gbMoviesInTag.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(84, 32)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Close"
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.picTopTitle)
        Me.pnlTop.Controls.Add(Me.lblTopDetails)
        Me.pnlTop.Controls.Add(Me.lblTopTitle)
        Me.pnlTop.Controls.Add(Me.pnlCancel)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(925, 64)
        Me.pnlTop.TabIndex = 1
        '
        'picTopTitle
        '
        Me.picTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.picTopTitle.Image = CType(resources.GetObject("picTopTitle.Image"), System.Drawing.Image)
        Me.picTopTitle.Location = New System.Drawing.Point(11, 7)
        Me.picTopTitle.Name = "picTopTitle"
        Me.picTopTitle.Size = New System.Drawing.Size(36, 48)
        Me.picTopTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picTopTitle.TabIndex = 41
        Me.picTopTitle.TabStop = False
        '
        'lblTopDetails
        '
        Me.lblTopDetails.AutoSize = True
        Me.lblTopDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblTopDetails.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopDetails.ForeColor = System.Drawing.Color.White
        Me.lblTopDetails.Location = New System.Drawing.Point(61, 38)
        Me.lblTopDetails.Name = "lblTopDetails"
        Me.lblTopDetails.Size = New System.Drawing.Size(111, 13)
        Me.lblTopDetails.TabIndex = 1
        Me.lblTopDetails.Text = "Manage XBMC Tags"
        '
        'lblTopTitle
        '
        Me.lblTopTitle.AutoSize = True
        Me.lblTopTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTopTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTopTitle.ForeColor = System.Drawing.Color.White
        Me.lblTopTitle.Location = New System.Drawing.Point(58, 3)
        Me.lblTopTitle.Name = "lblTopTitle"
        Me.lblTopTitle.Size = New System.Drawing.Size(164, 32)
        Me.lblTopTitle.TabIndex = 0
        Me.lblTopTitle.Text = "Tag Manager"
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
        Me.pnlCancel.Location = New System.Drawing.Point(365, 3)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(403, 76)
        Me.pnlCancel.TabIndex = 40
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
        Me.lblCompiling.Text = "Loading Movies..."
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
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 64)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(925, 550)
        Me.pnlMain.TabIndex = 16
        '
        'tblMain
        '
        Me.tblMain.AutoScroll = True
        Me.tblMain.AutoSize = True
        Me.tblMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblMain.ColumnCount = 4
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.gbMovies, 2, 0)
        Me.tblMain.Controls.Add(Me.gbTags, 0, 0)
        Me.tblMain.Controls.Add(Me.gbMoviesInTag, 1, 0)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 3
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(925, 550)
        Me.tblMain.TabIndex = 3
        '
        'gbMovies
        '
        Me.gbMovies.Controls.Add(Me.gbMoviesFilter)
        Me.gbMovies.Controls.Add(Me.dgvMovies)
        Me.gbMovies.Controls.Add(Me.btnAddMovie)
        Me.gbMovies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovies.Location = New System.Drawing.Point(470, 3)
        Me.gbMovies.Name = "gbMovies"
        Me.gbMovies.Size = New System.Drawing.Size(443, 541)
        Me.gbMovies.TabIndex = 7
        Me.gbMovies.TabStop = False
        Me.gbMovies.Text = "available Movies"
        '
        'gbMoviesFilter
        '
        Me.gbMoviesFilter.Controls.Add(Me.rdMoviesFiltered)
        Me.gbMoviesFilter.Controls.Add(Me.rdMoviesAll)
        Me.gbMoviesFilter.Controls.Add(Me.rdMoviesSelected)
        Me.gbMoviesFilter.Location = New System.Drawing.Point(257, 21)
        Me.gbMoviesFilter.Name = "gbMoviesFilter"
        Me.gbMoviesFilter.Size = New System.Drawing.Size(164, 90)
        Me.gbMoviesFilter.TabIndex = 57
        Me.gbMoviesFilter.TabStop = False
        Me.gbMoviesFilter.Text = "Filter"
        '
        'rdMoviesFiltered
        '
        Me.rdMoviesFiltered.AutoSize = True
        Me.rdMoviesFiltered.Location = New System.Drawing.Point(6, 21)
        Me.rdMoviesFiltered.Name = "rdMoviesFiltered"
        Me.rdMoviesFiltered.Size = New System.Drawing.Size(104, 17)
        Me.rdMoviesFiltered.TabIndex = 54
        Me.rdMoviesFiltered.TabStop = True
        Me.rdMoviesFiltered.Text = "Filtered movies"
        Me.rdMoviesFiltered.UseVisualStyleBackColor = True
        '
        'rdMoviesAll
        '
        Me.rdMoviesAll.AutoSize = True
        Me.rdMoviesAll.Location = New System.Drawing.Point(6, 67)
        Me.rdMoviesAll.Name = "rdMoviesAll"
        Me.rdMoviesAll.Size = New System.Drawing.Size(79, 17)
        Me.rdMoviesAll.TabIndex = 56
        Me.rdMoviesAll.TabStop = True
        Me.rdMoviesAll.Text = "All movies"
        Me.rdMoviesAll.UseVisualStyleBackColor = True
        '
        'rdMoviesSelected
        '
        Me.rdMoviesSelected.AutoSize = True
        Me.rdMoviesSelected.Location = New System.Drawing.Point(6, 44)
        Me.rdMoviesSelected.Name = "rdMoviesSelected"
        Me.rdMoviesSelected.Size = New System.Drawing.Size(108, 17)
        Me.rdMoviesSelected.TabIndex = 55
        Me.rdMoviesSelected.TabStop = True
        Me.rdMoviesSelected.Text = "Selected movies"
        Me.rdMoviesSelected.UseVisualStyleBackColor = True
        '
        'dgvMovies
        '
        Me.dgvMovies.AllowUserToAddRows = False
        Me.dgvMovies.AllowUserToDeleteRows = False
        Me.dgvMovies.AllowUserToResizeColumns = False
        Me.dgvMovies.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.dgvMovies.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvMovies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMovies.BackgroundColor = System.Drawing.Color.White
        Me.dgvMovies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvMovies.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvMovies.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable
        Me.dgvMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMovies.Enabled = False
        Me.dgvMovies.GridColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvMovies.Location = New System.Drawing.Point(8, 21)
        Me.dgvMovies.Name = "dgvMovies"
        Me.dgvMovies.ReadOnly = True
        Me.dgvMovies.RowHeadersVisible = False
        Me.dgvMovies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMovies.ShowCellErrors = False
        Me.dgvMovies.ShowRowErrors = False
        Me.dgvMovies.Size = New System.Drawing.Size(243, 485)
        Me.dgvMovies.StandardTab = True
        Me.dgvMovies.TabIndex = 51
        '
        'btnAddMovie
        '
        Me.btnAddMovie.Enabled = False
        Me.btnAddMovie.Image = CType(resources.GetObject("btnAddMovie.Image"), System.Drawing.Image)
        Me.btnAddMovie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddMovie.Location = New System.Drawing.Point(10, 512)
        Me.btnAddMovie.Name = "btnAddMovie"
        Me.btnAddMovie.Size = New System.Drawing.Size(23, 23)
        Me.btnAddMovie.TabIndex = 1
        Me.btnAddMovie.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddMovie.UseVisualStyleBackColor = True
        '
        'gbTags
        '
        Me.gbTags.Controls.Add(Me.txtEditTag)
        Me.gbTags.Controls.Add(Me.btnRemoveTag)
        Me.gbTags.Controls.Add(Me.btnEditTag)
        Me.gbTags.Controls.Add(Me.btnNewTag)
        Me.gbTags.Controls.Add(Me.lbTags)
        Me.gbTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTags.Location = New System.Drawing.Point(3, 3)
        Me.gbTags.Name = "gbTags"
        Me.gbTags.Size = New System.Drawing.Size(221, 541)
        Me.gbTags.TabIndex = 5
        Me.gbTags.TabStop = False
        Me.gbTags.Text = "Tags"
        '
        'txtEditTag
        '
        Me.txtEditTag.Location = New System.Drawing.Point(9, 484)
        Me.txtEditTag.Name = "txtEditTag"
        Me.txtEditTag.Size = New System.Drawing.Size(178, 22)
        Me.txtEditTag.TabIndex = 39
        '
        'btnRemoveTag
        '
        Me.btnRemoveTag.Image = CType(resources.GetObject("btnRemoveTag.Image"), System.Drawing.Image)
        Me.btnRemoveTag.Location = New System.Drawing.Point(191, 511)
        Me.btnRemoveTag.Name = "btnRemoveTag"
        Me.btnRemoveTag.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveTag.TabIndex = 3
        Me.btnRemoveTag.UseVisualStyleBackColor = True
        '
        'btnEditTag
        '
        Me.btnEditTag.Image = CType(resources.GetObject("btnEditTag.Image"), System.Drawing.Image)
        Me.btnEditTag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditTag.Location = New System.Drawing.Point(191, 484)
        Me.btnEditTag.Name = "btnEditTag"
        Me.btnEditTag.Size = New System.Drawing.Size(23, 23)
        Me.btnEditTag.TabIndex = 2
        Me.btnEditTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditTag.UseVisualStyleBackColor = True
        '
        'btnNewTag
        '
        Me.btnNewTag.Image = CType(resources.GetObject("btnNewTag.Image"), System.Drawing.Image)
        Me.btnNewTag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNewTag.Location = New System.Drawing.Point(9, 511)
        Me.btnNewTag.Name = "btnNewTag"
        Me.btnNewTag.Size = New System.Drawing.Size(23, 23)
        Me.btnNewTag.TabIndex = 1
        Me.btnNewTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNewTag.UseVisualStyleBackColor = True
        '
        'lbTags
        '
        Me.lbTags.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbTags.Location = New System.Drawing.Point(6, 20)
        Me.lbTags.Name = "lbTags"
        Me.lbTags.Size = New System.Drawing.Size(205, 459)
        Me.lbTags.Sorted = True
        Me.lbTags.TabIndex = 0
        '
        'gbMoviesInTag
        '
        Me.gbMoviesInTag.Controls.Add(Me.lblCurrentTag)
        Me.gbMoviesInTag.Controls.Add(Me.btnRemoveMovie)
        Me.gbMoviesInTag.Controls.Add(Me.lbMoviesInTag)
        Me.gbMoviesInTag.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMoviesInTag.Location = New System.Drawing.Point(230, 3)
        Me.gbMoviesInTag.Name = "gbMoviesInTag"
        Me.gbMoviesInTag.Size = New System.Drawing.Size(234, 541)
        Me.gbMoviesInTag.TabIndex = 6
        Me.gbMoviesInTag.TabStop = False
        Me.gbMoviesInTag.Text = "Movies In Tag"
        '
        'lblCurrentTag
        '
        Me.lblCurrentTag.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCurrentTag.Location = New System.Drawing.Point(6, 20)
        Me.lblCurrentTag.Name = "lblCurrentTag"
        Me.lblCurrentTag.Size = New System.Drawing.Size(102, 23)
        Me.lblCurrentTag.TabIndex = 0
        Me.lblCurrentTag.Text = "None Selected"
        Me.lblCurrentTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnRemoveMovie
        '
        Me.btnRemoveMovie.Enabled = False
        Me.btnRemoveMovie.Image = CType(resources.GetObject("btnRemoveMovie.Image"), System.Drawing.Image)
        Me.btnRemoveMovie.Location = New System.Drawing.Point(205, 512)
        Me.btnRemoveMovie.Name = "btnRemoveMovie"
        Me.btnRemoveMovie.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveMovie.TabIndex = 4
        Me.btnRemoveMovie.UseVisualStyleBackColor = True
        '
        'lbMoviesInTag
        '
        Me.lbMoviesInTag.Enabled = False
        Me.lbMoviesInTag.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbMoviesInTag.FormattingEnabled = True
        Me.lbMoviesInTag.HorizontalScrollbar = True
        Me.lbMoviesInTag.Location = New System.Drawing.Point(6, 46)
        Me.lbMoviesInTag.Name = "lbMoviesInTag"
        Me.lbMoviesInTag.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbMoviesInTag.Size = New System.Drawing.Size(222, 459)
        Me.lbMoviesInTag.TabIndex = 1
        '
        'btnglobalTagsSync
        '
        Me.btnglobalTagsSync.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnglobalTagsSync.Location = New System.Drawing.Point(93, 3)
        Me.btnglobalTagsSync.Name = "btnglobalTagsSync"
        Me.btnglobalTagsSync.Size = New System.Drawing.Size(164, 32)
        Me.btnglobalTagsSync.TabIndex = 17
        Me.btnglobalTagsSync.Text = "Save to database/NFO"
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 614)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(925, 38)
        Me.pnlBottom.TabIndex = 18
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 3
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.OK_Button, 0, 0)
        Me.tblBottom.Controls.Add(Me.btnglobalTagsSync, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(925, 38)
        Me.tblBottom.TabIndex = 0
        '
        'dlgTagManager
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(925, 652)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgTagManager"
        Me.Text = "Tag Manager"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.picTopTitle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlSaving.ResumeLayout(False)
        Me.pnlSaving.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        Me.gbMovies.ResumeLayout(False)
        Me.gbMoviesFilter.ResumeLayout(False)
        Me.gbMoviesFilter.PerformLayout()
        CType(Me.dgvMovies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTags.ResumeLayout(False)
        Me.gbTags.PerformLayout()
        Me.gbMoviesInTag.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblTopDetails As System.Windows.Forms.Label
    Friend WithEvents lblTopTitle As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents pnlSaving As System.Windows.Forms.Panel
    Friend WithEvents lblSaving As System.Windows.Forms.Label
    Friend WithEvents prbSaving As System.Windows.Forms.ProgressBar
    Friend WithEvents prbCompile As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gbTags As System.Windows.Forms.GroupBox
    Friend WithEvents txtEditTag As System.Windows.Forms.TextBox
    Friend WithEvents btnRemoveTag As System.Windows.Forms.Button
    Friend WithEvents btnEditTag As System.Windows.Forms.Button
    Friend WithEvents btnNewTag As System.Windows.Forms.Button
    Friend WithEvents lbTags As System.Windows.Forms.ListBox
    Friend WithEvents gbMovies As System.Windows.Forms.GroupBox
    Friend WithEvents dgvMovies As System.Windows.Forms.DataGridView
    Friend WithEvents btnAddMovie As System.Windows.Forms.Button
    Friend WithEvents gbMoviesInTag As System.Windows.Forms.GroupBox
    Friend WithEvents lblCurrentTag As System.Windows.Forms.Label
    Friend WithEvents btnRemoveMovie As System.Windows.Forms.Button
    Friend WithEvents lbMoviesInTag As System.Windows.Forms.ListBox
    Friend WithEvents picTopTitle As System.Windows.Forms.PictureBox
    Friend WithEvents btnglobalTagsSync As System.Windows.Forms.Button
    Friend WithEvents tblMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents tblBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbMoviesFilter As System.Windows.Forms.GroupBox
    Friend WithEvents rdMoviesFiltered As System.Windows.Forms.RadioButton
    Friend WithEvents rdMoviesAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdMoviesSelected As System.Windows.Forms.RadioButton
End Class
