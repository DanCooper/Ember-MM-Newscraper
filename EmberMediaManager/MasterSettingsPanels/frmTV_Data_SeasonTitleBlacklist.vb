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

Public Class frmTV_Data_SeasonTitleBlacklist

#Region "Properties"

    Public Property Result As New Settings.ExtendedListOfString(Enums.DefaultType.TitleBlackList_TVSeason)

#End Region 'Properties

#Region "Dialog"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        FormUtils.Forms.ResizeAndMoveDialog(Me, Me)
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        DataGridView_Fill()
    End Sub

    Private Sub DialogResult_Cancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_OK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Save_Blacklist()
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Setup()
        Text = Master.eLang.GetString(464, "Season Title Blacklist")
        btnCancel.Text = Master.eLang.Cancel
        btnOK.Text = Master.eLang.OK
        btnSetDefaults.Text = Master.eLang.GetString(713, "Defaults")
        lblHint.Text = Master.eLang.GetString(629, "This list contains patterns of season titles that should be ignored when scraping.{0}Use %{season_number} to mark the location of the season number.").Replace("{0}", Environment.NewLine)
    End Sub

    Public Overloads Function ShowDialog(ByVal list As Settings.ExtendedListOfString) As DialogResult
        Result = list
        Setup()
        Return ShowDialog()
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Sub DataGridView_Fill()
        dgvBlacklist.Rows.Clear()
        For Each v In Result
            dgvBlacklist.Rows.Add(New Object() {v})
        Next
        dgvBlacklist.ClearSelection()
    End Sub

    Private Sub Load_Defaults_Click(sender As Object, e As EventArgs) Handles btnSetDefaults.Click
        Result = New Settings.ExtendedListOfString(Enums.DefaultType.TitleBlackList_TVSeason).GetDefaults
        DataGridView_Fill()
    End Sub

    Private Sub Save_Blacklist()
        Dim newList As New Settings.ExtendedListOfString(Enums.DefaultType.TitleBlackList_TVSeason)
        For Each r As DataGridViewRow In dgvBlacklist.Rows
            If Not r.IsNewRow AndAlso r.Cells(0).Value IsNot Nothing Then newList.Add(r.Cells(0).Value.ToString.Trim)
        Next
        Result = newList
    End Sub

#End Region 'Methods

End Class