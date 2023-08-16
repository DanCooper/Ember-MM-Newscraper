<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTV_Information_SeasonTitleBlacklist
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTV_Information_SeasonTitleBlacklist))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblTop = New System.Windows.Forms.TableLayoutPanel()
        Me.lblHint = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.tblMain = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvBlacklist = New System.Windows.Forms.DataGridView()
        Me.colBlacklistText = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblBottom = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSetDefaults = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.pnlTop.SuspendLayout()
        Me.tblTop.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.tblMain.SuspendLayout()
        CType(Me.dgvBlacklist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.tblBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.AutoSize = True
        Me.pnlTop.Controls.Add(Me.tblTop)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(484, 36)
        Me.pnlTop.TabIndex = 3
        '
        'tblTop
        '
        Me.tblTop.AutoSize = True
        Me.tblTop.ColumnCount = 1
        Me.tblTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblTop.Controls.Add(Me.lblHint, 0, 0)
        Me.tblTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblTop.Location = New System.Drawing.Point(0, 0)
        Me.tblTop.Name = "tblTop"
        Me.tblTop.RowCount = 1
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.tblTop.Size = New System.Drawing.Size(484, 36)
        Me.tblTop.TabIndex = 0
        '
        'lblHint
        '
        Me.lblHint.AutoSize = True
        Me.lblHint.Location = New System.Drawing.Point(3, 0)
        Me.lblHint.Name = "lblHint"
        Me.lblHint.Padding = New System.Windows.Forms.Padding(5)
        Me.lblHint.Size = New System.Drawing.Size(435, 36)
        Me.lblHint.TabIndex = 1
        Me.lblHint.Text = "This list contains patterns of season titles that should be ignored when scraping" &
    "." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Use %{season_number} to mark the location of the season number."
        '
        'pnlMain
        '
        Me.pnlMain.AutoSize = True
        Me.pnlMain.Controls.Add(Me.tblMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 36)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(484, 424)
        Me.pnlMain.TabIndex = 5
        '
        'tblMain
        '
        Me.tblMain.AutoSize = True
        Me.tblMain.ColumnCount = 1
        Me.tblMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMain.Controls.Add(Me.dgvBlacklist, 0, 0)
        Me.tblMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMain.Location = New System.Drawing.Point(0, 0)
        Me.tblMain.Name = "tblMain"
        Me.tblMain.RowCount = 1
        Me.tblMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMain.Size = New System.Drawing.Size(484, 424)
        Me.tblMain.TabIndex = 0
        '
        'dgvBlacklist
        '
        Me.dgvBlacklist.AllowUserToResizeColumns = False
        Me.dgvBlacklist.AllowUserToResizeRows = False
        Me.dgvBlacklist.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvBlacklist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvBlacklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBlacklist.ColumnHeadersVisible = False
        Me.dgvBlacklist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colBlacklistText})
        Me.dgvBlacklist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBlacklist.Location = New System.Drawing.Point(3, 3)
        Me.dgvBlacklist.Name = "dgvBlacklist"
        Me.dgvBlacklist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvBlacklist.Size = New System.Drawing.Size(478, 454)
        Me.dgvBlacklist.TabIndex = 17
        '
        'colBlacklistText
        '
        Me.colBlacklistText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colBlacklistText.HeaderText = "Text"
        Me.colBlacklistText.Name = "colBlacklistText"
        Me.colBlacklistText.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'pnlBottom
        '
        Me.pnlBottom.AutoSize = True
        Me.pnlBottom.Controls.Add(Me.tblBottom)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 460)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(484, 29)
        Me.pnlBottom.TabIndex = 6
        '
        'tblBottom
        '
        Me.tblBottom.AutoSize = True
        Me.tblBottom.ColumnCount = 4
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblBottom.Controls.Add(Me.btnSetDefaults, 0, 0)
        Me.tblBottom.Controls.Add(Me.btnCancel, 3, 0)
        Me.tblBottom.Controls.Add(Me.btnOK, 2, 0)
        Me.tblBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblBottom.Location = New System.Drawing.Point(0, 0)
        Me.tblBottom.Name = "tblBottom"
        Me.tblBottom.RowCount = 1
        Me.tblBottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblBottom.Size = New System.Drawing.Size(484, 29)
        Me.tblBottom.TabIndex = 0
        '
        'btnSetDefaults
        '
        Me.btnSetDefaults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetDefaults.Image = CType(resources.GetObject("btnSetDefaults.Image"), System.Drawing.Image)
        Me.btnSetDefaults.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSetDefaults.Location = New System.Drawing.Point(3, 3)
        Me.btnSetDefaults.Name = "btnSetDefaults"
        Me.btnSetDefaults.Size = New System.Drawing.Size(105, 23)
        Me.btnSetDefaults.TabIndex = 3
        Me.btnSetDefaults.Text = "Defaults"
        Me.btnSetDefaults.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSetDefaults.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.AutoSize = True
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(406, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.AutoSize = True
        Me.btnOK.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(350, 3)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(50, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 489)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(484, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'frmTV_Data_SeasonTitleBlacklist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 511)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(500, 550)
        Me.Name = "frmTV_Data_SeasonTitleBlacklist"
        Me.Text = "Season Title Blacklist"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblTop.ResumeLayout(False)
        Me.tblTop.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.tblMain.ResumeLayout(False)
        CType(Me.dgvBlacklist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblBottom.ResumeLayout(False)
        Me.tblBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents tblTop As TableLayoutPanel
    Friend WithEvents lblHint As Label
    Friend WithEvents pnlMain As Panel
    Friend WithEvents tblMain As TableLayoutPanel
    Friend WithEvents dgvBlacklist As DataGridView
    Friend WithEvents colBlacklistText As DataGridViewTextBoxColumn
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents tblBottom As TableLayoutPanel
    Friend WithEvents btnSetDefaults As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents StatusStrip1 As StatusStrip
End Class
