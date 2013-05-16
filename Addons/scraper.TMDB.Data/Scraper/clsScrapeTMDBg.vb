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

Imports System.Globalization
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports RestSharp
Imports WatTmdb


Namespace TMDBg

	Public Class MovieSearchResults

#Region "Fields"

		Private _Matches As New List(Of MediaContainers.Movie)

#End Region	'Fields

#Region "Properties"

		Public Property Matches() As List(Of MediaContainers.Movie)
			Get
				Return _Matches
			End Get
			Set(ByVal value As List(Of MediaContainers.Movie))
				_Matches = value
			End Set
		End Property

#End Region	'Properties

	End Class

	Public Class Scraper

#Region "Fields"

		Private _TMDBConf As V3.TmdbConfiguration
		Private _TMDBConfE As V3.TmdbConfiguration
		Private _TMDBApi As V3.Tmdb
		Private _TMDBApiE As V3.Tmdb
        Private _MySettings As TMDB_Data.sMySettings

        Friend WithEvents bwTMDBg As New System.ComponentModel.BackgroundWorker

        Private _sPoster As String

#End Region	'Fields

#Region "Properties"
		Public ReadOnly Property TMDBConf() As V3.TmdbConfiguration
			Get
				Return _TMDBConf
			End Get
		End Property
#End Region

#Region "Enumerations"

		Private Enum SearchType
			Movies = 0
			Details = 1
			SearchDetails = 2
		End Enum

#End Region	'Enumerations

#Region "Events"

		Public Event Exception(ByVal ex As Exception)

		Public Event SearchMovieInfoDownloaded(ByVal sPoster As String, ByVal bSuccess As Boolean)

		Public Event SearchResultsDownloaded(ByVal mResults As TMDBg.MovieSearchResults)

#End Region	'Events

