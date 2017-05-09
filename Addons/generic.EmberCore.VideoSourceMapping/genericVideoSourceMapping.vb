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

Public Class genericVideoSourceMapping
    Implements Interfaces.GenericModule

#Region "Fields"

    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object)) Implements Interfaces.GenericModule.GenericEvent

    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged

    Public Event ModuleSetupChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged

    Public Event SetupNeedsRestart() Implements Interfaces.GenericModule.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    Public Property Enabled() As Boolean Implements Interfaces.GenericModule.Enabled
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)
        End Set
    End Property

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.GenericModule.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ModuleName() As String Implements Interfaces.GenericModule.ModuleName
        Get
            Return "Media Sources Editor"
        End Get
    End Property

    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic})
        End Get
    End Property

    Public ReadOnly Property ModuleVersion() As String Implements Interfaces.GenericModule.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        'Master.eLang.LoadLanguage(Master.eSettings.Language, sExecutable)
    End Sub

    Public Function InjectSetup() As EmberAPI.Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        SPanel.Name = Master.eLang.GetString(784, "Video Source Mapping")
        SPanel.Text = Master.eLang.GetString(784, "Video Source Mapping")
        SPanel.Prefix = "VideoSourceMapping_"
        SPanel.Type = Master.eLang.GetString(429, "Miscellaneous")
        SPanel.ImageIndex = -1
        SPanel.Image = My.Resources.MediaSourcesEditor
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlGenres
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Public Function RunGeneric(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric

    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        If Not _setup Is Nothing Then _setup.SaveChanges()
        If DoDispose Then
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

#End Region 'Methods

End Class
