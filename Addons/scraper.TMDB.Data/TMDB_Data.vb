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
    Private _MySettings_Movie As New sMySettings
    Private _MySettings_MovieSet As New sMySettings
    Private _MySettings_TV As New sMySettings
    Private _Name As String = "TMDB_Data"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_MovieSet As frmSettingsHolder_MovieSet
    Private _setup_TV As frmSettingsHolder_TV

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Data_Movie.ModuleSettingsChanged
    Public Event ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_Movie.ScraperEvent
    Public Event ScraperSetupChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_Movie.ScraperSetupChanged
    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Data_Movie.SetupNeedsRestart

    Public Event ModuleSettingsChanged_MovieSet() Implements Interfaces.ScraperModule_Data_MovieSet.ModuleSettingsChanged
    Public Event ScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_MovieSet.ScraperEvent
    Public Event ScraperSetupChanged_MovieSet(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_MovieSet.ScraperSetupChanged
    Public Event SetupNeedsRestart_MovieSet() Implements Interfaces.ScraperModule_Data_MovieSet.SetupNeedsRestart

    Public Event ModuleSettingsChanged_TV() Implements Interfaces.ScraperModule_Data_TV.ModuleSettingsChanged
    Public Event ScraperEvent_TV(ByVal eType As Enums.ScraperEventType_TV, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_TV.ScraperEvent
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
        _setup_Movie.API = _setup_Movie.txtApiKey.Text
        _setup_Movie.Lang = _setup_Movie.cbPrefLanguage.Text
        _setup_Movie.chkEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.cbPrefLanguage.Text = _MySettings_Movie.PrefLanguage
        _setup_Movie.chkCast.Checked = ConfigOptions_Movie.bCast
        _setup_Movie.chkCollectionID.Checked = ConfigOptions_Movie.bCollectionID
        _setup_Movie.chkCountry.Checked = ConfigOptions_Movie.bCountry
        _setup_Movie.chkDirector.Checked = ConfigOptions_Movie.bDirector
        _setup_Movie.chkFallBackEng.Checked = _MySettings_Movie.FallBackEng
        _setup_Movie.chkGenre.Checked = ConfigOptions_Movie.bGenre
        _setup_Movie.chkGetAdultItems.Checked = _MySettings_Movie.GetAdultItems
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
        _setup_Movie.chkVotes.Checked = ConfigOptions_Movie.bVotes
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
        _setup_MovieSet.API = _setup_MovieSet.txtApiKey.Text
        _setup_MovieSet.Lang = _setup_MovieSet.cbPrefLanguage.Text
        _setup_MovieSet.chkEnabled.Checked = _ScraperEnabled_MovieSet
        _setup_MovieSet.cbPrefLanguage.Text = _MySettings_MovieSet.PrefLanguage
        _setup_MovieSet.chkFallBackEng.Checked = _MySettings_MovieSet.FallBackEng
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
        _setup_TV.API = _setup_TV.txtApiKey.Text
        _setup_TV.chkEnabled.Checked = _ScraperEnabled_TV
        _setup_TV.chkFallBackEng.Checked = _MySettings_TV.FallBackEng
        _setup_TV.chkGetAdultItems.Checked = _MySettings_TV.GetAdultItems
        _setup_TV.chkScraperEpActors.Checked = ConfigOptions_TV.bEpActors
        _setup_TV.chkScraperEpAired.Checked = ConfigOptions_TV.bEpAired
        _setup_TV.chkScraperEpCredits.Checked = ConfigOptions_TV.bEpCredits
        _setup_TV.chkScraperEpDirector.Checked = ConfigOptions_TV.bEpDirector
        _setup_TV.chkScraperEpGuestStars.Checked = ConfigOptions_TV.bEpGuestStars
        _setup_TV.chkScraperEpPlot.Checked = ConfigOptions_TV.bEpPlot
        _setup_TV.chkScraperEpRating.Checked = ConfigOptions_TV.bEpRating
        _setup_TV.chkScraperEpTitle.Checked = ConfigOptions_TV.bEpTitle
        _setup_TV.chkScraperEpVotes.Checked = ConfigOptions_TV.bEpVotes
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
        _setup_TV.chkScraperShowVotes.Checked = ConfigOptions_TV.bShowVotes
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
        ConfigOptions_Movie.bCast = clsAdvancedSettings.GetBooleanSetting("DoCast", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bCollectionID = clsAdvancedSettings.GetBooleanSetting("DoCollectionID", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bCountry = clsAdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bFullCrew = clsAdvancedSettings.GetBooleanSetting("DoFullCrews", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bFullCrew = clsAdvancedSettings.GetBooleanSetting("FullCrew", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bGenre = clsAdvancedSettings.GetBooleanSetting("DoGenres", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bMPAA = clsAdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bOriginalTitle = clsAdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bMusicBy = clsAdvancedSettings.GetBooleanSetting("DoMusic", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bOtherCrew = clsAdvancedSettings.GetBooleanSetting("DoOtherCrews", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bOutline = clsAdvancedSettings.GetBooleanSetting("DoOutline", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bProducers = clsAdvancedSettings.GetBooleanSetting("DoProducers", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bRelease = clsAdvancedSettings.GetBooleanSetting("DoRelease", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bTagline = clsAdvancedSettings.GetBooleanSetting("DoTagline", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bTop250 = clsAdvancedSettings.GetBooleanSetting("DoTop250", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bTrailer = clsAdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bWriters = clsAdvancedSettings.GetBooleanSetting("DoWriters", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bYear = clsAdvancedSettings.GetBooleanSetting("DoYear", True, , Enums.Content_Type.Movie)

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "", , Enums.Content_Type.Movie)
        _MySettings_Movie.FallBackEng = clsAdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.Content_Type.Movie)
        _MySettings_Movie.GetAdultItems = clsAdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.Content_Type.Movie)
        _MySettings_Movie.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
        _MySettings_Movie.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.DoSearch = True
        ConfigScrapeModifier_Movie.MainMeta = True
        ConfigScrapeModifier_Movie.MainNFO = True
    End Sub

    Sub LoadSettings_MovieSet()
        ConfigOptions_MovieSet.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.Content_Type.MovieSet)
        ConfigOptions_MovieSet.bTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.Content_Type.MovieSet)

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "", , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.FallBackEng = clsAdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.GetAdultItems = clsAdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
        _MySettings_MovieSet.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_Movie.DoSearch = True
        ConfigScrapeModifier_Movie.MainMeta = False
        ConfigScrapeModifier_Movie.MainNFO = True
    End Sub

    Sub LoadSettings_TV()
        ConfigOptions_TV.bEpActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bEpAired = clsAdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bEpCredits = clsAdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bEpDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bEpGuestStars = clsAdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bEpPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bEpRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bEpTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bEpVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions_TV.bShowActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowCountry = clsAdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowCreator = clsAdvancedSettings.GetBooleanSetting("DoCreator", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowEpisodeGuide = clsAdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowGenre = clsAdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowOriginalTitle = clsAdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowPremiered = clsAdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowStatus = clsAdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.Content_Type.TVShow)
        ConfigOptions_TV.bShowVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.Content_Type.TVShow)

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "", , Enums.Content_Type.TV)
        _MySettings_TV.FallBackEng = clsAdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.Content_Type.TV)
        _MySettings_TV.GetAdultItems = clsAdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.Content_Type.TV)
        _MySettings_TV.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
        _MySettings_TV.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.Content_Type.TV)
        ConfigScrapeModifier_TV.DoSearch = True
        ConfigScrapeModifier_TV.MainNFO = True
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoCast", ConfigOptions_Movie.bCast, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoCert", ConfigOptions_Movie.bCert, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoCollectionID", ConfigOptions_Movie.bCollectionID, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoCountry", ConfigOptions_Movie.bCountry, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoDirector", ConfigOptions_Movie.bDirector, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier_Movie.MainFanart, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoFullCrews", ConfigOptions_Movie.bFullCrew, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoGenres", ConfigOptions_Movie.bGenre, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoMPAA", ConfigOptions_Movie.bMPAA, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoOriginalTitle", ConfigOptions_Movie.bOriginalTitle, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoMusic", ConfigOptions_Movie.bMusicBy, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoOtherCrews", ConfigOptions_Movie.bOtherCrew, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoOutline", ConfigOptions_Movie.bOutline, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_Movie.bPlot, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier_Movie.MainPoster, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoProducers", ConfigOptions_Movie.bProducers, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoRating", ConfigOptions_Movie.bRating, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoRelease", ConfigOptions_Movie.bRelease, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoRuntime", ConfigOptions_Movie.bRuntime, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoStudio", ConfigOptions_Movie.bStudio, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoTagline", ConfigOptions_Movie.bTagline, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_Movie.bTitle, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoTop250", ConfigOptions_Movie.bTop250, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoTrailer", ConfigOptions_Movie.bTrailer, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoVotes", ConfigOptions_Movie.bVotes, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoWriters", ConfigOptions_Movie.bWriters, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoYear", ConfigOptions_Movie.bYear, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("FallBackEn", _MySettings_Movie.FallBackEng, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("FullCrew", ConfigOptions_Movie.bFullCrew, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("GetAdultItems", _MySettings_Movie.GetAdultItems, , , Enums.Content_Type.Movie)
            settings.SetSetting("APIKey", _setup_Movie.txtApiKey.Text, , , Enums.Content_Type.Movie)
            settings.SetSetting("PrefLanguage", _MySettings_Movie.PrefLanguage, , , Enums.Content_Type.Movie)
        End Using
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoPlot", ConfigOptions_MovieSet.bPlot, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_MovieSet.bTitle, , , Enums.Content_Type.MovieSet)
            settings.SetSetting("APIKey", _setup_MovieSet.txtApiKey.Text, , , Enums.Content_Type.MovieSet)
            settings.SetSetting("PrefLanguage", _MySettings_MovieSet.PrefLanguage, , , Enums.Content_Type.MovieSet)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoActors", ConfigOptions_TV.bEpActors, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoAired", ConfigOptions_TV.bEpAired, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoCredits", ConfigOptions_TV.bEpCredits, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoDirector", ConfigOptions_TV.bEpDirector, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", ConfigOptions_TV.bEpGuestStars, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_TV.bEpPlot, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoRating", ConfigOptions_TV.bEpRating, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_TV.bEpTitle, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoVotes", ConfigOptions_TV.bEpVotes, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoActors", ConfigOptions_TV.bShowActors, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoCert", ConfigOptions_TV.bShowCert, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoCountry", ConfigOptions_TV.bShowCountry, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoCreator", ConfigOptions_TV.bShowCreator, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoEpisodeGuide", ConfigOptions_TV.bShowEpisodeGuide, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoGenre", ConfigOptions_TV.bShowGenre, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoOriginalTitle", ConfigOptions_TV.bShowOriginalTitle, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_TV.bShowPlot, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoPremiered", ConfigOptions_TV.bShowPremiered, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoRating", ConfigOptions_TV.bShowRating, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoStatus", ConfigOptions_TV.bShowStatus, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoStudio", ConfigOptions_TV.bShowStudio, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_TV.bShowTitle, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoVotes", ConfigOptions_TV.bShowVotes, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("FallBackEn", _MySettings_TV.FallBackEng, , , Enums.Content_Type.TV)
            settings.SetBooleanSetting("GetAdultItems", _MySettings_TV.GetAdultItems, , , Enums.Content_Type.TV)
            settings.SetSetting("APIKey", _setup_TV.txtApiKey.Text, , , Enums.Content_Type.TV)
            settings.SetSetting("PrefLanguage", _MySettings_TV.PrefLanguage, , , Enums.Content_Type.TV)
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
        ConfigOptions_Movie.bVotes = _setup_Movie.chkVotes.Checked
        ConfigOptions_Movie.bWriters = _setup_Movie.chkWriters.Checked
        ConfigOptions_Movie.bYear = _setup_Movie.chkYear.Checked
        _MySettings_Movie.FallBackEng = _setup_Movie.chkFallBackEng.Checked
        _MySettings_Movie.GetAdultItems = _setup_Movie.chkGetAdultItems.Checked
        _MySettings_Movie.PrefLanguage = _setup_Movie.cbPrefLanguage.Text
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
        _MySettings_MovieSet.FallBackEng = _setup_MovieSet.chkFallBackEng.Checked
        _MySettings_MovieSet.GetAdultItems = _setup_MovieSet.chkGetAdultItems.Checked
        _MySettings_MovieSet.PrefLanguage = _setup_MovieSet.cbPrefLanguage.Text
        SaveSettings_MovieSet()
        If DoDispose Then
            RemoveHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movieset
            RemoveHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
            _setup_MovieSet.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_TV(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_TV.SaveSetupScraper
        ConfigOptions_TV.bEpActors = _setup_TV.chkScraperEpActors.Checked
        ConfigOptions_TV.bEpAired = _setup_TV.chkScraperEpAired.Checked
        ConfigOptions_TV.bEpCredits = _setup_TV.chkScraperEpCredits.Checked
        ConfigOptions_TV.bEpDirector = _setup_TV.chkScraperEpDirector.Checked
        ConfigOptions_TV.bEpGuestStars = _setup_TV.chkScraperEpGuestStars.Checked
        ConfigOptions_TV.bEpPlot = _setup_TV.chkScraperEpPlot.Checked
        ConfigOptions_TV.bEpRating = _setup_TV.chkScraperEpRating.Checked
        ConfigOptions_TV.bEpTitle = _setup_TV.chkScraperEpTitle.Checked
        ConfigOptions_TV.bEpVotes = _setup_TV.chkScraperEpVotes.Checked
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
        ConfigOptions_TV.bShowVotes = _setup_TV.chkScraperShowVotes.Checked
        _MySettings_TV.FallBackEng = _setup_TV.chkFallBackEng.Checked
        _MySettings_TV.GetAdultItems = _setup_TV.chkGetAdultItems.Checked
        SaveSettings_TV()
        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            _setup_TV.Dispose()
        End If
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef sStudio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        If (DBMovie.Movie Is Nothing OrElse (String.IsNullOrEmpty(DBMovie.Movie.IMDBID) AndAlso String.IsNullOrEmpty(DBMovie.Movie.TMDBID))) Then
            logger.Error("Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If

        LoadSettings_Movie()
        Dim Settings As TMDB.Scraper.sMySettings_ForScraper
        Settings.ApiKey = _MySettings_Movie.APIKey
        Settings.FallBackEng = _MySettings_Movie.FallBackEng
        Settings.GetAdultItems = _MySettings_Movie.GetAdultItems
        Settings.PrefLanguage = _MySettings_Movie.PrefLanguage

        Dim _scraper As New TMDB.Scraper(Settings)
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

            Dim Settings As TMDB.Scraper.sMySettings_ForScraper
            Settings.ApiKey = _MySettings_Movie.APIKey
            Settings.FallBackEng = _MySettings_Movie.FallBackEng
            Settings.GetAdultItems = _MySettings_Movie.GetAdultItems
            Settings.PrefLanguage = _MySettings_Movie.PrefLanguage

            Dim _scraper As New TMDB.Scraper(Settings)

            sTMDBID = _scraper.GetMovieID(sIMDBID)
        End If
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetCollectionID(ByVal sIMDBID As String, ByRef sCollectionID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_MovieSet.GetCollectionID

        LoadSettings_MovieSet()

        Dim Settings As TMDB.Scraper.sMySettings_ForScraper
        Settings.ApiKey = _MySettings_MovieSet.APIKey
        Settings.FallBackEng = _MySettings_MovieSet.FallBackEng
        Settings.GetAdultItems = _MySettings_MovieSet.GetAdultItems
        Settings.PrefLanguage = _MySettings_MovieSet.PrefLanguage

        Dim _scraper As New TMDB.Scraper(Settings)

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
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_Movie(ByRef oDBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV, ByRef Options As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.Scraper
        logger.Trace("Started TMDB Scraper")

        LoadSettings_Movie()

        Dim Settings As TMDB.Scraper.sMySettings_ForScraper
        Settings.ApiKey = _MySettings_Movie.APIKey
        Settings.FallBackEng = _MySettings_Movie.FallBackEng
        Settings.GetAdultItems = _MySettings_Movie.GetAdultItems
        Settings.PrefLanguage = _MySettings_Movie.PrefLanguage

        Dim _scraper As New TMDB.Scraper(Settings)
        Dim filterOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(Options, ConfigOptions_Movie)

        If Master.GlobalScrapeMod.MainNFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
            If Not String.IsNullOrEmpty(oDBMovie.Movie.ID) Then
                'IMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                _scraper.GetMovieInfo(oDBMovie.Movie.ID, nMovie, filterOptions.bFullCrew, False, filterOptions, False)
            ElseIf Not String.IsNullOrEmpty(oDBMovie.Movie.TMDBID) Then
                'TMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                _scraper.GetMovieInfo(oDBMovie.Movie.TMDBID, nMovie, filterOptions.bFullCrew, False, filterOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape Then
                'no IMDB-ID or TMDB-ID for movie --> search first and try to get ID!
                If Not String.IsNullOrEmpty(oDBMovie.Movie.Title) Then
                    _scraper.GetSearchMovieInfo(oDBMovie.Movie.Title, oDBMovie, nMovie, ScrapeType, filterOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nMovie.TMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nMovie.TMDBID) Then
            Select Case ScrapeType
                Case Enums.ScrapeType_Movie_MovieSet_TV.FilterAuto, Enums.ScrapeType_Movie_MovieSet_TV.FullAuto, Enums.ScrapeType_Movie_MovieSet_TV.MarkAuto, Enums.ScrapeType_Movie_MovieSet_TV.NewAuto, Enums.ScrapeType_Movie_MovieSet_TV.MissAuto
                    nMovie = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape OrElse ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleAuto Then
            If String.IsNullOrEmpty(oDBMovie.Movie.ID) AndAlso String.IsNullOrEmpty(oDBMovie.Movie.TMDBID) Then
                Using dSearch As New dlgTMDBSearchResults_Movie(Settings, _scraper)
                    If dSearch.ShowDialog(nMovie, oDBMovie.Movie.Title, oDBMovie.Filename, filterOptions, oDBMovie.Movie.Year) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetMovieInfo(nMovie.TMDBID, nMovie, filterOptions.bFullCrew, False, filterOptions, False)
                        'if a movie is found, set DoSearch back to "false" for following scrapers
                        Functions.SetScraperMod_Movie_MovieSet(Enums.ModType_Movie.DoSearch, False, False)
                    Else
                        nMovie = Nothing
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        'set new informations for following scrapers
        If Not String.IsNullOrEmpty(nMovie.Title) Then
            oDBMovie.Movie.Title = nMovie.Title
        End If
        If Not String.IsNullOrEmpty(nMovie.OriginalTitle) Then
            oDBMovie.Movie.OriginalTitle = nMovie.OriginalTitle
        End If
        If Not String.IsNullOrEmpty(nMovie.Year) Then
            oDBMovie.Movie.Year = nMovie.Year
        End If
        If Not String.IsNullOrEmpty(nMovie.ID) Then
            oDBMovie.Movie.ID = nMovie.ID
        End If
        If Not String.IsNullOrEmpty(nMovie.IMDBID) Then
            oDBMovie.Movie.IMDBID = nMovie.IMDBID
        End If
        If Not String.IsNullOrEmpty(nMovie.TMDBID) Then
            oDBMovie.Movie.TMDBID = nMovie.TMDBID
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function Scraper_MovieSet(ByRef DBMovieSet As Structures.DBMovieSet, ByRef ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV, ByRef Options As Structures.ScrapeOptions_MovieSet) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_MovieSet.Scraper
        logger.Trace("Started scrape TMDB")

        LoadSettings_MovieSet()

        Dim Settings As TMDB.Scraper.sMySettings_ForScraper
        Settings.ApiKey = _MySettings_MovieSet.APIKey
        Settings.FallBackEng = _MySettings_MovieSet.FallBackEng
        Settings.GetAdultItems = _MySettings_MovieSet.GetAdultItems
        Settings.PrefLanguage = _MySettings_MovieSet.PrefLanguage

        Dim _scraper As New TMDB.Scraper(Settings)

        Dim tTitle As String = String.Empty
        Dim OldTitle As String = DBMovieSet.ListTitle
        Dim filterOptions As Structures.ScrapeOptions_MovieSet = Functions.MovieSetScrapeOptionsAndAlso(Options, ConfigOptions_MovieSet)

        If Master.GlobalScrapeMod.MainNFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
            If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then
                _scraper.GetMovieSetInfo(DBMovieSet.MovieSet.ID, DBMovieSet.MovieSet, False, filterOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape Then
                If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then
                    DBMovieSet.MovieSet = _scraper.GetSearchMovieSetInfo(DBMovieSet.MovieSet.Title, DBMovieSet, ScrapeType, filterOptions)
                End If
                If String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        ' why a scraper should initialize the DBMovie structure?
        ' Answer (DanCooper): If you want to CHANGE the movie. For this, all existing (incorrect) information must be deleted.
        If ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape AndAlso Master.GlobalScrapeMod.DoSearch _
         AndAlso ModulesManager.Instance.externalScrapersModules_Data_MovieSet.OrderBy(Function(y) y.ModuleOrder).FirstOrDefault(Function(e) e.ProcessorModule.ScraperEnabled).AssemblyName = _AssemblyName Then
            DBMovieSet.MovieSet.ID = String.Empty
            DBMovieSet.RemoveBanner = True
            DBMovieSet.RemoveClearArt = True
            DBMovieSet.RemoveClearLogo = True
            DBMovieSet.RemoveDiscArt = True
            DBMovieSet.RemoveFanart = True
            DBMovieSet.RemoveLandscape = True
            DBMovieSet.RemovePoster = True
            DBMovieSet.BannerPath = String.Empty
            DBMovieSet.ClearArtPath = String.Empty
            DBMovieSet.ClearLogoPath = String.Empty
            DBMovieSet.DiscArtPath = String.Empty
            DBMovieSet.FanartPath = String.Empty
            DBMovieSet.LandscapePath = String.Empty
            DBMovieSet.NfoPath = String.Empty
            DBMovieSet.PosterPath = String.Empty
            DBMovieSet.MovieSet.Clear()
        End If

        If String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then
            Select Case ScrapeType
                Case Enums.ScrapeType_Movie_MovieSet_TV.FilterAuto, Enums.ScrapeType_Movie_MovieSet_TV.FullAuto, Enums.ScrapeType_Movie_MovieSet_TV.MarkAuto, Enums.ScrapeType_Movie_MovieSet_TV.NewAuto, Enums.ScrapeType_Movie_MovieSet_TV.MissAuto
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
            If ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape Then

                'This is a workaround to remove the "TreeView" error on search results window. The problem is that the last search results are still existing in _TMDBg. 
                'I don't know another way to remove it. It works, It works so far without errors.
                _scraper = New TMDB.Scraper(Settings)

                Using dSearch As New dlgTMDBSearchResults_MovieSet(Settings, _scraper)
                    Dim tmpTitle As String = DBMovieSet.MovieSet.Title
                    If String.IsNullOrEmpty(tmpTitle) Then
                        tmpTitle = DBMovieSet.ListTitle
                    End If
                    If dSearch.ShowDialog(tmpTitle, filterOptions) = Windows.Forms.DialogResult.OK Then
                        If Not String.IsNullOrEmpty(Master.tmpMovieSet.ID) Then
                            ' if we changed the ID tipe we need to clear everything and rescrape
                            ' TODO: check TMDB if IMDB NullOrEmpty
                            If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) AndAlso Not (DBMovieSet.MovieSet.ID = Master.tmpMovieSet.ID) Then
                                Master.currMovieSet.RemoveBanner = True
                                Master.currMovieSet.RemoveClearArt = True
                                Master.currMovieSet.RemoveClearLogo = True
                                Master.currMovieSet.RemoveDiscArt = True
                                Master.currMovieSet.RemoveFanart = True
                                Master.currMovieSet.RemoveLandscape = True
                                Master.currMovieSet.RemovePoster = True
                                Master.currMovieSet.BannerPath = String.Empty
                                Master.currMovieSet.ClearArtPath = String.Empty
                                Master.currMovieSet.ClearLogoPath = String.Empty
                                Master.currMovieSet.DiscArtPath = String.Empty
                                Master.currMovieSet.FanartPath = String.Empty
                                Master.currMovieSet.NfoPath = String.Empty
                                Master.currMovieSet.LandscapePath = String.Empty
                                Master.currMovieSet.PosterPath = String.Empty
                            End If
                            DBMovieSet.MovieSet.ID = Master.tmpMovieSet.ID
                        End If
                        If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) AndAlso Master.GlobalScrapeMod.MainNFO Then
                            _scraper.GetMovieSetInfo(DBMovieSet.MovieSet.ID, DBMovieSet.MovieSet, False, filterOptions, False)
                        End If
                    Else
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then
            tTitle = StringUtils.SortTokens_MovieSet(DBMovieSet.MovieSet.Title)
            DBMovieSet.ListTitle = tTitle
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
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_TV(ByRef oDBTV As Structures.DBTV, ByRef nShow As MediaContainers.TVShow, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV, ByRef Options As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVShow
        logger.Trace("Started TMDB Scraper")

        LoadSettings_TV()

        Dim Settings As TMDB.Scraper.sMySettings_ForScraper
        Settings.ApiKey = _MySettings_TV.APIKey
        Settings.FallBackEng = _MySettings_TV.FallBackEng
        Settings.GetAdultItems = _MySettings_TV.GetAdultItems
        Settings.PrefLanguage = oDBTV.Language

        Dim _scraper As New TMDB.Scraper(Settings)
        Dim filterOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions_TV)

        If ScrapeModifier.MainNFO AndAlso Not ScrapeModifier.DoSearch Then
            If Not String.IsNullOrEmpty(oDBTV.TVShow.TMDB) Then
                'TMDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                _scraper.GetTVShowInfo(oDBTV.TVShow.TMDB, nShow, False, filterOptions, False, ScrapeModifier.withEpisodes)
            ElseIf Not String.IsNullOrEmpty(oDBTV.TVShow.TVDB) Then
                oDBTV.TVShow.TMDB = _scraper.GetTMDBbyTVDB(oDBTV.TVShow.TVDB)
                If String.IsNullOrEmpty(oDBTV.TVShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                _scraper.GetTVShowInfo(oDBTV.TVShow.TMDB, nShow, False, filterOptions, False, ScrapeModifier.withEpisodes)
            ElseIf Not String.IsNullOrEmpty(oDBTV.TVShow.IMDB) Then
                oDBTV.TVShow.TMDB = _scraper.GetTMDBbyIMDB(oDBTV.TVShow.IMDB)
                If String.IsNullOrEmpty(oDBTV.TVShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                _scraper.GetTVShowInfo(oDBTV.TVShow.TMDB, nShow, False, filterOptions, False, ScrapeModifier.withEpisodes)
            ElseIf Not ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If Not String.IsNullOrEmpty(oDBTV.TVShow.Title) Then
                    _scraper.GetSearchTVShowInfo(oDBTV.TVShow.Title, oDBTV, nShow, ScrapeType, filterOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nShow.TMDB) Then
            Select Case ScrapeType
                Case Enums.ScrapeType_Movie_MovieSet_TV.FilterAuto, Enums.ScrapeType_Movie_MovieSet_TV.FullAuto, Enums.ScrapeType_Movie_MovieSet_TV.MarkAuto, Enums.ScrapeType_Movie_MovieSet_TV.NewAuto, Enums.ScrapeType_Movie_MovieSet_TV.MissAuto
                    nShow = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape OrElse ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleAuto Then
            If String.IsNullOrEmpty(oDBTV.TVShow.TMDB) Then
                Using dSearch As New dlgTMDBSearchResults_TV(Settings, _scraper)
                    If dSearch.ShowDialog(nShow, oDBTV.TVShow.Title, oDBTV.ShowPath, filterOptions) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetTVShowInfo(nShow.TMDB, nShow, False, filterOptions, False, ScrapeModifier.withEpisodes)
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
            oDBTV.TVShow.Title = nShow.Title
        End If
        If Not String.IsNullOrEmpty(nShow.TVDB) Then
            oDBTV.TVShow.TVDB = nShow.TVDB
        End If
        If Not String.IsNullOrEmpty(nShow.IMDB) Then
            oDBTV.TVShow.IMDB = nShow.IMDB
        End If
        If Not String.IsNullOrEmpty(nShow.TMDB) Then
            oDBTV.TVShow.TMDB = nShow.TMDB
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function Scraper_TVEpisode(ByRef oDBTVEpisode As Structures.DBTV, ByRef nEpisode As MediaContainers.EpisodeDetails, ByVal Options As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVEpisode
        logger.Trace("Started TMDB Scraper")

        LoadSettings_TV()

        Dim Settings As TMDB.Scraper.sMySettings_ForScraper
        Settings.ApiKey = _MySettings_TV.APIKey
        Settings.FallBackEng = _MySettings_TV.FallBackEng
        Settings.GetAdultItems = _MySettings_TV.GetAdultItems
        Settings.PrefLanguage = oDBTVEpisode.Language

        Dim _scraper As New TMDB.Scraper(Settings)
        Dim filterOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions_TV)

        If String.IsNullOrEmpty(oDBTVEpisode.TVShow.TMDB) AndAlso Not String.IsNullOrEmpty(oDBTVEpisode.TVShow.TVDB) Then
            oDBTVEpisode.TVShow.TMDB = _scraper.GetTMDBbyTVDB(oDBTVEpisode.TVShow.TVDB)
        End If

        If Not String.IsNullOrEmpty(oDBTVEpisode.TVShow.TMDB) Then
            If Not oDBTVEpisode.TVEp.Episode = -1 AndAlso Not oDBTVEpisode.TVEp.Season = -1 Then
                nEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBTVEpisode.TVShow.TMDB), oDBTVEpisode.TVEp.Season, oDBTVEpisode.TVEp.Episode, filterOptions)
            ElseIf Not String.IsNullOrEmpty(oDBTVEpisode.TVEp.Aired) Then
                nEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBTVEpisode.TVShow.TMDB), oDBTVEpisode.TVEp.Aired, filterOptions)
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

    Structure sMySettings

#Region "Fields"
        Dim FallBackEng As Boolean
        Dim GetAdultItems As Boolean
        Dim APIKey As String
        Dim PrefLanguage As String
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class