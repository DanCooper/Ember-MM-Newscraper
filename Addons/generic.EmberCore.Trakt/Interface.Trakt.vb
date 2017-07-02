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
Imports System.Windows.Forms
Imports System.Xml.Serialization

Public Class TraktInterface
    Implements Interfaces.GenericModule

#Region "Delegates"

    Public Delegate Sub Delegate_AddToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
    Public Delegate Sub Delegate_RemoveToolsStripItem(control As System.Windows.Forms.ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)

#End Region 'Delegates

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Private _xmlSettingsPath As String = Path.Combine(Master.SettingsPath, "Interface.Trakt.xml")
    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty
    Private WithEvents cmnuTrayToolsTrakt As New ToolStripMenuItem
    Private WithEvents mnuMainToolsTrakt As New ToolStripMenuItem
    Private _Enabled As Boolean = False
    Private _Name As String = "Trakt.tv Manager"
    Private _SpecialSettings As New SpecialSettings
    Private _TraktAPI As clsAPITrakt
    Private _needNewAPI As Boolean = False

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements EmberAPI.Interfaces.GenericModule.GenericEvent
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
                If _SpecialSettings.GetWatchedState AndAlso _SpecialSettings.GetWatchedStateBeforeEdit_Movie AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.BeforeEdit_TVEpisode
                If _SpecialSettings.GetWatchedState AndAlso _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_TVEpisode(_dbelement)
                End If
            Case Enums.ModuleEventType.CommandLine
                _TraktAPI.SyncToEmber_All()
            Case Enums.ModuleEventType.ScraperMulti_Movie
                If _SpecialSettings.GetWatchedState AndAlso _SpecialSettings.GetWatchedStateScraperMulti_Movie AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.Remove_Movie
                If _SpecialSettings.CollectionRemove_Movie AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.RemoveFromCollection_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperMulti_TVShow, Enums.ModuleEventType.ScraperMulti_TVEpisode
                If _SpecialSettings.GetWatchedState AndAlso _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_TVEpisode(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperSingle_Movie
                If _SpecialSettings.GetWatchedState AndAlso _SpecialSettings.GetWatchedStateScraperSingle_Movie AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperSingle_TVShow, Enums.ModuleEventType.ScraperSingle_TVEpisode
                If _SpecialSettings.GetWatchedState AndAlso _SpecialSettings.GetWatchedStateScraperSingle_TVEpisode AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_TVEpisode(_dbelement)
                End If
        End Select

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Disable()
        Dim tsi As New ToolStripMenuItem

        'mnuMainTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, mnuMainToolsTrakt)

        'cmnuTrayTools
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        RemoveToolsStripItem(tsi, cmnuTrayToolsTrakt)
    End Sub

    Sub Enable()
        _TraktAPI = New clsAPITrakt(_SpecialSettings)
        _SpecialSettings.Token = _TraktAPI.Token

        Dim tsi As New ToolStripMenuItem

        'mnuMainTools menu
        mnuMainToolsTrakt.Image = New Bitmap(My.Resources.icon)
        mnuMainToolsTrakt.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.MainMenu.Items("mnuMainTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, mnuMainToolsTrakt)

        'cmnuTrayTools
        cmnuTrayToolsTrakt.Image = New Bitmap(My.Resources.icon)
        cmnuTrayToolsTrakt.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        tsi = DirectCast(ModulesManager.Instance.RuntimeObjects.TrayMenu.Items("cmnuTrayTools"), ToolStripMenuItem)
        AddToolsStripItem(tsi, cmnuTrayToolsTrakt)
    End Sub

    Public Sub AddToolsStripItem(control As ToolStripMenuItem, value As System.Windows.Forms.ToolStripItem)
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
        RaiseEvent ModuleEnabledChanged(_Name, State, 0)
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        _needNewAPI = True
    End Sub

    Private Sub Handle_AccountSettingsChanged()
        _needNewAPI = True
    End Sub

    Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _Enabled
        _setup.chkGetShowProgress.Checked = _SpecialSettings.GetShowProgress
        _setup.chkGetWatchedState.Checked = _SpecialSettings.GetWatchedState
        _setup.chkGetWatchedStateBeforeEdit_Movie.Checked = _SpecialSettings.GetWatchedStateBeforeEdit_Movie
        _setup.chkGetWatchedStateBeforeEdit_TVEpisode.Checked = _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode
        _setup.chkGetWatchedStateScraperMulti_Movie.Checked = _SpecialSettings.GetWatchedStateScraperMulti_Movie
        _setup.chkGetWatchedStateScraperMulti_TVEpisode.Checked = _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode
        _setup.chkGetWatchedStateScraperSingle_Movie.Checked = _SpecialSettings.GetWatchedStateScraperSingle_Movie
        _setup.chkGetWatchedStateScraperSingle_TVEpisode.Checked = _SpecialSettings.GetWatchedStateScraperSingle_TVEpisode
        _setup.txtPassword.Text = _SpecialSettings.Password
        _setup.txtUsername.Text = _SpecialSettings.Username

        SPanel.Name = _Name
        SPanel.Text = "Trakt.tv Interface"
        SPanel.Prefix = "Trakt_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_Enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.AccountSettingsChanged, AddressOf Handle_AccountSettingsChanged
        Return SPanel
    End Function

    Private Sub MyMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMainToolsTrakt.Click, cmnuTrayToolsTrakt.Click
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", False}))

        Using dTrakttvManager As New dlgTrakttvManager(_TraktAPI, _SpecialSettings.GetShowProgress)
            dTrakttvManager.ShowDialog()
        End Using

        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"controlsenabled", True}))
        RaiseEvent GenericEvent(Enums.ModuleEventType.Generic, New List(Of Object)(New Object() {"filllist", True, True, True}))
    End Sub

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

    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = _setup.chkEnabled.Checked
        _SpecialSettings.GetShowProgress = _setup.chkGetShowProgress.Checked
        _SpecialSettings.GetWatchedState = _setup.chkGetWatchedState.Checked
        _SpecialSettings.GetWatchedStateBeforeEdit_Movie = _setup.chkGetWatchedStateBeforeEdit_Movie.Checked
        _SpecialSettings.GetWatchedStateBeforeEdit_TVEpisode = _setup.chkGetWatchedStateBeforeEdit_TVEpisode.Checked
        _SpecialSettings.GetWatchedStateScraperMulti_Movie = _setup.chkGetWatchedStateScraperMulti_Movie.Checked
        _SpecialSettings.GetWatchedStateScraperMulti_TVEpisode = _setup.chkGetWatchedStateScraperMulti_TVEpisode.Checked
        _SpecialSettings.GetWatchedStateScraperSingle_Movie = _setup.chkGetWatchedStateScraperSingle_Movie.Checked
        _SpecialSettings.GetWatchedStateScraperSingle_TVEpisode = _setup.chkGetWatchedStateScraperSingle_TVEpisode.Checked
        _SpecialSettings.Password = _setup.txtPassword.Text
        _SpecialSettings.Username = _setup.txtUsername.Text

        If _needNewAPI Then
            _SpecialSettings.Token = String.Empty
            _TraktAPI = New clsAPITrakt(_SpecialSettings)
            _SpecialSettings.Token = _TraktAPI.Token
            _needNewAPI = False
        End If

        SaveSettings()

        If DoDispose Then
            RemoveHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.AccountSettingsChanged, AddressOf Handle_AccountSettingsChanged
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

