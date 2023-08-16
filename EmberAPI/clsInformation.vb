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

Public Class Information

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields

#Region "Methods"

    Private Shared Function ClearDatafield(ByVal settings As InformationItemBase, ByVal type As Enums.ContentType) As Boolean
        Select Case type
            Case Enums.ContentType.Movie
                Return Master.eSettings.Movie.InformationSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
            Case Enums.ContentType.Movieset
                Return Master.eSettings.Movieset.InformationSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
            Case Enums.ContentType.TVEpisode
                Return Master.eSettings.TVEpisode.InformationSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
            Case Enums.ContentType.TVSeason
                Return Master.eSettings.TVSeason.InformationSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
            Case Enums.ContentType.TVShow
                Return Master.eSettings.TVShow.InformationSettings.ClearDisabledFields AndAlso Not settings.Enabled AndAlso Not settings.Locked
        End Select
        Return False
    End Function

    Private Shared Sub Filter_OnlyPersonsWithImage(ByRef lstPerson As List(Of MediaContainers.Person))
        If lstPerson IsNot Nothing Then
            lstPerson = lstPerson.Where(Function(f) f.URLOriginalSpecified).ToList
        End If
    End Sub

    Private Shared Sub Filter_CountLimit(ByVal iLimit As Integer, ByRef lstPerson As List(Of MediaContainers.Person))
        If Not iLimit = 0 AndAlso iLimit < lstPerson.Count Then
            lstPerson.RemoveRange(iLimit, lstPerson.Count - iLimit)
        End If
    End Sub

    Private Shared Sub Filter_CountLimit(ByVal iLimit As Integer, ByRef lstString As List(Of String))
        If Not iLimit = 0 AndAlso iLimit < lstString.Count Then
            lstString.RemoveRange(iLimit, lstString.Count - iLimit)
        End If
    End Sub
    ''' <summary>
    ''' Returns the "merged" result of each data scraper results
    ''' </summary>
    ''' <param name="dbElement">DBElement to be scraped</param>
    ''' <param name="scraperResults"><c>List(Of MediaContainers.MainDetails)</c> which contains unfiltered results of each data scraper</param>
    ''' <returns>The scrape result of DBElement (after applying various global scraper settings here)</returns>
    ''' <remarks>
    ''' </remarks>
    Public Shared Function MergeResults(ByVal dbElement As Database.DBElement, ByVal scraperResults As List(Of MediaContainers.MainDetails)) As Database.DBElement
        Dim nScrapeOptions As Structures.ScrapeOptionsBase
        Dim nSettings As InformationBase
        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                nScrapeOptions = dbElement.ScrapeOptions
                nSettings = Master.eSettings.Movie.InformationSettings
            Case Enums.ContentType.Movieset
                nSettings = Master.eSettings.Movieset.InformationSettings
                nScrapeOptions = dbElement.ScrapeOptions
            Case Enums.ContentType.TVEpisode
                nSettings = Master.eSettings.TVEpisode.InformationSettings
                nScrapeOptions = dbElement.ScrapeOptions.Episodes
            Case Enums.ContentType.TVSeason
                nSettings = Master.eSettings.TVSeason.InformationSettings
                nScrapeOptions = dbElement.ScrapeOptions.Seasons
            Case Enums.ContentType.TVShow
                nSettings = Master.eSettings.TVShow.InformationSettings
                nScrapeOptions = dbElement.ScrapeOptions
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
            For Each cScraperData In scraperResults
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
                If OverwriteValue(nScrapeOptions.Actors, new_Actors, dbElement.MainDetails.ActorsSpecified, cScraperData.ActorsSpecified, .Actors) Then
                    If .Actors.WithImageOnly Then
                        Filter_OnlyPersonsWithImage(cScraperData.Actors)
                    End If
                    Filter_CountLimit(.Actors.Limit, cScraperData.Actors)
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
                If OverwriteValue(nScrapeOptions.Aired, new_Aired, dbElement.MainDetails.AiredSpecified, cScraperData.AiredSpecified, .Aired) Then
                    dbElement.MainDetails.Aired = cScraperData.Aired
                    new_Aired = True
                ElseIf ClearDatafield(.Aired, dbElement.ContentType) Then
                    dbElement.MainDetails.Aired = String.Empty
                End If

                'Certifications
                If OverwriteValue(nScrapeOptions.Certifications, new_Certifications, dbElement.MainDetails.CertificationsSpecified, cScraperData.CertificationsSpecified, .Certifications) Then
                    If .Certifications.Filter = Master.eLang.CommonWordsList.All Then
                        dbElement.MainDetails.Certifications = cScraperData.Certifications
                        new_Certifications = True
                    Else
                        Dim Country = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha2 = .Certifications.Filter)
                        If Country IsNot Nothing AndAlso Country.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(Country.Name) Then
                            For Each Certification In cScraperData.Certifications
                                If Certification.StartsWith(Country.Name) Then
                                    dbElement.MainDetails.Certifications.Clear()
                                    dbElement.MainDetails.Certifications.Add(Certification)
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
                If OverwriteValue(nScrapeOptions.Collection, new_Collection, dbElement.MainDetails.SetsSpecified, cScraperData.SetsSpecified, .Collection) Then
                    dbElement.MainDetails.Sets.Items.Clear()
                    For Each Movieset In cScraperData.Sets.Items
                        If Not String.IsNullOrEmpty(Movieset.Title) Then
                            For Each Mapping In Master.eSettings.MoviesetTitleRenaming
                                Movieset.Title = Movieset.Title.Replace(Mapping.Input, Mapping.MappedTo)
                            Next
                        End If
                    Next
                    dbElement.MainDetails.Sets.AddRange(cScraperData.Sets)
                    new_Collection = True
                End If

                'Countries
                If OverwriteValue(nScrapeOptions.Countries, new_Countries, dbElement.MainDetails.CountriesSpecified, cScraperData.CountriesSpecified, .Countries) Then
                    Filter_CountLimit(.Countries.Limit, cScraperData.Countries)
                    dbElement.MainDetails.Countries = cScraperData.Countries
                    new_Countries = True
                ElseIf ClearDatafield(.Countries, dbElement.ContentType) Then
                    dbElement.MainDetails.Countries.Clear()
                End If

                'Creators
                If OverwriteValue(nScrapeOptions.Creators, new_Creators, dbElement.MainDetails.CreatorsSpecified, cScraperData.CreatorsSpecified, .Creators) Then
                    dbElement.MainDetails.Creators = cScraperData.Creators
                    new_Creators = True
                ElseIf ClearDatafield(.Creators, dbElement.ContentType) Then
                    dbElement.MainDetails.Creators.Clear()
                End If

                'Credits
                If OverwriteValue(nScrapeOptions.Credits, new_Credits, dbElement.MainDetails.CreditsSpecified, cScraperData.CreditsSpecified, .Credits) Then
                    dbElement.MainDetails.Credits = cScraperData.Credits
                    new_Credits = True
                ElseIf ClearDatafield(.Credits, dbElement.ContentType) Then
                    dbElement.MainDetails.Credits.Clear()
                End If

                'Directors
                If OverwriteValue(nScrapeOptions.Directors, new_Directors, dbElement.MainDetails.DirectorsSpecified, cScraperData.DirectorsSpecified, .Directors) Then
                    dbElement.MainDetails.Directors = cScraperData.Directors
                    new_Directors = True
                ElseIf ClearDatafield(.Directors, dbElement.ContentType) Then
                    dbElement.MainDetails.Directors.Clear()
                End If

                'EpisodeGuideURL
                If OverwriteValue(nScrapeOptions.EpisodeGuideURL, new_EpisodeGuideURL, dbElement.MainDetails.EpisodeGuideURLSpecified, cScraperData.EpisodeGuideURLSpecified, .EpisodeGuideURL) Then
                    dbElement.MainDetails.EpisodeGuideURL = cScraperData.EpisodeGuideURL
                    new_EpisodeGuideURL = True
                ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowEpiGuideURL Then
                    dbElement.MainDetails.EpisodeGuideURL = New MediaContainers.EpisodeGuide
                End If

                'Genres
                If OverwriteValue(nScrapeOptions.Genres, new_Genres, dbElement.MainDetails.GenresSpecified, cScraperData.GenresSpecified, .Genres) Then
                    APIXML.GenreMappings.RunMapping(cScraperData.Genres)
                    Filter_CountLimit(.Genres.Limit, cScraperData.Genres)
                    dbElement.MainDetails.Genres = cScraperData.Genres
                    new_Genres = True
                ElseIf ClearDatafield(.Genres, dbElement.ContentType) Then
                    dbElement.MainDetails.Genres.Clear()
                End If

                'GuestStars
                If OverwriteValue(nScrapeOptions.GuestStars, new_GuestStars, dbElement.MainDetails.GuestStarsSpecified, cScraperData.GuestStarsSpecified, .GuestStars) Then
                    If .GuestStars.WithImageOnly Then
                        Filter_OnlyPersonsWithImage(cScraperData.GuestStars)
                    End If
                    Filter_CountLimit(.GuestStars.Limit, cScraperData.GuestStars)
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
                If OverwriteValue(nScrapeOptions.MPAA, new_MPAA, dbElement.MainDetails.MPAASpecified, cScraperData.MPAASpecified, .MPAA) Then
                    dbElement.MainDetails.MPAA = cScraperData.MPAA
                    new_MPAA = True
                ElseIf ClearDatafield(.MPAA, dbElement.ContentType) Then
                    dbElement.MainDetails.MPAA = String.Empty
                End If

                'OriginalTitle
                If OverwriteValue(nScrapeOptions.OriginalTitle, new_OriginalTitle, dbElement.MainDetails.OriginalTitleSpecified, cScraperData.OriginalTitleSpecified, .OriginalTitle) Then
                    dbElement.MainDetails.OriginalTitle = cScraperData.OriginalTitle
                    new_OriginalTitle = True
                ElseIf ClearDatafield(.OriginalTitle, dbElement.ContentType) Then
                    dbElement.MainDetails.OriginalTitle = String.Empty
                End If

                'Outline
                If OverwriteValue(nScrapeOptions.Outline, new_Outline, dbElement.MainDetails.OutlineSpecified, cScraperData.OutlineSpecified, .Outline) Then
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
                If OverwriteValue(nScrapeOptions.Plot, new_Plot, dbElement.MainDetails.PlotSpecified, cScraperData.PlotSpecified, .Plot) Then
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
                If OverwriteValue(nScrapeOptions.Premiered, new_Premiered, dbElement.MainDetails.PremieredSpecified, cScraperData.PremieredSpecified, .Premiered) Then
                    dbElement.MainDetails.Premiered = NumUtils.DateToISO8601Date(cScraperData.Premiered)
                    new_Premiered = True
                ElseIf ClearDatafield(.Premiered, dbElement.ContentType) Then
                    dbElement.MainDetails.Premiered = String.Empty
                End If

                'Ratings
                If OverwriteValue(nScrapeOptions.Ratings, False, dbElement.MainDetails.RatingsSpecified, cScraperData.RatingsSpecified, .Ratings) Then
                    'remove old entries that cannot be assigned to a source
                    dbElement.MainDetails.Ratings.Items.RemoveAll(Function(f) f.Type = "default")
                    'add new ratings
                    dbElement.MainDetails.Ratings.AddRange(cScraperData.Ratings)
                ElseIf ClearDatafield(.Ratings, dbElement.ContentType) Then
                    dbElement.MainDetails.Ratings.Items.Clear()
                    dbElement.MainDetails.Rating = String.Empty
                    dbElement.MainDetails.Votes = String.Empty
                End If

                'Runtime
                If OverwriteValue(nScrapeOptions.Runtime, new_Runtime, dbElement.MainDetails.RuntimeSpecified, cScraperData.RuntimeSpecified, .Runtime) Then
                    dbElement.MainDetails.Runtime = cScraperData.Runtime
                    new_Runtime = True
                ElseIf ClearDatafield(.Runtime, dbElement.ContentType) Then
                    dbElement.MainDetails.Runtime = String.Empty
                End If

                'Status
                If OverwriteValue(nScrapeOptions.Status, new_Status, dbElement.MainDetails.StatusSpecified, cScraperData.StatusSpecified, .Status) Then
                    dbElement.MainDetails.Status = cScraperData.Status
                    new_Status = True
                ElseIf ClearDatafield(.Status, dbElement.ContentType) Then
                    dbElement.MainDetails.Status = String.Empty
                End If

                'Studios
                If OverwriteValue(nScrapeOptions.Studios, new_Studios, dbElement.MainDetails.StudiosSpecified, cScraperData.StudiosSpecified, .Studios) Then
                    Filter_CountLimit(.Studios.Limit, cScraperData.Studios)
                    dbElement.MainDetails.Studios = cScraperData.Studios
                    new_Studios = True
                ElseIf ClearDatafield(.Studios, dbElement.ContentType) Then
                    dbElement.MainDetails.Studios.Clear()
                End If

                'Tagline
                If OverwriteValue(nScrapeOptions.Tagline, new_Tagline, dbElement.MainDetails.TaglineSpecified, cScraperData.TaglineSpecified, .Tagline) Then
                    dbElement.MainDetails.Tagline = cScraperData.Tagline
                    new_Tagline = True
                ElseIf ClearDatafield(.Tagline, dbElement.ContentType) Then
                    dbElement.MainDetails.Tagline = String.Empty
                End If

                'Tags
                If OverwriteValue(nScrapeOptions.Tags, new_Tags, dbElement.MainDetails.TagsSpecified, cScraperData.TagsSpecified, .Tags) Then
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
                If OverwriteValue(nScrapeOptions.Title, new_Title, dbElement.MainDetails.TitleSpecified, cScraperData.TitleSpecified, .Title) Then
                    If dbElement.ContentType = Enums.ContentType.Movieset Then
                        For Each Mapping In Master.eSettings.MoviesetTitleRenaming
                            dbElement.MainDetails.Title = dbElement.MainDetails.Title.Replace(Mapping.Input, Mapping.MappedTo)
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
                If OverwriteValue(nScrapeOptions.Top250, new_Top250, dbElement.MainDetails.Top250Specified, True, .Top250) Then
                    dbElement.MainDetails.Top250 = cScraperData.Top250
                    new_Top250 = If(cScraperData.Top250Specified, True, False)
                ElseIf ClearDatafield(.Top250, dbElement.ContentType) Then
                    dbElement.MainDetails.Top250 = 0
                End If

                'Trailer
                If OverwriteValue(nScrapeOptions.TrailerLink, new_Trailer, dbElement.MainDetails.TrailerSpecified, cScraperData.TrailerSpecified, .TrailerLink) Then
                    If .TrailerLink.SaveKodiCompatible AndAlso YouTube.UrlUtils.IsYouTubeUrl(cScraperData.Trailer) Then
                        dbElement.MainDetails.Trailer = StringUtils.ConvertFromYouTubeURLToKodiTrailerFormat(cScraperData.Trailer)
                    Else
                        dbElement.MainDetails.Trailer = cScraperData.Trailer
                    End If
                    new_Trailer = True
                ElseIf ClearDatafield(.TrailerLink, dbElement.ContentType) Then
                    dbElement.MainDetails.Trailer = String.Empty
                End If

                'User Rating
                If OverwriteValue(nScrapeOptions.UserRating, new_UserRatings, dbElement.MainDetails.UserRatingSpecified, cScraperData.UserRatingSpecified, .UserRating) Then
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
                If dbElement.ScrapeModifiers.withEpisodes Then
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
                tmpstring = If(Master.eSettings.Movie.InformationSettings.Certifications.Filter = "us", StringUtils.USACertToMPAA(String.Join(" / ", dbElement.MainDetails.Certifications.ToArray)), If(.CertificationsOnlyValue, String.Join(" / ", dbElement.MainDetails.Certifications.ToArray).Split(Convert.ToChar(":"))(1), String.Join(" / ", dbElement.MainDetails.Certifications.ToArray)))
                'only update DBMovie if scraped result is not empty/nothing!
                If Not String.IsNullOrEmpty(tmpstring) Then
                    dbElement.MainDetails.MPAA = tmpstring
                End If
            End If

            'MPAA value if MPAA is not available
            If Not dbElement.MainDetails.MPAASpecified AndAlso .MPAA.NotRatedValueSpecified Then
                dbElement.MainDetails.MPAA = .MPAA.NotRatedValue
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

            'TVShow Runtime for Episode Runtime
            If Not dbElement.MainDetails.RuntimeSpecified AndAlso Master.eSettings.TVScraperUseSRuntimeForEp AndAlso
               dbElement.TVShowDetailsSpecified AndAlso dbElement.TVShowDetails.RuntimeSpecified Then
                dbElement.MainDetails.Runtime = dbElement.TVShowDetails.Runtime
            End If

            'Seasons
            For Each KnownSeason As Integer In KnownSeasonsIndex
                'create a list of specified episode informations from all scrapers
                Dim ScrapedSeasonList As New List(Of MediaContainers.MainDetails)
                For Each nShow As MediaContainers.MainDetails In scraperResults
                    For Each nSeasonDetails As MediaContainers.MainDetails In nShow.KnownSeasons.Where(Function(f) f.Season = KnownSeason)
                        ScrapedSeasonList.Add(nSeasonDetails)
                    Next
                Next
                'check if we have already saved season information for this scraped season
                Dim lSeasonList = dbElement.Seasons.Where(Function(f) f.MainDetails.Season = KnownSeason)

                If lSeasonList IsNot Nothing AndAlso lSeasonList.Count > 0 Then
                    For Each nSeason As Database.DBElement In lSeasonList
                        MergeResults(nSeason, ScrapedSeasonList)
                    Next
                Else
                    'no existing season found -> add it as "missing" season
                    Dim mSeason As New Database.DBElement(Enums.ContentType.TVSeason) With {.MainDetails = New MediaContainers.MainDetails With {.Season = KnownSeason}}
                    mSeason = Master.DB.Load_TVShowInfoIntoDBElement(mSeason, dbElement)
                    dbElement.Seasons.Add(MergeResults(mSeason, ScrapedSeasonList))
                End If
            Next
            'add all season informations to TVShow (for saving season informations to tv show NFO)
            dbElement.MainDetails.Seasons.Seasons.Clear()
            For Each kSeason As Database.DBElement In dbElement.Seasons.OrderBy(Function(f) f.MainDetails.Season)
                dbElement.MainDetails.Seasons.Seasons.Add(kSeason.MainDetails)
            Next

            'Episodes
            If dbElement.ScrapeModifiers.withEpisodes Then
                'update the tvshow information for each local episode
                For Each lEpisode In dbElement.Episodes
                    lEpisode = Master.DB.Load_TVShowInfoIntoDBElement(lEpisode, dbElement)
                Next

                For Each KnownEpisode As KnownEpisode In KnownEpisodesIndex.OrderBy(Function(f) f.Episode).OrderBy(Function(f) f.Season)

                    'convert the episode and season number if needed
                    Dim iEpisode As Integer = -1
                    Dim iSeason As Integer = -1
                    Dim strAiredDate As String = KnownEpisode.AiredDate
                    If dbElement.EpisodeOrdering = Enums.EpisodeOrdering.Absolute Then
                        iEpisode = KnownEpisode.EpisodeAbsolute
                        iSeason = 1
                    ElseIf dbElement.EpisodeOrdering = Enums.EpisodeOrdering.DVD Then
                        iEpisode = CInt(KnownEpisode.EpisodeDVD)
                        iSeason = KnownEpisode.SeasonDVD
                    ElseIf dbElement.EpisodeOrdering = Enums.EpisodeOrdering.Standard Then
                        iEpisode = KnownEpisode.Episode
                        iSeason = KnownEpisode.Season
                    End If

                    If Not iEpisode = -1 AndAlso Not iSeason = -1 Then
                        'create a list of specified episode informations from all scrapers
                        Dim ScrapedEpisodeList As New List(Of MediaContainers.MainDetails)
                        For Each nShow As MediaContainers.MainDetails In scraperResults
                            For Each nEpisodeDetails As MediaContainers.MainDetails In nShow.KnownEpisodes.Where(Function(f) f.Episode = KnownEpisode.Episode AndAlso f.Season = KnownEpisode.Season)
                                ScrapedEpisodeList.Add(nEpisodeDetails)
                            Next
                        Next

                        'check if we have a local episode file for this scraped episode
                        Dim lEpisodeList = dbElement.Episodes.Where(Function(f) f.FilenameSpecified AndAlso f.MainDetails.Episode = iEpisode AndAlso f.MainDetails.Season = iSeason)

                        If lEpisodeList IsNot Nothing AndAlso lEpisodeList.Count > 0 Then
                            For Each nEpisode As Database.DBElement In lEpisodeList
                                MergeResults(nEpisode, ScrapedEpisodeList)
                            Next
                        Else
                            'try to get the episode by AiredDate
                            Dim dEpisodeList = dbElement.Episodes.Where(Function(f) f.FilenameSpecified AndAlso
                                                                       f.MainDetails.Episode = -1 AndAlso
                                                                       f.MainDetails.AiredSpecified AndAlso
                                                                       f.MainDetails.Aired = strAiredDate)

                            If dEpisodeList IsNot Nothing AndAlso dEpisodeList.Count > 0 Then
                                For Each nEpisode As Database.DBElement In dEpisodeList
                                    MergeResults(nEpisode, ScrapedEpisodeList)
                                    'we have to add the proper season and episode number if the episode was found by AiredDate
                                    nEpisode.MainDetails.Episode = iEpisode
                                    nEpisode.MainDetails.Season = iSeason
                                Next
                            Else
                                'no local episode found -> add it as "missing" episode
                                Dim mEpisode As New Database.DBElement(Enums.ContentType.TVEpisode) With {.MainDetails = New MediaContainers.MainDetails With {.Episode = iEpisode, .Season = iSeason}}
                                mEpisode = Master.DB.Load_TVShowInfoIntoDBElement(mEpisode, dbElement)
                                MergeResults(mEpisode, ScrapedEpisodeList)
                                If mEpisode.MainDetails.TitleSpecified Then
                                    dbElement.Episodes.Add(mEpisode)
                                Else
                                    _Logger.Warn(String.Format("Missing Episode Ignored | {0} - S{1}E{2} | No Episode Title found", mEpisode.TVShowDetails.Title, mEpisode.MainDetails.Season, mEpisode.MainDetails.Episode))
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
                tmpAllSeasons = Master.DB.Load_TVShowInfoIntoDBElement(tmpAllSeasons, dbElement)
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

        'fallback for movieset title if title is empty
        If dbElement.ContentType = Enums.ContentType.Movieset AndAlso Not dbElement.MainDetails.TitleSpecified Then
            dbElement.MainDetails.Title = "No Title (ERROR)"
        End If

        Return dbElement
    End Function
    ''' <summary>
    ''' Returns the "merged" result of each data scraper results
    ''' </summary>
    ''' <param name="dbElement">Movie to be scraped</param>
    ''' <param name="scrapedList"><c>List(Of MediaContainers.Movie)</c> which contains unfiltered results of each data scraper</param>
    ''' <returns>The scrape result of movie (after applying various global scraper settings here)</returns>
    ''' <remarks>
    ''' This is used to determine the result of data scraping by going through all scraperesults of every data scraper and applying global data scraper settings here!
    ''' 
    ''' 2014/09/01 Cocotus - First implementation: Moved all global lock settings in various data scrapers to this function, only apply them once and not in every data scraper module! Should be more maintainable!
    ''' </remarks>
    Public Shared Function MergeDataScraperResults_Movie(ByVal dbElement As Database.DBElement, ByVal scrapedList As List(Of MediaContainers.MainDetails)) As Database.DBElement
        'Dim scrapeType = dbElement.ScrapeType
        'Dim scrapeOptions = dbElement.ScrapeOptions

        ''protects the first scraped result against overwriting
        'Dim new_Actors As Boolean = False
        'Dim new_Certification As Boolean = False
        'Dim new_CollectionID As Boolean = False
        'Dim new_Collections As Boolean = False
        'Dim new_Countries As Boolean = False
        'Dim new_Credits As Boolean = False
        'Dim new_Directors As Boolean = False
        'Dim new_Genres As Boolean = False
        'Dim new_MPAA As Boolean = False
        'Dim new_OriginalTitle As Boolean = False
        'Dim new_Outline As Boolean = False
        'Dim new_Plot As Boolean = False
        'Dim new_Premiered As Boolean = False
        'Dim new_Runtime As Boolean = False
        'Dim new_Studio As Boolean = False
        'Dim new_Tagline As Boolean = False
        'Dim new_Title As Boolean = False
        'Dim new_Top250 As Boolean = False
        'Dim new_Trailer As Boolean = False
        'Dim new_UserRating As Boolean = False

        'For Each scrapedmovie In scrapedList

        '    'UniqueIDs
        '    If scrapedmovie.UniqueIDsSpecified Then
        '        dbElement.MainDetails.UniqueIDs.AddRange(scrapedmovie.UniqueIDs)
        '    End If

        '    'Actors
        '    If (Not dbElement.MainDetails.ActorsSpecified OrElse Not Master.eSettings.MovieLockActors) AndAlso scrapeOptions.Actors AndAlso
        '        scrapedmovie.ActorsSpecified AndAlso Master.eSettings.MovieScraperCast AndAlso Not new_Actors Then

        '        If Master.eSettings.MovieScraperCastWithImgOnly Then
        '            Filter_OnlyPersonsWithImage(scrapedmovie.Actors)
        '        End If

        '        Filter_CountLimit(Master.eSettings.MovieScraperCastLimit, scrapedmovie.Actors)

        '        'added check if there's any actors left to add, if not then try with results of following scraper...
        '        If scrapedmovie.ActorsSpecified Then
        '            ReorderPersons(scrapedmovie.Actors)
        '            dbElement.MainDetails.Actors = scrapedmovie.Actors
        '            new_Actors = True
        '        End If

        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCast AndAlso Not Master.eSettings.MovieLockActors Then
        '        dbElement.MainDetails.Actors.Clear()
        '    End If

        '    'Certification
        '    If (Not dbElement.MainDetails.CertificationsSpecified OrElse Not Master.eSettings.MovieLockCert) AndAlso scrapeOptions.Certifications AndAlso
        '        scrapedmovie.CertificationsSpecified AndAlso Master.eSettings.MovieScraperCert AndAlso Not new_Certification Then

        '        If Master.eSettings.MovieScraperCertCountry = Master.eLang.CommonWordsList.All Then
        '            dbElement.MainDetails.Certifications = scrapedmovie.Certifications
        '            new_Certification = True
        '        Else
        '            Dim Country = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha2 = Master.eSettings.MovieScraperCertCountry)
        '            If Country IsNot Nothing AndAlso Country.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(Country.Name) Then
        '                For Each tCert In scrapedmovie.Certifications
        '                    If tCert.StartsWith(Country.Name) Then
        '                        dbElement.MainDetails.Certifications.Clear()
        '                        dbElement.MainDetails.Certifications.Add(tCert)
        '                        new_Certification = True
        '                        Exit For
        '                    End If
        '                Next
        '            Else
        '                _Logger.Error("Movie Certification Language (Limit) not found. Please check your settings!")
        '            End If
        '        End If
        '        APIXML.CertificationMappings.RunMapping(dbElement.MainDetails.Certifications)
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCert AndAlso Not Master.eSettings.MovieLockCert Then
        '        dbElement.MainDetails.Certifications.Clear()
        '    End If

        '    'Credits
        '    If (Not dbElement.MainDetails.CreditsSpecified OrElse Not Master.eSettings.MovieLockCredits) AndAlso
        '        scrapedmovie.CreditsSpecified AndAlso Master.eSettings.MovieScraperCredits AndAlso Not new_Credits Then
        '        dbElement.MainDetails.Credits = scrapedmovie.Credits
        '        new_Credits = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCredits AndAlso Not Master.eSettings.MovieLockCredits Then
        '        dbElement.MainDetails.Credits.Clear()
        '    End If

        '    'Collection ID
        '    If (Not dbElement.MainDetails.UniqueIDs.TMDbCollectionIdSpecified OrElse Not Master.eSettings.MovieLockCollectionID) AndAlso scrapeOptions.Collection AndAlso
        '        scrapedmovie.UniqueIDs.TMDbCollectionIdSpecified AndAlso Master.eSettings.MovieScraperCollectionID AndAlso Not new_CollectionID Then
        '        dbElement.MainDetails.UniqueIDs.TMDbCollectionId = scrapedmovie.UniqueIDs.TMDbCollectionId
        '        new_CollectionID = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCollectionID AndAlso Not Master.eSettings.MovieLockCollectionID Then
        '        dbElement.MainDetails.UniqueIDs.TMDbCollectionId = -1
        '    End If

        '    'Collections
        '    If (Not dbElement.MainDetails.SetsSpecified OrElse Not Master.eSettings.MovieLockCollections) AndAlso
        '        scrapedmovie.SetsSpecified AndAlso Master.eSettings.MovieScraperCollectionsAuto AndAlso Not new_Collections Then
        '        dbElement.MainDetails.Sets.Items.Clear()
        '        For Each movieset In scrapedmovie.Sets.Items
        '            If Not String.IsNullOrEmpty(movieset.Title) Then
        '                For Each sett In Master.eSettings.MoviesetTitleRenaming
        '                    movieset.Title = movieset.Title.Replace(sett.Input, sett.MappedTo)
        '                Next
        '            End If
        '        Next
        '        dbElement.MainDetails.Sets.AddRange(scrapedmovie.Sets)
        '        new_Collections = True
        '    End If

        '    'Countries
        '    If (Not dbElement.MainDetails.CountriesSpecified OrElse Not Master.eSettings.MovieLockCountry) AndAlso scrapeOptions.Countries AndAlso
        '        scrapedmovie.CountriesSpecified AndAlso Master.eSettings.MovieScraperCountry AndAlso Not new_Countries Then

        '        Filter_CountLimit(Master.eSettings.MovieScraperCountryLimit, scrapedmovie.Countries)

        '        dbElement.MainDetails.Countries = scrapedmovie.Countries
        '        APIXML.CountryMappings.RunMapping(dbElement.MainDetails.Countries)
        '        new_Countries = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCountry AndAlso Not Master.eSettings.MovieLockCountry Then
        '        dbElement.MainDetails.Countries.Clear()
        '    End If

        '    'Directors
        '    If (Not dbElement.MainDetails.DirectorsSpecified OrElse Not Master.eSettings.MovieLockDirector) AndAlso scrapeOptions.Directors AndAlso
        '        scrapedmovie.DirectorsSpecified AndAlso Master.eSettings.MovieScraperDirector AndAlso Not new_Directors Then
        '        dbElement.MainDetails.Directors = scrapedmovie.Directors
        '        new_Directors = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperDirector AndAlso Not Master.eSettings.MovieLockDirector Then
        '        dbElement.MainDetails.Directors.Clear()
        '    End If

        '    'Genres
        '    If (Not dbElement.MainDetails.GenresSpecified OrElse Not Master.eSettings.MovieLockGenre) AndAlso scrapeOptions.Genres AndAlso
        '        scrapedmovie.GenresSpecified AndAlso Master.eSettings.MovieScraperGenre AndAlso Not new_Genres Then

        '        APIXML.GenreMappings.RunMapping(scrapedmovie.Genres)

        '        Filter_CountLimit(Master.eSettings.MovieScraperGenreLimit, scrapedmovie.Genres)

        '        dbElement.MainDetails.Genres = scrapedmovie.Genres
        '        new_Genres = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperGenre AndAlso Not Master.eSettings.MovieLockGenre Then
        '        dbElement.MainDetails.Genres.Clear()
        '    End If

        '    'MPAA
        '    If (Not dbElement.MainDetails.MPAASpecified OrElse Not Master.eSettings.MovieLockMPAA) AndAlso scrapeOptions.MPAA AndAlso
        '        scrapedmovie.MPAASpecified AndAlso Master.eSettings.MovieScraperMPAA AndAlso Not new_MPAA Then
        '        dbElement.MainDetails.MPAA = scrapedmovie.MPAA
        '        new_MPAA = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperMPAA AndAlso Not Master.eSettings.MovieLockMPAA Then
        '        dbElement.MainDetails.MPAA = String.Empty
        '    End If

        '    'Originaltitle
        '    If (Not dbElement.MainDetails.OriginalTitleSpecified OrElse Not Master.eSettings.MovieLockOriginalTitle) AndAlso scrapeOptions.OriginalTitle AndAlso
        '        scrapedmovie.OriginalTitleSpecified AndAlso Master.eSettings.MovieScraperOriginalTitle AndAlso Not new_OriginalTitle Then
        '        dbElement.MainDetails.OriginalTitle = scrapedmovie.OriginalTitle
        '        new_OriginalTitle = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperOriginalTitle AndAlso Not Master.eSettings.MovieLockOriginalTitle Then
        '        dbElement.MainDetails.OriginalTitle = String.Empty
        '    End If

        '    'Outline
        '    If (Not dbElement.MainDetails.OutlineSpecified OrElse Not Master.eSettings.MovieLockOutline) AndAlso scrapeOptions.Outline AndAlso
        '        scrapedmovie.OutlineSpecified AndAlso Master.eSettings.MovieScraperOutline AndAlso Not new_Outline Then
        '        dbElement.MainDetails.Outline = scrapedmovie.Outline
        '        new_Outline = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperOutline AndAlso Not Master.eSettings.MovieLockOutline Then
        '        dbElement.MainDetails.Outline = String.Empty
        '    End If
        '    'check if brackets should be removed...
        '    If Master.eSettings.MovieScraperCleanPlotOutline Then
        '        dbElement.MainDetails.Outline = StringUtils.RemoveBrackets(dbElement.MainDetails.Outline)
        '    End If

        '    'Plot
        '    If (Not dbElement.MainDetails.PlotSpecified OrElse Not Master.eSettings.MovieLockPlot) AndAlso scrapeOptions.Plot AndAlso
        '        scrapedmovie.PlotSpecified AndAlso Master.eSettings.MovieScraperPlot AndAlso Not new_Plot Then
        '        dbElement.MainDetails.Plot = scrapedmovie.Plot
        '        new_Plot = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperPlot AndAlso Not Master.eSettings.MovieLockPlot Then
        '        dbElement.MainDetails.Plot = String.Empty
        '    End If
        '    'check if brackets should be removed...
        '    If Master.eSettings.MovieScraperCleanPlotOutline Then
        '        dbElement.MainDetails.Plot = StringUtils.RemoveBrackets(dbElement.MainDetails.Plot)
        '    End If

        '    'Premiered
        '    If (Not dbElement.MainDetails.PremieredSpecified OrElse Not Master.eSettings.MovieLockPremiered) AndAlso scrapeOptions.Premiered AndAlso
        '        scrapedmovie.PremieredSpecified AndAlso Master.eSettings.MovieScraperPremiered AndAlso Not new_Premiered Then
        '        dbElement.MainDetails.Premiered = NumUtils.DateToISO8601Date(scrapedmovie.Premiered)
        '        new_Premiered = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperPremiered AndAlso Not Master.eSettings.MovieLockPremiered Then
        '        dbElement.MainDetails.Premiered = String.Empty
        '    End If

        '    'Ratings
        '    If scrapeOptions.Ratings AndAlso scrapedmovie.RatingsSpecified AndAlso Master.eSettings.MovieScraperRating Then
        '        'remove old entries that cannot be assigned to a source
        '        dbElement.MainDetails.Ratings.Items.RemoveAll(Function(f) f.Type = "default")
        '        'add new ratings
        '        dbElement.MainDetails.Ratings.AddRange(scrapedmovie.Ratings)
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRating AndAlso Not Master.eSettings.MovieLockRating Then
        '        dbElement.MainDetails.Ratings.Items.Clear()
        '    End If

        '    'Studios
        '    If (Not dbElement.MainDetails.StudiosSpecified OrElse Not Master.eSettings.MovieLockStudio) AndAlso scrapeOptions.Studios AndAlso
        '        scrapedmovie.StudiosSpecified AndAlso Master.eSettings.MovieScraperStudio AndAlso Not new_Studio Then
        '        dbElement.MainDetails.Studios.Clear()

        '        Dim _studios As New List(Of String)
        '        _studios.AddRange(scrapedmovie.Studios)
        '        APIXML.StudioMappings.RunMapping(_studios)

        '        If Master.eSettings.MovieScraperStudioWithImgOnly Then
        '            For i = _studios.Count - 1 To 0 Step -1
        '                If APIXML.StudioIcons.ContainsKey(_studios.Item(i).ToLower) = False Then
        '                    _studios.RemoveAt(i)
        '                End If
        '            Next
        '        End If

        '        Filter_CountLimit(Master.eSettings.MovieScraperStudioLimit, _studios)

        '        dbElement.MainDetails.Studios.AddRange(_studios)
        '        'added check if there's any studios left to add, if not then try with results of following scraper...
        '        If _studios.Count > 0 Then
        '            new_Studio = True
        '        End If

        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperStudio AndAlso Not Master.eSettings.MovieLockStudio Then
        '        dbElement.MainDetails.Studios.Clear()
        '    End If

        '    'Tagline
        '    If (Not dbElement.MainDetails.TaglineSpecified OrElse Not Master.eSettings.MovieLockTagline) AndAlso scrapeOptions.Tagline AndAlso
        '        scrapedmovie.TaglineSpecified AndAlso Master.eSettings.MovieScraperTagline AndAlso Not new_Tagline Then
        '        dbElement.MainDetails.Tagline = scrapedmovie.Tagline
        '        new_Tagline = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTagline AndAlso Not Master.eSettings.MovieLockTagline Then
        '        dbElement.MainDetails.Tagline = String.Empty
        '    End If

        '    'Title
        '    If (Not dbElement.MainDetails.TitleSpecified OrElse Not Master.eSettings.MovieLockTitle) AndAlso scrapeOptions.Title AndAlso
        '        scrapedmovie.TitleSpecified AndAlso Master.eSettings.MovieScraperTitle AndAlso Not new_Title Then
        '        dbElement.MainDetails.Title = scrapedmovie.Title
        '        new_Title = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTitle AndAlso Not Master.eSettings.MovieLockTitle Then
        '        dbElement.MainDetails.Title = String.Empty
        '    End If

        '    'Top250 (special handling: no check if "scrapedmovie.Top250Specified" and only set "new_Top250 = True" if a value over 0 has been set)
        '    If (Not dbElement.MainDetails.Top250Specified OrElse Not Master.eSettings.MovieLockTop250) AndAlso scrapeOptions.Top250 AndAlso
        '        Master.eSettings.MovieScraperTop250 AndAlso Not new_Top250 Then
        '        dbElement.MainDetails.Top250 = scrapedmovie.Top250
        '        new_Top250 = If(scrapedmovie.Top250Specified, True, False)
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTop250 AndAlso Not Master.eSettings.MovieLockTop250 Then
        '        dbElement.MainDetails.Top250 = 0
        '    End If

        '    'Trailer
        '    If (Not dbElement.MainDetails.TrailerSpecified OrElse Not Master.eSettings.MovieLockTrailer) AndAlso scrapeOptions.Trailer AndAlso
        '        scrapedmovie.TrailerSpecified AndAlso Master.eSettings.MovieScraperTrailer AndAlso Not new_Trailer Then
        '        If Master.eSettings.MovieScraperXBMCTrailerFormat AndAlso YouTube.UrlUtils.IsYouTubeUrl(scrapedmovie.Trailer) Then
        '            dbElement.MainDetails.Trailer = StringUtils.ConvertFromYouTubeURLToKodiTrailerFormat(scrapedmovie.Trailer)
        '        Else
        '            dbElement.MainDetails.Trailer = scrapedmovie.Trailer
        '        End If
        '        new_Trailer = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTrailer AndAlso Not Master.eSettings.MovieLockTrailer Then
        '        dbElement.MainDetails.Trailer = String.Empty
        '    End If

        '    'User Rating
        '    If (Not dbElement.MainDetails.UserRatingSpecified OrElse Not Master.eSettings.MovieLockUserRating) AndAlso scrapeOptions.UserRating AndAlso
        '        scrapedmovie.UserRatingSpecified AndAlso Master.eSettings.MovieScraperUserRating AndAlso Not new_UserRating Then
        '        dbElement.MainDetails.UserRating = scrapedmovie.UserRating
        '        new_UserRating = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperUserRating AndAlso Not Master.eSettings.MovieLockUserRating Then
        '        dbElement.MainDetails.UserRating = 0
        '    End If

        '    'Runtime
        '    If (Not dbElement.MainDetails.RuntimeSpecified OrElse Not Master.eSettings.MovieLockRuntime) AndAlso scrapeOptions.Runtime AndAlso
        '        scrapedmovie.RuntimeSpecified AndAlso Master.eSettings.MovieScraperRuntime AndAlso Not new_Runtime Then
        '        dbElement.MainDetails.Runtime = scrapedmovie.Runtime
        '        new_Runtime = True
        '    ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRuntime AndAlso Not Master.eSettings.MovieLockRuntime Then
        '        dbElement.MainDetails.Runtime = String.Empty
        '    End If

        'Next

        ''Certification for MPAA
        'If dbElement.MainDetails.CertificationsSpecified AndAlso Master.eSettings.MovieScraperCertForMPAA AndAlso
        '    (Not Master.eSettings.MovieScraperCertForMPAAFallback AndAlso (Not dbElement.MainDetails.MPAASpecified OrElse Not Master.eSettings.MovieLockMPAA) OrElse
        '     Not new_MPAA AndAlso (Not dbElement.MainDetails.MPAASpecified OrElse Not Master.eSettings.MovieLockMPAA)) Then

        '    Dim tmpstring As String = String.Empty
        '    tmpstring = If(Master.eSettings.MovieScraperCertCountry = "us", StringUtils.USACertToMPAA(String.Join(" / ", dbElement.MainDetails.Certifications.ToArray)), If(Master.eSettings.MovieScraperCertOnlyValue, String.Join(" / ", dbElement.MainDetails.Certifications.ToArray).Split(Convert.ToChar(":"))(1), String.Join(" / ", dbElement.MainDetails.Certifications.ToArray)))
        '    'only update DBMovie if scraped result is not empty/nothing!
        '    If Not String.IsNullOrEmpty(tmpstring) Then
        '        dbElement.MainDetails.MPAA = tmpstring
        '    End If
        'End If

        ''MPAA value if MPAA is not available
        'If Not dbElement.MainDetails.MPAASpecified AndAlso Not String.IsNullOrEmpty(Master.eSettings.MovieScraperMPAANotRated) Then
        '    dbElement.MainDetails.MPAA = Master.eSettings.MovieScraperMPAANotRated
        'End If

        ''OriginalTitle as Title
        'If (Not dbElement.MainDetails.TitleSpecified OrElse Not Master.eSettings.MovieLockTitle) AndAlso Master.eSettings.MovieScraperOriginalTitleAsTitle AndAlso dbElement.MainDetails.OriginalTitleSpecified Then
        '    dbElement.MainDetails.Title = dbElement.MainDetails.OriginalTitle
        'End If

        ''Plot for Outline
        'If ((Not dbElement.MainDetails.OutlineSpecified OrElse Not Master.eSettings.MovieLockOutline) AndAlso Master.eSettings.MovieScraperPlotForOutline AndAlso Not Master.eSettings.MovieScraperPlotForOutlineIfEmpty) OrElse
        '    (Not dbElement.MainDetails.OutlineSpecified AndAlso Master.eSettings.MovieScraperPlotForOutline AndAlso Master.eSettings.MovieScraperPlotForOutlineIfEmpty) Then
        '    dbElement.MainDetails.Outline = StringUtils.ShortenOutline(dbElement.MainDetails.Plot, Master.eSettings.MovieScraperOutlineLimit)
        'End If

        ''sort Ratings by Default and Type (name)
        'dbElement.MainDetails.Ratings.Items.Sort()

        ''sort UniqueIds by Default and Type (name)
        'dbElement.MainDetails.UniqueIDs.Items.Sort()

        ''fallback for title if title is empty
        'If Not dbElement.MainDetails.TitleSpecified Then
        '    dbElement.MainDetails.Title = StringUtils.FilterTitleFromPath_Movie(dbElement.Filename, dbElement.IsSingle, dbElement.Source.UseFolderName)
        'End If

        Return dbElement
    End Function
    ''' <summary>
    ''' Returns the "merged" result of each data scraper results
    ''' </summary>
    ''' <param name="dbElement">TV Show to be scraped</param>
    ''' <param name="scrapedList"><c>List(Of MediaContainers.TVShow)</c> which contains unfiltered results of each data scraper</param>
    ''' <returns>The scrape result of movie (after applying various global scraper settings here)</returns>
    ''' <remarks>
    ''' This is used to determine the result of data scraping by going through all scraperesults of every data scraper and applying global data scraper settings here!
    ''' 
    ''' 2014/09/01 Cocotus - First implementation: Moved all global lock settings in various data scrapers to this function, only apply them once and not in every data scraper module! Should be more maintainable!
    ''' </remarks>
    Public Shared Function MergeDataScraperResults_TV(ByVal dbElement As Database.DBElement, ByVal scrapedList As List(Of MediaContainers.MainDetails)) As Database.DBElement
        Dim scrapeType = dbElement.ScrapeType
        Dim scrapeOptions = dbElement.ScrapeOptions
        Dim withEpisodes = dbElement.ScrapeModifiers.withEpisodes

        'protects the first scraped result against overwriting
        Dim new_Actors As Boolean = False
        Dim new_Certification As Boolean = False
        Dim new_Creators As Boolean = False
        Dim new_Countries As Boolean = False
        Dim new_Credits As Boolean = False
        Dim new_Genres As Boolean = False
        Dim new_MPAA As Boolean = False
        Dim new_OriginalTitle As Boolean = False
        Dim new_Plot As Boolean = False
        Dim new_Premiered As Boolean = False
        Dim new_Runtime As Boolean = False
        Dim new_Status As Boolean = False
        Dim new_Studio As Boolean = False
        Dim new_Tagline As Boolean = False
        Dim new_Title As Boolean = False
        Dim new_UserRating As Boolean = False

        Dim KnownEpisodesIndex As New List(Of KnownEpisode)
        Dim KnownSeasonsIndex As New List(Of Integer)

        For Each scrapedshow In scrapedList

            'UniqueIDs
            If scrapedshow.UniqueIDsSpecified Then
                dbElement.MainDetails.UniqueIDs.AddRange(scrapedshow.UniqueIDs)
            End If

            'Actors
            If (Not dbElement.MainDetails.ActorsSpecified OrElse Not Master.eSettings.TVLockShowActors) AndAlso scrapeOptions.Actors AndAlso
                scrapedshow.ActorsSpecified AndAlso Master.eSettings.TVScraperShowActors AndAlso Not new_Actors Then

                If Master.eSettings.TVScraperCastWithImgOnly Then
                    Filter_OnlyPersonsWithImage(scrapedshow.Actors)
                End If
                Filter_CountLimit(Master.eSettings.TVScraperShowActorsLimit, scrapedshow.Actors)
                'added check if there's any actors left to add, if not then try with results of following scraper...
                If scrapedshow.ActorsSpecified Then
                    ReorderPersons(scrapedshow.Actors)
                    dbElement.MainDetails.Actors = scrapedshow.Actors
                    new_Actors = True
                End If
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowActors AndAlso Not Master.eSettings.TVLockShowActors Then
                dbElement.MainDetails.Actors.Clear()
            End If

            'Certification
            If (Not dbElement.MainDetails.CertificationsSpecified OrElse Not Master.eSettings.TVLockShowCert) AndAlso scrapeOptions.Certifications AndAlso
                scrapedshow.CertificationsSpecified AndAlso Master.eSettings.TVScraperShowCert AndAlso Not new_Certification Then

                If Master.eSettings.TVScraperShowCertCountry = Master.eLang.CommonWordsList.All Then
                    dbElement.MainDetails.Certifications = scrapedshow.Certifications
                    new_Certification = True
                Else
                    Dim Country = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha2 = Master.eSettings.TVScraperShowCertCountry)
                    If Country IsNot Nothing AndAlso Country.Name IsNot Nothing AndAlso Not String.IsNullOrEmpty(Country.Name) Then
                        For Each tCert In scrapedshow.Certifications
                            If tCert.StartsWith(Country.Name) Then
                                dbElement.MainDetails.Certifications.Clear()
                                dbElement.MainDetails.Certifications.Add(tCert)
                                new_Certification = True
                                Exit For
                            End If
                        Next
                    Else
                        _Logger.Error("TV Show Certification Language (Limit) not found. Please check your settings!")
                    End If
                End If
                APIXML.CertificationMappings.RunMapping(dbElement.MainDetails.Certifications)
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowCert AndAlso Not Master.eSettings.TVLockShowCert Then
                dbElement.MainDetails.Certifications.Clear()
            End If

            'Creators
            If (Not dbElement.MainDetails.CreatorsSpecified OrElse Not Master.eSettings.TVLockShowCreators) AndAlso scrapeOptions.Creators AndAlso
                scrapedshow.CreatorsSpecified AndAlso Master.eSettings.TVScraperShowCreators AndAlso Not new_Creators Then
                dbElement.MainDetails.Creators = scrapedshow.Creators
                new_Creators = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowCreators AndAlso Not Master.eSettings.TVLockShowCreators Then
                dbElement.MainDetails.Creators.Clear()
            End If

            'Countries
            If (Not dbElement.MainDetails.CountriesSpecified OrElse Not Master.eSettings.TVLockShowCountry) AndAlso scrapeOptions.Countries AndAlso
                scrapedshow.CountriesSpecified AndAlso Master.eSettings.TVScraperShowCountry AndAlso Not new_Countries Then

                Filter_CountLimit(Master.eSettings.TVScraperShowCountryLimit, scrapedshow.Countries)

                dbElement.MainDetails.Countries = scrapedshow.Countries
                APIXML.CountryMappings.RunMapping(dbElement.MainDetails.Countries)
                new_Countries = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowCountry AndAlso Not Master.eSettings.TVLockShowCountry Then
                dbElement.MainDetails.Countries.Clear()
            End If

            'EpisodeGuideURL
            If scrapeOptions.EpisodeGuideURL AndAlso scrapedshow.EpisodeGuideURLSpecified AndAlso Master.eSettings.TVScraperShowEpiGuideURL Then
                dbElement.MainDetails.EpisodeGuideURL = scrapedshow.EpisodeGuideURL
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowEpiGuideURL Then
                dbElement.MainDetails.EpisodeGuideURL = New MediaContainers.EpisodeGuide
            End If

            'Genres
            If (Not dbElement.MainDetails.GenresSpecified OrElse Not Master.eSettings.TVLockShowGenre) AndAlso scrapeOptions.Genres AndAlso
                scrapedshow.GenresSpecified AndAlso Master.eSettings.TVScraperShowGenre AndAlso Not new_Genres Then

                APIXML.GenreMappings.RunMapping(scrapedshow.Genres)

                Filter_CountLimit(Master.eSettings.TVScraperShowGenreLimit, scrapedshow.Genres)

                dbElement.MainDetails.Genres = scrapedshow.Genres
                new_Genres = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowGenre AndAlso Not Master.eSettings.TVLockShowGenre Then
                dbElement.MainDetails.Genres.Clear()
            End If

            'MPAA
            If (Not dbElement.MainDetails.MPAASpecified OrElse Not Master.eSettings.TVLockShowMPAA) AndAlso scrapeOptions.MPAA AndAlso
              scrapedshow.MPAASpecified AndAlso Master.eSettings.TVScraperShowMPAA AndAlso Not new_MPAA Then
                dbElement.MainDetails.MPAA = scrapedshow.MPAA
                new_MPAA = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowMPAA AndAlso Not Master.eSettings.TVLockShowMPAA Then
                dbElement.MainDetails.MPAA = String.Empty
            End If

            'Originaltitle
            If (Not dbElement.MainDetails.OriginalTitleSpecified OrElse Not Master.eSettings.TVLockShowOriginalTitle) AndAlso scrapeOptions.OriginalTitle AndAlso
                scrapedshow.OriginalTitleSpecified AndAlso Master.eSettings.TVScraperShowOriginalTitle AndAlso Not new_OriginalTitle Then
                dbElement.MainDetails.OriginalTitle = scrapedshow.OriginalTitle
                new_OriginalTitle = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowOriginalTitle AndAlso Not Master.eSettings.TVLockShowOriginalTitle Then
                dbElement.MainDetails.OriginalTitle = String.Empty
            End If

            'Plot
            If (Not dbElement.MainDetails.PlotSpecified OrElse Not Master.eSettings.TVLockShowPlot) AndAlso scrapeOptions.Plot AndAlso
                 scrapedshow.PlotSpecified AndAlso Master.eSettings.TVScraperShowPlot AndAlso Not new_Plot Then
                dbElement.MainDetails.Plot = scrapedshow.Plot
                new_Plot = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowPlot AndAlso Not Master.eSettings.TVLockShowPlot Then
                dbElement.MainDetails.Plot = String.Empty
            End If

            'Premiered
            If (Not dbElement.MainDetails.PremieredSpecified OrElse Not Master.eSettings.TVLockShowPremiered) AndAlso scrapeOptions.Premiered AndAlso
                scrapedshow.PremieredSpecified AndAlso Master.eSettings.TVScraperShowPremiered AndAlso Not new_Premiered Then
                dbElement.MainDetails.Premiered = NumUtils.DateToISO8601Date(scrapedshow.Premiered)
                new_Premiered = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowPremiered AndAlso Not Master.eSettings.TVLockShowPremiered Then
                dbElement.MainDetails.Premiered = String.Empty
            End If

            'Ratings
            If scrapeOptions.Ratings AndAlso scrapedshow.RatingsSpecified AndAlso Master.eSettings.TVScraperShowRating Then
                'remove old entries that cannot be assigned to a source
                dbElement.MainDetails.Ratings.Items.RemoveAll(Function(f) f.Type = "default")
                'add new ratings
                dbElement.MainDetails.Ratings.AddRange(scrapedshow.Ratings)
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowRating AndAlso Not Master.eSettings.TVLockShowRating Then
                dbElement.MainDetails.Ratings.Items.Clear()
            End If

            'Runtime
            If (Not dbElement.MainDetails.RuntimeSpecified OrElse Not Master.eSettings.TVLockShowRuntime) AndAlso scrapeOptions.Runtime AndAlso
                scrapedshow.RuntimeSpecified AndAlso Master.eSettings.TVScraperShowRuntime AndAlso Not new_Runtime Then
                dbElement.MainDetails.Runtime = scrapedshow.Runtime
                new_Runtime = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowRuntime AndAlso Not Master.eSettings.TVLockShowRuntime Then
                dbElement.MainDetails.Runtime = String.Empty
            End If


            'Status
            If (dbElement.MainDetails.StatusSpecified OrElse Not Master.eSettings.TVLockShowStatus) AndAlso scrapeOptions.Status AndAlso
                scrapedshow.StatusSpecified AndAlso Master.eSettings.TVScraperShowStatus AndAlso Not new_Status Then
                dbElement.MainDetails.Status = scrapedshow.Status
                APIXML.StatusMappings.RunMapping(dbElement.MainDetails.Status)
                new_Status = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowStatus AndAlso Not Master.eSettings.TVLockShowStatus Then
                dbElement.MainDetails.Status = String.Empty
            End If

            'Studios
            If (Not dbElement.MainDetails.StudiosSpecified OrElse Not Master.eSettings.TVLockShowStudio) AndAlso scrapeOptions.Studios AndAlso
                scrapedshow.StudiosSpecified AndAlso Master.eSettings.TVScraperShowStudio AndAlso Not new_Studio Then

                Dim _studios As New List(Of String)
                _studios.AddRange(scrapedshow.Studios)
                APIXML.StudioMappings.RunMapping(_studios)

                Filter_CountLimit(Master.eSettings.TVScraperShowStudioLimit, _studios)
                dbElement.MainDetails.Studios = _studios
                new_Studio = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowStudio AndAlso Not Master.eSettings.TVLockShowStudio Then
                dbElement.MainDetails.Studios.Clear()
            End If

            'Tagline
            If (Not dbElement.MainDetails.TaglineSpecified OrElse Not Master.eSettings.TVLockShowTagline) AndAlso scrapeOptions.Tagline AndAlso
                scrapedshow.TaglineSpecified AndAlso Master.eSettings.TVScraperShowTagline AndAlso Not new_Tagline Then
                dbElement.MainDetails.Tagline = scrapedshow.Tagline
                new_Tagline = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowTagline AndAlso Not Master.eSettings.TVLockShowTagline Then
                dbElement.MainDetails.Tagline = String.Empty
            End If

            'Title
            If (Not dbElement.MainDetails.TitleSpecified OrElse Not Master.eSettings.TVLockShowTitle) AndAlso scrapeOptions.Title AndAlso
                scrapedshow.TitleSpecified AndAlso Master.eSettings.TVScraperShowTitle AndAlso Not new_Title Then
                dbElement.MainDetails.Title = scrapedshow.Title
                new_Title = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowTitle AndAlso Not Master.eSettings.TVLockShowTitle Then
                dbElement.MainDetails.Title = String.Empty
            End If

            'User Rating
            If (Not dbElement.MainDetails.UserRatingSpecified OrElse Not Master.eSettings.TVLockShowUserRating) AndAlso scrapeOptions.UserRating AndAlso
                scrapedshow.UserRatingSpecified AndAlso Master.eSettings.TVScraperShowUserRating AndAlso Not new_UserRating Then
                dbElement.MainDetails.UserRating = scrapedshow.UserRating
                new_UserRating = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowUserRating AndAlso Not Master.eSettings.TVLockShowUserRating Then
                dbElement.MainDetails.UserRating = 0
            End If

            'Create KnowSeasons index
            For Each kSeason In scrapedshow.KnownSeasons
                If Not KnownSeasonsIndex.Contains(kSeason.Season) Then
                    KnownSeasonsIndex.Add(kSeason.Season)
                End If
            Next

            'Create KnownEpisodes index (season and episode number)
            If withEpisodes Then
                For Each kEpisode In scrapedshow.KnownEpisodes
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
            End If
        Next

        'Certification for MPAA
        If dbElement.MainDetails.CertificationsSpecified AndAlso Master.eSettings.TVScraperShowCertForMPAA AndAlso
            (Not Master.eSettings.Movie.InformationSettings.CertificationsForMPAAFallback AndAlso (Not dbElement.MainDetails.MPAASpecified OrElse Not Master.eSettings.TVLockShowMPAA) OrElse
             Not new_MPAA AndAlso (Not dbElement.MainDetails.MPAASpecified OrElse Not Master.eSettings.TVLockShowMPAA)) Then

            Dim tmpstring As String = String.Empty
            tmpstring = If(Master.eSettings.TVScraperShowCertCountry = "us", StringUtils.USACertToMPAA(String.Join(" / ", dbElement.MainDetails.Certifications.ToArray)), If(Master.eSettings.TVScraperShowCertOnlyValue, String.Join(" / ", dbElement.MainDetails.Certifications.ToArray).Split(Convert.ToChar(":"))(1), String.Join(" / ", dbElement.MainDetails.Certifications.ToArray)))
            'only update DBMovie if scraped result is not empty/nothing!
            If Not String.IsNullOrEmpty(tmpstring) Then
                dbElement.MainDetails.MPAA = tmpstring
            End If
        End If

        'MPAA value if MPAA is not available
        If Not dbElement.MainDetails.MPAASpecified AndAlso Not String.IsNullOrEmpty(Master.eSettings.TVScraperShowMPAANotRated) Then
            dbElement.MainDetails.MPAA = Master.eSettings.TVScraperShowMPAANotRated
        End If

        'OriginalTitle as Title
        If (Not dbElement.MainDetails.TitleSpecified OrElse Not Master.eSettings.TVLockShowTitle) AndAlso Master.eSettings.TVScraperShowOriginalTitleAsTitle AndAlso dbElement.MainDetails.OriginalTitleSpecified Then
            dbElement.MainDetails.Title = dbElement.MainDetails.OriginalTitle
        End If

        'sort Ratings by Default and Type (name)
        dbElement.MainDetails.Ratings.Items.Sort()

        'sort UniqueIds by Default and Type (name)
        dbElement.MainDetails.UniqueIDs.Items.Sort()

        'fallback for title if title is empty
        If Not dbElement.MainDetails.TitleSpecified Then
            dbElement.MainDetails.Title = StringUtils.FilterTitleFromPath_TVShow(dbElement.ShowPath)
        End If


        'Seasons
        For Each aKnownSeason As Integer In KnownSeasonsIndex
            'create a list of specified episode informations from all scrapers
            Dim ScrapedSeasonList As New List(Of MediaContainers.MainDetails)
            For Each nShow In scrapedList
                For Each nSeasonDetails In nShow.KnownSeasons.Where(Function(f) f.Season = aKnownSeason)
                    ScrapedSeasonList.Add(nSeasonDetails)
                Next
            Next
            'check if we have already saved season information for this scraped season
            Dim lSeasonList = dbElement.Seasons.Where(Function(f) f.MainDetails.Season = aKnownSeason)

            If lSeasonList IsNot Nothing AndAlso lSeasonList.Count > 0 Then
                For Each nSeason As Database.DBElement In lSeasonList
                    MergeDataScraperResults_TVSeason(nSeason, ScrapedSeasonList)
                Next
            Else
                'no existing season found -> add it as "missing" season
                Dim mSeason As New Database.DBElement(Enums.ContentType.TVSeason) With {.MainDetails = New MediaContainers.MainDetails With {.Season = aKnownSeason}}
                mSeason = Master.DB.Load_TVShowInfoIntoDBElement(mSeason, dbElement)
                dbElement.Seasons.Add(MergeDataScraperResults_TVSeason(mSeason, ScrapedSeasonList))
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
                lEpisode = Master.DB.Load_TVShowInfoIntoDBElement(lEpisode, dbElement)
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
                    For Each nShow In scrapedList
                        For Each nEpisodeDetails In nShow.KnownEpisodes.Where(Function(f) f.Episode = aKnownEpisode.Episode AndAlso f.Season = aKnownEpisode.Season)
                            ScrapedEpisodeList.Add(nEpisodeDetails)
                        Next
                    Next

                    'check if we have a local episode file for this scraped episode
                    Dim lEpisodeList = dbElement.Episodes.Where(Function(f) f.FilenameSpecified AndAlso f.MainDetails.Episode = iEpisode AndAlso f.MainDetails.Season = iSeason)

                    If lEpisodeList IsNot Nothing AndAlso lEpisodeList.Count > 0 Then
                        For Each nEpisode As Database.DBElement In lEpisodeList
                            MergeDataScraperResults_TVEpisode(nEpisode, ScrapedEpisodeList, scrapeOptions)
                        Next
                    Else
                        'try to get the episode by AiredDate
                        Dim dEpisodeList = dbElement.Episodes.Where(Function(f) f.FilenameSpecified AndAlso
                                                                   f.MainDetails.Episode = -1 AndAlso
                                                                   f.MainDetails.AiredSpecified AndAlso
                                                                   f.MainDetails.Aired = strAiredDate)

                        If dEpisodeList IsNot Nothing AndAlso dEpisodeList.Count > 0 Then
                            For Each nEpisode As Database.DBElement In dEpisodeList
                                MergeDataScraperResults_TVEpisode(nEpisode, ScrapedEpisodeList, scrapeOptions)
                                'we have to add the proper season and episode number if the episode was found by AiredDate
                                nEpisode.MainDetails.Episode = iEpisode
                                nEpisode.MainDetails.Season = iSeason
                            Next
                        Else
                            'no local episode found -> add it as "missing" episode
                            Dim mEpisode As New Database.DBElement(Enums.ContentType.TVEpisode) With {.MainDetails = New MediaContainers.MainDetails With {.Episode = iEpisode, .Season = iSeason}}
                            mEpisode = Master.DB.Load_TVShowInfoIntoDBElement(mEpisode, dbElement)
                            MergeDataScraperResults_TVEpisode(mEpisode, ScrapedEpisodeList, scrapeOptions)
                            If mEpisode.MainDetails.TitleSpecified Then
                                dbElement.Episodes.Add(mEpisode)
                            Else
                                _Logger.Warn(String.Format("Missing Episode Ignored | {0} - S{1}E{2} | No Episode Title found", mEpisode.TVShowDetails.Title, mEpisode.MainDetails.Season, mEpisode.MainDetails.Episode))
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
            tmpAllSeasons = Master.DB.Load_TVShowInfoIntoDBElement(tmpAllSeasons, dbElement)
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

        Return dbElement
    End Function

    Public Shared Function MergeDataScraperResults_TVSeason(ByRef dbElement As Database.DBElement, ByVal scrapedList As List(Of MediaContainers.MainDetails)) As Database.DBElement
        Dim scrapeOptions = dbElement.ScrapeOptions

        'protects the first scraped result against overwriting
        Dim new_Aired As Boolean = False
        Dim new_Plot As Boolean = False
        Dim new_Season As Boolean = False
        Dim new_Title As Boolean = False

        For Each scrapedseason In scrapedList

            'UniqueIDs
            If scrapedseason.UniqueIDsSpecified Then
                dbElement.MainDetails.UniqueIDs.AddRange(scrapedseason.UniqueIDs)
            End If

            'Season number
            If scrapedseason.SeasonSpecified AndAlso Not new_Season Then
                dbElement.MainDetails.Season = scrapedseason.Season
                new_Season = True
            End If

            'Aired
            If (Not dbElement.MainDetails.AiredSpecified OrElse Not Master.eSettings.TVLockEpisodeAired) AndAlso scrapeOptions.Seasons.Aired AndAlso
                scrapedseason.AiredSpecified AndAlso Master.eSettings.TVScraperEpisodeAired AndAlso Not new_Aired Then
                dbElement.MainDetails.Aired = scrapedseason.Aired
                new_Aired = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeAired AndAlso Not Master.eSettings.TVLockEpisodeAired Then
                dbElement.MainDetails.Aired = String.Empty
            End If

            'Plot
            If (Not dbElement.MainDetails.PlotSpecified OrElse Not Master.eSettings.TVLockEpisodePlot) AndAlso scrapeOptions.Seasons.Plot AndAlso
                scrapedseason.PlotSpecified AndAlso Master.eSettings.TVScraperEpisodePlot AndAlso Not new_Plot Then
                dbElement.MainDetails.Plot = scrapedseason.Plot
                new_Plot = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodePlot AndAlso Not Master.eSettings.TVLockEpisodePlot Then
                dbElement.MainDetails.Plot = String.Empty
            End If

            'Title
            If (Not dbElement.MainDetails.TitleSpecified OrElse Not Master.eSettings.TVLockSeasonTitle) AndAlso scrapeOptions.Seasons.Title AndAlso
                scrapedseason.TitleSpecified AndAlso Not String.IsNullOrEmpty(StringUtils.FilterSeasonTitle(scrapedseason.Title)) AndAlso Master.eSettings.TVScraperSeasonTitle AndAlso Not new_Title Then
                dbElement.MainDetails.Title = scrapedseason.Title
                new_Title = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperSeasonTitle AndAlso Not Master.eSettings.TVLockSeasonTitle Then
                dbElement.MainDetails.Title = String.Empty
            End If
        Next

        'sort UniqueIds by Default and Type (name)
        dbElement.MainDetails.UniqueIDs.Items.Sort()

        Return dbElement
    End Function
    ''' <summary>
    ''' Returns the "merged" result of each data scraper results
    ''' </summary>
    ''' <param name="dbElement">Episode to be scraped</param>
    ''' <param name="scrapedList"><c>List(Of MediaContainers.EpisodeDetails)</c> which contains unfiltered results of each data scraper</param>
    ''' <returns>The scrape result of episode (after applying various global scraper settings here)</returns>
    ''' <remarks>
    ''' This is used to determine the result of data scraping by going through all scraperesults of every data scraper and applying global data scraper settings here!
    ''' 
    ''' 2014/09/01 Cocotus - First implementation: Moved all global lock settings in various data scrapers to this function, only apply them once and not in every data scraper module! Should be more maintainable!
    ''' </remarks>
    Private Shared Function MergeDataScraperResults_TVEpisode(ByRef dbElement As Database.DBElement, ByVal scrapedList As List(Of MediaContainers.MainDetails), ByVal scrapeOptions As Structures.ScrapeOptions) As Database.DBElement

        'protects the first scraped result against overwriting
        Dim new_Actors As Boolean = False
        Dim new_Aired As Boolean = False
        Dim new_Countries As Boolean = False
        Dim new_Credits As Boolean = False
        Dim new_Directors As Boolean = False
        Dim new_Episode As Boolean = False
        Dim new_GuestStars As Boolean = False
        Dim new_OriginalTitle As Boolean = False
        Dim new_Plot As Boolean = False
        Dim new_Rating As Boolean = False
        Dim new_Runtime As Boolean = False
        Dim new_Season As Boolean = False
        Dim new_ThumbPoster As Boolean = False
        Dim new_Title As Boolean = False
        Dim new_UserRating As Boolean = False

        ''If "Use Preview Datascraperresults" option is enabled, a preview window which displays all datascraperresults will be opened before showing the Edit Movie page!
        'If (ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape OrElse ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleField) AndAlso Master.eSettings.MovieScraperUseDetailView AndAlso ScrapedList.Count > 0 Then
        '    PreviewDataScraperResults(ScrapedList)
        'End If

        For Each scrapedepisode In scrapedList

            'UniqueIDs
            If scrapedepisode.UniqueIDsSpecified Then
                dbElement.MainDetails.UniqueIDs.AddRange(scrapedepisode.UniqueIDs)
            End If

            'DisplayEpisode
            If scrapedepisode.DisplayEpisodeSpecified Then
                dbElement.MainDetails.DisplayEpisode = scrapedepisode.DisplayEpisode
            End If

            'DisplaySeason
            If scrapedepisode.DisplaySeasonSpecified Then
                dbElement.MainDetails.DisplaySeason = scrapedepisode.DisplaySeason
            End If

            'Actors
            If (Not dbElement.MainDetails.ActorsSpecified OrElse Not Master.eSettings.TVLockEpisodeActors) AndAlso scrapeOptions.Episodes.Actors AndAlso
                scrapedepisode.ActorsSpecified AndAlso Master.eSettings.TVScraperEpisodeActors AndAlso Not new_Actors Then

                If Master.eSettings.TVScraperCastWithImgOnly Then
                    Filter_OnlyPersonsWithImage(scrapedepisode.Actors)
                End If
                Filter_CountLimit(Master.eSettings.TVScraperEpisodeActorsLimit, scrapedepisode.Actors)
                'added check if there's any actors left to add, if not then try with results of following scraper...
                If scrapedepisode.ActorsSpecified Then
                    ReorderPersons(scrapedepisode.Actors)
                    dbElement.MainDetails.Actors = scrapedepisode.Actors
                    new_Actors = True
                End If

            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeActors AndAlso Not Master.eSettings.TVLockEpisodeActors Then
                dbElement.MainDetails.Actors.Clear()
            End If

            'Aired
            If (Not dbElement.MainDetails.AiredSpecified OrElse Not Master.eSettings.TVLockEpisodeAired) AndAlso scrapeOptions.Episodes.Aired AndAlso
                scrapedepisode.AiredSpecified AndAlso Master.eSettings.TVScraperEpisodeAired AndAlso Not new_Aired Then
                dbElement.MainDetails.Aired = NumUtils.DateToISO8601Date(scrapedepisode.Aired)
                new_Aired = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeAired AndAlso Not Master.eSettings.TVLockEpisodeAired Then
                dbElement.MainDetails.Aired = String.Empty
            End If

            'Credits
            If (Not dbElement.MainDetails.CreditsSpecified OrElse Not Master.eSettings.TVLockEpisodeCredits) AndAlso
                scrapedepisode.CreditsSpecified AndAlso Master.eSettings.TVScraperEpisodeCredits AndAlso Not new_Credits Then
                dbElement.MainDetails.Credits = scrapedepisode.Credits
                new_Credits = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeCredits AndAlso Not Master.eSettings.TVLockEpisodeCredits Then
                dbElement.MainDetails.Credits.Clear()
            End If

            'Directors
            If (Not dbElement.MainDetails.DirectorsSpecified OrElse Not Master.eSettings.TVLockEpisodeDirector) AndAlso scrapeOptions.Episodes.Directors AndAlso
                scrapedepisode.DirectorsSpecified AndAlso Master.eSettings.TVScraperEpisodeDirector AndAlso Not new_Directors Then
                dbElement.MainDetails.Directors = scrapedepisode.Directors
                new_Directors = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeDirector AndAlso Not Master.eSettings.TVLockEpisodeDirector Then
                dbElement.MainDetails.Directors.Clear()
            End If

            'GuestStars
            If (Not dbElement.MainDetails.GuestStarsSpecified OrElse Not Master.eSettings.TVLockEpisodeGuestStars) AndAlso scrapeOptions.Episodes.GuestStars AndAlso
                scrapedepisode.GuestStarsSpecified AndAlso Master.eSettings.TVScraperEpisodeGuestStars AndAlso Not new_GuestStars Then

                If Master.eSettings.TVScraperCastWithImgOnly Then
                    Filter_OnlyPersonsWithImage(scrapedepisode.GuestStars)
                End If
                Filter_CountLimit(Master.eSettings.TVScraperEpisodeGuestStarsLimit, scrapedepisode.GuestStars)
                'added check if there's any actors left to add, if not then try with results of following scraper...
                If scrapedepisode.GuestStarsSpecified Then
                    ReorderPersons(scrapedepisode.GuestStars)
                    dbElement.MainDetails.GuestStars = scrapedepisode.GuestStars
                    new_GuestStars = True
                End If
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeGuestStars AndAlso Not Master.eSettings.TVLockEpisodeGuestStars Then
                dbElement.MainDetails.GuestStars.Clear()
            End If

            'Plot
            If (Not dbElement.MainDetails.PlotSpecified OrElse Not Master.eSettings.TVLockEpisodePlot) AndAlso scrapeOptions.Episodes.Plot AndAlso
                scrapedepisode.PlotSpecified AndAlso Master.eSettings.TVScraperEpisodePlot AndAlso Not new_Plot Then
                dbElement.MainDetails.Plot = scrapedepisode.Plot
                new_Plot = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodePlot AndAlso Not Master.eSettings.TVLockEpisodePlot Then
                dbElement.MainDetails.Plot = String.Empty
            End If

            'Ratings
            If scrapeOptions.Ratings AndAlso scrapedepisode.RatingsSpecified AndAlso Master.eSettings.TVScraperEpisodeRating Then
                'remove old entries that cannot be assigned to a source
                dbElement.MainDetails.Ratings.Items.RemoveAll(Function(f) f.Type = "default")
                'add new ratings
                dbElement.MainDetails.Ratings.AddRange(scrapedepisode.Ratings)
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeRating AndAlso Not Master.eSettings.TVLockEpisodeRating Then
                dbElement.MainDetails.Ratings.Items.Clear()
            End If

            'User Rating
            If (Not dbElement.MainDetails.UserRatingSpecified OrElse Not Master.eSettings.TVLockEpisodeUserRating) AndAlso scrapeOptions.Episodes.UserRating AndAlso
                scrapedepisode.UserRatingSpecified AndAlso Master.eSettings.TVScraperEpisodeUserRating AndAlso Not new_UserRating Then
                dbElement.MainDetails.UserRating = scrapedepisode.UserRating
                new_UserRating = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeUserRating AndAlso Not Master.eSettings.TVLockEpisodeUserRating Then
                dbElement.MainDetails.UserRating = 0
            End If

            'Runtime
            If (Not dbElement.MainDetails.RuntimeSpecified OrElse Not Master.eSettings.TVLockEpisodeRuntime) AndAlso scrapeOptions.Episodes.Runtime AndAlso
                scrapedepisode.RuntimeSpecified AndAlso Master.eSettings.TVScraperEpisodeRuntime AndAlso Not new_Runtime Then
                dbElement.MainDetails.Runtime = scrapedepisode.Runtime
                new_Runtime = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeRuntime AndAlso Not Master.eSettings.TVLockEpisodeRuntime Then
                dbElement.MainDetails.Runtime = String.Empty
            End If

            'ThumbPoster
            If (Not String.IsNullOrEmpty(scrapedepisode.ThumbPoster.URLOriginal) OrElse Not String.IsNullOrEmpty(scrapedepisode.ThumbPoster.URLThumb)) AndAlso Not new_ThumbPoster Then
                dbElement.MainDetails.ThumbPoster = scrapedepisode.ThumbPoster
                new_ThumbPoster = True
            End If

            'Title
            If (Not dbElement.MainDetails.TitleSpecified OrElse Not Master.eSettings.TVLockEpisodeTitle) AndAlso scrapeOptions.Episodes.Title AndAlso
               scrapedepisode.TitleSpecified AndAlso Master.eSettings.TVScraperEpisodeTitle AndAlso Not new_Title Then
                dbElement.MainDetails.Title = scrapedepisode.Title
                new_Title = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeTitle AndAlso Not Master.eSettings.TVLockEpisodeTitle Then
                dbElement.MainDetails.Title = String.Empty
            End If
        Next

        'Add GuestStars to Actors
        If dbElement.MainDetails.GuestStarsSpecified AndAlso Master.eSettings.TVScraperEpisodeGuestStarsToActors AndAlso Not Master.eSettings.TVLockEpisodeActors Then
            dbElement.MainDetails.Actors.AddRange(dbElement.MainDetails.GuestStars)

            'run the limit filter again
            Filter_CountLimit(Master.eSettings.TVScraperEpisodeActorsLimit, dbElement.MainDetails.Actors)

            'reorder again
            ReorderPersons(dbElement.MainDetails.Actors)
        End If

        'sort Ratings by Default and Type (name)
        dbElement.MainDetails.Ratings.Items.Sort()

        'sort UniqueIds by Default and Type (name)
        dbElement.MainDetails.UniqueIDs.Items.Sort()

        'TV Show Runtime for Episode Runtime
        If Not dbElement.MainDetails.RuntimeSpecified AndAlso Master.eSettings.TVScraperUseSRuntimeForEp AndAlso dbElement.TVShowDetails.RuntimeSpecified Then
            dbElement.MainDetails.Runtime = dbElement.TVShowDetails.Runtime
        End If

        Return dbElement
    End Function

    Public Shared Function MergeDataScraperResults_TVEpisode_Single(ByRef dbElement As Database.DBElement, ByVal scrapedList As List(Of MediaContainers.MainDetails)) As Database.DBElement
        Dim scrapeOptions = dbElement.ScrapeOptions
        Dim KnownEpisodesIndex As New List(Of KnownEpisode)

        For Each kEpisode In scrapedList
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
            If dbElement.EpisodeOrdering = Enums.EpisodeOrdering.Absolute Then
                iEpisode = KnownEpisodesIndex.Item(0).EpisodeAbsolute
                iSeason = 1
            ElseIf dbElement.EpisodeOrdering = Enums.EpisodeOrdering.DVD Then
                iEpisode = CInt(KnownEpisodesIndex.Item(0).EpisodeDVD)
                iSeason = KnownEpisodesIndex.Item(0).SeasonDVD
            ElseIf dbElement.EpisodeOrdering = Enums.EpisodeOrdering.Standard Then
                iEpisode = KnownEpisodesIndex.Item(0).Episode
                iSeason = KnownEpisodesIndex.Item(0).Season
            End If

            If Not iEpisode = -1 AndAlso Not iSeason = -1 Then
                MergeDataScraperResults_TVEpisode(dbElement, scrapedList, scrapeOptions)
                If dbElement.MainDetails.Episode = -1 Then dbElement.MainDetails.Episode = iEpisode
                If dbElement.MainDetails.Season = -1 Then dbElement.MainDetails.Season = iSeason
            Else
                _Logger.Warn("No valid episode or season number found")
            End If
        Else
            _Logger.Warn("Episode could not be clearly determined.")
        End If

        Return dbElement
    End Function

    Private Shared Function OverwriteValue(ByVal scrapeOptionEnabled As Boolean,
                                           ByVal alreadyNewSet As Boolean,
                                           ByVal oldSpecified As Boolean,
                                           ByVal newSpecified As Boolean,
                                           ByVal settings As InformationItemBase) As Boolean
        Return scrapeOptionEnabled AndAlso
            Not alreadyNewSet AndAlso
            newSpecified AndAlso
            settings.Enabled AndAlso
            (Not oldSpecified OrElse Not settings.Locked)
    End Function

    Private Shared Sub ReorderPersons(ByRef person As List(Of MediaContainers.Person))
        Dim Order As Integer = 0
        For Each nPerson In person
            nPerson.Order = Order
            Order += 1
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

    End Class 'Nested Types

#End Region

End Class