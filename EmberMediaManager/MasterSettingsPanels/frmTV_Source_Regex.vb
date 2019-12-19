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

Public Class frmTV_Source_Regex
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
            .Contains = Enums.SettingsPanelType.None,
            .ImageIndex = 5,
            .Order = 200,
            .Panel = pnlSettings,
            .SettingsPanelID = "TV_Source_Regex",
            .Title = Master.eLang.GetString(699, "Regex"),
            .Type = Enums.SettingsPanelType.TVSource
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings.TVEpisode.SourceSettings
            .EpisodeMultiPartMatching.Regex = txtEpisodeMultipartMatching.Text
        End With
        Save_EpisodeMatching()
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.TVEpisode.SourceSettings
            txtEpisodeMultipartMatching.Text = .EpisodeMultiPartMatching.Regex

            DataGridView_Fill_EpisodeMatching(.EpisodeMatching)
        End With
    End Sub

    Private Sub Setup()
        With Master.eLang
            colEpisodeMatchingByDate.HeaderText = .GetString(698, "by Date")
            colEpisodeMatchingDefaultSeason.HeaderText = .GetString(695, "Default Season")
            colEpisodeMatchingRegex.HeaderText = .GetString(699, "Regex")
            gbEpisodeMatching.Text = .GetString(670, "Episode Matching")
            gbEpisodeMultipartMatching.Text = .GetString(671, "Episode Multipart Matching")
        End With
    End Sub

    Private Sub DataGridView_Fill_EpisodeMatching(ByVal List As EpisodeMatchingSpecification)
        dgvEpisodeMatching.Rows.Clear()
        Dim i As Integer
        For Each item In List
            dgvEpisodeMatching.Rows.Add(New Object() {
                                        i,
                                        item.RegExp,
                                        item.DefaultSeason,
                                        item.ByDate
                                        })
            i += 1
        Next
        dgvEpisodeMatching.Sort(dgvEpisodeMatching.Columns(0), ComponentModel.ListSortDirection.Ascending)
        dgvEpisodeMatching.ClearSelection()
    End Sub

    Private Sub DataGridView_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvEpisodeMatching.KeyDown
        Dim dgvList As DataGridView = DirectCast(sender, DataGridView)
        Dim currRowIndex As Integer = dgvList.CurrentRow.Index
        Dim currRowIsNew As Boolean = dgvList.CurrentRow.IsNewRow
        If Not currRowIsNew Then
            Select Case True
                Case e.Alt And e.KeyCode = Keys.Up AndAlso Not currRowIndex = 0
                    dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) - 1
                    dgvList.Rows(currRowIndex - 1).Cells(0).Value = currRowIndex
                    currRowIndex -= 1
                    e.Handled = True
                Case e.Alt And e.KeyCode = Keys.Down AndAlso Not currRowIndex = dgvList.Rows.Count - 1 AndAlso Not dgvList.Rows(currRowIndex + 1).IsNewRow
                    dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) + 1
                    dgvList.Rows(currRowIndex + 1).Cells(0).Value = currRowIndex
                    currRowIndex += 1
                    e.Handled = True
                Case e.Alt And e.KeyCode = Keys.Down
                    e.Handled = True
                    Return
                Case Else
                    Return
            End Select
            dgvList.Sort(dgvList.Columns(0), ComponentModel.ListSortDirection.Ascending)
            If Not dgvList.SelectedRows(0).State.HasFlag(DataGridViewElementStates.Displayed) Then
                dgvList.FirstDisplayedScrollingRowIndex = currRowIndex
            End If
        End If
    End Sub

    Private Sub LoadDefaults_EpisodeMatching() Handles btnEpisodeMatchingDefaults.Click
        If MessageBox.Show(Master.eLang.GetString(844, "Are you sure you want to reset to the default list of Episode Matching?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_EpisodeMatching(Master.eSettings.TVEpisode.SourceSettings.EpisodeMatching.GetDefaults(Enums.ContentType.TVEpisode))
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub LoadDefaults_EpisodeMultipartMatching() Handles btnEpisodeMultipartMatchingDefaults.Click
        txtEpisodeMultipartMatching.Text = Master.eSettings.TVEpisode.SourceSettings.EpisodeMultiPartMatching.GetDefaults(Enums.ContentType.TVEpisode)
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Save_EpisodeMatching()
        With Master.eSettings.TVEpisode.SourceSettings.EpisodeMatching
            .Clear()
            For Each r As DataGridViewRow In dgvEpisodeMatching.Rows
                If Not r.IsNewRow AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString.Trim) Then
                    .Add(New EpisodeMatchingSpecificationItem With {
                         .ByDate = DirectCast(r.Cells(3).Value, Boolean),
                         .DefaultSeason = If(Integer.TryParse(r.Cells(2).Value.ToString, 0), CInt(r.Cells(2).Value), -1),
                         .RegExp = r.Cells(1).Value.ToString.Trim
                         })
                End If
            Next
        End With
    End Sub

#End Region 'Methods

End Class