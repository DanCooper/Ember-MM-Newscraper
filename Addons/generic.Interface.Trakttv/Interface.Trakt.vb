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
Imports System.IO
Imports System.Xml.Serialization
Imports TraktApiSharp

Public Class Addon
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(control As ToolStripMenuItem, value As ToolStripItem)
    Public Delegate Sub Delegate_AddToolStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolStripItem(value As ToolStripItem)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _xmlSettingsPath As String = Path.Combine(Master.SettingsPath, "Interface.Trakt.xml")
    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty
    Private cmnu_MovieSets As New ToolStripMenuItem
    Private cmnu_Movies As New ToolStripMenuItem
    Private cmnu_TVEpisodes As New ToolStripMenuItem
    Private cmnu_TVSeasons As New ToolStripMenuItem
    Private cmnu_TVShows As New ToolStripMenuItem
    Private cmnuSep_MovieSets As New ToolStripSeparator
    Private cmnuSep_Movies As New ToolStripSeparator
    Private cmnuSep_TVEpisodes As New ToolStripSeparator
    Private cmnuSep_TVSeasons As New ToolStripSeparator
    Private cmnuSep_TVShows As New ToolStripSeparator
    Private cmnuTrayToolsTrakt As New ToolStripMenuItem
    Private mnuMainToolsTrakt As New ToolStripMenuItem
    Private _Enabled As Boolean = False
    Private _Name As String = "Trakt.tv Manager"
    Private _AddonSettings As New AddonSettings
    Private _TraktAPI As clsAPITrakt

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.GenericModule.GenericEvent
    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged
    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged
    Public Event SetupNeedsRestart() Implements Interfaces.GenericModule.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {
                                                      Enums.ModuleEventType.BeforeEdit_Movie,
                                                      Enums.ModuleEventType.BeforeEdit_TVEpisode,
                                                      Enums.ModuleEventType.BeforeEdit_TVShow,
                                                      Enums.ModuleEventType.CommandLine,
                                                      Enums.ModuleEventType.Generic,
                                                      Enums.ModuleEventType.Remove_Movie,
                                                      Enums.ModuleEventType.Remove_TVEpisode,
                                                      Enums.ModuleEventType.Remove_TVSeason,
                                                      Enums.ModuleEventType.Remove_TVShow,
                                                      Enums.ModuleEventType.ScraperMulti_Movie,
                                                      Enums.ModuleEventType.ScraperMulti_TVEpisode,
                                                      Enums.ModuleEventType.ScraperMulti_TVSeason,
                                                      Enums.ModuleEventType.ScraperMulti_TVShow,
                                                      Enums.ModuleEventType.ScraperSingle_Movie,
                                                      Enums.ModuleEventType.ScraperSingle_TVEpisode,
                                                      Enums.ModuleEventType.ScraperSingle_TVSeason,
                                                      Enums.ModuleEventType.ScraperSingle_TVShow})
        End Get
    End Property

    Property Enabled() As Boolean Implements Interfaces.GenericModule.Enabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            If _Enabled = value Then Return
            _Enabled = value
            If _Enabled Then
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

    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Select Case mType
            Case Enums.ModuleEventType.BeforeEdit_Movie
                If _AddonSettings.GetWatchedStateBeforeEdit_Movie AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.BeforeEdit_TVEpisode
                If _AddonSettings.GetWatchedStateBeforeEdit_TVEpisode AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_TVEpisode(_dbelement)
                End If
            Case Enums.ModuleEventType.CommandLine
                '_TraktAPI.SyncToEmber_All()
            Case Enums.ModuleEventType.ScraperMulti_Movie
                If _AddonSettings.GetWatchedStateScraperMulti_Movie AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.Remove_Movie
                If _AddonSettings.CollectionRemove_Movie AndAlso _dbelement IsNot Nothing Then
                    '_TraktAPI.RemoveFromCollection_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperMulti_TVShow, Enums.ModuleEventType.ScraperMulti_TVEpisode
                If _AddonSettings.GetWatchedStateScraperMulti_TVEpisode AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_TVEpisode(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperSingle_Movie
                If _AddonSettings.GetWatchedStateScraperSingle_Movie AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperSingle_TVShow, Enums.ModuleEventType.ScraperSingle_TVEpisode
                If _AddonSettings.GetWatchedStateScraperSingle_TVEpisode AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.GetWatchedState_TVEpisode(_dbelement)
                End If
        End Select

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolStripItem(tsi, mnuMainToolsTrakt)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolStripItem(tsi, cmnuTrayToolsTrakt)
    End Sub

    Sub Enable()
        _TraktAPI.CreateAPI(_AddonSettings, "77bfb26ae2b5e2217151bbb3a20309dbd5e638c5a85e17505457edd14c6399fa", "240bd5554e2c8065dfe3cf7027b292193a113d96106a5f04a0b088f9d2371757")
        PopulateMenus()
    End Sub

    Public Sub RemoveToolStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_Movies(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_Movies), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_MovieSets(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuMovieSetList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_MovieSets), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVEpisodes(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVEpisodes), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVSeasons(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVSeasons), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVShows(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVShows), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Remove(value)
        End If
    End Sub

    Public Sub AddToolStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_Movies(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_Movies), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_MovieSets(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuMovieSetList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_MovieSets), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVEpisodes(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVEpisodes), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVSeasons(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVSeasons), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVShows(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVShows), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Add(value)
        End If
    End Sub

    Private Sub Handle_GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef params As List(Of Object))
        RaiseEvent GenericEvent(mType, params)
    End Sub

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent ModuleEnabledChanged(_Name, State, 0)
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_NewToken()
        _AddonSettings.APIAccessToken = _TraktAPI.AccessToken
        _AddonSettings.APICreated = Functions.ConvertToUnixTimestamp(_TraktAPI.Created).ToString
        _AddonSettings.APIExpiresInSeconds = _TraktAPI.ExpiresInSeconds.ToString
        _AddonSettings.APIRefreshToken = _TraktAPI.RefreshToken
        SaveSettings()
    End Sub

    Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
        _TraktAPI = New clsAPITrakt
        AddHandler _TraktAPI.NewTokenCreated, AddressOf Handle_NewToken
    End Sub

    Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _Enabled
        _setup.chkGetWatchedStateBeforeEdit_Movie.Checked = _AddonSettings.GetWatchedStateBeforeEdit_Movie
        _setup.chkGetWatchedStateBeforeEdit_TVEpisode.Checked = _AddonSettings.GetWatchedStateBeforeEdit_TVEpisode
        _setup.chkGetWatchedStateScraperMulti_Movie.Checked = _AddonSettings.GetWatchedStateScraperMulti_Movie
        _setup.chkGetWatchedStateScraperMulti_TVEpisode.Checked = _AddonSettings.GetWatchedStateScraperMulti_TVEpisode
        _setup.chkGetWatchedStateScraperSingle_Movie.Checked = _AddonSettings.GetWatchedStateScraperSingle_Movie
        _setup.chkGetWatchedStateScraperSingle_TVEpisode.Checked = _AddonSettings.GetWatchedStateScraperSingle_TVEpisode

        SPanel.Name = _Name
        SPanel.Text = "Trakt.tv Interface"
        SPanel.Prefix = "Trakt_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_Enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Sub LoadSettings()
        _AddonSettings = New AddonSettings
        If File.Exists(_xmlSettingsPath) Then
            Dim xmlSer As XmlSerializer = Nothing
            Using xmlSR As StreamReader = New StreamReader(_xmlSettingsPath)
                xmlSer = New XmlSerializer(GetType(AddonSettings))
                _AddonSettings = DirectCast(xmlSer.Deserialize(xmlSR), AddonSettings)
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Get movie watched state from Trakt
    ''' </summary>
    ''' <param name="sender">context menu "Get Movie WatchedState"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuGetWatchedState_Movie_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lstWatchedMovies = _TraktAPI.GetWatched_Movies
        For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
            Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
            Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                If _TraktAPI.GetWatchedState_Movie(DBElement, lstWatchedMovies) Then
                    Master.DB.Save_Movie(DBElement, False, True, False, True, False)
                    logger.Trace(String.Format("[TraktWorker] GetWatchedStateSelected_Movie: ""{0}"" | Synced to Ember", DBElement.Movie.Title))
                    RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {DBElement.ID}))
                End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' Get episode watched state from Trakt
    ''' </summary>
    ''' <param name="sender">context menu "Get TVEpisode WatchedState"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuGetWatchedState_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lstWatchedShows = _TraktAPI.GetWatched_TVShows
        For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
            Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
            Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                If _TraktAPI.GetWatchedState_TVEpisode(DBElement, lstWatchedShows) Then
                    Master.DB.Save_TVEpisode(DBElement, False, True, False, False, True, False)
                    logger.Trace(String.Format("[TraktWorker] GetWatchedStateSelected_TVEpisode: ""{0}: S{1}E{2} - {3}"" | Synced to Ember",
                                               DBElement.TVShow.Title,
                                               DBElement.TVEpisode.Season,
                                               DBElement.TVEpisode.Episode,
                                               DBElement.TVEpisode.Title))
                    RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {DBElement.ID}))
                End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' Get episodes playcount for whole season on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get TVSeason Playcount"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Update details of season in Kodi DB
    ''' </remarks>
    Private Sub cmnuGetWatchedState_TVSeason_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lstWatchedShows = _TraktAPI.GetWatched_TVShows
        For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
            Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
            Dim DBTVSeason As Database.DBElement = Master.DB.Load_TVSeason(ID, True, True)
            For Each DBElement In DBTVSeason.Episodes
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                    If _TraktAPI.GetWatchedState_TVEpisode(DBElement, lstWatchedShows) Then
                        Master.DB.Save_TVEpisode(DBElement, False, True, False, False, True, False)
                        logger.Trace(String.Format("[TraktWorker] GetWatchedStateSelected_TVEpisode: ""{0}: S{1}E{2} - {3}"" | Synced to Ember",
                                                   DBElement.TVShow.Title,
                                                   DBElement.TVEpisode.Season,
                                                   DBElement.TVEpisode.Episode,
                                                   DBElement.TVEpisode.Title))
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {DBElement.ID}))
                    End If
                End If
            Next
            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVSeason, New List(Of Object)(New Object() {DBTVSeason.ID}))
        Next
    End Sub
    ''' <summary>
    ''' Get episodes playcount for whole tv show on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get Tvshow Playcount"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuGetWatchedState_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lstWatchedShows = _TraktAPI.GetWatched_TVShows
        For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
            Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
            Dim DBTVSShow As Database.DBElement = Master.DB.Load_TVShow(ID, False, True)
            For Each DBElement In DBTVSShow.Episodes
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                    If _TraktAPI.GetWatchedState_TVEpisode(DBElement, lstWatchedShows) Then
                        Master.DB.Save_TVEpisode(DBElement, False, True, False, False, True, False)
                        logger.Trace(String.Format("[TraktWorker] GetWatchedStateSelected_TVEpisode: ""{0}: S{1}E{2} - {3}"" | Synced to Ember",
                                                   DBElement.TVShow.Title,
                                                   DBElement.TVEpisode.Season,
                                                   DBElement.TVEpisode.Episode,
                                                   DBElement.TVEpisode.Title))
                        RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {DBElement.ID}))
                    End If
                End If
            Next
            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {DBTVSShow.ID}))
        Next
    End Sub
    ''' <summary>
    '''  Get WatchedState of all movies of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub mnuGetPlaycount_Movies_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim nDialog As New dlgWorker(_TraktAPI, Enums.ContentType.Movie)
        AddHandler nDialog.GenericEvent, AddressOf Handle_GenericEvent
        nDialog.ShowDialog()
        RemoveHandler nDialog.GenericEvent, AddressOf Handle_GenericEvent
    End Sub
    ''' <summary>
    '''  Get WatchedState of all tv shows of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub mnuGetPlaycount_TVEpisodes_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim nDialog As New dlgWorker(_TraktAPI, Enums.ContentType.TVEpisode)
        AddHandler nDialog.GenericEvent, AddressOf Handle_GenericEvent
        nDialog.ShowDialog()
        RemoveHandler nDialog.GenericEvent, AddressOf Handle_GenericEvent
    End Sub

    Private Sub mnuTrakttvManager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub PopulateMenus()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        mnuMainToolsTrakt.DropDownItems.Clear()
        mnuMainToolsTrakt.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsTrakt.Text = "Trakt.tv"
        mnuMainToolsTrakt.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForMovieSets = True, .IfTabMovieSets = True, .ForTVShows = True, .IfTabTVShows = True}
        CreateToolsMenu(mnuMainToolsTrakt)
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolStripItem(tsi, mnuMainToolsTrakt)

        'mnuTrayTools
        cmnuTrayToolsTrakt.DropDownItems.Clear()
        cmnuTrayToolsTrakt.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsTrakt.Text = "Trakt.tv"
        CreateToolsMenu(cmnuTrayToolsTrakt)
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolStripItem(tsi, cmnuTrayToolsTrakt)

        'cmnuMovies
        cmnu_Movies.DropDownItems.Clear()
        cmnu_Movies.Image = New Bitmap(My.Resources.icon)
        cmnu_Movies.Text = "Trakt.tv"
        CreateContextMenu(cmnu_Movies, Enums.ContentType.Movie)
        AddToolStripItem_Movies(cmnuSep_Movies)
        AddToolStripItem_Movies(cmnu_Movies)

        'cmnuTVEpisodes
        cmnu_TVEpisodes.DropDownItems.Clear()
        cmnu_TVEpisodes.Image = New Bitmap(My.Resources.icon)
        cmnu_TVEpisodes.Text = "Trakt.tv"
        CreateContextMenu(cmnu_TVEpisodes, Enums.ContentType.TVEpisode)
        AddToolStripItem_TVEpisodes(cmnuSep_TVEpisodes)
        AddToolStripItem_TVEpisodes(cmnu_TVEpisodes)

        'cmnuTVSeasons
        cmnu_TVSeasons.DropDownItems.Clear()
        cmnu_TVSeasons.Image = New Bitmap(My.Resources.icon)
        cmnu_TVSeasons.Text = "Trakt.tv"
        CreateContextMenu(cmnu_TVSeasons, Enums.ContentType.TVSeason)
        AddToolStripItem_TVSeasons(cmnuSep_TVSeasons)
        AddToolStripItem_TVSeasons(cmnu_TVSeasons)

        'cmnuTVShows
        cmnu_TVShows.DropDownItems.Clear()
        cmnu_TVShows.Image = New Bitmap(My.Resources.icon)
        cmnu_TVShows.Text = "Trakt.tv"
        CreateContextMenu(cmnu_TVShows, Enums.ContentType.TVShow)
        AddToolStripItem_TVShows(cmnuSep_TVShows)
        AddToolStripItem_TVShows(cmnu_TVShows)
    End Sub

    Private Sub CreateContextMenu(ByRef tMenu As ToolStripMenuItem, ByVal tContentType As Enums.ContentType)
        'Dim mnuHostSyncItem As New ToolStripMenuItem
        'mnuHostSyncItem.Image = New Bitmap(My.Resources.menuSync)
        'mnuHostSyncItem.Text = Master.eLang.GetString(1446, "Sync")
        'Select Case tContentType
        '    Case Enums.ContentType.Movie
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_Movie_Click
        '    Case Enums.ContentType.MovieSet
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_MovieSet_Click
        '    Case Enums.ContentType.TVEpisode
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVEpisode_Click
        '    Case Enums.ContentType.TVSeason
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVSeason_Click
        '    Case Enums.ContentType.TVShow
        '        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVShow_Click
        'End Select
        'tMenu.DropDownItems.Add(mnuHostSyncItem)
        'If tContentType = Enums.ContentType.TVSeason OrElse tContentType = Enums.ContentType.TVShow Then
        '    Dim mnuHostSyncFullItem As New ToolStripMenuItem
        '    mnuHostSyncFullItem.Image = New Bitmap(My.Resources.menuSync)
        '    mnuHostSyncFullItem.Text = Master.eLang.GetString(1008, "Sync Full")
        '    Select Case tContentType
        '        Case Enums.ContentType.TVSeason
        '            AddHandler mnuHostSyncFullItem.Click, AddressOf cmnuHostSyncFullItem_TVSeason_Click
        '        Case Enums.ContentType.TVShow
        '            AddHandler mnuHostSyncFullItem.Click, AddressOf cmnuHostSyncFullItem_TVShow_Click
        '    End Select
        '    tMenu.DropDownItems.Add(mnuHostSyncFullItem)
        'End If
        If tContentType = Enums.ContentType.Movie OrElse tContentType = Enums.ContentType.TVEpisode OrElse tContentType = Enums.ContentType.TVSeason OrElse tContentType = Enums.ContentType.TVShow Then
            Dim mnuGetWatchedState As New ToolStripMenuItem
            mnuGetWatchedState.Image = New Bitmap(My.Resources.menuWatchedState)
            mnuGetWatchedState.Text = Master.eLang.GetString(1070, "Get Watched State")
            Select Case tContentType
                Case Enums.ContentType.Movie
                    AddHandler mnuGetWatchedState.Click, AddressOf cmnuGetWatchedState_Movie_Click
                Case Enums.ContentType.TVEpisode
                    AddHandler mnuGetWatchedState.Click, AddressOf cmnuGetWatchedState_TVEpisode_Click
                Case Enums.ContentType.TVSeason
                    AddHandler mnuGetWatchedState.Click, AddressOf cmnuGetWatchedState_TVSeason_Click
                Case Enums.ContentType.TVShow
                    AddHandler mnuGetWatchedState.Click, AddressOf cmnuGetWatchedState_TVShow_Click
            End Select
            tMenu.DropDownItems.Add(mnuGetWatchedState)
        End If
        'If tContentType = Enums.ContentType.Movie OrElse tContentType = Enums.ContentType.TVEpisode OrElse tContentType = Enums.ContentType.TVShow Then
        '    Dim mnuHostRemoveItem As New ToolStripMenuItem
        '    mnuHostRemoveItem.Image = New Bitmap(My.Resources.menuRemove)
        '    mnuHostRemoveItem.Text = Master.eLang.GetString(30, "Remove")
        '    Select Case tContentType
        '        Case Enums.ContentType.Movie
        '            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_Movie_Click
        '        Case Enums.ContentType.TVEpisode
        '            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_TVEpisode_Click
        '        Case Enums.ContentType.TVShow
        '            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_TVShow_Click
        '    End Select
        '    tMenu.DropDownItems.Add(mnuHostRemoveItem)
        'End If
    End Sub

    Private Sub CreateToolsMenu(ByRef tMenu As ToolStripMenuItem)
        Dim mnuTrakttvManager As New ToolStripMenuItem
        mnuTrakttvManager.Image = New Bitmap(My.Resources.icon)
        mnuTrakttvManager.Text = String.Concat(Master.eLang.GetString(871, "Trakt.tv Manager"), " (temp. disabled for rework)")
        mnuTrakttvManager.Enabled = False
        AddHandler mnuTrakttvManager.Click, AddressOf mnuTrakttvManager_Click
        tMenu.DropDownItems.Add(mnuTrakttvManager)
        tMenu.DropDownItems.Add(New ToolStripSeparator)

        Dim mnuGetPlaycount_Movies As New ToolStripMenuItem
        mnuGetPlaycount_Movies.Image = New Bitmap(My.Resources.menuWatchedState)
        mnuGetPlaycount_Movies.Text = String.Format("{0} - {1}",
                                                        Master.eLang.GetString(1070, "Get Watched State"),
                                                        Master.eLang.GetString(36, "Movies"))
        AddHandler mnuGetPlaycount_Movies.Click, AddressOf mnuGetPlaycount_Movies_Click
        tMenu.DropDownItems.Add(mnuGetPlaycount_Movies)

        Dim mnuGetPlaycount_TVEpisodes As New ToolStripMenuItem
        mnuGetPlaycount_TVEpisodes.Image = New Bitmap(My.Resources.menuWatchedState)
        mnuGetPlaycount_TVEpisodes.Text = String.Format("{0} - {1}",
                                                            Master.eLang.GetString(1070, "Get Watched State"),
                                                            Master.eLang.GetString(682, "Episodes"))
        AddHandler mnuGetPlaycount_TVEpisodes.Click, AddressOf mnuGetPlaycount_TVEpisodes_Click
        tMenu.DropDownItems.Add(mnuGetPlaycount_TVEpisodes)
    End Sub

    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = _setup.chkEnabled.Checked
        _AddonSettings.GetWatchedStateBeforeEdit_Movie = _setup.chkGetWatchedStateBeforeEdit_Movie.Checked
        _AddonSettings.GetWatchedStateBeforeEdit_TVEpisode = _setup.chkGetWatchedStateBeforeEdit_TVEpisode.Checked
        _AddonSettings.GetWatchedStateScraperMulti_Movie = _setup.chkGetWatchedStateScraperMulti_Movie.Checked
        _AddonSettings.GetWatchedStateScraperMulti_TVEpisode = _setup.chkGetWatchedStateScraperMulti_TVEpisode.Checked
        _AddonSettings.GetWatchedStateScraperSingle_Movie = _setup.chkGetWatchedStateScraperSingle_Movie.Checked
        _AddonSettings.GetWatchedStateScraperSingle_TVEpisode = _setup.chkGetWatchedStateScraperSingle_TVEpisode.Checked

        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
        If Not File.Exists(_xmlSettingsPath) OrElse (Not CBool(File.GetAttributes(_xmlSettingsPath) And FileAttributes.ReadOnly)) Then
            If File.Exists(_xmlSettingsPath) Then
                Dim fAtt As FileAttributes = File.GetAttributes(_xmlSettingsPath)
                Try
                    File.SetAttributes(_xmlSettingsPath, FileAttributes.Normal)
                Catch ex As Exception
                    logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
            Using xmlSW As New StreamWriter(_xmlSettingsPath)
                Dim xmlSer As New XmlSerializer(GetType(AddonSettings))
                xmlSer.Serialize(xmlSW, _AddonSettings)
            End Using
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    <Serializable()>
    <XmlRoot("interface.trakt")>
    Class AddonSettings

#Region "Properties"

        <XmlElement("apiaccesstoken")>
        Public Property APIAccessToken() As String = String.Empty

        <XmlElement("apicreated")>
        Public Property APICreated() As String = String.Empty

        <XmlElement("apiexpiresinseconds")>
        Public Property APIExpiresInSeconds() As String = String.Empty

        <XmlElement("apirefreshtoken")>
        Public Property APIRefreshToken() As String = String.Empty

        <XmlElement("collectionremove_movie")>
        Public Property CollectionRemove_Movie() As Boolean

        <XmlElement("getshowprogress")>
        Public Property GetShowProgress() As Boolean

        <XmlElement("getwatchedstatebeforeedit_movie")>
        Public Property GetWatchedStateBeforeEdit_Movie() As Boolean

        <XmlElement("getwatchedstatebeforeedit_tvepisode")>
        Public Property GetWatchedStateBeforeEdit_TVEpisode() As Boolean

        <XmlElement("getwatchedstatescrapermulti_movie")>
        Public Property GetWatchedStateScraperMulti_Movie() As Boolean

        <XmlElement("getwatchedstatescrapermulti_tvepisode")>
        Public Property GetWatchedStateScraperMulti_TVEpisode() As Boolean

        <XmlElement("getwatchedstatescrapersingle_movie")>
        Public Property GetWatchedStateScraperSingle_Movie() As Boolean

        <XmlElement("getwatchedstatescrapersingle_tvepisode")>
        Public Property GetWatchedStateScraperSingle_TVEpisode() As Boolean

#End Region 'Properties

    End Class
    ''' <summary>
    ''' structure used to read setting file of Kodi Interface
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    <XmlRoot("interface.kodi")>
    Class KodiSettings

#Region "Fields"

        Private _hosts As New List(Of Host)
        Private _sendnotifications As Boolean
        Private _syncplaycounts As Boolean
        Private _syncplaycountshost As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("sendnotifications")>
        Public Property SendNotifications() As Boolean
            Get
                Return _sendnotifications
            End Get
            Set(ByVal value As Boolean)
                _sendnotifications = value
            End Set
        End Property

        <XmlElement("syncplaycounts")>
        Public Property SyncPlayCounts() As Boolean
            Get
                Return _syncplaycounts
            End Get
            Set(ByVal value As Boolean)
                _syncplaycounts = value
            End Set
        End Property

        <XmlElement("syncplaycountshost")>
        Public Property SyncPlayCountsHost() As String
            Get
                Return _syncplaycountshost
            End Get
            Set(ByVal value As String)
                _syncplaycountshost = value
            End Set
        End Property

        <XmlElement("host")>
        Public Property Hosts() As List(Of Host)
            Get
                Return _hosts
            End Get
            Set(ByVal value As List(Of Host))
                _hosts = value
            End Set
        End Property

#End Region 'Properties

    End Class

    <Serializable()>
    Class Host

#Region "Fields"

        Private _address As String
        Private _label As String
        Private _moviesetartworkspath As String
        Private _password As String
        Private _port As Integer
        Private _realtimesync As Boolean
        Private _sources As New List(Of Source)
        Private _username As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("label")>
        Public Property Label() As String
            Get
                Return _label
            End Get
            Set(ByVal value As String)
                _label = value
            End Set
        End Property

        <XmlElement("address")>
        Public Property Address() As String
            Get
                Return _address
            End Get
            Set(ByVal value As String)
                _address = value
            End Set
        End Property

        <XmlElement("port")>
        Public Property Port() As Integer
            Get
                Return _port
            End Get
            Set(ByVal value As Integer)
                _port = value
            End Set
        End Property

        <XmlElement("username")>
        Public Property Username() As String
            Get
                Return _username
            End Get
            Set(ByVal value As String)
                _username = value
            End Set
        End Property

        <XmlElement("password")>
        Public Property Password() As String
            Get
                Return _password
            End Get
            Set(ByVal value As String)
                _password = value
            End Set
        End Property

        <XmlElement("realtimesync")>
        Public Property RealTimeSync() As Boolean
            Get
                Return _realtimesync
            End Get
            Set(ByVal value As Boolean)
                _realtimesync = value
            End Set
        End Property

        <XmlElement("moviesetartworkspath")>
        Public Property MovieSetArtworksPath() As String
            Get
                Return _moviesetartworkspath
            End Get
            Set(ByVal value As String)
                _moviesetartworkspath = value
            End Set
        End Property

        <XmlElement("source")>
        Public Property Sources() As List(Of Source)
            Get
                Return _sources
            End Get
            Set(ByVal value As List(Of Source))
                _sources = value
            End Set
        End Property

#End Region 'Properties

    End Class


    <Serializable()>
    Class Source

#Region "Fields"

        Private _contenttype As Enums.ContentType
        Private _localpath As String
        Private _remotepath As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("contenttype")>
        Public Property ContentType() As Enums.ContentType
            Get
                Return _contenttype
            End Get
            Set(ByVal value As Enums.ContentType)
                _contenttype = value
            End Set
        End Property

        <XmlElement("localpath")>
        Public Property LocalPath() As String
            Get
                Return _localpath
            End Get
            Set(ByVal value As String)
                _localpath = value
            End Set
        End Property

        <XmlElement("remotepath")>
        Public Property RemotePath() As String
            Get
                Return _remotepath
            End Get
            Set(ByVal value As String)
                _remotepath = value
            End Set
        End Property

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class






