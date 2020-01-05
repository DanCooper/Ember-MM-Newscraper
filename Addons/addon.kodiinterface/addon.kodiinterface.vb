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
Imports System.IO
Imports System.Xml.Serialization

Public Class Generic
    Implements Interfaces.IGenericAddon

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolStripItem(ts As ToolStrip, value As ToolStripItem)
    Public Delegate Sub Delegate_AddToolStripMenuItem(tsi As ToolStripMenuItem, value As ToolStripMenuItem)
    Public Delegate Sub Delegate_AddToolStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolStripItem(value As ToolStripItem)

    Public Delegate Sub Delegate_ChangeTaskManagerStatus(control As ToolStripLabel, value As String)
    Public Delegate Sub Delegate_ChangeTaskManagerProgressBar(control As ToolStripProgressBar, value As ProgressBarStyle)

#End Region 'Delegates

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    'reflects the current host(s) settings/setup configured in settings, will be filled at module startup from XML settings (and is used to write changes of settings back into XML)
    Private _SpecialSettings As New SpecialSettings
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel
    Private _xmlSettingsPath As String = Path.Combine(Master.SettingsPath, "Interface.Kodi.xml")
    Private _CmnuKodi_MovieSets As New ToolStripMenuItem
    Private _CmnuKodi_Movies As New ToolStripMenuItem
    Private _CmnuKodi_TVEpisodes As New ToolStripMenuItem
    Private _CmnuKodi_TVSeasons As New ToolStripMenuItem
    Private _CmnuKodi_TVShows As New ToolStripMenuItem
    Private _CmnuSep_MovieSets As New ToolStripSeparator
    Private _CmnuSep_Movies As New ToolStripSeparator
    Private _CmnuSep_TVEpisodes As New ToolStripSeparator
    Private _CmnuSep_TVSeasons As New ToolStripSeparator
    Private _CmnuSep_TVShows As New ToolStripSeparator
    Private _CnuMainToolsKodi As New ToolStripMenuItem
    Private _CnuTrayToolsKodi As New ToolStripMenuItem

    Private _LblTaskManagerStatus As New ToolStripLabel With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _LblTaskManagerTitle As New ToolStripLabel With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TspTaskManager As New ToolStripProgressBar With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TssTaskManager1 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TssTaskManager2 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TssTaskManager3 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TssTaskManager4 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TssTaskManager5 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TssTaskManager6 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TssTaskManager7 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private _TssTaskManager8 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}

    ''' <summary>
    ''' pool of Update tasks for KodiInterface (can be filled extremely fast when updating whole tvshow at once)
    ''' </summary>
    ''' <remarks></remarks>
    Private _TaskList As New Queue(Of KodiTask)
    ''' <summary>
    ''' control variable: true=Ready to start tmrRunTasks-Timer and work through items of tasklist, false= Timer already tickting, executing tasks
    ''' </summary>
    ''' <remarks></remarks>
    Private _TasksDone As Boolean = True

#End Region 'Fields

