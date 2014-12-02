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

Public Class BulkRenamerModule
    Implements Interfaces.GenericModule

#Region "Delegates"
    Public Delegate Sub Delegate_SetToolsStripItem(value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_DropDownItemsAdd(value As System.Windows.Forms.ToolStripMenuItem, tsi As System.Windows.Forms.ToolStripMenuItem)
#End Region 'Fields

#Region "Fields"

    Private WithEvents MyMenu As New System.Windows.Forms.ToolStripMenuItem
    Private MySettings As New _MySettings
    Private WithEvents MyTrayMenu As New System.Windows.Forms.ToolStripMenuItem
    Private _AssemblyName As String = String.Empty
    Private _enabled As Boolean = False
    Private _Name As String = "Renamer"
    Private _setup As frmSettingsHolder
    Private ctxMyMenu_Movies As New System.Windows.Forms.ToolStripMenuItem
    Private ctxMyMenu_Episodes As New System.Windows.Forms.ToolStripMenuItem
    Private MyMenuSep_Movies As New System.Windows.Forms.ToolStripSeparator
    Private MyMenuSep_Episodes As New System.Windows.Forms.ToolStripSeparator
    Private WithEvents ctxMySubMenu1 As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ctxMySubMenu2 As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ctxMySubMenu3 As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents ctxMySubMenu4 As New System.Windows.Forms.ToolStripMenuItem
#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.GenericModule.GenericEvent

    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged

    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.RenameAuto_Movie, Enums.ModuleEventType.AfterEdit_Movie, _
                                                                                    Enums.ModuleEventType.RenameManual_Movie, Enums.ModuleEventType.ScraperRDYtoSave_Movie, _
                                                                                   Enums.ModuleEventType.RenameAuto_TVEpisode, Enums.ModuleEventType.AfterEdit_TVEpisode, _
                                                                                    Enums.ModuleEventType.RenameManual_TVEpisode, Enums.ModuleEventType.ScraperMulti_TVEpisode, _
                                                                                   Enums.ModuleEventType.ScraperSingle_TVEpisode})
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

    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _refparam As Object, ByRef _dbmovie As Structures.DBMovie, ByRef _dbtv As Structures.DBTV) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Select Case mType
            Case Enums.ModuleEventType.ScraperRDYtoSave_Movie
                If MySettings.AutoRenameMulti_Movies AndAlso Master.GlobalScrapeMod.NFO AndAlso (Not String.IsNullOrEmpty(MySettings.FoldersPattern_Movies) AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Movies)) Then
                    'Dim tDBMovie As EmberAPI.Structures.DBMovie = DirectCast(_refparam, EmberAPI.Structures.DBMovie)
                    FileFolderRenamer.RenameSingle_Movie(_dbmovie, MySettings.FoldersPattern_Movies, MySettings.FilesPattern_Movies, False, False, False, False)
                End If
            Case Enums.ModuleEventType.RenameAuto_Movie
                If MySettings.AutoRenameSingle_Movies AndAlso Not String.IsNullOrEmpty(MySettings.FoldersPattern_Movies) AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Movies) Then
                    Dim tDBMovie As EmberAPI.Structures.DBMovie = DirectCast(_refparam, EmberAPI.Structures.DBMovie)
                    Dim BatchMode As Boolean = DirectCast(_params(0), Boolean)
                    Dim ToNFO As Boolean = DirectCast(_params(1), Boolean)
                    Dim ShowErrors As Boolean = DirectCast(_params(2), Boolean)
                    FileFolderRenamer.RenameSingle_Movie(tDBMovie, MySettings.FoldersPattern_Movies, MySettings.FilesPattern_Movies, BatchMode, ToNFO, ShowErrors, True)
                End If
            Case Enums.ModuleEventType.RenameManual_Movie
                Using dRenameManual As New dlgRenameManual
                    Select Case dRenameManual.ShowDialog()
                        Case Windows.Forms.DialogResult.OK
                            Return New Interfaces.ModuleResult With {.Cancelled = False, .breakChain = False}
                        Case Else
                            Return New Interfaces.ModuleResult With {.Cancelled = True, .breakChain = False}
                    End Select
                End Using
            Case Enums.ModuleEventType.AfterEdit_TVEpisode
                If MySettings.AutoRenameEdit_Shows AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    Dim tDBTV As EmberAPI.Structures.DBTV = DirectCast(_refparam, EmberAPI.Structures.DBTV)
                    Dim BatchMode As Boolean = DirectCast(_params(0), Boolean)
                    Dim ToNFO As Boolean = DirectCast(_params(1), Boolean)
                    Dim ShowErrors As Boolean = DirectCast(_params(2), Boolean)
                    FileFolderRenamer.RenameSingle_Episode(tDBTV, MySettings.FilesPattern_Episodes, BatchMode, ToNFO, ShowErrors, True)
                End If
            Case Enums.ModuleEventType.ScraperMulti_TVEpisode
                If MySettings.AutoRenameMulti_Shows AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    'Dim tDBTV As EmberAPI.Structures.DBTV = DirectCast(_refparam, EmberAPI.Structures.DBTV)
                    FileFolderRenamer.RenameSingle_Episode(_dbtv, MySettings.FilesPattern_Episodes, False, False, False, False)
                End If
            Case Enums.ModuleEventType.ScraperSingle_TVEpisode
                If MySettings.AutoRenameSingle_Shows AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    'Dim tDBTV As EmberAPI.Structures.DBTV = DirectCast(_refparam, EmberAPI.Structures.DBTV)
                    FileFolderRenamer.RenameSingle_Episode(_dbtv, MySettings.FilesPattern_Episodes, False, False, False, True)
                End If
        End Select
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Private Sub FolderSubMenuItemAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ctxMySubMenu1.Click
        Cursor.Current = Cursors.WaitCursor
        Dim indX As Integer = ModulesManager.Instance.RuntimeObjects.MediaList.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(ModulesManager.Instance.RuntimeObjects.MediaList.Item(0, indX).Value)
        FileFolderRenamer.RenameSingle_Movie(Master.currMovie, MySettings.FoldersPattern_Movies, MySettings.FilesPattern_Movies, True, True, True, True)
        RaiseEvent GenericEvent(Enums.ModuleEventType.RenameAuto_Movie, New List(Of Object)(New Object() {ID, indX}))
        Cursor.Current = Cursors.Default
    End Sub
    Private Sub FolderSubMenuItemManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ctxMySubMenu2.Click
        Dim indX As Integer = ModulesManager.Instance.RuntimeObjects.MediaList.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(ModulesManager.Instance.RuntimeObjects.MediaList.Item(0, indX).Value)
        Using dRenameManual As New dlgRenameManual
            Select Case dRenameManual.ShowDialog()
                Case Windows.Forms.DialogResult.OK
                    RaiseEvent GenericEvent(Enums.ModuleEventType.RenameAuto_Movie, New List(Of Object)(New Object() {ID, indX}))
            End Select
        End Using
    End Sub

    Private Sub FolderSubMenuItemAutoEpisodes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ctxMySubMenu3.Click
        Cursor.Current = Cursors.WaitCursor
        Dim indX As Integer = ModulesManager.Instance.RuntimeObjects.MediaListEpisodes.SelectedRows(0).Index
        Dim ID As Integer = Convert.ToInt32(ModulesManager.Instance.RuntimeObjects.MediaListEpisodes.Item(0, indX).Value)
        FileFolderRenamer.RenameSingle_Episode(Master.currShow, MySettings.FilesPattern_Episodes, True, True, True, True)
        RaiseEvent GenericEvent(Enums.ModuleEventType.RenameAuto_TVEpisode, New List(Of Object)(New Object() {ID, indX}))
        Cursor.Current = Cursors.Default
    End Sub

    Sub Disable()
        Dim tsi As New ToolStripMenuItem
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(MyMenu)
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(MyTrayMenu)
        RemoveToolsStripItem_Movies(MyMenuSep_Movies)
        RemoveToolsStripItem_Movies(ctxMyMenu_Movies)
        'Episodes
        RemoveToolsStripItem_Movies(MyMenuSep_Movies)
        RemoveToolsStripItem_Movies(ctxMyMenu_Episodes)
    End Sub

    Public Sub RemoveToolsStripItem_Episodes(value As System.Windows.Forms.ToolStripItem)
        If (ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.InvokeRequired) Then
            ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Episodes), New Object() {value})
            Exit Sub
        End If
        ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.Items.Remove(value)
    End Sub

    Public Sub RemoveToolsStripItem_Movies(value As System.Windows.Forms.ToolStripItem)
        If (ModulesManager.Instance.RuntimeObjects.MenuMovieList.InvokeRequired) Then
            ModulesManager.Instance.RuntimeObjects.MenuMovieList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Movies), New Object() {value})
            Exit Sub
        End If
        ModulesManager.Instance.RuntimeObjects.MenuMovieList.Items.Remove(value)
    End Sub

    Sub Enable()
        Dim tsi As New ToolStripMenuItem
        MyMenu.Image = New Bitmap(My.Resources.icon)
        MyMenu.Text = Master.eLang.GetString(291, "Bulk &Renamer")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        MyMenu.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True}
        DropDownItemsAdd(MyMenu, tsi)
        MyTrayMenu.Image = New Bitmap(My.Resources.icon)
        MyTrayMenu.Text = Master.eLang.GetString(291, "Bulk &Renamer")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Add(MyTrayMenu)

        ctxMyMenu_Movies.Image = New Bitmap(My.Resources.icon)
        ctxMyMenu_Movies.Text = Master.eLang.GetString(257, "Rename")
        ctxMyMenu_Movies.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        ctxMySubMenu1.Text = Master.eLang.GetString(293, "Auto")
        ctxMySubMenu2.Text = Master.eLang.GetString(294, "Manual")
        ctxMyMenu_Movies.DropDownItems.Add(ctxMySubMenu1)
        ctxMyMenu_Movies.DropDownItems.Add(ctxMySubMenu2)

        SetToolsStripItem_Movies(MyMenuSep_Movies)
        SetToolsStripItem_Movies(ctxMyMenu_Movies)

        'Episodes
        ctxMyMenu_Episodes.Image = New Bitmap(My.Resources.icon)
        ctxMyMenu_Episodes.Text = Master.eLang.GetString(257, "Rename")
        ctxMyMenu_Episodes.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        ctxMySubMenu3.Text = Master.eLang.GetString(293, "Auto")
        ctxMySubMenu4.Text = Master.eLang.GetString(294, "Manual")
        ctxMyMenu_Episodes.DropDownItems.Add(ctxMySubMenu3)
        ctxMyMenu_Episodes.DropDownItems.Add(ctxMySubMenu4)

        SetToolsStripItem_Episodes(MyMenuSep_Episodes)
        SetToolsStripItem_Episodes(ctxMyMenu_Episodes)
    End Sub

    Public Sub DropDownItemsAdd(value As System.Windows.Forms.ToolStripMenuItem, tsi As System.Windows.Forms.ToolStripMenuItem)
        If (tsi.Owner.InvokeRequired) Then
            tsi.Owner.Invoke(New Delegate_DropDownItemsAdd(AddressOf DropDownItemsAdd), New Object() {value, tsi})
            Exit Sub
        End If
        tsi.DropDownItems.Add(MyMenu)
    End Sub

    Public Sub SetToolsStripItem_Episodes(value As System.Windows.Forms.ToolStripItem)
        If (ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.InvokeRequired) Then
            ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Episodes), New Object() {value})
            Exit Sub
        End If
        ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.Items.Add(value)
    End Sub

    Public Sub SetToolsStripItem_Movies(value As System.Windows.Forms.ToolStripItem)
        If (ModulesManager.Instance.RuntimeObjects.MenuMovieList.InvokeRequired) Then
            ModulesManager.Instance.RuntimeObjects.MenuMovieList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Movies), New Object() {value})
            Exit Sub
        End If
        ModulesManager.Instance.RuntimeObjects.MenuMovieList.Items.Add(value)
    End Sub
    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupChanged(ByVal state As Boolean, ByVal difforder As Integer)
        RaiseEvent ModuleEnabledChanged(Me._Name, state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        Me._setup = New frmSettingsHolder
        Me._setup.chkEnabled.Checked = Me._enabled
        Me._setup.txtFolderPatternMovies.Text = MySettings.FoldersPattern_Movies
        Me._setup.txtFolderPatternSeasons.Text = MySettings.FoldersPattern_Seasons
        Me._setup.txtFolderPatternShows.Text = MySettings.FoldersPattern_Shows
        Me._setup.txtFilePatternEpisodes.Text = MySettings.FilesPattern_Episodes
        Me._setup.txtFilePatternMovies.Text = MySettings.FilesPattern_Movies
        Me._setup.chkRenameEditMovies.Checked = MySettings.AutoRenameEdit_Movies
        Me._setup.chkRenameEditShows.Checked = MySettings.AutoRenameEdit_Shows
        Me._setup.chkRenameMultiMovies.Checked = MySettings.AutoRenameMulti_Movies
        Me._setup.chkRenameMultiShows.Checked = MySettings.AutoRenameMulti_Shows
        Me._setup.chkRenameSingleMovies.Checked = MySettings.AutoRenameSingle_Movies
        Me._setup.chkRenameSingleShows.Checked = MySettings.AutoRenameSingle_Shows
        Me._setup.chkGenericModule.Checked = MySettings.GenericModule
        Me._setup.chkBulkRenamer.Checked = MySettings.BulkRenamer
        SPanel.Name = Me._Name
        SPanel.Text = Master.eLang.GetString(295, "Renamer")
        SPanel.Prefix = "Renamer_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(Me._enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings()
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_SetupChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Sub LoadSettings()
        MySettings.FoldersPattern_Movies = clsAdvancedSettings.GetSetting("FoldersPattern", "$T {($Y)}", , Enums.Content_Type.Movie)
        MySettings.FoldersPattern_Seasons = clsAdvancedSettings.GetSetting("FoldersPattern", "$T {($Y)}", , Enums.Content_Type.Season)
        MySettings.FoldersPattern_Shows = clsAdvancedSettings.GetSetting("FoldersPattern", "$T {($Y)}", , Enums.Content_Type.Show)
        MySettings.FilesPattern_Episodes = clsAdvancedSettings.GetSetting("FilesPattern", "$Z - S$WE$Q - $T", , Enums.Content_Type.Episode)
        MySettings.FilesPattern_Movies = clsAdvancedSettings.GetSetting("FilesPattern", "$T{.$S}", , Enums.Content_Type.Movie)
        MySettings.AutoRenameEdit_Movies = clsAdvancedSettings.GetBooleanSetting("AutoRenameEdit", False, , Enums.Content_Type.Movie)
        MySettings.AutoRenameEdit_Shows = clsAdvancedSettings.GetBooleanSetting("AutoRenameEdit", False, , Enums.Content_Type.Show)
        MySettings.AutoRenameMulti_Movies = clsAdvancedSettings.GetBooleanSetting("AutoRenameMulti", False, , Enums.Content_Type.Movie)
        MySettings.AutoRenameMulti_Shows = clsAdvancedSettings.GetBooleanSetting("AutoRenameMulti", False, , Enums.Content_Type.Show)
        MySettings.AutoRenameSingle_Movies = clsAdvancedSettings.GetBooleanSetting("AutoRenameSingle", False, , Enums.Content_Type.Movie)
        MySettings.AutoRenameSingle_Shows = clsAdvancedSettings.GetBooleanSetting("AutoRenameSingle", False, , Enums.Content_Type.Show)
        MySettings.BulkRenamer = clsAdvancedSettings.GetBooleanSetting("BulkRenamer", True)
        MySettings.GenericModule = clsAdvancedSettings.GetBooleanSetting("GenericModule", True)
    End Sub

    Private Sub MyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyMenu.Click, MyTrayMenu.Click

        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Select Case ModulesManager.Instance.RuntimeObjects.MediaTabSelected
            Case 0
                Using dBulkRename As New dlgBulkRenamer_Movie
                    dBulkRename.FilterMovies = ModulesManager.Instance.RuntimeObjects.FilterMovies
                    dBulkRename.FilterMoviesSearch = ModulesManager.Instance.RuntimeObjects.FilterMoviesSearch
                    dBulkRename.FilterMoviesType = ModulesManager.Instance.RuntimeObjects.FilterMoviesType
                    dBulkRename.txtFolderPattern.Text = MySettings.FoldersPattern_Movies
                    dBulkRename.txtFilePattern.Text = MySettings.FilesPattern_Movies
                    Try
                        If dBulkRename.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            ModulesManager.Instance.RuntimeObjects.InvokeLoadMedia(New Structures.Scans With {.Movies = True}, String.Empty)
                        End If
                    Catch ex As Exception
                    End Try
                End Using
            Case 1
                MsgBox("Not implemented yet", MsgBoxStyle.OkOnly, "Info")
                'Using dTVBulkRename As New dlgtvBulkRenamer
                'If dTVBulkRename.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'End If
                'End Using
        End Select
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
    End Sub

    Sub SaveEmberExternalModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Me._enabled = Me._setup.chkEnabled.Checked
        MySettings.FoldersPattern_Movies = Me._setup.txtFolderPatternMovies.Text
        MySettings.FoldersPattern_Seasons = Me._setup.txtFolderPatternSeasons.Text
        MySettings.FoldersPattern_Shows = Me._setup.txtFolderPatternShows.Text
        MySettings.FilesPattern_Episodes = Me._setup.txtFilePatternEpisodes.Text
        MySettings.FilesPattern_Movies = Me._setup.txtFilePatternMovies.Text
        MySettings.AutoRenameEdit_Movies = Me._setup.chkRenameEditMovies.Checked
        MySettings.AutoRenameEdit_Shows = Me._setup.chkRenameEditShows.Checked
        MySettings.AutoRenameMulti_Movies = Me._setup.chkRenameMultiMovies.Checked
        MySettings.AutoRenameMulti_Shows = Me._setup.chkRenameMultiShows.Checked
        MySettings.AutoRenameSingle_Movies = Me._setup.chkRenameSingleMovies.Checked
        MySettings.AutoRenameSingle_Shows = Me._setup.chkRenameSingleShows.Checked
        MySettings.GenericModule = Me._setup.chkGenericModule.Checked
        MySettings.BulkRenamer = Me._setup.chkBulkRenamer.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_SetupChanged
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetSetting("FoldersPattern", MySettings.FoldersPattern_Movies, , , Enums.Content_Type.Movie)
            settings.SetSetting("FoldersPattern", MySettings.FoldersPattern_Seasons, , , Enums.Content_Type.Season)
            settings.SetSetting("FoldersPattern", MySettings.FoldersPattern_Shows, , , Enums.Content_Type.Show)
            settings.SetSetting("FilesPattern", MySettings.FilesPattern_Episodes, , , Enums.Content_Type.Episode)
            settings.SetSetting("FilesPattern", MySettings.FilesPattern_Movies, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("AutoRenameEdit", MySettings.AutoRenameEdit_Movies, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("AutoRenameEdit", MySettings.AutoRenameEdit_Shows, , , Enums.Content_Type.Show)
            settings.SetBooleanSetting("AutoRenameMulti", MySettings.AutoRenameMulti_Movies, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("AutoRenameMulti", MySettings.AutoRenameMulti_Shows, , , Enums.Content_Type.Show)
            settings.SetBooleanSetting("AutoRenameSingle", MySettings.AutoRenameSingle_Movies, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("AutoRenameSingle", MySettings.AutoRenameSingle_Shows, , , Enums.Content_Type.Show)
            settings.SetBooleanSetting("BulkRenamer", MySettings.BulkRenamer)
            settings.SetBooleanSetting("GenericModule", MySettings.GenericModule)
        End Using
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure _MySettings

#Region "Fields"

        Dim AutoRenameEdit_Movies As Boolean
        Dim AutoRenameEdit_Shows As Boolean
        Dim AutoRenameMulti_Movies As Boolean
        Dim AutoRenameMulti_Shows As Boolean
        Dim AutoRenameSingle_Movies As Boolean
        Dim AutoRenameSingle_Shows As Boolean
        Dim BulkRenamer As Boolean
        Dim FilesPattern_Episodes As String
        Dim FilesPattern_Movies As String
        Dim FoldersPattern_Movies As String
        Dim FoldersPattern_Seasons As String
        Dim FoldersPattern_Shows As String
        Dim GenericModule As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class