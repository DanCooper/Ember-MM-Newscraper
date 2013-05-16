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
Imports EmberAPI
Imports RestSharp
Imports WatTmdb
Imports EmberScraperModule.TMDBg

''' <summary>
''' Native Scraper
''' </summary>
''' <remarks></remarks>
Public Class EmberTMDBScraperModule
	Implements Interfaces.EmberMovieScraperModule


#Region "Fields"

	Public Shared ConfigOptions As New Structures.ScrapeOptions
	Public Shared ConfigScrapeModifier As New Structures.ScrapeModifier
	Public Shared _AssemblyName As String

	Private dFImgSelect As dlgImgSelect = Nothing

	Private TMDBId As String
	'Private IMDBid As String

	''' <summary>
	''' Scraping Here
	''' </summary>
	''' <remarks></remarks>
	Private _MySettings As New sMySettings
	Private _TMDBg As TMDBg.Scraper
	Private _Name As String = "Ember TMDB Movie Scrapers"
	Private _PostScraperEnabled As Boolean = False
	Private _ScraperEnabled As Boolean = False
	Private _setup As frmTMDBInfoSettingsHolder
	Private _setupPost As frmTMDBMediaSettingsHolder
	Private _TMDBConf As V3.TmdbConfiguration
	Private _TMDBConfE As V3.TmdbConfiguration
	Private _TMDBApi As V3.Tmdb
	Private _TMDBApiE As V3.Tmdb

#End Region	'Fields

#Region "Events"

	Public Event ModuleSettingsChanged() Implements Interfaces.EmberMovieScraperModule.ModuleSettingsChanged

	'Public Event ScraperUpdateMediaList(ByVal col As Integer, ByVal v As Boolean) Implements Interfaces.EmberMovieScraperModule.MovieScraperEvent
	Public Event MovieScraperEvent(ByVal eType As Enums.MovieScraperEventType, ByVal Parameter As Object) Implements Interfaces.EmberMovieScraperModule.MovieScraperEvent

	Public Event SetupPostScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberMovieScraperModule.PostScraperSetupChanged

	Public Event SetupScraperChanged(ByVal name As String, ByVal State As Boolean, ByVal difforder As Integer) Implements Interfaces.EmberMovieScraperModule.ScraperSetupChanged

	Public Event SetupNeedsRestart() Implements Interfaces.EmberMovieScraperModule.SetupNeedsRestart

#End Region	'Events

#Region "Properties"

	ReadOnly Property IsPostScraper() As Boolean Implements Interfaces.EmberMovieScraperModule.IsPostScraper
		Get
			Return True
		End Get
	End Property

	ReadOnly Property IsScraper() As Boolean Implements Interfaces.EmberMovieScraperModule.IsScraper
		Get
			Return True
		End Get
	End Property

	ReadOnly Property ModuleName() As String Implements Interfaces.EmberMovieScraperModule.ModuleName
		Get
			Return _Name
		End Get
	End Property

	ReadOnly Property ModuleVersion() As String Implements Interfaces.EmberMovieScraperModule.ModuleVersion
		Get
			Return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion.ToString
		End Get
	End Property

	Property PostScraperEnabled() As Boolean Implements Interfaces.EmberMovieScraperModule.PostScraperEnabled
		Get
			Return _PostScraperEnabled
		End Get
		Set(ByVal value As Boolean)
			_PostScraperEnabled = value
		End Set
	End Property

	Property ScraperEnabled() As Boolean Implements Interfaces.EmberMovieScraperModule.ScraperEnabled
		Get
			Return _ScraperEnabled
		End Get
		Set(ByVal value As Boolean)
			_ScraperEnabled = value
		End Set
	End Property

#End Region	'Properties

