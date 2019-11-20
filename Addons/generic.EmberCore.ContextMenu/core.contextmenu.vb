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

Public Class Core
    Implements Interfaces.IGenericModule

#Region "Fields"

    Private _PnlSettingsPanel As frmSettingsPanel

#End Region 'Fields

#Region "Properties"

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IGenericModule.IsBusy
        Get
            Return False
        End Get
    End Property

    Public Property IsEnabled() As Boolean Implements Interfaces.IGenericModule.IsEnabled
        Get
            Return True
        End Get
        Set(value As Boolean)
            Return
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IGenericModule.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IGenericModule.SettingsPanel


    Public ReadOnly Property Type() As List(Of Enums.ModuleEventType) Implements Interfaces.IGenericModule.Type
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic})
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.IGenericModule.GenericEvent
    Public Event NeedsRestart() Implements Interfaces.IGenericModule.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IGenericModule.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IGenericModule.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IGenericModule.Init
        Return
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IGenericModule.InjectSettingsPanel
        _PnlSettingsPanel = New frmSettingsPanel
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = -1,
            .Image = My.Resources.ContextMenu,
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = Master.eLang.GetString(1395, "Context Menu"),
            .Type = Enums.SettingsPanelType.Core
        }
    End Sub

    Public Function Run(ByVal ModuleEventType As Enums.ModuleEventType, ByRef Parameters As List(Of Object), ByRef SingleObjekt As Object, ByRef DBElement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericModule.Run
        Return New Interfaces.ModuleResult
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericModule.SaveSetup
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods 

End Class