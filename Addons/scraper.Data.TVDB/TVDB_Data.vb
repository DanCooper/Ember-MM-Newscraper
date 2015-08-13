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

    Private _SpecialSettings As New SpecialSettings
    Private _Name As String = "TVDB_Data"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder
    Private strPrivateAPIKey As String = String.Empty

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.ScraperModule_Data_TV.ModuleSettingsChanged
    Public Event ScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Data_TV.ScraperEvent
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

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Data_TV.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Data_TV.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _ScraperEnabled
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
        ConfigOptions.bEpActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bEpAired = clsAdvancedSettings.GetBooleanSetting("DoAired", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bEpCredits = clsAdvancedSettings.GetBooleanSetting("DoCredits", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bEpDirector = clsAdvancedSettings.GetBooleanSetting("DoDirector", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bEpGuestStars = clsAdvancedSettings.GetBooleanSetting("DoGuestStars", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bEpPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bEpRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bEpTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bEpVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.ContentType.TVEpisode)
        ConfigOptions.bShowActors = clsAdvancedSettings.GetBooleanSetting("DoActors", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowEpisodeGuide = clsAdvancedSettings.GetBooleanSetting("DoEpisodeGuide", False, , Enums.ContentType.TVShow)
        ConfigOptions.bShowGenre = clsAdvancedSettings.GetBooleanSetting("DoGenre", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowMPAA = clsAdvancedSettings.GetBooleanSetting("DoMPAA", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowPremiered = clsAdvancedSettings.GetBooleanSetting("DoPremiered", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowRating = clsAdvancedSettings.GetBooleanSetting("DoRating", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowRuntime = clsAdvancedSettings.GetBooleanSetting("DoRuntime", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowStatus = clsAdvancedSettings.GetBooleanSetting("DoStatus", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowStudio = clsAdvancedSettings.GetBooleanSetting("DoStudio", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True, , Enums.ContentType.TVShow)
        ConfigOptions.bShowVotes = clsAdvancedSettings.GetBooleanSetting("DoVotes", True, , Enums.ContentType.TVShow)

        strPrivateAPIKey = clsAdvancedSettings.GetSetting("APIKey", "")
        _SpecialSettings.APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "353783CE455412FD", strPrivateAPIKey)
        ConfigScrapeModifier.DoSearch = True
        ConfigScrapeModifier.EpisodeMeta = True
        ConfigScrapeModifier.MainNFO = True
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoActors", ConfigOptions.bEpActors, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoAired", ConfigOptions.bEpAired, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoCredits", ConfigOptions.bEpCredits, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoDirector", ConfigOptions.bEpDirector, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoGuestStars", ConfigOptions.bEpGuestStars, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bEpPlot, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoRating", ConfigOptions.bShowRating, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoTitle", ConfigOptions.bEpTitle, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoVotes", ConfigOptions.bEpVotes, , , Enums.ContentType.TVEpisode)
            settings.SetBooleanSetting("DoActors", ConfigOptions.bShowActors, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoEpisodeGuide", ConfigOptions.bShowEpisodeGuide, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoGenre", ConfigOptions.bShowGenre, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoMPAA", ConfigOptions.bShowMPAA, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bShowPlot, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoPremiered", ConfigOptions.bShowPremiered, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoRating", ConfigOptions.bShowRating, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStatus", ConfigOptions.bShowStatus, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoStudio", ConfigOptions.bShowStudio, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoTitle", ConfigOptions.bShowTitle, , , Enums.ContentType.TVShow)
            settings.SetBooleanSetting("DoVotes", ConfigOptions.bShowVotes, , , Enums.ContentType.TVShow)
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
    ''' <param name="ScrapeOptions">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Database.DBElement Object (nMovie) which contains the scraped data</returns>
    ''' <remarks></remarks>
    Function Scraper(ByRef oDBTV As Database.DBElement, ByRef nShow As MediaContainers.TVShow, ByRef ScrapeModifier As Structures.ScrapeModifier, ByRef ScrapeType As Enums.ScrapeType, ByRef ScrapeOptions As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVShow
        logger.Trace("Started TVDB Scraper")

        LoadSettings()

        Dim Settings As New SpecialSettings
        Settings.APIKey = _SpecialSettings.APIKey
        Settings.Language = oDBTV.Language

        Dim _scraper As New TVDBs.Scraper(Settings)
        Dim FilteredOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions)

        If ScrapeModifier.MainNFO AndAlso Not ScrapeModifier.DoSearch Then
            If Not String.IsNullOrEmpty(oDBTV.TVShow.TVDB) Then
                'TVDB-ID already available -> scrape and save data into an empty tv show container (nShow)
                _scraper.GetTVShowInfo(oDBTV.TVShow.TVDB, nShow, False, FilteredOptions, False, ScrapeModifier.withEpisodes)
            ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
                'no TVDB-ID for tv show --> search first and try to get ID!
                If Not String.IsNullOrEmpty(oDBTV.TVShow.Title) Then
                    _scraper.GetSearchTVShowInfo(oDBTV.TVShow.Title, oDBTV, nShow, ScrapeType, FilteredOptions)
                End If
                'if still no ID retrieved -> exit
                If String.IsNullOrEmpty(nShow.TVDB) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
            End If
        End If

        If String.IsNullOrEmpty(nShow.TVDB) Then
            Select Case ScrapeType
                Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.MissAuto
                    nShow = Nothing
                    Return New Interfaces.ModuleResult With {.breakChain = False}
            End Select
        End If

        If ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleAuto Then
            If String.IsNullOrEmpty(oDBTV.TVShow.TVDB) Then
                Using dSearch As New dlgTVDBSearchResults(Settings, _scraper)
                    If dSearch.ShowDialog(nShow, oDBTV.TVShow.Title, oDBTV.ShowPath, FilteredOptions) = Windows.Forms.DialogResult.OK Then
                        _scraper.GetTVShowInfo(nShow.TVDB, nShow, False, FilteredOptions, False, ScrapeModifier.withEpisodes)
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

    Public Function Scraper_TVEpisode(ByRef oDBTVEpisode As Database.DBElement, ByRef nEpisode As MediaContainers.EpisodeDetails, ByVal ScrapeOptions As Structures.ScrapeOptions_TV) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_TV.Scraper_TVEpisode
        logger.Trace("Started TVDB Scraper")

        LoadSettings()

        Dim Settings As New SpecialSettings
        Settings.APIKey = _SpecialSettings.APIKey
        Settings.Language = oDBTVEpisode.Language

        Dim _scraper As New TVDBs.Scraper(Settings)
        Dim FilteredOptions As Structures.ScrapeOptions_TV = Functions.TVScrapeOptionsAndAlso(ScrapeOptions, ConfigOptions)

        If Not String.IsNullOrEmpty(oDBTVEpisode.TVShow.TVDB) Then
            If Not oDBTVEpisode.TVEpisode.Episode = -1 AndAlso Not oDBTVEpisode.TVEpisode.Season = -1 Then
                nEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBTVEpisode.TVShow.TVDB), oDBTVEpisode.TVEpisode.Season, oDBTVEpisode.TVEpisode.Episode, FilteredOptions)
            ElseIf Not String.IsNullOrEmpty(oDBTVEpisode.TVEpisode.Aired) Then
                nEpisode = _scraper.GetTVEpisodeInfo(CInt(oDBTVEpisode.TVShow.TVDB), oDBTVEpisode.TVEpisode.Aired, FilteredOptions)
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

    Structure SpecialSettings

#Region "Fields"

        Dim APIKey As String
        Dim Language As String

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class