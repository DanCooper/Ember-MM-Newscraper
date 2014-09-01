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
Imports NLog
Imports System.Diagnostics

Namespace FanartTVs

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
        Private _MySettings As FanartTV_Image.sMySettings
        Private _FanartTV As FanartTv.API
        Private _APIInvalid As Boolean = False

        Friend WithEvents bwFANARTTV As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

        '#Region "Events"

        '		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

        '		Public Event ProgressUpdated(ByVal iPercent As Integer)

        '#End Region	'Events

#Region "Methods"

        Public Sub New(ByRef tMySettings As FanartTV_Image.sMySettings)
            _MySettings = tMySettings
            _FanartTV = New FanartTv.API(_MySettings.ApiKey)
            'Dim Result As FanartTV.V1.FanartTVMovie = _FanartTV.GetMovieInfo(New FanartTV.V1.FanartTVMovieRequest("1", "JSON", "all", 1, 1))
            'If IsNothing(Result) Then
            '    If Not IsNothing(_FanartTV.Error) Then
            '        logger.Error(_FanartTV.Error)
            _APIInvalid = False
            '    End If
            'End If
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
        '		logger.Error(New StackFrame().GetMethod().Name,ex)
        '	End Try
        'End Sub

        Public Function GetImages_Movie_MovieSet(ByVal imdbID_tmdbID As String, ByVal Type As Enums.ScraperCapabilities, ByRef Settings As sMySettings_ForScraper) As List(Of MediaContainers.Image)
            Dim alPosters As New List(Of MediaContainers.Image) 'main poster list
            Dim alPostersP As New List(Of MediaContainers.Image) 'preferred language poster list
            Dim alPostersE As New List(Of MediaContainers.Image) 'english poster list
            Dim alPostersO As New List(Of MediaContainers.Image) 'all others poster list
            Dim alPostersOs As New List(Of MediaContainers.Image) 'all others sorted poster list
            Dim alPostersN As New List(Of MediaContainers.Image) 'neutral/none language poster list (lang="00")
            Dim ParentID As String

            If _APIInvalid Then
                Return Nothing
            End If
            Try
                Dim Results = New FanartTv.Movie(imdbID_tmdbID, Settings.ApiKey)
                If bwFANARTTV.CancellationPending Then Return Nothing

                'Poster
                If Type = Enums.ScraperCapabilities.Poster Then
                    If IsNothing(Results) Then Return alPosters
                    If Results.List.Count = 0 Then Return alPosters
                    For Each Result In Results.List
                        If Result.Value.Poster.Count > 0 Then
                            For Each Image In Result.Value.Poster
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                If Image.Lang = Settings.PrefLanguage Then
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "1426", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                ElseIf Image.Lang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "1426", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                ElseIf Image.Lang = "00" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "1426", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "1426", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                End If
                            Next
                        End If
                    Next

                    'Fanart
                ElseIf Type = Enums.ScraperCapabilities.Fanart Then
                    If IsNothing(Results) Then Return alPosters
                    If Results.List.Count = 0 Then Return alPosters
                    For Each Result In Results.List
                        If Result.Value.Poster.Count > 0 Then
                            For Each Image In Result.Value.Background
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                alPosters.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1920", .Height = "1080", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                alPosters.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                            Next
                        End If
                    Next

                    'Banner
                ElseIf Type = Enums.ScraperCapabilities.Banner Then
                    If IsNothing(Results) Then Return alPosters
                    If Results.List.Count = 0 Then Return alPosters
                    For Each Result In Results.List
                        If Result.Value.Banner.Count > 0 Then
                            For Each Image In Result.Value.Banner
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                If Image.Lang = Settings.PrefLanguage Then
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "185", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                ElseIf Image.Lang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "185", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                ElseIf Image.Lang = "00" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "185", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "185", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                End If
                            Next
                        End If
                    Next

                    'CharacterArt
                ElseIf Type = Enums.ScraperCapabilities.CharacterArt Then

                    'ClearArt
                ElseIf Type = Enums.ScraperCapabilities.ClearArt Then
                    'If IsNothing(Result) Then Return alPosters
                    'If (IsNothing(Result.movieinfo.hdmovieclearart) AndAlso Settings.ClearArtOnlyHD) OrElse (IsNothing(Result.movieinfo.hdmovieclearart) AndAlso IsNothing(Result.movieinfo.movieart)) Then Return alPosters
                    'If Not IsNothing(Result.movieinfo.hdmovieclearart) Then
                    '    For Each Image In Result.movieinfo.hdmovieclearart
                    If IsNothing(Results) Then Return alPosters
                    If Results.List.Count = 0 Then Return alPosters
                    For Each Result In Results.List
                        If Result.Value.HD_Clearart.Count > 0 Then
                            For Each Image In Result.Value.HD_Clearart
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                If Image.Lang = Settings.PrefLanguage Then
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "562", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                ElseIf Image.Lang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "562", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                ElseIf Image.Lang = "00" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "562", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "562", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                End If
                            Next
                        End If
                        'If Not IsNothing(Result.movieinfo.movieart) AndAlso Not Settings.ClearArtOnlyHD Then
                        If Result.Value.Art.Count > 0 AndAlso Not Settings.ClearArtOnlyHD Then
                            For Each Image In Result.Value.Art
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                If Image.Lang = Settings.PrefLanguage Then
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                ElseIf Image.Lang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                ElseIf Image.Lang = "00" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                End If
                            Next
                        End If
                    Next

                    'ClearLogo
                ElseIf Type = Enums.ScraperCapabilities.ClearLogo Then
                    'If IsNothing(Result) Then Return alPosters
                    'If (IsNothing(Result.movieinfo.hdmovielogo) AndAlso Settings.ClearLogoOnlyHD) OrElse (IsNothing(Result.movieinfo.hdmovielogo) AndAlso IsNothing(Result.movieinfo.movielogo)) Then Return alPosters

                    If IsNothing(Results) Then Return alPosters
                    If Results.List.Count = 0 Then Return alPosters
                    For Each Result In Results.List
                        If Result.Value.HD_Logo.Count > 0 Then
                            For Each Image In Result.Value.HD_Logo
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                If Image.Lang = Settings.PrefLanguage Then
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "800", .Height = "310", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                ElseIf Image.Lang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "800", .Height = "310", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                ElseIf Image.Lang = "00" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "800", .Height = "310", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "800", .Height = "310", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                End If
                            Next
                        End If

                        If Result.Value.Logo.Count > 0 AndAlso Not Settings.ClearLogoOnlyHD Then
                            For Each Image In Result.Value.Logo
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                If Image.Lang = Settings.PrefLanguage Then
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "400", .Height = "155", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                ElseIf Image.Lang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "400", .Height = "155", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                ElseIf Image.Lang = "00" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "400", .Height = "155", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "400", .Height = "155", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                End If
                            Next
                        End If
                    Next

                    'DiscArt
                ElseIf Type = Enums.ScraperCapabilities.DiscArt Then
                    If IsNothing(Results) Then Return alPosters
                    If Results.List.Count = 0 Then Return alPosters
                    For Each Result In Results.List
                        If Result.Value.Disc.Count > 0 Then
                            For Each Image In Result.Value.Disc
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                If Image.Lang = Settings.PrefLanguage Then
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "1000", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                ElseIf Image.Lang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "1000", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                ElseIf Image.Lang = "00" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "1000", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "1000", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                End If
                            Next
                        End If
                    Next

                    'Landscape
                ElseIf Type = Enums.ScraperCapabilities.Landscape Then
                    If IsNothing(Results) Then Return alPosters
                    If Results.List.Count = 0 Then Return alPosters
                    For Each Result In Results.List
                        If Result.Value.Thumb.Count > 0 Then
                            For Each Image In Result.Value.Thumb
                                ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                                If Image.Lang = Settings.PrefLanguage Then
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "562", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                ElseIf Image.Lang = "en" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "562", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                ElseIf Image.Lang = "00" Then
                                    If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "562", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                Else
                                    If Not Settings.PrefLanguageOnly Then
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "1000", .Height = "562", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                        alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang))})
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            Catch ex As Exception
                logger.Error(New StackFrame().GetMethod().Name, ex)
            End Try

            'Image sorting
            For Each xPoster As MediaContainers.Image In alPostersO.OrderBy(Function(p) (p.LongLang))
                alPostersOs.Add(xPoster)
            Next
            alPosters.AddRange(alPostersP)
            alPosters.AddRange(alPostersE)
            alPosters.AddRange(alPostersOs)
            alPosters.AddRange(alPostersN)

            Return alPosters
        End Function

        'Private Sub bwFANARTTVA_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwFANARTTV.DoWork
        '	Dim Args As Arguments = DirectCast(e.Argument, Arguments)
        '	Try
        '		e.Result = GetFANARTTVImages(Args.Parameter)
        '	Catch ex As Exception
        '		logger.Error(New StackFrame().GetMethod().Name,ex)
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

#End Region 'Fields

        End Structure

        Private Structure Results

#Region "Fields"

            Dim Result As Object
            Dim ResultList As List(Of MediaContainers.Image)

#End Region 'Fields

        End Structure

        Structure sMySettings_ForScraper

#Region "Fields"
            Dim ApiKey As String
            Dim ClearArtOnlyHD As Boolean
            Dim ClearLogoOnlyHD As Boolean
            Dim GetEnglishImages As Boolean
            Dim GetBlankImages As Boolean
            Dim PrefLanguage As String
            Dim PrefLanguageOnly As Boolean
#End Region 'Fields

        End Structure

#End Region 'Nested Types

    End Class

End Namespace

