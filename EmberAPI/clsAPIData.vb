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

Imports NLog
Imports System.Text

Public Class Data

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"
    ''' <summary>
    ''' Returns the "merged" result of each data scraper results
    ''' </summary>
    ''' <param name="dbElement">DBElement to be scraped</param>
    ''' <param name="scrapedList"><c>List(Of MediaContainers.MainDetails)</c> which contains unfiltered results of each data scraper</param>
    ''' <returns>The scrape result of DBElement (after applying various global scraper settings here)</returns>
    ''' <remarks>
    ''' </remarks>
    Public Shared Function MergeResults(ByVal dbElement As Database.DBElement, ByVal scrapedList As List(Of MediaContainers.MainDetails), ByVal scrapeOptions As Structures.ScrapeOptions, ByVal withEpisodes As Boolean) As Database.DBElement
        Dim nScrapeOptions As Structures.ScrapeOptionsBase
        Dim nSettings As DataSettings
        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                nScrapeOptions = scrapeOptions
                nSettings = Master.eSettings.Movie.DataSettings
            Case Enums.ContentType.Movieset
                nSettings = Master.eSettings.Movieset.DataSettings
                nScrapeOptions = scrapeOptions
            Case Enums.ContentType.TVEpisode
                nSettings = Master.eSettings.TVEpisode.DataSettings
                nScrapeOptions = scrapeOptions.Episodes
            Case Enums.ContentType.TVSeason
                nSettings = Master.eSettings.TVSeason.DataSettings
                nScrapeOptions = scrapeOptions.Seasons
            Case Enums.ContentType.TVShow
                nSettings = Master.eSettings.TVShow.DataSettings
                nScrapeOptions = scrapeOptions
            Case Else
                Return dbElement
        End Select

        If nScrapeOptions Is Nothing Then Return dbElement

        With nSettings
            'protects the first scraped result against overwriting
            Dim new_Actors As Boolean = False
            Dim new_Aired As Boolean = False
            Dim new_Certifications As Boolean = False
            Dim new_Creators As Boolean = False
            Dim new_Collection As Boolean = False
            Dim new_Countries As Boolean = False
            Dim new_Credits As Boolean = False
            Dim new_Directors As Boolean = False
            Dim new_EpisodeGuideURL As Boolean = False
            Dim new_Genres As Boolean = False
            Dim new_GuestStars As Boolean = False
            Dim new_MPAA As Boolean = False
            Dim new_OriginalTitle As Boolean = False
            Dim new_Outline As Boolean = False
            Dim new_Plot As Boolean = False
            Dim new_Premiered As Boolean = False
            Dim new_Ratings As Boolean = False
            Dim new_Runtime As Boolean = False
            Dim new_Status As Boolean = False
            Dim new_Studios As Boolean = False
            Dim new_Tagline As Boolean = False
            Dim new_Tags As Boolean = False
            Dim new_ThumbPoster As Boolean = False
            Dim new_Title As Boolean = False
            Dim new_Top250 As Boolean = False
            Dim new_Trailer As Boolean = False
            Dim new_UserRatings As Boolean = False

            Dim KnownEpisodesIndex As New List(Of KnownEpisode)
            Dim KnownSeasonsIndex As New List(Of Integer)
            '
            'Primary data and settings
            '
            For Each cScraperData In scrapedList
                'UniqueIDs
                If cScraperData.UniqueIDsSpecified Then
                    dbElement.MainDetails.UniqueIDs.AddRange(cScraperData.UniqueIDs)
                End If

                'DisplayEpisode
                If cScraperData.DisplayEpisodeSpecified Then
                    dbElement.MainDetails.DisplayEpisode = cScraperData.DisplayEpisode
                End If

                'DisplaySeason
                If cScraperData.DisplaySeasonSpecified Then
                    dbElement.MainDetails.DisplaySeason = cScraperData.DisplaySeason
                End If

                'Actors
                If OverwriteValue(scrapeOptions.Actors, new_Actors, dbElement.MainDetails.ActorsSpecified, cScraperData.ActorsSpecified, .Actors) Then
                    If .Actors.WithImageOnly Then
                        FilterOnlyPersonsWithImage(cScraperData.Actors)
                    End If
                    FilterCountLimit(.Actors.Limit, cScraperData.Actors)
                    'added check if there's any actors left to add, if not then try with results of following scraper...
                    If cScraperData.ActorsSpecified Then
                        ReorderPersons(cScraperData.Actors)
                        dbElement.MainDetails.Actors = cScraperData.Actors
                        new_Actors = True
                    End If
                ElseIf ClearDatafield(.Actors, dbElement.ContentType) Then
                    dbElement.MainDetails.Actors.Clear()
                End If

                'Aired
                If OverwriteValue(scrapeOptions.Aired, new_Aired, dbElement.MainDetails.AiredSpecified, cScraperData.AiredSpecified, .Aired) Then
                    dbElement.MainDetails.Aired = cScraperData.Aired
                    new_Aired = True
                ElseIf ClearDatafield(.Aired, dbElement.ContentType) Then
                    dbElement.MainDetails.Aired = String.Empty
                End If

                'Certifications
                If OverwriteValue(scrapeOptions.Certifications, new_Certifications, dbElement.MainDetails.CertificationsSpecified, cScraperData.CertificationsSpecified, .Certifications) Then
                    If .Certifications.Filter = Master.eLang.All Then
                        dbElement.MainDetails.Certifications = cScraperData.Certifications
                        new_Certifications = True
                    Else
                        Dim CertificationLanguage = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = .Certifications.Filter)
                        If CertificationLanguage IsNot Nothing AndAlso CertificationLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CertificationLanguage.name) Then
                            For Each tCert In cScraperData.Certifications
                                If tCert.StartsWith(CertificationLanguage.name) Then
                                    dbElement.MainDetails.Certifications.Clear()
                                    dbElement.MainDetails.Certifications.Add(tCert)
                                    new_Certifications = True
                                    Exit For
                                End If
                            Next
                        Else
                            _Logger.Error("Certification Language (Limit) not found. Please check your settings!")
                        End If
                    End If
                ElseIf ClearDatafield(.Certifications, dbElement.ContentType) Then
                    dbElement.MainDetails.Certifications.Clear()
                End If

                'Collection
                If OverwriteValue(scrapeOptions.Collection, new_Collection, dbElement.MainDetails.SetsSpecified, cScraperData.SetsSpecified, .Collection) AndAlso .Collection.AutoAddToCollection Then
                    dbElement.MainDetails.Sets.Clear()
                    For Each movieset In cScraperData.Sets
                        If Not String.IsNullOrEmpty(movieset.Title) Then
                            For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:")) 'TODO: TitleRenamer
                                movieset.Title = movieset.Title.Replace(sett.Name.Substring(21), sett.Value)
                            Next
                        End If
                    Next
                    dbElement.MainDetails.Sets.AddRange(cScraperData.Sets)
                    new_Collection = True
                End If

                'Countries
                If OverwriteValue(scrapeOptions.Countries, new_Countries, dbElement.MainDetails.CountriesSpecified, cScraperData.CountriesSpecified, .Countries) Then
                    FilterCountLimit(.Countries.Limit, cScraperData.Countries)
                    dbElement.MainDetails.Countries = cScraperData.Countries
                    new_Countries = True
                ElseIf ClearDatafield(.Countries, dbElement.ContentType) Then
                    dbElement.MainDetails.Countries.Clear()
                End If

                'Creators
                If OverwriteValue(scrapeOptions.Creators, new_Creators, dbElement.MainDetails.CreatorsSpecified, cScraperData.CreatorsSpecified, .Creators) Then
                    dbElement.MainDetails.Creators = cScraperData.Creators
                    new_Creators = True
                ElseIf ClearDatafield(.Creators, dbElement.ContentType) Then
                    dbElement.MainDetails.Creators.Clear()
                End If

                'Credits
                If OverwriteValue(scrapeOptions.Credits, new_Credits, dbElement.MainDetails.CreditsSpecified, cScraperData.CreditsSpecified, .Credits) Then
                    dbElement.MainDetails.Credits = cScraperData.Credits
                    new_Credits = True
                ElseIf ClearDatafield(.Credits, dbElement.ContentType) Then
                    dbElement.MainDetails.Credits.Clear()
                End If

                'Directors
                If OverwriteValue(scrapeOptions.Directors, new_Directors, dbElement.MainDetails.DirectorsSpecified, cScraperData.DirectorsSpecified, .Directors) Then
                    dbElement.MainDetails.Directors = cScraperData.Directors
                    new_Directors = True
                ElseIf ClearDatafield(.Directors, dbElement.ContentType) Then
                    dbElement.MainDetails.Directors.Clear()
                End If

                'EpisodeGuideURL
                If OverwriteValue(scrapeOptions.EpisodeGuideURL, new_EpisodeGuideURL, dbElement.MainDetails.EpisodeGuideURLSpecified, cScraperData.EpisodeGuideURLSpecified, .EpisodeGuideURL) Then
                    dbElement.MainDetails.EpisodeGuideURL = cScraperData.EpisodeGuideURL
                    new_EpisodeGuideURL = True
                ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowEpiGuideURL Then
                    dbElement.MainDetails.EpisodeGuideURL = New MediaContainers.EpisodeGuide
                End If

                'Genres
                If OverwriteValue(scrapeOptions.Genres, new_Genres, dbElement.MainDetails.GenresSpecified, cScraperData.GenresSpecified, .Genres) Then
                    StringUtils.GenreFilter(cScraperData.Genres)
                    FilterCountLimit(.Genres.Limit, cScraperData.Genres)
                    dbElement.MainDetails.Genres = cScraperData.Genres
                    new_Genres = True
                ElseIf ClearDatafield(.Genres, dbElement.ContentType) Then
                    dbElement.MainDetails.Genres.Clear()
                End If

                'GuestStars
                If OverwriteValue(scrapeOptions.GuestStars, new_GuestStars, dbElement.MainDetails.GuestStarsSpecified, cScraperData.GuestStarsSpecified, .GuestStars) Then
                    If .GuestStars.WithImageOnly Then
                        FilterOnlyPersonsWithImage(cScraperData.GuestStars)
                    End If
                    FilterCountLimit(.GuestStars.Limit, cScraperData.GuestStars)
                    'added check if there's any actors left to add, if not then try with results of following scraper...
                    If cScraperData.GuestStarsSpecified Then
                        ReorderPersons(cScraperData.GuestStars)
                        dbElement.MainDetails.GuestStars = cScraperData.GuestStars
                        new_GuestStars = True
                    End If
                ElseIf ClearDatafield(.GuestStars, dbElement.ContentType) Then
                    dbElement.MainDetails.GuestStars.Clear()
                End If

                'MPAA
                If OverwriteValue(scrapeOptions.MPAA, new_MPAA, dbElement.MainDetails.MPAASpecified, cScraperData.MPAASpecified, .MPAA) Then
                    dbElement.MainDetails.MPAA = cScraperData.MPAA
                    new_MPAA = True
                ElseIf ClearDatafield(.MPAA, dbElement.ContentType) Then
                    dbElement.MainDetails.MPAA = String.Empty
                End If

                'OriginalTitle
                If OverwriteValue(scrapeOptions.OriginalTitle, new_OriginalTitle, dbElement.MainDetails.OriginalTitleSpecified, cScraperData.OriginalTitleSpecified, .OriginalTitle) Then
                    dbElement.MainDetails.OriginalTitle = cScraperData.OriginalTitle
                    new_OriginalTitle = True
                ElseIf ClearDatafield(.OriginalTitle, dbElement.ContentType) Then
                    dbElement.MainDetails.OriginalTitle = String.Empty
                End If

                'Outline
                If OverwriteValue(scrapeOptions.Outline, new_Outline, dbElement.MainDetails.OutlineSpecified, cScraperData.OutlineSpecified, .Outline) Then
                    dbElement.MainDetails.Outline = cScraperData.Outline
                    new_Outline = True
                ElseIf ClearDatafield(.Outline, dbElement.ContentType) Then
                    dbElement.MainDetails.Outline = String.Empty
                End If
                'check if brackets should be removed...
                If .CleanPlotAndOutline Then
                    dbElement.MainDetails.Outline = StringUtils.RemoveBrackets(dbElement.MainDetails.Outline)
                End If

                'Plot
                If OverwriteValue(scrapeOptions.Plot, new_Plot, dbElement.MainDetails.PlotSpecified, cScraperData.PlotSpecified, .Plot) Then
                    dbElement.MainDetails.Plot = cScraperData.Plot
                    new_Plot = True
                ElseIf ClearDatafield(.Plot, dbElement.ContentType) Then
                    dbElement.MainDetails.Plot = String.Empty
                End If
                'check if brackets should be removed...
                If .CleanPlotAndOutline Then
                    dbElement.MainDetails.Plot = StringUtils.RemoveBrackets(dbElement.MainDetails.Plot)
                End If

                'Premiered
                If OverwriteValue(scrapeOptions.Premiered, new_Premiered, dbElement.MainDetails.PremieredSpecified, cScraperData.PremieredSpecified, .Premiered) Then
                    dbElement.MainDetails.Premiered = NumUtils.DateToISO8601Date(cScraperData.Premiered)
                    new_Premiered = True
                ElseIf ClearDatafield(.Premiered, dbElement.ContentType) Then
                    dbElement.MainDetails.Premiered = String.Empty
                End If

                'Ratings
                If OverwriteValue(scrapeOptions.Ratings, new_UserRatings, dbElement.MainDetails.RatingsSpecified, cScraperData.RatingsSpecified, .Ratings) Then
                    For Each nRating In cScraperData.Ratings
                        'remove old rating(s) from the same source
                        dbElement.MainDetails.Ratings.RemoveAll(Function(f) f.Name = nRating.Name)
                        dbElement.MainDetails.Ratings.Add(nRating)
                    Next
                ElseIf ClearDatafield(.Ratings, dbElement.ContentType) Then
                    dbElement.MainDetails.Ratings.Clear()
                    dbElement.MainDetails.Rating = String.Empty
                    dbElement.MainDetails.Votes = String.Empty
                End If

                'Runtime
                If OverwriteValue(scrapeOptions.Runtime, new_Runtime, dbElement.MainDetails.RuntimeSpecified, cScraperData.RuntimeSpecified, .Runtime) Then
                    dbElement.MainDetails.Runtime = cScraperData.Runtime
                    new_Runtime = True
                ElseIf ClearDatafield(.Runtime, dbElement.ContentType) Then
                    dbElement.MainDetails.Runtime = String.Empty
                End If

                'Status
                If OverwriteValue(scrapeOptions.Status, new_Status, dbElement.MainDetails.StatusSpecified, cScraperData.StatusSpecified, .Status) Then
                    dbElement.MainDetails.Status = cScraperData.Status
                    new_Status = True
                ElseIf ClearDatafield(.Status, dbElement.ContentType) Then
                    dbElement.MainDetails.Status = String.Empty
                End If

                'Studios
                If OverwriteValue(scrapeOptions.Studios, new_Studios, dbElement.MainDetails.StudiosSpecified, cScraperData.StudiosSpecified, .Studios) Then
                    FilterCountLimit(.Studios.Limit, cScraperData.Studios)
                    dbElement.MainDetails.Studios = cScraperData.Studios
                    new_Studios = True
                ElseIf ClearDatafield(.Studios, dbElement.ContentType) Then
                    dbElement.MainDetails.Studios.Clear()
                End If

                'Tagline
                If OverwriteValue(scrapeOptions.Tagline, new_Tagline, dbElement.MainDetails.TaglineSpecified, cScraperData.TaglineSpecified, .Tagline) Then
                    dbElement.MainDetails.Tagline = cScraperData.Tagline
                    new_Tagline = True
                ElseIf ClearDatafield(.Tagline, dbElement.ContentType) Then
                    dbElement.MainDetails.Tagline = String.Empty
                End If

                'Tags
                If OverwriteValue(scrapeOptions.Tags, new_Tags, dbElement.MainDetails.TagsSpecified, cScraperData.TagsSpecified, .Tags) Then
                    dbElement.MainDetails.Tags = cScraperData.Tags
                    'TODO: add Whitelist
                ElseIf ClearDatafield(.Tags, dbElement.ContentType) Then
                    dbElement.MainDetails.Tags.Clear()
                End If

                'ThumbPoster
                If (Not String.IsNullOrEmpty(cScraperData.ThumbPoster.URLOriginal) OrElse Not String.IsNullOrEmpty(cScraperData.ThumbPoster.URLThumb)) AndAlso Not new_ThumbPoster Then
                    dbElement.MainDetails.ThumbPoster = cScraperData.ThumbPoster
                    new_ThumbPoster = True
                End If

                'Title
                If OverwriteValue(scrapeOptions.Title, new_Title, dbElement.MainDetails.TitleSpecified, cScraperData.TitleSpecified, .Title) Then
                    If dbElement.ContentType = Enums.ContentType.Movieset Then
                        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
                            dbElement.MainDetails.Title = dbElement.MainDetails.Title.Replace(sett.Name.Substring(21), sett.Value)
                        Next
                    Else
                        dbElement.MainDetails.Title = cScraperData.Title
                    End If
                    new_Title = True
                ElseIf ClearDatafield(.Title, dbElement.ContentType) Then
                    dbElement.MainDetails.Title = String.Empty
                End If

                'Top250 (special handling: no check if "scrapedmovie.Top250Specified" and only set "new_Top250 = True" if a value > 0 has been set)
                'otherwise a movie that's no longer in the Top250 list can't be corrected
                If OverwriteValue(scrapeOptions.Top250, new_Top250, dbElement.MainDetails.Top250Specified, True, .Top250) Then
                    dbElement.MainDetails.Top250 = cScraperData.Top250
                    new_Top250 = If(cScraperData.Top250Specified, True, False)
                ElseIf ClearDatafield(.Top250, dbElement.ContentType) Then
                    dbElement.MainDetails.Top250 = 0
                End If

                'Trailer
                If OverwriteValue(scrapeOptions.Trailer, new_Trailer, dbElement.MainDetails.TrailerSpecified, cScraperData.TrailerSpecified, .TrailerLink) Then
                    If .TrailerLink.SaveKodiCompatible AndAlso YouTube.UrlUtils.IsYouTubeURL(cScraperData.Trailer) Then
                        dbElement.MainDetails.Trailer = StringUtils.ConvertFromYouTubeURLToKodiTrailerFormat(cScraperData.Trailer)
                    Else
                        dbElement.MainDetails.Trailer = cScraperData.Trailer
                    End If
                    new_Trailer = True
                ElseIf ClearDatafield(.TrailerLink, dbElement.ContentType) Then
                    dbElement.MainDetails.Trailer = String.Empty
                End If

                'User Rating
                If OverwriteValue(scrapeOptions.UserRating, new_UserRatings, dbElement.MainDetails.UserRatingSpecified, cScraperData.UserRatingSpecified, .UserRating) Then
                    dbElement.MainDetails.UserRating = cScraperData.UserRating
                    new_UserRatings = True
                ElseIf ClearDatafield(.UserRating, dbElement.ContentType) Then
                    dbElement.MainDetails.UserRating = 0
                End If

                'Create KnowSeasons index
                For Each kSeason As MediaContainers.MainDetails In cScraperData.KnownSeasons
                    If Not KnownSeasonsIndex.Contains(kSeason.Season) Then
                        KnownSeasonsIndex.Add(kSeason.Season)
                    End If
                Next

                'Create KnownEpisodes index (season and episode number)
                If withEpisodes Then
                    For Each kEpisode As MediaContainers.MainDetails In cScraperData.KnownEpisodes
                        Dim nKnownEpisode As New KnownEpisode With {
                            .AiredDate = kEpisode.Aired,
                            .Episode = kEpisode.Episode,
                            .EpisodeAbsolute = kEpisode.EpisodeAbsolute,
                            .EpisodeCombined = kEpisode.EpisodeCombined,
                            .EpisodeDVD = kEpisode.EpisodeDVD,
                            .Season = kEpisode.Season,
                            .SeasonCombined = kEpisode.SeasonCombined,
                            .SeasonDVD = kEpisode.SeasonDVD
                        }
                        If KnownEpisodesIndex.Where(Function(f) f.Episode = nKnownEpisode.Episode AndAlso f.Season = nKnownEpisode.Season).Count = 0 Then
                            KnownEpisodesIndex.Add(nKnownEpisode)

                            'try to get an episode information with more numbers
                        ElseIf KnownEpisodesIndex.Where(Function(f) f.Episode = nKnownEpisode.Episode AndAlso f.Season = nKnownEpisode.Season AndAlso
                                ((nKnownEpisode.EpisodeAbsolute > -1 AndAlso Not f.EpisodeAbsolute = nKnownEpisode.EpisodeAbsolute) OrElse
                                 (nKnownEpisode.EpisodeCombined > -1 AndAlso Not f.EpisodeCombined = nKnownEpisode.EpisodeCombined) OrElse
                                 (nKnownEpisode.EpisodeDVD > -1 AndAlso Not f.EpisodeDVD = nKnownEpisode.EpisodeDVD) OrElse
                                 (nKnownEpisode.SeasonCombined > -1 AndAlso Not f.SeasonCombined = nKnownEpisode.SeasonCombined) OrElse
                                 (nKnownEpisode.SeasonDVD > -1 AndAlso Not f.SeasonDVD = nKnownEpisode.SeasonDVD))).Count = 1 Then
                            Dim toRemove As KnownEpisode = KnownEpisodesIndex.FirstOrDefault(Function(f) f.Episode = nKnownEpisode.Episode AndAlso f.Season = nKnownEpisode.Season)
                            KnownEpisodesIndex.Remove(toRemove)
                            KnownEpisodesIndex.Add(nKnownEpisode)
                        End If
                    Next
                End If
            Next
            '
            'Additional data and settings
            '
            'Certification for MPAA
            If dbElement.MainDetails.CertificationsSpecified AndAlso .CertificationsForMPAA AndAlso
                (Not .CertificationsForMPAAFallback AndAlso (Not dbElement.MainDetails.MPAASpecified OrElse Not .MPAA.Locked) OrElse
                 Not new_MPAA AndAlso (Not dbElement.MainDetails.MPAASpecified OrElse Not .MPAA.Locked)) Then
                Dim tmpstring As String = String.Empty
                tmpstring = If(Master.eSettings.Movie.DataSettings.Certifications.Filter = "us", StringUtils.USACertToMPAA(String.Join(" / ", dbElement.MainDetails.Certifications.ToArray)), If(.CertificationsOnlyValue, String.Join(" / ", dbElement.MainDetails.Certifications.ToArray).Split(Convert.ToChar(":"))(1), String.Join(" / ", dbElement.MainDetails.Certifications.ToArray)))
                'only update DBMovie if scraped result is not empty/nothing!
                If Not String.IsNullOrEmpty(tmpstring) Then
                    dbElement.MainDetails.MPAA = tmpstring
                End If
            End If

            'GuestStars as Actors TODO: move to NFO modification
            If dbElement.MainDetails.GuestStarsSpecified AndAlso Master.eSettings.TVScraperEpisodeGuestStarsToActors AndAlso Not .Actors.Locked Then
                dbElement.MainDetails.Actors.AddRange(dbElement.MainDetails.GuestStars)

                'run the limit filter again
                FilterCountLimit(Master.eSettings.TVScraperEpisodeActorsLimit, dbElement.MainDetails.Actors)

                'reorder again
                ReorderPersons(dbElement.MainDetails.Actors)
            End If

            'MPAA value if MPAA is not available
            If Not dbElement.MainDetails.MPAASpecified AndAlso .MPAANotRatedValueSpecified Then
                dbElement.MainDetails.MPAA = .MPAANotRatedValue
            End If

            'OriginalTitle as Title
            If (Not dbElement.MainDetails.TitleSpecified OrElse Not .Title.Locked) AndAlso .Title.UseOriginalTitle AndAlso dbElement.MainDetails.OriginalTitleSpecified Then
                dbElement.MainDetails.Title = dbElement.MainDetails.OriginalTitle
            End If

            'Plot for Outline
            If ((Not dbElement.MainDetails.OutlineSpecified OrElse Not .Outline.Locked) AndAlso .Outline.UsePlot AndAlso Not .Outline.UsePlotAsFallback) OrElse
                (Not dbElement.MainDetails.OutlineSpecified AndAlso .Outline.UsePlot AndAlso .Outline.UsePlotAsFallback) Then
                dbElement.MainDetails.Outline = StringUtils.ShortenOutline(dbElement.MainDetails.Plot, .Outline.Limit)
            End If

            'Rating/Votes
            'TODO: set the default rating/votes
            If (Not dbElement.MainDetails.RatingSpecified OrElse Not .Ratings.Locked) AndAlso scrapeOptions.Ratings AndAlso
                    dbElement.MainDetails.RatingsSpecified AndAlso .Ratings.Enabled Then
                dbElement.MainDetails.Rating = dbElement.MainDetails.Ratings.Item(0).Value.ToString
                dbElement.MainDetails.Votes = NumUtils.CleanVotes(dbElement.MainDetails.Ratings.Item(0).Votes.ToString)
            ElseIf .ClearDisabledFields AndAlso Not .Ratings.Enabled AndAlso Not .Ratings.Locked Then
                dbElement.MainDetails.Ratings.Clear()
                dbElement.MainDetails.Rating = String.Empty
                dbElement.MainDetails.Votes = String.Empty
            End If

            'TVShow Runtime for Episode Runtime
            If Not dbElement.MainDetails.RuntimeSpecified AndAlso Master.eSettings.TVScraperUseSRuntimeForEp AndAlso dbElement.TVShowDetails.RuntimeSpecified Then
                dbElement.MainDetails.Runtime = dbElement.TVShowDetails.Runtime
            End If

            'Seasons
            For Each aKnownSeason As Integer In KnownSeasonsIndex
                'create a list of specified episode informations from all scrapers
                Dim ScrapedSeasonList As New List(Of MediaContainers.MainDetails)
                For Each nShow As MediaContainers.MainDetails In scrapedList
                    For Each nSeasonDetails As MediaContainers.MainDetails In nShow.KnownSeasons.Where(Function(f) f.Season = aKnownSeason)
                        ScrapedSeasonList.Add(nSeasonDetails)
                    Next
                Next
                'check if we have already saved season information for this scraped season
                Dim lSeasonList = dbElement.Seasons.Where(Function(f) f.MainDetails.Season = aKnownSeason)

                If lSeasonList IsNot Nothing AndAlso lSeasonList.Count > 0 Then
                    For Each nSeason As Database.DBElement In lSeasonList
                        MergeResults(nSeason, ScrapedSeasonList, scrapeOptions, False)
                    Next
                Else
                    'no existing season found -> add it as "missing" season
                    Dim mSeason As New Database.DBElement(Enums.ContentType.TVSeason) With {.MainDetails = New MediaContainers.MainDetails With {.Season = aKnownSeason}}
                    mSeason = Master.DB.AddTVShowInfoToDBElement(mSeason, dbElement)
                    dbElement.Seasons.Add(MergeResults(mSeason, ScrapedSeasonList, scrapeOptions, False))
                End If
            Next
            'add all season informations to TVShow (for saving season informations to tv show NFO)
            dbElement.MainDetails.Seasons.Seasons.Clear()
            For Each kSeason As Database.DBElement In dbElement.Seasons.OrderBy(Function(f) f.MainDetails.Season)
                dbElement.MainDetails.Seasons.Seasons.Add(kSeason.MainDetails)
            Next

            'Episodes
            If withEpisodes Then
                'update the tvshow information for each local episode
                For Each lEpisode In dbElement.Episodes
                    lEpisode = Master.DB.AddTVShowInfoToDBElement(lEpisode, dbElement)
                Next

                For Each aKnownEpisode As KnownEpisode In KnownEpisodesIndex.OrderBy(Function(f) f.Episode).OrderBy(Function(f) f.Season)

                    'convert the episode and season number if needed
                    Dim iEpisode As Integer = -1
                    Dim iSeason As Integer = -1
                    Dim strAiredDate As String = aKnownEpisode.AiredDate
                    If dbElement.EpisodeOrdering = Enums.EpisodeOrdering.Absolute Then
                        iEpisode = aKnownEpisode.EpisodeAbsolute
                        iSeason = 1
                    ElseIf dbElement.EpisodeOrdering = Enums.EpisodeOrdering.DVD Then
                        iEpisode = CInt(aKnownEpisode.EpisodeDVD)
                        iSeason = aKnownEpisode.SeasonDVD
                    ElseIf dbElement.EpisodeOrdering = Enums.EpisodeOrdering.Standard Then
                        iEpisode = aKnownEpisode.Episode
                        iSeason = aKnownEpisode.Season
                    End If

                    If Not iEpisode = -1 AndAlso Not iSeason = -1 Then
                        'create a list of specified episode informations from all scrapers
                        Dim ScrapedEpisodeList As New List(Of MediaContainers.MainDetails)
                        For Each nShow As MediaContainers.MainDetails In scrapedList
                            For Each nEpisodeDetails As MediaContainers.MainDetails In nShow.KnownEpisodes.Where(Function(f) f.Episode = aKnownEpisode.Episode AndAlso f.Season = aKnownEpisode.Season)
                                ScrapedEpisodeList.Add(nEpisodeDetails)
                            Next
                        Next

                        'check if we have a local episode file for this scraped episode
                        Dim lEpisodeList = dbElement.Episodes.Where(Function(f) f.FileItemSpecified AndAlso f.MainDetails.Episode = iEpisode AndAlso f.MainDetails.Season = iSeason)

                        If lEpisodeList IsNot Nothing AndAlso lEpisodeList.Count > 0 Then
                            For Each nEpisode As Database.DBElement In lEpisodeList
                                MergeResults(nEpisode, ScrapedEpisodeList, scrapeOptions, False)
                            Next
                        Else
                            'try to get the episode by AiredDate
                            Dim dEpisodeList = dbElement.Episodes.Where(Function(f) f.FileItemSpecified AndAlso
                                                                       f.MainDetails.Episode = -1 AndAlso
                                                                       f.MainDetails.AiredSpecified AndAlso
                                                                       f.MainDetails.Aired = strAiredDate)

                            If dEpisodeList IsNot Nothing AndAlso dEpisodeList.Count > 0 Then
                                For Each nEpisode As Database.DBElement In dEpisodeList
                                    MergeResults(nEpisode, ScrapedEpisodeList, scrapeOptions, False)
                                    'we have to add the proper season and episode number if the episode was found by AiredDate
                                    nEpisode.MainDetails.Episode = iEpisode
                                    nEpisode.MainDetails.Season = iSeason
                                Next
                            Else
                                'no local episode found -> add it as "missing" episode
                                Dim mEpisode As New Database.DBElement(Enums.ContentType.TVEpisode) With {.MainDetails = New MediaContainers.MainDetails With {.Episode = iEpisode, .Season = iSeason}}
                                mEpisode = Master.DB.AddTVShowInfoToDBElement(mEpisode, dbElement)
                                MergeResults(mEpisode, ScrapedEpisodeList, scrapeOptions, False)
                                If mEpisode.MainDetails.TitleSpecified Then
                                    dbElement.Episodes.Add(mEpisode)
                                Else
                                    _Logger.Warn(String.Format("Missing Episode Ignored | {0} - S{1}E{2} | No Episode Title found", mEpisode.MainDetails.Title, mEpisode.MainDetails.Season, mEpisode.MainDetails.Episode))
                                End If
                            End If
                        End If
                    Else
                        _Logger.Warn("No valid episode or season number found")
                    End If
                Next
            End If

            'create the "* All Seasons" entry if needed
            Dim tmpAllSeasons As Database.DBElement = dbElement.Seasons.FirstOrDefault(Function(f) f.MainDetails.Season_IsAllSeasons)
            If tmpAllSeasons Is Nothing OrElse tmpAllSeasons.MainDetails Is Nothing Then
                tmpAllSeasons = New Database.DBElement(Enums.ContentType.TVSeason) With {
                    .MainDetails = New MediaContainers.MainDetails With {.Season = -1}
                }
                tmpAllSeasons = Master.DB.AddTVShowInfoToDBElement(tmpAllSeasons, dbElement)
                dbElement.Seasons.Add(tmpAllSeasons)
            End If

            'cleanup seasons they don't have any episode
            Dim iIndex As Integer = 0
            While iIndex <= dbElement.Seasons.Count - 1
                Dim iSeason As Integer = dbElement.Seasons.Item(iIndex).MainDetails.Season
                If Not iSeason = -1 AndAlso dbElement.Episodes.Where(Function(f) f.MainDetails.Season = iSeason).Count = 0 Then
                    dbElement.Seasons.RemoveAt(iIndex)
                Else
                    iIndex += 1
                End If
            End While
        End With
        Return dbElement
    End Function

    Public Shared Function MergeResults_TVEpisode_Single(ByRef DBTVEpisode As Database.DBElement, ByVal ScrapedList As List(Of MediaContainers.MainDetails), ByVal ScrapeOptions As Structures.ScrapeOptions) As Database.DBElement
        Dim KnownEpisodesIndex As New List(Of KnownEpisode)

        For Each kEpisode As MediaContainers.MainDetails In ScrapedList
            Dim nKnownEpisode As New KnownEpisode With {.AiredDate = kEpisode.Aired,
                                                        .Episode = kEpisode.Episode,
                                                        .EpisodeAbsolute = kEpisode.EpisodeAbsolute,
                                                        .EpisodeCombined = kEpisode.EpisodeCombined,
                                                        .EpisodeDVD = kEpisode.EpisodeDVD,
                                                        .Season = kEpisode.Season,
                                                        .SeasonCombined = kEpisode.SeasonCombined,
                                                        .SeasonDVD = kEpisode.SeasonDVD}
            If KnownEpisodesIndex.Where(Function(f) f.Episode = nKnownEpisode.Episode AndAlso f.Season = nKnownEpisode.Season).Count = 0 Then
                KnownEpisodesIndex.Add(nKnownEpisode)

                'try to get an episode information with more numbers
            ElseIf KnownEpisodesIndex.Where(Function(f) f.Episode = nKnownEpisode.Episode AndAlso f.Season = nKnownEpisode.Season AndAlso
                        ((nKnownEpisode.EpisodeAbsolute > -1 AndAlso Not f.EpisodeAbsolute = nKnownEpisode.EpisodeAbsolute) OrElse
                         (nKnownEpisode.EpisodeCombined > -1 AndAlso Not f.EpisodeCombined = nKnownEpisode.EpisodeCombined) OrElse
                         (nKnownEpisode.EpisodeDVD > -1 AndAlso Not f.EpisodeDVD = nKnownEpisode.EpisodeDVD) OrElse
                         (nKnownEpisode.SeasonCombined > -1 AndAlso Not f.SeasonCombined = nKnownEpisode.SeasonCombined) OrElse
                         (nKnownEpisode.SeasonDVD > -1 AndAlso Not f.SeasonDVD = nKnownEpisode.SeasonDVD))).Count = 1 Then
                Dim toRemove As KnownEpisode = KnownEpisodesIndex.FirstOrDefault(Function(f) f.Episode = nKnownEpisode.Episode AndAlso f.Season = nKnownEpisode.Season)
                KnownEpisodesIndex.Remove(toRemove)
                KnownEpisodesIndex.Add(nKnownEpisode)
            End If
        Next

        If KnownEpisodesIndex.Count = 1 Then
            'convert the episode and season number if needed
            Dim iEpisode As Integer = -1
            Dim iSeason As Integer = -1
            Dim strAiredDate As String = KnownEpisodesIndex.Item(0).AiredDate
            If DBTVEpisode.EpisodeOrdering = Enums.EpisodeOrdering.Absolute Then
                iEpisode = KnownEpisodesIndex.Item(0).EpisodeAbsolute
                iSeason = 1
            ElseIf DBTVEpisode.EpisodeOrdering = Enums.EpisodeOrdering.DVD Then
                iEpisode = CInt(KnownEpisodesIndex.Item(0).EpisodeDVD)
                iSeason = KnownEpisodesIndex.Item(0).SeasonDVD
            ElseIf DBTVEpisode.EpisodeOrdering = Enums.EpisodeOrdering.Standard Then
                iEpisode = KnownEpisodesIndex.Item(0).Episode
                iSeason = KnownEpisodesIndex.Item(0).Season
            End If

            If Not iEpisode = -1 AndAlso Not iSeason = -1 Then
                MergeResults(DBTVEpisode, ScrapedList, ScrapeOptions, False)
                If DBTVEpisode.MainDetails.Episode = -1 Then DBTVEpisode.MainDetails.Episode = iEpisode
                If DBTVEpisode.MainDetails.Season = -1 Then DBTVEpisode.MainDetails.Season = iSeason
            Else
                _Logger.Warn("No valid episode or season number found")
            End If
        Else
            _Logger.Warn("Episode could not be clearly determined.")
        End If

        Return DBTVEpisode
    End Function

    Private Shared Function ClearDatafield(ByVal settings As DataSpecificationItem, ByVal type As Enums.ContentType) As Boolean
        Select Case type
            Case Enums.ContentType.Movie
                Return Master.eSettings.Movie.DataSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
            Case Enums.ContentType.Movieset
                Return Master.eSettings.Movieset.DataSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
            Case Enums.ContentType.TVEpisode
                Return Master.eSettings.TVEpisode.DataSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
            Case Enums.ContentType.TVSeason
                Return Master.eSettings.TVSeason.DataSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
            Case Enums.ContentType.TVShow
                Return Master.eSettings.TVShow.DataSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
        End Select
        Return False
    End Function

    Private Shared Sub FilterOnlyPersonsWithImage(ByRef lstPerson As List(Of MediaContainers.Person))
        If lstPerson IsNot Nothing Then
            lstPerson = lstPerson.Where(Function(f) f.URLOriginalSpecified).ToList
        End If
    End Sub

    Private Shared Sub FilterCountLimit(ByVal iLimit As Integer, ByRef lstPerson As List(Of MediaContainers.Person))
        If Not iLimit = 0 AndAlso iLimit < lstPerson.Count Then
            lstPerson.RemoveRange(iLimit, lstPerson.Count - iLimit)
        End If
    End Sub

    Private Shared Sub FilterCountLimit(ByVal iLimit As Integer, ByRef lstString As List(Of String))
        If Not iLimit = 0 AndAlso iLimit < lstString.Count Then
            lstString.RemoveRange(iLimit, lstString.Count - iLimit)
        End If
    End Sub

    Public Shared Function FIToString(ByVal dbElement As Database.DBElement) As String
        Dim strOutput As New StringBuilder
        Dim iVS As Integer = 1
        Dim iAS As Integer = 1
        Dim iSS As Integer = 1
        Dim nFileInfo = dbElement.MainDetails.FileInfo

        If nFileInfo IsNot Nothing Then
            If nFileInfo.StreamDetailsSpecified Then
                If nFileInfo.StreamDetails.VideoSpecified Then strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(595, "Video Streams"), nFileInfo.StreamDetails.Video.Count.ToString, Environment.NewLine)
                If nFileInfo.StreamDetails.AudioSpecified Then strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(596, "Audio Streams"), nFileInfo.StreamDetails.Audio.Count.ToString, Environment.NewLine)
                If nFileInfo.StreamDetails.SubtitleSpecified Then strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(597, "Subtitle  Streams"), nFileInfo.StreamDetails.Subtitle.Count.ToString, Environment.NewLine)
                'video streams
                For Each miVideo As MediaContainers.Video In nFileInfo.StreamDetails.Video
                    strOutput.AppendFormat("{0}{1} {2}{0}", Environment.NewLine, Master.eLang.GetString(617, "Video Stream"), iVS)
                    If miVideo.WidthSpecified AndAlso miVideo.HeightSpecified Then strOutput.AppendFormat("- {0}{1}", String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), miVideo.Width, miVideo.Height), Environment.NewLine)
                    If miVideo.AspectSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(614, "Aspect Ratio"), miVideo.Aspect, Environment.NewLine)
                    If miVideo.ScantypeSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(605, "Scan Type"), miVideo.Scantype, Environment.NewLine)
                    If miVideo.CodecSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(604, "Codec"), miVideo.Codec, Environment.NewLine)
                    If miVideo.BitrateSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", "Bitrate", miVideo.Bitrate, Environment.NewLine)
                    If miVideo.DurationSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(609, "Duration"), miVideo.Duration, Environment.NewLine)
                    'for now return filesize in mbytes instead of bytes(default)
                    If dbElement.FileItemSpecified AndAlso dbElement.FileItem.TotalSizeSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(1455, "Filesize"), dbElement.FileItem.TotalSizeAsReadableString, Environment.NewLine)
                    If miVideo.LongLanguageSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(610, "Language"), miVideo.LongLanguage, Environment.NewLine)
                    If miVideo.MultiViewCountSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(1156, "MultiView Count"), miVideo.MultiViewCount, Environment.NewLine)
                    If miVideo.MultiViewLayoutSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(1157, "MultiView Layout"), miVideo.MultiViewLayout, Environment.NewLine)
                    If miVideo.StereoModeSpecified Then strOutput.AppendFormat("- {0}: {1} ({2})", Master.eLang.GetString(1286, "StereoMode"), miVideo.StereoMode, miVideo.ShortStereoMode)
                    iVS += 1
                Next

                strOutput.Append(Environment.NewLine)

                'audio streams
                For Each miAudio As MediaContainers.Audio In nFileInfo.StreamDetails.Audio
                    strOutput.AppendFormat("{0}{1} {2}{0}", Environment.NewLine, Master.eLang.GetString(618, "Audio Stream"), iAS.ToString)
                    If miAudio.CodecSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(604, "Codec"), miAudio.Codec, Environment.NewLine)
                    If miAudio.ChannelsSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(611, "Channels"), miAudio.Channels, Environment.NewLine)
                    If miAudio.BitrateSpecified Then strOutput.AppendFormat("- {0}: {1}{2}", "Bitrate", miAudio.Bitrate, Environment.NewLine)
                    If miAudio.LongLanguageSpecified Then strOutput.AppendFormat("- {0}: {1}", Master.eLang.GetString(610, "Language"), miAudio.LongLanguage)
                    iAS += 1
                Next

                strOutput.Append(Environment.NewLine)

                'subtitle streams
                For Each miSub As MediaContainers.Subtitle In nFileInfo.StreamDetails.Subtitle
                    strOutput.AppendFormat("{0}{1} {2}{0}", Environment.NewLine, Master.eLang.GetString(619, "Subtitle Stream"), iSS.ToString)
                    If miSub.LongLanguageSpecified Then strOutput.AppendFormat("- {0}: {1}", Master.eLang.GetString(610, "Language"), miSub.LongLanguage)
                    iSS += 1
                Next
            End If
        End If

        If strOutput.ToString.Trim.Length > 0 Then
            Return strOutput.ToString
        Else
            Return Master.eLang.GetString(419, "Metadata is not available. Try rescanning.")
        End If
        Return String.Empty
    End Function

    Public Shared Function GetBestVideo(ByVal miFIV As MediaContainers.Fileinfo) As MediaContainers.Video
        Dim nBestVideo = miFIV.StreamDetails.Video.OrderBy(Function(f) f.Width).Reverse.FirstOrDefault
        If nBestVideo IsNot Nothing Then
            Return nBestVideo
        Else
            Return New MediaContainers.Video
        End If
    End Function

    Public Shared Function GetDimensionsFromVideo(ByVal video As MediaContainers.Video) As String
        If video.WidthSpecified AndAlso video.HeightSpecified AndAlso video.AspectSpecified Then
            Return String.Format("{0}x{1} ({2})", video.Width, video.Height, video.Aspect)
        ElseIf video.WidthSpecified AndAlso video.HeightSpecified Then
            Return String.Format("{0}x{1}", video.Width, video.Height)
        End If
        Return String.Empty
    End Function
    ''' <summary>
    ''' Get the resolution of the video from the dimensions provided by MediaInfo.dll
    ''' </summary>
    ''' <param name="video"></param>
    ''' <returns></returns>
    Public Shared Function GetResolutionFromDimensions(ByVal video As MediaContainers.Video) As String
        Dim iWidth As Integer = video.Width
        Dim iHeight As Integer = video.Height
        Dim strResolution As String = String.Empty


        Select Case True
            'exact
            Case iWidth = 7680 AndAlso iHeight = 4320   'UHD 8K
                strResolution = "4320"
            Case iWidth = 4096 AndAlso iHeight = 2160   'UHD 4K (cinema)
                strResolution = "2160"
            Case iWidth = 3840 AndAlso iHeight = 2160   'UHD 4K
                strResolution = "2160"
            Case iWidth = 2560 AndAlso iHeight = 1600   'WQXGA (16:10)
                strResolution = "1600"
            Case iWidth = 2560 AndAlso iHeight = 1440   'WQHD (16:9)
                strResolution = "1440"
            Case iWidth = 1920 AndAlso iHeight = 1200   'WUXGA (16:10)
                strResolution = "1200"
            Case iWidth = 1920 AndAlso iHeight = 1080   'HD1080 (16:9)
                strResolution = "1080"
            Case iWidth = 1680 AndAlso iHeight = 1050   'WSXGA+ (16:10)
                strResolution = "1050"
            Case iWidth = 1600 AndAlso iHeight = 900    'HD+ (16:9)
                strResolution = "900"
            Case iWidth = 1280 AndAlso iHeight = 720    'HD720 / WXGA (16:9)
                strResolution = "720"
            Case iWidth = 800 AndAlso iHeight = 480     'Rec. 601 plus a quarter (5:3)
                strResolution = "480"
            Case iWidth = 768 AndAlso iHeight = 576     'PAL
                strResolution = "576"
            Case iWidth = 720 AndAlso iHeight = 480     'Rec. 601 (3:2)
                strResolution = "480"
            Case iWidth = 720 AndAlso iHeight = 576     'PAL (DVD)
                strResolution = "576"
            Case iWidth = 720 AndAlso iHeight = 540     'half of 1080p (16:9)
                strResolution = "540"
            Case iWidth = 640 AndAlso iHeight = 480     'VGA (4:3)
                strResolution = "480"
            Case iWidth = 640 AndAlso iHeight = 360     'Wide 360p (16:9)
                strResolution = "360"
            Case iWidth = 480 AndAlso iHeight = 360     '360p (4:3, uncommon)
                strResolution = "360"
            Case iWidth = 426 AndAlso iHeight = 240     'NTSC widescreen (16:9)
                strResolution = "240"
            Case iWidth = 352 AndAlso iHeight = 240     'NTSC-standard VCD / super-long-play DVD (4:3)
                strResolution = "240"
            Case iWidth = 320 AndAlso iHeight = 240     'CGA / NTSC square pixel (4:3)
                strResolution = "240"
            Case iWidth = 256 AndAlso iHeight = 144     'One tenth of 1440p (16:9)
                strResolution = "144"
            Case Else
                '
                ' MAM: simple version, totally sufficient. Add new res at the end of the list if they become available (before "99999999" of course!)
                ' Warning: this list needs to be sorted from lowest to highes resolution, else the search routine will go nuts!
                '
                Dim aVres() = New Dictionary(Of Integer, String) From
                        {
                        {0, "unknown"},
                        {426, "240"},
                        {480, "360"},
                        {640, "480"},
                        {720, "576"},
                        {1280, "720"},
                        {1920, "1080"},
                        {4096, "2160"},
                        {7680, "4320"},
                        {99999999, String.Empty}
                    }.ToArray
                '
                ' search appropriate horizontal resolution
                ' Note: Array's last entry must be a ridiculous high number, else this loop will surely crash!
                '
                Dim i As Integer
                While (aVres(i).Key < iWidth)
                    i = i + 1
                End While
                strResolution = aVres(i).Value
        End Select

        If Not String.IsNullOrEmpty(strResolution) AndAlso Not String.IsNullOrEmpty(video.Scantype) Then
            Return String.Concat(strResolution, If(video.Scantype.ToLower = "progressive", "p", "i"))
        End If

        Return strResolution
    End Function

    Private Shared Function OverwriteValue(ByVal scrapeOptionEnabled As Boolean,
                                           ByVal alreadyNewSet As Boolean,
                                           ByVal oldSpecified As Boolean,
                                           ByVal newSpecified As Boolean,
                                           ByVal settings As DataSpecificationItem) As Boolean
        Return scrapeOptionEnabled AndAlso
            Not alreadyNewSet AndAlso
            newSpecified AndAlso
            settings.Enabled AndAlso
            (Not oldSpecified OrElse Not settings.Locked)
    End Function

    Private Shared Sub ReorderPersons(ByRef lstPerson As List(Of MediaContainers.Person))
        Dim iOrder As Integer = 0
        For Each nPerson In lstPerson
            nPerson.Order = iOrder
            iOrder += 1
        Next
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Class KnownEpisode

#Region "Properties"

        Public Property AiredDate() As String = String.Empty

        Public Property Episode() As Integer = -1

        Public Property EpisodeAbsolute() As Integer = -1

        Public Property EpisodeCombined() As Double = -1

        Public Property EpisodeDVD() As Double = -1

        Public Property Season() As Integer = -1

        Public Property SeasonCombined() As Integer = -1

        Public Property SeasonDVD() As Integer = -1

#End Region 'Properties

    End Class

#End Region 'Nested Types

End Class