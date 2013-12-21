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

Public Class OFDB_Data
    Implements Interfaces.EmberMovieScraperModule_Data


#Region "Fields"

    Public Shared ConfigOptions As New Structures.ScrapeOptions
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

    Public Event ModuleSettingsChanged() Implements Interfaces.EmberMovieScraperModule_Data.ModuleSettingsChanged

    'Public Event ScraperUpdateMediaList(ByVal col As Integer, ByVal v As Boolean) Implements Interfaces.EmberMovieScraperModule.MovieScraperEvent
    Public Event MovieScraperEvent(ByVal eType As Enums.MovieScraperEventType, ByVal Parameter As Object) Implements Interfaces.EmberMovieScraperModule_Data.MovieScraperEvent

    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberMovieScraperModule_Data.ScraperSetupChanged

    Public Event SetupNeedsRestart() Implements Interfaces.EmberMovieScraperModule_Data.SetupNeedsRestart

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.EmberMovieScraperModule_Data.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.EmberMovieScraperModule_Data.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.EmberMovieScraperModule_Data.ScraperEnabled
        Get
            Return _ScraperEnabled
        End Get
        Set(ByVal value As Boolean)
            _ScraperEnabled = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"
    Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule_Data.GetMovieStudio
        'Dim IMDB As New IMDB.Scraper
        'IMDB.UseOFDBTitle = MySettings.UseOFDBTitle
        'IMDB.UseOFDBOutline = MySettings.UseOFDBOutline
        'IMDB.UseOFDBPlot = MySettings.UseOFDBPlot
        'IMDB.UseOFDBGenre = MySettings.UseOFDBGenre
        'IMDB.IMDBURL = MySettings.IMDBURL
        'studio = IMDB.GetMovieStudios(DBMovie.Movie.IMDBID)
        'Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Private Sub Handle_ModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
        ScraperEnabled = state
        RaiseEvent SetupScraperChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.EmberMovieScraperModule_Data.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub


    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberMovieScraperModule_Data.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmOFDBInfoSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.chkOFDBTitle.Checked = ConfigOptions.bTitle
        _setup.chkOFDBOutline.Checked = ConfigOptions.bOutline
        _setup.chkOFDBPlot.Checked = ConfigOptions.bPlot
        _setup.chkOFDBGenre.Checked = ConfigOptions.bGenre
        _setup.chkOFDBCleanPlotOutline.Checked = ConfigOptions.bCleanPlotOutline

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
        ConfigOptions.bTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True)
        ConfigOptions.bYear = False
        ConfigOptions.bMPAA = False
        ConfigOptions.bRelease = False
        ConfigOptions.bRuntime = False
        ConfigOptions.bRating = False
        ConfigOptions.bVotes = False
        ConfigOptions.bStudio = False
        ConfigOptions.bTagline = False
        ConfigOptions.bOutline = AdvancedSettings.GetBooleanSetting("DoOutline", True)
        ConfigOptions.bPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True)
        ConfigOptions.bCast = False
        ConfigOptions.bDirector = False
        ConfigOptions.bWriters = False
        ConfigOptions.bProducers = False
        ConfigOptions.bGenre = AdvancedSettings.GetBooleanSetting("DoGenres", True)
        ConfigOptions.bTrailer = False
        ConfigOptions.bMusicBy = False
        ConfigOptions.bOtherCrew = False
        ConfigOptions.bFullCast = False
        ConfigOptions.bFullCrew = False
        ConfigOptions.bTop250 = False
        ConfigOptions.bCountry = False
        ConfigOptions.bCert = False
        ConfigOptions.bFullCast = False
        ConfigOptions.bFullCrew = False
        ConfigOptions.bCleanPlotOutline = AdvancedSettings.GetBooleanSetting("CleanPlotOutline", False)

        ConfigScrapeModifier.DoSearch = True
        ConfigScrapeModifier.Meta = True
        ConfigScrapeModifier.NFO = True
        ConfigScrapeModifier.EThumbs = True
        ConfigScrapeModifier.EFanarts = True
        ConfigScrapeModifier.Actors = True

        ConfigScrapeModifier.Poster = AdvancedSettings.GetBooleanSetting("DoPoster", True)
        ConfigScrapeModifier.Fanart = AdvancedSettings.GetBooleanSetting("DoFanart", True)
        ConfigScrapeModifier.Trailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True)
    End Sub

    Sub SaveSettings()
        Using settings = New AdvancedSettings()
            settings.SetBooleanSetting("DoTitle", ConfigOptions.bTitle)
            settings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
            settings.SetBooleanSetting("DoGenres", ConfigOptions.bGenre)
            settings.SetBooleanSetting("CleanPlotOutline", ConfigOptions.bCleanPlotOutline)

            settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier.Poster)
            settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier.Fanart)
            'settings.SetBooleanSetting("DoTrailer", ConfigScrapeModifier.Trailer)
        End Using
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberMovieScraperModule_Data.SaveSetupScraper
        ConfigOptions.bTitle = _setup.chkOFDBTitle.Checked
        ConfigOptions.bOutline = _setup.chkOFDBOutline.Checked
        ConfigOptions.bPlot = _setup.chkOFDBPlot.Checked
        ConfigOptions.bGenre = _setup.chkOFDBGenre.Checked
        ConfigOptions.bCleanPlotOutline = _setup.chkOFDBCleanPlotOutline.Checked
        SaveSettings()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule_Data.Scraper
        ' as we do not have a OFDB search / show dialog we use IMDB
        If String.IsNullOrEmpty(DBMovie.Movie.ID) Then
            Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
            Dim IMDB As New IMDB_Data
            Dim aRet As Interfaces.ModuleResult = IMDB.Scraper(DBMovie, ScrapeType, tOpt)
            If String.IsNullOrEmpty(DBMovie.Movie.ID) Then
                Return aRet
            End If
        End If

        ' we have the ID
        Dim tOFDB As New OFDB(DBMovie.Movie.ID, DBMovie.Movie)

        Dim filterOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(Options, ConfigOptions)

        If filterOptions.bTitle AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Title) OrElse Not Master.eSettings.LockTitle) Then
            If Not String.IsNullOrEmpty(tOFDB.Title) Then
                DBMovie.Movie.Title = tOFDB.Title
            End If
        End If
        If filterOptions.bOutline AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Outline) OrElse Not Master.eSettings.LockOutline) Then

            If Not String.IsNullOrEmpty(tOFDB.Outline) Then
                'check if brackets should be removed...
                If ConfigOptions.bCleanPlotOutline Then
                    DBMovie.Movie.Outline = StringUtils.RemoveBrackets(tOFDB.Outline)
                Else
                    DBMovie.Movie.Outline = tOFDB.Outline
                End If

            End If
        End If

        If filterOptions.bPlot AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Plot) OrElse Not Master.eSettings.LockPlot) Then
            If Not String.IsNullOrEmpty(tOFDB.Plot) Then
                'check if brackets should be removed...
                If ConfigOptions.bCleanPlotOutline Then
                    DBMovie.Movie.Plot = StringUtils.RemoveBrackets(tOFDB.Plot)
                Else
                    DBMovie.Movie.Plot = tOFDB.Plot
                End If
            End If
        End If

        If filterOptions.bGenre AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Genre) OrElse Not Master.eSettings.LockGenre) Then
            If Not String.IsNullOrEmpty(tOFDB.Genre) Then
                DBMovie.Movie.Genre = tOFDB.Genre
            End If
        End If

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.EmberMovieScraperModule_Data.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

End Class