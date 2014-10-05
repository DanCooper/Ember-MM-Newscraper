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
    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.GetMovieStudio
        Return New Interfaces.ModuleResult With {.breakChain = False}
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
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    'Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.Scraper
    '    ' as we do not have a OFDB search / show dialog we use IMDB
    '    If String.IsNullOrEmpty(DBMovie.Movie.ID) Then
    '        Dim tOpt As New Structures.ScrapeOptions_Movie 'all false value not to override any field
    '        Dim IMDB As New IMDB_Data
    '        Dim aRet As Interfaces.ModuleResult = IMDB.Scraper(DBMovie, ScrapeType, tOpt)
    '        If String.IsNullOrEmpty(DBMovie.Movie.ID) Then
    '            Return aRet
    '        End If
    '    End If

    '    ' we have the ID
    '    Dim tOFDB As New OFDB(DBMovie.Movie.ID, DBMovie.Movie)

    '    Dim filterOptions As Structures.ScrapeOptions_Movie = Functions.MovieScrapeOptionsAndAlso(Options, ConfigOptions)

    '    If filterOptions.bTitle AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Title) OrElse Not Master.eSettings.MovieLockTitle) Then
    '        If Not String.IsNullOrEmpty(tOFDB.Title) Then
    '            DBMovie.Movie.Title = tOFDB.Title
    '        End If
    '    End If

    '    If filterOptions.bOutline AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Outline) OrElse Not Master.eSettings.MovieLockOutline OrElse (Master.eSettings.MovieScraperOutlinePlotEnglishOverwrite AndAlso StringUtils.isEnglishText(DBMovie.Movie.Plot))) Then
    '        If Not String.IsNullOrEmpty(tOFDB.Outline) Then
    '            'check if brackets should be removed...
    '            If ConfigOptions.bCleanPlotOutline Then
    '                DBMovie.Movie.Outline = StringUtils.RemoveBrackets(tOFDB.Outline)
    '            Else
    '                DBMovie.Movie.Outline = tOFDB.Outline
    '            End If

    '        End If
    '    End If

    '    If filterOptions.bPlot AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Plot) OrElse Not Master.eSettings.MovieLockPlot OrElse (Master.eSettings.MovieScraperOutlinePlotEnglishOverwrite AndAlso StringUtils.isEnglishText(DBMovie.Movie.Plot))) Then
    '        If Not String.IsNullOrEmpty(tOFDB.Plot) Then
    '            'check if brackets should be removed...
    '            If ConfigOptions.bCleanPlotOutline Then
    '                DBMovie.Movie.Plot = StringUtils.RemoveBrackets(tOFDB.Plot)
    '            Else
    '                DBMovie.Movie.Plot = tOFDB.Plot
    '            End If
    '        End If
    '    End If

    '    If filterOptions.bGenre AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Genre) OrElse Not Master.eSettings.MovieLockGenre) Then
    '        If Not String.IsNullOrEmpty(tOFDB.Genre) Then
    '            DBMovie.Movie.Genre = tOFDB.Genre
    '        End If
    '    End If

    '    'Use OFDB FSK?
    '    If filterOptions.bCert AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Certification) OrElse Not Master.eSettings.MovieLockMPAA) Then


    '        If Not String.IsNullOrEmpty(tOFDB.FSK) Then
    '            Select Case CInt(tOFDB.FSK)
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


    '    Return New Interfaces.ModuleResult With {.breakChain = False}
    'End Function


    ''' <summary>
    '''  Scrape MovieDetails from OFDB
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped. DBMovie as ByRef to use existing data for identifing movie and to fill with IMDB/TMDB ID for next scraper</param>
    ''' <param name="nMovie">New scraped movie data</param>
    ''' <param name="Options">(NOT used at moment!)What kind of data is being requested from the scrape(global scraper settings)</param>
    ''' <returns>Structures.DBMovie Object (nMovie) which contains the scraped data</returns>
    ''' <remarks>Cocotus/Dan 2014/08/30 - Reworked structure: Scraper should NOT consider global scraper settings/locks in Ember, just scraper options of module</remarks>
    Function ScraperNew(ByRef DBMovie As Structures.DBMovie, ByRef nMovie As MediaContainers.Movie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions_Movie) As Interfaces.ModuleResult Implements Interfaces.ScraperModule_Data_Movie.ScraperNew
        logger.Trace("Started OFDB ScraperNew")

        'datascraper needs imdb of movie!
        If String.IsNullOrEmpty(DBMovie.Movie.ID) Then
            logger.Trace("IMDB-ID of movie is needed, but not availaible! Leave OFDB scraper...")
            Return New Interfaces.ModuleResult With {.breakChain = False}
        End If

        nMovie.Scrapersource = "OFDB"

        ' we have the IMDB-ID -> now we can use scraper methods!
        Dim tOFDB As New OFDB(DBMovie.Movie.ID)

        'Use OFDB title?
        If ConfigOptions.bTitle Then
            If Not String.IsNullOrEmpty(tOFDB.Title) Then
                nMovie.Title = tOFDB.Title
            End If
        End If

        'Use OFDB outline?
        If ConfigOptions.bOutline Then
            If Not String.IsNullOrEmpty(tOFDB.Outline) Then
                nMovie.Outline = tOFDB.Outline
            End If
        End If

        'Use OFDB plot?
        If ConfigOptions.bPlot Then
            If Not String.IsNullOrEmpty(tOFDB.Plot) Then
                nMovie.Plot = tOFDB.Plot
            End If
        End If

        'Use OFDB genres?
        If ConfigOptions.bGenre Then
            If Not String.IsNullOrEmpty(tOFDB.Genre) Then
                nMovie.Genre = tOFDB.Genre
            End If
        End If

        'Use OFDB FSK?
        If ConfigOptions.bCert Then
            If Not String.IsNullOrEmpty(tOFDB.FSK) Then

                Select Case CInt(tOFDB.FSK)
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

        logger.Trace("Finished OFDB ScraperNew")
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.ScraperModule_Data_Movie.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

End Class