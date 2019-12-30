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
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Data_Movie
    Private _PrivateAPIKey As String = String.Empty
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Data_Movie.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

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

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef Studios As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Data_Movie.GetMovieStudio
        _Logger.Trace("[TMDB_Data] [GetMovieStudio] [Start]")
        If Not DBMovie.MainDetails.UniqueIDsSpecified Then
            _Logger.Trace("[TMDB_Data] [GetMovieStudio] [Abort] Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult
        End If
        If Not _Scraper.IsClientCreated Then
            Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
        End If
        If _Scraper.IsClientCreated Then
            If DBMovie.MainDetails.UniqueIDs.IMDbIdSpecified Then
                'IMDB-ID is available
                Studios.AddRange(_Scraper.GetMovieStudios(DBMovie.MainDetails.UniqueIDs.IMDbId))
            ElseIf DBMovie.MainDetails.UniqueIDs.TMDbIdSpecified Then
                'TMDB-ID is available
                Studios.AddRange(_Scraper.GetMovieStudios(DBMovie.MainDetails.UniqueIDs.TMDbId))
            End If
        End If
        _Logger.Trace("[TMDB_Data] [GetMovieStudio] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDbID(ByVal IMDbID As String, ByRef TMDbID As String) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Data_Movie.GetTMDbID
        _Logger.Trace("[TMDB_Data] [GetTMDBID] [Start]")
        If Not String.IsNullOrEmpty(IMDbID) Then
            If Not _Scraper.IsClientCreated Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
            If _Scraper.IsClientCreated Then
                TMDbID = _Scraper.GetMovieID(IMDbID)
            End If
        Else
            _Logger.Trace("[TMDB_Data] [GetTMDBID] [Abort] No IMDB ID to get the TMDB ID")
        End If
        _Logger.Trace("[TMDB_Data] [GetTMDBID] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub Init() Implements Interfaces.IScraperAddon_Data_Movie.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Data_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Data_Movie
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkActors.Checked = _ConfigScrapeOptions.Actors
        _PnlSettingsPanel.chkCollection.Checked = _ConfigScrapeOptions.Collection
        _PnlSettingsPanel.chkCountries.Checked = _ConfigScrapeOptions.Countries
        _PnlSettingsPanel.chkDirectors.Checked = _ConfigScrapeOptions.Directors
        _PnlSettingsPanel.chkFallBackEng.Checked = _AddonSettings.FallBackEng
        _PnlSettingsPanel.chkGenres.Checked = _ConfigScrapeOptions.Genres
        _PnlSettingsPanel.chkGetAdultItems.Checked = _AddonSettings.GetAdultItems
        _PnlSettingsPanel.chkCertifications.Checked = _ConfigScrapeOptions.MPAA
        _PnlSettingsPanel.chkOriginalTitle.Checked = _ConfigScrapeOptions.OriginalTitle
        _PnlSettingsPanel.chkPlot.Checked = _ConfigScrapeOptions.Plot
        _PnlSettingsPanel.chkPremiered.Checked = _ConfigScrapeOptions.Premiered
        _PnlSettingsPanel.chkRating.Checked = _ConfigScrapeOptions.Ratings
        _PnlSettingsPanel.chkRuntime.Checked = _ConfigScrapeOptions.Runtime
        _PnlSettingsPanel.chkSearchDeviant.Checked = _AddonSettings.SearchDeviant
        _PnlSettingsPanel.chkStudios.Checked = _ConfigScrapeOptions.Studios
        _PnlSettingsPanel.chkTagline.Checked = _ConfigScrapeOptions.Tagline
        _PnlSettingsPanel.chkTags.Checked = _ConfigScrapeOptions.Tags
        _PnlSettingsPanel.chkTitle.Checked = _ConfigScrapeOptions.Title
        _PnlSettingsPanel.chkTrailer.Checked = _ConfigScrapeOptions.Trailer
        _PnlSettingsPanel.chkWriters.Checked = _ConfigScrapeOptions.Credits
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If
        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.MovieData
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Data_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Run(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_Movie Implements Interfaces.IScraperAddon_Data_Movie.Run
        _Logger.Trace("[TMDB_Data] [Scraper_Movie] [Start]")
        Dim nMovie As MediaContainers.MainDetails = Nothing
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)

        _Scraper.DefaultLanguage = oDBElement.Language

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                'TMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                nMovie = _Scraper.GetInfo_Movie(oDBElement.MainDetails.UniqueIDs.TMDbId, FilteredOptions, False)
            ElseIf oDBElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                'IMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                nMovie = _Scraper.GetInfo_Movie(oDBElement.MainDetails.UniqueIDs.IMDbId, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID or TMDB-ID for movie --> search first and try to get ID!
                If oDBElement.MainDetails.TitleSpecified Then
                    nMovie = _Scraper.GetSearchMovieInfo(oDBElement.MainDetails.Title, oDBElement, ScrapeType, FilteredOptions)
                End If
                'if still no search result -> exit
                If nMovie Is Nothing Then
                    _Logger.Trace("[TMDB_Data] [Scraper_Movie] [Abort] No search result found")
                    Return New Interfaces.ModuleResult_Data_Movie With {.Result = Nothing}
                End If
            End If
        End If

        If nMovie Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    _Logger.Trace("[TMDB_Data] [Scraper_Movie] [Abort] No search result found")
                    Return New Interfaces.ModuleResult_Data_Movie With {.Result = Nothing}
            End Select
        Else
            _Logger.Trace("[TMDB_Data] [Scraper_Movie] [Done]")
            Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults_Movie(_AddonSettings, _Scraper)
                    If dlgSearch.ShowDialog(oDBElement.MainDetails.Title, oDBElement.FileItem.FirstPathFromStack, FilteredOptions, oDBElement.MainDetails.Year) = DialogResult.OK Then
                        nMovie = _Scraper.GetInfo_Movie(dlgSearch.Result.UniqueIDs.TMDbId, FilteredOptions, False)
                        'if a movie is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        _Logger.Trace(String.Format("[TMDB_Data] [Scraper_Movie] [Cancelled] Cancelled by user"))
                        Return New Interfaces.ModuleResult_Data_Movie With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        _Logger.Trace("[TMDB_Data] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Data_Movie.SaveSetup
        _ConfigScrapeOptions.Actors = _PnlSettingsPanel.chkActors.Checked
        _ConfigScrapeOptions.Certifications = _PnlSettingsPanel.chkCertifications.Checked
        _ConfigScrapeOptions.Collection = _PnlSettingsPanel.chkCollection.Checked
        _ConfigScrapeOptions.Countries = _PnlSettingsPanel.chkCountries.Checked
        _ConfigScrapeOptions.Directors = _PnlSettingsPanel.chkDirectors.Checked
        _ConfigScrapeOptions.Genres = _PnlSettingsPanel.chkGenres.Checked
        _ConfigScrapeOptions.MPAA = _PnlSettingsPanel.chkCertifications.Checked
        _ConfigScrapeOptions.OriginalTitle = _PnlSettingsPanel.chkOriginalTitle.Checked
        _ConfigScrapeOptions.Outline = _PnlSettingsPanel.chkPlot.Checked
        _ConfigScrapeOptions.Plot = _PnlSettingsPanel.chkPlot.Checked
        _ConfigScrapeOptions.Premiered = _PnlSettingsPanel.chkPremiered.Checked
        _ConfigScrapeOptions.Ratings = _PnlSettingsPanel.chkRating.Checked
        _ConfigScrapeOptions.Runtime = _PnlSettingsPanel.chkRuntime.Checked
        _ConfigScrapeOptions.Studios = _PnlSettingsPanel.chkStudios.Checked
        _ConfigScrapeOptions.Tagline = _PnlSettingsPanel.chkTagline.Checked
        _ConfigScrapeOptions.Tags = _PnlSettingsPanel.chkTags.Checked
        _ConfigScrapeOptions.Title = _PnlSettingsPanel.chkTitle.Checked
        _ConfigScrapeOptions.Top250 = False
        _ConfigScrapeOptions.Trailer = _PnlSettingsPanel.chkTrailer.Checked
        _ConfigScrapeOptions.Credits = _PnlSettingsPanel.chkWriters.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.FallBackEng = _PnlSettingsPanel.chkFallBackEng.Checked
        _AddonSettings.GetAdultItems = _PnlSettingsPanel.chkGetAdultItems.Checked
        _AddonSettings.SearchDeviant = _PnlSettingsPanel.chkSearchDeviant.Checked
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)

        Settings_Save()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

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
        _ConfigScrapeOptions.Collection = AdvancedSettings.GetBooleanSetting("DoCollection", True, , Enums.ContentType.Movie)
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
        _ConfigScrapeOptions.Tags = AdvancedSettings.GetBooleanSetting("DoTags", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Top250 = AdvancedSettings.GetBooleanSetting("DoTop250", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Trailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)
        _ConfigScrapeOptions.Credits = AdvancedSettings.GetBooleanSetting("DoWriters", True, , Enums.ContentType.Movie)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)
        _AddonSettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.Movie)
        _AddonSettings.GetAdultItems = AdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.Movie)
        _AddonSettings.SearchDeviant = AdvancedSettings.GetBooleanSetting("SearchDeviant", False, , Enums.ContentType.Movie)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoCast", _ConfigScrapeOptions.Actors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCert", _ConfigScrapeOptions.Certifications, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCollection", _ConfigScrapeOptions.Collection, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoCountry", _ConfigScrapeOptions.Countries, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoDirector", _ConfigScrapeOptions.Directors, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFanart", _ConfigScrapeModifier.MainFanart, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoGenres", _ConfigScrapeOptions.Genres, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoMPAA", _ConfigScrapeOptions.MPAA, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOriginalTitle", _ConfigScrapeOptions.OriginalTitle, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoOutline", _ConfigScrapeOptions.Outline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPlot", _ConfigScrapeOptions.Plot, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPoster", _ConfigScrapeModifier.MainPoster, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoPremiered", _ConfigScrapeOptions.Premiered, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.Ratings, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoRuntime", _ConfigScrapeOptions.Runtime, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoStudio", _ConfigScrapeOptions.Studios, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTagline", _ConfigScrapeOptions.Tagline, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTags", _ConfigScrapeOptions.Tags, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTitle", _ConfigScrapeOptions.Title, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTop250", _ConfigScrapeOptions.Top250, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTrailer", _ConfigScrapeOptions.Trailer, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoWriters", _ConfigScrapeOptions.Credits, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("FallBackEn", _AddonSettings.FallBackEng, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("GetAdultItems", _AddonSettings.GetAdultItems, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("SearchDeviant", _AddonSettings.SearchDeviant, , , Enums.ContentType.Movie)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text.Trim, , , Enums.ContentType.Movie)
        End Using
    End Sub

#End Region 'Methods 

End Class

Public Class Data_MovieSet
    Implements Interfaces.IScraperAddon_Data_MovieSet

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigScrapeModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigScrapeOptions As New Structures.ScrapeOptions
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Data_MovieSet
    Private _PrivateAPIKey As String = String.Empty
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Data_MovieSet.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperAddon_Data_MovieSet.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Data_MovieSet.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Data_MovieSet.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Data_MovieSet.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Data_MovieSet.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Data_MovieSet.StateChanged


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

    Function GetCollectionID(ByVal IMDbID As String, ByRef CollectionID As String) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Data_MovieSet.GetCollectionID
        _Logger.Trace("[TMDB_Data] [GetCollectionID] [Start]")
        If Not String.IsNullOrEmpty(IMDbID) Then
            If Not _Scraper.IsClientCreated Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
            If _Scraper.IsClientCreated Then
                CollectionID = _Scraper.GetMovieCollectionID(IMDbID)
                If String.IsNullOrEmpty(CollectionID) Then
                    _Logger.Trace("[TMDB_Data] [GetCollectionID] [Abort] nor search result")
                    Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                End If
            End If
        End If
        _Logger.Trace("[TMDB_Data] [GetCollectionID] [Done]")
        Return New Interfaces.ModuleResult
    End Function

    Public Sub Init() Implements Interfaces.IScraperAddon_Data_MovieSet.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Data_MovieSet.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Data_MovieSet
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkFallBackEng.Checked = _AddonSettings.FallBackEng
        _PnlSettingsPanel.chkGetAdultItems.Checked = _AddonSettings.GetAdultItems
        _PnlSettingsPanel.chkPlot.Checked = _ConfigScrapeOptions.Plot
        _PnlSettingsPanel.chkTitle.Checked = _ConfigScrapeOptions.Title
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.MoviesetData
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Data_MovieSet.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function Run(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_MovieSet Implements Interfaces.IScraperAddon_Data_MovieSet.Run
        _Logger.Trace("[TMDB_Data] [Scraper_MovieSet] [Start]")
        Dim nMovieSet As MediaContainers.MainDetails = Nothing
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)

        _Scraper.DefaultLanguage = oDBElement.Language

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                'TMDB-ID already available -> scrape and save data into an empty movieset container (nMovieSet)
                nMovieSet = _Scraper.GetInfo_MovieSet(oDBElement.MainDetails.UniqueIDs.TMDbId, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no ITMDB-ID for movieset --> search first and try to get ID!
                If oDBElement.MainDetails.TitleSpecified Then
                    nMovieSet = _Scraper.GetSearchMovieSetInfo(oDBElement.MainDetails.Title, oDBElement, ScrapeType, FilteredOptions)
                End If
                'if still no search result -> exit
                If nMovieSet Is Nothing Then
                    _Logger.Trace(String.Format("[TMDB_Data] [Scraper_MovieSet] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_MovieSet With {.Result = Nothing}
                End If
            End If
        End If

        If nMovieSet Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    _Logger.Trace(String.Format("[TMDB_Data] [Scraper_MovieSet] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_MovieSet With {.Result = Nothing}
            End Select
        Else
            _Logger.Trace("[TMDB_Data] [Scraper_MovieSet] [Done]")
            Return New Interfaces.ModuleResult_Data_MovieSet With {.Result = nMovieSet}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults_MovieSet(_AddonSettings, _Scraper)
                    If dlgSearch.ShowDialog(oDBElement.MainDetails.Title, FilteredOptions) = DialogResult.OK Then
                        nMovieSet = _Scraper.GetInfo_MovieSet(dlgSearch.Result.UniqueIDs.TMDbId, FilteredOptions, False)
                        'if a movieset is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        _Logger.Trace(String.Format("[TMDB_Data] [Scraper_MovieSet] [Cancelled] Cancelled by user"))
                        Return New Interfaces.ModuleResult_Data_MovieSet With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        _Logger.Trace("[TMDB_Data] [Scraper_MovieSet] [Done]")
        Return New Interfaces.ModuleResult_Data_MovieSet With {.Result = nMovieSet}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Data_MovieSet.SaveSetup
        _ConfigScrapeOptions.Plot = _PnlSettingsPanel.chkPlot.Checked
        _ConfigScrapeOptions.Title = _PnlSettingsPanel.chkTitle.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.FallBackEng = _PnlSettingsPanel.chkFallBackEng.Checked
        _AddonSettings.GetAdultItems = _PnlSettingsPanel.chkGetAdultItems.Checked
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)

        Settings_Save()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

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
        _ConfigScrapeOptions.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.Movieset)
        _ConfigScrapeOptions.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.Movieset)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movieset)
        _AddonSettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.Movieset)
        _AddonSettings.GetAdultItems = AdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.Movieset)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPlot", _ConfigScrapeOptions.Plot, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoTitle", _ConfigScrapeOptions.Title, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("GetAdultItems", _AddonSettings.GetAdultItems, , , Enums.ContentType.Movieset)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text.Trim, , , Enums.ContentType.Movieset)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Class Data_TV
    Implements Interfaces.IScraperAddon_Data_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigScrapeModifier As New Structures.ScrapeModifiers
    Public Shared _ConfigScrapeOptions As New Structures.ScrapeOptions
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Data_TV
    Private _PrivateAPIKey As String = String.Empty
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Data_TV.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property
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
        _PnlSettingsPanel.chkFallBackEng.Checked = _AddonSettings.FallBackEng
        _PnlSettingsPanel.chkGetAdultItems.Checked = _AddonSettings.GetAdultItems
        _PnlSettingsPanel.chkScraperEpisodeActors.Checked = _ConfigScrapeOptions.Episodes.Actors
        _PnlSettingsPanel.chkScraperEpisodeAired.Checked = _ConfigScrapeOptions.Episodes.Aired
        _PnlSettingsPanel.chkScraperEpisodeCredits.Checked = _ConfigScrapeOptions.Episodes.Credits
        _PnlSettingsPanel.chkScraperEpisodeDirectors.Checked = _ConfigScrapeOptions.Episodes.Directors
        _PnlSettingsPanel.chkScraperEpisodeGuestStars.Checked = _ConfigScrapeOptions.Episodes.GuestStars
        _PnlSettingsPanel.chkScraperEpisodePlot.Checked = _ConfigScrapeOptions.Episodes.Plot
        _PnlSettingsPanel.chkScraperEpisodeRating.Checked = _ConfigScrapeOptions.Episodes.Ratings
        _PnlSettingsPanel.chkScraperEpisodeTitle.Checked = _ConfigScrapeOptions.Episodes.Title
        _PnlSettingsPanel.chkScraperSeasonAired.Checked = _ConfigScrapeOptions.Seasons.Aired
        _PnlSettingsPanel.chkScraperSeasonPlot.Checked = _ConfigScrapeOptions.Seasons.Plot
        _PnlSettingsPanel.chkScraperSeasonTitle.Checked = _ConfigScrapeOptions.Seasons.Title
        _PnlSettingsPanel.chkScraperShowActors.Checked = _ConfigScrapeOptions.Actors
        _PnlSettingsPanel.chkScraperShowCertifications.Checked = _ConfigScrapeOptions.Certifications
        _PnlSettingsPanel.chkScraperShowCountries.Checked = _ConfigScrapeOptions.Countries
        _PnlSettingsPanel.chkScraperShowCreators.Checked = _ConfigScrapeOptions.Creators
        _PnlSettingsPanel.chkScraperShowGenres.Checked = _ConfigScrapeOptions.Genres
        _PnlSettingsPanel.chkScraperShowOriginalTitle.Checked = _ConfigScrapeOptions.OriginalTitle
        _PnlSettingsPanel.chkScraperShowPlot.Checked = _ConfigScrapeOptions.Plot
        _PnlSettingsPanel.chkScraperShowPremiered.Checked = _ConfigScrapeOptions.Premiered
        _PnlSettingsPanel.chkScraperShowRating.Checked = _ConfigScrapeOptions.Ratings
        _PnlSettingsPanel.chkScraperShowRuntime.Checked = _ConfigScrapeOptions.Runtime
        _PnlSettingsPanel.chkScraperShowStatus.Checked = _ConfigScrapeOptions.Status
        _PnlSettingsPanel.chkScraperShowStudios.Checked = _ConfigScrapeOptions.Studios
        _PnlSettingsPanel.chkScraperShowTitle.Checked = _ConfigScrapeOptions.Title
        _PnlSettingsPanel.chkTags_TVShow.Checked = _ConfigScrapeOptions.Tags
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.TVData
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Data_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function Run_TVEpisode(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.IScraperAddon_Data_TV.Run_TVEpisode
        _Logger.Trace("[TMDB_Data] [Scraper_TVEpisode] [Start]")
        Dim nTVEpisode As New MediaContainers.MainDetails
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)

        _Scraper.DefaultLanguage = oDBElement.Language

        If Not oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified AndAlso oDBElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
            oDBElement.MainDetails.UniqueIDs.TMDbId = _Scraper.GetTMDBbyTVDB(oDBElement.MainDetails.UniqueIDs.TVDbId)
        End If

        If oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
            If Not oDBElement.MainDetails.Episode = -1 AndAlso Not oDBElement.MainDetails.Season = -1 Then
                nTVEpisode = _Scraper.GetInfo_TVEpisode(CInt(oDBElement.MainDetails.UniqueIDs.TMDbId), oDBElement.MainDetails.Season, oDBElement.MainDetails.Episode, FilteredOptions)
            ElseIf oDBElement.MainDetails.AiredSpecified Then
                nTVEpisode = _Scraper.GetInfo_TVEpisode(CInt(oDBElement.MainDetails.UniqueIDs.TMDbId), oDBElement.MainDetails.Aired, FilteredOptions)
            Else
                _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No search result found"))
                Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
            End If
            'if still no search result -> exit
            If nTVEpisode Is Nothing Then
                _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No search result found"))
                Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
            End If
        Else
            _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No TV Show TMDB ID available"))
            Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
        End If

        _Logger.Trace("[TMDB_Data] [Scraper_TVEpisode] [Done]")
        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = nTVEpisode}
    End Function

    Public Function Run_TVSeason(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.IScraperAddon_Data_TV.Run_TVSeason
        _Logger.Trace("[TMDB_Data] [Scraper_TVSeason] [Start]")
        Dim nTVSeason As New MediaContainers.MainDetails
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)

        _Scraper.DefaultLanguage = oDBElement.Language

        If Not oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified AndAlso oDBElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
            oDBElement.MainDetails.UniqueIDs.TMDbId = _Scraper.GetTMDBbyTVDB(oDBElement.MainDetails.UniqueIDs.TVDbId)
        End If

        If oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
            If oDBElement.MainDetails.SeasonSpecified Then
                nTVSeason = _Scraper.GetInfo_TVSeason(CInt(oDBElement.MainDetails.UniqueIDs.TMDbId), oDBElement.MainDetails.Season, FilteredOptions)
            Else
                _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVSeason] [Abort] Season is not specified"))
                Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
            End If
            'if still no search result -> exit
            If nTVSeason Is Nothing Then
                _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVSeason] [Abort] No search result found"))
                Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
            End If
        Else
            _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVSeason] [Abort] No TV Show TMDB ID available"))
            Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
        End If

        _Logger.Trace("[TMDB_Data] [Scraper_TVSeason] [Done]")
        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = nTVSeason}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="oDBTV">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Public Function Run_TVShow(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.IScraperAddon_Data_TV.Run_TVShow
        _Logger.Trace("[TMDB_Data] [Scraper_TV] [Start]")
        Dim nTVShow As MediaContainers.MainDetails = Nothing
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, _ConfigScrapeOptions)

        _Scraper.DefaultLanguage = oDBElement.Language

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch Then
            If oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                'TMDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                nTVShow = _Scraper.GetInfo_TVShow(oDBElement.MainDetails.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf oDBElement.MainDetails.UniqueIDs.TVDbIdSpecified Then
                oDBElement.MainDetails.UniqueIDs.TMDbId = _Scraper.GetTMDBbyTVDB(oDBElement.MainDetails.UniqueIDs.TVDbId)
                If Not oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                nTVShow = _Scraper.GetInfo_TVShow(oDBElement.MainDetails.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf oDBElement.MainDetails.UniqueIDs.IMDbIdSpecified Then
                oDBElement.MainDetails.UniqueIDs.TMDbId = _Scraper.GetTMDBbyIMDB(oDBElement.MainDetails.UniqueIDs.IMDbId)
                If Not oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                nTVShow = _Scraper.GetInfo_TVShow(oDBElement.MainDetails.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If oDBElement.MainDetails.TitleSpecified Then
                    nTVShow = _Scraper.GetSearchTVShowInfo(oDBElement.MainDetails.Title, oDBElement, ScrapeType, ScrapeModifiers, FilteredOptions)
                End If
                'if still no search result -> exit
                If nTVShow Is Nothing Then
                    _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TV] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
                End If
            End If
        End If

        If nTVShow Is Nothing Then
            Select Case ScrapeType
                Case Enums.ScrapeType.AllAuto, Enums.ScrapeType.FilterAuto, Enums.ScrapeType.MarkedAuto, Enums.ScrapeType.MissingAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.SelectedAuto
                    _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TV] [Abort] No search result found"))
                    Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
            End Select
        Else
            _Logger.Trace("[TMDB_Data] [Scraper_TV] [Done]")
            Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If Not oDBElement.MainDetails.UniqueIDs.TMDbIdSpecified Then
                Using dlgSearch As New dlgSearchResults_TV(_AddonSettings, _Scraper)
                    If dlgSearch.ShowDialog(oDBElement.MainDetails.Title, oDBElement.ShowPath, FilteredOptions) = DialogResult.OK Then
                        nTVShow = _Scraper.GetInfo_TVShow(dlgSearch.Result.UniqueIDs.TMDbId, ScrapeModifiers, FilteredOptions, False)
                        'if a tvshow is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifiers.DoSearch = False
                    Else
                        _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TV] [Cancelled] Cancelled by user"))
                        Return New Interfaces.ModuleResult_Data_TVShow With {.Cancelled = True, .Result = Nothing}
                    End If
                End Using
            End If
        End If

        _Logger.Trace("[TMDB_Data] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVShow}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Data_TV.SaveSetup
        _ConfigScrapeOptions.Episodes.Actors = _PnlSettingsPanel.chkScraperEpisodeActors.Checked
        _ConfigScrapeOptions.Episodes.Aired = _PnlSettingsPanel.chkScraperEpisodeAired.Checked
        _ConfigScrapeOptions.Episodes.Credits = _PnlSettingsPanel.chkScraperEpisodeCredits.Checked
        _ConfigScrapeOptions.Episodes.Directors = _PnlSettingsPanel.chkScraperEpisodeDirectors.Checked
        _ConfigScrapeOptions.Episodes.GuestStars = _PnlSettingsPanel.chkScraperEpisodeGuestStars.Checked
        _ConfigScrapeOptions.Episodes.Plot = _PnlSettingsPanel.chkScraperEpisodePlot.Checked
        _ConfigScrapeOptions.Episodes.Ratings = _PnlSettingsPanel.chkScraperEpisodeRating.Checked
        _ConfigScrapeOptions.Episodes.Title = _PnlSettingsPanel.chkScraperEpisodeTitle.Checked
        _ConfigScrapeOptions.Actors = _PnlSettingsPanel.chkScraperShowActors.Checked
        _ConfigScrapeOptions.Certifications = _PnlSettingsPanel.chkScraperShowCertifications.Checked
        _ConfigScrapeOptions.Creators = _PnlSettingsPanel.chkScraperShowCreators.Checked
        _ConfigScrapeOptions.Countries = _PnlSettingsPanel.chkScraperShowCountries.Checked
        _ConfigScrapeOptions.Genres = _PnlSettingsPanel.chkScraperShowGenres.Checked
        _ConfigScrapeOptions.OriginalTitle = _PnlSettingsPanel.chkScraperShowOriginalTitle.Checked
        _ConfigScrapeOptions.Plot = _PnlSettingsPanel.chkScraperShowPlot.Checked
        _ConfigScrapeOptions.Premiered = _PnlSettingsPanel.chkScraperShowPremiered.Checked
        _ConfigScrapeOptions.Ratings = _PnlSettingsPanel.chkScraperShowRating.Checked
        _ConfigScrapeOptions.Runtime = _PnlSettingsPanel.chkScraperShowRuntime.Checked
        _ConfigScrapeOptions.Status = _PnlSettingsPanel.chkScraperShowStatus.Checked
        _ConfigScrapeOptions.Studios = _PnlSettingsPanel.chkScraperShowStudios.Checked
        _ConfigScrapeOptions.Tags = _PnlSettingsPanel.chkTags_TVShow.Checked
        _ConfigScrapeOptions.Title = _PnlSettingsPanel.chkScraperShowTitle.Checked
        _ConfigScrapeOptions.Seasons.Aired = _PnlSettingsPanel.chkScraperSeasonAired.Checked
        _ConfigScrapeOptions.Seasons.Plot = _PnlSettingsPanel.chkScraperSeasonPlot.Checked
        _ConfigScrapeOptions.Seasons.Title = _PnlSettingsPanel.chkScraperSeasonTitle.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.FallBackEng = _PnlSettingsPanel.chkFallBackEng.Checked
        _AddonSettings.GetAdultItems = _PnlSettingsPanel.chkGetAdultItems.Checked
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)

        Settings_Save()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

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
        _ConfigScrapeOptions.Episodes.Actors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Aired = AdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Credits = AdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Directors = AdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.GuestStars = AdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Ratings = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Episodes.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        _ConfigScrapeOptions.Seasons.Aired = AdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVSeason)
        _ConfigScrapeOptions.Seasons.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVSeason)
        _ConfigScrapeOptions.Seasons.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVSeason)
        _ConfigScrapeOptions.Actors = AdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Certifications = AdvancedSettings.GetBooleanSetting("DoCert", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Countries = AdvancedSettings.GetBooleanSetting("DoCountry", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Creators = AdvancedSettings.GetBooleanSetting("DoCreator", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.EpisodeGuideURL = AdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Genres = AdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.OriginalTitle = AdvancedSettings.GetBooleanSetting("DoOriginalTitle", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Plot = AdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Premiered = AdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Ratings = AdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Runtime = AdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Status = AdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Studios = AdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Tags = AdvancedSettings.GetBooleanSetting("DoTags", True, , Enums.ContentType.TVShow)
        _ConfigScrapeOptions.Title = AdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.TV)
        _AddonSettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.TV)
        _AddonSettings.GetAdultItems = AdvancedSettings.GetBooleanSetting("GetAdultItems", False, , Enums.ContentType.TV)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoActors", _ConfigScrapeOptions.Episodes.Actors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", _ConfigScrapeOptions.Episodes.Aired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", _ConfigScrapeOptions.Episodes.Credits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", _ConfigScrapeOptions.Episodes.Directors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", _ConfigScrapeOptions.Episodes.GuestStars, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", _ConfigScrapeOptions.Episodes.Plot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.Episodes.Ratings, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", _ConfigScrapeOptions.Episodes.Title, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", _ConfigScrapeOptions.Seasons.Aired, , , Enums.ContentType.TVSeason)
            settings.SetBooleanSetting("DoPlot", _ConfigScrapeOptions.Seasons.Plot, , , Enums.ContentType.TVSeason)
            settings.SetBooleanSetting("DoTitle", _ConfigScrapeOptions.Seasons.Title, , , Enums.ContentType.TVSeason)
            settings.SetBooleanSetting("DoActors", _ConfigScrapeOptions.Actors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCert", _ConfigScrapeOptions.Certifications, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCountry", _ConfigScrapeOptions.Countries, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoCreator", _ConfigScrapeOptions.Creators, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoEpisodeGuide", _ConfigScrapeOptions.EpisodeGuideURL, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", _ConfigScrapeOptions.Genres, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoOriginalTitle", _ConfigScrapeOptions.OriginalTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", _ConfigScrapeOptions.Plot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", _ConfigScrapeOptions.Premiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", _ConfigScrapeOptions.Ratings, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRuntime", _ConfigScrapeOptions.Runtime, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStatus", _ConfigScrapeOptions.Status, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", _ConfigScrapeOptions.Studios, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTags", _ConfigScrapeOptions.Tags, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", _ConfigScrapeOptions.Title, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("FallBackEn", _AddonSettings.FallBackEng, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("GetAdultItems", _AddonSettings.GetAdultItems, , , Enums.ContentType.TV)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text.Trim, , , Enums.ContentType.TV)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Class Image_Movie
    Implements Interfaces.IScraperAddon_Image_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Image_Movie
    Private _PrivateAPIKey As String = String.Empty
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Image_Movie.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperAddon_Image_Movie.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Image_Movie.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Image_Movie.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Image_Movie.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Image_Movie.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Image_Movie.StateChanged

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

    Public Sub Init() Implements Interfaces.IScraperAddon_Image_Movie.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Image_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Image_Movie
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapeFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapePoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.MovieImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Image_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperAddon_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
        End Select
        Return False
    End Function

    Public Function Run(ByRef DBMovie As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Image_Movie.Run
        _Logger.Trace("[TMDB_Image] [Scraper_Movie] [Start]")
        If Not DBMovie.MainDetails.UniqueIDs.TMDbIdSpecified Then
            DBMovie.MainDetails.UniqueIDs.TMDbId = AddonsManager.Instance.GetMovieTMDbID(DBMovie.MainDetails.UniqueIDs.IMDbId)
        End If

        If DBMovie.MainDetails.UniqueIDs.TMDbIdSpecified Then
            Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)
            ImagesContainer = _Scraper.GetImages_Movie_MovieSet(DBMovie.MainDetails.UniqueIDs.TMDbId, FilteredModifiers, Enums.ContentType.Movie)
        End If

        _Logger.Trace("[TMDB_Image] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Image_Movie.SaveSetup
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapePoster.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeFanart.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)

        Settings_Save()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

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
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movie)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movie)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.Movie)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.Movie)
        End Using
    End Sub
