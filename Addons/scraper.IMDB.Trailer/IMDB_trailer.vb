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
Imports System.Text
Imports System.Text.RegularExpressions

Imports EmberAPI

''' <summary>
''' Native Scraper
''' </summary>
''' <remarks></remarks>
Public Class IMDB_trailer
    Implements Interfaces.EmberMovieScraperModule_Trailer


#Region "Fields"

    Public Shared ConfigOptions As New Structures.ScrapeOptions
    Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
    Public Shared _AssemblyName As String

    Private MySettings As New _MySettings
    Private _Name As String = "IMDB_Trailer"
    Private _ScraperEnabled As Boolean = False
    Private _setup As frmIMDBTrailerSettingsHolder

#End Region 'Fields

#Region "Events"

    Public Event ModuleSettingsChanged() Implements Interfaces.EmberMovieScraperModule_Trailer.ModuleSettingsChanged

    Public Event MovieScraperEvent(ByVal eType As Enums.MovieScraperEventType, ByVal Parameter As Object) Implements Interfaces.EmberMovieScraperModule_Trailer.MovieScraperEvent

    Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberMovieScraperModule_Trailer.ScraperSetupChanged

    Public Event SetupNeedsRestart() Implements Interfaces.EmberMovieScraperModule_Trailer.SetupNeedsRestart

    'Public Event ProgressUpdated(ByVal iPercent As Integer) Implements Interfaces.EmberMovieScraperModule_Trailer.ProgressUpdated

#End Region 'Events

