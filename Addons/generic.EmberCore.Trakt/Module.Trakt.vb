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

Public Class Trakt_Generic
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty
    Private WithEvents cmnuTrayToolsTrakt As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuMainToolsTrakt As New System.Windows.Forms.ToolStripMenuItem
    Private _enabled As Boolean = False
    Private _Name As String = "Trakt.tv Manager"
    Private MySettings As New _MySettings

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object)) Implements EmberAPI.Interfaces.GenericModule.GenericEvent

    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged

    Public Event ModuleSettingsChanged() Implements EmberAPI.Interfaces.GenericModule.ModuleSettingsChanged

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

    ''' <summary>
    ''' Commandline call: Update/Sync playcounts of movies and episodes
    ''' </summary>
    ''' <remarks>
    ''' TODO: Needs some testing (error handling..)!? Idea: Can be executed via commandline to update/sync playcounts of movies and episodes
    ''' </remarks>
    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _refparam As Object, ByRef _dbmovie As Structures.DBMovie, ByRef _dbtv As Structures.DBTV, ByRef _dbmovieset As Structures.DBMovieSet) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Try
            dlgTrakttvManager.CLSyncPlaycount()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, mnuMainToolsTrakt)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, cmnuTrayToolsTrakt)
    End Sub

    Sub Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        mnuMainToolsTrakt.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsTrakt.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsTrakt)

        'cmnuTrayTools
        cmnuTrayToolsTrakt.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsTrakt.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsTrakt)
    End Sub

    Public Sub AddToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Remove(value)
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
        Dim SPanel As New Containers.SettingsPanel
        Me._setup = New frmSettingsHolder
        Me._setup.chkEnabled.Checked = Me._enabled
        Me._setup.txtUsername.Text = MySettings.Username
        Me._setup.txtPassword.Text = MySettings.Password
        Me._setup.chkGetShowProgress.Checked = MySettings.GetShowProgress
        SPanel.Name = Me._Name
        SPanel.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        SPanel.Prefix = "Trakt_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(Me._enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings
        AddHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Private Sub MyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsTrakt.Click, cmnuTrayToolsTrakt.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))

        Using dTrakttvManager As New dlgTrakttvManager
            dTrakttvManager.ShowDialog()
        End Using

        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub
    Sub LoadSettings()
        MySettings.Username = clsAdvancedSettings.GetSetting("Username", "")
        MySettings.Password = clsAdvancedSettings.GetSetting("Password", "")
        MySettings.GetShowProgress = clsAdvancedSettings.GetBooleanSetting("GetShowProgress", False)
    End Sub
    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Me.Enabled = Me._setup.chkEnabled.Checked
        MySettings.Username = Me._setup.txtUsername.Text
        MySettings.Password = Me._setup.txtPassword.Text
        MySettings.GetShowProgress = Me._setup.chkGetShowProgress.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetSetting("Username", MySettings.Username)
            settings.SetSetting("Password", MySettings.Password)
            settings.SetBooleanSetting("GetShowProgress", MySettings.GetShowProgress)
        End Using
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure _MySettings

#Region "Fields"
        Dim Username As String
        Dim Password As String
        Dim GetShowProgress As Boolean
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class
