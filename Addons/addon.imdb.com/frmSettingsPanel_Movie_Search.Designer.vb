<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettingsPanel_Movie_Search
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
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.tblSettings = New System.Windows.Forms.TableLayoutPanel()
        Me.gbScraper = New System.Windows.Forms.GroupBox()
        Me.tblScraper = New System.Windows.Forms.TableLayoutPanel()
        Me.chkPopularTitles = New System.Windows.Forms.CheckBox()
        Me.chkPartialTitles = New System.Windows.Forms.CheckBox()
        Me.chkTvTitles = New System.Windows.Forms.CheckBox()
        Me.chkVideoTitles = New System.Windows.Forms.CheckBox()
        Me.chkShortTitles = New System.Windows.Forms.CheckBox()
        Me.pnlSettings.SuspendLayout()
        Me.tblSettings.SuspendLayout()
        Me.gbScraper.SuspendLayout()
        Me.tblScraper.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.tblSettings)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(615, 491)
        Me.pnlSettings.TabIndex = 0
        '
        'tblSettings
        '
        Me.tblSettings.AutoScroll = True
        Me.tblSettings.AutoSize = True
        Me.tblSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblSettings.ColumnCount = 2
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettings.Controls.Add(Me.gbScraper, 0, 0)
        Me.tblSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettings.Location = New System.Drawing.Point(0, 0)
        Me.tblSettings.Name = "tblSettings"
        Me.tblSettings.RowCount = 2
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettings.Size = New System.Drawing.Size(615, 491)
        Me.tblSettings.TabIndex = 99
        '
        'gbScraper
        '
        Me.gbScraper.AutoSize = True
        Me.gbScraper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gbScraper.Controls.Add(Me.tblScraper)
        Me.gbScraper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbScraper.Location = New System.Drawing.Point(3, 3)
        Me.gbScraper.Name = "gbScraper"
        Me.gbScraper.Size = New System.Drawing.Size(113, 136)
        Me.gbScraper.TabIndex = 97
        Me.gbScraper.TabStop = False
        Me.gbScraper.Text = "Scraper Options"
        '
        'tblScraper
        '
        Me.tblScraper.AutoSize = True
        Me.tblScraper.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tblScraper.ColumnCount = 2
        Me.tblScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraper.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblScraper.Controls.Add(Me.chkPopularTitles, 0, 0)
        Me.tblScraper.Controls.Add(Me.chkPartialTitles, 0, 1)
        Me.tblScraper.Controls.Add(Me.chkTvTitles, 0, 2)
        Me.tblScraper.Controls.Add(Me.chkVideoTitles, 0, 3)
        Me.tblScraper.Controls.Add(Me.chkShortTitles, 0, 4)
        Me.tblScraper.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblScraper.Location = New System.Drawing.Point(3, 18)
        Me.tblScraper.Name = "tblScraper"
        Me.tblScraper.RowCount = 5
        Me.tblScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraper.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblScraper.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblScraper.Size = New System.Drawing.Size(107, 115)
        Me.tblScraper.TabIndex = 1
        '
        'chkPopularTitles
        '
        Me.chkPopularTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPopularTitles.AutoSize = True
        Me.chkPopularTitles.Location = New System.Drawing.Point(3, 3)
        Me.chkPopularTitles.Name = "chkPopularTitles"
        Me.chkPopularTitles.Size = New System.Drawing.Size(95, 17)
        Me.chkPopularTitles.TabIndex = 0
        Me.chkPopularTitles.Text = "Popular Titles"
        Me.chkPopularTitles.UseVisualStyleBackColor = True
        '
        'chkPartialTitles
        '
        Me.chkPartialTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkPartialTitles.AutoSize = True
        Me.chkPartialTitles.Location = New System.Drawing.Point(3, 26)
        Me.chkPartialTitles.Name = "chkPartialTitles"
        Me.chkPartialTitles.Size = New System.Drawing.Size(87, 17)
        Me.chkPartialTitles.TabIndex = 1
        Me.chkPartialTitles.Text = "Partial Titles"
        Me.chkPartialTitles.UseVisualStyleBackColor = True
        '
        'chkTvTitles
        '
        Me.chkTvTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTvTitles.AutoSize = True
        Me.chkTvTitles.Location = New System.Drawing.Point(3, 49)
        Me.chkTvTitles.Name = "chkTvTitles"
        Me.chkTvTitles.Size = New System.Drawing.Size(101, 17)
        Me.chkTvTitles.TabIndex = 2
        Me.chkTvTitles.Text = "TV Movie Titles"
        Me.chkTvTitles.UseVisualStyleBackColor = True
        '
        'chkVideoTitles
        '
        Me.chkVideoTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkVideoTitles.AutoSize = True
        Me.chkVideoTitles.Location = New System.Drawing.Point(3, 72)
        Me.chkVideoTitles.Name = "chkVideoTitles"
        Me.chkVideoTitles.Size = New System.Drawing.Size(85, 17)
        Me.chkVideoTitles.TabIndex = 3
        Me.chkVideoTitles.Text = "Video Titles"
        Me.chkVideoTitles.UseVisualStyleBackColor = True
        '
        'chkShortTitles
        '
        Me.chkShortTitles.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkShortTitles.AutoSize = True
        Me.chkShortTitles.Location = New System.Drawing.Point(3, 95)
        Me.chkShortTitles.Name = "chkShortTitles"
        Me.chkShortTitles.Size = New System.Drawing.Size(83, 17)
        Me.chkShortTitles.TabIndex = 80
        Me.chkShortTitles.Text = "Short Titles"
        Me.chkShortTitles.UseVisualStyleBackColor = True
        '
        'frmSettingsPanel_Movie_Search
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(615, 491)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsPanel_Movie_Search"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scraper Setup"
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.tblSettings.ResumeLayout(False)
        Me.tblSettings.PerformLayout()
        Me.gbScraper.ResumeLayout(False)
        Me.gbScraper.PerformLayout()
        Me.tblScraper.ResumeLayout(False)
        Me.tblScraper.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents tblSettings As TableLayoutPanel
    Friend WithEvents gbScraper As GroupBox
    Friend WithEvents tblScraper As TableLayoutPanel
    Friend WithEvents chkPopularTitles As CheckBox
    Friend WithEvents chkPartialTitles As CheckBox
    Friend WithEvents chkTvTitles As CheckBox
    Friend WithEvents chkVideoTitles As CheckBox
    Friend WithEvents chkShortTitles As CheckBox
End Class
