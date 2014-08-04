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

Public Class TelevisionTunes_Theme
    Implements Interfaces.EmberTVScraperModule_Theme


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
    Public Shared ConfigOptions As New Structures.MovieScrapeOptions
    Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    ''' <summary>
    ''' Scraping Here
    ''' </summary>
    ''' <remarks></remarks>
    Private _MySettings As New sMySettings
    Private _Name As String = "TelevisionTunes_Theme"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmTelevisionTunesInfoSettingsHolder

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.EmberTVScraperModule_Theme.ModuleSettingsChanged

    Public Event TVScraperEvent(ByVal eType As Enums.TVScraperEventType, ByVal Parameter As Object) Implements Interfaces.EmberTVScraperModule_Theme.TVScraperEvent

    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberTVScraperModule_Theme.ScraperSetupChanged

    Public Event SetupNeedsRestart() Implements Interfaces.EmberTVScraperModule_Theme.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.EmberTVScraperModule_Theme.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.EmberTVScraperModule_Theme.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.EmberTVScraperModule_Theme.ScraperEnabled
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

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.EmberTVScraperModule_Theme.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberTVScraperModule_Theme.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmTelevisionTunesInfoSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled

        _setup.orderChanged()
        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = "TelevisionTunes"
        SPanel.Prefix = "TelevisionTunesTVTheme_"
        SPanel.Order = 110
        SPanel.Parent = "pnlTVTheme"
        SPanel.Type = Master.eLang.GetString(653, "TV Shows")
        SPanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        Return SPanel
    End Function

    Sub LoadSettings()
        ConfigScrapeModifier.Theme = clsAdvancedSettings.GetBooleanSetting("DoTheme", True)
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoTheme", ConfigScrapeModifier.Theme)
        End Using
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberTVScraperModule_Theme.SaveSetupScraper
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Function Scraper(ByVal DBTV As Structures.DBTV, ByRef ThemeList As List(Of Themes)) As Interfaces.ModuleResult Implements Interfaces.EmberTVScraperModule_Theme.Scraper
        logger.Trace("Started scrape")

        Dim tTelevisionTunes As New TelevisionTunes(DBTV.TVShow.Title)

        If tTelevisionTunes.ThemeList.Count > 0 Then
            ThemeList = tTelevisionTunes.ThemeList
        End If

        logger.Trace("Finished scrape")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.EmberTVScraperModule_Theme.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure sMySettings

#Region "Fields"

#End Region 'Fields

    End Structure

#End Region 'Nested Types


End Class