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
        ChildType = Enums.SettingsPanelType.MovieImage
        IsEnabled = True
        Image = Nothing
        ImageIndex = 6
        Order = 500
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(497, "Images")
        ParentType = Enums.SettingsPanelType.Movie
        UniqueId = "Movie_Image"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        chkCacheEnabled.Text = Master.eLang.GetString(249, "Enable Image Caching")
        chkDisplayImageSelectDialog.Text = Master.eLang.GetString(499, "Display ""Select Images"" dialog while single scraping")
        chkFilterGetBlankImages.Text = Master.eLang.GetString(1207, "Also Get Blank Images")
        chkFilterGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        chkFilterMediaLanguage.Text = Master.eLang.GetString(736, "Only Get Images for the Media Language")
        chkForceLanguage.Text = Master.eLang.GetString(1034, "Force Language")
        gbImageTypes.Text = Master.eLang.GetString(304, "Image Types")
        gbLanguages.Text = Master.eLang.GetString(741, "Preferred Language")
        gbOptions.Text = Master.eLang.GetString(390, "Options")
        lblActorthumbs.Text = Master.eLang.GetString(991, "Actor Thumbs")
        lblBanner.Text = Master.eLang.GetString(838, "Banner")
        lblClearart.Text = Master.eLang.GetString(1096, "ClearArt")
        lblClearlogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        lblDiscart.Text = Master.eLang.GetString(1098, "DiscArt")
        lblExtrafanarts.Text = Master.eLang.GetString(992, "Extrafanarts")
        lblExtrathumbs.Text = Master.eLang.GetString(153, "Extrathumbs")
        lblFanart.Text = Master.eLang.GetString(149, "Fanart")
        lblHintPreselect.Text = String.Concat("* ", Master.eLang.GetString(1023, "Preselect images in ""Select Images"" dialog"))
        lblHintVideoExtraction.Text = String.Concat("** ", Master.eLang.GetString(666, "Extract images from video file"))
        lblHintVideoExtractionPreferred.Text = String.Concat("*** ", Master.eLang.GetString(667, "Prefer extracted images"))
        lblKeepExisting.Text = Master.eLang.GetString(971, "Keep existing")
        lblKeyart.Text = Master.eLang.GetString(296, "KeyArt")
        lblLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        lblLimit.Text = Master.eLang.GetString(578, "Limit")
        lblMaxHeight.Text = Master.eLang.GetString(480, "Max Height")
        lblMaxWidth.Text = Master.eLang.GetString(479, "Max Width")
        lblPoster.Text = Master.eLang.GetString(148, "Poster")
        lblPreferredSize.Text = Master.eLang.GetString(482, "Preferred Size")
        lblPreferredSizeOnly.Text = Master.eLang.GetString(145, "Only")
        lblPreselect.Text = String.Concat(Master.eLang.GetString(308, "Preselect"), " *")
        lblResize.Text = Master.eLang.GetString(481, "Resize")
        lblVideoExtraction.Text = String.Concat(Master.eLang.GetString(538, "Extract"), " **")

        Load_BannerSizes()
        Load_ClearartSizes()
        Load_ClearlogoSizes()
        Load_DiscartSizes()
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
        With Master.eSettings.Movie.ImageSettings
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
            .Actorthumbs.KeepExisting = chkActorthumbsKeepExisting.Checked
            'Banner
            .Banner.KeepExisting = chkBannerKeepExisting.Checked
            .Banner.MaxHeight = If(Not String.IsNullOrEmpty(txtBannerHeight.Text), Convert.ToInt32(txtBannerHeight.Text), 0)
            .Banner.MaxWidth = If(Not String.IsNullOrEmpty(txtBannerWidth.Text), Convert.ToInt32(txtBannerWidth.Text), 0)
            .Banner.PreferredSize = CType(cbBannerPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Banner.PreferredSizeOnly = chkBannerPreferredSizeOnly.Checked
            .Banner.Resize = chkBannerResize.Checked
            'Clearart
            .Clearart.KeepExisting = chkClearartKeepExisting.Checked
            .Clearart.PreferredSize = CType(cbClearartPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Clearart.PreferredSizeOnly = chkClearartPreferredSizeOnly.Checked
            'Clearlogo
            .Clearlogo.KeepExisting = chkClearlogoKeepExisting.Checked
            .Clearlogo.PreferredSize = CType(cbClearlogoPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Clearlogo.PreferredSizeOnly = chkClearlogoPreferredSizeOnly.Checked
            'Discart
            .Discart.KeepExisting = chkDiscartKeepExisting.Checked
            .Discart.PreferredSize = CType(cbDiscartPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Discart.PreferredSizeOnly = chkDiscartPreferredSizeOnly.Checked
            'Extrafanarts
            .Extrafanarts.KeepExisting = chkExtrafanartsKeepExisting.Checked
            .Extrafanarts.Limit = If(Not String.IsNullOrEmpty(txtExtrafanartsLimit.Text), Convert.ToInt32(txtExtrafanartsLimit.Text), 0)
            .Extrafanarts.MaxHeight = If(Not String.IsNullOrEmpty(txtExtrafanartsHeight.Text), Convert.ToInt32(txtExtrafanartsHeight.Text), 0)
            .Extrafanarts.MaxWidth = If(Not String.IsNullOrEmpty(txtExtrafanartsWidth.Text), Convert.ToInt32(txtExtrafanartsWidth.Text), 0)
            .Extrafanarts.PreferredSize = CType(cbExtrafanartsPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Extrafanarts.PreferredSizeOnly = chkExtrafanartsPreferredSizeOnly.Checked
            .Extrafanarts.Preselect = chkExtrafanartsPreselect.Checked
            .Extrafanarts.Resize = chkExtrafanartsResize.Checked
            'Extrathumbs
            .Extrathumbs.KeepExisting = chkExtrathumbsKeepExisting.Checked
            .Extrathumbs.Limit = If(Not String.IsNullOrEmpty(txtExtrathumbsLimit.Text), Convert.ToInt32(txtExtrathumbsLimit.Text), 0)
            .Extrathumbs.MaxHeight = If(Not String.IsNullOrEmpty(txtExtrathumbsHeight.Text), Convert.ToInt32(txtExtrathumbsHeight.Text), 0)
            .Extrathumbs.MaxWidth = If(Not String.IsNullOrEmpty(txtExtrathumbsWidth.Text), Convert.ToInt32(txtExtrathumbsWidth.Text), 0)
            .Extrathumbs.PreferredSize = CType(cbExtrathumbsPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Extrathumbs.PreferredSizeOnly = chkExtrathumbsPreferredSizeOnly.Checked
            .Extrathumbs.Preselect = chkExtrathumbsPreselect.Checked
            .Extrathumbs.Resize = chkExtrathumbsResize.Checked
            .Extrathumbs.VideoExtraction = chkExtrathumbsVideoExtraction.Checked
            .Extrathumbs.VideoExtractionPreferred = chkExtrathumbsVideoExtractionPreferred.Checked
            'Fanart
            .Fanart.KeepExisting = chkFanartKeepExisting.Checked
            .Fanart.MaxHeight = If(Not String.IsNullOrEmpty(txtFanartHeight.Text), Convert.ToInt32(txtFanartHeight.Text), 0)
            .Fanart.MaxWidth = If(Not String.IsNullOrEmpty(txtFanartWidth.Text), Convert.ToInt32(txtFanartWidth.Text), 0)
            .Fanart.PreferredSize = CType(cbFanartPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Fanart.PreferredSizeOnly = chkFanartPreferredSizeOnly.Checked
            .Fanart.Resize = chkFanartResize.Checked
            'Keyart
            .Keyart.KeepExisting = chkKeyartKeepExisting.Checked
            .Keyart.MaxHeight = If(Not String.IsNullOrEmpty(txtKeyartHeight.Text), Convert.ToInt32(txtKeyartHeight.Text), 0)
            .Keyart.MaxWidth = If(Not String.IsNullOrEmpty(txtKeyartWidth.Text), Convert.ToInt32(txtKeyartWidth.Text), 0)
            .Keyart.PreferredSize = CType(cbKeyartPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Keyart.PreferredSizeOnly = chkKeyartPreferredSizeOnly.Checked
            .Keyart.Resize = chkKeyartResize.Checked
            'Landscape
            .Landscape.KeepExisting = chkLandscapeKeepExisting.Checked
            .Landscape.PreferredSize = CType(cbLandscapePreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Landscape.PreferredSizeOnly = chkLandscapePreferredSizeOnly.Checked
            'Poster
            .Poster.KeepExisting = chkPosterKeepExisting.Checked
            .Poster.MaxHeight = If(Not String.IsNullOrEmpty(txtPosterHeight.Text), Convert.ToInt32(txtPosterHeight.Text), 0)
            .Poster.MaxWidth = If(Not String.IsNullOrEmpty(txtPosterWidth.Text), Convert.ToInt32(txtPosterWidth.Text), 0)
            .Poster.PreferredSize = CType(cbPosterPreferredSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .Poster.PreferredSizeOnly = chkPosterPreferredSizeOnly.Checked
            .Poster.Resize = chkPosterResize.Checked
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Private Sub EnableApplyButton(sender As Object, e As EventArgs) Handles _
        cbPosterPreferredSize.SelectedIndexChanged,
        cbLandscapePreferredSize.SelectedIndexChanged,
        cbKeyartPreferredSize.SelectedIndexChanged,
        cbForcedLanguage.SelectedIndexChanged,
        cbFanartPreferredSize.SelectedIndexChanged,
        cbExtrathumbsPreferredSize.SelectedIndexChanged,
        cbExtrafanartsPreferredSize.SelectedIndexChanged,
        cbDiscartPreferredSize.SelectedIndexChanged,
        cbClearlogoPreferredSize.SelectedIndexChanged,
        cbClearartPreferredSize.SelectedIndexChanged,
        cbBannerPreferredSize.SelectedIndexChanged,
        chkActorthumbsKeepExisting.CheckedChanged,
        chkPosterResize.CheckedChanged,
        chkPosterPreferredSizeOnly.CheckedChanged,
        chkPosterKeepExisting.CheckedChanged,
        chkLandscapePreferredSizeOnly.CheckedChanged,
        chkLandscapeKeepExisting.CheckedChanged,
        chkKeyartResize.CheckedChanged,
        chkKeyartPreferredSizeOnly.CheckedChanged,
        chkKeyartKeepExisting.CheckedChanged,
        chkFilterGetEnglishImages.CheckedChanged,
        chkFilterGetBlankImages.CheckedChanged,
        chkFanartResize.CheckedChanged,
        chkFanartPreferredSizeOnly.CheckedChanged,
        chkFanartKeepExisting.CheckedChanged,
        chkExtrathumbsVideoExtractionPreferred.CheckedChanged,
        chkExtrathumbsResize.CheckedChanged,
        chkExtrathumbsPreferredSizeOnly.CheckedChanged,
        chkExtrathumbsKeepExisting.CheckedChanged,
        chkExtrafanartsResize.CheckedChanged,
        chkExtrafanartsPreselect.CheckedChanged,
        chkExtrafanartsPreferredSizeOnly.CheckedChanged,
        chkExtrafanartsKeepExisting.CheckedChanged,
        chkDisplayImageSelectDialog.CheckedChanged,
        chkDiscartPreferredSizeOnly.CheckedChanged,
        chkDiscartKeepExisting.CheckedChanged,
        chkClearlogoPreferredSizeOnly.CheckedChanged,
        chkClearlogoKeepExisting.CheckedChanged,
        chkClearartPreferredSizeOnly.CheckedChanged,
        chkClearartKeepExisting.CheckedChanged,
        chkCacheEnabled.CheckedChanged,
        chkBannerResize.CheckedChanged,
        chkBannerPreferredSizeOnly.CheckedChanged,
        chkBannerKeepExisting.CheckedChanged,
        txtPosterWidth.TextChanged,
        txtPosterHeight.TextChanged,
        txtKeyartWidth.TextChanged,
        txtKeyartHeight.TextChanged,
        txtFanartWidth.TextChanged,
        txtFanartHeight.TextChanged,
        txtExtrathumbsWidth.TextChanged,
        txtExtrathumbsHeight.TextChanged,
        txtExtrafanartsWidth.TextChanged,
        txtExtrafanartsHeight.TextChanged,
        txtBannerWidth.TextChanged,
        txtBannerHeight.TextChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub BannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkBannerResize.CheckedChanged
        txtBannerWidth.Enabled = chkBannerResize.Checked
        txtBannerHeight.Enabled = chkBannerResize.Checked

        If Not chkBannerResize.Checked Then
            txtBannerWidth.Text = String.Empty
            txtBannerHeight.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ExtrafanartsResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkExtrafanartsResize.CheckedChanged
        txtExtrafanartsWidth.Enabled = chkExtrafanartsResize.Checked
        txtExtrafanartsHeight.Enabled = chkExtrafanartsResize.Checked

        If Not chkExtrafanartsResize.Checked Then
            txtExtrafanartsWidth.Text = String.Empty
            txtExtrafanartsHeight.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ExtrathumbsResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkExtrathumbsResize.CheckedChanged
        txtExtrathumbsWidth.Enabled = chkExtrathumbsResize.Checked
        txtExtrathumbsHeight.Enabled = chkExtrathumbsResize.Checked

        If Not chkExtrathumbsResize.Checked Then
            txtExtrathumbsWidth.Text = String.Empty
            txtExtrathumbsHeight.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ExtrathumbsVideoExtraction_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkExtrathumbsVideoExtraction.CheckedChanged
        chkExtrathumbsVideoExtractionPreferred.Enabled = chkExtrathumbsVideoExtraction.Checked
        If Not chkExtrathumbsVideoExtraction.Checked Then
            chkExtrathumbsVideoExtractionPreferred.Checked = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub FanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkFanartResize.CheckedChanged
        txtFanartWidth.Enabled = chkFanartResize.Checked
        txtFanartHeight.Enabled = chkFanartResize.Checked

        If Not chkFanartResize.Checked Then
            txtFanartWidth.Text = String.Empty
            txtFanartHeight.Text = String.Empty
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

    Private Sub KeyartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkKeyartResize.CheckedChanged
        txtKeyartWidth.Enabled = chkKeyartResize.Checked
        txtKeyartHeight.Enabled = chkKeyartResize.Checked

        If Not chkKeyartResize.Checked Then
            txtKeyartWidth.Text = String.Empty
            txtKeyartHeight.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Load_BannerSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x185", Enums.ImageSize.HD185}
        }
        LoadItemsToComboBox(cbBannerPreferredSize, items)
    End Sub

    Private Sub Load_ClearartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x562", Enums.ImageSize.HD562},
            {"500x281", Enums.ImageSize.SD281}
        }
        LoadItemsToComboBox(cbClearartPreferredSize, items)
    End Sub

    Private Sub Load_ClearlogoSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"800x310", Enums.ImageSize.HD310},
            {"400x155", Enums.ImageSize.SD155}
        }
        LoadItemsToComboBox(cbClearlogoPreferredSize, items)
    End Sub

    Private Sub Load_DiscartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"1000x1000", Enums.ImageSize.HD1000}
        }
        LoadItemsToComboBox(cbDiscartPreferredSize, items)
    End Sub

    Private Sub Load_FanartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"3840x2160", Enums.ImageSize.UHD2160},
            {"2560x1440", Enums.ImageSize.QHD1440},
            {"1920x1080", Enums.ImageSize.HD1080},
            {"1280x720", Enums.ImageSize.HD720}
        }
        LoadItemsToComboBox(cbExtrafanartsPreferredSize, items)
        LoadItemsToComboBox(cbExtrathumbsPreferredSize, items)
        LoadItemsToComboBox(cbFanartPreferredSize, items)
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
        LoadItemsToComboBox(cbLandscapePreferredSize, items)
    End Sub

    Private Sub Load_PosterSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize) From {
            {Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any},
            {"2000x3000", Enums.ImageSize.HD3000},
            {"1400x2100", Enums.ImageSize.HD2100},
            {"1000x1500", Enums.ImageSize.HD1500},
            {"1000x1426", Enums.ImageSize.HD1426}
        }
        LoadItemsToComboBox(cbKeyartPreferredSize, items)
        LoadItemsToComboBox(cbPosterPreferredSize, items)
    End Sub

    Private Sub PosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkPosterResize.CheckedChanged
        txtPosterWidth.Enabled = chkPosterResize.Checked
        txtPosterHeight.Enabled = chkPosterResize.Checked

        If Not chkPosterResize.Checked Then
            txtPosterWidth.Text = String.Empty
            txtPosterHeight.Text = String.Empty
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Public Sub Settings_Load()
        With Master.eSettings.Movie.ImageSettings
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

            'ImageTypes
            'Actorthumbs
            chkActorthumbsKeepExisting.Checked = .Actorthumbs.KeepExisting
            'Banner
            cbBannerPreferredSize.SelectedValue = .Banner.PreferredSize
            chkBannerKeepExisting.Checked = .Banner.KeepExisting
            chkBannerPreferredSizeOnly.Checked = .Banner.PreferredSizeOnly
            chkBannerResize.Checked = .Banner.Resize
            If .Banner.Resize Then
                txtBannerHeight.Text = .Banner.MaxHeight.ToString
                txtBannerWidth.Text = .Banner.MaxWidth.ToString
            End If
            'Clearart
            cbClearartPreferredSize.SelectedValue = .Clearart.PreferredSize
            chkClearartKeepExisting.Checked = .Clearart.KeepExisting
            chkClearartPreferredSizeOnly.Checked = .Clearart.PreferredSizeOnly
            'Clearlogo
            cbClearlogoPreferredSize.SelectedValue = .Clearlogo.PreferredSize
            chkClearlogoKeepExisting.Checked = .Clearlogo.KeepExisting
            chkClearlogoPreferredSizeOnly.Checked = .Clearlogo.PreferredSizeOnly
            'Discart
            cbDiscartPreferredSize.SelectedValue = .Discart.PreferredSize
            chkDiscartKeepExisting.Checked = .Discart.KeepExisting
            chkDiscartPreferredSizeOnly.Checked = .Discart.PreferredSizeOnly
            'Extrafanarts
            cbExtrafanartsPreferredSize.SelectedValue = .Extrafanarts.PreferredSize
            chkExtrafanartsKeepExisting.Checked = .Extrafanarts.KeepExisting
            chkExtrafanartsPreferredSizeOnly.Checked = .Extrafanarts.PreferredSizeOnly
            chkExtrafanartsPreselect.Checked = .Extrafanarts.Preselect
            chkExtrafanartsResize.Checked = .Extrafanarts.Resize
            If .Extrafanarts.Resize Then
                txtExtrafanartsHeight.Text = .Extrafanarts.MaxHeight.ToString
                txtExtrafanartsWidth.Text = .Extrafanarts.MaxWidth.ToString
            End If
            txtExtrafanartsLimit.Text = .Extrafanarts.Limit.ToString
            'Extrathumbs
            cbExtrathumbsPreferredSize.SelectedValue = .Extrathumbs.PreferredSize
            chkExtrathumbsKeepExisting.Checked = .Extrathumbs.KeepExisting
            chkExtrathumbsPreferredSizeOnly.Checked = .Extrathumbs.PreferredSizeOnly
            chkExtrathumbsPreselect.Checked = .Extrathumbs.Preselect
            chkExtrathumbsResize.Checked = .Extrathumbs.Resize
            If .Extrathumbs.Resize Then
                txtExtrathumbsHeight.Text = .Extrathumbs.MaxHeight.ToString
                txtExtrathumbsWidth.Text = .Extrathumbs.MaxWidth.ToString
            End If
            chkExtrathumbsVideoExtraction.Checked = .Extrathumbs.VideoExtraction
            chkExtrathumbsVideoExtractionPreferred.Checked = .Extrathumbs.VideoExtractionPreferred
            txtExtrathumbsLimit.Text = .Extrathumbs.Limit.ToString
            'Fanart
            cbFanartPreferredSize.SelectedValue = .Fanart.PreferredSize
            chkFanartKeepExisting.Checked = .Fanart.KeepExisting
            chkFanartPreferredSizeOnly.Checked = .Fanart.PreferredSizeOnly
            chkFanartResize.Checked = .Fanart.Resize
            If .Fanart.Resize Then
                txtFanartHeight.Text = .Fanart.MaxHeight.ToString
                txtFanartWidth.Text = .Fanart.MaxWidth.ToString
            End If
            'Keyart
            cbKeyartPreferredSize.SelectedValue = .Keyart.PreferredSize
            chkKeyartKeepExisting.Checked = .Keyart.KeepExisting
            chkKeyartPreferredSizeOnly.Checked = .Keyart.PreferredSizeOnly
            chkKeyartResize.Checked = .Keyart.Resize
            If .Keyart.Resize Then
                txtKeyartHeight.Text = .Keyart.MaxHeight.ToString
                txtKeyartWidth.Text = .Keyart.MaxWidth.ToString
            End If
            'Landscape
            cbLandscapePreferredSize.SelectedValue = .Landscape.PreferredSize
            chkLandscapeKeepExisting.Checked = .Landscape.KeepExisting
            chkLandscapePreferredSizeOnly.Checked = .Landscape.PreferredSizeOnly
            'Poster
            cbPosterPreferredSize.SelectedValue = .Poster.PreferredSize
            chkPosterKeepExisting.Checked = .Poster.KeepExisting
            chkPosterPreferredSizeOnly.Checked = .Poster.PreferredSizeOnly
            chkPosterResize.Checked = .Poster.Resize
            If .Poster.Resize Then
                txtPosterHeight.Text = .Poster.MaxHeight.ToString
                txtPosterWidth.Text = .Poster.MaxWidth.ToString
            End If
        End With
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        txtBannerHeight.KeyPress,
        txtBannerWidth.KeyPress,
        txtExtrafanartsHeight.KeyPress,
        txtExtrafanartsLimit.KeyPress,
        txtExtrafanartsWidth.KeyPress,
        txtExtrathumbsHeight.KeyPress,
        txtExtrathumbsLimit.KeyPress,
        txtExtrathumbsWidth.KeyPress,
        txtFanartHeight.KeyPress,
        txtFanartWidth.KeyPress,
        txtKeyartHeight.KeyPress,
        txtKeyartWidth.KeyPress,
        txtPosterHeight.KeyPress,
        txtPosterWidth.KeyPress

        e.Handled = StringUtils.IntegerOnly(e.KeyChar)
    End Sub

    Private Sub TextBox_Limit_Leave(sender As Object, e As EventArgs) Handles _
        txtExtrafanartsLimit.Leave,
        txtExtrathumbsLimit.Leave

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