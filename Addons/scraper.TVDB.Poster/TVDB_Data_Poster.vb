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
    Implements Interfaces.EmberTVScraperModule

#Region "Fields"

    Public Shared _AssemblyName As String

    Public Shared TVScraper As Scraper
    Public Shared ConfigOptions As New Structures.TVScrapeOptions

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

    Public Event ModuleSettingsChanged() Implements Interfaces.EmberTVScraperModule.ModuleSettingsChanged

    Public Event SetupPostScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberTVScraperModule.SetupPostScraperChanged

    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberTVScraperModule.SetupScraperChanged

    Public Event TVScraperEvent(ByVal eType As Enums.TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object) Implements Interfaces.EmberTVScraperModule.TVScraperEvent

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property IsBusy() As Boolean Implements Interfaces.EmberTVScraperModule.IsBusy
        Get
            Return TVScraper.IsBusy
        End Get
    End Property

    Public ReadOnly Property IsPostScraper() As Boolean Implements Interfaces.EmberTVScraperModule.IsPostScraper
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property IsScraper() As Boolean Implements Interfaces.EmberTVScraperModule.IsScraper
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property ModuleName() As String Implements Interfaces.EmberTVScraperModule.ModuleName
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property ModuleVersion() As String Implements Interfaces.EmberTVScraperModule.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Public Property PostScraperEnabled() As Boolean Implements Interfaces.EmberTVScraperModule.PostScraperEnabled
        Get
            Return _PostScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _PostScraperEnabled = value
        End Set
    End Property

    Public Property ScraperEnabled() As Boolean Implements Interfaces.EmberTVScraperModule.ScraperEnabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub CancelAsync() Implements Interfaces.EmberTVScraperModule.CancelAsync
        TVScraper.CancelAsync()
    End Sub

    Public Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String, ByRef epDet As MediaContainers.EpisodeDetails) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.ChangeEpisode
        epDet = TVScraper.ChangeEpisode(ShowID, TVDBID, Lang)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function GetLangs(ByVal sMirror As String, ByRef Langs As List(Of Containers.TVLanguage)) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.GetLangs
        Langs = TVScraper.GetLangs(sMirror)
        Return New Interfaces.ModuleResult With {.breakChain = True}
    End Function

    Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByRef epDetails As MediaContainers.EpisodeDetails) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.GetSingleEpisode
        epDetails = TVScraper.GetSingleEpisode(ShowID, TVDBID, Season, Episode, Lang, Ordering, Options)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function GetSingleImage(ByVal Title As String, ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Type As Enums.TVImageType, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal CurrentImage As Images, ByRef Image As Images) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.GetSingleImage
        TVScraper.GetSingleImage(Title, ShowID, TVDBID, Type, Season, Episode, Lang, Ordering, CurrentImage, Image)
        Return New Interfaces.ModuleResult With {.breakChain = True}
    End Function

    Public Sub Handler_ScraperEvent(ByVal eType As Enums.TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)
        RaiseEvent TVScraperEvent(eType, iProgress, Parameter)
    End Sub

    Public Sub Init(ByVal sAssemblyName As String) Implements Interfaces.EmberTVScraperModule.Init
        _AssemblyName = sAssemblyName
        _APIKey = AdvancedSettings.GetSetting("TVDBAPIKey", "Get your API Key from http://thetvdb.com/?tab=apiregister")
        _Lang = AdvancedSettings.GetSetting("TVDBLanguage", "en")
        _TVDBMirror = AdvancedSettings.GetSetting("TVDBMirror", "thetvdb.com")
        TVScraper = New Scraper(_APIKey)

        ConfigOptions.bEpActors = CBool(AdvancedSettings.GetSetting("ScraperEpActors", "false"))
        ConfigOptions.bEpAired = CBool(AdvancedSettings.GetSetting("ScraperEpAired", "true"))
        ConfigOptions.bEpCredits = CBool(AdvancedSettings.GetSetting("ScraperEpCredits", "false"))
        ConfigOptions.bEpDirector = CBool(AdvancedSettings.GetSetting("ScraperEpDirector", "false"))
        ConfigOptions.bEpEpisode = CBool(AdvancedSettings.GetSetting("ScraperEpEpisode", "true"))
        ConfigOptions.bEpPlot = CBool(AdvancedSettings.GetSetting("ScraperEpPlot", "true"))
        ConfigOptions.bEpRating = CBool(AdvancedSettings.GetSetting("ScraperEpRating", "true"))
        ConfigOptions.bEpSeason = CBool(AdvancedSettings.GetSetting("ScraperEpSeason", "true"))
        ConfigOptions.bEpTitle = CBool(AdvancedSettings.GetSetting("ScraperEpTitle", "true"))
        ConfigOptions.bShowActors = CBool(AdvancedSettings.GetSetting("ScraperShowActors", "false"))
        ConfigOptions.bShowEpisodeGuide = CBool(AdvancedSettings.GetSetting("ScraperShowEGU", "false"))
        ConfigOptions.bShowGenre = CBool(AdvancedSettings.GetSetting("ScraperShowGenre", "true"))
        ConfigOptions.bShowMPAA = CBool(AdvancedSettings.GetSetting("ScraperShowMPAA", "true"))
        ConfigOptions.bShowPlot = CBool(AdvancedSettings.GetSetting("ScraperShowPlot", "true"))
        ConfigOptions.bShowPremiered = CBool(AdvancedSettings.GetSetting("ScraperShowPremiered", "true"))
        ConfigOptions.bShowRating = CBool(AdvancedSettings.GetSetting("ScraperShowRating", "true"))
        ConfigOptions.bShowStudio = CBool(AdvancedSettings.GetSetting("ScraperShowStudio", "true"))
        ConfigOptions.bShowTitle = CBool(AdvancedSettings.GetSetting("ScraperShowTitle", "true"))

        AddHandler TVScraper.ScraperEvent, AddressOf Handler_ScraperEvent
    End Sub

    Public Function InjectSetupPostScraper() As Containers.SettingsPanel Implements Interfaces.EmberTVScraperModule.InjectSetupPostScraper
        Dim SPanel As New Containers.SettingsPanel
        _setupPost = New frmTVMediaSettingsHolder
        _setupPost.txtTVDBApiKey.Text = _APIKey
        _setupPost.cbEnabled.Checked = _PostScraperEnabled

        _setupPost.chkOnlyTVImagesLanguage.Checked = CBool(AdvancedSettings.GetSetting("OnlyGetTVImagesForSelectedLanguage", "True"))
        _setupPost.chkGetEnglishImages.Checked = CBool(AdvancedSettings.GetSetting("AlwaysGetEnglishTVImages", "True"))
        If _setupPost.cbTVLanguage.Items.Count > 0 Then
            _setupPost.cbTVLanguage.Text = Master.eSettings.Languages.FirstOrDefault(Function(l) l.ShortLang = _Lang).LongLang
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
    Public Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberTVScraperModule.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmTVInfoSettingsHolder
        _setup.txtTVDBApiKey.Text = _APIKey
        _setup.cbEnabled.Checked = _ScraperEnabled

        If _setup.cbTVLanguage.Items.Count > 0 Then
            _setup.cbTVLanguage.Text = Master.eSettings.Languages.FirstOrDefault(Function(l) l.ShortLang = _Lang).LongLang
        End If

        _setup.txtTVDBMirror.Text = _TVDBMirror
        _setup.chkScraperShowTitle.Checked = ConfigOptions.bShowTitle
        _setup.chkScraperShowEGU.Checked = ConfigOptions.bShowEpisodeGuide
        _setup.chkScraperShowGenre.Checked = ConfigOptions.bShowGenre
        _setup.chkScraperShowMPAA.Checked = ConfigOptions.bShowMPAA
        _setup.chkScraperShowPlot.Checked = ConfigOptions.bShowPlot
        _setup.chkScraperShowPremiered.Checked = ConfigOptions.bShowPremiered
        _setup.chkScraperShowRating.Checked = ConfigOptions.bShowRating
        _setup.chkScraperShowStudio.Checked = ConfigOptions.bShowStudio
        _setup.chkScraperShowActors.Checked = ConfigOptions.bShowActors
        _setup.chkScraperEpTitle.Checked = ConfigOptions.bEpTitle
        _setup.chkScraperEpSeason.Checked = ConfigOptions.bEpSeason
        _setup.chkScraperEpEpisode.Checked = ConfigOptions.bEpEpisode
        _setup.chkScraperEpAired.Checked = ConfigOptions.bEpAired
        _setup.chkScraperEpRating.Checked = ConfigOptions.bEpRating
        _setup.chkScraperEpPlot.Checked = ConfigOptions.bEpPlot
        _setup.chkScraperEpDirector.Checked = ConfigOptions.bEpDirector
        _setup.chkScraperEpCredits.Checked = ConfigOptions.bEpCredits
        _setup.chkScraperEpActors.Checked = ConfigOptions.bEpActors

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
        Return SPanel
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        _APIKey = _setup.txtTVDBApiKey.Text
        _TVDBMirror = _setup.txtTVDBMirror.Text
        _Lang = Master.eSettings.Languages.FirstOrDefault(Function(l) l.LongLang = _setup.cbTVLanguage.Text).ShortLang
        _setupPost.txtTVDBApiKey.Text = _setup.txtTVDBApiKey.Text
        _setupPost.txtTVDBMirror = _setup.txtTVDBMirror
        _setupPost.cbTVLanguage.Text = _setup.cbTVLanguage.Text
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        _APIKey = _setupPost.txtTVDBApiKey.Text
        _Lang = Master.eSettings.Languages.FirstOrDefault(Function(l) l.LongLang = _setupPost.cbTVLanguage.Text).ShortLang
        _TVDBMirror = _setupPost.txtTVDBMirror.Text
        _setup.txtTVDBApiKey.Text = _setupPost.txtTVDBApiKey.Text
        _setup.txtTVDBMirror = _setupPost.txtTVDBMirror
        _setup.cbTVLanguage.Text = _setupPost.cbTVLanguage.Text
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
    Public Function PostScraper(ByRef DBTV As Structures.DBTV, ByVal ScrapeType As Enums.ScrapeType) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.PostScraper
    End Function

    Public Function SaveImages() As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.SaveImages
        TVScraper.SaveImages()
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub SaveSetupPostScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberTVScraperModule.SaveSetupPostScraper
        Using settings = New AdvancedSettings()
            If Not String.IsNullOrEmpty(_APIKey) Then
                settings.SetSetting("TVDBAPIKey", _APIKey)
            Else
                settings.SetSetting("TVDBAPIKey", Master.eLang.GetString(942, "Get your API Key from thetvdb.com"))
            End If
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

    Public Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberTVScraperModule.SaveSetupScraper
        Using settings = New AdvancedSettings()
            If Not String.IsNullOrEmpty(_APIKey) Then
                settings.SetSetting("TVDBAPIKey", _APIKey)
            Else
                settings.SetSetting("TVDBAPIKey", Master.eLang.GetString(942, "Get your API Key from thetvdb.com"))
            End If
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
            settings.SetBooleanSetting("ScraperShowStudio", _setup.chkScraperShowStudio.Checked)
            settings.SetBooleanSetting("ScraperShowActors", _setup.chkScraperShowActors.Checked)
            settings.SetBooleanSetting("ScraperEpTitle", _setup.chkScraperEpTitle.Checked)
            settings.SetBooleanSetting("ScraperEpSeason", _setup.chkScraperEpSeason.Checked)
            settings.SetBooleanSetting("ScraperEpEpisode", _setup.chkScraperEpEpisode.Checked)
            settings.SetBooleanSetting("ScraperEpAired", _setup.chkScraperEpAired.Checked)
            settings.SetBooleanSetting("ScraperEpRating", _setup.chkScraperEpRating.Checked)
            settings.SetBooleanSetting("ScraperEpPlot", _setup.chkScraperEpPlot.Checked)
            settings.SetBooleanSetting("ScraperEpDirector", _setup.chkScraperEpDirector.Checked)
            settings.SetBooleanSetting("ScraperEpCredits", _setup.chkScraperEpCredits.Checked)
            settings.SetBooleanSetting("ScraperEpActors", _setup.chkScraperEpActors.Checked)

        End Using

        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If

    End Sub

    Public Function ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.ScrapeEpisode
        Dim filterOptions As Structures.TVScrapeOptions = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions)
        TVScraper.ScrapeEpisode(ShowID, ShowTitle, TVDBID, iEpisode, iSeason, Lang, Ordering, filterOptions)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function Scraper(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.Scraper
        Dim filterOptions As Structures.TVScrapeOptions = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions)
        TVScraper.SingleScrape(ShowID, ShowTitle, TVDBID, Lang, Ordering, filterOptions, ScrapeType, WithCurrent)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function ScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule.ScrapeSeason
        Dim filterOptions As Structures.TVScrapeOptions = Functions.TVScrapeOptionsAndAlso(Options, ConfigOptions)
        TVScraper.ScrapeSeason(ShowID, ShowTitle, TVDBID, iSeason, Lang, Ordering, filterOptions)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

#End Region 'Methods

End Class