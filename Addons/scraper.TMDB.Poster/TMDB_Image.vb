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
Imports NLog
Imports System.Diagnostics

Public Class TMDB_Image
    Implements Interfaces.ScraperModule_Image_Movie
    Implements Interfaces.ScraperModule_Image_MovieSet


#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared ConfigScrapeModifier_Movie As New Structures.ScrapeModifier
    Public Shared ConfigScrapeModifier_MovieSet As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    Private TMDBId As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private strPrivateAPIKey As String = String.Empty
    Private _MySettings_Movie As New sMySettings
    Private _MySettings_MovieSet As New sMySettings
    Private _Name As String = "TMDB_Image"
    Private _ScraperEnabled_Movie As Boolean = False
    Private _ScraperEnabled_MovieSet As Boolean = False
    Private _setup_Movie As frmTMDBMediaSettingsHolder_Movie
    Private _setup_MovieSet As frmTMDBMediaSettingsHolder_MovieSet

#End Region 'Fields

#Region "Events"

    'Movie part
    Public Event ModuleSettingsChanged_Movie() Implements Interfaces.ScraperModule_Image_Movie.ModuleSettingsChanged

    Public Event MovieScraperEvent_Movie(ByVal eType As Enums.ScraperEventType_Movie, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_Movie.ScraperEvent

    Public Event SetupScraperChanged_Movie(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_Movie.ScraperSetupChanged

    Public Event SetupNeedsRestart_Movie() Implements Interfaces.ScraperModule_Image_Movie.SetupNeedsRestart

    Public Event ImagesDownloaded_Movie(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_Movie.ImagesDownloaded

    Public Event ProgressUpdated_Movie(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_Movie.ProgressUpdated

    'MovieSet part
    Public Event ModuleSettingsChanged_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.ModuleSettingsChanged

    Public Event MovieScraperEvent_MovieSet(ByVal eType As Enums.ScraperEventType_MovieSet, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEvent

    Public Event SetupScraperChanged_MovieSet(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ScraperSetupChanged

    Public Event SetupNeedsRestart_MovieSet() Implements Interfaces.ScraperModule_Image_MovieSet.SetupNeedsRestart

    Public Event ImagesDownloaded_MovieSet(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_MovieSet.ImagesDownloaded

    Public Event ProgressUpdated_MovieSet(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_MovieSet.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleName, Interfaces.ScraperModule_Image_MovieSet.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleVersion, Interfaces.ScraperModule_Image_MovieSet.ModuleVersion
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled_Movie() As Boolean Implements Interfaces.ScraperModule_Image_Movie.ScraperEnabled
        Get
            Return _ScraperEnabled_Movie
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_Movie = value
        End Set
    End Property

    Property ScraperEnabled_MovieSet() As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.ScraperEnabled
        Get
            Return _ScraperEnabled_MovieSet
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled_MovieSet = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Function QueryScraperCapabilities_Movie(ByVal cap As Enums.ScraperCapabilities) As Boolean Implements Interfaces.ScraperModule_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ScraperCapabilities.Fanart
                Return ConfigScrapeModifier_Movie.Fanart
            Case Enums.ScraperCapabilities.Poster
                Return ConfigScrapeModifier_Movie.Poster
        End Select
        Return False
    End Function

    Function QueryScraperCapabilities_MovieSet(ByVal cap As Enums.ScraperCapabilities) As Boolean Implements Interfaces.ScraperModule_Image_MovieSet.QueryScraperCapabilities
        Select Case cap
            Case Enums.ScraperCapabilities.Fanart
                Return ConfigScrapeModifier_MovieSet.Fanart
            Case Enums.ScraperCapabilities.Poster
                Return ConfigScrapeModifier_MovieSet.Poster
        End Select
        Return False
    End Function

    Private Sub Handle_ModuleSettingsChanged_Movie()
        RaiseEvent ModuleSettingsChanged_Movie()
    End Sub

    Private Sub Handle_SetupNeedsRestart_Movie()
        RaiseEvent SetupNeedsRestart_Movie()
    End Sub

    Private Sub Handle_ModuleSettingsChanged_MovieSet()
        RaiseEvent ModuleSettingsChanged_MovieSet()
    End Sub

    Private Sub Handle_SetupNeedsRestart_MovieSet()
        RaiseEvent SetupNeedsRestart_MovieSet()
    End Sub

    Private Sub Handle_SetupScraperChanged_Movie(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_Movie = state
        RaiseEvent SetupScraperChanged_Movie(String.Concat(Me._Name, "_Movie"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged_MovieSet(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled_MovieSet = state
        RaiseEvent SetupScraperChanged_MovieSet(String.Concat(Me._Name, "_MovieSet"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings_Movie()
    End Sub

    Sub Init_MovieSet(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_MovieSet.Init
        _AssemblyName = sAssemblyName
        LoadSettings_MovieSet()
    End Sub

    Function InjectSetupScraper_Movie() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_Movie.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_Movie = New frmTMDBMediaSettingsHolder_Movie
        LoadSettings_Movie()
        _setup_Movie.cbEnabled.Checked = _ScraperEnabled_Movie
        _setup_Movie.chkScrapePoster.Checked = ConfigScrapeModifier_Movie.Poster
        _setup_Movie.chkScrapeFanart.Checked = ConfigScrapeModifier_Movie.Fanart
        _setup_Movie.txtApiKey.Text = strPrivateAPIKey
        _setup_Movie.cbPrefLanguage.Text = _MySettings_Movie.PrefLanguage
        _setup_Movie.chkGetBlankImages.Checked = _MySettings_Movie.GetBlankImages
        _setup_Movie.chkGetEnglishImages.Checked = _MySettings_Movie.GetEnglishImages
        _setup_Movie.chkPrefLanguageOnly.Checked = _MySettings_Movie.PrefLanguageOnly
        _setup_Movie.Lang = _setup_Movie.cbPrefLanguage.Text
        _setup_Movie.API = _setup_Movie.txtApiKey.Text

        _setup_Movie.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_Movie")
        Spanel.Text = Master.eLang.GetString(937, "TMDB")
        Spanel.Prefix = "TMDBMovieMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlMovieMedia"
        Spanel.Type = Master.eLang.GetString(36, "Movies")
        Spanel.ImageIndex = If(Me._ScraperEnabled_Movie, 9, 10)
        Spanel.Panel = Me._setup_Movie.pnlSettings

        AddHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
        AddHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
        AddHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
        Return Spanel
    End Function

    Function InjectSetupScraper_MovieSet() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_MovieSet.InjectSetupScraper
        Dim Spanel As New Containers.SettingsPanel
        _setup_MovieSet = New frmTMDBMediaSettingsHolder_MovieSet
        LoadSettings_MovieSet()
        _setup_MovieSet.cbEnabled.Checked = _ScraperEnabled_MovieSet
        _setup_MovieSet.chkScrapePoster.Checked = ConfigScrapeModifier_MovieSet.Poster
        _setup_MovieSet.chkScrapeFanart.Checked = ConfigScrapeModifier_MovieSet.Fanart
        _setup_MovieSet.txtApiKey.Text = strPrivateAPIKey
        _setup_MovieSet.cbPrefLanguage.Text = _MySettings_MovieSet.PrefLanguage
        _setup_MovieSet.chkGetBlankImages.Checked = _MySettings_MovieSet.GetBlankImages
        _setup_MovieSet.chkGetEnglishImages.Checked = _MySettings_MovieSet.GetEnglishImages
        _setup_MovieSet.chkPrefLanguageOnly.Checked = _MySettings_MovieSet.PrefLanguageOnly
        _setup_MovieSet.Lang = _setup_MovieSet.cbPrefLanguage.Text
        _setup_MovieSet.API = _setup_MovieSet.txtApiKey.Text

        _setup_MovieSet.orderChanged()

        Spanel.Name = String.Concat(Me._Name, "_MovieSet")
        Spanel.Text = Master.eLang.GetString(937, "TMDB")
        Spanel.Prefix = "TMDBMovieSetMedia_"
        Spanel.Order = 110
        Spanel.Parent = "pnlMovieSetMedia"
        Spanel.Type = Master.eLang.GetString(1203, "MovieSets")
        Spanel.ImageIndex = If(Me._ScraperEnabled_MovieSet, 9, 10)
        Spanel.Panel = Me._setup_MovieSet.pnlSettings

        AddHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
        AddHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
        AddHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
        Return Spanel
    End Function

    Sub LoadSettings_Movie()

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "", , Enums.Content_Type.Movie)
        _MySettings_Movie.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
        _MySettings_Movie.GetBlankImages = clsAdvancedSettings.GetBooleanSetting("GetBlankImages", False, , Enums.Content_Type.Movie)
        _MySettings_Movie.GetEnglishImages = clsAdvancedSettings.GetBooleanSetting("GetEnglishImages", False, , Enums.Content_Type.Movie)
        _MySettings_Movie.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.Content_Type.Movie)
        _MySettings_Movie.PrefLanguageOnly = clsAdvancedSettings.GetBooleanSetting("PrefLanguageOnly", False, , Enums.Content_Type.Movie)

        ConfigScrapeModifier_Movie.Poster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.Content_Type.Movie)
        ConfigScrapeModifier_Movie.Fanart = clsAdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.Content_Type.Movie)

    End Sub

    Sub LoadSettings_MovieSet()

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "", , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
        _MySettings_MovieSet.GetBlankImages = clsAdvancedSettings.GetBooleanSetting("GetBlankImages", False, , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.GetEnglishImages = clsAdvancedSettings.GetBooleanSetting("GetEnglishImages", False, , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.PrefLanguage = clsAdvancedSettings.GetSetting("PrefLanguage", "en", , Enums.Content_Type.MovieSet)
        _MySettings_MovieSet.PrefLanguageOnly = clsAdvancedSettings.GetBooleanSetting("PrefLanguageOnly", False, , Enums.Content_Type.MovieSet)

        ConfigScrapeModifier_MovieSet.Poster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True, , Enums.Content_Type.MovieSet)
        ConfigScrapeModifier_MovieSet.Fanart = clsAdvancedSettings.GetBooleanSetting("DoFanart", True, , Enums.Content_Type.MovieSet)

    End Sub

    Async Function Scraper(ByVal DBMovie As Structures.DBMovie, ByVal Type As Enums.ScraperCapabilities, ByVal ImageList As List(Of MediaContainers.Image)) As Threading.Tasks.Task(Of Interfaces.ModuleResult) Implements Interfaces.ScraperModule_Image_Movie.Scraper
        ' Return Objects are
        ' DBMovie
        ' ImageList

        Dim RetO As Interfaces.ModuleResult

        logger.Trace("Started scrape TMDB")

        LoadSettings_Movie()

        If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            DBMovie.Movie.TMDBID = ModulesManager.Instance.GetMovieTMDBID(DBMovie.Movie.ID)
        End If

        If Not String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            Dim Settings As TMDB.Scraper.sMySettings_ForScraper
            Settings.GetBlankImages = _MySettings_Movie.GetBlankImages
            Settings.GetEnglishImages = _MySettings_Movie.GetEnglishImages
            Settings.APIKey = _MySettings_Movie.APIKey
            Settings.PrefLanguage = _MySettings_Movie.PrefLanguage
            Settings.PrefLanguageOnly = _MySettings_Movie.PrefLanguageOnly

            Dim _scraper As New TMDB.Scraper(Settings)

            ImageList = _scraper.GetTMDBImages(DBMovie.Movie.TMDBID, Type, Settings)
        End If

        logger.Trace("Finished TMDB Scraper")
        RetO = New Interfaces.ModuleResult
        RetO.breakChain = False
        RetO.Cancelled = False
        RetO.ReturnObj.Add(DBMovie)
        RetO.ReturnObj.Add(ImageList)
        Return RetO

    End Function

    Function Scraper(ByRef DBMovieSet As Structures.DBMovieSet, ByVal Type As Enums.ScraperCapabilities, ByRef ImageList As List(Of MediaContainers.Image)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_MovieSet.Scraper
        logger.Trace("Started scrape TMDB")

        LoadSettings_MovieSet()

        If String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then
            If Not IsNothing(DBMovieSet.Movies) AndAlso DBMovieSet.Movies.Count > 0 Then
                DBMovieSet.MovieSet.ID = ModulesManager.Instance.GetMovieCollectionID(DBMovieSet.Movies.Item(0).Movie.ID)
            End If
        End If

        If Not String.IsNullOrEmpty(DBMovieSet.MovieSet.ID) Then
            Dim Settings As TMDB.Scraper.sMySettings_ForScraper
            Settings.GetBlankImages = _MySettings_MovieSet.GetBlankImages
            Settings.GetEnglishImages = _MySettings_MovieSet.GetEnglishImages
            Settings.APIKey = _MySettings_MovieSet.APIKey
            Settings.PrefLanguage = _MySettings_MovieSet.PrefLanguage
            Settings.PrefLanguageOnly = _MySettings_MovieSet.PrefLanguageOnly

            Dim _scraper As New TMDB.Scraper(Settings)

            ImageList = _scraper.GetTMDBImages(DBMovieSet.MovieSet.ID, Type, Settings)
        End If

        logger.Trace("Finished TMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSettings_Movie()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier_Movie.Poster, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier_Movie.Fanart, , , Enums.Content_Type.Movie)

            settings.SetSetting("APIKey", _setup_Movie.txtApiKey.Text, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("GetBlankImages", _MySettings_Movie.GetBlankImages, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("GetEnglishImages", _MySettings_Movie.GetEnglishImages, , , Enums.Content_Type.Movie)
            settings.SetBooleanSetting("PrefLanguageOnly", _MySettings_Movie.PrefLanguageOnly, , , Enums.Content_Type.Movie)
            settings.SetSetting("PrefLanguage", _MySettings_Movie.PrefLanguage, , , Enums.Content_Type.Movie)
        End Using
    End Sub

    Sub SaveSettings_MovieSet()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier_MovieSet.Poster, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier_MovieSet.Fanart, , , Enums.Content_Type.MovieSet)

            settings.SetSetting("APIKey", _setup_MovieSet.txtApiKey.Text, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("GetBlankImages", _MySettings_MovieSet.GetBlankImages, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("GetEnglishImages", _MySettings_MovieSet.GetEnglishImages, , , Enums.Content_Type.MovieSet)
            settings.SetBooleanSetting("PrefLanguageOnly", _MySettings_MovieSet.PrefLanguageOnly, , , Enums.Content_Type.MovieSet)
            settings.SetSetting("PrefLanguage", _MySettings_MovieSet.PrefLanguage, , , Enums.Content_Type.MovieSet)
        End Using
    End Sub

    Sub SaveSetupScraper_Movie(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_Movie.SaveSetupScraper
        _MySettings_Movie.PrefLanguage = _setup_Movie.cbPrefLanguage.Text
        _MySettings_Movie.GetBlankImages = _setup_Movie.chkGetBlankImages.Checked
        _MySettings_Movie.GetEnglishImages = _setup_Movie.chkGetEnglishImages.Checked
        _MySettings_Movie.PrefLanguageOnly = _setup_Movie.chkPrefLanguageOnly.Checked
        ConfigScrapeModifier_Movie.Poster = _setup_Movie.chkScrapePoster.Checked
        ConfigScrapeModifier_Movie.Fanart = _setup_Movie.chkScrapeFanart.Checked
        SaveSettings_Movie()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup_Movie.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_Movie
            RemoveHandler _setup_Movie.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_Movie
            RemoveHandler _setup_Movie.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_Movie
            _setup_Movie.Dispose()
        End If
    End Sub

    Sub SaveSetupScraper_MovieSet(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_MovieSet.SaveSetupScraper
        _MySettings_MovieSet.PrefLanguage = _setup_MovieSet.cbPrefLanguage.Text
        _MySettings_MovieSet.GetBlankImages = _setup_MovieSet.chkGetBlankImages.Checked
        _MySettings_MovieSet.GetEnglishImages = _setup_MovieSet.chkGetEnglishImages.Checked
        _MySettings_MovieSet.PrefLanguageOnly = _setup_MovieSet.chkPrefLanguageOnly.Checked
        ConfigScrapeModifier_MovieSet.Poster = _setup_MovieSet.chkScrapePoster.Checked
        ConfigScrapeModifier_MovieSet.Fanart = _setup_MovieSet.chkScrapeFanart.Checked
        SaveSettings_MovieSet()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup_MovieSet.SetupScraperChanged, AddressOf Handle_SetupScraperChanged_MovieSet
            RemoveHandler _setup_MovieSet.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged_MovieSet
            RemoveHandler _setup_MovieSet.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart_MovieSet
            _setup_MovieSet.Dispose()
        End If
    End Sub

    Public Sub ScraperOrderChanged_Movie() Implements EmberAPI.Interfaces.ScraperModule_Image_Movie.ScraperOrderChanged
        _setup_Movie.orderChanged()
    End Sub

    Public Sub ScraperOrderChanged_MovieSet() Implements EmberAPI.Interfaces.ScraperModule_Image_MovieSet.ScraperOrderChanged
        _setup_MovieSet.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"
        Dim APIKey As String
        Dim PrefLanguage As String
        Dim PrefLanguageOnly As Boolean
        Dim GetEnglishImages As Boolean
        Dim GetBlankImages As Boolean
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class