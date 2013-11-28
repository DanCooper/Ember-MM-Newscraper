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
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.gbVideoStreams = New System.Windows.Forms.GroupBox()
        Me.lblVideoMultiView = New System.Windows.Forms.Label()
        Me.txtVideoMultiview = New System.Windows.Forms.TextBox()
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
        Me.rbSubtitleExternal = New System.Windows.Forms.RadioButton()
        Me.rbSubtitleEmbedded = New System.Windows.Forms.RadioButton()
        Me.lblSubtitleLanguage = New System.Windows.Forms.Label()
        Me.cbSubtitleLanguage = New System.Windows.Forms.ComboBox()
        Me.pnlStreamEditor = New System.Windows.Forms.Panel()
        Me.txtEncodingSettings = New System.Windows.Forms.TextBox()
        Me.lblEncoding = New System.Windows.Forms.Label()
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
        Me.tlpButtons.Controls.Add(Me.OK_Button, 0, 0)
        Me.tlpButtons.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.tlpButtons.Location = New System.Drawing.Point(88, 372)
        Me.tlpButtons.Name = "tlpButtons"
        Me.tlpButtons.RowCount = 1
        Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Size = New System.Drawing.Size(146, 29)
        Me.tlpButtons.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'gbVideoStreams
        '
        Me.gbVideoStreams.Controls.Add(Me.lblVideoMultiView)
        Me.gbVideoStreams.Controls.Add(Me.txtVideoMultiview)
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
        Me.gbVideoStreams.Location = New System.Drawing.Point(21, 7)
        Me.gbVideoStreams.Name = "gbVideoStreams"
        Me.gbVideoStreams.Size = New System.Drawing.Size(191, 248)
        Me.gbVideoStreams.TabIndex = 0
        Me.gbVideoStreams.TabStop = False
        Me.gbVideoStreams.Text = "Video Streams"
        Me.gbVideoStreams.Visible = False
        '
        'lblVideoMultiView
        '
        Me.lblVideoMultiView.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoMultiView.Location = New System.Drawing.Point(8, 220)
        Me.lblVideoMultiView.Name = "lblVideoMultiView"
        Me.lblVideoMultiView.Size = New System.Drawing.Size(63, 22)
        Me.lblVideoMultiView.TabIndex = 16
        Me.lblVideoMultiView.Text = "MultiView"
        Me.lblVideoMultiView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoMultiview
        '
        Me.txtVideoMultiview.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoMultiview.Location = New System.Drawing.Point(76, 220)
        Me.txtVideoMultiview.Name = "txtVideoMultiview"
        Me.txtVideoMultiview.Size = New System.Drawing.Size(48, 22)
        Me.txtVideoMultiview.TabIndex = 17
        '
        'lblVideoBitrate
        '
        Me.lblVideoBitrate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoBitrate.Location = New System.Drawing.Point(16, 196)
        Me.lblVideoBitrate.Name = "lblVideoBitrate"
        Me.lblVideoBitrate.Size = New System.Drawing.Size(56, 13)
        Me.lblVideoBitrate.TabIndex = 14
        Me.lblVideoBitrate.Text = "Bitrate"
        Me.lblVideoBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoBitrate
        '
        Me.txtVideoBitrate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoBitrate.Location = New System.Drawing.Point(76, 192)
        Me.txtVideoBitrate.Name = "txtVideoBitrate"
        Me.txtVideoBitrate.Size = New System.Drawing.Size(68, 22)
        Me.txtVideoBitrate.TabIndex = 15
        '
        'lblVideoLanguage
        '
        Me.lblVideoLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoLanguage.Location = New System.Drawing.Point(4, 167)
        Me.lblVideoLanguage.Name = "lblVideoLanguage"
        Me.lblVideoLanguage.Size = New System.Drawing.Size(68, 19)
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
        Me.cbVideoLanguage.Location = New System.Drawing.Point(76, 165)
        Me.cbVideoLanguage.Name = "cbVideoLanguage"
        Me.cbVideoLanguage.Size = New System.Drawing.Size(93, 21)
        Me.cbVideoLanguage.TabIndex = 13
        '
        'lblVideoAspect
        '
        Me.lblVideoAspect.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoAspect.Location = New System.Drawing.Point(5, 116)
        Me.lblVideoAspect.Name = "lblVideoAspect"
        Me.lblVideoAspect.Size = New System.Drawing.Size(68, 19)
        Me.lblVideoAspect.TabIndex = 8
        Me.lblVideoAspect.Text = "Aspect Ratio"
        Me.lblVideoAspect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoAspect
        '
        Me.txtVideoAspect.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoAspect.Location = New System.Drawing.Point(76, 115)
        Me.txtVideoAspect.Name = "txtVideoAspect"
        Me.txtVideoAspect.Size = New System.Drawing.Size(48, 22)
        Me.txtVideoAspect.TabIndex = 9
        '
        'rbVideoInterlaced
        '
        Me.rbVideoInterlaced.AutoSize = True
        Me.rbVideoInterlaced.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbVideoInterlaced.Location = New System.Drawing.Point(111, 45)
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
        Me.rbVideoProgressive.Location = New System.Drawing.Point(25, 45)
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
        Me.lblVideoCodec.Location = New System.Drawing.Point(22, 21)
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
        Me.cbVideoCodec.Location = New System.Drawing.Point(76, 19)
        Me.cbVideoCodec.Name = "cbVideoCodec"
        Me.cbVideoCodec.Size = New System.Drawing.Size(93, 21)
        Me.cbVideoCodec.TabIndex = 1
        '
        'lblVideoDuration
        '
        Me.lblVideoDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoDuration.Location = New System.Drawing.Point(17, 143)
        Me.lblVideoDuration.Name = "lblVideoDuration"
        Me.lblVideoDuration.Size = New System.Drawing.Size(56, 13)
        Me.lblVideoDuration.TabIndex = 10
        Me.lblVideoDuration.Text = "Duration"
        Me.lblVideoDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoDuration
        '
        Me.txtVideoDuration.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoDuration.Location = New System.Drawing.Point(76, 139)
        Me.txtVideoDuration.Name = "txtVideoDuration"
        Me.txtVideoDuration.Size = New System.Drawing.Size(68, 22)
        Me.txtVideoDuration.TabIndex = 11
        '
        'lblVideoHeight
        '
        Me.lblVideoHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoHeight.Location = New System.Drawing.Point(17, 93)
        Me.lblVideoHeight.Name = "lblVideoHeight"
        Me.lblVideoHeight.Size = New System.Drawing.Size(56, 16)
        Me.lblVideoHeight.TabIndex = 6
        Me.lblVideoHeight.Text = "Height"
        Me.lblVideoHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVideoWidth
        '
        Me.lblVideoWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblVideoWidth.Location = New System.Drawing.Point(20, 69)
        Me.lblVideoWidth.Name = "lblVideoWidth"
        Me.lblVideoWidth.Size = New System.Drawing.Size(53, 19)
        Me.lblVideoWidth.TabIndex = 4
        Me.lblVideoWidth.Text = "Width"
        Me.lblVideoWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVideoHeight
        '
        Me.txtVideoHeight.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoHeight.Location = New System.Drawing.Point(76, 91)
        Me.txtVideoHeight.Name = "txtVideoHeight"
        Me.txtVideoHeight.Size = New System.Drawing.Size(48, 22)
        Me.txtVideoHeight.TabIndex = 7
        '
        'txtVideoWidth
        '
        Me.txtVideoWidth.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtVideoWidth.Location = New System.Drawing.Point(76, 68)
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
        Me.gbAudioStreams.Location = New System.Drawing.Point(21, 7)
        Me.gbAudioStreams.Name = "gbAudioStreams"
        Me.gbAudioStreams.Size = New System.Drawing.Size(191, 194)
        Me.gbAudioStreams.TabIndex = 5
        Me.gbAudioStreams.TabStop = False
        Me.gbAudioStreams.Text = "Audio Streams"
        Me.gbAudioStreams.Visible = False
        '
        'txtAudioBitrate
        '
        Me.txtAudioBitrate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtAudioBitrate.Location = New System.Drawing.Point(75, 100)
        Me.txtAudioBitrate.Name = "txtAudioBitrate"
        Me.txtAudioBitrate.Size = New System.Drawing.Size(93, 22)
        Me.txtAudioBitrate.TabIndex = 19
        '
        'lblAudioBitrate
        '
        Me.lblAudioBitrate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblAudioBitrate.Location = New System.Drawing.Point(8, 98)
        Me.lblAudioBitrate.Name = "lblAudioBitrate"
        Me.lblAudioBitrate.Size = New System.Drawing.Size(63, 22)
        Me.lblAudioBitrate.TabIndex = 18
        Me.lblAudioBitrate.Text = "Bitrate"
        Me.lblAudioBitrate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAudioChannels
        '
        Me.lblAudioChannels.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAudioChannels.Location = New System.Drawing.Point(3, 75)
        Me.lblAudioChannels.Name = "lblAudioChannels"
        Me.lblAudioChannels.Size = New System.Drawing.Size(68, 19)
        Me.lblAudioChannels.TabIndex = 4
        Me.lblAudioChannels.Text = "Channels"
        Me.lblAudioChannels.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAudioChannels
        '
        Me.cbAudioChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAudioChannels.FormattingEnabled = True
        Me.cbAudioChannels.Location = New System.Drawing.Point(75, 73)
        Me.cbAudioChannels.Name = "cbAudioChannels"
        Me.cbAudioChannels.Size = New System.Drawing.Size(93, 21)
        Me.cbAudioChannels.TabIndex = 5
        '
        'lblAudioCodec
        '
        Me.lblAudioCodec.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAudioCodec.Location = New System.Drawing.Point(3, 48)
        Me.lblAudioCodec.Name = "lblAudioCodec"
        Me.lblAudioCodec.Size = New System.Drawing.Size(68, 19)
        Me.lblAudioCodec.TabIndex = 2
        Me.lblAudioCodec.Text = "Codec"
        Me.lblAudioCodec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbAudioCodec
        '
        Me.cbAudioCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAudioCodec.FormattingEnabled = True
        Me.cbAudioCodec.Location = New System.Drawing.Point(75, 46)
        Me.cbAudioCodec.Name = "cbAudioCodec"
        Me.cbAudioCodec.Size = New System.Drawing.Size(93, 21)
        Me.cbAudioCodec.TabIndex = 3
        '
        'lblAudioLanguage
        '
        Me.lblAudioLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAudioLanguage.Location = New System.Drawing.Point(3, 21)
        Me.lblAudioLanguage.Name = "lblAudioLanguage"
        Me.lblAudioLanguage.Size = New System.Drawing.Size(68, 19)
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
        Me.cbAudioLanguage.Location = New System.Drawing.Point(75, 19)
        Me.cbAudioLanguage.Name = "cbAudioLanguage"
        Me.cbAudioLanguage.Size = New System.Drawing.Size(93, 21)
        Me.cbAudioLanguage.Sorted = True
        Me.cbAudioLanguage.TabIndex = 1
        '
        'gbSubtitleStreams
        '
        Me.gbSubtitleStreams.Controls.Add(Me.rbSubtitleExternal)
        Me.gbSubtitleStreams.Controls.Add(Me.rbSubtitleEmbedded)
        Me.gbSubtitleStreams.Controls.Add(Me.lblSubtitleLanguage)
        Me.gbSubtitleStreams.Controls.Add(Me.cbSubtitleLanguage)
        Me.gbSubtitleStreams.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSubtitleStreams.Location = New System.Drawing.Point(21, 7)
        Me.gbSubtitleStreams.Name = "gbSubtitleStreams"
        Me.gbSubtitleStreams.Size = New System.Drawing.Size(191, 194)
        Me.gbSubtitleStreams.TabIndex = 6
        Me.gbSubtitleStreams.TabStop = False
        Me.gbSubtitleStreams.Text = "Subtitle  Streams"
        Me.gbSubtitleStreams.Visible = False
        '
        'rbSubtitleExternal
        '
        Me.rbSubtitleExternal.AutoSize = True
        Me.rbSubtitleExternal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbSubtitleExternal.Location = New System.Drawing.Point(94, 46)
        Me.rbSubtitleExternal.Name = "rbSubtitleExternal"
        Me.rbSubtitleExternal.Size = New System.Drawing.Size(67, 17)
        Me.rbSubtitleExternal.TabIndex = 24
        Me.rbSubtitleExternal.TabStop = True
        Me.rbSubtitleExternal.Text = "External"
        Me.rbSubtitleExternal.UseVisualStyleBackColor = True
        '
        'rbSubtitleEmbedded
        '
        Me.rbSubtitleEmbedded.AutoSize = True
        Me.rbSubtitleEmbedded.Checked = True
        Me.rbSubtitleEmbedded.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rbSubtitleEmbedded.Location = New System.Drawing.Point(8, 46)
        Me.rbSubtitleEmbedded.Name = "rbSubtitleEmbedded"
        Me.rbSubtitleEmbedded.Size = New System.Drawing.Size(81, 17)
        Me.rbSubtitleEmbedded.TabIndex = 23
        Me.rbSubtitleEmbedded.TabStop = True
        Me.rbSubtitleEmbedded.Text = "Embedded"
        Me.rbSubtitleEmbedded.UseVisualStyleBackColor = True
        '
        'lblSubtitleLanguage
        '
        Me.lblSubtitleLanguage.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitleLanguage.Location = New System.Drawing.Point(5, 21)
        Me.lblSubtitleLanguage.Name = "lblSubtitleLanguage"
        Me.lblSubtitleLanguage.Size = New System.Drawing.Size(71, 19)
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
        Me.cbSubtitleLanguage.Location = New System.Drawing.Point(79, 21)
        Me.cbSubtitleLanguage.Name = "cbSubtitleLanguage"
        Me.cbSubtitleLanguage.Size = New System.Drawing.Size(93, 21)
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
        Me.pnlStreamEditor.Size = New System.Drawing.Size(236, 264)
        Me.pnlStreamEditor.TabIndex = 1
        '
        'txtEncodingSettings
        '
        Me.txtEncodingSettings.Location = New System.Drawing.Point(3, 289)
        Me.txtEncodingSettings.Multiline = True
        Me.txtEncodingSettings.Name = "txtEncodingSettings"
        Me.txtEncodingSettings.ReadOnly = True
        Me.txtEncodingSettings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEncodingSettings.Size = New System.Drawing.Size(236, 77)
        Me.txtEncodingSettings.TabIndex = 2
        '
        'lblEncoding
        '
        Me.lblEncoding.AutoSize = True
        Me.lblEncoding.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblEncoding.Location = New System.Drawing.Point(0, 270)
        Me.lblEncoding.Name = "lblEncoding"
        Me.lblEncoding.Size = New System.Drawing.Size(101, 13)
        Me.lblEncoding.TabIndex = 11
        Me.lblEncoding.Text = "Encoding Settings"
        Me.lblEncoding.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'dlgFIStreamEditor
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(242, 404)
        Me.Controls.Add(Me.lblEncoding)
        Me.Controls.Add(Me.txtEncodingSettings)
        Me.Controls.Add(Me.pnlStreamEditor)
        Me.Controls.Add(Me.tlpButtons)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgFIStreamEditor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
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
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
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
    Friend WithEvents rbSubtitleExternal As System.Windows.Forms.RadioButton
    Friend WithEvents rbSubtitleEmbedded As System.Windows.Forms.RadioButton
    Friend WithEvents pnlStreamEditor As System.Windows.Forms.Panel
    Friend WithEvents lblVideoMultiView As System.Windows.Forms.Label
    Friend WithEvents txtVideoMultiview As System.Windows.Forms.TextBox
    Friend WithEvents lblVideoBitrate As System.Windows.Forms.Label
    Friend WithEvents txtVideoBitrate As System.Windows.Forms.TextBox
    Friend WithEvents txtAudioBitrate As System.Windows.Forms.TextBox
    Friend WithEvents lblAudioBitrate As System.Windows.Forms.Label
    Friend WithEvents txtEncodingSettings As System.Windows.Forms.TextBox
    Friend WithEvents lblEncoding As System.Windows.Forms.Label

End Class
