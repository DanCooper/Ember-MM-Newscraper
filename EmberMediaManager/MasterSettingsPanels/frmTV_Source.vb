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
    Implements Interfaces.ISettingsPanel

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _TmpSources As New Dictionary(Of Database.DBSource, State)

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
        ChildType = Enums.SettingsPanelType.TVSource
        IsEnabled = True
        Image = Nothing
        ImageIndex = 5
        Order = 200
        MainPanel = pnlSettings
        Title = Master.eLang.GetString(621, "Sources & Import Options")
        ParentType = Enums.SettingsPanelType.TV
        UniqueId = "TV_Source"

        Setup()
        Settings_Load()
    End Sub

    Private Sub Setup()
        With Master.eLang
            btnTitleFilterDefaults_TVEpisode.Text = .GetString(713, "Defaults")
            btnTitleFilterDefaults_TVShow.Text = .GetString(713, "Defaults")
            chkCleanLibraryAfterUpdate.Text = .GetString(668, "Clean database after Library Update")
            chkDateAddedIgnoreNFO.Text = .GetString(1209, "Ignore <dateadded> from NFO")
            chkMarkAsMarkedWithoutNFO_TVEpisode.Text = .GetString(114, "Only if no valid NFO exists")
            chkMarkAsMarkedWithoutNFO_TVShow.Text = .GetString(114, "Only if no valid NFO exists")
            chkMarkAsMarked_TVEpisode.Text = .GetString(459, "Mark as ""Marked""")
            chkMarkAsMarked_TVShow.Text = .GetString(459, "Mark as ""Marked""")
            chkMarkAsNewWithoutNFO_TVEpisode.Text = .GetString(114, "Only if no valid NFO exists")
            chkMarkAsNewWithoutNFO_TVShow.Text = .GetString(114, "Only if no valid NFO exists")
            chkMarkAsNew_TVEpisode.Text = .GetString(530, "Mark as ""New""")
            chkMarkAsNew_TVShow.Text = .GetString(530, "Mark as ""New""")
            chkOverwriteNfo.Text = .GetString(433, "Overwrite invalid NFOs")
            chkResetNewBeforeDBUpdate_TVEpisode.Text = .GetString(693, "Before any Library Update")
            chkResetNewBeforeDBUpdate_TVShow.Text = .GetString(693, "Before any Library Update")
            chkResetNewOnExit_TVEpisode.Text = .GetString(734, "On Exit")
            chkResetNewOnExit_TVShow.Text = .GetString(734, "On Exit")
            chkTitleFiltersEnabled_TVEpisode.Text = .GetString(451, "Enable Title Filters")
            chkTitleFiltersEnabled_TVShow.Text = .GetString(451, "Enable Title Filters")
            chkTitleProperCase_TVEpisode.Text = .GetString(452, "Convert Names to Proper Case")
            chkTitleProperCase_TVShow.Text = .GetString(452, "Convert Names to Proper Case")
            chkVideoSourceFromFolder.Text = .GetString(711, "Search in the full path for VideoSource information")
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
            colTitleFiltersRegex_TVEpisode.HeaderText = .GetString(699, "Regex")
            colTitleFiltersRegex_TVShow.HeaderText = .GetString(699, "Regex")
            gbImportOptions.Text = .GetString(559, "Import Options")
            gbMarkNew.Text = .GetString(691, "Mark newly added")
            gbResetNew.Text = .GetString(692, "Reset marker ""New""")
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

#End Region 'Constructors

