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
' ###############################################################################

Imports EmberAPI
Imports NLog

Public Class TMDB_Trailer
    Implements Interfaces.IScraperModule_Trailer_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel
    Private _PrivateAPIKey As String = String.Empty
    Private _Scraper As New Scraper

    Private Const _EmberAPIKey As String = "44810eefccd9cb1fa1d57e7b0d67b08d"

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Trailer_Movie.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperModule_Trailer_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperModule_Trailer_Movie.SettingsPanel


#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Trailer_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Trailer_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Trailer_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperModule_Trailer_Movie.StateChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_StateChanged(ByVal State As Boolean, ByVal DiffOrder As Integer)
        IsEnabled = State
        RaiseEvent StateChanged(SettingsPanel.SettingsPanelID, State, DiffOrder)
    End Sub

#End Region 'Event Methods

#Region "Interface Methods"

    Sub Init() Implements Interfaces.IScraperModule_Trailer_Movie.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Trailer_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel
        _PnlSettingsPanel.chkEnabled.Checked = _Enabled
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey
        _PnlSettingsPanel.chkFallBackEng.Checked = _AddonSettings.FallBackEng

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(_Enabled, 9, 10),
            .Order = 110,
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.MovieTrailer
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Trailer_Movie.OrderChanged
        _PnlSettingsPanel.orderChanged(OrderState)
    End Sub

    Public Function Run(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef TrailerList As List(Of MediaContainers.Trailer)) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Trailer_Movie.Run
        _Logger.Trace("[TMDB_Trailer] [Scraper_Movie] [Start]")

        _Scraper.DefaultLanguage = DBMovie.Language

        If Not DBMovie.Movie.UniqueIDs.TMDbIdSpecified Then
            DBMovie.Movie.UniqueIDs.TMDbId = ModulesManager.Instance.GetMovieTMDBID(DBMovie.Movie.UniqueIDs.IMDbId)
        End If

        If DBMovie.Movie.UniqueIDs.TMDbIdSpecified Then
            TrailerList = _Scraper.GetTrailers(DBMovie.Movie.UniqueIDs.TMDbId)
        End If

        _Logger.Trace("[TMDB_Trailer] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Trailer_Movie.SaveSetup
        _AddonSettings.FallBackEng = _PnlSettingsPanel.chkFallBackEng.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.FallBackEng = _PnlSettingsPanel.chkFallBackEng.Checked
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), _EmberAPIKey, _PrivateAPIKey)

        Settings_Save()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub Settings_Load()
        _ConfigModifier.MainTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)

        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", _PrivateAPIKey)
        _AddonSettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.Movie)
        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("FallBackEn", _AddonSettings.FallBackEng, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTrailer", _ConfigModifier.MainTrailer, , , Enums.ContentType.Movie)
        End Using
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AddonSettings

#Region "Fields"

        Dim APIKey As String
        Dim PrefLanguage As String
        Dim FallBackEng As Boolean

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class