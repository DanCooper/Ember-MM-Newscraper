<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovie_Information
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
        Me.gbScraperFields = New System.Windows.Forms.GroupBox()
        Me.tblScraperFields = New System.Windows.Forms.TableLayoutPanel()
        Me.lblScraperFieldsHeaderLocked = New System.Windows.Forms.Label()
        Me.lblScraperFieldsHeaderLimit = New System.Windows.Forms.Label()
        Me.lblMPAANotRatedValue = New System.Windows.Forms.Label()
        Me.cbCertificationsLimit = New System.Windows.Forms.ComboBox()
        Me.chkRatingsLocked = New System.Windows.Forms.CheckBox()
        Me.chkTitleLocked = New System.Windows.Forms.CheckBox()
        Me.chkCollectionLocked = New System.Windows.Forms.CheckBox()
        Me.chkOriginalTitleLocked = New System.Windows.Forms.CheckBox()
        Me.chkPremieredLocked = New System.Windows.Forms.CheckBox()
        Me.chkPlotLocked = New System.Windows.Forms.CheckBox()
        Me.chkOutlineLocked = New System.Windows.Forms.CheckBox()
        Me.chkTaglineLocked = New System.Windows.Forms.CheckBox()
        Me.chkTop250Locked = New System.Windows.Forms.CheckBox()
        Me.chkMPAALocked = New System.Windows.Forms.CheckBox()
        Me.chkCertificationsLocked = New System.Windows.Forms.CheckBox()
        Me.chkRuntimeLocked = New System.Windows.Forms.CheckBox()
        Me.chkStudiosLocked = New System.Windows.Forms.CheckBox()
        Me.chkTagsLocked = New System.Windows.Forms.CheckBox()
        Me.chkTrailerLinkLocked = New System.Windows.Forms.CheckBox()
        Me.chkGenresLocked = New System.Windows.Forms.CheckBox()
        Me.chkActorsLocked = New System.Windows.Forms.CheckBox()
        Me.chkCountriesLocked = New System.Windows.Forms.CheckBox()
        Me.chkDirectorsLocked = New System.Windows.Forms.CheckBox()
        Me.chkCreditsLocked = New System.Windows.Forms.CheckBox()
        Me.chkUserRatingLocked = New System.Windows.Forms.CheckBox()
        Me.btnTagsWhitelist = New System.Windows.Forms.Button()
        Me.chkTitleEnabled = New System.Windows.Forms.CheckBox()
        Me.chkTitleUseOriginalTitle = New System.Windows.Forms.CheckBox()
        Me.chkOriginalTitleEnabled = New System.Windows.Forms.CheckBox()
        Me.chkPremieredEnabled = New System.Windows.Forms.CheckBox()
        Me.chkPlotEnabled = New System.Windows.Forms.CheckBox()
        Me.chkOutlineEnabled = New System.Windows.Forms.CheckBox()
        Me.chkOutlineUsePlot = New System.Windows.Forms.CheckBox()
        Me.chkOutlineUsePlotAsFallback = New System.Windows.Forms.CheckBox()
        Me.chkTaglineEnabled = New System.Windows.Forms.CheckBox()
        Me.chkRatingsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkUserRatingEnabled = New System.Windows.Forms.CheckBox()
        Me.chkTop250Enabled = New System.Windows.Forms.CheckBox()
        Me.chkMPAAEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCertificationsForMPAA = New System.Windows.Forms.CheckBox()
        Me.chkCertificationsForMPAAFallback = New System.Windows.Forms.CheckBox()
        Me.chkCertificationsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCertificationsValueOnly = New System.Windows.Forms.CheckBox()
        Me.chkRuntimeEnabled = New System.Windows.Forms.CheckBox()
        Me.chkTagsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkTrailerLinkEnabled = New System.Windows.Forms.CheckBox()
        Me.chkGenresEnabled = New System.Windows.Forms.CheckBox()
        Me.chkActorsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkActorsWithImageOnly = New System.Windows.Forms.CheckBox()
        Me.chkDirectorsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCreditsEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCountriesEnabled = New System.Windows.Forms.CheckBox()
        Me.chkStudiosEnabled = New System.Windows.Forms.CheckBox()
        Me.chkCollectionEnabled = New System.Windows.Forms.CheckBox()
        Me.txtMPAANotRatedValue = New System.Windows.Forms.TextBox()
        Me.nudOutlineLimit = New System.Windows.Forms.NumericUpDown()
        Me.nudGenresLimit = New System.Windows.Forms.NumericUpDown()
        Me.nudActorsLimit = New System.Windows.Forms.NumericUpDown()
        Me.nudCountriesLimit = New System.Windows.Forms.NumericUpDown()
        Me.nudStudiosLimit = New System.Windows.Forms.NumericUpDown()
        Me.gbMiscellaneous = New System.Windows.Forms.GroupBox()
        Me.tblMiscellaneous = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTrailerLinkSaveKodiCompatible = New System.Windows.Forms.CheckBox()
        Me.chkCleanPlotAndOutline = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.chkClearDisabledFields = New System.Windows.Forms.CheckBox()
        Me.gbUniqueIDs = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.gbRatings = New System.Windows.Forms.GroupBox()
        Me.tblRatings = New System.Windows.Forms.TableLayoutPanel()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbCollection = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkAddAutomaticallyToCollection = New System.Windows.Forms.CheckBox()
        Me.chkSaveExtendedInformation = New System.Windows.Forms.CheckBox()
        Me.chkSaveYAMJCompatible = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbScraperFields.SuspendLayout()
        Me.tblScraperFields.SuspendLayout()
        CType(Me.nudOutlineLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudGenresLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudActorsLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCountriesLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudStudiosLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbMiscellaneous.SuspendLayout()
        Me.tblMiscellaneous.SuspendLayout()
        Me.gbUniqueIDs.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbRatings.SuspendLayout()
        Me.tblRatings.SuspendLayout()
        Me.cbCollection.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(1064, 858)
        Me.pnlSettings.TabIndex = 15
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 3
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbScraperFields, 0, 0)
        Me.tblSettings.Controls.Add(Me.gbMiscellaneous, 1, 3)
        Me.tblSettings.Controls.Add(Me.gbUniqueIDs, 1, 2)
        Me.tblSettings.Controls.Add(Me.gbRatings, 1, 1)
        Me.tblSettings.Controls.Add(Me.cbCollection, 1, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 6
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(1064, 858)
        Me.tblSettings.TabIndex = 69
        '
        'gbScraperFields
        '
        Me.gbScraperFields.AutoSize = True
        Me.gbScraperFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbScraperFields.Controls.Add(Me.tblScraperFields)
        Me.gbScraperFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraperFields.Location = New System.Drawing.Point(3, 3)
        Me.gbScraperFields.Name = "gbScraperFields"
        Me.tblSettings.SetRowSpan(Me.gbScraperFields, 5)
        Me.gbScraperFields.Size = New System.Drawing.Size(400, 702)
        Me.gbScraperFields.TabIndex = 1
        Me.gbScraperFields.TabStop = False
        Me.gbScraperFields.Text = "Scraper Fields - Global"
        '
        'tblScraperFields
        '
        Me.tblScraperFields.AutoScroll = True
        Me.tblScraperFields.AutoSize = True
        Me.tblScraperFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblScraperFields.ColumnCount = 3
        Me.tblScraperFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFields.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraperFields.Controls.Add(Me.lblScraperFieldsHeaderLocked, 1, 0)
        Me.tblScraperFields.Controls.Add(Me.lblScraperFieldsHeaderLimit, 2, 0)
        Me.tblScraperFields.Controls.Add(Me.lblMPAANotRatedValue, 0, 14)
        Me.tblScraperFields.Controls.Add(Me.cbCertificationsLimit, 2, 15)
        Me.tblScraperFields.Controls.Add(Me.chkRatingsLocked, 1, 9)
        Me.tblScraperFields.Controls.Add(Me.chkTitleLocked, 1, 1)
        Me.tblScraperFields.Controls.Add(Me.chkCollectionLocked, 1, 27)
        Me.tblScraperFields.Controls.Add(Me.chkOriginalTitleLocked, 1, 3)
        Me.tblScraperFields.Controls.Add(Me.chkPremieredLocked, 1, 4)
        Me.tblScraperFields.Controls.Add(Me.chkPlotLocked, 1, 5)
        Me.tblScraperFields.Controls.Add(Me.chkOutlineLocked, 1, 6)
        Me.tblScraperFields.Controls.Add(Me.chkTaglineLocked, 1, 8)
        Me.tblScraperFields.Controls.Add(Me.chkTop250Locked, 1, 11)
        Me.tblScraperFields.Controls.Add(Me.chkMPAALocked, 1, 12)
        Me.tblScraperFields.Controls.Add(Me.chkCertificationsLocked, 1, 15)
        Me.tblScraperFields.Controls.Add(Me.chkRuntimeLocked, 1, 17)
        Me.tblScraperFields.Controls.Add(Me.chkStudiosLocked, 1, 26)
        Me.tblScraperFields.Controls.Add(Me.chkTagsLocked, 1, 18)
        Me.tblScraperFields.Controls.Add(Me.chkTrailerLinkLocked, 1, 19)
        Me.tblScraperFields.Controls.Add(Me.chkGenresLocked, 1, 20)
        Me.tblScraperFields.Controls.Add(Me.chkActorsLocked, 1, 21)
        Me.tblScraperFields.Controls.Add(Me.chkCountriesLocked, 1, 25)
        Me.tblScraperFields.Controls.Add(Me.chkDirectorsLocked, 1, 23)
        Me.tblScraperFields.Controls.Add(Me.chkCreditsLocked, 1, 24)
        Me.tblScraperFields.Controls.Add(Me.chkUserRatingLocked, 1, 10)
        Me.tblScraperFields.Controls.Add(Me.btnTagsWhitelist, 2, 18)
        Me.tblScraperFields.Controls.Add(Me.chkTitleEnabled, 0, 1)
        Me.tblScraperFields.Controls.Add(Me.chkTitleUseOriginalTitle, 0, 2)
        Me.tblScraperFields.Controls.Add(Me.chkOriginalTitleEnabled, 0, 3)
        Me.tblScraperFields.Controls.Add(Me.chkPremieredEnabled, 0, 4)
        Me.tblScraperFields.Controls.Add(Me.chkPlotEnabled, 0, 5)
        Me.tblScraperFields.Controls.Add(Me.chkOutlineEnabled, 0, 6)
        Me.tblScraperFields.Controls.Add(Me.chkOutlineUsePlot, 0, 7)
        Me.tblScraperFields.Controls.Add(Me.chkOutlineUsePlotAsFallback, 2, 7)
        Me.tblScraperFields.Controls.Add(Me.chkTaglineEnabled, 0, 8)
        Me.tblScraperFields.Controls.Add(Me.chkRatingsEnabled, 0, 9)
        Me.tblScraperFields.Controls.Add(Me.chkUserRatingEnabled, 0, 10)
        Me.tblScraperFields.Controls.Add(Me.chkTop250Enabled, 0, 11)
        Me.tblScraperFields.Controls.Add(Me.chkMPAAEnabled, 0, 12)
        Me.tblScraperFields.Controls.Add(Me.chkCertificationsForMPAA, 0, 13)
        Me.tblScraperFields.Controls.Add(Me.chkCertificationsForMPAAFallback, 2, 13)
        Me.tblScraperFields.Controls.Add(Me.chkCertificationsEnabled, 0, 15)
        Me.tblScraperFields.Controls.Add(Me.chkCertificationsValueOnly, 0, 16)
        Me.tblScraperFields.Controls.Add(Me.chkRuntimeEnabled, 0, 17)
        Me.tblScraperFields.Controls.Add(Me.chkTagsEnabled, 0, 18)
        Me.tblScraperFields.Controls.Add(Me.chkTrailerLinkEnabled, 0, 19)
        Me.tblScraperFields.Controls.Add(Me.chkGenresEnabled, 0, 20)
        Me.tblScraperFields.Controls.Add(Me.chkActorsEnabled, 0, 21)
        Me.tblScraperFields.Controls.Add(Me.chkActorsWithImageOnly, 0, 22)
        Me.tblScraperFields.Controls.Add(Me.chkDirectorsEnabled, 0, 23)
        Me.tblScraperFields.Controls.Add(Me.chkCreditsEnabled, 0, 24)
        Me.tblScraperFields.Controls.Add(Me.chkCountriesEnabled, 0, 25)
        Me.tblScraperFields.Controls.Add(Me.chkStudiosEnabled, 0, 26)
        Me.tblScraperFields.Controls.Add(Me.chkCollectionEnabled, 0, 27)
        Me.tblScraperFields.Controls.Add(Me.txtMPAANotRatedValue, 1, 14)
        Me.tblScraperFields.Controls.Add(Me.nudOutlineLimit, 2, 6)
        Me.tblScraperFields.Controls.Add(Me.nudGenresLimit, 2, 20)
        Me.tblScraperFields.Controls.Add(Me.nudActorsLimit, 2, 21)
        Me.tblScraperFields.Controls.Add(Me.nudCountriesLimit, 2, 25)
        Me.tblScraperFields.Controls.Add(Me.nudStudiosLimit, 2, 26)
        Me.tblScraperFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraperFields.Location = New System.Drawing.Point(3, 18)
        Me.tblScraperFields.Name = "tblScraperFields"
        Me.tblScraperFields.RowCount = 29
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraperFields.Size = New System.Drawing.Size(394, 681)
        Me.tblScraperFields.TabIndex = 0
        '
        'lblScraperFieldsHeaderLocked
        '
        Me.lblScraperFieldsHeaderLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblScraperFieldsHeaderLocked.AutoSize = True
        Me.lblScraperFieldsHeaderLocked.Location = New System.Drawing.Point(195, 3)
        Me.lblScraperFieldsHeaderLocked.Name = "lblScraperFieldsHeaderLocked"
        Me.lblScraperFieldsHeaderLocked.Size = New System.Drawing.Size(43, 13)
        Me.lblScraperFieldsHeaderLocked.TabIndex = 12
        Me.lblScraperFieldsHeaderLocked.Text = "Locked"
        '
        'lblScraperFieldsHeaderLimit
        '
        Me.lblScraperFieldsHeaderLimit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblScraperFieldsHeaderLimit.AutoSize = True
        Me.lblScraperFieldsHeaderLimit.Location = New System.Drawing.Point(302, 3)
        Me.lblScraperFieldsHeaderLimit.Name = "lblScraperFieldsHeaderLimit"
        Me.lblScraperFieldsHeaderLimit.Size = New System.Drawing.Size(31, 13)
        Me.lblScraperFieldsHeaderLimit.TabIndex = 14
        Me.lblScraperFieldsHeaderLimit.Text = "Limit"
        '
        'lblMPAANotRatedValue
        '
        Me.lblMPAANotRatedValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMPAANotRatedValue.AutoSize = True
        Me.lblMPAANotRatedValue.Location = New System.Drawing.Point(3, 331)
        Me.lblMPAANotRatedValue.Name = "lblMPAANotRatedValue"
        Me.lblMPAANotRatedValue.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.lblMPAANotRatedValue.Size = New System.Drawing.Size(178, 13)
        Me.lblMPAANotRatedValue.TabIndex = 26
        Me.lblMPAANotRatedValue.Text = "Value if no rating is available:"
        '
        'cbCertificationsLimit
        '
        Me.cbCertificationsLimit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbCertificationsLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCertificationsLimit.Enabled = False
        Me.cbCertificationsLimit.Location = New System.Drawing.Point(244, 355)
        Me.cbCertificationsLimit.Name = "cbCertificationsLimit"
        Me.cbCertificationsLimit.Size = New System.Drawing.Size(130, 21)
        Me.cbCertificationsLimit.Sorted = True
        Me.cbCertificationsLimit.TabIndex = 30
        '
        'chkRatingsLocked
        '
        Me.chkRatingsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkRatingsLocked.AutoSize = True
        Me.chkRatingsLocked.Location = New System.Drawing.Point(209, 213)
        Me.chkRatingsLocked.Name = "chkRatingsLocked"
        Me.chkRatingsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkRatingsLocked.TabIndex = 17
        Me.chkRatingsLocked.UseVisualStyleBackColor = True
        '
        'chkTitleLocked
        '
        Me.chkTitleLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTitleLocked.Location = New System.Drawing.Point(209, 23)
        Me.chkTitleLocked.Name = "chkTitleLocked"
        Me.chkTitleLocked.Size = New System.Drawing.Size(14, 17)
        Me.chkTitleLocked.TabIndex = 1
        Me.chkTitleLocked.UseVisualStyleBackColor = True
        '
        'chkCollectionLocked
        '
        Me.chkCollectionLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCollectionLocked.AutoSize = True
        Me.chkCollectionLocked.Location = New System.Drawing.Point(209, 662)
        Me.chkCollectionLocked.Name = "chkCollectionLocked"
        Me.chkCollectionLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkCollectionLocked.TabIndex = 57
        Me.chkCollectionLocked.UseVisualStyleBackColor = True
        '
        'chkOriginalTitleLocked
        '
        Me.chkOriginalTitleLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkOriginalTitleLocked.AutoSize = True
        Me.chkOriginalTitleLocked.Location = New System.Drawing.Point(209, 70)
        Me.chkOriginalTitleLocked.Name = "chkOriginalTitleLocked"
        Me.chkOriginalTitleLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkOriginalTitleLocked.TabIndex = 4
        Me.chkOriginalTitleLocked.UseVisualStyleBackColor = True
        '
        'chkPremieredLocked
        '
        Me.chkPremieredLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPremieredLocked.AutoSize = True
        Me.chkPremieredLocked.Location = New System.Drawing.Point(209, 93)
        Me.chkPremieredLocked.Name = "chkPremieredLocked"
        Me.chkPremieredLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkPremieredLocked.TabIndex = 6
        Me.chkPremieredLocked.UseVisualStyleBackColor = True
        '
        'chkPlotLocked
        '
        Me.chkPlotLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkPlotLocked.AutoSize = True
        Me.chkPlotLocked.Location = New System.Drawing.Point(209, 116)
        Me.chkPlotLocked.Name = "chkPlotLocked"
        Me.chkPlotLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkPlotLocked.TabIndex = 8
        Me.chkPlotLocked.UseVisualStyleBackColor = True
        '
        'chkOutlineLocked
        '
        Me.chkOutlineLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkOutlineLocked.AutoSize = True
        Me.chkOutlineLocked.Location = New System.Drawing.Point(209, 142)
        Me.chkOutlineLocked.Name = "chkOutlineLocked"
        Me.chkOutlineLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkOutlineLocked.TabIndex = 10
        Me.chkOutlineLocked.UseVisualStyleBackColor = True
        '
        'chkTaglineLocked
        '
        Me.chkTaglineLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTaglineLocked.AutoSize = True
        Me.chkTaglineLocked.Location = New System.Drawing.Point(209, 190)
        Me.chkTaglineLocked.Name = "chkTaglineLocked"
        Me.chkTaglineLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkTaglineLocked.TabIndex = 15
        Me.chkTaglineLocked.UseVisualStyleBackColor = True
        '
        'chkTop250Locked
        '
        Me.chkTop250Locked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTop250Locked.AutoSize = True
        Me.chkTop250Locked.Location = New System.Drawing.Point(209, 259)
        Me.chkTop250Locked.Name = "chkTop250Locked"
        Me.chkTop250Locked.Size = New System.Drawing.Size(15, 14)
        Me.chkTop250Locked.TabIndex = 21
        Me.chkTop250Locked.UseVisualStyleBackColor = True
        '
        'chkMPAALocked
        '
        Me.chkMPAALocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkMPAALocked.AutoSize = True
        Me.chkMPAALocked.Location = New System.Drawing.Point(209, 282)
        Me.chkMPAALocked.Name = "chkMPAALocked"
        Me.chkMPAALocked.Size = New System.Drawing.Size(15, 14)
        Me.chkMPAALocked.TabIndex = 23
        Me.chkMPAALocked.UseVisualStyleBackColor = True
        '
        'chkCertificationsLocked
        '
        Me.chkCertificationsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCertificationsLocked.AutoSize = True
        Me.chkCertificationsLocked.Location = New System.Drawing.Point(209, 358)
        Me.chkCertificationsLocked.Name = "chkCertificationsLocked"
        Me.chkCertificationsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkCertificationsLocked.TabIndex = 29
        Me.chkCertificationsLocked.UseVisualStyleBackColor = True
        '
        'chkRuntimeLocked
        '
        Me.chkRuntimeLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkRuntimeLocked.AutoSize = True
        Me.chkRuntimeLocked.Location = New System.Drawing.Point(209, 406)
        Me.chkRuntimeLocked.Name = "chkRuntimeLocked"
        Me.chkRuntimeLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkRuntimeLocked.TabIndex = 33
        Me.chkRuntimeLocked.UseVisualStyleBackColor = True
        '
        'chkStudiosLocked
        '
        Me.chkStudiosLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkStudiosLocked.AutoSize = True
        Me.chkStudiosLocked.Location = New System.Drawing.Point(209, 637)
        Me.chkStudiosLocked.Name = "chkStudiosLocked"
        Me.chkStudiosLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkStudiosLocked.TabIndex = 54
        Me.chkStudiosLocked.UseVisualStyleBackColor = True
        '
        'chkTagsLocked
        '
        Me.chkTagsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTagsLocked.AutoSize = True
        Me.chkTagsLocked.Location = New System.Drawing.Point(209, 432)
        Me.chkTagsLocked.Name = "chkTagsLocked"
        Me.chkTagsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkTagsLocked.TabIndex = 35
        Me.chkTagsLocked.UseVisualStyleBackColor = True
        '
        'chkTrailerLinkLocked
        '
        Me.chkTrailerLinkLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTrailerLinkLocked.AutoSize = True
        Me.chkTrailerLinkLocked.Location = New System.Drawing.Point(209, 458)
        Me.chkTrailerLinkLocked.Name = "chkTrailerLinkLocked"
        Me.chkTrailerLinkLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkTrailerLinkLocked.TabIndex = 38
        Me.chkTrailerLinkLocked.UseVisualStyleBackColor = True
        '
        'chkGenresLocked
        '
        Me.chkGenresLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkGenresLocked.AutoSize = True
        Me.chkGenresLocked.Location = New System.Drawing.Point(209, 484)
        Me.chkGenresLocked.Name = "chkGenresLocked"
        Me.chkGenresLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkGenresLocked.TabIndex = 40
        Me.chkGenresLocked.UseVisualStyleBackColor = True
        '
        'chkActorsLocked
        '
        Me.chkActorsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkActorsLocked.AutoSize = True
        Me.chkActorsLocked.Location = New System.Drawing.Point(209, 512)
        Me.chkActorsLocked.Name = "chkActorsLocked"
        Me.chkActorsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkActorsLocked.TabIndex = 43
        Me.chkActorsLocked.UseVisualStyleBackColor = True
        '
        'chkCountriesLocked
        '
        Me.chkCountriesLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCountriesLocked.AutoSize = True
        Me.chkCountriesLocked.Location = New System.Drawing.Point(209, 609)
        Me.chkCountriesLocked.Name = "chkCountriesLocked"
        Me.chkCountriesLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkCountriesLocked.TabIndex = 51
        Me.chkCountriesLocked.UseVisualStyleBackColor = True
        '
        'chkDirectorsLocked
        '
        Me.chkDirectorsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkDirectorsLocked.AutoSize = True
        Me.chkDirectorsLocked.Location = New System.Drawing.Point(209, 560)
        Me.chkDirectorsLocked.Name = "chkDirectorsLocked"
        Me.chkDirectorsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkDirectorsLocked.TabIndex = 47
        Me.chkDirectorsLocked.UseVisualStyleBackColor = True
        '
        'chkCreditsLocked
        '
        Me.chkCreditsLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCreditsLocked.AutoSize = True
        Me.chkCreditsLocked.Location = New System.Drawing.Point(209, 583)
        Me.chkCreditsLocked.Name = "chkCreditsLocked"
        Me.chkCreditsLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkCreditsLocked.TabIndex = 49
        Me.chkCreditsLocked.UseVisualStyleBackColor = True
        '
        'chkUserRatingLocked
        '
        Me.chkUserRatingLocked.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkUserRatingLocked.AutoSize = True
        Me.chkUserRatingLocked.Location = New System.Drawing.Point(209, 236)
        Me.chkUserRatingLocked.Name = "chkUserRatingLocked"
        Me.chkUserRatingLocked.Size = New System.Drawing.Size(15, 14)
        Me.chkUserRatingLocked.TabIndex = 19
        Me.chkUserRatingLocked.UseVisualStyleBackColor = True
        '
        'btnTagsWhitelist
        '
        Me.btnTagsWhitelist.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTagsWhitelist.AutoSize = True
        Me.btnTagsWhitelist.Enabled = False
        Me.btnTagsWhitelist.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnTagsWhitelist.Location = New System.Drawing.Point(244, 428)
        Me.btnTagsWhitelist.Name = "btnTagsWhitelist"
        Me.btnTagsWhitelist.Size = New System.Drawing.Size(130, 23)
        Me.btnTagsWhitelist.TabIndex = 36
        Me.btnTagsWhitelist.Text = "Whitelist"
        Me.btnTagsWhitelist.UseVisualStyleBackColor = True
        '
        'chkTitleEnabled
        '
        Me.chkTitleEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleEnabled.AutoSize = True
        Me.chkTitleEnabled.Location = New System.Drawing.Point(3, 23)
        Me.chkTitleEnabled.Name = "chkTitleEnabled"
        Me.chkTitleEnabled.Size = New System.Drawing.Size(48, 17)
        Me.chkTitleEnabled.TabIndex = 0
        Me.chkTitleEnabled.Text = "Title"
        Me.chkTitleEnabled.UseVisualStyleBackColor = True
        '
        'chkTitleUseOriginalTitle
        '
        Me.chkTitleUseOriginalTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTitleUseOriginalTitle.AutoSize = True
        Me.chkTitleUseOriginalTitle.Enabled = False
        Me.chkTitleUseOriginalTitle.Location = New System.Drawing.Point(3, 46)
        Me.chkTitleUseOriginalTitle.Name = "chkTitleUseOriginalTitle"
        Me.chkTitleUseOriginalTitle.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkTitleUseOriginalTitle.Size = New System.Drawing.Size(174, 17)
        Me.chkTitleUseOriginalTitle.TabIndex = 2
        Me.chkTitleUseOriginalTitle.Text = "Use Original Title as Title"
        Me.chkTitleUseOriginalTitle.UseVisualStyleBackColor = True
        '
        'chkOriginalTitleEnabled
        '
        Me.chkOriginalTitleEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOriginalTitleEnabled.AutoSize = True
        Me.chkOriginalTitleEnabled.Location = New System.Drawing.Point(3, 69)
        Me.chkOriginalTitleEnabled.Name = "chkOriginalTitleEnabled"
        Me.chkOriginalTitleEnabled.Size = New System.Drawing.Size(93, 17)
        Me.chkOriginalTitleEnabled.TabIndex = 3
        Me.chkOriginalTitleEnabled.Text = "Original Title"
        Me.chkOriginalTitleEnabled.UseVisualStyleBackColor = True
        '
        'chkPremieredEnabled
        '
        Me.chkPremieredEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPremieredEnabled.AutoSize = True
        Me.chkPremieredEnabled.Location = New System.Drawing.Point(3, 92)
        Me.chkPremieredEnabled.Name = "chkPremieredEnabled"
        Me.chkPremieredEnabled.Size = New System.Drawing.Size(77, 17)
        Me.chkPremieredEnabled.TabIndex = 5
        Me.chkPremieredEnabled.Text = "Premiered"
        Me.chkPremieredEnabled.UseVisualStyleBackColor = True
        '
        'chkPlotEnabled
        '
        Me.chkPlotEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPlotEnabled.AutoSize = True
        Me.chkPlotEnabled.Location = New System.Drawing.Point(3, 115)
        Me.chkPlotEnabled.Name = "chkPlotEnabled"
        Me.chkPlotEnabled.Size = New System.Drawing.Size(46, 17)
        Me.chkPlotEnabled.TabIndex = 7
        Me.chkPlotEnabled.Text = "Plot"
        Me.chkPlotEnabled.UseVisualStyleBackColor = True
        '
        'chkOutlineEnabled
        '
        Me.chkOutlineEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOutlineEnabled.AutoSize = True
        Me.chkOutlineEnabled.Location = New System.Drawing.Point(3, 140)
        Me.chkOutlineEnabled.Name = "chkOutlineEnabled"
        Me.chkOutlineEnabled.Size = New System.Drawing.Size(65, 17)
        Me.chkOutlineEnabled.TabIndex = 9
        Me.chkOutlineEnabled.Text = "Outline"
        Me.chkOutlineEnabled.UseVisualStyleBackColor = True
        '
        'chkOutlineUsePlot
        '
        Me.chkOutlineUsePlot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOutlineUsePlot.AutoSize = True
        Me.chkOutlineUsePlot.Enabled = False
        Me.chkOutlineUsePlot.Location = New System.Drawing.Point(3, 166)
        Me.chkOutlineUsePlot.Name = "chkOutlineUsePlot"
        Me.chkOutlineUsePlot.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkOutlineUsePlot.Size = New System.Drawing.Size(151, 17)
        Me.chkOutlineUsePlot.TabIndex = 12
        Me.chkOutlineUsePlot.Text = "Use Plot for Outline "
        Me.chkOutlineUsePlot.UseVisualStyleBackColor = True
        '
        'chkOutlineUsePlotAsFallback
        '
        Me.chkOutlineUsePlotAsFallback.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOutlineUsePlotAsFallback.AutoSize = True
        Me.chkOutlineUsePlotAsFallback.Enabled = False
        Me.chkOutlineUsePlotAsFallback.Location = New System.Drawing.Point(244, 166)
        Me.chkOutlineUsePlotAsFallback.Name = "chkOutlineUsePlotAsFallback"
        Me.chkOutlineUsePlotAsFallback.Size = New System.Drawing.Size(147, 17)
        Me.chkOutlineUsePlotAsFallback.TabIndex = 13
        Me.chkOutlineUsePlotAsFallback.Text = "Only if Outline is empty"
        Me.chkOutlineUsePlotAsFallback.UseVisualStyleBackColor = True
        '
        'chkTaglineEnabled
        '
        Me.chkTaglineEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTaglineEnabled.AutoSize = True
        Me.chkTaglineEnabled.Location = New System.Drawing.Point(3, 189)
        Me.chkTaglineEnabled.Name = "chkTaglineEnabled"
        Me.chkTaglineEnabled.Size = New System.Drawing.Size(63, 17)
        Me.chkTaglineEnabled.TabIndex = 14
        Me.chkTaglineEnabled.Text = "Tagline"
        Me.chkTaglineEnabled.UseVisualStyleBackColor = True
        '
        'chkRatingsEnabled
        '
        Me.chkRatingsEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRatingsEnabled.AutoSize = True
        Me.chkRatingsEnabled.Location = New System.Drawing.Point(3, 212)
        Me.chkRatingsEnabled.Name = "chkRatingsEnabled"
        Me.chkRatingsEnabled.Size = New System.Drawing.Size(65, 17)
        Me.chkRatingsEnabled.TabIndex = 16
        Me.chkRatingsEnabled.Text = "Ratings"
        Me.chkRatingsEnabled.UseVisualStyleBackColor = True
        '
        'chkUserRatingEnabled
        '
        Me.chkUserRatingEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkUserRatingEnabled.AutoSize = True
        Me.chkUserRatingEnabled.Location = New System.Drawing.Point(3, 235)
        Me.chkUserRatingEnabled.Name = "chkUserRatingEnabled"
        Me.chkUserRatingEnabled.Size = New System.Drawing.Size(86, 17)
        Me.chkUserRatingEnabled.TabIndex = 18
        Me.chkUserRatingEnabled.Text = "User Rating"
        Me.chkUserRatingEnabled.UseVisualStyleBackColor = True
        '
        'chkTop250Enabled
        '
        Me.chkTop250Enabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTop250Enabled.AutoSize = True
        Me.chkTop250Enabled.Location = New System.Drawing.Point(3, 258)
        Me.chkTop250Enabled.Name = "chkTop250Enabled"
        Me.chkTop250Enabled.Size = New System.Drawing.Size(66, 17)
        Me.chkTop250Enabled.TabIndex = 20
        Me.chkTop250Enabled.Text = "Top 250"
        Me.chkTop250Enabled.UseVisualStyleBackColor = True
        '
        'chkMPAAEnabled
        '
        Me.chkMPAAEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMPAAEnabled.AutoSize = True
        Me.chkMPAAEnabled.Location = New System.Drawing.Point(3, 281)
        Me.chkMPAAEnabled.Name = "chkMPAAEnabled"
        Me.chkMPAAEnabled.Size = New System.Drawing.Size(55, 17)
        Me.chkMPAAEnabled.TabIndex = 22
        Me.chkMPAAEnabled.Text = "MPAA"
        Me.chkMPAAEnabled.UseVisualStyleBackColor = True
        '
        'chkCertificationsForMPAA
        '
        Me.chkCertificationsForMPAA.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertificationsForMPAA.AutoSize = True
        Me.chkCertificationsForMPAA.Enabled = False
        Me.chkCertificationsForMPAA.Location = New System.Drawing.Point(3, 304)
        Me.chkCertificationsForMPAA.Name = "chkCertificationsForMPAA"
        Me.chkCertificationsForMPAA.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkCertificationsForMPAA.Size = New System.Drawing.Size(186, 17)
        Me.chkCertificationsForMPAA.TabIndex = 24
        Me.chkCertificationsForMPAA.Text = "Use Certifications for MPAA"
        Me.chkCertificationsForMPAA.UseVisualStyleBackColor = True
        '
        'chkCertificationsForMPAAFallback
        '
        Me.chkCertificationsForMPAAFallback.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertificationsForMPAAFallback.AutoSize = True
        Me.chkCertificationsForMPAAFallback.Enabled = False
        Me.chkCertificationsForMPAAFallback.Location = New System.Drawing.Point(244, 304)
        Me.chkCertificationsForMPAAFallback.Name = "chkCertificationsForMPAAFallback"
        Me.chkCertificationsForMPAAFallback.Size = New System.Drawing.Size(137, 17)
        Me.chkCertificationsForMPAAFallback.TabIndex = 25
        Me.chkCertificationsForMPAAFallback.Text = "Only if MPAA is empty"
        Me.chkCertificationsForMPAAFallback.UseVisualStyleBackColor = True
        '
        'chkCertificationsEnabled
        '
        Me.chkCertificationsEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertificationsEnabled.AutoSize = True
        Me.chkCertificationsEnabled.Location = New System.Drawing.Point(3, 357)
        Me.chkCertificationsEnabled.Name = "chkCertificationsEnabled"
        Me.chkCertificationsEnabled.Size = New System.Drawing.Size(94, 17)
        Me.chkCertificationsEnabled.TabIndex = 28
        Me.chkCertificationsEnabled.Text = "Certifications"
        Me.chkCertificationsEnabled.UseVisualStyleBackColor = True
        '
        'chkCertificationsValueOnly
        '
        Me.chkCertificationsValueOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCertificationsValueOnly.AutoSize = True
        Me.chkCertificationsValueOnly.Enabled = False
        Me.chkCertificationsValueOnly.Location = New System.Drawing.Point(3, 382)
        Me.chkCertificationsValueOnly.Name = "chkCertificationsValueOnly"
        Me.chkCertificationsValueOnly.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkCertificationsValueOnly.Size = New System.Drawing.Size(124, 17)
        Me.chkCertificationsValueOnly.TabIndex = 31
        Me.chkCertificationsValueOnly.Text = "Save value only"
        Me.chkCertificationsValueOnly.UseVisualStyleBackColor = True
        '
        'chkRuntimeEnabled
        '
        Me.chkRuntimeEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkRuntimeEnabled.AutoSize = True
        Me.chkRuntimeEnabled.Location = New System.Drawing.Point(3, 405)
        Me.chkRuntimeEnabled.Name = "chkRuntimeEnabled"
        Me.chkRuntimeEnabled.Size = New System.Drawing.Size(69, 17)
        Me.chkRuntimeEnabled.TabIndex = 32
        Me.chkRuntimeEnabled.Text = "Runtime"
        Me.chkRuntimeEnabled.UseVisualStyleBackColor = True
        '
        'chkTagsEnabled
        '
        Me.chkTagsEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTagsEnabled.AutoSize = True
        Me.chkTagsEnabled.Location = New System.Drawing.Point(3, 431)
        Me.chkTagsEnabled.Name = "chkTagsEnabled"
        Me.chkTagsEnabled.Size = New System.Drawing.Size(49, 17)
        Me.chkTagsEnabled.TabIndex = 34
        Me.chkTagsEnabled.Text = "Tags"
        Me.chkTagsEnabled.UseVisualStyleBackColor = True
        '
        'chkTrailerLinkEnabled
        '
        Me.chkTrailerLinkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTrailerLinkEnabled.AutoSize = True
        Me.chkTrailerLinkEnabled.Location = New System.Drawing.Point(3, 457)
        Me.chkTrailerLinkEnabled.Name = "chkTrailerLinkEnabled"
        Me.chkTrailerLinkEnabled.Size = New System.Drawing.Size(82, 17)
        Me.chkTrailerLinkEnabled.TabIndex = 37
        Me.chkTrailerLinkEnabled.Text = "Trailer-Link"
        Me.chkTrailerLinkEnabled.UseVisualStyleBackColor = True
        '
        'chkGenresEnabled
        '
        Me.chkGenresEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkGenresEnabled.AutoSize = True
        Me.chkGenresEnabled.Location = New System.Drawing.Point(3, 482)
        Me.chkGenresEnabled.Name = "chkGenresEnabled"
        Me.chkGenresEnabled.Size = New System.Drawing.Size(62, 17)
        Me.chkGenresEnabled.TabIndex = 39
        Me.chkGenresEnabled.Text = "Genres"
        Me.chkGenresEnabled.UseVisualStyleBackColor = True
        '
        'chkActorsEnabled
        '
        Me.chkActorsEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkActorsEnabled.AutoSize = True
        Me.chkActorsEnabled.Location = New System.Drawing.Point(3, 510)
        Me.chkActorsEnabled.Name = "chkActorsEnabled"
        Me.chkActorsEnabled.Size = New System.Drawing.Size(58, 17)
        Me.chkActorsEnabled.TabIndex = 42
        Me.chkActorsEnabled.Text = "Actors"
        Me.chkActorsEnabled.UseVisualStyleBackColor = True
        '
        'chkActorsWithImageOnly
        '
        Me.chkActorsWithImageOnly.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkActorsWithImageOnly.AutoSize = True
        Me.chkActorsWithImageOnly.Enabled = False
        Me.chkActorsWithImageOnly.Location = New System.Drawing.Point(3, 536)
        Me.chkActorsWithImageOnly.Name = "chkActorsWithImageOnly"
        Me.chkActorsWithImageOnly.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkActorsWithImageOnly.Size = New System.Drawing.Size(167, 17)
        Me.chkActorsWithImageOnly.TabIndex = 45
        Me.chkActorsWithImageOnly.Text = "Only those with images"
        Me.chkActorsWithImageOnly.UseVisualStyleBackColor = True
        '
        'chkDirectorsEnabled
        '
        Me.chkDirectorsEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkDirectorsEnabled.AutoSize = True
        Me.chkDirectorsEnabled.Location = New System.Drawing.Point(3, 559)
        Me.chkDirectorsEnabled.Name = "chkDirectorsEnabled"
        Me.chkDirectorsEnabled.Size = New System.Drawing.Size(72, 17)
        Me.chkDirectorsEnabled.TabIndex = 46
        Me.chkDirectorsEnabled.Text = "Directors"
        Me.chkDirectorsEnabled.UseVisualStyleBackColor = True
        '
        'chkCreditsEnabled
        '
        Me.chkCreditsEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCreditsEnabled.AutoSize = True
        Me.chkCreditsEnabled.Location = New System.Drawing.Point(3, 582)
        Me.chkCreditsEnabled.Name = "chkCreditsEnabled"
        Me.chkCreditsEnabled.Size = New System.Drawing.Size(62, 17)
        Me.chkCreditsEnabled.TabIndex = 48
        Me.chkCreditsEnabled.Text = "Credits"
        Me.chkCreditsEnabled.UseVisualStyleBackColor = True
        '
        'chkCountriesEnabled
        '
        Me.chkCountriesEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCountriesEnabled.AutoSize = True
        Me.chkCountriesEnabled.Location = New System.Drawing.Point(3, 607)
        Me.chkCountriesEnabled.Name = "chkCountriesEnabled"
        Me.chkCountriesEnabled.Size = New System.Drawing.Size(76, 17)
        Me.chkCountriesEnabled.TabIndex = 50
        Me.chkCountriesEnabled.Text = "Countries"
        Me.chkCountriesEnabled.UseVisualStyleBackColor = True
        '
        'chkStudiosEnabled
        '
        Me.chkStudiosEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkStudiosEnabled.AutoSize = True
        Me.chkStudiosEnabled.Location = New System.Drawing.Point(3, 635)
        Me.chkStudiosEnabled.Name = "chkStudiosEnabled"
        Me.chkStudiosEnabled.Size = New System.Drawing.Size(65, 17)
        Me.chkStudiosEnabled.TabIndex = 53
        Me.chkStudiosEnabled.Text = "Studios"
        Me.chkStudiosEnabled.UseVisualStyleBackColor = True
        '
        'chkCollectionEnabled
        '
        Me.chkCollectionEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCollectionEnabled.AutoSize = True
        Me.chkCollectionEnabled.Location = New System.Drawing.Point(3, 661)
        Me.chkCollectionEnabled.Name = "chkCollectionEnabled"
        Me.chkCollectionEnabled.Size = New System.Drawing.Size(78, 17)
        Me.chkCollectionEnabled.TabIndex = 56
        Me.chkCollectionEnabled.Text = "Collection"
        Me.chkCollectionEnabled.UseVisualStyleBackColor = True
        '
        'txtMPAANotRatedValue
        '
        Me.txtMPAANotRatedValue.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.tblScraperFields.SetColumnSpan(Me.txtMPAANotRatedValue, 2)
        Me.txtMPAANotRatedValue.Location = New System.Drawing.Point(195, 327)
        Me.txtMPAANotRatedValue.Name = "txtMPAANotRatedValue"
        Me.txtMPAANotRatedValue.Size = New System.Drawing.Size(180, 22)
        Me.txtMPAANotRatedValue.TabIndex = 27
        '
        'nudOutlineLimit
        '
        Me.nudOutlineLimit.Enabled = False
        Me.nudOutlineLimit.Location = New System.Drawing.Point(244, 138)
        Me.nudOutlineLimit.Name = "nudOutlineLimit"
        Me.nudOutlineLimit.Size = New System.Drawing.Size(50, 22)
        Me.nudOutlineLimit.TabIndex = 11
        '
        'nudGenresLimit
        '
        Me.nudGenresLimit.Enabled = False
        Me.nudGenresLimit.Location = New System.Drawing.Point(244, 480)
        Me.nudGenresLimit.Name = "nudGenresLimit"
        Me.nudGenresLimit.Size = New System.Drawing.Size(50, 22)
        Me.nudGenresLimit.TabIndex = 41
        '
        'nudActorsLimit
        '
        Me.nudActorsLimit.Enabled = False
        Me.nudActorsLimit.Location = New System.Drawing.Point(244, 508)
        Me.nudActorsLimit.Name = "nudActorsLimit"
        Me.nudActorsLimit.Size = New System.Drawing.Size(50, 22)
        Me.nudActorsLimit.TabIndex = 44
        '
        'nudCountriesLimit
        '
        Me.nudCountriesLimit.Enabled = False
        Me.nudCountriesLimit.Location = New System.Drawing.Point(244, 605)
        Me.nudCountriesLimit.Name = "nudCountriesLimit"
        Me.nudCountriesLimit.Size = New System.Drawing.Size(50, 22)
        Me.nudCountriesLimit.TabIndex = 52
        '
        'nudStudiosLimit
        '
        Me.nudStudiosLimit.Enabled = False
        Me.nudStudiosLimit.Location = New System.Drawing.Point(244, 633)
        Me.nudStudiosLimit.Name = "nudStudiosLimit"
        Me.nudStudiosLimit.Size = New System.Drawing.Size(50, 22)
        Me.nudStudiosLimit.TabIndex = 55
        '
        'gbMiscellaneous
        '
        Me.gbMiscellaneous.AutoSize = True
        Me.gbMiscellaneous.Controls.Add(Me.tblMiscellaneous)
        Me.gbMiscellaneous.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMiscellaneous.Location = New System.Drawing.Point(409, 301)
        Me.gbMiscellaneous.Name = "gbMiscellaneous"
        Me.gbMiscellaneous.Size = New System.Drawing.Size(535, 113)
        Me.gbMiscellaneous.TabIndex = 0
        Me.gbMiscellaneous.TabStop = False
        Me.gbMiscellaneous.Text = "Miscellaneous"
        '
        'tblMiscellaneous
        '
        Me.tblMiscellaneous.AutoSize = True
        Me.tblMiscellaneous.ColumnCount = 1
        Me.tblMiscellaneous.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMiscellaneous.Controls.Add(Me.chkTrailerLinkSaveKodiCompatible, 0, 3)
        Me.tblMiscellaneous.Controls.Add(Me.chkCleanPlotAndOutline, 0, 1)
        Me.tblMiscellaneous.Controls.Add(Me.CheckBox5, 0, 2)
        Me.tblMiscellaneous.Controls.Add(Me.chkClearDisabledFields, 0, 0)
        Me.tblMiscellaneous.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMiscellaneous.Location = New System.Drawing.Point(3, 18)
        Me.tblMiscellaneous.Name = "tblMiscellaneous"
        Me.tblMiscellaneous.RowCount = 4
        Me.tblMiscellaneous.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMiscellaneous.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMiscellaneous.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMiscellaneous.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMiscellaneous.Size = New System.Drawing.Size(529, 92)
        Me.tblMiscellaneous.TabIndex = 78
        '
        'chkTrailerLinkSaveKodiCompatible
        '
        Me.chkTrailerLinkSaveKodiCompatible.AutoSize = True
        Me.chkTrailerLinkSaveKodiCompatible.Location = New System.Drawing.Point(3, 72)
        Me.chkTrailerLinkSaveKodiCompatible.Name = "chkTrailerLinkSaveKodiCompatible"
        Me.chkTrailerLinkSaveKodiCompatible.Size = New System.Drawing.Size(297, 17)
        Me.chkTrailerLinkSaveKodiCompatible.TabIndex = 83
        Me.chkTrailerLinkSaveKodiCompatible.Text = "Save YouTube-Trailer-Links in Kodi compatible format"
        Me.chkTrailerLinkSaveKodiCompatible.UseVisualStyleBackColor = True
        '
        'chkCleanPlotAndOutline
        '
        Me.chkCleanPlotAndOutline.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCleanPlotAndOutline.AutoSize = True
        Me.chkCleanPlotAndOutline.Location = New System.Drawing.Point(3, 26)
        Me.chkCleanPlotAndOutline.Name = "chkCleanPlotAndOutline"
        Me.chkCleanPlotAndOutline.Size = New System.Drawing.Size(121, 17)
        Me.chkCleanPlotAndOutline.TabIndex = 76
        Me.chkCleanPlotAndOutline.Text = "Clean Plot/Outline"
        Me.chkCleanPlotAndOutline.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(3, 49)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(367, 17)
        Me.CheckBox5.TabIndex = 84
        Me.CheckBox5.Text = "Create <releasedate> node in NFO with the value of <premiered>"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'chkClearDisabledFields
        '
        Me.chkClearDisabledFields.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkClearDisabledFields.AutoSize = True
        Me.chkClearDisabledFields.Location = New System.Drawing.Point(3, 3)
        Me.chkClearDisabledFields.Name = "chkClearDisabledFields"
        Me.chkClearDisabledFields.Size = New System.Drawing.Size(130, 17)
        Me.chkClearDisabledFields.TabIndex = 79
        Me.chkClearDisabledFields.Text = "Clear disabled fields"
        Me.chkClearDisabledFields.UseVisualStyleBackColor = True
        '
        'gbUniqueIDs
        '
        Me.gbUniqueIDs.AutoSize = True
        Me.gbUniqueIDs.Controls.Add(Me.TableLayoutPanel1)
        Me.gbUniqueIDs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbUniqueIDs.Location = New System.Drawing.Point(409, 177)
        Me.gbUniqueIDs.Name = "gbUniqueIDs"
        Me.gbUniqueIDs.Size = New System.Drawing.Size(535, 118)
        Me.gbUniqueIDs.TabIndex = 79
        Me.gbUniqueIDs.TabStop = False
        Me.gbUniqueIDs.Text = "Unique IDs"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBox1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBox1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBox2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CheckBox3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(529, 97)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Default ID"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(68, 3)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(120, 22)
        Me.TextBox1.TabIndex = 1
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBox1, 4)
        Me.CheckBox1.Location = New System.Drawing.Point(3, 31)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(356, 17)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "Create <id> node in NFO (ID set as default or default ID is used)"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBox2, 4)
        Me.CheckBox2.Location = New System.Drawing.Point(3, 54)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(174, 17)
        Me.CheckBox2.TabIndex = 2
        Me.CheckBox2.Text = "Create <tmdb> node in NFO"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(229, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "imdb, tmdb"
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.CheckBox3, 4)
        Me.CheckBox3.Location = New System.Drawing.Point(3, 77)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(199, 17)
        Me.CheckBox3.TabIndex = 2
        Me.CheckBox3.Text = "Create <tmdbcolid> node in NFO"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(194, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "e.g.:"
        '
        'gbRatings
        '
        Me.gbRatings.AutoSize = True
        Me.gbRatings.Controls.Add(Me.tblRatings)
        Me.gbRatings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbRatings.Location = New System.Drawing.Point(409, 99)
        Me.gbRatings.Name = "gbRatings"
        Me.gbRatings.Size = New System.Drawing.Size(535, 72)
        Me.gbRatings.TabIndex = 80
        Me.gbRatings.TabStop = False
        Me.gbRatings.Text = "Ratings"
        '
        'tblRatings
        '
        Me.tblRatings.AutoSize = True
        Me.tblRatings.ColumnCount = 5
        Me.tblRatings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRatings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRatings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRatings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRatings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblRatings.Controls.Add(Me.CheckBox4, 0, 1)
        Me.tblRatings.Controls.Add(Me.Label3, 0, 0)
        Me.tblRatings.Controls.Add(Me.Label4, 2, 0)
        Me.tblRatings.Controls.Add(Me.TextBox2, 1, 0)
        Me.tblRatings.Controls.Add(Me.Label5, 3, 0)
        Me.tblRatings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblRatings.Location = New System.Drawing.Point(3, 18)
        Me.tblRatings.Name = "tblRatings"
        Me.tblRatings.RowCount = 3
        Me.tblRatings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRatings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRatings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblRatings.Size = New System.Drawing.Size(529, 51)
        Me.tblRatings.TabIndex = 0
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.tblRatings.SetColumnSpan(Me.CheckBox4, 4)
        Me.CheckBox4.Location = New System.Drawing.Point(3, 31)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(442, 17)
        Me.CheckBox4.TabIndex = 0
        Me.CheckBox4.Text = "Create <rating> and <votes> nodes in NFO (default rating or first rating is used)" &
    ""
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Default Rating"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(217, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "e.g.:"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(91, 3)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(120, 22)
        Me.TextBox2.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(247, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(279, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "imdb, themoviedb, trakt, metacritic, tomatometerallcritics"
        '
        'cbCollection
        '
        Me.cbCollection.AutoSize = True
        Me.cbCollection.Controls.Add(Me.TableLayoutPanel2)
        Me.cbCollection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbCollection.Location = New System.Drawing.Point(409, 3)
        Me.cbCollection.Name = "cbCollection"
        Me.cbCollection.Size = New System.Drawing.Size(535, 90)
        Me.cbCollection.TabIndex = 81
        Me.cbCollection.TabStop = False
        Me.cbCollection.Text = "Collection"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.chkAddAutomaticallyToCollection, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.chkSaveExtendedInformation, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.chkSaveYAMJCompatible, 0, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(529, 69)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'chkAddAutomaticallyToCollection
        '
        Me.chkAddAutomaticallyToCollection.AutoSize = True
        Me.chkAddAutomaticallyToCollection.Location = New System.Drawing.Point(3, 3)
        Me.chkAddAutomaticallyToCollection.Name = "chkAddAutomaticallyToCollection"
        Me.chkAddAutomaticallyToCollection.Size = New System.Drawing.Size(226, 17)
        Me.chkAddAutomaticallyToCollection.TabIndex = 0
        Me.chkAddAutomaticallyToCollection.Text = "Add Movie automatically to Collections"
        Me.chkAddAutomaticallyToCollection.UseVisualStyleBackColor = True
        '
        'chkSaveExtendedInformation
        '
        Me.chkSaveExtendedInformation.AutoSize = True
        Me.chkSaveExtendedInformation.Location = New System.Drawing.Point(3, 26)
        Me.chkSaveExtendedInformation.Name = "chkSaveExtendedInformation"
        Me.chkSaveExtendedInformation.Size = New System.Drawing.Size(411, 17)
        Me.chkSaveExtendedInformation.TabIndex = 1
        Me.chkSaveExtendedInformation.Text = "Save extended Collection information to NFO (Kodi 16.0 ""Jarvis"" and newer)"
        Me.chkSaveExtendedInformation.UseVisualStyleBackColor = True
        '
        'chkSaveYAMJCompatible
        '
        Me.chkSaveYAMJCompatible.AutoSize = True
        Me.chkSaveYAMJCompatible.Location = New System.Drawing.Point(3, 49)
        Me.chkSaveYAMJCompatible.Name = "chkSaveYAMJCompatible"
        Me.chkSaveYAMJCompatible.Size = New System.Drawing.Size(201, 17)
        Me.chkSaveYAMJCompatible.TabIndex = 2
        Me.chkSaveYAMJCompatible.Text = "Save YAMJ compatible Sets to NFO"
        Me.chkSaveYAMJCompatible.UseVisualStyleBackColor = True
        '
        'frmMovie_Information
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 858)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmMovie_Information"
        Me.Text = "frmMovie_Data"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbScraperFields.ResumeLayout(False)
        Me.gbScraperFields.PerformLayout()
        Me.tblScraperFields.ResumeLayout(False)
        Me.tblScraperFields.PerformLayout()
        CType(Me.nudOutlineLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudGenresLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudActorsLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCountriesLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudStudiosLimit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbMiscellaneous.ResumeLayout(False)
        Me.gbMiscellaneous.PerformLayout()
        Me.tblMiscellaneous.ResumeLayout(False)
        Me.tblMiscellaneous.PerformLayout()
        Me.gbUniqueIDs.ResumeLayout(False)
        Me.gbUniqueIDs.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.gbRatings.ResumeLayout(False)
        Me.gbRatings.PerformLayout()
        Me.tblRatings.ResumeLayout(False)
        Me.tblRatings.PerformLayout()
        Me.cbCollection.ResumeLayout(False)
        Me.cbCollection.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbScraperFields As GroupBox
    Friend WithEvents tblScraperFields As TableLayoutPanel
    Friend WithEvents lblScraperFieldsHeaderLocked As Label
    Friend WithEvents lblScraperFieldsHeaderLimit As Label
    Friend WithEvents cbCertificationsLimit As ComboBox
    Friend WithEvents chkRatingsLocked As CheckBox
    Friend WithEvents chkTitleLocked As CheckBox
    Friend WithEvents chkTitleEnabled As CheckBox
    Friend WithEvents chkRatingsEnabled As CheckBox
    Friend WithEvents chkCollectionEnabled As CheckBox
    Friend WithEvents chkCollectionLocked As CheckBox
    Friend WithEvents chkOriginalTitleEnabled As CheckBox
    Friend WithEvents chkOriginalTitleLocked As CheckBox
    Friend WithEvents chkPremieredEnabled As CheckBox
    Friend WithEvents chkPremieredLocked As CheckBox
    Friend WithEvents chkPlotEnabled As CheckBox
    Friend WithEvents chkPlotLocked As CheckBox
    Friend WithEvents chkOutlineEnabled As CheckBox
    Friend WithEvents chkOutlineLocked As CheckBox
    Friend WithEvents chkTaglineEnabled As CheckBox
    Friend WithEvents chkTaglineLocked As CheckBox
    Friend WithEvents chkTop250Enabled As CheckBox
    Friend WithEvents chkTop250Locked As CheckBox
    Friend WithEvents chkMPAAEnabled As CheckBox
    Friend WithEvents chkMPAALocked As CheckBox
    Friend WithEvents chkCertificationsEnabled As CheckBox
    Friend WithEvents chkCertificationsLocked As CheckBox
    Friend WithEvents chkRuntimeEnabled As CheckBox
    Friend WithEvents chkRuntimeLocked As CheckBox
    Friend WithEvents chkStudiosEnabled As CheckBox
    Friend WithEvents chkStudiosLocked As CheckBox
    Friend WithEvents chkTagsEnabled As CheckBox
    Friend WithEvents chkTagsLocked As CheckBox
    Friend WithEvents chkTrailerLinkEnabled As CheckBox
    Friend WithEvents chkTrailerLinkLocked As CheckBox
    Friend WithEvents chkGenresEnabled As CheckBox
    Friend WithEvents chkGenresLocked As CheckBox
    Friend WithEvents chkActorsEnabled As CheckBox
    Friend WithEvents chkActorsLocked As CheckBox
    Friend WithEvents chkCountriesEnabled As CheckBox
    Friend WithEvents chkCountriesLocked As CheckBox
    Friend WithEvents chkDirectorsEnabled As CheckBox
    Friend WithEvents chkDirectorsLocked As CheckBox
    Friend WithEvents chkCreditsEnabled As CheckBox
    Friend WithEvents chkCreditsLocked As CheckBox
    Friend WithEvents chkUserRatingEnabled As CheckBox
    Friend WithEvents chkUserRatingLocked As CheckBox
    Friend WithEvents chkCertificationsValueOnly As CheckBox
    Friend WithEvents chkCertificationsForMPAAFallback As CheckBox
    Friend WithEvents chkCertificationsForMPAA As CheckBox
    Friend WithEvents txtMPAANotRatedValue As TextBox
    Friend WithEvents lblMPAANotRatedValue As Label
    Friend WithEvents gbMiscellaneous As GroupBox
    Friend WithEvents tblMiscellaneous As TableLayoutPanel
    Friend WithEvents chkCleanPlotAndOutline As CheckBox
    Friend WithEvents chkClearDisabledFields As CheckBox
    Friend WithEvents chkActorsWithImageOnly As CheckBox
    Friend WithEvents chkOutlineUsePlot As CheckBox
    Friend WithEvents chkTrailerLinkSaveKodiCompatible As CheckBox
    Friend WithEvents chkOutlineUsePlotAsFallback As CheckBox
    Friend WithEvents chkTitleUseOriginalTitle As CheckBox
    Friend WithEvents btnTagsWhitelist As Button
    Friend WithEvents nudOutlineLimit As NumericUpDown
    Friend WithEvents nudGenresLimit As NumericUpDown
    Friend WithEvents nudActorsLimit As NumericUpDown
    Friend WithEvents nudCountriesLimit As NumericUpDown
    Friend WithEvents nudStudiosLimit As NumericUpDown
    Friend WithEvents gbUniqueIDs As GroupBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents gbRatings As GroupBox
    Friend WithEvents tblRatings As TableLayoutPanel
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cbCollection As GroupBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents chkAddAutomaticallyToCollection As CheckBox
    Friend WithEvents chkSaveExtendedInformation As CheckBox
    Friend WithEvents chkSaveYAMJCompatible As CheckBox
    Friend WithEvents CheckBox5 As CheckBox
End Class
