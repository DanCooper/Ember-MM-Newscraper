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
Imports ScraperModule.TVDBs

Public Class TVDB_Image
    Implements Interfaces.ScraperModule_Image_TV

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared ConfigModifier As New Structures.ScrapeModifiers
    Public Shared _AssemblyName As String

    Private _SpecialSettings As New SpecialSettings
    Private _Name As String = "TVDB_Image"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder
    Private strPrivateAPIKey As String = String.Empty

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.ScraperModule_Image_TV.ModuleSettingsChanged
    Public Event MovieScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_TV.ScraperEvent
    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_TV.ScraperSetupChanged
    Public Event SetupNeedsRestart() Implements Interfaces.ScraperModule_Image_TV.SetupNeedsRestart
    Public Event ImagesDownloaded(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_TV.ImagesDownloaded
    Public Event ProgressUpdated(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_TV.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Image_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Image_TV.ModuleVersion
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.ScraperModule_Image_TV.ScraperEnabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.ScraperModule_Image_TV.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.EpisodePoster
                Return ConfigModifier.EpisodePoster
            Case Enums.ModifierType.SeasonBanner
                Return ConfigModifier.SeasonBanner
            Case Enums.ModifierType.SeasonPoster
                Return ConfigModifier.SeasonPoster
            Case Enums.ModifierType.MainBanner
                Return ConfigModifier.MainBanner
            Case Enums.ModifierType.MainFanart
                Return ConfigModifier.MainFanart
            Case Enums.ModifierType.MainPoster
                Return ConfigModifier.MainPoster
        End Select
        Return False
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_TV.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _ScraperEnabled
        _setup.chkScrapeEpisodePoster.Checked = ConfigModifier.EpisodePoster
        _setup.chkScrapeSeasonBanner.Checked = ConfigModifier.SeasonBanner
        _setup.chkScrapeSeasonPoster.Checked = ConfigModifier.SeasonPoster
        _setup.chkScrapeShowBanner.Checked = ConfigModifier.MainBanner
        _setup.chkScrapeShowFanart.Checked = ConfigModifier.MainFanart
        _setup.chkScrapeShowPoster.Checked = ConfigModifier.MainPoster
        _setup.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup.lblEMMAPI.Visible = False
            _setup.txtApiKey.Enabled = True
        End If

        _setup.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "Scraper")
        Spanel.Text = "TVDB"
        Spanel.Prefix = "TVDBMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlTVMedia"
        Spanel.Type = Master.eLang.GetString(653, "TV Shows")
        Spanel.ImageIndex = If(Me._ScraperEnabled, 9, 10)
        Spanel.Panel = Me._setup.pnlSettings

        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return Spanel
    End Function

    Sub LoadSettings()
        ConfigModifier.EpisodePoster = AdvancedSettings.GetBooleanSetting("DoEpisodePoster", True)
        ConfigModifier.SeasonBanner = AdvancedSettings.GetBooleanSetting("DoSeasonBanner", True)
        ConfigModifier.SeasonPoster = AdvancedSettings.GetBooleanSetting("DoSeasonPoster", True)
        ConfigModifier.MainBanner = AdvancedSettings.GetBooleanSetting("DoShowBanner", True)
        ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoShowFanart", True)
        ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoShowPoster", True)

        strPrivateAPIKey = AdvancedSettings.GetSetting("ApiKey", "")
        _SpecialSettings.ApiKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "353783CE455412FD", strPrivateAPIKey)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoEpisodePoster", ConfigModifier.EpisodePoster)
            settings.SetBooleanSetting("DoSeasonBanner", ConfigModifier.SeasonBanner)
            settings.SetBooleanSetting("DoSeasonPoster", ConfigModifier.SeasonPoster)
            settings.SetBooleanSetting("DoShowBanner", ConfigModifier.MainBanner)
            settings.SetBooleanSetting("DoShowFanart", ConfigModifier.MainFanart)
            settings.SetBooleanSetting("DoShowPoster", ConfigModifier.MainPoster)

            settings.SetSetting("ApiKey", _setup.txtApiKey.Text)
        End Using
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_TV.SaveSetupScraper
        ConfigModifier.EpisodePoster = _setup.chkScrapeEpisodePoster.Checked
        ConfigModifier.SeasonBanner = _setup.chkScrapeSeasonBanner.Checked
        ConfigModifier.SeasonPoster = _setup.chkScrapeSeasonPoster.Checked
        ConfigModifier.MainBanner = _setup.chkScrapeShowBanner.Checked
        ConfigModifier.MainFanart = _setup.chkScrapeShowFanart.Checked
        ConfigModifier.MainPoster = _setup.chkScrapeShowPoster.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

    Function Scraper(ByRef DBTV As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_TV.Scraper
        logger.Trace("[TVDB_Image] [Scraper] [Start]")

        LoadSettings()
        Dim _scraper As New Scraper(_SpecialSettings)

        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, ConfigModifier)

        Select Case DBTV.ContentType
            Case Enums.ContentType.TVEpisode
                If Not String.IsNullOrEmpty(DBTV.TVShow.TVDB) Then
                    ImagesContainer = _scraper.GetImages_TVEpisode(DBTV.TVShow.TVDB, DBTV.TVEpisode.Season, DBTV.TVEpisode.Episode, DBTV.Ordering, FilteredModifiers)
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = _scraper.GetImages_TV(DBTV.TVShow.TVDB, FilteredModifiers).MainFanarts
                    End If
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Enums.ContentType.TVSeason
                If Not String.IsNullOrEmpty(DBTV.TVShow.TVDB) Then
                    ImagesContainer = _scraper.GetImages_TV(DBTV.TVShow.TVDB, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Enums.ContentType.TVShow
                If Not String.IsNullOrEmpty(DBTV.TVShow.TVDB) Then
                    ImagesContainer = _scraper.GetImages_TV(DBTV.TVShow.TVDB, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Else
        End Select

        logger.Trace("[TVDB_Image] [Scraper] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.ScraperModule_Image_TV.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure SpecialSettings

#Region "Fields"

        Dim ApiKey As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class
