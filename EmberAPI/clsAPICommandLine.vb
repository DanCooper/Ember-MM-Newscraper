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


Public Class CommandLine

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Events"

    Public Event TaskEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

    'Singleton Instace for CommandLine manager .. allways use this one
    Private Shared Singleton As CommandLine = Nothing

#End Region 'Events

#Region "Properties"

    Public Shared ReadOnly Property Instance() As CommandLine
        Get
            If (Singleton Is Nothing) Then
                Singleton = New CommandLine()
            End If
            Return Singleton
        End Get
    End Property

#End Region 'Properties

#Region "Methods"

    Public Sub RunCommandLine(ByVal Args() As String)
        If Args.Count = 0 Then Return

        logger.Trace("Call CommandLine")

        Dim MoviePath As String = String.Empty
        Dim isSingle As Boolean = False
        Dim clExport As Boolean = False
        Dim clExportResizePoster As Integer = 0
        Dim clExportTemplate As String = "template"
        Dim nowindow As Boolean = False
        Dim RunModule As Boolean = False
        Dim ModuleName As String = String.Empty

        For i As Integer = 0 To Args.Count - 1

            Select Case Args(i).ToLower
                Case "-addmoviesource"
                    If Args.Count - 1 > i Then
                        If Directory.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"addmoviesource", Args(i + 1).Replace("""", String.Empty)}))
                            i += 1
                        End If
                    Else
                        logger.Warn("[CommandLine] No path or invalid path specified for -addmoviesource command")
                    End If
                Case "-addtvshowsource"
                    If Args.Count - 1 > i Then
                        If Directory.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"addtvshowsource", Args(i + 1).Replace("""", String.Empty)}))
                            i += 1
                        End If
                    Else
                        logger.Warn("[CommandLine] No path or invalid path specified for -addtvshowsource command")
                    End If
                Case "-cleanvideodb"
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"cleanvideodb"}))
                Case "-run"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        Dim strModuleName As String = Args(i + 1)
                        i += 1
                        Dim sParams As List(Of Object) = Nothing
                        i = SetModuleParameters(Args, i, sParams)
                        RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"run", strModuleName, sParams}))
                    Else
                        logger.Warn("[CommandLine] Missing module name for command ""-run""")
                    End If
                Case "-scanfolder"
                    If Args.Count - 1 > i Then
                        If Directory.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.SpecificFolder = True}, -1, Args(i + 1).Replace("""", String.Empty)}))
                            i += 1
                        End If
                    Else
                        logger.Warn("[CommandLine] No path or invalid path specified for command ""-scanfolder""")
                    End If
                Case "-scrapemovies"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        i += 1
                        Dim ScrapeType As String = Args(i)
                        Select Case ScrapeType
                            Case "allask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.AllAsk, CustomScrapeModifiers}))
                            Case "allauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.AllAuto, CustomScrapeModifiers}))
                            Case "allskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.AllSkip, CustomScrapeModifiers}))
                            Case "markedask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MarkedAsk, CustomScrapeModifiers}))
                            Case "markedauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MarkedAuto, CustomScrapeModifiers}))
                            Case "markedskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MarkedSkip, CustomScrapeModifiers}))
                            Case "missingask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MissingAsk, CustomScrapeModifiers}))
                            Case "missingauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MissingAuto, CustomScrapeModifiers}))
                            Case "missingskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MissingSkip, CustomScrapeModifiers}))
                            Case "newask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.NewAsk, CustomScrapeModifiers}))
                            Case "newauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.NewAuto, CustomScrapeModifiers}))
                            Case "newskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.NewSkip, CustomScrapeModifiers}))
                            Case Else
                                logger.Warn("[CommandLine] Invalid ScrapeType specified for command ""-scrapemovies""")
                        End Select
                    Else
                        logger.Warn("[CommandLine] No ScrapeType specified for command ""-scrapemovies""")
                    End If
                Case "-scrapemoviesets"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        i += 1
                        Dim ScrapeType As String = Args(i)
                        Select Case ScrapeType
                            Case "allask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.AllAsk, CustomScrapeModifiers}))
                            Case "allauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.AllAuto, CustomScrapeModifiers}))
                            Case "allskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.AllSkip, CustomScrapeModifiers}))
                            Case "markedask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.MarkedAsk, CustomScrapeModifiers}))
                            Case "markedauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.MarkedAuto, CustomScrapeModifiers}))
                            Case "markedskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.MarkedSkip, CustomScrapeModifiers}))
                            Case "missingask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.MissingAsk, CustomScrapeModifiers}))
                            Case "missingauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.MissingAuto, CustomScrapeModifiers}))
                            Case "missingskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.MissingSkip, CustomScrapeModifiers}))
                            Case "newask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.NewAsk, CustomScrapeModifiers}))
                            Case "newauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.NewAuto, CustomScrapeModifiers}))
                            Case "newskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemoviesets", Enums.ScrapeType.NewSkip, CustomScrapeModifiers}))
                            Case Else
                                logger.Warn("[CommandLine] Invalid ScrapeType specified for command ""-scrapemoviesets""")
                        End Select
                    Else
                        logger.Warn("[CommandLine] No ScrapeType specified for command ""-scrapemoviesets""")
                    End If
                Case "-scrapetvshows"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        i += 1
                        Dim ScrapeType As String = Args(i)
                        Select Case ScrapeType
                            Case "allask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.AllAsk, CustomScrapeModifiers}))
                            Case "allauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.AllAuto, CustomScrapeModifiers}))
                            Case "allskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.AllSkip, CustomScrapeModifiers}))
                            Case "markedask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MarkedAsk, CustomScrapeModifiers}))
                            Case "markedauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MarkedAuto, CustomScrapeModifiers}))
                            Case "markedskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MarkedSkip, CustomScrapeModifiers}))
                            Case "missingask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MissingAsk, CustomScrapeModifiers}))
                            Case "missingauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MissingAuto, CustomScrapeModifiers}))
                            Case "missingskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MissingSkip, CustomScrapeModifiers}))
                            Case "newask"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.NewAsk, CustomScrapeModifiers}))
                            Case "newauto"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.NewAuto, CustomScrapeModifiers}))
                            Case "newskip"
                                Dim CustomScrapeModifiers As New Structures.ScrapeModifiers
                                i = SetScraperMod(Args, i, CustomScrapeModifiers)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.NewSkip, CustomScrapeModifiers}))
                            Case Else
                                logger.Warn("[CommandLine] Invalid ScrapeType specified for command ""-scrapemovies""")
                        End Select
                    Else
                        logger.Warn("[CommandLine] No ScrapeType specified for command ""-scrapemovies""")
                    End If
                Case "--verbose"
                Case "-nowindow"
                    Master.fLoading.Hide()
                Case "-updatemovies"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        Dim clArg As String = Args(i + 1).Replace("""", String.Empty)
                        Dim sSource As Database.DBSource = Master.DB.LoadAll_Sources_Movie.FirstOrDefault(Function(f) f.Name.ToLower = clArg.ToLower)
                        If sSource IsNot Nothing Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.Movies = True}, sSource.ID, String.Empty}))
                            i += 1
                        Else
                            sSource = Master.DB.LoadAll_Sources_Movie.FirstOrDefault(Function(f) f.Path.ToLower = clArg.ToLower)
                            If sSource IsNot Nothing Then
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                        New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.Movies = True}, sSource.ID, String.Empty}))
                                i += 1
                            Else
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.Movies = True}, -1, String.Empty}))
                            End If
                        End If
                    Else
                        RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.Movies = True}, -1, String.Empty}))
                    End If
                Case "-updatetvshows"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        Dim clArg As String = Args(i + 1).Replace("""", String.Empty)
                        Dim sSource As Database.DBSource = Master.DB.LoadAll_Sources_TVShow.FirstOrDefault(Function(f) f.Name.ToLower = clArg.ToLower)
                        If sSource IsNot Nothing Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.TV = True}, sSource.ID, String.Empty}))
                            i += 1
                        Else
                            sSource = Master.DB.LoadAll_Sources_TVShow.FirstOrDefault(Function(f) f.Path.ToLower = clArg.ToLower)
                            If sSource IsNot Nothing Then
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                        New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.TV = True}, sSource.ID, String.Empty}))
                                i += 1
                            Else
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.TV = True}, -1, String.Empty}))
                            End If
                        End If
                    Else
                        RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.TV = True}, -1, String.Empty}))
                    End If
                Case "-profile"
                    'has been handled in ApplicationEvents.vb
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        'skip profile name
                        i += 1
                    End If
                Case Else
                    logger.Warn(String.Concat("[CommandLine] Invalid command: ", Args(i)))
            End Select
        Next
    End Sub

    Private Function SetModuleParameters(ByVal Args() As String, ByVal iStartPos As Integer, ByRef Parameters As List(Of Object)) As Integer
        Dim iEndPos As Integer = iStartPos

        For i As Integer = iStartPos + 1 To Args.Count - 1
            If Not Args(i).StartsWith("-") Then
                If Parameters Is Nothing Then Parameters = New List(Of Object)
                Parameters.Add(Args(i))
            Else
                Return i - 1
            End If
            iEndPos = i
        Next

        Return iEndPos
    End Function

    Private Function SetScraperMod(ByVal Args() As String, ByVal iStartPos As Integer, ByRef ScrapeModifiers As Structures.ScrapeModifiers) As Integer
        Dim iEndPos As Integer = iStartPos

        For i As Integer = iStartPos + 1 To Args.Count - 1
            Select Case Args(i).ToLower
                Case "all"
                    ScrapeModifiers.AllSeasonsBanner = True
                    ScrapeModifiers.AllSeasonsFanart = True
                    ScrapeModifiers.AllSeasonsLandscape = True
                    ScrapeModifiers.AllSeasonsPoster = True
                    ScrapeModifiers.EpisodeActorThumbs = True
                    ScrapeModifiers.EpisodeFanart = True
                    ScrapeModifiers.EpisodeMeta = True
                    ScrapeModifiers.EpisodeNFO = True
                    ScrapeModifiers.EpisodePoster = True
                    ScrapeModifiers.MainActorthumbs = True
                    ScrapeModifiers.MainBanner = True
                    ScrapeModifiers.MainCharacterArt = True
                    ScrapeModifiers.MainClearArt = True
                    ScrapeModifiers.MainClearLogo = True
                    ScrapeModifiers.MainDiscArt = True
                    ScrapeModifiers.MainExtrafanarts = True
                    ScrapeModifiers.MainExtrathumbs = True
                    ScrapeModifiers.MainFanart = True
                    ScrapeModifiers.MainLandscape = True
                    ScrapeModifiers.MainMeta = True
                    ScrapeModifiers.MainNFO = True
                    ScrapeModifiers.MainPoster = True
                    ScrapeModifiers.MainSubtitles = True
                    ScrapeModifiers.MainTheme = True
                    ScrapeModifiers.MainTrailer = True
                    ScrapeModifiers.SeasonBanner = True
                    ScrapeModifiers.SeasonFanart = True
                    ScrapeModifiers.SeasonLandscape = True
                    ScrapeModifiers.SeasonNFO = True
                    ScrapeModifiers.SeasonPoster = True
                    ScrapeModifiers.withEpisodes = True
                    ScrapeModifiers.withSeasons = True
                Case "episodeall"
                    ScrapeModifiers.EpisodeActorThumbs = True
                    ScrapeModifiers.EpisodeFanart = True
                    ScrapeModifiers.EpisodeMeta = True
                    ScrapeModifiers.EpisodeNFO = True
                    ScrapeModifiers.EpisodePoster = True
                    ScrapeModifiers.withEpisodes = True
                Case "episodeactorthumbs"
                    ScrapeModifiers.EpisodeActorThumbs = True
                    ScrapeModifiers.withEpisodes = True
                Case "episodefanart"
                    ScrapeModifiers.EpisodeFanart = True
                    ScrapeModifiers.withEpisodes = True
                Case "episodemeta"
                    ScrapeModifiers.EpisodeMeta = True
                    ScrapeModifiers.withEpisodes = True
                Case "episodenfo"
                    ScrapeModifiers.EpisodeNFO = True
                    ScrapeModifiers.withEpisodes = True
                Case "episodeposter"
                    ScrapeModifiers.EpisodePoster = True
                    ScrapeModifiers.withEpisodes = True
                Case "actorthumbs"
                    ScrapeModifiers.MainActorthumbs = True
                Case "banner"
                    ScrapeModifiers.MainBanner = True
                Case "characterart"
                    ScrapeModifiers.MainCharacterArt = True
                Case "clearart"
                    ScrapeModifiers.MainClearArt = True
                Case "clearlogo"
                    ScrapeModifiers.MainClearLogo = True
                Case "discart"
                    ScrapeModifiers.MainDiscArt = True
                Case "extrafanarts"
                    ScrapeModifiers.MainExtrafanarts = True
                Case "extrathumbs"
                    ScrapeModifiers.MainExtrathumbs = True
                Case "fanart"
                    ScrapeModifiers.MainFanart = True
                Case "landscape"
                    ScrapeModifiers.MainLandscape = True
                Case "meta"
                    ScrapeModifiers.MainMeta = True
                Case "nfo"
                    ScrapeModifiers.MainNFO = True
                Case "poster"
                    ScrapeModifiers.MainPoster = True
                Case "subtitles"
                    ScrapeModifiers.MainSubtitles = True
                Case "theme"
                    ScrapeModifiers.MainTheme = True
                Case "trailer"
                    ScrapeModifiers.MainTrailer = True
                Case "seasonall"
                    ScrapeModifiers.SeasonBanner = True
                    ScrapeModifiers.SeasonFanart = True
                    ScrapeModifiers.SeasonLandscape = True
                    ScrapeModifiers.SeasonNFO = True
                    ScrapeModifiers.SeasonPoster = True
                    ScrapeModifiers.withSeasons = True
                Case "seasonbanner"
                    ScrapeModifiers.SeasonBanner = True
                    ScrapeModifiers.withSeasons = True
                Case "seasonfanart"
                    ScrapeModifiers.SeasonFanart = True
                    ScrapeModifiers.withSeasons = True
                Case "seasonlandscape"
                    ScrapeModifiers.SeasonLandscape = True
                    ScrapeModifiers.withSeasons = True
                Case "seasonnfo"
                    ScrapeModifiers.SeasonNFO = True
                    ScrapeModifiers.withSeasons = True
                Case "seasonposter"
                    ScrapeModifiers.SeasonPoster = True
                    ScrapeModifiers.withSeasons = True
                Case Else
                    Return i - 1
            End Select
            iEndPos = i
        Next

        Return iEndPos
    End Function

#End Region 'Methods

End Class
