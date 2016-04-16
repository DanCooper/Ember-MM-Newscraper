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

Public Class genericMediaListEditor
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_SetTabPageItem(value As TabPage)
    Public Delegate Sub Delegate_RemoveTabPageItem(value As TabPage)
    Public Delegate Sub Delegate_TabPageAdd(cTabs As List(Of TabPage), tabc As System.Windows.Forms.TabControl)

#End Region 'Delegates

#Region "Fields"

    Private _AssemblyName As String = String.Empty
    Private _name As String = "Media List Editor"
    Private _setup As frmSettingsHolder

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
            Enable()
        End Set
    End Property

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.GenericModule.IsBusy
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property ModuleName() As String Implements Interfaces.GenericModule.ModuleName
        Get
            Return _name
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
    End Sub

    Public Function InjectSetup() As EmberAPI.Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        SPanel.Name = _name
        SPanel.Text = Master.eLang.GetString(1385, "Media List Editor")
        SPanel.Prefix = "MediaListEditor_"
        SPanel.Type = Master.eLang.GetString(429, "Miscellaneous")
        SPanel.ImageIndex = -1
        SPanel.Image = My.Resources.FilterEditor
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlMediaListEditor
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return SPanel
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Public Function RunGeneric(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        If Not _setup Is Nothing Then _setup.SaveChanges()
        If DoDispose Then
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

    Sub Enable()
        Dim CustomTabs As List(Of AdvancedSettingsComplexSettingsTableItem) = clsAdvancedSettings.GetComplexSetting("CustomTabs", "*EmberAPP")
        If CustomTabs IsNot Nothing Then
            Dim tabc As New TabControl
            Dim NewCustomTabs As New List(Of TabPage)
            tabc = DirectCast(ModulesManager.Instance.RuntimeObjects.MainTabControl, TabControl)
            For Each cTab In CustomTabs
                If Master.DB.ViewExists(cTab.Value) Then
                    Dim cTabType As Enums.ContentType = Enums.ContentType.None
                    If cTab.Value.StartsWith("movie-") Then
                        cTabType = Enums.ContentType.Movie
                    ElseIf cTab.Value.StartsWith("sets-") Then
                        cTabType = Enums.ContentType.MovieSet
                    ElseIf cTab.Value.StartsWith("tvshow-") Then
                        cTabType = Enums.ContentType.TV
                    End If
                    If Not cTabType = Enums.ContentType.None AndAlso Not String.IsNullOrEmpty(cTab.Name) Then
                        Dim NewTabPage As New TabPage
                        NewTabPage.Text = cTab.Name
                        NewTabPage.Tag = New Structures.MainTabType With {.ContentName = cTab.Name, .ContentType = cTabType, .DefaultList = cTab.Value}
                        NewCustomTabs.Add(NewTabPage)
                    End If
                End If
            Next
            TabPageAdd(NewCustomTabs, tabc)
        End If
    End Sub

    Public Sub TabPageAdd(cTabs As List(Of TabPage), tabc As System.Windows.Forms.TabControl)
        If (tabc.InvokeRequired) Then
            tabc.Invoke(New Delegate_TabPageAdd(AddressOf TabPageAdd), New Object() {cTabs, tabc})
            Exit Sub
        End If
        tabc.TabPages.AddRange(cTabs.ToArray)
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
