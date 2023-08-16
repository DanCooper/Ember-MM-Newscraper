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

Imports EmberAPI
Imports NLog

Public Class dlgCustomScraper

#Region "Fields"

    Shared logger As Logger = LogManager.GetCurrentClassLogger()

    Private CustomUpdater As New Structures.CustomUpdaterStruct
    Private _ContentType As Enums.ContentType

    'ScrapeModifiers
    Private mEpisodeActorThumbsAllowed As Boolean
    Private mEpisodeFanartAllowed As Boolean
    Private mEpisodeMetaDataAllowed As Boolean
    Private mEpisodeNFOAllowed As Boolean
    Private mEpisodePosterAllowed As Boolean
    Private mMainActorThumbsAllowed As Boolean
    Private mMainBannerAllowed As Boolean
    Private mMainCharacterArtAllowed As Boolean
    Private mMainClearArtAllowed As Boolean
    Private mMainClearLogoAllowed As Boolean
    Private mMainDiscArtAllowed As Boolean
    Private mMainExtrafanartsAllowed As Boolean
    Private mMainExtrathumbsAllowed As Boolean
    Private mMainFanartAllowed As Boolean
    Private mMainKeyartAllowed As Boolean
    Private mMainLandscapeAllowed As Boolean
    Private mMainMetaDataAllowed As Boolean
    Private mMainNFOAllowed As Boolean
    Private mMainPosterAllowed As Boolean
    Private mMainThemeAllowed As Boolean
    Private mMainTrailerAllowed As Boolean
    Private mSeasonBannerAllowed As Boolean
    Private mSeasonFanartAllowed As Boolean
    Private mSeasonLandscapeAllowed As Boolean
    Private mSeasonPosterAllowed As Boolean

    'ScrapeOptions
    Private oEpisodeActorsAllowed As Boolean
    Private oEpisodeAiredAllowed As Boolean
    Private oEpisodeDirectorsAllowed As Boolean
    Private oEpisodeGuestStarsAllowed As Boolean
    Private oEpisodeOriginalTitleAllowed As Boolean
    Private oEpisodePlotAllowed As Boolean
    Private oEpisodeRatingAllowed As Boolean
    Private oEpisodeRuntimeAllowed As Boolean
    Private oEpisodeTitleAllowed As Boolean
    Private oEpisodeWritersAllowed As Boolean
    Private oMainActorsAllowed As Boolean
    Private oMainCertificationsAllowed As Boolean
    Private oMainCollectionIDAllowed As Boolean
    Private oMainCountriesAllowed As Boolean
    Private oMainCreatorsAllowed As Boolean
    Private oMainDirectorsAllowed As Boolean
    Private oMainEpisodeGuideURLAllowed As Boolean
    Private oMainGenresAllowed As Boolean
    Private oMainMPAAAllowed As Boolean
    Private oMainOriginalTitleAllowed As Boolean
    Private oMainOutlineAllowed As Boolean
    Private oMainPlotAllowed As Boolean
    Private oMainPremieredAllowed As Boolean
    Private oMainProducersAllowed As Boolean
    Private oMainRatingAllowed As Boolean
    Private oMainRuntimeAllowed As Boolean
    Private oMainStatusAllowed As Boolean
    Private oMainStudiosAllowed As Boolean
    Private oMainTaglineAllowed As Boolean
    Private oMainTitleAllowed As Boolean
    Private oMainTop250Allowed As Boolean
    Private oMainTrailerAllowed As Boolean
    Private oMainWritersAllowed As Boolean
    Private oSeasonAiredAllowed As Boolean
    Private oSeasonPlotAllowed As Boolean
    Private oSeasonTitleAllowed As Boolean

#End Region 'Fields

