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

Public Class Addon
    Implements Interfaces.IGenericModule

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private WithEvents _CmnuTrayToolsTag As New ToolStripMenuItem
    Private WithEvents _MnuMainToolsTag As New ToolStripMenuItem
    Private _enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel

#End Region 'Fields

#Region "Properties"

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IGenericModule.IsBusy
        Get
            Return False
        End Get
    End Property

    Property IsEnabled() As Boolean Implements Interfaces.IGenericModule.IsEnabled
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            If _enabled = value Then Return
            _enabled = value
            If _enabled Then
                ToolsStripMenu_Enable()
            Else
                ToolsStripMenu_Disable()
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IGenericModule.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IGenericModule.SettingsPanel

    Public ReadOnly Property Type() As List(Of Enums.ModuleEventType) Implements Interfaces.IGenericModule.Type
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {
                                                      Enums.ModuleEventType.Generic,
                                                      Enums.ModuleEventType.CommandLine
                                                      })
        End Get
    End Property

#End Region 'Properties

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(Control As ToolStripMenuItem, Value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(Control As ToolStripMenuItem, Value As ToolStripItem)

#End Region 'Delegates

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

    Private Sub Handle_StateChanged(ByVal State As Boolean)
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, 0)
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IGenericModule.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IGenericModule.InjectSettingsPanel
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.chkEnabled.Checked = _enabled
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(_enabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = Master.eLang.GetString(868, "Tag Manager"),
            .Type = Enums.SettingsPanelType.Core
        }
    End Sub
    ''' <summary>
    ''' Commandline call: Update/Sync tags
    ''' </summary>
    ''' <remarks>
    ''' TODO
    ''' </remarks>
    Public Function Run(ByVal ModuleEventType As Enums.ModuleEventType,
                               ByRef Parameters As List(Of Object),
                               ByRef SingleObjekt As Object,
                               ByRef DBElement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericModule.Run
        Return New Interfaces.ModuleResult
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericModule.SaveSetup
        IsEnabled = _PnlSettingsPanel.chkEnabled.Checked
        Settings_Save()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub ToolsStripMenu_Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, _MnuMainToolsTag)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, _CmnuTrayToolsTag)
    End Sub

    Sub ToolsStripMenu_Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        _MnuMainToolsTag.Image = New Bitmap(My.Resources._set)
        _MnuMainToolsTag.Text = Master.eLang.GetString(868, "Tag Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, _MnuMainToolsTag)

        'cmnuTrayTools
        _CmnuTrayToolsTag.Image = New Bitmap(My.Resources._set)
        _CmnuTrayToolsTag.Text = Master.eLang.GetString(868, "Tag Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, _CmnuTrayToolsTag)
    End Sub

    Public Sub RemoveToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Remove(value)
        End If
    End Sub

    Public Sub AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Private Sub MyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _MnuMainToolsTag.Click, _CmnuTrayToolsTag.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))

        Using dTagManager As New dlgTagManager
            dTagManager.ShowDialog()
        End Using

        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
    End Sub

    Sub Settings_Load()
        Return
    End Sub

    Sub Settings_Save()
        Return
    End Sub

#End Region 'Methods

End Class