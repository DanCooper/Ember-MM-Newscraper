<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettingsHolder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlSettingsTop = New System.Windows.Forms.Panel()
        Me.tblSettingsTop = New System.Windows.Forms.TableLayoutPanel()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.tblSettingsMain = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGetWatchedState = New System.Windows.Forms.GroupBox()
        Me.tblGetWatchedState = New System.Windows.Forms.TableLayoutPanel()
        Me.gbGetWatchedStateTVEpisodes = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGetWatchedStateBeforeEdit_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperMulti_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperSingle_TVEpisode = New System.Windows.Forms.CheckBox()
        Me.gbGetWatchedStateMovies = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkGetWatchedStateBeforeEdit_Movie = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperMulti_Movie = New System.Windows.Forms.CheckBox()
        Me.chkGetWatchedStateScraperSingle_Movie = New System.Windows.Forms.CheckBox()
        Me.pnlSettingsMain = New System.Windows.Forms.Panel()
        Me.pnlSettings = New System.Windows.Forms.Panel()
        Me.pnlSettingsTop.SuspendLayout()
        Me.tblSettingsTop.SuspendLayout()
        Me.tblSettingsMain.SuspendLayout()
        Me.gbGetWatchedState.SuspendLayout()
        Me.tblGetWatchedState.SuspendLayout()
        Me.gbGetWatchedStateTVEpisodes.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.gbGetWatchedStateMovies.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.pnlSettingsMain.SuspendLayout()
        Me.pnlSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSettingsTop
        '
        Me.pnlSettingsTop.AutoSize = True
        Me.pnlSettingsTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSettingsTop.Controls.Add(Me.tblSettingsTop)
        Me.pnlSettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettingsTop.Name = "pnlSettingsTop"
        Me.pnlSettingsTop.Size = New System.Drawing.Size(423, 23)
        Me.pnlSettingsTop.TabIndex = 0
        '
        'tblSettingsTop
        '
        Me.tblSettingsTop.AutoSize = True
        Me.tblSettingsTop.ColumnCount = 2
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsTop.Controls.Add(Me.chkEnabled, 0, 0)
        Me.tblSettingsTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsTop.Name = "tblSettingsTop"
        Me.tblSettingsTop.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tblSettingsTop.RowCount = 2
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsTop.Size = New System.Drawing.Size(423, 23)
        Me.tblSettingsTop.TabIndex = 21
        '
        'chkEnabled
        '
        Me.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(8, 3)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(68, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'tblSettingsMain
        '
        Me.tblSettingsMain.AutoScroll = True
        Me.tblSettingsMain.AutoSize = True
        Me.tblSettingsMain.ColumnCount = 2
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblSettingsMain.Controls.Add(Me.gbGetWatchedState, 0, 2)
        Me.tblSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblSettingsMain.Location = New System.Drawing.Point(0, 0)
        Me.tblSettingsMain.Name = "tblSettingsMain"
        Me.tblSettingsMain.RowCount = 4
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblSettingsMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tblSettingsMain.Size = New System.Drawing.Size(423, 285)
        Me.tblSettingsMain.TabIndex = 21
        '
        'gbGetWatchedState
        '
        Me.gbGetWatchedState.AutoSize = True
        Me.gbGetWatchedState.Controls.Add(Me.tblGetWatchedState)
        Me.gbGetWatchedState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedState.Location = New System.Drawing.Point(3, 3)
        Me.gbGetWatchedState.Name = "gbGetWatchedState"
        Me.gbGetWatchedState.Size = New System.Drawing.Size(334, 117)
        Me.gbGetWatchedState.TabIndex = 55
        Me.gbGetWatchedState.TabStop = False
        Me.gbGetWatchedState.Text = "Get Watched State"
        '
        'tblGetWatchedState
        '
        Me.tblGetWatchedState.AutoSize = True
        Me.tblGetWatchedState.ColumnCount = 3
        Me.tblGetWatchedState.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGetWatchedState.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGetWatchedState.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblGetWatchedState.Controls.Add(Me.gbGetWatchedStateTVEpisodes, 1, 0)
        Me.tblGetWatchedState.Controls.Add(Me.gbGetWatchedStateMovies, 0, 0)
        Me.tblGetWatchedState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblGetWatchedState.Location = New System.Drawing.Point(3, 18)
        Me.tblGetWatchedState.Name = "tblGetWatchedState"
        Me.tblGetWatchedState.RowCount = 2
        Me.tblGetWatchedState.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGetWatchedState.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblGetWatchedState.Size = New System.Drawing.Size(328, 96)
        Me.tblGetWatchedState.TabIndex = 7
        '
        'gbGetWatchedStateTVEpisodes
        '
        Me.gbGetWatchedStateTVEpisodes.AutoSize = True
        Me.gbGetWatchedStateTVEpisodes.Controls.Add(Me.TableLayoutPanel3)
        Me.gbGetWatchedStateTVEpisodes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedStateTVEpisodes.Location = New System.Drawing.Point(167, 3)
        Me.gbGetWatchedStateTVEpisodes.Name = "gbGetWatchedStateTVEpisodes"
        Me.gbGetWatchedStateTVEpisodes.Size = New System.Drawing.Size(158, 90)
        Me.gbGetWatchedStateTVEpisodes.TabIndex = 90
        Me.gbGetWatchedStateTVEpisodes.TabStop = False
        Me.gbGetWatchedStateTVEpisodes.Text = "Episodes"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.AutoSize = True
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.Controls.Add(Me.chkGetWatchedStateBeforeEdit_TVEpisode, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.chkGetWatchedStateScraperMulti_TVEpisode, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.chkGetWatchedStateScraperSingle_TVEpisode, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(152, 69)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'chkGetWatchedStateBeforeEdit_TVEpisode
        '
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.AutoSize = True
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Location = New System.Drawing.Point(3, 3)
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Name = "chkGetWatchedStateBeforeEdit_TVEpisode"
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Size = New System.Drawing.Size(82, 17)
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.TabIndex = 87
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.Text = "Before Edit"
        Me.chkGetWatchedStateBeforeEdit_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkGetWatchedStateScraperMulti_TVEpisode
        '
        Me.chkGetWatchedStateScraperMulti_TVEpisode.AutoSize = True
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Location = New System.Drawing.Point(3, 49)
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Name = "chkGetWatchedStateScraperMulti_TVEpisode"
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Size = New System.Drawing.Size(141, 17)
        Me.chkGetWatchedStateScraperMulti_TVEpisode.TabIndex = 87
        Me.chkGetWatchedStateScraperMulti_TVEpisode.Text = "During Multi-Scraping"
        Me.chkGetWatchedStateScraperMulti_TVEpisode.UseVisualStyleBackColor = True
        '
        'chkGetWatchedStateScraperSingle_TVEpisode
        '
        Me.chkGetWatchedStateScraperSingle_TVEpisode.AutoSize = True
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Location = New System.Drawing.Point(3, 26)
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Name = "chkGetWatchedStateScraperSingle_TVEpisode"
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Size = New System.Drawing.Size(146, 17)
        Me.chkGetWatchedStateScraperSingle_TVEpisode.TabIndex = 87
        Me.chkGetWatchedStateScraperSingle_TVEpisode.Text = "During Single-Scraping"
        Me.chkGetWatchedStateScraperSingle_TVEpisode.UseVisualStyleBackColor = True
        '
        'gbGetWatchedStateMovies
        '
        Me.gbGetWatchedStateMovies.AutoSize = True
        Me.gbGetWatchedStateMovies.Controls.Add(Me.TableLayoutPanel2)
        Me.gbGetWatchedStateMovies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbGetWatchedStateMovies.Location = New System.Drawing.Point(3, 3)
        Me.gbGetWatchedStateMovies.Name = "gbGetWatchedStateMovies"
        Me.gbGetWatchedStateMovies.Size = New System.Drawing.Size(158, 90)
        Me.gbGetWatchedStateMovies.TabIndex = 89
        Me.gbGetWatchedStateMovies.TabStop = False
        Me.gbGetWatchedStateMovies.Text = "Movies"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.chkGetWatchedStateBeforeEdit_Movie, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.chkGetWatchedStateScraperMulti_Movie, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.chkGetWatchedStateScraperSingle_Movie, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(152, 69)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'chkGetWatchedStateBeforeEdit_Movie
        '
        Me.chkGetWatchedStateBeforeEdit_Movie.AutoSize = True
        Me.chkGetWatchedStateBeforeEdit_Movie.Location = New System.Drawing.Point(3, 3)
        Me.chkGetWatchedStateBeforeEdit_Movie.Name = "chkGetWatchedStateBeforeEdit_Movie"
        Me.chkGetWatchedStateBeforeEdit_Movie.Size = New System.Drawing.Size(82, 17)
        Me.chkGetWatchedStateBeforeEdit_Movie.TabIndex = 87
        Me.chkGetWatchedStateBeforeEdit_Movie.Text = "Before Edit"
        Me.chkGetWatchedStateBeforeEdit_Movie.UseVisualStyleBackColor = True
        '
        'chkGetWatchedStateScraperMulti_Movie
        '
        Me.chkGetWatchedStateScraperMulti_Movie.AutoSize = True
        Me.chkGetWatchedStateScraperMulti_Movie.Location = New System.Drawing.Point(3, 49)
        Me.chkGetWatchedStateScraperMulti_Movie.Name = "chkGetWatchedStateScraperMulti_Movie"
        Me.chkGetWatchedStateScraperMulti_Movie.Size = New System.Drawing.Size(141, 17)
        Me.chkGetWatchedStateScraperMulti_Movie.TabIndex = 87
        Me.chkGetWatchedStateScraperMulti_Movie.Text = "During Multi-Scraping"
        Me.chkGetWatchedStateScraperMulti_Movie.UseVisualStyleBackColor = True
        '
        'chkGetWatchedStateScraperSingle_Movie
        '
        Me.chkGetWatchedStateScraperSingle_Movie.AutoSize = True
        Me.chkGetWatchedStateScraperSingle_Movie.Location = New System.Drawing.Point(3, 26)
        Me.chkGetWatchedStateScraperSingle_Movie.Name = "chkGetWatchedStateScraperSingle_Movie"
        Me.chkGetWatchedStateScraperSingle_Movie.Size = New System.Drawing.Size(146, 17)
        Me.chkGetWatchedStateScraperSingle_Movie.TabIndex = 87
        Me.chkGetWatchedStateScraperSingle_Movie.Text = "During Single-Scraping"
        Me.chkGetWatchedStateScraperSingle_Movie.UseVisualStyleBackColor = True
        '
        'pnlSettingsMain
        '
        Me.pnlSettingsMain.AutoSize = True
        Me.pnlSettingsMain.Controls.Add(Me.tblSettingsMain)
        Me.pnlSettingsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettingsMain.Location = New System.Drawing.Point(0, 23)
        Me.pnlSettingsMain.Name = "pnlSettingsMain"
        Me.pnlSettingsMain.Size = New System.Drawing.Size(423, 285)
        Me.pnlSettingsMain.TabIndex = 1
        '
        'pnlSettings
        '
        Me.pnlSettings.AutoSize = True
        Me.pnlSettings.Controls.Add(Me.pnlSettingsMain)
        Me.pnlSettings.Controls.Add(Me.pnlSettingsTop)
        Me.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSettings.Location = New System.Drawing.Point(0, 0)
        Me.pnlSettings.Name = "pnlSettings"
        Me.pnlSettings.Size = New System.Drawing.Size(423, 308)
        Me.pnlSettings.TabIndex = 1
        '
        'frmSettingsHolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(423, 308)
        Me.Controls.Add(Me.pnlSettings)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettingsHolder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmSettingsHolder"
        Me.pnlSettingsTop.ResumeLayout(False)
        Me.pnlSettingsTop.PerformLayout()
        Me.tblSettingsTop.ResumeLayout(False)
        Me.tblSettingsTop.PerformLayout()
        Me.tblSettingsMain.ResumeLayout(False)
        Me.tblSettingsMain.PerformLayout()
        Me.gbGetWatchedState.ResumeLayout(False)
        Me.gbGetWatchedState.PerformLayout()
        Me.tblGetWatchedState.ResumeLayout(False)
        Me.tblGetWatchedState.PerformLayout()
        Me.gbGetWatchedStateTVEpisodes.ResumeLayout(False)
        Me.gbGetWatchedStateTVEpisodes.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.gbGetWatchedStateMovies.ResumeLayout(False)
        Me.gbGetWatchedStateMovies.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.pnlSettingsMain.ResumeLayout(False)
        Me.pnlSettingsMain.PerformLayout()
        Me.pnlSettings.ResumeLayout(False)
        Me.pnlSettings.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlSettingsTop As System.Windows.Forms.Panel
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents tblSettingsTop As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettingsMain As System.Windows.Forms.Panel
    Friend WithEvents tblSettingsMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlSettings As System.Windows.Forms.Panel
    Friend WithEvents gbGetWatchedState As System.Windows.Forms.GroupBox
    Friend WithEvents tblGetWatchedState As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents gbGetWatchedStateMovies As Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As Windows.Forms.TableLayoutPanel
    Friend WithEvents chkGetWatchedStateBeforeEdit_Movie As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedStateScraperMulti_Movie As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedStateScraperSingle_Movie As Windows.Forms.CheckBox
    Friend WithEvents gbGetWatchedStateTVEpisodes As Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel3 As Windows.Forms.TableLayoutPanel
    Friend WithEvents chkGetWatchedStateBeforeEdit_TVEpisode As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedStateScraperMulti_TVEpisode As Windows.Forms.CheckBox
    Friend WithEvents chkGetWatchedStateScraperSingle_TVEpisode As Windows.Forms.CheckBox
End Class
