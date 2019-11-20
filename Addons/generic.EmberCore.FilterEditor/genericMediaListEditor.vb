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

Public Class genericMediaListEditor
    Implements Interfaces.IGenericModule

#Region "Fields"

    Private _setup As frmSettingsHolder

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.IGenericModule.GenericEvent
    Public Event NeedsRestart() Implements Interfaces.IGenericModule.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IGenericModule.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IGenericModule.StateChanged

#End Region 'Events

#Region "Properties"

    Public Property IsEnabled() As Boolean Implements Interfaces.IGenericModule.IsEnabled
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)
        End Set
    End Property

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IGenericModule.IsBusy
        Get
            Return False
        End Get
    End Property

    Property Order As Integer Implements Interfaces.IGenericModule.Order

    Property SettingsPanelID As String Implements Interfaces.IGenericModule.SettingsPanelID

    Public ReadOnly Property Type() As List(Of Enums.ModuleEventType) Implements Interfaces.IGenericModule.Type
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic})
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub Init() Implements Interfaces.IGenericModule.Init
        Return
    End Sub

    Public Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IGenericModule.InjectSettingsPanel
        _setup = New frmSettingsHolder
        AddHandler _setup.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart

        Return New Containers.SettingsPanel With {
            .Image = My.Resources.FilterEditor,
            .ImageIndex = -1,
            .Panel = _setup.pnlMediaListEditor,
            .Title = Master.eLang.GetString(1385, "Media List Editor"),
            .Type = Enums.SettingsPanelType.Core
        }
    End Function

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

    Public Function Run(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericModule.Run
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericModule.SaveSetup
        If Not _setup Is Nothing Then _setup.SaveChanges()
        If DoDispose Then
            RemoveHandler _setup.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

#End Region 'Methods

End Class