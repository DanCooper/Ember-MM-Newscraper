<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgFIStreamEditor
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
        Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.gbVideoStreams = New System.Windows.Forms.GroupBox()
        Me.lblVideoFileSize = New System.Windows.Forms.Label()
        Me.txtVideoFileSize = New System.Windows.Forms.TextBox()
        Me.txtVideoStereoMode = New System.Windows.Forms.TextBox()
        Me.lblVideoStereoMode = New System.Windows.Forms.Label()
        Me.cbVideoMultiViewLayout = New System.Windows.Forms.ComboBox()
        Me.lblVideoMultiViewLayout = New System.Windows.Forms.Label()
        Me.lblVideoMultiViewCount = New System.Windows.Forms.Label()
        Me.txtVideoMultiViewCount = New System.Windows.Forms.TextBox()
        Me.lblVideoBitrate = New System.Windows.Forms.Label()
        Me.txtVideoBitrate = New System.Windows.Forms.TextBox()
        Me.lblVideoLanguage = New System.Windows.Forms.Label()
        Me.cbVideoLanguage = New System.Windows.Forms.ComboBox()
        Me.lblVideoAspect = New System.Windows.Forms.Label()
        Me.txtVideoAspect = New System.Windows.Forms.TextBox()
        Me.rbVideoInterlaced = New System.Windows.Forms.RadioButton()
        Me.rbVideoProgressive = New System.Windows.Forms.RadioButton()
        Me.lblVideoCodec = New System.Windows.Forms.Label()
        Me.cbVideoCodec = New System.Windows.Forms.ComboBox()
        Me.lblVideoDuration = New System.Windows.Forms.Label()
        Me.txtVideoDuration = New System.Windows.Forms.TextBox()
        Me.lblVideoHeight = New System.Windows.Forms.Label()
        Me.lblVideoWidth = New System.Windows.Forms.Label()
        Me.txtVideoHeight = New System.Windows.Forms.TextBox()
        Me.txtVideoWidth = New System.Windows.Forms.TextBox()
        Me.gbAudioStreams = New System.Windows.Forms.GroupBox()
        Me.txtAudioBitrate = New System.Windows.Forms.TextBox()
        Me.lblAudioBitrate = New System.Windows.Forms.Label()
        Me.lblAudioChannels = New System.Windows.Forms.Label()
        Me.cbAudioChannels = New System.Windows.Forms.ComboBox()
        Me.lblAudioCodec = New System.Windows.Forms.Label()
        Me.cbAudioCodec = New System.Windows.Forms.ComboBox()
        Me.lblAudioLanguage = New System.Windows.Forms.Label()
        Me.cbAudioLanguage = New System.Windows.Forms.ComboBox()
        Me.gbSubtitleStreams = New System.Windows.Forms.GroupBox()
        Me.txtSubtitleType = New System.Windows.Forms.TextBox()
        Me.txtSubtitlePath = New System.Windows.Forms.TextBox()
        Me.chkSubtitleForced = New System.Windows.Forms.CheckBox()
        Me.lblSubtitleLanguage = New System.Windows.Forms.Label()
        Me.cbSubtitleLanguage = New System.Windows.Forms.ComboBox()
        Me.pnlStreamEditor = New System.Windows.Forms.Panel()
        Me.tlpButtons.SuspendLayout()
        Me.gbVideoStreams.SuspendLayout()
        Me.gbAudioStreams.SuspendLayout()
        Me.gbSubtitleStreams.SuspendLayout()
        Me.pnlStreamEditor.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpButtons
        '
        Me.tlpButtons.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpButtons.ColumnCount = 2
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Controls.Add(Me.btnOK, 0, 0)
        Me.tlpButtons.Controls.Add(Me.btnCancel, 1, 0)
        Me.tlpButtons.Location = New System.Drawing.Point(178, 439)
        Me.tlpButtons.Name = "tlpButtons"
        Me.tlpButtons.RowCount = 1
        Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Size = New System.Drawing.Size(146, 29)
        Me.tlpButtons.TabIndex = 0
        '
        'OK_Button
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOK.Location = New System.Drawing.Point(3, 3)
        Me.btnOK.Name = "OK_Button"
        Me.btnOK.Size = New System.Drawing.Size(67, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        '
        'Cancel_Button
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(76, 3)
        Me.btnCancel.Name = "Cancel_Button"
        Me.btnCancel.Size = New System.Drawing.Size(67, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'gbVideoStreams
        '
        Me.gbVideoStreams.Controls.Add(Me.lblVideoFileSize)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoFileSize)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoStereoMode)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoStereoMode)
        Me.gbVideoStreams.Controls.Add(Me.cbVideoMultiViewLayout)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoMultiViewLayout)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoMultiViewCount)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoMultiViewCount)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoBitrate)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoBitrate)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoLanguage)
        Me.gbVideoStreams.Controls.Add(Me.cbVideoLanguage)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoAspect)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoAspect)
        Me.gbVideoStreams.Controls.Add(Me.rbVideoInterlaced)
        Me.gbVideoStreams.Controls.Add(Me.rbVideoProgressive)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoCodec)
        Me.gbVideoStreams.Controls.Add(Me.cbVideoCodec)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoDuration)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoDuration)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoHeight)
        Me.gbVideoStreams.Controls.Add(Me.lblVideoWidth)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoHeight)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoWidth)
        Me.gbVideoStreams.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbVideoStreams.Location = New System.Drawing.Point(3, 3)
        Me.gbVideoStreams.Name = "gbVideoStreams"
        Me.gbVideoStreams.Size = New System.Drawing.Size(314, 343)
        Me.gbVideoStreams.TabIndex = 0
        Me.gbVideoStreams.TabStop = False
        Me.gbVideoStreams.Text = "Video Streams"
        Me.gbVideoStreams.Visible = False
        '
        'lblVideoFileSize
        '
        Me.lblVideoFileSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoFileSize.Location = New System.Drawing.Point(6, 179)
        Me.lblVideoFileSize.Name = "lblVideoFileSize"
        Me.lblVideoFileSize.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoFileSize.TabIndex = 23
        Me.lblVideoFileSize.Text = "FileSize [MB]"
        Me.lblVideoFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoFileSize
        '
        Me.txtVideoFileSize.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoFileSize.Location = New System.Drawing.Point(150, 176)
        Me.txtVideoFileSize.Name = "txtVideoFileSize"
        Me.txtVideoFileSize.Size = New System.Drawing.Size(68, 22)
        Me.txtVideoFileSize.TabIndex = 24
        '
        'txtVideoStereoMode
        '
        Me.txtVideoStereoMode.Enabled = False
        Me.txtVideoStereoMode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoStereoMode.Location = New System.Drawing.Point(149, 306)
        Me.txtVideoStereoMode.Name = "txtVideoStereoMode"
        Me.txtVideoStereoMode.Size = New System.Drawing.Size(158, 22)
        Me.txtVideoStereoMode.TabIndex = 22
        '
        'lblVideoStereoMode
        '
        Me.lblVideoStereoMode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoStereoMode.Location = New System.Drawing.Point(5, 309)
        Me.lblVideoStereoMode.Name = "lblVideoStereoMode"
        Me.lblVideoStereoMode.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoStereoMode.TabIndex = 21
        Me.lblVideoStereoMode.Text = "StereoMode"
        Me.lblVideoStereoMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbVideoMultiViewLayout
        '
        Me.cbVideoMultiViewLayout.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbVideoMultiViewLayout.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbVideoMultiViewLayout.DropDownWidth = 120
        Me.cbVideoMultiViewLayout.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbVideoMultiViewLayout.FormattingEnabled = True
        Me.cbVideoMultiViewLayout.Items.AddRange(New Object() {"", "Side by Side (left eye first)", "Top-Bottom (right eye first)", "Top-Bottom (left eye first)", "Checkboard (right eye first)", "Checkboard (left eye first)", "Row Interleaved (right eye first)", "Row Interleaved (left eye first)", "Column Interleaved (right eye first)", "Column Interleaved (left eye first)", "Anaglyph (cyan/red)", "Side by Side (right eye first)", "Anaglyph (green/magenta) ", "Both Eyes laced in one block (left eye first)", "Both Eyes laced in one block (right eye first)"})
        Me.cbVideoMultiViewLayout.Location = New System.Drawing.Point(149, 281)
        Me.cbVideoMultiViewLayout.Name = "cbVideoMultiViewLayout"
        Me.cbVideoMultiViewLayout.Size = New System.Drawing.Size(158, 21)
        Me.cbVideoMultiViewLayout.TabIndex = 20
        '
        'lblVideoMultiViewLayout
        '
        Me.lblVideoMultiViewLayout.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoMultiViewLayout.Location = New System.Drawing.Point(5, 284)
        Me.lblVideoMultiViewLayout.Name = "lblVideoMultiViewLayout"
        Me.lblVideoMultiViewLayout.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoMultiViewLayout.TabIndex = 18
        Me.lblVideoMultiViewLayout.Text = "MultiView Layout"
        Me.lblVideoMultiViewLayout.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVideoMultiViewCount
        '
        Me.lblVideoMultiViewCount.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoMultiViewCount.Location = New System.Drawing.Point(5, 258)
        Me.lblVideoMultiViewCount.Name = "lblVideoMultiViewCount"
        Me.lblVideoMultiViewCount.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoMultiViewCount.TabIndex = 16
        Me.lblVideoMultiViewCount.Text = "MultiView Count"
        Me.lblVideoMultiViewCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoMultiViewCount
        '
        Me.txtVideoMultiViewCount.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoMultiViewCount.Location = New System.Drawing.Point(149, 255)
        Me.txtVideoMultiViewCount.Name = "txtVideoMultiViewCount"
        Me.txtVideoMultiViewCount.Size = New System.Drawing.Size(48, 22)
        Me.txtVideoMultiViewCount.TabIndex = 17
        '
        'lblVideoBitrate
        '
        Me.lblVideoBitrate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoBitrate.Location = New System.Drawing.Point(5, 232)
        Me.lblVideoBitrate.Name = "lblVideoBitrate"
        Me.lblVideoBitrate.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoBitrate.TabIndex = 14
        Me.lblVideoBitrate.Text = "Bitrate"
        Me.lblVideoBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoBitrate
        '
        Me.txtVideoBitrate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoBitrate.Location = New System.Drawing.Point(149, 229)
        Me.txtVideoBitrate.Name = "txtVideoBitrate"
        Me.txtVideoBitrate.Size = New System.Drawing.Size(68, 22)
        Me.txtVideoBitrate.TabIndex = 15
        '
        'lblVideoLanguage
        '
        Me.lblVideoLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoLanguage.Location = New System.Drawing.Point(5, 207)
        Me.lblVideoLanguage.Name = "lblVideoLanguage"
        Me.lblVideoLanguage.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoLanguage.TabIndex = 12
        Me.lblVideoLanguage.Text = "Language"
        Me.lblVideoLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbVideoLanguage
        '
        Me.cbVideoLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbVideoLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbVideoLanguage.DropDownWidth = 120
        Me.cbVideoLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbVideoLanguage.FormattingEnabled = True
        Me.cbVideoLanguage.Location = New System.Drawing.Point(149, 204)
        Me.cbVideoLanguage.Name = "cbVideoLanguage"
        Me.cbVideoLanguage.Size = New System.Drawing.Size(93, 21)
        Me.cbVideoLanguage.TabIndex = 13
        '
        'lblVideoAspect
        '
        Me.lblVideoAspect.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoAspect.Location = New System.Drawing.Point(6, 125)
        Me.lblVideoAspect.Name = "lblVideoAspect"
        Me.lblVideoAspect.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoAspect.TabIndex = 8
        Me.lblVideoAspect.Text = "Aspect Ratio"
        Me.lblVideoAspect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoAspect
        '
        Me.txtVideoAspect.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoAspect.Location = New System.Drawing.Point(150, 122)
        Me.txtVideoAspect.Name = "txtVideoAspect"
        Me.txtVideoAspect.Size = New System.Drawing.Size(48, 22)
        Me.txtVideoAspect.TabIndex = 9
        '
        'rbVideoInterlaced
        '
        Me.rbVideoInterlaced.AutoSize = True
        Me.rbVideoInterlaced.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbVideoInterlaced.Location = New System.Drawing.Point(185, 47)
        Me.rbVideoInterlaced.Name = "rbVideoInterlaced"
        Me.rbVideoInterlaced.Size = New System.Drawing.Size(76, 17)
        Me.rbVideoInterlaced.TabIndex = 3
        Me.rbVideoInterlaced.TabStop = True
        Me.rbVideoInterlaced.Text = "Interlaced"
        Me.rbVideoInterlaced.UseVisualStyleBackColor = True
        '
        'rbVideoProgressive
        '
        Me.rbVideoProgressive.AutoSize = True
        Me.rbVideoProgressive.Checked = True
        Me.rbVideoProgressive.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbVideoProgressive.Location = New System.Drawing.Point(99, 47)
        Me.rbVideoProgressive.Name = "rbVideoProgressive"
        Me.rbVideoProgressive.Size = New System.Drawing.Size(85, 17)
        Me.rbVideoProgressive.TabIndex = 2
        Me.rbVideoProgressive.TabStop = True
        Me.rbVideoProgressive.Text = "Progressive"
        Me.rbVideoProgressive.UseVisualStyleBackColor = True
        '
        'lblVideoCodec
        '
        Me.lblVideoCodec.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoCodec.Location = New System.Drawing.Point(93, 23)
        Me.lblVideoCodec.Name = "lblVideoCodec"
        Me.lblVideoCodec.Size = New System.Drawing.Size(51, 15)
        Me.lblVideoCodec.TabIndex = 0
        Me.lblVideoCodec.Text = "Codec"
        Me.lblVideoCodec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbVideoCodec
        '
        Me.cbVideoCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVideoCodec.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbVideoCodec.FormattingEnabled = True
        Me.cbVideoCodec.Location = New System.Drawing.Point(150, 21)
        Me.cbVideoCodec.Name = "cbVideoCodec"
        Me.cbVideoCodec.Size = New System.Drawing.Size(93, 21)
        Me.cbVideoCodec.TabIndex = 1
        '
        'lblVideoDuration
        '
        Me.lblVideoDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoDuration.Location = New System.Drawing.Point(6, 151)
        Me.lblVideoDuration.Name = "lblVideoDuration"
        Me.lblVideoDuration.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoDuration.TabIndex = 10
        Me.lblVideoDuration.Text = "Duration"
        Me.lblVideoDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoDuration
        '
        Me.txtVideoDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoDuration.Location = New System.Drawing.Point(150, 148)
        Me.txtVideoDuration.Name = "txtVideoDuration"
        Me.txtVideoDuration.Size = New System.Drawing.Size(68, 22)
        Me.txtVideoDuration.TabIndex = 11
        '
        'lblVideoHeight
        '
        Me.lblVideoHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoHeight.Location = New System.Drawing.Point(6, 99)
        Me.lblVideoHeight.Name = "lblVideoHeight"
        Me.lblVideoHeight.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoHeight.TabIndex = 6
        Me.lblVideoHeight.Text = "Height"
        Me.lblVideoHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVideoWidth
        '
        Me.lblVideoWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoWidth.Location = New System.Drawing.Point(6, 73)
        Me.lblVideoWidth.Name = "lblVideoWidth"
        Me.lblVideoWidth.Size = New System.Drawing.Size(138, 13)
        Me.lblVideoWidth.TabIndex = 4
        Me.lblVideoWidth.Text = "Width"
        Me.lblVideoWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoHeight
        '
        Me.txtVideoHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoHeight.Location = New System.Drawing.Point(150, 96)
        Me.txtVideoHeight.Name = "txtVideoHeight"
        Me.txtVideoHeight.Size = New System.Drawing.Size(48, 22)
        Me.txtVideoHeight.TabIndex = 7
        '
        'txtVideoWidth
        '
        Me.txtVideoWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoWidth.Location = New System.Drawing.Point(150, 70)
        Me.txtVideoWidth.Name = "txtVideoWidth"
        Me.txtVideoWidth.Size = New System.Drawing.Size(48, 22)
        Me.txtVideoWidth.TabIndex = 5
        '
        'gbAudioStreams
        '
        Me.gbAudioStreams.Controls.Add(Me.txtAudioBitrate)
        Me.gbAudioStreams.Controls.Add(Me.lblAudioBitrate)
        Me.gbAudioStreams.Controls.Add(Me.lblAudioChannels)
        Me.gbAudioStreams.Controls.Add(Me.cbAudioChannels)
        Me.gbAudioStreams.Controls.Add(Me.lblAudioCodec)
        Me.gbAudioStreams.Controls.Add(Me.cbAudioCodec)
        Me.gbAudioStreams.Controls.Add(Me.lblAudioLanguage)
        Me.gbAudioStreams.Controls.Add(Me.cbAudioLanguage)
        Me.gbAudioStreams.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbAudioStreams.Location = New System.Drawing.Point(3, 3)
        Me.gbAudioStreams.Name = "gbAudioStreams"
        Me.gbAudioStreams.Size = New System.Drawing.Size(315, 194)
        Me.gbAudioStreams.TabIndex = 5
        Me.gbAudioStreams.TabStop = False
        Me.gbAudioStreams.Text = "Audio Streams"
        Me.gbAudioStreams.Visible = False
        '
        'txtAudioBitrate
        '
        Me.txtAudioBitrate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtAudioBitrate.Location = New System.Drawing.Point(100, 100)
        Me.txtAudioBitrate.Name = "txtAudioBitrate"
        Me.txtAudioBitrate.Size = New System.Drawing.Size(93, 22)
        Me.txtAudioBitrate.TabIndex = 19
        '
        'lblAudioBitrate
        '
        Me.lblAudioBitrate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAudioBitrate.Location = New System.Drawing.Point(6, 103)
        Me.lblAudioBitrate.Name = "lblAudioBitrate"
        Me.lblAudioBitrate.Size = New System.Drawing.Size(88, 13)
        Me.lblAudioBitrate.TabIndex = 18
        Me.lblAudioBitrate.Text = "Bitrate"
        Me.lblAudioBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAudioChannels
        '
        Me.lblAudioChannels.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAudioChannels.Location = New System.Drawing.Point(6, 76)
        Me.lblAudioChannels.Name = "lblAudioChannels"
        Me.lblAudioChannels.Size = New System.Drawing.Size(88, 13)
        Me.lblAudioChannels.TabIndex = 4
        Me.lblAudioChannels.Text = "Channels"
        Me.lblAudioChannels.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAudioChannels
        '
        Me.cbAudioChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAudioChannels.FormattingEnabled = True
        Me.cbAudioChannels.Location = New System.Drawing.Point(100, 73)
        Me.cbAudioChannels.Name = "cbAudioChannels"
        Me.cbAudioChannels.Size = New System.Drawing.Size(209, 21)
        Me.cbAudioChannels.TabIndex = 5
        '
        'lblAudioCodec
        '
        Me.lblAudioCodec.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAudioCodec.Location = New System.Drawing.Point(6, 49)
        Me.lblAudioCodec.Name = "lblAudioCodec"
        Me.lblAudioCodec.Size = New System.Drawing.Size(88, 13)
        Me.lblAudioCodec.TabIndex = 2
        Me.lblAudioCodec.Text = "Codec"
        Me.lblAudioCodec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAudioCodec
        '
        Me.cbAudioCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAudioCodec.FormattingEnabled = True
        Me.cbAudioCodec.Location = New System.Drawing.Point(100, 46)
        Me.cbAudioCodec.Name = "cbAudioCodec"
        Me.cbAudioCodec.Size = New System.Drawing.Size(209, 21)
        Me.cbAudioCodec.TabIndex = 3
        '
        'lblAudioLanguage
        '
        Me.lblAudioLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAudioLanguage.Location = New System.Drawing.Point(6, 22)
        Me.lblAudioLanguage.Name = "lblAudioLanguage"
        Me.lblAudioLanguage.Size = New System.Drawing.Size(88, 13)
        Me.lblAudioLanguage.TabIndex = 0
        Me.lblAudioLanguage.Text = "Language"
        Me.lblAudioLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAudioLanguage
        '
        Me.cbAudioLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbAudioLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbAudioLanguage.DropDownWidth = 120
        Me.cbAudioLanguage.FormattingEnabled = True
        Me.cbAudioLanguage.Location = New System.Drawing.Point(100, 19)
        Me.cbAudioLanguage.Name = "cbAudioLanguage"
        Me.cbAudioLanguage.Size = New System.Drawing.Size(209, 21)
        Me.cbAudioLanguage.Sorted = True
        Me.cbAudioLanguage.TabIndex = 1
        '
        'gbSubtitleStreams
        '
        Me.gbSubtitleStreams.Controls.Add(Me.txtSubtitleType)
        Me.gbSubtitleStreams.Controls.Add(Me.txtSubtitlePath)
        Me.gbSubtitleStreams.Controls.Add(Me.chkSubtitleForced)
        Me.gbSubtitleStreams.Controls.Add(Me.lblSubtitleLanguage)
        Me.gbSubtitleStreams.Controls.Add(Me.cbSubtitleLanguage)
        Me.gbSubtitleStreams.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSubtitleStreams.Location = New System.Drawing.Point(3, 3)
        Me.gbSubtitleStreams.Name = "gbSubtitleStreams"
        Me.gbSubtitleStreams.Size = New System.Drawing.Size(315, 73)
        Me.gbSubtitleStreams.TabIndex = 6
        Me.gbSubtitleStreams.TabStop = False
        Me.gbSubtitleStreams.Text = "Subtitle  Streams"
        Me.gbSubtitleStreams.Visible = False
        '
        'txtSubtitleType
        '
        Me.txtSubtitleType.Enabled = False
        Me.txtSubtitleType.Location = New System.Drawing.Point(189, 44)
        Me.txtSubtitleType.Name = "txtSubtitleType"
        Me.txtSubtitleType.Size = New System.Drawing.Size(54, 22)
        Me.txtSubtitleType.TabIndex = 25
        Me.txtSubtitleType.Visible = False
        '
        'txtSubtitlePath
        '
        Me.txtSubtitlePath.Enabled = False
        Me.txtSubtitlePath.Location = New System.Drawing.Point(248, 44)
        Me.txtSubtitlePath.Name = "txtSubtitlePath"
        Me.txtSubtitlePath.Size = New System.Drawing.Size(54, 22)
        Me.txtSubtitlePath.TabIndex = 24
        Me.txtSubtitlePath.Visible = False
        '
        'chkSubtitleForced
        '
        Me.chkSubtitleForced.AutoSize = True
        Me.chkSubtitleForced.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSubtitleForced.Location = New System.Drawing.Point(53, 46)
        Me.chkSubtitleForced.Name = "chkSubtitleForced"
        Me.chkSubtitleForced.Size = New System.Drawing.Size(61, 17)
        Me.chkSubtitleForced.TabIndex = 23
        Me.chkSubtitleForced.Text = "Forced"
        Me.chkSubtitleForced.UseVisualStyleBackColor = True
        '
        'lblSubtitleLanguage
        '
        Me.lblSubtitleLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitleLanguage.Location = New System.Drawing.Point(6, 22)
        Me.lblSubtitleLanguage.Name = "lblSubtitleLanguage"
        Me.lblSubtitleLanguage.Size = New System.Drawing.Size(89, 13)
        Me.lblSubtitleLanguage.TabIndex = 22
        Me.lblSubtitleLanguage.Text = "Language"
        Me.lblSubtitleLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbSubtitleLanguage
        '
        Me.cbSubtitleLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbSubtitleLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbSubtitleLanguage.DropDownWidth = 120
        Me.cbSubtitleLanguage.FormattingEnabled = True
        Me.cbSubtitleLanguage.Location = New System.Drawing.Point(100, 19)
        Me.cbSubtitleLanguage.Name = "cbSubtitleLanguage"
        Me.cbSubtitleLanguage.Size = New System.Drawing.Size(202, 21)
        Me.cbSubtitleLanguage.Sorted = True
        Me.cbSubtitleLanguage.TabIndex = 0
        '
        'pnlStreamEditor
        '
        Me.pnlStreamEditor.BackColor = System.Drawing.Color.White
        Me.pnlStreamEditor.Controls.Add(Me.gbVideoStreams)
        Me.pnlStreamEditor.Controls.Add(Me.gbSubtitleStreams)
        Me.pnlStreamEditor.Controls.Add(Me.gbAudioStreams)
        Me.pnlStreamEditor.Location = New System.Drawing.Point(3, 3)
        Me.pnlStreamEditor.Name = "pnlStreamEditor"
        Me.pnlStreamEditor.Size = New System.Drawing.Size(321, 349)
        Me.pnlStreamEditor.TabIndex = 1
        '
        'dlgFIStreamEditor
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(332, 471)
        Me.Controls.Add(Me.pnlStreamEditor)
        Me.Controls.Add(Me.tlpButtons)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgFIStreamEditor"
        Me.ShowInTaskbar = False
        Me.Text = "Stream Editor"
        Me.tlpButtons.ResumeLayout(False)
        Me.gbVideoStreams.ResumeLayout(False)
        Me.gbVideoStreams.PerformLayout()
        Me.gbAudioStreams.ResumeLayout(False)
        Me.gbAudioStreams.PerformLayout()
        Me.gbSubtitleStreams.ResumeLayout(False)
        Me.gbSubtitleStreams.PerformLayout()
        Me.pnlStreamEditor.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents gbVideoStreams As System.Windows.Forms.GroupBox
    Friend WithEvents gbAudioStreams As System.Windows.Forms.GroupBox
    Friend WithEvents gbSubtitleStreams As System.Windows.Forms.GroupBox
    Friend WithEvents lblVideoHeight As System.Windows.Forms.Label
    Friend WithEvents lblVideoWidth As System.Windows.Forms.Label
    Friend WithEvents txtVideoHeight As System.Windows.Forms.TextBox
    Friend WithEvents txtVideoWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblVideoCodec As System.Windows.Forms.Label
    Friend WithEvents cbVideoCodec As System.Windows.Forms.ComboBox
    Friend WithEvents lblVideoDuration As System.Windows.Forms.Label
    Friend WithEvents txtVideoDuration As System.Windows.Forms.TextBox
    Friend WithEvents rbVideoInterlaced As System.Windows.Forms.RadioButton
    Friend WithEvents rbVideoProgressive As System.Windows.Forms.RadioButton
    Friend WithEvents lblVideoAspect As System.Windows.Forms.Label
    Friend WithEvents txtVideoAspect As System.Windows.Forms.TextBox
    Friend WithEvents lblAudioCodec As System.Windows.Forms.Label
    Friend WithEvents cbAudioCodec As System.Windows.Forms.ComboBox
    Friend WithEvents lblAudioLanguage As System.Windows.Forms.Label
    Friend WithEvents cbAudioLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblAudioChannels As System.Windows.Forms.Label
    Friend WithEvents cbAudioChannels As System.Windows.Forms.ComboBox
    Friend WithEvents lblSubtitleLanguage As System.Windows.Forms.Label
    Friend WithEvents cbSubtitleLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents lblVideoLanguage As System.Windows.Forms.Label
    Friend WithEvents cbVideoLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents pnlStreamEditor As System.Windows.Forms.Panel
    Friend WithEvents lblVideoMultiViewCount As System.Windows.Forms.Label
    Friend WithEvents txtVideoMultiViewCount As System.Windows.Forms.TextBox
    Friend WithEvents lblVideoBitrate As System.Windows.Forms.Label
    Friend WithEvents txtVideoBitrate As System.Windows.Forms.TextBox
    Friend WithEvents txtAudioBitrate As System.Windows.Forms.TextBox
    Friend WithEvents lblAudioBitrate As System.Windows.Forms.Label
    Friend WithEvents lblVideoMultiViewLayout As System.Windows.Forms.Label
    Friend WithEvents cbVideoMultiViewLayout As System.Windows.Forms.ComboBox
    Friend WithEvents txtVideoStereoMode As System.Windows.Forms.TextBox
    Friend WithEvents lblVideoStereoMode As System.Windows.Forms.Label
    Friend WithEvents chkSubtitleForced As System.Windows.Forms.CheckBox
    Friend WithEvents txtSubtitlePath As System.Windows.Forms.TextBox
    Friend WithEvents txtSubtitleType As System.Windows.Forms.TextBox
    Friend WithEvents lblVideoFileSize As System.Windows.Forms.Label
    Friend WithEvents txtVideoFileSize As System.Windows.Forms.TextBox

End Class