#Region "Interface Methodes"

    Public Sub DoDispose() Implements Interfaces.ISettingsPanel.DoDispose
        Dispose()
    End Sub

    Public Sub Addon_Order_Changed(ByVal totalCount As Integer) Implements Interfaces.ISettingsPanel.OrderChanged
        Return
    End Sub

    Public Sub SaveSettings() Implements Interfaces.ISettingsPanel.SaveSettings
        With Master.eSettings
            '.DateAddedIgnoreNfo = chkDateAddedIgnoreNFO.Checked
            '.DateAddedDateTime = CType(cbDateAddedDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
            '.MarkNewAsMarked = chkMarkAsMarked_TVEpisode.Checked
            '.MarkNewAsMarkedWithoutNFO = chkMarkAsMarkedWithoutNFO_TVEpisode.Checked
            '.MarkNewAsNew = chkMarkAsNew_TVEpisode.Checked
            '.MarkNewAsNewWithoutNFO = chkMarkAsNewWithoutNFO_TVEpisode.Checked
            '.OverWriteNfo = chkOverwriteNfo.Checked
            '.ResetNewBeforeDBUpdate = chkResetNewBeforeDBUpdate_TVEpisode.Checked
            '.ResetNewOnExit = chkResetNewOnExit_TVEpisode.Checked
            'If Not String.IsNullOrEmpty(txtSkipLessThan.Text) AndAlso Integer.TryParse(txtSkipLessThan.Text, 0) Then
            '    .SkipLessThan = Convert.ToInt32(txtSkipLessThan.Text)
            'Else
            '    .SkipLessThan = 0
            'End If
            '.TitleFiltersEnabled = chkTitleFiltersEnabled_TVEpisode.Checked
            '.TitleProperCase = chkTitleProperCase_TVEpisode.Checked
        End With

        With Master.eSettings
            '.CleanLibraryAfterUpdate = chkCleanLibraryAfterUpdate.Checked
            '.DateAddedIgnoreNfo = chkDateAddedIgnoreNFO.Checked
            '.DateAddedDateTime = CType(cbDateAddedDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
            'If Not String.IsNullOrEmpty(cbSourcesDefaultsEpisodeOrdering.Text) Then
            '    .DefaultEpisodeOrdering = CType(cbSourcesDefaultsEpisodeOrdering.SelectedItem, KeyValuePair(Of String, Enums.EpisodeOrdering)).Value
            'End If
            'If Not String.IsNullOrEmpty(cbSourcesDefaultsLanguage.Text) Then
            '    .DefaultLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbSourcesDefaultsLanguage.Text).Abbreviation
            'End If
            '.MarkNewAsMarked = chkMarkAsMarked_TVShow.Checked
            '.MarkNewAsMarkedWithoutNFO = chkMarkAsMarkedWithoutNFO_TVShow.Checked
            '.MarkNewAsNew = chkMarkAsNew_TVShow.Checked
            '.MarkNewAsNewWithoutNFO = chkMarkAsNewWithoutNFO_TVShow.Checked
            '.OverWriteNfo = chkOverwriteNfo.Checked
            '.ResetNewBeforeDBUpdate = chkResetNewBeforeDBUpdate_TVShow.Checked
            '.ResetNewOnExit = chkResetNewOnExit_TVShow.Checked
            '.TitleFiltersEnabled = chkTitleFiltersEnabled_TVShow.Checked
            '.TitleProperCase = chkTitleProperCase_TVShow.Checked
        End With
        'With Master.eSettings.TVEpisode.SourceSettings
        '    .DateAddedIgnoreNfo = chkDateAddedIgnoreNFO.Checked
        '    .DateAddedDateTime = CType(cbDateAddedDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
        '    .MarkNewAsMarked = chkMarkAsMarked_TVEpisode.Checked
        '    .MarkNewAsMarkedWithoutNFO = chkMarkAsMarkedWithoutNFO_TVEpisode.Checked
        '    .MarkNewAsNew = chkMarkAsNew_TVEpisode.Checked
        '    .MarkNewAsNewWithoutNFO = chkMarkAsNewWithoutNFO_TVEpisode.Checked
        '    .OverWriteNfo = chkOverwriteNfo.Checked
        '    .ResetNewBeforeDBUpdate = chkResetNewBeforeDBUpdate_TVEpisode.Checked
        '    .ResetNewOnExit = chkResetNewOnExit_TVEpisode.Checked
        '    If Not String.IsNullOrEmpty(txtSkipLessThan.Text) AndAlso Integer.TryParse(txtSkipLessThan.Text, 0) Then
        '        .SkipLessThan = Convert.ToInt32(txtSkipLessThan.Text)
        '    Else
        '        .SkipLessThan = 0
        '    End If
        '    .TitleFiltersEnabled = chkTitleFiltersEnabled_TVEpisode.Checked
        '    .TitleProperCase = chkTitleProperCase_TVEpisode.Checked
        'End With

        'With Master.eSettings.TVShow.SourceSettings
        '    .CleanLibraryAfterUpdate = chkCleanLibraryAfterUpdate.Checked
        '    .DateAddedIgnoreNfo = chkDateAddedIgnoreNFO.Checked
        '    .DateAddedDateTime = CType(cbDateAddedDateTime.SelectedItem, KeyValuePair(Of String, Enums.DateTimeStamp)).Value
        '    If Not String.IsNullOrEmpty(cbSourcesDefaultsEpisodeOrdering.Text) Then
        '        .DefaultEpisodeOrdering = CType(cbSourcesDefaultsEpisodeOrdering.SelectedItem, KeyValuePair(Of String, Enums.EpisodeOrdering)).Value
        '    End If
        '    If Not String.IsNullOrEmpty(cbSourcesDefaultsLanguage.Text) Then
        '        .DefaultLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Description = cbSourcesDefaultsLanguage.Text).Abbreviation
        '    End If
        '    .MarkNewAsMarked = chkMarkAsMarked_TVShow.Checked
        '    .MarkNewAsMarkedWithoutNFO = chkMarkAsMarkedWithoutNFO_TVShow.Checked
        '    .MarkNewAsNew = chkMarkAsNew_TVShow.Checked
        '    .MarkNewAsNewWithoutNFO = chkMarkAsNewWithoutNFO_TVShow.Checked
        '    .OverWriteNfo = chkOverwriteNfo.Checked
        '    .ResetNewBeforeDBUpdate = chkResetNewBeforeDBUpdate_TVShow.Checked
        '    .ResetNewOnExit = chkResetNewOnExit_TVShow.Checked
        '    .TitleFiltersEnabled = chkTitleFiltersEnabled_TVShow.Checked
        '    .TitleProperCase = chkTitleProperCase_TVShow.Checked
        'End With

        With Master.eSettings
            .TVGeneralMarkNewEpisodes = chkMarkAsNew_TVEpisode.Checked
            .TVGeneralMarkNewShows = chkMarkAsNew_TVShow.Checked
        End With

        Save_Sources()
        Save_TitleFilters_TVEpisode()
        Save_TitleFilters_TVShow()
    End Sub

