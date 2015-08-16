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
Imports NLog

''' <summary>
''' Native Scraper
''' </summary>
''' <remarks></remarks>
Public Class IMDB_Data
    Implements Interfaces.ScraperModule_Data_Movie
    Implements Interfaces.ScraperModule_Data_TV


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared _AssemblyName As String
    Public Shared ConfigOptions_Movie As New Structures.ScrapeOptions_Movie
    Public Shared ConfigOptions_TV As New Structures.ScrapeOptions_TV
    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifier
    Public Shared ConfigScrapeModifier_TV As New Structures.ScrapeModifier

    Private _SpecialSettings_Movie As New SpecialSettings
    Private _SpecialSettings_TV As New SpecialSettings
    Private _Name As String = "IMDB_Data"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_TV As frmSettingsHolder_TV

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Data_Movie.ModuleSettingsChanged
    Public Event ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_Movie.ScraperEvent
    Public Event ScraperSetupChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_Movie.ScraperSetupChanged
    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Data_Movie.SetupNeedsRestart

    'TV part
    Public Event ModuleSettingsChanged_TV() Implements Interfaces.ScraperModule_Data_TV.ModuleSettingsChanged
    Public Event ScraperEvent_TV(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_TV.ScraperEvent
    Public Event ScraperSetupChanged_TV(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_TV.ScraperSetupChanged
    Public Event SetupNeedsRestart_TV() Implements Interfaces.ScraperModule_Data_TV.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleName, Interfaces.ScraperModule_Data_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleVersion, Interfaces.ScraperModule_Data_TV.ModuleVersion
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

    Private Sub Handle_ModuleSettingsChanged_TV()
        RaiseEvent ModuleSettingsChanged_TV()
    End Sub

    Private Sub Handle_SetupNeedsRestart_Movie()
        RaiseEvent SetupNeedsRestart_Movie()
    End Sub

    Private Sub Handle_SetupNeedsRestart_TV()
        RaiseEvent SetupNeedsRestart_TV()
    End Sub

    Private Sub Handle_SetupScraperChanged_Movie(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_Movie = state
        RaiseEvent ScraperSetupChanged_Movie(String.Concat(Me._Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_TV(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_TV = state
        RaiseEvent ScraperSetupChanged_TV(String.Concat(Me._Name, "_TV"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
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

        _setup_Movie.chkCast.Checked = ConfigOptions_Movie.bCast
        _setup_Movie.chkCertification.Checked = ConfigOptions_Movie.bCert
        _setup_Movie.chkCountry.Checked = ConfigOptions_Movie.bCountry
        _setup_Movie.chkCrew.Checked = ConfigOptions_Movie.bOtherCrew
        _setup_Movie.chkDirector.Checked = ConfigOptions_Movie.bDirector
        _setup_Movie.chkFullCrew.Checked = ConfigOptions_Movie.bFullCrew
        _setup_Movie.chkGenre.Checked = ConfigOptions_Movie.bGenre
        _setup_Movie.chkMPAA.Checked = ConfigOptions_Movie.bMPAA
        _setup_Movie.chkMusicBy.Checked = ConfigOptions_Movie.bMusicBy
        _setup_Movie.chkOriginalTitle.Checked = ConfigOptions_Movie.bOriginalTitle
        _setup_Movie.chkOutline.Checked = ConfigOptions_Movie.bOutline
        _setup_Movie.chkPlot.Checked = ConfigOptions_Movie.bPlot
        _setup_Movie.chkProducers.Checked = ConfigOptions_Movie.bProducers
        _setup_Movie.chkRating.Checked = ConfigOptions_Movie.bRating
        _setup_Movie.chkRelease.Checked = ConfigOptions_Movie.bRelease
        _setup_Movie.chkRuntime.Checked = ConfigOptions_Movie.bRuntime
        _setup_Movie.chkStudio.Checked = ConfigOptions_Movie.bStudio
        _setup_Movie.chkTagline.Checked = ConfigOptions_Movie.bTagline
        _setup_Movie.chkTitle.Checked = ConfigOptions_Movie.bTitle
        _setup_Movie.chkTop250.Checked = ConfigOptions_Movie.bTop250
        _setup_Movie.chkTrailer.Checked = ConfigOptions_Movie.bTrailer
        _setup_Movie.chkVotes.Checked = ConfigOptions_Movie.bVotes
        _setup_Movie.chkWriters.Checked = ConfigOptions_Movie.bWriters
        _setup_Movie.chkYear.Checked = ConfigOptions_Movie.bYear

        _setup_Movie.cbForceTitleLanguage.Text = _SpecialSettings_Movie.ForceTitleLanguage
        _setup_Movie.chkCountryAbbreviation.Checked = _SpecialSettings_Movie.CountryAbbreviation
        _setup_Movie.chkFallBackworldwide.Checked = _SpecialSettings_Movie.FallBackWorldwide
        _setup_Movie.chkPartialTitles.Checked = _SpecialSettings_Movie.SearchPartialTitles
        _setup_Movie.chkPopularTitles.Checked = _SpecialSettings_Movie.SearchPopularTitles
        _setup_Movie.chkTvTitles.Checked = _SpecialSettings_Movie.SearchTvTitles
        _setup_Movie.chkVideoTitles.Checked = _SpecialSettings_Movie.SearchVideoTitles
        _setup_Movie.chkShortTitles.Checked = _SpecialSettings_Movie.SearchShortTitles
        _setup_Movie.chkStudiowithDistributors.Checked = _SpecialSettings_Movie.StudiowithDistributors

        _setup_Movie.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "_Movie")
        SPanel.Text = "IMDB"
        SPanel.Prefix = "IMDBMovieInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieData"
        SPanel.Type = Master.eLang.GetString(36, "Movies")
        SPanel.ImageIndex = If(_ScraperEnabled_Movie, 9, 10)
        SPanel.Panel = _setup_Movie.pnlSettings

        AddHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
        AddHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
        Return SPanel
    End Function

    Function InjectSetupScraper_TV() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_TV.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_TV = New frmSettingsHolder_TV
        LoadSettings_TV()
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

        _setup_TV.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "_TV")
        SPanel.Text = "IMDB"
        SPanel.Prefix = "IMDBTVInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlTVData"
        SPanel.Type = Master.eLang.GetString(653, "TV Shows")
        SPanel.ImageIndex = If(_ScraperEnabled_TV, 9, 10)
        SPanel.Panel = _setup_TV.pnlSettings

        AddHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
        AddHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
        Return SPanel
    End Function

    Sub LoadSettings_Movie()
        ConfigOptions_Movie.bCast = clsAdvancedSettings.GetBooleanSetting("DoCast", True)
        ConfigOptions_Movie.bCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True)
        ConfigOptions_Movie.bCountry = clsAdvancedSettings.GetBooleanSetting("DoCountry", True)
        ConfigOptions_Movie.bDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True)
        ConfigOptions_Movie.bFullCrew = clsAdvancedSettings.GetBooleanSetting("DoFullCrews", True)
        ConfigOptions_Movie.bGenre = clsAdvancedSettings.GetBooleanSetting("DoGenres", True)
        ConfigOptions_Movie.bMPAA = clsAdvancedSettings.GetBooleanSetting("DoMPAA", True)
        ConfigOptions_Movie.bMusicBy = clsAdvancedSettings.GetBooleanSetting("DoMusic", True)
        ConfigOptions_Movie.bOriginalTitle = clsAdvancedSettings.GetBooleanSetting("DoOriginalTitle", True)
        ConfigOptions_Movie.bOtherCrew = clsAdvancedSettings.GetBooleanSetting("DoOtherCrews", True)
        ConfigOptions_Movie.bOutline = clsAdvancedSettings.GetBooleanSetting("DoOutline", True)
        ConfigOptions_Movie.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True)
        ConfigOptions_Movie.bProducers = clsAdvancedSettings.GetBooleanSetting("DoProducers", True)
        ConfigOptions_Movie.bRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True)
        ConfigOptions_Movie.bRelease = clsAdvancedSettings.GetBooleanSetting("DoRelease", True)
        ConfigOptions_Movie.bRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True)
        ConfigOptions_Movie.bStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True)
        ConfigOptions_Movie.bTagline = clsAdvancedSettings.GetBooleanSetting("DoTagline", True)
        ConfigOptions_Movie.bTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True)
        ConfigOptions_Movie.bTop250 = clsAdvancedSettings.GetBooleanSetting("DoTop250", True)
        ConfigOptions_Movie.bTrailer = clsAdvancedSettings.GetBooleanSetting("DoTrailer", True)
        ConfigOptions_Movie.bVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True)
        ConfigOptions_Movie.bWriters = clsAdvancedSettings.GetBooleanSetting("DoWriters", True)
        ConfigOptions_Movie.bYear = clsAdvancedSettings.GetBooleanSetting("DoYear", True)

        _SpecialSettings_Movie.CountryAbbreviation = clsAdvancedSettings.GetBooleanSetting("CountryAbbreviation", False)
        _SpecialSettings_Movie.FallBackWorldwide = clsAdvancedSettings.GetBooleanSetting("FallBackWorldwide", False)
        _SpecialSettings_Movie.ForceTitleLanguage = clsAdvancedSettings.GetSetting("ForceTitleLanguage", "")
        _SpecialSettings_Movie.SearchPartialTitles = clsAdvancedSettings.GetBooleanSetting("SearchPartialTitles", True)
        _SpecialSettings_Movie.SearchPopularTitles = clsAdvancedSettings.GetBooleanSetting("SearchPopularTitles", True)
        _SpecialSettings_Movie.SearchTvTitles = clsAdvancedSettings.GetBooleanSetting("SearchTvTitles", False)
        _SpecialSettings_Movie.SearchVideoTitles = clsAdvancedSettings.GetBooleanSetting("SearchVideoTitles", False)
        _SpecialSettings_Movie.SearchShortTitles = clsAdvancedSettings.GetBooleanSetting("SearchShortTitles", False)
        _SpecialSettings_Movie.StudiowithDistributors = clsAdvancedSettings.GetBooleanSetting("StudiowithDistributors", False)
    End Sub

    Sub LoadSettings_TV()
        ConfigOptions_TV.bEpActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpAired = clsAdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpCredits = clsAdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpGuestStars = clsAdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bEpVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.ContentType.TVEpisode)
        ConfigOptions_TV.bShowActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowCountry = clsAdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowCreator = clsAdvancedSettings.GetBooleanSetting("DoCreator", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowGenre = clsAdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowOriginalTitle = clsAdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowPremiered = clsAdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowStatus = clsAdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)
        ConfigOptions_TV.bShowVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.ContentType.TVShow)
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoCast", ConfigOptions_Movie.bCast)
            settings.SetBooleanSetting("DoCert", ConfigOptions_Movie.bCert)
            settings.SetBooleanSetting("DoCountry", ConfigOptions_Movie.bCountry)
            settings.SetBooleanSetting("DoDirector", ConfigOptions_Movie.bDirector)
            settings.SetBooleanSetting("DoFullCrews", ConfigOptions_Movie.bFullCrew)
            settings.SetBooleanSetting("DoGenres", ConfigOptions_Movie.bGenre)
            settings.SetBooleanSetting("DoMPAA", ConfigOptions_Movie.bMPAA)
            settings.SetBooleanSetting("DoMusic", ConfigOptions_Movie.bMusicBy)
            settings.SetBooleanSetting("DoOriginalTitle", ConfigOptions_Movie.bOriginalTitle)
            settings.SetBooleanSetting("DoOtherCrews", ConfigOptions_Movie.bOtherCrew)
            settings.SetBooleanSetting("DoOutline", ConfigOptions_Movie.bOutline)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_Movie.bPlot)
            settings.SetBooleanSetting("DoProducers", ConfigOptions_Movie.bProducers)
            settings.SetBooleanSetting("DoRating", ConfigOptions_Movie.bRating)
            settings.SetBooleanSetting("DoRelease", ConfigOptions_Movie.bRelease)
            settings.SetBooleanSetting("DoRuntime", ConfigOptions_Movie.bRuntime)
            settings.SetBooleanSetting("DoStudio", ConfigOptions_Movie.bStudio)
            settings.SetBooleanSetting("DoTagline", ConfigOptions_Movie.bTagline)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_Movie.bTitle)
            settings.SetBooleanSetting("DoTop250", ConfigOptions_Movie.bTop250)
            settings.SetBooleanSetting("DoTrailer", ConfigOptions_Movie.bTrailer)
            settings.SetBooleanSetting("DoVotes", ConfigOptions_Movie.bVotes)
            settings.SetBooleanSetting("DoWriters", ConfigOptions_Movie.bWriters)
            settings.SetBooleanSetting("DoYear", ConfigOptions_Movie.bYear)
            settings.SetBooleanSetting("CountryAbbreviation", _SpecialSettings_Movie.CountryAbbreviation)
            settings.SetBooleanSetting("FallBackWorldwide", _SpecialSettings_Movie.FallBackWorldwide)
            settings.SetBooleanSetting("SearchPartialTitles", _SpecialSettings_Movie.SearchPartialTitles)
            settings.SetBooleanSetting("SearchPopularTitles", _SpecialSettings_Movie.SearchPopularTitles)
            settings.SetBooleanSetting("SearchTvTitles", _SpecialSettings_Movie.SearchTvTitles)
            settings.SetBooleanSetting("SearchVideoTitles", _SpecialSettings_Movie.SearchVideoTitles)
            settings.SetBooleanSetting("SearchShortTitles", _SpecialSettings_Movie.SearchShortTitles)
            settings.SetSetting("ForceTitleLanguage", _SpecialSettings_Movie.ForceTitleLanguage)
            settings.SetBooleanSetting("StudiowithDistributors", _SpecialSettings_Movie.StudiowithDistributors)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoActors", ConfigOptions_TV.bEpActors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", ConfigOptions_TV.bEpAired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", ConfigOptions_TV.bEpCredits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", ConfigOptions_TV.bEpDirector, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", ConfigOptions_TV.bEpGuestStars, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_TV.bEpPlot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", ConfigOptions_TV.bEpRating, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_TV.bEpTitle, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoVotes", ConfigOptions_TV.bEpVotes, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoActors", ConfigOptions_TV.bShowActors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCert", ConfigOptions_TV.bShowCert, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCountry", ConfigOptions_TV.bShowCountry, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCreator", ConfigOptions_TV.bShowCreator, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", ConfigOptions_TV.bShowGenre, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoOriginalTitle", ConfigOptions_TV.bShowOriginalTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", ConfigOptions_TV.bShowPlot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", ConfigOptions_TV.bShowPremiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", ConfigOptions_TV.bShowRating, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStatus", ConfigOptions_TV.bShowStatus, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", ConfigOptions_TV.bShowStudio, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", ConfigOptions_TV.bShowTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoVotes", ConfigOptions_TV.bShowVotes, , , Enums.ContentType.TVShow)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        ConfigOptions_Movie.bCast = _setup_Movie.chkCast.Checked
        ConfigOptions_Movie.bCert = _setup_Movie.chkCertification.Checked
        ConfigOptions_Movie.bCountry = _setup_Movie.chkCountry.Checked
        ConfigOptions_Movie.bDirector = _setup_Movie.chkDirector.Checked
        ConfigOptions_Movie.bFullCrew = _setup_Movie.chkFullCrew.Checked
        ConfigOptions_Movie.bGenre = _setup_Movie.chkGenre.Checked
        ConfigOptions_Movie.bMPAA = _setup_Movie.chkMPAA.Checked
        ConfigOptions_Movie.bMusicBy = _setup_Movie.chkMusicBy.Checked
        ConfigOptions_Movie.bOriginalTitle = _setup_Movie.chkOriginalTitle.Checked
        ConfigOptions_Movie.bOtherCrew = _setup_Movie.chkCrew.Checked
        ConfigOptions_Movie.bOutline = _setup_Movie.chkOutline.Checked
        ConfigOptions_Movie.bPlot = _setup_Movie.chkPlot.Checked
        ConfigOptions_Movie.bProducers = _setup_Movie.chkProducers.Checked
        ConfigOptions_Movie.bRating = _setup_Movie.chkRating.Checked
        ConfigOptions_Movie.bRelease = _setup_Movie.chkRelease.Checked
        ConfigOptions_Movie.bRuntime = _setup_Movie.chkRuntime.Checked
        ConfigOptions_Movie.bStudio = _setup_Movie.chkStudio.Checked
        ConfigOptions_Movie.bTagline = _setup_Movie.chkTagline.Checked
        ConfigOptions_Movie.bTitle = _setup_Movie.chkTitle.Checked
        ConfigOptions_Movie.bTop250 = _setup_Movie.chkTop250.Checked
        ConfigOptions_Movie.bTrailer = _setup_Movie.chkTrailer.Checked
        ConfigOptions_Movie.bVotes = _setup_Movie.chkVotes.Checked
        ConfigOptions_Movie.bWriters = _setup_Movie.chkWriters.Checked
        ConfigOptions_Movie.bYear = _setup_Movie.chkYear.Checked

        _SpecialSettings_Movie.CountryAbbreviation = _setup_Movie.chkCountryAbbreviation.Checked
        _SpecialSettings_Movie.FallBackWorldwide = _setup_Movie.chkFallBackworldwide.Checked
        _SpecialSettings_Movie.ForceTitleLanguage = _setup_Movie.cbForceTitleLanguage.Text
        _SpecialSettings_Movie.SearchPartialTitles = _setup_Movie.chkPartialTitles.Checked
        _SpecialSettings_Movie.SearchPopularTitles = _setup_Movie.chkPopularTitles.Checked
        _SpecialSettings_Movie.SearchTvTitles = _setup_Movie.chkTvTitles.Checked
        _SpecialSettings_Movie.SearchVideoTitles = _setup_Movie.chkVideoTitles.Checked
        _SpecialSettings_Movie.SearchShortTitles = _setup_Movie.chkShortTitles.Checked
        _SpecialSettings_Movie.StudiowithDistributors = _setup_Movie.chkStudiowithDistributors.Checked

        SaveSettings_Movie()
        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            _setup_Movie.Dispose()
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
        SaveSettings_TV()
        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            _setup_TV.Dispose()
        End If
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        If (DBMovie.Movie Is Nothing OrElse String.IsNullOrEmpty(DBMovie.Movie.IMDBID)) Then
            logger.Error("Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If

        LoadSettings_Movie()
        Dim _scraper As New IMDB.Scraper(_SpecialSettings_Movie)

        studio.AddRange(_scraper.GetMovieStudios(DBMovie.Movie.IMDBID))
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDBID
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function Scraper_GetSingleEpisode(ByRef oDBTVEpisode As Database.DBElement, ByRef nEpisode As MediaContainers.EpisodeDetails, ByVal ScrapeOptions As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVEpisode
        logger.Trace("Started TMDB Scraper")

        'LoadSettings_TV()

        'Dim Settings As TMDB.Scraper.sMySettings_ForScraper
        'Settings.ApiKey = _MySettings_TV.APIKey
        'Settings.FallBackEng = _MySettings_TV.FallBackEng
        'Settings.GetAdultItems = _MySettings_TV.GetAdultItems
        'Settings.PrefLanguage = Lang

        'Dim _scraper As New TMDB.Scraper(Settings)
        'Dim filterOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions_TV)

        'If Ordering = Enums.Ordering.Standard Then
        '    nEpisode = _scraper.GetTVEpisodeInfo(CInt(_scraper.GetTMDBbyTVDB(TVDBID)), Season, Episode, filterOptions)
        'End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from IMDB
    ''' </summary>
    ''' <param name="oDBMovie">Movie to be scraped. oDBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper(ByRef oDBMovie As Database.DBElement, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.Scraper
        logger.Trace("Started IMDB Scraper")

        LoadSettings_Movie()
        Dim _scraper As New IMDB.Scraper(_SpecialSettings_Movie)

        Dim FilteredOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions_Movie)

        If ScrapeModifier.MainNFO AndAlso Not ScrapeModifier.DoSearch Then
            If Not String.IsNullOrEmpty(oDBMovie.Movie.IMDBID) Then
                'IMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                _scraper.GetMovieInfo(oDBMovie.Movie.IMDBID, nMovie, FilteredOptions.bFullCrew, False, FilteredOptions, False, _SpecialSettings_Movie.FallBackWorldwide, _SpecialSettings_Movie.ForceTitleLanguage, _SpecialSettings_Movie.CountryAbbreviation, _SpecialSettings_Movie.StudiowithDistributors)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID for movie --> search first!
                _scraper.GetSearchMovieInfo(oDBMovie.Movie.Title, oDBMovie.Movie.Year, oDBMovie, nMovie, ScrapeType, FilteredOptions, FilteredOptions.bFullCrew, _SpecialSettings_Movie.FallBackWorldwide, _SpecialSettings_Movie.ForceTitleLanguage, _SpecialSettings_Movie.CountryAbbreviation, _SpecialSettings_Movie.StudiowithDistributors)
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nMovie.IMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nMovie.IMDBID) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.MissAuto
                    nMovie = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If String.IsNullOrEmpty(oDBMovie.Movie.IMDBID) AndAlso String.IsNullOrEmpty(oDBMovie.Movie.TMDBID) Then
                Using dSearch As New dlgIMDBSearchResults_Movie(_SpecialSettings_Movie, _scraper)
                    If dSearch.ShowDialog(nMovie, oDBMovie.Movie.Title, oDBMovie.Movie.Year, oDBMovie.Filename, FilteredOptions) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetMovieInfo(nMovie.IMDBID, nMovie, FilteredOptions.bFullCrew, False, FilteredOptions, False, _SpecialSettings_Movie.FallBackWorldwide, _SpecialSettings_Movie.ForceTitleLanguage, _SpecialSettings_Movie.CountryAbbreviation, _SpecialSettings_Movie.StudiowithDistributors)
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

        logger.Trace("Finished IMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from IMDB
    ''' </summary>
    ''' <param name="oDBTV">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="nShow">New scraped TV Show data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_TV(ByRef oDBTV As Database.DBElement, ByRef nShow As MediaContainers.TVShow, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVShow
        logger.Trace("Started IMDB Scraper")

        LoadSettings_TV()
        Dim _scraper As New IMDB.Scraper(_SpecialSettings_TV)

        Dim FilteredOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions_TV)

        If ScrapeModifier.MainNFO AndAlso Not ScrapeModifier.DoSearch Then
            If Not String.IsNullOrEmpty(oDBTV.TVShow.IMDB) Then
                'IMDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                _scraper.GetTVShowInfo(oDBTV.TVShow.IMDB, nShow, False, FilteredOptions, False, ScrapeModifier.withEpisodes)
                'ElseIf Not String.IsNullOrEmpty(oDBTV.TVShow.TVDBID) Then
                '    'oDBTV.TVShow.TMDB = _scraper.GetTMDBbyTVDB(oDBTV.TVShow.TVDBID)
                '    'If String.IsNullOrEmpty(oDBTV.TVShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                '    '_scraper.GetTVShowInfo(oDBTV.TVShow.TMDB, nShow, False, filterOptions, False, withEpisodes)
                'ElseIf Not String.IsNullOrEmpty(oDBTV.TVShow.IMDB) Then
                '    'oDBTV.TVShow.TMDB = _scraper.GetTMDBbyIMDB(oDBTV.TVShow.IMDB)
                '    If String.IsNullOrEmpty(oDBTV.TVShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                '    _scraper.GetTVShowInfo(oDBTV.TVShow.TMDB, nShow, False, filterOptions, False, withEpisodes)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If Not String.IsNullOrEmpty(oDBTV.TVShow.Title) Then
                    _scraper.GetSearchTVShowInfo(oDBTV.TVShow.Title, oDBTV, nShow, ScrapeType, FilteredOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nShow.TMDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nShow.TMDB) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.MissAuto
                    nShow = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If String.IsNullOrEmpty(oDBTV.TVShow.IMDB) Then
                Using dSearch As New dlgIMDBSearchResults_TV(_SpecialSettings_TV, _scraper)
                    If dSearch.ShowDialog(nShow, oDBTV.TVShow.Title, oDBTV.ShowPath, FilteredOptions) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetTVShowInfo(nShow.IMDB, nShow, False, FilteredOptions, False, ScrapeModifier.withEpisodes)
                        'if a movie is found, set DoSearch back to "false" for following scrapers
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

        logger.Trace("Finished IMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements EmberAPI.Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_tv() Implements EmberAPI.Interfaces.ScraperModule_Data_TV.ScraperOrderChanged
        _setup_TV.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure SpecialSettings

#Region "Fields"

        Dim FallBackWorldwide As Boolean
        Dim ForceTitleLanguage As String
        Dim SearchPartialTitles As Boolean
        Dim SearchPopularTitles As Boolean
        Dim SearchTvTitles As Boolean
        Dim SearchVideoTitles As Boolean
        Dim SearchShortTitles As Boolean
        Dim CountryAbbreviation As Boolean
        Dim StudiowithDistributors As Boolean
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class