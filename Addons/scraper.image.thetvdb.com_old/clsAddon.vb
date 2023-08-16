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

Public Class Addon
    Implements Interfaces.IAddon_Image_Scraper_TV

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared ConfigModifier As New Structures.ScrapeModifiers
    Public Shared _AssemblyName As String

    Private _SpecialSettings As New AddonSettings
    Private _Name As String = "TVDB_Image"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder
    Private strPrivateAPIKey As String = String.Empty

    Private _AddonSettings As New AddonSettings

#End Region 'Fields

#Region "Events"

    Public Event AddonSettingsChanged() Implements Interfaces.IAddon_Image_Scraper_TV.AddonSettingsChanged
    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.IAddon_Image_Scraper_TV.AddonStateChanged
    Public Event AddonNeedsRestart() Implements Interfaces.IAddon_Image_Scraper_TV.AddonNeedsRestart
    Public Event ImagesDownloaded(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.IAddon_Image_Scraper_TV.ImagesDownloaded
    Public Event ProgressUpdated(ByVal iPercent As Integer) Implements Interfaces.IAddon_Image_Scraper_TV.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.IAddon_Image_Scraper_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.IAddon_Image_Scraper_TV.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.IAddon_Image_Scraper_TV.ScraperEnabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IAddon_Image_Scraper_TV.QueryScraperCapabilities
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
        RaiseEvent AddonSettingsChanged()
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent AddonNeedsRestart()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(_Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.IAddon_Image_Scraper_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IAddon_Image_Scraper_TV.InjectSettingsPanel
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

        Spanel.UniqueName = String.Concat(_Name, "Scraper")
        Spanel.Title = "TheTVDb.com (old API)"
        Spanel.Order = 110
        Spanel.Parent = "pnlTVMedia"
        Spanel.Type = Master.eLang.GetString(653, "TV Shows")
        Spanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        Spanel.Panel = _setup.pnlSettings

        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return Spanel
    End Function

    Sub LoadSettings()
        ConfigModifier.EpisodePoster = Master.eAdvancedSettings.GetBooleanSetting("DoEpisodePoster", True)
        ConfigModifier.SeasonBanner = Master.eAdvancedSettings.GetBooleanSetting("DoSeasonBanner", True)
        ConfigModifier.SeasonPoster = Master.eAdvancedSettings.GetBooleanSetting("DoSeasonPoster", True)
        ConfigModifier.MainBanner = Master.eAdvancedSettings.GetBooleanSetting("DoShowBanner", True)
        ConfigModifier.MainFanart = Master.eAdvancedSettings.GetBooleanSetting("DoShowFanart", True)
        ConfigModifier.MainPoster = Master.eAdvancedSettings.GetBooleanSetting("DoShowPoster", True)
        ConfigModifier.MainExtrafanarts = ConfigModifier.MainFanart
        ConfigModifier.MainFanart = ConfigModifier.MainPoster

        strPrivateAPIKey = Master.eAdvancedSettings.GetStringSetting("ApiKey", "")
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

            settings.SetStringSetting("ApiKey", _setup.txtApiKey.Text)
        End Using
    End Sub

    Sub SaveSettings(ByVal DoDispose As Boolean) Implements Interfaces.IAddon_Image_Scraper_TV.SaveSettings
        ConfigModifier.EpisodePoster = _setup.chkScrapeEpisodePoster.Checked
        ConfigModifier.SeasonBanner = _setup.chkScrapeSeasonBanner.Checked
        ConfigModifier.SeasonPoster = _setup.chkScrapeSeasonPoster.Checked
        ConfigModifier.MainBanner = _setup.chkScrapeShowBanner.Checked
        ConfigModifier.MainFanart = _setup.chkScrapeShowFanart.Checked
        ConfigModifier.MainPoster = _setup.chkScrapeShowPoster.Checked
        ConfigModifier.MainExtrafanarts = ConfigModifier.MainFanart
        ConfigModifier.MainFanart = ConfigModifier.MainPoster
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

    Function Scraper(ByRef DBTV As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Image_Scraper_TV.Scraper
        logger.Trace("[TVDB_Image] [Scraper] [Start]")

        LoadSettings()
        Dim _scraper As New Scraper(_SpecialSettings)

        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, ConfigModifier)

        Select Case DBTV.ContentType
            Case Enums.ContentType.TVEpisode
                If DBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetImages_TVEpisode(DBTV.TVShow.UniqueIDs.TVDbId, DBTV.TVEpisode.Season, DBTV.TVEpisode.Episode, DBTV.Ordering, FilteredModifiers)
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = _scraper.GetAllImages_TV(DBTV.TVShow.UniqueIDs.TVDbId, FilteredModifiers, DBTV.Language_Main).MainFanarts
                    End If
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.TVEpisode.Title))
                End If
            Case Enums.ContentType.TVSeason
                If DBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetAllImages_TV(DBTV.TVShow.UniqueIDs.TVDbId, FilteredModifiers, DBTV.Language_Main)
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.TVSeason.Title))
                End If
            Case Enums.ContentType.TVShow
                If DBTV.TVShow.UniqueIDs.TVDbIdSpecified Then
                    ImagesContainer = _scraper.GetAllImages_TV(DBTV.TVShow.UniqueIDs.TVDbId, FilteredModifiers, DBTV.Language_Main)
                Else
                    logger.Trace(String.Concat("[TVDB_Image] [Scraper] [Abort] No TVDB ID exist to search: ", DBTV.TVShow.Title))
                End If
            Case Else
        End Select

        logger.Trace("[TVDB_Image] [Scraper] [Done]")
        Return New Interfaces.AddonResult_Generic
    End Function

    Public Sub ScraperOrderChanged() Implements Interfaces.IAddon_Image_Scraper_TV.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure AddonSettings

#Region "Fields"

        Dim ApiKey As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class