#Region "Dialog"

    Public Sub New(ByVal tContentType As Enums.ContentType)
        ' This call is required by the designer.
        InitializeComponent()
        Left = Master.AppPos.Left + (Master.AppPos.Width - Width) \ 2
        Top = Master.AppPos.Top + (Master.AppPos.Height - Height) \ 2
        StartPosition = FormStartPosition.Manual

        _ContentType = tContentType
    End Sub

    Private Sub DialogResult_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
    End Sub

    Private Sub DialogResult_OK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub Dialog_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Setup()

            Dim iBackground As New Bitmap(pnlTop.Width, pnlTop.Height)
            Using g As Graphics = Graphics.FromImage(iBackground)
                g.FillRectangle(New Drawing2D.LinearGradientBrush(pnlTop.ClientRectangle, Color.SteelBlue, Color.LightSteelBlue, Drawing2D.LinearGradientMode.Horizontal), pnlTop.ClientRectangle)
                pnlTop.BackgroundImage = iBackground
            End Using

            'set defaults
            SetParameters()

            CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
            CustomUpdater.SelectionType = Enums.SelectionType.All
            'Functions.SetScrapeModifiers(CustomUpdater.ScrapeModifiers, Enums.ModifierType.All, True)

            CheckEnable()

        Catch ex As Exception
            logger.Error(ex, New StackFrame().GetMethod().Name)
        End Try
    End Sub

    Private Sub Dialog_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
        Activate()
    End Sub

    Private Sub Setup()
        Text = Master.eLang.GetString(384, "Custom Scraper")
        lblTopTitle.Text = Text

        'Actors
        Dim strActors As String = Master.eLang.GetString(231, "Actors")
        chkEpisodeOptionsActors.Text = strActors
        chkMainOptionsActors.Text = strActors

        'Actor Thumbs
        Dim strActorThumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        chkEpisodeModifierActorThumbs.Text = strActorThumbs
        chkMainModifierActorThumbs.Text = strActorThumbs

        'Aired
        Dim strAired As String = Master.eLang.GetString(728, "Aired")
        chkEpisodeOptionsAired.Text = strAired
        chkSeasonOptionsAired.Text = strAired

        'All
        Dim strAll As String = Master.eLang.GetString(68, "All")
        rbScrapeType_All.Text = strAll

        'All Items
        Dim strAllItems As String = Master.eLang.GetString(70, "All Items")
        chkEpisodeModifierAll.Text = strAllItems
        chkEpisodeOptionsAll.Text = strAllItems
        chkMainModifierAll.Text = strAllItems
        chkMainOptionsAll.Text = strAllItems
        chkSeasonModifierAll.Text = strAllItems
        chkSeasonOptionsAll.Text = strAllItems
        chkSpecialModifierAll.Text = strAllItems

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        chkMainModifierBanner.Text = strBanner
        chkSeasonModifierBanner.Text = strBanner

        'Cancel
        Dim strCancel As String = Master.eLang.GetString(167, "Cancel")
        btnCancel.Text = strCancel

        'Certifications
        Dim strCertifications As String = Master.eLang.GetString(56, "Certifications")
        chkMainOptionsCertifications.Text = strCertifications

        'CharacterArt
        Dim strCharacterArt As String = Master.eLang.GetString(1140, "CharacterArt")
        chkMainModifierCharacterArt.Text = strCharacterArt

        'ClearArt
        Dim strClearArt As String = Master.eLang.GetString(1096, "ClearArt")
        chkMainModifierClearArt.Text = strClearArt

        'ClearLogo
        Dim strClearLogo As String = Master.eLang.GetString(1097, "ClearLogo")
        chkMainModifierClearLogo.Text = strClearLogo

        'Collection ID
        Dim strCollectionsId As String = Master.eLang.GetString(1135, "Collection ID")
        chkMainOptionsCollectionId.Text = strCollectionsId

        'Countries
        Dim strCountries As String = Master.eLang.GetString(237, "Countries")
        chkMainOptionsCountries.Text = strCountries

        'Creators
        Dim strCreators As String = Master.eLang.GetString(744, "Creators")
        chkMainOptionsCreators.Text = strCreators

        'Directors
        Dim strDirectors As String = Master.eLang.GetString(940, "Directors")
        chkEpisodeOptionsDirectors.Text = strDirectors
        chkMainOptionsDirectors.Text = strDirectors

        'DiscArt
        Dim strDiscArt As String = Master.eLang.GetString(1098, "DiscArt")
        chkMainModifierDiscArt.Text = strDiscArt

        'Episode Guide URL
        Dim strEpsiodeGuideUrl As String = Master.eLang.GetString(723, "Episode Guide URL")
        chkMainOptionsEpisodeGuideUrl.Text = strEpsiodeGuideUrl

        'Extrafanarts
        Dim strExtrafanarts As String = Master.eLang.GetString(992, "Extrafanarts")
        chkMainModifierExtrafanarts.Text = strExtrafanarts

        'Extrathumbs
        Dim strExtrathumbs As String = Master.eLang.GetString(153, "Extrathumbs")
        chkMainModifierExtrathumbs.Text = strExtrathumbs

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        chkEpisodeModifierFanart.Text = strFanart
        chkMainModifierFanart.Text = strFanart
        chkSeasonModifierFanart.Text = strFanart

        'Genres
        Dim strGenres As String = Master.eLang.GetString(725, "Genres")
        chkMainOptionsGenres.Text = strGenres

        'Guest Stars
        Dim strGuestStars As String = Master.eLang.GetString(508, "Guest Stars")
        chkEpisodeOptionsGuestStars.Text = strGuestStars

        'Keyart
        Dim strKeyart As String = Master.eLang.GetString(1237, "Keyart")
        chkMainModifierKeyart.Text = strKeyart

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        chkMainModifierLandscape.Text = strLandscape
        chkSeasonModifierLandscape.Text = strLandscape

        'MetaData
        Dim strMetaData As String = Master.eLang.GetString(59, "Meta Data")
        chkEpisodeModifierMetaData.Text = strMetaData
        chkMainModifierMetaData.Text = strMetaData

        'MPAA
        Dim strMPAA As String = Master.eLang.GetString(401, "MPAA")
        chkMainOptionsMPAA.Text = strMPAA

        'NFO
        Dim strNFO As String = Master.eLang.GetString(150, "NFO")
        chkEpisodeModifierNfo.Text = strNFO
        chkMainModifierNfo.Text = strNFO

        'Original Title
        Dim strOriginalTitle As String = Master.eLang.GetString(302, "Original Title")
        chkEpisodeOptionsOriginalTitle.Text = strOriginalTitle
        chkMainOptionsOriginalTitle.Text = strOriginalTitle

        'Plot
        Dim strPlot As String = Master.eLang.GetString(65, "Plot")
        chkEpisodeOptionsPlot.Text = strPlot
        chkMainOptionsPlot.Text = strPlot

        'Plot Outline
        Dim strPlotOutline As String = Master.eLang.GetString(64, "Plot Outline")
        chkMainOptionsOutline.Text = strPlotOutline

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        chkEpisodeModifierPoster.Text = strPoster
        chkMainModifierPoster.Text = strPoster
        chkSeasonModifierPoster.Text = strPoster

        'Rating
        Dim strRating As String = Master.eLang.GetString(245, "Rating")
        chkEpisodeOptionsRating.Text = strRating
        chkMainOptionsRating.Text = strRating

        'Runtime
        Dim strRuntime As String = Master.eLang.GetString(238, "Runtime")
        chkEpisodeOptionsRuntime.Text = strRuntime
        chkMainOptionsRuntime.Text = strRuntime

        'Select None
        Dim strSelectNone As String = Master.eLang.GetString(1139, "Select None")
        btnEpisodeScrapeModifierNone.Text = strSelectNone
        btnEpisodeScrapeOptionsNone.Text = strSelectNone
        btnMainScrapeModifierNone.Text = strSelectNone
        btnMainScrapeOptionsNone.Text = strSelectNone
        btnSeasonScrapeModifierNone.Text = strSelectNone
        btnSeasonScrapeOptionsNone.Text = strSelectNone
        btnSpecialScrapeModifierNone.Text = strSelectNone

        'Studios
        Dim strStudios As String = Master.eLang.GetString(226, "Studios")
        chkMainOptionsStudios.Text = strStudios

        'Tagline
        Dim strTagline As String = Master.eLang.GetString(397, "Tagline")
        chkMainOptionsTagline.Text = strTagline

        'Theme
        Dim strTheme As String = Master.eLang.GetString(1118, "Theme")
        chkMainModifierTheme.Text = strTheme

        'Title
        Dim strTitle As String = Master.eLang.GetString(21, "Title")
        chkEpisodeOptionsTitle.Text = strTitle
        chkMainOptionsTitle.Text = strTitle

        'Top 250
        Dim strTop250 As String = Master.eLang.GetString(591, "Top 250")
        chkMainOptionsTop250.Text = strTop250

        'Trailer
        Dim strTrailer As String = Master.eLang.GetString(151, "Trailer")
        chkMainModifierTrailer.Text = strTrailer
        chkMainOptionsTrailer.Text = strTrailer

        'with Episodes
        Dim strWithEpisodes As String = Master.eLang.GetString(960, "with Episodes")
        chkSpecialModifierWithEpisodes.Text = strWithEpisodes

        'with Seasons
        Dim strWithSeasons As String = Master.eLang.GetString(959, "with Seasons")
        chkSpecialModifierWithSeasons.Text = strWithSeasons

        'Writers
        Dim strWriters As String = Master.eLang.GetString(394, "Writers")
        chkEpisodeOptionsWriters.Text = strWriters
        chkMainOptionsWriters.Text = strWriters


        btnOK.Text = Master.eLang.GetString(389, "Begin")
        gbEpisodeScrapeModifiers.Text = Master.eLang.GetString(1247, "Episode Modifiers")
        gbEpisodeScrapeOptions.Text = Master.eLang.GetString(1251, "Episode Options")
        gbMainScrapeModifiers.Text = Master.eLang.GetString(388, "Modifiers")
        gbMainScrapeOptions.Text = Master.eLang.GetString(390, "Options")
        gbScrapeType_Filter.Text = Master.eLang.GetString(386, "Selection Filter")
        gbScrapeType_Mode.Text = Master.eLang.GetString(387, "Update Mode")
        gbSeasonScrapeModifiers.Text = Master.eLang.GetString(1250, "Season Modifiers")
        gbSeasonScrapeOptions.Text = Master.eLang.GetString(1252, "Season Options")
        gbSpecialScrapeModifiers.Text = Master.eLang.GetString(1246, "Spezial Modifiers")
        lblTopDescription.Text = Master.eLang.GetString(385, "Create a custom scraper")
        rbScrapeType_Ask.Text = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        rbScrapeType_Auto.Text = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        rbScrapeType_Filter.Text = Master.eLang.GetString(624, "Current Filter")
        rbScrapeType_Marked.Text = Master.eLang.GetString(48, "Marked")
        rbScrapeType_Missing.Text = Master.eLang.GetString(40, "Missing Items")
        rbScrapeType_New.Text = Master.eLang.GetString(47, "New")
        rbScrapeType_Selected.Text = Master.eLang.GetString(1076, "Selected")
        rbScrapeType_Skip.Text = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")
    End Sub

    Public Overloads Function ShowDialog() As Structures.CustomUpdaterStruct
        If MyBase.ShowDialog() = DialogResult.OK Then
            CustomUpdater.Canceled = False
        Else
            CustomUpdater.Canceled = True
        End If
        Return CustomUpdater
    End Function

#End Region 'Dialog

