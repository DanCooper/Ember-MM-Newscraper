<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated> _
Partial Class dlgExportMovies
    Inherits System.Windows.Forms.Form

#Region "Fields"
    Friend WithEvents cbTemplate As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblTemplate As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents wbPreview As System.Windows.Forms.WebBrowser

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

#End Region 'Fields

#Region "Methods"

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode>
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
    <System.Diagnostics.DebuggerStepThrough>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgExportMovies))
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnBuild = New System.Windows.Forms.Button()
        Me.lblTemplate = New System.Windows.Forms.Label()
        Me.cbTemplate = New System.Windows.Forms.ComboBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.wbPreview = New System.Windows.Forms.WebBrowser()
        Me.ssStatus = New System.Windows.Forms.StatusStrip()
        Me.tslblFile = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslblSpring = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tspbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.lblList_Movies = New System.Windows.Forms.Label()
        Me.lblList_TVShows = New System.Windows.Forms.Label()
        Me.cbList_Movies = New System.Windows.Forms.ComboBox()
        Me.cbList_TVShows = New System.Windows.Forms.ComboBox()
        Me.lblExportPath = New System.Windows.Forms.Label()
        Me.txtExportPath = New System.Windows.Forms.TextBox()
        Me.btnExportPath = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.ssStatus.SuspendLayout()
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
        'pnlMain
        '
        Me.pnlMain.AutoScroll = True
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
        'ssStatus
        '
        Me.ssStatus.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.ssStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslblFile, Me.tslblSpring, Me.tslblStatus, Me.tspbStatus})
        Me.ssStatus.Location = New System.Drawing.Point(0, 581)
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(1084, 22)
        Me.ssStatus.TabIndex = 5
        '
        'tslblFile
        '
        Me.tslblFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tslblFile.Name = "tslblFile"
        Me.tslblFile.Size = New System.Drawing.Size(25, 17)
        Me.tslblFile.Text = "File"
        Me.tslblFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.tslblFile.Visible = False
        '
        'tslblSpring
        '
        Me.tslblSpring.Name = "tslblSpring"
        Me.tslblSpring.Size = New System.Drawing.Size(1069, 17)
        Me.tslblSpring.Spring = True
        '
        'tslblStatus
        '
        Me.tslblStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.tslblStatus.Name = "tslblStatus"
        Me.tslblStatus.Size = New System.Drawing.Size(50, 17)
        Me.tslblStatus.Text = "Loading"
        Me.tslblStatus.Visible = False
        '
        'tspbStatus
        '
        Me.tspbStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tspbStatus.AutoSize = False
        Me.tspbStatus.Name = "tspbStatus"
        Me.tspbStatus.Size = New System.Drawing.Size(150, 16)
        Me.tspbStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.tspbStatus.Visible = False
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
        Me.ClientSize = New System.Drawing.Size(1084, 603)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.ssStatus)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "dlgExportMovies"
        Me.Text = "Export Movies"
        Me.pnlMain.ResumeLayout(False)
        Me.ssStatus.ResumeLayout(False)
        Me.ssStatus.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuild As System.Windows.Forms.Button
    Friend WithEvents ssStatus As StatusStrip
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents lblList_Movies As Label
    Friend WithEvents lblList_TVShows As Label
    Friend WithEvents cbList_Movies As ComboBox
    Friend WithEvents cbList_TVShows As ComboBox
    Friend WithEvents txtExportPath As TextBox
    Friend WithEvents lblExportPath As Label
    Friend WithEvents btnExportPath As Button
    Friend WithEvents tspbStatus As ToolStripProgressBar
    Friend WithEvents tslblFile As ToolStripStatusLabel
    Friend WithEvents tslblSpring As ToolStripStatusLabel
    Friend WithEvents tslblStatus As ToolStripStatusLabel

#End Region 'Methods

End Class