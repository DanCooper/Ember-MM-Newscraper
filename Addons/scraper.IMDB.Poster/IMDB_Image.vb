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
Public Class IMDB_Image
    Implements Interfaces.ScraperModule_Image_Movie


#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Public Shared ConfigModifier As New Structures.ScrapeModifiers
    Public Shared _AssemblyName As String

    Private _Name As String = "IMDB_Poster"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmSettingsHolder
    Private IMDB As IMDB.Scraper

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.ScraperModule_Image_Movie.ModuleSettingsChanged
    Public Event MovieScraperEvent(ByVal eType As Enums.ScraperEventType, ByVal Parameter As Object) Implements Interfaces.ScraperModule_Image_Movie.ScraperEvent
    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.ScraperModule_Image_Movie.ScraperSetupChanged
    Public Event SetupNeedsRestart() Implements Interfaces.ScraperModule_Image_Movie.SetupNeedsRestart
    Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image)) Implements Interfaces.ScraperModule_Image_Movie.ImagesDownloaded
    Public Event ProgressUpdated(ByVal iPercent As Integer) Implements Interfaces.ScraperModule_Image_Movie.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.ScraperModule_Image_Movie.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.ScraperModule_Image_Movie.ScraperEnabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Function QueryScraperCapabilities(ByVal cap As Enums.ModifierType) As Boolean Implements Interfaces.ScraperModule_Image_Movie.QueryScraperCapabilities
        Select Case cap
            Case Enums.ModifierType.MainPoster
                Return ConfigModifier.MainPoster
        End Select
        Return False
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

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.ScraperModule_Image_Movie.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
        IMDB = New IMDB.Scraper()
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.ScraperModule_Image_Movie.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmSettingsHolder
        LoadSettings()
        _setup.chkEnabled.Checked = _ScraperEnabled
        _setup.orderChanged()
        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = "IMDB"
        SPanel.Prefix = "IMDBMovieMedia_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieMedia"
        SPanel.Type = Master.eLang.GetString(36, "Movies")
        SPanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return SPanel
    End Function

    Sub LoadSettings()
        ConfigModifier.MainPoster = clsAdvancedSettings.GetBooleanSetting("DoPoster", True)
    End Sub

    Function Scraper(ByRef DBMovie As Database.DBElement, ByRef ImagesContainer As MediaContainers.SearchResultsContainer, ByVal ScrapeModifiers As Structures.ScrapeModifiers) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Image_Movie.Scraper
        logger.Trace("[IMDB_Image] [Scraper] [Start]")
        LoadSettings()

        ImagesContainer = IMDB.GetIMDBPosters(DBMovie.Movie.IMDBID)

        logger.Trace("[IMDB_Image] [Scraper] [Start]")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoPoster", ConfigModifier.MainPoster)
        End Using
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Image_Movie.SaveSetupScraper
        SaveSettings()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.ScraperModule_Image_Movie.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"


#End Region 'Nested Types

End Class