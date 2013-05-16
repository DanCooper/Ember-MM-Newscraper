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

Imports EmberAPI
Imports RestSharp
Imports WatTmdb


Namespace TMDB

	Public Class Scraper

#Region "Fields"

		Private _TMDBConf As V3.TmdbConfiguration
		Private _TMDBConfE As V3.TmdbConfiguration
		Private _TMDBApi As V3.Tmdb
		Private _TMDBApiE As V3.Tmdb
		Private _MySettings As EmberTMDBScraperModule.sMySettings

		Private backdrop_names(3) As v3Size
		Private poster_names(5) As v3Size


		Friend WithEvents bwTMDB As New System.ComponentModel.BackgroundWorker

#End Region	'Fields

#Region "Events"

		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

		Public Event ProgressUpdated(ByVal iPercent As Integer)

#End Region	'Events

#Region "Methods"

		Public Sub New(ByRef tTMDBConf As V3.TmdbConfiguration, ByRef tTMDBConfE As V3.TmdbConfiguration, ByRef tTMDBApi As V3.Tmdb, ByRef tTMDBApiE As V3.Tmdb, ByRef tMySettings As EmberTMDBScraperModule.sMySettings)
			_TMDBConf = tTMDBConf
			_TMDBConfE = tTMDBConfE
			_TMDBApi = tTMDBApi
			_TMDBApiE = tTMDBApiE
			_MySettings = tMySettings
			' v3 does not have description anymore
			poster_names(0).description = "thumb"
			poster_names(0).size = "w92"
			poster_names(0).width = 92
			poster_names(1).description = "w154"
			poster_names(1).size = "w154"
			poster_names(1).width = 154
			poster_names(2).description = "cover"
			poster_names(2).size = "w185"
			poster_names(2).width = 185
			poster_names(3).description = "w342"
			poster_names(3).size = "w342"
			poster_names(3).width = 342
			poster_names(4).description = "mid"
			poster_names(4).size = "w500"
			poster_names(4).width = 500
			poster_names(5).description = "original"
			poster_names(5).size = "original"
			poster_names(5).width = 0
			backdrop_names(0).description = "thumb"
			backdrop_names(0).size = "w300"
			backdrop_names(0).width = 300
			backdrop_names(1).description = "poster"
			backdrop_names(1).size = "w780"
			backdrop_names(1).width = 780
			backdrop_names(2).description = "w1280"
			backdrop_names(2).size = "w1280"
			backdrop_names(2).width = 1280
			backdrop_names(3).description = "original"
			backdrop_names(3).size = "original"
			backdrop_names(3).width = 0
		End Sub

		Public Sub Cancel()
			If bwTMDB.IsBusy Then bwTMDB.CancelAsync()

			While bwTMDB.IsBusy
				Application.DoEvents()
				Threading.Thread.Sleep(50)
			End While
		End Sub

		Public Sub GetImagesAsync(ByVal imdbID As String, ByVal sType As String)
			Try
				If Not bwTMDB.IsBusy Then
					bwTMDB.WorkerSupportsCancellation = True
					bwTMDB.WorkerReportsProgress = True
					bwTMDB.RunWorkerAsync(New Arguments With {.Parameter = imdbID, .sType = sType})
				End If
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try
		End Sub

		Public Function GetTMDBImages(ByVal imdbID As String, ByVal sType As String) As List(Of MediaContainers.Image)
			Dim alPosters As New List(Of MediaContainers.Image)
			Dim images As V3.TmdbMovieImages
			Dim aW, aH As Integer

			If bwTMDB.CancellationPending Then Return Nothing
			Try
				images = _TMDBApi.GetMovieImages(CInt(imdbID), _MySettings.TMDBLanguage)
				If sType = "poster" Then
					If IsNothing(images.posters) OrElse images.posters.Count = 0 Then
						images = _TMDBApiE.GetMovieImages(CInt(imdbID))
						If IsNothing(images.posters) OrElse images.posters.Count = 0 Then
							Return alPosters
						End If
					End If
				Else
					If IsNothing(images.backdrops) OrElse images.backdrops.Count = 0 Then
						images = _TMDBApiE.GetMovieImages(CInt(imdbID))
						If IsNothing(images.backdrops) OrElse images.backdrops.Count = 0 Then
							Return alPosters
						End If
					End If
				End If
				If bwTMDB.WorkerReportsProgress Then
					bwTMDB.ReportProgress(1)
				End If

				If bwTMDB.CancellationPending Then Return Nothing

				If sType = "poster" Then
					For Each tmdbI As V3.Poster In images.posters
						If bwTMDB.CancellationPending Then Return Nothing
						For Each aSize In poster_names
							Select Case aSize.size
								Case "original"
									aW = tmdbI.width
									aH = tmdbI.width
								Case Else
									aW = aSize.width
									aH = CInt(aW / tmdbI.aspect_ratio)
							End Select
							Dim tmpPoster As New MediaContainers.Image With {.URL = _TMDBConf.images.base_url & aSize.size & tmdbI.file_path, .Description = aSize.description, .Width = CStr(aW), .Height = CStr(aH), .ParentID = tmdbI.file_path}
							alPosters.Add(tmpPoster)
						Next
					Next
				ElseIf sType = "backdrop" Then
					For Each tmdbI As V3.Backdrop In images.backdrops
						If bwTMDB.CancellationPending Then Return Nothing
						For Each aSize In backdrop_names
							Select Case aSize.size
								Case "original"
									aW = tmdbI.width
									aH = tmdbI.width
								Case Else
									aW = aSize.width
									aH = CInt(aW / tmdbI.aspect_ratio)
							End Select
							Dim tmpPoster As New MediaContainers.Image With {.URL = _TMDBConf.images.base_url & aSize.size & tmdbI.file_path, .Description = aSize.description, .Width = CStr(aW), .Height = CStr(aH), .ParentID = tmdbI.file_path}
							alPosters.Add(tmpPoster)
						Next
					Next
				End If

				If bwTMDB.WorkerReportsProgress Then
					bwTMDB.ReportProgress(3)
				End If
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try

			Return alPosters
		End Function

		Public Function GetTrailers(ByVal imdbID As String) As List(Of String)
			Dim trailers As V3.TmdbMovieTrailers
			Dim YT As New List(Of String)

			Try
				If bwTMDB.CancellationPending Then Return Nothing
				trailers = _TMDBApi.GetMovieTrailers(CInt(imdbID), _MySettings.TMDBLanguage)
				If IsNothing(trailers.youtube) OrElse trailers.youtube.Count = 0 Then
					trailers = _TMDBApiE.GetMovieTrailers(CInt(imdbID))
					If IsNothing(trailers.youtube) OrElse trailers.youtube.Count = 0 Then
						Return Nothing
					End If
				End If

				If bwTMDB.WorkerReportsProgress Then
					bwTMDB.ReportProgress(1)
				End If
				If bwTMDB.CancellationPending Then Return Nothing

				'If bwTMDB.WorkerReportsProgress Then
				'	bwTMDB.ReportProgress(2)
				'End If

				If trailers.youtube.Count > 0 Then
					For Each trailer In trailers.youtube
						YT.Add(String.Format("http://www.youtube.com/watch?v={0}{1})", trailer.source, CStr(IIf(trailer.size = "HD", "&hd=1", ""))))
					Next
				End If

				If bwTMDB.WorkerReportsProgress Then
					bwTMDB.ReportProgress(3)
				End If

			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try

			Return YT
		End Function

		Private Sub bwTMDB_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTMDB.DoWork
			Dim Args As Arguments = DirectCast(e.Argument, Arguments)
			Try
				e.Result = GetTMDBImages(Args.Parameter, Args.sType)
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
				e.Result = Nothing
			End Try
		End Sub

		Private Sub bwTMDB_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwTMDB.ProgressChanged
			If Not bwTMDB.CancellationPending Then
				RaiseEvent ProgressUpdated(e.ProgressPercentage)
			End If
		End Sub

		Private Sub bwTMDB_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTMDB.RunWorkerCompleted
			If Not IsNothing(e.Result) Then
				RaiseEvent PostersDownloaded(DirectCast(e.Result, List(Of MediaContainers.Image)))
			End If
		End Sub

#End Region	'Methods

#Region "Nested Types"

		Private Structure Arguments

#Region "Fields"

			Dim Parameter As String
			Dim sType As String

#End Region	'Fields

		End Structure

		Private Structure v3Size

#Region "Fields"

			Dim size As String
			Dim description As String
			Dim width As Integer

#End Region	'Fields

		End Structure

#End Region	'Nested Types

	End Class

End Namespace