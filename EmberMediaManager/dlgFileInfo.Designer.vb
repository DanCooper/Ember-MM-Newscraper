<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgFileInfo
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Video Streams", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Audio Streams", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Subtitles Stream", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"1", "H264", "Progressive"}, -1)
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"1", "AC3", "English"}, -1)
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("1")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgFileInfo))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lvStreams = New System.Windows.Forms.ListView()
        Me.colIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCodec = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colScanType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colWidth = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHeight = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAspect = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDuration = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colFileSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colLanguage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBitrate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMultiView_Count = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMultiView_Layout = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colStereoMode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnStreamRemove = New System.Windows.Forms.Button()
        Me.btnStreamEdit = New System.Windows.Forms.Button()
        Me.btnStreamNew = New System.Windows.Forms.Button()
        Me.lblStreamType = New System.Windows.Forms.Label()
        Me.cbStreamType = New System.Windows.Forms.ComboBox()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ssBottom = New System.Windows.Forms.StatusStrip()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(914, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.Visible = False
        '
        'lvStreams
        '
        Me.lvStreams.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colIndex, Me.colCodec, Me.colScanType, Me.colWidth, Me.colHeight, Me.colAspect, Me.colDuration, Me.colFileSize, Me.colLanguage, Me.colBitrate, Me.colMultiView_Count, Me.colMultiView_Layout, Me.colStereoMode})
        Me.lvStreams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvStreams.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvStreams.FullRowSelect = True
        ListViewGroup1.Header = "Video Streams"
        ListViewGroup1.Name = "VideoStreams"
        ListViewGroup2.Header = "Audio Streams"
        ListViewGroup2.Name = "AudioStreams"
        ListViewGroup3.Header = "Subtitles Stream"
        ListViewGroup3.Name = "SubtitlesStream"
        Me.lvStreams.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3})
        Me.lvStreams.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListViewItem1.Group = ListViewGroup1
        ListViewItem2.Group = ListViewGroup2
        ListViewItem3.Group = ListViewGroup3
        Me.lvStreams.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3})
        Me.lvStreams.Location = New System.Drawing.Point(0, 0)
        Me.lvStreams.MultiSelect = False
        Me.lvStreams.Name = "lvStreams"
        Me.lvStreams.Size = New System.Drawing.Size(992, 501)
        Me.lvStreams.TabIndex = 0
        Me.lvStreams.UseCompatibleStateImageBehavior = False
        Me.lvStreams.View = System.Windows.Forms.View.Details
        '
        'colIndex
        '
        Me.colIndex.Width = 20
        '
        'colCodec
        '
        Me.colCodec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colCodec.Width = 85
        '
        'colScanType
        '
        Me.colScanType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colScanType.Width = 85
        '
        'colWidth
        '
        Me.colWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'colHeight
        '
        Me.colHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'colAspect
        '
        Me.colAspect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'colDuration
        '
        Me.colDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'colFileSize
        '
        Me.colFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colFileSize.Width = 80
        '
        'colLanguage
        '
        Me.colLanguage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colLanguage.Width = 70
        '
        'colBitrate
        '
        Me.colBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colBitrate.Width = 50
        '
        'colMultiView_Count
        '
        Me.colMultiView_Count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colMultiView_Count.Width = 100
        '
        'colMultiView_Layout
        '
        Me.colMultiView_Layout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colMultiView_Layout.Width = 100
        '
        'colStereoMode
        '
        Me.colStereoMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colStereoMode.Width = 80
        '
        'btnStreamRemove
        '
        Me.btnStreamRemove.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnStreamRemove.Enabled = False
        Me.btnStreamRemove.Image = CType(resources.GetObject("btnStreamRemove.Image"), System.Drawing.Image)
        Me.btnStreamRemove.Location = New System.Drawing.Point(257, 3)
        Me.btnStreamRemove.Name = "btnStreamRemove"
        Me.btnStreamRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnStreamRemove.TabIndex = 3
        Me.btnStreamRemove.UseVisualStyleBackColor = True
        '
        'btnStreamEdit
        '
        Me.btnStreamEdit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnStreamEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnStreamEdit.Enabled = False
        Me.btnStreamEdit.Image = CType(resources.GetObject("btnStreamEdit.Image"), System.Drawing.Image)
        Me.btnStreamEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStreamEdit.Location = New System.Drawing.Point(208, 3)
        Me.btnStreamEdit.Name = "btnStreamEdit"
        Me.btnStreamEdit.Size = New System.Drawing.Size(23, 23)
        Me.btnStreamEdit.TabIndex = 2
        Me.btnStreamEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnStreamEdit.UseVisualStyleBackColor = True
        '
        'btnStreamNew
        '
        Me.btnStreamNew.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnStreamNew.Enabled = False
        Me.btnStreamNew.Image = CType(resources.GetObject("btnStreamNew.Image"), System.Drawing.Image)
        Me.btnStreamNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnStreamNew.Location = New System.Drawing.Point(179, 3)
        Me.btnStreamNew.Name = "btnStreamNew"
        Me.btnStreamNew.Size = New System.Drawing.Size(23, 23)
        Me.btnStreamNew.TabIndex = 1
        Me.btnStreamNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnStreamNew.UseVisualStyleBackColor = True
        '
        'lblStreamType
        '
        Me.lblStreamType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblStreamType.AutoSize = True
        Me.lblStreamType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStreamType.Location = New System.Drawing.Point(3, 8)
        Me.lblStreamType.Name = "lblStreamType"
        Me.lblStreamType.Size = New System.Drawing.Size(71, 13)
        Me.lblStreamType.TabIndex = 2
        Me.lblStreamType.Text = "Stream Type"
        Me.lblStreamType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbStreamType
        '
        Me.cbStreamType.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbStreamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStreamType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbStreamType.FormattingEnabled = True
        Me.cbStreamType.Items.AddRange(New Object() {"Video", "Audio", "Subtitle"})
        Me.cbStreamType.Location = New System.Drawing.Point(80, 4)
        Me.cbStreamType.Name = "cbStreamType"
        Me.cbStreamType.Size = New System.Drawing.Size(93, 21)
        Me.cbStreamType.TabIndex = 0
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 501)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(992, 29)
        Me.pnlBottom.TabIndex = 1
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 9
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnOK, 7, 0)
        Me.tblBottom.Controls.Add(Me.lblStreamType, 0, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 8, 0)
        Me.tblBottom.Controls.Add(Me.cbStreamType, 1, 0)
        Me.tblBottom.Controls.Add(Me.btnStreamNew, 2, 0)
        Me.tblBottom.Controls.Add(Me.btnStreamRemove, 5, 0)
        Me.tblBottom.Controls.Add(Me.btnStreamEdit, 3, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(992, 29)
        Me.tblBottom.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.AutoSize = True
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(833, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        Me.btnOK.Visible = False
        '
        'ssBottom
        '
        Me.ssBottom.Location = New System.Drawing.Point(0, 508)
        Me.ssBottom.Name = "ssBottom"
        Me.ssBottom.Size = New System.Drawing.Size(992, 22)
        Me.ssBottom.TabIndex = 9
        Me.ssBottom.Text = "StatusStrip1"
        Me.ssBottom.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.Controls.Add(Me.lvStreams)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(992, 501)
        Me.pnlMain.TabIndex = 0
        '
        'dlgFileInfo
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(992, 530)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.ssBottom)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgFileInfo"
        Me.ShowInTaskbar = False
        Me.Text = "Meta Data Editor"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lvStreams As System.Windows.Forms.ListView
    Friend WithEvents colIndex As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCodec As System.Windows.Forms.ColumnHeader
    Friend WithEvents colScanType As System.Windows.Forms.ColumnHeader
    Friend WithEvents colWidth As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHeight As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnStreamRemove As System.Windows.Forms.Button
    Friend WithEvents btnStreamEdit As System.Windows.Forms.Button
    Friend WithEvents btnStreamNew As System.Windows.Forms.Button
    Friend WithEvents lblStreamType As System.Windows.Forms.Label
    Friend WithEvents cbStreamType As System.Windows.Forms.ComboBox
    Friend WithEvents colAspect As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDuration As System.Windows.Forms.ColumnHeader
    Friend WithEvents colFileSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents colLanguage As System.Windows.Forms.ColumnHeader
    Friend WithEvents colBitrate As System.Windows.Forms.ColumnHeader
    Friend WithEvents colMultiView_Count As System.Windows.Forms.ColumnHeader
    Friend WithEvents colMultiView_Layout As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStereoMode As System.Windows.Forms.ColumnHeader
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents btnOK As Button
    Friend WithEvents ssBottom As StatusStrip
    Friend WithEvents pnlMain As Panel
End Class
