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

Public Class TVDB_Data
    Implements Interfaces.EmberTVScraperModule_Data

#Region "Fields"

    Public Shared TVScraper As Scraper
    Public Shared _AssemblyName As String

    Private _Name As String = "TVDB Scraper"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmTVDBInfoSettingsHolder
    Private _MySettings As New sMySettings

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.EmberTVScraperModule_Data.ModuleSettingsChanged

    Public Event ScraperSetupChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberTVScraperModule_Data.ScraperSetupChanged

    Public Event TVScraperEvent(ByVal eType As Enums.TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object) Implements Interfaces.EmberTVScraperModule_Data.TVScraperEvent

    Public Event SetupNeedsRestart() Implements Interfaces.EmberTVScraperModule_Data.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property IsBusy() As Boolean Implements Interfaces.EmberTVScraperModule_Data.IsBusy
        Get
            Return TVScraper.IsBusy
        End Get
    End Property

    Public ReadOnly Property ModuleName() As String Implements Interfaces.EmberTVScraperModule_Data.ModuleName
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property ModuleVersion() As String Implements Interfaces.EmberTVScraperModule_Data.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Public Property ScraperEnabled() As Boolean Implements Interfaces.EmberTVScraperModule_Data.ScraperEnabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub CancelAsync() Implements Interfaces.EmberTVScraperModule_Data.CancelAsync
        TVScraper.CancelAsync()
    End Sub

    Public Function ChangeEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Lang As String, ByRef epDet As MediaContainers.EpisodeDetails) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule_Data.ChangeEpisode
        epDet = TVScraper.ChangeEpisode(ShowID, TVDBID, Lang)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function GetLangs(ByVal sMirror As String, ByRef Langs As List(Of Containers.TVLanguage)) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule_Data.GetLangs
        Langs = TVScraper.GetLangs(sMirror)
        Return New Interfaces.ModuleResult With {.breakChain = True}
    End Function

    Public Function GetSingleEpisode(ByVal ShowID As Integer, ByVal TVDBID As String, ByVal Season As Integer, ByVal Episode As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByRef epDetails As MediaContainers.EpisodeDetails) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule_Data.GetSingleEpisode
        epDetails = TVScraper.GetSingleEpisode(ShowID, TVDBID, Season, Episode, Lang, Ordering, Options)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Public Sub Handler_ScraperEvent(ByVal eType As Enums.TVScraperEventType, ByVal iProgress As Integer, ByVal Parameter As Object)
        RaiseEvent TVScraperEvent(eType, iProgress, Parameter)
    End Sub

    Public Sub Init(ByVal sAssemblyName As String) Implements Interfaces.EmberTVScraperModule_Data.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
        AddHandler TVScraper.ScraperEvent, AddressOf Handler_ScraperEvent
        TVScraper = New Scraper(_MySettings)
    End Sub

    Public Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberTVScraperModule_Data.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmTVDBInfoSettingsHolder
        LoadSettings()
        If String.IsNullOrEmpty(_MySettings.TVDBAPIKey) Then
            _MySettings.TVDBAPIKey = Master.eLang.GetString(122, "Get your API Key from www.themoviedb.org")
        End If
        _setup.txtTVDBApiKey.Text = _MySettings.TVDBAPIKey

        _setup.cbEnabled.Checked = _ScraperEnabled
        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = Master.eLang.GetString(0, "TVDB Scraper")
        SPanel.Prefix = "TVDBInfo_"
        SPanel.Type = Master.eLang.GetString(698, "TV Shows", True)
        SPanel.ImageIndex = If(Me._ScraperEnabled, 9, 10)
        SPanel.Order = 100
        SPanel.Panel = Me._setup.pnlSettings
        SPanel.Parent = "pnlTVData"
        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_PostModuleSettingsChanged
        Return SPanel
    End Function

    Sub LoadSettings()
        _MySettings.TVDBAPIKey = AdvancedSettings.GetSetting("TVDBAPIKey", "Get your API Key from http://www.themoviedb.org")
    End Sub

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent ScraperSetupChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
    End Sub

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.EmberTVScraperModule_Data.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

    Sub SaveSettings()
        AdvancedSettings.SetSetting("TVDBAPIKey", _MySettings.TVDBAPIKey)
    End Sub

    Public Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberTVScraperModule_Data.SaveSetupScraper
        If Not String.IsNullOrEmpty(_setup.txtTVDBApiKey.Text) Then
            _MySettings.TVDBAPIKey = _setup.txtTVDBApiKey.Text
        Else
            _MySettings.TVDBAPIKey = Master.eLang.GetString(116, "Get your key from www.tvdb.com")
        End If
        SaveSettings()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

    Public Function ScrapeEpisode(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iEpisode As Integer, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule_Data.ScrapeEpisode
        TVScraper.ScrapeEpisode(ShowID, ShowTitle, TVDBID, iEpisode, iSeason, Lang, Ordering, Options)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function Scraper(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions, ByVal ScrapeType As Enums.ScrapeType, ByVal WithCurrent As Boolean) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule_Data.Scraper
        TVScraper.SingleScrape(ShowID, ShowTitle, TVDBID, Lang, Ordering, Options, ScrapeType, WithCurrent)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Function ScrapeSeason(ByVal ShowID As Integer, ByVal ShowTitle As String, ByVal TVDBID As String, ByVal iSeason As Integer, ByVal Lang As String, ByVal Ordering As Enums.Ordering, ByVal Options As Structures.TVScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule_Data.ScrapeSeason
        TVScraper.ScrapeSeason(ShowID, ShowTitle, TVDBID, iSeason, Lang, Ordering, Options)
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"
        Dim TVDBAPIKey As String
#End Region 'Fields

    End Structure

#End Region 'Nested Types
End Class