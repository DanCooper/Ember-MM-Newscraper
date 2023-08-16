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
Imports System.Windows.Forms

Public Class frmSettingsHolder

#Region "Fields"

    Private _temp_AudioCodecsMappings As New List(Of SimpleMapping)
    Private _temp_VideoCodecsMappings As New List(Of SimpleMapping)

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged()

#End Region 'Events

#Region "Dialog Methods"

    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _temp_AudioCodecsMappings = APIXML.AudioCodecMappings.Mappings
        _temp_VideoCodecsMappings = APIXML.VideoCodecMappings.Mappings

        DataGridView_Load_Audio()
        DataGridView_Load_Video()
        Setup()
    End Sub

    Sub Setup()
        btnLoadDefaultsAudio.Text = Master.eLang.GetString(713, "Defaults")
        btnLoadDefaultsVideo.Text = Master.eLang.GetString(713, "Defaults")
        dgvAudio.Columns(0).HeaderText = Master.eLang.GetString(637, "Mediainfo Codec")
        dgvAudio.Columns(1).HeaderText = Master.eLang.GetString(638, "Mapped Codec")
        dgvVideo.Columns(0).HeaderText = Master.eLang.GetString(637, "Mediainfo Codec")
        dgvVideo.Columns(1).HeaderText = Master.eLang.GetString(638, "Mapped Codec")
        lblAudio.Text = Master.eLang.GetString(634, "Audio")
        lblVideo.Text = Master.eLang.GetString(636, "Video")
    End Sub

#End Region 'Dialog Methods

#Region "Methods"

    Private Sub DataGridView_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgvAudio.CurrentCellDirtyStateChanged, dgvVideo.CurrentCellDirtyStateChanged
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub DataGridView_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles dgvAudio.KeyDown, dgvVideo.KeyDown
        e.Handled = (e.KeyCode = Keys.Enter) OrElse (e.KeyCode = Keys.Escape)
    End Sub

    Private Sub DataGridView_Load_Audio()
        dgvAudio.Rows.Clear()
        For Each item In _temp_AudioCodecsMappings
            dgvAudio.Rows.Add(New Object() {
                              item.Input,
                              item.MappedTo
                              })
        Next
        dgvAudio.ClearSelection()
    End Sub

    Private Sub DataGridView_Load_Video()
        dgvVideo.Rows.Clear()
        For Each item In _temp_VideoCodecsMappings
            dgvVideo.Rows.Add(New Object() {
                              item.Input,
                              item.MappedTo
                              })
        Next
        dgvVideo.ClearSelection()
    End Sub

    Private Sub LoadDefaults_Audio(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadDefaultsAudio.Click
        _temp_AudioCodecsMappings = APIXML.AudioCodecMappings.GetDefaults()
        DataGridView_Load_Audio()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub LoadDefaults_Video(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadDefaultsVideo.Click
        _temp_VideoCodecsMappings = APIXML.VideoCodecMappings.GetDefaults()
        DataGridView_Load_Video()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Public Sub SaveChanges()
        APIXML.AudioCodecMappings.Mappings.Clear()
        For Each r As DataGridViewRow In dgvAudio.Rows
            If Not r.IsNewRow AndAlso
                r.Cells(0).Value IsNot Nothing AndAlso
                r.Cells(1).Value IsNot Nothing AndAlso
                Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso
                Not String.IsNullOrEmpty(r.Cells(1).Value.ToString) AndAlso
                APIXML.AudioCodecMappings.Mappings.FindIndex(Function(f) f.Input = r.Cells(0).Value.ToString) = -1 Then
                APIXML.AudioCodecMappings.Mappings.Add(New SimpleMapping With {
                                                       .Input = r.Cells(0).Value.ToString,
                                                       .MappedTo = r.Cells(1).Value.ToString
                                                       })
            End If
        Next
        APIXML.AudioCodecMappings.Save()

        APIXML.VideoCodecMappings.Mappings.Clear()
        For Each r As DataGridViewRow In dgvVideo.Rows
            If Not r.IsNewRow AndAlso
                r.Cells(0).Value IsNot Nothing AndAlso
                r.Cells(1).Value IsNot Nothing AndAlso
                Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) AndAlso
                Not String.IsNullOrEmpty(r.Cells(1).Value.ToString) AndAlso
                APIXML.VideoCodecMappings.Mappings.FindIndex(Function(f) f.Input = r.Cells(0).Value.ToString) = -1 Then
                APIXML.VideoCodecMappings.Mappings.Add(New SimpleMapping With {
                                                       .Input = r.Cells(0).Value.ToString,
                                                       .MappedTo = r.Cells(1).Value.ToString
                                                       })
            End If
        Next
        APIXML.VideoCodecMappings.Save()
    End Sub

#End Region 'Methods

End Class