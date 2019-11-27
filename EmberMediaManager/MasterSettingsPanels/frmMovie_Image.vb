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

Public Class frmMovie_Image
    Implements Interfaces.IMasterSettingsPanel

#Region "Events"

    Public Event NeedsDBClean_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_Movie
    Public Event NeedsDBClean_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBClean_TV
    Public Event NeedsDBUpdate_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_Movie
    Public Event NeedsDBUpdate_TV() Implements Interfaces.IMasterSettingsPanel.NeedsDBUpdate_TV
    Public Event NeedsReload_Movie() Implements Interfaces.IMasterSettingsPanel.NeedsReload_Movie
    Public Event NeedsReload_MovieSet() Implements Interfaces.IMasterSettingsPanel.NeedsReload_MovieSet
    Public Event NeedsReload_TVEpisode() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVEpisode
    Public Event NeedsReload_TVShow() Implements Interfaces.IMasterSettingsPanel.NeedsReload_TVShow
    Public Event NeedsRestart() Implements Interfaces.IMasterSettingsPanel.NeedsRestart
    Public Event SettingsChanged() Implements Interfaces.IMasterSettingsPanel.SettingsChanged

#End Region 'Events

#Region "Event Methods"

    Private Sub Handle_NeedsDBClean_Movie()
        RaiseEvent NeedsDBClean_Movie()
    End Sub

    Private Sub Handle_NeedsDBClean_TV()
        RaiseEvent NeedsDBClean_TV()
    End Sub

    Private Sub Handle_NeedsDBUpdate_Movie()
        RaiseEvent NeedsDBUpdate_Movie()
    End Sub

    Private Sub Handle_NeedsDBUpdate_TV()
        RaiseEvent NeedsDBUpdate_TV()
    End Sub

    Private Sub Handle_NeedsReload_Movie()
        RaiseEvent NeedsReload_Movie()
    End Sub

    Private Sub Handle_NeedsReload_MovieSet()
        RaiseEvent NeedsReload_MovieSet()
    End Sub

    Private Sub Handle_NeedsReload_TVEpisode()
        RaiseEvent NeedsReload_TVEpisode()
    End Sub

    Private Sub Handle_NeedsReload_TVShow()
        RaiseEvent NeedsReload_TVShow()
    End Sub

    Private Sub Handle_NeedsRestart()
        RaiseEvent NeedsRestart()
    End Sub

    Private Sub Handle_SettingsChanged()
        RaiseEvent SettingsChanged()
    End Sub

