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
        ' Add any initialization after the InitializeComponent() call.

        Dim formataudioconversions As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("AudioFormatConverts", "*EmberAPP")
        If formataudioconversions IsNot Nothing Then
            For Each sett In formataudioconversions
                dgvAudio.Rows.Add(New Object() {sett.Name, sett.Value})
            Next
        End If
        dgvAudio.ClearSelection()

        Dim formatvideoconversions As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("VideoFormatConverts", "*EmberAPP")
        If formatvideoconversions IsNot Nothing Then
            For Each sett In formatvideoconversions
                dgvVideo.Rows.Add(New Object() {sett.Name, sett.Value})
            Next
        End If
        dgvVideo.ClearSelection()
        SetUp()
    End Sub

    Private Sub btnAddAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAudio.Click
        Dim i As Integer = dgvAudio.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvAudio.Rows(i).Tag = False
        dgvAudio.CurrentCell = dgvAudio.Rows(i).Cells(0)
        dgvAudio.BeginEdit(True)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnAddVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddVideo.Click
        Dim i As Integer = dgvVideo.Rows.Add(New Object() {String.Empty, String.Empty})
        dgvVideo.Rows(i).Tag = False
        dgvVideo.CurrentCell = dgvVideo.Rows(i).Cells(0)
        dgvVideo.BeginEdit(True)
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnRemoveAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAudio.Click
        If dgvAudio.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvAudio.Rows(dgvAudio.SelectedCells(0).RowIndex).Tag) Then
            dgvAudio.Rows.RemoveAt(dgvAudio.SelectedCells(0).RowIndex)
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub

    Private Sub btnRemoveVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveVideo.Click
        If dgvVideo.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvVideo.Rows(dgvVideo.SelectedCells(0).RowIndex).Tag) Then
            dgvVideo.Rows.RemoveAt(dgvVideo.SelectedCells(0).RowIndex)
            RaiseEvent ModuleSettingsChanged()
        End If
    End Sub

    Private Sub btnSetDefaultsAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefaultsAudio.Click
        Using settings = New AdvancedSettings()
            settings.SetDefaults("AudioFormatConverts")
        End Using
        LoadAudio()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub btnSetDefaultsVideo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefaultsVideo.Click
        Using settings = New AdvancedSettings()
            settings.SetDefaults("VideoFormatConverts")
        End Using
        LoadVideo()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub LoadAudio()
        dgvAudio.Rows.Clear()
        Dim formatconversions As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("AudioFormatConverts", "*EmberAPP")
        If formatconversions IsNot Nothing Then
            For Each sett In formatconversions
                dgvAudio.Rows.Add(New Object() {sett.Name, sett.Value})
            Next
        End If
        dgvAudio.ClearSelection()
    End Sub

    Private Sub LoadVideo()
        dgvVideo.Rows.Clear()
        Dim formatconversions As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("VideoFormatConverts", "*EmberAPP")
        If formatconversions IsNot Nothing Then
            For Each sett In formatconversions
                dgvVideo.Rows.Add(New Object() {sett.Name, sett.Value})
            Next
        End If
        dgvVideo.ClearSelection()
    End Sub

    Private Sub dgvAudio_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvAudio.CurrentCellDirtyStateChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub dgvVideo_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvVideo.CurrentCellDirtyStateChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub dgvAudio_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvAudio.SelectionChanged
        If dgvAudio.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvAudio.Rows(dgvAudio.SelectedCells(0).RowIndex).Tag) Then
            btnRemoveAudio.Enabled = True
        Else
            btnRemoveAudio.Enabled = False
        End If
    End Sub

    Private Sub dgvVideo_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvVideo.SelectionChanged
        If dgvVideo.SelectedCells.Count > 0 AndAlso Not Convert.ToBoolean(dgvVideo.Rows(dgvVideo.SelectedCells(0).RowIndex).Tag) Then
            btnRemoveVideo.Enabled = True
        Else
            btnRemoveVideo.Enabled = False
        End If
    End Sub


    Private Sub dgvAudio_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvAudio.KeyDown
        e.Handled = (e.KeyCode = Keys.Enter)
    End Sub

    Private Sub dgvVideo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvVideo.KeyDown
        e.Handled = (e.KeyCode = Keys.Enter)
    End Sub

    Sub SetUp()
        btnAddAudio.Text = Master.eLang.GetString(28, "Add")
        btnAddVideo.Text = Master.eLang.GetString(28, "Add")
        btnRemoveAudio.Text = Master.eLang.GetString(30, "Remove")
        btnRemoveVideo.Text = Master.eLang.GetString(30, "Remove")
        btnSetDefaultsAudio.Text = Master.eLang.GetString(713, "Defaults")
        btnSetDefaultsVideo.Text = Master.eLang.GetString(713, "Defaults")
        Label1.Text = Master.eLang.GetString(634, "Audio")
        Label2.Text = Master.eLang.GetString(636, "Video")
        dgvAudio.Columns(0).HeaderText = Master.eLang.GetString(637, "Mediainfo Codec")
        dgvAudio.Columns(1).HeaderText = Master.eLang.GetString(638, "Mapped Codec")
        dgvVideo.Columns(0).HeaderText = Master.eLang.GetString(637, "Mediainfo Codec")
        dgvVideo.Columns(1).HeaderText = Master.eLang.GetString(638, "Mapped Codec")
    End Sub

    Public Sub SaveChanges()
        Dim deleteitem As New List(Of String)
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("AudioFormatConvert:"))
            deleteitem.Add(sett.Name)
        Next
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("VideoFormatConvert:"))
            deleteitem.Add(sett.Name)
        Next

        Using settings = New AdvancedSettings()
            For Each s As String In deleteitem
                settings.CleanSetting(s, "*EmberAPP")
            Next

            Dim formataudioconversions As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each r As DataGridViewRow In dgvAudio.Rows
                If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso (formataudioconversions.FindIndex(Function(f) f.Name = r.Cells(0).Value.ToString) = -1) Then
                    formataudioconversions.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = r.Cells(0).Value.ToString, .Value = r.Cells(1).Value.ToString})
                End If
            Next
            If formataudioconversions IsNot Nothing Then
                settings.SetComplexSetting("AudioFormatConverts", formataudioconversions, "*EmberAPP")
            End If

            Dim formatvideoconversions As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each r As DataGridViewRow In dgvVideo.Rows
                If Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso (formatvideoconversions.FindIndex(Function(f) f.Name = r.Cells(0).Value.ToString) = -1) Then
                    formatvideoconversions.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = r.Cells(0).Value.ToString, .Value = r.Cells(1).Value.ToString})
                End If
            Next
            If formatvideoconversions IsNot Nothing Then
                settings.SetComplexSetting("VideoFormatConverts", formatvideoconversions, "*EmberAPP")
            End If

        End Using
    End Sub

#End Region 'Methods

End Class
