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
Imports ScraperModule.FanartTVs
Imports NLog
Imports System.Diagnostics

Public Class FanartTV_Image
    Implements Interfaces.ScraperModule_Image_Movie
    Implements Interfaces.ScraperModule_Image_MovieSet
    Implements Interfaces.ScraperModule_Image_TV


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Shared ConfigModifier_Movie As New Structures.ScrapeModifiers
    Public Shared ConfigModifier_MovieSet As New Structures.ScrapeModifiers
    Public Shared ConfigModifier_TV As New Structures.ScrapeModifiers
    Public Shared _AssemblyName As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private _SpecialSettings_Movie As New SpecialSettings
    Private _SpecialSettings_MovieSet As New SpecialSettings
    Private _SpecialSettings_TV As New SpecialSettings
    Private _Name As String = "FanartTV_Image"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_MovieSet As frmSettingsHolder_MovieSet
    Private _setup_TV As frmSettingsHolder_TV

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Image_Movie.ModuleSettingsChanged
    Public Event MovieScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_Movie.ScraperEvent
    Public Event SetupScraperChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_Movie.ScraperSetupChanged
    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Image_Movie.SetupNeedsRestart
    Public Event ImagesDownloaded_Movie(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_Movie.ImagesDownloaded
    Public Event ProgressUpdated_Movie(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_Movie.ProgressUpdated

    'MovieSet part
    Public Event ModuleSettingsChanged_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.ModuleSettingsChanged
    Public Event MovieScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEvent
    Public Event SetupScraperChanged_MovieSet(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperSetupChanged
    Public Event SetupNeedsRestart_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.SetupNeedsRestart
    Public Event ImagesDownloaded_MovieSet(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_MovieSet.ImagesDownloaded
    Public Event ProgressUpdated_MovieSet(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ProgressUpdated

    'TV part
    Public Event ModuleSettingsChanged_TV() Implements Interfaces.ScraperModule_Image_TV.ModuleSettingsChanged
    Public Event MovieScraperEvent_TV(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_TV.ScraperEvent
    Public Event SetupScraperChanged_TV(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_TV.ScraperSetupChanged
    Public Event SetupNeedsRestart_TV() Implements Interfaces.ScraperModule_Image_TV.SetupNeedsRestart
    Public Event ImagesDownloaded_TV(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_TV.ImagesDownloaded
    Public Event ProgressUpdated_TV(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_TV.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleName, Interfaces.ScraperModule_Image_MovieSet.ModuleName, Interfaces.ScraperModule_Image_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleVersion, Interfaces.ScraperModule_Image_MovieSet.ModuleVersion, Interfaces.ScraperModule_Image_TV.ModuleVersion
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

    Property ScraperEnabled_TV() As Boolean Implements Interfaces.ScraperModule_Image_TV.ScraperEnabled
        Get
            Return _ScraperEnabled_TV
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_TV = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Function QueryScraperCapabilities_Movie(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.ScraperModule_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainBanner
                Return ConfigModifier_Movie.MainBanner
            Case Enums.ModifierType.MainClearArt
                Return ConfigModifier_Movie.MainClearArt
            Case Enums.ModifierType.MainClearLogo
                Return ConfigModifier_Movie.MainClearLogo
            Case Enums.ModifierType.MainDiscArt
                Return ConfigModifier_Movie.MainDiscArt
            Case Enums.ModifierType.MainFanart
                Return ConfigModifier_Movie.MainFanart
            Case Enums.ModifierType.MainLandscape
                Return ConfigModifier_Movie.MainLandscape
            Case Enums.ModifierType.MainPoster
                Return ConfigModifier_Movie.MainPoster
        End Select
        Return False
    End Function

    Function QueryScraperCapabilities_MovieSet(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainBanner
                Return ConfigModifier_MovieSet.MainBanner
            Case Enums.ModifierType.MainClearArt
                Return ConfigModifier_MovieSet.MainClearArt
            Case Enums.ModifierType.MainClearLogo
                Return ConfigModifier_MovieSet.MainClearLogo
            Case Enums.ModifierType.MainDiscArt
                Return ConfigModifier_MovieSet.MainDiscArt
            Case Enums.ModifierType.MainFanart
                Return ConfigModifier_MovieSet.MainFanart
            Case Enums.ModifierType.MainLandscape
                Return ConfigModifier_MovieSet.MainLandscape
            Case Enums.ModifierType.MainPoster
                Return ConfigModifier_MovieSet.MainPoster
        End Select
        Return False
    End Function

    Function QueryScraperCapabilities_TV(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.ScraperModule_Image_TV.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainBanner
                Return ConfigModifier_TV.MainBanner
            Case Enums.ModifierType.MainCharacterArt
                Return ConfigModifier_TV.MainCharacterArt
            Case Enums.ModifierType.MainClearArt
                Return ConfigModifier_TV.MainClearArt
            Case Enums.ModifierType.MainClearLogo
                Return ConfigModifier_TV.MainClearLogo
            Case Enums.ModifierType.MainFanart
                Return ConfigModifier_TV.MainFanart
            Case Enums.ModifierType.MainLandscape
                Return ConfigModifier_TV.MainLandscape
            Case Enums.ModifierType.MainPoster
                Return ConfigModifier_TV.MainPoster
            Case Enums.ModifierType.SeasonBanner
                Return ConfigModifier_TV.SeasonBanner
            Case Enums.ModifierType.SeasonLandscape
                Return ConfigModifier_TV.SeasonLandscape
            Case Enums.ModifierType.SeasonPoster
                Return ConfigModifier_TV.SeasonPoster
        End Select
        Return False
    End Function

    Private Sub Handle_ModuleSettingsChanged_Movie()
        RaiseEvent ModuleSettingsChanged_Movie()
    End Sub

    Private Sub Handle_ModuleSettingsChanged_MovieSet()
        RaiseEvent ModuleSettingsChanged_MovieSet()
    End Sub

    Private Sub Handle_ModuleSettingsChanged_TV()
        RaiseEvent ModuleSettingsChanged_TV()
    End Sub

    Private Sub Handle_SetupNeedsRestart_Movie()
        RaiseEvent SetupNeedsRestart_Movie()
    End Sub

    Private Sub Handle_SetupNeedsRestart_MovieSet()
        RaiseEvent SetupNeedsRestart_MovieSet()
    End Sub

    Private Sub Handle_SetupNeedsRestart_TV()
        RaiseEvent SetupNeedsRestart_TV()
    End Sub

    Private Sub Handle_SetupScraperChanged_Movie(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_Movie = state
        RaiseEvent SetupScraperChanged_Movie(String.Concat(Me._Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_MovieSet(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_MovieSet = state
        RaiseEvent SetupScraperChanged_MovieSet(String.Concat(Me._Name, "_MovieSet"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_TV(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_TV = state
        RaiseEvent SetupScraperChanged_TV(String.Concat(Me._Name, "_TV"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
    End Sub

    Sub Init_MovieSet(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_MovieSet.Init
        _AssemblyName = sAssemblyName
        LoadSettings_MovieSet()
    End Sub

    Sub Init_TV(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings_TV()
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_Movie.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_Movie = New frmSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.chkEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.chkScrapePoster.Checked = ConfigModifier_Movie.MainPoster
        _setup_Movie.chkScrapeFanart.Checked = ConfigModifier_Movie.MainFanart
        _setup_Movie.chkScrapeBanner.Checked = ConfigModifier_Movie.MainBanner
        _setup_Movie.chkScrapeClearArt.Checked = ConfigModifier_Movie.MainClearArt
        _setup_Movie.chkScrapeClearArtOnlyHD.Checked = _SpecialSettings_Movie.ClearArtOnlyHD
        _setup_Movie.chkScrapeClearLogo.Checked = ConfigModifier_Movie.MainClearLogo
        _setup_Movie.chkScrapeClearLogoOnlyHD.Checked = _SpecialSettings_Movie.ClearLogoOnlyHD
        _setup_Movie.chkScrapeDiscArt.Checked = ConfigModifier_Movie.MainDiscArt
        _setup_Movie.chkScrapeLandscape.Checked = ConfigModifier_Movie.MainLandscape
        _setup_Movie.txtApiKey.Text = _SpecialSettings_Movie.ApiKey

        If Not String.IsNullOrEmpty(_SpecialSettings_Movie.ApiKey) Then
            _setup_Movie.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_Movie.lblEMMAPI.Visible = False
            _setup_Movie.txtApiKey.Enabled = True
        End If

        _setup_Movie.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_Movie")
        Spanel.Text = "FanartTV"
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
        _setup_MovieSet = New frmSettingsHolder_MovieSet
        LoadSettings_MovieSet()
        _setup_MovieSet.chkEnabled.Checked = _ScraperEnabled_MovieSet
        _setup_MovieSet.chkScrapePoster.Checked = ConfigModifier_MovieSet.MainPoster
        _setup_MovieSet.chkScrapeFanart.Checked = ConfigModifier_MovieSet.MainFanart
        _setup_MovieSet.chkScrapeBanner.Checked = ConfigModifier_MovieSet.MainBanner
        _setup_MovieSet.chkScrapeClearArt.Checked = ConfigModifier_MovieSet.MainClearArt
        _setup_MovieSet.chkScrapeClearArtOnlyHD.Checked = _SpecialSettings_MovieSet.ClearArtOnlyHD
        _setup_MovieSet.chkScrapeClearLogo.Checked = ConfigModifier_MovieSet.MainClearLogo
        _setup_MovieSet.chkScrapeClearLogoOnlyHD.Checked = _SpecialSettings_MovieSet.ClearLogoOnlyHD
        _setup_MovieSet.chkScrapeDiscArt.Checked = ConfigModifier_MovieSet.MainDiscArt
        _setup_MovieSet.chkScrapeLandscape.Checked = ConfigModifier_MovieSet.MainLandscape
        _setup_MovieSet.txtApiKey.Text = _SpecialSettings_MovieSet.ApiKey

        If Not String.IsNullOrEmpty(_SpecialSettings_MovieSet.ApiKey) Then
            _setup_MovieSet.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_MovieSet.lblEMMAPI.Visible = False
            _setup_MovieSet.txtApiKey.Enabled = True
        End If

        _setup_MovieSet.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_MovieSet")
        Spanel.Text = "FanartTV"
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

    Function InjectSetupScraper_TV() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_TV.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_TV = New frmSettingsHolder_TV
        LoadSettings_TV()
        _setup_TV.chkEnabled.Checked = _ScraperEnabled_TV
        _setup_TV.chkScrapeSeasonBanner.Checked = ConfigModifier_TV.SeasonBanner
        _setup_TV.chkScrapeSeasonLandscape.Checked = ConfigModifier_TV.SeasonLandscape
        _setup_TV.chkScrapeSeasonPoster.Checked = ConfigModifier_TV.SeasonPoster
        _setup_TV.chkScrapeShowBanner.Checked = ConfigModifier_TV.MainBanner
        _setup_TV.chkScrapeShowCharacterArt.Checked = ConfigModifier_TV.MainCharacterArt
        _setup_TV.chkScrapeShowClearArt.Checked = ConfigModifier_TV.MainClearArt
        _setup_TV.chkScrapeShowClearArtOnlyHD.Checked = _SpecialSettings_TV.ClearArtOnlyHD
        _setup_TV.chkScrapeShowClearLogo.Checked = ConfigModifier_TV.MainClearLogo
        _setup_TV.chkScrapeShowClearLogoOnlyHD.Checked = _SpecialSettings_TV.ClearLogoOnlyHD
        _setup_TV.chkScrapeShowFanart.Checked = ConfigModifier_TV.MainFanart
        _setup_TV.chkScrapeShowLandscape.Checked = ConfigModifier_TV.MainLandscape
        _setup_TV.chkScrapeShowPoster.Checked = ConfigModifier_TV.MainPoster
        _setup_TV.txtApiKey.Text = _SpecialSettings_TV.ApiKey

        If Not String.IsNullOrEmpty(_SpecialSettings_TV.ApiKey) Then
            _setup_TV.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_TV.lblEMMAPI.Visible = False
            _setup_TV.txtApiKey.Enabled = True
        End If

        _setup_TV.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_TV")
        Spanel.Text = "FanartTV"
        Spanel.Prefix = "FanartTVTVMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlTVMedia"
        Spanel.Type = Master.eLang.GetString(653, "TV Shows")
        Spanel.ImageIndex = If(Me._ScraperEnabled_TV, 9, 10)
        Spanel.Panel = Me._setup_TV.pnlSettings

        AddHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
        AddHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
        AddHandler _setup_TV.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_TV
        Return Spanel
    End Function

    Sub LoadSettings_Movie()
        _SpecialSettings_Movie.ApiKey = clsAdvancedSettings.GetSetting("ApiKey", "", , Enums.ContentType.Movie)
        _SpecialSettings_Movie.ClearArtOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearArtOnlyHD", False, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.ClearLogoOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearLogoOnlyHD", False, , Enums.ContentType.Movie)

        ConfigModifier_Movie.MainPoster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainFanart = clsAdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainBanner = clsAdvancedSettings.GetBooleanSetting("DoBanner", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainClearArt = clsAdvancedSettings.GetBooleanSetting("DoClearArt", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainClearLogo = clsAdvancedSettings.GetBooleanSetting("DoClearLogo", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainDiscArt = clsAdvancedSettings.GetBooleanSetting("DoDiscArt", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainLandscape = clsAdvancedSettings.GetBooleanSetting("DoLandscape", True, , Enums.ContentType.Movie)
        ConfigModifier_Movie.MainExtrafanarts = ConfigModifier_Movie.MainFanart
        ConfigModifier_Movie.MainExtrathumbs = ConfigModifier_Movie.MainFanart
    End Sub

    Sub LoadSettings_MovieSet()
        _SpecialSettings_MovieSet.ApiKey = clsAdvancedSettings.GetSetting("ApiKey", "", , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.ClearArtOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearArtOnlyHD", False, , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.ClearLogoOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearLogoOnlyHD", False, , Enums.ContentType.MovieSet)

        ConfigModifier_MovieSet.MainPoster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainFanart = clsAdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainBanner = clsAdvancedSettings.GetBooleanSetting("DoBanner", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainClearArt = clsAdvancedSettings.GetBooleanSetting("DoClearArt", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainClearLogo = clsAdvancedSettings.GetBooleanSetting("DoClearLogo", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainDiscArt = clsAdvancedSettings.GetBooleanSetting("DoDiscArt", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainLandscape = clsAdvancedSettings.GetBooleanSetting("DoLandscape", True, , Enums.ContentType.MovieSet)
        ConfigModifier_MovieSet.MainExtrafanarts = ConfigModifier_MovieSet.MainFanart
        ConfigModifier_MovieSet.MainExtrathumbs = ConfigModifier_MovieSet.MainFanart
    End Sub

    Sub LoadSettings_TV()
        _SpecialSettings_TV.ApiKey = clsAdvancedSettings.GetSetting("ApiKey", "", , Enums.ContentType.TV)
        _SpecialSettings_TV.ClearArtOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearArtOnlyHD", False, , Enums.ContentType.TV)
        _SpecialSettings_TV.ClearLogoOnlyHD = clsAdvancedSettings.GetBooleanSetting("ClearLogoOnlyHD", False, , Enums.ContentType.TV)

        ConfigModifier_TV.SeasonBanner = clsAdvancedSettings.GetBooleanSetting("DoSeasonBanner", True, , Enums.ContentType.TV)
        ConfigModifier_TV.SeasonLandscape = clsAdvancedSettings.GetBooleanSetting("DoSeasonLandscape", True, , Enums.ContentType.TV)
        ConfigModifier_TV.SeasonPoster = clsAdvancedSettings.GetBooleanSetting("DoSeasonPoster", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainBanner = clsAdvancedSettings.GetBooleanSetting("DoShowBanner", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainCharacterArt = clsAdvancedSettings.GetBooleanSetting("DoShowCharacterArt", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainClearArt = clsAdvancedSettings.GetBooleanSetting("DoShowClearArt", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainClearLogo = clsAdvancedSettings.GetBooleanSetting("DoShowClearLogo", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainFanart = clsAdvancedSettings.GetBooleanSetting("DoShowFanart", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainLandscape = clsAdvancedSettings.GetBooleanSetting("DoShowLandscape", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainPoster = clsAdvancedSettings.GetBooleanSetting("DoShowPoster", True, , Enums.ContentType.TV)
        ConfigModifier_TV.MainExtrafanarts = ConfigModifier_TV.MainFanart
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("ClearArtOnlyHD", _SpecialSettings_Movie.ClearArtOnlyHD, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("ClearLogoOnlyHD", _SpecialSettings_Movie.ClearLogoOnlyHD, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPoster", ConfigModifier_Movie.MainPoster, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigModifier_Movie.MainFanart, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoBanner", ConfigModifier_Movie.MainBanner, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoClearArt", ConfigModifier_Movie.MainClearArt, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoClearLogo", ConfigModifier_Movie.MainClearLogo, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoDiscArt", ConfigModifier_Movie.MainDiscArt, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoLandscape", ConfigModifier_Movie.MainLandscape, , , Enums.ContentType.Movie)

            settings.SetSetting("ApiKey", _setup_Movie.txtApiKey.Text, , , Enums.ContentType.Movie)
        End Using
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("ClearArtOnlyHD", _SpecialSettings_MovieSet.ClearArtOnlyHD, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("ClearLogoOnlyHD", _SpecialSettings_MovieSet.ClearLogoOnlyHD, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoPoster", ConfigModifier_MovieSet.MainPoster, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoFanart", ConfigModifier_MovieSet.MainFanart, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoBanner", ConfigModifier_MovieSet.MainBanner, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoClearArt", ConfigModifier_MovieSet.MainClearArt, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoClearLogo", ConfigModifier_MovieSet.MainClearLogo, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoDiscArt", ConfigModifier_MovieSet.MainDiscArt, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoLandscape", ConfigModifier_MovieSet.MainLandscape, , , Enums.ContentType.MovieSet)

            settings.SetSetting("ApiKey", _setup_MovieSet.txtApiKey.Text, , , Enums.ContentType.MovieSet)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("ClearArtOnlyHD", _SpecialSettings_TV.ClearArtOnlyHD, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("ClearLogoOnlyHD", _SpecialSettings_TV.ClearLogoOnlyHD, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoSeasonBanner", ConfigModifier_TV.SeasonBanner, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoSeasonLandscape", ConfigModifier_TV.SeasonLandscape, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoSeasonPoster", ConfigModifier_TV.SeasonPoster, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowBanner", ConfigModifier_TV.MainBanner, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowCharacterArt", ConfigModifier_TV.MainCharacterArt, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowClearArt", ConfigModifier_TV.MainClearArt, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowClearLogo", ConfigModifier_TV.MainClearLogo, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowFanart", ConfigModifier_TV.MainFanart, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowLandscape", ConfigModifier_TV.MainLandscape, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowPoster", ConfigModifier_TV.MainPoster, , , Enums.ContentType.TV)

            settings.SetSetting("ApiKey", _setup_TV.txtApiKey.Text, , , Enums.ContentType.TV)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_Movie.SaveSetupScraper
        _SpecialSettings_Movie.ClearArtOnlyHD = _setup_Movie.chkScrapeClearArtOnlyHD.Checked
        _SpecialSettings_Movie.ClearLogoOnlyHD = _setup_Movie.chkScrapeClearLogoOnlyHD.Checked
        ConfigModifier_Movie.MainPoster = _setup_Movie.chkScrapePoster.Checked
        ConfigModifier_Movie.MainFanart = _setup_Movie.chkScrapeFanart.Checked
        ConfigModifier_Movie.MainBanner = _setup_Movie.chkScrapeBanner.Checked
        ConfigModifier_Movie.MainClearArt = _setup_Movie.chkScrapeClearArt.Checked
        ConfigModifier_Movie.MainClearLogo = _setup_Movie.chkScrapeClearLogo.Checked
        ConfigModifier_Movie.MainDiscArt = _setup_Movie.chkScrapeDiscArt.Checked
        ConfigModifier_Movie.MainLandscape = _setup_Movie.chkScrapeLandscape.Checked
        SaveSettings_Movie()
        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            RemoveHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_MovieSet(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_MovieSet.SaveSetupScraper
        _SpecialSettings_MovieSet.ClearArtOnlyHD = _setup_MovieSet.chkScrapeClearArtOnlyHD.Checked
        _SpecialSettings_MovieSet.ClearLogoOnlyHD = _setup_MovieSet.chkScrapeClearLogoOnlyHD.Checked
        ConfigModifier_MovieSet.MainPoster = _setup_MovieSet.chkScrapePoster.Checked
        ConfigModifier_MovieSet.MainFanart = _setup_MovieSet.chkScrapeFanart.Checked
        ConfigModifier_MovieSet.MainBanner = _setup_MovieSet.chkScrapeBanner.Checked
        ConfigModifier_MovieSet.MainClearArt = _setup_MovieSet.chkScrapeClearArt.Checked
        ConfigModifier_MovieSet.MainClearLogo = _setup_MovieSet.chkScrapeClearLogo.Checked
        ConfigModifier_MovieSet.MainDiscArt = _setup_MovieSet.chkScrapeDiscArt.Checked
        ConfigModifier_MovieSet.MainLandscape = _setup_MovieSet.chkScrapeLandscape.Checked
        SaveSettings_MovieSet()
        If DoDispose Then
            RemoveHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
            RemoveHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
            RemoveHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
            _setup_MovieSet.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_TV(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_TV.SaveSetupScraper
        _SpecialSettings_TV.ClearArtOnlyHD = _setup_TV.chkScrapeShowClearArtOnlyHD.Checked
        _SpecialSettings_TV.ClearLogoOnlyHD = _setup_TV.chkScrapeShowClearLogoOnlyHD.Checked
        ConfigModifier_TV.SeasonBanner = _setup_TV.chkScrapeSeasonBanner.Checked
        ConfigModifier_TV.SeasonLandscape = _setup_TV.chkScrapeSeasonLandscape.Checked
        ConfigModifier_TV.SeasonPoster = _setup_TV.chkScrapeSeasonPoster.Checked
        ConfigModifier_TV.MainBanner = _setup_TV.chkScrapeShowBanner.Checked
        ConfigModifier_TV.MainCharacterArt = _setup_TV.chkScrapeShowCharacterArt.Checked
        ConfigModifier_TV.MainClearArt = _setup_TV.chkScrapeShowClearArt.Checked
        ConfigModifier_TV.MainClearLogo = _setup_TV.chkScrapeShowClearLogo.Checked
        ConfigModifier_TV.MainFanart = _setup_TV.chkScrapeShowFanart.Checked
        ConfigModifier_TV.MainLandscape = _setup_TV.chkScrapeShowLandscape.Checked
        ConfigModifier_TV.MainPoster = _setup_TV.chkScrapeShowPoster.Checked
        SaveSettings_TV()
        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            RemoveHandler _setup_TV.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_TV
            _setup_TV.Dispose()
        End If
    End Sub

    Function Scraper_Movie(ByRef DBMovie As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_Movie.Scraper
        logger.Trace("[FanartTV_Image] [Scraper_Movie] [Start]")

        LoadSettings_Movie()
        Dim _scraper As New Scraper(_SpecialSettings_Movie)

        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, ConfigModifier_Movie)

        If Not String.IsNullOrEmpty(DBMovie.Movie.ID) Then
            ImagesContainer = _scraper.GetImages_Movie_MovieSet(DBMovie.Movie.ID, FilteredModifiers)
        ElseIf Not String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            ImagesContainer = _scraper.GetImages_Movie_MovieSet(DBMovie.Movie.TMDBID, FilteredModifiers)
        Else
            logger.Trace(String.Concat("[FanartTV_Image] [Scraper_Movie] [Abort] No IMDB and TMDB ID exist to search: ", DBMovie.ListTitle))
        End If

        logger.Trace("[FanartTV_Image] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper_MovieSet(ByRef DBMovieset As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_MovieSet.Scraper
        logger.Trace("[FanartTV_Image] [Scraper_MovieSet] [Start]")

        If String.IsNullOrEmpty(DBMovieset.MovieSet.TMDB) AndAlso DBMovieset.MoviesInSetSpecified Then
            DBMovieset.MovieSet.TMDB = ModulesManager.Instance.GetMovieCollectionID(DBMovieset.MoviesInSet.Item(0).DBMovie.Movie.ID)
        End If

        If Not String.IsNullOrEmpty(DBMovieset.MovieSet.TMDB) Then
            LoadSettings_MovieSet()
            Dim _scraper As New Scraper(_SpecialSettings_MovieSet)

            Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, ConfigModifier_MovieSet)

            ImagesContainer = _scraper.GetImages_Movie_MovieSet(DBMovieset.MovieSet.TMDB, FilteredModifiers)
        End If

        logger.Trace("[FanartTV_Image] [Scraper_MovieSet] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper_TV(ByRef DBTV As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_TV.Scraper
        logger.Trace("[FanartTV_Image] [Scraper_TV] [Start]")

        LoadSettings_TV()
        Dim _scraper As New Scraper(_SpecialSettings_TV)

        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, ConfigModifier_TV)

        Select Case DBTV.ContentType
            Case Enums.ContentType.TVEpisode
                If Not String.IsNullOrEmpty(DBTV.TVShow.TVDB) Then
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = _scraper.GetImages_TV(DBTV.TVShow.TVDB, FilteredModifiers).MainFanarts
                    End If
                Else
                    logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Enums.ContentType.TVSeason
                If Not String.IsNullOrEmpty(DBTV.TVShow.TVDB) Then
                    ImagesContainer = _scraper.GetImages_TV(DBTV.TVShow.TVDB, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Enums.ContentType.TVShow
                If Not String.IsNullOrEmpty(DBTV.TVShow.TVDB) Then
                    ImagesContainer = _scraper.GetImages_TV(DBTV.TVShow.TVDB, FilteredModifiers)
                Else
                    logger.Trace(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.ListTitle))
                End If
            Case Else
                logger.Error(String.Concat("[FanartTV_Image] [Scraper_TV] [Abort] Unhandled ContentType"))
        End Select

        logger.Trace("[FanartTV_Image] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements EmberAPI.Interfaces.ScraperModule_Image_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_MovieSet() Implements EmberAPI.Interfaces.ScraperModule_Image_MovieSet.ScraperOrderChanged
        _setup_MovieSet.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_TV() Implements EmberAPI.Interfaces.ScraperModule_Image_TV.ScraperOrderChanged
        _setup_TV.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure SpecialSettings

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