#End Region 'Methods

End Class

Public Class Image_Movieset
    Implements Interfaces.IScraperAddon_Image_Movieset

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _Enabled As Boolean = False
    Private _PrivateAPIKey As String = String.Empty
    Private _PnlSettingsPanel As frmSettingsPanel_Image_MovieSet
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Image_Movieset.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperAddon_Image_Movieset.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Image_Movieset.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Image_Movieset.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Image_Movieset.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Image_Movieset.SettingsChanged
    Public Event StateChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.IScraperAddon_Image_Movieset.StateChanged

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

    Sub Init() Implements Interfaces.IScraperAddon_Image_Movieset.Init
        LoadSettings_MovieSet()
    End Sub

    Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Image_Movieset.InjectSettingsPanel
        LoadSettings_MovieSet()
        _PnlSettingsPanel = New frmSettingsPanel_Image_MovieSet
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapeFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapePoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.MoviesetImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Image_Movieset.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperAddon_Image_Movieset.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
        End Select
        Return False
    End Function

    Function Run(ByRef DBMovieSet As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Image_Movieset.Run
        logger.Trace("[TMDB_Image] [Scraper_MovieSet] [Start]")
        If Not DBMovieSet.MainDetails.UniqueIDs.TMDbIdSpecified AndAlso DBMovieSet.MoviesInSetSpecified Then
            DBMovieSet.MainDetails.UniqueIDs.TMDbId = AddonsManager.Instance.GetMovieCollectionID(DBMovieSet.MoviesInSet.Item(0).DBMovie.MainDetails.UniqueIDs.IMDbId)
        End If

        If DBMovieSet.MainDetails.UniqueIDs.TMDbIdSpecified Then
            Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)
            ImagesContainer = _Scraper.GetImages_Movie_MovieSet(DBMovieSet.MainDetails.UniqueIDs.TMDbId, FilteredModifiers, Enums.ContentType.Movieset)
        End If

        logger.Trace("[TMDB_Image] [Scraper_MovieSet] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Image_Movieset.SaveSetup
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapePoster.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeFanart.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)

        SaveSettings_MovieSet()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub LoadSettings_MovieSet()
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.ContentType.Movieset)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.ContentType.Movieset)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movieset)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.Movieset)
            settings.SetBooleanSetting("DoFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.Movieset)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.Movieset)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Class Image_TV
    Implements Interfaces.IScraperAddon_Image_TV

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _Enabled As Boolean = False
    Private _PrivateAPIKey As String = String.Empty
    Private _PnlSettingsPanel As frmSettingsPanel_Image_TV
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Image_TV.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

    Property Order As Integer Implements Interfaces.IScraperAddon_Image_TV.Order

    Property SettingsPanel As Containers.SettingsPanel = Nothing Implements Interfaces.IScraperAddon_Image_TV.SettingsPanel

#End Region 'Properties

#Region "Events"

    Public Event NeedsRestart() Implements Interfaces.IScraperAddon_Image_TV.NeedsRestart
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperAddon_Image_TV.ScraperEvent
    Public Event SettingsChanged() Implements Interfaces.IScraperAddon_Image_TV.SettingsChanged
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperAddon_Image_TV.StateChanged


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

    Sub Init() Implements Interfaces.IScraperAddon_Image_TV.Init
        LoadSettings_TV()
    End Sub

    Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Image_TV.InjectSettingsPanel
        LoadSettings_TV()
        _PnlSettingsPanel = New frmSettingsPanel_Image_TV
        _PnlSettingsPanel.chkEnabled.Checked = IsEnabled
        _PnlSettingsPanel.chkScrapeEpisodePoster.Checked = _ConfigModifier.EpisodePoster
        _PnlSettingsPanel.chkScrapeSeasonPoster.Checked = _ConfigModifier.SeasonPoster
        _PnlSettingsPanel.chkScrapeShowFanart.Checked = _ConfigModifier.MainFanart
        _PnlSettingsPanel.chkScrapeShowPoster.Checked = _ConfigModifier.MainPoster
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(IsEnabled, 9, 10),
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.TVImage
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Image_TV.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.IScraperAddon_Image_TV.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.EpisodePoster
                Return _ConfigModifier.EpisodePoster
            Case Enums.ModifierType.MainFanart
                Return _ConfigModifier.MainFanart
            Case Enums.ModifierType.MainPoster
                Return _ConfigModifier.MainPoster
            Case Enums.ModifierType.SeasonPoster
                Return _ConfigModifier.SeasonPoster
        End Select
        Return False
    End Function

    Function Run(ByRef DBTV As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Image_TV.Run
        _Logger.Trace("[TMDB_Image] [Scraper_TV] [Start]")
        Dim FilteredModifiers As Structures.ScrapeModifiers = Functions.ScrapeModifiersAndAlso(ScrapeModifiers, _ConfigModifier)

        If DBTV.MainDetails IsNot Nothing AndAlso Not DBTV.MainDetails.UniqueIDs.TMDbIdSpecified Then
            If DBTV.MainDetails.UniqueIDs.TVDbIdSpecified Then
                DBTV.MainDetails.UniqueIDs.TMDbId = _Scraper.GetTMDBbyTVDB(DBTV.MainDetails.UniqueIDs.TVDbId)
            ElseIf DBTV.MainDetails.UniqueIDs.IMDbIdSpecified Then
                DBTV.MainDetails.UniqueIDs.TMDbId = _Scraper.GetTMDBbyIMDB(DBTV.MainDetails.UniqueIDs.IMDbId)
            End If
        End If

        Select Case DBTV.ContentType
            Case Enums.ContentType.TVEpisode
                If DBTV.MainDetails.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _Scraper.GetImages_TVEpisode(DBTV.MainDetails.UniqueIDs.TMDbId, DBTV.MainDetails.Season, DBTV.MainDetails.Episode, FilteredModifiers)
                    If FilteredModifiers.MainFanart Then
                        ImagesContainer.MainFanarts = _Scraper.GetImages_TV(DBTV.MainDetails.UniqueIDs.TMDbId, FilteredModifiers).MainFanarts
                    End If
                Else
                    _Logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TMDB ID exist to search: ", DBTV.MainDetails.Title))
                End If
            Case Enums.ContentType.TVSeason
                If DBTV.MainDetails.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _Scraper.GetImages_TV(DBTV.MainDetails.UniqueIDs.TMDbId, FilteredModifiers)
                Else
                    _Logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.MainDetails.Title))
                End If
            Case Enums.ContentType.TVShow
                If DBTV.MainDetails.UniqueIDs.TMDbIdSpecified Then
                    ImagesContainer = _Scraper.GetImages_TV(DBTV.MainDetails.UniqueIDs.TMDbId, FilteredModifiers)
                Else
                    _Logger.Trace(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] No TVDB ID exist to search: ", DBTV.MainDetails.Title))
                End If
            Case Else
                _Logger.Error(String.Concat("[TMDB_Image] [Scraper_TV] [Abort] Unhandled ContentType"))
        End Select

        _Logger.Trace("[TMDB_Image] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Image_TV.SaveSetup
        _ConfigModifier.EpisodePoster = _PnlSettingsPanel.chkScrapeEpisodePoster.Checked
        _ConfigModifier.SeasonPoster = _PnlSettingsPanel.chkScrapeSeasonPoster.Checked
        _ConfigModifier.MainFanart = _PnlSettingsPanel.chkScrapeShowFanart.Checked
        _ConfigModifier.MainPoster = _PnlSettingsPanel.chkScrapeShowPoster.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)

        SaveSettings_TV()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

        If DoDispose Then
            RemoveHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
            RemoveHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
            RemoveHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged
            _PnlSettingsPanel.Dispose()
        End If
    End Sub

