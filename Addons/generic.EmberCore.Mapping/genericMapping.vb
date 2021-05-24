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
Imports NLog
Imports System.Drawing
Imports System.Windows.Forms

Public Class genericMapping
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_AddToolsStripItem(tsi As ToolStripMenuItem, value As ToolStripMenuItem)

#End Region 'Delegates

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _Name As String = "MappingManager"
    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty
    Private _enabled As Boolean = False
    Private WithEvents cmnuTrayToolsCertificationMapping As New ToolStripMenuItem
    Private WithEvents cmnuTrayToolsCountryMapping As New ToolStripMenuItem
    Private WithEvents cmnuTrayToolsEditionMapping As New ToolStripMenuItem
    Private WithEvents cmnuTrayToolsGenreMapping As New ToolStripMenuItem
    Private WithEvents cmnuTrayToolsStatusMapping As New ToolStripMenuItem
    Private WithEvents cmnuTrayToolsStudioMapping As New ToolStripMenuItem
    Private WithEvents mnuMainToolsCertificationMapping As New ToolStripMenuItem
    Private WithEvents mnuMainToolsCountryMapping As New ToolStripMenuItem
    Private WithEvents mnuMainToolsEditionMapping As New ToolStripMenuItem
    Private WithEvents mnuMainToolsGenreMapping As New ToolStripMenuItem
    Private WithEvents mnuMainToolsStatusMapping As New ToolStripMenuItem
    Private WithEvents mnuMainToolsStudioMapping As New ToolStripMenuItem


#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.GenericModule.GenericEvent
    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged
    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged
    Public Event SetupNeedsRestart() Implements Interfaces.GenericModule.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    Public Property Enabled() As Boolean Implements Interfaces.GenericModule.Enabled
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

    Public ReadOnly Property ModuleName() As String Implements Interfaces.GenericModule.ModuleName
        Get
            Return _Name
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

    Public Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        _setup.chkEnabled.Checked = _enabled
        SPanel.Name = _Name
        SPanel.Text = "Mapping Manager"
        SPanel.Prefix = "MappingManager_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_enabled, 9, 10)
        SPanel.Image = My.Resources.icon
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        Return SPanel
    End Function

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent ModuleEnabledChanged(_Name, State, 0)
    End Sub

    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(mnuMainToolsCertificationMapping)
        tsi.DropDownItems.Remove(mnuMainToolsCountryMapping)
        tsi.DropDownItems.Remove(mnuMainToolsEditionMapping)
        tsi.DropDownItems.Remove(mnuMainToolsGenreMapping)
        tsi.DropDownItems.Remove(mnuMainToolsStatusMapping)
        tsi.DropDownItems.Remove(mnuMainToolsStudioMapping)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(cmnuTrayToolsCertificationMapping)
        tsi.DropDownItems.Remove(cmnuTrayToolsCountryMapping)
        tsi.DropDownItems.Remove(cmnuTrayToolsEditionMapping)
        tsi.DropDownItems.Remove(cmnuTrayToolsGenreMapping)
        tsi.DropDownItems.Remove(cmnuTrayToolsStatusMapping)
        tsi.DropDownItems.Remove(cmnuTrayToolsStudioMapping)
    End Sub

    Sub Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        mnuMainToolsCertificationMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsCertificationMapping.Text = Master.eLang.GetString(1114, "Certification Mapping")
        mnuMainToolsCertificationMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsCertificationMapping)

        mnuMainToolsCountryMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsCountryMapping.Text = Master.eLang.GetString(884, "Country Mapping")
        mnuMainToolsCountryMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsCountryMapping)

        mnuMainToolsEditionMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsEditionMapping.Text = Master.eLang.GetString(126, "Edition Mapping")
        mnuMainToolsEditionMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsEditionMapping)

        mnuMainToolsGenreMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsGenreMapping.Text = Master.eLang.GetString(782, "Genre Mapping")
        mnuMainToolsGenreMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsGenreMapping)

        mnuMainToolsStatusMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsStatusMapping.Text = Master.eLang.GetString(1144, "Status Mapping")
        mnuMainToolsStatusMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsStatusMapping)

        mnuMainToolsStudioMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsStudioMapping.Text = Master.eLang.GetString(1113, "Studio Mapping")
        mnuMainToolsStudioMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsStudioMapping)

        'cmnuTrayTools
        cmnuTrayToolsCertificationMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsCertificationMapping.Text = Master.eLang.GetString(1114, "Certification Mapping")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsCertificationMapping)

        cmnuTrayToolsCountryMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsCountryMapping.Text = Master.eLang.GetString(884, "Country Mapping")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsCountryMapping)

        cmnuTrayToolsEditionMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsEditionMapping.Text = Master.eLang.GetString(126, "Edition Mapping")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsEditionMapping)

        cmnuTrayToolsGenreMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsGenreMapping.Text = Master.eLang.GetString(782, "Genre Mapping")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsGenreMapping)

        cmnuTrayToolsStatusMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsStatusMapping.Text = Master.eLang.GetString(1144, "Status Mapping")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsStatusMapping)

        cmnuTrayToolsStudioMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsStudioMapping.Text = Master.eLang.GetString(1113, "Studio Mapping")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsStudioMapping)
    End Sub

    Public Sub AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Private Sub CertificationMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsCertificationMapping.Click, cmnuTrayToolsCertificationMapping.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgSimpleMapping(dlgSimpleMapping.MappingType.CertificationMapping)
            dlgMapping.ShowDialog()
        End Using
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub CountryMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsCountryMapping.Click, cmnuTrayToolsCountryMapping.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgSimpleMapping(dlgSimpleMapping.MappingType.CountryMapping)
            dlgMapping.ShowDialog()
        End Using
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub EditionMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsEditionMapping.Click, cmnuTrayToolsEditionMapping.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgRegexMapping(dlgRegexMapping.MappingType.EditionMapping)
            dlgMapping.ShowDialog()
        End Using
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub GenereMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsGenreMapping.Click, cmnuTrayToolsGenreMapping.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgGenreMapping
            dlgMapping.ShowDialog()
        End Using
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub StatusMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsStatusMapping.Click, cmnuTrayToolsStatusMapping.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgSimpleMapping(dlgSimpleMapping.MappingType.StatusMapping)
            dlgMapping.ShowDialog()
        End Using
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub StudioMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsStudioMapping.Click, cmnuTrayToolsStudioMapping.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgSimpleMapping(dlgSimpleMapping.MappingType.StudioMapping)
            dlgMapping.ShowDialog()
        End Using
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = _setup.chkEnabled.Checked
        If DoDispose Then
            RemoveHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            _setup.Dispose()
        End If
    End Sub

#End Region 'Methods

End Class