#Region "Properties"

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IGenericAddon.IsBusy
        Get
            Return Not _TasksDone
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
    ''' <summary>
    ''' Subscribe to Eventtypes here
    ''' </summary>
    Public ReadOnly Property Type() As List(Of Enums.AddonEventType) Implements Interfaces.IGenericAddon.Type
        Get
            Return New List(Of Enums.AddonEventType)(New Enums.AddonEventType() {
                                                      Enums.AddonEventType.BeforeEdit_Movie,
                                                      Enums.AddonEventType.BeforeEdit_TVEpisode,
                                                      Enums.AddonEventType.Remove_Movie,
                                                      Enums.AddonEventType.Remove_TVEpisode,
                                                      Enums.AddonEventType.Remove_TVShow,
                                                      Enums.AddonEventType.ScraperMulti_Movie,
                                                      Enums.AddonEventType.ScraperMulti_TVEpisode,
                                                      Enums.AddonEventType.ScraperMulti_TVSeason,
                                                      Enums.AddonEventType.ScraperMulti_TVShow,
                                                      Enums.AddonEventType.ScraperSingle_Movie,
                                                      Enums.AddonEventType.ScraperSingle_TVEpisode,
                                                      Enums.AddonEventType.ScraperSingle_TVSeason,
                                                      Enums.AddonEventType.ScraperSingle_TVShow,
                                                      Enums.AddonEventType.Sync_Movie,
                                                      Enums.AddonEventType.Sync_MovieSet,
                                                      Enums.AddonEventType.Sync_TVEpisode,
                                                      Enums.AddonEventType.Sync_TVSeason,
                                                      Enums.AddonEventType.Sync_TVShow})
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object)) Implements Interfaces.IGenericAddon.GenericEvent
    Public Event NeedsRestart() Implements Interfaces.IGenericAddon.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IGenericAddon.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IGenericAddon.StateChanged

#End Region 'Events

#Region "Event Methods"

    Sub Handle_GenericEvent(ByVal ModuleEventType As Enums.AddonEventType, ByRef Parameters As List(Of Object))
        RaiseEvent GenericEvent(ModuleEventType, Parameters)
    End Sub

    Sub Handle_GenericEventAsync(ByVal mGenericEventCallBack As GenericEventCallBackAsync)
        RaiseEvent GenericEvent(mGenericEventCallBack.tEventType, mGenericEventCallBack.tParams)
    End Sub

    Sub Handle_GenericSubEventAsync(ByVal mGenericSubEventCallBack As GenericSubEventCallBackAsync)
        mGenericSubEventCallBack.tProgress.Report(mGenericSubEventCallBack.tGenericEventCallBackAsync)
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
    ''' <summary>
    ''' Implementation of Realtime Sync, triggered outside of this module i.e after finishing edits of a movie (=Enums.ModuleEventType.Sync_Movie)
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/26 Cocotus - First implementation, code prepared by DanCooper
    ''' this works through listening to various Enums.ModuleEventTypes, i.e Enums.ModuleEventType.Sync_Movie which will be triggered whenever movie details were changed
    ''' TODO, 2015/07/06 Cocotus  
    ''' - RunGeneric is a synched function, so we can't use Await in conjunction with async KodiAPI here (which is preferred). For Ember 1.5 I suggest to change RunGeneric to Public Async Function
    ''' - As soon as RunGeneric is Async, switch all API calling subs/function in here to async to so we can use await and enable result notification in Ember (see commented code below)
    ''' 2015/08/18 Cocotus  
    ''' For now we use concept of storing pool of API tasks in list (="TaskList") and use a timer object and its tick-event to get the work done
    ''' Timer tick event is async so we can queue with await all API tasks
    ''' </remarks>
    Public Function Run(ByVal mType As Enums.AddonEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericAddon.Run
        If Not Master.isCL AndAlso (
                mType = Enums.AddonEventType.Remove_Movie OrElse
                mType = Enums.AddonEventType.Remove_TVEpisode OrElse
                mType = Enums.AddonEventType.Remove_TVShow OrElse
                mType = Enums.AddonEventType.Sync_Movie OrElse
                mType = Enums.AddonEventType.Sync_MovieSet OrElse
                mType = Enums.AddonEventType.Sync_TVEpisode OrElse
                mType = Enums.AddonEventType.Sync_TVSeason OrElse
                mType = Enums.AddonEventType.Sync_TVShow) Then
            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mType = mType, .mDBElement = _dbelement})
            Return New Interfaces.ModuleResult With {.breakChain = False}
        Else
            Select Case mType
                Case Enums.AddonEventType.BeforeEdit_Movie
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateBeforeEdit_Movie Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.AddonEventType.BeforeEdit_TVEpisode
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.AddonEventType.ScraperMulti_Movie
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateScraperMulti_Movie Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.AddonEventType.ScraperMulti_TVEpisode, Enums.AddonEventType.ScraperMulti_TVSeason, Enums.AddonEventType.ScraperMulti_TVShow
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.AddonEventType.ScraperSingle_Movie
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateScraperSingle_Movie Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.AddonEventType.ScraperSingle_TVEpisode, Enums.AddonEventType.ScraperSingle_TVSeason, Enums.AddonEventType.ScraperSingle_TVShow
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateScraperSingle_TVEpisode Then
                        Return New Interfaces.ModuleResult
                    End If
            End Select

            Dim mDBElement As Database.DBElement = _dbelement
            Dim tTask As Task(Of Boolean) = Task.Run(Function() DoCommandLine(mType, mDBElement))
            While Not tTask.IsCompleted
                Threading.Thread.Sleep(50)
            End While
        End If
    End Function
    ''' <summary>
    ''' Load and fill controls of settings page of module
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Triggered when user enters settings in Ember
    Sub InjectSettingsPanel() Implements Interfaces.IGenericAddon.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.chkEnabled.Checked = _Enabled
        _PnlSettingsPanel.chkGetWatchedState.Checked = _SpecialSettings.GetWatchedState
        _PnlSettingsPanel.chkGetWatchedStateBeforeEdit_Movie.Checked = _SpecialSettings.GetWatchedStateBeforeEdit_Movie
        _PnlSettingsPanel.chkGetWatchedStateBeforeEdit_TVEpisode.Checked = _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode
        _PnlSettingsPanel.chkGetWatchedStateScraperMulti_Movie.Checked = _SpecialSettings.GetWatchedStateScraperMulti_Movie
        _PnlSettingsPanel.chkGetWatchedStateScraperMulti_TVEpisode.Checked = _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode
        _PnlSettingsPanel.chkGetWatchedStateScraperSingle_Movie.Checked = _SpecialSettings.GetWatchedStateScraperSingle_Movie
        _PnlSettingsPanel.chkGetWatchedStateScraperSingle_TVEpisode.Checked = _SpecialSettings.GetWatchedStateScraperSingle_TVEpisode
        _PnlSettingsPanel.chkNotification.Checked = _SpecialSettings.SendNotifications
        If _SpecialSettings.GetWatchedState Then
            _PnlSettingsPanel.cbGetWatchedStateHost.Enabled = True
        Else
            _PnlSettingsPanel.cbGetWatchedStateHost.Enabled = False
        End If
        _PnlSettingsPanel.HostList = _SpecialSettings.Hosts
        _PnlSettingsPanel.lbHosts.Items.Clear()
        For Each tHost As Host In _PnlSettingsPanel.HostList
            _PnlSettingsPanel.cbGetWatchedStateHost.Items.Add(tHost.Label)
            _PnlSettingsPanel.lbHosts.Items.Add(tHost.Label)
        Next
        _PnlSettingsPanel.cbGetWatchedStateHost.SelectedIndex = _PnlSettingsPanel.cbGetWatchedStateHost.FindStringExact(_SpecialSettings.GetWatchedStateHost)
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(_Enabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings(),
            .Title = "Kodi Interface",
            .Type = Enums.SettingsPanelType.Addon
        }
    End Sub

    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.IGenericAddon.SaveSetup
        IsEnabled = _PnlSettingsPanel.chkEnabled.Checked
        _SpecialSettings.SendNotifications = _PnlSettingsPanel.chkNotification.Checked
        _SpecialSettings.GetWatchedState = _PnlSettingsPanel.chkGetWatchedState.Checked AndAlso _PnlSettingsPanel.cbGetWatchedStateHost.SelectedItem IsNot Nothing
        _SpecialSettings.GetWatchedStateBeforeEdit_Movie = _PnlSettingsPanel.chkGetWatchedStateBeforeEdit_Movie.Checked
        _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode = _PnlSettingsPanel.chkGetWatchedStateBeforeEdit_TVEpisode.Checked
        _SpecialSettings.GetWatchedStateScraperMulti_Movie = _PnlSettingsPanel.chkGetWatchedStateScraperMulti_Movie.Checked
        _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode = _PnlSettingsPanel.chkGetWatchedStateScraperMulti_TVEpisode.Checked
        _SpecialSettings.GetWatchedStateScraperSingle_Movie = _PnlSettingsPanel.chkGetWatchedStateScraperSingle_Movie.Checked
        _SpecialSettings.GetWatchedStateScraperSingle_TVEpisode = _PnlSettingsPanel.chkGetWatchedStateScraperSingle_TVEpisode.Checked
        _SpecialSettings.GetWatchedStateHost = If(_PnlSettingsPanel.cbGetWatchedStateHost.SelectedItem IsNot Nothing, _PnlSettingsPanel.cbGetWatchedStateHost.SelectedItem.ToString(), String.Empty)
        Settings_Save()
        If IsEnabled Then PopulateMenus()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Private Async Function DoCommandLine(ByVal mType As Enums.AddonEventType, ByVal mDBElement As Database.DBElement) As Task(Of Boolean)
        Dim GenericEventActionAsync As New Action(Of GenericEventCallBackAsync)(AddressOf Handle_GenericEventAsync)
        Dim GenericEventProgressAsync = New Progress(Of GenericEventCallBackAsync)(GenericEventActionAsync)
        Return Await Task.Run(Function() GenericRunCallBack(mType, mDBElement, GenericEventProgressAsync))
    End Function

    Private Sub AddTask(ByRef nTask As KodiTask)
        _TaskList.Enqueue(nTask)
        If _TasksDone Then
            RunTasks()
        Else
            ChangeTaskManagerStatus(_LblTaskManagerStatus, String.Concat("Pending Tasks: ", (_TaskList.Count + 1).ToString))
        End If
    End Sub

    Private Async Sub RunTasks()
        Dim getError As Boolean = False
        Dim GenericEventActionAsync As New Action(Of GenericEventCallBackAsync)(AddressOf Handle_GenericEventAsync)
        Dim GenericEventProgressAsync = New Progress(Of GenericEventCallBackAsync)(GenericEventActionAsync)

        _TasksDone = False
        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1439, "Run Tasks"), New Bitmap(My.Resources.logo)}))
        While _TaskList.Count > 0
            ChangeTaskManagerStatus(_LblTaskManagerStatus, String.Concat("Pending Tasks: ", _TaskList.Count.ToString))
            ChangeTaskManagerProgressBar(_TspTaskManager, ProgressBarStyle.Marquee)
            Dim kTask As KodiTask = _TaskList.Dequeue()
            If Not Await GenericRunCallBack(kTask.mType, kTask.mDBElement, GenericEventProgressAsync, kTask.mHost, kTask.mInternalType) Then
                getError = True
            End If
        End While
        _TasksDone = True
        ChangeTaskManagerProgressBar(_TspTaskManager, ProgressBarStyle.Continuous)
        ChangeTaskManagerStatus(_LblTaskManagerStatus, "No Pending Tasks")
        If Not getError Then
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(251, "All Tasks Done"), New Bitmap(My.Resources.logo)}))
        Else
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), String.Format(Master.eLang.GetString(969, "One or more Task(s) failed.{0}Please check log for more informations"), Environment.NewLine), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' This is a generic callback function to handle all realtime-sync work for KODI-Api
    ''' </summary>
    ''' <param name="mType"></param>
    ''' <remarks>
    ''' Worker function used to handle all ApiTaks in List(of KodiTask)
    ''' Made async to await async Kodi API
    ''' </remarks>
    Private Async Function GenericRunCallBack(ByVal mType As Enums.AddonEventType, ByVal mDBElement As Database.DBElement, ByVal GenericEventProcess As IProgress(Of GenericEventCallBackAsync), Optional mHost As Host = Nothing, Optional mInternalType As InternalType = Nothing) As Task(Of Boolean)
        Dim getError As Boolean = False
        Dim GenericSubEventActionAsync As New Action(Of GenericSubEventCallBackAsync)(AddressOf Handle_GenericSubEventAsync)
        Dim GenericSubEventProgressAsync = New Progress(Of GenericSubEventCallBackAsync)(GenericSubEventActionAsync)

        'check if at least one host is configured, else skip
        If _SpecialSettings.Hosts.Count > 0 Then
            Select Case mType

                'Before Edit Movie / Scraper Multi Movie / Scraper Single Movie
                Case Enums.AddonEventType.BeforeEdit_Movie, Enums.AddonEventType.ScraperMulti_Movie, Enums.AddonEventType.ScraperSingle_Movie
                    If mDBElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                        mHost = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(mDBElement, True) Then
                                    If mDBElement.NfoPathSpecified Then
                                        'run task
                                        Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_Movie(mDBElement, GenericSubEventProgressAsync, GenericEventProcess))
                                        If Result IsNot Nothing Then
                                            If Not Result.AlreadyInSync Then
                                                mDBElement.MainDetails.LastPlayed = Result.LastPlayed
                                                mDBElement.MainDetails.PlayCount = Result.PlayCount
                                            End If
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            _Logger.Warn(String.Format("[KodiInterface] [GenericRunCallBack]: Hostname ({0}) not found in host list!", _SpecialSettings.GetWatchedStateHost))
                        End If
                    End If

                'Before Edit TVEpisode / Scraper Multi TVEpisode / Scraper Single TVEpisode
                Case Enums.AddonEventType.BeforeEdit_TVEpisode, Enums.AddonEventType.ScraperMulti_TVEpisode, Enums.AddonEventType.ScraperSingle_TVEpisode
                    If mDBElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                        mHost = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(mDBElement, True) Then
                                    If mDBElement.NfoPathSpecified Then
                                        'run task
                                        Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_TVEpisode(mDBElement, GenericSubEventProgressAsync, GenericEventProcess))
                                        If Result IsNot Nothing Then
                                            If Not Result.AlreadyInSync Then
                                                mDBElement.MainDetails.LastPlayed = Result.LastPlayed
                                                mDBElement.MainDetails.PlayCount = Result.PlayCount
                                            End If
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            _Logger.Warn(String.Format("[KodiInterface] [GenericRunCallBack]: Hostname ({0}) not found in host list!", _SpecialSettings.GetWatchedStateHost))
                        End If
                    End If

                Case Enums.AddonEventType.ScraperMulti_TVSeason, Enums.AddonEventType.ScraperMulti_TVShow, Enums.AddonEventType.ScraperSingle_TVSeason, Enums.AddonEventType.ScraperSingle_TVShow
                    If mDBElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                        mHost = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                If mDBElement.Episodes IsNot Nothing Then
                                    For Each tEpisode In mDBElement.Episodes.Where(Function(f) f.FileItemSpecified)
                                        If tEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(tEpisode, True) Then
                                            If tEpisode.NfoPathSpecified Then
                                                'run task
                                                Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_TVEpisode(tEpisode, GenericSubEventProgressAsync, GenericEventProcess))
                                                If Result IsNot Nothing Then
                                                    If Not Result.AlreadyInSync Then
                                                        tEpisode.MainDetails.LastPlayed = Result.LastPlayed
                                                        tEpisode.MainDetails.PlayCount = Result.PlayCount
                                                    End If
                                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", tEpisode.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                                Else
                                                    _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", tEpisode.MainDetails.Title))
                                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", tEpisode.MainDetails.Title), Nothing}))
                                                    getError = True
                                                End If
                                            Else
                                                _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                                'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                                getError = True
                                            End If
                                        Else
                                            _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                            getError = True
                                        End If
                                    Next
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            _Logger.Warn(String.Format("[KodiInterface] [GenericRunCallBack]: Hostname ({0}) not found in host list!", _SpecialSettings.GetWatchedStateHost))
                        End If
                    End If

                'Remove Movie
                Case Enums.AddonEventType.Remove_Movie
                    If mDBElement.FileItemSpecified Then
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                'run task
                                If Await Task.Run(Function() _APIKodi.Remove_Movie(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                Else
                                    _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.MainDetails.Title))
                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            For Each tHost As Host In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.Movie).Count > 0)
                                Dim _APIKodi As New Kodi.APIKodi(tHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.Remove_Movie(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        _Logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.MainDetails.Title))
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Next
                        End If
                    Else
                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: No file name specified")
                    End If

                'Remove TVEpisode
                Case Enums.AddonEventType.Remove_TVEpisode
                    If mDBElement.FileItemSpecified Then

                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                'run task
                                If Await Task.Run(Function() _APIKodi.Remove_TVEpisode(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                Else
                                    _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.MainDetails.Title))
                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                Dim _APIKodi As New Kodi.APIKodi(tHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.Remove_TVEpisode(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        _Logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.MainDetails.Title))
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Next
                        End If
                    Else
                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: No file name specified")
                    End If

                    'Remove TVShow
                Case Enums.AddonEventType.Remove_TVShow
                    If mDBElement.ShowPathSpecified Then
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                'run task
                                If Await Task.Run(Function() _APIKodi.Remove_TVShow(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                Else
                                    _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.MainDetails.Title))
                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                Dim _APIKodi As New Kodi.APIKodi(tHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.Remove_TVShow(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        _Logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.MainDetails.Title))
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Next
                        End If
                    Else
                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: No tvshow path specified")
                    End If

                'Sync Movie
                Case Enums.AddonEventType.Sync_Movie
                    If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(mDBElement, True) Then
                        If mDBElement.NfoPathSpecified Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_Movie(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Else
                                For Each tHost As Host In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.Movie).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)

                                    'connection test
                                    If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                        'run task
                                        If Await Task.Run(Function() _APIKodi.UpdateInfo_Movie(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            _Logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        getError = True
                                    End If
                                Next
                            End If
                        Else
                            _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                            getError = True
                        End If
                    Else
                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                        getError = True
                    End If

                    'Sync MovieSet
                Case Enums.AddonEventType.Sync_MovieSet
                    If mDBElement.MoviesInSetSpecified Then
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                'run task
                                If Await Task.Run(Function() _APIKodi.UpdateInfo_MovieSet(mDBElement, _SpecialSettings.SendNotifications)) Then
                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                Else
                                    _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso Not String.IsNullOrEmpty(f.MovieSetArtworksPath))
                                Dim _APIKodi As New Kodi.APIKodi(tHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_MovieSet(mDBElement, _SpecialSettings.SendNotifications)) Then
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        _Logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Next
                        End If
                    Else
                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: No movies in set!")
                        getError = True
                    End If

                    'Sync TVEpisode
                Case Enums.AddonEventType.Sync_TVEpisode
                    If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(mDBElement, True) Then
                        If mDBElement.NfoPathSpecified Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_TVEpisode(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)

                                    'connection test
                                    If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                        'run task
                                        If Await Task.Run(Function() _APIKodi.UpdateInfo_TVEpisode(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            _Logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        getError = True
                                    End If
                                Next
                            End If
                        Else
                            _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                            getError = True
                        End If
                    Else
                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                        getError = True
                    End If

                    'Sync TVSeason
                Case Enums.AddonEventType.Sync_TVSeason
                    If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(mDBElement, True) Then
                        If mDBElement.IDSpecified Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_TVSeason(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)

                                    'connection test
                                    If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                        'run task
                                        If Await Task.Run(Function() _APIKodi.UpdateInfo_TVSeason(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            _Logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        getError = True
                                    End If
                                Next
                            End If
                        Else
                            _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                            getError = True
                        End If
                    Else
                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                        getError = True
                    End If

                    'Sync TVShow
                Case Enums.AddonEventType.Sync_TVShow
                    If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(mDBElement, True) Then
                        If mDBElement.NfoPathSpecified Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_TVShow(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        _Logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)

                                    'connection test
                                    If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                        'run task
                                        If Await Task.Run(Function() _APIKodi.UpdateInfo_TVShow(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            _Logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MainDetails.Title))
                                            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MainDetails.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        getError = True
                                    End If
                                Next
                            End If
                        Else
                            _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                            getError = True
                        End If
                    Else
                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                        getError = True
                    End If

                    'General Tasks
                Case Enums.AddonEventType.Task
                    Select Case mInternalType

                        'Get Playcount
                        Case InternalType.GetPlaycount
                            If mDBElement IsNot Nothing AndAlso mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    Select Case mDBElement.ContentType

                                        'Get Movie Playcount
                                        Case Enums.ContentType.Movie
                                            If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(mDBElement, True) Then
                                                If mDBElement.NfoPathSpecified Then
                                                    'run task
                                                    Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_Movie(mDBElement, GenericSubEventProgressAsync, GenericEventProcess))
                                                    If Result IsNot Nothing Then
                                                        If Not Result.AlreadyInSync Then
                                                            mDBElement.MainDetails.LastPlayed = Result.LastPlayed
                                                            mDBElement.MainDetails.PlayCount = Result.PlayCount
                                                            Master.DB.Save_Movie(mDBElement, False, True, False, True, False)
                                                            RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_Movie, New List(Of Object)(New Object() {mDBElement.ID}))
                                                        End If
                                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                                    End If
                                                Else
                                                    _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                                    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                                    getError = True
                                                End If
                                            Else
                                                _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                                getError = True
                                            End If

                                        'Get TVEpisode Playcount
                                        Case Enums.ContentType.TVEpisode
                                            If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(mDBElement, True) Then
                                                If mDBElement.NfoPathSpecified Then
                                                    'run task
                                                    Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_TVEpisode(mDBElement, GenericSubEventProgressAsync, GenericEventProcess))
                                                    If Result IsNot Nothing Then
                                                        If Not Result.AlreadyInSync Then
                                                            mDBElement.MainDetails.LastPlayed = Result.LastPlayed
                                                            mDBElement.MainDetails.PlayCount = Result.PlayCount
                                                            Master.DB.Save_TVEpisode(mDBElement, False, True, False, False, True)
                                                            RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {mDBElement.ID}))
                                                        End If
                                                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                                    End If
                                                Else
                                                    _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                                    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                                    getError = True
                                                End If
                                            Else
                                                _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                                getError = True
                                            End If

                                        'Get TVSeason / TVShow Playcount
                                        Case Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                                            If mDBElement.Episodes IsNot Nothing Then
                                                For Each tEpisode In mDBElement.Episodes
                                                    If tEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(tEpisode, True) Then
                                                        If tEpisode.NfoPathSpecified Then
                                                            'run task
                                                            Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_TVEpisode(tEpisode, GenericSubEventProgressAsync, GenericEventProcess))
                                                            If Result IsNot Nothing Then
                                                                If Not Result.AlreadyInSync Then
                                                                    tEpisode.MainDetails.LastPlayed = Result.LastPlayed
                                                                    tEpisode.MainDetails.PlayCount = Result.PlayCount
                                                                    Master.DB.Save_TVEpisode(tEpisode, False, True, False, False, True)
                                                                    RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {tEpisode.ID}))
                                                                End If
                                                                AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", tEpisode.MainDetails.Title), New Bitmap(My.Resources.logo)}))
                                                            End If
                                                        Else
                                                            _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                                            getError = True
                                                        End If
                                                    Else
                                                        _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                                        getError = True
                                                    End If
                                                Next
                                            End If
                                    End Select
                                Else
                                    getError = True
                                End If
                            End If

                            'Get WatchedState of all movies
                        Case InternalType.GetPlaycount_AllMovies
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_AllMovies(GenericSubEventProgressAsync, GenericEventProcess))
                                    If Result IsNot Nothing AndAlso Result.movies IsNot Nothing AndAlso Result.movies.Count > 0 Then
                                        Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                                            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                                SQLNewcommand.CommandText = "SELECT idMovie, MoviePath, PlayCount, iLastPlayed, Title FROM movielist ORDER BY Title ASC;"
                                                Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                                                    If SQLreader.HasRows Then
                                                        While SQLreader.Read()
                                                            Try
                                                                Dim nMovieToSync = Result.movies.FirstOrDefault(Function(f) f.file = _APIKodi.GetRemotePath(SQLreader("MoviePath").ToString))
                                                                If nMovieToSync IsNot Nothing Then
                                                                    Dim intLastPlayed As String = Nothing
                                                                    Dim intPlaycount As Integer = 0
                                                                    If Not DBNull.Value.Equals(SQLreader("iLastPlayed")) Then intLastPlayed = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("iLastPlayed"))).ToString("yyyy-MM-dd HH:mm:ss")
                                                                    If Not DBNull.Value.Equals(SQLreader("PlayCount")) Then intPlaycount = Convert.ToInt32(SQLreader("PlayCount"))
                                                                    If Not intPlaycount = nMovieToSync.playcount OrElse Not intLastPlayed = nMovieToSync.lastplayed Then
                                                                        Dim nDBElement = Master.DB.Load_Movie(Convert.ToInt64(SQLreader("idMovie")))
                                                                        nDBElement.MainDetails.LastPlayed = nMovieToSync.lastplayed
                                                                        nDBElement.MainDetails.PlayCount = nMovieToSync.playcount
                                                                        Master.DB.Save_Movie(nDBElement, True, True, False, True, False)
                                                                        RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_Movie, New List(Of Object)(New Object() {nDBElement.ID}))
                                                                        _Logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_AllMovies: ""{1}"" | Synced to Ember", mHost.Label, SQLreader("Title").ToString))
                                                                    Else
                                                                        _Logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_AllMovies: ""{1}"" | Nothing to sync", mHost.Label, SQLreader("Title").ToString))
                                                                    End If
                                                                End If
                                                            Catch ex As Exception
                                                                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                                                            End Try
                                                        End While
                                                    End If
                                                End Using
                                            End Using
                                            SQLTransaction.Commit()
                                        End Using
                                    End If
                                Else
                                    _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                    getError = True
                                End If
                            End If

                            'Get WatchedState of all episodes
                        Case InternalType.GetPlaycount_AllTVEpisodes
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    'run task
                                    Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_AllTVEpisodes(GenericSubEventProgressAsync, GenericEventProcess))
                                    If Result IsNot Nothing AndAlso Result.episodes IsNot Nothing AndAlso Result.episodes.Count > 0 Then
                                        Using SQLTransaction As SQLite.SQLiteTransaction = Master.DB.MyVideosDBConn.BeginTransaction()
                                            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                                                SQLNewcommand.CommandText = "SELECT idEpisode, strFilePath, PlayCount, iLastPlayed, Title FROM episodelist WHERE Missing=0 ORDER BY idShow ASC;"
                                                Using SQLreader As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                                                    If SQLreader.HasRows Then
                                                        Dim queShowID As New Queue(Of Long)
                                                        While SQLreader.Read()
                                                            Try
                                                                Dim nEpisodeToSync = Result.episodes.FirstOrDefault(Function(f) f.file = _APIKodi.GetRemotePath(SQLreader("strFilePath").ToString))
                                                                If nEpisodeToSync IsNot Nothing Then
                                                                    Dim intLastPlayed As String = Nothing
                                                                    Dim intPlaycount As Integer = 0
                                                                    If Not DBNull.Value.Equals(SQLreader("iLastPlayed")) Then intLastPlayed = Functions.ConvertFromUnixTimestamp(Convert.ToInt64(SQLreader("iLastPlayed"))).ToString("yyyy-MM-dd HH:mm:ss")
                                                                    If Not DBNull.Value.Equals(SQLreader("PlayCount")) Then intPlaycount = Convert.ToInt32(SQLreader("PlayCount"))
                                                                    If Not intPlaycount = nEpisodeToSync.playcount OrElse Not intLastPlayed = nEpisodeToSync.lastplayed Then
                                                                        Dim nDBElement = Master.DB.Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), True)
                                                                        If Not queShowID.Contains(nDBElement.ShowID) Then queShowID.Enqueue(nDBElement.ShowID)
                                                                        nDBElement.MainDetails.LastPlayed = nEpisodeToSync.lastplayed
                                                                        nDBElement.MainDetails.PlayCount = nEpisodeToSync.playcount
                                                                        Master.DB.Save_TVEpisode(nDBElement, True, True, False, False, True, False)
                                                                        RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {nDBElement.ID}))
                                                                        _Logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_AllTVEpisodes: ""{1}"" | Synced to Ember", mHost.Label, SQLreader("Title").ToString))
                                                                    Else
                                                                        _Logger.Trace(String.Format("[APIKodi] [{0}] GetPlaycount_AllTVEpisodes: ""{1}"" | Nothing to sync", mHost.Label, SQLreader("Title").ToString))
                                                                    End If
                                                                    If queShowID.Count > 1 Then
                                                                        Dim intShowID = queShowID.Dequeue
                                                                        RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {intShowID}))
                                                                    End If
                                                                End If
                                                            Catch ex As Exception
                                                                _Logger.Error(ex, New StackFrame().GetMethod().Name)
                                                            End Try
                                                        End While
                                                        If queShowID.Count > 0 Then
                                                            Dim intShowID = queShowID.Dequeue
                                                            RaiseEvent GenericEvent(Enums.AddonEventType.AfterEdit_TVShow, New List(Of Object)(New Object() {intShowID}))
                                                        End If
                                                    End If
                                                End Using
                                            End Using
                                            SQLTransaction.Commit()
                                        End Using
                                    End If
                                Else
                                    _Logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                    getError = True
                                End If
                            End If

                            'Clean Video Library
                        Case InternalType.VideoLibrary_Clean
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)
                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    Await _APIKodi.VideoLibrary_Clean.ConfigureAwait(False)
                                    While Await _APIKodi.Host_IsScanningVideo()
                                        Threading.Thread.Sleep(1000)
                                    End While
                                Else
                                    getError = True
                                End If
                            End If

                            'Update Video Library
                        Case InternalType.VideoLibrary_Update
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)
                                'connection test
                                If Await Task.Run(Function() _APIKodi.GetConnectionToHost) Then
                                    Await _APIKodi.VideoLibrary_Scan.ConfigureAwait(False)
                                    While Await _APIKodi.Host_IsScanningVideo()
                                        Threading.Thread.Sleep(1000)
                                    End While
                                Else
                                    getError = True
                                End If
                            End If
                    End Select
            End Select
        Else
            _Logger.Warn("[KodiInterface] [GenericRunCallBack]: No Host Configured!")
            getError = True
        End If

        If Not getError Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Load module settings
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Used at module startup(=Ember startup) to load Kodi Hosts and also set other module settings
    Sub Settings_Load()
        _SpecialSettings = New SpecialSettings
        If File.Exists(_xmlSettingsPath) Then
            Dim xmlSer As XmlSerializer = Nothing
            Using xmlSR As StreamReader = New StreamReader(_xmlSettingsPath)
                xmlSer = New XmlSerializer(GetType(SpecialSettings))
                _SpecialSettings = DirectCast(xmlSer.Deserialize(xmlSR), SpecialSettings)
            End Using
        End If
    End Sub

    Private Sub CreateContextMenu(ByRef tMenu As ToolStripMenuItem, ByVal tContentType As Enums.ContentType)
        'Single Host
        If _SpecialSettings.Hosts IsNot Nothing AndAlso _SpecialSettings.Hosts.Count = 1 Then
            Dim mnuHostSyncItem As New ToolStripMenuItem
            mnuHostSyncItem.Image = New Bitmap(My.Resources.menuSync)
            mnuHostSyncItem.Tag = _SpecialSettings.Hosts(0)
            mnuHostSyncItem.Text = Master.eLang.GetString(1446, "Sync")
            Select Case tContentType
                Case Enums.ContentType.Movie
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_Movie_Click
                Case Enums.ContentType.Movieset
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_MovieSet_Click
                Case Enums.ContentType.TVEpisode
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVEpisode_Click
                Case Enums.ContentType.TVSeason
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVSeason_Click
                Case Enums.ContentType.TVShow
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVShow_Click
            End Select
            tMenu.DropDownItems.Add(mnuHostSyncItem)
            If tContentType = Enums.ContentType.TVSeason OrElse tContentType = Enums.ContentType.TVShow Then
                Dim mnuHostSyncFullItem As New ToolStripMenuItem
                mnuHostSyncFullItem.Image = New Bitmap(My.Resources.menuSync)
                mnuHostSyncFullItem.Tag = _SpecialSettings.Hosts(0)
                mnuHostSyncFullItem.Text = Master.eLang.GetString(1008, "Sync Full")
                Select Case tContentType
                    Case Enums.ContentType.TVSeason
                        AddHandler mnuHostSyncFullItem.Click, AddressOf cmnuHostSyncFullItem_TVSeason_Click
                    Case Enums.ContentType.TVShow
                        AddHandler mnuHostSyncFullItem.Click, AddressOf cmnuHostSyncFullItem_TVShow_Click
                End Select
                tMenu.DropDownItems.Add(mnuHostSyncFullItem)
            End If
            If tContentType = Enums.ContentType.Movie OrElse tContentType = Enums.ContentType.TVEpisode OrElse tContentType = Enums.ContentType.TVSeason OrElse tContentType = Enums.ContentType.TVShow Then
                If _SpecialSettings.GetWatchedState AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                    Dim mHost As Host = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                    If mHost IsNot Nothing Then
                        Dim mnuHostGetPlaycount As New ToolStripMenuItem
                        mnuHostGetPlaycount.Image = New Bitmap(My.Resources.menuWatchedState)
                        mnuHostGetPlaycount.Tag = mHost
                        mnuHostGetPlaycount.Text = Master.eLang.GetString(1070, "Get Watched State")
                        Select Case tContentType
                            Case Enums.ContentType.Movie
                                AddHandler mnuHostGetPlaycount.Click, AddressOf cmnuHostGetPlaycount_Movie_Click
                            Case Enums.ContentType.TVEpisode
                                AddHandler mnuHostGetPlaycount.Click, AddressOf cmnuHostGetPlaycount_TVEpisode_Click
                            Case Enums.ContentType.TVSeason
                                AddHandler mnuHostGetPlaycount.Click, AddressOf cmnuHostGetPlaycount_TVSeason_Click
                            Case Enums.ContentType.TVShow
                                AddHandler mnuHostGetPlaycount.Click, AddressOf cmnuHostGetPlaycount_TVShow_Click
                        End Select
                        tMenu.DropDownItems.Add(mnuHostGetPlaycount)
                    End If
                End If
            End If
            If tContentType = Enums.ContentType.Movie OrElse tContentType = Enums.ContentType.TVEpisode OrElse tContentType = Enums.ContentType.TVShow Then
                Dim mnuHostRemoveItem As New ToolStripMenuItem
                mnuHostRemoveItem.Image = New Bitmap(My.Resources.menuRemove)
                mnuHostRemoveItem.Tag = _SpecialSettings.Hosts(0)
                mnuHostRemoveItem.Text = Master.eLang.GetString(30, "Remove")
                Select Case tContentType
                    Case Enums.ContentType.Movie
                        AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_Movie_Click
                    Case Enums.ContentType.TVEpisode
                        AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_TVEpisode_Click
                    Case Enums.ContentType.TVShow
                        AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_TVShow_Click
                End Select
                tMenu.DropDownItems.Add(mnuHostRemoveItem)
            End If

            'Multiple Hosts
        ElseIf _SpecialSettings.Hosts IsNot Nothing AndAlso _SpecialSettings.Hosts.Count > 1 Then
            For Each kHost As Host In _SpecialSettings.Hosts
                Dim mnuHost As New ToolStripMenuItem
                mnuHost.Image = New Bitmap(My.Resources.icon)
                mnuHost.Text = kHost.Label
                Dim mnuHostSyncItem As New ToolStripMenuItem
                mnuHostSyncItem.Image = New Bitmap(My.Resources.menuSync)
                mnuHostSyncItem.Tag = kHost
                mnuHostSyncItem.Text = Master.eLang.GetString(1446, "Sync")
                Select Case tContentType
                    Case Enums.ContentType.Movie
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_Movie_Click
                    Case Enums.ContentType.Movieset
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_MovieSet_Click
                    Case Enums.ContentType.TVEpisode
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVEpisode_Click
                    Case Enums.ContentType.TVSeason
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVSeason_Click
                    Case Enums.ContentType.TVShow
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVShow_Click
                End Select
                mnuHost.DropDownItems.Add(mnuHostSyncItem)
                If tContentType = Enums.ContentType.TVSeason OrElse tContentType = Enums.ContentType.TVShow Then
                    Dim mnuHostSyncFullItem As New ToolStripMenuItem
                    mnuHostSyncFullItem.Image = New Bitmap(My.Resources.menuSync)
                    mnuHostSyncFullItem.Tag = kHost
                    mnuHostSyncFullItem.Text = Master.eLang.GetString(1008, "Sync Full")
                    Select Case tContentType
                        Case Enums.ContentType.TVSeason
                            AddHandler mnuHostSyncFullItem.Click, AddressOf cmnuHostSyncFullItem_TVSeason_Click
                        Case Enums.ContentType.TVShow
                            AddHandler mnuHostSyncFullItem.Click, AddressOf cmnuHostSyncFullItem_TVShow_Click
                    End Select
                    mnuHost.DropDownItems.Add(mnuHostSyncFullItem)
                End If
                If tContentType = Enums.ContentType.Movie OrElse tContentType = Enums.ContentType.TVEpisode OrElse tContentType = Enums.ContentType.TVShow Then
                    Dim mnuHostRemoveItem As New ToolStripMenuItem
                    mnuHostRemoveItem.Image = New Bitmap(My.Resources.menuRemove)
                    mnuHostRemoveItem.Tag = kHost
                    mnuHostRemoveItem.Text = Master.eLang.GetString(30, "Remove")
                    Select Case tContentType
                        Case Enums.ContentType.Movie
                            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_Movie_Click
                        Case Enums.ContentType.TVEpisode
                            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_TVEpisode_Click
                        Case Enums.ContentType.TVShow
                            AddHandler mnuHostRemoveItem.Click, AddressOf cmnuHostRemoveItem_TVShow_Click
                    End Select
                    mnuHost.DropDownItems.Add(mnuHostRemoveItem)
                End If
                tMenu.DropDownItems.Add(mnuHost)
            Next
            If tContentType = Enums.ContentType.Movie OrElse tContentType = Enums.ContentType.TVEpisode OrElse tContentType = Enums.ContentType.TVSeason OrElse tContentType = Enums.ContentType.TVShow Then
                If _SpecialSettings.GetWatchedState AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                    Dim mHost As Host = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                    If mHost IsNot Nothing Then
                        Dim mnuHostGetPlaycount As New ToolStripMenuItem
                        mnuHostGetPlaycount.Image = New Bitmap(My.Resources.menuWatchedState)
                        mnuHostGetPlaycount.Tag = mHost
                        mnuHostGetPlaycount.Text = String.Format("{0} ({1})", Master.eLang.GetString(1070, "Get Watched State"), _SpecialSettings.GetWatchedStateHost)
                        Select Case tContentType
                            Case Enums.ContentType.Movie
                                AddHandler mnuHostGetPlaycount.Click, AddressOf cmnuHostGetPlaycount_Movie_Click
                            Case Enums.ContentType.TVEpisode
                                AddHandler mnuHostGetPlaycount.Click, AddressOf cmnuHostGetPlaycount_TVEpisode_Click
                            Case Enums.ContentType.TVSeason
                                AddHandler mnuHostGetPlaycount.Click, AddressOf cmnuHostGetPlaycount_TVSeason_Click
                            Case Enums.ContentType.TVShow
                                AddHandler mnuHostGetPlaycount.Click, AddressOf cmnuHostGetPlaycount_TVShow_Click
                        End Select
                        tMenu.DropDownItems.Add(mnuHostGetPlaycount)
                    End If
                End If
            End If
        Else
            Dim mnuDummy As New ToolStripMenuItem
            mnuDummy.Enabled = False
            mnuDummy.Text = Master.eLang.GetString(1447, "No Host Configured!")
            tMenu.DropDownItems.Add(mnuDummy)
        End If
    End Sub

    Private Sub CreateToolsMenu(ByRef tMenu As ToolStripMenuItem)
        If _SpecialSettings.Hosts IsNot Nothing AndAlso _SpecialSettings.Hosts.Count = 1 Then
            Dim mnuHostScanVideoLibrary As New ToolStripMenuItem
            mnuHostScanVideoLibrary.Image = New Bitmap(My.Resources.menuSync)
            mnuHostScanVideoLibrary.Tag = _SpecialSettings.Hosts(0)
            mnuHostScanVideoLibrary.Text = Master.eLang.GetString(82, "Update Library")
            AddHandler mnuHostScanVideoLibrary.Click, AddressOf mnuHostScanVideoLibrary_Click
            tMenu.DropDownItems.Add(mnuHostScanVideoLibrary)
            Dim mnuHostCleanVideoLibrary As New ToolStripMenuItem
            mnuHostCleanVideoLibrary.Image = New Bitmap(My.Resources.menuClean)
            mnuHostCleanVideoLibrary.Tag = _SpecialSettings.Hosts(0)
            mnuHostCleanVideoLibrary.Text = Master.eLang.GetString(709, "Clean Database")
            AddHandler mnuHostCleanVideoLibrary.Click, AddressOf mnuHostCleanVideoLibrary_Click
            tMenu.DropDownItems.Add(mnuHostCleanVideoLibrary)
            If _SpecialSettings.GetWatchedState AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                Dim mHost As Host = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                If mHost IsNot Nothing Then
                    tMenu.DropDownItems.Add(New ToolStripSeparator)

                    Dim mnuHostGetPlaycount_Movies As New ToolStripMenuItem
                    mnuHostGetPlaycount_Movies.Image = New Bitmap(My.Resources.menuWatchedState)
                    mnuHostGetPlaycount_Movies.Tag = mHost
                    mnuHostGetPlaycount_Movies.Text = String.Format("{0} - {1}",
                                                                    Master.eLang.GetString(1070, "Get Watched State"),
                                                                    Master.eLang.GetString(36, "Movies"))
                    AddHandler mnuHostGetPlaycount_Movies.Click, AddressOf mnuHostGetPlaycount_Movies_Click
                    tMenu.DropDownItems.Add(mnuHostGetPlaycount_Movies)

                    Dim mnuHostGetPlaycount_TVEpisodes As New ToolStripMenuItem
                    mnuHostGetPlaycount_TVEpisodes.Image = New Bitmap(My.Resources.menuWatchedState)
                    mnuHostGetPlaycount_TVEpisodes.Tag = mHost
                    mnuHostGetPlaycount_TVEpisodes.Text = String.Format("{0} - {1}",
                                                                    Master.eLang.GetString(1070, "Get Watched State"),
                                                                    Master.eLang.GetString(682, "Episodes"))
                    AddHandler mnuHostGetPlaycount_TVEpisodes.Click, AddressOf mnuHostGetPlaycount_TVEpisodes_Click
                    tMenu.DropDownItems.Add(mnuHostGetPlaycount_TVEpisodes)
                End If
            End If
        ElseIf _SpecialSettings.Hosts IsNot Nothing AndAlso _SpecialSettings.Hosts.Count > 1 Then
            For Each kHost As Host In _SpecialSettings.Hosts
                Dim mnuHost As New ToolStripMenuItem
                mnuHost.Image = New Bitmap(My.Resources.icon)
                mnuHost.Text = kHost.Label
                Dim mnuHostScanVideoLibrary As New ToolStripMenuItem
                mnuHostScanVideoLibrary.Image = New Bitmap(My.Resources.menuSync)
                mnuHostScanVideoLibrary.Tag = kHost
                mnuHostScanVideoLibrary.Text = Master.eLang.GetString(82, "Update Library")
                AddHandler mnuHostScanVideoLibrary.Click, AddressOf mnuHostScanVideoLibrary_Click
                mnuHost.DropDownItems.Add(mnuHostScanVideoLibrary)
                Dim mnuHostCleanVideoLibrary As New ToolStripMenuItem
                mnuHostCleanVideoLibrary.Image = New Bitmap(My.Resources.menuClean)
                mnuHostCleanVideoLibrary.Tag = kHost
                mnuHostCleanVideoLibrary.Text = Master.eLang.GetString(709, "Clean Database")
                AddHandler mnuHostCleanVideoLibrary.Click, AddressOf mnuHostCleanVideoLibrary_Click
                mnuHost.DropDownItems.Add(mnuHostCleanVideoLibrary)
                tMenu.DropDownItems.Add(mnuHost)
            Next
            If _SpecialSettings.GetWatchedState AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                Dim mHost As Host = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                If mHost IsNot Nothing Then
                    tMenu.DropDownItems.Add(New ToolStripSeparator)

                    Dim mnuHostGetPlaycount_Movies As New ToolStripMenuItem
                    mnuHostGetPlaycount_Movies.Image = New Bitmap(My.Resources.menuWatchedState)
                    mnuHostGetPlaycount_Movies.Tag = mHost
                    mnuHostGetPlaycount_Movies.Text = String.Format("{0} ({1}) - {2}",
                                                                    Master.eLang.GetString(1070, "Get Watched State"),
                                                                    _SpecialSettings.GetWatchedStateHost,
                                                                    Master.eLang.GetString(36, "Movies"))
                    AddHandler mnuHostGetPlaycount_Movies.Click, AddressOf mnuHostGetPlaycount_Movies_Click
                    tMenu.DropDownItems.Add(mnuHostGetPlaycount_Movies)

                    Dim mnuHostGetPlaycount_TVEpisodes As New ToolStripMenuItem
                    mnuHostGetPlaycount_TVEpisodes.Image = New Bitmap(My.Resources.menuWatchedState)
                    mnuHostGetPlaycount_TVEpisodes.Tag = mHost
                    mnuHostGetPlaycount_TVEpisodes.Text = String.Format("{0} ({1}) - {2}",
                                                                    Master.eLang.GetString(1070, "Get Watched State"),
                                                                    _SpecialSettings.GetWatchedStateHost,
                                                                    Master.eLang.GetString(682, "Episodes"))
                    AddHandler mnuHostGetPlaycount_TVEpisodes.Click, AddressOf mnuHostGetPlaycount_TVEpisodes_Click
                    tMenu.DropDownItems.Add(mnuHostGetPlaycount_TVEpisodes)
                End If
            End If
        Else
            Dim mnuDummy As New ToolStripMenuItem
            mnuDummy.Enabled = False
            mnuDummy.Text = Master.eLang.GetString(1447, "No Host Configured!")
            tMenu.DropDownItems.Add(mnuDummy)
        End If
    End Sub

    Private Sub PopulateMenus()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        _CnuMainToolsKodi.DropDownItems.Clear()
        _CnuMainToolsKodi.Image = New Bitmap(My.Resources.icon)
        _CnuMainToolsKodi.Text = "Kodi Interface"
        _CnuMainToolsKodi.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForMovieSets = True, .IfTabMovieSets = True, .ForTVShows = True, .IfTabTVShows = True}
        CreateToolsMenu(_CnuMainToolsKodi)
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolStripItem(tsi, _CnuMainToolsKodi)

        'mnuTrayTools
        _CnuTrayToolsKodi.DropDownItems.Clear()
        _CnuTrayToolsKodi.Image = New Bitmap(My.Resources.icon)
        _CnuTrayToolsKodi.Text = "Kodi Interface"
        CreateToolsMenu(_CnuTrayToolsKodi)
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolStripItem(tsi, _CnuTrayToolsKodi)

        'cmnuMovies
        _CmnuKodi_Movies.DropDownItems.Clear()
        _CmnuKodi_Movies.Image = New Bitmap(My.Resources.icon)
        _CmnuKodi_Movies.Text = "Kodi Interface"
        CreateContextMenu(_CmnuKodi_Movies, Enums.ContentType.Movie)
        AddToolStripItem_Movies(_CmnuSep_Movies)
        AddToolStripItem_Movies(_CmnuKodi_Movies)

        'cmnuMovieSets
        _CmnuKodi_MovieSets.DropDownItems.Clear()
        _CmnuKodi_MovieSets.Image = New Bitmap(My.Resources.icon)
        _CmnuKodi_MovieSets.Text = "Kodi Interface"
        CreateContextMenu(_CmnuKodi_MovieSets, Enums.ContentType.Movieset)
        AddToolStripItem_MovieSets(_CmnuSep_MovieSets)
        AddToolStripItem_MovieSets(_CmnuKodi_MovieSets)

        'cmnuTVEpisodes
        _CmnuKodi_TVEpisodes.DropDownItems.Clear()
        _CmnuKodi_TVEpisodes.Image = New Bitmap(My.Resources.icon)
        _CmnuKodi_TVEpisodes.Text = "Kodi Interface"
        CreateContextMenu(_CmnuKodi_TVEpisodes, Enums.ContentType.TVEpisode)
        AddToolStripItem_TVEpisodes(_CmnuSep_TVEpisodes)
        AddToolStripItem_TVEpisodes(_CmnuKodi_TVEpisodes)

        'cmnuTVSeasons
        _CmnuKodi_TVSeasons.DropDownItems.Clear()
        _CmnuKodi_TVSeasons.Image = New Bitmap(My.Resources.icon)
        _CmnuKodi_TVSeasons.Text = "Kodi Interface"
        CreateContextMenu(_CmnuKodi_TVSeasons, Enums.ContentType.TVSeason)
        AddToolStripItem_TVSeasons(_CmnuSep_TVSeasons)
        AddToolStripItem_TVSeasons(_CmnuKodi_TVSeasons)

        'cmnuTVShows
        _CmnuKodi_TVShows.DropDownItems.Clear()
        _CmnuKodi_TVShows.Image = New Bitmap(My.Resources.icon)
        _CmnuKodi_TVShows.Text = "Kodi Interface"
        CreateContextMenu(_CmnuKodi_TVShows, Enums.ContentType.TVShow)
        AddToolStripItem_TVShows(_CmnuSep_TVShows)
        AddToolStripItem_TVShows(_CmnuKodi_TVShows)

        'Task Manager
        _LblTaskManagerStatus.Text = "No Pending Tasks"
        _LblTaskManagerTitle.Text = "Kodi Interface Task Manager"
        Dim ts As ToolStrip = DirectCast(AddonsManager.Instance.RuntimeObjects.MainToolStrip, ToolStrip)
        AddToolStripItem(ts, _TssTaskManager1)
        AddToolStripItem(ts, _TssTaskManager2)
        AddToolStripItem(ts, _TspTaskManager)
        AddToolStripItem(ts, _TssTaskManager3)
        AddToolStripItem(ts, _TssTaskManager4)
        AddToolStripItem(ts, _LblTaskManagerStatus)
        AddToolStripItem(ts, _TssTaskManager5)
        AddToolStripItem(ts, _TssTaskManager6)
        AddToolStripItem(ts, _LblTaskManagerTitle)
        AddToolStripItem(ts, _TssTaskManager7)
        AddToolStripItem(ts, _TssTaskManager8)
    End Sub

    ''' <summary>
    ''' Actions on module startup (Ember startup) and runtime if module is enabled
    ''' </summary>
    ''' <remarks></remarks>
    Sub ToolsStripMenu_Enable()
        PopulateMenus()
    End Sub
    ''' <summary>
    '''  Actions on module startup (Ember startup) and runtime if module is disabled
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Used at module startup(=Ember startup) and during runtime to hide menu buttons of module in Ember
    Sub ToolsStripMenu_Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(_CnuMainToolsKodi)

        'cmnuTrayTools
        tsi = DirectCast(AddonsManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(_CnuTrayToolsKodi)

        'cmnuMovies
        RemoveToolStripItem_Movies(_CmnuSep_Movies)
        RemoveToolStripItem_Movies(_CmnuKodi_Movies)
        'cmnuEpisodes
        RemoveToolStripItem_TVEpisodes(_CmnuSep_TVEpisodes)
        RemoveToolStripItem_TVEpisodes(_CmnuKodi_TVEpisodes)
        'cmnuShows
        RemoveToolStripItem_TVShows(_CmnuSep_TVShows)
        RemoveToolStripItem_TVShows(_CmnuKodi_TVShows)
        'cmnuSeasons
        RemoveToolStripItem_TVSeasons(_CmnuSep_TVSeasons)
        RemoveToolStripItem_TVSeasons(_CmnuKodi_TVSeasons)
        'cmnuSets
        RemoveToolStripItem_MovieSets(_CmnuSep_MovieSets)
        RemoveToolStripItem_MovieSets(_CmnuKodi_MovieSets)

        'Task Manager
        Dim ts As ToolStrip = AddonsManager.Instance.RuntimeObjects.MainToolStrip
        ts.Items.Remove(_LblTaskManagerStatus)
        ts.Items.Remove(_LblTaskManagerTitle)
        ts.Items.Remove(_TspTaskManager)
        ts.Items.Remove(_TssTaskManager1)
        ts.Items.Remove(_TssTaskManager2)
        ts.Items.Remove(_TssTaskManager3)
        ts.Items.Remove(_TssTaskManager4)
        ts.Items.Remove(_TssTaskManager5)
        ts.Items.Remove(_TssTaskManager6)
    End Sub

    Sub Settings_Save()
        If Not File.Exists(_xmlSettingsPath) OrElse (Not CBool(File.GetAttributes(_xmlSettingsPath) And FileAttributes.ReadOnly)) Then
            If File.Exists(_xmlSettingsPath) Then
                Dim fAtt As FileAttributes = File.GetAttributes(_xmlSettingsPath)
                Try
                    File.SetAttributes(_xmlSettingsPath, FileAttributes.Normal)
                Catch ex As Exception
                    _Logger.Error(ex, New StackFrame().GetMethod().Name)
                End Try
            End If
            Using xmlSW As New StreamWriter(_xmlSettingsPath)
                Dim xmlSer As New XmlSerializer(GetType(SpecialSettings))
                xmlSer.Serialize(xmlSW, _SpecialSettings)
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Get movie playcount from Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get Movie Playcount"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuHostGetPlaycount_Movie_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim mHost As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If mHost IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = mHost, .mInternalType = InternalType.GetPlaycount, .mType = Enums.AddonEventType.Task})
                    Else
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Get episode playcount on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get TVEpisode Playcount"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuHostGetPlaycount_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mInternalType = InternalType.GetPlaycount, .mType = Enums.AddonEventType.Task})
                    Else
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Get episodes playcount for whole season on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get TVSeason Playcount"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Update details of season in Kodi DB
    ''' </remarks>
    Private Sub cmnuHostGetPlaycount_TVSeason_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    'add job to tasklist and get everything done
                    AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mInternalType = InternalType.GetPlaycount, .mType = Enums.AddonEventType.Task})
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Get episodes playcount for whole tv show on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get Tvshow Playcount"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuHostGetPlaycount_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(ID, True, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mInternalType = InternalType.GetPlaycount, .mType = Enums.AddonEventType.Task})
                    Else
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Remove movie details on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Remove Movie"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuHostRemoveItem_Movie_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
                'add job to tasklist and get everything done
                AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Remove_Movie})
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Remove tvepisode details on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Remove TVEpisode"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuHostRemoveItem_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
                'add job to tasklist and get everything done
                AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Remove_TVEpisode})
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Remove details of tvshow on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Remove Tvshow"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuHostRemoveItem_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)
                'add job to tasklist and get everything done
                AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Remove_TVShow})
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Update movie details on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Sync Movie"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Update details of movie in Kodi DB
    ''' </remarks>
    Private Sub cmnuHostSyncItem_Movie_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Sync_Movie})
                    Else
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Update movieset details on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Sync Movieset"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Update details of movieset in Kodi DB
    ''' </remarks>
    Private Sub cmnuHostSyncItem_MovieSet_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSet").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_Movieset(ID)
                If DBElement.MainDetails.TitleSpecified Then
                    'add job to tasklist and get everything done
                    AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Sync_MovieSet})
                Else
                    AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Update episode details on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Sync TVEpisode"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Update details of episode in Kodi DB
    ''' </remarks>
    Private Sub cmnuHostSyncItem_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Sync_TVEpisode})
                    Else
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Update season details on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Sync TVSeason"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Update details of season in Kodi DB
    ''' </remarks>
    Private Sub cmnuHostSyncItem_TVSeason_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    'add job to tasklist and get everything done
                    AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Sync_TVSeason})
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Update season and episodes details on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Sync TVSeason"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Update details of season in Kodi DB
    ''' </remarks>
    Private Sub cmnuHostSyncFullItem_TVSeason_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    'add job to tasklist and get everything done
                    AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Sync_TVSeason})
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Update details of tvshow on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Sync Tvshow"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Update details of tvshow in Kodi DB
    ''' </remarks>
    Private Sub cmnuHostSyncItem_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Sync_TVShow})
                    Else
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Update details of tvshow, seasons and episodes on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Sync Tvshow"</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Update details of tvshow in Kodi DB
    ''' </remarks>
    Private Sub cmnuHostSyncFullItem_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In AddonsManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(ID, True, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.AddonEventType.Sync_TVShow})
                    Else
                        AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    '''  Clean video library of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub mnuHostCleanVideoLibrary_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1450, "Cleaning Video Library..."), New Bitmap(My.Resources.logo)}))

            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mHost = Host, .mInternalType = InternalType.VideoLibrary_Clean, .mType = Enums.AddonEventType.Task})
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    '''  Get WatchedState of all movies of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub mnuHostGetPlaycount_Movies_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1303, "Sync Playcount") & "...", New Bitmap(My.Resources.logo)}))

            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mHost = Host, .mInternalType = InternalType.GetPlaycount_AllMovies, .mType = Enums.AddonEventType.Task})
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    '''  Get WatchedState of all tv shows of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub mnuHostGetPlaycount_TVEpisodes_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1303, "Sync Playcount") & "...", New Bitmap(My.Resources.logo)}))

            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mHost = Host, .mInternalType = InternalType.GetPlaycount_AllTVEpisodes, .mType = Enums.AddonEventType.Task})
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    '''  Scan video library of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub mnuHostScanVideoLibrary_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1448, "Updating Video Library..."), New Bitmap(My.Resources.logo)}))

            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mHost = Host, .mInternalType = InternalType.VideoLibrary_Update, .mType = Enums.AddonEventType.Task})
        Else
            AddonsManager.Instance.RunGeneric(Enums.AddonEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub

    Public Sub AddToolStripItem(control As ToolStripMenuItem, value As ToolStripItem)
        If control.Owner IsNot Nothing Then
            If control.Owner.InvokeRequired Then
                control.Owner.Invoke(New Delegate_AddToolStripMenuItem(AddressOf AddToolStripItem), New Object() {control, value})
            Else
                control.DropDownItems.Add(value)
            End If
        End If
    End Sub

    Public Sub AddToolStripItem(control As ToolStrip, value As ToolStripItem)
        If control.InvokeRequired Then
            control.Invoke(New Delegate_SetToolStripItem(AddressOf AddToolStripItem), New Object() {control, value})
        Else
            control.Items.Add(value)
        End If
    End Sub

    Public Sub ChangeTaskManagerStatus(control As ToolStripLabel, value As String)
        If control.Owner.InvokeRequired Then
            control.Owner.BeginInvoke(New Delegate_ChangeTaskManagerStatus(AddressOf ChangeTaskManagerStatus), New Object() {control, value})
        Else
            control.Text = value
        End If
    End Sub

    Public Sub ChangeTaskManagerProgressBar(control As ToolStripProgressBar, value As ProgressBarStyle)
        If control.Owner.InvokeRequired Then
            control.Owner.BeginInvoke(New Delegate_ChangeTaskManagerProgressBar(AddressOf ChangeTaskManagerProgressBar), New Object() {control, value})
        Else
            control.Style = value
        End If
    End Sub

    Public Sub RemoveToolStripItem_Movies(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_Movies), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_MovieSets(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_MovieSets), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVEpisodes(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVEpisodes), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVSeasons(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVSeasons), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolStripItem_TVShows(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_RemoveToolStripItem(AddressOf RemoveToolStripItem_TVShows), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Remove(value)
        End If
    End Sub

    Public Sub AddToolStripItem_Movies(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_Movies), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_MovieSets(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_MovieSets), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuMovieSetList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVEpisodes(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVEpisodes), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVEpisodeList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVSeasons(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVSeasons), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVSeasonList.Items.Add(value)
        End If
    End Sub

    Public Sub AddToolStripItem_TVShows(value As ToolStripItem)
        If AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.InvokeRequired Then
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Invoke(New Delegate_AddToolStripItem(AddressOf AddToolStripItem_TVShows), New Object() {value})
        Else
            AddonsManager.Instance.RuntimeObjects.ContextMenuTVShowList.Items.Add(value)
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Structure GenericEventCallBackAsync

        Dim tEventType As Enums.AddonEventType
        Dim tParams As List(Of Object)

    End Structure

    Public Structure GenericSubEventCallBackAsync

        Dim tGenericEventCallBackAsync As GenericEventCallBackAsync
        Dim tProgress As IProgress(Of GenericEventCallBackAsync)

    End Structure

    Private Enum InternalType
        None = 0
        GetPlaycount = 1
        GetPlaycount_AllMovies = 2
        GetPlaycount_AllTVEpisodes = 3
        VideoLibrary_Clean = 4
        VideoLibrary_Update = 5
    End Enum
    ''' <summary>
    ''' structure used to store Update Movie/TV/Movieset-Tasks for Kodi Interface
    ''' </summary>
    ''' <remarks></remarks>
    Private Structure KodiTask

        Dim mDBElement As Database.DBElement
        Dim mHost As Host
        Dim mInternalType As InternalType
        Dim mType As Enums.AddonEventType

    End Structure

    <Serializable()>
    <XmlRoot("interface.kodi")>
    Class SpecialSettings

#Region "Properties"

        <XmlElement("getwatchedstate")>
        Public Property GetWatchedState() As Boolean = False

        <XmlElement("getwatchedstatebeforeedit_movie")>
        Public Property GetWatchedStateBeforeEdit_Movie() As Boolean = False

        <XmlElement("getwatchedstatebeforeedit_tvepisode")>
        Public Property GetWatchedStateBeforeEdit_TVEpisode() As Boolean = False

        <XmlElement("getwatchedstatehost")>
        Public Property GetWatchedStateHost() As String = String.Empty

        <XmlElement("getwatchedstatescrapermulti_movie")>
        Public Property GetWatchedStateScraperMulti_Movie() As Boolean = False

        <XmlElement("getwatchedstatescrapermulti_tvepisode")>
        Public Property GetWatchedStateScraperMulti_TVEpisode() As Boolean = False

        <XmlElement("getwatchedstatescrapersingle_movie")>
        Public Property GetWatchedStateScraperSingle_Movie() As Boolean = False

        <XmlElement("getwatchedstatescrapersingle_tvepisode")>
        Public Property GetWatchedStateScraperSingle_TVEpisode() As Boolean = False

        <XmlElement("host")>
        Public Property Hosts() As List(Of Host) = New List(Of Host)

        <XmlElement("sendnotifications")>
        Public Property SendNotifications() As Boolean = False

#End Region 'Properties 

    End Class

    <Serializable()>
    Public Class Host

#Region "Properties"

        <XmlElement("label")>
        Public Property Label() As String = "New Host"

        <XmlElement("address")>
        Public Property Address() As String = "localhost"

        <XmlElement("port")>
        Public Property Port() As Integer = 8080

        <XmlElement("username")>
        Public Property Username() As String = "kodi"

        <XmlElement("password")>
        Public Property Password() As String = String.Empty

        <XmlElement("realtimesync")>
        Public Property RealTimeSync() As Boolean = False

        <XmlElement("moviesetartworkspath")>
        Public Property MovieSetArtworksPath() As String = String.Empty

        <XmlElement("source")>
        Public Property Sources() As List(Of Source) = New List(Of Source)

        <XmlIgnore>
        Public Property APIVersionInfo() As XBMCRPC.JSONRPC.VersionResponse = New XBMCRPC.JSONRPC.VersionResponse

#End Region 'Properties

    End Class


    <Serializable()>
    Public Class Source

#Region "Properties"

        <XmlElement("contenttype")>
        Public Property ContentType() As Enums.ContentType = Enums.ContentType.Movie

        <XmlElement("localpath")>
        Public Property LocalPath() As String = String.Empty

        <XmlElement("remotepath")>
        Public Property RemotePath() As String = String.Empty

#End Region 'Properties 

    End Class

#End Region 'Nested Types

End Class
