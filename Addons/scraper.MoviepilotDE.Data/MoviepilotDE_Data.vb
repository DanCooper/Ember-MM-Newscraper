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

Imports NLog
Imports EmberAPI

Public Class MoviepilotDE_Data
    Implements Interfaces.IScraperModule_Data_Movie


#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Public Shared ConfigScrapeOptions As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifiers As New Structures.ScrapeModifiers
    Public Shared _SettingsPanelID_Movie As String = String.Empty

    Private _scraper As New MoviepilotDE.Scraper
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder

#End Region 'Fields

#Region "Events"

    Public Event ScraperEvent_Movie(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.IScraperModule_Data_Movie.ScraperEvent_Movie
    Public Event NeedsRestart() Implements Interfaces.IScraperModule_Data_Movie.NeedsRestart_Movie
    Public Event SettingsChanged() Implements Interfaces.IScraperModule_Data_Movie.SettingsChanged_Movie
    Public Event StateChanged(ByVal SettingsPanelID As String, ByVal State As Boolean, ByVal DiffOrder As Integer) Implements Interfaces.IScraperModule_Data_Movie.StateChanged_Movie

#End Region 'Events

#Region "Properties"

    Property Enabled() As Boolean Implements Interfaces.IScraperModule_Data_Movie.Enabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Handle_StateChanged(ByVal State As Boolean, ByVal DiffOrder As Integer)
        Enabled = State
        RaiseEvent StateChanged(_SettingsPanelID_Movie, State, DiffOrder)
    End Sub

    Sub Init() Implements Interfaces.IScraperModule_Data_Movie.Init_Movie
        LoadSettings()
    End Sub

    Function InjectSettingsPanel(ByVal SettingsPanelID As String) As Containers.SettingsPanel Implements Interfaces.IScraperModule_Data_Movie.InjectSettingsPanel_Movie
        _SettingsPanelID_Movie = SettingsPanelID

        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _ScraperEnabled
        _setup.chkCertifications.Checked = ConfigScrapeOptions.bMainCertifications
        _setup.chkOutline.Checked = ConfigScrapeOptions.bMainOutline
        _setup.chkPlot.Checked = ConfigScrapeOptions.bMainPlot

        _setup.orderChanged()

        AddHandler _setup.SetupScraperChanged, AddressOf Handle_StateChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged

        Return New Containers.SettingsPanel With {
            .ImageIndex = If(_ScraperEnabled, 9, 10),
            .Order = 110,
            .Panel = _setup.pnlSettings,
            .Title = "Moviepilot.de",
            .Type = Enums.SettingsPanelType.MovieData
        }
    End Function

    Sub LoadSettings()
        ConfigScrapeOptions.bMainOutline = AdvancedSettings.GetBooleanSetting("DoOutline", True)
        ConfigScrapeOptions.bMainPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True)
        ConfigScrapeOptions.bMainCertifications = AdvancedSettings.GetBooleanSetting("DoCert", True)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoOutline", ConfigScrapeOptions.bMainOutline)
            settings.SetBooleanSetting("DoPlot", ConfigScrapeOptions.bMainPlot)
            settings.SetBooleanSetting("DoCert", ConfigScrapeOptions.bMainCertifications)
        End Using
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.IScraperModule_Data_Movie.SaveSetupScraper
        ConfigScrapeOptions.bMainCertifications = _setup.chkCertifications.Checked
        ConfigScrapeOptions.bMainOutline = _setup.chkOutline.Checked
        ConfigScrapeOptions.bMainPlot = _setup.chkPlot.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_StateChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from Moviepilot.de (German site)
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper_Movie(ByRef oDBMovie As Database.DBElement, ByRef ScrapeModifiers As Structures.ScrapeModifiers, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions) As Interfaces.ModuleResult_Data_Movie Implements Interfaces.IScraperModule_Data_Movie.Scraper_Movie
        logger.Trace("[MoviepilotDE_Data] [Scraper_Movie] [Start]")

        LoadSettings()

        Dim nMovie As New MediaContainers.Movie
        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(ScrapeOptions, ConfigScrapeOptions)

        If ScrapeModifiers.MainNFO Then
            nMovie = _scraper.GetMovieInfo(oDBMovie.Movie.OriginalTitle, oDBMovie.Movie.Title, oDBMovie.Movie.Year, FilteredOptions, oDBMovie.Language)
        End If

        logger.Trace("[MoviepilotDE_Data] [Scraper_Movie] [Done]")
        Return New Interfaces.ModuleResult_Data_Movie With {.Result = nMovie}
    End Function

    Public Sub ScraperOrderChanged() Implements Interfaces.IScraperModule_Data_Movie.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Database.DBElement, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Data_Movie.GetMovieStudio
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.IScraperModule_Data_Movie.GetTMDBID
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

#End Region 'Methods

End Class