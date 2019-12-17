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

Public Class Generic
    Implements Interfaces.IGenericAddon

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_SetToolsStripItemVisibility(control As ToolStripItem, value As Boolean)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Friend WithEvents bwCopyDirectory As New System.ComponentModel.BackgroundWorker

    Private _AddonSettings As New AddonSettings
    Private _CmnuMediaCustomList As New List(Of ToolStripMenuItem)
    Private _CmnuMedia_Movies As New ToolStripMenuItem
    Private _CmnuMedia_Shows As New ToolStripMenuItem
    Private _CmnuSep_Movies As New ToolStripSeparator
    Private _CmnuSep_Shows As New ToolStripSeparator
    Private WithEvents _CmnuMediaCopy_Movies As New ToolStripMenuItem
    Private WithEvents _CmnuMediaCopy_Shows As New ToolStripMenuItem
    Private WithEvents _CmnuMediaMove_Movies As New ToolStripMenuItem
    Private WithEvents _CmnuMediaMove_Shows As New ToolStripMenuItem
    Private _Enabled As Boolean = False
    Private _ESettings As New Settings
    Private _PnlSettingsPanel As frmSettingsHolder
    Private _WithErrors As Boolean

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
                ToolStripMenu_Disable()
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IGenericAddon.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IGenericAddon.SettingsPanel

    Public ReadOnly Property Type() As List(Of Enums.ModuleEventType) Implements Interfaces.IGenericAddon.Type
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic})
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
        _PnlSettingsPanel = New frmSettingsHolder
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkTeraCopyEnable.Checked = _AddonSettings.TeraCopy
        _PnlSettingsPanel.txtTeraCopyPath.Text = _AddonSettings.TeraCopyPath
        _PnlSettingsPanel.lvPaths.Items.Clear()
        Dim lvItem As ListViewItem
        For Each e As SettingItem In _ESettings.ModuleSettings
            lvItem = New ListViewItem
            lvItem.Text = e.Name
            lvItem.SubItems.Add(e.FolderPath)
            lvItem.SubItems.Add(e.Type.ToString)
            _PnlSettingsPanel.lvPaths.Items.Add(lvItem)
        Next
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(_Enabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = Master.eLang.GetString(311, "File Manager"),
            .Type = Enums.SettingsPanelType.Addon
        }
    End Sub

    Public Function Run(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericAddon.Run
        Return New Interfaces.ModuleResult
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericAddon.SaveSetup
        IsEnabled = _PnlSettingsPanel.chkEnabled.Checked
        _AddonSettings.TeraCopy = _PnlSettingsPanel.chkTeraCopyEnable.Checked
        _AddonSettings.TeraCopyPath = _PnlSettingsPanel.txtTeraCopyPath.Text
        _ESettings.ModuleSettings.Clear()
        For Each e As ListViewItem In _PnlSettingsPanel.lvPaths.Items
            If Not String.IsNullOrEmpty(e.SubItems(0).Text) AndAlso Not String.IsNullOrEmpty(e.SubItems(1).Text) AndAlso e.SubItems(2).Text = "Movie" Then
                _ESettings.ModuleSettings.Add(New SettingItem With {.Name = e.SubItems(0).Text, .FolderPath = e.SubItems(1).Text, .Type = Enums.ContentType.Movie})
            End If
        Next
        For Each e As ListViewItem In _PnlSettingsPanel.lvPaths.Items
            If Not String.IsNullOrEmpty(e.SubItems(0).Text) AndAlso Not String.IsNullOrEmpty(e.SubItems(1).Text) AndAlso e.SubItems(2).Text = "TVShow" Then
                _ESettings.ModuleSettings.Add(New SettingItem With {.Name = e.SubItems(0).Text, .FolderPath = e.SubItems(1).Text, .Type = Enums.ContentType.TVShow})
            End If
        Next
        Settings_Save()
        PopulateFolders(_CmnuMediaMove_Movies, Enums.ContentType.Movie)
        PopulateFolders(_CmnuMediaMove_Shows, Enums.ContentType.TVShow)
        PopulateFolders(_CmnuMediaCopy_Movies, Enums.ContentType.Movie)
        PopulateFolders(_CmnuMediaCopy_Shows, Enums.ContentType.TVShow)
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Private Sub bwCopyDirectory_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwCopyDirectory.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        If Not Args.Source = Args.Destination Then
            _WithErrors = False
            DirectoryCopyMove(Args.Source, Args.Destination, Args.DoMove)
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
            bwCopyDirectory.RunWorkerAsync(New Arguments With {.Source = src, .Destination = dst, .DoMove = False})
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
            bwCopyDirectory.RunWorkerAsync(New Arguments With {.Source = src, .Destination = dst, .DoMove = True})
            While bwCopyDirectory.IsBusy
                Application.DoEvents()
                Threading.Thread.Sleep(50)
            End While
        End Using
    End Sub

    Public Sub Settings_Load()
        _ESettings.ModuleSettings.Clear()
        Dim eMovies As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("MoviePaths")
        If eMovies IsNot Nothing Then
            For Each sett In eMovies
                _ESettings.ModuleSettings.Add(New SettingItem With {.Name = sett.Name, .FolderPath = sett.Value, .Type = Enums.ContentType.Movie})
            Next
        End If
        Dim eShows As List(Of AdvancedSettingsComplexSettingsTableItem) = AdvancedSettings.GetComplexSetting("ShowPaths")
        If eShows IsNot Nothing Then
            For Each sett In eShows
                _ESettings.ModuleSettings.Add(New SettingItem With {.Name = sett.Name, .FolderPath = sett.Value, .Type = Enums.ContentType.TVShow})
            Next
        End If
        _AddonSettings.TeraCopy = AdvancedSettings.GetBooleanSetting("TeraCopy", False)
        _AddonSettings.TeraCopyPath = AdvancedSettings.GetSetting("TeraCopyPath", String.Empty)
    End Sub

    Public Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("TeraCopy", _AddonSettings.TeraCopy)
            settings.SetSetting("TeraCopyPath", _AddonSettings.TeraCopyPath)

            Dim eMovies As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each e As SettingItem In _ESettings.ModuleSettings.Where(Function(f) f.Type = Enums.ContentType.Movie)
                eMovies.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = e.Name, .Value = e.FolderPath})
            Next
            If eMovies IsNot Nothing Then
                settings.SetComplexSetting("MoviePaths", eMovies)
            End If

            Dim eShows As New List(Of AdvancedSettingsComplexSettingsTableItem)
            For Each e As SettingItem In _ESettings.ModuleSettings.Where(Function(f) f.Type = Enums.ContentType.TVShow)
                eShows.Add(New AdvancedSettingsComplexSettingsTableItem With {.Name = e.Name, .Value = e.FolderPath})
            Next
            If eShows IsNot Nothing Then
                settings.SetComplexSetting("ShowPaths", eShows)
            End If
        End Using
    End Sub

    Sub ToolStripMenu_Disable()
        RemoveToolsStripItem_Movies(_CmnuMedia_Movies)
        RemoveToolsStripItem_Movies(_CmnuSep_Movies)
        RemoveToolsStripItem_Shows(_CmnuMedia_Shows)
        RemoveToolsStripItem_Shows(_CmnuSep_Shows)
    End Sub

    Sub ToolsStripMenu_Enable()
        'cmnuMovies
        _CmnuMedia_Movies.Text = Master.eLang.GetString(311, "File Manager")
        _CmnuMediaMove_Movies.Text = Master.eLang.GetString(312, "Move To")
        _CmnuMediaMove_Movies.Tag = "MOVE"
        _CmnuMediaCopy_Movies.Text = Master.eLang.GetString(313, "Copy To")
        _CmnuMediaCopy_Movies.Tag = "COPY"
        _CmnuMedia_Movies.DropDownItems.Add(_CmnuMediaMove_Movies)
        _CmnuMedia_Movies.DropDownItems.Add(_CmnuMediaCopy_Movies)

        SetToolsStripItem_Movies(_CmnuSep_Movies)
        SetToolsStripItem_Movies(_CmnuMedia_Movies)

        'cmnuShows
        _CmnuMedia_Shows.Text = Master.eLang.GetString(311, "File Manager")
        _CmnuMediaMove_Shows.Text = Master.eLang.GetString(312, "Move To")
        _CmnuMediaMove_Shows.Tag = "MOVE"
        _CmnuMediaCopy_Shows.Text = Master.eLang.GetString(313, "Copy To")
        _CmnuMediaCopy_Shows.Tag = "COPY"
        _CmnuMedia_Shows.DropDownItems.Add(_CmnuMediaMove_Shows)
        _CmnuMedia_Shows.DropDownItems.Add(_CmnuMediaCopy_Shows)

        SetToolsStripItem_Shows(_CmnuSep_Shows)
        SetToolsStripItem_Shows(_CmnuMedia_Shows)

        PopulateFolders(_CmnuMediaMove_Movies, Enums.ContentType.Movie)
        PopulateFolders(_CmnuMediaMove_Shows, Enums.ContentType.TVShow)
        PopulateFolders(_CmnuMediaCopy_Movies, Enums.ContentType.Movie)
        PopulateFolders(_CmnuMediaCopy_Shows, Enums.ContentType.TVShow)
        SetToolsStripItemVisibility(_CmnuMedia_Movies, True)
        SetToolsStripItemVisibility(_CmnuMedia_Shows, True)
        SetToolsStripItemVisibility(_CmnuSep_Movies, True)
        SetToolsStripItemVisibility(_CmnuSep_Shows, True)
    End Sub

    Public Sub RemoveToolsStripItem_Movies(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Movies), New Object() {value})
            Exit Sub
        End If
        AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Remove(value)
    End Sub

    Public Sub RemoveToolsStripItem_Shows(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Shows), New Object() {value})
            Exit Sub
        End If
        AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Remove(value)
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
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Movies), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_Shows(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Shows), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Add(value)
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

            If _AddonSettings.TeraCopy AndAlso (String.IsNullOrEmpty(_AddonSettings.TeraCopyPath) OrElse Not File.Exists(_AddonSettings.TeraCopyPath)) Then
                MessageBox.Show(Master.eLang.GetString(398, "TeraCopy.exe not found"), Master.eLang.GetString(1134, "Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Try
            End If

            Dim mTeraCopy As New TeraCopy.Filelist

            If Not String.IsNullOrEmpty(dstPath) Then
                If ContentType = Enums.ContentType.Movie Then
                    For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                        ID = Convert.ToInt64(sRow.Cells("idMovie").Value)
                        If Not MediaToWork.Contains(ID) Then
                            MediaToWork.Add(ID)
                        End If
                    Next
                ElseIf ContentType = Enums.ContentType.TVShow Then
                    For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
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
                                ItemsToWork = FileUtils.Common.GetAllItemsOfDBElement(mMovie)
                                If ItemsToWork.Count = 1 AndAlso Directory.Exists(ItemsToWork(0).ToString) Then
                                    If _AddonSettings.TeraCopy Then
                                        mTeraCopy.Sources.Add(ItemsToWork(0).ToString)
                                    Else
                                        Select Case tMItem.OwnerItem.Tag.ToString
                                            Case "MOVE"
                                                DirectoryMove(ItemsToWork(0).ToString, Path.Combine(dstPath, Path.GetFileName(ItemsToWork(0).ToString)), Master.eLang.GetString(316, "Moving Movie"))
                                                Master.DB.Remove_Movie(movieID, False)
                                            Case "COPY"
                                                DirectoryCopy(ItemsToWork(0).ToString, Path.Combine(dstPath, Path.GetFileName(ItemsToWork(0).ToString)), Master.eLang.GetString(317, "Copying Movie"))
                                        End Select
                                    End If
                                End If
                            Next
                            'If Not _AddonSettings.TeraCopy AndAlso doMove Then AddonsManager.Instance.RuntimeObjects.InvokeLoadMedia(New Scanner.ScanOrCleanOptions With {.Movies = True}) 'TODO
                        ElseIf ContentType = Enums.ContentType.TVShow Then
                            Dim FileDelete As New FileUtils.Delete
                            For Each tShowID As Long In MediaToWork
                                Dim mShow As Database.DBElement = Master.DB.Load_TVShow(tShowID, False, False)
                                If Directory.Exists(mShow.ShowPath) Then
                                    If _AddonSettings.TeraCopy Then
                                        mTeraCopy.Sources.Add(mShow.ShowPath)
                                    Else
                                        Select Case tMItem.OwnerItem.Tag.ToString
                                            Case "MOVE"
                                                DirectoryMove(mShow.ShowPath, Path.Combine(dstPath, Path.GetFileName(mShow.ShowPath)), Master.eLang.GetString(899, "Moving TV Show"))
                                                Master.DB.Remove_TVShow(tShowID, False)
                                            Case "COPY"
                                                DirectoryCopy(mShow.ShowPath, Path.Combine(dstPath, Path.GetFileName(mShow.ShowPath)), Master.eLang.GetString(900, "Copying TV Show"))
                                        End Select
                                    End If
                                End If
                            Next
                            'If Not _AddonSettings.TeraCopy AndAlso doMove Then AddonsManager.Instance.RuntimeObjects.InvokeLoadMedia(New Scanner.ScanOrCleanOptions With {.TV = True}) 'TODO
                        End If
                        If _AddonSettings.TeraCopy Then mTeraCopy.RunTeraCopy(_AddonSettings.TeraCopyPath, dstPath, doMove)
                    End If
                End If
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Sub PopulateFolders(ByVal mnu As ToolStripMenuItem, ByVal ContentType As Enums.ContentType)
        mnu.DropDownItems.Clear()
        _CmnuMediaCustomList.RemoveAll(Function(b) True)

        Dim FolderSubMenuItemCustom As New ToolStripMenuItem
        FolderSubMenuItemCustom.Text = String.Concat(Master.eLang.GetString(338, "Select path"), "...")
        FolderSubMenuItemCustom.Tag = New SettingItem With {.Name = "CUSTOM", .FolderPath = "CUSTOM", .Type = ContentType}
        mnu.DropDownItems.Add(FolderSubMenuItemCustom)
        AddHandler FolderSubMenuItemCustom.Click, AddressOf FolderSubMenuItem_Click

        If _ESettings.ModuleSettings.Where(Function(f) f.Type = ContentType).Count > 0 Then
            Dim SubMenuSep As New System.Windows.Forms.ToolStripSeparator
            mnu.DropDownItems.Add(SubMenuSep)
        End If

        For Each e In _ESettings.ModuleSettings.Where(Function(f) f.Type = ContentType)
            Dim FolderSubMenuItem As New ToolStripMenuItem
            FolderSubMenuItem.Text = e.Name
            FolderSubMenuItem.Tag = New SettingItem With {.Name = e.Name, .FolderPath = e.FolderPath, .Type = ContentType}
            _CmnuMediaCustomList.Add(FolderSubMenuItem)
            AddHandler FolderSubMenuItem.Click, AddressOf FolderSubMenuItem_Click
        Next

        For Each i In _CmnuMediaCustomList
            mnu.DropDownItems.Add(i)
        Next

        SetToolsStripItemVisibility(_CmnuSep_Movies, True)
        SetToolsStripItemVisibility(_CmnuSep_Shows, True)
        SetToolsStripItemVisibility(_CmnuMedia_Movies, True)
        SetToolsStripItemVisibility(_CmnuMedia_Shows, True)
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

    Private Structure AddonSettings

#Region "Fields"

        Dim TeraCopy As Boolean
        Dim TeraCopyPath As String

#End Region 'Fields

    End Structure

    Private Structure Arguments

#Region "Fields"

        Dim Destination As String
        Dim DoMove As Boolean
        Dim Source As String

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

#Region "Properties"

        Public Property ModuleSettings() As List(Of SettingItem) = New List(Of SettingItem)

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class