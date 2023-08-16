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
    Implements Interfaces.IAddon_Theme_Scraper_Movie
    Implements Interfaces.IAddon_Theme_Scraper_TV


#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifiers
    Public Shared ConfigScrapeModifier_TV As New Structures.ScrapeModifiers
    Public Shared _AssemblyName As String

    Private _Name As String = "TelevisionTunes_Theme"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_TV As Boolean = False
    Private _setup_Movie As frmSettingsHolder_Movie
    Private _setup_TV As frmSettingsHolder_TV

    Private _AddonSettings As New AddonSettings

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.IAddon_Theme_Scraper_Movie.AddonSettingsChanged
    Public Event SetupScraperChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.IAddon_Theme_Scraper_Movie.AddonStateChanged
    Public Event SetupNeedsRestart_Movie() Implements Interfaces.IAddon_Theme_Scraper_Movie.AddonNeedsRestart

    'TV part
    Public Event ModuleSettingsChanged_TV() Implements Interfaces.IAddon_Theme_Scraper_TV.AddonSettingsChanged
    Public Event SetupScraperChanged_TV(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.IAddon_Theme_Scraper_TV.AddonStateChanged
    Public Event SetupNeedsRestart_TV() Implements Interfaces.IAddon_Theme_Scraper_TV.AddonNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.IAddon_Theme_Scraper_TV.ModuleName, Interfaces.IAddon_Theme_Scraper_Movie.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.IAddon_Theme_Scraper_TV.ModuleVersion, Interfaces.IAddon_Theme_Scraper_Movie.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_TV() As Boolean Implements Interfaces.IAddon_Theme_Scraper_TV.ScraperEnabled
        Get
            Return _ScraperEnabled_TV
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_TV = value
        End Set
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.IAddon_Theme_Scraper_Movie.ScraperEnabled
        Get
            Return _ScraperEnabled_Movie
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_Movie = value
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

    Private Sub Handle_SetupScraperChanged_Movie(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_Movie = state
        RaiseEvent SetupScraperChanged_Movie(String.Concat(Me._Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_TV(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_TV = state
        RaiseEvent SetupScraperChanged_TV(String.Concat(Me._Name, "_TV"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.IAddon_Theme_Scraper_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
    End Sub

    Sub Init_TV(ByVal sAssemblyName As String) Implements Interfaces.IAddon_Theme_Scraper_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings_TV()
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.IAddon_Theme_Scraper_Movie.InjectSettingsPanel
        Dim SPanel As New Containers.SettingsPanel
        _setup_Movie = New frmSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.chkEnabled.Checked = _ScraperEnabled_Movie

        _setup_Movie.orderChanged()

        SPanel.UniqueName = String.Concat(Me._Name, "_Movie")
        SPanel.Title = "TelevisionTunes.com"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieTheme"
        SPanel.Type = Master.eLang.GetString(36, "Movies")
        SPanel.ImageIndex = If(_ScraperEnabled_Movie, 9, 10)
        SPanel.Panel = _setup_Movie.pnlSettings
        AddHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
        AddHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
        Return SPanel
    End Function

    Function InjectSetupScraper_TV() As Containers.SettingsPanel Implements Interfaces.IAddon_Theme_Scraper_TV.InjectSettingsPanel
        Dim SPanel As New Containers.SettingsPanel
        _setup_TV = New frmSettingsHolder_TV
        LoadSettings_TV()
        _setup_TV.chkEnabled.Checked = _ScraperEnabled_TV

        _setup_TV.orderChanged()

        SPanel.UniqueName = String.Concat(Me._Name, "_TV")
        SPanel.Title = "TelevisionTunes.com"
        SPanel.Order = 110
        SPanel.Parent = "pnlTVTheme"
        SPanel.Type = Master.eLang.GetString(653, "TV Shows")
        SPanel.ImageIndex = If(_ScraperEnabled_TV, 9, 10)
        SPanel.Panel = _setup_TV.pnlSettings
        AddHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
        AddHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
        Return SPanel
    End Function

    Sub LoadSettings_Movie()
        ConfigScrapeModifier_Movie.MainTheme = Master.eAdvancedSettings.GetBooleanSetting("DoTheme", True, , Enums.ContentType.Movie)
    End Sub

    Sub LoadSettings_TV()
        ConfigScrapeModifier_TV.MainTheme = Master.eAdvancedSettings.GetBooleanSetting("DoTheme", True, , Enums.ContentType.TV)
    End Sub

    Sub SaveSettings_Movie()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoTheme", ConfigScrapeModifier_Movie.MainTheme, , , Enums.ContentType.Movie)
        End Using
    End Sub

    Sub SaveSettings_TV()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoTheme", ConfigScrapeModifier_TV.MainTheme, , , Enums.ContentType.TV)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.IAddon_Theme_Scraper_Movie.SaveSettings
        SaveSettings_Movie()
        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_TV(ByVal DoDispose As Boolean) Implements Interfaces.IAddon_Theme_Scraper_TV.SaveSettings
        SaveSettings_TV()
        If DoDispose Then
            RemoveHandler _setup_TV.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_TV
            RemoveHandler _setup_TV.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_TV
            _setup_TV.Dispose()
        End If
    End Sub

    Function Scraper_Movie(ByRef DBMovie As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef ThemeList As List(Of MediaContainers.MediaFile)) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Theme_Scraper_Movie.Scraper
        logger.Trace("[TelevisionTunes_Theme] [Scraper_Movie] [Start]")

        Dim tTelevisionTunes As New Scraper(DBMovie.Movie.OriginalTitle)

        If tTelevisionTunes.ThemeList.Count > 0 Then
            ThemeList = tTelevisionTunes.ThemeList
        End If

        logger.Trace("[TelevisionTunes_Theme] [Scraper_Movie] [Done]")
        Return New Interfaces.AddonResult_Generic
    End Function

    Function Scraper_TV(ByRef DBTV As Database.DBElement, ByVal Type As Enums.ModifierType, ByRef ThemeList As List(Of MediaContainers.MediaFile)) As Interfaces.AddonResult_Generic Implements Interfaces.IAddon_Theme_Scraper_TV.Scraper
        logger.Trace("[TelevisionTunes_Theme] [Scraper_TV] [Start]")

        Dim tTelevisionTunes As New Scraper(DBTV.TVShow.Title)

        If tTelevisionTunes.ThemeList.Count > 0 Then
            ThemeList = tTelevisionTunes.ThemeList
        End If

        logger.Trace("[TelevisionTunes_Theme] [Scraper_TV] [Done]")
        Return New Interfaces.AddonResult_Generic
    End Function

    Public Sub ScraperOrderChanged_Movie() Implements Interfaces.IAddon_Theme_Scraper_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_TV() Implements Interfaces.IAddon_Theme_Scraper_TV.ScraperOrderChanged
        _setup_TV.orderChanged()
    End Sub

#End Region 'Methods

End Class