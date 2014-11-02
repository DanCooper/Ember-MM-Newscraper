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
Imports NLog
Imports EmberAPI

Public Class OFDB_Data
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
    'Private IMDB As New IMDB.Scraper
    Private _Name As String = "OFDB_Data"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmOFDBInfoSettingsHolder


#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.ScraperModule_Data_Movie.ModuleSettingsChanged

    'Public Event ScraperUpdateMediaList(ByVal col As Integer, ByVal v As Boolean) Implements Interfaces.EmberMovieScraperModule.MovieScraperEvent
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
    Async Function GetMovieStudio(ByVal DBMovie As Structures.DBMovie, ByVal studio As List(Of String)) As Threading.Tasks.Task(Of Interfaces.ModuleResult) Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        ' return objects
        ' DBMovie
        ' studio
        Dim ret As New Interfaces.ModuleResult(True)
        ret.breakChain = False
        ret.ReturnObj.Add(DBMovie)
        ret.ReturnObj.Add(studio)
        Return ret
    End Function

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDBID
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Private Sub Handle_ModuleSettingsChanged()
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
        _setup = New frmOFDBInfoSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.chkTitle.Checked = ConfigOptions.bTitle
        _setup.chkOutline.Checked = ConfigOptions.bOutline
        _setup.chkPlot.Checked = ConfigOptions.bPlot
        _setup.chkGenre.Checked = ConfigOptions.bGenre
        _setup.chkRating.Checked = ConfigOptions.bCert
        _setup.orderChanged()
        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = Master.eLang.GetString(895, "OFDB")
        SPanel.Prefix = "OFDBMovieInfo_"
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
        ' Only the ones we can get
        ConfigOptions.bTitle = clsAdvancedSettings.GetBooleanSetting("DoTitle", True)
        ConfigOptions.bOutline = clsAdvancedSettings.GetBooleanSetting("DoOutline", True)
        ConfigOptions.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", True)
        ConfigOptions.bGenre = clsAdvancedSettings.GetBooleanSetting("DoGenres", True)
        ConfigOptions.bCert = clsAdvancedSettings.GetBooleanSetting("DoCert", False)
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoTitle", ConfigOptions.bTitle)
            settings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
            settings.SetBooleanSetting("DoGenres", ConfigOptions.bGenre)
            settings.SetBooleanSetting("DoCert", ConfigOptions.bCert)
        End Using
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        ConfigOptions.bCert = _setup.chkRating.Checked
        ConfigOptions.bTitle = _setup.chkTitle.Checked
        ConfigOptions.bOutline = _setup.chkOutline.Checked
        ConfigOptions.bPlot = _setup.chkPlot.Checked
        ConfigOptions.bGenre = _setup.chkGenre.Checked
        SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub
    ''' <summary>
    '''  Scrape MovieDetails from OFDB
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks>Cocotus/Dan 2014/08/30 - Reworked structure: Scraper should NOT consider global scraper settings/locks in Ember, just scraper options of module</remarks>
    Async Function Scraper(ByVal oDBMovie As Structures.DBMovie, ByVal nMovie As MediaContainers.Movie, ByVal ScrapeType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie) As Threading.Tasks.Task(Of Interfaces.ModuleResult) Implements Interfaces.ScraperModule_Data_Movie.Scraper
        ' Return Objects are
        ' oDBMovie
        ' nMovie
        ' ScrapeType
        ' Options        
        logger.Trace("Started OFDB Scraper")
        Dim ret As New Interfaces.ModuleResult(True)
        ret.Cancelled = False
        ret.breakChain = False
        ret.ReturnObj.Add(oDBMovie)
        ret.ReturnObj.Add(nMovie)
        ret.ReturnObj.Add(ScrapeType)
        ret.ReturnObj.Add(Options)

        Dim filterOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(Options, ConfigOptions)

        'datascraper needs imdb of movie!
        If String.IsNullOrEmpty(oDBMovie.Movie.ID) Then
            logger.Trace("IMDB-ID of movie is needed, but not availaible! Leave OFDB scraper...")
            Return ret
        End If

        nMovie.Scrapersource = "OFDB"

        ' we have the IMDB-ID -> now we can use scraper methods!
        Dim _scraper As New OFDB.Scraper()
        Await _scraper.Init(oDBMovie.Movie.ID)

        'Use OFDB title?
        If filterOptions.bTitle Then
            If Not String.IsNullOrEmpty(_scraper.Title) Then
                nMovie.Title = _scraper.Title
            End If
        End If

        'Use OFDB outline?
        If filterOptions.bOutline Then
            If Not String.IsNullOrEmpty(_scraper.Outline) Then
                nMovie.Outline = _scraper.Outline
            End If
        End If

        'Use OFDB plot?
        If filterOptions.bPlot Then
            If Not String.IsNullOrEmpty(_scraper.Plot) Then
                nMovie.Plot = _scraper.Plot
            End If
        End If

        'Use OFDB genres?
        If filterOptions.bGenre Then
            If Not String.IsNullOrEmpty(_scraper.Genre) Then
                nMovie.Genre = _scraper.Genre
            End If
        End If

        'Use OFDB FSK?
        If filterOptions.bCert Then
            If Not String.IsNullOrEmpty(_scraper.FSK) Then

                Select Case CInt(_scraper.FSK)
                    Case 0
                        nMovie.Certification = "Germany:0"
                        If Master.eSettings.MovieScraperCertOnlyValue = False Then
                            nMovie.MPAA = "Germany:0"
                        Else
                            nMovie.MPAA = "0"
                        End If
                    Case 6
                        nMovie.Certification = "Germany:6"
                        If Master.eSettings.MovieScraperCertOnlyValue = False Then
                            nMovie.MPAA = "Germany:6"
                        Else
                            nMovie.MPAA = "6"
                        End If
                    Case 16
                        nMovie.Certification = "Germany:16"
                        If Master.eSettings.MovieScraperCertOnlyValue = False Then
                            nMovie.MPAA = "Germany:16"
                        Else
                            nMovie.MPAA = "16"
                        End If
                    Case 12
                        nMovie.Certification = "Germany:12"
                        If Master.eSettings.MovieScraperCertOnlyValue = False Then
                            nMovie.MPAA = "Germany:12"
                        Else
                            nMovie.MPAA = "12"
                        End If
                    Case 18
                        nMovie.Certification = "Germany:18"
                        If Master.eSettings.MovieScraperCertOnlyValue = False Then
                            nMovie.MPAA = "Germany:18"
                        Else
                            nMovie.MPAA = "18"
                        End If
                End Select
            End If
        End If

        logger.Trace("Finished OFDB Scraper")
        ret.Cancelled = False
        ret.breakChain = False
        ret.ReturnObj.Clear()
        ret.ReturnObj.Add(oDBMovie)
        ret.ReturnObj.Add(nMovie)
        ret.ReturnObj.Add(ScrapeType)
        ret.ReturnObj.Add(Options)

        Return ret
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

End Class