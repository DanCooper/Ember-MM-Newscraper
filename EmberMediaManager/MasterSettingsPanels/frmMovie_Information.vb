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

Public Class frmMovie_Information
    Implements Interfaces.ISettingsPanel

#Region "Fields"

    Private _MovieMeta As New List(Of Settings.MetadataPerType)
    Private _TempTagsWhitelist As New ExtendedListOfString(Enums.DefaultType.Generic)

#End Region 'Fields

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.ISettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.ISettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV(ByVal id As Long) Implements Interfaces.ISettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.ISettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.ISettingsPanel.NeedsReload_Movieset
    Public Event NeedsReload_TVEpisode() Implements Interfaces.ISettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.ISettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.ISettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.ISettingsPanel.SettingsChanged
    Public Event StateChanged(ByVal uniqueSettingsPanelId As String, ByVal state As Boolean, ByVal diffOrder As Integer) Implements Interfaces.ISettingsPanel.StateChanged

#End Region 'Events

#Region "Properties"

    Public ReadOnly Property ChildType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ChildType

    Public Property Image As Image Implements Interfaces.ISettingsPanel.Image

    Public Property ImageIndex As Integer Implements Interfaces.ISettingsPanel.ImageIndex

    Public Property IsEnabled As Boolean Implements Interfaces.ISettingsPanel.IsEnabled

    Public ReadOnly Property MainPanel As Panel Implements Interfaces.ISettingsPanel.MainPanel

    Public Property Order As Integer Implements Interfaces.ISettingsPanel.Order

    Public ReadOnly Property ParentType As Enums.SettingsPanelType Implements Interfaces.ISettingsPanel.ParentType

    Public ReadOnly Property Title As String Implements Interfaces.ISettingsPanel.Title

    Public Property UniqueId As String Implements Interfaces.ISettingsPanel.UniqueId

#End Region 'Properties

#Region "Dialog Methods"

    Public Sub New()
        InitializeComponent()
        'Set Master Panel Data
        ChildType = Enums.SettingsPanelType.MovieInformation
        IsEnabled = True
        Image = Nothing
        ImageIndex = 3
        Order = 400
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(556, "NFO")
        ParentType = Enums.SettingsPanelType.Movie
        UniqueId = "Movie_Information"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        With Master.eLang
            btnTagsWhitelist.Text = .GetString(841, "Whitelist")
            chkActorsEnabled.Text = .GetString(231, "Actors")
            chkActorsWithImageOnly.Text = .GetString(510, "Only those with images")
            chkCertificationsEnabled.Text = .GetString(56, "Certifications")
            chkCertificationsForMPAA.Text = .GetString(511, "Use Certifications for MPAA")
            chkCertificationsForMPAAFallback.Text = .GetString(1293, "Only if MPAA is empty")
            chkCleanPlotAndOutline.Text = .GetString(985, "Clean Plot/Outline")
            chkClearDisabledFields.Text = .GetString(125, "Clear disabled fields")
            chkCollectionEnabled.Text = .GetString(424, "Collection")
            chkSaveExtendedInformation.Text = .GetString(1075, "Save extended Collection information to NFO (Kodi 16.0 ""Jarvis"" and newer)")
            chkCountriesEnabled.Text = .GetString(237, "Countries")
            chkCreditsEnabled.Text = .GetString(394, "Writers")
            chkDirectorsEnabled.Text = .GetString(940, "Directors")
            chkGenresEnabled.Text = .GetString(725, "Genres")
            chkMPAAEnabled.Text = .GetString(401, "MPAA")
            chkCertificationsValueOnly.Text = .GetString(835, "Save value only")
            chkOriginalTitleEnabled.Text = .GetString(302, "Original Title")
            chkOutlineEnabled.Text = .GetString(64, "Plot Outline")
            chkOutlineUsePlot.Text = .GetString(965, "Use Plot for Outline")
            chkOutlineUsePlotAsFallback.Text = .GetString(958, "Only if Plot Outline is empty")
            chkPlotEnabled.Text = .GetString(65, "Plot")
            chkPremieredEnabled.Text = .GetString(724, "Premiered")
            chkRatingsEnabled.Text = .GetString(245, "Ratings")
            chkRuntimeEnabled.Text = .GetString(238, "Runtime")
            chkStudiosEnabled.Text = .GetString(226, "Studios")
            chkTaglineEnabled.Text = .GetString(397, "Tagline")
            chkTitleEnabled.Text = .GetString(21, "Title")
            chkTitleUseOriginalTitle.Text = .GetString(240, "Use Original Title as Title")
            chkTop250Enabled.Text = .GetString(591, "Top 250")
            chkTrailerLinkEnabled.Text = .GetString(937, "Trailer-Link")
            chkTrailerLinkSaveKodiCompatible.Text = .GetString(1187, "Save YouTube-Trailer-Links in Kodi compatible format")
            chkUserRatingEnabled.Text = .GetString(1467, "User Rating")
            gbMiscellaneous.Text = .GetString(429, "Miscellaneous")
            gbScraperFields.Text = .GetString(577, "Scraper Fields - Global")
            lblMPAANotRatedValue.Text = String.Concat(.GetString(832, "Value if no rating is available"), ":")
            lblScraperFieldsHeaderLimit.Text = .GetString(578, "Limit")
            lblScraperFieldsHeaderLocked.Text = .GetString(43, "Locked")
        End With

        Load_Certifications()
    End Sub

