<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgImgSelect
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgImgSelect))
        Me.btnRemoveSubImage = New System.Windows.Forms.Button()
        Me.btnRestoreSubImage = New System.Windows.Forms.Button()
        Me.pnlImgSelect = New System.Windows.Forms.Panel()
        Me.pnlImageList = New System.Windows.Forms.Panel()
        Me.pnlImgSelectLeft = New System.Windows.Forms.Panel()
        Me.tblImgSelectLeft = New System.Windows.Forms.TableLayoutPanel()
        Me.cbSubImageType = New System.Windows.Forms.ComboBox()
        Me.pnlSubImages = New System.Windows.Forms.Panel()
        Me.pnlImgSelectBottom = New System.Windows.Forms.Panel()
        Me.tblImgSelectBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.tblFilter = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSelectButtons = New System.Windows.Forms.Panel()
        Me.tblSelectButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnSelectNone = New System.Windows.Forms.Button()
        Me.pnlImgSelectTop = New System.Windows.Forms.Panel()
        Me.tblImgSelectTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnRestoreTopImage = New System.Windows.Forms.Button()
        Me.btnRemoveTopImage = New System.Windows.Forms.Button()
        Me.pnlTopImages = New System.Windows.Forms.Panel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbStatus = New System.Windows.Forms.ToolStripProgressBar()
        Me.tmrReorderMainList = New System.Windows.Forms.Timer(Me.components)
        Me.pnlImgSelect.SuspendLayout()
        Me.pnlImgSelectLeft.SuspendLayout()
        Me.tblImgSelectLeft.SuspendLayout()
        Me.pnlImgSelectBottom.SuspendLayout()
        Me.tblImgSelectBottom.SuspendLayout()
        Me.pnlFilter.SuspendLayout()
        Me.pnlSelectButtons.SuspendLayout()
        Me.tblSelectButtons.SuspendLayout()
        Me.pnlImgSelectTop.SuspendLayout()
        Me.tblImgSelectTop.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRemoveSubImage
        '
        Me.btnRemoveSubImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveSubImage.Enabled = False
        Me.btnRemoveSubImage.Image = CType(resources.GetObject("btnRemoveSubImage.Image"), System.Drawing.Image)
        Me.btnRemoveSubImage.Location = New System.Drawing.Point(154, 382)
        Me.btnRemoveSubImage.Margin = New System.Windows.Forms.Padding(3, 3, 23, 3)
        Me.btnRemoveSubImage.Name = "btnRemoveSubImage"
        Me.btnRemoveSubImage.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveSubImage.TabIndex = 2
        Me.btnRemoveSubImage.UseVisualStyleBackColor = True
        '
        'btnRestoreSubImage
        '
        Me.btnRestoreSubImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRestoreSubImage.Enabled = False
        Me.btnRestoreSubImage.Image = CType(resources.GetObject("btnRestoreSubImage.Image"), System.Drawing.Image)
        Me.btnRestoreSubImage.Location = New System.Drawing.Point(6, 382)
        Me.btnRestoreSubImage.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.btnRestoreSubImage.Name = "btnRestoreSubImage"
        Me.btnRestoreSubImage.Size = New System.Drawing.Size(23, 23)
        Me.btnRestoreSubImage.TabIndex = 1
        Me.btnRestoreSubImage.UseVisualStyleBackColor = True
        '
        'pnlImgSelect
        '
        Me.pnlImgSelect.Controls.Add(Me.pnlImageList)
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectLeft)
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectBottom)
        Me.pnlImgSelect.Controls.Add(Me.pnlImgSelectTop)
        Me.pnlImgSelect.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImgSelect.Location = New System.Drawing.Point(0, 0)
        Me.pnlImgSelect.Name = "pnlImgSelect"
        Me.pnlImgSelect.Size = New System.Drawing.Size(1334, 711)
        Me.pnlImgSelect.TabIndex = 3
        '
        'pnlImageList
        '
        Me.pnlImageList.AutoScroll = True
        Me.pnlImageList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImageList.Location = New System.Drawing.Point(200, 203)
        Me.pnlImageList.Name = "pnlImageList"
        Me.pnlImageList.Size = New System.Drawing.Size(1134, 408)
        Me.pnlImageList.TabIndex = 3
        '
        'pnlImgSelectLeft
        '
        Me.pnlImgSelectLeft.AutoSize = True
        Me.pnlImgSelectLeft.Controls.Add(Me.tblImgSelectLeft)
        Me.pnlImgSelectLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlImgSelectLeft.Location = New System.Drawing.Point(0, 203)
        Me.pnlImgSelectLeft.Name = "pnlImgSelectLeft"
        Me.pnlImgSelectLeft.Size = New System.Drawing.Size(200, 408)
        Me.pnlImgSelectLeft.TabIndex = 2
        '
        'tblImgSelectLeft
        '
        Me.tblImgSelectLeft.AutoSize = True
        Me.tblImgSelectLeft.ColumnCount = 2
        Me.tblImgSelectLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectLeft.Controls.Add(Me.btnRestoreSubImage, 0, 2)
        Me.tblImgSelectLeft.Controls.Add(Me.btnRemoveSubImage, 1, 2)
        Me.tblImgSelectLeft.Controls.Add(Me.cbSubImageType, 0, 0)
        Me.tblImgSelectLeft.Controls.Add(Me.pnlSubImages, 0, 1)
        Me.tblImgSelectLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImgSelectLeft.Location = New System.Drawing.Point(0, 0)
        Me.tblImgSelectLeft.Name = "tblImgSelectLeft"
        Me.tblImgSelectLeft.RowCount = 3
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblImgSelectLeft.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectLeft.Size = New System.Drawing.Size(200, 408)
        Me.tblImgSelectLeft.TabIndex = 0
        '
        'cbSubImageType
        '
        Me.tblImgSelectLeft.SetColumnSpan(Me.cbSubImageType, 2)
        Me.cbSubImageType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbSubImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSubImageType.Enabled = False
        Me.cbSubImageType.FormattingEnabled = True
        Me.cbSubImageType.Location = New System.Drawing.Point(3, 3)
        Me.cbSubImageType.Name = "cbSubImageType"
        Me.cbSubImageType.Size = New System.Drawing.Size(194, 21)
        Me.cbSubImageType.TabIndex = 3
        '
        'pnlSubImages
        '
        Me.pnlSubImages.AutoScroll = True
        Me.tblImgSelectLeft.SetColumnSpan(Me.pnlSubImages, 2)
        Me.pnlSubImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSubImages.Location = New System.Drawing.Point(3, 30)
        Me.pnlSubImages.Name = "pnlSubImages"
        Me.pnlSubImages.Size = New System.Drawing.Size(194, 346)
        Me.pnlSubImages.TabIndex = 4
        '
        'pnlImgSelectBottom
        '
        Me.pnlImgSelectBottom.Controls.Add(Me.tblImgSelectBottom)
        Me.pnlImgSelectBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlImgSelectBottom.Location = New System.Drawing.Point(0, 611)
        Me.pnlImgSelectBottom.Name = "pnlImgSelectBottom"
        Me.pnlImgSelectBottom.Size = New System.Drawing.Size(1334, 100)
        Me.pnlImgSelectBottom.TabIndex = 1
        '
        'tblImgSelectBottom
        '
        Me.tblImgSelectBottom.ColumnCount = 5
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectBottom.Controls.Add(Me.btnOK, 3, 0)
        Me.tblImgSelectBottom.Controls.Add(Me.btnCancel, 4, 0)
        Me.tblImgSelectBottom.Controls.Add(Me.pnlFilter, 0, 0)
        Me.tblImgSelectBottom.Controls.Add(Me.pnlSelectButtons, 1, 0)
        Me.tblImgSelectBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImgSelectBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblImgSelectBottom.Name = "tblImgSelectBottom"
        Me.tblImgSelectBottom.RowCount = 1
        Me.tblImgSelectBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectBottom.Size = New System.Drawing.Size(1334, 100)
        Me.tblImgSelectBottom.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.Location = New System.Drawing.Point(1175, 41)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.Location = New System.Drawing.Point(1256, 41)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pnlFilter
        '
        Me.pnlFilter.Controls.Add(Me.tblFilter)
        Me.pnlFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilter.Location = New System.Drawing.Point(3, 3)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Size = New System.Drawing.Size(200, 100)
        Me.pnlFilter.TabIndex = 2
        '
        'tblFilter
        '
        Me.tblFilter.ColumnCount = 2
        Me.tblFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFilter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilter.Location = New System.Drawing.Point(0, 0)
        Me.tblFilter.Name = "tblFilter"
        Me.tblFilter.RowCount = 2
        Me.tblFilter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFilter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblFilter.Size = New System.Drawing.Size(200, 100)
        Me.tblFilter.TabIndex = 0
        '
        'pnlSelectButtons
        '
        Me.pnlSelectButtons.AutoSize = True
        Me.pnlSelectButtons.Controls.Add(Me.tblSelectButtons)
        Me.pnlSelectButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelectButtons.Location = New System.Drawing.Point(209, 3)
        Me.pnlSelectButtons.Name = "pnlSelectButtons"
        Me.pnlSelectButtons.Size = New System.Drawing.Size(82, 100)
        Me.pnlSelectButtons.TabIndex = 3
        '
        'tblSelectButtons
        '
        Me.tblSelectButtons.AutoSize = True
        Me.tblSelectButtons.ColumnCount = 1
        Me.tblSelectButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSelectButtons.Controls.Add(Me.btnSelectAll, 0, 1)
        Me.tblSelectButtons.Controls.Add(Me.btnSelectNone, 0, 2)
        Me.tblSelectButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSelectButtons.Location = New System.Drawing.Point(0, 0)
        Me.tblSelectButtons.Name = "tblSelectButtons"
        Me.tblSelectButtons.RowCount = 4
        Me.tblSelectButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSelectButtons.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSelectButtons.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSelectButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblSelectButtons.Size = New System.Drawing.Size(82, 100)
        Me.tblSelectButtons.TabIndex = 0
        '
        'btnSelectAll
        '
        Me.btnSelectAll.AutoSize = True
        Me.btnSelectAll.Enabled = False
        Me.btnSelectAll.Location = New System.Drawing.Point(3, 24)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectAll.TabIndex = 0
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnSelectNone
        '
        Me.btnSelectNone.AutoSize = True
        Me.btnSelectNone.Enabled = False
        Me.btnSelectNone.Location = New System.Drawing.Point(3, 53)
        Me.btnSelectNone.Name = "btnSelectNone"
        Me.btnSelectNone.Size = New System.Drawing.Size(76, 23)
        Me.btnSelectNone.TabIndex = 1
        Me.btnSelectNone.Text = "Select None"
        Me.btnSelectNone.UseVisualStyleBackColor = True
        '
        'pnlImgSelectTop
        '
        Me.pnlImgSelectTop.AutoSize = True
        Me.pnlImgSelectTop.Controls.Add(Me.tblImgSelectTop)
        Me.pnlImgSelectTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlImgSelectTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlImgSelectTop.Name = "pnlImgSelectTop"
        Me.pnlImgSelectTop.Size = New System.Drawing.Size(1334, 203)
        Me.pnlImgSelectTop.TabIndex = 0
        '
        'tblImgSelectTop
        '
        Me.tblImgSelectTop.AutoSize = True
        Me.tblImgSelectTop.ColumnCount = 2
        Me.tblImgSelectTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblImgSelectTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImgSelectTop.Controls.Add(Me.btnRestoreTopImage, 1, 0)
        Me.tblImgSelectTop.Controls.Add(Me.btnRemoveTopImage, 1, 1)
        Me.tblImgSelectTop.Controls.Add(Me.pnlTopImages, 0, 0)
        Me.tblImgSelectTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImgSelectTop.Location = New System.Drawing.Point(0, 0)
        Me.tblImgSelectTop.Name = "tblImgSelectTop"
        Me.tblImgSelectTop.RowCount = 2
        Me.tblImgSelectTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImgSelectTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblImgSelectTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImgSelectTop.Size = New System.Drawing.Size(1334, 203)
        Me.tblImgSelectTop.TabIndex = 0
        '
        'btnRestoreTopImage
        '
        Me.btnRestoreTopImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRestoreTopImage.Enabled = False
        Me.btnRestoreTopImage.Image = CType(resources.GetObject("btnRestoreTopImage.Image"), System.Drawing.Image)
        Me.btnRestoreTopImage.Location = New System.Drawing.Point(1308, 6)
        Me.btnRestoreTopImage.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.btnRestoreTopImage.Name = "btnRestoreTopImage"
        Me.btnRestoreTopImage.Size = New System.Drawing.Size(23, 23)
        Me.btnRestoreTopImage.TabIndex = 1
        Me.btnRestoreTopImage.UseVisualStyleBackColor = True
        '
        'btnRemoveTopImage
        '
        Me.btnRemoveTopImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveTopImage.Enabled = False
        Me.btnRemoveTopImage.Image = CType(resources.GetObject("btnRemoveTopImage.Image"), System.Drawing.Image)
        Me.btnRemoveTopImage.Location = New System.Drawing.Point(1308, 157)
        Me.btnRemoveTopImage.Margin = New System.Windows.Forms.Padding(3, 3, 3, 23)
        Me.btnRemoveTopImage.Name = "btnRemoveTopImage"
        Me.btnRemoveTopImage.Size = New System.Drawing.Size(23, 23)
        Me.btnRemoveTopImage.TabIndex = 2
        Me.btnRemoveTopImage.UseVisualStyleBackColor = True
        '
        'pnlTopImages
        '
        Me.pnlTopImages.AutoScroll = True
        Me.pnlTopImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopImages.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopImages.Name = "pnlTopImages"
        Me.tblImgSelectTop.SetRowSpan(Me.pnlTopImages, 2)
        Me.pnlTopImages.Size = New System.Drawing.Size(1299, 197)
        Me.pnlTopImages.TabIndex = 3
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.pbStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 689)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1334, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(1147, 17)
        Me.lblStatus.Spring = True
        Me.lblStatus.Text = "Downloading"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStatus.Visible = False
        '
        'pbStatus
        '
        Me.pbStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(100, 16)
        Me.pbStatus.Visible = False
        '
        'tmrReorderMainList
        '
        '
        'dlgImgSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1334, 711)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.pnlImgSelect)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "dlgImgSelect"
        Me.Text = "Image Select"
        Me.pnlImgSelect.ResumeLayout(False)
        Me.pnlImgSelect.PerformLayout()
        Me.pnlImgSelectLeft.ResumeLayout(False)
        Me.pnlImgSelectLeft.PerformLayout()
        Me.tblImgSelectLeft.ResumeLayout(False)
        Me.pnlImgSelectBottom.ResumeLayout(False)
        Me.tblImgSelectBottom.ResumeLayout(False)
        Me.tblImgSelectBottom.PerformLayout()
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlSelectButtons.ResumeLayout(False)
        Me.pnlSelectButtons.PerformLayout()
        Me.tblSelectButtons.ResumeLayout(False)
        Me.tblSelectButtons.PerformLayout()
        Me.pnlImgSelectTop.ResumeLayout(False)
        Me.pnlImgSelectTop.PerformLayout()
        Me.tblImgSelectTop.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRemoveSubImage As System.Windows.Forms.Button
    Friend WithEvents btnRestoreSubImage As System.Windows.Forms.Button
    Friend WithEvents pnlImgSelect As System.Windows.Forms.Panel
    Friend WithEvents pnlImageList As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectLeft As System.Windows.Forms.Panel
    Friend WithEvents tblImgSelectLeft As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cbSubImageType As System.Windows.Forms.ComboBox
    Friend WithEvents pnlSubImages As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlImgSelectTop As System.Windows.Forms.Panel
    Friend WithEvents tblImgSelectTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnRestoreTopImage As System.Windows.Forms.Button
    Friend WithEvents btnRemoveTopImage As System.Windows.Forms.Button
    Friend WithEvents pnlTopImages As System.Windows.Forms.Panel
    Friend WithEvents tblImgSelectBottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents tblFilter As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSelectButtons As System.Windows.Forms.Panel
    Friend WithEvents tblSelectButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectNone As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tmrReorderMainList As System.Windows.Forms.Timer
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pbStatus As System.Windows.Forms.ToolStripProgressBar
End Class
