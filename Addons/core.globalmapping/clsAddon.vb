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

Public Class clsAddon
    Implements Interfaces.IAddon

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(tsi As ToolStripMenuItem, value As ToolStripMenuItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_SetToolsStripItem(value As ToolStripItem)

#End Region 'Delegates

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _SettingsPanel_Addon As frmSettingsPanel



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

    'Public Event GenericEvent(ByVal eventType As Enums.AddonEventType, ByRef parameters As List(Of Object)) Implements Interfaces.IAddon_Generic.GenericEvent
    'Public Event AddonSettingsChanged() Implements Interfaces.IAddon_Generic.AddonSettingsChanged
    'Public Event AddonStateChanged(ByVal name As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.IAddon_Generic.AddonStateChanged
    'Public Event AddonNeedsRestart() Implements Interfaces.IAddon_Generic.AddonNeedsRestart

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property Capabilities_AddonEventTypes As List(Of Enums.AddonEventType) Implements Interfaces.IAddon.Capabilities_AddonEventTypes
        Get
            Return New List(Of Enums.AddonEventType)
        End Get
    End Property

    Public ReadOnly Property Capabilities_ScraperCapatibilities As List(Of Enums.ScraperCapatibility) Implements Interfaces.IAddon.Capabilities_ScraperCapatibilities
        Get
            Return New List(Of Enums.ScraperCapatibility)
        End Get
    End Property

    Public ReadOnly Property IsBusy As Boolean Implements Interfaces.IAddon.IsBusy
        Get
            Return False
        End Get
    End Property

    Public Property IsEnabled_Generic As Boolean Implements Interfaces.IAddon.IsEnabled_Generic

    Public Property IsEnabled_Data_Movie As Boolean Implements Interfaces.IAddon.IsEnabled_Data_Movie

    Public Property IsEnabled_Data_Movieset As Boolean Implements Interfaces.IAddon.IsEnabled_Data_Movieset

    Public Property IsEnabled_Data_TV As Boolean Implements Interfaces.IAddon.IsEnabled_Data_TV

    Public Property IsEnabled_Image_Movie As Boolean Implements Interfaces.IAddon.IsEnabled_Image_Movie

    Public Property IsEnabled_Image_Movieset As Boolean Implements Interfaces.IAddon.IsEnabled_Image_Movieset

    Public Property IsEnabled_Image_TV As Boolean Implements Interfaces.IAddon.IsEnabled_Image_TV

    Public Property IsEnabled_Theme_Movie As Boolean Implements Interfaces.IAddon.IsEnabled_Theme_Movie

    Public Property IsEnabled_Theme_TV As Boolean Implements Interfaces.IAddon.IsEnabled_Theme_TV

    Public Property IsEnabled_Trailer_Movie As Boolean Implements Interfaces.IAddon.IsEnabled_Trailer_Movie

    Public Property SettingsPanels As New List(Of Interfaces.ISettingsPanel) Implements Interfaces.IAddon.SettingsPanels

#End Region 'Properties

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IAddon.Init
        Enable()
    End Sub

    Public Sub InjectSettingsPanels() Implements Interfaces.IAddon.InjectSettingsPanels
        'Addon global
        _SettingsPanel_Addon = New frmSettingsPanel
        _SettingsPanel_Addon.IsEnabled = True
        SettingsPanels.Add(_SettingsPanel_Addon)
    End Sub

    Public Sub SaveSettings(ByVal doDispose As Boolean) Implements Interfaces.IAddon.SaveSettings
        'Global
        'Enabled = _SettingsPanel_Addon.chkEnabled.Checked
        If doDispose Then
            _SettingsPanel_Addon.Dispose()
        End If
    End Sub

    Public Function Run(ByRef dbElement As Database.DBElement, type As Enums.AddonEventType, lstCommandLineParams As List(Of Object)) As Interfaces.AddonResult Implements Interfaces.IAddon.Run
        Return New Interfaces.AddonResult
    End Function

#End Region 'Interface Methods

#Region "Methods"

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(mnuMainToolsCertificationMapping)
        tsi.DropDownItems.Remove(mnuMainToolsCountryMapping)
        tsi.DropDownItems.Remove(mnuMainToolsEditionMapping)
        tsi.DropDownItems.Remove(mnuMainToolsGenreMapping)
        tsi.DropDownItems.Remove(mnuMainToolsStatusMapping)
        tsi.DropDownItems.Remove(mnuMainToolsStudioMapping)

        'cmnuTrayTools
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
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
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsCertificationMapping)

        mnuMainToolsCountryMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsCountryMapping.Text = Master.eLang.GetString(884, "Country Mapping")
        mnuMainToolsCountryMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsCountryMapping)

        mnuMainToolsEditionMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsEditionMapping.Text = Master.eLang.GetString(126, "Edition Mapping")
        mnuMainToolsEditionMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsEditionMapping)

        mnuMainToolsGenreMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsGenreMapping.Text = Master.eLang.GetString(782, "Genre Mapping")
        mnuMainToolsGenreMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsGenreMapping)

        mnuMainToolsStatusMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsStatusMapping.Text = Master.eLang.GetString(1144, "Status Mapping")
        mnuMainToolsStatusMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsStatusMapping)

        mnuMainToolsStudioMapping.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsStudioMapping.Text = Master.eLang.GetString(1113, "Studio Mapping")
        mnuMainToolsStudioMapping.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsStudioMapping)

        'cmnuTrayTools
        cmnuTrayToolsCertificationMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsCertificationMapping.Text = Master.eLang.GetString(1114, "Certification Mapping")
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsCertificationMapping)

        cmnuTrayToolsCountryMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsCountryMapping.Text = Master.eLang.GetString(884, "Country Mapping")
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsCountryMapping)

        cmnuTrayToolsEditionMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsEditionMapping.Text = Master.eLang.GetString(126, "Edition Mapping")
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsEditionMapping)

        cmnuTrayToolsGenreMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsGenreMapping.Text = Master.eLang.GetString(782, "Genre Mapping")
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsGenreMapping)

        cmnuTrayToolsStatusMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsStatusMapping.Text = Master.eLang.GetString(1144, "Status Mapping")
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsStatusMapping)

        cmnuTrayToolsStudioMapping.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsStudioMapping.Text = Master.eLang.GetString(1113, "Studio Mapping")
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
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
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgSimpleMapping(dlgSimpleMapping.MappingType.CertificationMapping)
            dlgMapping.ShowDialog()
        End Using
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub CountryMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsCountryMapping.Click, cmnuTrayToolsCountryMapping.Click
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgSimpleMapping(dlgSimpleMapping.MappingType.CountryMapping)
            dlgMapping.ShowDialog()
        End Using
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        ''RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub EditionMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsEditionMapping.Click, cmnuTrayToolsEditionMapping.Click
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgRegexMapping(dlgRegexMapping.MappingType.EditionMapping)
            dlgMapping.ShowDialog()
        End Using
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub GenereMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsGenreMapping.Click, cmnuTrayToolsGenreMapping.Click
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgGenreMapping
            dlgMapping.ShowDialog()
        End Using
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub StatusMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsStatusMapping.Click, cmnuTrayToolsStatusMapping.Click
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgSimpleMapping(dlgSimpleMapping.MappingType.StatusMapping)
            dlgMapping.ShowDialog()
        End Using
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Private Sub StudioMapping_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsStudioMapping.Click, cmnuTrayToolsStudioMapping.Click
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Using dlgMapping As New dlgSimpleMapping(dlgSimpleMapping.MappingType.StudioMapping)
            dlgMapping.ShowDialog()
        End Using
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        'RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

#End Region 'Methods

End Class