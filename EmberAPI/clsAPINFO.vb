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
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml.Serialization
Imports System.Windows.Forms

Public Class Info

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

#End Region

#Region "Methods"
    ''' <summary>
    ''' Returns the "merged" result of each data scraper results
    ''' </summary>
    ''' <param name="DBMovie">Movie to be scraped</param>
    ''' <param name="ScrapedList"><c>List(Of MediaContainers.Movie)</c> which contains unfiltered results of each data scraper</param>
    ''' <returns>The scrape result of movie (after applying various global scraper settings here)</returns>
    ''' <remarks>
    ''' This is used to determine the result of data scraping by going through all scraperesults of every data scraper and applying global data scraper settings here!
    ''' 
    ''' 2014/09/01 Cocotus - First implementation: Moved all global lock settings in various data scrapers to this function, only apply them once and not in every data scraper module! Should be more maintainable!
    ''' </remarks>
    Public Shared Function MergeDataScraperResults_Movie(ByVal DBMovie As Database.DBElement, ByVal ScrapedList As List(Of MediaContainers.Movie), ByVal ScrapeType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions) As Database.DBElement

        'protects the first scraped result against overwriting
        Dim new_Actors As Boolean = False
        Dim new_Certification As Boolean = False
        Dim new_CollectionID As Boolean = False
        Dim new_Collections As Boolean = False
        Dim new_Countries As Boolean = False
        Dim new_Credits As Boolean = False
        Dim new_Directors As Boolean = False
        Dim new_Genres As Boolean = False
        Dim new_MPAA As Boolean = False
        Dim new_OriginalTitle As Boolean = False
        Dim new_Outline As Boolean = False
        Dim new_Plot As Boolean = False
        Dim new_ReleaseDate As Boolean = False
        Dim new_Runtime As Boolean = False
        Dim new_Studio As Boolean = False
        Dim new_Tagline As Boolean = False
        Dim new_Title As Boolean = False
        Dim new_Top250 As Boolean = False
        Dim new_Trailer As Boolean = False
        Dim new_UserRating As Boolean = False
        Dim new_Year As Boolean = False

        'If "Use Preview Datascraperresults" option is enabled, a preview window which displays all datascraperresults will be opened before showing the Edit Movie page!
        If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleField) AndAlso Master.eSettings.MovieScraperUseDetailView AndAlso ScrapedList.Count > 0 Then
            PreviewDataScraperResults_Movie(ScrapedList)
        End If

        For Each scrapedmovie In ScrapedList

            'UniqueIDs
            If scrapedmovie.UniqueIDsSpecified Then
                DBMovie.Movie.UniqueIDs.AddRange(scrapedmovie.UniqueIDs)
            End If

            'Actors
            If (Not DBMovie.Movie.ActorsSpecified OrElse Not Master.eSettings.MovieLockActors) AndAlso ScrapeOptions.bMainActors AndAlso
                scrapedmovie.ActorsSpecified AndAlso Master.eSettings.MovieScraperCast AndAlso Not new_Actors Then
                If Master.eSettings.MovieScraperCastWithImgOnly Then
                    FilterOnlyPersonsWithImage(scrapedmovie.Actors)
                End If
                FilterCountLimit(Master.eSettings.MovieScraperCastLimit, scrapedmovie.Actors)
                'added check if there's any actors left to add, if not then try with results of following scraper...
                If scrapedmovie.ActorsSpecified Then
                    ReorderPersons(scrapedmovie.Actors)
                    DBMovie.Movie.Actors = scrapedmovie.Actors
                    new_Actors = True
                End If

            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCast AndAlso Not Master.eSettings.MovieLockActors Then
                DBMovie.Movie.Actors.Clear()
            End If

            'Certification
            If (Not DBMovie.Movie.CertificationsSpecified OrElse Not Master.eSettings.MovieLockCert) AndAlso ScrapeOptions.bMainCertifications AndAlso
                scrapedmovie.CertificationsSpecified AndAlso Master.eSettings.MovieScraperCert AndAlso Not new_Certification Then
                If Master.eSettings.MovieScraperCertLang = Master.eLang.All Then
                    DBMovie.Movie.Certifications = scrapedmovie.Certifications
                    new_Certification = True
                Else
                    Dim CertificationLanguage = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.MovieScraperCertLang)
                    If CertificationLanguage IsNot Nothing AndAlso CertificationLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CertificationLanguage.name) Then
                        For Each tCert In scrapedmovie.Certifications
                            If tCert.StartsWith(CertificationLanguage.name) Then
                                DBMovie.Movie.Certifications.Clear()
                                DBMovie.Movie.Certifications.Add(tCert)
                                new_Certification = True
                                Exit For
                            End If
                        Next
                    Else
                        logger.Error("Movie Certification Language (Limit) not found. Please check your settings!")
                    End If
                End If
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCert AndAlso Not Master.eSettings.MovieLockCert Then
                DBMovie.Movie.Certifications.Clear()
            End If

            'Credits
            If (Not DBMovie.Movie.CreditsSpecified OrElse Not Master.eSettings.MovieLockCredits) AndAlso
                scrapedmovie.CreditsSpecified AndAlso Master.eSettings.MovieScraperCredits AndAlso Not new_Credits Then
                DBMovie.Movie.Credits = scrapedmovie.Credits
                new_Credits = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCredits AndAlso Not Master.eSettings.MovieLockCredits Then
                DBMovie.Movie.Credits.Clear()
            End If

            'Collection ID
            If (Not DBMovie.Movie.UniqueIDs.TMDbCollectionIdSpecified OrElse Not Master.eSettings.MovieLockCollectionID) AndAlso ScrapeOptions.bMainCollectionID AndAlso
                scrapedmovie.UniqueIDs.TMDbCollectionIdSpecified AndAlso Master.eSettings.MovieScraperCollectionID AndAlso Not new_CollectionID Then
                DBMovie.Movie.UniqueIDs.TMDbCollectionId = scrapedmovie.UniqueIDs.TMDbCollectionId
                new_CollectionID = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCollectionID AndAlso Not Master.eSettings.MovieLockCollectionID Then
                DBMovie.Movie.UniqueIDs.TMDbCollectionId = String.Empty
            End If

            'Collections
            If (Not DBMovie.Movie.SetsSpecified OrElse Not Master.eSettings.MovieLockCollections) AndAlso
                scrapedmovie.SetsSpecified AndAlso Master.eSettings.MovieScraperCollectionsAuto AndAlso Not new_Collections Then
                DBMovie.Movie.Sets.Clear()
                For Each movieset In scrapedmovie.Sets
                    If Not String.IsNullOrEmpty(movieset.Title) Then
                        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
                            movieset.Title = movieset.Title.Replace(sett.Name.Substring(21), sett.Value)
                        Next
                    End If
                Next
                DBMovie.Movie.Sets.AddRange(scrapedmovie.Sets)
                new_Collections = True
            End If

            'Countries
            If (Not DBMovie.Movie.CountriesSpecified OrElse Not Master.eSettings.MovieLockCountry) AndAlso ScrapeOptions.bMainCountries AndAlso
                scrapedmovie.CountriesSpecified AndAlso Master.eSettings.MovieScraperCountry AndAlso Not new_Countries Then
                FilterCountLimit(Master.eSettings.MovieScraperCountryLimit, scrapedmovie.Countries)
                DBMovie.Movie.Countries = scrapedmovie.Countries
                new_Countries = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCountry AndAlso Not Master.eSettings.MovieLockCountry Then
                DBMovie.Movie.Countries.Clear()
            End If

            'Directors
            If (Not DBMovie.Movie.DirectorsSpecified OrElse Not Master.eSettings.MovieLockDirector) AndAlso ScrapeOptions.bMainDirectors AndAlso
                scrapedmovie.DirectorsSpecified AndAlso Master.eSettings.MovieScraperDirector AndAlso Not new_Directors Then
                DBMovie.Movie.Directors = scrapedmovie.Directors
                new_Directors = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperDirector AndAlso Not Master.eSettings.MovieLockDirector Then
                DBMovie.Movie.Directors.Clear()
            End If

            'Genres
            If (Not DBMovie.Movie.GenresSpecified OrElse Not Master.eSettings.MovieLockGenre) AndAlso ScrapeOptions.bMainGenres AndAlso
                scrapedmovie.GenresSpecified AndAlso Master.eSettings.MovieScraperGenre AndAlso Not new_Genres Then
                StringUtils.GenreFilter(scrapedmovie.Genres)
                FilterCountLimit(Master.eSettings.MovieScraperGenreLimit, scrapedmovie.Genres)
                DBMovie.Movie.Genres = scrapedmovie.Genres
                new_Genres = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperGenre AndAlso Not Master.eSettings.MovieLockGenre Then
                DBMovie.Movie.Genres.Clear()
            End If

            'MPAA
            If (Not DBMovie.Movie.MPAASpecified OrElse Not Master.eSettings.MovieLockMPAA) AndAlso ScrapeOptions.bMainMPAA AndAlso
                scrapedmovie.MPAASpecified AndAlso Master.eSettings.MovieScraperMPAA AndAlso Not new_MPAA Then
                DBMovie.Movie.MPAA = scrapedmovie.MPAA
                new_MPAA = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperMPAA AndAlso Not Master.eSettings.MovieLockMPAA Then
                DBMovie.Movie.MPAA = String.Empty
            End If

            'Originaltitle
            If (Not DBMovie.Movie.OriginalTitleSpecified OrElse Not Master.eSettings.MovieLockOriginalTitle) AndAlso ScrapeOptions.bMainOriginalTitle AndAlso
                scrapedmovie.OriginalTitleSpecified AndAlso Master.eSettings.MovieScraperOriginalTitle AndAlso Not new_OriginalTitle Then
                DBMovie.Movie.OriginalTitle = scrapedmovie.OriginalTitle
                new_OriginalTitle = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperOriginalTitle AndAlso Not Master.eSettings.MovieLockOriginalTitle Then
                DBMovie.Movie.OriginalTitle = String.Empty
            End If

            'Outline
            If (Not DBMovie.Movie.OutlineSpecified OrElse Not Master.eSettings.MovieLockOutline) AndAlso ScrapeOptions.bMainOutline AndAlso
                scrapedmovie.OutlineSpecified AndAlso Master.eSettings.MovieScraperOutline AndAlso Not new_Outline Then
                DBMovie.Movie.Outline = scrapedmovie.Outline
                new_Outline = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperOutline AndAlso Not Master.eSettings.MovieLockOutline Then
                DBMovie.Movie.Outline = String.Empty
            End If
            'check if brackets should be removed...
            If Master.eSettings.MovieScraperCleanPlotOutline Then
                DBMovie.Movie.Outline = StringUtils.RemoveBrackets(DBMovie.Movie.Outline)
            End If

            'Plot
            If (Not DBMovie.Movie.PlotSpecified OrElse Not Master.eSettings.MovieLockPlot) AndAlso ScrapeOptions.bMainPlot AndAlso
                scrapedmovie.PlotSpecified AndAlso Master.eSettings.MovieScraperPlot AndAlso Not new_Plot Then
                DBMovie.Movie.Plot = scrapedmovie.Plot
                new_Plot = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperPlot AndAlso Not Master.eSettings.MovieLockPlot Then
                DBMovie.Movie.Plot = String.Empty
            End If
            'check if brackets should be removed...
            If Master.eSettings.MovieScraperCleanPlotOutline Then
                DBMovie.Movie.Plot = StringUtils.RemoveBrackets(DBMovie.Movie.Plot)
            End If

            'Ratings
            If (Not DBMovie.Movie.RatingsSpecified OrElse Not Master.eSettings.MovieLockRating) AndAlso ScrapeOptions.bMainRating AndAlso
                 scrapedmovie.RatingsSpecified AndAlso Master.eSettings.MovieScraperRating Then
                For Each nRating In scrapedmovie.Ratings
                    'remove old rating(s) from the same source
                    DBMovie.Movie.Ratings.RemoveAll(Function(f) f.Name = nRating.Name)
                    DBMovie.Movie.Ratings.Add(nRating)
                Next
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRating AndAlso Not Master.eSettings.MovieLockRating Then
                DBMovie.Movie.Ratings.Clear()
                DBMovie.Movie.Rating = String.Empty
                DBMovie.Movie.Votes = String.Empty
            End If

            'ReleaseDate
            If (Not DBMovie.Movie.ReleaseDateSpecified OrElse Not Master.eSettings.MovieLockReleaseDate) AndAlso ScrapeOptions.bMainRelease AndAlso
                scrapedmovie.ReleaseDateSpecified AndAlso Master.eSettings.MovieScraperRelease AndAlso Not new_ReleaseDate Then
                DBMovie.Movie.ReleaseDate = NumUtils.DateToISO8601Date(scrapedmovie.ReleaseDate)
                new_ReleaseDate = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRelease AndAlso Not Master.eSettings.MovieLockReleaseDate Then
                DBMovie.Movie.ReleaseDate = String.Empty
            End If

            'Studios
            If (Not DBMovie.Movie.StudiosSpecified OrElse Not Master.eSettings.MovieLockStudio) AndAlso ScrapeOptions.bMainStudios AndAlso
                scrapedmovie.StudiosSpecified AndAlso Master.eSettings.MovieScraperStudio AndAlso Not new_Studio Then
                DBMovie.Movie.Studios.Clear()

                Dim _studios As New List(Of String)
                _studios.AddRange(scrapedmovie.Studios)

                If Master.eSettings.MovieScraperStudioWithImgOnly Then
                    For i = _studios.Count - 1 To 0 Step -1
                        If APIXML.dStudios.ContainsKey(_studios.Item(i).ToLower) = False Then
                            _studios.RemoveAt(i)
                        End If
                    Next
                End If

                FilterCountLimit(Master.eSettings.MovieScraperStudioLimit, _studios)

                DBMovie.Movie.Studios.AddRange(_studios)
                'added check if there's any studios left to add, if not then try with results of following scraper...
                If _studios.Count > 0 Then
                    new_Studio = True
                End If

            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperStudio AndAlso Not Master.eSettings.MovieLockStudio Then
                DBMovie.Movie.Studios.Clear()
            End If

            'Tagline
            If (Not DBMovie.Movie.TaglineSpecified OrElse Not Master.eSettings.MovieLockTagline) AndAlso ScrapeOptions.bMainTagline AndAlso
                scrapedmovie.TaglineSpecified AndAlso Master.eSettings.MovieScraperTagline AndAlso Not new_Tagline Then
                DBMovie.Movie.Tagline = scrapedmovie.Tagline
                new_Tagline = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTagline AndAlso Not Master.eSettings.MovieLockTagline Then
                DBMovie.Movie.Tagline = String.Empty
            End If

            'Title
            If (Not DBMovie.Movie.TitleSpecified OrElse Not Master.eSettings.MovieLockTitle) AndAlso ScrapeOptions.bMainTitle AndAlso
                scrapedmovie.TitleSpecified AndAlso Master.eSettings.MovieScraperTitle AndAlso Not new_Title Then
                DBMovie.Movie.Title = scrapedmovie.Title
                new_Title = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTitle AndAlso Not Master.eSettings.MovieLockTitle Then
                DBMovie.Movie.Title = String.Empty
            End If

            'Top250 (special handling: no check if "scrapedmovie.Top250Specified" and only set "new_Top250 = True" if a value over 0 has been set)
            If (Not DBMovie.Movie.Top250Specified OrElse Not Master.eSettings.MovieLockTop250) AndAlso ScrapeOptions.bMainTop250 AndAlso
                Master.eSettings.MovieScraperTop250 AndAlso Not new_Top250 Then
                DBMovie.Movie.Top250 = scrapedmovie.Top250
                new_Top250 = If(scrapedmovie.Top250Specified, True, False)
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTop250 AndAlso Not Master.eSettings.MovieLockTop250 Then
                DBMovie.Movie.Top250 = 0
            End If

            'Trailer
            If (Not DBMovie.Movie.TrailerSpecified OrElse Not Master.eSettings.MovieLockTrailer) AndAlso ScrapeOptions.bMainTrailer AndAlso
                scrapedmovie.TrailerSpecified AndAlso Master.eSettings.MovieScraperTrailer AndAlso Not new_Trailer Then
                If Master.eSettings.MovieScraperXBMCTrailerFormat AndAlso YouTube.UrlUtils.IsYouTubeURL(scrapedmovie.Trailer) Then
                    DBMovie.Movie.Trailer = StringUtils.ConvertFromYouTubeURLToKodiTrailerFormat(scrapedmovie.Trailer)
                Else
                    DBMovie.Movie.Trailer = scrapedmovie.Trailer
                End If
                new_Trailer = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTrailer AndAlso Not Master.eSettings.MovieLockTrailer Then
                DBMovie.Movie.Trailer = String.Empty
            End If

            'User Rating
            If (Not DBMovie.Movie.UserRatingSpecified OrElse Not Master.eSettings.MovieLockUserRating) AndAlso ScrapeOptions.bMainUserRating AndAlso
                scrapedmovie.UserRatingSpecified AndAlso Master.eSettings.MovieScraperUserRating AndAlso Not new_UserRating Then
                DBMovie.Movie.UserRating = scrapedmovie.UserRating
                new_UserRating = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperUserRating AndAlso Not Master.eSettings.MovieLockUserRating Then
                DBMovie.Movie.UserRating = 0
            End If

            'Year
            If (Not DBMovie.Movie.YearSpecified OrElse Not Master.eSettings.MovieLockYear) AndAlso ScrapeOptions.bMainYear AndAlso
                scrapedmovie.YearSpecified AndAlso Master.eSettings.MovieScraperYear AndAlso Not new_Year Then
                DBMovie.Movie.Year = scrapedmovie.Year
                new_Year = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperYear AndAlso Not Master.eSettings.MovieLockYear Then
                DBMovie.Movie.Year = 0
            End If

            'Runtime
            If (Not DBMovie.Movie.RuntimeSpecified OrElse Not Master.eSettings.MovieLockRuntime) AndAlso ScrapeOptions.bMainRuntime AndAlso
                scrapedmovie.RuntimeSpecified AndAlso Master.eSettings.MovieScraperRuntime AndAlso Not new_Runtime Then
                DBMovie.Movie.Runtime = scrapedmovie.Runtime
                new_Runtime = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRuntime AndAlso Not Master.eSettings.MovieLockRuntime Then
                DBMovie.Movie.Runtime = String.Empty
            End If

        Next

        'Certification for MPAA
        If DBMovie.Movie.CertificationsSpecified AndAlso Master.eSettings.MovieScraperCertForMPAA AndAlso
            (Not Master.eSettings.MovieScraperCertForMPAAFallback AndAlso (Not DBMovie.Movie.MPAASpecified OrElse Not Master.eSettings.MovieLockMPAA) OrElse
             Not new_MPAA AndAlso (Not DBMovie.Movie.MPAASpecified OrElse Not Master.eSettings.MovieLockMPAA)) Then
            Dim tmpstring As String = String.Empty
            tmpstring = If(Master.eSettings.MovieScraperCertLang = "us", StringUtils.USACertToMPAA(String.Join(" / ", DBMovie.Movie.Certifications.ToArray)), If(Master.eSettings.MovieScraperCertOnlyValue, String.Join(" / ", DBMovie.Movie.Certifications.ToArray).Split(Convert.ToChar(":"))(1), String.Join(" / ", DBMovie.Movie.Certifications.ToArray)))
            'only update DBMovie if scraped result is not empty/nothing!
            If Not String.IsNullOrEmpty(tmpstring) Then
                DBMovie.Movie.MPAA = tmpstring
            End If
        End If

        'MPAA value if MPAA is not available
        If Not DBMovie.Movie.MPAASpecified AndAlso Not String.IsNullOrEmpty(Master.eSettings.MovieScraperMPAANotRated) Then
            DBMovie.Movie.MPAA = Master.eSettings.MovieScraperMPAANotRated
        End If

        'OriginalTitle as Title
        If (Not DBMovie.Movie.TitleSpecified OrElse Not Master.eSettings.MovieLockTitle) AndAlso Master.eSettings.MovieScraperOriginalTitleAsTitle AndAlso DBMovie.Movie.OriginalTitleSpecified Then
            DBMovie.Movie.Title = DBMovie.Movie.OriginalTitle
        End If

        'Plot for Outline
        If ((Not DBMovie.Movie.OutlineSpecified OrElse Not Master.eSettings.MovieLockOutline) AndAlso Master.eSettings.MovieScraperPlotForOutline AndAlso Not Master.eSettings.MovieScraperPlotForOutlineIfEmpty) OrElse
            (Not DBMovie.Movie.OutlineSpecified AndAlso Master.eSettings.MovieScraperPlotForOutline AndAlso Master.eSettings.MovieScraperPlotForOutlineIfEmpty) Then
            DBMovie.Movie.Outline = StringUtils.ShortenOutline(DBMovie.Movie.Plot, Master.eSettings.MovieScraperOutlineLimit)
        End If

        'Rating/Votes
        'TODO: set the default rating/votes
        If (Not DBMovie.Movie.RatingSpecified OrElse Not Master.eSettings.MovieLockRating) AndAlso ScrapeOptions.bMainRating AndAlso
                DBMovie.Movie.RatingsSpecified AndAlso Master.eSettings.MovieScraperRating Then
            DBMovie.Movie.Rating = DBMovie.Movie.Ratings.Item(0).Value.ToString
            DBMovie.Movie.Votes = NumUtils.CleanVotes(DBMovie.Movie.Ratings.Item(0).Votes.ToString)
        ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRating AndAlso Not Master.eSettings.MovieLockRating Then
            DBMovie.Movie.Ratings.Clear()
            DBMovie.Movie.Rating = String.Empty
            DBMovie.Movie.Votes = String.Empty
        End If

        'UniqueID
        'TODO: set the default uniqueid

        Return DBMovie
    End Function

    Public Shared Function MergeDataScraperResults_MovieSet(ByVal DBMovieSet As Database.DBElement, ByVal ScrapedList As List(Of MediaContainers.MovieSet), ByVal ScrapeType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions) As Database.DBElement

        'protects the first scraped result against overwriting
        Dim new_Plot As Boolean = False
        Dim new_Title As Boolean = False

        For Each scrapedmovieset In ScrapedList

            'UniqueIDs
            If scrapedmovieset.UniqueIDsSpecified Then
                DBMovieSet.Movieset.UniqueIDs.AddRange(scrapedmovieset.UniqueIDs)
            End If

            'Plot
            If (Not DBMovieSet.Movieset.PlotSpecified OrElse Not Master.eSettings.MovieSetLockPlot) AndAlso ScrapeOptions.bMainPlot AndAlso
                scrapedmovieset.PlotSpecified AndAlso Master.eSettings.MovieSetScraperPlot AndAlso Not new_Plot Then
                DBMovieSet.Movieset.Plot = scrapedmovieset.Plot
                new_Plot = True
                'ElseIf Master.eSettings.MovieSetScraperCleanFields AndAlso Not Master.eSettings.MovieSetScraperPlot AndAlso Not Master.eSettings.MovieSetLockPlot Then
                '    DBMovieSet.MovieSet.Plot = String.Empty
            End If

            'Title
            If (Not DBMovieSet.Movieset.TitleSpecified OrElse Not Master.eSettings.MovieSetLockTitle) AndAlso ScrapeOptions.bMainTitle AndAlso
                 scrapedmovieset.TitleSpecified AndAlso Master.eSettings.MovieSetScraperTitle AndAlso Not new_Title Then
                DBMovieSet.Movieset.Title = scrapedmovieset.Title
                new_Title = True
                'ElseIf Master.eSettings.MovieSetScraperCleanFields AndAlso Not Master.eSettings.MovieSetScraperTitle AndAlso Not Master.eSettings.MovieSetLockTitle Then
                '    DBMovieSet.MovieSet.Title = String.Empty
            End If
        Next

        'set Title
        For Each sett As AdvancedSettingsSetting In AdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
            DBMovieSet.Movieset.Title = DBMovieSet.Movieset.Title.Replace(sett.Name.Substring(21), sett.Value)
        Next

        Return DBMovieSet
    End Function
    ''' <summary>
    ''' Returns the "merged" result of each data scraper results
    ''' </summary>
    ''' <param name="DBTV">TV Show to be scraped</param>
    ''' <param name="ScrapedList"><c>List(Of MediaContainers.TVShow)</c> which contains unfiltered results of each data scraper</param>
    ''' <returns>The scrape result of movie (after applying various global scraper settings here)</returns>
    ''' <remarks>
    ''' This is used to determine the result of data scraping by going through all scraperesults of every data scraper and applying global data scraper settings here!
    ''' 
    ''' 2014/09/01 Cocotus - First implementation: Moved all global lock settings in various data scrapers to this function, only apply them once and not in every data scraper module! Should be more maintainable!
    ''' </remarks>
    Public Shared Function MergeDataScraperResults_TV(ByVal DBTV As Database.DBElement, ByVal ScrapedList As List(Of MediaContainers.TVShow), ByVal ScrapeType As Enums.ScrapeType, ByVal ScrapeOptions As Structures.ScrapeOptions, ByVal withEpisodes As Boolean) As Database.DBElement

        'protects the first scraped result against overwriting
        Dim new_Actors As Boolean = False
        Dim new_Certification As Boolean = False
        Dim new_Creators As Boolean = False
        Dim new_Collections As Boolean = False
        Dim new_ShowCountries As Boolean = False
        Dim new_Credits As Boolean = False
        Dim new_Directors As Boolean = False
        Dim new_Genres As Boolean = False
        Dim new_MPAA As Boolean = False
        Dim new_Outline As Boolean = False
        Dim new_Plot As Boolean = False
        Dim new_Rating As Boolean = False
        Dim new_Premiered As Boolean = False
        Dim new_Runtime As Boolean = False
        Dim new_Status As Boolean = False
        Dim new_Studio As Boolean = False
        Dim new_Tagline As Boolean = False
        Dim new_Title As Boolean = False
        Dim new_OriginalTitle As Boolean = False
        Dim new_Trailer As Boolean = False
        Dim new_UserRating As Boolean = False

        Dim KnownEpisodesIndex As New List(Of KnownEpisode)
        Dim KnownSeasonsIndex As New List(Of Integer)

        ''If "Use Preview Datascraperresults" option is enabled, a preview window which displays all datascraperresults will be opened before showing the Edit Movie page!
        'If (ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape OrElse ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleField) AndAlso Master.eSettings.MovieScraperUseDetailView AndAlso ScrapedList.Count > 0 Then
        '    PreviewDataScraperResults(ScrapedList)
        'End If

        For Each scrapedshow In ScrapedList

            'UniqueIDs
            If scrapedshow.UniqueIDsSpecified Then
                DBTV.TVShow.UniqueIDs.AddRange(scrapedshow.UniqueIDs)
            End If

            'Actors
            If (Not DBTV.TVShow.ActorsSpecified OrElse Not Master.eSettings.TVLockShowActors) AndAlso ScrapeOptions.bMainActors AndAlso
                scrapedshow.ActorsSpecified AndAlso Master.eSettings.TVScraperShowActors AndAlso Not new_Actors Then
                If Master.eSettings.TVScraperCastWithImgOnly Then
                    FilterOnlyPersonsWithImage(scrapedshow.Actors)
                End If
                FilterCountLimit(Master.eSettings.TVScraperShowActorsLimit, scrapedshow.Actors)
                'added check if there's any actors left to add, if not then try with results of following scraper...
                If scrapedshow.ActorsSpecified Then
                    ReorderPersons(scrapedshow.Actors)
                    DBTV.TVShow.Actors = scrapedshow.Actors
                    new_Actors = True
                End If
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowActors AndAlso Not Master.eSettings.TVLockShowActors Then
                DBTV.TVShow.Actors.Clear()
            End If

            'Certification
            If (Not DBTV.TVShow.CertificationsSpecified OrElse Not Master.eSettings.TVLockShowCert) AndAlso ScrapeOptions.bMainCertifications AndAlso
                scrapedshow.CertificationsSpecified AndAlso Master.eSettings.TVScraperShowCert AndAlso Not new_Certification Then
                If Master.eSettings.TVScraperShowCertLang = Master.eLang.All Then
                    DBTV.TVShow.Certifications = scrapedshow.Certifications
                    new_Certification = True
                Else
                    Dim CertificationLanguage = APIXML.CertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.TVScraperShowCertLang)
                    If CertificationLanguage IsNot Nothing AndAlso CertificationLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(CertificationLanguage.name) Then
                        For Each tCert In scrapedshow.Certifications
                            If tCert.StartsWith(CertificationLanguage.name) Then
                                DBTV.TVShow.Certifications.Clear()
                                DBTV.TVShow.Certifications.Add(tCert)
                                new_Certification = True
                                Exit For
                            End If
                        Next
                    Else
                        logger.Error("TV Show Certification Language (Limit) not found. Please check your settings!")
                    End If
                End If
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowCert AndAlso Not Master.eSettings.TVLockShowCert Then
                DBTV.TVShow.Certifications.Clear()
            End If

            'Creators
            If (Not DBTV.TVShow.CreatorsSpecified OrElse Not Master.eSettings.TVLockShowCreators) AndAlso ScrapeOptions.bMainCreators AndAlso
                scrapedshow.CreatorsSpecified AndAlso Master.eSettings.TVScraperShowCreators AndAlso Not new_Creators Then
                DBTV.TVShow.Creators = scrapedshow.Creators
                new_Creators = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowCreators AndAlso Not Master.eSettings.TVLockShowCreators Then
                DBTV.TVShow.Creators.Clear()
            End If

            'Countries
            If (Not DBTV.TVShow.CountriesSpecified OrElse Not Master.eSettings.TVLockShowCountry) AndAlso ScrapeOptions.bMainCountries AndAlso
                scrapedshow.CountriesSpecified AndAlso Master.eSettings.TVScraperShowCountry AndAlso Not new_ShowCountries Then
                FilterCountLimit(Master.eSettings.TVScraperShowCountryLimit, scrapedshow.Countries)
                DBTV.TVShow.Countries = scrapedshow.Countries
                new_ShowCountries = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowCountry AndAlso Not Master.eSettings.TVLockShowCountry Then
                DBTV.TVShow.Countries.Clear()
            End If

            'EpisodeGuideURL
            If ScrapeOptions.bMainEpisodeGuide AndAlso scrapedshow.EpisodeGuideSpecified AndAlso Master.eSettings.TVScraperShowEpiGuideURL Then
                DBTV.TVShow.EpisodeGuide = scrapedshow.EpisodeGuide
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowEpiGuideURL Then
                DBTV.TVShow.EpisodeGuide = New MediaContainers.EpisodeGuide
            End If

            'Genres
            If (Not DBTV.TVShow.GenresSpecified OrElse Not Master.eSettings.TVLockShowGenre) AndAlso ScrapeOptions.bMainGenres AndAlso
                scrapedshow.GenresSpecified AndAlso Master.eSettings.TVScraperShowGenre AndAlso Not new_Genres Then
                StringUtils.GenreFilter(scrapedshow.Genres)
                FilterCountLimit(Master.eSettings.TVScraperShowGenreLimit, scrapedshow.Genres)
                DBTV.TVShow.Genres = scrapedshow.Genres
                new_Genres = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowGenre AndAlso Not Master.eSettings.TVLockShowGenre Then
                DBTV.TVShow.Genres.Clear()
            End If

            'MPAA
            If (Not DBTV.TVShow.MPAASpecified OrElse Not Master.eSettings.TVLockShowMPAA) AndAlso ScrapeOptions.bMainMPAA AndAlso
              scrapedshow.MPAASpecified AndAlso Master.eSettings.TVScraperShowMPAA AndAlso Not new_MPAA Then
                DBTV.TVShow.MPAA = scrapedshow.MPAA
                new_MPAA = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowMPAA AndAlso Not Master.eSettings.TVLockShowMPAA Then
                DBTV.TVShow.MPAA = String.Empty
            End If

            'Originaltitle
            If (Not DBTV.TVShow.OriginalTitleSpecified OrElse Not Master.eSettings.TVLockShowOriginalTitle) AndAlso ScrapeOptions.bMainOriginalTitle AndAlso
                scrapedshow.OriginalTitleSpecified AndAlso Master.eSettings.TVScraperShowOriginalTitle AndAlso Not new_OriginalTitle Then
                DBTV.TVShow.OriginalTitle = scrapedshow.OriginalTitle
                new_OriginalTitle = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowOriginalTitle AndAlso Not Master.eSettings.TVLockShowOriginalTitle Then
                DBTV.TVShow.OriginalTitle = String.Empty
            End If

            'Plot
            If (Not DBTV.TVShow.PlotSpecified OrElse Not Master.eSettings.TVLockShowPlot) AndAlso ScrapeOptions.bMainPlot AndAlso
                 scrapedshow.PlotSpecified AndAlso Master.eSettings.TVScraperShowPlot AndAlso Not new_Plot Then
                DBTV.TVShow.Plot = scrapedshow.Plot
                new_Plot = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowPlot AndAlso Not Master.eSettings.TVLockShowPlot Then
                DBTV.TVShow.Plot = String.Empty
            End If

            'Premiered
            If (Not DBTV.TVShow.PremieredSpecified OrElse Not Master.eSettings.TVLockShowPremiered) AndAlso ScrapeOptions.bMainPremiered AndAlso
                scrapedshow.PremieredSpecified AndAlso Master.eSettings.TVScraperShowPremiered AndAlso Not new_Premiered Then
                DBTV.TVShow.Premiered = NumUtils.DateToISO8601Date(scrapedshow.Premiered)
                new_Premiered = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowPremiered AndAlso Not Master.eSettings.TVLockShowPremiered Then
                DBTV.TVShow.Premiered = String.Empty
            End If

            'Ratings
            If (Not DBTV.TVShow.RatingsSpecified OrElse Not Master.eSettings.TVLockShowRating) AndAlso ScrapeOptions.bMainRating AndAlso
                 scrapedshow.RatingsSpecified AndAlso Master.eSettings.TVScraperShowRating Then
                For Each nRating In scrapedshow.Ratings
                    'remove old rating(s) from the same source
                    DBTV.TVShow.Ratings.RemoveAll(Function(f) f.Name = nRating.Name)
                    DBTV.TVShow.Ratings.Add(nRating)
                Next
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowRating AndAlso Not Master.eSettings.TVLockShowRating Then
                DBTV.TVShow.Ratings.Clear()
                DBTV.TVShow.Rating = String.Empty
                DBTV.TVShow.Votes = String.Empty
            End If

            'Runtime
            If (Not DBTV.TVShow.RuntimeSpecified OrElse Not Master.eSettings.TVLockShowRuntime) AndAlso ScrapeOptions.bMainRuntime AndAlso
                scrapedshow.RuntimeSpecified AndAlso Master.eSettings.TVScraperShowRuntime AndAlso Not new_Runtime Then
                DBTV.TVShow.Runtime = scrapedshow.Runtime
                new_Runtime = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowRuntime AndAlso Not Master.eSettings.TVLockShowRuntime Then
                DBTV.TVShow.Runtime = String.Empty
            End If


            'Status
            If (DBTV.TVShow.StatusSpecified OrElse Not Master.eSettings.TVLockShowStatus) AndAlso ScrapeOptions.bMainStatus AndAlso
                scrapedshow.StatusSpecified AndAlso Master.eSettings.TVScraperShowStatus AndAlso Not new_Status Then
                DBTV.TVShow.Status = scrapedshow.Status
                new_Status = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowStatus AndAlso Not Master.eSettings.TVLockShowStatus Then
                DBTV.TVShow.Status = String.Empty
            End If

            'Studios
            If (Not DBTV.TVShow.StudiosSpecified OrElse Not Master.eSettings.TVLockShowStudio) AndAlso ScrapeOptions.bMainStudios AndAlso
                scrapedshow.StudiosSpecified AndAlso Master.eSettings.TVScraperShowStudio AndAlso Not new_Studio Then

                FilterCountLimit(Master.eSettings.TVScraperShowStudioLimit, scrapedshow.Studios)
                DBTV.TVShow.Studios = scrapedshow.Studios
                new_Studio = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowStudio AndAlso Not Master.eSettings.TVLockShowStudio Then
                DBTV.TVShow.Studios.Clear()
            End If

            'Title
            If (Not DBTV.TVShow.TitleSpecified OrElse Not Master.eSettings.TVLockShowTitle) AndAlso ScrapeOptions.bMainTitle AndAlso
                scrapedshow.TitleSpecified AndAlso Master.eSettings.TVScraperShowTitle AndAlso Not new_Title Then
                DBTV.TVShow.Title = scrapedshow.Title
                new_Title = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowTitle AndAlso Not Master.eSettings.TVLockShowTitle Then
                DBTV.TVShow.Title = String.Empty
            End If

            'User Rating
            If (Not DBTV.TVShow.UserRatingSpecified OrElse Not Master.eSettings.TVLockShowUserRating) AndAlso ScrapeOptions.bMainUserRating AndAlso
                scrapedshow.UserRatingSpecified AndAlso Master.eSettings.TVScraperShowUserRating AndAlso Not new_UserRating Then
                DBTV.TVShow.UserRating = scrapedshow.UserRating
                new_UserRating = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperShowUserRating AndAlso Not Master.eSettings.TVLockShowUserRating Then
                DBTV.TVShow.UserRating = 0
            End If

            'Create KnowSeasons index
            For Each kSeason As MediaContainers.SeasonDetails In scrapedshow.KnownSeasons
                If Not KnownSeasonsIndex.Contains(kSeason.Season) Then
                    KnownSeasonsIndex.Add(kSeason.Season)
                End If
            Next

            'Create KnownEpisodes index (season and episode number)
            If withEpisodes Then
                For Each kEpisode As MediaContainers.EpisodeDetails In scrapedshow.KnownEpisodes
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
        If DBTV.TVShow.CertificationsSpecified AndAlso Master.eSettings.TVScraperShowCertForMPAA AndAlso
            (Not Master.eSettings.MovieScraperCertForMPAAFallback AndAlso (Not DBTV.TVShow.MPAASpecified OrElse Not Master.eSettings.TVLockShowMPAA) OrElse
             Not new_MPAA AndAlso (Not DBTV.TVShow.MPAASpecified OrElse Not Master.eSettings.TVLockShowMPAA)) Then

            Dim tmpstring As String = String.Empty
            tmpstring = If(Master.eSettings.TVScraperShowCertLang = "us", StringUtils.USACertToMPAA(String.Join(" / ", DBTV.TVShow.Certifications.ToArray)), If(Master.eSettings.TVScraperShowCertOnlyValue, String.Join(" / ", DBTV.TVShow.Certifications.ToArray).Split(Convert.ToChar(":"))(1), String.Join(" / ", DBTV.TVShow.Certifications.ToArray)))
            'only update DBMovie if scraped result is not empty/nothing!
            If Not String.IsNullOrEmpty(tmpstring) Then
                DBTV.TVShow.MPAA = tmpstring
            End If
        End If

        'MPAA value if MPAA is not available
        If Not DBTV.TVShow.MPAASpecified AndAlso Not String.IsNullOrEmpty(Master.eSettings.TVScraperShowMPAANotRated) Then
            DBTV.TVShow.MPAA = Master.eSettings.TVScraperShowMPAANotRated
        End If

        'OriginalTitle as Title
        If (Not DBTV.TVShow.TitleSpecified OrElse Not Master.eSettings.TVLockShowTitle) AndAlso Master.eSettings.TVScraperShowOriginalTitleAsTitle AndAlso DBTV.TVShow.OriginalTitleSpecified Then
            DBTV.TVShow.Title = DBTV.TVShow.OriginalTitle
        End If

        'Rating/Votes
        'TODO: set the default rating/votes

        'UniqueID
        'TODO: set the default uniqueid

        'Seasons
        For Each aKnownSeason As Integer In KnownSeasonsIndex
            'create a list of specified episode informations from all scrapers
            Dim ScrapedSeasonList As New List(Of MediaContainers.SeasonDetails)
            For Each nShow As MediaContainers.TVShow In ScrapedList
                For Each nSeasonDetails As MediaContainers.SeasonDetails In nShow.KnownSeasons.Where(Function(f) f.Season = aKnownSeason)
                    ScrapedSeasonList.Add(nSeasonDetails)
                Next
            Next
            'check if we have already saved season information for this scraped season
            Dim lSeasonList = DBTV.Seasons.Where(Function(f) f.TVSeason.Season = aKnownSeason)

            If lSeasonList IsNot Nothing AndAlso lSeasonList.Count > 0 Then
                For Each nSeason As Database.DBElement In lSeasonList
                    MergeDataScraperResults_TVSeason(nSeason, ScrapedSeasonList, ScrapeOptions)
                Next
            Else
                'no existing season found -> add it as "missing" season
                Dim mSeason As New Database.DBElement(Enums.ContentType.TVSeason) With {.TVSeason = New MediaContainers.SeasonDetails With {.Season = aKnownSeason}}
                mSeason = Master.DB.AddTVShowInfoToDBElement(mSeason, DBTV)
                DBTV.Seasons.Add(MergeDataScraperResults_TVSeason(mSeason, ScrapedSeasonList, ScrapeOptions))
            End If
        Next
        'add all season informations to TVShow (for saving season informations to tv show NFO)
        DBTV.TVShow.Seasons.Seasons.Clear()
        For Each kSeason As Database.DBElement In DBTV.Seasons.OrderBy(Function(f) f.TVSeason.Season)
            DBTV.TVShow.Seasons.Seasons.Add(kSeason.TVSeason)
        Next

        'Episodes
        If withEpisodes Then
            'update the tvshow information for each local episode
            For Each lEpisode In DBTV.Episodes
                lEpisode = Master.DB.AddTVShowInfoToDBElement(lEpisode, DBTV)
            Next

            For Each aKnownEpisode As KnownEpisode In KnownEpisodesIndex.OrderBy(Function(f) f.Episode).OrderBy(Function(f) f.Season)

                'convert the episode and season number if needed
                Dim iEpisode As Integer = -1
                Dim iSeason As Integer = -1
                Dim strAiredDate As String = aKnownEpisode.AiredDate
                If DBTV.EpisodeOrdering = Enums.EpisodeOrdering.Absolute Then
                    iEpisode = aKnownEpisode.EpisodeAbsolute
                    iSeason = 1
                ElseIf DBTV.EpisodeOrdering = Enums.EpisodeOrdering.DVD Then
                    iEpisode = CInt(aKnownEpisode.EpisodeDVD)
                    iSeason = aKnownEpisode.SeasonDVD
                ElseIf DBTV.EpisodeOrdering = Enums.EpisodeOrdering.Standard Then
                    iEpisode = aKnownEpisode.Episode
                    iSeason = aKnownEpisode.Season
                End If

                If Not iEpisode = -1 AndAlso Not iSeason = -1 Then
                    'create a list of specified episode informations from all scrapers
                    Dim ScrapedEpisodeList As New List(Of MediaContainers.EpisodeDetails)
                    For Each nShow As MediaContainers.TVShow In ScrapedList
                        For Each nEpisodeDetails As MediaContainers.EpisodeDetails In nShow.KnownEpisodes.Where(Function(f) f.Episode = aKnownEpisode.Episode AndAlso f.Season = aKnownEpisode.Season)
                            ScrapedEpisodeList.Add(nEpisodeDetails)
                        Next
                    Next

                    'check if we have a local episode file for this scraped episode
                    Dim lEpisodeList = DBTV.Episodes.Where(Function(f) f.FileItemSpecified AndAlso f.TVEpisode.Episode = iEpisode AndAlso f.TVEpisode.Season = iSeason)

                    If lEpisodeList IsNot Nothing AndAlso lEpisodeList.Count > 0 Then
                        For Each nEpisode As Database.DBElement In lEpisodeList
                            MergeDataScraperResults_TVEpisode(nEpisode, ScrapedEpisodeList, ScrapeOptions)
                        Next
                    Else
                        'try to get the episode by AiredDate
                        Dim dEpisodeList = DBTV.Episodes.Where(Function(f) f.FileItemSpecified AndAlso
                                                                   f.TVEpisode.Episode = -1 AndAlso
                                                                   f.TVEpisode.AiredSpecified AndAlso
                                                                   f.TVEpisode.Aired = strAiredDate)

                        If dEpisodeList IsNot Nothing AndAlso dEpisodeList.Count > 0 Then
                            For Each nEpisode As Database.DBElement In dEpisodeList
                                MergeDataScraperResults_TVEpisode(nEpisode, ScrapedEpisodeList, ScrapeOptions)
                                'we have to add the proper season and episode number if the episode was found by AiredDate
                                nEpisode.TVEpisode.Episode = iEpisode
                                nEpisode.TVEpisode.Season = iSeason
                            Next
                        Else
                            'no local episode found -> add it as "missing" episode
                            Dim mEpisode As New Database.DBElement(Enums.ContentType.TVEpisode) With {.TVEpisode = New MediaContainers.EpisodeDetails With {.Episode = iEpisode, .Season = iSeason}}
                            mEpisode = Master.DB.AddTVShowInfoToDBElement(mEpisode, DBTV)
                            MergeDataScraperResults_TVEpisode(mEpisode, ScrapedEpisodeList, ScrapeOptions)
                            If mEpisode.TVEpisode.TitleSpecified Then
                                DBTV.Episodes.Add(mEpisode)
                            Else
                                logger.Warn(String.Format("Missing Episode Ignored | {0} - S{1}E{2} | No Episode Title found", mEpisode.TVShow.Title, mEpisode.TVEpisode.Season, mEpisode.TVEpisode.Episode))
                            End If
                        End If
                    End If
                Else
                    logger.Warn("No valid episode or season number found")
                End If
            Next
        End If

        'create the "* All Seasons" entry if needed
        Dim tmpAllSeasons As Database.DBElement = DBTV.Seasons.FirstOrDefault(Function(f) f.TVSeason.IsAllSeasons)
        If tmpAllSeasons Is Nothing OrElse tmpAllSeasons.TVSeason Is Nothing Then
            tmpAllSeasons = New Database.DBElement(Enums.ContentType.TVSeason)
            tmpAllSeasons.TVSeason = New MediaContainers.SeasonDetails With {.Season = -1}
            tmpAllSeasons = Master.DB.AddTVShowInfoToDBElement(tmpAllSeasons, DBTV)
            DBTV.Seasons.Add(tmpAllSeasons)
        End If

        'cleanup seasons they don't have any episode
        Dim iIndex As Integer = 0
        While iIndex <= DBTV.Seasons.Count - 1
            Dim iSeason As Integer = DBTV.Seasons.Item(iIndex).TVSeason.Season
            If Not iSeason = -1 AndAlso DBTV.Episodes.Where(Function(f) f.TVEpisode.Season = iSeason).Count = 0 Then
                DBTV.Seasons.RemoveAt(iIndex)
            Else
                iIndex += 1
            End If
        End While

        Return DBTV
    End Function

    Public Shared Function MergeDataScraperResults_TVSeason(ByRef DBTVSeason As Database.DBElement, ByVal ScrapedList As List(Of MediaContainers.SeasonDetails), ByVal ScrapeOptions As Structures.ScrapeOptions) As Database.DBElement

        'protects the first scraped result against overwriting
        Dim new_Aired As Boolean = False
        Dim new_Plot As Boolean = False
        Dim new_Season As Boolean = False
        Dim new_Title As Boolean = False

        For Each scrapedseason In ScrapedList

            'UniqueIDs
            If scrapedseason.UniqueIDsSpecified Then
                DBTVSeason.TVSeason.UniqueIDs.AddRange(scrapedseason.UniqueIDs)
            End If

            'Season number
            If scrapedseason.SeasonSpecified AndAlso Not new_Season Then
                DBTVSeason.TVSeason.Season = scrapedseason.Season
                new_Season = True
            End If

            'Aired
            If (Not DBTVSeason.TVSeason.AiredSpecified OrElse Not Master.eSettings.TVLockEpisodeAired) AndAlso ScrapeOptions.bSeasonAired AndAlso
                scrapedseason.AiredSpecified AndAlso Master.eSettings.TVScraperEpisodeAired AndAlso Not new_Aired Then
                DBTVSeason.TVSeason.Aired = scrapedseason.Aired
                new_Aired = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeAired AndAlso Not Master.eSettings.TVLockEpisodeAired Then
                DBTVSeason.TVSeason.Aired = String.Empty
            End If

            'Plot
            If (Not DBTVSeason.TVSeason.PlotSpecified OrElse Not Master.eSettings.TVLockEpisodePlot) AndAlso ScrapeOptions.bSeasonPlot AndAlso
                scrapedseason.PlotSpecified AndAlso Master.eSettings.TVScraperEpisodePlot AndAlso Not new_Plot Then
                DBTVSeason.TVSeason.Plot = scrapedseason.Plot
                new_Plot = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodePlot AndAlso Not Master.eSettings.TVLockEpisodePlot Then
                DBTVSeason.TVSeason.Plot = String.Empty
            End If

            'Title
            If (Not DBTVSeason.TVSeason.TitleSpecified OrElse Not Master.eSettings.TVLockSeasonTitle) AndAlso ScrapeOptions.bSeasonTitle AndAlso
                scrapedseason.TitleSpecified AndAlso Master.eSettings.TVScraperSeasonTitle AndAlso Not new_Title Then
                Dim nTitle = StringUtils.FilterSeasonTitle(scrapedseason.Title)
                If Not String.IsNullOrEmpty(nTitle) Then
                    DBTVSeason.TVSeason.Title = nTitle
                    new_Title = True
                End If
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperSeasonTitle AndAlso Not Master.eSettings.TVLockSeasonTitle Then
                DBTVSeason.TVSeason.Title = String.Empty
            End If
        Next

        'UniqueID
        'TODO: set the default uniqueid

        Return DBTVSeason
    End Function
    ''' <summary>
    ''' Returns the "merged" result of each data scraper results
    ''' </summary>
    ''' <param name="DBTVEpisode">Episode to be scraped</param>
    ''' <param name="ScrapedList"><c>List(Of MediaContainers.EpisodeDetails)</c> which contains unfiltered results of each data scraper</param>
    ''' <returns>The scrape result of episode (after applying various global scraper settings here)</returns>
    ''' <remarks>
    ''' This is used to determine the result of data scraping by going through all scraperesults of every data scraper and applying global data scraper settings here!
    ''' 
    ''' 2014/09/01 Cocotus - First implementation: Moved all global lock settings in various data scrapers to this function, only apply them once and not in every data scraper module! Should be more maintainable!
    ''' </remarks>
    Private Shared Function MergeDataScraperResults_TVEpisode(ByRef DBTVEpisode As Database.DBElement, ByVal ScrapedList As List(Of MediaContainers.EpisodeDetails), ByVal ScrapeOptions As Structures.ScrapeOptions) As Database.DBElement

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
        Dim new_Runtime As Boolean = False
        Dim new_Season As Boolean = False
        Dim new_ThumbPoster As Boolean = False
        Dim new_Title As Boolean = False
        Dim new_UserRating As Boolean = False

        ''If "Use Preview Datascraperresults" option is enabled, a preview window which displays all datascraperresults will be opened before showing the Edit Movie page!
        'If (ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleScrape OrElse ScrapeType = Enums.ScrapeType_Movie_MovieSet_TV.SingleField) AndAlso Master.eSettings.MovieScraperUseDetailView AndAlso ScrapedList.Count > 0 Then
        '    PreviewDataScraperResults(ScrapedList)
        'End If

        For Each scrapedepisode In ScrapedList

            'UniqueIDs
            If scrapedepisode.UniqueIDsSpecified Then
                DBTVEpisode.TVEpisode.UniqueIDs.AddRange(scrapedepisode.UniqueIDs)
            End If

            'DisplayEpisode
            If scrapedepisode.DisplayEpisodeSpecified Then
                DBTVEpisode.TVEpisode.DisplayEpisode = scrapedepisode.DisplayEpisode
            End If

            'DisplaySeason
            If scrapedepisode.DisplaySeasonSpecified Then
                DBTVEpisode.TVEpisode.DisplaySeason = scrapedepisode.DisplaySeason
            End If

            'Actors
            If (Not DBTVEpisode.TVEpisode.ActorsSpecified OrElse Not Master.eSettings.TVLockEpisodeActors) AndAlso ScrapeOptions.bEpisodeActors AndAlso
                scrapedepisode.ActorsSpecified AndAlso Master.eSettings.TVScraperEpisodeActors AndAlso Not new_Actors Then

                If Master.eSettings.TVScraperCastWithImgOnly Then
                    FilterOnlyPersonsWithImage(scrapedepisode.Actors)
                End If
                FilterCountLimit(Master.eSettings.TVScraperEpisodeActorsLimit, scrapedepisode.Actors)
                'added check if there's any actors left to add, if not then try with results of following scraper...
                If scrapedepisode.ActorsSpecified Then
                    ReorderPersons(scrapedepisode.Actors)
                    DBTVEpisode.TVEpisode.Actors = scrapedepisode.Actors
                    new_Actors = True
                End If

            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeActors AndAlso Not Master.eSettings.TVLockEpisodeActors Then
                DBTVEpisode.TVEpisode.Actors.Clear()
            End If

            'Aired
            If (Not DBTVEpisode.TVEpisode.AiredSpecified OrElse Not Master.eSettings.TVLockEpisodeAired) AndAlso ScrapeOptions.bEpisodeAired AndAlso
                scrapedepisode.AiredSpecified AndAlso Master.eSettings.TVScraperEpisodeAired AndAlso Not new_Aired Then
                DBTVEpisode.TVEpisode.Aired = NumUtils.DateToISO8601Date(scrapedepisode.Aired)
                new_Aired = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeAired AndAlso Not Master.eSettings.TVLockEpisodeAired Then
                DBTVEpisode.TVEpisode.Aired = String.Empty
            End If

            'Credits
            If (Not DBTVEpisode.TVEpisode.CreditsSpecified OrElse Not Master.eSettings.TVLockEpisodeCredits) AndAlso
                scrapedepisode.CreditsSpecified AndAlso Master.eSettings.TVScraperEpisodeCredits AndAlso Not new_Credits Then
                DBTVEpisode.TVEpisode.Credits = scrapedepisode.Credits
                new_Credits = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeCredits AndAlso Not Master.eSettings.TVLockEpisodeCredits Then
                DBTVEpisode.TVEpisode.Credits.Clear()
            End If

            'Directors
            If (Not DBTVEpisode.TVEpisode.DirectorsSpecified OrElse Not Master.eSettings.TVLockEpisodeDirector) AndAlso ScrapeOptions.bEpisodeDirectors AndAlso
                scrapedepisode.DirectorsSpecified AndAlso Master.eSettings.TVScraperEpisodeDirector AndAlso Not new_Directors Then
                DBTVEpisode.TVEpisode.Directors = scrapedepisode.Directors
                new_Directors = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeDirector AndAlso Not Master.eSettings.TVLockEpisodeDirector Then
                DBTVEpisode.TVEpisode.Directors.Clear()
            End If

            'GuestStars
            If (Not DBTVEpisode.TVEpisode.GuestStarsSpecified OrElse Not Master.eSettings.TVLockEpisodeGuestStars) AndAlso ScrapeOptions.bEpisodeGuestStars AndAlso
                scrapedepisode.GuestStarsSpecified AndAlso Master.eSettings.TVScraperEpisodeGuestStars AndAlso Not new_GuestStars Then

                If Master.eSettings.TVScraperCastWithImgOnly Then
                    FilterOnlyPersonsWithImage(scrapedepisode.GuestStars)
                End If
                FilterCountLimit(Master.eSettings.TVScraperEpisodeGuestStarsLimit, scrapedepisode.GuestStars)
                'added check if there's any actors left to add, if not then try with results of following scraper...
                If scrapedepisode.GuestStarsSpecified Then
                    ReorderPersons(scrapedepisode.GuestStars)
                    DBTVEpisode.TVEpisode.GuestStars = scrapedepisode.GuestStars
                    new_GuestStars = True
                End If
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeGuestStars AndAlso Not Master.eSettings.TVLockEpisodeGuestStars Then
                DBTVEpisode.TVEpisode.GuestStars.Clear()
            End If

            'Plot
            If (Not DBTVEpisode.TVEpisode.PlotSpecified OrElse Not Master.eSettings.TVLockEpisodePlot) AndAlso ScrapeOptions.bEpisodePlot AndAlso
                scrapedepisode.PlotSpecified AndAlso Master.eSettings.TVScraperEpisodePlot AndAlso Not new_Plot Then
                DBTVEpisode.TVEpisode.Plot = scrapedepisode.Plot
                new_Plot = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodePlot AndAlso Not Master.eSettings.TVLockEpisodePlot Then
                DBTVEpisode.TVEpisode.Plot = String.Empty
            End If

            'Ratings
            If (Not DBTVEpisode.TVEpisode.RatingsSpecified OrElse Not Master.eSettings.TVLockEpisodeRating) AndAlso ScrapeOptions.bEpisodeRating AndAlso
                scrapedepisode.RatingsSpecified AndAlso Master.eSettings.TVScraperEpisodeRating Then
                For Each nRating In scrapedepisode.Ratings
                    'remove old rating(s) from the same source
                    DBTVEpisode.TVEpisode.Ratings.RemoveAll(Function(f) f.Name = nRating.Name)
                    DBTVEpisode.TVEpisode.Ratings.Add(nRating)
                Next
                DBTVEpisode.TVEpisode.Rating = scrapedepisode.Rating
                DBTVEpisode.TVEpisode.Votes = NumUtils.CleanVotes(scrapedepisode.Votes)
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeRating AndAlso Not Master.eSettings.TVLockEpisodeRating Then
                DBTVEpisode.TVEpisode.Ratings.Clear()
                DBTVEpisode.TVEpisode.Rating = String.Empty
                DBTVEpisode.TVEpisode.Votes = String.Empty
            End If

            'User Rating
            If (Not DBTVEpisode.TVEpisode.UserRatingSpecified OrElse Not Master.eSettings.TVLockEpisodeUserRating) AndAlso ScrapeOptions.bEpisodeUserRating AndAlso
                scrapedepisode.UserRatingSpecified AndAlso Master.eSettings.TVScraperEpisodeUserRating AndAlso Not new_UserRating Then
                DBTVEpisode.TVEpisode.UserRating = scrapedepisode.UserRating
                new_UserRating = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeUserRating AndAlso Not Master.eSettings.TVLockEpisodeUserRating Then
                DBTVEpisode.TVEpisode.UserRating = 0
            End If

            'Runtime
            If (Not DBTVEpisode.TVEpisode.RuntimeSpecified OrElse Not Master.eSettings.TVLockEpisodeRuntime) AndAlso ScrapeOptions.bEpisodeRuntime AndAlso
                scrapedepisode.RuntimeSpecified AndAlso Master.eSettings.TVScraperEpisodeRuntime AndAlso Not new_Runtime Then
                DBTVEpisode.TVEpisode.Runtime = scrapedepisode.Runtime
                new_Runtime = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeRuntime AndAlso Not Master.eSettings.TVLockEpisodeRuntime Then
                DBTVEpisode.TVEpisode.Runtime = String.Empty
            End If

            'ThumbPoster
            If (Not String.IsNullOrEmpty(scrapedepisode.ThumbPoster.URLOriginal) OrElse Not String.IsNullOrEmpty(scrapedepisode.ThumbPoster.URLThumb)) AndAlso Not new_ThumbPoster Then
                DBTVEpisode.TVEpisode.ThumbPoster = scrapedepisode.ThumbPoster
                new_ThumbPoster = True
            End If

            'Title
            If (Not DBTVEpisode.TVEpisode.TitleSpecified OrElse Not Master.eSettings.TVLockEpisodeTitle) AndAlso ScrapeOptions.bEpisodeTitle AndAlso
               scrapedepisode.TitleSpecified AndAlso Master.eSettings.TVScraperEpisodeTitle AndAlso Not new_Title Then
                DBTVEpisode.TVEpisode.Title = scrapedepisode.Title
                new_Title = True
            ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeTitle AndAlso Not Master.eSettings.TVLockEpisodeTitle Then
                DBTVEpisode.TVEpisode.Title = String.Empty
            End If
        Next

        'Add GuestStars to Actors
        If DBTVEpisode.TVEpisode.GuestStarsSpecified AndAlso Master.eSettings.TVScraperEpisodeGuestStarsToActors AndAlso Not Master.eSettings.TVLockEpisodeActors Then
            DBTVEpisode.TVEpisode.Actors.AddRange(DBTVEpisode.TVEpisode.GuestStars)

            'run the limit filter again
            FilterCountLimit(Master.eSettings.TVScraperEpisodeActorsLimit, DBTVEpisode.TVEpisode.Actors)

            'reorder again
            ReorderPersons(DBTVEpisode.TVEpisode.Actors)
        End If

        'TV Show Runtime for Episode Runtime
        If Not DBTVEpisode.TVEpisode.RuntimeSpecified AndAlso Master.eSettings.TVScraperUseSRuntimeForEp AndAlso DBTVEpisode.TVShow.RuntimeSpecified Then
            DBTVEpisode.TVEpisode.Runtime = DBTVEpisode.TVShow.Runtime
        End If

        'Rating/Votes
        'TODO: set the default rating/votes
        If (Not DBTVEpisode.TVEpisode.RatingSpecified OrElse Not Master.eSettings.TVLockEpisodeRating) AndAlso ScrapeOptions.bEpisodeRating AndAlso
                DBTVEpisode.TVEpisode.RatingsSpecified AndAlso Master.eSettings.TVScraperEpisodeRating Then
            DBTVEpisode.TVEpisode.Rating = DBTVEpisode.TVEpisode.Ratings.Item(0).Value.ToString
            DBTVEpisode.TVEpisode.Votes = NumUtils.CleanVotes(DBTVEpisode.TVEpisode.Ratings.Item(0).Votes.ToString)
        ElseIf Master.eSettings.TVScraperCleanFields AndAlso Not Master.eSettings.TVScraperEpisodeRating AndAlso Not Master.eSettings.TVLockEpisodeRating Then
            DBTVEpisode.TVEpisode.Ratings.Clear()
            DBTVEpisode.TVEpisode.Rating = String.Empty
            DBTVEpisode.TVEpisode.Votes = String.Empty
        End If

        'UniqueID
        'TODO: set the default uniqueid

        Return DBTVEpisode
    End Function

    Public Shared Function MergeDataScraperResults_TVEpisode_Single(ByRef DBTVEpisode As Database.DBElement, ByVal ScrapedList As List(Of MediaContainers.EpisodeDetails), ByVal ScrapeOptions As Structures.ScrapeOptions) As Database.DBElement
        Dim KnownEpisodesIndex As New List(Of KnownEpisode)

        For Each kEpisode As MediaContainers.EpisodeDetails In ScrapedList
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
                MergeDataScraperResults_TVEpisode(DBTVEpisode, ScrapedList, ScrapeOptions)
                If DBTVEpisode.TVEpisode.Episode = -1 Then DBTVEpisode.TVEpisode.Episode = iEpisode
                If DBTVEpisode.TVEpisode.Season = -1 Then DBTVEpisode.TVEpisode.Season = iSeason
            Else
                logger.Warn("No valid episode or season number found")
            End If
        Else
            logger.Warn("Episode could not be clearly determined.")
        End If

        Return DBTVEpisode
    End Function
    ''' <summary>
    ''' Open MovieDataScraperPreview Window
    ''' </summary>
    ''' <param name="ScrapedList"><c>List(Of MediaContainers.Movie)</c> which contains unfiltered results of each data scraper</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation: Display all scrapedresults in preview window, so that user can select the information which should be used
    ''' </remarks>
    Public Shared Sub PreviewDataScraperResults_Movie(ByRef ScrapedList As List(Of MediaContainers.Movie))
        Try
            Application.DoEvents()
            'Open/Show preview window
            Using dlgMovieDataScraperPreview As New dlgMovieDataScraperPreview(ScrapedList)
                Select Case dlgMovieDataScraperPreview.ShowDialog()
                    Case Windows.Forms.DialogResult.OK
                        'For now nothing here
                End Select
            End Using
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Function CleanNFO_Movies(ByVal mNFO As MediaContainers.Movie) As MediaContainers.Movie
        If mNFO IsNot Nothing Then
            mNFO.Outline = mNFO.Outline.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            mNFO.Plot = mNFO.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            mNFO.ReleaseDate = NumUtils.DateToISO8601Date(mNFO.ReleaseDate)
            mNFO.Votes = NumUtils.CleanVotes(mNFO.Votes)
            If mNFO.FileInfoSpecified Then
                If mNFO.FileInfo.StreamDetails.AudioSpecified Then
                    For Each aStream In mNFO.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        aStream.LongLanguage = Localization.ISOGetLangByCode3(aStream.Language)
                    Next
                End If
                If mNFO.FileInfo.StreamDetails.SubtitleSpecified Then
                    For Each sStream In mNFO.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        sStream.LongLanguage = Localization.ISOGetLangByCode3(sStream.Language)
                    Next
                End If
            End If
            If mNFO.SetsSpecified Then
                For i = mNFO.Sets.Count - 1 To 0 Step -1
                    If Not mNFO.Sets(i).TitleSpecified Then
                        mNFO.Sets.RemoveAt(i)
                    End If
                Next
            End If

            'changes a LongLanguage to Alpha2 code
            If mNFO.LanguageSpecified Then
                Dim Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Name = mNFO.Language)
                If Language IsNot Nothing Then
                    mNFO.Language = Language.Abbreviation
                Else
                    'check if it's a valid Alpha2 code or remove the information the use the source default language
                    Dim ShortLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = mNFO.Language)
                    If ShortLanguage Is Nothing Then
                        mNFO.Language = String.Empty
                    End If
                End If
            End If

            Return mNFO
        Else
            Return mNFO
        End If
    End Function

    Public Shared Function CleanNFO_TVEpisodes(ByVal eNFO As MediaContainers.EpisodeDetails) As MediaContainers.EpisodeDetails
        If eNFO IsNot Nothing Then
            eNFO.Aired = NumUtils.DateToISO8601Date(eNFO.Aired)
            eNFO.Votes = NumUtils.CleanVotes(eNFO.Votes)
            If eNFO.FileInfoSpecified Then
                If eNFO.FileInfo.StreamDetails.AudioSpecified Then
                    For Each aStream In eNFO.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        aStream.LongLanguage = Localization.ISOGetLangByCode3(aStream.Language)
                    Next
                End If
                If eNFO.FileInfo.StreamDetails.SubtitleSpecified Then
                    For Each sStream In eNFO.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                        sStream.LongLanguage = Localization.ISOGetLangByCode3(sStream.Language)
                    Next
                End If
            End If
            Return eNFO
        Else
            Return eNFO
        End If
    End Function

    Public Shared Function CleanNFO_TVShow(ByVal mNFO As MediaContainers.TVShow) As MediaContainers.TVShow
        If mNFO IsNot Nothing Then
            mNFO.Plot = mNFO.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            mNFO.Premiered = NumUtils.DateToISO8601Date(mNFO.Premiered)
            mNFO.Votes = NumUtils.CleanVotes(mNFO.Votes)

            'changes a LongLanguage to Alpha2 code
            If mNFO.LanguageSpecified Then
                Dim Language = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Name = mNFO.Language)
                If Language IsNot Nothing Then
                    mNFO.Language = Language.Abbreviation
                Else
                    'check if it's a valid Alpha2 code or remove the information the use the source default language
                    Dim ShortLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation = mNFO.Language)
                    If ShortLanguage Is Nothing Then
                        mNFO.Language = String.Empty
                    End If
                End If
            End If

            'Boxee support
            If Master.eSettings.TVUseBoxee Then
                If mNFO.BoxeeTvDbSpecified AndAlso Not mNFO.UniqueIDs.TVDbIdSpecified Then
                    mNFO.UniqueIDs.TVDbId = mNFO.BoxeeTvDb
                    mNFO.BlankBoxeeId()
                End If
            End If

            Return mNFO
        Else
            Return mNFO
        End If
    End Function
    ''' <summary>
    ''' Delete all movie NFOs
    ''' </summary>
    ''' <param name="DBMovie"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteNFO_Movie(ByVal DBMovie As Database.DBElement, ByVal ForceFileCleanup As Boolean)
        If Not DBMovie.FileItemSpecified Then Return

        Try
            For Each a In FileUtils.FileNames.GetFileNames(DBMovie, Enums.ModifierType.MainNFO, ForceFileCleanup)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovie.FileItem.FirstPathFromStack & ">")
        End Try
    End Sub
    ''' <summary>
    ''' Delete all movie NFOs
    ''' </summary>
    ''' <param name="DBMovieSet"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteNFO_MovieSet(ByVal DBMovieSet As Database.DBElement, ByVal ForceFileCleanup As Boolean, Optional bForceOldTitle As Boolean = False)
        If Not DBMovieSet.Movieset.TitleSpecified Then Return

        Try
            For Each a In FileUtils.FileNames.GetFileNames(DBMovieSet, Enums.ModifierType.MainNFO, bForceOldTitle)
                If File.Exists(a) Then
                    File.Delete(a)
                End If
            Next
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name & Convert.ToChar(Windows.Forms.Keys.Tab) & "<" & DBMovieSet.FileItem.FirstPathFromStack & ">")
        End Try
    End Sub

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
        Dim nFileInfo As New MediaContainers.FileInfo

        Select Case dbElement.ContentType
            Case Enums.ContentType.Movie
                nFileInfo = dbElement.Movie.FileInfo
            Case Enums.ContentType.TVEpisode
                nFileInfo = dbElement.TVEpisode.FileInfo
        End Select

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
            Select Case dbElement.ContentType
                Case Enums.ContentType.Movie
                    Return Master.eLang.GetString(419, "Meta Data is not available for this movie. Try rescanning.")
                Case Enums.ContentType.TVEpisode
                    Return Master.eLang.GetString(504, "Meta Data is not available for this episode. Try rescanning.")
            End Select
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Return the "best" or the "prefered language" audio stream of the videofile
    ''' </summary>
    ''' <param name="fileInfo"><c>MediaInfo.FileInfo</c> The Mediafile-container of the videofile</param>
    ''' <returns>The best <c>MediaInfo.Audio</c> stream information of the videofile</returns>
    ''' <remarks>
    ''' This is used to determine which audio stream information should be displayed in Ember main view (icon display)
    ''' The audiostream with most channels will be returned - if there are 2 or more streams which have the same "highest" channelcount then either the "DTSHD" stream or the one with highest bitrate will be returned
    ''' 
    ''' 2014/08/12 cocotus - Should work better: If there's more than one audiostream which highest channelcount, the one with highest bitrate or the DTSHD stream will be returned
    ''' </remarks>
    Public Shared Function GetBestAudio(ByVal fileInfo As MediaContainers.FileInfo, ByVal contentType As Enums.ContentType) As MediaContainers.Audio
        Dim nBestAudio As New MediaContainers.Audio
        Dim nFilteredAudio As New MediaContainers.FileInfo
        Dim bGetPrefLanguage As Boolean = False
        Dim bHasPrefLanguage As Boolean = False
        Dim strPrefLanguage As String = String.Empty
        Dim iHighestBitrate As Integer = 0
        Dim iHighestChannels As Integer = 0

        Select Case contentType
            Case Enums.ContentType.Movie
                If Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralFlagLang) Then
                    bGetPrefLanguage = True
                    strPrefLanguage = Master.eSettings.MovieGeneralFlagLang.ToLower
                End If
            Case Enums.ContentType.TVEpisode
                If Not String.IsNullOrEmpty(Master.eSettings.TVGeneralFlagLang) Then
                    bGetPrefLanguage = True
                    strPrefLanguage = Master.eSettings.TVGeneralFlagLang.ToLower
                End If
        End Select

        If bGetPrefLanguage AndAlso fileInfo.StreamDetails.Audio.Where(Function(f) f.LongLanguage.ToLower = strPrefLanguage).Count > 0 Then
            For Each Stream As MediaContainers.Audio In fileInfo.StreamDetails.Audio
                If Stream.LongLanguage.ToLower = strPrefLanguage Then
                    nFilteredAudio.StreamDetails.Audio.Add(Stream)
                End If
            Next
        Else
            nFilteredAudio.StreamDetails.Audio.AddRange(fileInfo.StreamDetails.Audio)
        End If

        For Each miAudio As MediaContainers.Audio In nFilteredAudio.StreamDetails.Audio
            If miAudio.ChannelsSpecified Then
                If miAudio.Channels >= iHighestChannels AndAlso (miAudio.Bitrate > iHighestBitrate OrElse miAudio.Bitrate = 0) Then
                    iHighestBitrate = miAudio.Bitrate
                    iHighestChannels = miAudio.Channels
                    nBestAudio.Bitrate = miAudio.Bitrate
                    nBestAudio.Channels = miAudio.Channels
                    nBestAudio.Codec = miAudio.Codec
                    nBestAudio.Language = miAudio.Language
                    nBestAudio.LongLanguage = miAudio.LongLanguage
                End If
            End If
            If bGetPrefLanguage AndAlso miAudio.LongLanguage.ToLower = strPrefLanguage Then nBestAudio.HasPreferred = True
        Next

        Return nBestAudio
    End Function

    Public Shared Function GetBestVideo(ByVal miFIV As MediaContainers.FileInfo) As MediaContainers.Video
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

    Public Shared Function GetIMDBFromNonConf(ByVal path As String, ByVal isSingle As Boolean) As NonConf
        Dim tNonConf As New NonConf
        Dim dirPath As String = Directory.GetParent(path).FullName
        Dim lstFiles As New List(Of String)

        If isSingle Then
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, "*.nfo"))
            Catch
            End Try
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, "*.info"))
            Catch
            End Try
        Else
            Dim fName As String = IO.Path.GetFileNameWithoutExtension(FileUtils.Common.RemoveStackingMarkers(path)).ToLower
            Dim oName As String = IO.Path.GetFileNameWithoutExtension(path)
            fName = If(fName.EndsWith("*"), fName, String.Concat(fName, "*"))
            oName = If(oName.EndsWith("*"), oName, String.Concat(oName, "*"))

            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(fName, ".nfo")))
            Catch
            End Try
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(oName, ".nfo")))
            Catch
            End Try
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(fName, ".info")))
            Catch
            End Try
            Try
                lstFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(oName, ".info")))
            Catch
            End Try
        End If

        For Each sFile As String In lstFiles
            Using srInfo As New StreamReader(sFile)
                Dim sInfo As String = srInfo.ReadToEnd
                Dim strIMDBID As String = Regex.Match(sInfo, "tt\d\d\d\d\d\d\d*", RegexOptions.Multiline Or RegexOptions.Singleline Or RegexOptions.IgnoreCase).ToString

                If Not String.IsNullOrEmpty(strIMDBID) Then
                    tNonConf.IMDBID = strIMDBID
                    'now lets try to see if the rest of the file is a proper nfo
                    If sInfo.ToLower.Contains("</movie>") Then
                        tNonConf.Text = APIXML.XMLToLowerCase(sInfo.Substring(0, sInfo.ToLower.IndexOf("</movie>") + 8))
                    End If
                    Exit For
                Else
                    strIMDBID = Regex.Match(path, "tt\d\d\d\d\d\d\d*", RegexOptions.Multiline Or RegexOptions.Singleline Or RegexOptions.IgnoreCase).ToString
                    If Not String.IsNullOrEmpty(strIMDBID) Then
                        tNonConf.IMDBID = strIMDBID
                    End If
                End If
            End Using
        Next
        Return tNonConf
    End Function

    Public Shared Function GetNfoPath_MovieSet(ByVal dbElement As Database.DBElement) As String
        For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO)
            If File.Exists(a) Then
                Return a
            End If
        Next

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

    Public Shared Function IsConformingNFO_Movie(ByVal path As String) As Boolean
        Dim testSer As XmlSerializer = Nothing

        Try
            If (IO.Path.GetExtension(path) = ".nfo" OrElse IO.Path.GetExtension(path) = ".info") AndAlso File.Exists(path) Then
                Using testSR As StreamReader = New StreamReader(path)
                    testSer = New XmlSerializer(GetType(MediaContainers.Movie))
                    Dim testMovie As MediaContainers.Movie = DirectCast(testSer.Deserialize(testSR), MediaContainers.Movie)
                    testMovie = Nothing
                    testSer = Nothing
                End Using
                Return True
            Else
                Return False
            End If
        Catch
            If testSer IsNot Nothing Then
                testSer = Nothing
            End If

            Return False
        End Try
    End Function

    Public Shared Function IsConformingNFO_TVEpisode(ByVal path As String) As Boolean
        Dim testSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.EpisodeDetails))
        Dim testEp As New MediaContainers.EpisodeDetails

        Try
            If (IO.Path.GetExtension(path) = ".nfo" OrElse IO.Path.GetExtension(path) = ".info") AndAlso File.Exists(path) Then
                Using xmlSR As StreamReader = New StreamReader(path)
                    Dim xmlStr As String = xmlSR.ReadToEnd
                    Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                    If rMatches.Count = 1 Then
                        Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                            testEp = DirectCast(testSer.Deserialize(xmlRead), MediaContainers.EpisodeDetails)
                            testSer = Nothing
                            testEp = Nothing
                            Return True
                        End Using
                    ElseIf rMatches.Count > 1 Then
                        'read them all... if one fails, the entire nfo is non conforming
                        For Each xmlReg As Match In rMatches
                            Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                testEp = DirectCast(testSer.Deserialize(xmlRead), MediaContainers.EpisodeDetails)
                                testEp = Nothing
                            End Using
                        Next
                        testSer = Nothing
                        Return True
                    Else
                        testSer = Nothing
                        If testEp IsNot Nothing Then
                            testEp = Nothing
                        End If
                        Return False
                    End If
                End Using
            Else
                testSer = Nothing
                testEp = Nothing
                Return False
            End If
        Catch
            If testSer IsNot Nothing Then
                testSer = Nothing
            End If
            If testEp IsNot Nothing Then
                testEp = Nothing
            End If
            Return False
        End Try
    End Function

    Public Shared Function IsConformingNFO_TVShow(ByVal path As String) As Boolean
        Dim testSer As XmlSerializer = Nothing

        Try
            If (IO.Path.GetExtension(path) = ".nfo" OrElse IO.Path.GetExtension(path) = ".info") AndAlso File.Exists(path) Then
                Using testSR As StreamReader = New StreamReader(path)
                    testSer = New XmlSerializer(GetType(MediaContainers.TVShow))
                    Dim testShow As MediaContainers.TVShow = DirectCast(testSer.Deserialize(testSR), MediaContainers.TVShow)
                    testShow = Nothing
                    testSer = Nothing
                End Using
                Return True
            Else
                Return False
            End If
        Catch
            If testSer IsNot Nothing Then
                testSer = Nothing
            End If

            Return False
        End Try
    End Function

    Public Shared Function LoadFromNFO_Movie(ByVal path As String, ByVal isSingle As Boolean) As MediaContainers.Movie
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlMov As New MediaContainers.Movie

        If Not String.IsNullOrEmpty(path) Then
            Try
                If File.Exists(path) AndAlso IO.Path.GetExtension(path).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(path)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.Movie))
                        xmlMov = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.Movie)
                        xmlMov = CleanNFO_Movies(xmlMov)
                    End Using
                Else
                    If Not String.IsNullOrEmpty(path) Then
                        Dim sReturn As New NonConf
                        sReturn = GetIMDBFromNonConf(path, isSingle)
                        xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                        Try
                            If Not String.IsNullOrEmpty(sReturn.Text) Then
                                Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                                    xmlSer = New XmlSerializer(GetType(MediaContainers.Movie))
                                    xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.Movie)
                                    xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                                    xmlMov = CleanNFO_Movies(xmlMov)
                                End Using
                            End If
                        Catch
                        End Try
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)

                xmlMov = New MediaContainers.Movie
                If Not String.IsNullOrEmpty(path) Then

                    'go ahead and rename it now, will still be picked up in getimdbfromnonconf
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_Movie(path, True)
                    End If

                    Dim sReturn As New NonConf
                    sReturn = GetIMDBFromNonConf(path, isSingle)
                    xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                    Try
                        If Not String.IsNullOrEmpty(sReturn.Text) Then
                            Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                                xmlSer = New XmlSerializer(GetType(MediaContainers.Movie))
                                xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.Movie)
                                xmlMov.UniqueIDs.IMDbId = sReturn.IMDBID
                                xmlMov = CleanNFO_Movies(xmlMov)
                            End Using
                        End If
                    Catch
                    End Try
                End If
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlMov
    End Function

    Public Shared Function LoadFromNFO_MovieSet(ByVal path As String) As MediaContainers.MovieSet
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlMovSet As New MediaContainers.MovieSet

        If Not String.IsNullOrEmpty(path) Then
            Try
                If File.Exists(path) AndAlso IO.Path.GetExtension(path).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(path)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.MovieSet))
                        xmlMovSet = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.MovieSet)
                        xmlMovSet.Plot = xmlMovSet.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                    End Using
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
                xmlMovSet = New MediaContainers.MovieSet
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlMovSet
    End Function

    Public Shared Function LoadFromNFO_TVEpisode(ByVal path As String, ByVal seasonNumber As Integer, ByVal episodeNumber As Integer) As MediaContainers.EpisodeDetails
        Dim xmlSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.EpisodeDetails))
        Dim xmlEp As New MediaContainers.EpisodeDetails

        If Not String.IsNullOrEmpty(path) AndAlso seasonNumber >= -1 Then
            Try
                If File.Exists(path) AndAlso IO.Path.GetExtension(path).ToLower = ".nfo" Then
                    'better way to read multi-root xml??
                    Using xmlSR As StreamReader = New StreamReader(path)
                        Dim xmlStr As String = xmlSR.ReadToEnd
                        Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                        If rMatches.Count = 1 Then
                            'only one episodedetail... assume it's the proper one
                            Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                                xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.EpisodeDetails)
                                xmlEp = CleanNFO_TVEpisodes(xmlEp)
                                xmlSer = Nothing
                                If xmlEp.FileInfoSpecified Then
                                    If xmlEp.FileInfo.StreamDetails.AudioSpecified Then
                                        For Each aStream In xmlEp.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            aStream.LongLanguage = Localization.ISOGetLangByCode3(aStream.Language)
                                        Next
                                    End If
                                    If xmlEp.FileInfo.StreamDetails.SubtitleSpecified Then
                                        For Each sStream In xmlEp.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            sStream.LongLanguage = Localization.ISOGetLangByCode3(sStream.Language)
                                        Next
                                    End If
                                End If
                                Return xmlEp
                            End Using
                        ElseIf rMatches.Count > 1 Then
                            For Each xmlReg As Match In rMatches
                                Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                    xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.EpisodeDetails)
                                    xmlEp = CleanNFO_TVEpisodes(xmlEp)
                                    If xmlEp.Episode = episodeNumber AndAlso xmlEp.Season = seasonNumber Then
                                        xmlSer = Nothing
                                        Return xmlEp
                                    End If
                                End Using
                            Next
                        End If
                    End Using

                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVEpisode(path, True)
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameNonConfNFO_TVEpisode(path, True)
                End If
            End Try
        End If

        Return New MediaContainers.EpisodeDetails
    End Function

    Public Shared Function LoadFromNFO_TVEpisode(ByVal path As String, ByVal seasonNumber As Integer, ByVal airedDate As String) As MediaContainers.EpisodeDetails
        Dim xmlSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.EpisodeDetails))
        Dim xmlEp As New MediaContainers.EpisodeDetails

        If Not String.IsNullOrEmpty(path) AndAlso seasonNumber >= -1 Then
            Try
                If File.Exists(path) AndAlso IO.Path.GetExtension(path).ToLower = ".nfo" Then
                    'better way to read multi-root xml??
                    Using xmlSR As StreamReader = New StreamReader(path)
                        Dim xmlStr As String = xmlSR.ReadToEnd
                        Dim rMatches As MatchCollection = Regex.Matches(xmlStr, "<episodedetails.*?>.*?</episodedetails>", RegexOptions.IgnoreCase Or RegexOptions.Singleline Or RegexOptions.IgnorePatternWhitespace)
                        If rMatches.Count = 1 Then
                            'only one episodedetail... assume it's the proper one
                            Using xmlRead As StringReader = New StringReader(rMatches(0).Value)
                                xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.EpisodeDetails)
                                xmlEp = CleanNFO_TVEpisodes(xmlEp)
                                xmlSer = Nothing
                                If xmlEp.FileInfoSpecified Then
                                    If xmlEp.FileInfo.StreamDetails.AudioSpecified Then
                                        For Each aStream In xmlEp.FileInfo.StreamDetails.Audio.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            aStream.LongLanguage = Localization.ISOGetLangByCode3(aStream.Language)
                                        Next
                                    End If
                                    If xmlEp.FileInfo.StreamDetails.SubtitleSpecified Then
                                        For Each sStream In xmlEp.FileInfo.StreamDetails.Subtitle.Where(Function(f) f.LanguageSpecified AndAlso Not f.LongLanguageSpecified)
                                            sStream.LongLanguage = Localization.ISOGetLangByCode3(sStream.Language)
                                        Next
                                    End If
                                End If
                                Return xmlEp
                            End Using
                        ElseIf rMatches.Count > 1 Then
                            For Each xmlReg As Match In rMatches
                                Using xmlRead As StringReader = New StringReader(xmlReg.Value)
                                    xmlEp = DirectCast(xmlSer.Deserialize(xmlRead), MediaContainers.EpisodeDetails)
                                    xmlEp = CleanNFO_TVEpisodes(xmlEp)
                                    If xmlEp.Aired = airedDate AndAlso xmlEp.Season = seasonNumber Then
                                        xmlSer = Nothing
                                        Return xmlEp
                                    End If
                                End Using
                            Next
                        End If
                    End Using

                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVEpisode(path, True)
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameNonConfNFO_TVEpisode(path, True)
                End If
            End Try
        End If

        Return New MediaContainers.EpisodeDetails
    End Function

    Public Shared Function LoadFromNFO_TVShow(ByVal path As String) As MediaContainers.TVShow
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlShow As New MediaContainers.TVShow

        If Not String.IsNullOrEmpty(path) Then
            Try
                If File.Exists(path) AndAlso IO.Path.GetExtension(path).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(path)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.TVShow))
                        xmlShow = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.TVShow)
                        xmlShow = CleanNFO_TVShow(xmlShow)
                    End Using
                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVShow(path)
                    End If
                End If

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)

                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameNonConfNFO_TVShow(path)
                End If
            End Try

            Try
                Dim params As New List(Of Object)(New Object() {xmlShow})
                Dim doContinue As Boolean = True
                AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFORead_TVShow, params, doContinue, False)

            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If xmlSer IsNot Nothing Then
                xmlSer = Nothing
            End If
        End If

        Return xmlShow
    End Function

    Private Shared Sub RenameNonConfNFO_Movie(ByVal path As String, ByVal isChecked As Boolean)
        'test if current nfo is non-conforming... rename per setting

        Try
            If isChecked OrElse Not IsConformingNFO_Movie(path) Then
                If isChecked OrElse File.Exists(path) Then
                    RenameToInfo(path)
                End If
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameNonConfNFO_TVEpisode(ByVal path As String, ByVal isChecked As Boolean)
        'test if current nfo is non-conforming... rename per setting

        Try
            If File.Exists(path) AndAlso Not IsConformingNFO_TVEpisode(path) Then
                RenameToInfo(path)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameNonConfNFO_TVShow(ByVal path As String)
        'test if current nfo is non-conforming... rename per setting

        Try
            If File.Exists(path) AndAlso Not IsConformingNFO_TVShow(path) Then
                RenameToInfo(path)
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub RenameToInfo(ByVal path As String)
        Try
            Dim i As Integer = 1
            Dim strNewName As String = String.Concat(FileUtils.Common.RemoveExtFromPath(path), ".info")
            'in case there is already a .info file
            If File.Exists(strNewName) Then
                Do
                    strNewName = String.Format("{0}({1}).info", FileUtils.Common.RemoveExtFromPath(path), i)
                    i += 1
                Loop While File.Exists(strNewName)
                strNewName = String.Format("{0}({1}).info", FileUtils.Common.RemoveExtFromPath(path), i)
            End If
            File.Move(path, strNewName)
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Shared Sub ReorderPersons(ByRef lstPerson As List(Of MediaContainers.Person))
        Dim iOrder As Integer = 0
        For Each nPerson In lstPerson
            nPerson.Order = iOrder
            iOrder += 1
        Next
    End Sub

    Public Shared Sub SaveToNFO_Movie(ByRef dbElement As Database.DBElement, ByVal forceFileCleanup As Boolean)
        Try
            Try
                Dim params As New List(Of Object)(New Object() {dbElement})
                Dim doContinue As Boolean = True
                AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_Movie, params, doContinue, False)
                If Not doContinue Then Return
            Catch ex As Exception
                logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try

            If dbElement.FileItemSpecified Then
                'cleanup old NFOs if needed
                If forceFileCleanup Then DeleteNFO_Movie(dbElement, forceFileCleanup)

                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tMovie As MediaContainers.Movie = CType(dbElement.Movie.CloneDeep, MediaContainers.Movie)

                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.Movie))
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                'YAMJ support
                If Master.eSettings.MovieUseYAMJ AndAlso Master.eSettings.MovieNFOYAMJ Then
                    If tMovie.UniqueIDs.TMDbIdSpecified Then
                        tMovie.UniqueIDs.TMDbId = String.Empty
                    End If
                End If

                'digit grouping symbol for Votes count
                If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                    If tMovie.VotesSpecified Then
                        Dim vote As String = Double.Parse(tMovie.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        If vote IsNot Nothing Then tMovie.Votes = vote
                    End If
                End If

                For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO)
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_Movie(a, False)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then
                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If
                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, tMovie)
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_MovieSet(ByRef dbElement As Database.DBElement)
        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {moviesetToSave})
            '    Dim doContinue As Boolean = True
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieSetNFOSave, params, doContinue, False)
            '    If Not doContinue Then Return
            'Catch ex As Exception
            'End Try

            If Not String.IsNullOrEmpty(dbElement.Movieset.Title) Then
                If dbElement.Movieset.TitleHasChanged Then DeleteNFO_MovieSet(dbElement, False, True)

                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.MovieSet))
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO)
                    'If Not Master.eSettings.GeneralOverwriteNfo Then
                    '    RenameNonConfNfo(a, False)
                    'End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then
                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If
                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, dbElement.Movieset)
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_TVEpisode(ByRef dbElement As Database.DBElement)
        Try
            If dbElement.FileItemSpecified Then
                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tTVEpisode As MediaContainers.EpisodeDetails = CType(dbElement.TVEpisode.CloneDeep, MediaContainers.EpisodeDetails)

                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.EpisodeDetails))

                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True
                Dim EpList As New List(Of MediaContainers.EpisodeDetails)
                Dim sBuilder As New StringBuilder

                For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.EpisodeNFO)
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVEpisode(a, False)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then

                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If

                        Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                            'TODO: move that query to clsAPIDatabase
                            SQLCommand.CommandText = "SELECT idEpisode FROM episode WHERE idEpisode <> (?) AND idFile IN (SELECT idFile FROM file WHERE path = (?)) ORDER BY Episode"
                            Dim parID As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parID", DbType.Int64, 0, "idEpisode")
                            Dim parPath As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parPath", DbType.String, 0, "path")

                            parID.Value = dbElement.ID
                            parPath.Value = dbElement.FileItem.FullPath

                            Using SQLreader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                                While SQLreader.Read
                                    EpList.Add(Master.DB.Load_TVEpisode(Convert.ToInt64(SQLreader("idEpisode")), False).TVEpisode)
                                End While
                            End Using

                            EpList.Add(tTVEpisode)

                            Dim NS As New XmlSerializerNamespaces
                            NS.Add(String.Empty, String.Empty)

                            For Each tvEp As MediaContainers.EpisodeDetails In EpList.OrderBy(Function(s) s.Season).OrderBy(Function(e) e.Episode)

                                'digit grouping symbol for Votes count
                                If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                                    If tvEp.VotesSpecified Then
                                        Dim vote As String = Double.Parse(tvEp.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                                        If vote IsNot Nothing Then tvEp.Votes = vote
                                    End If
                                End If

                                'removing <displayepisode> and <displayseason> if disabled
                                If Not Master.eSettings.TVScraperUseDisplaySeasonEpisode Then
                                    tvEp.DisplayEpisode = -1
                                    tvEp.DisplaySeason = -1
                                End If

                                Using xmlSW As New Utf8StringWriter
                                    xmlSer.Serialize(xmlSW, tvEp, NS)
                                    If sBuilder.Length > 0 Then
                                        sBuilder.Append(Environment.NewLine)
                                        xmlSW.GetStringBuilder.Remove(0, xmlSW.GetStringBuilder.ToString.IndexOf(Environment.NewLine) + 1)
                                    End If
                                    sBuilder.Append(xmlSW.ToString)
                                End Using
                            Next

                            dbElement.NfoPath = a

                            If sBuilder.Length > 0 Then
                                Using fSW As New StreamWriter(a)
                                    fSW.Write(sBuilder.ToString)
                                End Using
                            End If
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Public Shared Sub SaveToNFO_TVShow(ByRef dbElement As Database.DBElement)
        Try
            Dim params As New List(Of Object)(New Object() {dbElement})
            Dim doContinue As Boolean = True
            AddonsManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_TVShow, params, doContinue, False)
            If Not doContinue Then Return
        Catch ex As Exception
        End Try

        Try
            If dbElement.ShowPathSpecified Then
                'Create a clone of MediaContainer to prevent changes on database data that only needed in NFO
                Dim tTVShow As MediaContainers.TVShow = CType(dbElement.TVShow.CloneDeep, MediaContainers.TVShow)

                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.TVShow))
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                'Boxee support
                If Master.eSettings.TVUseBoxee Then
                    If tTVShow.UniqueIDs.TVDbIdSpecified Then
                        tTVShow.BoxeeTvDb = tTVShow.UniqueIDs.TVDbId
                        tTVShow.BlankId()
                    End If
                End If

                'digit grouping symbol for Votes count
                If Master.eSettings.GeneralDigitGrpSymbolVotes Then
                    If tTVShow.VotesSpecified Then
                        Dim vote As String = Double.Parse(tTVShow.Votes, Globalization.CultureInfo.InvariantCulture).ToString("N0", Globalization.CultureInfo.CurrentCulture)
                        If vote IsNot Nothing Then tTVShow.Votes = vote
                    End If
                End If

                For Each a In FileUtils.FileNames.GetFileNames(dbElement, Enums.ModifierType.MainNFO)
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNFO_TVShow(a)
                    End If

                    doesExist = File.Exists(a)
                    If Not doesExist OrElse (Not CBool(File.GetAttributes(a) And FileAttributes.ReadOnly)) Then

                        If doesExist Then
                            fAtt = File.GetAttributes(a)
                            Try
                                File.SetAttributes(a, FileAttributes.Normal)
                            Catch ex As Exception
                                fAttWritable = False
                            End Try
                        End If

                        Using xmlSW As New StreamWriter(a)
                            dbElement.NfoPath = a
                            xmlSer.Serialize(xmlSW, tTVShow)
                        End Using

                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Class NonConf

#Region "Properties"

        Public Property IMDBID() As String = String.Empty

        Public Property Text() As String = String.Empty

#End Region 'Properties

    End Class

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

    Public NotInheritable Class Utf8StringWriter
        Inherits StringWriter
        Public Overloads Overrides ReadOnly Property Encoding() As Encoding
            Get
                Return Encoding.UTF8
            End Get
        End Property
    End Class

#End Region 'Nested Types

End Class