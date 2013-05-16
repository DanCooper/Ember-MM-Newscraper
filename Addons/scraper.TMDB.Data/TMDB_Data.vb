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
Imports EmberAPI
Imports RestSharp
Imports WatTmdb
Imports EmberScraperModule.TMDBg

Public Class TMDB_Data
    Implements Interfaces.EmberMovieScraperModule_Data


#Region "Fields"

    Public Shared ConfigOptions As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    Private TMDBId As String
    'Private IMDBid As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private _MySettings As New sMySettings
    Private _TMDBg As TMDBg.Scraper
    Private _Name As String = "TMDB_Data"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmTMDBInfoSettingsHolder
    Private _TMDBConf As V3.TmdbConfiguration
    Private _TMDBConfE As V3.TmdbConfiguration
    Private _TMDBApi As V3.Tmdb
    Private _TMDBApiE As V3.Tmdb

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
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberMovieScraperModule_Data.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmTMDBInfoSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.chkTitle.Checked = ConfigOptions.bTitle
        _setup.chkYear.Checked = ConfigOptions.bYear
        _setup.chkMPAA.Checked = ConfigOptions.bMPAA
        _setup.chkRelease.Checked = ConfigOptions.bRelease
        _setup.chkRuntime.Checked = ConfigOptions.bRuntime
        _setup.chkRating.Checked = ConfigOptions.bRating
        _setup.chkVotes.Checked = ConfigOptions.bVotes
        _setup.chkStudio.Checked = ConfigOptions.bStudio
        _setup.chkTagline.Checked = ConfigOptions.bTagline
        _setup.chkOutline.Checked = ConfigOptions.bOutline
        _setup.chkCast.Checked = ConfigOptions.bFullCast
        _setup.chkGenre.Checked = ConfigOptions.bGenre
        _setup.chkTrailer.Checked = ConfigOptions.bTrailer
        _setup.chkCountry.Checked = ConfigOptions.bCountry
        _setup.chkCrew.Checked = ConfigOptions.bFullCrew
        
        If String.IsNullOrEmpty(_MySettings.TMDBAPIKey) Then
            _MySettings.TMDBAPIKey = Master.eLang.GetString(122, "Get your API Key from www.themoviedb.org")
        End If
        _setup.txtTMDBApiKey.Text = _MySettings.TMDBAPIKey
        _setup.cbTMDBPrefLanguage.Text = _MySettings.TMDBLanguage
        _setup.chkFallBackEng.Checked = _MySettings.FallBackEng
        _setup.Lang = _setup.cbTMDBPrefLanguage.Text
        _setup.API = _setup.txtTMDBApiKey.Text

        _setup.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = Master.eLang.GetString(104, "TMDB")
        SPanel.Prefix = "TMDBMovieInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieData"
        SPanel.Type = Master.eLang.GetString(36, "Movies", True)
        SPanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return SPanel
    End Function

    Sub LoadSettings()
        ConfigOptions.bTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True)
        ConfigOptions.bYear = AdvancedSettings.GetBooleanSetting("DoYear", True)
        ConfigOptions.bMPAA = AdvancedSettings.GetBooleanSetting("DoMPAA", True)
        ConfigOptions.bRelease = AdvancedSettings.GetBooleanSetting("DoRelease", True)
        ConfigOptions.bRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True)
        ConfigOptions.bRating = AdvancedSettings.GetBooleanSetting("DoRating", True)
        ConfigOptions.bVotes = AdvancedSettings.GetBooleanSetting("DoVotes", True)
        ConfigOptions.bStudio = AdvancedSettings.GetBooleanSetting("DoStudio", True)
        ConfigOptions.bTagline = AdvancedSettings.GetBooleanSetting("DoTagline", True)
        ConfigOptions.bOutline = AdvancedSettings.GetBooleanSetting("DoOutline", True)
        ConfigOptions.bPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True)
        ConfigOptions.bCast = AdvancedSettings.GetBooleanSetting("DoCast", True)
        ConfigOptions.bDirector = AdvancedSettings.GetBooleanSetting("DoDirector", True)
        ConfigOptions.bWriters = AdvancedSettings.GetBooleanSetting("DoWriters", True)
        ConfigOptions.bProducers = AdvancedSettings.GetBooleanSetting("DoProducers", True)
        ConfigOptions.bGenre = AdvancedSettings.GetBooleanSetting("DoGenres", True)
        ConfigOptions.bTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True)
        ConfigOptions.bMusicBy = AdvancedSettings.GetBooleanSetting("DoMusic", True)
        ConfigOptions.bOtherCrew = AdvancedSettings.GetBooleanSetting("DoOtherCrews", True)
        ConfigOptions.bFullCast = AdvancedSettings.GetBooleanSetting("DoFullCast", True)
        ConfigOptions.bFullCrew = AdvancedSettings.GetBooleanSetting("DoFullCrews", True)
        ConfigOptions.bTop250 = AdvancedSettings.GetBooleanSetting("DoTop250", True)
        ConfigOptions.bCountry = AdvancedSettings.GetBooleanSetting("DoCountry", True)
        ConfigOptions.bCert = AdvancedSettings.GetBooleanSetting("DoCert", True)
        ConfigOptions.bFullCast = AdvancedSettings.GetBooleanSetting("FullCast", True)
        ConfigOptions.bFullCrew = AdvancedSettings.GetBooleanSetting("FullCrew", True)

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
        ConfigScrapeModifier.Trailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True)
    End Sub

    Sub SaveSettings()
        AdvancedSettings.SetBooleanSetting("DoFullCast", ConfigOptions.bFullCast)
        AdvancedSettings.SetBooleanSetting("DoFullCrews", ConfigOptions.bFullCrew)
        AdvancedSettings.SetBooleanSetting("DoTitle", ConfigOptions.bTitle)
        AdvancedSettings.SetBooleanSetting("DoYear", ConfigOptions.bYear)
        AdvancedSettings.SetBooleanSetting("DoMPAA", ConfigOptions.bMPAA)
        AdvancedSettings.SetBooleanSetting("DoRelease", ConfigOptions.bRelease)
        AdvancedSettings.SetBooleanSetting("DoRuntime", ConfigOptions.bRuntime)
        AdvancedSettings.SetBooleanSetting("DoRating", ConfigOptions.bRating)
        AdvancedSettings.SetBooleanSetting("DoVotes", ConfigOptions.bVotes)
        AdvancedSettings.SetBooleanSetting("DoStudio", ConfigOptions.bStudio)
        AdvancedSettings.SetBooleanSetting("DoTagline", ConfigOptions.bTagline)
        AdvancedSettings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
        AdvancedSettings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
        AdvancedSettings.SetBooleanSetting("DoCast", ConfigOptions.bCast)
        AdvancedSettings.SetBooleanSetting("DoDirector", ConfigOptions.bDirector)
        AdvancedSettings.SetBooleanSetting("DoWriters", ConfigOptions.bWriters)
        AdvancedSettings.SetBooleanSetting("DoProducers", ConfigOptions.bProducers)
        AdvancedSettings.SetBooleanSetting("DoGenres", ConfigOptions.bGenre)
        AdvancedSettings.SetBooleanSetting("DoTrailer", ConfigOptions.bTrailer)
        AdvancedSettings.SetBooleanSetting("DoMusic", ConfigOptions.bMusicBy)
        AdvancedSettings.SetBooleanSetting("DoOtherCrews", ConfigOptions.bOtherCrew)
        AdvancedSettings.SetBooleanSetting("DoCountry", ConfigOptions.bCountry)
        AdvancedSettings.SetBooleanSetting("DoTop250", ConfigOptions.bTop250)
        AdvancedSettings.SetBooleanSetting("DoCert", ConfigOptions.bCert)

        AdvancedSettings.SetBooleanSetting("FullCast", ConfigOptions.bFullCast)
        AdvancedSettings.SetBooleanSetting("FullCrew", ConfigOptions.bFullCrew)

        AdvancedSettings.SetBooleanSetting("DoPoster", ConfigScrapeModifier.Poster)
        AdvancedSettings.SetBooleanSetting("DoFanart", ConfigScrapeModifier.Fanart)

        AdvancedSettings.SetSetting("TMDBAPIKey", _MySettings.TMDBAPIKey)
        AdvancedSettings.SetBooleanSetting("FallBackEn", _MySettings.FallBackEng)
        AdvancedSettings.SetSetting("TMDBLanguage", _MySettings.TMDBLanguage)
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberMovieScraperModule_Data.SaveSetupScraper
        If Not String.IsNullOrEmpty(_setup.txtTMDBApiKey.Text) Then
            _MySettings.TMDBAPIKey = _setup.txtTMDBApiKey.Text
        Else
            _MySettings.TMDBAPIKey = Master.eLang.GetString(122, "Get your API Key from www.themoviedb.org")
        End If
        _MySettings.TMDBLanguage = _setup.cbTMDBPrefLanguage.Text
        _MySettings.FallBackEng = _setup.chkFallBackEng.Checked
        ConfigOptions.bTitle = _setup.chkTitle.Checked
        ConfigOptions.bYear = _setup.chkYear.Checked
        ConfigOptions.bMPAA = _setup.chkMPAA.Checked
        ConfigOptions.bRelease = _setup.chkRelease.Checked
        ConfigOptions.bRuntime = _setup.chkRuntime.Checked
        ConfigOptions.bRating = _setup.chkRating.Checked
        ConfigOptions.bVotes = _setup.chkVotes.Checked
        ConfigOptions.bStudio = _setup.chkStudio.Checked
        ConfigOptions.bTagline = _setup.chkTagline.Checked
        ConfigOptions.bOutline = _setup.chkOutline.Checked
        ConfigOptions.bPlot = False
        ConfigOptions.bCast = _setup.chkCast.Checked
        ConfigOptions.bDirector = _setup.chkCrew.Checked
        ConfigOptions.bWriters = _setup.chkCrew.Checked
        ConfigOptions.bProducers = _setup.chkCrew.Checked
        ConfigOptions.bGenre = _setup.chkGenre.Checked
        ConfigOptions.bTrailer = _setup.chkTrailer.Checked
        ConfigOptions.bMusicBy = _setup.chkCrew.Checked
        ConfigOptions.bOtherCrew = _setup.chkCrew.Checked
        ConfigOptions.bCountry = _setup.chkCountry.Checked
        ConfigOptions.bTop250 = False
        ConfigOptions.bFullCrew = _setup.chkCrew.Checked
        ConfigOptions.bFullCast = _setup.chkCast.Checked
        ConfigOptions.bCert = ConfigOptions.bMPAA
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
        'LoadSettings()
        ''TMDBg.IMDBURL = MySettings.IMDBURL
        ''TMDBg.UseOFDBTitle = MySettings.UseOFDBTitle
        ''TMDBg.UseOFDBOutline = MySettings.UseOFDBOutline
        ''TMDBg.UseOFDBPlot = MySettings.UseOFDBPlot
        ''TMDBg.UseOFDBGenre = MySettings.UseOFDBGenre
        Dim tTitle As String = String.Empty
        Dim OldTitle As String = DBMovie.Movie.Title

        If IsNothing(_TMDBApi) Then
            Master.eLog.WriteToErrorLog(Master.eLang.GetString(119, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message, "Error")
            Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
        Else
            If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
                Master.eLog.WriteToErrorLog(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString(), "Error")
                Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If Master.GlobalScrapeMod.NFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
            If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
                _TMDBg.GetMovieInfo(DBMovie.Movie.ID, DBMovie.Movie, Options.bFullCrew, Options.bFullCast, False, Options, False)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                DBMovie.Movie = _TMDBg.GetSearchMovieInfo(DBMovie.Movie.Title, DBMovie, ScrapeType, Options)
                If String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        'If ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.GlobalScrapeMod.DoSearch _
        ' AndAlso ModulesManager.Instance.externalDataScrapersModules.OrderBy(Function(y) y.ScraperOrder).FirstOrDefault(Function(e) e.ProcessorModule.ScraperEnabled).AssemblyName = _AssemblyName Then
        '	DBMovie.Movie.IMDBID = String.Empty
        '	DBMovie.ClearExtras = True
        '	DBMovie.PosterPath = String.Empty
        '	DBMovie.FanartPath = String.Empty
        '	DBMovie.TrailerPath = String.Empty
        '	DBMovie.ExtraPath = String.Empty
        '	DBMovie.SubPath = String.Empty
        '	DBMovie.NfoPath = String.Empty
        '	DBMovie.Movie.Clear()
        'End If
        If String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.UpdateAuto
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
            If ScrapeType = Enums.ScrapeType.SingleScrape Then
                Dim dSearch As New dlgTMDBSearchResults(_MySettings, Me._TMDBg)
                ''			dSearch.IMDBURL = MySettings.IMDBURL
                Dim tmpTitle As String = DBMovie.Movie.Title
                If String.IsNullOrEmpty(tmpTitle) Then
                    If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
                        tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name, False)
                    ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
                        tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name, False)
                    Else
                        tmpTitle = StringUtils.FilterName(If(DBMovie.isSingle, Directory.GetParent(DBMovie.Filename).Name, Path.GetFileNameWithoutExtension(DBMovie.Filename)))
                    End If
                End If
                Dim filterOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(Options, ConfigOptions)
                If dSearch.ShowDialog(tmpTitle, filterOptions) = Windows.Forms.DialogResult.OK Then
                    If Not String.IsNullOrEmpty(Master.tmpMovie.IMDBID) Then
                        ' if we changed the ID tipe we need to clear everything and rescrape
                        If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) AndAlso Not (DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID) Then
                            Master.currMovie.ClearExtras = True
                            Master.currMovie.PosterPath = String.Empty
                            Master.currMovie.FanartPath = String.Empty
                            Master.currMovie.TrailerPath = String.Empty
                            Master.currMovie.ExtraPath = String.Empty
                            Master.currMovie.SubPath = String.Empty
                            Master.currMovie.NfoPath = String.Empty
                        End If
                        DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID
                    End If
                    If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) AndAlso Master.GlobalScrapeMod.NFO Then
                        _TMDBg.GetMovieInfo(DBMovie.Movie.ID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
                    End If
                Else
                    Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                End If
            End If
        End If

        If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
            tTitle = StringUtils.FilterTokens(DBMovie.Movie.Title)
            If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = tTitle
            If Master.eSettings.DisplayYear AndAlso Not String.IsNullOrEmpty(DBMovie.Movie.Year) Then
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
        Dim TMDBAPIKey As String
        Dim TMDBLanguage As String
        Dim FallBackEng As Boolean
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class