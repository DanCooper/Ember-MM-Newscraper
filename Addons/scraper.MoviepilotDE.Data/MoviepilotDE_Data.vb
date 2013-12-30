﻿' ################################################################################
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

Public Class MoviepilotDE_Data
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
    Private _Name As String = "MoviepilotDE_Data"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmMoviepilotDEInfoSettingsHolder


#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.EmberMovieScraperModule_Data.ModuleSettingsChanged

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
        _setup = New frmMoviepilotDEInfoSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.chkMoviepilotRating.Checked = ConfigOptions.bCert
        _setup.chkMoviepilotOutline.Checked = ConfigOptions.bOutline
        _setup.chkMoviepilotPlot.Checked = ConfigOptions.bPlot
        _setup.chkMoviepilotCleanPlotOutline.Checked = ConfigOptions.bCleanPlotOutline

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
        ConfigOptions.bTitle = False
        ConfigOptions.bYear = False
        ConfigOptions.bMPAA = False
        ConfigOptions.bRelease = False
        ConfigOptions.bRuntime = False
        ConfigOptions.bRating = False
        ConfigOptions.bVotes = False
        ConfigOptions.bStudio = False
        ConfigOptions.bTagline = False
        ConfigOptions.bOutline = AdvancedSettings.GetBooleanSetting("DoOutline", False)
        ConfigOptions.bPlot = AdvancedSettings.GetBooleanSetting("DoPlot", False)
        ConfigOptions.bCast = False
        ConfigOptions.bDirector = False
        ConfigOptions.bWriters = False
        ConfigOptions.bProducers = False
        ConfigOptions.bGenre = False
        ConfigOptions.bTrailer = False
        ConfigOptions.bMusicBy = False
        ConfigOptions.bOtherCrew = False
        ConfigOptions.bFullCast = False
        ConfigOptions.bFullCrew = False
        ConfigOptions.bTop250 = False
        ConfigOptions.bCountry = False
        ConfigOptions.bCert = AdvancedSettings.GetBooleanSetting("DoCert", False)
        ConfigOptions.bFullCast = False
        ConfigOptions.bFullCrew = False
        ConfigOptions.bCleanPlotOutline = AdvancedSettings.GetBooleanSetting("CleanPlotOutline", True)

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
            settings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
            settings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
            settings.SetBooleanSetting("DoCert", ConfigOptions.bCert)
            settings.SetBooleanSetting("CleanPlotOutline", ConfigOptions.bCleanPlotOutline)

            'settings.SetBooleanSetting("DoPoster", ConfigScrapeModifier.Poster)
            'settings.SetBooleanSetting("DoFanart", ConfigScrapeModifier.Fanart)
        End Using
    End Sub

    Private Sub Handle_PostModuleSettingsChanged()
        RaiseEvent ModuleSettingsChanged()
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberMovieScraperModule_Data.SaveSetupScraper
        ConfigOptions.bCert = _setup.chkMoviepilotRating.Checked
        ConfigOptions.bOutline = _setup.chkMoviepilotOutline.Checked
        ConfigOptions.bPlot = _setup.chkMoviepilotPlot.Checked
        ConfigOptions.bCleanPlotOutline = _setup.chkMoviepilotCleanPlotOutline.Checked
        SaveSettings()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            _setup.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' Scrape MovieDetails from Moviepilot.de (German site)
    ''' </summary> 
    ''' <remarks>Main method to retrieve Moviepilot information - from here all other class methods gets called
    ''' 
    ''' 2013/12/21 Cocotus - First implementation
    Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule_Data.Scraper
        ' as we do not have a OFDB search / show dialog we use IMDB
        If String.IsNullOrEmpty(DBMovie.Movie.ID) Then
            Dim tOpt As New Structures.ScrapeOptions 'all false value not to override any field
            Dim IMDB As New IMDB_Data
            Dim aRet As Interfaces.ModuleResult = IMDB.Scraper(DBMovie, ScrapeType, tOpt)
            If String.IsNullOrEmpty(DBMovie.Movie.OriginalTitle) Then
                Return aRet
            End If
        End If

        ' we have the originaltitle -> now we can use scraper methods!
        Dim tMoviepilotDE As New MoviepilotDE(DBMovie.Movie.OriginalTitle, DBMovie.Movie)

        'Now check what information we want to take over...
        Dim filterOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(Options, ConfigOptions)

        'Use Moviepilot FSK?
        If filterOptions.bCert AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Certification) OrElse Not Master.eSettings.LockMPAA) Then


            If Not String.IsNullOrEmpty(tMoviepilotDE.FSK) Then
                Select Case CInt(tMoviepilotDE.FSK)
                    Case 0
                        DBMovie.Movie.Certification = "Germany:0"
                        If Master.eSettings.OnlyValueForCert = False Then
                            DBMovie.Movie.MPAA = "Germany:0"
                        Else
                            DBMovie.Movie.MPAA = "0"
                        End If
                    Case 6
                        DBMovie.Movie.Certification = "Germany:6"
                        If Master.eSettings.OnlyValueForCert = False Then
                            DBMovie.Movie.MPAA = "Germany:6"
                        Else
                            DBMovie.Movie.MPAA = "6"
                        End If
                    Case 16
                        DBMovie.Movie.Certification = "Germany:16"
                        If Master.eSettings.OnlyValueForCert = False Then
                            DBMovie.Movie.MPAA = "Germany:16"
                        Else
                            DBMovie.Movie.MPAA = "16"
                        End If
                    Case 12
                        DBMovie.Movie.Certification = "Germany:12"
                        If Master.eSettings.OnlyValueForCert = False Then
                            DBMovie.Movie.MPAA = "Germany:12"
                        Else
                            DBMovie.Movie.MPAA = "12"
                        End If
                    Case 18
                        DBMovie.Movie.Certification = "Germany:18"
                        If Master.eSettings.OnlyValueForCert = False Then
                            DBMovie.Movie.MPAA = "Germany:18"
                        Else
                            DBMovie.Movie.MPAA = "18"
                        End If
                End Select
            End If

        End If

        'Use Moviepilot Outline?
        If filterOptions.bOutline AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Outline) OrElse Not Master.eSettings.LockOutline) Then

            If Not String.IsNullOrEmpty(tMoviepilotDE.Outline) Then
                'check if brackets should be removed...
                If ConfigOptions.bCleanPlotOutline Then
                    DBMovie.Movie.Outline = StringUtils.RemoveBrackets(tMoviepilotDE.Outline)
                Else
                    DBMovie.Movie.Outline = tMoviepilotDE.Outline
                End If
            End If
        End If

        'Use Moviepilot Plot?
        If filterOptions.bPlot AndAlso (String.IsNullOrEmpty(DBMovie.Movie.Plot) OrElse Not Master.eSettings.LockPlot) Then
            If Not String.IsNullOrEmpty(tMoviepilotDE.Plot) Then
                'check if brackets should be removed...
                If ConfigOptions.bCleanPlotOutline Then
                    DBMovie.Movie.Plot = StringUtils.RemoveBrackets(tMoviepilotDE.Plot)
                Else
                    DBMovie.Movie.Plot = tMoviepilotDE.Plot
                End If
            End If
        End If

        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.EmberMovieScraperModule_Data.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

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

#End Region 'Methods

End Class