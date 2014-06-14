﻿' ################################################################################
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
Imports EmberAPI
Imports RestSharp
Imports WatTmdb
Imports EmberScraperModule.TMDBg
Imports NLog
Imports System.Diagnostics

Public Class TMDB_Data
    Implements Interfaces.EmberMovieScraperModule_Data


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Shared ConfigOptions As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    Private TMDBId As String
    'Private IMDBid As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private strPrivateAPIKey As String = String.Empty
    Private _MySettings As New sMySettings
    Private _TMDBg As TMDBg.Scraper
    Private _Name As String = "TMDB_Data"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmTMDBInfoSettingsHolder
    Private _TMDBConf As V3.TmdbConfiguration
    Private _TMDBConfE As V3.TmdbConfiguration
    Private _TMDBApi As V3.Tmdb 'preferred language
    Private _TMDBApiE As V3.Tmdb 'english language
    Private _TMDBApiA As V3.Tmdb 'all languages

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.EmberMovieScraperModule_Data.ModuleSettingsChanged

    Public Event MovieScraperEvent(ByVal eType As Enums.MovieScraperEventType, ByVal Parameter As Object) Implements Interfaces.EmberMovieScraperModule_Data.MovieScraperEvent

    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberMovieScraperModule_Data.ScraperSetupChanged

    Public Event SetupNeedsRestart() Implements Interfaces.EmberMovieScraperModule_Data.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.EmberMovieScraperModule_Data.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.EmberMovieScraperModule_Data.ModuleVersion
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.EmberMovieScraperModule_Data.ScraperEnabled
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

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.EmberMovieScraperModule_Data.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
        'Must be after Load settings to retrieve the correct API key
        _TMDBApi = New WatTmdb.V3.Tmdb(_MySettings.TMDBAPIKey, _MySettings.TMDBLanguage)
        If IsNothing(_TMDBApi) Then
            logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message)
        Else
            If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                logger.Error(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString())
            End If
        End If
        _TMDBConf = _TMDBApi.GetConfiguration()
        _TMDBApiE = New WatTmdb.V3.Tmdb(_MySettings.TMDBAPIKey)
        _TMDBConfE = _TMDBApiE.GetConfiguration()
        _TMDBApiA = New WatTmdb.V3.Tmdb(_MySettings.TMDBAPIKey, "")
        _TMDBg = New TMDBg.Scraper(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _TMDBApiA)
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberMovieScraperModule_Data.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmTMDBInfoSettingsHolder
        LoadSettings()
        _setup.API = _setup.txtTMDBApiKey.Text
        _setup.Lang = _setup.cbTMDBPrefLanguage.Text
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.cbTMDBPrefLanguage.Text = _MySettings.TMDBLanguage
        _setup.chkCast.Checked = ConfigOptions.bFullCast
        _setup.chkCollection.Checked = ConfigOptions.bCollection
        _setup.chkCountry.Checked = ConfigOptions.bCountry
        _setup.chkCrew.Checked = ConfigOptions.bFullCrew
        _setup.chkFallBackEng.Checked = _MySettings.FallBackEng
        _setup.chkGenre.Checked = ConfigOptions.bGenre
        _setup.chkGetAdultItems.Checked = _MySettings.GetAdultItems
        _setup.chkMPAA.Checked = ConfigOptions.bMPAA
        _setup.chkPlot.Checked = ConfigOptions.bPlot
        _setup.chkRating.Checked = ConfigOptions.bRating
        _setup.chkRelease.Checked = ConfigOptions.bRelease
        _setup.chkRuntime.Checked = ConfigOptions.bRuntime
        _setup.chkStudio.Checked = ConfigOptions.bStudio
        _setup.chkTMDBCleanPlotOutline.Checked = ConfigOptions.bCleanPlotOutline
        _setup.chkTagline.Checked = ConfigOptions.bTagline
        _setup.chkTitle.Checked = ConfigOptions.bTitle
        _setup.chkTrailer.Checked = ConfigOptions.bTrailer
        _setup.chkVotes.Checked = ConfigOptions.bVotes
        _setup.chkYear.Checked = ConfigOptions.bYear
        _setup.txtTMDBApiKey.Text = strPrivateAPIKey
        _setup.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = Master.eLang.GetString(937, "TMDB")
        SPanel.Prefix = "TMDBMovieInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieData"
        SPanel.Type = Master.eLang.GetString(36, "Movies")
        SPanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return SPanel
    End Function

    Sub LoadSettings()
        ConfigOptions.bCast = AdvancedSettings.GetBooleanSetting("DoCast", True)
        ConfigOptions.bCert = AdvancedSettings.GetBooleanSetting("DoCert", True)
        ConfigOptions.bCleanPlotOutline = AdvancedSettings.GetBooleanSetting("CleanPlotOutline", True)
        ConfigOptions.bCollection = AdvancedSettings.GetBooleanSetting("DoCollection", True)
        ConfigOptions.bCountry = AdvancedSettings.GetBooleanSetting("DoCountry", True)
        ConfigOptions.bDirector = AdvancedSettings.GetBooleanSetting("DoDirector", True)
        ConfigOptions.bFullCast = AdvancedSettings.GetBooleanSetting("DoFullCast", True)
        ConfigOptions.bFullCast = AdvancedSettings.GetBooleanSetting("FullCast", True)
        ConfigOptions.bFullCrew = AdvancedSettings.GetBooleanSetting("DoFullCrews", True)
        ConfigOptions.bFullCrew = AdvancedSettings.GetBooleanSetting("FullCrew", True)
        ConfigOptions.bGenre = AdvancedSettings.GetBooleanSetting("DoGenres", True)
        ConfigOptions.bMPAA = AdvancedSettings.GetBooleanSetting("DoMPAA", True)
        ConfigOptions.bMusicBy = AdvancedSettings.GetBooleanSetting("DoMusic", True)
        ConfigOptions.bOtherCrew = AdvancedSettings.GetBooleanSetting("DoOtherCrews", True)
        ConfigOptions.bOutline = AdvancedSettings.GetBooleanSetting("DoOutline", True)
        ConfigOptions.bPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True)
        ConfigOptions.bProducers = AdvancedSettings.GetBooleanSetting("DoProducers", True)
        ConfigOptions.bRating = AdvancedSettings.GetBooleanSetting("DoRating", True)
        ConfigOptions.bRelease = AdvancedSettings.GetBooleanSetting("DoRelease", True)
        ConfigOptions.bRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True)
        ConfigOptions.bStudio = AdvancedSettings.GetBooleanSetting("DoStudio", True)
        ConfigOptions.bTagline = AdvancedSettings.GetBooleanSetting("DoTagline", True)
        ConfigOptions.bTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True)
        ConfigOptions.bTop250 = AdvancedSettings.GetBooleanSetting("DoTop250", True)
        ConfigOptions.bTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True)
        ConfigOptions.bVotes = AdvancedSettings.GetBooleanSetting("DoVotes", True)
        ConfigOptions.bWriters = AdvancedSettings.GetBooleanSetting("DoWriters", True)
        ConfigOptions.bYear = AdvancedSettings.GetBooleanSetting("DoYear", True)

        strPrivateAPIKey = AdvancedSettings.GetSetting("TMDBAPIKey", "")
        _MySettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False)
        _MySettings.GetAdultItems = AdvancedSettings.GetBooleanSetting("GetAdultItems", False)
        _MySettings.TMDBAPIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "44810eefccd9cb1fa1d57e7b0d67b08d", strPrivateAPIKey)
        _MySettings.TMDBLanguage = AdvancedSettings.GetSetting("TMDBLanguage", "en")
        ConfigScrapeModifier.DoSearch = True
        ConfigScrapeModifier.Meta = True
        ConfigScrapeModifier.NFO = True
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("CleanPlotOutline", ConfigOptions.bCleanPlotOutline)
            settings.SetBooleanSetting("DoCast", ConfigOptions.bCast)
            settings.SetBooleanSetting("DoCert", ConfigOptions.bCert)
            settings.SetBooleanSetting("DoCollection", ConfigOptions.bCollection)
            settings.SetBooleanSetting("DoCountry", ConfigOptions.bCountry)
            settings.SetBooleanSetting("DoDirector", ConfigOptions.bDirector)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier.Fanart)
            settings.SetBooleanSetting("DoFullCast", ConfigOptions.bFullCast)
            settings.SetBooleanSetting("DoFullCrews", ConfigOptions.bFullCrew)
            settings.SetBooleanSetting("DoGenres", ConfigOptions.bGenre)
            settings.SetBooleanSetting("DoMPAA", ConfigOptions.bMPAA)
            settings.SetBooleanSetting("DoMusic", ConfigOptions.bMusicBy)
            settings.SetBooleanSetting("DoOtherCrews", ConfigOptions.bOtherCrew)
            settings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier.Poster)
            settings.SetBooleanSetting("DoProducers", ConfigOptions.bProducers)
            settings.SetBooleanSetting("DoRating", ConfigOptions.bRating)
            settings.SetBooleanSetting("DoRelease", ConfigOptions.bRelease)
            settings.SetBooleanSetting("DoRuntime", ConfigOptions.bRuntime)
            settings.SetBooleanSetting("DoStudio", ConfigOptions.bStudio)
            settings.SetBooleanSetting("DoTagline", ConfigOptions.bTagline)
            settings.SetBooleanSetting("DoTitle", ConfigOptions.bTitle)
            settings.SetBooleanSetting("DoTop250", ConfigOptions.bTop250)
            settings.SetBooleanSetting("DoTrailer", ConfigOptions.bTrailer)
            settings.SetBooleanSetting("DoVotes", ConfigOptions.bVotes)
            settings.SetBooleanSetting("DoWriters", ConfigOptions.bWriters)
            settings.SetBooleanSetting("DoYear", ConfigOptions.bYear)
            settings.SetBooleanSetting("FallBackEn", _MySettings.FallBackEng)
            settings.SetBooleanSetting("FullCast", ConfigOptions.bFullCast)
            settings.SetBooleanSetting("FullCrew", ConfigOptions.bFullCrew)
            settings.SetBooleanSetting("GetAdultItems", _MySettings.GetAdultItems)
            settings.SetSetting("TMDBAPIKey", _setup.txtTMDBApiKey.Text)
            settings.SetSetting("TMDBLanguage", _MySettings.TMDBLanguage)
        End Using
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberMovieScraperModule_Data.SaveSetupScraper
        ConfigOptions.bCast = _setup.chkCast.Checked
        ConfigOptions.bCert = ConfigOptions.bMPAA
        ConfigOptions.bCleanPlotOutline = _setup.chkTMDBCleanPlotOutline.Checked
        ConfigOptions.bCollection = _setup.chkCollection.Checked
        ConfigOptions.bCountry = _setup.chkCountry.Checked
        ConfigOptions.bDirector = _setup.chkCrew.Checked
        ConfigOptions.bFullCast = _setup.chkCast.Checked
        ConfigOptions.bFullCrew = _setup.chkCrew.Checked
        ConfigOptions.bGenre = _setup.chkGenre.Checked
        ConfigOptions.bMPAA = _setup.chkMPAA.Checked
        ConfigOptions.bMusicBy = _setup.chkCrew.Checked
        ConfigOptions.bOtherCrew = _setup.chkCrew.Checked
        ConfigOptions.bOutline = _setup.chkPlot.Checked
        ConfigOptions.bPlot = _setup.chkPlot.Checked
        ConfigOptions.bProducers = _setup.chkCrew.Checked
        ConfigOptions.bRating = _setup.chkRating.Checked
        ConfigOptions.bRelease = _setup.chkRelease.Checked
        ConfigOptions.bRuntime = _setup.chkRuntime.Checked
        ConfigOptions.bStudio = _setup.chkStudio.Checked
        ConfigOptions.bTagline = _setup.chkTagline.Checked
        ConfigOptions.bTitle = _setup.chkTitle.Checked
        ConfigOptions.bTop250 = False
        ConfigOptions.bTrailer = _setup.chkTrailer.Checked
        ConfigOptions.bVotes = _setup.chkVotes.Checked
        ConfigOptions.bWriters = _setup.chkCrew.Checked
        ConfigOptions.bYear = _setup.chkYear.Checked
        _MySettings.FallBackEng = _setup.chkFallBackEng.Checked
        _MySettings.GetAdultItems = _setup.chkGetAdultItems.Checked
        _MySettings.TMDBLanguage = _setup.cbTMDBPrefLanguage.Text
        SaveSettings()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef sStudio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule_Data.GetMovieStudio
        Return Nothing
    End Function

    Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule_Data.Scraper
        Dim tTitle As String = String.Empty
        Dim OldTitle As String = DBMovie.Movie.Title
        Dim filterOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(Options, ConfigOptions)

        If IsNothing(_TMDBApi) Then
            logger.Error(Master.eLang.GetString(938, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message)
            Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
        Else
            If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                logger.Error(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString())
                Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If Master.GlobalScrapeMod.NFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
            If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
                _TMDBg.GetMovieInfo(DBMovie.Movie.ID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
                    DBMovie.Movie = _TMDBg.GetSearchMovieInfo(DBMovie.Movie.Title, DBMovie, ScrapeType, filterOptions)
                End If
                If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        ' why a scraper should initialize the DBMovie structure?
        ' Answer (DanCooper): If you want to CHANGE the movie. For this, all existing (incorrect) information must be deleted.
        If ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.GlobalScrapeMod.DoSearch _
         AndAlso ModulesManager.Instance.externalDataScrapersModules.OrderBy(Function(y) y.ScraperOrder).FirstOrDefault(Function(e) e.ProcessorModule.ScraperEnabled).AssemblyName = _AssemblyName Then
            DBMovie.Movie.IMDBID = String.Empty
            DBMovie.ClearBanner = True
            DBMovie.ClearClearArt = True
            DBMovie.ClearClearLogo = True
            DBMovie.ClearDiscArt = True
            DBMovie.ClearEFanarts = True
            DBMovie.ClearEThumbs = True
            DBMovie.ClearFanart = True
            DBMovie.ClearLandscape = True
            DBMovie.ClearPoster = True
            DBMovie.ClearTheme = True
            DBMovie.ClearTrailer = True
            DBMovie.BannerPath = String.Empty
            DBMovie.ClearArtPath = String.Empty
            DBMovie.ClearLogoPath = String.Empty
            DBMovie.DiscArtPath = String.Empty
            DBMovie.EFanartsPath = String.Empty
            DBMovie.EThumbsPath = String.Empty
            DBMovie.FanartPath = String.Empty
            DBMovie.LandscapePath = String.Empty
            DBMovie.NfoPath = String.Empty
            DBMovie.PosterPath = String.Empty
            DBMovie.SubPath = String.Empty
            DBMovie.ThemePath = String.Empty
            DBMovie.TrailerPath = String.Empty
            DBMovie.Movie.Clear()
        End If

        If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.UpdateAuto
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
            If ScrapeType = Enums.ScrapeType.SingleScrape Then

                'This is a workaround to remove the "TreeView" error on search results window. The problem is that the last search results are still existing in _TMDBg. 
                'I don't know another way to remove it. It works, It works so far without errors.
                'TODO: maybe find another solution.
                Me._TMDBg = New TMDBg.Scraper(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _TMDBApiA)

                Using dSearch As New dlgTMDBSearchResults(_MySettings, Me._TMDBg)
                    Dim tmpTitle As String = DBMovie.Movie.Title
                    Dim tmpYear As Integer = CInt(IIf(Not String.IsNullOrEmpty(DBMovie.Movie.Year), DBMovie.Movie.Year, 0))
                    If String.IsNullOrEmpty(tmpTitle) Then
                        If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
                            tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name, False)
                        ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
                            tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name, False)
                        Else
                            tmpTitle = StringUtils.FilterName(If(DBMovie.isSingle, Directory.GetParent(DBMovie.Filename).Name, Path.GetFileNameWithoutExtension(DBMovie.Filename)))
                        End If
                    End If
                    If dSearch.ShowDialog(tmpTitle, DBMovie.Filename, filterOptions, tmpYear) = Windows.Forms.DialogResult.OK Then
                        If Not String.IsNullOrEmpty(Master.tmpMovie.TMDBID) Then
                            ' if we changed the ID tipe we need to clear everything and rescrape
                            ' TODO: check TMDB if IMDB NullOrEmpty
                            If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) AndAlso Not (DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID) Then
                                Master.currMovie.ClearBanner = True
                                Master.currMovie.ClearClearArt = True
                                Master.currMovie.ClearClearLogo = True
                                Master.currMovie.ClearDiscArt = True
                                Master.currMovie.ClearEThumbs = True
                                Master.currMovie.ClearEFanarts = True
                                Master.currMovie.ClearFanart = True
                                Master.currMovie.ClearLandscape = True
                                Master.currMovie.ClearPoster = True
                                Master.currMovie.ClearTheme = True
                                Master.currMovie.ClearTrailer = True
                                Master.currMovie.BannerPath = String.Empty
                                Master.currMovie.ClearArtPath = String.Empty
                                Master.currMovie.ClearLogoPath = String.Empty
                                Master.currMovie.DiscArtPath = String.Empty
                                Master.currMovie.EFanartsPath = String.Empty
                                Master.currMovie.EThumbsPath = String.Empty
                                Master.currMovie.FanartPath = String.Empty
                                Master.currMovie.NfoPath = String.Empty
                                Master.currMovie.LandscapePath = String.Empty
                                Master.currMovie.PosterPath = String.Empty
                                Master.currMovie.SubPath = String.Empty
                                Master.currMovie.ThemePath = String.Empty
                                Master.currMovie.TrailerPath = String.Empty
                            End If
                            DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID
                            DBMovie.Movie.TMDBID = Master.tmpMovie.TMDBID
                        End If
                        If Not String.IsNullOrEmpty(DBMovie.Movie.TMDBID) AndAlso Master.GlobalScrapeMod.NFO Then
                            _TMDBg.GetMovieInfo(DBMovie.Movie.TMDBID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
                        End If
                    Else
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
            tTitle = StringUtils.FilterTokens(DBMovie.Movie.Title)
            If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = tTitle
            If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(DBMovie.Movie.Year) Then
                DBMovie.ListTitle = String.Format("{0} ({1})", tTitle, DBMovie.Movie.Year)
            Else
                DBMovie.ListTitle = tTitle
            End If
        Else
            If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
                DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name)
            ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
                DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name)
            Else
                If DBMovie.UseFolder AndAlso DBMovie.isSingle Then
                    DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(DBMovie.Filename).Name)
                Else
                    DBMovie.ListTitle = StringUtils.FilterName(Path.GetFileNameWithoutExtension(DBMovie.Filename))
                End If
            End If
            If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = DBMovie.ListTitle
        End If

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.EmberMovieScraperModule_Data.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"
        Dim FallBackEng As Boolean
        Dim GetAdultItems As Boolean
        Dim TMDBAPIKey As String
        Dim TMDBLanguage As String
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class