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
Imports NLog
Imports System.Xml.Serialization


Public Class KodiInterface
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_SetToolStripItem(ts As ToolStrip, value As ToolStripItem)
    Public Delegate Sub Delegate_AddToolStripMenuItem(tsi As ToolStripMenuItem, value As ToolStripMenuItem)
    Public Delegate Sub Delegate_AddToolStripItem(value As ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolStripItem(value As ToolStripItem)

    Public Delegate Sub Delegate_ChangeTaskManagerStatus(control As ToolStripLabel, value As String)
    Public Delegate Sub Delegate_ChangeTaskManagerProgressBar(control As ToolStripProgressBar, value As ProgressBarStyle)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    'reflects the current host(s) settings/setup configured in settings, will be filled at module startup from XML settings (and is used to write changes of settings back into XML)
    Private _SpecialSettings As New SpecialSettings
    Private _AssemblyName As String = String.Empty
    Private _Enabled As Boolean = False
    Private _Name As String = "Kodi"
    Private _setup As frmSettingsHolder
    Private _xmlSettingsPath As String = Path.Combine(Master.SettingsPath, "Interface.Kodi.xml")
    Private cmnuKodi_MovieSets As New ToolStripMenuItem
    Private cmnuKodi_Movies As New ToolStripMenuItem
    Private cmnuKodi_TVEpisodes As New ToolStripMenuItem
    Private cmnuKodi_TVSeasons As New ToolStripMenuItem
    Private cmnuKodi_TVShows As New ToolStripMenuItem
    Private cmnuSep_MovieSets As New ToolStripSeparator
    Private cmnuSep_Movies As New ToolStripSeparator
    Private cmnuSep_TVEpisodes As New ToolStripSeparator
    Private cmnuSep_TVSeasons As New ToolStripSeparator
    Private cmnuSep_TVShows As New ToolStripSeparator
    Private mnuMainToolsKodi As New ToolStripMenuItem
    Private mnuTrayToolsKodi As New ToolStripMenuItem

    Private lblTaskManagerStatus As New ToolStripLabel With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private lblTaskManagerTitle As New ToolStripLabel With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tspTaskManager As New ToolStripProgressBar With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tssTaskManager1 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tssTaskManager2 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tssTaskManager3 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tssTaskManager4 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tssTaskManager5 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tssTaskManager6 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tssTaskManager7 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}
    Private tssTaskManager8 As New ToolStripSeparator With {.Alignment = ToolStripItemAlignment.Right, .Visible = True}

    ''' <summary>
    ''' pool of Update tasks for KodiInterface (can be filled extremely fast when updating whole tvshow at once)
    ''' </summary>
    ''' <remarks></remarks>
    Private TaskList As New Queue(Of KodiTask)
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
    Public Event SetupNeedsRestart() Implements Interfaces.GenericModule.SetupNeedsRestart

#End Region 'Events

