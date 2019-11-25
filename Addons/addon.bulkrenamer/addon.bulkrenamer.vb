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

Public Class Generic
    Implements Interfaces.IGenericAddon

#Region "Fields"

    Private _AddonSettings As New AddonSettings
    Private _CmnuRenamer_Movies As New ToolStripMenuItem
    Private _CmnuRenamer_Episodes As New ToolStripMenuItem
    Private _CmnuRenamer_Shows As New ToolStripMenuItem
    Private _CmnuSep_Movies As New ToolStripSeparator
    Private _CmnuSep_Episodes As New ToolStripSeparator
    Private _CmnuSep_Shows As New ToolStripSeparator
    Private WithEvents _CmnuRenamerAuto_Movie As New ToolStripMenuItem
    Private WithEvents _CmnuRenamerManual_Movie As New ToolStripMenuItem
    Private WithEvents _CmnuRenamerAuto_TVEpisode As New ToolStripMenuItem
    Private WithEvents _CmnuRenamerManual_TVEpisode As New ToolStripMenuItem
    Private WithEvents _CmnuRenamerAuto_TVShow As New ToolStripMenuItem
    Private WithEvents _CmnuRenamerManual_TVShows As New ToolStripMenuItem
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
                                                      Enums.ModuleEventType.AfterEdit_Movie,
                                                      Enums.ModuleEventType.ScraperMulti_Movie,
                                                      Enums.ModuleEventType.ScraperSingle_Movie,
                                                      Enums.ModuleEventType.AfterEdit_TVEpisode,
                                                      Enums.ModuleEventType.ScraperMulti_TVEpisode,
                                                      Enums.ModuleEventType.ScraperSingle_TVEpisode,
                                                      Enums.ModuleEventType.AfterEdit_TVShow,
                                                      Enums.ModuleEventType.ScraperMulti_TVShow,
                                                      Enums.ModuleEventType.ScraperSingle_TVShow,
                                                      Enums.ModuleEventType.DuringUpdateDB_TV
                                                      })
        End Get
    End Property

