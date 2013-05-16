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
' # HD Movie Logos -> logo.png (choose this first)
' # ClearLOGOs -> logo.png (use this as a backup if no HD Logo in the lanaguage specified)
' # ClearART -> clearart.png (use this as a backup if no HD ClearArt, in the language specified)
' # HDClearART -> clearart.png (choose this first)
' # cdART -> disc.png
' # Movie Backgrounds -> Fanart (this is the only fanart.tv artwork that might overlap with 'typical' artwork scraping from IMDB/TMDB)
' # Movie Banner -> Banner (not poster - Frodo supports both now, <moviename>-poster.jpg/png and <moviename>-banner.jpg/png or poster.jpg/png and banner.jpg/png)
' # Movie Thumbs -> landscape.png
' # Special note - the Logos and ClearArts are language-specific and should be tagged with the appropriate language. Will want to have a setting allowing users to specify a language so as not to get a bunch of foreign-language artwork.
' # 1) Logo.png - to be added at a later stage, today is not possible to save
' # 2) Clearart.png - to be added at a later stage, today is not possible to save
' # 3) Disc.png - to be added at a later stage, today is not possible to save
' # 4) Landscape.png - to be added at a later stage, today is not possible to save
' # language is in image properties

Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports FanartTVAPI

Namespace FANARTTVs

	Public Class Scraper

#Region "Fields"

        Private _MySettings As FanartTV_Poster.sMySettings
        Private _FanartTV As FanartTV.V1.FanartTV
        Friend WithEvents bwFANARTTV As New System.ComponentModel.BackgroundWorker
        Private _APIInvalid As Boolean = False
#End Region 'Fields

        '#Region "Events"

        '		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

        '		Public Event ProgressUpdated(ByVal iPercent As Integer)

        '#End Region	'Events

#Region "Methods"

        Public Sub New(ByRef tMySettings As FanartTV_Poster.sMySettings)
            _MySettings = tMySettings
            _FanartTV = New FanartTV.V1.FanartTV(_MySettings.FANARTTVApiKey)
            Dim Result As FanartTV.V1.FanartTVMovie = _FanartTV.GetMovieInfo(New FanartTV.V1.FanartTVRequest("1", "JSON", "all", 1, 1))
            If IsNothing(Result) Then
                If Not IsNothing(_FanartTV.Error) Then
                    'Master.eLog.WriteToErrorLog(_FanartTV.Error, "", "Error")
                    _APIInvalid = True
                End If
            End If
        End Sub

        'Public Sub Cancel()
        '	If Me.bwFANARTTV.IsBusy Then Me.bwFANARTTV.CancelAsync()

        '	While Me.bwFANARTTV.IsBusy
        '		Application.DoEvents()
        '		Threading.Thread.Sleep(50)
        '	End While
        'End Sub

        'Public Sub GetImagesAsync(ByVal sURL As String)
        '	Try
        '		If Not bwFANARTTV.IsBusy Then
        '			bwFANARTTV.WorkerSupportsCancellation = True
        '			bwFANARTTV.WorkerReportsProgress = True
        '			bwFANARTTV.RunWorkerAsync(New Arguments With {.Parameter = sURL})
        '		End If
        '	Catch ex As Exception
        '		Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        '	End Try
        'End Sub

        Public Function GetFANARTTVImages(ByVal imdbID As String) As List(Of MediaContainers.Image)
            Dim alPoster As New List(Of MediaContainers.Image)
            Dim ParentID As String

            If _APIInvalid Then
                Return Nothing
            End If
            Try
                Dim Result As FanartTV.V1.FanartTVMovie = _FanartTV.GetMovieInfo(New FanartTV.V1.FanartTVRequest(imdbID, "JSON", "all", 1, 2))
                If bwFANARTTV.CancellationPending Then Return Nothing
                If IsNothing(Result) Then Return alPoster
                If IsNothing(Result.movieinfo.moviebackground) Then Return alPoster
                For Each image In Result.movieinfo.moviebackground
                    ParentID = image.url.Substring(image.url.LastIndexOf("/") + 1, image.url.Length - (image.url.LastIndexOf("/") + 1))
                    alPoster.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.url, .Width = "1920", .Height = "1080", .ParentID = image.url})
                    alPoster.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.url & "/preview", .Width = "200", .Height = "112", .ParentID = image.url})
                Next
            Catch ex As Exception
                Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            End Try

            Return alPoster
        End Function

        'Private Sub bwFANARTTVA_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwFANARTTV.DoWork
        '	Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        '	Try
        '		e.Result = GetFANARTTVImages(Args.Parameter)
        '	Catch ex As Exception
        '		Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
        '		e.Result = Nothing
        '	End Try
        'End Sub

        'Private Sub bwFANARTTV_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwFANARTTV.ProgressChanged
        '	If Not bwFANARTTV.CancellationPending Then
        '		RaiseEvent ProgressUpdated(e.ProgressPercentage)
        '	End If
        'End Sub

        'Private Sub bwFANARTTV_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwFANARTTV.RunWorkerCompleted
        '	If Not IsNothing(e.Result) Then
        '		RaiseEvent PostersDownloaded(DirectCast(e.Result, List(Of MediaContainers.Image)))
        '	End If
        'End Sub


#End Region 'Methods

#Region "Nested Types"

		Private Structure Arguments

#Region "Fields"

			Dim Parameter As String
			Dim sType As String

#End Region	'Fields

		End Structure

		Private Structure Results

#Region "Fields"

			Dim Result As Object
			Dim ResultList As List(Of MediaContainers.Image)

#End Region	'Fields

		End Structure

#End Region	'Nested Types

	End Class

End Namespace

