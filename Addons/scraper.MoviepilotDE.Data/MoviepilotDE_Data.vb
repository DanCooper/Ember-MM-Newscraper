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
Imports NLog
Imports EmberAPI

Public Class MoviepilotDE_Data
    Implements Interfaces.ScraperModule_Data_Movie


#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Shared ConfigOptions As New Structures.ScrapeOptions_Movie
    Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    Private _Name As String = "MoviepilotDE_Data"
    Private _scraper As New MoviepilotDE.Scraper
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.ScraperModule_Data_Movie.ModuleSettingsChanged

    Public Event MovieScraperEvent(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_Movie.ScraperEvent

    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_Movie.ScraperSetupChanged

    Public Event SetupNeedsRestart() Implements Interfaces.ScraperModule_Data_Movie.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Data_Movie.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.ScraperModule_Data_Movie.ScraperEnabled
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
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_Movie.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _ScraperEnabled
        _setup.chkCertification.Checked = ConfigOptions.bCert
        _setup.chkOutline.Checked = ConfigOptions.bOutline
        _setup.chkPlot.Checked = ConfigOptions.bPlot

        _setup.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = "Moviepilot"
        SPanel.Prefix = "MoviepilotDEMovieInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieData"
        SPanel.Type = Master.eLang.GetString(36, "Movies")
        SPanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        SPanel.Panel = _setup.pnlSettings

        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Sub LoadSettings()
        ConfigOptions.bOutline = clsAdvancedSettings.GetBooleanSetting("DoOutline", True)
        ConfigOptions.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True)
        ConfigOptions.bCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True)
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
            settings.SetBooleanSetting("DoCert", ConfigOptions.bCert)
        End Using
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        ConfigOptions.bCert = _setup.chkCertification.Checked
        ConfigOptions.bOutline = _setup.chkOutline.Checked
        ConfigOptions.bPlot = _setup.chkPlot.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from Moviepilot.de (German site)
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper(ByRef oDBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV, ByRef ScrapeOptions As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.Scraper
        logger.Trace("Started MoviepilotDE Scraper")

        LoadSettings()

        Dim FilteredOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions)

        If ScrapeModifier.MainNFO Then
            _scraper.GetMovieInfo(oDBMovie.Movie.OriginalTitle, oDBMovie.Movie.Title, oDBMovie.Movie.Year, nMovie, FilteredOptions)
        End If

        logger.Trace("Finished MoviepilotDE Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDBID
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

#End Region 'Methods

End Class