#Region "Properties"

    ReadOnly Property ModuleName() As String Implements Interfaces.EmberMovieScraperModule_Trailer.ModuleName
        Get
            Return _Name
        End Get
    End Property

    ReadOnly Property ModuleVersion() As String Implements Interfaces.EmberMovieScraperModule_Trailer.ModuleVersion
        Get
            Return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
        End Get
    End Property

    Property ScraperEnabled() As Boolean Implements Interfaces.EmberMovieScraperModule_Trailer.ScraperEnabled
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

    Private Sub Handle_SetupNeedsRestart()
        RaiseEvent SetupNeedsRestart()
    End Sub

    Sub Init(ByVal sAssemblyName As String) Implements Interfaces.EmberMovieScraperModule_Trailer.Init
        _AssemblyName = sAssemblyName
        LoadSettings()
    End Sub

    Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberMovieScraperModule_Trailer.InjectSetupScraper
        Dim SPanel As New Containers.SettingsPanel
        _setup = New frmIMDBTrailerSettingsHolder
        LoadSettings()
        _setup.cbEnabled.Checked = _ScraperEnabled
        _setup.txtTimeout.Text = MySettings.TrailerTimeout.ToString

        SPanel.Name = String.Concat(Me._Name, "Scraper")
        SPanel.Text = Master.eLang.GetString(104, "IMDB")
        SPanel.Prefix = "IMDBTrailer_"
        SPanel.Order = 110
        SPanel.Parent = "pnlMovieTrailer"
        SPanel.Type = Master.eLang.GetString(36, "Movies", True)
        SPanel.ImageIndex = If(_ScraperEnabled, 9, 10)
        SPanel.Panel = _setup.pnlSettings
        AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
        AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
        AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
        Return SPanel
    End Function

    Sub LoadSettings()
        MySettings.TrailerTimeout = Convert.ToInt32(AdvancedSettings.GetSetting("TrailerTimeout", "10"))

        ConfigScrapeModifier.DoSearch = True
        ConfigScrapeModifier.Meta = True
        ConfigScrapeModifier.NFO = True
        ConfigScrapeModifier.Extra = True
        ConfigScrapeModifier.Actors = True

        ConfigScrapeModifier.Poster = AdvancedSettings.GetBooleanSetting("DoPoster", False)
        ConfigScrapeModifier.Fanart = AdvancedSettings.GetBooleanSetting("DoFanart", False)
        ConfigScrapeModifier.Trailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True)
    End Sub

    Sub SaveSettings()
        AdvancedSettings.SetSetting("TrailerTimeout", MySettings.TrailerTimeout.ToString)
        AdvancedSettings.SetBooleanSetting("DoTrailer", ConfigScrapeModifier.Trailer)
    End Sub

    Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberMovieScraperModule_Trailer.SaveSetupScraper
        SaveSettings()
        'ModulesManager.Instance.SaveSettings()
        If DoDispose Then
            RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
            RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
            RemoveHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
            _setup.Dispose()
        End If
    End Sub

    Function Scraper(ByRef DBMovie As Structures.DBMovie, ByVal Type As Enums.PostScraperCapabilities, ByRef URLList As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule_Trailer.Scraper

        Dim TrailerNumber As Integer = 0
        Dim Links As MatchCollection
        Dim trailerPage As String
        Dim trailerUrl As String
        Dim Link As Match
        Dim currPage As Integer = 0

        Dim WebPage As New HTTP
        Dim _ImdbTrailerPage As String = String.Empty

        If Master.GlobalScrapeMod.Trailer Then
            _ImdbTrailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.IMDBURL, "/title/tt", DBMovie.Movie.IMDBID, "/videogallery/content_type-Trailer"))
            If _ImdbTrailerPage.ToLower.Contains("page not found") Then
                _ImdbTrailerPage = String.Empty
            End If

            If Not String.IsNullOrEmpty(_ImdbTrailerPage) Then
                Link = Regex.Match(_ImdbTrailerPage, "of [0-9]{1,3}")

                If Link.Success Then
                    TrailerNumber = Convert.ToInt32(Link.Value.Substring(3))

                    If TrailerNumber > 0 Then
                        currPage = Convert.ToInt32(Math.Ceiling(TrailerNumber / 10))

                        For i As Integer = 1 To currPage
                            If Not i = 1 Then
                                _ImdbTrailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.IMDBURL, "/title/tt", DBMovie.Movie.IMDBID, "/videogallery/content_type-Trailer?page=", i))
                            End If

                            Links = Regex.Matches(_ImdbTrailerPage, "screenplay/(vi[0-9]+)/")
                            Dim linksCollection As String() = From m As Object In Links Select CType(m, Match).Value Distinct.ToArray()

                            Links = Regex.Matches(_ImdbTrailerPage, "imdb/(vi[0-9]+)/")
                            linksCollection = linksCollection.Concat(From m As Object In Links Select CType(m, Match).Value Distinct.ToArray()).ToArray

                            For Each value As String In linksCollection
                                If value.Contains("screenplay") Then
                                    trailerPage = WebPage.DownloadData(String.Concat("http://", Master.eSettings.IMDBURL, "/video/", value, "player"))
                                    trailerUrl = Web.HttpUtility.UrlDecode(Regex.Match(trailerPage, "http.+mp4").Value)
                                    If Not String.IsNullOrEmpty(trailerUrl) AndAlso WebPage.IsValidURL(trailerUrl) Then
                                        Dim tLink As String = String.Empty
                                        If Regex.IsMatch(trailerUrl, "http:\/\/.*youtube.*\/watch\?v=(.{11})&?.*") Then
                                            Dim YT As New YouTube.Scraper
                                            YT.GetVideoLinks(trailerUrl)
                                            If YT.VideoLinks.ContainsKey(Master.eSettings.PreferredTrailerQuality) Then
                                                tLink = YT.VideoLinks(Master.eSettings.PreferredTrailerQuality).URL
                                            Else
                                                Select Case Master.eSettings.PreferredTrailerQuality
                                                    Case Enums.TrailerQuality.HD1080p
                                                        If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720p) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.HD720p).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQFLV) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.HQFLV).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQMP4) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQMP4).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQFLV) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQFLV).URL
                                                        End If
                                                    Case Enums.TrailerQuality.HD1080pVP8
                                                        If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HD720pVP8) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.HD720pVP8).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQVP8) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.HQVP8).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQVP8) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQVP8).URL
                                                        End If
                                                    Case Enums.TrailerQuality.HD720p
                                                        If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQFLV) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.HQFLV).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQMP4) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQMP4).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQFLV) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQFLV).URL
                                                        End If
                                                    Case Enums.TrailerQuality.HD720pVP8
                                                        If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.HQVP8) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.HQVP8).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQVP8) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQVP8).URL
                                                        End If
                                                    Case Enums.TrailerQuality.HQFLV
                                                        If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQMP4) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQMP4).URL
                                                        ElseIf YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQFLV) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQFLV).URL
                                                        End If
                                                    Case Enums.TrailerQuality.HQVP8
                                                        If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQVP8) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQVP8).URL
                                                        End If
                                                    Case Enums.TrailerQuality.SQMP4
                                                        If YT.VideoLinks.ContainsKey(Enums.TrailerQuality.SQFLV) Then
                                                            tLink = YT.VideoLinks(Enums.TrailerQuality.SQFLV).URL
                                                        End If
                                                    Case Enums.TrailerQuality.SQFLV
                                                        tLink = String.Empty
                                                    Case Enums.TrailerQuality.SQVP8
                                                        tLink = String.Empty
                                                End Select
                                            End If
                                        Else
                                            tLink = trailerUrl
                                        End If
                                        URLList.Add(tLink)
                                    End If
                                End If
                            Next
                        Next
                    End If

                End If
            End If
        End If
        Return New Interfaces.ModuleResult With {.breakChain = False}
    End Function

    Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.EmberMovieScraperModule_Trailer.ScraperOrderChanged
        _setup.orderChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Structure _MySettings

#Region "Fields"
        Dim TrailerTimeout As Integer
#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class