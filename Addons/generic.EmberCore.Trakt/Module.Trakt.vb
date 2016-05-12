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

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty
    Private WithEvents cmnuTrayToolsTrakt As New ToolStripMenuItem
    Private WithEvents mnuMainToolsTrakt As New ToolStripMenuItem
    Private _enabled As Boolean = False
    Private _Name As String = "Trakt.tv Manager"
    Private _MySettings As New MySettings
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
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Generic, Enums.ModuleEventType.CommandLine,
                                                      Enums.ModuleEventType.BeforeEdit_Movie, Enums.ModuleEventType.ScraperMulti_Movie, Enums.ModuleEventType.ScraperSingle_Movie,
                                                      Enums.ModuleEventType.BeforeEdit_TVEpisode, Enums.ModuleEventType.ScraperMulti_TVEpisode, Enums.ModuleEventType.ScraperSingle_TVEpisode,
                                                      Enums.ModuleEventType.BeforeEdit_TVShow, Enums.ModuleEventType.ScraperMulti_TVShow, Enums.ModuleEventType.ScraperSingle_TVShow})
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
        LoadSettings()
        Select Case mType
            Case Enums.ModuleEventType.BeforeEdit_Movie
                If _MySettings.SyncPlaycountEditMovies AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.BeforeEdit_TVEpisode
                If _MySettings.SyncPlaycountEditEpisodes AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_TVEpisode(_dbelement)
                End If
            Case Enums.ModuleEventType.CommandLine
                _TraktAPI.SyncToEmber_All()
            Case Enums.ModuleEventType.ScraperMulti_Movie
                If _MySettings.SyncPlaycountMultiMovies AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperMulti_TVShow, Enums.ModuleEventType.ScraperMulti_TVEpisode
                If _MySettings.SyncPlaycountMultiEpisodes AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_TVEpisode(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperSingle_Movie
                If _MySettings.SyncPlaycountSingleMovies AndAlso _dbelement IsNot Nothing Then
                    _TraktAPI.SetWatchedState_Movie(_dbelement)
                End If
            Case Enums.ModuleEventType.ScraperSingle_TVShow, Enums.ModuleEventType.ScraperSingle_TVEpisode
                If _MySettings.SyncPlaycountSingleEpisodes AndAlso _dbelement IsNot Nothing Then
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
        _TraktAPI = New clsAPITrakt(_MySettings)

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
        _setup.chkEnabled.Checked = _enabled
        _setup.txtUsername.Text = _MySettings.Username
        _setup.txtPassword.Text = _MySettings.Password
        _setup.chkGetShowProgress.Checked = _MySettings.GetShowProgress
        'LastPlayed
        _setup.chkSyncLastPlayedEditMovies.Checked = _MySettings.SyncLastPlayedEditMovies
        _setup.chkSyncLastPlayedEditEpisodes.Checked = _MySettings.SyncLastPlayedEditEpisodes
        _setup.chkSyncLastPlayedMultiEpisodes.Checked = _MySettings.SyncLastPlayedMultiEpisodes
        _setup.chkSyncLastPlayedMultiMovies.Checked = _MySettings.SyncLastPlayedMultiMovies
        _setup.chkSyncLastPlayedSingleEpisodes.Checked = _MySettings.SyncLastPlayedSingleEpisodes
        _setup.chkSyncLastPlayedSingleMovies.Checked = _MySettings.SyncLastPlayedSingleMovies
        'Playcount
        _setup.chkSyncPlaycountEditMovies.Checked = _MySettings.SyncPlaycountEditMovies
        _setup.chkSyncPlaycountEditEpisodes.Checked = _MySettings.SyncPlaycountEditEpisodes
        _setup.chkSyncPlaycountMultiEpisodes.Checked = _MySettings.SyncPlaycountMultiEpisodes
        _setup.chkSyncPlaycountMultiMovies.Checked = _MySettings.SyncPlaycountMultiMovies
        _setup.chkSyncPlaycountSingleEpisodes.Checked = _MySettings.SyncPlaycountSingleEpisodes
        _setup.chkSyncPlaycountSingleMovies.Checked = _MySettings.SyncPlaycountSingleMovies

        SPanel.Name = _Name
        SPanel.Text = Master.eLang.GetString(871, "Trakt.tv Manager")
        SPanel.Prefix = "Trakt_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(_enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.AccountSettingsChanged, AddressOf Handle_AccountSettingsChanged
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
        _MySettings.GetShowProgress = clsAdvancedSettings.GetBooleanSetting("GetShowProgress", False)
        _MySettings.Password = clsAdvancedSettings.GetSetting("Password", String.Empty)
        _MySettings.Token = clsAdvancedSettings.GetSetting("Token", String.Empty)
        _MySettings.Username = clsAdvancedSettings.GetSetting("Username", String.Empty)
        'LastPlayed
        _MySettings.SyncLastPlayedEditMovies = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedEditMovies", False)
        _MySettings.SyncLastPlayedEditEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedEditEpisodes", False)
        _MySettings.SyncLastPlayedMultiEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedMultiEpisodes", False)
        _MySettings.SyncLastPlayedMultiMovies = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedMultiMovies", False)
        _MySettings.SyncLastPlayedSingleEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedSingleEpisodes", False)
        _MySettings.SyncLastPlayedSingleMovies = clsAdvancedSettings.GetBooleanSetting("SyncLastPlayedSingleMovies", False)
        'Playcount
        _MySettings.SyncPlaycountEditMovies = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountEditMovies", False)
        _MySettings.SyncPlaycountEditEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountEditEpisodes", False)
        _MySettings.SyncPlaycountMultiEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountMultiEpisodes", False)
        _MySettings.SyncPlaycountMultiMovies = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountMultiMovies", False)
        _MySettings.SyncPlaycountSingleEpisodes = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountSingleEpisodes", False)
        _MySettings.SyncPlaycountSingleMovies = clsAdvancedSettings.GetBooleanSetting("SyncPlaycountSingleMovies", False)
    End Sub

    Sub SaveSetupModule(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Enabled = _setup.chkEnabled.Checked
        _MySettings.GetShowProgress = _setup.chkGetShowProgress.Checked
        _MySettings.Password = _setup.txtPassword.Text
        _MySettings.Username = _setup.txtUsername.Text
        'LastPlayed
        _MySettings.SyncLastPlayedEditMovies = _setup.chkSyncLastPlayedEditMovies.Checked
        _MySettings.SyncLastPlayedEditEpisodes = _setup.chkSyncLastPlayedEditEpisodes.Checked
        _MySettings.SyncLastPlayedMultiEpisodes = _setup.chkSyncLastPlayedMultiEpisodes.Checked
        _MySettings.SyncLastPlayedMultiMovies = _setup.chkSyncLastPlayedMultiMovies.Checked
        _MySettings.SyncLastPlayedSingleEpisodes = _setup.chkSyncLastPlayedSingleEpisodes.Checked
        _MySettings.SyncLastPlayedSingleMovies = _setup.chkSyncLastPlayedSingleMovies.Checked
        'Playcount
        _MySettings.SyncPlaycountEditMovies = _setup.chkSyncPlaycountEditMovies.Checked
        _MySettings.SyncPlaycountEditEpisodes = _setup.chkSyncPlaycountEditEpisodes.Checked
        _MySettings.SyncPlaycountMultiEpisodes = _setup.chkSyncPlaycountMultiEpisodes.Checked
        _MySettings.SyncPlaycountMultiMovies = _setup.chkSyncPlaycountMultiMovies.Checked
        _MySettings.SyncPlaycountSingleEpisodes = _setup.chkSyncPlaycountSingleEpisodes.Checked
        _MySettings.SyncPlaycountSingleMovies = _setup.chkSyncPlaycountSingleMovies.Checked

        SaveSettings()

        If _needNewAPI Then
            _MySettings.Token = String.Empty
            _TraktAPI = New clsAPITrakt(_MySettings)
            _needNewAPI = False
        End If

        If DoDispose Then
            RemoveHandler _setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.AccountSettingsChanged, AddressOf Handle_AccountSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetSetting("Password", _MySettings.Password)
            settings.SetSetting("Token", _MySettings.Token)
            settings.SetSetting("Username", _MySettings.Username)
            settings.SetBooleanSetting("GetShowProgress", _MySettings.GetShowProgress)
            'LastPlayed
            settings.SetBooleanSetting("SyncLastPlayedEditMovies", _MySettings.SyncLastPlayedEditMovies)
            settings.SetBooleanSetting("SyncLastPlayedEditEpisodes", _MySettings.SyncLastPlayedEditEpisodes)
            settings.SetBooleanSetting("SyncLastPlayedMultiEpisodes", _MySettings.SyncLastPlayedMultiEpisodes)
            settings.SetBooleanSetting("SyncLastPlayedMultiMovies", _MySettings.SyncLastPlayedMultiMovies)
            settings.SetBooleanSetting("SyncLastPlayedSingleEpisodes", _MySettings.SyncLastPlayedSingleEpisodes)
            settings.SetBooleanSetting("SyncLastPlayedSingleMovies", _MySettings.SyncLastPlayedSingleMovies)
            'Playcount
            settings.SetBooleanSetting("SyncPlaycountEditMovies", _MySettings.SyncPlaycountEditMovies)
            settings.SetBooleanSetting("SyncPlaycountEditEpisodes", _MySettings.SyncPlaycountEditEpisodes)
            settings.SetBooleanSetting("SyncPlaycountMultiEpisodes", _MySettings.SyncPlaycountMultiEpisodes)
            settings.SetBooleanSetting("SyncPlaycountMultiMovies", _MySettings.SyncPlaycountMultiMovies)
            settings.SetBooleanSetting("SyncPlaycountSingleEpisodes", _MySettings.SyncPlaycountSingleEpisodes)
            settings.SetBooleanSetting("SyncPlaycountSingleMovies", _MySettings.SyncPlaycountSingleMovies)
        End Using
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure MySettings

#Region "Fields"

        Dim GetShowProgress As Boolean
        Dim Password As String
        Dim Token As String
        Dim Username As String

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

#End Region 'Fields

    End Structure


    ''' <summary>
    ''' structure used to read setting file of Kodi Interface
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    <XmlRoot("interface.kodi")>
    Class SpecialSettings

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






