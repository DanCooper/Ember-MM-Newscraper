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

Imports EmberAPI
Imports NLog
Imports System.Diagnostics

Public Class Scraper

#Region "Fields"

        Shared logger As Logger = LogManager.GetCurrentClassLogger()

        Friend WithEvents bwFANARTTV As New System.ComponentModel.BackgroundWorker

    Private _SpecialSettings As Addon.AddonSettings

#End Region 'Fields

#Region "Events"

    '		Public Event PostersDownloaded(ByVal Posters As List(Of MediaContainers.Image))

    '		Public Event ProgressUpdated(ByVal iPercent As Integer)

#End Region 'Events

#Region "Methods"

    Public Sub New(ByVal SpecialSettings As Addon.AddonSettings)
        Try
            _SpecialSettings = SpecialSettings

            FanartTv.API.Key = "ea68f9d0847c1b7643813c70cbfc0196"
            FanartTv.API.cKey = SpecialSettings.ApiKey

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Function GetImages_Movie_MovieSet(ByVal imdbIdOrTmdbId As String, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
            Dim alImagesContainer As New MediaContainers.SearchResultsContainer

            Try
                Dim Results = New FanartTv.Movies.Movie(imdbIdOrTmdbId)
                If Results Is Nothing OrElse FanartTv.API.ErrorOccurred Then
                    If FanartTv.API.ErrorMessage IsNot Nothing Then
                        logger.Error(FanartTv.API.ErrorMessage)
                    End If
                    Return alImagesContainer
                End If
                If bwFANARTTV.CancellationPending Then Return alImagesContainer

                'Banner
                If FilteredModifiers.MainBanner AndAlso Results.List.Moviebanner IsNot Nothing Then
                    For Each image In Results.List.Moviebanner
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "185",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .ShortLang = image.Lang,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "1000"
                        }

                        alImagesContainer.MainBanners.Add(tmpPoster)
                    Next
                End If

                'ClearArt
                If FilteredModifiers.MainClearArt Then
                    If Results.List.Hdmovieclearart IsNot Nothing Then
                        For Each image In Results.List.Hdmovieclearart
                            Dim tmpPoster As New MediaContainers.Image With {
                                .Height = "562",
                                .Likes = CInt(image.Likes),
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                                .Scraper = "Fanart.tv",
                                .ShortLang = image.Lang,
                                .URLOriginal = image.Url,
                                .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                                .Width = "1000"
                            }

                            alImagesContainer.MainClearArts.Add(tmpPoster)
                        Next
                    End If
                    If Results.List.Movieart IsNot Nothing AndAlso Not _SpecialSettings.ClearArtOnlyHD Then
                        For Each image In Results.List.Movieart
                            Dim tmpPoster As New MediaContainers.Image With {
                                .Height = "281",
                                .Likes = CInt(image.Likes),
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                                .Scraper = "Fanart.tv",
                                .ShortLang = image.Lang,
                                .URLOriginal = image.Url,
                                .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                                .Width = "500"
                            }

                            alImagesContainer.MainClearArts.Add(tmpPoster)
                        Next
                    End If
                End If

                'ClearLogo
                If FilteredModifiers.MainClearLogo Then
                    If Results.List.Hdmovielogo IsNot Nothing Then
                        For Each image In Results.List.Hdmovielogo
                            Dim tmpPoster As New MediaContainers.Image With {
                                .Height = "310",
                                .Likes = CInt(image.Likes),
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                                .Scraper = "Fanart.tv",
                                .ShortLang = image.Lang,
                                .URLOriginal = image.Url,
                                .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                                .Width = "800"
                            }

                            alImagesContainer.MainClearLogos.Add(tmpPoster)
                        Next
                    End If

                    If Results.List.Movielogo IsNot Nothing AndAlso Not _SpecialSettings.ClearLogoOnlyHD Then
                        For Each image In Results.List.Movielogo
                            Dim tmpPoster As New MediaContainers.Image With {
                                .Height = "155",
                                .Likes = CInt(image.Likes),
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                                .Scraper = "Fanart.tv",
                                .ShortLang = image.Lang,
                                .URLOriginal = image.Url,
                                .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                                .Width = "400"
                            }

                            alImagesContainer.MainClearLogos.Add(tmpPoster)
                        Next
                    End If
                End If

                'DiscArt
                If FilteredModifiers.MainDiscArt AndAlso Results.List.Moviedisc IsNot Nothing Then
                    For Each image In Results.List.Moviedisc
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Disc = CInt(image.Disc),
                            .DiscType = image.DiscType,
                            .Height = "1000",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .ShortLang = image.Lang,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "1000"
                        }

                        alImagesContainer.MainDiscArts.Add(tmpPoster)
                    Next
                End If

                'Fanart
                If (FilteredModifiers.MainExtrafanarts OrElse FilteredModifiers.MainExtrathumbs OrElse FilteredModifiers.MainFanart) AndAlso Results.List.Moviebackground IsNot Nothing Then
                    For Each image In Results.List.Moviebackground
                        alImagesContainer.MainFanarts.Add(New MediaContainers.Image With {
                                                          .URLOriginal = image.Url,
                                                          .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                                                          .Width = "1920",
                                                          .Height = "1080",
                                                          .Scraper = "Fanart.tv",
                                                          .ShortLang = image.Lang,
                                                          .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                                                          .Likes = CInt(image.Likes)
                                                          })
                    Next
                End If

                'Landscape
                If FilteredModifiers.MainLandscape AndAlso Results.List.Moviethumb IsNot Nothing Then
                    For Each image In Results.List.Moviethumb
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "562",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .ShortLang = image.Lang,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "1000"
                        }

                        alImagesContainer.MainLandscapes.Add(tmpPoster)
                    Next
                End If

                'Poster / Keyart
                If (FilteredModifiers.MainPoster OrElse FilteredModifiers.MainKeyart) AndAlso Results.List.Movieposter IsNot Nothing Then
                    For Each image In Results.List.Movieposter
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "1426",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .ShortLang = image.Lang,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "1000"
                        }

                        alImagesContainer.MainPosters.Add(tmpPoster)
                    Next
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return alImagesContainer
        End Function

        Public Function GetImages_TV(ByVal tvdbId As Integer, ByVal FilteredModifiers As Structures.ScrapeModifiers) As MediaContainers.SearchResultsContainer
            Dim alImagesContainer As New MediaContainers.SearchResultsContainer

            Try
                Dim Results = New FanartTv.TV.Show(tvdbId.ToString)
                If Results Is Nothing OrElse FanartTv.API.ErrorOccurred Then
                    If FanartTv.API.ErrorMessage IsNot Nothing Then
                        logger.Error(FanartTv.API.ErrorMessage)
                    End If
                    Return alImagesContainer
                End If
                If bwFANARTTV.CancellationPending Then Return alImagesContainer

                'MainBanner
                If FilteredModifiers.MainBanner AndAlso Results.List.Tvbanner IsNot Nothing Then
                    For Each image In Results.List.Tvbanner
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "185",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .ShortLang = image.Lang,
                            .TVBannerType = Enums.TVBannerType.Any,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "1000"
                        }

                        alImagesContainer.MainBanners.Add(tmpPoster)
                    Next
                End If

                'MainCharacterArt
                If FilteredModifiers.MainCharacterArt AndAlso Results.List.Characterart IsNot Nothing Then
                    For Each image In Results.List.Characterart
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "512",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .ShortLang = image.Lang,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "512"
                        }

                        alImagesContainer.MainCharacterArts.Add(tmpPoster)
                    Next
                End If

                'MainClearArt
                If FilteredModifiers.MainClearArt Then
                    If Results.List.Hdclearart IsNot Nothing Then
                        For Each image In Results.List.Hdclearart
                            Dim tmpPoster As New MediaContainers.Image With {
                                .Height = "562",
                                .Likes = CInt(image.Likes),
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                                .Scraper = "Fanart.tv",
                                .ShortLang = image.Lang,
                                .URLOriginal = image.Url,
                                .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                                .Width = "1000"
                            }

                            alImagesContainer.MainClearArts.Add(tmpPoster)
                        Next
                    End If
                    If Results.List.Clearart IsNot Nothing AndAlso Not _SpecialSettings.ClearArtOnlyHD Then
                        For Each image In Results.List.Clearart
                            Dim tmpPoster As New MediaContainers.Image With {
                                .Height = "281",
                                .Likes = CInt(image.Likes),
                                .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                                .Scraper = "Fanart.tv",
                                .ShortLang = image.Lang,
                                .URLOriginal = image.Url,
                                .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                                .Width = "500"
                            }

                            alImagesContainer.MainClearArts.Add(tmpPoster)
                        Next
                    End If
                End If

                'MainClearLogo
                If FilteredModifiers.MainClearLogo Then
                    If Results.List.HdTListvlogo IsNot Nothing Then
                        For Each Image In Results.List.HdTListvlogo
                            Dim tmpPoster As New MediaContainers.Image With {
                                .Height = "310",
                                .Likes = CInt(Image.Likes),
                                .LongLang = If(String.IsNullOrEmpty(Image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(Image.Lang)),
                                .Scraper = "Fanart.tv",
                                .ShortLang = Image.Lang,
                                .URLOriginal = Image.Url,
                                .URLThumb = Image.Url.Replace("/fanart/", "/preview/"),
                                .Width = "800"
                            }

                            alImagesContainer.MainClearLogos.Add(tmpPoster)
                        Next
                    End If
                    If Results.List.Clearlogo IsNot Nothing AndAlso Not _SpecialSettings.ClearLogoOnlyHD Then
                        For Each Image In Results.List.Clearlogo
                            Dim tmpPoster As New MediaContainers.Image With {
                                .Height = "155",
                                .Likes = CInt(Image.Likes),
                                .LongLang = If(String.IsNullOrEmpty(Image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(Image.Lang)),
                                .Scraper = "Fanart.tv",
                                .ShortLang = Image.Lang,
                                .URLOriginal = Image.Url,
                                .URLThumb = Image.Url.Replace("/fanart/", "/preview/"),
                                .Width = "400"
                            }

                            alImagesContainer.MainClearLogos.Add(tmpPoster)
                        Next
                    End If
                End If

                'MainFanart
                If FilteredModifiers.MainFanart AndAlso Results.List.Showbackground IsNot Nothing Then
                    For Each image In Results.List.Showbackground
                        alImagesContainer.MainFanarts.Add(New MediaContainers.Image With {
                                                          .URLOriginal = image.Url,
                                                          .Width = "1920",
                                                          .Height = "1080",
                                                          .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                                                          .Scraper = "Fanart.tv",
                                                          .ShortLang = image.Lang,
                                                          .LongLang = If(String.IsNullOrEmpty(image.Lang), "", Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                                                          .Likes = CInt(image.Likes)
                                                          })
                    Next
                End If

                'MainLandscape
                If FilteredModifiers.MainLandscape AndAlso Results.List.Tvthumb IsNot Nothing Then
                    For Each Image In Results.List.Tvthumb
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "281",
                            .Likes = CInt(Image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(Image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(Image.Lang)),
                            .Scraper = "Fanart.tv",
                            .ShortLang = Image.Lang,
                            .URLOriginal = Image.Url,
                            .URLThumb = Image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "500"
                        }

                        alImagesContainer.MainLandscapes.Add(tmpPoster)
                    Next
                End If

                'MainPoster / MainKeyart
                If (FilteredModifiers.MainPoster OrElse FilteredModifiers.MainKeyart) AndAlso Results.List.Tvposter IsNot Nothing Then
                    For Each image In Results.List.Tvposter
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "1426",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .ShortLang = image.Lang,
                            .TVBannerType = Enums.TVBannerType.Any,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "1000"
                        }

                        alImagesContainer.MainPosters.Add(tmpPoster)
                    Next
                End If

                'SeasonBanner
                If FilteredModifiers.SeasonBanner AndAlso Results.List.Seasonbanner IsNot Nothing Then
                    For Each image In Results.List.Seasonbanner
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "185",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .Season = If(image.Season = "all", -1, CInt(image.Season)),
                            .ShortLang = image.Lang,
                            .TVBannerType = Enums.TVBannerType.Any,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "1000"
                        }

                        alImagesContainer.SeasonBanners.Add(tmpPoster)
                    Next
                End If

                'SeasonLandscape
                If FilteredModifiers.SeasonLandscape AndAlso Results.List.Seasonthumb IsNot Nothing Then
                    For Each Image In Results.List.Seasonthumb
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "281",
                            .Likes = CInt(Image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(Image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(Image.Lang)),
                            .Scraper = "Fanart.tv",
                            .Season = If(Image.Season = "all", -1, CInt(Image.Season)),
                            .ShortLang = Image.Lang,
                            .TVBannerType = Enums.TVBannerType.Any,
                            .URLOriginal = Image.Url,
                            .URLThumb = Image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "500"
                        }

                        alImagesContainer.SeasonLandscapes.Add(tmpPoster)
                    Next
                End If

                'SeasonPoster
                If FilteredModifiers.SeasonPoster AndAlso Results.List.Seasonposter IsNot Nothing Then
                    For Each image In Results.List.Seasonposter
                        Dim tmpPoster As New MediaContainers.Image With {
                            .Height = "1426",
                            .Likes = CInt(image.Likes),
                            .LongLang = If(String.IsNullOrEmpty(image.Lang), String.Empty, Localization.Languages.Get_Name_By_Alpha2(image.Lang)),
                            .Scraper = "Fanart.tv",
                            .Season = If(image.Season = "all", -1, CInt(image.Season)),
                            .ShortLang = image.Lang,
                            .TVBannerType = Enums.TVBannerType.Any,
                            .URLOriginal = image.Url,
                            .URLThumb = image.Url.Replace("/fanart/", "/preview/"),
                            .Width = "1000"
                        }

                        alImagesContainer.SeasonPosters.Add(tmpPoster)
                    Next
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            Return alImagesContainer
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

#End Region 'Nested Types

End Class