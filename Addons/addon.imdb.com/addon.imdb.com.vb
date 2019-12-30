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

Public Class Data_Movie
    Implements Interfaces.IScraperAddon_Data_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigScrapeModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigScrapeOptions As New Structures.ScrapeOptions
    Private _PnlSettingsPanel As frmSettingsPanel_Data_Movie

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Data_Movie.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperAddon_Data_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Data_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Data_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Data_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Data_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Data_Movie.StateChanged

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

    Public Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef Studios As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Data_Movie.GetMovieStudio
        If (DBMovie.MainDetails Is Nothing OrElse String.IsNullOrEmpty(DBMovie.MainDetails.UniqueIDs.IMDbId)) Then
            _Logger.Error("Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If

        Settings_Load()
        Dim _scraper As New Scraper(_AddonSettings)

        Studios.AddRange(_scraper.GetMovieStudios(DBMovie.MainDetails.UniqueIDs.IMDbId))
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function GetTMDbID(ByVal IMDbID As String, ByRef TMDbID As String) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Data_Movie.GetTMDbID
        Return New Interfaces.ModuleResult
    End Function

    Public Sub Init() Implements Interfaces.IScraperAddon_Data_Movie.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Data_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Data_Movie
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        _PnlSettingsPanel.chkActors.Checked = _ConfigScrapeOptions.Actors
        _PnlSettingsPanel.chkCertifications.Checked = _ConfigScrapeOptions.Certifications
        _PnlSettingsPanel.chkCountries.Checked = _ConfigScrapeOptions.Countries
        _PnlSettingsPanel.chkDirectors.Checked = _ConfigScrapeOptions.Directors
        _PnlSettingsPanel.chkGenres.Checked = _ConfigScrapeOptions.Genres
        _PnlSettingsPanel.chkMPAA.Checked = _ConfigScrapeOptions.MPAA
        _PnlSettingsPanel.chkOriginalTitle.Checked = _ConfigScrapeOptions.OriginalTitle
        _PnlSettingsPanel.chkOutline.Checked = _ConfigScrapeOptions.Outline
        _PnlSettingsPanel.chkPlot.Checked = _ConfigScrapeOptions.Plot
        _PnlSettingsPanel.chkPremiered.Checked = _ConfigScrapeOptions.Premiered
        _PnlSettingsPanel.chkRating.Checked = _ConfigScrapeOptions.Ratings
        _PnlSettingsPanel.chkRuntime.Checked = _ConfigScrapeOptions.Runtime
        _PnlSettingsPanel.chkStudios.Checked = _ConfigScrapeOptions.Studios
        _PnlSettingsPanel.chkTagline.Checked = _ConfigScrapeOptions.Tagline
        _PnlSettingsPanel.chkTitle.Checked = _ConfigScrapeOptions.Title
        _PnlSettingsPanel.chkTop250.Checked = _ConfigScrapeOptions.Top250
        _PnlSettingsPanel.chkTrailer.Checked = _ConfigScrapeOptions.Trailer
        _PnlSettingsPanel.chkWriters.Checked = _ConfigScrapeOptions.Credits

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

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Data_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from IMDB
    ''' </summary>
    ''' <param name="oDBElement">Movie to be scraped. oDBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_Movie Implements Interfaces.IScraperAddon_Data_Movie.Run
        _Logger.Trace("[IMDB_Data] [Scraper_Movie] [Start]")

        Settings_Load()

        Dim nMovie As MediaContainers.MainDetails = Nothing
        _AddonSettings.PrefLanguage = oDBElement.Language
        Dim _scraper As New Scraper(_AddonSettings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If Not String.IsNullOrEmpty(oDBElement.MainDetails.UniqueIDs.IMDbId) Then
                'IMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                nMovie = _scraper.GetMovieInfo(oDBElement.MainDetails.UniqueIDs.IMDbId, False, FilteredOptions)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID for movie --> search first!
                nMovie = _scraper.GetSearchMovieInfo(oDBElement.MainDetails.Title, StringUtils.GetYearFromString(oDBElement.MainDetails.Premiered), oDBElement, ScrapeType, FilteredOptions)
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
            If Not oDBElement.MainDetails.UniqueIDs.IMDbIdSpecified AndAlso Not oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults_Movie(_AddonSettings, _scraper)
                    If dlgSearch.ShowDialog(oDBElement.MainDetails.Title, StringUtils.GetYearFromString(oDBElement.MainDetails.Premiered), oDBElement.FileItem.FirstPathFromStack, FilteredOptions) = DialogResult.OK Then
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

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Data_Movie.SaveSetup
        _ConfigScrapeOptions.Actors = _PnlSettingsPanel.chkActors.Checked
        _ConfigScrapeOptions.Certifications = _PnlSettingsPanel.chkCertifications.Checked
        _ConfigScrapeOptions.Countries = _PnlSettingsPanel.chkCountries.Checked
        _ConfigScrapeOptions.Directors = _PnlSettingsPanel.chkDirectors.Checked
        _ConfigScrapeOptions.Genres = _PnlSettingsPanel.chkGenres.Checked
        _ConfigScrapeOptions.MPAA = _PnlSettingsPanel.chkMPAA.Checked
        _ConfigScrapeOptions.OriginalTitle = _PnlSettingsPanel.chkOriginalTitle.Checked
        _ConfigScrapeOptions.Outline = _PnlSettingsPanel.chkOutline.Checked
        _ConfigScrapeOptions.Plot = _PnlSettingsPanel.chkPlot.Checked
        _ConfigScrapeOptions.Premiered = _PnlSettingsPanel.chkPremiered.Checked
        _ConfigScrapeOptions.Ratings = _PnlSettingsPanel.chkRating.Checked
        _ConfigScrapeOptions.Runtime = _PnlSettingsPanel.chkRuntime.Checked
        _ConfigScrapeOptions.Studios = _PnlSettingsPanel.chkStudios.Checked
        _ConfigScrapeOptions.Tagline = _PnlSettingsPanel.chkTagline.Checked
        _ConfigScrapeOptions.Title = _PnlSettingsPanel.chkTitle.Checked
        _ConfigScrapeOptions.Top250 = _PnlSettingsPanel.chkTop250.Checked
        _ConfigScrapeOptions.Trailer = _PnlSettingsPanel.chkTrailer.Checked
        _ConfigScrapeOptions.Credits = _PnlSettingsPanel.chkWriters.Checked

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
        _ConfigScrapeOptions.Actors = AdvancedSettings.GetBooleanSetting("DoCast", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Certifications = AdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Countries = AdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Directors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Genres = AdvancedSettings.GetBooleanSetting("DoGenres", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.MPAA = AdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.OriginalTitle = AdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Outline = AdvancedSettings.GetBooleanSetting("DoOutline", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Premiered = AdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Ratings = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Runtime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Studios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Tagline = AdvancedSettings.GetBooleanSetting("DoTagline", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Top250 = AdvancedSettings.GetBooleanSetting("DoTop250", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Trailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Credits = AdvancedSettings.GetBooleanSetting("DoWriters", True, , Enums.ContentType.Movie)

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
            settings.SetBooleanSetting("DoCast", _ConfigScrapeOptions.Actors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCert", _ConfigScrapeOptions.Certifications, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCountry", _ConfigScrapeOptions.Countries, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoDirector", _ConfigScrapeOptions.Directors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoGenres", _ConfigScrapeOptions.Genres, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoMPAA", _ConfigScrapeOptions.MPAA, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOriginalTitle", _ConfigScrapeOptions.OriginalTitle, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOutline", _ConfigScrapeOptions.Outline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPlot", _ConfigScrapeOptions.Plot, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPremiered", _ConfigScrapeOptions.Premiered, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.Ratings, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRuntime", _ConfigScrapeOptions.Runtime, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoStudio", _ConfigScrapeOptions.Studios, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTagline", _ConfigScrapeOptions.Tagline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTitle", _ConfigScrapeOptions.Title, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTop250", _ConfigScrapeOptions.Top250, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTrailer", _ConfigScrapeOptions.Trailer, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoWriters", _ConfigScrapeOptions.Credits, , , Enums.ContentType.Movie)
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

Public Class Data_TV
    Implements Interfaces.IScraperAddon_Data_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigOptions As New Structures.ScrapeOptions
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_Data_TV

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Data_TV.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperAddon_Data_TV.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Data_TV.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Data_TV.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Data_TV.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Data_TV.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Data_TV.StateChanged

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

    Public Sub Init() Implements Interfaces.IScraperAddon_Data_TV.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Data_TV.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Data_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        _PnlSettingsPanel.chkScraperEpActors.Checked = _ConfigOptions.Episodes.Actors
        _PnlSettingsPanel.chkScraperEpAired.Checked = _ConfigOptions.Episodes.Aired
        _PnlSettingsPanel.chkScraperEpCredits.Checked = _ConfigOptions.Episodes.Credits
        _PnlSettingsPanel.chkScraperEpDirectors.Checked = _ConfigOptions.Episodes.Directors
        _PnlSettingsPanel.chkScraperEpPlot.Checked = _ConfigOptions.Episodes.Plot
        _PnlSettingsPanel.chkScraperEpRating.Checked = _ConfigOptions.Episodes.Ratings
        _PnlSettingsPanel.chkScraperEpTitle.Checked = _ConfigOptions.Episodes.Title
        _PnlSettingsPanel.chkScraperShowActors.Checked = _ConfigOptions.Actors
        _PnlSettingsPanel.chkScraperShowCertifications.Checked = _ConfigOptions.Certifications
        _PnlSettingsPanel.chkScraperShowCountries.Checked = _ConfigOptions.Countries
        _PnlSettingsPanel.chkScraperShowCreators.Checked = _ConfigOptions.Creators
        _PnlSettingsPanel.chkScraperShowGenres.Checked = _ConfigOptions.Genres
        _PnlSettingsPanel.chkScraperShowOriginalTitle.Checked = _ConfigOptions.OriginalTitle
        _PnlSettingsPanel.chkScraperShowPlot.Checked = _ConfigOptions.Plot
        _PnlSettingsPanel.chkScraperShowPremiered.Checked = _ConfigOptions.Premiered
        _PnlSettingsPanel.chkScraperShowRating.Checked = _ConfigOptions.Ratings
        _PnlSettingsPanel.chkScraperShowRuntime.Checked = _ConfigOptions.Runtime
        _PnlSettingsPanel.chkScraperShowStudios.Checked = _ConfigOptions.Studios
        _PnlSettingsPanel.chkScraperShowTitle.Checked = _ConfigOptions.Title

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

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Data_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function Run_TVEpisode(ByRef oDBTVEpisode As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.IScraperAddon_Data_TV.Run_TVEpisode
        _Logger.Trace("[IMDB_Data] [Scraper_TVEpisode] [Start]")

        Settings_Load()

        Dim nTVEpisode As New MediaContainers.MainDetails
        _AddonSettings.PrefLanguage = oDBTVEpisode.Language
        Dim _scraper As New Scraper(_AddonSettings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigOptions)

        If oDBTVEpisode.MainDetails.UniqueIDs.IMDbIdSpecified Then
            nTVEpisode = _scraper.GetTVEpisodeInfo(oDBTVEpisode.MainDetails.UniqueIDs.IMDbId, FilteredOptions)
        ElseIf oDBTVEpisode.TVShowDetails.UniqueIDs.IMDbIdSpecified AndAlso oDBTVEpisode.MainDetails.SeasonSpecified AndAlso oDBTVEpisode.MainDetails.EpisodeSpecified Then
            nTVEpisode = _scraper.GetTVEpisodeInfo(oDBTVEpisode.TVShowDetails.UniqueIDs.IMDbId, oDBTVEpisode.MainDetails.Season, oDBTVEpisode.MainDetails.Episode, FilteredOptions)
        Else
            _Logger.Trace("[IMDB_Data] [Scraper_TVEpisode] [Abort] No Episode and TV Show IMDB ID available")
            Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
        End If

        _Logger.Trace("[IMDB_Data] [Scraper_TVEpisode] [Done]")
        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = nTVEpisode}
    End Function

    Public Function Run_TVSeason(ByRef oDBTVSeason As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.IScraperAddon_Data_TV.Run_TVSeason
        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from IMDB
    ''' </summary>
    ''' <param name="oDBElement">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run_TVShow(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.IScraperAddon_Data_TV.Run_TVShow
        _Logger.Trace("[IMDB_Data] [Scraper_TV] [Start]")

        Settings_Load()

        Dim nTVShow As MediaContainers.MainDetails = Nothing
        _AddonSettings.PrefLanguage = oDBElement.Language
        Dim _scraper As New Scraper(_AddonSettings)
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigOptions)

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                'IMDB-ID already available -> scrape and save data into an empty tvshow container (nTVShow)
                nTVShow = _scraper.GetTVShowInfo(oDBElement.MainDetails.UniqueIDs.IMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID for tvshow --> search first!
                nTVShow = _scraper.GetSearchTVShowInfo(oDBElement.MainDetails.Title, oDBElement, ScrapeType, ScrapeModifiers, FilteredOptions)
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
            If Not oDBElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults_TV(_AddonSettings, _scraper)
                    If dlgSearch.ShowDialog(oDBElement.MainDetails.Title, oDBElement.ShowPath, ScrapeModifiers, FilteredOptions) = DialogResult.OK Then
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

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Data_TV.SaveSetup
        _ConfigOptions.Episodes.Actors = _PnlSettingsPanel.chkScraperEpActors.Checked
        _ConfigOptions.Episodes.Aired = _PnlSettingsPanel.chkScraperEpAired.Checked
        _ConfigOptions.Episodes.Credits = _PnlSettingsPanel.chkScraperEpCredits.Checked
        _ConfigOptions.Episodes.Directors = _PnlSettingsPanel.chkScraperEpDirectors.Checked
        _ConfigOptions.Episodes.Plot = _PnlSettingsPanel.chkScraperEpPlot.Checked
        _ConfigOptions.Episodes.Ratings = _PnlSettingsPanel.chkScraperEpRating.Checked
        _ConfigOptions.Episodes.Title = _PnlSettingsPanel.chkScraperEpTitle.Checked
        _ConfigOptions.Actors = _PnlSettingsPanel.chkScraperShowActors.Checked
        _ConfigOptions.Certifications = _PnlSettingsPanel.chkScraperShowCertifications.Checked
        _ConfigOptions.Countries = _PnlSettingsPanel.chkScraperShowCountries.Checked
        _ConfigOptions.Creators = _PnlSettingsPanel.chkScraperShowCreators.Checked
        _ConfigOptions.Genres = _PnlSettingsPanel.chkScraperShowGenres.Checked
        _ConfigOptions.OriginalTitle = _PnlSettingsPanel.chkScraperShowOriginalTitle.Checked
        _ConfigOptions.Plot = _PnlSettingsPanel.chkScraperShowPlot.Checked
        _ConfigOptions.Premiered = _PnlSettingsPanel.chkScraperShowPremiered.Checked
        _ConfigOptions.Ratings = _PnlSettingsPanel.chkScraperShowRating.Checked
        _ConfigOptions.Runtime = _PnlSettingsPanel.chkScraperShowRuntime.Checked
        _ConfigOptions.Studios = _PnlSettingsPanel.chkScraperShowStudios.Checked
        _ConfigOptions.Title = _PnlSettingsPanel.chkScraperShowTitle.Checked

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
        _ConfigOptions.Episodes.Actors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.Episodes.Aired = AdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.Episodes.Credits = AdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.Episodes.Directors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.Episodes.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.Episodes.Ratings = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.Episodes.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        _ConfigOptions.Actors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Certifications = AdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Countries = AdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Creators = AdvancedSettings.GetBooleanSetting("DoCreator", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Genres = AdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        _ConfigOptions.OriginalTitle = AdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Premiered = AdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Ratings = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Runtime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Studios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        _ConfigOptions.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)

        _AddonSettings.FallBackWorldwide = AdvancedSettings.GetBooleanSetting("FallBackWorldwide", False, , Enums.ContentType.TVShow)
        _AddonSettings.ForceTitleLanguage = AdvancedSettings.GetSetting("ForceTitleLanguage", String.Empty, , Enums.ContentType.TVShow)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoActors", _ConfigOptions.Episodes.Actors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", _ConfigOptions.Episodes.Aired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", _ConfigOptions.Episodes.Credits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", _ConfigOptions.Episodes.Directors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", _ConfigOptions.Episodes.Plot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", _ConfigOptions.Episodes.Ratings, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", _ConfigOptions.Episodes.Title, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoActors", _ConfigOptions.Actors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCert", _ConfigOptions.Certifications, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCountry", _ConfigOptions.Countries, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCreator", _ConfigOptions.Creators, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", _ConfigOptions.Genres, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoOriginalTitle", _ConfigOptions.OriginalTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", _ConfigOptions.Plot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", _ConfigOptions.Premiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", _ConfigOptions.Ratings, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRuntime", _ConfigOptions.Runtime, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", _ConfigOptions.Studios, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", _ConfigOptions.Title, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("FallBackWorldwide", _AddonSettings.FallBackWorldwide, , , Enums.ContentType.TVShow)
            settings.SetSetting("ForceTitleLanguage", _AddonSettings.ForceTitleLanguage, , , Enums.ContentType.TVShow)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Class Trailer_Movie
    Implements Interfaces.IScraperAddon_Trailer_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared _ConfigScrapeModifiers As New Structures.ScrapeModifiers
    Private _PnlSettingsPanel As frmSettingsPanel_Trailer_Movie

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Trailer_Movie.IsEnabled

    Property Order As Integer Implements Interfaces.IScraperAddon_Trailer_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Trailer_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Trailer_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Trailer_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Trailer_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Trailer_Movie.StateChanged

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

    Public Sub Init() Implements Interfaces.IScraperAddon_Trailer_Movie.Init
        LoadSettings()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Trailer_Movie.InjectSettingsPanel
        LoadSettings()
        _PnlSettingsPanel = New frmSettingsPanel_Trailer_Movie
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled

        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "IMDb.com",
            .Type = Enums.SettingsPanelType.MovieTrailer
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Trailer_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function Run(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef TrailerList As List(Of MediaContainers.Trailer)) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Trailer_Movie.Run
        _Logger.Trace("[IMDB_Trailer] [Scraper_Movie] [Start]")

        If DBMovie.MainDetails.UniqueIDs.IMDbIdSpecified Then
            TrailerList = Scraper.GetTrailers(DBMovie.MainDetails.UniqueIDs.IMDbId)
        End If

        _Logger.Trace("[IMDB_Trailer] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Trailer_Movie.SaveSetup
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
        _ConfigScrapeModifiers.MainTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoTrailer", _ConfigScrapeModifiers.MainTrailer)
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