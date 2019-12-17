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
Imports System.IO

Public Class Generic
    Implements Interfaces.IGenericAddon

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)

#End Region 'Delegates

#Region "Fields"

    Private WithEvents _CmnuTrayToolsExporter As New ToolStripMenuItem
    Private WithEvents _MnuMainToolsExporter As New ToolStripMenuItem
    Private _AddonSettings As New AddonSettings
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel

#End Region 'Fields

#Region "Properties"

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IGenericAddon.IsBusy
        Get
            Return False
        End Get
    End Property

    Property IsEnabled() As Boolean Implements Interfaces.IGenericAddon.IsEnabled
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

    Public ReadOnly Property Type() As List(Of Enums.ModuleEventType) Implements Interfaces.IGenericAddon.Type
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {
                                                      Enums.ModuleEventType.Generic,
                                                      Enums.ModuleEventType.CommandLine
                                                      })
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.IGenericAddon.GenericEvent
    Public Event NeedsRestart() Implements Interfaces.IGenericAddon.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IGenericAddon.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IGenericAddon.StateChanged

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

    Public Sub Init() Implements Interfaces.IGenericAddon.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IGenericAddon.InjectSettingsPanel
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.cbEnabled.Checked = IsEnabled
        _PnlSettingsPanel.txtExportPath.Text = _AddonSettings.ExportPath
        _PnlSettingsPanel.chkExportMissingEpisodes.Checked = _AddonSettings.ExportMissingEpisodes
        AddHandler _PnlSettingsPanel.ModuleEnabledChanged, AddressOf Handle_StateChanged
        AddHandler _PnlSettingsPanel.ModuleSettingsChanged, AddressOf Handle_SettingsChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(_Enabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = Master.eLang.GetString(328, "Website Creator"),
            .Type = Enums.SettingsPanelType.Addon
        }
    End Sub

    Public Function Run(ByVal ModuleEventType As Enums.ModuleEventType, ByRef Parameters As List(Of Object), ByRef SingleObjekt As Object, ByRef DBElement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericAddon.Run
        Select Case ModuleEventType
            Case Enums.ModuleEventType.CommandLine
                Dim strTemplatePath As String = String.Empty
                Dim BuildPath As String = String.Empty

                If Parameters IsNot Nothing Then
                    For Each tParameter In Parameters
                        'check if tParameter is a path or template name
                        If Not String.IsNullOrEmpty(Path.GetPathRoot(tParameter.ToString)) Then
                            BuildPath = tParameter.ToString
                        Else
                            'search in Ember custom templates
                            Dim diCustom As DirectoryInfo = New DirectoryInfo(Path.Combine(Master.SettingsPath, "Templates"))
                            If diCustom.Exists Then
                                For Each i As DirectoryInfo In diCustom.GetDirectories
                                    If Not (i.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden AndAlso i.Name = tParameter.ToString Then
                                        strTemplatePath = i.FullName
                                    End If
                                Next
                            End If

                            If String.IsNullOrEmpty(strTemplatePath) Then
                                'search in Ember default templates
                                Dim diDefault As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Addons\addon.websitecreator", "Templates"))
                                If diDefault.Exists Then
                                    For Each i As DirectoryInfo In diDefault.GetDirectories
                                        If Not (i.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden AndAlso i.Name = tParameter.ToString Then
                                            strTemplatePath = i.FullName
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If

                If String.IsNullOrEmpty(BuildPath) Then
                    BuildPath = _AddonSettings.ExportPath
                End If

                If String.IsNullOrEmpty(strTemplatePath) Then
                    strTemplatePath = _AddonSettings.DefaultTemplate
                End If

                Dim MovieList As New List(Of Database.DBElement)
                ' Load nfo movies using path from DB
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT idMovie FROM movielist ORDER BY SortedTitle COLLATE NOCASE;")
                    Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLreader.Read()
                            MovieList.Add(Master.DB.Load_Movie(Convert.ToInt32(SQLreader("idMovie"))))
                        End While
                    End Using
                End Using

                Dim TVShowList As New List(Of Database.DBElement)
                ' Load nfo tv shows using path from DB
                Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                    SQLNewcommand.CommandText = String.Concat("SELECT idShow FROM tvshowlist ORDER BY SortedTitle COLLATE NOCASE;")
                    Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                        While SQLreader.Read()
                            TVShowList.Add(Master.DB.Load_TVShow(Convert.ToInt32(SQLreader("idShow")), True, True, _AddonSettings.ExportMissingEpisodes))
                        End While
                    End Using
                End Using

                Dim MExporter As New WebsiteCreator
                MExporter.CreateTemplate(strTemplatePath, MovieList, TVShowList, BuildPath, Nothing)
        End Select

        Return New Interfaces.ModuleResult
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericAddon.SaveSetup
        IsEnabled = _PnlSettingsPanel.cbEnabled.Checked
        _AddonSettings.ExportPath = _PnlSettingsPanel.txtExportPath.Text
        _AddonSettings.ExportMissingEpisodes = _PnlSettingsPanel.chkExportMissingEpisodes.Checked
        Settings_Save()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.ModuleEnabledChanged, AddressOf Handle_StateChanged
            RemoveHandler _PnlSettingsPanel.ModuleSettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub ToolsStripMenu_Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, _MnuMainToolsExporter)

        'cmnuTrayTools
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, _CmnuTrayToolsExporter)
    End Sub

    Sub ToolsStripMenu_Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        _MnuMainToolsExporter.Image = New Bitmap(My.Resources.icon)
        _MnuMainToolsExporter.Text = Master.eLang.GetString(328, "Website Creator")
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, _MnuMainToolsExporter)

        'cmnuTrayTools
        _CmnuTrayToolsExporter.Image = New Bitmap(My.Resources.icon)
        _CmnuTrayToolsExporter.Text = Master.eLang.GetString(328, "Website Creator")
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, _CmnuTrayToolsExporter)
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

    Private Sub MyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _MnuMainToolsExporter.Click, _CmnuTrayToolsExporter.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))

        Using dExportMovies As New dlgWebsiteCreator
            dExportMovies.ShowDialog()
        End Using

        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
    End Sub

    Sub Settings_Load()
        _AddonSettings.DefaultTemplate = AdvancedSettings.GetSetting("DefaultTemplate", "template")
        _AddonSettings.ExportPath = AdvancedSettings.GetSetting("ExportPath", String.Empty)
        _AddonSettings.ExportMissingEpisodes = AdvancedSettings.GetBooleanSetting("ExportMissingEpisodes", False)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetSetting("DefaultTemplate", _AddonSettings.DefaultTemplate)
            settings.SetSetting("ExportPath", _AddonSettings.ExportPath)
            settings.SetBooleanSetting("ExportMissingEpisodes", _AddonSettings.ExportMissingEpisodes)
        End Using
    End Sub


#End Region 'Methods

#Region "Nested Types"

    Structure AddonSettings

#Region "Fields"

        Dim DefaultTemplate As String
        Dim ExportPath As String
        Dim ExportMissingEpisodes As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class