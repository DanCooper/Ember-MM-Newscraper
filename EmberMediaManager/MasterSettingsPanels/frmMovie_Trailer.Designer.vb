<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovie_Trailer
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
        Me.gbMovieTrailerOpts = New System.Windows.Forms.GroupBox()
        Me.tblMovieTrailerOpts = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMovieTrailerDefaultSearch = New System.Windows.Forms.TextBox()
        Me.lblMovieTrailerDefaultSearch = New System.Windows.Forms.Label()
        Me.chkMovieTrailerKeepExisting = New System.Windows.Forms.CheckBox()
        Me.cbMovieTrailerMinVideoQual = New System.Windows.Forms.ComboBox()
        Me.lblMovieTrailerMinQual = New System.Windows.Forms.Label()
        Me.lblMovieTrailerPrefQual = New System.Windows.Forms.Label()
        Me.cbMovieTrailerPrefVideoQual = New System.Windows.Forms.ComboBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbMovieTrailerOpts.SuspendLayout()
        Me.tblMovieTrailerOpts.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.BackColor = System.Drawing.Color.White
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(800, 450)
        Me.pnlSettings.TabIndex = 22
        Me.pnlSettings.Visible = False
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbMovieTrailerOpts, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(800, 450)
        Me.tblSettings.TabIndex = 2
        '
        'gbMovieTrailerOpts
        '
        Me.gbMovieTrailerOpts.AutoSize = True
        Me.gbMovieTrailerOpts.Controls.Add(Me.tblMovieTrailerOpts)
        Me.gbMovieTrailerOpts.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbMovieTrailerOpts.Location = New System.Drawing.Point(3, 3)
        Me.gbMovieTrailerOpts.Name = "gbMovieTrailerOpts"
        Me.gbMovieTrailerOpts.Size = New System.Drawing.Size(194, 186)
        Me.gbMovieTrailerOpts.TabIndex = 1
        Me.gbMovieTrailerOpts.TabStop = False
        Me.gbMovieTrailerOpts.Text = "Trailers"
        '
        'tblMovieTrailerOpts
        '
        Me.tblMovieTrailerOpts.AutoSize = True
        Me.tblMovieTrailerOpts.ColumnCount = 2
        Me.tblMovieTrailerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieTrailerOpts.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblMovieTrailerOpts.Controls.Add(Me.txtMovieTrailerDefaultSearch, 0, 6)
        Me.tblMovieTrailerOpts.Controls.Add(Me.lblMovieTrailerDefaultSearch, 0, 5)
        Me.tblMovieTrailerOpts.Controls.Add(Me.chkMovieTrailerKeepExisting, 0, 0)
        Me.tblMovieTrailerOpts.Controls.Add(Me.cbMovieTrailerMinVideoQual, 0, 4)
        Me.tblMovieTrailerOpts.Controls.Add(Me.lblMovieTrailerMinQual, 0, 3)
        Me.tblMovieTrailerOpts.Controls.Add(Me.lblMovieTrailerPrefQual, 0, 1)
        Me.tblMovieTrailerOpts.Controls.Add(Me.cbMovieTrailerPrefVideoQual, 0, 2)
        Me.tblMovieTrailerOpts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblMovieTrailerOpts.Location = New System.Drawing.Point(3, 18)
        Me.tblMovieTrailerOpts.Name = "tblMovieTrailerOpts"
        Me.tblMovieTrailerOpts.RowCount = 8
        Me.tblMovieTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblMovieTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieTrailerOpts.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblMovieTrailerOpts.Size = New System.Drawing.Size(188, 165)
        Me.tblMovieTrailerOpts.TabIndex = 2
        '
        'txtMovieTrailerDefaultSearch
        '
        Me.txtMovieTrailerDefaultSearch.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMovieTrailerDefaultSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtMovieTrailerDefaultSearch.Location = New System.Drawing.Point(3, 140)
        Me.txtMovieTrailerDefaultSearch.Name = "txtMovieTrailerDefaultSearch"
        Me.txtMovieTrailerDefaultSearch.Size = New System.Drawing.Size(182, 22)
        Me.txtMovieTrailerDefaultSearch.TabIndex = 10
        '
        'lblMovieTrailerDefaultSearch
        '
        Me.lblMovieTrailerDefaultSearch.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieTrailerDefaultSearch.AutoSize = True
        Me.lblMovieTrailerDefaultSearch.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieTrailerDefaultSearch.Location = New System.Drawing.Point(3, 120)
        Me.lblMovieTrailerDefaultSearch.Name = "lblMovieTrailerDefaultSearch"
        Me.lblMovieTrailerDefaultSearch.Size = New System.Drawing.Size(139, 13)
        Me.lblMovieTrailerDefaultSearch.TabIndex = 11
        Me.lblMovieTrailerDefaultSearch.Text = "Default Search Parameter:"
        '
        'chkMovieTrailerKeepExisting
        '
        Me.chkMovieTrailerKeepExisting.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMovieTrailerKeepExisting.AutoSize = True
        Me.chkMovieTrailerKeepExisting.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMovieTrailerKeepExisting.Location = New System.Drawing.Point(3, 3)
        Me.chkMovieTrailerKeepExisting.Name = "chkMovieTrailerKeepExisting"
        Me.chkMovieTrailerKeepExisting.Size = New System.Drawing.Size(94, 17)
        Me.chkMovieTrailerKeepExisting.TabIndex = 4
        Me.chkMovieTrailerKeepExisting.Text = "Keep existing"
        Me.chkMovieTrailerKeepExisting.UseVisualStyleBackColor = True
        '
        'cbMovieTrailerMinVideoQual
        '
        Me.cbMovieTrailerMinVideoQual.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbMovieTrailerMinVideoQual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieTrailerMinVideoQual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbMovieTrailerMinVideoQual.FormattingEnabled = True
        Me.cbMovieTrailerMinVideoQual.Location = New System.Drawing.Point(3, 93)
        Me.cbMovieTrailerMinVideoQual.Name = "cbMovieTrailerMinVideoQual"
        Me.cbMovieTrailerMinVideoQual.Size = New System.Drawing.Size(125, 21)
        Me.cbMovieTrailerMinVideoQual.TabIndex = 9
        '
        'lblMovieTrailerMinQual
        '
        Me.lblMovieTrailerMinQual.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieTrailerMinQual.AutoSize = True
        Me.lblMovieTrailerMinQual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieTrailerMinQual.Location = New System.Drawing.Point(3, 73)
        Me.lblMovieTrailerMinQual.Name = "lblMovieTrailerMinQual"
        Me.lblMovieTrailerMinQual.Size = New System.Drawing.Size(97, 13)
        Me.lblMovieTrailerMinQual.TabIndex = 8
        Me.lblMovieTrailerMinQual.Text = "Minimum Quality:"
        '
        'lblMovieTrailerPrefQual
        '
        Me.lblMovieTrailerPrefQual.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMovieTrailerPrefQual.AutoSize = True
        Me.lblMovieTrailerPrefQual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMovieTrailerPrefQual.Location = New System.Drawing.Point(3, 26)
        Me.lblMovieTrailerPrefQual.Name = "lblMovieTrailerPrefQual"
        Me.lblMovieTrailerPrefQual.Size = New System.Drawing.Size(96, 13)
        Me.lblMovieTrailerPrefQual.TabIndex = 6
        Me.lblMovieTrailerPrefQual.Text = "Preferred Quality:"
        '
        'cbMovieTrailerPrefVideoQual
        '
        Me.cbMovieTrailerPrefVideoQual.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cbMovieTrailerPrefVideoQual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbMovieTrailerPrefVideoQual.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cbMovieTrailerPrefVideoQual.FormattingEnabled = True
        Me.cbMovieTrailerPrefVideoQual.Location = New System.Drawing.Point(3, 46)
        Me.cbMovieTrailerPrefVideoQual.Name = "cbMovieTrailerPrefVideoQual"
        Me.cbMovieTrailerPrefVideoQual.Size = New System.Drawing.Size(125, 21)
        Me.cbMovieTrailerPrefVideoQual.TabIndex = 7
        '
        'frmMovie_Trailer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlSettings)
        Me.Name = "frmMovie_Trailer"
        Me.Text = "frmMovie_Trailer"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbMovieTrailerOpts.ResumeLayout(False)
        Me.gbMovieTrailerOpts.PerformLayout()
        Me.tblMovieTrailerOpts.ResumeLayout(False)
        Me.tblMovieTrailerOpts.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlSettings As Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbMovieTrailerOpts As GroupBox
    Friend WithEvents tblMovieTrailerOpts As TableLayoutPanel
    Friend WithEvents txtMovieTrailerDefaultSearch As TextBox
    Friend WithEvents lblMovieTrailerDefaultSearch As Label
    Friend WithEvents chkMovieTrailerKeepExisting As CheckBox
    Friend WithEvents cbMovieTrailerMinVideoQual As ComboBox
    Friend WithEvents lblMovieTrailerMinQual As Label
    Friend WithEvents lblMovieTrailerPrefQual As Label
    Friend WithEvents cbMovieTrailerPrefVideoQual As ComboBox
End Class
