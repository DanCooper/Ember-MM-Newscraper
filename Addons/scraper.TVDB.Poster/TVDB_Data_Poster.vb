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


Public Class TVDB_Data_Poster
    Implements Interfaces.ScraperModule_TV

#Region "Fields"

    Public Shared _AssemblyName As String

    Public Shared TVScraper As Scraper
    Public Shared ConfigOptions As New Structures.TVScrapeOptions

    Private strPrivateAPIKey As String = String.Empty
    Private _Name As String = "TVDB Data Poster"
    Private _PostScraperEnabled As Boolean = False
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmTVInfoSettingsHolder
    Private _setupPost As frmTVMediaSettingsHolder
    Private _TVDBMirror As String
    Private _APIKey As String
    Private _Lang As String


#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.ScraperModule_TV.ModuleSettingsChanged

    Public Event SetupPostScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_TV.SetupPostScraperChanged

    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_TV.SetupScraperChanged

    Public Event TVScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object) Implements Interfaces.ScraperModule_TV.TVScraperEvent

    Public Event SetupNeedsRestart() Implements Interfaces.ScraperModule_TV.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property IsBusy() As Boolean Implements Interfaces.ScraperModule_TV.IsBusy
        Get
            Return TVScraper.IsBusy
        End Get
    End Property

    Public ReadOnly Property IsPostScraper() As Boolean Implements Interfaces.ScraperModule_TV.IsPostScraper
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsScraper() As Boolean Implements Interfaces.ScraperModule_TV.IsScraper
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_TV.ModuleName
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_TV.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Public Property PostScraperEnabled() As Boolean Implements Interfaces.ScraperModule_TV.PosterScraperEnabled
        Get
            Return _PostScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _PostScraperEnabled = value
        End Set
    End Property

    Public Property ScraperEnabled() As Boolean Implements Interfaces.ScraperModule_TV.ScraperEnabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub CancelAsync() Implements Interfaces.ScraperModule_TV.CancelAsync
        TVScraper.CancelAsync()
    End Sub

    Public Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String, ByRef epDet As MediaContainers.EpisodeDetails) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.ChangeEpisode
        epDet = TVScraper.ChangeEpisode(ShowID, TVDBID, Lang)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function GetLangs(ByVal sMirror As String, ByRef Langs As clsXMLTVDBLanguages) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.GetLangs
        Langs = TVScraper.GetLangs(sMirror)
        Return New Interfaces.ModuleResult With {.breakChain = True}
    End Function

    Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByRef epDetails As MediaContainers.EpisodeDetails) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.GetSingleEpisode
        epDetails = TVScraper.GetSingleEpisode(ShowID, TVDBID, Season, Episode, Lang, Ordering, Options)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images, ByRef Image As Images) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.GetSingleImage
        TVScraper.GetSingleImage(Title, ShowID, TVDBID, Type, Season, Episode, Lang, Ordering, CurrentImage, Image)
        Return New Interfaces.ModuleResult With {.breakChain = True}
    End Function

    Public Sub Handler_ScraperEvent(ByVal eType As Enums.ScraperEventType_TV, ByVal iProgress As Integer, ByVal Parameter As Object)
        RaiseEvent TVScraperEvent(eType, iProgress, Parameter)
    End Sub

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Public Sub Init(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_TV.Init
        _AssemblyName = sAssemblyName
        strPrivateAPIKey = clsAdvancedSettings.GetSetting("TVDBAPIKey", "")
        _APIKey = If(String.IsNullOrEmpty(strPrivateAPIKey), "353783CE455412FD", strPrivateAPIKey)
        _Lang = clsAdvancedSettings.GetSetting("TVDBLanguage", If(Not String.IsNullOrEmpty(Master.eSettings.TVGeneralLanguage), Master.eSettings.TVGeneralLanguage, "en"))
        _TVDBMirror = clsAdvancedSettings.GetSetting("TVDBMirror", "thetvdb.com")
        TVScraper = New Scraper(_APIKey)

        LoadSettings()

        AddHandler TVScraper.ScraperEvent, AddressOf Handler_ScraperEvent
    End Sub

    Public Function InjectSetupPostScraper() As Containers.SettingsPanel Implements Interfaces.ScraperModule_TV.InjectSetupPostScraper
        Dim SPanel As New Containers.SettingsPanel
        _setupPost = New frmTVMediaSettingsHolder
        _setupPost.txtTVDBApiKey.Text = strPrivateAPIKey
        _setupPost.cbEnabled.Checked = _PostScraperEnabled

        _setupPost.chkOnlyTVImagesLanguage.Checked = CBool(clsAdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True"))
        _setupPost.chkGetEnglishImages.Checked = CBool(clsAdvancedSettings.GetSetting("AlwaysGetEnglishTVImages", "True"))
        If _setupPost.cbTVScraperLanguage.Items.Count > 0 Then
            _setupPost.cbTVScraperLanguage.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = _Lang).name
        End If
        _setupPost.txtTVDBMirror.Text = _TVDBMirror


        SPanel.Name = String.Concat(Me._Name, "PostScraper")
        SPanel.Text = Master.eLang.GetString(941, "TVDB")
        SPanel.Prefix = "TVDBData_"
        SPanel.Type = Master.eLang.GetString(653, "TV Shows")
        SPanel.ImageIndex = If(Me._ScraperEnabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setupPost.pnlSettings
        SPanel.Parent = "pnlTVMedia"
        AddHandler _setupPost.SetupPostScraperChanged, AddressOf Handle_SetupPostScraperChanged
        AddHandler _setupPost.ModuleSettingsChanged, AddressOf Handle_PostModuleSettingsChanged
        Return SPanel
    End Function

    'Public Event ScraperUpdateMediaList(ByVal col As Integer, ByVal v As Boolean) Implements Interfaces.EmberTVScraperModule.ScraperUpdateMediaList
    Public Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.ScraperModule_TV.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmTVInfoSettingsHolder
        LoadSettings()
        _setup.txtTVDBApiKey.Text = strPrivateAPIKey
        _setup.cbEnabled.Checked = _ScraperEnabled

        If _setup.cbTVScraperLanguage.Items.Count > 0 Then
            _setup.cbTVScraperLanguage.Text = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = _Lang).name
        End If

        _setup.txtTVDBMirror.Text = _TVDBMirror
        _setup.chkScraperShowTitle.Checked = ConfigOptions.bShowTitle
        _setup.chkScraperShowEGU.Checked = ConfigOptions.bShowEpisodeGuide
        _setup.chkScraperShowGenre.Checked = ConfigOptions.bShowGenre
        _setup.chkScraperShowMPAA.Checked = ConfigOptions.bShowMPAA
        _setup.chkScraperShowPlot.Checked = ConfigOptions.bShowPlot
        _setup.chkScraperShowPremiered.Checked = ConfigOptions.bShowPremiered
        _setup.chkScraperShowRating.Checked = ConfigOptions.bShowRating
        _setup.chkScraperShowRuntime.Checked = ConfigOptions.bShowRuntime
        _setup.chkScraperShowStatus.Checked = ConfigOptions.bShowStatus
        _setup.chkScraperShowStudio.Checked = ConfigOptions.bShowStudio
        _setup.chkScraperShowActors.Checked = ConfigOptions.bShowActors
        _setup.chkScraperShowVotes.Checked = ConfigOptions.bShowVotes
        _setup.chkScraperEpTitle.Checked = ConfigOptions.bEpTitle
        _setup.chkScraperEpSeason.Checked = ConfigOptions.bEpSeason
        _setup.chkScraperEpEpisode.Checked = ConfigOptions.bEpEpisode
        _setup.chkScraperEpAired.Checked = ConfigOptions.bEpAired
        _setup.chkScraperEpRating.Checked = ConfigOptions.bEpRating
        _setup.chkScraperEpPlot.Checked = ConfigOptions.bEpPlot
        _setup.chkScraperEpDirector.Checked = ConfigOptions.bEpDirector
        _setup.chkScraperEpCredits.Checked = ConfigOptions.bEpCredits
        _setup.chkScraperEpActors.Checked = ConfigOptions.bEpActors
        _setup.chkScraperEpVotes.Checked = ConfigOptions.bEpVotes

        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = Master.eLang.GetString(941, "TVDB")
        SPanel.Prefix = "TVDBMedia_"
        SPanel.Type = Master.eLang.GetString(653, "TV Shows")
        SPanel.ImageIndex = If(Me._ScraperEnabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings
        SPanel.Parent = "pnlTVData"
        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return SPanel
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        strPrivateAPIKey = _setup.txtTVDBApiKey.Text
        _TVDBMirror = _setup.txtTVDBMirror.Text
        _Lang = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = _setup.cbTVScraperLanguage.Text).abbreviation
        _setupPost.txtTVDBApiKey.Text = _setup.txtTVDBApiKey.Text
        _setupPost.txtTVDBMirror = _setup.txtTVDBMirror
        _setupPost.cbTVScraperLanguage.Text = _setup.cbTVScraperLanguage.Text
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        strPrivateAPIKey = _setupPost.txtTVDBApiKey.Text
        _Lang = Master.eSettings.TVGeneralLanguages.Language.FirstOrDefault(Function(l) l.name = _setupPost.cbTVScraperLanguage.Text).abbreviation
        _TVDBMirror = _setupPost.txtTVDBMirror.Text
        _setup.txtTVDBApiKey.Text = _setupPost.txtTVDBApiKey.Text
        _setup.txtTVDBMirror = _setupPost.txtTVDBMirror
        _setup.cbTVScraperLanguage.Text = _setupPost.cbTVScraperLanguage.Text
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupPostScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        PostScraperEnabled = state
        RaiseEvent SetupPostScraperChanged(String.Concat(Me._Name, "PostScraper"), state, difforder)
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
    End Sub

    Sub LoadSettings()
        ConfigOptions.bEpActors = clsAdvancedSettings.GetBooleanSetting("ScraperEpActors", True)
        ConfigOptions.bEpAired = clsAdvancedSettings.GetBooleanSetting("ScraperEpAired", True)
        ConfigOptions.bEpCredits = clsAdvancedSettings.GetBooleanSetting("ScraperEpCredits", True)
        ConfigOptions.bEpDirector = clsAdvancedSettings.GetBooleanSetting("ScraperEpDirector", True)
        ConfigOptions.bEpEpisode = clsAdvancedSettings.GetBooleanSetting("ScraperEpEpisode", True)
        ConfigOptions.bEpPlot = clsAdvancedSettings.GetBooleanSetting("ScraperEpPlot", True)
        ConfigOptions.bEpRating = clsAdvancedSettings.GetBooleanSetting("ScraperEpRating", True)
        ConfigOptions.bEpSeason = clsAdvancedSettings.GetBooleanSetting("ScraperEpSeason", True)
        ConfigOptions.bEpTitle = clsAdvancedSettings.GetBooleanSetting("ScraperEpTitle", True)
        ConfigOptions.bEpVotes = clsAdvancedSettings.GetBooleanSetting("ScraperEpVotes", True)
        ConfigOptions.bShowActors = clsAdvancedSettings.GetBooleanSetting("ScraperShowActors", True)
        ConfigOptions.bShowEpisodeGuide = clsAdvancedSettings.GetBooleanSetting("ScraperShowEGU", False)
        ConfigOptions.bShowGenre = clsAdvancedSettings.GetBooleanSetting("ScraperShowGenre", True)
        ConfigOptions.bShowMPAA = clsAdvancedSettings.GetBooleanSetting("ScraperShowMPAA", True)
        ConfigOptions.bShowPlot = clsAdvancedSettings.GetBooleanSetting("ScraperShowPlot", True)
        ConfigOptions.bShowPremiered = clsAdvancedSettings.GetBooleanSetting("ScraperShowPremiered", True)
        ConfigOptions.bShowRating = clsAdvancedSettings.GetBooleanSetting("ScraperShowRating", True)
        ConfigOptions.bShowRuntime = clsAdvancedSettings.GetBooleanSetting("ScraperShowRuntime", True)
        ConfigOptions.bShowStatus = clsAdvancedSettings.GetBooleanSetting("ScraperShowStatus", True)
        ConfigOptions.bShowStudio = clsAdvancedSettings.GetBooleanSetting("ScraperShowStudio", True)
        ConfigOptions.bShowTitle = clsAdvancedSettings.GetBooleanSetting("ScraperShowTitle", True)
        ConfigOptions.bShowVotes = clsAdvancedSettings.GetBooleanSetting("ScraperShowVotes", True)
    End Sub
    Public Function PostScraper(ByRef DBTV As Structures.DBTV, ByVal ScrapeType As Enums.ScrapeType) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.PosterScraper
    End Function

    Public Function SaveImages() As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.SaveImages
        TVScraper.SaveImages()
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub SaveSetupPostScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_TV.SaveSetupPosterScraper
        Using settings = New clsAdvancedSettings()
            settings.SetSetting("TVDBAPIKey", strPrivateAPIKey)
            settings.SetSetting("OnlyGetTVImagesForSelectedLanguage", CStr(_setupPost.chkOnlyTVImagesLanguage.Checked))
            settings.SetSetting("AlwaysGetEnglishTVImages", CStr(_setupPost.chkGetEnglishImages.Checked))


            If Not String.IsNullOrEmpty(_Lang) Then
                settings.SetSetting("TVDBLanguage", _Lang)
            Else
                settings.SetSetting("TVDBLanguage", "en")
            End If

            If Not String.IsNullOrEmpty(_TVDBMirror) Then
                settings.SetSetting("TVDBMirror", Strings.Replace(_TVDBMirror, "http://", String.Empty))
            Else
                settings.SetSetting("TVDBMirror", "thetvdb.com")
            End If

        End Using

        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Public Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_TV.SaveSetupScraper
        Using settings = New clsAdvancedSettings()
            settings.SetSetting("TVDBAPIKey", strPrivateAPIKey)
            If Not String.IsNullOrEmpty(_Lang) Then
                settings.SetSetting("TVDBLanguage", _Lang)
            Else
                settings.SetSetting("TVDBLanguage", "en")
            End If

            If Not String.IsNullOrEmpty(_TVDBMirror) Then
                settings.SetSetting("TVDBMirror", Strings.Replace(_TVDBMirror, "http://", String.Empty))
            Else
                settings.SetSetting("TVDBMirror", "thetvdb.com")
            End If
            settings.SetBooleanSetting("ScraperShowTitle", _setup.chkScraperShowTitle.Checked)
            settings.SetBooleanSetting("ScraperShowEGU", _setup.chkScraperShowEGU.Checked)
            settings.SetBooleanSetting("ScraperShowGenre", _setup.chkScraperShowGenre.Checked)
            settings.SetBooleanSetting("ScraperShowMPAA", _setup.chkScraperShowMPAA.Checked)
            settings.SetBooleanSetting("ScraperShowPlot", _setup.chkScraperShowPlot.Checked)
            settings.SetBooleanSetting("ScraperShowPremiered", _setup.chkScraperShowPremiered.Checked)
            settings.SetBooleanSetting("ScraperShowRating", _setup.chkScraperShowRating.Checked)
            settings.SetBooleanSetting("ScraperShowStatus", _setup.chkScraperShowStatus.Checked)
            settings.SetBooleanSetting("ScraperShowStudio", _setup.chkScraperShowStudio.Checked)
            settings.SetBooleanSetting("ScraperShowActors", _setup.chkScraperShowActors.Checked)
            settings.SetBooleanSetting("ScraperShowVotes", _setup.chkScraperShowVotes.Checked)
            settings.SetBooleanSetting("ScraperEpTitle", _setup.chkScraperEpTitle.Checked)
            settings.SetBooleanSetting("ScraperEpSeason", _setup.chkScraperEpSeason.Checked)
            settings.SetBooleanSetting("ScraperEpEpisode", _setup.chkScraperEpEpisode.Checked)
            settings.SetBooleanSetting("ScraperEpAired", _setup.chkScraperEpAired.Checked)
            settings.SetBooleanSetting("ScraperEpRating", _setup.chkScraperEpRating.Checked)
            settings.SetBooleanSetting("ScraperEpPlot", _setup.chkScraperEpPlot.Checked)
            settings.SetBooleanSetting("ScraperEpDirector", _setup.chkScraperEpDirector.Checked)
            settings.SetBooleanSetting("ScraperEpCredits", _setup.chkScraperEpCredits.Checked)
            settings.SetBooleanSetting("ScraperEpActors", _setup.chkScraperEpActors.Checked)
            settings.SetBooleanSetting("ScraperEpVotes", _setup.chkScraperEpVotes.Checked)
        End Using

        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If

    End Sub

    Public Function ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.ScrapeEpisode
        LoadSettings()
        Dim filterOptions As Structures.TVScrapeOptions = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions)
        TVScraper.ScrapeEpisode(ShowID, ShowTitle, TVDBID, iEpisode, iSeason, Lang, Ordering, filterOptions)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function Scraper(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal ShowLang As String, ByVal SourceLang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.Scraper
        LoadSettings()
        Dim filterOptions As Structures.TVScrapeOptions = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions)
        TVScraper.SingleScrape(ShowID, ShowTitle, TVDBID, ShowLang, SourceLang, Ordering, filterOptions, ScrapeType, WithCurrent)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function ScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_TV.ScrapeSeason
        LoadSettings()
        Dim filterOptions As Structures.TVScrapeOptions = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions)
        TVScraper.ScrapeSeason(ShowID, ShowTitle, TVDBID, iSeason, Lang, Ordering, filterOptions)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

#End Region 'Methods

End Class