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
Imports System.IO


Public Class frmOption_General
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
            .Contains = Enums.SettingsPanelType.OptionsGeneral,
            .ImageIndex = 0,
            .Order = 100,
            .Panel = pnlSettings,
            .SettingsPanelID = "Option_General",
            .Title = Master.eLang.GetString(38, "General"),
            .Type = Enums.SettingsPanelType.Options
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Manager.mSettings.MainOptions.GuiSettings
            .DoubleClickScrapeEnabled = chkDoubleClickScrape.Checked
            .DisplayBanner = chkDisplayBanner.Checked
            .DisplayCharacterArt = chkDisplayCharacterArt.Checked
            .DisplayClearArt = chkDisplayClearArt.Checked
            .DisplayClearLogo = chkDisplayClearLogo.Checked
            .DisplayDiscArt = chkDisplayDiscArt.Checked
            .DisplayFanart = chkDisplayFanart.Checked
            .DisplayFanartSmall = chkDisplayFanartSmall.Checked
            .DisplayKeyArt = chkDisplayKeyArt.Checked
            .DisplayLandscape = chkDisplayLandscape.Checked
            .DisplayPoster = chkDisplayPoster.Checked
            .ShowImgGlassOverlay = chkImagesGlassOverlay.Checked
            .ShowGenresText = chkDisplayGenresText.Checked
            .ShowStudioText = chkDisplayStudioText.Checked
            .ShowLangFlags = chkDisplayLangFlags.Checked
            .ShowImgDimensions = chkDisplayImageDimension.Checked
            .ShowImgNames = chkDisplayImageNames.Checked
            .Theme = cbTheme.Text
        End With
        With Master.eSettings.Options.General
            .CheckForUpdates = chkCheckForUpdates.Checked
            .DigitGrpSymbolVotesEnabled = chkDigitGrpSymbolVotes.Checked
            .Language = cbInterfaceLanguage.Text
            .ShowNews = chkShowNews.Checked
        End With

        With Master.eSettings
            .GeneralImageFilter = chkImageFilter.Checked
            .GeneralImageFilterAutoscraper = chkImageFilterAutoscraper.Checked
            .GeneralImageFilterFanart = chkImageFilterFanart.Checked
            .GeneralImageFilterImagedialog = chklImageFilterImageDialog.Checked
            .GeneralImageFilterPoster = chkImageFilterPoster.Checked
            If Not String.IsNullOrEmpty(txtImageFilterFanartMatchRate.Text) AndAlso Integer.TryParse(txtImageFilterFanartMatchRate.Text, 4) Then
                .GeneralImageFilterFanartMatchTolerance = Convert.ToInt32(txtImageFilterFanartMatchRate.Text)
            Else
                .GeneralImageFilterFanartMatchTolerance = 4
            End If
            If Not String.IsNullOrEmpty(txtImageFilterPosterMatchRate.Text) AndAlso Integer.TryParse(txtImageFilterPosterMatchRate.Text, 1) Then
                .GeneralImageFilterPosterMatchTolerance = Convert.ToInt32(txtImageFilterPosterMatchRate.Text)
            Else
                .GeneralImageFilterPosterMatchTolerance = 1
            End If
        End With
        Save_SortTokens()
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Manager.mSettings.MainOptions.GuiSettings
            cbTheme.SelectedItem = .Theme
            chkDoubleClickScrape.Checked = .DoubleClickScrapeEnabled
            chkDisplayBanner.Checked = .DisplayBanner
            chkDisplayCharacterArt.Checked = .DisplayCharacterArt
            chkDisplayClearArt.Checked = .DisplayClearArt
            chkDisplayClearLogo.Checked = .DisplayClearLogo
            chkDisplayDiscArt.Checked = .DisplayDiscArt
            chkDisplayFanart.Checked = .DisplayFanart
            chkDisplayFanartSmall.Checked = .DisplayFanartSmall
            chkDisplayKeyArt.Checked = .DisplayKeyArt
            chkDisplayLandscape.Checked = .DisplayLandscape
            chkDisplayPoster.Checked = .DisplayPoster
            chkImagesGlassOverlay.Checked = .ShowImgGlassOverlay
            chkDisplayGenresText.Checked = .ShowGenresText
            chkDisplayStudioText.Checked = .ShowStudioText
            chkDisplayLangFlags.Checked = .ShowLangFlags
            chkDisplayImageDimension.Checked = .ShowImgDimensions
            chkDisplayImageNames.Checked = .ShowImgNames
        End With
        With Master.eSettings.Options.General
            RemoveHandler cbInterfaceLanguage.SelectedIndexChanged, AddressOf InterfaceLanguage_SelectedIndexChanged
            cbInterfaceLanguage.SelectedItem = .Language
            AddHandler cbInterfaceLanguage.SelectedIndexChanged, AddressOf InterfaceLanguage_SelectedIndexChanged
            chkCheckForUpdates.Checked = .CheckForUpdates
            chkDigitGrpSymbolVotes.Checked = .DigitGrpSymbolVotesEnabled
            chkShowNews.Checked = .ShowNews

            DataGridView_Fill_SortTokens(.SortTokens)
        End With
        With Master.eSettings
            chkImageFilter.Checked = .GeneralImageFilter
            chkImageFilterAutoscraper.Checked = .GeneralImageFilterAutoscraper
            txtImageFilterFanartMatchRate.Enabled = .GeneralImageFilterFanart
            chkImageFilterFanart.Checked = .GeneralImageFilterFanart
            chklImageFilterImageDialog.Checked = .GeneralImageFilterImagedialog
            chkImageFilterPoster.Checked = .GeneralImageFilterPoster
            txtImageFilterPosterMatchRate.Enabled = .GeneralImageFilterPoster
            txtImageFilterPosterMatchRate.Text = .GeneralImageFilterPosterMatchTolerance.ToString
            txtImageFilterFanartMatchRate.Text = .GeneralImageFilterFanartMatchTolerance.ToString

        End With
    End Sub

    Private Sub Setup()
        With Master.eLang
            btnDigitGrpSymbolSettings.Text = .GetString(420, "Settings")
            btnSortTokensDefaults.Text = .GetString(713, "Defaults")
            chkCheckForUpdates.Text = .GetString(432, "Check for Updates")
            chkDigitGrpSymbolVotes.Text = .GetString(1387, "Use digit grouping symbol for Votes count")
            chkDisplayBanner.Text = .GetString(1146, "Display Banner")
            chkDisplayCharacterArt.Text = .GetString(1147, "Display CharacterArt")
            chkDisplayClearArt.Text = .GetString(1148, "Display ClearArt")
            chkDisplayClearLogo.Text = .GetString(1149, "Display ClearLogo")
            chkDisplayDiscArt.Text = .GetString(1150, "Display DiscArt")
            chkDisplayFanart.Text = .GetString(455, "Display Fanart")
            chkDisplayFanartSmall.Text = .GetString(967, "Display Small Fanart")
            chkDisplayGenresText.Text = .GetString(453, "Always Display Genre Text")
            chkDisplayImageDimension.Text = .GetString(457, "Display Image Dimensions")
            chkDisplayImageNames.Text = .GetString(1255, "Display Image Names")
            chkDisplayLandscape.Text = .GetString(1151, "Display Landscape")
            chkDisplayLangFlags.Text = .GetString(489, "Display Language Flags")
            chkDisplayPoster.Text = .GetString(456, "Display Poster")
            chkDoubleClickScrape.Text = .GetString(1198, "Enable Image Scrape On Double Right Click")
            chkImageFilter.Text = .GetString(1459, "Activate ImageFilter to avoid duplicate images")
            chkImageFilterAutoscraper.Text = .GetString(1457, "Autoscraper")
            chkImageFilterFanart.Text = .GetString(149, "Fanart")
            chkImageFilterPoster.Text = .GetString(148, "Poster")
            chkImagesGlassOverlay.Text = .GetString(966, "Enable Images Glass Overlay")
            chkShowNews.Text = .GetString(669, "Show News and Information after Start")
            chklImageFilterImageDialog.Text = .GetString(1458, "Imagedialog")
            gbInterface.Text = .GetString(795, "Interface")
            gbMainWindow.Text = .GetString(1152, "Main Window")
            gbMiscellaneous.Text = .GetString(429, "Miscellaneous")
            gbSortTokens.Text = .GetString(463, "Sort Tokens to Ignore")
            lblImageFilterFanartMatchRate.Text = .GetString(149, "Fanart") & " " & .GetString(461, "Mismatch Tolerance:")
            lblImageFilterPosterMatchRate.Text = .GetString(148, "Poster") & " " & .GetString(461, "Mismatch Tolerance:")
            lblInterfaceLanguage.Text = .GetString(430, "Interface Language:")
            lblTheme.Text = String.Concat(.GetString(620, "Theme"), ":")
        End With
        Load_InterfaceLanguages()
        Load_Themes()
    End Sub
    Private Sub Enable_ApplyButton(ByVal sender As Object, ByVal e As EventArgs) Handles _
        chkCheckForUpdates.CheckedChanged,
        chkDigitGrpSymbolVotes.CheckedChanged,
        chkDisplayBanner.CheckedChanged,
        chkDisplayCharacterArt.CheckedChanged,
        chkDisplayClearArt.CheckedChanged,
        chkDisplayClearLogo.CheckedChanged,
        chkDisplayDiscArt.CheckedChanged,
        chkDisplayFanart.CheckedChanged,
        chkDisplayFanartSmall.CheckedChanged,
        chkDisplayGenresText.CheckedChanged,
        chkDisplayImageDimension.CheckedChanged,
        chkDisplayImageNames.CheckedChanged,
        chkDisplayKeyArt.CheckedChanged,
        chkDisplayLandscape.CheckedChanged,
        chkDisplayLangFlags.CheckedChanged,
        chkDisplayPoster.CheckedChanged,
        chkDisplayStudioText.CheckedChanged,
        chkDoubleClickScrape.CheckedChanged,
        dgvSortTokens.CellValueChanged,
        dgvSortTokens.RowsAdded,
        dgvSortTokens.RowsRemoved

        CheckHideSettings()
        Handle_SettingsChanged()
    End Sub

    Private Sub CheckHideSettings()
        If chkDisplayBanner.Checked OrElse
            chkDisplayCharacterArt.Checked OrElse
            chkDisplayClearArt.Checked OrElse
            chkDisplayClearLogo.Checked OrElse
            chkDisplayDiscArt.Checked OrElse
            chkDisplayFanart.Checked OrElse
            chkDisplayFanartSmall.Checked OrElse
            chkDisplayKeyArt.Checked OrElse
            chkDisplayLandscape.Checked OrElse
            chkDisplayPoster.Checked Then
            chkImagesGlassOverlay.Enabled = True
            chkDisplayImageDimension.Enabled = True
            chkDisplayImageNames.Enabled = True
        Else
            chkImagesGlassOverlay.Enabled = False
            chkDisplayImageDimension.Enabled = False
            chkDisplayImageNames.Enabled = False
        End If
    End Sub

    Private Sub DataGridView_Fill_SortTokens(ByVal List As List(Of String))
        dgvSortTokens.Rows.Clear()
        For Each token In List
            dgvSortTokens.Rows.Add(New Object() {token})
        Next
        dgvSortTokens.ClearSelection()
    End Sub

    Private Sub DigitalGroupSymbolSettings_Click(sender As Object, e As EventArgs) Handles btnDigitGrpSymbolSettings.Click
        Process.Start("INTL.CPL")
    End Sub

    Private Sub ImageFilter_AutoScraper_CheckedChanged(sender As Object, e As EventArgs) Handles chkImageFilterAutoscraper.CheckedChanged
        If chklImageFilterImageDialog.Checked = False AndAlso chkImageFilterAutoscraper.Checked = False Then
            chkImageFilterPoster.Enabled = False
            chkImageFilterFanart.Enabled = False
        Else
            chkImageFilterPoster.Enabled = True
            chkImageFilterFanart.Enabled = True
        End If
        Handle_SettingsChanged()
    End Sub

    Private Sub ImageFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkImageFilter.CheckedChanged
        chkImageFilterAutoscraper.Enabled = chkImageFilter.Checked
        chkImageFilterFanart.Enabled = chkImageFilter.Checked
        chklImageFilterImageDialog.Enabled = chkImageFilter.Checked
        chkImageFilterPoster.Enabled = chkImageFilter.Checked
        lblImageFilterFanartMatchRate.Enabled = chkImageFilter.Checked
        lblImageFilterPosterMatchRate.Enabled = chkImageFilter.Checked
        txtImageFilterFanartMatchRate.Enabled = chkImageFilter.Checked
        txtImageFilterPosterMatchRate.Enabled = chkImageFilter.Checked
        Handle_SettingsChanged()
    End Sub

    Private Sub ImageFilter_Fanart_CheckedChanged(sender As Object, e As EventArgs) Handles chkImageFilterFanart.CheckedChanged
        lblImageFilterFanartMatchRate.Enabled = chkImageFilterFanart.Checked
        txtImageFilterFanartMatchRate.Enabled = chkImageFilterFanart.Checked
        Handle_SettingsChanged()
    End Sub

    Private Sub ImageFilter_FanartMatchRate_TextChanged(sender As Object, e As EventArgs) Handles txtImageFilterFanartMatchRate.TextChanged
        If chkImageFilter.Checked Then
            Dim txtbox As TextBox = CType(sender, TextBox)
            Dim matchfactor As Integer
            Dim bBadValue As Boolean
            If Integer.TryParse(txtbox.Text, matchfactor) Then
                If matchfactor < 0 OrElse matchfactor > 10 Then
                    bBadValue = True
                End If
            Else
                bBadValue = True
            End If
            If bBadValue Then
                txtbox.Text = String.Empty
                MessageBox.Show(Master.eLang.GetString(1460, "Match Tolerance should be between 0 - 10 | 0 = 100% identical images, 10 = different images"),
                                Master.eLang.GetString(356, "Warning"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            End If
        End If
        Handle_SettingsChanged()
    End Sub

    Private Sub ImageFilter_Poster_CheckedChanged(sender As Object, e As EventArgs) Handles chkImageFilterPoster.CheckedChanged
        lblImageFilterPosterMatchRate.Enabled = chkImageFilterPoster.Checked
        txtImageFilterPosterMatchRate.Enabled = chkImageFilterPoster.Checked
        Handle_SettingsChanged()
    End Sub

    Private Sub InterfaceLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbInterfaceLanguage.SelectedIndexChanged
        Cursor.Current = Cursors.WaitCursor
        Master.eLang.LoadAllLanguage(cbInterfaceLanguage.SelectedItem.ToString, True)
        Setup()
        Cursor.Current = Cursors.Default
        Handle_SettingsChanged()
    End Sub

    Private Sub Load_InterfaceLanguages()
        cbInterfaceLanguage.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Langs")) Then
            Dim alL As New List(Of String)
            Dim alLangs As New List(Of String)
            Try
                alL.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Langs"), "*).xml"))
            Catch
            End Try
            alLangs.AddRange(alL.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL)).ToList)
            cbInterfaceLanguage.Items.AddRange(alLangs.ToArray)
        End If
    End Sub

    Private Sub Load_Themes()
        cbTheme.Items.Clear()
        Dim diDefaults As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Themes"))
        If diDefaults.Exists Then cbTheme.Items.AddRange(diDefaults.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)

        Dim diCustom As DirectoryInfo = New DirectoryInfo(Path.Combine(Master.SettingsPath, "Themes"))
        If diCustom.Exists Then cbTheme.Items.AddRange(diCustom.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)
    End Sub

    Private Sub LoadDefaults_SortTokens() Handles btnSortTokensDefaults.Click
        DataGridView_Fill_SortTokens(Master.eSettings.GetDefaultsForList_SortTokens())
        Handle_SettingsChanged()
    End Sub

    Private Sub Save_SortTokens()
        With Master.eSettings.Options.General.SortTokens
            .Clear()
            For Each r As DataGridViewRow In dgvSortTokens.Rows
                If r.Cells(0).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(0).Value.ToString.Trim) Then
                    .Add(r.Cells(0).Value.ToString.Trim)
                End If
            Next
        End With
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles _
        txtImageFilterFanartMatchRate.KeyPress,
        txtImageFilterPosterMatchRate.KeyPress

        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

#End Region 'Methods

End Class