#End Region 'Methods

#Region "Nested Types"

    <Serializable()>
    <XmlRoot("interface.trakt")>
    Class SpecialSettings

#Region "Fields"

        Private _collectionremove_movie As Boolean
        Private _getshowprogress As Boolean
        Private _getwatchedstate As Boolean
        Private _getwatchedstatebeforeedit_movie As Boolean
        Private _getwatchedstatebeforeedit_tvepisode As Boolean
        Private _getwatchedstatescrapermulti_movie As Boolean
        Private _getwatchedstatescrapermulti_tvepisode As Boolean
        Private _getwatchedstatescrapersingle_movie As Boolean
        Private _getwatchedstatescrapersingle_tvepisode As Boolean
        Private _password As String
        Private _token As String
        Private _username As String

#End Region 'Fields

#Region "Properties"

        <XmlElement("collectionremove_movie")>
        Public Property CollectionRemove_Movie() As Boolean
            Get
                Return _collectionremove_movie
            End Get
            Set(ByVal value As Boolean)
                _collectionremove_movie = value
            End Set
        End Property

        <XmlElement("getshowprogress")>
        Public Property GetShowProgress() As Boolean
            Get
                Return _getshowprogress
            End Get
            Set(ByVal value As Boolean)
                _getshowprogress = value
            End Set
        End Property

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

        <XmlElement("password")>
        Public Property Password() As String
            Get
                Return _password
            End Get
            Set(ByVal value As String)
                _password = value
            End Set
        End Property

        <XmlElement("token")>
        Public Property Token() As String
            Get
                Return _token
            End Get
            Set(ByVal value As String)
                _token = value
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

#End Region 'Properties

#Region "Constructors"

        Public Sub New()
            Clear()
        End Sub

#End Region 'Constructors

#Region "Methods"

        Public Sub Clear()
            _collectionremove_movie = False
            _getshowprogress = False
            _getwatchedstate = False
            _getwatchedstatebeforeedit_movie = False
            _getwatchedstatebeforeedit_tvepisode = False
            _getwatchedstatescrapermulti_movie = False
            _getwatchedstatescrapermulti_tvepisode = False
            _getwatchedstatescrapersingle_movie = False
            _getwatchedstatescrapersingle_tvepisode = False
            _password = String.Empty
            _token = String.Empty
            _username = String.Empty
        End Sub

#End Region 'Methods

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