#End Region 'Interface Methods

#Region "Methods"

    Sub LoadSettings_TV()
        _ConfigModifier.EpisodePoster = AdvancedSettings.GetBooleanSetting("DoEpisodePoster", True, , Enums.ContentType.TV)
        _ConfigModifier.SeasonPoster = AdvancedSettings.GetBooleanSetting("DoSeasonPoster", True, , Enums.ContentType.TV)
        _ConfigModifier.MainFanart = AdvancedSettings.GetBooleanSetting("DoShowFanart", True, , Enums.ContentType.TV)
        _ConfigModifier.MainPoster = AdvancedSettings.GetBooleanSetting("DoShowPoster", True, , Enums.ContentType.TV)

        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.TV)
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)
    End Sub

    Sub SaveSettings_TV()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoEpisodePoster", _ConfigModifier.EpisodePoster, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoSeasonPoster", _ConfigModifier.SeasonPoster, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowFanart", _ConfigModifier.MainFanart, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("DoShowPoster", _ConfigModifier.MainPoster, , , Enums.ContentType.TV)
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.TV)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Class Trailer_Movie
    Implements Interfaces.IScraperAddon_Trailer_Movie

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _AddonSettings As New AddonSettings
    Public Shared _ConfigModifier As New Structures.ScrapeModifiers
    Private _Enabled As Boolean = False
    Private _PnlSettingsPanel As frmSettingsPanel_Trailer_Movie
    Private _PrivateAPIKey As String = String.Empty
    Private _Scraper As New Scraper

