﻿Imports EmberAPI

Public Class genericTrakt
    Implements Interfaces.GenericModule

#Region "Fields"

    Private _setup As frmTrakt
    Private _AssemblyName As String = String.Empty

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
            Return "Trakt Settings"
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
        'Master.eLang.LoadLanguage(Master.eSettings.Language, sExecutable)
    End Sub

    Public Function InjectSetup() As EmberAPI.Containers.SettingsPanel Implements EmberAPI.Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        Me._setup = New frmTrakt
        SPanel.Name = Master.eLang.GetString(871, "Trakt Settings")
        SPanel.Text = Master.eLang.GetString(871, "Trakt Settings")
        SPanel.Prefix = "TraktSettings_"
        SPanel.Type = Master.eLang.GetString(429, "Miscellaneous")
        SPanel.ImageIndex = -1
        SPanel.Image = My.Resources.trakt
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlTrakt
        AddHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub


    Public Async Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByVal _params As List(Of Object), ByVal _refparam As Object, ByVal _dbmovie As Structures.DBMovie) As Threading.Tasks.Task(Of Interfaces.ModuleResult) Implements Interfaces.GenericModule.RunGeneric
        ' return parameters will add in ReturnObject
        ' _params
        '_refparam 
        '_dbmovie 
        Dim ret As New Interfaces.ModuleResult
        ret.ReturnObj.Add(_params)
        ret.ReturnObj.Add(_refparam)
        ret.ReturnObj.Add(_dbmovie)
        Return ret
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements EmberAPI.Interfaces.GenericModule.SaveSetup
        _setup.SaveChanges()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub
#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
