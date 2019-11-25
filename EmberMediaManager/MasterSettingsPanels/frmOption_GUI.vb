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


Public Class frmOption_GUI
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
            .SettingsPanelID = "Option_GUI",
            .Title = Master.eLang.GetString(38, "General"),
            .Type = Enums.SettingsPanelType.Options
        }
    End Function

    Public Sub SaveSetup() Implements Interfaces.IMasterSettingsPanel.SaveSetup
        With Master.eSettings
            .GeneralCheckUpdates = chkGeneralCheckUpdates.Checked
            .GeneralDateAddedIgnoreNFO = chkGeneralDateAddedIgnoreNFO.Checked
            .GeneralDigitGrpSymbolVotes = chkGeneralDigitGrpSymbolVotes.Checked
            .GeneralDateTime = CType(cbGeneralDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTime)).Value
            .GeneralDoubleClickScrape = chkGeneralDoubleClickScrape.Checked
            .GeneralDisplayBanner = chkGeneralDisplayBanner.Checked
            .GeneralDisplayCharacterArt = chkGeneralDisplayCharacterArt.Checked
            .GeneralDisplayClearArt = chkGeneralDisplayClearArt.Checked
            .GeneralDisplayClearLogo = chkGeneralDisplayClearLogo.Checked
            .GeneralDisplayDiscArt = chkGeneralDisplayDiscArt.Checked
            .GeneralDisplayFanart = chkGeneralDisplayFanart.Checked
            .GeneralDisplayFanartSmall = chkGeneralDisplayFanartSmall.Checked
            .GeneralDisplayKeyArt = chkGeneralDisplayKeyArt.Checked
            .GeneralDisplayLandscape = chkGeneralDisplayLandscape.Checked
            .GeneralDisplayPoster = chkGeneralDisplayPoster.Checked
            .GeneralImagesGlassOverlay = chkGeneralImagesGlassOverlay.Checked
            .GeneralImageFilter = chkGeneralImageFilter.Checked
            .GeneralImageFilterAutoscraper = chkGeneralImageFilterAutoscraper.Checked
            .GeneralImageFilterFanart = chkGeneralImageFilterFanart.Checked
            .GeneralImageFilterImagedialog = chkGeneralImageFilterImagedialog.Checked
            .GeneralImageFilterPoster = chkGeneralImageFilterPoster.Checked
            If Not String.IsNullOrEmpty(txtGeneralImageFilterFanartMatchRate.Text) AndAlso Integer.TryParse(txtGeneralImageFilterFanartMatchRate.Text, 4) Then
                .GeneralImageFilterFanartMatchTolerance = Convert.ToInt32(txtGeneralImageFilterFanartMatchRate.Text)
            Else
                .GeneralImageFilterFanartMatchTolerance = 4
            End If
            If Not String.IsNullOrEmpty(txtGeneralImageFilterPosterMatchRate.Text) AndAlso Integer.TryParse(txtGeneralImageFilterPosterMatchRate.Text, 1) Then
                .GeneralImageFilterPosterMatchTolerance = Convert.ToInt32(txtGeneralImageFilterPosterMatchRate.Text)
            Else
                .GeneralImageFilterPosterMatchTolerance = 1
            End If
            .GeneralLanguage = cbGeneralLanguage.Text
            .GeneralOverwriteNfo = chkGeneralOverwriteNfo.Checked
            .GeneralShowGenresText = chkGeneralDisplayGenresText.Checked
            .GeneralShowLangFlags = chkGeneralDisplayLangFlags.Checked
            .GeneralShowImgDims = chkGeneralDisplayImgDims.Checked
            .GeneralShowImgNames = chkGeneralDisplayImgNames.Checked
            .GeneralSourceFromFolder = chkGeneralSourceFromFolder.Checked
            .GeneralTheme = cbGeneralTheme.Text
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            cbGeneralDateTime.SelectedValue = .GeneralDateTime
            RemoveHandler cbGeneralLanguage.SelectedIndexChanged, AddressOf cbGeneralLanguage_SelectedIndexChanged
            cbGeneralLanguage.SelectedItem = .GeneralLanguage
            AddHandler cbGeneralLanguage.SelectedIndexChanged, AddressOf cbGeneralLanguage_SelectedIndexChanged
            cbGeneralTheme.SelectedItem = .GeneralTheme
            chkGeneralCheckUpdates.Checked = .GeneralCheckUpdates
            chkGeneralDateAddedIgnoreNFO.Checked = .GeneralDateAddedIgnoreNFO
            chkGeneralDigitGrpSymbolVotes.Checked = .GeneralDigitGrpSymbolVotes
            chkGeneralImageFilter.Checked = .GeneralImageFilter
            chkGeneralImageFilterAutoscraper.Checked = .GeneralImageFilterAutoscraper
            txtGeneralImageFilterFanartMatchRate.Enabled = .GeneralImageFilterFanart
            chkGeneralImageFilterFanart.Checked = .GeneralImageFilterFanart
            chkGeneralImageFilterImagedialog.Checked = .GeneralImageFilterImagedialog
            chkGeneralImageFilterPoster.Checked = .GeneralImageFilterPoster
            txtGeneralImageFilterPosterMatchRate.Enabled = .GeneralImageFilterPoster
            chkGeneralDoubleClickScrape.Checked = .GeneralDoubleClickScrape
            chkGeneralDisplayBanner.Checked = .GeneralDisplayBanner
            chkGeneralDisplayCharacterArt.Checked = .GeneralDisplayCharacterArt
            chkGeneralDisplayClearArt.Checked = .GeneralDisplayClearArt
            chkGeneralDisplayClearLogo.Checked = .GeneralDisplayClearLogo
            chkGeneralDisplayDiscArt.Checked = .GeneralDisplayDiscArt
            chkGeneralDisplayFanart.Checked = .GeneralDisplayFanart
            chkGeneralDisplayFanartSmall.Checked = .GeneralDisplayFanartSmall
            chkGeneralDisplayKeyArt.Checked = .GeneralDisplayKeyArt
            chkGeneralDisplayLandscape.Checked = .GeneralDisplayLandscape
            chkGeneralDisplayPoster.Checked = .GeneralDisplayPoster
            chkGeneralImagesGlassOverlay.Checked = .GeneralImagesGlassOverlay
            chkGeneralOverwriteNfo.Checked = .GeneralOverwriteNfo
            chkGeneralDisplayGenresText.Checked = .GeneralShowGenresText
            chkGeneralDisplayLangFlags.Checked = .GeneralShowLangFlags
            chkGeneralDisplayImgDims.Checked = .GeneralShowImgDims
            chkGeneralDisplayImgNames.Checked = .GeneralShowImgNames
            chkGeneralSourceFromFolder.Checked = .GeneralSourceFromFolder
            txtGeneralImageFilterPosterMatchRate.Text = .GeneralImageFilterPosterMatchTolerance.ToString
            txtGeneralImageFilterFanartMatchRate.Text = .GeneralImageFilterFanartMatchTolerance.ToString
        End With
    End Sub

    Private Sub Setup()
        gbGeneralMainWindowOpts.Text = Master.eLang.GetString(1152, "Main Window")
        gbGeneralMiscOpts.Text = Master.eLang.GetString(429, "Miscellaneous")
        btnGeneralDigitGrpSymbolSettings.Text = Master.eLang.GetString(420, "Settings")
        chkGeneralCheckUpdates.Text = Master.eLang.GetString(432, "Check for Updates")
        chkGeneralDateAddedIgnoreNFO.Text = Master.eLang.GetString(1209, "Ignore <dateadded> from NFO")
        chkGeneralDigitGrpSymbolVotes.Text = Master.eLang.GetString(1387, "Use digit grouping symbol for Votes count")
        chkGeneralDoubleClickScrape.Text = Master.eLang.GetString(1198, "Enable Image Scrape On Double Right Click")
        chkGeneralDisplayBanner.Text = Master.eLang.GetString(1146, "Display Banner")
        chkGeneralDisplayCharacterArt.Text = Master.eLang.GetString(1147, "Display CharacterArt")
        chkGeneralDisplayClearArt.Text = Master.eLang.GetString(1148, "Display ClearArt")
        chkGeneralDisplayClearLogo.Text = Master.eLang.GetString(1149, "Display ClearLogo")
        chkGeneralDisplayDiscArt.Text = Master.eLang.GetString(1150, "Display DiscArt")
        chkGeneralDisplayFanart.Text = Master.eLang.GetString(455, "Display Fanart")
        chkGeneralDisplayFanartSmall.Text = Master.eLang.GetString(967, "Display Small Fanart")
        chkGeneralDisplayLandscape.Text = Master.eLang.GetString(1151, "Display Landscape")
        chkGeneralDisplayPoster.Text = Master.eLang.GetString(456, "Display Poster")
        chkGeneralImagesGlassOverlay.Text = Master.eLang.GetString(966, "Enable Images Glass Overlay")
        chkGeneralImageFilter.Text = Master.eLang.GetString(1459, "Activate ImageFilter to avoid duplicate images")
        chkGeneralImageFilterAutoscraper.Text = Master.eLang.GetString(1457, "Autoscraper")
        chkGeneralImageFilterFanart.Text = Master.eLang.GetString(149, "Fanart")
        chkGeneralImageFilterImagedialog.Text = Master.eLang.GetString(1458, "Imagedialog")
        chkGeneralImageFilterPoster.Text = Master.eLang.GetString(148, "Poster")
        chkGeneralOverwriteNfo.Text = Master.eLang.GetString(433, "Overwrite Non-conforming nfos")
        chkGeneralDisplayGenresText.Text = Master.eLang.GetString(453, "Always Display Genre Text")
        chkGeneralDisplayLangFlags.Text = Master.eLang.GetString(489, "Display Language Flags")
        chkGeneralDisplayImgDims.Text = Master.eLang.GetString(457, "Display Image Dimensions")
        chkGeneralDisplayImgNames.Text = Master.eLang.GetString(1255, "Display Image Names")
        chkGeneralSourceFromFolder.Text = Master.eLang.GetString(711, "Include Folder Name in Source Type Check")
        gbGeneralDateAdded.Text = Master.eLang.GetString(792, "Adding Date")
        gbGeneralInterface.Text = Master.eLang.GetString(795, "Interface")
        lblGeneralImageFilterPosterMatchRate.Text = Master.eLang.GetString(148, "Poster") & " " & Master.eLang.GetString(461, "Mismatch Tolerance:")
        lblGeneralImageFilterFanartMatchRate.Text = Master.eLang.GetString(149, "Fanart") & " " & Master.eLang.GetString(461, "Mismatch Tolerance:")
        lblGeneralOverwriteNfo.Text = Master.eLang.GetString(434, "(If unchecked, non-conforming nfos will be renamed to <filename>.info)")
        lblGeneralIntLang.Text = Master.eLang.GetString(430, "Interface Language:")
        lblGeneralTheme.Text = String.Concat(Master.eLang.GetString(620, "Theme"), ":")

        LoadGeneralDateTime()
        LoadIntLangs()
        LoadThemes()
    End Sub

    Private Sub cbGeneralLanguage_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Cursor.Current = Cursors.WaitCursor
        Master.eLang.LoadAllLanguage(cbGeneralLanguage.SelectedItem.ToString, True)
        SetUp()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub CheckHideSettings()
        If chkGeneralDisplayBanner.Checked OrElse chkGeneralDisplayCharacterArt.Checked OrElse chkGeneralDisplayClearArt.Checked OrElse chkGeneralDisplayClearLogo.Checked OrElse
              chkGeneralDisplayDiscArt.Checked OrElse chkGeneralDisplayFanart.Checked OrElse chkGeneralDisplayFanartSmall.Checked OrElse chkGeneralDisplayLandscape.Checked OrElse chkGeneralDisplayPoster.Checked Then
            chkGeneralImagesGlassOverlay.Enabled = True
            chkGeneralDisplayImgDims.Enabled = True
            chkGeneralDisplayImgNames.Enabled = True
        Else
            chkGeneralImagesGlassOverlay.Enabled = False
            chkGeneralDisplayImgDims.Enabled = False
            chkGeneralDisplayImgNames.Enabled = False
        End If
    End Sub

    Private Sub chkGeneralDisplayBanner_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayCharacterArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayClearArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayClearLogo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayDiscArt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayFanart_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayLandscape_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayPoster_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub chkGeneralDisplayFanartSmall_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Handle_SettingsChanged()
        CheckHideSettings()
    End Sub

    Private Sub LoadIntLangs()
        cbGeneralLanguage.Items.Clear()
        If Directory.Exists(Path.Combine(Functions.AppPath, "Langs")) Then
            Dim alL As New List(Of String)
            Dim alLangs As New List(Of String)
            Try
                alL.AddRange(Directory.GetFiles(Path.Combine(Functions.AppPath, "Langs"), "*).xml"))
            Catch
            End Try
            alLangs.AddRange(alL.Cast(Of String)().Select(Function(AL) Path.GetFileNameWithoutExtension(AL)).ToList)
            cbGeneralLanguage.Items.AddRange(alLangs.ToArray)
        End If
    End Sub

    Private Sub LoadThemes()
        cbGeneralTheme.Items.Clear()
        Dim diDefaults As DirectoryInfo = New DirectoryInfo(Path.Combine(Functions.AppPath, "Themes"))
        If diDefaults.Exists Then cbGeneralTheme.Items.AddRange(diDefaults.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)

        Dim diCustom As DirectoryInfo = New DirectoryInfo(Path.Combine(Master.SettingsPath, "Themes"))
        If diCustom.Exists Then cbGeneralTheme.Items.AddRange(diCustom.GetFiles("*.xml").Cast(Of FileInfo)().Select(Function(f) Path.GetFileNameWithoutExtension(f.Name)).ToArray)
    End Sub

    Private Sub LoadGeneralDateTime()
        Dim items As New Dictionary(Of String, Enums.DateTime)
        items.Add(Master.eLang.GetString(1210, "Current DateTime when adding"), Enums.DateTime.Now)
        items.Add(Master.eLang.GetString(1227, "ctime (fallback to mtime)"), Enums.DateTime.ctime)
        items.Add(Master.eLang.GetString(1211, "mtime (fallback to ctime)"), Enums.DateTime.mtime)
        items.Add(Master.eLang.GetString(1212, "Newer of mtime and ctime"), Enums.DateTime.Newer)
        cbGeneralDateTime.DataSource = items.ToList
        cbGeneralDateTime.DisplayMember = "Key"
        cbGeneralDateTime.ValueMember = "Value"
    End Sub

    Private Sub btnGeneralDigitGrpSymbolSettings_Click(sender As Object, e As EventArgs)
        Process.Start("INTL.CPL")
    End Sub

    Private Sub chkGeneralImageFilter_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()
        chkGeneralImageFilterAutoscraper.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterFanart.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterImagedialog.Enabled = chkGeneralImageFilter.Checked
        chkGeneralImageFilterPoster.Enabled = chkGeneralImageFilter.Checked
        lblGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilter.Checked
        lblGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilter.Checked
        txtGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilter.Checked
        txtGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilter.Checked
    End Sub

    Private Sub chkGeneralImageFilterAutoscraperImagedialog_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()
        If chkGeneralImageFilterImagedialog.Checked = False AndAlso chkGeneralImageFilterAutoscraper.Checked = False Then
            chkGeneralImageFilterPoster.Enabled = False
            chkGeneralImageFilterFanart.Enabled = False
        Else
            chkGeneralImageFilterPoster.Enabled = True
            chkGeneralImageFilterFanart.Enabled = True
        End If
    End Sub
    Private Sub chkGeneralImageFilterPoster_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()
        lblGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilterPoster.Checked
        txtGeneralImageFilterPosterMatchRate.Enabled = chkGeneralImageFilterPoster.Checked
    End Sub
    Private Sub chkGeneralImageFilterFanart_CheckedChanged(sender As Object, e As EventArgs)
        Handle_SettingsChanged()
        lblGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilterFanart.Checked
        txtGeneralImageFilterFanartMatchRate.Enabled = chkGeneralImageFilterFanart.Checked
    End Sub

    Private Sub txtGeneralImageFilterMatchRate_TextChanged(sender As Object, e As EventArgs)
        If chkGeneralImageFilter.Checked Then
            Dim txtbox As TextBox = CType(sender, TextBox)
            Dim matchfactor As Integer = 0
            Dim NotGood As Boolean = False
            If Integer.TryParse(txtbox.Text, matchfactor) Then
                If matchfactor < 0 OrElse matchfactor > 10 Then
                    NotGood = True
                End If
            Else
                NotGood = True
            End If
            If NotGood Then
                txtbox.Text = String.Empty
                MessageBox.Show(Master.eLang.GetString(1460, "Match Tolerance should be between 0 - 10 | 0 = 100% identical images, 10= different images"), Master.eLang.GetString(356, "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

#End Region 'Methods

End Class