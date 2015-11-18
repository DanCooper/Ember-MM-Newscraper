<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgExportMovies
    Inherits System.Windows.Forms.Form

    #Region "Fields"

    Friend  WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cbTemplate As System.Windows.Forms.ComboBox
    Friend  WithEvents btnClose As System.Windows.Forms.Button
    Friend  WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lblTemplate As System.Windows.Forms.Label
    Friend WithEvents lblCanceling As System.Windows.Forms.Label
    Friend WithEvents lblCompiling As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents pbCompile As System.Windows.Forms.ProgressBar
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlCancel As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents wbPreview As System.Windows.Forms.WebBrowser

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgExportMovies))
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBuild = New System.Windows.Forms.Button()
        Me.lblTemplate = New System.Windows.Forms.Label()
        Me.cbTemplate = New System.Windows.Forms.ComboBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlCancel = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pbCompile = New System.Windows.Forms.ProgressBar()
        Me.lblCompiling = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.lblCanceling = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.wbPreview = New System.Windows.Forms.WebBrowser()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblList_Movies = New System.Windows.Forms.Label()
        Me.lblList_TVShows = New System.Windows.Forms.Label()
        Me.cbList_Movies = New System.Windows.Forms.ComboBox()
        Me.cbList_TVShows = New System.Windows.Forms.ComboBox()
        Me.lblExportPath = New System.Windows.Forms.Label()
        Me.txtExportPath = New System.Windows.Forms.TextBox()
        Me.btnExportPath = New System.Windows.Forms.Button()
        Me.pnlCancel.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnSave.Enabled = False
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSave.Location = New System.Drawing.Point(837, 30)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(119, 23)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnClose.Location = New System.Drawing.Point(962, 30)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(119, 23)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnBuild
        '
        Me.btnBuild.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnBuild.Enabled = False
        Me.btnBuild.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnBuild.Location = New System.Drawing.Point(588, 30)
        Me.btnBuild.Name = "btnBuild"
        Me.btnBuild.Size = New System.Drawing.Size(169, 23)
        Me.btnBuild.TabIndex = 5
        Me.btnBuild.Text = "Generate HTML..."
        '
        'lblTemplate
        '
        Me.lblTemplate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTemplate.AutoSize = True
        Me.lblTemplate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblTemplate.Location = New System.Drawing.Point(3, 21)
        Me.lblTemplate.Name = "lblTemplate"
        Me.tblBottom.SetRowSpan(Me.lblTemplate, 2)
        Me.lblTemplate.Size = New System.Drawing.Size(54, 13)
        Me.lblTemplate.TabIndex = 1
        Me.lblTemplate.Text = "Template"
        '
        'cbTemplate
        '
        Me.cbTemplate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTemplate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTemplate.FormattingEnabled = True
        Me.cbTemplate.Location = New System.Drawing.Point(77, 17)
        Me.cbTemplate.Name = "cbTemplate"
        Me.tblBottom.SetRowSpan(Me.cbTemplate, 2)
        Me.cbTemplate.Size = New System.Drawing.Size(200, 21)
        Me.cbTemplate.TabIndex = 2
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "asc.png")
        Me.ImageList1.Images.SetKeyName(1, "desc.png")
        '
        'pnlCancel
        '
        Me.pnlCancel.BackColor = System.Drawing.Color.LightGray
        Me.pnlCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCancel.Controls.Add(Me.btnCancel)
        Me.pnlCancel.Controls.Add(Me.pbCompile)
        Me.pnlCancel.Controls.Add(Me.lblCompiling)
        Me.pnlCancel.Controls.Add(Me.lblFile)
        Me.pnlCancel.Controls.Add(Me.lblCanceling)
        Me.pnlCancel.Location = New System.Drawing.Point(242, 12)
        Me.pnlCancel.Name = "pnlCancel"
        Me.pnlCancel.Size = New System.Drawing.Size(403, 76)
        Me.pnlCancel.TabIndex = 1
        Me.pnlCancel.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'pbCompile
        '
        Me.pbCompile.Location = New System.Drawing.Point(8, 36)
        Me.pbCompile.Name = "pbCompile"
        Me.pbCompile.Size = New System.Drawing.Size(388, 18)
        Me.pbCompile.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbCompile.TabIndex = 3
        '
        'lblCompiling
        '
        Me.lblCompiling.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblCompiling.Location = New System.Drawing.Point(3, 11)
        Me.lblCompiling.Name = "lblCompiling"
        Me.lblCompiling.Size = New System.Drawing.Size(395, 20)
        Me.lblCompiling.TabIndex = 0
        Me.lblCompiling.Text = "Compiling Movie List..."
        Me.lblCompiling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.lblCanceling.Text = "Canceling Compilation..."
        Me.lblCanceling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCanceling.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.Controls.Add(Me.pnlCancel)
        Me.pnlMain.Controls.Add(Me.wbPreview)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1084, 497)
        Me.pnlMain.TabIndex = 4
        '
        'wbPreview
        '
        Me.wbPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wbPreview.Location = New System.Drawing.Point(0, 0)
        Me.wbPreview.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wbPreview.Name = "wbPreview"
        Me.wbPreview.Size = New System.Drawing.Size(1084, 497)
        Me.wbPreview.TabIndex = 0
        Me.wbPreview.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 581)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1084, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 497)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1084, 84)
        Me.pnlBottom.TabIndex = 6
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 10
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnClose, 9, 1)
        Me.tblBottom.Controls.Add(Me.lblTemplate, 0, 0)
        Me.tblBottom.Controls.Add(Me.btnSave, 8, 1)
        Me.tblBottom.Controls.Add(Me.btnBuild, 6, 1)
        Me.tblBottom.Controls.Add(Me.lblList_Movies, 3, 0)
        Me.tblBottom.Controls.Add(Me.lblList_TVShows, 3, 1)
        Me.tblBottom.Controls.Add(Me.cbList_Movies, 4, 0)
        Me.tblBottom.Controls.Add(Me.cbList_TVShows, 4, 1)
        Me.tblBottom.Controls.Add(Me.lblExportPath, 0, 2)
        Me.tblBottom.Controls.Add(Me.txtExportPath, 1, 2)
        Me.tblBottom.Controls.Add(Me.btnExportPath, 5, 2)
        Me.tblBottom.Controls.Add(Me.cbTemplate, 1, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 3
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(1084, 84)
        Me.tblBottom.TabIndex = 0
        '
        'lblList_Movies
        '
        Me.lblList_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblList_Movies.AutoSize = True
        Me.lblList_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblList_Movies.Location = New System.Drawing.Point(303, 7)
        Me.lblList_Movies.Name = "lblList_Movies"
        Me.lblList_Movies.Size = New System.Drawing.Size(61, 13)
        Me.lblList_Movies.TabIndex = 1
        Me.lblList_Movies.Text = "Movie List"
        '
        'lblList_TVShows
        '
        Me.lblList_TVShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblList_TVShows.AutoSize = True
        Me.lblList_TVShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblList_TVShows.Location = New System.Drawing.Point(303, 35)
        Me.lblList_TVShows.Name = "lblList_TVShows"
        Me.lblList_TVShows.Size = New System.Drawing.Size(73, 13)
        Me.lblList_TVShows.TabIndex = 1
        Me.lblList_TVShows.Text = "TV Show List"
        '
        'cbList_Movies
        '
        Me.cbList_Movies.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblBottom.SetColumnSpan(Me.cbList_Movies, 2)
        Me.cbList_Movies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbList_Movies.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbList_Movies.FormattingEnabled = True
        Me.cbList_Movies.Location = New System.Drawing.Point(382, 3)
        Me.cbList_Movies.Name = "cbList_Movies"
        Me.cbList_Movies.Size = New System.Drawing.Size(200, 21)
        Me.cbList_Movies.TabIndex = 2
        '
        'cbList_TVShows
        '
        Me.cbList_TVShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblBottom.SetColumnSpan(Me.cbList_TVShows, 2)
        Me.cbList_TVShows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbList_TVShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbList_TVShows.FormattingEnabled = True
        Me.cbList_TVShows.Location = New System.Drawing.Point(382, 31)
        Me.cbList_TVShows.Name = "cbList_TVShows"
        Me.cbList_TVShows.Size = New System.Drawing.Size(200, 21)
        Me.cbList_TVShows.TabIndex = 2
        '
        'lblExportPath
        '
        Me.lblExportPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblExportPath.AutoSize = True
        Me.lblExportPath.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblExportPath.Location = New System.Drawing.Point(3, 63)
        Me.lblExportPath.Name = "lblExportPath"
        Me.lblExportPath.Size = New System.Drawing.Size(68, 13)
        Me.lblExportPath.TabIndex = 1
        Me.lblExportPath.Text = "Export Path"
        '
        'txtExportPath
        '
        Me.tblBottom.SetColumnSpan(Me.txtExportPath, 4)
        Me.txtExportPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtExportPath.Location = New System.Drawing.Point(77, 59)
        Me.txtExportPath.Name = "txtExportPath"
        Me.txtExportPath.Size = New System.Drawing.Size(481, 22)
        Me.txtExportPath.TabIndex = 7
        '
        'btnExportPath
        '
        Me.btnExportPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnExportPath.Location = New System.Drawing.Point(561, 59)
        Me.btnExportPath.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExportPath.Name = "btnExportPath"
        Me.btnExportPath.Size = New System.Drawing.Size(24, 22)
        Me.btnExportPath.TabIndex = 15
        Me.btnExportPath.Text = "..."
        Me.btnExportPath.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnExportPath.UseVisualStyleBackColor = True
        '
        'dlgExportMovies
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(1084, 603)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "dlgExportMovies"
        Me.Text = "Export Movies"
        Me.pnlCancel.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuild As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents lblList_Movies As Label
    Friend WithEvents lblList_TVShows As Label
    Friend WithEvents cbList_Movies As ComboBox
    Friend WithEvents cbList_TVShows As ComboBox
    Friend WithEvents txtExportPath As TextBox
    Friend WithEvents lblExportPath As Label
    Friend WithEvents btnExportPath As Button

#End Region 'Methods

End Class