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
Imports System.Windows.Forms
Imports System.Drawing
Imports NLog
Imports System.Xml.Serialization

Public Class Trakt_Generic
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty
    Private WithEvents cmnuTrayToolsTrakt As New System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuMainToolsTrakt As New System.Windows.Forms.ToolStripMenuItem
    Private _enabled As Boolean = False
    Private _Name As String = "Trakt.tv Manager"
    Private MySettings As New _MySettings

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As EmberAPI.Enums.ModuleEventType, ByRef _params As System.Collections.Generic.List(Of Object)) Implements EmberAPI.Interfaces.GenericModule.GenericEvent

    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged

    Public Event ModuleSettingsChanged() Implements EmberAPI.Interfaces.GenericModule.ModuleSettingsChanged

    Public Event SetupNeedsRestart() Implements EmberAPI.Interfaces.GenericModule.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic, Enums.ModuleEventType.CommandLine})
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
    ''' Commandline call: Update/Sync playcounts of movies and episodes
    ''' </summary>
    ''' <remarks>
    ''' TODO: Needs some testing (error handling..)!? Idea: Can be executed via commandline to update/sync playcounts of movies and episodes
    ''' </remarks>
    Public Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object), ByRef _singleobjekt As Object, ByRef _dbelement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.GenericModule.RunGeneric
        Try
            dlgTrakttvManager.CLSyncPlaycount()
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, mnuMainToolsTrakt)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, cmnuTrayToolsTrakt)
    End Sub

    Sub Enable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        mnuMainToolsTrakt.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsTrakt.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TopMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsTrakt)

        'cmnuTrayTools
        cmnuTrayToolsTrakt.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsTrakt.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsTrakt)
    End Sub

    Public Sub AddToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_AddToolsStripItem(AddressOf AddToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Add(value)
        End If
    End Sub

    Public Sub RemoveToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
        If control.Owner.InvokeRequired Then
            control.Owner.Invoke(New Delegate_RemoveToolsStripItem(AddressOf RemoveToolsStripItem), New Object() {control, value})
        Else
            control.DropDownItems.Remove(value)
        End If
    End Sub

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent ModuleEnabledChanged(Me._Name, State, 0)
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
        Me._setup = New frmSettingsHolder
        Me._setup.chkEnabled.Checked = Me._enabled
        Me._setup.txtUsername.Text = MySettings.Username
        Me._setup.txtPassword.Text = MySettings.Password
        Me._setup.chkGetShowProgress.Checked = MySettings.GetShowProgress
        'LastPlayed
        Me._setup.chkSyncLastPlayedEditMovies.Checked = MySettings.SyncLastPlayedEditMovies
        Me._setup.chkSyncLastPlayedEditEpisodes.Checked = MySettings.SyncLastPlayedEditEpisodes
        Me._setup.chkSyncLastPlayedMultiEpisodes.Checked = MySettings.SyncLastPlayedMultiEpisodes
        Me._setup.chkSyncLastPlayedMultiMovies.Checked = MySettings.SyncLastPlayedMultiMovies
        Me._setup.chkSyncLastPlayedSingleEpisodes.Checked = MySettings.SyncLastPlayedSingleEpisodes
        Me._setup.chkSyncLastPlayedSingleMovies.Checked = MySettings.SyncLastPlayedSingleMovies
        'Playcount
        Me._setup.chkSyncPlaycountEditMovies.Checked = MySettings.SyncPlaycountEditMovies
        Me._setup.chkSyncPlaycountEditEpisodes.Checked = MySettings.SyncPlaycountEditEpisodes
        Me._setup.chkSyncPlaycountMultiEpisodes.Checked = MySettings.SyncPlaycountMultiEpisodes
        Me._setup.chkSyncPlaycountMultiMovies.Checked = MySettings.SyncPlaycountMultiMovies
        Me._setup.chkSyncPlaycountSingleEpisodes.Checked = MySettings.SyncPlaycountSingleEpisodes
        Me._setup.chkSyncPlaycountSingleMovies.Checked = MySettings.SyncPlaycountSingleMovies

        SPanel.Name = Me._Name
        SPanel.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        SPanel.Prefix = "Trakt_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(Me._enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings
        AddHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Private Sub MyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsTrakt.Click, cmnuTrayToolsTrakt.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))

        Using dTrakttvManager As New dlgTrakttvManager
            dTrakttvManager.ShowDialog()
        End Using

        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub
    Sub LoadSettings()
        MySettings.Username = clsAdvancedSettings.GetSetting("Username", "")
        MySettings.Password = clsAdvancedSettings.GetSetting("Password", "")
        MySettings.GetShowProgress = clsAdvancedSettings.GetBooleanSetting("GetShowProgress", False)
        'LastPlayed
        MySettings.SyncLastPlayedEditMovies = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedEditMovies", False)
        MySettings.SyncLastPlayedEditEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedEditEpisodes", False)
        MySettings.SyncLastPlayedMultiEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedMultiEpisodes", False)
        MySettings.SyncLastPlayedMultiMovies = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedMultiMovies", False)
        MySettings.SyncLastPlayedSingleEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedSingleEpisodes", False)
        MySettings.SyncLastPlayedSingleMovies = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedSingleMovies", False)
        'Playcount
        MySettings.SyncPlaycountEditMovies = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountEditMovies", False)
        MySettings.SyncPlaycountEditEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountEditEpisodes", False)
        MySettings.SyncPlaycountMultiEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountMultiEpisodes", False)
        MySettings.SyncPlaycountMultiMovies = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountMultiMovies", False)
        MySettings.SyncPlaycountSingleEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountSingleEpisodes", False)
        MySettings.SyncPlaycountSingleMovies = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountSingleMovies", False)
    End Sub
    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Me.Enabled = Me._setup.chkEnabled.Checked
        MySettings.Username = Me._setup.txtUsername.Text
        MySettings.Password = Me._setup.txtPassword.Text
        MySettings.GetShowProgress = Me._setup.chkGetShowProgress.Checked
        'LastPlayed
        MySettings.SyncLastPlayedEditMovies = Me._setup.chkSyncLastPlayedEditMovies.Checked
        MySettings.SyncLastPlayedEditEpisodes = Me._setup.chkSyncLastPlayedEditEpisodes.Checked
        MySettings.SyncLastPlayedMultiEpisodes = Me._setup.chkSyncLastPlayedMultiEpisodes.Checked
        MySettings.SyncLastPlayedMultiMovies = Me._setup.chkSyncLastPlayedMultiMovies.Checked
        MySettings.SyncLastPlayedSingleEpisodes = Me._setup.chkSyncLastPlayedSingleEpisodes.Checked
        MySettings.SyncLastPlayedSingleMovies = Me._setup.chkSyncLastPlayedSingleMovies.Checked
        'Playcount
        MySettings.SyncPlaycountEditMovies = Me._setup.chkSyncPlaycountEditMovies.Checked
        MySettings.SyncPlaycountEditEpisodes = Me._setup.chkSyncPlaycountEditEpisodes.Checked
        MySettings.SyncPlaycountMultiEpisodes = Me._setup.chkSyncPlaycountMultiEpisodes.Checked
        MySettings.SyncPlaycountMultiMovies = Me._setup.chkSyncPlaycountMultiMovies.Checked
        MySettings.SyncPlaycountSingleEpisodes = Me._setup.chkSyncPlaycountSingleEpisodes.Checked
        MySettings.SyncPlaycountSingleMovies = Me._setup.chkSyncPlaycountSingleMovies.Checked

        SaveSettings()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetSetting("Username", MySettings.Username)
            settings.SetSetting("Password", MySettings.Password)
            settings.SetBooleanSetting("GetShowProgress", MySettings.GetShowProgress)
            'LastPlayed
            settings.SetBooleanSetting("SyncLastPlayedEditMovies", MySettings.SyncLastPlayedEditMovies)
            settings.SetBooleanSetting("SyncLastPlayedEditEpisodes", MySettings.SyncLastPlayedEditEpisodes)
            settings.SetBooleanSetting("SyncLastPlayedMultiEpisodes", MySettings.SyncLastPlayedMultiEpisodes)
            settings.SetBooleanSetting("SyncLastPlayedMultiMovies", MySettings.SyncLastPlayedMultiMovies)
            settings.SetBooleanSetting("SyncLastPlayedSingleEpisodes", MySettings.SyncLastPlayedSingleEpisodes)
            settings.SetBooleanSetting("SyncLastPlayedSingleMovies", MySettings.SyncLastPlayedSingleMovies)
            'Playcount
            settings.SetBooleanSetting("SyncPlaycountEditMovies", MySettings.SyncPlaycountEditMovies)
            settings.SetBooleanSetting("SyncPlaycountEditEpisodes", MySettings.SyncPlaycountEditEpisodes)
            settings.SetBooleanSetting("SyncPlaycountMultiEpisodes", MySettings.SyncPlaycountMultiEpisodes)
            settings.SetBooleanSetting("SyncPlaycountMultiMovies", MySettings.SyncPlaycountMultiMovies)
            settings.SetBooleanSetting("SyncPlaycountSingleEpisodes", MySettings.SyncPlaycountSingleEpisodes)
            settings.SetBooleanSetting("SyncPlaycountSingleMovies", MySettings.SyncPlaycountSingleMovies)
        End Using
    End Sub

#Region "Sync functions"
    ''Token generated after successfull login to trakt.tv account - without a token no scraping is possible
    'Private _Token As String
    ''collection of watched movies - contains last played date and playcount
    'Private _traktWatchedMovies As IEnumerable(Of TraktAPI.Model.TraktMovieWatched) = Nothing
    ''collection of watched episodes - contains last played date and playcount
    'Private _traktWatchedEpisodes As IEnumerable(Of TraktAPI.Model.TraktEpisodeWatched) = Nothing

    ''' <summary>
    '''  Login to trakttv and retrieve personal video data
    ''' </summary>
    ''' <param name="SpecialSettings">Special settings of trakttv scraper module</param>
    ''' <param name="Scrapermode">0= movie scraper, 1= tv scraper</param>
    ''' <remarks>
    ''' 2015/11/18 Cocotus - First implementation
    ''' Constructor for trakt.tv data scraper - use this to do things that only needs to be done one time
    ''' Connect to trakt.tv and scrape personal video data (personal ratings, last played) only once to minimize amount of queries to trakt.tv server
    ''' </remarks>
    'Public Sub New(ByVal SpecialSettings As Trakttv_Data.SpecialSettings, ByVal Scrapermode As Byte)
    '    Try
    '        _SpecialSettings = SpecialSettings

    '        _Token = Trakttv.TraktMethods.LoginToTrakt(_SpecialSettings.TrakttvUserName, _SpecialSettings.TrakttvPassword)
    '        If String.IsNullOrEmpty(_Token) Then
    '            logger.Error(String.Concat("[New] Can't login to trakt.tv account!"))
    '        Else
    '            'Movie Mode
    '            If Scrapermode = 0 Then
    '                'Retrieve at scraper startup all user video data on trakt.tv like personal playcount, last played, ratings (only need to do this once and NOT for every scraped movie/show)
    '                _traktWatchedMovies = TrakttvAPI.GetWatchedMovies
    '                If _traktWatchedMovies Is Nothing Then
    '                    logger.Error(String.Concat("[New] Could not scrape personal trakt.tv watched data!"))
    '                End If
    '                If _SpecialSettings.UsePersonalRatings Then
    '                    _traktRatedMovies = TrakttvAPI.GetRatedMovies
    '                    If _traktRatedMovies Is Nothing Then
    '                        logger.Error(String.Concat("[New] Could not scrape personal trakt.tv ratings!"))
    '                    End If
    '                End If
    '                'TV Mode
    '            ElseIf Scrapermode = 1 Then
    '                'Retrieve at scraper startup all user video data on trakt.tv like personal playcount, last played, ratings (only need to do this once and NOT for every scraped movie/show)
    '                _traktWatchedEpisodes = TrakttvAPI.GetWatchedEpisodes
    '                If _traktWatchedEpisodes Is Nothing Then
    '                    logger.Error(String.Concat("[New] Could not scrape personal trakt.tv watched data!"))
    '                End If
    '                If _SpecialSettings.UsePersonalRatings Then
    '                    _traktRatedEpisodes = TrakttvAPI.GetRatedEpisodes
    '                    If _traktRatedEpisodes Is Nothing Then
    '                        logger.Error(String.Concat("[New] Could not scrape personal trakt.tv ratings!"))
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        logger.Error(New StackFrame().GetMethod().Name, ex)
    '    End Try
    'End Sub

    ''Playcount / LastPlayed
    '            If _SpecialSettings.Playcount OrElse _SpecialSettings.LastPlayed Then
    ''scrape playcount and lastplayed date
    '                If Not _traktWatchedMovies Is Nothing Then
    '' Go through each item in collection	 
    '                    For Each watchedMovie As TraktAPI.Model.TraktMovieWatched In _traktWatchedMovies
    '                        If Not watchedMovie.Movie.Ids Is Nothing Then
    ''Check if information is stored...
    '                            If (Not String.IsNullOrEmpty(nMovie.IMDBID) AndAlso Not watchedMovie.Movie.Ids.Imdb Is Nothing AndAlso watchedMovie.Movie.Ids.Imdb = strID) OrElse (Not String.IsNullOrEmpty(nMovie.TMDBID) AndAlso Not watchedMovie.Movie.Ids.Tmdb Is Nothing AndAlso watchedMovie.Movie.Ids.Tmdb.ToString = strID) Then
    '                                If _SpecialSettings.Playcount Then
    '                                    nMovie.PlayCount = watchedMovie.Plays
    '                                End If
    '                                If _SpecialSettings.LastPlayed Then
    ''listed-At is not user friendly formatted, so change format a bit
    ''"listed_at": 2014-09-01T09:10:11.000Z (original)
    ''new format here: 2014-09-01  09:10:11
    'Dim myDateString As String = watchedMovie.LastWatchedAt
    'Dim myDate As DateTime
    'Dim isDate As Boolean = DateTime.TryParse(myDateString, myDate)
    '                                    If isDate Then
    '                                        nMovie.LastPlayed = myDate.ToString("yyyy-MM-dd HH:mm:ss")
    '                                    End If
    '                                End If
    '                                Exit For
    '                            End If
    '                        End If
    '                    Next
    '                Else
    '                    logger.Info("[GetMovieInfo] No playcounts/lastplayed values of movies scraped from trakt.tv! Current movie: " & strID)
    '                End If
    '            End If

    ''Playcount / LastPlayed
    '        If _SpecialSettings.EpisodePlaycount OrElse _SpecialSettings.EpisodeLastPlayed Then
    ''scrape playcount and lastplayed date
    '            If Not _traktWatchedEpisodes Is Nothing Then
    'Dim SyncThisItem = True
    '                For Each watchedshow In _traktWatchedEpisodes
    '                    If SyncThisItem = False Then Exit For
    ''find correct tvshow
    '                    If Not watchedshow Is Nothing AndAlso Not watchedshow.Show Is Nothing AndAlso Not watchedshow.Show.Ids Is Nothing AndAlso (watchedshow.Show.Ids.Tvdb.ToString = ShowID OrElse watchedshow.Show.Ids.Tmdb.ToString = ShowID OrElse watchedshow.Show.Ids.Imdb.ToString = ShowID) Then
    ''loop through every season of watched show
    '                        For Each watchedseason In watchedshow.Seasons
    '                            If SyncThisItem = False Then Exit For
    ''..and find the correct season!
    '                            If watchedseason.Number = SeasonNumber Then
    ''loop through every episode of watched season
    '                                For Each watchedEpi In watchedseason.Episodes
    '                                    If SyncThisItem = False Then Exit For
    ''...and find correct episode
    '                                    If watchedEpi.Number = EpisodeNumber Then
    ''playcount
    '                                        If _SpecialSettings.EpisodePlaycount Then
    '                                            nEpisode.Playcount = watchedEpi.Plays
    '                                        End If
    ''lastplayed
    '                                        If _SpecialSettings.EpisodeLastPlayed Then
    ''listed-At is not user friendly formatted, so change format a bit
    ''"listed_at": 2014-09-01T09:10:11.000Z (original)
    ''new format here: 2014-09-01  09:10:11
    'Dim myDateString As String = watchedEpi.WatchedAt
    'Dim myDate As DateTime
    'Dim isDate As Boolean = DateTime.TryParse(myDateString, myDate)
    '                                            If isDate Then
    '                                                nEpisode.LastPlayed = myDate.ToString("yyyy-MM-dd HH:mm:ss")
    '                                            End If
    '                                        End If
    '                                        SyncThisItem = False
    '                                        Exit For
    '                                    End If
    '                                Next
    '                            End If
    '                        Next
    '                    Else
    '                        logger.Info("[GetTVEpisodeInfo] Invalid show data! Current show: ", ShowID)
    '                    End If
    '                Next
    '            Else
    '                logger.Info("[GetTVEpisodeInfo] No playcounts/lastplayed values of episodes scraped from trakt.tv! Current show: ", ShowID)
    '            End If
    '        End If
#End Region

   

#End Region 'Methods

#Region "Nested Types"

    Structure _MySettings
#Region "Fields"
        Dim Username As String
        Dim Password As String
        Dim GetShowProgress As Boolean

        'LastPlayed
        Dim SyncLastPlayedEditMovies As Boolean
        Dim SyncLastPlayedEditEpisodes As Boolean
        Dim SyncLastPlayedMultiEpisodes As Boolean
        Dim SyncLastPlayedMultiMovies As Boolean
        Dim SyncLastPlayedSingleEpisodes As Boolean
        Dim SyncLastPlayedSingleMovies As Boolean
        'Playcount
        Dim SyncPlaycountEditMovies As Boolean
        Dim SyncPlaycountEditEpisodes As Boolean
        Dim SyncPlaycountMultiEpisodes As Boolean
        Dim SyncPlaycountMultiMovies As Boolean
        Dim SyncPlaycountSingleEpisodes As Boolean
        Dim SyncPlaycountSingleMovies As Boolean

        Dim Sync As Boolean
#End Region 'Fields
    End Structure


    ''' <summary>
    ''' structure used to read setting file of Kodi Interface
    ''' </summary>
    ''' <remarks></remarks>
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
    End Class

#End Region 'Nested Types

End Class






