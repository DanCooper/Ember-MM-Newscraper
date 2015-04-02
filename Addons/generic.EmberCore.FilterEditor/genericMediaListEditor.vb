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
    Implements Interfaces.GenericModule

#Region "Fields"

    Private _AssemblyName As String = String.Empty
    Private _name As String = "Media List Editor"
    Private _setup As frmMediaListEditor

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object)) Implements EmberAPI.Interfaces.GenericModule.GenericEvent

    Public Event ModuleSettingsChanged() Implements EmberAPI.Interfaces.GenericModule.ModuleSettingsChanged

    Public Event ModuleSetupChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements EmberAPI.Interfaces.GenericModule.ModuleSetupChanged

#End Region 'Events

#Region "Properties"

    Public Property Enabled() As Boolean Implements EmberAPI.Interfaces.GenericModule.Enabled
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)
        End Set
    End Property

    Public ReadOnly Property ModuleName() As String Implements EmberAPI.Interfaces.GenericModule.ModuleName
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property ModuleType() As System.Collections.Generic.List(Of EmberAPI.Enums.ModuleEventType) Implements EmberAPI.Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic})
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
    End Sub

    Public Function InjectSetup() As EmberAPI.Containers.SettingsPanel Implements EmberAPI.Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        Me._setup = New frmMediaListEditor
        SPanel.Name = Master.eLang.GetString(9999, "Media List Editor")
        SPanel.Text = Master.eLang.GetString(9999, "Media List Editor")
        SPanel.Prefix = "MediaListEditor_"
        SPanel.Type = Master.eLang.GetString(429, "Miscellaneous")
        SPanel.ImageIndex = -1
        SPanel.Image = My.Resources.FilterEditor
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlMediaListEditor
        AddHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Public Function RunGeneric(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object), ByRef _refparam As Object, ByRef _dbmovie As Structures.DBMovie, ByRef _dbtv As Structures.DBTV) As EmberAPI.Interfaces.ModuleResult Implements EmberAPI.Interfaces.GenericModule.RunGeneric

    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements EmberAPI.Interfaces.GenericModule.SaveSetup
        If DoDispose Then
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
