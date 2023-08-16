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

Public Class Addon
    Implements Interfaces.IAddon_Data_Scraper_Movie


#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()
    Public Shared ConfigScrapeOptions As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifiers As New Structures.ScrapeModifiers
    Public Shared _AssemblyFileName As String

    Private _Name As String = "MoviepilotDE_Data"
    Private _scraper As New Scraper
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder

    Private _AddonSettings As New AddonSettings

#End Region 'Fields

#Region "Events"

    Public Event AddonSettingsChanged() Implements Interfaces.IAddon_Data_Scraper_Movie.AddonSettingsChanged
    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.IAddon_Data_Scraper_Movie.AddonStateChanged
    Public Event AddonNeedsRestart() Implements Interfaces.IAddon_Data_Scraper_Movie.AddonNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property Name() As String Implements Interfaces.IAddon_Data_Scraper_Movie.Name
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property Version() As String Implements Interfaces.IAddon_Data_Scraper_Movie.Version
        Get
            Return FileVersionInfo.GetVersionInfo(Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property Enabled() As Boolean Implements Interfaces.IAddon_Data_Scraper_Movie.Enabled
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
        RaiseEvent AddonSettingsChanged()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        Enabled = state
        RaiseEvent SetupScraperChanged(String.Concat(_Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.IAddon_Data_Scraper_Movie.Init
        _AssemblyFileName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IAddon_Data_Scraper_Movie.InjectSettingsPanel
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _ScraperEnabled
        _setup.chkCertifications.Checked = ConfigScrapeOptions.bMainCertifications
        _setup.chkOutline.Checked = ConfigScrapeOptions.bMainOutline
        _setup.chkPlot.Checked = ConfigScrapeOptions.bMainPlot

        _setup.orderChanged()

        SPanel.UniqueName = String.Concat(_Name, "Scraper")
        SPanel.Title = "Moviepilot.de"
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
        ConfigScrapeOptions.bMainOutline = _AddonSettings.GetBooleanSetting("DoOutline", True)
        ConfigScrapeOptions.bMainPlot = _AddonSettings.GetBooleanSetting("DoPlot", True)
        ConfigScrapeOptions.bMainCertifications = _AddonSettings.GetBooleanSetting("DoCert", True)
    End Sub

    Sub SaveSettings()
        _AddonSettings.SetBooleanSetting("DoOutline", ConfigScrapeOptions.bMainOutline)
        _AddonSettings.SetBooleanSetting("DoPlot", ConfigScrapeOptions.bMainPlot)
        _AddonSettings.SetBooleanSetting("DoCert", ConfigScrapeOptions.bMainCertifications)
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent AddonSettingsChanged()
    End Sub

    Sub SaveSettings(ByVal DoDispose As Boolean) Implements Interfaces.IAddon_Data_Scraper_Movie.SaveSettings
        ConfigScrapeOptions.bMainCertifications = _setup.chkCertifications.Checked
        ConfigScrapeOptions.bMainOutline = _setup.chkOutline.Checked
        ConfigScrapeOptions.bMainPlot = _setup.chkPlot.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Function Scraper_Movie(ByRef dbElement As Database.DBElement,
                           ByRef scrapeModifiers As Structures.ScrapeModifiers,
                           ByRef scrapeType As Enums.ScrapeType,
                           ByRef scrapeOptions As Structures.ScrapeOptions
                           ) As Interfaces.AddonResult_Data_Scraper_Movie Implements Interfaces.IAddon_Data_Scraper_Movie.Scraper_Movie
        logger.Trace("[Moviepilot.de_Data] [Scraper_Movie] [Start]")

        LoadSettings()

        Dim FilteredOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(scrapeOptions, ConfigScrapeOptions)
        Dim Result As MediaContainers.Movie = Nothing

        If scrapeModifiers.MainNFO Then
            Result = _scraper.GetMovieInfo(dbElement.Movie.OriginalTitle, dbElement.Movie.Title, dbElement.Movie.Year, FilteredOptions, dbElement.Language)
        End If

        If Result IsNot Nothing Then
            logger.Trace("[Moviepilot.de_Data] [Scraper_Movie] [Done]")
            Return New Interfaces.AddonResult_Data_Scraper_Movie(Result)
        Else
            logger.Trace("[Moviepilot.de_Data] [Scraper_Movie] [Abort] No search result found")
            Return New Interfaces.AddonResult_Data_Scraper_Movie(Interfaces.ResultStatus.NoResult)
        End If
    End Function

    Public Sub OrderChanged() Implements Interfaces.IAddon_Data_Scraper_Movie.OrderChanged
        _setup.orderChanged()
    End Sub

    Function GetTMDbIdByIMDbId(ByVal imdbId As String, ByRef tmdbId As Integer) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Data_Scraper_Movie.GetTMDbIdByIMDbId
        Return New Interfaces.AddonResult_Generic
    End Function

#End Region 'Methods

End Class