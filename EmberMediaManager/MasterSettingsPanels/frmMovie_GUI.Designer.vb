<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovie_GUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovie_GUI))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieGeneralCustomMarker = New System.Windows.Forms.GroupBox()
        Me.tblMovieGeneralCustomMarker = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieGeneralCustomMarker4 = New System.Windows.Forms.Button()
        Me.lblMovieGeneralCustomMarker1 = New System.Windows.Forms.Label()
        Me.btnMovieGeneralCustomMarker3 = New System.Windows.Forms.Button()
        Me.txtMovieGeneralCustomMarker4 = New System.Windows.Forms.TextBox()
        Me.btnMovieGeneralCustomMarker2 = New System.Windows.Forms.Button()
        Me.lblMovieGeneralCustomMarker2 = New System.Windows.Forms.Label()
        Me.btnMovieGeneralCustomMarker1 = New System.Windows.Forms.Button()
        Me.lblMovieGeneralCustomMarker4 = New System.Windows.Forms.Label()
        Me.txtMovieGeneralCustomMarker3 = New System.Windows.Forms.TextBox()
        Me.lblMovieGeneralCustomMarker3 = New System.Windows.Forms.Label()
        Me.txtMovieGeneralCustomMarker1 = New System.Windows.Forms.TextBox()
        Me.txtMovieGeneralCustomMarker2 = New System.Windows.Forms.TextBox()
        Me.gbMovieGeneralMiscOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieGeneralMiscOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.chkMovieClickScrapeAsk = New System.Windows.Forms.CheckBox()
        Me.chkMovieClickScrape = New System.Windows.Forms.CheckBox()
        Me.gbMovieGeneralMainWindowOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieGeneralMainWindow = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMovieLanguageOverlay = New System.Windows.Forms.Label()
        Me.cbMovieLanguageOverlay = New System.Windows.Forms.ComboBox()
        Me.gbMovieGeneralCustomScrapeButton = New System.Windows.Forms.GroupBox()
        Me.tblMovieGeneralCustomScrapeButton = New System.Windows.Forms.TableLayoutPanel()
        Me.cbMovieGeneralCustomScrapeButtonScrapeType = New System.Windows.Forms.ComboBox()
        Me.cbMovieGeneralCustomScrapeButtonModifierType = New System.Windows.Forms.ComboBox()
        Me.txtMovieGeneralCustomScrapeButtonScrapeType = New System.Windows.Forms.Label()
        Me.txtMovieGeneralCustomScrapeButtonModifierType = New System.Windows.Forms.Label()
        Me.rbMovieGeneralCustomScrapeButtonEnabled = New System.Windows.Forms.RadioButton()
        Me.rbMovieGeneralCustomScrapeButtonDisabled = New System.Windows.Forms.RadioButton()
        Me.gbMovieGeneralMediaListOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieGeneralMediaListOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.gbMovieGeneralMediaListSortTokensOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieGeneralSortTokensOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieSortTokenReset = New System.Windows.Forms.Button()
        Me.btnMovieSortTokenRemove = New System.Windows.Forms.Button()
        Me.lstMovieSortTokens = New System.Windows.Forms.ListBox()
        Me.btnMovieSortTokenAdd = New System.Windows.Forms.Button()
        Me.txtMovieSortToken = New System.Windows.Forms.TextBox()
        Me.chkMovieLevTolerance = New System.Windows.Forms.CheckBox()
        Me.lblMovieLevTolerance = New System.Windows.Forms.Label()
        Me.txtMovieLevTolerance = New System.Windows.Forms.TextBox()
        Me.gbMovieGeneralMediaListSorting = New System.Windows.Forms.GroupBox()
        Me.tblMovieGeneralMediaListSorting = New System.Windows.Forms.TableLayoutPanel()
        Me.btnMovieGeneralMediaListSortingDown = New System.Windows.Forms.Button()
        Me.btnMovieGeneralMediaListSortingUp = New System.Windows.Forms.Button()
        Me.lvMovieGeneralMediaListSorting = New System.Windows.Forms.ListView()
        Me.colMovieGeneralMediaListSortingDisplayIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieGeneralMediaListSortingColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieGeneralMediaListSortingLabel = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colMovieGeneralMediaListSortingHide = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnMovieGeneralMediaListSortingReset = New System.Windows.Forms.Button()
        Me.cdColor = New System.Windows.Forms.ColorDialog()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMovieGeneralCustomMarker.SuspendLayout()
        Me.tblMovieGeneralCustomMarker.SuspendLayout()
        Me.gbMovieGeneralMiscOpts.SuspendLayout()
        Me.tblMovieGeneralMiscOpts.SuspendLayout()
        Me.gbMovieGeneralMainWindowOpts.SuspendLayout()
        Me.tblMovieGeneralMainWindow.SuspendLayout()
        Me.gbMovieGeneralCustomScrapeButton.SuspendLayout()
        Me.tblMovieGeneralCustomScrapeButton.SuspendLayout()
        Me.gbMovieGeneralMediaListOpts.SuspendLayout()
        Me.tblMovieGeneralMediaListOpts.SuspendLayout()
        Me.gbMovieGeneralMediaListSortTokensOpts.SuspendLayout()
        Me.tblMovieGeneralSortTokensOpts.SuspendLayout()
        Me.gbMovieGeneralMediaListSorting.SuspendLayout()
        Me.tblMovieGeneralMediaListSorting.SuspendLayout()
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
        Me.pnlSettings.Size = New System.Drawing.Size(780, 562)
        Me.pnlSettings.TabIndex = 16
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
        Me.tblSettings.Controls.Add(Me.gbMovieGeneralCustomMarker, 1, 3)
        Me.tblSettings.Controls.Add(Me.gbMovieGeneralMiscOpts, 1, 1)
        Me.tblSettings.Controls.Add(Me.gbMovieGeneralMainWindowOpts, 1, 0)
        Me.tblSettings.Controls.Add(Me.gbMovieGeneralCustomScrapeButton, 1, 2)
        Me.tblSettings.Controls.Add(Me.gbMovieGeneralMediaListOpts, 0, 0)
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
        Me.tblSettings.Size = New System.Drawing.Size(780, 562)
        Me.tblSettings.TabIndex = 10
        '
        'gbMovieGeneralCustomMarker
        '
        Me.gbMovieGeneralCustomMarker.AutoSize = True
        Me.gbMovieGeneralCustomMarker.Controls.Add(Me.tblMovieGeneralCustomMarker)
        Me.gbMovieGeneralCustomMarker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieGeneralCustomMarker.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieGeneralCustomMarker.Location = New System.Drawing.Point(233, 234)
        Me.gbMovieGeneralCustomMarker.Name = "gbMovieGeneralCustomMarker"
        Me.gbMovieGeneralCustomMarker.Size = New System.Drawing.Size(497, 133)
        Me.gbMovieGeneralCustomMarker.TabIndex = 9
        Me.gbMovieGeneralCustomMarker.TabStop = False
        Me.gbMovieGeneralCustomMarker.Text = "Custom Marker"
        '
        'tblMovieGeneralCustomMarker
        '
        Me.tblMovieGeneralCustomMarker.AutoSize = True
        Me.tblMovieGeneralCustomMarker.ColumnCount = 4
        Me.tblMovieGeneralCustomMarker.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralCustomMarker.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMovieGeneralCustomMarker.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralCustomMarker.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.btnMovieGeneralCustomMarker4, 2, 3)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.lblMovieGeneralCustomMarker1, 0, 0)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.btnMovieGeneralCustomMarker3, 2, 2)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.txtMovieGeneralCustomMarker4, 1, 3)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.btnMovieGeneralCustomMarker2, 2, 1)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.lblMovieGeneralCustomMarker2, 0, 1)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.btnMovieGeneralCustomMarker1, 2, 0)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.lblMovieGeneralCustomMarker4, 0, 3)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.txtMovieGeneralCustomMarker3, 1, 2)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.lblMovieGeneralCustomMarker3, 0, 2)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.txtMovieGeneralCustomMarker1, 1, 0)
        Me.tblMovieGeneralCustomMarker.Controls.Add(Me.txtMovieGeneralCustomMarker2, 1, 1)
        Me.tblMovieGeneralCustomMarker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralCustomMarker.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralCustomMarker.Name = "tblMovieGeneralCustomMarker"
        Me.tblMovieGeneralCustomMarker.RowCount = 5
        Me.tblMovieGeneralCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomMarker.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomMarker.Size = New System.Drawing.Size(491, 112)
        Me.tblMovieGeneralCustomMarker.TabIndex = 10
        '
        'btnMovieGeneralCustomMarker4
        '
        Me.btnMovieGeneralCustomMarker4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieGeneralCustomMarker4.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnMovieGeneralCustomMarker4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMovieGeneralCustomMarker4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieGeneralCustomMarker4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMovieGeneralCustomMarker4.Location = New System.Drawing.Point(464, 87)
        Me.btnMovieGeneralCustomMarker4.Name = "btnMovieGeneralCustomMarker4"
        Me.btnMovieGeneralCustomMarker4.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieGeneralCustomMarker4.TabIndex = 24
        Me.btnMovieGeneralCustomMarker4.UseVisualStyleBackColor = False
        '
        'lblMovieGeneralCustomMarker1
        '
        Me.lblMovieGeneralCustomMarker1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieGeneralCustomMarker1.AutoSize = True
        Me.lblMovieGeneralCustomMarker1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieGeneralCustomMarker1.Location = New System.Drawing.Point(3, 7)
        Me.lblMovieGeneralCustomMarker1.Name = "lblMovieGeneralCustomMarker1"
        Me.lblMovieGeneralCustomMarker1.Size = New System.Drawing.Size(55, 13)
        Me.lblMovieGeneralCustomMarker1.TabIndex = 0
        Me.lblMovieGeneralCustomMarker1.Text = "Custom 1"
        '
        'btnMovieGeneralCustomMarker3
        '
        Me.btnMovieGeneralCustomMarker3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieGeneralCustomMarker3.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnMovieGeneralCustomMarker3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMovieGeneralCustomMarker3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieGeneralCustomMarker3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMovieGeneralCustomMarker3.Location = New System.Drawing.Point(464, 59)
        Me.btnMovieGeneralCustomMarker3.Name = "btnMovieGeneralCustomMarker3"
        Me.btnMovieGeneralCustomMarker3.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieGeneralCustomMarker3.TabIndex = 21
        Me.btnMovieGeneralCustomMarker3.UseVisualStyleBackColor = False
        '
        'txtMovieGeneralCustomMarker4
        '
        Me.txtMovieGeneralCustomMarker4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMovieGeneralCustomMarker4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieGeneralCustomMarker4.Location = New System.Drawing.Point(64, 87)
        Me.txtMovieGeneralCustomMarker4.Name = "txtMovieGeneralCustomMarker4"
        Me.txtMovieGeneralCustomMarker4.Size = New System.Drawing.Size(394, 22)
        Me.txtMovieGeneralCustomMarker4.TabIndex = 23
        '
        'btnMovieGeneralCustomMarker2
        '
        Me.btnMovieGeneralCustomMarker2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieGeneralCustomMarker2.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnMovieGeneralCustomMarker2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMovieGeneralCustomMarker2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieGeneralCustomMarker2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMovieGeneralCustomMarker2.Location = New System.Drawing.Point(464, 31)
        Me.btnMovieGeneralCustomMarker2.Name = "btnMovieGeneralCustomMarker2"
        Me.btnMovieGeneralCustomMarker2.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieGeneralCustomMarker2.TabIndex = 18
        Me.btnMovieGeneralCustomMarker2.UseVisualStyleBackColor = False
        '
        'lblMovieGeneralCustomMarker2
        '
        Me.lblMovieGeneralCustomMarker2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieGeneralCustomMarker2.AutoSize = True
        Me.lblMovieGeneralCustomMarker2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieGeneralCustomMarker2.Location = New System.Drawing.Point(3, 35)
        Me.lblMovieGeneralCustomMarker2.Name = "lblMovieGeneralCustomMarker2"
        Me.lblMovieGeneralCustomMarker2.Size = New System.Drawing.Size(55, 13)
        Me.lblMovieGeneralCustomMarker2.TabIndex = 16
        Me.lblMovieGeneralCustomMarker2.Text = "Custom 2"
        '
        'btnMovieGeneralCustomMarker1
        '
        Me.btnMovieGeneralCustomMarker1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieGeneralCustomMarker1.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.btnMovieGeneralCustomMarker1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMovieGeneralCustomMarker1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMovieGeneralCustomMarker1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnMovieGeneralCustomMarker1.Location = New System.Drawing.Point(464, 3)
        Me.btnMovieGeneralCustomMarker1.Name = "btnMovieGeneralCustomMarker1"
        Me.btnMovieGeneralCustomMarker1.Size = New System.Drawing.Size(24, 22)
        Me.btnMovieGeneralCustomMarker1.TabIndex = 15
        Me.btnMovieGeneralCustomMarker1.UseVisualStyleBackColor = False
        '
        'lblMovieGeneralCustomMarker4
        '
        Me.lblMovieGeneralCustomMarker4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieGeneralCustomMarker4.AutoSize = True
        Me.lblMovieGeneralCustomMarker4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieGeneralCustomMarker4.Location = New System.Drawing.Point(3, 91)
        Me.lblMovieGeneralCustomMarker4.Name = "lblMovieGeneralCustomMarker4"
        Me.lblMovieGeneralCustomMarker4.Size = New System.Drawing.Size(55, 13)
        Me.lblMovieGeneralCustomMarker4.TabIndex = 22
        Me.lblMovieGeneralCustomMarker4.Text = "Custom 4"
        '
        'txtMovieGeneralCustomMarker3
        '
        Me.txtMovieGeneralCustomMarker3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMovieGeneralCustomMarker3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieGeneralCustomMarker3.Location = New System.Drawing.Point(64, 59)
        Me.txtMovieGeneralCustomMarker3.Name = "txtMovieGeneralCustomMarker3"
        Me.txtMovieGeneralCustomMarker3.Size = New System.Drawing.Size(394, 22)
        Me.txtMovieGeneralCustomMarker3.TabIndex = 20
        '
        'lblMovieGeneralCustomMarker3
        '
        Me.lblMovieGeneralCustomMarker3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieGeneralCustomMarker3.AutoSize = True
        Me.lblMovieGeneralCustomMarker3.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieGeneralCustomMarker3.Location = New System.Drawing.Point(3, 63)
        Me.lblMovieGeneralCustomMarker3.Name = "lblMovieGeneralCustomMarker3"
        Me.lblMovieGeneralCustomMarker3.Size = New System.Drawing.Size(55, 13)
        Me.lblMovieGeneralCustomMarker3.TabIndex = 19
        Me.lblMovieGeneralCustomMarker3.Text = "Custom 3"
        '
        'txtMovieGeneralCustomMarker1
        '
        Me.txtMovieGeneralCustomMarker1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMovieGeneralCustomMarker1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieGeneralCustomMarker1.Location = New System.Drawing.Point(64, 3)
        Me.txtMovieGeneralCustomMarker1.Name = "txtMovieGeneralCustomMarker1"
        Me.txtMovieGeneralCustomMarker1.Size = New System.Drawing.Size(394, 22)
        Me.txtMovieGeneralCustomMarker1.TabIndex = 1
        '
        'txtMovieGeneralCustomMarker2
        '
        Me.txtMovieGeneralCustomMarker2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMovieGeneralCustomMarker2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieGeneralCustomMarker2.Location = New System.Drawing.Point(64, 31)
        Me.txtMovieGeneralCustomMarker2.Name = "txtMovieGeneralCustomMarker2"
        Me.txtMovieGeneralCustomMarker2.Size = New System.Drawing.Size(394, 22)
        Me.txtMovieGeneralCustomMarker2.TabIndex = 17
        '
        'gbMovieGeneralMiscOpts
        '
        Me.gbMovieGeneralMiscOpts.AutoSize = True
        Me.gbMovieGeneralMiscOpts.Controls.Add(Me.tblMovieGeneralMiscOpts)
        Me.gbMovieGeneralMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieGeneralMiscOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieGeneralMiscOpts.Location = New System.Drawing.Point(233, 57)
        Me.gbMovieGeneralMiscOpts.Name = "gbMovieGeneralMiscOpts"
        Me.gbMovieGeneralMiscOpts.Size = New System.Drawing.Size(497, 67)
        Me.gbMovieGeneralMiscOpts.TabIndex = 1
        Me.gbMovieGeneralMiscOpts.TabStop = False
        Me.gbMovieGeneralMiscOpts.Text = "Miscellaneous"
        '
        'tblMovieGeneralMiscOpts
        '
        Me.tblMovieGeneralMiscOpts.AutoSize = True
        Me.tblMovieGeneralMiscOpts.ColumnCount = 2
        Me.tblMovieGeneralMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMiscOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMiscOpts.Controls.Add(Me.chkMovieClickScrapeAsk, 0, 2)
        Me.tblMovieGeneralMiscOpts.Controls.Add(Me.chkMovieClickScrape, 0, 1)
        Me.tblMovieGeneralMiscOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralMiscOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralMiscOpts.Name = "tblMovieGeneralMiscOpts"
        Me.tblMovieGeneralMiscOpts.RowCount = 4
        Me.tblMovieGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMiscOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMiscOpts.Size = New System.Drawing.Size(491, 46)
        Me.tblMovieGeneralMiscOpts.TabIndex = 10
        '
        'chkMovieClickScrapeAsk
        '
        Me.chkMovieClickScrapeAsk.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieClickScrapeAsk.AutoSize = True
        Me.chkMovieClickScrapeAsk.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMovieClickScrapeAsk.Location = New System.Drawing.Point(3, 26)
        Me.chkMovieClickScrapeAsk.Name = "chkMovieClickScrapeAsk"
        Me.chkMovieClickScrapeAsk.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.chkMovieClickScrapeAsk.Size = New System.Drawing.Size(152, 17)
        Me.chkMovieClickScrapeAsk.TabIndex = 64
        Me.chkMovieClickScrapeAsk.Text = "Show Results Dialog"
        Me.chkMovieClickScrapeAsk.UseVisualStyleBackColor = True
        '
        'chkMovieClickScrape
        '
        Me.chkMovieClickScrape.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieClickScrape.AutoSize = True
        Me.chkMovieClickScrape.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.chkMovieClickScrape.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieClickScrape.Name = "chkMovieClickScrape"
        Me.chkMovieClickScrape.Size = New System.Drawing.Size(126, 17)
        Me.chkMovieClickScrape.TabIndex = 65
        Me.chkMovieClickScrape.Text = "Enable Click-Scrape"
        Me.chkMovieClickScrape.UseVisualStyleBackColor = True
        '
        'gbMovieGeneralMainWindowOpts
        '
        Me.gbMovieGeneralMainWindowOpts.AutoSize = True
        Me.gbMovieGeneralMainWindowOpts.Controls.Add(Me.tblMovieGeneralMainWindow)
        Me.gbMovieGeneralMainWindowOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieGeneralMainWindowOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieGeneralMainWindowOpts.Location = New System.Drawing.Point(233, 3)
        Me.gbMovieGeneralMainWindowOpts.Name = "gbMovieGeneralMainWindowOpts"
        Me.gbMovieGeneralMainWindowOpts.Size = New System.Drawing.Size(497, 48)
        Me.gbMovieGeneralMainWindowOpts.TabIndex = 10
        Me.gbMovieGeneralMainWindowOpts.TabStop = False
        Me.gbMovieGeneralMainWindowOpts.Text = "Main Window"
        '
        'tblMovieGeneralMainWindow
        '
        Me.tblMovieGeneralMainWindow.AutoSize = True
        Me.tblMovieGeneralMainWindow.ColumnCount = 2
        Me.tblMovieGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblMovieGeneralMainWindow.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMainWindow.Controls.Add(Me.lblMovieLanguageOverlay, 0, 0)
        Me.tblMovieGeneralMainWindow.Controls.Add(Me.cbMovieLanguageOverlay, 1, 0)
        Me.tblMovieGeneralMainWindow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralMainWindow.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralMainWindow.Name = "tblMovieGeneralMainWindow"
        Me.tblMovieGeneralMainWindow.RowCount = 1
        Me.tblMovieGeneralMainWindow.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMainWindow.Size = New System.Drawing.Size(491, 27)
        Me.tblMovieGeneralMainWindow.TabIndex = 0
        '
        'lblMovieLanguageOverlay
        '
        Me.lblMovieLanguageOverlay.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieLanguageOverlay.AutoSize = True
        Me.lblMovieLanguageOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieLanguageOverlay.Location = New System.Drawing.Point(3, 0)
        Me.lblMovieLanguageOverlay.MaximumSize = New System.Drawing.Size(250, 0)
        Me.lblMovieLanguageOverlay.Name = "lblMovieLanguageOverlay"
        Me.lblMovieLanguageOverlay.Size = New System.Drawing.Size(243, 26)
        Me.lblMovieLanguageOverlay.TabIndex = 16
        Me.lblMovieLanguageOverlay.Text = "Display best Audio Stream with the following Language:"
        '
        'cbMovieLanguageOverlay
        '
        Me.cbMovieLanguageOverlay.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbMovieLanguageOverlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieLanguageOverlay.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbMovieLanguageOverlay.FormattingEnabled = True
        Me.cbMovieLanguageOverlay.Location = New System.Drawing.Point(314, 3)
        Me.cbMovieLanguageOverlay.Name = "cbMovieLanguageOverlay"
        Me.cbMovieLanguageOverlay.Size = New System.Drawing.Size(174, 21)
        Me.cbMovieLanguageOverlay.Sorted = True
        Me.cbMovieLanguageOverlay.TabIndex = 17
        '
        'gbMovieGeneralCustomScrapeButton
        '
        Me.gbMovieGeneralCustomScrapeButton.AutoSize = True
        Me.gbMovieGeneralCustomScrapeButton.Controls.Add(Me.tblMovieGeneralCustomScrapeButton)
        Me.gbMovieGeneralCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieGeneralCustomScrapeButton.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbMovieGeneralCustomScrapeButton.Location = New System.Drawing.Point(233, 130)
        Me.gbMovieGeneralCustomScrapeButton.Name = "gbMovieGeneralCustomScrapeButton"
        Me.gbMovieGeneralCustomScrapeButton.Size = New System.Drawing.Size(497, 98)
        Me.gbMovieGeneralCustomScrapeButton.TabIndex = 11
        Me.gbMovieGeneralCustomScrapeButton.TabStop = False
        Me.gbMovieGeneralCustomScrapeButton.Text = "Scrape Button"
        '
        'tblMovieGeneralCustomScrapeButton
        '
        Me.tblMovieGeneralCustomScrapeButton.AutoSize = True
        Me.tblMovieGeneralCustomScrapeButton.ColumnCount = 2
        Me.tblMovieGeneralCustomScrapeButton.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralCustomScrapeButton.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tblMovieGeneralCustomScrapeButton.Controls.Add(Me.cbMovieGeneralCustomScrapeButtonScrapeType, 1, 1)
        Me.tblMovieGeneralCustomScrapeButton.Controls.Add(Me.cbMovieGeneralCustomScrapeButtonModifierType, 1, 2)
        Me.tblMovieGeneralCustomScrapeButton.Controls.Add(Me.txtMovieGeneralCustomScrapeButtonScrapeType, 0, 1)
        Me.tblMovieGeneralCustomScrapeButton.Controls.Add(Me.txtMovieGeneralCustomScrapeButtonModifierType, 0, 2)
        Me.tblMovieGeneralCustomScrapeButton.Controls.Add(Me.rbMovieGeneralCustomScrapeButtonEnabled, 1, 0)
        Me.tblMovieGeneralCustomScrapeButton.Controls.Add(Me.rbMovieGeneralCustomScrapeButtonDisabled, 0, 0)
        Me.tblMovieGeneralCustomScrapeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralCustomScrapeButton.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralCustomScrapeButton.Name = "tblMovieGeneralCustomScrapeButton"
        Me.tblMovieGeneralCustomScrapeButton.RowCount = 4
        Me.tblMovieGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomScrapeButton.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralCustomScrapeButton.Size = New System.Drawing.Size(491, 77)
        Me.tblMovieGeneralCustomScrapeButton.TabIndex = 0
        '
        'cbMovieGeneralCustomScrapeButtonScrapeType
        '
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.Enabled = False
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.FormattingEnabled = True
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.Location = New System.Drawing.Point(159, 26)
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.Name = "cbMovieGeneralCustomScrapeButtonScrapeType"
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(329, 21)
        Me.cbMovieGeneralCustomScrapeButtonScrapeType.TabIndex = 1
        '
        'cbMovieGeneralCustomScrapeButtonModifierType
        '
        Me.cbMovieGeneralCustomScrapeButtonModifierType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbMovieGeneralCustomScrapeButtonModifierType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieGeneralCustomScrapeButtonModifierType.Enabled = False
        Me.cbMovieGeneralCustomScrapeButtonModifierType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbMovieGeneralCustomScrapeButtonModifierType.FormattingEnabled = True
        Me.cbMovieGeneralCustomScrapeButtonModifierType.Location = New System.Drawing.Point(159, 53)
        Me.cbMovieGeneralCustomScrapeButtonModifierType.Name = "cbMovieGeneralCustomScrapeButtonModifierType"
        Me.cbMovieGeneralCustomScrapeButtonModifierType.Size = New System.Drawing.Size(329, 21)
        Me.cbMovieGeneralCustomScrapeButtonModifierType.TabIndex = 2
        '
        'txtMovieGeneralCustomScrapeButtonScrapeType
        '
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.AutoSize = True
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.Enabled = False
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.Location = New System.Drawing.Point(3, 30)
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.Name = "txtMovieGeneralCustomScrapeButtonScrapeType"
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.Size = New System.Drawing.Size(66, 13)
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.TabIndex = 3
        Me.txtMovieGeneralCustomScrapeButtonScrapeType.Text = "Scrape Type"
        '
        'txtMovieGeneralCustomScrapeButtonModifierType
        '
        Me.txtMovieGeneralCustomScrapeButtonModifierType.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieGeneralCustomScrapeButtonModifierType.AutoSize = True
        Me.txtMovieGeneralCustomScrapeButtonModifierType.Enabled = False
        Me.txtMovieGeneralCustomScrapeButtonModifierType.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieGeneralCustomScrapeButtonModifierType.Location = New System.Drawing.Point(3, 57)
        Me.txtMovieGeneralCustomScrapeButtonModifierType.Name = "txtMovieGeneralCustomScrapeButtonModifierType"
        Me.txtMovieGeneralCustomScrapeButtonModifierType.Size = New System.Drawing.Size(76, 13)
        Me.txtMovieGeneralCustomScrapeButtonModifierType.TabIndex = 4
        Me.txtMovieGeneralCustomScrapeButtonModifierType.Text = "Modifier Type"
        '
        'rbMovieGeneralCustomScrapeButtonEnabled
        '
        Me.rbMovieGeneralCustomScrapeButtonEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbMovieGeneralCustomScrapeButtonEnabled.AutoSize = True
        Me.rbMovieGeneralCustomScrapeButtonEnabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMovieGeneralCustomScrapeButtonEnabled.Location = New System.Drawing.Point(159, 3)
        Me.rbMovieGeneralCustomScrapeButtonEnabled.Name = "rbMovieGeneralCustomScrapeButtonEnabled"
        Me.rbMovieGeneralCustomScrapeButtonEnabled.Size = New System.Drawing.Size(150, 17)
        Me.rbMovieGeneralCustomScrapeButtonEnabled.TabIndex = 5
        Me.rbMovieGeneralCustomScrapeButtonEnabled.TabStop = True
        Me.rbMovieGeneralCustomScrapeButtonEnabled.Text = "Custom Scrape Function"
        Me.rbMovieGeneralCustomScrapeButtonEnabled.UseVisualStyleBackColor = True
        '
        'rbMovieGeneralCustomScrapeButtonDisabled
        '
        Me.rbMovieGeneralCustomScrapeButtonDisabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbMovieGeneralCustomScrapeButtonDisabled.AutoSize = True
        Me.rbMovieGeneralCustomScrapeButtonDisabled.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMovieGeneralCustomScrapeButtonDisabled.Location = New System.Drawing.Point(3, 3)
        Me.rbMovieGeneralCustomScrapeButtonDisabled.Name = "rbMovieGeneralCustomScrapeButtonDisabled"
        Me.rbMovieGeneralCustomScrapeButtonDisabled.Size = New System.Drawing.Size(150, 17)
        Me.rbMovieGeneralCustomScrapeButtonDisabled.TabIndex = 6
        Me.rbMovieGeneralCustomScrapeButtonDisabled.TabStop = True
        Me.rbMovieGeneralCustomScrapeButtonDisabled.Text = "Open Drop Down Menu"
        Me.rbMovieGeneralCustomScrapeButtonDisabled.UseVisualStyleBackColor = True
        '
        'gbMovieGeneralMediaListOpts
        '
        Me.gbMovieGeneralMediaListOpts.AutoSize = True
        Me.gbMovieGeneralMediaListOpts.Controls.Add(Me.tblMovieGeneralMediaListOpts)
        Me.gbMovieGeneralMediaListOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieGeneralMediaListOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieGeneralMediaListOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieGeneralMediaListOpts.Name = "gbMovieGeneralMediaListOpts"
        Me.tblSettings.SetRowSpan(Me.gbMovieGeneralMediaListOpts, 5)
        Me.gbMovieGeneralMediaListOpts.Size = New System.Drawing.Size(224, 516)
        Me.gbMovieGeneralMediaListOpts.TabIndex = 4
        Me.gbMovieGeneralMediaListOpts.TabStop = False
        Me.gbMovieGeneralMediaListOpts.Text = "Media List Options"
        '
        'tblMovieGeneralMediaListOpts
        '
        Me.tblMovieGeneralMediaListOpts.AutoSize = True
        Me.tblMovieGeneralMediaListOpts.ColumnCount = 3
        Me.tblMovieGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListOpts.Controls.Add(Me.gbMovieGeneralMediaListSortTokensOpts, 0, 2)
        Me.tblMovieGeneralMediaListOpts.Controls.Add(Me.chkMovieLevTolerance, 0, 0)
        Me.tblMovieGeneralMediaListOpts.Controls.Add(Me.lblMovieLevTolerance, 0, 1)
        Me.tblMovieGeneralMediaListOpts.Controls.Add(Me.txtMovieLevTolerance, 1, 1)
        Me.tblMovieGeneralMediaListOpts.Controls.Add(Me.gbMovieGeneralMediaListSorting, 0, 3)
        Me.tblMovieGeneralMediaListOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralMediaListOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralMediaListOpts.Name = "tblMovieGeneralMediaListOpts"
        Me.tblMovieGeneralMediaListOpts.RowCount = 5
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieGeneralMediaListOpts.Size = New System.Drawing.Size(218, 495)
        Me.tblMovieGeneralMediaListOpts.TabIndex = 10
        '
        'gbMovieGeneralMediaListSortTokensOpts
        '
        Me.gbMovieGeneralMediaListSortTokensOpts.AutoSize = True
        Me.tblMovieGeneralMediaListOpts.SetColumnSpan(Me.gbMovieGeneralMediaListSortTokensOpts, 2)
        Me.gbMovieGeneralMediaListSortTokensOpts.Controls.Add(Me.tblMovieGeneralSortTokensOpts)
        Me.gbMovieGeneralMediaListSortTokensOpts.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.gbMovieGeneralMediaListSortTokensOpts.Location = New System.Drawing.Point(3, 54)
        Me.gbMovieGeneralMediaListSortTokensOpts.Name = "gbMovieGeneralMediaListSortTokensOpts"
        Me.gbMovieGeneralMediaListSortTokensOpts.Size = New System.Drawing.Size(212, 126)
        Me.gbMovieGeneralMediaListSortTokensOpts.TabIndex = 71
        Me.gbMovieGeneralMediaListSortTokensOpts.TabStop = False
        Me.gbMovieGeneralMediaListSortTokensOpts.Text = "Sort Tokens to Ignore"
        '
        'tblMovieGeneralSortTokensOpts
        '
        Me.tblMovieGeneralSortTokensOpts.AutoSize = True
        Me.tblMovieGeneralSortTokensOpts.ColumnCount = 5
        Me.tblMovieGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralSortTokensOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralSortTokensOpts.Controls.Add(Me.btnMovieSortTokenReset, 3, 1)
        Me.tblMovieGeneralSortTokensOpts.Controls.Add(Me.btnMovieSortTokenRemove, 2, 1)
        Me.tblMovieGeneralSortTokensOpts.Controls.Add(Me.lstMovieSortTokens, 0, 0)
        Me.tblMovieGeneralSortTokensOpts.Controls.Add(Me.btnMovieSortTokenAdd, 1, 1)
        Me.tblMovieGeneralSortTokensOpts.Controls.Add(Me.txtMovieSortToken, 0, 1)
        Me.tblMovieGeneralSortTokensOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralSortTokensOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralSortTokensOpts.Name = "tblMovieGeneralSortTokensOpts"
        Me.tblMovieGeneralSortTokensOpts.RowCount = 3
        Me.tblMovieGeneralSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralSortTokensOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralSortTokensOpts.Size = New System.Drawing.Size(206, 105)
        Me.tblMovieGeneralSortTokensOpts.TabIndex = 11
        '
        'btnMovieSortTokenReset
        '
        Me.btnMovieSortTokenReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieSortTokenReset.Image = CType(resources.GetObject("btnMovieSortTokenReset.Image"), System.Drawing.Image)
        Me.btnMovieSortTokenReset.Location = New System.Drawing.Point(180, 79)
        Me.btnMovieSortTokenReset.Name = "btnMovieSortTokenReset"
        Me.btnMovieSortTokenReset.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSortTokenReset.TabIndex = 9
        Me.btnMovieSortTokenReset.UseVisualStyleBackColor = True
        '
        'btnMovieSortTokenRemove
        '
        Me.btnMovieSortTokenRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieSortTokenRemove.Image = CType(resources.GetObject("btnMovieSortTokenRemove.Image"), System.Drawing.Image)
        Me.btnMovieSortTokenRemove.Location = New System.Drawing.Point(99, 79)
        Me.btnMovieSortTokenRemove.Name = "btnMovieSortTokenRemove"
        Me.btnMovieSortTokenRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSortTokenRemove.TabIndex = 3
        Me.btnMovieSortTokenRemove.UseVisualStyleBackColor = True
        '
        'lstMovieSortTokens
        '
        Me.tblMovieGeneralSortTokensOpts.SetColumnSpan(Me.lstMovieSortTokens, 4)
        Me.lstMovieSortTokens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMovieSortTokens.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lstMovieSortTokens.FormattingEnabled = True
        Me.lstMovieSortTokens.Location = New System.Drawing.Point(3, 3)
        Me.lstMovieSortTokens.Name = "lstMovieSortTokens"
        Me.lstMovieSortTokens.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstMovieSortTokens.Size = New System.Drawing.Size(200, 70)
        Me.lstMovieSortTokens.Sorted = True
        Me.lstMovieSortTokens.TabIndex = 0
        '
        'btnMovieSortTokenAdd
        '
        Me.btnMovieSortTokenAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMovieSortTokenAdd.Image = CType(resources.GetObject("btnMovieSortTokenAdd.Image"), System.Drawing.Image)
        Me.btnMovieSortTokenAdd.Location = New System.Drawing.Point(70, 79)
        Me.btnMovieSortTokenAdd.Name = "btnMovieSortTokenAdd"
        Me.btnMovieSortTokenAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieSortTokenAdd.TabIndex = 2
        Me.btnMovieSortTokenAdd.UseVisualStyleBackColor = True
        '
        'txtMovieSortToken
        '
        Me.txtMovieSortToken.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieSortToken.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieSortToken.Location = New System.Drawing.Point(3, 79)
        Me.txtMovieSortToken.Name = "txtMovieSortToken"
        Me.txtMovieSortToken.Size = New System.Drawing.Size(61, 22)
        Me.txtMovieSortToken.TabIndex = 1
        '
        'chkMovieLevTolerance
        '
        Me.chkMovieLevTolerance.AutoSize = True
        Me.tblMovieGeneralMediaListOpts.SetColumnSpan(Me.chkMovieLevTolerance, 3)
        Me.chkMovieLevTolerance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieLevTolerance.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieLevTolerance.Name = "chkMovieLevTolerance"
        Me.chkMovieLevTolerance.Size = New System.Drawing.Size(178, 17)
        Me.chkMovieLevTolerance.TabIndex = 72
        Me.chkMovieLevTolerance.Text = "Check Title Match Confidence"
        Me.chkMovieLevTolerance.UseVisualStyleBackColor = True
        '
        'lblMovieLevTolerance
        '
        Me.lblMovieLevTolerance.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblMovieLevTolerance.AutoSize = True
        Me.lblMovieLevTolerance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieLevTolerance.Location = New System.Drawing.Point(3, 30)
        Me.lblMovieLevTolerance.Name = "lblMovieLevTolerance"
        Me.lblMovieLevTolerance.Size = New System.Drawing.Size(110, 13)
        Me.lblMovieLevTolerance.TabIndex = 73
        Me.lblMovieLevTolerance.Text = "Mismatch Tolerance:"
        Me.lblMovieLevTolerance.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtMovieLevTolerance
        '
        Me.txtMovieLevTolerance.Enabled = False
        Me.txtMovieLevTolerance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMovieLevTolerance.Location = New System.Drawing.Point(119, 26)
        Me.txtMovieLevTolerance.Name = "txtMovieLevTolerance"
        Me.txtMovieLevTolerance.Size = New System.Drawing.Size(40, 22)
        Me.txtMovieLevTolerance.TabIndex = 74
        '
        'gbMovieGeneralMediaListSorting
        '
        Me.gbMovieGeneralMediaListSorting.AutoSize = True
        Me.tblMovieGeneralMediaListOpts.SetColumnSpan(Me.gbMovieGeneralMediaListSorting, 2)
        Me.gbMovieGeneralMediaListSorting.Controls.Add(Me.tblMovieGeneralMediaListSorting)
        Me.gbMovieGeneralMediaListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbMovieGeneralMediaListSorting.Location = New System.Drawing.Point(3, 186)
        Me.gbMovieGeneralMediaListSorting.Name = "gbMovieGeneralMediaListSorting"
        Me.gbMovieGeneralMediaListSorting.Size = New System.Drawing.Size(212, 306)
        Me.gbMovieGeneralMediaListSorting.TabIndex = 14
        Me.gbMovieGeneralMediaListSorting.TabStop = False
        Me.gbMovieGeneralMediaListSorting.Text = "Movie List Sorting"
        '
        'tblMovieGeneralMediaListSorting
        '
        Me.tblMovieGeneralMediaListSorting.AutoSize = True
        Me.tblMovieGeneralMediaListSorting.ColumnCount = 6
        Me.tblMovieGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListSorting.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieGeneralMediaListSorting.Controls.Add(Me.btnMovieGeneralMediaListSortingDown, 2, 1)
        Me.tblMovieGeneralMediaListSorting.Controls.Add(Me.btnMovieGeneralMediaListSortingUp, 1, 1)
        Me.tblMovieGeneralMediaListSorting.Controls.Add(Me.lvMovieGeneralMediaListSorting, 0, 0)
        Me.tblMovieGeneralMediaListSorting.Controls.Add(Me.btnMovieGeneralMediaListSortingReset, 4, 1)
        Me.tblMovieGeneralMediaListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieGeneralMediaListSorting.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieGeneralMediaListSorting.Name = "tblMovieGeneralMediaListSorting"
        Me.tblMovieGeneralMediaListSorting.RowCount = 3
        Me.tblMovieGeneralMediaListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMediaListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMediaListSorting.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieGeneralMediaListSorting.Size = New System.Drawing.Size(206, 285)
        Me.tblMovieGeneralMediaListSorting.TabIndex = 0
        '
        'btnMovieGeneralMediaListSortingDown
        '
        Me.btnMovieGeneralMediaListSortingDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMovieGeneralMediaListSortingDown.Image = CType(resources.GetObject("btnMovieGeneralMediaListSortingDown.Image"), System.Drawing.Image)
        Me.btnMovieGeneralMediaListSortingDown.Location = New System.Drawing.Point(91, 259)
        Me.btnMovieGeneralMediaListSortingDown.Name = "btnMovieGeneralMediaListSortingDown"
        Me.btnMovieGeneralMediaListSortingDown.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieGeneralMediaListSortingDown.TabIndex = 12
        Me.btnMovieGeneralMediaListSortingDown.UseVisualStyleBackColor = True
        '
        'btnMovieGeneralMediaListSortingUp
        '
        Me.btnMovieGeneralMediaListSortingUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnMovieGeneralMediaListSortingUp.Image = CType(resources.GetObject("btnMovieGeneralMediaListSortingUp.Image"), System.Drawing.Image)
        Me.btnMovieGeneralMediaListSortingUp.Location = New System.Drawing.Point(62, 259)
        Me.btnMovieGeneralMediaListSortingUp.Name = "btnMovieGeneralMediaListSortingUp"
        Me.btnMovieGeneralMediaListSortingUp.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieGeneralMediaListSortingUp.TabIndex = 13
        Me.btnMovieGeneralMediaListSortingUp.UseVisualStyleBackColor = True
        '
        'lvMovieGeneralMediaListSorting
        '
        Me.lvMovieGeneralMediaListSorting.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colMovieGeneralMediaListSortingDisplayIndex, Me.colMovieGeneralMediaListSortingColumn, Me.colMovieGeneralMediaListSortingLabel, Me.colMovieGeneralMediaListSortingHide})
        Me.tblMovieGeneralMediaListSorting.SetColumnSpan(Me.lvMovieGeneralMediaListSorting, 5)
        Me.lvMovieGeneralMediaListSorting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMovieGeneralMediaListSorting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lvMovieGeneralMediaListSorting.FullRowSelect = True
        Me.lvMovieGeneralMediaListSorting.HideSelection = False
        Me.lvMovieGeneralMediaListSorting.Location = New System.Drawing.Point(3, 3)
        Me.lvMovieGeneralMediaListSorting.Name = "lvMovieGeneralMediaListSorting"
        Me.lvMovieGeneralMediaListSorting.Size = New System.Drawing.Size(200, 250)
        Me.lvMovieGeneralMediaListSorting.TabIndex = 10
        Me.lvMovieGeneralMediaListSorting.UseCompatibleStateImageBehavior = False
        Me.lvMovieGeneralMediaListSorting.View = System.Windows.Forms.View.Details
        '
        'colMovieGeneralMediaListSortingDisplayIndex
        '
        Me.colMovieGeneralMediaListSortingDisplayIndex.Text = "DisplayIndex"
        Me.colMovieGeneralMediaListSortingDisplayIndex.Width = 0
        '
        'colMovieGeneralMediaListSortingColumn
        '
        Me.colMovieGeneralMediaListSortingColumn.Text = "DBName"
        Me.colMovieGeneralMediaListSortingColumn.Width = 0
        '
        'colMovieGeneralMediaListSortingLabel
        '
        Me.colMovieGeneralMediaListSortingLabel.Text = "Column"
        Me.colMovieGeneralMediaListSortingLabel.Width = 110
        '
        'colMovieGeneralMediaListSortingHide
        '
        Me.colMovieGeneralMediaListSortingHide.Text = "Hide"
        '
        'btnMovieGeneralMediaListSortingReset
        '
        Me.btnMovieGeneralMediaListSortingReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMovieGeneralMediaListSortingReset.Image = CType(resources.GetObject("btnMovieGeneralMediaListSortingReset.Image"), System.Drawing.Image)
        Me.btnMovieGeneralMediaListSortingReset.Location = New System.Drawing.Point(180, 259)
        Me.btnMovieGeneralMediaListSortingReset.Name = "btnMovieGeneralMediaListSortingReset"
        Me.btnMovieGeneralMediaListSortingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnMovieGeneralMediaListSortingReset.TabIndex = 11
        Me.btnMovieGeneralMediaListSortingReset.UseVisualStyleBackColor = True
        '
        'frmMovie_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 562)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmMovie_GUI"
        Me.Text = "frmMovie_GUI"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbMovieGeneralCustomMarker.ResumeLayout(False)
        Me.gbMovieGeneralCustomMarker.PerformLayout()
        Me.tblMovieGeneralCustomMarker.ResumeLayout(False)
        Me.tblMovieGeneralCustomMarker.PerformLayout()
        Me.gbMovieGeneralMiscOpts.ResumeLayout(False)
        Me.gbMovieGeneralMiscOpts.PerformLayout()
        Me.tblMovieGeneralMiscOpts.ResumeLayout(False)
        Me.tblMovieGeneralMiscOpts.PerformLayout()
        Me.gbMovieGeneralMainWindowOpts.ResumeLayout(False)
        Me.gbMovieGeneralMainWindowOpts.PerformLayout()
        Me.tblMovieGeneralMainWindow.ResumeLayout(False)
        Me.tblMovieGeneralMainWindow.PerformLayout()
        Me.gbMovieGeneralCustomScrapeButton.ResumeLayout(False)
        Me.gbMovieGeneralCustomScrapeButton.PerformLayout()
        Me.tblMovieGeneralCustomScrapeButton.ResumeLayout(False)
        Me.tblMovieGeneralCustomScrapeButton.PerformLayout()
        Me.gbMovieGeneralMediaListOpts.ResumeLayout(False)
        Me.gbMovieGeneralMediaListOpts.PerformLayout()
        Me.tblMovieGeneralMediaListOpts.ResumeLayout(False)
        Me.tblMovieGeneralMediaListOpts.PerformLayout()
        Me.gbMovieGeneralMediaListSortTokensOpts.ResumeLayout(False)
        Me.gbMovieGeneralMediaListSortTokensOpts.PerformLayout()
        Me.tblMovieGeneralSortTokensOpts.ResumeLayout(False)
        Me.tblMovieGeneralSortTokensOpts.PerformLayout()
        Me.gbMovieGeneralMediaListSorting.ResumeLayout(False)
        Me.gbMovieGeneralMediaListSorting.PerformLayout()
        Me.tblMovieGeneralMediaListSorting.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbMovieGeneralMiscOpts As GroupBox
    Friend WithEvents tblMovieGeneralMiscOpts As TableLayoutPanel
    Friend WithEvents chkMovieClickScrapeAsk As CheckBox
    Friend WithEvents chkMovieClickScrape As CheckBox
    Friend WithEvents gbMovieGeneralMediaListOpts As GroupBox
    Friend WithEvents tblMovieGeneralMediaListOpts As TableLayoutPanel
    Friend WithEvents gbMovieGeneralMediaListSortTokensOpts As GroupBox
    Friend WithEvents tblMovieGeneralSortTokensOpts As TableLayoutPanel
    Friend WithEvents btnMovieSortTokenReset As Button
    Friend WithEvents btnMovieSortTokenRemove As Button
    Friend WithEvents lstMovieSortTokens As ListBox
    Friend WithEvents btnMovieSortTokenAdd As Button
    Friend WithEvents txtMovieSortToken As TextBox
    Friend WithEvents chkMovieLevTolerance As CheckBox
    Friend WithEvents lblMovieLevTolerance As Label
    Friend WithEvents txtMovieLevTolerance As TextBox
    Friend WithEvents gbMovieGeneralMediaListSorting As GroupBox
    Friend WithEvents tblMovieGeneralMediaListSorting As TableLayoutPanel
    Friend WithEvents btnMovieGeneralMediaListSortingDown As Button
    Friend WithEvents btnMovieGeneralMediaListSortingUp As Button
    Friend WithEvents lvMovieGeneralMediaListSorting As ListView
    Friend WithEvents colMovieGeneralMediaListSortingDisplayIndex As ColumnHeader
    Friend WithEvents colMovieGeneralMediaListSortingColumn As ColumnHeader
    Friend WithEvents colMovieGeneralMediaListSortingLabel As ColumnHeader
    Friend WithEvents colMovieGeneralMediaListSortingHide As ColumnHeader
    Friend WithEvents btnMovieGeneralMediaListSortingReset As Button
    Friend WithEvents gbMovieGeneralCustomMarker As GroupBox
    Friend WithEvents tblMovieGeneralCustomMarker As TableLayoutPanel
    Friend WithEvents btnMovieGeneralCustomMarker4 As Button
    Friend WithEvents lblMovieGeneralCustomMarker1 As Label
    Friend WithEvents btnMovieGeneralCustomMarker3 As Button
    Friend WithEvents txtMovieGeneralCustomMarker4 As TextBox
    Friend WithEvents btnMovieGeneralCustomMarker2 As Button
    Friend WithEvents lblMovieGeneralCustomMarker2 As Label
    Friend WithEvents btnMovieGeneralCustomMarker1 As Button
    Friend WithEvents lblMovieGeneralCustomMarker4 As Label
    Friend WithEvents txtMovieGeneralCustomMarker3 As TextBox
    Friend WithEvents lblMovieGeneralCustomMarker3 As Label
    Friend WithEvents txtMovieGeneralCustomMarker1 As TextBox
    Friend WithEvents txtMovieGeneralCustomMarker2 As TextBox
    Friend WithEvents gbMovieGeneralMainWindowOpts As GroupBox
    Friend WithEvents tblMovieGeneralMainWindow As TableLayoutPanel
    Friend WithEvents lblMovieLanguageOverlay As Label
    Friend WithEvents cbMovieLanguageOverlay As ComboBox
    Friend WithEvents gbMovieGeneralCustomScrapeButton As GroupBox
    Friend WithEvents tblMovieGeneralCustomScrapeButton As TableLayoutPanel
    Friend WithEvents cbMovieGeneralCustomScrapeButtonScrapeType As ComboBox
    Friend WithEvents cbMovieGeneralCustomScrapeButtonModifierType As ComboBox
    Friend WithEvents txtMovieGeneralCustomScrapeButtonScrapeType As Label
    Friend WithEvents txtMovieGeneralCustomScrapeButtonModifierType As Label
    Friend WithEvents rbMovieGeneralCustomScrapeButtonEnabled As RadioButton
    Friend WithEvents rbMovieGeneralCustomScrapeButtonDisabled As RadioButton
    Friend WithEvents cdColor As ColorDialog
End Class
