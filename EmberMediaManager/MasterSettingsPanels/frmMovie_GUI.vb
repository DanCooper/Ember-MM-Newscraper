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

Public Class frmMovie_GUI
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
            .Contains = Enums.SettingsPanelType.MovieGUI,
            .ImageIndex = 0,
            .Order = 100,
            .Panel = pnlSettings,
            .SettingsPanelID = "Movie_GUI",
            .Title = Master.eLang.GetString(335, "GUI"),
            .Type = Enums.SettingsPanelType.Movie
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Manager.mSettings.Movie.GuiSettings
            .ClickScrapeEnabled = chkClickScrapeEnabled.Checked
            .ClickScrapeShowResults = chkClickScrapeShowResults.Checked
            .CustomScrapeButtonEnabled = rbCustomScrapeButtonEnabled.Checked
            .CustomScrapeButtonModifierType = CType(cbCustomScrapeButtonType.SelectedItem, KeyValuePair(Of String, Enums.ModifierType)).Value
            .CustomScrapeButtonScrapeType = CType(cbCustomScrapeButtonScrapeType.SelectedItem, KeyValuePair(Of String, Enums.ScrapeType)).Value
            .PreferredAudioLanguage = If(cbLanguageOverlay.Text = Master.eLang.Disabled, String.Empty, cbLanguageOverlay.Text)
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

    Public Sub Settings_Load()
        With Manager.mSettings.Movie.GuiSettings
            cbCustomScrapeButtonScrapeType.SelectedValue = .CustomScrapeButtonScrapeType
            cbCustomScrapeButtonType.SelectedValue = .CustomScrapeButtonModifierType
            cbLanguageOverlay.SelectedItem = If(.PreferredAudioLanguageSpecified, .PreferredAudioLanguage, Master.eLang.Disabled)
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
        Load_Languages()
    End Sub

    Private Sub Enable_ApplyButton() Handles _
        cbCustomScrapeButtonScrapeType.SelectedIndexChanged,
        cbCustomScrapeButtonType.SelectedIndexChanged,
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
            cbCustomScrapeButtonType.Enabled = False
            cbCustomScrapeButtonScrapeType.Enabled = False
            txtCustomScrapeButtonModifierType.Enabled = False
            txtCustomScrapeButtonScrapeType.Enabled = False
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub CustomScrapeButtonEnabled_CheckedChanged() Handles rbCustomScrapeButtonEnabled.CheckedChanged
        If rbCustomScrapeButtonEnabled.Checked Then
            cbCustomScrapeButtonType.Enabled = True
            cbCustomScrapeButtonScrapeType.Enabled = True
            txtCustomScrapeButtonModifierType.Enabled = True
            txtCustomScrapeButtonScrapeType.Enabled = True
        End If
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub DataGridView_Fill_MediaListSorting(ByVal List As List(Of GuiSettings.ListSorting))
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

    Private Sub DataGridView_MediaListSorting_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvMediaListSorting.KeyDown
        Dim dgvList As DataGridView = DirectCast(sender, DataGridView)
        Dim currRowIndex As Integer = dgvList.CurrentRow.Index
        Select Case True
            Case e.Alt And e.KeyCode = Keys.Down AndAlso Not currRowIndex = dgvList.Rows.Count - 1
                dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) + 1
                dgvList.Rows(currRowIndex + 1).Cells(0).Value = currRowIndex
                e.Handled = True
            Case e.Alt And e.KeyCode = Keys.Up AndAlso Not currRowIndex = 0
                dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) - 1
                dgvList.Rows(currRowIndex - 1).Cells(0).Value = currRowIndex
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
            {Master.eLang.GetString(76, "Meta Data Only"), Enums.ModifierType.MainMeta},
            {Master.eLang.GetString(71, "NFO Only"), Enums.ModifierType.MainNFO},
            {Master.eLang.GetString(72, "Poster Only"), Enums.ModifierType.MainPoster},
            {Master.eLang.GetString(1125, "Theme Only"), Enums.ModifierType.MainTheme},
            {Master.eLang.GetString(75, "Trailer Only"), Enums.ModifierType.MainTrailer}
        }
        cbCustomScrapeButtonType.DataSource = items.ToList
        cbCustomScrapeButtonType.DisplayMember = "Key"
        cbCustomScrapeButtonType.ValueMember = "Value"
    End Sub

    Private Sub Load_CustomScraperButton_ScrapeTypes()
        Dim strAll As String = Master.eLang.GetString(68, "All")
        Dim strFilter As String = Master.eLang.GetString(624, "Current Filter")
        Dim strMarked As String = Master.eLang.GetString(48, "Marked")
        Dim strMissing As String = Master.eLang.GetString(40, "Missing Items")
        Dim strNew As String = Master.eLang.GetString(47, "New")

        Dim strAsk As String = Master.eLang.GetString(77, "Ask (Require Input If No Exact Match)")
        Dim strAuto As String = Master.eLang.GetString(69, "Automatic (Force Best Match)")
        Dim strSkip As String = Master.eLang.GetString(1041, "Skip (Skip If More Than One Match)")

        Dim items As New Dictionary(Of String, Enums.ScrapeType) From {
            {String.Concat(strAll, " - ", strAuto), Enums.ScrapeType.AllAuto},
            {String.Concat(strAll, " - ", strAsk), Enums.ScrapeType.AllAsk},
            {String.Concat(strAll, " - ", strSkip), Enums.ScrapeType.AllSkip},
            {String.Concat(strMissing, " - ", strAuto), Enums.ScrapeType.MissingAuto},
            {String.Concat(strMissing, " - ", strAsk), Enums.ScrapeType.MissingAsk},
            {String.Concat(strMissing, " - ", strSkip), Enums.ScrapeType.MissingSkip},
            {String.Concat(strNew, " - ", strAuto), Enums.ScrapeType.NewAuto},
            {String.Concat(strNew, " - ", strAsk), Enums.ScrapeType.NewAsk},
            {String.Concat(strNew, " - ", strSkip), Enums.ScrapeType.NewSkip},
            {String.Concat(strMarked, " - ", strAuto), Enums.ScrapeType.MarkedAuto},
            {String.Concat(strMarked, " - ", strAsk), Enums.ScrapeType.MarkedAsk},
            {String.Concat(strMarked, " - ", strSkip), Enums.ScrapeType.MarkedSkip},
            {String.Concat(strFilter, " - ", strAuto), Enums.ScrapeType.FilterAuto},
            {String.Concat(strFilter, " - ", strAsk), Enums.ScrapeType.FilterAsk},
            {String.Concat(strFilter, " - ", strSkip), Enums.ScrapeType.FilterSkip}
        }
        cbCustomScrapeButtonScrapeType.DataSource = items.ToList
        cbCustomScrapeButtonScrapeType.DisplayMember = "Key"
        cbCustomScrapeButtonScrapeType.ValueMember = "Value"
    End Sub

    Private Sub Load_Languages()
        cbLanguageOverlay.Items.Add(Master.eLang.Disabled)
        cbLanguageOverlay.Items.AddRange(Localization.ISOLangGetLanguagesList.ToArray)
    End Sub

    Private Sub LoadDefaults_MediaListSorting() Handles btnMediaListSortingDefaults.Click
        DataGridView_Fill_MediaListSorting(Manager.mSettings.GetDefaultsForList_MediaListSorting_Movie())
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Save_MediaListSorting()
        With Manager.mSettings.Movie.GuiSettings.MediaListSorting
            .Clear()
            For Each r As DataGridViewRow In dgvMediaListSorting.Rows
                .Add(New GuiSettings.ListSorting With {
                     .AutoSizeMode = DirectCast(r.Cells(3).Value, DataGridViewAutoSizeColumnMode),
                     .Column = DirectCast(r.Tag, GuiSettings.ListSorting).Column,
                     .DisplayIndex = DirectCast(r.Cells(0).Value, Integer),
                     .LabelID = DirectCast(r.Tag, GuiSettings.ListSorting).LabelID,
                     .LabelText = DirectCast(r.Tag, GuiSettings.ListSorting).LabelText,
                     .Show = DirectCast(r.Cells(1).Value, Boolean)
                     })
            Next
        End With
    End Sub

    Private Sub TextBox_NumOnly_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtLevTolerance.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
    End Sub

#End Region 'Methods

End Class