#End Region 'Event Methods

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
            .Contains = Enums.SettingsPanelType.MovieImage,
            .ImageIndex = 6,
            .Order = 500,
            .Panel = pnlSettings,
            .SettingsPanelID = "Movie_Image",
            .Title = Master.eLang.GetString(497, "Images"),
            .Type = Enums.SettingsPanelType.Movie
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings
            .MovieActorThumbsKeepExisting = chkMovieActorThumbsKeepExisting.Checked
            '.MovieActorThumbsQual = Me.tbMovieActorThumbsQual.value
            .MovieBannerHeight = If(Not String.IsNullOrEmpty(txtMovieBannerHeight.Text), Convert.ToInt32(txtMovieBannerHeight.Text), 0)
            .MovieBannerKeepExisting = chkMovieBannerKeepExisting.Checked
            .MovieBannerPrefSize = CType(cbMovieBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieBannerPrefSizeOnly = chkMovieBannerPrefSizeOnly.Checked
            .MovieBannerResize = chkMovieBannerResize.Checked
            .MovieBannerWidth = If(Not String.IsNullOrEmpty(txtMovieBannerWidth.Text), Convert.ToInt32(txtMovieBannerWidth.Text), 0)
            .MovieClearArtKeepExisting = chkMovieClearArtKeepExisting.Checked
            .MovieClearArtPrefSize = CType(cbMovieClearArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieClearArtPrefSizeOnly = chkMovieClearArtPrefSizeOnly.Checked
            .MovieClearLogoKeepExisting = chkMovieClearLogoKeepExisting.Checked
            .MovieClearLogoPrefSize = CType(cbMovieClearLogoPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieClearLogoPrefSizeOnly = chkMovieClearLogoPrefSizeOnly.Checked
            .MovieDiscArtKeepExisting = chkMovieDiscArtKeepExisting.Checked
            .MovieDiscArtPrefSize = CType(cbMovieDiscArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieDiscArtPrefSizeOnly = chkMovieDiscArtPrefSizeOnly.Checked
            .MovieExtrafanartsHeight = If(Not String.IsNullOrEmpty(txtMovieExtrafanartsHeight.Text), Convert.ToInt32(txtMovieExtrafanartsHeight.Text), 0)
            .MovieExtrafanartsLimit = If(Not String.IsNullOrEmpty(txtMovieExtrafanartsLimit.Text), Convert.ToInt32(txtMovieExtrafanartsLimit.Text), 0)
            .MovieExtrafanartsKeepExisting = chkMovieExtrafanartsKeepExisting.Checked
            .MovieExtrafanartsPrefSize = CType(cbMovieExtrafanartsPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieExtrafanartsPrefSizeOnly = chkMovieExtrafanartsPrefSizeOnly.Checked
            .MovieExtrafanartsPreselect = chkMovieExtrafanartsPreselect.Checked
            .MovieExtrafanartsResize = chkMovieExtrafanartsResize.Checked
            .MovieExtrafanartsWidth = If(Not String.IsNullOrEmpty(txtMovieExtrafanartsWidth.Text), Convert.ToInt32(txtMovieExtrafanartsWidth.Text), 0)
            .MovieExtrathumbsHeight = If(Not String.IsNullOrEmpty(txtMovieExtrathumbsHeight.Text), Convert.ToInt32(txtMovieExtrathumbsHeight.Text), 0)
            .MovieExtrathumbsLimit = If(Not String.IsNullOrEmpty(txtMovieExtrathumbsLimit.Text), Convert.ToInt32(txtMovieExtrathumbsLimit.Text), 0)
            .MovieExtrathumbsKeepExisting = chkMovieExtrathumbsKeepExisting.Checked
            .MovieExtrathumbsPrefSize = CType(cbMovieExtrathumbsPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieExtrathumbsPrefSizeOnly = chkMovieExtrathumbsPrefSizeOnly.Checked
            .MovieExtrathumbsPreselect = chkMovieExtrathumbsPreselect.Checked
            .MovieExtrathumbsResize = chkMovieExtrathumbsResize.Checked
            .MovieExtrathumbsVideoExtraction = chkMovieExtrathumbsVideoExtraction.Checked
            .MovieExtrathumbsVideoExtractionPref = chkMovieExtrathumbsVideoExtractionPref.Checked
            .MovieExtrathumbsWidth = If(Not String.IsNullOrEmpty(txtMovieExtrathumbsWidth.Text), Convert.ToInt32(txtMovieExtrathumbsWidth.Text), 0)
            .MovieFanartHeight = If(Not String.IsNullOrEmpty(txtMovieFanartHeight.Text), Convert.ToInt32(txtMovieFanartHeight.Text), 0)
            .MovieFanartKeepExisting = chkMovieFanartKeepExisting.Checked
            .MovieFanartPrefSize = CType(cbMovieFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieFanartPrefSizeOnly = chkMovieFanartPrefSizeOnly.Checked
            .MovieFanartResize = chkMovieFanartResize.Checked
            .MovieFanartWidth = If(Not String.IsNullOrEmpty(txtMovieFanartWidth.Text), Convert.ToInt32(txtMovieFanartWidth.Text), 0)
            .MovieImagesCacheEnabled = chkMovieImagesCacheEnabled.Checked
            .MovieImagesDisplayImageSelect = chkMovieImagesDisplayImageSelect.Checked
            If Not String.IsNullOrEmpty(cbMovieImagesForcedLanguage.Text) Then
                .MovieImagesForcedLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Name = cbMovieImagesForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .MovieImagesForceLanguage = chkMovieImagesForceLanguage.Checked
            .MovieImagesGetBlankImages = chkMovieImagesGetBlankImages.Checked
            .MovieImagesGetEnglishImages = chkMovieImagesGetEnglishImages.Checked
            .MovieImagesMediaLanguageOnly = chkMovieImagesMediaLanguageOnly.Checked
            .MovieImagesNotSaveURLToNfo = chkMovieImagesNotSaveURLToNfo.Checked
            .MovieKeyArtHeight = If(Not String.IsNullOrEmpty(txtMovieKeyArtHeight.Text), Convert.ToInt32(txtMovieKeyArtHeight.Text), 0)
            .MovieKeyArtKeepExisting = chkMovieKeyArtKeepExisting.Checked
            .MovieKeyArtPrefSize = CType(cbMovieKeyArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieKeyArtPrefSizeOnly = chkMovieKeyArtPrefSizeOnly.Checked
            .MovieKeyArtResize = chkMovieKeyArtResize.Checked
            .MovieKeyArtWidth = If(Not String.IsNullOrEmpty(txtMovieKeyArtWidth.Text), Convert.ToInt32(txtMovieKeyArtWidth.Text), 0)
            .MovieLandscapeKeepExisting = chkMovieLandscapeKeepExisting.Checked
            .MovieLandscapePrefSize = CType(cbMovieLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieLandscapePrefSizeOnly = chkMovieLandscapePrefSizeOnly.Checked
            .MoviePosterHeight = If(Not String.IsNullOrEmpty(txtMoviePosterHeight.Text), Convert.ToInt32(txtMoviePosterHeight.Text), 0)
            .MoviePosterKeepExisting = chkMoviePosterKeepExisting.Checked
            .MoviePosterPrefSize = CType(cbMoviePosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MoviePosterPrefSizeOnly = chkMoviePosterPrefSizeOnly.Checked
            .MoviePosterResize = chkMoviePosterResize.Checked
            .MoviePosterWidth = If(Not String.IsNullOrEmpty(txtMoviePosterWidth.Text), Convert.ToInt32(txtMoviePosterWidth.Text), 0)

        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            cbMovieBannerPrefSize.SelectedValue = .MovieBannerPrefSize
            cbMovieClearArtPrefSize.SelectedValue = .MovieClearArtPrefSize
            cbMovieClearLogoPrefSize.SelectedValue = .MovieClearLogoPrefSize
            cbMovieDiscArtPrefSize.SelectedValue = .MovieDiscArtPrefSize
            cbMovieExtrafanartsPrefSize.SelectedValue = .MovieExtrafanartsPrefSize
            cbMovieExtrathumbsPrefSize.SelectedValue = .MovieExtrathumbsPrefSize
            cbMovieFanartPrefSize.SelectedValue = .MovieFanartPrefSize
            cbMovieKeyArtPrefSize.SelectedValue = .MovieKeyArtPrefSize
            cbMovieLandscapePrefSize.SelectedValue = .MovieLandscapePrefSize
            cbMoviePosterPrefSize.SelectedValue = .MoviePosterPrefSize
            chkMovieActorThumbsKeepExisting.Checked = .MovieActorThumbsKeepExisting
            chkMovieBannerKeepExisting.Checked = .MovieBannerKeepExisting
            chkMovieBannerPrefSizeOnly.Checked = .MovieBannerPrefSizeOnly
            chkMovieBannerResize.Checked = .MovieBannerResize
            If .MovieBannerResize Then
                txtMovieBannerHeight.Text = .MovieBannerHeight.ToString
                txtMovieBannerWidth.Text = .MovieBannerWidth.ToString
            End If
            chkMovieClearArtKeepExisting.Checked = .MovieClearArtKeepExisting
            chkMovieClearArtPrefSizeOnly.Checked = .MovieClearArtPrefSizeOnly
            chkMovieClearLogoKeepExisting.Checked = .MovieClearLogoKeepExisting
            chkMovieClearLogoPrefSizeOnly.Checked = .MovieClearLogoPrefSizeOnly
            chkMovieDiscArtKeepExisting.Checked = .MovieDiscArtKeepExisting
            chkMovieDiscArtPrefSizeOnly.Checked = .MovieDiscArtPrefSizeOnly
            chkMovieExtrafanartsKeepExisting.Checked = .MovieExtrafanartsKeepExisting
            chkMovieExtrafanartsPrefSizeOnly.Checked = .MovieExtrafanartsPrefSizeOnly
            chkMovieExtrafanartsPreselect.Checked = .MovieExtrafanartsPreselect
            chkMovieExtrafanartsResize.Checked = .MovieExtrafanartsResize
            If .MovieExtrafanartsResize Then
                txtMovieExtrafanartsHeight.Text = .MovieExtrafanartsHeight.ToString
                txtMovieExtrafanartsWidth.Text = .MovieExtrafanartsWidth.ToString
            End If
            chkMovieExtrathumbsKeepExisting.Checked = .MovieExtrathumbsKeepExisting
            chkMovieExtrathumbsPrefSizeOnly.Checked = .MovieExtrathumbsPrefSizeOnly
            chkMovieExtrathumbsPreselect.Checked = .MovieExtrathumbsPreselect
            chkMovieExtrathumbsResize.Checked = .MovieExtrathumbsResize
            If .MovieExtrathumbsResize Then
                txtMovieExtrathumbsHeight.Text = .MovieExtrathumbsHeight.ToString
                txtMovieExtrathumbsWidth.Text = .MovieExtrathumbsWidth.ToString
            End If
            chkMovieExtrathumbsVideoExtraction.Checked = .MovieExtrathumbsVideoExtraction
            chkMovieExtrathumbsVideoExtractionPref.Checked = .MovieExtrathumbsVideoExtractionPref
            chkMovieFanartKeepExisting.Checked = .MovieFanartKeepExisting
            chkMovieFanartPrefSizeOnly.Checked = .MovieFanartPrefSizeOnly
            chkMovieFanartResize.Checked = .MovieFanartResize
            If .MovieFanartResize Then
                txtMovieFanartHeight.Text = .MovieFanartHeight.ToString
                txtMovieFanartWidth.Text = .MovieFanartWidth.ToString
            End If
            chkMovieImagesCacheEnabled.Checked = .MovieImagesCacheEnabled
            chkMovieImagesDisplayImageSelect.Checked = .MovieImagesDisplayImageSelect
            chkMovieImagesForceLanguage.Checked = .MovieImagesForceLanguage
            If .MovieImagesMediaLanguageOnly Then
                chkMovieImagesMediaLanguageOnly.Checked = True
                chkMovieImagesGetBlankImages.Checked = .MovieImagesGetBlankImages
                chkMovieImagesGetEnglishImages.Checked = .MovieImagesGetEnglishImages
            End If
            chkMovieImagesNotSaveURLToNfo.Checked = .MovieImagesNotSaveURLToNfo
            chkMovieKeyArtKeepExisting.Checked = .MovieKeyArtKeepExisting
            chkMovieKeyArtPrefSizeOnly.Checked = .MovieKeyArtPrefSizeOnly
            chkMovieKeyArtResize.Checked = .MovieKeyArtResize
            If .MovieKeyArtResize Then
                txtMovieKeyArtHeight.Text = .MovieKeyArtHeight.ToString
                txtMovieKeyArtWidth.Text = .MovieKeyArtWidth.ToString
            End If
            chkMovieLandscapeKeepExisting.Checked = .MovieLandscapeKeepExisting
            chkMovieLandscapePrefSizeOnly.Checked = .MovieLandscapePrefSizeOnly
            chkMoviePosterKeepExisting.Checked = .MoviePosterKeepExisting
            chkMoviePosterPrefSizeOnly.Checked = .MoviePosterPrefSizeOnly
            chkMoviePosterResize.Checked = .MoviePosterResize
            If .MoviePosterResize Then
                txtMoviePosterHeight.Text = .MoviePosterHeight.ToString
                txtMoviePosterWidth.Text = .MoviePosterWidth.ToString
            End If
            txtMovieExtrafanartsLimit.Text = .MovieExtrafanartsLimit.ToString
            txtMovieExtrathumbsLimit.Text = .MovieExtrathumbsLimit.ToString

            Try
                cbMovieImagesForcedLanguage.Items.Clear()
                cbMovieImagesForcedLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Name).Distinct.ToArray)
                If cbMovieImagesForcedLanguage.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.MovieImagesForcedLanguage) Then
                        Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = .MovieImagesForcedLanguage)
                        If tLanguage IsNot Nothing Then
                            cbMovieImagesForcedLanguage.Text = tLanguage.Name
                        Else
                            tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.MovieImagesForcedLanguage))
                            If tLanguage IsNot Nothing Then
                                cbMovieImagesForcedLanguage.Text = tLanguage.Name
                            Else
                                cbMovieImagesForcedLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                            End If
                        End If
                    Else
                        cbMovieImagesForcedLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                    End If
                End If
            Catch ex As Exception
                'logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End With
    End Sub

    Private Sub chkMovieBannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieBannerWidth.Enabled = chkMovieBannerResize.Checked
        txtMovieBannerHeight.Enabled = chkMovieBannerResize.Checked

        If Not chkMovieBannerResize.Checked Then
            txtMovieBannerWidth.Text = String.Empty
            txtMovieBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieExtrafanartsResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieExtrafanartsWidth.Enabled = chkMovieExtrafanartsResize.Checked
        txtMovieExtrafanartsHeight.Enabled = chkMovieExtrafanartsResize.Checked

        If Not chkMovieExtrafanartsResize.Checked Then
            txtMovieExtrafanartsWidth.Text = String.Empty
            txtMovieExtrafanartsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieExtrathumbsResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieExtrathumbsWidth.Enabled = chkMovieExtrathumbsResize.Checked
        txtMovieExtrathumbsHeight.Enabled = chkMovieExtrathumbsResize.Checked

        If Not chkMovieExtrathumbsResize.Checked Then
            txtMovieExtrathumbsWidth.Text = String.Empty
            txtMovieExtrathumbsHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieFanartWidth.Enabled = chkMovieFanartResize.Checked
        txtMovieFanartHeight.Enabled = chkMovieFanartResize.Checked

        If Not chkMovieFanartResize.Checked Then
            txtMovieFanartWidth.Text = String.Empty
            txtMovieFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieImagesForceLanguage_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()

        cbMovieImagesForcedLanguage.Enabled = chkMovieImagesForceLanguage.Checked
    End Sub

    Private Sub chkMovieImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()

        chkMovieImagesGetBlankImages.Enabled = chkMovieImagesMediaLanguageOnly.Checked
        chkMovieImagesGetEnglishImages.Enabled = chkMovieImagesMediaLanguageOnly.Checked

        If Not chkMovieImagesMediaLanguageOnly.Checked Then
            chkMovieImagesGetBlankImages.Checked = False
            chkMovieImagesGetEnglishImages.Checked = False
        End If
    End Sub
    Private Sub chkMovieExtrathumbsVideoExtraction_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        chkMovieExtrathumbsVideoExtractionPref.Enabled = chkMovieExtrathumbsVideoExtraction.Checked
        If Not chkMovieExtrathumbsVideoExtraction.Checked Then
            chkMovieExtrathumbsVideoExtractionPref.Checked = False
        End If
        Handle_SettingsChanged()
    End Sub

    Private Sub chkMovieKeyArtResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieKeyArtWidth.Enabled = chkMovieKeyArtResize.Checked
        txtMovieKeyArtHeight.Enabled = chkMovieKeyArtResize.Checked

        If Not chkMovieKeyArtResize.Checked Then
            txtMovieKeyArtWidth.Text = String.Empty
            txtMovieKeyArtHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMoviePosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMoviePosterWidth.Enabled = chkMoviePosterResize.Checked
        txtMoviePosterHeight.Enabled = chkMoviePosterResize.Checked

        If Not chkMoviePosterResize.Checked Then
            txtMoviePosterWidth.Text = String.Empty
            txtMoviePosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub LoadBannerSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x185", Enums.ImageSize.HD185)
        LoadItemsToComboBox(cbMovieBannerPrefSize, items)
    End Sub

    Private Sub LoadClearArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x562", Enums.ImageSize.HD562)
        items.Add("500x281", Enums.ImageSize.SD281)
        LoadItemsToComboBox(cbMovieClearArtPrefSize, items)
    End Sub

    Private Sub LoadClearLogoSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("800x310", Enums.ImageSize.HD310)
        items.Add("400x155", Enums.ImageSize.SD155)
        LoadItemsToComboBox(cbMovieClearLogoPrefSize, items)
    End Sub

    Private Sub LoadDiscArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x1000", Enums.ImageSize.HD1000)
        LoadItemsToComboBox(cbMovieDiscArtPrefSize, items)
    End Sub

    Private Sub LoadFanartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("3840x2160", Enums.ImageSize.UHD2160)
        items.Add("2560x1440", Enums.ImageSize.QHD1440)
        items.Add("1920x1080", Enums.ImageSize.HD1080)
        items.Add("1280x720", Enums.ImageSize.HD720)
        LoadItemsToComboBox(cbMovieExtrafanartsPrefSize, items)
        LoadItemsToComboBox(cbMovieExtrathumbsPrefSize, items)
        LoadItemsToComboBox(cbMovieFanartPrefSize, items)
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
        LoadItemsToComboBox(cbMovieLandscapePrefSize, items)
    End Sub

    Private Sub LoadPosterSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("2000x3000", Enums.ImageSize.HD3000)
        items.Add("1400x2100", Enums.ImageSize.HD2100)
        items.Add("1000x1500", Enums.ImageSize.HD1500)
        items.Add("1000x1426", Enums.ImageSize.HD1426)
        LoadItemsToComboBox(cbMovieKeyArtPrefSize, items)
        LoadItemsToComboBox(cbMoviePosterPrefSize, items)
    End Sub

    Private Sub Setup()
        chkMovieImagesCacheEnabled.Text = Master.eLang.GetString(249, "Enable Image Caching")
        chkMovieImagesDisplayImageSelect.Text = Master.eLang.GetString(499, "Display ""Select Images"" dialog while single scraping")
        chkMovieImagesForceLanguage.Text = Master.eLang.GetString(1034, "Force Language")
        chkMovieImagesGetBlankImages.Text = Master.eLang.GetString(1207, "Also Get Blank Images")
        chkMovieImagesGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        chkMovieImagesMediaLanguageOnly.Text = Master.eLang.GetString(736, "Only Get Images for the Media Language")
        chkMovieImagesNotSaveURLToNfo.Text = Master.eLang.GetString(498, "Do not save URLs to NFO")
        gbMovieImagesImageTypesOpts.Text = Master.eLang.GetString(304, "Image Types")
        gbMovieImagesLanguageOpts.Text = Master.eLang.GetString(741, "Preferred Language")
        gbMovieImagesOpts.Text = Master.eLang.GetString(497, "Images")
        lblMovieImagesHeaderActorthumbs.Text = Master.eLang.GetString(991, "Actor Thumbs")
        lblMovieImagesHeaderBanner.Text = Master.eLang.GetString(838, "Banner")
        lblMovieImagesHeaderClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        lblMovieImagesHeaderClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        lblMovieImagesHeaderDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        lblMovieImagesHeaderExtrafanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        lblMovieImagesHeaderExtrathumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        lblMovieImagesHeaderFanart.Text = Master.eLang.GetString(149, "Fanart")
        lblMovieImagesHeaderKeepExisting.Text = Master.eLang.GetString(971, "Keep existing")
        lblMovieImagesHeaderKeyArt.Text = Master.eLang.GetString(296, "KeyArt")
        lblMovieImagesHeaderLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        lblMovieImagesHeaderLimit.Text = Master.eLang.GetString(578, "Limit")
        lblMovieImagesHeaderMaxHeight.Text = Master.eLang.GetString(480, "Max Height")
        lblMovieImagesHeaderMaxWidth.Text = Master.eLang.GetString(479, "Max Width")
        lblMovieImagesHeaderPoster.Text = Master.eLang.GetString(148, "Poster")
        lblMovieImagesHeaderPrefSize.Text = Master.eLang.GetString(482, "Preferred Size")
        lblMovieImagesHeaderPrefSizeOnly.Text = Master.eLang.GetString(145, "Only")
        lblMovieImagesHeaderPreselect.Text = String.Concat(Master.eLang.GetString(308, "Preselect"), " *")
        lblMovieImagesHeaderResize.Text = Master.eLang.GetString(481, "Resize")
        lblMovieImagesHeaderVideoExtraction.Text = String.Concat(Master.eLang.GetString(538, "Extract"), " **")
        lblMovieImagesHintPreselect.Text = String.Concat("* ", Master.eLang.GetString(1023, "Preselect images in ""Select Images"" dialog"))
        lblMovieImagesHintVideoExtraction.Text = String.Concat("** ", Master.eLang.GetString(666, "Extract images from video file"))
        lblMovieImagesHintVideoExtractionPref.Text = String.Concat("*** ", Master.eLang.GetString(667, "Prefer extracted images"))

        LoadBannerSizes()
        LoadClearArtSizes()
        LoadClearLogoSizes()
        LoadDiscArtSizes()
        LoadFanartSizes()
        LoadLandscapeSizes()
        LoadPosterSizes()
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        txtMovieBannerHeight.KeyPress,
        txtMovieBannerWidth.KeyPress,
        txtMovieExtrafanartsHeight.KeyPress,
        txtMovieExtrafanartsLimit.KeyPress,
        txtMovieExtrafanartsWidth.KeyPress,
        txtMovieExtrathumbsHeight.KeyPress,
        txtMovieExtrathumbsLimit.KeyPress,
        txtMovieExtrathumbsWidth.KeyPress,
        txtMovieFanartHeight.KeyPress,
        txtMovieFanartWidth.KeyPress,
        txtMovieKeyArtHeight.KeyPress,
        txtMovieKeyArtWidth.KeyPress,
        txtMoviePosterHeight.KeyPress,
        txtMoviePosterWidth.KeyPress

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