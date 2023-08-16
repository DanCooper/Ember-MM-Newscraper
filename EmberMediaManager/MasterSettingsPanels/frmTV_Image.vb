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

Public Class frmTV_Image
    Implements Interfaces.ISettingsPanel

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
        ChildType = Enums.SettingsPanelType.TVImage
        IsEnabled = True
        Image = Nothing
        ImageIndex = 6
        Order = 500
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(497, "Images")
        ParentType = Enums.SettingsPanelType.TV
        UniqueId = "TV_Image"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        'Actor Thumbs
        Dim strActorthumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        lblActorthumbs_TVEpisode.Text = strActorthumbs
        lblActorthumbs_TVShow.Text = strActorthumbs

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        lblBanner_TVAllSeasons.Text = strBanner
        lblBanner_TVSeason.Text = strBanner
        lblBanner_TVShow.Text = strBanner

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        lblFanart_TVAllSeasons.Text = strFanart
        lblFanart_TVEpisode.Text = strFanart
        lblFanart_TVSeason.Text = strFanart
        lblFanart_TVShow.Text = strFanart

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        lblLandscape_TVAllSeasons.Text = strLandscape
        lblLandscape_TVSeason.Text = strLandscape
        lblLandscape_TVShow.Text = strLandscape

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        lblPoster_TVAllSeasons.Text = strPoster
        lblPoster_TVEpisode.Text = strPoster
        lblPoster_TVSeason.Text = strPoster
        lblPoster_TVShow.Text = strPoster

        chkCacheEnabled.Text = Master.eLang.GetString(249, "Enable Image Caching")
        chkDisplayImageSelectDialog.Text = Master.eLang.GetString(499, "Display ""Select Images"" dialog while single scraping")
        chkFilterGetBlankImages.Text = Master.eLang.GetString(1207, "Also Get Blank Images")
        chkFilterGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        chkFilterMediaLanguage.Text = Master.eLang.GetString(736, "Only Get Images for the Media Language")
        chkForceLanguage.Text = Master.eLang.GetString(1034, "Force Language")
        gbImageTypes.Text = Master.eLang.GetString(304, "Image Types")
        gbLanguages.Text = Master.eLang.GetString(741, "Preferred Language")
        gbOptions.Text = Master.eLang.GetString(390, "Options")
        lblCharacterart_TVShow.Text = Master.eLang.GetString(1140, "CharacterArt")
        lblClearart_TVShow.Text = Master.eLang.GetString(1096, "ClearArt")
        lblClearlogo_TVShow.Text = Master.eLang.GetString(1097, "ClearLogo")
        lblExtrafanarts_TVShow.Text = Master.eLang.GetString(992, "Extrafanarts")
        lblHintPreselect.Text = String.Concat("* ", Master.eLang.GetString(1023, "Preselect images in ""Select Images"" dialog"))
        lblHintVideoExtraction.Text = String.Concat("** ", Master.eLang.GetString(666, "Extract images from video file"))
        lblHintVideoExtractionPreferred.Text = String.Concat("*** ", Master.eLang.GetString(667, "Prefer extracted images"))
        lblKeepExisting.Text = Master.eLang.GetString(971, "Keep existing")
        lblKeyart_TVShow.Text = Master.eLang.GetString(296, "KeyArt")
        lblLimit.Text = Master.eLang.GetString(578, "Limit")
        lblMaxHeight.Text = Master.eLang.GetString(480, "Max Height")
        lblMaxWidth.Text = Master.eLang.GetString(479, "Max Width")
        lblPreferredSize.Text = Master.eLang.GetString(482, "Preferred Size")
        lblPreferredSizeOnly.Text = Master.eLang.GetString(145, "Only")
        lblPreferredType.Text = Master.eLang.GetString(730, "Preferred Type")
        lblPreselect.Text = String.Concat(Master.eLang.GetString(308, "Preselect"), " *")
        lblTVImagesHeaderAllSeasons.Text = Master.eLang.GetString(1202, "All Seasons")
        lblTVImagesHeaderEpisode.Text = Master.eLang.GetString(727, "Episode")
        lblTVImagesHeaderResize.Text = Master.eLang.GetString(481, "Resize")
        lblTVImagesHeaderSeason.Text = Master.eLang.GetString(650, "Season")
        lblTVImagesHeaderTVShow.Text = Master.eLang.GetString(700, "TV Show")
        lblVideoExtraction.Text = String.Concat(Master.eLang.GetString(538, "Extract"), " **")

        Load_BannerSizes()
        Load_BannerTypes()
        Load_CharacterartSizes()
        Load_ClearartSizes()
        Load_ClearlogoSizes()
        Load_FanartSizes()
        Load_LandscapeSizes()
        Load_PosterSizes()
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
        'TVAllSeasons
        With Master.eSettings.TVAllSeasons.ImageSettings
            'Options
            .CacheEnabled = chkCacheEnabled.Checked
            .DisplayImageSelectDialog = chkDisplayImageSelectDialog.Checked

            'Languages
            .ForceLanguage = chkForceLanguage.Checked
            If Not String.IsNullOrEmpty(cbForcedLanguage.Text) Then
                .ForcedLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = cbForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .FilterGetBlankImages = chkFilterGetBlankImages.Checked
            .FilterGetEnglishImages = chkFilterGetEnglishImages.Checked
            .FilterMediaLanguage = chkFilterMediaLanguage.Checked

            'ImageTypes
            'Banner
            .Banner.KeepExisting = chkBannerKeepExisting_TVAllSeasons.Checked
            .Banner.MaxHeight = If(Not String.IsNullOrEmpty(txtBannerHeight_TVAllSeasons.Text), Convert.ToInt32(txtBannerHeight_TVAllSeasons.Text), 0)
            .Banner.MaxWidth = If(Not String.IsNullOrEmpty(txtBannerWidth_TVAllSeasons.Text), Convert.ToInt32(txtBannerWidth_TVAllSeasons.Text), 0)
            .Banner.PreferredSize = CType(cbBannerPreferredSize_TVAllSeasons.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Banner.PreferredSizeOnly = chkBannerPreferredSizeOnly_TVAllSeasons.Checked
            .Banner.Resize = chkBannerResize_TVAllSeasons.Checked
            'Fanart
            .Fanart.KeepExisting = chkFanartKeepExisting_TVAllSeasons.Checked
            .Fanart.MaxHeight = If(Not String.IsNullOrEmpty(txtFanartHeight_TVAllSeasons.Text), Convert.ToInt32(txtFanartHeight_TVAllSeasons.Text), 0)
            .Fanart.MaxWidth = If(Not String.IsNullOrEmpty(txtFanartWidth_TVAllSeasons.Text), Convert.ToInt32(txtFanartWidth_TVAllSeasons.Text), 0)
            .Fanart.PreferredSize = CType(cbFanartPreferredSize_TVAllSeasons.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Fanart.PreferredSizeOnly = chkFanartPreferredSizeOnly_TVAllSeasons.Checked
            .Fanart.Resize = chkFanartResize_TVAllSeasons.Checked
            'Landscape
            .Landscape.KeepExisting = chkLandscapeKeepExisting_TVAllSeasons.Checked
            .Landscape.PreferredSize = CType(cbLandscapePreferredSize_TVAllSeasons.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Landscape.PreferredSizeOnly = chkLandscapePreferredSizeOnly_TVAllSeasons.Checked
            'Poster
            .Poster.KeepExisting = chkPosterKeepExisting_TVAllSeasons.Checked
            .Poster.MaxHeight = If(Not String.IsNullOrEmpty(txtPosterHeight_TVAllSeasons.Text), Convert.ToInt32(txtPosterHeight_TVAllSeasons.Text), 0)
            .Poster.MaxWidth = If(Not String.IsNullOrEmpty(txtPosterWidth_TVAllSeasons.Text), Convert.ToInt32(txtPosterWidth_TVAllSeasons.Text), 0)
            .Poster.PreferredSize = CType(cbPosterPreferredSize_TVAllSeasons.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Poster.PreferredSizeOnly = chkPosterPreferredSizeOnly_TVAllSeasons.Checked
            .Poster.Resize = chkPosterResize_TVAllSeasons.Checked
        End With
        'TVEpisode
        With Master.eSettings.TVEpisode.ImageSettings
            'Options
            .CacheEnabled = chkCacheEnabled.Checked
            .DisplayImageSelectDialog = chkDisplayImageSelectDialog.Checked

            'Languages
            .ForceLanguage = chkForceLanguage.Checked
            If Not String.IsNullOrEmpty(cbForcedLanguage.Text) Then
                .ForcedLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = cbForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .FilterGetBlankImages = chkFilterGetBlankImages.Checked
            .FilterGetEnglishImages = chkFilterGetEnglishImages.Checked
            .FilterMediaLanguage = chkFilterMediaLanguage.Checked

            'ImageTypes
            'Actorthumbs
            .Actorthumbs.KeepExisting = chkActorthumbsKeepExisting_TVEpisode.Checked
            'Fanart
            .Fanart.KeepExisting = chkFanartKeepExisting_TVEpisode.Checked
            .Fanart.MaxHeight = If(Not String.IsNullOrEmpty(txtFanartHeight_TVEpisode.Text), Convert.ToInt32(txtFanartHeight_TVEpisode.Text), 0)
            .Fanart.MaxWidth = If(Not String.IsNullOrEmpty(txtFanartWidth_TVEpisode.Text), Convert.ToInt32(txtFanartWidth_TVEpisode.Text), 0)
            .Fanart.PreferredSize = CType(cbFanartPreferredSize_TVEpisode.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Fanart.PreferredSizeOnly = chkFanartPreferredSizeOnly_TVEpisode.Checked
            .Fanart.Resize = chkFanartResize_TVEpisode.Checked
            'Poster
            .Poster.KeepExisting = chkPosterKeepExisting_TVEpisode.Checked
            .Poster.MaxHeight = If(Not String.IsNullOrEmpty(txtPosterHeight_TVEpisode.Text), Convert.ToInt32(txtPosterHeight_TVEpisode.Text), 0)
            .Poster.MaxWidth = If(Not String.IsNullOrEmpty(txtPosterWidth_TVEpisode.Text), Convert.ToInt32(txtPosterWidth_TVEpisode.Text), 0)
            .Poster.PreferredSize = CType(cbPosterPreferredSize_TVEpisode.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Poster.PreferredSizeOnly = chkPosterPreferredSizeOnly_TVEpisode.Checked
            .Poster.Resize = chkPosterResize_TVEpisode.Checked
        End With
        'TVSeason
        With Master.eSettings.TVSeason.ImageSettings
            'Options
            .CacheEnabled = chkCacheEnabled.Checked
            .DisplayImageSelectDialog = chkDisplayImageSelectDialog.Checked

            'Languages
            .ForceLanguage = chkForceLanguage.Checked
            If Not String.IsNullOrEmpty(cbForcedLanguage.Text) Then
                .ForcedLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = cbForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .FilterGetBlankImages = chkFilterGetBlankImages.Checked
            .FilterGetEnglishImages = chkFilterGetEnglishImages.Checked
            .FilterMediaLanguage = chkFilterMediaLanguage.Checked

            'ImageTypes
            'Banner
            .Banner.KeepExisting = chkBannerKeepExisting_TVSeason.Checked
            .Banner.MaxHeight = If(Not String.IsNullOrEmpty(txtBannerHeight_TVSeason.Text), Convert.ToInt32(txtBannerHeight_TVSeason.Text), 0)
            .Banner.MaxWidth = If(Not String.IsNullOrEmpty(txtBannerWidth_TVSeason.Text), Convert.ToInt32(txtBannerWidth_TVSeason.Text), 0)
            .Banner.PreferredSize = CType(cbBannerPreferredSize_TVSeason.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Banner.PreferredSizeOnly = chkBannerPreferredSizeOnly_TVSeason.Checked
            .Banner.Resize = chkBannerResize_TVSeason.Checked
            'Fanart
            .Fanart.KeepExisting = chkFanartKeepExisting_TVSeason.Checked
            .Fanart.MaxHeight = If(Not String.IsNullOrEmpty(txtFanartHeight_TVSeason.Text), Convert.ToInt32(txtFanartHeight_TVSeason.Text), 0)
            .Fanart.MaxWidth = If(Not String.IsNullOrEmpty(txtFanartWidth_TVSeason.Text), Convert.ToInt32(txtFanartWidth_TVSeason.Text), 0)
            .Fanart.PreferredSize = CType(cbFanartPreferredSize_TVSeason.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Fanart.PreferredSizeOnly = chkFanartPreferredSizeOnly_TVSeason.Checked
            .Fanart.Resize = chkFanartResize_TVSeason.Checked
            'Landscape
            .Landscape.KeepExisting = chkLandscapeKeepExisting_TVSeason.Checked
            .Landscape.PreferredSize = CType(cbLandscapePreferredSize_TVSeason.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Landscape.PreferredSizeOnly = chkLandscapePreferredSizeOnly_TVSeason.Checked
            'Poster
            .Poster.KeepExisting = chkPosterKeepExisting_TVSeason.Checked
            .Poster.MaxHeight = If(Not String.IsNullOrEmpty(txtPosterHeight_TVSeason.Text), Convert.ToInt32(txtPosterHeight_TVSeason.Text), 0)
            .Poster.MaxWidth = If(Not String.IsNullOrEmpty(txtPosterWidth_TVSeason.Text), Convert.ToInt32(txtPosterWidth_TVSeason.Text), 0)
            .Poster.PreferredSize = CType(cbPosterPreferredSize_TVSeason.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Poster.PreferredSizeOnly = chkPosterPreferredSizeOnly_TVSeason.Checked
            .Poster.Resize = chkPosterResize_TVSeason.Checked
        End With
        'TVShow
        With Master.eSettings.TVShow.ImageSettings
            'Options
            .CacheEnabled = chkCacheEnabled.Checked
            .DisplayImageSelectDialog = chkDisplayImageSelectDialog.Checked

            'Languages
            .ForceLanguage = chkForceLanguage.Checked
            If Not String.IsNullOrEmpty(cbForcedLanguage.Text) Then
                .ForcedLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = cbForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .FilterGetBlankImages = chkFilterGetBlankImages.Checked
            .FilterGetEnglishImages = chkFilterGetEnglishImages.Checked
            .FilterMediaLanguage = chkFilterMediaLanguage.Checked

            'ImageTypes
            'Actorthumbs
            .Actorthumbs.KeepExisting = chkActorthumbsKeepExisting_TVShow.Checked
            'Banner
            .Banner.KeepExisting = chkBannerKeepExisting_TVShow.Checked
            .Banner.MaxHeight = If(Not String.IsNullOrEmpty(txtBannerHeight_TVShow.Text), Convert.ToInt32(txtBannerHeight_TVShow.Text), 0)
            .Banner.MaxWidth = If(Not String.IsNullOrEmpty(txtBannerWidth_TVShow.Text), Convert.ToInt32(txtBannerWidth_TVShow.Text), 0)
            .Banner.PreferredSize = CType(cbBannerPreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Banner.PreferredSizeOnly = chkBannerPreferredSizeOnly_TVShow.Checked
            .Banner.Resize = chkBannerResize_TVShow.Checked
            'Characterart
            .Characterart.KeepExisting = chkCharacterartKeepExisting_TVShow.Checked
            .Characterart.PreferredSize = CType(cbCharacterartPreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Characterart.PreferredSizeOnly = chkCharacterartPreferredSizeOnly_TVShow.Checked
            'Clearart
            .Clearart.KeepExisting = chkClearartKeepExisting_TVShow.Checked
            .Clearart.PreferredSize = CType(cbClearartPreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Clearart.PreferredSizeOnly = chkClearartPreferredSizeOnly_TVShow.Checked
            'Clearlogo
            .Clearlogo.KeepExisting = chkClearlogoKeepExisting_TVShow.Checked
            .Clearlogo.PreferredSize = CType(cbClearlogoPreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Clearlogo.PreferredSizeOnly = chkClearlogoPreferredSizeOnly_TVShow.Checked
            'Extrafanarts
            .Extrafanarts.KeepExisting = chkExtrafanartsKeepExisting_TVShow.Checked
            .Extrafanarts.Limit = If(Not String.IsNullOrEmpty(txtExtrafanartsLimit_TVShow.Text), Convert.ToInt32(txtExtrafanartsLimit_TVShow.Text), 0)
            .Extrafanarts.MaxHeight = If(Not String.IsNullOrEmpty(txtExtrafanartsHeight_TVShow.Text), Convert.ToInt32(txtExtrafanartsHeight_TVShow.Text), 0)
            .Extrafanarts.MaxWidth = If(Not String.IsNullOrEmpty(txtExtrafanartsWidth_TVShow.Text), Convert.ToInt32(txtExtrafanartsWidth_TVShow.Text), 0)
            .Extrafanarts.PreferredSize = CType(cbExtrafanartsPreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Extrafanarts.PreferredSizeOnly = chkExtrafanartsPreferredSizeOnly_TVShow.Checked
            .Extrafanarts.Preselect = chkExtrafanartsPreselect_TVShow.Checked
            .Extrafanarts.Resize = chkExtrafanartsResize_TVShow.Checked
            'Fanart
            .Fanart.KeepExisting = chkFanartKeepExisting_TVShow.Checked
            .Fanart.MaxHeight = If(Not String.IsNullOrEmpty(txtFanartHeight_TVShow.Text), Convert.ToInt32(txtFanartHeight_TVShow.Text), 0)
            .Fanart.MaxWidth = If(Not String.IsNullOrEmpty(txtFanartWidth_TVShow.Text), Convert.ToInt32(txtFanartWidth_TVShow.Text), 0)
            .Fanart.PreferredSize = CType(cbFanartPreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Fanart.PreferredSizeOnly = chkFanartPreferredSizeOnly_TVShow.Checked
            .Fanart.Resize = chkFanartResize_TVShow.Checked
            'Keyart
            .Keyart.KeepExisting = chkKeyartKeepExisting_TVShow.Checked
            .Keyart.MaxHeight = If(Not String.IsNullOrEmpty(txtKeyartHeight_TVShow.Text), Convert.ToInt32(txtKeyartHeight_TVShow.Text), 0)
            .Keyart.MaxWidth = If(Not String.IsNullOrEmpty(txtKeyartWidth_TVShow.Text), Convert.ToInt32(txtKeyartWidth_TVShow.Text), 0)
            .Keyart.PreferredSize = CType(cbKeyartPreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Keyart.PreferredSizeOnly = chkKeyartPreferredSizeOnly_TVShow.Checked
            .Keyart.Resize = chkKeyartResize_TVShow.Checked
            'Landscape
            .Landscape.KeepExisting = chkLandscapeKeepExisting_TVShow.Checked
            .Landscape.PreferredSize = CType(cbLandscapePreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Landscape.PreferredSizeOnly = chkLandscapePreferredSizeOnly_TVShow.Checked
            'Poster
            .Poster.KeepExisting = chkPosterKeepExisting_TVShow.Checked
            .Poster.MaxHeight = If(Not String.IsNullOrEmpty(txtPosterHeight_TVShow.Text), Convert.ToInt32(txtPosterHeight_TVShow.Text), 0)
            .Poster.MaxWidth = If(Not String.IsNullOrEmpty(txtPosterWidth_TVShow.Text), Convert.ToInt32(txtPosterWidth_TVShow.Text), 0)
            .Poster.PreferredSize = CType(cbPosterPreferredSize_TVShow.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Poster.PreferredSizeOnly = chkPosterPreferredSizeOnly_TVShow.Checked
            .Poster.Resize = chkPosterResize_TVShow.Checked
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Private Sub EnableApplyButton(sender As Object, e As EventArgs) Handles _
        cbBannerPreferredSize_TVAllSeasons.SelectedIndexChanged,
        cbBannerPreferredSize_TVSeason.SelectedIndexChanged,
        cbBannerPreferredSize_TVShow.SelectedIndexChanged,
        cbBannerPreferredType_TVAllSeasons.SelectedIndexChanged,
        cbBannerPreferredType_TVSeason.SelectedIndexChanged,
        cbBannerPreferredType_TVShow.SelectedIndexChanged,
        cbCharacterartPreferredSize_TVShow.SelectedIndexChanged,
        cbClearartPreferredSize_TVShow.SelectedIndexChanged,
        cbClearlogoPreferredSize_TVShow.SelectedIndexChanged,
        cbExtrafanartsPreferredSize_TVShow.SelectedIndexChanged,
        cbFanartPreferredSize_TVAllSeasons.SelectedIndexChanged,
        cbFanartPreferredSize_TVEpisode.SelectedIndexChanged,
        cbFanartPreferredSize_TVSeason.SelectedIndexChanged,
        cbFanartPreferredSize_TVShow.SelectedIndexChanged,
        cbForcedLanguage.SelectedIndexChanged,
        cbKeyartPreferredSize_TVShow.SelectedIndexChanged,
        cbLandscapePreferredSize_TVAllSeasons.SelectedIndexChanged,
        cbLandscapePreferredSize_TVSeason.SelectedIndexChanged,
        cbLandscapePreferredSize_TVShow.SelectedIndexChanged,
        cbPosterPreferredSize_TVAllSeasons.SelectedIndexChanged,
        cbPosterPreferredSize_TVEpisode.SelectedIndexChanged,
        cbPosterPreferredSize_TVSeason.SelectedIndexChanged,
        cbPosterPreferredSize_TVShow.SelectedIndexChanged,
        chkActorthumbsKeepExisting_TVEpisode.CheckedChanged,
        chkActorthumbsKeepExisting_TVShow.CheckedChanged,
        chkBannerKeepExisting_TVAllSeasons.CheckedChanged,
        chkBannerKeepExisting_TVSeason.CheckedChanged,
        chkBannerKeepExisting_TVShow.CheckedChanged,
        chkBannerPreferredSizeOnly_TVAllSeasons.CheckedChanged,
        chkBannerPreferredSizeOnly_TVSeason.CheckedChanged,
        chkBannerPreferredSizeOnly_TVShow.CheckedChanged,
        chkCacheEnabled.CheckedChanged,
        chkCharacterartKeepExisting_TVShow.CheckedChanged,
        chkCharacterartPreferredSizeOnly_TVShow.CheckedChanged,
        chkClearartKeepExisting_TVShow.CheckedChanged,
        chkClearartPreferredSizeOnly_TVShow.CheckedChanged,
        chkClearlogoKeepExisting_TVShow.CheckedChanged,
        chkClearlogoPreferredSizeOnly_TVShow.CheckedChanged,
        chkDisplayImageSelectDialog.CheckedChanged,
        chkExtrafanartsKeepExisting_TVShow.CheckedChanged,
        chkExtrafanartsPreferredSizeOnly_TVShow.CheckedChanged,
        chkExtrafanartsPreselect_TVShow.CheckedChanged,
        chkFanartKeepExisting_TVAllSeasons.CheckedChanged,
        chkFanartKeepExisting_TVEpisode.CheckedChanged,
        chkFanartKeepExisting_TVSeason.CheckedChanged,
        chkFanartKeepExisting_TVShow.CheckedChanged,
        chkFanartPreferredSizeOnly_TVAllSeasons.CheckedChanged,
        chkFanartPreferredSizeOnly_TVEpisode.CheckedChanged,
        chkFanartPreferredSizeOnly_TVSeason.CheckedChanged,
        chkFanartPreferredSizeOnly_TVShow.CheckedChanged,
        chkFilterGetBlankImages.CheckedChanged,
        chkFilterGetEnglishImages.CheckedChanged,
        chkKeyartKeepExisting_TVShow.CheckedChanged,
        chkKeyartPreferredSizeOnly_TVShow.CheckedChanged,
        chkLandscapeKeepExisting_TVAllSeasons.CheckedChanged,
        chkLandscapeKeepExisting_TVSeason.CheckedChanged,
        chkLandscapeKeepExisting_TVShow.CheckedChanged,
        chkLandscapePreferredSizeOnly_TVAllSeasons.CheckedChanged,
        chkLandscapePreferredSizeOnly_TVSeason.CheckedChanged,
        chkLandscapePreferredSizeOnly_TVShow.CheckedChanged,
        chkPosterKeepExisting_TVAllSeasons.CheckedChanged,
        chkPosterKeepExisting_TVEpisode.CheckedChanged,
        chkPosterKeepExisting_TVSeason.CheckedChanged,
        chkPosterKeepExisting_TVShow.CheckedChanged,
        chkPosterPreferredSizeOnly_TVAllSeasons.CheckedChanged,
        chkPosterPreferredSizeOnly_TVEpisode.CheckedChanged,
        chkPosterPreferredSizeOnly_TVSeason.CheckedChanged,
        chkPosterPreferredSizeOnly_TVShow.CheckedChanged,
        chkPosterVideoExtractionPreferred_TVEpisode.CheckedChanged,
        txtBannerHeight_TVAllSeasons.TextChanged,
        txtBannerHeight_TVSeason.TextChanged,
        txtBannerHeight_TVShow.TextChanged,
        txtBannerWidth_TVAllSeasons.TextChanged,
        txtBannerWidth_TVSeason.TextChanged,
        txtBannerWidth_TVShow.TextChanged,
        txtExtrafanartsHeight_TVShow.TextChanged,
        txtExtrafanartsLimit_TVShow.TextChanged,
        txtExtrafanartsWidth_TVShow.TextChanged,
        txtFanartHeight_TVAllSeasons.TextChanged,
        txtFanartHeight_TVEpisode.TextChanged,
        txtFanartHeight_TVSeason.TextChanged,
        txtFanartHeight_TVShow.TextChanged,
        txtFanartWidth_TVAllSeasons.TextChanged,
        txtFanartWidth_TVEpisode.TextChanged,
        txtFanartWidth_TVSeason.TextChanged,
        txtFanartWidth_TVShow.TextChanged,
        txtKeyartHeight_TVShow.TextChanged,
        txtKeyartWidth_TVShow.TextChanged,
        txtPosterHeight_TVAllSeasons.TextChanged,
        txtPosterHeight_TVEpisode.TextChanged,
        txtPosterHeight_TVSeason.TextChanged,
        txtPosterHeight_TVShow.TextChanged,
        txtPosterWidth_TVAllSeasons.TextChanged,
        txtPosterWidth_TVEpisode.TextChanged,
        txtPosterWidth_TVSeason.TextChanged,
        txtPosterWidth_TVShow.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub BannerResize_CheckedChanged_TVAllSeasons(ByVal sender As Object, ByVal e As EventArgs) Handles chkBannerResize_TVAllSeasons.CheckedChanged
        txtBannerWidth_TVAllSeasons.Enabled = chkBannerResize_TVAllSeasons.Checked
        txtBannerHeight_TVAllSeasons.Enabled = chkBannerResize_TVAllSeasons.Checked

        If Not chkBannerResize_TVAllSeasons.Checked Then
            txtBannerWidth_TVAllSeasons.Text = String.Empty
            txtBannerHeight_TVAllSeasons.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub BannerResize_CheckedChanged_TVSeason(ByVal sender As Object, ByVal e As EventArgs) Handles chkBannerResize_TVSeason.CheckedChanged
        txtBannerWidth_TVSeason.Enabled = chkBannerResize_TVSeason.Checked
        txtBannerHeight_TVSeason.Enabled = chkBannerResize_TVSeason.Checked

        If Not chkBannerResize_TVSeason.Checked Then
            txtBannerWidth_TVSeason.Text = String.Empty
            txtBannerHeight_TVSeason.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub BannerResize_CheckedChanged_TVShow(ByVal sender As Object, ByVal e As EventArgs) Handles chkBannerResize_TVShow.CheckedChanged
        txtBannerWidth_TVShow.Enabled = chkBannerResize_TVShow.Checked
        txtBannerHeight_TVShow.Enabled = chkBannerResize_TVShow.Checked

        If Not chkBannerResize_TVShow.Checked Then
            txtBannerWidth_TVShow.Text = String.Empty
            txtBannerHeight_TVShow.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ExtrafanartsResize_CheckedChanged_TVShow(ByVal sender As Object, ByVal e As EventArgs) Handles chkExtrafanartsResize_TVShow.CheckedChanged
        txtExtrafanartsWidth_TVShow.Enabled = chkExtrafanartsResize_TVShow.Checked
        txtExtrafanartsHeight_TVShow.Enabled = chkExtrafanartsResize_TVShow.Checked

        If Not chkExtrafanartsResize_TVShow.Checked Then
            txtExtrafanartsWidth_TVShow.Text = String.Empty
            txtExtrafanartsHeight_TVShow.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub FanartResize_CheckedChanged_TVAllseasons(ByVal sender As Object, ByVal e As EventArgs) Handles chkFanartResize_TVAllSeasons.CheckedChanged
        txtFanartWidth_TVAllSeasons.Enabled = chkFanartResize_TVAllSeasons.Checked
        txtFanartHeight_TVAllSeasons.Enabled = chkFanartResize_TVAllSeasons.Checked

        If Not chkFanartResize_TVAllSeasons.Checked Then
            txtFanartWidth_TVAllSeasons.Text = String.Empty
            txtFanartHeight_TVAllSeasons.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub FanartResize_CheckedChanged_TVEpisode(ByVal sender As Object, ByVal e As EventArgs) Handles chkFanartResize_TVEpisode.CheckedChanged
        txtFanartWidth_TVEpisode.Enabled = chkFanartResize_TVEpisode.Checked
        txtFanartHeight_TVEpisode.Enabled = chkFanartResize_TVEpisode.Checked

        If Not chkFanartResize_TVEpisode.Checked Then
            txtFanartWidth_TVEpisode.Text = String.Empty
            txtFanartHeight_TVEpisode.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub FanartResize_CheckedChanged_TVSeason(ByVal sender As Object, ByVal e As EventArgs) Handles chkFanartResize_TVSeason.CheckedChanged
        txtFanartWidth_TVSeason.Enabled = chkFanartResize_TVSeason.Checked
        txtFanartHeight_TVSeason.Enabled = chkFanartResize_TVSeason.Checked

        If Not chkFanartResize_TVSeason.Checked Then
            txtFanartWidth_TVSeason.Text = String.Empty
            txtFanartHeight_TVSeason.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub FanartResize_CheckedChanged_TVShow(ByVal sender As Object, ByVal e As EventArgs) Handles chkFanartResize_TVShow.CheckedChanged
        txtFanartWidth_TVShow.Enabled = chkFanartResize_TVShow.Checked
        txtFanartHeight_TVShow.Enabled = chkFanartResize_TVShow.Checked

        If Not chkFanartResize_TVShow.Checked Then
            txtFanartWidth_TVShow.Text = String.Empty
            txtFanartHeight_TVShow.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub FilterMediaLanguage_CheckedChanged(sender As Object, e As EventArgs) Handles chkFilterMediaLanguage.CheckedChanged
        chkFilterGetBlankImages.Enabled = chkFilterMediaLanguage.Checked
        chkFilterGetEnglishImages.Enabled = chkFilterMediaLanguage.Checked

        If Not chkFilterMediaLanguage.Checked Then
            chkFilterGetBlankImages.Checked = False
            chkFilterGetEnglishImages.Checked = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ForceLanguage_CheckedChanged(sender As Object, e As EventArgs) Handles chkForceLanguage.CheckedChanged
        cbForcedLanguage.Enabled = chkForceLanguage.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub KeyartResize_CheckedChanged_TVShow(ByVal sender As Object, ByVal e As EventArgs) Handles chkKeyartResize_TVShow.CheckedChanged
        txtKeyartWidth_TVShow.Enabled = chkKeyartResize_TVShow.Checked
        txtKeyartHeight_TVShow.Enabled = chkKeyartResize_TVShow.Checked

        If Not chkKeyartResize_TVShow.Checked Then
            txtKeyartWidth_TVShow.Text = String.Empty
            txtKeyartHeight_TVShow.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Load_BannerSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x185", Enums.ImageSize.HD185},
            {"758x140", Enums.ImageSize.HD140}
        }
        LoadItemsToComboBox(cbBannerPreferredSize_TVAllSeasons, items)
        LoadItemsToComboBox(cbBannerPreferredSize_TVSeason, items)
        LoadItemsToComboBox(cbBannerPreferredSize_TVShow, items)
    End Sub

    Private Sub Load_BannerTypes()
        Dim items As New Dictionary(Of String, Enums.TVBannerType) From {
            {Master.eLang.GetString(745, "Any"), Enums.TVBannerType.Any},
            {Master.eLang.GetString(746, "Blank"), Enums.TVBannerType.Blank},
            {Master.eLang.GetString(747, "Graphical"), Enums.TVBannerType.Graphical},
            {Master.eLang.GetString(748, "Text"), Enums.TVBannerType.Text}
        }
        cbBannerPreferredType_TVAllSeasons.DataSource = items.ToList
        cbBannerPreferredType_TVAllSeasons.DisplayMember = "Key"
        cbBannerPreferredType_TVAllSeasons.ValueMember = "Value"
        cbBannerPreferredType_TVSeason.DataSource = items.ToList
        cbBannerPreferredType_TVSeason.DisplayMember = "Key"
        cbBannerPreferredType_TVSeason.ValueMember = "Value"
        cbBannerPreferredType_TVShow.DataSource = items.ToList
        cbBannerPreferredType_TVShow.DisplayMember = "Key"
        cbBannerPreferredType_TVShow.ValueMember = "Value"
    End Sub

    Private Sub Load_CharacterartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"512x512", Enums.ImageSize.HD512}
        }
        LoadItemsToComboBox(cbCharacterartPreferredSize_TVShow, items)
    End Sub

    Private Sub Load_ClearartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x562", Enums.ImageSize.HD562},
            {"500x281", Enums.ImageSize.SD281}
        }
        LoadItemsToComboBox(cbClearartPreferredSize_TVShow, items)
    End Sub

    Private Sub Load_ClearlogoSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"800x310", Enums.ImageSize.HD310},
            {"400x155", Enums.ImageSize.SD155}
        }
        LoadItemsToComboBox(cbClearlogoPreferredSize_TVShow, items)
    End Sub

    Private Sub Load_FanartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"3840x2160", Enums.ImageSize.UHD2160},
            {"2560x1440", Enums.ImageSize.QHD1440},
            {"1920x1080", Enums.ImageSize.HD1080},
            {"1280x720", Enums.ImageSize.HD720}
        }
        LoadItemsToComboBox(cbFanartPreferredSize_TVAllSeasons, items)
        LoadItemsToComboBox(cbFanartPreferredSize_TVEpisode, items)
        LoadItemsToComboBox(cbFanartPreferredSize_TVSeason, items)
        LoadItemsToComboBox(cbExtrafanartsPreferredSize_TVShow, items)
        LoadItemsToComboBox(cbFanartPreferredSize_TVShow, items)
    End Sub

    Private Sub LoadItemsToComboBox(ByRef CBox As ComboBox, Items As Dictionary(Of String, Enums.ImageSize))
        CBox.DataSource = Items.ToList
        CBox.DisplayMember = "Key"
        CBox.ValueMember = "Value"
    End Sub

    Private Sub Load_LandscapeSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x562", Enums.ImageSize.HD562}
        }
        LoadItemsToComboBox(cbLandscapePreferredSize_TVAllSeasons, items)
        LoadItemsToComboBox(cbLandscapePreferredSize_TVSeason, items)
        LoadItemsToComboBox(cbLandscapePreferredSize_TVShow, items)
    End Sub

    Private Sub Load_PosterSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"2000x3000", Enums.ImageSize.HD3000},
            {"1000x1500", Enums.ImageSize.HD1500},
            {"1000x1426", Enums.ImageSize.HD1426},
            {"680x1000", Enums.ImageSize.HD1000}
        }
        LoadItemsToComboBox(cbPosterPreferredSize_TVAllSeasons, items)
        LoadItemsToComboBox(cbKeyartPreferredSize_TVShow, items)
        LoadItemsToComboBox(cbPosterPreferredSize_TVShow, items)

        Dim items2 As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x1500", Enums.ImageSize.HD1500},
            {"1000x1426", Enums.ImageSize.HD1426},
            {"400x578", Enums.ImageSize.HD578}
        }
        LoadItemsToComboBox(cbPosterPreferredSize_TVSeason, items2)

        Dim items3 As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"3840x2160", Enums.ImageSize.UHD2160},
            {"1920x1080", Enums.ImageSize.HD1080},
            {"1280x720", Enums.ImageSize.HD720},
            {"400x225", Enums.ImageSize.SD225}
        }
        LoadItemsToComboBox(cbPosterPreferredSize_TVEpisode, items3)
    End Sub

    Private Sub PosterResize_CheckedChanged_TVAllSeasons(ByVal sender As Object, ByVal e As EventArgs) Handles chkPosterResize_TVAllSeasons.CheckedChanged
        txtPosterWidth_TVAllSeasons.Enabled = chkPosterResize_TVAllSeasons.Checked
        txtPosterHeight_TVAllSeasons.Enabled = chkPosterResize_TVAllSeasons.Checked

        If Not chkPosterResize_TVAllSeasons.Checked Then
            txtPosterWidth_TVAllSeasons.Text = String.Empty
            txtPosterHeight_TVAllSeasons.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub PosterResize_CheckedChanged_TVEpisode(ByVal sender As Object, ByVal e As EventArgs) Handles chkPosterResize_TVEpisode.CheckedChanged
        txtPosterWidth_TVEpisode.Enabled = chkPosterResize_TVEpisode.Checked
        txtPosterHeight_TVEpisode.Enabled = chkPosterResize_TVEpisode.Checked

        If Not chkPosterResize_TVEpisode.Checked Then
            txtPosterWidth_TVEpisode.Text = String.Empty
            txtPosterHeight_TVEpisode.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub PosterResize_CheckedChanged_TVSeason(ByVal sender As Object, ByVal e As EventArgs) Handles chkPosterResize_TVSeason.CheckedChanged
        txtPosterWidth_TVSeason.Enabled = chkPosterResize_TVSeason.Checked
        txtPosterHeight_TVSeason.Enabled = chkPosterResize_TVSeason.Checked

        If Not chkPosterResize_TVSeason.Checked Then
            txtPosterWidth_TVSeason.Text = String.Empty
            txtPosterHeight_TVSeason.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub PosterResize_CheckedChanged_TVShow(ByVal sender As Object, ByVal e As EventArgs) Handles chkPosterResize_TVShow.CheckedChanged
        txtPosterWidth_TVShow.Enabled = chkPosterResize_TVShow.Checked
        txtPosterHeight_TVShow.Enabled = chkPosterResize_TVShow.Checked

        If Not chkPosterResize_TVShow.Checked Then
            txtPosterWidth_TVShow.Text = String.Empty
            txtPosterHeight_TVShow.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub PosterVideoExtraction_CheckedChanged_TVEpisode(ByVal sender As Object, ByVal e As EventArgs) Handles chkPosterVideoExtraction_TVEpisode.CheckedChanged
        chkPosterVideoExtractionPreferred_TVEpisode.Enabled = chkPosterVideoExtraction_TVEpisode.Checked
        If Not chkPosterVideoExtraction_TVEpisode.Checked Then
            chkPosterVideoExtractionPreferred_TVEpisode.Checked = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Public Sub Settings_Load()
        'Global
        '(loaded from "TVShow", should be the same settings like "TVAllSeasons", "TVSeasons" and "TVEpisode")
        With Master.eSettings.TVShow.ImageSettings
            'Options
            chkCacheEnabled.Checked = .CacheEnabled
            chkDisplayImageSelectDialog.Checked = .DisplayImageSelectDialog

            'Language
            chkForceLanguage.Checked = .ForceLanguage
            Try
                cbForcedLanguage.Items.Clear()
                cbForcedLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Name).Distinct.ToArray)
                If cbForcedLanguage.Items.Count > 0 Then
                    If .ForcedLanguageSpecified Then
                        Dim tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = .ForcedLanguage)
                        If tLanguage IsNot Nothing Then
                            cbForcedLanguage.Text = tLanguage.Name
                        Else
                            tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.ForcedLanguage))
                            If tLanguage IsNot Nothing Then
                                cbForcedLanguage.Text = tLanguage.Name
                            Else
                                cbForcedLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                            End If
                        End If
                    Else
                        cbForcedLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                    End If
                End If
            Catch ex As Exception
                'logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
            If .FilterMediaLanguage Then
                chkFilterMediaLanguage.Checked = True
                chkFilterGetBlankImages.Checked = .FilterGetBlankImages
                chkFilterGetEnglishImages.Checked = .FilterGetEnglishImages
            End If
        End With

        'ImageTypes
        'TVAllSeasons
        With Master.eSettings.TVAllSeasons.ImageSettings
            'Banner
            cbBannerPreferredSize_TVAllSeasons.SelectedValue = .Banner.PreferredSize
            chkBannerKeepExisting_TVAllSeasons.Checked = .Banner.KeepExisting
            chkBannerPreferredSizeOnly_TVAllSeasons.Checked = .Banner.PreferredSizeOnly
            chkBannerResize_TVAllSeasons.Checked = .Banner.Resize
            If .Banner.Resize Then
                txtBannerHeight_TVAllSeasons.Text = .Banner.MaxHeight.ToString
                txtBannerWidth_TVAllSeasons.Text = .Banner.MaxWidth.ToString
            End If
            'Fanart
            cbFanartPreferredSize_TVAllSeasons.SelectedValue = .Fanart.PreferredSize
            chkFanartKeepExisting_TVAllSeasons.Checked = .Fanart.KeepExisting
            chkFanartPreferredSizeOnly_TVAllSeasons.Checked = .Fanart.PreferredSizeOnly
            chkFanartResize_TVAllSeasons.Checked = .Fanart.Resize
            If .Fanart.Resize Then
                txtFanartHeight_TVAllSeasons.Text = .Fanart.MaxHeight.ToString
                txtFanartWidth_TVAllSeasons.Text = .Fanart.MaxWidth.ToString
            End If
            'Landscape
            cbLandscapePreferredSize_TVAllSeasons.SelectedValue = .Landscape.PreferredSize
            chkLandscapeKeepExisting_TVAllSeasons.Checked = .Landscape.KeepExisting
            chkLandscapePreferredSizeOnly_TVAllSeasons.Checked = .Landscape.PreferredSizeOnly
            'Poster
            cbPosterPreferredSize_TVAllSeasons.SelectedValue = .Poster.PreferredSize
            chkPosterKeepExisting_TVAllSeasons.Checked = .Poster.KeepExisting
            chkPosterPreferredSizeOnly_TVAllSeasons.Checked = .Poster.PreferredSizeOnly
            chkPosterResize_TVAllSeasons.Checked = .Poster.Resize
            If .Poster.Resize Then
                txtPosterHeight_TVAllSeasons.Text = .Poster.MaxHeight.ToString
                txtPosterWidth_TVAllSeasons.Text = .Poster.MaxWidth.ToString
            End If
        End With

        'TVEpisode
        With Master.eSettings.TVEpisode.ImageSettings
            'Actorthumbs
            chkActorthumbsKeepExisting_TVEpisode.Checked = .Actorthumbs.KeepExisting
            'Fanart
            cbFanartPreferredSize_TVEpisode.SelectedValue = .Fanart.PreferredSize
            chkFanartKeepExisting_TVEpisode.Checked = .Fanart.KeepExisting
            chkFanartPreferredSizeOnly_TVEpisode.Checked = .Fanart.PreferredSizeOnly
            chkFanartResize_TVEpisode.Checked = .Fanart.Resize
            If .Fanart.Resize Then
                txtFanartHeight_TVEpisode.Text = .Fanart.MaxHeight.ToString
                txtFanartWidth_TVEpisode.Text = .Fanart.MaxWidth.ToString
            End If
            'Poster
            cbPosterPreferredSize_TVEpisode.SelectedValue = .Poster.PreferredSize
            chkPosterKeepExisting_TVEpisode.Checked = .Poster.KeepExisting
            chkPosterPreferredSizeOnly_TVEpisode.Checked = .Poster.PreferredSizeOnly
            chkPosterResize_TVEpisode.Checked = .Poster.Resize
            If .Poster.Resize Then
                txtPosterHeight_TVEpisode.Text = .Poster.MaxHeight.ToString
                txtPosterWidth_TVEpisode.Text = .Poster.MaxWidth.ToString
            End If
            chkPosterVideoExtraction_TVEpisode.Checked = .Extrathumbs.VideoExtraction
            chkPosterVideoExtractionPreferred_TVEpisode.Checked = .Extrathumbs.VideoExtractionPreferred
        End With

        'TVSeason
        With Master.eSettings.TVSeason.ImageSettings
            'Banner
            cbBannerPreferredSize_TVSeason.SelectedValue = .Banner.PreferredSize
            chkBannerKeepExisting_TVSeason.Checked = .Banner.KeepExisting
            chkBannerPreferredSizeOnly_TVSeason.Checked = .Banner.PreferredSizeOnly
            chkBannerResize_TVSeason.Checked = .Banner.Resize
            If .Banner.Resize Then
                txtBannerHeight_TVSeason.Text = .Banner.MaxHeight.ToString
                txtBannerWidth_TVSeason.Text = .Banner.MaxWidth.ToString
            End If
            'Fanart
            cbFanartPreferredSize_TVSeason.SelectedValue = .Fanart.PreferredSize
            chkFanartKeepExisting_TVSeason.Checked = .Fanart.KeepExisting
            chkFanartPreferredSizeOnly_TVSeason.Checked = .Fanart.PreferredSizeOnly
            chkFanartResize_TVSeason.Checked = .Fanart.Resize
            If .Fanart.Resize Then
                txtFanartHeight_TVSeason.Text = .Fanart.MaxHeight.ToString
                txtFanartWidth_TVSeason.Text = .Fanart.MaxWidth.ToString
            End If
            'Landscape
            cbLandscapePreferredSize_TVSeason.SelectedValue = .Landscape.PreferredSize
            chkLandscapeKeepExisting_TVSeason.Checked = .Landscape.KeepExisting
            chkLandscapePreferredSizeOnly_TVSeason.Checked = .Landscape.PreferredSizeOnly
            'Poster
            cbPosterPreferredSize_TVSeason.SelectedValue = .Poster.PreferredSize
            chkPosterKeepExisting_TVSeason.Checked = .Poster.KeepExisting
            chkPosterPreferredSizeOnly_TVSeason.Checked = .Poster.PreferredSizeOnly
            chkPosterResize_TVSeason.Checked = .Poster.Resize
            If .Poster.Resize Then
                txtPosterHeight_TVSeason.Text = .Poster.MaxHeight.ToString
                txtPosterWidth_TVSeason.Text = .Poster.MaxWidth.ToString
            End If
        End With

        'TVShow
        With Master.eSettings.TVShow.ImageSettings
            'Actorthumbs
            chkActorthumbsKeepExisting_TVShow.Checked = .Actorthumbs.KeepExisting
            'Banner
            cbBannerPreferredSize_TVShow.SelectedValue = .Banner.PreferredSize
            chkBannerKeepExisting_TVShow.Checked = .Banner.KeepExisting
            chkBannerPreferredSizeOnly_TVShow.Checked = .Banner.PreferredSizeOnly
            chkBannerResize_TVShow.Checked = .Banner.Resize
            If .Banner.Resize Then
                txtBannerHeight_TVShow.Text = .Banner.MaxHeight.ToString
                txtBannerWidth_TVShow.Text = .Banner.MaxWidth.ToString
            End If
            'Characterart
            cbCharacterartPreferredSize_TVShow.SelectedValue = .Discart.PreferredSize
            chkCharacterartKeepExisting_TVShow.Checked = .Discart.KeepExisting
            chkCharacterartPreferredSizeOnly_TVShow.Checked = .Discart.PreferredSizeOnly
            'Clearart
            cbClearartPreferredSize_TVShow.SelectedValue = .Clearart.PreferredSize
            chkClearartKeepExisting_TVShow.Checked = .Clearart.KeepExisting
            chkClearartPreferredSizeOnly_TVShow.Checked = .Clearart.PreferredSizeOnly
            'Clearlogo
            cbClearlogoPreferredSize_TVShow.SelectedValue = .Clearlogo.PreferredSize
            chkClearlogoKeepExisting_TVShow.Checked = .Clearlogo.KeepExisting
            chkClearlogoPreferredSizeOnly_TVShow.Checked = .Clearlogo.PreferredSizeOnly
            'Extrafanarts
            cbExtrafanartsPreferredSize_TVShow.SelectedValue = .Extrafanarts.PreferredSize
            chkExtrafanartsKeepExisting_TVShow.Checked = .Extrafanarts.KeepExisting
            chkExtrafanartsPreferredSizeOnly_TVShow.Checked = .Extrafanarts.PreferredSizeOnly
            chkExtrafanartsPreselect_TVShow.Checked = .Extrafanarts.Preselect
            chkExtrafanartsResize_TVShow.Checked = .Extrafanarts.Resize
            If .Extrafanarts.Resize Then
                txtExtrafanartsHeight_TVShow.Text = .Extrafanarts.MaxHeight.ToString
                txtExtrafanartsWidth_TVShow.Text = .Extrafanarts.MaxWidth.ToString
            End If
            txtExtrafanartsLimit_TVShow.Text = .Extrafanarts.Limit.ToString
            'Fanart
            cbFanartPreferredSize_TVShow.SelectedValue = .Fanart.PreferredSize
            chkFanartKeepExisting_TVShow.Checked = .Fanart.KeepExisting
            chkFanartPreferredSizeOnly_TVShow.Checked = .Fanart.PreferredSizeOnly
            chkFanartResize_TVShow.Checked = .Fanart.Resize
            If .Fanart.Resize Then
                txtFanartHeight_TVShow.Text = .Fanart.MaxHeight.ToString
                txtFanartWidth_TVShow.Text = .Fanart.MaxWidth.ToString
            End If
            'Keyart
            cbKeyartPreferredSize_TVShow.SelectedValue = .Keyart.PreferredSize
            chkKeyartKeepExisting_TVShow.Checked = .Keyart.KeepExisting
            chkKeyartPreferredSizeOnly_TVShow.Checked = .Keyart.PreferredSizeOnly
            chkKeyartResize_TVShow.Checked = .Keyart.Resize
            If .Keyart.Resize Then
                txtKeyartHeight_TVShow.Text = .Keyart.MaxHeight.ToString
                txtKeyartWidth_TVShow.Text = .Keyart.MaxWidth.ToString
            End If
            'Landscape
            cbLandscapePreferredSize_TVShow.SelectedValue = .Landscape.PreferredSize
            chkLandscapeKeepExisting_TVShow.Checked = .Landscape.KeepExisting
            chkLandscapePreferredSizeOnly_TVShow.Checked = .Landscape.PreferredSizeOnly
            'Poster
            cbPosterPreferredSize_TVShow.SelectedValue = .Poster.PreferredSize
            chkPosterKeepExisting_TVShow.Checked = .Poster.KeepExisting
            chkPosterPreferredSizeOnly_TVShow.Checked = .Poster.PreferredSizeOnly
            chkPosterResize_TVShow.Checked = .Poster.Resize
            If .Poster.Resize Then
                txtPosterHeight_TVShow.Text = .Poster.MaxHeight.ToString
                txtPosterWidth_TVShow.Text = .Poster.MaxWidth.ToString
            End If
        End With
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        txtFanartWidth_TVShow.KeyPress,
        txtPosterWidth_TVShow.KeyPress,
        txtPosterWidth_TVSeason.KeyPress,
        txtPosterWidth_TVEpisode.KeyPress,
        txtPosterWidth_TVAllSeasons.KeyPress,
        txtPosterHeight_TVShow.KeyPress,
        txtPosterHeight_TVSeason.KeyPress,
        txtPosterHeight_TVEpisode.KeyPress,
        txtPosterHeight_TVAllSeasons.KeyPress,
        txtKeyartWidth_TVShow.KeyPress,
        txtKeyartHeight_TVShow.KeyPress,
        txtFanartWidth_TVSeason.KeyPress,
        txtFanartWidth_TVEpisode.KeyPress,
        txtFanartWidth_TVAllSeasons.KeyPress,
        txtFanartHeight_TVShow.KeyPress,
        txtFanartHeight_TVSeason.KeyPress,
        txtFanartHeight_TVEpisode.KeyPress,
        txtFanartHeight_TVAllSeasons.KeyPress,
        txtExtrafanartsWidth_TVShow.KeyPress,
        txtExtrafanartsLimit_TVShow.KeyPress,
        txtExtrafanartsHeight_TVShow.KeyPress,
        txtBannerWidth_TVShow.KeyPress,
        txtBannerWidth_TVSeason.KeyPress,
        txtBannerWidth_TVAllSeasons.KeyPress,
        txtBannerHeight_TVShow.KeyPress,
        txtBannerHeight_TVSeason.KeyPress,
        txtBannerHeight_TVAllSeasons.KeyPress

        e.Handled = StringUtils.IntegerOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_Limit_Leave(sender As Object, e As EventArgs) Handles _
        txtExtrafanartsLimit_TVShow.Leave

        Dim iLimit As Integer
        Dim tTextBox = CType(sender, TextBox)
        If Not Integer.TryParse(tTextBox.Text, iLimit) OrElse iLimit > Settings._ExtraImagesLimit OrElse iLimit = 0 Then
            Dim strTitle As String = Master.eLang.GetString(934, "Image Limit")
            Dim strText As String = Master.eLang.GetString(935, "We have to limit the amount of images downloaded to a suitable value to prevent needless traffic on the image providers.{0}The limit for automatically downloaded Extrafanarts and Extrathumbs is 20.{0}{0}Notes: Most skins can't show more than 4 Extrathumbs.{0}It's still possible to manually select as many as you want in the ""Image Select"" dialog.")
            MessageBox.Show(String.Format(strText, Environment.NewLine), strTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            tTextBox.Text = "20"
        End If
    End Sub

#End Region 'Methods

End Class