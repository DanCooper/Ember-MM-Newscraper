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

Public Class Movie
    Implements Interfaces.IScraperModule_Data_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigOptions As New Structures.ScrapeOptions
    Private _PnlSettingsPanel As frmSettingsPanel_Movie

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Data_Movie.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperModule_Data_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperModule_Data_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Data_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Data_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Data_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperModule_Data_Movie.StateChanged

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

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Data_Movie.GetTMDBID
        Return New Interfaces.ModuleResult
    End Function

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Data_Movie.GetMovieStudio
        If (DBMovie.Movie Is Nothing OrElse String.IsNullOrEmpty(DBMovie.Movie.UniqueIDs.IMDbId)) Then
            _Logger.Error("Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If

        Settings_Load()
        Dim _scraper As New IMDB.Scraper(_AddonSettings)

        studio.AddRange(_scraper.GetMovieStudios(DBMovie.Movie.UniqueIDs.IMDbId))
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub Init() Implements Interfaces.IScraperModule_Data_Movie.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Data_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Movie
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        _PnlSettingsPanel.chkActors.Checked = _ConfigOptions.bMainActors
        _PnlSettingsPanel.chkCertifications.Checked = _ConfigOptions.bMainCertifications
        _PnlSettingsPanel.chkCountries.Checked = _ConfigOptions.bMainCountries
        _PnlSettingsPanel.chkDirectors.Checked = _ConfigOptions.bMainDirectors
        _PnlSettingsPanel.chkGenres.Checked = _ConfigOptions.bMainGenres
        _PnlSettingsPanel.chkMPAA.Checked = _ConfigOptions.bMainMPAA
        _PnlSettingsPanel.chkOriginalTitle.Checked = _ConfigOptions.bMainOriginalTitle
        _PnlSettingsPanel.chkOutline.Checked = _ConfigOptions.bMainOutline
        _PnlSettingsPanel.chkPlot.Checked = _ConfigOptions.bMainPlot
        _PnlSettingsPanel.chkRating.Checked = _ConfigOptions.bMainRating
        _PnlSettingsPanel.chkRelease.Checked = _ConfigOptions.bMainRelease
        _PnlSettingsPanel.chkRuntime.Checked = _ConfigOptions.bMainRuntime
        _PnlSettingsPanel.chkStudios.Checked = _ConfigOptions.bMainStudios
        _PnlSettingsPanel.chkTagline.Checked = _ConfigOptions.bMainTagline
        _PnlSettingsPanel.chkTitle.Checked = _ConfigOptions.bMainTitle
        _PnlSettingsPanel.chkTop250.Checked = _ConfigOptions.bMainTop250
        _PnlSettingsPanel.chkTrailer.Checked = _ConfigOptions.bMainTrailer
        _PnlSettingsPanel.chkWriters.Checked = _ConfigOptions.bMainWriters
        _PnlSettingsPanel.chkYear.Checked = _ConfigOptions.bMainYear

        _PnlSettingsPanel.cbForceTitleLanguage.Text = _AddonSettings.ForceTitleLanguage
        _PnlSettingsPanel.chkCountryAbbreviation.Checked = _AddonSettings.CountryAbbreviation
        _PnlSettingsPanel.chkFallBackworldwide.Checked = _AddonSettings.FallBackWorldwide
        _PnlSettingsPanel.chkMPAADescription.Checked = _AddonSettings.MPAADescription
        _PnlSettingsPanel.chkPartialTitles.Checked = _AddonSettings.SearchPartialTitles
        _PnlSettingsPanel.chkPopularTitles.Checked = _AddonSettings.SearchPopularTitles
        _PnlSettingsPanel.chkTvTitles.Checked = _AddonSettings.SearchTvTitles
        _PnlSettingsPanel.chkVideoTitles.Checked = _AddonSettings.SearchVideoTitles
        _PnlSettingsPanel.chkShortTitles.Checked = _AddonSettings.SearchShortTitles
        _PnlSettingsPanel.chkStudiowithDistributors.Checked = _AddonSettings.StudiowithDistributors

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "IMDb.com",
            .Type = Enums.SettingsPanelType.MovieData
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Data_Movie.OrderChanged
        _PnlSettingsPanel.orderChanged(OrderState)
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from IMDB
    ''' </summary>
    ''' <param name="oDBElement">Movie to be scraped. oDBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_Movie Implements Interfaces.IScraperModule_Data_Movie.Run
        _Logger.Trace("[IMDB_Data] [Scraper_Movie] [Start]")

        Settings_Load()

        Dim nMovie As MediaContainers.Movie = Nothing
        _AddonSettings.PrefLanguage = oDBElement.Language
        Dim _scraper As New IMDB.Scraper(_AddonSettings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigOptions)

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If Not String.IsNullOrEmpty(oDBElement.Movie.UniqueIDs.IMDbId) Then
                'IMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                nMovie = _scraper.GetMovieInfo(oDBElement.Movie.UniqueIDs.IMDbId, False, FilteredOptions)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID for movie --> search first!
                nMovie = _scraper.GetSearchMovieInfo(oDBElement.Movie.Title, oDBElement.Movie.Year, oDBElement, ScrapeType, FilteredOptions)
                'if still no search result -> exit
                _Logger.Trace(String.Format("[IMDB_Data] [Scraper_Movie] [Abort] No search result found"))
                If nMovie Is Nothing Then Return New Interfaces.ModuleResult_Data_Movie With {.Result = Nothing}
            End If
        End If

        If nMovie Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    _Logger.Trace(String.Format("[IMDB_Data] [Scraper_Movie] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_Movie With {.Result = Nothing}
            End Select
        Else
            _Logger.Trace("[IMDB_Data] [Scraper_Movie] [Done]")
            Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBElement.Movie.UniqueIDs.IMDbIdSpecified AndAlso Not oDBElement.Movie.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults_Movie(_AddonSettings, _scraper)
                    If dlgSearch.ShowDialog(oDBElement.Movie.Title, oDBElement.Movie.Year, oDBElement.FileItem.FirstPathFromStack, FilteredOptions) = DialogResult.OK Then
                        nMovie = _scraper.GetMovieInfo(dlgSearch.Result.UniqueIDs.IMDbId, False, FilteredOptions)
                        'if a movie is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        _Logger.Trace(String.Format("[IMDB_Data] [Scraper_Movie] [Cancelled] Cancelled by user"))
                        Return New Interfaces.ModuleResult_Data_Movie With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        _Logger.Trace("[IMDB_Data] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Data_Movie.SaveSetup
        _ConfigOptions.bMainActors = _PnlSettingsPanel.chkActors.Checked
        _ConfigOptions.bMainCertifications = _PnlSettingsPanel.chkCertifications.Checked
        _ConfigOptions.bMainCountries = _PnlSettingsPanel.chkCountries.Checked
        _ConfigOptions.bMainDirectors = _PnlSettingsPanel.chkDirectors.Checked
        _ConfigOptions.bMainGenres = _PnlSettingsPanel.chkGenres.Checked
        _ConfigOptions.bMainMPAA = _PnlSettingsPanel.chkMPAA.Checked
        _ConfigOptions.bMainOriginalTitle = _PnlSettingsPanel.chkOriginalTitle.Checked
        _ConfigOptions.bMainOutline = _PnlSettingsPanel.chkOutline.Checked
        _ConfigOptions.bMainPlot = _PnlSettingsPanel.chkPlot.Checked
        _ConfigOptions.bMainRating = _PnlSettingsPanel.chkRating.Checked
        _ConfigOptions.bMainRelease = _PnlSettingsPanel.chkRelease.Checked
        _ConfigOptions.bMainRuntime = _PnlSettingsPanel.chkRuntime.Checked
        _ConfigOptions.bMainStudios = _PnlSettingsPanel.chkStudios.Checked
        _ConfigOptions.bMainTagline = _PnlSettingsPanel.chkTagline.Checked
        _ConfigOptions.bMainTitle = _PnlSettingsPanel.chkTitle.Checked
        _ConfigOptions.bMainTop250 = _PnlSettingsPanel.chkTop250.Checked
        _ConfigOptions.bMainTrailer = _PnlSettingsPanel.chkTrailer.Checked
        _ConfigOptions.bMainWriters = _PnlSettingsPanel.chkWriters.Checked
        _ConfigOptions.bMainYear = _PnlSettingsPanel.chkYear.Checked

        _AddonSettings.CountryAbbreviation = _PnlSettingsPanel.chkCountryAbbreviation.Checked
        _AddonSettings.FallBackWorldwide = _PnlSettingsPanel.chkFallBackworldwide.Checked
        _AddonSettings.ForceTitleLanguage = _PnlSettingsPanel.cbForceTitleLanguage.Text
        _AddonSettings.MPAADescription = _PnlSettingsPanel.chkMPAADescription.Checked
        _AddonSettings.SearchPartialTitles = _PnlSettingsPanel.chkPartialTitles.Checked
        _AddonSettings.SearchPopularTitles = _PnlSettingsPanel.chkPopularTitles.Checked
        _AddonSettings.SearchTvTitles = _PnlSettingsPanel.chkTvTitles.Checked
        _AddonSettings.SearchVideoTitles = _PnlSettingsPanel.chkVideoTitles.Checked
        _AddonSettings.SearchShortTitles = _PnlSettingsPanel.chkShortTitles.Checked
        _AddonSettings.StudiowithDistributors = _PnlSettingsPanel.chkStudiowithDistributors.Checked

        Settings_Save()

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
        _ConfigOptions.bMainActors = AdvancedSettings.GetBooleanSetting("DoCast", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainCertifications = AdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainCountries = AdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainDirectors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainGenres = AdvancedSettings.GetBooleanSetting("DoGenres", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainMPAA = AdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainOriginalTitle = AdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainOutline = AdvancedSettings.GetBooleanSetting("DoOutline", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainRelease = AdvancedSettings.GetBooleanSetting("DoRelease", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainStudios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainTagline = AdvancedSettings.GetBooleanSetting("DoTagline", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainTop250 = AdvancedSettings.GetBooleanSetting("DoTop250", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainWriters = AdvancedSettings.GetBooleanSetting("DoWriters", True, , Enums.ContentType.Movie)
        _ConfigOptions.bMainYear = AdvancedSettings.GetBooleanSetting("DoYear", True, , Enums.ContentType.Movie)

        _AddonSettings.CountryAbbreviation = AdvancedSettings.GetBooleanSetting("CountryAbbreviation", False, , Enums.ContentType.Movie)
        _AddonSettings.FallBackWorldwide = AdvancedSettings.GetBooleanSetting("FallBackWorldwide", False, , Enums.ContentType.Movie)
        _AddonSettings.ForceTitleLanguage = AdvancedSettings.GetSetting("ForceTitleLanguage", String.Empty, , Enums.ContentType.Movie)
        _AddonSettings.MPAADescription = AdvancedSettings.GetBooleanSetting("MPAADescription", False, , Enums.ContentType.Movie)
        _AddonSettings.SearchPartialTitles = AdvancedSettings.GetBooleanSetting("SearchPartialTitles", True, , Enums.ContentType.Movie)
        _AddonSettings.SearchPopularTitles = AdvancedSettings.GetBooleanSetting("SearchPopularTitles", True, , Enums.ContentType.Movie)
        _AddonSettings.SearchTvTitles = AdvancedSettings.GetBooleanSetting("SearchTvTitles", False, , Enums.ContentType.Movie)
        _AddonSettings.SearchVideoTitles = AdvancedSettings.GetBooleanSetting("SearchVideoTitles", False, , Enums.ContentType.Movie)
        _AddonSettings.SearchShortTitles = AdvancedSettings.GetBooleanSetting("SearchShortTitles", False, , Enums.ContentType.Movie)
        _AddonSettings.StudiowithDistributors = AdvancedSettings.GetBooleanSetting("StudiowithDistributors", False, , Enums.ContentType.Movie)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoCast", _ConfigOptions.bMainActors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCert", _ConfigOptions.bMainCertifications, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCountry", _ConfigOptions.bMainCountries, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoDirector", _ConfigOptions.bMainDirectors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoGenres", _ConfigOptions.bMainGenres, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoMPAA", _ConfigOptions.bMainMPAA, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOriginalTitle", _ConfigOptions.bMainOriginalTitle, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOutline", _ConfigOptions.bMainOutline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPlot", _ConfigOptions.bMainPlot, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRating", _ConfigOptions.bMainRating, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRelease", _ConfigOptions.bMainRelease, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRuntime", _ConfigOptions.bMainRuntime, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoStudio", _ConfigOptions.bMainStudios, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTagline", _ConfigOptions.bMainTagline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTitle", _ConfigOptions.bMainTitle, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTop250", _ConfigOptions.bMainTop250, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTrailer", _ConfigOptions.bMainTrailer, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoWriters", _ConfigOptions.bMainWriters, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoYear", _ConfigOptions.bMainYear, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("CountryAbbreviation", _AddonSettings.CountryAbbreviation, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("FallBackWorldwide", _AddonSettings.FallBackWorldwide, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("MPAADescription", _AddonSettings.MPAADescription, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("SearchPartialTitles", _AddonSettings.SearchPartialTitles, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("SearchPopularTitles", _AddonSettings.SearchPopularTitles, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("SearchTvTitles", _AddonSettings.SearchTvTitles, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("SearchVideoTitles", _AddonSettings.SearchVideoTitles, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("SearchShortTitles", _AddonSettings.SearchShortTitles, , , Enums.ContentType.Movie)
            settings.SetSetting("ForceTitleLanguage", _AddonSettings.ForceTitleLanguage, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("StudiowithDistributors", _AddonSettings.StudiowithDistributors, , , Enums.ContentType.Movie)
        End Using
    End Sub

#End Region 'Methods 

End Class

Public Class TV
    Implements Interfaces.IScraperModule_Data_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigOptions As New Structures.ScrapeOptions
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_TV

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperModule_Data_TV.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperModule_Data_TV.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperModule_Data_TV.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Data_TV.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Data_TV.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Data_TV.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperModule_Data_TV.StateChanged

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

    Public Sub Init() Implements Interfaces.IScraperModule_Data_TV.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperModule_Data_TV.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        _PnlSettingsPanel.chkScraperEpActors.Checked = _ConfigOptions.bEpisodeActors
        _PnlSettingsPanel.chkScraperEpAired.Checked = _ConfigOptions.bEpisodeAired
        _PnlSettingsPanel.chkScraperEpCredits.Checked = _ConfigOptions.bEpisodeCredits
        _PnlSettingsPanel.chkScraperEpDirectors.Checked = _ConfigOptions.bEpisodeDirectors
        _PnlSettingsPanel.chkScraperEpPlot.Checked = _ConfigOptions.bEpisodePlot
        _PnlSettingsPanel.chkScraperEpRating.Checked = _ConfigOptions.bEpisodeRating
        _PnlSettingsPanel.chkScraperEpTitle.Checked = _ConfigOptions.bEpisodeTitle
        _PnlSettingsPanel.chkScraperShowActors.Checked = _ConfigOptions.bMainActors
        _PnlSettingsPanel.chkScraperShowCertifications.Checked = _ConfigOptions.bMainCertifications
        _PnlSettingsPanel.chkScraperShowCountries.Checked = _ConfigOptions.bMainCountries
        _PnlSettingsPanel.chkScraperShowCreators.Checked = _ConfigOptions.bMainCreators
        _PnlSettingsPanel.chkScraperShowGenres.Checked = _ConfigOptions.bMainGenres
        _PnlSettingsPanel.chkScraperShowOriginalTitle.Checked = _ConfigOptions.bMainOriginalTitle
        _PnlSettingsPanel.chkScraperShowPlot.Checked = _ConfigOptions.bMainPlot
        _PnlSettingsPanel.chkScraperShowPremiered.Checked = _ConfigOptions.bMainPremiered
        _PnlSettingsPanel.chkScraperShowRating.Checked = _ConfigOptions.bMainRating
        _PnlSettingsPanel.chkScraperShowRuntime.Checked = _ConfigOptions.bMainRuntime
        _PnlSettingsPanel.chkScraperShowStudios.Checked = _ConfigOptions.bMainStudios
        _PnlSettingsPanel.chkScraperShowTitle.Checked = _ConfigOptions.bMainTitle

        _PnlSettingsPanel.cbForceTitleLanguage.Text = _AddonSettings.ForceTitleLanguage
        _PnlSettingsPanel.chkFallBackworldwide.Checked = _AddonSettings.FallBackWorldwide

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "IMDb.com",
            .Type = Enums.SettingsPanelType.TVData
            }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperModule_Data_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function Run_TVEpisode(ByRef oDBTVEpisode As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.IScraperModule_Data_TV.Run_TVEpisode
        _Logger.Trace("[IMDB_Data] [Scraper_TVEpisode] [Start]")

        Settings_Load()

        Dim nTVEpisode As New MediaContainers.EpisodeDetails
        _AddonSettings.PrefLanguage = oDBTVEpisode.Language
        Dim _scraper As New IMDB.Scraper(_AddonSettings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigOptions)

        If oDBTVEpisode.TVEpisode.UniqueIDs.IMDbIdSpecified Then
            nTVEpisode = _scraper.GetTVEpisodeInfo(oDBTVEpisode.TVEpisode.UniqueIDs.IMDbId, FilteredOptions)
        ElseIf oDBTVEpisode.TVShow.UniqueIDs.IMDbIdSpecified AndAlso oDBTVEpisode.TVEpisode.SeasonSpecified AndAlso oDBTVEpisode.TVEpisode.EpisodeSpecified Then
            nTVEpisode = _scraper.GetTVEpisodeInfo(oDBTVEpisode.TVShow.UniqueIDs.IMDbId, oDBTVEpisode.TVEpisode.Season, oDBTVEpisode.TVEpisode.Episode, FilteredOptions)
        Else
            _Logger.Trace("[IMDB_Data] [Scraper_TVEpisode] [Abort] No Episode and TV Show IMDB ID available")
            Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
        End If

        _Logger.Trace("[IMDB_Data] [Scraper_TVEpisode] [Done]")
        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = nTVEpisode}
    End Function

    Public Function Run_TVSeason(ByRef oDBTVSeason As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.IScraperModule_Data_TV.Run_TVSeason
        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from IMDB
    ''' </summary>
    ''' <param name="oDBElement">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run_TVShow(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.IScraperModule_Data_TV.Run_TVShow
        _Logger.Trace("[IMDB_Data] [Scraper_TV] [Start]")

        Settings_Load()

        Dim nTVShow As MediaContainers.TVShow = Nothing
        _AddonSettings.PrefLanguage = oDBElement.Language
        Dim _scraper As New IMDB.Scraper(_AddonSettings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigOptions)

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBElement.TVShow.UniqueIDs.IMDbIdSpecified Then
                'IMDB-ID already available -> scrape and save data into an empty tvshow container (nTVShow)
                nTVShow = _scraper.GetTVShowInfo(oDBElement.TVShow.UniqueIDs.IMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID for tvshow --> search first!
                nTVShow = _scraper.GetSearchTVShowInfo(oDBElement.TVShow.Title, oDBElement, ScrapeType, ScrapeModifiers, FilteredOptions)
                'if still no search result -> exit
                _Logger.Trace(String.Format("[IMDB_Data] [Scraper_TV] [Abort] No search result found"))
                If nTVShow Is Nothing Then Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
            End If
        End If

        If nTVShow Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    _Logger.Trace(String.Format("[IMDB_Data] [Scraper_TV] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
            End Select
        Else
            _Logger.Trace("[IMDB_Data] [Scraper_TV] [Done]")
            Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBElement.TVShow.UniqueIDs.IMDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults_TV(_AddonSettings, _scraper)
                    If dlgSearch.ShowDialog(oDBElement.TVShow.Title, oDBElement.ShowPath, ScrapeModifiers, FilteredOptions) = DialogResult.OK Then
                        nTVShow = _scraper.GetTVShowInfo(dlgSearch.Result.UniqueIDs.IMDbId, ScrapeModifiers, FilteredOptions, False)
                        'if a tvshow is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        _Logger.Trace(String.Format("[IMDB_Data] [Scraper_TV] [Cancelled] Cancelled by user"))
                        Return New Interfaces.ModuleResult_Data_TVShow With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        _Logger.Trace("[IMDB_Data] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Data_TV.SaveSetup
        _ConfigOptions.bEpisodeActors = _PnlSettingsPanel.chkScraperEpActors.Checked
        _ConfigOptions.bEpisodeAired = _PnlSettingsPanel.chkScraperEpAired.Checked
        _ConfigOptions.bEpisodeCredits = _PnlSettingsPanel.chkScraperEpCredits.Checked
        _ConfigOptions.bEpisodeDirectors = _PnlSettingsPanel.chkScraperEpDirectors.Checked
        _ConfigOptions.bEpisodePlot = _PnlSettingsPanel.chkScraperEpPlot.Checked
        _ConfigOptions.bEpisodeRating = _PnlSettingsPanel.chkScraperEpRating.Checked
        _ConfigOptions.bEpisodeTitle = _PnlSettingsPanel.chkScraperEpTitle.Checked
        _ConfigOptions.bMainActors = _PnlSettingsPanel.chkScraperShowActors.Checked
        _ConfigOptions.bMainCertifications = _PnlSettingsPanel.chkScraperShowCertifications.Checked
        _ConfigOptions.bMainCountries = _PnlSettingsPanel.chkScraperShowCountries.Checked
        _ConfigOptions.bMainCreators = _PnlSettingsPanel.chkScraperShowCreators.Checked
        _ConfigOptions.bMainGenres = _PnlSettingsPanel.chkScraperShowGenres.Checked
        _ConfigOptions.bMainOriginalTitle = _PnlSettingsPanel.chkScraperShowOriginalTitle.Checked
        _ConfigOptions.bMainPlot = _PnlSettingsPanel.chkScraperShowPlot.Checked
        _ConfigOptions.bMainPremiered = _PnlSettingsPanel.chkScraperShowPremiered.Checked
        _ConfigOptions.bMainRating = _PnlSettingsPanel.chkScraperShowRating.Checked
        _ConfigOptions.bMainRuntime = _PnlSettingsPanel.chkScraperShowRuntime.Checked
        _ConfigOptions.bMainStudios = _PnlSettingsPanel.chkScraperShowStudios.Checked
        _ConfigOptions.bMainTitle = _PnlSettingsPanel.chkScraperShowTitle.Checked

        _AddonSettings.FallBackWorldwide = _PnlSettingsPanel.chkFallBackworldwide.Checked
        _AddonSettings.ForceTitleLanguage = _PnlSettingsPanel.cbForceTitleLanguage.Text

        Settings_Save()
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
        _ConfigOptions.bEpisodeActors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeAired = AdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeCredits = AdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeDirectors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodePlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bEpisodeTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.bMainActors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainCertifications = AdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainCountries = AdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainCreators = AdvancedSettings.GetBooleanSetting("DoCreator", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainGenres = AdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainOriginalTitle = AdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainPremiered = AdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainRating = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainStudios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        _ConfigOptions.bMainTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)

        _AddonSettings.FallBackWorldwide = AdvancedSettings.GetBooleanSetting("FallBackWorldwide", False, , Enums.ContentType.TVShow)
        _AddonSettings.ForceTitleLanguage = AdvancedSettings.GetSetting("ForceTitleLanguage", String.Empty, , Enums.ContentType.TVShow)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoActors", _ConfigOptions.bEpisodeActors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", _ConfigOptions.bEpisodeAired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", _ConfigOptions.bEpisodeCredits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", _ConfigOptions.bEpisodeDirectors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", _ConfigOptions.bEpisodePlot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", _ConfigOptions.bEpisodeRating, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", _ConfigOptions.bEpisodeTitle, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoActors", _ConfigOptions.bMainActors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCert", _ConfigOptions.bMainCertifications, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCountry", _ConfigOptions.bMainCountries, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCreator", _ConfigOptions.bMainCreators, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", _ConfigOptions.bMainGenres, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoOriginalTitle", _ConfigOptions.bMainOriginalTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", _ConfigOptions.bMainPlot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", _ConfigOptions.bMainPremiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", _ConfigOptions.bMainRating, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRuntime", _ConfigOptions.bMainRuntime, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", _ConfigOptions.bMainStudios, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", _ConfigOptions.bMainTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("FallBackWorldwide", _AddonSettings.FallBackWorldwide, , , Enums.ContentType.TVShow)
            settings.SetSetting("ForceTitleLanguage", _AddonSettings.ForceTitleLanguage, , , Enums.ContentType.TVShow)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Structure AddonSettings

#Region "Fields"

    Dim CountryAbbreviation As Boolean
    Dim FallBackWorldwide As Boolean
    Dim ForceTitleLanguage As String
    Dim MPAADescription As Boolean
    Dim PrefLanguage As String
    Dim SearchPartialTitles As Boolean
    Dim SearchPopularTitles As Boolean
    Dim SearchTvTitles As Boolean
    Dim SearchVideoTitles As Boolean
    Dim SearchShortTitles As Boolean
    Dim StudiowithDistributors As Boolean

#End Region 'Fields

End Structure