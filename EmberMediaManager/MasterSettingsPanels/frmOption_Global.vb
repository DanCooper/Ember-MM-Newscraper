' ################################################################################
' #                             EMBER MEDIA MANAGER                              #
' ################################################################################
' ################################################################################
' # This file is part of Ember Media Manager.                                    #
' #                                                                              #
' # Ember Media Manager is free software: you can redistribute it and/or modify  #
' # it under the terms of the GNU General Public License as published by         #
' # the Free Software Foundation, either version 3 of the License, or            #
' # (at your option) any later version.                                          #
' #                                                                              #
' # Ember Media Manager is distributed in the hope that it will be useful,       #
' # but WITHOUT ANY WARRANTY; without even the implied warranty of               #
' # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
' # GNU General Public License for more details.                                 #
' #                                                                              #
' # You should have received a copy of the GNU General Public License            #
' # along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
' ################################################################################

Imports EmberAPI


Public Class frmOption_Global
    Implements Interfaces.IMasterSettingsPanel

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.IMasterSettingsPanel.NeedsReload_MovieSet
    Public Event NeedsReload_TVEpisode() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.IMasterSettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IMasterSettingsPanel.SettingsChanged

#End Region 'Events

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

#End Region 'Constructors 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.IMasterSettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IMasterSettingsPanel.InjectSettingsPanel
        Settings_Load()

        Return New Containers.SettingsPanel With {
            .Contains = Enums.SettingsPanelType.OptionsGlobal,
            .ImageIndex = 0,
            .Order = 200,
            .Panel = pnlSettings,
            .SettingsPanelID = "Option_Global",
            .Title = Master.eLang.GetString(490, "Global"),
            .Type = Enums.SettingsPanelType.Options
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings.Options.Global
            .CheckForUpdates = chkCheckForUpdates.Checked
            .DigitGrpSymbolVotesEnabled = chkDigitGrpSymbolVotes.Checked
            .ShowNews = chkShowNews.Checked
        End With

        With Master.eSettings
            .GeneralImageFilter = chkImageFilter.Checked
            .GeneralImageFilterAutoscraper = chkImageFilterAutoscraper.Checked
            .GeneralImageFilterFanart = chkImageFilterFanart.Checked
            .GeneralImageFilterImagedialog = chklImageFilterImageDialog.Checked
            .GeneralImageFilterPoster = chkImageFilterPoster.Checked
            If Not String.IsNullOrEmpty(txtImageFilterFanartMatchRate.Text) AndAlso Integer.TryParse(txtImageFilterFanartMatchRate.Text, 4) Then
                .GeneralImageFilterFanartMatchTolerance = Convert.ToInt32(txtImageFilterFanartMatchRate.Text)
            Else
                .GeneralImageFilterFanartMatchTolerance = 4
            End If
            If Not String.IsNullOrEmpty(txtImageFilterPosterMatchRate.Text) AndAlso Integer.TryParse(txtImageFilterPosterMatchRate.Text, 1) Then
                .GeneralImageFilterPosterMatchTolerance = Convert.ToInt32(txtImageFilterPosterMatchRate.Text)
            Else
                .GeneralImageFilterPosterMatchTolerance = 1
            End If
        End With
        Save_SortTokens()
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.Options.Global
            chkCheckForUpdates.Checked = .CheckForUpdates
            chkDigitGrpSymbolVotes.Checked = .DigitGrpSymbolVotesEnabled
            chkShowNews.Checked = .ShowNews

            DataGridView_Fill_SortTokens(.SortTokens)
        End With
        With Master.eSettings
            chkImageFilter.Checked = .GeneralImageFilter
            chkImageFilterAutoscraper.Checked = .GeneralImageFilterAutoscraper
            chkImageFilterFanart.Checked = .GeneralImageFilterFanart
            chkImageFilterPoster.Checked = .GeneralImageFilterPoster
            chklImageFilterImageDialog.Checked = .GeneralImageFilterImagedialog
            txtImageFilterFanartMatchRate.Enabled = .GeneralImageFilterFanart
            txtImageFilterFanartMatchRate.Text = .GeneralImageFilterFanartMatchTolerance.ToString
            txtImageFilterPosterMatchRate.Enabled = .GeneralImageFilterPoster
            txtImageFilterPosterMatchRate.Text = .GeneralImageFilterPosterMatchTolerance.ToString

        End With
    End Sub

    Private Sub Setup()
        With Master.eLang
            btnDigitGrpSymbolSettings.Text = .GetString(420, "Settings")
            btnSortTokensDefaults.Text = .GetString(713, "Defaults")
            chkCheckForUpdates.Text = .GetString(432, "Check for Updates")
            chkDigitGrpSymbolVotes.Text = .GetString(1387, "Use digit grouping symbol for Votes count")
            chkImageFilter.Text = .GetString(1459, "Activate ImageFilter to avoid duplicate images")
            chkImageFilterAutoscraper.Text = .GetString(1457, "Autoscraper")
            chkImageFilterFanart.Text = .GetString(149, "Fanart")
            chkImageFilterPoster.Text = .GetString(148, "Poster")
            chkShowNews.Text = .GetString(669, "Show News and Information after Start")
            chklImageFilterImageDialog.Text = .GetString(1458, "Imagedialog")
            gbMiscellaneous.Text = .GetString(429, "Miscellaneous")
            gbSortTokens.Text = .GetString(463, "Sort Tokens to Ignore")
            lblImageFilterFanartMatchRate.Text = .GetString(149, "Fanart") & " " & .GetString(461, "Mismatch Tolerance:")
            lblImageFilterPosterMatchRate.Text = .GetString(148, "Poster") & " " & .GetString(461, "Mismatch Tolerance:")
        End With
    End Sub
    Private Sub Enable_ApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        chkCheckForUpdates.CheckedChanged,
        chkDigitGrpSymbolVotes.CheckedChanged,
        dgvSortTokens.CellValueChanged,
        dgvSortTokens.RowsAdded,
        dgvSortTokens.RowsRemoved

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_Fill_SortTokens(ByVal List As List(Of String))
        dgvSortTokens.Rows.Clear()
        For Each token In List
            dgvSortTokens.Rows.Add(New Object() {token})
        Next
        dgvSortTokens.ClearSelection()
    End Sub

    Private Sub DigitalGroupSymbolSettings_Click(sender As Object, e As EventArgs) Handles btnDigitGrpSymbolSettings.Click
        Process.Start("INTL.CPL")
    End Sub

    Private Sub ImageFilter_AutoScraper_CheckedChanged(sender As Object, e As EventArgs) Handles chkImageFilterAutoscraper.CheckedChanged
        If chklImageFilterImageDialog.Checked = False AndAlso chkImageFilterAutoscraper.Checked = False Then
            chkImageFilterPoster.Enabled = False
            chkImageFilterFanart.Enabled = False
        Else
            chkImageFilterPoster.Enabled = True
            chkImageFilterFanart.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ImageFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkImageFilter.CheckedChanged
        chkImageFilterAutoscraper.Enabled = chkImageFilter.Checked
        chkImageFilterFanart.Enabled = chkImageFilter.Checked
        chklImageFilterImageDialog.Enabled = chkImageFilter.Checked
        chkImageFilterPoster.Enabled = chkImageFilter.Checked
        lblImageFilterFanartMatchRate.Enabled = chkImageFilter.Checked
        lblImageFilterPosterMatchRate.Enabled = chkImageFilter.Checked
        txtImageFilterFanartMatchRate.Enabled = chkImageFilter.Checked
        txtImageFilterPosterMatchRate.Enabled = chkImageFilter.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ImageFilter_Fanart_CheckedChanged(sender As Object, e As EventArgs) Handles chkImageFilterFanart.CheckedChanged
        lblImageFilterFanartMatchRate.Enabled = chkImageFilterFanart.Checked
        txtImageFilterFanartMatchRate.Enabled = chkImageFilterFanart.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ImageFilter_FanartMatchRate_TextChanged(sender As Object, e As EventArgs) Handles txtImageFilterFanartMatchRate.TextChanged
        If chkImageFilter.Checked Then
            Dim txtbox As TextBox = CType(sender, TextBox)
            Dim matchfactor As Integer
            Dim bBadValue As Boolean
            If Integer.TryParse(txtbox.Text, matchfactor) Then
                If matchfactor < 0 OrElse matchfactor > 10 Then
                    bBadValue = True
                End If
            Else
                bBadValue = True
            End If
            If bBadValue Then
                txtbox.Text = String.Empty
                MessageBox.Show(Master.eLang.GetString(1460, "Match Tolerance should be between 0 - 10 | 0 = 100% identical images, 10 = different images"),
                                Master.eLang.GetString(356, "Warning"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            End If
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ImageFilter_Poster_CheckedChanged(sender As Object, e As EventArgs) Handles chkImageFilterPoster.CheckedChanged
        lblImageFilterPosterMatchRate.Enabled = chkImageFilterPoster.Checked
        txtImageFilterPosterMatchRate.Enabled = chkImageFilterPoster.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub LoadDefaults_SortTokens() Handles btnSortTokensDefaults.Click
        DataGridView_Fill_SortTokens(Master.eSettings.Options.Global.SortTokens.GetDefaults(Enums.DefaultType.SortTokens))
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Save_SortTokens()
        With Master.eSettings.Options.Global.SortTokens
            .Clear()
            For Each r As DataGridViewRow In dgvSortTokens.Rows
                If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString.Trim) Then
                    .Add(r.Cells(0).Value.ToString.Trim)
                End If
            Next
        End With
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        txtImageFilterFanartMatchRate.KeyPress,
        txtImageFilterPosterMatchRate.KeyPress

        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

#End Region 'Methods

End Class