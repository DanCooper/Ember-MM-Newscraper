<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTV_Source_Regex
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTV_Source_Regex))
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.gbTVSourcesRegexTVShowMatching = New System.Windows.Forms.GroupBox()
        Me.tblTVSourcesRegexTVShowMatching = New System.Windows.Forms.TableLayoutPanel()
        Me.lvTVSourcesRegexTVShowMatching = New System.Windows.Forms.ListView()
        Me.colTVSourcesRegexTVShowMatchingID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesRegexTVShowMatchingRegex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.coTVSourcesRegexTVShowMatchingDefaultSeason = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTVSourcesRegexTVShowMatchingByDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnTVSourcesRegexTVShowMatchingClear = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingDown = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingUp = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingEdit = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingReset = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingRemove = New System.Windows.Forms.Button()
        Me.btnTVSourcesRegexTVShowMatchingAdd = New System.Windows.Forms.Button()
        Me.tblTVSourcesRegexTVShowMatchingEdit = New System.Windows.Forms.TableLayoutPanel()
        Me.lblTVSourcesRegexTVShowMatchingRegex = New System.Windows.Forms.Label()
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason = New System.Windows.Forms.Label()
        Me.txtTVSourcesRegexTVShowMatchingRegex = New System.Windows.Forms.TextBox()
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason = New System.Windows.Forms.TextBox()
        Me.lblTVSourcesRegexTVShowMatchingByDate = New System.Windows.Forms.Label()
        Me.chkTVSourcesRegexTVShowMatchingByDate = New System.Windows.Forms.CheckBox()
        Me.gbTVSourcesRegexMultiPartMatching = New System.Windows.Forms.GroupBox()
        Me.tblTVSourcesRegexMultiPartMatching = New System.Windows.Forms.TableLayoutPanel()
        Me.txtTVSourcesRegexMultiPartMatching = New System.Windows.Forms.TextBox()
        Me.btnTVSourcesRegexMultiPartMatchingReset = New System.Windows.Forms.Button()
        Me.pnlSettings.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.gbTVSourcesRegexTVShowMatching.SuspendLayout()
        Me.tblTVSourcesRegexTVShowMatching.SuspendLayout()
        Me.tblTVSourcesRegexTVShowMatchingEdit.SuspendLayout()
        Me.gbTVSourcesRegexMultiPartMatching.SuspendLayout()
        Me.tblTVSourcesRegexMultiPartMatching.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(903, 531)
        Me.pnlSettings.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoScroll = True
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.gbTVSourcesRegexMultiPartMatching, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.gbTVSourcesRegexTVShowMatching, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(903, 531)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'gbTVSourcesRegexTVShowMatching
        '
        Me.gbTVSourcesRegexTVShowMatching.AutoSize = True
        Me.gbTVSourcesRegexTVShowMatching.Controls.Add(Me.tblTVSourcesRegexTVShowMatching)
        Me.gbTVSourcesRegexTVShowMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVSourcesRegexTVShowMatching.Location = New System.Drawing.Point(3, 3)
        Me.gbTVSourcesRegexTVShowMatching.Name = "gbTVSourcesRegexTVShowMatching"
        Me.gbTVSourcesRegexTVShowMatching.Size = New System.Drawing.Size(812, 368)
        Me.gbTVSourcesRegexTVShowMatching.TabIndex = 8
        Me.gbTVSourcesRegexTVShowMatching.TabStop = False
        Me.gbTVSourcesRegexTVShowMatching.Text = "TV Show Matching"
        '
        'tblTVSourcesRegexTVShowMatching
        '
        Me.tblTVSourcesRegexTVShowMatching.AutoSize = True
        Me.tblTVSourcesRegexTVShowMatching.ColumnCount = 8
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.lvTVSourcesRegexTVShowMatching, 0, 1)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingClear, 0, 4)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingDown, 3, 2)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingUp, 2, 2)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingEdit, 0, 2)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingReset, 6, 0)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingRemove, 5, 2)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.btnTVSourcesRegexTVShowMatchingAdd, 5, 4)
        Me.tblTVSourcesRegexTVShowMatching.Controls.Add(Me.tblTVSourcesRegexTVShowMatchingEdit, 0, 3)
        Me.tblTVSourcesRegexTVShowMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesRegexTVShowMatching.Location = New System.Drawing.Point(3, 18)
        Me.tblTVSourcesRegexTVShowMatching.Name = "tblTVSourcesRegexTVShowMatching"
        Me.tblTVSourcesRegexTVShowMatching.RowCount = 6
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatching.Size = New System.Drawing.Size(806, 347)
        Me.tblTVSourcesRegexTVShowMatching.TabIndex = 8
        '
        'lvTVSourcesRegexTVShowMatching
        '
        Me.lvTVSourcesRegexTVShowMatching.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTVSourcesRegexTVShowMatchingID, Me.colTVSourcesRegexTVShowMatchingRegex, Me.coTVSourcesRegexTVShowMatchingDefaultSeason, Me.colTVSourcesRegexTVShowMatchingByDate})
        Me.tblTVSourcesRegexTVShowMatching.SetColumnSpan(Me.lvTVSourcesRegexTVShowMatching, 7)
        Me.lvTVSourcesRegexTVShowMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTVSourcesRegexTVShowMatching.FullRowSelect = True
        Me.lvTVSourcesRegexTVShowMatching.HideSelection = False
        Me.lvTVSourcesRegexTVShowMatching.Location = New System.Drawing.Point(3, 32)
        Me.lvTVSourcesRegexTVShowMatching.Name = "lvTVSourcesRegexTVShowMatching"
        Me.lvTVSourcesRegexTVShowMatching.Size = New System.Drawing.Size(800, 200)
        Me.lvTVSourcesRegexTVShowMatching.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvTVSourcesRegexTVShowMatching.TabIndex = 0
        Me.lvTVSourcesRegexTVShowMatching.UseCompatibleStateImageBehavior = False
        Me.lvTVSourcesRegexTVShowMatching.View = System.Windows.Forms.View.Details
        '
        'colTVSourcesRegexTVShowMatchingID
        '
        Me.colTVSourcesRegexTVShowMatchingID.DisplayIndex = 2
        Me.colTVSourcesRegexTVShowMatchingID.Width = 0
        '
        'colTVSourcesRegexTVShowMatchingRegex
        '
        Me.colTVSourcesRegexTVShowMatchingRegex.DisplayIndex = 0
        Me.colTVSourcesRegexTVShowMatchingRegex.Text = "Regex"
        Me.colTVSourcesRegexTVShowMatchingRegex.Width = 600
        '
        'coTVSourcesRegexTVShowMatchingDefaultSeason
        '
        Me.coTVSourcesRegexTVShowMatchingDefaultSeason.DisplayIndex = 1
        Me.coTVSourcesRegexTVShowMatchingDefaultSeason.Text = "Default Season"
        Me.coTVSourcesRegexTVShowMatchingDefaultSeason.Width = 90
        '
        'colTVSourcesRegexTVShowMatchingByDate
        '
        Me.colTVSourcesRegexTVShowMatchingByDate.Text = "by Date"
        '
        'btnTVSourcesRegexTVShowMatchingClear
        '
        Me.btnTVSourcesRegexTVShowMatchingClear.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVSourcesRegexTVShowMatchingClear.AutoSize = True
        Me.btnTVSourcesRegexTVShowMatchingClear.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingClear.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourcesRegexTVShowMatchingClear.Location = New System.Drawing.Point(3, 321)
        Me.btnTVSourcesRegexTVShowMatchingClear.Name = "btnTVSourcesRegexTVShowMatchingClear"
        Me.btnTVSourcesRegexTVShowMatchingClear.Size = New System.Drawing.Size(100, 23)
        Me.btnTVSourcesRegexTVShowMatchingClear.TabIndex = 8
        Me.btnTVSourcesRegexTVShowMatchingClear.Text = "Clear"
        Me.btnTVSourcesRegexTVShowMatchingClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourcesRegexTVShowMatchingClear.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingDown
        '
        Me.btnTVSourcesRegexTVShowMatchingDown.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVSourcesRegexTVShowMatchingDown.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingDown.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingDown.Location = New System.Drawing.Point(406, 238)
        Me.btnTVSourcesRegexTVShowMatchingDown.Name = "btnTVSourcesRegexTVShowMatchingDown"
        Me.btnTVSourcesRegexTVShowMatchingDown.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSourcesRegexTVShowMatchingDown.TabIndex = 5
        Me.btnTVSourcesRegexTVShowMatchingDown.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingUp
        '
        Me.btnTVSourcesRegexTVShowMatchingUp.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnTVSourcesRegexTVShowMatchingUp.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingUp.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingUp.Location = New System.Drawing.Point(377, 238)
        Me.btnTVSourcesRegexTVShowMatchingUp.Name = "btnTVSourcesRegexTVShowMatchingUp"
        Me.btnTVSourcesRegexTVShowMatchingUp.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSourcesRegexTVShowMatchingUp.TabIndex = 4
        Me.btnTVSourcesRegexTVShowMatchingUp.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingEdit
        '
        Me.btnTVSourcesRegexTVShowMatchingEdit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnTVSourcesRegexTVShowMatchingEdit.AutoSize = True
        Me.btnTVSourcesRegexTVShowMatchingEdit.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingEdit.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourcesRegexTVShowMatchingEdit.Location = New System.Drawing.Point(3, 238)
        Me.btnTVSourcesRegexTVShowMatchingEdit.Name = "btnTVSourcesRegexTVShowMatchingEdit"
        Me.btnTVSourcesRegexTVShowMatchingEdit.Size = New System.Drawing.Size(100, 23)
        Me.btnTVSourcesRegexTVShowMatchingEdit.TabIndex = 3
        Me.btnTVSourcesRegexTVShowMatchingEdit.Text = "Edit Regex"
        Me.btnTVSourcesRegexTVShowMatchingEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourcesRegexTVShowMatchingEdit.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingReset
        '
        Me.btnTVSourcesRegexTVShowMatchingReset.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVSourcesRegexTVShowMatchingReset.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingReset.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingReset.Location = New System.Drawing.Point(780, 3)
        Me.btnTVSourcesRegexTVShowMatchingReset.Name = "btnTVSourcesRegexTVShowMatchingReset"
        Me.btnTVSourcesRegexTVShowMatchingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSourcesRegexTVShowMatchingReset.TabIndex = 2
        Me.btnTVSourcesRegexTVShowMatchingReset.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingRemove
        '
        Me.btnTVSourcesRegexTVShowMatchingRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVSourcesRegexTVShowMatchingRemove.AutoSize = True
        Me.tblTVSourcesRegexTVShowMatching.SetColumnSpan(Me.btnTVSourcesRegexTVShowMatchingRemove, 2)
        Me.btnTVSourcesRegexTVShowMatchingRemove.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingRemove.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourcesRegexTVShowMatchingRemove.Location = New System.Drawing.Point(703, 238)
        Me.btnTVSourcesRegexTVShowMatchingRemove.Name = "btnTVSourcesRegexTVShowMatchingRemove"
        Me.btnTVSourcesRegexTVShowMatchingRemove.Size = New System.Drawing.Size(100, 23)
        Me.btnTVSourcesRegexTVShowMatchingRemove.TabIndex = 6
        Me.btnTVSourcesRegexTVShowMatchingRemove.Text = "Remove"
        Me.btnTVSourcesRegexTVShowMatchingRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourcesRegexTVShowMatchingRemove.UseVisualStyleBackColor = True
        '
        'btnTVSourcesRegexTVShowMatchingAdd
        '
        Me.btnTVSourcesRegexTVShowMatchingAdd.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnTVSourcesRegexTVShowMatchingAdd.AutoSize = True
        Me.tblTVSourcesRegexTVShowMatching.SetColumnSpan(Me.btnTVSourcesRegexTVShowMatchingAdd, 2)
        Me.btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        Me.btnTVSourcesRegexTVShowMatchingAdd.Image = CType(resources.GetObject("btnTVSourcesRegexTVShowMatchingAdd.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexTVShowMatchingAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTVSourcesRegexTVShowMatchingAdd.Location = New System.Drawing.Point(703, 321)
        Me.btnTVSourcesRegexTVShowMatchingAdd.Name = "btnTVSourcesRegexTVShowMatchingAdd"
        Me.btnTVSourcesRegexTVShowMatchingAdd.Size = New System.Drawing.Size(100, 23)
        Me.btnTVSourcesRegexTVShowMatchingAdd.TabIndex = 9
        Me.btnTVSourcesRegexTVShowMatchingAdd.Text = "Add Regex"
        Me.btnTVSourcesRegexTVShowMatchingAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnTVSourcesRegexTVShowMatchingAdd.UseVisualStyleBackColor = True
        '
        'tblTVSourcesRegexTVShowMatchingEdit
        '
        Me.tblTVSourcesRegexTVShowMatchingEdit.AutoSize = True
        Me.tblTVSourcesRegexTVShowMatchingEdit.ColumnCount = 3
        Me.tblTVSourcesRegexTVShowMatching.SetColumnSpan(Me.tblTVSourcesRegexTVShowMatchingEdit, 7)
        Me.tblTVSourcesRegexTVShowMatchingEdit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatchingEdit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatchingEdit.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.lblTVSourcesRegexTVShowMatchingRegex, 0, 0)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.lblTVSourcesRegexTVShowMatchingDefaultSeason, 1, 0)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.txtTVSourcesRegexTVShowMatchingRegex, 0, 1)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.txtTVSourcesRegexTVShowMatchingDefaultSeason, 1, 1)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.lblTVSourcesRegexTVShowMatchingByDate, 2, 0)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Controls.Add(Me.chkTVSourcesRegexTVShowMatchingByDate, 2, 1)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesRegexTVShowMatchingEdit.Location = New System.Drawing.Point(3, 267)
        Me.tblTVSourcesRegexTVShowMatchingEdit.Name = "tblTVSourcesRegexTVShowMatchingEdit"
        Me.tblTVSourcesRegexTVShowMatchingEdit.RowCount = 2
        Me.tblTVSourcesRegexTVShowMatchingEdit.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesRegexTVShowMatchingEdit.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexTVShowMatchingEdit.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblTVSourcesRegexTVShowMatchingEdit.Size = New System.Drawing.Size(800, 48)
        Me.tblTVSourcesRegexTVShowMatchingEdit.TabIndex = 12
        '
        'lblTVSourcesRegexTVShowMatchingRegex
        '
        Me.lblTVSourcesRegexTVShowMatchingRegex.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTVSourcesRegexTVShowMatchingRegex.AutoSize = True
        Me.lblTVSourcesRegexTVShowMatchingRegex.Location = New System.Drawing.Point(3, 3)
        Me.lblTVSourcesRegexTVShowMatchingRegex.Name = "lblTVSourcesRegexTVShowMatchingRegex"
        Me.lblTVSourcesRegexTVShowMatchingRegex.Size = New System.Drawing.Size(38, 13)
        Me.lblTVSourcesRegexTVShowMatchingRegex.TabIndex = 0
        Me.lblTVSourcesRegexTVShowMatchingRegex.Text = "Regex"
        '
        'lblTVSourcesRegexTVShowMatchingDefaultSeason
        '
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.AutoSize = True
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Location = New System.Drawing.Point(575, 3)
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Name = "lblTVSourcesRegexTVShowMatchingDefaultSeason"
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Size = New System.Drawing.Size(85, 13)
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.TabIndex = 2
        Me.lblTVSourcesRegexTVShowMatchingDefaultSeason.Text = "Default Season"
        '
        'txtTVSourcesRegexTVShowMatchingRegex
        '
        Me.txtTVSourcesRegexTVShowMatchingRegex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTVSourcesRegexTVShowMatchingRegex.Location = New System.Drawing.Point(3, 23)
        Me.txtTVSourcesRegexTVShowMatchingRegex.Name = "txtTVSourcesRegexTVShowMatchingRegex"
        Me.txtTVSourcesRegexTVShowMatchingRegex.Size = New System.Drawing.Size(566, 22)
        Me.txtTVSourcesRegexTVShowMatchingRegex.TabIndex = 1
        '
        'txtTVSourcesRegexTVShowMatchingDefaultSeason
        '
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Location = New System.Drawing.Point(575, 23)
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Name = "txtTVSourcesRegexTVShowMatchingDefaultSeason"
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.Size = New System.Drawing.Size(90, 22)
        Me.txtTVSourcesRegexTVShowMatchingDefaultSeason.TabIndex = 11
        '
        'lblTVSourcesRegexTVShowMatchingByDate
        '
        Me.lblTVSourcesRegexTVShowMatchingByDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblTVSourcesRegexTVShowMatchingByDate.AutoSize = True
        Me.lblTVSourcesRegexTVShowMatchingByDate.Location = New System.Drawing.Point(711, 3)
        Me.lblTVSourcesRegexTVShowMatchingByDate.Name = "lblTVSourcesRegexTVShowMatchingByDate"
        Me.lblTVSourcesRegexTVShowMatchingByDate.Size = New System.Drawing.Size(46, 13)
        Me.lblTVSourcesRegexTVShowMatchingByDate.TabIndex = 6
        Me.lblTVSourcesRegexTVShowMatchingByDate.Text = "by Date"
        '
        'chkTVSourcesRegexTVShowMatchingByDate
        '
        Me.chkTVSourcesRegexTVShowMatchingByDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkTVSourcesRegexTVShowMatchingByDate.AutoSize = True
        Me.chkTVSourcesRegexTVShowMatchingByDate.Location = New System.Drawing.Point(726, 27)
        Me.chkTVSourcesRegexTVShowMatchingByDate.Name = "chkTVSourcesRegexTVShowMatchingByDate"
        Me.chkTVSourcesRegexTVShowMatchingByDate.Size = New System.Drawing.Size(15, 14)
        Me.chkTVSourcesRegexTVShowMatchingByDate.TabIndex = 10
        Me.chkTVSourcesRegexTVShowMatchingByDate.UseVisualStyleBackColor = True
        '
        'gbTVSourcesRegexMultiPartMatching
        '
        Me.gbTVSourcesRegexMultiPartMatching.AutoSize = True
        Me.gbTVSourcesRegexMultiPartMatching.Controls.Add(Me.tblTVSourcesRegexMultiPartMatching)
        Me.gbTVSourcesRegexMultiPartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbTVSourcesRegexMultiPartMatching.Location = New System.Drawing.Point(3, 377)
        Me.gbTVSourcesRegexMultiPartMatching.Name = "gbTVSourcesRegexMultiPartMatching"
        Me.gbTVSourcesRegexMultiPartMatching.Size = New System.Drawing.Size(812, 50)
        Me.gbTVSourcesRegexMultiPartMatching.TabIndex = 9
        Me.gbTVSourcesRegexMultiPartMatching.TabStop = False
        Me.gbTVSourcesRegexMultiPartMatching.Text = "TV Show Multi Part Matching"
        '
        'tblTVSourcesRegexMultiPartMatching
        '
        Me.tblTVSourcesRegexMultiPartMatching.AutoSize = True
        Me.tblTVSourcesRegexMultiPartMatching.ColumnCount = 2
        Me.tblTVSourcesRegexMultiPartMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexMultiPartMatching.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTVSourcesRegexMultiPartMatching.Controls.Add(Me.txtTVSourcesRegexMultiPartMatching, 0, 0)
        Me.tblTVSourcesRegexMultiPartMatching.Controls.Add(Me.btnTVSourcesRegexMultiPartMatchingReset, 1, 0)
        Me.tblTVSourcesRegexMultiPartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTVSourcesRegexMultiPartMatching.Location = New System.Drawing.Point(3, 18)
        Me.tblTVSourcesRegexMultiPartMatching.Name = "tblTVSourcesRegexMultiPartMatching"
        Me.tblTVSourcesRegexMultiPartMatching.RowCount = 2
        Me.tblTVSourcesRegexMultiPartMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexMultiPartMatching.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTVSourcesRegexMultiPartMatching.Size = New System.Drawing.Size(806, 29)
        Me.tblTVSourcesRegexMultiPartMatching.TabIndex = 0
        '
        'txtTVSourcesRegexMultiPartMatching
        '
        Me.txtTVSourcesRegexMultiPartMatching.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTVSourcesRegexMultiPartMatching.Location = New System.Drawing.Point(3, 3)
        Me.txtTVSourcesRegexMultiPartMatching.Name = "txtTVSourcesRegexMultiPartMatching"
        Me.txtTVSourcesRegexMultiPartMatching.Size = New System.Drawing.Size(759, 22)
        Me.txtTVSourcesRegexMultiPartMatching.TabIndex = 0
        '
        'btnTVSourcesRegexMultiPartMatchingReset
        '
        Me.btnTVSourcesRegexMultiPartMatchingReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTVSourcesRegexMultiPartMatchingReset.Image = CType(resources.GetObject("btnTVSourcesRegexMultiPartMatchingReset.Image"), System.Drawing.Image)
        Me.btnTVSourcesRegexMultiPartMatchingReset.Location = New System.Drawing.Point(780, 3)
        Me.btnTVSourcesRegexMultiPartMatchingReset.Name = "btnTVSourcesRegexMultiPartMatchingReset"
        Me.btnTVSourcesRegexMultiPartMatchingReset.Size = New System.Drawing.Size(23, 23)
        Me.btnTVSourcesRegexMultiPartMatchingReset.TabIndex = 3
        Me.btnTVSourcesRegexMultiPartMatchingReset.UseVisualStyleBackColor = True
        '
        'frmTV_Source_Regex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(903, 531)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Name = "frmTV_Source_Regex"
        Me.Text = "frmTV_Source_Regex"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.gbTVSourcesRegexTVShowMatching.ResumeLayout(False)
        Me.gbTVSourcesRegexTVShowMatching.PerformLayout()
        Me.tblTVSourcesRegexTVShowMatching.ResumeLayout(False)
        Me.tblTVSourcesRegexTVShowMatching.PerformLayout()
        Me.tblTVSourcesRegexTVShowMatchingEdit.ResumeLayout(False)
        Me.tblTVSourcesRegexTVShowMatchingEdit.PerformLayout()
        Me.gbTVSourcesRegexMultiPartMatching.ResumeLayout(False)
        Me.gbTVSourcesRegexMultiPartMatching.PerformLayout()
        Me.tblTVSourcesRegexMultiPartMatching.ResumeLayout(False)
        Me.tblTVSourcesRegexMultiPartMatching.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents gbTVSourcesRegexTVShowMatching As GroupBox
    Friend WithEvents tblTVSourcesRegexTVShowMatching As TableLayoutPanel
    Friend WithEvents lvTVSourcesRegexTVShowMatching As ListView
    Friend WithEvents colTVSourcesRegexTVShowMatchingID As ColumnHeader
    Friend WithEvents colTVSourcesRegexTVShowMatchingRegex As ColumnHeader
    Friend WithEvents coTVSourcesRegexTVShowMatchingDefaultSeason As ColumnHeader
    Friend WithEvents colTVSourcesRegexTVShowMatchingByDate As ColumnHeader
    Friend WithEvents btnTVSourcesRegexTVShowMatchingClear As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingDown As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingUp As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingEdit As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingReset As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingRemove As Button
    Friend WithEvents btnTVSourcesRegexTVShowMatchingAdd As Button
    Friend WithEvents tblTVSourcesRegexTVShowMatchingEdit As TableLayoutPanel
    Friend WithEvents lblTVSourcesRegexTVShowMatchingRegex As Label
    Friend WithEvents lblTVSourcesRegexTVShowMatchingDefaultSeason As Label
    Friend WithEvents txtTVSourcesRegexTVShowMatchingRegex As TextBox
    Friend WithEvents txtTVSourcesRegexTVShowMatchingDefaultSeason As TextBox
    Friend WithEvents lblTVSourcesRegexTVShowMatchingByDate As Label
    Friend WithEvents chkTVSourcesRegexTVShowMatchingByDate As CheckBox
    Friend WithEvents gbTVSourcesRegexMultiPartMatching As GroupBox
    Friend WithEvents tblTVSourcesRegexMultiPartMatching As TableLayoutPanel
    Friend WithEvents txtTVSourcesRegexMultiPartMatching As TextBox
    Friend WithEvents btnTVSourcesRegexMultiPartMatchingReset As Button
End Class
