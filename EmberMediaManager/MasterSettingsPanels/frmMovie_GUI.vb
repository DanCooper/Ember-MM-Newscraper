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

Public Class frmMovie_GUI
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
        ChildType = Enums.SettingsPanelType.MovieGUI
        IsEnabled = True
        Image = Nothing
        ImageIndex = 0
        Order = 100
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(335, "GUI")
        ParentType = Enums.SettingsPanelType.Movie
        UniqueId = "Movie_GUI"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        With Master.eLang
            chkClickScrapeEnabled.Text = .GetString(849, "Enable Click-Scrape")
            chkClickScrapeShowResults.Text = .GetString(852, "Show Results Dialog")
            chkLevTolerance.Text = .GetString(462, "Check Title Match Confidence")
            colMediaListSorting_Show.HeaderText = .GetString(465, "Show")
            colMediaListSorting_Column.HeaderText = .GetString(1331, "Column")
            gbCustomMarker.Text = .GetString(1190, "Custom Marker")
            gbMainWindow.Text = .GetString(1152, "Main Window")
            gbMediaList.Text = .GetString(460, "Media List")
            lblCustomMarker1.Text = String.Concat(.GetString(1191, "Custom"), " #1")
            lblCustomMarker2.Text = String.Concat(.GetString(1191, "Custom"), " #2")
            lblCustomMarker3.Text = String.Concat(.GetString(1191, "Custom"), " #3")
            lblCustomMarker4.Text = String.Concat(.GetString(1191, "Custom"), " #4")
            lblLanguageOverlay.Text = String.Concat(.GetString(436, "Display best Audio Stream with the following Language"), ":")
            lblLevTolerance.Text = .GetString(461, "Mismatch Tolerance:")
            lblMediaListSorting.Text = .GetString(456, "Use ALT + UP / DOWN to move the rows")
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
        With Manager.mSettings.Movie.GuiSettings
            .ClickScrapeEnabled = chkClickScrapeEnabled.Checked
            .ClickScrapeShowResults = chkClickScrapeShowResults.Checked
            .CustomScrapeButtonEnabled = rbCustomScrapeButtonEnabled.Checked
            .CustomScrapeButtonModifierType = CType(cbCustomScrapeButtonModifierType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .CustomScrapeButtonScrapeType = CType(cbCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .CustomScrapeButtonSelectionType = CType(cbCustomScrapeButtonSelectionType.SelectedItem, KeyValuePair(Of String, Enums.SelectionType)).Value
            .PreferredAudioLanguage = If(cbLanguageOverlay.Text = Master.eLang.CommonWordsList.Disabled, String.Empty, cbLanguageOverlay.Text)
            Save_MediaListSorting()
        End With
        With Master.eSettings
            .MovieGeneralCustomMarker1Color = btnCustomMarker1.BackColor.ToArgb
            .MovieGeneralCustomMarker2Color = btnCustomMarker2.BackColor.ToArgb
            .MovieGeneralCustomMarker3Color = btnCustomMarker3.BackColor.ToArgb
            .MovieGeneralCustomMarker4Color = btnCustomMarker4.BackColor.ToArgb
            .MovieGeneralCustomMarker1Name = txtCustomMarker1.Text
            .MovieGeneralCustomMarker2Name = txtCustomMarker2.Text
            .MovieGeneralCustomMarker3Name = txtCustomMarker3.Text
            .MovieGeneralCustomMarker4Name = txtCustomMarker4.Text
            .MovieLevTolerance = If(Not String.IsNullOrEmpty(txtLevTolerance.Text), Convert.ToInt32(txtLevTolerance.Text), 0)
        End With
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Private Sub Handle_SettingsChanged() Handles _
        cbCustomScrapeButtonModifierType.SelectedIndexChanged,
        cbCustomScrapeButtonScrapeType.SelectedIndexChanged,
        cbCustomScrapeButtonSelectionType.SelectedIndexChanged,
        cbLanguageOverlay.SelectedIndexChanged,
        chkClickScrapeShowResults.CheckedChanged,
        dgvMediaListSorting.CellValueChanged,
        txtCustomMarker1.TextChanged,
        txtCustomMarker2.TextChanged,
        txtCustomMarker3.TextChanged,
        txtCustomMarker4.TextChanged,
        txtLevTolerance.TextChanged
        RaiseEvent SettingsChanged()
    End Sub

    Public Sub Settings_Load()
        With Manager.mSettings.Movie.GuiSettings
            cbCustomScrapeButtonModifierType.SelectedValue = .CustomScrapeButtonModifierType
            cbCustomScrapeButtonScrapeType.SelectedValue = .CustomScrapeButtonScrapeType
            cbCustomScrapeButtonSelectionType.SelectedValue = .CustomScrapeButtonSelectionType
            cbLanguageOverlay.SelectedItem = If(.PreferredAudioLanguageSpecified, .PreferredAudioLanguage, Master.eLang.CommonWordsList.Disabled)
            chkClickScrapeEnabled.Checked = .ClickScrapeEnabled
            chkClickScrapeShowResults.Checked = .ClickScrapeShowResults
            chkClickScrapeShowResults.Enabled = chkClickScrapeEnabled.Checked
            rbCustomScrapeButtonDisabled.Checked = Not .CustomScrapeButtonEnabled
            rbCustomScrapeButtonEnabled.Checked = .CustomScrapeButtonEnabled
            DataGridView_Fill_MediaListSorting(.MediaListSorting)
        End With
        With Master.eSettings
            btnCustomMarker1.BackColor = Color.FromArgb(.MovieGeneralCustomMarker1Color)
            btnCustomMarker2.BackColor = Color.FromArgb(.MovieGeneralCustomMarker2Color)
            btnCustomMarker3.BackColor = Color.FromArgb(.MovieGeneralCustomMarker3Color)
            btnCustomMarker4.BackColor = Color.FromArgb(.MovieGeneralCustomMarker4Color)
            txtCustomMarker1.Text = .MovieGeneralCustomMarker1Name
            txtCustomMarker2.Text = .MovieGeneralCustomMarker2Name
            txtCustomMarker3.Text = .MovieGeneralCustomMarker3Name
            txtCustomMarker4.Text = .MovieGeneralCustomMarker4Name

            If .MovieLevTolerance > 0 Then
                chkLevTolerance.Checked = True
                txtLevTolerance.Enabled = True
                txtLevTolerance.Text = .MovieLevTolerance.ToString
            End If
        End With
    End Sub

    Private Sub ClickScrape_CheckedChanged() Handles chkClickScrapeEnabled.CheckedChanged
        chkClickScrapeShowResults.Enabled = chkClickScrapeEnabled.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CustomMarker_Click(sender As Object, e As EventArgs) Handles _
        btnCustomMarker1.Click,
        btnCustomMarker2.Click,
        btnCustomMarker3.Click,
        btnCustomMarker4.Click

        With cdColor
            If .ShowDialog = DialogResult.OK Then
                If Not .Color = Nothing Then
                    Select Case True
                        Case sender Is btnCustomMarker1
                            btnCustomMarker1.BackColor = .Color
                        Case sender Is btnCustomMarker2
                            btnCustomMarker2.BackColor = .Color
                        Case sender Is btnCustomMarker3
                            btnCustomMarker3.BackColor = .Color
                        Case sender Is btnCustomMarker4
                            btnCustomMarker4.BackColor = .Color
                    End Select
                    RaiseEvent SettingsChanged()
                End If
            End If
        End With
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

    Private Sub DataGridView_Fill_MediaListSorting(ByVal List As List(Of GuiBase.ListSorting))
        dgvMediaListSorting.Rows.Clear()
        For Each item In List
            Dim currRow As Integer = dgvMediaListSorting.Rows.Add(New Object() {
                                                                  item.DisplayIndex,
                                                                  item.Show,
                                                                  Master.eLang.GetString(item.LabelID, item.LabelText),
                                                                  item.AutoSizeMode
                                                                  })
            dgvMediaListSorting.Rows(currRow).Tag = item
        Next
        dgvMediaListSorting.Sort(dgvMediaListSorting.Columns(0), ComponentModel.ListSortDirection.Ascending)
        dgvMediaListSorting.ClearSelection()
    End Sub

    Private Sub DataGridView_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvMediaListSorting.KeyDown
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

    Private Sub LevTolerance_CheckedChanged() Handles chkLevTolerance.CheckedChanged
        txtLevTolerance.Enabled = chkLevTolerance.Checked
        If Not chkLevTolerance.Checked Then txtLevTolerance.Text = String.Empty
        RaiseEvent SettingsChanged()
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
        DirectCast(dgvMediaListSorting.Columns(3), DataGridViewComboBoxColumn).DataSource = items.ToList
        DirectCast(dgvMediaListSorting.Columns(3), DataGridViewComboBoxColumn).DisplayMember = "Key"
        DirectCast(dgvMediaListSorting.Columns(3), DataGridViewComboBoxColumn).ValueMember = "Value"
    End Sub

    Private Sub Load_CustomScraperButton_ModifierTypes()
        Dim items As New Dictionary(Of String, Enums.ModifierType) From {
            {Master.eLang.GetString(70, "All Items"), Enums.ModifierType.All},
            {Master.eLang.GetString(973, "Actor Thumbs Only"), Enums.ModifierType.MainActorThumbs},
            {Master.eLang.GetString(1060, "Banner Only"), Enums.ModifierType.MainBanner},
            {Master.eLang.GetString(1122, "ClearArt Only"), Enums.ModifierType.MainClearArt},
            {Master.eLang.GetString(1123, "ClearLogo Only"), Enums.ModifierType.MainClearLogo},
            {Master.eLang.GetString(1124, "DiscArt Only"), Enums.ModifierType.MainDiscArt},
            {Master.eLang.GetString(975, "Extrafanarts Only"), Enums.ModifierType.MainExtrafanarts},
            {Master.eLang.GetString(74, "Extrathumbs Only"), Enums.ModifierType.MainExtrathumbs},
            {Master.eLang.GetString(73, "Fanart Only"), Enums.ModifierType.MainFanart},
            {Master.eLang.GetString(303, "KeyArt Only"), Enums.ModifierType.MainKeyArt},
            {Master.eLang.GetString(1061, "Landscape Only"), Enums.ModifierType.MainLandscape},
            {Master.eLang.GetString(76, "Metadata Only"), Enums.ModifierType.MainMetadata},
            {Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO},
            {Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster},
            {Master.eLang.GetString(1125, "Theme Only"), Enums.ModifierType.MainTheme},
            {Master.eLang.GetString(75, "Trailer Only"), Enums.ModifierType.MainTrailer}
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

    Private Sub LoadDefaults_MediaListSorting() Handles btnMediaListSortingDefaults.Click
        DataGridView_Fill_MediaListSorting(Manager.mSettings.GetDefaultsForList_MediaListSorting_Movie())
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Save_MediaListSorting()
        With Manager.mSettings.Movie.GuiSettings.MediaListSorting
            .Clear()
            For Each r As DataGridViewRow In dgvMediaListSorting.Rows
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

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtLevTolerance.KeyPress
        e.Handled = StringUtils.IntegerOnly(e.KeyChar)
    End Sub

#End Region 'Methods

End Class