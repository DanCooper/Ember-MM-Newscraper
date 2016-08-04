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

Imports System.Text.RegularExpressions
Imports NLog
Imports System.Windows.Forms
Imports System.Drawing

Public Class dlgMovieDataScraperPreview

#Region "Fields"

    Shared logger As Logger = NLog.LogManager.GetCurrentClassLogger()

    Private lvwActorSorter As ListViewColumnSorter
    Dim _ScrapedList As New List(Of MediaContainers.Movie)

#End Region

#Region "Methods/Functions"
    ''' <summary>
    ''' Modified Constructor: Since we change list of scrapedresults (filled by datascrapers of Ember) we need reference here and change it directly!
    ''' </summary>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation: 
    ''' </remarks>
    Public Sub New(ByRef ScrapedList As List(Of MediaContainers.Movie))
        InitializeComponent()
        'since its ByRef any changes we do in this class on _ScrapedList will directly alter ScrapedList as well (which is what we want)
        _ScrapedList = ScrapedList
        'set labels according to language setting
        SetUp()
    End Sub

    ''' <summary>
    ''' Load strings for labels and other controls
    ''' </summary>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation
    ''' </remarks>
    Private Sub SetUp()
        Cancel_Button.Text = Master.eLang.GetString(167, "Cancel")
        OK_Button.Text = Master.eLang.GetString(179, "OK")
        colNameIMDB.Text = Master.eLang.GetString(232, "Name")
        colRoleIMDB.Text = Master.eLang.GetString(233, "Role")
        colThumbIMDB.Text = Master.eLang.GetString(234, "Thumb")
        lblActors.Text = String.Concat(Master.eLang.GetString(231, "Actors"), ":")
        lblCertifications.Text = String.Concat(Master.eLang.GetString(56, "Certifications"), ":")
        lblCountries.Text = String.Concat(Master.eLang.GetString(237, "Countries"), ":")
        lblCredits.Text = Master.eLang.GetString(228, "Credits:")
        lblDirectors.Text = String.Concat(Master.eLang.GetString(940, "Directors"), ":")
        lblGenres.Text = Master.eLang.GetString(51, "Genre(s):")
        lblMPAA.Text = Master.eLang.GetString(235, "MPAA Rating:")
        lblOriginalTitle.Text = String.Concat(Master.eLang.GetString(302, "Original Title"), ":")
        lblOutline.Text = Master.eLang.GetString(242, "Plot Outline:")
        lblPlot.Text = Master.eLang.GetString(241, "Plot:")
        lblRating.Text = Master.eLang.GetString(245, "Rating:")
        lblReleaseDate.Text = Master.eLang.GetString(236, "Release Date:")
        lblRuntime.Text = Master.eLang.GetString(238, "Runtime:")
        lblStudios.Text = String.Concat(Master.eLang.GetString(226, "Studios"), ":")
        lblTagline.Text = Master.eLang.GetString(243, "Tagline:")
        lblTitle.Text = Master.eLang.GetString(246, "Title:")
        lblTop250.Text = Master.eLang.GetString(240, "Top 250:")
        lblTopDetails.Text = Master.eLang.GetString(1254, "Only data in selected tabs will be used!")
        lblTopTitle.Text = Master.eLang.GetString(1253, "Scraperresults")
        lblTrailerURL.Text = String.Concat(Master.eLang.GetString(227, "Trailer URL"), ":")
        lblVotes.Text = Master.eLang.GetString(244, "Votes:")
        lblYear.Text = Master.eLang.GetString(49, "Year:")
    End Sub

    ''' <summary>
    ''' Fill control of each tab which scraperresult data, display only tabs if there are scraped data
    ''' </summary>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation: Display all scraped results from datascrapers (IMDB/TMDB/OFDB/Moviepilot) on this page, but only if data is not empty (else remove tab for that information)
    ''' </remarks>
    Private Sub FillInfo()
        Dim isActivatedIMDB As Boolean = False
        Dim isActivatedTMDB As Boolean = False
        Dim isActivatedOFDB As Boolean = False
        Dim isActivatedMoviepilot As Boolean = False

        'scraperlist needs to be reversed to make sure the results of favorite scrapers are highlighted by default
        If _ScrapedList.Count > 1 Then
            _ScrapedList.Reverse()
        End If

        For Each scraperresult In _ScrapedList
            With Me
                If scraperresult.Scrapersource.ToUpper = "IMDB" Then
                    isActivatedIMDB = True
                    'Title
                    If scraperresult.TitleSpecified Then
                        .txtTitleIMDB.Text = scraperresult.Title
                        tbTitel.SelectedTab = tbTitelIMDB
                    Else
                        tbTitel.TabPages.Remove(tbTitelIMDB)
                    End If

                    'OriginalTitle
                    If scraperresult.OriginalTitleSpecified Then
                        .txtOriginalTitleIMDB.Text = scraperresult.OriginalTitle
                        tbOriginalTitel.SelectedTab = tbOriginalTitelIMDB
                    Else
                        tbOriginalTitel.TabPages.Remove(tbOriginalTitelIMDB)
                    End If

                    'Cert
                    If scraperresult.CertificationsSpecified Then
                        .txtCertIMDB.Text = String.Join(" / ", scraperresult.Certifications.ToArray)
                        tbCert.SelectedTab = tbCertIMDB
                    Else
                        tbCert.TabPages.Remove(tbCertIMDB)
                    End If

                    'Country
                    If scraperresult.CountriesSpecified Then
                        .txtCountryIMDB.Text = String.Join(" / ", scraperresult.Countries.ToArray)
                        tbCountry.SelectedTab = tbCountryIMDB
                    Else
                        tbCountry.TabPages.Remove(tbCountryIMDB)
                    End If

                    'Credits
                    If scraperresult.CreditsSpecified Then
                        .txtCreditsIMDB.Text = String.Join(" / ", scraperresult.Credits.ToArray)
                        tbCredits.SelectedTab = tbCreditsIMDB
                    Else
                        tbCredits.TabPages.Remove(tbCreditsIMDB)
                    End If

                    'Director
                    If scraperresult.DirectorsSpecified Then
                        .txtDirectorIMDB.Text = String.Join(" / ", scraperresult.Directors.ToArray)
                        tbDirector.SelectedTab = tbDirectorIMDB
                    Else
                        tbDirector.TabPages.Remove(tbDirectorIMDB)
                    End If

                    'Genre
                    If scraperresult.GenresSpecified Then
                        .txtGenreIMDB.Text = String.Join(" / ", scraperresult.Genres.ToArray)
                        tbGenre.SelectedTab = tbGenreIMDB
                    Else
                        tbGenre.TabPages.Remove(tbGenreIMDB)
                    End If

                    'MPAA
                    If scraperresult.MPAASpecified Then
                        .txtMPAAIMDB.Text = scraperresult.MPAA
                        tbMPAA.SelectedTab = tbMPAAIMDB
                    Else
                        tbMPAA.TabPages.Remove(tbMPAAIMDB)
                    End If

                    'Outline
                    If scraperresult.OutlineSpecified Then
                        .txtOutlineIMDB.Text = scraperresult.Outline
                        tbOutline.SelectedTab = tbOutlineIMDB
                    Else
                        tbOutline.TabPages.Remove(tbOutlineIMDB)
                    End If

                    'Plot
                    If scraperresult.PlotSpecified Then
                        .txtPlotIMDB.Text = scraperresult.Plot
                        tbPlot.SelectedTab = tbPlotIMDB
                    Else
                        tbPlot.TabPages.Remove(tbPlotIMDB)
                    End If

                    'Rating
                    If scraperresult.RatingSpecified Then
                        .txtRatingIMDB.Text = scraperresult.Rating
                        tbRating.SelectedTab = tbRatingIMDB
                    Else
                        tbRating.TabPages.Remove(tbRatingIMDB)
                    End If

                    'ReleaseDate
                    If scraperresult.ReleaseDateSpecified Then
                        .txtReleaseDateIMDB.Text = scraperresult.ReleaseDate
                        tbReleaseYear.SelectedTab = tbReleaseYearIMDB
                    Else
                        tbReleaseYear.TabPages.Remove(tbReleaseYearIMDB)
                    End If

                    'Runtime
                    If scraperresult.RuntimeSpecified Then
                        .txtRuntimeIMDB.Text = scraperresult.Runtime
                        tbRuntime.SelectedTab = tbRuntimeIMDB
                    Else
                        tbRuntime.TabPages.Remove(tbRuntimeIMDB)
                    End If

                    'Studio
                    If scraperresult.StudiosSpecified Then
                        .txtStudioIMDB.Text = String.Join(" / ", scraperresult.Studios.ToArray)
                        tbStudio.SelectedTab = tbStudioIMDB
                    Else
                        tbStudio.TabPages.Remove(tbStudioIMDB)
                    End If

                    'Tagline
                    If scraperresult.TaglineSpecified Then
                        .txtTaglineIMDB.Text = scraperresult.Tagline
                        tbTagline.SelectedTab = tbTaglineIMDB
                    Else
                        tbTagline.TabPages.Remove(tbTaglineIMDB)
                    End If

                    'Top250
                    If scraperresult.Top250Specified Then
                        .txtTOP250IMDB.Text = scraperresult.Top250.ToString
                        tbTOP250.SelectedTab = tbTOP250IMDB
                    Else
                        tbTOP250.TabPages.Remove(tbTOP250IMDB)
                    End If

                    'Trailer
                    If scraperresult.TrailerSpecified Then
                        .txtTrailerIMDB.Text = scraperresult.Trailer
                        tbTrailer.SelectedTab = tbTrailerIMDB
                    Else
                        tbTrailer.TabPages.Remove(tbTrailerIMDB)
                    End If

                    'Votes
                    If scraperresult.VotesSpecified Then
                        .txtVotesIMDB.Text = scraperresult.Votes
                        tbVotes.SelectedTab = tbVotesIMDB
                    Else
                        tbVotes.TabPages.Remove(tbVotesIMDB)
                    End If

                    'Year
                    If scraperresult.YearSpecified Then
                        .txtYearIMDB.Text = scraperresult.Year
                        tbYear.SelectedTab = tbYearIMDB
                    Else
                        tbYear.TabPages.Remove(tbYearIMDB)
                    End If

                    'Actors
                    Dim lvItem As ListViewItem
                    .lvActorsIMDB.Items.Clear()
                    If scraperresult.Actors.Count > 0 Then
                        For Each imdbAct As MediaContainers.Person In scraperresult.Actors
                            lvItem = .lvActorsIMDB.Items.Add(imdbAct.Name)
                            lvItem.SubItems.Add(imdbAct.Role)
                            lvItem.SubItems.Add(imdbAct.URLOriginal)
                        Next
                        tbActors.SelectedTab = tbActorsIMDB
                    Else
                        tbActors.TabPages.Remove(tbActorsIMDB)
                    End If

                ElseIf scraperresult.Scrapersource.ToUpper = "TMDB" Then
                    isActivatedTMDB = True
                    'Title
                    If scraperresult.TitleSpecified Then
                        .txtTitleTMDB.Text = scraperresult.Title
                        tbTitel.SelectedTab = tbTitelTMDB
                    Else
                        tbTitel.TabPages.Remove(tbTitelTMDB)
                    End If

                    'OriginalTitle
                    If scraperresult.OriginalTitleSpecified Then
                        .txtOriginalTitleTMDB.Text = scraperresult.OriginalTitle
                        tbOriginalTitel.SelectedTab = tbOriginalTitelTMDB
                    Else
                        tbOriginalTitel.TabPages.Remove(tbOriginalTitelTMDB)
                    End If

                    'Cert
                    If scraperresult.CertificationsSpecified Then
                        .txtCertTMDB.Text = String.Join(" / ", scraperresult.Certifications.ToArray)
                        tbCert.SelectedTab = tbCertTMDB
                    Else
                        tbCert.TabPages.Remove(tbCertTMDB)
                    End If

                    'Country
                    If scraperresult.CountriesSpecified Then
                        .txtCountryTMDB.Text = String.Join(" / ", scraperresult.Countries.ToArray)
                        tbCountry.SelectedTab = tbCountryTMDB
                    Else
                        tbCountry.TabPages.Remove(tbCountryTMDB)
                    End If

                    'Credits
                    If scraperresult.CreditsSpecified Then
                        .txtCreditsTMDB.Text = String.Join(" / ", scraperresult.Credits.ToArray)
                        tbCredits.SelectedTab = tbCreditsTMDB
                    Else
                        tbCredits.TabPages.Remove(tbCreditsTMDB)
                    End If

                    'Director
                    If scraperresult.DirectorsSpecified Then
                        .txtDirectorTMDB.Text = String.Join(" / ", scraperresult.Directors.ToArray)
                        tbDirector.SelectedTab = tbDirectorTMDB
                    Else
                        tbDirector.TabPages.Remove(tbDirectorTMDB)
                    End If

                    'Genre
                    If scraperresult.GenresSpecified Then
                        .txtGenreTMDB.Text = String.Join(" / ", scraperresult.Genres.ToArray)
                        tbGenre.SelectedTab = tbGenreTMDB
                    Else
                        tbGenre.TabPages.Remove(tbGenreTMDB)
                    End If

                    'MPAA
                    If scraperresult.MPAASpecified Then
                        .txtMPAATMDB.Text = scraperresult.MPAA
                        tbMPAA.SelectedTab = tbMPAATMDB
                    Else
                        tbMPAA.TabPages.Remove(tbMPAATMDB)
                    End If

                    'Outline
                    If scraperresult.OutlineSpecified Then
                        .txtOutlineTMDB.Text = scraperresult.Outline
                        tbOutline.SelectedTab = tbOutlineTMDB
                    Else
                        tbOutline.TabPages.Remove(tbOutlineTMDB)
                    End If

                    'Plot
                    If scraperresult.PlotSpecified Then
                        .txtPlotTMDB.Text = scraperresult.Plot
                        tbPlot.SelectedTab = tbPlotTMDB
                    Else
                        tbPlot.TabPages.Remove(tbPlotTMDB)
                    End If

                    'Rating
                    If scraperresult.RatingSpecified Then
                        .txtRatingTMDB.Text = scraperresult.Rating
                        tbRating.SelectedTab = tbRatingTMDB
                    Else
                        tbRating.TabPages.Remove(tbRatingTMDB)
                    End If

                    'ReleaseDate
                    If scraperresult.ReleaseDateSpecified Then
                        .txtReleaseDateTMDB.Text = scraperresult.ReleaseDate
                        tbReleaseYear.SelectedTab = tbReleaseYearTMDB
                    Else
                        tbReleaseYear.TabPages.Remove(tbReleaseYearTMDB)
                    End If

                    'Runtime
                    If scraperresult.RuntimeSpecified Then
                        .txtRuntimeTMDB.Text = scraperresult.Runtime
                        tbRuntime.SelectedTab = tbRuntimeTMDB
                    Else
                        tbRuntime.TabPages.Remove(tbRuntimeTMDB)
                    End If

                    'Studio
                    If scraperresult.StudiosSpecified Then
                        .txtStudioTMDB.Text = String.Join(" / ", scraperresult.Studios.ToArray)
                        tbStudio.SelectedTab = tbStudioTMDB
                    Else
                        tbStudio.TabPages.Remove(tbStudioTMDB)
                    End If

                    'Tagline
                    If scraperresult.TaglineSpecified Then
                        .txtTaglineTMDB.Text = scraperresult.Tagline
                        tbTagline.SelectedTab = tbTaglineTMDB
                    Else
                        tbTagline.TabPages.Remove(tbTaglineTMDB)
                    End If

                    'Top250
                    If scraperresult.Top250Specified Then
                        .txtTOP250TMDB.Text = scraperresult.Top250.ToString
                        tbTOP250.SelectedTab = tbTOP250TMDB
                    Else
                        tbTOP250.TabPages.Remove(tbTOP250TMDB)
                    End If

                    'Trailer
                    If scraperresult.TrailerSpecified Then
                        .txtTrailerTMDB.Text = scraperresult.Trailer
                        tbTrailer.SelectedTab = tbTrailerTMDB
                    Else
                        tbTrailer.TabPages.Remove(tbTrailerTMDB)
                    End If

                    'Votes
                    If scraperresult.VotesSpecified Then
                        .txtVotesTMDB.Text = scraperresult.Votes
                        tbVotes.SelectedTab = tbVotesTMDB
                    Else
                        tbVotes.TabPages.Remove(tbVotesTMDB)
                    End If

                    'Year
                    If scraperresult.YearSpecified Then
                        .txtYearTMDB.Text = scraperresult.Year
                        tbYear.SelectedTab = tbYearTMDB
                    Else
                        tbYear.TabPages.Remove(tbYearTMDB)
                    End If

                    'Actors
                    Dim lvItem As ListViewItem
                    .lvActorsTMDB.Items.Clear()
                    If scraperresult.Actors.Count > 0 Then
                        For Each TMDBAct As MediaContainers.Person In scraperresult.Actors
                            lvItem = .lvActorsTMDB.Items.Add(TMDBAct.Name)
                            lvItem.SubItems.Add(TMDBAct.Role)
                            lvItem.SubItems.Add(TMDBAct.URLOriginal)
                        Next
                        tbActors.SelectedTab = tbActorsTMDB
                    Else
                        tbActors.TabPages.Remove(tbActorsTMDB)
                    End If
                ElseIf scraperresult.Scrapersource.ToUpper = "OFDB" Then
                    isActivatedOFDB = True
                    'Title
                    If scraperresult.TitleSpecified Then
                        .txtTitleOFDB.Text = scraperresult.Title
                        tbTitel.SelectedTab = tbTitelOFDB
                    Else
                        tbTitel.TabPages.Remove(tbTitelOFDB)
                    End If

                    'OriginalTitle
                    If scraperresult.OriginalTitleSpecified Then
                        .txtOriginalTitleOFDB.Text = scraperresult.OriginalTitle
                        tbOriginalTitel.SelectedTab = tbOriginalTitelOFDB
                    Else
                        tbOriginalTitel.TabPages.Remove(tbOriginalTitelOFDB)
                    End If

                    'Cert
                    If scraperresult.CertificationsSpecified Then
                        .txtCertOFDB.Text = String.Join(" / ", scraperresult.Certifications.ToArray)
                        tbCert.SelectedTab = tbCertOFDB
                    Else
                        tbCert.TabPages.Remove(tbCertOFDB)
                    End If

                    'Country
                    If scraperresult.CountriesSpecified Then
                        .txtCountryOFDB.Text = String.Join(" / ", scraperresult.Countries.ToArray)
                        tbCountry.SelectedTab = tbCountryOFDB
                    Else
                        tbCountry.TabPages.Remove(tbCountryOFDB)
                    End If

                    'Credits
                    If scraperresult.CreditsSpecified Then
                        .txtCreditsOFDB.Text = String.Join(" / ", scraperresult.Credits.ToArray)
                        tbCredits.SelectedTab = tbCreditsOFDB
                    Else
                        tbCredits.TabPages.Remove(tbCreditsOFDB)
                    End If

                    'Director
                    If scraperresult.DirectorsSpecified Then
                        .txtDirectorOFDB.Text = String.Join(" / ", scraperresult.Directors.ToArray)
                        tbDirector.SelectedTab = tbDirectorOFDB
                    Else
                        tbDirector.TabPages.Remove(tbDirectorOFDB)
                    End If

                    'Genre
                    If scraperresult.GenresSpecified Then
                        .txtGenreOFDB.Text = String.Join(" / ", scraperresult.Genres.ToArray)
                        tbGenre.SelectedTab = tbGenreOFDB
                    Else
                        tbGenre.TabPages.Remove(tbGenreOFDB)
                    End If

                    'MPAA
                    If scraperresult.MPAASpecified Then
                        .txtMPAAOFDB.Text = scraperresult.MPAA
                        tbMPAA.SelectedTab = tbMPAAOFDB
                    Else
                        tbMPAA.TabPages.Remove(tbMPAAOFDB)
                    End If

                    'Outline
                    If scraperresult.OutlineSpecified Then
                        .txtOutlineOFDB.Text = scraperresult.Outline
                        tbOutline.SelectedTab = tbOutlineOFDB
                    Else
                        tbOutline.TabPages.Remove(tbOutlineOFDB)
                    End If

                    'Plot
                    If scraperresult.PlotSpecified Then
                        .txtPlotOFDB.Text = scraperresult.Plot
                        tbPlot.SelectedTab = tbPlotOFDB
                    Else
                        tbPlot.TabPages.Remove(tbPlotOFDB)
                    End If

                    'Rating
                    If scraperresult.RatingSpecified Then
                        .txtRatingOFDB.Text = scraperresult.Rating
                        tbRating.SelectedTab = tbRatingOFDB
                    Else
                        tbRating.TabPages.Remove(tbRatingOFDB)
                    End If

                    'ReleaseDate
                    If scraperresult.ReleaseDateSpecified Then
                        .txtReleaseDateOFDB.Text = scraperresult.ReleaseDate
                        tbReleaseYear.SelectedTab = tbReleaseYearOFDB
                    Else
                        tbReleaseYear.TabPages.Remove(tbReleaseYearOFDB)
                    End If

                    'Runtime
                    If scraperresult.RuntimeSpecified Then
                        .txtRuntimeOFDB.Text = scraperresult.Runtime
                        tbRuntime.SelectedTab = tbRuntimeOFDB
                    Else
                        tbRuntime.TabPages.Remove(tbRuntimeOFDB)
                    End If

                    'Studio
                    If scraperresult.StudiosSpecified Then
                        .txtStudioOFDB.Text = String.Join(" / ", scraperresult.Studios.ToArray)
                        tbStudio.SelectedTab = tbStudioOFDB
                    Else
                        tbStudio.TabPages.Remove(tbStudioOFDB)
                    End If

                    'Tagline
                    If scraperresult.TaglineSpecified Then
                        .txtTaglineOFDB.Text = scraperresult.Tagline
                        tbTagline.SelectedTab = tbTaglineOFDB
                    Else
                        tbTagline.TabPages.Remove(tbTaglineOFDB)
                    End If

                    'Top250
                    If scraperresult.Top250Specified Then
                        .txtTOP250OFDB.Text = scraperresult.Top250.ToString
                        tbTOP250.SelectedTab = tbTOP250OFDB
                    Else
                        tbTOP250.TabPages.Remove(tbTOP250OFDB)
                    End If

                    'Trailer
                    If scraperresult.TrailerSpecified Then
                        .txtTrailerOFDB.Text = scraperresult.Trailer
                        tbTrailer.SelectedTab = tbTrailerOFDB
                    Else
                        tbTrailer.TabPages.Remove(tbTrailerOFDB)
                    End If

                    'Votes
                    If scraperresult.VotesSpecified Then
                        .txtVotesOFDB.Text = scraperresult.Votes
                        tbVotes.SelectedTab = tbVotesOFDB
                    Else
                        tbVotes.TabPages.Remove(tbVotesOFDB)
                    End If

                    'Year
                    If scraperresult.YearSpecified Then
                        .txtYearOFDB.Text = scraperresult.Year
                        tbYear.SelectedTab = tbYearOFDB
                    Else
                        tbYear.TabPages.Remove(tbYearOFDB)
                    End If

                    'Actors
                    Dim lvItem As ListViewItem
                    .lvActorsOFDB.Items.Clear()
                    If scraperresult.Actors.Count > 0 Then
                        For Each OFDBAct As MediaContainers.Person In scraperresult.Actors
                            lvItem = .lvActorsOFDB.Items.Add(OFDBAct.Name)
                            lvItem.SubItems.Add(OFDBAct.Role)
                            lvItem.SubItems.Add(OFDBAct.URLOriginal)
                        Next
                        tbActors.SelectedTab = tbActorsOFDB
                    Else
                        tbActors.TabPages.Remove(tbActorsOFDB)
                    End If
                ElseIf scraperresult.Scrapersource.ToUpper = "MOVIEPILOT" Then
                    isActivatedMoviepilot = True
                    'Title
                    If scraperresult.TitleSpecified Then
                        .txtTitleMoviepilot.Text = scraperresult.Title
                        tbTitel.SelectedTab = tbTitelMoviepilot
                    Else
                        tbTitel.TabPages.Remove(tbTitelMoviepilot)
                    End If

                    'OriginalTitle
                    If scraperresult.OriginalTitleSpecified Then
                        .txtOriginalTitleMoviepilot.Text = scraperresult.OriginalTitle
                        tbOriginalTitel.SelectedTab = tbOriginalTitelMoviepilot
                    Else
                        tbOriginalTitel.TabPages.Remove(tbOriginalTitelMoviepilot)
                    End If

                    'Cert
                    If scraperresult.CertificationsSpecified Then
                        .txtCertMoviepilot.Text = String.Join(" / ", scraperresult.Certifications.ToArray)
                        tbCert.SelectedTab = tbCertMoviepilot
                    Else
                        tbCert.TabPages.Remove(tbCertMoviepilot)
                    End If

                    'Country
                    If scraperresult.CountriesSpecified Then
                        .txtCountryMoviepilot.Text = String.Join(" / ", scraperresult.Countries.ToArray)
                        tbCountry.SelectedTab = tbCountryMoviepilot
                    Else
                        tbCountry.TabPages.Remove(tbCountryMoviepilot)
                    End If

                    'Credits
                    If scraperresult.CreditsSpecified Then
                        .txtCreditsMoviepilot.Text = String.Join(" / ", scraperresult.Credits.ToArray)
                        tbCredits.SelectedTab = tbCreditsMoviepilot
                    Else
                        tbCredits.TabPages.Remove(tbCreditsMoviepilot)
                    End If

                    'Director
                    If scraperresult.DirectorsSpecified Then
                        .txtDirectorMoviepilot.Text = String.Join(" / ", scraperresult.Directors.ToArray)
                        tbDirector.SelectedTab = tbDirectorMoviepilot
                    Else
                        tbDirector.TabPages.Remove(tbDirectorMoviepilot)
                    End If

                    'Genre
                    If scraperresult.GenresSpecified Then
                        .txtGenreMoviepilot.Text = String.Join(" / ", scraperresult.Genres.ToArray)
                        tbGenre.SelectedTab = tbGenreMoviepilot
                    Else
                        tbGenre.TabPages.Remove(tbGenreMoviepilot)
                    End If

                    'MPAA
                    If scraperresult.MPAASpecified Then
                        .txtMPAAMoviepilot.Text = scraperresult.MPAA
                        tbMPAA.SelectedTab = tbMPAAMoviepilot
                    Else
                        tbMPAA.TabPages.Remove(tbMPAAMoviepilot)
                    End If

                    'Outline
                    If scraperresult.OutlineSpecified Then
                        .txtOutlineMoviepilot.Text = scraperresult.Outline
                        tbOutline.SelectedTab = tbOutlineMoviepilot
                    Else
                        tbOutline.TabPages.Remove(tbOutlineMoviepilot)
                    End If

                    'Plot
                    If scraperresult.PlotSpecified Then
                        .txtPlotMoviepilot.Text = scraperresult.Plot
                        tbPlot.SelectedTab = tbPlotMoviepilot
                    Else
                        tbPlot.TabPages.Remove(tbPlotMoviepilot)
                    End If

                    'Rating
                    If scraperresult.RatingSpecified Then
                        .txtRatingMoviepilot.Text = scraperresult.Rating
                        tbRating.SelectedTab = tbRatingMoviepilot
                    Else
                        tbRating.TabPages.Remove(tbRatingMoviepilot)
                    End If

                    'ReleaseDate
                    If scraperresult.ReleaseDateSpecified Then
                        .txtReleaseDateMoviepilot.Text = scraperresult.ReleaseDate
                        tbReleaseYear.SelectedTab = tbReleaseYearMoviepilot
                    Else
                        tbReleaseYear.TabPages.Remove(tbReleaseYearMoviepilot)
                    End If

                    'Runtime
                    If scraperresult.RuntimeSpecified Then
                        .txtRuntimeMoviepilot.Text = scraperresult.Runtime
                        tbRuntime.SelectedTab = tbRuntimeMoviepilot
                    Else
                        tbRuntime.TabPages.Remove(tbRuntimeMoviepilot)
                    End If

                    'Studio
                    If scraperresult.StudiosSpecified Then
                        .txtStudioMoviepilot.Text = String.Join(" / ", scraperresult.Studios.ToArray)
                        tbStudio.SelectedTab = tbStudioMoviepilot
                    Else
                        tbStudio.TabPages.Remove(tbStudioMoviepilot)
                    End If

                    'Tagline
                    If scraperresult.TaglineSpecified Then
                        .txtTaglineMoviepilot.Text = scraperresult.Tagline
                        tbTagline.SelectedTab = tbTaglineMoviepilot
                    Else
                        tbTagline.TabPages.Remove(tbTaglineMoviepilot)
                    End If

                    'Top250
                    If scraperresult.Top250Specified Then
                        .txtTOP250Moviepilot.Text = scraperresult.Top250.ToString
                        tbTOP250.SelectedTab = tbTOP250Moviepilot
                    Else
                        tbTOP250.TabPages.Remove(tbTOP250Moviepilot)
                    End If

                    'Trailer
                    If scraperresult.TrailerSpecified Then
                        .txtTrailerMoviepilot.Text = scraperresult.Trailer
                        tbTrailer.SelectedTab = tbTrailerMoviepilot
                    Else
                        tbTrailer.TabPages.Remove(tbTrailerMoviepilot)
                    End If

                    'Votes
                    If scraperresult.VotesSpecified Then
                        .txtVotesMoviepilot.Text = scraperresult.Votes
                        tbVotes.SelectedTab = tbVotesMoviepilot
                    Else
                        tbVotes.TabPages.Remove(tbVotesMoviepilot)
                    End If

                    'Year
                    If scraperresult.YearSpecified Then
                        .txtYearMoviepilot.Text = scraperresult.Year
                        tbYear.SelectedTab = tbYearMoviepilot
                    Else
                        tbYear.TabPages.Remove(tbYearMoviepilot)
                    End If

                    'Actors
                    Dim lvItem As ListViewItem
                    .lvActorsMoviepilot.Items.Clear()
                    If scraperresult.Actors.Count > 0 Then
                        For Each MoviepilotAct As MediaContainers.Person In scraperresult.Actors
                            lvItem = .lvActorsMoviepilot.Items.Add(MoviepilotAct.Name)
                            lvItem.SubItems.Add(MoviepilotAct.Role)
                            lvItem.SubItems.Add(MoviepilotAct.URLOriginal)
                        Next
                        tbActors.SelectedTab = tbActorsMoviepilot
                    Else
                        tbActors.TabPages.Remove(tbActorsMoviepilot)
                    End If
                End If

            End With
        Next
        'Remove all tabs of disabled scraper
        If isActivatedIMDB = False Then
            tbTitel.TabPages.Remove(tbTitelIMDB)
            tbOriginalTitel.TabPages.Remove(tbOriginalTitelIMDB)
            tbCert.TabPages.Remove(tbCertIMDB)
            tbCountry.TabPages.Remove(tbCountryIMDB)
            tbCredits.TabPages.Remove(tbCreditsIMDB)
            tbDirector.TabPages.Remove(tbDirectorIMDB)
            tbGenre.TabPages.Remove(tbGenreIMDB)
            tbMPAA.TabPages.Remove(tbMPAAIMDB)
            tbOutline.TabPages.Remove(tbOutlineIMDB)
            tbPlot.TabPages.Remove(tbPlotIMDB)
            tbRating.TabPages.Remove(tbRatingIMDB)
            tbReleaseYear.TabPages.Remove(tbReleaseYearIMDB)
            tbRuntime.TabPages.Remove(tbRuntimeIMDB)
            tbStudio.TabPages.Remove(tbStudioIMDB)
            tbTagline.TabPages.Remove(tbTaglineIMDB)
            tbTOP250.TabPages.Remove(tbTOP250IMDB)
            tbTrailer.TabPages.Remove(tbTrailerIMDB)
            tbVotes.TabPages.Remove(tbVotesIMDB)
            tbYear.TabPages.Remove(tbYearIMDB)
            tbActors.TabPages.Remove(tbActorsIMDB)
        End If
        If isActivatedTMDB = False Then
            tbTitel.TabPages.Remove(tbTitelTMDB)
            tbOriginalTitel.TabPages.Remove(tbOriginalTitelTMDB)
            tbCert.TabPages.Remove(tbCertTMDB)
            tbCountry.TabPages.Remove(tbCountryTMDB)
            tbCredits.TabPages.Remove(tbCreditsTMDB)
            tbDirector.TabPages.Remove(tbDirectorTMDB)
            tbGenre.TabPages.Remove(tbGenreTMDB)
            tbMPAA.TabPages.Remove(tbMPAATMDB)
            tbOutline.TabPages.Remove(tbOutlineTMDB)
            tbPlot.TabPages.Remove(tbPlotTMDB)
            tbRating.TabPages.Remove(tbRatingTMDB)
            tbReleaseYear.TabPages.Remove(tbReleaseYearTMDB)
            tbRuntime.TabPages.Remove(tbRuntimeTMDB)
            tbStudio.TabPages.Remove(tbStudioTMDB)
            tbTagline.TabPages.Remove(tbTaglineTMDB)
            tbTOP250.TabPages.Remove(tbTOP250TMDB)
            tbTrailer.TabPages.Remove(tbTrailerTMDB)
            tbVotes.TabPages.Remove(tbVotesTMDB)
            tbYear.TabPages.Remove(tbYearTMDB)
            tbActors.TabPages.Remove(tbActorsTMDB)
        End If
        If isActivatedOFDB = False Then
            tbTitel.TabPages.Remove(tbTitelOFDB)
            tbOriginalTitel.TabPages.Remove(tbOriginalTitelOFDB)
            tbCert.TabPages.Remove(tbCertOFDB)
            tbCountry.TabPages.Remove(tbCountryOFDB)
            tbCredits.TabPages.Remove(tbCreditsOFDB)
            tbDirector.TabPages.Remove(tbDirectorOFDB)
            tbGenre.TabPages.Remove(tbGenreOFDB)
            tbMPAA.TabPages.Remove(tbMPAAOFDB)
            tbOutline.TabPages.Remove(tbOutlineOFDB)
            tbPlot.TabPages.Remove(tbPlotOFDB)
            tbRating.TabPages.Remove(tbRatingOFDB)
            tbReleaseYear.TabPages.Remove(tbReleaseYearOFDB)
            tbRuntime.TabPages.Remove(tbRuntimeOFDB)
            tbStudio.TabPages.Remove(tbStudioOFDB)
            tbTagline.TabPages.Remove(tbTaglineOFDB)
            tbTOP250.TabPages.Remove(tbTOP250OFDB)
            tbTrailer.TabPages.Remove(tbTrailerOFDB)
            tbVotes.TabPages.Remove(tbVotesOFDB)
            tbYear.TabPages.Remove(tbYearOFDB)
            tbActors.TabPages.Remove(tbActorsOFDB)
        End If
        If isActivatedMoviepilot = False Then
            tbTitel.TabPages.Remove(tbTitelMoviepilot)
            tbOriginalTitel.TabPages.Remove(tbOriginalTitelMoviepilot)
            tbCert.TabPages.Remove(tbCertMoviepilot)
            tbCountry.TabPages.Remove(tbCountryMoviepilot)
            tbCredits.TabPages.Remove(tbCreditsMoviepilot)
            tbDirector.TabPages.Remove(tbDirectorMoviepilot)
            tbGenre.TabPages.Remove(tbGenreMoviepilot)
            tbMPAA.TabPages.Remove(tbMPAAMoviepilot)
            tbOutline.TabPages.Remove(tbOutlineMoviepilot)
            tbPlot.TabPages.Remove(tbPlotMoviepilot)
            tbRating.TabPages.Remove(tbRatingMoviepilot)
            tbReleaseYear.TabPages.Remove(tbReleaseYearMoviepilot)
            tbRuntime.TabPages.Remove(tbRuntimeMoviepilot)
            tbStudio.TabPages.Remove(tbStudioMoviepilot)
            tbTagline.TabPages.Remove(tbTaglineMoviepilot)
            tbTOP250.TabPages.Remove(tbTOP250Moviepilot)
            tbTrailer.TabPages.Remove(tbTrailerMoviepilot)
            tbVotes.TabPages.Remove(tbVotesMoviepilot)
            tbYear.TabPages.Remove(tbYearMoviepilot)
            tbActors.TabPages.Remove(tbActorsMoviepilot)
        End If

        tbTrailer_TabIndexChanged(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Overwrite scraperresults with data from selected tabs
    ''' </summary>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation
    ''' </remarks>
    Private Sub SaveSelectedScraperData()
        'Step 1: First save all selected information in temporay movie container
        Dim _nmovie As New MediaContainers.Movie

        For Each scraperresult In _ScrapedList
            If tbActors.TabCount > 0 AndAlso tbActors.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Actors = scraperresult.Actors
            End If
            If tbCert.TabCount > 0 AndAlso tbCert.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Certifications = scraperresult.Certifications
            End If
            If tbCountry.TabCount > 0 AndAlso tbCountry.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Countries = scraperresult.Countries
            End If
            If tbCredits.TabCount > 0 AndAlso tbCredits.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Credits = scraperresult.Credits
            End If
            If tbDirector.TabCount > 0 AndAlso tbDirector.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Directors = scraperresult.Directors
            End If
            If tbGenre.TabCount > 0 AndAlso tbGenre.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Genres = scraperresult.Genres
            End If
            If tbMPAA.TabCount > 0 AndAlso tbMPAA.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.MPAA = scraperresult.MPAA
            End If
            If tbOriginalTitel.TabCount > 0 AndAlso tbOriginalTitel.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.OriginalTitle = scraperresult.OriginalTitle
            End If
            If tbOutline.TabCount > 0 AndAlso tbOutline.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Outline = scraperresult.Outline
            End If
            If tbPlot.TabCount > 0 AndAlso tbPlot.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Plot = scraperresult.Plot
            End If
            If tbRating.TabCount > 0 AndAlso tbRating.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Rating = scraperresult.Rating
            End If
            If tbReleaseYear.TabCount > 0 AndAlso tbReleaseYear.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.ReleaseDate = scraperresult.ReleaseDate
            End If
            If tbRuntime.TabCount > 0 AndAlso tbRuntime.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Runtime = scraperresult.Runtime
            End If
            If tbStudio.TabCount > 0 AndAlso tbStudio.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Studios = scraperresult.Studios
            End If
            If tbTagline.TabCount > 0 AndAlso tbTagline.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Tagline = scraperresult.Tagline
            End If
            If tbTitel.TabCount > 0 AndAlso tbTitel.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Title = scraperresult.Title
            End If
            If tbTOP250.TabCount > 0 AndAlso tbTOP250.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Top250 = scraperresult.Top250
            End If
            If tbTrailer.TabCount > 0 AndAlso tbTrailer.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Trailer = scraperresult.Trailer
            End If
            If tbVotes.TabCount > 0 AndAlso tbVotes.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Votes = scraperresult.Votes
            End If
            If tbYear.TabCount > 0 AndAlso tbYear.SelectedTab.Name.ToUpper.Contains(scraperresult.Scrapersource.ToUpper) Then
                _nmovie.Year = scraperresult.Year
            End If
        Next
        'Step 2: Now overwrite scraped fields of every data scraperresult with selected data(=_nmovie)
        For Each scraperresult In _ScrapedList
            scraperresult.Actors = _nmovie.Actors
            scraperresult.Certifications = _nmovie.Certifications
            scraperresult.Countries = _nmovie.Countries
            scraperresult.Credits = _nmovie.Credits
            scraperresult.Directors = _nmovie.Directors
            scraperresult.Genres = _nmovie.Genres
            scraperresult.MPAA = _nmovie.MPAA
            scraperresult.OriginalTitle = _nmovie.OriginalTitle
            scraperresult.Outline = _nmovie.Outline
            scraperresult.Plot = _nmovie.Plot
            scraperresult.Rating = _nmovie.Rating
            scraperresult.ReleaseDate = _nmovie.ReleaseDate
            scraperresult.Runtime = _nmovie.Runtime
            scraperresult.Studios = _nmovie.Studios
            scraperresult.Tagline = _nmovie.Tagline
            scraperresult.Title = _nmovie.Title
            scraperresult.Top250 = _nmovie.Top250
            scraperresult.Trailer = _nmovie.Trailer
            scraperresult.Votes = _nmovie.Votes
            scraperresult.Year = _nmovie.Year
        Next
    End Sub
#End Region

#Region "GUI-Eventhandler"

    ''' <summary>
    ''' Load event of form
    ''' </summary>
    ''' <param name="sender">Window</param>
    ''' <param name="e">Window</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation
    ''' </remarks>
    Private Sub dlgMovieDataScraperPreview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Initiate Listviewsorter
            lvwActorSorter = New ListViewColumnSorter()
            lvActorsIMDB.ListViewItemSorter = lvwActorSorter
            lvActorsTMDB.ListViewItemSorter = lvwActorSorter
            lvActorsOFDB.ListViewItemSorter = lvwActorSorter
            lvActorsMoviepilot.ListViewItemSorter = lvwActorSorter

            BringToFront()

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            ' Fill control of each tab which scraperresult data
            FillInfo()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    ''' <summary>
    ''' Overwrite scraperresults with data from selected tabs and close preview window
    ''' </summary>
    ''' <param name="sender">OK_Button</param>
    ''' <param name="e">OK_Button</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation
    ''' </remarks>
    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Try
            SaveSelectedScraperData()
            DialogResult = System.Windows.Forms.DialogResult.OK
            Close()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            DialogResult = System.Windows.Forms.DialogResult.OK
            Close()
        End Try
    End Sub

    ''' <summary>
    ''' Sorter logic for IMDB Actors listview
    ''' </summary>
    ''' <param name="sender">Actors listview</param>
    ''' <param name="e">Actors listview</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation
    ''' </remarks>
    Private Sub lvActorsIMDB_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvActorsIMDB.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.
        Try
            If (e.Column = lvwActorSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (lvwActorSorter.Order = SortOrder.Ascending) Then
                    lvwActorSorter.Order = SortOrder.Descending
                Else
                    lvwActorSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                lvwActorSorter.SortColumn = e.Column
                lvwActorSorter.Order = SortOrder.Ascending
            End If

            ' Perform the sort with these new sort options.
            lvActorsIMDB.Sort()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    ''' <summary>
    ''' Sorter logic for OFDB Actors listview
    ''' </summary>
    ''' <param name="sender">Actors listview</param>
    ''' <param name="e">Actors listview</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation
    ''' </remarks>
    Private Sub lvActorsOFDB_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvActorsOFDB.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.
        Try
            If (e.Column = lvwActorSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (lvwActorSorter.Order = SortOrder.Ascending) Then
                    lvwActorSorter.Order = SortOrder.Descending
                Else
                    lvwActorSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                lvwActorSorter.SortColumn = e.Column
                lvwActorSorter.Order = SortOrder.Ascending
            End If

            ' Perform the sort with these new sort options.
            lvActorsOFDB.Sort()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    ''' <summary>
    ''' Sorter logic for Moviepilot Actors listview
    ''' </summary>
    ''' <param name="sender">Actors listview</param>
    ''' <param name="e">Actors listview</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation
    ''' </remarks>
    Private Sub lvActorsMoviepilot_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvActorsMoviepilot.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.
        Try
            If (e.Column = lvwActorSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (lvwActorSorter.Order = SortOrder.Ascending) Then
                    lvwActorSorter.Order = SortOrder.Descending
                Else
                    lvwActorSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                lvwActorSorter.SortColumn = e.Column
                lvwActorSorter.Order = SortOrder.Ascending
            End If

            ' Perform the sort with these new sort options.
            lvActorsMoviepilot.Sort()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    ''' <summary>
    ''' Sorter logic for TMDB Actors listview
    ''' </summary>
    ''' <param name="sender">Actors listview</param>
    ''' <param name="e">Actors listview</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation
    ''' </remarks>
    Private Sub lvActorsTMDB_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvActorsTMDB.ColumnClick
        ' Determine if the clicked column is already the column that is
        ' being sorted.
        Try
            If (e.Column = lvwActorSorter.SortColumn) Then
                ' Reverse the current sort direction for this column.
                If (lvwActorSorter.Order = SortOrder.Ascending) Then
                    lvwActorSorter.Order = SortOrder.Descending
                Else
                    lvwActorSorter.Order = SortOrder.Ascending
                End If
            Else
                ' Set the column number that is to be sorted; default to ascending.
                lvwActorSorter.SortColumn = e.Column
                lvwActorSorter.Order = SortOrder.Ascending
            End If

            ' Perform the sort with these new sort options.
            lvActorsTMDB.Sort()
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    ''' <summary>
    ''' Enable/Disable Trailerbutton
    ''' </summary>
    ''' <param name="sender">trailer-tabcontrol</param>
    ''' <param name="e">trailer-tabcontrol</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation: Only enable play trailer button if theres a scraped trailerpath otherwise disable trailer button!
    ''' </remarks>
    Private Sub tbTrailer_TabIndexChanged(sender As Object, e As EventArgs) Handles tbTrailer.TabIndexChanged
        Try
            If tbTrailer.TabPages.Count > 0 Then
                If tbTrailer.SelectedTab.Name = tbTrailerIMDB.Name Then
                    If txtTrailerIMDB.Text = "" Then
                        btnPlayTrailer.Enabled = False
                    Else
                        btnPlayTrailer.Enabled = True
                    End If
                ElseIf tbTrailer.SelectedTab.Name = tbTrailerTMDB.Name Then
                    If txtTrailerTMDB.Text = "" Then
                        btnPlayTrailer.Enabled = False
                    Else
                        btnPlayTrailer.Enabled = True
                    End If
                ElseIf tbTrailer.SelectedTab.Name = tbVotesMoviepilot.Name Then
                    If txtTrailerMoviepilot.Text = "" Then
                        btnPlayTrailer.Enabled = False
                    Else
                        btnPlayTrailer.Enabled = True
                    End If
                ElseIf tbTrailer.SelectedTab.Name = tbTrailerOFDB.Name Then
                    If txtTrailerOFDB.Text = "" Then
                        btnPlayTrailer.Enabled = False
                    Else
                        btnPlayTrailer.Enabled = True
                    End If
                End If
            Else
                btnPlayTrailer.Enabled = False
            End If
        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
            btnPlayTrailer.Enabled = False
        End Try
    End Sub

    ''' <summary>
    ''' Play selected trailer
    ''' </summary>
    ''' <param name="sender">trailer-button</param>
    ''' <param name="e">trailer-button</param>
    ''' <remarks>
    ''' 2014/09/13 Cocotus - First implementation: If trailer-URL available, play trailer!
    ''' </remarks>
    Private Sub btnPlayTrailer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayTrailer.Click
        Try
            Dim tPath As String = String.Empty
            If tbTrailer.TabPages.Count > 0 Then
                Select Case tbTrailer.SelectedTab.Name
                    Case "tbTrailerIMDB"
                        If Not String.IsNullOrEmpty(txtTrailerIMDB.Text) Then
                            tPath = String.Concat("""", txtTrailerIMDB.Text, """")
                        End If
                    Case "tbTrailerTMDB"
                        If Not String.IsNullOrEmpty(txtTrailerTMDB.Text) Then
                            tPath = String.Concat("""", txtTrailerTMDB.Text, """")
                        End If
                    Case "tbTrailerOFDB"
                        If Not String.IsNullOrEmpty(txtTrailerOFDB.Text) Then
                            tPath = String.Concat("""", txtTrailerOFDB.Text, """")
                        End If
                    Case "tbTrailerMoviepilot"
                        If Not String.IsNullOrEmpty(txtTrailerMoviepilot.Text) Then
                            tPath = String.Concat("""", txtTrailerMoviepilot.Text, """")
                        End If
                End Select
            End If

            If Not String.IsNullOrEmpty(tPath) Then
                If Master.isWindows Then
                    If Regex.IsMatch(tPath, "plugin:\/\/plugin\.video\.youtube\/\?action=play_video&videoid=") Then
                        tPath = tPath.Replace("plugin://plugin.video.youtube/?action=play_video&videoid=", "http://www.youtube.com/watch?v=")
                    End If
                    Process.Start(tPath)
                Else
                    Using Explorer As New Process
                        Explorer.StartInfo.FileName = "xdg-open"
                        Explorer.StartInfo.Arguments = tPath
                        Explorer.Start()
                    End Using
                End If
            End If

        Catch
            MessageBox.Show(Master.eLang.GetString(270, "The trailer could not be played. This could be due to an invalid URI or you do not have the proper player to play the trailer type."), Master.eLang.GetString(271, "Error Playing Trailer"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

#End Region

End Class