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
    Implements Interfaces.ISettingsPanel

#Region "Fields"

    Private _temp_AudioCodecsMappings As New List(Of SimpleMapping)
    Private _temp_VideoCodecsMappings As New List(Of SimpleMapping)

#End Region 'Fields

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.ISettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.ISettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.ISettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.ISettingsPanel.NeedsReload_Movieset
    Public Event NeedsReload_TVEpisode() Implements Interfaces.ISettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.ISettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.ISettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.ISettingsPanel.SettingsChanged
    Public Event StateChanged(ByVal uniqueSettingsPanelId As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.ISettingsPanel.StateChanged

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ChildType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ChildType

    Public Property Image As Image Implements Interfaces.ISettingsPanel.Image

    Public Property ImageIndex As Integer Implements Interfaces.ISettingsPanel.ImageIndex

    Public Property IsEnabled As Boolean Implements Interfaces.ISettingsPanel.IsEnabled

    Public ReadOnly Property MainPanel As Panel Implements Interfaces.ISettingsPanel.MainPanel

    Public Property Order As Integer Implements Interfaces.ISettingsPanel.Order

    Public ReadOnly Property ParentType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ParentType

    Public ReadOnly Property Title As String Implements Interfaces.ISettingsPanel.Title

    Public Property UniqueId As String Implements Interfaces.ISettingsPanel.UniqueId

#End Region 'Properties

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        'Set Master Panel Data
        ChildType = Enums.SettingsPanelType.None
        IsEnabled = True
        Image = Nothing
        ImageIndex = 13
        Order = 500
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(785, "Audio & Video Codec Mapping")
        ParentType = Enums.SettingsPanelType.Options
        UniqueId = "Option_AVCodecMapping"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        With Master.eLang
            colAudioAdditionalFeatures.HeaderText = .GetString(336, "Additional Features")
            colAudioDetected.HeaderText = .GetString(126, "Detected")
            colAudioMappedCodec.HeaderText = .GetString(638, "Mapped Codec")
            colVideoDetected.HeaderText = .GetString(126, "Detected")
            colVideoMappedCodec.HeaderText = .GetString(638, "Mapped Codec")
            btnLoadDefaultsAudio.Text = .GetString(713, "Defaults")
            btnLoadDefaultsVideo.Text = .GetString(713, "Defaults")
            gbAudio.Text = .GetString(464, "Audio Codec Mapping")
            gbVideo.Text = .GetString(557, "Video Codec Mapping")
        End With
    End Sub

#End Region 'Dialog Methods 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.ISettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Sub Addon_Order_Changed(ByVal totalCount As Integer) Implements Interfaces.ISettingsPanel.OrderChanged
        Return
    End Sub

    Public Sub SaveSettings() Implements Interfaces.ISettingsPanel.SaveSettings
        SaveChanges()
        'With Master.eSettings
        '    Save_Audio()
        '    Save_Video()
        'End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Private Sub Handle_SettingsChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _
        dgvAudio.CellValueChanged,
        dgvAudio.RowsAdded,
        dgvAudio.RowsRemoved,
        dgvVideo.CellValueChanged,
        dgvVideo.RowsAdded,
        dgvVideo.RowsRemoved

        RaiseEvent SettingsChanged()
    End Sub

    Public Sub Settings_Load()
        _temp_AudioCodecsMappings = APIXML.AudioCodecMappings.Mappings
        _temp_VideoCodecsMappings = APIXML.VideoCodecMappings.Mappings
        DataGridView_Load_Audio()
        DataGridView_Load_Video()

        'With Master.eSettings.Options.AVCodecMapping
        '    DataGridView_Fill_Audio(.Audio)
        '    DataGridView_Fill_Video(.Video)
        'End With
    End Sub

    'Private Sub DataGridView_Fill_Audio(ByVal List As List(Of AVCodecMapping.CodecMapping))
    '    dgvAudio.Rows.Clear()
    '    For Each sett In List
    '        Dim i As Integer = dgvAudio.Rows.Add(New Object() {
    '                                             sett.Codec,
    '                                             sett.Mapping,
    '                                             sett.AdditionalFeatures
    '                                             })
    '    Next
    '    dgvAudio.ClearSelection()
    'End Sub

    'Private Sub DataGridView_Fill_Video(ByVal List As List(Of AVCodecMapping.CodecMapping))
    '    dgvVideo.Rows.Clear()
    '    For Each sett In List
    '        Dim i As Integer = dgvVideo.Rows.Add(New Object() {
    '                                             sett.Codec,
    '                                             sett.Mapping
    '                                             })
    '    Next
    '    dgvVideo.ClearSelection()
    'End Sub

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
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub LoadDefaults_Video(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadDefaultsVideo.Click
        _temp_VideoCodecsMappings = APIXML.VideoCodecMappings.GetDefaults()
        DataGridView_Load_Video()
        RaiseEvent SettingsChanged()
    End Sub

    'Private Sub LoadDefaults_Audio(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadDefaultsAudio.Click
    '    DataGridView_Fill_Audio(Master.eSettings.Options.AVCodecMapping.GetDefaults(Enums.DefaultType.AudioCodecMapping))
    '    'DataGridView_Fill_Audio(Master.eSettings.Options.AVCodecMapping.GetDefaults(Enums.DefaultType.AudioCodecMapping))
    '    RaiseEvent SettingsChanged()
    'End Sub

    'Private Sub LoadDefaults_Video(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadDefaultsVideo.Click
    '    DataGridView_Fill_Video(Master.eSettings.Options.AVCodecMapping.GetDefaults(Enums.DefaultType.VideoCodecMapping))
    '    'DataGridView_Fill_Video(Master.eSettings.Options.AVCodecMapping.GetDefaults(Enums.DefaultType.VideoCodecMapping))
    '    RaiseEvent SettingsChanged()
    'End Sub

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

    'Private Sub Save_Audio()
    '    With Master.eSettings.Options.AVCodecMapping.Audio
    '        .Clear()
    '        For Each r As DataGridViewRow In dgvAudio.Rows
    '            If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) Then
    '                .Add(New AVCodecMapping.CodecMapping With {
    '                     .Codec = r.Cells(0).Value.ToString,
    '                     .Mapping = If(r.Cells(1).Value IsNot Nothing, r.Cells(1).Value.ToString, String.Empty),
    '                     .AdditionalFeatures = If(r.Cells(2).Value IsNot Nothing, r.Cells(2).Value.ToString, String.Empty)
    '                     })
    '            End If
    '        Next
    '    End With
    'End Sub

    'Private Sub Save_Video()
    '    With Master.eSettings.Options.AVCodecMapping.Video
    '        .Clear()
    '        For Each r As DataGridViewRow In dgvVideo.Rows
    '            If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString) Then
    '                .Add(New AVCodecMapping.CodecMapping With {
    '                     .Codec = r.Cells(0).Value.ToString,
    '                     .Mapping = If(r.Cells(1).Value IsNot Nothing, r.Cells(1).Value.ToString, String.Empty)
    '                     })
    '            End If
    '        Next
    '    End With
    'End Sub

#End Region 'Methods

End Class