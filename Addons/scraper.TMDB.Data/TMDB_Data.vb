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
Imports ScraperModule.TMDBdata
Imports NLog
Imports System.Diagnostics

Public Class TMDB_Data
    Implements Interfaces.ScraperModule_Data_Movie
    Implements Interfaces.ScraperModule_Data_MovieSet


#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Shared ConfigOptions_Movie As New Structures.ScrapeOptions_Movie
    Public Shared ConfigOptions_MovieSet As New Structures.ScrapeOptions_MovieSet
    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifier
    Public Shared ConfigScrapeModifier_MovieSet As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    Private TMDBId As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private strPrivateAPIKey As String = String.Empty
    Private _MySettings_Movie As New sMySettings
    Private _MySettings_MovieSet As New sMySettings
    Private _TMDBg As TMDBdata.Scraper
    Private _Name As String = "TMDB_Data"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _setup_Movie As frmTMDBInfoSettingsHolder_Movie
    Private _setup_MovieSet As frmTMDBInfoSettingsHolder_MovieSet
    Private _TMDBConf As V3.TmdbConfiguration
    Private _TMDBConfE As V3.TmdbConfiguration
    Private _TMDBApi As V3.Tmdb 'preferred language
    Private _TMDBApiE As V3.Tmdb 'english language
    Private _TMDBApiA As V3.Tmdb 'all languages
    Private _TMDBConf_MovieSet As V3.TmdbConfiguration
    Private _TMDBConfE_MovieSet As V3.TmdbConfiguration
    Private _TMDBApi_MovieSet As V3.Tmdb 'preferred language
    Private _TMDBApiE_MovieSet As V3.Tmdb 'english language
    Private _TMDBApiA_MovieSet As V3.Tmdb 'all languages

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

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleName, Interfaces.ScraperModule_Data_MovieSet.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleVersion, Interfaces.ScraperModule_Data_MovieSet.ModuleVersion
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

#End Region 'Properties