#End Region 'Properties

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolsStripItem(Value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(Value As ToolStripItem)
    Public Delegate Sub Delegate_AddToolsStripItem(ToolStripMenuItem As ToolStripMenuItem, Value As ToolStripMenuItem)

#End Region 'Delegates

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef Parameters As List(Of Object)) Implements Interfaces.IGenericAddon.GenericEvent
    Public Event NeedsRestart() Implements Interfaces.IGenericAddon.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IGenericAddon.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IGenericAddon.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

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
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.txtFolderPatternMovies.Text = _AddonSettings.FoldersPattern_Movies
        _PnlSettingsPanel.txtFolderPatternSeasons.Text = _AddonSettings.FoldersPattern_Seasons
        _PnlSettingsPanel.txtFolderPatternShows.Text = _AddonSettings.FoldersPattern_Shows
        _PnlSettingsPanel.txtFilePatternEpisodes.Text = _AddonSettings.FilesPattern_Episodes
        _PnlSettingsPanel.txtFilePatternMovies.Text = _AddonSettings.FilesPattern_Movies
        _PnlSettingsPanel.chkRenameEditMovies.Checked = _AddonSettings.RenameEdit_Movies
        _PnlSettingsPanel.chkRenameEditEpisodes.Checked = _AddonSettings.RenameEdit_Episodes
        _PnlSettingsPanel.chkRenameMultiMovies.Checked = _AddonSettings.RenameMulti_Movies
        _PnlSettingsPanel.chkRenameMultiShows.Checked = _AddonSettings.RenameMulti_Shows
        _PnlSettingsPanel.chkRenameSingleMovies.Checked = _AddonSettings.RenameSingle_Movies
        _PnlSettingsPanel.chkRenameSingleShows.Checked = _AddonSettings.RenameSingle_Shows
        _PnlSettingsPanel.chkRenameUpdateEpisodes.Checked = _AddonSettings.RenameUpdate_Episodes
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = Master.eLang.GetString(295, "Bulk Renamer"),
            .Type = Enums.SettingsPanelType.Addon
        }
    End Sub

    Public Function Run(ByVal ModuleEventType As Enums.ModuleEventType, ByRef Parameters As List(Of Object), ByRef SingleObjekt As Object, ByRef DBElement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericAddon.Run
        Select Case ModuleEventType
            Case Enums.ModuleEventType.AfterEdit_Movie
                If _AddonSettings.RenameEdit_Movies AndAlso Not String.IsNullOrEmpty(_AddonSettings.FoldersPattern_Movies) AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Movies) Then
                    Renamer.RenameSingle_Movie(DBElement, _AddonSettings.FoldersPattern_Movies, _AddonSettings.FilesPattern_Movies, False, False, False)
                End If
            Case Enums.ModuleEventType.AfterEdit_TVEpisode
                If _AddonSettings.RenameEdit_Episodes AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVEpisode(DBElement, _AddonSettings.FoldersPattern_Seasons, _AddonSettings.FilesPattern_Episodes, False, False, False)
                End If
            Case Enums.ModuleEventType.DuringUpdateDB_TV
                If DBElement.NfoPathSpecified AndAlso _AddonSettings.RenameUpdate_Episodes AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVEpisode(DBElement, _AddonSettings.FoldersPattern_Seasons, _AddonSettings.FilesPattern_Episodes, True, False, False)
                End If
            Case Enums.ModuleEventType.ScraperMulti_Movie
                If _AddonSettings.RenameMulti_Movies AndAlso Not String.IsNullOrEmpty(_AddonSettings.FoldersPattern_Movies) AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Movies) Then
                    Renamer.RenameSingle_Movie(DBElement, _AddonSettings.FoldersPattern_Movies, _AddonSettings.FilesPattern_Movies, False, False, False)
                End If
            Case Enums.ModuleEventType.ScraperMulti_TVEpisode
                If _AddonSettings.RenameMulti_Shows AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVEpisode(DBElement, _AddonSettings.FoldersPattern_Seasons, _AddonSettings.FilesPattern_Episodes, False, False, False)
                End If
            Case Enums.ModuleEventType.ScraperMulti_TVShow
                If _AddonSettings.RenameMulti_Shows AndAlso Not String.IsNullOrEmpty(_AddonSettings.FoldersPattern_Shows) AndAlso Not String.IsNullOrEmpty(_AddonSettings.FoldersPattern_Seasons) AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVShow(DBElement, _AddonSettings.FoldersPattern_Shows, _AddonSettings.FoldersPattern_Seasons, _AddonSettings.FilesPattern_Episodes, False, False, False)
                End If
            Case Enums.ModuleEventType.ScraperSingle_Movie
                If _AddonSettings.RenameSingle_Movies AndAlso Not String.IsNullOrEmpty(_AddonSettings.FoldersPattern_Movies) AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Movies) Then
                    Renamer.RenameSingle_Movie(DBElement, _AddonSettings.FoldersPattern_Movies, _AddonSettings.FilesPattern_Movies, False, False, False)
                End If
            Case Enums.ModuleEventType.ScraperSingle_TVEpisode
                If _AddonSettings.RenameSingle_Shows AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVEpisode(DBElement, _AddonSettings.FoldersPattern_Seasons, _AddonSettings.FilesPattern_Episodes, False, False, False)
                End If
            Case Enums.ModuleEventType.ScraperSingle_TVShow
                If _AddonSettings.RenameSingle_Shows AndAlso Not String.IsNullOrEmpty(_AddonSettings.FoldersPattern_Shows) AndAlso Not String.IsNullOrEmpty(_AddonSettings.FoldersPattern_Seasons) AndAlso Not String.IsNullOrEmpty(_AddonSettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVShow(DBElement, _AddonSettings.FoldersPattern_Shows, _AddonSettings.FoldersPattern_Seasons, _AddonSettings.FilesPattern_Episodes, False, False, False)
                End If
        End Select
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericAddon.SaveSetup
        IsEnabled = _PnlSettingsPanel.chkEnabled.Checked
        _AddonSettings.FoldersPattern_Movies = _PnlSettingsPanel.txtFolderPatternMovies.Text
        _AddonSettings.FoldersPattern_Seasons = _PnlSettingsPanel.txtFolderPatternSeasons.Text
        _AddonSettings.FoldersPattern_Shows = _PnlSettingsPanel.txtFolderPatternShows.Text
        _AddonSettings.FilesPattern_Episodes = _PnlSettingsPanel.txtFilePatternEpisodes.Text
        _AddonSettings.FilesPattern_Movies = _PnlSettingsPanel.txtFilePatternMovies.Text
        _AddonSettings.RenameEdit_Movies = _PnlSettingsPanel.chkRenameEditMovies.Checked
        _AddonSettings.RenameEdit_Episodes = _PnlSettingsPanel.chkRenameEditEpisodes.Checked
        _AddonSettings.RenameMulti_Movies = _PnlSettingsPanel.chkRenameMultiMovies.Checked
        _AddonSettings.RenameMulti_Shows = _PnlSettingsPanel.chkRenameMultiShows.Checked
        _AddonSettings.RenameSingle_Movies = _PnlSettingsPanel.chkRenameSingleMovies.Checked
        _AddonSettings.RenameSingle_Shows = _PnlSettingsPanel.chkRenameSingleShows.Checked
        _AddonSettings.RenameUpdate_Episodes = _PnlSettingsPanel.chkRenameUpdateEpisodes.Checked
        Settings_Save()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Private Sub cmnuRenamerAuto_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _CmnuRenamerAuto_Movie.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                Renamer.RenameSingle_Movie(DBElement, _AddonSettings.FoldersPattern_Movies, _AddonSettings.FilesPattern_Movies, False, True, True)
                RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idMovie").Value)}))
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerAuto_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _CmnuRenamerAuto_TVEpisode.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value), True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                Renamer.RenameSingle_TVEpisode(DBElement, _AddonSettings.FoldersPattern_Seasons, _AddonSettings.FilesPattern_Episodes, False, True, True)
                RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idEpisode").Value)})) 'TODO: should be idFile (MultiEpisode handling)
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerAuto_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _CmnuRenamerAuto_TVShow.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, True, True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                Renamer.RenameSingle_TVShow(DBElement, _AddonSettings.FoldersPattern_Shows, _AddonSettings.FoldersPattern_Seasons, _AddonSettings.FilesPattern_Episodes, False, True, True)
                RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idShow").Value)}))
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerManual_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _CmnuRenamerManual_Movie.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                Using dRenameManual As New dlgRenameManual_Movie(DBElement)
                    Select Case dRenameManual.ShowDialog()
                        Case DialogResult.OK
                            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idMovie").Value)}))
                    End Select
                End Using
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerManual_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _CmnuRenamerManual_TVEpisode.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value), True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                Using dRenameManual As New dlgRenameManual_TVEpisode(DBElement)
                    Select Case dRenameManual.ShowDialog()
                        Case DialogResult.OK
                            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idEpisode").Value)}))
                    End Select
                End Using
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerManual_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _CmnuRenamerManual_TVShows.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, True, True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                Using dRenameManual As New dlgRenameManual_TVShow(DBElement)
                    Select Case dRenameManual.ShowDialog()
                        Case DialogResult.OK
                            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idShow").Value)}))
                    End Select
                End Using
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Public Sub RemoveToolsStripItem_Episodes(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Episodes), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem_Movies(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Movies), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem_Shows(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Shows), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Remove(value)
        End If
    End Sub

    Sub ToolsStripMenu_Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(_MnuMainToolsRenamer)

        'cmnuTrayTools
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(_CmnuTrayToolsRenamer)

        'cmnuMovies
        RemoveToolsStripItem_Movies(_CmnuSep_Movies)
        RemoveToolsStripItem_Movies(_CmnuRenamer_Movies)

        'cmnuEpisodes
        RemoveToolsStripItem_Episodes(_CmnuSep_Episodes)
        RemoveToolsStripItem_Episodes(_CmnuRenamer_Episodes)

        'cmnuShows
        RemoveToolsStripItem_Shows(_CmnuSep_Shows)
        RemoveToolsStripItem_Shows(_CmnuRenamer_Shows)
    End Sub

    Sub ToolsStripMenu_Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        _MnuMainToolsRenamer.Image = New Bitmap(My.Resources.icon)
        _MnuMainToolsRenamer.Text = Master.eLang.GetString(291, "Bulk &Renamer")
        _MnuMainToolsRenamer.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, _MnuMainToolsRenamer)

        'cmnuTrayTools
        _CmnuTrayToolsRenamer.Image = New Bitmap(My.Resources.icon)
        _CmnuTrayToolsRenamer.Text = Master.eLang.GetString(291, "Bulk &Renamer")
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, _CmnuTrayToolsRenamer)

        'cmnuMovies
        _CmnuRenamer_Movies.Image = New Bitmap(My.Resources.icon)
        _CmnuRenamer_Movies.Text = Master.eLang.GetString(257, "Rename")
        _CmnuRenamer_Movies.ShortcutKeys = CType((Keys.Control Or Keys.R), Keys)
        _CmnuRenamerAuto_Movie.Text = Master.eLang.GetString(293, "Auto")
        _CmnuRenamerManual_Movie.Text = Master.eLang.GetString(294, "Manual")
        _CmnuRenamer_Movies.DropDownItems.Add(_CmnuRenamerAuto_Movie)
        _CmnuRenamer_Movies.DropDownItems.Add(_CmnuRenamerManual_Movie)

        SetToolsStripItem_Movies(_CmnuSep_Movies)
        SetToolsStripItem_Movies(_CmnuRenamer_Movies)

        'cmnuEpisodes
        _CmnuRenamer_Episodes.Image = New Bitmap(My.Resources.icon)
        _CmnuRenamer_Episodes.Text = Master.eLang.GetString(257, "Rename")
        _CmnuRenamer_Episodes.ShortcutKeys = CType((Keys.Control Or Keys.R), Keys)
        _CmnuRenamerAuto_TVEpisode.Text = Master.eLang.GetString(293, "Auto")
        _CmnuRenamerManual_TVEpisode.Text = Master.eLang.GetString(294, "Manual")
        _CmnuRenamer_Episodes.DropDownItems.Add(_CmnuRenamerAuto_TVEpisode)
        _CmnuRenamer_Episodes.DropDownItems.Add(_CmnuRenamerManual_TVEpisode)

        SetToolsStripItem_Episodes(_CmnuSep_Episodes)
        SetToolsStripItem_Episodes(_CmnuRenamer_Episodes)

        'cmnuShows
        _CmnuRenamer_Shows.Image = New Bitmap(My.Resources.icon)
        _CmnuRenamer_Shows.Text = Master.eLang.GetString(257, "Rename")
        _CmnuRenamer_Shows.ShortcutKeys = CType((Keys.Control Or Keys.R), Keys)
        _CmnuRenamerAuto_TVShow.Text = Master.eLang.GetString(293, "Auto")
        _CmnuRenamerManual_TVShows.Text = Master.eLang.GetString(294, "Manual")
        _CmnuRenamer_Shows.DropDownItems.Add(_CmnuRenamerAuto_TVShow)
        _CmnuRenamer_Shows.DropDownItems.Add(_CmnuRenamerManual_TVShows)

        SetToolsStripItem_Shows(_CmnuSep_Shows)
        SetToolsStripItem_Shows(_CmnuRenamer_Shows)
    End Sub

    Public Sub AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_Episodes(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Episodes), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Add(value)
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

    Private Sub mnuMainToolsRenamer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles _MnuMainToolsRenamer.Click, _CmnuTrayToolsRenamer.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Select Case AddonsManager.Instance.RuntimeObjects.MediaTabSelected.ContentType
            Case Enums.ContentType.Movie
                Using dBulkRename As New dlgBulkRenamer_Movie
                    dBulkRename._FilterMovies = AddonsManager.Instance.RuntimeObjects.FilterMovies
                    dBulkRename._FilterMoviesSearch = AddonsManager.Instance.RuntimeObjects.FilterMoviesSearch
                    dBulkRename._FilterMoviesType = AddonsManager.Instance.RuntimeObjects.FilterMoviesType
                    dBulkRename._ListMovies = AddonsManager.Instance.RuntimeObjects.ListMovies
                    dBulkRename.txtFilePattern.Text = _AddonSettings.FilesPattern_Movies
                    dBulkRename.txtFolderPattern.Text = _AddonSettings.FoldersPattern_Movies
                    dBulkRename.ShowDialog()
                End Using
            Case Enums.ContentType.TV
                Using dBulkRename As New dlgBulkRenamer_TV
                    dBulkRename._FilterShows = AddonsManager.Instance.RuntimeObjects.FilterTVShows
                    dBulkRename._FilterShowsSearch = AddonsManager.Instance.RuntimeObjects.FilterTVShowsSearch
                    dBulkRename._FilterShowsType = AddonsManager.Instance.RuntimeObjects.FilterTVShowsType
                    dBulkRename._ListShows = AddonsManager.Instance.RuntimeObjects.ListTVShows
                    dBulkRename.txtFilePatternEpisodes.Text = _AddonSettings.FilesPattern_Episodes
                    dBulkRename.txtFolderPatternSeasons.Text = _AddonSettings.FoldersPattern_Seasons
                    dBulkRename.txtFolderPatternShows.Text = _AddonSettings.FoldersPattern_Shows
                    dBulkRename.ShowDialog()
                End Using
        End Select
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Sub Settings_Load()
        _AddonSettings.FoldersPattern_Movies = AdvancedSettings.GetSetting("FoldersPattern", "$T {($Y)}", , Enums.ContentType.Movie)
        _AddonSettings.FoldersPattern_Seasons = AdvancedSettings.GetSetting("FoldersPattern", "Season $K2_?", , Enums.ContentType.TVSeason)
        _AddonSettings.FoldersPattern_Shows = AdvancedSettings.GetSetting("FoldersPattern", "$Z", , Enums.ContentType.TVShow)
        _AddonSettings.FilesPattern_Episodes = AdvancedSettings.GetSetting("FilesPattern", "$Z - $W2_S?2E?{ - $T}", , Enums.ContentType.TVEpisode)
        _AddonSettings.FilesPattern_Movies = AdvancedSettings.GetSetting("FilesPattern", "$T{.$S}", , Enums.ContentType.Movie)
        _AddonSettings.RenameEdit_Movies = AdvancedSettings.GetBooleanSetting("RenameEdit", False, , Enums.ContentType.Movie)
        _AddonSettings.RenameEdit_Episodes = AdvancedSettings.GetBooleanSetting("RenameEdit", False, , Enums.ContentType.TVShow)
        _AddonSettings.RenameMulti_Movies = AdvancedSettings.GetBooleanSetting("RenameMulti", False, , Enums.ContentType.Movie)
        _AddonSettings.RenameMulti_Shows = AdvancedSettings.GetBooleanSetting("RenameMulti", False, , Enums.ContentType.TVShow)
        _AddonSettings.RenameSingle_Movies = AdvancedSettings.GetBooleanSetting("RenameSingle", False, , Enums.ContentType.Movie)
        _AddonSettings.RenameSingle_Shows = AdvancedSettings.GetBooleanSetting("RenameSingle", False, , Enums.ContentType.TVShow)
        _AddonSettings.RenameUpdate_Episodes = AdvancedSettings.GetBooleanSetting("RenameUpdate", False, , Enums.ContentType.TVEpisode)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetSetting("FoldersPattern", _AddonSettings.FoldersPattern_Movies, , , Enums.ContentType.Movie)
            settings.SetSetting("FoldersPattern", _AddonSettings.FoldersPattern_Seasons, , , Enums.ContentType.TVSeason)
            settings.SetSetting("FoldersPattern", _AddonSettings.FoldersPattern_Shows, , , Enums.ContentType.TVShow)
            settings.SetSetting("FilesPattern", _AddonSettings.FilesPattern_Episodes, , , Enums.ContentType.TVEpisode)
            settings.SetSetting("FilesPattern", _AddonSettings.FilesPattern_Movies, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("RenameEdit", _AddonSettings.RenameEdit_Movies, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("RenameEdit", _AddonSettings.RenameEdit_Episodes, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("RenameMulti", _AddonSettings.RenameMulti_Movies, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("RenameMulti", _AddonSettings.RenameMulti_Shows, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("RenameSingle", _AddonSettings.RenameSingle_Movies, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("RenameSingle", _AddonSettings.RenameSingle_Shows, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("RenameUpdate", _AddonSettings.RenameUpdate_Episodes, , , Enums.ContentType.TVEpisode)
        End Using
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AddonSettings

#Region "Fields"

        Dim FilesPattern_Episodes As String
        Dim FilesPattern_Movies As String
        Dim FoldersPattern_Movies As String
        Dim FoldersPattern_Seasons As String
        Dim FoldersPattern_Shows As String
        Dim RenameEdit_Movies As Boolean
        Dim RenameEdit_Episodes As Boolean
        Dim RenameMulti_Movies As Boolean
        Dim RenameMulti_Shows As Boolean
        Dim RenameSingle_Movies As Boolean
        Dim RenameSingle_Shows As Boolean
        Dim RenameUpdate_Episodes As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class