#Region "Methods"

    Private Sub btnEpisodeScrapeModifierNone_Click(sender As Object, e As EventArgs) Handles btnEpisodeScrapeModifierNone.Click
        chkEpisodeModifierAll.Checked = False

        chkEpisodeModifierActorThumbs.Checked = False
        chkEpisodeModifierFanart.Checked = False
        chkEpisodeModifierMetaData.Checked = False
        chkEpisodeModifierNfo.Checked = False
        chkEpisodeModifierPoster.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnEpisodeScrapeOptionsNone_Click(sender As Object, e As EventArgs) Handles btnEpisodeScrapeOptionsNone.Click
        chkEpisodeOptionsAll.Checked = False

        chkEpisodeOptionsActors.Checked = False
        chkEpisodeOptionsAired.Checked = False
        chkEpisodeOptionsDirectors.Checked = False
        chkEpisodeOptionsGuestStars.Checked = False
        chkEpisodeOptionsOriginalTitle.Checked = False
        chkEpisodeOptionsPlot.Checked = False
        chkEpisodeOptionsRating.Checked = False
        chkEpisodeOptionsRuntime.Checked = False
        chkEpisodeOptionsTitle.Checked = False
        chkEpisodeOptionsWriters.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnMainScrapeModifierNone_Click(sender As Object, e As EventArgs) Handles btnMainScrapeModifierNone.Click
        chkMainModifierAll.Checked = False

        chkMainModifierActorThumbs.Checked = False
        chkMainModifierBanner.Checked = False
        chkMainModifierCharacterArt.Checked = False
        chkMainModifierClearArt.Checked = False
        chkMainModifierClearLogo.Checked = False
        chkMainModifierDiscArt.Checked = False
        chkMainModifierExtrafanarts.Checked = False
        chkMainModifierExtrathumbs.Checked = False
        chkMainModifierFanart.Checked = False
        chkMainModifierKeyart.Checked = False
        chkMainModifierLandscape.Checked = False
        chkMainModifierMetaData.Checked = False
        chkMainModifierNfo.Checked = False
        chkMainModifierPoster.Checked = False
        chkMainModifierTheme.Checked = False
        chkMainModifierTrailer.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnMainScrapeOptionsNone_Click(sender As Object, e As EventArgs) Handles btnMainScrapeOptionsNone.Click
        chkMainOptionsAll.Checked = False

        chkMainOptionsActors.Checked = False
        chkMainOptionsCertifications.Checked = False
        chkMainOptionsCollectionId.Checked = False
        chkMainOptionsCountries.Checked = False
        chkMainOptionsCreators.Checked = False
        chkMainOptionsDirectors.Checked = False
        chkMainOptionsEpisodeGuideUrl.Checked = False
        chkMainOptionsGenres.Checked = False
        chkMainOptionsMPAA.Checked = False
        chkMainOptionsOriginalTitle.Checked = False
        chkMainOptionsOutline.Checked = False
        chkMainOptionsPlot.Checked = False
        chkMainOptionsPremiered.Checked = False
        chkMainOptionsRating.Checked = False
        chkMainOptionsRuntime.Checked = False
        chkMainOptionsStatus.Checked = False
        chkMainOptionsStudios.Checked = False
        chkMainOptionsTagline.Checked = False
        chkMainOptionsTitle.Checked = False
        chkMainOptionsTop250.Checked = False
        chkMainOptionsTrailer.Checked = False
        chkMainOptionsWriters.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnSeasonScrapeModifierNone_Click(sender As Object, e As EventArgs) Handles btnSeasonScrapeModifierNone.Click
        chkSeasonModifierAll.Checked = False

        chkSeasonModifierBanner.Checked = False
        chkSeasonModifierFanart.Checked = False
        chkSeasonModifierLandscape.Checked = False
        chkSeasonModifierPoster.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnSeasonScrapeOptionsNone_Click(sender As Object, e As EventArgs) Handles btnSeasonScrapeOptionsNone.Click
        chkSeasonOptionsAll.Checked = False

        chkSeasonOptionsAired.Checked = False
        chkSeasonOptionsPlot.Checked = False
        chkSeasonOptionsTitle.Checked = False

        CheckEnable()
    End Sub

    Private Sub btnSpecialScrapeModifierNone_Click(sender As Object, e As EventArgs) Handles btnSpecialScrapeModifierNone.Click
        chkSpecialModifierAll.Checked = False

        chkSpecialModifierWithEpisodes.Checked = False
        chkSpecialModifierWithSeasons.Checked = False

        CheckEnable()
    End Sub

    Private Sub CheckEnable()

        'Main Modifiers
        If chkMainModifierAll.Checked Then
            chkMainModifierActorThumbs.Checked = mMainActorThumbsAllowed
            chkMainModifierActorThumbs.Enabled = False
            chkMainModifierBanner.Checked = mMainBannerAllowed
            chkMainModifierBanner.Enabled = False
            chkMainModifierCharacterArt.Checked = mMainCharacterArtAllowed
            chkMainModifierCharacterArt.Enabled = False
            chkMainModifierClearArt.Checked = mMainClearArtAllowed
            chkMainModifierClearArt.Enabled = False
            chkMainModifierClearLogo.Checked = mMainClearLogoAllowed
            chkMainModifierClearLogo.Enabled = False
            chkMainModifierDiscArt.Checked = mMainDiscArtAllowed
            chkMainModifierDiscArt.Enabled = False
            chkMainModifierExtrafanarts.Checked = mMainExtrafanartsAllowed
            chkMainModifierExtrafanarts.Enabled = False
            chkMainModifierExtrathumbs.Checked = mMainExtrathumbsAllowed
            chkMainModifierExtrathumbs.Enabled = False
            chkMainModifierFanart.Checked = mMainFanartAllowed
            chkMainModifierFanart.Enabled = False
            chkMainModifierKeyart.Checked = mMainKeyartAllowed
            chkMainModifierKeyart.Enabled = False
            chkMainModifierLandscape.Checked = mMainLandscapeAllowed
            chkMainModifierLandscape.Enabled = False
            chkMainModifierMetaData.Checked = mMainMetaDataAllowed
            chkMainModifierMetaData.Enabled = False
            chkMainModifierNfo.Checked = mMainNFOAllowed
            chkMainModifierNfo.Enabled = False
            chkMainModifierPoster.Checked = mMainPosterAllowed
            chkMainModifierPoster.Enabled = False
            chkMainModifierTheme.Checked = mMainThemeAllowed
            chkMainModifierTheme.Enabled = False
            chkMainModifierTrailer.Checked = mMainTrailerAllowed
            chkMainModifierTrailer.Enabled = False
        Else
            chkMainModifierActorThumbs.Enabled = mMainActorThumbsAllowed
            chkMainModifierBanner.Enabled = mMainBannerAllowed
            chkMainModifierCharacterArt.Enabled = mMainCharacterArtAllowed
            chkMainModifierClearArt.Enabled = mMainClearArtAllowed
            chkMainModifierClearLogo.Enabled = mMainClearLogoAllowed
            chkMainModifierDiscArt.Enabled = mMainDiscArtAllowed
            chkMainModifierExtrafanarts.Enabled = mMainExtrafanartsAllowed
            chkMainModifierExtrathumbs.Enabled = mMainExtrathumbsAllowed
            chkMainModifierFanart.Enabled = mMainFanartAllowed
            chkMainModifierKeyart.Enabled = mMainKeyartAllowed
            chkMainModifierLandscape.Enabled = mMainLandscapeAllowed
            chkMainModifierMetaData.Enabled = mMainMetaDataAllowed
            chkMainModifierNfo.Enabled = mMainNFOAllowed
            chkMainModifierPoster.Enabled = mMainPosterAllowed
            chkMainModifierTheme.Enabled = mMainThemeAllowed
            chkMainModifierTrailer.Enabled = mMainTrailerAllowed
        End If

        'Main Options
        If chkMainModifierNfo.Checked Then
            gbMainScrapeOptions.Enabled = True
            If chkMainOptionsAll.Checked Then
                chkMainOptionsActors.Checked = oMainActorsAllowed
                chkMainOptionsActors.Enabled = False
                chkMainOptionsCertifications.Checked = oMainCertificationsAllowed
                chkMainOptionsCertifications.Enabled = False
                chkMainOptionsCollectionId.Checked = oMainCollectionIDAllowed
                chkMainOptionsCollectionId.Enabled = False
                chkMainOptionsCountries.Checked = oMainCountriesAllowed
                chkMainOptionsCountries.Enabled = False
                chkMainOptionsCreators.Checked = oMainCreatorsAllowed
                chkMainOptionsCreators.Enabled = False
                chkMainOptionsDirectors.Checked = oMainDirectorsAllowed
                chkMainOptionsDirectors.Enabled = False
                chkMainOptionsEpisodeGuideUrl.Checked = oMainEpisodeGuideURLAllowed
                chkMainOptionsEpisodeGuideUrl.Enabled = False
                chkMainOptionsGenres.Checked = oMainGenresAllowed
                chkMainOptionsGenres.Enabled = False
                chkMainOptionsMPAA.Checked = oMainMPAAAllowed
                chkMainOptionsMPAA.Enabled = False
                chkMainOptionsOriginalTitle.Checked = oMainOriginalTitleAllowed
                chkMainOptionsOriginalTitle.Enabled = False
                chkMainOptionsOutline.Checked = oMainOutlineAllowed
                chkMainOptionsOutline.Enabled = False
                chkMainOptionsPlot.Checked = oMainPlotAllowed
                chkMainOptionsPlot.Enabled = False
                chkMainOptionsPremiered.Checked = oMainPremieredAllowed
                chkMainOptionsPremiered.Enabled = False
                chkMainOptionsRating.Checked = oMainRatingAllowed
                chkMainOptionsRating.Enabled = False
                chkMainOptionsRuntime.Checked = oMainRuntimeAllowed
                chkMainOptionsRuntime.Enabled = False
                chkMainOptionsStatus.Checked = oMainStatusAllowed
                chkMainOptionsStatus.Enabled = False
                chkMainOptionsStudios.Checked = oMainStudiosAllowed
                chkMainOptionsStudios.Enabled = False
                chkMainOptionsTagline.Checked = oMainTaglineAllowed
                chkMainOptionsTagline.Enabled = False
                chkMainOptionsTitle.Checked = oMainTitleAllowed
                chkMainOptionsTitle.Enabled = False
                chkMainOptionsTop250.Checked = oMainTop250Allowed
                chkMainOptionsTop250.Enabled = False
                chkMainOptionsTrailer.Checked = oMainTrailerAllowed
                chkMainOptionsTrailer.Enabled = False
                chkMainOptionsWriters.Checked = oMainWritersAllowed
                chkMainOptionsWriters.Enabled = False
            Else
                chkMainOptionsActors.Enabled = oMainActorsAllowed
                chkMainOptionsCertifications.Enabled = oMainCertificationsAllowed
                chkMainOptionsCollectionId.Enabled = oMainCollectionIDAllowed
                chkMainOptionsCountries.Enabled = oMainCountriesAllowed
                chkMainOptionsCreators.Enabled = oMainCreatorsAllowed
                chkMainOptionsDirectors.Enabled = oMainDirectorsAllowed
                chkMainOptionsEpisodeGuideUrl.Enabled = oMainEpisodeGuideURLAllowed
                chkMainOptionsGenres.Enabled = oMainGenresAllowed
                chkMainOptionsMPAA.Enabled = oMainMPAAAllowed
                chkMainOptionsOriginalTitle.Enabled = oMainOriginalTitleAllowed
                chkMainOptionsOutline.Enabled = oMainOutlineAllowed
                chkMainOptionsPlot.Enabled = oMainPlotAllowed
                chkMainOptionsPremiered.Enabled = oMainPremieredAllowed
                chkMainOptionsRating.Enabled = oMainRatingAllowed
                chkMainOptionsRuntime.Enabled = oMainRuntimeAllowed
                chkMainOptionsStatus.Enabled = oMainStatusAllowed
                chkMainOptionsStudios.Enabled = oMainStudiosAllowed
                chkMainOptionsTagline.Enabled = oMainTaglineAllowed
                chkMainOptionsTitle.Enabled = oMainTitleAllowed
                chkMainOptionsTop250.Enabled = oMainTop250Allowed
                chkMainOptionsTrailer.Enabled = oMainTrailerAllowed
                chkMainOptionsWriters.Enabled = oMainWritersAllowed
            End If
        Else
            gbMainScrapeOptions.Enabled = False
        End If

        'Special Modifiers
        If chkSpecialModifierAll.Checked Then
            chkSpecialModifierWithEpisodes.Checked = True
            chkSpecialModifierWithEpisodes.Enabled = False
            chkSpecialModifierWithSeasons.Checked = True
            chkSpecialModifierWithSeasons.Enabled = False
        Else
            chkSpecialModifierWithEpisodes.Enabled = True
            chkSpecialModifierWithSeasons.Enabled = True
        End If

        'with Episodes
        If chkSpecialModifierWithEpisodes.Checked Then
            gbEpisodeScrapeModifiers.Enabled = True

            'Episode Modifiers
            If chkEpisodeModifierAll.Checked Then
                chkEpisodeModifierActorThumbs.Checked = mEpisodeActorThumbsAllowed
                chkEpisodeModifierActorThumbs.Enabled = False
                chkEpisodeModifierFanart.Checked = mEpisodeFanartAllowed
                chkEpisodeModifierFanart.Enabled = False
                chkEpisodeModifierMetaData.Checked = mEpisodeMetaDataAllowed
                chkEpisodeModifierMetaData.Enabled = False
                chkEpisodeModifierNfo.Checked = mEpisodeNFOAllowed
                chkEpisodeModifierNfo.Enabled = False
                chkEpisodeModifierPoster.Checked = mEpisodePosterAllowed
                chkEpisodeModifierPoster.Enabled = False
            Else
                chkEpisodeModifierActorThumbs.Enabled = mEpisodeActorThumbsAllowed
                chkEpisodeModifierFanart.Enabled = mEpisodeFanartAllowed
                chkEpisodeModifierMetaData.Enabled = mEpisodeMetaDataAllowed
                chkEpisodeModifierNfo.Enabled = mEpisodeNFOAllowed
                chkEpisodeModifierPoster.Enabled = mEpisodePosterAllowed
            End If

            'Episode Options
            If chkEpisodeModifierNfo.Checked Then
                gbEpisodeScrapeOptions.Enabled = True
                If chkEpisodeOptionsAll.Checked Then
                    chkEpisodeOptionsActors.Checked = oEpisodeActorsAllowed
                    chkEpisodeOptionsActors.Enabled = False
                    chkEpisodeOptionsAired.Checked = oEpisodeAiredAllowed
                    chkEpisodeOptionsAired.Enabled = False
                    chkEpisodeOptionsDirectors.Checked = oEpisodeDirectorsAllowed
                    chkEpisodeOptionsDirectors.Enabled = False
                    chkEpisodeOptionsGuestStars.Checked = oEpisodeGuestStarsAllowed
                    chkEpisodeOptionsGuestStars.Enabled = False
                    chkEpisodeOptionsOriginalTitle.Checked = oEpisodeOriginalTitleAllowed
                    chkEpisodeOptionsOriginalTitle.Enabled = False
                    chkEpisodeOptionsPlot.Checked = oEpisodePlotAllowed
                    chkEpisodeOptionsPlot.Enabled = False
                    chkEpisodeOptionsRating.Checked = oEpisodeRatingAllowed
                    chkEpisodeOptionsRating.Enabled = False
                    chkEpisodeOptionsRuntime.Checked = oEpisodeRuntimeAllowed
                    chkEpisodeOptionsRuntime.Enabled = False
                    chkEpisodeOptionsTitle.Checked = oEpisodeTitleAllowed
                    chkEpisodeOptionsTitle.Enabled = False
                    chkEpisodeOptionsWriters.Checked = oEpisodeWritersAllowed
                    chkEpisodeOptionsWriters.Enabled = False
                Else
                    chkEpisodeOptionsActors.Enabled = oEpisodeActorsAllowed
                    chkEpisodeOptionsAired.Enabled = oEpisodeAiredAllowed
                    chkEpisodeOptionsDirectors.Enabled = oEpisodeDirectorsAllowed
                    chkEpisodeOptionsGuestStars.Enabled = oEpisodeGuestStarsAllowed
                    chkEpisodeOptionsOriginalTitle.Enabled = oEpisodeOriginalTitleAllowed
                    chkEpisodeOptionsPlot.Enabled = oEpisodePlotAllowed
                    chkEpisodeOptionsRating.Enabled = oEpisodeRatingAllowed
                    chkEpisodeOptionsRuntime.Enabled = oEpisodeRuntimeAllowed
                    chkEpisodeOptionsTitle.Enabled = oEpisodeTitleAllowed
                    chkEpisodeOptionsWriters.Enabled = oEpisodeWritersAllowed
                End If
            Else
                gbEpisodeScrapeOptions.Enabled = False
            End If
        Else
            gbEpisodeScrapeModifiers.Enabled = False
            gbEpisodeScrapeOptions.Enabled = False
        End If

        'with Seasons
        If chkSpecialModifierWithSeasons.Checked Then
            gbSeasonScrapeModifiers.Enabled = True

            'Season Modifiers
            If chkSeasonModifierAll.Checked Then
                chkSeasonModifierBanner.Checked = mSeasonBannerAllowed
                chkSeasonModifierBanner.Enabled = False
                chkSeasonModifierFanart.Checked = mSeasonFanartAllowed
                chkSeasonModifierFanart.Enabled = False
                chkSeasonModifierLandscape.Checked = mSeasonLandscapeAllowed
                chkSeasonModifierLandscape.Enabled = False
                chkSeasonModifierPoster.Checked = mSeasonPosterAllowed
                chkSeasonModifierPoster.Enabled = False
            Else
                chkSeasonModifierBanner.Enabled = mSeasonBannerAllowed
                chkSeasonModifierFanart.Enabled = mSeasonFanartAllowed
                chkSeasonModifierLandscape.Enabled = mSeasonLandscapeAllowed
                chkSeasonModifierPoster.Enabled = mSeasonPosterAllowed
            End If

            'Season Options
            If chkMainModifierNfo.Checked Then 'TODO: check. Atm we save the season infos to tv show NFO
                gbSeasonScrapeOptions.Enabled = True
                If chkSeasonOptionsAll.Checked Then
                    chkSeasonOptionsAired.Checked = oSeasonAiredAllowed
                    chkSeasonOptionsAired.Enabled = False
                    chkSeasonOptionsPlot.Checked = oSeasonPlotAllowed
                    chkSeasonOptionsPlot.Enabled = False
                    chkSeasonOptionsTitle.Checked = oSeasonTitleAllowed
                    chkSeasonOptionsTitle.Enabled = False
                Else
                    chkSeasonOptionsAired.Enabled = oSeasonAiredAllowed
                    chkSeasonOptionsPlot.Enabled = oSeasonPlotAllowed
                    chkSeasonOptionsTitle.Enabled = oSeasonTitleAllowed
                End If
            Else
                gbSeasonScrapeOptions.Enabled = False
            End If
        Else
            gbSeasonScrapeModifiers.Enabled = False
            gbSeasonScrapeOptions.Enabled = False
        End If

        'Scrape Modifiers
        CustomUpdater.ScrapeModifiers.Episodes.Actorthumbs = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierActorThumbs.Checked
        CustomUpdater.ScrapeModifiers.Episodes.Fanart = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierFanart.Checked
        CustomUpdater.ScrapeModifiers.Episodes.Metadata = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierMetaData.Checked
        CustomUpdater.ScrapeModifiers.Episodes.Information = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked
        CustomUpdater.ScrapeModifiers.Episodes.Poster = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierPoster.Checked
        CustomUpdater.ScrapeModifiers.Actorthumbs = chkMainModifierActorThumbs.Checked
        CustomUpdater.ScrapeModifiers.Banner = chkMainModifierBanner.Checked
        CustomUpdater.ScrapeModifiers.Characterart = chkMainModifierCharacterArt.Checked
        CustomUpdater.ScrapeModifiers.Clearart = chkMainModifierClearArt.Checked
        CustomUpdater.ScrapeModifiers.Clearlogo = chkMainModifierClearLogo.Checked
        CustomUpdater.ScrapeModifiers.Discart = chkMainModifierDiscArt.Checked
        CustomUpdater.ScrapeModifiers.Fanart = chkMainModifierFanart.Checked
        CustomUpdater.ScrapeModifiers.Extrathumbs = chkMainModifierExtrathumbs.Checked
        CustomUpdater.ScrapeModifiers.Extrafanarts = chkMainModifierExtrafanarts.Checked
        CustomUpdater.ScrapeModifiers.Fanart = chkMainModifierFanart.Checked
        CustomUpdater.ScrapeModifiers.Keyart = chkMainModifierKeyart.Checked
        CustomUpdater.ScrapeModifiers.Landscape = chkMainModifierLandscape.Checked
        CustomUpdater.ScrapeModifiers.Metadata = chkMainModifierMetaData.Checked
        CustomUpdater.ScrapeModifiers.Information = chkMainModifierNfo.Checked
        CustomUpdater.ScrapeModifiers.Poster = chkMainModifierPoster.Checked
        CustomUpdater.ScrapeModifiers.Theme = chkMainModifierTheme.Checked
        CustomUpdater.ScrapeModifiers.Trailer = chkMainModifierTrailer.Checked
        CustomUpdater.ScrapeModifiers.Seasons.Banner = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierBanner.Checked
        CustomUpdater.ScrapeModifiers.Seasons.Fanart = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierFanart.Checked
        CustomUpdater.ScrapeModifiers.Seasons.Landscape = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierLandscape.Checked
        CustomUpdater.ScrapeModifiers.Seasons.Poster = chkSpecialModifierWithSeasons.Checked AndAlso chkSeasonModifierPoster.Checked
        CustomUpdater.ScrapeModifiers.withEpisodes = chkSpecialModifierWithEpisodes.Checked
        CustomUpdater.ScrapeModifiers.withSeasons = chkSpecialModifierWithSeasons.Checked
        CustomUpdater.ScrapeModifiers.AllSeasonsBanner = CustomUpdater.ScrapeModifiers.Seasons.Banner
        CustomUpdater.ScrapeModifiers.AllSeasonsFanart = CustomUpdater.ScrapeModifiers.Seasons.Fanart
        CustomUpdater.ScrapeModifiers.AllSeasonsLandscape = CustomUpdater.ScrapeModifiers.Seasons.Landscape
        CustomUpdater.ScrapeModifiers.AllSeasonsPoster = CustomUpdater.ScrapeModifiers.Seasons.Poster

        'Scrape Options
        CustomUpdater.ScrapeOptions.Episodes.Actors = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsActors.Checked
        CustomUpdater.ScrapeOptions.Episodes.Aired = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsAired.Checked
        CustomUpdater.ScrapeOptions.Episodes.Credits = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.Episodes.Directors = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.Episodes.GuestStars = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsGuestStars.Checked
        CustomUpdater.ScrapeOptions.Episodes.OriginalTitle = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsOriginalTitle.Checked
        CustomUpdater.ScrapeOptions.Episodes.Plot = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.Episodes.Ratings = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsRating.Checked
        CustomUpdater.ScrapeOptions.Episodes.Runtime = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.Episodes.Title = chkSpecialModifierWithEpisodes.Checked AndAlso chkEpisodeModifierNfo.Checked AndAlso chkEpisodeOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.Actors = chkMainModifierNfo.Checked AndAlso chkMainOptionsActors.Checked
        CustomUpdater.ScrapeOptions.Certifications = chkMainModifierNfo.Checked AndAlso chkMainOptionsCertifications.Checked
        CustomUpdater.ScrapeOptions.Collection = chkMainModifierNfo.Checked AndAlso chkMainOptionsCollectionId.Checked
        CustomUpdater.ScrapeOptions.Countries = chkMainModifierNfo.Checked AndAlso chkMainOptionsCountries.Checked
        CustomUpdater.ScrapeOptions.Creators = chkMainModifierNfo.Checked AndAlso chkMainOptionsCreators.Checked
        CustomUpdater.ScrapeOptions.Credits = chkMainModifierNfo.Checked AndAlso chkMainOptionsWriters.Checked
        CustomUpdater.ScrapeOptions.Directors = chkMainModifierNfo.Checked AndAlso chkMainOptionsDirectors.Checked
        CustomUpdater.ScrapeOptions.EpisodeGuideURL = chkMainModifierNfo.Checked AndAlso chkMainOptionsEpisodeGuideUrl.Checked
        CustomUpdater.ScrapeOptions.Genres = chkMainModifierNfo.Checked AndAlso chkMainOptionsGenres.Checked
        CustomUpdater.ScrapeOptions.MPAA = chkMainModifierNfo.Checked AndAlso chkMainOptionsMPAA.Checked
        CustomUpdater.ScrapeOptions.OriginalTitle = chkMainModifierNfo.Checked AndAlso chkMainOptionsOriginalTitle.Checked
        CustomUpdater.ScrapeOptions.Outline = chkMainModifierNfo.Checked AndAlso chkMainOptionsOutline.Checked
        CustomUpdater.ScrapeOptions.Plot = chkMainModifierNfo.Checked AndAlso chkMainOptionsPlot.Checked
        CustomUpdater.ScrapeOptions.Premiered = chkMainModifierNfo.Checked AndAlso chkMainOptionsPremiered.Checked
        CustomUpdater.ScrapeOptions.Ratings = chkMainModifierNfo.Checked AndAlso chkMainOptionsRating.Checked
        CustomUpdater.ScrapeOptions.Runtime = chkMainModifierNfo.Checked AndAlso chkMainOptionsRuntime.Checked
        CustomUpdater.ScrapeOptions.Status = chkMainModifierNfo.Checked AndAlso chkMainOptionsStatus.Checked
        CustomUpdater.ScrapeOptions.Studios = chkMainModifierNfo.Checked AndAlso chkMainOptionsStudios.Checked
        CustomUpdater.ScrapeOptions.Tagline = chkMainModifierNfo.Checked AndAlso chkMainOptionsTagline.Checked
        CustomUpdater.ScrapeOptions.Title = chkMainModifierNfo.Checked AndAlso chkMainOptionsTitle.Checked
        CustomUpdater.ScrapeOptions.Top250 = chkMainModifierNfo.Checked AndAlso chkMainOptionsTop250.Checked
        CustomUpdater.ScrapeOptions.TrailerLink = chkMainModifierNfo.Checked AndAlso chkMainOptionsTrailer.Checked
        CustomUpdater.ScrapeOptions.Seasons.Aired = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNfo.Checked AndAlso chkSeasonOptionsAired.Checked   'TODO: check. Atm we save the season infos to tv show NFO
        CustomUpdater.ScrapeOptions.Seasons.Plot = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNfo.Checked AndAlso chkSeasonOptionsPlot.Checked     'TODO: check. Atm we save the season infos to tv show NFO
        CustomUpdater.ScrapeOptions.Seasons.Title = chkSpecialModifierWithSeasons.Checked AndAlso chkMainModifierNfo.Checked AndAlso chkSeasonOptionsTitle.Checked     'TODO: check. Atm we save the season infos to tv show NFO

        If CustomUpdater.ScrapeModifiers.Episodes.Actorthumbs OrElse
            CustomUpdater.ScrapeModifiers.Episodes.Fanart OrElse
            CustomUpdater.ScrapeModifiers.Episodes.Metadata OrElse
            CustomUpdater.ScrapeModifiers.Episodes.Poster OrElse
            CustomUpdater.ScrapeModifiers.Episodes.Subtitles OrElse
            CustomUpdater.ScrapeModifiers.Actorthumbs OrElse
            CustomUpdater.ScrapeModifiers.Banner OrElse
            CustomUpdater.ScrapeModifiers.Characterart OrElse
            CustomUpdater.ScrapeModifiers.Clearart OrElse
            CustomUpdater.ScrapeModifiers.Clearlogo OrElse
            CustomUpdater.ScrapeModifiers.Discart OrElse
            CustomUpdater.ScrapeModifiers.Extrafanarts OrElse
            CustomUpdater.ScrapeModifiers.Extrathumbs OrElse
            CustomUpdater.ScrapeModifiers.Fanart OrElse
            CustomUpdater.ScrapeModifiers.Keyart OrElse
            CustomUpdater.ScrapeModifiers.Landscape OrElse
            CustomUpdater.ScrapeModifiers.Metadata OrElse
            CustomUpdater.ScrapeModifiers.Poster OrElse
            CustomUpdater.ScrapeModifiers.Subtitles OrElse
            CustomUpdater.ScrapeModifiers.Theme OrElse
            CustomUpdater.ScrapeModifiers.Trailer OrElse
            CustomUpdater.ScrapeModifiers.Seasons.Banner OrElse
            CustomUpdater.ScrapeModifiers.Seasons.Fanart OrElse
            CustomUpdater.ScrapeModifiers.Seasons.Landscape OrElse
            CustomUpdater.ScrapeModifiers.Seasons.Poster Then
            btnOK.Enabled = True
        ElseIf CustomUpdater.ScrapeModifiers.Episodes.Information AndAlso (
            CustomUpdater.ScrapeOptions.Episodes.Actors OrElse
            CustomUpdater.ScrapeOptions.Episodes.Aired OrElse
            CustomUpdater.ScrapeOptions.Episodes.Credits OrElse
            CustomUpdater.ScrapeOptions.Episodes.Directors OrElse
            CustomUpdater.ScrapeOptions.Episodes.GuestStars OrElse
            CustomUpdater.ScrapeOptions.Episodes.OriginalTitle OrElse
            CustomUpdater.ScrapeOptions.Episodes.Plot OrElse
            CustomUpdater.ScrapeOptions.Episodes.Ratings OrElse
            CustomUpdater.ScrapeOptions.Episodes.Runtime OrElse
            CustomUpdater.ScrapeOptions.Episodes.Title) Then
            btnOK.Enabled = True
        ElseIf CustomUpdater.ScrapeModifiers.Information AndAlso (
            CustomUpdater.ScrapeOptions.Actors OrElse
            CustomUpdater.ScrapeOptions.Certifications OrElse
            CustomUpdater.ScrapeOptions.Collection OrElse
            CustomUpdater.ScrapeOptions.Countries OrElse
            CustomUpdater.ScrapeOptions.Creators OrElse
            CustomUpdater.ScrapeOptions.Credits OrElse
            CustomUpdater.ScrapeOptions.Directors OrElse
            CustomUpdater.ScrapeOptions.EpisodeGuideURL OrElse
            CustomUpdater.ScrapeOptions.Genres OrElse
            CustomUpdater.ScrapeOptions.MPAA OrElse
            CustomUpdater.ScrapeOptions.OriginalTitle OrElse
            CustomUpdater.ScrapeOptions.Outline OrElse
            CustomUpdater.ScrapeOptions.Plot OrElse
            CustomUpdater.ScrapeOptions.Premiered OrElse
            CustomUpdater.ScrapeOptions.Ratings OrElse
            CustomUpdater.ScrapeOptions.Runtime OrElse
            CustomUpdater.ScrapeOptions.Status OrElse
            CustomUpdater.ScrapeOptions.Studios OrElse
            CustomUpdater.ScrapeOptions.Tagline OrElse
            CustomUpdater.ScrapeOptions.Tags OrElse
            CustomUpdater.ScrapeOptions.Title OrElse
            CustomUpdater.ScrapeOptions.Top250 OrElse
            CustomUpdater.ScrapeOptions.TrailerLink OrElse
            CustomUpdater.ScrapeOptions.Seasons.Aired OrElse
            CustomUpdater.ScrapeOptions.Seasons.Plot) Then
            btnOK.Enabled = True
        Else
            btnOK.Enabled = False
        End If
    End Sub

    Private Sub SetParameters()
        Dim NameID As String = String.Empty
        Dim NameTable As String = String.Empty

        With Master.eSettings
            Select Case _ContentType
                Case Enums.ContentType.Movie
                    NameID = "idMovie"
                    NameTable = "movie"
                    gbEpisodeScrapeModifiers.Visible = False
                    gbEpisodeScrapeOptions.Visible = False
                    gbSeasonScrapeModifiers.Visible = False
                    gbSeasonScrapeOptions.Visible = False
                    gbSpecialScrapeModifiers.Visible = False

                    mEpisodeActorThumbsAllowed = False
                    mEpisodeFanartAllowed = False
                    mEpisodeMetaDataAllowed = False
                    mEpisodeNFOAllowed = False
                    mEpisodePosterAllowed = False
                    mMainActorThumbsAllowed = .MovieActorthumbsAnyEnabled
                    mMainBannerAllowed = .MovieBannerAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = False
                    mMainClearArtAllowed = .MovieClearArtAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .MovieClearLogoAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = .MovieDiscArtAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainDiscArt)
                    mMainExtrafanartsAllowed = .MovieExtrafanartsAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainExtrathumbsAllowed = .MovieExtrathumbsAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainFanartAllowed = .MovieFanartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainFanart)
                    mMainKeyartAllowed = .MovieKeyartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainKeyart)
                    mMainLandscapeAllowed = .MovieLandscapeAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = .MovieScraperMetaDataScan
                    mMainNFOAllowed = .MovieNFOAnyEnabled
                    mMainPosterAllowed = .MoviePosterAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movie(Enums.ModifierType.MainPoster)
                    mMainThemeAllowed = .MovieThemeAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Theme_Movie()
                    mMainTrailerAllowed = .MovieTrailerAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Trailer_Movie()
                    mSeasonBannerAllowed = False
                    mSeasonFanartAllowed = False
                    mSeasonLandscapeAllowed = False
                    mSeasonPosterAllowed = False

                    With Master.eSettings.Movie.InformationSettings
                        oEpisodeActorsAllowed = False
                        oEpisodeAiredAllowed = False
                        oEpisodeDirectorsAllowed = False
                        oEpisodeGuestStarsAllowed = False
                        oEpisodeOriginalTitleAllowed = False
                        oEpisodePlotAllowed = False
                        oEpisodeRatingAllowed = False
                        oEpisodeRuntimeAllowed = False
                        oEpisodeTitleAllowed = False
                        oEpisodeWritersAllowed = False
                        oMainActorsAllowed = .Actors.Enabled
                        oMainCertificationsAllowed = .Certifications.Enabled
                        oMainCollectionIDAllowed = .Collection.Enabled
                        oMainCountriesAllowed = .Countries.Enabled
                        oMainCreatorsAllowed = False
                        oMainDirectorsAllowed = .Directors.Enabled
                        oMainEpisodeGuideURLAllowed = False
                        oMainGenresAllowed = .Genres.Enabled
                        oMainMPAAAllowed = .MPAA.Enabled
                        oMainOriginalTitleAllowed = .OriginalTitle.Enabled
                        oMainOutlineAllowed = .Outline.Enabled
                        oMainPlotAllowed = .Plot.Enabled
                        oMainPremieredAllowed = .Premiered.Enabled
                        oMainProducersAllowed = False
                        oMainRatingAllowed = .Ratings.Enabled
                        oMainRuntimeAllowed = .Runtime.Enabled
                        oMainStatusAllowed = False
                        oMainStudiosAllowed = .Status.Enabled
                        oMainTaglineAllowed = .Tagline.Enabled
                        oMainTitleAllowed = .Title.Enabled
                        oMainTop250Allowed = .Top250.Enabled
                        oMainTrailerAllowed = .TrailerLink.Enabled
                        oMainWritersAllowed = .Creators.Enabled
                        oSeasonAiredAllowed = False
                        oSeasonPlotAllowed = False
                        oSeasonTitleAllowed = False
                    End With

                    chkMainModifierAll.Checked = True
                    chkMainOptionsAll.Checked = True

                    rbScrapeType_Filter.Enabled = Addons.Instance.RuntimeObjects.MediaListMovies.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), Addons.Instance.RuntimeObjects.MediaListMovies.Rows.Count)
                    rbScrapeType_Selected.Enabled = Addons.Instance.RuntimeObjects.MediaListMovies.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), Addons.Instance.RuntimeObjects.MediaListMovies.SelectedRows.Count)

                Case Enums.ContentType.Movieset
                    NameID = "idSet"
                    NameTable = "sets"
                    gbEpisodeScrapeModifiers.Visible = False
                    gbEpisodeScrapeOptions.Visible = False
                    gbSeasonScrapeModifiers.Visible = False
                    gbSeasonScrapeOptions.Visible = False
                    gbSpecialScrapeModifiers.Visible = False

                    mEpisodeActorThumbsAllowed = False
                    mEpisodeFanartAllowed = False
                    mEpisodeMetaDataAllowed = False
                    mEpisodeNFOAllowed = False
                    mEpisodePosterAllowed = False
                    mMainActorThumbsAllowed = False
                    mMainBannerAllowed = .MovieSetBannerAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movieset(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = False
                    mMainClearArtAllowed = .MovieSetClearArtAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movieset(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .MovieSetClearLogoAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movieset(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = .MovieSetDiscArtAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movieset(Enums.ModifierType.MainDiscArt)
                    mMainExtrafanartsAllowed = False
                    mMainExtrathumbsAllowed = False
                    mMainFanartAllowed = .MovieSetFanartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movieset(Enums.ModifierType.MainFanart)
                    mMainKeyartAllowed = .MovieSetKeyartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movieset(Enums.ModifierType.MainKeyart)
                    mMainLandscapeAllowed = .MovieSetLandscapeAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movieset(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = False
                    mMainNFOAllowed = .MovieSetNFOAnyEnabled
                    mMainPosterAllowed = .MovieSetPosterAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_Movieset(Enums.ModifierType.MainPoster)
                    mMainThemeAllowed = False
                    mMainTrailerAllowed = False
                    mSeasonBannerAllowed = False
                    mSeasonFanartAllowed = False
                    mSeasonLandscapeAllowed = False
                    mSeasonPosterAllowed = False

                    With Master.eSettings.Movieset.InformationSettings
                        oEpisodeActorsAllowed = False
                        oEpisodeAiredAllowed = False
                        oEpisodeDirectorsAllowed = False
                        oEpisodeGuestStarsAllowed = False
                        oEpisodeOriginalTitleAllowed = False
                        oEpisodePlotAllowed = False
                        oEpisodeRatingAllowed = False
                        oEpisodeRuntimeAllowed = False
                        oEpisodeTitleAllowed = False
                        oEpisodeWritersAllowed = False
                        oMainActorsAllowed = False
                        oMainCertificationsAllowed = False
                        oMainCollectionIDAllowed = False
                        oMainCountriesAllowed = False
                        oMainCreatorsAllowed = False
                        oMainDirectorsAllowed = False
                        oMainEpisodeGuideURLAllowed = False
                        oMainGenresAllowed = False
                        oMainMPAAAllowed = False
                        oMainOriginalTitleAllowed = False
                        oMainOutlineAllowed = False
                        oMainPlotAllowed = .Plot.Enabled
                        oMainPremieredAllowed = False
                        oMainProducersAllowed = False
                        oMainRatingAllowed = False
                        oMainRuntimeAllowed = False
                        oMainStatusAllowed = False
                        oMainStudiosAllowed = False
                        oMainTaglineAllowed = False
                        oMainTitleAllowed = .Title.Enabled
                        oMainTop250Allowed = False
                        oMainTrailerAllowed = False
                        oMainWritersAllowed = False
                        oSeasonAiredAllowed = False
                        oSeasonPlotAllowed = False
                        oSeasonTitleAllowed = False
                    End With

                    chkMainModifierAll.Checked = True
                    chkMainOptionsAll.Checked = True

                    rbScrapeType_Filter.Enabled = Addons.Instance.RuntimeObjects.MediaListMovieSets.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), Addons.Instance.RuntimeObjects.MediaListMovieSets.Rows.Count)
                    rbScrapeType_Selected.Enabled = Addons.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), Addons.Instance.RuntimeObjects.MediaListMovieSets.SelectedRows.Count)

                Case Enums.ContentType.TV
                    NameID = "idShow"
                    NameTable = "tvshow"

                    mEpisodeActorThumbsAllowed = .TVEpisodeActorThumbsAnyEnabled
                    mEpisodeFanartAllowed = .TVEpisodeFanartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodeFanart)
                    mEpisodeMetaDataAllowed = .TVScraperMetaDataScan
                    mEpisodeNFOAllowed = .TVEpisodeNFOAnyEnabled
                    mEpisodePosterAllowed = .TVEpisodePosterAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.EpisodePoster)
                    mMainActorThumbsAllowed = .TVShowActorThumbsAnyEnabled
                    mMainBannerAllowed = .TVShowBannerAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainBanner)
                    mMainCharacterArtAllowed = .TVShowCharacterArtAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainCharacterArt)
                    mMainClearArtAllowed = .TVShowClearArtAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearArt)
                    mMainClearLogoAllowed = .TVShowClearLogoAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainClearLogo)
                    mMainDiscArtAllowed = False
                    mMainExtrafanartsAllowed = .TVShowExtrafanartsAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mMainExtrathumbsAllowed = False
                    mMainFanartAllowed = .TVShowFanartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainFanart)
                    mMainKeyartAllowed = .TVShowKeyartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainKeyart)
                    mMainLandscapeAllowed = .TVShowLandscapeAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainLandscape)
                    mMainMetaDataAllowed = False
                    mMainNFOAllowed = .TVShowNFOAnyEnabled
                    mMainPosterAllowed = .TVShowPosterAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.MainPoster)
                    mMainThemeAllowed = .TvShowThemeAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Theme_TV()
                    mMainTrailerAllowed = False
                    mSeasonBannerAllowed = .TVSeasonBannerAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonBanner)
                    mSeasonFanartAllowed = .TVSeasonFanartAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonFanart)
                    mSeasonLandscapeAllowed = .TVSeasonLandscapeAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonLandscape)
                    mSeasonPosterAllowed = .TVSeasonPosterAnyEnabled AndAlso Addons.Instance.ScraperWithCapabilityAnyEnabled_Image_TV(Enums.ModifierType.SeasonPoster)

                    With Master.eSettings.TVEpisode.InformationSettings
                        oEpisodeActorsAllowed = .Actors.Enabled
                        oEpisodeAiredAllowed = .Aired.Enabled
                        oEpisodeDirectorsAllowed = .Directors.Enabled
                        oEpisodeGuestStarsAllowed = .GuestStars.Enabled
                        oEpisodeOriginalTitleAllowed = .OriginalTitle.Enabled
                        oEpisodePlotAllowed = .Plot.Enabled
                        oEpisodeRatingAllowed = .Ratings.Enabled
                        oEpisodeRuntimeAllowed = .Runtime.Enabled
                        oEpisodeTitleAllowed = .Title.Enabled
                        oEpisodeWritersAllowed = .Credits.Enabled
                    End With
                    With Master.eSettings.TVShow.InformationSettings
                        oMainActorsAllowed = .Actors.Enabled
                        oMainCertificationsAllowed = .Certifications.Enabled
                        oMainCollectionIDAllowed = False
                        oMainCountriesAllowed = .Countries.Enabled
                        oMainCreatorsAllowed = .Creators.Enabled
                        oMainDirectorsAllowed = False
                        oMainEpisodeGuideURLAllowed = .EpisodeGuideURL.Enabled
                        oMainGenresAllowed = .Genres.Enabled
                        oMainMPAAAllowed = .MPAA.Enabled
                        oMainOriginalTitleAllowed = .OriginalTitle.Enabled
                        oMainOutlineAllowed = False
                        oMainPlotAllowed = .Plot.Enabled
                        oMainPremieredAllowed = .Premiered.Enabled
                        oMainProducersAllowed = False
                        oMainRatingAllowed = .Ratings.Enabled
                        oMainRuntimeAllowed = .Runtime.Enabled
                        oMainStatusAllowed = .Status.Enabled
                        oMainStudiosAllowed = .Studios.Enabled
                        oMainTaglineAllowed = .Tagline.Enabled
                        oMainTitleAllowed = .Title.Enabled
                        oMainTop250Allowed = False
                        oMainTrailerAllowed = False
                        oMainWritersAllowed = False
                    End With
                    With Master.eSettings.TVSeason.InformationSettings
                        oSeasonAiredAllowed = .Aired.Enabled
                        oSeasonPlotAllowed = .Plot.Enabled
                        oSeasonTitleAllowed = .Title.Enabled
                    End With

                    chkMainModifierAll.Checked = True
                    chkMainOptionsAll.Checked = True
                    chkSpecialModifierAll.Checked = True
                    chkEpisodeModifierAll.Checked = True
                    chkEpisodeOptionsAll.Checked = True
                    chkSeasonModifierAll.Checked = True
                    chkSeasonOptionsAll.Checked = True

                    rbScrapeType_Filter.Enabled = Addons.Instance.RuntimeObjects.MediaListTVShows.Rows.Count > 0
                    rbScrapeType_Filter.Text = String.Format(String.Concat(Master.eLang.GetString(624, "Current Filter"), " ({0})"), Addons.Instance.RuntimeObjects.MediaListTVShows.Rows.Count)
                    rbScrapeType_Selected.Enabled = Addons.Instance.RuntimeObjects.MediaListTVShows.SelectedRows.Count > 0
                    rbScrapeType_Selected.Text = String.Format(String.Concat(Master.eLang.GetString(1076, "Selected"), " ({0})"), Addons.Instance.RuntimeObjects.MediaListTVShows.SelectedRows.Count)
            End Select
        End With

        'check if we have "New" or "Marked" medias
        If Not String.IsNullOrEmpty(NameID) AndAlso Not String.IsNullOrEmpty(NameTable) Then
            Using SQLNewcommand As SQLite.SQLiteCommand = Master.DB.MyVideosDBConn.CreateCommand()
                SQLNewcommand.CommandText = String.Format("SELECT COUNT({0}) AS ncount FROM {1} WHERE new = 1;", NameID, NameTable)
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    rbScrapeType_New.Enabled = Convert.ToInt32(SQLcount("ncount")) > 0
                    rbScrapeType_New.Text = String.Format(String.Concat(Master.eLang.GetString(47, "New"), " ({0})"), Convert.ToInt32(SQLcount("ncount")))
                End Using

                SQLNewcommand.CommandText = String.Format("SELECT COUNT({0}) AS mcount FROM {1} WHERE mark = 1;", NameID, NameTable)
                Using SQLcount As SQLite.SQLiteDataReader = SQLNewcommand.ExecuteReader()
                    SQLcount.Read()
                    rbScrapeType_Marked.Enabled = Convert.ToInt32(SQLcount("mcount")) > 0
                    rbScrapeType_Marked.Text = String.Format(String.Concat(Master.eLang.GetString(48, "Marked"), " ({0})"), Convert.ToInt32(SQLcount("mcount")))
                End Using
            End Using
        End If
    End Sub

    Private Sub rbUpdateModifier_All_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_All.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.All
        End Select
    End Sub

    Private Sub rbUpdateModifier_Filter_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_Filter.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
        End Select
    End Sub

    Private Sub rbUpdateModifier_Marked_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_Marked.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
        End Select
    End Sub

    Private Sub rbUpdateModifier_Missing_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_Missing.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.MissingContent
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.MissingContent
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.MissingContent
        End Select
    End Sub

    Private Sub rbUpdateModifier_New_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_New.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
        End Select
    End Sub

    Private Sub rbUpdateModifier_Selected_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_Selected.CheckedChanged
        Select Case True
            Case rbScrapeType_Ask.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
            Case rbScrapeType_Auto.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
            Case rbScrapeType_Skip.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
        End Select
    End Sub

    Private Sub rbUpdate_Ask_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_Ask.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.MissingContent
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Ask
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
        End Select
    End Sub

    Private Sub rbUpdate_Auto_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_Auto.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.MissingContent
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Auto
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
        End Select
    End Sub

    Private Sub rbUpdate_Skip_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbScrapeType_Skip.CheckedChanged
        Select Case True
            Case rbScrapeType_All.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.All
            Case rbScrapeType_Filter.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Filtered
            Case rbScrapeType_Marked.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Marked
            Case rbScrapeType_Missing.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.MissingContent
            Case rbScrapeType_New.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.[New]
            Case rbScrapeType_Selected.Checked
                CustomUpdater.ScrapeType = Enums.ScrapeType.Skip
                CustomUpdater.SelectionType = Enums.SelectionType.Selected
        End Select
    End Sub

    Private Sub CheckBoxes_Click(sender As Object, e As EventArgs) Handles _
        chkEpisodeModifierActorThumbs.Click,
        chkEpisodeModifierAll.Click,
        chkEpisodeModifierFanart.Click,
        chkEpisodeModifierMetaData.Click,
        chkEpisodeModifierNfo.Click,
        chkEpisodeModifierPoster.Click,
        chkEpisodeOptionsActors.Click,
        chkEpisodeOptionsAired.Click,
        chkEpisodeOptionsAll.Click,
        chkEpisodeOptionsDirectors.Click,
        chkEpisodeOptionsGuestStars.Click,
        chkEpisodeOptionsOriginalTitle.Click,
        chkEpisodeOptionsPlot.Click,
        chkEpisodeOptionsRating.Click,
        chkEpisodeOptionsRuntime.Click,
        chkEpisodeOptionsTitle.Click,
        chkEpisodeOptionsWriters.Click,
        chkMainModifierActorThumbs.Click,
        chkMainModifierAll.Click,
        chkMainModifierBanner.Click,
        chkMainModifierCharacterArt.Click,
        chkMainModifierClearArt.Click,
        chkMainModifierClearLogo.Click,
        chkMainModifierDiscArt.Click,
        chkMainModifierExtrafanarts.Click,
        chkMainModifierExtrathumbs.Click,
        chkMainModifierFanart.Click,
        chkMainModifierKeyart.Click,
        chkMainModifierLandscape.Click,
        chkMainModifierMetaData.Click,
        chkMainModifierNfo.Click,
        chkMainModifierPoster.Click,
        chkMainModifierTheme.Click,
        chkMainModifierTrailer.Click,
        chkMainOptionsActors.Click,
        chkMainOptionsAll.Click,
        chkMainOptionsCertifications.Click,
        chkMainOptionsCollectionId.Click,
        chkMainOptionsCountries.Click,
        chkMainOptionsCreators.Click,
        chkMainOptionsDirectors.Click,
        chkMainOptionsEpisodeGuideUrl.Click,
        chkMainOptionsGenres.Click,
        chkMainOptionsMPAA.Click,
        chkMainOptionsOriginalTitle.Click,
        chkMainOptionsOutline.Click,
        chkMainOptionsPlot.Click,
        chkMainOptionsPremiered.Click,
        chkMainOptionsRating.Click,
        chkMainOptionsRuntime.Click,
        chkMainOptionsStatus.Click,
        chkMainOptionsStudios.Click,
        chkMainOptionsTagline.Click,
        chkMainOptionsTitle.Click,
        chkMainOptionsTop250.Click,
        chkMainOptionsTrailer.Click,
        chkMainOptionsWriters.Click,
        chkSeasonModifierAll.Click,
        chkSeasonModifierBanner.Click,
        chkSeasonModifierFanart.Click,
        chkSeasonModifierLandscape.Click,
        chkSeasonModifierPoster.Click,
        chkSeasonOptionsAired.Click,
        chkSeasonOptionsAll.Click,
        chkSeasonOptionsPlot.Click,
        chkSeasonOptionsTitle.Click,
        chkSpecialModifierAll.Click,
        chkSpecialModifierWithEpisodes.Click,
        chkSpecialModifierWithSeasons.Click

        CheckEnable()
    End Sub

#End Region 'Methods

End Class