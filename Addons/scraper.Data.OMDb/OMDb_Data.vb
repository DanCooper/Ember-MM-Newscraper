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

Public Class OMDb_Data
    Implements Interfaces.ScraperModule_Data_Movie
    Implements Interfaces.ScraperModule_Data_TV


#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared _AssemblyName As String
    Public Shared ConfigScrapeOptions_Movie As New Structures.ScrapeOptions
    Public Shared ConfigScrapeOptions_TV As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifiers
    Public Shared ConfigScrapeModifier_TV As New Structures.ScrapeModifiers

    Private _SpecialSettings_Movie As New SpecialSettings
    Private _SpecialSettings_TV As New SpecialSettings
    Private _SpecialSettings_TVEpisode As New SpecialSettings
    Private _Name As String = "OMDb_Data"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_TV As frmSettingsHolder_TV
    Private _OMDbAPI_Movie As New Scraper
    Private _OMDbAPI_TV As New Scraper

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
            Return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.ScraperModule_Data_Movie.ScraperEnabled
        Get
            Return _ScraperEnabled_Movie
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_Movie = value
            If _ScraperEnabled_Movie Then
                _OMDbAPI_Movie.CreateAPI(_SpecialSettings_Movie)
            End If
        End Set
    End Property

    Property ScraperEnabled_TV() As Boolean Implements Interfaces.ScraperModule_Data_TV.ScraperEnabled
        Get
            Return _ScraperEnabled_TV
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_TV = value
            If _ScraperEnabled_TV Then
                _OMDbAPI_TV.CreateAPI(_SpecialSettings_TV)
            End If
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
        RaiseEvent ScraperSetupChanged_Movie(String.Concat(_Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_TV(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_TV = state
        RaiseEvent ScraperSetupChanged_TV(String.Concat(_Name, "_TV"), state, difforder)
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
        _setup_Movie.chkIMDb.Checked = _SpecialSettings_Movie.IMDb
        _setup_Movie.chkMetascore.Checked = _SpecialSettings_Movie.Metascore
        _setup_Movie.chkTomatometer.Checked = _SpecialSettings_Movie.Tomatometer
        _setup_Movie.txtApiKey.Text = _SpecialSettings_Movie.APIKey

        _setup_Movie.OrderChanged()

        SPanel.Name = String.Concat(_Name, "_Movie")
        SPanel.Text = "OMDb"
        SPanel.Prefix = "OMDbMovieInfo_"
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

    Function InjectSetupScraper_TV() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_TV.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup_TV = New frmSettingsHolder_TV
        LoadSettings_TV()
        _setup_TV.chkEnabled.Checked = _ScraperEnabled_TV
        _setup_TV.chkEpisodeIMDb.Checked = _SpecialSettings_TVEpisode.IMDb
        _setup_TV.chkShowIMDb.Checked = _SpecialSettings_TV.IMDb
        _setup_TV.txtApiKey.Text = _SpecialSettings_TV.APIKey

        _setup_TV.OrderChanged()

        SPanel.Name = String.Concat(_Name, "_TV")
        SPanel.Text = "OMDb"
        SPanel.Prefix = "OMDbTVInfo_"
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
        _SpecialSettings_Movie.APIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.IMDb = AdvancedSettings.GetBooleanSetting("IMDb", False, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.Metascore = AdvancedSettings.GetBooleanSetting("Metascore", False, , Enums.ContentType.Movie)
        _SpecialSettings_Movie.Tomatometer = AdvancedSettings.GetBooleanSetting("Tomatometer", False, , Enums.ContentType.Movie)

        ConfigScrapeOptions_Movie.bMainRating = _SpecialSettings_Movie.AnyRatingEnabled
    End Sub

    Sub LoadSettings_TV()
        _SpecialSettings_TV.APIKey = AdvancedSettings.GetSetting("APIKey", String.Empty, , Enums.ContentType.TV)
        _SpecialSettings_TVEpisode.IMDb = AdvancedSettings.GetBooleanSetting("IMDb", False, , Enums.ContentType.TVEpisode)
        _SpecialSettings_TV.IMDb = AdvancedSettings.GetBooleanSetting("IMDb", False, , Enums.ContentType.TVShow)

        ConfigScrapeOptions_TV.bMainRating = _SpecialSettings_TV.AnyRatingEnabled
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New AdvancedSettings()
            settings.SetSetting("APIKey", _SpecialSettings_Movie.APIKey, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("IMDb", _SpecialSettings_Movie.IMDb, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("Metascore", _SpecialSettings_Movie.Metascore, , , Enums.ContentType.Movie)
            settings.SetBooleanSetting("Tomatometer", _SpecialSettings_Movie.Tomatometer, , , Enums.ContentType.Movie)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New AdvancedSettings()
            settings.SetSetting("APIKey", _SpecialSettings_TV.APIKey, , , Enums.ContentType.TV)
            settings.SetBooleanSetting("IMDb", _SpecialSettings_TVEpisode.IMDb, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("IMDb", _SpecialSettings_TV.IMDb, , , Enums.ContentType.TVShow)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        Dim bAPIKeyChanged = Not _SpecialSettings_Movie.APIKey = _setup_Movie.txtApiKey.Text.Trim
        _SpecialSettings_Movie.APIKey = _setup_Movie.txtApiKey.Text.Trim
        _SpecialSettings_Movie.IMDb = _setup_Movie.chkIMDb.Checked
        _SpecialSettings_Movie.Metascore = _setup_Movie.chkMetascore.Checked
        _SpecialSettings_Movie.Tomatometer = _setup_Movie.chkTomatometer.Checked

        ConfigScrapeOptions_Movie.bMainRating = _SpecialSettings_Movie.AnyRatingEnabled

        SaveSettings_Movie()

        If bAPIKeyChanged Then _OMDbAPI_Movie.CreateAPI(_SpecialSettings_Movie)

        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_TV(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_TV.SaveSetupScraper
        Dim bAPIKeyChanged = Not _SpecialSettings_TV.APIKey = _setup_TV.txtApiKey.Text.Trim
        _SpecialSettings_TV.APIKey = _setup_TV.txtApiKey.Text.Trim
        _SpecialSettings_TV.IMDb = _setup_TV.chkShowIMDb.Checked
        _SpecialSettings_TVEpisode.IMDb = _setup_TV.chkEpisodeIMDb.Checked

        ConfigScrapeOptions_TV.bMainRating = _SpecialSettings_TV.AnyRatingEnabled

        SaveSettings_TV()

        If bAPIKeyChanged Then _OMDbAPI_TV.CreateAPI(_SpecialSettings_TV)

        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            _setup_TV.Dispose()
        End If
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_Movie(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_Movie Implements Interfaces.ScraperModule_Data_Movie.Scraper_Movie
        _Logger.Trace("[OMDb_Data] [Scraper_Movie] [Start]")
        Dim nMovie As MediaContainers.Movie = Nothing
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_Movie)

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch AndAlso _OMDbAPI_Movie.IsClientCreated Then
            If oDBElement.Movie.UniqueIDs.IMDbIdSpecified Then
                Dim nRatings = _OMDbAPI_Movie.GetRatingsByImbId(oDBElement.Movie.UniqueIDs.IMDbId, oDBElement.ContentType, FilteredOptions)
                If nRatings IsNot Nothing Then
                    nMovie = New MediaContainers.Movie With {.Ratings = nRatings}
                End If
            Else
                _Logger.Trace("[OMDb_Data] [Scraper_Movie] [Abort] Need IMDb ID to get data")
                Return New Interfaces.ModuleResult_Data_Movie With {.Result = Nothing}
            End If
        ElseIf Not _OMDbAPI_Movie.IsClientCreated Then
            _Logger.Error("[OMDb_Data] [Scraper_Movie] [Abort] Can't create API client (API key missing?)")
        End If

        _Logger.Trace("[OMDb_Data] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
    End Function
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="oDBTV">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TMDB/TVDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_TV(ByRef oDBElement As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVShow Implements Interfaces.ScraperModule_Data_TV.Scraper_TVShow
        _Logger.Trace("[OMDb_Data] [Scraper_TV] [Start]")
        Dim nTVshow As MediaContainers.TVShow = Nothing
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_TV)

        If ScrapeModifiers.MainNFO AndAlso Not ScrapeModifiers.DoSearch AndAlso _OMDbAPI_TV.IsClientCreated Then
            If oDBElement.TVShow.UniqueIDs.IMDbIdSpecified Then
                Dim nRatings = _OMDbAPI_TV.GetRatingsByImbId(oDBElement.TVShow.UniqueIDs.IMDbId, oDBElement.ContentType, FilteredOptions)
                If nRatings IsNot Nothing Then
                    nTVshow = New MediaContainers.TVShow With {.Ratings = nRatings}
                End If
            Else
                _Logger.Trace("[OMDb_Data] [Scraper_TV] [Abort] Need IMDb ID to get data")
                Return New Interfaces.ModuleResult_Data_TVShow With {.Result = Nothing}
            End If
        ElseIf Not _OMDbAPI_TV.IsClientCreated Then
            _Logger.Error("[OMDb_Data] [Scraper_TV] [Abort] Can't create API client (API key missing?)")
        End If

        _Logger.Trace("[OMDb_Data] [Scraper_TV] [Done]")
        Return New Interfaces.ModuleResult_Data_TVShow With {.Result = nTVshow}
    End Function

    Public Function Scraper_TVEpisode(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVEpisode Implements Interfaces.ScraperModule_Data_TV.Scraper_TVEpisode
        _Logger.Trace("[OMDb_Data] [Scraper_TVEpisode] [Start]")
        Dim nTVEpisode As MediaContainers.EpisodeDetails = Nothing
        '    Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_TV)

        '    If oDBElement.TVShow.TMDBSpecified Then
        '        If Not oDBElement.TVEpisode.Episode = -1 AndAlso Not oDBElement.TVEpisode.Season = -1 Then
        '            nTVEpisode = _OMDbAPI_TV.GetInfo_TVEpisode(CInt(oDBElement.TVShow.TMDB), oDBElement.TVEpisode.Season, oDBElement.TVEpisode.Episode, FilteredOptions)
        '        ElseIf oDBElement.TVEpisode.AiredSpecified Then
        '            nTVEpisode = _OMDbAPI_TV.GetInfo_TVEpisode(CInt(oDBElement.TVShow.TMDB), oDBElement.TVEpisode.Aired, FilteredOptions)
        '        Else
        '            _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No search result found"))
        '            Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
        '        End If
        '        'if still no search result -> exit
        '        If nTVEpisode Is Nothing Then
        '            _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No search result found"))
        '            Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
        '        End If
        '    Else
        '        _Logger.Trace(String.Format("[TMDB_Data] [Scraper_TVEpisode] [Abort] No TV Show TMDB ID available"))
        '        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = Nothing}
        '    End If

        _Logger.Trace("[OMDb_Data] [Scraper_TVEpisode] [Done]")
        Return New Interfaces.ModuleResult_Data_TVEpisode With {.Result = nTVEpisode}
    End Function

    Public Function Scraper_TVSeason(ByRef oDBElement As Database.DBElement, ByVal ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_TVSeason Implements Interfaces.ScraperModule_Data_TV.Scraper_TVSeason
        _Logger.Trace("[OMDb_Data] [Scraper_TVSeason] [Start]")
        Dim nTVSeason As MediaContainers.SeasonDetails = Nothing
        '    Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions_TV)

        '    If Not oDBElement.TVShow.TMDBSpecified AndAlso oDBElement.TVShow.TVDBSpecified Then
        '        oDBElement.TVShow.TMDB = _OMDbAPI_TV.GetTMDBbyTVDB(oDBElement.TVShow.TVDB)
        '    End If

        '    If oDBElement.TVShow.TMDBSpecified Then
        '        If oDBElement.TVSeason.SeasonSpecified Then
        '            nTVSeason = _OMDbAPI_TV.GetInfo_TVSeason(CInt(oDBElement.TVShow.TMDB), oDBElement.TVSeason.Season, FilteredOptions)
        '        Else
        '            _Logger.Trace(String.Format("[OMDb_Data] [Scraper_TVSeason] [Abort] Season is not specified"))
        '            Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
        '        End If
        '        'if still no search result -> exit
        '        If nTVSeason Is Nothing Then
        '            _Logger.Trace(String.Format("[OMDb_Data] [Scraper_TVSeason] [Abort] No search result found"))
        '            Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
        '        End If
        '    Else
        '        _Logger.Trace(String.Format("[OMDb_Data] [Scraper_TVSeason] [Abort] No TV Show TMDB ID available"))
        '        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = Nothing}
        '    End If

        _Logger.Trace("[OMDb_Data] [Scraper_TVSeason] [Done]")
        Return New Interfaces.ModuleResult_Data_TVSeason With {.Result = nTVSeason}
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup_Movie.OrderChanged()
    End Sub

    Public Sub ScraperOrderChanged_TV() Implements Interfaces.ScraperModule_Data_TV.ScraperOrderChanged
        _setup_TV.OrderChanged()
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDbIdByIMDbId(ByVal imdbId As String, ByRef tmdbId As Integer) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDbIdByIMDbId
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

#End Region 'Methods

#Region "Nested Types"

    Public Class SpecialSettings

#Region "Properties"

        Public ReadOnly Property AnyRatingEnabled As Boolean
            Get
                Return IMDb OrElse Metascore OrElse Tomatometer
            End Get
        End Property

        Public Property APIKey As String = String.Empty

        Public Property IMDb As Boolean

        Public Property Metascore As Boolean

        Public Property Tomatometer As Boolean

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class