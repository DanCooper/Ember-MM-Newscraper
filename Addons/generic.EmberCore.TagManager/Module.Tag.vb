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
Imports System.Drawing
Imports NLog

Public Class Tag_Generic
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(control As ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(control As ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty
    Private WithEvents mnuMainToolsTag As New ToolStripMenuItem
    Private WithEvents cmnuTrayToolsTag As New ToolStripMenuItem
    Private _enabled As Boolean = False
    Private _Name As String = "Tag Manager"

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.GenericModule.GenericEvent
    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged
    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged
    Public Event SetupNeedsRestart() Implements Interfaces.GenericModule.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic, Enums.ModuleEventType.CommandLine})
        End Get
    End Property

    Property Enabled() As Boolean Implements Interfaces.GenericModule.Enabled
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            If _enabled = value Then Return
            _enabled = value
            If _enabled Then
                Enable()
            Else
                Disable()
            End If
        End Set
    End Property

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.GenericModule.IsBusy
        Get
            Return False
        End Get
    End Property

    ReadOnly Property ModuleName() As String Implements Interfaces.GenericModule.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.GenericModule.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    ''' <summary>
    ''' Commandline call: Update/Sync tags
    ''' </summary>
    ''' <remarks>
    ''' TODO
    ''' </remarks>
    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, mnuMainToolsTag)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, cmnuTrayToolsTag)
    End Sub

    Public Sub RemoveToolsStripItem(control As ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Remove(value)
        End If
    End Sub

    Sub Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        mnuMainToolsTag.Image = New Bitmap(My.Resources._set)
        mnuMainToolsTag.Text = Master.eLang.GetString(868, "Tag Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsTag)

        'cmnuTrayTools
        cmnuTrayToolsTag.Image = New Bitmap(My.Resources._set)
        cmnuTrayToolsTag.Text = Master.eLang.GetString(868, "Tag Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsTag)
    End Sub

    Public Sub AddToolsStripItem(control As ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent ModuleEnabledChanged(_Name, State, 0)
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        _setup.chkEnabled.Checked = _enabled
        SPanel.Name = _Name
        SPanel.Text = Master.eLang.GetString(868, "Tag Manager")
        SPanel.Prefix = "TagManager_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Private Sub MyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsTag.Click, cmnuTrayToolsTag.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))

        Using dTagManager As New dlgTagManager
            dTagManager.ShowDialog()
        End Using

        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
    End Sub
    Sub LoadSettings()
    End Sub

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = _setup.chkEnabled.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
    End Sub

#End Region 'Methods

#Region "Nested Types"

#End Region 'Nested Types

End Class