#Region "Methods"
	Function QueryPostScraperCapabilities(ByVal cap As Enums.PostScraperCapabilities) As Boolean Implements Interfaces.EmberMovieScraperModule.QueryPostScraperCapabilities
		Select Case cap
			Case Enums.PostScraperCapabilities.Fanart
				Return True
			Case Enums.PostScraperCapabilities.Poster
				Return True
			Case Enums.PostScraperCapabilities.Trailer
				If _MySettings.DownloadTrailers Then Return True
		End Select
		Return False
	End Function

	Function DownloadTrailer(ByRef DBMovie As Structures.DBMovie, ByRef sURL As String) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule.DownloadTrailer
		Using dTrailer As New dlgTrailer(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _MySettings)

			sURL = dTrailer.ShowDialog(DBMovie.Movie.TMDBID, DBMovie.Filename)
		End Using
		Return New Interfaces.ModuleResult With {.breakChain = False}
	End Function

	Function GetMovieStudio(ByRef DBMovie As Structures.DBMovie, ByRef studio As List(Of String)) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule.GetMovieStudio
		'' perche' lo crea?

		''Dim TMDBg As New TMDBg.Scraper
		''TMDBg.IMDBURL = MySettings.IMDBURL
		studio = _TMDBg.GetMovieStudios(DBMovie.Movie.ID)
		Return New Interfaces.ModuleResult With {.breakChain = False}
	End Function

	Private Sub Handle_ModuleSettingsChanged()
		RaiseEvent ModuleSettingsChanged()
	End Sub

	Private Sub Handle_PostModuleSettingsChanged()
		RaiseEvent ModuleSettingsChanged()
	End Sub

	Private Sub Handle_SetupNeedsRestart()
		RaiseEvent SetupNeedsRestart()
	End Sub


	Private Sub Handle_SetupPostScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
		PostScraperEnabled = state
		RaiseEvent SetupPostScraperChanged(String.Concat(Me._Name, "PostScraper"), state, difforder)
	End Sub

	Private Sub Handle_SetupScraperChanged(ByVal state As Boolean, ByVal difforder As Integer)
		ScraperEnabled = state
		RaiseEvent SetupScraperChanged(String.Concat(Me._Name, "Scraper"), state, difforder)
	End Sub

	Sub Init(ByVal sAssemblyName As String) Implements Interfaces.EmberMovieScraperModule.Init
		_AssemblyName = sAssemblyName
		LoadSettings()
		'Must be after Load settings to retrieve the correct API key
		_TMDBApi = New WatTmdb.V3.Tmdb(_MySettings.TMDBAPIKey, _MySettings.TMDBLanguage)
		If IsNothing(_TMDBApi) Then
			Master.eLog.WriteToErrorLog(Master.eLang.GetString(119, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message, "Info")
		Else
			If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
				Master.eLog.WriteToErrorLog(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString(), "Error")
			End If
		End If
		_TMDBConf = _TMDBApi.GetConfiguration()
			_TMDBApiE = New WatTmdb.V3.Tmdb(_MySettings.TMDBAPIKey)
			_TMDBConfE = _TMDBApiE.GetConfiguration()
			_TMDBg = New TMDBg.Scraper(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _MySettings)

	End Sub

	Function InjectSetupPostScraper() As Containers.SettingsPanel Implements Interfaces.EmberMovieScraperModule.InjectSetupPostScraper
		Dim Spanel As New Containers.SettingsPanel
		_setupPost = New frmTMDBMediaSettingsHolder
		LoadSettings()
		_setupPost.cbEnabled.Checked = _PostScraperEnabled
		_setupPost.chkTrailerIMDB.Checked = _MySettings.UseIMDBTrailer
		_setupPost.chkTrailerTMDB.Checked = _MySettings.UseTMDBTrailer
		_setupPost.cbTrailerTMDBPref.Text = _MySettings.UseTMDBTrailerPref
		_setupPost.chkTrailerTMDBXBMC.Checked = _MySettings.UseTMDBTrailerXBMC
		_setupPost.chkScrapePoster.Checked = ConfigScrapeModifier.Poster
		_setupPost.chkScrapeFanart.Checked = ConfigScrapeModifier.Fanart
		_setupPost.chkUseIMDBp.Checked = _MySettings.UseIMDB
		_setupPost.chkUseIMPA.Checked = _MySettings.UseIMPA
		_setupPost.chkUseMPDB.Checked = _MySettings.UseMPDB
		_setupPost.chkUseFANARTTV.Checked = _MySettings.UseFANARTTV

		' ad size from TMDB
		'_setupPost.cbManualETSize.Items.Clear()
		'If Not IsNothing(_TMDBg.TMDBConf.images) Then
		'	For Each aSi In _TMDBg.TMDBConf.images.poster_sizes
		'	_setupPost.cbManualETSize.Items.Add(aSi)
		'		Next
		'
		'End If
		_setupPost.cbManualETSize.Text = _MySettings.ManualETSize

		_setupPost.txtTimeout.Text = _MySettings.TrailerTimeout.ToString
		_setupPost.chkDownloadTrailer.Checked = _MySettings.DownloadTrailers
		_setupPost.CheckTrailer()
		_setupPost.orderChanged()
		Spanel.Name = String.Concat(Me._Name, "PostScraper")
		Spanel.Text = Master.eLang.GetString(104, "Ember TMDB Movie Scrapers")
		Spanel.Prefix = "TMDBMovieMedia_"
		Spanel.Order = 110
		Spanel.Parent = "pnlMovieMedia"
		Spanel.Type = Master.eLang.GetString(36, "Movies", True)
		Spanel.ImageIndex = If(Me._PostScraperEnabled, 9, 10)
		Spanel.Panel = Me._setupPost.pnlSettings

		AddHandler _setupPost.SetupPostScraperChanged, AddressOf Handle_SetupPostScraperChanged
		AddHandler _setupPost.ModuleSettingsChanged, AddressOf Handle_PostModuleSettingsChanged
		AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
		Return Spanel
	End Function

	Function InjectSetupScraper() As Containers.SettingsPanel Implements Interfaces.EmberMovieScraperModule.InjectSetupScraper
		Dim SPanel As New Containers.SettingsPanel
		_setup = New frmTMDBInfoSettingsHolder
		LoadSettings()
		_setup.cbEnabled.Checked = _ScraperEnabled
		_setup.chkTitle.Checked = ConfigOptions.bTitle
		_setup.chkYear.Checked = ConfigOptions.bYear
		_setup.chkMPAA.Checked = ConfigOptions.bMPAA
		_setup.chkRelease.Checked = ConfigOptions.bRelease
		_setup.chkRuntime.Checked = ConfigOptions.bRuntime
		_setup.chkRating.Checked = ConfigOptions.bRating
		_setup.chkVotes.Checked = ConfigOptions.bVotes
		_setup.chkStudio.Checked = ConfigOptions.bStudio
		_setup.chkTagline.Checked = ConfigOptions.bTagline
		_setup.chkOutline.Checked = ConfigOptions.bOutline
		_setup.chkCast.Checked = ConfigOptions.bFullCast
		_setup.chkGenre.Checked = ConfigOptions.bGenre
		_setup.chkTrailer.Checked = ConfigOptions.bTrailer
		_setup.chkCountry.Checked = ConfigOptions.bCountry
		_setup.chkCrew.Checked = ConfigOptions.bFullCrew
		_setup.chkFallBackEng.Checked = _MySettings.FallBackEng
		_setup.cbTMDBPrefLanguage.Text = _MySettings.TMDBLanguage

		'_setup.chkCertification.Checked = ConfigOptions.bCert

		If String.IsNullOrEmpty(_MySettings.TMDBAPIKey) Then
			_MySettings.TMDBAPIKey = Master.eLang.GetString(122, "Get your API Key from www.themoviedb.org")
		End If
		_setup.txtTMDBApiKey.Text = _MySettings.TMDBAPIKey
		_setup.cbTMDBPrefLanguage.Text = _MySettings.TMDBLanguage
		_setup.chkFallBackEng.Checked = _MySettings.FallBackEng
		_setup.orderChanged()
		If String.IsNullOrEmpty(_MySettings.FANARTTVApiKey) Then
			_MySettings.FANARTTVApiKey = Master.eLang.GetString(123, "Get your API Key from fanart.tv")
		End If
		_setup.txtFANARTTVApiKey.Text = _MySettings.FANARTTVApiKey

		SPanel.Name = String.Concat(Me._Name, "Scraper")
		SPanel.Text = Master.eLang.GetString(104, "Ember TMDB Movie Scrapers")
		SPanel.Prefix = "TMDBMovieInfo_"
		SPanel.Order = 110
		SPanel.Parent = "pnlMovieData"
		SPanel.Type = Master.eLang.GetString(36, "Movies", True)
		SPanel.ImageIndex = If(_ScraperEnabled, 9, 10)
		SPanel.Panel = _setup.pnlSettings
		AddHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
		AddHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
		AddHandler _setup.SetupNeedsRestart, AddressOf Handle_SetupNeedsRestart
		Return SPanel
	End Function

	Sub LoadSettings()
		ConfigOptions.bTitle = AdvancedSettings.GetBooleanSetting("DoTitle", True)
		ConfigOptions.bYear = AdvancedSettings.GetBooleanSetting("DoYear", True)
		ConfigOptions.bMPAA = AdvancedSettings.GetBooleanSetting("DoMPAA", True)
		ConfigOptions.bRelease = AdvancedSettings.GetBooleanSetting("DoRelease", True)
		ConfigOptions.bRuntime = AdvancedSettings.GetBooleanSetting("DoRuntime", True)
		ConfigOptions.bRating = AdvancedSettings.GetBooleanSetting("DoRating", True)
		ConfigOptions.bVotes = AdvancedSettings.GetBooleanSetting("DoVotes", True)
		ConfigOptions.bStudio = AdvancedSettings.GetBooleanSetting("DoStudio", True)
		ConfigOptions.bTagline = AdvancedSettings.GetBooleanSetting("DoTagline", True)
		ConfigOptions.bOutline = AdvancedSettings.GetBooleanSetting("DoOutline", True)
		ConfigOptions.bPlot = AdvancedSettings.GetBooleanSetting("DoPlot", True)
		ConfigOptions.bCast = AdvancedSettings.GetBooleanSetting("DoCast", True)
		ConfigOptions.bDirector = AdvancedSettings.GetBooleanSetting("DoDirector", True)
		ConfigOptions.bWriters = AdvancedSettings.GetBooleanSetting("DoWriters", True)
		ConfigOptions.bProducers = AdvancedSettings.GetBooleanSetting("DoProducers", True)
		ConfigOptions.bGenre = AdvancedSettings.GetBooleanSetting("DoGenres", True)
		ConfigOptions.bTrailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True)
		ConfigOptions.bMusicBy = AdvancedSettings.GetBooleanSetting("DoMusic", True)
		ConfigOptions.bOtherCrew = AdvancedSettings.GetBooleanSetting("DoOtherCrews", True)
		ConfigOptions.bFullCast = AdvancedSettings.GetBooleanSetting("DoFullCast", True)
		ConfigOptions.bFullCrew = AdvancedSettings.GetBooleanSetting("DoFullCrews", True)
		ConfigOptions.bTop250 = AdvancedSettings.GetBooleanSetting("DoTop250", True)
		ConfigOptions.bCountry = AdvancedSettings.GetBooleanSetting("DoCountry", True)
		ConfigOptions.bCert = AdvancedSettings.GetBooleanSetting("DoCert", True)
		ConfigOptions.bFullCast = AdvancedSettings.GetBooleanSetting("FullCast", True)
		ConfigOptions.bFullCrew = AdvancedSettings.GetBooleanSetting("FullCrew", True)

		_MySettings.TMDBAPIKey = AdvancedSettings.GetSetting("TMDBAPIKey", "Get your API Key from http://www.themoviedb.org")
		_MySettings.FANARTTVApiKey = AdvancedSettings.GetSetting("FANARTTVApiKey", "Get your API Key from http://fanart.tv")
		_MySettings.FallBackEng = AdvancedSettings.GetBooleanSetting("FallBackEn", False)
		_MySettings.TMDBLanguage = AdvancedSettings.GetSetting("TMDBLanguage", "en")
		_MySettings.DownloadTrailers = AdvancedSettings.GetBooleanSetting("DownloadTraliers", False)

		_MySettings.TrailerTimeout = Convert.ToInt32(AdvancedSettings.GetSetting("TrailerTimeout", "10"))
		_MySettings.UseTMDBTrailer = AdvancedSettings.GetBooleanSetting("UseTMDBTrailer", True)
		_MySettings.UseTMDBTrailerXBMC = AdvancedSettings.GetBooleanSetting("UseTMDBTrailerXBMC", False)
		_MySettings.UseIMPA = AdvancedSettings.GetBooleanSetting("UseIMPA", False)
		_MySettings.UseMPDB = AdvancedSettings.GetBooleanSetting("UseMPDB", False)
		_MySettings.UseIMDB = AdvancedSettings.GetBooleanSetting("UseIMDB", False)
		_MySettings.UseFANARTTV = AdvancedSettings.GetBooleanSetting("UseFANARTTV", False)
		_MySettings.UseIMDBTrailer = AdvancedSettings.GetBooleanSetting("UseIMDBTrailer", True)
		_MySettings.ManualETSize = Convert.ToString(AdvancedSettings.GetSetting("ManualETSize", "thumb"))
		_MySettings.UseTMDBTrailerPref = Convert.ToString(AdvancedSettings.GetSetting("UseTMDBTrailerPref", "en"))

		ConfigScrapeModifier.DoSearch = True
		ConfigScrapeModifier.Meta = True
		ConfigScrapeModifier.NFO = True
		ConfigScrapeModifier.Extra = True
		ConfigScrapeModifier.Actors = True

		ConfigScrapeModifier.Poster = AdvancedSettings.GetBooleanSetting("DoPoster", True)
		ConfigScrapeModifier.Fanart = AdvancedSettings.GetBooleanSetting("DoFanart", True)
		ConfigScrapeModifier.Trailer = AdvancedSettings.GetBooleanSetting("DoTrailer", True)
	End Sub

	Function PostScraper(ByRef DBMovie As Structures.DBMovie, ByVal ScrapeType As Enums.ScrapeType) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule.PostScraper
		'LoadSettings()
		Dim Poster As New Images
		Dim Fanart As New Images
		Dim pResults As Containers.ImgResult
		Dim fResults As Containers.ImgResult
		Dim tURL As String = String.Empty
		Dim Trailer As Trailers
		Dim aScrapeImages As ScrapeImages

		LoadSettings()
		If String.IsNullOrEmpty(DBMovie.Movie.TMDBID) Then
			_TMDBg.GetMovieID(DBMovie)
		End If
		Trailer = New Trailers(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _MySettings)
		Dim saveModifier As Structures.ScrapeModifier = Master.GlobalScrapeMod
		Master.GlobalScrapeMod = Functions.ScrapeModifierAndAlso(Master.GlobalScrapeMod, ConfigScrapeModifier)

		Dim doSave As Boolean = False

		aScrapeImages = New ScrapeImages(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _MySettings)
		If Master.GlobalScrapeMod.Poster Then
			Poster.Clear()
			If Poster.IsAllowedToDownload(DBMovie, Enums.ImageType.Posters) Then
				pResults = New Containers.ImgResult
				If aScrapeImages.GetPreferredImage(Poster, DBMovie.Movie.ID, DBMovie.Movie.TMDBID, Enums.ImageType.Posters, pResults, DBMovie.Filename, False, If(ScrapeType = Enums.ScrapeType.FullAsk OrElse ScrapeType = Enums.ScrapeType.NewAsk OrElse ScrapeType = Enums.ScrapeType.MarkAsk OrElse ScrapeType = Enums.ScrapeType.UpdateAsk, True, False)) Then
					If Not IsNothing(Poster.Image) Then
						pResults.ImagePath = Poster.SaveAsPoster(DBMovie)
						If Not String.IsNullOrEmpty(pResults.ImagePath) Then
							DBMovie.PosterPath = pResults.ImagePath
							RaiseEvent MovieScraperEvent(Enums.MovieScraperEventType.PosterItem, True) '4, True)
							If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
								DBMovie.Movie.Thumb = pResults.Posters
							End If
						End If
					ElseIf ScrapeType = Enums.ScrapeType.FullAsk OrElse ScrapeType = Enums.ScrapeType.NewAsk OrElse ScrapeType = Enums.ScrapeType.MarkAsk OrElse ScrapeType = Enums.ScrapeType.UpdateAsk Then
						MsgBox(Master.eLang.GetString(76, "A poster of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(77, "No Preferred Size"))
						Using dImgSelect As New dlgImgSelect(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApi, _MySettings)
							pResults = dImgSelect.ShowDialog(DBMovie, Enums.ImageType.Posters)
							If Not String.IsNullOrEmpty(pResults.ImagePath) Then
								DBMovie.PosterPath = pResults.ImagePath
								RaiseEvent MovieScraperEvent(Enums.MovieScraperEventType.PosterItem, True) '4, True)
								If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
									DBMovie.Movie.Thumb = pResults.Posters
								End If
							End If
						End Using
					End If
				End If
			End If
		End If
		Dim didEts As Boolean
		If Master.GlobalScrapeMod.Fanart Then
			Fanart.Clear()
			If Fanart.IsAllowedToDownload(DBMovie, Enums.ImageType.Fanart) Then
				fResults = New Containers.ImgResult
				didEts = True
				If aScrapeImages.GetPreferredImage(Fanart, DBMovie.Movie.IMDBID, DBMovie.Movie.TMDBID, Enums.ImageType.Fanart, fResults, DBMovie.Filename, Master.GlobalScrapeMod.Extra, If(ScrapeType = Enums.ScrapeType.FullAsk OrElse ScrapeType = Enums.ScrapeType.NewAsk OrElse ScrapeType = Enums.ScrapeType.MarkAsk OrElse ScrapeType = Enums.ScrapeType.UpdateAsk, True, False)) Then
					If Not IsNothing(Fanart.Image) Then
						fResults.ImagePath = Fanart.SaveAsFanart(DBMovie)
						If Not String.IsNullOrEmpty(fResults.ImagePath) Then
							DBMovie.FanartPath = fResults.ImagePath
							RaiseEvent MovieScraperEvent(Enums.MovieScraperEventType.FanartItem, True) '
							If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
								DBMovie.Movie.Fanart = fResults.Fanart
							End If
						End If
					ElseIf ScrapeType = Enums.ScrapeType.FullAsk OrElse ScrapeType = Enums.ScrapeType.NewAsk OrElse ScrapeType = Enums.ScrapeType.MarkAsk OrElse ScrapeType = Enums.ScrapeType.UpdateAsk Then
						MsgBox(Master.eLang.GetString(78, "Fanart of your preferred size could not be found. Please choose another."), MsgBoxStyle.Information, Master.eLang.GetString(77, "No Preferred Size:"))

						Using dImgSelect As New dlgImgSelect(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApi, _MySettings)
							fResults = dImgSelect.ShowDialog(DBMovie, Enums.ImageType.Fanart)
							If Not String.IsNullOrEmpty(fResults.ImagePath) Then
								DBMovie.FanartPath = fResults.ImagePath
								RaiseEvent MovieScraperEvent(Enums.MovieScraperEventType.FanartItem, True)
								If Master.GlobalScrapeMod.NFO AndAlso Not Master.eSettings.NoSaveImagesToNfo Then
									DBMovie.Movie.Fanart = fResults.Fanart
								End If
							End If
						End Using
					End If
				End If
			End If
		End If
		If Master.GlobalScrapeMod.Trailer AndAlso _MySettings.DownloadTrailers Then
			tURL = Trailer.DownloadSingleTrailer(DBMovie.Filename, DBMovie.Movie.TMDBID, DBMovie.isSingle, DBMovie.Movie.Trailer)
			If Not String.IsNullOrEmpty(tURL) Then
				If tURL.Substring(0, 22) = "http://www.youtube.com" Then
					If AdvancedSettings.GetBooleanSetting("UseTMDBTrailerXBMC", False) Then
						DBMovie.Movie.Trailer = Replace(tURL, "http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
					Else
						DBMovie.Movie.Trailer = tURL
					End If
				ElseIf tURL.Substring(0, 7) = "http://" Then
					DBMovie.Movie.Trailer = tURL
				Else
					DBMovie.TrailerPath = tURL
					RaiseEvent MovieScraperEvent(Enums.MovieScraperEventType.TrailerItem, True)
				End If
			End If
		End If
		If Master.GlobalScrapeMod.Extra Then
			If Master.eSettings.AutoET AndAlso DBMovie.isSingle Then
				Try
					aScrapeImages.GetPreferredFAasET(DBMovie.Movie.TMDBID, DBMovie.Filename)
					RaiseEvent MovieScraperEvent(Enums.MovieScraperEventType.ThumbsItem, True)
				Catch ex As Exception
				End Try
			End If
		End If
        If Master.GlobalScrapeMod.Actors AndAlso Master.eSettings.ScraperActorThumbs Then
            For Each act As MediaContainers.Person In DBMovie.Movie.Actors
                Dim img As New Images
                img.FromWeb(act.Thumb)
                If Not IsNothing(img.Image) Then
                    img.SaveAsActorThumb(act, Directory.GetParent(DBMovie.Filename).FullName, DBMovie)
                End If
            Next
        End If
		Master.GlobalScrapeMod = saveModifier
		Return New Interfaces.ModuleResult With {.breakChain = False, .BoolProperty = didEts}
	End Function

	Sub SaveSettings()
		AdvancedSettings.SetBooleanSetting("DoFullCast", ConfigOptions.bFullCast)
		AdvancedSettings.SetBooleanSetting("DoFullCrews", ConfigOptions.bFullCrew)
		AdvancedSettings.SetBooleanSetting("DoTitle", ConfigOptions.bTitle)
		AdvancedSettings.SetBooleanSetting("DoYear", ConfigOptions.bYear)
		AdvancedSettings.SetBooleanSetting("DoMPAA", ConfigOptions.bMPAA)
		AdvancedSettings.SetBooleanSetting("DoRelease", ConfigOptions.bRelease)
		AdvancedSettings.SetBooleanSetting("DoRuntime", ConfigOptions.bRuntime)
		AdvancedSettings.SetBooleanSetting("DoRating", ConfigOptions.bRating)
		AdvancedSettings.SetBooleanSetting("DoVotes", ConfigOptions.bVotes)
		AdvancedSettings.SetBooleanSetting("DoStudio", ConfigOptions.bStudio)
		AdvancedSettings.SetBooleanSetting("DoTagline", ConfigOptions.bTagline)
		AdvancedSettings.SetBooleanSetting("DoOutline", ConfigOptions.bOutline)
		AdvancedSettings.SetBooleanSetting("DoPlot", ConfigOptions.bPlot)
		AdvancedSettings.SetBooleanSetting("DoCast", ConfigOptions.bCast)
		AdvancedSettings.SetBooleanSetting("DoDirector", ConfigOptions.bDirector)
		AdvancedSettings.SetBooleanSetting("DoWriters", ConfigOptions.bWriters)
		AdvancedSettings.SetBooleanSetting("DoProducers", ConfigOptions.bProducers)
		AdvancedSettings.SetBooleanSetting("DoGenres", ConfigOptions.bGenre)
		AdvancedSettings.SetBooleanSetting("DoTrailer", ConfigOptions.bTrailer)
		AdvancedSettings.SetBooleanSetting("DoMusic", ConfigOptions.bMusicBy)
		AdvancedSettings.SetBooleanSetting("DoOtherCrews", ConfigOptions.bOtherCrew)
		AdvancedSettings.SetBooleanSetting("DoCountry", ConfigOptions.bCountry)
		AdvancedSettings.SetBooleanSetting("DoTop250", ConfigOptions.bTop250)
		AdvancedSettings.SetBooleanSetting("DoCert", ConfigOptions.bCert)

		AdvancedSettings.SetBooleanSetting("FullCast", ConfigOptions.bFullCast)
		AdvancedSettings.SetBooleanSetting("FullCrew", ConfigOptions.bFullCrew)
		AdvancedSettings.SetBooleanSetting("DownloadTraliers", _MySettings.DownloadTrailers)

		AdvancedSettings.SetSetting("TrailerTimeout", _MySettings.TrailerTimeout.ToString)
		AdvancedSettings.SetBooleanSetting("UseTMDBTrailer", _MySettings.UseTMDBTrailer)
		AdvancedSettings.SetBooleanSetting("UseTMDBTrailerXBMC", _MySettings.UseTMDBTrailerXBMC)
		AdvancedSettings.SetBooleanSetting("UseIMDBTrailer", _MySettings.UseIMDBTrailer)

		AdvancedSettings.SetSetting("ManualETSize", _MySettings.ManualETSize.ToString)
		AdvancedSettings.SetSetting("UseTMDBTrailerPref", _MySettings.UseTMDBTrailerPref.ToString)

		AdvancedSettings.SetBooleanSetting("DoPoster", ConfigScrapeModifier.Poster)
		AdvancedSettings.SetBooleanSetting("DoFanart", ConfigScrapeModifier.Fanart)
		AdvancedSettings.SetBooleanSetting("DoTrailer", ConfigScrapeModifier.Trailer)

		AdvancedSettings.SetSetting("TMDBAPIKey", _MySettings.TMDBAPIKey)
		AdvancedSettings.SetSetting("FANARTTVApiKey", _MySettings.FANARTTVApiKey)
		AdvancedSettings.SetBooleanSetting("FallBackEn", _MySettings.FallBackEng)
		AdvancedSettings.SetSetting("TMDBLanguage", _MySettings.TMDBLanguage)
		AdvancedSettings.SetBooleanSetting("UseIMPA", _MySettings.UseIMPA)
		AdvancedSettings.SetBooleanSetting("UseMPDB", _MySettings.UseMPDB)
		AdvancedSettings.SetBooleanSetting("UseIMDB", _MySettings.UseIMDB)
		AdvancedSettings.SetBooleanSetting("UseFANARTTV", _MySettings.UseFANARTTV)
	End Sub

	Sub SaveSetupPostScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberMovieScraperModule.SaveSetupPostScraper
		_MySettings.DownloadTrailers = _setupPost.chkDownloadTrailer.Checked
		_MySettings.UseIMDBTrailer = _setupPost.chkTrailerIMDB.Checked
		_MySettings.UseTMDBTrailer = _setupPost.chkTrailerTMDB.Checked
		_MySettings.UseTMDBTrailerXBMC = _setupPost.chkTrailerTMDBXBMC.Checked
		_MySettings.TrailerTimeout = Convert.ToInt32(_setupPost.txtTimeout.Text)
		_MySettings.ManualETSize = _setupPost.cbManualETSize.Text
		_MySettings.UseTMDBTrailerPref = _setupPost.cbTrailerTMDBPref.Text
		_MySettings.UseIMDB = _setupPost.chkUseIMDBp.Checked
		_MySettings.UseIMPA = _setupPost.chkUseIMPA.Checked
		_MySettings.UseMPDB = _setupPost.chkUseMPDB.Checked
		_MySettings.UseFANARTTV = _setupPost.chkUseFANARTTV.Checked
		ConfigScrapeModifier.Poster = _setupPost.chkScrapePoster.Checked
		ConfigScrapeModifier.Fanart = _setupPost.chkScrapeFanart.Checked
		SaveSettings()
		'ModulesManager.Instance.SaveSettings()
		If DoDispose Then
			RemoveHandler _setupPost.SetupPostScraperChanged, AddressOf Handle_SetupPostScraperChanged
			RemoveHandler _setupPost.ModuleSettingsChanged, AddressOf Handle_PostModuleSettingsChanged
			_setupPost.Dispose()
		End If
	End Sub

	Sub SaveSetupScraper(ByVal DoDispose As Boolean) Implements Interfaces.EmberMovieScraperModule.SaveSetupScraper
		If Not String.IsNullOrEmpty(_setup.txtTMDBApiKey.Text) Then
			_MySettings.TMDBAPIKey = _setup.txtTMDBApiKey.Text
		Else
			_MySettings.TMDBAPIKey = Master.eLang.GetString(122, "Get your API Key from www.themoviedb.org")
		End If
		If Not String.IsNullOrEmpty(_setup.txtFANARTTVApiKey.Text) Then
			_MySettings.FANARTTVApiKey = _setup.txtFANARTTVApiKey.Text
		Else
			_MySettings.FANARTTVApiKey = Master.eLang.GetString(123, "Get your API Key from fanart.tv")
		End If
		_MySettings.TMDBLanguage = _setup.cbTMDBPrefLanguage.Text
		_MySettings.FallBackEng = _setup.chkFallBackEng.Checked
		ConfigOptions.bTitle = _setup.chkTitle.Checked
		ConfigOptions.bYear = _setup.chkYear.Checked
		ConfigOptions.bMPAA = _setup.chkMPAA.Checked
		ConfigOptions.bRelease = _setup.chkRelease.Checked
		ConfigOptions.bRuntime = _setup.chkRuntime.Checked
		ConfigOptions.bRating = _setup.chkRating.Checked
		ConfigOptions.bVotes = _setup.chkVotes.Checked
		ConfigOptions.bStudio = _setup.chkStudio.Checked
		ConfigOptions.bTagline = _setup.chkTagline.Checked
		ConfigOptions.bOutline = _setup.chkOutline.Checked
		ConfigOptions.bPlot = False
		ConfigOptions.bCast = _setup.chkCast.Checked
		ConfigOptions.bDirector = _setup.chkCrew.Checked
		ConfigOptions.bWriters = _setup.chkCrew.Checked
		ConfigOptions.bProducers = _setup.chkCrew.Checked
		ConfigOptions.bGenre = _setup.chkGenre.Checked
		ConfigOptions.bTrailer = _setup.chkTrailer.Checked
		ConfigOptions.bMusicBy = _setup.chkCrew.Checked
		ConfigOptions.bOtherCrew = _setup.chkCrew.Checked
		ConfigOptions.bCountry = _setup.chkCountry.Checked
		ConfigOptions.bTop250 = False
		ConfigOptions.bFullCrew = _setup.chkCrew.Checked
		ConfigOptions.bFullCast = _setup.chkCast.Checked
		ConfigOptions.bCert = ConfigOptions.bMPAA
		_MySettings.FallBackEng = _setup.chkFallBackEng.Checked
		_MySettings.TMDBLanguage = _setup.cbTMDBPrefLanguage.Text
		SaveSettings()
		'ModulesManager.Instance.SaveSettings()
		If DoDispose Then
			RemoveHandler _setup.SetupScraperChanged, AddressOf Handle_SetupScraperChanged
			RemoveHandler _setup.ModuleSettingsChanged, AddressOf Handle_ModuleSettingsChanged
			_setup.Dispose()
		End If
	End Sub

	Function Scraper(ByRef DBMovie As Structures.DBMovie, ByRef ScrapeType As Enums.ScrapeType, ByRef Options As Structures.ScrapeOptions) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule.Scraper
		'LoadSettings()
		''TMDBg.IMDBURL = MySettings.IMDBURL
		''TMDBg.UseOFDBTitle = MySettings.UseOFDBTitle
		''TMDBg.UseOFDBOutline = MySettings.UseOFDBOutline
		''TMDBg.UseOFDBPlot = MySettings.UseOFDBPlot
		''TMDBg.UseOFDBGenre = MySettings.UseOFDBGenre
		Dim tTitle As String = String.Empty
		Dim OldTitle As String = DBMovie.Movie.Title

		If IsNothing(_TMDBApi) Then
			Master.eLog.WriteToErrorLog(Master.eLang.GetString(119, "TheMovieDB API is missing or not valid"), _TMDBApi.Error.status_message, "Error")
			Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
		Else
			If Not IsNothing(_TMDBApi.Error) AndAlso _TMDBApi.Error.status_message.Length > 0 Then
				Master.eLog.WriteToErrorLog(_TMDBApi.Error.status_message, _TMDBApi.Error.status_code.ToString(), "Error")
				Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
			End If
		End If


		If Master.GlobalScrapeMod.NFO AndAlso Not Master.GlobalScrapeMod.DoSearch Then
			If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
				_TMDBg.GetMovieInfo(DBMovie.Movie.ID, DBMovie.Movie, Options.bFullCrew, Options.bFullCast, False, Options, False)
			ElseIf Not ScrapeType = Enums.ScrapeType.SingleScrape Then
				DBMovie.Movie = _TMDBg.GetSearchMovieInfo(DBMovie.Movie.Title, DBMovie, ScrapeType, Options)
				If String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
			End If
		End If

		If ScrapeType = Enums.ScrapeType.SingleScrape AndAlso Master.GlobalScrapeMod.DoSearch _
		 AndAlso ModulesManager.Instance.externalScrapersModules.OrderBy(Function(y) y.ScraperOrder).FirstOrDefault(Function(e) e.ProcessorModule.IsScraper AndAlso e.ProcessorModule.ScraperEnabled).AssemblyName = _AssemblyName Then
			DBMovie.Movie.IMDBID = String.Empty
			DBMovie.ClearExtras = True
			DBMovie.PosterPath = String.Empty
			DBMovie.FanartPath = String.Empty
			DBMovie.TrailerPath = String.Empty
			DBMovie.ExtraPath = String.Empty
			DBMovie.SubPath = String.Empty
			DBMovie.NfoPath = String.Empty
			DBMovie.Movie.Clear()
		End If
		If String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then
			Select Case ScrapeType
				Case Enums.ScrapeType.FilterAuto, Enums.ScrapeType.FullAuto, Enums.ScrapeType.MarkAuto, Enums.ScrapeType.NewAuto, Enums.ScrapeType.UpdateAuto
					Return New Interfaces.ModuleResult With {.breakChain = False}
			End Select
			If ScrapeType = Enums.ScrapeType.SingleScrape Then
				Using dSearch As New dlgTMDBSearchResults(_MySettings, Me._TMDBg)
					''			dSearch.IMDBURL = MySettings.IMDBURL
					Dim tmpTitle As String = DBMovie.Movie.Title
					If String.IsNullOrEmpty(tmpTitle) Then
						If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
							tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name, False)
						ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
							tmpTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name, False)
						Else
							tmpTitle = StringUtils.FilterName(If(DBMovie.isSingle, Directory.GetParent(DBMovie.Filename).Name, Path.GetFileNameWithoutExtension(DBMovie.Filename)))
						End If
					End If
					Dim filterOptions As Structures.ScrapeOptions = Functions.ScrapeOptionsAndAlso(Options, ConfigOptions)
					If dSearch.ShowDialog(tmpTitle, filterOptions) = Windows.Forms.DialogResult.OK Then
						If Not String.IsNullOrEmpty(Master.tmpMovie.IMDBID) Then
							DBMovie.Movie.IMDBID = Master.tmpMovie.IMDBID
						End If
						If Not String.IsNullOrEmpty(DBMovie.Movie.IMDBID) Then

							Master.currMovie.ClearExtras = True
							Master.currMovie.PosterPath = String.Empty
							Master.currMovie.FanartPath = String.Empty
							Master.currMovie.TrailerPath = String.Empty
							Master.currMovie.ExtraPath = String.Empty
							Master.currMovie.SubPath = String.Empty
							Master.currMovie.NfoPath = String.Empty


							_TMDBg.GetMovieInfo(DBMovie.Movie.ID, DBMovie.Movie, filterOptions.bFullCrew, filterOptions.bFullCast, False, filterOptions, False)
						End If
					Else
						Return New Interfaces.ModuleResult With {.breakChain = False, .Cancelled = True}
					End If
				End Using
			End If
		End If

		If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
			tTitle = StringUtils.FilterTokens(DBMovie.Movie.Title)
			If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = tTitle
			If Master.eSettings.DisplayYear AndAlso Not String.IsNullOrEmpty(DBMovie.Movie.Year) Then
				DBMovie.ListTitle = String.Format("{0} ({1})", tTitle, DBMovie.Movie.Year)
			Else
				DBMovie.ListTitle = tTitle
			End If
		Else
			If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
				DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name)
			ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
				DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name)
			Else
				If DBMovie.UseFolder AndAlso DBMovie.isSingle Then
					DBMovie.ListTitle = StringUtils.FilterName(Directory.GetParent(DBMovie.Filename).Name)
				Else
					DBMovie.ListTitle = StringUtils.FilterName(Path.GetFileNameWithoutExtension(DBMovie.Filename))
				End If
			End If
			If Not OldTitle = DBMovie.Movie.Title OrElse String.IsNullOrEmpty(DBMovie.Movie.SortTitle) Then DBMovie.Movie.SortTitle = DBMovie.ListTitle
		End If

		Return New Interfaces.ModuleResult With {.breakChain = False}
	End Function

	Function SelectImageOfType(ByRef mMovie As Structures.DBMovie, ByVal _DLType As Enums.ImageType, ByRef pResults As Containers.ImgResult, Optional ByVal _isEdit As Boolean = False, Optional ByVal preload As Boolean = False) As Interfaces.ModuleResult Implements Interfaces.EmberMovieScraperModule.SelectImageOfType
		If String.IsNullOrEmpty(mMovie.Movie.TMDBID) Then
			_TMDBg.GetMovieID(mMovie)
		End If
		If preload AndAlso _DLType = Enums.ImageType.Fanart AndAlso Not IsNothing(dFImgSelect) Then
			pResults = dFImgSelect.ShowDialog()
			dFImgSelect = Nothing
		Else
			Using dImgSelect As New dlgImgSelect(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _MySettings)
				If preload Then
					dFImgSelect = New dlgImgSelect(_TMDBConf, _TMDBConfE, _TMDBApi, _TMDBApiE, _MySettings)
					dFImgSelect.PreLoad(mMovie, Enums.ImageType.Fanart, _isEdit)
				End If
				''	dImgSelect.IMDBURL = MySettings.IMDBURL
				pResults = dImgSelect.ShowDialog(mMovie, _DLType, _isEdit)
			End Using
		End If
		Return New Interfaces.ModuleResult With {.breakChain = False}
	End Function
	Public Sub PostScraperOrderChanged() Implements EmberAPI.Interfaces.EmberMovieScraperModule.PostScraperOrderChanged
		_setup.orderChanged()
	End Sub

	Public Sub ScraperOrderChanged() Implements EmberAPI.Interfaces.EmberMovieScraperModule.ScraperOrderChanged
		_setupPost.orderChanged()
	End Sub

#End Region	'Methods

#Region "Nested Types"

	Structure sMySettings

#Region "Fields"
		Dim DownloadTrailers As Boolean
		Dim TrailerTimeout As Integer
		Dim UseTMDBTrailer As Boolean
		Dim UseTMDBTrailerXBMC As Boolean
		Dim ManualETSize As String
		Dim UseTMDBTrailerPref As String
		Dim TMDBAPIKey As String
		Dim FANARTTVApiKey As String
		Dim TMDBLanguage As String
		Dim FallBackEng As Boolean
		Dim UseIMDB As Boolean
		Dim UseIMPA As Boolean
		Dim UseMPDB As Boolean
		Dim UseFANARTTV As Boolean
		Dim UseIMDBTrailer As Boolean
#End Region	'Fields

	End Structure

#End Region	'Nested Types

End Class