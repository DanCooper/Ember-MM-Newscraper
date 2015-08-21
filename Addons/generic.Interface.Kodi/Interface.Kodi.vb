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
Imports XBMCRPC
Imports NLog


Public Class KodiInterface
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolsStripItem(value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_AddToolsStripItem(tsi As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripMenuItem)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    'reflects the current host(s) settings/setup configured in settings, will be filled at module startup from XML settings (and is used to write changes of settings back into XML)
    Private MySettings As New _MySettings
    Private _AssemblyName As String = String.Empty
    Private _enabled As Boolean = False
    Private _Name As String = "Kodi"
    Private _setup As frmSettingsHolder
    Private cmnuKodi_Movies As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuKodi_Episodes As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuKodi_Shows As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuKodi_Sets As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuKodi_Seasons As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuSep_Movies As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_Episodes As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_Shows As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_Sets As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_Seasons As New System.Windows.Forms.ToolStripSeparator
    Private WithEvents cmnuKodiSync_Movie As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuKodiSync_TVEpisode As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuKodiSync_TVShow As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuKodiSync_TVSeason As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuKodiSync_Movieset As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuTrayToolsKodi As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuTrayToolsKodiCleanVideoLibrary As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents cmnuTrayToolsKodiScanVideoLibrary As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuMainToolsKodi As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuMainToolsKodiCleanVideoLibrary As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuMainToolsKodiScanVideoLibrary As New System.Windows.Forms.ToolStripMenuItem

    'structure used to store Update Movie/TV/Movieset-Tasks for Kodi Interface
    Private Structure KodiTask
        Dim mType As Enums.ModuleEventType
        Dim Params As List(Of Object)
        Dim Refparam As Object
        Dim dbelement As Database.DBElement
    End Structure
    'pool of Update tasks for KodiInterface (can be filled extremely fast when updating whole tvshow at once)
    Private TaskList As New List(Of KodiTask)
    'control variable: true=Ready to start tmrRunTasks-Timer and work through items of tasklist, false= Timer already tickting, executing tasks
    Private TasksDone As Boolean = True
    'Timer - on tick event work through items of tasklist
    Private tmrRunTasks As New Timer

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.GenericModule.GenericEvent
    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged
    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged
    Public Event SetupNeedsRestart() Implements EmberAPI.Interfaces.GenericModule.SetupNeedsRestart

#End Region 'Events

#Region "Properties"
    ''' <summary>
    ''' Subscribe to Eventtypes here
    ''' </summary>
    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Sync_Movie, _
                                                                                   Enums.ModuleEventType.Sync_MovieSet, _
                                                                                   Enums.ModuleEventType.Sync_TVEpisode, _
                                                                                   Enums.ModuleEventType.Sync_TVSeason, _
                                                                                   Enums.ModuleEventType.Sync_TVShow})
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
    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        'add job to tasklist and get everything done in Tick-Event thread of timer
        TaskList.Add(New KodiTask With {.mType = mType, .Params = _params, .Refparam = _singleobjekt, .dbelement = _dbelement})
        If TasksDone Then
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1443, "Start Syncing"), New Bitmap(My.Resources.logo)}))
            Me.tmrRunTasks.Start()
            Me.TasksDone = False
        End If
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    ''' <summary>
    ''' Tick event of timer object. Used to work through jobs of Tasklist
    ''' </summary>
    ''' <param name="sender">Timer tick event</param>
    ''' <param name="e">Timer tick event</param>
    ''' <remarks>
    ''' Worker function used to handle all ApiTaks in List(of KodiTask)
    ''' Made async to await async Kodi API
    ''' </remarks>
    Private Async Sub tmrRunTasks_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.tmrRunTasks.Enabled = False
        Me.TasksDone = False
        While Me.TaskList.Count > 0
            Await GenericRunCallBack(TaskList.Item(0).mType, TaskList.Item(0).Params, TaskList.Item(0).Refparam, TaskList.Item(0).dbelement)
            Me.TaskList.RemoveAt(0)
        End While
        Me.TasksDone = True
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1444, "Sync OK"), New Bitmap(My.Resources.logo)}))
    End Sub

    ''' <summary>
    ''' This is a generic callback function to handle all realtime-sync work for KODI-Api
    ''' </summary>
    ''' <param name="mType"></param>
    ''' <param name="_params"></param>
    ''' <remarks>
    ''' Worker function used to handle all ApiTaks in List(of KodiTask)
    ''' Made async to await async Kodi API
    ''' </remarks>
    Private Async Function GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByVal _params As List(Of Object), ByVal _refparam As Object, ByVal _dbelement As Database.DBElement) As Task(Of Boolean)
        'check if at least one host is configured, else skip
        Dim result As Boolean = False
        If MySettings.KodiHosts.host.ToList.Count > 0 Then
            Select Case mType

                'Movie syncing
                Case Enums.ModuleEventType.Sync_Movie
                    Dim tDBMovie As EmberAPI.Database.DBElement = DirectCast(_refparam, EmberAPI.Database.DBElement)
                    If tDBMovie.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(tDBMovie, True) Then
                        If Not String.IsNullOrEmpty(tDBMovie.NfoPath) Then
                            For Each host In MySettings.KodiHosts.host.ToList
                                'only update movie if realtimesync of host is enabled
                                If host.realtimesync Then
                                    'check if at least one source is type = movie, otherwise don't use this host!
                                    Dim Sourceok As Boolean = False
                                    For Each source In host.source
                                        If source.type = "movie" Then
                                            Sourceok = True
                                            Exit For
                                        End If
                                    Next
                                    If Sourceok = True Then
                                        Dim _APIKodi As New Kodi.APIKodi(host)
                                        'run task
                                        result = Await Task.Run(Function() _APIKodi.UpdateMovieInfo(tDBMovie.ID, MySettings.SendNotifications))
                                        'Update EmberDB Playcount value if configured by user
                                        If MySettings.SyncPlayCount AndAlso MySettings.SyncPlayCountHost = host.name Then
                                            result = Await Task.Run(Function() _APIKodi.SyncPlaycount(tDBMovie.ID, "movie"))
                                        End If
                                        'Synchronously waiting for an async method... not good and no ideal solution here. The asynchronous code of KodiAPI works best if it doesn’t get synchronously blocked - so for now I moved notifcation in Ember in async APIKodi to avoid waiting here for the task to finish. 
                                        'solution for now until Ember v1.5 (in future better use await and change all methods/functions to async code, all the way up in Ember (like msavazzi prepared)) 
                                        'TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                        'result.Wait()
                                        'If result.Result = True Then
                                        '    logger.Warn("[KodiInterface] RunGeneric MovieUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & Master.currMovie.Movie.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & Master.currMovie.Movie.Title, New Bitmap(My.Resources.logo)}))
                                        'Else
                                        '    logger.Warn("[KodiInterface] RunGeneric MovieUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & Master.currMovie.Movie.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", host.name & " | " & Master.eLang.GetString(9999, "Sync Failed") & ": " & Master.currMovie.Movie.Title, Nothing}))
                                        'End If
                                    End If
                                End If
                            Next
                        Else
                            logger.Warn("[KodiInterface] GenericRunCallBack MovieUpdate: " & Master.eLang.GetString(1442, "Please Scrape In Ember First!"))
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                        End If
                    Else
                        logger.Warn("[KodiInterface] GenericRunCallBack MovieUpdate: Not online!")
                    End If

                    'MovieSet syncing
                Case Enums.ModuleEventType.Sync_MovieSet
                    Dim tDBMovieset As EmberAPI.Database.DBElement = DirectCast(_refparam, EmberAPI.Database.DBElement)
                    If tDBMovieset.MovieList.Count > 0 Then
                        If Not String.IsNullOrEmpty(tDBMovieset.MovieSet.TMDB) Then
                            For Each host In MySettings.KodiHosts.host.ToList
                                'only update movie if realtimesync of host is enabled
                                If host.realtimesync Then
                                    'only update movie if moviesetpath of host is set
                                    If Not String.IsNullOrEmpty(host.moviesetpath) Then
                                        Dim _APIKodi As New Kodi.APIKodi(host)
                                        result = Await Task.Run(Function() _APIKodi.UpdateMovieSetInfo(tDBMovieset.ID, MySettings.SendNotifications))
                                        ''TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                        'If result.Result = True Then
                                        '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title, New Bitmap(My.Resources.logo)}))
                                        'Else
                                        '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title, Nothing}))
                                        'End If
                                    Else
                                        logger.Warn("[KodiInterface] GenericRunCallBack MoviesetUpdate: " & host.name & " | No Remote MoviesetPath configured!")
                                    End If
                                End If
                            Next
                        Else
                            logger.Warn("[KodiInterface] GenericRunCallBack MoviesetUpdate: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                        End If
                    Else
                        logger.Warn("[KodiInterface] GenericRunCallBack MoviesetUpdate: No movies in set!")
                    End If


                    'Episode syncing
                Case Enums.ModuleEventType.Sync_TVEpisode
                    Dim tDBTV As EmberAPI.Database.DBElement = DirectCast(_refparam, EmberAPI.Database.DBElement)
                    If tDBTV.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(tDBTV, True) Then
                        If Not String.IsNullOrEmpty(tDBTV.NfoPath) Then
                            For Each host In MySettings.KodiHosts.host.ToList
                                'only update movie if realtimesync of host is enabled
                                If host.realtimesync Then
                                    'check if at least one source is type = movie, otherwise don't use this host!
                                    Dim Sourceok As Boolean = False
                                    For Each source In host.source
                                        If source.type = "tvshow" Then
                                            Sourceok = True
                                            Exit For
                                        End If
                                    Next
                                    If Sourceok = True Then
                                        Dim _APIKodi As New Kodi.APIKodi(host)
                                        result = Await Task.Run(Function() _APIKodi.UpdateTVEpisodeInfo(tDBTV.ID, MySettings.SendNotifications))
                                        'Update EmberDB Playcount value if configured by user
                                        If MySettings.SyncPlayCount AndAlso MySettings.SyncPlayCountHost = host.name Then
                                            result = Await Task.Run(Function() _APIKodi.SyncPlaycount(tDBTV.ID, "movie"))
                                        End If
                                        ''TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                        'If result.Result = True Then
                                        '    logger.Warn("[KodiInterface] RunGeneric EpisodeUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVEp.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVEp.Title, New Bitmap(My.Resources.logo)}))
                                        'Else
                                        '    logger.Warn("[KodiInterface] RunGeneric EpisodeUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVEp.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVEp.Title, Nothing}))
                                        'End If
                                    End If
                                End If
                            Next
                        Else
                            logger.Warn("[KodiInterface] GenericRunCallBack EpisodeUpdate: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                        End If
                    Else
                        logger.Warn("[KodiInterface] GenericRunCallBack EpisodeUpdate: Not online!")
                    End If

                    'TVSeason syncing
                Case Enums.ModuleEventType.Sync_TVSeason
                    Dim tDBTV As EmberAPI.Database.DBElement = DirectCast(_refparam, EmberAPI.Database.DBElement)
                    If tDBTV.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(tDBTV, True) Then
                        If Not String.IsNullOrEmpty(tDBTV.ID.ToString) Then
                            For Each host In MySettings.KodiHosts.host.ToList
                                'only update movie if realtimesync of host is enabled
                                If host.realtimesync Then
                                    'check if at least one source is type = movie, otherwise don't use this host!
                                    Dim Sourceok As Boolean = False
                                    For Each source In host.source
                                        If source.type = "tvshow" Then
                                            Sourceok = True
                                            Exit For
                                        End If
                                    Next
                                    If Sourceok = True Then
                                        Dim _APIKodi As New Kodi.APIKodi(host)
                                        result = Await Task.Run(Function() _APIKodi.UpdateTVSeasonInfo(tDBTV.ID, MySettings.SendNotifications))
                                        ''TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                        'If result.Result = True Then
                                        '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title, New Bitmap(My.Resources.logo)}))
                                        'Else
                                        '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title, Nothing}))
                                        'End If
                                    End If
                                End If
                            Next
                        Else
                            logger.Warn("[KodiInterface] GenericRunCallBack TVSeasonUpdate: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                        End If
                    Else
                        logger.Warn("[KodiInterface] GenericRunCallBack TVSeasonUpdate: Not online!")
                    End If

                    'TVShow syncing
                Case Enums.ModuleEventType.Sync_TVShow
                    Dim tDBTV As EmberAPI.Database.DBElement = DirectCast(_refparam, EmberAPI.Database.DBElement)
                    If tDBTV.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(tDBTV, True) Then
                        If Not String.IsNullOrEmpty(tDBTV.NfoPath) Then
                            For Each host In MySettings.KodiHosts.host.ToList
                                'only update movie if realtimesync of host is enabled
                                If host.realtimesync Then
                                    'check if at least one source is type = movie, otherwise don't use this host!
                                    Dim Sourceok As Boolean = False
                                    For Each source In host.source
                                        If source.type = "tvshow" Then
                                            Sourceok = True
                                            Exit For
                                        End If
                                    Next
                                    If Sourceok = True Then
                                        Dim _APIKodi As New Kodi.APIKodi(host)
                                        result = Await Task.Run(Function() _APIKodi.UpdateTVShowInfo(tDBTV.ShowID, MySettings.SendNotifications))
                                        ''TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                        'If result.Result = True Then
                                        '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title, New Bitmap(My.Resources.logo)}))
                                        'Else
                                        '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title)
                                        '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title, Nothing}))
                                        'End If
                                    End If
                                End If
                            Next
                        Else
                            logger.Warn("[KodiInterface] GenericRunCallBack TVShowUpdate: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                        End If
                    Else
                        logger.Warn("[KodiInterface] GenericRunCallBack TVShowUpdate: Not online!")
                    End If
            End Select
        Else
            logger.Warn("[KodiInterface] GenericRunCallBack: No Host Configured!")
        End If
        Return result
    End Function

    ''' <summary>
    ''' Actions on module startup (Ember startup)
    ''' </summary>
    ''' <remarks>
    ''' - load module settings
    ''' - load XML configuration of hosts
    ''' 2015/06/26 Cocotus - First implementation, prepared by DanCooper
    ''' </remarks>
    Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub
    ''' <summary>
    ''' Load module settings
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Used at module startup(=Ember startup) to load Kodi Hosts and also set other module settings
    Sub LoadSettings()
        AddHandler tmrRunTasks.Tick, AddressOf tmrRunTasks_Tick
        MySettings.SendNotifications = clsAdvancedSettings.GetBooleanSetting("HostNotifications", False)
        MySettings.SyncPlayCount = clsAdvancedSettings.GetBooleanSetting("SyncPlayCount", False)
        MySettings.SyncPlayCountHost = clsAdvancedSettings.GetSetting("SyncPlayCountHost", "")
        'load XML configuration of hosts
        Dim hostsPath As String = FileUtils.Common.ReturnSettingsFile("Settings", "Hosts.xml")
        Dim tmphosts As New clsXMLHosts
        Dim xHosts As New Xml.Serialization.XmlSerializer(tmphosts.GetType)
        If IO.File.Exists(hostsPath) Then
            Dim objStreamReader As StreamReader
            objStreamReader = New StreamReader(hostsPath)
            MySettings.KodiHosts = CType(xHosts.Deserialize(objStreamReader), clsXMLHosts)
            objStreamReader.Close()
        Else
            'setting file is missing
            Dim hostsPathD As String = FileUtils.Common.ReturnSettingsFile("Defaults", "DefaultHosts.xml")
            Dim objStreamReader As StreamReader
            objStreamReader = New StreamReader(hostsPathD)
            MySettings.KodiHosts = CType(xHosts.Deserialize(objStreamReader), clsXMLHosts)
            objStreamReader.Close()
            Try
                File.Copy(hostsPathD, hostsPath)
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try
        End If
    End Sub
    ''' <summary>
    ''' Actions on module startup (Ember startup) and runtime if module is enabled
    ''' </summary>
    ''' <remarks></remarks>
    Sub Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        mnuMainToolsKodi.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsKodi.Text = "Kodi"
        mnuMainToolsKodi.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForMovieSets = True, .IfTabMovieSets = True, .ForTVShows = True, .IfTabTVShows = True}
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        mnuMainToolsKodiScanVideoLibrary.Image = New Bitmap(My.Resources.menuSync)
        mnuMainToolsKodiScanVideoLibrary.Text = Master.eLang.GetString(82, "Update Library")
        mnuMainToolsKodi.DropDownItems.Add(mnuMainToolsKodiScanVideoLibrary)
        mnuMainToolsKodiCleanVideoLibrary.Image = New Bitmap(My.Resources.menuSync)
        mnuMainToolsKodiCleanVideoLibrary.Text = Master.eLang.GetString(709, "Clean Database")
        mnuMainToolsKodi.DropDownItems.Add(mnuMainToolsKodiCleanVideoLibrary)
        AddToolsStripItem(tsi, mnuMainToolsKodi)

        'cmnuTrayTools
        cmnuTrayToolsKodi.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsKodi.Text = "Kodi"
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        cmnuTrayToolsKodiScanVideoLibrary.Image = New Bitmap(My.Resources.menuSync)
        cmnuTrayToolsKodiScanVideoLibrary.Text = Master.eLang.GetString(82, "Update Library")
        cmnuTrayToolsKodi.DropDownItems.Add(cmnuTrayToolsKodiScanVideoLibrary)
        cmnuTrayToolsKodiCleanVideoLibrary.Image = New Bitmap(My.Resources.menuSync)
        cmnuTrayToolsKodiCleanVideoLibrary.Text = Master.eLang.GetString(709, "Clean Database")
        cmnuTrayToolsKodi.DropDownItems.Add(cmnuTrayToolsKodiCleanVideoLibrary)
        AddToolsStripItem(tsi, cmnuTrayToolsKodi)

        'cmnuMovies
        cmnuKodi_Movies.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_Movies.Text = "Kodi"
        cmnuKodi_Movies.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        cmnuKodiSync_Movie.Image = New Bitmap(My.Resources.menuSync)
        cmnuKodiSync_Movie.Text = Master.eLang.GetString(1446, "Sync")
        cmnuKodi_Movies.DropDownItems.Add(cmnuKodiSync_Movie)

        SetToolsStripItem_Movies(cmnuSep_Movies)
        SetToolsStripItem_Movies(cmnuKodi_Movies)

        'cmnuEpisodes
        cmnuKodi_Episodes.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_Episodes.Text = "Kodi"
        cmnuKodi_Episodes.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        cmnuKodiSync_TVEpisode.Image = New Bitmap(My.Resources.menuSync)
        cmnuKodiSync_TVEpisode.Text = Master.eLang.GetString(1446, "Sync")
        cmnuKodi_Episodes.DropDownItems.Add(cmnuKodiSync_TVEpisode)

        SetToolsStripItem_TVEpisodes(cmnuSep_Episodes)
        SetToolsStripItem_TVEpisodes(cmnuKodi_Episodes)

        'cmnuShows
        cmnuKodi_Shows.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_Shows.Text = "Kodi"
        cmnuKodi_Shows.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        cmnuKodiSync_TVShow.Image = New Bitmap(My.Resources.menuSync)
        cmnuKodiSync_TVShow.Text = Master.eLang.GetString(1446, "Sync")
        cmnuKodi_Shows.DropDownItems.Add(cmnuKodiSync_TVShow)

        SetToolsStripItem_TVShows(cmnuSep_Shows)
        SetToolsStripItem_TVShows(cmnuKodi_Shows)

        'cmnuSeasons
        cmnuKodi_Seasons.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_Seasons.Text = "Kodi"
        cmnuKodi_Seasons.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        cmnuKodiSync_TVSeason.Image = New Bitmap(My.Resources.menuSync)
        cmnuKodiSync_TVSeason.Text = Master.eLang.GetString(1446, "Sync")
        cmnuKodi_Seasons.DropDownItems.Add(cmnuKodiSync_TVSeason)

        SetToolsStripItem_TVSeasons(cmnuSep_Seasons)
        SetToolsStripItem_TVSeasons(cmnuKodi_Seasons)

        'cmnuSets
        cmnuKodi_Sets.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_Sets.Text = "Kodi"
        cmnuKodi_Sets.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        cmnuKodiSync_Movieset.Image = New Bitmap(My.Resources.menuSync)
        cmnuKodiSync_Movieset.Text = Master.eLang.GetString(1446, "Sync")
        cmnuKodi_Sets.DropDownItems.Add(cmnuKodiSync_Movieset)

        SetToolsStripItem_MovieSets(cmnuSep_Sets)
        SetToolsStripItem_MovieSets(cmnuKodi_Sets)
    End Sub
    ''' <summary>
    '''  Actions on module startup (Ember startup) and runtime if module is disabled
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Used at module startup(=Ember startup) and during runtime to hide menu buttons of module in Ember
    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(mnuMainToolsKodi)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(cmnuTrayToolsKodi)

        'cmnuMovies
        RemoveToolsStripItem_Movies(cmnuSep_Movies)
        RemoveToolsStripItem_Movies(cmnuKodi_Movies)
        'cmnuEpisodes
        RemoveToolsStripItem_TVEpisodes(cmnuSep_Episodes)
        RemoveToolsStripItem_TVEpisodes(cmnuKodi_Episodes)
        'cmnuShows
        RemoveToolsStripItem_TVShows(cmnuSep_Shows)
        RemoveToolsStripItem_TVShows(cmnuKodi_Shows)
        'cmnuSeasons
        RemoveToolsStripItem_TVSeasons(cmnuSep_Seasons)
        RemoveToolsStripItem_TVSeasons(cmnuKodi_Seasons)
        'cmnuSets
        RemoveToolsStripItem_MovieSets(cmnuSep_Sets)
        RemoveToolsStripItem_MovieSets(cmnuKodi_Sets)
    End Sub
    ''' <summary>
    ''' Load and fill controls of settings page of module
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Triggered when user enters settings in Ember
    Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        Me._setup = New frmSettingsHolder
        Me._setup.chkEnabled.Checked = Me._enabled

        'consider user settings
        Me._setup.chkNotification.Checked = MySettings.SendNotifications
        Me._setup.chkPlayCount.Checked = MySettings.SyncPlayCount
        Me._setup.xmlHosts = MySettings.KodiHosts
        Me._setup.lbHosts.Items.Clear()
        If MySettings.SyncPlayCount = False Then
            Me._setup.cbPlayCountHost.Enabled = False
        Else
            Me._setup.cbPlayCountHost.Enabled = True
        End If
        Me._setup.cbPlayCountHost.Items.Clear()
        If Not MySettings.KodiHosts Is Nothing Then
            For Each host In MySettings.KodiHosts.host
                Me._setup.lbHosts.Items.Add(host.name)
                Me._setup.cbPlayCountHost.Items.Add(host.name)
            Next
        End If
        Me._setup.cbPlayCountHost.SelectedIndex = Me._setup.cbPlayCountHost.FindStringExact(MySettings.SyncPlayCountHost)
        SPanel.Name = Me._Name
        SPanel.Text = "Kodi Interface"
        SPanel.Prefix = "Kodi_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(Me._enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings()
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function
    ''' <summary>
    ''' Save module settings
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Triggered when user submit changes in settings
    ''' Besides module settings also XML host configuration will be saved/updated
    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Me.Enabled = Me._setup.chkEnabled.Checked
        MySettings.SendNotifications = Me._setup.chkNotification.Checked
        MySettings.SyncPlayCount = _setup.chkPlayCount.Checked
        MySettings.SyncPlayCountHost = If(Me._setup.cbPlayCountHost.SelectedItem IsNot Nothing, Me._setup.cbPlayCountHost.SelectedItem.ToString(), String.Empty)
        SaveSettings()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub
    ''' <summary>
    ''' Save module settings
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Triggered when user submit changes in settings
    ''' Besides module settings also XML host configuration will be saved/updated
    Sub SaveSettings()
        'module settings will be saved
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("HostNotifications", MySettings.SendNotifications)
            settings.SetBooleanSetting("SyncPlayCount", MySettings.SyncPlayCount)
            settings.SetSetting("SyncPlayCountHost", MySettings.SyncPlayCountHost)
        End Using
        'XML host configuration will be saved/updated
        Dim aPath As String = ""
        If Directory.Exists(String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar)) Then
            aPath = String.Concat(Functions.AppPath, "Settings", Path.DirectorySeparatorChar, "Hosts.xml")
        End If
        If Not String.IsNullOrEmpty(aPath) Then

            'get up to date host configuration/save host file
            MySettings.KodiHosts = _setup.xmlHosts
            Dim tmpKodiHosts As New clsXMLHosts
            tmpKodiHosts = _setup.xmlHosts
            Dim xmlSer As New Xml.Serialization.XmlSerializer(MySettings.KodiHosts.GetType)
            Using xmlSW As New StreamWriter(aPath)
                xmlSer.Serialize(xmlSW, MySettings.KodiHosts)
            End Using
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
    Private Async Sub cmnuKodiSync_Movie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuKodiSync_Movie.Click
        If MySettings.KodiHosts.host.ToList.Count > 0 Then
            Dim indX As Integer = ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(ModulesManager.Instance.RuntimeObjects.MediaListMovies.Item(0, indX).Value)
            Dim DBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID, False)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                Cursor.Current = Cursors.WaitCursor
                For Each host In MySettings.KodiHosts.host.ToList
                    If Not String.IsNullOrEmpty(DBElement.NfoPath) AndAlso Not String.IsNullOrEmpty(DBElement.Movie.ID) Then
                        'check if at least one source is type = movie, otherwise don't use this host!
                        Dim Sourceok As Boolean = False
                        For Each source In host.source
                            If source.type = "movie" Then
                                Sourceok = True
                                Exit For
                            End If
                        Next
                        If Sourceok = True Then
                            Dim _APIKodi As New Kodi.APIKodi(host)
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1443, "Start Syncing") & ": " & DBElement.Movie.Title, New Bitmap(My.Resources.logo)}))
                            If Await _APIKodi.UpdateMovieInfo(DBElement.ID, MySettings.SendNotifications) Then
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & DBElement.Movie.Title, New Bitmap(My.Resources.logo)}))
                            Else
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & DBElement.Movie.Title, Nothing}))
                            End If
                            'Update EmberDB Playcount value if configured by user
                            If MySettings.SyncPlayCount AndAlso MySettings.SyncPlayCountHost = host.name Then
                                If Await _APIKodi.SyncPlaycount(DBElement.ID, "movie") Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1452, "Playcount") & " " & Master.eLang.GetString(1444, "Sync OK") & ": " & DBElement.Movie.Title, New Bitmap(My.Resources.logo)}))
                                Else
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1452, "Playcount") & " " & Master.eLang.GetString(1445, "Sync Failed") & ": " & DBElement.Movie.Title, Nothing}))
                                End If
                            End If
                        End If
                    Else
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                Next
                Cursor.Current = Cursors.Default
            End If
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
    Private Async Sub cmnuKodiSync_Movieset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuKodiSync_Movieset.Click
        If MySettings.KodiHosts.host.ToList.Count > 0 Then
            Dim indX As Integer = ModulesManager.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(ModulesManager.Instance.RuntimeObjects.MediaListMovieSets.Item(0, indX).Value)
            Dim DBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID, False)
            If DBElement.MovieList.Count > 0 Then
                Cursor.Current = Cursors.WaitCursor
                If Not String.IsNullOrEmpty(DBElement.ID.ToString) Then
                    For Each host In MySettings.KodiHosts.host.ToList
                        'only update movie if moviesetpath of host is set
                        If Not String.IsNullOrEmpty(host.moviesetpath) Then
                            Dim _APIKodi As New Kodi.APIKodi(host)
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1443, "Start Syncing") & ": " & DBElement.MovieSet.Title, New Bitmap(My.Resources.logo)}))
                            If Await _APIKodi.UpdateMovieSetInfo(DBElement.ID, MySettings.SendNotifications) Then
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & DBElement.MovieSet.Title, New Bitmap(My.Resources.logo)}))
                            Else
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & DBElement.MovieSet.Title, Nothing}))
                            End If
                        Else
                            logger.Warn("[KodiInterface] cmnuKodiSync_Movieset_Click: " & host.name & " | No Remote MoviesetPath configured!")
                        End If
                    Next
                Else
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                End If
                Cursor.Current = Cursors.Default
            End If
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
    Private Async Sub cmnuKodiSync_TVEpisode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuKodiSync_TVEpisode.Click
        If MySettings.KodiHosts.host.ToList.Count > 0 Then
            Dim indX As Integer = ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes.Item(0, indX).Value)
            Dim DBElement As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(ID, True, False)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                Cursor.Current = Cursors.WaitCursor
                If Not String.IsNullOrEmpty(DBElement.NfoPath) AndAlso Not String.IsNullOrEmpty(DBElement.ID.ToString) Then
                    For Each host In MySettings.KodiHosts.host.ToList
                        'check if at least one source is type = tvshow, otherwise don't use this host!
                        Dim Sourceok As Boolean = False
                        For Each source In host.source
                            If source.type = "tvshow" Then
                                Sourceok = True
                                Exit For
                            End If
                        Next
                        If Sourceok = True Then
                            Dim _APIKodi As New Kodi.APIKodi(host)
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1443, "Start Syncing") & ": " & DBElement.TVEpisode.Title, New Bitmap(My.Resources.logo)}))
                            If Await _APIKodi.UpdateTVEpisodeInfo(DBElement.ID, MySettings.SendNotifications) Then
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & DBElement.TVEpisode.Title, New Bitmap(My.Resources.logo)}))
                            Else
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & DBElement.TVEpisode.Title, Nothing}))
                            End If
                            'Update EmberDB Playcount value if configured by user
                            If MySettings.SyncPlayCount AndAlso MySettings.SyncPlayCountHost = host.name Then
                                If Await _APIKodi.SyncPlaycount(DBElement.ID, "episode") Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1452, "Playcount") & " " & Master.eLang.GetString(1444, "Sync OK") & ": " & DBElement.TVEpisode.Title, New Bitmap(My.Resources.logo)}))
                                Else
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1452, "Playcount") & " " & Master.eLang.GetString(1445, "Sync Failed") & ": " & DBElement.TVEpisode.Title, Nothing}))
                                End If
                            End If
                        End If
                    Next
                Else
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                End If
                Cursor.Current = Cursors.Default
            End If
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
    Private Async Sub cmnuKodiSync_TVSeason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuKodiSync_TVSeason.Click
        If MySettings.KodiHosts.host.ToList.Count > 0 Then
            Dim indX As Integer = ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons.Item(0, indX).Value)
            Dim DBElement As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True, False)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                Cursor.Current = Cursors.WaitCursor
                If Not String.IsNullOrEmpty(DBElement.ID.ToString) Then
                    For Each host In MySettings.KodiHosts.host.ToList
                        'check if at least one source is type = tvshow, otherwise don't use this host!
                        Dim Sourceok As Boolean = False
                        For Each source In host.source
                            If source.type = "tvshow" Then
                                Sourceok = True
                                Exit For
                            End If
                        Next
                        If Sourceok = True Then
                            Dim _APIKodi As New Kodi.APIKodi(host)
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1443, "Start Syncing") & ": " & DBElement.TVSeason.Season, New Bitmap(My.Resources.logo)}))
                            If Await _APIKodi.UpdateTVSeasonInfo(DBElement.ID, MySettings.SendNotifications) Then
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & DBElement.TVSeason.Season, New Bitmap(My.Resources.logo)}))
                            Else
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & DBElement.TVSeason.Season, Nothing}))
                            End If
                        End If
                    Next
                Else
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                End If
                Cursor.Current = Cursors.Default
            End If
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
    Private Async Sub cmnuKodiSync_TVShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuKodiSync_TVShow.Click
        If MySettings.KodiHosts.host.ToList.Count > 0 Then
            Dim indX As Integer = ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows(0).Index
            Dim ID As Integer = Convert.ToInt32(ModulesManager.Instance.RuntimeObjects.MediaListTVShows.Item(0, indX).Value)
            Dim DBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False, False)
            If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                Cursor.Current = Cursors.WaitCursor
                If Not String.IsNullOrEmpty(DBElement.NfoPath) AndAlso Not String.IsNullOrEmpty(DBElement.TVShow.TVDB) Then
                    For Each host In MySettings.KodiHosts.host.ToList
                        'check if at least one source is type = tvshow, otherwise don't use this host!
                        Dim Sourceok As Boolean = False
                        For Each source In host.source
                            If source.type = "tvshow" Then
                                Sourceok = True
                                Exit For
                            End If
                        Next
                        If Sourceok = True Then
                            Dim _APIKodi As New Kodi.APIKodi(host)
                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1443, "Start Syncing") & ": " & DBElement.TVShow.Title, New Bitmap(My.Resources.logo)}))
                            If Await _APIKodi.UpdateTVShowInfo(DBElement.ShowID, MySettings.SendNotifications) Then
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & DBElement.TVShow.Title, New Bitmap(My.Resources.logo)}))
                            Else
                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & DBElement.TVShow.Title, Nothing}))
                            End If
                        End If
                    Next
                Else
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                End If
                Cursor.Current = Cursors.Default
            End If
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    '''  Clean video library of host
    ''' </summary>
    ''' <param name="sender">main Kodi menu "Clean Video Library"</param>
    ''' <remarks>
    ''' 2015/06/04 Cocotus - First implementation
    ''' </remarks>
    Private Async Sub mnuHostCleanVideoLibrary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsKodiCleanVideoLibrary.Click, cmnuTrayToolsKodiCleanVideoLibrary.Click
        If MySettings.KodiHosts.host.ToList.Count > 0 Then
            For Each host In MySettings.KodiHosts.host.ToList
                Dim _APIKodi As New Kodi.APIKodi(host)
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1450, "Cleaning Video Library..."), New Bitmap(My.Resources.logo)}))
                Dim response = Await _APIKodi.CleanVideoLibrary()
                If response = Nothing Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1451, "Cleaning Failed"), Nothing}))
                Else
                    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, "Kodi Interface", "Video library updated", Nothing}))
                End If
            Next
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    '''  Scan video library of host
    ''' </summary>
    ''' <param name="sender">main Kodi menu "Update Video Library"</param>
    ''' <remarks>
    ''' 2015/06/04 Cocotus - First implementation
    ''' </remarks>
    Private Async Sub mnuHostScanVideoLibrary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsKodiScanVideoLibrary.Click, cmnuTrayToolsKodiScanVideoLibrary.Click
        If MySettings.KodiHosts.host.ToList.Count > 0 Then
            For Each host In MySettings.KodiHosts.host.ToList
                Dim _APIKodi As New Kodi.APIKodi(host)
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1448, "Updating Video Library..."), New Bitmap(My.Resources.logo)}))
                Dim response = Await _APIKodi.ScanVideoLibrary()
                If response = Nothing Then
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1449, "Update Failed"), Nothing}))
                Else
                    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, "Kodi Interface", "Video library updated", Nothing}))
                End If
            Next
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    ''' Get all video sources configured in host
    ''' </summary>
    ''' <param name="kHost">specific host to query</param>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation
    ''' Called from dlgHost.vb when user hits "Populate" button to get host sources
    ''' </remarks>
    Public Shared Function GetSources(ByVal kHost As Host) As List(Of XBMCRPC.List.Items.SourcesItem)
        Dim listSources As New List(Of XBMCRPC.List.Items.SourcesItem)
        Try
            Dim _APIKodi As New Kodi.APIKodi(kHost)
            listSources = _APIKodi.GetSources(XBMCRPC.Files.Media.video).Result
            Return listSources
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return listSources
    End Function
    ''' <summary>
    ''' Get JSONRPC version of host
    ''' </summary>
    ''' <param name="kHost">specific host to query</param>
    ''' <remarks>
    ''' 2015/06/29 Cocotus - First implementation
    ''' </remarks>
    Public Shared Function GetJSONHostVersion(ByVal kHost As Host) As String
        Try
            Dim _APIKodi As New Kodi.APIKodi(kHost)
            Return _APIKodi.GetHostJSONVersion.Result
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
            Return ""
        End Try
    End Function

    Public Sub AddToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner IsNot Nothing Then
            If control.Owner.InvokeRequired Then
                control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
            Else
                control.DropDownItems.Add(value)
            End If
        End If
    End Sub

    Public Sub RemoveToolsStripItem_Movies(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuMovieList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuMovieList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_Movies), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuMovieList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem_MovieSets(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuMovieSetList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuMovieSetList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_MovieSets), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuMovieSetList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem_TVEpisodes(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_TVEpisodes), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem_TVSeasons(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuTVSeasonList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuTVSeasonList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_TVSeasons), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuTVSeasonList.Items.Remove(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem_TVShows(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuTVShowList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuTVShowList.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem_TVShows), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuTVShowList.Items.Remove(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_Movies(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuMovieList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuMovieList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_Movies), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuMovieList.Items.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_MovieSets(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuMovieSetList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuMovieSetList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_MovieSets), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuMovieSetList.Items.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_TVEpisodes(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_TVEpisodes), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuTVEpisodeList.Items.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_TVSeasons(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuTVSeasonList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuTVSeasonList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_TVSeasons), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuTVSeasonList.Items.Add(value)
        End If
    End Sub

    Public Sub SetToolsStripItem_TVShows(value As System.Windows.Forms.ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.MenuTVShowList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.MenuTVShowList.Invoke(New Delegate_SetToolsStripItem(AddressOf SetToolsStripItem_TVShows), New Object() {value})
        Else
            ModulesManager.Instance.RuntimeObjects.MenuTVShowList.Items.Add(value)
        End If
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent ModuleEnabledChanged(Me._Name, State, 0)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure _MySettings

#Region "Fields"
        'Kodi Host type already declared in EmberAPI (XML serialization)
        Dim KodiHosts As clsXMLHosts
        Dim SendNotifications As Boolean
        Dim SyncPlayCountHost As String
        Dim SyncPlayCount As Boolean
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class
