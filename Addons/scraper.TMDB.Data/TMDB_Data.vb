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

Public Class TMDB_Data
    Implements Interfaces.ScraperModule_Data_Movie
    Implements Interfaces.ScraperModule_Data_MovieSet
    Implements Interfaces.ScraperModule_Data_TV


#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared _AssemblyName As String
    Public Shared ConfigOptions_Movie As New Structures.ScrapeOptions_Movie
    Public Shared ConfigOptions_MovieSet As New Structures.ScrapeOptions_MovieSet
    Public Shared ConfigOptions_TV As New Structures.ScrapeOptions_TV
    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifier
    Public Shared ConfigScrapeModifier_MovieSet As New Structures.ScrapeModifier
    Public Shared ConfigScrapeModifier_TV As New Structures.ScrapeModifier

    Private strPrivateAPIKey As String = String.Empty
    Private _SpecialSettings_Movie As New SpecialSettings
    Private _SpecialSettings_MovieSet As New SpecialSettings
    Private _SpecialSettings_TV As New SpecialSettings
    Private _Name As String = "TMDB_Data"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_MovieSet As frmSettingsHolder_MovieSet
    Private _setup_TV As frmSettingsHolder_TV

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Data_Movie.ModuleSettingsChanged
    Public Event ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_Movie.ScraperEvent
    Public Event ScraperSetupChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_Movie.ScraperSetupChanged
    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Data_Movie.SetupNeedsRestart

    'MovieSet part
    Public Event ModuleSettingsChanged_MovieSet() Implements Interfaces.ScraperModule_Data_MovieSet.ModuleSettingsChanged
    Public Event ScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_MovieSet.ScraperEvent
    Public Event ScraperSetupChanged_MovieSet(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_MovieSet.ScraperSetupChanged
    Public Event SetupNeedsRestart_MovieSet() Implements Interfaces.ScraperModule_Data_MovieSet.SetupNeedsRestart

    'TV part
    Public Event ModuleSettingsChanged_TV() Implements Interfaces.ScraperModule_Data_TV.ModuleSettingsChanged
    Public Event ScraperEvent_TV(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_TV.ScraperEvent
    Public Event ScraperSetupChanged_TV(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_TV.ScraperSetupChanged
    Public Event SetupNeedsRestart_TV() Implements Interfaces.ScraperModule_Data_TV.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleName, Interfaces.ScraperModule_Data_MovieSet.ModuleName, Interfaces.ScraperModule_Data_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleVersion, Interfaces.ScraperModule_Data_MovieSet.ModuleVersion, Interfaces.ScraperModule_Data_TV.ModuleVersion
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.ScraperModule_Data_Movie.ScraperEnabled
        Get
            Return _ScraperEnabled_Movie
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_Movie = value
        End Set
    End Property

    Property ScraperEnabled_MovieSet() As Boolean Implements Interfaces.ScraperModule_Data_MovieSet.ScraperEnabled
        Get
            Return _ScraperEnabled_MovieSet
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_MovieSet = value
        End Set
    End Property

    Property ScraperEnabled_TV() As Boolean Implements Interfaces.ScraperModule_Data_TV.ScraperEnabled
        Get
            Return _ScraperEnabled_TV
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_TV = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

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
        RaiseEvent ScraperSetupChanged_Movie(String.Concat(Me._Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_MovieSet(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_MovieSet = state
        RaiseEvent ScraperSetupChanged_MovieSet(String.Concat(Me._Name, "_MovieSet"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_TV(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_TV = state
        RaiseEvent ScraperSetupChanged_TV(String.Concat(Me._Name, "_TV"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
    End Sub

    Sub Init_MovieSet(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_MovieSet.Init
        _AssemblyName = sAssemblyName
        LoadSettings_MovieSet()
    End Sub

    Sub Init_TV(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings_TV()
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_Movie.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_Movie = New frmSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.chkEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.cbPrefLanguage.Text = _SpecialSettings_Movie.PrefLanguage
        _setup_Movie.chkCast.Checked = ConfigOptions_Movie.bCast
        _setup_Movie.chkCollectionID.Checked = ConfigOptions_Movie.bCollectionID
        _setup_Movie.chkCountry.Checked = ConfigOptions_Movie.bCountry
        _setup_Movie.chkDirector.Checked = ConfigOptions_Movie.bDirector
        _setup_Movie.chkFallBackEng.Checked = _SpecialSettings_Movie.FallBackEng
        _setup_Movie.chkGenre.Checked = ConfigOptions_Movie.bGenre
        _setup_Movie.chkGetAdultItems.Checked = _SpecialSettings_Movie.GetAdultItems
        _setup_Movie.chkCertification.Checked = ConfigOptions_Movie.bMPAA
        _setup_Movie.chkOriginalTitle.Checked = ConfigOptions_Movie.bOriginalTitle
        _setup_Movie.chkPlot.Checked = ConfigOptions_Movie.bPlot
        _setup_Movie.chkRating.Checked = ConfigOptions_Movie.bRating
        _setup_Movie.chkRelease.Checked = ConfigOptions_Movie.bRelease
        _setup_Movie.chkRuntime.Checked = ConfigOptions_Movie.bRuntime
        _setup_Movie.chkStudio.Checked = ConfigOptions_Movie.bStudio
        _setup_Movie.chkTagline.Checked = ConfigOptions_Movie.bTagline
        _setup_Movie.chkTitle.Checked = ConfigOptions_Movie.bTitle
        _setup_Movie.chkTrailer.Checked = ConfigOptions_Movie.bTrailer
        _setup_Movie.chkWriters.Checked = ConfigOptions_Movie.bWriters
        _setup_Movie.chkYear.Checked = ConfigOptions_Movie.bYear
        _setup_Movie.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_Movie.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_Movie.lblEMMAPI.Visible = False
            _setup_Movie.txtApiKey.Enabled = True
        End If

        _setup_Movie.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "_Movie")
        SPanel.Text = "TMDB"
        SPanel.Prefix = "TMDBMovieInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieData"
        SPanel.Type = Master.eLang.GetString(36, "Movies")
        SPanel.ImageIndex = If(_ScraperEnabled_Movie, 9, 10)
        SPanel.Panel = _setup_Movie.pnlSettings

        AddHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
        AddHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
        AddHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
        Return SPanel
    End Function

    Function InjectSetupScraper_MovieSet() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_MovieSet.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_MovieSet = New frmSettingsHolder_MovieSet
        LoadSettings_MovieSet()
        _setup_MovieSet.chkEnabled.Checked = _ScraperEnabled_MovieSet
        _setup_MovieSet.cbPrefLanguage.Text = _SpecialSettings_MovieSet.PrefLanguage
        _setup_MovieSet.chkFallBackEng.Checked = _SpecialSettings_MovieSet.FallBackEng
        _setup_MovieSet.chkGetAdultItems.Checked = _SpecialSettings_MovieSet.GetAdultItems
        _setup_MovieSet.chkPlot.Checked = ConfigOptions_MovieSet.bPlot
        _setup_MovieSet.chkTitle.Checked = ConfigOptions_MovieSet.bTitle
        _setup_MovieSet.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_MovieSet.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_MovieSet.lblEMMAPI.Visible = False
            _setup_MovieSet.txtApiKey.Enabled = True
        End If

        _setup_MovieSet.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "_MovieSet")
        SPanel.Text = "TMDB"
        SPanel.Prefix = "TMDBMovieSetInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieSetData"
        SPanel.Type = Master.eLang.GetString(1203, "MovieSets")
        SPanel.ImageIndex = If(_ScraperEnabled_MovieSet, 9, 10)
        SPanel.Panel = _setup_MovieSet.pnlSettings

        AddHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
        AddHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
        AddHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
        Return SPanel
    End Function

    Function InjectSetupScraper_TV() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_TV.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_TV = New frmSettingsHolder_TV
        LoadSettings_TV()
        _setup_TV.chkEnabled.Checked = _ScraperEnabled_TV
        _setup_TV.chkFallBackEng.Checked = _SpecialSettings_TV.FallBackEng
        _setup_TV.chkGetAdultItems.Checked = _SpecialSettings_TV.GetAdultItems
        _setup_TV.chkScraperEpisodeActors.Checked = ConfigOptions_TV.bEpisodeActors
        _setup_TV.chkScraperEpisodeAired.Checked = ConfigOptions_TV.bEpisodeAired
        _setup_TV.chkScraperEpisodeCredits.Checked = ConfigOptions_TV.bEpisodeCredits
        _setup_TV.chkScraperEpisodeDirector.Checked = ConfigOptions_TV.bEpisodeDirector
        _setup_TV.chkScraperEpisodeGuestStars.Checked = ConfigOptions_TV.bEpisodeGuestStars
        _setup_TV.chkScraperEpisodePlot.Checked = ConfigOptions_TV.bEpisodePlot
        _setup_TV.chkScraperEpisodeRating.Checked = ConfigOptions_TV.bEpisodeRating
        _setup_TV.chkScraperEpisodeTitle.Checked = ConfigOptions_TV.bEpisodeTitle
        _setup_TV.chkScraperShowActors.Checked = ConfigOptions_TV.bShowActors
        _setup_TV.chkScraperShowCert.Checked = ConfigOptions_TV.bShowCert
        _setup_TV.chkScraperShowCountry.Checked = ConfigOptions_TV.bShowCountry
        _setup_TV.chkScraperShowCreator.Checked = ConfigOptions_TV.bShowCreator
        _setup_TV.chkScraperShowGenre.Checked = ConfigOptions_TV.bShowGenre
        _setup_TV.chkScraperShowOriginalTitle.Checked = ConfigOptions_TV.bShowOriginalTitle
        _setup_TV.chkScraperShowPlot.Checked = ConfigOptions_TV.bShowPlot
        _setup_TV.chkScraperShowPremiered.Checked = ConfigOptions_TV.bShowPremiered
        _setup_TV.chkScraperShowRating.Checked = ConfigOptions_TV.bShowRating
        _setup_TV.chkScraperShowRuntime.Checked = ConfigOptions_TV.bShowRuntime
        _setup_TV.chkScraperShowStatus.Checked = ConfigOptions_TV.bShowStatus
        _setup_TV.chkScraperShowStudio.Checked = ConfigOptions_TV.bShowStudio
        _setup_TV.chkScraperShowTitle.Checked = ConfigOptions_TV.bShowTitle
        _setup_TV.txtApiKey.Text = strPrivateAPIKey

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup_TV.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup_TV.lblEMMAPI.Visible = False
            _setup_TV.txtApiKey.Enabled = True
        End If

        _setup_TV.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "_TV")
        SPanel.Text = "TMDB"
        SPanel.Prefix = "TMDBTVInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlTVData"
        SPanel.Type = Master.eLang.GetString(653, "TV Shows")
        SPanel.ImageIndex = If(_ScraperEnabled_TV, 9, 10)
        SPanel.Panel = _setup_TV.pnlSettings

        AddHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
        AddHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
        AddHandler _setup_TV.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_TV
        Return SPanel
    End Function

    Sub LoadSettings_Movie()
        ConfigOptions_Movie.bCast = clsAdvancedSettings.GetBooleanSetting("DoCast", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bCollectionID = clsAdvancedSettings.GetBooleanSetting("DoCollectionID", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bCountry = clsAdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bFullCrew = clsAdvancedSettings.GetBooleanSetting("DoFullCrews", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bFullCrew = clsAdvancedSettings.GetBooleanSetting("FullCrew", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bGenre = clsAdvancedSettings.GetBooleanSetting("DoGenres", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bMPAA = clsAdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bOriginalTitle = clsAdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bMusicBy = clsAdvancedSettings.GetBooleanSetting("DoMusic", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bOtherCrew = clsAdvancedSettings.GetBooleanSetting("DoOtherCrews", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bOutline = clsAdvancedSettings.GetBooleanSetting("DoOutline", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bProducers = clsAdvancedSettings.GetBooleanSetting("DoProducers", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bRelease = clsAdvancedSettings.GetBooleanSetting("DoRelease", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bTagline = clsAdvancedSettings.GetBooleanSetting("DoTagline", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bTop250 = clsAdvancedSettings.GetBooleanSetting("DoTop250", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bTrailer = clsAdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bWriters = clsAdvancedSettings.GetBooleanSetting("DoWriters", True, , Enums.ContentType.Movie)
        ConfigOptions_Movie.bYear = clsAdvancedSettings.GetBooleanSetting("DoYear", True, , Enums.ContentType.Movie)

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.FallBackEng = clsAdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.GetAdultItems = clsAdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
        _SpecialSettings_Movie.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.ContentType.Movie)
    End Sub

    Sub LoadSettings_MovieSet()
        ConfigOptions_MovieSet.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.MovieSet)
        ConfigOptions_MovieSet.bTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.MovieSet)

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.FallBackEng = clsAdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.GetAdultItems = clsAdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.MovieSet)
        _SpecialSettings_MovieSet.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
        _SpecialSettings_MovieSet.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.ContentType.MovieSet)
    End Sub

    Sub LoadSettings_TV()
        ConfigOptions_TV.bEpisodeActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpisodeAired = clsAdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpisodeCredits = clsAdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpisodeDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpisodeGuestStars = clsAdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpisodePlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpisodeRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpisodeTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bShowActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowCountry = clsAdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowCreator = clsAdvancedSettings.GetBooleanSetting("DoCreator", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowEpisodeGuide = clsAdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowGenre = clsAdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowOriginalTitle = clsAdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowPremiered = clsAdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowStatus = clsAdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.TV)
        _SpecialSettings_TV.FallBackEng = clsAdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.TV)
        _SpecialSettings_TV.GetAdultItems = clsAdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.TV)
        _SpecialSettings_TV.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoCast", ConfigOptions_Movie.bCast, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCert", ConfigOptions_Movie.bCert, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCollectionID", ConfigOptions_Movie.bCollectionID, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCountry", ConfigOptions_Movie.bCountry, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoDirector", ConfigOptions_Movie.bDirector, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier_Movie.MainFanart, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFullCrews", ConfigOptions_Movie.bFullCrew, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoGenres", ConfigOptions_Movie.bGenre, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoMPAA", ConfigOptions_Movie.bMPAA, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOriginalTitle", ConfigOptions_Movie.bOriginalTitle, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoMusic", ConfigOptions_Movie.bMusicBy, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOtherCrews", ConfigOptions_Movie.bOtherCrew, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOutline", ConfigOptions_Movie.bOutline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_Movie.bPlot, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier_Movie.MainPoster, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoProducers", ConfigOptions_Movie.bProducers, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRating", ConfigOptions_Movie.bRating, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRelease", ConfigOptions_Movie.bRelease, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRuntime", ConfigOptions_Movie.bRuntime, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoStudio", ConfigOptions_Movie.bStudio, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTagline", ConfigOptions_Movie.bTagline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_Movie.bTitle, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTop250", ConfigOptions_Movie.bTop250, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTrailer", ConfigOptions_Movie.bTrailer, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoWriters", ConfigOptions_Movie.bWriters, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoYear", ConfigOptions_Movie.bYear, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("FallBackEn", _SpecialSettings_Movie.FallBackEng, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("FullCrew", ConfigOptions_Movie.bFullCrew, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("GetAdultItems", _SpecialSettings_Movie.GetAdultItems, , , Enums.ContentType.Movie)
            settings.SetSetting("APIKey", _setup_Movie.txtApiKey.Text, , , Enums.ContentType.Movie)
            settings.SetSetting("PrefLanguage", _SpecialSettings_Movie.PrefLanguage, , , Enums.ContentType.Movie)
        End Using
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoPlot", ConfigOptions_MovieSet.bPlot, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_MovieSet.bTitle, , , Enums.ContentType.MovieSet)
            settings.SetBooleanSetting("GetAdultItems", _SpecialSettings_MovieSet.GetAdultItems, , , Enums.ContentType.MovieSet)
            settings.SetSetting("APIKey", _setup_MovieSet.txtApiKey.Text, , , Enums.ContentType.MovieSet)
            settings.SetSetting("PrefLanguage", _SpecialSettings_MovieSet.PrefLanguage, , , Enums.ContentType.MovieSet)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoActors", ConfigOptions_TV.bEpisodeActors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", ConfigOptions_TV.bEpisodeAired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", ConfigOptions_TV.bEpisodeCredits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", ConfigOptions_TV.bEpisodeDirector, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", ConfigOptions_TV.bEpisodeGuestStars, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_TV.bEpisodePlot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", ConfigOptions_TV.bEpisodeRating, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_TV.bEpisodeTitle, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoActors", ConfigOptions_TV.bShowActors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCert", ConfigOptions_TV.bShowCert, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCountry", ConfigOptions_TV.bShowCountry, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCreator", ConfigOptions_TV.bShowCreator, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoEpisodeGuide", ConfigOptions_TV.bShowEpisodeGuide, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", ConfigOptions_TV.bShowGenre, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoOriginalTitle", ConfigOptions_TV.bShowOriginalTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_TV.bShowPlot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", ConfigOptions_TV.bShowPremiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", ConfigOptions_TV.bShowRating, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStatus", ConfigOptions_TV.bShowStatus, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", ConfigOptions_TV.bShowStudio, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_TV.bShowTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("FallBackEn", _SpecialSettings_TV.FallBackEng, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("GetAdultItems", _SpecialSettings_TV.GetAdultItems, , , Enums.ContentType.TV)
            settings.SetSetting("APIKey", _setup_TV.txtApiKey.Text, , , Enums.ContentType.TV)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        ConfigOptions_Movie.bCast = _setup_Movie.chkCast.Checked
        ConfigOptions_Movie.bCert = _setup_Movie.chkCertification.Checked
        ConfigOptions_Movie.bCollectionID = _setup_Movie.chkCollectionID.Checked
        ConfigOptions_Movie.bCountry = _setup_Movie.chkCountry.Checked
        ConfigOptions_Movie.bDirector = _setup_Movie.chkDirector.Checked
        ConfigOptions_Movie.bFullCrew = True
        ConfigOptions_Movie.bGenre = _setup_Movie.chkGenre.Checked
        ConfigOptions_Movie.bMPAA = _setup_Movie.chkCertification.Checked
        ConfigOptions_Movie.bOriginalTitle = _setup_Movie.chkOriginalTitle.Checked
        ConfigOptions_Movie.bMusicBy = False
        ConfigOptions_Movie.bOtherCrew = False
        ConfigOptions_Movie.bOutline = _setup_Movie.chkPlot.Checked
        ConfigOptions_Movie.bPlot = _setup_Movie.chkPlot.Checked
        ConfigOptions_Movie.bProducers = _setup_Movie.chkDirector.Checked
        ConfigOptions_Movie.bRating = _setup_Movie.chkRating.Checked
        ConfigOptions_Movie.bRelease = _setup_Movie.chkRelease.Checked
        ConfigOptions_Movie.bRuntime = _setup_Movie.chkRuntime.Checked
        ConfigOptions_Movie.bStudio = _setup_Movie.chkStudio.Checked
        ConfigOptions_Movie.bTagline = _setup_Movie.chkTagline.Checked
        ConfigOptions_Movie.bTitle = _setup_Movie.chkTitle.Checked
        ConfigOptions_Movie.bTop250 = False
        ConfigOptions_Movie.bTrailer = _setup_Movie.chkTrailer.Checked
        ConfigOptions_Movie.bWriters = _setup_Movie.chkWriters.Checked
        ConfigOptions_Movie.bYear = _setup_Movie.chkYear.Checked
        _SpecialSettings_Movie.FallBackEng = _setup_Movie.chkFallBackEng.Checked
        _SpecialSettings_Movie.GetAdultItems = _setup_Movie.chkGetAdultItems.Checked
        _SpecialSettings_Movie.PrefLanguage = _setup_Movie.cbPrefLanguage.Text
        SaveSettings_Movie()
        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_MovieSet(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_MovieSet.SaveSetupScraper
        ConfigOptions_MovieSet.bPlot = _setup_MovieSet.chkPlot.Checked
        ConfigOptions_MovieSet.bTitle = _setup_MovieSet.chkTitle.Checked
        _SpecialSettings_MovieSet.FallBackEng = _setup_MovieSet.chkFallBackEng.Checked
        _SpecialSettings_MovieSet.GetAdultItems = _setup_MovieSet.chkGetAdultItems.Checked
        _SpecialSettings_MovieSet.PrefLanguage = _setup_MovieSet.cbPrefLanguage.Text
        SaveSettings_MovieSet()
        If DoDispose Then
            RemoveHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movieset
            RemoveHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
            _setup_MovieSet.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_TV(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_TV.SaveSetupScraper
        ConfigOptions_TV.bEpisodeActors = _setup_TV.chkScraperEpisodeActors.Checked
        ConfigOptions_TV.bEpisodeAired = _setup_TV.chkScraperEpisodeAired.Checked
        ConfigOptions_TV.bEpisodeCredits = _setup_TV.chkScraperEpisodeCredits.Checked
        ConfigOptions_TV.bEpisodeDirector = _setup_TV.chkScraperEpisodeDirector.Checked
        ConfigOptions_TV.bEpisodeGuestStars = _setup_TV.chkScraperEpisodeGuestStars.Checked
        ConfigOptions_TV.bEpisodePlot = _setup_TV.chkScraperEpisodePlot.Checked
        ConfigOptions_TV.bEpisodeRating = _setup_TV.chkScraperEpisodeRating.Checked
        ConfigOptions_TV.bEpisodeTitle = _setup_TV.chkScraperEpisodeTitle.Checked
        ConfigOptions_TV.bShowActors = _setup_TV.chkScraperShowActors.Checked
        ConfigOptions_TV.bShowCert = _setup_TV.chkScraperShowCert.Checked
        ConfigOptions_TV.bShowCreator = _setup_TV.chkScraperShowCreator.Checked
        ConfigOptions_TV.bShowGenre = _setup_TV.chkScraperShowGenre.Checked
        ConfigOptions_TV.bShowOriginalTitle = _setup_TV.chkScraperShowOriginalTitle.Checked
        ConfigOptions_TV.bShowPlot = _setup_TV.chkScraperShowPlot.Checked
        ConfigOptions_TV.bShowPremiered = _setup_TV.chkScraperShowPremiered.Checked
        ConfigOptions_TV.bShowRating = _setup_TV.chkScraperShowRating.Checked
        ConfigOptions_TV.bShowRuntime = _setup_TV.chkScraperShowRuntime.Checked
        ConfigOptions_TV.bShowStatus = _setup_TV.chkScraperShowStatus.Checked
        ConfigOptions_TV.bShowStudio = _setup_TV.chkScraperShowStudio.Checked
        ConfigOptions_TV.bShowTitle = _setup_TV.chkScraperShowTitle.Checked
        _SpecialSettings_TV.FallBackEng = _setup_TV.chkFallBackEng.Checked
        _SpecialSettings_TV.GetAdultItems = _setup_TV.chkGetAdultItems.Checked
        SaveSettings_TV()
        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            _setup_TV.Dispose()
        End If
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef sStudio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        If (DBMovie.Movie Is Nothing OrElse (String.IsNullOrEmpty(DBMovie.Movie.IMDBID) AndAlso String.IsNullOrEmpty(DBMovie.Movie.TMDBID))) Then
            logger.Error("Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If

        LoadSettings_Movie()

        Dim _scraper As New TMDB.Scraper(_SpecialSettings_Movie)

        If Not String.IsNullOrEmpty(DBMovie.Movie.ID) Then
            'IMDB-ID is available
            sStudio.AddRange(_scraper.GetMovieStudios(DBMovie.Movie.ID))
        ElseIf Not String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            'TMDB-ID is available
            sStudio.AddRange(_scraper.GetMovieStudios(DBMovie.Movie.TMDBID))
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDBID
        If Not String.IsNullOrEmpty(sIMDBID) Then

            LoadSettings_Movie()

            Dim _scraper As New TMDB.Scraper(_SpecialSettings_Movie)

            sTMDBID = _scraper.GetMovieID(sIMDBID)
        End If
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetCollectionID(ByVal sIMDBID As String, ByRef sCollectionID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_MovieSet.GetCollectionID

        LoadSettings_MovieSet()

        Dim _scraper As New TMDB.Scraper(_SpecialSettings_MovieSet)

        sCollectionID = _scraper.GetMovieCollectionID(sIMDBID)

        If String.IsNullOrEmpty(sCollectionID) Then
            Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
        End If
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_Movie(ByRef oDBElement As Database.DBElement, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.Scraper
        logger.Trace("Started TMDB Scraper")

        LoadSettings_Movie()

        Dim _scraper As New TMDB.Scraper(_SpecialSettings_Movie)

        Dim FilteredOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions_Movie)

        If ScrapeModifier.MainNFO AndAlso Not ScrapeModifier.DoSearch Then
            If Not String.IsNullOrEmpty(oDBElement.Movie.ID) Then
                'IMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                _scraper.GetMovieInfo(oDBElement.Movie.ID, nMovie, FilteredOptions.bFullCrew, False, FilteredOptions, False)
            ElseIf Not String.IsNullOrEmpty(oDBElement.Movie.TMDBID) Then
                'TMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                _scraper.GetMovieInfo(oDBElement.Movie.TMDBID, nMovie, FilteredOptions.bFullCrew, False, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID or TMDB-ID for movie --> search first and try to get ID!
                If Not String.IsNullOrEmpty(oDBElement.Movie.Title) Then
                    _scraper.GetSearchMovieInfo(oDBElement.Movie.Title, oDBElement, nMovie, ScrapeType, FilteredOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nMovie.TMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nMovie.TMDBID) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    nMovie = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If String.IsNullOrEmpty(oDBElement.Movie.ID) AndAlso String.IsNullOrEmpty(oDBElement.Movie.TMDBID) Then
                Using dSearch As New dlgTMDBSearchResults_Movie(_SpecialSettings_Movie, _scraper)
                    If dSearch.ShowDialog(nMovie, oDBElement.Movie.Title, oDBElement.Filename, FilteredOptions, oDBElement.Movie.Year) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetMovieInfo(nMovie.TMDBID, nMovie, FilteredOptions.bFullCrew, False, FilteredOptions, False)
                        'if a movie is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifier.DoSearch = False
                    Else
                        nMovie = Nothing
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        'set new informations for following scrapers
        If Not String.IsNullOrEmpty(nMovie.Title) Then
            oDBElement.Movie.Title = nMovie.Title
        End If
        If Not String.IsNullOrEmpty(nMovie.OriginalTitle) Then
            oDBElement.Movie.OriginalTitle = nMovie.OriginalTitle
        End If
        If Not String.IsNullOrEmpty(nMovie.Year) Then
            oDBElement.Movie.Year = nMovie.Year
        End If
        If Not String.IsNullOrEmpty(nMovie.ID) Then
            oDBElement.Movie.ID = nMovie.ID
        End If
        If Not String.IsNullOrEmpty(nMovie.IMDBID) Then
            oDBElement.Movie.IMDBID = nMovie.IMDBID
        End If
        If Not String.IsNullOrEmpty(nMovie.TMDBID) Then
            oDBElement.Movie.TMDBID = nMovie.TMDBID
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper_MovieSet(ByRef oDBElement As Database.DBElement, ByRef nMovieSet As MediaContainers.MovieSet, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_MovieSet) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_MovieSet.Scraper
        logger.Trace("Started scrape TMDB")

        LoadSettings_MovieSet()

        Dim _scraper As New TMDB.Scraper(_SpecialSettings_MovieSet)

        Dim FilteredOptions As Structures.ScrapeOptions_MovieSet = Functions.MovieSetScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions_MovieSet)

        If ScrapeModifier.MainNFO AndAlso Not ScrapeModifier.DoSearch Then
            If Not String.IsNullOrEmpty(oDBElement.MovieSet.TMDB) Then
                'TMDB-ID already available -> scrape and save data into an empty movieset container (nMovieSet)
                _scraper.GetMovieSetInfo(oDBElement.MovieSet.TMDB, nMovieSet, False, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no ITMDB-ID for movieset --> search first and try to get ID!
                If Not String.IsNullOrEmpty(oDBElement.MovieSet.Title) Then
                    _scraper.GetSearchMovieSetInfo(oDBElement.MovieSet.Title, oDBElement, nMovieSet, ScrapeType, FilteredOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nMovieSet.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nMovieSet.TMDB) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    nMovieSet = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If String.IsNullOrEmpty(oDBElement.MovieSet.TMDB) Then
                Using dSearch As New dlgTMDBSearchResults_MovieSet(_SpecialSettings_MovieSet, _scraper)
                    If dSearch.ShowDialog(nMovieSet, oDBElement.MovieSet.Title, FilteredOptions) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetMovieSetInfo(nMovieSet.TMDB, nMovieSet, False, FilteredOptions, False)
                        'if a movie is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifier.DoSearch = False
                    Else
                        nMovieSet = Nothing
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        'set new informations for following scrapers
        If Not String.IsNullOrEmpty(nMovieSet.Title) Then
            oDBElement.MovieSet.Title = nMovieSet.Title
        End If
        If Not String.IsNullOrEmpty(nMovieSet.TMDB) Then
            oDBElement.MovieSet.TMDB = nMovieSet.TMDB
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="oDBTV">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="nShow">New scraped TV Show data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_TV(ByRef oDBElement As Database.DBElement, ByRef nShow As MediaContainers.TVShow, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVShow
        logger.Trace("Started TMDB Scraper")

        LoadSettings_TV()
        _SpecialSettings_TV.PrefLanguage = oDBElement.Language

        Dim _scraper As New TMDB.Scraper(_SpecialSettings_TV)

        Dim FilteredOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions_TV)

        If ScrapeModifier.MainNFO AndAlso Not ScrapeModifier.DoSearch Then
            If Not String.IsNullOrEmpty(oDBElement.TVShow.TMDB) Then
                'TMDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                _scraper.GetTVShowInfo(oDBElement.TVShow.TMDB, nShow, False, FilteredOptions, False, ScrapeModifier.withEpisodes)
            ElseIf Not String.IsNullOrEmpty(oDBElement.TVShow.TVDB) Then
                oDBElement.TVShow.TMDB = _scraper.GetTMDBbyTVDB(oDBElement.TVShow.TVDB)
                If String.IsNullOrEmpty(oDBElement.TVShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                _scraper.GetTVShowInfo(oDBElement.TVShow.TMDB, nShow, False, FilteredOptions, False, ScrapeModifier.withEpisodes)
            ElseIf Not String.IsNullOrEmpty(oDBElement.TVShow.IMDB) Then
                oDBElement.TVShow.TMDB = _scraper.GetTMDBbyIMDB(oDBElement.TVShow.IMDB)
                If String.IsNullOrEmpty(oDBElement.TVShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                _scraper.GetTVShowInfo(oDBElement.TVShow.TMDB, nShow, False, FilteredOptions, False, ScrapeModifier.withEpisodes)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If Not String.IsNullOrEmpty(oDBElement.TVShow.Title) Then
                    _scraper.GetSearchTVShowInfo(oDBElement.TVShow.Title, oDBElement, nShow, ScrapeType, FilteredOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nShow.TMDB) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    nShow = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If String.IsNullOrEmpty(oDBElement.TVShow.TMDB) Then
                Using dSearch As New dlgTMDBSearchResults_TV(_SpecialSettings_TV, _scraper)
                    If dSearch.ShowDialog(nShow, oDBElement.TVShow.Title, oDBElement.ShowPath, FilteredOptions) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetTVShowInfo(nShow.TMDB, nShow, False, FilteredOptions, False, ScrapeModifier.withEpisodes)
                        'if a tvshow is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifier.DoSearch = False
                    Else
                        nShow = Nothing
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        'set new informations for following scrapers
        If Not String.IsNullOrEmpty(nShow.Title) Then
            oDBElement.TVShow.Title = nShow.Title
        End If
        If Not String.IsNullOrEmpty(nShow.TVDB) Then
            oDBElement.TVShow.TVDB = nShow.TVDB
        End If
        If Not String.IsNullOrEmpty(nShow.IMDB) Then
            oDBElement.TVShow.IMDB = nShow.IMDB
        End If
        If Not String.IsNullOrEmpty(nShow.TMDB) Then
            oDBElement.TVShow.TMDB = nShow.TMDB
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function Scraper_TVEpisode(ByRef oDBElement As Database.DBElement, ByRef nEpisode As MediaContainers.EpisodeDetails, ByVal ScrapeOptions As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVEpisode
        logger.Trace("Started TMDB Scraper")

        LoadSettings_TV()
        _SpecialSettings_TV.PrefLanguage = oDBElement.Language

        Dim _scraper As New TMDB.Scraper(_SpecialSettings_TV)

        Dim FilteredOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions_TV)

        If String.IsNullOrEmpty(oDBElement.TVShow.TMDB) AndAlso Not String.IsNullOrEmpty(oDBElement.TVShow.TVDB) Then
            oDBElement.TVShow.TMDB = _scraper.GetTMDBbyTVDB(oDBElement.TVShow.TVDB)
        End If

        If Not String.IsNullOrEmpty(oDBElement.TVShow.TMDB) Then
            If Not oDBElement.TVEpisode.Episode = -1 AndAlso Not oDBElement.TVEpisode.Season = -1 Then
                nEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBElement.TVShow.TMDB), oDBElement.TVEpisode.Season, oDBElement.TVEpisode.Episode, FilteredOptions)
            ElseIf Not String.IsNullOrEmpty(oDBElement.TVEpisode.Aired) Then
                nEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBElement.TVShow.TMDB), oDBElement.TVEpisode.Aired, FilteredOptions)
            Else
                nEpisode = Nothing
            End If
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements EmberAPI.Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_MovieSet() Implements EmberAPI.Interfaces.ScraperModule_Data_MovieSet.ScraperOrderChanged
        _setup_MovieSet.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_TV() Implements EmberAPI.Interfaces.ScraperModule_Data_TV.ScraperOrderChanged
        _setup_TV.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure SpecialSettings

#Region "Fields"

        Dim FallBackEng As Boolean
        Dim GetAdultItems As Boolean
        Dim APIKey As String
        Dim PrefLanguage As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class