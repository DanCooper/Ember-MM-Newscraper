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

Imports System.IO
Imports EmberAPI
Imports WatTmdb
Imports EmberScraperModule.TMDBg

Public Class TMDB_Poster
    Implements Interfaces.EmberMovieScraperModule_Poster


#Region "Fields"

    Public Shared ConfigOptions As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    Private TMDBId As String
    Private _TMDBg As TMDBg.Scraper
    Private TMDB As TMDB.Scraper

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private _MySettings As New sMySettings
    Private _Name As String = "TMDB_Poster"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmTMDBMediaSettingsHolder
    Private _TMDBConf As V3.TmdbConfiguration
    Private _TMDBConfE As V3.TmdbConfiguration
    Private _TMDBApi As V3.Tmdb
    Private _TMDBApiE As V3.Tmdb

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.EmberMovieScraperModule_Poster.ModuleSettingsChanged

    Public Event MovieScraperEvent(ByVal eType As Enums.MovieScraperEventType, ByVal Parameter As Object) Implements Interfaces.EmberMovieScraperModule_Poster.MovieScraperEvent

    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberMovieScraperModule_Poster.ScraperSetupChanged

    Public Event SetupNeedsRestart() Implements Interfaces.EmberMovieScraperModule_Poster.SetupNeedsRestart

    Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.EmberMovieScraperModule_Poster.PostersDownloaded

    Public Event ProgressUpdated(ByVal iPercent As Integer) Implements Interfaces.EmberMovieScraperModule_Poster.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.EmberMovieScraperModule_Poster.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.EmberMovieScraperModule_Poster.ModuleVersion
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.EmberMovieScraperModule_Poster.ScraperEnabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"
    Function QueryScraperCapabilities(ByVal cap As Enums.PostScraperCapabilities) As Boolean Implements Interfaces.EmberMovieScraperModule_Poster.QueryScraperCapabilities
        Select Case cap
            Case Enums.PostScraperCapabilities.Fanart
                Return ConfigScrapeModifier.Fanart 'True ' ConfigScrapeModifier.Fanart
            Case Enums.PostScraperCapabilities.Poster
                Return ConfigScrapeModifier.Poster 'True 'ConfigScrapeModifier.Poster
        End Select
        Return False
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.EmberMovieScraperModule_Poster.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
        'Must be after Load settings to retrieve the correct API key
        _TMDBApi = New WatTmdb.V3.Tmdb(_MySettings.TMDBAPIKey, _MySettings.TMDBLanguage)
        If IsNothing(_TMDBApi) Then
            Master.eLog.WriteToErrorLog(Master.eLang.GetString(119, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message, "Info")
        Else
            If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                Master.eLog.WriteToErrorLog(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString(), "Error")
            End If
        End If
        _TMDBConf = _TMDBApi.GetConfiguration()
        _TMDBApiE = New WatTmdb.V3.Tmdb(_MySettings.TMDBAPIKey)
        _TMDBConfE = _TMDBApiE.GetConfiguration()
        _TMDBg = New TMDBg.Scraper(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE)
        TMDB = New TMDB.Scraper(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _MySettings)
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberMovieScraperModule_Poster.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup = New frmTMDBMediaSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.chkScrapePoster.Checked = ConfigScrapeModifier.Poster
        _setup.chkScrapeFanart.Checked = ConfigScrapeModifier.Fanart

        If String.IsNullOrEmpty(_MySettings.TMDBAPIKey) Then
            _MySettings.TMDBAPIKey = Master.eLang.GetString(122, "Get your API Key from www.themoviedb.org")
        End If
        _setup.txtTMDBApiKey.Text = _MySettings.TMDBAPIKey
        _setup.cbTMDBPrefLanguage.Text = _MySettings.TMDBLanguage
        _setup.chkFallBackEng.Checked = _MySettings.FallBackEng
        _setup.Lang = _setup.cbTMDBPrefLanguage.Text
        _setup.API = _setup.txtTMDBApiKey.Text

        _setup.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "Scraper")
        Spanel.Text = Master.eLang.GetString(104, "TMDB")
        Spanel.Prefix = "TMDBMovieMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlMovieMedia"
        Spanel.Type = Master.eLang.GetString(36, "Movies", True)
        Spanel.ImageIndex = If(Me._ScraperEnabled, 9, 10)
        Spanel.Panel = Me._setup.pnlSettings

        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return Spanel
    End Function

    Sub LoadSettings()

        _MySettings.TMDBAPIKey = AdvancedSettings.GetSetting("TMDBAPIKey", "Get your API Key from http://www.themoviedb.org")
        _MySettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False)
        _MySettings.TMDBLanguage = AdvancedSettings.GetSetting("TMDBLanguage", "en")

        ConfigScrapeModifier.DoSearch = True
        ConfigScrapeModifier.Meta = True
        ConfigScrapeModifier.NFO = True
        ConfigScrapeModifier.Extra = True
        ConfigScrapeModifier.Actors = True

        ConfigScrapeModifier.Poster = AdvancedSettings.GetBooleanSetting("DoPoster", True)
        ConfigScrapeModifier.Fanart = AdvancedSettings.GetBooleanSetting("DoFanart", True)
        ConfigScrapeModifier.Trailer = False

    End Sub

    Function Scraper(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.PostScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule_Poster.Scraper
        'LoadSettings()

        LoadSettings()
        If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            _TMDBg.GetMovieID(DBMovie)
        End If

        ImageList = TMDB.GetTMDBImages(DBMovie.Movie.TMDBID, Type)

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSettings()
        AdvancedSettings.SetBooleanSetting("DoPoster", ConfigScrapeModifier.Poster)
        AdvancedSettings.SetBooleanSetting("DoFanart", ConfigScrapeModifier.Fanart)

        AdvancedSettings.SetSetting("TMDBAPIKey", _MySettings.TMDBAPIKey)
        AdvancedSettings.SetBooleanSetting("FallBackEn", _MySettings.FallBackEng)
        AdvancedSettings.SetSetting("TMDBLanguage", _MySettings.TMDBLanguage)
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberMovieScraperModule_Poster.SaveSetupScraper
        If Not String.IsNullOrEmpty(_setup.txtTMDBApiKey.Text) Then
            _MySettings.TMDBAPIKey = _setup.txtTMDBApiKey.Text
        Else
            _MySettings.TMDBAPIKey = Master.eLang.GetString(122, "Get your API Key from www.themoviedb.org")
        End If
        _MySettings.TMDBLanguage = _setup.cbTMDBPrefLanguage.Text
        _MySettings.FallBackEng = _setup.chkFallBackEng.Checked
        ConfigScrapeModifier.Poster = _setup.chkScrapePoster.Checked
        ConfigScrapeModifier.Fanart = _setup.chkScrapeFanart.Checked
        SaveSettings()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.EmberMovieScraperModule_Poster.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"
        Dim TMDBAPIKey As String
        Dim TMDBLanguage As String
        Dim FallBackEng As Boolean
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class