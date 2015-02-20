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
            Dim ParentID As String

            Try
                FanartTv.API.Key = "ea68f9d0847c1b7643813c70cbfc0196"
                FanartTv.API.cKey = Settings.ApiKey

                Dim Results = New FanartTv.Movies.Movie(imdbID_tmdbID)
                If IsNothing(Results) OrElse FanartTv.API.ErrorOccurred Then
                    If Not IsNothing(FanartTv.API.ErrorMessage) Then
                        logger.Error(FanartTv.API.ErrorMessage)
                    End If
                    Return Nothing
                End If
                If bwFANARTTV.CancellationPending Then Return Nothing

                'Banner
                If Type = Enums.ScraperCapabilities_Movie_MovieSet.Banner Then
                    If IsNothing(Results.List.Moviebanner) Then Return alPosters
                    For Each image In Results.List.Moviebanner
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        End If
                    Next

                    'ClearArt
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.ClearArt Then
                    If ((IsNothing(Results.List.Hdmovieclearart) AndAlso Settings.ClearArtOnlyHD) OrElse (IsNothing(Results.List.Hdmovieclearart) AndAlso IsNothing(Results.List.Movieart))) Then Return alPosters
                    If Not IsNothing(Results.List.Hdmovieclearart) Then
                        For Each image In Results.List.Hdmovieclearart
                            ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                            If image.Lang = Settings.PrefLanguage Then
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            ElseIf image.Lang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang))})
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            ElseIf image.Lang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            End If
                        Next
                    End If
                    If Not IsNothing(Results.List.Movieart) AndAlso Not Settings.ClearArtOnlyHD Then
                        For Each image In Results.List.Movieart
                            ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                            If image.Lang = Settings.PrefLanguage Then
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "500", .Height = "281", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            ElseIf image.Lang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "500", .Height = "281", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            ElseIf image.Lang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "500", .Height = "281", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "500", .Height = "281", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            End If
                        Next
                    End If

                    'ClearLogo
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.ClearLogo Then
                    If ((IsNothing(Results.List.Hdmovielogo) AndAlso Settings.ClearLogoOnlyHD) OrElse (IsNothing(Results.List.Hdmovielogo) AndAlso IsNothing(Results.List.Movielogo))) Then Return alPosters
                    If Not IsNothing(Results.List.Hdmovielogo) Then
                        For Each image In Results.List.Hdmovielogo
                            ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                            If image.Lang = Settings.PrefLanguage Then
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "800", .Height = "310", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            ElseIf image.Lang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "800", .Height = "310", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            ElseIf image.Lang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "800", .Height = "310", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "800", .Height = "310", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            End If
                        Next
                    End If

                    If Not IsNothing(Results.List.Movielogo) AndAlso Not Settings.ClearLogoOnlyHD Then
                        For Each image In Results.List.Movielogo
                            ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                            If image.Lang = Settings.PrefLanguage Then
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "400", .Height = "155", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            ElseIf image.Lang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "400", .Height = "155", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            ElseIf image.Lang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "400", .Height = "155", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "400", .Height = "155", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            End If
                        Next
                    End If

                    'DiscArt
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.DiscArt Then
                    If IsNothing(Results.List.Moviedisc.Count) Then Return alPosters
                    For Each image In Results.List.Moviedisc
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1000", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Disc = CInt(image.Disc), .DiscType = image.DiscType})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Disc = CInt(image.Disc), .DiscType = image.DiscType})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1000", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Disc = CInt(image.Disc), .DiscType = image.DiscType})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Disc = CInt(image.Disc), .DiscType = image.DiscType})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1000", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Disc = CInt(image.Disc), .DiscType = image.DiscType})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Disc = CInt(image.Disc), .DiscType = image.DiscType})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1000", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Disc = CInt(image.Disc), .DiscType = image.DiscType})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Disc = CInt(image.Disc), .DiscType = image.DiscType})
                            End If
                        End If
                    Next

                    'Fanart
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.Fanart Then
                    If IsNothing(Results.List.Moviebackground) Then Return alPosters
                    For Each image In Results.List.Moviebackground
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        alPosters.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1920", .Height = "1080", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                        alPosters.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                    Next

                    'Landscape
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.Landscape Then
                    If IsNothing(Results.List.Moviethumb) Then Return alPosters
                    For Each image In Results.List.Moviethumb
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        End If
                    Next

                    'Poster
                ElseIf Type = Enums.ScraperCapabilities_Movie_MovieSet.Poster Then
                    If IsNothing(Results.List.Movieposter) Then Return alPosters
                    For Each image In Results.List.Movieposter
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(CInt(image.Likes))})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
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
            Dim ParentID As String

            Try
                FanartTv.API.Key = "ea68f9d0847c1b7643813c70cbfc0196"
                FanartTv.API.cKey = Settings.ApiKey

                Dim Results = New FanartTv.TV.Show(tvdbID)
                If IsNothing(Results) OrElse FanartTv.API.ErrorOccurred Then
                    If Not IsNothing(FanartTv.API.ErrorMessage) Then
                        logger.Error(FanartTv.API.ErrorMessage)
                    End If
                    Return Nothing
                End If
                If bwFANARTTV.CancellationPending Then Return Nothing

                'Banner AllSeasons/Show
                If Type = Enums.ScraperCapabilities_TV.AllSeasonsBanner OrElse Type = Enums.ScraperCapabilities_TV.ShowBanner Then
                    If IsNothing(Results.List.Tvbanner) Then Return alPosters
                    For Each image In Results.List.Tvbanner
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        End If
                    Next

                    'Banner Season
                ElseIf Type = Enums.ScraperCapabilities_TV.SeasonBanner Then
                    If IsNothing(Results.List.Seasonbanner) Then Return alPosters
                    For Each image In Results.List.Seasonbanner
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "185", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "37", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                            End If
                        End If
                    Next

                    'CharacterArt Show
                ElseIf Type = Enums.ScraperCapabilities_TV.ShowCharacterArt Then
                    If IsNothing(Results.List.Characterart) Then Return alPosters
                    For Each image In Results.List.Characterart
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "512", .Height = "512", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "512", .Height = "512", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "512", .Height = "512", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "512", .Height = "512", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "200", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        End If
                    Next

                    'ClearArt Show
                ElseIf Type = Enums.ScraperCapabilities_TV.ShowClearArt Then
                    If ((IsNothing(Results.List.Hdclearart) AndAlso Settings.ClearArtOnlyHD) OrElse (IsNothing(Results.List.Hdclearart) AndAlso IsNothing(Results.List.Clearart))) Then Return alPosters
                    If Not IsNothing(Results.List.Hdclearart) Then
                        For Each image In Results.List.Hdclearart
                            ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                            If image.Lang = Settings.PrefLanguage Then
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            ElseIf image.Lang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang))})
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            ElseIf image.Lang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "562", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            End If
                        Next
                    End If
                    If Not IsNothing(Results.List.Clearart) AndAlso Not Settings.ClearArtOnlyHD Then
                        For Each image In Results.List.Clearart
                            ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                            If image.Lang = Settings.PrefLanguage Then
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "500", .Height = "281", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            ElseIf image.Lang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "500", .Height = "281", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            ElseIf image.Lang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "500", .Height = "281", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "500", .Height = "281", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                End If
                            End If
                        Next
                    End If

                    'ClearLogo Show
                ElseIf Type = Enums.ScraperCapabilities_TV.ShowClearLogo Then
                    If ((IsNothing(Results.List.HdTListvlogo) AndAlso Settings.ClearLogoOnlyHD) OrElse (IsNothing(Results.List.HdTListvlogo) AndAlso IsNothing(Results.List.Clearlogo))) Then Return alPosters
                    If Not IsNothing(Results.List.HdTListvlogo) Then
                        For Each Image In Results.List.HdTListvlogo
                            ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                            If Image.Lang = Settings.PrefLanguage Then
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "800", .Height = "310", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                            ElseIf Image.Lang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "800", .Height = "310", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                End If
                            ElseIf Image.Lang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "800", .Height = "310", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "800", .Height = "310", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                End If
                            End If
                        Next
                    End If
                    If Not IsNothing(Results.List.Clearlogo) AndAlso Not Settings.ClearLogoOnlyHD Then
                        For Each Image In Results.List.Clearlogo
                            ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                            If Image.Lang = Settings.PrefLanguage Then
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "400", .Height = "155", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                            ElseIf Image.Lang = "en" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "400", .Height = "155", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                    alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                End If
                            ElseIf Image.Lang = "00" Then
                                If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "400", .Height = "155", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                    alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                End If
                            Else
                                If Not Settings.PrefLanguageOnly Then
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "400", .Height = "155", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                    alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "77", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                End If
                            End If
                        Next
                    End If

                    'Fanart AllSeasons/Season/Show
                ElseIf Type = Enums.ScraperCapabilities_TV.AllSeasonsFanart OrElse Type = Enums.ScraperCapabilities_TV.SeasonFanart OrElse Type = Enums.ScraperCapabilities_TV.ShowFanart Then
                    If IsNothing(Results.List.Showbackground) Then Return alPosters
                    For Each image In Results.List.Showbackground
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        alPosters.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1920", .Height = "1080", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                        alPosters.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                    Next

                    'Landscape AllSeasons/Show
                ElseIf Type = Enums.ScraperCapabilities_TV.AllSeasonsLandscape OrElse Type = Enums.ScraperCapabilities_TV.ShowLandscape Then
                    If IsNothing(Results.List.Tvthumb) Then Return alPosters
                    For Each Image In Results.List.Tvthumb
                        ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                        If Image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                        ElseIf Image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                            End If
                        ElseIf Image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes)})
                            End If
                        End If
                    Next

                    'Landscape Season
                ElseIf Type = Enums.ScraperCapabilities_TV.SeasonLandscape Then
                    If IsNothing(Results.List.Seasonthumb) Then Return alPosters
                    For Each Image In Results.List.Seasonthumb
                        ParentID = Image.Url.Substring(Image.Url.LastIndexOf("/") + 1, Image.Url.Length - (Image.Url.LastIndexOf("/") + 1))
                        If Image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes), .Season = CInt(Image.Season)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes), .Season = CInt(Image.Season)})
                        ElseIf Image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes), .Season = CInt(Image.Season)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes), .Season = CInt(Image.Season)})
                            End If
                        ElseIf Image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes), .Season = CInt(Image.Season)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes), .Season = CInt(Image.Season)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = Image.Url, .Width = "500", .Height = "281", .ThumbURL = Image.Url.Replace("/fanart/", "/preview/"), .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes), .Season = CInt(Image.Season)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = Image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "112", .ParentID = Image.Url, .ShortLang = Image.Lang, .LongLang = If(String.IsNullOrEmpty(Image.Lang), "", Localization.ISOGetLangByCode2(Image.Lang)), .Likes = CInt(Image.Likes), .Season = CInt(Image.Season)})
                            End If
                        End If
                    Next

                    'Poster AllSeasons/Show
                ElseIf Type = Enums.ScraperCapabilities_TV.AllSeasonsPoster OrElse Type = Enums.ScraperCapabilities_TV.ShowPoster Then
                    If IsNothing(Results.List.Tvposter) Then Return alPosters
                    For Each image In Results.List.Tvposter
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes)})
                            End If
                        End If
                    Next

                    'Poster Season
                ElseIf Type = Enums.ScraperCapabilities_TV.SeasonPoster Then
                    If IsNothing(Results.List.Seasonposter) Then Return alPosters
                    For Each image In Results.List.Seasonposter
                        ParentID = image.Url.Substring(image.Url.LastIndexOf("/") + 1, image.Url.Length - (image.Url.LastIndexOf("/") + 1))
                        If image.Lang = Settings.PrefLanguage Then
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                            alPostersP.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                        ElseIf image.Lang = "en" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                                alPostersE.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                            End If
                        ElseIf image.Lang = "00" Then
                            If Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetBlankImages) Then
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                                alPostersN.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                            End If
                        Else
                            If Not Settings.PrefLanguageOnly Then
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(3).description, .URL = image.Url, .Width = "1000", .Height = "1426", .ThumbURL = image.Url.Replace("/fanart/", "/preview/"), .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
                                alPostersO.Add(New MediaContainers.Image With {.Description = Master.eSize.backdrop_names(0).description, .URL = image.Url.Replace("/fanart/", "/preview/"), .Width = "200", .Height = "285", .ParentID = image.Url, .ShortLang = image.Lang, .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.ISOGetLangByCode2(image.Lang)), .Likes = CInt(image.Likes), .Season = CInt(image.Season)})
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

