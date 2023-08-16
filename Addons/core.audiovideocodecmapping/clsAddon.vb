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

Public Class Addon
    Implements Interfaces.IAddon_Generic

#Region "Fields"

    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal eventType As Enums.AddonEventType, ByRef parameters As List(Of Object)) Implements Interfaces.IAddon_Generic.GenericEvent
    Public Event AddonSettingsChanged() Implements Interfaces.IAddon_Generic.AddonSettingsChanged
    Public Event AddonStateChanged(ByVal name As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.IAddon_Generic.AddonStateChanged
    Public Event AddonNeedsRestart() Implements Interfaces.IAddon_Generic.AddonNeedsRestart

#End Region 'Events

#Region "Properties"

    Public Property Enabled() As Boolean Implements Interfaces.IAddon_Generic.Enabled
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)
            Return
        End Set
    End Property

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IAddon_Generic.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements Interfaces.IAddon_Generic.Name
        Get
            Return "Audio & Video Codec Mapping"
        End Get
    End Property

    Public ReadOnly Property EventType() As List(Of Enums.AddonEventType) Implements Interfaces.IAddon_Generic.EventType
        Get
            Return New List(Of Enums.AddonEventType)(New Enums.AddonEventType() {Enums.AddonEventType.Generic})
        End Get
    End Property

    Public ReadOnly Property Version() As String Implements Interfaces.IAddon_Generic.Version
        Get
            Return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub Init(ByVal assemblyName As String, ByVal executable As String) Implements Interfaces.IAddon_Generic.Init
        _AssemblyName = assemblyName
    End Sub

    Public Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IAddon_Generic.InjectSettingsPanel
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        SPanel.UniqueName = _AssemblyName
        SPanel.Title = Master.eLang.GetString(785, "Audio & Video Codec Mapping")
        SPanel.Type = Master.eLang.GetString(429, "Miscellaneous")
        SPanel.ImageIndex = -1
        SPanel.Image = My.Resources.MetadataEditor
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlMain
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent AddonSettingsChanged()
    End Sub

    Public Function RunGeneric(ByVal eventType As Enums.AddonEventType, ByRef parameters As List(Of Object), ByRef singleObject As Object, ByRef dbElement As Database.DBElement) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Generic.RunGeneric
        Return New Interfaces.AddonResult_Generic
    End Function

    Public Sub SaveSettings(ByVal doDispose As Boolean) Implements Interfaces.IAddon_Generic.SaveSettings
        If Not _setup Is Nothing Then _setup.SaveChanges()
        If doDispose Then
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

#End Region 'Methods

End Class