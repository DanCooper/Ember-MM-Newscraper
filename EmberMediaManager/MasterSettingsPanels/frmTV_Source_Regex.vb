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

#Region "Fields"

    Private TVShowMatching As New List(Of Settings.regexp)

#End Region 'Fields

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
            .MultiPartMatching = txtTVSourcesRegexMultiPartMatching.Text
        End With

        With Master.eSettings
            .TVShowMatching.Clear()
            .TVShowMatching.AddRange(TVShowMatching)
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.TVEpisode.SourceSettings
            txtTVSourcesRegexMultiPartMatching.Text = .MultiPartMatching
        End With
        With Master.eSettings
            TVShowMatching.AddRange(.TVShowMatching)
            LoadTVShowMatching()
        End With
    End Sub

    Private Sub Setup()
        btnTVSourcesRegexTVShowMatchingAdd.Tag = String.Empty
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(690, "Edit Regex")
        btnTVSourcesRegexTVShowMatchingClear.Text = Master.eLang.GetString(123, "Clear")
        btnTVSourcesRegexTVShowMatchingEdit.Text = Master.eLang.GetString(690, "Edit Regex")
        btnTVSourcesRegexTVShowMatchingRemove.Text = Master.eLang.GetString(30, "Remove")
        gbTVSourcesRegexTVShowMatching.Text = Master.eLang.GetString(691, "Show Match Regex")
        lblTVSourcesRegexTVShowMatchingByDate.Text = Master.eLang.GetString(698, "by Date")
        lblTVSourcesRegexTVShowMatchingRegex.Text = Master.eLang.GetString(699, "Regex")
        lblTVSourcesRegexTVShowMatchingDefaultSeason.Text = Master.eLang.GetString(695, "Default Season")
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        If String.IsNullOrEmpty(btnTVSourcesRegexTVShowMatchingAdd.Tag.ToString) Then
            Dim lID = (From lRegex As Settings.regexp In TVShowMatching Select lRegex.ID).Max
            TVShowMatching.Add(New Settings.regexp With {
                               .ID = Convert.ToInt32(lID) + 1,
                               .Regexp = txtTVSourcesRegexTVShowMatchingRegex.Text,
                               .defaultSeason = If(String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text) OrElse Not Integer.TryParse(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text, 0), -2, CInt(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text)),
                               .byDate = chkTVSourcesRegexTVShowMatchingByDate.Checked})
        Else
            Dim selRex = From lRegex As Settings.regexp In TVShowMatching Where lRegex.ID = Convert.ToInt32(btnTVSourcesRegexTVShowMatchingAdd.Tag)
            If selRex.Count > 0 Then
                selRex(0).Regexp = txtTVSourcesRegexTVShowMatchingRegex.Text
                selRex(0).defaultSeason = CInt(If(String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text), "-2", txtTVSourcesRegexTVShowMatchingDefaultSeason.Text))
                selRex(0).byDate = chkTVSourcesRegexTVShowMatchingByDate.Checked
            End If
        End If

        ClearTVShowMatching()
        LoadTVShowMatching()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingClear_Click(ByVal sender As Object, ByVal e As EventArgs)
        ClearTVShowMatching()
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        If lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 Then EditTVShowMatching(lvTVSourcesRegexTVShowMatching.SelectedItems(0))
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingUp_Click(ByVal sender As Object, ByVal e As EventArgs)
        If lvTVSourcesRegexTVShowMatching.Items.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 AndAlso Not lvTVSourcesRegexTVShowMatching.SelectedItems(0).Index = 0 Then
            Dim selItem As Settings.regexp = TVShowMatching.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(lvTVSourcesRegexTVShowMatching.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvTVSourcesRegexTVShowMatching.SuspendLayout()
                Dim iIndex As Integer = TVShowMatching.IndexOf(selItem)
                Dim selIndex As Integer = lvTVSourcesRegexTVShowMatching.SelectedIndices(0)
                TVShowMatching.Remove(selItem)
                TVShowMatching.Insert(iIndex - 1, selItem)

                RenumberTVShowMatching()
                LoadTVShowMatching()

                lvTVSourcesRegexTVShowMatching.Items(selIndex - 1).Selected = True
                lvTVSourcesRegexTVShowMatching.ResumeLayout()
            End If

            lvTVSourcesRegexTVShowMatching.Focus()
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingDown_Click(ByVal sender As Object, ByVal e As EventArgs)
        If lvTVSourcesRegexTVShowMatching.Items.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 AndAlso lvTVSourcesRegexTVShowMatching.SelectedItems(0).Index < (lvTVSourcesRegexTVShowMatching.Items.Count - 1) Then
            Dim selItem As Settings.regexp = TVShowMatching.FirstOrDefault(Function(r) r.ID = Convert.ToInt32(lvTVSourcesRegexTVShowMatching.SelectedItems(0).Text))

            If selItem IsNot Nothing Then
                lvTVSourcesRegexTVShowMatching.SuspendLayout()
                Dim iIndex As Integer = TVShowMatching.IndexOf(selItem)
                Dim selIndex As Integer = lvTVSourcesRegexTVShowMatching.SelectedIndices(0)
                TVShowMatching.Remove(selItem)
                TVShowMatching.Insert(iIndex + 1, selItem)

                RenumberTVShowMatching()
                LoadTVShowMatching()

                lvTVSourcesRegexTVShowMatching.Items(selIndex + 1).Selected = True
                lvTVSourcesRegexTVShowMatching.ResumeLayout()
            End If

            lvTVSourcesRegexTVShowMatching.Focus()
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingReset_Click(ByVal sender As Object, ByVal e As EventArgs)
        If MessageBox.Show(Master.eLang.GetString(844, "Are you sure you want to reset to the default list of show regex?"), Master.eLang.GetString(104, "Are You Sure?"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVShowMatching, True)
            TVShowMatching.Clear()
            TVShowMatching.AddRange(Master.eSettings.TVShowMatching)
            LoadTVShowMatching()
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub btnTVSourcesRegexMultiPartMatchingReset_Click(ByVal sender As Object, ByVal e As EventArgs)
        txtTVSourcesRegexMultiPartMatching.Text = "^[-_ex]+([0-9]+(?:(?:[a-i]|\.[1-9])(?![0-9]))?)"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub btnTVSourcesRegexTVShowMatchingRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        RemoveTVShowMatching()
    End Sub

    Private Sub ClearTVShowMatching()
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(115, "Add Regex")
        btnTVSourcesRegexTVShowMatchingAdd.Tag = String.Empty
        btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        txtTVSourcesRegexTVShowMatchingRegex.Text = String.Empty
        txtTVSourcesRegexTVShowMatchingDefaultSeason.Text = String.Empty
        chkTVSourcesRegexTVShowMatchingByDate.Checked = False
    End Sub

    Private Sub EditTVShowMatching(ByVal lItem As ListViewItem)
        btnTVSourcesRegexTVShowMatchingAdd.Text = Master.eLang.GetString(124, "Update Regex")
        btnTVSourcesRegexTVShowMatchingAdd.Tag = lItem.Text

        txtTVSourcesRegexTVShowMatchingRegex.Text = lItem.SubItems(1).Text.ToString
        txtTVSourcesRegexTVShowMatchingDefaultSeason.Text = If(Not lItem.SubItems(2).Text = "-2", lItem.SubItems(2).Text, String.Empty)

        Select Case lItem.SubItems(3).Text
            Case "Yes"
                chkTVSourcesRegexTVShowMatchingByDate.Checked = True
            Case "No"
                chkTVSourcesRegexTVShowMatchingByDate.Checked = False
        End Select
    End Sub

    Private Sub LoadTVShowMatching()
        Dim lvItem As ListViewItem
        lvTVSourcesRegexTVShowMatching.Items.Clear()
        For Each rShow As Settings.regexp In TVShowMatching
            lvItem = New ListViewItem(rShow.ID.ToString)
            lvItem.SubItems.Add(rShow.Regexp)
            lvItem.SubItems.Add(If(Not rShow.defaultSeason.ToString = "-2", rShow.defaultSeason.ToString, String.Empty))
            lvItem.SubItems.Add(If(rShow.byDate, "Yes", "No"))
            lvTVSourcesRegexTVShowMatching.Items.Add(lvItem)
        Next
    End Sub

    Private Sub RemoveTVShowMatching()
        Dim ID As Integer
        For Each lItem As ListViewItem In lvTVSourcesRegexTVShowMatching.SelectedItems
            ID = Convert.ToInt32(lItem.Text)
            Dim selRex = From lRegex As Settings.regexp In TVShowMatching Where lRegex.ID = ID
            If selRex.Count > 0 Then
                TVShowMatching.Remove(selRex(0))
                RaiseEvent SettingsChanged()
            End If
        Next
        LoadTVShowMatching()
    End Sub

    Private Sub txtTVSourcesRegexTVShowMatchingRegex_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        ValidateTVShowMatching()
    End Sub

    Private Sub txtTVSourcesRegexTVShowMatchingDefaultSeason_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        ValidateTVShowMatching()
    End Sub

    Private Sub ValidateTVShowMatching()
        If Not String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingRegex.Text) AndAlso
            (String.IsNullOrEmpty(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text.Trim) OrElse Integer.TryParse(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text, 0) AndAlso
            CInt(txtTVSourcesRegexTVShowMatchingDefaultSeason.Text.Trim) >= 0) Then
            btnTVSourcesRegexTVShowMatchingAdd.Enabled = True
        Else
            btnTVSourcesRegexTVShowMatchingAdd.Enabled = False
        End If
    End Sub

    Private Sub RenumberTVShowMatching()
        For i As Integer = 0 To TVShowMatching.Count - 1
            TVShowMatching(i).ID = i
        Next
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        If lvTVSourcesRegexTVShowMatching.SelectedItems.Count > 0 Then EditTVShowMatching(lvTVSourcesRegexTVShowMatching.SelectedItems(0))
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then RemoveTVShowMatching()
    End Sub

    Private Sub lvTVSourcesRegexTVShowMatching_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Not String.IsNullOrEmpty(btnTVSourcesRegexTVShowMatchingAdd.Tag.ToString) Then ClearTVShowMatching()
    End Sub

#End Region 'Methods

End Class