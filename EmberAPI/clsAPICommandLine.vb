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

    Public Event GenericEvent(ByVal mType As Enums.ModuleEventType, ByRef _params As List(Of Object))

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
        Dim hasSpec As Boolean = False
        Dim clScrapeType As Enums.ScrapeType = Enums.ScrapeType.None
        Dim clExport As Boolean = False
        Dim clExportResizePoster As Integer = 0
        Dim clExportTemplate As String = "template"
        Dim clAsk As Boolean = False
        Dim nowindow As Boolean = False
        Dim RunModule As Boolean = False
        Dim ModuleName As String = String.Empty
        Dim UpdateTVShows As Boolean = False
        Dim specFolder As String = String.Empty

        For i As Integer = 0 To Args.Count - 1

            Select Case Args(i).ToLower
                Case "-fullask"
                    clScrapeType = Enums.ScrapeType.FullAsk
                    clAsk = True
                Case "-fullauto"
                    clScrapeType = Enums.ScrapeType.FullAuto
                    clAsk = False
                Case "-fullskip"
                    clScrapeType = Enums.ScrapeType.FullSkip
                    clAsk = False
                Case "-missask"
                    clScrapeType = Enums.ScrapeType.MissAsk
                    clAsk = True
                Case "-missauto"
                    clScrapeType = Enums.ScrapeType.MissAuto
                    clAsk = False
                Case "-missskip"
                    clScrapeType = Enums.ScrapeType.MissSkip
                    clAsk = True
                Case "-newask"
                    clScrapeType = Enums.ScrapeType.NewAsk
                    clAsk = True
                Case "-newauto"
                    clScrapeType = Enums.ScrapeType.NewAuto
                    clAsk = False
                Case "-newskip"
                    clScrapeType = Enums.ScrapeType.NewSkip
                    clAsk = False
                Case "-markask"
                    clScrapeType = Enums.ScrapeType.MarkAsk
                    clAsk = True
                Case "-markauto"
                    clScrapeType = Enums.ScrapeType.MarkAuto
                    clAsk = False
                Case "-markskip"
                    clScrapeType = Enums.ScrapeType.MarkSkip
                    clAsk = True
                Case "-file"
                    If Args.Count - 1 > i Then
                        isSingle = False
                        hasSpec = True
                        clScrapeType = Enums.ScrapeType.SingleScrape
                        If File.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            MoviePath = Args(i + 1).Replace("""", String.Empty)
                            i += 1
                        End If
                    Else
                        Exit For
                    End If
                Case "-folder"
                    If Args.Count - 1 > i Then
                        isSingle = True
                        hasSpec = True
                        clScrapeType = Enums.ScrapeType.SingleScrape
                        If File.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            MoviePath = Args(i + 1).Replace("""", String.Empty)
                            i += 1
                        End If
                    Else
                        Exit For
                    End If
                Case "-folderscan"
                    If Args.Count - 1 > i Then
                        hasSpec = True
                        If Directory.Exists(Args(i + 1).Replace("""", String.Empty)) Then
                            specFolder = Args(i + 1).Replace("""", String.Empty)
                            i += 1
                        End If
                    Else
                        Exit For
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
                Case "-all"
                    Functions.SetScraperMod(Enums.ModType_Movie.All, True)
                Case "-banner"
                    Functions.SetScraperMod(Enums.ModType_Movie.Banner, True)
                Case "-clearart"
                    Functions.SetScraperMod(Enums.ModType_Movie.ClearArt, True)
                Case "-clearlogo"
                    Functions.SetScraperMod(Enums.ModType_Movie.ClearLogo, True)
                Case "-discart"
                    Functions.SetScraperMod(Enums.ModType_Movie.DiscArt, True)
                Case "-efanarts"
                    Functions.SetScraperMod(Enums.ModType_Movie.EFanarts, True)
                Case "-ethumbs"
                    Functions.SetScraperMod(Enums.ModType_Movie.EThumbs, True)
                Case "-fanart"
                    Functions.SetScraperMod(Enums.ModType_Movie.Fanart, True)
                Case "-landscape"
                    Functions.SetScraperMod(Enums.ModType_Movie.Landscape, True)
                Case "-nfo"
                    Functions.SetScraperMod(Enums.ModType_Movie.NFO, True)
                Case "-poster"
                    Functions.SetScraperMod(Enums.ModType_Movie.Poster, True)
                Case "-theme"
                    Functions.SetScraperMod(Enums.ModType_Movie.Theme, True)
                Case "-trailer"
                    Functions.SetScraperMod(Enums.ModType_Movie.Trailer, True)
                Case "--verbose"
                    clAsk = True
                Case "-nowindow"
                    nowindow = True
                Case "-run"
                    If Args.Count - 1 > i Then
                        ModuleName = Args(i + 1).Replace("""", String.Empty)
                        RunModule = True
                    Else
                        Exit For
                    End If
                Case "-tvupdate"
                    UpdateTVShows = True
                    'Case Else
                    'If File.Exists(Args(2).Replace("""", String.Empty)) Then
                    'MoviePath = Args(2).Replace("""", String.Empty)
                    'End If
            End Select
        Next

        If hasSpec Then
            If Not String.IsNullOrEmpty(specFolder) Then
                LoadMedia((New Structures.Scans With {.SpecificFolder = True}), String.Empty, specFolder)
            End If
        End If

    End Sub

    Public Sub LoadMedia(ByVal Scan As Structures.Scans, Optional ByVal SourceName As String = "", Optional ByVal Folder As String = "")
        RaiseEvent GenericEvent(Enums.ModuleEventType.CommandLine, New List(Of Object)(New Object() {"loadmedia", Scan, SourceName, Folder}))
    End Sub

    Public Sub DirectoryScan()

    End Sub

#End Region 'Methods

End Class
