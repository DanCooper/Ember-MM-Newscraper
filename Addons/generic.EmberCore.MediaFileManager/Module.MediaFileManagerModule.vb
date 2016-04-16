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

Imports System.IO
Imports EmberAPI
Imports NLog

Public Class FileManagerExternalModule
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_SetToolsStripItemVisibility(control As ToolStripItem, value As Boolean)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCopyDirectory As New System.ComponentModel.BackgroundWorker

    Private _AssemblyName As String = String.Empty
    Private _MySettings As New MySettings
    Private eSettings As New Settings
    Private _enabled As Boolean = False
    Private _Name As String = Master.eLang.GetString(311, "Media File Manager")
    Private _setup As frmSettingsHolder
    Private withErrors As Boolean
    Private cmnuMediaCustomList As New List(Of ToolStripMenuItem)
    Private cmnuMedia_Movies As New ToolStripMenuItem
    Private cmnuMedia_Shows As New ToolStripMenuItem
    Private cmnuSep_Movies As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_Shows As New System.Windows.Forms.ToolStripSeparator
    Private WithEvents cmnuMediaCopy_Movies As New ToolStripMenuItem
    Private WithEvents cmnuMediaCopy_Shows As New ToolStripMenuItem
    Private WithEvents cmnuMediaMove_Movies As New ToolStripMenuItem
    Private WithEvents cmnuMediaMove_Shows As New ToolStripMenuItem

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
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic})
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
            Return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    'Public Shared Function MoveFileWithStream(ByVal sPathFrom As String, ByVal sPathTo As String) As Boolean
    '    Try
    '        Using SourceStream As FileStream = New FileStream(String.Concat("", sPathFrom, ""), FileMode.Open, FileAccess.Read)
    '            Using DestinationStream As FileStream = New FileStream(String.Concat("", sPathTo, ""), FileMode.Create, FileAccess.Write)
    '                Dim StreamBuffer(4096) As Byte
    '                Dim nbytes As Integer
    '                Do
    '                    nbytes = SourceStream.Read(StreamBuffer, 0, 4096)
    '                    DestinationStream.Write(StreamBuffer, 0, nbytes)
    '                Loop While nbytes > 0
    '                StreamBuffer = Nothing
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        Return False
    '        logger.Error(ex, New StackFrame().GetMethod().Name)
    '    End Try
    '    Return True
    'End Function

    Public Sub LoadSettings()
        eSettings.ModuleSettings.Clear()
        Dim eMovies As List(Of AdvancedSettingsComplexSettingsTableItem) = clsAdvancedSettings.GetComplexSetting("MoviePaths")
        If eMovies IsNot Nothing Then
            For Each sett In eMovies
                eSettings.ModuleSettings.Add(New SettingItem With {.Name = sett.Name, .FolderPath = sett.Value, .Type = Enums.ContentType.Movie})
            Next
        End If
        Dim eShows As List(Of AdvancedSettingsComplexSettingsTableItem) = clsAdvancedSettings.GetComplexSetting("ShowPaths")
        If eShows IsNot Nothing Then
            For Each sett In eShows
                eSettings.ModuleSettings.Add(New SettingItem With {.Name = sett.Name, .FolderPath = sett.Value, .Type = Enums.ContentType.TVShow})
            Next
        End If
        _MySettings.TeraCopy = clsAdvancedSettings.GetBooleanSetting("TeraCopy", False)
        _MySettings.TeraCopyPath = clsAdvancedSettings.GetSetting("TeraCopyPath", String.Empty)
    End Sub

    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("TeraCopy", _MySettings.TeraCopy)
            settings.SetSetting("TeraCopyPath", _MySettings.TeraCopyPath)

            Dim eMovies As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each e As SettingItem In eSettings.ModuleSettings.Where(Function(f) f.Type = Enums.ContentType.Movie)
                eMovies.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = e.Name, .Value = e.FolderPath})
            Next
            If eMovies IsNot Nothing Then
                settings.SetComplexSetting("MoviePaths", eMovies)
            End If

            Dim eShows As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each e As SettingItem In eSettings.ModuleSettings.Where(Function(f) f.Type = Enums.ContentType.TVShow)
                eShows.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = e.Name, .Value = e.FolderPath})
            Next
            If eShows IsNot Nothing Then
                settings.SetComplexSetting("ShowPaths", eShows)
            End If
        End Using
    End Sub

    Private Sub bwCopyDirectory_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCopyDirectory.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        If Not Args.src = Args.dst Then
            withErrors = False
            DirectoryCopyMove(Args.src, Args.dst, Args.doMove)
        End If
    End Sub

    Sub DirectoryCopy(ByVal src As String, ByVal dst As String, Optional ByVal title As String = "")
        Using dCopy As New dlgCopyFiles
            dCopy.Show()
            dCopy.prbStatus.Style = ProgressBarStyle.Marquee
            dCopy.Text = title
            dCopy.lblFilename.Text = Path.GetFileNameWithoutExtension(src)
            bwCopyDirectory.WorkerReportsProgress = True
            bwCopyDirectory.WorkerSupportsCancellation = True
            bwCopyDirectory.RunWorkerAsync(New Arguments With {.src = src, .dst = dst, .doMove = False})
            While bwCopyDirectory.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Using
    End Sub

    Sub DirectoryMove(ByVal src As String, ByVal dst As String, Optional ByVal title As String = "")
        Using dCopy As New dlgCopyFiles
            dCopy.Show()
            dCopy.prbStatus.Style = ProgressBarStyle.Marquee
            dCopy.Text = title
            dCopy.lblFilename.Text = Path.GetFileNameWithoutExtension(src)
            bwCopyDirectory.WorkerReportsProgress = True
            bwCopyDirectory.WorkerSupportsCancellation = True
            bwCopyDirectory.RunWorkerAsync(New Arguments With {.src = src, .dst = dst, .doMove = True})
            While bwCopyDirectory.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Using
    End Sub

    Sub Disable()
        RemoveToolsStripItem_Movies(cmnuMedia_Movies)
        RemoveToolsStripItem_Movies(cmnuSep_Movies)
        RemoveToolsStripItem_Shows(cmnuMedia_Shows)
        RemoveToolsStripItem_Shows(cmnuSep_Shows)
    End Sub

    Public Sub RemoveToolsStripItem_Movies(value As ToolStripItem)
        If (ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired) Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Movies), New Object() {value})
            Exit Sub
        End If
        ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Remove(value)
    End Sub

    Public Sub RemoveToolsStripItem_Shows(value As ToolStripItem)
        If (ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired) Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Shows), New Object() {value})
            Exit Sub
        End If
        ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Remove(value)
    End Sub

    Sub Enable()
        'cmnuMovies
        cmnuMedia_Movies.Text = Master.eLang.GetString(311, "Media File Manager")
        cmnuMediaMove_Movies.Text = Master.eLang.GetString(312, "Move To")
        cmnuMediaMove_Movies.Tag = "MOVE"
        cmnuMediaCopy_Movies.Text = Master.eLang.GetString(313, "Copy To")
        cmnuMediaCopy_Movies.Tag = "COPY"
        cmnuMedia_Movies.DropDownItems.Add(cmnuMediaMove_Movies)
        cmnuMedia_Movies.DropDownItems.Add(cmnuMediaCopy_Movies)

        SetToolsStripItem_Movies(cmnuSep_Movies)
        SetToolsStripItem_Movies(cmnuMedia_Movies)

        'cmnuShows
        cmnuMedia_Shows.Text = Master.eLang.GetString(311, "Media File Manager")
        cmnuMediaMove_Shows.Text = Master.eLang.GetString(312, "Move To")
        cmnuMediaMove_Shows.Tag = "MOVE"
        cmnuMediaCopy_Shows.Text = Master.eLang.GetString(313, "Copy To")
        cmnuMediaCopy_Shows.Tag = "COPY"
        cmnuMedia_Shows.DropDownItems.Add(cmnuMediaMove_Shows)
        cmnuMedia_Shows.DropDownItems.Add(cmnuMediaCopy_Shows)

        SetToolsStripItem_Shows(cmnuSep_Shows)
        SetToolsStripItem_Shows(cmnuMedia_Shows)

        PopulateFolders(cmnuMediaMove_Movies, Enums.ContentType.Movie)
        PopulateFolders(cmnuMediaMove_Shows, Enums.ContentType.TVShow)
        PopulateFolders(cmnuMediaCopy_Movies, Enums.ContentType.Movie)
        PopulateFolders(cmnuMediaCopy_Shows, Enums.ContentType.TVShow)
        SetToolsStripItemVisibility(cmnuMedia_Movies, True)
        SetToolsStripItemVisibility(cmnuMedia_Shows, True)
        SetToolsStripItemVisibility(cmnuSep_Movies, True)
        SetToolsStripItemVisibility(cmnuSep_Shows, True)
    End Sub

    Public Sub SetToolsStripItemVisibility(control As ToolStripItem, value As Boolean)
        If control.Owner IsNot Nothing Then
            If control.Owner.InvokeRequired Then
                control.Owner.Invoke(New Delegate_SetToolsStripItemVisibility(AddressOf SetToolsStripItemVisibility), New Object() {control, value})
            Else
                control.Visible = value
            End If
        End If
    End Sub

    Public Sub SetToolsStripItem_Movies(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Movies), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_Shows(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Shows), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Add(value)
        End If
    End Sub

    Private Sub FolderSubMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim ItemsToWork As New List(Of IO.FileSystemInfo)
            Dim MediaToWork As New List(Of Long)
            Dim ID As Int64 = -1
            Dim tMItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
            Dim doMove As Boolean = False
            Dim dstPath As String = String.Empty
            Dim useTeraCopy As Boolean = False
            Dim ContentType As Enums.ContentType = DirectCast(tMItem.Tag, SettingItem).Type

            If DirectCast(tMItem.Tag, SettingItem).FolderPath = "CUSTOM" Then
                Dim fl As New FolderBrowserDialog
                fl.ShowDialog()
                dstPath = fl.SelectedPath
            Else
                dstPath = DirectCast(tMItem.Tag, SettingItem).FolderPath
            End If

            Select Case tMItem.OwnerItem.Tag.ToString
                Case "MOVE"
                    doMove = True
            End Select

            If _MySettings.TeraCopy AndAlso (String.IsNullOrEmpty(_MySettings.TeraCopyPath) OrElse Not File.Exists(_MySettings.TeraCopyPath)) Then
                MessageBox.Show(Master.eLang.GetString(398, "TeraCopy.exe not found"), Master.eLang.GetString(1134, "Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Try
            End If

            Dim mTeraCopy As New TeraCopy.Filelist(_MySettings.TeraCopyPath, dstPath, doMove)

            If Not String.IsNullOrEmpty(dstPath) Then
                If ContentType = Enums.ContentType.Movie Then
                    For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                        ID = Convert.ToInt64(sRow.Cells("idMovie").Value)
                        If Not MediaToWork.Contains(ID) Then
                            MediaToWork.Add(ID)
                        End If
                    Next
                ElseIf ContentType = Enums.ContentType.TVShow Then
                    For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                        ID = Convert.ToInt64(sRow.Cells("idShow").Value)
                        If Not MediaToWork.Contains(ID) Then
                            MediaToWork.Add(ID)
                        End If
                    Next
                End If
                If MediaToWork.Count > 0 Then
                    Dim strMove As String = String.Empty
                    Dim strCopy As String = String.Empty
                    If ContentType = Enums.ContentType.Movie Then
                        strMove = Master.eLang.GetString(314, "Move {0} Movie(s) To {1}")
                        strCopy = Master.eLang.GetString(315, "Copy {0} Movie(s) To {1}")
                    ElseIf ContentType = Enums.ContentType.TVShow Then
                        strMove = Master.eLang.GetString(888, "Move {0} TV Show(s) To {1}")
                        strCopy = Master.eLang.GetString(889, "Copy {0} TV Show(s) To {1}")
                    End If

                    If MessageBox.Show(String.Format(If(doMove, strMove, strCopy),
                                            MediaToWork.Count, dstPath), If(doMove, Master.eLang.GetString(910, "Move"), Master.eLang.GetString(911, "Copy")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        If ContentType = Enums.ContentType.Movie Then
                            Dim FileDelete As New FileUtils.Delete
                            For Each movieID As Long In MediaToWork
                                Dim mMovie As Database.DBElement = Master.DB.Load_Movie(movieID)
                                ItemsToWork = FileDelete.GetItemsToDelete(False, mMovie)
                                If ItemsToWork.Count = 1 AndAlso Directory.Exists(ItemsToWork(0).ToString) Then
                                    If _MySettings.TeraCopy Then
                                        mTeraCopy.Sources.Add(ItemsToWork(0).ToString)
                                    Else
                                        Select Case tMItem.OwnerItem.Tag.ToString
                                            Case "MOVE"
                                                DirectoryMove(ItemsToWork(0).ToString, Path.Combine(dstPath, Path.GetFileName(ItemsToWork(0).ToString)), Master.eLang.GetString(316, "Moving Movie"))
                                                Master.DB.Delete_Movie(movieID, False)
                                            Case "COPY"
                                                DirectoryCopy(ItemsToWork(0).ToString, Path.Combine(dstPath, Path.GetFileName(ItemsToWork(0).ToString)), Master.eLang.GetString(317, "Copying Movie"))
                                        End Select
                                    End If
                                End If
                            Next
                            If Not _MySettings.TeraCopy AndAlso doMove Then ModulesManager.Instance.RuntimeObjects.InvokeLoadMedia(New Structures.ScanOrClean With {.Movies = True})
                        ElseIf ContentType = Enums.ContentType.TVShow Then
                            Dim FileDelete As New FileUtils.Delete
                            For Each tShowID As Long In MediaToWork
                                Dim mShow As Database.DBElement = Master.DB.Load_TVShow(tShowID, False, False)
                                If Directory.Exists(mShow.ShowPath) Then
                                    If _MySettings.TeraCopy Then
                                        mTeraCopy.Sources.Add(mShow.ShowPath)
                                    Else
                                        Select Case tMItem.OwnerItem.Tag.ToString
                                            Case "MOVE"
                                                DirectoryMove(mShow.ShowPath, Path.Combine(dstPath, Path.GetFileName(mShow.ShowPath)), Master.eLang.GetString(899, "Moving TV Show"))
                                                Master.DB.Delete_TVShow(tShowID, False)
                                            Case "COPY"
                                                DirectoryCopy(mShow.ShowPath, Path.Combine(dstPath, Path.GetFileName(mShow.ShowPath)), Master.eLang.GetString(900, "Copying TV Show"))
                                        End Select
                                    End If
                                End If
                            Next
                            If Not _MySettings.TeraCopy AndAlso doMove Then ModulesManager.Instance.RuntimeObjects.InvokeLoadMedia(New Structures.ScanOrClean With {.TV = True})
                        End If
                        If _MySettings.TeraCopy Then mTeraCopy.RunTeraCopy()
                    End If
                End If
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
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
        _setup.chkTeraCopyEnable.Checked = _MySettings.TeraCopy
        _setup.txtTeraCopyPath.Text = _MySettings.TeraCopyPath
        _setup.lvPaths.Items.Clear()
        Dim lvItem As ListViewItem
        For Each e As SettingItem In eSettings.ModuleSettings
            lvItem = New ListViewItem
            lvItem.Text = e.Name
            lvItem.SubItems.Add(e.FolderPath)
            lvItem.SubItems.Add(e.Type.ToString)
            _setup.lvPaths.Items.Add(lvItem)
        Next
        SPanel.Name = _Name
        SPanel.Text = Master.eLang.GetString(311, "Media File Manager")
        SPanel.Prefix = "FileManager_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Sub PopulateFolders(ByVal mnu As ToolStripMenuItem, ByVal ContentType As Enums.ContentType)
        mnu.DropDownItems.Clear()
        cmnuMediaCustomList.RemoveAll(Function(b) True)

        Dim FolderSubMenuItemCustom As New ToolStripMenuItem
        FolderSubMenuItemCustom.Text = String.Concat(Master.eLang.GetString(338, "Select path"), "...")
        FolderSubMenuItemCustom.Tag = New SettingItem With {.Name = "CUSTOM", .FolderPath = "CUSTOM", .Type = ContentType}
        mnu.DropDownItems.Add(FolderSubMenuItemCustom)
        AddHandler FolderSubMenuItemCustom.Click, AddressOf FolderSubMenuItem_Click

        If eSettings.ModuleSettings.Where(Function(f) f.Type = ContentType).Count > 0 Then
            Dim SubMenuSep As New System.Windows.Forms.ToolStripSeparator
            mnu.DropDownItems.Add(SubMenuSep)
        End If

        For Each e In eSettings.ModuleSettings.Where(Function(f) f.Type = ContentType)
            Dim FolderSubMenuItem As New ToolStripMenuItem
            FolderSubMenuItem.Text = e.Name
            FolderSubMenuItem.Tag = New SettingItem With {.Name = e.Name, .FolderPath = e.FolderPath, .Type = ContentType}
            cmnuMediaCustomList.Add(FolderSubMenuItem)
            AddHandler FolderSubMenuItem.Click, AddressOf FolderSubMenuItem_Click
        Next

        For Each i In cmnuMediaCustomList
            mnu.DropDownItems.Add(i)
        Next

        SetToolsStripItemVisibility(cmnuSep_Movies, True)
        SetToolsStripItemVisibility(cmnuSep_Shows, True)
        SetToolsStripItemVisibility(cmnuMedia_Movies, True)
        SetToolsStripItemVisibility(cmnuMedia_Shows, True)
    End Sub

    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = _setup.chkEnabled.Checked
        _MySettings.TeraCopy = _setup.chkTeraCopyEnable.Checked
        _MySettings.TeraCopyPath = _setup.txtTeraCopyPath.Text
        eSettings.ModuleSettings.Clear()
        For Each e As ListViewItem In _setup.lvPaths.Items
            If Not String.IsNullOrEmpty(e.SubItems(0).Text) AndAlso Not String.IsNullOrEmpty(e.SubItems(1).Text) AndAlso e.SubItems(2).Text = "Movie" Then
                eSettings.ModuleSettings.Add(New SettingItem With {.Name = e.SubItems(0).Text, .FolderPath = e.SubItems(1).Text, .Type = Enums.ContentType.Movie})
            End If
        Next
        For Each e As ListViewItem In _setup.lvPaths.Items
            If Not String.IsNullOrEmpty(e.SubItems(0).Text) AndAlso Not String.IsNullOrEmpty(e.SubItems(1).Text) AndAlso e.SubItems(2).Text = "Show" Then
                eSettings.ModuleSettings.Add(New SettingItem With {.Name = e.SubItems(0).Text, .FolderPath = e.SubItems(1).Text, .Type = Enums.ContentType.TVShow})
            End If
        Next
        SaveSettings()
        PopulateFolders(cmnuMediaMove_Movies, Enums.ContentType.Movie)
        PopulateFolders(cmnuMediaMove_Shows, Enums.ContentType.TVShow)
        PopulateFolders(cmnuMediaCopy_Movies, Enums.ContentType.Movie)
        PopulateFolders(cmnuMediaCopy_Shows, Enums.ContentType.TVShow)
        If DoDispose Then
            RemoveHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Private Sub DirectoryCopyMove(ByVal sourceDirName As String, ByVal destDirName As String, ByVal doMove As Boolean)
        If Not doMove Then
            FileUtils.Common.DirectoryCopy(sourceDirName, destDirName, True, True)
        Else
            FileUtils.Common.DirectoryMove(sourceDirName, destDirName, True, True)
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim dst As String
        Dim src As String
        Dim doMove As Boolean

#End Region 'Fields

    End Structure

    Private Structure MySettings

#Region "Fields"

        Dim TeraCopy As Boolean
        Dim TeraCopyPath As String

#End Region 'Fields

    End Structure

    Class SettingItem

#Region "Fields"

        Public FolderPath As String
        Public Name As String
        Public Type As Enums.ContentType

#End Region 'Fields

    End Class

    Class Settings

#Region "Fields"

        Private _settings As New List(Of SettingItem)

#End Region 'Fields

#Region "Properties"

        Public Property ModuleSettings() As List(Of SettingItem)
            Get
                Return _settings
            End Get
            Set(ByVal value As List(Of SettingItem))
                _settings = value
            End Set
        End Property

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class