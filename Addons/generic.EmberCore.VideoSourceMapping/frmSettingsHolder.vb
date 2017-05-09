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

Imports System.Windows.Forms
Imports EmberAPI

Public Class frmSettingsHolder

#Region "Events"

    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Methods"

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Try
            ' Add any initialization after the InitializeComponent() call.
            LoadSources()

            dgvByFile.Rows.Clear()
            For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("VideoSourceByExtension:"))
                Dim i As Integer = dgvByFile.Rows.Add(New Object() {sett.Name.Substring(24), sett.Value})
            Next
            SetByFileStatus(False)
            chkMapByFile.Checked = AdvancedSettings.GetBooleanSetting("VideoSourceByExtension", False, "*EmberAPP")
        Catch ex As Exception
        End Try
        SetUp()
    End Sub

    Private Sub LoadSources()
        dgvSources.Rows.Clear()
        Dim sources As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("VideoSourceMapping", "*EmberAPP")
        If sources IsNot Nothing Then
            For Each sett In sources
                Dim i As Integer = dgvSources.Rows.Add(New Object() {sett.Name, sett.Value})
            Next
        End If
        dgvSources.ClearSelection()
    End Sub


    Private Sub btnAddSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSource.Click
        Dim i As Integer = dgvSources.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvSources.Rows(i).Tag = False
        dgvSources.CurrentCell = dgvSources.Rows(i).Cells(0)
        dgvSources.BeginEdit(True)
        RaiseEvent ModuleSettingsChanged()
    End Sub
    Private Sub btnRemoveSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSource.Click
        If dgvSources.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvSources.Rows(dgvSources.SelectedCells(0).RowIndex).Tag) Then
            dgvSources.Rows.RemoveAt(dgvSources.SelectedCells(0).RowIndex)
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub
    Private Sub dgvSources_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSources.CurrentCellDirtyStateChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub


    Private Sub dgvSources_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSources.SelectionChanged
        If dgvSources.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvSources.Rows(dgvSources.SelectedCells(0).RowIndex).Tag) Then
            btnRemoveSource.Enabled = True
        Else
            btnRemoveSource.Enabled = False
        End If
    End Sub

    Private Sub dgvSources_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvSources.KeyDown
        e.Handled = (e.KeyCode = Keys.Enter)
    End Sub

    Private Sub dgvVideo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        e.Handled = (e.KeyCode = Keys.Enter)
    End Sub

    Sub SetUp()
        btnAddSource.Text = Master.eLang.GetString(28, "Add")
        btnRemoveSource.Text = Master.eLang.GetString(30, "Remove")
        btnAddByFile.Text = Master.eLang.GetString(28, "Add")
        btnRemoveByFile.Text = Master.eLang.GetString(30, "Remove")
        btnSetDefaults.Text = Master.eLang.GetString(713, "Defaults")
        Label1.Text = Master.eLang.GetString(602, "Sources")
        Me.dgvSources.Columns(0).HeaderText = Master.eLang.GetString(763, "Search String")
        Me.dgvSources.Columns(1).HeaderText = Master.eLang.GetString(764, "Source Name")
        Me.chkMapByFile.Text = Master.eLang.GetString(765, "Map Video Source by File Extension")
        Me.dgvByFile.Columns(0).HeaderText = Master.eLang.GetString(775, "File Extension")
        Me.dgvByFile.Columns(1).HeaderText = Master.eLang.GetString(764, "Source Name")
    End Sub

    Public Sub SaveChanges()
        Dim deleteitem As New List(Of String)
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("VideoSourceByExtension:"))
            deleteitem.Add(sett.Name)
        Next
        Using settings = New AdvancedSettings()
            For Each s As String In deleteitem
                settings.CleanSetting(s, "*EmberAPP")
            Next

            Dim sources As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each r As DataGridViewRow In dgvSources.Rows
                If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso (sources.FindIndex(Function(f) f.Name = r.Cells(0).Value.ToString) = -1) Then
                    sources.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = r.Cells(0).Value.ToString, .Value = r.Cells(1).Value.ToString})
                End If
            Next
            If sources IsNot Nothing Then
                settings.SetComplexSetting("VideoSourceMapping", sources, "*EmberAPP")
            End If
            settings.SetBooleanSetting("VideoSourceByExtension", chkMapByFile.Checked, "*EmberAPP")
            For Each r As DataGridViewRow In dgvByFile.Rows
                If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso (sources.FindIndex(Function(f) f.Name = r.Cells(0).Value.ToString) = -1) Then
                    settings.SetSetting(String.Concat("VideoSourceByExtension:", r.Cells(0).Value.ToString), r.Cells(1).Value.ToString, "*EmberAPP")
                End If
            Next
        End Using
    End Sub

    Private Sub btnSetDefaults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefaults.Click
        Using settings = New AdvancedSettings()
            settings.SetDefaults("VideoSourceMapping")
        End Using
        LoadSources()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub chkMapByFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMapByFile.CheckedChanged
        SetByFileStatus(chkMapByFile.Checked)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub SetByFileStatus(ByVal b As Boolean)
        dgvByFile.ClearSelection()
        dgvByFile.Enabled = b
        btnAddByFile.Enabled = b
        btnRemoveByFile.Enabled = b
    End Sub

    Private Sub btnAddByFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddByFile.Click
        Dim i As Integer = dgvByFile.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvByFile.Rows(i).Tag = False
        dgvByFile.CurrentCell = dgvByFile.Rows(i).Cells(0)
        dgvByFile.BeginEdit(True)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnRemoveByFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveByFile.Click
        If dgvByFile.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvByFile.Rows(dgvByFile.SelectedCells(0).RowIndex).Tag) Then
            dgvByFile.Rows.RemoveAt(dgvByFile.SelectedCells(0).RowIndex)
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub

    Private Sub dgvByFile_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvByFile.CurrentCellDirtyStateChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub


    Private Sub dgvByFile_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvByFile.SelectionChanged
        If dgvByFile.Enabled AndAlso dgvByFile.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvByFile.Rows(dgvByFile.SelectedCells(0).RowIndex).Tag) Then
            btnRemoveByFile.Enabled = True
        Else
            btnRemoveByFile.Enabled = False
        End If
    End Sub

#End Region 'Methods

End Class
