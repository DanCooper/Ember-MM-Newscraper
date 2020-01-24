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

Public Class frmMovieset_Data
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
            .Contains = Enums.SettingsPanelType.MoviesetData,
            .ImageIndex = 3,
            .Order = 400,
            .Panel = pnlSettings,
            .SettingsPanelID = "Movieset_Data",
            .Title = Master.eLang.GetString(556, "NFO"),
            .Type = Enums.SettingsPanelType.Movieset
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings
            .MovieSetLockPlot = chkMovieSetLockPlot.Checked
            .MovieSetLockTitle = chkMovieSetLockTitle.Checked
            .MoviesetScraperPlot = chkMovieSetScraperPlot.Checked
            .MoviesetScraperTitle = chkMovieSetScraperTitle.Checked
        End With

        SaveMovieSetScraperTitleRenamer()
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            chkMovieSetLockPlot.Checked = .MovieSetLockPlot
            chkMovieSetLockTitle.Checked = .MovieSetLockTitle
            chkMovieSetScraperPlot.Checked = .MoviesetScraperPlot
            chkMovieSetScraperTitle.Checked = .MoviesetScraperTitle
        End With

        FillMovieSetScraperTitleRenamer()
    End Sub

    Private Sub Setup()
        lblMovieSetScraperGlobalHeaderLock.Text = Master.eLang.GetString(24, "Lock")
        lblMovieSetScraperGlobalPlot.Text = Master.eLang.GetString(65, "Plot")
        gbMovieSetScraperGlobalOpts.Text = Master.eLang.GetString(577, "Scraper Fields - Global")
        lblMovieSetScraperGlobalTitle.Text = Master.eLang.GetString(21, "Title")
        btnMovieSetScraperTitleRenamerAdd.Text = Master.eLang.GetString(28, "Add")
        btnMovieSetScraperTitleRenamerRemove.Text = Master.eLang.GetString(30, "Remove")
        dgvMovieSetScraperTitleRenamer.Columns(0).HeaderText = Master.eLang.GetString(1277, "From")
        dgvMovieSetScraperTitleRenamer.Columns(1).HeaderText = Master.eLang.GetString(1278, "To")
        gbMovieSetScraperTitleRenamerOpts.Text = Master.eLang.GetString(1279, "Title Renamer")

    End Sub

    Private Sub FillMovieSetScraperTitleRenamer()
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
            Dim i As Integer = dgvMovieSetScraperTitleRenamer.Rows.Add(New Object() {sett.Name.Substring(21), sett.Value})
            If Not sett.DefaultValue = String.Empty Then
                dgvMovieSetScraperTitleRenamer.Rows(i).Tag = True
                dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0).ReadOnly = True
                dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0).Style.SelectionForeColor = Color.Red
            Else
                dgvMovieSetScraperTitleRenamer.Rows(i).Tag = False
            End If
        Next
        dgvMovieSetScraperTitleRenamer.ClearSelection()
    End Sub

    Private Sub SaveMovieSetScraperTitleRenamer()
        Dim deleteitem As New List(Of String)
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
            deleteitem.Add(sett.Name)
        Next

        Using settings = New AdvancedSettings()
            For Each s As String In deleteitem
                settings.CleanSetting(s, "*EmberAPP")
            Next
            For Each r As DataGridViewRow In dgvMovieSetScraperTitleRenamer.Rows
                If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString) Then
                    settings.SetSetting(String.Concat("MovieSetTitleRenamer:", r.Cells(0).Value.ToString), r.Cells(1).Value.ToString, "*EmberAPP")
                End If
            Next
        End Using
    End Sub

    Private Sub btnMovieSetScraperMapperAdd_Click(sender As Object, e As EventArgs)
        Dim i As Integer = dgvMovieSetScraperTitleRenamer.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvMovieSetScraperTitleRenamer.Rows(i).Tag = False
        dgvMovieSetScraperTitleRenamer.CurrentCell = dgvMovieSetScraperTitleRenamer.Rows(i).Cells(0)
        dgvMovieSetScraperTitleRenamer.BeginEdit(True)
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub btnMovieSetScraperMapperRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        If dgvMovieSetScraperTitleRenamer.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMovieSetScraperTitleRenamer.Rows(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex).Tag) Then
            dgvMovieSetScraperTitleRenamer.Rows.RemoveAt(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex)
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub dgvMovieSetScraperMapper_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub dgvMovieSetScraperMapper_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs)
        If dgvMovieSetScraperTitleRenamer.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvMovieSetScraperTitleRenamer.Rows(dgvMovieSetScraperTitleRenamer.SelectedCells(0).RowIndex).Tag) Then
            btnMovieSetScraperTitleRenamerRemove.Enabled = True
        Else
            btnMovieSetScraperTitleRenamerRemove.Enabled = False
        End If
    End Sub

    Private Sub dgvMovieSetScraperMapper_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        e.Handled = e.KeyCode = Keys.Enter
    End Sub

#End Region 'Methods

End Class