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
Imports NLog

''' <summary>
''' Native Scraper
''' </summary>
''' <remarks></remarks>
Public Class IMDB_Data
    Implements Interfaces.ScraperModule_Data_Movie


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared ConfigOptions As New Structures.ScrapeOptions_Movie
    Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String
    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private _MySettings As New sMySettings
    Private _scraper As New IMDB.Scraper
    Private _Name As String = "IMDB_Data"
    Private _PostScraperEnabled As Boolean = False
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmIMDBInfoSettingsHolder

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

    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        If (DBMovie.Movie Is Nothing OrElse String.IsNullOrEmpty(DBMovie.Movie.IMDBID)) Then
            logger.Error("Attempting to get studio for undefined movie")
            Return New Interfaces.ModuleResult With {.Cancelled = True}    'DEKKER500 VERIFY PLEASE
        End If
        studio = _scraper.GetMovieStudios(DBMovie.Movie.IMDBID)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDBID
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
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
        _setup = New frmIMDBInfoSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.chkTitle.Checked = ConfigOptions.bTitle
        _setup.chkYear.Checked = ConfigOptions.bYear
        _setup.chkRelease.Checked = ConfigOptions.bRelease
        _setup.chkRuntime.Checked = ConfigOptions.bRuntime
        _setup.chkRating.Checked = ConfigOptions.bRating
        _setup.chkVotes.Checked = ConfigOptions.bVotes
        _setup.chkStudio.Checked = ConfigOptions.bStudio
        _setup.chkTagline.Checked = ConfigOptions.bTagline
        _setup.chkOutline.Checked = ConfigOptions.bOutline
        _setup.chkPlot.Checked = ConfigOptions.bPlot
        _setup.chkCast.Checked = ConfigOptions.bCast
        _setup.chkDirector.Checked = ConfigOptions.bDirector
        _setup.chkWriters.Checked = ConfigOptions.bWriters
        _setup.chkProducers.Checked = ConfigOptions.bProducers
        _setup.chkGenre.Checked = ConfigOptions.bGenre
        _setup.chkTrailer.Checked = ConfigOptions.bTrailer
        _setup.chkMusicBy.Checked = ConfigOptions.bMusicBy
        _setup.chkCrew.Checked = ConfigOptions.bOtherCrew
        _setup.chkCountry.Checked = ConfigOptions.bCountry
        _setup.chkTop250.Checked = ConfigOptions.bTop250
        _setup.chkCertification.Checked = ConfigOptions.bCert
        _setup.chkFullCrew.Checked = ConfigOptions.bFullCrew
        _setup.chkFallBackworldwide.Checked = _MySettings.FallBackWorldwide
        _setup.cbForceTitleLanguage.Text = _MySettings.ForceTitleLanguage
        _setup.chkPartialTitles.Checked = _MySettings.SearchPartialTitles
        _setup.chkPopularTitles.Checked = _MySettings.SearchPopularTitles
        _setup.chkTvTitles.Checked = _MySettings.SearchTvTitles
        _setup.chkVideoTitles.Checked = _MySettings.SearchVideoTitles
        _setup.chkCountryAbbreviation.Checked = _MySettings.CountryAbbreviation

        _setup.orderChanged()
        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = Master.eLang.GetString(885, "IMDB")
        SPanel.Prefix = "IMDBMovieInfo_"
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
        ConfigOptions.bTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True)
        ConfigOptions.bYear = clsAdvancedSettings.GetBooleanSetting("DoYear", True)
        ConfigOptions.bMPAA = clsAdvancedSettings.GetBooleanSetting("DoMPAA", True)
        ConfigOptions.bRelease = clsAdvancedSettings.GetBooleanSetting("DoRelease", True)
        ConfigOptions.bRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True)
        ConfigOptions.bRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True)
        ConfigOptions.bVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True)
        ConfigOptions.bStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True)
        ConfigOptions.bTagline = clsAdvancedSettings.GetBooleanSetting("DoTagline", True)
        ConfigOptions.bOutline = clsAdvancedSettings.GetBooleanSetting("DoOutline", True)
        ConfigOptions.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True)
        ConfigOptions.bCast = clsAdvancedSettings.GetBooleanSetting("DoCast", True)
        ConfigOptions.bDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True)
        ConfigOptions.bWriters = clsAdvancedSettings.GetBooleanSetting("DoWriters", True)
        ConfigOptions.bProducers = clsAdvancedSettings.GetBooleanSetting("DoProducers", True)
        ConfigOptions.bGenre = clsAdvancedSettings.GetBooleanSetting("DoGenres", True)
        ConfigOptions.bTrailer = clsAdvancedSettings.GetBooleanSetting("DoTrailer", True)
        ConfigOptions.bMusicBy = clsAdvancedSettings.GetBooleanSetting("DoMusic", True)
        ConfigOptions.bOtherCrew = clsAdvancedSettings.GetBooleanSetting("DoOtherCrews", True)
        ConfigOptions.bFullCrew = clsAdvancedSettings.GetBooleanSetting("DoFullCrews", True)
        ConfigOptions.bTop250 = clsAdvancedSettings.GetBooleanSetting("DoTop250", True)
        ConfigOptions.bCountry = clsAdvancedSettings.GetBooleanSetting("DoCountry", True)
        ConfigOptions.bCert = clsAdvancedSettings.GetBooleanSetting("DoCert", True)
        ConfigOptions.bFullCrew = clsAdvancedSettings.GetBooleanSetting("FullCrew", True)

        ConfigScrapeModifier.DoSearch = True
        ConfigScrapeModifier.Meta = True
        ConfigScrapeModifier.NFO = True

        _MySettings.FallBackWorldwide = clsAdvancedSettings.GetBooleanSetting("FallBackWorldwide", False)
        _MySettings.ForceTitleLanguage = clsAdvancedSettings.GetSetting("ForceTitleLanguage", "")
        _MySettings.SearchPartialTitles = clsAdvancedSettings.GetBooleanSetting("SearchPartialTitles", True)
        _MySettings.SearchPopularTitles = clsAdvancedSettings.GetBooleanSetting("SearchPopularTitles", True)
        _MySettings.SearchTvTitles = clsAdvancedSettings.GetBooleanSetting("SearchTvTitles", False)
        _MySettings.SearchVideoTitles = clsAdvancedSettings.GetBooleanSetting("SearchVideoTitles", False)
        _MySettings.CountryAbbreviation = clsAdvancedSettings.GetBooleanSetting("CountryAbbreviation", False)

    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoFullCrews", ConfigOptions.bFullCrew)
            settings.SetBooleanSetting("DoTitle", ConfigOptions.bTitle)
            settings.SetBooleanSetting("DoYear", ConfigOptions.bYear)
            settings.SetBooleanSetting("DoMPAA", ConfigOptions.bMPAA)
            settings.SetBooleanSetting("DoRelease", ConfigOptions.bRelease)
            settings.SetBooleanSetting("DoRuntime", ConfigOptions.bRuntime)
            settings.SetBooleanSetting("DoRating", ConfigOptions.bRating)
            settings.SetBooleanSetting("DoVotes", ConfigOptions.bVotes)
            settings.SetBooleanSetting("DoStudio", ConfigOptions.bStudio)
            settings.SetBooleanSetting("DoTagline", ConfigOptions.bTagline)
            settings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
            settings.SetBooleanSetting("DoCast", ConfigOptions.bCast)
            settings.SetBooleanSetting("DoDirector", ConfigOptions.bDirector)
            settings.SetBooleanSetting("DoWriters", ConfigOptions.bWriters)
            settings.SetBooleanSetting("DoProducers", ConfigOptions.bProducers)
            settings.SetBooleanSetting("DoGenres", ConfigOptions.bGenre)
            settings.SetBooleanSetting("DoTrailer", ConfigOptions.bTrailer)
            settings.SetBooleanSetting("DoMusic", ConfigOptions.bMusicBy)
            settings.SetBooleanSetting("DoOtherCrews", ConfigOptions.bOtherCrew)
            settings.SetBooleanSetting("DoCountry", ConfigOptions.bCountry)
            settings.SetBooleanSetting("DoTop250", ConfigOptions.bTop250)
            settings.SetBooleanSetting("DoCert", ConfigOptions.bCert)
            settings.SetBooleanSetting("FullCrew", ConfigOptions.bFullCrew)

            settings.SetBooleanSetting("FallBackWorldwide", _MySettings.FallBackWorldwide)
            settings.SetSetting("ForceTitleLanguage", _MySettings.ForceTitleLanguage)
            settings.SetBooleanSetting("SearchPartialTitles", _MySettings.SearchPartialTitles)
            settings.SetBooleanSetting("SearchPopularTitles", _MySettings.SearchPopularTitles)
            settings.SetBooleanSetting("SearchTvTitles", _MySettings.SearchTvTitles)
            settings.SetBooleanSetting("SearchVideoTitles", _MySettings.SearchVideoTitles)
            settings.SetBooleanSetting("CountryAbbreviation", _MySettings.CountryAbbreviation)
        End Using
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        ConfigOptions.bTitle = _setup.chkTitle.Checked
        ConfigOptions.bYear = _setup.chkYear.Checked
        ConfigOptions.bMPAA = _setup.chkCertification.Checked
        ConfigOptions.bRelease = _setup.chkRelease.Checked
        ConfigOptions.bRuntime = _setup.chkRuntime.Checked
        ConfigOptions.bRating = _setup.chkRating.Checked
        ConfigOptions.bVotes = _setup.chkVotes.Checked
        ConfigOptions.bStudio = _setup.chkStudio.Checked
        ConfigOptions.bTagline = _setup.chkTagline.Checked
        ConfigOptions.bOutline = _setup.chkOutline.Checked
        ConfigOptions.bPlot = _setup.chkPlot.Checked
        ConfigOptions.bCast = _setup.chkCast.Checked
        ConfigOptions.bDirector = _setup.chkDirector.Checked
        ConfigOptions.bWriters = _setup.chkWriters.Checked
        ConfigOptions.bProducers = _setup.chkProducers.Checked
        ConfigOptions.bGenre = _setup.chkGenre.Checked
        ConfigOptions.bTrailer = _setup.chkTrailer.Checked
        ConfigOptions.bMusicBy = _setup.chkMusicBy.Checked
        ConfigOptions.bOtherCrew = _setup.chkCrew.Checked
        ConfigOptions.bCountry = _setup.chkCountry.Checked
        ConfigOptions.bTop250 = _setup.chkTop250.Checked
        ConfigOptions.bCert = _setup.chkCertification.Checked
        ConfigOptions.bFullCrew = _setup.chkFullCrew.Checked

        _MySettings.FallBackWorldwide = _setup.chkFallBackworldwide.Checked
        _MySettings.ForceTitleLanguage = _setup.cbForceTitleLanguage.Text
        _MySettings.SearchPartialTitles = _setup.chkPartialTitles.Checked
        _MySettings.SearchPopularTitles = _setup.chkPopularTitles.Checked
        _MySettings.SearchTvTitles = _setup.chkTvTitles.Checked
        _MySettings.SearchVideoTitles = _setup.chkVideoTitles.Checked
        _MySettings.CountryAbbreviation = _setup.chkCountryAbbreviation.Checked

        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from TMDB
    ''' </summary>
    ''' <param name="oDBMovie">Movie to be scraped. oDBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper(ByRef oDBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.Scraper
        logger.Trace("Started IMDB Scraper")


        'LoadSettings()
        Dim tTitle As String = String.Empty
        Dim OldTitle As String = oDBMovie.Movie.Title
        Dim filterOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(Options, ConfigOptions)

        If Master.GlobalScrapeMod.NFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
            If Not String.IsNullOrEmpty(oDBMovie.Movie.IMDBID) Then
                'IMDB-ID already available -> scrape and save data into an empty movie container (nMovie)
                _scraper.GetMovieInfo(oDBMovie.Movie.IMDBID, nMovie, filterOptions.bFullCrew, False, filterOptions, False, _MySettings.FallBackWorldwide, _MySettings.ForceTitleLanguage, _MySettings.CountryAbbreviation)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no IMDB-ID for movie --> search first!
                oDBMovie.Movie = _scraper.GetSearchMovieInfo(oDBMovie.Movie.Title, oDBMovie, nMovie, ScrapeType, filterOptions, filterOptions.bFullCrew, _MySettings.FallBackWorldwide, _MySettings.ForceTitleLanguage, _MySettings.CountryAbbreviation)
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(oDBMovie.Movie.IMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If


        ' why a scraper should initialize the DBMovie structure?
        ' Answer (DanCooper): If you want to CHANGE the movie. For this, all existing (incorrect) information must be deleted.
        If ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.GlobalScrapeMod.DoSearch _
            AndAlso ModulesManager.Instance.externalScrapersModules_Data_Movie.OrderBy(Function(y) y.ModuleOrder).FirstOrDefault(Function(e) e.ProcessorModule.ScraperEnabled).AssemblyName = _AssemblyName Then
            oDBMovie.Movie.IMDBID = String.Empty
            oDBMovie.RemoveActorThumbs = True
            oDBMovie.RemoveBanner = True
            oDBMovie.RemoveClearArt = True
            oDBMovie.RemoveClearLogo = True
            oDBMovie.RemoveDiscArt = True
            oDBMovie.RemoveEThumbs = True
            oDBMovie.RemoveEFanarts = True
            oDBMovie.RemoveFanart = True
            oDBMovie.RemoveLandscape = True
            oDBMovie.RemovePoster = True
            oDBMovie.RemoveTheme = True
            oDBMovie.RemoveTrailer = True
            oDBMovie.BannerPath = String.Empty
            oDBMovie.ClearArtPath = String.Empty
            oDBMovie.ClearLogoPath = String.Empty
            oDBMovie.DiscArtPath = String.Empty
            oDBMovie.EFanartsPath = String.Empty
            oDBMovie.EThumbsPath = String.Empty
            oDBMovie.FanartPath = String.Empty
            oDBMovie.NfoPath = String.Empty
            oDBMovie.LandscapePath = String.Empty
            oDBMovie.PosterPath = String.Empty
            oDBMovie.SubPath = String.Empty
            oDBMovie.ThemePath = String.Empty
            oDBMovie.TrailerPath = String.Empty
            oDBMovie.Movie.Clear()
        End If

        If String.IsNullOrEmpty(oDBMovie.Movie.IMDBID) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.MissAuto
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
            If ScrapeType = Enums.ScrapeType.SingleScrape Then
                Using dSearch As New dlgIMDBSearchResults
                    Dim tmpTitle As String = oDBMovie.Movie.Title
                    If String.IsNullOrEmpty(tmpTitle) Then
                        If FileUtils.Common.isVideoTS(oDBMovie.Filename) Then
                            tmpTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(oDBMovie.Filename).FullName).Name, False)
                        ElseIf FileUtils.Common.isBDRip(oDBMovie.Filename) Then
                            tmpTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(Directory.GetParent(oDBMovie.Filename).FullName).FullName).Name, False)
                        Else
                            tmpTitle = StringUtils.FilterName_Movie(If(oDBMovie.IsSingle, Directory.GetParent(oDBMovie.Filename).Name, Path.GetFileNameWithoutExtension(oDBMovie.Filename)))
                        End If
                    End If
                    If dSearch.ShowDialog(tmpTitle, oDBMovie.Filename, filterOptions) = Windows.Forms.DialogResult.OK Then
                        If Not String.IsNullOrEmpty(Master.tmpMovie.IMDBID) Then
                            ' if we changed the ID tipe we need to clear everything and rescrape
                            If Not String.IsNullOrEmpty(oDBMovie.Movie.IMDBID) AndAlso Not (oDBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID) Then
                                Master.currMovie.RemoveActorThumbs = True
                                Master.currMovie.RemoveBanner = True
                                Master.currMovie.RemoveClearArt = True
                                Master.currMovie.RemoveClearLogo = True
                                Master.currMovie.RemoveDiscArt = True
                                Master.currMovie.RemoveEThumbs = True
                                Master.currMovie.RemoveEFanarts = True
                                Master.currMovie.RemoveFanart = True
                                Master.currMovie.RemoveLandscape = True
                                Master.currMovie.RemovePoster = True
                                Master.currMovie.RemoveTheme = True
                                Master.currMovie.RemoveTrailer = True
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
                            oDBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID
                        End If
                        If Not String.IsNullOrEmpty(oDBMovie.Movie.IMDBID) AndAlso Master.GlobalScrapeMod.NFO Then
                            'IMDB-ID available -> scrape and save data into an empty movie container (nMovie)
                            _scraper.GetMovieInfo(oDBMovie.Movie.IMDBID, nMovie, filterOptions.bFullCrew, False, filterOptions, False, _MySettings.FallBackWorldwide, _MySettings.ForceTitleLanguage, _MySettings.CountryAbbreviation)
                            oDBMovie.Movie.OriginalTitle = nMovie.OriginalTitle
                            oDBMovie.Movie.Title = nMovie.Title
                            oDBMovie.Movie.ID = nMovie.ID
                        End If
                    Else
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        logger.Trace("Finished IMDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"
        Dim FallBackWorldwide As Boolean
        Dim ForceTitleLanguage As String
        Dim SearchPartialTitles As Boolean
        Dim SearchPopularTitles As Boolean
        Dim SearchTvTitles As Boolean
        Dim SearchVideoTitles As Boolean
        Dim CountryAbbreviation As Boolean
#End Region 'Fields

    End Structure

#End Region 'Nested Types


End Class