#End Region 'Dialog Methods 

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.ISettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Sub Addon_Order_Changed(ByVal totalCount As Integer) Implements Interfaces.ISettingsPanel.OrderChanged
        Return
    End Sub

    Public Sub SaveSettings() Implements Interfaces.ISettingsPanel.SaveSettings
        With Master.eSettings.Movie.InformationSettings
            '
            'Actors
            '
            .Actors.Enabled = chkActorsEnabled.Checked
            .Actors.Limit = Convert.ToInt32(nudActorsLimit.Value)
            .Actors.Locked = chkActorsLocked.Checked
            .Actors.WithImageOnly = chkActorsWithImageOnly.Checked
            '
            'Certifications
            '
            .Certifications.Enabled = chkCertificationsEnabled.Checked
            If Not String.IsNullOrEmpty(cbCertificationsLimit.Text) Then
                If cbCertificationsLimit.SelectedIndex = 0 Then
                    .Certifications.Filter = Master.eLang.CommonWordsList.All
                Else
                    .Certifications.Filter = Localization.Countries.Items.FirstOrDefault(Function(f) f.Name = cbCertificationsLimit.Text).Alpha2
                End If
            End If
            .Certifications.Locked = chkCertificationsLocked.Checked
            .CertificationsForMPAA = chkCertificationsForMPAA.Checked
            .CertificationsForMPAAFallback = chkCertificationsForMPAAFallback.Checked
            .CertificationsOnlyValue = chkCertificationsValueOnly.Checked
            '
            'Collection
            '
            .Collection.AddAutomaticallyToCollection = chkAddAutomaticallyToCollection.Checked
            .Collection.Enabled = chkCollectionEnabled.Checked
            .Collection.Locked = chkCollectionLocked.Checked
            .Collection.SaveExtendedInformation = chkSaveExtendedInformation.Checked
            .Collection.SaveYAMJCompatible = chkSaveYAMJCompatible.Checked
            '
            'Countries
            '
            .Countries.Enabled = chkCountriesEnabled.Checked
            .Countries.Limit = Convert.ToInt32(nudCountriesLimit.Value)
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
            .Genres.Limit = Convert.ToInt32(nudGenresLimit.Value)
            .Genres.Locked = chkGenresLocked.Checked
            '
            'MPAA
            '
            .MPAA.Enabled = chkMPAAEnabled.Checked
            .MPAA.Locked = chkMPAALocked.Checked
            .MPAA.NotRatedValue = txtMPAANotRatedValue.Text.Trim
            '
            'OriginalTitle
            '
            .OriginalTitle.Enabled = chkOriginalTitleEnabled.Checked
            .OriginalTitle.Locked = chkOriginalTitleLocked.Checked
            '
            'Outline
            '
            .Outline.Enabled = chkOutlineEnabled.Checked
            .Outline.Limit = Convert.ToInt32(nudOutlineLimit.Value)
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
            'Runtime
            '
            .Runtime.Enabled = chkRuntimeEnabled.Checked
            .Runtime.Locked = chkRuntimeLocked.Checked
            '
            'Studios
            '
            .Studios.Enabled = chkStudiosEnabled.Checked
            .Studios.Limit = Convert.ToInt32(nudStudiosLimit.Value)
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
            .Title.Locked = chkTitleLocked.Checked
            .Title.UseOriginalTitle = chkTitleUseOriginalTitle.Checked
            '
            'Top250
            '
            .Top250.Enabled = chkTop250Enabled.Checked
            .Top250.Locked = chkTop250Locked.Checked
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
            .MovieMetadataPerFileType.AddRange(_MovieMeta)
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.Movie.InformationSettings
            '
            'Actors
            '
            chkActorsEnabled.Checked = .Actors.Enabled
            chkActorsLocked.Checked = .Actors.Locked
            chkActorsWithImageOnly.Checked = .Actors.WithImageOnly
            nudActorsLimit.Text = .Actors.Limit.ToString
            '
            'Certifications
            '
            chkCertificationsEnabled.Checked = .Certifications.Enabled
            If cbCertificationsLimit.Items.Count > 0 Then
                If .Certifications.FilterSpecified Then
                    Dim Language = Localization.Countries.Items.FirstOrDefault(Function(l) l.Alpha2 = .Certifications.Filter)
                    If Language IsNot Nothing AndAlso Language.NameSpecified Then
                        cbCertificationsLimit.Text = Language.Name
                    Else
                        cbCertificationsLimit.SelectedIndex = 0
                    End If
                Else
                    cbCertificationsLimit.SelectedIndex = 0
                End If
            End If
            chkCertificationsLocked.Checked = .Certifications.Locked
            chkCertificationsForMPAA.Checked = .CertificationsForMPAA
            chkCertificationsForMPAAFallback.Checked = .CertificationsForMPAAFallback
            chkCertificationsValueOnly.Checked = .CertificationsOnlyValue
            '
            'Collection
            '
            chkAddAutomaticallyToCollection.Checked = .Collection.AddAutomaticallyToCollection
            chkCollectionEnabled.Checked = .Collection.Enabled
            chkCollectionLocked.Checked = .Collection.Locked
            chkSaveExtendedInformation.Checked = .Collection.SaveExtendedInformation
            chkSaveYAMJCompatible.Checked = .Collection.SaveYAMJCompatible
            '
            'Countries
            '
            chkCountriesEnabled.Checked = .Countries.Enabled
            chkCountriesLocked.Checked = .Countries.Locked
            nudCountriesLimit.Text = .Countries.Limit.ToString
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
            nudGenresLimit.Text = .Genres.Limit.ToString
            '
            'MPAA
            '
            chkMPAAEnabled.Checked = .MPAA.Enabled
            chkMPAALocked.Checked = .MPAA.Locked
            txtMPAANotRatedValue.Text = .MPAA.NotRatedValue
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
            nudOutlineLimit.Text = .Outline.Limit.ToString
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
            nudStudiosLimit.Text = .Studios.Limit.ToString
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
            chkTitleLocked.Checked = .Title.Locked
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
            'UniqueIDs
            '
            chkUserRatingEnabled.Checked = .UserRating.Enabled
            chkUserRatingLocked.Checked = .UserRating.Locked
            '
            'UserRating
            '
            chkUserRatingEnabled.Checked = .UserRating.Enabled
            chkUserRatingLocked.Checked = .UserRating.Locked
            '
            'Miscellaneous
            '
            chkCleanPlotAndOutline.Checked = .CleanPlotAndOutline
            chkClearDisabledFields.Checked = .ClearDisabledFields
        End With
    End Sub

    Private Sub Enable_ApplyButton() Handles _
            cbCertificationsLimit.SelectedIndexChanged,
            chkActorsLocked.CheckedChanged,
            chkActorsWithImageOnly.CheckedChanged,
            chkCertificationsForMPAAFallback.CheckedChanged,
            chkCertificationsLocked.CheckedChanged,
            chkCleanPlotAndOutline.CheckedChanged,
            chkClearDisabledFields.CheckedChanged,
            chkCollectionEnabled.CheckedChanged,
            chkCollectionLocked.CheckedChanged,
            chkSaveExtendedInformation.CheckedChanged,
            chkCountriesLocked.CheckedChanged,
            chkCreditsEnabled.CheckedChanged,
            chkCreditsLocked.CheckedChanged,
            chkDirectorsEnabled.CheckedChanged,
            chkDirectorsLocked.CheckedChanged,
            chkGenresLocked.CheckedChanged,
            chkMPAAEnabled.CheckedChanged,
            chkMPAALocked.CheckedChanged,
            chkCertificationsValueOnly.CheckedChanged,
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
            chkTitleLocked.CheckedChanged,
            chkTitleUseOriginalTitle.CheckedChanged,
            chkTop250Enabled.CheckedChanged,
            chkTop250Locked.CheckedChanged,
            chkTrailerLinkEnabled.CheckedChanged,
            chkTrailerLinkLocked.CheckedChanged,
            chkTrailerLinkSaveKodiCompatible.CheckedChanged,
            chkUserRatingEnabled.CheckedChanged,
            chkUserRatingLocked.CheckedChanged,
            nudActorsLimit.ValueChanged,
            nudCountriesLimit.ValueChanged,
            nudGenresLimit.ValueChanged,
            nudOutlineLimit.ValueChanged,
            nudStudiosLimit.ValueChanged,
            txtMPAANotRatedValue.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ActorsEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkActorsEnabled.CheckedChanged
        chkActorsWithImageOnly.Enabled = chkActorsEnabled.Checked
        nudActorsLimit.Enabled = chkActorsEnabled.Checked
        If Not chkActorsEnabled.Checked Then nudActorsLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CertificationsEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCertificationsEnabled.CheckedChanged
        If chkCertificationsEnabled.Checked Then
            cbCertificationsLimit.Enabled = chkCertificationsEnabled.Checked
            chkCertificationsForMPAA.Enabled = chkCertificationsEnabled.Checked
            chkCertificationsValueOnly.Enabled = chkCertificationsEnabled.Checked
        Else
            cbCertificationsLimit.Enabled = chkCertificationsEnabled.Checked
            chkCertificationsForMPAA.Enabled = chkCertificationsEnabled.Checked
            chkCertificationsValueOnly.Enabled = chkCertificationsEnabled.Checked
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

    Private Sub CountriesEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCountriesEnabled.CheckedChanged
        nudCountriesLimit.Enabled = chkCountriesEnabled.Checked
        If Not chkCountriesEnabled.Checked Then nudCountriesLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub GenresEnabled_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkGenresEnabled.CheckedChanged
        nudGenresLimit.Enabled = chkGenresEnabled.Checked
        If Not chkGenresEnabled.Checked Then nudGenresLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Load_Certifications()
        cbCertificationsLimit.Items.Clear()
        cbCertificationsLimit.Items.Add(Master.eLang.CommonWordsList.All)
        cbCertificationsLimit.Items.AddRange((From lLang In Localization.Countries.Items Select lLang.Name).ToArray)
    End Sub

    Private Sub OriginalTitleEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkOriginalTitleEnabled.CheckedChanged
        chkTitleUseOriginalTitle.Enabled = chkOriginalTitleEnabled.Checked
        If Not chkOriginalTitleEnabled.Checked Then chkTitleUseOriginalTitle.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub OutlineEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkOutlineEnabled.CheckedChanged
        nudOutlineLimit.Enabled = chkOutlineEnabled.Checked
        If Not chkOutlineEnabled.Checked Then nudOutlineLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub OutlineUsePlot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOutlineUsePlot.CheckedChanged
        nudOutlineLimit.Enabled = chkOutlineUsePlot.Checked
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
        nudStudiosLimit.Enabled = chkStudiosEnabled.Checked
        If Not chkStudiosEnabled.Checked Then nudStudiosLimit.Text = "0"
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TagsEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkTagsEnabled.CheckedChanged
        btnTagsWhitelist.Enabled = chkTagsEnabled.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TagsWhitelist_Click(sender As Object, e As EventArgs) Handles btnTagsWhitelist.Click
        If frmMovie_Information_TagsWhitelist.ShowDialog(_TempTagsWhitelist) = DialogResult.OK Then
            _TempTagsWhitelist = frmMovie_Information_TagsWhitelist.Result
            RaiseEvent SettingsChanged()
        End If
    End Sub

#End Region 'Methods

End Class