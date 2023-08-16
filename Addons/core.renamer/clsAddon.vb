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

Public Class Addon
    Implements Interfaces.IAddon_Generic

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_AddToolsStripItem(tsi As ToolStripMenuItem, value As ToolStripMenuItem)

#End Region 'Delegates

#Region "Fields"

    Private MySettings As New AddonSettings
    Private _AssemblyName As String = String.Empty
    Private _enabled As Boolean = False
    Private _Name As String = "Renamer"
    Private _setup As frmSettingsHolder
    Private cmnuRenamer_Movies As New ToolStripMenuItem
    Private cmnuRenamer_Episodes As New ToolStripMenuItem
    Private cmnuRenamer_Shows As New ToolStripMenuItem
    Private cmnuSep_Movies As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_Episodes As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_Shows As New System.Windows.Forms.ToolStripSeparator
    Private WithEvents cmnuRenamerAuto_Movie As New ToolStripMenuItem
    Private WithEvents cmnuRenamerManual_Movie As New ToolStripMenuItem
    Private WithEvents cmnuRenamerAuto_TVEpisode As New ToolStripMenuItem
    Private WithEvents cmnuRenamerManual_TVEpisode As New ToolStripMenuItem
    Private WithEvents cmnuRenamerAuto_TVShow As New ToolStripMenuItem
    Private WithEvents cmnuRenamerManual_TVShows As New ToolStripMenuItem
    Private WithEvents cmnuTrayToolsRenamer As New ToolStripMenuItem
    Private WithEvents mnuMainToolsRenamer As New ToolStripMenuItem

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal eventType As Enums.AddonEventType, ByRef parameters As List(Of Object)) Implements Interfaces.IAddon_Generic.GenericEvent
    Public Event AddonSettingsChanged() Implements Interfaces.IAddon_Generic.AddonSettingsChanged
    Public Event AddonStateChanged(ByVal name As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.IAddon_Generic.AddonStateChanged
    Public Event AddonNeedsRestart() Implements Interfaces.IAddon_Generic.AddonNeedsRestart

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property EventType() As List(Of Enums.AddonEventType) Implements Interfaces.IAddon_Generic.EventType
        Get
            Return New List(Of Enums.AddonEventType)(New Enums.AddonEventType() {Enums.AddonEventType.AfterEdit_Movie, Enums.AddonEventType.ScraperMulti_Movie, Enums.AddonEventType.ScraperSingle_Movie,
                                                                                   Enums.AddonEventType.AfterEdit_TVEpisode, Enums.AddonEventType.ScraperMulti_TVEpisode, Enums.AddonEventType.ScraperSingle_TVEpisode,
                                                                                   Enums.AddonEventType.AfterEdit_TVShow, Enums.AddonEventType.ScraperMulti_TVShow, Enums.AddonEventType.ScraperSingle_TVShow,
                                                                                   Enums.AddonEventType.DuringUpdateDB_TV})
        End Get
    End Property

    Property Enabled() As Boolean Implements Interfaces.IAddon_Generic.Enabled
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

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IAddon_Generic.IsBusy
        Get
            Return False
        End Get
    End Property

    ReadOnly Property Name() As String Implements Interfaces.IAddon_Generic.Name
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property Version() As String Implements Interfaces.IAddon_Generic.Version
        Get
            Return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Function RunGeneric(ByVal eventType As Enums.AddonEventType, ByRef parameters As List(Of Object), ByRef singleObject As Object, ByRef dbElement As Database.DBElement) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Generic.RunGeneric
        Select Case eventType
            Case Enums.AddonEventType.AfterEdit_Movie
                If MySettings.RenameEdit_Movies AndAlso Not String.IsNullOrEmpty(MySettings.FoldersPattern_Movies) AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Movies) Then
                    Renamer.RenameSingle_Movie(dbElement, MySettings.FoldersPattern_Movies, MySettings.FilesPattern_Movies, False, False, False)
                End If
            Case Enums.AddonEventType.AfterEdit_TVEpisode
                If MySettings.RenameEdit_Episodes AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVEpisode(dbElement, MySettings.FoldersPattern_Seasons, MySettings.FilesPattern_Episodes, False, False, False)
                End If
            Case Enums.AddonEventType.DuringUpdateDB_TV
                If dbElement.NfoPathSpecified AndAlso MySettings.RenameUpdate_Episodes AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVEpisode(dbElement, MySettings.FoldersPattern_Seasons, MySettings.FilesPattern_Episodes, True, False, False)
                End If
            Case Enums.AddonEventType.ScraperMulti_Movie
                If MySettings.RenameMulti_Movies AndAlso Not String.IsNullOrEmpty(MySettings.FoldersPattern_Movies) AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Movies) Then
                    Renamer.RenameSingle_Movie(dbElement, MySettings.FoldersPattern_Movies, MySettings.FilesPattern_Movies, False, False, False)
                End If
            Case Enums.AddonEventType.ScraperMulti_TVEpisode
                If MySettings.RenameMulti_Shows AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVEpisode(dbElement, MySettings.FoldersPattern_Seasons, MySettings.FilesPattern_Episodes, False, False, False)
                End If
            Case Enums.AddonEventType.ScraperMulti_TVShow
                If MySettings.RenameMulti_Shows AndAlso Not String.IsNullOrEmpty(MySettings.FoldersPattern_Shows) AndAlso Not String.IsNullOrEmpty(MySettings.FoldersPattern_Seasons) AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVShow(dbElement, MySettings.FoldersPattern_Shows, MySettings.FoldersPattern_Seasons, MySettings.FilesPattern_Episodes, False, False, False)
                End If
            Case Enums.AddonEventType.ScraperSingle_Movie
                If MySettings.RenameSingle_Movies AndAlso Not String.IsNullOrEmpty(MySettings.FoldersPattern_Movies) AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Movies) Then
                    Renamer.RenameSingle_Movie(dbElement, MySettings.FoldersPattern_Movies, MySettings.FilesPattern_Movies, False, False, False)
                End If
            Case Enums.AddonEventType.ScraperSingle_TVEpisode
                If MySettings.RenameSingle_Shows AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVEpisode(dbElement, MySettings.FoldersPattern_Seasons, MySettings.FilesPattern_Episodes, False, False, False)
                End If
            Case Enums.AddonEventType.ScraperSingle_TVShow
                If MySettings.RenameSingle_Shows AndAlso Not String.IsNullOrEmpty(MySettings.FoldersPattern_Shows) AndAlso Not String.IsNullOrEmpty(MySettings.FoldersPattern_Seasons) AndAlso Not String.IsNullOrEmpty(MySettings.FilesPattern_Episodes) Then
                    Renamer.RenameSingle_TVShow(dbElement, MySettings.FoldersPattern_Shows, MySettings.FoldersPattern_Seasons, MySettings.FilesPattern_Episodes, False, False, False)
                End If
        End Select
        Return New Interfaces.AddonResult_Generic
    End Function

    Private Sub cmnuRenamerAuto_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuRenamerAuto_Movie.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In Addons.Instance.RuntimeObjects.MediaListMovies.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                Renamer.RenameSingle_Movie(DBElement, MySettings.FoldersPattern_Movies, MySettings.FilesPattern_Movies, False, True, True)
                RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_Movie, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idMovie").Value)}))
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerAuto_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuRenamerAuto_TVEpisode.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In Addons.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value), True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                Renamer.RenameSingle_TVEpisode(DBElement, MySettings.FoldersPattern_Seasons, MySettings.FilesPattern_Episodes, False, True, True)
                RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idEpisode").Value)})) 'TODO: should be idFile (MultiEpisode handling)
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerAuto_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuRenamerAuto_TVShow.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In Addons.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, True, True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                Renamer.RenameSingle_TVShow(DBElement, MySettings.FoldersPattern_Shows, MySettings.FoldersPattern_Seasons, MySettings.FilesPattern_Episodes, False, True, True)
                RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idShow").Value)}))
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerManual_Movie_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuRenamerManual_Movie.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In Addons.Instance.RuntimeObjects.MediaListMovies.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_Movie(Convert.ToInt64(sRow.Cells("idMovie").Value))
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                Using dRenameManual As New dlgRenameManual_Movie(DBElement)
                    Select Case dRenameManual.ShowDialog()
                        Case DialogResult.OK
                            RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_Movie, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idMovie").Value)}))
                    End Select
                End Using
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerManual_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuRenamerManual_TVEpisode.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In Addons.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(Convert.ToInt64(sRow.Cells("idEpisode").Value), True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                Using dRenameManual As New dlgRenameManual_TVEpisode(DBElement)
                    Select Case dRenameManual.ShowDialog()
                        Case DialogResult.OK
                            RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idEpisode").Value)}))
                    End Select
                End Using
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub cmnuRenamerManual_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuRenamerManual_TVShows.Click
        Cursor.Current = Cursors.WaitCursor
        For Each sRow As DataGridViewRow In Addons.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
            Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(Convert.ToInt64(sRow.Cells("idShow").Value), True, True, True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                Using dRenameManual As New dlgRenameManual_TVShow(DBElement)
                    Select Case dRenameManual.ShowDialog()
                        Case DialogResult.OK
                            RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {Convert.ToInt64(sRow.Cells("idShow").Value)}))
                    End Select
                End Using
            End If
        Next
        Cursor.Current = Cursors.Default
    End Sub

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(mnuMainToolsRenamer)

        'cmnuTrayTools
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(cmnuTrayToolsRenamer)

        'cmnuMovies
        RemoveToolsStripItem_Movies(cmnuSep_Movies)
        RemoveToolsStripItem_Movies(cmnuRenamer_Movies)

        'cmnuEpisodes
        RemoveToolsStripItem_Episodes(cmnuSep_Episodes)
        RemoveToolsStripItem_Episodes(cmnuRenamer_Episodes)

        'cmnuShows
        RemoveToolsStripItem_Shows(cmnuSep_Shows)
        RemoveToolsStripItem_Shows(cmnuRenamer_Shows)
    End Sub

    Public Sub RemoveToolsStripItem_Episodes(value As ToolStripItem)
        If Addons.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            Addons.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Episodes), New Object() {value})
        Else
            Addons.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem_Movies(value As ToolStripItem)
        If Addons.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            Addons.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Movies), New Object() {value})
        Else
            Addons.Instance.RuntimeObjects.ContextMenuMovieList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem_Shows(value As ToolStripItem)
        If Addons.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            Addons.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Shows), New Object() {value})
        Else
            Addons.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Remove(value)
        End If
    End Sub

    Sub Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        mnuMainToolsRenamer.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsRenamer.Text = Master.eLang.GetString(291, "Bulk &Renamer")
        mnuMainToolsRenamer.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(Addons.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsRenamer)

        'cmnuTrayTools
        cmnuTrayToolsRenamer.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsRenamer.Text = Master.eLang.GetString(291, "Bulk &Renamer")
        tsi = DirectCast(Addons.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsRenamer)

        'cmnuMovies
        cmnuRenamer_Movies.Image = New Bitmap(My.Resources.icon)
        cmnuRenamer_Movies.Text = Master.eLang.GetString(257, "Rename")
        cmnuRenamer_Movies.ShortcutKeys = CType((Keys.Control Or Keys.R), Keys)
        cmnuRenamerAuto_Movie.Text = Master.eLang.GetString(293, "Auto")
        cmnuRenamerManual_Movie.Text = Master.eLang.GetString(294, "Manual")
        cmnuRenamer_Movies.DropDownItems.Add(cmnuRenamerAuto_Movie)
        cmnuRenamer_Movies.DropDownItems.Add(cmnuRenamerManual_Movie)

        SetToolsStripItem_Movies(cmnuSep_Movies)
        SetToolsStripItem_Movies(cmnuRenamer_Movies)

        'cmnuEpisodes
        cmnuRenamer_Episodes.Image = New Bitmap(My.Resources.icon)
        cmnuRenamer_Episodes.Text = Master.eLang.GetString(257, "Rename")
        cmnuRenamer_Episodes.ShortcutKeys = CType((Keys.Control Or Keys.R), Keys)
        cmnuRenamerAuto_TVEpisode.Text = Master.eLang.GetString(293, "Auto")
        cmnuRenamerManual_TVEpisode.Text = Master.eLang.GetString(294, "Manual")
        cmnuRenamer_Episodes.DropDownItems.Add(cmnuRenamerAuto_TVEpisode)
        cmnuRenamer_Episodes.DropDownItems.Add(cmnuRenamerManual_TVEpisode)

        SetToolsStripItem_Episodes(cmnuSep_Episodes)
        SetToolsStripItem_Episodes(cmnuRenamer_Episodes)

        'cmnuShows
        cmnuRenamer_Shows.Image = New Bitmap(My.Resources.icon)
        cmnuRenamer_Shows.Text = Master.eLang.GetString(257, "Rename")
        cmnuRenamer_Shows.ShortcutKeys = CType((Keys.Control Or Keys.R), Keys)
        cmnuRenamerAuto_TVShow.Text = Master.eLang.GetString(293, "Auto")
        cmnuRenamerManual_TVShows.Text = Master.eLang.GetString(294, "Manual")
        cmnuRenamer_Shows.DropDownItems.Add(cmnuRenamerAuto_TVShow)
        cmnuRenamer_Shows.DropDownItems.Add(cmnuRenamerManual_TVShows)

        SetToolsStripItem_Shows(cmnuSep_Shows)
        SetToolsStripItem_Shows(cmnuRenamer_Shows)
    End Sub

    Public Sub AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_Episodes(value As ToolStripItem)
        If Addons.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            Addons.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Episodes), New Object() {value})
        Else
            Addons.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_Movies(value As ToolStripItem)
        If Addons.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            Addons.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Movies), New Object() {value})
        Else
            Addons.Instance.RuntimeObjects.ContextMenuMovieList.Items.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_Shows(value As ToolStripItem)
        If Addons.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            Addons.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Shows), New Object() {value})
        Else
            Addons.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Add(value)
        End If
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent AddonSettingsChanged()
    End Sub

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent AddonStateChanged(_Name, State, 0)
    End Sub

    Public Sub Init(ByVal assemblyName As String, ByVal executable As String) Implements Interfaces.IAddon_Generic.Init
        _AssemblyName = assemblyName
        LoadSettings()
    End Sub

    Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IAddon_Generic.InjectSettingsPanel
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        _setup.chkEnabled.Checked = _enabled
        _setup.txtFolderPatternMovies.Text = MySettings.FoldersPattern_Movies
        _setup.txtFolderPatternSeasons.Text = MySettings.FoldersPattern_Seasons
        _setup.txtFolderPatternShows.Text = MySettings.FoldersPattern_Shows
        _setup.txtFilePatternEpisodes.Text = MySettings.FilesPattern_Episodes
        _setup.txtFilePatternMovies.Text = MySettings.FilesPattern_Movies
        _setup.chkRenameEditMovies.Checked = MySettings.RenameEdit_Movies
        _setup.chkRenameEditEpisodes.Checked = MySettings.RenameEdit_Episodes
        _setup.chkRenameMultiMovies.Checked = MySettings.RenameMulti_Movies
        _setup.chkRenameMultiShows.Checked = MySettings.RenameMulti_Shows
        _setup.chkRenameSingleMovies.Checked = MySettings.RenameSingle_Movies
        _setup.chkRenameSingleShows.Checked = MySettings.RenameSingle_Shows
        _setup.chkRenameUpdateEpisodes.Checked = MySettings.RenameUpdate_Episodes
        SPanel.UniqueName = _Name
        SPanel.Title = Master.eLang.GetString(295, "Renamer")
        SPanel.Type = Master.eLang.GetString(802, "Addons")
        SPanel.ImageIndex = If(_enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings()
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Sub LoadSettings()
        MySettings.FoldersPattern_Movies = Master.eAdvancedSettings.GetStringSetting("FoldersPattern", "$T {($Y)}", , Enums.ContentType.Movie)
        MySettings.FoldersPattern_Seasons = Master.eAdvancedSettings.GetStringSetting("FoldersPattern", "Season $K2_?", , Enums.ContentType.TVSeason)
        MySettings.FoldersPattern_Shows = Master.eAdvancedSettings.GetStringSetting("FoldersPattern", "$Z", , Enums.ContentType.TVShow)
        MySettings.FilesPattern_Episodes = Master.eAdvancedSettings.GetStringSetting("FilesPattern", "$Z - $W2_S?2E?{ - $T}", , Enums.ContentType.TVEpisode)
        MySettings.FilesPattern_Movies = Master.eAdvancedSettings.GetStringSetting("FilesPattern", "$T{.$S}", , Enums.ContentType.Movie)
        MySettings.RenameEdit_Movies = Master.eAdvancedSettings.GetBooleanSetting("RenameEdit", False, , Enums.ContentType.Movie)
        MySettings.RenameEdit_Episodes = Master.eAdvancedSettings.GetBooleanSetting("RenameEdit", False, , Enums.ContentType.TVShow)
        MySettings.RenameMulti_Movies = Master.eAdvancedSettings.GetBooleanSetting("RenameMulti", False, , Enums.ContentType.Movie)
        MySettings.RenameMulti_Shows = Master.eAdvancedSettings.GetBooleanSetting("RenameMulti", False, , Enums.ContentType.TVShow)
        MySettings.RenameSingle_Movies = Master.eAdvancedSettings.GetBooleanSetting("RenameSingle", False, , Enums.ContentType.Movie)
        MySettings.RenameSingle_Shows = Master.eAdvancedSettings.GetBooleanSetting("RenameSingle", False, , Enums.ContentType.TVShow)
        MySettings.RenameUpdate_Episodes = Master.eAdvancedSettings.GetBooleanSetting("RenameUpdate", False, , Enums.ContentType.TVEpisode)
    End Sub

    Private Sub mnuMainToolsRenamer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuMainToolsRenamer.Click, cmnuTrayToolsRenamer.Click
        RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))
        Select Case Addons.Instance.RuntimeObjects.MediaTabSelected.ContentType
            Case Enums.ContentType.Movie
                Using dBulkRename As New dlgBulkRenamer_Movie
                    dBulkRename.FilterMovies = Addons.Instance.RuntimeObjects.FilterMovies
                    dBulkRename.FilterMoviesSearch = Addons.Instance.RuntimeObjects.FilterMoviesSearch
                    dBulkRename.FilterMoviesType = Addons.Instance.RuntimeObjects.FilterMoviesType
                    dBulkRename.ListMovies = Addons.Instance.RuntimeObjects.ListMovies
                    dBulkRename.txtFilePattern.Text = MySettings.FilesPattern_Movies
                    dBulkRename.txtFolderPattern.Text = MySettings.FoldersPattern_Movies
                    dBulkRename.ShowDialog()
                End Using
            Case Enums.ContentType.TV
                Using dBulkRename As New dlgBulkRenamer_TV
                    dBulkRename.FilterShows = Addons.Instance.RuntimeObjects.FilterTVShows
                    dBulkRename.FilterShowsSearch = Addons.Instance.RuntimeObjects.FilterTVShowsSearch
                    dBulkRename.FilterShowsType = Addons.Instance.RuntimeObjects.FilterTVShowsType
                    dBulkRename.ListShows = Addons.Instance.RuntimeObjects.ListTVShows
                    dBulkRename.txtFilePatternEpisodes.Text = MySettings.FilesPattern_Episodes
                    dBulkRename.txtFolderPatternSeasons.Text = MySettings.FoldersPattern_Seasons
                    dBulkRename.txtFolderPatternShows.Text = MySettings.FoldersPattern_Shows
                    dBulkRename.ShowDialog()
                End Using
        End Select
        RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.AddonEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

    Sub SaveSettings(ByVal doDispose As Boolean) Implements Interfaces.IAddon_Generic.SaveSettings
        Enabled = _setup.chkEnabled.Checked
        MySettings.FoldersPattern_Movies = _setup.txtFolderPatternMovies.Text
        MySettings.FoldersPattern_Seasons = _setup.txtFolderPatternSeasons.Text
        MySettings.FoldersPattern_Shows = _setup.txtFolderPatternShows.Text
        MySettings.FilesPattern_Episodes = _setup.txtFilePatternEpisodes.Text
        MySettings.FilesPattern_Movies = _setup.txtFilePatternMovies.Text
        MySettings.RenameEdit_Movies = _setup.chkRenameEditMovies.Checked
        MySettings.RenameEdit_Episodes = _setup.chkRenameEditEpisodes.Checked
        MySettings.RenameMulti_Movies = _setup.chkRenameMultiMovies.Checked
        MySettings.RenameMulti_Shows = _setup.chkRenameMultiShows.Checked
        MySettings.RenameSingle_Movies = _setup.chkRenameSingleMovies.Checked
        MySettings.RenameSingle_Shows = _setup.chkRenameSingleShows.Checked
        MySettings.RenameUpdate_Episodes = _setup.chkRenameUpdateEpisodes.Checked
        SaveSettings()
        If doDispose Then
            RemoveHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetStringSetting("FoldersPattern", MySettings.FoldersPattern_Movies, , , Enums.ContentType.Movie)
            settings.SetStringSetting("FoldersPattern", MySettings.FoldersPattern_Seasons, , , Enums.ContentType.TVSeason)
            settings.SetStringSetting("FoldersPattern", MySettings.FoldersPattern_Shows, , , Enums.ContentType.TVShow)
            settings.SetStringSetting("FilesPattern", MySettings.FilesPattern_Episodes, , , Enums.ContentType.TVEpisode)
            settings.SetStringSetting("FilesPattern", MySettings.FilesPattern_Movies, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("RenameEdit", MySettings.RenameEdit_Movies, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("RenameEdit", MySettings.RenameEdit_Episodes, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("RenameMulti", MySettings.RenameMulti_Movies, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("RenameMulti", MySettings.RenameMulti_Shows, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("RenameSingle", MySettings.RenameSingle_Movies, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("RenameSingle", MySettings.RenameSingle_Shows, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("RenameUpdate", MySettings.RenameUpdate_Episodes, , , Enums.ContentType.TVEpisode)
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