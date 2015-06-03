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
    Public Sub RunCommandLine(ByVal Args() As String, ByVal isFirstInstance As Boolean)
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

        If isFirstInstance Then
            Master.fLoading.SetLoadingMesg(Master.eLang.GetString(858, "Loading database..."))
            Master.DB.ConnectMyVideosDB()
            Master.DB.LoadMovieSourcesFromDB()
            Master.DB.LoadTVSourcesFromDB()
            Master.DB.LoadExcludeDirsFromDB()
        End If

        For i As Integer = 0 To Args.Count - 1

            Select Case Args(i).ToLower
                Case "-addmoviesource"
                    If Args.Count - 1 > i Then
                        If Directory.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"addmoviesource", Args(i + 1).Replace("""", String.Empty)}))
                            i += 1
                        End If
                    Else
                        logger.Warn("No path or invalid path specified for -addmoviesource command")
                    End If
                Case "-addtvshowsource"
                    If Args.Count - 1 > i Then
                        If Directory.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"addtvshowsource", Args(i + 1).Replace("""", String.Empty)}))
                            i += 1
                        End If
                    Else
                        logger.Warn("No path or invalid path specified for -addtvshowsource command")
                    End If
                Case "-cleanvideodb"
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"cleanvideodb"}))
                Case "-fullask"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.FullAsk, CustomScrapeModifier}))
                Case "-fullauto"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.FullAuto, CustomScrapeModifier}))
                Case "-fullskip"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.FullSkip, CustomScrapeModifier}))
                Case "-missask"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.MissAsk, CustomScrapeModifier}))
                Case "-missauto"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.MissAuto, CustomScrapeModifier}))
                Case "-missskip"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.MissSkip, CustomScrapeModifier}))
                Case "-newask"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.NewAsk, CustomScrapeModifier}))
                Case "-newauto"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.NewAuto, CustomScrapeModifier}))
                Case "-newskip"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.NewSkip, CustomScrapeModifier}))
                Case "-markask"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.MarkAsk, CustomScrapeModifier}))
                Case "-markauto"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.MarkAuto, CustomScrapeModifier}))
                Case "-markskip"
                    Dim CustomScrapeModifier As New Structures.ScrapeModifier_Movie_MovieSet
                    i = SetScraperMod(Args, i, CustomScrapeModifier)
                    RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"scrapemovie", Enums.ScrapeType_Movie_MovieSet_TV.MarkSkip, CustomScrapeModifier}))
                Case "-file"
                    'If Args.Count - 1 > i Then
                    '    isSingle = False
                    '    hasSpec = True
                    '    clScrapeType = Enums.ScrapeType_Movie.SingleScrape
                    '    If File.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                    '        MoviePath = Args(i + 1).Replace("""", String.Empty)
                    '        i += 1
                    '    End If
                    'Else
                    '    Exit For
                    'End If
                Case "-folder"
                    'If Args.Count - 1 > i Then
                    '    isSingle = True
                    '    hasSpec = True
                    '    clScrapeType = Enums.ScrapeType_Movie.SingleScrape
                    '    If File.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                    '        MoviePath = Args(i + 1).Replace("""", String.Empty)
                    '        i += 1
                    '    End If
                    'Else
                    '    Exit For
                    'End If
                Case "-scanfolder"
                    If Args.Count - 1 > i Then
                        If Directory.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.SpecificFolder = True}, String.Empty, Args(i + 1).Replace("""", String.Empty)}))
                            i += 1
                        End If
                    Else
                        logger.Warn("No path or invalid path specified for -scanfolder command")
                    End If
                Case "-export"
                    If Args.Count - 1 > i Then
                        MoviePath = Args(i + 1).Replace("""", String.Empty)
                        clExport = True
                    Else
                        Exit For
                    End If
                Case "-template"
                    If Args.Count - 1 > i Then
                        clExportTemplate = Args(i + 1).Replace("""", String.Empty)
                    Else
                        Exit For
                    End If
                Case "-resize"
                    If Args.Count - 1 > i Then
                        clExportResizePoster = Convert.ToUInt16(Args(i + 1).Replace("""", String.Empty))
                    Else
                        Exit For
                    End If
                Case "--verbose"
                Case "-nowindow"
                    Master.fLoading.Hide()
                Case "-run"
                    If Args.Count - 1 > i Then
                        ModuleName = Args(i + 1).Replace("""", String.Empty)
                        RunModule = True
                    Else
                        Exit For
                    End If
                Case "-updatemovies"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        Dim clArg As String = Args(i + 1).Replace("""", String.Empty)
                        Dim SourceName As String = Master.MovieSources.FirstOrDefault(Function(f) f.Name.ToLower = clArg.ToLower).Name
                        If SourceName IsNot Nothing Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.Movies = True}, SourceName, String.Empty}))
                            i += 1
                        Else
                            SourceName = Master.MovieSources.FirstOrDefault(Function(f) f.Path.ToLower = clArg.ToLower).Name
                            If SourceName IsNot Nothing Then
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                        New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.Movies = True}, SourceName, String.Empty}))
                                i += 1
                            Else
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.Movies = True}, String.Empty, String.Empty}))
                            End If
                        End If
                    Else
                        RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.Movies = True}, String.Empty, String.Empty}))
                    End If
                Case "-updatetvshows"
                    If Args.Count - 1 > i AndAlso Not Args(i + 1).StartsWith("-") Then
                        Dim clArg As String = Args(i + 1).Replace("""", String.Empty)
                        Dim SourceName As String = Master.TVSources.FirstOrDefault(Function(f) f.Name.ToLower = clArg.ToLower).Name
                        If SourceName IsNot Nothing Then
                            RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.TV = True}, SourceName, String.Empty}))
                            i += 1
                        Else
                            SourceName = Master.TVSources.FirstOrDefault(Function(f) f.Path.ToLower = clArg.ToLower).Name
                            If SourceName IsNot Nothing Then
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                        New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.TV = True}, SourceName, String.Empty}))
                                i += 1
                            Else
                                RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.TV = True}, String.Empty, String.Empty}))
                            End If
                        End If
                    Else
                        RaiseEvent TaskEvent(Enums.ModuleEventType.CommandLine, _
                                                    New List(Of Object)(New Object() {"loadmedia", New Structures.Scans With {.TV = True}, String.Empty, String.Empty}))
                    End If
            End Select
        Next
    End Sub

    Private Function SetScraperMod(ByVal Args() As String, ByVal iStartPos As Integer, ByRef ScrapeModifier As Structures.ScrapeModifier_Movie_MovieSet) As Integer
        Dim iEndPos As Integer = iStartPos

        For i As Integer = iStartPos + 1 To Args.Count - 1
            Select Case Args(i).ToLower
                Case "-all"
                    ScrapeModifier.ActorThumbs = True
                    ScrapeModifier.Banner = True
                    ScrapeModifier.CharacterArt = True
                    ScrapeModifier.ClearArt = True
                    ScrapeModifier.ClearLogo = True
                    ScrapeModifier.DiscArt = True
                    ScrapeModifier.EFanarts = True
                    ScrapeModifier.EThumbs = True
                    ScrapeModifier.Fanart = True
                    ScrapeModifier.Landscape = True
                    ScrapeModifier.Meta = True
                    ScrapeModifier.NFO = True
                    ScrapeModifier.Poster = True
                    ScrapeModifier.Theme = True
                    ScrapeModifier.Trailer = True
                Case "-actorthumbs"
                    ScrapeModifier.ActorThumbs = True
                Case "-banner"
                    ScrapeModifier.Banner = True
                Case "-characterart"
                    ScrapeModifier.CharacterArt = True
                Case "-clearart"
                    ScrapeModifier.ClearArt = True
                Case "-clearlogo"
                    ScrapeModifier.ClearLogo = True
                Case "-discart"
                    ScrapeModifier.DiscArt = True
                Case "-efanarts"
                    ScrapeModifier.EFanarts = True
                Case "-ethumbs"
                    ScrapeModifier.EThumbs = True
                Case "-fanart"
                    ScrapeModifier.Fanart = True
                Case "-landscape"
                    ScrapeModifier.Landscape = True
                Case "-meta"
                    ScrapeModifier.Meta = True
                Case "-nfo"
                    ScrapeModifier.NFO = True
                Case "-poster"
                    ScrapeModifier.Poster = True
                Case "-theme"
                    ScrapeModifier.Theme = True
                Case "-trailer"
                    ScrapeModifier.Trailer = True
                Case Else
                    Return i - 1
            End Select
            iEndPos = i
        Next

        Return iEndPos
    End Function

#End Region 'Methods

End Class
