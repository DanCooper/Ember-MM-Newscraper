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

Public Class frmMovie_Data
    Implements Interfaces.IMasterSettingsPanel

#Region "Fields"

    Private MovieMeta As New List(Of Settings.MetadataPerType)
    Private _TempTagsWhitelist As New ExtendedListOfString

#End Region 'Fields

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.IMasterSettingsPanel.NeedsReload_MovieSet
    Public Event NeedsReload_TVEpisode() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.IMasterSettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IMasterSettingsPanel.SettingsChanged

#End Region 'Events 

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
        Setup()
    End Sub

#End Region 'Constructors 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.IMasterSettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Function InjectSettingsPanel() As Containers.SettingsPanel Implements Interfaces.IMasterSettingsPanel.InjectSettingsPanel
        Settings_Load()

        Return New Containers.SettingsPanel With {
            .Contains = Enums.SettingsPanelType.MovieData,
            .ImageIndex = 3,
            .Order = 400,
            .Panel = pnlSettings,
            .SettingsPanelID = "Movie_Data",
            .Title = Master.eLang.GetString(556, "NFO"),
            .Type = Enums.SettingsPanelType.Movie
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings.Movie.DataSettings
            '
            'Actors
            '
            .Actors.Enabled = chkActorsEnabled.Checked
            Integer.TryParse(txtActorsLimit.Text, .Actors.Limit)
            .Actors.Locked = chkActorsLocked.Checked
            .Actors.WithImageOnly = chkActorsWithImageOnly.Checked
            '
            'Certifications
            '
            .Certifications.Enabled = chkCertificationsEnabled.Checked
            If Not String.IsNullOrEmpty(cbCertificationsLimit.Text) Then
                If cbCertificationsLimit.SelectedIndex = 0 Then
                    .Certifications.Filter = Master.eLang.All
                Else
                    .Certifications.Filter = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.name = cbCertificationsLimit.Text).abbreviation
                End If
            End If
            .Certifications.Locked = chkCertificationsLocked.Checked
            .CertificationsForMPAA = chkCertificationsForMPAA.Checked
            .CertificationsForMPAAFallback = chkCertificationsForMPAAFallback.Checked
            .CertificationsOnlyValue = chkCertificationsOnlyValue.Checked
            '
            'Collection
            '
            .Collection.Enabled = chkCollectionEnabled.Checked
            .Collection.Locked = chkCollectionLocked.Checked
            .Collection.AutoAddToCollection = chkCollectionAutoAddToCollection.Checked
            .Collection.SaveExtendedInformation = chkCollectionSaveExtendedInformation.Checked
            .Collection.SaveYAMJCompatible = chkCollectionSaveYAMJCompatible.Checked
            '
            'Countries
            '
            .Countries.Enabled = chkCountriesEnabled.Checked
            Integer.TryParse(txtCountriesLimit.Text, .Countries.Limit)
            .Countries.Locked = chkCountriesLocked.Checked
            '
            'Credits
            '
            .Credits.Enabled = chkCreditsEnabled.Checked
            .Credits.Locked = chkCreditsLocked.Checked
            '
            'Directors
            '
            .Directors.Enabled = chkDirectorsEnabled.Checked
            .Directors.Locked = chkDirectorsLocked.Checked
            '
            'Genres
            '
            .Genres.Enabled = chkGenresEnabled.Checked
            Integer.TryParse(txtGenresLimit.Text, .Genres.Limit)
            .Genres.Locked = chkGenresLocked.Checked
            '
            'Metadata
            '
            .MetadataScan.Enabled = chkMetaDataScanEnabled.Checked
            .MetadataScan.DurationForRuntimeEnabled = chkMetadataScanDurationForRuntimeEnabled.Checked
            .MetadataScan.DurationForRuntimeFormat = txtMetadataScanDurationForRuntimeFormat.Text.Trim
            .MetadataScan.LockAudioLanguage = chkMetadataScanLockAudioLanguage.Checked
            .MetadataScan.LockVideoLanguage = chkMetadataScanLockVideoLanguage.Checked
            '
            'MPAA
            '
            .MPAA.Enabled = chkMPAAEnabled.Checked
            .MPAA.Locked = chkMPAALocked.Checked
            .MPAANotRatedValue = txtMPAANotRatedValue.Text.Trim

            '
            'OriginalTitle
            '
            .OriginalTitle.Enabled = chkOriginalTitleEnabled.Checked
            .OriginalTitle.Locked = chkOriginalTitleLocked.Checked
            '
            'Outline
            '
            .Outline.Enabled = chkOutlineEnabled.Checked
            Integer.TryParse(txtOutlineLimit.Text, .Outline.Limit)
            .Outline.Locked = chkOutlineLocked.Checked
            .Outline.UsePlot = chkOutlineUsePlot.Checked
            .Outline.UsePlotAsFallback = chkOutlineUsePlotAsFallback.Checked
            '
            'Plot
            '
            .Plot.Enabled = chkPlotEnabled.Checked
            .Plot.Locked = chkPlotLocked.Checked
            '
            'Premiered
            '
            .Premiered.Enabled = chkPremieredEnabled.Checked
            .Premiered.Locked = chkPremieredLocked.Checked
            '
            'Ratings
            '
            .Ratings.Enabled = chkRatingsEnabled.Checked
            .Ratings.Locked = chkRatingsLocked.Checked
            '
            'Studios
            '
            .Studios.Enabled = chkStudiosEnabled.Checked
            Integer.TryParse(txtStudiosLimit.Text, .Studios.Limit)
            .Studios.Locked = chkStudiosLocked.Checked
            '
            'Tagline
            '
            .Tagline.Enabled = chkTaglineEnabled.Checked
            .Tagline.Locked = chkTaglineLocked.Checked
            '
            'Tags
            '
            .Tags.Enabled = chkTagsEnabled.Checked
            .Tags.Locked = chkTagsLocked.Checked
            .Tags.LimitAsList = _TempTagsWhitelist
            '
            'Title
            '
            .Title.Enabled = chkTitleEnabled.Checked
            .Title.Locked = chkTitleLock.Checked
            .Title.UseOriginalTitle = chkTitleUseOriginalTitle.Checked
            '
            'Top250
            '
            chkTop250Enabled.Checked = .Top250.Enabled
            chkTop250Locked.Checked = .Top250.Locked
            '
            'Trailer
            '
            .TrailerLink.Enabled = chkTrailerLinkEnabled.Checked
            .TrailerLink.Locked = chkTrailerLinkLocked.Checked
            .TrailerLink.SaveKodiCompatible = chkTrailerLinkSaveKodiCompatible.Checked
            '
            'UserRating
            '
            .UserRating.Enabled = chkUserRatingEnabled.Checked
            .UserRating.Locked = chkUserRatingLocked.Checked

            .CleanPlotAndOutline = chkCleanPlotAndOutline.Checked
            .ClearDisabledFields = chkClearDisabledFields.Checked
        End With

        With Master.eSettings
            .MovieMetadataPerFileType.Clear()
            .MovieMetadataPerFileType.AddRange(MovieMeta)
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.Movie.DataSettings
            '
            'Actors
            '
            chkActorsEnabled.Checked = .Actors.Enabled
            chkActorsLocked.Checked = .Actors.Locked
            chkActorsWithImageOnly.Checked = .Actors.WithImageOnly
            txtActorsLimit.Text = .Actors.Limit.ToString
            '
            'Certifications
            '
            chkCertificationsEnabled.Checked = .Certifications.Enabled
            Try
                cbCertificationsLimit.Items.Clear()
                cbCertificationsLimit.Items.Add(Master.eLang.All)
                cbCertificationsLimit.Items.AddRange((From lLang In APIXML.CertificationLanguages.Language Select lLang.name).ToArray)
                If cbCertificationsLimit.Items.Count > 0 Then
                    If .Certifications.Filter = Master.eLang.All Then
                        cbCertificationsLimit.SelectedIndex = 0
                    ElseIf Not String.IsNullOrEmpty(.Certifications.Filter) Then
                        Dim tLanguage As CertLanguages = APIXML.CertificationLanguages.Language.FirstOrDefault(Function(l) l.abbreviation = .Certifications.Filter)
                        If tLanguage IsNot Nothing AndAlso tLanguage.name IsNot Nothing AndAlso Not String.IsNullOrEmpty(tLanguage.name) Then
                            cbCertificationsLimit.Text = tLanguage.name
                        Else
                            cbCertificationsLimit.SelectedIndex = 0
                        End If
                    Else
                        cbCertificationsLimit.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
                'logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
            chkCertificationsLocked.Checked = .Certifications.Locked
            chkCertificationsForMPAA.Checked = .CertificationsForMPAA
            chkCertificationsForMPAAFallback.Checked = .CertificationsForMPAAFallback
            chkCertificationsOnlyValue.Checked = .CertificationsOnlyValue
            '
            'Collection
            '
            chkCollectionEnabled.Checked = .Collection.Enabled
            chkCollectionLocked.Checked = .Collection.Locked
            chkCollectionAutoAddToCollection.Checked = .Collection.AutoAddToCollection
            chkCollectionSaveExtendedInformation.Checked = .Collection.SaveExtendedInformation
            chkCollectionSaveYAMJCompatible.Checked = .Collection.SaveYAMJCompatible
            '
            'Countries
            '
            chkCountriesEnabled.Checked = .Countries.Enabled
            chkCountriesLocked.Checked = .Countries.Locked
            txtCountriesLimit.Text = .Countries.Limit.ToString
            '
            'Credits
            '
            chkCreditsEnabled.Checked = .Credits.Enabled
            chkCreditsLocked.Checked = .Credits.Locked

            '
            'Directors
            '
            chkDirectorsEnabled.Checked = .Directors.Enabled
            chkDirectorsLocked.Checked = .Directors.Locked
            '
            'Genres
            '
            chkGenresEnabled.Checked = .Genres.Enabled
            chkGenresLocked.Checked = .Genres.Locked
            txtGenresLimit.Text = .Genres.Limit.ToString
            '
            'Metadata
            '
            chkMetaDataScanEnabled.Checked = .MetadataScan.Enabled
            chkMetadataScanDurationForRuntimeEnabled.Checked = .MetadataScan.DurationForRuntimeEnabled
            txtMetadataScanDurationForRuntimeFormat.Text = .MetadataScan.DurationForRuntimeFormat
            chkMetadataScanLockAudioLanguage.Checked = .MetadataScan.LockAudioLanguage
            chkMetadataScanLockVideoLanguage.Checked = .MetadataScan.LockVideoLanguage
            '
            'MPAA
            '
            chkMPAAEnabled.Checked = .MPAA.Enabled
            chkMPAALocked.Checked = .MPAA.Locked
            txtMPAANotRatedValue.Text = .MPAANotRatedValue
            '
            'OriginalTitle
            '
            chkOriginalTitleEnabled.Checked = .OriginalTitle.Enabled
            chkOriginalTitleLocked.Checked = .OriginalTitle.Locked
            '
            'Outline
            '
            chkOutlineEnabled.Checked = .Outline.Enabled
            chkOutlineLocked.Checked = .Outline.Locked
            chkOutlineUsePlot.Checked = .Outline.UsePlot
            chkOutlineUsePlotAsFallback.Checked = .Outline.UsePlotAsFallback
            txtOutlineLimit.Text = .Outline.Limit.ToString
            '
            'Plot
            '
            chkPlotEnabled.Checked = .Plot.Enabled
            chkPlotLocked.Checked = .Plot.Locked
            '
            'Premiered
            '
            chkPremieredEnabled.Checked = .Premiered.Enabled
            chkPremieredLocked.Checked = .Premiered.Locked
            '
            'Ratings
            '
            chkRatingsEnabled.Checked = .Ratings.Enabled
            chkRatingsLocked.Checked = .Ratings.Locked
            '
            'Runtime
            '
            chkRuntimeEnabled.Checked = .Runtime.Enabled
            chkRuntimeLocked.Checked = .Runtime.Locked
            '
            'Studios
            '
            chkStudiosEnabled.Checked = .Studios.Enabled
            chkStudiosLocked.Checked = .Studios.Locked
            txtStudiosLimit.Text = .Studios.Limit.ToString
            '
            'Tagline
            '
            chkTaglineEnabled.Checked = .Tagline.Enabled
            chkTaglineLocked.Checked = .Tagline.Locked
            '
            'Tags
            '
            chkTagsEnabled.Checked = .Tags.Enabled
            chkTagsLocked.Checked = .Tags.Locked
            _TempTagsWhitelist = .Tags.LimitAsList
            '
            'Title
            '
            chkTitleEnabled.Checked = .Title.Enabled
            chkTitleLock.Checked = .Title.Locked
            chkTitleUseOriginalTitle.Checked = .Title.UseOriginalTitle
            '
            'Top250
            '
            chkTop250Enabled.Checked = .Top250.Enabled
            chkTop250Locked.Checked = .Top250.Locked
            '
            'Trailer
            '
            chkTrailerLinkEnabled.Checked = .TrailerLink.Enabled
            chkTrailerLinkLocked.Checked = .TrailerLink.Locked
            chkTrailerLinkSaveKodiCompatible.Checked = .TrailerLink.SaveKodiCompatible
            '
            'UserRating
            '
            chkUserRatingEnabled.Checked = .UserRating.Enabled
            chkUserRatingLocked.Checked = .UserRating.Locked


            chkCleanPlotAndOutline.Checked = .CleanPlotAndOutline
            chkClearDisabledFields.Checked = .ClearDisabledFields
        End With

        With Master.eSettings
            MovieMeta.AddRange(.MovieMetadataPerFileType)
        End With

        Load_MovieMetadata()
    End Sub

    Private Sub Setup()
        With Master.eLang
            chkActorsWithImageOnly.Text = .GetString(510, "Scrape Only Actors With Images")
            chkCertificationsForMPAA.Text = .GetString(511, "Use Certification for MPAA")
            chkCertificationsForMPAAFallback.Text = .GetString(1293, "Only if no MPAA is found")
            chkCertificationsOnlyValue.Text = .GetString(835, "Save value only")
            chkClearDisabledFields.Text = .GetString(125, "Clear disabled fields")
            chkCleanPlotAndOutline.Text = .GetString(985, "Clean Plot/Outline")
            chkCollectionAutoAddToCollection.Text = .GetString(1266, "Add Movie automatically to Collections")
            chkCollectionSaveExtendedInformation.Text = .GetString(1075, "Save extended Collection information to NFO (Kodi 16.0 ""Jarvis"" and newer)")
            chkCollectionSaveYAMJCompatible.Text = .GetString(561, "Save YAMJ Compatible Sets to NFO")
            chkMetaDataScanEnabled.Text = .GetString(517, "Scan Metadata")
            lblPlotForOutline.Text = .GetString(965, "Use Plot for Outline")
            lblPlotForOutlineAsFallback.Text = .GetString(958, "Only if Plot Outline is empty")
            chkMetadataScanDurationForRuntimeEnabled.Text = .GetString(516, "Use Duration for Runtime")
            chkTrailerLinkSaveKodiCompatible.Text = .GetString(1187, "Save YouTube-Trailer-Links in Kodi compatible format")
            lblOriginalTitleAsTitle.Text = .GetString(240, "Use Original Title as Title")
            gbMPAA.Text = .GetString(401, "MPAA")
            gbMovieScraperDefFIExtOpts.Text = .GetString(625, "Defaults by File Type")
            gbMovieScraperDefFIExtOpts.Text = .GetString(625, "Defaults by File Type")
            gbScraperFields.Text = .GetString(577, "Scraper Fields - Global")
            gbMetadata.Text = .GetString(59, "Metadata")
            gbMovieScraperMiscOpts.Text = .GetString(429, "Miscellaneous")
            lblMovieScraperDefFIExt.Text = String.Concat(.GetString(626, "File Type"), ":")
            lblDurationForRuntimeFormat.Text = String.Format(.GetString(732, "<h>=Hours{0}<m>=Minutes{0}<s>=Seconds"), Environment.NewLine)
            lblActors.Text = .GetString(231, "Actors")
            lblCertifications.Text = .GetString(56, "Certifications")
            lblCollection.Text = .GetString(424, "Collection")
            lblCountries.Text = .GetString(237, "Countries")
            lblCredits.Text = .GetString(394, "Writers")
            lblDirectors.Text = .GetString(940, "Directors")
            lblGenres.Text = .GetString(725, "Genres")
            lblScraperFieldsHeaderLimit.Text = .GetString(578, "Limit")
            lblScraperFieldsHeaderLocked.Text = .GetString(43, "Locked")
            lblLanguageAudio.Text = .GetString(431, "Metadata Audio Language")
            lblLanguageVideo.Text = .GetString(435, "Metadata Video Language")
            lblMPAA.Text = .GetString(401, "MPAA")
            lblOriginalTitle.Text = .GetString(302, "Original Title")
            lblOutline.Text = .GetString(64, "Plot Outline")
            lblPlot.Text = .GetString(65, "Plot")
            lblRatings.Text = .GetString(245, "Ratings")
            lblRuntime.Text = .GetString(238, "Runtime")
            lblStudios.Text = .GetString(226, "Studios")
            lblTagline.Text = .GetString(397, "Tagline")
            lblTitle.Text = .GetString(21, "Title")
            lblTop250.Text = .GetString(591, "Top 250")
            lblTrailerLink.Text = .GetString(937, "Trailer-Link")
            lblUserRating.Text = .GetString(1467, "User Rating")
            lblMovieScraperMPAANotRated.Text = String.Concat(.GetString(832, "MPAA value if no rating is available"), ":")
            lblPremiered.Text = .GetString(724, "Premiered")
            btnTagsWhitelist.Text = .GetString(841, "Whitelist")
        End With
    End Sub

    Private Sub Enable_ApplyButton() Handles _
            cbCertificationsLimit.SelectedIndexChanged,
            chkActorsLocked.CheckedChanged,
            chkActorsWithImageOnly.CheckedChanged,
            chkCertificationsForMPAAFallback.CheckedChanged,
            chkCertificationsLocked.CheckedChanged,
            chkCertificationsOnlyValue.CheckedChanged,
            chkCleanPlotAndOutline.CheckedChanged,
            chkClearDisabledFields.CheckedChanged,
            chkCollectionAutoAddToCollection.CheckedChanged,
            chkCollectionLocked.CheckedChanged,
            chkCollectionSaveExtendedInformation.CheckedChanged,
            chkCollectionSaveYAMJCompatible.CheckedChanged,
            chkCountriesLocked.CheckedChanged,
            chkCreditsEnabled.CheckedChanged,
            chkCreditsLocked.CheckedChanged,
            chkDirectorsEnabled.CheckedChanged,
            chkDirectorsLocked.CheckedChanged,
            chkGenresLocked.CheckedChanged,
            chkMetadataScanLockAudioLanguage.CheckedChanged,
            chkMetadataScanLockVideoLanguage.CheckedChanged,
            chkMPAAEnabled.CheckedChanged,
            chkMPAALocked.CheckedChanged,
            chkTitleUseOriginalTitle.CheckedChanged,
            chkOriginalTitleLocked.CheckedChanged,
            chkOutlineEnabled.CheckedChanged,
            chkOutlineLocked.CheckedChanged,
            chkOutlineUsePlotAsFallback.CheckedChanged,
            chkPlotLocked.CheckedChanged,
            chkPremieredEnabled.CheckedChanged,
            chkPremieredLocked.CheckedChanged,
            chkRatingsEnabled.CheckedChanged,
            chkRatingsLocked.CheckedChanged,
            chkRuntimeEnabled.CheckedChanged,
            chkRuntimeLocked.CheckedChanged,
            chkStudiosLocked.CheckedChanged,
            chkTaglineEnabled.CheckedChanged,
            chkTaglineLocked.CheckedChanged,
            chkTagsLocked.CheckedChanged,
            chkTitleEnabled.CheckedChanged,
            chkTitleLock.CheckedChanged,
            chkTop250Enabled.CheckedChanged,
            chkTop250Locked.CheckedChanged,
            chkTrailerLinkEnabled.CheckedChanged,
            chkTrailerLinkLocked.CheckedChanged,
            chkTrailerLinkSaveKodiCompatible.CheckedChanged,
            chkUserRatingEnabled.CheckedChanged,
            chkUserRatingLocked.CheckedChanged,
            txtActorsLimit.TextChanged,
            txtCountriesLimit.TextChanged,
            txtGenresLimit.TextChanged,
            txtMetadataScanDurationForRuntimeFormat.TextChanged,
            txtMovieScraperDefFIExt.TextChanged,
            txtMPAANotRatedValue.TextChanged,
            txtOutlineLimit.TextChanged,
            txtStudiosLimit.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ActorsEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkActorsEnabled.CheckedChanged
        chkActorsWithImageOnly.Enabled = chkActorsEnabled.Checked
        txtActorsLimit.Enabled = chkActorsEnabled.Checked
        If Not chkActorsEnabled.Checked Then txtActorsLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CertificationsEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCertificationsEnabled.CheckedChanged
        If Not chkCertificationsEnabled.Checked Then
            cbCertificationsLimit.Enabled = False
            cbCertificationsLimit.SelectedIndex = 0
            chkCertificationsForMPAA.Enabled = False
            chkCertificationsForMPAA.Checked = False
            chkCertificationsOnlyValue.Enabled = False
            chkCertificationsOnlyValue.Checked = False
        Else
            cbCertificationsLimit.Enabled = True
            cbCertificationsLimit.SelectedIndex = 0
            chkCertificationsForMPAA.Enabled = True
            chkCertificationsOnlyValue.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CertificationsForMPAA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCertificationsForMPAA.CheckedChanged
        If Not chkCertificationsForMPAA.Checked Then
            chkCertificationsForMPAAFallback.Enabled = False
            chkCertificationsForMPAAFallback.Checked = False
        Else
            chkCertificationsForMPAAFallback.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CollectionEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCollectionEnabled.CheckedChanged
        chkCollectionAutoAddToCollection.Enabled = chkCollectionEnabled.Checked
        If Not chkCollectionEnabled.Checked Then
            chkCollectionAutoAddToCollection.Checked = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CountriesEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCountriesEnabled.CheckedChanged
        txtCountriesLimit.Enabled = chkCountriesEnabled.Checked
        If Not chkCountriesEnabled.Checked Then txtCountriesLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub GenresEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGenresEnabled.CheckedChanged
        txtGenresLimit.Enabled = chkGenresEnabled.Checked
        If Not chkGenresEnabled.Checked Then txtGenresLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Load_MovieMetadata()
        lstMovieScraperDefFIExt.Items.Clear()
        For Each x As Settings.MetadataPerType In MovieMeta
            lstMovieScraperDefFIExt.Items.Add(x.FileType)
        Next
    End Sub

    Private Sub MetadataScanDurationForRuntimeEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMetadataScanDurationForRuntimeEnabled.CheckedChanged
        txtMetadataScanDurationForRuntimeFormat.Enabled = chkMetadataScanDurationForRuntimeEnabled.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub MetaDataScanEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkMetaDataScanEnabled.CheckedChanged
        chkMetadataScanDurationForRuntimeEnabled.Enabled = chkMetaDataScanEnabled.Checked
        If Not chkMetaDataScanEnabled.Checked Then chkMetadataScanDurationForRuntimeEnabled.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub OriginalTitleEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkOriginalTitleEnabled.CheckedChanged
        chkTitleUseOriginalTitle.Enabled = chkOriginalTitleEnabled.Checked
        If Not chkOriginalTitleEnabled.Checked Then chkTitleUseOriginalTitle.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub OutlineEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkOutlineEnabled.CheckedChanged
        txtOutlineLimit.Enabled = chkOutlineEnabled.Checked
        If Not chkOutlineEnabled.Checked Then txtOutlineLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub OutlineUsePlot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOutlineUsePlot.CheckedChanged
        txtOutlineLimit.Enabled = chkOutlineUsePlot.Checked
        chkOutlineUsePlotAsFallback.Enabled = chkOutlineUsePlot.Checked
        If Not chkOutlineUsePlot.Checked Then
            chkOutlineUsePlotAsFallback.Checked = False
            chkOutlineUsePlotAsFallback.Enabled = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub PlotEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPlotEnabled.CheckedChanged
        chkOutlineUsePlot.Enabled = chkPlotEnabled.Checked
        If Not chkPlotEnabled.Checked Then
            chkOutlineUsePlot.Checked = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub StudiosEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkStudiosEnabled.CheckedChanged
        txtStudiosLimit.Enabled = chkStudiosEnabled.Checked
        If Not chkStudiosEnabled.Checked Then txtStudiosLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TagsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkTagsEnabled.CheckedChanged
        btnTagsWhitelist.Enabled = chkTagsEnabled.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TagsWhitelist_Click(sender As Object, e As EventArgs) Handles btnTagsWhitelist.Click
        If frmMovie_Data_TagsWhitelist.ShowDialog(_TempTagsWhitelist) = DialogResult.OK Then
            _TempTagsWhitelist = frmMovie_Data_TagsWhitelist.Result
            RaiseEvent SettingsChanged()
        End If
    End Sub








    Private Sub btnMovieScraperDefFIExtAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not txtMovieScraperDefFIExt.Text.StartsWith(".") Then txtMovieScraperDefFIExt.Text = String.Concat(".", txtMovieScraperDefFIExt.Text)
        Using dFileInfo As New dlgFileInfo(New MediaContainers.Fileinfo)
            If dFileInfo.ShowDialog() = DialogResult.OK Then
                Dim fi = dFileInfo.Result
                If Not fi Is Nothing Then
                    Dim m As New Settings.MetadataPerType With {
                        .FileType = txtMovieScraperDefFIExt.Text,
                        .MetaData = fi
                    }
                    MovieMeta.Add(m)
                    Load_MovieMetadata()
                    txtMovieScraperDefFIExt.Text = String.Empty
                    txtMovieScraperDefFIExt.Focus()
                    RaiseEvent SettingsChanged()
                End If
            End If
        End Using
    End Sub

    Private Sub btnMovieScraperDefFIExtEdit_Click(ByVal sender As Object, ByVal e As EventArgs)
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each tMetadata As Settings.MetadataPerType In MovieMeta
                If tMetadata.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                    Using dFileInfo As New dlgFileInfo(tMetadata.MetaData)
                        If dFileInfo.ShowDialog = DialogResult.OK Then
                            tMetadata.MetaData = dFileInfo.Result
                            Load_MovieMetadata()
                            RaiseEvent SettingsChanged()
                        End If
                    End Using
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnMovieScraperDefFIExtRemove_Click(ByVal sender As Object, ByVal e As EventArgs)
        RemoveMovieMetaData()
    End Sub

    Private Sub lstMovieScraperDefFIExt_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            btnMovieScraperDefFIExtEdit.Enabled = True
            btnMovieScraperDefFIExtRemove.Enabled = True
            txtMovieScraperDefFIExt.Text = String.Empty
        Else
            btnMovieScraperDefFIExtEdit.Enabled = False
            btnMovieScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

    Private Sub RemoveMovieMetaData()
        If lstMovieScraperDefFIExt.SelectedItems.Count > 0 Then
            For Each x As Settings.MetadataPerType In MovieMeta
                If x.FileType = lstMovieScraperDefFIExt.SelectedItems(0).ToString Then
                    MovieMeta.Remove(x)
                    Load_MovieMetadata()
                    RaiseEvent SettingsChanged()
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub lstMovieScraperDefFIExt_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then RemoveMovieMetaData()
    End Sub

    Private Sub txtMovieScraperDefFIExt_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        btnMovieScraperDefFIExtAdd.Enabled = Not String.IsNullOrEmpty(txtMovieScraperDefFIExt.Text) AndAlso Not lstMovieScraperDefFIExt.Items.Contains(If(txtMovieScraperDefFIExt.Text.StartsWith("."), txtMovieScraperDefFIExt.Text, String.Concat(".", txtMovieScraperDefFIExt.Text)))
        If btnMovieScraperDefFIExtAdd.Enabled Then
            btnMovieScraperDefFIExtEdit.Enabled = False
            btnMovieScraperDefFIExtRemove.Enabled = False
        End If
    End Sub

#End Region 'Methods

End Class