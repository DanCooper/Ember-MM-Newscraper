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

Public Class MovieExporterModule
    Implements Interfaces.GenericModule

#Region "Delegates"
    Public Delegate Sub Delegate_AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)

#End Region 'Fields

#Region "Fields"

    Private WithEvents mnuMainToolsExporter As New ToolStripMenuItem
    Private WithEvents cmnuTrayToolsExporter As New ToolStripMenuItem
    Private _AssemblyName As String = String.Empty
    Private _enabled As Boolean = False
    Private _Name As String = "MovieListExporter"
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

    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Select Case mType
            Case Enums.ModuleEventType.CommandLine
                Dim strTemplatePath As String = String.Empty
                Dim BuildPath As String = String.Empty

                If _params IsNot Nothing Then
                    For Each tParameter In _params
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
                                Dim diDefault As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Modules", "Templates"))
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
                    BuildPath = MySettings.ExportPath
                End If

                If String.IsNullOrEmpty(strTemplatePath) Then
                    strTemplatePath = MySettings.DefaultTemplate
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
                            TVShowList.Add(Master.DB.Load_TVShow(Convert.ToInt32(SQLreader("idShow")), True, True, MySettings.ExportMissingEpisodes))
                        End While
                    End Using
                End Using

                Dim MExporter As New MediaExporter
                MExporter.CreateTemplate(strTemplatePath, MovieList, TVShowList, BuildPath, Nothing)
        End Select

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, mnuMainToolsExporter)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, cmnuTrayToolsExporter)
    End Sub

    Public Sub RemoveToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
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
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsExporter)

        'cmnuTrayTools
        cmnuTrayToolsExporter.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsExporter.Text = Master.eLang.GetString(336, "Export Movie List")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsExporter)
    End Sub

    Public Sub AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
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
        _setup = New frmSettingsHolder
        _setup.cbEnabled.Checked = _enabled
        Dim SPanel As New Containers.SettingsPanel

        _setup.txtExportPath.Text = MySettings.ExportPath
        _setup.chkExportMissingEpisodes.Checked = MySettings.ExportMissingEpisodes

        SPanel.Name = _Name
        SPanel.Text = Master.eLang.GetString(335, "Movie List Exporter")
        SPanel.Prefix = "Exporter_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
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
        MySettings.DefaultTemplate = AdvancedSettings.GetSetting("DefaultTemplate", "template")
        MySettings.ExportPath = AdvancedSettings.GetSetting("ExportPath", String.Empty)
        MySettings.ExportMissingEpisodes = AdvancedSettings.GetBooleanSetting("ExportMissingEpisodes", False)
    End Sub

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = _setup.cbEnabled.Checked
        MySettings.ExportPath = _setup.txtExportPath.Text
        MySettings.ExportMissingEpisodes = _setup.chkExportMissingEpisodes.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetSetting("DefaultTemplate", MySettings.DefaultTemplate)
            settings.SetSetting("ExportPath", MySettings.ExportPath)
            settings.SetBooleanSetting("ExportMissingEpisodes", MySettings.ExportMissingEpisodes)
        End Using
    End Sub


#End Region 'Methods

#Region "Nested Types"

    Structure _MySettings

#Region "Fields"

        Dim DefaultTemplate As String
        Dim ExportPath As String
        Dim ExportMissingEpisodes As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class