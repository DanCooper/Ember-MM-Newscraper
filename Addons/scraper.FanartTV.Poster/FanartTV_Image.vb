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

Imports System.IO
Imports EmberAPI
Imports RestSharp
Imports WatTmdb
Imports ScraperModule.FanartTVs
Imports NLog
Imports System.Diagnostics

Public Class FanartTV_Image
    Implements Interfaces.ScraperModule_Image_Movie
    Implements Interfaces.ScraperModule_Image_MovieSet


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Shared ConfigOptions_Movie As New Structures.ScrapeOptions_Movie
    Public Shared ConfigOptions_MovieSet As New Structures.ScrapeOptions_MovieSet
    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifier
    Public Shared ConfigScrapeModifier_MovieSet As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private strPrivateAPIKey As String = String.Empty
    Private _MySettings_Movie As New sMySettings
    Private _MySettings_MovieSet As New sMySettings
    Private _Name As String = "FanartTV_Poster"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _setup_Movie As frmFanartTVMediaSettingsHolder_Movie
    Private _setup_MovieSet As frmFanartTVMediaSettingsHolder_MovieSet
    Private _fanartTV As FanartTVs.Scraper

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Image_Movie.ModuleSettingsChanged

    Public Event MovieScraperEvent_Movie(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_Movie.ScraperEvent

    Public Event SetupScraperChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_Movie.ScraperSetupChanged

    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Image_Movie.SetupNeedsRestart

    Public Event ImagesDownloaded_Movie(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_Movie.ImagesDownloaded

    Public Event ProgressUpdated_Movie(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_Movie.ProgressUpdated


    Public Event ModuleSettingsChanged_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.ModuleSettingsChanged

    Public Event MovieScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEvent

    Public Event SetupScraperChanged_MovieSet(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperSetupChanged

    Public Event SetupNeedsRestart_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.SetupNeedsRestart

    Public Event ImagesDownloaded_MovieSet(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_MovieSet.ImagesDownloaded

    Public Event ProgressUpdated_MovieSet(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleName, Interfaces.ScraperModule_Image_MovieSet.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleVersion, Interfaces.ScraperModule_Image_MovieSet.ModuleVersion
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.ScraperModule_Image_Movie.ScraperEnabled
        Get
            Return _ScraperEnabled_Movie
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_Movie = value
        End Set
    End Property

    Property ScraperEnabled_MovieSet() As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEnabled
        Get
            Return _ScraperEnabled_MovieSet
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_MovieSet = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Function QueryScraperCapabilities_Movie(ByVal cap As Enums.ScraperCapabilities) As Boolean Implements Interfaces.ScraperModule_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ScraperCapabilities.Banner
                Return ConfigScrapeModifier_Movie.Banner
            Case Enums.ScraperCapabilities.CharacterArt
                Return ConfigScrapeModifier_Movie.CharacterArt
            Case Enums.ScraperCapabilities.ClearArt
                Return ConfigScrapeModifier_Movie.ClearArt
            Case Enums.ScraperCapabilities.ClearLogo
                Return ConfigScrapeModifier_Movie.ClearLogo
            Case Enums.ScraperCapabilities.DiscArt
                Return ConfigScrapeModifier_Movie.DiscArt
            Case Enums.ScraperCapabilities.Fanart
                Return ConfigScrapeModifier_Movie.Fanart
            Case Enums.ScraperCapabilities.Landscape
                Return ConfigScrapeModifier_Movie.Landscape
            Case Enums.ScraperCapabilities.Poster
                Return ConfigScrapeModifier_Movie.Poster
        End Select
        Return False
    End Function
    Function QueryScraperCapabilities_MovieSet(ByVal cap As Enums.ScraperCapabilities) As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.QueryScraperCapabilities
        Select Case cap
            Case Enums.ScraperCapabilities.Banner
                Return ConfigScrapeModifier_MovieSet.Banner
            Case Enums.ScraperCapabilities.CharacterArt
                Return ConfigScrapeModifier_MovieSet.CharacterArt
            Case Enums.ScraperCapabilities.ClearArt
                Return ConfigScrapeModifier_MovieSet.ClearArt
            Case Enums.ScraperCapabilities.ClearLogo
                Return ConfigScrapeModifier_MovieSet.ClearLogo
            Case Enums.ScraperCapabilities.DiscArt
                Return ConfigScrapeModifier_MovieSet.DiscArt
            Case Enums.ScraperCapabilities.Fanart
                Return ConfigScrapeModifier_MovieSet.Fanart
            Case Enums.ScraperCapabilities.Landscape
                Return ConfigScrapeModifier_MovieSet.Landscape
            Case Enums.ScraperCapabilities.Poster
                Return ConfigScrapeModifier_MovieSet.Poster
        End Select
        Return False
    End Function

    Private Sub Handle_ModuleSettingsChanged_Movie()
        RaiseEvent ModuleSettingsChanged_Movie()
    End Sub

    Private Sub Handle_ModuleSettingsChanged_MovieSet()
        RaiseEvent ModuleSettingsChanged_MovieSet()
    End Sub

    Private Sub Handle_SetupNeedsRestart_Movie()
        RaiseEvent SetupNeedsRestart_Movie()
    End Sub

    Private Sub Handle_SetupNeedsRestart_MovieSet()
        RaiseEvent SetupNeedsRestart_MovieSet()
    End Sub

    Private Sub Handle_SetupScraperChanged_Movie(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_Movie = state
        RaiseEvent SetupScraperChanged_Movie(String.Concat(Me._Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_MovieSet(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_MovieSet = state
        RaiseEvent SetupScraperChanged_MovieSet(String.Concat(Me._Name, "_MovieSet"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
        'Must be after Load settings to retrieve the correct API key
        _fanartTV = New FanartTVs.Scraper(_MySettings_Movie)
    End Sub

    Sub Init_MovieSet(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_MovieSet.Init
        _AssemblyName = sAssemblyName
        LoadSettings_MovieSet()
        'Must be after Load settings to retrieve the correct API key
        _fanartTV = New FanartTVs.Scraper(_MySettings_MovieSet)
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_Movie.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_Movie = New frmFanartTVMediaSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.cbEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.chkGetBlankImages.Checked = _MySettings_Movie.GetBlankImages
        _setup_Movie.chkGetEnglishImages.Checked = _MySettings_Movie.GetEnglishImages
        _setup_Movie.chkPrefLanguageOnly.Checked = _MySettings_Movie.PrefLanguageOnly
        _setup_Movie.chkScrapePoster.Checked = ConfigScrapeModifier_Movie.Poster
        _setup_Movie.chkScrapeFanart.Checked = ConfigScrapeModifier_Movie.Fanart
        _setup_Movie.chkScrapeBanner.Checked = ConfigScrapeModifier_Movie.Banner
        _setup_Movie.chkScrapeCharacterArt.Checked = ConfigScrapeModifier_Movie.CharacterArt
        _setup_Movie.chkScrapeClearArt.Checked = ConfigScrapeModifier_Movie.ClearArt
        _setup_Movie.chkScrapeClearArtOnlyHD.Checked = _MySettings_Movie.ClearArtOnlyHD
        _setup_Movie.chkScrapeClearLogo.Checked = ConfigScrapeModifier_Movie.ClearLogo
        _setup_Movie.chkScrapeClearLogoOnlyHD.Checked = _MySettings_Movie.ClearLogoOnlyHD
        _setup_Movie.chkScrapeDiscArt.Checked = ConfigScrapeModifier_Movie.DiscArt
        _setup_Movie.chkScrapeLandscape.Checked = ConfigScrapeModifier_Movie.Landscape
        _setup_Movie.txtApiKey.Text = strPrivateAPIKey
        _setup_Movie.cbPrefLanguage.Text = _MySettings_Movie.PrefLanguage

        _setup_Movie.orderChanged()
        Spanel.Name = String.Concat(Me._Name, "_Movie")
        Spanel.Text = Master.eLang.GetString(788, "FanartTV")
        Spanel.Prefix = "FanartTVMovieMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlMovieMedia"
        Spanel.Type = Master.eLang.GetString(36, "Movies")
        Spanel.ImageIndex = If(Me._ScraperEnabled_Movie, 9, 10)
        Spanel.Panel = Me._setup_Movie.pnlSettings

        AddHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
        AddHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
        AddHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
        Return Spanel
    End Function

    Function InjectSetupScraper_MovieSet() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_MovieSet.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_MovieSet = New frmFanartTVMediaSettingsHolder_MovieSet
        LoadSettings_MovieSet()
        _setup_MovieSet.cbEnabled.Checked = _ScraperEnabled_MovieSet
        _setup_MovieSet.chkGetBlankImages.Checked = _MySettings_MovieSet.GetBlankImages
        _setup_MovieSet.chkGetEnglishImages.Checked = _MySettings_MovieSet.GetEnglishImages
        _setup_MovieSet.chkPrefLanguageOnly.Checked = _MySettings_MovieSet.PrefLanguageOnly
        _setup_MovieSet.chkScrapePoster.Checked = ConfigScrapeModifier_MovieSet.Poster
        _setup_MovieSet.chkScrapeFanart.Checked = ConfigScrapeModifier_MovieSet.Fanart
        _setup_MovieSet.chkScrapeBanner.Checked = ConfigScrapeModifier_MovieSet.Banner
        _setup_MovieSet.chkScrapeCharacterArt.Checked = ConfigScrapeModifier_MovieSet.CharacterArt
        _setup_MovieSet.chkScrapeClearArt.Checked = ConfigScrapeModifier_MovieSet.ClearArt
        _setup_MovieSet.chkScrapeClearArtOnlyHD.Checked = _MySettings_MovieSet.ClearArtOnlyHD
        _setup_MovieSet.chkScrapeClearLogo.Checked = ConfigScrapeModifier_MovieSet.ClearLogo
        _setup_MovieSet.chkScrapeClearLogoOnlyHD.Checked = _MySettings_MovieSet.ClearLogoOnlyHD
        _setup_MovieSet.chkScrapeDiscArt.Checked = ConfigScrapeModifier_MovieSet.DiscArt
        _setup_MovieSet.chkScrapeLandscape.Checked = ConfigScrapeModifier_MovieSet.Landscape
        _setup_MovieSet.txtApiKey.Text = strPrivateAPIKey
        _setup_MovieSet.cbPrefLanguage.Text = _MySettings_MovieSet.PrefLanguage

        _setup_MovieSet.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_MovieSet")
        Spanel.Text = Master.eLang.GetString(788, "FanartTV")
        Spanel.Prefix = "FanartTVMovieSetMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlMovieSetMedia"
        Spanel.Type = Master.eLang.GetString(1203, "MovieSets")
        Spanel.ImageIndex = If(Me._ScraperEnabled_MovieSet, 9, 10)
        Spanel.Panel = Me._setup_MovieSet.pnlSettings

        AddHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
        AddHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
        AddHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
        Return Spanel
    End Function

    Sub LoadSettings_Movie()
        strPrivateAPIKey = clsAdvancedSettings.GetSetting("ApiKey", "", , Enums.Content_Type.Movie)
        _MySettings_Movie.ApiKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "ea68f9d0847c1b7643813c70cbfc0196", strPrivateAPIKey)
        _MySettings_Movie.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.Content_Type.Movie)
        _MySettings_Movie.PrefLanguageOnly = clsAdvancedSettings.GetBooleanSetting("PrefLanguageOnly", False, , Enums.Content_Type.Movie)
        _MySettings_Movie.GetBlankImages = clsAdvancedSettings.GetBooleanSetting("GetBlankImages", False, , Enums.Content_Type.Movie)
        _MySettings_Movie.GetEnglishImages = clsAdvancedSettings.GetBooleanSetting("GetEnglishImages", False, , Enums.Content_Type.Movie)
        _MySettings_Movie.ClearArtOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearArtOnlyHD", False, , Enums.Content_Type.Movie)
        _MySettings_Movie.ClearLogoOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearLogoOnlyHD", False, , Enums.Content_Type.Movie)

        ConfigScrapeModifier_Movie.Poster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.Fanart = clsAdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.Banner = clsAdvancedSettings.GetBooleanSetting("DoBanner", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.CharacterArt = clsAdvancedSettings.GetBooleanSetting("DoCharacterArt", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.ClearArt = clsAdvancedSettings.GetBooleanSetting("DoClearArt", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.ClearLogo = clsAdvancedSettings.GetBooleanSetting("DoClearLogo", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.DiscArt = clsAdvancedSettings.GetBooleanSetting("DoDiscArt", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.Landscape = clsAdvancedSettings.GetBooleanSetting("DoLandscape", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.EThumbs = ConfigScrapeModifier_Movie.Fanart
        ConfigScrapeModifier_Movie.EFanarts = ConfigScrapeModifier_Movie.Fanart
    End Sub

    Sub LoadSettings_MovieSet()
        strPrivateAPIKey = clsAdvancedSettings.GetSetting("ApiKey", "", , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.ApiKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "ea68f9d0847c1b7643813c70cbfc0196", strPrivateAPIKey)
        _MySettings_MovieSet.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.PrefLanguageOnly = clsAdvancedSettings.GetBooleanSetting("PrefLanguageOnly", False, , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.GetBlankImages = clsAdvancedSettings.GetBooleanSetting("GetBlankImages", False, , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.GetEnglishImages = clsAdvancedSettings.GetBooleanSetting("GetEnglishImages", False, , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.ClearArtOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearArtOnlyHD", False, , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.ClearLogoOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearLogoOnlyHD", False, , Enums.Content_Type.MovieSet)

        ConfigScrapeModifier_MovieSet.Poster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.Fanart = clsAdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.Banner = clsAdvancedSettings.GetBooleanSetting("DoBanner", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.CharacterArt = clsAdvancedSettings.GetBooleanSetting("DoCharacterArt", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.ClearArt = clsAdvancedSettings.GetBooleanSetting("DoClearArt", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.ClearLogo = clsAdvancedSettings.GetBooleanSetting("DoClearLogo", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.DiscArt = clsAdvancedSettings.GetBooleanSetting("DoDiscArt", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.Landscape = clsAdvancedSettings.GetBooleanSetting("DoLandscape", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.EThumbs = ConfigScrapeModifier_MovieSet.Fanart
        ConfigScrapeModifier_MovieSet.EFanarts = ConfigScrapeModifier_MovieSet.Fanart
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("ClearArtOnlyHD", _MySettings_Movie.ClearArtOnlyHD, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("ClearLogoOnlyHD", _MySettings_Movie.ClearLogoOnlyHD, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier_Movie.Poster, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier_Movie.Fanart, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoBanner", ConfigScrapeModifier_Movie.Banner, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoCharacterArt", ConfigScrapeModifier_Movie.CharacterArt, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoClearArt", ConfigScrapeModifier_Movie.ClearArt, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoClearLogo", ConfigScrapeModifier_Movie.ClearLogo, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoDiscArt", ConfigScrapeModifier_Movie.DiscArt, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoLandscape", ConfigScrapeModifier_Movie.Landscape, , , Enums.Content_Type.Movie)

            settings.SetSetting("ApiKey", _setup_Movie.txtApiKey.Text, , , Enums.Content_Type.Movie)
            settings.SetSetting("PrefLanguage", _MySettings_Movie.PrefLanguage, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("GetBlankImages", _MySettings_Movie.GetBlankImages, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("GetEnglishImages", _MySettings_Movie.GetEnglishImages, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("PrefLanguageOnly", _MySettings_Movie.PrefLanguageOnly, , , Enums.Content_Type.Movie)
        End Using
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("ClearArtOnlyHD", _MySettings_MovieSet.ClearArtOnlyHD, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("ClearLogoOnlyHD", _MySettings_MovieSet.ClearLogoOnlyHD, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier_MovieSet.Poster, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier_MovieSet.Fanart, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoBanner", ConfigScrapeModifier_MovieSet.Banner, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoCharacterArt", ConfigScrapeModifier_MovieSet.CharacterArt, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoClearArt", ConfigScrapeModifier_MovieSet.ClearArt, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoClearLogo", ConfigScrapeModifier_MovieSet.ClearLogo, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoDiscArt", ConfigScrapeModifier_MovieSet.DiscArt, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoLandscape", ConfigScrapeModifier_MovieSet.Landscape, , , Enums.Content_Type.MovieSet)

            settings.SetSetting("ApiKey", _setup_MovieSet.txtApiKey.Text, , , Enums.Content_Type.MovieSet)
            settings.SetSetting("PrefLanguage", _MySettings_MovieSet.PrefLanguage, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("GetBlankImages", _MySettings_MovieSet.GetBlankImages, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("GetEnglishImages", _MySettings_MovieSet.GetEnglishImages, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("PrefLanguageOnly", _MySettings_MovieSet.PrefLanguageOnly, , , Enums.Content_Type.MovieSet)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_Movie.SaveSetupScraper
        _MySettings_Movie.PrefLanguage = _setup_Movie.cbPrefLanguage.Text
        _MySettings_Movie.PrefLanguageOnly = _setup_Movie.chkPrefLanguageOnly.Checked
        _MySettings_Movie.GetBlankImages = _setup_Movie.chkGetBlankImages.Checked
        _MySettings_Movie.GetEnglishImages = _setup_Movie.chkGetEnglishImages.Checked
        _MySettings_Movie.ClearArtOnlyHD = _setup_Movie.chkScrapeClearArtOnlyHD.Checked
        _MySettings_Movie.ClearLogoOnlyHD = _setup_Movie.chkScrapeClearLogoOnlyHD.Checked
        ConfigScrapeModifier_Movie.Poster = _setup_Movie.chkScrapePoster.Checked
        ConfigScrapeModifier_Movie.Fanart = _setup_Movie.chkScrapeFanart.Checked
        ConfigScrapeModifier_Movie.Banner = _setup_Movie.chkScrapeBanner.Checked
        ConfigScrapeModifier_Movie.CharacterArt = _setup_Movie.chkScrapeCharacterArt.Checked
        ConfigScrapeModifier_Movie.ClearArt = _setup_Movie.chkScrapeClearArt.Checked
        ConfigScrapeModifier_Movie.ClearLogo = _setup_Movie.chkScrapeClearLogo.Checked
        ConfigScrapeModifier_Movie.DiscArt = _setup_Movie.chkScrapeDiscArt.Checked
        ConfigScrapeModifier_Movie.Landscape = _setup_Movie.chkScrapeLandscape.Checked
        SaveSettings_Movie()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            RemoveHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_MovieSet(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_MovieSet.SaveSetupScraper
        _MySettings_MovieSet.PrefLanguage = _setup_MovieSet.cbPrefLanguage.Text
        _MySettings_MovieSet.PrefLanguageOnly = _setup_MovieSet.chkPrefLanguageOnly.Checked
        _MySettings_MovieSet.GetBlankImages = _setup_MovieSet.chkGetBlankImages.Checked
        _MySettings_MovieSet.GetEnglishImages = _setup_MovieSet.chkGetEnglishImages.Checked
        _MySettings_MovieSet.ClearArtOnlyHD = _setup_MovieSet.chkScrapeClearArtOnlyHD.Checked
        _MySettings_MovieSet.ClearLogoOnlyHD = _setup_MovieSet.chkScrapeClearLogoOnlyHD.Checked
        ConfigScrapeModifier_MovieSet.Poster = _setup_MovieSet.chkScrapePoster.Checked
        ConfigScrapeModifier_MovieSet.Fanart = _setup_MovieSet.chkScrapeFanart.Checked
        ConfigScrapeModifier_MovieSet.Banner = _setup_MovieSet.chkScrapeBanner.Checked
        ConfigScrapeModifier_MovieSet.CharacterArt = _setup_MovieSet.chkScrapeCharacterArt.Checked
        ConfigScrapeModifier_MovieSet.ClearArt = _setup_MovieSet.chkScrapeClearArt.Checked
        ConfigScrapeModifier_MovieSet.ClearLogo = _setup_MovieSet.chkScrapeClearLogo.Checked
        ConfigScrapeModifier_MovieSet.DiscArt = _setup_MovieSet.chkScrapeDiscArt.Checked
        ConfigScrapeModifier_MovieSet.Landscape = _setup_MovieSet.chkScrapeLandscape.Checked
        SaveSettings_MovieSet()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
            RemoveHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
            RemoveHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
            _setup_MovieSet.Dispose()
        End If
    End Sub

    Function Scraper(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_Movie.Scraper
        logger.Trace("Started scrape")

        LoadSettings_Movie()

        Dim Settings As FanartTVs.Scraper.sMySettings_ForScraper
        Settings.ApiKey = _MySettings_Movie.ApiKey
        Settings.ClearArtOnlyHD = _MySettings_Movie.ClearArtOnlyHD
        Settings.ClearLogoOnlyHD = _MySettings_Movie.ClearLogoOnlyHD
        Settings.GetBlankImages = _MySettings_Movie.GetBlankImages
        Settings.GetEnglishImages = _MySettings_Movie.GetEnglishImages
        Settings.PrefLanguage = _MySettings_Movie.PrefLanguage
        Settings.PrefLanguageOnly = _MySettings_Movie.PrefLanguageOnly

        If Not String.IsNullOrEmpty(DBMovie.Movie.ID) Then
            ImageList = _fanartTV.GetImages_Movie_MovieSet(DBMovie.Movie.ID, Type, Settings)
        ElseIf Not String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            ImageList = _fanartTV.GetImages_Movie_MovieSet(DBMovie.Movie.TMDBID, Type, Settings)
        Else
            logger.Trace(String.Concat("No IMDB and TMDB ID exist to search: ", DBMovie.ListTitle))
        End If

        logger.Trace(New StackFrame().GetMethod().Name, "Finished scrape")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper(ByRef DBMovieset As Structures.DBMovieSet, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_MovieSet.Scraper
        logger.Trace("Started scrape")

        LoadSettings_MovieSet()

        Dim Settings As FanartTVs.Scraper.sMySettings_ForScraper
        Settings.ApiKey = _MySettings_MovieSet.ApiKey
        Settings.ClearArtOnlyHD = _MySettings_MovieSet.ClearArtOnlyHD
        Settings.ClearLogoOnlyHD = _MySettings_MovieSet.ClearLogoOnlyHD
        Settings.GetBlankImages = _MySettings_MovieSet.GetBlankImages
        Settings.GetEnglishImages = _MySettings_MovieSet.GetEnglishImages
        Settings.PrefLanguage = _MySettings_MovieSet.PrefLanguage
        Settings.PrefLanguageOnly = _MySettings_MovieSet.PrefLanguageOnly

        ImageList = _fanartTV.GetImages_Movie_MovieSet(DBMovieset.MovieSet.ID, Type, Settings)

        logger.Trace("Finished scrape")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements EmberAPI.Interfaces.ScraperModule_Image_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_MovieSet() Implements EmberAPI.Interfaces.ScraperModule_Image_MovieSet.ScraperOrderChanged
        _setup_MovieSet.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"
        Dim ApiKey As String
        Dim ClearArtOnlyHD As Boolean
        Dim ClearLogoOnlyHD As Boolean
        Dim GetEnglishImages As Boolean
        Dim GetBlankImages As Boolean
        Dim PrefLanguage As String
        Dim PrefLanguageOnly As Boolean
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class