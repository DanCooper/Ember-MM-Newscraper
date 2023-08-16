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
' ###############################################################################

Imports EmberAPI
Imports NLog

Public Class Addon
    Implements Interfaces.IAddon_Trailer_Scraper_Movie


#Region "Fields"
    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared ConfigScrapeModifiers As New Structures.ScrapeModifiers
    Public Shared _AssemblyName As String

    Private _Name As String = "YouTube_Trailer"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder

    Private _AddonSettings As New AddonSettings

#End Region 'Fields

#Region "Events"

    Public Event AddonSettingsChanged() Implements Interfaces.IAddon_Trailer_Scraper_Movie.AddonSettingsChanged
    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.IAddon_Trailer_Scraper_Movie.AddonStateChanged
    Public Event AddonNeedsRestart() Implements Interfaces.IAddon_Trailer_Scraper_Movie.AddonNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.IAddon_Trailer_Scraper_Movie.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.IAddon_Trailer_Scraper_Movie.ModuleVersion
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.IAddon_Trailer_Scraper_Movie.ScraperEnabled
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

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent AddonNeedsRestart()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(_Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.IAddon_Trailer_Scraper_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IAddon_Trailer_Scraper_Movie.InjectSettingsPanel
        Dim Spanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _ScraperEnabled

        _setup.orderChanged()

        Spanel.UniqueName = String.Concat(_Name, "Scraper")
        Spanel.Title = "YouTube.com"
        Spanel.Order = 110
        Spanel.Parent = "pnlMovieTrailer"
        Spanel.Type = Master.eLang.GetString(36, "Movies")
        Spanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        Spanel.Panel = _setup.pnlSettings

        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return Spanel
    End Function

    Sub LoadSettings()
        ConfigScrapeModifiers.MainTrailer = Master.eAdvancedSettings.GetBooleanSetting("DoTrailer", True)
    End Sub

    Function Scraper_Movie(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef TrailerList As List(Of MediaContainers.MediaFile)) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Trailer_Scraper_Movie.Scraper
        logger.Trace("[YouTube_Trailer] [Scraper_Movie] [Start]")

        LoadSettings()

        If DBMovie.Movie.TitleSpecified Then
            Dim _scraper As New Scraper()
            TrailerList = _scraper.GetTrailers(DBMovie.Movie.Title)
        End If

        logger.Trace("[YouTube_Trailer] [Scraper_Movie] [Done]")
        Return New Interfaces.AddonResult_Generic
    End Function

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoTrailer", ConfigScrapeModifiers.MainTrailer)
        End Using
    End Sub

    Sub SaveSettings(ByVal DoDispose As Boolean) Implements Interfaces.IAddon_Trailer_Scraper_Movie.SaveSettings
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.IAddon_Trailer_Scraper_Movie.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

End Class