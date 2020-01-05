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

Imports System.Drawing
Imports System.Windows.Forms
Imports EmberAPI
Imports NLog

Public Class Core
    Implements Interfaces.IGenericAddon

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private WithEvents _CmnuTrayToolsRenamer As New ToolStripMenuItem
    Private WithEvents _MnuMainToolsRenamer As New ToolStripMenuItem
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel


#End Region 'Fields

#Region "Properties"

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IGenericAddon.IsBusy
        Get
            Return False
        End Get
    End Property

    Public Property IsEnabled() As Boolean Implements Interfaces.IGenericAddon.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            If _Enabled = value Then Return
            _Enabled = value
            If _Enabled Then
                ToolsStripMenu_Enable()
            Else
                ToolsStripMenu_Disable()
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IGenericAddon.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IGenericAddon.SettingsPanel

    Public ReadOnly Property Type() As List(Of Enums.AddonEventType) Implements Interfaces.IGenericAddon.Type
        Get
            Return New List(Of Enums.AddonEventType)(New Enums.AddonEventType() {Enums.AddonEventType.Generic})
        End Get
    End Property

#End Region 'Properties

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_AddToolsStripItem(tsi As ToolStripMenuItem, value As ToolStripMenuItem)

#End Region 'Delegates

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object)) Implements Interfaces.IGenericAddon.GenericEvent
    Public Event NeedsRestart() Implements Interfaces.IGenericAddon.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IGenericAddon.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IGenericAddon.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_StateChanged(ByVal State As Boolean)
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, 0)
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IGenericAddon.Init
        Return
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IGenericAddon.InjectSettingsPanel
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.chkEnabled.Checked = _Enabled
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .Image = My.Resources.icon,
            .ImageIndex = If(_Enabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = Master.eLang.GetString(782, "Genre Manager"),
            .Type = Enums.SettingsPanelType.Options
        }
    End Sub

    Public Function Run(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericAddon.Run
        Return New Interfaces.ModuleResult
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericAddon.SaveSetup
        IsEnabled = _PnlSettingsPanel.chkEnabled.Checked
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub ToolsStripMenu_Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(_MnuMainToolsRenamer)

        'cmnuTrayTools
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(_CmnuTrayToolsRenamer)
    End Sub

    Sub ToolsStripMenu_Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        _MnuMainToolsRenamer.Image = New Bitmap(My.Resources.icon)
        _MnuMainToolsRenamer.Text = Master.eLang.GetString(782, "Genre Manager")
        _MnuMainToolsRenamer.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, _MnuMainToolsRenamer)

        'cmnuTrayTools
        _CmnuTrayToolsRenamer.Image = New Bitmap(My.Resources.icon)
        _CmnuTrayToolsRenamer.Text = Master.eLang.GetString(782, "Genre Manager")
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, _CmnuTrayToolsRenamer)
    End Sub

    Public Sub AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Private Sub mnuMainToolsRenamer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MnuMainToolsRenamer.Click, _CmnuTrayToolsRenamer.Click
        RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dGenreManager As New dlgGenreManager
            dGenreManager.ShowDialog()
        End Using
        RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

#End Region 'Methods 

End Class
