Imports EmberAPI

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

Public Class NotificationsModule
    Implements Interfaces.GenericModule

#Region "Fields"

    Private dNotify As frmNotify
    Private eSettings As New NotifySettings
    Private _enabled As Boolean = False
    Private _name As String = "Notifications"
    Private _setup As frmSettingsHolder
    Private _AssemblyName As String = String.Empty

#End Region 'Fields

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.GenericModule.GenericEvent

    Public Event ModuleEnabledChanged(ByVal Name As String, ByVal State As Boolean, ByVal diffOrder As Integer) Implements Interfaces.GenericModule.ModuleSetupChanged

    Public Event ModuleSettingsChanged() Implements Interfaces.GenericModule.ModuleSettingsChanged

#End Region 'Events

#Region "Properties"

    Public Property Enabled() As Boolean Implements Interfaces.GenericModule.Enabled
        Get
            Return Me._enabled
        End Get
        Set(ByVal value As Boolean)
            Me._enabled = value
        End Set
    End Property

    Public ReadOnly Property ModuleName() As String Implements Interfaces.GenericModule.ModuleName
        Get
            Return Me._name
        End Get
    End Property

    Public ReadOnly Property ModuleType() As List(Of Enums.ModuleEventType) Implements Interfaces.GenericModule.ModuleType
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Notification}) 'Enums.ModuleType.Notification
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.GenericModule.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub Init(ByVal sAssemblyName As String, ByVal sExecutable As String) Implements Interfaces.GenericModule.Init
        _AssemblyName = sAssemblyName
        'Master.eLang.LoadLanguage(Master.eSettings.Language, sExecutable)
        LoadSettings()
    End Sub

    Public Function InjectSetup() As Containers.SettingsPanel Implements Interfaces.GenericModule.InjectSetup
        Dim SPanel As New Containers.SettingsPanel
        Me._setup = New frmSettingsHolder
        Me._setup.chkEnabled.Checked = Me._enabled
        Me._setup.chkOnError.Checked = eSettings.OnError
        Me._setup.chkOnNewMovie.Checked = eSettings.OnNewMovie
        Me._setup.chkOnMovieScraped.Checked = eSettings.OnMovieScraped
        Me._setup.chkOnNewEp.Checked = eSettings.OnNewEp
        SPanel.Name = Me._name
        SPanel.Text = Master.eLang.GetString(487, "Notifications")
        SPanel.Prefix = "Notify_"
        SPanel.Type = Master.eLang.GetString(802, "Modules")
        SPanel.ImageIndex = If(Me._enabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings
        AddHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
        AddHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Public Async Function RunGeneric(ByVal mType As Enums.ModuleEventType, ByVal _params As List(Of Object), ByVal _refparam As Object, ByVal _dbmovie As Structures.DBMovie) As Threading.Tasks.Task(Of Interfaces.ModuleResult) Implements Interfaces.GenericModule.RunGeneric
        ' return parameters will add in ReturnObject
        ' _params
        '_refparam 
        '_dbmovie 
        Dim RetO As New Interfaces.ModuleResult

        Try
            If mType = Enums.ModuleEventType.Notification Then
                Dim ShowIt As Boolean = False

                Select Case True
                    Case _params(0).ToString = "error" AndAlso eSettings.OnError
                        ShowIt = True
                    Case _params(0).ToString = "newmovie" AndAlso eSettings.OnNewMovie
                        ShowIt = True
                    Case _params(0).ToString = "moviescraped" AndAlso eSettings.OnMovieScraped
                        ShowIt = True
                    Case _params(0).ToString = "newep" AndAlso eSettings.OnNewEp
                        ShowIt = True
                    Case _params(0).ToString = "info"
                        ShowIt = True
                End Select

                If ShowIt Then
                    dNotify = New frmNotify
                    AddHandler dNotify.NotifierClicked, AddressOf Me.Handle_NotifierClicked
                    AddHandler dNotify.NotifierClosed, AddressOf Me.Handle_NotifierClosed
                    dNotify.Show(_params(0).ToString, Convert.ToInt32(_params(1)), _params(2).ToString, _params(3).ToString, If(Not IsNothing(_params(4)), DirectCast(_params(4), Image), Nothing))
                End If
            End If
        Catch ex As Exception
        End Try
        RetO = New Interfaces.ModuleResult
        RetO.breakChain = False
        RetO.Cancelled = False
        RetO.ReturnObj.Add(_params)
        RetO.ReturnObj.Add(_refparam)
        RetO.ReturnObj.Add(_dbmovie)
        Return RetO
    End Function

    Private Sub Handle_ModuleEnabledChanged(ByVal State As Boolean)
        RaiseEvent ModuleEnabledChanged(Me._name, State, 0)
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_NotifierClicked(ByVal _type As String)
        RaiseEvent GenericEvent(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {_type}))
    End Sub

    Private Sub Handle_NotifierClosed()
        RemoveHandler Me.dNotify.NotifierClicked, AddressOf Me.Handle_NotifierClicked
        RemoveHandler Me.dNotify.NotifierClosed, AddressOf Me.Handle_NotifierClosed
    End Sub

    Private Sub LoadSettings()
        eSettings.OnError = clsAdvancedSettings.GetBooleanSetting("NotifyOnError", True)
        eSettings.OnNewMovie = clsAdvancedSettings.GetBooleanSetting("NotifyOnNewMovie", False)
        eSettings.OnMovieScraped = clsAdvancedSettings.GetBooleanSetting("NotifyOnMovieScraped", True)
        eSettings.OnNewEp = clsAdvancedSettings.GetBooleanSetting("NotifyOnNewEp", False)
    End Sub

    Private Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("NotifyOnError", eSettings.OnError)
            settings.SetBooleanSetting("NotifyOnNewMovie", eSettings.OnNewMovie)
            settings.SetBooleanSetting("NotifyOnMovieScraped", eSettings.OnMovieScraped)
            settings.SetBooleanSetting("NotifyOnNewEp", eSettings.OnNewEp)
        End Using
    End Sub

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.GenericModule.SaveSetup
        Me._enabled = _setup.chkEnabled.Checked
        eSettings.OnError = _setup.chkOnError.Checked
        eSettings.OnNewMovie = _setup.chkOnNewMovie.Checked
        eSettings.OnMovieScraped = _setup.chkOnMovieScraped.Checked
        eSettings.OnNewEp = _setup.chkOnNewEp.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler Me._setup.ModuleEnabledChanged, AddressOf Handle_ModuleEnabledChanged
            RemoveHandler Me._setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Class NotifySettings

#Region "Fields"

        Private _onerror As Boolean
        Private _onmoviescraped As Boolean
        Private _onnewep As Boolean
        Private _onnewmovie As Boolean

#End Region 'Fields

#Region "Properties"

        Public Property OnError() As Boolean
            Get
                Return Me._onerror
            End Get
            Set(ByVal value As Boolean)
                Me._onerror = value
            End Set
        End Property

        Public Property OnMovieScraped() As Boolean
            Get
                Return Me._onmoviescraped
            End Get
            Set(ByVal value As Boolean)
                Me._onmoviescraped = value
            End Set
        End Property

        Public Property OnNewEp() As Boolean
            Get
                Return Me._onnewep
            End Get
            Set(ByVal value As Boolean)
                Me._onnewep = value
            End Set
        End Property

        Public Property OnNewMovie() As Boolean
            Get
                Return Me._onnewmovie
            End Get
            Set(ByVal value As Boolean)
                Me._onnewmovie = value
            End Set
        End Property

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class