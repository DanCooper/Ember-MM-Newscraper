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

Public Class MovieExporterModule
    Implements Interfaces.GenericModule

#Region "Delegates"
    Public Delegate Sub Delegate_AddToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)

#End Region 'Fields

#Region "Fields"

    Private WithEvents mnuMainToolsExporter As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuTrayToolsExporter As New System.Windows.Forms.ToolStripMenuItem
    Private _AssemblyName As String = String.Empty
    Private _enabled As Boolean = False
    Private _Name As String = "Movie List Exporter"
    Private _setup As frmSettingsHolder
    Private MySettings As New _MySettings

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.GenericModule.GenericEvent

    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged

    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged

    Public Event SetupNeedsRestart() Implements EmberAPI.Interfaces.GenericModule.SetupNeedsRestart

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

    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _refparam As Object, ByRef _dbmovie As Database.DBElement, ByRef _dbtv As Database.DBElement, ByRef _dbmovieset As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Try
            dlgExportMovies.CLExport(DirectCast(_params(0), String), DirectCast(_params(1), String), DirectCast(_params(2), Int32))

        Catch ex As Exception
        End Try

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, mnuMainToolsExporter)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, cmnuTrayToolsExporter)
    End Sub

    Public Sub RemoveToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Remove(value)
        End If
    End Sub

    Sub Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        mnuMainToolsExporter.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsExporter.Text = Master.eLang.GetString(336, "Export Movie List")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsExporter)

        'cmnuTrayTools
        cmnuTrayToolsExporter.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsExporter.Text = Master.eLang.GetString(336, "Export Movie List")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsExporter)
    End Sub

    Public Sub AddToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent ModuleEnabledChanged(Me._Name, State, 0)
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Me._setup = New frmSettingsHolder
        Me._setup.cbEnabled.Checked = Me._enabled
        Dim SPanel As New Containers.SettingsPanel

        _setup.txt_exportmoviepath.Text = MySettings.ExportPath
        _setup.chkExportTVShows.Checked = MySettings.ExportTVShows
        _setup.cbo_exportmovieposter.Text = CStr(MySettings.ExportPosterHeight)
        _setup.cbo_exportmoviefanart.Text = CStr(MySettings.ExportFanartWidth)
        _setup.cbo_exportmoviequality.Text = CStr(MySettings.ExportImageQuality)
        _setup.lbl_exportmoviefilter1saved.Text = MySettings.ExportFilter1
        _setup.lbl_exportmoviefilter2saved.Text = MySettings.ExportFilter2
        _setup.lbl_exportmoviefilter3saved.Text = MySettings.ExportFilter3

        SPanel.Name = Me._Name
        SPanel.Text = Master.eLang.GetString(335, "Movie List Exporter")
        SPanel.Prefix = "Exporter_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(Me._enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings
        AddHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Private Sub MyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsExporter.Click, cmnuTrayToolsExporter.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))

        Using dExportMovies As New dlgExportMovies
            dExportMovies.ShowDialog()
        End Using

        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
    End Sub

    Sub LoadSettings()
        MySettings.ExportPath = clsAdvancedSettings.GetSetting("ExportPath", "")
        MySettings.ExportTVShows = clsAdvancedSettings.GetBooleanSetting("ExportTVShows", False)
        MySettings.ExportPosterHeight = CInt(clsAdvancedSettings.GetSetting("ExportPosterHeight", "300"))
        MySettings.ExportFanartWidth = CInt(clsAdvancedSettings.GetSetting("ExportFanartWidth", "800"))
        MySettings.ExportFilter1 = clsAdvancedSettings.GetSetting("ExportFilter1", "-")
        MySettings.ExportFilter2 = clsAdvancedSettings.GetSetting("ExportFilter2", "-")
        MySettings.ExportFilter3 = clsAdvancedSettings.GetSetting("ExportFilter3", "-")
        MySettings.ExportImageQuality = CInt(clsAdvancedSettings.GetSetting("ExportImageQuality", "70"))
    End Sub

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Me.Enabled = Me._setup.cbEnabled.Checked
        MySettings.ExportPath = _setup.txt_exportmoviepath.Text
        MySettings.ExportTVShows = _setup.chkExportTVShows.Checked
        MySettings.ExportPosterHeight = CInt(_setup.cbo_exportmovieposter.Text)
        MySettings.ExportFanartWidth = CInt(_setup.cbo_exportmoviefanart.Text)
        MySettings.ExportFilter1 = _setup.lbl_exportmoviefilter1saved.Text
        MySettings.ExportFilter2 = _setup.lbl_exportmoviefilter2saved.Text
        MySettings.ExportFilter3 = _setup.lbl_exportmoviefilter3saved.Text
        MySettings.ExportImageQuality = CInt(_setup.cbo_exportmoviequality.Text)
        SaveSettings()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetSetting("ExportPath", MySettings.ExportPath)
            settings.SetBooleanSetting("ExportTVShows", MySettings.ExportTVShows)
            settings.SetSetting("ExportPosterHeight", CStr(MySettings.ExportPosterHeight))
            settings.SetSetting("ExportFanartWidth", CStr(MySettings.ExportFanartWidth))
            settings.SetSetting("ExportFilter1", MySettings.ExportFilter1)
            settings.SetSetting("ExportFilter2", MySettings.ExportFilter2)
            settings.SetSetting("ExportFilter3", MySettings.ExportFilter3)
            settings.SetSetting("ExportImageQuality", CStr(MySettings.ExportImageQuality))
        End Using
    End Sub


#End Region 'Methods

#Region "Nested Types"

    Structure _MySettings

#Region "Fields"

        Dim ExportPath As String
        Dim ExportFanartWidth As Integer
        Dim ExportPosterHeight As Integer
        Dim ExportFilter1 As String
        Dim ExportFilter2 As String
        Dim ExportFilter3 As String
        Dim ExportTVShows As Boolean
        Dim ExportImageQuality As Integer

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class