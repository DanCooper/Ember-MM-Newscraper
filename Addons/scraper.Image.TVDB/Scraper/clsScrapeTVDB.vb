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
Imports System.IO.Compression
Imports System.Text
Imports System.Text.RegularExpressions
Imports EmberAPI
Imports NLog
Imports System.Diagnostics

Namespace TVDBs

    Public Class Scraper

#Region "Fields"
        Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

        Friend WithEvents bwTVDB As New System.ComponentModel.BackgroundWorker

#End Region 'Fields

#Region "Events"

        '		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

        '		Public Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

        Public Function GetImages_TV(ByVal tvdbID As String, ByVal Type As Enums.ScraperCapabilities_TV, ByRef Settings As sMySettings_ForScraper) As List(Of MediaContainers.Image)
            Dim alPosters As New List(Of MediaContainers.Image) 'main poster list
            Dim alPostersP As New List(Of MediaContainers.Image) 'preferred language poster list
            Dim alPostersE As New List(Of MediaContainers.Image) 'english poster list
            Dim alPostersO As New List(Of MediaContainers.Image) 'all others poster list
            Dim alPostersOs As New List(Of MediaContainers.Image) 'all others sorted poster list
            Dim alPostersN As New List(Of MediaContainers.Image) 'neutral/none language poster list (lang="00")

            Try
                Dim tvdbAPI = New TVDB.Web.WebInterface("353783CE455412FD")
                Dim tvdbMirror As New TVDB.Model.Mirror With {.Address = "http://thetvdb.com", .ContainsBannerFile = True, .ContainsXmlFile = True, .ContainsZipFile = False}

                Dim Results As TVDB.Model.SeriesDetails = tvdbAPI.GetFullSeriesById(CInt(tvdbID), Settings.PrefLanguage, tvdbMirror).Result
                If Results Is Nothing Then
                    Return Nothing
                End If
                If bwTVDB.CancellationPending Then Return Nothing

                'Banner AllSeasons/Show
                If Type = Enums.ScraperCapabilities_TV.AllSeasonsBanner OrElse Type = Enums.ScraperCapabilities_TV.ShowBanner Then
                    If Results.Banners Is Nothing Then Return alPosters
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.series)
                        Dim img As New MediaContainers.Image With {.Height = "140", _
                                                                   .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                   .Season = image.Season, _
                                                                   .ShortLang = image.Language, _
                                                                   .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                   .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                   .VoteAverage = CStr(image.Rating), _
                                                                   .VoteCount = image.RatingCount, _
                                                                   .Width = "758"}
                        If img.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(img)
                        ElseIf img.ShortLang = "en" AndAlso Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                            alPostersE.Add(img)
                        ElseIf Not Settings.PrefLanguageOnly Then
                            alPostersO.Add(img)
                        End If
                    Next

                    'Banner Season
                ElseIf Type = Enums.ScraperCapabilities_TV.SeasonBanner Then
                    If Results.Banners Is Nothing Then Return alPosters
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso f.BannerPath.Contains("seasonswide"))
                        Dim img As New MediaContainers.Image With {.Height = "140", _
                                                                   .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                   .Season = image.Season, _
                                                                   .ShortLang = image.Language, _
                                                                   .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                   .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                   .VoteAverage = CStr(image.Rating), _
                                                                   .VoteCount = image.RatingCount, _
                                                                   .Width = "758"}
                        If img.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(img)
                        ElseIf img.ShortLang = "en" AndAlso Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                            alPostersE.Add(img)
                        ElseIf Not Settings.PrefLanguageOnly Then
                            alPostersO.Add(img)
                        End If
                    Next

                    'Fanart AllSeasons/Season/Show
                ElseIf Type = Enums.ScraperCapabilities_TV.AllSeasonsFanart OrElse Type = Enums.ScraperCapabilities_TV.SeasonFanart OrElse Type = Enums.ScraperCapabilities_TV.ShowFanart Then
                    If Results.Banners Is Nothing Then Return alPosters
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.fanart)
                        alPosters.Add(New MediaContainers.Image With {.Height = StringUtils.StringToSize(image.Dimension).Height.ToString, _
                                                                      .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                      .Season = image.Season, _
                                                                      .ShortLang = image.Language, _
                                                                      .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                      .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                      .VoteAverage = CStr(image.Rating), _
                                                                      .VoteCount = image.RatingCount, _
                                                                      .Width = StringUtils.StringToSize(image.Dimension).Width.ToString})
                    Next

                    'Poster AllSeasons/Show
                ElseIf Type = Enums.ScraperCapabilities_TV.AllSeasonsPoster OrElse Type = Enums.ScraperCapabilities_TV.ShowPoster Then
                    If Results.Banners Is Nothing Then Return alPosters
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.poster)
                        Dim img As New MediaContainers.Image With {.Height = StringUtils.StringToSize(image.Dimension).Height.ToString, _
                                                                      .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                      .Season = image.Season, _
                                                                      .ShortLang = image.Language, _
                                                                      .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                      .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                      .VoteAverage = CStr(image.Rating), _
                                                                      .VoteCount = image.RatingCount, _
                                                                      .Width = StringUtils.StringToSize(image.Dimension).Width.ToString}
                        If img.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(img)
                        ElseIf img.ShortLang = "en" AndAlso Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                            alPostersE.Add(img)
                        ElseIf Not Settings.PrefLanguageOnly Then
                            alPostersO.Add(img)
                        End If
                    Next

                    'Poster Season
                ElseIf Type = Enums.ScraperCapabilities_TV.SeasonPoster Then
                    If Results.Banners Is Nothing Then Return alPosters
                    For Each image As TVDB.Model.Banner In Results.Banners.Where(Function(f) f.Type = TVDB.Model.BannerTyp.season AndAlso Not f.BannerPath.Contains("seasonswide"))
                        Dim img As New MediaContainers.Image With {.Height = "578", _
                                                                   .LongLang = Localization.ISOGetLangByCode2(image.Language), _
                                                                   .Season = image.Season, _
                                                                   .ShortLang = image.Language, _
                                                                   .ThumbURL = If(Not String.IsNullOrEmpty(image.ThumbnailPath), String.Concat(tvdbMirror.Address, "/banners/", image.ThumbnailPath), String.Empty), _
                                                                   .URL = String.Concat(tvdbMirror.Address, "/banners/", image.BannerPath), _
                                                                   .VoteAverage = CStr(image.Rating), _
                                                                   .VoteCount = image.RatingCount, _
                                                                   .Width = "400"}
                        If img.ShortLang = Settings.PrefLanguage Then
                            alPostersP.Add(img)
                        ElseIf img.ShortLang = "en" AndAlso Not Settings.PrefLanguageOnly OrElse (Settings.PrefLanguageOnly AndAlso Settings.GetEnglishImages) Then
                            alPostersE.Add(img)
                        ElseIf Not Settings.PrefLanguageOnly Then
                            alPostersO.Add(img)
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

