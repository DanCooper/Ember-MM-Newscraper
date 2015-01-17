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

Imports System
Imports System.IO
Imports System.Xml.Serialization

Imports EmberAPI

Public Class VLCPlayer
    Implements Interfaces.GenericModule

#Region "Fields"

    Private WithEvents MyMenu As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents MyTrayMenu As New System.Windows.Forms.ToolStripMenuItem
    Private _AssemblyName As String = String.Empty
    Private _enabled As Boolean = False
    Private _MySettings As New MySettings
    Private _name As String = "VLC Player"
    Private _setup As frmSettingsHolder
    Private frmVLCAudio As frmVLCAudio
    Private frmVLCVideo As frmVLCVideo

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object)) Implements EmberAPI.Interfaces.GenericModule.GenericEvent

    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged

    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged

#End Region 'Events

#Region "Properties"

    Public Property Enabled() As Boolean Implements EmberAPI.Interfaces.GenericModule.Enabled
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            If _enabled = value Then Return
            _enabled = value
        End Set
    End Property

    Public ReadOnly Property ModuleName() As String Implements EmberAPI.Interfaces.GenericModule.ModuleName
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property ModuleType() As System.Collections.Generic.List(Of EmberAPI.Enums.ModuleEventType) Implements EmberAPI.Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.MediaPlayer_Audio, Enums.ModuleEventType.MediaPlayer_Video})
        End Get
    End Property

    Public ReadOnly Property ModuleVersion() As String Implements EmberAPI.Interfaces.GenericModule.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements EmberAPI.Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Public Function InjectSetup() As EmberAPI.Containers.SettingsPanel Implements EmberAPI.Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = Me._enabled

        _setup.chkUseAsAudioPlayer.Checked = _MySettings.UseAsAudioPlayer
        _setup.chkUseAsVideoPlayer.Checked = _MySettings.UseAsVideoPlayer

        SPanel.Name = Me._name
        SPanel.Text = "VLC Player"
        SPanel.Prefix = "VLCPlayer_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(Me._enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings()
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_SetupChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Public Function RunGeneric(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object), ByRef _refparam As Object, ByRef _dbmovie As Structures.DBMovie, ByRef _dbtv As Structures.DBTV) As EmberAPI.Interfaces.ModuleResult Implements EmberAPI.Interfaces.GenericModule.RunGeneric
        If clsVLCTest.DoTest Then
            Select Case mType
                Case Enums.ModuleEventType.MediaPlayer_Audio
                    If _MySettings.UseAsAudioPlayer Then
                        frmVLCAudio = New frmVLCAudio
                        _params(0) = frmVLCAudio.pnlExtrator
                        AddHandler frmVLCAudio.GenericEvent, AddressOf Handle_GenericEvent
                    End If
                Case Enums.ModuleEventType.MediaPlayer_Video
                    If _MySettings.UseAsVideoPlayer Then
                        frmVLCVideo = New frmVLCVideo
                        _params(0) = frmVLCVideo.pnlVLC
                        AddHandler frmVLCVideo.GenericEvent, AddressOf Handle_GenericEvent
                    End If
            End Select
        End If
    End Function

    Sub Handle_GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object))
        RaiseEvent GenericEvent(mType, _params)
    End Sub

    Sub LoadSettings()
        _MySettings.UseAsAudioPlayer = clsAdvancedSettings.GetBooleanSetting("UseAsAudioPlayer", False)
        _MySettings.UseAsVideoPlayer = clsAdvancedSettings.GetBooleanSetting("UseAsVideoPlayer", False)
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("UseAsAudioPlayer", _MySettings.UseAsAudioPlayer)
            settings.SetBooleanSetting("UseAsVideoPlayer", _MySettings.UseAsVideoPlayer)
        End Using
    End Sub

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements EmberAPI.Interfaces.GenericModule.SaveSetup
        Me.Enabled = _setup.chkEnabled.Checked
        _MySettings.UseAsAudioPlayer = _setup.chkUseAsAudioPlayer.Checked
        _MySettings.UseAsVideoPlayer = _setup.chkUseAsVideoPlayer.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_SetupChanged
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupChanged(ByVal state As Boolean, ByVal difforder As Integer)
        RaiseEvent ModuleEnabledChanged(Me._name, state, difforder)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure MySettings

#Region "Fields"

        Dim UseAsAudioPlayer As Boolean
        Dim UseAsVideoPlayer As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class