#End Region 'Interface Methodes

#Region "Methods"

    Public Sub Settings_Load()
        With Master.eSettings
            'btnTitleFilterDefaults_TVEpisode.Enabled = .TitleFiltersEnabled
            'chkMarkAsMarked_TVEpisode.Checked = .MarkNewAsMarked
            'chkMarkAsMarkedWithoutNFO_TVEpisode.Enabled = .MarkNewAsMarked
            'chkMarkAsMarkedWithoutNFO_TVEpisode.Checked = .MarkNewAsMarkedWithoutNFO
            'chkMarkAsNew_TVEpisode.Checked = .MarkNewAsNew
            'chkMarkAsNewWithoutNFO_TVEpisode.Enabled = .MarkNewAsNew
            'chkMarkAsNewWithoutNFO_TVEpisode.Checked = .MarkNewAsNewWithoutNFO
            'chkResetNewBeforeDBUpdate_TVEpisode.Checked = .ResetNewBeforeDBUpdate
            'chkResetNewOnExit_TVEpisode.Checked = .ResetNewOnExit
            'chkTitleFiltersEnabled_TVEpisode.Checked = .TitleFiltersEnabled
            'chkTitleProperCase_TVEpisode.Checked = .TitleProperCase
            'dgvTitleFilters_TVEpisode.Enabled = .TitleFiltersEnabled
            'lblTitleFilters_TVEpisode.Enabled = .TitleFiltersEnabled
            'txtSkipLessThan.Text = .SkipLessThan.ToString

            DataGridView_Fill_TitleFilters_TVEpisode(.TVEpisodeFilterCustom)
        End With

        With Master.eSettings
            'cbDateAddedDateTime.SelectedValue = .DateAddedDateTime
            'cbSourcesDefaultsEpisodeOrdering.SelectedValue = .DefaultEpisodeOrdering
            'If cbSourcesDefaultsLanguage.Items.Count > 0 Then
            '    If Not String.IsNullOrEmpty(.DefaultLanguage) Then
            '        Dim tLanguage As languageProperty = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = .DefaultLanguage)
            '        If tLanguage IsNot Nothing Then
            '            cbSourcesDefaultsLanguage.Text = tLanguage.Description
            '        Else
            '            tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.DefaultLanguage))
            '            If tLanguage IsNot Nothing Then
            '                cbSourcesDefaultsLanguage.Text = tLanguage.Description
            '            Else
            '                cbSourcesDefaultsLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
            '            End If
            '        End If
            '    Else
            '        cbSourcesDefaultsLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
            '    End If
            'End If
            'btnTitleFilterDefaults_TVShow.Enabled = .TitleFiltersEnabled
            'chkCleanLibraryAfterUpdate.Checked = .CleanLibraryAfterUpdate
            'chkDateAddedIgnoreNFO.Checked = .DateAddedIgnoreNfo
            'chkMarkAsMarked_TVShow.Checked = .MarkNewAsMarked
            'chkMarkAsMarkedWithoutNFO_TVShow.Enabled = .MarkNewAsMarked
            'chkMarkAsMarkedWithoutNFO_TVShow.Checked = .MarkNewAsMarkedWithoutNFO
            'chkMarkAsNew_TVShow.Checked = .MarkNewAsNew
            'chkMarkAsNewWithoutNFO_TVShow.Enabled = .MarkNewAsNew
            'chkMarkAsNewWithoutNFO_TVShow.Checked = .MarkNewAsNewWithoutNFO
            'chkOverwriteNfo.Checked = .OverWriteNfo
            'chkResetNewBeforeDBUpdate_TVShow.Checked = .ResetNewBeforeDBUpdate
            'chkResetNewOnExit_TVShow.Checked = .ResetNewOnExit
            'chkTitleProperCase_TVShow.Checked = .TitleProperCase
            'dgvTitleFilters_TVShow.Enabled = .TitleFiltersEnabled
            'lblTitleFilters_TVShow.Enabled = .TitleFiltersEnabled

            DataGridView_Fill_TitleFilters_TVShow(.TVShowFilterCustom)
        End With
        'With Master.eSettings.TVEpisode.SourceSettings
        '    btnTitleFilterDefaults_TVEpisode.Enabled = .TitleFiltersEnabled
        '    chkMarkAsMarked_TVEpisode.Checked = .MarkNewAsMarked
        '    chkMarkAsMarkedWithoutNFO_TVEpisode.Enabled = .MarkNewAsMarked
        '    chkMarkAsMarkedWithoutNFO_TVEpisode.Checked = .MarkNewAsMarkedWithoutNFO
        '    chkMarkAsNew_TVEpisode.Checked = .MarkNewAsNew
        '    chkMarkAsNewWithoutNFO_TVEpisode.Enabled = .MarkNewAsNew
        '    chkMarkAsNewWithoutNFO_TVEpisode.Checked = .MarkNewAsNewWithoutNFO
        '    chkResetNewBeforeDBUpdate_TVEpisode.Checked = .ResetNewBeforeDBUpdate
        '    chkResetNewOnExit_TVEpisode.Checked = .ResetNewOnExit
        '    chkTitleFiltersEnabled_TVEpisode.Checked = .TitleFiltersEnabled
        '    chkTitleProperCase_TVEpisode.Checked = .TitleProperCase
        '    dgvTitleFilters_TVEpisode.Enabled = .TitleFiltersEnabled
        '    lblTitleFilters_TVEpisode.Enabled = .TitleFiltersEnabled
        '    txtSkipLessThan.Text = .SkipLessThan.ToString

        '    DataGridView_Fill_TitleFilters_TVEpisode(.TitleFilters)
        'End With

        'With Master.eSettings.TVShow.SourceSettings
        '    cbDateAddedDateTime.SelectedValue = .DateAddedDateTime
        '    cbSourcesDefaultsEpisodeOrdering.SelectedValue = .DefaultEpisodeOrdering
        '    If cbSourcesDefaultsLanguage.Items.Count > 0 Then
        '        If Not String.IsNullOrEmpty(.DefaultLanguage) Then
        '            Dim tLanguage As languageProperty = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = .DefaultLanguage)
        '            If tLanguage IsNot Nothing Then
        '                cbSourcesDefaultsLanguage.Text = tLanguage.Description
        '            Else
        '                tLanguage = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation.StartsWith(.DefaultLanguage))
        '                If tLanguage IsNot Nothing Then
        '                    cbSourcesDefaultsLanguage.Text = tLanguage.Description
        '                Else
        '                    cbSourcesDefaultsLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
        '                End If
        '            End If
        '        Else
        '            cbSourcesDefaultsLanguage.Text = APIXML.ScraperLanguages.Languages.FirstOrDefault(Function(l) l.Abbreviation = "en-US").Description
        '        End If
        '    End If
        '    btnTitleFilterDefaults_TVShow.Enabled = .TitleFiltersEnabled
        '    chkCleanLibraryAfterUpdate.Checked = .CleanLibraryAfterUpdate
        '    chkDateAddedIgnoreNFO.Checked = .DateAddedIgnoreNfo
        '    chkMarkAsMarked_TVShow.Checked = .MarkNewAsMarked
        '    chkMarkAsMarkedWithoutNFO_TVShow.Enabled = .MarkNewAsMarked
        '    chkMarkAsMarkedWithoutNFO_TVShow.Checked = .MarkNewAsMarkedWithoutNFO
        '    chkMarkAsNew_TVShow.Checked = .MarkNewAsNew
        '    chkMarkAsNewWithoutNFO_TVShow.Enabled = .MarkNewAsNew
        '    chkMarkAsNewWithoutNFO_TVShow.Checked = .MarkNewAsNewWithoutNFO
        '    chkOverwriteNfo.Checked = .OverWriteNfo
        '    chkResetNewBeforeDBUpdate_TVShow.Checked = .ResetNewBeforeDBUpdate
        '    chkResetNewOnExit_TVShow.Checked = .ResetNewOnExit
        '    chkTitleProperCase_TVShow.Checked = .TitleProperCase
        '    dgvTitleFilters_TVShow.Enabled = .TitleFiltersEnabled
        '    lblTitleFilters_TVShow.Enabled = .TitleFiltersEnabled

        '    DataGridView_Fill_TitleFilters_TVShow(.TitleFilters)
        'End With


        With Master.eSettings
            chkMarkAsNew_TVEpisode.Checked = .TVGeneralMarkNewEpisodes
            chkMarkAsNew_TVShow.Checked = .TVGeneralMarkNewShows
        End With

        For Each source In Master.DB.LoadAll_Sources_TVShow
            _TmpSources.Add(source, State.Existing)
        Next

        DataGridView_Fill_Sources()
    End Sub

    Private Sub EnableApplyButton() Handles _
            cbDateAddedDateTime.SelectedIndexChanged,
            cbSourcesDefaultsEpisodeOrdering.SelectedIndexChanged,
            cbSourcesDefaultsLanguage.SelectedIndexChanged,
            chkCleanLibraryAfterUpdate.CheckedChanged,
            chkDateAddedIgnoreNFO.CheckedChanged,
            chkMarkAsMarkedWithoutNFO_TVEpisode.CheckedChanged,
            chkMarkAsMarkedWithoutNFO_TVShow.CheckedChanged,
            chkMarkAsNewWithoutNFO_TVEpisode.CheckedChanged,
            chkMarkAsNewWithoutNFO_TVShow.CheckedChanged,
            chkOverwriteNfo.CheckedChanged,
            chkTitleProperCase_TVEpisode.CheckedChanged,
            chkTitleProperCase_TVShow.CheckedChanged,
            chkResetNewBeforeDBUpdate_TVShow.CheckedChanged,
            chkResetNewOnExit_TVShow.CheckedChanged,
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
        Using dlgSource As New dlgSource_TVShow(_TmpSources.Keys.ToList)
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
            Using dlgSource As New dlgSource_TVShow(_TmpSources.Keys.ToList)
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

    Private Sub DataGridView_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvTitleFilters_TVEpisode.KeyDown, dgvTitleFilters_TVShow.KeyDown
        Dim dgvList As DataGridView = DirectCast(sender, DataGridView)
        Dim currRowIndex As Integer = dgvList.CurrentRow.Index
        Dim currRowIsNew As Boolean = dgvList.CurrentRow.IsNewRow
        If Not currRowIsNew Then
            Select Case True
                Case e.Alt And e.KeyCode = Keys.Up AndAlso Not currRowIndex = 0
                    dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) - 1
                    dgvList.Rows(currRowIndex - 1).Cells(0).Value = currRowIndex
                    currRowIndex -= 1
                    e.Handled = True
                Case e.Alt And e.KeyCode = Keys.Down AndAlso Not currRowIndex = dgvList.Rows.Count - 1 AndAlso Not dgvList.Rows(currRowIndex + 1).IsNewRow
                    dgvList.CurrentRow.Cells(0).Value = DirectCast(dgvList.CurrentRow.Cells(0).Value, Integer) + 1
                    dgvList.Rows(currRowIndex + 1).Cells(0).Value = currRowIndex
                    currRowIndex += 1
                    e.Handled = True
                Case e.Alt And e.KeyCode = Keys.Down
                    e.Handled = True
                    Return
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

    Private Sub LoadDefaults_TitleFilters_TVEpisode() Handles btnTitleFilterDefaults_TVEpisode.Click
        If MessageBox.Show(Master.eLang.GetString(840, "Are you sure you want to reset to the default filter list?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_TitleFilters_TVEpisode(Master.eSettings.TVEpisodeFilterCustom.GetDefaults())
            'DataGridView_Fill_TitleFilters_TVEpisode(Master.eSettings.TVEpisode.SourceSettings.TitleFilters.GetDefaults(Enums.DefaultType.TitleFilters_TVEpisode))
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub LoadDefaults_TitleFilters_TVShow() Handles btnTitleFilterDefaults_TVShow.Click
        If MessageBox.Show(Master.eLang.GetString(840, "Are you sure you want to reset to the default filter list?"),
                           Master.eLang.GetString(104, "Are You Sure?"),
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            DataGridView_Fill_TitleFilters_TVShow(Master.eSettings.TVShowFilterCustom.GetDefaults())
            'DataGridView_Fill_TitleFilters_TVShow(Master.eSettings.TVShow.SourceSettings.TitleFilters.GetDefaults(Enums.DefaultType.TitleFilters_TVShow))
            RaiseEvent SettingsChanged()
        End If
    End Sub

    Private Sub MarkAsMarked_TVEpisode_CheckedChanged() Handles chkMarkAsMarked_TVEpisode.CheckedChanged
        chkMarkAsMarkedWithoutNFO_TVEpisode.Enabled = chkMarkAsMarked_TVEpisode.Checked
        If Not chkMarkAsMarked_TVEpisode.Checked Then chkMarkAsMarkedWithoutNFO_TVEpisode.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub MarkAsMarked_TVShow_CheckedChanged() Handles chkMarkAsMarked_TVShow.CheckedChanged
        chkMarkAsMarkedWithoutNFO_TVShow.Enabled = chkMarkAsMarked_TVShow.Checked
        If Not chkMarkAsMarked_TVShow.Checked Then chkMarkAsMarkedWithoutNFO_TVShow.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub MarkAsNew_TVEpisode_CheckedChanged() Handles chkMarkAsNew_TVEpisode.CheckedChanged
        chkMarkAsNewWithoutNFO_TVEpisode.Enabled = chkMarkAsNew_TVEpisode.Checked
        If Not chkMarkAsNew_TVEpisode.Checked Then chkMarkAsNewWithoutNFO_TVEpisode.Checked = False
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub MarkAsNew_TVShow_CheckedChanged() Handles chkMarkAsNew_TVShow.CheckedChanged
        chkMarkAsNewWithoutNFO_TVShow.Enabled = chkMarkAsNew_TVShow.Checked
        If Not chkMarkAsNew_TVShow.Checked Then chkMarkAsNewWithoutNFO_TVShow.Checked = False
        RaiseEvent SettingsChanged()
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
        With Master.eSettings.TVEpisodeFilterCustom
            .Clear()
            For Each r As DataGridViewRow In dgvTitleFilters_TVEpisode.Rows
                If r.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString.Trim) Then .Add(r.Cells(1).Value.ToString)
            Next
        End With
        'With Master.eSettings.TVEpisode.SourceSettings.TitleFilters
        '    .Clear()
        '    For Each r As DataGridViewRow In dgvTitleFilters_TVEpisode.Rows
        '        If r.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString.Trim) Then .Add(r.Cells(1).Value.ToString)
        '    Next
        'End With
    End Sub

    Private Sub Save_TitleFilters_TVShow()
        With Master.eSettings.TVShowFilterCustom
            .Clear()
            For Each r As DataGridViewRow In dgvTitleFilters_TVShow.Rows
                If r.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString.Trim) Then .Add(r.Cells(1).Value.ToString)
            Next
        End With
        'With Master.eSettings.TVShow.SourceSettings.TitleFilters
        '    .Clear()
        '    For Each r As DataGridViewRow In dgvTitleFilters_TVShow.Rows
        '        If r.Cells(1).Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Cells(1).Value.ToString.Trim) Then .Add(r.Cells(1).Value.ToString)
        '    Next
        'End With
    End Sub

    Private Sub SkipLessThan_TextChanged(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSkipLessThan.KeyPress
        e.Handled = StringUtils.IntegerOnly(e.KeyChar)
        RaiseEvent NeedsDBClean_TV()
        RaiseEvent NeedsDBUpdate_TV(-1)
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TitleFilters_Enabled_TVEpisode_CheckedChanged(sender As Object, e As EventArgs) Handles chkTitleFiltersEnabled_TVEpisode.CheckedChanged
        dgvTitleFilters_TVEpisode.Enabled = chkTitleFiltersEnabled_TVEpisode.Checked
        lblTitleFilters_TVEpisode.Enabled = chkTitleFiltersEnabled_TVEpisode.Checked
        btnTitleFilterDefaults_TVEpisode.Enabled = chkTitleFiltersEnabled_TVEpisode.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TitleFilters_Enabled_TVShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkTitleFiltersEnabled_TVShow.CheckedChanged
        dgvTitleFilters_TVShow.Enabled = chkTitleFiltersEnabled_TVShow.Checked
        lblTitleFilters_TVShow.Enabled = chkTitleFiltersEnabled_TVShow.Checked
        btnTitleFilterDefaults_TVShow.Enabled = chkTitleFiltersEnabled_TVShow.Checked
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TitleProperCase_TVEpisode_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTitleProperCase_TVEpisode.CheckedChanged
        RaiseEvent NeedsReload_TVEpisode()
        RaiseEvent SettingsChanged()
    End Sub

    Private Sub TitleProperCase_TVShow_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkTitleProperCase_TVShow.CheckedChanged
        RaiseEvent NeedsReload_TVShow()
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

    Private Sub LoadDefaults_TitleFilters_TVEpisode(sender As Object, e As EventArgs) Handles btnTitleFilterDefaults_TVEpisode.Click

    End Sub

    Private Sub EnableApplyButton(sender As Object, e As EventArgs) Handles chkVideoSourceFromFolder.CheckedChanged, chkTitleProperCase_TVShow.CheckedChanged, chkTitleProperCase_TVEpisode.CheckedChanged, chkResetNewOnExit_TVShow.CheckedChanged, chkResetNewBeforeDBUpdate_TVShow.CheckedChanged, chkOverwriteNfo.CheckedChanged, chkMarkAsNewWithoutNFO_TVShow.CheckedChanged, chkMarkAsNewWithoutNFO_TVEpisode.CheckedChanged, chkMarkAsMarkedWithoutNFO_TVShow.CheckedChanged, chkMarkAsMarkedWithoutNFO_TVEpisode.CheckedChanged, chkDateAddedIgnoreNFO.CheckedChanged, chkCleanLibraryAfterUpdate.CheckedChanged, cbSourcesDefaultsLanguage.SelectedIndexChanged, cbSourcesDefaultsEpisodeOrdering.SelectedIndexChanged, cbDateAddedDateTime.SelectedIndexChanged

    End Sub

    Private Sub LoadDefaults_TitleFilters_TVShow(sender As Object, e As EventArgs) Handles btnTitleFilterDefaults_TVShow.Click

    End Sub

#End Region 'Nested Types

End Class