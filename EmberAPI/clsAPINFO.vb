﻿' ################################################################################
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
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Serialization
Imports NLog
Imports System.Windows.Forms

Public Class NFO

#Region "Fields"
    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()
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
    Public Shared Function MergeDataScraperResults(ByVal DBMovie As Structures.DBMovie, ByVal ScrapedList As List(Of MediaContainers.Movie), ByVal ScrapeType As Enums.ScrapeType, ByVal Options As Structures.ScrapeOptions_Movie) As Structures.DBMovie

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
        Dim new_Rating As Boolean = False
        Dim new_ReleaseDate As Boolean = False
        Dim new_Runtime As Boolean = False
        Dim new_Studio As Boolean = False
        Dim new_Tagline As Boolean = False
        Dim new_Title As Boolean = False
        Dim new_Top250 As Boolean = False
        Dim new_Trailer As Boolean = False
        Dim new_Votes As Boolean = False
        Dim new_Year As Boolean = False

        'If "Use Preview Datascraperresults" option is enabled, a preview window which displays all datascraperresults will be opened before showing the Edit Movie page!
        If (ScrapeType = Enums.ScrapeType.SingleScrape OrElse ScrapeType = Enums.ScrapeType.SingleField) AndAlso Master.eSettings.MovieScraperUseDetailView AndAlso ScrapedList.Count > 0 Then
            PreviewDataScraperResults(ScrapedList)
        End If

        For Each scrapedmovie In ScrapedList

            'TMDBScraper fills ID and TMDBID, IMDB-Scraper fills IMDBID - add that to nMovie!
            If Not String.IsNullOrEmpty(scrapedmovie.IMDBID) Then
                DBMovie.Movie.IMDBID = scrapedmovie.IMDBID
            End If
            If Not String.IsNullOrEmpty(scrapedmovie.ID) Then
                DBMovie.Movie.ID = scrapedmovie.ID
            End If
            If Not String.IsNullOrEmpty(scrapedmovie.TMDBID) Then
                DBMovie.Movie.TMDBID = scrapedmovie.TMDBID
            End If

            'Originaltitle
            If (String.IsNullOrEmpty(DBMovie.Movie.OriginalTitle) OrElse Not Master.eSettings.MovieLockOriginalTitle) AndAlso Options.bOriginalTitle AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.OriginalTitle) AndAlso Master.eSettings.MovieScraperOriginalTitle AndAlso Not new_OriginalTitle Then
                DBMovie.Movie.OriginalTitle = scrapedmovie.OriginalTitle
                new_OriginalTitle = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperOriginalTitle AndAlso Not Master.eSettings.MovieLockOriginalTitle Then
                DBMovie.Movie.OriginalTitle = String.Empty
            End If

            'Title
            If (String.IsNullOrEmpty(DBMovie.Movie.Title) OrElse Not Master.eSettings.MovieLockTitle) AndAlso Options.bTitle AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Title) AndAlso Master.eSettings.MovieScraperTitle AndAlso Not new_Title Then
                DBMovie.Movie.Title = scrapedmovie.Title
                new_Title = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTitle AndAlso Not Master.eSettings.MovieLockTitle Then
                DBMovie.Movie.Title = String.Empty
            End If

            'Year
            If (String.IsNullOrEmpty(DBMovie.Movie.Year) OrElse Not Master.eSettings.MovieLockYear) AndAlso Options.bYear AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Year) AndAlso Master.eSettings.MovieScraperYear AndAlso Not new_Year Then
                DBMovie.Movie.Year = scrapedmovie.Year
                new_Year = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperYear AndAlso Not Master.eSettings.MovieLockYear Then
                DBMovie.Movie.Year = String.Empty
            End If

            'MPAA
            If (String.IsNullOrEmpty(DBMovie.Movie.MPAA) OrElse Not Master.eSettings.MovieLockMPAA) AndAlso Options.bMPAA AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.MPAA) AndAlso Master.eSettings.MovieScraperMPAA AndAlso Not new_MPAA Then
                DBMovie.Movie.MPAA = scrapedmovie.MPAA
                new_MPAA = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperMPAA AndAlso Not Master.eSettings.MovieLockMPAA Then
                DBMovie.Movie.MPAA = String.Empty
            End If

            'Certification
            If (DBMovie.Movie.Certifications.Count < 1 OrElse Not Master.eSettings.MovieLockCert) AndAlso Options.bCert AndAlso _
                scrapedmovie.Certifications.Count > 0 AndAlso Master.eSettings.MovieScraperCert AndAlso Not new_Certification Then
                If Master.eSettings.MovieScraperCertLang = Master.eLang.All Then
                    DBMovie.Movie.Certifications.Clear()
                    DBMovie.Movie.Certifications.AddRange(scrapedmovie.Certifications)
                    new_Certification = True
                Else
                    For Each tCert In scrapedmovie.Certifications
                        If tCert.StartsWith(APIXML.MovieCertLanguagesXML.Language.FirstOrDefault(Function(l) l.abbreviation = Master.eSettings.MovieScraperCertLang).name) Then
                            DBMovie.Movie.Certifications.Clear()
                            DBMovie.Movie.Certifications.Add(tCert)
                            new_Certification = True
                            Exit For
                        End If
                    Next
                End If
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCert AndAlso Not Master.eSettings.MovieLockCert Then
                DBMovie.Movie.Certifications.Clear()
            End If

            'ReleaseDate
            If (String.IsNullOrEmpty(DBMovie.Movie.ReleaseDate) OrElse Not Master.eSettings.MovieLockReleaseDate) AndAlso Options.bRelease AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.ReleaseDate) AndAlso Master.eSettings.MovieScraperRelease AndAlso Not new_ReleaseDate Then
                If Master.eSettings.MovieScraperReleaseFormat = False Then
                    Dim formatteddate As Date
                    If DateTime.TryParseExact(scrapedmovie.ReleaseDate, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentUICulture, Globalization.DateTimeStyles.None, formatteddate) Then
                        DBMovie.Movie.ReleaseDate = Strings.FormatDateTime(formatteddate, Microsoft.VisualBasic.DateFormat.ShortDate).ToString
                    Else
                        DBMovie.Movie.ReleaseDate = scrapedmovie.ReleaseDate
                    End If
                Else
                    DBMovie.Movie.ReleaseDate = scrapedmovie.ReleaseDate
                End If
                new_ReleaseDate = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRelease AndAlso Not Master.eSettings.MovieLockReleaseDate Then
                DBMovie.Movie.ReleaseDate = String.Empty
            End If

            'Rating
            If (String.IsNullOrEmpty(DBMovie.Movie.Rating) OrElse DBMovie.Movie.Runtime = "0" OrElse Not Master.eSettings.MovieLockRating) AndAlso Options.bRating AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Rating) AndAlso Not scrapedmovie.Rating = "0" AndAlso Master.eSettings.MovieScraperRating AndAlso Not new_Rating Then
                DBMovie.Movie.Rating = scrapedmovie.Rating
                new_Rating = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRating AndAlso Not Master.eSettings.MovieLockRating Then
                DBMovie.Movie.Rating = String.Empty
            End If

            'Trailer
            If (String.IsNullOrEmpty(DBMovie.Movie.Trailer) OrElse Not Master.eSettings.MovieLockTrailer) AndAlso Options.bTrailer AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Trailer) AndAlso Master.eSettings.MovieScraperTrailer AndAlso Not new_Trailer Then
                If Master.eSettings.MovieScraperXBMCTrailerFormat Then
                    DBMovie.Movie.Trailer = Replace(scrapedmovie.Trailer.Trim, "http://www.youtube.com/watch?v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
                    DBMovie.Movie.Trailer = Replace(DBMovie.Movie.Trailer, "http://www.youtube.com/watch?hd=1&v=", "plugin://plugin.video.youtube/?action=play_video&videoid=")
                Else
                    DBMovie.Movie.Trailer = scrapedmovie.Trailer
                End If
                new_Trailer = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTrailer AndAlso Not Master.eSettings.MovieLockTrailer Then
                DBMovie.Movie.Trailer = String.Empty
            End If

            'Votes
            If (String.IsNullOrEmpty(DBMovie.Movie.Votes) OrElse DBMovie.Movie.Runtime = "0" OrElse Not Master.eSettings.MovieLockVotes) AndAlso Options.bVotes AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Votes) AndAlso Not scrapedmovie.Votes = "0" AndAlso Master.eSettings.MovieScraperVotes AndAlso Not new_Votes Then
                DBMovie.Movie.Votes = scrapedmovie.Votes
                new_Votes = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperVotes AndAlso Not Master.eSettings.MovieLockVotes Then
                DBMovie.Movie.Votes = String.Empty
            End If

            'Top250
            If (String.IsNullOrEmpty(DBMovie.Movie.Top250) OrElse Not Master.eSettings.MovieLockTop250) AndAlso Options.bTop250 AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Top250) AndAlso Master.eSettings.MovieScraperTop250 AndAlso Not new_Top250 Then
                DBMovie.Movie.Top250 = scrapedmovie.Top250
                new_Top250 = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTop250 AndAlso Not Master.eSettings.MovieLockTop250 Then
                DBMovie.Movie.Top250 = String.Empty
            End If

            'Actors
            If (DBMovie.Movie.Actors.Count < 1 OrElse Not Master.eSettings.MovieLockActors) AndAlso Options.bCast AndAlso _
                scrapedmovie.Actors.Count > 0 AndAlso Master.eSettings.MovieScraperCast AndAlso Not new_Actors Then

                If Master.eSettings.MovieScraperCastWithImgOnly Then
                    For i = scrapedmovie.Actors.Count - 1 To 0 Step -1
                        If String.IsNullOrEmpty(scrapedmovie.Actors(i).Thumb) Then
                            scrapedmovie.Actors.RemoveAt(i)
                        End If
                    Next
                End If

                If Master.eSettings.MovieScraperCastLimit > 0 AndAlso scrapedmovie.Actors.Count > Master.eSettings.MovieScraperCastLimit Then
                    scrapedmovie.Actors.RemoveRange(Master.eSettings.MovieScraperCastLimit, scrapedmovie.Actors.Count - Master.eSettings.MovieScraperCastLimit)
                End If

                DBMovie.Movie.Actors = scrapedmovie.Actors
                'added check if there's any actors left to add, if not then try with results of following scraper...
                If scrapedmovie.Actors.Count > 0 Then
                    new_Actors = True
                End If

            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCast AndAlso Not Master.eSettings.MovieLockActors Then
                DBMovie.Movie.Actors.Clear()
            End If

            'Tagline
            If (String.IsNullOrEmpty(DBMovie.Movie.Tagline) OrElse Not Master.eSettings.MovieLockTagline) AndAlso Options.bTagline AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Tagline) AndAlso Master.eSettings.MovieScraperTagline AndAlso Not new_Tagline Then
                DBMovie.Movie.Tagline = scrapedmovie.Tagline
                new_Tagline = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperTagline AndAlso Not Master.eSettings.MovieLockTagline Then
                DBMovie.Movie.Tagline = String.Empty
            End If

            'Director
            'If (String.IsNullOrEmpty(DBMovie.Movie.Director) OrElse Not Master.eSettings.MovieLockDirector) AndAlso Not String.IsNullOrEmpty(scrapedmovie.Director) AndAlso Master.eSettings.MovieScraperDirector Then
            '    DBMovie.Movie.Director = scrapedmovie.Director
            'End If

            'Directors
            If (DBMovie.Movie.Directors.Count < 1 OrElse Not Master.eSettings.MovieLockDirector) AndAlso Options.bDirector AndAlso _
                scrapedmovie.Directors.Count > 0 AndAlso Master.eSettings.MovieScraperDirector AndAlso Not new_Directors Then
                DBMovie.Movie.Directors.Clear()
                DBMovie.Movie.Directors.AddRange(scrapedmovie.Directors)
                new_Directors = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperDirector AndAlso Not Master.eSettings.MovieLockDirector Then
                DBMovie.Movie.Directors.Clear()
            End If

            'Country
            'If (String.IsNullOrEmpty(DBMovie.Movie.Country) OrElse Not Master.eSettings.MovieLockCountry) AndAlso Not String.IsNullOrEmpty(scrapedmovie.Country) AndAlso Master.eSettings.MovieScraperCountry Then
            '    DBMovie.Movie.Country = scrapedmovie.Country
            'End If

            'Countries
            If (DBMovie.Movie.Countries.Count < 1 OrElse Not Master.eSettings.MovieLockCountry) AndAlso Options.bCountry AndAlso _
                scrapedmovie.Countries.Count > 0 AndAlso Master.eSettings.MovieScraperCountry AndAlso Not new_Countries Then
                DBMovie.Movie.Countries.Clear()
                DBMovie.Movie.Countries.AddRange(scrapedmovie.Countries)
                new_Countries = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCountry AndAlso Not Master.eSettings.MovieLockCountry Then
                DBMovie.Movie.Countries.Clear()
            End If

            'Outline
            If (String.IsNullOrEmpty(DBMovie.Movie.Outline) OrElse Not Master.eSettings.MovieLockOutline) AndAlso Options.bOutline AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Outline) AndAlso Master.eSettings.MovieScraperOutline AndAlso (Not new_Outline OrElse (Master.eSettings.MovieScraperOutlinePlotEnglishOverwrite AndAlso StringUtils.isEnglishText(DBMovie.Movie.Outline))) Then
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
            If (String.IsNullOrEmpty(DBMovie.Movie.Plot) OrElse Not Master.eSettings.MovieLockPlot) AndAlso Options.bPlot AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Plot) AndAlso Master.eSettings.MovieScraperPlot AndAlso (Not new_Plot OrElse (Master.eSettings.MovieScraperOutlinePlotEnglishOverwrite AndAlso StringUtils.isEnglishText(DBMovie.Movie.Plot))) Then
                DBMovie.Movie.Plot = scrapedmovie.Plot
                new_Plot = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperPlot AndAlso Not Master.eSettings.MovieLockPlot Then
                DBMovie.Movie.Plot = String.Empty
            End If
            'check if brackets should be removed...
            If Master.eSettings.MovieScraperCleanPlotOutline Then
                DBMovie.Movie.Plot = StringUtils.RemoveBrackets(DBMovie.Movie.Plot)
            End If

            'Genres
            If (DBMovie.Movie.Genres.Count < 1 OrElse Not Master.eSettings.MovieLockGenre) AndAlso Options.bGenre AndAlso _
                scrapedmovie.Genres.Count > 0 AndAlso Master.eSettings.MovieScraperGenre AndAlso Not new_Genres Then
                'Check if scraped genre(s) are in user language and filter list if not!
                'TODO StringUtils.GenreFilter too much "/" joins/array-converts for my taste - just work with List of String in future! 
                Dim tGenre As String = Strings.Join(scrapedmovie.Genres.ToArray, "/").Trim
                Dim _genres As New List(Of String)
                tGenre = StringUtils.GenreFilter(tGenre)
                If Not String.IsNullOrEmpty(tGenre) Then
                    Dim sGenres() As String = Strings.Split(tGenre, "/")
                    _genres.AddRange(sGenres.ToList)
                End If

                If Master.eSettings.MovieScraperGenreLimit > 0 AndAlso Master.eSettings.MovieScraperGenreLimit < _genres.Count AndAlso _genres.Count > 0 Then
                    _genres.RemoveRange(Master.eSettings.MovieScraperGenreLimit, _genres.Count - Master.eSettings.MovieScraperGenreLimit)
                End If
                DBMovie.Movie.Genres.Clear()
                DBMovie.Movie.Genres.AddRange(_genres)
                new_Genres = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperGenre AndAlso Not Master.eSettings.MovieLockGenre Then
                DBMovie.Movie.Genres.Clear()
            End If

            'Runtime
            If (String.IsNullOrEmpty(DBMovie.Movie.Runtime) OrElse DBMovie.Movie.Runtime = "0" OrElse Not Master.eSettings.MovieLockRuntime) AndAlso Options.bRuntime AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.Runtime) AndAlso Not scrapedmovie.Runtime = "0" AndAlso Master.eSettings.MovieScraperRuntime AndAlso Not new_Runtime Then
                DBMovie.Movie.Runtime = scrapedmovie.Runtime
                new_Runtime = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperRuntime AndAlso Not Master.eSettings.MovieLockRuntime Then
                DBMovie.Movie.Runtime = String.Empty
            End If

            'Studios
            If (DBMovie.Movie.Studios.Count < 1 OrElse Not Master.eSettings.MovieLockStudio) AndAlso Options.bStudio AndAlso _
                scrapedmovie.Studios.Count > 0 AndAlso Master.eSettings.MovieScraperStudio AndAlso Not new_Studio Then
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

                If Master.eSettings.MovieScraperStudioLimit > 0 AndAlso Master.eSettings.MovieScraperStudioLimit < _studios.Count AndAlso _studios.Count > 0 Then
                    _studios.RemoveRange(Master.eSettings.MovieScraperStudioLimit, _studios.Count - Master.eSettings.MovieScraperStudioLimit)
                End If


                DBMovie.Movie.Studios.AddRange(_studios)
                'added check if there's any studios left to add, if not then try with results of following scraper...
                If _studios.Count > 0 Then
                    new_Studio = True
                End If


            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperStudio AndAlso Not Master.eSettings.MovieLockStudio Then
                DBMovie.Movie.Studios.Clear()
            End If

            'OldCredits: Writers/Producers/MusicBy/OtherCrew - its all in this field
            'If (String.IsNullOrEmpty(DBMovie.Movie.OldCredits) OrElse Not Master.eSettings.MovieLockWriters) AndAlso Not String.IsNullOrEmpty(scrapedmovie.OldCredits) AndAlso Master.eSettings.MovieScraperCrew Then
            '    DBMovie.Movie.OldCredits = scrapedmovie.OldCredits
            'End If

            'Credits
            If (DBMovie.Movie.Credits.Count < 1 OrElse Not Master.eSettings.MovieLockCredits) AndAlso _
                scrapedmovie.Credits.Count > 0 AndAlso Master.eSettings.MovieScraperCredits AndAlso Not new_Credits Then
                DBMovie.Movie.Credits.Clear()
                DBMovie.Movie.Credits.AddRange(scrapedmovie.Credits)
                new_Credits = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCredits AndAlso Not Master.eSettings.MovieLockCredits Then
                DBMovie.Movie.Credits.Clear()
            End If

            'Collection ID
            If (String.IsNullOrEmpty(DBMovie.Movie.TMDBColID) OrElse Not Master.eSettings.MovieLockCollectionID) AndAlso Options.bCollectionID AndAlso _
                Not String.IsNullOrEmpty(scrapedmovie.TMDBColID) AndAlso Master.eSettings.MovieScraperCollectionID AndAlso Not new_CollectionID Then
                DBMovie.Movie.TMDBColID = scrapedmovie.TMDBColID
                new_CollectionID = True
            ElseIf Master.eSettings.MovieScraperCleanFields AndAlso Not Master.eSettings.MovieScraperCollectionID AndAlso Not Master.eSettings.MovieLockCollectionID Then
                DBMovie.Movie.TMDBColID = String.Empty
            End If

            'Collections
            If (DBMovie.Movie.Sets.Count = 0 OrElse Not Master.eSettings.MovieLockCollections) AndAlso _
                scrapedmovie.Sets.Count > 0 AndAlso Master.eSettings.MovieScraperCollectionsAuto AndAlso Not new_Collections Then
                DBMovie.Movie.Sets.Clear()
                For Each movieset In scrapedmovie.Sets
                    If Not String.IsNullOrEmpty(movieset.Title) Then
                        For Each sett As AdvancedSettingsSetting In clsAdvancedSettings.GetAllSettings.Where(Function(y) y.Name.StartsWith("MovieSetTitleRenamer:"))
                            movieset.Title = Replace(movieset.Title, sett.Name.Substring(21), sett.Value)
                        Next
                    End If
                Next
                DBMovie.Movie.Sets.AddRange(scrapedmovie.Sets)
                new_Collections = True
            End If
            'TODO Tags
        Next

        'Plot for Outline
        '2015/02/12 Cooctus Fixed 
        'Before: If there was no outline scraped at all, plot won't be used for outline ever! Also outline was overwritten regardless if it was empty or not before! (Option in Ember is described: Use Plot for Outline if Outline is Empty)
        'Now: - Instead of checking only the outline we check if new plot or/and outline was set - if one of those was updated, copy plot to outline!
        '     - Overwrite outline only if outline was empty or if option overwritenglishtext is true!
        If (String.IsNullOrEmpty(DBMovie.Movie.Outline) OrElse Not Master.eSettings.MovieLockOutline) AndAlso _
            Master.eSettings.MovieScraperPlotForOutline AndAlso Options.bOutline AndAlso (new_Plot OrElse new_Outline) Then
            If Not String.IsNullOrEmpty(DBMovie.Movie.Plot) AndAlso ((Master.eSettings.MovieScraperOutlinePlotEnglishOverwrite AndAlso StringUtils.isEnglishText(DBMovie.Movie.Outline)) OrElse String.IsNullOrEmpty(DBMovie.Movie.Outline)) Then
                DBMovie.Movie.Outline = StringUtils.ShortenOutline(DBMovie.Movie.Plot, Master.eSettings.MovieScraperOutlineLimit)
            End If
        End If

        'Certification for MPAA
        If DBMovie.Movie.Certifications.Count > 0 AndAlso Master.eSettings.MovieScraperCertForMPAA AndAlso _
            (Not Master.eSettings.MovieScraperCertForMPAAFallback AndAlso (String.IsNullOrEmpty(DBMovie.Movie.MPAA) OrElse Not Master.eSettings.MovieLockMPAA) OrElse _
             Not new_MPAA AndAlso (String.IsNullOrEmpty(DBMovie.Movie.MPAA) OrElse Not Master.eSettings.MovieLockMPAA)) Then

            Dim tmpstring As String = String.Empty
            tmpstring = If(Master.eSettings.MovieScraperCertLang = "us", StringUtils.USACertToMPAA(DBMovie.Movie.Certification), If(Master.eSettings.MovieScraperCertOnlyValue, DBMovie.Movie.Certification.Split(Convert.ToChar(":"))(1), DBMovie.Movie.Certification))
            'only update DBMovie if scraped result is not empty/nothing!
            If Not String.IsNullOrEmpty(tmpstring) Then
                DBMovie.Movie.MPAA = tmpstring
            End If
        End If

        'set ListTitle at the end of merging
        If Not String.IsNullOrEmpty(DBMovie.Movie.Title) Then
            Dim tTitle As String = StringUtils.FilterTokens_Movie(DBMovie.Movie.Title)
            If Master.eSettings.MovieDisplayYear AndAlso Not String.IsNullOrEmpty(DBMovie.Movie.Year) Then
                DBMovie.ListTitle = String.Format("{0} ({1})", tTitle, DBMovie.Movie.Year)
            Else
                DBMovie.ListTitle = tTitle
            End If
        Else
            If FileUtils.Common.isVideoTS(DBMovie.Filename) Then
                DBMovie.ListTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).Name)
            ElseIf FileUtils.Common.isBDRip(DBMovie.Filename) Then
                DBMovie.ListTitle = StringUtils.FilterName_Movie(Directory.GetParent(Directory.GetParent(Directory.GetParent(DBMovie.Filename).FullName).FullName).Name)
            Else
                If DBMovie.UseFolder AndAlso DBMovie.IsSingle Then
                    DBMovie.ListTitle = StringUtils.FilterName_Movie(Directory.GetParent(DBMovie.Filename).Name)
                Else
                    DBMovie.ListTitle = StringUtils.FilterName_Movie(Path.GetFileNameWithoutExtension(DBMovie.Filename))
                End If
            End If
        End If

        Return DBMovie
    End Function
    ''' <summary>
    ''' Open MovieDataScraperPreview Window
    ''' </summary>
    ''' <param name="ScrapedList"><c>List(Of MediaContainers.Movie)</c> which contains unfiltered results of each data scraper</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation: Display all scrapedresults in preview window, so that user can select the information which should be used
    ''' </remarks>
    Public Shared Sub PreviewDataScraperResults(ByRef ScrapedList As List(Of MediaContainers.Movie))
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
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Function CleanNFO_Movies(ByVal mNFO As MediaContainers.Movie) As MediaContainers.Movie
        If Not IsNothing(mNFO) Then
            mNFO.Genre = Strings.Join(mNFO.LGenre.ToArray, " / ")
            mNFO.Outline = mNFO.Outline.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
            mNFO.Plot = mNFO.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
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
            If mNFO.Sets.Count > 0 Then
                For i = mNFO.Sets.Count - 1 To 0 Step -1
                    If Not mNFO.Sets(i).TitleSpecified Then
                        mNFO.Sets.RemoveAt(i)
                    End If
                Next
            End If
            Return mNFO
        Else
            Return mNFO
        End If
    End Function

    Public Shared Function CleanNFO_TVEpisodes(ByVal eNFO As MediaContainers.EpisodeDetails) As MediaContainers.EpisodeDetails
        If Not IsNothing(eNFO) Then
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

    Public Shared Function FIToString(ByVal miFI As MediaInfo.Fileinfo, ByVal isTV As Boolean) As String
        '//
        ' Convert Fileinfo into a string to be displayed in the GUI
        '\\

        Dim strOutput As New StringBuilder
        Dim iVS As Integer = 1
        Dim iAS As Integer = 1
        Dim iSS As Integer = 1

        Try
            If Not IsNothing(miFI) Then

                If Not IsNothing(miFI.StreamDetails) Then
                    If miFI.StreamDetails.Video.Count > 0 Then
                        strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(595, "Video Streams"), miFI.StreamDetails.Video.Count.ToString, vbNewLine)
                    End If

                    If miFI.StreamDetails.Audio.Count > 0 Then
                        strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(596, "Audio Streams"), miFI.StreamDetails.Audio.Count.ToString, vbNewLine)
                    End If

                    If miFI.StreamDetails.Subtitle.Count > 0 Then
                        strOutput.AppendFormat("{0}: {1}{2}", Master.eLang.GetString(597, "Subtitle  Streams"), miFI.StreamDetails.Subtitle.Count.ToString, vbNewLine)
                    End If

                    For Each miVideo As MediaInfo.Video In miFI.StreamDetails.Video
                        strOutput.AppendFormat("{0}{1} {2}{0}", vbNewLine, Master.eLang.GetString(617, "Video Stream"), iVS)
                        If Not String.IsNullOrEmpty(miVideo.Width) AndAlso Not String.IsNullOrEmpty(miVideo.Height) Then
                            strOutput.AppendFormat("- {0}{1}", String.Format(Master.eLang.GetString(269, "Size: {0}x{1}"), miVideo.Width, miVideo.Height), vbNewLine)
                        End If
                        If Not String.IsNullOrEmpty(miVideo.Aspect) Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(614, "Aspect Ratio"), miVideo.Aspect, vbNewLine)
                        If Not String.IsNullOrEmpty(miVideo.Scantype) Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(605, "Scan Type"), miVideo.Scantype, vbNewLine)
                        If Not String.IsNullOrEmpty(miVideo.Codec) Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(604, "Codec"), miVideo.Codec, vbNewLine)
                        If Not String.IsNullOrEmpty(miVideo.Bitrate) Then strOutput.AppendFormat("- {0}: {1}{2}", "Bitrate", miVideo.Bitrate, vbNewLine)
                        If Not String.IsNullOrEmpty(miVideo.MultiViewCount) Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(1156, "MultiView Count"), miVideo.MultiViewCount, vbNewLine)
                        If Not String.IsNullOrEmpty(miVideo.MultiViewLayout) Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(1157, "MultiView Layout"), miVideo.MultiViewLayout, vbNewLine)
                        If Not String.IsNullOrEmpty(miVideo.Duration) Then strOutput.AppendFormat("- {0}: {1}", Master.eLang.GetString(609, "Duration"), miVideo.Duration)
                        If Not String.IsNullOrEmpty(miVideo.LongLanguage) Then strOutput.AppendFormat("{0}- {1}: {2}", vbNewLine, Master.eLang.GetString(610, "Language"), miVideo.LongLanguage)
                        iVS += 1
                    Next

                    strOutput.Append(vbNewLine)

                    For Each miAudio As MediaInfo.Audio In miFI.StreamDetails.Audio
                        'audio
                        strOutput.AppendFormat("{0}{1} {2}{0}", vbNewLine, Master.eLang.GetString(618, "Audio Stream"), iAS.ToString)
                        If Not String.IsNullOrEmpty(miAudio.Codec) Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(604, "Codec"), miAudio.Codec, vbNewLine)
                        If Not String.IsNullOrEmpty(miAudio.Channels) Then strOutput.AppendFormat("- {0}: {1}{2}", Master.eLang.GetString(611, "Channels"), miAudio.Channels, vbNewLine)
                        If Not String.IsNullOrEmpty(miAudio.Bitrate) Then strOutput.AppendFormat("- {0}: {1}{2}", "Bitrate", miAudio.Bitrate, vbNewLine)
                        If Not String.IsNullOrEmpty(miAudio.LongLanguage) Then strOutput.AppendFormat("- {0}: {1}", Master.eLang.GetString(610, "Language"), miAudio.LongLanguage)
                        iAS += 1
                    Next

                    strOutput.Append(vbNewLine)

                    For Each miSub As MediaInfo.Subtitle In miFI.StreamDetails.Subtitle
                        'subtitles
                        strOutput.AppendFormat("{0}{1} {2}{0}", vbNewLine, Master.eLang.GetString(619, "Subtitle Stream"), iSS.ToString)
                        If Not String.IsNullOrEmpty(miSub.LongLanguage) Then strOutput.AppendFormat("- {0}: {1}", Master.eLang.GetString(610, "Language"), miSub.LongLanguage)
                        iSS += 1
                    Next
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        If strOutput.ToString.Trim.Length > 0 Then
            Return strOutput.ToString
        Else
            If isTV Then
                Return Master.eLang.GetString(504, "Meta Data is not available for this episode. Try rescanning.")
            Else
                Return Master.eLang.GetString(419, "Meta Data is not available for this movie. Try rescanning.")
            End If
        End If
    End Function

    ''' <summary>
    ''' Return the "best" or the "prefered language" audio stream of the videofile
    ''' </summary>
    ''' <param name="miFIA"><c>MediaInfo.Fileinfo</c> The Mediafile-container of the videofile</param>
    ''' <returns>The best <c>MediaInfo.Audio</c> stream information of the videofile</returns>
    ''' <remarks>
    ''' This is used to determine which audio stream information should be displayed in Ember main view (icon display)
    ''' The audiostream with most channels will be returned - if there are 2 or more streams which have the same "highest" channelcount then either the "DTSHD" stream or the one with highest bitrate will be returned
    ''' 
    ''' 2014/08/12 cocotus - Should work better: If there's more than one audiostream which highest channelcount, the one with highest bitrate or the DTSHD stream will be returned
    ''' </remarks>
    Public Shared Function GetBestAudio(ByVal miFIA As MediaInfo.Fileinfo, ByVal ForTV As Boolean) As MediaInfo.Audio
        '//
        ' Get the highest values from file info
        '\\

        Dim fiaOut As New MediaInfo.Audio
        Try
            Dim cmiFIA As New MediaInfo.Fileinfo

            Dim getPrefLanguage As Boolean = False
            Dim hasPrefLanguage As Boolean = False
            Dim prefLanguage As String = String.Empty
            Dim sinMostChannels As Single = 0
            Dim sinChans As Single = 0
            Dim sinMostBitrate As Single = 0
            Dim sinBitrate As Single = 0
            Dim sinCodec As String = String.Empty
            fiaOut.Codec = String.Empty
            fiaOut.Channels = String.Empty
            fiaOut.Language = String.Empty
            fiaOut.LongLanguage = String.Empty
            fiaOut.Bitrate = String.Empty

            If ForTV Then
                If Not String.IsNullOrEmpty(Master.eSettings.TVGeneralFlagLang) Then
                    getPrefLanguage = True
                    prefLanguage = Master.eSettings.TVGeneralFlagLang.ToLower
                End If
            Else
                If Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralFlagLang) Then
                    getPrefLanguage = True
                    prefLanguage = Master.eSettings.MovieGeneralFlagLang.ToLower
                End If
            End If

            If getPrefLanguage AndAlso miFIA.StreamDetails.Audio.Where(Function(f) f.LongLanguage.ToLower = prefLanguage).Count > 0 Then
                For Each Stream As MediaInfo.Audio In miFIA.StreamDetails.Audio
                    If Stream.LongLanguage.ToLower = prefLanguage Then
                        cmiFIA.StreamDetails.Audio.Add(Stream)
                    End If
                Next
            Else
                cmiFIA.StreamDetails.Audio.AddRange(miFIA.StreamDetails.Audio)
            End If

            For Each miAudio As MediaInfo.Audio In cmiFIA.StreamDetails.Audio
                If Not String.IsNullOrEmpty(miAudio.Channels) Then
                    sinChans = NumUtils.ConvertToSingle(EmberAPI.MediaInfo.FormatAudioChannel(miAudio.Channels))
                    sinBitrate = 0
                    If IsNumeric(miAudio.Bitrate) Then
                        sinBitrate = CInt(miAudio.Bitrate)
                    End If
                    If sinChans >= sinMostChannels AndAlso (sinBitrate > sinMostBitrate OrElse miAudio.Codec.ToLower.Contains("dtshd") OrElse sinBitrate = 0) Then
                        If IsNumeric(miAudio.Bitrate) Then
                            sinMostBitrate = CInt(miAudio.Bitrate)
                        End If
                        sinMostChannels = sinChans
                        fiaOut.Bitrate = miAudio.Bitrate
                        fiaOut.Channels = sinChans.ToString
                        fiaOut.Codec = miAudio.Codec
                        fiaOut.Language = miAudio.Language
                        fiaOut.LongLanguage = miAudio.LongLanguage
                    End If
                End If

                If ForTV Then
                    If Not String.IsNullOrEmpty(Master.eSettings.TVGeneralFlagLang) AndAlso miAudio.LongLanguage.ToLower = Master.eSettings.TVGeneralFlagLang.ToLower Then fiaOut.HasPreferred = True
                Else
                    If Not String.IsNullOrEmpty(Master.eSettings.MovieGeneralFlagLang) AndAlso miAudio.LongLanguage.ToLower = Master.eSettings.MovieGeneralFlagLang.ToLower Then fiaOut.HasPreferred = True
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return fiaOut
    End Function

    Public Shared Function GetBestVideo(ByVal miFIV As MediaInfo.Fileinfo) As MediaInfo.Video
        '//
        ' Get the highest values from file info
        '\\

        Dim fivOut As New MediaInfo.Video
        Try
            Dim iWidest As Integer = 0
            Dim iWidth As Integer = 0

            'set some defaults to make it easy on ourselves
            fivOut.Width = String.Empty
            fivOut.Height = String.Empty
            fivOut.Aspect = String.Empty
            fivOut.Codec = String.Empty
            fivOut.Duration = String.Empty
            fivOut.Scantype = String.Empty
            fivOut.Language = String.Empty
            'cocotus, 2013/02 Added support for new MediaInfo-fields
            fivOut.Bitrate = String.Empty
            fivOut.MultiViewCount = String.Empty
            fivOut.MultiViewLayout = String.Empty
            fivOut.EncodedSettings = String.Empty
            'cocotus end

            For Each miVideo As MediaInfo.Video In miFIV.StreamDetails.Video
                If Not String.IsNullOrEmpty(miVideo.Width) Then
                    iWidth = Convert.ToInt32(miVideo.Width)
                    If iWidth > iWidest Then
                        iWidest = iWidth
                        fivOut.Width = miVideo.Width
                        fivOut.Height = miVideo.Height
                        fivOut.Aspect = miVideo.Aspect
                        fivOut.Codec = miVideo.Codec
                        fivOut.Duration = miVideo.Duration
                        fivOut.Scantype = miVideo.Scantype
                        fivOut.Language = miVideo.Language

                        'cocotus, 2013/02 Added support for new MediaInfo-fields

                        'MultiViewCount (3D) handling, simply map field
                        fivOut.MultiViewCount = miVideo.MultiViewCount

                        'MultiViewLayout (3D) handling, simply map field
                        fivOut.MultiViewLayout = miVideo.MultiViewLayout

                        'EncodedSettings handling, simply map field
                        fivOut.EncodedSettings = miVideo.EncodedSettings

                        'Bitrate handling, simply map field
                        fivOut.Bitrate = miVideo.Bitrate
                        'cocotus end

                    End If
                End If
            Next

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
        Return fivOut
    End Function

    Public Shared Function GetDimensionsFromVideo(ByVal fiRes As MediaInfo.Video) As String
        '//
        ' Get the dimension values of the video from the information provided by MediaInfo.dll
        '\\

        Dim result As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(fiRes.Width) AndAlso Not String.IsNullOrEmpty(fiRes.Height) AndAlso Not String.IsNullOrEmpty(fiRes.Aspect) Then
                Dim iWidth As Integer = Convert.ToInt32(fiRes.Width)
                Dim iHeight As Integer = Convert.ToInt32(fiRes.Height)
                Dim sinADR As Single = NumUtils.ConvertToSingle(fiRes.Aspect)

                result = String.Format("{0}x{1} ({2})", iWidth, iHeight, sinADR.ToString("0.00"))
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        Return result
    End Function

    Public Shared Function GetEpNfoPath(ByVal EpPath As String) As String
        Dim nPath As String = String.Empty

        If File.Exists(String.Concat(FileUtils.Common.RemoveExtFromPath(EpPath), ".nfo")) Then
            nPath = String.Concat(FileUtils.Common.RemoveExtFromPath(EpPath), ".nfo")
        End If

        Return nPath
    End Function

    Public Shared Function GetIMDBFromNonConf(ByVal sPath As String, ByVal isSingle As Boolean) As NonConf
        Dim tNonConf As New NonConf
        Dim dirPath As String = Directory.GetParent(sPath).FullName
        Dim lFiles As New List(Of String)

        If isSingle Then
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, "*.nfo"))
            Catch
            End Try
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, "*.info"))
            Catch
            End Try
        Else
            Dim fName As String = StringUtils.CleanStackingMarkers(Path.GetFileNameWithoutExtension(sPath)).ToLower
            Dim oName As String = Path.GetFileNameWithoutExtension(sPath)
            fName = If(fName.EndsWith("*"), fName, String.Concat(fName, "*"))
            oName = If(oName.EndsWith("*"), oName, String.Concat(oName, "*"))

            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(fName, ".nfo")))
            Catch
            End Try
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(oName, ".nfo")))
            Catch
            End Try
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(fName, ".info")))
            Catch
            End Try
            Try
                lFiles.AddRange(Directory.GetFiles(dirPath, String.Concat(oName, ".info")))
            Catch
            End Try
        End If

        For Each sFile As String In lFiles
            Using srInfo As New StreamReader(sFile)
                Dim sInfo As String = srInfo.ReadToEnd
                Dim sIMDBID As String = Regex.Match(sInfo, "tt\d\d\d\d\d\d\d", RegexOptions.Multiline Or RegexOptions.Singleline Or RegexOptions.IgnoreCase).ToString

                If Not String.IsNullOrEmpty(sIMDBID) Then
                    tNonConf.IMDBID = sIMDBID
                    'now lets try to see if the rest of the file is a proper nfo
                    If sInfo.ToLower.Contains("</movie>") Then
                        tNonConf.Text = APIXML.XMLToLowerCase(sInfo.Substring(0, sInfo.ToLower.IndexOf("</movie>") + 8))
                    End If
                    Exit For
                Else
                    sIMDBID = Regex.Match(sPath, "tt\d\d\d\d\d\d\d", RegexOptions.Multiline Or RegexOptions.Singleline Or RegexOptions.IgnoreCase).ToString
                    If Not String.IsNullOrEmpty(sIMDBID) Then
                        tNonConf.IMDBID = sIMDBID
                    End If
                End If
            End Using
        Next
        Return tNonConf
    End Function

    Public Shared Function GetNfoPath_Movie(ByVal sPath As String, ByVal isSingle As Boolean) As String
        '//
        ' Get the proper path to NFO
        '\\

        For Each a In FileUtils.GetFilenameList.Movie(sPath, isSingle, Enums.ModType_Movie.NFO)
            If File.Exists(a) Then
                Return a
            End If
        Next

        If Not isSingle Then
            Return String.Empty
        Else
            'return movie path so we can use it for looking for non-conforming nfos
            Return sPath
        End If

    End Function

    Public Shared Function GetNfoPath_MovieSet(ByVal sPath As String) As String
        '//
        ' Get the proper path to NFO
        '\\

        For Each a In FileUtils.GetFilenameList.MovieSet(sPath, Enums.ModType_Movie.NFO)
            If File.Exists(a) Then
                Return a
            End If
        Next

        Return String.Empty

    End Function

    Public Shared Function GetResFromDimensions(ByVal fiRes As MediaInfo.Video) As String
        '//
        ' Get the resolution of the video from the dimensions provided by MediaInfo.dll
        '\\

        Dim resOut As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(fiRes.Width) AndAlso Not String.IsNullOrEmpty(fiRes.Height) AndAlso Not String.IsNullOrEmpty(fiRes.Aspect) Then
                Dim iWidth As Integer = Convert.ToInt32(fiRes.Width)
                Dim iHeight As Integer = Convert.ToInt32(fiRes.Height)
                Dim sinADR As Single = NumUtils.ConvertToSingle(fiRes.Aspect)

                Select Case True
                    Case iWidth < 640
                        resOut = "SD"
                        'exact
                    Case (iWidth = 1920 AndAlso (iHeight = 1080 OrElse iHeight = 800)) OrElse (iWidth = 1440 AndAlso iHeight = 1080) OrElse (iWidth = 1280 AndAlso iHeight = 1080)
                        resOut = "1080"
                    Case (iWidth = 1366 AndAlso iHeight = 768) OrElse (iWidth = 1024 AndAlso iHeight = 768)
                        resOut = "768"
                    Case (iWidth = 960 AndAlso iHeight = 720) OrElse (iWidth = 1280 AndAlso (iHeight = 720 OrElse iHeight = 544))
                        resOut = "720"
                    Case (iWidth = 1024 AndAlso iHeight = 576) OrElse (iWidth = 720 AndAlso iHeight = 576)
                        resOut = "576"
                    Case (iWidth = 720 OrElse iWidth = 960) AndAlso iHeight = 540
                        resOut = "540"
                    Case (iWidth = 852 OrElse iWidth = 720 OrElse iWidth = 704 OrElse iWidth = 640) AndAlso iHeight = 480
                        resOut = "480"
                        'by ADR
                    Case sinADR >= 1.4 AndAlso iWidth = 1920
                        resOut = "1080"
                    Case sinADR >= 1.4 AndAlso iWidth = 1366
                        resOut = "768"
                    Case sinADR >= 1.4 AndAlso iWidth = 1280
                        resOut = "720"
                    Case sinADR >= 1.4 AndAlso iWidth = 1024
                        resOut = "576"
                    Case sinADR >= 1.4 AndAlso iWidth = 960
                        resOut = "540"
                    Case sinADR >= 1.4 AndAlso iWidth = 852
                        resOut = "480"
                        'loose
                    Case iWidth >= 1200 AndAlso iHeight > 768
                        resOut = "1080"
                    Case iWidth >= 1000 AndAlso iHeight > 720
                        resOut = "768"
                    Case iWidth >= 1000 AndAlso iHeight > 500
                        resOut = "720"
                    Case iWidth >= 700 AndAlso iHeight > 540
                        resOut = "576"
                    Case iWidth >= 700 AndAlso iHeight > 480
                        resOut = "540"
                    Case Else
                        resOut = "480"
                End Select
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try

        If Not String.IsNullOrEmpty(resOut) Then
            If String.IsNullOrEmpty(fiRes.Scantype) Then
                Return String.Concat(resOut)
            Else
                Return String.Concat(resOut, If(fiRes.Scantype.ToLower = "progressive", "p", "i"))
            End If
        Else
            Return String.Empty
        End If
    End Function

    Public Shared Function GetShowNfoPath(ByVal ShowPath As String) As String
        Dim nPath As String = String.Empty

        If File.Exists(Path.Combine(ShowPath, "tvshow.nfo")) Then
            nPath = Path.Combine(ShowPath, "tvshow.nfo")
        End If

        Return nPath
    End Function

    Public Shared Function IsConformingEpNfo(ByVal sPath As String) As Boolean
        Dim testSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.EpisodeDetails))
        Dim testEp As New MediaContainers.EpisodeDetails

        Try
            If (Path.GetExtension(sPath) = ".nfo" OrElse Path.GetExtension(sPath) = ".info") AndAlso File.Exists(sPath) Then
                Using xmlSR As StreamReader = New StreamReader(sPath)
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
                        If Not IsNothing(testEp) Then
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
            If Not IsNothing(testSer) Then
                testSer = Nothing
            End If
            If Not IsNothing(testEp) Then
                testEp = Nothing
            End If
            Return False
        End Try
    End Function

    Public Shared Function IsConformingNfo(ByVal sPath As String) As Boolean
        Dim testSer As XmlSerializer = Nothing

        Try
            If (Path.GetExtension(sPath) = ".nfo" OrElse Path.GetExtension(sPath) = ".info") AndAlso File.Exists(sPath) Then
                Using testSR As StreamReader = New StreamReader(sPath)
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
            If Not IsNothing(testSer) Then
                testSer = Nothing
            End If

            Return False
        End Try
    End Function

    Public Shared Function IsConformingShowNfo(ByVal sPath As String) As Boolean
        Dim testSer As XmlSerializer = Nothing

        Try
            If (Path.GetExtension(sPath) = ".nfo" OrElse Path.GetExtension(sPath) = ".info") AndAlso File.Exists(sPath) Then
                Using testSR As StreamReader = New StreamReader(sPath)
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
            If Not IsNothing(testSer) Then
                testSer = Nothing
            End If

            Return False
        End Try
    End Function

    Public Shared Function LoadMovieFromNFO(ByVal sPath As String, ByVal isSingle As Boolean) As MediaContainers.Movie
        '//
        ' Deserialze the NFO to pass all the data to a MediaContainers.Movie
        '\\

        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlMov As New MediaContainers.Movie

        If Not String.IsNullOrEmpty(sPath) Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.Movie))
                        xmlMov = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.Movie)
                        xmlMov = CleanNFO_Movies(xmlMov)
                    End Using
                Else
                    If Not String.IsNullOrEmpty(sPath) Then
                        Dim sReturn As New NonConf
                        sReturn = GetIMDBFromNonConf(sPath, isSingle)
                        xmlMov.IMDBID = sReturn.IMDBID
                        Try
                            If Not String.IsNullOrEmpty(sReturn.Text) Then
                                Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                                    xmlSer = New XmlSerializer(GetType(MediaContainers.Movie))
                                    xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.Movie)
                                    xmlMov.IMDBID = sReturn.IMDBID
                                    xmlMov = CleanNFO_Movies(xmlMov)
                                End Using
                            End If
                        Catch
                        End Try
                    End If
                End If

            Catch
                xmlMov.Clear()
                If Not String.IsNullOrEmpty(sPath) Then

                    'go ahead and rename it now, will still be picked up in getimdbfromnonconf
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNfo(sPath, True)
                    End If

                    Dim sReturn As New NonConf
                    sReturn = GetIMDBFromNonConf(sPath, isSingle)
                    xmlMov.IMDBID = sReturn.IMDBID
                    Try
                        If Not String.IsNullOrEmpty(sReturn.Text) Then
                            Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                                xmlSer = New XmlSerializer(GetType(MediaContainers.Movie))
                                xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.Movie)
                                xmlMov.IMDBID = sReturn.IMDBID
                                xmlMov = CleanNFO_Movies(xmlMov)
                            End Using
                        End If
                    Catch
                    End Try
                End If
            End Try

            If Not IsNothing(xmlSer) Then
                xmlSer = Nothing
            End If
        End If

        Return xmlMov
    End Function

    Public Shared Function LoadMovieSetFromNFO(ByVal sPath As String) As MediaContainers.MovieSet
        '//
        ' Deserialze the NFO to pass all the data to a MediaContainers.Movie
        '\\

        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlMovSet As New MediaContainers.MovieSet

        If Not String.IsNullOrEmpty(sPath) Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.MovieSet))
                        xmlMovSet = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.MovieSet)
                        xmlMovSet.Plot = xmlMovSet.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                    End Using
                    'Else
                    '    If Not String.IsNullOrEmpty(sPath) Then
                    '        Dim sReturn As New NonConf
                    '        sReturn = GetIMDBFromNonConf(sPath, isSingle)
                    '        xmlMov.IMDBID = sReturn.IMDBID
                    '        Try
                    '            If Not String.IsNullOrEmpty(sReturn.Text) Then
                    '                Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                    '                    xmlSer = New XmlSerializer(GetType(MediaContainers.Movie))
                    '                    xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.Movie)
                    '                    xmlMov.Genre = Strings.Join(xmlMov.LGenre.ToArray, " / ")
                    '                    xmlMov.Outline = xmlMov.Outline.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                    '                    xmlMovSet.Plot = xmlMovSet.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                    '                    xmlMov.IMDBID = sReturn.IMDBID
                    '                End Using
                    '            End If
                    '        Catch
                    '        End Try
                    '    End If
                End If

            Catch
                xmlMovSet.Clear()
                'If Not String.IsNullOrEmpty(sPath) Then

                '    'go ahead and rename it now, will still be picked up in getimdbfromnonconf
                '    If Not Master.eSettings.GeneralOverwriteNfo Then
                '        RenameNonConfNfo(sPath, True)
                '    End If

                '    Dim sReturn As New NonConf
                '    sReturn = GetIMDBFromNonConf(sPath, isSingle)
                '    xmlMov.IMDBID = sReturn.IMDBID
                '    Try
                '        If Not String.IsNullOrEmpty(sReturn.Text) Then
                '            Using xmlSTR As StringReader = New StringReader(sReturn.Text)
                '                xmlSer = New XmlSerializer(GetType(MediaContainers.Movie))
                '                xmlMov = DirectCast(xmlSer.Deserialize(xmlSTR), MediaContainers.Movie)
                '                xmlMov.Genre = Strings.Join(xmlMov.LGenre.ToArray, " / ")
                '                xmlMov.Outline = xmlMov.Outline.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                '                xmlMovSet.Plot = xmlMovSet.Plot.Replace(vbCrLf, vbLf).Replace(vbLf, vbCrLf)
                '                xmlMov.IMDBID = sReturn.IMDBID
                '            End Using
                '        End If
                '    Catch
                '    End Try
                'End If
            End Try

            If Not IsNothing(xmlSer) Then
                xmlSer = Nothing
            End If
        End If

        Return xmlMovSet
    End Function

    Public Shared Function LoadTVEpFromNFO(ByVal sPath As String, ByVal SeasonNumber As Integer, ByVal EpisodeNumber As Integer) As MediaContainers.EpisodeDetails
        Dim xmlSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.EpisodeDetails))
        Dim xmlEp As New MediaContainers.EpisodeDetails

        If Not String.IsNullOrEmpty(sPath) AndAlso SeasonNumber >= -1 Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    'better way to read multi-root xml??
                    Using xmlSR As StreamReader = New StreamReader(sPath)
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
                                    If xmlEp.Episode = EpisodeNumber AndAlso xmlEp.Season = SeasonNumber Then
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
                        RenameEpNonConfNfo(sPath, True)
                    End If
                End If

            Catch
                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameEpNonConfNfo(sPath, True)
                End If
            End Try
        End If

        Return New MediaContainers.EpisodeDetails
    End Function

    Public Shared Function LoadTVEpFromNFO(ByVal sPath As String, ByVal SeasonNumber As Integer, ByVal Aired As String) As MediaContainers.EpisodeDetails
        Dim xmlSer As XmlSerializer = New XmlSerializer(GetType(MediaContainers.EpisodeDetails))
        Dim xmlEp As New MediaContainers.EpisodeDetails

        If Not String.IsNullOrEmpty(sPath) AndAlso SeasonNumber >= -1 Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    'better way to read multi-root xml??
                    Using xmlSR As StreamReader = New StreamReader(sPath)
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
                                    If xmlEp.Aired = Aired AndAlso xmlEp.Season = SeasonNumber Then
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
                        RenameEpNonConfNfo(sPath, True)
                    End If
                End If

            Catch
                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameEpNonConfNfo(sPath, True)
                End If
            End Try
        End If

        Return New MediaContainers.EpisodeDetails
    End Function

    Public Shared Function LoadTVShowFromNFO(ByVal sPath As String) As MediaContainers.TVShow
        Dim xmlSer As XmlSerializer = Nothing
        Dim xmlShow As New MediaContainers.TVShow

        If Not String.IsNullOrEmpty(sPath) Then
            Try
                If File.Exists(sPath) AndAlso Path.GetExtension(sPath).ToLower = ".nfo" Then
                    Using xmlSR As StreamReader = New StreamReader(sPath)
                        xmlSer = New XmlSerializer(GetType(MediaContainers.TVShow))
                        xmlShow = DirectCast(xmlSer.Deserialize(xmlSR), MediaContainers.TVShow)
                        xmlShow.Genre = Strings.Join(xmlShow.LGenre.ToArray, " / ")
                    End Using
                Else
                    'not really anything else to do with non-conforming nfos aside from rename them
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameShowNonConfNfo(sPath)
                    End If
                End If

            Catch
                'not really anything else to do with non-conforming nfos aside from rename them
                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameShowNonConfNfo(sPath)
                End If
            End Try

            Try
                Dim params As New List(Of Object)(New Object() {xmlShow})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFORead_TVShow, params, doContinue, False)

            Catch ex As Exception
            End Try

            'Boxee support
            If Master.eSettings.TVUseBoxee Then
                If xmlShow.BoxeeIDSpecified() Then
                    xmlShow.ID = xmlShow.BoxeeTvDb
                    xmlShow.BlankBoxeeId()
                End If
            End If

            If Not IsNothing(xmlSer) Then
                xmlSer = Nothing
            End If
        End If

        Return xmlShow
    End Function

    Public Shared Sub SaveMovieToNFO(ByRef movieToSave As Structures.DBMovie)
        '//
        ' Serialize MediaContainers.Movie to an NFO
        '\\
        Try
            Try
                Dim params As New List(Of Object)(New Object() {movieToSave})
                Dim doContinue As Boolean = True
                ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_Movie, params, doContinue, False)
                If Not doContinue Then Return
            Catch ex As Exception
            End Try

            If Not String.IsNullOrEmpty(movieToSave.Filename) Then
                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.Movie))
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                'YAMJ support
                If Master.eSettings.TVUseYAMJ AndAlso Master.eSettings.MovieNFOYAMJ Then
                    If movieToSave.Movie.TMDBIDSpecified Then
                        movieToSave.Movie.TMDBID = Nothing
                    End If
                    If movieToSave.Movie.IDMovieDBSpecified Then
                        movieToSave.Movie.IDMovieDB = Nothing
                    End If
                End If

                For Each a In FileUtils.GetFilenameList.Movie(movieToSave.Filename, movieToSave.IsSingle, Enums.ModType_Movie.NFO)
                    If Not Master.eSettings.GeneralOverwriteNfo Then
                        RenameNonConfNfo(a, False)
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
                            movieToSave.NfoPath = a
                            xmlSer.Serialize(xmlSW, movieToSave.Movie)
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Sub SaveMovieSetToNFO(ByRef moviesetToSave As Structures.DBMovieSet)
        '//
        ' Serialize MediaContainers.MovieSet to an NFO
        '\\
        Try
            'Try
            '    Dim params As New List(Of Object)(New Object() {moviesetToSave})
            '    Dim doContinue As Boolean = True
            '    ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnMovieSetNFOSave, params, doContinue, False)
            '    If Not doContinue Then Return
            'Catch ex As Exception
            'End Try

            If Not String.IsNullOrEmpty(moviesetToSave.MovieSet.Title) Then
                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.MovieSet))
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                For Each a In FileUtils.GetFilenameList.MovieSet(moviesetToSave.MovieSet.Title, Enums.ModType_Movie.NFO)
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
                            moviesetToSave.NfoPath = a
                            xmlSer.Serialize(xmlSW, moviesetToSave.MovieSet)
                        End Using
                        If doesExist And fAttWritable Then File.SetAttributes(a, fAtt)
                    End If
                Next
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Sub SaveSingleNFOItem(ByVal sPath As String, ByVal strToWrite As String, ByVal strNode As String)
        '//
        ' Save just one item of an NFO file
        '\\

        Try
            Dim xmlDoc As New XmlDocument()
            'use streamreader to open NFO so we don't get any access violations when trying to save
            Dim xmlSR As New StreamReader(sPath)
            'copy NFO to string
            Dim xmlString As String = xmlSR.ReadToEnd
            'close the streamreader... we're done with it
            xmlSR.Close()
            xmlSR = Nothing

            xmlDoc.LoadXml(xmlString)
            Dim xNode As XmlNode = xmlDoc.SelectSingleNode(strNode)
            xNode.InnerText = strToWrite
            xmlDoc.Save(sPath)

            xmlDoc = Nothing
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public NotInheritable Class Utf8StringWriter
        Inherits StringWriter
        Public Overloads Overrides ReadOnly Property Encoding() As Encoding
            Get
                Return Encoding.UTF8
            End Get
        End Property
    End Class

    Public Shared Sub SaveTVEpToNFO(ByRef tvEpToSave As Structures.DBTV)
        Try

            If Not String.IsNullOrEmpty(tvEpToSave.Filename) Then
                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.EpisodeDetails))

                Dim tPath As String = String.Empty
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True
                Dim EpList As New List(Of MediaContainers.EpisodeDetails)
                Dim sBuilder As New StringBuilder

                Dim tmpName As String = Path.GetFileNameWithoutExtension(tvEpToSave.Filename)
                tPath = String.Concat(Path.Combine(Directory.GetParent(tvEpToSave.Filename).FullName, tmpName), ".nfo")

                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameEpNonConfNfo(tPath, False)
                End If

                doesExist = File.Exists(tPath)
                If Not doesExist OrElse (Not CBool(File.GetAttributes(tPath) And FileAttributes.ReadOnly)) Then

                    If doesExist Then
                        fAtt = File.GetAttributes(tPath)
                        Try
                            File.SetAttributes(tPath, FileAttributes.Normal)
                        Catch ex As Exception
                            fAttWritable = False
                        End Try
                    End If

                    Using SQLCommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                        SQLCommand.CommandText = "SELECT ID FROM TVEps WHERE ID <> (?) AND TVEpPathID IN (SELECT ID FROM TVEpPaths WHERE TVEpPath = (?)) ORDER BY Episode"
                        Dim parID As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parID", DbType.Int64, 0, "ID")
                        Dim parTVEpPath As SQLite.SQLiteParameter = SQLCommand.Parameters.Add("parTVEpPath", DbType.String, 0, "TVEpPath")

                        parID.Value = tvEpToSave.EpID
                        parTVEpPath.Value = tvEpToSave.Filename

                        Using SQLreader As SQLite.SQLiteDataReader = SQLCommand.ExecuteReader
                            While SQLreader.Read
                                EpList.Add(Master.DB.LoadTVEpFromDB(Convert.ToInt64(SQLreader("ID")), False).TVEp)
                            End While
                        End Using

                        EpList.Add(tvEpToSave.TVEp)

                        Dim NS As New XmlSerializerNamespaces
                        NS.Add(String.Empty, String.Empty)

                        For Each tvEp As MediaContainers.EpisodeDetails In EpList.OrderBy(Function(s) s.Season)
                            Using xmlSW As New Utf8StringWriter
                                xmlSer.Serialize(xmlSW, tvEp, NS)
                                If sBuilder.Length > 0 Then
                                    sBuilder.Append(vbNewLine)
                                    xmlSW.GetStringBuilder.Remove(0, xmlSW.GetStringBuilder.ToString.IndexOf(vbNewLine) + 1)
                                End If
                                sBuilder.Append(xmlSW.ToString)
                            End Using
                        Next

                        tvEpToSave.EpNfoPath = tPath

                        If sBuilder.Length > 0 Then
                            Using fSW As New StreamWriter(tPath)
                                fSW.Write(sBuilder.ToString)
                            End Using
                        End If
                    End Using
                    If doesExist And fAttWritable Then File.SetAttributes(tPath, fAtt)
                End If
            End If

        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Sub SaveTVShowToNFO(ByRef tvShowToSave As Structures.DBTV)
        '//
        ' Serialize MediaContainers.TVShow to an NFO
        '\\

        Try
            Dim params As New List(Of Object)(New Object() {tvShowToSave})
            Dim doContinue As Boolean = True
            ModulesManager.Instance.RunGeneric(Enums.ModuleEventType.OnNFOSave_TVShow, params, doContinue, False)
            If Not doContinue Then Return
        Catch ex As Exception
        End Try

        Try
            If Not String.IsNullOrEmpty(tvShowToSave.ShowPath) Then
                Dim xmlSer As New XmlSerializer(GetType(MediaContainers.TVShow))

                Dim tPath As String = String.Empty
                Dim doesExist As Boolean = False
                Dim fAtt As New FileAttributes
                Dim fAttWritable As Boolean = True

                tPath = Path.Combine(tvShowToSave.ShowPath, "tvshow.nfo")

                If Not Master.eSettings.GeneralOverwriteNfo Then
                    RenameShowNonConfNfo(tPath)
                End If

                'Boxee support
                If Master.eSettings.TVUseBoxee Then
                    If tvShowToSave.TVShow.IDSpecified() Then
                        tvShowToSave.TVShow.BoxeeTvDb = tvShowToSave.TVShow.ID
                        tvShowToSave.TVShow.BlankId()
                    End If
                End If

                doesExist = File.Exists(tPath)
                If Not doesExist OrElse (Not CBool(File.GetAttributes(tPath) And FileAttributes.ReadOnly)) Then

                    If doesExist Then
                        fAtt = File.GetAttributes(tPath)
                        Try
                            File.SetAttributes(tPath, FileAttributes.Normal)
                        Catch ex As Exception
                            fAttWritable = False
                        End Try
                    End If

                    Using xmlSW As New StreamWriter(tPath)
                        tvShowToSave.ShowNfoPath = tPath
                        xmlSer.Serialize(xmlSW, tvShowToSave.TVShow)
                    End Using

                    If doesExist And fAttWritable Then File.SetAttributes(tPath, fAtt)
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Shared Sub RenameEpNonConfNfo(ByVal sPath As String, ByVal isChecked As Boolean)
        'test if current nfo is non-conforming... rename per setting

        Try
            If File.Exists(sPath) AndAlso Not IsConformingEpNfo(sPath) Then
                RenameToInfo(sPath)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Shared Sub RenameNonConfNfo(ByVal sPath As String, ByVal isChecked As Boolean)
        'test if current nfo is non-conforming... rename per setting

        Try
            If isChecked OrElse Not IsConformingNfo(sPath) Then
                If isChecked OrElse File.Exists(sPath) Then
                    RenameToInfo(sPath)
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Shared Sub RenameShowNonConfNfo(ByVal sPath As String)
        'test if current nfo is non-conforming... rename per setting

        Try
            If File.Exists(sPath) AndAlso Not IsConformingShowNfo(sPath) Then
                RenameToInfo(sPath)
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Private Shared Sub RenameToInfo(ByVal sPath As String)
        Try
            Dim i As Integer = 1
            Dim strNewName As String = String.Concat(FileUtils.Common.RemoveExtFromPath(sPath), ".info")
            'in case there is already a .info file
            If File.Exists(strNewName) Then
                Do
                    strNewName = String.Format("{0}({1}).info", FileUtils.Common.RemoveExtFromPath(sPath), i)
                    i += 1
                Loop While File.Exists(strNewName)
                strNewName = String.Format("{0}({1}).info", FileUtils.Common.RemoveExtFromPath(sPath), i)
            End If
            My.Computer.FileSystem.RenameFile(sPath, Path.GetFileName(strNewName))
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

    Public Shared Sub LoadTVEpDuration(ByVal _TVEpDB As Structures.DBTV)
        Try
            Dim tRuntime As String = String.Empty
            If Master.eSettings.TVScraperUseMDDuration Then
                If Not IsNothing(_TVEpDB.TVEp.FileInfo.StreamDetails) AndAlso _TVEpDB.TVEp.FileInfo.StreamDetails.Video.Count > 0 Then
                    Dim cTotal As String = String.Empty
                    For Each tVid As MediaInfo.Video In _TVEpDB.TVEp.FileInfo.StreamDetails.Video
                        cTotal = cTotal + tVid.Duration
                    Next
                    tRuntime = MediaInfo.FormatDuration(MediaInfo.DurationToSeconds(cTotal, True), Master.eSettings.TVScraperDurationRuntimeFormat)
                End If
            End If

            If String.IsNullOrEmpty(tRuntime) Then
                If (String.IsNullOrEmpty(_TVEpDB.TVEp.Runtime) OrElse Not Master.eSettings.TVLockEpisodeRuntime) AndAlso Not String.IsNullOrEmpty(_TVEpDB.TVShow.Runtime) AndAlso Master.eSettings.TVScraperUseSRuntimeForEp Then
                    _TVEpDB.TVEp.Runtime = _TVEpDB.TVShow.Runtime
                End If
            Else
                If (String.IsNullOrEmpty(_TVEpDB.TVEp.Runtime) OrElse Not Master.eSettings.TVLockEpisodeRuntime) AndAlso Master.eSettings.TVScraperUseMDDuration Then
                    _TVEpDB.TVEp.Runtime = tRuntime
                End If
            End If
        Catch ex As Exception
            logger.Error(New StackFrame().GetMethod().Name, ex)
        End Try
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Public Class NonConf

#Region "Fields"

        Private _imdbid As String
        Private _text As String

#End Region 'Fields

#Region "Constructors"

        Public Sub New()
            Me.Clear()
        End Sub

#End Region 'Constructors

#Region "Properties"

        Public Property IMDBID() As String
            Get
                Return Me._imdbid
            End Get
            Set(ByVal value As String)
                Me._imdbid = value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return Me._text
            End Get
            Set(ByVal value As String)
                Me._text = value
            End Set
        End Property

#End Region 'Properties

#Region "Methods"

        Public Sub Clear()
            Me._imdbid = String.Empty
            Me._text = String.Empty
        End Sub

#End Region 'Methods

    End Class

#End Region 'Nested Types

End Class