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
Imports System.Text.RegularExpressions

Public Class dlgSettingsSeasonTitleBlacklist

#Region "Fields"

#End Region 'Fields

#Region "Properties"

    Public Property Result As List(Of String) = New List(Of String)

#End Region 'Properties

#Region "Dialog"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormsUtils.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Data_Fill()
    End Sub

    Private Sub DialogResult_Cancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_OK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Data_Save()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Setup()
        Text = Master.eLang.GetString(1113, "Season Title Blacklist")
        btnCancel.Text = Master.eLang.Cancel
        btnOK.Text = Master.eLang.OK
        btnSetDefaults.Text = Master.eLang.GetString(713, "Defaults")
        lblHint.Text = Master.eLang.GetString(1114, "This list contains patterns of season titles that should be ignored when scraping.{0}Use %{season_number} to mark the location of the season number.").Replace("{0}", Environment.NewLine)
    End Sub

    Public Overloads Function ShowDialog(ByVal SeasonTitleBlacklist As List(Of String)) As DialogResult
        Result = SeasonTitleBlacklist
        Setup()
        Return ShowDialog()
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Sub Data_Fill()
        dgvBlacklist.Rows.Clear()
        For Each v In Result
            dgvBlacklist.Rows.Add(New Object() {v})
        Next
        dgvBlacklist.ClearSelection()
    End Sub

    Private Sub Data_Save()
        Result = DataGridView_RowsToList(dgvBlacklist)
    End Sub

    Private Function DataGridView_RowsToList(dgv As DataGridView) As List(Of String)
        Dim newList As New List(Of String)
        For Each r As DataGridViewRow In dgv.Rows
            If r.Cells(0).Value IsNot Nothing Then newList.Add(r.Cells(0).Value.ToString)
        Next
        Return newList
    End Function

    Private Sub btnSetDefaults_Click(sender As Object, e As EventArgs) Handles btnSetDefaults.Click
        Master.eSettings.SetDefaultsForLists(Enums.DefaultType.TVSeasonTitleBlacklist, True)
        Result = Master.eSettings.TVScraperSeasonTitleBlacklist
        Data_Fill()
    End Sub

#End Region 'Methods

End Class