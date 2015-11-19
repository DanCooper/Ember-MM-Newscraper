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

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

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
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.AllAsk, CustomScrapeModifier}))
                            Case "allauto"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.AllAuto, CustomScrapeModifier}))
                            Case "allskip"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.AllSkip, CustomScrapeModifier}))
                            Case "markedask"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MarkedAsk, CustomScrapeModifier}))
                            Case "markedauto"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MarkedAuto, CustomScrapeModifier}))
                            Case "markedskip"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MarkedSkip, CustomScrapeModifier}))
                            Case "missingask"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MissingAsk, CustomScrapeModifier}))
                            Case "missingauto"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MissingAuto, CustomScrapeModifier}))
                            Case "missingskip"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.MissingSkip, CustomScrapeModifier}))
                            Case "newask"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.NewAsk, CustomScrapeModifier}))
                            Case "newauto"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.NewAuto, CustomScrapeModifier}))
                            Case "newskip"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovies", Enums.ScrapeType.NewSkip, CustomScrapeModifier}))
                            Case Else
                                logger.Warn("[CommandLine] Invalid ScrapeType specified for command ""-scrapemovies""")
                        End Select
                    Else
                        logger.Warn("[CommandLine] No ScrapeType specified for command ""-scrapemovies""")
                    End If
                Case "-scrapetvshows"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        i += 1
                        Dim ScrapeType As String = Args(i)
                        Select Case ScrapeType
                            Case "allask"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.AllAsk, CustomScrapeModifier}))
                            Case "allauto"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.AllAuto, CustomScrapeModifier}))
                            Case "allskip"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.AllSkip, CustomScrapeModifier}))
                            Case "markedask"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MarkedAsk, CustomScrapeModifier}))
                            Case "markedauto"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MarkedAuto, CustomScrapeModifier}))
                            Case "markedskip"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MarkedSkip, CustomScrapeModifier}))
                            Case "missingask"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MissingAsk, CustomScrapeModifier}))
                            Case "missingauto"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MissingAuto, CustomScrapeModifier}))
                            Case "missingskip"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.MissingSkip, CustomScrapeModifier}))
                            Case "newask"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.NewAsk, CustomScrapeModifier}))
                            Case "newauto"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.NewAuto, CustomScrapeModifier}))
                            Case "newskip"
                                Dim CustomScrapeModifier As New Structures.ScrapeModifier
                                i = SetScraperMod(Args, i, CustomScrapeModifier)
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapetvshows", Enums.ScrapeType.NewSkip, CustomScrapeModifier}))
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
                        Dim sSource As Database.DBSource = Master.MovieSources.FirstOrDefault(Function(f) f.Name.ToLower = clArg.ToLower)
                        If sSource IsNot Nothing Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.Movies = True}, sSource.ID, String.Empty}))
                            i += 1
                        Else
                            sSource = Master.MovieSources.FirstOrDefault(Function(f) f.Path.ToLower = clArg.ToLower)
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
                        Dim sSource As Database.DBSource = Master.TVShowSources.FirstOrDefault(Function(f) f.Name.ToLower = clArg.ToLower)
                        If sSource IsNot Nothing Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine,
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.ScanOrClean With {.TV = True}, sSource.ID, String.Empty}))
                            i += 1
                        Else
                            sSource = Master.TVShowSources.FirstOrDefault(Function(f) f.Path.ToLower = clArg.ToLower)
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

    Private Function SetScraperMod(ByVal Args() As String, ByVal iStartPos As Integer, ByRef ScrapeModifier As Structures.ScrapeModifier) As Integer
        Dim iEndPos As Integer = iStartPos

        For i As Integer = iStartPos + 1 To Args.Count - 1
            Select Case Args(i).ToLower
                Case "all"
                    ScrapeModifier.MainActorthumbs = True
                    ScrapeModifier.MainBanner = True
                    ScrapeModifier.MainCharacterArt = True
                    ScrapeModifier.MainClearArt = True
                    ScrapeModifier.MainClearLogo = True
                    ScrapeModifier.MainDiscArt = True
                    ScrapeModifier.MainExtrafanarts = True
                    ScrapeModifier.MainExtrathumbs = True
                    ScrapeModifier.MainFanart = True
                    ScrapeModifier.MainLandscape = True
                    ScrapeModifier.MainMeta = True
                    ScrapeModifier.MainNFO = True
                    ScrapeModifier.MainPoster = True
                    ScrapeModifier.MainSubtitles = True
                    ScrapeModifier.MainTheme = True
                    ScrapeModifier.MainTrailer = True
                Case "actorthumbs"
                    ScrapeModifier.MainActorthumbs = True
                Case "banner"
                    ScrapeModifier.MainBanner = True
                Case "characterart"
                    ScrapeModifier.MainCharacterArt = True
                Case "clearart"
                    ScrapeModifier.MainClearArt = True
                Case "clearlogo"
                    ScrapeModifier.MainClearLogo = True
                Case "discart"
                    ScrapeModifier.MainDiscArt = True
                Case "extrafanarts"
                    ScrapeModifier.MainExtrafanarts = True
                Case "extrathumbs"
                    ScrapeModifier.MainExtrathumbs = True
                Case "fanart"
                    ScrapeModifier.MainFanart = True
                Case "landscape"
                    ScrapeModifier.MainLandscape = True
                Case "meta"
                    ScrapeModifier.MainMeta = True
                Case "nfo"
                    ScrapeModifier.MainNFO = True
                Case "poster"
                    ScrapeModifier.MainPoster = True
                Case "subtitles"
                    ScrapeModifier.MainSubtitles = True
                Case "theme"
                    ScrapeModifier.MainTheme = True
                Case "trailer"
                    ScrapeModifier.MainTrailer = True
                Case Else
                    Return i - 1
            End Select
            iEndPos = i
        Next

        Return iEndPos
    End Function

#End Region 'Methods

End Class
