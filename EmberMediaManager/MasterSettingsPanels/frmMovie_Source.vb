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

Public Class frmMovie_Source
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
            .Contains = Enums.SettingsPanelType.MovieSource,
            .ImageIndex = 5,
            .Order = 200,
            .Panel = pnlSettings,
            .SettingsPanelID = "Movie_Source",
            .Title = Master.eLang.GetString(621, "Sources & Import Options"),
            .Type = Enums.SettingsPanelType.Movie
        }
    End Function

    Public Sub SaveSettings() Implements Interfaces.IMasterSettingsPanel.SaveSettings
        With Master.eSettings.Movie.SourceSettings
            .CleanLibraryAfterUpdate = chkCleanLibraryAfterUpdate.Checked
            .DateAddedIgnoreNfo = chkDateAddedIgnoreNFO.Checked
            .DateAddedDateTime = CType(cbDateAddedDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
            If Not String.IsNullOrEmpty(cbSourcesDefaultsLanguage.Text) Then
                .DefaultLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbSourcesDefaultsLanguage.Text).Abbreviation
            End If
            .MarkNewAsMarked = chkMarkAsMarked.Checked
            .MarkNewAsMarkedWithoutNFO = chkMarkAsMarkedWithoutNFO.Checked
            .MarkNewAsNew = chkMarkAsNew.Checked
            .MarkNewAsNewWithoutNFO = chkMarkAsNewWithoutNFO.Checked
            .OverWriteNfo = chkOverwriteNfo.Checked
            If Not String.IsNullOrEmpty(txtSkipLessThan.Text) AndAlso Integer.TryParse(txtSkipLessThan.Text, 0) Then
                .SkipLessThan = Convert.ToInt32(txtSkipLessThan.Text)
            Else
                .SkipLessThan = 0
            End If
            .SortBeforeScan = chkSortBeforeScan.Checked
            .TitleFiltersEnabled = chkTitleFiltersEnabled.Checked
            .TitleProperCase = chkTitleProperCase.Checked
            .UnmarkNewBeforeDBUpdate = chkUnmarkNewBeforeDBUpdate.Checked
            .UnmarkNewOnExit = chkUnmarkNewOnExit.Checked
            .VideoSourceFromFolder = chkVideoSourceFromFolder.Checked
        End With
        Save_Sources()
        Save_TitleFilters()
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings.Movie.SourceSettings
            cbDateAddedDateTime.SelectedValue = .DateAddedDateTime
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
            btnTitleFilterDefaults.Enabled = .TitleFiltersEnabled
            chkCleanLibraryAfterUpdate.Checked = .CleanLibraryAfterUpdate
            chkDateAddedIgnoreNFO.Checked = .DateAddedIgnoreNfo
            chkMarkAsMarked.Checked = .MarkNewAsMarked
            chkMarkAsMarkedWithoutNFO.Enabled = .MarkNewAsMarked
            chkMarkAsMarkedWithoutNFO.Checked = .MarkNewAsMarkedWithoutNFO
            chkMarkAsNew.Checked = .MarkNewAsNew
            chkMarkAsNewWithoutNFO.Enabled = .MarkNewAsNew
            chkMarkAsNewWithoutNFO.Checked = .MarkNewAsNewWithoutNFO
            chkOverwriteNfo.Checked = .OverWriteNfo
            chkSortBeforeScan.Checked = .SortBeforeScan
            chkTitleFiltersEnabled.Checked = .TitleFiltersEnabled
            chkTitleProperCase.Checked = .TitleProperCase
            chkVideoSourceFromFolder.Checked = .VideoSourceFromFolder
            dgvTitleFilters.Enabled = .TitleFiltersEnabled
            lblTitleFilters.Enabled = .TitleFiltersEnabled
            txtSkipLessThan.Text = .SkipLessThan.ToString

            DataGridView_Fill_TitleFilters(.TitleFilters)
        End With

        For Each source In Master.DB.Load_AllSources_Movie
            _TmpSources.Add(source, State.Existing)
        Next

        DataGridView_Fill_Sources()
    End Sub

    Private Sub Setup()
        With Master.eLang
            chkCleanLibraryAfterUpdate.Text = .GetString(668, "Clean database after Library Update")
            chkDateAddedIgnoreNFO.Text = .GetString(1209, "Ignore <dateadded> from NFO")
            chkMarkAsMarked.Text = .GetString(459, "Mark as ""Marked""")
            chkMarkAsMarkedWithoutNFO.Text = .GetString(114, "Only if no valid NFO exists")
            chkMarkAsNew.Text = .GetString(530, "Mark as ""New""")
            chkMarkAsNewWithoutNFO.Text = .GetString(114, "Only if no valid NFO exists")
            chkOverwriteNfo.Text = .GetString(433, "Overwrite invalid NFOs")
            chkSortBeforeScan.Text = .GetString(712, "Sort files into folder before each Library Update")
            chkTitleFiltersEnabled.Text = .GetString(451, "Enable Title Filters")
            chkTitleProperCase.Text = .GetString(452, "Convert Names to Proper Case")
            chkVideoSourceFromFolder.Text = .GetString(711, "Search in the full path for VideoSource information")
            cmnuSourcesAdd.Text = .GetString(407, "Add Source")
            cmnuSourcesEdit.Text = .GetString(535, "Edit Source")
            cmnuSourcesMarkToRemove.Text = .GetString(493, "Mark to Remove")
            cmnuSourcesReject.Text = .GetString(494, "Reject Remove Marker")
            colSourcesExclude.HeaderText = .GetString(264, "Exclude")
            colSourcesGetYear.HeaderText = .GetString(586, "Get Year")
            colSourcesIsSingle.HeaderText = .GetString(413, "Single Video")
            colSourcesLanguage.HeaderText = .GetString(610, "Language")
            colSourcesName.HeaderText = .GetString(232, "Name")
            colSourcesPath.HeaderText = .GetString(410, "Path")
            colSourcesRecursive.HeaderText = .GetString(411, "Recursive")
            colSourcesUseFolderName.HeaderText = .GetString(412, "Use Folder Name")
            gbImportOptions.Text = .GetString(559, "Import Options")
            gbSourcesDefaults.Text = .GetString(252, "Defaults for new Sources")
            gbTitleCleanup.Text = Master.eLang.GetString(455, "Title Cleanup")
            lblDateAdded.Text = .GetString(792, "Default value for <dateadded>")
            lblOverwriteNfo.Text = .GetString(434, "(If unchecked, invalid NFOs will be renamed to <filename>.info)")
            lblSkipLessThan.Text = String.Concat(.GetString(540, "Skip files smaller than"), ":")
            lblSkipLessThanMB.Text = .GetString(539, "MB")
            lblSourcesDefaultsLanguage.Text = String.Concat(Master.eLang.GetString(1166, "Default Language"), ":")
            lblTitleFilters.Text = .GetString(456, "Use ALT + UP / DOWN to move the rows")
        End With

        Load_GeneralDateTime()
        Load_ScraperLanguages()
    End Sub

    Private Sub EnableApplyButton() Handles _
            cbDateAddedDateTime.SelectedIndexChanged,
            cbSourcesDefaultsLanguage.SelectedIndexChanged,
            chkCleanLibraryAfterUpdate.CheckedChanged,
            chkDateAddedIgnoreNFO.CheckedChanged,
            chkMarkAsMarkedWithoutNFO.CheckedChanged,
            chkMarkAsNewWithoutNFO.CheckedChanged,
            chkOverwriteNfo.CheckedChanged,
            chkTitleProperCase.CheckedChanged,
            chkUnmarkNewBeforeDBUpdate.CheckedChanged,
            chkUnmarkNewOnExit.CheckedChanged,
            chkVideoSourceFromFolder.CheckedChanged

        RaiseEvent SettingsChanged()
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
        Using dlgSource As New dlgSource_Movie(_TmpSources.Keys.ToList)
            If dlgSource.ShowDialog() = DialogResult.OK AndAlso dlgSource.Result IsNot Nothing Then
                _TmpSources.Add(dlgSource.Result, State.New)
                DataGridView_Fill_Sources()
                RaiseEvent SettingsChanged()
            End If
        End Using
    End Sub

    Private Sub DataGridView_ContextMenu_Edit(ByVal sender As Object, ByVal e As EventArgs) Handles cmnuSourcesEdit.Click
        If dgvSources.SelectedRows.Count = 1 Then
            Dim lngID As Long = CLng(dgvSources.SelectedRows(0).Cells(1).Value)
            Dim kSource = _TmpSources.FirstOrDefault(Function(f) f.Key.ID = lngID)
            Using dlgSource As New dlgSource_Movie(_TmpSources.Keys.ToList)
                If dlgSource.ShowDialog(lngID) = DialogResult.OK AndAlso dlgSource.Result IsNot Nothing Then
                    _TmpSources.Remove(kSource.Key)
                    _TmpSources.Add(dlgSource.Result, State.Edited)
                    DataGridView_Fill_Sources()
                    RaiseEvent SettingsChanged()
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
                                source.Key.ScanRecursive,
                                source.Key.UseFolderName,
                                source.Key.IsSingle,
                                source.Key.Exclude,
                                source.Key.GetYear
                                })
        Next

        dgvSources.ClearSelection()
        dgvSources.Invalidate()
    End Sub

    Private Sub DataGridView_Fill_TitleFilters(ByVal List As List(Of String))
        dgvTitleFilters.Rows.Clear()
        Dim iIndex As Integer = 0
        For Each item In List
            dgvTitleFilters.Rows.Add(New Object() {iIndex, item})
            iIndex += 1
        Next
        dgvTitleFilters.ClearSelection()
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

    Private Sub DataGridView_TitleFilters_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvTitleFilters.KeyDown
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

    Private Sub Load_GeneralDateTime()
        cbDateAddedDateTime.DataSource = Functions.GetDateTimeStampOptions()
        cbDateAddedDateTime.DisplayMember = "Key"
        cbDateAddedDateTime.ValueMember = "Value"
    End Sub

    Private Sub Load_ScraperLanguages()
        cbSourcesDefaultsLanguage.Items.AddRange((From lLang In APIXML.ScraperLanguages.Languages Select lLang.Description).ToArray)
    End Sub

    Private Sub LoadDefaults_TitleFilters() Handles btnTitleFilterDefaults.Click
        If MessageBox.Show(Master.eLang.GetString(840, "Are you sure you want to reset to the default filter list?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_TitleFilters(Master.eSettings.Movie.SourceSettings.TitleFilters.GetDefaults(Enums.ContentType.Movie))
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub MarkAsMarked_CheckedChanged() Handles chkMarkAsMarked.CheckedChanged
        chkMarkAsMarkedWithoutNFO.Enabled = chkMarkAsMarked.Checked
        If Not chkMarkAsMarked.Checked Then chkMarkAsMarkedWithoutNFO.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub MarkAsNew_CheckedChanged() Handles chkMarkAsNew.CheckedChanged
        chkMarkAsNewWithoutNFO.Enabled = chkMarkAsNew.Checked
        If Not chkMarkAsNew.Checked Then chkMarkAsNewWithoutNFO.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub Save_Sources()
        For Each r As DataGridViewRow In dgvSources.Rows
            Select Case True
                Case CInt(r.Cells(0).Value) = State.Existing
                    '0 = existing and unedited source 
                Case CInt(r.Cells(0).Value) = State.[New]
                    '1 = new source
                    RaiseEvent NeedsDBUpdate_Movie(Master.DB.Save_Source_Movie(_TmpSources.Keys.FirstOrDefault(Function(f) f.ID = CLng(r.Cells(1).Value))))
                Case CInt(r.Cells(0).Value) = State.Edited
                    '2 = existing and edited source
                    Master.DB.Save_Source_Movie(_TmpSources.Keys.FirstOrDefault(Function(f) f.ID = CLng(r.Cells(1).Value)))
                    RaiseEvent NeedsReload_Movie()
                Case CInt(r.Cells(0).Value) = State.ExistingToRemove
                    '3 = existing and unedited source is marked to remove
                    Master.DB.Remove_Source_Movie(CLng(r.Cells(1).Value), False)
                Case CInt(r.Cells(0).Value) = State.EditedToRemove
                    '4 = existing and edited source is marked to remove
                    Master.DB.Remove_Source_Movie(CLng(r.Cells(1).Value), False)
            End Select
        Next
        DataGridView_Fill_Sources()
    End Sub

    Private Sub Save_TitleFilters()
        With Master.eSettings.Movie.SourceSettings.TitleFilters
            .Clear()
            For Each r As DataGridViewRow In dgvTitleFilters.Rows
                If r.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString.Trim) Then .Add(r.Cells(1).Value.ToString)
            Next
        End With
    End Sub

    Private Sub SkipLessThan_TextChanged(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSkipLessThan.KeyPress
        e.Handled = StringUtils.NumericOnly(e.KeyChar)
        RaiseEvent NeedsDBClean_Movie()
        RaiseEvent NeedsDBUpdate_Movie(-1)
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TitleFilters_Enabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkTitleFiltersEnabled.CheckedChanged
        dgvTitleFilters.Enabled = chkTitleFiltersEnabled.Checked
        lblTitleFilters.Enabled = chkTitleFiltersEnabled.Checked
        btnTitleFilterDefaults.Enabled = chkTitleFiltersEnabled.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TitleProperCase_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTitleProperCase.CheckedChanged
        RaiseEvent NeedsReload_Movie()
        RaiseEvent SettingsChanged()
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