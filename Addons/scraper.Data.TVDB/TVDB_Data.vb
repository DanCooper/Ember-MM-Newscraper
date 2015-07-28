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

Public Class TVDB_Data
    Implements Interfaces.ScraperModule_Data_TV


#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared ConfigOptions As New Structures.ScrapeOptions_TV
    Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private strPrivateAPIKey As String = String.Empty
    Private _MySettings As New sMySettings
    Private _Name As String = "TVDB_Data"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder
    Private _TVDBMirror As String

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.ScraperModule_Data_TV.ModuleSettingsChanged
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_TV.ScraperEvent
    Public Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Data_TV.ScraperSetupChanged
    Public Event SetupNeedsRestart() Implements Interfaces.ScraperModule_Data_TV.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Data_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Data_TV.ModuleVersion
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.ScraperModule_Data_TV.ScraperEnabled
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

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent ScraperSetupChanged(String.Concat(Me._Name, "_TV"), state, difforder)
    End Sub

    Sub Init_Movie(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_TV.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.API = _setup.txtApiKey.Text
        _setup.chkEnabled.Checked = _ScraperEnabled
        _setup.txtTVDBMirror.Text = _TVDBMirror
        _setup.chkScraperEpActors.Checked = ConfigOptions.bEpActors
        _setup.chkScraperEpAired.Checked = ConfigOptions.bEpAired
        _setup.chkScraperEpCredits.Checked = ConfigOptions.bEpCredits
        _setup.chkScraperEpDirector.Checked = ConfigOptions.bEpDirector
        _setup.chkScraperEpGuestStars.Checked = ConfigOptions.bEpGuestStars
        _setup.chkScraperEpPlot.Checked = ConfigOptions.bEpPlot
        _setup.chkScraperEpRating.Checked = ConfigOptions.bEpRating
        _setup.chkScraperEpTitle.Checked = ConfigOptions.bEpTitle
        _setup.chkScraperEpVotes.Checked = ConfigOptions.bEpVotes
        _setup.chkScraperShowActors.Checked = ConfigOptions.bShowActors
        _setup.chkScraperShowEpisodeGuide.Checked = ConfigOptions.bShowEpisodeGuide
        _setup.chkScraperShowGenre.Checked = ConfigOptions.bShowGenre
        _setup.chkScraperShowMPAA.Checked = ConfigOptions.bShowMPAA
        _setup.chkScraperShowPlot.Checked = ConfigOptions.bShowPlot
        _setup.chkScraperShowPremiered.Checked = ConfigOptions.bShowPremiered
        _setup.chkScraperShowRating.Checked = ConfigOptions.bShowRating
        _setup.chkScraperShowRuntime.Checked = ConfigOptions.bShowRuntime
        _setup.chkScraperShowStatus.Checked = ConfigOptions.bShowStatus
        _setup.chkScraperShowStudio.Checked = ConfigOptions.bShowStudio
        _setup.chkScraperShowTitle.Checked = ConfigOptions.bShowTitle
        _setup.chkScraperShowVotes.Checked = ConfigOptions.bShowVotes

        If Not String.IsNullOrEmpty(strPrivateAPIKey) Then
            _setup.btnUnlockAPI.Text = Master.eLang.GetString(443, "Use embedded API Key")
            _setup.lblEMMAPI.Visible = False
            _setup.txtApiKey.Enabled = True
        End If

        _setup.orderChanged()

        SPanel.Name = String.Concat(Me._Name, "_TV")
        SPanel.Text = "TVDB"
        SPanel.Prefix = "TVDBTVInfo_"
        SPanel.Order = 110
        SPanel.Parent = "pnlTVData"
        SPanel.Type = Master.eLang.GetString(653, "TV Shows")
        SPanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        SPanel.Panel = _setup.pnlSettings

        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return SPanel
    End Function

    Sub LoadSettings()
        ConfigOptions.bEpActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bEpAired = clsAdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bEpCredits = clsAdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bEpDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bEpGuestStars = clsAdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bEpPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bEpRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bEpTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bEpVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.Content_Type.TVEpisode)
        ConfigOptions.bShowActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowEpisodeGuide = clsAdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowGenre = clsAdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowMPAA = clsAdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowPremiered = clsAdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowStatus = clsAdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.Content_Type.TVShow)
        ConfigOptions.bShowVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.Content_Type.TVShow)

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "")
        _MySettings.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "353783CE455412FD", strPrivateAPIKey)
        ConfigScrapeModifier.DoSearch = True
        ConfigScrapeModifier.EpisodeMeta = True
        ConfigScrapeModifier.MainNFO = True
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoActors", ConfigOptions.bEpActors, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoAired", ConfigOptions.bEpAired, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoCredits", ConfigOptions.bEpCredits, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoDirector", ConfigOptions.bEpDirector, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", ConfigOptions.bEpGuestStars, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bEpPlot, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoRating", ConfigOptions.bShowRating, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoTitle", ConfigOptions.bEpTitle, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoVotes", ConfigOptions.bEpVotes, , , Enums.Content_Type.TVEpisode)
            settings.SetBooleanSetting("DoActors", ConfigOptions.bShowActors, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoEpisodeGuide", ConfigOptions.bShowEpisodeGuide, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoGenre", ConfigOptions.bShowGenre, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoMPAA", ConfigOptions.bShowMPAA, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bShowPlot, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoPremiered", ConfigOptions.bShowPremiered, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoRating", ConfigOptions.bShowRating, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoStatus", ConfigOptions.bShowStatus, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoStudio", ConfigOptions.bShowStudio, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoTitle", ConfigOptions.bShowTitle, , , Enums.Content_Type.TVShow)
            settings.SetBooleanSetting("DoVotes", ConfigOptions.bShowVotes, , , Enums.Content_Type.TVShow)
            settings.SetSetting("APIKey", _setup.txtApiKey.Text)
        End Using
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_TV.SaveSetupScraper
        ConfigOptions.bEpActors = _setup.chkScraperEpActors.Checked
        ConfigOptions.bEpAired = _setup.chkScraperEpAired.Checked
        ConfigOptions.bEpCredits = _setup.chkScraperEpCredits.Checked
        ConfigOptions.bEpDirector = _setup.chkScraperEpDirector.Checked
        ConfigOptions.bEpGuestStars = _setup.chkScraperEpGuestStars.Checked
        ConfigOptions.bEpPlot = _setup.chkScraperEpPlot.Checked
        ConfigOptions.bEpRating = _setup.chkScraperEpRating.Checked
        ConfigOptions.bEpTitle = _setup.chkScraperEpTitle.Checked
        ConfigOptions.bEpVotes = _setup.chkScraperEpVotes.Checked
        ConfigOptions.bShowActors = _setup.chkScraperShowActors.Checked
        ConfigOptions.bShowEpisodeGuide = _setup.chkScraperShowEpisodeGuide.Checked
        ConfigOptions.bShowGenre = _setup.chkScraperShowGenre.Checked
        ConfigOptions.bShowMPAA = _setup.chkScraperShowMPAA.Checked
        ConfigOptions.bShowPlot = _setup.chkScraperShowPlot.Checked
        ConfigOptions.bShowPremiered = _setup.chkScraperShowPremiered.Checked
        ConfigOptions.bShowRating = _setup.chkScraperShowRating.Checked
        ConfigOptions.bShowRuntime = _setup.chkScraperShowRuntime.Checked
        ConfigOptions.bShowStatus = _setup.chkScraperShowStatus.Checked
        ConfigOptions.bShowStudio = _setup.chkScraperShowStudio.Checked
        ConfigOptions.bShowTitle = _setup.chkScraperShowTitle.Checked
        ConfigOptions.bShowVotes = _setup.chkScraperShowVotes.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub
    ''' <summary>
    '''  Scrape TVShowDetails from TVDB
    ''' </summary>
    ''' <param name="oDBTV">TV Show to be scraped. DBTV as ByRef to use existing data for identifing tv show and to fill with IMDB/TVDB ID for next scraper</param>
    ''' <param name="nShow">New scraped TV Show data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper(ByRef oDBTV As Structures.DBTV, ByRef nShow As MediaContainers.TVShow, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV, ByRef Options As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVShow
        logger.Trace("Started TVDB Scraper")

        LoadSettings()

        Dim Settings As TVDBs.Scraper.MySettings
        Settings.ApiKey = _MySettings.APIKey
        Settings.Language = oDBTV.Language

        Dim _scraper As New TVDBs.Scraper(Settings)
        Dim filterOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions)

        If ScrapeModifier.MainNFO AndAlso Not ScrapeModifier.DoSearch Then
            If Not String.IsNullOrEmpty(oDBTV.TVShow.TVDB) Then
                'TVDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                _scraper.GetTVShowInfo(oDBTV.TVShow.TVDB, nShow, False, filterOptions, False, ScrapeModifier.withEpisodes)
            ElseIf Not ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If Not String.IsNullOrEmpty(oDBTV.TVShow.Title) Then
                    _scraper.GetSearchTVShowInfo(oDBTV.TVShow.Title, oDBTV, nShow, ScrapeType, filterOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nShow.TVDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nShow.TVDB) Then
            Select Case ScrapeType
                Case Enums.ScrapeType_Movie_MovieSet_TV.FilterAuto, Enums.ScrapeType_Movie_MovieSet_TV.FullAuto, Enums.ScrapeType_Movie_MovieSet_TV.MarkAuto, Enums.ScrapeType_Movie_MovieSet_TV.NewAuto, Enums.ScrapeType_Movie_MovieSet_TV.MissAuto
                    nShow = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape OrElse ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleAuto Then
            If String.IsNullOrEmpty(oDBTV.TVShow.TVDB) Then
                Using dSearch As New dlgTVDBSearchResults(Settings, _scraper)
                    If dSearch.ShowDialog(nShow, oDBTV.TVShow.Title, oDBTV.ShowPath, filterOptions) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetTVShowInfo(nShow.TVDB, nShow, False, filterOptions, False, ScrapeModifier.withEpisodes)
                        'if a tvshow is found, set DoSearch back to "false" for following scrapers
                        ScrapeModifier.DoSearch = False
                    Else
                        nShow = Nothing
                        Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
                    End If
                End Using
            End If
        End If

        'set new informations for following scrapers
        If Not String.IsNullOrEmpty(nShow.Title) Then
            oDBTV.TVShow.Title = nShow.Title
        End If
        If Not String.IsNullOrEmpty(nShow.TVDB) Then
            oDBTV.TVShow.TVDB = nShow.TVDB
        End If
        If Not String.IsNullOrEmpty(nShow.IMDB) Then
            oDBTV.TVShow.IMDB = nShow.IMDB
        End If

        logger.Trace("Finished TVDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function Scraper_TVEpisode(ByRef oDBTVEpisode As Structures.DBTV, ByRef nEpisode As MediaContainers.EpisodeDetails, ByVal Options As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVEpisode
        logger.Trace("Started TVDB Scraper")

        LoadSettings()

        Dim Settings As TVDBs.Scraper.MySettings
        Settings.ApiKey = _MySettings.APIKey
        Settings.Language = oDBTVEpisode.Language

        Dim _scraper As New TVDBs.Scraper(Settings)
        Dim filterOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions)

        If Not String.IsNullOrEmpty(oDBTVEpisode.TVShow.TVDB) Then
            If Not oDBTVEpisode.TVEp.Episode = -1 AndAlso Not oDBTVEpisode.TVEp.Season = -1 Then
                nEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBTVEpisode.TVShow.TVDB), oDBTVEpisode.TVEp.Season, oDBTVEpisode.TVEp.Episode, filterOptions)
            ElseIf Not String.IsNullOrEmpty(oDBTVEpisode.TVEp.Aired) Then
                nEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBTVEpisode.TVShow.TVDB), oDBTVEpisode.TVEp.Aired, filterOptions)
            Else
                nEpisode = Nothing
            End If
        End If

        logger.Trace("Finished TVDB Scraper")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.ScraperModule_Data_TV.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"

        Dim APIKey As String
        Dim Language As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class