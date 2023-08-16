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

Public Class frmTV_GUI
    Implements Interfaces.ISettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private TVGeneralEpisodeListSorting As New List(Of GuiBase.ListSorting)
    Private TVGeneralSeasonListSorting As New List(Of GuiBase.ListSorting)
    Private TVGeneralShowListSorting As New List(Of GuiBase.ListSorting)

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
        ChildType = Enums.SettingsPanelType.TVGUI
        IsEnabled = True
        Image = Nothing
        ImageIndex = 0
        Order = 100
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(335, "GUI")
        ParentType = Enums.SettingsPanelType.TV
        UniqueId = "TV_GUI"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        With Master.eLang
            'Column
            Dim strColumn As String = .GetString(1331, "Column")
            colMediaListSorting_Column_TVEpisode.HeaderText = strColumn
            colMediaListSorting_Column_TVSeason.HeaderText = strColumn
            colMediaListSorting_Column_TVShow.HeaderText = strColumn

            'Show
            Dim strShow As String = .GetString(465, "Show")
            colMediaListSorting_Show_TVEpisode.HeaderText = strShow
            colMediaListSorting_Show_TVSeason.HeaderText = strShow
            colMediaListSorting_Show_TVShow.HeaderText = strShow

            'Use ALT + UP / DOWN to move the rows
            Dim strUseArrows As String = .GetString(456, "Use ALT + UP / DOWN to move the rows")
            lblMediaListSorting_TVEpisode.Text = strUseArrows
            lblMediaListSorting_TVSeason.Text = strUseArrows
            lblMediaListSorting_TVShow.Text = strUseArrows

            chkClickScrapeEnabled_TVShow.Text = .GetString(849, "Enable Click-Scrape")
            chkClickScrapeShowResults_TVShow.Text = .GetString(852, "Show Results Dialog")
            chkDisplayMissingEpisodes.Text = .GetString(733, "Display Missing Episodes")
            gbMainWindow.Text = .GetString(1152, "Main Window")
            gbMediaList.Text = .GetString(460, "Media List")
            gbMediaListSorting_TVEpisode.Text = .GetString(682, "Episodes")
            gbMediaListSorting_TVSeason.Text = .GetString(681, "Seasons")
            gbMediaListSorting_TVShow.Text = .GetString(653, "TV Shows")
            lblLanguageOverlay.Text = String.Concat(.GetString(436, "Display best Audio Stream with the following Language"), ":")
        End With

        Load_AutoSizeModes()
        Load_CustomScraperButton_ModifierTypes()
        Load_CustomScraperButton_ScrapeTypes()
        Load_CustomScraperButton_SelectionTypes()
        Load_Languages()
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
        With Manager.mSettings.TVEpisode.GuiSettings
            .ClickScrapeEnabled = chkClickScrapeEnabled_TVEpisode.Checked
            .ClickScrapeShowResults = chkClickScrapeShowResults_TVEpisode.Checked
            .PreferredAudioLanguage = If(cbLanguageOverlay.Text = Master.eLang.CommonWordsList.Disabled, String.Empty, cbLanguageOverlay.Text)
            .DisplayMissingElements = chkDisplayMissingEpisodes.Checked
            Save_MediaListSorting(Enums.ContentType.TVEpisode)
        End With
        With Manager.mSettings.TVSeason.GuiSettings
            .ClickScrapeEnabled = chkClickScrapeEnabled_TVSeason.Checked
            .ClickScrapeShowResults = chkClickScrapeShowResults_TVSeason.Checked
            Save_MediaListSorting(Enums.ContentType.TVSeason)
        End With
        With Manager.mSettings.TVShow.GuiSettings
            .ClickScrapeEnabled = chkClickScrapeEnabled_TVShow.Checked
            .ClickScrapeShowResults = chkClickScrapeShowResults_TVShow.Checked
            .CustomScrapeButtonEnabled = rbCustomScrapeButtonEnabled.Checked
            .CustomScrapeButtonModifierType = CType(cbCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .CustomScrapeButtonScrapeType = CType(cbCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .CustomScrapeButtonSelectionType = CType(cbCustomScrapeButtonSelectionType.SelectedItem, KeyValuePair(Of String, Enums.SelectionType)).Value
            Save_MediaListSorting(Enums.ContentType.TVShow)
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Manager.mSettings.TVEpisode.GuiSettings
            cbLanguageOverlay.SelectedItem = If(.PreferredAudioLanguageSpecified, .PreferredAudioLanguage, Master.eLang.CommonWordsList.Disabled)
            chkClickScrapeEnabled_TVEpisode.Checked = .ClickScrapeEnabled
            chkClickScrapeShowResults_TVEpisode.Checked = .ClickScrapeShowResults
            chkClickScrapeShowResults_TVEpisode.Enabled = chkClickScrapeEnabled_TVShow.Checked
            chkDisplayMissingEpisodes.Checked = .DisplayMissingElements
            DataGridView_Fill_MediaListSorting(.MediaListSorting, Enums.ContentType.TVEpisode)
        End With
        With Manager.mSettings.TVSeason.GuiSettings
            chkClickScrapeEnabled_TVSeason.Checked = .ClickScrapeEnabled
            chkClickScrapeShowResults_TVSeason.Checked = .ClickScrapeShowResults
            chkClickScrapeShowResults_TVSeason.Enabled = chkClickScrapeEnabled_TVShow.Checked
            DataGridView_Fill_MediaListSorting(.MediaListSorting, Enums.ContentType.TVSeason)
        End With
        With Manager.mSettings.TVShow.GuiSettings
            cbCustomScrapeButtonModifierType.SelectedValue = .CustomScrapeButtonModifierType
            cbCustomScrapeButtonScrapeType.SelectedValue = .CustomScrapeButtonScrapeType
            cbCustomScrapeButtonSelectionType.SelectedValue = .CustomScrapeButtonSelectionType
            chkClickScrapeEnabled_TVShow.Checked = .ClickScrapeEnabled
            chkClickScrapeShowResults_TVShow.Checked = .ClickScrapeShowResults
            chkClickScrapeShowResults_TVShow.Enabled = chkClickScrapeEnabled_TVShow.Checked
            rbCustomScrapeButtonDisabled.Checked = Not .CustomScrapeButtonEnabled
            rbCustomScrapeButtonEnabled.Checked = .CustomScrapeButtonEnabled
            DataGridView_Fill_MediaListSorting(.MediaListSorting, Enums.ContentType.TVShow)
        End With
    End Sub

    Private Sub Enable_ApplyButton() Handles _
        cbCustomScrapeButtonModifierType.SelectedIndexChanged,
        cbCustomScrapeButtonScrapeType.SelectedIndexChanged,
        cbCustomScrapeButtonSelectionType.SelectedIndexChanged,
        cbLanguageOverlay.SelectedIndexChanged,
        chkClickScrapeShowResults_TVEpisode.CheckedChanged,
        chkClickScrapeShowResults_TVSeason.CheckedChanged,
        chkClickScrapeShowResults_TVShow.CheckedChanged,
        dgvMediaListSorting_TVEpisode.CellValueChanged,
        dgvMediaListSorting_TVSeason.CellValueChanged,
        dgvMediaListSorting_TVShow.CellValueChanged

        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ClickScrape_CheckedChanged_TVEpisode() Handles chkClickScrapeEnabled_TVEpisode.CheckedChanged
        chkClickScrapeShowResults_TVEpisode.Enabled = chkClickScrapeEnabled_TVEpisode.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ClickScrape_CheckedChanged_TVSeason() Handles chkClickScrapeEnabled_TVSeason.CheckedChanged
        chkClickScrapeShowResults_TVSeason.Enabled = chkClickScrapeEnabled_TVSeason.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub ClickScrape_CheckedChanged_TVShow() Handles chkClickScrapeEnabled_TVShow.CheckedChanged
        chkClickScrapeShowResults_TVShow.Enabled = chkClickScrapeEnabled_TVShow.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CustomScrapeButtonDisabled_CheckedChanged() Handles rbCustomScrapeButtonDisabled.CheckedChanged
        If rbCustomScrapeButtonDisabled.Checked Then
            cbCustomScrapeButtonModifierType.Enabled = False
            cbCustomScrapeButtonScrapeType.Enabled = False
            cbCustomScrapeButtonSelectionType.Enabled = False
            lblCustomScrapeButtonModifierType.Enabled = False
            lblCustomScrapeButtonScrapeType.Enabled = False
            lblCustomScrapeButtonSelectionType.Enabled = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CustomScrapeButtonEnabled_CheckedChanged() Handles rbCustomScrapeButtonEnabled.CheckedChanged
        If rbCustomScrapeButtonEnabled.Checked Then
            cbCustomScrapeButtonModifierType.Enabled = True
            cbCustomScrapeButtonScrapeType.Enabled = True
            cbCustomScrapeButtonSelectionType.Enabled = True
            lblCustomScrapeButtonModifierType.Enabled = True
            lblCustomScrapeButtonScrapeType.Enabled = True
            lblCustomScrapeButtonSelectionType.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_Fill_MediaListSorting(ByVal List As List(Of GuiBase.ListSorting), ByVal ContentType As Enums.ContentType)
        Dim dgvList As DataGridView = Nothing
        Select Case ContentType
            Case Enums.ContentType.TVEpisode
                dgvList = dgvMediaListSorting_TVEpisode
            Case Enums.ContentType.TVSeason
                dgvList = dgvMediaListSorting_TVSeason
            Case Enums.ContentType.TVShow
                dgvList = dgvMediaListSorting_TVShow
        End Select

        If dgvList IsNot Nothing Then
            dgvList.Rows.Clear()
            For Each item In List
                Dim currRow As Integer = dgvList.Rows.Add(New Object() {
                                                          item.DisplayIndex,
                                                          item.Show,
                                                          Master.eLang.GetString(item.LabelID, item.LabelText),
                                                          item.AutoSizeMode
                                                          })
                dgvList.Rows(currRow).Tag = item
            Next
            dgvList.Sort(dgvList.Columns(0), ComponentModel.ListSortDirection.Ascending)
            dgvList.ClearSelection()
        End If
    End Sub

    Private Sub DataGridView_MediaListSorting_KeyDown(sender As Object, e As KeyEventArgs) Handles _
        dgvMediaListSorting_TVEpisode.KeyDown,
        dgvMediaListSorting_TVSeason.KeyDown,
        dgvMediaListSorting_TVShow.KeyDown

        Dim dgvList As DataGridView = DirectCast(sender, DataGridView)
        Dim currRowIndex As Integer = dgvList.CurrentRow.Index
        Select Case True
            Case e.Alt And e.KeyCode = Keys.Up AndAlso Not currRowIndex = 0
                dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) - 1
                dgvList.Rows(currRowIndex - 1).Cells(0).Value = currRowIndex
                e.Handled = True
            Case e.Alt And e.KeyCode = Keys.Down AndAlso Not currRowIndex = dgvList.Rows.Count - 1
                dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) + 1
                dgvList.Rows(currRowIndex + 1).Cells(0).Value = currRowIndex
                e.Handled = True
        End Select
        dgvList.Sort(dgvList.Columns(0), ComponentModel.ListSortDirection.Ascending)
    End Sub

    Private Sub Load_AutoSizeModes()
        Dim items As New Dictionary(Of String, DataGridViewAutoSizeColumnMode) From {
            {"AllCells", DataGridViewAutoSizeColumnMode.AllCells},
            {"AllCellsExceptHeader", DataGridViewAutoSizeColumnMode.AllCellsExceptHeader},
            {"ColumnHeader", DataGridViewAutoSizeColumnMode.ColumnHeader},
            {"DisplayedCells", DataGridViewAutoSizeColumnMode.DisplayedCells},
            {"DisplayedCellsExceptHeader", DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader},
            {"Fill", DataGridViewAutoSizeColumnMode.Fill},
            {"None", DataGridViewAutoSizeColumnMode.None},
            {"NotSet", DataGridViewAutoSizeColumnMode.NotSet}
        }
        DirectCast(dgvMediaListSorting_TVEpisode.Columns(3), DataGridViewComboBoxColumn).DataSource = items.ToList
        DirectCast(dgvMediaListSorting_TVEpisode.Columns(3), DataGridViewComboBoxColumn).DisplayMember = "Key"
        DirectCast(dgvMediaListSorting_TVEpisode.Columns(3), DataGridViewComboBoxColumn).ValueMember = "Value"
        DirectCast(dgvMediaListSorting_TVSeason.Columns(3), DataGridViewComboBoxColumn).DataSource = items.ToList
        DirectCast(dgvMediaListSorting_TVSeason.Columns(3), DataGridViewComboBoxColumn).DisplayMember = "Key"
        DirectCast(dgvMediaListSorting_TVSeason.Columns(3), DataGridViewComboBoxColumn).ValueMember = "Value"
        DirectCast(dgvMediaListSorting_TVShow.Columns(3), DataGridViewComboBoxColumn).DataSource = items.ToList
        DirectCast(dgvMediaListSorting_TVShow.Columns(3), DataGridViewComboBoxColumn).DisplayMember = "Key"
        DirectCast(dgvMediaListSorting_TVShow.Columns(3), DataGridViewComboBoxColumn).ValueMember = "Value"
    End Sub

    Private Sub Load_CustomScraperButton_ModifierTypes()
        Dim items As New Dictionary(Of String, Enums.ModifierType) From {
            {Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All},
            {Master.eLang.GetString(973, "Actor Thumbs Only"), Enums.ModifierType.MainActorThumbs},
            {Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner},
            {Master.eLang.GetString(1121, "CharacterArt Only"), Enums.ModifierType.MainCharacterArt},
            {Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt},
            {Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo},
            {Master.eLang.GetString(975, "Extrafanarts Only"), Enums.ModifierType.MainExtrafanarts},
            {Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart},
            {Master.eLang.GetString(303, "KeyArt Only"), Enums.ModifierType.MainKeyArt},
            {Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape},
            {Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO},
            {Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster},
            {Master.eLang.GetString(1125, "Theme Only"), Enums.ModifierType.MainTheme}
        }
        cbCustomScrapeButtonModifierType.DataSource = items.ToList
        cbCustomScrapeButtonModifierType.DisplayMember = "Key"
        cbCustomScrapeButtonModifierType.ValueMember = "Value"
    End Sub

    Private Sub Load_CustomScraperButton_ScrapeTypes()

        Dim strAsk As String = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        Dim strAuto As String = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        Dim strSkip As String = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")

        Dim items As New Dictionary(Of String, Enums.ScrapeType) From {
            {String.Concat(strAuto), Enums.ScrapeType.Auto},
            {String.Concat(strAsk), Enums.ScrapeType.Ask},
            {String.Concat(strSkip), Enums.ScrapeType.Skip}
        }
        cbCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbCustomScrapeButtonScrapeType.ValueMember = "Value"
    End Sub

    Private Sub Load_CustomScraperButton_SelectionTypes()
        Dim strAll As String = Master.eLang.GetString(68, "All")
        Dim strFilter As String = Master.eLang.GetString(624, "Current Filter")
        Dim strMarked As String = Master.eLang.GetString(48, "Marked")
        Dim strMissing As String = Master.eLang.GetString(40, "Missing Items")
        Dim strNew As String = Master.eLang.GetString(47, "New")

        Dim items As New Dictionary(Of String, Enums.SelectionType) From {
            {String.Concat(strAll), Enums.SelectionType.All},
            {String.Concat(strMissing), Enums.SelectionType.MissingContent},
            {String.Concat(strNew), Enums.SelectionType.[New]},
            {String.Concat(strMarked), Enums.SelectionType.Marked},
            {String.Concat(strFilter), Enums.SelectionType.Filtered}
        }
        cbCustomScrapeButtonSelectionType.DataSource = items.ToList
        cbCustomScrapeButtonSelectionType.DisplayMember = "Key"
        cbCustomScrapeButtonSelectionType.ValueMember = "Value"
    End Sub

    Private Sub Load_Languages()
        cbLanguageOverlay.Items.Add(Master.eLang.CommonWordsList.Disabled)
        cbLanguageOverlay.Items.AddRange(Localization.Languages.Get_Languages_List.ToArray)
    End Sub

    Private Sub LoadDefaults_MediaListSorting(sender As Object, e As EventArgs) Handles _
        btnMediaListSortingDefaults_TVEpisode.Click,
        btnMediaListSortingDefaults_TVSeason.Click,
        btnMediaListSortingDefaults_TVShow.Click

        Dim eContentType As Enums.ContentType
        Dim lstDefaultListSorting As List(Of GuiBase.ListSorting)
        Select Case True
            Case sender Is btnMediaListSortingDefaults_TVEpisode
                eContentType = Enums.ContentType.TVEpisode
                lstDefaultListSorting = Manager.mSettings.GetDefaultsForList_MediaListSorting_TVEpisode
            Case sender Is btnMediaListSortingDefaults_TVSeason
                eContentType = Enums.ContentType.TVSeason
                lstDefaultListSorting = Manager.mSettings.GetDefaultsForList_MediaListSorting_TVSeason
            Case sender Is btnMediaListSortingDefaults_TVShow
                eContentType = Enums.ContentType.TVShow
                lstDefaultListSorting = Manager.mSettings.GetDefaultsForList_MediaListSorting_TVShow
            Case Else
                Return
        End Select

        DataGridView_Fill_MediaListSorting(lstDefaultListSorting, eContentType)
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Save_MediaListSorting(ByVal ContentType As Enums.ContentType)
        Dim currDGV As DataGridView
        Dim lstListSorting As List(Of GuiBase.ListSorting)

        Select Case ContentType
            Case Enums.ContentType.TVEpisode
                currDGV = dgvMediaListSorting_TVEpisode
                lstListSorting = Manager.mSettings.TVEpisode.GuiSettings.MediaListSorting
            Case Enums.ContentType.TVSeason
                currDGV = dgvMediaListSorting_TVSeason
                lstListSorting = Manager.mSettings.TVSeason.GuiSettings.MediaListSorting
            Case Enums.ContentType.TVShow
                currDGV = dgvMediaListSorting_TVShow
                lstListSorting = Manager.mSettings.TVShow.GuiSettings.MediaListSorting
            Case Else
                Return
        End Select
        With lstListSorting
            .Clear()
            For Each r As DataGridViewRow In currDGV.Rows
                .Add(New GuiBase.ListSorting With {
                     .AutoSizeMode = DirectCast(r.Cells(3).Value, DataGridViewAutoSizeColumnMode),
                     .Column = DirectCast(r.Tag, GuiBase.ListSorting).Column,
                     .DisplayIndex = DirectCast(r.Cells(0).Value, Integer),
                     .LabelID = DirectCast(r.Tag, GuiBase.ListSorting).LabelID,
                     .LabelText = DirectCast(r.Tag, GuiBase.ListSorting).LabelText,
                     .Show = DirectCast(r.Cells(1).Value, Boolean)
                     })
            Next
        End With
    End Sub

    Private Sub Enable_ApplyButton(sender As Object, e As EventArgs)

    End Sub

#End Region 'Methods

End Class