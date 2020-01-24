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

Public Class Theme_Movie
    Implements Interfaces.IScraperAddon_Theme_Movie

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Public Shared ConfigScrapeModifier_TV As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_Movie

#End Region 'Fields

#Region "Properties"

    Private Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Theme_Movie.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperAddon_Theme_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Theme_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Theme_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Theme_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Theme_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Theme_Movie.StateChanged

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

    Sub Init() Implements Interfaces.IScraperAddon_Theme_Movie.Init
        LoadSettings()
    End Sub

    Sub InjectSetupScraper_Movie() Implements Interfaces.IScraperAddon_Theme_Movie.InjectSettingsPanel
        LoadSettings()
        _PnlSettingsPanel = New frmSettingsPanel_Movie
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TelevisionTunes.com",
            .Type = Enums.SettingsPanelType.MovieTheme
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Theme_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function Run(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef ThemeList As List(Of MediaContainers.Theme)) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Theme_Movie.Run
        logger.Trace("[TelevisionTunes_Theme] [Scraper_Movie] [Start]")

        Dim tTelevisionTunes As New Scraper
        tTelevisionTunes.GetThemes(DBMovie.MainDetails.OriginalTitle)

        If tTelevisionTunes.ThemeList.Count > 0 Then
            ThemeList = tTelevisionTunes.ThemeList
        End If

        logger.Trace("[TelevisionTunes_Theme] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Theme_Movie.SaveSetup
        SaveSettings()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub LoadSettings()
        _ConfigModifier.MainTheme = AdvancedSettings.GetBooleanSetting("DoTheme", True, , Enums.ContentType.Movie)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoTheme", _ConfigModifier.MainTheme, , , Enums.ContentType.Movie)
        End Using
    End Sub

#End Region 'Methods 

End Class

Public Class Theme_TV
    Implements Interfaces.IScraperAddon_Theme_TV

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_TV

#End Region 'Fields

#Region "Properties"

    Private Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Theme_TV.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperAddon_Theme_TV.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Theme_TV.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Theme_TV.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Theme_TV.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Theme_TV.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Theme_TV.StateChanged

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

    Sub Init_TV() Implements Interfaces.IScraperAddon_Theme_TV.Init
        LoadSettings()
    End Sub

    Sub InjectSetupScraper_TV() Implements Interfaces.IScraperAddon_Theme_TV.InjectSettingsPanel
        LoadSettings()
        _PnlSettingsPanel = New frmSettingsPanel_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TelevisionTunes.com",
            .Type = Enums.SettingsPanelType.TVTheme
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Theme_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function Run(ByRef DBTV As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef ThemeList As List(Of MediaContainers.Theme)) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Theme_TV.Run
        logger.Trace("[TelevisionTunes_Theme] [Scraper_TV] [Start]")

        Dim tTelevisionTunes As New Scraper
        tTelevisionTunes.GetThemes(DBTV.MainDetails.Title)

        If tTelevisionTunes.ThemeList.Count > 0 Then
            ThemeList = tTelevisionTunes.ThemeList
        End If

        logger.Trace("[TelevisionTunes_Theme] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Theme_TV.SaveSetup
        SaveSettings()
        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub LoadSettings()
        _ConfigModifier.MainTheme = AdvancedSettings.GetBooleanSetting("DoTheme", True, , Enums.ContentType.TV)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoTheme", _ConfigModifier.MainTheme, , , Enums.ContentType.TV)
        End Using
    End Sub

#End Region 'Methods

End Class