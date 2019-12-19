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

Public Class frmOption_AVCodecMapping
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
            .ImageIndex = 13,
            .Order = 500,
            .Panel = pnlSettings,
            .SettingsPanelID = "Option_AVCodecMapping",
            .Title = Master.eLang.GetString(785, "Audio & Video Codec Mapping"),
            .Type = Enums.SettingsPanelType.Options
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings
            Save_Audio()
            Save_Video()
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.Options.AVCodecMapping
            DataGridView_Fill_Audio(.Audio)
            DataGridView_Fill_Video(.Video)
        End With
    End Sub

    Private Sub Setup()
        With Master.eLang
            colAudioAdditionalFeatures.HeaderText = .GetString(336, "Additional Features")
            colAudioDetected.HeaderText = .GetString(126, "Detected")
            colAudioMappedCodec.HeaderText = .GetString(638, "Mapped Codec")
            colVideoDetected.HeaderText = .GetString(126, "Detected")
            colVideoMappedCodec.HeaderText = .GetString(638, "Mapped Codec")
            btnAudioDefaults.Text = .GetString(713, "Defaults")
            btnVideoDefaults.Text = .GetString(713, "Defaults")
            gbAudio.Text = .GetString(464, "Audio Codec Mapping")
            gbVideo.Text = .GetString(557, "Video Codec Mapping")
        End With
    End Sub

    Private Sub Enable_ApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        dgvAudio.CellValueChanged,
        dgvAudio.RowsAdded,
        dgvAudio.RowsRemoved,
        dgvVideo.CellValueChanged,
        dgvVideo.RowsAdded,
        dgvVideo.RowsRemoved

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_Fill_Audio(ByVal List As List(Of AVCodecMapping.CodecMapping))
        dgvAudio.Rows.Clear()
        For Each sett In List
            Dim i As Integer = dgvAudio.Rows.Add(New Object() {
                                                 sett.Codec,
                                                 sett.Mapping,
                                                 sett.AdditionalFeatures
                                                 })
        Next
        dgvAudio.ClearSelection()
    End Sub

    Private Sub DataGridView_Fill_Video(ByVal List As List(Of AVCodecMapping.CodecMapping))
        dgvVideo.Rows.Clear()
        For Each sett In List
            Dim i As Integer = dgvVideo.Rows.Add(New Object() {
                                                 sett.Codec,
                                                 sett.Mapping
                                                 })
        Next
        dgvVideo.ClearSelection()
    End Sub

    Private Sub LoadDefaults_Audio(ByVal sender As Object, ByVal e As EventArgs) Handles btnAudioDefaults.Click
        DataGridView_Fill_Audio(Master.eSettings.GetDefaultsForList_AudioCodecMappings())
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub LoadDefaults_Video(ByVal sender As Object, ByVal e As EventArgs) Handles btnVideoDefaults.Click
        DataGridView_Fill_Video(Master.eSettings.GetDefaultsForList_VideoCodecMappings())
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Save_Audio()
        With Master.eSettings.Options.AVCodecMapping.Audio
            .Clear()
            For Each r As DataGridViewRow In dgvAudio.Rows
                If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) Then
                    .Add(New AVCodecMapping.CodecMapping With {
                         .Codec = r.Cells(0).Value.ToString,
                         .Mapping = If(r.Cells(1).Value IsNot Nothing, r.Cells(1).Value.ToString, String.Empty),
                         .AdditionalFeatures = If(r.Cells(2).Value IsNot Nothing, r.Cells(2).Value.ToString, String.Empty)
                         })
                End If
            Next
        End With
    End Sub

    Private Sub Save_Video()
        With Master.eSettings.Options.AVCodecMapping.Video
            .Clear()
            For Each r As DataGridViewRow In dgvVideo.Rows
                If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) Then
                    .Add(New AVCodecMapping.CodecMapping With {
                         .Codec = r.Cells(0).Value.ToString,
                         .Mapping = If(r.Cells(1).Value IsNot Nothing, r.Cells(1).Value.ToString, String.Empty)
                         })
                End If
            Next
        End With
    End Sub

#End Region 'Methods

End Class