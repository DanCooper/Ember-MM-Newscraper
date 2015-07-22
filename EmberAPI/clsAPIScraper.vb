'################################################################################
'#                             EMBER MEDIA MANAGER                              #
'################################################################################
'################################################################################
'# This file is part of Ember Media Manager.                                    #
'#                                                                              #
'# Ember Media Manager is free software: you can redistribute it and/or modify  #
'# it under the terms of the GNU General Public License as published by         #
'# the Free Software Foundation, either version 3 of the License, or            #
'# (at your option) any later version.                                          #
'#                                                                              #
'# Ember Media Manager is distributed in the hope that it will be useful,       #
'# but WITHOUT ANY WARRANTY; without even the implied warranty of               #
'# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
'# GNU General Public License for more details.                                 #
'#                                                                              #
'# You should have received a copy of the GNU General Public License            #
'# along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
'################################################################################

Imports NLog
Imports System.Windows.Forms

Public Class Scraper

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Friend WithEvents bwScraper As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

    Public Event ScraperUpdated(ByVal iType As Integer, ByVal sText As String)

    Public Event ScraperCompleted()

#End Region 'Events

#Region "Methods"

    Public Sub Cancel()
        If Me.bwScraper.IsBusy Then Me.bwScraper.CancelAsync()
    End Sub

    Public Sub CancelAndWait()
        If bwScraper.IsBusy Then bwScraper.CancelAsync()
        While bwScraper.IsBusy
            Application.DoEvents()
            Threading.Thread.Sleep(50)
        End While
    End Sub

    Public Function IsBusy() As Boolean
        Return bwScraper.IsBusy
    End Function

    Public Sub Start(ByVal ScrapeList As List(Of DataRow), ByVal ScrapeMod_TV As Structures.ScrapeModifier_TV, ByVal ScrapeOptions_TV As Structures.ScrapeOptions_TV, ByVal ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV)
        Me.bwScraper = New System.ComponentModel.BackgroundWorker
        Me.bwScraper.WorkerReportsProgress = True
        Me.bwScraper.WorkerSupportsCancellation = True
        Me.bwScraper.RunWorkerAsync(New Arguments With {.ScrapeList = ScrapeList, .ScrapeMod_TV = ScrapeMod_TV, .ScrapeOptions_TV = ScrapeOptions_TV, .ScrapeType = ScrapeType})
    End Sub

    Private Sub bwScraper_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwScraper.DoWork
        Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        Dim Cancelled As Boolean = False
        Dim DBScrapeShow As New Structures.DBTV
        Dim dScrapeRow As DataRow

        logger.Trace("Starting TV Show scrape")

        For Each dRow As DataRow In Args.ScrapeList
            Dim aContainer As New MediaContainers.SearchResultsContainer_TV
            Dim OldListTitle As String = String.Empty

            Cancelled = False

            OldListTitle = dRow.Item("ListTitle").ToString
            
            dScrapeRow = dRow

            logger.Trace(String.Concat("Start scraping: ", OldListTitle))

            If Args.ScrapeMod_TV.withEpisodes Then
                DBScrapeShow = Master.DB.LoadTVFullShowFromDB(Convert.ToInt64(dRow.Item("idShow")))
            Else
                DBScrapeShow = Master.DB.LoadTVShowFromDB(Convert.ToInt64(dRow.Item("idShow")), False, True)
            End If

            If Args.ScrapeMod_TV.ShowNFO Then
                If ModulesManager.Instance.ScrapeData_TVShow(DBScrapeShow, Args.ScrapeType, Args.ScrapeOptions_TV, Args.ScrapeList.Count = 1, Args.ScrapeMod_TV.withEpisodes) Then
                    Cancelled = True
                End If
            Else
                ' if we do not have the tvshow ID we need to retrive it even if is just a Poster/Fanart/Trailer/Actors update
                If String.IsNullOrEmpty(DBScrapeShow.TVShow.TVDB) AndAlso (Master.GlobalScrapeMod.ActorThumbs Or Master.GlobalScrapeMod.Banner Or Master.GlobalScrapeMod.CharacterArt Or _
                                                                         Master.GlobalScrapeMod.ClearArt Or Master.GlobalScrapeMod.ClearLogo Or Master.GlobalScrapeMod.EFanarts Or _
                                                                         Master.GlobalScrapeMod.Fanart Or Master.GlobalScrapeMod.Landscape Or Master.GlobalScrapeMod.Poster Or _
                                                                         Master.GlobalScrapeMod.Theme) Then
                    Dim tOpt As New Structures.ScrapeOptions_TV 'all false value not to override any field
                    If ModulesManager.Instance.ScrapeData_TVShow(DBScrapeShow, Args.ScrapeType, tOpt, Args.ScrapeList.Count = 1, False) Then
                        Exit For
                    End If
                End If
            End If

            If bwScraper.CancellationPending Then Exit For

            If Not Cancelled Then

                'get all images
                If Not ModulesManager.Instance.ScrapeImage_TV(DBScrapeShow, Enums.ScraperCapabilities_TV.All, aContainer, Args.ScrapeList.Count = 1) Then

                End If
            End If
        Next

        logger.Trace("Ended TV Show scrape")
    End Sub

    Private Sub bwPrelim_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwScraper.ProgressChanged
        Dim tProgressValue As ProgressValue = DirectCast(e.UserState, ProgressValue)
        RaiseEvent ScraperUpdated(e.ProgressPercentage, tProgressValue.Message)

        Select Case tProgressValue.Type
            Case 0
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"newmovie", 3, Master.eLang.GetString(817, "New Movie Added"), tProgressValue.Message, Nothing}))
            Case 1
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.Notification, New List(Of Object)(New Object() {"newep", 4, Master.eLang.GetString(818, "New Episode Added"), tProgressValue.Message, Nothing}))
        End Select
    End Sub

    Private Sub bwPrelim_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwScraper.RunWorkerCompleted
        If Not e.Cancelled Then
            Dim Args As Arguments = DirectCast(e.Result, Arguments)
            'If Args.Scan.Movies Then
            '    Dim params As New List(Of Object)(New Object() {False, False, False, True, Args.SourceName})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterUpdateDB_Movie, params, Nothing)
            'End If
            'If Args.Scan.TV Then
            '    Dim params As New List(Of Object)(New Object() {False, False, False, True, Args.SourceName})
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.AfterUpdateDB_TV, params, Nothing)
            'End If
            RaiseEvent ScraperCompleted()
        End If
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Structure Arguments

#Region "Fields"

        Dim ScrapeOptions_TV As Structures.ScrapeOptions_TV
        Dim ScrapeList As List(Of DataRow)
        Dim ScrapeMod_TV As Structures.ScrapeModifier_TV
        Dim ScrapeType As Enums.ScrapeType_Movie_MovieSet_TV

#End Region 'Fields

    End Structure

    Private Structure ProgressValue

#Region "Fields"

        Dim Message As String
        Dim Type As Integer

#End Region 'Fields

    End Structure

#End Region 'Nested Types

End Class 'Scraper
