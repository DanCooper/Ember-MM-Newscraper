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

Public Class MoviepilotDE_Data
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
    Private _Name As String = "MoviepilotDE_Data"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmMoviepilotDEInfoSettingsHolder

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
        _setup = New frmMoviepilotDEInfoSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.chkMoviepilotRating.Checked = ConfigOptions.bCert
        _setup.chkMoviepilotOutline.Checked = ConfigOptions.bOutline
        _setup.chkMoviepilotPlot.Checked = ConfigOptions.bPlot

        _setup.orderChanged()
        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = "Moviepilot"
        SPanel.Prefix = "MoviepilotDEMovieInfo_"
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
        ConfigOptions.bOutline = clsAdvancedSettings.GetBooleanSetting("DoOutline", False)
        ConfigOptions.bPlot = clsAdvancedSettings.GetBooleanSetting("DoPlot", False)
        ConfigOptions.bCert = clsAdvancedSettings.GetBooleanSetting("DoCert", False)
    End Sub

    Sub SaveSettings()
        Using settings = New clsAdvancedSettings()
            settings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
            settings.SetBooleanSetting("DoCert", ConfigOptions.bCert)
        End Using
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.ScraperModule_Data_Movie.SaveSetupScraper
        ConfigOptions.bCert = _setup.chkMoviepilotRating.Checked
        ConfigOptions.bOutline = _setup.chkMoviepilotOutline.Checked
        ConfigOptions.bPlot = _setup.chkMoviepilotPlot.Checked
        SaveSettings()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    ' ''' <summary>
    ' ''' Scrape MovieDetails from Moviepilot.de (German site)
    ' ''' </summary> 
    ' ''' <remarks>Main method to retrieve Moviepilot information - from here all other class methods gets called
    ' ''' 
    ' ''' 2013/12/21 Cocotus - First implementation
    'Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.Scraper
    '    ' as we do not have a OFDB search / show dialog we use IMDB
    '    If String.IsNullOrEmpty(DBMovie.Movie.ID) Then
    '        Dim tOpt As New Structures.ScrapeOptions_Movie 'all false value not to override any field
    '        Dim IMDB As New IMDB_Data
    '        Dim aRet As Interfaces.ModuleResult = IMDB.Scraper(DBMovie, ScrapeType, tOpt)
    '        If String.IsNullOrEmpty(DBMovie.Movie.OriginalTitle) Then
    '            Return aRet
    '        End If
    '    End If

    '    ' we have the originaltitle -> now we can use scraper methods!
    '    Dim tMoviepilotDE As New MoviepilotDE(DBMovie.Movie.OriginalTitle)

    '    'Now check what information we want to take over...
    '    Dim filterOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(Options, ConfigOptions)

    '    'Use Moviepilot FSK?
    '    If filterOptions.bCert AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Certification) OrElse Not Master.eSettings.MovieLockMPAA) Then


    '        If Not String.IsNullOrEmpty(tMoviepilotDE.FSK) Then
    '            Select Case CInt(tMoviepilotDE.FSK)
    '                Case 0
    '                    DBMovie.Movie.Certification = "Germany:0"
    '                    If Master.eSettings.MovieScraperOnlyValueForMPAA = False Then
    '                        DBMovie.Movie.MPAA = "Germany:0"
    '                    Else
    '                        DBMovie.Movie.MPAA = "0"
    '                    End If
    '                Case 6
    '                    DBMovie.Movie.Certification = "Germany:6"
    '                    If Master.eSettings.MovieScraperOnlyValueForMPAA = False Then
    '                        DBMovie.Movie.MPAA = "Germany:6"
    '                    Else
    '                        DBMovie.Movie.MPAA = "6"
    '                    End If
    '                Case 16
    '                    DBMovie.Movie.Certification = "Germany:16"
    '                    If Master.eSettings.MovieScraperOnlyValueForMPAA = False Then
    '                        DBMovie.Movie.MPAA = "Germany:16"
    '                    Else
    '                        DBMovie.Movie.MPAA = "16"
    '                    End If
    '                Case 12
    '                    DBMovie.Movie.Certification = "Germany:12"
    '                    If Master.eSettings.MovieScraperOnlyValueForMPAA = False Then
    '                        DBMovie.Movie.MPAA = "Germany:12"
    '                    Else
    '                        DBMovie.Movie.MPAA = "12"
    '                    End If
    '                Case 18
    '                    DBMovie.Movie.Certification = "Germany:18"
    '                    If Master.eSettings.MovieScraperOnlyValueForMPAA = False Then
    '                        DBMovie.Movie.MPAA = "Germany:18"
    '                    Else
    '                        DBMovie.Movie.MPAA = "18"
    '                    End If
    '            End Select
    '        End If

    '    End If

    '    'Use Moviepilot Outline?
    '    If filterOptions.bOutline AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Outline) OrElse Not Master.eSettings.MovieLockOutline OrElse (Master.eSettings.MovieScraperOutlinePlotEnglishOverwrite AndAlso StringUtils.isEnglishText(DBMovie.Movie.Outline))) Then

    '        If Not String.IsNullOrEmpty(tMoviepilotDE.Outline) Then
    '            DBMovie.Movie.Outline = tMoviepilotDE.Outline
    '        End If
    '    End If

    '    'Use Moviepilot Plot?
    '    If filterOptions.bPlot AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Plot) OrElse Not Master.eSettings.MovieLockPlot OrElse (Master.eSettings.MovieScraperOutlinePlotEnglishOverwrite AndAlso StringUtils.isEnglishText(DBMovie.Movie.Plot))) Then

    '        If Not String.IsNullOrEmpty(tMoviepilotDE.Plot) Then
    '            DBMovie.Movie.Plot = tMoviepilotDE.Plot
    '        End If
    '    End If

    '    Return New Interfaces.ModuleResult With {.breakChain = False}
    'End Function


    ''' <summary>
    '''  Scrape MovieDetails from Moviepilot.de (German site)
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">(NOT used at moment!)What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks>Cocotus/Dan 2014/08/30 - Reworked structure: Scraper should NOT consider global scraper settings/locks in Ember, just scraper options of module</remarks>
    Function ScraperNew(ByRef DBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.ScraperNew
        logger.Trace("Started MoviepilotDE ScraperNew")

        'Moviepilot-datascraper needs originaltitle of movie!
        If String.IsNullOrEmpty(DBMovie.Movie.OriginalTitle) Then
            logger.Trace("Originaltitle of movie is needed, but not availaible! Leave MoviepilotDE scraper...")
            Return New Interfaces.ModuleResult With {.breakChain = False}
        End If

        'we have the originaltitle -> now we can use scraper methods!
        Dim tmpMoviepilotDE As New MoviepilotDE(DBMovie.Movie.OriginalTitle)

        'Use Moviepilot FSK?
        If ConfigOptions.bCert Then
            If Not String.IsNullOrEmpty(tmpMoviepilotDE.FSK) Then
                Select Case CInt(tmpMoviepilotDE.FSK)
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
        nMovie.Scrapersource = "MOVIEPILOT"

        'Use Moviepilot Outline?
        If ConfigOptions.bOutline Then
            If Not String.IsNullOrEmpty(tmpMoviepilotDE.Outline) Then
                nMovie.Outline = tmpMoviepilotDE.Outline
            End If
        End If

        'Use Moviepilot Plot?
        If ConfigOptions.bPlot Then
            If Not String.IsNullOrEmpty(tmpMoviepilotDE.Plot) Then
                nMovie.Plot = tmpMoviepilotDE.Plot
            End If
        End If

        logger.Trace("Finished MoviepilotDE ScraperNew")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Function GetTMDBID(ByVal sIMDBID As String, ByRef sTMDBID As String) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetTMDBID
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

#End Region 'Methods

End Class