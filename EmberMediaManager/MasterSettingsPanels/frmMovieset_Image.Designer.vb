<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovieset_Image
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbLanguages = New System.Windows.Forms.GroupBox()
        Me.tblLanguages = New System.Windows.Forms.TableLayoutPanel()
        Me.chkFilterGetBlankImages = New System.Windows.Forms.CheckBox()
        Me.chkFilterGetEnglishImages = New System.Windows.Forms.CheckBox()
        Me.chkFilterMediaLanguage = New System.Windows.Forms.CheckBox()
        Me.chkForceLanguage = New System.Windows.Forms.CheckBox()
        Me.cbForcedLanguage = New System.Windows.Forms.ComboBox()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.tblOptions = New System.Windows.Forms.TableLayoutPanel()
        Me.chkCacheEnabled = New System.Windows.Forms.CheckBox()
        Me.chkDisplayImageSelectDialog = New System.Windows.Forms.CheckBox()
        Me.gbImageTypes = New System.Windows.Forms.GroupBox()
        Me.tblImageTypes = New System.Windows.Forms.TableLayoutPanel()
        Me.txtPosterHeight = New System.Windows.Forms.TextBox()
        Me.chkLandscapeKeepExisting = New System.Windows.Forms.CheckBox()
        Me.txtPosterWidth = New System.Windows.Forms.TextBox()
        Me.chkClearartKeepExisting = New System.Windows.Forms.CheckBox()
        Me.chkClearlogoKeepExisting = New System.Windows.Forms.CheckBox()
        Me.chkDiscartKeepExisting = New System.Windows.Forms.CheckBox()
        Me.txtBannerHeight = New System.Windows.Forms.TextBox()
        Me.chkPosterResize = New System.Windows.Forms.CheckBox()
        Me.txtFanartHeight = New System.Windows.Forms.TextBox()
        Me.txtBannerWidth = New System.Windows.Forms.TextBox()
        Me.chkBannerResize = New System.Windows.Forms.CheckBox()
        Me.lblBanner = New System.Windows.Forms.Label()
        Me.txtFanartWidth = New System.Windows.Forms.TextBox()
        Me.lblClearart = New System.Windows.Forms.Label()
        Me.lblClearlogo = New System.Windows.Forms.Label()
        Me.chkBannerPreferredSizeOnly = New System.Windows.Forms.CheckBox()
        Me.chkPosterPreferredSizeOnly = New System.Windows.Forms.CheckBox()
        Me.chkFanartResize = New System.Windows.Forms.CheckBox()
        Me.chkFanartKeepExisting = New System.Windows.Forms.CheckBox()
        Me.chkBannerKeepExisting = New System.Windows.Forms.CheckBox()
        Me.chkPosterKeepExisting = New System.Windows.Forms.CheckBox()
        Me.chkFanartPreferredSizeOnly = New System.Windows.Forms.CheckBox()
        Me.lblDiscart = New System.Windows.Forms.Label()
        Me.cbFanartPreferredSize = New System.Windows.Forms.ComboBox()
        Me.lblFanart = New System.Windows.Forms.Label()
        Me.lblLandscape = New System.Windows.Forms.Label()
        Me.cbBannerPreferredSize = New System.Windows.Forms.ComboBox()
        Me.cbPosterPreferredSize = New System.Windows.Forms.ComboBox()
        Me.lblPoster = New System.Windows.Forms.Label()
        Me.lblPreferredSize = New System.Windows.Forms.Label()
        Me.lblPreferredSizeOnly = New System.Windows.Forms.Label()
        Me.lblKeepExisting = New System.Windows.Forms.Label()
        Me.lblResize = New System.Windows.Forms.Label()
        Me.lblMaxWidth = New System.Windows.Forms.Label()
        Me.lblMaxHeight = New System.Windows.Forms.Label()
        Me.cbClearartPreferredSize = New System.Windows.Forms.ComboBox()
        Me.cbClearlogoPreferredSize = New System.Windows.Forms.ComboBox()
        Me.cbDiscartPreferredSize = New System.Windows.Forms.ComboBox()
        Me.cbLandscapePreferredSize = New System.Windows.Forms.ComboBox()
        Me.chkClearartPreferredSizeOnly = New System.Windows.Forms.CheckBox()
        Me.chkClearlogoPreferredSizeOnly = New System.Windows.Forms.CheckBox()
        Me.chkDiscartPreferredSizeOnly = New System.Windows.Forms.CheckBox()
        Me.chkLandscapePreferredSizeOnly = New System.Windows.Forms.CheckBox()
        Me.lblKeyart = New System.Windows.Forms.Label()
        Me.cbKeyartPreferredSize = New System.Windows.Forms.ComboBox()
        Me.chkKeyartPreferredSizeOnly = New System.Windows.Forms.CheckBox()
        Me.chkKeyartKeepExisting = New System.Windows.Forms.CheckBox()
        Me.chkKeyartResize = New System.Windows.Forms.CheckBox()
        Me.txtKeyartWidth = New System.Windows.Forms.TextBox()
        Me.txtKeyartHeight = New System.Windows.Forms.TextBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbLanguages.SuspendLayout()
        Me.tblLanguages.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        Me.tblOptions.SuspendLayout()
        Me.gbImageTypes.SuspendLayout()
        Me.tblImageTypes.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(800, 450)
        Me.pnlSettings.TabIndex = 28
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 5
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbLanguages, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbOptions, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbImageTypes, 0, 1)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 3
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 450)
        Me.tblSettings.TabIndex = 16
        '
        'gbLanguages
        '
        Me.gbLanguages.AutoSize = True
        Me.gbLanguages.Controls.Add(Me.tblLanguages)
        Me.gbLanguages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbLanguages.Location = New System.Drawing.Point(307, 3)
        Me.gbLanguages.Name = "gbLanguages"
        Me.gbLanguages.Size = New System.Drawing.Size(266, 117)
        Me.gbLanguages.TabIndex = 1
        Me.gbLanguages.TabStop = False
        Me.gbLanguages.Text = "Preferred Language"
        '
        'tblLanguages
        '
        Me.tblLanguages.AutoSize = True
        Me.tblLanguages.ColumnCount = 2
        Me.tblLanguages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLanguages.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblLanguages.Controls.Add(Me.chkFilterGetBlankImages, 0, 3)
        Me.tblLanguages.Controls.Add(Me.chkFilterGetEnglishImages, 0, 2)
        Me.tblLanguages.Controls.Add(Me.chkFilterMediaLanguage, 0, 1)
        Me.tblLanguages.Controls.Add(Me.chkForceLanguage, 0, 0)
        Me.tblLanguages.Controls.Add(Me.cbForcedLanguage, 1, 0)
        Me.tblLanguages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblLanguages.Location = New System.Drawing.Point(3, 18)
        Me.tblLanguages.Name = "tblLanguages"
        Me.tblLanguages.RowCount = 5
        Me.tblLanguages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLanguages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLanguages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLanguages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLanguages.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblLanguages.Size = New System.Drawing.Size(260, 96)
        Me.tblLanguages.TabIndex = 97
        '
        'chkFilterGetBlankImages
        '
        Me.chkFilterGetBlankImages.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterGetBlankImages.AutoSize = True
        Me.tblLanguages.SetColumnSpan(Me.chkFilterGetBlankImages, 2)
        Me.chkFilterGetBlankImages.Enabled = False
        Me.chkFilterGetBlankImages.Location = New System.Drawing.Point(3, 76)
        Me.chkFilterGetBlankImages.Name = "chkFilterGetBlankImages"
        Me.chkFilterGetBlankImages.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkFilterGetBlankImages.Size = New System.Drawing.Size(159, 17)
        Me.chkFilterGetBlankImages.TabIndex = 4
        Me.chkFilterGetBlankImages.Text = "Also Get Blank Images"
        Me.chkFilterGetBlankImages.UseVisualStyleBackColor = True
        '
        'chkFilterGetEnglishImages
        '
        Me.chkFilterGetEnglishImages.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterGetEnglishImages.AutoSize = True
        Me.tblLanguages.SetColumnSpan(Me.chkFilterGetEnglishImages, 2)
        Me.chkFilterGetEnglishImages.Enabled = False
        Me.chkFilterGetEnglishImages.Location = New System.Drawing.Point(3, 53)
        Me.chkFilterGetEnglishImages.Name = "chkFilterGetEnglishImages"
        Me.chkFilterGetEnglishImages.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkFilterGetEnglishImages.Size = New System.Drawing.Size(169, 17)
        Me.chkFilterGetEnglishImages.TabIndex = 3
        Me.chkFilterGetEnglishImages.Text = "Also Get English Images"
        Me.chkFilterGetEnglishImages.UseVisualStyleBackColor = True
        '
        'chkFilterMediaLanguage
        '
        Me.chkFilterMediaLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkFilterMediaLanguage.AutoSize = True
        Me.tblLanguages.SetColumnSpan(Me.chkFilterMediaLanguage, 2)
        Me.chkFilterMediaLanguage.Location = New System.Drawing.Point(3, 30)
        Me.chkFilterMediaLanguage.Name = "chkFilterMediaLanguage"
        Me.chkFilterMediaLanguage.Size = New System.Drawing.Size(237, 17)
        Me.chkFilterMediaLanguage.TabIndex = 2
        Me.chkFilterMediaLanguage.Text = "Only Get Images for the Media Language"
        Me.chkFilterMediaLanguage.UseVisualStyleBackColor = True
        '
        'chkForceLanguage
        '
        Me.chkForceLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkForceLanguage.AutoSize = True
        Me.chkForceLanguage.Location = New System.Drawing.Point(3, 5)
        Me.chkForceLanguage.Name = "chkForceLanguage"
        Me.chkForceLanguage.Size = New System.Drawing.Size(108, 17)
        Me.chkForceLanguage.TabIndex = 15
        Me.chkForceLanguage.Text = "Force Language"
        Me.chkForceLanguage.UseVisualStyleBackColor = True
        '
        'cbForcedLanguage
        '
        Me.cbForcedLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbForcedLanguage.Enabled = False
        Me.cbForcedLanguage.FormattingEnabled = True
        Me.cbForcedLanguage.Location = New System.Drawing.Point(117, 3)
        Me.cbForcedLanguage.Name = "cbForcedLanguage"
        Me.cbForcedLanguage.Size = New System.Drawing.Size(140, 21)
        Me.cbForcedLanguage.TabIndex = 1
        '
        'gbOptions
        '
        Me.gbOptions.AutoSize = True
        Me.gbOptions.Controls.Add(Me.tblOptions)
        Me.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbOptions.Location = New System.Drawing.Point(3, 3)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(298, 117)
        Me.gbOptions.TabIndex = 0
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Images"
        '
        'tblOptions
        '
        Me.tblOptions.AutoSize = True
        Me.tblOptions.ColumnCount = 2
        Me.tblOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblOptions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblOptions.Controls.Add(Me.chkCacheEnabled, 0, 1)
        Me.tblOptions.Controls.Add(Me.chkDisplayImageSelectDialog, 0, 0)
        Me.tblOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblOptions.Location = New System.Drawing.Point(3, 18)
        Me.tblOptions.Name = "tblOptions"
        Me.tblOptions.RowCount = 3
        Me.tblOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblOptions.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblOptions.Size = New System.Drawing.Size(292, 96)
        Me.tblOptions.TabIndex = 0
        '
        'chkCacheEnabled
        '
        Me.chkCacheEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCacheEnabled.AutoSize = True
        Me.chkCacheEnabled.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkCacheEnabled.Location = New System.Drawing.Point(3, 26)
        Me.chkCacheEnabled.Name = "chkCacheEnabled"
        Me.chkCacheEnabled.Size = New System.Drawing.Size(140, 17)
        Me.chkCacheEnabled.TabIndex = 1
        Me.chkCacheEnabled.Text = "Enable Image Caching"
        Me.chkCacheEnabled.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkCacheEnabled.UseVisualStyleBackColor = True
        '
        'chkDisplayImageSelectDialog
        '
        Me.chkDisplayImageSelectDialog.AutoSize = True
        Me.chkDisplayImageSelectDialog.Location = New System.Drawing.Point(3, 3)
        Me.chkDisplayImageSelectDialog.Name = "chkDisplayImageSelectDialog"
        Me.chkDisplayImageSelectDialog.Size = New System.Drawing.Size(286, 17)
        Me.chkDisplayImageSelectDialog.TabIndex = 0
        Me.chkDisplayImageSelectDialog.Text = "Display ""Image Select"" dialog while single scraping"
        Me.chkDisplayImageSelectDialog.UseVisualStyleBackColor = True
        '
        'gbImageTypes
        '
        Me.gbImageTypes.AutoSize = True
        Me.tblSettings.SetColumnSpan(Me.gbImageTypes, 2)
        Me.gbImageTypes.Controls.Add(Me.tblImageTypes)
        Me.gbImageTypes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbImageTypes.Location = New System.Drawing.Point(3, 126)
        Me.gbImageTypes.Name = "gbImageTypes"
        Me.gbImageTypes.Size = New System.Drawing.Size(570, 261)
        Me.gbImageTypes.TabIndex = 2
        Me.gbImageTypes.TabStop = False
        Me.gbImageTypes.Text = "Image Types"
        '
        'tblImageTypes
        '
        Me.tblImageTypes.AutoSize = True
        Me.tblImageTypes.ColumnCount = 8
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblImageTypes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImageTypes.Controls.Add(Me.txtPosterHeight, 6, 8)
        Me.tblImageTypes.Controls.Add(Me.chkLandscapeKeepExisting, 3, 7)
        Me.tblImageTypes.Controls.Add(Me.txtPosterWidth, 5, 8)
        Me.tblImageTypes.Controls.Add(Me.chkClearartKeepExisting, 3, 2)
        Me.tblImageTypes.Controls.Add(Me.chkClearlogoKeepExisting, 3, 3)
        Me.tblImageTypes.Controls.Add(Me.chkDiscartKeepExisting, 3, 4)
        Me.tblImageTypes.Controls.Add(Me.txtBannerHeight, 6, 1)
        Me.tblImageTypes.Controls.Add(Me.chkPosterResize, 4, 8)
        Me.tblImageTypes.Controls.Add(Me.txtFanartHeight, 6, 5)
        Me.tblImageTypes.Controls.Add(Me.txtBannerWidth, 5, 1)
        Me.tblImageTypes.Controls.Add(Me.chkBannerResize, 4, 1)
        Me.tblImageTypes.Controls.Add(Me.lblBanner, 0, 1)
        Me.tblImageTypes.Controls.Add(Me.txtFanartWidth, 5, 5)
        Me.tblImageTypes.Controls.Add(Me.lblClearart, 0, 2)
        Me.tblImageTypes.Controls.Add(Me.lblClearlogo, 0, 3)
        Me.tblImageTypes.Controls.Add(Me.chkBannerPreferredSizeOnly, 2, 1)
        Me.tblImageTypes.Controls.Add(Me.chkPosterPreferredSizeOnly, 2, 8)
        Me.tblImageTypes.Controls.Add(Me.chkFanartResize, 4, 5)
        Me.tblImageTypes.Controls.Add(Me.chkFanartKeepExisting, 3, 5)
        Me.tblImageTypes.Controls.Add(Me.chkBannerKeepExisting, 3, 1)
        Me.tblImageTypes.Controls.Add(Me.chkPosterKeepExisting, 3, 8)
        Me.tblImageTypes.Controls.Add(Me.chkFanartPreferredSizeOnly, 2, 5)
        Me.tblImageTypes.Controls.Add(Me.lblDiscart, 0, 4)
        Me.tblImageTypes.Controls.Add(Me.cbFanartPreferredSize, 1, 5)
        Me.tblImageTypes.Controls.Add(Me.lblFanart, 0, 5)
        Me.tblImageTypes.Controls.Add(Me.lblLandscape, 0, 7)
        Me.tblImageTypes.Controls.Add(Me.cbBannerPreferredSize, 1, 1)
        Me.tblImageTypes.Controls.Add(Me.cbPosterPreferredSize, 1, 8)
        Me.tblImageTypes.Controls.Add(Me.lblPoster, 0, 8)
        Me.tblImageTypes.Controls.Add(Me.lblPreferredSize, 1, 0)
        Me.tblImageTypes.Controls.Add(Me.lblPreferredSizeOnly, 2, 0)
        Me.tblImageTypes.Controls.Add(Me.lblKeepExisting, 3, 0)
        Me.tblImageTypes.Controls.Add(Me.lblResize, 4, 0)
        Me.tblImageTypes.Controls.Add(Me.lblMaxWidth, 5, 0)
        Me.tblImageTypes.Controls.Add(Me.lblMaxHeight, 6, 0)
        Me.tblImageTypes.Controls.Add(Me.cbClearartPreferredSize, 1, 2)
        Me.tblImageTypes.Controls.Add(Me.cbClearlogoPreferredSize, 1, 3)
        Me.tblImageTypes.Controls.Add(Me.cbDiscartPreferredSize, 1, 4)
        Me.tblImageTypes.Controls.Add(Me.cbLandscapePreferredSize, 1, 7)
        Me.tblImageTypes.Controls.Add(Me.chkClearartPreferredSizeOnly, 2, 2)
        Me.tblImageTypes.Controls.Add(Me.chkClearlogoPreferredSizeOnly, 2, 3)
        Me.tblImageTypes.Controls.Add(Me.chkDiscartPreferredSizeOnly, 2, 4)
        Me.tblImageTypes.Controls.Add(Me.chkLandscapePreferredSizeOnly, 2, 7)
        Me.tblImageTypes.Controls.Add(Me.lblKeyart, 0, 6)
        Me.tblImageTypes.Controls.Add(Me.cbKeyartPreferredSize, 1, 6)
        Me.tblImageTypes.Controls.Add(Me.chkKeyartPreferredSizeOnly, 2, 6)
        Me.tblImageTypes.Controls.Add(Me.chkKeyartKeepExisting, 3, 6)
        Me.tblImageTypes.Controls.Add(Me.chkKeyartResize, 4, 6)
        Me.tblImageTypes.Controls.Add(Me.txtKeyartWidth, 5, 6)
        Me.tblImageTypes.Controls.Add(Me.txtKeyartHeight, 6, 6)
        Me.tblImageTypes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblImageTypes.Location = New System.Drawing.Point(3, 18)
        Me.tblImageTypes.Name = "tblImageTypes"
        Me.tblImageTypes.RowCount = 10
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblImageTypes.Size = New System.Drawing.Size(564, 240)
        Me.tblImageTypes.TabIndex = 0
        '
        'txtPosterHeight
        '
        Me.txtPosterHeight.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtPosterHeight.Enabled = False
        Me.txtPosterHeight.Location = New System.Drawing.Point(421, 215)
        Me.txtPosterHeight.Name = "txtPosterHeight"
        Me.txtPosterHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtPosterHeight.TabIndex = 8
        '
        'chkLandscapeKeepExisting
        '
        Me.chkLandscapeKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkLandscapeKeepExisting.AutoSize = True
        Me.chkLandscapeKeepExisting.Location = New System.Drawing.Point(243, 191)
        Me.chkLandscapeKeepExisting.Name = "chkLandscapeKeepExisting"
        Me.chkLandscapeKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkLandscapeKeepExisting.TabIndex = 4
        Me.chkLandscapeKeepExisting.UseVisualStyleBackColor = True
        '
        'txtPosterWidth
        '
        Me.txtPosterWidth.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtPosterWidth.Enabled = False
        Me.txtPosterWidth.Location = New System.Drawing.Point(350, 215)
        Me.txtPosterWidth.Name = "txtPosterWidth"
        Me.txtPosterWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtPosterWidth.TabIndex = 6
        '
        'chkClearartKeepExisting
        '
        Me.chkClearartKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkClearartKeepExisting.AutoSize = True
        Me.chkClearartKeepExisting.Location = New System.Drawing.Point(243, 54)
        Me.chkClearartKeepExisting.Name = "chkClearartKeepExisting"
        Me.chkClearartKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkClearartKeepExisting.TabIndex = 2
        Me.chkClearartKeepExisting.UseVisualStyleBackColor = True
        '
        'chkClearlogoKeepExisting
        '
        Me.chkClearlogoKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkClearlogoKeepExisting.AutoSize = True
        Me.chkClearlogoKeepExisting.Location = New System.Drawing.Point(243, 81)
        Me.chkClearlogoKeepExisting.Name = "chkClearlogoKeepExisting"
        Me.chkClearlogoKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkClearlogoKeepExisting.TabIndex = 2
        Me.chkClearlogoKeepExisting.UseVisualStyleBackColor = True
        '
        'chkDiscartKeepExisting
        '
        Me.chkDiscartKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkDiscartKeepExisting.AutoSize = True
        Me.chkDiscartKeepExisting.Location = New System.Drawing.Point(243, 108)
        Me.chkDiscartKeepExisting.Name = "chkDiscartKeepExisting"
        Me.chkDiscartKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkDiscartKeepExisting.TabIndex = 2
        Me.chkDiscartKeepExisting.UseVisualStyleBackColor = True
        '
        'txtBannerHeight
        '
        Me.txtBannerHeight.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtBannerHeight.Enabled = False
        Me.txtBannerHeight.Location = New System.Drawing.Point(421, 23)
        Me.txtBannerHeight.Name = "txtBannerHeight"
        Me.txtBannerHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtBannerHeight.TabIndex = 5
        '
        'chkPosterResize
        '
        Me.chkPosterResize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPosterResize.AutoSize = True
        Me.chkPosterResize.Location = New System.Drawing.Point(306, 219)
        Me.chkPosterResize.Name = "chkPosterResize"
        Me.chkPosterResize.Size = New System.Drawing.Size(15, 14)
        Me.chkPosterResize.TabIndex = 4
        Me.chkPosterResize.UseVisualStyleBackColor = True
        '
        'txtFanartHeight
        '
        Me.txtFanartHeight.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtFanartHeight.Enabled = False
        Me.txtFanartHeight.Location = New System.Drawing.Point(421, 132)
        Me.txtFanartHeight.Name = "txtFanartHeight"
        Me.txtFanartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtFanartHeight.TabIndex = 5
        '
        'txtBannerWidth
        '
        Me.txtBannerWidth.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtBannerWidth.Enabled = False
        Me.txtBannerWidth.Location = New System.Drawing.Point(350, 23)
        Me.txtBannerWidth.Name = "txtBannerWidth"
        Me.txtBannerWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtBannerWidth.TabIndex = 4
        '
        'chkBannerResize
        '
        Me.chkBannerResize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkBannerResize.AutoSize = True
        Me.chkBannerResize.Location = New System.Drawing.Point(306, 27)
        Me.chkBannerResize.Name = "chkBannerResize"
        Me.chkBannerResize.Size = New System.Drawing.Size(15, 14)
        Me.chkBannerResize.TabIndex = 3
        Me.chkBannerResize.UseVisualStyleBackColor = True
        '
        'lblBanner
        '
        Me.lblBanner.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblBanner.AutoSize = True
        Me.lblBanner.Location = New System.Drawing.Point(3, 27)
        Me.lblBanner.Name = "lblBanner"
        Me.lblBanner.Size = New System.Drawing.Size(43, 13)
        Me.lblBanner.TabIndex = 0
        Me.lblBanner.Text = "Banner"
        '
        'txtFanartWidth
        '
        Me.txtFanartWidth.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtFanartWidth.Enabled = False
        Me.txtFanartWidth.Location = New System.Drawing.Point(350, 132)
        Me.txtFanartWidth.Name = "txtFanartWidth"
        Me.txtFanartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtFanartWidth.TabIndex = 4
        '
        'lblClearart
        '
        Me.lblClearart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblClearart.AutoSize = True
        Me.lblClearart.Location = New System.Drawing.Point(3, 55)
        Me.lblClearart.Name = "lblClearart"
        Me.lblClearart.Size = New System.Drawing.Size(48, 13)
        Me.lblClearart.TabIndex = 0
        Me.lblClearart.Text = "ClearArt"
        '
        'lblClearlogo
        '
        Me.lblClearlogo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblClearlogo.AutoSize = True
        Me.lblClearlogo.Location = New System.Drawing.Point(3, 82)
        Me.lblClearlogo.Name = "lblClearlogo"
        Me.lblClearlogo.Size = New System.Drawing.Size(59, 13)
        Me.lblClearlogo.TabIndex = 0
        Me.lblClearlogo.Text = "ClearLogo"
        '
        'chkBannerPreferredSizeOnly
        '
        Me.chkBannerPreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkBannerPreferredSizeOnly.AutoSize = True
        Me.chkBannerPreferredSizeOnly.Location = New System.Drawing.Point(184, 27)
        Me.chkBannerPreferredSizeOnly.Name = "chkBannerPreferredSizeOnly"
        Me.chkBannerPreferredSizeOnly.Size = New System.Drawing.Size(15, 14)
        Me.chkBannerPreferredSizeOnly.TabIndex = 1
        Me.chkBannerPreferredSizeOnly.UseVisualStyleBackColor = True
        '
        'chkPosterPreferredSizeOnly
        '
        Me.chkPosterPreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPosterPreferredSizeOnly.AutoSize = True
        Me.chkPosterPreferredSizeOnly.Location = New System.Drawing.Point(184, 219)
        Me.chkPosterPreferredSizeOnly.Name = "chkPosterPreferredSizeOnly"
        Me.chkPosterPreferredSizeOnly.Size = New System.Drawing.Size(15, 14)
        Me.chkPosterPreferredSizeOnly.TabIndex = 2
        Me.chkPosterPreferredSizeOnly.UseVisualStyleBackColor = True
        '
        'chkFanartResize
        '
        Me.chkFanartResize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkFanartResize.AutoSize = True
        Me.chkFanartResize.Location = New System.Drawing.Point(306, 136)
        Me.chkFanartResize.Name = "chkFanartResize"
        Me.chkFanartResize.Size = New System.Drawing.Size(15, 14)
        Me.chkFanartResize.TabIndex = 3
        Me.chkFanartResize.UseVisualStyleBackColor = True
        '
        'chkFanartKeepExisting
        '
        Me.chkFanartKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkFanartKeepExisting.AutoSize = True
        Me.chkFanartKeepExisting.Location = New System.Drawing.Point(243, 136)
        Me.chkFanartKeepExisting.Name = "chkFanartKeepExisting"
        Me.chkFanartKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkFanartKeepExisting.TabIndex = 2
        Me.chkFanartKeepExisting.UseVisualStyleBackColor = True
        '
        'chkBannerKeepExisting
        '
        Me.chkBannerKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkBannerKeepExisting.AutoSize = True
        Me.chkBannerKeepExisting.Location = New System.Drawing.Point(243, 27)
        Me.chkBannerKeepExisting.Name = "chkBannerKeepExisting"
        Me.chkBannerKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkBannerKeepExisting.TabIndex = 2
        Me.chkBannerKeepExisting.UseVisualStyleBackColor = True
        '
        'chkPosterKeepExisting
        '
        Me.chkPosterKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPosterKeepExisting.AutoSize = True
        Me.chkPosterKeepExisting.Location = New System.Drawing.Point(243, 219)
        Me.chkPosterKeepExisting.Name = "chkPosterKeepExisting"
        Me.chkPosterKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkPosterKeepExisting.TabIndex = 3
        Me.chkPosterKeepExisting.UseVisualStyleBackColor = True
        '
        'chkFanartPreferredSizeOnly
        '
        Me.chkFanartPreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkFanartPreferredSizeOnly.AutoSize = True
        Me.chkFanartPreferredSizeOnly.Location = New System.Drawing.Point(184, 136)
        Me.chkFanartPreferredSizeOnly.Name = "chkFanartPreferredSizeOnly"
        Me.chkFanartPreferredSizeOnly.Size = New System.Drawing.Size(15, 14)
        Me.chkFanartPreferredSizeOnly.TabIndex = 1
        Me.chkFanartPreferredSizeOnly.UseVisualStyleBackColor = True
        '
        'lblDiscart
        '
        Me.lblDiscart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDiscart.AutoSize = True
        Me.lblDiscart.Location = New System.Drawing.Point(3, 109)
        Me.lblDiscart.Name = "lblDiscart"
        Me.lblDiscart.Size = New System.Drawing.Size(43, 13)
        Me.lblDiscart.TabIndex = 0
        Me.lblDiscart.Text = "DiscArt"
        '
        'cbFanartPreferredSize
        '
        Me.cbFanartPreferredSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbFanartPreferredSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFanartPreferredSize.FormattingEnabled = True
        Me.cbFanartPreferredSize.Location = New System.Drawing.Point(70, 132)
        Me.cbFanartPreferredSize.Name = "cbFanartPreferredSize"
        Me.cbFanartPreferredSize.Size = New System.Drawing.Size(100, 21)
        Me.cbFanartPreferredSize.TabIndex = 0
        '
        'lblFanart
        '
        Me.lblFanart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFanart.AutoSize = True
        Me.lblFanart.Location = New System.Drawing.Point(3, 136)
        Me.lblFanart.Name = "lblFanart"
        Me.lblFanart.Size = New System.Drawing.Size(40, 13)
        Me.lblFanart.TabIndex = 0
        Me.lblFanart.Text = "Fanart"
        '
        'lblLandscape
        '
        Me.lblLandscape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblLandscape.AutoSize = True
        Me.lblLandscape.Location = New System.Drawing.Point(3, 192)
        Me.lblLandscape.Name = "lblLandscape"
        Me.lblLandscape.Size = New System.Drawing.Size(61, 13)
        Me.lblLandscape.TabIndex = 0
        Me.lblLandscape.Text = "Landscape"
        '
        'cbBannerPreferredSize
        '
        Me.cbBannerPreferredSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbBannerPreferredSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBannerPreferredSize.FormattingEnabled = True
        Me.cbBannerPreferredSize.Location = New System.Drawing.Point(70, 23)
        Me.cbBannerPreferredSize.Name = "cbBannerPreferredSize"
        Me.cbBannerPreferredSize.Size = New System.Drawing.Size(100, 21)
        Me.cbBannerPreferredSize.TabIndex = 0
        '
        'cbPosterPreferredSize
        '
        Me.cbPosterPreferredSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbPosterPreferredSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPosterPreferredSize.FormattingEnabled = True
        Me.cbPosterPreferredSize.Location = New System.Drawing.Point(70, 215)
        Me.cbPosterPreferredSize.Name = "cbPosterPreferredSize"
        Me.cbPosterPreferredSize.Size = New System.Drawing.Size(100, 21)
        Me.cbPosterPreferredSize.TabIndex = 1
        '
        'lblPoster
        '
        Me.lblPoster.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPoster.AutoSize = True
        Me.lblPoster.Location = New System.Drawing.Point(3, 219)
        Me.lblPoster.Name = "lblPoster"
        Me.lblPoster.Size = New System.Drawing.Size(39, 13)
        Me.lblPoster.TabIndex = 0
        Me.lblPoster.Text = "Poster"
        '
        'lblPreferredSize
        '
        Me.lblPreferredSize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPreferredSize.AutoSize = True
        Me.lblPreferredSize.Location = New System.Drawing.Point(81, 3)
        Me.lblPreferredSize.Name = "lblPreferredSize"
        Me.lblPreferredSize.Size = New System.Drawing.Size(77, 13)
        Me.lblPreferredSize.TabIndex = 0
        Me.lblPreferredSize.Text = "Preferred Size"
        '
        'lblPreferredSizeOnly
        '
        Me.lblPreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblPreferredSizeOnly.AutoSize = True
        Me.lblPreferredSizeOnly.Location = New System.Drawing.Point(176, 3)
        Me.lblPreferredSizeOnly.Name = "lblPreferredSizeOnly"
        Me.lblPreferredSizeOnly.Size = New System.Drawing.Size(31, 13)
        Me.lblPreferredSizeOnly.TabIndex = 0
        Me.lblPreferredSizeOnly.Text = "Only"
        '
        'lblKeepExisting
        '
        Me.lblKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblKeepExisting.AutoSize = True
        Me.lblKeepExisting.Location = New System.Drawing.Point(213, 3)
        Me.lblKeepExisting.Name = "lblKeepExisting"
        Me.lblKeepExisting.Size = New System.Drawing.Size(75, 13)
        Me.lblKeepExisting.TabIndex = 0
        Me.lblKeepExisting.Text = "Keep Existing"
        '
        'lblResize
        '
        Me.lblResize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblResize.AutoSize = True
        Me.lblResize.Location = New System.Drawing.Point(294, 3)
        Me.lblResize.Name = "lblResize"
        Me.lblResize.Size = New System.Drawing.Size(39, 13)
        Me.lblResize.TabIndex = 0
        Me.lblResize.Text = "Resize"
        '
        'lblMaxWidth
        '
        Me.lblMaxWidth.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblMaxWidth.AutoSize = True
        Me.lblMaxWidth.Location = New System.Drawing.Point(339, 3)
        Me.lblMaxWidth.Name = "lblMaxWidth"
        Me.lblMaxWidth.Size = New System.Drawing.Size(63, 13)
        Me.lblMaxWidth.TabIndex = 0
        Me.lblMaxWidth.Text = "Max Width"
        '
        'lblMaxHeight
        '
        Me.lblMaxHeight.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblMaxHeight.AutoSize = True
        Me.lblMaxHeight.Location = New System.Drawing.Point(408, 3)
        Me.lblMaxHeight.Name = "lblMaxHeight"
        Me.lblMaxHeight.Size = New System.Drawing.Size(66, 13)
        Me.lblMaxHeight.TabIndex = 0
        Me.lblMaxHeight.Text = "Max Height"
        '
        'cbClearartPreferredSize
        '
        Me.cbClearartPreferredSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbClearartPreferredSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbClearartPreferredSize.FormattingEnabled = True
        Me.cbClearartPreferredSize.Location = New System.Drawing.Point(70, 51)
        Me.cbClearartPreferredSize.Name = "cbClearartPreferredSize"
        Me.cbClearartPreferredSize.Size = New System.Drawing.Size(100, 21)
        Me.cbClearartPreferredSize.TabIndex = 0
        '
        'cbClearlogoPreferredSize
        '
        Me.cbClearlogoPreferredSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbClearlogoPreferredSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbClearlogoPreferredSize.FormattingEnabled = True
        Me.cbClearlogoPreferredSize.Location = New System.Drawing.Point(70, 78)
        Me.cbClearlogoPreferredSize.Name = "cbClearlogoPreferredSize"
        Me.cbClearlogoPreferredSize.Size = New System.Drawing.Size(100, 21)
        Me.cbClearlogoPreferredSize.TabIndex = 0
        '
        'cbDiscartPreferredSize
        '
        Me.cbDiscartPreferredSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbDiscartPreferredSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDiscartPreferredSize.FormattingEnabled = True
        Me.cbDiscartPreferredSize.Location = New System.Drawing.Point(70, 105)
        Me.cbDiscartPreferredSize.Name = "cbDiscartPreferredSize"
        Me.cbDiscartPreferredSize.Size = New System.Drawing.Size(100, 21)
        Me.cbDiscartPreferredSize.TabIndex = 0
        '
        'cbLandscapePreferredSize
        '
        Me.cbLandscapePreferredSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbLandscapePreferredSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLandscapePreferredSize.FormattingEnabled = True
        Me.cbLandscapePreferredSize.Location = New System.Drawing.Point(70, 188)
        Me.cbLandscapePreferredSize.Name = "cbLandscapePreferredSize"
        Me.cbLandscapePreferredSize.Size = New System.Drawing.Size(100, 21)
        Me.cbLandscapePreferredSize.TabIndex = 1
        '
        'chkClearartPreferredSizeOnly
        '
        Me.chkClearartPreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkClearartPreferredSizeOnly.AutoSize = True
        Me.chkClearartPreferredSizeOnly.Location = New System.Drawing.Point(184, 54)
        Me.chkClearartPreferredSizeOnly.Name = "chkClearartPreferredSizeOnly"
        Me.chkClearartPreferredSizeOnly.Size = New System.Drawing.Size(15, 14)
        Me.chkClearartPreferredSizeOnly.TabIndex = 1
        Me.chkClearartPreferredSizeOnly.UseVisualStyleBackColor = True
        '
        'chkClearlogoPreferredSizeOnly
        '
        Me.chkClearlogoPreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkClearlogoPreferredSizeOnly.AutoSize = True
        Me.chkClearlogoPreferredSizeOnly.Location = New System.Drawing.Point(184, 81)
        Me.chkClearlogoPreferredSizeOnly.Name = "chkClearlogoPreferredSizeOnly"
        Me.chkClearlogoPreferredSizeOnly.Size = New System.Drawing.Size(15, 14)
        Me.chkClearlogoPreferredSizeOnly.TabIndex = 1
        Me.chkClearlogoPreferredSizeOnly.UseVisualStyleBackColor = True
        '
        'chkDiscartPreferredSizeOnly
        '
        Me.chkDiscartPreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkDiscartPreferredSizeOnly.AutoSize = True
        Me.chkDiscartPreferredSizeOnly.Location = New System.Drawing.Point(184, 108)
        Me.chkDiscartPreferredSizeOnly.Name = "chkDiscartPreferredSizeOnly"
        Me.chkDiscartPreferredSizeOnly.Size = New System.Drawing.Size(15, 14)
        Me.chkDiscartPreferredSizeOnly.TabIndex = 1
        Me.chkDiscartPreferredSizeOnly.UseVisualStyleBackColor = True
        '
        'chkLandscapePreferredSizeOnly
        '
        Me.chkLandscapePreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkLandscapePreferredSizeOnly.AutoSize = True
        Me.chkLandscapePreferredSizeOnly.Location = New System.Drawing.Point(184, 191)
        Me.chkLandscapePreferredSizeOnly.Name = "chkLandscapePreferredSizeOnly"
        Me.chkLandscapePreferredSizeOnly.Size = New System.Drawing.Size(15, 14)
        Me.chkLandscapePreferredSizeOnly.TabIndex = 2
        Me.chkLandscapePreferredSizeOnly.UseVisualStyleBackColor = True
        '
        'lblKeyart
        '
        Me.lblKeyart.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblKeyart.AutoSize = True
        Me.lblKeyart.Location = New System.Drawing.Point(3, 164)
        Me.lblKeyart.Name = "lblKeyart"
        Me.lblKeyart.Size = New System.Drawing.Size(39, 13)
        Me.lblKeyart.TabIndex = 0
        Me.lblKeyart.Text = "KeyArt"
        '
        'cbKeyartPreferredSize
        '
        Me.cbKeyartPreferredSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbKeyartPreferredSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbKeyartPreferredSize.FormattingEnabled = True
        Me.cbKeyartPreferredSize.Location = New System.Drawing.Point(70, 160)
        Me.cbKeyartPreferredSize.Name = "cbKeyartPreferredSize"
        Me.cbKeyartPreferredSize.Size = New System.Drawing.Size(100, 21)
        Me.cbKeyartPreferredSize.TabIndex = 1
        '
        'chkKeyartPreferredSizeOnly
        '
        Me.chkKeyartPreferredSizeOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkKeyartPreferredSizeOnly.AutoSize = True
        Me.chkKeyartPreferredSizeOnly.Location = New System.Drawing.Point(184, 164)
        Me.chkKeyartPreferredSizeOnly.Name = "chkKeyartPreferredSizeOnly"
        Me.chkKeyartPreferredSizeOnly.Size = New System.Drawing.Size(15, 14)
        Me.chkKeyartPreferredSizeOnly.TabIndex = 2
        Me.chkKeyartPreferredSizeOnly.UseVisualStyleBackColor = True
        '
        'chkKeyartKeepExisting
        '
        Me.chkKeyartKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkKeyartKeepExisting.AutoSize = True
        Me.chkKeyartKeepExisting.Location = New System.Drawing.Point(243, 164)
        Me.chkKeyartKeepExisting.Name = "chkKeyartKeepExisting"
        Me.chkKeyartKeepExisting.Size = New System.Drawing.Size(15, 14)
        Me.chkKeyartKeepExisting.TabIndex = 3
        Me.chkKeyartKeepExisting.UseVisualStyleBackColor = True
        '
        'chkKeyartResize
        '
        Me.chkKeyartResize.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkKeyartResize.AutoSize = True
        Me.chkKeyartResize.Location = New System.Drawing.Point(306, 164)
        Me.chkKeyartResize.Name = "chkKeyartResize"
        Me.chkKeyartResize.Size = New System.Drawing.Size(15, 14)
        Me.chkKeyartResize.TabIndex = 4
        Me.chkKeyartResize.UseVisualStyleBackColor = True
        '
        'txtKeyartWidth
        '
        Me.txtKeyartWidth.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtKeyartWidth.Enabled = False
        Me.txtKeyartWidth.Location = New System.Drawing.Point(350, 160)
        Me.txtKeyartWidth.Name = "txtKeyartWidth"
        Me.txtKeyartWidth.Size = New System.Drawing.Size(40, 22)
        Me.txtKeyartWidth.TabIndex = 6
        '
        'txtKeyartHeight
        '
        Me.txtKeyartHeight.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtKeyartHeight.Enabled = False
        Me.txtKeyartHeight.Location = New System.Drawing.Point(421, 160)
        Me.txtKeyartHeight.Name = "txtKeyartHeight"
        Me.txtKeyartHeight.Size = New System.Drawing.Size(40, 22)
        Me.txtKeyartHeight.TabIndex = 8
        '
        'frmMovieset_Image
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmMovieset_Image"
        Me.Text = "frmMovieset_Image"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbLanguages.ResumeLayout(False)
        Me.gbLanguages.PerformLayout()
        Me.tblLanguages.ResumeLayout(False)
        Me.tblLanguages.PerformLayout()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.tblOptions.ResumeLayout(False)
        Me.tblOptions.PerformLayout()
        Me.gbImageTypes.ResumeLayout(False)
        Me.gbImageTypes.PerformLayout()
        Me.tblImageTypes.ResumeLayout(False)
        Me.tblImageTypes.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbLanguages As GroupBox
    Friend WithEvents tblLanguages As TableLayoutPanel
    Friend WithEvents chkFilterGetBlankImages As CheckBox
    Friend WithEvents chkFilterGetEnglishImages As CheckBox
    Friend WithEvents chkForceLanguage As CheckBox
    Friend WithEvents cbForcedLanguage As ComboBox
    Friend WithEvents gbOptions As GroupBox
    Friend WithEvents tblOptions As TableLayoutPanel
    Friend WithEvents chkCacheEnabled As CheckBox
    Friend WithEvents chkDisplayImageSelectDialog As CheckBox
    Friend WithEvents gbImageTypes As GroupBox
    Friend WithEvents tblImageTypes As TableLayoutPanel
    Friend WithEvents txtPosterHeight As TextBox
    Friend WithEvents chkLandscapeKeepExisting As CheckBox
    Friend WithEvents txtPosterWidth As TextBox
    Friend WithEvents chkClearartKeepExisting As CheckBox
    Friend WithEvents chkClearlogoKeepExisting As CheckBox
    Friend WithEvents chkDiscartKeepExisting As CheckBox
    Friend WithEvents txtBannerHeight As TextBox
    Friend WithEvents chkPosterResize As CheckBox
    Friend WithEvents txtFanartHeight As TextBox
    Friend WithEvents txtBannerWidth As TextBox
    Friend WithEvents chkBannerResize As CheckBox
    Friend WithEvents lblBanner As Label
    Friend WithEvents txtFanartWidth As TextBox
    Friend WithEvents lblClearart As Label
    Friend WithEvents lblClearlogo As Label
    Friend WithEvents chkBannerPreferredSizeOnly As CheckBox
    Friend WithEvents chkPosterPreferredSizeOnly As CheckBox
    Friend WithEvents chkFanartResize As CheckBox
    Friend WithEvents chkFanartKeepExisting As CheckBox
    Friend WithEvents chkBannerKeepExisting As CheckBox
    Friend WithEvents chkPosterKeepExisting As CheckBox
    Friend WithEvents chkFanartPreferredSizeOnly As CheckBox
    Friend WithEvents lblDiscart As Label
    Friend WithEvents cbFanartPreferredSize As ComboBox
    Friend WithEvents lblFanart As Label
    Friend WithEvents lblLandscape As Label
    Friend WithEvents cbBannerPreferredSize As ComboBox
    Friend WithEvents cbPosterPreferredSize As ComboBox
    Friend WithEvents lblPoster As Label
    Friend WithEvents lblPreferredSize As Label
    Friend WithEvents lblPreferredSizeOnly As Label
    Friend WithEvents lblKeepExisting As Label
    Friend WithEvents lblResize As Label
    Friend WithEvents lblMaxWidth As Label
    Friend WithEvents lblMaxHeight As Label
    Friend WithEvents cbClearartPreferredSize As ComboBox
    Friend WithEvents cbClearlogoPreferredSize As ComboBox
    Friend WithEvents cbDiscartPreferredSize As ComboBox
    Friend WithEvents cbLandscapePreferredSize As ComboBox
    Friend WithEvents chkClearartPreferredSizeOnly As CheckBox
    Friend WithEvents chkClearlogoPreferredSizeOnly As CheckBox
    Friend WithEvents chkDiscartPreferredSizeOnly As CheckBox
    Friend WithEvents chkLandscapePreferredSizeOnly As CheckBox
    Friend WithEvents lblKeyart As Label
    Friend WithEvents cbKeyartPreferredSize As ComboBox
    Friend WithEvents chkKeyartPreferredSizeOnly As CheckBox
    Friend WithEvents chkKeyartKeepExisting As CheckBox
    Friend WithEvents chkKeyartResize As CheckBox
    Friend WithEvents txtKeyartWidth As TextBox
    Friend WithEvents txtKeyartHeight As TextBox
    Friend WithEvents chkFilterMediaLanguage As CheckBox
End Class
