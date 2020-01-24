<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgThemeSelect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dlgThemeSelect))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnSkip = New System.Windows.Forms.Button()
        Me.gbSelectTheme = New System.Windows.Forms.GroupBox()
        Me.lvThemes = New System.Windows.Forms.ListView()
        Me.colNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colURL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colWebURL = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colBitrate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDuration = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colExtension = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSource = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colScraper = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pbStatus = New System.Windows.Forms.ProgressBar()
        Me.gbSelectTheme.SuspendLayout()
        Me.pnlStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(470, 258)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnSkip
        '
        Me.btnSkip.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSkip.Location = New System.Drawing.Point(551, 258)
        Me.btnSkip.Name = "btnSkip"
        Me.btnSkip.Size = New System.Drawing.Size(75, 23)
        Me.btnSkip.TabIndex = 2
        Me.btnSkip.Text = "Skip"
        Me.btnSkip.UseVisualStyleBackColor = True
        '
        'gbSelectTheme
        '
        Me.gbSelectTheme.Controls.Add(Me.lvThemes)
        Me.gbSelectTheme.Location = New System.Drawing.Point(12, 12)
        Me.gbSelectTheme.Name = "gbSelectTheme"
        Me.gbSelectTheme.Size = New System.Drawing.Size(614, 240)
        Me.gbSelectTheme.TabIndex = 3
        Me.gbSelectTheme.TabStop = False
        Me.gbSelectTheme.Text = "Select Theme to Scrape"
        '
        'lvThemes
        '
        Me.lvThemes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNumber, Me.colURL, Me.colWebURL, Me.colDescription, Me.colBitrate, Me.colDuration, Me.colExtension, Me.colSource, Me.colScraper})
        Me.lvThemes.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvThemes.HideSelection = False
        Me.lvThemes.Location = New System.Drawing.Point(6, 19)
        Me.lvThemes.Name = "lvThemes"
        Me.lvThemes.Size = New System.Drawing.Size(602, 215)
        Me.lvThemes.TabIndex = 5
        Me.lvThemes.UseCompatibleStateImageBehavior = False
        Me.lvThemes.View = System.Windows.Forms.View.Details
        '
        'colNumber
        '
        Me.colNumber.Text = "#"
        Me.colNumber.Width = 20
        '
        'colURL
        '
        Me.colURL.Text = "URL"
        Me.colURL.Width = 0
        '
        'colWebURL
        '
        Me.colWebURL.Text = "WebURL"
        Me.colWebURL.Width = 0
        '
        'colDescription
        '
        Me.colDescription.Text = "Description"
        Me.colDescription.Width = 180
        '
        'colBitrate
        '
        Me.colBitrate.DisplayIndex = 7
        Me.colBitrate.Text = "Bitrate"
        '
        'colDuration
        '
        Me.colDuration.DisplayIndex = 4
        Me.colDuration.Text = "Duration"
        '
        'colExtension
        '
        Me.colExtension.DisplayIndex = 8
        Me.colExtension.Text = "Extension"
        '
        'colSource
        '
        Me.colSource.DisplayIndex = 5
        Me.colSource.Text = "Source"
        Me.colSource.Width = 80
        '
        'colScraper
        '
        Me.colScraper.DisplayIndex = 6
        Me.colScraper.Text = "Scraper"
        Me.colScraper.Width = 80
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.White
        Me.pnlStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatus.Controls.Add(Me.lblStatus)
        Me.pnlStatus.Controls.Add(Me.pbStatus)
        Me.pnlStatus.Location = New System.Drawing.Point(218, 87)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(200, 54)
        Me.pnlStatus.TabIndex = 4
        Me.pnlStatus.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(3, 10)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(124, 13)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Compiling theme list..."
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbStatus
        '
        Me.pbStatus.Location = New System.Drawing.Point(3, 32)
        Me.pbStatus.MarqueeAnimationSpeed = 25
        Me.pbStatus.Name = "pbStatus"
        Me.pbStatus.Size = New System.Drawing.Size(192, 17)
        Me.pbStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pbStatus.TabIndex = 1
        '
        'dlgThemeSelect
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.CancelButton = Me.btnSkip
        Me.ClientSize = New System.Drawing.Size(636, 291)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlStatus)
        Me.Controls.Add(Me.gbSelectTheme)
        Me.Controls.Add(Me.btnSkip)
        Me.Controls.Add(Me.btnOK)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgThemeSelect"
        Me.Text = "Select Theme"
        Me.gbSelectTheme.ResumeLayout(False)
        Me.pnlStatus.ResumeLayout(False)
        Me.pnlStatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnSkip As System.Windows.Forms.Button
    Friend WithEvents gbSelectTheme As System.Windows.Forms.GroupBox
    Friend WithEvents lvThemes As System.Windows.Forms.ListView
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents pbStatus As System.Windows.Forms.ProgressBar
    Friend WithEvents colNumber As ColumnHeader
    Friend WithEvents colURL As ColumnHeader
    Friend WithEvents colWebURL As ColumnHeader
    Friend WithEvents colDescription As ColumnHeader
    Friend WithEvents colBitrate As ColumnHeader
    Friend WithEvents colDuration As ColumnHeader
    Friend WithEvents colExtension As ColumnHeader
    Friend WithEvents colSource As ColumnHeader
    Friend WithEvents colScraper As ColumnHeader
End Class
