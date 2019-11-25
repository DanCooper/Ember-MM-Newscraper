<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTV_GUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTV_GUI))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblTVGeneral = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTVGeneralCustomScrapeButton = New System.Windows.Forms.GroupBox()
        Me.tblTVGeneralCustomScrapeButton = New System.Windows.Forms.TableLayoutPanel()
        Me.cbTVGeneralCustomScrapeButtonScrapeType = New System.Windows.Forms.ComboBox()
        Me.cbTVGeneralCustomScrapeButtonModifierType = New System.Windows.Forms.ComboBox()
        Me.txtTVGeneralCustomScrapeButtonScrapeType = New System.Windows.Forms.Label()
        Me.txtTVGeneralCustomScrapeButtonModifierType = New System.Windows.Forms.Label()
        Me.rbTVGeneralCustomScrapeButtonEnabled = New System.Windows.Forms.RadioButton()
        Me.rbTVGeneralCustomScrapeButtonDisabled = New System.Windows.Forms.RadioButton()
        Me.gbTVGeneralMiscOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVGeneralMiscOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTVGeneralMarkNewEpisodes = New System.Windows.Forms.CheckBox()
        Me.chkTVGeneralMarkNewShows = New System.Windows.Forms.CheckBox()
        Me.chkTVGeneralClickScrape = New System.Windows.Forms.CheckBox()
        Me.chkTVGeneralClickScrapeAsk = New System.Windows.Forms.CheckBox()
        Me.gbTVGeneralMediaListOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVGeneralMediaListOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTVGeneralEpisodeListSorting = New System.Windows.Forms.GroupBox()
        Me.tblTVGeneralEpisodeListSorting = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTVGeneralEpisodeListSortingDown = New System.Windows.Forms.Button()
        Me.btnTVGeneralEpisodeListSortingUp = New System.Windows.Forms.Button()
        Me.lvTVGeneralEpisodeListSorting = New System.Windows.Forms.ListView()
        Me.colTVGeneralEpisodeListSortingDisplayIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralEpisodeListSortingColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralEpisodeListSortingLabel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralEpisodeListSortingHide = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnTVGeneralEpisodeListSortingReset = New System.Windows.Forms.Button()
        Me.gbTVGeneralSeasonListSortingOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVGeneralSeasonListSorting = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTVGeneralSeasonListSortingDown = New System.Windows.Forms.Button()
        Me.btnTVGeneralSeasonListSortingUp = New System.Windows.Forms.Button()
        Me.lvTVGeneralSeasonListSorting = New System.Windows.Forms.ListView()
        Me.colTVGeneralSeasonListSortingDisplayIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralSeasonListSortingColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralSeasonListSortingLabel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralSeasonListSortingHide = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnTVGeneralSeasonListSortingReset = New System.Windows.Forms.Button()
        Me.gbTVGeneralShowListSortingOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVGeneralShowListSorting = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTVGeneralShowListSortingDown = New System.Windows.Forms.Button()
        Me.btnTVGeneralShowListSortingUp = New System.Windows.Forms.Button()
        Me.lvTVGeneralShowListSorting = New System.Windows.Forms.ListView()
        Me.colTVGeneralShowListSortingDisplayIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralShowListSortingColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralShowListSortingLabel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVGeneralShowListSortingHide = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnTVGeneralShowListSortingReset = New System.Windows.Forms.Button()
        Me.gbTVGeneralMediaListSortTokensOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVGeneralMediaListSortTokensOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTVSortTokenReset = New System.Windows.Forms.Button()
        Me.btnTVSortTokenRemove = New System.Windows.Forms.Button()
        Me.lstTVSortTokens = New System.Windows.Forms.ListBox()
        Me.btnTVSortTokenAdd = New System.Windows.Forms.Button()
        Me.txtTVSortToken = New System.Windows.Forms.TextBox()
        Me.chkTVDisplayMissingEpisodes = New System.Windows.Forms.CheckBox()
        Me.gbTVEpisodeFilterOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVEpisodeFilterOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTVEpisodeFilterRemove = New System.Windows.Forms.Button()
        Me.btnTVEpisodeFilterDown = New System.Windows.Forms.Button()
        Me.btnTVEpisodeFilterReset = New System.Windows.Forms.Button()
        Me.btnTVEpisodeFilterUp = New System.Windows.Forms.Button()
        Me.chkTVEpisodeNoFilter = New System.Windows.Forms.CheckBox()
        Me.chkTVEpisodeProperCase = New System.Windows.Forms.CheckBox()
        Me.btnTVEpisodeFilterAdd = New System.Windows.Forms.Button()
        Me.lstTVEpisodeFilter = New System.Windows.Forms.ListBox()
        Me.txtTVEpisodeFilter = New System.Windows.Forms.TextBox()
        Me.gbTVShowFilterOpts = New System.Windows.Forms.GroupBox()
        Me.tblTVShowFilterOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnTVShowFilterRemove = New System.Windows.Forms.Button()
        Me.btnTVShowFilterDown = New System.Windows.Forms.Button()
        Me.btnTVShowFilterReset = New System.Windows.Forms.Button()
        Me.btnTVShowFilterUp = New System.Windows.Forms.Button()
        Me.chkTVShowProperCase = New System.Windows.Forms.CheckBox()
        Me.lstTVShowFilter = New System.Windows.Forms.ListBox()
        Me.btnTVShowFilterAdd = New System.Windows.Forms.Button()
        Me.txtTVShowFilter = New System.Windows.Forms.TextBox()
        Me.gbTVGeneralMainWindowOpts = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTVLanguageOverlay = New System.Windows.Forms.Label()
        Me.cbTVLanguageOverlay = New System.Windows.Forms.ComboBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblTVGeneral.SuspendLayout()
        Me.gbTVGeneralCustomScrapeButton.SuspendLayout()
        Me.tblTVGeneralCustomScrapeButton.SuspendLayout()
        Me.gbTVGeneralMiscOpts.SuspendLayout()
        Me.tblTVGeneralMiscOpts.SuspendLayout()
        Me.gbTVGeneralMediaListOpts.SuspendLayout()
        Me.tblTVGeneralMediaListOpts.SuspendLayout()
        Me.gbTVGeneralEpisodeListSorting.SuspendLayout()
        Me.tblTVGeneralEpisodeListSorting.SuspendLayout()
        Me.gbTVGeneralSeasonListSortingOpts.SuspendLayout()
        Me.tblTVGeneralSeasonListSorting.SuspendLayout()
        Me.gbTVGeneralShowListSortingOpts.SuspendLayout()
        Me.tblTVGeneralShowListSorting.SuspendLayout()
        Me.gbTVGeneralMediaListSortTokensOpts.SuspendLayout()
        Me.tblTVGeneralMediaListSortTokensOpts.SuspendLayout()
        Me.gbTVEpisodeFilterOpts.SuspendLayout()
        Me.tblTVEpisodeFilterOpts.SuspendLayout()
        Me.gbTVShowFilterOpts.SuspendLayout()
        Me.tblTVShowFilterOpts.SuspendLayout()
        Me.gbTVGeneralMainWindowOpts.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblTVGeneral)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(864, 611)
        Me.pnlSettings.TabIndex = 21
        Me.pnlSettings.Visible = False
        '
        'tblTVGeneral
        '
        Me.tblTVGeneral.AutoScroll = True
        Me.tblTVGeneral.AutoSize = True
        Me.tblTVGeneral.ColumnCount = 4
        Me.tblTVGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneral.Controls.Add(Me.gbTVGeneralCustomScrapeButton, 2, 3)
        Me.tblTVGeneral.Controls.Add(Me.gbTVGeneralMiscOpts, 0, 0)
        Me.tblTVGeneral.Controls.Add(Me.gbTVGeneralMediaListOpts, 0, 1)
        Me.tblTVGeneral.Controls.Add(Me.gbTVEpisodeFilterOpts, 2, 2)
        Me.tblTVGeneral.Controls.Add(Me.gbTVShowFilterOpts, 2, 1)
        Me.tblTVGeneral.Controls.Add(Me.gbTVGeneralMainWindowOpts, 2, 0)
        Me.tblTVGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneral.Location = New System.Drawing.Point(0, 0)
        Me.tblTVGeneral.Name = "tblTVGeneral"
        Me.tblTVGeneral.RowCount = 5
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneral.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneral.Size = New System.Drawing.Size(864, 611)
        Me.tblTVGeneral.TabIndex = 5
        '
        'gbTVGeneralCustomScrapeButton
        '
        Me.gbTVGeneralCustomScrapeButton.AutoSize = True
        Me.gbTVGeneralCustomScrapeButton.Controls.Add(Me.tblTVGeneralCustomScrapeButton)
        Me.gbTVGeneralCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVGeneralCustomScrapeButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTVGeneralCustomScrapeButton.Location = New System.Drawing.Point(451, 478)
        Me.gbTVGeneralCustomScrapeButton.Name = "gbTVGeneralCustomScrapeButton"
        Me.gbTVGeneralCustomScrapeButton.Size = New System.Drawing.Size(497, 105)
        Me.gbTVGeneralCustomScrapeButton.TabIndex = 13
        Me.gbTVGeneralCustomScrapeButton.TabStop = False
        Me.gbTVGeneralCustomScrapeButton.Text = "Scrape Button"
        '
        'tblTVGeneralCustomScrapeButton
        '
        Me.tblTVGeneralCustomScrapeButton.AutoSize = True
        Me.tblTVGeneralCustomScrapeButton.ColumnCount = 2
        Me.tblTVGeneralCustomScrapeButton.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralCustomScrapeButton.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblTVGeneralCustomScrapeButton.Controls.Add(Me.cbTVGeneralCustomScrapeButtonScrapeType, 1, 1)
        Me.tblTVGeneralCustomScrapeButton.Controls.Add(Me.cbTVGeneralCustomScrapeButtonModifierType, 1, 2)
        Me.tblTVGeneralCustomScrapeButton.Controls.Add(Me.txtTVGeneralCustomScrapeButtonScrapeType, 0, 1)
        Me.tblTVGeneralCustomScrapeButton.Controls.Add(Me.txtTVGeneralCustomScrapeButtonModifierType, 0, 2)
        Me.tblTVGeneralCustomScrapeButton.Controls.Add(Me.rbTVGeneralCustomScrapeButtonEnabled, 1, 0)
        Me.tblTVGeneralCustomScrapeButton.Controls.Add(Me.rbTVGeneralCustomScrapeButtonDisabled, 0, 0)
        Me.tblTVGeneralCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneralCustomScrapeButton.Location = New System.Drawing.Point(3, 18)
        Me.tblTVGeneralCustomScrapeButton.Name = "tblTVGeneralCustomScrapeButton"
        Me.tblTVGeneralCustomScrapeButton.RowCount = 4
        Me.tblTVGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralCustomScrapeButton.Size = New System.Drawing.Size(491, 84)
        Me.tblTVGeneralCustomScrapeButton.TabIndex = 0
        '
        'cbTVGeneralCustomScrapeButtonScrapeType
        '
        Me.cbTVGeneralCustomScrapeButtonScrapeType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbTVGeneralCustomScrapeButtonScrapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVGeneralCustomScrapeButtonScrapeType.Enabled = False
        Me.cbTVGeneralCustomScrapeButtonScrapeType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTVGeneralCustomScrapeButtonScrapeType.FormattingEnabled = True
        Me.cbTVGeneralCustomScrapeButtonScrapeType.Location = New System.Drawing.Point(159, 26)
        Me.cbTVGeneralCustomScrapeButtonScrapeType.Name = "cbTVGeneralCustomScrapeButtonScrapeType"
        Me.cbTVGeneralCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(329, 21)
        Me.cbTVGeneralCustomScrapeButtonScrapeType.TabIndex = 1
        '
        'cbTVGeneralCustomScrapeButtonModifierType
        '
        Me.cbTVGeneralCustomScrapeButtonModifierType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbTVGeneralCustomScrapeButtonModifierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVGeneralCustomScrapeButtonModifierType.Enabled = False
        Me.cbTVGeneralCustomScrapeButtonModifierType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTVGeneralCustomScrapeButtonModifierType.FormattingEnabled = True
        Me.cbTVGeneralCustomScrapeButtonModifierType.Location = New System.Drawing.Point(159, 53)
        Me.cbTVGeneralCustomScrapeButtonModifierType.Name = "cbTVGeneralCustomScrapeButtonModifierType"
        Me.cbTVGeneralCustomScrapeButtonModifierType.Size = New System.Drawing.Size(329, 21)
        Me.cbTVGeneralCustomScrapeButtonModifierType.TabIndex = 2
        '
        'txtTVGeneralCustomScrapeButtonScrapeType
        '
        Me.txtTVGeneralCustomScrapeButtonScrapeType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTVGeneralCustomScrapeButtonScrapeType.AutoSize = True
        Me.txtTVGeneralCustomScrapeButtonScrapeType.Enabled = False
        Me.txtTVGeneralCustomScrapeButtonScrapeType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVGeneralCustomScrapeButtonScrapeType.Location = New System.Drawing.Point(3, 30)
        Me.txtTVGeneralCustomScrapeButtonScrapeType.Name = "txtTVGeneralCustomScrapeButtonScrapeType"
        Me.txtTVGeneralCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(66, 13)
        Me.txtTVGeneralCustomScrapeButtonScrapeType.TabIndex = 3
        Me.txtTVGeneralCustomScrapeButtonScrapeType.Text = "Scrape Type"
        '
        'txtTVGeneralCustomScrapeButtonModifierType
        '
        Me.txtTVGeneralCustomScrapeButtonModifierType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTVGeneralCustomScrapeButtonModifierType.AutoSize = True
        Me.txtTVGeneralCustomScrapeButtonModifierType.Enabled = False
        Me.txtTVGeneralCustomScrapeButtonModifierType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVGeneralCustomScrapeButtonModifierType.Location = New System.Drawing.Point(3, 57)
        Me.txtTVGeneralCustomScrapeButtonModifierType.Name = "txtTVGeneralCustomScrapeButtonModifierType"
        Me.txtTVGeneralCustomScrapeButtonModifierType.Size = New System.Drawing.Size(76, 13)
        Me.txtTVGeneralCustomScrapeButtonModifierType.TabIndex = 4
        Me.txtTVGeneralCustomScrapeButtonModifierType.Text = "Modifier Type"
        '
        'rbTVGeneralCustomScrapeButtonEnabled
        '
        Me.rbTVGeneralCustomScrapeButtonEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbTVGeneralCustomScrapeButtonEnabled.AutoSize = True
        Me.rbTVGeneralCustomScrapeButtonEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTVGeneralCustomScrapeButtonEnabled.Location = New System.Drawing.Point(159, 3)
        Me.rbTVGeneralCustomScrapeButtonEnabled.Name = "rbTVGeneralCustomScrapeButtonEnabled"
        Me.rbTVGeneralCustomScrapeButtonEnabled.Size = New System.Drawing.Size(150, 17)
        Me.rbTVGeneralCustomScrapeButtonEnabled.TabIndex = 5
        Me.rbTVGeneralCustomScrapeButtonEnabled.TabStop = True
        Me.rbTVGeneralCustomScrapeButtonEnabled.Text = "Custom Scrape Function"
        Me.rbTVGeneralCustomScrapeButtonEnabled.UseVisualStyleBackColor = True
        '
        'rbTVGeneralCustomScrapeButtonDisabled
        '
        Me.rbTVGeneralCustomScrapeButtonDisabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbTVGeneralCustomScrapeButtonDisabled.AutoSize = True
        Me.rbTVGeneralCustomScrapeButtonDisabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTVGeneralCustomScrapeButtonDisabled.Location = New System.Drawing.Point(3, 3)
        Me.rbTVGeneralCustomScrapeButtonDisabled.Name = "rbTVGeneralCustomScrapeButtonDisabled"
        Me.rbTVGeneralCustomScrapeButtonDisabled.Size = New System.Drawing.Size(150, 17)
        Me.rbTVGeneralCustomScrapeButtonDisabled.TabIndex = 6
        Me.rbTVGeneralCustomScrapeButtonDisabled.TabStop = True
        Me.rbTVGeneralCustomScrapeButtonDisabled.Text = "Open Drop Down Menu"
        Me.rbTVGeneralCustomScrapeButtonDisabled.UseVisualStyleBackColor = True
        '
        'gbTVGeneralMiscOpts
        '
        Me.gbTVGeneralMiscOpts.AutoSize = True
        Me.gbTVGeneralMiscOpts.Controls.Add(Me.tblTVGeneralMiscOpts)
        Me.gbTVGeneralMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVGeneralMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVGeneralMiscOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbTVGeneralMiscOpts.Name = "gbTVGeneralMiscOpts"
        Me.gbTVGeneralMiscOpts.Size = New System.Drawing.Size(297, 74)
        Me.gbTVGeneralMiscOpts.TabIndex = 0
        Me.gbTVGeneralMiscOpts.TabStop = False
        Me.gbTVGeneralMiscOpts.Text = "Miscellaneous"
        '
        'tblTVGeneralMiscOpts
        '
        Me.tblTVGeneralMiscOpts.AutoSize = True
        Me.tblTVGeneralMiscOpts.ColumnCount = 3
        Me.tblTVGeneralMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMiscOpts.Controls.Add(Me.chkTVGeneralMarkNewEpisodes, 0, 1)
        Me.tblTVGeneralMiscOpts.Controls.Add(Me.chkTVGeneralMarkNewShows, 0, 0)
        Me.tblTVGeneralMiscOpts.Controls.Add(Me.chkTVGeneralClickScrape, 1, 0)
        Me.tblTVGeneralMiscOpts.Controls.Add(Me.chkTVGeneralClickScrapeAsk, 1, 1)
        Me.tblTVGeneralMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneralMiscOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVGeneralMiscOpts.Name = "tblTVGeneralMiscOpts"
        Me.tblTVGeneralMiscOpts.RowCount = 3
        Me.tblTVGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMiscOpts.Size = New System.Drawing.Size(291, 53)
        Me.tblTVGeneralMiscOpts.TabIndex = 74
        '
        'chkTVGeneralMarkNewEpisodes
        '
        Me.chkTVGeneralMarkNewEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVGeneralMarkNewEpisodes.AutoSize = True
        Me.chkTVGeneralMarkNewEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVGeneralMarkNewEpisodes.Location = New System.Drawing.Point(3, 26)
        Me.chkTVGeneralMarkNewEpisodes.Name = "chkTVGeneralMarkNewEpisodes"
        Me.chkTVGeneralMarkNewEpisodes.Size = New System.Drawing.Size(127, 17)
        Me.chkTVGeneralMarkNewEpisodes.TabIndex = 4
        Me.chkTVGeneralMarkNewEpisodes.Text = "Mark New Episodes"
        Me.chkTVGeneralMarkNewEpisodes.UseVisualStyleBackColor = True
        '
        'chkTVGeneralMarkNewShows
        '
        Me.chkTVGeneralMarkNewShows.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVGeneralMarkNewShows.AutoSize = True
        Me.chkTVGeneralMarkNewShows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVGeneralMarkNewShows.Location = New System.Drawing.Point(3, 3)
        Me.chkTVGeneralMarkNewShows.Name = "chkTVGeneralMarkNewShows"
        Me.chkTVGeneralMarkNewShows.Size = New System.Drawing.Size(115, 17)
        Me.chkTVGeneralMarkNewShows.TabIndex = 3
        Me.chkTVGeneralMarkNewShows.Text = "Mark New Shows"
        Me.chkTVGeneralMarkNewShows.UseVisualStyleBackColor = True
        '
        'chkTVGeneralClickScrape
        '
        Me.chkTVGeneralClickScrape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVGeneralClickScrape.AutoSize = True
        Me.chkTVGeneralClickScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkTVGeneralClickScrape.Location = New System.Drawing.Point(136, 3)
        Me.chkTVGeneralClickScrape.Name = "chkTVGeneralClickScrape"
        Me.chkTVGeneralClickScrape.Size = New System.Drawing.Size(126, 17)
        Me.chkTVGeneralClickScrape.TabIndex = 66
        Me.chkTVGeneralClickScrape.Text = "Enable Click-Scrape"
        Me.chkTVGeneralClickScrape.UseVisualStyleBackColor = True
        '
        'chkTVGeneralClickScrapeAsk
        '
        Me.chkTVGeneralClickScrapeAsk.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVGeneralClickScrapeAsk.AutoSize = True
        Me.chkTVGeneralClickScrapeAsk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkTVGeneralClickScrapeAsk.Location = New System.Drawing.Point(136, 26)
        Me.chkTVGeneralClickScrapeAsk.Name = "chkTVGeneralClickScrapeAsk"
        Me.chkTVGeneralClickScrapeAsk.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkTVGeneralClickScrapeAsk.Size = New System.Drawing.Size(152, 17)
        Me.chkTVGeneralClickScrapeAsk.TabIndex = 67
        Me.chkTVGeneralClickScrapeAsk.Text = "Show Results Dialog"
        Me.chkTVGeneralClickScrapeAsk.UseVisualStyleBackColor = True
        '
        'gbTVGeneralMediaListOpts
        '
        Me.gbTVGeneralMediaListOpts.AutoSize = True
        Me.tblTVGeneral.SetColumnSpan(Me.gbTVGeneralMediaListOpts, 2)
        Me.gbTVGeneralMediaListOpts.Controls.Add(Me.tblTVGeneralMediaListOpts)
        Me.gbTVGeneralMediaListOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVGeneralMediaListOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVGeneralMediaListOpts.Location = New System.Drawing.Point(3, 83)
        Me.gbTVGeneralMediaListOpts.Name = "gbTVGeneralMediaListOpts"
        Me.tblTVGeneral.SetRowSpan(Me.gbTVGeneralMediaListOpts, 3)
        Me.gbTVGeneralMediaListOpts.Size = New System.Drawing.Size(442, 500)
        Me.gbTVGeneralMediaListOpts.TabIndex = 1
        Me.gbTVGeneralMediaListOpts.TabStop = False
        Me.gbTVGeneralMediaListOpts.Text = "Media List Options"
        '
        'tblTVGeneralMediaListOpts
        '
        Me.tblTVGeneralMediaListOpts.AutoSize = True
        Me.tblTVGeneralMediaListOpts.ColumnCount = 3
        Me.tblTVGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMediaListOpts.Controls.Add(Me.gbTVGeneralEpisodeListSorting, 1, 5)
        Me.tblTVGeneralMediaListOpts.Controls.Add(Me.gbTVGeneralSeasonListSortingOpts, 1, 4)
        Me.tblTVGeneralMediaListOpts.Controls.Add(Me.gbTVGeneralShowListSortingOpts, 0, 4)
        Me.tblTVGeneralMediaListOpts.Controls.Add(Me.gbTVGeneralMediaListSortTokensOpts, 1, 0)
        Me.tblTVGeneralMediaListOpts.Controls.Add(Me.chkTVDisplayMissingEpisodes, 0, 0)
        Me.tblTVGeneralMediaListOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneralMediaListOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVGeneralMediaListOpts.Name = "tblTVGeneralMediaListOpts"
        Me.tblTVGeneralMediaListOpts.RowCount = 7
        Me.tblTVGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListOpts.Size = New System.Drawing.Size(436, 479)
        Me.tblTVGeneralMediaListOpts.TabIndex = 74
        '
        'gbTVGeneralEpisodeListSorting
        '
        Me.gbTVGeneralEpisodeListSorting.AutoSize = True
        Me.gbTVGeneralEpisodeListSorting.Controls.Add(Me.tblTVGeneralEpisodeListSorting)
        Me.gbTVGeneralEpisodeListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVGeneralEpisodeListSorting.Location = New System.Drawing.Point(221, 295)
        Me.gbTVGeneralEpisodeListSorting.Name = "gbTVGeneralEpisodeListSorting"
        Me.gbTVGeneralEpisodeListSorting.Size = New System.Drawing.Size(212, 181)
        Me.gbTVGeneralEpisodeListSorting.TabIndex = 76
        Me.gbTVGeneralEpisodeListSorting.TabStop = False
        Me.gbTVGeneralEpisodeListSorting.Text = "Episode List Sorting"
        '
        'tblTVGeneralEpisodeListSorting
        '
        Me.tblTVGeneralEpisodeListSorting.AutoSize = True
        Me.tblTVGeneralEpisodeListSorting.ColumnCount = 6
        Me.tblTVGeneralEpisodeListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralEpisodeListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralEpisodeListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralEpisodeListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralEpisodeListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralEpisodeListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralEpisodeListSorting.Controls.Add(Me.btnTVGeneralEpisodeListSortingDown, 2, 1)
        Me.tblTVGeneralEpisodeListSorting.Controls.Add(Me.btnTVGeneralEpisodeListSortingUp, 1, 1)
        Me.tblTVGeneralEpisodeListSorting.Controls.Add(Me.lvTVGeneralEpisodeListSorting, 0, 0)
        Me.tblTVGeneralEpisodeListSorting.Controls.Add(Me.btnTVGeneralEpisodeListSortingReset, 4, 1)
        Me.tblTVGeneralEpisodeListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneralEpisodeListSorting.Location = New System.Drawing.Point(3, 18)
        Me.tblTVGeneralEpisodeListSorting.Name = "tblTVGeneralEpisodeListSorting"
        Me.tblTVGeneralEpisodeListSorting.RowCount = 3
        Me.tblTVGeneralEpisodeListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralEpisodeListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralEpisodeListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralEpisodeListSorting.Size = New System.Drawing.Size(206, 160)
        Me.tblTVGeneralEpisodeListSorting.TabIndex = 0
        '
        'btnTVGeneralEpisodeListSortingDown
        '
        Me.btnTVGeneralEpisodeListSortingDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVGeneralEpisodeListSortingDown.Image = CType(resources.GetObject("btnTVGeneralEpisodeListSortingDown.Image"), System.Drawing.Image)
        Me.btnTVGeneralEpisodeListSortingDown.Location = New System.Drawing.Point(91, 134)
        Me.btnTVGeneralEpisodeListSortingDown.Name = "btnTVGeneralEpisodeListSortingDown"
        Me.btnTVGeneralEpisodeListSortingDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralEpisodeListSortingDown.TabIndex = 12
        Me.btnTVGeneralEpisodeListSortingDown.UseVisualStyleBackColor = True
        '
        'btnTVGeneralEpisodeListSortingUp
        '
        Me.btnTVGeneralEpisodeListSortingUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVGeneralEpisodeListSortingUp.Image = CType(resources.GetObject("btnTVGeneralEpisodeListSortingUp.Image"), System.Drawing.Image)
        Me.btnTVGeneralEpisodeListSortingUp.Location = New System.Drawing.Point(62, 134)
        Me.btnTVGeneralEpisodeListSortingUp.Name = "btnTVGeneralEpisodeListSortingUp"
        Me.btnTVGeneralEpisodeListSortingUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralEpisodeListSortingUp.TabIndex = 13
        Me.btnTVGeneralEpisodeListSortingUp.UseVisualStyleBackColor = True
        '
        'lvTVGeneralEpisodeListSorting
        '
        Me.lvTVGeneralEpisodeListSorting.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTVGeneralEpisodeListSortingDisplayIndex, Me.colTVGeneralEpisodeListSortingColumn, Me.colTVGeneralEpisodeListSortingLabel, Me.colTVGeneralEpisodeListSortingHide})
        Me.tblTVGeneralEpisodeListSorting.SetColumnSpan(Me.lvTVGeneralEpisodeListSorting, 5)
        Me.lvTVGeneralEpisodeListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTVGeneralEpisodeListSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvTVGeneralEpisodeListSorting.FullRowSelect = True
        Me.lvTVGeneralEpisodeListSorting.HideSelection = False
        Me.lvTVGeneralEpisodeListSorting.Location = New System.Drawing.Point(3, 3)
        Me.lvTVGeneralEpisodeListSorting.Name = "lvTVGeneralEpisodeListSorting"
        Me.lvTVGeneralEpisodeListSorting.Size = New System.Drawing.Size(200, 125)
        Me.lvTVGeneralEpisodeListSorting.TabIndex = 10
        Me.lvTVGeneralEpisodeListSorting.UseCompatibleStateImageBehavior = False
        Me.lvTVGeneralEpisodeListSorting.View = System.Windows.Forms.View.Details
        '
        'colTVGeneralEpisodeListSortingDisplayIndex
        '
        Me.colTVGeneralEpisodeListSortingDisplayIndex.Text = "DisplayIndex"
        Me.colTVGeneralEpisodeListSortingDisplayIndex.Width = 0
        '
        'colTVGeneralEpisodeListSortingColumn
        '
        Me.colTVGeneralEpisodeListSortingColumn.Text = "DBName"
        Me.colTVGeneralEpisodeListSortingColumn.Width = 0
        '
        'colTVGeneralEpisodeListSortingLabel
        '
        Me.colTVGeneralEpisodeListSortingLabel.Text = "Column"
        Me.colTVGeneralEpisodeListSortingLabel.Width = 110
        '
        'colTVGeneralEpisodeListSortingHide
        '
        Me.colTVGeneralEpisodeListSortingHide.Text = "Hide"
        '
        'btnTVGeneralEpisodeListSortingReset
        '
        Me.btnTVGeneralEpisodeListSortingReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVGeneralEpisodeListSortingReset.Image = CType(resources.GetObject("btnTVGeneralEpisodeListSortingReset.Image"), System.Drawing.Image)
        Me.btnTVGeneralEpisodeListSortingReset.Location = New System.Drawing.Point(180, 134)
        Me.btnTVGeneralEpisodeListSortingReset.Name = "btnTVGeneralEpisodeListSortingReset"
        Me.btnTVGeneralEpisodeListSortingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralEpisodeListSortingReset.TabIndex = 11
        Me.btnTVGeneralEpisodeListSortingReset.UseVisualStyleBackColor = True
        '
        'gbTVGeneralSeasonListSortingOpts
        '
        Me.gbTVGeneralSeasonListSortingOpts.AutoSize = True
        Me.gbTVGeneralSeasonListSortingOpts.Controls.Add(Me.tblTVGeneralSeasonListSorting)
        Me.gbTVGeneralSeasonListSortingOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVGeneralSeasonListSortingOpts.Location = New System.Drawing.Point(221, 108)
        Me.gbTVGeneralSeasonListSortingOpts.Name = "gbTVGeneralSeasonListSortingOpts"
        Me.gbTVGeneralSeasonListSortingOpts.Size = New System.Drawing.Size(212, 181)
        Me.gbTVGeneralSeasonListSortingOpts.TabIndex = 75
        Me.gbTVGeneralSeasonListSortingOpts.TabStop = False
        Me.gbTVGeneralSeasonListSortingOpts.Text = "Season List Sorting"
        '
        'tblTVGeneralSeasonListSorting
        '
        Me.tblTVGeneralSeasonListSorting.AutoSize = True
        Me.tblTVGeneralSeasonListSorting.ColumnCount = 6
        Me.tblTVGeneralSeasonListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralSeasonListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralSeasonListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralSeasonListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralSeasonListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralSeasonListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralSeasonListSorting.Controls.Add(Me.btnTVGeneralSeasonListSortingDown, 2, 1)
        Me.tblTVGeneralSeasonListSorting.Controls.Add(Me.btnTVGeneralSeasonListSortingUp, 1, 1)
        Me.tblTVGeneralSeasonListSorting.Controls.Add(Me.lvTVGeneralSeasonListSorting, 0, 0)
        Me.tblTVGeneralSeasonListSorting.Controls.Add(Me.btnTVGeneralSeasonListSortingReset, 4, 1)
        Me.tblTVGeneralSeasonListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneralSeasonListSorting.Location = New System.Drawing.Point(3, 18)
        Me.tblTVGeneralSeasonListSorting.Name = "tblTVGeneralSeasonListSorting"
        Me.tblTVGeneralSeasonListSorting.RowCount = 3
        Me.tblTVGeneralSeasonListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralSeasonListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralSeasonListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralSeasonListSorting.Size = New System.Drawing.Size(206, 160)
        Me.tblTVGeneralSeasonListSorting.TabIndex = 0
        '
        'btnTVGeneralSeasonListSortingDown
        '
        Me.btnTVGeneralSeasonListSortingDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVGeneralSeasonListSortingDown.Image = CType(resources.GetObject("btnTVGeneralSeasonListSortingDown.Image"), System.Drawing.Image)
        Me.btnTVGeneralSeasonListSortingDown.Location = New System.Drawing.Point(91, 134)
        Me.btnTVGeneralSeasonListSortingDown.Name = "btnTVGeneralSeasonListSortingDown"
        Me.btnTVGeneralSeasonListSortingDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralSeasonListSortingDown.TabIndex = 12
        Me.btnTVGeneralSeasonListSortingDown.UseVisualStyleBackColor = True
        '
        'btnTVGeneralSeasonListSortingUp
        '
        Me.btnTVGeneralSeasonListSortingUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVGeneralSeasonListSortingUp.Image = CType(resources.GetObject("btnTVGeneralSeasonListSortingUp.Image"), System.Drawing.Image)
        Me.btnTVGeneralSeasonListSortingUp.Location = New System.Drawing.Point(62, 134)
        Me.btnTVGeneralSeasonListSortingUp.Name = "btnTVGeneralSeasonListSortingUp"
        Me.btnTVGeneralSeasonListSortingUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralSeasonListSortingUp.TabIndex = 13
        Me.btnTVGeneralSeasonListSortingUp.UseVisualStyleBackColor = True
        '
        'lvTVGeneralSeasonListSorting
        '
        Me.lvTVGeneralSeasonListSorting.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTVGeneralSeasonListSortingDisplayIndex, Me.colTVGeneralSeasonListSortingColumn, Me.colTVGeneralSeasonListSortingLabel, Me.colTVGeneralSeasonListSortingHide})
        Me.tblTVGeneralSeasonListSorting.SetColumnSpan(Me.lvTVGeneralSeasonListSorting, 5)
        Me.lvTVGeneralSeasonListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTVGeneralSeasonListSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvTVGeneralSeasonListSorting.FullRowSelect = True
        Me.lvTVGeneralSeasonListSorting.HideSelection = False
        Me.lvTVGeneralSeasonListSorting.Location = New System.Drawing.Point(3, 3)
        Me.lvTVGeneralSeasonListSorting.Name = "lvTVGeneralSeasonListSorting"
        Me.lvTVGeneralSeasonListSorting.Size = New System.Drawing.Size(200, 125)
        Me.lvTVGeneralSeasonListSorting.TabIndex = 10
        Me.lvTVGeneralSeasonListSorting.UseCompatibleStateImageBehavior = False
        Me.lvTVGeneralSeasonListSorting.View = System.Windows.Forms.View.Details
        '
        'colTVGeneralSeasonListSortingDisplayIndex
        '
        Me.colTVGeneralSeasonListSortingDisplayIndex.Text = "DisplayIndex"
        Me.colTVGeneralSeasonListSortingDisplayIndex.Width = 0
        '
        'colTVGeneralSeasonListSortingColumn
        '
        Me.colTVGeneralSeasonListSortingColumn.Text = "DBName"
        Me.colTVGeneralSeasonListSortingColumn.Width = 0
        '
        'colTVGeneralSeasonListSortingLabel
        '
        Me.colTVGeneralSeasonListSortingLabel.Text = "Column"
        Me.colTVGeneralSeasonListSortingLabel.Width = 110
        '
        'colTVGeneralSeasonListSortingHide
        '
        Me.colTVGeneralSeasonListSortingHide.Text = "Hide"
        '
        'btnTVGeneralSeasonListSortingReset
        '
        Me.btnTVGeneralSeasonListSortingReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVGeneralSeasonListSortingReset.Image = CType(resources.GetObject("btnTVGeneralSeasonListSortingReset.Image"), System.Drawing.Image)
        Me.btnTVGeneralSeasonListSortingReset.Location = New System.Drawing.Point(180, 134)
        Me.btnTVGeneralSeasonListSortingReset.Name = "btnTVGeneralSeasonListSortingReset"
        Me.btnTVGeneralSeasonListSortingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralSeasonListSortingReset.TabIndex = 11
        Me.btnTVGeneralSeasonListSortingReset.UseVisualStyleBackColor = True
        '
        'gbTVGeneralShowListSortingOpts
        '
        Me.gbTVGeneralShowListSortingOpts.AutoSize = True
        Me.gbTVGeneralShowListSortingOpts.Controls.Add(Me.tblTVGeneralShowListSorting)
        Me.gbTVGeneralShowListSortingOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVGeneralShowListSortingOpts.Location = New System.Drawing.Point(3, 108)
        Me.gbTVGeneralShowListSortingOpts.Name = "gbTVGeneralShowListSortingOpts"
        Me.tblTVGeneralMediaListOpts.SetRowSpan(Me.gbTVGeneralShowListSortingOpts, 2)
        Me.gbTVGeneralShowListSortingOpts.Size = New System.Drawing.Size(212, 368)
        Me.gbTVGeneralShowListSortingOpts.TabIndex = 74
        Me.gbTVGeneralShowListSortingOpts.TabStop = False
        Me.gbTVGeneralShowListSortingOpts.Text = "Show List Sorting"
        '
        'tblTVGeneralShowListSorting
        '
        Me.tblTVGeneralShowListSorting.AutoSize = True
        Me.tblTVGeneralShowListSorting.ColumnCount = 6
        Me.tblTVGeneralShowListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralShowListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralShowListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralShowListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralShowListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralShowListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralShowListSorting.Controls.Add(Me.btnTVGeneralShowListSortingDown, 2, 1)
        Me.tblTVGeneralShowListSorting.Controls.Add(Me.btnTVGeneralShowListSortingUp, 1, 1)
        Me.tblTVGeneralShowListSorting.Controls.Add(Me.lvTVGeneralShowListSorting, 0, 0)
        Me.tblTVGeneralShowListSorting.Controls.Add(Me.btnTVGeneralShowListSortingReset, 4, 1)
        Me.tblTVGeneralShowListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneralShowListSorting.Location = New System.Drawing.Point(3, 18)
        Me.tblTVGeneralShowListSorting.Name = "tblTVGeneralShowListSorting"
        Me.tblTVGeneralShowListSorting.RowCount = 3
        Me.tblTVGeneralShowListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralShowListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralShowListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralShowListSorting.Size = New System.Drawing.Size(206, 347)
        Me.tblTVGeneralShowListSorting.TabIndex = 0
        '
        'btnTVGeneralShowListSortingDown
        '
        Me.btnTVGeneralShowListSortingDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVGeneralShowListSortingDown.Image = CType(resources.GetObject("btnTVGeneralShowListSortingDown.Image"), System.Drawing.Image)
        Me.btnTVGeneralShowListSortingDown.Location = New System.Drawing.Point(91, 321)
        Me.btnTVGeneralShowListSortingDown.Name = "btnTVGeneralShowListSortingDown"
        Me.btnTVGeneralShowListSortingDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralShowListSortingDown.TabIndex = 12
        Me.btnTVGeneralShowListSortingDown.UseVisualStyleBackColor = True
        '
        'btnTVGeneralShowListSortingUp
        '
        Me.btnTVGeneralShowListSortingUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVGeneralShowListSortingUp.Image = CType(resources.GetObject("btnTVGeneralShowListSortingUp.Image"), System.Drawing.Image)
        Me.btnTVGeneralShowListSortingUp.Location = New System.Drawing.Point(62, 321)
        Me.btnTVGeneralShowListSortingUp.Name = "btnTVGeneralShowListSortingUp"
        Me.btnTVGeneralShowListSortingUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralShowListSortingUp.TabIndex = 13
        Me.btnTVGeneralShowListSortingUp.UseVisualStyleBackColor = True
        '
        'lvTVGeneralShowListSorting
        '
        Me.lvTVGeneralShowListSorting.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTVGeneralShowListSortingDisplayIndex, Me.colTVGeneralShowListSortingColumn, Me.colTVGeneralShowListSortingLabel, Me.colTVGeneralShowListSortingHide})
        Me.tblTVGeneralShowListSorting.SetColumnSpan(Me.lvTVGeneralShowListSorting, 5)
        Me.lvTVGeneralShowListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTVGeneralShowListSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvTVGeneralShowListSorting.FullRowSelect = True
        Me.lvTVGeneralShowListSorting.HideSelection = False
        Me.lvTVGeneralShowListSorting.Location = New System.Drawing.Point(3, 3)
        Me.lvTVGeneralShowListSorting.Name = "lvTVGeneralShowListSorting"
        Me.lvTVGeneralShowListSorting.Size = New System.Drawing.Size(200, 312)
        Me.lvTVGeneralShowListSorting.TabIndex = 10
        Me.lvTVGeneralShowListSorting.UseCompatibleStateImageBehavior = False
        Me.lvTVGeneralShowListSorting.View = System.Windows.Forms.View.Details
        '
        'colTVGeneralShowListSortingDisplayIndex
        '
        Me.colTVGeneralShowListSortingDisplayIndex.Text = "DisplayIndex"
        Me.colTVGeneralShowListSortingDisplayIndex.Width = 0
        '
        'colTVGeneralShowListSortingColumn
        '
        Me.colTVGeneralShowListSortingColumn.Text = "DBName"
        Me.colTVGeneralShowListSortingColumn.Width = 0
        '
        'colTVGeneralShowListSortingLabel
        '
        Me.colTVGeneralShowListSortingLabel.Text = "Column"
        Me.colTVGeneralShowListSortingLabel.Width = 110
        '
        'colTVGeneralShowListSortingHide
        '
        Me.colTVGeneralShowListSortingHide.Text = "Hide"
        '
        'btnTVGeneralShowListSortingReset
        '
        Me.btnTVGeneralShowListSortingReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVGeneralShowListSortingReset.Image = CType(resources.GetObject("btnTVGeneralShowListSortingReset.Image"), System.Drawing.Image)
        Me.btnTVGeneralShowListSortingReset.Location = New System.Drawing.Point(180, 321)
        Me.btnTVGeneralShowListSortingReset.Name = "btnTVGeneralShowListSortingReset"
        Me.btnTVGeneralShowListSortingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVGeneralShowListSortingReset.TabIndex = 11
        Me.btnTVGeneralShowListSortingReset.UseVisualStyleBackColor = True
        '
        'gbTVGeneralMediaListSortTokensOpts
        '
        Me.gbTVGeneralMediaListSortTokensOpts.AutoSize = True
        Me.gbTVGeneralMediaListSortTokensOpts.Controls.Add(Me.tblTVGeneralMediaListSortTokensOpts)
        Me.gbTVGeneralMediaListSortTokensOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVGeneralMediaListSortTokensOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVGeneralMediaListSortTokensOpts.Location = New System.Drawing.Point(221, 3)
        Me.gbTVGeneralMediaListSortTokensOpts.Name = "gbTVGeneralMediaListSortTokensOpts"
        Me.tblTVGeneralMediaListOpts.SetRowSpan(Me.gbTVGeneralMediaListSortTokensOpts, 3)
        Me.gbTVGeneralMediaListSortTokensOpts.Size = New System.Drawing.Size(212, 99)
        Me.gbTVGeneralMediaListSortTokensOpts.TabIndex = 72
        Me.gbTVGeneralMediaListSortTokensOpts.TabStop = False
        Me.gbTVGeneralMediaListSortTokensOpts.Text = "Sort Tokens to Ignore"
        '
        'tblTVGeneralMediaListSortTokensOpts
        '
        Me.tblTVGeneralMediaListSortTokensOpts.AutoSize = True
        Me.tblTVGeneralMediaListSortTokensOpts.ColumnCount = 5
        Me.tblTVGeneralMediaListSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMediaListSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMediaListSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMediaListSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMediaListSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVGeneralMediaListSortTokensOpts.Controls.Add(Me.btnTVSortTokenReset, 3, 1)
        Me.tblTVGeneralMediaListSortTokensOpts.Controls.Add(Me.btnTVSortTokenRemove, 2, 1)
        Me.tblTVGeneralMediaListSortTokensOpts.Controls.Add(Me.lstTVSortTokens, 0, 0)
        Me.tblTVGeneralMediaListSortTokensOpts.Controls.Add(Me.btnTVSortTokenAdd, 1, 1)
        Me.tblTVGeneralMediaListSortTokensOpts.Controls.Add(Me.txtTVSortToken, 0, 1)
        Me.tblTVGeneralMediaListSortTokensOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVGeneralMediaListSortTokensOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVGeneralMediaListSortTokensOpts.Name = "tblTVGeneralMediaListSortTokensOpts"
        Me.tblTVGeneralMediaListSortTokensOpts.RowCount = 3
        Me.tblTVGeneralMediaListSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVGeneralMediaListSortTokensOpts.Size = New System.Drawing.Size(206, 78)
        Me.tblTVGeneralMediaListSortTokensOpts.TabIndex = 74
        '
        'btnTVSortTokenReset
        '
        Me.btnTVSortTokenReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVSortTokenReset.Image = CType(resources.GetObject("btnTVSortTokenReset.Image"), System.Drawing.Image)
        Me.btnTVSortTokenReset.Location = New System.Drawing.Point(180, 52)
        Me.btnTVSortTokenReset.Name = "btnTVSortTokenReset"
        Me.btnTVSortTokenReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSortTokenReset.TabIndex = 11
        Me.btnTVSortTokenReset.UseVisualStyleBackColor = True
        '
        'btnTVSortTokenRemove
        '
        Me.btnTVSortTokenRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVSortTokenRemove.Image = CType(resources.GetObject("btnTVSortTokenRemove.Image"), System.Drawing.Image)
        Me.btnTVSortTokenRemove.Location = New System.Drawing.Point(128, 52)
        Me.btnTVSortTokenRemove.Name = "btnTVSortTokenRemove"
        Me.btnTVSortTokenRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSortTokenRemove.TabIndex = 3
        Me.btnTVSortTokenRemove.UseVisualStyleBackColor = True
        '
        'lstTVSortTokens
        '
        Me.tblTVGeneralMediaListSortTokensOpts.SetColumnSpan(Me.lstTVSortTokens, 4)
        Me.lstTVSortTokens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTVSortTokens.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstTVSortTokens.FormattingEnabled = True
        Me.lstTVSortTokens.Location = New System.Drawing.Point(3, 3)
        Me.lstTVSortTokens.Name = "lstTVSortTokens"
        Me.lstTVSortTokens.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTVSortTokens.Size = New System.Drawing.Size(200, 43)
        Me.lstTVSortTokens.Sorted = True
        Me.lstTVSortTokens.TabIndex = 0
        '
        'btnTVSortTokenAdd
        '
        Me.btnTVSortTokenAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVSortTokenAdd.Image = CType(resources.GetObject("btnTVSortTokenAdd.Image"), System.Drawing.Image)
        Me.btnTVSortTokenAdd.Location = New System.Drawing.Point(99, 52)
        Me.btnTVSortTokenAdd.Name = "btnTVSortTokenAdd"
        Me.btnTVSortTokenAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSortTokenAdd.TabIndex = 2
        Me.btnTVSortTokenAdd.UseVisualStyleBackColor = True
        '
        'txtTVSortToken
        '
        Me.txtTVSortToken.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTVSortToken.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVSortToken.Location = New System.Drawing.Point(3, 52)
        Me.txtTVSortToken.Name = "txtTVSortToken"
        Me.txtTVSortToken.Size = New System.Drawing.Size(90, 22)
        Me.txtTVSortToken.TabIndex = 1
        '
        'chkTVDisplayMissingEpisodes
        '
        Me.chkTVDisplayMissingEpisodes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVDisplayMissingEpisodes.AutoSize = True
        Me.chkTVDisplayMissingEpisodes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVDisplayMissingEpisodes.Location = New System.Drawing.Point(3, 3)
        Me.chkTVDisplayMissingEpisodes.Name = "chkTVDisplayMissingEpisodes"
        Me.chkTVDisplayMissingEpisodes.Size = New System.Drawing.Size(155, 17)
        Me.chkTVDisplayMissingEpisodes.TabIndex = 3
        Me.chkTVDisplayMissingEpisodes.Text = "Display Missing Episodes"
        Me.chkTVDisplayMissingEpisodes.UseVisualStyleBackColor = True
        '
        'gbTVEpisodeFilterOpts
        '
        Me.gbTVEpisodeFilterOpts.AutoSize = True
        Me.gbTVEpisodeFilterOpts.Controls.Add(Me.tblTVEpisodeFilterOpts)
        Me.gbTVEpisodeFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVEpisodeFilterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVEpisodeFilterOpts.Location = New System.Drawing.Point(451, 269)
        Me.gbTVEpisodeFilterOpts.Name = "gbTVEpisodeFilterOpts"
        Me.gbTVEpisodeFilterOpts.Size = New System.Drawing.Size(497, 203)
        Me.gbTVEpisodeFilterOpts.TabIndex = 3
        Me.gbTVEpisodeFilterOpts.TabStop = False
        Me.gbTVEpisodeFilterOpts.Text = "Episode Folder/File Name Filters"
        '
        'tblTVEpisodeFilterOpts
        '
        Me.tblTVEpisodeFilterOpts.AutoSize = True
        Me.tblTVEpisodeFilterOpts.ColumnCount = 6
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterRemove, 4, 3)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterDown, 3, 3)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterReset, 4, 1)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterUp, 2, 3)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.chkTVEpisodeNoFilter, 0, 0)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.chkTVEpisodeProperCase, 0, 1)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.btnTVEpisodeFilterAdd, 1, 3)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.lstTVEpisodeFilter, 0, 2)
        Me.tblTVEpisodeFilterOpts.Controls.Add(Me.txtTVEpisodeFilter, 0, 3)
        Me.tblTVEpisodeFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVEpisodeFilterOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVEpisodeFilterOpts.Name = "tblTVEpisodeFilterOpts"
        Me.tblTVEpisodeFilterOpts.RowCount = 5
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVEpisodeFilterOpts.Size = New System.Drawing.Size(491, 182)
        Me.tblTVEpisodeFilterOpts.TabIndex = 5
        '
        'btnTVEpisodeFilterRemove
        '
        Me.btnTVEpisodeFilterRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVEpisodeFilterRemove.Image = CType(resources.GetObject("btnTVEpisodeFilterRemove.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterRemove.Location = New System.Drawing.Point(294, 156)
        Me.btnTVEpisodeFilterRemove.Name = "btnTVEpisodeFilterRemove"
        Me.btnTVEpisodeFilterRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterRemove.TabIndex = 8
        Me.btnTVEpisodeFilterRemove.UseVisualStyleBackColor = True
        '
        'btnTVEpisodeFilterDown
        '
        Me.btnTVEpisodeFilterDown.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVEpisodeFilterDown.Image = CType(resources.GetObject("btnTVEpisodeFilterDown.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterDown.Location = New System.Drawing.Point(262, 156)
        Me.btnTVEpisodeFilterDown.Name = "btnTVEpisodeFilterDown"
        Me.btnTVEpisodeFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterDown.TabIndex = 7
        Me.btnTVEpisodeFilterDown.UseVisualStyleBackColor = True
        '
        'btnTVEpisodeFilterReset
        '
        Me.btnTVEpisodeFilterReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVEpisodeFilterReset.Image = CType(resources.GetObject("btnTVEpisodeFilterReset.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterReset.Location = New System.Drawing.Point(294, 26)
        Me.btnTVEpisodeFilterReset.Name = "btnTVEpisodeFilterReset"
        Me.btnTVEpisodeFilterReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterReset.TabIndex = 3
        Me.btnTVEpisodeFilterReset.UseVisualStyleBackColor = True
        '
        'btnTVEpisodeFilterUp
        '
        Me.btnTVEpisodeFilterUp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVEpisodeFilterUp.Image = CType(resources.GetObject("btnTVEpisodeFilterUp.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterUp.Location = New System.Drawing.Point(233, 156)
        Me.btnTVEpisodeFilterUp.Name = "btnTVEpisodeFilterUp"
        Me.btnTVEpisodeFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterUp.TabIndex = 6
        Me.btnTVEpisodeFilterUp.UseVisualStyleBackColor = True
        '
        'chkTVEpisodeNoFilter
        '
        Me.chkTVEpisodeNoFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVEpisodeNoFilter.AutoSize = True
        Me.chkTVEpisodeNoFilter.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.tblTVEpisodeFilterOpts.SetColumnSpan(Me.chkTVEpisodeNoFilter, 5)
        Me.chkTVEpisodeNoFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkTVEpisodeNoFilter.Location = New System.Drawing.Point(3, 3)
        Me.chkTVEpisodeNoFilter.Name = "chkTVEpisodeNoFilter"
        Me.chkTVEpisodeNoFilter.Size = New System.Drawing.Size(222, 17)
        Me.chkTVEpisodeNoFilter.TabIndex = 0
        Me.chkTVEpisodeNoFilter.Text = "Build Episode Title Instead of Filtering"
        Me.chkTVEpisodeNoFilter.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkTVEpisodeNoFilter.UseVisualStyleBackColor = True
        '
        'chkTVEpisodeProperCase
        '
        Me.chkTVEpisodeProperCase.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVEpisodeProperCase.AutoSize = True
        Me.tblTVEpisodeFilterOpts.SetColumnSpan(Me.chkTVEpisodeProperCase, 4)
        Me.chkTVEpisodeProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVEpisodeProperCase.Location = New System.Drawing.Point(3, 29)
        Me.chkTVEpisodeProperCase.Name = "chkTVEpisodeProperCase"
        Me.chkTVEpisodeProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkTVEpisodeProperCase.TabIndex = 1
        Me.chkTVEpisodeProperCase.Text = "Convert Names to Proper Case"
        Me.chkTVEpisodeProperCase.UseVisualStyleBackColor = True
        '
        'btnTVEpisodeFilterAdd
        '
        Me.btnTVEpisodeFilterAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVEpisodeFilterAdd.Image = CType(resources.GetObject("btnTVEpisodeFilterAdd.Image"), System.Drawing.Image)
        Me.btnTVEpisodeFilterAdd.Location = New System.Drawing.Point(204, 156)
        Me.btnTVEpisodeFilterAdd.Name = "btnTVEpisodeFilterAdd"
        Me.btnTVEpisodeFilterAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVEpisodeFilterAdd.TabIndex = 5
        Me.btnTVEpisodeFilterAdd.UseVisualStyleBackColor = True
        '
        'lstTVEpisodeFilter
        '
        Me.tblTVEpisodeFilterOpts.SetColumnSpan(Me.lstTVEpisodeFilter, 5)
        Me.lstTVEpisodeFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTVEpisodeFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstTVEpisodeFilter.FormattingEnabled = True
        Me.lstTVEpisodeFilter.Location = New System.Drawing.Point(3, 55)
        Me.lstTVEpisodeFilter.Name = "lstTVEpisodeFilter"
        Me.lstTVEpisodeFilter.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTVEpisodeFilter.Size = New System.Drawing.Size(314, 95)
        Me.lstTVEpisodeFilter.TabIndex = 2
        '
        'txtTVEpisodeFilter
        '
        Me.txtTVEpisodeFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTVEpisodeFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVEpisodeFilter.Location = New System.Drawing.Point(3, 156)
        Me.txtTVEpisodeFilter.Name = "txtTVEpisodeFilter"
        Me.txtTVEpisodeFilter.Size = New System.Drawing.Size(195, 22)
        Me.txtTVEpisodeFilter.TabIndex = 4
        '
        'gbTVShowFilterOpts
        '
        Me.gbTVShowFilterOpts.AutoSize = True
        Me.gbTVShowFilterOpts.Controls.Add(Me.tblTVShowFilterOpts)
        Me.gbTVShowFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVShowFilterOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbTVShowFilterOpts.Location = New System.Drawing.Point(451, 83)
        Me.gbTVShowFilterOpts.Name = "gbTVShowFilterOpts"
        Me.gbTVShowFilterOpts.Size = New System.Drawing.Size(497, 180)
        Me.gbTVShowFilterOpts.TabIndex = 2
        Me.gbTVShowFilterOpts.TabStop = False
        Me.gbTVShowFilterOpts.Text = "Show Folder/File Name Filters"
        '
        'tblTVShowFilterOpts
        '
        Me.tblTVShowFilterOpts.AutoSize = True
        Me.tblTVShowFilterOpts.ColumnCount = 6
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterRemove, 4, 2)
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterDown, 3, 2)
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterReset, 4, 0)
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterUp, 2, 2)
        Me.tblTVShowFilterOpts.Controls.Add(Me.chkTVShowProperCase, 0, 0)
        Me.tblTVShowFilterOpts.Controls.Add(Me.lstTVShowFilter, 0, 1)
        Me.tblTVShowFilterOpts.Controls.Add(Me.btnTVShowFilterAdd, 1, 2)
        Me.tblTVShowFilterOpts.Controls.Add(Me.txtTVShowFilter, 0, 2)
        Me.tblTVShowFilterOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVShowFilterOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblTVShowFilterOpts.Name = "tblTVShowFilterOpts"
        Me.tblTVShowFilterOpts.RowCount = 4
        Me.tblTVShowFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVShowFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVShowFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVShowFilterOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVShowFilterOpts.Size = New System.Drawing.Size(491, 159)
        Me.tblTVShowFilterOpts.TabIndex = 5
        '
        'btnTVShowFilterRemove
        '
        Me.btnTVShowFilterRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVShowFilterRemove.Image = CType(resources.GetObject("btnTVShowFilterRemove.Image"), System.Drawing.Image)
        Me.btnTVShowFilterRemove.Location = New System.Drawing.Point(294, 133)
        Me.btnTVShowFilterRemove.Name = "btnTVShowFilterRemove"
        Me.btnTVShowFilterRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterRemove.TabIndex = 7
        Me.btnTVShowFilterRemove.UseVisualStyleBackColor = True
        '
        'btnTVShowFilterDown
        '
        Me.btnTVShowFilterDown.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVShowFilterDown.Image = CType(resources.GetObject("btnTVShowFilterDown.Image"), System.Drawing.Image)
        Me.btnTVShowFilterDown.Location = New System.Drawing.Point(262, 133)
        Me.btnTVShowFilterDown.Name = "btnTVShowFilterDown"
        Me.btnTVShowFilterDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterDown.TabIndex = 6
        Me.btnTVShowFilterDown.UseVisualStyleBackColor = True
        '
        'btnTVShowFilterReset
        '
        Me.btnTVShowFilterReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVShowFilterReset.Image = CType(resources.GetObject("btnTVShowFilterReset.Image"), System.Drawing.Image)
        Me.btnTVShowFilterReset.Location = New System.Drawing.Point(294, 3)
        Me.btnTVShowFilterReset.Name = "btnTVShowFilterReset"
        Me.btnTVShowFilterReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterReset.TabIndex = 2
        Me.btnTVShowFilterReset.UseVisualStyleBackColor = True
        '
        'btnTVShowFilterUp
        '
        Me.btnTVShowFilterUp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVShowFilterUp.Image = CType(resources.GetObject("btnTVShowFilterUp.Image"), System.Drawing.Image)
        Me.btnTVShowFilterUp.Location = New System.Drawing.Point(233, 133)
        Me.btnTVShowFilterUp.Name = "btnTVShowFilterUp"
        Me.btnTVShowFilterUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterUp.TabIndex = 5
        Me.btnTVShowFilterUp.UseVisualStyleBackColor = True
        '
        'chkTVShowProperCase
        '
        Me.chkTVShowProperCase.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTVShowProperCase.AutoSize = True
        Me.tblTVShowFilterOpts.SetColumnSpan(Me.chkTVShowProperCase, 4)
        Me.chkTVShowProperCase.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTVShowProperCase.Location = New System.Drawing.Point(3, 6)
        Me.chkTVShowProperCase.Name = "chkTVShowProperCase"
        Me.chkTVShowProperCase.Size = New System.Drawing.Size(181, 17)
        Me.chkTVShowProperCase.TabIndex = 0
        Me.chkTVShowProperCase.Text = "Convert Names to Proper Case"
        Me.chkTVShowProperCase.UseVisualStyleBackColor = True
        '
        'lstTVShowFilter
        '
        Me.tblTVShowFilterOpts.SetColumnSpan(Me.lstTVShowFilter, 5)
        Me.lstTVShowFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstTVShowFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstTVShowFilter.FormattingEnabled = True
        Me.lstTVShowFilter.Location = New System.Drawing.Point(3, 32)
        Me.lstTVShowFilter.Name = "lstTVShowFilter"
        Me.lstTVShowFilter.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstTVShowFilter.Size = New System.Drawing.Size(314, 95)
        Me.lstTVShowFilter.TabIndex = 1
        '
        'btnTVShowFilterAdd
        '
        Me.btnTVShowFilterAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVShowFilterAdd.Image = CType(resources.GetObject("btnTVShowFilterAdd.Image"), System.Drawing.Image)
        Me.btnTVShowFilterAdd.Location = New System.Drawing.Point(204, 133)
        Me.btnTVShowFilterAdd.Name = "btnTVShowFilterAdd"
        Me.btnTVShowFilterAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnTVShowFilterAdd.TabIndex = 4
        Me.btnTVShowFilterAdd.UseVisualStyleBackColor = True
        '
        'txtTVShowFilter
        '
        Me.txtTVShowFilter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTVShowFilter.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTVShowFilter.Location = New System.Drawing.Point(3, 133)
        Me.txtTVShowFilter.Name = "txtTVShowFilter"
        Me.txtTVShowFilter.Size = New System.Drawing.Size(195, 22)
        Me.txtTVShowFilter.TabIndex = 3
        '
        'gbTVGeneralMainWindowOpts
        '
        Me.gbTVGeneralMainWindowOpts.AutoSize = True
        Me.gbTVGeneralMainWindowOpts.Controls.Add(Me.TableLayoutPanel4)
        Me.gbTVGeneralMainWindowOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVGeneralMainWindowOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTVGeneralMainWindowOpts.Location = New System.Drawing.Point(451, 3)
        Me.gbTVGeneralMainWindowOpts.Name = "gbTVGeneralMainWindowOpts"
        Me.gbTVGeneralMainWindowOpts.Size = New System.Drawing.Size(497, 74)
        Me.gbTVGeneralMainWindowOpts.TabIndex = 4
        Me.gbTVGeneralMainWindowOpts.TabStop = False
        Me.gbTVGeneralMainWindowOpts.Text = "Main Window"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.AutoSize = True
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel4.Controls.Add(Me.lblTVLanguageOverlay, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.cbTVLanguageOverlay, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 3
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(491, 53)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'lblTVLanguageOverlay
        '
        Me.lblTVLanguageOverlay.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTVLanguageOverlay.AutoSize = True
        Me.lblTVLanguageOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTVLanguageOverlay.Location = New System.Drawing.Point(3, 0)
        Me.lblTVLanguageOverlay.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblTVLanguageOverlay.Name = "lblTVLanguageOverlay"
        Me.lblTVLanguageOverlay.Size = New System.Drawing.Size(240, 26)
        Me.lblTVLanguageOverlay.TabIndex = 1
        Me.lblTVLanguageOverlay.Text = "Display best Audio Stream with the following Language:"
        Me.lblTVLanguageOverlay.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'cbTVLanguageOverlay
        '
        Me.cbTVLanguageOverlay.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbTVLanguageOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTVLanguageOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbTVLanguageOverlay.FormattingEnabled = True
        Me.cbTVLanguageOverlay.Location = New System.Drawing.Point(36, 29)
        Me.cbTVLanguageOverlay.Name = "cbTVLanguageOverlay"
        Me.cbTVLanguageOverlay.Size = New System.Drawing.Size(174, 21)
        Me.cbTVLanguageOverlay.Sorted = True
        Me.cbTVLanguageOverlay.TabIndex = 2
        '
        'frmTV_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 611)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmTV_GUI"
        Me.Text = "frmTV_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblTVGeneral.ResumeLayout(False)
        Me.tblTVGeneral.PerformLayout()
        Me.gbTVGeneralCustomScrapeButton.ResumeLayout(False)
        Me.gbTVGeneralCustomScrapeButton.PerformLayout()
        Me.tblTVGeneralCustomScrapeButton.ResumeLayout(False)
        Me.tblTVGeneralCustomScrapeButton.PerformLayout()
        Me.gbTVGeneralMiscOpts.ResumeLayout(False)
        Me.gbTVGeneralMiscOpts.PerformLayout()
        Me.tblTVGeneralMiscOpts.ResumeLayout(False)
        Me.tblTVGeneralMiscOpts.PerformLayout()
        Me.gbTVGeneralMediaListOpts.ResumeLayout(False)
        Me.gbTVGeneralMediaListOpts.PerformLayout()
        Me.tblTVGeneralMediaListOpts.ResumeLayout(False)
        Me.tblTVGeneralMediaListOpts.PerformLayout()
        Me.gbTVGeneralEpisodeListSorting.ResumeLayout(False)
        Me.gbTVGeneralEpisodeListSorting.PerformLayout()
        Me.tblTVGeneralEpisodeListSorting.ResumeLayout(False)
        Me.gbTVGeneralSeasonListSortingOpts.ResumeLayout(False)
        Me.gbTVGeneralSeasonListSortingOpts.PerformLayout()
        Me.tblTVGeneralSeasonListSorting.ResumeLayout(False)
        Me.gbTVGeneralShowListSortingOpts.ResumeLayout(False)
        Me.gbTVGeneralShowListSortingOpts.PerformLayout()
        Me.tblTVGeneralShowListSorting.ResumeLayout(False)
        Me.gbTVGeneralMediaListSortTokensOpts.ResumeLayout(False)
        Me.gbTVGeneralMediaListSortTokensOpts.PerformLayout()
        Me.tblTVGeneralMediaListSortTokensOpts.ResumeLayout(False)
        Me.tblTVGeneralMediaListSortTokensOpts.PerformLayout()
        Me.gbTVEpisodeFilterOpts.ResumeLayout(False)
        Me.gbTVEpisodeFilterOpts.PerformLayout()
        Me.tblTVEpisodeFilterOpts.ResumeLayout(False)
        Me.tblTVEpisodeFilterOpts.PerformLayout()
        Me.gbTVShowFilterOpts.ResumeLayout(False)
        Me.gbTVShowFilterOpts.PerformLayout()
        Me.tblTVShowFilterOpts.ResumeLayout(False)
        Me.tblTVShowFilterOpts.PerformLayout()
        Me.gbTVGeneralMainWindowOpts.ResumeLayout(False)
        Me.gbTVGeneralMainWindowOpts.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblTVGeneral As TableLayoutPanel
    Friend WithEvents gbTVGeneralCustomScrapeButton As GroupBox
    Friend WithEvents tblTVGeneralCustomScrapeButton As TableLayoutPanel
    Friend WithEvents cbTVGeneralCustomScrapeButtonScrapeType As ComboBox
    Friend WithEvents cbTVGeneralCustomScrapeButtonModifierType As ComboBox
    Friend WithEvents txtTVGeneralCustomScrapeButtonScrapeType As Label
    Friend WithEvents txtTVGeneralCustomScrapeButtonModifierType As Label
    Friend WithEvents rbTVGeneralCustomScrapeButtonEnabled As RadioButton
    Friend WithEvents rbTVGeneralCustomScrapeButtonDisabled As RadioButton
    Friend WithEvents gbTVGeneralMiscOpts As GroupBox
    Friend WithEvents tblTVGeneralMiscOpts As TableLayoutPanel
    Friend WithEvents chkTVGeneralMarkNewEpisodes As CheckBox
    Friend WithEvents chkTVGeneralMarkNewShows As CheckBox
    Friend WithEvents chkTVGeneralClickScrape As CheckBox
    Friend WithEvents chkTVGeneralClickScrapeAsk As CheckBox
    Friend WithEvents gbTVGeneralMediaListOpts As GroupBox
    Friend WithEvents tblTVGeneralMediaListOpts As TableLayoutPanel
    Friend WithEvents gbTVGeneralEpisodeListSorting As GroupBox
    Friend WithEvents tblTVGeneralEpisodeListSorting As TableLayoutPanel
    Friend WithEvents btnTVGeneralEpisodeListSortingDown As Button
    Friend WithEvents btnTVGeneralEpisodeListSortingUp As Button
    Friend WithEvents lvTVGeneralEpisodeListSorting As ListView
    Friend WithEvents colTVGeneralEpisodeListSortingDisplayIndex As ColumnHeader
    Friend WithEvents colTVGeneralEpisodeListSortingColumn As ColumnHeader
    Friend WithEvents colTVGeneralEpisodeListSortingLabel As ColumnHeader
    Friend WithEvents colTVGeneralEpisodeListSortingHide As ColumnHeader
    Friend WithEvents btnTVGeneralEpisodeListSortingReset As Button
    Friend WithEvents gbTVGeneralSeasonListSortingOpts As GroupBox
    Friend WithEvents tblTVGeneralSeasonListSorting As TableLayoutPanel
    Friend WithEvents btnTVGeneralSeasonListSortingDown As Button
    Friend WithEvents btnTVGeneralSeasonListSortingUp As Button
    Friend WithEvents lvTVGeneralSeasonListSorting As ListView
    Friend WithEvents colTVGeneralSeasonListSortingDisplayIndex As ColumnHeader
    Friend WithEvents colTVGeneralSeasonListSortingColumn As ColumnHeader
    Friend WithEvents colTVGeneralSeasonListSortingLabel As ColumnHeader
    Friend WithEvents colTVGeneralSeasonListSortingHide As ColumnHeader
    Friend WithEvents btnTVGeneralSeasonListSortingReset As Button
    Friend WithEvents gbTVGeneralShowListSortingOpts As GroupBox
    Friend WithEvents tblTVGeneralShowListSorting As TableLayoutPanel
    Friend WithEvents btnTVGeneralShowListSortingDown As Button
    Friend WithEvents btnTVGeneralShowListSortingUp As Button
    Friend WithEvents lvTVGeneralShowListSorting As ListView
    Friend WithEvents colTVGeneralShowListSortingDisplayIndex As ColumnHeader
    Friend WithEvents colTVGeneralShowListSortingColumn As ColumnHeader
    Friend WithEvents colTVGeneralShowListSortingLabel As ColumnHeader
    Friend WithEvents colTVGeneralShowListSortingHide As ColumnHeader
    Friend WithEvents btnTVGeneralShowListSortingReset As Button
    Friend WithEvents chkTVDisplayMissingEpisodes As CheckBox
    Friend WithEvents gbTVEpisodeFilterOpts As GroupBox
    Friend WithEvents tblTVEpisodeFilterOpts As TableLayoutPanel
    Friend WithEvents btnTVEpisodeFilterRemove As Button
    Friend WithEvents btnTVEpisodeFilterDown As Button
    Friend WithEvents btnTVEpisodeFilterReset As Button
    Friend WithEvents btnTVEpisodeFilterUp As Button
    Friend WithEvents chkTVEpisodeNoFilter As CheckBox
    Friend WithEvents chkTVEpisodeProperCase As CheckBox
    Friend WithEvents btnTVEpisodeFilterAdd As Button
    Friend WithEvents lstTVEpisodeFilter As ListBox
    Friend WithEvents txtTVEpisodeFilter As TextBox
    Friend WithEvents gbTVShowFilterOpts As GroupBox
    Friend WithEvents tblTVShowFilterOpts As TableLayoutPanel
    Friend WithEvents btnTVShowFilterRemove As Button
    Friend WithEvents btnTVShowFilterDown As Button
    Friend WithEvents btnTVShowFilterReset As Button
    Friend WithEvents btnTVShowFilterUp As Button
    Friend WithEvents chkTVShowProperCase As CheckBox
    Friend WithEvents lstTVShowFilter As ListBox
    Friend WithEvents btnTVShowFilterAdd As Button
    Friend WithEvents txtTVShowFilter As TextBox
    Friend WithEvents gbTVGeneralMainWindowOpts As GroupBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents lblTVLanguageOverlay As Label
    Friend WithEvents cbTVLanguageOverlay As ComboBox
    Friend WithEvents gbTVGeneralMediaListSortTokensOpts As GroupBox
    Friend WithEvents tblTVGeneralMediaListSortTokensOpts As TableLayoutPanel
    Friend WithEvents btnTVSortTokenReset As Button
    Friend WithEvents btnTVSortTokenRemove As Button
    Friend WithEvents lstTVSortTokens As ListBox
    Friend WithEvents btnTVSortTokenAdd As Button
    Friend WithEvents txtTVSortToken As TextBox
End Class