#Region "Methods"

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
        RaiseEvent ScraperSetupChanged_Movie(String.Concat(Me._Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_MovieSet(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_MovieSet = state
        RaiseEvent ScraperSetupChanged_MovieSet(String.Concat(Me._Name, "_MovieSet"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
        'Must be after Load settings to retrieve the correct API key
        _TMDBApi = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey, _MySettings_Movie.PrefLanguage)
        If IsNothing(_TMDBApi) Then
            logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message)
        Else
            If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                logger.Error(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString())
            End If
        End If
        _TMDBConf = _TMDBApi.GetConfiguration()
        _TMDBApiE = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey)
        _TMDBConfE = _TMDBApiE.GetConfiguration()
        _TMDBApiA = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey, "")
        _TMDBg = New TMDBdata.Scraper(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _TMDBApiA, True)
    End Sub

    Sub Init_MovieSet(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_MovieSet.Init
        _AssemblyName = sAssemblyName
        LoadSettings_MovieSet()
        'Must be after Load settings to retrieve the correct API key
        _TMDBApi_MovieSet = New WatTmdb.V3.Tmdb(_MySettings_MovieSet.APIKey, _MySettings_MovieSet.PrefLanguage)
        If IsNothing(_TMDBApi_MovieSet) Then
            logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi_MovieSet.Error.status_message)
        Else
            If Not IsNothing(_TMDBApi_MovieSet.Error) AndAlso _TMDBApi_MovieSet.Error.status_message.Length > 0 Then
                logger.Error(_TMDBApi_MovieSet.Error.status_message, _TMDBApi_MovieSet.Error.status_code.ToString())
            End If
        End If
        _TMDBConf_MovieSet = _TMDBApi_MovieSet.GetConfiguration()
        _TMDBApiE_MovieSet = New WatTmdb.V3.Tmdb(_MySettings_MovieSet.APIKey)
        _TMDBConfE_MovieSet = _TMDBApiE_MovieSet.GetConfiguration()
        _TMDBApiA_MovieSet = New WatTmdb.V3.Tmdb(_MySettings_MovieSet.APIKey, "")
        _TMDBg = New TMDBdata.Scraper(_TMDBConf_MovieSet, _TMDBConfE_MovieSet, _TMDBApi_MovieSet, _TMDBApiE_MovieSet, _TMDBApiA_MovieSet, False)
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_Movie.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_Movie = New frmTMDBInfoSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.API = _setup_Movie.txtApiKey.Text
        _setup_Movie.Lang = _setup_Movie.cbPrefLanguage.Text
        _setup_Movie.cbEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.cbPrefLanguage.Text = _MySettings_Movie.PrefLanguage
        _setup_Movie.chkCast.Checked = ConfigOptions_Movie.bFullCast
        _setup_Movie.chkCollection.Checked = ConfigOptions_Movie.bCollection
        _setup_Movie.chkCountry.Checked = ConfigOptions_Movie.bCountry
        _setup_Movie.chkCrew.Checked = ConfigOptions_Movie.bFullCrew
        _setup_Movie.chkFallBackEng.Checked = _MySettings_Movie.FallBackEng
        _setup_Movie.chkGenre.Checked = ConfigOptions_Movie.bGenre
        _setup_Movie.chkGetAdultItems.Checked = _MySettings_Movie.GetAdultItems
        _setup_Movie.chkMPAA.Checked = ConfigOptions_Movie.bMPAA
        _setup_Movie.chkPlot.Checked = ConfigOptions_Movie.bPlot
        _setup_Movie.chkRating.Checked = ConfigOptions_Movie.bRating
        _setup_Movie.chkRelease.Checked = ConfigOptions_Movie.bRelease
        _setup_Movie.chkRuntime.Checked = ConfigOptions_Movie.bRuntime
        _setup_Movie.chkStudio.Checked = ConfigOptions_Movie.bStudio
        _setup_Movie.chkTagline.Checked = ConfigOptions_Movie.bTagline
        _setup_Movie.chkTitle.Checked = ConfigOptions_Movie.bTitle
        _setup_Movie.chkTrailer.Checked = ConfigOptions_Movie.bTrailer
        _setup_Movie.chkVotes.Checked = ConfigOptions_Movie.bVotes
        _setup_Movie.chkYear.Checked = ConfigOptions_Movie.bYear
        _setup_Movie.txtApiKey.Text = strPrivateAPIKey

        _setup_Movie.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "_Movie")
        SPanel.Text = Master.eLang.GetString(937, "TMDB")
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
        _setup_MovieSet = New frmTMDBInfoSettingsHolder_MovieSet
        LoadSettings_MovieSet()
        _setup_MovieSet.API = _setup_MovieSet.txtApiKey.Text
        _setup_MovieSet.Lang = _setup_MovieSet.cbPrefLanguage.Text
        _setup_MovieSet.cbEnabled.Checked = _ScraperEnabled_MovieSet
        _setup_MovieSet.cbPrefLanguage.Text = _MySettings_MovieSet.PrefLanguage
        _setup_MovieSet.chkFallBackEng.Checked = _MySettings_MovieSet.FallBackEng
        _setup_MovieSet.chkPlot.Checked = ConfigOptions_MovieSet.bPlot
        _setup_MovieSet.chkTitle.Checked = ConfigOptions_MovieSet.bTitle
        _setup_MovieSet.txtApiKey.Text = strPrivateAPIKey

        _setup_MovieSet.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "_MovieSet")
        SPanel.Text = Master.eLang.GetString(937, "TMDB")
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

    Sub LoadSettings_Movie()
        ConfigOptions_Movie.bCast = clsAdvancedSettings.GetBooleanSetting("DoCast", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bCollection = clsAdvancedSettings.GetBooleanSetting("DoCollection", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bCountry = clsAdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bFullCast = clsAdvancedSettings.GetBooleanSetting("DoFullCast", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bFullCast = clsAdvancedSettings.GetBooleanSetting("FullCast", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bFullCrew = clsAdvancedSettings.GetBooleanSetting("DoFullCrews", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bFullCrew = clsAdvancedSettings.GetBooleanSetting("FullCrew", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bGenre = clsAdvancedSettings.GetBooleanSetting("DoGenres", True, , Enums.Content_Type.Movie)
        ConfigOptions_Movie.bMPAA = clsAdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.Content_Type.Movie)
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
        ConfigScrapeModifier_Movie.Meta = True
        ConfigScrapeModifier_Movie.NFO = True
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
        ConfigScrapeModifier_Movie.Meta = False
        ConfigScrapeModifier_Movie.NFO = True
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoCast", ConfigOptions_Movie.bCast, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoCert", ConfigOptions_Movie.bCert, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoCollection", ConfigOptions_Movie.bCollection, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoCountry", ConfigOptions_Movie.bCountry, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoDirector", ConfigOptions_Movie.bDirector, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier_Movie.Fanart, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoFullCast", ConfigOptions_Movie.bFullCast, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoFullCrews", ConfigOptions_Movie.bFullCrew, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoGenres", ConfigOptions_Movie.bGenre, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoMPAA", ConfigOptions_Movie.bMPAA, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoMusic", ConfigOptions_Movie.bMusicBy, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoOtherCrews", ConfigOptions_Movie.bOtherCrew, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoOutline", ConfigOptions_Movie.bOutline, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_Movie.bPlot, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier_Movie.Poster, , , Enums.Content_Type.Movie)
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
            settings.SetBooleanSetting("FullCast", ConfigOptions_Movie.bFullCast, , , Enums.Content_Type.Movie)
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

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        ConfigOptions_Movie.bCast = _setup_Movie.chkCast.Checked
        ConfigOptions_Movie.bCert = ConfigOptions_Movie.bMPAA
        ConfigOptions_Movie.bCollection = _setup_Movie.chkCollection.Checked
        ConfigOptions_Movie.bCountry = _setup_Movie.chkCountry.Checked
        ConfigOptions_Movie.bDirector = _setup_Movie.chkCrew.Checked
        ConfigOptions_Movie.bFullCast = _setup_Movie.chkCast.Checked
        ConfigOptions_Movie.bFullCrew = _setup_Movie.chkCrew.Checked
        ConfigOptions_Movie.bGenre = _setup_Movie.chkGenre.Checked
        ConfigOptions_Movie.bMPAA = _setup_Movie.chkMPAA.Checked
        ConfigOptions_Movie.bMusicBy = _setup_Movie.chkCrew.Checked
        ConfigOptions_Movie.bOtherCrew = _setup_Movie.chkCrew.Checked
        ConfigOptions_Movie.bOutline = _setup_Movie.chkPlot.Checked
        ConfigOptions_Movie.bPlot = _setup_Movie.chkPlot.Checked
        ConfigOptions_Movie.bProducers = _setup_Movie.chkCrew.Checked
        ConfigOptions_Movie.bRating = _setup_Movie.chkRating.Checked
        ConfigOptions_Movie.bRelease = _setup_Movie.chkRelease.Checked
        ConfigOptions_Movie.bRuntime = _setup_Movie.chkRuntime.Checked
        ConfigOptions_Movie.bStudio = _setup_Movie.chkStudio.Checked
        ConfigOptions_Movie.bTagline = _setup_Movie.chkTagline.Checked
        ConfigOptions_Movie.bTitle = _setup_Movie.chkTitle.Checked
        ConfigOptions_Movie.bTop250 = False
        ConfigOptions_Movie.bTrailer = _setup_Movie.chkTrailer.Checked
        ConfigOptions_Movie.bVotes = _setup_Movie.chkVotes.Checked
        ConfigOptions_Movie.bWriters = _setup_Movie.chkCrew.Checked
        ConfigOptions_Movie.bYear = _setup_Movie.chkYear.Checked
        _MySettings_Movie.FallBackEng = _setup_Movie.chkFallBackEng.Checked
        _MySettings_Movie.GetAdultItems = _setup_Movie.chkGetAdultItems.Checked
        _MySettings_Movie.PrefLanguage = _setup_Movie.cbPrefLanguage.Text
        SaveSettings_Movie()
        'ModulesManager.Instance.SaveSettings()
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
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movieset
            RemoveHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
            _setup_MovieSet.Dispose()
        End If
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef sStudio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDBID
        If Not String.IsNullOrEmpty(sIMDBID) Then
            sTMDBID = _TMDBg.GetMovieID(sIMDBID)
        End If
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetCollectionID(ByVal sIMDBID As String, ByRef sCollectionID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_MovieSet.GetCollectionID
        sCollectionID = _TMDBg.GetMovieCollectionID(sIMDBID)
        If String.IsNullOrEmpty(sCollectionID) Then
            Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
        End If
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    'Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.Scraper
    '    Dim tTitle As String = String.Empty
    '    Dim OldTitle As String = DBMovie.Movie.Title
    '    Dim filterOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(Options, ConfigOptions_Movie)

    '    If IsNothing(_TMDBApi) Then
    '        logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message)
    '        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
    '    Else
    '        If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
    '            logger.Error(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString())
    '            'try to create a new one
    '            logger.Trace("Create new TMDB API")
    '            _TMDBApi = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey, _MySettings_Movie.PrefLanguage)
    '            _TMDBApiE = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey)
    '            _TMDBApiA = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey, "")
    '            If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
    '                Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
    '            End If
    '        End If
    '    End If

    '    If Master.GlobalScrapeMod.NFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
    '        If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
    '            _TMDBg.GetMovieInfo(DBMovie.Movie.ID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
    '        ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
    '            If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
    '                DBMovie.Movie = _TMDBg.GetSearchMovieInfo(DBMovie.Movie.Title, DBMovie, ScrapeType, filterOptions)
    '            End If
    '            If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
    '        End If
    '    End If

    '    ' why a scraper should initialize the DBMovie structure?
    '    ' Answer (DanCooper): If you want to CHANGE the movie. For this, all existing (incorrect) information must be deleted.
    '    If ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.GlobalScrapeMod.DoSearch _
    '     AndAlso ModulesManager.Instance.externalScrapersModules_Data_Movie.OrderBy(Function(y) y.ModuleOrder).FirstOrDefault(Function(e) e.ProcessorModule.ScraperEnabled).AssemblyName = _AssemblyName Then
    '        DBMovie.Movie.IMDBID = String.Empty
    '        DBMovie.RemoveActorThumbs = True
    '        DBMovie.RemoveBanner = True
    '        DBMovie.RemoveClearArt = True
    '        DBMovie.RemoveClearLogo = True
    '        DBMovie.RemoveDiscArt = True
    '        DBMovie.RemoveEFanarts = True
    '        DBMovie.RemoveEThumbs = True
    '        DBMovie.RemoveFanart = True
    '        DBMovie.RemoveLandscape = True
    '        DBMovie.RemovePoster = True
    '        DBMovie.RemoveTheme = True
    '        DBMovie.RemoveTrailer = True
    '        DBMovie.BannerPath = String.Empty
    '        DBMovie.ClearArtPath = String.Empty
    '        DBMovie.ClearLogoPath = String.Empty
    '        DBMovie.DiscArtPath = String.Empty
    '        DBMovie.EFanartsPath = String.Empty
    '        DBMovie.EThumbsPath = String.Empty
    '        DBMovie.FanartPath = String.Empty
    '        DBMovie.LandscapePath = String.Empty
    '        DBMovie.NfoPath = String.Empty
    '        DBMovie.PosterPath = String.Empty
    '        DBMovie.SubPath = String.Empty
    '        DBMovie.ThemePath = String.Empty
    '        DBMovie.TrailerPath = String.Empty
    '        DBMovie.Movie.Clear()
    '    End If

    '    If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
    '        Select Case ScrapeType
    '            Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.UpdateAuto
    '                Return New Interfaces.ModuleResult With {.breakChain = False}
    '        End Select
    '        If ScrapeType = Enums.ScrapeType.SingleScrape Then

    '            'This is a workaround to remove the "TreeView" error on search results window. The problem is that the last search results are still existing in _TMDBg. 
    '            'I don't know another way to remove it. It works, It works so far without errors.
    '            'TODO: maybe find another solution.
    '            Me._TMDBg = New TMDBg.Scraper(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _TMDBApiA, True)

    '            Using dSearch As New dlgTMDBSearchResults_Movie(_MySettings_Movie, Me._TMDBg)
    '                Dim tmpTitle As String = DBMovie.Movie.Title
    '                Dim tmpYear As Integer = CInt(IIf(Not String.IsNullOrEmpty(DBMovie.Movie.Year), DBMovie.Movie.Year, 0))
    '                If String.IsNullOrEmpty(tmpTitle) Then
    '                    If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
    '                        tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name, False)
    '                    ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
    '                        tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name, False)
    '                    Else
    '                        tmpTitle = StringUtils.FilterName(If(DBMovie.IsSingle, Directory.GetParent(DBMovie.Filename).Name, Path.GetFileNameWithoutExtension(DBMovie.Filename)))
    '                    End If
    '                End If
    '                If dSearch.ShowDialog(tmpTitle, DBMovie.Filename, filterOptions, tmpYear) = Windows.Forms.DialogResult.OK Then
    '                    If Not String.IsNullOrEmpty(Master.tmpMovie.TMDBID) Then
    '                        ' if we changed the ID tipe we need to clear everything and rescrape
    '                        ' TODO: check TMDB if IMDB NullOrEmpty
    '                        If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) AndAlso Not (DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID) Then
    '                            Master.currMovie.RemoveActorThumbs = True
    '                            Master.currMovie.RemoveBanner = True
    '                            Master.currMovie.RemoveClearArt = True
    '                            Master.currMovie.RemoveClearLogo = True
    '                            Master.currMovie.RemoveDiscArt = True
    '                            Master.currMovie.RemoveEThumbs = True
    '                            Master.currMovie.RemoveEFanarts = True
    '                            Master.currMovie.RemoveFanart = True
    '                            Master.currMovie.RemoveLandscape = True
    '                            Master.currMovie.RemovePoster = True
    '                            Master.currMovie.RemoveTheme = True
    '                            Master.currMovie.RemoveTrailer = True
    '                            Master.currMovie.BannerPath = String.Empty
    '                            Master.currMovie.ClearArtPath = String.Empty
    '                            Master.currMovie.ClearLogoPath = String.Empty
    '                            Master.currMovie.DiscArtPath = String.Empty
    '                            Master.currMovie.EFanartsPath = String.Empty
    '                            Master.currMovie.EThumbsPath = String.Empty
    '                            Master.currMovie.FanartPath = String.Empty
    '                            Master.currMovie.NfoPath = String.Empty
    '                            Master.currMovie.LandscapePath = String.Empty
    '                            Master.currMovie.PosterPath = String.Empty
    '                            Master.currMovie.SubPath = String.Empty
    '                            Master.currMovie.ThemePath = String.Empty
    '                            Master.currMovie.TrailerPath = String.Empty
    '                        End If
    '                        DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID
    '                        DBMovie.Movie.TMDBID = Master.tmpMovie.TMDBID
    '                    End If
    '                    If Not String.IsNullOrEmpty(DBMovie.Movie.TMDBID) AndAlso Master.GlobalScrapeMod.NFO Then
    '                        _TMDBg.GetMovieInfo(DBMovie.Movie.TMDBID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
    '                    End If
    '                Else
    '                    Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
    '                End If
    '            End Using
    '        End If
    '    End If

    '    If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
    '        tTitle = StringUtils.FilterTokens_Movie(DBMovie.Movie.Title)
    '        If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = tTitle
    '        If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(DBMovie.Movie.Year) Then
    '            DBMovie.ListTitle = String.Format("{0} ({1})", tTitle, DBMovie.Movie.Year)
    '        Else
    '            DBMovie.ListTitle = tTitle
    '        End If
    '    Else
    '        If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
    '            DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name)
    '        ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
    '            DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name)
    '        Else
    '            If DBMovie.UseFolder AndAlso DBMovie.IsSingle Then
    '                DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(DBMovie.Filename).Name)
    '            Else
    '                DBMovie.ListTitle = StringUtils.FilterName(Path.GetFileNameWithoutExtension(DBMovie.Filename))
    '            End If
    '        End If
    '        If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = DBMovie.ListTitle
    '    End If

    '    Return New Interfaces.ModuleResult With {.breakChain = False}
    'End Function

    Function Scraper(ByRef DBMovieSet As Structures.DBMovieSet, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_MovieSet) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_MovieSet.Scraper
        Dim tTitle As String = String.Empty
        Dim OldTitle As String = DBMovieSet.ListTitle
        Dim filterOptions As Structures.ScrapeOptions_MovieSet = Functions.MovieSetScrapeOptionsAndAlso(Options, ConfigOptions_MovieSet)

        If IsNothing(_TMDBApi_MovieSet) Then
            logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi_MovieSet.Error.status_message)
            Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
        Else
            If Not IsNothing(_TMDBApi_MovieSet.Error) AndAlso _TMDBApi_MovieSet.Error.status_message.Length > 0 Then
                logger.Error(_TMDBApi_MovieSet.Error.status_message, _TMDBApi_MovieSet.Error.status_code.ToString())
                'try to create a new one
                logger.Trace("Create new TMDB API")
                _TMDBApi_MovieSet = New WatTmdb.V3.Tmdb(_MySettings_MovieSet.APIKey, _MySettings_MovieSet.PrefLanguage)
                _TMDBApiE_MovieSet = New WatTmdb.V3.Tmdb(_MySettings_MovieSet.APIKey)
                _TMDBApiA_MovieSet = New WatTmdb.V3.Tmdb(_MySettings_MovieSet.APIKey, "")
                If Not IsNothing(_TMDBApi_MovieSet.Error) AndAlso _TMDBApi_MovieSet.Error.status_message.Length > 0 Then
                    logger.Error(_TMDBApi_MovieSet.Error.status_message, _TMDBApi_MovieSet.Error.status_code.ToString())
                    Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                End If
            End If
        End If

        If Master.GlobalScrapeMod.NFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
            If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then
                _TMDBg.GetMovieSetInfo(DBMovieSet.MovieSet.ID, DBMovieSet.MovieSet, False, filterOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then
                    DBMovieSet.MovieSet = _TMDBg.GetSearchMovieSetInfo(DBMovieSet.MovieSet.Title, DBMovieSet, ScrapeType, filterOptions)
                End If
                If String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        ' why a scraper should initialize the DBMovie structure?
        ' Answer (DanCooper): If you want to CHANGE the movie. For this, all existing (incorrect) information must be deleted.
        If ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.GlobalScrapeMod.DoSearch _
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
                Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.UpdateAuto
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
            If ScrapeType = Enums.ScrapeType.SingleScrape Then

                'This is a workaround to remove the "TreeView" error on search results window. The problem is that the last search results are still existing in _TMDBg. 
                'I don't know another way to remove it. It works, It works so far without errors.
                'TODO: maybe find another solution.
                Me._TMDBg = New TMDBdata.Scraper(_TMDBConf_MovieSet, _TMDBConfE_MovieSet, _TMDBApi_MovieSet, _TMDBApiE_MovieSet, _TMDBApiA_MovieSet, False)

                Using dSearch As New dlgTMDBSearchResults_MovieSet(_MySettings_MovieSet, Me._TMDBg)
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
                        If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) AndAlso Master.GlobalScrapeMod.NFO Then
                            _TMDBg.GetMovieSetInfo(DBMovieSet.MovieSet.ID, DBMovieSet.MovieSet, False, filterOptions, False)
                        End If
                    Else
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.Title) Then
            tTitle = StringUtils.FilterTokens_MovieSet(DBMovieSet.MovieSet.Title)
            DBMovieSet.ListTitle = tTitle
        End If

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function


    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">(NOT used at moment!)What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks>Cocotus/Dan 2014/08/30 - Reworked structure: Scraper should NOT consider global scraper settings/locks in Ember, just scraper options of module</remarks>
    Function ScraperNew(ByRef DBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.ScraperNew
        logger.Trace("Started TMDB ScraperNew")

        Dim tTitle As String = String.Empty
        Dim OldTitle As String = DBMovie.Movie.Title
        'only scraper module settings will be considered
        Dim filterOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(ConfigOptions_Movie, ConfigOptions_Movie)

        If IsNothing(_TMDBApi) Then
            logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message)
            Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
        Else
            If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                logger.Error(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString())
                'try to create a new one
                logger.Trace("Create new TMDB API")
                _TMDBApi = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey, _MySettings_Movie.PrefLanguage)
                _TMDBApiE = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey)
                _TMDBApiA = New WatTmdb.V3.Tmdb(_MySettings_Movie.APIKey, "")
                If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                    Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                End If
            End If
        End If

        If Master.GlobalScrapeMod.NFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
            If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
                'IMDB-ID already available -> scrape movie data from IMDB
                _TMDBg.GetMovieInfo(DBMovie.Movie.ID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID for movie --> search first and try to get ID!
                If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
                    DBMovie.Movie = _TMDBg.GetSearchMovieInfo(DBMovie.Movie.Title, DBMovie, ScrapeType, filterOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        ' why a scraper should initialize the DBMovie structure?
        ' Answer (DanCooper): If you want to CHANGE the movie. For this, all existing (incorrect) information must be deleted.
        If ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.GlobalScrapeMod.DoSearch _
         AndAlso ModulesManager.Instance.externalScrapersModules_Data_Movie.OrderBy(Function(y) y.ModuleOrder).FirstOrDefault(Function(e) e.ProcessorModule.ScraperEnabled).AssemblyName = _AssemblyName Then
            DBMovie.Movie.IMDBID = String.Empty
            DBMovie.RemoveActorThumbs = True
            DBMovie.RemoveBanner = True
            DBMovie.RemoveClearArt = True
            DBMovie.RemoveClearLogo = True
            DBMovie.RemoveDiscArt = True
            DBMovie.RemoveEFanarts = True
            DBMovie.RemoveEThumbs = True
            DBMovie.RemoveFanart = True
            DBMovie.RemoveLandscape = True
            DBMovie.RemovePoster = True
            DBMovie.RemoveTheme = True
            DBMovie.RemoveTrailer = True
            DBMovie.BannerPath = String.Empty
            DBMovie.ClearArtPath = String.Empty
            DBMovie.ClearLogoPath = String.Empty
            DBMovie.DiscArtPath = String.Empty
            DBMovie.EFanartsPath = String.Empty
            DBMovie.EThumbsPath = String.Empty
            DBMovie.FanartPath = String.Empty
            DBMovie.LandscapePath = String.Empty
            DBMovie.NfoPath = String.Empty
            DBMovie.PosterPath = String.Empty
            DBMovie.SubPath = String.Empty
            DBMovie.ThemePath = String.Empty
            DBMovie.TrailerPath = String.Empty
            DBMovie.Movie.Clear()
        End If

        If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.UpdateAuto
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
            If ScrapeType = Enums.ScrapeType.SingleScrape Then

                'This is a workaround to remove the "TreeView" error on search results window. The problem is that the last search results are still existing in _TMDBg. 
                'I don't know another way to remove it. It works, It works so far without errors.
                'TODO: maybe find another solution.
                Me._TMDBg = New TMDBdata.Scraper(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _TMDBApiA, True)

                Using dSearch As New dlgTMDBSearchResults_Movie(_MySettings_Movie, Me._TMDBg)
                    Dim tmpTitle As String = DBMovie.Movie.Title
                    Dim tmpYear As Integer = CInt(IIf(Not String.IsNullOrEmpty(DBMovie.Movie.Year), DBMovie.Movie.Year, 0))
                    If String.IsNullOrEmpty(tmpTitle) Then
                        If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
                            tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name, False)
                        ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
                            tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name, False)
                        Else
                            tmpTitle = StringUtils.FilterName(If(DBMovie.IsSingle, Directory.GetParent(DBMovie.Filename).Name, Path.GetFileNameWithoutExtension(DBMovie.Filename)))
                        End If
                    End If
                    If dSearch.ShowDialog(tmpTitle, DBMovie.Filename, filterOptions, tmpYear) = Windows.Forms.DialogResult.OK Then
                        If Not String.IsNullOrEmpty(Master.tmpMovie.TMDBID) Then
                            ' if we changed the ID tipe we need to clear everything and rescrape
                            ' TODO: check TMDB if IMDB NullOrEmpty
                            If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) AndAlso Not (DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID) Then
                                Master.currMovie.RemoveActorThumbs = True
                                Master.currMovie.RemoveBanner = True
                                Master.currMovie.RemoveClearArt = True
                                Master.currMovie.RemoveClearLogo = True
                                Master.currMovie.RemoveDiscArt = True
                                Master.currMovie.RemoveEThumbs = True
                                Master.currMovie.RemoveEFanarts = True
                                Master.currMovie.RemoveFanart = True
                                Master.currMovie.RemoveLandscape = True
                                Master.currMovie.RemovePoster = True
                                Master.currMovie.RemoveTheme = True
                                Master.currMovie.RemoveTrailer = True
                                Master.currMovie.BannerPath = String.Empty
                                Master.currMovie.ClearArtPath = String.Empty
                                Master.currMovie.ClearLogoPath = String.Empty
                                Master.currMovie.DiscArtPath = String.Empty
                                Master.currMovie.EFanartsPath = String.Empty
                                Master.currMovie.EThumbsPath = String.Empty
                                Master.currMovie.FanartPath = String.Empty
                                Master.currMovie.NfoPath = String.Empty
                                Master.currMovie.LandscapePath = String.Empty
                                Master.currMovie.PosterPath = String.Empty
                                Master.currMovie.SubPath = String.Empty
                                Master.currMovie.ThemePath = String.Empty
                                Master.currMovie.TrailerPath = String.Empty
                            End If
                            DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID
                            DBMovie.Movie.TMDBID = Master.tmpMovie.TMDBID
                        End If
                        If Not String.IsNullOrEmpty(DBMovie.Movie.TMDBID) AndAlso Master.GlobalScrapeMod.NFO Then
                            _TMDBg.GetMovieInfo(DBMovie.Movie.TMDBID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
                        End If
                    Else
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
            tTitle = StringUtils.FilterTokens_Movie(DBMovie.Movie.Title)
            If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = tTitle
            If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(DBMovie.Movie.Year) Then
                DBMovie.ListTitle = String.Format("{0} ({1})", tTitle, DBMovie.Movie.Year)
            Else
                DBMovie.ListTitle = tTitle
            End If
        Else
            If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
                DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name)
            ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
                DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name)
            Else
                If DBMovie.UseFolder AndAlso DBMovie.IsSingle Then
                    DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(DBMovie.Filename).Name)
                Else
                    DBMovie.ListTitle = StringUtils.FilterName(Path.GetFileNameWithoutExtension(DBMovie.Filename))
                End If
            End If
            If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = DBMovie.ListTitle
        End If


        'GetMovieInfo writes retrieved IMDB data into DBMovie - copy that into nmovie
        nMovie = CloneFromStruct(DBMovie.Movie, nMovie)

        logger.Trace("Finished TMDB ScraperNew")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function
    Public Function CloneFromStruct(ByVal DBMovie As MediaContainers.Movie, TheClone As MediaContainers.Movie) As MediaContainers.Movie
        TheClone.Title = DBMovie.Title
        TheClone.OriginalTitle = DBMovie.OriginalTitle
        TheClone.SortTitle = DBMovie.SortTitle
        TheClone.Year = DBMovie.Year
        TheClone.Rating = DBMovie.Rating
        TheClone.Votes = DBMovie.Votes
        TheClone.MPAA = DBMovie.MPAA
        TheClone.Top250 = DBMovie.Top250
        TheClone.Countries = DBMovie.Countries
        TheClone.Outline = DBMovie.Outline
        TheClone.Plot = DBMovie.Plot
        TheClone.Tagline = DBMovie.Tagline
        TheClone.Trailer = DBMovie.Trailer
        TheClone.Certification = DBMovie.Certification
        TheClone.Genres = DBMovie.Genres
        TheClone.Runtime = DBMovie.Runtime
        TheClone.ReleaseDate = DBMovie.ReleaseDate
        TheClone.Studio = DBMovie.Studio
        TheClone.Directors = DBMovie.Directors
        TheClone.Credits = DBMovie.Credits
        TheClone.PlayCount = DBMovie.PlayCount
        TheClone.Thumb = DBMovie.Thumb
        TheClone.Fanart = DBMovie.Fanart
        TheClone.Actors = DBMovie.Actors
        TheClone.FileInfo = DBMovie.FileInfo
        TheClone.YSets = DBMovie.YSets
        TheClone.XSets = DBMovie.XSets
        TheClone.Lev = DBMovie.Lev
        TheClone.VideoSource = DBMovie.VideoSource
        TheClone.DateAdded = DBMovie.DateAdded
        Return TheClone
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements EmberAPI.Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_MovieSet() Implements EmberAPI.Interfaces.ScraperModule_Data_MovieSet.ScraperOrderChanged
        _setup_MovieSet.orderChanged()
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