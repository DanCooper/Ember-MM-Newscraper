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

        Friend WithEvents bwFANARTTV As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

        '		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

        '		Public Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

        'Public Sub New(ByRef sMySettings As FanartTV_Image.sMySettings)
        '    FanartTv.API.Key = "ea68f9d0847c1b7643813c70cbfc0196"
        '    FanartTv.API.cKey = sMySettings.ApiKey
        '    Dim Test = New FanartTv.Movies.Latest()
        '    If FanartTv.API.ErrorOccurred Then
        '        If Not IsNothing(FanartTv.API.ErrorMessage) Then
        '            logger.Error(FanartTv.API.ErrorMessage)
        '        End If
        '    End If
        'End Sub

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

        Public Function GetImages_Movie_MovieSet(ByVal imdbID_tmdbID As String, ByVal Type As Enums.ScraperCapabilities_Movie_MovieSet, ByRef Settings As sMySettings_ForScraper) As List(Of MediaContainers.Image)
            Dim alPosters As New List(Of MediaContainers.Image) 'main poster list
            Dim alPostersP As New List(Of MediaContainers.Image) 'preferred language poster list
            Dim alPostersE As New List(Of MediaContainers.Image) 'english poster list
            Dim alPostersO As New List(Of MediaContainers.Image) 'all others poster list
            Dim alPostersOs As New List(Of MediaContainers.Image) 'all others sorted poster list
            Dim alPostersN As New List(Of MediaContainers.Image) 'neutral/none language poster list (lang="00")

            Try
                FanartTv.API.Key = "ea68f9d0847c1b7643813c70cbfc0196"
                FanartTv.API.cKey = Settings.ApiKey

                Dim Results = New FanartTv.Movies.Movie(imdbID_tmdbID)
                If Results Is Nothing OrElse FanartTv.API.ErrorOccurred Then
                    If FanartTv.API.ErrorMessage IsNot Nothing Then
                        logger.Error(FanartTv.API.ErrorMessage)
                    End If
                    Return Nothing
                End If
                If bwFANARTTV.CancellationPending Then Return Nothing

                'Banner
                If Type = Enums.ScraperCapabilities_Movie_MovieSet.Banner Then
                    If Results.List.Moviebanner Is Nothing Then Return alPosters
                    For Each image In Results.List.Moviebanner
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "185", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .URL = image.Url, _
                            .Width = "1000"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'ClearArt
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.ClearArt Then
                    If ((Results.List.Hdmovieclearart Is Nothing AndAlso Settings.ClearArtOnlyHD) OrElse (Results.List.Hdmovieclearart Is Nothing AndAlso Results.List.Movieart Is Nothing)) Then Return alPosters
                    If Results.List.Hdmovieclearart IsNot Nothing Then
                        For Each image In Results.List.Hdmovieclearart
                            Dim tmpPoster As New MediaContainers.Image With { _
                                .Height = "562", _
                                .Likes = CInt(image.Likes), _
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                                .ShortLang = image.Lang, _
                                .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                                .URL = image.Url, _
                                .Width = "1000"}

                            If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                alPostersP.Add(tmpPoster)
                            ElseIf tmpPoster.ShortLang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(tmpPoster)
                                End If
                            ElseIf tmpPoster.ShortLang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(tmpPoster)
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(tmpPoster)
                                End If
                            End If
                        Next
                    End If
                    If Results.List.Movieart IsNot Nothing AndAlso Not Settings.ClearArtOnlyHD Then
                        For Each image In Results.List.Movieart
                            Dim tmpPoster As New MediaContainers.Image With { _
                                .Height = "281", _
                                .Likes = CInt(image.Likes), _
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                                .ShortLang = image.Lang, _
                                .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                                .URL = image.Url, _
                                .Width = "500"}

                            If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                alPostersP.Add(tmpPoster)
                            ElseIf tmpPoster.ShortLang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(tmpPoster)
                                End If
                            ElseIf tmpPoster.ShortLang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(tmpPoster)
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(tmpPoster)
                                End If
                            End If
                        Next
                    End If

                    'ClearLogo
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.ClearLogo Then
                    If ((Results.List.Hdmovielogo Is Nothing AndAlso Settings.ClearLogoOnlyHD) OrElse (Results.List.Hdmovielogo Is Nothing AndAlso Results.List.Movielogo Is Nothing)) Then Return alPosters
                    If Results.List.Hdmovielogo IsNot Nothing Then
                        For Each image In Results.List.Hdmovielogo
                            Dim tmpPoster As New MediaContainers.Image With { _
                                .Height = "310", _
                                .Likes = CInt(image.Likes), _
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                                .ShortLang = image.Lang, _
                                .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                                .URL = image.Url, _
                                .Width = "800"}

                            If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                alPostersP.Add(tmpPoster)
                            ElseIf tmpPoster.ShortLang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(tmpPoster)
                                End If
                            ElseIf tmpPoster.ShortLang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(tmpPoster)
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(tmpPoster)
                                End If
                            End If
                        Next
                    End If

                    If Results.List.Movielogo IsNot Nothing AndAlso Not Settings.ClearLogoOnlyHD Then
                        For Each image In Results.List.Movielogo
                            Dim tmpPoster As New MediaContainers.Image With { _
                                .Height = "155", _
                                .Likes = CInt(image.Likes), _
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                                .ShortLang = image.Lang, _
                                .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                                .URL = image.Url, _
                                .Width = "400"}

                            If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                alPostersP.Add(tmpPoster)
                            ElseIf tmpPoster.ShortLang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(tmpPoster)
                                End If
                            ElseIf tmpPoster.ShortLang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(tmpPoster)
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(tmpPoster)
                                End If
                            End If
                        Next
                    End If

                    'DiscArt
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.DiscArt Then
                    If Results.List.Moviedisc Is Nothing Then Return alPosters
                    For Each image In Results.List.Moviedisc
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Disc = CInt(image.Disc), _
                            .DiscType = image.DiscType, _
                            .Height = "1000", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .URL = image.Url, _
                            .Width = "1000"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'Fanart
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.Fanart Then
                    If Results.List.Moviebackground Is Nothing Then Return alPosters
                    For Each image In Results.List.Moviebackground
                        alPosters.Add(New MediaContainers.Image With {.URL = image.Url, .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .Width = "1920", .Height = "1080", .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                    Next

                    'Landscape
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.Landscape Then
                    If Results.List.Moviethumb Is Nothing Then Return alPosters
                    For Each image In Results.List.Moviethumb
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "562", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .URL = image.Url, _
                            .Width = "1000"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'Poster
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.Poster Then
                    If Results.List.Movieposter Is Nothing Then Return alPosters
                    For Each image In Results.List.Movieposter
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "1426", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .URL = image.Url, _
                            .Width = "1000"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
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

        Public Function GetImages_TV(ByVal tvdbID As String, ByVal Type As Enums.ScraperCapabilities_TV, ByRef Settings As sMySettings_ForScraper) As List(Of MediaContainers.Image)
            Dim alPosters As New List(Of MediaContainers.Image) 'main poster list
            Dim alPostersP As New List(Of MediaContainers.Image) 'preferred language poster list
            Dim alPostersE As New List(Of MediaContainers.Image) 'english poster list
            Dim alPostersO As New List(Of MediaContainers.Image) 'all others poster list
            Dim alPostersOs As New List(Of MediaContainers.Image) 'all others sorted poster list
            Dim alPostersN As New List(Of MediaContainers.Image) 'neutral/none language poster list (lang="00")

            Try
                FanartTv.API.Key = "ea68f9d0847c1b7643813c70cbfc0196"
                FanartTv.API.cKey = Settings.ApiKey

                Dim Results = New FanartTv.TV.Show(tvdbID)
                If Results Is Nothing OrElse FanartTv.API.ErrorOccurred Then
                    If FanartTv.API.ErrorMessage IsNot Nothing Then
                        logger.Error(FanartTv.API.ErrorMessage)
                    End If
                    Return Nothing
                End If
                If bwFANARTTV.CancellationPending Then Return Nothing

                'Banner AllSeasons/Show
                If Type = Enums.ScraperCapabilities_TV.AllSeasonsBanner OrElse Type = Enums.ScraperCapabilities_TV.ShowBanner Then
                    If Results.List.Tvbanner Is Nothing Then Return alPosters
                    For Each image In Results.List.Tvbanner
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "185", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .TVBannerType = Enums.TVBannerType.Any, _
                            .URL = image.Url, _
                            .Width = "1000"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'Banner Season
                ElseIf Type = Enums.ScraperCapabilities_TV.SeasonBanner Then
                    If Results.List.Seasonbanner Is Nothing Then Return alPosters
                    For Each image In Results.List.Seasonbanner
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "185", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .Season = If(image.Season = "all", 999, CInt(image.Season)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .TVBannerType = Enums.TVBannerType.Any, _
                            .URL = image.Url, _
                            .Width = "1000"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'CharacterArt Show
                ElseIf Type = Enums.ScraperCapabilities_TV.ShowCharacterArt Then
                    If Results.List.Characterart Is Nothing Then Return alPosters
                    For Each image In Results.List.Characterart
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "512", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .URL = image.Url, _
                            .Width = "512"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'ClearArt Show
                ElseIf Type = Enums.ScraperCapabilities_TV.ShowClearArt Then
                    If ((Results.List.Hdclearart Is Nothing AndAlso Settings.ClearArtOnlyHD) OrElse (Results.List.Hdclearart Is Nothing AndAlso Results.List.Clearart Is Nothing)) Then Return alPosters
                    If Results.List.Hdclearart IsNot Nothing Then
                        For Each image In Results.List.Hdclearart
                            Dim tmpPoster As New MediaContainers.Image With { _
                                .Height = "562", _
                                .Likes = CInt(image.Likes), _
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                                .ShortLang = image.Lang, _
                                .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                                .URL = image.Url, _
                                .Width = "1000"}

                            If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                alPostersP.Add(tmpPoster)
                            ElseIf tmpPoster.ShortLang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(tmpPoster)
                                End If
                            ElseIf tmpPoster.ShortLang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(tmpPoster)
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(tmpPoster)
                                End If
                            End If
                        Next
                    End If
                    If Results.List.Clearart IsNot Nothing AndAlso Not Settings.ClearArtOnlyHD Then
                        For Each image In Results.List.Clearart
                            Dim tmpPoster As New MediaContainers.Image With { _
                                .Height = "281", _
                                .Likes = CInt(image.Likes), _
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                                .ShortLang = image.Lang, _
                                .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                                .URL = image.Url, _
                                .Width = "500"}

                            If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                alPostersP.Add(tmpPoster)
                            ElseIf tmpPoster.ShortLang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(tmpPoster)
                                End If
                            ElseIf tmpPoster.ShortLang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(tmpPoster)
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(tmpPoster)
                                End If
                            End If
                        Next
                    End If

                    'ClearLogo Show
                ElseIf Type = Enums.ScraperCapabilities_TV.ShowClearLogo Then
                    If ((Results.List.HdTListvlogo Is Nothing AndAlso Settings.ClearLogoOnlyHD) OrElse (Results.List.HdTListvlogo Is Nothing AndAlso Results.List.Clearlogo Is Nothing)) Then Return alPosters
                    If Results.List.HdTListvlogo IsNot Nothing Then
                        For Each Image In Results.List.HdTListvlogo
                            Dim tmpPoster As New MediaContainers.Image With { _
                                .Height = "310", _
                                .Likes = CInt(Image.Likes), _
                                .LongLang = If(String.IsNullOrEmpty(Image.Lang), String.Empty, Localization.ISOGetLangByCode2(Image.Lang)), _
                                .ShortLang = Image.Lang, _
                                .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), _
                                .URL = Image.Url, _
                                .Width = "800"}

                            If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                alPostersP.Add(tmpPoster)
                            ElseIf tmpPoster.ShortLang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(tmpPoster)
                                End If
                            ElseIf tmpPoster.ShortLang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(tmpPoster)
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(tmpPoster)
                                End If
                            End If
                        Next
                    End If
                    If Results.List.Clearlogo IsNot Nothing AndAlso Not Settings.ClearLogoOnlyHD Then
                        For Each Image In Results.List.Clearlogo
                            Dim tmpPoster As New MediaContainers.Image With { _
                                .Height = "155", _
                                .Likes = CInt(Image.Likes), _
                                .LongLang = If(String.IsNullOrEmpty(Image.Lang), String.Empty, Localization.ISOGetLangByCode2(Image.Lang)), _
                                .ShortLang = Image.Lang, _
                                .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), _
                                .URL = Image.Url, _
                                .Width = "400"}

                            If tmpPoster.ShortLang = Settings.PrefLanguage Then
                                alPostersP.Add(tmpPoster)
                            ElseIf tmpPoster.ShortLang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(tmpPoster)
                                End If
                            ElseIf tmpPoster.ShortLang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(tmpPoster)
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(tmpPoster)
                                End If
                            End If
                        Next
                    End If

                    'Fanart AllSeasons/Season/Show
                ElseIf Type = Enums.ScraperCapabilities_TV.AllSeasonsFanart OrElse Type = Enums.ScraperCapabilities_TV.SeasonFanart OrElse Type = Enums.ScraperCapabilities_TV.ShowFanart Then
                    If Results.List.Showbackground Is Nothing Then Return alPosters
                    For Each image In Results.List.Showbackground
                        alPosters.Add(New MediaContainers.Image With {.URL = image.Url, .Width = "1920", .Height = "1080", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                    Next

                    'Landscape AllSeasons/Show
                ElseIf Type = Enums.ScraperCapabilities_TV.AllSeasonsLandscape OrElse Type = Enums.ScraperCapabilities_TV.ShowLandscape Then
                    If Results.List.Tvthumb Is Nothing Then Return alPosters
                    For Each Image In Results.List.Tvthumb
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "281", _
                            .Likes = CInt(Image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(Image.Lang), String.Empty, Localization.ISOGetLangByCode2(Image.Lang)), _
                            .ShortLang = Image.Lang, _
                            .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), _
                            .URL = Image.Url, _
                            .Width = "500"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'Landscape Season
                ElseIf Type = Enums.ScraperCapabilities_TV.SeasonLandscape Then
                    If Results.List.Seasonthumb Is Nothing Then Return alPosters
                    For Each Image In Results.List.Seasonthumb
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "281", _
                            .Likes = CInt(Image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(Image.Lang), String.Empty, Localization.ISOGetLangByCode2(Image.Lang)), _
                            .Season = If(Image.Season = "all", 999, CInt(Image.Season)), _
                            .ShortLang = Image.Lang, _
                            .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), _
                            .TVBannerType = Enums.TVBannerType.Any, _
                            .URL = Image.Url, _
                            .Width = "500"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'Poster AllSeasons/Show
                ElseIf Type = Enums.ScraperCapabilities_TV.AllSeasonsPoster OrElse Type = Enums.ScraperCapabilities_TV.ShowPoster Then
                    If Results.List.Tvposter Is Nothing Then Return alPosters
                    For Each image In Results.List.Tvposter
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "1426", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .TVBannerType = Enums.TVBannerType.Any, _
                            .URL = image.Url, _
                            .Width = "1000"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
                        End If
                    Next

                    'Poster Season
                ElseIf Type = Enums.ScraperCapabilities_TV.SeasonPoster Then
                    If Results.List.Seasonposter Is Nothing Then Return alPosters
                    For Each image In Results.List.Seasonposter
                        Dim tmpPoster As New MediaContainers.Image With { _
                            .Height = "1426", _
                            .Likes = CInt(image.Likes), _
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.ISOGetLangByCode2(image.Lang)), _
                            .Season = If(image.Season = "all", 999, CInt(image.Season)), _
                            .ShortLang = image.Lang, _
                            .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), _
                            .TVBannerType = Enums.TVBannerType.Any, _
                            .URL = image.Url, _
                            .Width = "1000"}

                        If tmpPoster.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(tmpPoster)
                        ElseIf tmpPoster.ShortLang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(tmpPoster)
                            End If
                        ElseIf tmpPoster.ShortLang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(tmpPoster)
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(tmpPoster)
                            End If
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