#Region "Methods"

        Public Sub New(ByRef tTMDBConf As V3.TmdbConfiguration, ByRef tTMDBConfE As V3.TmdbConfiguration, ByRef tTMDBApi As V3.Tmdb, ByRef tTMDBApiE As V3.Tmdb)
            _MySettings.TMDBAPIKey = AdvancedSettings.GetSetting("TMDBAPIKey", "Get your API Key from http://www.themoviedb.org")
            _MySettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False)
            _MySettings.TMDBLanguage = AdvancedSettings.GetSetting("TMDBLanguage", "en")

            _TMDBApi = tTMDBApi
            _TMDBConf = tTMDBConf
            _TMDBApiE = tTMDBApiE
            _TMDBConfE = tTMDBConfE
        End Sub

		Public Sub CancelAsync()
			If bwTMDBg.IsBusy Then bwTMDBg.CancelAsync()

			While bwTMDBg.IsBusy
				Application.DoEvents()
				Threading.Thread.Sleep(50)
			End While
		End Sub

		Public Sub GetMovieID(ByRef DBMovie As Structures.DBMovie)
			Try
				Dim Movie As WatTmdb.V3.TmdbMovie
				Dim MovieE As WatTmdb.V3.TmdbMovie

				If bwTMDBg.CancellationPending Then Return

				Movie = _TMDBApi.GetMovieByIMDB(DBMovie.Movie.ID, _MySettings.TMDBLanguage)
				MovieE = _TMDBApiE.GetMovieByIMDB(DBMovie.Movie.ID)
				If IsNothing(Movie) AndAlso Not _MySettings.FallBackEng Then
					Return
				End If

				DBMovie.Movie.TMDBID = CStr(IIf(String.IsNullOrEmpty(Movie.id.ToString) AndAlso _MySettings.FallBackEng, MovieE.id.ToString, Movie.id.ToString))

			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try

		End Sub

		Public Function GetMovieInfo(ByVal strID As String, ByRef IMDBMovie As MediaContainers.Movie, ByVal FullCrew As Boolean, ByVal FullCast As Boolean, ByVal GetPoster As Boolean, ByVal Options As Structures.ScrapeOptions, ByVal IsSearch As Boolean) As Boolean
			Try
				Dim Movie As WatTmdb.V3.TmdbMovie
				Dim MovieE As WatTmdb.V3.TmdbMovie
				Dim tStr As String

				If bwTMDBg.CancellationPending Then Return Nothing

				If Strings.Left(strID.ToLower(), 2) = "tt" Then
					Movie = _TMDBApi.GetMovieByIMDB(strID, _MySettings.TMDBLanguage)
					MovieE = _TMDBApiE.GetMovieByIMDB(strID)
				Else
					Movie = _TMDBApi.GetMovieInfo(CInt(strID), _MySettings.TMDBLanguage)
					MovieE = _TMDBApiE.GetMovieInfo(CInt(strID))
                End If

				If IsNothing(Movie) AndAlso Not _MySettings.FallBackEng Then
					Return False
				End If

				IMDBMovie.ID = CStr(IIf(String.IsNullOrEmpty(Movie.imdb_id) AndAlso _MySettings.FallBackEng, MovieE.imdb_id, Movie.imdb_id))
				IMDBMovie.TMDBID = CStr(IIf(String.IsNullOrEmpty(Movie.id.ToString) AndAlso _MySettings.FallBackEng, MovieE.id.ToString, Movie.id.ToString))

				If bwTMDBg.CancellationPending Or IsNothing(Movie) Then Return Nothing

				Dim Keywords As WatTmdb.V3.TmdbMovieKeywords
				Keywords = _TMDBApi.GetMovieKeywords(Movie.id)
				If Not IsNothing(Keywords) AndAlso Not IsNothing(Keywords.keywords) Then
					If Keywords.keywords.Count <> 0 AndAlso _MySettings.FallBackEng Then
						Keywords = _TMDBApiE.GetMovieKeywords(Movie.id)
					End If
				Else
					If _MySettings.FallBackEng Then
						Keywords = _TMDBApiE.GetMovieKeywords(Movie.id)
					End If
				End If

				' to be added the tags structure
				'' <movie>
				'' ...
				'' <tag>Name of the tag</tag>
				'' ...
				'' </movie>
				If Options.bTitle Then
					IMDBMovie.OriginalTitle = CStr(IIf(String.IsNullOrEmpty(Movie.original_title) AndAlso _MySettings.FallBackEng, MovieE.original_title, Movie.original_title))
					If String.IsNullOrEmpty(IMDBMovie.Title) OrElse Not Master.eSettings.LockTitle Then
						IMDBMovie.Title = CStr(IIf(String.IsNullOrEmpty(Movie.title) AndAlso _MySettings.FallBackEng, MovieE.title, Movie.title))
					End If
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				If GetPoster Then
					' I will add original always. to be updated if size, TMDBConf.images.poster_sizes(0) & 
					Dim Images As WatTmdb.V3.TmdbMovieImages
					Images = _TMDBApi.GetMovieImages(Movie.id, _MySettings.TMDBLanguage)
					If Not IsNothing(Images) AndAlso Not IsNothing(Images.posters) Then
						If (Images.posters.Count = 0) AndAlso _MySettings.FallBackEng Then
							Images = _TMDBApiE.GetMovieImages(Movie.id)
						End If
					Else
						If _MySettings.FallBackEng Then
							Images = _TMDBApiE.GetMovieImages(Movie.id)
						End If
					End If
					If Not IsNothing(Images) AndAlso Not IsNothing(Images.posters) Then
						If Images.posters.Count > 0 Then
							_sPoster = _TMDBConf.images.base_url & "w92" & Images.posters(0).file_path
						Else
							_sPoster = ""
						End If
					End If
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				If Options.bYear Then IMDBMovie.Year = Left(CStr(IIf(String.IsNullOrEmpty(Movie.release_date) AndAlso _MySettings.FallBackEng, MovieE.release_date, Movie.release_date)), 4)

				Dim Releases As WatTmdb.V3.TmdbMovieReleases = Nothing
				If Options.bMPAA Then
					IMDBMovie.MPAA = ""
					Releases = _TMDBApi.GetMovieReleases(Movie.id)
					If Not IsNothing(Releases) AndAlso Not IsNothing(Releases.countries) Then
						If (Releases.countries.Count = 0) AndAlso _MySettings.FallBackEng Then
							Releases = _TMDBApiE.GetMovieReleases(Movie.id)
						End If
					Else
						If _MySettings.FallBackEng Then
							Releases = _TMDBApiE.GetMovieReleases(Movie.id)
						End If
					End If

					If Not IsNothing(Releases) AndAlso Not IsNothing(Releases.countries) Then
						For Each Country In Releases.countries
							If Country.iso_3166_1.ToUpper = CStr(IIf(Master.eSettings.CertificationLang = "", "US", Master.eSettings.CertificationLang)) Then
								IMDBMovie.MPAA = Country.certification
								Exit For
							End If
						Next
					End If
					IMDBMovie.Certification = IMDBMovie.MPAA
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				If Options.bRelease Then
					IMDBMovie.ReleaseDate = CStr(IIf(String.IsNullOrEmpty(Movie.release_date) AndAlso _MySettings.FallBackEng, MovieE.release_date, Movie.release_date))
				End If

				If Options.bRating AndAlso (String.IsNullOrEmpty(IMDBMovie.Rating) OrElse Not Master.eSettings.LockRating) Then
					IMDBMovie.Rating = CStr(IIf(IsNothing(Movie.vote_average) AndAlso Movie.vote_average = 0 AndAlso _MySettings.FallBackEng, MovieE.vote_average, Movie.vote_average))
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				'trailer
				If Options.bTrailer AndAlso (String.IsNullOrEmpty(IMDBMovie.Trailer) OrElse Not Master.eSettings.LockTrailer) Then
					Dim Trailers As WatTmdb.V3.TmdbMovieTrailers
					Trailers = _TMDBApi.GetMovieTrailers(Movie.id)
					If Not IsNothing(Trailers) AndAlso Not IsNothing(Trailers.youtube) Then
						If (Trailers.youtube.Count = 0) AndAlso _MySettings.FallBackEng Then
							Trailers = _TMDBApiE.GetMovieTrailers(Movie.id)
						End If
					Else
						If _MySettings.FallBackEng Then
							Trailers = _TMDBApiE.GetMovieTrailers(Movie.id)
						End If
					End If

					IMDBMovie.Trailer = ""
					If Not IsNothing(Trailers) AndAlso Not IsNothing(Trailers.youtube) Then
						If Trailers.youtube.Count > 0 Then
							IMDBMovie.Trailer = "http://www.youtube.com/watch?hd=1&v=" & Trailers.youtube(0).source
						End If
					End If

				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				If Options.bVotes Then
					IMDBMovie.Votes = CStr(IIf(IsNothing(Movie.vote_count) AndAlso Movie.vote_count = 0 AndAlso _MySettings.FallBackEng, MovieE.vote_count.ToString(), Movie.vote_count.ToString()))
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				Dim aCast As WatTmdb.V3.TmdbMovieCast = Nothing
				If Options.bCast Then
					aCast = _TMDBApi.GetMovieCast(Movie.id)
					If Not IsNothing(aCast) AndAlso Not IsNothing(aCast.cast) Then
						If (aCast.cast.Count = 0) AndAlso _MySettings.FallBackEng Then
							aCast = _TMDBApiE.GetMovieCast(Movie.id)
						End If
					Else
						If _MySettings.FallBackEng Then
							aCast = _TMDBApiE.GetMovieCast(Movie.id)
						End If
					End If

					Dim Cast As New List(Of MediaContainers.Person)
					If Not IsNothing(aCast) AndAlso Not IsNothing(aCast.cast) Then
						For Each aAc As WatTmdb.V3.Cast In aCast.cast
							Dim aPer As New MediaContainers.Person
							aPer.Name = aAc.name
							aPer.Role = aAc.character
							' to be added / dialog to choose the size of the images
							aPer.Thumb = _TMDBConf.images.base_url & "original" & aAc.profile_path
							Cast.Add(aPer)
						Next
					End If
					IMDBMovie.Actors = Cast
				End If

				If Options.bTagline AndAlso (String.IsNullOrEmpty(IMDBMovie.Tagline) OrElse Not Master.eSettings.LockTagline) Then
					IMDBMovie.Tagline = CStr(IIf(String.IsNullOrEmpty(IMDBMovie.Tagline) AndAlso _MySettings.FallBackEng, MovieE.tagline, Movie.tagline))
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				'Get countries of the movie
				If Options.bCountry Then
					If IsNothing(Releases) Then
						Releases = _TMDBApi.GetMovieReleases(Movie.id)
						If Not IsNothing(Releases) AndAlso Not IsNothing(Releases.countries) Then
							If (Releases.countries.Count = 0) AndAlso _MySettings.FallBackEng Then
								Releases = _TMDBApiE.GetMovieReleases(Movie.id)
							End If
						Else
							If _MySettings.FallBackEng Then
								Releases = _TMDBApiE.GetMovieReleases(Movie.id)
							End If
						End If
					End If
					IMDBMovie.Countries.Clear()
					If Not IsNothing(Releases) AndAlso Not IsNothing(Releases.countries) Then
						For Each aCo As WatTmdb.V3.ReleaseCountry In Releases.countries
							IMDBMovie.Countries.Add(aCo.iso_3166_1)
						Next
					End If
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				'Get genres of the movie
				If Options.bGenre AndAlso (String.IsNullOrEmpty(IMDBMovie.Genre) OrElse Not Master.eSettings.LockGenre) Then
					IMDBMovie.Genres.Clear()
					Dim tGen As System.Collections.Generic.List(Of WatTmdb.V3.MovieGenre)
					If Not IsNothing(Movie) AndAlso Not IsNothing(Movie.genres) Then
						tGen = CType(IIf(Movie.genres.Count = 0 AndAlso _MySettings.FallBackEng, MovieE.genres, Movie.genres), Global.System.Collections.Generic.List(Of Global.WatTmdb.V3.MovieGenre))
					Else
						tGen = CType(IIf(_MySettings.FallBackEng, MovieE.genres, Nothing), Global.System.Collections.Generic.List(Of Global.WatTmdb.V3.MovieGenre))
					End If


					If Not IsNothing(tGen) Then
						For Each aGen As WatTmdb.V3.MovieGenre In tGen
							IMDBMovie.Genres.Add(aGen.name)
						Next
					End If
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				If Options.bOutline AndAlso (String.IsNullOrEmpty(IMDBMovie.Outline) OrElse Not Master.eSettings.LockOutline) Then
					IMDBMovie.Outline = CStr(IIf(String.IsNullOrEmpty(Movie.overview) AndAlso _MySettings.FallBackEng, MovieE.overview, Movie.overview))
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				If Options.bPlot AndAlso (String.IsNullOrEmpty(IMDBMovie.Plot) OrElse Not Master.eSettings.LockPlot) Then
					If Master.eSettings.OutlineForPlot AndAlso String.IsNullOrEmpty(IMDBMovie.Plot) AndAlso Not String.IsNullOrEmpty(IMDBMovie.Outline) Then
						IMDBMovie.Plot = IMDBMovie.Outline
					Else
						IMDBMovie.Plot = ""
					End If
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				'Get the movie duration
				If Options.bRuntime Then
					IMDBMovie.Runtime = CStr(IIf(IsNothing(Movie.runtime) AndAlso Movie.runtime = 0 AndAlso _MySettings.FallBackEng, MovieE.runtime.ToString(), Movie.runtime.ToString()))
				End If

				'Get Production Studio
				If Options.bStudio AndAlso (String.IsNullOrEmpty(IMDBMovie.Studio) OrElse Not Master.eSettings.LockStudio) Then
					tStr = ""
					Dim tPC As System.Collections.Generic.List(Of WatTmdb.V3.ProductionCompany)
					If Not IsNothing(Movie) AndAlso Not IsNothing(Movie.genres) Then
						tPC = CType(IIf(Movie.production_companies.Count = 0 AndAlso _MySettings.FallBackEng, MovieE.production_companies, Movie.production_companies), Global.System.Collections.Generic.List(Of WatTmdb.V3.ProductionCompany))
					Else
						tPC = CType(IIf(_MySettings.FallBackEng, MovieE.production_companies, Nothing), Global.System.Collections.Generic.List(Of WatTmdb.V3.ProductionCompany))
					End If

					If Not IsNothing(tPC) Then
						For Each aPro As WatTmdb.V3.ProductionCompany In tPC
							tStr = tStr & " / " & aPro.name
						Next
					End If
					If Len(tStr) > 3 Then
						tStr = Trim(Right(tStr, Len(tStr) - 3))
					End If
					IMDBMovie.Studio = tStr
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				'Get All Other Info
				If FullCrew Or Options.bWriters Or Options.bDirector Then
					If IsNothing(aCast) Then
						aCast = _TMDBApi.GetMovieCast(Movie.id)
						If Not IsNothing(aCast) AndAlso Not IsNothing(aCast.cast) Then
							If (aCast.crew.Count = 0) AndAlso _MySettings.FallBackEng Then
								aCast = _TMDBApiE.GetMovieCast(Movie.id)
							End If
						Else
							If _MySettings.FallBackEng Then
								aCast = _TMDBApiE.GetMovieCast(Movie.id)
							End If
						End If
					End If
					IMDBMovie.Credits.Clear()
					IMDBMovie.Directors.Clear()
					If Not IsNothing(aCast.crew) Then
						For Each aAc As WatTmdb.V3.Crew In aCast.crew
							If FullCrew Then
								IMDBMovie.Credits.Add(aAc.name)
							ElseIf Options.bWriters Then
								If aAc.department = "Writing" AndAlso aAc.job = "Writer" Then
									IMDBMovie.Credits.Add(aAc.name)
								End If
							End If
							If Options.bDirector Then
								If aAc.job = "Director" Then
									IMDBMovie.Directors.Add(aAc.name)
								End If
							End If
						Next
					End If
				End If

				If bwTMDBg.CancellationPending Then Return Nothing

				Return True
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
				Return False
			End Try
		End Function

		Public Function GetMovieStudios(ByVal strID As String) As List(Of String)
			Dim alStudio As New List(Of String)
			Dim Movie As WatTmdb.V3.TmdbMovie
			Movie = _TMDBApi.GetMovieByIMDB(strID, _MySettings.TMDBLanguage)
			If Movie.production_companies.Count = 0 And _MySettings.FallBackEng Then
				Movie = _TMDBApi.GetMovieByIMDB(strID, "en")
			End If
			For Each aComp In Movie.production_companies
				alStudio.Add(aComp.name)
			Next

			Return alStudio

		End Function

		Public Function GetSearchMovieInfo(ByVal sMovieName As String, ByRef dbMovie As Structures.DBMovie, ByVal iType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions) As MediaContainers.Movie
			Dim r As MovieSearchResults = SearchMovie(sMovieName)
			Dim b As Boolean = False
			Dim imdbMovie As MediaContainers.Movie = dbMovie.Movie

			Try
				Select Case iType
					Case Enums.ScrapeType.FullAsk, Enums.ScrapeType.UpdateAsk, Enums.ScrapeType.NewAsk, Enums.ScrapeType.MarkAsk, Enums.ScrapeType.FilterAsk

						If r.Matches.Count = 1 Then	'redirected to imdb info page
							b = GetMovieInfo(r.Matches.Item(0).ID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
						Else
							Master.tmpMovie.Clear()
							Using dIMDB As New dlgTMDBSearchResults(_MySettings, Me)

								If dIMDB.ShowDialog(r, sMovieName) = Windows.Forms.DialogResult.OK Then
									If String.IsNullOrEmpty(Master.tmpMovie.IMDBID) Then
										b = False
									Else
										b = GetMovieInfo(Master.tmpMovie.ID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
									End If
								Else
									b = False
								End If
							End Using
						End If
					Case Enums.ScrapeType.FullAuto, Enums.ScrapeType.UpdateAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.SingleScrape, Enums.ScrapeType.FilterAuto
						Dim exactHaveYear As Integer = FindYear(dbMovie.Filename, r.Matches)
						If r.Matches.Count = 1 Then	'redirected to imdb info page
							b = GetMovieInfo(r.Matches.Item(0).ID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
						Else
							b = GetMovieInfo(r.Matches.Item(If(exactHaveYear >= 0, exactHaveYear, 0)).ID, imdbMovie, Master.eSettings.FullCrew, Master.eSettings.FullCast, False, Options, True)
						End If
				End Select

				If b Then
					Return imdbMovie
				Else
					Return New MediaContainers.Movie
				End If
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
				Return New MediaContainers.Movie
			End Try
		End Function

		Private Function FindYear(ByVal tmpname As String, ByVal lst As List(Of MediaContainers.Movie)) As Integer
			Dim tmpyear As String = ""
			Dim i As Integer
			Dim ret As Integer = -1
			tmpname = Path.GetFileNameWithoutExtension(tmpname)
			tmpname = tmpname.Replace(".", " ").Trim.Replace("(", " ").Replace(")", "").Trim
			i = tmpname.LastIndexOf(" ")
			If i >= 0 Then
				tmpyear = tmpname.Substring(i + 1, tmpname.Length - i - 1)
				If IsNumeric(tmpyear) AndAlso Convert.ToInt32(tmpyear) > 1950 Then 'let's assume there are no movies older then 1950
					For c = 0 To lst.Count - 1
						If lst(c).Year = tmpyear Then
							ret = c
							Exit For
						End If
					Next
				End If
			End If
			Return ret
		End Function

		Public Sub GetSearchMovieInfoAsync(ByVal imdbID As String, ByVal IMDBMovie As MediaContainers.Movie, ByVal Options As Structures.ScrapeOptions)
			'' The rule is that if there is a tt is an IMDB otherwise is a TMDB
			Try
				If Not bwTMDBg.IsBusy Then
					bwTMDBg.WorkerReportsProgress = False
					bwTMDBg.WorkerSupportsCancellation = True
					bwTMDBg.RunWorkerAsync(New Arguments With {.Search = SearchType.SearchDetails, _
					  .Parameter = imdbID, .IMDBMovie = IMDBMovie, .Options = Options})
				End If
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try
		End Sub


		Public Sub SearchMovieAsync(ByVal sMovie As String, ByVal filterOptions As Structures.ScrapeOptions)
			'' The rule is that if there is a tt is an IMDB otherwise is a TMDB
			Try
				If Not bwTMDBg.IsBusy Then
					bwTMDBg.WorkerReportsProgress = False
					bwTMDBg.WorkerSupportsCancellation = True
					bwTMDBg.RunWorkerAsync(New Arguments With {.Search = SearchType.Movies, _
					  .Parameter = sMovie, .Options = filterOptions})
				End If
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try
		End Sub

		Private Sub bwTMDBg_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwTMDBg.DoWork
			Dim Args As Arguments = DirectCast(e.Argument, Arguments)
			'' The rule is that if there is a tt is an IMDB otherwise is a TMDB
			Try
				Select Case Args.Search
					Case SearchType.Movies
						Dim r As MovieSearchResults = SearchMovie(Args.Parameter)
						e.Result = New Results With {.ResultType = SearchType.Movies, .Result = r}
					Case SearchType.SearchDetails
						Dim s As Boolean = GetMovieInfo(Args.Parameter, Args.IMDBMovie, False, False, True, Args.Options, True)
						e.Result = New Results With {.ResultType = SearchType.SearchDetails, .Success = s}
				End Select
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try
		End Sub

		Private Sub BW_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwTMDBg.RunWorkerCompleted
			Dim Res As Results = DirectCast(e.Result, Results)

            Try
                Select Case Res.ResultType
                    Case SearchType.Movies
                        RaiseEvent SearchResultsDownloaded(DirectCast(Res.Result, MovieSearchResults))
                    Case SearchType.SearchDetails
                        Dim movieInfo As MovieSearchResults = DirectCast(Res.Result, MovieSearchResults)
                        RaiseEvent SearchMovieInfoDownloaded(_sPoster, Res.Success)
                End Select
            Catch ex As Exception
                Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
            End Try
		End Sub

		Private Function CleanTitle(ByVal sString As String) As String
			Dim CleanString As String = sString

			Try
				If sString.StartsWith("""") Then CleanString = sString.Remove(0, 1)

				If sString.EndsWith("""") Then CleanString = CleanString.Remove(CleanString.Length - 1, 1)
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
			End Try
			Return CleanString
		End Function

		'Private Function GetForcedTitle(ByVal strID As String, ByVal oTitle As String) As String
		'	Dim fTitle As String = oTitle
		'	Dim Movie As WatTmdb.V3.TmdbMovie

		'	Try
		'		'' The rule is that if there is a tt is an IMDB otherwise is a TMDB
		'		If Left(strID.ToLower(), 4) = "tt" Then
		'			Movie = _TMDBApi.GetMovieInfo(CInt(Right(strID, Len(strID) - 4)), _MySettings.TMDBLanguage)
		'			If IsNothing(Movie) And _MySettings.FallBackEng Then
		'				Movie = _TMDBApi.GetMovieInfo(CInt(Right(strID, Len(strID) - 4)), "en")
		'			End If
		'		Else
		'			Movie = _TMDBApi.GetMovieByIMDB(strID)
		'		End If

		'		fTitle = Movie.title

		'		Return fTitle
		'	Catch ex As Exception
		'		Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
		'		Return fTitle
		'	End Try
		'End Function

		Private Function SearchMovie(ByVal sMovie As String) As MovieSearchResults
			Try
				Dim R As New MovieSearchResults
				Dim Page As Integer = 1
				Dim Movies As WatTmdb.V3.TmdbMovieSearch
				Dim TotP As Integer
				Dim aE As Boolean
				Movies = _TMDBApi.SearchMovie(sMovie, Page, _MySettings.TMDBLanguage)
				If Movies.total_results = 0 And _MySettings.FallBackEng Then
					Movies = _TMDBApiE.SearchMovie(sMovie, Page)
					aE = True
				End If
				If Movies.total_results > 0 Then
					Dim t1 As String
					Dim t2 As String
					Dim t3 As String
					TotP = Movies.total_pages
					While Page <= TotP And Page <= 3
						For Each aMovie In Movies.results
							Dim aMI As WatTmdb.V3.TmdbMovie
							aMI = _TMDBApi.GetMovieInfo(aMovie.id)
							If IsNothing(aMI) Then
								aMI = _TMDBApiE.GetMovieInfo(aMovie.id)
							End If
							If IsNothing(aMI.imdb_id) Then
								t1 = ""
							Else
								t1 = aMI.imdb_id.ToString
							End If
							t2 = CStr(IIf(String.IsNullOrEmpty(aMovie.title), "", aMovie.title))
							t3 = Left(CStr(IIf(String.IsNullOrEmpty(aMovie.release_date), "", aMovie.release_date)), 4)
							Dim lNewMovie As MediaContainers.Movie = New MediaContainers.Movie(t1, t2, t3, 0)
							lNewMovie.TMDBID = aMI.id.ToString
							R.Matches.Add(lNewMovie)
						Next
						Page = Page + 1
						If aE Then
							Movies = _TMDBApiE.SearchMovie(sMovie, Page)
						Else
							Movies = _TMDBApi.SearchMovie(sMovie, Page, _MySettings.TMDBLanguage)
						End If

					End While
				End If

				Return R
			Catch ex As Exception
				Master.eLog.WriteToErrorLog(ex.Message, ex.StackTrace, "Error")
				Return Nothing
			End Try
		End Function

#End Region	'Methods

#Region "Nested Types"

		Private Structure Arguments

#Region "Fields"

			Dim FullCast As Boolean
			Dim FullCrew As Boolean
			Dim IMDBMovie As MediaContainers.Movie
			Dim Options As Structures.ScrapeOptions
			Dim Parameter As String
			Dim Search As SearchType
			'Dim TMDBConf As V3.TmdbConfiguration
			'Dim TMDBApi As V3.Tmdb
			'Dim FallBackEng As Boolean
			'Dim TMDBLang As String
#End Region	'Fields

		End Structure

		Private Structure Results

#Region "Fields"

			Dim Result As Object
			Dim ResultType As SearchType
			Dim Success As Boolean

#End Region	'Fields

		End Structure

#End Region	'Nested Types

	End Class

End Namespace

