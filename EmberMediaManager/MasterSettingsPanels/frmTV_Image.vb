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
    Implements Interfaces.IMasterSettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

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
            .Contains = Enums.SettingsPanelType.TVImage,
            .ImageIndex = 6,
            .Order = 500,
            .Panel = pnlSettings,
            .SettingsPanelID = "TV_Image",
            .Title = Master.eLang.GetString(497, "Images"),
            .Type = Enums.SettingsPanelType.TV
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings
            .TVAllSeasonsBannerHeight = If(Not String.IsNullOrEmpty(txtTVAllSeasonsBannerHeight.Text), Convert.ToInt32(txtTVAllSeasonsBannerHeight.Text), 0)
            .TVAllSeasonsBannerKeepExisting = chkTVAllSeasonsBannerKeepExisting.Checked
            .TVAllSeasonsBannerPrefSize = CType(cbTVAllSeasonsBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVAllSeasonsBannerPrefSizeOnly = chkTVAllSeasonsBannerPrefSizeOnly.Checked
            .TVAllSeasonsBannerPrefType = CType(cbTVAllSeasonsBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVAllSeasonsBannerResize = chkTVAllSeasonsBannerResize.Checked
            .TVAllSeasonsBannerWidth = If(Not String.IsNullOrEmpty(txtTVAllSeasonsBannerWidth.Text), Convert.ToInt32(txtTVAllSeasonsBannerWidth.Text), 0)
            .TVAllSeasonsFanartHeight = If(Not String.IsNullOrEmpty(txtTVAllSeasonsFanartHeight.Text), Convert.ToInt32(txtTVAllSeasonsFanartHeight.Text), 0)
            .TVAllSeasonsFanartKeepExisting = chkTVAllSeasonsFanartKeepExisting.Checked
            .TVAllSeasonsFanartPrefSize = CType(cbTVAllSeasonsFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVAllSeasonsFanartPrefSizeOnly = chkTVAllSeasonsFanartPrefSizeOnly.Checked
            .TVAllSeasonsFanartResize = chkTVAllSeasonsFanartResize.Checked
            .TVAllSeasonsFanartWidth = If(Not String.IsNullOrEmpty(txtTVAllSeasonsFanartWidth.Text), Convert.ToInt32(txtTVAllSeasonsFanartWidth.Text), 0)
            .TVAllSeasonsLandscapeKeepExisting = chkTVAllSeasonsLandscapeKeepExisting.Checked
            .TVAllSeasonsLandscapePrefSize = CType(cbTVAllSeasonsLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVAllSeasonsLandscapePrefSizeOnly = chkTVAllSeasonsLandscapePrefSizeOnly.Checked
            .TVAllSeasonsPosterHeight = If(Not String.IsNullOrEmpty(txtTVAllSeasonsPosterHeight.Text), Convert.ToInt32(txtTVAllSeasonsPosterHeight.Text), 0)
            .TVAllSeasonsPosterKeepExisting = chkTVAllSeasonsPosterKeepExisting.Checked
            .TVAllSeasonsPosterPrefSize = CType(cbTVAllSeasonsPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVAllSeasonsPosterPrefSizeOnly = chkTVAllSeasonsPosterPrefSizeOnly.Checked
            .TVAllSeasonsPosterResize = chkTVAllSeasonsPosterResize.Checked
            .TVAllSeasonsPosterWidth = If(Not String.IsNullOrEmpty(txtTVAllSeasonsPosterWidth.Text), Convert.ToInt32(txtTVAllSeasonsPosterWidth.Text), 0)
            .TVEpisodeFanartHeight = If(Not String.IsNullOrEmpty(txtTVEpisodeFanartHeight.Text), Convert.ToInt32(txtTVEpisodeFanartHeight.Text), 0)
            .TVEpisodeFanartKeepExisting = chkTVEpisodeFanartKeepExisting.Checked
            .TVEpisodeFanartPrefSize = CType(cbTVEpisodeFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVEpisodeFanartPrefSizeOnly = chkTVEpisodeFanartPrefSizeOnly.Checked
            .TVEpisodeFanartResize = chkTVEpisodeFanartResize.Checked
            .TVEpisodeFanartWidth = If(Not String.IsNullOrEmpty(txtTVEpisodeFanartWidth.Text), Convert.ToInt32(txtTVEpisodeFanartWidth.Text), 0)
            .TVEpisodePosterHeight = If(Not String.IsNullOrEmpty(txtTVEpisodePosterHeight.Text), Convert.ToInt32(txtTVEpisodePosterHeight.Text), 0)
            .TVEpisodePosterKeepExisting = chkTVEpisodePosterKeepExisting.Checked
            .TVEpisodePosterPrefSize = CType(cbTVEpisodePosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVEpisodePosterPrefSizeOnly = chkTVEpisodePosterPrefSizeOnly.Checked
            .TVEpisodePosterResize = chkTVEpisodePosterResize.Checked
            .TVEpisodePosterVideoExtraction = chkTVEpisodePosterVideoExtraction.Checked
            .TVEpisodePosterWidth = If(Not String.IsNullOrEmpty(txtTVEpisodePosterWidth.Text), Convert.ToInt32(txtTVEpisodePosterWidth.Text), 0)
            .TVImagesCacheEnabled = chkTVImagesCacheEnabled.Checked
            .TVImagesDisplayImageSelect = chkTVImagesDisplayImageSelect.Checked
            If Not String.IsNullOrEmpty(cbTVImagesForcedLanguage.Text) Then
                .TVImagesForcedLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Name = cbTVImagesForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .TVImagesForceLanguage = chkTVImagesForceLanguage.Checked
            .TVImagesGetBlankImages = chkTVImagesGetBlankImages.Checked
            .TVImagesGetEnglishImages = chkTVImagesGetEnglishImages.Checked
            .TVImagesMediaLanguageOnly = chkTVImagesMediaLanguageOnly.Checked
            .TVSeasonBannerHeight = If(Not String.IsNullOrEmpty(txtTVSeasonBannerHeight.Text), Convert.ToInt32(txtTVSeasonBannerHeight.Text), 0)
            .TVSeasonBannerKeepExisting = chkTVSeasonBannerKeepExisting.Checked
            .TVSeasonBannerPrefSize = CType(cbTVSeasonBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVSeasonBannerPrefSizeOnly = chkTVSeasonBannerPrefSizeOnly.Checked
            .TVSeasonBannerPrefType = CType(cbTVSeasonBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVSeasonBannerResize = chkTVSeasonBannerResize.Checked
            .TVSeasonBannerWidth = If(Not String.IsNullOrEmpty(txtTVSeasonBannerWidth.Text), Convert.ToInt32(txtTVSeasonBannerWidth.Text), 0)
            .TVSeasonFanartHeight = If(Not String.IsNullOrEmpty(txtTVSeasonFanartHeight.Text), Convert.ToInt32(txtTVSeasonFanartHeight.Text), 0)
            .TVSeasonFanartKeepExisting = chkTVSeasonFanartKeepExisting.Checked
            .TVSeasonFanartPrefSize = CType(cbTVSeasonFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVSeasonFanartPrefSizeOnly = chkTVSeasonFanartPrefSizeOnly.Checked
            .TVSeasonFanartResize = chkTVSeasonFanartResize.Checked
            .TVSeasonFanartWidth = If(Not String.IsNullOrEmpty(txtTVSeasonFanartWidth.Text), Convert.ToInt32(txtTVSeasonFanartWidth.Text), 0)
            .TVSeasonLandscapeKeepExisting = chkTVSeasonLandscapeKeepExisting.Checked
            .TVSeasonLandscapePrefSize = CType(cbTVSeasonLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVSeasonLandscapePrefSizeOnly = chkTVSeasonLandscapePrefSizeOnly.Checked
            .TVSeasonPosterHeight = If(Not String.IsNullOrEmpty(txtTVSeasonPosterHeight.Text), Convert.ToInt32(txtTVSeasonPosterHeight.Text), 0)
            .TVSeasonPosterKeepExisting = chkTVSeasonPosterKeepExisting.Checked
            .TVSeasonPosterPrefSize = CType(cbTVSeasonPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVSeasonPosterPrefSizeOnly = chkTVSeasonPosterPrefSizeOnly.Checked
            .TVSeasonPosterResize = chkTVSeasonPosterResize.Checked
            .TVSeasonPosterWidth = If(Not String.IsNullOrEmpty(txtTVSeasonPosterWidth.Text), Convert.ToInt32(txtTVSeasonPosterWidth.Text), 0)
            .TVShowBannerHeight = If(Not String.IsNullOrEmpty(txtTVShowBannerHeight.Text), Convert.ToInt32(txtTVShowBannerHeight.Text), 0)
            .TVShowBannerKeepExisting = chkTVShowBannerKeepExisting.Checked
            .TVShowBannerPrefSize = CType(cbTVShowBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowBannerPrefSizeOnly = chkTVShowBannerPrefSizeOnly.Checked
            .TVShowBannerPrefType = CType(cbTVShowBannerPrefType.SelectedItem, KeyValuePair(Of String, Enums.TVBannerType)).Value
            .TVShowBannerResize = chkTVShowBannerResize.Checked
            .TVShowBannerWidth = If(Not String.IsNullOrEmpty(txtTVShowBannerWidth.Text), Convert.ToInt32(txtTVShowBannerWidth.Text), 0)
            .TVShowCharacterArtKeepExisting = chkTVShowCharacterArtKeepExisting.Checked
            .TVShowCharacterArtPrefSize = CType(cbTVShowCharacterArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowCharacterArtPrefSizeOnly = chkTVShowCharacterArtPrefSizeOnly.Checked
            .TVShowClearArtKeepExisting = chkTVShowClearArtKeepExisting.Checked
            .TVShowClearArtPrefSize = CType(cbTVShowClearArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowClearArtPrefSizeOnly = chkTVShowClearArtPrefSizeOnly.Checked
            .TVShowClearLogoKeepExisting = chkTVShowClearLogoKeepExisting.Checked
            .TVShowClearLogoPrefSize = CType(cbTVShowClearLogoPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowClearLogoPrefSizeOnly = chkTVShowClearLogoPrefSizeOnly.Checked
            .TVShowExtrafanartsHeight = If(Not String.IsNullOrEmpty(txtTVShowExtrafanartsHeight.Text), Convert.ToInt32(txtTVShowExtrafanartsHeight.Text), 0)
            .TVShowExtrafanartsLimit = If(Not String.IsNullOrEmpty(txtTVShowExtrafanartsLimit.Text), Convert.ToInt32(txtTVShowExtrafanartsLimit.Text), 0)
            .TVShowExtrafanartsKeepExisting = chkTVShowExtrafanartsKeepExisting.Checked
            .TVShowExtrafanartsPrefSize = CType(cbTVShowExtrafanartsPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowExtrafanartsPrefSizeOnly = chkTVShowExtrafanartsPrefSizeOnly.Checked
            .TVShowExtrafanartsPreselect = chkTVShowExtrafanartsPreselect.Checked
            .TVShowExtrafanartsResize = chkTVShowExtrafanartsResize.Checked
            .TVShowExtrafanartsWidth = If(Not String.IsNullOrEmpty(txtTVShowExtrafanartsWidth.Text), Convert.ToInt32(txtTVShowExtrafanartsWidth.Text), 0)
            .TVShowFanartHeight = If(Not String.IsNullOrEmpty(txtTVShowFanartHeight.Text), Convert.ToInt32(txtTVShowFanartHeight.Text), 0)
            .TVShowFanartKeepExisting = chkTVShowFanartKeepExisting.Checked
            .TVShowFanartPrefSize = CType(cbTVShowFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowFanartPrefSizeOnly = chkTVShowFanartPrefSizeOnly.Checked
            .TVShowFanartResize = chkTVShowFanartResize.Checked
            .TVShowFanartWidth = If(Not String.IsNullOrEmpty(txtTVShowFanartWidth.Text), Convert.ToInt32(txtTVShowFanartWidth.Text), 0)
            .TVShowKeyArtHeight = If(Not String.IsNullOrEmpty(txtTVShowKeyArtHeight.Text), Convert.ToInt32(txtTVShowKeyArtHeight.Text), 0)
            .TVShowKeyArtKeepExisting = chkTVShowKeyArtKeepExisting.Checked
            .TVShowKeyArtPrefSize = CType(cbTVShowKeyArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowKeyArtPrefSizeOnly = chkTVShowKeyArtPrefSizeOnly.Checked
            .TVShowKeyArtResize = chkTVShowKeyArtResize.Checked
            .TVShowKeyArtWidth = If(Not String.IsNullOrEmpty(txtTVShowKeyArtWidth.Text), Convert.ToInt32(txtTVShowKeyArtWidth.Text), 0)
            .TVShowLandscapeKeepExisting = chkTVShowLandscapeKeepExisting.Checked
            .TVShowLandscapePrefSize = CType(cbTVShowLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowLandscapePrefSizeOnly = chkTVShowLandscapePrefSizeOnly.Checked
            .TVShowPosterHeight = If(Not String.IsNullOrEmpty(txtTVShowPosterHeight.Text), Convert.ToInt32(txtTVShowPosterHeight.Text), 0)
            .TVShowPosterKeepExisting = chkTVShowPosterKeepExisting.Checked
            .TVShowPosterPrefSize = CType(cbTVShowPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .TVShowPosterPrefSizeOnly = chkTVShowPosterPrefSizeOnly.Checked
            .TVShowPosterResize = chkTVShowPosterResize.Checked
            .TVShowPosterWidth = If(Not String.IsNullOrEmpty(txtTVShowPosterWidth.Text), Convert.ToInt32(txtTVShowPosterWidth.Text), 0)
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            cbTVAllSeasonsBannerPrefSize.SelectedValue = .TVAllSeasonsBannerPrefSize
            cbTVAllSeasonsBannerPrefType.SelectedValue = .TVAllSeasonsBannerPrefType
            cbTVAllSeasonsFanartPrefSize.SelectedValue = .TVAllSeasonsFanartPrefSize
            cbTVAllSeasonsLandscapePrefSize.SelectedValue = .TVAllSeasonsLandscapePrefSize
            cbTVAllSeasonsPosterPrefSize.SelectedValue = .TVAllSeasonsPosterPrefSize
            cbTVEpisodeFanartPrefSize.SelectedValue = .TVEpisodeFanartPrefSize
            cbTVEpisodePosterPrefSize.SelectedValue = .TVEpisodePosterPrefSize
            cbTVSeasonBannerPrefSize.SelectedValue = .TVSeasonBannerPrefSize
            cbTVSeasonBannerPrefType.SelectedValue = .TVSeasonBannerPrefType
            cbTVSeasonFanartPrefSize.SelectedValue = .TVSeasonFanartPrefSize
            cbTVSeasonLandscapePrefSize.SelectedValue = .TVSeasonLandscapePrefSize
            cbTVSeasonPosterPrefSize.SelectedValue = .TVSeasonPosterPrefSize
            cbTVShowBannerPrefSize.SelectedValue = .TVShowBannerPrefSize
            cbTVShowBannerPrefType.SelectedValue = .TVShowBannerPrefType
            cbTVShowCharacterArtPrefSize.SelectedValue = .TVShowCharacterArtPrefSize
            cbTVShowClearArtPrefSize.SelectedValue = .TVShowClearArtPrefSize
            cbTVShowClearLogoPrefSize.SelectedValue = .TVShowClearLogoPrefSize
            cbTVShowExtrafanartsPrefSize.SelectedValue = .TVShowExtrafanartsPrefSize
            cbTVShowFanartPrefSize.SelectedValue = .TVShowFanartPrefSize
            cbTVShowKeyArtPrefSize.SelectedValue = .TVShowKeyArtPrefSize
            cbTVShowLandscapePrefSize.SelectedValue = .TVShowLandscapePrefSize
            cbTVShowPosterPrefSize.SelectedValue = .TVShowPosterPrefSize
            chkTVAllSeasonsBannerKeepExisting.Checked = .TVAllSeasonsBannerKeepExisting
            chkTVAllSeasonsBannerPrefSizeOnly.Checked = .TVAllSeasonsBannerPrefSizeOnly
            chkTVAllSeasonsBannerResize.Checked = .TVAllSeasonsBannerResize
            If .TVAllSeasonsBannerResize Then
                txtTVAllSeasonsBannerHeight.Text = .TVAllSeasonsBannerHeight.ToString
                txtTVAllSeasonsBannerWidth.Text = .TVAllSeasonsBannerWidth.ToString
            End If
            chkTVAllSeasonsFanartKeepExisting.Checked = .TVAllSeasonsFanartKeepExisting
            chkTVAllSeasonsFanartPrefSizeOnly.Checked = .TVAllSeasonsFanartPrefSizeOnly
            chkTVAllSeasonsFanartResize.Checked = .TVAllSeasonsFanartResize
            If .TVAllSeasonsFanartResize Then
                txtTVAllSeasonsFanartHeight.Text = .TVAllSeasonsFanartHeight.ToString
                txtTVAllSeasonsFanartWidth.Text = .TVAllSeasonsFanartWidth.ToString
            End If
            chkTVAllSeasonsLandscapeKeepExisting.Checked = .TVAllSeasonsLandscapeKeepExisting
            chkTVAllSeasonsLandscapePrefSizeOnly.Checked = .TVAllSeasonsLandscapePrefSizeOnly
            chkTVAllSeasonsPosterKeepExisting.Checked = .TVAllSeasonsPosterKeepExisting
            chkTVAllSeasonsPosterPrefSizeOnly.Checked = .TVAllSeasonsPosterPrefSizeOnly
            chkTVAllSeasonsPosterResize.Checked = .TVAllSeasonsPosterResize
            If .TVAllSeasonsPosterResize Then
                txtTVAllSeasonsPosterHeight.Text = .TVAllSeasonsPosterHeight.ToString
                txtTVAllSeasonsPosterWidth.Text = .TVAllSeasonsPosterWidth.ToString
            End If
            chkTVEpisodeFanartKeepExisting.Checked = .TVEpisodeFanartKeepExisting
            chkTVEpisodeFanartPrefSizeOnly.Checked = .TVEpisodeFanartPrefSizeOnly
            chkTVEpisodeFanartResize.Checked = .TVEpisodeFanartResize
            If .TVEpisodeFanartResize Then
                txtTVEpisodeFanartHeight.Text = .TVEpisodeFanartHeight.ToString
                txtTVEpisodeFanartWidth.Text = .TVEpisodeFanartWidth.ToString
            End If
            chkTVEpisodePosterKeepExisting.Checked = .TVEpisodePosterKeepExisting
            chkTVEpisodePosterPrefSizeOnly.Checked = .TVEpisodePosterPrefSizeOnly
            chkTVEpisodePosterResize.Checked = .TVEpisodePosterResize
            If .TVEpisodePosterResize Then
                txtTVEpisodePosterHeight.Text = .TVEpisodePosterHeight.ToString
                txtTVEpisodePosterWidth.Text = .TVEpisodePosterWidth.ToString
            End If
            chkTVEpisodePosterVideoExtraction.Checked = .TVEpisodePosterVideoExtraction
            chkTVEpisodePosterVideoExtractionPref.Checked = .TVEpisodePosterVideoExtractionPref
            chkTVImagesCacheEnabled.Checked = .TVImagesCacheEnabled
            chkTVImagesDisplayImageSelect.Checked = .TVImagesDisplayImageSelect
            chkTVImagesForceLanguage.Checked = .TVImagesForceLanguage
            If .TVImagesMediaLanguageOnly Then
                chkTVImagesMediaLanguageOnly.Checked = True
                chkTVImagesGetBlankImages.Checked = .TVImagesGetBlankImages
                chkTVImagesGetEnglishImages.Checked = .TVImagesGetEnglishImages
            End If
            chkTVSeasonBannerKeepExisting.Checked = .TVSeasonBannerKeepExisting
            chkTVSeasonBannerPrefSizeOnly.Checked = .TVSeasonBannerPrefSizeOnly
            chkTVSeasonBannerResize.Checked = .TVSeasonBannerResize
            If .TVSeasonBannerResize Then
                txtTVSeasonBannerHeight.Text = .TVSeasonBannerHeight.ToString
                txtTVSeasonBannerWidth.Text = .TVSeasonBannerWidth.ToString
            End If
            chkTVShowExtrafanartsKeepExisting.Checked = .TVShowExtrafanartsKeepExisting
            chkTVShowExtrafanartsPrefSizeOnly.Checked = .TVShowExtrafanartsPrefSizeOnly
            chkTVShowExtrafanartsPreselect.Checked = .TVShowExtrafanartsPreselect
            chkTVShowExtrafanartsResize.Checked = .TVShowExtrafanartsResize
            If .TVShowExtrafanartsResize Then
                txtTVShowExtrafanartsHeight.Text = .TVShowExtrafanartsHeight.ToString
                txtTVShowExtrafanartsWidth.Text = .TVShowExtrafanartsWidth.ToString
            End If
            chkTVSeasonFanartKeepExisting.Checked = .TVSeasonFanartKeepExisting
            chkTVSeasonFanartPrefSizeOnly.Checked = .TVSeasonFanartPrefSizeOnly
            chkTVSeasonFanartResize.Checked = .TVSeasonFanartResize
            If .TVSeasonFanartResize Then
                txtTVSeasonFanartHeight.Text = .TVSeasonFanartHeight.ToString
                txtTVSeasonFanartWidth.Text = .TVSeasonFanartWidth.ToString
            End If
            chkTVSeasonLandscapeKeepExisting.Checked = .TVSeasonLandscapeKeepExisting
            chkTVSeasonLandscapePrefSizeOnly.Checked = .TVSeasonLandscapePrefSizeOnly
            chkTVSeasonPosterKeepExisting.Checked = .TVSeasonPosterKeepExisting
            chkTVSeasonPosterPrefSizeOnly.Checked = .TVSeasonPosterPrefSizeOnly
            chkTVSeasonPosterResize.Checked = .TVSeasonPosterResize
            If .TVSeasonPosterResize Then
                txtTVSeasonPosterHeight.Text = .TVSeasonPosterHeight.ToString
                txtTVSeasonPosterWidth.Text = .TVSeasonPosterWidth.ToString
            End If
            chkTVShowBannerKeepExisting.Checked = .TVShowBannerKeepExisting
            chkTVShowBannerPrefSizeOnly.Checked = .TVShowBannerPrefSizeOnly
            chkTVShowBannerResize.Checked = .TVShowBannerResize
            If .TVShowBannerResize Then
                txtTVShowBannerHeight.Text = .TVShowBannerHeight.ToString
                txtTVShowBannerWidth.Text = .TVShowBannerWidth.ToString
            End If
            chkTVShowCharacterArtKeepExisting.Checked = .TVShowCharacterArtKeepExisting
            chkTVShowCharacterArtPrefSizeOnly.Checked = .TVShowCharacterArtPrefSizeOnly
            chkTVShowClearArtKeepExisting.Checked = .TVShowClearArtKeepExisting
            chkTVShowClearArtPrefSizeOnly.Checked = .TVShowClearArtPrefSizeOnly
            chkTVShowClearLogoKeepExisting.Checked = .TVShowClearLogoKeepExisting
            chkTVShowClearLogoPrefSizeOnly.Checked = .TVShowClearLogoPrefSizeOnly
            chkTVShowFanartKeepExisting.Checked = .TVShowFanartKeepExisting
            chkTVShowFanartPrefSizeOnly.Checked = .TVShowFanartPrefSizeOnly
            chkTVShowFanartResize.Checked = .TVShowFanartResize
            If .TVShowFanartResize Then
                txtTVShowFanartHeight.Text = .TVShowFanartHeight.ToString
                txtTVShowFanartWidth.Text = .TVShowFanartWidth.ToString
            End If
            chkTVShowLandscapeKeepExisting.Checked = .TVShowLandscapeKeepExisting
            chkTVShowLandscapePrefSizeOnly.Checked = .TVShowLandscapePrefSizeOnly
            chkTVShowKeyArtKeepExisting.Checked = .TVShowKeyArtKeepExisting
            chkTVShowKeyArtPrefSizeOnly.Checked = .TVShowKeyArtPrefSizeOnly
            chkTVShowKeyArtResize.Checked = .TVShowKeyArtResize
            If .TVShowKeyArtResize Then
                txtTVShowKeyArtHeight.Text = .TVShowKeyArtHeight.ToString
                txtTVShowKeyArtWidth.Text = .TVShowKeyArtWidth.ToString
            End If
            chkTVShowPosterKeepExisting.Checked = .TVShowPosterKeepExisting
            chkTVShowPosterPrefSizeOnly.Checked = .TVShowPosterPrefSizeOnly
            chkTVShowPosterResize.Checked = .TVShowPosterResize
            If .TVShowPosterResize Then
                txtTVShowPosterHeight.Text = .TVShowPosterHeight.ToString
                txtTVShowPosterWidth.Text = .TVShowPosterWidth.ToString
            End If
            txtTVShowExtrafanartsLimit.Text = .TVShowExtrafanartsLimit.ToString

            Try
                cbTVImagesForcedLanguage.Items.Clear()
                cbTVImagesForcedLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Name).Distinct.ToArray)
                If cbTVImagesForcedLanguage.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.TVImagesForcedLanguage) Then
                        Dim tLanguage As languageProperty = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = .TVImagesForcedLanguage)
                        If tLanguage IsNot Nothing Then
                            cbTVImagesForcedLanguage.Text = tLanguage.Name
                        Else
                            tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.TVImagesForcedLanguage))
                            If tLanguage IsNot Nothing Then
                                cbTVImagesForcedLanguage.Text = tLanguage.Name
                            Else
                                cbTVImagesForcedLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                            End If
                        End If
                    Else
                        cbTVImagesForcedLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                    End If
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End With
    End Sub

    Private Sub Setup()

        'Actor Thumbs
        Dim strActorthumbs As String = Master.eLang.GetString(991, "Actor Thumbs")
        lblTVImagesHeaderEpisodeActorthumbs.Text = strActorthumbs
        lblTVImagesHeaderTVShowActorthumbs.Text = strActorthumbs

        'Banner
        Dim strBanner As String = Master.eLang.GetString(838, "Banner")
        lblTVImagesHeaderAllSeasonsBanner.Text = strBanner
        lblTVImagesHeaderSeasonBanner.Text = strBanner
        lblTVImagesHeaderTVShowBanner.Text = strBanner

        'Fanart
        Dim strFanart As String = Master.eLang.GetString(149, "Fanart")
        lblTVImagesHeaderAllSeasonsFanart.Text = strFanart
        lblTVImagesHeaderEpisodeFanart.Text = strFanart
        lblTVImagesHeaderSeasonFanart.Text = strFanart
        lblTVImagesHeaderTVShowFanart.Text = strFanart

        'Landscape
        Dim strLandscape As String = Master.eLang.GetString(1059, "Landscape")
        lblTVImagesHeaderAllSeasonsLandscape.Text = strLandscape
        lblTVImagesHeaderSeasonLandscape.Text = strLandscape
        lblTVImagesHeaderTVShowLandscape.Text = strLandscape

        'Poster
        Dim strPoster As String = Master.eLang.GetString(148, "Poster")
        lblTVImagesHeaderAllSeasonsPoster.Text = strPoster
        lblTVImagesHeaderEpisodePoster.Text = strPoster
        lblTVImagesHeaderSeasonPoster.Text = strPoster
        lblTVImagesHeaderTVShowPoster.Text = strPoster

        lblTVImagesHeaderAllSeasons.Text = Master.eLang.GetString(1202, "All Seasons")
        chkTVImagesGetBlankImages.Text = Master.eLang.GetString(1207, "Also Get Blank Images")
        chkTVImagesGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        lblTVImagesHeaderTVShowCharacterArt.Text = Master.eLang.GetString(1140, "CharacterArt")
        lblTVImagesHeaderTVShowClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        lblTVImagesHeaderTVShowClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        chkTVImagesDisplayImageSelect.Text = Master.eLang.GetString(499, "Display ""Select Images"" dialog while single scraping")
        chkTVImagesNotSaveURLToNfo.Text = Master.eLang.GetString(498, "Do not save URLs to NFO")
        chkTVImagesCacheEnabled.Text = Master.eLang.GetString(249, "Enable Image Caching")
        lblTVImagesHeaderEpisode.Text = Master.eLang.GetString(727, "Episode")
        lblTVImagesHeaderVideoExtraction.Text = String.Concat(Master.eLang.GetString(538, "Extract"), " **")
        lblTVImagesHintVideoExtraction.Text = String.Concat("** ", Master.eLang.GetString(666, "Extract images from video file"))
        lblTVImagesHeaderTVShowExtrafanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        chkTVImagesForceLanguage.Text = Master.eLang.GetString(1034, "Force Language")
        gbTVImagesOpts.Text = Master.eLang.GetString(497, "Images")
        gbTVImagesImageTypesOpts.Text = Master.eLang.GetString(304, "Image Types")
        lblTVImagesHeaderKeepExisting.Text = Master.eLang.GetString(971, "Keep existing")
        lblTVImagesHeaderTVShowKeyArt.Text = Master.eLang.GetString(296, "KeyArt")
        lblTVImagesHeaderLimit.Text = Master.eLang.GetString(578, "Limit")
        lblTVImagesHeaderMaxHeight.Text = Master.eLang.GetString(480, "Max Height")
        lblTVImagesHeaderMaxWidth.Text = Master.eLang.GetString(479, "Max Width")
        lblTVImagesHeaderPrefSizeOnly.Text = Master.eLang.GetString(145, "Only")
        chkTVImagesMediaLanguageOnly.Text = Master.eLang.GetString(736, "Only Get Images for the Media Language")
        lblTVImagesHintVideoExtractionPref.Text = String.Concat("*** ", Master.eLang.GetString(667, "Prefer extracted images"))
        gbTVImagesLanguageOpts.Text = Master.eLang.GetString(741, "Preferred Language")
        lblTVImagesHeaderPrefSize.Text = Master.eLang.GetString(482, "Preferred Size")
        lblTVImagesHeaderPrefType.Text = Master.eLang.GetString(730, "Preferred Type")
        lblTVImagesHeaderPreselect.Text = String.Concat(Master.eLang.GetString(308, "Preselect"), " *")
        lblTVImagesHintPreselect.Text = String.Concat("* ", Master.eLang.GetString(1023, "Preselect images in ""Select Images"" dialog"))
        lblTVImagesHeaderResize.Text = Master.eLang.GetString(481, "Resize")
        lblTVImagesHeaderSeason.Text = Master.eLang.GetString(650, "Season")
        lblTVImagesHeaderTVShow.Text = Master.eLang.GetString(700, "TV Show")

        LoadBannerSizes_TV()
        LoadBannerTypes()
        LoadCharacterArtSizes()
        LoadClearArtSizes()
        LoadClearLogoSizes()
        LoadFanartSizes()
        LoadLandscapeSizes()
        LoadPosterSizes_TV()
    End Sub

    Private Sub chkTVAllSeasonsBannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVAllSeasonsBannerWidth.Enabled = chkTVAllSeasonsBannerResize.Checked
        txtTVAllSeasonsBannerHeight.Enabled = chkTVAllSeasonsBannerResize.Checked

        If Not chkTVAllSeasonsBannerResize.Checked Then
            txtTVAllSeasonsBannerWidth.Text = String.Empty
            txtTVAllSeasonsBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVAllSeasonsFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVAllSeasonsFanartWidth.Enabled = chkTVAllSeasonsFanartResize.Checked
        txtTVAllSeasonsFanartHeight.Enabled = chkTVAllSeasonsFanartResize.Checked

        If Not chkTVAllSeasonsFanartResize.Checked Then
            txtTVAllSeasonsFanartWidth.Text = String.Empty
            txtTVAllSeasonsFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVAllSeasonsosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVAllSeasonsPosterWidth.Enabled = chkTVAllSeasonsPosterResize.Checked
        txtTVAllSeasonsPosterHeight.Enabled = chkTVAllSeasonsPosterResize.Checked

        If Not chkTVAllSeasonsPosterResize.Checked Then
            txtTVAllSeasonsPosterWidth.Text = String.Empty
            txtTVAllSeasonsPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodeFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVEpisodeFanartWidth.Enabled = chkTVEpisodeFanartResize.Checked
        txtTVEpisodeFanartHeight.Enabled = chkTVEpisodeFanartResize.Checked

        If Not chkTVEpisodeFanartResize.Checked Then
            txtTVEpisodeFanartWidth.Text = String.Empty
            txtTVEpisodeFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodePosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVEpisodePosterWidth.Enabled = chkTVEpisodePosterResize.Checked
        txtTVEpisodePosterHeight.Enabled = chkTVEpisodePosterResize.Checked

        If Not chkTVEpisodeFanartResize.Checked Then
            txtTVEpisodePosterWidth.Text = String.Empty
            txtTVEpisodePosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVEpisodePosterVideoExtraction_CheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent SettingsChanged()
        chkTVEpisodePosterVideoExtractionPref.Enabled = chkTVEpisodePosterVideoExtraction.Checked
        If Not chkTVEpisodePosterVideoExtraction.Checked Then
            chkTVEpisodePosterVideoExtractionPref.Checked = False
        End If
    End Sub

    Private Sub chkTVShowExtrafanartsResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVShowExtrafanartsWidth.Enabled = chkTVShowExtrafanartsResize.Checked
        txtTVShowExtrafanartsHeight.Enabled = chkTVShowExtrafanartsResize.Checked

        If Not chkTVShowExtrafanartsResize.Checked Then
            txtTVShowExtrafanartsWidth.Text = String.Empty
            txtTVShowExtrafanartsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowbannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVShowBannerWidth.Enabled = chkTVShowBannerResize.Checked
        txtTVShowBannerHeight.Enabled = chkTVShowBannerResize.Checked

        If Not chkTVShowBannerResize.Checked Then
            txtTVShowBannerWidth.Text = String.Empty
            txtTVShowBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVShowFanartWidth.Enabled = chkTVShowFanartResize.Checked
        txtTVShowFanartHeight.Enabled = chkTVShowFanartResize.Checked

        If Not chkTVShowFanartResize.Checked Then
            txtTVShowFanartWidth.Text = String.Empty
            txtTVShowFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowKeyArtResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVShowKeyArtWidth.Enabled = chkTVShowKeyArtResize.Checked
        txtTVShowKeyArtHeight.Enabled = chkTVShowKeyArtResize.Checked

        If Not chkTVShowKeyArtResize.Checked Then
            txtTVShowKeyArtWidth.Text = String.Empty
            txtTVShowKeyArtHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVShowPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVShowPosterWidth.Enabled = chkTVShowPosterResize.Checked
        txtTVShowPosterHeight.Enabled = chkTVShowPosterResize.Checked

        If Not chkTVShowPosterResize.Checked Then
            txtTVShowPosterWidth.Text = String.Empty
            txtTVShowPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonbannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVSeasonBannerWidth.Enabled = chkTVSeasonBannerResize.Checked
        txtTVSeasonBannerHeight.Enabled = chkTVSeasonBannerResize.Checked

        If Not chkTVSeasonBannerResize.Checked Then
            txtTVSeasonBannerWidth.Text = String.Empty
            txtTVSeasonBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVSeasonFanartWidth.Enabled = chkTVSeasonFanartResize.Checked
        txtTVSeasonFanartHeight.Enabled = chkTVSeasonFanartResize.Checked

        If Not chkTVSeasonFanartResize.Checked Then
            txtTVSeasonFanartWidth.Text = String.Empty
            txtTVSeasonFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVSeasonPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent SettingsChanged()

        txtTVSeasonPosterWidth.Enabled = chkTVSeasonPosterResize.Checked
        txtTVSeasonPosterHeight.Enabled = chkTVSeasonPosterResize.Checked

        If Not chkTVSeasonPosterResize.Checked Then
            txtTVSeasonPosterWidth.Text = String.Empty
            txtTVSeasonPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkTVImagesForceLanguage_CheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent SettingsChanged()

        cbTVImagesForcedLanguage.Enabled = chkTVImagesForceLanguage.Checked
    End Sub

    Private Sub chkTVImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent SettingsChanged()

        chkTVImagesGetBlankImages.Enabled = chkTVImagesMediaLanguageOnly.Checked
        chkTVImagesGetEnglishImages.Enabled = chkTVImagesMediaLanguageOnly.Checked

        If Not chkTVImagesMediaLanguageOnly.Checked Then
            chkTVImagesGetBlankImages.Checked = False
            chkTVImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub LoadBannerSizes_TV()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x185", Enums.ImageSize.HD185)
        items.Add("758x140", Enums.ImageSize.HD140)
        LoadItemsToComboBox(cbTVAllSeasonsBannerPrefSize, items)
        LoadItemsToComboBox(cbTVSeasonBannerPrefSize, items)
        LoadItemsToComboBox(cbTVShowBannerPrefSize, items)
    End Sub

    Private Sub LoadBannerTypes()
        Dim items As New Dictionary(Of String, Enums.TVBannerType)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.TVBannerType.Any)
        items.Add(Master.eLang.GetString(746, "Blank"), Enums.TVBannerType.Blank)
        items.Add(Master.eLang.GetString(747, "Graphical"), Enums.TVBannerType.Graphical)
        items.Add(Master.eLang.GetString(748, "Text"), Enums.TVBannerType.Text)
        cbTVAllSeasonsBannerPrefType.DataSource = items.ToList
        cbTVAllSeasonsBannerPrefType.DisplayMember = "Key"
        cbTVAllSeasonsBannerPrefType.ValueMember = "Value"
        cbTVSeasonBannerPrefType.DataSource = items.ToList
        cbTVSeasonBannerPrefType.DisplayMember = "Key"
        cbTVSeasonBannerPrefType.ValueMember = "Value"
        cbTVShowBannerPrefType.DataSource = items.ToList
        cbTVShowBannerPrefType.DisplayMember = "Key"
        cbTVShowBannerPrefType.ValueMember = "Value"
    End Sub

    Private Sub LoadCharacterArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("512x512", Enums.ImageSize.HD512)
        LoadItemsToComboBox(cbTVShowCharacterArtPrefSize, items)
    End Sub

    Private Sub LoadClearArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x562", Enums.ImageSize.HD562)
        items.Add("500x281", Enums.ImageSize.SD281)
        LoadItemsToComboBox(cbTVShowClearArtPrefSize, items)
    End Sub

    Private Sub LoadClearLogoSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("800x310", Enums.ImageSize.HD310)
        items.Add("400x155", Enums.ImageSize.SD155)
        LoadItemsToComboBox(cbTVShowClearLogoPrefSize, items)
    End Sub

    Private Sub LoadFanartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("3840x2160", Enums.ImageSize.UHD2160)
        items.Add("2560x1440", Enums.ImageSize.QHD1440)
        items.Add("1920x1080", Enums.ImageSize.HD1080)
        items.Add("1280x720", Enums.ImageSize.HD720)
        LoadItemsToComboBox(cbTVAllSeasonsFanartPrefSize, items)
        LoadItemsToComboBox(cbTVEpisodeFanartPrefSize, items)
        LoadItemsToComboBox(cbTVSeasonFanartPrefSize, items)
        LoadItemsToComboBox(cbTVShowExtrafanartsPrefSize, items)
        LoadItemsToComboBox(cbTVShowFanartPrefSize, items)
    End Sub

    Private Sub LoadItemsToComboBox(ByRef CBox As ComboBox, Items As Dictionary(Of String, Enums.ImageSize))
        CBox.DataSource = Items.ToList
        CBox.DisplayMember = "Key"
        CBox.ValueMember = "Value"
    End Sub

    Private Sub LoadLandscapeSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x562", Enums.ImageSize.HD562)
        LoadItemsToComboBox(cbTVAllSeasonsLandscapePrefSize, items)
        LoadItemsToComboBox(cbTVSeasonLandscapePrefSize, items)
        LoadItemsToComboBox(cbTVShowLandscapePrefSize, items)
    End Sub

    Private Sub LoadPosterSizes_TV()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("2000x3000", Enums.ImageSize.HD3000)
        items.Add("1000x1500", Enums.ImageSize.HD1500)
        items.Add("1000x1426", Enums.ImageSize.HD1426)
        items.Add("680x1000", Enums.ImageSize.HD1000)
        LoadItemsToComboBox(cbTVAllSeasonsPosterPrefSize, items)
        LoadItemsToComboBox(cbTVShowKeyArtPrefSize, items)
        LoadItemsToComboBox(cbTVShowPosterPrefSize, items)

        Dim items2 As New Dictionary(Of String, Enums.ImageSize)
        items2.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items2.Add("1000x1500", Enums.ImageSize.HD1500)
        items2.Add("1000x1426", Enums.ImageSize.HD1426)
        items2.Add("400x578", Enums.ImageSize.HD578)
        LoadItemsToComboBox(cbTVSeasonPosterPrefSize, items2)

        Dim items3 As New Dictionary(Of String, Enums.ImageSize)
        items3.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items3.Add("3840x2160", Enums.ImageSize.UHD2160)
        items3.Add("1920x1080", Enums.ImageSize.HD1080)
        items3.Add("1280x720", Enums.ImageSize.HD720)
        items3.Add("400x225", Enums.ImageSize.SD225)
        LoadItemsToComboBox(cbTVEpisodePosterPrefSize, items3)
    End Sub

    Private Sub EnableApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        cbTVAllSeasonsBannerPrefType.SelectedIndexChanged,
        cbTVAllSeasonsFanartPrefSize.SelectedIndexChanged,
        cbTVAllSeasonsLandscapePrefSize.SelectedIndexChanged,
        cbTVAllSeasonsPosterPrefSize.SelectedIndexChanged,
        cbTVEpisodeFanartPrefSize.SelectedIndexChanged,
        cbTVEpisodePosterPrefSize.SelectedIndexChanged,
        cbTVImagesForcedLanguage.SelectedIndexChanged,
        cbTVSeasonBannerPrefType.SelectedIndexChanged,
        cbTVSeasonFanartPrefSize.SelectedIndexChanged,
        cbTVSeasonLandscapePrefSize.SelectedIndexChanged,
        cbTVSeasonPosterPrefSize.SelectedIndexChanged,
        cbTVShowBannerPrefSize.SelectedIndexChanged,
        cbTVShowBannerPrefType.SelectedIndexChanged,
        cbTVShowCharacterArtPrefSize.SelectedIndexChanged,
        cbTVShowClearArtPrefSize.SelectedIndexChanged,
        cbTVShowClearLogoPrefSize.SelectedIndexChanged,
        cbTVShowExtrafanartsPrefSize.SelectedIndexChanged,
        cbTVShowFanartPrefSize.SelectedIndexChanged,
        cbTVShowKeyArtPrefSize.SelectedIndexChanged,
        cbTVShowLandscapePrefSize.SelectedIndexChanged,
        cbTVShowPosterPrefSize.SelectedIndexChanged,
        chkTVAllSeasonsBannerKeepExisting.CheckedChanged,
        chkTVAllSeasonsBannerPrefSizeOnly.CheckedChanged,
        chkTVAllSeasonsFanartKeepExisting.CheckedChanged,
        chkTVAllSeasonsFanartPrefSizeOnly.CheckedChanged,
        chkTVAllSeasonsLandscapeKeepExisting.CheckedChanged,
        chkTVAllSeasonsLandscapePrefSizeOnly.CheckedChanged,
        chkTVAllSeasonsPosterKeepExisting.CheckedChanged,
        chkTVAllSeasonsPosterPrefSizeOnly.CheckedChanged,
        chkTVEpisodeFanartKeepExisting.CheckedChanged,
        chkTVEpisodeFanartPrefSizeOnly.CheckedChanged,
        chkTVEpisodePosterKeepExisting.CheckedChanged,
        chkTVEpisodePosterPrefSizeOnly.CheckedChanged,
        chkTVEpisodePosterVideoExtractionPref.CheckedChanged,
        chkTVImagesCacheEnabled.CheckedChanged,
        chkTVImagesDisplayImageSelect.CheckedChanged,
        chkTVImagesGetBlankImages.CheckedChanged,
        chkTVImagesGetEnglishImages.CheckedChanged,
        chkTVImagesNotSaveURLToNfo.CheckedChanged,
        chkTVSeasonBannerKeepExisting.CheckedChanged,
        chkTVSeasonBannerPrefSizeOnly.CheckedChanged,
        chkTVSeasonFanartKeepExisting.CheckedChanged,
        chkTVSeasonFanartPrefSizeOnly.CheckedChanged,
        chkTVSeasonLandscapeKeepExisting.CheckedChanged,
        chkTVSeasonLandscapePrefSizeOnly.CheckedChanged,
        chkTVSeasonPosterKeepExisting.CheckedChanged,
        chkTVSeasonPosterPrefSizeOnly.CheckedChanged,
        chkTVShowBannerKeepExisting.CheckedChanged,
        chkTVShowBannerPrefSizeOnly.CheckedChanged,
        chkTVShowCharacterArtKeepExisting.CheckedChanged,
        chkTVShowCharacterArtPrefSizeOnly.CheckedChanged,
        chkTVShowClearArtKeepExisting.CheckedChanged,
        chkTVShowClearArtPrefSizeOnly.CheckedChanged,
        chkTVShowClearLogoKeepExisting.CheckedChanged,
        chkTVShowClearLogoPrefSizeOnly.CheckedChanged,
        chkTVShowExtrafanartsKeepExisting.CheckedChanged,
        chkTVShowExtrafanartsPrefSizeOnly.CheckedChanged,
        chkTVShowExtrafanartsPreselect.CheckedChanged,
        chkTVShowFanartKeepExisting.CheckedChanged,
        chkTVShowFanartPrefSizeOnly.CheckedChanged,
        chkTVShowKeyArtKeepExisting.CheckedChanged,
        chkTVShowKeyArtPrefSizeOnly.CheckedChanged,
        chkTVShowLandscapeKeepExisting.CheckedChanged,
        chkTVShowLandscapePrefSizeOnly.CheckedChanged,
        chkTVShowPosterKeepExisting.CheckedChanged,
        chkTVShowPosterPrefSizeOnly.CheckedChanged,
        txtTVAllSeasonsBannerHeight.TextChanged,
        txtTVAllSeasonsBannerWidth.TextChanged,
        txtTVAllSeasonsFanartHeight.TextChanged,
        txtTVAllSeasonsFanartWidth.TextChanged,
        txtTVAllSeasonsPosterHeight.TextChanged,
        txtTVAllSeasonsPosterWidth.TextChanged,
        txtTVEpisodeFanartHeight.TextChanged,
        txtTVEpisodeFanartWidth.TextChanged,
        txtTVEpisodePosterHeight.TextChanged,
        txtTVEpisodePosterWidth.TextChanged,
        txtTVSeasonBannerHeight.TextChanged,
        txtTVSeasonBannerWidth.TextChanged,
        txtTVSeasonFanartHeight.TextChanged,
        txtTVSeasonFanartWidth.TextChanged,
        txtTVSeasonPosterHeight.TextChanged,
        txtTVSeasonPosterWidth.TextChanged,
        txtTVShowBannerHeight.TextChanged,
        txtTVShowBannerWidth.TextChanged,
        txtTVShowExtrafanartsHeight.TextChanged,
        txtTVShowExtrafanartsLimit.TextChanged,
        txtTVShowExtrafanartsWidth.TextChanged,
        txtTVShowFanartHeight.TextChanged,
        txtTVShowFanartWidth.TextChanged,
        txtTVShowKeyArtHeight.TextChanged,
        txtTVShowKeyArtWidth.TextChanged,
        txtTVShowPosterHeight.TextChanged,
        txtTVShowPosterWidth.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        txtTVShowFanartWidth.KeyPress

        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_Limit_Leave(sender As Object, e As EventArgs)
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