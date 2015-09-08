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
Imports System.Xml.Serialization


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
    Private _SpecialSettings As New SpecialSettings
    Private _AssemblyName As String = String.Empty
    Private _Enabled As Boolean = False
    Private _Name As String = "Kodi"
    Private _setup As frmSettingsHolder
    Private _xmlSettingsPath As String = FileUtils.Common.ReturnSettingsFile("Settings", "Interface.Kodi.xml")
    Private cmnuKodi_MovieSets As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuKodi_Movies As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuKodi_TVEpisodes As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuKodi_TVSeasons As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuKodi_TVShows As New System.Windows.Forms.ToolStripMenuItem
    Private cmnuSep_MovieSets As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_Movies As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_TVEpisodes As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_TVSeasons As New System.Windows.Forms.ToolStripSeparator
    Private cmnuSep_TVShows As New System.Windows.Forms.ToolStripSeparator
    Private mnuMainToolsKodi As New System.Windows.Forms.ToolStripMenuItem
    Private mnuTrayToolsKodi As New System.Windows.Forms.ToolStripMenuItem
    ''' <summary>
    ''' pool of Update tasks for KodiInterface (can be filled extremely fast when updating whole tvshow at once)
    ''' </summary>
    ''' <remarks></remarks>
    Private TaskList As New List(Of KodiTask)
    ''' <summary>
    ''' control variable: true=Ready to start tmrRunTasks-Timer and work through items of tasklist, false= Timer already tickting, executing tasks
    ''' </summary>
    ''' <remarks></remarks>
    Private TasksDone As Boolean = True

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
        'add job to tasklist and get everything done
        TaskList.Add(New KodiTask With {.mType = mType, .mDBElement = _dbelement})
        If TasksDone Then Me.RunTasks()
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
    Private Async Sub RunTasks()
        Me.TasksDone = False
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1439, "Run Tasks"), New Bitmap(My.Resources.logo)}))
        While Me.TaskList.Count > 0
            Await GenericRunCallBack(TaskList.Item(0).mType, TaskList.Item(0).mDBElement, TaskList.Item(0).mHost)
            Me.TaskList.RemoveAt(0)
        End While
        Me.TasksDone = True
        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(251, "All Tasks Done"), New Bitmap(My.Resources.logo)}))
    End Sub

    Sub Handle_GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object))
        RaiseEvent GenericEvent(mType, _params)
    End Sub
    ''' <summary>
    ''' This is a generic callback function to handle all realtime-sync work for KODI-Api
    ''' </summary>
    ''' <param name="mType"></param>
    ''' <remarks>
    ''' Worker function used to handle all ApiTaks in List(of KodiTask)
    ''' Made async to await async Kodi API
    ''' </remarks>
    Private Async Function GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByVal mDBElement As Database.DBElement, Optional mHost As Host = Nothing) As Task(Of Boolean)
        'check if at least one host is configured, else skip
        If _SpecialSettings.Hosts.Count > 0 Then
            Select Case mType

                'Movie syncing
                Case Enums.ModuleEventType.Sync_Movie
                    Dim tDBMovie As EmberAPI.Database.DBElement = mDBElement
                    If tDBMovie.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(tDBMovie, True) Then
                        If Not String.IsNullOrEmpty(tDBMovie.NfoPath) Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)
                                'run task
                                AddHandler _APIKodi.GenericEvent, AddressOf Handle_GenericEvent
                                If Await Task.Run(Function() _APIKodi.UpdateMovieInfo(tDBMovie.ID, _SpecialSettings.SendNotifications, _SpecialSettings.SyncPlayCounts AndAlso _SpecialSettings.SyncPlayCountsHost = mHost.Label)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", mHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBMovie.Movie.Title, New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn("[KodiInterface] RunGeneric MovieUpdate: " & mHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBMovie.Movie.Title)
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", mHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBMovie.Movie.Title, Nothing}))
                                End If
                                RemoveHandler _APIKodi.GenericEvent, AddressOf Handle_GenericEvent
                            Else
                                For Each tHost As Host In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.Movie).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)
                                    'run task
                                    AddHandler _APIKodi.GenericEvent, AddressOf Handle_GenericEvent
                                    If Await Task.Run(Function() _APIKodi.UpdateMovieInfo(tDBMovie.ID, _SpecialSettings.SendNotifications, _SpecialSettings.SyncPlayCounts AndAlso _SpecialSettings.SyncPlayCountsHost = tHost.Label)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBMovie.Movie.Title, New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn("[KodiInterface] RunGeneric MovieUpdate: " & tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBMovie.Movie.Title)
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBMovie.Movie.Title, Nothing}))
                                    End If
                                    RemoveHandler _APIKodi.GenericEvent, AddressOf Handle_GenericEvent
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
                                Next
                            End If
                        Else
                            logger.Warn("[KodiInterface] GenericRunCallBack MovieUpdate: " & Master.eLang.GetString(1442, "Please Scrape In Ember First!"))
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                        End If
                    Else
                        logger.Warn("[KodiInterface] GenericRunCallBack MovieUpdate: Not online!")
                    End If

                    'MovieSet syncing
                Case Enums.ModuleEventType.Sync_MovieSet
                    Dim tDBMovieset As EmberAPI.Database.DBElement = mDBElement
                    If tDBMovieset.MovieList.Count > 0 Then
                        If Not String.IsNullOrEmpty(tDBMovieset.MovieSet.TMDB) Then
                            For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso Not String.IsNullOrEmpty(f.MovieSetArtworksPath))
                                Dim _APIKodi As New Kodi.APIKodi(tHost)
                                If Await Task.Run(Function() _APIKodi.UpdateMovieSetInfo(tDBMovieset.ID, _SpecialSettings.SendNotifications)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBMovieset.MovieSet.Title, New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn("[KodiInterface] RunGeneric MovieSetUpdate: " & tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBMovieset.MovieSet.Title)
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBMovieset.MovieSet.Title, Nothing}))
                                End If
                                ''TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                'If result.Result = True Then
                                '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title)
                                '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title, New Bitmap(My.Resources.logo)}))
                                'Else
                                '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title)
                                '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title, Nothing}))
                                'End If
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
                    Dim tDBTV As EmberAPI.Database.DBElement = mDBElement
                    If tDBTV.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(tDBTV, True) Then
                        If Not String.IsNullOrEmpty(tDBTV.NfoPath) Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)
                                AddHandler _APIKodi.GenericEvent, AddressOf Handle_GenericEvent
                                If Await Task.Run(Function() _APIKodi.UpdateTVEpisodeInfo(tDBTV.ID, _SpecialSettings.SendNotifications, _SpecialSettings.SyncPlayCounts AndAlso _SpecialSettings.SyncPlayCountsHost = mHost.Label)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", mHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVEpisode.Title, New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn("[KodiInterface] RunGeneric TVEpisodeUpdate: " & mHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVEpisode.Title)
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", mHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVEpisode.Title, Nothing}))
                                End If
                                RemoveHandler _APIKodi.GenericEvent, AddressOf Handle_GenericEvent
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)
                                    AddHandler _APIKodi.GenericEvent, AddressOf Handle_GenericEvent
                                    If Await Task.Run(Function() _APIKodi.UpdateTVEpisodeInfo(tDBTV.ID, _SpecialSettings.SendNotifications, _SpecialSettings.SyncPlayCounts AndAlso _SpecialSettings.SyncPlayCountsHost = tHost.Label)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVEpisode.Title, New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn("[KodiInterface] RunGeneric TVEpisodeUpdate: " & tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVEpisode.Title)
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVEpisode.Title, Nothing}))
                                    End If
                                    RemoveHandler _APIKodi.GenericEvent, AddressOf Handle_GenericEvent
                                    ''TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                    'If result.Result = True Then
                                    '    logger.Warn("[KodiInterface] RunGeneric EpisodeUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVEp.Title)
                                    '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVEp.Title, New Bitmap(My.Resources.logo)}))
                                    'Else
                                    '    logger.Warn("[KodiInterface] RunGeneric EpisodeUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVEp.Title)
                                    '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVEp.Title, Nothing}))
                                    'End If
                                Next
                            End If
                        Else
                            logger.Warn("[KodiInterface] GenericRunCallBack TVEpisodeUpdate: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                        End If
                    Else
                        logger.Warn("[KodiInterface] GenericRunCallBack TVEpisodeUpdate: Not online!")
                    End If

                    'TVSeason syncing
                Case Enums.ModuleEventType.Sync_TVSeason
                    Dim tDBTV As EmberAPI.Database.DBElement = mDBElement
                    If tDBTV.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(tDBTV, True) Then
                        If Not String.IsNullOrEmpty(tDBTV.ID.ToString) Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)
                                If Await Task.Run(Function() _APIKodi.UpdateTVSeasonInfo(tDBTV.ID, _SpecialSettings.SendNotifications)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", mHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVSeason.Title, New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn("[KodiInterface] RunGeneric TVSeasonUpdate: " & mHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVSeason.Title)
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", mHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVSeason.Title, Nothing}))
                                End If
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)
                                    If Await Task.Run(Function() _APIKodi.UpdateTVSeasonInfo(tDBTV.ID, _SpecialSettings.SendNotifications)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVSeason.Title, New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn("[KodiInterface] RunGeneric TVSeasonUpdate: " & tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVSeason.Title)
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVSeason.Title, Nothing}))
                                    End If
                                    ''TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                    'If result.Result = True Then
                                    '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title)
                                    '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title, New Bitmap(My.Resources.logo)}))
                                    'Else
                                    '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title)
                                    '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title, Nothing}))
                                    'End If
                                Next
                            End If
                        Else
                            logger.Warn("[KodiInterface] GenericRunCallBack TVSeasonUpdate: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                        End If
                    Else
                        logger.Warn("[KodiInterface] GenericRunCallBack TVSeasonUpdate: Not online!")
                    End If

                    'TVShow syncing
                Case Enums.ModuleEventType.Sync_TVShow
                    Dim tDBTV As EmberAPI.Database.DBElement = mDBElement
                    If tDBTV.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(tDBTV, True) Then
                        If Not String.IsNullOrEmpty(tDBTV.NfoPath) Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)
                                If Await Task.Run(Function() _APIKodi.UpdateTVShowInfo(tDBTV.ShowID, _SpecialSettings.SendNotifications)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", mHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title, New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & mHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title)
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", mHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title, Nothing}))
                                End If
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)
                                    If Await Task.Run(Function() _APIKodi.UpdateTVShowInfo(tDBTV.ShowID, _SpecialSettings.SendNotifications)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title, New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title)
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", tHost.Label & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title, Nothing}))
                                    End If
                                    ''TODO We don't wait here for Async API to be finished (because it will block UI thread for a few seconds), any idea?
                                    'If result.Result = True Then
                                    '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title)
                                    '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1444, "Sync OK") & ": " & tDBTV.TVShow.Title, New Bitmap(My.Resources.logo)}))
                                    'Else
                                    '    logger.Warn("[KodiInterface] RunGeneric TVShowUpdate: " & host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title)
                                    '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), host.name & " | " & Master.eLang.GetString(1445, "Sync Failed") & ": " & tDBTV.TVShow.Title, Nothing}))
                                    'End If
                                Next
                            End If
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

        Return True
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
        _SpecialSettings.Clear()
        If File.Exists(_xmlSettingsPath) Then
            Dim xmlSer As XmlSerializer = Nothing
            Using xmlSR As StreamReader = New StreamReader(_xmlSettingsPath)
                xmlSer = New XmlSerializer(GetType(SpecialSettings))
                _SpecialSettings = DirectCast(xmlSer.Deserialize(xmlSR), SpecialSettings)
            End Using
        End If
    End Sub

    Private Sub CreateContextMenu(ByRef tMenu As ToolStripMenuItem, ByVal tContentType As Enums.ContentType)
        If _SpecialSettings.Hosts IsNot Nothing AndAlso _SpecialSettings.Hosts.Count = 1 Then
            Dim mnuHostSyncItem As New ToolStripMenuItem
            mnuHostSyncItem.Image = New Bitmap(My.Resources.menuSync)
            mnuHostSyncItem.Tag = _SpecialSettings.Hosts(0)
            mnuHostSyncItem.Text = Master.eLang.GetString(1446, "Sync")
            Select Case tContentType
                Case Enums.ContentType.Movie
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_Movie_Click
                Case Enums.ContentType.MovieSet
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_MovieSet_Click
                Case Enums.ContentType.TVEpisode
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVEpisode_Click
                Case Enums.ContentType.TVSeason
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVSeason_Click
                Case Enums.ContentType.TVShow
                    AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVShow_Click
            End Select
            tMenu.DropDownItems.Add(mnuHostSyncItem)
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
                    Case Enums.ContentType.MovieSet
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_MovieSet_Click
                    Case Enums.ContentType.TVEpisode
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVEpisode_Click
                    Case Enums.ContentType.TVSeason
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVSeason_Click
                    Case Enums.ContentType.TVShow
                        AddHandler mnuHostSyncItem.Click, AddressOf cmnuHostSyncItem_TVShow_Click
                End Select
                mnuHost.DropDownItems.Add(mnuHostSyncItem)
                tMenu.DropDownItems.Add(mnuHost)
            Next
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
        mnuMainToolsKodi.DropDownItems.Clear()
        mnuMainToolsKodi.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsKodi.Text = "Kodi"
        mnuMainToolsKodi.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForMovieSets = True, .IfTabMovieSets = True, .ForTVShows = True, .IfTabTVShows = True}
        CreateToolsMenu(mnuMainToolsKodi)
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsKodi)

        'mnuTrayTools
        mnuTrayToolsKodi.DropDownItems.Clear()
        mnuTrayToolsKodi.Image = New Bitmap(My.Resources.icon)
        mnuTrayToolsKodi.Text = "Kodi"
        CreateToolsMenu(mnuTrayToolsKodi)
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuTrayToolsKodi)

        'cmnuMovies
        cmnuKodi_Movies.DropDownItems.Clear()
        cmnuKodi_Movies.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_Movies.Text = "Kodi"
        cmnuKodi_Movies.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        CreateContextMenu(cmnuKodi_Movies, Enums.ContentType.Movie)
        SetToolsStripItem_Movies(cmnuSep_Movies)
        SetToolsStripItem_Movies(cmnuKodi_Movies)

        'cmnuMovieSets
        cmnuKodi_MovieSets.DropDownItems.Clear()
        cmnuKodi_MovieSets.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_MovieSets.Text = "Kodi"
        cmnuKodi_MovieSets.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        CreateContextMenu(cmnuKodi_MovieSets, Enums.ContentType.MovieSet)
        SetToolsStripItem_MovieSets(cmnuSep_MovieSets)
        SetToolsStripItem_MovieSets(cmnuKodi_MovieSets)

        'cmnuTVEpisodes
        cmnuKodi_TVEpisodes.DropDownItems.Clear()
        cmnuKodi_TVEpisodes.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_TVEpisodes.Text = "Kodi"
        cmnuKodi_TVEpisodes.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        CreateContextMenu(cmnuKodi_TVEpisodes, Enums.ContentType.TVEpisode)
        SetToolsStripItem_TVEpisodes(cmnuSep_TVEpisodes)
        SetToolsStripItem_TVEpisodes(cmnuKodi_TVEpisodes)

        'cmnuTVSeasons
        cmnuKodi_TVSeasons.DropDownItems.Clear()
        cmnuKodi_TVSeasons.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_TVSeasons.Text = "Kodi"
        cmnuKodi_TVSeasons.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        CreateContextMenu(cmnuKodi_TVSeasons, Enums.ContentType.TVSeason)
        SetToolsStripItem_TVSeasons(cmnuSep_TVSeasons)
        SetToolsStripItem_TVSeasons(cmnuKodi_TVSeasons)

        'cmnuTVShows
        cmnuKodi_TVShows.DropDownItems.Clear()
        cmnuKodi_TVShows.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_TVShows.Text = "Kodi"
        cmnuKodi_TVShows.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        CreateContextMenu(cmnuKodi_TVShows, Enums.ContentType.TVShow)
        SetToolsStripItem_TVShows(cmnuSep_TVShows)
        SetToolsStripItem_TVShows(cmnuKodi_TVShows)
    End Sub

    ''' <summary>
    ''' Actions on module startup (Ember startup) and runtime if module is enabled
    ''' </summary>
    ''' <remarks></remarks>
    Sub Enable()
        PopulateMenus()
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
        tsi.DropDownItems.Remove(mnuTrayToolsKodi)

        'cmnuMovies
        RemoveToolsStripItem_Movies(cmnuSep_Movies)
        RemoveToolsStripItem_Movies(cmnuKodi_Movies)
        'cmnuEpisodes
        RemoveToolsStripItem_TVEpisodes(cmnuSep_TVEpisodes)
        RemoveToolsStripItem_TVEpisodes(cmnuKodi_TVEpisodes)
        'cmnuShows
        RemoveToolsStripItem_TVShows(cmnuSep_TVShows)
        RemoveToolsStripItem_TVShows(cmnuKodi_TVShows)
        'cmnuSeasons
        RemoveToolsStripItem_TVSeasons(cmnuSep_TVSeasons)
        RemoveToolsStripItem_TVSeasons(cmnuKodi_TVSeasons)
        'cmnuSets
        RemoveToolsStripItem_MovieSets(cmnuSep_MovieSets)
        RemoveToolsStripItem_MovieSets(cmnuKodi_MovieSets)
    End Sub
    ''' <summary>
    ''' Load and fill controls of settings page of module
    ''' </summary>
    ''' <remarks>
    ''' 2015/06/27 Cocotus - First implementation, prepared by DanCooper
    ''' Triggered when user enters settings in Ember
    Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _Enabled
        _setup.chkNotification.Checked = _SpecialSettings.SendNotifications
        _setup.chkPlayCount.Checked = _SpecialSettings.SyncPlayCounts
        If _SpecialSettings.SyncPlayCounts Then
            _setup.cbPlayCountHost.Enabled = True
        Else
            _setup.cbPlayCountHost.Enabled = False
        End If
        _setup.HostList = _SpecialSettings.Hosts
        _setup.lbHosts.Items.Clear()
        For Each tHost As Host In _setup.HostList
            _setup.cbPlayCountHost.Items.Add(tHost.Label)
            _setup.lbHosts.Items.Add(tHost.Label)
        Next
        _setup.cbPlayCountHost.SelectedIndex = _setup.cbPlayCountHost.FindStringExact(_SpecialSettings.SyncPlayCountsHost)

        SPanel.Name = _Name
        SPanel.Text = "Kodi Interface"
        SPanel.Prefix = "Kodi_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_Enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings()

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
        _SpecialSettings.SendNotifications = Me._setup.chkNotification.Checked
        _SpecialSettings.SyncPlayCounts = _setup.chkPlayCount.Checked AndAlso Me._setup.cbPlayCountHost.SelectedItem IsNot Nothing
        _SpecialSettings.SyncPlayCountsHost = If(Me._setup.cbPlayCountHost.SelectedItem IsNot Nothing, Me._setup.cbPlayCountHost.SelectedItem.ToString(), String.Empty)
        SaveSettings()
        If Me.Enabled Then PopulateMenus()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub
    ''' <summary>
    ''' Save module settings
    ''' </summary>
    ''' <remarks></remarks>
    Sub SaveSettings()
        If Not File.Exists(_xmlSettingsPath) OrElse (Not CBool(File.GetAttributes(_xmlSettingsPath) And FileAttributes.ReadOnly)) Then
            If File.Exists(_xmlSettingsPath) Then
                Dim fAtt As FileAttributes = File.GetAttributes(_xmlSettingsPath)
                Try
                    File.SetAttributes(_xmlSettingsPath, FileAttributes.Normal)
                Catch ex As Exception
                    logger.Error(New StackFrame().GetMethod().Name, ex)
                End Try
            End If
            Using xmlSW As New StreamWriter(_xmlSettingsPath)
                Dim xmlSer As New XmlSerializer(GetType(SpecialSettings))
                xmlSer.Serialize(xmlSW, _SpecialSettings)
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
    Private Sub cmnuHostSyncItem_Movie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
                Dim DBElement As Database.DBElement = Master.DB.LoadMovieFromDB(ID, False)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                    If Not String.IsNullOrEmpty(DBElement.NfoPath) Then
                        'add job to tasklist and get everything done
                        TaskList.Add(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_Movie})
                        If TasksDone Then Me.RunTasks()
                    Else
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
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
    Private Sub cmnuHostSyncItem_MovieSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSet").Value)
                Dim DBElement As Database.DBElement = Master.DB.LoadMovieSetFromDB(ID, False)
                If Not String.IsNullOrEmpty(DBElement.MovieSet.Title) Then
                    'add job to tasklist and get everything done
                    TaskList.Add(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_MovieSet})
                    If TasksDone Then Me.RunTasks()
                Else
                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                End If
            Next
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
    Private Sub cmnuHostSyncItem_TVEpisode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                Dim DBElement As Database.DBElement = Master.DB.LoadTVEpisodeFromDB(ID, True, False)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                    If Not String.IsNullOrEmpty(DBElement.NfoPath) Then
                        'add job to tasklist and get everything done
                        TaskList.Add(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_TVEpisode})
                        If TasksDone Then Me.RunTasks()
                    Else
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
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
    Private Sub cmnuHostSyncItem_TVSeason_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
                Dim DBElement As Database.DBElement = Master.DB.LoadTVSeasonFromDB(ID, True, False)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                    'add job to tasklist and get everything done
                    TaskList.Add(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_TVSeason})
                    If TasksDone Then Me.RunTasks()
                End If
            Next
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
    Private Sub cmnuHostSyncItem_TVShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.LoadTVShowFromDB(ID, False, False, False)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                    If Not String.IsNullOrEmpty(DBElement.NfoPath) Then
                        'add job to tasklist and get everything done
                        TaskList.Add(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_TVShow})
                        If TasksDone Then Me.RunTasks()
                    Else
                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                    End If
                End If
            Next
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    '''  Clean video library of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Async Sub mnuHostCleanVideoLibrary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1450, "Cleaning Video Library..."), New Bitmap(My.Resources.logo)}))
            Dim response = Await _APIKodi.CleanVideoLibrary()
            If response = Nothing Then
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1451, "Cleaning Failed"), Nothing}))
            Else
                'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, "Kodi Interface", "Video library updated", Nothing}))
            End If
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub
    ''' <summary>
    '''  Scan video library of submitted host
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' </remarks>
    Private Async Sub mnuHostScanVideoLibrary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1448, "Updating Video Library..."), New Bitmap(My.Resources.logo)}))
            Dim response = Await _APIKodi.ScanVideoLibrary()
            If response = Nothing Then
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1449, "Update Failed"), Nothing}))
            Else
                'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, "Kodi Interface", "Video library updated", Nothing}))
            End If
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
        End If
    End Sub

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
    ''' <summary>
    ''' structure used to store Update Movie/TV/Movieset-Tasks for Kodi Interface
    ''' </summary>
    ''' <remarks></remarks>
    Private Structure KodiTask

        Dim mDBElement As Database.DBElement
        Dim mHost As Host
        Dim mType As Enums.ModuleEventType

    End Structure

    <Serializable()> _
    <XmlRoot("interface.kodi")> _
    Class SpecialSettings

#Region "Fields"

        Private _hosts As New List(Of Host)
        Private _sendnotifications As Boolean
        Private _syncplaycounts As Boolean
        Private _syncplaycountshost As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("sendnotifications")> _
        Public Property SendNotifications() As Boolean
            Get
                Return Me._sendnotifications
            End Get
            Set(ByVal value As Boolean)
                Me._sendnotifications = value
            End Set
        End Property

        <XmlElement("syncplaycounts")> _
        Public Property SyncPlayCounts() As Boolean
            Get
                Return Me._syncplaycounts
            End Get
            Set(ByVal value As Boolean)
                Me._syncplaycounts = value
            End Set
        End Property

        <XmlElement("syncplaycountshost")> _
        Public Property SyncPlayCountsHost() As String
            Get
                Return Me._syncplaycountshost
            End Get
            Set(ByVal value As String)
                Me._syncplaycountshost = value
            End Set
        End Property

        <XmlElement("host")> _
        Public Property Hosts() As List(Of Host)
            Get
                Return Me._hosts
            End Get
            Set(ByVal value As List(Of Host))
                Me._hosts = value
            End Set
        End Property

#End Region 'Properties

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Sub Clear()
            Me._hosts.Clear()
            Me._sendnotifications = False
            Me._syncplaycounts = False
            Me._syncplaycountshost = String.Empty
        End Sub

#End Region 'Methods


    End Class

    <Serializable()> _
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

        <XmlElement("label")> _
        Public Property Label() As String
            Get
                Return Me._label
            End Get
            Set(ByVal value As String)
                Me._label = value
            End Set
        End Property

        <XmlElement("address")> _
        Public Property Address() As String
            Get
                Return Me._address
            End Get
            Set(ByVal value As String)
                Me._address = value
            End Set
        End Property

        <XmlElement("port")> _
        Public Property Port() As Integer
            Get
                Return Me._port
            End Get
            Set(ByVal value As Integer)
                Me._port = value
            End Set
        End Property

        <XmlElement("username")> _
        Public Property Username() As String
            Get
                Return Me._username
            End Get
            Set(ByVal value As String)
                Me._username = value
            End Set
        End Property

        <XmlElement("password")> _
        Public Property Password() As String
            Get
                Return Me._password
            End Get
            Set(ByVal value As String)
                Me._password = value
            End Set
        End Property

        <XmlElement("realtimesync")> _
        Public Property RealTimeSync() As Boolean
            Get
                Return Me._realtimesync
            End Get
            Set(ByVal value As Boolean)
                Me._realtimesync = value
            End Set
        End Property

        <XmlElement("moviesetartworkspath")> _
        Public Property MovieSetArtworksPath() As String
            Get
                Return Me._moviesetartworkspath
            End Get
            Set(ByVal value As String)
                Me._moviesetartworkspath = value
            End Set
        End Property

        <XmlElement("source")> _
        Public Property Sources() As List(Of Source)
            Get
                Return Me._sources
            End Get
            Set(ByVal value As List(Of Source))
                Me._sources = value
            End Set
        End Property

#End Region 'Properties

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Sub Clear()
            Me._address = "localhost"
            Me._moviesetartworkspath = String.Empty
            Me._label = "New Host"
            Me._password = String.Empty
            Me._port = 80
            Me._realtimesync = False
            Me._sources.Clear()
            Me._username = "kodi"
        End Sub

#End Region 'Methods

    End Class


    <Serializable()> _
    Class Source


#Region "Fields"

        Private _contenttype As Enums.ContentType
        Private _localpath As String
        Private _remotepath As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("contenttype")> _
        Public Property ContentType() As Enums.ContentType
            Get
                Return Me._contenttype
            End Get
            Set(ByVal value As Enums.ContentType)
                Me._contenttype = value
            End Set
        End Property

        <XmlElement("localpath")> _
        Public Property LocalPath() As String
            Get
                Return Me._localpath
            End Get
            Set(ByVal value As String)
                Me._localpath = value
            End Set
        End Property

        <XmlElement("remotepath")> _
        Public Property RemotePath() As String
            Get
                Return Me._remotepath
            End Get
            Set(ByVal value As String)
                Me._remotepath = value
            End Set
        End Property

#End Region 'Properties

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Sub Clear()
            Me._contenttype = Enums.ContentType.Movie
            Me._localpath = String.Empty
            Me._remotepath = String.Empty
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class