#End Region 'Fields

#Region "Properties"

    Property IsEnabled() As Boolean Implements Interfaces.IScraperAddon_Trailer_Movie.IsEnabled
        Get
            Return _Enabled
        End Get
        Set(ByVal value As Boolean)
            _Enabled = value
            If _Enabled Then
                Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))
            End If
        End Set
    End Property

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

    Sub Init() Implements Interfaces.IScraperAddon_Trailer_Movie.Init
        Settings_Load()
    End Sub

    Public Sub InjectSettingsPanel() Implements Interfaces.IScraperAddon_Trailer_Movie.InjectSettingsPanel
        Settings_Load()
        _PnlSettingsPanel = New frmSettingsPanel_Trailer_Movie
        _PnlSettingsPanel.chkEnabled.Checked = _Enabled
        _PnlSettingsPanel.txtApiKey.Text = _PrivateAPIKey
        _PnlSettingsPanel.chkFallBackEng.Checked = _AddonSettings.FallBackEng

        If Not String.IsNullOrEmpty(_PrivateAPIKey) Then
            _PnlSettingsPanel.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _PnlSettingsPanel.lblEMMAPI.Visible = False
            _PnlSettingsPanel.txtApiKey.Enabled = True
        End If

        AddHandler _PnlSettingsPanel.NeedsRestart, AddressOf Handle_NeedsRestart
        AddHandler _PnlSettingsPanel.SettingsChanged, AddressOf Handle_SettingsChanged
        AddHandler _PnlSettingsPanel.StateChanged, AddressOf Handle_StateChanged

        SettingsPanel = New Containers.SettingsPanel With {
            .ImageIndex = If(_Enabled, 9, 10),
            .Order = 110,
            .Panel = _PnlSettingsPanel.pnlSettings,
            .Title = "TheMovieDB.org",
            .Type = Enums.SettingsPanelType.MovieTrailer
        }
    End Sub

    Public Sub OrderChanged(ByVal OrderState As Containers.SettingsPanel.OrderState) Implements Interfaces.IScraperAddon_Trailer_Movie.OrderChanged
        _PnlSettingsPanel.OrderChanged(OrderState)
    End Sub

    Public Function Run(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef TrailerList As List(Of MediaContainers.Trailer)) As Interfaces.ModuleResult Implements Interfaces.IScraperAddon_Trailer_Movie.Run
        _Logger.Trace("[TMDB_Trailer] [Scraper_Movie] [Start]")

        _Scraper.DefaultLanguage = DBMovie.Language

        If Not DBMovie.MainDetails.UniqueIDs.TMDbIdSpecified Then
            DBMovie.MainDetails.UniqueIDs.TMDbId = AddonsManager.Instance.GetMovieTMDbID(DBMovie.MainDetails.UniqueIDs.IMDbId)
        End If

        If DBMovie.MainDetails.UniqueIDs.TMDbIdSpecified Then
            TrailerList = _Scraper.GetTrailers(DBMovie.MainDetails.UniqueIDs.TMDbId)
        End If

        _Logger.Trace("[TMDB_Trailer] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSetup(ByVal DoDispose As Boolean) Implements Interfaces.IScraperAddon_Trailer_Movie.SaveSetup
        _AddonSettings.FallBackEng = _PnlSettingsPanel.chkFallBackEng.Checked

        Dim bAPIKeyChanged = Not _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _PrivateAPIKey = _PnlSettingsPanel.txtApiKey.Text.Trim
        _AddonSettings.FallBackEng = _PnlSettingsPanel.chkFallBackEng.Checked
        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), My.Resources.EmberAPIKey, _PrivateAPIKey)

        Settings_Save()

        If bAPIKeyChanged Then Task.Run(Function() _Scraper.CreateAPI(_AddonSettings))

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
        _ConfigModifier.MainTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True, , Enums.ContentType.Movie)

        _AddonSettings.APIKey = If(String.IsNullOrEmpty(_PrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", _PrivateAPIKey)
        _AddonSettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False, , Enums.ContentType.Movie)
        _PrivateAPIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
    End Sub

    Sub Settings_Save()
        Using settings = New AdvancedSettings()
            settings.SetSetting("APIKey", _PnlSettingsPanel.txtApiKey.Text, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("FallBackEn", _AddonSettings.FallBackEng, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("DoTrailer", _ConfigModifier.MainTrailer, , , Enums.ContentType.Movie)
        End Using
    End Sub

#End Region 'Methods

End Class

Public Structure AddonSettings

#Region "Fields"

    Dim APIKey As String
    Dim FallBackEng As Boolean
    Dim GetAdultItems As Boolean
    Dim SearchDeviant As Boolean

#End Region 'Fields

End Structure