#Region "Properties"
    ''' <summary>
    ''' Subscribe to Eventtypes here
    ''' </summary>
    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {
                                                      Enums.ModuleEventType.BeforeEdit_Movie,
                                                      Enums.ModuleEventType.BeforeEdit_TVEpisode,
                                                      Enums.ModuleEventType.Remove_Movie,
                                                      Enums.ModuleEventType.Remove_TVEpisode,
                                                      Enums.ModuleEventType.Remove_TVShow,
                                                      Enums.ModuleEventType.ScraperMulti_Movie,
                                                      Enums.ModuleEventType.ScraperMulti_TVEpisode,
                                                      Enums.ModuleEventType.ScraperMulti_TVSeason,
                                                      Enums.ModuleEventType.ScraperMulti_TVShow,
                                                      Enums.ModuleEventType.ScraperSingle_Movie,
                                                      Enums.ModuleEventType.ScraperSingle_TVEpisode,
                                                      Enums.ModuleEventType.ScraperSingle_TVSeason,
                                                      Enums.ModuleEventType.ScraperSingle_TVShow,
                                                      Enums.ModuleEventType.Sync_Movie,
                                                      Enums.ModuleEventType.Sync_MovieSet,
                                                      Enums.ModuleEventType.Sync_TVEpisode,
                                                      Enums.ModuleEventType.Sync_TVSeason,
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

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.GenericModule.IsBusy
        Get
            Return Not TasksDone
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
        If Not Master.isCL AndAlso (
                mType = Enums.ModuleEventType.Remove_Movie OrElse
                mType = Enums.ModuleEventType.Remove_TVEpisode OrElse
                mType = Enums.ModuleEventType.Remove_TVShow OrElse
                mType = Enums.ModuleEventType.Sync_Movie OrElse
                mType = Enums.ModuleEventType.Sync_MovieSet OrElse
                mType = Enums.ModuleEventType.Sync_TVEpisode OrElse
                mType = Enums.ModuleEventType.Sync_TVSeason OrElse
                mType = Enums.ModuleEventType.Sync_TVShow) Then
            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mType = mType, .mDBElement = _dbelement})
            Return New Interfaces.ModuleResult With {.breakChain = False}
        Else
            Select Case mType
                Case Enums.ModuleEventType.BeforeEdit_Movie
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateBeforeEdit_Movie Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.ModuleEventType.BeforeEdit_TVEpisode
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.ModuleEventType.ScraperMulti_Movie
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateScraperMulti_Movie Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.ModuleEventType.ScraperMulti_TVEpisode, Enums.ModuleEventType.ScraperMulti_TVSeason, Enums.ModuleEventType.ScraperMulti_TVShow
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.ModuleEventType.ScraperSingle_Movie
                    If Not _SpecialSettings.GetWatchedState OrElse Not _SpecialSettings.GetWatchedStateScraperSingle_Movie Then
                        Return New Interfaces.ModuleResult
                    End If
                Case Enums.ModuleEventType.ScraperSingle_TVEpisode, Enums.ModuleEventType.ScraperSingle_TVSeason, Enums.ModuleEventType.ScraperSingle_TVShow
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

    Private Async Function DoCommandLine(ByVal mType As Enums.ModuleEventType, ByVal mDBElement As Database.DBElement) As Task(Of Boolean)
        Dim GenericEventActionAsync As New Action(Of GenericEventCallBackAsync)(AddressOf Handle_GenericEventAsync)
        Dim GenericEventProgressAsync = New Progress(Of GenericEventCallBackAsync)(GenericEventActionAsync)
        Return Await Task.Run(Function() GenericRunCallBack(mType, mDBElement, GenericEventProgressAsync))
    End Function

    Private Sub AddTask(ByRef nTask As KodiTask)
        TaskList.Enqueue(nTask)
        If TasksDone Then
            RunTasks()
        Else
            ChangeTaskManagerStatus(lblTaskManagerStatus, String.Concat("Pending Tasks: ", (TaskList.Count + 1).ToString))
        End If
    End Sub

    Private Async Sub RunTasks()
        Dim getError As Boolean = False
        Dim GenericEventActionAsync As New Action(Of GenericEventCallBackAsync)(AddressOf Handle_GenericEventAsync)
        Dim GenericEventProgressAsync = New Progress(Of GenericEventCallBackAsync)(GenericEventActionAsync)

        TasksDone = False
        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1439, "Run Tasks"), New Bitmap(My.Resources.logo)}))
        While TaskList.Count > 0
            ChangeTaskManagerStatus(lblTaskManagerStatus, String.Concat("Pending Tasks: ", TaskList.Count.ToString))
            ChangeTaskManagerProgressBar(tspTaskManager, ProgressBarStyle.Marquee)
            Dim kTask As KodiTask = TaskList.Dequeue()
            If Not Await GenericRunCallBack(kTask.mType, kTask.mDBElement, GenericEventProgressAsync, kTask.mHost, kTask.mInternalType) Then
                getError = True
            End If
        End While
        TasksDone = True
        ChangeTaskManagerProgressBar(tspTaskManager, ProgressBarStyle.Continuous)
        ChangeTaskManagerStatus(lblTaskManagerStatus, "No Pending Tasks")
        If Not getError Then
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(251, "All Tasks Done"), New Bitmap(My.Resources.logo)}))
        Else
            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), String.Format(Master.eLang.GetString(969, "One or more Task(s) failed.{0}Please check log for more informations"), Environment.NewLine), Nothing}))
        End If
    End Sub

    Sub Handle_GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))
        RaiseEvent GenericEvent(mType, _params)
    End Sub

    Sub Handle_GenericEventAsync(ByVal mGenericEventCallBack As GenericEventCallBackAsync)
        RaiseEvent GenericEvent(mGenericEventCallBack.tEventType, mGenericEventCallBack.tParams)
    End Sub

    Sub Handle_GenericSubEventAsync(ByVal mGenericSubEventCallBack As GenericSubEventCallBackAsync)
        mGenericSubEventCallBack.tProgress.Report(mGenericSubEventCallBack.tGenericEventCallBackAsync)
    End Sub
    ''' <summary>
    ''' This is a generic callback function to handle all realtime-sync work for KODI-Api
    ''' </summary>
    ''' <param name="mType"></param>
    ''' <remarks>
    ''' Worker function used to handle all ApiTaks in List(of KodiTask)
    ''' Made async to await async Kodi API
    ''' </remarks>
    Private Async Function GenericRunCallBack(ByVal mType As Enums.ModuleEventType, ByVal mDBElement As Database.DBElement, ByVal GenericEventProcess As IProgress(Of GenericEventCallBackAsync), Optional mHost As Host = Nothing, Optional mInternalType As InternalType = Nothing) As Task(Of Boolean)
        Dim getError As Boolean = False
        Dim GenericSubEventActionAsync As New Action(Of GenericSubEventCallBackAsync)(AddressOf Handle_GenericSubEventAsync)
        Dim GenericSubEventProgressAsync = New Progress(Of GenericSubEventCallBackAsync)(GenericSubEventActionAsync)

        'check if at least one host is configured, else skip
        If _SpecialSettings.Hosts.Count > 0 Then
            Select Case mType

                'Before Edit Movie / Scraper Multi Movie / Scraper Single Movie
                Case Enums.ModuleEventType.BeforeEdit_Movie, Enums.ModuleEventType.ScraperMulti_Movie, Enums.ModuleEventType.ScraperSingle_Movie
                    If mDBElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                        mHost = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(mDBElement, True) Then
                                    If mDBElement.NfoPathSpecified Then
                                        'run task
                                        Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_Movie(mDBElement, GenericSubEventProgressAsync, GenericEventProcess))
                                        If Result IsNot Nothing Then
                                            If Not Result.AlreadyInSync Then
                                                mDBElement.Movie.LastPlayed = Result.LastPlayed
                                                mDBElement.Movie.PlayCount = Result.PlayCount
                                            End If
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.Movie.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.Movie.Title))
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.Movie.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            logger.Warn(String.Format("[KodiInterface] [GenericRunCallBack]: Hostname ({0}) not found in host list!", _SpecialSettings.GetWatchedStateHost))
                        End If
                    End If

                'Before Edit TVEpisode / Scraper Multi TVEpisode / Scraper Single TVEpisode
                Case Enums.ModuleEventType.BeforeEdit_TVEpisode, Enums.ModuleEventType.ScraperMulti_TVEpisode, Enums.ModuleEventType.ScraperSingle_TVEpisode
                    If mDBElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                        mHost = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(mDBElement, True) Then
                                    If mDBElement.NfoPathSpecified Then
                                        'run task
                                        Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_TVEpisode(mDBElement, GenericSubEventProgressAsync, GenericEventProcess))
                                        If Result IsNot Nothing Then
                                            If Not Result.AlreadyInSync Then
                                                mDBElement.TVEpisode.LastPlayed = Result.LastPlayed
                                                mDBElement.TVEpisode.Playcount = Result.PlayCount
                                            End If
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.TVEpisode.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.TVEpisode.Title))
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.TVEpisode.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                        'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            logger.Warn(String.Format("[KodiInterface] [GenericRunCallBack]: Hostname ({0}) not found in host list!", _SpecialSettings.GetWatchedStateHost))
                        End If
                    End If

                Case Enums.ModuleEventType.ScraperMulti_TVSeason, Enums.ModuleEventType.ScraperMulti_TVShow, Enums.ModuleEventType.ScraperSingle_TVSeason, Enums.ModuleEventType.ScraperSingle_TVShow
                    If mDBElement IsNot Nothing AndAlso Not String.IsNullOrEmpty(_SpecialSettings.GetWatchedStateHost) Then
                        mHost = _SpecialSettings.Hosts.FirstOrDefault(Function(f) f.Label = _SpecialSettings.GetWatchedStateHost)
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                If mDBElement.Episodes IsNot Nothing Then
                                    For Each tEpisode In mDBElement.Episodes.Where(Function(f) f.FilenameSpecified)
                                        If tEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(tEpisode, True) Then
                                            If tEpisode.NfoPathSpecified Then
                                                'run task
                                                Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_TVEpisode(tEpisode, GenericSubEventProgressAsync, GenericEventProcess))
                                                If Result IsNot Nothing Then
                                                    If Not Result.AlreadyInSync Then
                                                        tEpisode.TVEpisode.LastPlayed = Result.LastPlayed
                                                        tEpisode.TVEpisode.Playcount = Result.PlayCount
                                                    End If
                                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", tEpisode.TVEpisode.Title), New Bitmap(My.Resources.logo)}))
                                                Else
                                                    logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", tEpisode.TVEpisode.Title))
                                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", tEpisode.TVEpisode.Title), Nothing}))
                                                    getError = True
                                                End If
                                            Else
                                                logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                                'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                                getError = True
                                            End If
                                        Else
                                            logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                            getError = True
                                        End If
                                    Next
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            logger.Warn(String.Format("[KodiInterface] [GenericRunCallBack]: Hostname ({0}) not found in host list!", _SpecialSettings.GetWatchedStateHost))
                        End If
                    End If

                'Remove Movie
                Case Enums.ModuleEventType.Remove_Movie
                    If mDBElement.FilenameSpecified Then
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                'run task
                                If Await Task.Run(Function() _APIKodi.Remove_Movie(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.Movie.Title), New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.Movie.Title))
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.Movie.Title), Nothing}))
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            For Each tHost As Host In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.Movie).Count > 0)
                                Dim _APIKodi As New Kodi.APIKodi(tHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.Remove_Movie(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.Movie.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.Movie.Title))
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.Movie.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Next
                        End If
                    Else
                        logger.Warn("[KodiInterface] [GenericRunCallBack]: No file name specified")
                    End If

                'Remove TVEpisode
                Case Enums.ModuleEventType.Remove_TVEpisode
                    If mDBElement.FilenameSpecified Then

                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                'run task
                                If Await Task.Run(Function() _APIKodi.Remove_TVEpisode(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.TVEpisode.Title), New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.TVEpisode.Title))
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.TVEpisode.Title), Nothing}))
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                Dim _APIKodi As New Kodi.APIKodi(tHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.Remove_TVEpisode(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.TVEpisode.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.TVEpisode.Title))
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.TVEpisode.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Next
                        End If
                    Else
                        logger.Warn("[KodiInterface] [GenericRunCallBack]: No file name specified")
                    End If

                    'Remove TVShow
                Case Enums.ModuleEventType.Remove_TVShow
                    If mDBElement.ShowPathSpecified Then
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                'run task
                                If Await Task.Run(Function() _APIKodi.Remove_TVShow(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.TVShow.Title), New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.TVShow.Title))
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.TVShow.Title), Nothing}))
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                Dim _APIKodi As New Kodi.APIKodi(tHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.Remove_TVShow(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1031, "Removal OK"), ": ", mDBElement.TVShow.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Removal failed:  ", mDBElement.TVShow.Title))
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1032, "Removal failed"), ": ", mDBElement.TVShow.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Next
                        End If
                    Else
                        logger.Warn("[KodiInterface] [GenericRunCallBack]: No tvshow path specified")
                    End If

                'Sync Movie
                Case Enums.ModuleEventType.Sync_Movie
                    If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(mDBElement, True) Then
                        If mDBElement.NfoPathSpecified Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_Movie(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.Movie.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.Movie.Title))
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.Movie.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Else
                                For Each tHost As Host In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.Movie).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)

                                    'connection test
                                    If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                        'run task
                                        If Await Task.Run(Function() _APIKodi.UpdateInfo_Movie(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.Movie.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.Movie.Title))
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.Movie.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        getError = True
                                    End If
                                Next
                            End If
                        Else
                            logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                            getError = True
                        End If
                    Else
                        logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                        getError = True
                    End If

                    'Sync MovieSet
                Case Enums.ModuleEventType.Sync_MovieSet
                    If mDBElement.MoviesInSetSpecified Then
                        If mHost IsNot Nothing Then
                            Dim _APIKodi As New Kodi.APIKodi(mHost)

                            'connection test
                            If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                'run task
                                If Await Task.Run(Function() _APIKodi.UpdateInfo_MovieSet(mDBElement, _SpecialSettings.SendNotifications)) Then
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MovieSet.Title), New Bitmap(My.Resources.logo)}))
                                Else
                                    logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MovieSet.Title))
                                    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MovieSet.Title), Nothing}))
                                    getError = True
                                End If
                            Else
                                getError = True
                            End If
                        Else
                            For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso Not String.IsNullOrEmpty(f.MovieSetArtworksPath))
                                Dim _APIKodi As New Kodi.APIKodi(tHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_MovieSet(mDBElement, _SpecialSettings.SendNotifications)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.MovieSet.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.MovieSet.Title))
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.MovieSet.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Next
                        End If
                    Else
                        logger.Warn("[KodiInterface] [GenericRunCallBack]: No movies in set!")
                        getError = True
                    End If

                    'Sync TVEpisode
                Case Enums.ModuleEventType.Sync_TVEpisode
                    If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(mDBElement, True) Then
                        If mDBElement.NfoPathSpecified Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_TVEpisode(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.TVEpisode.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.TVEpisode.Title))
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.TVEpisode.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)

                                    'connection test
                                    If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                        'run task
                                        If Await Task.Run(Function() _APIKodi.UpdateInfo_TVEpisode(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.TVEpisode.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.TVEpisode.Title))
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.TVEpisode.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        getError = True
                                    End If
                                Next
                            End If
                        Else
                            logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                            getError = True
                        End If
                    Else
                        logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                        getError = True
                    End If

                    'Sync TVSeason
                Case Enums.ModuleEventType.Sync_TVSeason
                    If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(mDBElement, True) Then
                        If mDBElement.IDSpecified Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_TVSeason(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.TVSeason.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.TVSeason.Title))
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.TVSeason.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)

                                    'connection test
                                    If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                        'run task
                                        If Await Task.Run(Function() _APIKodi.UpdateInfo_TVSeason(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.TVSeason.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.TVSeason.Title))
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.TVSeason.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        getError = True
                                    End If
                                Next
                            End If
                        Else
                            logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                            getError = True
                        End If
                    Else
                        logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                        getError = True
                    End If

                    'Sync TVShow
                Case Enums.ModuleEventType.Sync_TVShow
                    If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(mDBElement, True) Then
                        If mDBElement.NfoPathSpecified Then
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    'run task
                                    If Await Task.Run(Function() _APIKodi.UpdateInfo_TVShow(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.TVShow.Title), New Bitmap(My.Resources.logo)}))
                                    Else
                                        logger.Warn(String.Concat("[KodiInterface] [", mHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.TVShow.Title))
                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.TVShow.Title), Nothing}))
                                        getError = True
                                    End If
                                Else
                                    getError = True
                                End If
                            Else
                                For Each tHost In _SpecialSettings.Hosts.Where(Function(f) f.RealTimeSync AndAlso f.Sources.Where(Function(c) c.ContentType = Enums.ContentType.TV).Count > 0)
                                    Dim _APIKodi As New Kodi.APIKodi(tHost)

                                    'connection test
                                    If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                        'run task
                                        If Await Task.Run(Function() _APIKodi.UpdateInfo_TVShow(mDBElement, _SpecialSettings.SendNotifications, GenericSubEventProgressAsync, GenericEventProcess)) Then
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.TVShow.Title), New Bitmap(My.Resources.logo)}))
                                        Else
                                            logger.Warn(String.Concat("[KodiInterface] [", tHost.Label, "] [GenericRunCallBack] | Sync Failed:  ", mDBElement.TVShow.Title))
                                            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", String.Concat(tHost.Label, " | ", Master.eLang.GetString(1445, "Sync Failed"), ": ", mDBElement.TVShow.Title), Nothing}))
                                            getError = True
                                        End If
                                    Else
                                        getError = True
                                    End If
                                Next
                            End If
                        Else
                            logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                            getError = True
                        End If
                    Else
                        logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                        getError = True
                    End If

                    'General Tasks
                Case Enums.ModuleEventType.Task
                    Select Case mInternalType

                        'Get Playcount
                        Case InternalType.GetPlaycount
                            If mDBElement IsNot Nothing AndAlso mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)

                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    Select Case mDBElement.ContentType

                                        'Get Movie Playcount
                                        Case Enums.ContentType.Movie
                                            If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(mDBElement, True) Then
                                                If mDBElement.NfoPathSpecified Then
                                                    'run task
                                                    Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_Movie(mDBElement, GenericSubEventProgressAsync, GenericEventProcess))
                                                    If Result IsNot Nothing Then
                                                        If Not Result.AlreadyInSync Then
                                                            mDBElement.Movie.LastPlayed = Result.LastPlayed
                                                            mDBElement.Movie.PlayCount = Result.PlayCount
                                                            Master.DB.Save_Movie(mDBElement, False, True, False, False)
                                                            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_Movie, New List(Of Object)(New Object() {mDBElement.ID}))
                                                        End If
                                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.Movie.Title), New Bitmap(My.Resources.logo)}))
                                                    End If
                                                Else
                                                    logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                                    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, "Kodi Interface", Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                                    getError = True
                                                End If
                                            Else
                                                logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                                getError = True
                                            End If

                                        'Get TVEpisode Playcount
                                        Case Enums.ContentType.TVEpisode
                                            If mDBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(mDBElement, True) Then
                                                If mDBElement.NfoPathSpecified Then
                                                    'run task
                                                    Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_TVEpisode(mDBElement, GenericSubEventProgressAsync, GenericEventProcess))
                                                    If Result IsNot Nothing Then
                                                        If Not Result.AlreadyInSync Then
                                                            mDBElement.TVEpisode.LastPlayed = Result.LastPlayed
                                                            mDBElement.TVEpisode.Playcount = Result.PlayCount
                                                            Master.DB.Save_TVEpisode(mDBElement, False, True, False, False, True)
                                                            RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {mDBElement.ID}))
                                                        End If
                                                        ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", mDBElement.TVEpisode.Title), New Bitmap(My.Resources.logo)}))
                                                    End If
                                                Else
                                                    logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                                    'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                                    getError = True
                                                End If
                                            Else
                                                logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                                getError = True
                                            End If

                                        'Get TVSeason / TVShow Playcount
                                        Case Enums.ContentType.TVSeason, Enums.ContentType.TVShow
                                            If mDBElement.Episodes IsNot Nothing Then
                                                For Each tEpisode In mDBElement.Episodes
                                                    If tEpisode.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(tEpisode, True) Then
                                                        If tEpisode.NfoPathSpecified Then
                                                            'run task
                                                            Dim Result = Await Task.Run(Function() _APIKodi.GetPlaycount_TVEpisode(tEpisode, GenericSubEventProgressAsync, GenericEventProcess))
                                                            If Result IsNot Nothing Then
                                                                If Not Result.AlreadyInSync Then
                                                                    tEpisode.TVEpisode.LastPlayed = Result.LastPlayed
                                                                    tEpisode.TVEpisode.Playcount = Result.PlayCount
                                                                    Master.DB.Save_TVEpisode(tEpisode, False, True, False, False, True)
                                                                    RaiseEvent GenericEvent(Enums.ModuleEventType.AfterEdit_TVEpisode, New List(Of Object)(New Object() {tEpisode.ID}))
                                                                End If
                                                                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", Nothing, "Kodi Interface", String.Concat(mHost.Label, " | ", Master.eLang.GetString(1444, "Sync OK"), ": ", tEpisode.TVEpisode.Title), New Bitmap(My.Resources.logo)}))
                                                            End If
                                                        Else
                                                            logger.Warn("[KodiInterface] [GenericRunCallBack]: Please Scrape In Ember First!")
                                                            'ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"error", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1442, "Please Scrape In Ember First!"), Nothing}))
                                                            getError = True
                                                        End If
                                                    Else
                                                        logger.Warn("[KodiInterface] [GenericRunCallBack]: Not online!")
                                                        getError = True
                                                    End If
                                                Next
                                            End If
                                    End Select
                                Else
                                    getError = True
                                End If
                            End If

                            'Clean Video Library
                        Case InternalType.VideoLibrary_Clean
                            If mHost IsNot Nothing Then
                                Dim _APIKodi As New Kodi.APIKodi(mHost)
                                'connection test
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    Await _APIKodi.VideoLibrary_Clean.ConfigureAwait(False)
                                    While Await _APIKodi.IsScanningVideo()
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
                                If Await Task.Run(Function() _APIKodi.TestConnectionToHost) Then
                                    Await _APIKodi.VideoLibrary_Scan.ConfigureAwait(False)
                                    While Await _APIKodi.IsScanningVideo()
                                        Threading.Thread.Sleep(1000)
                                    End While
                                Else
                                    getError = True
                                End If
                            End If
                    End Select
            End Select
        Else
            logger.Warn("[KodiInterface] [GenericRunCallBack]: No Host Configured!")
            getError = True
        End If

        If Not getError Then
            Return True
        Else
            Return False
        End If
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
        'Single Host
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
        Dim mnuHostSyncPlaycounts As New ToolStripMenuItem
        mnuHostSyncPlaycounts.Text = "Sync Playcount"
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
        mnuMainToolsKodi.Text = "Kodi Interface"
        mnuMainToolsKodi.Tag = New Structures.ModulesMenus With {.ForMovies = True, .IfTabMovies = True, .ForMovieSets = True, .IfTabMovieSets = True, .ForTVShows = True, .IfTabTVShows = True}
        CreateToolsMenu(mnuMainToolsKodi)
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolStripItem(tsi, mnuMainToolsKodi)

        'mnuTrayTools
        mnuTrayToolsKodi.DropDownItems.Clear()
        mnuTrayToolsKodi.Image = New Bitmap(My.Resources.icon)
        mnuTrayToolsKodi.Text = "Kodi Interface"
        CreateToolsMenu(mnuTrayToolsKodi)
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolStripItem(tsi, mnuTrayToolsKodi)

        'cmnuMovies
        cmnuKodi_Movies.DropDownItems.Clear()
        cmnuKodi_Movies.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_Movies.Text = "Kodi Interface"
        CreateContextMenu(cmnuKodi_Movies, Enums.ContentType.Movie)
        SetToolStripItem_Movies(cmnuSep_Movies)
        SetToolStripItem_Movies(cmnuKodi_Movies)

        'cmnuMovieSets
        cmnuKodi_MovieSets.DropDownItems.Clear()
        cmnuKodi_MovieSets.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_MovieSets.Text = "Kodi Interface"
        CreateContextMenu(cmnuKodi_MovieSets, Enums.ContentType.MovieSet)
        AddToolStripItem_MovieSets(cmnuSep_MovieSets)
        AddToolStripItem_MovieSets(cmnuKodi_MovieSets)

        'cmnuTVEpisodes
        cmnuKodi_TVEpisodes.DropDownItems.Clear()
        cmnuKodi_TVEpisodes.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_TVEpisodes.Text = "Kodi Interface"
        CreateContextMenu(cmnuKodi_TVEpisodes, Enums.ContentType.TVEpisode)
        AddToolStripItem_TVEpisodes(cmnuSep_TVEpisodes)
        AddToolStripItem_TVEpisodes(cmnuKodi_TVEpisodes)

        'cmnuTVSeasons
        cmnuKodi_TVSeasons.DropDownItems.Clear()
        cmnuKodi_TVSeasons.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_TVSeasons.Text = "Kodi Interface"
        CreateContextMenu(cmnuKodi_TVSeasons, Enums.ContentType.TVSeason)
        AddToolStripItem_TVSeasons(cmnuSep_TVSeasons)
        AddToolStripItem_TVSeasons(cmnuKodi_TVSeasons)

        'cmnuTVShows
        cmnuKodi_TVShows.DropDownItems.Clear()
        cmnuKodi_TVShows.Image = New Bitmap(My.Resources.icon)
        cmnuKodi_TVShows.Text = "Kodi Interface"
        CreateContextMenu(cmnuKodi_TVShows, Enums.ContentType.TVShow)
        AddToolStripItem_TVShows(cmnuSep_TVShows)
        AddToolStripItem_TVShows(cmnuKodi_TVShows)

        'Task Manager
        lblTaskManagerStatus.Text = "No Pending Tasks"
        lblTaskManagerTitle.Text = "Kodi Interface Task Manager"
        Dim ts As ToolStrip = DirectCast(ModulesManager.Instance.RuntimeObjects.MainToolStrip, ToolStrip)
        AddToolStripItem(ts, tssTaskManager1)
        AddToolStripItem(ts, tssTaskManager2)
        AddToolStripItem(ts, tspTaskManager)
        AddToolStripItem(ts, tssTaskManager3)
        AddToolStripItem(ts, tssTaskManager4)
        AddToolStripItem(ts, lblTaskManagerStatus)
        AddToolStripItem(ts, tssTaskManager5)
        AddToolStripItem(ts, tssTaskManager6)
        AddToolStripItem(ts, lblTaskManagerTitle)
        AddToolStripItem(ts, tssTaskManager7)
        AddToolStripItem(ts, tssTaskManager8)
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
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(mnuMainToolsKodi)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        tsi.DropDownItems.Remove(mnuTrayToolsKodi)

        'cmnuMovies
        RemoveToolStripItem_Movies(cmnuSep_Movies)
        RemoveToolStripItem_Movies(cmnuKodi_Movies)
        'cmnuEpisodes
        RemoveToolStripItem_TVEpisodes(cmnuSep_TVEpisodes)
        RemoveToolStripItem_TVEpisodes(cmnuKodi_TVEpisodes)
        'cmnuShows
        RemoveToolStripItem_TVShows(cmnuSep_TVShows)
        RemoveToolStripItem_TVShows(cmnuKodi_TVShows)
        'cmnuSeasons
        RemoveToolStripItem_TVSeasons(cmnuSep_TVSeasons)
        RemoveToolStripItem_TVSeasons(cmnuKodi_TVSeasons)
        'cmnuSets
        RemoveToolStripItem_MovieSets(cmnuSep_MovieSets)
        RemoveToolStripItem_MovieSets(cmnuKodi_MovieSets)

        'Task Manager
        Dim ts As ToolStrip = DirectCast(ModulesManager.Instance.RuntimeObjects.MainToolStrip, ToolStrip)
        ts.Items.Remove(lblTaskManagerStatus)
        ts.Items.Remove(lblTaskManagerTitle)
        ts.Items.Remove(tspTaskManager)
        ts.Items.Remove(tssTaskManager1)
        ts.Items.Remove(tssTaskManager2)
        ts.Items.Remove(tssTaskManager3)
        ts.Items.Remove(tssTaskManager4)
        ts.Items.Remove(tssTaskManager5)
        ts.Items.Remove(tssTaskManager6)
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
        _setup.chkGetWatchedState.Checked = _SpecialSettings.GetWatchedState
        _setup.chkGetWatchedStateBeforeEdit_Movie.Checked = _SpecialSettings.GetWatchedStateBeforeEdit_Movie
        _setup.chkGetWatchedStateBeforeEdit_TVEpisode.Checked = _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode
        _setup.chkGetWatchedStateScraperMulti_Movie.Checked = _SpecialSettings.GetWatchedStateScraperMulti_Movie
        _setup.chkGetWatchedStateScraperMulti_TVEpisode.Checked = _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode
        _setup.chkGetWatchedStateScraperSingle_Movie.Checked = _SpecialSettings.GetWatchedStateScraperSingle_Movie
        _setup.chkGetWatchedStateScraperSingle_TVEpisode.Checked = _SpecialSettings.GetWatchedStateScraperSingle_TVEpisode
        _setup.chkNotification.Checked = _SpecialSettings.SendNotifications
        If _SpecialSettings.GetWatchedState Then
            _setup.cbGetWatchedStateHost.Enabled = True
        Else
            _setup.cbGetWatchedStateHost.Enabled = False
        End If
        _setup.HostList = _SpecialSettings.Hosts
        _setup.lbHosts.Items.Clear()
        For Each tHost As Host In _setup.HostList
            _setup.cbGetWatchedStateHost.Items.Add(tHost.Label)
            _setup.lbHosts.Items.Add(tHost.Label)
        Next
        _setup.cbGetWatchedStateHost.SelectedIndex = _setup.cbGetWatchedStateHost.FindStringExact(_SpecialSettings.GetWatchedStateHost)

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

    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = _setup.chkEnabled.Checked
        _SpecialSettings.SendNotifications = _setup.chkNotification.Checked
        _SpecialSettings.GetWatchedState = _setup.chkGetWatchedState.Checked AndAlso _setup.cbGetWatchedStateHost.SelectedItem IsNot Nothing
        _SpecialSettings.GetWatchedStateBeforeEdit_Movie = _setup.chkGetWatchedStateBeforeEdit_Movie.Checked
        _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode = _setup.chkGetWatchedStateBeforeEdit_TVEpisode.Checked
        _SpecialSettings.GetWatchedStateScraperMulti_Movie = _setup.chkGetWatchedStateScraperMulti_Movie.Checked
        _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode = _setup.chkGetWatchedStateScraperMulti_TVEpisode.Checked
        _SpecialSettings.GetWatchedStateScraperSingle_Movie = _setup.chkGetWatchedStateScraperSingle_Movie.Checked
        _SpecialSettings.GetWatchedStateScraperSingle_TVEpisode = _setup.chkGetWatchedStateScraperSingle_TVEpisode.Checked
        _SpecialSettings.GetWatchedStateHost = If(_setup.cbGetWatchedStateHost.SelectedItem IsNot Nothing, _setup.cbGetWatchedStateHost.SelectedItem.ToString(), String.Empty)

        SaveSettings()

        If Enabled Then PopulateMenus()
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = mHost, .mInternalType = InternalType.GetPlaycount, .mType = Enums.ModuleEventType.Task})
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
    ''' Get episode playcount on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Get TVEpisode Playcount"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuHostGetPlaycount_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mInternalType = InternalType.GetPlaycount, .mType = Enums.ModuleEventType.Task})
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                    'add job to tasklist and get everything done
                    AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mInternalType = InternalType.GetPlaycount, .mType = Enums.ModuleEventType.Task})
                End If
            Next
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(ID, True, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mInternalType = InternalType.GetPlaycount, .mType = Enums.ModuleEventType.Task})
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
    ''' Remove movie details on Host DB
    ''' </summary>
    ''' <param name="sender">context menu "Remove Movie"</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmnuHostRemoveItem_Movie_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
                'add job to tasklist and get everything done
                AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Remove_Movie})
            Next
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
                'add job to tasklist and get everything done
                AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Remove_TVEpisode})
            Next
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)
                'add job to tasklist and get everything done
                AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Remove_TVShow})
            Next
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovies.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idMovie").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_Movie(ID)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_Movie(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_Movie})
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSet").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_MovieSet(ID)
                If DBElement.MovieSet.TitleSpecified Then
                    'add job to tasklist and get everything done
                    AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_MovieSet})
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
    Private Sub cmnuHostSyncItem_TVEpisode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVEpisodes.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idEpisode").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVEpisode(ID, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVEpisode(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_TVEpisode})
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
    Private Sub cmnuHostSyncItem_TVSeason_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, False)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                    'add job to tasklist and get everything done
                    AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_TVSeason})
                End If
            Next
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVSeasons.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idSeason").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVSeason(ID, True, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                    'add job to tasklist and get everything done
                    AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_TVSeason})
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
    Private Sub cmnuHostSyncItem_TVShow_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(ID, False, False)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_TVShow})
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
            For Each sRow As DataGridViewRow In ModulesManager.Instance.RuntimeObjects.MediaListTVShows.SelectedRows
                Dim ID As Long = Convert.ToInt64(sRow.Cells("idShow").Value)
                Dim DBElement As Database.DBElement = Master.DB.Load_TVShow(ID, True, True)
                If DBElement.IsOnline OrElse FileUtils.Common.CheckOnlineStatus_TVShow(DBElement, True) Then
                    If DBElement.NfoPathSpecified Then
                        'add job to tasklist and get everything done
                        AddTask(New KodiTask With {.mDBElement = DBElement, .mHost = Host, .mType = Enums.ModuleEventType.Sync_TVShow})
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
    Private Sub mnuHostCleanVideoLibrary_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1450, "Cleaning Video Library..."), New Bitmap(My.Resources.logo)}))

            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mHost = Host, .mInternalType = InternalType.VideoLibrary_Clean, .mType = Enums.ModuleEventType.Task})
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
    Private Sub mnuHostScanVideoLibrary_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1448, "Updating Video Library..."), New Bitmap(My.Resources.logo)}))

            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mHost = Host, .mInternalType = InternalType.VideoLibrary_Update, .mType = Enums.ModuleEventType.Task})
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
    Private Sub mnuHostGetPlaycount_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim Host As Host = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, Host)
        If Host IsNot Nothing Then
            Dim _APIKodi As New Kodi.APIKodi(Host)
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Host.Label & " | " & Master.eLang.GetString(1448, "Updating Video Library..."), New Bitmap(My.Resources.logo)}))

            'add job to tasklist and get everything done
            AddTask(New KodiTask With {.mHost = Host, .mInternalType = InternalType.GetPlaycount, .mType = Enums.ModuleEventType.Task})
        Else
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"info", 1, Master.eLang.GetString(1422, "Kodi Interface"), Master.eLang.GetString(1447, "No Host Configured!"), Nothing}))
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

    Public Sub SetToolStripItem_Movies(value As ToolStripItem)
        If ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.InvokeRequired Then
            ModulesManager.Instance.RuntimeObjects.ContextMenuMovieList.Invoke(New Delegate_AddToolStripItem(AddressOf SetToolStripItem_Movies), New Object() {value})
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

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent ModuleEnabledChanged(_Name, State, 0)
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Structure GenericEventCallBackAsync

        Dim tEventType As Enums.ModuleEventType
        Dim tParams As List(Of Object)

    End Structure

    Public Structure GenericSubEventCallBackAsync

        Dim tGenericEventCallBackAsync As GenericEventCallBackAsync
        Dim tProgress As IProgress(Of GenericEventCallBackAsync)

    End Structure

    Private Enum InternalType

        None = 0
        GetPlaycount = 1
        VideoLibrary_Clean = 2
        VideoLibrary_Update = 3

    End Enum
    ''' <summary>
    ''' structure used to store Update Movie/TV/Movieset-Tasks for Kodi Interface
    ''' </summary>
    ''' <remarks></remarks>
    Private Structure KodiTask

        Dim mDBElement As Database.DBElement
        Dim mHost As Host
        Dim mInternalType As InternalType
        Dim mType As Enums.ModuleEventType

    End Structure

    <Serializable()>
    <XmlRoot("interface.kodi")>
    Class SpecialSettings

#Region "Fields"

        Private _hosts As New List(Of Host)
        Private _sendnotifications As Boolean
        Private _getwatchedstate As Boolean
        Private _getwatchedstatebeforeedit_movie As Boolean
        Private _getwatchedstatebeforeedit_tvepisode As Boolean
        Private _getwatchedstatescrapermulti_movie As Boolean
        Private _getwatchedstatescrapermulti_tvepisode As Boolean
        Private _getwatchedstatescrapersingle_movie As Boolean
        Private _getwatchedstatescrapersingle_tvepisode As Boolean
        Private _getwatchedstatehost As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("getwatchedstate")>
        Public Property GetWatchedState() As Boolean
            Get
                Return _getwatchedstate
            End Get
            Set(ByVal value As Boolean)
                _getwatchedstate = value
            End Set
        End Property

        <XmlElement("getwatchedstatebeforeedit_movie")>
        Public Property GetWatchedStateBeforeEdit_Movie() As Boolean
            Get
                Return _getwatchedstatebeforeedit_movie
            End Get
            Set(ByVal value As Boolean)
                _getwatchedstatebeforeedit_movie = value
            End Set
        End Property

        <XmlElement("getwatchedstatebeforeedit_tvepisode")>
        Public Property GetWatchedStateBeforeEdit_TVEpisode() As Boolean
            Get
                Return _getwatchedstatebeforeedit_tvepisode
            End Get
            Set(ByVal value As Boolean)
                _getwatchedstatebeforeedit_tvepisode = value
            End Set
        End Property

        <XmlElement("getwatchedstatehost")>
        Public Property GetWatchedStateHost() As String
            Get
                Return _getwatchedstatehost
            End Get
            Set(ByVal value As String)
                _getwatchedstatehost = value
            End Set
        End Property

        <XmlElement("getwatchedstatescrapermulti_movie")>
        Public Property GetWatchedStateScraperMulti_Movie() As Boolean
            Get
                Return _getwatchedstatescrapermulti_movie
            End Get
            Set(ByVal value As Boolean)
                _getwatchedstatescrapermulti_movie = value
            End Set
        End Property

        <XmlElement("getwatchedstatescrapermulti_tvepisode")>
        Public Property GetWatchedStateScraperMulti_TVEpisode() As Boolean
            Get
                Return _getwatchedstatescrapermulti_tvepisode
            End Get
            Set(ByVal value As Boolean)
                _getwatchedstatescrapermulti_tvepisode = value
            End Set
        End Property

        <XmlElement("getwatchedstatescrapersingle_movie")>
        Public Property GetWatchedStateScraperSingle_Movie() As Boolean
            Get
                Return _getwatchedstatescrapersingle_movie
            End Get
            Set(ByVal value As Boolean)
                _getwatchedstatescrapersingle_movie = value
            End Set
        End Property

        <XmlElement("getwatchedstatescrapersingle_tvepisode")>
        Public Property GetWatchedStateScraperSingle_TVEpisode() As Boolean
            Get
                Return _getwatchedstatescrapersingle_tvepisode
            End Get
            Set(ByVal value As Boolean)
                _getwatchedstatescrapersingle_tvepisode = value
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

        <XmlElement("sendnotifications")>
        Public Property SendNotifications() As Boolean
            Get
                Return _sendnotifications
            End Get
            Set(ByVal value As Boolean)
                _sendnotifications = value
            End Set
        End Property

#End Region 'Properties

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Sub Clear()
            _hosts.Clear()
            _sendnotifications = False
            _getwatchedstate = False
            _getwatchedstatebeforeedit_movie = False
            _getwatchedstatebeforeedit_tvepisode = False
            _getwatchedstatehost = String.Empty
            _getwatchedstatescrapermulti_movie = False
            _getwatchedstatescrapermulti_tvepisode = False
            _getwatchedstatescrapersingle_movie = False
            _getwatchedstatescrapersingle_tvepisode = False
        End Sub

#End Region 'Methods

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

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Sub Clear()
            _address = "localhost"
            _moviesetartworkspath = String.Empty
            _label = "New Host"
            _password = String.Empty
            _port = 80
            _realtimesync = False
            _sources.Clear()
            _username = "kodi"
        End Sub

#End Region 'Methods

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

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Sub Clear()
            _contenttype = Enums.ContentType.Movie
            _localpath = String.Empty
            _remotepath = String.Empty
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class
