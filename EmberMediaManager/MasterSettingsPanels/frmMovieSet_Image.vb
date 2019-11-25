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

Public Class frmMovieset_Image
    Implements Interfaces.IMasterSettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

#End Region 'Fields


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

#Region "Handles"

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

#End Region 'Handles

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
            .Contains = Enums.SettingsPanelType.MoviesetImage,
            .ImageIndex = 6,
            .Order = 400,
            .Panel = pnlSettings,
            .SettingsPanelID = "Movieset_Image",
            .Title = Master.eLang.GetString(497, "Images"),
            .Type = Enums.SettingsPanelType.Movieset
        }
    End Function

    Public Sub SaveSetup() Implements Interfaces.IMasterSettingsPanel.SaveSetup
        With Master.eSettings
            .MovieSetBannerHeight = If(Not String.IsNullOrEmpty(txtMovieSetBannerHeight.Text), Convert.ToInt32(txtMovieSetBannerHeight.Text), 0)
            .MovieSetBannerKeepExisting = chkMovieSetBannerKeepExisting.Checked
            .MovieSetBannerPrefSize = CType(cbMovieSetBannerPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetBannerPrefSizeOnly = chkMovieSetBannerPrefSizeOnly.Checked
            .MovieSetBannerResize = chkMovieSetBannerResize.Checked
            .MovieSetBannerWidth = If(Not String.IsNullOrEmpty(txtMovieSetBannerWidth.Text), Convert.ToInt32(txtMovieSetBannerWidth.Text), 0)
            .MovieSetClearArtKeepExisting = chkMovieSetClearArtKeepExisting.Checked
            .MovieSetClearArtPrefSize = CType(cbMovieSetClearArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetClearArtPrefSizeOnly = chkMovieSetClearArtPrefSizeOnly.Checked
            .MovieSetClearLogoKeepExisting = chkMovieSetClearLogoKeepExisting.Checked
            .MovieSetClearLogoPrefSize = CType(cbMovieSetClearLogoPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetClearLogoPrefSizeOnly = chkMovieSetClearLogoPrefSizeOnly.Checked
            .MovieSetDiscArtKeepExisting = chkMovieSetDiscArtKeepExisting.Checked
            .MovieSetDiscArtPrefSize = CType(cbMovieSetDiscArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetDiscArtPrefSizeOnly = chkMovieSetDiscArtPrefSizeOnly.Checked
            .MovieSetFanartHeight = If(Not String.IsNullOrEmpty(txtMovieSetFanartHeight.Text), Convert.ToInt32(txtMovieSetFanartHeight.Text), 0)
            .MovieSetFanartKeepExisting = chkMovieSetFanartKeepExisting.Checked
            .MovieSetFanartPrefSize = CType(cbMovieSetFanartPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetFanartPrefSizeOnly = chkMovieSetFanartPrefSizeOnly.Checked
            .MovieSetFanartResize = chkMovieSetFanartResize.Checked
            .MovieSetFanartWidth = If(Not String.IsNullOrEmpty(txtMovieSetFanartWidth.Text), Convert.ToInt32(txtMovieSetFanartWidth.Text), 0)
            .MovieSetImagesCacheEnabled = chkMovieSetImagesCacheEnabled.Checked
            .MovieSetImagesDisplayImageSelect = chkMovieSetImagesDisplayImageSelect.Checked
            If Not String.IsNullOrEmpty(cbMovieSetImagesForcedLanguage.Text) Then
                .MovieSetImagesForcedLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Name = cbMovieSetImagesForcedLanguage.Text).Abbrevation_MainLanguage
            End If
            .MovieSetImagesForceLanguage = chkMovieSetImagesForceLanguage.Checked
            .MovieSetImagesGetBlankImages = chkMovieSetImagesGetBlankImages.Checked
            .MovieSetImagesGetEnglishImages = chkMovieSetImagesGetEnglishImages.Checked
            .MovieSetImagesMediaLanguageOnly = chkMovieSetImagesMediaLanguageOnly.Checked
            .MovieSetKeyArtHeight = If(Not String.IsNullOrEmpty(txtMovieSetKeyArtHeight.Text), Convert.ToInt32(txtMovieSetKeyArtHeight.Text), 0)
            .MovieSetKeyArtKeepExisting = chkMovieSetKeyArtKeepExisting.Checked
            .MovieSetKeyArtPrefSize = CType(cbMovieSetKeyArtPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetKeyArtPrefSizeOnly = chkMovieSetKeyArtPrefSizeOnly.Checked
            .MovieSetKeyArtResize = chkMovieSetKeyArtResize.Checked
            .MovieSetKeyArtWidth = If(Not String.IsNullOrEmpty(txtMovieSetKeyArtWidth.Text), Convert.ToInt32(txtMovieSetKeyArtWidth.Text), 0)
            .MovieSetLandscapeKeepExisting = chkMovieSetLandscapeKeepExisting.Checked
            .MovieSetLandscapePrefSize = CType(cbMovieSetLandscapePrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetLandscapePrefSizeOnly = chkMovieSetLandscapePrefSizeOnly.Checked
            .MovieSetPosterHeight = If(Not String.IsNullOrEmpty(txtMovieSetPosterHeight.Text), Convert.ToInt32(txtMovieSetPosterHeight.Text), 0)
            .MovieSetPosterKeepExisting = chkMovieSetPosterKeepExisting.Checked
            .MovieSetPosterPrefSize = CType(cbMovieSetPosterPrefSize.SelectedItem, KeyValuePair(Of String, Enums.ImageSize)).Value
            .MovieSetPosterPrefSizeOnly = chkMovieSetPosterPrefSizeOnly.Checked
            .MovieSetPosterResize = chkMovieSetPosterResize.Checked
            .MovieSetPosterWidth = If(Not String.IsNullOrEmpty(txtMovieSetPosterWidth.Text), Convert.ToInt32(txtMovieSetPosterWidth.Text), 0)
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            cbMovieSetBannerPrefSize.SelectedValue = .MovieSetBannerPrefSize
            cbMovieSetClearArtPrefSize.SelectedValue = .MovieSetClearArtPrefSize
            cbMovieSetClearLogoPrefSize.SelectedValue = .MovieSetClearLogoPrefSize
            cbMovieSetDiscArtPrefSize.SelectedValue = .MovieSetDiscArtPrefSize
            cbMovieSetFanartPrefSize.SelectedValue = .MovieSetFanartPrefSize
            cbMovieSetKeyArtPrefSize.SelectedValue = .MovieSetKeyArtPrefSize
            cbMovieSetLandscapePrefSize.SelectedValue = .MovieSetLandscapePrefSize
            cbMovieSetPosterPrefSize.SelectedValue = .MovieSetPosterPrefSize
            chkMovieSetBannerKeepExisting.Checked = .MovieSetBannerKeepExisting
            chkMovieSetBannerPrefSizeOnly.Checked = .MovieSetBannerPrefSizeOnly
            chkMovieSetBannerResize.Checked = .MovieSetBannerResize
            If .MovieSetBannerResize Then
                txtMovieSetBannerHeight.Text = .MovieSetBannerHeight.ToString
                txtMovieSetBannerWidth.Text = .MovieSetBannerWidth.ToString
            End If
            chkMovieSetClearArtKeepExisting.Checked = .MovieSetClearArtKeepExisting
            chkMovieSetClearArtPrefSizeOnly.Checked = .MovieSetClearArtPrefSizeOnly
            chkMovieSetClearLogoKeepExisting.Checked = .MovieSetClearLogoKeepExisting
            chkMovieSetClearLogoPrefSizeOnly.Checked = .MovieSetClearLogoPrefSizeOnly
            chkMovieSetDiscArtKeepExisting.Checked = .MovieSetDiscArtKeepExisting
            chkMovieSetDiscArtPrefSizeOnly.Checked = .MovieSetDiscArtPrefSizeOnly
            chkMovieSetFanartKeepExisting.Checked = .MovieSetFanartKeepExisting
            chkMovieSetFanartPrefSizeOnly.Checked = .MovieSetFanartPrefSizeOnly
            chkMovieSetFanartResize.Checked = .MovieSetFanartResize
            If .MovieSetFanartResize Then
                txtMovieSetFanartHeight.Text = .MovieSetFanartHeight.ToString
                txtMovieSetFanartWidth.Text = .MovieSetFanartWidth.ToString
            End If
            chkMovieSetImagesCacheEnabled.Checked = .MovieSetImagesCacheEnabled
            chkMovieSetImagesDisplayImageSelect.Checked = .MovieSetImagesDisplayImageSelect
            chkMovieSetImagesForceLanguage.Checked = .MovieSetImagesForceLanguage
            If .MovieSetImagesMediaLanguageOnly Then
                chkMovieSetImagesMediaLanguageOnly.Checked = True
                chkMovieSetImagesGetBlankImages.Checked = .MovieSetImagesGetBlankImages
                chkMovieSetImagesGetEnglishImages.Checked = .MovieSetImagesGetEnglishImages
            End If
            chkMovieSetKeyArtKeepExisting.Checked = .MovieSetKeyArtKeepExisting
            chkMovieSetKeyArtPrefSizeOnly.Checked = .MovieSetKeyArtPrefSizeOnly
            chkMovieSetKeyArtResize.Checked = .MovieSetKeyArtResize
            If .MovieSetKeyArtResize Then
                txtMovieSetKeyArtHeight.Text = .MovieSetKeyArtHeight.ToString
                txtMovieSetKeyArtWidth.Text = .MovieSetKeyArtWidth.ToString
            End If
            chkMovieSetLandscapeKeepExisting.Checked = .MovieSetLandscapeKeepExisting
            chkMovieSetLandscapePrefSizeOnly.Checked = .MovieSetLandscapePrefSizeOnly
            chkMovieSetPosterKeepExisting.Checked = .MovieSetPosterKeepExisting
            chkMovieSetPosterPrefSizeOnly.Checked = .MovieSetPosterPrefSizeOnly
            chkMovieSetPosterResize.Checked = .MovieSetPosterResize
            If .MovieSetPosterResize Then
                txtMovieSetPosterHeight.Text = .MovieSetPosterHeight.ToString
                txtMovieSetPosterWidth.Text = .MovieSetPosterWidth.ToString
            End If
            Try
                cbMovieSetImagesForcedLanguage.Items.Clear()
                cbMovieSetImagesForcedLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguagesXML.Languages Select lLang.Name).Distinct.ToArray)
                If cbMovieSetImagesForcedLanguage.Items.Count > 0 Then
                    If Not String.IsNullOrEmpty(.MovieSetImagesForcedLanguage) Then
                        Dim tLanguage As languageProperty = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = .MovieSetImagesForcedLanguage)
                        If tLanguage IsNot Nothing Then
                            cbMovieSetImagesForcedLanguage.Text = tLanguage.Name
                        Else
                            tLanguage = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.MovieSetImagesForcedLanguage))
                            If tLanguage IsNot Nothing Then
                                cbMovieSetImagesForcedLanguage.Text = tLanguage.Name
                            Else
                                cbMovieSetImagesForcedLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                            End If
                        End If
                    Else
                        cbMovieSetImagesForcedLanguage.Text = APIXML.ScraperLanguagesXML.Languages.FirstOrDefault(Function(l) l.Abbrevation_MainLanguage = "en").Name
                    End If
                End If
            Catch ex As Exception
                _Logger.Error(ex, New StackFrame().GetMethod().Name)
            End Try
        End With

        LoadBannerSizes()
        LoadClearArtSizes()
        LoadClearLogoSizes()
        LoadDiscArtSizes()
        LoadFanartSizes()
        LoadLandscapeSizes()
        LoadPosterSizes()
    End Sub

    Private Sub Setup()
        chkMovieSetImagesGetBlankImages.Text = Master.eLang.GetString(1207, "Also Get Blank Images")
        chkMovieSetImagesGetEnglishImages.Text = Master.eLang.GetString(737, "Also Get English Images")
        lblMovieSetImagesHeaderBanner.Text = Master.eLang.GetString(838, "Banner")
        lblMovieSetImagesHeaderClearArt.Text = Master.eLang.GetString(1096, "ClearArt")
        lblMovieSetImagesHeaderClearLogo.Text = Master.eLang.GetString(1097, "ClearLogo")
        lblMovieSetImagesHeaderDiscArt.Text = Master.eLang.GetString(1098, "DiscArt")
        chkMovieSetImagesDisplayImageSelect.Text = Master.eLang.GetString(499, "Display ""Select Images"" dialog while single scraping")
        chkMovieSetImagesCacheEnabled.Text = Master.eLang.GetString(249, "Enable Image Caching")
        lblMovieSetImagesHeaderFanart.Text = Master.eLang.GetString(149, "Fanart")
        chkMovieSetImagesForceLanguage.Text = Master.eLang.GetString(1034, "Force Language")
        gbMovieSetImagesOpts.Text = Master.eLang.GetString(497, "Images")
        gbMovieSetImagesImageTypesOpts.Text = Master.eLang.GetString(304, "Image Types")
        lblMovieSetImagesHeaderKeepExisting.Text = Master.eLang.GetString(971, "Keep existing")
        lblMovieSetImagesHeaderKeyArt.Text = Master.eLang.GetString(296, "KeyArt")
        lblMovieSetImagesHeaderLandscape.Text = Master.eLang.GetString(1059, "Landscape")
        lblMovieSetImagesHeaderMaxHeight.Text = Master.eLang.GetString(480, "Max Height")
        lblMovieSetImagesHeaderMaxWidth.Text = Master.eLang.GetString(479, "Max Width")
        lblMovieSetImagesHeaderPrefSizeOnly.Text = Master.eLang.GetString(145, "Only")
        chkMovieSetImagesMediaLanguageOnly.Text = Master.eLang.GetString(736, "Only Get Images for the Media Language")
        lblMovieSetImagesHeaderPoster.Text = Master.eLang.GetString(148, "Poster")
        gbMovieSetImagesLanguageOpts.Text = Master.eLang.GetString(741, "Preferred Language")
        lblMovieSetImagesHeaderPrefSize.Text = Master.eLang.GetString(482, "Preferred Size")
        lblMovieSetImagesHeaderBanner.Text = Master.eLang.GetString(481, "Resize")

    End Sub

    Private Sub chkMovieSetBannerResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieSetBannerWidth.Enabled = chkMovieSetBannerResize.Checked
        txtMovieSetBannerHeight.Enabled = chkMovieSetBannerResize.Checked

        If Not chkMovieSetBannerResize.Checked Then
            txtMovieSetBannerWidth.Text = String.Empty
            txtMovieSetBannerHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetFanartResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieSetFanartWidth.Enabled = chkMovieSetFanartResize.Checked
        txtMovieSetFanartHeight.Enabled = chkMovieSetFanartResize.Checked

        If Not chkMovieSetFanartResize.Checked Then
            txtMovieSetFanartWidth.Text = String.Empty
            txtMovieSetFanartHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetKeyArtResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieSetKeyArtWidth.Enabled = chkMovieSetKeyArtResize.Checked
        txtMovieSetKeyArtHeight.Enabled = chkMovieSetKeyArtResize.Checked

        If Not chkMovieSetKeyArtResize.Checked Then
            txtMovieSetKeyArtWidth.Text = String.Empty
            txtMovieSetKeyArtHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetPosterResize_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()

        txtMovieSetPosterWidth.Enabled = chkMovieSetPosterResize.Checked
        txtMovieSetPosterHeight.Enabled = chkMovieSetPosterResize.Checked

        If Not chkMovieSetPosterResize.Checked Then
            txtMovieSetPosterWidth.Text = String.Empty
            txtMovieSetPosterHeight.Text = String.Empty
        End If
    End Sub

    Private Sub chkMovieSetImagesForceLanguage_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()

        cbMovieSetImagesForcedLanguage.Enabled = chkMovieSetImagesForceLanguage.Checked
    End Sub

    Private Sub chkMovieSetImagesMediaLanguageOnly_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()

        chkMovieSetImagesGetBlankImages.Enabled = chkMovieSetImagesMediaLanguageOnly.Checked
        chkMovieSetImagesGetEnglishImages.Enabled = chkMovieSetImagesMediaLanguageOnly.Checked

        If Not chkMovieSetImagesMediaLanguageOnly.Checked Then
            chkMovieSetImagesGetBlankImages.Checked = False
            chkMovieSetImagesGetEnglishImages.Checked = False
        End If
    End Sub

    Private Sub LoadBannerSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x185", Enums.ImageSize.HD185)
        LoadItemsToComboBox(cbMovieSetBannerPrefSize, items)
    End Sub

    Private Sub LoadClearArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x562", Enums.ImageSize.HD562)
        items.Add("500x281", Enums.ImageSize.SD281)
        LoadItemsToComboBox(cbMovieSetClearArtPrefSize, items)
    End Sub

    Private Sub LoadClearLogoSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("800x310", Enums.ImageSize.HD310)
        items.Add("400x155", Enums.ImageSize.SD155)
        LoadItemsToComboBox(cbMovieSetClearLogoPrefSize, items)
    End Sub

    Private Sub LoadDiscArtSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("1000x1000", Enums.ImageSize.HD1000)
        LoadItemsToComboBox(cbMovieSetDiscArtPrefSize, items)
    End Sub

    Private Sub LoadFanartSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("3840x2160", Enums.ImageSize.UHD2160)
        items.Add("2560x1440", Enums.ImageSize.QHD1440)
        items.Add("1920x1080", Enums.ImageSize.HD1080)
        items.Add("1280x720", Enums.ImageSize.HD720)
        LoadItemsToComboBox(cbMovieSetFanartPrefSize, items)
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
        LoadItemsToComboBox(cbMovieSetLandscapePrefSize, items)
    End Sub

    Private Sub LoadPosterSizes()
        Dim items As New Dictionary(Of String, Enums.ImageSize)
        items.Add(Master.eLang.GetString(745, "Any"), Enums.ImageSize.Any)
        items.Add("2000x3000", Enums.ImageSize.HD3000)
        items.Add("1400x2100", Enums.ImageSize.HD2100)
        items.Add("1000x1500", Enums.ImageSize.HD1500)
        items.Add("1000x1426", Enums.ImageSize.HD1426)
        LoadItemsToComboBox(cbMovieSetKeyArtPrefSize, items)
        LoadItemsToComboBox(cbMovieSetPosterPrefSize, items)
    End Sub

#End Region 'Methods

End Class