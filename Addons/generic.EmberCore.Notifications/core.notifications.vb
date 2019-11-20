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

Public Class Core
    Implements Interfaces.IGenericModule

#Region "Fields"

    Private _AddonSettings As New AddonSettings
    Private _DlgNotification As dlgNotification
    Private _PnlSettingsPanel As frmSettingsPanel

#End Region 'Fields

#Region "Properties"

    ReadOnly Property IsBusy() As Boolean Implements Interfaces.IGenericModule.IsBusy
        Get
            Return False
        End Get
    End Property

    Public Property IsEnabled() As Boolean Implements Interfaces.IGenericModule.IsEnabled

    Property Order As Integer Implements Interfaces.IGenericModule.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IGenericModule.SettingsPanel

    Public ReadOnly Property Type() As List(Of Enums.ModuleEventType) Implements Interfaces.IGenericModule.Type
        Get
            Return New List(Of Enums.ModuleEventType)(New Enums.ModuleEventType() {Enums.ModuleEventType.Notification})
        End Get
    End Property

#End Region 'Properties

#Region "Events"

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object)) Implements Interfaces.IGenericModule.GenericEvent
    Public Event NeedsRestart() Implements Interfaces.IGenericModule.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IGenericModule.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IGenericModule.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_GenericEvent(ByVal _type As String)
        RaiseEvent GenericEvent(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {_type}))
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_StateChanged(ByVal State As Boolean)
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, 0)
    End Sub

    Private Sub Handle_NotifierClosed()
        RemoveHandler _DlgNotification.NotifierClicked, AddressOf Handle_GenericEvent
        RemoveHandler _DlgNotification.NotifierClosed, AddressOf Handle_NotifierClosed
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Public Sub Init() Implements Interfaces.IGenericModule.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IGenericModule.InjectSettingsPanel
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkOnError.Checked = _AddonSettings.OnError
        _PnlSettingsPanel.chkOnNewMovie.Checked = _AddonSettings.OnNewMovie
        _PnlSettingsPanel.chkOnMovieScraped.Checked = _AddonSettings.OnMovieScraped
        _PnlSettingsPanel.chkOnNewEpisode.Checked = _AddonSettings.OnNewEpisode
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = Master.eLang.GetString(487, "Notifications"),
            .Type = Enums.SettingsPanelType.Core
        }
    End Sub

    Public Function Run(ByVal ModuleEventType As Enums.ModuleEventType, ByRef Parameters As List(Of Object), ByRef SingleObjekt As Object, ByRef DBElement As Database.DBElement) As Interfaces.ModuleResult Implements Interfaces.IGenericModule.Run
        Try
            If ModuleEventType = Enums.ModuleEventType.Notification Then
                Dim ShowIt As Boolean = False

                Select Case True
                    Case Parameters(0).ToString = "error" AndAlso _AddonSettings.OnError
                        ShowIt = True
                    Case Parameters(0).ToString = "newmovie" AndAlso _AddonSettings.OnNewMovie
                        ShowIt = True
                    Case Parameters(0).ToString = "moviescraped" AndAlso _AddonSettings.OnMovieScraped
                        ShowIt = True
                    Case Parameters(0).ToString = "tvepisodescraped" AndAlso _AddonSettings.OnNewEpisode
                        ShowIt = True
                    Case Parameters(0).ToString = "tvseasonscraped" AndAlso _AddonSettings.OnNewEpisode
                        ShowIt = True
                    Case Parameters(0).ToString = "tvshowscraped" AndAlso _AddonSettings.OnNewEpisode
                        ShowIt = True
                    Case Parameters(0).ToString = "newep" AndAlso _AddonSettings.OnNewEpisode
                        ShowIt = True
                    Case Parameters(0).ToString = "info"
                        ShowIt = True
                End Select

                If ShowIt Then
                    _DlgNotification = New dlgNotification
                    AddHandler _DlgNotification.NotifierClicked, AddressOf Handle_GenericEvent
                    AddHandler _DlgNotification.NotifierClosed, AddressOf Handle_NotifierClosed
                    _DlgNotification.Show(Parameters(0).ToString, Convert.ToInt32(Parameters(1)), Parameters(2).ToString, Parameters(3).ToString, If(Parameters(4) IsNot Nothing, DirectCast(Parameters(4), Image), Nothing))
                End If
            End If
        Catch ex As Exception
        End Try
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IGenericModule.SaveSetup
        IsEnabled = _PnlSettingsPanel.chkEnabled.Checked
        _AddonSettings.OnError = _PnlSettingsPanel.chkOnError.Checked
        _AddonSettings.OnNewMovie = False '_setup.chkOnNewMovie.Checked
        _AddonSettings.OnMovieScraped = _PnlSettingsPanel.chkOnMovieScraped.Checked
        _AddonSettings.OnNewEpisode = False '_setup.chkOnNewEp.Checked
        Settings_Save()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Private Sub Settings_Load()
        _AddonSettings.OnError = AdvancedSettings.GetBooleanSetting("NotifyOnError", True)
        _AddonSettings.OnNewMovie = False 'clsAdvancedSettings.GetBooleanSetting("NotifyOnNewMovie", False)
        _AddonSettings.OnMovieScraped = AdvancedSettings.GetBooleanSetting("NotifyOnMovieScraped", True)
        _AddonSettings.OnNewEpisode = False 'clsAdvancedSettings.GetBooleanSetting("NotifyOnNewEp", False)
    End Sub

    Private Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("NotifyOnError", _AddonSettings.OnError)
            settings.SetBooleanSetting("NotifyOnNewMovie", _AddonSettings.OnNewMovie)
            settings.SetBooleanSetting("NotifyOnMovieScraped", _AddonSettings.OnMovieScraped)
            settings.SetBooleanSetting("NotifyOnNewEp", _AddonSettings.OnNewEpisode)
        End Using
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Class AddonSettings

#Region "Properties"

        Public Property OnError() As Boolean = True

        Public Property OnMovieScraped() As Boolean = False

        Public Property OnNewEpisode() As Boolean = False

        Public Property OnNewMovie() As Boolean = False

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class