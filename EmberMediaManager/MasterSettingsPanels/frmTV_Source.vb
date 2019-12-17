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

Public Class frmTV_Source
    Implements Interfaces.IMasterSettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _TmpSources As New Dictionary(Of Database.DBSource, State)

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

#Region "Handles"

    Private Sub Handle_NeedsDBClean_Movie()
        RaiseEvent NeedsDBClean_Movie()
    End Sub

    Private Sub Handle_NeedsDBClean_TV()
        RaiseEvent NeedsDBClean_TV()
    End Sub

    Private Sub Handle_NeedsDBUpdate_Movie(ByVal id As Long)
        RaiseEvent NeedsDBUpdate_Movie(id)
    End Sub

    Private Sub Handle_NeedsDBUpdate_TV(ByVal id As Long)
        RaiseEvent NeedsDBUpdate_TV(id)
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
            .Contains = Enums.SettingsPanelType.TVSource,
            .ImageIndex = 5,
            .Order = 200,
            .Panel = pnlSettings,
            .SettingsPanelID = "TV_Source",
            .Title = Master.eLang.GetString(621, "Sources & Import Options"),
            .Type = Enums.SettingsPanelType.TV
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings.TVEpisode.SourceSettings
            .DateAddedIgnoreNfo = chkDateAddedIgnoreNFO.Checked
            .DateAddedDateTime = CType(cbDateAddedDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
            .OverWriteNfo = chkOverwriteNfo.Checked
            If Not String.IsNullOrEmpty(txtSkipLessThan.Text) AndAlso Integer.TryParse(txtSkipLessThan.Text, 0) Then
                .SkipLessThan = Convert.ToInt32(txtSkipLessThan.Text)
            Else
                .SkipLessThan = 0
            End If
            .TitleFiltersEnabled = chkTitleFiltersEnabled_TVEpisode.Checked
            .TitleProperCase = chkTitleProperCase_TVEpisode.Checked
        End With

        With Master.eSettings.TVShow.SourceSettings
            .CleanLibraryAfterUpdate = chkCleanLibraryAfterUpdate.Checked
            .DateAddedIgnoreNfo = chkDateAddedIgnoreNFO.Checked
            .DateAddedDateTime = CType(cbDateAddedDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
            If Not String.IsNullOrEmpty(cbSourcesDefaultsEpisodeOrdering.Text) Then
                .DefaultEpisodeOrdering = CType(cbSourcesDefaultsEpisodeOrdering.SelectedItem, KeyValuePair(Of String, Enums.EpisodeOrdering)).Value
            End If
            If Not String.IsNullOrEmpty(cbSourcesDefaultsLanguage.Text) Then
                .DefaultLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbSourcesDefaultsLanguage.Text).Abbreviation
            End If
            .OverWriteNfo = chkOverwriteNfo.Checked
            .TitleFiltersEnabled = chkTitleFiltersEnabled_TVShow.Checked
            .TitleProperCase = chkTitleProperCase_TVShow.Checked
        End With

        With Master.eSettings
            .TVGeneralMarkNewEpisodes = chkMarkAsMarked_TVEpisode.Checked
            .TVGeneralMarkNewShows = chkMarkAsMarked_TVShow.Checked
        End With

        Save_Sources()
        Save_TitleFilters_TVEpisode()
        Save_TitleFilters_TVShow()
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.TVEpisode.SourceSettings
            chkTitleFiltersEnabled_TVEpisode.Checked = .TitleFiltersEnabled
            chkTitleProperCase_TVEpisode.Checked = .TitleProperCase
            txtSkipLessThan.Text = .SkipLessThan.ToString
            DataGridView_Fill_TitleFilters_TVEpisode(.TitleFilters)
        End With

        With Master.eSettings.TVShow.SourceSettings
            cbDateAddedDateTime.SelectedValue = .DateAddedDateTime
            cbSourcesDefaultsEpisodeOrdering.SelectedValue = .DefaultEpisodeOrdering
            If cbSourcesDefaultsLanguage.Items.Count > 0 Then
                If Not String.IsNullOrEmpty(.DefaultLanguage) Then
                    Dim tLanguage As languageProperty = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = .DefaultLanguage)
                    If tLanguage IsNot Nothing Then
                        cbSourcesDefaultsLanguage.Text = tLanguage.Description
                    Else
                        tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.DefaultLanguage))
                        If tLanguage IsNot Nothing Then
                            cbSourcesDefaultsLanguage.Text = tLanguage.Description
                        Else
                            cbSourcesDefaultsLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                        End If
                    End If
                Else
                    cbSourcesDefaultsLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
                End If
            End If
            chkCleanLibraryAfterUpdate.Checked = .CleanLibraryAfterUpdate
            chkDateAddedIgnoreNFO.Checked = .DateAddedIgnoreNfo
            chkOverwriteNfo.Checked = .OverWriteNfo
            chkTitleProperCase_TVShow.Checked = .TitleProperCase
            DataGridView_Fill_TitleFilters_TVShow(.TitleFilters)
        End With


        With Master.eSettings
            chkMarkAsMarked_TVEpisode.Checked = .TVGeneralMarkNewEpisodes
            chkMarkAsMarked_TVShow.Checked = .TVGeneralMarkNewShows
        End With

        For Each source In Master.DB.Load_AllSources_TVShow
            _TmpSources.Add(source, State.Existing)
        Next

        DataGridView_Fill_Sources()
    End Sub

    Private Sub Setup()
        With Master.eLang
            chkCleanLibraryAfterUpdate.Text = .GetString(668, "Clean database after Library Update")
            chkDateAddedIgnoreNFO.Text = .GetString(1209, "Ignore <dateadded> from NFO")
            chkOverwriteNfo.Text = .GetString(433, "Overwrite invalid NFOs")
            chkMarkAsMarked_TVEpisode.Text = .GetString(621, "Mark New Episodes")
            chkMarkAsMarked_TVShow.Text = .GetString(549, "Mark New Shows")
            chkTitleFiltersEnabled_TVEpisode.Text = .GetString(451, "Enable Title Filters")
            chkTitleFiltersEnabled_TVShow.Text = .GetString(451, "Enable Title Filters")
            chkTitleProperCase_TVEpisode.Text = .GetString(452, "Convert Names to Proper Case")
            chkTitleProperCase_TVShow.Text = .GetString(452, "Convert Names to Proper Case")
            cmnuSourcesAdd.Text = .GetString(407, "Add Source")
            cmnuSourcesEdit.Text = .GetString(535, "Edit Source")
            cmnuSourcesMarkToRemove.Text = .GetString(493, "Mark to Remove")
            cmnuSourcesReject.Text = .GetString(494, "Reject Remove Marker")
            colSourcesEpisodeOrdering.HeaderText = .GetString(1167, "Ordering")
            colSourcesExclude.HeaderText = .GetString(264, "Exclude")
            colSourcesIsSingle.HeaderText = .GetString(1053, "Single TV Show")
            colSourcesLanguage.HeaderText = .GetString(610, "Language")
            colSourcesName.HeaderText = .GetString(232, "Name")
            colSourcesPath.HeaderText = .GetString(410, "Path")
            colSourcesSorting.HeaderText = .GetString(895, "Sorting")
            gbImportOptions.Text = .GetString(559, "Import Options")
            gbSourcesDefaults.Text = .GetString(252, "Defaults for new Sources")
            gbTitleCleanup.Text = Master.eLang.GetString(455, "Title Cleanup")
            lblDateAdded.Text = .GetString(792, "Default value for <dateadded>")
            lblOverwriteNfo.Text = .GetString(434, "(If unchecked, invalid NFOs will be renamed to <filename>.info)")
            lblSkipLessThan.Text = String.Concat(.GetString(540, "Skip files smaller than"), ":")
            lblSkipLessThanMB.Text = .GetString(539, "MB")
            lblSourcesDefaultsEpisodeOrdering.Text = String.Concat(.GetString(797, "Default Episode Ordering"), ":")
            lblSourcesDefaultsLanguage.Text = String.Concat(Master.eLang.GetString(1166, "Default Language"), ":")
            lblTitleFilters_TVEpisode.Text = .GetString(456, "Use ALT + UP / DOWN to move the rows")
            lblTitleFilters_TVShow.Text = .GetString(456, "Use ALT + UP / DOWN to move the rows")
        End With

        Load_EpisodeOrdering()
        Load_GeneralDateTime()
        Load_ScraperLanguages()
    End Sub

    Private Sub DataGridView_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs) Handles dgvSources.CellPainting
        Dim dgvList As DataGridView = DirectCast(sender, DataGridView)
        If dgvList IsNot Nothing AndAlso e.RowIndex >= 0 Then
            Select Case True
                Case CInt(dgvList.Rows(e.RowIndex).Cells(0).Value) = 0
                    '0 = existing and unedited source
                    e.CellStyle.BackColor = SystemColors.Window
                    e.CellStyle.ForeColor = SystemColors.WindowText
                    e.CellStyle.SelectionBackColor = SystemColors.Highlight
                    e.CellStyle.SelectionForeColor = SystemColors.HighlightText
                Case CInt(dgvList.Rows(e.RowIndex).Cells(0).Value) = 1
                    '1 = new source
                    e.CellStyle.BackColor = Color.LightGreen
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.SelectionBackColor = Color.Green
                    e.CellStyle.SelectionForeColor = Color.White
                Case CInt(dgvList.Rows(e.RowIndex).Cells(0).Value) = 2
                    '2 = edited source
                    e.CellStyle.BackColor = Color.PeachPuff
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.SelectionBackColor = Color.DarkOrange
                    e.CellStyle.SelectionForeColor = Color.Black
                Case CInt(dgvList.Rows(e.RowIndex).Cells(0).Value) = 3
                    '3 = source is marked to remove
                    e.CellStyle.BackColor = Color.LightCoral
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.SelectionBackColor = Color.Red
                    e.CellStyle.SelectionForeColor = Color.White
                Case CInt(dgvList.Rows(e.RowIndex).Cells(0).Value) = 4
                    '4 = edited and marked to remove source
                    e.CellStyle.BackColor = Color.LightCoral
                    e.CellStyle.ForeColor = Color.Black
                    e.CellStyle.SelectionBackColor = Color.Red
                    e.CellStyle.SelectionForeColor = Color.White
            End Select
        End If
    End Sub

    Private Sub DataGridView_ContextMenu_Add(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSourcesAdd.Click
        Using dlgSource As New dlgSource_TVShow(_TmpSources.Keys.ToList)
            If dlgSource.ShowDialog() = DialogResult.OK AndAlso dlgSource.Result IsNot Nothing Then
                _TmpSources.Add(dlgSource.Result, State.New)
                DataGridView_Fill_Sources()
                Handle_SettingsChanged()
            End If
        End Using
    End Sub

    Private Sub DataGridView_ContextMenu_Edit(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSourcesEdit.Click
        If dgvSources.SelectedRows.Count = 1 Then
            Dim lngID As Long = CLng(dgvSources.SelectedRows(0).Cells(1).Value)
            Dim kSource = _TmpSources.FirstOrDefault(Function(f) f.Key.ID = lngID)
            Using dlgSource As New dlgSource_TVShow(_TmpSources.Keys.ToList)
                If dlgSource.ShowDialog(lngID) = DialogResult.OK AndAlso dlgSource.Result IsNot Nothing Then
                    _TmpSources.Remove(kSource.Key)
                    _TmpSources.Add(dlgSource.Result, State.Edited)
                    DataGridView_Fill_Sources()
                    Handle_SettingsChanged()
                End If
            End Using
        End If
    End Sub

    Private Sub DataGridView_ContextMenu_MarkToRemove(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSourcesMarkToRemove.Click
        If dgvSources.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In dgvSources.SelectedRows
                Select Case True
                    Case CInt(row.Cells(0).Value) = State.Existing
                        '0 = existing and unedited source: set it to "existing and unedited source is marked to remove"
                        row.Cells(0).Value = State.ExistingToRemove
                    Case CInt(row.Cells(0).Value) = State.[New]
                        '1 = new source: remove it instant
                        dgvSources.Rows.Remove(row)
                    Case CInt(row.Cells(0).Value) = State.Edited
                        '2 = existing and edited source: set it to "existing and edited source is marked to remove"
                        row.Cells(0).Value = State.EditedToRemove
                End Select
            Next
            dgvSources.Invalidate()
        End If
    End Sub

    Private Sub DataGridView_ContextMenu_Reject(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSourcesReject.Click
        If dgvSources.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In dgvSources.SelectedRows
                Select Case True
                    Case CInt(row.Cells(0).Value) = State.ExistingToRemove
                        '3 = existing and unedited source is marked to remove: set it back to "existing and unedited source"
                        row.Cells(0).Value = State.Existing
                    Case CInt(row.Cells(0).Value) = State.EditedToRemove
                        '4 = existing and edited source is marked to remove: set it back to "existing and edited source"
                        row.Cells(0).Value = State.Edited
                End Select
            Next
            dgvSources.Invalidate()
        End If
    End Sub

    Private Sub DataGridView_Fill_Sources()
        dgvSources.Rows.Clear()
        For Each source In _TmpSources
            dgvSources.Rows.Add(New Object() {
                                source.Value,
                                source.Key.ID,
                                source.Key.Name,
                                source.Key.Path,
                                source.Key.Language,
                                source.Key.EpisodeOrdering,
                                source.Key.EpisodeSorting,
                                source.Key.IsSingle,
                                source.Key.Exclude
                                })
        Next

        dgvSources.ClearSelection()
        dgvSources.Invalidate()
    End Sub

    Private Sub DataGridView_Fill_TitleFilters_TVEpisode(ByVal List As List(Of String))
        dgvTitleFilters_TVEpisode.Rows.Clear()
        Dim iIndex As Integer = 0
        For Each item In List
            dgvTitleFilters_TVEpisode.Rows.Add(New Object() {iIndex, item})
            iIndex += 1
        Next
        dgvTitleFilters_TVEpisode.ClearSelection()
    End Sub

    Private Sub DataGridView_Fill_TitleFilters_TVShow(ByVal List As List(Of String))
        dgvTitleFilters_TVShow.Rows.Clear()
        Dim iIndex As Integer = 0
        For Each item In List
            dgvTitleFilters_TVShow.Rows.Add(New Object() {iIndex, item})
            iIndex += 1
        Next
        dgvTitleFilters_TVShow.ClearSelection()
    End Sub

    Private Sub DataGridView_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles dgvSources.MouseDown
        If e.Button = MouseButtons.Right And dgvSources.RowCount > 0 Then
            Dim dgvHTI As DataGridView.HitTestInfo = dgvSources.HitTest(e.X, e.Y)
            If dgvHTI.Type = DataGridViewHitTestType.Cell Then
                If Not dgvSources.Rows(dgvHTI.RowIndex).Selected Then
                    dgvSources.ClearSelection()
                    dgvSources.Rows(dgvHTI.RowIndex).Selected = True
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView_TitleFilters_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvTitleFilters_TVEpisode.KeyDown, dgvTitleFilters_TVShow.KeyDown
        Dim dgvList As DataGridView = DirectCast(sender, DataGridView)
        Dim currRowIndex As Integer = dgvList.CurrentRow.Index
        Dim currRowIsNew As Boolean = dgvList.CurrentRow.IsNewRow
        If Not currRowIsNew Then
            Select Case True
                Case e.Alt And e.KeyCode = Keys.Down AndAlso Not currRowIndex = dgvList.Rows.Count - 1 AndAlso Not dgvList.Rows(currRowIndex + 1).IsNewRow
                    dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) + 1
                    dgvList.Rows(currRowIndex + 1).Cells(0).Value = currRowIndex
                    currRowIndex += 1
                    e.Handled = True
                Case e.Alt And e.KeyCode = Keys.Up AndAlso Not currRowIndex = 0
                    dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) - 1
                    dgvList.Rows(currRowIndex - 1).Cells(0).Value = currRowIndex
                    currRowIndex -= 1
                    e.Handled = True
                Case Else
                    Return
            End Select
            dgvList.Sort(dgvList.Columns(0), ComponentModel.ListSortDirection.Ascending)
            If Not dgvList.SelectedRows(0).State.HasFlag(DataGridViewElementStates.Displayed) Then
                dgvList.FirstDisplayedScrollingRowIndex = currRowIndex
            End If
        End If
    End Sub

    Private Sub Load_EpisodeOrdering()
        Dim items As New Dictionary(Of String, Enums.EpisodeOrdering)
        items.Add(Master.eLang.GetString(438, "Standard"), Enums.EpisodeOrdering.Standard)
        items.Add(Master.eLang.GetString(1067, "DVD"), Enums.EpisodeOrdering.DVD)
        items.Add(Master.eLang.GetString(839, "Absolute"), Enums.EpisodeOrdering.Absolute)
        items.Add(Master.eLang.GetString(1332, "Day Of Year"), Enums.EpisodeOrdering.DayOfYear)
        cbSourcesDefaultsEpisodeOrdering.DataSource = items.ToList
        cbSourcesDefaultsEpisodeOrdering.DisplayMember = "Key"
        cbSourcesDefaultsEpisodeOrdering.ValueMember = "Value"
    End Sub

    Private Sub Load_GeneralDateTime()
        cbDateAddedDateTime.DataSource = Functions.GetDateTimeStampOptions()
        cbDateAddedDateTime.DisplayMember = "Key"
        cbDateAddedDateTime.ValueMember = "Value"
    End Sub

    Private Sub Load_ScraperLanguages()
        cbSourcesDefaultsLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Description).ToArray)
    End Sub

    Private Sub LoadDefaults_TitleFilters_TVEpisode() Handles btnTitleFilterDefaults_TVepisode.Click
        If MessageBox.Show(Master.eLang.GetString(840, "Are you sure you want to reset to the default filter list?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_TitleFilters_TVEpisode(Master.eSettings.TVEpisode.SourceSettings.TitleFilters.GetDefaults(Enums.ContentType.TVEpisode))
            Handle_SettingsChanged()
        End If
    End Sub

    Private Sub LoadDefaults_TitleFilters_TVShow() Handles btnTitleFilterDefaults_TVShow.Click
        If MessageBox.Show(Master.eLang.GetString(840, "Are you sure you want to reset to the default filter list?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_TitleFilters_TVShow(Master.eSettings.TVShow.SourceSettings.TitleFilters.GetDefaults(Enums.ContentType.TVShow))
            Handle_SettingsChanged()
        End If
    End Sub

    Private Sub Save_Sources()
        For Each r As DataGridViewRow In dgvSources.Rows
            Select Case True
                Case CInt(r.Cells(0).Value) = State.Existing
                    '0 = existing and unedited source 
                Case CInt(r.Cells(0).Value) = State.[New]
                    '1 = new source
                    RaiseEvent NeedsDBUpdate_TV(Master.DB.Save_Source_TVShow(_TmpSources.Keys.FirstOrDefault(Function(f) f.ID = CLng(r.Cells(1).Value))))
                Case CInt(r.Cells(0).Value) = State.Edited
                    '2 = existing and edited source
                    Master.DB.Save_Source_TVShow(_TmpSources.Keys.FirstOrDefault(Function(f) f.ID = CLng(r.Cells(1).Value)))
                    RaiseEvent NeedsReload_TVShow()
                Case CInt(r.Cells(0).Value) = State.ExistingToRemove
                    '3 = existing and unedited source is marked to remove
                    Master.DB.Remove_Source_TVShow(CLng(r.Cells(1).Value), False)
                Case CInt(r.Cells(0).Value) = State.EditedToRemove
                    '4 = existing and edited source is marked to remove
                    Master.DB.Remove_Source_TVShow(CLng(r.Cells(1).Value), False)
            End Select
        Next
        DataGridView_Fill_Sources()
    End Sub

    Private Sub Save_TitleFilters_TVEpisode()
        With Master.eSettings.TVEpisode.SourceSettings.TitleFilters
            .Clear()
            For Each r As DataGridViewRow In dgvTitleFilters_TVEpisode.Rows
                If r.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString.Trim) Then .Add(r.Cells(1).Value.ToString)
            Next
        End With
    End Sub

    Private Sub Save_TitleFilters_TVShow()
        With Master.eSettings.TVShow.SourceSettings.TitleFilters
            .Clear()
            For Each r As DataGridViewRow In dgvTitleFilters_TVShow.Rows
                If r.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString.Trim) Then .Add(r.Cells(1).Value.ToString)
            Next
        End With
    End Sub

    Private Sub SkipLessThan_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtSkipLessThan.TextChanged
        RaiseEvent NeedsDBClean_TV()
        RaiseEvent NeedsDBUpdate_TV(-1)
        Handle_SettingsChanged()
    End Sub

    Private Sub TitleFilters_Enabled_TVEpisode_CheckedChanged(sender As Object, e As EventArgs) Handles chkTitleFiltersEnabled_TVEpisode.CheckedChanged
        dgvTitleFilters_TVEpisode.Enabled = chkTitleFiltersEnabled_TVEpisode.Checked
        lblTitleFilters_TVEpisode.Enabled = chkTitleFiltersEnabled_TVEpisode.Checked
        btnTitleFilterDefaults_TVepisode.Enabled = chkTitleFiltersEnabled_TVEpisode.Checked
        Handle_SettingsChanged()
    End Sub

    Private Sub TitleFilters_Enabled_TVShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkTitleFiltersEnabled_TVShow.CheckedChanged
        dgvTitleFilters_TVShow.Enabled = chkTitleFiltersEnabled_TVShow.Checked
        lblTitleFilters_TVShow.Enabled = chkTitleFiltersEnabled_TVShow.Checked
        btnTitleFilterDefaults_TVShow.Enabled = chkTitleFiltersEnabled_TVShow.Checked
        Handle_SettingsChanged()
    End Sub

    Private Sub TitleProperCase_TVEpisode_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTitleProperCase_TVEpisode.CheckedChanged
        Handle_NeedsReload_TVEpisode()
        Handle_SettingsChanged()
    End Sub

    Private Sub TitleProperCase_TVShow_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTitleProperCase_TVShow.CheckedChanged
        Handle_NeedsReload_TVShow()
        Handle_SettingsChanged()
    End Sub

#End Region 'Methods

#Region "Nested Types"

    Private Enum State As Integer
        Existing = 0
        [New] = 1
        Edited = 2
        ExistingToRemove = 3
        EditedToRemove = 4
    End Enum

#End Region 'Nested Types

End Class