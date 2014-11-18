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
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.cbEnabled = New System.Windows.Forms.CheckBox()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.gbFilterOpts = New System.Windows.Forms.GroupBox()
        Me.lblFilter3 = New System.Windows.Forms.Label()
        Me.lbl_exportmoviefilter3saved = New System.Windows.Forms.Label()
        Me.lblFilter2 = New System.Windows.Forms.Label()
        Me.lbl_exportmoviefilter2saved = New System.Windows.Forms.Label()
        Me.cbo_exportmoviefilter = New System.Windows.Forms.ComboBox()
        Me.lblFilter1 = New System.Windows.Forms.Label()
        Me.lbl_exportmoviefilter1saved = New System.Windows.Forms.Label()
        Me.lstSources = New System.Windows.Forms.CheckedListBox()
        Me.btnSource = New System.Windows.Forms.Button()
        Me.btn_Apply = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btn_Reset = New System.Windows.Forms.Button()
        Me.cbSearch = New System.Windows.Forms.ComboBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.lblIn = New System.Windows.Forms.Label()
        Me.gbGeneralOpts = New System.Windows.Forms.GroupBox()
        Me.chkExportTVShows = New System.Windows.Forms.CheckBox()
        Me.lblGeneralPath = New System.Windows.Forms.Label()
        Me.txt_exportmoviepath = New System.Windows.Forms.TextBox()
        Me.btn_exportmoviepath = New System.Windows.Forms.Button()
        Me.gbImageOpts = New System.Windows.Forms.GroupBox()
        Me.cbo_exportmoviequality = New System.Windows.Forms.ComboBox()
        Me.lblImageQuality = New System.Windows.Forms.Label()
        Me.cbo_exportmoviefanart = New System.Windows.Forms.ComboBox()
        Me.lblImageFanartWidth = New System.Windows.Forms.Label()
        Me.cbo_exportmovieposter = New System.Windows.Forms.ComboBox()
        Me.lblImagePosterHeight = New System.Windows.Forms.Label()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.tblGeneralOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.tblImageOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.tblFilterOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlSettingsTop.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.gbFilterOpts.SuspendLayout()
        Me.gbGeneralOpts.SuspendLayout()
        Me.gbImageOpts.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.tblGeneralOpts.SuspendLayout()
        Me.tblImageOpts.SuspendLayout()
        Me.tblFilterOpts.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(582, 23)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'cbEnabled
        '
        Me.cbEnabled.AutoSize = True
        Me.cbEnabled.Location = New System.Drawing.Point(8, 3)
        Me.cbEnabled.Name = "cbEnabled"
        Me.cbEnabled.Size = New System.Drawing.Size(68, 17)
        Me.cbEnabled.TabIndex = 0
        Me.cbEnabled.Text = "Enabled"
        Me.cbEnabled.UseVisualStyleBackColor = True
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(582, 392)
        Me.pnlSettings.TabIndex = 0
        '
        'gbFilterOpts
        '
        Me.gbFilterOpts.AutoSize = True
        Me.tblSettingsMain.SetColumnSpan(Me.gbFilterOpts, 2)
        Me.gbFilterOpts.Controls.Add(Me.tblFilterOpts)
        Me.gbFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbFilterOpts.Location = New System.Drawing.Point(3, 111)
        Me.gbFilterOpts.Name = "gbFilterOpts"
        Me.gbFilterOpts.Size = New System.Drawing.Size(558, 232)
        Me.gbFilterOpts.TabIndex = 22
        Me.gbFilterOpts.TabStop = False
        Me.gbFilterOpts.Text = "Filter Settings"
        '
        'lblFilter3
        '
        Me.lblFilter3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilter3.AutoSize = True
        Me.lblFilter3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilter3.Location = New System.Drawing.Point(3, 194)
        Me.lblFilter3.Name = "lblFilter3"
        Me.lblFilter3.Size = New System.Drawing.Size(42, 13)
        Me.lblFilter3.TabIndex = 14
        Me.lblFilter3.Text = "Filter 3"
        Me.lblFilter3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_exportmoviefilter3saved
        '
        Me.lbl_exportmoviefilter3saved.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lbl_exportmoviefilter3saved.AutoSize = True
        Me.tblFilterOpts.SetColumnSpan(Me.lbl_exportmoviefilter3saved, 5)
        Me.lbl_exportmoviefilter3saved.Enabled = False
        Me.lbl_exportmoviefilter3saved.Location = New System.Drawing.Point(92, 194)
        Me.lbl_exportmoviefilter3saved.Name = "lbl_exportmoviefilter3saved"
        Me.lbl_exportmoviefilter3saved.Size = New System.Drawing.Size(11, 13)
        Me.lbl_exportmoviefilter3saved.TabIndex = 13
        Me.lbl_exportmoviefilter3saved.Text = "-"
        Me.lbl_exportmoviefilter3saved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFilter2
        '
        Me.lblFilter2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilter2.AutoSize = True
        Me.lblFilter2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilter2.Location = New System.Drawing.Point(3, 174)
        Me.lblFilter2.Name = "lblFilter2"
        Me.lblFilter2.Size = New System.Drawing.Size(42, 13)
        Me.lblFilter2.TabIndex = 12
        Me.lblFilter2.Text = "Filter 2"
        Me.lblFilter2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_exportmoviefilter2saved
        '
        Me.lbl_exportmoviefilter2saved.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lbl_exportmoviefilter2saved.AutoSize = True
        Me.tblFilterOpts.SetColumnSpan(Me.lbl_exportmoviefilter2saved, 5)
        Me.lbl_exportmoviefilter2saved.Enabled = False
        Me.lbl_exportmoviefilter2saved.Location = New System.Drawing.Point(92, 174)
        Me.lbl_exportmoviefilter2saved.Name = "lbl_exportmoviefilter2saved"
        Me.lbl_exportmoviefilter2saved.Size = New System.Drawing.Size(11, 13)
        Me.lbl_exportmoviefilter2saved.TabIndex = 11
        Me.lbl_exportmoviefilter2saved.Text = "-"
        Me.lbl_exportmoviefilter2saved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbo_exportmoviefilter
        '
        Me.cbo_exportmoviefilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblFilterOpts.SetColumnSpan(Me.cbo_exportmoviefilter, 2)
        Me.cbo_exportmoviefilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_exportmoviefilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_exportmoviefilter.FormattingEnabled = True
        Me.cbo_exportmoviefilter.Items.AddRange(New Object() {"Filter 1", "Filter 2", "Filter 3"})
        Me.cbo_exportmoviefilter.Location = New System.Drawing.Point(92, 3)
        Me.cbo_exportmoviefilter.Name = "cbo_exportmoviefilter"
        Me.cbo_exportmoviefilter.Size = New System.Drawing.Size(77, 21)
        Me.cbo_exportmoviefilter.TabIndex = 10
        '
        'lblFilter1
        '
        Me.lblFilter1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilter1.AutoSize = True
        Me.lblFilter1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilter1.Location = New System.Drawing.Point(3, 154)
        Me.lblFilter1.Name = "lblFilter1"
        Me.lblFilter1.Size = New System.Drawing.Size(42, 13)
        Me.lblFilter1.TabIndex = 9
        Me.lblFilter1.Text = "Filter 1"
        Me.lblFilter1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_exportmoviefilter1saved
        '
        Me.lbl_exportmoviefilter1saved.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lbl_exportmoviefilter1saved.AutoSize = True
        Me.tblFilterOpts.SetColumnSpan(Me.lbl_exportmoviefilter1saved, 5)
        Me.lbl_exportmoviefilter1saved.Enabled = False
        Me.lbl_exportmoviefilter1saved.Location = New System.Drawing.Point(92, 154)
        Me.lbl_exportmoviefilter1saved.Name = "lbl_exportmoviefilter1saved"
        Me.lbl_exportmoviefilter1saved.Size = New System.Drawing.Size(11, 13)
        Me.lbl_exportmoviefilter1saved.TabIndex = 8
        Me.lbl_exportmoviefilter1saved.Text = "-"
        Me.lbl_exportmoviefilter1saved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstSources
        '
        Me.lstSources.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblFilterOpts.SetColumnSpan(Me.lstSources, 2)
        Me.lstSources.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstSources.FormattingEnabled = True
        Me.lstSources.Location = New System.Drawing.Point(3, 59)
        Me.lstSources.Name = "lstSources"
        Me.tblFilterOpts.SetRowSpan(Me.lstSources, 2)
        Me.lstSources.Size = New System.Drawing.Size(166, 89)
        Me.lstSources.TabIndex = 7
        Me.lstSources.Visible = False
        '
        'btnSource
        '
        Me.btnSource.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnSource.BackColor = System.Drawing.Color.MediumSpringGreen
        Me.btnSource.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSource.ImageIndex = 0
        Me.btnSource.Location = New System.Drawing.Point(175, 31)
        Me.btnSource.Name = "btnSource"
        Me.btnSource.Size = New System.Drawing.Size(38, 20)
        Me.btnSource.TabIndex = 2
        Me.btnSource.Text = "SET"
        Me.btnSource.UseVisualStyleBackColor = False
        Me.btnSource.Visible = False
        '
        'btn_Apply
        '
        Me.btn_Apply.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btn_Apply.BackColor = System.Drawing.Color.White
        Me.btn_Apply.Enabled = False
        Me.btn_Apply.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_Apply.Location = New System.Drawing.Point(365, 30)
        Me.btn_Apply.Name = "btn_Apply"
        Me.btn_Apply.Size = New System.Drawing.Size(101, 23)
        Me.btn_Apply.TabIndex = 5
        Me.btn_Apply.Text = "Save Filter"
        Me.btn_Apply.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblFilterOpts.SetColumnSpan(Me.txtSearch, 2)
        Me.txtSearch.Location = New System.Drawing.Point(3, 30)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(166, 22)
        Me.txtSearch.TabIndex = 3
        '
        'btn_Reset
        '
        Me.btn_Reset.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btn_Reset.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btn_Reset.Location = New System.Drawing.Point(365, 59)
        Me.btn_Reset.Name = "btn_Reset"
        Me.btn_Reset.Size = New System.Drawing.Size(101, 23)
        Me.btn_Reset.TabIndex = 6
        Me.btn_Reset.Text = "Reset Filter"
        Me.btn_Reset.UseVisualStyleBackColor = True
        '
        'cbSearch
        '
        Me.cbSearch.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbSearch.FormattingEnabled = True
        Me.cbSearch.Location = New System.Drawing.Point(242, 31)
        Me.cbSearch.Name = "cbSearch"
        Me.cbSearch.Size = New System.Drawing.Size(117, 21)
        Me.cbSearch.TabIndex = 4
        '
        'lblFilter
        '
        Me.lblFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblFilter.Location = New System.Drawing.Point(3, 7)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(83, 13)
        Me.lblFilter.TabIndex = 0
        Me.lblFilter.Text = "Generate Filter"
        Me.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIn
        '
        Me.lblIn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblIn.AutoSize = True
        Me.lblIn.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblIn.Location = New System.Drawing.Point(219, 35)
        Me.lblIn.Name = "lblIn"
        Me.lblIn.Size = New System.Drawing.Size(17, 13)
        Me.lblIn.TabIndex = 3
        Me.lblIn.Text = "in"
        Me.lblIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbGeneralOpts
        '
        Me.gbGeneralOpts.AutoSize = True
        Me.gbGeneralOpts.Controls.Add(Me.tblGeneralOpts)
        Me.gbGeneralOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGeneralOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbGeneralOpts.Name = "gbGeneralOpts"
        Me.gbGeneralOpts.Size = New System.Drawing.Size(335, 102)
        Me.gbGeneralOpts.TabIndex = 20
        Me.gbGeneralOpts.TabStop = False
        Me.gbGeneralOpts.Text = "General Settings"
        '
        'chkExportTVShows
        '
        Me.chkExportTVShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkExportTVShows.AutoSize = True
        Me.tblGeneralOpts.SetColumnSpan(Me.chkExportTVShows, 3)
        Me.chkExportTVShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkExportTVShows.Location = New System.Drawing.Point(3, 46)
        Me.chkExportTVShows.Name = "chkExportTVShows"
        Me.chkExportTVShows.Size = New System.Drawing.Size(111, 17)
        Me.chkExportTVShows.TabIndex = 15
        Me.chkExportTVShows.Text = "Export TV Shows"
        Me.chkExportTVShows.UseVisualStyleBackColor = True
        '
        'lblGeneralPath
        '
        Me.lblGeneralPath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGeneralPath.AutoSize = True
        Me.lblGeneralPath.Location = New System.Drawing.Point(3, 7)
        Me.lblGeneralPath.Name = "lblGeneralPath"
        Me.lblGeneralPath.Size = New System.Drawing.Size(63, 13)
        Me.lblGeneralPath.TabIndex = 12
        Me.lblGeneralPath.Text = "ExportPath"
        Me.lblGeneralPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_exportmoviepath
        '
        Me.txt_exportmoviepath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txt_exportmoviepath.Location = New System.Drawing.Point(72, 3)
        Me.txt_exportmoviepath.Name = "txt_exportmoviepath"
        Me.txt_exportmoviepath.ReadOnly = True
        Me.txt_exportmoviepath.Size = New System.Drawing.Size(230, 22)
        Me.txt_exportmoviepath.TabIndex = 13
        '
        'btn_exportmoviepath
        '
        Me.btn_exportmoviepath.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btn_exportmoviepath.Location = New System.Drawing.Point(305, 3)
        Me.btn_exportmoviepath.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_exportmoviepath.Name = "btn_exportmoviepath"
        Me.btn_exportmoviepath.Size = New System.Drawing.Size(24, 22)
        Me.btn_exportmoviepath.TabIndex = 14
        Me.btn_exportmoviepath.Text = "..."
        Me.btn_exportmoviepath.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btn_exportmoviepath.UseVisualStyleBackColor = True
        '
        'gbImageOpts
        '
        Me.gbImageOpts.AutoSize = True
        Me.gbImageOpts.Controls.Add(Me.tblImageOpts)
        Me.gbImageOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbImageOpts.Location = New System.Drawing.Point(344, 3)
        Me.gbImageOpts.Name = "gbImageOpts"
        Me.gbImageOpts.Size = New System.Drawing.Size(217, 102)
        Me.gbImageOpts.TabIndex = 19
        Me.gbImageOpts.TabStop = False
        Me.gbImageOpts.Text = "Image Settings"
        '
        'cbo_exportmoviequality
        '
        Me.cbo_exportmoviequality.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbo_exportmoviequality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_exportmoviequality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_exportmoviequality.FormattingEnabled = True
        Me.cbo_exportmoviequality.Items.AddRange(New Object() {"60", "70", "80", "90", "100"})
        Me.cbo_exportmoviequality.Location = New System.Drawing.Point(107, 57)
        Me.cbo_exportmoviequality.Name = "cbo_exportmoviequality"
        Me.cbo_exportmoviequality.Size = New System.Drawing.Size(101, 21)
        Me.cbo_exportmoviequality.TabIndex = 20
        '
        'lblImageQuality
        '
        Me.lblImageQuality.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblImageQuality.AutoSize = True
        Me.lblImageQuality.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblImageQuality.Location = New System.Drawing.Point(3, 61)
        Me.lblImageQuality.Name = "lblImageQuality"
        Me.lblImageQuality.Size = New System.Drawing.Size(43, 13)
        Me.lblImageQuality.TabIndex = 19
        Me.lblImageQuality.Text = "Quality"
        Me.lblImageQuality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbo_exportmoviefanart
        '
        Me.cbo_exportmoviefanart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbo_exportmoviefanart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_exportmoviefanart.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_exportmoviefanart.FormattingEnabled = True
        Me.cbo_exportmoviefanart.Items.AddRange(New Object() {"400", "600", "800", "1200", "original"})
        Me.cbo_exportmoviefanart.Location = New System.Drawing.Point(107, 3)
        Me.cbo_exportmoviefanart.Name = "cbo_exportmoviefanart"
        Me.cbo_exportmoviefanart.Size = New System.Drawing.Size(101, 21)
        Me.cbo_exportmoviefanart.TabIndex = 18
        '
        'lblImageFanartWidth
        '
        Me.lblImageFanartWidth.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblImageFanartWidth.AutoSize = True
        Me.lblImageFanartWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblImageFanartWidth.Location = New System.Drawing.Point(3, 7)
        Me.lblImageFanartWidth.Name = "lblImageFanartWidth"
        Me.lblImageFanartWidth.Size = New System.Drawing.Size(96, 13)
        Me.lblImageFanartWidth.TabIndex = 16
        Me.lblImageFanartWidth.Text = "Fanart Width [px]"
        Me.lblImageFanartWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbo_exportmovieposter
        '
        Me.cbo_exportmovieposter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbo_exportmovieposter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbo_exportmovieposter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbo_exportmovieposter.FormattingEnabled = True
        Me.cbo_exportmovieposter.Items.AddRange(New Object() {"300", "400", "600", "800", "original"})
        Me.cbo_exportmovieposter.Location = New System.Drawing.Point(107, 30)
        Me.cbo_exportmovieposter.Name = "cbo_exportmovieposter"
        Me.cbo_exportmovieposter.Size = New System.Drawing.Size(101, 21)
        Me.cbo_exportmovieposter.TabIndex = 17
        '
        'lblImagePosterHeight
        '
        Me.lblImagePosterHeight.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblImagePosterHeight.AutoSize = True
        Me.lblImagePosterHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblImagePosterHeight.Location = New System.Drawing.Point(3, 34)
        Me.lblImagePosterHeight.Name = "lblImagePosterHeight"
        Me.lblImagePosterHeight.Size = New System.Drawing.Size(98, 13)
        Me.lblImagePosterHeight.TabIndex = 15
        Me.lblImagePosterHeight.Text = "Poster Height [px]"
        Me.lblImagePosterHeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 2
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.cbEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(582, 23)
        Me.tblSettingsTop.TabIndex = 15
        '
        'tblGeneralOpts
        '
        Me.tblGeneralOpts.AutoSize = True
        Me.tblGeneralOpts.ColumnCount = 4
        Me.tblGeneralOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGeneralOpts.Controls.Add(Me.chkExportTVShows, 0, 1)
        Me.tblGeneralOpts.Controls.Add(Me.lblGeneralPath, 0, 0)
        Me.tblGeneralOpts.Controls.Add(Me.btn_exportmoviepath, 2, 0)
        Me.tblGeneralOpts.Controls.Add(Me.txt_exportmoviepath, 1, 0)
        Me.tblGeneralOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGeneralOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblGeneralOpts.Name = "tblGeneralOpts"
        Me.tblGeneralOpts.RowCount = 2
        Me.tblGeneralOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGeneralOpts.Size = New System.Drawing.Size(329, 81)
        Me.tblGeneralOpts.TabIndex = 23
        '
        'tblImageOpts
        '
        Me.tblImageOpts.AutoSize = True
        Me.tblImageOpts.ColumnCount = 3
        Me.tblImageOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageOpts.Controls.Add(Me.cbo_exportmoviequality, 1, 2)
        Me.tblImageOpts.Controls.Add(Me.lblImageFanartWidth, 0, 0)
        Me.tblImageOpts.Controls.Add(Me.cbo_exportmovieposter, 1, 1)
        Me.tblImageOpts.Controls.Add(Me.cbo_exportmoviefanart, 1, 0)
        Me.tblImageOpts.Controls.Add(Me.lblImageQuality, 0, 2)
        Me.tblImageOpts.Controls.Add(Me.lblImagePosterHeight, 0, 1)
        Me.tblImageOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImageOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblImageOpts.Name = "tblImageOpts"
        Me.tblImageOpts.RowCount = 4
        Me.tblImageOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageOpts.Size = New System.Drawing.Size(211, 81)
        Me.tblImageOpts.TabIndex = 23
        '
        'tblFilterOpts
        '
        Me.tblFilterOpts.AutoSize = True
        Me.tblFilterOpts.ColumnCount = 7
        Me.tblFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblFilterOpts.Controls.Add(Me.lbl_exportmoviefilter3saved, 1, 6)
        Me.tblFilterOpts.Controls.Add(Me.lblFilter3, 0, 6)
        Me.tblFilterOpts.Controls.Add(Me.txtSearch, 0, 1)
        Me.tblFilterOpts.Controls.Add(Me.btnSource, 2, 1)
        Me.tblFilterOpts.Controls.Add(Me.lbl_exportmoviefilter2saved, 1, 5)
        Me.tblFilterOpts.Controls.Add(Me.lblFilter2, 0, 5)
        Me.tblFilterOpts.Controls.Add(Me.lblIn, 3, 1)
        Me.tblFilterOpts.Controls.Add(Me.lbl_exportmoviefilter1saved, 1, 4)
        Me.tblFilterOpts.Controls.Add(Me.lblFilter1, 0, 4)
        Me.tblFilterOpts.Controls.Add(Me.lstSources, 0, 2)
        Me.tblFilterOpts.Controls.Add(Me.lblFilter, 0, 0)
        Me.tblFilterOpts.Controls.Add(Me.cbo_exportmoviefilter, 1, 0)
        Me.tblFilterOpts.Controls.Add(Me.btn_Apply, 6, 1)
        Me.tblFilterOpts.Controls.Add(Me.cbSearch, 4, 1)
        Me.tblFilterOpts.Controls.Add(Me.btn_Reset, 6, 2)
        Me.tblFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFilterOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblFilterOpts.Name = "tblFilterOpts"
        Me.tblFilterOpts.RowCount = 8
        Me.tblFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblFilterOpts.Size = New System.Drawing.Size(552, 211)
        Me.tblFilterOpts.TabIndex = 23
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(582, 369)
        Me.pnlSettingsMain.TabIndex = 23
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 3
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbGeneralOpts, 0, 0)
        Me.tblSettingsMain.Controls.Add(Me.gbFilterOpts, 0, 1)
        Me.tblSettingsMain.Controls.Add(Me.gbImageOpts, 1, 0)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 3
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.Size = New System.Drawing.Size(582, 369)
        Me.tblSettingsMain.TabIndex = 0
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(582, 392)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmSettingsHolder"
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.gbFilterOpts.ResumeLayout(False)
        Me.gbFilterOpts.PerformLayout()
        Me.gbGeneralOpts.ResumeLayout(False)
        Me.gbGeneralOpts.PerformLayout()
        Me.gbImageOpts.ResumeLayout(False)
        Me.gbImageOpts.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.tblGeneralOpts.ResumeLayout(False)
        Me.tblGeneralOpts.PerformLayout()
        Me.tblImageOpts.ResumeLayout(False)
        Me.tblImageOpts.PerformLayout()
        Me.tblFilterOpts.ResumeLayout(False)
        Me.tblFilterOpts.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents cbEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents lblGeneralPath As System.Windows.Forms.Label
    Friend WithEvents btn_exportmoviepath As System.Windows.Forms.Button
    Friend WithEvents txt_exportmoviepath As System.Windows.Forms.TextBox
    Friend WithEvents lblImageFanartWidth As System.Windows.Forms.Label
    Friend WithEvents cbo_exportmoviefanart As System.Windows.Forms.ComboBox
    Friend WithEvents lblImagePosterHeight As System.Windows.Forms.Label
    Friend WithEvents cbo_exportmovieposter As System.Windows.Forms.ComboBox
    Friend WithEvents gbImageOpts As System.Windows.Forms.GroupBox
    Friend WithEvents gbGeneralOpts As System.Windows.Forms.GroupBox
    Friend WithEvents btnSource As System.Windows.Forms.Button
    Friend WithEvents btn_Reset As System.Windows.Forms.Button
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents btn_Apply As System.Windows.Forms.Button
    Friend WithEvents lblIn As System.Windows.Forms.Label
    Friend WithEvents cbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lstSources As System.Windows.Forms.CheckedListBox
    Friend WithEvents gbFilterOpts As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_exportmoviefilter1saved As System.Windows.Forms.Label
    Friend WithEvents lblFilter1 As System.Windows.Forms.Label
    Friend WithEvents cbo_exportmoviefilter As System.Windows.Forms.ComboBox
    Friend WithEvents lblFilter2 As System.Windows.Forms.Label
    Friend WithEvents lbl_exportmoviefilter2saved As System.Windows.Forms.Label
    Friend WithEvents lblFilter3 As System.Windows.Forms.Label
    Friend WithEvents lbl_exportmoviefilter3saved As System.Windows.Forms.Label
    Friend WithEvents chkExportTVShows As System.Windows.Forms.CheckBox
    Friend WithEvents cbo_exportmoviequality As System.Windows.Forms.ComboBox
    Friend WithEvents lblImageQuality As System.Windows.Forms.Label
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblGeneralOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblFilterOpts As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents tblImageOpts As System.Windows.Forms.TableLayoutPanel

End Class
