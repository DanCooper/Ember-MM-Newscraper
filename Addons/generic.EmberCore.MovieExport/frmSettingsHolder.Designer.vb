<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsHolder
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.gpb_ExportFilterSettings = New System.Windows.Forms.GroupBox()
        Me.lbl_exportmoviefilter3 = New System.Windows.Forms.Label()
        Me.lbl_exportmoviefilter3saved = New System.Windows.Forms.Label()
        Me.lbl_exportmoviefilter2 = New System.Windows.Forms.Label()
        Me.lbl_exportmoviefilter2saved = New System.Windows.Forms.Label()
        Me.cbo_exportmoviefilter = New System.Windows.Forms.ComboBox()
        Me.lbl_exportmoviefilter1 = New System.Windows.Forms.Label()
        Me.lbl_exportmoviefilter1saved = New System.Windows.Forms.Label()
        Me.lstSources = New System.Windows.Forms.CheckedListBox()
        Me.btnSource = New System.Windows.Forms.Button()
        Me.btn_Apply = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btn_Reset = New System.Windows.Forms.Button()
        Me.cbSearch = New System.Windows.Forms.ComboBox()
        Me.lbl_exportmoviefiltergenerate = New System.Windows.Forms.Label()
        Me.lblIn = New System.Windows.Forms.Label()
        Me.gpb_ExportGeneralSettings = New System.Windows.Forms.GroupBox()
        Me.chkExportTVShows = New System.Windows.Forms.CheckBox()
        Me.lbl_exportmoviepath = New System.Windows.Forms.Label()
        Me.txt_exportmoviepath = New System.Windows.Forms.TextBox()
        Me.btn_exportmoviepath = New System.Windows.Forms.Button()
        Me.gpb_ExportImage = New System.Windows.Forms.GroupBox()
        Me.cbo_exportmoviequality = New System.Windows.Forms.ComboBox()
        Me.lbl_exportmoviequality = New System.Windows.Forms.Label()
        Me.cbo_exportmoviefanart = New System.Windows.Forms.ComboBox()
        Me.lbl_exportmoviefanart = New System.Windows.Forms.Label()
        Me.cbo_exportmovieposter = New System.Windows.Forms.ComboBox()
        Me.lbl_exportmovieposter = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.gpb_ExportFilterSettings.SuspendLayout()
        Me.gpb_ExportGeneralSettings.SuspendLayout()
        Me.gpb_ExportImage.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.cbEnabled)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(610, 25)
        Me.Panel1.TabIndex = 0
        '
        'cbEnabled
        '
        Me.cbEnabled.AutoSize = True
        Me.cbEnabled.Location = New System.Drawing.Point(10, 5)
        Me.cbEnabled.Name = "cbEnabled"
        Me.cbEnabled.Size = New System.Drawing.Size(68, 17)
        Me.cbEnabled.TabIndex = 0
        Me.cbEnabled.Text = "Enabled"
        Me.cbEnabled.UseVisualStyleBackColor = True
        '
        'pnlSettings
        '
        Me.pnlSettings.Controls.Add(Me.gpb_ExportFilterSettings)
        Me.pnlSettings.Controls.Add(Me.gpb_ExportGeneralSettings)
        Me.pnlSettings.Controls.Add(Me.gpb_ExportImage)
        Me.pnlSettings.Controls.Add(Me.Panel1)
        Me.pnlSettings.Location = New System.Drawing.Point(3, 12)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(610, 387)
        Me.pnlSettings.TabIndex = 0
        '
        'gpb_ExportFilterSettings
        '
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lbl_exportmoviefilter3)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lbl_exportmoviefilter3saved)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lbl_exportmoviefilter2)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lbl_exportmoviefilter2saved)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.cbo_exportmoviefilter)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lbl_exportmoviefilter1)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lbl_exportmoviefilter1saved)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lstSources)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.btnSource)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.btn_Apply)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.txtSearch)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.btn_Reset)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.cbSearch)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lbl_exportmoviefiltergenerate)
        Me.gpb_ExportFilterSettings.Controls.Add(Me.lblIn)
        Me.gpb_ExportFilterSettings.Location = New System.Drawing.Point(9, 152)
        Me.gpb_ExportFilterSettings.Name = "gpb_ExportFilterSettings"
        Me.gpb_ExportFilterSettings.Size = New System.Drawing.Size(596, 232)
        Me.gpb_ExportFilterSettings.TabIndex = 22
        Me.gpb_ExportFilterSettings.TabStop = False
        Me.gpb_ExportFilterSettings.Text = "Filter Settings"
        '
        'lbl_exportmoviefilter3
        '
        Me.lbl_exportmoviefilter3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_exportmoviefilter3.Location = New System.Drawing.Point(6, 206)
        Me.lbl_exportmoviefilter3.Name = "lbl_exportmoviefilter3"
        Me.lbl_exportmoviefilter3.Size = New System.Drawing.Size(87, 20)
        Me.lbl_exportmoviefilter3.TabIndex = 14
        Me.lbl_exportmoviefilter3.Text = "Filter 3"
        Me.lbl_exportmoviefilter3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_exportmoviefilter3saved
        '
        Me.lbl_exportmoviefilter3saved.Enabled = False
        Me.lbl_exportmoviefilter3saved.Location = New System.Drawing.Point(99, 206)
        Me.lbl_exportmoviefilter3saved.Name = "lbl_exportmoviefilter3saved"
        Me.lbl_exportmoviefilter3saved.Size = New System.Drawing.Size(475, 20)
        Me.lbl_exportmoviefilter3saved.TabIndex = 13
        Me.lbl_exportmoviefilter3saved.Text = "-"
        Me.lbl_exportmoviefilter3saved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_exportmoviefilter2
        '
        Me.lbl_exportmoviefilter2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_exportmoviefilter2.Location = New System.Drawing.Point(6, 189)
        Me.lbl_exportmoviefilter2.Name = "lbl_exportmoviefilter2"
        Me.lbl_exportmoviefilter2.Size = New System.Drawing.Size(87, 20)
        Me.lbl_exportmoviefilter2.TabIndex = 12
        Me.lbl_exportmoviefilter2.Text = "Filter 2"
        Me.lbl_exportmoviefilter2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_exportmoviefilter2saved
        '
        Me.lbl_exportmoviefilter2saved.Enabled = False
        Me.lbl_exportmoviefilter2saved.Location = New System.Drawing.Point(99, 189)
        Me.lbl_exportmoviefilter2saved.Name = "lbl_exportmoviefilter2saved"
        Me.lbl_exportmoviefilter2saved.Size = New System.Drawing.Size(475, 20)
        Me.lbl_exportmoviefilter2saved.TabIndex = 11
        Me.lbl_exportmoviefilter2saved.Text = "-"
        Me.lbl_exportmoviefilter2saved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbo_exportmoviefilter
        '
        Me.cbo_exportmoviefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_exportmoviefilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_exportmoviefilter.FormattingEnabled = True
        Me.cbo_exportmoviefilter.Items.AddRange(New Object() {"Filter 1", "Filter 2", "Filter 3"})
        Me.cbo_exportmoviefilter.Location = New System.Drawing.Point(102, 21)
        Me.cbo_exportmoviefilter.Name = "cbo_exportmoviefilter"
        Me.cbo_exportmoviefilter.Size = New System.Drawing.Size(87, 21)
        Me.cbo_exportmoviefilter.TabIndex = 10
        '
        'lbl_exportmoviefilter1
        '
        Me.lbl_exportmoviefilter1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_exportmoviefilter1.Location = New System.Drawing.Point(6, 172)
        Me.lbl_exportmoviefilter1.Name = "lbl_exportmoviefilter1"
        Me.lbl_exportmoviefilter1.Size = New System.Drawing.Size(87, 20)
        Me.lbl_exportmoviefilter1.TabIndex = 9
        Me.lbl_exportmoviefilter1.Text = "Filter 1"
        Me.lbl_exportmoviefilter1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_exportmoviefilter1saved
        '
        Me.lbl_exportmoviefilter1saved.Enabled = False
        Me.lbl_exportmoviefilter1saved.Location = New System.Drawing.Point(99, 172)
        Me.lbl_exportmoviefilter1saved.Name = "lbl_exportmoviefilter1saved"
        Me.lbl_exportmoviefilter1saved.Size = New System.Drawing.Size(475, 20)
        Me.lbl_exportmoviefilter1saved.TabIndex = 8
        Me.lbl_exportmoviefilter1saved.Text = "-"
        Me.lbl_exportmoviefilter1saved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstSources
        '
        Me.lstSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstSources.FormattingEnabled = True
        Me.lstSources.Location = New System.Drawing.Point(102, 73)
        Me.lstSources.Name = "lstSources"
        Me.lstSources.Size = New System.Drawing.Size(166, 89)
        Me.lstSources.TabIndex = 7
        Me.lstSources.Visible = False
        '
        'btnSource
        '
        Me.btnSource.BackColor = System.Drawing.Color.MediumSpringGreen
        Me.btnSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSource.ImageIndex = 0
        Me.btnSource.Location = New System.Drawing.Point(274, 46)
        Me.btnSource.Name = "btnSource"
        Me.btnSource.Size = New System.Drawing.Size(38, 20)
        Me.btnSource.TabIndex = 2
        Me.btnSource.Text = "SET"
        Me.btnSource.UseVisualStyleBackColor = False
        Me.btnSource.Visible = False
        '
        'btn_Apply
        '
        Me.btn_Apply.BackColor = System.Drawing.Color.White
        Me.btn_Apply.Enabled = False
        Me.btn_Apply.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_Apply.Location = New System.Drawing.Point(476, 47)
        Me.btn_Apply.Name = "btn_Apply"
        Me.btn_Apply.Size = New System.Drawing.Size(101, 51)
        Me.btn_Apply.TabIndex = 5
        Me.btn_Apply.Text = "Save Filter"
        Me.btn_Apply.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(102, 45)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(166, 22)
        Me.txtSearch.TabIndex = 3
        '
        'btn_Reset
        '
        Me.btn_Reset.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_Reset.Location = New System.Drawing.Point(476, 111)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(101, 51)
        Me.btn_Reset.TabIndex = 6
        Me.btn_Reset.Text = "Reset Filter"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'cbSearch
        '
        Me.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSearch.FormattingEnabled = True
        Me.cbSearch.Location = New System.Drawing.Point(344, 47)
        Me.cbSearch.Name = "cbSearch"
        Me.cbSearch.Size = New System.Drawing.Size(117, 21)
        Me.cbSearch.TabIndex = 4
        '
        'lbl_exportmoviefiltergenerate
        '
        Me.lbl_exportmoviefiltergenerate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_exportmoviefiltergenerate.Location = New System.Drawing.Point(6, 22)
        Me.lbl_exportmoviefiltergenerate.Name = "lbl_exportmoviefiltergenerate"
        Me.lbl_exportmoviefiltergenerate.Size = New System.Drawing.Size(90, 21)
        Me.lbl_exportmoviefiltergenerate.TabIndex = 0
        Me.lbl_exportmoviefiltergenerate.Text = "Generate Filter"
        Me.lbl_exportmoviefiltergenerate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIn
        '
        Me.lblIn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblIn.Location = New System.Drawing.Point(318, 45)
        Me.lblIn.Name = "lblIn"
        Me.lblIn.Size = New System.Drawing.Size(20, 22)
        Me.lblIn.TabIndex = 3
        Me.lblIn.Text = "in"
        Me.lblIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gpb_ExportGeneralSettings
        '
        Me.gpb_ExportGeneralSettings.Controls.Add(Me.chkExportTVShows)
        Me.gpb_ExportGeneralSettings.Controls.Add(Me.lbl_exportmoviepath)
        Me.gpb_ExportGeneralSettings.Controls.Add(Me.txt_exportmoviepath)
        Me.gpb_ExportGeneralSettings.Controls.Add(Me.btn_exportmoviepath)
        Me.gpb_ExportGeneralSettings.Location = New System.Drawing.Point(9, 42)
        Me.gpb_ExportGeneralSettings.Name = "gpb_ExportGeneralSettings"
        Me.gpb_ExportGeneralSettings.Size = New System.Drawing.Size(338, 81)
        Me.gpb_ExportGeneralSettings.TabIndex = 20
        Me.gpb_ExportGeneralSettings.TabStop = False
        Me.gpb_ExportGeneralSettings.Text = "General Settings"
        '
        'chkExportTVShows
        '
        Me.chkExportTVShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkExportTVShows.Location = New System.Drawing.Point(9, 53)
        Me.chkExportTVShows.Name = "chkExportTVShows"
        Me.chkExportTVShows.Size = New System.Drawing.Size(290, 17)
        Me.chkExportTVShows.TabIndex = 15
        Me.chkExportTVShows.Text = "Export TV Shows"
        Me.chkExportTVShows.UseVisualStyleBackColor = True
        '
        'lbl_exportmoviepath
        '
        Me.lbl_exportmoviepath.Location = New System.Drawing.Point(6, 22)
        Me.lbl_exportmoviepath.Name = "lbl_exportmoviepath"
        Me.lbl_exportmoviepath.Size = New System.Drawing.Size(65, 22)
        Me.lbl_exportmoviepath.TabIndex = 12
        Me.lbl_exportmoviepath.Text = "ExportPath"
        Me.lbl_exportmoviepath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_exportmoviepath
        '
        Me.txt_exportmoviepath.Location = New System.Drawing.Point(77, 22)
        Me.txt_exportmoviepath.Name = "txt_exportmoviepath"
        Me.txt_exportmoviepath.ReadOnly = True
        Me.txt_exportmoviepath.Size = New System.Drawing.Size(222, 22)
        Me.txt_exportmoviepath.TabIndex = 13
        '
        'btn_exportmoviepath
        '
        Me.btn_exportmoviepath.Location = New System.Drawing.Point(302, 22)
        Me.btn_exportmoviepath.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_exportmoviepath.Name = "btn_exportmoviepath"
        Me.btn_exportmoviepath.Size = New System.Drawing.Size(24, 22)
        Me.btn_exportmoviepath.TabIndex = 14
        Me.btn_exportmoviepath.Text = "..."
        Me.btn_exportmoviepath.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btn_exportmoviepath.UseVisualStyleBackColor = True
        '
        'gpb_ExportImage
        '
        Me.gpb_ExportImage.Controls.Add(Me.cbo_exportmoviequality)
        Me.gpb_ExportImage.Controls.Add(Me.lbl_exportmoviequality)
        Me.gpb_ExportImage.Controls.Add(Me.cbo_exportmoviefanart)
        Me.gpb_ExportImage.Controls.Add(Me.lbl_exportmoviefanart)
        Me.gpb_ExportImage.Controls.Add(Me.cbo_exportmovieposter)
        Me.gpb_ExportImage.Controls.Add(Me.lbl_exportmovieposter)
        Me.gpb_ExportImage.Location = New System.Drawing.Point(353, 42)
        Me.gpb_ExportImage.Name = "gpb_ExportImage"
        Me.gpb_ExportImage.Size = New System.Drawing.Size(251, 104)
        Me.gpb_ExportImage.TabIndex = 19
        Me.gpb_ExportImage.TabStop = False
        Me.gpb_ExportImage.Text = "Image Settings"
        '
        'cbo_exportmoviequality
        '
        Me.cbo_exportmoviequality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_exportmoviequality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_exportmoviequality.FormattingEnabled = True
        Me.cbo_exportmoviequality.Items.AddRange(New Object() {"60", "70", "80", "90", "100"})
        Me.cbo_exportmoviequality.Location = New System.Drawing.Point(132, 76)
        Me.cbo_exportmoviequality.Name = "cbo_exportmoviequality"
        Me.cbo_exportmoviequality.Size = New System.Drawing.Size(101, 21)
        Me.cbo_exportmoviequality.TabIndex = 20
        '
        'lbl_exportmoviequality
        '
        Me.lbl_exportmoviequality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_exportmoviequality.Location = New System.Drawing.Point(6, 76)
        Me.lbl_exportmoviequality.Name = "lbl_exportmoviequality"
        Me.lbl_exportmoviequality.Size = New System.Drawing.Size(144, 21)
        Me.lbl_exportmoviequality.TabIndex = 19
        Me.lbl_exportmoviequality.Text = "Quality"
        Me.lbl_exportmoviequality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbo_exportmoviefanart
        '
        Me.cbo_exportmoviefanart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_exportmoviefanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_exportmoviefanart.FormattingEnabled = True
        Me.cbo_exportmoviefanart.Items.AddRange(New Object() {"400", "600", "800", "1200", "original"})
        Me.cbo_exportmoviefanart.Location = New System.Drawing.Point(132, 22)
        Me.cbo_exportmoviefanart.Name = "cbo_exportmoviefanart"
        Me.cbo_exportmoviefanart.Size = New System.Drawing.Size(101, 21)
        Me.cbo_exportmoviefanart.TabIndex = 18
        '
        'lbl_exportmoviefanart
        '
        Me.lbl_exportmoviefanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_exportmoviefanart.Location = New System.Drawing.Point(6, 22)
        Me.lbl_exportmoviefanart.Name = "lbl_exportmoviefanart"
        Me.lbl_exportmoviefanart.Size = New System.Drawing.Size(144, 21)
        Me.lbl_exportmoviefanart.TabIndex = 16
        Me.lbl_exportmoviefanart.Text = "Fanart Width [px]"
        Me.lbl_exportmoviefanart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbo_exportmovieposter
        '
        Me.cbo_exportmovieposter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_exportmovieposter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_exportmovieposter.FormattingEnabled = True
        Me.cbo_exportmovieposter.Items.AddRange(New Object() {"300", "400", "600", "800", "original"})
        Me.cbo_exportmovieposter.Location = New System.Drawing.Point(132, 49)
        Me.cbo_exportmovieposter.Name = "cbo_exportmovieposter"
        Me.cbo_exportmovieposter.Size = New System.Drawing.Size(101, 21)
        Me.cbo_exportmovieposter.TabIndex = 17
        '
        'lbl_exportmovieposter
        '
        Me.lbl_exportmovieposter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lbl_exportmovieposter.Location = New System.Drawing.Point(6, 49)
        Me.lbl_exportmovieposter.Name = "lbl_exportmovieposter"
        Me.lbl_exportmovieposter.Size = New System.Drawing.Size(144, 21)
        Me.lbl_exportmovieposter.TabIndex = 15
        Me.lbl_exportmovieposter.Text = "Poster Height [px]"
        Me.lbl_exportmovieposter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(625, 411)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmSettingsHolder"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.gpb_ExportFilterSettings.ResumeLayout(False)
        Me.gpb_ExportFilterSettings.PerformLayout()
        Me.gpb_ExportGeneralSettings.ResumeLayout(False)
        Me.gpb_ExportGeneralSettings.PerformLayout()
        Me.gpb_ExportImage.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents lbl_exportmoviepath As System.Windows.Forms.Label
    Friend WithEvents btn_exportmoviepath As System.Windows.Forms.Button
    Friend WithEvents txt_exportmoviepath As System.Windows.Forms.TextBox
    Friend WithEvents lbl_exportmoviefanart As System.Windows.Forms.Label
    Friend WithEvents cbo_exportmoviefanart As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_exportmovieposter As System.Windows.Forms.Label
    Friend WithEvents cbo_exportmovieposter As System.Windows.Forms.ComboBox
    Friend WithEvents gpb_ExportImage As System.Windows.Forms.GroupBox
    Friend WithEvents gpb_ExportGeneralSettings As System.Windows.Forms.GroupBox
    Friend WithEvents btnSource As System.Windows.Forms.Button
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents lbl_exportmoviefiltergenerate As System.Windows.Forms.Label
    Friend WithEvents btn_Apply As System.Windows.Forms.Button
    Friend WithEvents lblIn As System.Windows.Forms.Label
    Friend WithEvents cbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lstSources As System.Windows.Forms.CheckedListBox
    Friend WithEvents gpb_ExportFilterSettings As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_exportmoviefilter1saved As System.Windows.Forms.Label
    Friend WithEvents lbl_exportmoviefilter1 As System.Windows.Forms.Label
    Friend WithEvents cbo_exportmoviefilter As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_exportmoviefilter2 As System.Windows.Forms.Label
    Friend WithEvents lbl_exportmoviefilter2saved As System.Windows.Forms.Label
    Friend WithEvents lbl_exportmoviefilter3 As System.Windows.Forms.Label
    Friend WithEvents lbl_exportmoviefilter3saved As System.Windows.Forms.Label
    Friend WithEvents chkExportTVShows As System.Windows.Forms.CheckBox
    Friend WithEvents cbo_exportmoviequality As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_exportmoviequality As System.Windows.Forms.Label

End Class
